// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Reports.MainReportRS
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using Microsoft.Reporting.WebForms;
using SIIV.BE;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web;
using System.Web.UI;

#nullable disable
namespace SIIV.WebApp.Reports
{
  public class MainReportRS : Page
  {
    protected ReportViewer ReportViewer1;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!this.Page.IsPostBack)
      {
        SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
        SystemParameterManagementBL parameterManagementBl = new SystemParameterManagementBL();
        ArrayList pobj1 = new ArrayList()
        {
          (object) "318",
          (object) "",
          (object) "1",
          (object) "1"
        };
        List<SIIV.BE.SystemParameter> systemParameterList1 = parameterManagementBl.Get((object) pobj1);
        string vValue1 = systemParameterList1[0].v_Value;
        string vValue2 = systemParameterList1[1].v_Value;
        string vValue3 = systemParameterList1[2].v_Value;
        string vValue4 = systemParameterList1[3].v_Value;
        string vValue5 = systemParameterList1[4].v_Value;
        string str1 = this.Request.QueryString["ReportName"].ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.Title = HttpUtility.UrlDecode(str1);
        this.ReportViewer1.ShowCredentialPrompts = false;
        this.ReportViewer1.ServerReport.ReportServerCredentials = (IReportServerCredentials) new CredencialesReporting(vValue1, vValue2, vValue3);
        this.ReportViewer1.ProcessingMode = ProcessingMode.Remote;
        this.ReportViewer1.ServerReport.ReportServerUrl = new Uri(vValue4);
        this.ReportViewer1.ServerReport.ReportPath = vValue5 + str1;
        this.ReportViewer1.ProcessingMode = ProcessingMode.Remote;
        if (this.Request.QueryString["ReportGroupId"] == null)
          return;
        ArrayList pobj2 = new ArrayList()
        {
          (object) this.Request.QueryString["ReportGroupId"].ToString((IFormatProvider) CultureInfo.CurrentCulture),
          (object) "",
          (object) "",
          (object) "1"
        };
        List<SIIV.BE.SystemParameter> systemParameterList2 = parameterManagementBl.Get((object) pobj2);
        if (systemParameterList2.Count > 0)
        {
          foreach (SIIV.BE.SystemParameter systemParameter in systemParameterList2)
          {
            string str2 = "";
            bool flag = true;
            bool visible = Convert.ToBoolean((object) systemParameter.i_Visible, (IFormatProvider) CultureInfo.CurrentCulture);
            switch (systemParameter.v_Value.ToUpper(CultureInfo.CurrentCulture).ToString((IFormatProvider) CultureInfo.CurrentCulture))
            {
              case "GETDATE()":
                str2 = !this.isDate(systemParameter.v_ReferenceId) ? DateTime.Today.ToString("yyyy-MM-dd", (IFormatProvider) CultureInfo.CurrentCulture) : systemParameter.v_ReferenceId;
                break;
              case "ROLNAME_ALIAS":
              case "ROLNAME_ROLCONFIG":
              case "ROLNAME_LOCATION":
                string str3 = "Coordinador Counter Lima";
                if (systemUser.v_RoleName.ToUpper(CultureInfo.CurrentCulture) == systemParameter.v_ReferenceId.ToUpper(CultureInfo.CurrentCulture) || systemUser.v_RoleName.ToUpper(CultureInfo.CurrentCulture) == str3.ToUpper(CultureInfo.CurrentCulture))
                {
                  visible = Convert.ToBoolean((object) systemParameter.i_Visible, (IFormatProvider) CultureInfo.CurrentCulture);
                  switch (systemParameter.v_Value.ToUpper(CultureInfo.CurrentCulture).ToString((IFormatProvider) CultureInfo.CurrentCulture))
                  {
                    case "ROLNAME_ALIAS":
                      str2 = systemUser.v_Alias;
                      break;
                    case "ROLNAME_ROLCONFIG":
                      str2 = systemUser.i_RoleConfigId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
                      break;
                    case "ROLNAME_LOCATION":
                      str2 = systemUser.i_LocationId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
                      break;
                    default:
                      str2 = "";
                      break;
                  }
                }
                else
                {
                  visible = false;
                  flag = false;
                  break;
                }
                break;
              default:
                str2 = systemParameter.v_Value;
                break;
            }
            string[] values = new string[1]{ str2 };
            if (flag)
              this.ReportViewer1.ServerReport.SetParameters((IEnumerable<ReportParameter>) new ReportParameter[1]
              {
                new ReportParameter(systemParameter.v_Description, values, visible)
              });
          }
        }
      }
      else
      {
        string script = "Rellena();";
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
    }

    public bool isDate(string value) => DateTime.TryParse(value, out DateTime _);

    protected void ReportViewer1_ReportError(object sender, ReportErrorEventArgs e)
    {
    }

    protected void ReportViewer1_SubmittingParameterValues(
      object sender,
      ReportParametersEventArgs e)
    {
    }

    protected void ReportViewer1_ReportRefresh(object sender, CancelEventArgs e)
    {
    }

    protected void ReportViewer1_PageNavigation(object sender, PageNavigationEventArgs e)
    {
    }

    protected void ReportViewer1_Load(object sender, EventArgs e)
    {
    }

    protected void ReportViewer1_Drillthrough(object sender, DrillthroughEventArgs e)
    {
    }

    protected void ReportViewer1_Init(object sender, EventArgs e)
    {
    }

    protected void ReportViewer1_SubmittingDataSourceCredentials(
      object sender,
      ReportCredentialsEventArgs e)
    {
    }
  }
}
