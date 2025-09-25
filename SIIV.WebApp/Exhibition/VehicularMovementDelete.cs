// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.VehicularMovementDelete
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition
{
  public class VehicularMovementDelete : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected HtmlTableRow TdRenovar;
    protected ImageButton imgSi;
    protected ImageButton imgNo;
    protected HtmlTableRow TdInfo;
    protected TextBox txtObservation;
    protected Button wibAccept;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.TdRenovar.Visible = true;
      this.TdInfo.Visible = false;
    }

    protected void imgSi_Click(object sender, ImageClickEventArgs e)
    {
      this.TdRenovar.Visible = false;
      this.TdInfo.Visible = true;
    }

    protected void imgNo_Click(object sender, ImageClickEventArgs e)
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void wibAccept_Click(object sender, EventArgs e)
    {
      this.Session["Observation"] = (object) this.txtObservation.Text;
      string script = "SendInfoPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }
  }
}
