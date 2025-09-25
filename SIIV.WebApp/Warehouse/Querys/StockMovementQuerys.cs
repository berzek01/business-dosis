// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Querys.StockMovementQuerys
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Warehouse.BL;
using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Querys
{
  public class StockMovementQuerys : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected DropDownList wddWarehouse1;
    protected Fecha wdpDateIni;
    protected Fecha wdpDateFin;
    protected RadioButtonList rblFlujo;
    protected DropDownList wddMovementType;
    protected Button btnSearch;
    protected DropDownList wddMotiveMovement;
    protected GridView wdgStockMovementReport;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadWarehouse();
      this.LoadMovementType(int.Parse(this.rblFlujo.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture));
      this.LoadMotiveMovement(Convert.ToInt32(this.wddMovementType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
      this.SetDatePicker();
    }

    protected void wddMovementType_SelectionChanged(object sender, EventArgs e)
    {
      this.LoadMotiveMovement(Convert.ToInt32(this.wddMovementType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
    }

    protected void btnSearch_Click(object sender, EventArgs e) => this.SearchStockMovementReport();

    protected void rblFlujo_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.LoadMovementType(int.Parse(this.rblFlujo.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture));
    }

    private void LoadWarehouse()
    {
      this.wddWarehouse1.DataSource = (object) new WarehouseQueriesBL().GetWarehouseBy(0, "", (this.Session["SystemUser"] as SystemUser).i_LocationId, -1);
      this.wddWarehouse1.DataTextField = "v_Description";
      this.wddWarehouse1.DataValueField = "i_WarehouseId";
      this.wddWarehouse1.DataBind();
      this.wddWarehouse1.Items.Insert(0, new ListItem("- Seleccione - ", "0"));
      this.wddWarehouse1.SelectedValue = "0";
    }

    private void LoadMovementType(int pintflowId)
    {
      int int32 = Convert.ToInt32((object) (this.Session["SystemUser"] as SystemUser).i_CompanyId, (IFormatProvider) CultureInfo.CurrentCulture);
      this.wddMovementType.DataSource = (object) new MovementTypeQueriesBL().GetMovementTypeBy(0, "", pintflowId, -1, 0, int32);
      this.wddMovementType.DataTextField = "v_Description";
      this.wddMovementType.DataValueField = "i_MovementTypeId";
      this.wddMovementType.DataBind();
      this.wddMovementType.Items.Insert(0, new ListItem("- Seleccione - ", "0"));
      this.wddMovementType.SelectedValue = "0";
    }

    private void LoadMotiveMovement(int pintMovementTypeId)
    {
      int int32 = Convert.ToInt32((object) (this.Session["SystemUser"] as SystemUser).i_CompanyId, (IFormatProvider) CultureInfo.CurrentCulture);
      this.wddMotiveMovement.DataSource = (object) new MotiveMovementQueriesBL().GetMotiveMovementBy(0, pintMovementTypeId, string.Empty, 0, int32);
      this.wddMotiveMovement.DataTextField = "v_Description";
      this.wddMotiveMovement.DataValueField = "i_MotiveMovementId";
      this.wddMotiveMovement.DataBind();
      this.wddMotiveMovement.Items.Insert(0, new ListItem("- Seleccione - ", "-1"));
      this.wddMotiveMovement.SelectedValue = "-1";
    }

    private void SearchStockMovementReport()
    {
      this.wdgStockMovementReport.DataSource = (object) new StockMovementQueriesBL().GetStockMovementReportBy(0, 0, Convert.ToInt32(this.wddMotiveMovement.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.wddWarehouse1.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
      this.wdgStockMovementReport.DataBind();
    }

    private void SetDatePicker()
    {
      this.wdpDateIni.Value = DateTime.Now.AddMonths(-1);
      this.wdpDateFin.Value = DateTime.Now;
    }
  }
}
