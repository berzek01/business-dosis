// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Inventory.View.MainRSInventario
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using Microsoft.Reporting.WebForms;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Inventory.View
{
  public class MainRSInventario : Page
  {
    protected Button Button1;
    protected ReportViewer ReportViewer1;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      List<SIIV.BE.SystemParameter> systemParameterList = new SystemParameterManagementBL().Get((object) new ArrayList()
      {
        (object) "318",
        (object) "",
        (object) "1",
        (object) "1"
      });
      string vValue1 = systemParameterList[0].v_Value;
      string vValue2 = systemParameterList[1].v_Value;
      string vValue3 = systemParameterList[2].v_Value;
      string vValue4 = systemParameterList[3].v_Value;
      string vValue5 = systemParameterList[4].v_Value;
      string str = this.Request.QueryString["ReportName"].ToString((IFormatProvider) CultureInfo.CurrentCulture);
      int int32_1 = Convert.ToInt32(this.Request.QueryString["Type"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
      int int32_2 = Convert.ToInt32(this.Request.QueryString["InventoryHistoricId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
      int int32_3 = Convert.ToInt32(this.Request.QueryString["TypeIncidence"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
      this.Title = HttpUtility.UrlDecode(str);
      this.ReportViewer1.ShowCredentialPrompts = false;
      this.ReportViewer1.ServerReport.ReportServerCredentials = (IReportServerCredentials) new CredencialesReporting(vValue1, vValue2, vValue3);
      this.ReportViewer1.ProcessingMode = ProcessingMode.Remote;
      this.ReportViewer1.ServerReport.ReportServerUrl = new Uri(vValue4);
      this.ReportViewer1.ServerReport.ReportPath = vValue5 + str;
      this.ReportViewer1.ProcessingMode = ProcessingMode.Remote;
      if (int32_1 == 1)
        this.ReportViewer1.ServerReport.SetParameters((IEnumerable<ReportParameter>) new ReportParameter[1]
        {
          new ReportParameter("i_InventoryHistoricId", int32_2.ToString((IFormatProvider) CultureInfo.CurrentCulture), false)
        });
      else if (int32_1 == 2)
      {
        this.ReportViewer1.ServerReport.SetParameters((IEnumerable<ReportParameter>) new ReportParameter[1]
        {
          new ReportParameter("i_InventoryHistoricId", int32_2.ToString((IFormatProvider) CultureInfo.CurrentCulture), false)
        });
        this.ReportViewer1.ServerReport.SetParameters((IEnumerable<ReportParameter>) new ReportParameter[1]
        {
          new ReportParameter("i_TypeIncidence", int32_3.ToString((IFormatProvider) CultureInfo.CurrentCulture), false)
        });
      }
    }

    protected void wibReturn_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("InventoryDetailsManagement.aspx?InventoryHistoricId=" + Convert.ToString(Convert.ToInt32(this.Request.QueryString["InventoryHistoricId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture));
    }
  }
}
