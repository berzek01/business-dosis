// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.SSS.Production.ProductionPortPlateRefund
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.SSS.BL;
using SIIV.Warehouse.BL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.SSS.Production
{
  public class ProductionPortPlateRefund : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected Label Label1;
    protected TextBox txtIdProduction;
    protected FilteredTextBoxExtender txtCantidad_Validator;
    protected Button wibAdd;
    protected GridView wdgProductionPortPlate;
    protected HtmlTableCell tdReadIdProd;
    protected TextBox txtIdProd;
    protected FilteredTextBoxExtender txtIdProd_FilteredTextBoxExtender;
    protected TextBox txtCantIdProd;
    protected FilteredTextBoxExtender txtCantIdProd_FilteredTextBoxExtender;
    protected Button wibReadIds;
    protected Label lblMessageReadIds;
    protected TextBox txtObservacion;
    protected Button wibAceptar;
    protected Button wibCancelar;
    protected Label lblMsg;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      if (this.Request.QueryString["Id_ProductioPorta"] != null)
        this.txtIdProduction.Text = this.Request.QueryString["Id_ProductioPorta"].ToString();
      this.wibAdd_Click((object) null, (EventArgs) null);
    }

    protected void wibAdd_Click(object sender, EventArgs e)
    {
      try
      {
        SSSQueriesBL sssQueriesBl = new SSSQueriesBL();
        DataTable dataTable1 = new DataTable();
        int int32 = Convert.ToInt32(this.txtIdProduction.Text);
        DataTable dataTable2 = sssQueriesBl.ProductPortPlateReport(int32, 1);
        if (dataTable2.Rows.Count > 0)
        {
          this.ViewState["i_PortaPlateProductionId"] = (object) int32;
          this.ViewState["dt_ResultDetailInicial"] = (object) dataTable2;
          this.ViewState["dt_ResultDetail"] = (object) dataTable2;
          this.wdgProductionPortPlate.DataSource = (object) dataTable2;
          this.wdgProductionPortPlate.DataBind();
        }
        else
        {
          this.txtIdProduction.Text = string.Empty;
          this.txtIdProduction.Focus();
          this.wibAceptar.Enabled = false;
          Message.SetMessage(this.lblMsg, enmMessageType.Warning, "Id de Produccion no encontrado");
        }
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, enmMessageType.Error, "Error*****<br>" + ex.Message);
      }
    }

    protected void wibReadIds_Click(object sender, EventArgs e)
    {
      this.lblMessageReadIds.Visible = false;
      if (!this.Validate() || this.ViewState["dt_ResultDetailInicial"] == null || this.ViewState["dt_ResultDetail"] == null)
        return;
      DataTable dataTable1 = new DataTable();
      DataTable dataTable2 = (DataTable) this.ViewState["dt_ResultDetailInicial"];
      DataTable dataTable3 = new DataTable();
      DataTable dtListPortPlate = (DataTable) this.ViewState["dt_ResultDetail"];
      if (dtListPortPlate.Rows.Count > 0)
      {
        if (this.txtCantIdProd.Text != "0")
        {
          for (int index = 0; index < dtListPortPlate.Rows.Count; ++index)
          {
            if (Convert.ToInt32(dtListPortPlate.Rows[index]["i_ProductId"].ToString()) == Convert.ToInt32(this.txtIdProd.Text.TrimEnd()))
            {
              if (Convert.ToInt32(dataTable2.Rows[index]["i_QuantityP"].ToString()) >= Convert.ToInt32(this.txtCantIdProd.Text.TrimEnd()))
              {
                this.TableReadOnly(dtListPortPlate);
                dtListPortPlate.Rows[index]["i_QuantityP"] = (object) (Convert.ToInt32(dataTable2.Rows[index]["i_QuantityP"]) - Convert.ToInt32(this.txtCantIdProd.Text.TrimEnd())).ToString();
                dtListPortPlate.Rows[index]["i_QuantityAR"] = (object) Convert.ToInt32(this.txtCantIdProd.Text.TrimEnd()).ToString();
                dtListPortPlate.Rows[index]["i_QuantityR"] = (object) (Convert.ToInt32(dtListPortPlate.Rows[index]["i_QuantityAR"]) + Convert.ToInt32(dtListPortPlate.Rows[index]["i_QuantityR"]));
                this.ViewState["dt_ResultDetail"] = (object) dtListPortPlate;
                this.wdgProductionPortPlate.DataSource = (object) dtListPortPlate;
                this.wdgProductionPortPlate.DataBind();
                this.wibAceptar.Enabled = true;
              }
              else
              {
                this.txtIdProd.Text = string.Empty;
                this.txtCantIdProd.Text = string.Empty;
                this.txtIdProd.Focus();
                Message.SetMessage(this.lblMessageReadIds, enmMessageType.Warning, "La cantidad ingresada no puede ser mayor a: " + dataTable2.Rows[index]["i_QuantityP"].ToString());
                return;
              }
            }
          }
          this.txtIdProd.Text = string.Empty;
          this.txtCantIdProd.Text = string.Empty;
          this.txtIdProd.Focus();
        }
        else
          Message.SetMessage(this.lblMessageReadIds, enmMessageType.Warning, "La cantidad ingresada debe ser mayor a 0.");
      }
      else
        Message.SetMessage(this.lblMessageReadIds, enmMessageType.Warning, "No existe ningun registro en la solicitud seleccionada");
    }

    protected void wibAceptar_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.ViewState["dt_ResultDetail"] != null)
        {
          DataTable dataTable1 = new DataTable();
          DataTable dataTable2 = (DataTable) this.ViewState["dt_ResultDetail"];
          StockMovementManagementBL movementManagementBl = new StockMovementManagementBL();
          StockMovement pobjStockMovement = this.ObjStockMovement();
          SSSQueriesBL sssQueriesBl = new SSSQueriesBL();
          int int32_1 = Convert.ToInt32(this.ViewState["i_PortaPlateProductionId"]);
          SystemUser systemUser = new SystemUser();
          DataTable dataTable3 = new DTStockMovementDetail().DataTableStockMovementDetail();
          if (dataTable2.Rows.Count == 0)
          {
            Message.SetMessage(this.lblMsg, enmMessageType.Error, "No existe ningun registro en el detalle");
          }
          else
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
            {
              if (Convert.ToInt32(row["i_QuantityAR"]) > 0)
              {
                DataRowCollection rows = dataTable3.Rows;
                object[] objArray = new object[22];
                objArray[1] = (object) 1;
                objArray[3] = row["i_ProductId"];
                objArray[4] = (object) 2;
                objArray[6] = (object) Convert.ToInt32(row["i_QuantityAR"]);
                objArray[7] = row["v_Description"];
                objArray[10] = (object) 1;
                rows.Add(objArray);
              }
            }
            bool flag = false;
            int int32_2 = Convert.ToInt32(dataTable2.Rows[0]["i_StockMovementId"].ToString());
            int i_StockMovementIdOut;
            movementManagementBl.StockMovementInsertFactory(pobjStockMovement, dataTable3, int32_2, out i_StockMovementIdOut);
            if (i_StockMovementIdOut > 0)
            {
              int num = 0;
              foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
                num += Convert.ToInt32(row["i_QuantityR"]);
              flag = Convert.ToInt32(dataTable2.Rows[0]["i_QuantityT"].ToString()) != num ? sssQueriesBl.ProductPortPlateUpdateStockId(int32_1, i_StockMovementIdOut, dataTable3, 21, Convert.ToInt32(systemUser.i_SystemUserId)) : sssQueriesBl.ProductPortPlateUpdateStockId(int32_1, i_StockMovementIdOut, dataTable3, 2, Convert.ToInt32(systemUser.i_SystemUserId));
              Message.SetMessage(this.lblMsg, enmMessageType.Success, "Retorno realizado correctamente");
              this.txtIdProduction.Text = string.Empty;
              this.txtObservacion.Text = string.Empty;
              this.wdgProductionPortPlate.DataSource = (object) null;
              this.wibAceptar.Enabled = false;
            }
            else
              Message.SetMessage(this.lblMsg, enmMessageType.Error, "Error al realizar el retorno");
          }
        }
        else
          Message.SetMessage(this.lblMsg, enmMessageType.Error, "Error al realizar el retorno");
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, enmMessageType.Error, "Error...br>" + ex.Message);
      }
    }

    protected void wibCancelar_Click(object sender, EventArgs e)
    {
      string script = "SendInfoPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private bool Validate()
    {
      bool flag = false;
      if (string.IsNullOrEmpty(this.txtIdProd.Text))
        Message.SetMessage(this.lblMessageReadIds, enmMessageType.Error, "Ingrese un producto válido");
      else if (string.IsNullOrEmpty(this.txtCantIdProd.Text))
        Message.SetMessage(this.lblMessageReadIds, enmMessageType.Error, "Ingrese una cantidad válida");
      else
        flag = true;
      return flag;
    }

    protected void TableReadOnly(DataTable dtListPortPlate)
    {
      dtListPortPlate.Columns["i_PortPlateProductionId"].ReadOnly = false;
      dtListPortPlate.Columns["i_StockMovementId"].ReadOnly = false;
      dtListPortPlate.Columns["v_Alias"].ReadOnly = false;
      dtListPortPlate.Columns["i_ProductId"].ReadOnly = false;
      dtListPortPlate.Columns["v_Description"].ReadOnly = false;
      dtListPortPlate.Columns["i_QuantityP"].ReadOnly = false;
      dtListPortPlate.Columns["i_QuantityAR"].ReadOnly = false;
      dtListPortPlate.Columns["i_QuantityR"].ReadOnly = false;
      dtListPortPlate.Columns["i_QuantityV"].ReadOnly = false;
      dtListPortPlate.Columns["i_QuantityT"].ReadOnly = false;
      dtListPortPlate.Columns["v_Observation"].ReadOnly = false;
      dtListPortPlate.Columns["i_ProductionStatus"].ReadOnly = false;
    }

    private StockMovement ObjStockMovement()
    {
      StockMovement stockMovement = new StockMovement();
      SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
      stockMovement.i_WarehouseId = new int?(38);
      stockMovement.i_MotiveMovementId = new int?(53);
      stockMovement.i_SupplierId = new int?();
      stockMovement.i_DocumentTypeId = new int?();
      stockMovement.v_DocumentNumber = (string) null;
      stockMovement.i_UserId = new int?(systemUser.i_SystemUserId);
      stockMovement.b_Checked = new bool?(true);
      stockMovement.v_Observation = this.txtObservacion.Text;
      stockMovement.d_InsertDate = new DateTime?(DateTime.Now);
      stockMovement.i_ProductionOrderId = new int?();
      return stockMovement;
    }
  }
}
