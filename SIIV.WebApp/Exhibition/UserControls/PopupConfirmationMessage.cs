// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.UserControls.PopupConfirmationMessage
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
  public class PopupConfirmationMessage : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected HtmlGenericControl contAddMoto;
    protected Label lblDistrito;
    protected Label lblFecha;
    protected Label lblTurno;
    protected ImageButton Yes;
    protected Label Label1;
    protected ImageButton btNo;

    protected void Page_Load(object sender, EventArgs e)
    {
      Convert.ToInt32(this.Request.QueryString["MessageTypeId"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.lblDistrito.Text = Convert.ToString(this.Request.QueryString["Distrito"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.lblFecha.Text = Convert.ToString(this.Request.QueryString["Dia"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.lblTurno.Text = Convert.ToString(this.Request.QueryString["Horario"], (IFormatProvider) CultureInfo.CurrentCulture);
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
