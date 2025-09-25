// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Migration.AssociatedMigration
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
namespace SIIV.WebApp.Exhibition.Migration
{
  public class AssociatedMigration : Page
  {
    protected HtmlForm form1;
    protected HiddenField validator;
    protected System.Web.UI.ScriptManager WebScriptManager1;
    protected HtmlGenericControl DivChangePassword;
    protected HtmlGenericControl DivValidateCode;
    protected Label Label5;
    protected Label Label26;
    protected TextBox txtUserName;
    protected Label Label6;
    protected TextBox txtPassword;
    protected PasswordStrength txtPassword_PasswordStrength;
    protected RegularExpressionValidator RegularExpressionValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender5;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected ValidatorCalloutExtender ValidatorCalloutExtender2;
    protected RequiredFieldValidator RequiredFieldValidator3;
    protected ValidatorCalloutExtender ValidatorCalloutExtender4;
    protected Label Label7;
    protected TextBox txtRepeatedPassword;
    protected CompareValidator CompareValidator1;
    protected ValidatorCalloutExtender CompareValidator1_ValidatorCalloutExtender;
    protected RequiredFieldValidator ValidatorPassword3;
    protected ValidatorCalloutExtender ValidatorCalloutExtender1;
    protected Button btnAccept;
    protected Button btnSave;
    protected Button btnLater;
    protected Button Button1;
    protected HtmlTableRow tr1;
    protected Label lblMessage2;
    protected HtmlTableRow trAccept;
    protected Label lblMessage;
    private SIIV.BE.SystemUser objSystemUser = new SIIV.BE.SystemUser();

    protected void Page_Load(object sender, EventArgs e)
    {
      this.objSystemUser = (SIIV.BE.SystemUser) this.Session["SystemUser"];
      if (!this.Page.IsPostBack)
      {
        if (this.objSystemUser == null)
          return;
        if (this.objSystemUser.i_StatusChange == 2)
        {
          this.btnAccept.Visible = false;
          this.btnLater.Visible = false;
        }
        this.txtUserName.Text = this.objSystemUser.v_Alias;
        this.txtPassword.Text = "";
        this.txtRepeatedPassword.Text = "";
        this.btnAccept.Visible = false;
      }
      else
        this.lblMessage.Text = "";
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
        (object) SystemParameterGroups.Updatepassword.ToString(),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string pstrEmailSubject = dataTable2.Rows[0]["v_Value"].ToString();
      string pstrEmailBody = string.Format(dataTable2.Rows[1]["v_Value"].ToString(), (object) pstrUserAccount);
      string pstrEmailFrom = dataTable2.Rows[2]["v_Value"].ToString();
      return Email.SendEmail(pstrSMTPUserName, pstrSMTPPassword, pstrEmailSubject, pstrEmailBody, pstrEmailUser, pstrSMTPServer, pintSMTPPort, pstrEmailFrom, boolean);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      string script = "ConfirmEnviar2();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      try
      {
        if (this.txtPassword.Text.Trim() == "")
          Message.SetMessage(this.lblMessage2, enmMessageType.Warning, "Adventencia: <br>Ingrese su contraseña ");
        else if (this.txtRepeatedPassword.Text.Trim() == "")
          Message.SetMessage(this.lblMessage2, enmMessageType.Warning, "Adventencia: <br>Repita nuevamente su contraseña, Tenga presente que deben coincidir");
        else if (this.txtPassword.Text.Trim() != this.txtRepeatedPassword.Text.Trim())
        {
          this.txtPassword.Text = "";
          this.txtRepeatedPassword.Text = "";
          Message.SetMessage(this.lblMessage2, enmMessageType.Warning, "Adventencia: <br>Las contraseñas no coninciden, digitelas nuevamente");
        }
        else
        {
          SIIV.BE.SystemUser pobjBE = new SIIV.BE.SystemUser();
          pobjBE.i_SystemUserId = this.objSystemUser.i_SystemUserId;
          pobjBE.v_Password = this.txtPassword.Text.Trim();
          pobjBE.d_UpdateDate = new DateTime?(DateTime.Now);
          pobjBE.i_UpdateUserId = new int?(0);
          if (this.objSystemUser.v_Password == Cryptography.GetHashMD5(pobjBE.v_Password))
            this.RequiredFieldValidator3.IsValid = false;
          else if (new SystemUserManagementBL().ChangePassword(pobjBE) == 0)
          {
            this.lblMessage.Text = "¡ Lo Sentimos , No se ha podido procesar el cambio de su contraseña, intente nuevamente. !";
            this.Session["ForgotSystemUser"] = (object) null;
          }
          else
          {
            this.objSystemUser.i_StatusChange = 0;
            this.Session["SystemUser"] = (object) this.objSystemUser;
            new SystemUserQueriesBL().UserUpdatePassword(pobjBE);
            if (this.SendEmailFinaly(this.objSystemUser.i_SystemUserId.ToString((IFormatProvider) CultureInfo.CurrentCulture), this.objSystemUser.v_Alias, this.objSystemUser.v_Email))
            {
              this.Label5.Text = "¡ Se realizó el cambio de contraseña correctamente !";
              this.lblMessage2.Text = "Su contraseña tiene un plazo maximo de duracion de 6 Meses";
              this.txtPassword.Enabled = false;
              this.txtRepeatedPassword.Enabled = false;
              this.btnLater.Visible = false;
              this.btnSave.Visible = false;
              this.btnAccept.Visible = true;
              this.Session["ForgotSystemUser"] = (object) null;
            }
            else
            {
              this.txtPassword.Enabled = false;
              this.txtRepeatedPassword.Enabled = false;
              this.btnLater.Visible = false;
              this.btnSave.Visible = false;
              this.btnAccept.Visible = true;
              this.Label5.Text = "¡ Se realizó el cambio de contraseña correctamente !";
              this.lblMessage2.Text = "Se realizo el cambio de contraseña, pero aparentemente el correo proporcionado no es válido y el  Email de Confirmacion que te sería enviado ha sido rechazado.";
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      finally
      {
      }
    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "strfnscripts", script, true);
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
