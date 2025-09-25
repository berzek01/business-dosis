// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Public.emailtrack
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Requirement.BL;
using System;
using System.Web.UI;

#nullable disable
namespace SIIV.WebApp.Public
{
  public class emailtrack : Page
  {
    private void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack || this.Request.Params["i_EmailNotificationId"] == null)
        return;
      this.StampSentEmail(this.Request.Params["i_TypeNotificationId"].ToString(), this.Request.Params["i_EmailNotificationId"].ToString());
    }

    protected override void OnInit(EventArgs e)
    {
      this.InitializeComponent();
      base.OnInit(e);
    }

    private void InitializeComponent() => this.Load += new EventHandler(this.Page_Load);

    private void StampSentEmail(string i_TypeNotificationId, string i_NotificationId)
    {
      if (new RequirementQueriesBL().UpdateConfirmationReadEmail(Convert.ToInt32(i_TypeNotificationId), Convert.ToInt32(i_NotificationId)) != 0)
        return;
      this.Response.Redirect("~/Default.aspx");
    }
  }
}
