// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Requirement.RegisterRequirementMRE
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Claim.BL;
using SIIV.Common.Resource;
using SIIV.Registration.BL;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Requirement
{
  public class RegisterRequirementMRE : Page
  {
    private SIIV.BE.Requirement objRequirement;
    private RequirementContributor objRequirementContributor;
    private VehicleRegistrationDetail objVehicleRegistrationDetail;
    private RequirementPlate objRequirementPlate;
    private RequirementContributor objContributorRequester;
    private Payment objPayment;
    private DataBankManagementBL objDataBankManagement;
    private RequirementProgramation objRequirementProgramation;
    private VehicleRegistration objVehicleRegistration;
    private VehicleRegistrationManagementBL oVehicleRegistrationBL;
    private DataTable dtCategory;
    private DataTable dtVoucher;
    private DataTable dtProduct;
    private DataTable dtDocumentType;
    private DataTable dtBankData;
    private DataTable dtClaim;
    private DataBankQueriesBL oDataBankQueriesBL;
    protected UpdatePanel updatePanel;
    protected Wizard Wizard1;
    protected WizardStep WS_SeleccionPago;
    protected Label Label3;
    protected Button btnYes;
    protected Button btnNo;
    protected WizardStep WS_DatosVehiculo;
    protected DropDownList cboSpecialPlate;
    protected DropDownList cboClass;
    protected DropDownList cboProcess;
    protected TextBox txtPlate;
    protected RequiredFieldValidator RequiredFieldValidator5;
    protected ValidatorCalloutExtender ValidatorCalloutExtender5;
    protected Button btnValidar;
    protected TextBox txtBrand;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected ValidatorCalloutExtender ValidatorCalloutExtender1;
    protected TextBox txtModel;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender2;
    protected TextBox txtSerialNumber;
    protected RequiredFieldValidator RequiredFieldValidator3;
    protected ValidatorCalloutExtender ValidatorCalloutExtender3;
    protected TextBox txtPlateOld;
    protected DropDownList cboCategory;
    protected TextBox txtOwner;
    protected RequiredFieldValidator RequiredFieldValidator4;
    protected ValidatorCalloutExtender ValidatorCalloutExtender4;
    protected DropDownList CboDocumentType;
    protected TextBox txtNumDoc;
    protected FilteredTextBoxExtender txtNumDoc_FilteredTextBoxExtender;
    protected RequiredFieldValidator RequiredFieldValidator6;
    protected ValidatorCalloutExtender ValidatorCalloutExtender6;
    protected CheckBox chDenuncia;
    protected CheckBox chkAcceptData;
    protected CustomValidator CustomValidator1;
    protected Label lblMessageData2;
    protected WizardStep WS_AcreditacionPago;
    protected DropDownList cboBank;
    protected RadioButtonList rdlPaymentType;
    protected Fecha cboPaymentDate;
    protected Label lblTerminal;
    protected TextBox txtTerminal;
    protected FilteredTextBoxExtender txtTerminal_FilteredTextBoxExtender;
    protected RequiredFieldValidator RequiredFieldValidator12;
    protected ValidatorCalloutExtender ValidatorCalloutExtender12;
    protected Label lblCodigoUsuario;
    protected TextBox txtUserBankCode;
    protected RequiredFieldValidator RequiredFieldValidator13;
    protected ValidatorCalloutExtender ValidatorCalloutExtender13;
    protected Button btnValidatePago;
    protected Image ImgVoucher;
    protected WizardStep WS_SeleccionComprobante;
    protected RadioButtonList rdbProofPayment;
    protected HtmlTableCell TagDatosComprobante;
    protected TextBox txtBeneficiaryName;
    protected FilteredTextBoxExtender txtBeneficiaryName_FilteredTextBoxExtender;
    protected RequiredFieldValidator RequiredFieldValidator8;
    protected ValidatorCalloutExtender RequiredFieldValidator1_ValidatorCalloutExtender;
    protected DropDownList cboDocumentTypeProof;
    protected TextBox TxtDocNumberProofPaper;
    protected RequiredFieldValidator RequiredFieldValidator9;
    protected ValidatorCalloutExtender ValidatorCalloutExtender8;
    protected TextBox txtAddress;
    protected FilteredTextBoxExtender txtAddress_FilteredTextBoxExtender;
    protected TextBox txtBeneficiaryMail;
    protected RequiredFieldValidator RequiredFieldValidator33;
    protected FilteredTextBoxExtender FilteredTextBoxExtender3;
    protected RegularExpressionValidator RegularExpressionValidator3;
    protected ValidatorCalloutExtender ValidatorCalloutExtender7;
    protected ValidatorCalloutExtender ValidatorCalloutExtender33;
    protected DropDownList cboDeliveryPoint;
    protected HtmlTableCell Td1;
    protected WizardStep WS_DatosFinales;
    protected Label lblTitleRequirement;
    protected Label lblPlateNumber;
    protected Label lblPlateNumberOld;
    protected Label lblBrand;
    protected Label lblModel;
    protected Label lblCategory;
    protected Label lblSerialNumber;
    protected Label lblUseType;
    protected Label lblOwner;
    protected Label lblOwnerDocumentType;
    protected Label lblOwnerDocumentNumber;
    protected Label lblProofPaymentType;
    protected Label lblBeneficiary;
    protected Label lblAddress;
    protected Label lblBeneficiaryDumentType;
    protected Label lblbeneficiaryDocumentNumber;
    protected Label lblDeliveryPoint;
    protected Label lblDeliveryPointAddress;
    protected Label lblmensajepreview2;
    protected CheckBox chkConfirmPreview;
    protected CustomValidator CustomValidator3;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.Wizard1.ActiveStepIndex = 0;
      this.Initialize();
    }

    protected void MoveNext(object sender, EventArgs e)
    {
      try
      {
        this.lblMessage.Visible = false;
        if (this.Wizard1.ActiveStepIndex == this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_DatosVehiculo))
          this.ValidateData();
        else if (this.Wizard1.ActiveStepIndex == this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_AcreditacionPago))
        {
          this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_SeleccionComprobante);
        }
        else
        {
          if (this.Wizard1.ActiveStepIndex != this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_SeleccionComprobante))
            return;
          this.ValidateProofPaymentData();
          new ValidatorRegularExpressionProofPaymentBL().ProofPaymentData(this.cboDocumentTypeProof.Text.Trim(), this.TxtDocNumberProofPaper.Text.Trim(), this.txtBeneficiaryName.Text.Trim(), this.txtAddress.Text.Trim(), this.txtBeneficiaryMail.Text.Trim());
          this.showFinalData();
          this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_DatosFinales);
        }
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

    protected void btnYes_Click(object sender, EventArgs e)
    {
      this.Session["PaymentAnswer"] = (object) 1;
      this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_DatosVehiculo);
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
      this.Session["PaymentAnswer"] = (object) 2;
      this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_DatosVehiculo);
    }

    protected void SaveRequirement(object sender, EventArgs e)
    {
      try
      {
        if (!this.chkConfirmPreview.Checked)
          throw new HandledException(1, "***Advertencia</br>DEBE CONFIRMAR LOS DATOS MOSTRADOS");
        if (this.chDenuncia.Visible && !this.chDenuncia.Checked)
          throw new HandledException(1, "***Advertencia</br>DEBE CONFIRMAR DENUNCIA POLICIAL");
        using (TransactionScope transactionScope = new TransactionScope())
        {
          this.objRequirement = new SIIV.BE.Requirement();
          SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterRequirementMRE.aspx");
          this.objRequirement.i_RequirementTypeId = new int?(1);
          this.objRequirement.f_Quantity = new double?(1.0);
          this.objRequirement.v_Observations = "";
          this.objRequirement.i_ProofPaymentTypeId = new int?(Convert.ToInt32(this.rdbProofPayment.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture));
          this.objRequirement.i_InsertUserId = new int?(systemUser.i_SystemUserId);
          this.objRequirement.v_Ubigeo = "";
          int num1 = (int) this.Session["PaymentAnswer"];
          if (num1 == 1)
            this.dtProduct = this.ViewState["Product"] as DataTable;
          else
            this.dtProduct = new RequirementQueriesBL().GetProductByRegistrationClass(Convert.ToInt32(this.cboProcess.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) != 4 ? 2 : 4, int.Parse(this.cboClass.SelectedItem.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture));
          string empty1 = string.Empty;
          string str = string.Empty;
          string empty2 = string.Empty;
          string pstrPlate = "E" + this.txtPlate.Text.Replace("-", "").ToUpper();
          str = new RequirementQueriesBL().GetCorrelativeTitle();
          this.objRequirementPlate = new RequirementPlate();
          if (this.ViewState["DataBank"] != null)
            this.dtVoucher = this.ViewState["DataBank"] as DataTable;
          this.objRequirementPlate.i_RequirementPlateTypeId = !(pstrPlate.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture) == "E") ? new int?(1) : new int?(2);
          int num2 = 2;
          int num3 = int.Parse(this.cboSpecialPlate.SelectedItem.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
          int num4 = int.Parse(this.cboClass.SelectedItem.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
          int.Parse(this.cboProcess.SelectedItem.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
          int num5 = 0;
          this.objVehicleRegistrationDetail = new VehicleRegistrationDetail();
          this.objVehicleRegistrationDetail.i_RegistrationTypeId = new int?(num2);
          this.objVehicleRegistrationDetail.i_RegistryOfficeId = new int?();
          this.objVehicleRegistrationDetail.i_RegistryZoneId = new int?();
          this.objVehicleRegistrationDetail.i_VehicleCategoryId = new int?(Convert.ToInt32(this.cboCategory.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
          this.objVehicleRegistrationDetail.v_PlateNew = pstrPlate;
          this.objVehicleRegistrationDetail.v_PlateOld = !(this.txtPlateOld.Text.Trim() != "") ? pstrPlate.Replace("-", "") : (this.txtPlateOld.Text.Trim().Length != 5 ? this.txtPlateOld.Text.ToUpper() : "E" + this.txtPlateOld.Text.ToUpper());
          this.objVehicleRegistrationDetail.v_Brand = this.txtBrand.Text.Trim();
          this.objVehicleRegistrationDetail.v_Model = this.txtModel.Text.Trim();
          this.objVehicleRegistrationDetail.v_SerialNumber = this.txtSerialNumber.Text.Trim();
          this.objVehicleRegistrationDetail.v_CompleteNameOwner = this.txtOwner.Text.Trim();
          this.objVehicleRegistrationDetail.v_TitleNumber = "";
          this.objRequirementPlate = new RequirementPlate();
          this.objRequirementPlate.i_RegistrationUseTypeOldId = new int?();
          this.objRequirementPlate.i_ProcessTypeId = new int?(Convert.ToInt32(this.cboProcess.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
          switch (num4)
          {
            case 1:
              num5 = 18;
              break;
            case 5:
              num5 = 19;
              break;
          }
          this.objVehicleRegistrationDetail.i_VehicleTypeUseId = new int?(num5);
          this.objVehicleRegistrationDetail.i_VehicleClassId = new int?(num4);
          this.objVehicleRegistrationDetail.i_SpecialPlateTypeId = new int?(num3);
          this.objRequirementPlate.i_ProductId = new int?(Convert.ToInt32(this.dtProduct.Rows[0]["i_productid"], (IFormatProvider) CultureInfo.CurrentCulture));
          this.objRequirementPlate.v_RegistrationCode = "";
          this.objRequirementPlate.i_ContingencyTypeId = new int?();
          this.objRequirementPlate.b_ContingencyDelivery = new bool?();
          this.objRequirementPlate.d_RegistrationDispatchDate = new DateTime?();
          this.objRequirementPlate.i_DeliveryPointId = new int?(Convert.ToInt32(this.cboDeliveryPoint.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
          this.objRequirementPlate.i_PlateTypeId = new int?(1);
          this.objRequirementPlate.b_PendingConfirmation = new bool?();
          this.objRequirementPlate.b_Migrated = new bool?();
          this.objRequirementPlate.i_VehicleId = new int?();
          this.objRequirementPlate.i_RequirementPlateTypeId = new int?(2);
          this.objRequirementContributor = new RequirementContributor();
          this.objRequirementContributor.i_DocumentTypeId = new int?(Convert.ToInt32(this.cboDocumentTypeProof.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture));
          this.objRequirementContributor.v_DocumentNumber = this.TxtDocNumberProofPaper.Text.Trim();
          this.objRequirementContributor.v_LastName = "";
          this.objRequirementContributor.v_FirstName = "";
          this.objRequirementContributor.v_CompleteName = this.txtBeneficiaryName.Text.Trim();
          this.objRequirementContributor.v_Address = this.txtAddress.Text.Trim();
          this.objRequirementContributor.v_Email = this.txtBeneficiaryMail.Text.Trim();
          this.objContributorRequester = new RequirementContributor();
          this.objContributorRequester.i_DocumentTypeId = systemUser.i_DocumentTypeId;
          this.objContributorRequester.v_DocumentNumber = systemUser.v_DocumentNumber;
          this.objContributorRequester.v_LastName = systemUser.v_LastName;
          this.objContributorRequester.v_FirstName = systemUser.v_FirstName;
          this.objContributorRequester.v_CompleteName = systemUser.v_FirstName + " " + systemUser.v_LastName;
          this.objContributorRequester.v_AddressLocation = systemUser.v_Ubigeo;
          this.objContributorRequester.v_Address = systemUser.v_Address;
          this.objContributorRequester.v_PhoneNumber = (string) null;
          this.objRequirementPlate.v_OwnerCompleteName = this.txtOwner.Text;
          this.objRequirementPlate.v_OwnerDocumentType = this.CboDocumentType.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture);
          this.objRequirementPlate.v_OwnerDocumentDescription = this.CboDocumentType.SelectedItem.ToString();
          this.objRequirementPlate.v_OwnerDocumentNumber = this.txtNumDoc.Text.Trim();
          this.objPayment = new Payment();
          this.objPayment.f_PriceSale = new double?(Convert.ToDouble(this.dtProduct.Rows[0]["f_PriceCost"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
          this.objPayment.f_PriceTax = new double?(Convert.ToDouble(this.dtProduct.Rows[0]["f_PriceTax"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
          this.objPayment.f_PriceTotal = new double?(Convert.ToDouble(this.dtProduct.Rows[0]["f_PriceSale"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
          if (num1 == 2)
          {
            this.objRequirement.i_Status = new int?(1);
            this.objRequirementPlate.i_Status = new int?(0);
            this.objPayment.i_Status = new int?(0);
            this.objPayment.i_PaymentTypeId = new int?(1);
            this.objPayment.i_BankId = new int?(1);
            this.objPayment.v_BankOperationNumber = (string) null;
            this.objPayment.v_BankOperationUser = (string) null;
            this.objPayment.v_BankOperationTerminal = (string) null;
            this.objPayment.i_AccountId = new int?();
            this.objRequirementPlate.i_DataBankId = (string) null;
            this.objPayment.d_BankOperationDate = new DateTime?();
          }
          else
          {
            this.objRequirement.i_Status = new int?(1);
            this.objRequirementPlate.i_Status = new int?(1);
            this.objPayment.i_Status = new int?(1);
            this.objPayment.i_PaymentTypeId = new int?(1);
            this.objRequirementPlate.i_DataBankId = (this.ViewState["DataBank"] as DataTable).Rows[0]["idVoucher"].ToString();
            this.dtBankData = new DataBankQueriesBL().getDataBankbyId(Convert.ToInt32((this.ViewState["DataBank"] as DataTable).Rows[0]["idVoucher"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
            this.objPayment.i_BankId = new int?(Convert.ToInt32(this.dtBankData.Rows[0]["i_IdTipoBanco"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
            this.objPayment.v_BankOperationNumber = this.dtBankData.Rows[0]["v_NroOperacion"].ToString();
            this.objPayment.v_BankOperationUser = this.txtUserBankCode.Text.Trim();
            this.objPayment.v_BankOperationTerminal = this.dtBankData.Rows[0]["v_Terminal"].ToString();
            this.objPayment.d_BankOperationDate = new DateTime?(this.cboPaymentDate.Value);
            this.objPayment.i_AccountId = new int?();
          }
          this.oVehicleRegistrationBL = new VehicleRegistrationManagementBL();
          this.objVehicleRegistration = new VehicleRegistration();
          this.objVehicleRegistration.i_RegistrationTypeId = new int?(num2);
          this.objVehicleRegistration.i_VehicleCategoryId = new int?(Convert.ToInt32(this.cboCategory.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
          this.objVehicleRegistration.v_PlateNew = pstrPlate;
          this.objVehicleRegistration.v_PlateOld = !(this.txtPlateOld.Text.Trim() != "") ? pstrPlate.Replace("-", "") : (this.txtPlateOld.Text.Trim().Length != 5 ? this.txtPlateOld.Text : "E" + this.txtPlateOld.Text);
          this.objVehicleRegistration.v_Brand = this.txtBrand.Text.Trim();
          this.objVehicleRegistration.v_Model = this.txtModel.Text.Trim();
          this.objVehicleRegistration.v_SerialNumber = this.txtSerialNumber.Text.Trim();
          this.objVehicleRegistration.v_OwnerCompleteName = this.txtOwner.Text.Trim();
          this.objVehicleRegistration.v_OwnerDocumentType = this.CboDocumentType.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture);
          this.objVehicleRegistration.v_OwnerDocumentDescription = this.CboDocumentType.SelectedItem.ToString();
          this.objVehicleRegistration.v_OwnerDocumentNumber = this.txtNumDoc.Text.Trim();
          this.objVehicleRegistration.i_VehicleUseId = new int?(num5);
          this.objVehicleRegistration.i_UpdateUserId = new int?();
          this.objVehicleRegistration.v_PlatePrevious = (string) null;
          this.objVehicleRegistration.v_BlankCode1 = (string) null;
          this.objVehicleRegistration.v_BlankCode2 = (string) null;
          this.objVehicleRegistration.v_RFIDCode = (string) null;
          this.objVehicleRegistration.v_TIDCode = (string) null;
          this.objVehicleRegistration.v_EPCCode = (string) null;
          this.objVehicleRegistration.v_OptimalNumberCode = (string) null;
          this.objVehicleRegistration.v_TitleNumber = "";
          this.objVehicleRegistration.i_InsertUserId = new int?(systemUser.i_SystemUserId);
          this.objVehicleRegistration.d_InsertDate = new DateTime?(DateTime.Now);
          this.objVehicleRegistration.i_UpdateUserId = new int?();
          this.objVehicleRegistration.d_UpdateDate = new DateTime?();
          this.objVehicleRegistration.i_UpdateAll = false;
          this.objVehicleRegistration.i_Status = new int?(1);
          this.oVehicleRegistrationBL.VehicleRegistrationInsert(this.objVehicleRegistration);
          this.objRequirementProgramation = new RequirementProgramation();
          this.objRequirementProgramation.i_ZoneReference = new int?();
          this.objRequirementProgramation.i_DistrictReference = new int?();
          int[] numArray = new RequirementManagementBL().RequirementInsertOne(this.objRequirement, this.objVehicleRegistrationDetail, this.objRequirementPlate, this.objRequirementContributor, this.objContributorRequester, this.objPayment, this.objRequirementProgramation, this.Request.UserHostAddress);
          if (num1 == 1)
          {
            this.objDataBankManagement = new DataBankManagementBL();
            this.objDataBankManagement.ConciliatDataBank(Convert.ToInt32(numArray[1]), pstrPlate, Convert.ToInt32((this.ViewState["DataBank"] as DataTable).Rows[0]["idVoucher"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
          }
          transactionScope.Complete();
          this.Response.Redirect("~/Requirement/SuccessfulRegistration.aspx?RequirementId=" + numArray[0].ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&RequirementPlateId=" + numArray[1].ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&mre=1", false);
        }
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

    protected void ReturnPage(object sender, EventArgs e)
    {
      this.Response.Redirect("~/Default.aspx");
    }

    protected void cboSpecialPlate_SelectedIndexChanged(object sender, EventArgs e)
    {
      int pintVehicleRegistration = 2;
      int pintSpecialPlate = int.Parse(this.cboSpecialPlate.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
      if (this.cboSpecialPlate.SelectedIndex != 0)
      {
        this.cboClass.Enabled = true;
        this.cboClass.DataSource = (object) new RequirementQueriesBL().GetSpecialPlateClass(pintVehicleRegistration, pintSpecialPlate);
        this.cboClass.DataValueField = "i_VehicleClassId";
        this.cboClass.DataTextField = "VehicleClassDescription";
        this.cboClass.Items.Insert(0, new ListItem("Seleccione", "-1"));
        this.cboClass.SelectedIndex = 0;
        this.cboClass.DataBind();
        this.cboProcess.Enabled = false;
        this.cboProcess.Items.Clear();
        this.txtPlate.Text = "";
        this.txtPlate.Text = (this.ViewState["SpecialPlate"] as DataTable).Rows[this.cboSpecialPlate.SelectedIndex]["preSpecialPlate"].ToString();
      }
      else
      {
        this.cboClass.Enabled = false;
        this.cboClass.SelectedIndex = 0;
        this.cboProcess.Enabled = false;
        this.cboProcess.SelectedIndex = 0;
      }
    }

    protected void cboClass_SelectedIndexChanged(object sender, EventArgs e)
    {
      int pintVehicleRegistration = 2;
      int pintSpecialPlateType = int.Parse(this.cboSpecialPlate.SelectedItem.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
      int pintVehicleClass = int.Parse(this.cboClass.SelectedItem.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
      if (this.cboClass.SelectedIndex != 0)
      {
        this.cboProcess.DataSource = (object) new RequirementQueriesBL().GetSpecialRequirementType(pintVehicleRegistration, pintSpecialPlateType, pintVehicleClass);
        this.cboProcess.DataTextField = "ProcessTypeDescription";
        this.cboProcess.DataValueField = "i_ProcessTypeId";
        this.cboProcess.Items.Insert(0, new ListItem("Seleccione", "-1"));
        this.cboProcess.SelectedIndex = 0;
        this.cboProcess.Enabled = true;
        this.cboProcess.DataBind();
      }
      else
      {
        this.cboProcess.Enabled = false;
        this.cboProcess.Items.Clear();
      }
    }

    protected void cboProcess_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.chDenuncia.Checked = false;
      if (this.cboProcess.SelectedIndex == 2)
        this.chDenuncia.Visible = true;
      else
        this.chDenuncia.Visible = false;
    }

    protected void CboDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.txtNumDoc.Text = "";
      if (this.CboDocumentType.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture) == "1")
      {
        this.txtNumDoc.MaxLength = 8;
        this.txtNumDoc_FilteredTextBoxExtender.ValidChars = "0123456789";
      }
      else if (this.CboDocumentType.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture) == "4")
      {
        this.txtNumDoc.MaxLength = 11;
        this.txtNumDoc_FilteredTextBoxExtender.ValidChars = "0123456789";
      }
      else
      {
        this.txtNumDoc.MaxLength = 12;
        this.txtNumDoc_FilteredTextBoxExtender.ValidChars = "01234567890ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz/,|";
      }
      this.txtNumDoc.Focus();
    }

    protected void CleanTxtDoc(object sender, EventArgs e)
    {
      this.TxtDocNumberProofPaper.Text = "";
      this.TxtDocNumberProofPaper.Focus();
    }

    protected void btnValidatePago_Click(object sender, EventArgs e)
    {
      try
      {
        double num1 = 0.0;
        this.ValidateAcreditationData();
        string text1 = this.txtTerminal.Text;
        string text2 = this.txtUserBankCode.Text;
        int int32 = Convert.ToInt32(this.cboBank.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        DateTime dateTime = Convert.ToDateTime((object) this.cboPaymentDate.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        int pintVehicleRegistration = 2;
        int pintVehicleClass = int.Parse(this.cboClass.SelectedItem.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        int.Parse(this.cboSpecialPlate.SelectedItem.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        int.Parse(this.cboProcess.SelectedItem.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        this.ViewState["Product"] = (object) new RequirementQueriesBL().GetProductByRegistrationClass(pintVehicleRegistration, pintVehicleClass);
        this.dtProduct = this.ViewState["Product"] as DataTable;
        if ((this.ViewState["Product"] as DataTable).Rows.Count > 0)
          num1 = double.Parse((this.ViewState["Product"] as DataTable).Rows[0]["f_Pricesale"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        this.oDataBankQueriesBL = new DataBankQueriesBL();
        int num2;
        if (this.rdlPaymentType.SelectedIndex == 0)
        {
          num2 = 1;
          this.dtVoucher = this.oDataBankQueriesBL.PaymentAcreditation(int32, dateTime.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture), text1, text2, this.dtProduct.Rows[0]["v_code"].ToString(), num2);
        }
        else
        {
          num2 = 2;
          this.dtVoucher = this.oDataBankQueriesBL.PaymentAcreditation(int32, dateTime.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture), (string) null, text2, this.dtProduct.Rows[0]["v_code"].ToString(), num2);
        }
        if (Convert.ToInt32(this.dtVoucher.Rows[0]["idVoucher"], (IFormatProvider) CultureInfo.CurrentCulture) == 0)
          throw new HandledException(1, "***Advertencia</br>EL IMPORTE ABONADO EN EL BANCO NO CORRESPONDE AL PRECIO DE PRODUCTO");
        if (Convert.ToInt32(this.dtVoucher.Rows[0]["idVoucher"], (IFormatProvider) CultureInfo.CurrentCulture) == -1)
          throw new HandledException(1, "***Advertencia</br>SU PAGO NO HA SIDO ENCONTRADO INTENTE NUEVAMENTE EN 24 HORAS. SI EL PROBLEMA PERSISTE COMUNIQUESE CON NUESTRO CALL CENTER EN LIMA AL 640-3636 Y EN PROVINCIAS AL 0800-7-1111");
        if (Convert.ToInt32(this.dtVoucher.Rows[0]["idVoucher"], (IFormatProvider) CultureInfo.CurrentCulture) == -2)
          throw new HandledException(1, "***Advertencia</br>EL PAGO DE BANCO FUE ACREDITADA POR OTRO TRAMITE DE PLACA");
        if (Convert.ToInt32(this.dtVoucher.Rows[0]["idVoucher"], (IFormatProvider) CultureInfo.CurrentCulture) == -3)
          throw new HandledException(1, "***Advertencia</br>SU PAGO NO PUEDE SER VALIDADO POR TENER UNA ANTIGUEDAD MAYOR A 60 DIAS, COMUNIQUESE CON LA APP.");
        if (Convert.ToInt32(this.dtVoucher.Rows[0]["idVoucher"], (IFormatProvider) CultureInfo.CurrentCulture) == -4)
          throw new HandledException(1, "***Advertencia</br>EL PAGO NO SE PUEDE ACREDITAR, HA SIDO REEMBOLSADO.");
        if (Convert.ToInt32(this.dtVoucher.Rows[0]["idVoucher"], (IFormatProvider) CultureInfo.CurrentCulture) == -5)
          throw new HandledException(1, "***Advertencia</br>EL PAGO NO SE PUEDE ACREDITAR, HA SIDO OBSERVADO POR AAP. POR FAVOR COMUNIQUESE CON CASOS ESPECIALES.");
        if (Convert.ToInt32(this.dtVoucher.Rows[0]["idVoucher"], (IFormatProvider) CultureInfo.CurrentCulture) == -8)
        {
          this.dtClaim = new RequirementClaimQueriesBL().RequirementClaimGetActiveByIdBank(int32, num2, dateTime.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture), text1, text2);
          throw new HandledException(1, "***Advertencia</br>EL PAGO NO SE PUEDE ACREDITAR,POSEE UN RECLAMO EN CURSO</br>EL NUMERO DE RECLAMO ES " + this.dtClaim.Rows[0]["v_ClaimCode"].ToString());
        }
        Button controlFromWizard = this.GetControlFromWizard(this.Wizard1, RegisterRequirementMRE.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnNext") as Button;
        this.ViewState["DataBank"] = (object) this.dtVoucher;
        controlFromWizard.Enabled = true;
        Message.SetMessage(this.lblMessage, new HandledException(1, "***Exito</br>El PAGO FUE ENCONTRADO SATISFACTORIAMETE"));
        this.txtTerminal.Enabled = false;
        this.txtUserBankCode.Enabled = false;
        this.rdlPaymentType.Enabled = false;
        this.cboBank.Enabled = false;
        this.cboPaymentDate.Enabled = false;
        this.btnValidatePago.Enabled = false;
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

    protected void rdbProofPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        string str;
        if (this.rdbProofPayment.SelectedIndex == 0)
        {
          str = "4";
          this.cboDocumentTypeProof.Items.FindByValue("4").Enabled = true;
          this.cboDocumentTypeProof.SelectedValue = str;
          this.cboDocumentTypeProof.Enabled = false;
          this.TxtDocNumberProofPaper.Text = "";
          this.txtBeneficiaryName.Text = "";
          this.cboDeliveryPoint.SelectedIndex = 0;
        }
        else
        {
          str = "1";
          this.cboDocumentTypeProof.SelectedValue = str;
          this.cboDocumentTypeProof.Enabled = true;
          this.TxtDocNumberProofPaper.Text = "";
          this.cboDocumentTypeProof.Items.FindByValue("4").Enabled = false;
        }
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<script language='javascript'>");
        stringBuilder.Append("index1='" + str + "';");
        stringBuilder.Append("</script>");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "JSCR", stringBuilder.ToString(), false);
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

    protected void cboBank_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.rdlPaymentType.Visible = false;
        this.rdlPaymentType.SelectedIndex = 0;
        if (this.cboBank.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture) == "1")
        {
          this.txtTerminal.Text = "";
          this.ImgVoucher.ImageUrl = "~/Images/Requirement/boucher-bcp.png";
          this.txtTerminal.MaxLength = 4;
          this.rdlPaymentType.Visible = true;
        }
        else if (this.cboBank.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture) == "2")
        {
          this.txtTerminal.Text = "";
          this.ImgVoucher.ImageUrl = "~/Images/Requirement/voucherbbva.png";
          this.txtTerminal.MaxLength = 4;
        }
        else if (this.cboBank.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture) == "3")
        {
          this.txtTerminal.Text = "";
          this.ImgVoucher.ImageUrl = "~/Images/Requirement/Scotiabank.JPG";
          this.txtTerminal.MaxLength = 6;
        }
        else if (this.cboBank.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture) == "4")
        {
          this.txtTerminal.Text = "";
          this.ImgVoucher.ImageUrl = "~/Images/Requirement/VoucherInterbank.PNG";
          this.txtTerminal.MaxLength = 6;
        }
        else if (this.cboBank.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture) == "5")
        {
          this.txtTerminal.Text = "";
          this.ImgVoucher.ImageUrl = "~/Images/Requirement/Voucher_BIF.PNG";
          this.txtTerminal.MaxLength = 6;
        }
        else
          this.ImgVoucher.ImageUrl = "~/Images/Requirement/placas/sinplaca.PNG";
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

    protected void rdlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.rdlPaymentType.SelectedIndex == 0)
        {
          this.lblTerminal.Text = "terminal";
          this.txtTerminal.MaxLength = 4;
          this.txtTerminal.Text = "";
          this.txtTerminal.ReadOnly = false;
          this.ImgVoucher.ImageUrl = "~/Images/Requirement/boucher-bcp.png";
          this.txtTerminal.MaxLength = 4;
          this.rdlPaymentType.Visible = true;
          this.lblTerminal.Visible = true;
          this.txtTerminal.Visible = true;
        }
        else
        {
          this.lblTerminal.Text = "terminal";
          this.txtTerminal.MaxLength = 10;
          this.txtTerminal.Text = "";
          this.ImgVoucher.ImageUrl = "~/Images/Requirement/voucher_BCP_Internet.PNG";
          this.txtTerminal.MaxLength = 4;
        }
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

    public void Initialize()
    {
      try
      {
        this.getSpecialPlate();
        this.getDocumentType();
        this.getProofPayment();
        this.getLocation();
        this.getBank();
        this.ViewState["DataBank"] = (object) null;
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

    public void getSpecialPlate()
    {
      try
      {
        this.ViewState["SpecialPlate"] = (object) new RequirementQueriesBL().GetSpecialPlate(2);
        this.cboSpecialPlate.DataSource = (object) (this.ViewState["SpecialPlate"] as DataTable);
        this.cboSpecialPlate.DataValueField = "i_SpecialPlateId";
        this.cboSpecialPlate.DataTextField = "SpecialPlate";
        this.cboSpecialPlate.DataBind();
        this.cboSpecialPlate.SelectedIndex = 0;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void getCategory()
    {
      try
      {
        this.dtCategory = new RequirementQueriesBL().GetCategory();
        DataTable dataTable = this.dtCategory.Clone();
        int num = int.Parse(this.cboClass.SelectedItem.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        dataTable.ImportRow(this.dtCategory.Rows[0]);
        foreach (DataRow row in this.dtCategory.Select(num != 5 ? "v_Description not like '%L%'" : "v_Description like 'L%'"))
        {
          if (row != null)
            dataTable.ImportRow(row);
        }
        this.cboCategory.DataSource = (object) dataTable;
        this.cboCategory.DataValueField = "i_parameterId";
        this.cboCategory.DataTextField = "v_description";
        this.cboCategory.DataBind();
        this.cboCategory.SelectedIndex = 0;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void getProofPayment()
    {
      try
      {
        this.rdbProofPayment.DataSource = (object) new RequirementQueriesBL().GetProofPaymenType();
        this.rdbProofPayment.DataTextField = "v_Description";
        this.rdbProofPayment.DataValueField = "i_ParameterId";
        this.rdbProofPayment.DataBind();
        this.rdbProofPayment.SelectedIndex = 0;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void getDocumentType()
    {
      try
      {
        this.dtDocumentType = new RequirementQueriesBL().GetDocumentType();
        this.CboDocumentType.DataSource = (object) this.dtDocumentType;
        this.CboDocumentType.DataTextField = "v_Description";
        this.CboDocumentType.DataValueField = "i_ParameterId";
        this.CboDocumentType.DataBind();
        this.CboDocumentType.SelectedValue = "4";
        this.cboDocumentTypeProof.DataSource = (object) this.dtDocumentType;
        this.cboDocumentTypeProof.DataTextField = "v_Description";
        this.cboDocumentTypeProof.DataValueField = "i_ParameterId";
        this.cboDocumentTypeProof.DataBind();
        this.cboDocumentTypeProof.SelectedValue = "4";
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void getLocation()
    {
      try
      {
        DataTable locationRequirement = new RequirementQueriesBL().GetLocationRequirement();
        foreach (DataRow row in (InternalDataCollectionBase) locationRequirement.Rows)
        {
          if (row["i_LocationId"].ToString() == "17")
            row.Delete();
        }
        this.cboDeliveryPoint.DataSource = (object) locationRequirement;
        this.cboDeliveryPoint.DataTextField = "v_Description";
        this.cboDeliveryPoint.DataValueField = "i_LocationId";
        this.cboDeliveryPoint.DataBind();
        this.cboDeliveryPoint.SelectedIndex = 0;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void getBank()
    {
      try
      {
        this.cboBank.DataSource = (object) new RequirementQueriesBL().GetBank();
        this.cboBank.DataTextField = "v_Description";
        this.cboBank.DataValueField = "i_ParameterId";
        this.cboBank.DataBind();
        this.cboBank.SelectedIndex = 0;
        this.cboPaymentDate.Value = DateTime.Now;
        this.txtTerminal.Text = "";
        this.txtUserBankCode.Text = "";
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
    {
      try
      {
        if (!(this.GetControlFromWizard(this.Wizard1, RegisterRequirementMRE.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnNext") is Button controlFromWizard))
          return;
        controlFromWizard.Enabled = true;
        if (this.Wizard1.ActiveStepIndex == this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_AcreditacionPago))
          this.VerifyDataBank();
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

    public void ValidateData()
    {
      try
      {
        if (!this.chkAcceptData.Checked)
          throw new HandledException(1, "DEBE CONFIRMAR A LA ASOCIACIÓN AUTOMOTRIZ DEL PERU QUE LA INFORMACION MOSTRADA ES LA CORRECTA");
        if (this.chDenuncia.Visible && !this.chDenuncia.Checked)
          throw new HandledException(1, "DEBE CONFIRMAR DENUNCIA POLICIAL");
        this.ValidateVehicleData();
        this.ValidateFormat();
        if ((int) this.Session["PaymentAnswer"] == 1)
          this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_AcreditacionPago);
        else
          this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_SeleccionComprobante);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ValidateVehicleData()
    {
      try
      {
        if (this.txtBrand.Text.Trim() == "")
          throw new HandledException(1, "***Advertencia</br>INGRESE MARCA VEHICULO");
        if (this.txtModel.Text.Trim() == "")
          throw new HandledException(1, "***Advertencia</br>INGRESE MODELO VEHICULO");
        if (this.txtSerialNumber.Text.Trim() == "")
          throw new HandledException(1, "***Advertencia</br>INGRESE NRO SERIE VEHICULO");
        if (this.cboSpecialPlate.SelectedIndex == 0)
          throw new HandledException(1, "***Advertencia</br>SELECCIONE EL TIPO DE PLACA DEL VEHICULO");
        if (this.cboClass.SelectedIndex == 0)
          throw new HandledException(1, "***Advertencia</br>SELECCIONE CLASE DEL VEHICULO");
        if (this.cboProcess.SelectedIndex == 0)
          throw new HandledException(1, "***Advertencia</br>SELECCIONE UN TRAMITE VEHICULAR");
        if (this.cboCategory.SelectedIndex == 0)
          throw new HandledException(1, "***Advertencia</br>SELECCIONE CATEGORIA VEHICULO");
        if (this.txtOwner.Text.Trim() == "")
          throw new HandledException(1, "***Advertencia</br>INGRESE PROPIETARIO VEHICULO");
        if (this.txtPlateOld.Text.Trim() != "" && this.txtPlateOld.Text.Trim().Length < 5)
          throw new HandledException(1, "***Advertencia</br>LA PLACA ANTIGUA NO TIENE UNA LONGITUD CORRECTA");
        if (this.txtPlate.Text.Trim() == "")
          throw new HandledException(1, "***Advertencia</br>INGRESE NUMERO DE PLACA");
        if (this.txtPlate.Text.Trim().Length < 5)
          throw new HandledException(1, "***Advertencia</br>LA PLACA NO TIENE UNA LONGITUD CORRECTA");
        if (this.CboDocumentType.SelectedIndex == 0)
          throw new HandledException(1, "***Advertencia</br>SELECCIONE TIPO DE DOCUMENTO DE IDENTIDAD");
        if (this.CboDocumentType.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture) == "1" && this.txtNumDoc.Text.Trim().Length < 8)
          throw new HandledException(1, "***Advertencia</br>DNI DEBE TENER 8 DIGITOS");
        if (this.CboDocumentType.SelectedValue == "4" && this.txtNumDoc.Text.Trim().Length < 10)
          throw new HandledException(1, "***Advertencia</br>RUC INGRESADO NO ES VALIDO LONGITUD INCORRECTA");
        if (this.CboDocumentType.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture) == "4" && this.txtNumDoc.Text.Trim().Substring(0, 1) != "1" && this.txtNumDoc.Text.Trim().Substring(0, 1) != "2")
          throw new HandledException(1, "***Advertencia</br>RUC INGRESADO NO ES VALIDO DEBE INICIARCE EN 1 Ó 2");
        if (this.txtNumDoc.Text.Trim() == "")
          throw new HandledException(1, "***Advertencia</br>INGRESE NRO DOCUMENTO PROPIETARIO");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ValidateFormat()
    {
      try
      {
        if (this.txtPlate.Text.Substring(0, 2) != (this.ViewState["SpecialPlate"] as DataTable).Rows[this.cboSpecialPlate.SelectedIndex]["preSpecialPlate"].ToString())
          throw new HandledException(1, "EL FORMATO DE LA PLACA " + (this.ViewState["SpecialPlate"] as DataTable).Rows[this.cboSpecialPlate.SelectedIndex]["SpecialPlate"].ToString() + "NO ES CORRECTO, EJMP:" + (this.ViewState["SpecialPlate"] as DataTable).Rows[this.cboSpecialPlate.SelectedIndex]["preSpecialPlate"].ToString() + "123");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public bool IsNumeric(object Expression)
    {
      return double.TryParse(Convert.ToString(Expression, (IFormatProvider) CultureInfo.CurrentCulture), NumberStyles.Any, (IFormatProvider) NumberFormatInfo.InvariantInfo, out double _);
    }

    private Control GetControlFromWizard(
      Wizard wizard,
      RegisterRequirementMRE.WizardNavigationTempContainer wzdTemplate,
      string controlName)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append((object) wzdTemplate);
      stringBuilder.Append("$");
      stringBuilder.Append(controlName);
      return wizard.FindControl(stringBuilder.ToString());
    }

    public void VerifyDataBank()
    {
      try
      {
        Button controlFromWizard = this.GetControlFromWizard(this.Wizard1, RegisterRequirementMRE.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnNext") as Button;
        if (this.ViewState["DataBank"] != null)
        {
          controlFromWizard.Enabled = true;
          this.txtTerminal.Enabled = false;
          this.txtUserBankCode.Enabled = false;
          this.rdlPaymentType.Enabled = false;
          this.cboBank.Enabled = false;
          this.cboPaymentDate.Enabled = false;
          this.btnValidatePago.Enabled = false;
        }
        else
        {
          controlFromWizard.Enabled = false;
          this.txtTerminal.Enabled = true;
          this.txtUserBankCode.Enabled = true;
          this.rdlPaymentType.Enabled = true;
          this.cboBank.Enabled = true;
          this.cboPaymentDate.Enabled = true;
          this.btnValidatePago.Enabled = true;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ValidateAcreditationData()
    {
      try
      {
        if (this.cboBank.SelectedIndex == 0)
          throw new HandledException(1, "***Advertencia</br>SELECCIONE BANCO DE PAGO");
        if (this.cboPaymentDate.Text == "")
          throw new HandledException(1, "***Advertencia</br>INGRESE FECHA DE PAGO");
        if (this.txtTerminal.Text == "" && this.rdlPaymentType.SelectedIndex == 0)
          throw new HandledException(1, "***Advertencia</br>INGRESE TERMINAL DEL VOUCHER");
        if (this.txtUserBankCode.Text == "")
          throw new HandledException(1, "***Advertencia</br>INGRESE CODIGO ID USUARIO VOUCHER");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ValidateProofPaymentData()
    {
      try
      {
        if (this.txtBeneficiaryName.Text == "")
          throw new HandledException(1, "***Advertencia</br>INGRESE NOMBRES O RAZON SOCIAL DEL COMPROBANTE");
        if (this.cboDocumentTypeProof.SelectedIndex == 0)
          throw new HandledException(1, "***Advertencia</br>SELECCIONE TIPO DE DOCUMENTO DE IDENTIDAD");
        if (this.cboDocumentTypeProof.SelectedValue == "")
          throw new HandledException(1, "***Advertencia</br>SELECCIONE TIPO DE DOCUMENTO DE IDENTIDAD");
        if (this.TxtDocNumberProofPaper.Text.Trim() == "")
          throw new HandledException(1, "***Advertencia</br>INGRESE NUMERO DE DOCUMENTO DEL COMPROBANTE");
        if (this.cboDocumentTypeProof.SelectedValue == "1" && this.TxtDocNumberProofPaper.Text.Trim().Length < 8)
          throw new HandledException(1, "***Advertencia</br>DNI DEBE TENER 8 DIGITOS");
        if (this.cboDocumentTypeProof.SelectedValue == "4" && this.TxtDocNumberProofPaper.Text.Trim().Length < 11)
          throw new HandledException(1, "***Advertencia</br>RUC INGRESADO NO ES VALIDO LONGITUD INCORRECTA");
        if (this.cboDocumentTypeProof.SelectedValue == "4" && this.TxtDocNumberProofPaper.Text.Trim().Substring(0, 1) != "1" && this.TxtDocNumberProofPaper.Text.Trim().Substring(0, 1) != "2")
          throw new HandledException(1, "***Advertencia</br>RUC INGRESADO NO ES VALIDO DEBE INICIARCE EN 1 Ó 2");
        if (this.cboDocumentTypeProof.SelectedValue == "4" && !this.ValidateRuc())
          throw new HandledException(1, "***Advertencia</br>LA ESTRUCTURA DEL NUMERO DE R.U.C. INGRESADO NO ES CORRECTO");
        if (this.cboDeliveryPoint.SelectedIndex == 0)
          throw new HandledException(1, "***Advertencia</br>SELECCIONE PUNTO DE ENTREGA");
        if (this.TxtDocNumberProofPaper.Text.Trim() == "20101973922")
          throw new HandledException(1, "***Advertencia</br>NO SE PUEDE REALIZAR TRAMITES CON ESTE NUMERO DE R.U.C.");
        if (!this.RestrictionRazonSocial())
          throw new HandledException(1, "***Advertencia</br>ESTA RESTRINGIDA LA CREACION DE TRAMITES CON ESTA RAZON SOCIAL");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private bool ValidateRuc()
    {
      string str = this.TxtDocNumberProofPaper.Text.Trim();
      int num = 11 - (int.Parse(str.Substring(0, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 5 + int.Parse(str.Substring(1, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 4 + int.Parse(str.Substring(2, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 3 + int.Parse(str.Substring(3, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 2 + int.Parse(str.Substring(4, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 7 + int.Parse(str.Substring(5, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 6 + int.Parse(str.Substring(6, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 5 + int.Parse(str.Substring(7, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 4 + int.Parse(str.Substring(8, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 3 + int.Parse(str.Substring(9, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 2) % 11;
      return (int.Parse(str.Length.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture) != 11 ? 10 : int.Parse(str.Substring(10, 1), (IFormatProvider) CultureInfo.CurrentCulture)) == (num != 10 ? 0 : 0) + (num != 11 ? 0 : 1) + (num >= 10 ? 0 : num);
    }

    private bool RestrictionRazonSocial()
    {
      string str = this.txtBeneficiaryName.Text.ToUpper(CultureInfo.CurrentCulture).Replace("À", "A").Replace("Á", "A").Replace("È", "E").Replace("É", "E").Replace("Ì", "I").Replace("Í", "I").Replace("Ó", "O").Replace("Ó", "O").Replace("Ù", "U").Replace("Ú", "U");
      return !str.Contains("ASOCIACION") || !str.Contains("AUTOMOTRIZ") || !str.Contains("PERU");
    }

    private void showFinalData()
    {
      try
      {
        this.lblTitleRequirement.Text = this.cboProcess.SelectedItem.Text;
        this.lblPlateNumber.Text = this.txtPlate.Text.ToUpper();
        this.lblPlateNumberOld.Text = this.txtPlateOld.Text.ToUpper();
        this.lblBrand.Text = this.txtBrand.Text;
        this.lblModel.Text = this.txtModel.Text;
        this.lblCategory.Text = this.cboCategory.SelectedItem.Text;
        this.lblSerialNumber.Text = this.txtSerialNumber.Text;
        this.lblUseType.Text = "-";
        this.lblOwner.Text = this.txtOwner.Text;
        this.lblOwnerDocumentType.Text = this.CboDocumentType.SelectedItem.Text;
        this.lblOwnerDocumentNumber.Text = this.txtNumDoc.Text;
        this.lblProofPaymentType.Text = this.rdbProofPayment.SelectedItem.Text;
        this.lblBeneficiary.Text = this.txtBeneficiaryName.Text;
        this.lblBeneficiaryDumentType.Text = this.CboDocumentType.SelectedItem.Text;
        this.lblbeneficiaryDocumentNumber.Text = this.TxtDocNumberProofPaper.Text;
        this.lblAddress.Text = this.txtAddress.Text;
        this.lblDeliveryPoint.Text = this.cboDeliveryPoint.SelectedItem.Text;
        this.lblDeliveryPointAddress.Text = new RequirementQueriesBL().GetLocation(this.cboDeliveryPoint.SelectedItem.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture), "", "").Rows[1]["v_Address"].ToString();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void btnValidar_Click(object sender, EventArgs e)
    {
      try
      {
        this.lblMessage.Visible = false;
        this.getCategory();
        this.ValidaPlaca();
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

    private void ValidaPlaca()
    {
      try
      {
        string empty = string.Empty;
        string pstrPlateNumber = "E" + this.txtPlate.Text.Replace("-", "");
        if (this.cboProcess.SelectedItem == null || this.txtPlate.Text.Length != 5)
          throw new HandledException(1, "***Advertencia</br>Ingrese datos ****");
        DataTable dataTable1 = new RequirementQueriesBL().ValidateExistMRE(pstrPlateNumber, Convert.ToInt32(this.cboProcess.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
        if (Convert.ToInt32(this.cboProcess.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) == 1 || Convert.ToInt32(this.cboProcess.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) == 4)
        {
          this.txtBrand.Text = dataTable1.Rows[0][0].ToString();
          this.txtModel.Text = dataTable1.Rows[0][1].ToString();
          this.txtSerialNumber.Text = dataTable1.Rows[0][2].ToString();
          this.txtPlateOld.Text = dataTable1.Rows[0][3].ToString();
          this.txtOwner.Text = dataTable1.Rows[0][5].ToString();
          this.txtNumDoc.Text = dataTable1.Rows[0][7].ToString();
          this.cboCategory.SelectedValue = dataTable1.Rows[0][4].ToString() == "" ? "0" : dataTable1.Rows[0][4].ToString();
        }
        else
        {
          this.txtBrand.Text = "";
          this.txtModel.Text = "";
          this.txtSerialNumber.Text = "";
          this.txtPlateOld.Text = "";
          this.cboCategory.SelectedIndex = 0;
          this.txtOwner.Text = "";
          this.txtNumDoc.Text = "";
          this.CboDocumentType.SelectedIndex = 0;
        }
        DataTable dataTable2 = new DataTable();
        DataTable documentType = new RequirementQueriesBL().GetDocumentType();
        for (int index = 0; index < documentType.Rows.Count; ++index)
        {
          if (documentType.Rows[index][2].ToString() == dataTable1.Rows[0][6].ToString())
          {
            this.CboDocumentType.SelectedValue = dataTable1.Rows[0][6].ToString();
            this.txtNumDoc.ReadOnly = false;
            this.CboDocumentType.Enabled = false;
            break;
          }
          this.txtNumDoc.ReadOnly = true;
          this.CboDocumentType.Enabled = true;
        }
        this.txtNumDoc.ReadOnly = this.txtNumDoc.Text != "";
        this.txtBrand.ReadOnly = this.txtBrand.Text != "";
        this.txtModel.ReadOnly = this.txtModel.Text != "";
        this.txtSerialNumber.ReadOnly = this.txtSerialNumber.Text != "";
        this.txtPlateOld.ReadOnly = this.txtPlateOld.Text != "";
        this.cboCategory.Enabled = this.cboCategory.SelectedIndex <= 0;
        this.txtOwner.ReadOnly = this.txtOwner.Text != "";
        this.txtBrand.Enabled = true;
        this.txtModel.Enabled = true;
        this.txtSerialNumber.Enabled = true;
        this.txtPlateOld.Enabled = true;
        this.txtOwner.Enabled = true;
        this.txtNumDoc.Enabled = true;
        this.cboSpecialPlate.Enabled = false;
        this.cboClass.Enabled = false;
        this.cboProcess.Enabled = false;
        this.txtPlate.Enabled = false;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public enum WizardNavigationTempContainer
    {
      StartNavigationTemplateContainerID = 1,
      StepNavigationTemplateContainerID = 2,
      FinishNavigationTemplateContainerID = 3,
    }
  }
}
