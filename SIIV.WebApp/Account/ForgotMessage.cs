// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Account.ForgotMessage
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.SystemParameter.BL;
using SIIV.SystemUser.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Account
{
  public class ForgotMessage : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected HiddenField validator;
    protected System.Web.UI.ScriptManager WebScriptManager1;
    protected Label lblTituloPage;
    protected Label lblUserAccount;
    protected Label lblMessage;
    protected HtmlGenericControl DivValidateCode;
    protected Label Label19;
    protected Label Label20;
    protected TextBox txtUser;
    protected RequiredFieldValidator ValidatorUser;
    protected ValidatorCalloutExtender ValidatorUser_ValidatorCalloutExtender;
    protected Label Label21;
    protected TextBox txtCodigo;
    protected RequiredFieldValidator ValidatorCodigo;
    protected ValidatorCalloutExtender ValidatorCodigo_ValidatorCalloutExtender;
    protected Button wibValidar;
    protected Button wibCancel;
    protected HtmlGenericControl DivChangePassword;
    protected Label Label1;
    protected Label Label2;
    protected TextBox txtPassword;
    protected RequiredFieldValidator ValidatorPassword;
    protected ValidatorCalloutExtender ValidatorPassword_ValidatorCalloutExtender;
    protected PasswordStrength txtPassword_PasswordStrength;
    protected RegularExpressionValidator RegularExpressionValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender5;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender3;
    protected Label Label3;
    protected TextBox txtPasswordRepeat;
    protected CompareValidator CompareValidator1;
    protected ValidatorCalloutExtender CompareValidator1_ValidatorCalloutExtender;
    protected Button BtnFinalizar;
    protected Label lblMessage2;
    protected Label lblMessage3;
    protected Label lblfooter;
    protected Label lblSign;
    protected HyperLink hlnkRedirect;

    protected void Page_Load(object sender, EventArgs e)
    {
      object obj = this.Session["SendEmail"];
      SIIV.BE.SystemUser systemUser = this.Session["ForgotSystemUser"] as SIIV.BE.SystemUser;
      if (!this.Page.IsPostBack)
      {
        if (systemUser != null)
        {
          DataTable dataTable = new SystemUserQueriesBL().SystemUserRead(systemUser.i_SystemUserId);
          this.Session["SystemUserValidate"] = (object) dataTable;
          systemUser.v_FirstName = dataTable.Rows[0]["v_FirstName"].ToString();
          systemUser.v_Email = dataTable.Rows[0]["v_Email"].ToString();
          systemUser.i_Status = new int?(Convert.ToInt32(dataTable.Rows[0]["i_Status"].ToString()));
          if (systemUser.i_Status.GetValueOrDefault() == 1)
          {
            if (obj != null)
            {
              if (this.SendEmail(systemUser.i_SystemUserId.ToString((IFormatProvider) CultureInfo.CurrentCulture), systemUser.v_Alias, systemUser.v_Code, systemUser.v_Email))
              {
                this.DivValidateCode.Visible = true;
                this.lblTituloPage.Text = "Hola ";
                this.lblUserAccount.Text = systemUser.v_FirstName;
                int length = systemUser.v_Email.IndexOf("@");
                string str = systemUser.v_Email.Substring(0, length);
                string oldValue = systemUser.v_Email.Substring(3, str.Length - 3);
                this.lblMessage2.Text = string.Format(this.lblMessage2.Text, (object) systemUser.v_Email.Replace(oldValue, "**********"));
                systemUser.i_Status = new int?(2);
                this.Session["SendEmail"] = (object) null;
                this.Session["RegisterSystemUser"] = (object) systemUser;
              }
              else
              {
                if (this.Session["isValidate"] == null)
                {
                  this.DivValidateCode.Visible = false;
                  this.lblTituloPage.Text = "Lo sentimos ";
                  this.lblUserAccount.Text = systemUser.v_FirstName;
                  this.lblMessage.Text = "";
                  this.lblMessage2.Text = "No hemos podido completar tu recuperación de contraseña, aparentemente el correo proporcionado no es válido y el <br> Email que te sería enviado ha sido rechazado. Por favor vuelve a intentar <br> ingresando un correo valido.";
                  this.lblMessage3.Text = "";
                }
                this.lblMessage2.Text = "Para restablecer su contraseña debe validar el código de verificación que enviamos al correo electrónico : <br> <b> " + systemUser.v_Email + " </b> <br/><br/>";
              }
            }
            else if (this.Session["isValidate"] != null)
            {
              this.DivValidateCode.Visible = true;
              this.lblTituloPage.Text = "Hola ";
              this.lblUserAccount.Text = systemUser.v_FirstName;
              int length = systemUser.v_Email.IndexOf("@");
              string str = systemUser.v_Email.Substring(0, length);
              string oldValue = systemUser.v_Email.Substring(3, str.Length - 3);
              this.lblMessage2.Text = string.Format(this.lblMessage2.Text, (object) systemUser.v_Email.Replace(oldValue, "**********"));
              systemUser.i_Status = new int?(2);
              this.Session["SendEmail"] = (object) null;
              this.Session["RegisterSystemUser"] = (object) systemUser;
            }
            else
            {
              this.lblUserAccount.Text = ((DataTable) this.Session["SystemUserValidate"]).Rows[0]["v_FirstName"].ToString();
              this.DivValidateCode.Visible = false;
              this.DivChangePassword.Visible = true;
              this.lblTituloPage.Text = "Muy bien";
              this.lblMessage.Text = "¡ Se realizó la validación de tu cuenta correctamente !";
              this.lblMessage2.Text = "Crea una Contraseña segura que no utilices en otros sitios web. Debe tener 8 caracteres como mínimo";
              this.lblMessage3.Text = "";
              this.lblfooter.Text = "";
            }
          }
          else if (systemUser != null)
          {
            this.DivValidateCode.Visible = false;
            this.lblTituloPage.Text = "Tu cuenta de usuario no se encuentra Habilitada";
            this.lblUserAccount.Text = "";
            this.lblMessage.Text = "Hola " + systemUser.v_FirstName + " tu cuenta de usuario no está habilitada,<br> por esta razón no se pudo completar tu recuperación de contraseña.";
            this.lblfooter.Text = "Disculpe los inconvenientes.";
            this.lblSign.Text = "Asociación Automotriz del Perú.";
          }
          else
          {
            this.DivValidateCode.Visible = false;
            this.lblTituloPage.Text = "Recuperación de contraseña Incorrecto";
            this.lblUserAccount.Text = "";
            this.lblMessage.Text = "El vinculo no es válido o ya caducó.";
            this.lblfooter.Text = "Disculpe los inconvenientes.";
            this.lblSign.Text = "Asociación Automotriz del Perú.";
          }
        }
        else
          this.Response.Redirect("~/index.aspx");
      }
      else if (this.Session["isValidate"] != null)
      {
        this.DivValidateCode.Visible = true;
        this.lblTituloPage.Text = "Hola ";
        this.lblUserAccount.Text = systemUser.v_FirstName;
        int length = systemUser.v_Email.IndexOf("@");
        string str = systemUser.v_Email.Substring(0, length);
        string oldValue = systemUser.v_Email.Substring(3, str.Length - 3);
        this.lblMessage2.Text = string.Format(this.lblMessage2.Text, (object) systemUser.v_Email.Replace(oldValue, "**********"));
        systemUser.i_Status = new int?(2);
        this.Session["SendEmail"] = (object) null;
        this.Session["RegisterSystemUser"] = (object) systemUser;
      }
      else
      {
        this.lblUserAccount.Text = ((DataTable) this.Session["SystemUserValidate"]).Rows[0]["v_FirstName"].ToString();
        this.DivValidateCode.Visible = false;
        this.DivChangePassword.Visible = true;
        this.lblTituloPage.Text = "Muy bien";
        this.lblMessage.Text = "¡ Se realizó la validación de tu cuenta correctamente !";
        this.lblMessage2.Text = "Crea una Contraseña segura que no utilices en otros sitios web. Debe tener 8 caracteres como mínimo";
        this.lblMessage3.Text = "";
        this.lblfooter.Text = "";
      }
    }

    public bool SendEmail(
      string pstrUserId,
      string pstrUserAccount,
      string pstrPassword,
      string pstrEmailUser)
    {
      DataTable dataTable1 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.SMTPServerConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string pstrSMTPServer = dataTable1.Rows[0]["v_Value"].ToString();
      int pintSMTPPort = int.Parse(dataTable1.Rows[1]["v_Value"].ToString());
      string pstrSMTPUserName = dataTable1.Rows[2]["v_Value"].ToString();
      string pstrSMTPPassword = dataTable1.Rows[3]["v_Value"].ToString();
      bool boolean = Convert.ToBoolean(dataTable1.Rows[4]["v_Value"]);
      DataTable dataTable2 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.ForgotEmailConfiguration.ToString(),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string pstrEmailSubject = dataTable2.Rows[0]["v_Value"].ToString();
      string pstrEmailBody = string.Format(dataTable2.Rows[1]["v_Value"].ToString(), (object) pstrUserAccount, (object) pstrPassword);
      string pstrEmailFrom = dataTable2.Rows[2]["v_Value"].ToString();
      return Email.SendEmail(pstrSMTPUserName, pstrSMTPPassword, pstrEmailSubject, pstrEmailBody, pstrEmailUser, pstrSMTPServer, pintSMTPPort, pstrEmailFrom, boolean);
    }

    public bool SendEmailFinaly(string pstrUserId, string pstrUserAccount, string pstrEmailUser)
    {
      DataTable dataTable1 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.SMTPServerConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string pstrSMTPServer = dataTable1.Rows[0]["v_Value"].ToString();
      int pintSMTPPort = int.Parse(dataTable1.Rows[1]["v_Value"].ToString());
      string pstrSMTPUserName = dataTable1.Rows[2]["v_Value"].ToString();
      string pstrSMTPPassword = dataTable1.Rows[3]["v_Value"].ToString();
      bool boolean = Convert.ToBoolean(dataTable1.Rows[4]["v_Value"]);
      DataTable dataTable2 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.ForgotEmailConfiguration.ToString(),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string pstrEmailSubject = dataTable2.Rows[0]["v_Value"].ToString();
      string pstrEmailBody = string.Format("" + "<html><head></head>" + "<body bgcolor =\"#E1F4FF\">" + "<table style =\"width:100%; font-family: Helvetica; font-size: 12.5pt; color: #70706E; text-align: left; font-weight: normal; font-style: normal;\">" + "<tr><td> Bienvenido al Sistema de Identificación Vehicular(SIIV)</td></tr>" + "<tr><td><p>Estimado(a) Usuario : <strong>{0}</strong></p></td></tr>" + "<tr><td> Se ha cambiado su contraseña en el sistema SIIV - <a href =\"http://www.placas.pe/\" target =\"_blank\"> http://www.placas.pe</a></td></tr>" + "<tr><td><p>Si Ud. no realizó esta operación comuniquese de inmediato con la Asociación Automotriz del Perú (AAP). Caso contrario omita este mensaje</p></td></tr>" + "<tr><td> Muchas gracias.</td></tr>" + "<tr><td> AAP -  Asociación Automotriz del Perú</td></tr>" + "</table></body></html>", (object) pstrUserAccount);
      string pstrEmailFrom = dataTable2.Rows[2]["v_Value"].ToString();
      return Email.SendEmail(pstrSMTPUserName, pstrSMTPPassword, pstrEmailSubject, pstrEmailBody, pstrEmailUser, pstrSMTPServer, pintSMTPPort, pstrEmailFrom, boolean);
    }

    protected void wibValidar_Click(object sender, EventArgs e)
    {
      if (this.Session["isValidate"] == null)
        return;
      int pintSystemUserId = 0;
      if (this.txtUser.Text == string.Empty)
        this.ValidatorUser.IsValid = false;
      else if (this.txtCodigo.Text == string.Empty)
      {
        this.ValidatorCodigo.IsValid = false;
      }
      else
      {
        try
        {
          SIIV.BE.SystemUser systemUser = new SIIV.BE.SystemUser();
          string pstrMessage;
          if (!new SystemUserQueriesBL().ValidateCodeVerification(this.txtUser.Text.Trim(), this.txtCodigo.Text.Trim(), out pstrMessage, out pintSystemUserId))
          {
            this.lblTituloPage.Text = "Lo Sentimos";
            this.lblMessage.Text = "¡ " + pstrMessage + " !";
          }
          else
          {
            this.lblUserAccount.Text = ((DataTable) this.Session["SystemUserValidate"]).Rows[0]["v_FirstName"].ToString();
            this.DivValidateCode.Visible = false;
            this.DivChangePassword.Visible = true;
            this.lblTituloPage.Text = "Muy bien";
            this.lblMessage.Text = "¡ Se realizó la validación de tu cuenta correctamente !";
            this.lblMessage2.Text = "Crea una Contraseña segura que no utilices en otros sitios web. Debe tener 8 caracteres como mínimo";
            this.lblMessage3.Text = "";
            this.lblfooter.Text = "";
            this.Session["isValidate"] = (object) null;
          }
        }
        catch (Exception ex)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Error, "Problemas al procesar información:<br>" + ex.Message);
        }
      }
    }

    protected void BtnFinalizar_Click(object sender, EventArgs e)
    {
      if (this.txtPassword.Text.Trim() == string.Empty)
        this.ValidatorPassword.IsValid = false;
      else if (this.txtPasswordRepeat.Text.Trim() == string.Empty)
        this.CompareValidator1.IsValid = false;
      else if (this.txtPassword.Text.Trim() != this.txtPasswordRepeat.Text.Trim())
        this.CompareValidator1.IsValid = false;
      else if (this.txtPassword.Text.Length <= 7)
      {
        this.RequiredFieldValidator2.IsValid = false;
      }
      else
      {
        SIIV.BE.SystemUser pobjBE = new SIIV.BE.SystemUser();
        DataTable dataTable1 = (DataTable) this.Session["SystemUserValidate"];
        pobjBE.i_SystemUserId = Convert.ToInt32(dataTable1.Rows[0]["i_SystemUserId"].ToString());
        pobjBE.v_Password = this.txtPassword.Text.Trim();
        pobjBE.d_UpdateDate = new DateTime?(DateTime.Now);
        pobjBE.i_UpdateUserId = new int?(0);
        if (new SystemUserManagementBL().ChangePassword(pobjBE) == 0)
        {
          this.lblTituloPage.Text = "Lo Sentimos";
          this.lblMessage.Text = "¡ No se ha podido procesar el cambio de su contraseña, intente nuevamente. !";
          this.Session["ForgotSystemUser"] = (object) null;
        }
        else
        {
          DataTable dataTable2 = (DataTable) this.Session["SystemUserValidate"];
          pobjBE.v_Alias = dataTable2.Rows[0]["v_Alias"].ToString();
          pobjBE.v_Email = dataTable2.Rows[0]["v_Email"].ToString();
          pobjBE.i_Status = new int?(Convert.ToInt32(dataTable2.Rows[0]["i_Status"].ToString()));
          if (this.SendEmailFinaly(pobjBE.i_SystemUserId.ToString((IFormatProvider) CultureInfo.CurrentCulture), pobjBE.v_Alias, pobjBE.v_Email))
          {
            this.lblUserAccount.Text = dataTable2.Rows[0]["v_FirstName"].ToString();
            this.DivValidateCode.Visible = false;
            this.DivChangePassword.Visible = false;
            this.lblTituloPage.Text = "Estimado(a)";
            this.lblMessage.Text = "¡ Se realizó el cambio de contraseña correctamente !";
            this.lblMessage2.Text = "Recuerde que su contraseña es de uso personal";
            this.lblMessage3.Text = "";
            this.lblfooter.Text = "Muchas Gracias";
            this.Session["ForgotSystemUser"] = (object) null;
          }
          else
          {
            this.DivValidateCode.Visible = false;
            this.lblTituloPage.Text = "Lo sentimos ";
            this.lblUserAccount.Text = dataTable2.Rows[0]["v_FirstName"].ToString();
            this.lblMessage.Text = "";
            this.lblMessage2.Text = "No hemos podido completar tu cambio de contraseña, aparentemente el correo proporcionado no es válido y el <br> Email que te sería enviado ha sido rechazado. Por favor vuelve a intentar <br> ingresando un correo valido.";
            this.lblMessage3.Text = "";
          }
        }
      }
    }

    protected void wibVolver_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("~/index.aspx");
    }

    public enum PasswordScore
    {
      VeryPoor,
      Weak,
      Average,
      Strong,
      Excellent,
    }
  }
}
