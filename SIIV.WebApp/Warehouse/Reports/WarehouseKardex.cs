// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Reports.WarehouseKardex
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.BE.CustomCode;
using SIIV.Common.Resource;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

#nullable disable
namespace SIIV.WebApp.Warehouse.Reports
{
  public class WarehouseKardex : Page
  {
    protected UpdatePanel UpdatePanel2;
    protected DropDownList wddWarehouse;
    protected CheckBox chkYear;
    protected DropDownList wddYear;
    protected DropDownList wddMonth;
    protected CheckBox chkDateBegin;
    protected Fecha wdpDateBegin;
    protected Fecha wdpDateEnd;
    protected DropDownList wddMotiveMovement;
    protected Button btnSearchProducts;
    protected Button btnExportExcel;
    protected Button btnExportPdf;
    protected Button btnExport;
    protected Label lblMessage;
    protected Button btnJavaScriptResponse;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.LoadWarehouse();
        this.LoadMovementType();
        this.LoadMotiveMovement(0);
        this.LoadMontns();
        this.LoadYears();
        this.SetDatePicker();
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

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private void Export()
    {
      string script = "Export();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script, true);
    }

    private void LoadYears()
    {
      try
      {
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("i_year", Type.GetType("System.Int32"));
        dataTable.Columns.Add("v_Description", Type.GetType("System.String"));
        DateTime dateTime = DateTime.Now;
        int year1 = dateTime.Year;
        int num1 = -20;
        dateTime = DateTime.Now;
        dateTime = dateTime.AddYears(num1);
        int year2 = dateTime.Year;
        int num2 = year2;
        while (year2 <= year1)
        {
          DataRow row = dataTable.NewRow();
          row["i_year"] = (object) year1;
          row["v_Description"] = (object) year1;
          --year1;
          dataTable.Rows.Add(row);
          ++num2;
        }
        DataRow row1 = dataTable.NewRow();
        row1["i_year"] = (object) 0;
        row1["v_Description"] = (object) "- Todos -";
        dataTable.Rows.InsertAt(row1, 0);
        this.wddYear.DataSource = (object) dataTable;
        this.wddYear.DataTextField = "v_Description";
        this.wddYear.DataValueField = "i_year";
        this.wddYear.SelectedValue = "0";
        this.wddYear.DataBind();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadWarehouse()
    {
      try
      {
        int pintLocationId = this.Session["SystemUser"] != null ? (this.Session["SystemUser"] as SystemUser).i_LocationId : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseKardex.aspx");
        DataTable dataTable = new DataTable();
        DataTable warehouseBy = new WarehouseQueriesBL().GetWarehouseBy(0, string.Empty, pintLocationId, 4);
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
        this.wddWarehouse.DataSource = (object) warehouseBy;
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

    private void LoadMovementType()
    {
      try
      {
        if (this.Session["SystemUser"] == null)
          throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseKardex.aspx");
        Convert.ToInt32((object) (this.Session["SystemUser"] as SystemUser).i_CompanyId, (IFormatProvider) CultureInfo.CurrentCulture);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadMotiveMovement(int pintWarehouseId)
    {
      try
      {
        DataTable motiveMovementFactory = new MotiveMovementQueriesBL().GetMotiveMovementFactory(pintWarehouseId, 0);
        this.wddMotiveMovement.DataSource = (object) motiveMovementFactory;
        DataRow row = motiveMovementFactory.NewRow();
        row["i_MotiveMovementId"] = (object) 0;
        row["v_Description"] = (object) "- Todos -";
        motiveMovementFactory.Rows.InsertAt(row, 0);
        this.wddMotiveMovement.DataTextField = "v_Description";
        this.wddMotiveMovement.DataValueField = "i_MotiveMovementId";
        this.wddMotiveMovement.SelectedValue = "0";
        this.wddMotiveMovement.DataBind();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadMontns()
    {
      try
      {
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) ("" + SystemParameterGroups.MonthsofYear.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
          (object) "",
          (object) "1",
          (object) "1"
        });
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["i_GroupId"].ToString() == SystemParameterGroups.MonthsofYear.ToString((IFormatProvider) CultureInfo.CurrentCulture))
              this.wddMonth.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          }
        }
        this.wddMonth.Items.Insert(0, new ListItem("- Todos - ", "-1"));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SetDatePicker()
    {
      try
      {
        this.wdpDateBegin.Value = DateTime.Now.AddMonths(-1);
        this.wdpDateEnd.Value = DateTime.Now;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void CreatePopUp(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void btnJavaScriptResponse_Click(object sender, EventArgs e)
    {
    }

    protected void chkDateBegin_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        this.wdpDateBegin.Enabled = this.chkDateBegin.Checked;
        this.wdpDateEnd.Enabled = this.chkDateBegin.Checked;
        if (!this.chkDateBegin.Checked)
          return;
        this.chkYear.Checked = !this.chkDateBegin.Checked;
        this.wddYear.Enabled = this.chkYear.Checked;
        this.wddMonth.Enabled = this.chkYear.Checked;
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

    protected void chkYear_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        this.wddYear.Enabled = this.chkYear.Checked;
        this.wddMonth.Enabled = this.chkYear.Checked;
        if (!this.chkYear.Checked)
          return;
        this.chkDateBegin.Checked = !this.chkYear.Checked;
        this.wdpDateBegin.Enabled = this.chkDateBegin.Checked;
        this.wdpDateEnd.Enabled = this.chkDateBegin.Checked;
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

    protected void btnSearchKardex_Click(object sender, EventArgs e)
    {
    }

    protected void wddWarehouse_SelectionChanged(object sender, EventArgs e)
    {
      try
      {
        this.Session["i_WarehouseId"] = (object) Convert.ToInt32(this.wddWarehouse.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        this.LoadMotiveMovement(Convert.ToInt32(this.wddWarehouse.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
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

    protected void btnExportPdf_Click(object sender, EventArgs e)
    {
      try
      {
        this.ExportData("Pdf");
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void ExportData(string exporttype)
    {
      try
      {
        if (this.wddWarehouse.SelectedValue == null || this.wddWarehouse.SelectedValue == "0")
          throw new HandledException(1, "¡Debe seleccionar el Almacén para poder realizar la consulta!");
        ProductKardex pobjProductKardex = new ProductKardex();
        pobjProductKardex.i_WarehouseId = Convert.ToInt32(this.wddWarehouse.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        pobjProductKardex.i_year = !this.chkYear.Checked || !(this.wddYear.SelectedValue != "-1") || !(this.wddYear.SelectedValue != "") || !(this.wddYear.SelectedValue != "0") ? new int?() : new int?(Convert.ToInt32(this.wddYear.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
        pobjProductKardex.i_montn = !this.chkYear.Checked || !(this.wddMonth.SelectedValue != "-1") || !(this.wddMonth.SelectedValue != "") || !(this.wddMonth.SelectedValue != "0") ? new int?() : new int?(Convert.ToInt32(this.wddMonth.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
        pobjProductKardex.d_fechabegin = this.chkDateBegin.Checked ? new DateTime?(Convert.ToDateTime((object) this.wdpDateBegin.Value, (IFormatProvider) CultureInfo.CurrentCulture)) : new DateTime?();
        pobjProductKardex.d_fechaend = this.chkDateBegin.Checked ? new DateTime?(Convert.ToDateTime((object) this.wdpDateEnd.Value, (IFormatProvider) CultureInfo.CurrentCulture)) : new DateTime?();
        pobjProductKardex.i_MotiveMovementId = this.wddMotiveMovement.SelectedValue == null || !(this.wddMotiveMovement.SelectedValue != "0") ? new int?() : new int?(Convert.ToInt32(this.wddMotiveMovement.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
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
        DataTable productKardexPep = new ProductWarehouseKardexBL().GetProductKardexPEP(pobjProductKardex);
        productKardexPep.TableName = "TablaPEP";
        DataTable productKardex = new ProductWarehouseKardexBL().GetProductKardex(pobjProductKardex);
        productKardex.TableName = "Tabla";
        DataTable productKardex1 = new ProductWarehouseKardexBL().GetProductKardex1(pobjProductKardex);
        productKardex1.TableName = "TablaFinishProduct";
        this.Session["dtExport"] = (object) productKardex;
        this.Session["dtExportPEP"] = (object) productKardexPep;
        this.Session["ExportType"] = (object) exporttype;
        this.Session["dtProductFinish"] = (object) productKardex1;
        this.Export();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void HttpPost(string URI, string strXml)
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(URI);
      byte[] bytes = Encoding.ASCII.GetBytes(strXml);
      httpWebRequest.Method = "POST";
      httpWebRequest.ContentType = "text/xml;charset=utf-8";
      httpWebRequest.ContentLength = (long) bytes.Length;
      Stream requestStream = httpWebRequest.GetRequestStream();
      requestStream.Write(bytes, 0, bytes.Length);
      requestStream.Close();
      HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse();
      StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.Default);
      streamReader.ReadToEnd();
      streamReader.Close();
      response.Close();
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
      try
      {
        this.ExportData("Excel");
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
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
        DataTable dataTable1 = (DataTable) this.Session["dtExport"];
        DataTable dt1 = (DataTable) this.Session["dtExportPEP"];
        DataTable dt2 = (DataTable) this.Session["dtProductFinish"];
        List<SIIV.BE.SystemParameter> LstCredential = new SystemParameterManagementBL().Get((object) new ArrayList()
        {
          (object) "519",
          (object) "",
          (object) "1",
          (object) "1"
        });
        ClsExportToExcelDataGrid clsExportXLS = new ClsExportToExcelDataGrid();
        DataTable dataTable2 = this.AgrupaWareHouse(dataTable1, "v_WareHouseSunat");
        string strPeriodo = !this.chkYear.Checked ? (!this.chkDateBegin.Checked ? "al " + DateTime.Today.ToString("dd/MM/yyyy", (IFormatProvider) CultureInfo.CurrentCulture) : this.wdpDateBegin.Text + " al " + this.wdpDateEnd.Text) : this.wddMonth.SelectedItem.Text + " - " + this.wddYear.SelectedItem.Text;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
          this.ExportKardex(clsExportXLS, dataTable1, "v_WareHouseSunat = '" + row["v_Description"].ToString() + "'", LstCredential, strPeriodo, row["v_Description"].ToString());
        this.ExportKardex(clsExportXLS, dataTable1, "orden <> 0", LstCredential, strPeriodo, "RESUMEN SD & MP");
        this.ExportKardex(clsExportXLS, dt1, "", LstCredential, strPeriodo, "PRODUCTOS EN PROCESO");
        this.ExportKardex(clsExportXLS, dt2, "", LstCredential, strPeriodo, "F 12.1 RIP Detalle");
        clsExportXLS.CerrarLibro();
        byte[] buffer = clsExportXLS.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=ReporteKardexValorizado.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
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

    private DataTable AgrupaWareHouse(DataTable pdtEnum, string pstrGroupBy)
    {
      try
      {
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("v_Description");
        foreach (IGrouping<string, DataRow> grouping in pdtEnum.AsEnumerable().GroupBy<DataRow, string>((System.Func<DataRow, string>) (item => item[pstrGroupBy].ToString())).Select<IGrouping<string, DataRow>, IGrouping<string, DataRow>>((System.Func<IGrouping<string, DataRow>, IGrouping<string, DataRow>>) (g => g)))
        {
          DataRow row = dataTable.NewRow();
          row["v_Description"] = (object) grouping.Key;
          dataTable.Rows.Add(row);
        }
        return dataTable;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void ExportKardex(
      ClsExportToExcelDataGrid clsExportXLS,
      DataTable dt,
      string strFiltro,
      List<SIIV.BE.SystemParameter> LstCredential,
      string strPeriodo,
      string strWareHouse)
    {
      try
      {
        DataRow[] dataRowArray = dt.Select(strFiltro);
        DataTable dt1 = dt.Clone();
        foreach (DataRow row1 in dataRowArray)
        {
          dt1.ImportRow(row1);
          if (row1["v_MotiveMovement"].ToString() == "TOTALES" && strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() == "RESUMEN")
          {
            DataRow row2 = dt1.NewRow();
            row2["Serie"] = (object) "";
            row2["i_WarehouseIdSunat"] = (object) 0;
            row2["v_WarehouseSunat"] = (object) 0;
            row2["_colSubTotal"] = (object) 0;
            dt1.Rows.Add(row2);
          }
        }
        if (dt1.Columns.Contains("orden"))
          dt1.Columns.Remove("orden");
        if (dt1.Columns.Contains("i_WarehouseIdSunat"))
          dt1.Columns.Remove("i_WarehouseIdSunat");
        if (dt1.Columns.Contains("v_WarehouseSunat"))
          dt1.Columns.Remove("v_WarehouseSunat");
        if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() == "RESUMEN")
        {
          if (dt1.Columns.Contains("v_DocumentTypeName"))
            dt1.Columns.Remove("v_DocumentTypeName");
          if (dt1.Columns.Contains("Serie"))
            dt1.Columns.Remove("Serie");
          if (dt1.Columns.Contains("v_DocumentNumber"))
            dt1.Columns.Remove("v_DocumentNumber");
        }
        if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() == "F 12.1 RIP Detalle")
        {
          if (dt1.Columns.Contains("i_BatchId"))
            dt1.Columns.Remove("i_BatchId");
          if (dt1.Columns.Contains("v_MotiveMovement"))
            dt1.Columns.Remove("v_MotiveMovement");
        }
        List<ClassRow> classRowList = new List<ClassRow>();
        if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() == "F 12.1 RIP Detalle")
        {
          classRowList.Add(new ClassRow("", 1, LstCredential[12].v_Description, 5));
          classRowList.Add(new ClassRow("", 2, LstCredential[13].v_Description, 5));
          classRowList.Add(new ClassRow("", 4, "", 10));
          classRowList.Add(new ClassRow("", 4, "", 10));
        }
        classRowList.Add(new ClassRow(LstCredential[0].v_Description, 4, strPeriodo, 10));
        classRowList.Add(new ClassRow(LstCredential[1].v_Description, 4, LstCredential[1].v_Value, 10));
        classRowList.Add(new ClassRow(LstCredential[2].v_Description, 4, LstCredential[2].v_Value, 10));
        if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() != "F 12.1 RIP Detalle")
          classRowList.Add(new ClassRow(LstCredential[3].v_Description, 4, LstCredential[3].v_Value, 10));
        if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() == "F 12.1 RIP Detalle")
          classRowList.Add(new ClassRow(LstCredential[3].v_Description, 4, LstCredential[14].v_Value, 10));
        if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() != "F 12.1 RIP Detalle")
          classRowList.Add(new ClassRow(LstCredential[4].v_Description, 4, LstCredential[4].v_Value, 10));
        if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() == "F 12.1 RIP Detalle")
          classRowList.Add(new ClassRow(LstCredential[4].v_Description, 4, "060", 10));
        if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() != "F 12.1 RIP Detalle")
          classRowList.Add(new ClassRow(LstCredential[5].v_Description, 4, strWareHouse, 10));
        if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() == "F 12.1 RIP Detalle")
          classRowList.Add(new ClassRow(LstCredential[10].v_Description, 4, LstCredential[10].v_Value, 10));
        if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() != "F 12.1 RIP Detalle")
          classRowList.Add(new ClassRow(LstCredential[6].v_Description, 4, LstCredential[6].v_Value, 10));
        if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() == "F 12.1 RIP Detalle")
          classRowList.Add(new ClassRow(LstCredential[6].v_Description, 4, LstCredential[15].v_Value, 10));
        if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() == "F 12.1 RIP Detalle")
          classRowList.Add(new ClassRow(LstCredential[11].v_Description, 4, LstCredential[11].v_Value, 10));
        if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() != "F 12.1 RIP Detalle")
        {
          classRowList.Add(new ClassRow(LstCredential[7].v_Description, 4, LstCredential[7].v_Value, 10));
          classRowList.Add(new ClassRow(LstCredential[8].v_Description, 4, LstCredential[8].v_Value, 10));
        }
        List<ClassColumns> classColumnsList1 = new List<ClassColumns>();
        if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() != "F 12.1 RIP Detalle")
        {
          if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() != "RESUMEN")
            classColumnsList1.Add(new ClassColumns("Comprobante de Pago", 4));
          else
            classColumnsList1.Add(new ClassColumns("", 1));
        }
        if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() == "F 12.1 RIP Detalle")
          classColumnsList1.Add(new ClassColumns("DOCUMENTO DE TRASLADO, COMPROBANTE DE PAGO, DOCUMENTO INTERNO O SIMILAR", 4));
        if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() != "F 12.1 RIP Detalle")
        {
          classColumnsList1.Add(new ClassColumns("", 1));
          classColumnsList1.Add(new ClassColumns("Entradas", 3));
          classColumnsList1.Add(new ClassColumns("Salidas", 3));
          classColumnsList1.Add(new ClassColumns("Saldo Final", 3));
        }
        if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() == "F 12.1 RIP Detalle")
        {
          classColumnsList1.Add(new ClassColumns("TIPO DE OPERACIÓN", 1));
          classColumnsList1.Add(new ClassColumns("MOVIMIENTOS", 3));
        }
        List<ClassColumns> classColumnsList2 = new List<ClassColumns>();
        classColumnsList2.Add(new ClassColumns("Fecha", 1, 150));
        if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() != "F 12.1 RIP Detalle")
        {
          if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() != "RESUMEN")
          {
            classColumnsList2.Add(new ClassColumns("Tipo", 1, 100));
            classColumnsList2.Add(new ClassColumns("Serie", 1, 80));
            classColumnsList2.Add(new ClassColumns("Número", 1, 80));
            classColumnsList2.Add(new ClassColumns("Operación", 1, 150));
          }
          else
            classColumnsList2.Add(new ClassColumns("", 1, 200));
          classColumnsList2.Add(new ClassColumns("Cantidad", 1, 100));
          classColumnsList2.Add(new ClassColumns("Costo Unitario", 1, 100));
          classColumnsList2.Add(new ClassColumns("Costo Total", 1, 100));
          classColumnsList2.Add(new ClassColumns("Cantidad", 1, 100));
          classColumnsList2.Add(new ClassColumns("Costo Unitario", 1, 100));
          classColumnsList2.Add(new ClassColumns("Costo Total", 1, 100));
          classColumnsList2.Add(new ClassColumns("Cantidad", 1, 100));
          classColumnsList2.Add(new ClassColumns("Costo Unitario", 1, 100));
          classColumnsList2.Add(new ClassColumns("Costo Total", 1, 100));
        }
        if (strWareHouse.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim() == "F 12.1 RIP Detalle")
        {
          classColumnsList2.Add(new ClassColumns("TIPO (TABLA 10)", 1, 100));
          classColumnsList2.Add(new ClassColumns("SERIE", 1, 100));
          classColumnsList2.Add(new ClassColumns("NÚMERO", 1, 100));
          classColumnsList2.Add(new ClassColumns("(TABLA 12)", 1, 100));
          classColumnsList2.Add(new ClassColumns("ENTRADAS", 1, 100));
          classColumnsList2.Add(new ClassColumns("SALIDAS", 1, 100));
          classColumnsList2.Add(new ClassColumns("SALDO  FINAL", 1, 100));
        }
        clsExportXLS.clsParam = classRowList;
        clsExportXLS.clsTitle = classColumnsList2;
        clsExportXLS.clsTitleGroup = classColumnsList1;
        clsExportXLS.AgregarHojaLibroKardex(dt1, strWareHouse, strPieGrupo: LstCredential[9].v_Value);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void btnSearchProducts_Click(object sender, EventArgs e)
    {
      if (Convert.ToDateTime(this.wdpDateBegin.Value).CompareTo(Convert.ToDateTime(this.wdpDateEnd.Value)) > 0 && this.chkDateBegin.Checked)
        Message.SetMessage(this.lblMessage, new HandledException(1, "<br>&nbsp;&nbsp; <br> •&nbsp;La fecha de Inicio es mayor a la Fecha Final."));
      else if (this.wddWarehouse.SelectedIndex != 0)
        this.CreatePopUp("Lista Productos", "../Searchs/KardexProductSearch.aspx", "800px", "800px");
      else
        Message.SetMessage(this.lblMessage, new HandledException(1, "<br>&nbsp;&nbsp; <br> •&nbsp;Seleccione un Almacén."));
    }
  }
}
