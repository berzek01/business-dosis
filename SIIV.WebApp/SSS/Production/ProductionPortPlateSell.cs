// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.SSS.Production.ProductionPortPlateSell
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.SSS.BL;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.SSS.Production
{
  public class ProductionPortPlateSell : Page
  {
    protected HtmlHead Head1;
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
    protected DropDownList wddDocumentType;
    protected TextBox txtDocumentType;
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
      this.LoadParameters();
      this.wibAdd_Click((object) null, (EventArgs) null);
    }

    protected void wibAdd_Click(object sender, EventArgs e)
    {
      try
      {
        SSSQueriesBL sssQueriesBl = new SSSQueriesBL();
        DataTable dataTable1 = new DataTable();
        int int32 = Convert.ToInt32(this.txtIdProduction.Text);
        DataTable dataTable2 = sssQueriesBl.ProductPortPlateReport(int32, 2);
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
              if (Convert.ToInt32(dataTable2.Rows[index]["i_QuantityR"].ToString()) >= Convert.ToInt32(this.txtCantIdProd.Text.TrimEnd()))
              {
                this.TableReadOnly(dtListPortPlate);
                dtListPortPlate.Rows[index]["i_QuantityR"] = (object) (Convert.ToInt32(dataTable2.Rows[index]["i_QuantityR"]) - Convert.ToInt32(this.txtCantIdProd.Text.TrimEnd())).ToString();
                dtListPortPlate.Rows[index]["i_QuantityAV"] = (object) Convert.ToInt32(this.txtCantIdProd.Text.TrimEnd()).ToString();
                dtListPortPlate.Rows[index]["i_QuantityV"] = (object) (Convert.ToInt32(dtListPortPlate.Rows[index]["i_QuantityAV"]) + Convert.ToInt32(dtListPortPlate.Rows[index]["i_QuantityV"]));
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
                Message.SetMessage(this.lblMessageReadIds, enmMessageType.Warning, "La cantidad ingresada no puede ser mayor a: " + dataTable2.Rows[index]["i_QuantityR"].ToString());
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
    }

    protected void wibAceptar_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.wddDocumentType.SelectedValue == "-1")
          Message.SetMessage(this.lblMsg, enmMessageType.Warning, "Seleccione un documento");
        else if (this.txtDocumentType.Text == "" || this.txtDocumentType.Text == string.Empty)
          Message.SetMessage(this.lblMsg, enmMessageType.Warning, "Ingrese un número de documento");
        else if (this.ViewState["dt_ResultDetail"] != null)
        {
          DataTable dataTable1 = new DataTable();
          DataTable dataTable2 = (DataTable) this.ViewState["dt_ResultDetail"];
          StockMovementManagementBL movementManagementBl = new StockMovementManagementBL();
          StockMovement pobjStockMovement = this.ObjStockMovement();
          SSSQueriesBL sssQueriesBl = new SSSQueriesBL();
          int int32 = Convert.ToInt32(this.ViewState["i_PortaPlateProductionId"]);
          SystemUser systemUser = new SystemUser();
          DataTable dtStockMovementDetail = new DTStockMovementDetail().DataTableStockMovementDetail();
          DataTable pdtStockMovementDetail = new DTStockMovementDetail().DataTableStockMovementDetail();
          if (dataTable2.Rows.Count == 0)
          {
            Message.SetMessage(this.lblMsg, enmMessageType.Error, "No existe ningun registro en el detalle");
          }
          else
          {
            foreach (DataRow row1 in (InternalDataCollectionBase) dataTable2.Rows)
            {
              if (Convert.ToInt32(row1["i_QuantityAV"]) > 0)
              {
                DataRowCollection rows1 = dtStockMovementDetail.Rows;
                object[] objArray1 = new object[22];
                objArray1[1] = (object) 1;
                objArray1[3] = row1["i_ProductId"];
                objArray1[4] = (object) 2;
                objArray1[6] = (object) Convert.ToInt32(row1["i_QuantityAV"]);
                objArray1[7] = row1["v_Description"];
                objArray1[10] = (object) 1;
                rows1.Add(objArray1);
                foreach (DataRow row2 in (InternalDataCollectionBase) new DataTable()
                {
                  Columns = {
                    {
                      "i_ProductId",
                      typeof (string)
                    },
                    {
                      "i_QuantityAV",
                      typeof (string)
                    },
                    {
                      "v_Description",
                      typeof (string)
                    }
                  },
                  Rows = {
                    new object[3]
                    {
                      row1["i_ProductId"],
                      row1["i_QuantityAV"],
                      row1["v_Description"]
                    },
                    new object[3]
                    {
                      (object) "28",
                      row1["i_QuantityAV"],
                      (object) "BOLSA PORTAPLACAS"
                    },
                    new object[3]
                    {
                      (object) "29",
                      row1["i_QuantityAV"],
                      (object) "TORNILLOS DE 6x3/8"
                    },
                    new object[3]
                    {
                      (object) "30",
                      row1["i_QuantityAV"],
                      (object) "INSTRUCTIVO PORTAPLACAS"
                    }
                  }
                }.Rows)
                {
                  DataRowCollection rows2 = pdtStockMovementDetail.Rows;
                  object[] objArray2 = new object[22];
                  objArray2[1] = (object) 1;
                  objArray2[3] = row2["i_ProductId"];
                  objArray2[4] = (object) 2;
                  objArray2[6] = (object) Convert.ToInt32(row2["i_QuantityAV"]);
                  objArray2[7] = row2["v_Description"];
                  objArray2[10] = (object) 1;
                  rows2.Add(objArray2);
                }
              }
            }
            bool flag = false;
            int i_StockMovementIdIn = 0;
            int i_StockMovementIdOut;
            movementManagementBl.StockMovementInsertFactory(pobjStockMovement, pdtStockMovementDetail, i_StockMovementIdIn, out i_StockMovementIdOut);
            if (i_StockMovementIdOut > 0)
            {
              int num = 0;
              foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
                num += Convert.ToInt32(row["i_QuantityV"]);
              flag = Convert.ToInt32(dataTable2.Rows[0]["i_QuantityT"].ToString()) != num ? sssQueriesBl.ProductPortPlateUpdateStockId(int32, i_StockMovementIdOut, dtStockMovementDetail, 31, Convert.ToInt32(systemUser.i_SystemUserId)) : sssQueriesBl.ProductPortPlateUpdateStockId(int32, i_StockMovementIdOut, dtStockMovementDetail, 3, Convert.ToInt32(systemUser.i_SystemUserId));
              Message.SetMessage(this.lblMsg, enmMessageType.Success, "Venta realizada correctamente");
              this.txtIdProduction.Text = string.Empty;
              this.txtDocumentType.Text = string.Empty;
              this.txtObservacion.Text = string.Empty;
              this.wddDocumentType.SelectedValue = "-1";
              this.wddDocumentType.Enabled = false;
              this.wdgProductionPortPlate.DataSource = (object) null;
              this.wibAceptar.Enabled = false;
            }
            else
              Message.SetMessage(this.lblMsg, enmMessageType.Error, "Error al realizar la venta");
          }
        }
        else
          Message.SetMessage(this.lblMsg, enmMessageType.Error, "Error al tratar de grabar el detalle");
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

    protected void LoadParameters()
    {
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) ("" + "520"),
        (object) "",
        (object) "1",
        (object) "1"
      });
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (row["i_GroupId"].ToString() == "520")
            this.wddDocumentType.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        }
      }
      this.wddDocumentType.Items.Insert(0, new ListItem("- Seleccione - ", "-1"));
      this.wddDocumentType.SelectedValue = "-1";
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
      dtListPortPlate.Columns["i_QuantityR"].ReadOnly = false;
      dtListPortPlate.Columns["i_QuantityAV"].ReadOnly = false;
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
      stockMovement.i_MotiveMovementId = new int?(56);
      stockMovement.i_SupplierId = new int?();
      stockMovement.i_DocumentTypeId = new int?(Convert.ToInt32(this.wddDocumentType.SelectedValue));
      stockMovement.v_DocumentNumber = this.txtDocumentType.Text.TrimEnd();
      stockMovement.i_UserId = new int?(systemUser.i_SystemUserId);
      stockMovement.b_Checked = new bool?(true);
      stockMovement.v_Observation = this.txtObservacion.Text;
      stockMovement.d_InsertDate = new DateTime?(DateTime.Now);
      stockMovement.i_ProductionOrderId = new int?();
      return stockMovement;
    }
  }
}
