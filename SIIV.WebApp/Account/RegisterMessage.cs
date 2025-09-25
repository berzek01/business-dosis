// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Account.RegisterMessage
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.Common.Resource.Utilities;
using SIIV.SystemParameter.BL;
using SIIV.SystemUser.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Account
{
  public class RegisterMessage : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected Label lblTituloPage;
    protected Label lblUserAccount;
    protected Label lblMessage;
    protected Label lblfooter;
    protected Label lblSign;
    protected HyperLink hlnkRedirect;

    public bool SendEmail(
      string pstrUserId,
      string pstrUserAccount,
      string pstrPassword,
      string pstrEmailUser)
    {
      Email email = new Email();
      DataTable dataTable1 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.SMTPServerConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string pstrSMTPServer = dataTable1.Rows[0]["v_Value"].ToString();
      int pintSMTPPort = int.Parse(dataTable1.Rows[1]["v_Value"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
      string pstrSMTPUserName = dataTable1.Rows[2]["v_Value"].ToString();
      string pstrSMTPPassword = dataTable1.Rows[3]["v_Value"].ToString();
      bool boolean = Convert.ToBoolean(dataTable1.Rows[4]["v_Value"], (IFormatProvider) CultureInfo.CurrentCulture);
      DataTable dataTable2 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.ConfigurationRegisterAccount.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string pstrEmailSubject = dataTable2.Rows[0]["v_Value"].ToString();
      string format1 = dataTable2.Rows[1]["v_Value"].ToString();
      string format2 = dataTable2.Rows[2]["v_Value"].ToString();
      DataTable dataTable3 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.Cryptography.ToString(),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string str1 = HttpUtility.UrlEncode(new Rijndael(dataTable3.Rows[0]["v_Value"].ToString(), dataTable3.Rows[1]["v_Value"].ToString()).Encrypt(pstrUserId));
      string str2 = string.Format((IFormatProvider) CultureInfo.CurrentCulture, format2, new object[1]
      {
        (object) str1
      });
      string pstrEmailBody = string.Format((IFormatProvider) CultureInfo.CurrentCulture, format1, new object[3]
      {
        (object) pstrUserAccount,
        (object) pstrPassword,
        (object) str2
      });
      string pstrEmailFrom = dataTable2.Rows[3]["v_Value"].ToString();
      return Email.SendEmail(pstrSMTPUserName, pstrSMTPPassword, pstrEmailSubject, pstrEmailBody, pstrEmailUser, pstrSMTPServer, pintSMTPPort, pstrEmailFrom, boolean);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      int num1;
      if (this.Session["RegisterSystemUser"] is SIIV.BE.SystemUser systemUser1)
      {
        int? iStatus = systemUser1.i_Status;
        int num2 = 0;
        num1 = iStatus.GetValueOrDefault() == num2 & iStatus.HasValue ? 1 : 0;
      }
      else
        num1 = 0;
      SIIV.BE.SystemUser systemUser2;
      if (num1 != 0)
      {
        if (this.SendEmail(systemUser1.i_SystemUserId.ToString((IFormatProvider) CultureInfo.CurrentCulture), systemUser1.v_Alias, systemUser1.v_Password, systemUser1.v_Email))
        {
          this.lblTituloPage.Text = "Bienvenido ";
          this.lblUserAccount.Text = systemUser1.v_FirstName;
          this.lblMessage.Text = string.Format(this.lblMessage.Text, (object) systemUser1.v_Email);
          this.Session["RegisterSystemUser"] = (object) systemUser1;
        }
        else
        {
          this.lblTituloPage.Text = "Lo sentimos ";
          this.lblUserAccount.Text = systemUser1.v_FirstName;
          this.lblMessage.Text = "No hemos podido completar tu registro, aparentemente el correo proporcionado no es válido y el <br> Email que te sería enviado ha sido rechazado. Por favor vuelve a intentar tu registro <br> ingresando un correo valido.";
          new SystemUserManagementBL().DeleteObject(systemUser1.i_SystemUserId);
          new UserConfigManagementBL().DeleteObject(systemUser1.i_SystemUserId);
        }
        systemUser2 = (SIIV.BE.SystemUser) null;
      }
      else if (systemUser1 != null)
      {
        this.lblTituloPage.Text = "Bienvenido ";
        this.lblUserAccount.Text = systemUser1.v_FirstName;
        this.lblMessage.Text = string.Format((IFormatProvider) CultureInfo.CurrentCulture, this.lblMessage.Text, new object[1]
        {
          (object) systemUser1.v_Email
        });
        systemUser2 = (SIIV.BE.SystemUser) null;
      }
      else
      {
        this.lblTituloPage.Text = "Registro Incorrecto!!";
        this.lblUserAccount.Text = "";
        this.lblMessage.Text = "El vinculo no es valido o ya caducó.";
        this.lblfooter.Text = "Disculpe los inconvenientes.";
        this.lblSign.Text = "Asociación Automotriz del Perú.";
      }
    }
  }
}
