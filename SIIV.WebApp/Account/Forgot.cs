// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Account.Forgot
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.Common.Resource;
using SIIV.SystemUser.BL;
using SIIV.WebApp.ig_res.CustomControls;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Account
{
  public class Forgot : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager WebScriptManager1;
    protected ModalPopupExtender ModalProgreso;
    protected Label lblTituloPage;
    protected Label lblSubTituloPage;
    protected Label lblMessage;
    protected Label Label1;
    protected HtmlGenericControl DivDelivery;
    protected RadioButtonList rblPersonType;
    protected HtmlGenericControl DivDatosDelivery;
    protected Label Label4;
    protected TextBox txtUserAccount;
    protected FilteredTextBoxExtender txtUserAccount_FilteredTextBoxExtender;
    protected RequiredFieldValidator ValidatorAccount;
    protected ValidatorCalloutExtender ValidatorAccount_ValidatorCalloutExtender;
    protected HtmlGenericControl WebCaptcha1;
    protected Image Image2;
    protected Button btnRefreshCatcha;
    protected TextBox txtimgcode;
    protected RequiredFieldValidator ValidatorCatcha;
    protected ValidatorCalloutExtender ValidatorCatcha_ValidatorCalloutExtender;
    protected Button wibContinue;
    protected Button wibCancel;
    protected Panel PanLoad;
    protected ucProgress ucProgress1;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.lblMessage.Visible = false;
      this.Habilitarcontroles(true);
    }

    protected void rblPersonType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.rblPersonType.SelectedIndex == 0)
      {
        this.Habilitarcontroles(true);
        this.Label4.Text = "Cuenta de Usuario:";
        this.txtUserAccount.Text = "";
        this.txtUserAccount_FilteredTextBoxExtender.ValidChars = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz1234567890@-._+*/&%$#!°[]:;><";
      }
      else if (this.rblPersonType.SelectedIndex == 1)
      {
        this.Habilitarcontroles(false);
        this.Label4.Text = "Número Documento:";
        this.txtUserAccount.Text = "";
        this.txtUserAccount_FilteredTextBoxExtender.ValidChars = "1234567890";
      }
      else
      {
        if (this.rblPersonType.SelectedIndex != 2)
          return;
        this.Habilitarcontroles(false);
        this.Label4.Text = "Correo Electronico:";
        this.txtUserAccount.Text = "";
        this.txtUserAccount_FilteredTextBoxExtender.ValidChars = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz1234567890@-._";
      }
    }

    protected void txtUserAccount_TextChanged(object sender, EventArgs e)
    {
    }

    protected void txtQuestion_TextChanged(object sender, EventArgs e)
    {
    }

    protected void txtAnswer_TextChanged(object sender, EventArgs e)
    {
    }

    protected void wibContinue_Click(object sender, EventArgs e)
    {
      int pintSystemUserId = 0;
      if (this.rblPersonType.SelectedIndex == 0 && this.txtUserAccount.Text.Trim() == string.Empty)
        this.ValidatorAccount.IsValid = false;
      else if (this.rblPersonType.SelectedIndex == 1 && this.txtUserAccount.Text.Trim() == string.Empty)
        this.ValidatorAccount.IsValid = false;
      else if (this.rblPersonType.SelectedIndex == 2 && this.txtUserAccount.Text.Trim() == string.Empty)
        this.ValidatorAccount.IsValid = false;
      else if (this.txtimgcode.Text != string.Empty)
      {
        if (this.Session["CaptchaImageText"].ToString() != this.txtimgcode.Text.ToString())
        {
          this.ValidatorCatcha.IsValid = false;
          this.ValidatorCatcha.ErrorMessage = "Los digitos ingresados no concuerdan con la imagen mostrada.";
          this.txtimgcode.Text = string.Empty;
        }
        else
        {
          try
          {
            SIIV.BE.SystemUser pobjBE = new SIIV.BE.SystemUser();
            string pstrMessage;
            bool flag;
            if (this.rblPersonType.SelectedIndex == 0)
            {
              flag = new SystemUserQueriesBL().ForgotUserValidate(this.txtUserAccount.Text, out pstrMessage, out pintSystemUserId);
              pobjBE.v_Alias = this.txtUserAccount.Text;
            }
            else if (this.rblPersonType.SelectedIndex == 1)
            {
              string pstrAlias;
              flag = new SystemUserQueriesBL().ForgotUserValidateDNI(this.txtUserAccount.Text, out pstrMessage, out pintSystemUserId, out pstrAlias);
              pobjBE.v_Alias = pstrAlias;
            }
            else
            {
              string pstrAlias;
              flag = new SystemUserQueriesBL().ForgotUserValidateEMAIL(this.txtUserAccount.Text, out pstrMessage, out pintSystemUserId, out pstrAlias);
              pobjBE.v_Alias = pstrAlias;
            }
            if (!flag)
            {
              Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Advertencia :<br>" + pstrMessage);
            }
            else
            {
              string str = Guid.NewGuid().ToString().Substring(0, 8);
              pobjBE.i_SystemUserId = pintSystemUserId;
              pobjBE.v_Code = str;
              pobjBE.d_InsertDate = new DateTime?(DateTime.Now);
              pobjBE.i_Status = new int?(1);
              this.Session["ForgotSystemUser"] = new SystemUserQueriesBL().VerificationCodeInsert(pobjBE) != 0 ? (object) pobjBE : throw new DataException("***No se ha podido procesar el recordatorio de su cuenta, intente en unos minutos.");
              this.Session["SendEmail"] = (object) 1;
              this.Session["isValidate"] = (object) 1;
              this.Response.Redirect("~/Account/ForgotMessage.aspx", false);
            }
          }
          catch (Exception ex)
          {
            Message.SetMessage(this.lblMessage, enmMessageType.Error, "Problemas al procesar información:<br>" + ex.Message);
            this.HidePopup();
          }
          finally
          {
            this.HidePopup();
          }
        }
      }
      else
      {
        this.ValidatorCatcha.IsValid = false;
        this.ValidatorCatcha.ErrorMessage = "Los digitos ingresados no concuerdan con la imagen mostrada.";
        this.txtimgcode.Text = string.Empty;
      }
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void wibCancel_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("~/index.aspx");
    }

    protected void Habilitarcontroles(bool bolStatus) => this.WebCaptcha1.Visible = true;

    protected void btnRefreshCatcha_Click(object sender, EventArgs e)
    {
      this.txtimgcode.Text = string.Empty;
      new Image().ImageUrl = "~/UserControls/FrmCaptcha.aspx";
    }
  }
}
