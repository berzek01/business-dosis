// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Public.PublicSite
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Public
{
  public class PublicSite : MasterPage
  {
    protected HtmlHead Head1;
    protected ContentPlaceHolder HeadContent;
    protected HtmlForm form1;
    protected ScriptManager WebScriptManager1;
    protected Label lblTitlePage;
    protected Label lblSubTitle;
    protected ContentPlaceHolder MainContent;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblTitlePage.Text = HttpUtility.HtmlEncode(this.Page.Title);
    }
  }
}
