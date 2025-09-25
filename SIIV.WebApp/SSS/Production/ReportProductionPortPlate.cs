// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.SSS.Production.ReportProductionPortPlate
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.SSS.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.SSS.Production
{
  public class ReportProductionPortPlate : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected Fecha wdpDateIni;
    protected Fecha wdpDateFin;
    protected Button wibSearch;
    protected Button wibExport;
    protected GridView wdgPortPlateList;
    protected Pager custPagerRPPP;
    protected Label lblMessage;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.SetDatePicker();
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchReport();

    protected void custPagerRPPP_PageChanged(object sender, CustomPageChangeArgs e)
    {
      this.SearchReportPPList(this.wdpDateIni.Text, this.wdpDateFin.Text, false);
    }

    protected void wibExport_Click(object sender, EventArgs e)
    {
      string script = "ExportExcelAll();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void Button1_Click(object sender, EventArgs e) => this.Export(this.ExportList());

    private void SetDatePicker()
    {
      this.wdpDateIni.Value = DateTime.Now.AddDays(-30.0);
      this.wdpDateFin.Value = DateTime.Now;
    }

    private void SearchReport()
    {
      try
      {
        this.SearchReportPPList(this.wdpDateIni.Text, this.wdpDateFin.Text, true);
        this.HidePopup();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, ex.Message);
        this.HidePopup();
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SearchReportPPList(string dFecIni, string dFecFin, bool pboolLoadPager)
    {
      try
      {
        int pintstartRowIndex = pboolLoadPager ? 1 : this.custPagerRPPP.CurrentPageNumber;
        int pintmaxRows = this.custPagerRPPP.CurrentPageSize == 0 ? 10 : this.custPagerRPPP.CurrentPageSize;
        int pinttotalRows;
        DataTable dataTable = new SSSQueriesBL().SearchReportProductPortPlate(dFecIni, dFecFin, pintstartRowIndex, pintmaxRows, out pinttotalRows);
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
          this.HidePopup();
        }
        else
          this.lblMessage.Visible = false;
        int num = pinttotalRows;
        this.wdgPortPlateList.DataSource = (object) dataTable;
        this.wdgPortPlateList.DataBind();
        this.custPagerRPPP.TotalPages = num % pintmaxRows == 0 ? num / pintmaxRows : num / pintmaxRows + 1;
        this.custPagerRPPP.TotalRecordCount = pinttotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerRPPP.LoadPager();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, "Error... Consulte con el Administrador. " + ex.Message);
      }
    }

    private DataTable ExportList()
    {
      DataTable dataTable = new DataTable();
      try
      {
        dataTable = new SSSQueriesBL().SearchReportProductPortPlate(this.wdpDateIni.Text, this.wdpDateFin.Text, 0, 0, out int _);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, ex.Message);
      }
      return dataTable;
    }

    private void Export(DataTable dt_Result)
    {
      ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
      exportToExcelDataGrid.clsTitle = new List<ClassColumns>()
      {
        new ClassColumns("i_PortPlateProductionId", 1, 40, "ID"),
        new ClassColumns("Producto", 1, 100, "Producto"),
        new ClassColumns("Cantidad", 1, 100, "Cantidad"),
        new ClassColumns("CantidadGeneral", 1, 150, "Cantidad General"),
        new ClassColumns("Fecha", 1, 200, "Fecha"),
        new ClassColumns("Estado", 1, 100, "Estado")
      };
      exportToExcelDataGrid.AgregarHojaLibro(dt_Result, "Hoja", "Reporte PortaPlaca");
      exportToExcelDataGrid.CerrarLibro();
      byte[] buffer = exportToExcelDataGrid.DownloadByte();
      this.Response.Clear();
      this.Response.AddHeader("content-disposition", "attachment; filename=ReportePortaPlacas.xls");
      this.Response.BinaryWrite(buffer);
      this.Response.End();
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
