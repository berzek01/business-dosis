// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.SSS.Production.ProductionPortPlateDetail
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

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
  public class ProductionPortPlateDetail : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected Label Label1;
    protected DropDownList ddlPortPlate;
    protected TextBox txtCantidad;
    protected FilteredTextBoxExtender txtCantidad_Validator;
    protected Button wibAdd;
    protected HiddenField hdf_CantTotal;
    protected GridView wdgProductionPortPlate;
    protected TextBox txtObservacion;
    protected Button wibAceptar;
    protected Button wibCancelar;
    protected Label lblMsg;
    protected HiddenField HiddenField1;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadParameters();
      this.hdf_CantTotal.Value = "0";
      this.wibAceptar.Enabled = false;
    }

    protected void wibAdd_Click(object sender, EventArgs e)
    {
      this.lblMsg.Visible = false;
      DataTable dataTable1 = new DataTable();
      if (this.ddlPortPlate.SelectedValue == "0")
        Message.SetMessage(this.lblMsg, enmMessageType.Warning, "Seleccione un producto");
      else if (this.txtCantidad.Text == "" || this.txtCantidad.Text == string.Empty)
        Message.SetMessage(this.lblMsg, enmMessageType.Warning, "Ingrese una cantidad");
      else if (this.txtCantidad.Text == "0")
      {
        Message.SetMessage(this.lblMsg, enmMessageType.Warning, "La Cantidad a ingresar debe ser mayor a 0.");
      }
      else
      {
        DataTable dataTable2 = this.ViewState["dt_PortPlate"] != null ? (DataTable) this.ViewState["dt_PortPlate"] : this.DT();
        for (int index = 0; index < dataTable2.Rows.Count; ++index)
        {
          if (Convert.ToInt32(dataTable2.Rows[index]["IdProducto"].ToString()) == Convert.ToInt32(this.ddlPortPlate.SelectedValue))
          {
            this.txtCantidad.Text = string.Empty;
            this.ddlPortPlate.SelectedIndex = 0;
            Message.SetMessage(this.lblMsg, enmMessageType.Warning, "Este producto ya se encuentra agregado a la lista");
            return;
          }
        }
        DataRow row = dataTable2.NewRow();
        row["IdProducto"] = (object) Convert.ToInt32(this.ddlPortPlate.SelectedValue);
        row["PortaPlaca"] = (object) this.ddlPortPlate.SelectedItem.Text;
        row["Cantidad"] = (object) Convert.ToInt16(this.txtCantidad.Text);
        dataTable2.Rows.Add(row);
        this.ViewState["dt_PortPlate"] = (object) dataTable2;
        this.wdgProductionPortPlate.DataSource = (object) dataTable2;
        this.wdgProductionPortPlate.DataBind();
        this.hdf_CantTotal.Value = Convert.ToString(Convert.ToInt32(this.hdf_CantTotal.Value) + Convert.ToInt32(this.txtCantidad.Text));
        this.wibAceptar.Enabled = true;
        this.txtCantidad.Text = string.Empty;
        this.ddlPortPlate.SelectedIndex = 0;
      }
    }

    protected void wdgProductionPortPlate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      GridViewRow row1 = this.wdgProductionPortPlate.Rows[Convert.ToInt32(e.CommandArgument)];
      if (!e.CommandName.Equals("Delete", StringComparison.CurrentCulture))
        return;
      DataTable dataTable1 = new DataTable();
      DataTable dataTable2 = (DataTable) this.ViewState["dt_PortPlate"];
      string text1 = row1.Cells[1].Text;
      string text2 = row1.Cells[4].Text;
      foreach (DataRow row2 in (InternalDataCollectionBase) dataTable2.Rows)
      {
        if (row2["IdProducto"].ToString() == text1)
        {
          this.hdf_CantTotal.Value = Convert.ToString(Convert.ToInt32(this.hdf_CantTotal.Value) - Convert.ToInt32(text2));
          dataTable2.Rows.Remove(row2);
          break;
        }
      }
      this.wdgProductionPortPlate.DataSource = (object) dataTable2;
      this.wdgProductionPortPlate.DataBind();
      this.ViewState["dt_PortPlate"] = (object) dataTable2;
    }

    protected void wibAceptar_Click(object sender, EventArgs e)
    {
      try
      {
        SSSQueriesBL sssQueriesBl = new SSSQueriesBL();
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        if (this.ViewState["dt_PortPlate"] == null)
          return;
        PortPlateProduction ObjPortPlateProduction = new PortPlateProduction();
        DataTable dataTable1 = new DataTable();
        DataTable dt_Detail = (DataTable) this.ViewState["dt_PortPlate"];
        ObjPortPlateProduction.i_QuantityT = Convert.ToInt32(this.hdf_CantTotal.Value);
        ObjPortPlateProduction.d_ProductionDate = new DateTime?(DateTime.Now);
        ObjPortPlateProduction.i_ProductionStatus = new int?(0);
        ObjPortPlateProduction.v_Observation = this.txtObservacion.Text;
        ObjPortPlateProduction.i_Status = new int?(1);
        ObjPortPlateProduction.i_InsertUserId = new int?(systemUser.i_SystemUserId);
        ObjPortPlateProduction.d_InsertDate = new DateTime?(DateTime.Now);
        int i_ProductionPortaId;
        if (sssQueriesBl.RequirementPortPlateInsert(ObjPortPlateProduction, dt_Detail, out i_ProductionPortaId))
        {
          this.hdf_CantTotal.Value = "0";
          this.txtObservacion.Text = string.Empty;
          this.wdgProductionPortPlate.DataSource = (object) null;
          this.wibAceptar.Enabled = false;
          dt_Detail.Clear();
          this.ViewState.Remove("dt_PortPlate");
          Message.SetMessage(this.lblMsg, enmMessageType.Success, "Se grabo correctamente la solicitud de PortaPlaca");
          StockMovementManagementBL movementManagementBl = new StockMovementManagementBL();
          StockMovement pobjStockMovement = this.ObjStockMovement();
          if (this.HiddenField1.Value == "1")
            return;
          DataTable dataTable2 = new DataTable();
          DataTable dataTable3 = new DTStockMovementDetail().DataTableStockMovementDetail();
          DataTable byId = sssQueriesBl.ProductPortPlateDetailGetById(i_ProductionPortaId);
          if (Convert.ToInt32(byId.Rows[0]["i_ProductionStatus"].ToString()) != 0)
          {
            Message.SetMessage(this.lblMsg, enmMessageType.Error, "No puede enviar a serigrafiado, Verificar el estado de la solicitud");
          }
          else
          {
            foreach (DataRow row in (InternalDataCollectionBase) byId.Rows)
            {
              DataRowCollection rows = dataTable3.Rows;
              object[] objArray = new object[22];
              objArray[1] = (object) 1;
              objArray[3] = (object) 27;
              objArray[4] = (object) 1;
              objArray[6] = (object) Convert.ToInt32(row["i_QuantityT"]);
              objArray[7] = row["v_Description"];
              objArray[10] = (object) 1;
              rows.Add(objArray);
            }
            int i_StockMovementIdIn = 0;
            int i_StockMovementIdOut;
            movementManagementBl.StockMovementInsertFactory(pobjStockMovement, dataTable3, i_StockMovementIdIn, out i_StockMovementIdOut);
            if (i_StockMovementIdOut > 0)
            {
              if (!sssQueriesBl.ProductPortPlateUpdateStockId(i_ProductionPortaId, i_StockMovementIdOut, dataTable3, 1, Convert.ToInt32(systemUser.i_SystemUserId)))
              {
                Message.SetMessage(this.lblMsg, enmMessageType.Error, "Error al enviar a serigrafiar");
              }
              else
              {
                this.wibCancelar_Click((object) null, (EventArgs) null);
                Message.SetMessage(this.lblMsg, enmMessageType.Success, "Envio a serigrafiado realizado correctamente.");
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, enmMessageType.Error, ex.Message);
        this.hdf_CantTotal.Value = "0";
      }
    }

    protected void wibCancelar_Click(object sender, EventArgs e)
    {
      string script = "SendInfoPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    public void LoadParameters()
    {
      SSSQueriesBL sssQueriesBl = new SSSQueriesBL();
      DataTable dataTable = new DataTable();
      this.FillPortPlateWdd(sssQueriesBl.ProductPortPlateGet());
      this.ddlPortPlate.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
      this.ddlPortPlate.SelectedIndex = 0;
    }

    private void FillPortPlateWdd(DataTable dt_Result)
    {
      if (dt_Result == null || dt_Result.Rows.Count == 0)
        return;
      this.ddlPortPlate.DataSource = (object) dt_Result;
      this.ddlPortPlate.DataTextField = "v_Description";
      this.ddlPortPlate.DataValueField = "i_ProductId";
      this.ddlPortPlate.DataBind();
    }

    private DataTable DT()
    {
      return new DataTable()
      {
        Columns = {
          {
            "IdProducto",
            typeof (int)
          },
          {
            "PortaPlaca",
            typeof (string)
          },
          {
            "Cantidad",
            typeof (int)
          }
        }
      };
    }

    private StockMovement ObjStockMovement()
    {
      StockMovement stockMovement = new StockMovement();
      SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
      stockMovement.i_WarehouseId = new int?(38);
      stockMovement.i_MotiveMovementId = new int?(55);
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

    protected void wdgProductionPortPlate_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgProductionPortPlate_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
  }
}
