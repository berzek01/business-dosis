// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Delivery.Reports.ReportProgramationPending
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Delivery.Reports
{
  public class ReportProgramationPending : Page
  {
    private SystemUser objUserBE;
    protected UpdatePanel UpdatePanel1;
    protected HtmlTable TblFecha;
    protected Fecha wdpStartDate;
    protected Label Label8;
    protected Fecha wdpEndDate;
    protected Button wibSearch0;
    protected GridView gv_List;
    protected Pager custPagerAP;
    protected Button wibExcel;
    protected Label lblMessage;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      this.SetDatePicker();
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchDeliveryPlates();

    protected void custPagerAP_PageChanged(object sender, CustomPageChangeArgs e)
    {
      this.SearchDeliveryPlatesList(Convert.ToDateTime(this.wdpStartDate.Value), Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString()), false);
    }

    protected void wibExport_Click(object sender, EventArgs e)
    {
      string script = "Export();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void Button1_Click(object sender, EventArgs e) => this.Export(this.ExportList());

    private void SearchDeliveryPlates()
    {
      this.lblMessage.Visible = false;
      try
      {
        this.SearchDeliveryPlatesList(Convert.ToDateTime(this.wdpStartDate.Value), Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString()), true);
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

    private void SearchDeliveryPlatesList(
      DateTime d_StartDate,
      DateTime d_EndDate,
      bool pboolLoadPager)
    {
      int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerAP.CurrentPageNumber;
      int pintMaxRows = this.custPagerAP.CurrentPageSize == 0 ? 10 : this.custPagerAP.CurrentPageSize;
      this.objUserBE = this.Session["SystemUser"] as SystemUser;
      int pintTotalRows;
      DataTable dataTable = new RequirementQueriesBL().SearchDeliveryPlatePaymentReport(this.objUserBE.i_SystemUserId, d_StartDate, d_EndDate, pintStartRowIndex, pintMaxRows, out pintTotalRows);
      if (dataTable == null || dataTable.Rows.Count == 0)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
        this.HidePopup();
      }
      else
      {
        this.lblMessage.Visible = false;
        this.wibExcel.Enabled = true;
      }
      int num = pintTotalRows;
      this.gv_List.DataSource = (object) dataTable;
      this.gv_List.DataBind();
      this.custPagerAP.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
      this.custPagerAP.TotalRecordCount = pintTotalRows;
      if (!pboolLoadPager)
        return;
      this.custPagerAP.LoadPager();
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private void SetDatePicker()
    {
      DateTime now = DateTime.Now;
      DateTime dateTime = DateTime.Now.AddDays(7.0);
      this.wdpStartDate.Value = now;
      this.wdpEndDate.Value = dateTime;
    }

    private DataTable ExportList()
    {
      DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
      DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
      DataTable dataTable = new DataTable();
      this.objUserBE = this.Session["SystemUser"] as SystemUser;
      try
      {
        dataTable = new RequirementQueriesBL().SearchDeliveryPlatePaymentReport(this.objUserBE.i_SystemUserId, dateTime1, dateTime2, 0, 0, out int _);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, ex.Message);
      }
      return dataTable;
    }

    private void Export(DataTable dt_Result)
    {
      try
      {
        ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
        List<ClassColumns> classColumnsList = new List<ClassColumns>();
        foreach (DataControlField column in (StateManagedCollection) this.gv_List.Columns)
        {
          if (column.Visible && column.GetType().Name == "BoundField")
          {
            BoundField boundField = (BoundField) column;
            classColumnsList.Add(new ClassColumns(boundField.DataField, 1, (int) column.ControlStyle.Width.Value, boundField.HeaderText));
          }
        }
        exportToExcelDataGrid.clsTitle = classColumnsList;
        exportToExcelDataGrid.AgregarHojaLibro(dt_Result, "Consulta Universal");
        exportToExcelDataGrid.CerrarLibro();
        byte[] buffer = exportToExcelDataGrid.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=ConsultaUniversal.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}
