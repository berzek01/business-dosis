// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.SSS.Projection.ProjectionReports
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.SSS.BL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.SSS.Projection
{
  public class ProjectionReports : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected Fecha wdpDateIni;
    protected Fecha wdpDateFin;
    protected Button wibSearch;
    protected Button wibExport;
    protected GridView wdgInventaryList;
    protected GridView wdgDecreaseList;
    protected Label lblMessageInventary;
    protected Label lblMessageDecrease;
    protected GridView wdgProductionList;
    protected GridView wdgDemandList;
    protected Label lblMessageProduction;
    protected Label lblMessageDemand;
    protected GridView wdgDemandProList;
    protected Label lblMessageDemandPro;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.SetDatePicker();
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        this.SearchInventary();
        this.SearchDecrease();
        this.SearchProduction();
        this.SearchDemand();
        this.SearchDemandPro();
        this.SearchInventary();
        this.SearchDecrease();
        this.SearchProduction();
        this.SearchDemand();
        this.SearchDemandPro();
      }
      catch (Exception ex)
      {
        throw;
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void wdgInventaryList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (this.ViewState["ColumnsCountInventary"] == null)
        return;
      int int32 = Convert.ToInt32(this.ViewState["ColumnsCountInventary"].ToString());
      for (int index = 2; index < int32; ++index)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          if (index % 2 == 0)
            e.Row.Cells[index].Text = "SIIV al " + Convert.ToDateTime(e.Row.Cells[index].Text.Substring(0, 10)).ToString("dd/MM/yyyy");
          else
            e.Row.Cells[index].Text = "Teorico al " + Convert.ToDateTime(e.Row.Cells[index].Text.Substring(0, 10)).ToString("dd/MM/yyyy");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
          e.Row.Cells[index].HorizontalAlign = HorizontalAlign.Right;
      }
    }

    protected void wdgDecreaseList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (this.ViewState["ColumnsCountDecrease"] == null)
        return;
      int int32 = Convert.ToInt32(this.ViewState["ColumnsCountDecrease"].ToString());
      for (int index = 2; index < int32; ++index)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          if (index % 2 == 0)
            e.Row.Cells[index].Text = "Mermas al " + Convert.ToDateTime(e.Row.Cells[index].Text.Substring(0, 10)).ToString("dd/MM/yyyy");
          else
            e.Row.Cells[index].Text = "N/C al " + Convert.ToDateTime(e.Row.Cells[index].Text.Substring(0, 10)).ToString("dd/MM/yyyy");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
          e.Row.Cells[index].HorizontalAlign = HorizontalAlign.Right;
      }
    }

    protected void wdgProductionList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (this.ViewState["ColumnsCountProduction"] == null)
        return;
      int int32 = Convert.ToInt32(this.ViewState["ColumnsCountProduction"].ToString());
      for (int index = 2; index < int32; ++index)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          if (index % 2 == 0)
            e.Row.Cells[index].Text = "Blanks al " + Convert.ToDateTime(e.Row.Cells[index].Text.Substring(0, 10)).ToString("dd/MM/yyyy");
          else
            e.Row.Cells[index].Text = "Kits al " + Convert.ToDateTime(e.Row.Cells[index].Text.Substring(0, 10)).ToString("dd/MM/yyyy");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
          e.Row.Cells[index].HorizontalAlign = HorizontalAlign.Right;
      }
    }

    protected void wdgDemandList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (this.ViewState["ColumnsCountDemand"] == null)
        return;
      int int32 = Convert.ToInt32(this.ViewState["ColumnsCountDemand"].ToString());
      for (int index = 2; index < int32; ++index)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          if (index % 2 == 0)
            e.Row.Cells[index].Text = "Blanks al " + Convert.ToDateTime(e.Row.Cells[index].Text.Substring(0, 10)).ToString("dd/MM/yyyy");
          else
            e.Row.Cells[index].Text = "Kits al " + Convert.ToDateTime(e.Row.Cells[index].Text.Substring(0, 10)).ToString("dd/MM/yyyy");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
          e.Row.Cells[index].HorizontalAlign = HorizontalAlign.Right;
      }
    }

    protected void wdgDemandProList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (this.ViewState["ColumnsCountDemandPro"] == null)
        return;
      int int32 = Convert.ToInt32(this.ViewState["ColumnsCountDemandPro"].ToString());
      for (int index = 2; index < int32; ++index)
      {
        if (e.Row.RowType == DataControlRowType.Header)
        {
          if (index % 2 == 0)
            e.Row.Cells[index].Text = "Blanks al " + Convert.ToDateTime(e.Row.Cells[index].Text.Substring(0, 10)).ToString("dd/MM/yyyy");
          else
            e.Row.Cells[index].Text = "Kits al " + Convert.ToDateTime(e.Row.Cells[index].Text.Substring(0, 10)).ToString("dd/MM/yyyy");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
          e.Row.Cells[index].HorizontalAlign = HorizontalAlign.Right;
      }
    }

    protected void wibExport_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable dataTable = (DataTable) this.Session["dtExport"];
        ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
        exportToExcelDataGrid.AgregarHojaLibro(new SSSQueriesBL().GetProjectionReports(0, this.wdpDateIni.Text, this.wdpDateFin.Text), "Inventarios");
        exportToExcelDataGrid.AgregarHojaLibro(new SSSQueriesBL().GetProjectionReports(3, this.wdpDateIni.Text, this.wdpDateFin.Text), "Mermas");
        exportToExcelDataGrid.AgregarHojaLibro(new SSSQueriesBL().GetProjectionReports(1, this.wdpDateIni.Text, this.wdpDateFin.Text), "Produccion");
        exportToExcelDataGrid.AgregarHojaLibro(new SSSQueriesBL().GetProjectionReports(2, this.wdpDateIni.Text, this.wdpDateFin.Text), "Demanda");
        exportToExcelDataGrid.AgregarHojaLibro(new SSSQueriesBL().GetProjectionReportsDemandPro(this.wdpDateIni.Text, this.wdpDateFin.Text), "Demanda Proyectada");
        exportToExcelDataGrid.CerrarLibro();
        byte[] buffer = exportToExcelDataGrid.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=PlacasAsignadas.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    private void SetDatePicker()
    {
      this.wdpDateIni.Value = DateTime.Now.AddDays(-30.0);
      this.wdpDateFin.Value = DateTime.Now;
    }

    private void SearchInventary()
    {
      this.lblMessageInventary.Visible = false;
      DataTable projectionReports = new SSSQueriesBL().GetProjectionReports(0, this.wdpDateIni.Text, this.wdpDateFin.Text);
      if (projectionReports == null || projectionReports.Rows.Count == 0)
        Message.SetMessage(this.lblMessageInventary, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
      else
        this.lblMessageInventary.Visible = false;
      this.wdgInventaryList.DataSource = (object) projectionReports;
      this.wdgInventaryList.DataBind();
      this.ViewState["ColumnsCountInventary"] = (object) projectionReports.Columns.Count;
    }

    private void SearchDecrease()
    {
      this.lblMessageDecrease.Visible = false;
      DataTable projectionReports = new SSSQueriesBL().GetProjectionReports(3, this.wdpDateIni.Text, this.wdpDateFin.Text);
      if (projectionReports == null || projectionReports.Rows.Count == 0)
        Message.SetMessage(this.lblMessageDecrease, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
      else
        this.lblMessageDecrease.Visible = false;
      this.wdgDecreaseList.DataSource = (object) projectionReports;
      this.wdgDecreaseList.DataBind();
      this.ViewState["ColumnsCountDecrease"] = (object) projectionReports.Columns.Count;
    }

    private void SearchProduction()
    {
      this.lblMessageProduction.Visible = false;
      DataTable projectionReports = new SSSQueriesBL().GetProjectionReports(1, this.wdpDateIni.Text, this.wdpDateFin.Text);
      if (projectionReports == null || projectionReports.Rows.Count == 0)
        Message.SetMessage(this.lblMessageProduction, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
      else
        this.lblMessageProduction.Visible = false;
      this.wdgProductionList.DataSource = (object) projectionReports;
      this.wdgProductionList.DataBind();
      this.ViewState["ColumnsCountProduction"] = (object) projectionReports.Columns.Count;
    }

    private void SearchDemand()
    {
      this.lblMessageDemand.Visible = false;
      DataTable projectionReports = new SSSQueriesBL().GetProjectionReports(2, this.wdpDateIni.Text, this.wdpDateFin.Text);
      if (projectionReports == null || projectionReports.Rows.Count == 0)
        Message.SetMessage(this.lblMessageDemand, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
      else
        this.lblMessageDemand.Visible = false;
      this.wdgDemandList.DataSource = (object) projectionReports;
      this.wdgDemandList.DataBind();
      this.ViewState["ColumnsCountDemand"] = (object) projectionReports.Columns.Count;
    }

    private void SearchDemandPro()
    {
      this.lblMessageDemandPro.Visible = false;
      DataTable reportsDemandPro = new SSSQueriesBL().GetProjectionReportsDemandPro(this.wdpDateIni.Text, this.wdpDateFin.Text);
      if (reportsDemandPro == null || reportsDemandPro.Rows.Count == 0)
        Message.SetMessage(this.lblMessageDemandPro, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
      else
        this.lblMessageDemandPro.Visible = false;
      this.wdgDemandProList.DataSource = (object) reportsDemandPro;
      this.wdgDemandProList.DataBind();
      this.ViewState["ColumnsCountDemandPro"] = (object) reportsDemandPro.Columns.Count;
    }

    private DataTable DT_Export(GridView GV)
    {
      DataTable dataTable = new DataTable();
      if (GV.HeaderRow != null)
      {
        for (int index = 0; index < GV.HeaderRow.Cells.Count; ++index)
          dataTable.Columns.Add(GV.HeaderRow.Cells[index].Text);
      }
      foreach (GridViewRow row1 in GV.Rows)
      {
        DataRow row2 = dataTable.NewRow();
        for (int index = 0; index < row1.Cells.Count; ++index)
          row2[index] = (object) row1.Cells[index].Text.Replace("&nbsp;", "");
        dataTable.Rows.Add(row2);
      }
      return dataTable;
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
