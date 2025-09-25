// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Reports.ProductStockReport
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Reports
{
  public class ProductStockReport : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected DropDownList wddLocation;
    protected DropDownList wddWarehouse;
    protected Button wibSearch;
    protected Button btnJavaScriptResponse;
    protected Button btnExport;
    protected Button wibExportDetail;
    protected Button btnExportDetail;
    protected HtmlTableCell celReport;
    protected GridView wdgList;
    protected Button wibExport;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.InitializeData();
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        this.BuildReport();
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

    protected void btnJavaScriptResponse_Click(object sender, EventArgs e)
    {
      try
      {
        int int32_1 = Convert.ToInt32(this.wddWarehouse.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        int int32_2 = Convert.ToInt32(this.wddLocation.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        string[] strArray = this.Request["__EVENTARGUMENT"].Trim().Split(',');
        this.Session["dtExport"] = (object) new ProductWarehouseQueriesBL().StockProductReport(int32_1, int32_2, Convert.ToInt32(strArray[0]));
        this.Session["ExportType"] = (object) strArray[1];
        this.Export();
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

    protected void wibExport_Click(object sender, EventArgs e)
    {
      try
      {
        string str = this.ViewState["vshtmlReport"].ToString();
        string path = this.Server.MapPath(this.Request.ApplicationPath) + "/Warehouse/Reports/ProductStockReport.xls";
        string url = "~/Warehouse/Reports/ProductStockReport.xls";
        FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
        StreamWriter streamWriter = new StreamWriter((Stream) fileStream);
        streamWriter.Write(str);
        streamWriter.Close();
        fileStream.Close();
        this.Response.Redirect(url);
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

    protected void btnExport_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable dataTable = (DataTable) this.Session["dtExport"];
        string str = this.Session["ExportType"].ToString();
        using (ReportDocument reportDocument = new ReportDocument())
        {
          string filename = this.Server.MapPath("../Rpt/ProductStockReportAAP.rpt");
          reportDocument.Load(filename);
          if (dataTable.Rows.Count > 0)
          {
            reportDocument.SetDataSource(dataTable);
            switch (str)
            {
              case "pdf":
                reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "Posición de Placas Detallado");
                break;
              case "xls":
                reportDocument.ExportToHttpResponse(ExportFormatType.Excel, this.Response, true, "Posición de Placas Detallado");
                break;
            }
          }
          else
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontro Información <br>"));
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

    protected void btnExportpdf_Click(object sender, EventArgs e)
    {
      try
      {
        this.Session["dtExport"] = (object) new ProductWarehouseQueriesBL().StockProductReport(Convert.ToInt32(this.wddWarehouse.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.wddLocation.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), 1);
        this.Session["ExportType"] = (object) "pdf";
        this.Export();
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

    protected void wibExportDetail_Click(object sender, EventArgs e)
    {
      try
      {
        this.ExportList();
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

    private void Export2()
    {
      string script = "ExportDetail();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void btnExportDetail_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable objDataTable = (DataTable) this.Session["dtExport"];
        ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
        List<ClassColumns> classColumnsList = new List<ClassColumns>();
        foreach (DataControlField column in (StateManagedCollection) this.wdgList.Columns)
        {
          if (column.Visible && column.GetType().Name == "BoundField")
          {
            BoundField boundField = (BoundField) column;
            classColumnsList.Add(new ClassColumns(boundField.DataField, 1, (int) column.ControlStyle.Width.Value, boundField.HeaderText));
          }
        }
        exportToExcelDataGrid.clsTitle = classColumnsList;
        exportToExcelDataGrid.AgregarHojaLibro(objDataTable, "Posicion de Placas Detallado");
        exportToExcelDataGrid.CerrarLibro();
        byte[] buffer = exportToExcelDataGrid.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=Posicion de Placas Detallado.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
      }
      catch (Exception ex)
      {
      }
    }

    private void InitializeData()
    {
      try
      {
        int num = this.Session["SystemUser"] != null ? (this.Session["SystemUser"] as SystemUser).i_LocationId : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - ProductStockReport.aspx");
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
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void BuildReport()
    {
      try
      {
        int int32_1 = Convert.ToInt32(this.wddLocation.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        int int32_2 = Convert.ToInt32(this.wddWarehouse.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        ShelfQueriesBL shelfQueriesBl = new ShelfQueriesBL();
        DataTable shelfByWarehouse = shelfQueriesBl.GetShelfByWarehouse(int32_2, int32_1);
        DataTable byShelfPosition = shelfQueriesBl.GetByShelfPosition(string.Empty, int32_1, int32_2, 1);
        string[] HeaderCol = new string[27]
        {
          "A",
          "B",
          "C",
          "D",
          "E",
          "F",
          "G",
          "H",
          "I",
          "J",
          "K",
          "L",
          "M",
          "N",
          "Ñ",
          "O",
          "P",
          "Q",
          "R",
          "S",
          "T",
          "U",
          "V",
          "W",
          "X",
          "Y",
          "Z"
        };
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<table border='1' width='700px' valign='top'>");
        stringBuilder.Append("<tr>");
        int num = 1;
        foreach (DataRow row in (InternalDataCollectionBase) shelfByWarehouse.Rows)
        {
          DataRow shelfRow = row;
          int int32_3 = Convert.ToInt32(shelfRow["i_PositionX"], (IFormatProvider) CultureInfo.CurrentCulture);
          int int32_4 = Convert.ToInt32(shelfRow["i_PositionY"], (IFormatProvider) CultureInfo.CurrentCulture);
          stringBuilder.Append("<td>");
          stringBuilder.Append("<table border=0 width='350px' valign='top'>");
          stringBuilder.Append("<tr>");
          stringBuilder.Append("<td>Anaquel: " + shelfRow["v_Desciption"]?.ToString() + "\nCapacidad :" + shelfRow["i_CapacityShelf"]?.ToString() + "</td>");
          stringBuilder.Append("<td>");
          stringBuilder.Append("<input type='submit'; onclick=__doPostBack('" + this.btnJavaScriptResponse.UniqueID + "','" + shelfRow["i_ShelfId"].ToString() + ",xls'); value='Xls' class='button-add' id='Button_" + shelfRow["i_ShelfId"].ToString() + "'/>");
          stringBuilder.Append("</td>");
          stringBuilder.Append("</tr>");
          stringBuilder.Append("<tr>");
          stringBuilder.Append("<th></th>");
          for (int index = 0; index < int32_4; ++index)
          {
            stringBuilder.Append("<th width=30 style='border: 1px solid #B8B8B8; background-color: #F0F0F0; color: #415699;font-size: 8pt; font-family: Trebuchet MS,sans-serif;'>");
            stringBuilder.Append(HeaderCol[index]);
            stringBuilder.Append("</th>");
          }
          stringBuilder.Append("</tr>");
          for (int j = 0; j < int32_3; j++)
          {
            stringBuilder.Append("<tr>");
            stringBuilder.Append("<th width=30 style='border: 1px solid #B8B8B8; background-color: #F0F0F0; color: #415699;font-size: 8pt; font-family: Trebuchet MS,sans-serif;'>");
            stringBuilder.Append(j + 1);
            stringBuilder.Append("</th>");
            for (int i = 0; i < int32_4; i++)
            {
              bool flag = false;
              foreach (var data in byShelfPosition.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (pos => pos.Field<int>("i_ShelfId") == Convert.ToInt32(shelfRow["i_ShelfId"], (IFormatProvider) CultureInfo.CurrentCulture) && pos.Field<int>("i_PositionX") == j + 1 && pos.Field<int>("i_PositionY") == (int) Convert.ToChar(HeaderCol[i], (IFormatProvider) CultureInfo.CurrentCulture))).Select(pos => new
              {
                CurrentQuantity = pos.Field<int>("i_CurrentQuantity"),
                CapacityShelf = pos.Field<int>("i_CapacityShelf"),
                IsAssigned = pos.Field<int>("i_isAssigned")
              }))
              {
                flag = true;
                if (data.CurrentQuantity > 0)
                {
                  if (data.CurrentQuantity >= data.CapacityShelf)
                    stringBuilder.Append("<td width=30 bgcolor='red' align='center' style='color:White'>" + data.CurrentQuantity.ToString() + "</td>");
                  else
                    stringBuilder.Append("<td width=30 bgcolor='darkblue' style='color:White' align='center'>" + data.CurrentQuantity.ToString() + "</td>");
                }
                else
                  stringBuilder.Append("<td width=30 bgcolor='darkblue' style='color:White' align='center'>" + data.CurrentQuantity.ToString() + "</td>");
              }
              if (!flag)
                stringBuilder.Append("<td width=30 bgcolor='darkblue' style='color:White' align='center'>0</td>");
            }
            stringBuilder.Append("</tr>");
          }
          stringBuilder.Append("</table>");
          stringBuilder.Append("</td>");
          if (num % 2 == 0)
            stringBuilder.Append("</tr><tr>");
          ++num;
        }
        stringBuilder.Append("</tr>");
        stringBuilder.Append("</table>");
        this.ViewState["vshtmlReport"] = (object) stringBuilder.ToString();
        this.celReport.InnerHtml = stringBuilder.ToString();
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
        string script = "ExportDetail();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ExportList()
    {
      try
      {
        int int32_1 = Convert.ToInt32(this.wddWarehouse.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        int int32_2 = Convert.ToInt32(this.wddLocation.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        ShelfQueriesBL shelfQueriesBl = new ShelfQueriesBL();
        DataTable dataTable = new DataTable();
        DataTable shelfPositionReport = shelfQueriesBl.GetShelfPositionReport(int32_1, int32_2);
        this.wdgList.DataSource = (object) shelfPositionReport;
        this.wdgList.DataBind();
        this.Session["dtExport"] = (object) shelfPositionReport;
        this.Export2();
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

    public string RenderControlObj(Control ctrl)
    {
      StringBuilder sb = new StringBuilder();
      HtmlTextWriter writer = new HtmlTextWriter((TextWriter) new StringWriter(sb));
      ctrl.RenderControl(writer);
      return sb.ToString();
    }
  }
}
