// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.ClaimDataNoAgree
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Claim.BL;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using SIIV.WebApp.Claims.CustomControls;
using System;
using System.Data;
using System.Globalization;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims
{
  public class ClaimDataNoAgree : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form2;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected Label lblTitleField;
    protected Label lblMessage;
    protected HtmlTableRow trButton1;
    protected Button btnQuit;
    protected Button btnContinue;
    protected RequestData RequestData1;
    protected DataNoAgree DataNoAgree1;
    protected RequesterData RequesterData1;
    protected HtmlTable tblPetitionary;
    protected Label lblPetitionary;
    protected TextBox txtPetitionary;
    protected HtmlTableRow trButton2;
    protected Button wibSend;
    protected Button wibQuit;
    protected Label lblMessageClaim;

    private void LoadClaimFields()
    {
      ClaimFieldManagementBL fieldManagementBl = new ClaimFieldManagementBL();
      ClaimField pobjBE = new ClaimField();
      pobjBE.i_CompanyId = new int?(2);
      pobjBE.i_ClaimTypeId = new int?(3);
      if (fieldManagementBl.Read(ref pobjBE) != 1)
        return;
      this.lblTitleField.Text = pobjBE.v_TitleField;
      this.RequestData1.SetLabels(pobjBE.v_RequestFields);
      this.DataNoAgree1.SetLabels(pobjBE.v_InputFields);
      this.RequesterData1.SetLabels(pobjBE.v_Requester);
    }

    private void SendData()
    {
      if (!this.ValidateData())
        return;
      this.lblMessageClaim.Visible = false;
      RequirementClaimManagementBL claimManagementBl = new RequirementClaimManagementBL();
      RequirementClaim pobjBE = new RequirementClaim();
      int num = 0;
      if (this.Session["SystemUser"] != null)
        num = ((SystemUser) this.Session["SystemUser"]).i_SystemUserId;
      DateTime now = DateTime.Now;
      pobjBE.i_RequirementClaimId = 0;
      pobjBE.i_ClaimTypeId = new int?(3);
      pobjBE.v_ClaimDate = now.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      pobjBE.v_ListRequirementId = "";
      pobjBE.v_Requester = this.RequesterData1.GetTexts();
      pobjBE.v_RequestValues = this.RequestData1.GetTexts();
      pobjBE.v_ReadValues = this.DataNoAgree1.GetOldTexts();
      pobjBE.v_InputValues = this.DataNoAgree1.GetNewTexts();
      pobjBE.v_Comments = this.txtPetitionary.Text;
      pobjBE.i_AssignedUserId = new int?(0);
      pobjBE.i_Priority = new int?(2);
      pobjBE.i_Status = new int?(1);
      pobjBE.i_VehicleId = Convert.ToInt32(this.Session["i_VehicleId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
      pobjBE.i_InsertUserId = new int?(num);
      using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
      {
        Timeout = new TimeSpan(0, 1, 0)
      }))
      {
        try
        {
          if (new RequirementClaimManagementBL().InsertClaim(ref pobjBE) > 0)
          {
            this.wibSend.Enabled = false;
            Message.SetMessage(this.lblMessage, enmMessageType.Success, "</br>***El Reclamo se registro satisfactoriamente.</br>El numero de reclamo es " + pobjBE.v_ClaimCode);
            this.ClearControls();
            this.Session["ClaimRegistered"] = (object) 1;
          }
          else
            Message.SetMessage(this.lblMessageClaim, enmMessageType.Warning, "Se encontró un problema en el registro del reclamo");
          transactionScope.Complete();
        }
        catch (Exception ex)
        {
          Message.SetMessage(this.lblMessageClaim, enmMessageType.Error, ex.Message);
        }
      }
    }

    private bool ValidateData()
    {
      this.lblMessageClaim.Visible = false;
      string pstrMessage1 = this.DataNoAgree1.ValidateDataEmpty();
      if (pstrMessage1.Length > 0)
      {
        this.lblMessageClaim.Visible = true;
        Message.SetMessage(this.lblMessageClaim, enmMessageType.Warning, pstrMessage1);
        return false;
      }
      string pstrMessage2 = this.DataNoAgree1.ValidateDataEqual();
      if (pstrMessage2.Length > 0)
      {
        this.lblMessageClaim.Visible = true;
        Message.SetMessage(this.lblMessageClaim, enmMessageType.Warning, pstrMessage2);
        return false;
      }
      if (!this.DataNoAgree1.ValidateCheckSome())
      {
        this.lblMessageClaim.Visible = true;
        Message.SetMessage(this.lblMessageClaim, enmMessageType.Warning, "No se puede registrar el reclamo porque se debe marcar algun dato para cambio");
        return false;
      }
      if (this.RequesterData1.DocumentType == "4")
      {
        if (this.RequesterData1.DocumentNumber.Length < 10 || !Format.ValidateRUCstructure(this.RequesterData1.DocumentNumber))
        {
          this.lblMessageClaim.Visible = true;
          Message.SetMessage(this.lblMessageClaim, enmMessageType.Warning, "El N° de RUC ingresado no es valido");
          return false;
        }
      }
      else if (this.RequesterData1.DocumentType == "1" && (this.RequesterData1.DocumentNumber.Length < 8 || this.RequesterData1.DocumentNumber.Length > 8))
      {
        this.lblMessageClaim.Visible = true;
        Message.SetMessage(this.lblMessageClaim, enmMessageType.Warning, "El N° de DNI ingresado no es valido");
        return false;
      }
      if (this.RequesterData1.TelephoneNumber.Length == 0 && this.RequesterData1.Email.Length == 0)
      {
        this.lblMessageClaim.Visible = true;
        Message.SetMessage(this.lblMessageClaim, enmMessageType.Warning, "Debe Especificar un Email o Número de Teléfono");
        return false;
      }
      if (this.RequesterData1.TelephoneNumber.Length <= 0 || this.RequesterData1.TelephoneNumber.Length <= 0 || this.RequesterData1.TelephoneNumber.Length == 9)
        return true;
      this.lblMessageClaim.Visible = true;
      Message.SetMessage(this.lblMessageClaim, enmMessageType.Warning, "Debe Especificar un Número de Teléfono o Celular Valido");
      return false;
    }

    private void ClearControls()
    {
      this.RequestData1.Visible = false;
      this.RequesterData1.Visible = false;
      this.DataNoAgree1.Visible = false;
      this.tblPetitionary.Visible = false;
    }

    private void SetControls(bool pb_Habilitar)
    {
      this.lblMessage.Text = "";
      this.RequestData1.Visible = pb_Habilitar;
      this.RequesterData1.Visible = pb_Habilitar;
      this.DataNoAgree1.Visible = pb_Habilitar;
      this.trButton1.Visible = !pb_Habilitar;
      this.trButton2.Visible = pb_Habilitar;
    }

    public void SetReadOnly(bool bolStatus)
    {
      this.RequesterData1.SetReadOnly(bolStatus);
      this.wibSend.Visible = !bolStatus;
      this.lblMessage.Visible = !bolStatus;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadClaimFields();
      this.SetControls(false);
      this.lblMessage.Text = Constants.CLAIM_UserMessageDataNoAgree;
      if (this.Request.QueryString["rv"] != null)
        this.RequestData1.SetTexts(this.Request.QueryString["rv"]);
      if (this.Request.QueryString["rq"] != null)
        this.RequesterData1.SetTexts(this.Request.QueryString["rq"]);
      if (this.Request.QueryString["od"] != null)
      {
        string str = this.ConcatenacionOwners(this.Request.QueryString["od"]);
        this.DataNoAgree1.SetOldTexts(str);
        this.DataNoAgree1.GenerateDataTable(str);
        this.SetControls(true);
      }
      if (this.Request.QueryString["nd"] != null)
      {
        this.DataNoAgree1.SetNewTexts(this.Request.QueryString["nd"], "");
        this.SetControls(true);
      }
      if (this.Request.QueryString["lv"] != null)
      {
        this.DataNoAgree1.SetNewIdList(this.Request.QueryString["lv"]);
        this.SetControls(true);
      }
      if (this.Request.QueryString["l"] != null)
      {
        this.DataNoAgree1.SetNewTextsList(this.Request.QueryString["l"]);
        this.SetControls(true);
      }
      if (this.Request.QueryString["lg"] != null)
      {
        this.DataNoAgree1.SetNewListGroup(this.Request.QueryString["lg"]);
        this.SetControls(true);
      }
      if (this.Request.QueryString["ro"] != null && Convert.ToInt32(this.Request.QueryString["ro"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32((object) enmStatus.ACTIVE))
        this.SetReadOnly(true);
    }

    private string ConcatenacionOwners(string strValues)
    {
      string[] strArray = strValues.Split('|');
      string str1 = strArray[0];
      string str2 = strArray[1];
      string str3 = strArray[2];
      string str4 = strArray[5];
      DataTable ownersByIdSunarp = new RequirementQueriesBL().GetOwnersByIdSunarp(Convert.ToInt32(strArray[3], (IFormatProvider) CultureInfo.CurrentCulture));
      string str5 = "";
      string str6 = "";
      string str7 = "";
      string str8 = "";
      string str9 = "";
      for (int index = 0; index < ownersByIdSunarp.Rows.Count; ++index)
      {
        str5 = str5 + "/" + ownersByIdSunarp.Rows[index]["CompleteName"].ToString();
        str6 = str6 + "/" + ownersByIdSunarp.Rows[index]["v_DocumentNumber"].ToString();
        str7 = str7 + "/" + ownersByIdSunarp.Rows[index]["v_DocumentTypeId"].ToString();
        str8 = str8 + "/" + ownersByIdSunarp.Rows[index]["i_Item"].ToString();
        str9 = str9 + "/" + ownersByIdSunarp.Rows[index]["v_Value"].ToString();
      }
      string str10 = str5.Substring(1, str5.Length - 1);
      string str11 = str6.Substring(1, str6.Length - 1);
      string str12 = str7.Substring(1, str7.Length - 1);
      string str13 = str8.Substring(1, str8.Length - 1);
      string str14 = str9.Substring(1, str9.Length - 1);
      return str1 + "|" + str2 + "|" + str3 + "|" + str10 + "|" + str11 + "|" + str4 + "|" + str12 + "|" + str13 + "|" + str14;
    }

    protected void btnContinue_Click(object sender, EventArgs e) => this.SetControls(true);

    protected void btnSend_Click(object sender, EventArgs e) => this.SendData();

    protected void btnQuit_Click(object sender, EventArgs e) => this.SendInfoProductPopupClose();

    private void SendInfoProductPopupClose()
    {
      string script = "SendInfoProductPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }
  }
}
