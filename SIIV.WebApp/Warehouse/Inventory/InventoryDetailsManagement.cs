// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Inventory.InventoryDetailsManagement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.Inventory.BL;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Inventory
{
  public class InventoryDetailsManagement : Page
  {
    private int intInventoryHistoricId;
    protected UpdatePanel UpdatePanel1;
    protected Label lblSincronizated;
    protected Label lblTS;
    protected Label lblRegularizated;
    protected Label lblTR;
    protected Label lblInserted;
    protected Label lblTI;
    protected Label lblCuestionated;
    protected Label lblTC;
    protected Label lblTotalSend;
    protected Label lblTSD;
    protected Button btnSend;
    protected Label lblCorrect;
    protected Label lblTCo;
    protected Button btnCorrect;
    protected Label lblMissing;
    protected Label lblTM;
    protected Button btnMissing;
    protected Label lblLeftover;
    protected Label lblTL;
    protected Button btnLeftover;
    protected Label lblInconsistent;
    protected Label lblTIn;
    protected Button btnInconsistent;
    protected Label lblObserved;
    protected Label lblTO;
    protected GridView wdgInventoryHistoricDetails;
    protected Pager ucPagerInventoryHistoricDetails;
    protected Button wibReturn;

    public void initialLoad(bool pboolLoadPager)
    {
      int startRowIndex = pboolLoadPager ? 1 : this.ucPagerInventoryHistoricDetails.CurrentPageNumber;
      int maxRows = this.ucPagerInventoryHistoricDetails.CurrentPageSize == 0 ? 10 : this.ucPagerInventoryHistoricDetails.CurrentPageSize;
      DataTable dataTable1 = new DataTable();
      this.intInventoryHistoricId = Convert.ToInt32(this.Request.QueryString["InventoryHistoricId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
      ShiftingInventoryQueriesBL inventoryQueriesBl = new ShiftingInventoryQueriesBL();
      int totalRows;
      this.wdgInventoryHistoricDetails.DataSource = (object) inventoryQueriesBl.InventoryProductStagesList(this.intInventoryHistoricId, startRowIndex, maxRows, out totalRows);
      this.wdgInventoryHistoricDetails.DataBind();
      this.ucPagerInventoryHistoricDetails.TotalPages = totalRows % maxRows == 0 ? totalRows / maxRows : totalRows / maxRows + 1;
      this.ucPagerInventoryHistoricDetails.TotalRecordCount = totalRows;
      DataTable dataTable2 = inventoryQueriesBl.InventoryHistoricById(this.intInventoryHistoricId);
      if (dataTable2.Rows.Count > 0)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
        {
          this.lblTS.Text = row["i_DownloadSincronizated"].ToString();
          this.lblTI.Text = row["i_DownloadInsert"].ToString();
          this.lblTR.Text = row["i_DownloadRegularized"].ToString();
          this.lblTSD.Text = row["i_DownloadSend"].ToString();
          this.lblTCo.Text = row["i_DownloadCorrect"].ToString();
          this.lblTM.Text = row["i_DownloadMissing"].ToString();
          this.lblTL.Text = row["i_DownloadLeftover"].ToString();
          this.lblTIn.Text = row["i_DownloadInconsistent"].ToString();
          this.lblTO.Text = (int.Parse(this.lblTM.Text, (IFormatProvider) CultureInfo.CurrentCulture) + int.Parse(this.lblTL.Text, (IFormatProvider) CultureInfo.CurrentCulture) + int.Parse(this.lblTIn.Text, (IFormatProvider) CultureInfo.CurrentCulture)).ToString((IFormatProvider) CultureInfo.CurrentCulture);
          this.lblTC.Text = (int.Parse(this.lblTO.Text, (IFormatProvider) CultureInfo.CurrentCulture) - int.Parse(this.lblTR.Text, (IFormatProvider) CultureInfo.CurrentCulture)).ToString((IFormatProvider) CultureInfo.CurrentCulture);
        }
      }
      if (!pboolLoadPager)
        return;
      this.ucPagerInventoryHistoricDetails.LoadPager();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.initialLoad(true);
    }

    public void CargarReporte(
      string strReporte,
      int intInventoryHistoric,
      int Tipo,
      int Incidencia)
    {
      this.Response.Redirect("MainRSInventario.aspx?InventoryHistoricId=" + Convert.ToString(intInventoryHistoric, (IFormatProvider) CultureInfo.CurrentCulture) + "&ReportName=" + strReporte + "&Type=" + Convert.ToString(Tipo, (IFormatProvider) CultureInfo.CurrentCulture) + "&TypeIncidence=" + Convert.ToString(Incidencia, (IFormatProvider) CultureInfo.CurrentCulture), true);
    }

    protected void ucPagerInventoryHistoricDetails_PageChanged(
      object sender,
      CustomPageChangeArgs e)
    {
      this.initialLoad(false);
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
      this.intInventoryHistoricId = Convert.ToInt32(this.Request.QueryString["InventoryHistoricId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
      this.CargarReporte("Reporte Resumen Inventario", this.intInventoryHistoricId, 1, 0);
    }

    protected void btnCorrect_Click(object sender, EventArgs e)
    {
      this.intInventoryHistoricId = Convert.ToInt32(this.Request.QueryString["InventoryHistoricId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
      this.CargarReporte("Reporte Detalle Inventario", this.intInventoryHistoricId, 2, 12);
    }

    protected void btnMissing_Click(object sender, EventArgs e)
    {
      this.intInventoryHistoricId = Convert.ToInt32(this.Request.QueryString["InventoryHistoricId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
      this.CargarReporte("Reporte Detalle Inventario", this.intInventoryHistoricId, 2, 13);
    }

    protected void btnLeftover_Click(object sender, EventArgs e)
    {
      this.intInventoryHistoricId = Convert.ToInt32(this.Request.QueryString["InventoryHistoricId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
      this.CargarReporte("Reporte Detalle Inventario", this.intInventoryHistoricId, 2, 14);
    }

    protected void btnInconsistent_Click(object sender, EventArgs e)
    {
      this.intInventoryHistoricId = Convert.ToInt32(this.Request.QueryString["InventoryHistoricId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
      this.CargarReporte("Reporte Detalle Inventario", this.intInventoryHistoricId, 2, 15);
    }

    protected void wibReturn_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("InventoryManagement.aspx");
    }
  }
}
