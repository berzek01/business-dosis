// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.WarehouseInventory
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using Newtonsoft.Json;
using SIIV.BE;
using SIIV.Warehouse.BL;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class WarehouseInventory : Page
  {
    protected TextBox txtInventoryDate;
    protected CalendarExtender txtInventoryDate_CalendarExtender;
    protected Button btnXMLFull;
    protected Button btnExcel;

    private static DataTable CreateTable()
    {
      DataTable table = new DataTable();
      table.Columns.Add(new DataColumn("i_AutoIncrement", Type.GetType("System.Int32"))
      {
        AutoIncrement = true,
        AutoIncrementSeed = 1L,
        AutoIncrementStep = 1L
      });
      table.Columns.Add(new DataColumn("i_RequirementPlateID", Type.GetType("System.Int32")));
      table.Columns.Add(new DataColumn("v_PlateNumber", Type.GetType("System.String")));
      table.Columns.Add(new DataColumn("i_Status", Type.GetType("System.Int32")));
      DataColumn[] dataColumnArray = new DataColumn[2]
      {
        table.Columns["i_RequirementPlateID"],
        null
      };
      table.PrimaryKey = dataColumnArray;
      return table;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      (this.Master.FindControl("body") as HtmlGenericControl).Attributes.Add("onLoad", "document.getElementById('txtRequirementID').focus();");
      this.Session["dtInventoryDetail"] = (object) WarehouseInventory.CreateTable();
      this.txtInventoryDate.Text = DateTime.Now.ToString("dd/MM/yyyy", (IFormatProvider) CultureInfo.CurrentCulture);
    }

    [WebMethod(EnableSession = true)]
    public static void CancelOperation()
    {
      HttpContext.Current.Session["dtInventoryDetail"] = (object) WarehouseInventory.CreateTable();
    }

    [WebMethod(EnableSession = true)]
    public static string SaveOperation(int pintInventoryID)
    {
      WarehouseInventory.WarehouseInventoryServiceResult inventoryServiceResult = new WarehouseInventory.WarehouseInventoryServiceResult();
      SystemUser systemUser = HttpContext.Current.Session["SystemUser"] as SystemUser;
      int iSystemUserId = systemUser.i_SystemUserId;
      int iLocationId = systemUser.i_LocationId;
      try
      {
        DataTable pdtInventoryDetail = HttpContext.Current.Session["dtInventoryDetail"] as DataTable;
        new WarehouseInventoryManagementBL().WarehouseInventoryInsert(pintInventoryID, pdtInventoryDetail, iSystemUserId, iLocationId);
        inventoryServiceResult.Success = true;
        inventoryServiceResult.ErrorMessage = "";
      }
      catch (Exception ex)
      {
        inventoryServiceResult.Success = false;
        inventoryServiceResult.ErrorMessage = ex.Message;
      }
      return JsonConvert.SerializeObject((object) inventoryServiceResult);
    }

    public void ExportDataTableToXML(DataTable dt)
    {
      if (dt.Rows.Count <= 0)
        return;
      string str = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "Download{0}_{1}.{2}", new object[3]
      {
        (object) "Xml",
        (object) DateTime.Now.ToString("ddMMyyyy-hhmmss"),
        (object) "xml"
      });
      StringWriter writer = new StringWriter();
      dt.TableName = "PhysicalInvetory";
      dt.WriteXml((TextWriter) writer, XmlWriteMode.WriteSchema, false);
      this.Response.ContentType = "text/xml";
      this.Response.AppendHeader("Content-Disposition", "attachment; filename=" + str);
      this.EnableViewState = false;
      this.Response.Write(writer.ToString());
      this.Response.End();
    }

    public void ExportDataTableToExcel(DataTable dt)
    {
      if (dt.Rows.Count <= 0)
        return;
      DateTime now = DateTime.Now;
      string format = "Download{0}_{1}.{2}";
      StringWriter writer1 = new StringWriter();
      HtmlTextWriter writer2 = new HtmlTextWriter((TextWriter) writer1);
      DataGrid dataGrid = new DataGrid();
      dataGrid.DataSource = (object) dt;
      dataGrid.DataBind();
      dataGrid.RenderControl(writer2);
      string str = string.Format((IFormatProvider) CultureInfo.CurrentCulture, format, new object[3]
      {
        (object) "Excel",
        (object) now.ToString("ddMMyyyy-hhmmss"),
        (object) "xls"
      });
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AppendHeader("Content-Disposition", "attachment; filename=" + str);
      this.EnableViewState = false;
      this.Response.Write(writer1.ToString());
      this.Response.End();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
      this.ExportDataTableToExcel(new WarehouseInventoryQueriesBL().GetInventoryPositionByRequirementList(this.Session["dtInventoryDetail"] as DataTable));
    }

    protected void btnXMLFull_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.txtInventoryDate.Text))
        return;
      this.ExportDataTableToXML(new WarehouseInventoryQueriesBL().GetPhysicalInvetoryList(Convert.ToInt32(Convert.ToDateTime(this.txtInventoryDate.Text, (IFormatProvider) CultureInfo.CurrentCulture).ToString("ddMMyy", (IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture)));
    }

    public class WarehouseInventoryServiceResult
    {
      public bool Success { get; set; }

      public string ErrorMessage { get; set; }
    }
  }
}
