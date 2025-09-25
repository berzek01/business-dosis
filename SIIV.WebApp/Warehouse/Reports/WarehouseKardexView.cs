// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Reports.WarehouseKardexView
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using SIIV.BE.CustomCode;
using SIIV.Warehouse.BL;
using SIIV.WebApp.Warehouse.Rpt;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

#nullable disable
namespace SIIV.WebApp.Warehouse.Reports
{
  public class WarehouseKardexView : Page
  {
    protected UpdatePanel UpdatePanel2;
    protected Button btnRefresh;
    protected Button btnExport;
    protected Button btnReturn;
    protected CrystalReportViewer CrystalReportViewer1;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadReport();
    }

    private void LoadReport()
    {
      ProductKardex pobjProductKardex = (ProductKardex) this.Session["ProductKardex"];
      XElement xelement = new XElement((XName) "ProductsList", (object) new XElement((XName) "Products"));
      DataTable dataTable = (DataTable) this.Session["sedtProductAdd"];
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          XElement content = new XElement((XName) "Products", (object) new XElement((XName) "i_ProductId", (object) Convert.ToInt32(row["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture)));
          xelement.Element((XName) "Products").Add((object) content);
        }
      }
      pobjProductKardex.s_ProductsList = dataTable == null || dataTable.Rows.Count == 0 ? "" : xelement.ToString();
      try
      {
        DataTable productKardex = new ProductWarehouseKardexBL().GetProductKardex(pobjProductKardex);
        productKardex.TableName = "Tabla";
        this.Session["DataSource"] = (object) productKardex;
        this.RefreshReport();
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    protected void CrystalReportViewer1_Navigate(object source, NavigateEventArgs e)
    {
      this.RefreshReport();
    }

    private void Export()
    {
      DataTable dataTable = this.Session["DataSource"] as DataTable;
      ReportKardex reportKardex = new ReportKardex();
      reportKardex.SetDataSource(dataTable);
      reportKardex.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "KardexValorizado");
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("~/Warehouse/Reports/WarehouseKardex.aspx");
    }

    protected void btnExport_Click(object sender, EventArgs e) => this.Export();

    protected void btnRefresh_Click(object sender, EventArgs e) => this.RefreshReport();

    private void RefreshReport()
    {
      ReportKardex reportKardex = new ReportKardex();
      DataTable dataTable = this.Session["DataSource"] as DataTable;
      reportKardex.SetDataSource(dataTable);
      this.CrystalReportViewer1.ReportSource = (object) reportKardex;
      this.CrystalReportViewer1.RefreshReport();
    }
  }
}
