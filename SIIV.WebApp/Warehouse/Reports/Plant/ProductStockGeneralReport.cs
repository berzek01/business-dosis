// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Reports.Plant.ProductStockGeneralReport
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Reports.Plant
{
  public class ProductStockGeneralReport : Page
  {
    protected UpdatePanel UpdatePanel2;
    protected DropDownList wddWarehouse;
    protected CheckBox chkFechas;
    protected Fecha wdpDateIni;
    protected Button wibSearch;
    protected Button wibExcel;
    protected GridView wdgBatchReception;
    protected GridView wdgListNew;
    protected Pager custPagerBatch;
    protected Label lblMessage;
    protected Button btnExport;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.InitializeData();
    }

    protected void wdgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowIndex < 0 || this.ViewState["ColumnsCountStock"] == null)
          return;
        int int32 = Convert.ToInt32(this.ViewState["ColumnsCountStock"]);
        for (int index = 0; index < int32 - 1; ++index)
        {
          if (e.Row.Cells[4].Text == "2")
            e.Row.Cells[index].CssClass = "Orange";
          if (e.Row.Cells[4].Text == "3")
            e.Row.Cells[index].CssClass = "Red";
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void chkFechas_CheckedChanged(object sender, EventArgs e)
    {
      this.wdpDateIni.Enabled = !this.chkFechas.Checked;
      this.HidePopup();
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchPSGR();

    protected void custPagerBatch_PageChanged(object sender, CustomPageChangeArgs e)
    {
      this.SearchPSGRList(Convert.ToInt32(this.wddWarehouse.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), !this.chkFechas.Checked ? new DateTime?(Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture)) : new DateTime?(), false);
    }

    protected void wibExcel_Click(object sender, EventArgs e)
    {
      string script = "Export();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script, true);
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
      try
      {
        this.Export(this.ExportList());
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    private void InitializeData()
    {
      SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
      this.LoadWarehouse();
      this.wdpDateIni.Value = DateTime.Today;
    }

    private void LoadWarehouse()
    {
      DataTable dataTable = new DataTable();
      int iLocationId = (this.Session["SystemUser"] as SystemUser).i_LocationId;
      DataTable warehouseBy = new WarehouseQueriesBL().GetWarehouseBy(0, string.Empty, iLocationId, -1);
      string RowFilter = "isnull(b_IsReserved,0) = 0";
      DataView dataView = new DataView(warehouseBy, RowFilter, "i_WarehouseId", DataViewRowState.CurrentRows);
      DataRow row = warehouseBy.NewRow();
      row["i_WarehouseId"] = (object) 0;
      row["v_Description"] = (object) "- Seleccione -";
      row["v_Address"] = (object) string.Empty;
      row["i_Status"] = (object) 1;
      row["i_LocationId"] = (object) 0;
      row["v_Description2"] = (object) string.Empty;
      row["i_UsePositionLogic"] = (object) 0;
      row["i_WarehouseTypeId"] = (object) 0;
      warehouseBy.Rows.InsertAt(row, 0);
      this.wddWarehouse.DataSource = (object) dataView.ToTable();
      this.wddWarehouse.DataTextField = "v_Description";
      this.wddWarehouse.DataValueField = "i_WarehouseId";
      this.wddWarehouse.DataBind();
      this.wddWarehouse.SelectedValue = "0";
    }

    protected void SearchPSGR()
    {
      this.lblMessage.Visible = false;
      if (this.wddWarehouse.SelectedValue == null || this.wddWarehouse.SelectedValue == "0")
      {
        this.HidePopup();
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Debe seleccionar un Almacén");
      }
      else
      {
        try
        {
          this.SearchPSGRList(Convert.ToInt32(this.wddWarehouse.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), !this.chkFechas.Checked ? new DateTime?(Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture)) : new DateTime?(), true);
          this.HidePopup();
        }
        catch (Exception ex)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Error, "Error<br>----------<br>" + ex.Message);
          this.HidePopup();
        }
        finally
        {
          this.HidePopup();
        }
      }
    }

    private void SearchPSGRList(int intWarehouseId, DateTime? pstrBeginDate, bool pboolLoadPager)
    {
      try
      {
        int startRowIndex = pboolLoadPager ? 1 : this.custPagerBatch.CurrentPageNumber;
        int maxRows = this.custPagerBatch.CurrentPageSize == 0 ? 10 : this.custPagerBatch.CurrentPageSize;
        int pinttotalRows;
        DataTable stockProductGeneral = new WarehouseReportsQueriesBL().GetWarehouseStockProductGeneral(intWarehouseId, pstrBeginDate, startRowIndex, maxRows, out pinttotalRows);
        if (stockProductGeneral == null || stockProductGeneral.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
          this.HidePopup();
          this.wibExcel.Enabled = false;
        }
        else
        {
          this.ViewState["dtResult"] = (object) stockProductGeneral;
          this.ViewState["ColumnsCountStock"] = (object) stockProductGeneral.Columns.Count;
          this.lblMessage.Visible = false;
          this.wibExcel.Enabled = true;
        }
        int num = pinttotalRows;
        this.wdgBatchReception.DataSource = (object) stockProductGeneral;
        this.wdgBatchReception.DataBind();
        this.custPagerBatch.TotalPages = num % maxRows == 0 ? num / maxRows : num / maxRows + 1;
        this.custPagerBatch.TotalRecordCount = pinttotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerBatch.LoadPager();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, "Error... Consulte con el Administrador. " + ex.Message);
      }
    }

    private DataTable ExportList()
    {
      DataTable dataTable = new DataTable();
      return (DataTable) this.ViewState["dtResult"];
    }

    private void Export(DataTable dt_Result)
    {
      DataTable objDataTable = dt_Result;
      ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
      List<ClassColumns> classColumnsList = new List<ClassColumns>();
      foreach (DataControlField column in (StateManagedCollection) this.wdgBatchReception.Columns)
      {
        if (column.HeaderText != "Oculto" && column.GetType().Name == "BoundField")
        {
          BoundField boundField = (BoundField) column;
          classColumnsList.Add(new ClassColumns(boundField.DataField, 1, (int) column.ControlStyle.Width.Value, boundField.HeaderText));
        }
      }
      exportToExcelDataGrid.clsTitle = classColumnsList;
      exportToExcelDataGrid.AgregarHojaLibro(objDataTable, "StockGeneral - al " + (this.chkFechas.Checked ? DateTime.Today.ToString("dd-MM-yyyy", (IFormatProvider) CultureInfo.CurrentCulture) : Convert.ToDateTime(this.wdpDateIni.Text).ToString("dd-MM-yyyy")));
      exportToExcelDataGrid.CerrarLibro();
      byte[] buffer = exportToExcelDataGrid.DownloadByte();
      this.Response.Clear();
      this.Response.AddHeader("content-disposition", "attachment; filename=StockGeneralPlanta.xls");
      this.Response.BinaryWrite(buffer);
      this.Response.End();
    }

    private void ExportData(string exporttype)
    {
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
