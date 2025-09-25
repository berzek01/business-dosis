// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.ClaimBatchNoAgree
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Claim.BL;
using SIIV.Common.Resource;
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
  public class ClaimBatchNoAgree : Page
  {
    private static int intClaimTypeId;
    protected HtmlForm form2;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected Label lblTitleField;
    protected Label lblMessage;
    protected BatchNoAgree BatchNoAgree1;
    protected Label Label1;
    protected TextBox txtComments;
    protected HtmlTableRow trButton2;
    protected Button wibSend;
    protected Button wibQuit;
    protected Label lblMessageClaim;

    private void LoadClaimFields()
    {
      try
      {
        ClaimField pobjBE = new ClaimField();
        pobjBE.i_CompanyId = new int?(2);
        pobjBE.i_ClaimTypeId = new int?(ClaimBatchNoAgree.intClaimTypeId);
        if (new ClaimFieldManagementBL().Read(ref pobjBE) != 1)
          return;
        this.lblTitleField.Text = pobjBE.v_TitleField;
        this.BatchNoAgree1.SetLabels(pobjBE.v_InputFields);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SendData()
    {
      try
      {
        RequirementClaim pobjBE = new RequirementClaim();
        this.ValidateData();
        int num = this.Session["SystemUser"] != null ? ((SystemUser) this.Session["SystemUser"]).i_SystemUserId : throw new HandledException(3, "Error de Sesión", "Error de Sesion 'SystemUser' en 'ClaimBatchNoAgree.aspx'");
        DateTime now = DateTime.Now;
        pobjBE.i_RequirementClaimId = 0;
        pobjBE.i_ClaimTypeId = new int?(ClaimBatchNoAgree.intClaimTypeId);
        pobjBE.v_ClaimDate = now.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        pobjBE.i_ClaimMotiveId = new int?(this.BatchNoAgree1.i_ClaimMotiveId);
        pobjBE.v_ListRequirementId = "";
        pobjBE.v_Requester = "";
        pobjBE.v_RequestValues = this.BatchNoAgree1.GetTexts();
        pobjBE.v_ReadValues = "";
        pobjBE.v_InputValues = "";
        pobjBE.v_Comments = this.txtComments.Text;
        pobjBE.i_AssignedUserId = new int?(0);
        pobjBE.i_Priority = new int?(2);
        pobjBE.i_Status = new int?(1);
        pobjBE.i_InsertUserId = new int?(num);
        pobjBE.i_BatchId = this.BatchNoAgree1.i_BatchId;
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
        {
          Timeout = new TimeSpan(1, 1, 1)
        }))
        {
          new RequirementClaimManagementBL().InsertClaim(ref pobjBE);
          this.wibSend.Enabled = false;
          this.ClearControls();
          transactionScope.Complete();
        }
        this.Session["PopupReturn"] = (object) "2";
        throw new HandledException(2, "El Reclamo se registro satisfactoriamente.");
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

    private void ValidateData()
    {
      if (this.BatchNoAgree1.i_ClaimMotiveId <= 0)
        throw new HandledException(1, "Debe seleccionar el motivo del reclamo del lote.");
    }

    private void ClearControls()
    {
      this.BatchNoAgree1.ClearControls();
      this.txtComments.Text = "";
    }

    public void SetReadOnly(bool bolStatus)
    {
      this.wibSend.Visible = !bolStatus;
      this.lblMessage.Visible = !bolStatus;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.Session["PopupReturn"] = (object) "0";
        ClaimBatchNoAgree.intClaimTypeId = 4;
        this.LoadClaimFields();
        this.lblMessage.Text = Constants.CLAIM_UserMessageProductNoAgree;
        if (this.Request.QueryString["bn"] != null)
          this.BatchNoAgree1.SetTexts(this.Request.QueryString["bn"]);
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void btnSend_Click(object sender, EventArgs e) => this.SendData();

    protected void btnQuit_Click(object sender, EventArgs e)
    {
      string script = "PopupClose();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }
  }
}
