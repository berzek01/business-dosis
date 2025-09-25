// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Account.Register
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Account
{
  public class Register : Page
  {
    protected CreateUserWizard RegisterUser;
    protected CreateUserWizardStep RegisterUserWizardStep;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.RegisterUser.ContinueDestinationPageUrl = this.Request.QueryString["ReturnUrl"];
    }

    protected void RegisterUser_CreatedUser(object sender, EventArgs e)
    {
      FormsAuthentication.SetAuthCookie(this.RegisterUser.UserName, false);
      string url = this.RegisterUser.ContinueDestinationPageUrl;
      if (string.IsNullOrEmpty(url))
        url = "~/";
      this.Response.Redirect(url);
    }
  }
}
