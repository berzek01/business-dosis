// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.UserControls.PopupConfirmationDeleteAlert
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.UserControls
{
  public class PopupConfirmationDeleteAlert : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected Image img;
    protected Label lblMensaje;
    protected ImageButton Yes;
    protected Label Label1;
    protected ImageButton btNo;

    protected void Page_Load(object sender, EventArgs e)
    {
      int int32 = Convert.ToInt32(this.Request.QueryString["MessageTypeId"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.lblMensaje.Text = Convert.ToString(this.Request.QueryString["MessageText"], (IFormatProvider) CultureInfo.CurrentCulture);
      if (int32 == 1)
        this.img.ImageUrl = "~/Images/Design/Buttons/Actions/WarningPopup.png";
      else
        this.img.ImageUrl = "~/Images/Design/Buttons/Actions/InformationPopup.png";
    }

    protected void Yes_Click(object sender, ImageClickEventArgs e)
    {
      string script = "SendInfoPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void btNo_Click(object sender, ImageClickEventArgs e)
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
