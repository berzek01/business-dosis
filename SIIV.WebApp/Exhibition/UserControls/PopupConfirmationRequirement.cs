// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.UserControls.PopupConfirmationRequirement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.UserControls
{
  public class PopupConfirmationRequirement : Page
  {
    public int I_Typemaintainer;
    public string strfnscript;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected Image img;
    protected Button btnYes;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.img.ImageUrl = "~/Images/Design/Buttons/Actions/InformationPopup.png";
    }

    protected void Yes_Click(object sender, EventArgs e)
    {
      this.strfnscript = "SendInfoPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", this.strfnscript, true);
    }
  }
}
