// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Reports.ProductStockGeneralReport
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Reports
{
  public class ProductStockGeneralReport : Page
  {
    protected UpdatePanel UpdatePanel2;
    protected DropDownList wddLocation;
    protected DropDownList wddWarehouse;
    protected Button wibPdf;
    protected Button wibExcel;
    protected Label lblMessage;
    protected Button btnExport;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      this.InitializeData();
    }

    protected void wibExcel_Click(object sender, EventArgs e)
    {
      try
      {
        this.ExportData("Excel");
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void wibPdf_Click(object sender, EventArgs e)
    {
      try
      {
        this.ExportData("Pdf");
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable dataTable = (DataTable) this.Session["dtExport"];
        string str = this.Session["ExportType"].ToString();
        using (ReportDocument reportDocument = new ReportDocument())
        {
          string filename = this.Server.MapPath("../Rpt/ProductStockGeneralReportAAP.rpt");
          reportDocument.Load(filename);
          if (dataTable.Rows.Count <= 0)
            return;
          reportDocument.SetDataSource(dataTable);
          switch (str)
          {
            case "Pdf":
              reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "Total de Productos");
              break;
            case "Excel":
              reportDocument.ExportToHttpResponse(ExportFormatType.Excel, this.Response, true, "Total de Productos");
              break;
          }
        }
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    private void InitializeData()
    {
      try
      {
        int num = this.Session["SystemUser"] != null ? (this.Session["SystemUser"] as SystemUser).i_LocationId : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - ProductStockGeneralReport.aspx");
        this.wddLocation.DataSource = (object) new LocationQueriesBL().GetLocationBy("", string.Empty, string.Empty);
        this.wddLocation.DataTextField = "v_Description";
        this.wddLocation.DataValueField = "i_LocationId";
        this.wddLocation.DataBind();
        this.wddLocation.SelectedValue = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.wddLocation.Enabled = false;
        this.LoadWarehouse();
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    private void LoadWarehouse()
    {
      try
      {
        DataTable dataTable = new DataTable();
        int pintLocationId = this.Session["SystemUser"] != null ? (this.Session["SystemUser"] as SystemUser).i_LocationId : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - ProductStockGeneralReport.aspx");
        DataTable warehouseBy = new WarehouseQueriesBL().GetWarehouseBy(0, string.Empty, pintLocationId, -1);
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
        this.wddWarehouse.SelectedValue = "0";
        this.wddWarehouse.DataBind();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ExportData(string exporttype)
    {
      try
      {
        if (this.wddWarehouse.SelectedIndex == 0)
          throw new HandledException(1, "Seleccione un Almacén");
        this.Session["dtExport"] = (object) new WarehouseReportsQueriesBL().GetWarehouseStockProductGeneral(Convert.ToInt32(this.wddWarehouse.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.wddLocation.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
        this.Session["ExportType"] = (object) exporttype;
        this.Export();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void Export()
    {
      try
      {
        string script = "ExportExcelAll();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
