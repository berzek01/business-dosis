// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Private.AdminEBIlling
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;

#nullable disable
namespace SIIV.WebApp.Private
{
  public class AdminEBIlling : Page
  {
    protected HtmlForm form1;
    protected HtmlGenericControl frame1;

    protected void Page_Load(object sender, EventArgs e)
    {
      ((HtmlControl) this.FindControl("frame1")).Attributes["src"] = ConfigurationManager.AppSettings["EBillingAdminUrl"].ToString();
    }
  }
}
