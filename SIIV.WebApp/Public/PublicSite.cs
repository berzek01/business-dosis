// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Public.PublicSite
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

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
