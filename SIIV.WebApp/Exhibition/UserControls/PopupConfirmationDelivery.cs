// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.UserControls.PopupConfirmationDelivery
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.UserControls
{
  public class PopupConfirmationDelivery : Page
  {
    public int I_Typemaintainer;
    public string strfnscript;
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
      string str = Convert.ToString(this.Request.QueryString["MessageText"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.I_Typemaintainer = Convert.ToInt32(this.Request.QueryString["Typemaintainer"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.lblMensaje.Text = str;
      if (int32 == 1)
        this.img.ImageUrl = "~/Images/Design/Buttons/Actions/WarningPopup.png";
      else
        this.img.ImageUrl = "~/Images/Design/Buttons/Actions/InformationPopup.png";
    }

    protected void Yes_Click(object sender, ImageClickEventArgs e)
    {
      if (this.I_Typemaintainer == 1)
        this.strfnscript = "SendInfoPopupZone();";
      if (this.I_Typemaintainer == 2)
        this.strfnscript = "SendInfoPopupDistric();";
      if (this.I_Typemaintainer == 3)
        this.strfnscript = "SendInfoPopupCourier();";
      if (this.I_Typemaintainer == 4)
        this.strfnscript = "SendInfoPopupSchedule();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", this.strfnscript, true);
    }

    protected void btNo_Click(object sender, ImageClickEventArgs e)
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
