// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Reports.Plant.WarehouseProductSupplier
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using CrystalDecisions.Shared;
using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using SIIV.WebApp.Warehouse.Rpt;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Reports.Plant
{
  public class WarehouseProductSupplier : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected RadioButtonList rblTypeReports;
    protected Label lblAlmacen;
    protected CheckBox chkFechas;
    protected Fecha wdpDateIni;
    protected Fecha wdpDateFin;
    protected DropDownList wddProduct;
    protected DropDownList wddSupplier;
    protected Button wibSearch;
    protected Button btnExportPdf;
    protected Button btnExportExcel;
    protected GridView wdgBatchReception;
    protected Pager custPagerBatch;
    protected Label lblRecordCount;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadParameters();
      this.SetDatePicker();
    }

    protected void rblTypeReports_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.wddProduct.Visible = this.rblTypeReports.SelectedValue == "1";
      this.wddSupplier.Visible = !(this.rblTypeReports.SelectedValue == "1");
    }

    protected void chkFechas_CheckedChanged(object sender, EventArgs e)
    {
      bool flag = this.chkFechas.Checked;
      this.wdpDateIni.Enabled = !flag;
      this.wdpDateFin.Enabled = !flag;
      this.HidePopup();
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchPS();

    protected void btnExportPdf_Click(object sender, EventArgs e)
    {
      this.ExportData("Pdf");
      this.HidePopup();
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
      this.ExportData("Excel");
      this.HidePopup();
    }

    protected void custPagerBatch_PageChanged(object sender, CustomPageChangeArgs e)
    {
      int pstrTypeReport;
      int pintProduct;
      int pintSupplier;
      if (this.rblTypeReports.SelectedValue == "1")
      {
        pstrTypeReport = 1;
        pintProduct = int.Parse(this.wddProduct.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        pintSupplier = 0;
      }
      else
      {
        pstrTypeReport = 2;
        pintProduct = 0;
        pintSupplier = int.Parse(this.wddSupplier.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      }
      DateTime? pstrBeginDate = !this.chkFechas.Checked ? new DateTime?(Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture)) : new DateTime?();
      DateTime? pstrEndDate = !this.chkFechas.Checked ? new DateTime?(Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture)) : new DateTime?();
      this.SearchPSList(pstrTypeReport, 38, pintProduct, pintSupplier, pstrBeginDate, pstrEndDate, false);
    }

    protected void LoadParameters()
    {
      DataTable productByWarehouse = new ProductWarehouseQueriesBL().GetProductByWarehouse(38);
      if (productByWarehouse != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) productByWarehouse.Rows)
          this.wddProduct.Items.Add(new ListItem(row[1].ToString(), row[0].ToString()));
      }
      this.wddProduct.Items.Insert(0, new ListItem("--Seleccione--", "-1"));
      this.wddProduct.SelectedValue = "-1";
      DataTable suppliertBy = new SupplierQueriesBL().GetSuppliertBy(0, string.Empty, string.Empty);
      if (suppliertBy != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) suppliertBy.Rows)
          this.wddSupplier.Items.Add(new ListItem(row[1].ToString(), row[0].ToString()));
      }
      this.wddSupplier.Items.Insert(0, new ListItem("--Seleccione--", "-1"));
      this.wddSupplier.SelectedValue = "-1";
      this.rblTypeReports_SelectedIndexChanged((object) null, (EventArgs) null);
    }

    private void SetDatePicker()
    {
      this.wdpDateIni.Value = DateTime.Now.AddDays(-29.0);
      this.wdpDateFin.Value = DateTime.Now;
    }

    protected void SearchPS()
    {
      try
      {
        int pstrTypeReport;
        int pintProduct;
        int pintSupplier;
        if (this.rblTypeReports.SelectedValue == "1")
        {
          pstrTypeReport = 1;
          pintProduct = int.Parse(this.wddProduct.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
          pintSupplier = 0;
        }
        else
        {
          pstrTypeReport = 2;
          pintProduct = 0;
          pintSupplier = int.Parse(this.wddSupplier.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        }
        DateTime? pstrBeginDate = !this.chkFechas.Checked ? new DateTime?(Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture)) : new DateTime?();
        DateTime? pstrEndDate = !this.chkFechas.Checked ? new DateTime?(Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture)) : new DateTime?();
        this.SearchPSList(pstrTypeReport, 38, pintProduct, pintSupplier, pstrBeginDate, pstrEndDate, true);
        this.HidePopup();
      }
      catch (Exception ex)
      {
        this.HidePopup();
        Message.SetMessage(this.lblMessage, enmMessageType.Error, "Error<br>----------<br>" + ex.Message);
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SearchPSList(
      int pstrTypeReport,
      int pstrWarehouse,
      int pintProduct,
      int pintSupplier,
      DateTime? pstrBeginDate,
      DateTime? pstrEndDate,
      bool pboolLoadPager)
    {
      int num1 = pboolLoadPager ? 1 : this.custPagerBatch.CurrentPageNumber;
      int num2 = this.custPagerBatch.CurrentPageSize == 0 ? 10 : this.custPagerBatch.CurrentPageSize;
      int pinttotalRows;
      DataTable warehouseProductSupplier = new WarehouseReportsQueriesBL().GetWarehouseProductSupplier(pstrWarehouse, pstrBeginDate, pstrEndDate, pintSupplier, pintProduct, 0, 0, out pinttotalRows);
      int num3 = pinttotalRows;
      this.wdgBatchReception.DataSource = (object) warehouseProductSupplier;
      this.wdgBatchReception.DataBind();
      this.custPagerBatch.TotalPages = num3 % num2 == 0 ? num3 / num2 : num3 / num2 + 1;
      this.custPagerBatch.TotalRecordCount = pinttotalRows;
      if (pstrTypeReport == 1)
      {
        this.wdgBatchReception.Columns[10].Visible = true;
        this.wdgBatchReception.Columns[9].Visible = false;
      }
      else
      {
        this.wdgBatchReception.Columns[10].Visible = false;
        this.wdgBatchReception.Columns[9].Visible = true;
      }
      if (!pboolLoadPager)
        return;
      this.custPagerBatch.LoadPager();
    }

    private void ExportData(string exporttype)
    {
      try
      {
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string str1;
        string text;
        int num1;
        int pintProductId;
        int pintSupplierId;
        if (this.rblTypeReports.SelectedValue == "1")
        {
          str1 = "PorProducto";
          text = this.wddProduct.SelectedItem.Text;
          num1 = 1;
          pintProductId = int.Parse(this.wddProduct.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
          pintSupplierId = 0;
        }
        else
        {
          str1 = "PorProveedor";
          text = this.wddSupplier.SelectedItem.Text;
          num1 = 2;
          pintProductId = 0;
          pintSupplierId = int.Parse(this.wddSupplier.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        }
        DataTable warehouseProductSupplier = new WarehouseReportsQueriesBL().GetWarehouseProductSupplier(38, !this.chkFechas.Checked ? new DateTime?(Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture)) : new DateTime?(), !this.chkFechas.Checked ? new DateTime?(Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture)) : new DateTime?(), pintSupplierId, pintProductId, 0, 0, out int _);
        warehouseProductSupplier.TableName = "Tabla";
        switch (exporttype)
        {
          case "Pdf":
            if (num1 == 1)
            {
              warehouseProductSupplier.Columns.Remove("v_Product");
              ReportProduct reportProduct = new ReportProduct();
              reportProduct.SetDataSource(warehouseProductSupplier);
              reportProduct.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, str1);
              reportProduct.Close();
              ((Component) reportProduct).Dispose();
              break;
            }
            warehouseProductSupplier.Columns.Remove("v_Supplier");
            ReportSupplier reportSupplier = new ReportSupplier();
            reportSupplier.SetDataSource(warehouseProductSupplier);
            reportSupplier.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, str1);
            reportSupplier.Close();
            ((Component) reportSupplier).Dispose();
            break;
          case "Excel":
            if (num1 == 1)
              warehouseProductSupplier.Columns.Remove("v_Product");
            else
              warehouseProductSupplier.Columns.Remove("v_Supplier");
            string str2 = str1;
            DateTime now = DateTime.Now;
            string[] strArray = new string[8]
            {
              str2,
              now.Day.ToString((IFormatProvider) CultureInfo.CurrentCulture),
              now.Month.ToString((IFormatProvider) CultureInfo.CurrentCulture),
              now.Year.ToString((IFormatProvider) CultureInfo.CurrentCulture),
              null,
              null,
              null,
              null
            };
            int num2 = now.Hour;
            strArray[4] = num2.ToString((IFormatProvider) CultureInfo.CurrentCulture);
            num2 = now.Minute;
            strArray[5] = num2.ToString((IFormatProvider) CultureInfo.CurrentCulture);
            num2 = now.Second;
            strArray[6] = num2.ToString((IFormatProvider) CultureInfo.CurrentCulture);
            strArray[7] = ".xls";
            string pstrFileTarget = string.Concat(strArray);
            ArrayList titulos = new ArrayList();
            string str3 = this.Server.MapPath("../") + pstrFileTarget;
            OtherFormats otherFormats = new OtherFormats(str3);
            for (int index = 0; index < this.wdgBatchReception.Columns.Count; ++index)
            {
              if (this.wdgBatchReception.Columns[index].Visible)
                titulos.Add((object) this.wdgBatchReception.Columns[index].HeaderText);
            }
            otherFormats.ExportClaimBook(str1 + " - " + text, titulos, warehouseProductSupplier);
            new ExportFile().Download(str3, pstrFileTarget);
            if (File.Exists(str3))
              File.Delete(str3);
            break;
        }
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, ex.Message + " " + ex.InnerException?.ToString() + " " + ex.StackTrace);
      }
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void wdgBatchReception_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
    }
  }
}
