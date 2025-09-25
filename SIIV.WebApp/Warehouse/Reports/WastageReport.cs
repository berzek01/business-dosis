// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Reports.WastageReport
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using Saplin.Controls;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

#nullable disable
namespace SIIV.WebApp.Warehouse.Reports
{
  public class WastageReport : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected Fecha wdpDateIni;
    protected Fecha wdpDateFin;
    protected DropDownCheckBoxes cboMotivo;
    protected DropDownCheckBoxes cboProduct;
    protected Button wibSearch;
    protected Button wibExport;
    protected GridView dgList1;
    protected Pager custPagerWR;
    protected GridView dgListDetail;
    protected Label lblMessage;
    protected Button btnExport;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      this.InitializeCombos();
      this.SetDatePicker();
    }

    protected void checkBoxes_SelectedIndexChanged(object sender, EventArgs e)
    {
      int num = 0;
      DropDownCheckBoxes dropDownCheckBoxes = (DropDownCheckBoxes) sender;
      foreach (ListItem listItem in ((ListControl) dropDownCheckBoxes).Items)
      {
        if (listItem.Selected)
          ++num;
      }
      dropDownCheckBoxes.Texts.SelectBoxCaption = num > 0 ? (num == ((ListControl) dropDownCheckBoxes).Items.Count ? "Todos" : "Selección Múltiple") : "Seleccione";
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchWR();

    protected void wibExport_Click(object sender, EventArgs e)
    {
      this.ExportData("XLS");
      this.HidePopup();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
      DataTable dt1 = (DataTable) this.Session["dtResult0"];
      DataTable dt2 = (DataTable) this.Session["dtResultDetail"];
      ClsExportToExcelDataGrid clsExportXLS = new ClsExportToExcelDataGrid();
      try
      {
        this.ExportReporteMerma(clsExportXLS, dt1, "Listado de Mermas");
        this.ExportReporteMerma(clsExportXLS, dt2, "Mermas Tipo Blank");
        clsExportXLS.CerrarLibro();
        byte[] buffer = clsExportXLS.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=ConsultaUniversal.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, ex.Message + " " + ex.InnerException?.ToString() + " " + ex.StackTrace);
      }
    }

    protected void custPagerWR_OnPageChanged(object sender, CustomPageChangeArgs e)
    {
      DateTime dateTime1 = Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture);
      DateTime dateTime2 = Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture);
      string str1 = this.checkBoxesSelected(this.cboProduct, "Products");
      string str2 = this.checkBoxesSelected(this.cboMotivo, "Motives");
      if (!this.Validate(str1, str2))
        return;
      this.SearchWRList(dateTime1, dateTime2, str1, str2, false);
    }

    private void InitializeCombos()
    {
      ((BaseDataBoundControl) this.cboProduct).DataSource = (object) new ProductWarehouseQueriesBL().GetProductWarehouseKardexBy(40, (this.Session["SystemUser"] as SystemUser).i_LocationId, "");
      ((Control) this.cboProduct).DataBind();
      ((BaseDataBoundControl) this.cboMotivo).DataSource = (object) new ProductWarehouseQueriesBL().GetWastageMotive();
      ((Control) this.cboMotivo).DataBind();
    }

    private void SetDatePicker()
    {
      this.wdpDateIni.Value = DateTime.Now.AddMonths(-1);
      this.wdpDateFin.Value = DateTime.Now;
    }

    private void SearchWR()
    {
      this.lblMessage.Visible = false;
      try
      {
        DateTime dateTime1 = Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        DateTime dateTime2 = Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        string str1 = this.checkBoxesSelected(this.cboProduct, "Products");
        string str2 = this.checkBoxesSelected(this.cboMotivo, "Motives");
        if (this.Validate(str1, str2))
          this.SearchWRList(dateTime1, dateTime2, str1, str2, true);
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

    protected string checkBoxesSelected(DropDownCheckBoxes cboCheck, string Listname)
    {
      XElement xelement = new XElement((XName) Listname, (object) new XElement((XName) "Items"));
      int num = 0;
      foreach (ListItem listItem in ((ListControl) cboCheck).Items)
      {
        if (listItem.Selected)
        {
          ++num;
          XElement content = new XElement((XName) "Item", new object[2]
          {
            (object) new XElement((XName) "Id", (object) listItem.Value),
            (object) new XElement((XName) "Value", (object) listItem.Text)
          });
          xelement.Element((XName) "Items").Add((object) content);
        }
      }
      return num > 0 ? "<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>\n" + xelement.ToString() : "";
    }

    private bool Validate(string strProductIds, string strMotives)
    {
      bool flag = false;
      if (string.IsNullOrEmpty(strProductIds))
        Message.SetMessage(this.lblMessage, enmMessageType.Error, "Seleccione Productos/Componentes");
      else if (string.IsNullOrEmpty(strMotives))
        Message.SetMessage(this.lblMessage, enmMessageType.Error, "Seleccione Motivos");
      else
        flag = true;
      return flag;
    }

    private void SearchWRList(
      DateTime d_FecIni,
      DateTime d_FecFin,
      string i_ProductIds,
      string v_Motives,
      bool pboolLoadPager)
    {
      int pintstartRowIndex = pboolLoadPager ? 1 : this.custPagerWR.CurrentPageNumber;
      int pintmaxRows = this.custPagerWR.CurrentPageSize == 0 ? 10 : this.custPagerWR.CurrentPageSize;
      int pinttotalRows;
      DataSet wastageProduct = new ProductWarehouseQueriesBL().GetWastageProduct(d_FecIni, d_FecFin, i_ProductIds, v_Motives, pintstartRowIndex, pintmaxRows, out pinttotalRows);
      if (wastageProduct == null)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
        this.HidePopup();
        this.wibExport.Enabled = false;
      }
      else
      {
        this.lblMessage.Visible = false;
        this.wibExport.Enabled = true;
      }
      int num = pinttotalRows;
      this.dgList1.DataSource = (object) wastageProduct.Tables[0];
      this.dgList1.DataBind();
      this.dgListDetail.DataSource = (object) wastageProduct.Tables[1];
      this.dgListDetail.DataBind();
      this.custPagerWR.TotalPages = num % pintmaxRows == 0 ? num / pintmaxRows : num / pintmaxRows + 1;
      this.custPagerWR.TotalRecordCount = pinttotalRows;
      if (!pboolLoadPager)
        return;
      this.custPagerWR.LoadPager();
    }

    private void ExportData(string exporttype)
    {
      DateTime dateTime1 = Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture);
      DateTime dateTime2 = Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture);
      string strProductIds = this.checkBoxesSelected(this.cboProduct, "Products");
      string strMotives = this.checkBoxesSelected(this.cboMotivo, "Motives");
      if (!this.Validate(strProductIds, strMotives))
        return;
      DataSet wastageProduct = new ProductWarehouseQueriesBL().GetWastageProduct(dateTime1, dateTime2, strProductIds, strMotives, 0, 0, out int _);
      this.Session["dtResult0"] = (object) wastageProduct.Tables[0];
      this.Session["dtResultDetail"] = (object) wastageProduct.Tables[1];
      this.Session["ExportType"] = (object) exporttype;
      this.Export();
    }

    private void Export()
    {
      string script = "Export();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void ExportReporteMerma(
      ClsExportToExcelDataGrid clsExportXLS,
      DataTable dt,
      string strTitle)
    {
      List<ClassColumns> classColumnsList = new List<ClassColumns>();
      List<ClassRow> classRowList = new List<ClassRow>();
      switch (strTitle)
      {
        case "Listado de Mermas":
          if (dt.Columns.Contains("Item"))
            dt.Columns.Remove("Item");
          if (dt.Columns.Contains("i_ProductId"))
            dt.Columns.Remove("i_ProductId");
          if (dt.Columns.Contains("v_CompTip"))
            dt.Columns.Remove("v_CompTip");
          if (dt.Columns.Contains("i_WastageMotiveId"))
            dt.Columns.Remove("i_WastageMotiveId");
          classColumnsList.Add(new ClassColumns("OP", 1, 80));
          classColumnsList.Add(new ClassColumns("Fecha", 1, 150));
          classColumnsList.Add(new ClassColumns("Placa", 1, 100));
          classColumnsList.Add(new ClassColumns("Producto/Componente", 1, 200));
          classColumnsList.Add(new ClassColumns("Value 1", 1, 100));
          classColumnsList.Add(new ClassColumns("Value 2", 1, 100));
          classColumnsList.Add(new ClassColumns("Motivo", 1, 250));
          classColumnsList.Add(new ClassColumns("Nro Factura Compra", 1, 150));
          break;
        case "Mermas Tipo Blank":
          classColumnsList.Add(new ClassColumns("Producto/Componente", 1, 200));
          classColumnsList.Add(new ClassColumns("Cantidad", 1, 100));
          classColumnsList.Add(new ClassColumns("CantidadNC", 1, 100));
          break;
        case "Reporte de Mermas":
          classColumnsList.Add(new ClassColumns("", 1, 150));
          classColumnsList.Add(new ClassColumns("", 1, 75));
          classColumnsList.Add(new ClassColumns("", 1, 75));
          classColumnsList.Add(new ClassColumns("", 1, 75));
          break;
      }
      clsExportXLS.clsParam = classRowList;
      clsExportXLS.clsTitle = classColumnsList;
      clsExportXLS.AgregarHojaLibro(dt, strTitle);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
