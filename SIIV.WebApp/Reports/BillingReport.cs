// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Reports.BillingReport
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.Common.Resource.PagingClass;
using SIIV.Common.Resource.Utilities;
using SIIV.Reports.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Reports
{
  public class BillingReport : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected Fecha wdpStartDate;
    protected Fecha wdpEndDate;
    protected RadioButton rbBilling;
    protected RadioButton RadioButton2;
    protected Button wibBillingSearch;
    protected Button wibExport;
    protected GridView wdgList;
    protected Pager custPagerBillingList;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.wdpStartDate.Value = DateTime.Now.AddMonths(-1);
      this.wdpEndDate.Value = DateTime.Now;
    }

    protected void btnJavaScriptResponse_Click(object sender, EventArgs e)
    {
    }

    protected void btnBillingSearch_Click(object sender, EventArgs e)
    {
      this.GetBillingReports(true);
    }

    private void GetBillingReports(bool pboolLoadPager)
    {
      this.lblMessage.Visible = false;
      BillingReportPagingParameters objParam = new BillingReportPagingParameters();
      objParam.StartDate = Convert.ToDateTime((object) this.wdpStartDate.Value, (IFormatProvider) CultureInfo.CurrentCulture);
      objParam.EndDate = Convert.ToDateTime((object) this.wdpEndDate.Value, (IFormatProvider) CultureInfo.CurrentCulture);
      objParam.Type = this.rbBilling.Checked ? 1 : 2;
      int pintstartRowIndex = pboolLoadPager ? 1 : this.custPagerBillingList.CurrentPageNumber;
      int pintmaxRows = this.custPagerBillingList.CurrentPageSize == 0 ? 10 : this.custPagerBillingList.CurrentPageSize;
      try
      {
        int pinttotalRows;
        DataTable all = new ManagementPagingBL().BillingReportGetAll(objParam, pintstartRowIndex, pintmaxRows, out pinttotalRows);
        int num = pinttotalRows;
        this.wdgList.DataSource = (object) all;
        this.wdgList.DataBind();
        this.custPagerBillingList.TotalPages = num % pintmaxRows == 0 ? num / pintmaxRows : num / pintmaxRows + 1;
        this.custPagerBillingList.TotalRecordCount = pinttotalRows;
        if (pboolLoadPager)
          this.custPagerBillingList.LoadPager();
        this.wibExport.Enabled = all != null && all.Rows.Count != 0;
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, ex.Message);
        this.HidePopup();
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void btnExport_Click(object sender, EventArgs e) => this.ExcelExport();

    public void ExcelExport()
    {
      DataTable datos = new ManagementPagingBL().BillingReportExport((PagingParameters) new BillingReportPagingParameters()
      {
        StartDate = Convert.ToDateTime((object) this.wdpStartDate.Value, (IFormatProvider) CultureInfo.CurrentCulture),
        EndDate = Convert.ToDateTime((object) this.wdpEndDate.Value, (IFormatProvider) CultureInfo.CurrentCulture),
        Type = (this.rbBilling.Checked ? 1 : 2)
      });
      string str1 = nameof (BillingReport);
      DateTime now = DateTime.Now;
      string[] strArray = new string[8];
      strArray[0] = str1;
      int num = now.Day;
      strArray[1] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      num = now.Month;
      strArray[2] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      num = now.Year;
      strArray[3] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      num = now.Hour;
      strArray[4] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      num = now.Minute;
      strArray[5] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      num = now.Second;
      strArray[6] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      strArray[7] = ".xls";
      string pstrFileTarget = string.Concat(strArray);
      ArrayList titulos = new ArrayList();
      DataTable dataTable = new DataTable();
      string str2 = this.Server.MapPath("../Reports/") + pstrFileTarget;
      OtherFormats otherFormats = new OtherFormats(str2);
      for (int index = 0; index < datos.Columns.Count; ++index)
        titulos.Add((object) datos.Columns[index].ColumnName);
      otherFormats.ExportClaimBook("Reporte Facturacion", titulos, datos);
      new ExportFile().Download(str2, pstrFileTarget);
      if (!File.Exists(str2))
        return;
      File.Delete(str2);
    }

    private string CreatePopUp(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}'); return false;", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void custPagerBillingList_OnPageChanged(object sender, CustomPageChangeArgs e)
    {
      this.GetBillingReports(false);
    }
  }
}
