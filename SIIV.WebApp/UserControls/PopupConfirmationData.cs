// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.UserControls.PopupConfirmationData
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
  public class PopupConfirmationData : Page
  {
    public string strfnscript;
    public string i_RequirementId;
    public string i_RequirementPlateId;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected Image img;
    protected Label lblPropietario;
    protected Label lblDocumento;
    protected Label lblNumeroDenuncia;
    protected Label lblClaveDenuncia;
    protected Button btnYes;
    protected Button bntNo;

    protected void Page_Load(object sender, EventArgs e)
    {
      int int32 = Convert.ToInt32(this.Request.QueryString["MessageTypeId"], (IFormatProvider) CultureInfo.CurrentCulture);
      string str1 = Convert.ToString(this.Session["OwnerName"], (IFormatProvider) CultureInfo.CurrentCulture);
      string str2 = Convert.ToString(this.Session["NroDoc"], (IFormatProvider) CultureInfo.CurrentCulture);
      string str3 = Convert.ToString(this.Session["OrdenDenuncia"], (IFormatProvider) CultureInfo.CurrentCulture);
      string str4 = Convert.ToString(this.Session["ClaveDenuncia"], (IFormatProvider) CultureInfo.CurrentCulture);
      string str5 = Convert.ToString(this.Session["RegistrationReason"], (IFormatProvider) CultureInfo.CurrentCulture);
      string str6 = this.Request.QueryString["HiddenData"];
      this.lblPropietario.Text = "Propietario: " + str1.ToUpper();
      this.lblDocumento.Text = "DNI: " + str2;
      if (Convert.ToInt32(str5) == 2)
      {
        this.lblNumeroDenuncia.Text = "Nro. de Orden de Denuncia Policial: No aplica (Motivo de registro: Intercambio)";
        this.lblClaveDenuncia.Text = "Clave de Denuncia Policial: No aplica (Motivo de registro: Intercambio)";
      }
      else
      {
        this.lblNumeroDenuncia.Text = "Nro. de Orden de Denuncia Policial: " + str3;
        this.lblClaveDenuncia.Text = "Clave de Denuncia Policial: " + str4;
      }
      if (int32 == 1)
        this.img.ImageUrl = "~/Images/Design/Buttons/Actions/WarningPopup.png";
      else
        this.img.ImageUrl = "~/Images/Design/Buttons/Actions/InformationPopup.png";
    }

    protected void Yes_Click(object sender, EventArgs e)
    {
      this.strfnscript = "SendInfoPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", this.strfnscript, true);
    }

    protected void No_Click(object sender, EventArgs e)
    {
      string script = "OpenNextPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
