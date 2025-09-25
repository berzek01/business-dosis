// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.ClaimApprove
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Claim.BL;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims
{
  public class ClaimApprove : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected Label lblTitle;
    protected TextBox txtComments;
    protected Button wibApprove;
    protected Button wibQuit;
    protected CheckBox chkSendEmail;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      if (this.Request.QueryString["flag"] != null)
      {
        short int16 = Convert.ToInt16(this.Request.QueryString["flag"], (IFormatProvider) CultureInfo.CurrentCulture);
        Label lblTitle = this.lblTitle;
        string str1;
        switch (int16)
        {
          case 1:
            str1 = "Aprobación Reclamo";
            break;
          case 2:
            str1 = "Cierre Reclamo";
            break;
          case 3:
            str1 = "Rechazar Reclamo";
            break;
          default:
            str1 = "Levantar Reclamo";
            break;
        }
        lblTitle.Text = str1;
        Button wibApprove = this.wibApprove;
        string str2;
        switch (int16)
        {
          case 1:
            str2 = "Aprobar";
            break;
          case 2:
            str2 = "Cerrar";
            break;
          case 3:
            str2 = "Rechazar";
            break;
          default:
            str2 = "Levantar";
            break;
        }
        wibApprove.Text = str2;
        this.ViewState["flag"] = (object) int16;
        if (Convert.ToInt32(this.Request.QueryString["ctm"].ToString((IFormatProvider) CultureInfo.CurrentCulture)) == 13)
          this.chkSendEmail.Visible = false;
        else
          this.chkSendEmail.Visible = this.Session["pObjRequirementClaim"] != null;
      }
      if (this.Request.QueryString["rcid"] != null)
        this.ViewState["i_requirementclaimid"] = (object) this.Request.QueryString["rcid"].ToString((IFormatProvider) CultureInfo.CurrentCulture);
      if (this.Request.QueryString["ctm"] != null)
        this.ViewState["i_claimtypeid"] = (object) this.Request.QueryString["ctm"].ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
      int num1 = 0;
      if (this.Session["SystemUser"] != null)
        num1 = ((SystemUser) this.Session["SystemUser"]).i_SystemUserId;
      int int16 = (int) Convert.ToInt16(this.ViewState["flag"], (IFormatProvider) CultureInfo.CurrentCulture);
      int num2;
      switch (int16)
      {
        case 1:
          num2 = 2;
          break;
        case 2:
          num2 = 5;
          break;
        case 3:
          num2 = 3;
          break;
        default:
          num2 = 6;
          break;
      }
      int num3 = num2;
      int num4;
      switch (int16)
      {
        case 1:
          num4 = 4;
          break;
        case 2:
          num4 = 5;
          break;
        default:
          num4 = 3;
          break;
      }
      int num5 = num4;
      ClaimTracing pobjBE = new ClaimTracing();
      pobjBE.i_RequirementClaimId = Convert.ToInt32(this.ViewState["i_requirementclaimid"], (IFormatProvider) CultureInfo.CurrentCulture);
      pobjBE.i_ActionId = new int?(num5);
      pobjBE.i_InsertUserId = new int?(num1);
      pobjBE.d_InsertDate = new DateTime?(DateTime.Now);
      pobjBE.i_Status = new int?(num3);
      pobjBE.v_Comments = this.txtComments.Text.TrimEnd();
      RequirementClaim pobjRC = new RequirementClaim();
      pobjRC.i_RequirementClaimId = Convert.ToInt32(this.ViewState["i_requirementclaimid"], (IFormatProvider) CultureInfo.CurrentCulture);
      pobjRC.i_ClaimTypeId = new int?(Convert.ToInt32(this.ViewState["i_claimtypeid"], (IFormatProvider) CultureInfo.CurrentCulture));
      pobjRC.i_Status = new int?(num3);
      pobjRC.d_UpdateDate = new DateTime?(DateTime.Now);
      pobjRC.i_ClaimCostAssignedTo = new int?(-1);
      pobjRC.i_UpdateUserId = new int?(num1);
      int num6 = 0;
      using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
      {
        Timeout = new TimeSpan(0, 1, 0)
      }))
      {
        try
        {
          num6 = new RequirementClaimManagementBL().TracingClaim(pobjBE, ref pobjRC);
          if (this.chkSendEmail.Checked)
            this.SendEmail();
          transactionScope.Complete();
        }
        catch (Exception ex)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Error, Constants.REQUIREMENT_ERROR_GENERICO + ex.Message);
          return;
        }
      }
      if (num6 > 0)
      {
        this.txtComments.Text = "";
        this.wibApprove.Enabled = false;
        if (this.chkSendEmail.Checked)
        {
          if (this.ViewState["v_Email"].ToString() != "")
            Message.SetMessage(this.lblMessage, enmMessageType.Success, "Se registró correctamente el seguimiento del reclamo");
          else
            Message.SetMessage(this.lblMessage, enmMessageType.Success, "Se registró correctamente el seguimiento del reclamo, pero el correo no logro enviarse porque no esta registrado en el reclamo");
        }
        else if (pobjRC.i_ClaimTypeId.GetValueOrDefault() == 13 && pobjRC.i_RequirementStatus > 0)
        {
          if (!this.SendNotificationAccounting(pobjRC))
          {
            this.lblMessage.Visible = true;
            Message.SetMessage(this.lblMessage, enmMessageType.Success, "Se registró correctamente el seguimiento del reclamo, Error al enviar email");
          }
          else
          {
            this.lblMessage.Visible = true;
            Message.SetMessage(this.lblMessage, enmMessageType.Success, "Se registró correctamente el seguimiento del reclamo, se envío mail satisfactoriamente");
          }
        }
        else if (pobjRC.i_IsDelivery == 1)
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Se registró correctamente el seguimiento del reclamo. Recuerde reprogramar la fecha delivery.");
        else
          Message.SetMessage(this.lblMessage, enmMessageType.Success, "Se registró correctamente el seguimiento del reclamo");
      }
      else
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Se encontró un problema en el registro del seguimiento");
    }

    protected void btnCancel_Click(object sender, EventArgs e) => this.PopupClose();

    public bool SendEmail()
    {
      Email email = new Email();
      DataView dataView = new DataView(new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) (SystemParameterGroups.SMTPServerConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ", " + SystemParameterGroups.RequerimentEmailClosing.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
        (object) "",
        (object) "1",
        (object) "1"
      }));
      dataView.RowFilter = "i_GroupId=" + SystemParameterGroups.SMTPServerConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      DataTable table1 = dataView.ToTable();
      string pstrSMTPServer = table1.Rows[0]["v_Value"].ToString();
      int pintSMTPPort = int.Parse(table1.Rows[1]["v_Value"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
      string pstrSMTPUserName = table1.Rows[2]["v_Value"].ToString();
      string pstrSMTPPassword = table1.Rows[3]["v_Value"].ToString();
      string str1 = table1.Rows[4]["v_Value"].ToString();
      dataView.RowFilter = "";
      dataView.RowFilter = "i_GroupId=" + SystemParameterGroups.RequerimentEmailClosing.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      DataTable table2 = dataView.ToTable();
      string pstrEmailSubject = table2.Rows[0]["v_Value"].ToString();
      string str2 = table2.Rows[1]["v_Value"].ToString();
      string pstrEmailFrom = table2.Rows[2]["v_Value"].ToString();
      string str3 = table2.Rows[3]["v_Value"].ToString();
      DataRow dataRow = (DataRow) this.Session["pObjRequirementClaim"];
      string str4 = dataRow["v_Requester"].ToString();
      string newValue1 = dataRow["v_ClaimType"].ToString();
      string newValue2 = dataRow["v_ClaimCode"].ToString();
      string[] strArray;
      if (str4.Length <= 0)
        strArray = new string[0];
      else
        strArray = str4.Split('|');
      string[] source = strArray;
      string newValue3 = ((IEnumerable<string>) source).Count<string>() > 0 ? source[0] : "";
      string pstrEmailTo = ((IEnumerable<string>) source).Count<string>() > 4 ? source[4] : "";
      this.ViewState["v_Email"] = (object) pstrEmailTo;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("<html><body>");
      string str5 = str2.Replace("[v_BeneficiaryName]", newValue3).Replace("[v_ClaimType]", newValue1).Replace("[v_ClaimCode]", newValue2);
      stringBuilder.AppendLine(str5);
      stringBuilder.AppendLine(str3);
      stringBuilder.AppendLine("</body></html>");
      return pstrEmailTo.Length > 0 && Email.SendEmail(pstrSMTPUserName, pstrSMTPPassword, pstrEmailSubject, stringBuilder.ToString(), pstrEmailTo, pstrSMTPServer, pintSMTPPort, pstrEmailFrom, Convert.ToBoolean(str1, (IFormatProvider) CultureInfo.CurrentCulture));
    }

    public bool SendNotificationAccounting(RequirementClaim RequirementClaim)
    {
      Email email = new Email();
      DataView dataView = new DataView(new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) (SystemParameterGroups.SMTPServerConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ", " + SystemParameterGroups.RequerimentEmailClosing.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
        (object) "",
        (object) "1",
        (object) "1"
      }));
      dataView.RowFilter = "i_GroupId=" + SystemParameterGroups.SMTPServerConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      DataTable table1 = dataView.ToTable();
      string pstrSMTPServer = table1.Rows[0]["v_Value"].ToString();
      int pintSMTPPort = int.Parse(table1.Rows[1]["v_Value"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
      string str1 = table1.Rows[2]["v_Value"].ToString();
      string pstrSMTPPassword = table1.Rows[3]["v_Value"].ToString();
      bool boolean = Convert.ToBoolean(table1.Rows[4]["v_Value"], (IFormatProvider) CultureInfo.CurrentCulture);
      dataView.RowFilter = "";
      dataView.RowFilter = "i_GroupId=" + SystemParameterGroups.RequerimentEmailClosing.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      DataTable table2 = dataView.ToTable();
      DataRow dataRow = (DataRow) this.Session["pObjRequirementClaim"];
      string str2 = dataRow["v_RequestValues"].ToString();
      dataRow["v_ClaimType"].ToString();
      string newValue1 = dataRow["v_ClaimCode"].ToString();
      string newValue2 = RequirementClaim.i_RequirementStatus == 1 ? "ACREDITADO" : "POR RECOGER";
      string[] strArray;
      if (str2.Length <= 0)
        strArray = new string[0];
      else
        strArray = str2.Split('|');
      string[] source = strArray;
      string newValue3 = ((IEnumerable<string>) source).Count<string>() > 0 ? source[0] : "";
      string str3 = ((IEnumerable<string>) source).Count<string>() > 0 ? source[11] : "";
      string pstrEmailSubject = str3 == "1" ? table2.Rows[5]["v_Value"].ToString() : table2.Rows[4]["v_Value"].ToString();
      string str4 = str3 == "1" ? table2.Rows[7]["v_Value"].ToString() : table2.Rows[6]["v_Value"].ToString();
      table2.Rows[2]["v_Value"].ToString();
      string str5 = table2.Rows[3]["v_Value"].ToString();
      string str6 = table2.Rows[8]["v_Value"].ToString();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("<html><body>");
      string str7 = str4.Replace("[v_PaymentCode]", newValue3).Replace("[v_ClaimCode]", newValue1);
      if (str3 == "1")
        str7 = str7.Replace("[v_status]", newValue2);
      stringBuilder.AppendLine(str7);
      stringBuilder.AppendLine(str5);
      stringBuilder.AppendLine("</body></html>");
      List<string> pstrEmailTo = new List<string>();
      if (str6 != null)
      {
        string str8 = str6;
        char[] chArray = new char[1]{ '|' };
        foreach (string str9 in str8.Split(chArray))
        {
          if (!string.IsNullOrEmpty(str9))
            pstrEmailTo.Add(str9.ToString());
        }
      }
      stringBuilder.ToString();
      List<string> pstrEmailCC = new List<string>();
      return pstrEmailTo.Count > 0 && Email.SendEmail(str1, pstrSMTPPassword, pstrEmailSubject, stringBuilder.ToString(), pstrEmailTo, pstrEmailCC, pstrSMTPServer, pintSMTPPort, str1, boolean);
    }

    private void PopupClose()
    {
      string script = "SendInfoPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }
  }
}
