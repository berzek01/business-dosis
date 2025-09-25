// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Requirement.RegisterClaimRequirement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Claim.BL;
using SIIV.Common.Resource;
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
  public class RegisterClaimRequirement : Page
  {
    private SIIV.BE.Requirement objRequirement;
    private SystemUser objUserBE;
    private RequirementContributor objRequirementContributor;
    private VehicleRegistrationDetail objVehicleRegistrationDetail;
    private RequirementPlate objRequirementPlate;
    private RequirementContributor objContributorRequester;
    private Payment objPayment;
    private RequirementProgramation objRequirementProgramation;
    private RequirementClaimManagementBL oRequirementClaimManagement;
    private RequirementQueriesBL oRequirementQueriesBL;
    private RequirementManagementBL oRequirementManagement;
    private SpecialRequirement objSpecialRequirement;
    private DataTable dtVehicleData;
    private DataTable dtOwners;
    private DataTable DtProduct;
    private DataTable dtDocument;
    private DataTable dtChangeUse;
    private Button btnNext;
    private Button btnNext1;
    private Button btnPrevious;
    private Button btnPrevious1;
    protected UpdatePanel updatePanel;
    protected Wizard Wizard1;
    protected WizardStep WS_ValidacionReclamo;
    protected TextBox txtClaimCode;
    protected RequiredFieldValidator RequiredFieldValidator3;
    protected ValidatorCalloutExtender ValidatorCalloutExtender1;
    protected WizardStep WS_ValidacionOficio;
    protected TextBox txtOficeCode;
    protected RequiredFieldValidator RequiredFieldValidator7;
    protected ValidatorCalloutExtender ValidatorCalloutExtender3;
    protected WizardStep WS_ValidacionOrden;
    protected TextBox txtPlateNumber;
    protected RequiredFieldValidator RequiredFieldValidator14;
    protected ValidatorCalloutExtender ValidatorCalloutExtender14;
    protected TextBox txtTitleNumber;
    protected RequiredFieldValidator RequiredFieldValidator15;
    protected ValidatorCalloutExtender ValidatorCalloutExtender15;
    protected FilteredTextBoxExtender txtTitleNumber_FilteredTextBoxExtender;
    protected Image Image1;
    protected WizardStep WS_ValidacionPlaca;
    protected TextBox txtPlateNumber1;
    protected RequiredFieldValidator RequiredFieldValidator16;
    protected ValidatorCalloutExtender ValidatorCalloutExtender16;
    protected WizardStep WS_DatosVehiculo1;
    protected Label Label2;
    protected TextBox txtPlateNew;
    protected TextBox txtPlateTitle1;
    protected TextBox txtPlateOld;
    protected TextBox txtBrand;
    protected TextBox txtModel;
    protected TextBox txtSerialNumber;
    protected TextBox txtOwners;
    protected TextBox txtDocumentType;
    protected TextBox txtDocumentNumber;
    protected TextBox txtUseType;
    protected TextBox txtDispatchDate;
    protected TextBox txtRegistryZone;
    protected TextBox txtRegistryOffice;
    protected TextBox txtCategory;
    protected TextBox txtProcess;
    protected TextBox txtProduct;
    protected TextBox txtPrice;
    protected CheckBox chkAccept;
    protected CustomValidator CustomValidator2;
    protected Label lblData1Message2;
    protected WizardStep WS_DatosVehiculo2;
    protected Label Label1;
    protected TextBox txtPlateNew3rd;
    protected TextBox txtPlateOld3rd;
    protected TextBox txtBrand3rd;
    protected TextBox txtModel3rd;
    protected TextBox txtSerialNumber3rd;
    protected TextBox txtCategory3rd;
    protected TextBox txtUseType3rd;
    protected Label lblPrice;
    protected TextBox txtPrice3rd;
    protected Panel pblUseNew;
    protected DropDownList cboUseNew;
    protected TextBox txtOwner3rd;
    protected CheckBox chkAccept3rd;
    protected CustomValidator CustomValidator1;
    protected Label lblData2Message2;
    protected WizardStep WS_ExisteComprobante;
    protected Label Label3;
    protected Button btnYes;
    protected Button btnNo;
    protected WizardStep WS_SeleccionComprobante;
    protected Label lblProofTitle;
    protected HtmlTableRow trProofType;
    protected RadioButtonList rdbProofPayment;
    protected HtmlTableRow trProofData;
    protected HtmlTableCell TagDatosComprobante;
    protected TextBox txtBeneficiaryName;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected ValidatorCalloutExtender RequiredFieldValidator1_ValidatorCalloutExtender;
    protected FilteredTextBoxExtender txtBeneficiaryName_FilteredTextBoxExtender;
    protected DropDownList CboDocumentType;
    protected TextBox TxtDocNumberProofPaper;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender2;
    protected TextBox txtAddress;
    protected FilteredTextBoxExtender txtAddress_FilteredTextBoxExtender;
    protected HtmlTableRow trDelivery;
    protected DropDownList cboDeliveryPoint;
    protected WizardStep WS_DatosFinales;
    protected Label lblTitleRequirement;
    protected Label lblPlateNumber;
    protected Label lblPlateNumberOld;
    protected Label lblBrand;
    protected Label lblModel;
    protected Label lblCategory;
    protected Label lblSerialNumber;
    protected Label lblUseType;
    protected HtmlTableRow TagUseNew;
    protected Label lblUseNew;
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
    protected WizardStep WS_DatosSolicitante;
    protected HtmlTableRow TagName1;
    protected TextBox txtRequesterName;
    protected RequiredFieldValidator RequiredFieldValidator4;
    protected ValidatorCalloutExtender ValidatorCalloutExtender4;
    protected HtmlTableRow TagName2;
    protected TextBox txtRequesterLast1;
    protected TextBoxWatermarkExtender txtRequesterLast1_TextBoxWatermarkExtender;
    protected TextBox txtRequesterLast2;
    protected TextBoxWatermarkExtender TextBoxWatermarkExtender1;
    protected RequiredFieldValidator RequiredFieldValidator5;
    protected ValidatorCalloutExtender ValidatorCalloutExtender5;
    protected RequiredFieldValidator RequiredFieldValidator6;
    protected ValidatorCalloutExtender ValidatorCalloutExtender6;
    protected DropDownList cboRequesterTypeDoc;
    protected TextBox txtRequesterNumberDoc;
    protected RequiredFieldValidator RequiredFieldValidator8;
    protected ValidatorCalloutExtender ValidatorCalloutExtender8;
    protected TextBox txtRequesterPhone;
    protected RequiredFieldValidator RequiredFieldValidator9;
    protected ValidatorCalloutExtender ValidatorCalloutExtender9;
    protected MaskedEditExtender txtUserPhone_MaskedEditExtender;
    protected Label Label25;
    protected TextBox txtRequesterMail;
    protected FilteredTextBoxExtender txtRequesterMail_FilteredTextBoxExtender1;
    protected RegularExpressionValidator RegularExpressionValidator1;
    protected ValidatorCalloutExtender RegularExpressionValidator1_ValidatorCalloutExtender;
    protected Label lblMessage;
    protected Button btnJavaScriptResponse;
    protected HiddenField hidSunarpId;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.IsPostBack)
        return;
      this.Initialize();
    }

    protected void ReturnPage(object sender, EventArgs e)
    {
      if (Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) == "AAP")
        this.Response.Redirect("~/Requirement/BeginRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) + "&idDoc=" + Convert.ToString(this.Request.QueryString["idDoc"], (IFormatProvider) CultureInfo.CurrentCulture));
      else
        this.Response.Redirect("~/Requirement/BeginRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture));
    }

    protected void MoveNext(object sender, EventArgs e)
    {
      try
      {
        if (this.Wizard1.ActiveStep == this.WS_ValidacionReclamo)
          this.validateClaimCode();
        else if (this.Wizard1.ActiveStep == this.WS_ValidacionOficio)
          this.validateOfficeCode();
        else if (this.Wizard1.ActiveStep == this.WS_DatosSolicitante)
          this.validateRequesterData();
        else if (this.Wizard1.ActiveStep == this.WS_ValidacionOrden)
          this.ValidateSunarp();
        else if (this.Wizard1.ActiveStep == this.WS_ValidacionPlaca)
          this.ValidatePlate();
        else if (this.Wizard1.ActiveStep == this.WS_DatosVehiculo1)
          this.redirectVehicleData();
        else if (this.Wizard1.ActiveStep == this.WS_DatosVehiculo2)
        {
          if (!this.Validate3rdData())
            return;
          this.redirectVehicleData();
        }
        else
        {
          if (this.Wizard1.ActiveStep != this.WS_SeleccionComprobante || !this.ValidateProofPaymentData())
            return;
          if ((int) this.Session["ProofAnswer"] == 1)
            new ValidatorRegularExpressionProofPaymentBL().ProofPaymentData(this.CboDocumentType.Text.Trim(), this.TxtDocNumberProofPaper.Text.Trim(), this.txtBeneficiaryName.Text.Trim(), this.txtAddress.Text.Trim(), "X");
          this.showFinalData((int) this.Session["ProcessId"]);
          this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosFinales);
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

    protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
    }

    protected void SaveRequirement(object sender, EventArgs e)
    {
      try
      {
        if (!this.chkConfirmPreview.Checked)
          throw new HandledException(1, "DEBE CONFIRMAR LOS DATOS MOSTRADOS");
        using (TransactionScope transactionScope = new TransactionScope())
        {
          this.oRequirementManagement = new RequirementManagementBL();
          this.oRequirementQueriesBL = new RequirementQueriesBL();
          this.objRequirement = new SIIV.BE.Requirement();
          this.objUserBE = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterClaimRequirement.aspx");
          this.objRequirement.i_RequirementTypeId = new int?(2);
          this.objRequirement.f_Quantity = new double?(1.0);
          this.objRequirement.v_Observations = "";
          this.objRequirement.i_InsertUserId = new int?(this.objUserBE.i_SystemUserId);
          this.objRequirement.v_Ubigeo = "";
          int num = this.Session["ProofAnswer"] != null ? (int) this.Session["ProofAnswer"] : throw new HandledException(3, "La sesión ha expirado.", "'ProofAnswer' - RegisterClaimRequirement.aspx");
          this.objRequirement.i_ProofPaymentTypeId = num != 1 ? new int?(0) : new int?(Convert.ToInt32(this.rdbProofPayment.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture));
          this.objVehicleRegistrationDetail = new VehicleRegistrationDetail();
          this.objVehicleRegistrationDetail.i_RegistrationTypeId = new int?(1);
          this.dtVehicleData = (int) this.Session["ProcessId"] != Convert.ToInt32((object) enmProccessType.Inmatriculacion) ? ((int) this.Session["ProcessId"] != Convert.ToInt32((object) enmProccessType.CambioUso) ? this.oRequirementQueriesBL.SunarpDataRead3rd(this.txtPlateNumber1.Text.Trim()) : this.oRequirementQueriesBL.SunarpDataReadChangeUse(this.txtPlateNumber1.Text.Trim())) : this.oRequirementQueriesBL.SunarpDataReadbyId(Convert.ToInt32(this.hidSunarpId.Value, (IFormatProvider) CultureInfo.CurrentCulture));
          this.objVehicleRegistrationDetail.i_RegistryOfficeId = new int?(Convert.ToInt32(this.dtVehicleData.Rows[0]["RegistryOfficeId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
          this.objVehicleRegistrationDetail.i_RegistryZoneId = new int?(Convert.ToInt32(this.dtVehicleData.Rows[0]["RegistryZoneId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
          this.objVehicleRegistrationDetail.i_VehicleCategoryId = !(this.dtVehicleData.Rows[0]["CategoryId"].ToString() != "") ? new int?() : new int?(Convert.ToInt32(this.dtVehicleData.Rows[0]["CategoryId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
          this.objVehicleRegistrationDetail.v_PlateNew = this.dtVehicleData.Rows[0]["v_platenew"].ToString();
          this.objVehicleRegistrationDetail.v_PlateOld = this.dtVehicleData.Rows[0]["v_plateold"].ToString();
          this.objVehicleRegistrationDetail.v_Brand = this.dtVehicleData.Rows[0]["v_brand"].ToString();
          this.objVehicleRegistrationDetail.v_Model = this.dtVehicleData.Rows[0]["v_model"].ToString();
          this.objVehicleRegistrationDetail.v_SerialNumber = this.dtVehicleData.Rows[0]["v_serialnumber"].ToString();
          this.objVehicleRegistrationDetail.v_CompleteNameOwner = this.dtVehicleData.Rows[0]["v_OwnerCompleteName"].ToString();
          this.objRequirementPlate = new RequirementPlate();
          this.objRequirementPlate.i_RegistrationUseTypeOldId = new int?();
          this.objRequirementPlate.i_ProcessTypeId = this.Session["ProcessId"] != null ? new int?((int) this.Session["ProcessId"]) : throw new HandledException(3, "La sesión ha expirado.", "'ProcessId' - RegisterClaimRequirement.aspx");
          if ((int) this.Session["ProcessId"] == 6)
          {
            this.ViewState["Product3rd"] = (object) this.oRequirementQueriesBL.GetProductbyTypeUse(Convert.ToInt32(this.cboUseNew.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
            this.objVehicleRegistrationDetail.i_VehicleTypeUseId = new int?(Convert.ToInt32((this.ViewState["Product3rd"] as DataTable).Rows[0]["i_VehicleUseTypeId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
            this.objVehicleRegistrationDetail.i_VehicleClassId = new int?(Convert.ToInt32((this.ViewState["Product3rd"] as DataTable).Rows[0]["i_VehicleClassId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
            this.objVehicleRegistrationDetail.i_RegistrationTypeId = new int?(Convert.ToInt32((this.ViewState["Product3rd"] as DataTable).Rows[0]["i_VehicleRegistrationId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
            this.objRequirementPlate.i_RegistrationUseTypeOldId = new int?(Convert.ToInt32(this.dtVehicleData.Rows[0]["UseTypeSunarp"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
          }
          else
          {
            this.objVehicleRegistrationDetail.i_VehicleClassId = new int?(Convert.ToInt32(this.dtVehicleData.Rows[0]["Classid"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
            this.objVehicleRegistrationDetail.i_VehicleTypeUseId = new int?(Convert.ToInt32(this.dtVehicleData.Rows[0]["UseTypeSunarp"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
            this.objVehicleRegistrationDetail.i_RegistrationTypeId = new int?(Convert.ToInt32(this.dtVehicleData.Rows[0]["i_VehicleRegistrationId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
          }
          string str1;
          if ((int) this.Session["ProcessId"] == 5)
          {
            this.objRequirementPlate.i_ProductId = new int?(Convert.ToInt32(this.dtVehicleData.Rows[0]["i_ProductId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
            this.objVehicleRegistrationDetail.v_TitleNumber = this.dtVehicleData.Rows[0]["v_titlenumber"].ToString();
            str1 = this.txtPlateNumber.Text.Trim().Replace("-", "");
          }
          else
          {
            this.objRequirementPlate.i_ProductId = (int) this.Session["ProcessId"] != 4 && (int) this.Session["ProcessId"] != 6 ? new int?(Convert.ToInt32(this.dtVehicleData.Rows[0]["i_ProductId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture)) : new int?(Convert.ToInt32((this.ViewState["Product3rd"] as DataTable).Rows[0]["i_ProductId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
            str1 = this.txtPlateNumber1.Text.Trim().Replace("-", "");
            this.objVehicleRegistrationDetail.v_TitleNumber = this.oRequirementQueriesBL.GetCorrelativeTitle();
          }
          if (num == 2)
            this.objRequirementPlate.i_ProductId = new int?(new RequirementQueriesBL().GetProductCorrespondence(this.objRequirementPlate.i_ProductId.Value, 3));
          this.objRequirementPlate.i_RequirementPlateTypeId = new int?(Convert.ToInt32((object) enmRequirementPlateType.RegularLiberada));
          this.Session["RequirementPlateType"] = (object) this.objRequirementPlate.i_RequirementPlateTypeId;
          this.objRequirementPlate.i_VehicleId = new int?(Convert.ToInt32(this.dtVehicleData.Rows[0]["i_VehicleId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
          this.objRequirementPlate.v_RegistrationCode = "";
          this.objRequirementPlate.i_ContingencyTypeId = new int?();
          this.objRequirementPlate.b_ContingencyDelivery = new bool?();
          this.objRequirementPlate.d_RegistrationDispatchDate = new DateTime?(Convert.ToDateTime(this.dtVehicleData.Rows[0]["d_DispatchDate"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
          this.objRequirementPlate.i_DeliveryPointId = new int?(Convert.ToInt32(this.cboDeliveryPoint.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
          this.objRequirementPlate.i_PlateTypeId = new int?(1);
          this.objRequirementPlate.b_PendingConfirmation = new bool?();
          this.objRequirementPlate.b_Migrated = new bool?();
          this.objRequirementContributor = new RequirementContributor();
          if (num == 1)
          {
            this.objRequirementContributor.i_DocumentTypeId = new int?(Convert.ToInt32(this.CboDocumentType.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture));
            this.objRequirementContributor.v_DocumentNumber = this.TxtDocNumberProofPaper.Text.Trim();
            this.objRequirementContributor.v_LastName = "";
            this.objRequirementContributor.v_FirstName = "";
            this.objRequirementContributor.v_CompleteName = this.txtBeneficiaryName.Text.Trim();
            this.objRequirementContributor.v_Address = this.txtAddress.Text.Trim();
          }
          else
          {
            this.objRequirementContributor.i_DocumentTypeId = new int?(0);
            this.objRequirementContributor.v_DocumentNumber = "";
            this.objRequirementContributor.v_LastName = "";
            this.objRequirementContributor.v_FirstName = "";
            this.objRequirementContributor.v_CompleteName = "-";
            this.objRequirementContributor.v_Address = "-";
          }
          this.objContributorRequester = new RequirementContributor();
          if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Regular) && (int) this.ViewState["PublicUser"] == 1)
          {
            this.objContributorRequester.i_DocumentTypeId = this.objUserBE.i_DocumentTypeId;
            this.objContributorRequester.v_DocumentNumber = this.objUserBE.v_DocumentNumber;
            this.objContributorRequester.v_LastName = this.objUserBE.v_LastName;
            this.objContributorRequester.v_FirstName = this.objUserBE.v_FirstName;
            this.objContributorRequester.v_CompleteName = this.objUserBE.v_FirstName + " " + this.objUserBE.v_LastName;
            this.objContributorRequester.v_Address = this.objUserBE.v_Address;
            this.objContributorRequester.v_AddressLocation = this.objUserBE.v_Ubigeo;
            this.objContributorRequester.v_PhoneNumber = (string) null;
            this.objContributorRequester.v_Email = this.objUserBE.v_Email;
            this.objContributorRequester.i_PersonTypeId = new int?();
          }
          else
          {
            this.objContributorRequester.i_DocumentTypeId = new int?(Convert.ToInt32(this.cboRequesterTypeDoc.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture));
            this.objContributorRequester.v_DocumentNumber = this.txtRequesterNumberDoc.Text.Trim();
            this.objContributorRequester.v_LastName = this.txtRequesterLast1.Text.Trim() + " " + this.txtRequesterLast2.Text.Trim();
            this.objContributorRequester.v_FirstName = this.txtRequesterName.Text.Trim();
            this.objContributorRequester.v_CompleteName = this.txtRequesterName.Text.Trim() + " " + this.txtRequesterLast1.Text.Trim() + " " + this.txtRequesterLast2.Text.Trim();
            this.objContributorRequester.v_AddressLocation = (string) null;
            this.objContributorRequester.v_Address = "";
            this.objContributorRequester.v_PhoneNumber = this.txtRequesterPhone.Text.Trim();
            this.objContributorRequester.v_Email = this.txtRequesterMail.Text.Trim();
            this.objContributorRequester.i_PersonTypeId = new int?(1);
          }
          this.dtOwners = this.oRequirementQueriesBL.GetOwnersByIdSunarp(Convert.ToInt32(this.hidSunarpId.Value, (IFormatProvider) CultureInfo.CurrentCulture));
          string str2 = "";
          string str3 = "";
          string str4 = "";
          string str5 = "";
          for (int index = 0; index < this.dtOwners.Rows.Count; ++index)
          {
            str2 = str2 + "/" + this.dtOwners.Rows[index]["CompleteName"].ToString();
            str3 = str3 + "/" + this.dtOwners.Rows[index]["v_DocumentTypeId"].ToString();
            str4 = str4 + "/" + this.dtOwners.Rows[index]["v_DocumentType"].ToString();
            str5 = str5 + "/" + this.dtOwners.Rows[index]["v_DocumentNumber"].ToString();
          }
          this.objRequirementPlate.v_OwnerCompleteName = str2.Substring(1, str2.Length - 1);
          this.objRequirementPlate.v_OwnerDocumentType = str3.Substring(1, str3.Length - 1);
          this.objRequirementPlate.v_OwnerDocumentDescription = str4.Substring(1, str4.Length - 1);
          this.objRequirementPlate.v_OwnerDocumentNumber = str5.Substring(1, str5.Length - 1);
          this.objPayment = new Payment();
          this.objPayment.f_PriceSale = new double?();
          this.objPayment.f_PriceTax = new double?();
          this.objPayment.f_PriceTotal = new double?();
          this.objRequirement.i_Status = new int?(1);
          this.objRequirementPlate.i_Status = new int?(1);
          this.objPayment.i_Status = new int?();
          this.objPayment.i_PaymentTypeId = new int?();
          this.objPayment.i_BankId = new int?();
          this.objPayment.v_BankOperationNumber = (string) null;
          this.objPayment.v_BankOperationUser = (string) null;
          this.objPayment.v_BankOperationTerminal = (string) null;
          this.objPayment.i_AccountId = new int?();
          this.objPayment.d_BankOperationDate = new DateTime?();
          this.objRequirementPlate.i_DataBankId = (string) null;
          if ((int) this.Session["ProcessId"] == Convert.ToInt32((object) enmProccessType.Inmatriculacion))
          {
            switch (this.oRequirementQueriesBL.ValidateExistRequirementByPlateTitle(this.dtVehicleData.Rows[0]["v_platenew"].ToString(), this.dtVehicleData.Rows[0]["v_titlenumber"].ToString()))
            {
              case -4:
                throw new HandledException(1, "EL NUMERO DE PLACA Y TITULO DETERMINAN UNA CAMBIO DE CLASE/CARACTERISTICAS DE PLACA NUEVA, SOLO ES UN REGISTRO INFORMATIVO PARA TRAMITES COMO DUPLICADO CON PLACA NUEVA, CAMBIO DE USO O DUPLICADO DE TERCERA PLACA");
              case -3:
                throw new HandledException(1, "EL NUMERO DE PLACA Y TITULO DETERMINAN UNA TRANFERENCIA DE PLACA NUEVA, SOLO ES UN REGISTRO INFORMATIVO PARA TRAMITES COMO DUPLICADO CON PLACA NUEVA, CAMBIO DE USO o DUPLICADO DE TERCERA PLACA");
              case -2:
                throw new HandledException(1, "EL NUMERO DE PLACA TERMINO UN TRAMITE DE PLACA ( ENTREGADA )");
            }
          }
          else if (this.oRequirementQueriesBL.Verify3rdPlate(this.dtVehicleData.Rows[0]["v_platenew"].ToString()) == -1)
            throw new HandledException(1, "EL NUMERO DE PLACA Y TITULO DETERMINAN UNA CAMBIO DE CLASE/CARACTERISTICAS DE PLACA NUEVA, SOLO ES UN REGISTRO INFORMATIVO PARA TRAMITES COMO DUPLICADO CON PLACA NUEVA, CAMBIO DE USO O DUPLICADO DE TERCERA PLACA");
          this.objRequirementProgramation = new RequirementProgramation();
          this.objRequirementProgramation.i_ZoneReference = new int?();
          this.objRequirementProgramation.i_DistrictReference = new int?();
          int[] numArray = this.oRequirementManagement.RequirementInsertOne(this.objRequirement, this.objVehicleRegistrationDetail, this.objRequirementPlate, this.objRequirementContributor, this.objContributorRequester, this.objPayment, this.objRequirementProgramation);
          this.oRequirementClaimManagement = new RequirementClaimManagementBL();
          this.oRequirementClaimManagement.RequirementClaimUpdateRequirementPlateId(this.txtClaimCode.Text.Trim(), Convert.ToInt32(numArray[1].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture), this.objUserBE.i_SystemUserId);
          if (Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) == "AAP")
          {
            this.oRequirementManagement = new RequirementManagementBL();
            this.oRequirementManagement.IncreaseRelatedDocumentQuantity(Convert.ToInt32(this.Request.QueryString["idDoc"], (IFormatProvider) CultureInfo.CurrentCulture), this.objUserBE.i_SystemUserId);
            this.objSpecialRequirement = new SpecialRequirement();
            this.objSpecialRequirement.i_RequirementPlateId = new int?(Convert.ToInt32(numArray[1].ToString((IFormatProvider) CultureInfo.CurrentCulture)));
            this.objSpecialRequirement.i_RelatedDocumentId = new int?(Convert.ToInt32(this.Request.QueryString["idDoc"], (IFormatProvider) CultureInfo.CurrentCulture));
            this.objSpecialRequirement.v_Observations = "";
            this.objSpecialRequirement.i_InsertUserId = new int?(this.objUserBE.i_SystemUserId);
            this.oRequirementManagement.SpecialRequirementInsert(this.objSpecialRequirement);
            this.Response.Redirect("~/Requirement/SuccessfulRegistration.aspx?RequirementId=" + numArray[0].ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&RequirementPlateId=" + numArray[1].ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&t=" + Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) + "&mre=0", false);
          }
          else
            this.Response.Redirect("~/Requirement/SuccessfulRegistration.aspx?RequirementId=" + numArray[0].ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&RequirementPlateId=" + numArray[1].ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&t=" + Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) + "&mre=0", false);
          transactionScope.Complete();
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
      try
      {
        this.redirectValidation(1);
        this.trProofType.Visible = true;
        this.trProofData.Visible = true;
        this.lblProofTitle.Text = "Seleccion de Comprobante";
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

    protected void btnNo_Click(object sender, EventArgs e)
    {
      try
      {
        this.redirectValidation(2);
        this.trProofType.Visible = false;
        this.trProofData.Visible = false;
        this.lblProofTitle.Text = "Seleccion de punto de entrega";
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
          this.CboDocumentType.Items.FindByValue("4").Enabled = true;
          this.CboDocumentType.SelectedValue = str;
          this.CboDocumentType.Enabled = false;
          this.TxtDocNumberProofPaper.Text = "";
          this.txtBeneficiaryName.Text = "";
          this.cboDeliveryPoint.SelectedIndex = 0;
        }
        else
        {
          str = "1";
          this.CboDocumentType.SelectedValue = str;
          this.CboDocumentType.Enabled = true;
          this.TxtDocNumberProofPaper.Text = "";
          this.CboDocumentType.Items.FindByValue("4").Enabled = false;
        }
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<script language='javascript'>");
        stringBuilder.Append("index2='" + str + "';");
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

    protected void Wizard1_ActiveStepChanged1(object sender, EventArgs e)
    {
      try
      {
        this.btnNext = this.GetControlFromWizard(this.Wizard1, RegisterClaimRequirement.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnNext") as Button;
        this.btnPrevious = this.GetControlFromWizard(this.Wizard1, RegisterClaimRequirement.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnPrevious") as Button;
        if (this.Wizard1.ActiveStep == this.WS_ExisteComprobante)
        {
          if (this.btnNext != null)
            this.btnNext.Visible = false;
        }
        else if (this.btnNext != null)
          this.btnNext.Visible = true;
        if (this.Wizard1.ActiveStep == this.WS_DatosSolicitante)
        {
          this.btnNext1 = this.GetControlFromWizard(this.Wizard1, RegisterClaimRequirement.WizardNavigationTempContainer.StartNavigationTemplateContainerID, "btnNext") as Button;
          this.btnPrevious1 = this.GetControlFromWizard(this.Wizard1, RegisterClaimRequirement.WizardNavigationTempContainer.StartNavigationTemplateContainerID, "btnCancel") as Button;
          if (this.btnNext1 != null)
            this.btnNext1.Visible = true;
          if (this.btnPrevious1 != null)
            this.btnPrevious1.Text = "Cancelar";
        }
        if (this.btnPrevious != null)
          this.btnPrevious.Visible = true;
        this.lblMessage.Visible = false;
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

    protected void btnJavaScriptResponse_Click(object sender, EventArgs e)
    {
      if ((int) this.Session["ClaimRegistered"] != 1)
        return;
      this.Response.Redirect("~/Requirement/BeginRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture));
    }

    public void Initialize()
    {
      try
      {
        this.getDocumentType();
        this.getProofPayment();
        this.getLocation();
        this.hidSunarpId.Value = "0";
        this.txtPlateNumber.Enabled = true;
        this.txtTitleNumber.Enabled = true;
        this.txtPlateNumber1.Enabled = true;
        this.beginTitle();
        if (Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) == "AAP")
        {
          this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_ValidacionOficio);
          this.getCodeOficeAAP();
        }
        else
          this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_ValidacionReclamo);
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

    public void getProofPayment()
    {
      try
      {
        this.oRequirementQueriesBL = new RequirementQueriesBL();
        this.rdbProofPayment.DataSource = (object) this.oRequirementQueriesBL.GetProofPaymenType();
        this.rdbProofPayment.DataTextField = "v_Description";
        this.rdbProofPayment.DataValueField = "i_ParameterId";
        this.rdbProofPayment.DataBind();
        this.rdbProofPayment.SelectedIndex = 0;
        this.CboDocumentType.SelectedValue = "4";
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void getCodeOficeAAP()
    {
      try
      {
        string empty = string.Empty;
        new RequirementClaimQueriesBL().getCodeOfficeAAP(Convert.ToInt32(this.Request.QueryString["idDoc"]), ref empty);
        this.txtOficeCode.Text = !(empty == "") ? empty : throw new HandledException(1, "EL CÓDIGO DE OFICIO AAP NO HA SIDO ENCONTRADO");
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
        this.oRequirementQueriesBL = new RequirementQueriesBL();
        this.dtDocument = this.oRequirementQueriesBL.GetDocumentType();
        this.CboDocumentType.DataSource = (object) this.dtDocument;
        this.CboDocumentType.DataTextField = "v_Description";
        this.CboDocumentType.DataValueField = "i_ParameterId";
        this.CboDocumentType.DataBind();
        this.CboDocumentType.SelectedIndex = 0;
        this.cboRequesterTypeDoc.DataSource = (object) this.dtDocument;
        this.cboRequesterTypeDoc.DataTextField = "v_Description";
        this.cboRequesterTypeDoc.DataValueField = "i_ParameterId";
        this.cboRequesterTypeDoc.DataBind();
        this.cboRequesterTypeDoc.SelectedIndex = 0;
        this.cboRequesterTypeDoc.Items.FindByValue("4").Enabled = false;
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

    public void beginTitle()
    {
      try
      {
        string str = "";
        if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Reclamo))
          str = "Registro de Solicitud de Reclamo";
        this.Page.Title = str;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void validateClaimCode()
    {
      try
      {
        switch (new RequirementClaimQueriesBL().ValidateExistClaimCode(this.txtClaimCode.Text.Trim()))
        {
          case 0:
            throw new HandledException(1, "EL CODIGO DE RECLAMO NO HA SIDO ENCONTRADO");
          case 1:
            throw new HandledException(1, "EL CODIGO DE RECLAMO NO SE ENCUENTRA APROBADO");
          default:
            this.autoSetRequester();
            this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosSolicitante);
            break;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void validateOfficeCode()
    {
      try
      {
        string str = Convert.ToString(this.Request.QueryString["idDoc"], (IFormatProvider) CultureInfo.CurrentCulture);
        switch (new RequirementClaimQueriesBL().ValidateExistOfficeCode(this.txtOficeCode.Text.Trim(), Convert.ToInt32(str)))
        {
          case -1:
            throw new HandledException(1, "EL CODIGO DE OFICIO SE ENCUENTRA ANULADO");
          case 0:
            throw new HandledException(1, "EL CODIGO DE OFICIO NO HA SIDO ENCONTRADO");
          case 1:
            throw new HandledException(1, "EL CODIGO DE OFICIO YA FUE UTILIZADO");
          default:
            this.autoSetRequester();
            this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosSolicitante);
            break;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void ValidateSunarp()
    {
      try
      {
        int? nullable1 = new int?();
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string str1 = this.txtPlateNumber.Text.Replace("-", "");
        string str2 = this.txtTitleNumber.Text.Replace("-", "");
        if (str1 == "")
          throw new HandledException(1, "POR FAVOR INGRESE SU NUMERO DE PLACA");
        if (str2 == "")
          throw new HandledException(1, "POR FAVOR INGRESE SU NUMERO DE TITULO");
        this.oRequirementQueriesBL = new RequirementQueriesBL();
        nullable1 = new int?(this.oRequirementQueriesBL.ValidateExistRequirementByPlateTitle(str1, str2));
        if (nullable1.GetValueOrDefault() == -2)
          throw new HandledException(1, "EL NUMERO DE PLACA TERMINO UN TRAMITE DE PLACA ( ENTREGADA )");
        if (nullable1.GetValueOrDefault() == -3)
          throw new HandledException(1, "EL NUMERO DE PLACA Y TITULO DETERMINAN UNA TRANFERENCIA DE PLACA NUEVA, SOLO ES UN REGISTRO INFORMATIVO PARA TRAMITES COMO DUPLICADO CON PLACA NUEVA, CAMBIO DE USO o DUPLICADO DE TERCERA PLACA");
        if (nullable1.GetValueOrDefault() == -4)
          throw new HandledException(1, "EL NUMERO DE PLACA Y TITULO DETERMINAN UNA CAMBIO DE CLASE/CARACTERISTICAS DE PLACA NUEVA, SOLO ES UN REGISTRO INFORMATIVO PARA TRAMITES COMO DUPLICADO CON PLACA NUEVA, CAMBIO DE USO O DUPLICADO DE TERCERA PLACA");
        nullable1 = new int?(this.oRequirementQueriesBL.ValidateExistSunarpByPlateTitle(str1, str2));
        if (nullable1.GetValueOrDefault() == 1)
        {
          this.lblMessage.Text = "";
          this.lblMessage.Visible = false;
          this.ShowVehicleData(str1, str2);
        }
        else
        {
          int? nullable2 = nullable1;
          int num = 0;
          if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
            throw new HandledException(1, "EL NUMERO DE PLACA Y TITULO NO COINCIDEN POR FAVOR REINTENTE");
          if (nullable1.GetValueOrDefault() == -1)
            throw new HandledException(1, "SU REGISTRO NO HA SIDO ENCONTRADO. POR FAVOR ESPERE 24 HORAS Y VUELVA A INTENTAR. SI EL PROBLEMA PERSISTE COMUNIQUESE CON SUNARP");
          if (nullable1.GetValueOrDefault() == -2)
            throw new HandledException(1, "SU REGISTRO HA SIDO ENCONTRADO. PERO EL TIPO DE TRAMITE REPORTADO NO AH SIDO HOMOLOGADO CON NUESTRO SISTEMA, POR FAVOR REINTENTE EN 48 HORAS. DISCULPE LAS MOLESTIAS");
        }
        this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosVehiculo1);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ShowVehicleData(string strPlateNumber, string strTitle)
    {
      try
      {
        this.oRequirementQueriesBL = new RequirementQueriesBL();
        this.dtVehicleData = this.oRequirementQueriesBL.SunarpDataRead(strPlateNumber, strTitle);
        this.hidSunarpId.Value = this.dtVehicleData.Rows[0][0].ToString();
        this.txtPlateNew.Text = this.dtVehicleData.Rows[0]["v_PlateNew"].ToString();
        this.txtPlateTitle1.Text = this.dtVehicleData.Rows[0]["v_TitleNumber"].ToString();
        this.txtPlateOld.Text = this.dtVehicleData.Rows[0]["v_PlateOld"].ToString();
        this.txtBrand.Text = this.dtVehicleData.Rows[0]["v_Brand"].ToString();
        this.txtModel.Text = this.dtVehicleData.Rows[0]["v_Model"].ToString();
        this.txtRegistryZone.Text = this.dtVehicleData.Rows[0]["registryZone"].ToString();
        this.txtRegistryOffice.Text = this.dtVehicleData.Rows[0]["registryOffice"].ToString();
        this.txtUseType.Text = this.dtVehicleData.Rows[0]["TypeUseDescrption"].ToString();
        this.txtProcess.Text = this.dtVehicleData.Rows[0]["ProcessType"].ToString();
        this.txtCategory.Text = this.dtVehicleData.Rows[0]["SunarpCategory"].ToString();
        this.txtProduct.Text = this.dtVehicleData.Rows[0]["v_Code"].ToString();
        this.txtPrice.Text = this.dtVehicleData.Rows[0]["f_PriceSale"].ToString();
        this.txtSerialNumber.Text = this.dtVehicleData.Rows[0]["v_SerialNumber"].ToString();
        this.txtDispatchDate.Text = Convert.ToDateTime(this.dtVehicleData.Rows[0]["d_DispatchDate"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture).ToString("dd/MM/yyyy", (IFormatProvider) CultureInfo.CurrentCulture);
        this.dtOwners = this.oRequirementQueriesBL.GetOwnersByIdSunarp(Convert.ToInt32(this.dtVehicleData.Rows[0][0].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
        this.txtOwners.Text = "";
        this.txtDocumentType.Text = "";
        this.txtDocumentNumber.Text = "";
        for (int index = 0; index < this.dtOwners.Rows.Count; ++index)
        {
          TextBox txtOwners = this.txtOwners;
          txtOwners.Text = txtOwners.Text + this.dtOwners.Rows[index]["completeName"].ToString() + "\n";
          TextBox txtDocumentType = this.txtDocumentType;
          txtDocumentType.Text = txtDocumentType.Text + this.dtOwners.Rows[index]["v_DocumentType"].ToString() + "\n";
          TextBox txtDocumentNumber = this.txtDocumentNumber;
          txtDocumentNumber.Text = txtDocumentNumber.Text + this.dtOwners.Rows[index]["v_DocumentNumber"].ToString() + "\n";
        }
        this.txtPlateNumber.Enabled = false;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void ValidatePlate()
    {
      try
      {
        if (this.hidSunarpId.Value == "0")
        {
          if (string.IsNullOrEmpty(this.txtPlateNumber1.Text))
            throw new HandledException(1, "POR FAVOR INGRESE SU NUMERO DE PLACA");
          string empty = string.Empty;
          string pstrPlateNumber = this.txtPlateNumber1.Text.Replace("-", "");
          this.oRequirementQueriesBL = new RequirementQueriesBL();
          if (this.oRequirementQueriesBL.Verify3rdPlate(pstrPlateNumber) == -1)
            throw new HandledException(1, "EL NUMERO DE PLACA INGRESADO NO TIENE REGISTRADO UN TRAMITE PREVIO DE PLACA NUEVA");
          int num = 1;
          if ((int) this.Session["ProcessId"] == Convert.ToInt32((object) enmProccessType.Duplicado3rd))
            num = this.oRequirementQueriesBL.VerifyProcess3rdPlate(pstrPlateNumber);
          if (num == 0)
            throw new HandledException(1, "LA PLACA INGRESADA PERTENECE A UNA CLASE VEHICULAR QUE NO PERMITE TERCERA PLACA");
          this.ShowVehicleData3rd();
        }
        this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosVehiculo2);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ShowVehicleData3rd()
    {
      try
      {
        this.oRequirementQueriesBL = new RequirementQueriesBL();
        this.pblUseNew.Visible = false;
        this.lblPrice.Visible = true;
        this.txtPrice3rd.Visible = true;
        if ((int) this.Session["ProcessId"] == Convert.ToInt32((object) enmProccessType.CambioUso))
        {
          this.dtVehicleData = this.oRequirementQueriesBL.SunarpDataReadChangeUse(this.txtPlateNumber1.Text.Trim());
          this.lblPrice.Visible = false;
          this.txtPrice3rd.Visible = false;
          if (this.dtVehicleData.Rows.Count > 0)
          {
            this.pblUseNew.Visible = true;
            int pintCategoryId = -1;
            if (this.dtVehicleData.Rows[0]["CategoryId"] != null && this.dtVehicleData.Rows[0]["CategoryId"].ToString() != "")
              pintCategoryId = Convert.ToInt32(this.dtVehicleData.Rows[0]["CategoryId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
            this.dtChangeUse = this.oRequirementQueriesBL.GetChangeUse(Convert.ToInt32(this.dtVehicleData.Rows[0]["UseTypeSunarp"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture), pintCategoryId);
            this.cboUseNew.DataSource = (object) this.dtChangeUse;
            this.cboUseNew.DataTextField = "v_Description";
            this.cboUseNew.DataValueField = "i_usetypetargetid";
            this.cboUseNew.DataBind();
            if (this.cboUseNew.Items.Count == 1)
              Message.SetMessage(this.lblMessage, new HandledException(1, "LA PLACA NO PUEDE REALIZAR NINGUN CAMBIO DE USO"));
          }
          else
            Message.SetMessage(this.lblMessage, new HandledException(1, "LA PLACA NO PUEDE REALIZAR NINGUN CAMBIO DE USO"));
        }
        else
          this.dtVehicleData = this.oRequirementQueriesBL.SunarpDataRead3rd(this.txtPlateNumber1.Text.Trim());
        this.hidSunarpId.Value = this.dtVehicleData.Rows[0][0].ToString();
        this.txtBrand3rd.Text = this.dtVehicleData.Rows[0]["v_Brand"].ToString();
        this.txtModel3rd.Text = this.dtVehicleData.Rows[0]["v_Model"].ToString();
        this.txtPlateNew3rd.Text = this.dtVehicleData.Rows[0]["v_PlateNew"].ToString();
        this.txtSerialNumber3rd.Text = this.dtVehicleData.Rows[0]["v_SerialNumber"].ToString();
        this.txtUseType3rd.Text = this.dtVehicleData.Rows[0]["TypeUseDescription"].ToString();
        this.txtCategory3rd.Text = this.dtVehicleData.Rows[0]["Category"].ToString();
        this.dtOwners = this.oRequirementQueriesBL.GetOwnersByIdSunarp(Convert.ToInt32(this.dtVehicleData.Rows[0][0].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
        for (int index = 0; index < this.dtOwners.Rows.Count; ++index)
        {
          TextBox txtOwner3rd = this.txtOwner3rd;
          txtOwner3rd.Text = txtOwner3rd.Text + this.dtOwners.Rows[index]["completeName"].ToString() + "\n";
        }
        if ((int) this.Session["ProcessId"] == Convert.ToInt32((object) enmProccessType.Duplicado3rd))
        {
          string empty = string.Empty;
          this.DtProduct = this.oRequirementQueriesBL.ValidateProductPrice3rd(this.txtPlateNumber1.Text.Replace("-", ""));
          this.ViewState["Product3rd"] = (object) this.DtProduct;
          this.txtPrice3rd.Text = this.DtProduct.Rows[0]["f_PriceSale"].ToString();
        }
        else
          this.txtPrice3rd.Text = this.dtVehicleData.Rows[0]["f_PriceSale"].ToString();
        this.txtPlateOld3rd.Text = this.dtVehicleData.Rows[0]["v_PlateOld"].ToString();
        this.txtPlateNumber1.Enabled = false;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void redirectVehicleData()
    {
      try
      {
        if (this.Wizard1.ActiveStep == this.WS_DatosVehiculo1)
        {
          if (this.txtOwners.Text.Trim() == "")
            throw new HandledException(1, "PROPIETARIO NO PUEDE SER VACIO");
          if (!this.chkAccept.Checked)
            throw new HandledException(1, "DEBE CONFIRMAR LA INFORMACION MOSTRADA");
        }
        if (this.Wizard1.ActiveStep == this.WS_DatosVehiculo2)
        {
          if (this.txtOwner3rd.Text.Trim() == "")
            throw new HandledException(1, "PROPIETARIO NO PUEDE SER VACIO");
          if (!this.chkAccept3rd.Checked)
            throw new HandledException(1, "DEBE CONFIRMAR LA INFORMACION MOSTRADA");
        }
        this.showOwnerData();
        this.Wizard1.MoveTo((WizardStepBase) this.WS_ExisteComprobante);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void showOwnerData()
    {
      try
      {
        this.dtOwners = new RequirementQueriesBL().GetOwnersByIdSunarp(Convert.ToInt32(this.hidSunarpId.Value, (IFormatProvider) CultureInfo.CurrentCulture));
        this.txtBeneficiaryName.Text = this.dtOwners.Rows[0]["CompleteName"].ToString();
        int int32 = Convert.ToInt32(this.dtOwners.Rows[0]["v_DocumentTypeId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        if (int32 == 4)
        {
          this.rdbProofPayment.SelectedIndex = 0;
          this.CboDocumentType.SelectedIndex = Convert.ToInt32(this.CboDocumentType.Items.FindByValue(int32.ToString()).Value);
          this.CboDocumentType.Enabled = false;
          this.TxtDocNumberProofPaper.Text = this.dtOwners.Rows[0]["v_DocumentNumber"].ToString();
        }
        else
        {
          this.rdbProofPayment.SelectedIndex = 1;
          this.CboDocumentType.Enabled = true;
          this.CboDocumentType.Items.FindByValue("4").Enabled = false;
          this.CboDocumentType.SelectedIndex = 0;
          if (int32 == 1)
          {
            this.CboDocumentType.SelectedIndex = Convert.ToInt32(this.CboDocumentType.Items.FindByValue(int32.ToString()).Value);
            this.TxtDocNumberProofPaper.Text = this.dtOwners.Rows[0]["v_DocumentNumber"].ToString();
          }
        }
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<script language='javascript'>");
        stringBuilder.Append("index2='" + int32.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "';");
        stringBuilder.Append("</script>");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "JSCR", stringBuilder.ToString(), false);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private bool Validate3rdData()
    {
      try
      {
        if ((int) this.Session["ProcessId"] == Convert.ToInt32((object) enmProccessType.CambioUso) && this.cboUseNew.SelectedIndex == 0)
          throw new HandledException(1, "DEBE SELECCIONAR UN NUEVO USO");
        this.lblMessage.Text = "";
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private bool ValidateProofPaymentData()
    {
      try
      {
        bool flag = true;
        if ((int) this.Session["ProofAnswer"] == 1)
        {
          if (this.txtBeneficiaryName.Text == "")
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "INGRESE NOMBRES O RAZON SOCIAL DEL COMPROBANTE"));
            flag = false;
          }
          else if (this.CboDocumentType.DataTextField == "")
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "SELECCIONE TIPO DE DOCUMENTO DE IDENTIDAD"));
            flag = false;
          }
          else if (this.CboDocumentType.SelectedIndex == 0)
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "SELECCIONE TIPO DE DOCUMENTO DE IDENTIDAD"));
            flag = false;
          }
          else if (this.TxtDocNumberProofPaper.Text.Trim() == "")
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "INGRESE NUMERO DE DOCUMENTO DEL COMPROBANTE"));
            flag = false;
          }
          else if (this.CboDocumentType.SelectedValue == "1" && this.TxtDocNumberProofPaper.Text.Trim().Length < 8)
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "DNI DEBE TENER 8 DIGITOS"));
            flag = false;
          }
          else if (this.CboDocumentType.SelectedValue == "4" && this.TxtDocNumberProofPaper.Text.Trim().Length < 11)
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "RUC INGRESADO NO ES VALIDO LONGITUD INCORRECTA"));
            flag = false;
          }
          else if (this.CboDocumentType.SelectedValue == "4" && this.TxtDocNumberProofPaper.Text.Trim().Substring(0, 1) != "1" && this.TxtDocNumberProofPaper.Text.Trim().Substring(0, 1) != "2")
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "RUC INGRESADO NO ES VALIDO DEBE INICIARCE EN 1 Ó 2"));
            flag = false;
          }
          else if (this.CboDocumentType.SelectedValue == "4" && !this.ValidateRuc(this.TxtDocNumberProofPaper.Text.Trim()))
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "LA ESTRUCTURA DEL NUMERO DE R.U.C. INGRESADO NO ES CORRECTO"));
            flag = false;
          }
          else if (this.txtDocumentNumber.Text.Trim() == "20101973922")
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "NO SE PUEDE REALIZAR TRAMITES CON ESTE NUMERO DE R.U.C."));
            flag = false;
          }
          else if (!this.RestrictionRazonSocial())
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "ESTA RESTRINGIDA LA CREACION DE TRAMITES CON ESTA RAZON SOCIAL"));
            flag = false;
          }
        }
        if (this.cboDeliveryPoint.SelectedIndex == 0)
        {
          Message.SetMessage(this.lblMessage, new HandledException(1, "SELECCIONE PUNTO DE ENTREGA"));
          flag = false;
        }
        return flag;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private bool ValidateRuc(string rucAValidar)
    {
      try
      {
        int num = 11 - (int.Parse(rucAValidar.Substring(0, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 5 + int.Parse(rucAValidar.Substring(1, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 4 + int.Parse(rucAValidar.Substring(2, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 3 + int.Parse(rucAValidar.Substring(3, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 2 + int.Parse(rucAValidar.Substring(4, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 7 + int.Parse(rucAValidar.Substring(5, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 6 + int.Parse(rucAValidar.Substring(6, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 5 + int.Parse(rucAValidar.Substring(7, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 4 + int.Parse(rucAValidar.Substring(8, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 3 + int.Parse(rucAValidar.Substring(9, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 2) % 11;
        return (int.Parse(rucAValidar.Length.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture) != 11 ? 10 : int.Parse(rucAValidar.Substring(10, 1), (IFormatProvider) CultureInfo.CurrentCulture)) == (num != 10 ? 0 : 0) + (num != 11 ? 0 : 1) + (num >= 10 ? 0 : num);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private bool RestrictionRazonSocial()
    {
      try
      {
        string str = this.txtBeneficiaryName.Text.ToUpper(CultureInfo.CurrentCulture).Replace("À", "A").Replace("Á", "A").Replace("È", "E").Replace("É", "E").Replace("Ì", "I").Replace("Í", "I").Replace("Ó", "O").Replace("Ó", "O").Replace("Ù", "U").Replace("Ú", "U");
        return !str.Contains("ASOCIACION") || !str.Contains("AUTOMOTRIZ") || !str.Contains("PERU");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void showFinalData(int pintProcessType)
    {
      try
      {
        this.oRequirementQueriesBL = new RequirementQueriesBL();
        switch (pintProcessType)
        {
          case 1:
            this.lblTitleRequirement.Text = "Solicitud Duplicado de placa de Rodaje con Nuevo Modelo de Placa ( Datos Consignados )";
            this.TagUseNew.Visible = false;
            goto default;
          case 4:
            this.lblTitleRequirement.Text = "Solicitud Duplicado de Tercera Placa ( Datos Consignados )";
            this.TagUseNew.Visible = false;
            goto default;
          case 5:
            this.lblTitleRequirement.Text = "Solicitud Nueva Placa Única Nacional de Rodaje ( Datos Consignados )";
            this.TagUseNew.Visible = false;
            this.lblPlateNumber.Text = this.txtPlateNew.Text;
            this.lblPlateNumberOld.Text = this.txtPlateOld.Text;
            this.lblBrand.Text = this.txtBrand.Text;
            this.lblModel.Text = this.txtModel.Text;
            this.lblCategory.Text = this.txtCategory.Text;
            this.lblUseType.Text = this.txtUseType.Text;
            this.lblSerialNumber.Text = this.txtSerialNumber.Text;
            this.lblOwner.Text = this.txtOwners.Text;
            this.lblOwnerDocumentType.Text = this.txtDocumentType.Text;
            this.lblOwnerDocumentNumber.Text = this.txtDocumentNumber.Text;
            break;
          case 6:
            this.lblTitleRequirement.Text = "Solicitud Cambio de Uso con Nuevo Modelo de Placa ( Datos Consignados )";
            this.TagUseNew.Visible = true;
            this.lblUseNew.Text = this.cboUseNew.SelectedItem.ToString();
            goto default;
          default:
            this.lblPlateNumber.Text = this.txtPlateNew3rd.Text;
            this.lblPlateNumberOld.Text = this.txtPlateOld3rd.Text;
            this.lblBrand.Text = this.txtBrand3rd.Text;
            this.lblModel.Text = this.txtModel3rd.Text;
            this.lblCategory.Text = this.txtCategory3rd.Text;
            this.lblUseType.Text = this.txtUseType3rd.Text;
            this.lblSerialNumber.Text = this.txtSerialNumber3rd.Text;
            this.dtOwners = this.oRequirementQueriesBL.GetOwnersByIdSunarp(Convert.ToInt32(this.hidSunarpId.Value, (IFormatProvider) CultureInfo.CurrentCulture));
            this.lblOwner.Text = "";
            this.lblOwnerDocumentType.Text = "";
            this.lblOwnerDocumentNumber.Text = "";
            for (int index = 0; index < this.dtOwners.Rows.Count; ++index)
            {
              Label lblOwner = this.lblOwner;
              lblOwner.Text = lblOwner.Text + this.dtOwners.Rows[index]["completeName"].ToString() + "<br/>";
              Label ownerDocumentType = this.lblOwnerDocumentType;
              ownerDocumentType.Text = ownerDocumentType.Text + this.dtOwners.Rows[index]["v_DocumentType"].ToString() + "<br/>";
              Label ownerDocumentNumber = this.lblOwnerDocumentNumber;
              ownerDocumentNumber.Text = ownerDocumentNumber.Text + this.dtOwners.Rows[index]["v_DocumentNumber"].ToString() + "<br/>";
            }
            break;
        }
        if ((int) this.Session["ProofAnswer"] == 1)
        {
          this.lblProofPaymentType.Text = this.rdbProofPayment.SelectedItem.Text;
          this.lblBeneficiary.Text = this.txtBeneficiaryName.Text;
          this.lblBeneficiaryDumentType.Text = this.CboDocumentType.SelectedItem.Text;
          this.lblbeneficiaryDocumentNumber.Text = this.TxtDocNumberProofPaper.Text;
          this.lblAddress.Text = this.txtAddress.Text;
        }
        else
        {
          this.lblProofPaymentType.Text = "-";
          this.lblBeneficiary.Text = "-";
          this.lblBeneficiaryDumentType.Text = "-";
          this.lblbeneficiaryDocumentNumber.Text = "-";
          this.lblAddress.Text = "-";
        }
        this.lblDeliveryPoint.Text = this.cboDeliveryPoint.SelectedItem.Text;
        this.lblDeliveryPointAddress.Text = this.oRequirementQueriesBL.GetLocation(this.cboDeliveryPoint.SelectedItem.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture), "", "").Rows[1]["v_Address"].ToString();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void redirectValidation(int intAnswer)
    {
      this.Session["ProofAnswer"] = (object) intAnswer;
      this.Wizard1.MoveTo((WizardStepBase) this.WS_SeleccionComprobante);
    }

    private Control GetControlFromWizard(
      Wizard wizard,
      RegisterClaimRequirement.WizardNavigationTempContainer wzdTemplate,
      string controlName)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append((object) wzdTemplate);
      stringBuilder.Append("$");
      stringBuilder.Append(controlName);
      return wizard.FindControl(stringBuilder.ToString());
    }

    public void validateRequesterData()
    {
      try
      {
        if (this.cboRequesterTypeDoc.SelectedIndex == 0)
          throw new HandledException(1, "SELECCIONE TIPO DE DOCUMENTO");
        if (this.txtRequesterNumberDoc.Text.Trim() == "")
          throw new HandledException(1, "INGRESE NUMERO DE DOCUMENTO DEL REPRESENTADO");
        if (this.txtRequesterPhone.Text.Trim() == "")
          throw new HandledException(1, "INGRESE NUMERO DE TELEFONO DEL REPRESENTADO");
        if (this.cboRequesterTypeDoc.SelectedValue == "1" && this.txtRequesterNumberDoc.Text.Trim().Length < 8)
          throw new HandledException(1, "DNI DEBE TENER 8 DIGITOS");
        if (this.cboRequesterTypeDoc.SelectedValue == "4" && this.txtRequesterNumberDoc.Text.Trim().Length < 11)
          throw new HandledException(1, "RUC INGRESADO NO ES VALIDO LONGITUD INCORRECTA");
        if (this.cboRequesterTypeDoc.SelectedValue == "4" && this.txtRequesterNumberDoc.Text.Trim().Substring(0, 1) != "1" && this.txtRequesterNumberDoc.Text.Trim().Substring(0, 1) != "2")
          throw new HandledException(1, "RUC INGRESADO NO ES VALIDO DEBE INICIARCE EN 1 Ó 2");
        if (this.cboRequesterTypeDoc.SelectedValue == "4" && !this.ValidateRuc(this.txtRequesterNumberDoc.Text.Trim()))
          throw new HandledException(1, "LA ESTRUCTURA DEL NUMERO DE R.U.C. INGRESADO NO ES CORRECTO");
        if ((int) this.Session["ProcessId"] == Convert.ToInt32((object) enmProccessType.Inmatriculacion))
          this.Wizard1.MoveTo((WizardStepBase) this.WS_ValidacionOrden);
        else
          this.Wizard1.MoveTo((WizardStepBase) this.WS_ValidacionPlaca);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void autoSetRequester()
    {
      try
      {
        this.objUserBE = new SystemUser();
        this.oRequirementQueriesBL = new RequirementQueriesBL();
        this.objUserBE = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterClaimRequirement.aspx");
        string userExtendedAction = this.oRequirementQueriesBL.GetSystemUserExtendedAction(this.objUserBE.i_SystemUserId, 1);
        if (!(userExtendedAction != ""))
          return;
        if (Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("3", StringComparison.CurrentCulture))) == "3")
        {
          this.txtRequesterName.Text = this.objUserBE.v_FirstName;
          string[] strArray = this.objUserBE.v_LastName.Trim().Split(' ');
          this.txtRequesterLast1.Text = "-";
          this.txtRequesterLast2.Text = "-";
          if (strArray[0] != "")
            this.txtRequesterLast1.Text = strArray[0];
          if (strArray.Length > 1)
          {
            for (int index = 1; index < strArray.Length; ++index)
              this.txtRequesterLast2.Text += strArray[index];
          }
          this.txtRequesterMail.Text = this.objUserBE.v_Email.Trim();
          this.txtRequesterPhone.Text = this.objUserBE.v_Telephone.Trim();
          this.cboRequesterTypeDoc.SelectedValue = this.objUserBE.i_DocumentTypeId.ToString();
          this.txtRequesterNumberDoc.Text = this.objUserBE.v_DocumentNumber.Trim();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void CreatePopUp(string url, string pstrtitle, string width, string height)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) url, (object) pstrtitle, (object) (width + "px"), (object) (height + "px"));
      this.Session["ClaimRegistered"] = (object) 0;
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }

    public enum WizardNavigationTempContainer
    {
      StartNavigationTemplateContainerID = 1,
      StepNavigationTemplateContainerID = 2,
      FinishNavigationTemplateContainerID = 3,
    }
  }
}
