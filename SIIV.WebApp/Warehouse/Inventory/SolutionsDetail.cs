// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Inventory.SolutionsDetail
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Inventory.BL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Inventory
{
  public class SolutionsDetail : Page
  {
    private ShiftingInventoryQueriesBL ObjShiftingInventoryQueriesBL;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected Label lblIdSolicitud;
    protected Label lblPlaca;
    protected Label lblProducto;
    protected Label lblResult;
    protected Label lblSunarp;
    protected Label lblPago;
    protected Label lblSolicitud;
    protected Label lblDespacho;
    protected Label lblRecepcion;
    protected Label lblEntrega;
    protected Label lblStockSis;
    protected Label lblStockFisico;
    protected DropDownList ddlMotivo;
    protected DropDownList ddlSolucion;
    protected Button btnSave;
    protected Button btnReturn;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.Initialize();
      this.ddlSolucion.Enabled = false;
    }

    protected void ddlMotivo_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.ddlMotivo.SelectedValue == "-- Seleccione --")
        {
          this.ddlSolucion.Enabled = false;
        }
        else
        {
          this.ddlSolucion.Enabled = true;
          this.ObjShiftingInventoryQueriesBL = new ShiftingInventoryQueriesBL();
          DataTable dataTable = new DataTable();
          this.ddlSolucion.DataSource = (object) this.ObjShiftingInventoryQueriesBL.InventoryMotiveGetAll(this.Request.QueryString["v_Code"].ToString(), Convert.ToInt32(this.ddlMotivo.SelectedValue));
          this.ddlSolucion.DataTextField = "v_PossibleSolution";
          this.ddlSolucion.DataValueField = "i_PossibleSolution";
          this.ddlSolucion.DataBind();
          this.ddlSolucion.Items.Insert(0, "-- Seleccione --");
          this.ddlSolucion.SelectedValue = "0";
        }
      }
      catch (Exception ex)
      {
      }
    }

    public void Initialize()
    {
      try
      {
        this.lblIdSolicitud.Text = this.Request.QueryString["v_IdSolicitud"].ToString();
        this.lblPlaca.Text = this.Request.QueryString["v_Plate"].ToString();
        this.lblProducto.Text = this.Request.QueryString["v_ProductName"].ToString();
        this.lblResult.Text = this.Request.QueryString["v_TypeIncidence"].ToString();
        this.lblSunarp.Text = this.Request.QueryString["v_Sunarp"].ToString();
        this.lblPago.Text = this.Request.QueryString["v_Pago"].ToString();
        this.lblSolicitud.Text = this.Request.QueryString["v_Solicitud"].ToString();
        this.lblDespacho.Text = this.Request.QueryString["v_Despacho"].ToString();
        this.lblRecepcion.Text = this.Request.QueryString["v_Recepcion"].ToString();
        this.lblEntrega.Text = this.Request.QueryString["v_Entrega"].ToString();
        this.lblStockSis.Text = this.Request.QueryString["v_StockSistema"].ToString();
        this.lblStockFisico.Text = this.Request.QueryString["v_StockFisico"].ToString();
        this.ObjShiftingInventoryQueriesBL = new ShiftingInventoryQueriesBL();
        DataTable dataTable = new DataTable();
        this.ddlMotivo.DataSource = (object) this.ObjShiftingInventoryQueriesBL.InventoryMotiveGetAll(this.Request.QueryString["v_Code"].ToString(), 0);
        this.ddlMotivo.DataTextField = "v_Motive";
        this.ddlMotivo.DataValueField = "i_MotiveId";
        this.ddlMotivo.DataBind();
        this.ddlMotivo.Items.Insert(0, "-- Seleccione --");
        this.ddlMotivo.SelectedValue = "0";
      }
      catch (Exception ex)
      {
      }
    }
  }
}
