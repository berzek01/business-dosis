// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.ClaimProductNoAgree
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Claim.BL;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using SIIV.WebApp.Claims.CustomControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims
{
  public class ClaimProductNoAgree : Page
  {
    private static int intClaimTypeId;
    protected HtmlForm form2;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected Label lblTitleField;
    protected Label lblMessage;
    protected ProductNoAgree ProductNoAgree1;
    protected RequesterData RequesterData1;
    protected Label Label1;
    protected TextBox txtComments;
    protected HtmlTableRow trButton2;
    protected Button wibSend;
    protected Button wibCancel;
    protected Label lblMessageClaim;

    public bool NotValideProduct
    {
      get
      {
        return Convert.ToBoolean(this.ViewState[nameof (NotValideProduct)], (IFormatProvider) CultureInfo.CurrentCulture);
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.Session["PopupReturn"] = (object) "0";
        ClaimProductNoAgree.intClaimTypeId = 5;
        this.lblMessage.Text = Constants.CLAIM_UserMessageProductNoAgree;
        if (this.Request.QueryString["pn"] != null)
          this.ProductNoAgree1.SetTexts(this.Request.QueryString["pn"]);
        if (this.Request.QueryString["rq"] != null)
          this.RequesterData1.SetTexts(this.Request.QueryString["rq"]);
        if (this.Request.QueryString["ro"] != null && Convert.ToInt32(this.Request.QueryString["ro"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32((object) enmStatus.ACTIVE))
          this.SetReadOnly(true);
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessageClaim, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageClaim, new HandledException(-100, ex));
      }
    }

    protected void wibSend_Click(object sender, EventArgs e) => this.SendData();

    protected void wibCancel_Click(object sender, EventArgs e) => this.SendInfoProductPopupClose();

    public void SetReadOnly(bool bolStatus) => this.ProductNoAgree1.SetReadOnly(bolStatus);

    private void SendData()
    {
      try
      {
        RequirementClaim pobjBE = new RequirementClaim();
        this.ValidateData();
        string empty = string.Empty;
        string[] strArray = (string[]) null;
        if (this.Request.QueryString["pn"] != null)
          strArray = this.Request.QueryString["pn"].Split('|');
        new BatchReceptionManagementBL().BatchReceptionDetailUpdateState(int.Parse(strArray[0], (IFormatProvider) CultureInfo.CurrentCulture), 5, 1, 3);
        DateTime now = DateTime.Now;
        pobjBE.i_RequirementClaimId = 0;
        pobjBE.i_ClaimTypeId = new int?(ClaimProductNoAgree.intClaimTypeId);
        pobjBE.i_RequirementPlateRefId = new int?(this.ProductNoAgree1.i_RequirementPlateId);
        pobjBE.v_ClaimDate = now.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        pobjBE.v_ListRequirementId = "";
        pobjBE.v_Requester = this.RequesterData1.GetTexts();
        pobjBE.v_RequestValues = this.ProductNoAgree1.GetTexts();
        pobjBE.v_ReadValues = "";
        pobjBE.v_InputValues = "";
        pobjBE.v_Comments = this.txtComments.Text;
        pobjBE.i_AssignedUserId = new int?(0);
        pobjBE.i_Priority = new int?(2);
        pobjBE.i_Status = new int?(1);
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "Error de Sesión", "Error de Sesion 'SystemUser' en 'ClaimProductNoAgree.aspx'");
        pobjBE.i_InsertUserId = new int?(systemUser.i_SystemUserId);
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
        {
          Timeout = new TimeSpan(1, 1, 1)
        }))
        {
          new RequirementClaimManagementBL().InsertClaim(ref pobjBE);
          this.Session["claimcodE"] = (object) pobjBE.v_ClaimCode;
          this.wibSend.Enabled = false;
          Message.SetMessage(this.lblMessageClaim, enmMessageType.Success, "El Reclamo se registró satisfactoriamente." + this.SendEMail().ToString());
          this.ClearControls();
          transactionScope.Complete();
        }
        this.Session["PopupReturn"] = (object) "1";
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessageClaim, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageClaim, new HandledException(-100, ex));
      }
    }

    private void ClearControls()
    {
      this.ProductNoAgree1.ClearControls();
      this.RequesterData1.ClearControls();
      this.txtComments.Text = "";
    }

    public string SendEMail()
    {
      try
      {
        string postrMessage = "";
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
        string str1 = dataTable1.Rows[2]["v_Value"].ToString();
        string pstrSMTPPassword = dataTable1.Rows[3]["v_Value"].ToString();
        bool boolean = Convert.ToBoolean(dataTable1.Rows[4]["v_Value"], (IFormatProvider) CultureInfo.CurrentCulture);
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        DataTable dataTable2 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.DissatisfiedProduct.ToString(),
          (object) "",
          (object) "1",
          (object) "1"
        });
        string str2 = this.Session["claimcodE"].ToString();
        string pstrEmailSubject = dataTable2.Rows[0]["v_Value"].ToString();
        string str3 = this.txtComments.Text.ToString();
        string pstrEmailBody = string.Format(dataTable2.Rows[1]["v_Value"].ToString(), (object) str2, (object) systemUser.v_FirstName, (object) str3);
        List<string> stringList1 = new List<string>();
        List<string> pstrEmailCC = new List<string>();
        DataTable dataTable3;
        if (systemUser.i_LocationId == 14)
          dataTable3 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
          {
            (object) SystemParameterGroups.ConfigurationRegular.ToString((IFormatProvider) CultureInfo.CurrentCulture),
            (object) "5",
            (object) "1",
            (object) "1"
          });
        else
          dataTable3 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
          {
            (object) SystemParameterGroups.ConfigurationRegular.ToString((IFormatProvider) CultureInfo.CurrentCulture),
            (object) "3",
            (object) "1",
            (object) "1"
          });
        string str4 = dataTable3.Rows[0]["v_Value"].ToString();
        List<string> stringList2 = new List<string>();
        stringList2.Add(str4);
        return Email.SendEmail(str1, pstrSMTPPassword, pstrEmailSubject, pstrEmailBody, stringList2, pstrEmailCC, pstrSMTPServer, pintSMTPPort, str1, boolean, stringList2, ref postrMessage) ? "</br> correo enviado satisfactoriamente." : "</br>(" + new HandledException(8, postrMessage, "Error el class Email.SendEmail").ErrorId.ToString() + ") Ocurrio un problema al enviar correo";
      }
      catch (Exception ex)
      {
        return "</br>(" + new HandledException(8, ex).ErrorId.ToString() + ") Ocurrio un problema al enviar correo";
      }
    }

    private void SendInfoProductPopupClose()
    {
      string script = "PopupClose();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void ValidateData()
    {
      try
      {
        if (this.RequesterData1.DocumentType == "4")
        {
          if (this.RequesterData1.DocumentNumber.Length < 10 || !Format.ValidateRUCstructure(this.RequesterData1.DocumentNumber))
            throw new HandledException(1, "El N° de RUC ingresado no es válido.");
        }
        else if (this.RequesterData1.DocumentType == "1" && (this.RequesterData1.DocumentNumber.Length < 8 || this.RequesterData1.DocumentNumber.Length > 8))
          throw new HandledException(1, "El N° de DNI ingresado no es válido.");
        if (this.RequesterData1.TelephoneNumber.Length == 0 && this.RequesterData1.Email.Length == 0)
          throw new HandledException(1, "Debe especificar un Email o Número de Teléfono.");
        if (this.RequesterData1.TelephoneNumber.Length > 0 && this.RequesterData1.TelephoneNumber.Length > 0 && this.RequesterData1.TelephoneNumber.Length != 9)
          throw new HandledException(1, "ebe Especificar un Número de Teléfono o Celular válido.");
        if (this.ProductNoAgree1.IsEnabledProductNew && (this.ProductNoAgree1.ProductNew == "-1" || this.ProductNoAgree1.ProductNew == ""))
          throw new HandledException(1, "Debe seleccionar el Nuevo Producto.");
        if (this.txtComments.Text == "")
          throw new HandledException(1, "Debe ingresar un Comentario.");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
