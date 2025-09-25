// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.PaymentPOS.RegisterPaymentPOS
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.PaymentPOS
{
  public class RegisterPaymentPOS : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtCod;
    protected Label lblCodPago;
    protected Label lblMontoPagar;
    protected Button btnAcred;
    protected Button btnCerrar;
    protected Label lblMensage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      if (this.Request.QueryString["CodPago"] != null && this.Request.QueryString["Monto"] != null)
      {
        this.txtCod.Focus();
        this.lblCodPago.Text = this.Request.QueryString["CodPago"].ToString();
        this.lblMontoPagar.Text = this.Request.QueryString["Monto"].ToString();
        this.btnAcred.Enabled = true;
      }
      else
        Message.SetMessage(this.lblMensage, new HandledException(1, "Error al cargar la información de la pagina."));
    }

    protected void btnAcred_Click(object sender, EventArgs e)
    {
      if (this.txtCod.Text != null || this.txtCod.Text != "")
      {
        RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
        if (requirementQueriesBl.UpdateETicketVisa(this.lblCodPago.Text, this.txtCod.Text.TrimEnd(), "") <= 0 || requirementQueriesBl.AccreditPayment(this.lblCodPago.Text, 1, "") <= 0)
          return;
        Message.SetMessage(this.lblMensage, enmMessageType.Success, "Código de pago acreditado correctamente.");
        this.btnAcred.Enabled = false;
      }
      else
        Message.SetMessage(this.lblMensage, enmMessageType.Warning, "Ingrese el código del comprabante.");
    }

    protected void btnCerrar_Click(object sender, EventArgs e)
    {
      string script = "SendInfoPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }
  }
}
