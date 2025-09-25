// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Account.RegisterConfirm
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
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Account
{
  public class RegisterConfirm : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected Label lblTituloPage;
    protected Label lblUserAccount;
    protected Label lblMessage;
    protected Label lblFooter;
    protected Label lblSign;
    protected HyperLink hlnkRedirect;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Request.QueryString["p"] == null)
      {
        this.lblTituloPage.Text = "Dirección Url Incorrecta !!";
        this.lblUserAccount.Text = "";
        this.lblMessage.Text = "Lo sentimos no hemos logrado activar tu cuenta. El dirección Url no es valida.<br>Verifica el Email que se te envio al crear tu cuenta de usuario";
        this.lblFooter.Text = "Disculpe los inconvenientes.";
        this.lblSign.Text = "Asociación Automotriz del Perú.";
      }
      else
      {
        string s = this.Request.QueryString["p"];
        DataTable dataTable1 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.Cryptography.ToString((IFormatProvider) CultureInfo.CurrentCulture),
          (object) "",
          (object) "1",
          (object) "1"
        });
        string str = new Rijndael(dataTable1.Rows[0]["v_Value"].ToString(), dataTable1.Rows[1]["v_Value"].ToString()).Decrypt(s);
        DataTable dataTable2 = new SystemUserQueriesBL().SystemUserRead(Convert.ToInt32(str, (IFormatProvider) CultureInfo.CurrentCulture));
        if (dataTable2.Rows.Count > 0)
        {
          if (dataTable2.Rows[0]["i_Status"].ToString() == "1")
          {
            this.lblTituloPage.Text = "Hola ";
            this.lblUserAccount.Text = dataTable2.Rows[0]["v_FirstName"].ToString();
            this.lblMessage.Text = "Tu cuenta de usuario ya ha sido activada.";
            this.lblFooter.Text = "Disculpe los inconvenientes.";
            this.lblSign.Text = "Asociación Automotriz del Perú.";
          }
          else
          {
            this.lblTituloPage.Text = "Activación de cuenta satisfactoria !!!!";
            this.lblUserAccount.Text = "";
            this.lblMessage.Text = "Hola " + dataTable2.Rows[0]["v_FirstName"].ToString() + " Tu cuenta de usuario " + dataTable2.Rows[0]["v_Alias"].ToString() + " ha sido activada.";
            this.lblMessage.Text += "<br>A partir de ahora puedes hacer uso de tu cuenta ingresando a nuestro sitio desde <a href='http://www.placas.pe'>este enlace</a>";
            this.lblFooter.Text = "Gracias.";
            this.lblSign.Text = "Asociación Automotriz del Perú.";
            if (new SystemUserManagementBL().Activate(new SIIV.BE.SystemUser()
            {
              i_SystemUserId = Convert.ToInt32(str, (IFormatProvider) CultureInfo.CurrentCulture),
              i_Status = new int?(1)
            }) == -1)
            {
              this.lblTituloPage.Text = "Problemas al enviar solicitud !!";
              this.lblUserAccount.Text = "";
              this.lblMessage.Text = "Estamos Experimentando problemas para activar tu cuenta.<br>Por favor intentar ne unos momentos.";
              this.lblFooter.Text = "Disculpe los inconvenientes.";
              this.lblSign.Text = "Asociación Automotriz del Perú.";
            }
          }
        }
        else
        {
          this.lblTituloPage.Text = "Validación Incorrecta !!";
          this.lblUserAccount.Text = "";
          this.lblMessage.Text = "Lo sentimos tu cuenta de usuario ha sido dada de baja.  Ha superado el tiempo maximo de espera para su confirmación.";
          this.lblFooter.Text = "Disculpe los inconvenientes.";
          this.lblSign.Text = "Asociación Automotriz del Perú.";
        }
      }
    }
  }
}
