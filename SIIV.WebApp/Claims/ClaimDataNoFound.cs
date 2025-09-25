// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.ClaimDataNoFound
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Claim.BL;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.SystemParameter.BL;
using SIIV.WebApp.Claims.CustomControls;
using System;
using System.Globalization;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims
{
  public class ClaimDataNoFound : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected Label lblTitleField;
    protected Label lblMessage;
    protected RequestData RequestData1;
    protected RequesterData RequesterData1;
    protected Button wibSend;
    protected Button wibSalir;
    protected Label lblMessageClaim;

    private void LoadClaimFields()
    {
      ClaimFieldManagementBL fieldManagementBl = new ClaimFieldManagementBL();
      ClaimField pobjBE = new ClaimField();
      pobjBE.i_CompanyId = new int?(2);
      pobjBE.i_ClaimTypeId = new int?(2);
      if (fieldManagementBl.Read(ref pobjBE) != 1)
        return;
      this.lblTitleField.Text = pobjBE.v_TitleField;
      this.RequestData1.SetLabels(pobjBE.v_RequestFields);
      this.RequesterData1.SetLabels(pobjBE.v_Requester);
    }

    private void SendData()
    {
      if (!this.ValidateData())
        return;
      this.lblMessageClaim.Visible = false;
      RequirementClaim pobjBE = new RequirementClaim();
      DateTime now = DateTime.Now;
      int num1 = 0;
      if (this.Session["SystemUser"] != null)
        num1 = ((SystemUser) this.Session["SystemUser"]).i_SystemUserId;
      pobjBE.i_RequirementClaimId = 0;
      pobjBE.i_ClaimTypeId = new int?(2);
      pobjBE.v_ClaimDate = now.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      pobjBE.v_ListRequirementId = "";
      pobjBE.v_Requester = this.RequesterData1.GetTexts();
      pobjBE.v_RequestValues = this.RequestData1.GetTexts();
      pobjBE.v_InputValues = "";
      pobjBE.v_ReadValues = "";
      pobjBE.v_Comments = "";
      pobjBE.i_AssignedUserId = new int?(0);
      pobjBE.i_Priority = new int?(2);
      pobjBE.i_Status = new int?(1);
      pobjBE.i_InsertUserId = new int?(num1);
      int num2 = 0;
      using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
      {
        Timeout = new TimeSpan(1, 1, 1)
      }))
      {
        try
        {
          num2 = new RequirementClaimManagementBL().InsertClaim(ref pobjBE);
          transactionScope.Complete();
        }
        catch (Exception ex)
        {
          this.lblMessageClaim.Visible = true;
          Message.SetMessage(this.lblMessageClaim, enmMessageType.Error, ex.Message);
        }
      }
      if (num2 > 0)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Success, "</br>***El Reclamo se registro satisfactoriamente.</br>El numero de reclamo es " + pobjBE.v_ClaimCode);
        this.ClearControls();
        this.wibSend.Enabled = false;
      }
      else
      {
        this.lblMessageClaim.Visible = true;
        Message.SetMessage(this.lblMessageClaim, enmMessageType.Warning, "Se encontró un problema en el registro del reclamo");
      }
    }

    private bool ValidateData()
    {
      this.lblMessageClaim.Visible = false;
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
      this.RequestData1.ClearControls();
      this.RequesterData1.ClearControls();
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
      this.lblMessage.Text = Constants.CLAIM_UserMessageProductNoAgree;
      if (this.Request.QueryString["rv"] != null)
        this.RequestData1.SetTexts(this.Request.QueryString["rv"]);
      if (this.Request.QueryString["rq"] != null)
        this.RequesterData1.SetTexts(this.Request.QueryString["rq"]);
      if (this.Request.QueryString["ro"] != null && Convert.ToInt32(this.Request.QueryString["ro"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32((object) enmStatus.ACTIVE))
        this.SetReadOnly(true);
    }

    protected void btnSend_Click(object sender, EventArgs e) => this.SendData();

    private void SendInfoProductPopupClose()
    {
      string script = "SendInfoProductPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void btnSalir_Click(object sender, EventArgs e) => this.SendInfoProductPopupClose();
  }
}
