// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Common.SystemError
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.SystemParameter.BL;
using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Common
{
  public class SystemError : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected HtmlTable tblLogin;
    protected Image imgSystemError;
    protected Label lblMessageTitle;
    protected Label lblMessageDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.lblMessageTitle.Text = Constants.ERROR_UserMessageTitle;
      if (this.Request.QueryString["aspxerrorpath"] != null)
      {
        string str = this.Request.QueryString["aspxerrorpath"].ToString();
        if (str != "")
          this.lblMessageDetail.Text = "Error en: <b>" + str + "</b>";
      }
      if (this.Request.QueryString["ErrId"] != null)
      {
        switch (this.Request.QueryString["ErrId"])
        {
          case "403":
            this.lblMessageDetail.Text = Constants.ERROR_NoAccess;
            break;
          case "404":
            this.lblMessageDetail.Text = Constants.ERROR_PageNoFound;
            break;
        }
      }
    }

    protected void btnLogIn_Click(object sender, EventArgs e)
    {
      this.Response.Redirect(ConfigurationManager.AppSettings["StartPage"]);
    }
  }
}
