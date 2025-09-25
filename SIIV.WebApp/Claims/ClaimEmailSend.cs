// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.ClaimEmailSend
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit.HTMLEditor;
using SIIV.BE;
using SIIV.Claim.BL;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims
{
  public class ClaimEmailSend : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtSubject;
    protected TextBox txtEmail;
    protected Editor WebHtmlEditor1;
    protected TextBox txtComments;
    protected Button wibSend;
    protected Button winCancel;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      if (this.Request.QueryString["rcid"] != null)
        this.ViewState["i_requirementclaimid"] = (object) this.Request.QueryString["rcid"].ToString((IFormatProvider) CultureInfo.CurrentCulture);
      if (this.Request.QueryString["st"] != null)
        this.ViewState["claimstatus"] = (object) this.Request.QueryString["st"].ToString((IFormatProvider) CultureInfo.CurrentCulture);
      this.WebHtmlEditor1.Content = "Escribir aquí el cuerpo del mensaje.";
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
      if (!this.ValidateControls())
        return;
      int num1 = 0;
      if (this.Session["SystemUser"] != null)
        num1 = ((SystemUser) this.Session["SystemUser"]).i_SystemUserId;
      ClaimTracing pobjBE = new ClaimTracing();
      pobjBE.i_RequirementClaimId = Convert.ToInt32(this.ViewState["i_requirementclaimid"], (IFormatProvider) CultureInfo.CurrentCulture);
      pobjBE.i_ActionId = new int?(6);
      pobjBE.i_InsertUserId = new int?(num1);
      pobjBE.d_InsertDate = new DateTime?(DateTime.Now);
      pobjBE.i_Status = new int?(Convert.ToInt32(this.ViewState["claimstatus"], (IFormatProvider) CultureInfo.CurrentCulture));
      pobjBE.v_Comments = this.txtComments.Text.TrimEnd();
      int num2 = 0;
      using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
      {
        Timeout = new TimeSpan(1, 1, 1)
      }))
      {
        try
        {
          RequirementClaim pobjRC = new RequirementClaim();
          num2 = new RequirementClaimManagementBL().TracingClaim(pobjBE, ref pobjRC);
          transactionScope.Complete();
        }
        catch (Exception ex)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Error, Constants.REQUIREMENT_ERROR_GENERICO + ex.Message);
          return;
        }
      }
      if (num2 > 0)
      {
        if (this.SendEmail())
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Se encontró un problema en el envío del Email");
        this.txtSubject.Text = "";
        this.WebHtmlEditor1.Content = "";
        this.txtEmail.Text = "";
        this.txtComments.Text = "";
        this.wibSend.Enabled = false;
        Message.SetMessage(this.lblMessage, enmMessageType.Success, "Se registró correctamente el seguimiento del reclamo");
      }
      else
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Se encontró un problema en el registro del seguimiento");
    }

    protected void btnCancel_Click(object sender, EventArgs e) => this.PopupClose();

    private bool ValidateControls()
    {
      if (this.txtSubject.Text.Length == 0)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Se debe ingresar el asunto del correo");
        return false;
      }
      if (this.WebHtmlEditor1.Content.Length == 0)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Se debe ingresar el cuerpo del correo");
        return false;
      }
      if (this.txtEmail.Text.Length == 0)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Se debe ingresar el Email");
        return false;
      }
      if (!this.ValidarEmail(this.txtEmail.Text))
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Se debe ingresar un Email válido");
        return false;
      }
      this.lblMessage.Visible = false;
      return true;
    }

    private bool ValidarEmail(string text)
    {
      return Regex.IsMatch(text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
    }

    public bool SendEmail()
    {
      string text1 = this.txtEmail.Text;
      string text2 = this.txtSubject.Text;
      string content = this.WebHtmlEditor1.Content;
      Email email = new Email();
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.SMTPServerConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string pstrSMTPServer = dataTable.Rows[0]["v_Value"].ToString();
      int pintSMTPPort = int.Parse(dataTable.Rows[1]["v_Value"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
      string pstrSMTPUserName = dataTable.Rows[2]["v_Value"].ToString();
      string pstrSMTPPassword = dataTable.Rows[3]["v_Value"].ToString();
      string pstrEmailFrom = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.ClaimCustomEmailConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      }).Rows[2]["v_Value"].ToString();
      return Email.SendEmail(pstrSMTPUserName, pstrSMTPPassword, text2, content, text1, pstrSMTPServer, pintSMTPPort, pstrEmailFrom, true);
    }

    private void PopupClose()
    {
      string script = "SendInfoPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }
  }
}
