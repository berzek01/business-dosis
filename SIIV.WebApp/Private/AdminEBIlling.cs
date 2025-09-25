// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Private.AdminEBIlling
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

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
