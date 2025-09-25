// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.ClaimRegister
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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims
{
  public class ClaimRegister : Page
  {
    private RequestData ucRequestData1;
    private RequestData ucRequestData2;
    private DataNoAgree ucDataNoAgree1;
    private BatchNoAgree ucBatchNoAgree1;
    private ProductNoAgree ucProductNoAgree1;
    private ReimbursementPayment ucReimbursementPayment1;
    private CallCenter ucCallCenter1;
    private RequesterData ucRequesterDataTmp;
    private UseChange ucUseChange1;
    private ChangeDeliveryPlate ucChangeDeliveryPlate1;
    private RemoveMotivation ucRemoveMotivation;
    private EnablePaymentCode ucEnablePaymentCode;
    private RemoveRequirement ucRemoveRequirement;
    private RegulariceMovementForDeliver ucRegulariceMovementForDeliver;
    private RequirementData RequirementData1;
    protected UpdatePanel UpdatePanel1;
    protected DropDownList wddClaimType;
    protected Panel pControls;
    protected HtmlGenericControl spanRequestData1;
    protected HtmlGenericControl spanRequestData2;
    protected HtmlGenericControl spanDataNoAgree1;
    protected HtmlGenericControl spanBatchNoAgree1;
    protected HtmlGenericControl spanProductNoAgree1;
    protected HtmlGenericControl spanReimbursementPayment1;
    protected HtmlGenericControl spanCallCenter1;
    protected HtmlGenericControl spanUseChange1;
    protected HtmlGenericControl spanChangeDeliveryPlate1;
    protected HtmlGenericControl spanRemoveMotivation;
    protected HtmlGenericControl spanEnablePaymentCode;
    protected HtmlGenericControl spanRemoveRequirement;
    protected HtmlGenericControl spanRegulariceMovementForDeliver;
    protected HtmlGenericControl spanRequirementData;
    protected Panel Panel2;
    protected HtmlGenericControl spanRequesterDataTmp;
    protected ClaimData ClaimData1;
    protected Label lblPetitorio;
    protected Label lblPetitorio0;
    protected RadioButtonList rblTypePetitory;
    protected TextBox txtComents;
    protected Label lblmasage;
    protected Label lblMsgError;
    protected Button wibSave;
    protected Button wibCancel;

    protected void Page_PreInit(object sender, EventArgs e)
    {
      try
      {
        this.ucRequestData1 = (RequestData) this.LoadControl("~/Claims/CustomControls/RequestData.ascx");
        this.ucRequestData2 = (RequestData) this.LoadControl("~/Claims/CustomControls/RequestData.ascx");
        this.ucDataNoAgree1 = (DataNoAgree) this.LoadControl("~/Claims/CustomControls/DataNoAgree.ascx");
        this.ucBatchNoAgree1 = (BatchNoAgree) this.LoadControl("~/Claims/CustomControls/BatchNoAgree.ascx");
        this.ucProductNoAgree1 = (ProductNoAgree) this.LoadControl("~/Claims/CustomControls/ProductNoAgree.ascx");
        this.ucRequesterDataTmp = (RequesterData) this.LoadControl("~/Claims/CustomControls/RequesterData.ascx");
        this.ucUseChange1 = (UseChange) this.LoadControl("~/Claims/CustomControls/UseChange.ascx");
        this.ucChangeDeliveryPlate1 = (ChangeDeliveryPlate) this.LoadControl("~/Claims/CustomControls/ChangeDeliveryPlate.ascx");
        this.ucChangeDeliveryPlate1.OnDatosOK += new ChangeDeliveryPlate.DatosOK(this.ucChangeDeliveryPlate1_OnDatosOK);
        this.ucChangeDeliveryPlate1.OnDatosWrong += new ChangeDeliveryPlate.DatosWrong(this.ucChangeDeliveryPlate1_OnDatosWrong);
        this.ucReimbursementPayment1 = (ReimbursementPayment) this.LoadControl("~/Claims/CustomControls/ReimbursementPayment.ascx");
        this.ucReimbursementPayment1.OnDatosOK += new ReimbursementPayment.DatosOK(this.ucReimbursementPayment1_OnDatosOK);
        this.ucReimbursementPayment1.OnDatosWrong += new ReimbursementPayment.DatosWrong(this.ucReimbursementPayment1_OnDatosWrong);
        this.ucCallCenter1 = (CallCenter) this.LoadControl("~/Claims/CustomControls/CallCenter.ascx");
        this.ucCallCenter1.OnDatosOK += new CallCenter.DatosOK(this.ucCallCenter1_OnDatosOK);
        this.ucCallCenter1.OnDatosWrong += new CallCenter.DatosWrong(this.ucCallCenter1_OnDatosWrong);
        this.RequirementData1 = (RequirementData) this.LoadControl("~/Claims/CustomControls/RequirementData.ascx");
        this.RequirementData1.OnDatosOK += new RequirementData.DatosOK(this.RequirementData1_OnDatosOK);
        this.RequirementData1.OnDatosWrong += new RequirementData.DatosWrong(this.RequirementData1_OnDatosWrong);
        this.ucUseChange1.OnDatosOK += new UseChange.DatosOK(this.ucUseChange1_OnDatosOK);
        this.ucUseChange1.OnDatosWrong += new UseChange.DatosWrong(this.ucUseChange1_OnDatosWrong);
        this.ucRemoveMotivation = (RemoveMotivation) this.LoadControl("~/Claims/CustomControls/RemoveMotivation.ascx");
        this.ucRemoveMotivation.OnDatosOK += new RemoveMotivation.DatosOK(this.ucRemoveMotivation_OnDatosOK);
        this.ucRemoveMotivation.OnDatosWrong += new RemoveMotivation.DatosWrong(this.ucRemoveMotivation_OnDatosWrong);
        this.ucEnablePaymentCode = (EnablePaymentCode) this.LoadControl("~/Claims/CustomControls/EnablePaymentCode.ascx");
        this.ucEnablePaymentCode.OnDatosOK += new EnablePaymentCode.DatosOK(this.ucEnablePaymentCode_OnDatosOK);
        this.ucEnablePaymentCode.OnDatosWrong += new EnablePaymentCode.DatosWrong(this.ucEnablePaymentCode_OnDatosWrong);
        this.ucRemoveRequirement = (RemoveRequirement) this.LoadControl("~/Claims/CustomControls/RemoveRequirement.ascx");
        this.ucRemoveRequirement.OnDatosOK += new RemoveRequirement.DatosOK(this.ucRemoveRequirement_OnDatosOK);
        this.ucRemoveRequirement.OnDatosWrong += new RemoveRequirement.DatosWrong(this.ucRemoveRequirement_OnDatosWrong);
        this.ucRegulariceMovementForDeliver = (RegulariceMovementForDeliver) this.LoadControl("~/Claims/CustomControls/RegulariceMovementForDeliver.ascx");
        this.ucRegulariceMovementForDeliver.OnDatosOK += new RegulariceMovementForDeliver.DatosOK(this.ucRegulariceMovementForDeliver_OnDatosOK);
        this.ucRegulariceMovementForDeliver.OnDatosWrong += new RegulariceMovementForDeliver.DatosWrong(this.ucRegulariceMovementForDeliver_OnDatosWrong);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgError, enmMessageType.Error, Constants.REQUIREMENT_ERROR_GENERICO + ex.Message);
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.spanRequestData1.Controls.Add((Control) this.ucRequestData1);
      this.spanRequestData2.Controls.Add((Control) this.ucRequestData2);
      this.spanDataNoAgree1.Controls.Add((Control) this.ucDataNoAgree1);
      this.spanBatchNoAgree1.Controls.Add((Control) this.ucBatchNoAgree1);
      this.spanProductNoAgree1.Controls.Add((Control) this.ucProductNoAgree1);
      this.spanReimbursementPayment1.Controls.Add((Control) this.ucReimbursementPayment1);
      this.spanCallCenter1.Controls.Add((Control) this.ucCallCenter1);
      this.spanRequesterDataTmp.Controls.Add((Control) this.ucRequesterDataTmp);
      this.spanUseChange1.Controls.Add((Control) this.ucUseChange1);
      this.spanChangeDeliveryPlate1.Controls.Add((Control) this.ucChangeDeliveryPlate1);
      this.spanRemoveMotivation.Controls.Add((Control) this.ucRemoveMotivation);
      this.spanEnablePaymentCode.Controls.Add((Control) this.ucEnablePaymentCode);
      this.spanRemoveRequirement.Controls.Add((Control) this.ucRemoveRequirement);
      this.spanRegulariceMovementForDeliver.Controls.Add((Control) this.ucRegulariceMovementForDeliver);
      this.spanRequirementData.Controls.Add((Control) this.RequirementData1);
      if (!this.Page.IsPostBack)
      {
        this.LoadParameters();
        this.ucRequesterDataTmp.EnabledControls(false);
        this.ClaimData1.EnableControls(false);
        this.spanRequestData1.Style.Add("display", "none");
        this.spanRequestData2.Style.Add("display", "none");
        this.spanDataNoAgree1.Style.Add("display", "none");
        this.spanBatchNoAgree1.Style.Add("display", "none");
        this.spanProductNoAgree1.Style.Add("display", "none");
        this.spanReimbursementPayment1.Style.Add("display", "none");
        this.spanCallCenter1.Style.Add("display", "none");
        this.spanRequesterDataTmp.Style.Add("display", "none");
        this.spanUseChange1.Style.Add("display", "none");
        this.spanChangeDeliveryPlate1.Style.Add("display", "none");
        this.spanRemoveMotivation.Style.Add("display", "none");
        this.spanEnablePaymentCode.Style.Add("display", "none");
        this.spanRemoveRequirement.Style.Add("display", "none");
        this.spanRegulariceMovementForDeliver.Style.Add("display", "none");
        this.spanRequirementData.Style.Add("display", "none");
      }
      this.lblMsgError.Visible = false;
    }

    protected void wddClaimType_SelectionChanged(object sender, EventArgs e) => this.LoadPanel();

    protected void btnSave_Click(object sender, EventArgs e)
    {
      if (!this.ValidateParameters())
        return;
      if (this.wddClaimType.SelectedValue == "7")
      {
        if (this.rblTypePetitory.SelectedIndex < 0)
        {
          Message.SetMessage(this.lblMsgError, enmMessageType.Warning, "Debe seleccionar un tipo de petitorio");
          return;
        }
        if (this.txtComents.Text.Trim() == "")
        {
          Message.SetMessage(this.lblMsgError, enmMessageType.Warning, "El campo detalle es obligatorio");
          return;
        }
      }
      using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
      {
        Timeout = new TimeSpan(0, 1, 1)
      }))
      {
        try
        {
          RequirementClaim pobjBE = this.GetObject();
          if (new RequirementClaimManagementBL().InsertClaim(ref pobjBE) > 0)
          {
            this.wibSave.Enabled = false;
            string[] source = pobjBE.v_RequestValues.Split('|');
            if (((IEnumerable<string>) source).Count<string>() > 5)
            {
              if (source[5].Contains("DELIVERY") && pobjBE.i_ClaimTypeId.GetValueOrDefault() == 12)
                Message.SetMessage(this.lblMsgError, enmMessageType.Success, "El Reclamo se registró satisfactoriamente. Recuerde reprogramar la fecha delivery después de ser aprobado.");
              else
                Message.SetMessage(this.lblMsgError, enmMessageType.Success, "El Reclamo se registró satisfactoriamente");
            }
            this.ClearControls();
          }
          else
            Message.SetMessage(this.lblMsgError, enmMessageType.Warning, "Se encontró un problema en el registró del reclamo");
          transactionScope.Complete();
        }
        catch (Exception ex)
        {
          Message.SetMessage(this.lblMsgError, enmMessageType.Error, ex.Message);
        }
      }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      this.ClearControls();
      this.lblMsgError.Visible = false;
    }

    private void LoadParameters()
    {
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.ClaimTypes.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      this.wddClaimType.Items.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (Convert.ToInt32(row["i_ParameterId"], (IFormatProvider) CultureInfo.CurrentCulture) == 7)
          this.wddClaimType.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        else if (Convert.ToInt32(row["i_ParameterId"], (IFormatProvider) CultureInfo.CurrentCulture) == 6)
          this.wddClaimType.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        else if (Convert.ToInt32(row["i_ParameterId"], (IFormatProvider) CultureInfo.CurrentCulture) == 10)
          this.wddClaimType.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        else if (Convert.ToInt32(row["i_ParameterId"], (IFormatProvider) CultureInfo.CurrentCulture) == 11)
          this.wddClaimType.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        else if (Convert.ToInt32(row["i_ParameterId"], (IFormatProvider) CultureInfo.CurrentCulture) == 12)
          this.wddClaimType.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        else if (Convert.ToInt32(row["i_ParameterId"], (IFormatProvider) CultureInfo.CurrentCulture) == 13)
          this.wddClaimType.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        else if (Convert.ToInt32(row["i_ParameterId"], (IFormatProvider) CultureInfo.CurrentCulture) == 14)
          this.wddClaimType.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        else if (Convert.ToInt32(row["i_ParameterId"], (IFormatProvider) CultureInfo.CurrentCulture) == 15)
          this.wddClaimType.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
      }
      this.wddClaimType.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Constants.OPCIONLISTA_Seleccione, Constants.OPCIONLISTA_ValorSinSeleccion));
    }

    public void LoadrblTypePetitory()
    {
      try
      {
        this.rblTypePetitory.ClearSelection();
        this.rblTypePetitory.Items.Clear();
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.TypeClaim.ToString((IFormatProvider) CultureInfo.CurrentCulture),
          (object) "",
          (object) "1",
          (object) "1"
        });
        if (dataTable == null)
          return;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          this.rblTypePetitory.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["v_Value"].ToString()));
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblmasage, ex);
      }
    }

    private void LoadPanel()
    {
      if (this.wddClaimType.SelectedValue == "-1" || this.wddClaimType.SelectedValue == "")
        return;
      enmClaimType int32 = (enmClaimType) Convert.ToInt32(this.wddClaimType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      this.spanRequestData1.Style.Add("display", "none");
      this.spanRequestData2.Style.Add("display", "none");
      this.spanDataNoAgree1.Style.Add("display", "none");
      this.spanBatchNoAgree1.Style.Add("display", "none");
      this.spanProductNoAgree1.Style.Add("display", "none");
      this.spanReimbursementPayment1.Style.Add("display", "none");
      this.spanCallCenter1.Style.Add("display", "none");
      this.spanRequesterDataTmp.Style.Add("display", "none");
      this.spanUseChange1.Style.Add("display", "none");
      this.spanChangeDeliveryPlate1.Style.Add("display", "none");
      this.spanRemoveMotivation.Style.Add("display", "none");
      this.spanEnablePaymentCode.Style.Add("display", "none");
      this.spanRemoveRequirement.Style.Add("display", "none");
      this.spanRegulariceMovementForDeliver.Style.Add("display", "none");
      this.spanRequirementData.Style.Add("display", "none");
      this.pControls.Visible = true;
      this.Panel2.Visible = false;
      this.txtComents.Visible = true;
      this.rblTypePetitory.Visible = false;
      this.lblPetitorio.Visible = false;
      this.wibSave.Enabled = true;
      switch (int32)
      {
        case enmClaimType.LogicInconsistency:
          this.spanRequesterDataTmp.Style.Add("display", "");
          this.Panel2.Visible = true;
          break;
        case enmClaimType.DataNoFound:
          this.spanRequestData1.Style.Add("display", "");
          this.spanRequesterDataTmp.Style.Add("display", "");
          this.Panel2.Visible = true;
          break;
        case enmClaimType.DataNoAgree:
          this.spanRequestData2.Style.Add("display", "");
          this.spanDataNoAgree1.Style.Add("display", "");
          this.spanRequesterDataTmp.Style.Add("display", "");
          this.Panel2.Visible = true;
          break;
        case enmClaimType.BatchNoAgree:
          this.spanBatchNoAgree1.Style.Add("display", "");
          break;
        case enmClaimType.ProductNoAgree:
          this.spanProductNoAgree1.Style.Add("display", "");
          this.spanRequesterDataTmp.Style.Add("display", "");
          this.Panel2.Visible = true;
          break;
        case enmClaimType.ReimbursementPayment:
          this.spanReimbursementPayment1.Style.Add("display", "");
          break;
        case enmClaimType.CallCenter:
          this.spanCallCenter1.Style.Add("display", "");
          this.spanRequesterDataTmp.Style.Add("display", "");
          this.Panel2.Visible = true;
          this.txtComents.Visible = true;
          this.rblTypePetitory.Visible = true;
          this.lblPetitorio.Visible = true;
          this.LoadrblTypePetitory();
          break;
        case enmClaimType.UseChange:
          this.spanUseChange1.Style.Add("display", "");
          this.spanRequesterDataTmp.Style.Add("display", "");
          this.Panel2.Visible = true;
          break;
        case enmClaimType.ChangeDeliveryPlate:
          this.spanChangeDeliveryPlate1.Style.Add("display", "");
          this.Panel2.Visible = true;
          break;
        case enmClaimType.RemoveMotivation:
          this.spanRemoveMotivation.Style.Add("display", "");
          this.Panel2.Visible = true;
          break;
        case enmClaimType.EnablePaymentCode:
          this.spanEnablePaymentCode.Style.Add("display", "");
          this.Panel2.Visible = true;
          break;
        case enmClaimType.RemoveRequirement:
          this.spanRemoveRequirement.Style.Add("display", "");
          this.Panel2.Visible = true;
          break;
        case enmClaimType.RegulariceMovementForDeliver:
          this.spanRegulariceMovementForDeliver.Style.Add("display", "");
          this.Panel2.Visible = true;
          break;
        case enmClaimType.UpdateApplicantData:
          this.spanRequirementData.Style.Add("display", "");
          this.Panel2.Visible = true;
          this.rblTypePetitory.Visible = true;
          this.rblTypePetitory.Enabled = false;
          this.txtComents.Enabled = false;
          this.lblPetitorio.Visible = true;
          this.LoadrblTypePetitory();
          break;
      }
    }

    private RequirementClaim GetObject()
    {
      string str = string.Empty;
      RequirementClaim requirementClaim = new RequirementClaim();
      requirementClaim.i_RequirementClaimId = 0;
      requirementClaim.i_ClaimTypeId = new int?((int) Convert.ToInt16(this.wddClaimType.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture));
      requirementClaim.v_ClaimDate = this.ClaimData1.ClaimDate.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      requirementClaim.i_Status = new int?(this.ClaimData1.ClaimStatus);
      requirementClaim.i_Priority = new int?(0);
      switch (Convert.ToInt16(this.wddClaimType.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture))
      {
        case 2:
          requirementClaim.v_RequestValues = this.ucRequestData1.GetTexts();
          requirementClaim.v_Requester = this.ucRequesterDataTmp.GetTexts();
          break;
        case 3:
          requirementClaim.v_RequestValues = "";
          requirementClaim.v_Requester = this.ucRequesterDataTmp.GetTexts();
          break;
        case 4:
          requirementClaim.v_RequestValues = this.ucBatchNoAgree1.GetTexts();
          requirementClaim.i_ClaimMotiveId = new int?(this.ucBatchNoAgree1.i_ClaimMotiveId);
          break;
        case 5:
          requirementClaim.v_RequestValues = this.ucProductNoAgree1.GetTexts();
          requirementClaim.v_Requester = this.ucRequesterDataTmp.GetTexts();
          break;
        case 6:
          requirementClaim.v_RequestValues = this.ucReimbursementPayment1.GetTexts();
          requirementClaim.i_ClaimMotiveId = new int?(this.ucReimbursementPayment1.i_ClaimMotiveId);
          requirementClaim.v_Requester = this.ucReimbursementPayment1.GetTextsRequester();
          break;
        case 7:
          requirementClaim.v_RequestValues = this.ucCallCenter1.GetTexts();
          requirementClaim.i_RequirementPlateRefId = new int?(Convert.ToInt32(this.Session["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture));
          requirementClaim.v_Requester = this.ucRequesterDataTmp.GetTexts();
          requirementClaim.i_ClaimMotiveId = new int?(this.ucCallCenter1.i_ClaimMotiveId);
          this.txtComents.Visible = true;
          this.lblPetitorio.Visible = true;
          this.rblTypePetitory.Visible = true;
          str = this.rblTypePetitory.SelectedItem.Text + "|" + this.txtComents.Text;
          break;
        case 9:
          requirementClaim.v_RequestValues = this.ucUseChange1.GetTexts();
          requirementClaim.v_Requester = this.ucRequesterDataTmp.GetTexts();
          break;
        case 10:
          requirementClaim.v_RequestValues = this.ucChangeDeliveryPlate1.GetTexts();
          requirementClaim.i_RequirementPlateRefId = new int?(Convert.ToInt32(this.Session["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture));
          this.txtComents.Visible = true;
          this.rblTypePetitory.Visible = false;
          this.lblPetitorio.Visible = false;
          break;
        case 11:
          requirementClaim.v_RequestValues = this.ucRemoveMotivation.GetTexts();
          requirementClaim.i_RequirementPlateRefId = new int?(Convert.ToInt32(this.Session["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture));
          this.txtComents.Visible = true;
          this.rblTypePetitory.Visible = false;
          this.lblPetitorio.Visible = false;
          break;
        case 12:
          requirementClaim.v_RequestValues = this.ucEnablePaymentCode.GetTexts();
          requirementClaim.i_RequirementId = Convert.ToInt32(this.Session["i_RequirementId"], (IFormatProvider) CultureInfo.CurrentCulture);
          this.txtComents.Visible = true;
          this.rblTypePetitory.Visible = false;
          this.lblPetitorio.Visible = false;
          break;
        case 13:
          requirementClaim.v_RequestValues = this.ucRemoveRequirement.GetTexts();
          requirementClaim.i_RequirementId = Convert.ToInt32(this.Session["i_RequirementId"], (IFormatProvider) CultureInfo.CurrentCulture);
          this.txtComents.Visible = true;
          this.rblTypePetitory.Visible = false;
          this.lblPetitorio.Visible = false;
          break;
        case 14:
          requirementClaim.v_RequestValues = this.ucRegulariceMovementForDeliver.GetTexts();
          requirementClaim.i_RequirementPlateRefId = new int?(Convert.ToInt32(this.Session["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture));
          this.txtComents.Visible = true;
          this.rblTypePetitory.Visible = false;
          this.lblPetitorio.Visible = false;
          break;
        case 15:
          requirementClaim.i_RequirementPlateId = new int?(Convert.ToInt32(this.Session["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture));
          requirementClaim.v_RequestValues = this.RequirementData1.GetRequestTexts();
          requirementClaim.v_Requester = this.RequirementData1.GetRequesterTexts();
          str = this.rblTypePetitory.SelectedItem.Text + " | " + this.txtComents.Text;
          break;
      }
      requirementClaim.d_AssignedDate = new DateTime?(this.ClaimData1.ClaimDate);
      requirementClaim.v_Comments = str == string.Empty ? this.txtComents.Text.TrimEnd() : str;
      requirementClaim.i_AssignedUserId = new int?(0);
      if (this.Session["SystemUser"] != null)
      {
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        requirementClaim.i_InsertUserId = new int?(systemUser.i_SystemUserId);
        requirementClaim.i_LocationId = new int?(systemUser.i_LocationId);
      }
      return requirementClaim;
    }

    private void ClearControls()
    {
      if (this.wddClaimType.SelectedValue != "")
      {
        switch (Convert.ToInt32(this.wddClaimType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
        {
          case 2:
            this.ucRequestData1.ClearControls();
            this.spanRequestData1.Style.Add("display", "none");
            break;
          case 3:
            this.ucRequestData2.ClearControls();
            this.spanRequestData2.Style.Add("display", "none");
            this.ucDataNoAgree1.ClearControls();
            this.spanDataNoAgree1.Style.Add("display", "none");
            break;
          case 4:
            this.ucBatchNoAgree1.ClearControls();
            this.spanBatchNoAgree1.Style.Add("display", "none");
            break;
          case 5:
            this.ucProductNoAgree1.ClearControls();
            this.spanProductNoAgree1.Style.Add("display", "none");
            break;
          case 6:
            this.ucReimbursementPayment1.ClearControls();
            this.spanReimbursementPayment1.Style.Add("display", "none");
            break;
          case 7:
            this.ucCallCenter1.ClearControls();
            this.spanCallCenter1.Style.Add("display", "none");
            break;
          case 9:
            this.ucUseChange1.ClearControls();
            this.spanUseChange1.Style.Add("display", "none");
            break;
          case 10:
            this.ucChangeDeliveryPlate1.ClearControls();
            this.spanChangeDeliveryPlate1.Style.Add("display", "none");
            break;
          case 11:
            this.ucRemoveMotivation.ClearControls();
            this.spanRemoveMotivation.Style.Add("display", "none");
            break;
          case 12:
            this.ucEnablePaymentCode.ClearControls();
            this.spanEnablePaymentCode.Style.Add("display", "none");
            break;
          case 13:
            this.ucRemoveRequirement.ClearControls();
            this.spanRemoveRequirement.Style.Add("display", "none");
            break;
          case 14:
            this.ucRegulariceMovementForDeliver.ClearControls();
            this.spanRegulariceMovementForDeliver.Style.Add("display", "none");
            break;
          case 15:
            this.RequirementData1.ClearControls();
            this.spanRequirementData.Style.Add("display", "none");
            this.rblTypePetitory.ClearSelection();
            break;
        }
      }
      this.wddClaimType.Enabled = true;
      this.wddClaimType.SelectedValue = "-1";
      this.ClaimData1.ClearControls();
      this.ucRequesterDataTmp.ClearControls();
      this.ucRequesterDataTmp.EnabledControls(false);
      this.ClaimData1.EnableControls(false);
      this.rblTypePetitory.Visible = false;
      this.lblPetitorio.Visible = false;
      this.txtComents.Text = "";
      this.txtComents.Enabled = false;
    }

    private void ucChangeDeliveryPlate1_OnDatosWrong(object sender)
    {
      this.ClaimData1.EnableControls(false);
      this.wddClaimType.Enabled = true;
    }

    private void ucChangeDeliveryPlate1_OnDatosOK(object sender)
    {
      this.ClaimData1.EnableControls(true);
      this.txtComents.Enabled = true;
      this.wddClaimType.Enabled = false;
    }

    private void ucReimbursementPayment1_OnDatosOK(object sender)
    {
      this.ClaimData1.EnableControls(true);
      this.txtComents.Enabled = true;
      this.wddClaimType.Enabled = false;
    }

    private void ucReimbursementPayment1_OnDatosWrong(object sender)
    {
      this.ClaimData1.EnableControls(false);
      this.wddClaimType.Enabled = true;
    }

    private void ucCallCenter1_OnDatosWrong(object sender)
    {
      this.ucRequesterDataTmp.EnabledControls(false);
      this.ClaimData1.EnableControls(false);
      this.wddClaimType.Enabled = true;
    }

    private void RequirementData1_OnDatosWrong(object sender)
    {
      this.ucRequesterDataTmp.EnabledControls(false);
      this.ClaimData1.EnableControls(false);
      this.wddClaimType.Enabled = true;
      this.txtComents.Text = "";
      this.txtComents.Enabled = false;
      this.rblTypePetitory.SelectedIndex = -1;
      this.rblTypePetitory.Enabled = false;
    }

    private void ucCallCenter1_OnDatosOK(object sender)
    {
      this.ucRequesterDataTmp.EnabledControls(true);
      this.ClaimData1.EnableControls(true);
      this.txtComents.Enabled = true;
      this.wddClaimType.Enabled = false;
    }

    private void RequirementData1_OnDatosOK(object sender)
    {
      this.ucRequesterDataTmp.EnabledControls(true);
      this.ClaimData1.EnableControls(true);
      this.txtComents.Enabled = true;
      this.wddClaimType.Enabled = false;
      this.rblTypePetitory.Enabled = true;
    }

    private void ucUseChange1_OnDatosWrong(object sender)
    {
      this.ClaimData1.EnableControls(false);
      this.wddClaimType.Enabled = true;
    }

    private void ucUseChange1_OnDatosOK(object sender)
    {
      this.ucRequesterDataTmp.EnabledControls(true);
      this.ClaimData1.EnableControls(true);
      this.txtComents.Enabled = true;
      this.wddClaimType.Enabled = false;
    }

    private void ucRemoveMotivation_OnDatosOK(object sender)
    {
      this.ClaimData1.EnableControls(true);
      this.txtComents.Enabled = true;
      this.wddClaimType.Enabled = false;
    }

    private void ucRemoveMotivation_OnDatosWrong(object sender)
    {
      this.ClaimData1.EnableControls(false);
      this.wddClaimType.Enabled = true;
    }

    private void ucEnablePaymentCode_OnDatosOK(object sender)
    {
      this.ClaimData1.EnableControls(true);
      this.txtComents.Enabled = true;
      this.wddClaimType.Enabled = false;
    }

    private void ucEnablePaymentCode_OnDatosWrong(object sender)
    {
      this.ClaimData1.EnableControls(false);
      this.wddClaimType.Enabled = true;
    }

    private void ucRemoveRequirement_OnDatosOK(object sender)
    {
      this.ClaimData1.EnableControls(true);
      this.txtComents.Enabled = true;
      this.wddClaimType.Enabled = false;
    }

    private void ucRemoveRequirement_OnDatosWrong(object sender)
    {
      this.ClaimData1.EnableControls(false);
      this.wddClaimType.Enabled = true;
    }

    private void ucRegulariceMovementForDeliver_OnDatosOK(object sender)
    {
      this.ClaimData1.EnableControls(true);
      this.txtComents.Enabled = true;
      this.wddClaimType.Enabled = false;
    }

    private void ucRegulariceMovementForDeliver_OnDatosWrong(object sender)
    {
      this.ClaimData1.EnableControls(false);
      this.wddClaimType.Enabled = true;
    }

    private bool ValidateParameters()
    {
      string pstrMessage = "";
      this.lblMsgError.Visible = false;
      if (this.wddClaimType.SelectedValue == "-1" || this.wddClaimType.SelectedValue == "")
      {
        Message.SetMessage(this.lblMsgError, enmMessageType.Warning, "***Advertencia</br>Debe seleccionar un tipo de reclamo");
        return false;
      }
      if (this.wddClaimType.SelectedValue != "-1")
      {
        enmClaimType int32 = (enmClaimType) Convert.ToInt32(this.wddClaimType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        switch (int32)
        {
          case enmClaimType.DataNoFound:
            if (!this.ucRequestData1.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos de solicitud";
              break;
            }
            break;
          case enmClaimType.DataNoAgree:
            if (!this.ucRequestData2.CompletedData)
              pstrMessage = "***Advertencia</br>Faltan completar datos de solicitud";
            if (!this.ucDataNoAgree1.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
          case enmClaimType.BatchNoAgree:
            if (!this.ucBatchNoAgree1.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
          case enmClaimType.ProductNoAgree:
            if (!this.ucProductNoAgree1.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
          case enmClaimType.ReimbursementPayment:
            if (!this.ucReimbursementPayment1.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
          case enmClaimType.CallCenter:
            if (!this.ucCallCenter1.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
          case enmClaimType.ChangeDeliveryPlate:
            if (!this.ucChangeDeliveryPlate1.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
          case enmClaimType.RemoveMotivation:
            if (!this.ucRemoveMotivation.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
          case enmClaimType.EnablePaymentCode:
            if (!this.ucEnablePaymentCode.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
          case enmClaimType.RemoveRequirement:
            if (!this.ucRemoveRequirement.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
          case enmClaimType.RegulariceMovementForDeliver:
            if (!this.ucRegulariceMovementForDeliver.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
          case enmClaimType.UpdateApplicantData:
            if (!this.RequirementData1.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Debe completar todos los datos de la sección de \"Datos de nuevo solicitante\".";
              break;
            }
            if (this.txtComents.Text == "" || this.rblTypePetitory.SelectedIndex == -1)
            {
              pstrMessage = "***Advertencia</br>Debe completar todos los datos de la sección de \"Petitorio\".";
              break;
            }
            break;
        }
        if (pstrMessage.Length > 0)
        {
          Message.SetMessage(this.lblMsgError, enmMessageType.Warning, pstrMessage);
          return false;
        }
        switch (int32)
        {
          case enmClaimType.DataNoFound:
            pstrMessage = this.ValidateDataRequester();
            break;
          case enmClaimType.DataNoAgree:
            pstrMessage = this.ValidateDataRequester();
            break;
          case enmClaimType.ProductNoAgree:
            pstrMessage = this.ValidateDataRequester();
            break;
          case enmClaimType.ReimbursementPayment:
            pstrMessage = this.ucReimbursementPayment1.ValidateDataRequester();
            break;
          case enmClaimType.CallCenter:
            pstrMessage = this.ValidateDataRequester();
            break;
          case enmClaimType.UseChange:
            pstrMessage = this.ValidateDataRequester();
            break;
          case enmClaimType.UpdateApplicantData:
            pstrMessage = this.RequirementData1.ValidateDataRequester();
            break;
        }
        if (pstrMessage.Length > 0)
        {
          Message.SetMessage(this.lblMsgError, enmMessageType.Warning, pstrMessage);
          return false;
        }
      }
      if (this.ClaimData1.According == 1)
        return true;
      Message.SetMessage(this.lblMsgError, enmMessageType.Warning, "***Advertencia</br>Debe Declarar estar conforme con el reclamo");
      return false;
    }

    private string ValidateDataRequester()
    {
      string str = "";
      if (this.ucRequesterDataTmp.DocumentType == "4")
      {
        if (this.ucRequesterDataTmp.DocumentNumber.Length < 10 || !Format.ValidateRUCstructure(this.ucRequesterDataTmp.DocumentNumber))
          str = "***Advertencia</br>El N° de RUC ingresado no es valido";
      }
      else if (this.ucRequesterDataTmp.DocumentType == "1" && (this.ucRequesterDataTmp.DocumentNumber.Length < 8 || this.ucRequesterDataTmp.DocumentNumber.Length > 8))
        str = "***Advertencia</br>El N° de DNI ingresado no es valido";
      else if (this.ucRequesterDataTmp.TelephoneNumber.Length == 0 && this.ucRequesterDataTmp.Email.Length == 0)
        str = "***Advertencia</br>Debe Especificar un Email o Número de Teléfono";
      else if (this.ucRequesterDataTmp.TelephoneNumber.Length > 0 && this.ucRequesterDataTmp.TelephoneNumber.Length > 0 && this.ucRequesterDataTmp.TelephoneNumber.Length != 9)
        str = "***Advertencia</br>Debe Especificar un Número de Teléfono o Celular Valido";
      return str;
    }
  }
}
