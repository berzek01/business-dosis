// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Requirement.RegisterRequirement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using SIIV.BE;
using SIIV.Claim.BL;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.Product.BL;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using SIIV.WebApp.Claim;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Requirement
{
  public class RegisterRequirement : Page
  {
    private SIIV.BE.Requirement objRequirement;
    private SystemUser objUserBE;
    private RequirementContributor objRequirementContributor;
    private VehicleRegistrationDetail objVehicleRegistrationDetail;
    private RequirementPlate objRequirementPlate;
    private RequirementContributor objContributorRequester;
    private Payment objPayment;
    private DataBankManagementBL objDataBankManagement;
    private RequirementProgramation objRequirementProgramation;
    private double ComisionCanalAtencion = Convert.ToDouble(ConfigurationManager.AppSettings[nameof (ComisionCanalAtencion)]);
    private Button btnClaim;
    private Button btnNext;
    private Button btnNext1;
    private Button btnPrevious;
    private Button btnPrevious1;
    private Button btnFinish;
    private Button btnCancel;
    private bool isRegisterCall = false;
    private int StatusCallCenter;
    private int RucValidation;
    private int TypeApplicant;
    private int RegistrationReason;
    private string prrof;
    protected UpdatePanel updatePanel;
    protected HiddenField hdiPrice;
    protected Wizard Wizard1;
    protected WizardStep WS_Applicant;
    protected RadioButtonList rblPropApoderado;
    protected Panel pnlMensajePropietario;
    protected Label lblMensajePropietario;
    protected Button btnSiPropietario;
    protected Button btnNoPropietario;
    protected Panel pnlMensajeApoderado;
    protected Label lblMensajeApoderado;
    protected WizardStep WS_DatosSolicitante;
    protected HtmlTableRow TagName1;
    protected TextBox txtRequesterName;
    protected RequiredFieldValidator RequiredFieldValidator4;
    protected ValidatorCalloutExtender ValidatorCalloutExtender4;
    protected Label lblNameReq;
    protected HtmlTableRow TagName2;
    protected TextBox txtRequesterLast1;
    protected TextBoxWatermarkExtender txtRequesterLast1_TextBoxWatermarkExtender;
    protected Label lblApatReq;
    protected TextBox txtRequesterLast2;
    protected TextBoxWatermarkExtender TextBoxWatermarkExtender1;
    protected Label lblAmatReq;
    protected RequiredFieldValidator RequiredFieldValidator5;
    protected ValidatorCalloutExtender ValidatorCalloutExtender5;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected ValidatorCalloutExtender ValidatorCalloutExtender1;
    protected DropDownList cboRequesterTypeDoc;
    protected TextBox txtRequesterNumberDoc;
    protected RequiredFieldValidator RequiredFieldValidator8;
    protected ValidatorCalloutExtender ValidatorCalloutExtender8;
    protected Label lblNrDocReq;
    protected TextBox txtRequesterPhone;
    protected Label lblTelReq;
    protected TextBox txtRequesterMail;
    protected FilteredTextBoxExtender txtRequesterMail_FilteredTextBoxExtender1;
    protected RegularExpressionValidator RegularExpressionValidator1;
    protected ValidatorCalloutExtender RegularExpressionValidator1_ValidatorCalloutExtender;
    protected Panel PanelJuridicos;
    protected Panel tblRegistro;
    protected RadioButtonList rblDenuncia;
    protected RequiredFieldValidator rfvDenuncia;
    protected ValidatorCalloutExtender rfvDenuncia_ValidatorCalloutExtender;
    protected Panel PanelDenuncia;
    protected Panel PanelIntercambio;
    protected Panel Panel1;
    protected Panel PanelDenunciaIntercambio;
    protected Panel tblDenuncia;
    protected HtmlTableRow Tr5;
    protected TextBox TextBoxFechaHoraImpresion;
    protected RequiredFieldValidator RequiredFieldValidatorFechaHora;
    protected ValidatorCalloutExtender ValidatorCalloutExtenderFechaHora;
    protected TextBox TextBoxNumeroOrden;
    protected RequiredFieldValidator RequiredFieldValidatorNumeroOrden;
    protected ValidatorCalloutExtender ValidatorCalloutExtenderNumeroOrden;
    protected TextBox TextBoxClave;
    protected RequiredFieldValidator RequiredFieldValidatorClave;
    protected ValidatorCalloutExtender ValidatorCalloutExtenderClave;
    protected WizardStep WS_ValidacionOrden;
    protected TextBox txtPlateNumber;
    protected FilteredTextBoxExtender txtPlateNumber_FilteredTextBoxExtender;
    protected RequiredFieldValidator RequiredFieldValidator14;
    protected ValidatorCalloutExtender ValidatorCalloutExtender14;
    protected TextBox txtTitleNumber;
    protected RequiredFieldValidator RequiredFieldValidator15;
    protected ValidatorCalloutExtender ValidatorCalloutExtender15;
    protected FilteredTextBoxExtender txtTitleNumber_FilteredTextBoxExtender;
    protected Image Image1;
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
    protected TextBox txtVehicleType;
    protected TextBox txtMotorNumber;
    protected TextBox txtRegistryZone;
    protected TextBox txtRegistryOffice;
    protected TextBox txtCategory;
    protected TextBox txtProcess;
    protected TextBox txtProduct;
    protected TextBox txtPrice;
    protected CheckBox chkAccept;
    protected CustomValidator CustomValidator2;
    protected Label lblData1Message2;
    protected WizardStep WS_SeleccionComprobante;
    protected RadioButtonList rdbProofPayment;
    protected HtmlTableCell TagDatosComprobante;
    protected DropDownList CboDocumentType;
    protected TextBox TxtDocNumberProofPaper;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender2;
    protected Label lblMessage1;
    protected TextBox txtBeneficiaryName;
    protected FilteredTextBoxExtender txtBeneficiaryName_FilteredTextBoxExtender;
    protected TextBox txtAddress;
    protected FilteredTextBoxExtender txtAddress_FilteredTextBoxExtender;
    protected TextBox txtBeneficiaryMail;
    protected CheckBox CheckhasnotEmail;
    protected DropDownList cboDeliveryPoint;
    protected Button btnReturnPopupConfirmation;
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
    protected HtmlTableCell Td1;
    protected HtmlTableCell Td2;
    protected Label lblUseNew;
    protected Label lblOwner;
    protected Label lblOwnerDocumentType;
    protected Label lblOwnerDocumentNumber;
    protected Label lblEmail;
    protected Label lblProofPaymentType;
    protected Label lblBeneficiary;
    protected Label lblAddress;
    protected Label lblBeneficiaryDumentType;
    protected Label lblbeneficiaryDocumentNumber;
    protected Label lblDeliveryPoint;
    protected Label lblDeliveryPointAddress;
    protected CheckBox chkConfirmPreview;
    protected HtmlGenericControl divDatosDelivery;
    protected Label lblEntregaPropietario;
    protected Label Label4;
    protected Label lblPriceDeliver;
    protected Label Label6;
    protected CheckBox chkYes;
    protected CheckBox chkNo;
    protected Label lblCobertura;
    protected Button btnCobertura;
    protected HtmlTableRow trAdditionalProducts;
    protected HtmlTableCell Td3;
    protected HtmlGenericControl divImgPortaplaca;
    protected HtmlGenericControl divImgPortaPlacaMoto;
    protected HtmlGenericControl divImgDelivery;
    protected HtmlGenericControl contAdd;
    protected CheckBox chkPorta;
    protected HtmlTableRow trTorni;
    protected CheckBox chkTorni;
    protected HtmlTableRow tr4;
    protected CheckBox chkServi3;
    protected Label lblServiceDescription1;
    protected Label lblPriceCallCenter1;
    protected HtmlGenericControl contAddMoto;
    protected CheckBox chkPortaMoto;
    protected HtmlTableRow tr2;
    protected CheckBox chkServi;
    protected Label lblServiceDescription;
    protected Label lblPriceCallCenter;
    protected HtmlGenericControl DivDelivery;
    protected DropDownList wddDeliveryDescription;
    protected TextBox txtDeliveryDescription;
    protected DropDownList wddDeliveryDescription2;
    protected TextBox txtDeliveryDescription2;
    protected DropDownList wddDistrict;
    protected TextBox txtReference;
    protected TextBox txtMailDelivery;
    protected FilteredTextBoxExtender FilteredTextBoxExtender2;
    protected RegularExpressionValidator RegularExpressionValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender7;
    protected TextBox txtTelephoneDelivery;
    protected TextBox txtTelephoneMovilDelivery;
    protected MaskedEditExtender MaskedEditExtender3;
    protected Label lblProductDescription;
    protected Label lblPriceProducto;
    protected Label lblPriceDelivery;
    protected HtmlTableRow tr3;
    protected Label lblServiceDescription2;
    protected Label lblPriceCallCenter2;
    protected HtmlGenericControl Div2;
    protected TextBox txtTotal;
    protected Label lblmensajepreview2;
    protected HtmlTableRow CheckDelivery;
    protected Label LblDelivery;
    protected CheckBox chkDelivery1;
    protected CustomValidator CustomValidator3;
    protected WizardStep WS_ValidacionPlaca;
    protected TextBox txtPlateNumber1;
    protected FilteredTextBoxExtender FilteredTextBoxExtender1;
    protected RequiredFieldValidator RequiredFieldValidator16;
    protected ValidatorCalloutExtender ValidatorCalloutExtender16;
    protected TextBox txtVinSerieFirst;
    protected TextBox txtVinSerie;
    protected RequiredFieldValidator RequiredFieldValidator17;
    protected ValidatorCalloutExtender ValidatorCalloutExtender17;
    protected FilteredTextBoxExtender FilteredTextBoxExtender3;
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
    protected HtmlTableRow trUseNew;
    protected TextBox txtUseNew;
    protected TextBox txtOwner3rd;
    protected CheckBox chkAccept3rd;
    protected CustomValidator CustomValidator1;
    protected Label lblData2Message2;
    protected Panel PanelValidationData;
    protected Literal litDocumentNumber;
    protected WizardStep WS_SeleccionPago;
    protected Label Label3;
    protected Button btnYes;
    protected Button btnNo;
    protected WizardStep WS_AcreditacionPago;
    protected DropDownList cboBank;
    protected RadioButtonList rdlPaymentType;
    protected Fecha cboPaymentDate;
    protected HtmlTableRow TagTerminal;
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
    protected WizardStep WS_SeleccionMedioPago;
    protected Panel PanelDuplicado;
    protected HtmlGenericControl divTipoPago;
    protected RadioButtonList rbTypePayment;
    protected HtmlGenericControl divVisa;
    protected HtmlGenericControl divVisa2;
    protected Image Image7;
    protected CheckBox chkVisa;
    protected HyperLink PoliticaDevolucion;
    protected Label lblComision;
    protected Label lblTotalAPagar;
    protected Label lblMessage;
    protected Button BtnValidate;
    protected Button BtnValidateRuc;
    protected Button btnJavaScriptResponse;
    protected Button btnJavaScriptCloseVISA;
    protected HiddenField hidSunarpId;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.txtBeneficiaryName.Enabled = true;
      this.lblMessage.Visible = false;
      this.txtTotal.Style.Add("TextAlign", "center");
      SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
      this.StatusCallCenter = int.Parse(ConfigurationManager.AppSettings["ServiceCallCenter"]);
      this.RucValidation = int.Parse(ConfigurationManager.AppSettings["RucValidation"]);
      if (this.RucValidation == 1 && this.CboDocumentType.SelectedValue == "4" && this.ViewState["ValidationRUC"] != null && (int) this.ViewState["ValidationRUC"] == 1)
        this.txtBeneficiaryName.Enabled = false;
      string str = systemUser.i_RoleConfigId.ToString();
      this.isRegisterCall = ((IEnumerable<string>) new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.RoleCall.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      }).Rows[0]["v_Value"].ToString().Split('|')).Contains<string>(str);
      if (this.StatusCallCenter == 0)
        this.isRegisterCall = false;
      if (this.IsPostBack)
        return;
      this.setScriptEvents();
      this.Initialize();
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
          this.lblMessage1.Text = "";
          this.txtBeneficiaryName.Text = "";
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

    protected void ReturnPage(object sender, EventArgs e)
    {
      try
      {
        if (this.Session["RequirementPlateType"] == null)
          return;
        if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Regular))
        {
          if ((int) this.ViewState["PublicUser"] == 0)
          {
            if (this.Wizard1.ActiveStep == this.WS_SeleccionPago)
              this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosSolicitante);
            else
              this.Response.Redirect("~/Requirement/BeginRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"]));
          }
          else
            this.Response.Redirect("~/Requirement/BeginRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"]));
        }
        else if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Premium) || (int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.NotariaLiberada))
        {
          if (this.Wizard1.ActiveStep == this.WS_SeleccionPago)
            this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosSolicitante);
          else
            this.Response.Redirect("~/Requirement/BeginRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"]));
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

    protected void MovePrevious(object sender, EventArgs e)
    {
      try
      {
        if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.Inmatriculacion) || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 2 || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 3 || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 7)
        {
          if (this.Wizard1.ActiveStep == this.WS_DatosFinales)
          {
            if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Premium))
              this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosVehiculo1);
            else
              this.Wizard1.MoveTo((WizardStepBase) this.WS_SeleccionComprobante);
          }
          else if (this.Wizard1.ActiveStep == this.WS_SeleccionComprobante)
            this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosVehiculo1);
          else if (this.Wizard1.ActiveStep == this.WS_DatosVehiculo1)
          {
            this.Wizard1.MoveTo((WizardStepBase) this.WS_ValidacionOrden);
          }
          else
          {
            if (this.Wizard1.ActiveStep != this.WS_ValidacionOrden)
              return;
            this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosSolicitante);
          }
        }
        else if (this.Wizard1.ActiveStep == this.WS_DatosFinales)
        {
          if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Premium))
            this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosVehiculo2);
          else
            this.Wizard1.MoveTo((WizardStepBase) this.WS_SeleccionComprobante);
        }
        else if (this.Wizard1.ActiveStep == this.WS_SeleccionComprobante)
          this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosVehiculo2);
        else if (this.Wizard1.ActiveStep == this.WS_DatosVehiculo2)
        {
          this.PanelValidationData.Visible = false;
          this.Wizard1.MoveTo((WizardStepBase) this.WS_ValidacionPlaca);
          this.hidSunarpId.Value = "0";
        }
        else if (this.Wizard1.ActiveStep == this.WS_ValidacionPlaca)
          this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosSolicitante);
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

    protected void MoveLastPrevious(object sender, EventArgs e)
    {
      try
      {
        if (this.Wizard1.ActiveStep != this.WS_SeleccionMedioPago)
          return;
        if (this.ViewState["ids"] != null)
          this.ViewState.Remove("ids");
        if (this.Session["ETicket"] != null)
          this.Session.Remove("Eticket");
        this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosFinales);
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

    protected void MoveNext(object sender, EventArgs e)
    {
      try
      {
        if (this.Wizard1.ActiveStep == this.WS_Applicant)
          this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosSolicitante);
        else if (this.Wizard1.ActiveStep == this.WS_DatosSolicitante)
        {
          this.validateRequesterData();
          this.Session["EmailSoli"] = (object) this.txtRequesterMail.Text.Trim();
        }
        else if (this.Wizard1.ActiveStep == this.WS_ValidacionOrden)
        {
          string str = this.txtPlateNumber.Text.Replace("-", "");
          string strTitle = this.txtTitleNumber.Text.Replace("-", "");
          this.txtVinSerie.Text.Trim().ToUpper();
          if (str.Length <= 0 || strTitle.Length <= 0)
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_o_Titulo_Vacio);
          string ErrorMessage1 = str.Length >= 6 ? this.ValidateExistRequirement(str, strTitle) : throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Longitud6_Incorrecta);
          if (ErrorMessage1.Length != 0)
            throw new HandledException(1, ErrorMessage1);
          string ErrorMessage2 = this.ValidateSunarp(str, strTitle);
          if (ErrorMessage2.Length != 0)
            throw new HandledException(1, ErrorMessage2);
          this.ShowVehicleData(str, strTitle);
          if ((int) this.Session["PaymentAnswer"] == 1 && (int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Regular))
            this.Wizard1.MoveTo((WizardStepBase) this.WS_AcreditacionPago);
          else
            this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosVehiculo1);
        }
        else if (this.Wizard1.ActiveStep == this.WS_ValidacionPlaca)
          this.ValidatePlate();
        else if (this.Wizard1.ActiveStep == this.WS_AcreditacionPago)
          this.redirectPaymentAcreditation();
        else if (this.Wizard1.ActiveStep == this.WS_DatosVehiculo1)
        {
          this.txtBeneficiaryMail.Text = this.txtRequesterMail.Text.Trim();
          this.redirectVehicleData();
        }
        else if (this.Wizard1.ActiveStep == this.WS_DatosVehiculo2)
        {
          if (Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 1)
          {
            if (this.rblPropApoderado.SelectedValue == "Propietario")
            {
              bool boolean1 = Convert.ToBoolean(this.Session["HasLegalDocumentType"]);
              bool boolean2 = Convert.ToBoolean(this.Session["FoundOwner"]);
              if (boolean1)
              {
                this.Validate3rdData();
                this.redirectVehicleData();
              }
              else if (!boolean2)
              {
                this.litDocumentNumber.Text = this.txtRequesterNumberDoc.Text;
                this.PanelValidationData.Visible = true;
              }
              else
              {
                this.Validate3rdData();
                this.redirectVehicleData();
              }
            }
            else
            {
              this.Validate3rdData();
              this.redirectVehicleData();
            }
          }
          else
          {
            this.Validate3rdData();
            this.redirectVehicleData();
          }
        }
        else if (this.Wizard1.ActiveStep == this.WS_SeleccionComprobante)
        {
          this.ValidateProofPaymentData();
          new ValidatorRegularExpressionProofPaymentBL().ProofPaymentData(this.CboDocumentType.Text.Trim(), this.TxtDocNumberProofPaper.Text.Trim(), this.txtBeneficiaryName.Text.Trim(), this.txtAddress.Text.Trim(), this.txtBeneficiaryMail.Text.Trim());
          if (this.isRegisterCall)
          {
            DataTable priceServiceDelivery = new RequirementQueriesBL().GetPriceServiceDelivery(471, 0);
            if (Convert.ToInt16(this.ViewState["VehicleClassId"].ToString()) != (short) 5)
            {
              this.chkServi3.Checked = true;
              this.chkServi3.Enabled = false;
              this.tr4.Visible = true;
              this.lblPriceCallCenter1.Text = priceServiceDelivery.Rows[0]["f_PriceProduct"].ToString();
              this.lblServiceDescription1.Text = priceServiceDelivery.Rows[0]["v_Description"].ToString();
            }
            else
            {
              this.chkServi.Checked = true;
              this.chkServi.Enabled = false;
              this.tr2.Visible = true;
              this.lblPriceCallCenter.Text = priceServiceDelivery.Rows[0]["f_PriceProduct"].ToString();
              this.lblServiceDescription.Text = priceServiceDelivery.Rows[0]["v_Description"].ToString();
            }
          }
          this.showFinalData((int) Convert.ToInt16(this.Session["ProcessId"].ToString()), (int) Convert.ToInt16(this.ViewState["VehicleClassId"].ToString()));
          this.CleanTextDelivery();
          this.DivDelivery.Visible = false;
          this.CheckDelivery.Visible = false;
          int int32 = Convert.ToInt32(this.cboDeliveryPoint.SelectedValue);
          if (int.Parse(ConfigurationManager.AppSettings["LocationAnnouncement"]) == 1)
          {
            if (int32 != 14)
            {
              string empty = string.Empty;
              this.CreatePopUpServer("SIIV - Anuncio", "../../UserControls/PopupConfirmationRequirement.aspx?", "370px", "250px");
            }
            else
              this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosFinales);
          }
          else
            this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosFinales);
        }
        else if (this.Wizard1.ActiveStep == this.WS_DatosFinales)
        {
          if (!this.chkConfirmPreview.Checked)
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Confirma_Accion);
          if (this.divDatosDelivery.Visible && !this.chkYes.Checked && !this.chkNo.Checked)
            throw new HandledException(1, "Debe seleccionar si desea o no el Servicio de Delivery");
          if (this.chkYes.Checked && !this.chkDelivery1.Checked)
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Confirma_Accion_Delivery);
          if (this.chkYes.Checked)
          {
            if (this.txtDeliveryDescription.Text.Trim() == "" || this.txtDeliveryDescription2.Text.Trim() == "" || this.wddDistrict.SelectedIndex == 0)
              throw new HandledException(1, "Debe ingresar todos los datos de la dirección");
            if (this.txtMailDelivery.Text.Trim() == "")
              throw new HandledException(1, "Debe ingresar el correo para el Servicio Delivery");
            if (this.txtTelephoneDelivery.Text.Trim() == "" && this.txtTelephoneMovilDelivery.Text.Trim() == "___-___-___")
              throw new HandledException(1, "Debe ingresar al menos un teléfono para el Servicio Delivery");
            this.SaveRequirement((object) null, (EventArgs) null);
          }
          this.ViewState["f_PriceTotal"] = (object) this.txtTotal.Text;
          if (((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Premium) || ConfigurationManager.AppSettings["Habilitar"].ToString() == "0") && this.rbTypePayment.Items.Count > 1)
            this.rbTypePayment.Items.RemoveAt(1);
          this.Wizard1.MoveTo((WizardStepBase) this.WS_SeleccionMedioPago);
        }
        else
        {
          if (this.Wizard1.ActiveStep != this.WS_SeleccionMedioPago)
            return;
          this.SaveRequirement((object) null, (EventArgs) null);
          string[] strArray = this.ViewState["ids"].ToString().Split('|');
          this.Response.Redirect("~/Requirement/SuccessfulRegistration.aspx?RequirementId=" + strArray[0].ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&RequirementPlateId=" + strArray[1].ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&t=" + strArray[2].ToString() + "&mre=0", false);
          this.Session["SendEmail"] = (object) 1;
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

    protected void BtnClick1(object sender, EventArgs e)
    {
      this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosFinales);
    }

    protected void registerClaim(object sender, EventArgs e)
    {
      try
      {
        string pstrPlate = "";
        if (this.Wizard1.ActiveStep == this.WS_ValidacionOrden || this.Wizard1.ActiveStep == this.WS_DatosVehiculo1)
        {
          pstrPlate = this.txtPlateNumber.Text.Trim().Replace("-", "");
          string str = this.txtTitleNumber.Text.Trim();
          if (pstrPlate == "")
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Vacia);
          if (pstrPlate.Length < 6)
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Longitud6_Incorrecta);
          if (str == "")
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Titulo_Vacio);
        }
        else if (this.Wizard1.ActiveStep == this.WS_ValidacionPlaca || this.Wizard1.ActiveStep == this.WS_DatosVehiculo2)
        {
          pstrPlate = this.txtPlateNumber1.Text.Trim().Replace("-", "");
          if (pstrPlate == "")
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Vacia);
          if (pstrPlate.Length < 6)
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Longitud6_Incorrecta);
        }
        DataTable activeByPlate = new RequirementClaimQueriesBL().RequirementClaimGetActiveByPlate(pstrPlate);
        if (this.Wizard1.ActiveStepIndex == this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_ValidacionOrden) || this.Wizard1.ActiveStepIndex == this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_ValidacionPlaca))
        {
          if (activeByPlate.Rows.Count > 0)
          {
            string str = activeByPlate.Rows[0]["v_ClaimCode"].ToString();
            if (this.Wizard1.ActiveStep == this.WS_ValidacionOrden)
              Message.SetMessage(this.lblMessage, new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Reclamo + str));
            else if (this.Wizard1.ActiveStep == this.WS_ValidacionPlaca)
              Message.SetMessage(this.lblMessage, new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Reclamo + str));
          }
          else
            this.registerClaim_DataNotFound();
        }
        else if (this.Wizard1.ActiveStepIndex == this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_DatosVehiculo1) || this.Wizard1.ActiveStepIndex == this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_DatosVehiculo2))
        {
          if (activeByPlate.Rows.Count > 0)
          {
            string str = activeByPlate.Rows[0]["v_ClaimCode"].ToString();
            if (this.Wizard1.ActiveStep == this.WS_DatosVehiculo1)
              Message.SetMessage(this.lblMessage, new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Reclamo + str));
            else if (this.Wizard1.ActiveStep == this.WS_DatosVehiculo2)
              Message.SetMessage(this.lblMessage, new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Reclamo + str));
          }
          else
          {
            this.registerClaim_DataNoAgree();
            this.Session["i_VehicleId"] = (object) this.hidSunarpId.Value;
          }
        }
        activeByPlate.Dispose();
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

    protected void SaveRequirement(object sender, EventArgs e)
    {
      try
      {
        if (!this.chkConfirmPreview.Checked)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Confirma_Accion);
        if (this.divDatosDelivery.Visible && !this.chkYes.Checked && !this.chkNo.Checked)
          throw new HandledException(1, "Debe seleccionar si desea o no el Servicio de Delivery");
        if (this.chkYes.Checked && !this.chkDelivery1.Checked)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Confirma_Accion_Delivery);
        using (TransactionScope transactionScope = new TransactionScope())
        {
          this.objUserBE = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterRequirement.aspx");
          this.objRequirement = new SIIV.BE.Requirement();
          this.objRequirement.i_RequirementTypeId = new int?(1);
          this.objRequirement.f_Quantity = new double?(1.0);
          this.objRequirement.v_Observations = "";
          this.objRequirement.i_InsertUserId = new int?(this.objUserBE.i_SystemUserId);
          this.objRequirement.v_Ubigeo = "";
          this.objRequirement.i_ProofPaymentTypeId = new int?(Convert.ToInt32(this.rdbProofPayment.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
          this.objVehicleRegistrationDetail = new VehicleRegistrationDetail();
          this.objVehicleRegistrationDetail.i_RegistrationTypeId = new int?(1);
          DataTable dataTable1 = new DataTable();
          if (this.Session["ProcessId"] == null)
            throw new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
          if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.Inmatriculacion) || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 2 || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 3 || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 7)
            dataTable1 = !this.isRegisterCall ? new RequirementQueriesBL().SunarpDataReadbyId(Convert.ToInt32(this.hidSunarpId.Value)) : (!this.chkYes.Checked || this.StatusCallCenter != 2 ? new RequirementQueriesBL().SunarpDataReadbyId(Convert.ToInt32(this.hidSunarpId.Value), this.isRegisterCall) : new RequirementQueriesBL().SunarpDataReadbyId(Convert.ToInt32(this.hidSunarpId.Value)));
          else if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.CambioUso))
            dataTable1 = !this.isRegisterCall ? new RequirementQueriesBL().SunarpDataReadChangeUse(this.txtPlateNumber.Text.Trim()) : (!this.chkYes.Checked || this.StatusCallCenter != 2 ? new RequirementQueriesBL().SunarpDataReadChangeUse(this.txtPlateNumber.Text.Trim(), this.isRegisterCall) : new RequirementQueriesBL().SunarpDataReadChangeUse(this.txtPlateNumber.Text.Trim()));
          else if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.CambioUsoRectificacion))
          {
            if (this.Session["VehicleIdClaim"] != null)
            {
              int int32 = Convert.ToInt32(this.Session["VehicleIdClaim"]);
              dataTable1 = new RequirementQueriesBL().SunarpDataReadChangeUseRectification(this.txtPlateNumber.Text.Trim(), int32);
            }
          }
          else
            dataTable1 = !this.isRegisterCall ? new RequirementQueriesBL().SunarpDataRead3rd(this.txtPlateNumber1.Text.Trim()) : (!this.chkYes.Checked || this.StatusCallCenter != 2 ? new RequirementQueriesBL().SunarpDataRead3rd(this.txtPlateNumber1.Text.Trim(), this.isRegisterCall) : new RequirementQueriesBL().SunarpDataRead3rd(this.txtPlateNumber1.Text.Trim()));
          this.objVehicleRegistrationDetail.i_RegistryOfficeId = new int?(Convert.ToInt32(dataTable1.Rows[0]["RegistryOfficeId"].ToString()));
          this.objVehicleRegistrationDetail.i_RegistryZoneId = new int?(Convert.ToInt32(dataTable1.Rows[0]["RegistryZoneId"].ToString()));
          this.objVehicleRegistrationDetail.i_VehicleCategoryId = !(dataTable1.Rows[0]["CategoryId"].ToString() != "") ? new int?() : new int?(Convert.ToInt32(dataTable1.Rows[0]["CategoryId"].ToString()));
          this.objVehicleRegistrationDetail.v_PlateNew = dataTable1.Rows[0]["v_platenew"].ToString();
          this.objVehicleRegistrationDetail.v_PlateOld = dataTable1.Rows[0]["v_plateold"].ToString();
          this.objVehicleRegistrationDetail.v_Brand = dataTable1.Rows[0]["v_brand"].ToString();
          this.objVehicleRegistrationDetail.v_Model = dataTable1.Rows[0]["v_model"].ToString();
          this.objVehicleRegistrationDetail.v_SerialNumber = dataTable1.Rows[0]["v_serialnumber"].ToString();
          this.objVehicleRegistrationDetail.v_CompleteNameOwner = dataTable1.Rows[0]["v_OwnerCompleteName"].ToString();
          this.objRequirementPlate = new RequirementPlate();
          this.objRequirementPlate.i_RegistrationUseTypeOldId = new int?();
          if (this.Session["ProcessId"] == null)
            throw new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
          this.objRequirementPlate.i_ProcessTypeId = Convert.ToInt16(this.Session["ProcessId"].ToString()) != (short) 10 ? new int?((int) Convert.ToInt16(this.Session["ProcessId"].ToString())) : new int?(6);
          if (this.Session["ProcessId"] == null)
            throw new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
          if (Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 6)
          {
            this.objVehicleRegistrationDetail.i_VehicleClassId = new int?(Convert.ToInt32(dataTable1.Rows[0]["Classid"].ToString()));
            this.objVehicleRegistrationDetail.i_VehicleTypeUseId = new int?(Convert.ToInt32(dataTable1.Rows[0]["UseTypeSunarp"].ToString()));
            this.objVehicleRegistrationDetail.i_RegistrationTypeId = new int?(Convert.ToInt32(dataTable1.Rows[0]["i_VehicleRegistrationId"].ToString()));
          }
          else if (Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 10)
          {
            this.objVehicleRegistrationDetail.i_VehicleTypeUseId = new int?(Convert.ToInt32(dataTable1.Rows[0]["UseTypeSunarpNew"].ToString()));
            this.objVehicleRegistrationDetail.i_VehicleClassId = new int?(Convert.ToInt32(dataTable1.Rows[0]["Classid"].ToString()));
            this.objVehicleRegistrationDetail.i_RegistrationTypeId = new int?(Convert.ToInt32(dataTable1.Rows[0]["i_VehicleRegistrationId"].ToString()));
            this.objRequirementPlate.i_RegistrationUseTypeOldId = new int?(Convert.ToInt32(dataTable1.Rows[0]["UseTypeSunarp"].ToString()));
          }
          else
          {
            this.objVehicleRegistrationDetail.i_VehicleClassId = new int?(Convert.ToInt32(dataTable1.Rows[0]["Classid"].ToString()));
            this.objVehicleRegistrationDetail.i_VehicleTypeUseId = new int?(Convert.ToInt32(dataTable1.Rows[0]["UseTypeSunarp"].ToString()));
            this.objVehicleRegistrationDetail.i_RegistrationTypeId = new int?(Convert.ToInt32(dataTable1.Rows[0]["i_VehicleRegistrationId"].ToString()));
          }
          if (this.Session["ProcessId"] == null)
            throw new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
          string str1;
          if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.Inmatriculacion) || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 2 || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 3 || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 7 || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 6)
          {
            this.objRequirementPlate.i_ProductId = new int?(Convert.ToInt32(dataTable1.Rows[0]["i_ProductId"].ToString()));
            this.objVehicleRegistrationDetail.v_TitleNumber = dataTable1.Rows[0]["v_titlenumber"].ToString();
            str1 = this.txtPlateNumber.Text.Trim().Replace("-", "");
          }
          else
          {
            this.objRequirementPlate.i_ProductId = Convert.ToInt16(this.Session["ProcessId"].ToString()) != (short) 4 ? new int?(Convert.ToInt32(dataTable1.Rows[0]["i_ProductId"].ToString())) : new int?(Convert.ToInt32((this.ViewState["Product3rd"] as DataTable).Rows[0]["i_ProductId"].ToString()));
            str1 = this.txtPlateNumber1.Text.Trim().Replace("-", "");
            this.objVehicleRegistrationDetail.v_TitleNumber = new RequirementQueriesBL().GetCorrelativeTitle();
          }
          if (this.chkPorta.Checked)
            this.objRequirementPlate.i_ProductId = !this.chkTorni.Checked ? new int?(new RequirementQueriesBL().GetProductCorrespondence(this.objRequirementPlate.i_ProductId.Value, 4)) : new int?(new RequirementQueriesBL().GetProductCorrespondence(this.objRequirementPlate.i_ProductId.Value, 5));
          if (this.chkPortaMoto.Checked)
            this.objRequirementPlate.i_ProductId = new int?(new RequirementQueriesBL().GetProductCorrespondence(this.objRequirementPlate.i_ProductId.Value, 4));
          if (this.chkYes.Checked)
          {
            if (this.txtDeliveryDescription.Text.Trim() == "" || this.txtDeliveryDescription2.Text.Trim() == "" || this.wddDistrict.SelectedIndex == 0)
              throw new HandledException(1, "Debe ingresar todos los datos de la dirección");
            if (this.txtMailDelivery.Text.Trim() == "")
              throw new HandledException(1, "Debe ingresar el correo para el Servicio Delivery");
            if (this.txtTelephoneDelivery.Text.Trim() == "" && this.txtTelephoneMovilDelivery.Text.Trim() == "___-___-___")
              throw new HandledException(1, "Debe ingresar al menos un teléfono para el Servicio Delivery");
            DataTable dataTable2 = new RequirementQueriesBL().ValidateDeliveryZone(Convert.ToInt32(this.cboDeliveryPoint.SelectedValue));
            Convert.ToInt32(dataTable2.Rows[0]["i_DeliveryReferenceId"]);
            Convert.ToInt32(dataTable2.Rows[0]["i_DefaultServiceDeliveryAuto"]);
            Convert.ToInt32(dataTable2.Rows[0]["i_DefaultServiceDeliveryMoto"]);
            this.objRequirementPlate.i_ProductId = new int?(new RequirementQueriesBL().GetProductCorrespondence(Convert.ToInt32(dataTable1.Rows[0]["i_ProductId"].ToString()), Convert.ToInt32(this.ViewState["intProducIdDelivery"])));
          }
          if (this.Session["RequirementPlateType"] == null)
            throw new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
          if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Regular))
            this.objRequirementPlate.i_RequirementPlateTypeId = !(str1.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture) == "E") ? new int?(Convert.ToInt32((object) enmRequirementPlateType.Regular)) : new int?(Convert.ToInt32((object) enmRequirementPlateType.Especial));
          else if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Premium))
            this.objRequirementPlate.i_RequirementPlateTypeId = new int?(Convert.ToInt32((object) enmRequirementPlateType.Premium));
          else if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.NotariaLiberada))
            this.objRequirementPlate.i_RequirementPlateTypeId = new int?(Convert.ToInt32((object) enmRequirementPlateType.NotariaLiberada));
          this.objRequirementPlate.i_VehicleId = new int?(Convert.ToInt32(dataTable1.Rows[0]["i_VehicleId"].ToString()));
          this.objRequirementPlate.v_RegistrationCode = "";
          this.objRequirementPlate.i_ContingencyTypeId = new int?();
          this.objRequirementPlate.b_ContingencyDelivery = new bool?();
          this.objRequirementPlate.d_RegistrationDispatchDate = new DateTime?(Convert.ToDateTime(dataTable1.Rows[0]["d_DispatchDate"].ToString()));
          this.objRequirementProgramation = new RequirementProgramation();
          if (!this.chkYes.Checked)
          {
            this.objRequirementPlate.i_DeliveryPointId = new int?(Convert.ToInt32(this.cboDeliveryPoint.SelectedValue));
            this.objRequirementPlate.i_PlateTypeId = new int?(1);
          }
          else
          {
            this.objRequirementPlate.i_DeliveryPointId = new int?(Convert.ToInt32(new RequirementQueriesBL().ValidateDeliveryZone(Convert.ToInt32(this.cboDeliveryPoint.SelectedValue)).Rows[0]["i_DeliveryReferenceId"]));
            this.objRequirementPlate.i_PlateTypeId = new int?(12);
            this.objRequirementProgramation.i_ZoneReference = new int?(Convert.ToInt32(this.ViewState["varZona"].ToString()));
            this.objRequirementProgramation.i_DistrictReference = new int?(Convert.ToInt32(this.ViewState["varDistrito"].ToString()));
          }
          this.objRequirementPlate.b_PendingConfirmation = new bool?();
          this.objRequirementPlate.b_Migrated = new bool?();
          this.objRequirementContributor = new RequirementContributor();
          this.objRequirementContributor.i_DocumentTypeId = new int?(Convert.ToInt32(this.CboDocumentType.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
          this.objRequirementContributor.v_DocumentNumber = this.TxtDocNumberProofPaper.Text.Trim();
          this.objRequirementContributor.v_LastName = "";
          this.objRequirementContributor.v_FirstName = "";
          this.objRequirementContributor.v_CompleteName = this.txtBeneficiaryName.Text.Trim();
          this.objRequirementContributor.v_Email = this.txtBeneficiaryMail.Text.Trim();
          this.objRequirementContributor.v_Address = this.txtAddress.Text.Trim();
          this.objContributorRequester = new RequirementContributor();
          if (this.Session["RequirementPlateType"] == null)
            throw new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
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
            this.objContributorRequester.i_DocumentTypeId = new int?(Convert.ToInt32(this.cboRequesterTypeDoc.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
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
          if (this.chkYes.Checked)
          {
            this.objContributorRequester.v_AddressLocation = this.wddDeliveryDescription.SelectedItem.Text + " " + this.txtDeliveryDescription.Text + " " + this.wddDeliveryDescription2.SelectedItem.Text + " " + this.txtDeliveryDescription2.Text + " |" + this.txtReference.Text;
            this.objContributorRequester.v_Email = this.txtMailDelivery.Text.Trim();
            this.objContributorRequester.v_PhoneNumber = this.txtTelephoneDelivery.Text.Trim() + "|" + this.txtTelephoneMovilDelivery.Text.Trim();
          }
          string str2 = HttpContext.Current.Session["FechaDenuncia"]?.ToString();
          this.objContributorRequester.d_DatePrintingPoliceReport = !string.IsNullOrEmpty(str2) ? new DateTime?(Convert.ToDateTime(str2)) : new DateTime?();
          this.objContributorRequester.v_PoliceReportOrderNumber = string.IsNullOrWhiteSpace(this.TextBoxNumeroOrden.Text) ? (string) null : this.TextBoxNumeroOrden.Text.Trim();
          this.objContributorRequester.v_PoliceReportKey = string.IsNullOrWhiteSpace(this.TextBoxClave.Text) ? (string) null : this.TextBoxClave.Text.Trim();
          this.TypeApplicant = Convert.ToInt32(this.ViewState["TypeApplicant"]);
          this.objContributorRequester.i_TypeApplicant = this.TypeApplicant == 0 ? new int?() : new int?(this.TypeApplicant);
          if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.Duplicado))
          {
            this.RegistrationReason = Convert.ToInt32(this.ViewState["RegistrationReason"]);
            this.objContributorRequester.i_RegistrationReason = this.RegistrationReason == 0 ? new int?() : new int?(this.RegistrationReason);
          }
          DataTable ownersByIdSunarp = new RequirementQueriesBL().GetOwnersByIdSunarp(Convert.ToInt32(this.hidSunarpId.Value));
          string str3 = "";
          string str4 = "";
          string str5 = "";
          string str6 = "";
          for (int index = 0; index < ownersByIdSunarp.Rows.Count; ++index)
          {
            str3 = str3 + "/" + ownersByIdSunarp.Rows[index]["CompleteName"].ToString();
            str4 = str4 + "/" + ownersByIdSunarp.Rows[index]["v_DocumentTypeId"].ToString();
            str5 = str5 + "/" + ownersByIdSunarp.Rows[index]["v_DocumentType"].ToString();
            str6 = str6 + "/" + ownersByIdSunarp.Rows[index]["v_DocumentNumber"].ToString();
          }
          this.objRequirementPlate.v_OwnerCompleteName = str3.Substring(1, str3.Length - 1);
          this.objRequirementPlate.v_OwnerDocumentType = str4.Substring(1, str4.Length - 1);
          this.objRequirementPlate.v_OwnerDocumentDescription = str5.Substring(1, str5.Length - 1);
          this.objRequirementPlate.v_OwnerDocumentNumber = str6.Substring(1, str6.Length - 1);
          ownersByIdSunarp.Dispose();
          this.objPayment = new Payment();
          int num = 0;
          if (this.Session["RequirementPlateType"] != null)
          {
            if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Regular))
            {
              if (!this.chkPorta.Checked && !this.chkPortaMoto.Checked && !this.chkYes.Checked)
              {
                if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.Duplicado3rd))
                {
                  this.objPayment.f_PriceSale = new double?(Convert.ToDouble((this.ViewState["Product3rd"] as DataTable).Rows[0]["f_PriceCost"].ToString()));
                  this.objPayment.f_PriceTax = new double?(Convert.ToDouble((this.ViewState["Product3rd"] as DataTable).Rows[0]["f_PriceTax"].ToString()));
                  this.objPayment.f_PriceTotal = new double?(Convert.ToDouble((this.ViewState["Product3rd"] as DataTable).Rows[0]["f_PriceSale"].ToString()));
                }
                else if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.CambioUso))
                {
                  this.objPayment.f_PriceSale = new double?(Convert.ToDouble(dataTable1.Rows[0]["f_PriceCost"].ToString()));
                  this.objPayment.f_PriceTax = new double?(Convert.ToDouble(dataTable1.Rows[0]["f_PriceTax"].ToString()));
                  this.objPayment.f_PriceTotal = new double?(Convert.ToDouble(dataTable1.Rows[0]["f_PriceSale"].ToString()));
                }
                else
                {
                  this.objPayment.f_PriceSale = new double?(Convert.ToDouble(dataTable1.Rows[0]["f_PriceCost"].ToString()));
                  this.objPayment.f_PriceTax = new double?(Convert.ToDouble(dataTable1.Rows[0]["f_PriceTax"].ToString()));
                  this.objPayment.f_PriceTotal = new double?(Convert.ToDouble(dataTable1.Rows[0]["f_PriceSale"].ToString()));
                }
              }
              else
              {
                DataTable dataTable3 = new ProductCostQueriesBL().GetbyFilter(new ArrayList()
                {
                  (object) 2,
                  (object) this.objRequirementPlate.i_ProductId,
                  (object) 1
                });
                this.objPayment.f_PriceSale = new double?(Convert.ToDouble(dataTable3.Rows[0]["f_PriceCost"].ToString()));
                this.objPayment.f_PriceTax = new double?(Convert.ToDouble(dataTable3.Rows[0]["f_PriceTax"].ToString()));
                this.objPayment.f_PriceTotal = new double?(Convert.ToDouble(dataTable3.Rows[0]["f_PriceSale"].ToString()));
                dataTable3.Dispose();
              }
              if (this.Session["PaymentAnswer"] != null)
              {
                num = (int) this.Session["PaymentAnswer"];
                if (num == 2)
                {
                  this.objRequirement.i_Status = new int?(1);
                  this.objRequirementPlate.i_Status = new int?(0);
                  this.objPayment.i_Status = new int?(0);
                  this.objPayment.i_PaymentTypeId = new int?(1);
                  this.objPayment.i_BankId = new int?(0);
                  this.objPayment.v_BankOperationNumber = (string) null;
                  this.objPayment.v_BankOperationUser = (string) null;
                  this.objPayment.v_BankOperationTerminal = (string) null;
                  this.objPayment.i_AccountId = new int?();
                  this.objRequirementPlate.i_DataBankId = (string) null;
                }
                else if (this.ViewState["DataBank"] != null)
                {
                  this.objRequirement.i_Status = new int?(1);
                  this.objRequirementPlate.i_Status = new int?(1);
                  this.objPayment.i_Status = new int?(1);
                  this.objPayment.i_PaymentTypeId = new int?(1);
                  this.objRequirementPlate.i_DataBankId = (this.ViewState["DataBank"] as DataTable).Rows[0]["idVoucher"].ToString();
                  DataTable dataBankbyId = new DataBankQueriesBL().getDataBankbyId(Convert.ToInt32((this.ViewState["DataBank"] as DataTable).Rows[0]["idVoucher"].ToString()));
                  this.objPayment.i_BankId = new int?(Convert.ToInt32(dataBankbyId.Rows[0]["i_IdTipoBanco"].ToString()));
                  this.objPayment.v_BankOperationNumber = dataBankbyId.Rows[0]["v_NroOperacion"].ToString();
                  this.objPayment.v_BankOperationUser = this.txtUserBankCode.Text.Trim();
                  this.objPayment.v_BankOperationTerminal = dataBankbyId.Rows[0]["v_Terminal"].ToString();
                  this.objPayment.d_BankOperationDate = new DateTime?(this.cboPaymentDate.Value);
                  this.objPayment.i_AccountId = new int?();
                  dataBankbyId.Dispose();
                  this.objPayment.f_PriceSale = new double?(Convert.ToDouble((this.ViewState["DataBank"] as DataTable).Rows[0]["Precio"].ToString()) - Convert.ToDouble((this.ViewState["DataBank"] as DataTable).Rows[0]["IGV"].ToString()));
                  this.objPayment.f_PriceTax = new double?(Convert.ToDouble((this.ViewState["DataBank"] as DataTable).Rows[0]["IGV"].ToString()));
                  this.objPayment.f_PriceTotal = new double?(Convert.ToDouble((this.ViewState["DataBank"] as DataTable).Rows[0]["Precio"].ToString()));
                }
              }
            }
            else if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Premium) || (int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.NotariaLiberada))
            {
              this.objPayment.f_PriceSale = new double?();
              this.objPayment.f_PriceTax = new double?();
              this.objPayment.f_PriceTotal = new double?();
              this.objRequirement.i_Status = new int?(1);
              this.objRequirementPlate.i_Status = new int?(0);
              this.objPayment.i_Status = new int?();
              this.objPayment.i_PaymentTypeId = new int?();
              this.objPayment.i_BankId = new int?();
              this.objPayment.v_BankOperationNumber = (string) null;
              this.objPayment.v_BankOperationUser = (string) null;
              this.objPayment.v_BankOperationTerminal = (string) null;
              this.objPayment.i_AccountId = new int?();
              this.objPayment.d_BankOperationDate = new DateTime?();
              this.objRequirementPlate.i_DataBankId = (string) null;
            }
          }
          if (this.Session["ProcessId"] != null)
          {
            if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.Inmatriculacion) || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 2 || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 3 || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 7 || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 6)
            {
              switch (new RequirementQueriesBL().ValidateExistRequirementByPlateTitle(dataTable1.Rows[0]["v_platenew"].ToString(), dataTable1.Rows[0]["v_titlenumber"].ToString()))
              {
                case -4:
                  throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Cambio_Clase);
                case -3:
                  throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Transferencia_Placa_Nueva);
                case -2:
                  throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Tramite_Terminado);
                case -1:
                  throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Titulo_Tramite_Anterior);
                case 0:
                  throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Tramite_Curso);
              }
            }
            else
            {
              switch (new RequirementQueriesBL().Verify3rdPlate(dataTable1.Rows[0]["v_platenew"].ToString()))
              {
                case -2:
                  if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) != Convert.ToInt32((object) enmProccessType.CambioUsoRectificacion))
                  {
                    DataTable activeByPlate = new RequirementClaimQueriesBL().RequirementClaimGetActiveByPlate(this.txtPlateNumber.Text.Trim().Replace("-", ""));
                    string str7 = activeByPlate.Rows[0]["v_ClaimCode"].ToString();
                    activeByPlate.Dispose();
                    throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Reclamo + str7);
                  }
                  break;
                case -1:
                  throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Tramite_Previo);
                case 0:
                  throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Tramite_Curso);
              }
            }
          }
          if (num == 1)
          {
            switch (new DataBankQueriesBL().getDataBankConciliate(Convert.ToInt32((this.ViewState["DataBank"] as DataTable).Rows[0]["idVoucher"].ToString())))
            {
              case 1:
                throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Pago_Tramite);
              case 2:
                throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Pago_No_Encontrado);
            }
          }
          this.ViewState["f_PriceTotal"] = (object) this.objPayment.f_PriceTotal;
          int[] numArray = new RequirementManagementBL().RequirementInsertOne(this.objRequirement, this.objVehicleRegistrationDetail, this.objRequirementPlate, this.objRequirementContributor, this.objContributorRequester, this.objPayment, this.objRequirementProgramation, this.Request.UserHostAddress);
          if (this.Session["RequirementPlateType"] != null && this.ViewState["DataBank"] != null && num == 1 && (int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Regular))
          {
            this.objDataBankManagement = new DataBankManagementBL();
            this.objDataBankManagement.ConciliatDataBank(Convert.ToInt32(numArray[1]), this.objVehicleRegistrationDetail.v_PlateNew, Convert.ToInt32((this.ViewState["DataBank"] as DataTable).Rows[0]["idVoucher"].ToString()));
          }
          if (this.Session["ProcessId"] == null)
            throw new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
          if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.CambioUsoRectificacion))
            new RequirementClaimManagementBL().RequirementClaimUpdateRequirementPlateId(Convert.ToString(this.Session["ClaimCode"]), Convert.ToInt32(numArray[1].ToString((IFormatProvider) CultureInfo.CurrentCulture)), this.objUserBE.i_SystemUserId);
          if (this.Session["RequirementPlateType"] != null && ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Premium) || (int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.NotariaLiberada)))
          {
            ExceptionalRequirement pobjExceptionalRequirement = new ExceptionalRequirement();
            pobjExceptionalRequirement.i_ExceptionalRequirementTypeId = new int?((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Premium) ? 4 : 5);
            pobjExceptionalRequirement.i_RequirementPlateId = new int?(numArray[1]);
            pobjExceptionalRequirement.i_ReferenceRequirementPlateId = new int?();
            if (this.Session["SystemUser"] != null)
            {
              this.objUserBE = this.Session["SystemUser"] as SystemUser;
              pobjExceptionalRequirement.i_InsertUserId = new int?(this.objUserBE.i_SystemUserId);
            }
            else
              pobjExceptionalRequirement.i_InsertUserId = new int?(0);
            pobjExceptionalRequirement.i_ResponsibleCompanyId = new int?(2);
            int pintExceptionalRequirement = new RequirementManagementBL().ExceptionalRequirementInsert(pobjExceptionalRequirement);
            string userExtendedAction = new RequirementQueriesBL().GetSystemUserExtendedAction(this.objUserBE.i_SystemUserId, 1);
            if (userExtendedAction != "")
            {
              if (Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("1", StringComparison.CurrentCulture))) == "1")
                new RequirementManagementBL().UpdateExceptionalRequirmentStatus(numArray[1], pintExceptionalRequirement, 1, 1, "", this.objUserBE.i_SystemUserId);
            }
          }
          transactionScope.Complete();
          if (this.objRequirementPlate.i_PlateTypeId.GetValueOrDefault() == 12)
            this.Response.Redirect("~/Delivery/BookQuery.aspx?Plate=" + this.lblPlateNumber.Text, false);
          else
            this.ViewState["ids"] = (object) (numArray[0].ToString() + "|" + numArray[1].ToString() + "|" + this.Request.QueryString["t"]);
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

    protected void Wizard1_ActiveStepChanged1(object sender, EventArgs e)
    {
      try
      {
        this.btnNext = this.GetControlFromWizard(this.Wizard1, RegisterRequirement.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnNext") as Button;
        this.btnClaim = this.GetControlFromWizard(this.Wizard1, RegisterRequirement.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnClaim") as Button;
        this.btnPrevious = this.GetControlFromWizard(this.Wizard1, RegisterRequirement.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnPrevious") as Button;
        this.btnCancel = this.GetControlFromWizard(this.Wizard1, RegisterRequirement.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnCancel") as Button;
        if (this.Wizard1.ActiveStep == this.WS_SeleccionPago)
        {
          this.btnNext1 = this.GetControlFromWizard(this.Wizard1, RegisterRequirement.WizardNavigationTempContainer.StartNavigationTemplateContainerID, "btnNext") as Button;
          this.btnPrevious1 = this.GetControlFromWizard(this.Wizard1, RegisterRequirement.WizardNavigationTempContainer.StartNavigationTemplateContainerID, "btnCancel") as Button;
          if (this.btnNext1 != null)
            this.btnNext1.Visible = false;
          if (this.Session["RequirementPlateType"] == null)
            throw new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
          if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Regular))
          {
            if (this.btnPrevious1 != null)
              this.btnPrevious1.Text = (int) this.ViewState["PublicUser"] != 0 ? "Cancelar" : "Regresar";
          }
          else if (((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Premium) || (int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.NotariaLiberada)) && this.btnPrevious1 != null)
            this.btnPrevious1.Text = "Regresar";
        }
        if (this.Wizard1.ActiveStep == this.WS_DatosSolicitante)
        {
          this.btnNext1 = this.GetControlFromWizard(this.Wizard1, RegisterRequirement.WizardNavigationTempContainer.StartNavigationTemplateContainerID, "btnNext") as Button;
          this.btnPrevious1 = this.GetControlFromWizard(this.Wizard1, RegisterRequirement.WizardNavigationTempContainer.StartNavigationTemplateContainerID, "btnCancel") as Button;
          if (this.btnNext1 != null)
            this.btnNext1.Visible = true;
          if (this.btnPrevious1 != null)
            this.btnPrevious1.Text = "Cancelar";
        }
        if (this.btnPrevious != null)
          this.btnPrevious.Visible = true;
        if (this.btnNext != null)
        {
          this.btnNext.Enabled = true;
          if (this.Wizard1.ActiveStep == this.WS_AcreditacionPago)
            this.VerifyDataBank();
        }
        int num = this.Session["ProcessId"] != null ? (int) Convert.ToInt16(this.Session["ProcessId"].ToString()) : throw new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
        if (this.btnClaim != null)
        {
          if (this.Wizard1.ActiveStep == this.WS_ValidacionPlaca && (num == Convert.ToInt32((object) enmProccessType.Inmatriculacion) || num == 2 || num == 3 || num == 7) || this.Wizard1.ActiveStep == this.WS_ValidacionOrden && (num == Convert.ToInt32((object) enmProccessType.Inmatriculacion) || num == 2 || num == 3 || num == 7) || this.Wizard1.ActiveStep == this.WS_DatosVehiculo1 || this.Wizard1.ActiveStep == this.WS_DatosVehiculo2)
            this.btnClaim.Visible = true;
          else
            this.btnClaim.Visible = false;
        }
        if (this.Wizard1.ActiveStep != this.WS_SeleccionMedioPago)
          return;
        this.rbTypePayment.SelectedIndex = 0;
        this.rbTypePament_SelectedIndexChanged((object) null, (EventArgs) null);
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

    protected void btnYes_Click(object sender, EventArgs e)
    {
      try
      {
        this.redirectValidation(1);
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
          this.TagTerminal.Visible = true;
          this.lblTerminal.Text = "Terminal";
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
          this.TagTerminal.Visible = false;
          this.lblTerminal.Text = "Terminal";
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

    protected void btnValidatePago_Click(object sender, EventArgs e)
    {
      try
      {
        string v_UserId = this.txtUserBankCode.Text.Trim().ToUpper(CultureInfo.CurrentCulture).Replace("-", "");
        bool flag1 = false;
        string str1 = this.txtUserBankCode.Text.Replace("-", "").ToUpper(CultureInfo.CurrentCulture).Trim().Replace(" ", "");
        this.ValidateAcreditationData();
        DataTable dataTable1 = new DataTable();
        if (this.Session["ProcessId"] == null)
          throw new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
        DataTable dtSunarpData = (int) Convert.ToInt16(this.Session["ProcessId"].ToString()) != Convert.ToInt32((object) enmProccessType.Inmatriculacion) && Convert.ToInt16(this.Session["ProcessId"].ToString()) != (short) 2 && Convert.ToInt16(this.Session["ProcessId"].ToString()) != (short) 3 && Convert.ToInt16(this.Session["ProcessId"].ToString()) != (short) 7 ? ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) != Convert.ToInt32((object) enmProccessType.CambioUso) ? new RequirementQueriesBL().SunarpDataRead3rd(this.txtPlateNumber.Text.Trim()) : (!this.isRegisterCall ? new RequirementQueriesBL().SunarpDataReadChangeUse(this.txtPlateNumber.Text.Trim()) : new RequirementQueriesBL().SunarpDataReadChangeUse(this.txtPlateNumber.Text.Trim(), this.isRegisterCall))) : (!this.isRegisterCall ? new RequirementQueriesBL().SunarpDataReadbyId(Convert.ToInt32(this.hidSunarpId.Value)) : new RequirementQueriesBL().SunarpDataReadbyId(Convert.ToInt32(this.hidSunarpId.Value), this.isRegisterCall));
        string str2 = dtSunarpData.Rows[0]["v_PlateOld"].ToString();
        if (this.Session["ProcessId"] == null)
          throw new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
        string str3 = (int) Convert.ToInt16(this.Session["ProcessId"].ToString()) != Convert.ToInt32((object) enmProccessType.Inmatriculacion) && Convert.ToInt16(this.Session["ProcessId"].ToString()) != (short) 2 && Convert.ToInt16(this.Session["ProcessId"].ToString()) != (short) 3 && Convert.ToInt16(this.Session["ProcessId"].ToString()) != (short) 7 ? this.txtPlateNumber.Text.Trim().Replace("-", "") : this.txtPlateNumber.Text.Trim().Replace("-", "");
        string pstrProduct;
        double num1;
        if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.Duplicado3rd))
        {
          pstrProduct = (this.ViewState["Product3rd"] as DataTable).Rows[0]["v_code"].ToString();
          num1 = Convert.ToDouble((this.ViewState["Product3rd"] as DataTable).Rows[0]["f_PriceSale"].ToString());
        }
        else
        {
          pstrProduct = dtSunarpData.Rows[0]["v_code"].ToString();
          num1 = Convert.ToDouble(dtSunarpData.Rows[0]["f_priceSale"].ToString());
        }
        this.objUserBE = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterRequirement.aspx");
        string userExtendedAction = new RequirementQueriesBL().GetSystemUserExtendedAction(this.objUserBE.i_SystemUserId, 1);
        string str4 = "";
        if (userExtendedAction != "")
          str4 = Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("2", StringComparison.CurrentCulture)));
        dtSunarpData.Dispose();
        if (str4 != "2")
        {
          if (str3.ToUpper(CultureInfo.CurrentCulture) == v_UserId)
            flag1 = true;
          if (!flag1)
          {
            v_UserId = v_UserId.Substring(0, 1) + "0" + v_UserId.Substring(2, v_UserId.Length - 2);
            if (str3.ToUpper(CultureInfo.CurrentCulture) == v_UserId)
              flag1 = true;
          }
          if (!flag1)
          {
            string str5 = str1;
            string str6 = str5.Substring(0, 3);
            bool flag2 = false;
            if (str2 == str1)
              flag2 = true;
            if (str6.Replace("O", "0") + str5.Substring(3) == str3)
              flag2 = true;
            if (str6.Replace("0", "O") + str5.Substring(3) == str3)
              flag2 = true;
            if (str6.Replace("1", "L") + str5.Substring(3) == str3)
              flag2 = true;
            if (str6.Replace("1", "I") + str5.Substring(3) == str3)
              flag2 = true;
            if (str6.Replace("I", "L") + str5.Substring(3) == str3)
              flag2 = true;
            if (str6.Replace("I", "1") + str5.Substring(3) == str3)
              flag2 = true;
            if (str6.Replace("L", "1") + str5.Substring(3) == str3)
              flag2 = true;
            if (str6.Replace("L", "I") + str5.Substring(3) == str3)
              flag2 = true;
            if (!flag2)
              throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Codigo_No_Coincide_Placa);
          }
        }
        string text = this.txtTerminal.Text;
        int int32 = Convert.ToInt32(this.cboBank.SelectedValue);
        DateTime dateTime = Convert.ToDateTime(this.cboPaymentDate.Value);
        DataTable dataTable2 = new DataTable();
        int num2;
        DataTable dataTable3;
        if (this.rdlPaymentType.SelectedIndex == 0)
        {
          num2 = 1;
          dataTable3 = new DataBankQueriesBL().PaymentAcreditation(int32, dateTime.ToString("yyyyMMdd"), text, str1, pstrProduct, num2);
          if (Convert.ToInt32(dataTable3.Rows[0]["idVoucher"]) == -1 && str3.Substring(1, 1) == "0")
          {
            string pstrUserCode = str1.Substring(0, 1) + "0" + str1.Substring(2, str1.Length - 2);
            dataTable3 = new DataBankQueriesBL().PaymentAcreditation(int32, dateTime.ToString("yyyyMMdd"), text, pstrUserCode, pstrProduct, num2);
          }
          if (Convert.ToInt32(dataTable3.Rows[0]["idVoucher"]) == -1)
          {
            string pstrUserCode = str1.Substring(0, 1) + "O" + str1.Substring(2, str1.Length - 2);
            dataTable3 = new DataBankQueriesBL().PaymentAcreditation(int32, dateTime.ToString("yyyyMMdd"), text, pstrUserCode, pstrProduct, num2);
          }
          if (Convert.ToInt32(dataTable3.Rows[0]["idVoucher"]) == -1)
          {
            if (str1.Substring(1, 1) == "I" || str1.Substring(1, 1) == "L" || str1.Substring(1, 1) == "1")
              dataTable3 = this.validaTeWildCart2do3er(str1, dtSunarpData);
            if (str1.Substring(2, 1) == "I" || str1.Substring(2, 1) == "L" || str1.Substring(2, 1) == "1")
              dataTable3 = this.validaTeWildCart2do3er(str1, dtSunarpData);
          }
        }
        else
        {
          num2 = 2;
          dataTable3 = new DataBankQueriesBL().PaymentAcreditation(int32, dateTime.ToString("yyyyMMdd"), (string) null, str1, pstrProduct, num2);
        }
        if (Convert.ToInt32(dataTable3.Rows[0]["idVoucher"]) == 0)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Pago_Diferente_Precio_Producto);
        if (Convert.ToInt32(dataTable3.Rows[0]["idVoucher"]) == -1)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Pago_No_Encontrado_Detalle);
        if (Convert.ToInt32(dataTable3.Rows[0]["idVoucher"]) == -2)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Pago_Tramite);
        if (Convert.ToInt32(dataTable3.Rows[0]["idVoucher"]) == -3)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Pago_No_Valido_Periodo);
        if (Convert.ToInt32(dataTable3.Rows[0]["idVoucher"]) == -4)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Pago_Reembolso);
        if (Convert.ToInt32(dataTable3.Rows[0]["idVoucher"]) == -5)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Pago_Observado);
        if (Convert.ToInt32(dataTable3.Rows[0]["idVoucher"]) == -8)
        {
          DataTable activeByIdBank = new RequirementClaimQueriesBL().RequirementClaimGetActiveByIdBank(int32, num2, dateTime.ToString("yyyyMMdd"), text, v_UserId);
          string str7 = activeByIdBank.Rows[0]["v_ClaimCode"].ToString();
          activeByIdBank.Dispose();
          throw new HandledException(2, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Pago_Con_Reclamo + str7);
        }
        dataTable3.Dispose();
        Button controlFromWizard = this.GetControlFromWizard(this.Wizard1, RegisterRequirement.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnNext") as Button;
        this.ViewState["DataBank"] = (object) dataTable3;
        controlFromWizard.Enabled = true;
        Message.SetMessage(this.lblMessage, new HandledException(2, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Pago_Encontrado));
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

    protected void chkYes_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        if (!this.chkYes.Checked)
          return;
        DataTable dataTable1 = new DataTable();
        this.objUserBE = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterRequirement.aspx");
        if (new RequirementQueriesBL().GetCantDeliveryServices(this.objUserBE.i_SystemUserId) > 4)
          throw new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_DETRACTION);
        this.chkNo.Checked = false;
        DataTable priceServiceDelivery1 = new RequirementQueriesBL().GetPriceServiceDelivery(471, 0);
        if (this.isRegisterCall)
        {
          if (this.StatusCallCenter == 1)
          {
            this.tr3.Visible = true;
            this.lblPriceCallCenter2.Text = priceServiceDelivery1.Rows[0]["f_PriceProduct"].ToString();
            this.lblServiceDescription2.Text = priceServiceDelivery1.Rows[0]["v_Description"].ToString();
          }
          else
            this.hdiPrice.Value = this.Session["hdiPrice3"].ToString();
        }
        this.lblCobertura.Visible = true;
        this.btnCobertura.Visible = true;
        this.DivDelivery.Visible = true;
        this.divImgDelivery.Visible = true;
        this.chkDelivery1.Visible = true;
        this.CheckDelivery.Visible = true;
        this.trAdditionalProducts.Visible = true;
        this.txtTotal.Text = this.hdiPrice.Value.ToString();
        this.contAdd.Visible = false;
        this.contAddMoto.Visible = false;
        this.divImgPortaplaca.Visible = false;
        this.divImgPortaPlacaMoto.Visible = false;
        this.Div2.Visible = true;
        this.CleanTextDelivery();
        this.chkTorni.Checked = false;
        this.chkPorta.Checked = false;
        this.chkPortaMoto.Checked = false;
        double num1 = Convert.ToDouble(this.hdiPrice.Value.ToString());
        this.trTorni.Visible = false;
        this.SetDeliveryEnabled(true, true, true, true, true, true, true, true, true);
        short int16 = Convert.ToInt16(this.Session["ProcessId"].ToString());
        int num2;
        switch (int16)
        {
          case 1:
          case 3:
          case 7:
            num2 = 1;
            break;
          default:
            num2 = (int) int16 == Convert.ToInt32((object) enmProccessType.Inmatriculacion) ? 1 : (int16 == (short) 2 ? 1 : 0);
            break;
        }
        DataTable dataTable2 = num2 == 0 ? ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) != Convert.ToInt32((object) enmProccessType.CambioUso) ? new RequirementQueriesBL().SunarpDataRead3rd(this.txtPlateNumber.Text.Trim()) : new RequirementQueriesBL().SunarpDataReadChangeUse(this.txtPlateNumber.Text.Trim())) : new RequirementQueriesBL().SunarpDataReadbyId(Convert.ToInt32(this.hidSunarpId.Value));
        DataTable dataTable3 = new RequirementQueriesBL().ValidateDeliveryZone(Convert.ToInt32(this.Session["PuntoEntrega"]));
        Convert.ToInt32(dataTable3.Rows[0]["i_DeliveryReferenceId"]);
        int int32_1 = Convert.ToInt32(dataTable3.Rows[0]["i_DefaultServiceDeliveryAuto"]);
        int int32_2 = Convert.ToInt32(dataTable3.Rows[0]["i_DefaultServiceDeliveryMoto"]);
        string descriptionProduct;
        DataTable priceServiceDelivery2;
        if (Convert.ToInt16(this.ViewState["VehicleClassId"].ToString()) != (short) 5)
        {
          int productCorrespondence = new RequirementQueriesBL().GetProductCorrespondence(Convert.ToInt32(dataTable2.Rows[0]["i_ProductId"].ToString()), int32_1);
          descriptionProduct = new RequirementQueriesBL().GetDescriptionProduct(productCorrespondence);
          priceServiceDelivery2 = new RequirementQueriesBL().GetPriceServiceDelivery(productCorrespondence, int32_1);
        }
        else
        {
          int productCorrespondence = new RequirementQueriesBL().GetProductCorrespondence(Convert.ToInt32(dataTable2.Rows[0]["i_ProductId"].ToString()), int32_2);
          descriptionProduct = new RequirementQueriesBL().GetDescriptionProduct(productCorrespondence);
          priceServiceDelivery2 = new RequirementQueriesBL().GetPriceServiceDelivery(productCorrespondence, int32_2);
        }
        this.lblProductDescription.Text = descriptionProduct;
        this.lblPriceProducto.Text = priceServiceDelivery2.Rows[0]["f_PriceProduct"].ToString();
        this.lblPriceDelivery.Text = "0.00";
        this.txtTotal.Text = Convert.ToDecimal(string.Format("{0:F2}", (object) num1)).ToString();
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

    protected void chkNo_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        if (!this.chkNo.Checked)
          return;
        if (this.isRegisterCall && this.StatusCallCenter == 2)
          this.hdiPrice.Value = this.Session["f_PriceSale"].ToString();
        this.chkYes.Checked = false;
        this.lblCobertura.Visible = false;
        this.btnCobertura.Visible = false;
        this.DivDelivery.Visible = false;
        this.divImgDelivery.Visible = false;
        this.chkDelivery1.Visible = false;
        this.CheckDelivery.Visible = false;
        this.trAdditionalProducts.Visible = true;
        this.txtTotal.Text = Convert.ToDecimal(string.Format("{0:F2}", (object) Convert.ToDouble(this.hdiPrice.Value))).ToString();
        this.ViewState["f_PriceCostF"] = (object) Convert.ToDouble(this.ViewState["f_PriceCost"]);
        DataTable priceServiceDelivery = new RequirementQueriesBL().GetPriceServiceDelivery(471, 0);
        if (Convert.ToInt16(this.ViewState["VehicleClassId"].ToString()) != (short) 5)
        {
          if (this.isRegisterCall)
          {
            this.chkServi3.Checked = true;
            this.chkServi3.Enabled = false;
            this.tr4.Visible = true;
            this.lblPriceCallCenter1.Text = priceServiceDelivery.Rows[0]["f_PriceProduct"].ToString();
            this.lblServiceDescription1.Text = priceServiceDelivery.Rows[0]["v_Description"].ToString();
          }
          this.divImgPortaplaca.Visible = true;
          this.contAdd.Visible = true;
          this.contAddMoto.Visible = false;
        }
        else
        {
          if (this.isRegisterCall)
          {
            this.chkServi.Checked = true;
            this.chkServi.Enabled = false;
            this.tr2.Visible = true;
            this.lblPriceCallCenter.Text = priceServiceDelivery.Rows[0]["f_PriceProduct"].ToString();
            this.lblServiceDescription.Text = priceServiceDelivery.Rows[0]["v_Description"].ToString();
          }
          this.divImgPortaPlacaMoto.Visible = true;
          this.contAdd.Visible = false;
          this.contAddMoto.Visible = true;
        }
        this.Div2.Visible = true;
        this.CleanTextDelivery();
        this.SetDeliveryEnabled(false, false, false, false, false, false, false, false, false);
        this.chkTorni.Checked = false;
        this.chkPorta.Checked = false;
        this.chkPortaMoto.Checked = false;
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

    protected void btnCobertura_Click(object sender, EventArgs e)
    {
      this.CreatePopUp("../Delivery/DeliveryZone.aspx?VehicleClassId=" + Convert.ToInt16(this.ViewState["VehicleClassId"].ToString()).ToString(), "Listado Distritos", "500px", "350px");
    }

    protected void wddDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        string text = this.wddDistrict.SelectedItem.Text;
        DataTable district = new RequirementQueriesBL().GetDistrict(Convert.ToInt32(this.wddDistrict.SelectedValue.ToString()), Convert.ToInt32(this.ViewState["VehicleClassId"]));
        if (district.Rows.Count <= 0)
          return;
        this.ViewState["varZona"] = (object) district.Rows[0]["v_ReferenceId"].ToString();
        this.ViewState["varDistrito"] = (object) district.Rows[0]["i_ParameterId"].ToString();
        this.ViewState["intProducIdDelivery"] = (object) district.Rows[0]["i_ServiceDelivery"].ToString();
        this.setChangeDeliveryZone(Convert.ToInt32(this.ViewState["intProducIdDelivery"]));
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

    protected void chkPorta_CheckedChanged1(object sender, EventArgs e)
    {
      try
      {
        double num1 = Convert.ToDouble(this.hdiPrice.Value.ToString());
        this.CleanTextDelivery();
        this.SetDeliveryEnabled(false, false, false, false, false, false, false, false, false);
        if (this.chkPorta.Checked)
        {
          this.chkTorni.Checked = false;
          this.trTorni.Visible = true;
          double num2 = num1 + 29.0;
          this.ViewState["f_PriceCostF"] = (object) (Convert.ToDouble(this.ViewState["f_PriceCostF"]) + 24.58);
          this.txtTotal.Text = Convert.ToDecimal(string.Format("{0:F2}", (object) num2)).ToString();
        }
        else
        {
          double num3 = Convert.ToDouble(this.txtTotal.Text);
          this.trTorni.Visible = false;
          double num4;
          if (this.chkTorni.Checked)
          {
            num4 = num3 - 33.9;
            this.ViewState["f_PriceCostF"] = (object) (Convert.ToDouble(this.ViewState["f_PriceCostF"]) - 28.73);
          }
          else
          {
            num4 = num3 - 29.0;
            this.ViewState["f_PriceCostF"] = (object) (Convert.ToDouble(this.ViewState["f_PriceCostF"]) - 24.58);
          }
          this.txtTotal.Text = Convert.ToDecimal(string.Format("{0:F2}", (object) num4)).ToString();
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

    public void setChangeDeliveryZone(int iProductIdDelivery)
    {
      double num = Convert.ToDouble(this.hdiPrice.Value.ToString());
      short int16 = Convert.ToInt16(this.Session["ProcessId"].ToString());
      DataTable dataTable = new DataTable();
      int productCorrespondence = new RequirementQueriesBL().GetProductCorrespondence(Convert.ToInt32((int16 != (short) 7 && int16 != (short) 1 && int16 != (short) 3 && (int) int16 != Convert.ToInt32((object) enmProccessType.Inmatriculacion) && int16 != (short) 2 ? ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) != Convert.ToInt32((object) enmProccessType.CambioUso) ? new RequirementQueriesBL().SunarpDataRead3rd(this.txtPlateNumber.Text.Trim()) : (!this.isRegisterCall ? new RequirementQueriesBL().SunarpDataReadChangeUse(this.txtPlateNumber.Text.Trim()) : new RequirementQueriesBL().SunarpDataReadChangeUse(this.txtPlateNumber.Text.Trim(), this.isRegisterCall))) : new RequirementQueriesBL().SunarpDataReadbyId(Convert.ToInt32(this.hidSunarpId.Value))).Rows[0]["i_ProductId"].ToString()), iProductIdDelivery);
      this.lblProductDescription.Text = new RequirementQueriesBL().GetDescriptionProduct(productCorrespondence);
      DataTable priceServiceDelivery = new RequirementQueriesBL().GetPriceServiceDelivery(productCorrespondence, iProductIdDelivery);
      this.lblPriceProducto.Text = priceServiceDelivery.Rows[0]["f_PriceProduct"].ToString();
      this.lblPriceDelivery.Text = priceServiceDelivery.Rows[0]["f_PriceDelivery"].ToString();
      this.txtTotal.Text = Convert.ToDecimal(string.Format("{0:F2}", (object) (num + Convert.ToDouble(priceServiceDelivery.Rows[0]["f_PriceDelivery"])))).ToString();
    }

    protected void chkPortaMoto_CheckedChanged1(object sender, EventArgs e)
    {
      try
      {
        double num1 = Convert.ToDouble(this.hdiPrice.Value.ToString());
        this.CleanTextDelivery();
        this.SetDeliveryEnabled(false, false, false, false, false, false, false, false, false);
        if (this.chkPortaMoto.Checked)
        {
          this.chkTorni.Checked = false;
          this.trTorni.Visible = false;
          double num2 = 8.47;
          if (this.ViewState["f_PriceCost"].ToString() == "38.31")
            num2 = 8.46;
          this.ViewState["f_PriceCostF"] = (object) (Convert.ToDouble(this.ViewState["f_PriceCost"]) + num2);
          this.txtTotal.Text = Convert.ToDecimal(string.Format("{0:F2}", (object) (num1 + 9.99))).ToString();
        }
        else
        {
          this.chkTorni.Checked = false;
          this.trTorni.Visible = false;
          double num3 = num1;
          this.ViewState["f_PriceCostF"] = (object) Convert.ToDouble(this.ViewState["f_PriceCost"]);
          this.txtTotal.Text = Convert.ToDecimal(string.Format("{0:F2}", (object) num3)).ToString();
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

    protected void chkTorni_CheckedChanged1(object sender, EventArgs e)
    {
      try
      {
        double num1 = Convert.ToDouble(this.txtTotal.Text);
        double num2;
        if (this.chkTorni.Checked)
        {
          num2 = num1 + 4.9;
          this.ViewState["f_PriceCostF"] = (object) (Convert.ToDouble(this.ViewState["f_PriceCostF"]) + 4.15);
        }
        else
        {
          num2 = num1 - 4.9;
          this.ViewState["f_PriceCostF"] = (object) (Convert.ToDouble(this.ViewState["f_PriceCostF"]) - 4.15);
        }
        this.txtTotal.Text = Convert.ToDecimal(string.Format("{0:F2}", (object) num2)).ToString();
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

    public void setScriptEvents()
    {
    }

    public void Initialize()
    {
      try
      {
        this.objUserBE = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterRequirement.aspx");
        this.ViewState["PublicUser"] = (object) new RequirementQueriesBL().GetSystemUserPublic(this.objUserBE.i_SystemUserId, 1);
        this.ViewState.Remove("ids");
        this.Session.Remove("Eticket");
        this.Session.Remove("EticketD");
        if (this.Session["RequirementPlateType"] != null)
        {
          if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Regular))
          {
            if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.Duplicado))
            {
              this.btnNext = this.GetControlFromWizard(this.Wizard1, RegisterRequirement.WizardNavigationTempContainer.StartNavigationTemplateContainerID, "btnNext") as Button;
              this.btnNext.Visible = false;
              this.btnCancel = this.GetControlFromWizard(this.Wizard1, RegisterRequirement.WizardNavigationTempContainer.StartNavigationTemplateContainerID, "btnCancel") as Button;
              this.btnCancel.Visible = false;
              this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_Applicant);
              ((this.Wizard1.FindControl("WS_DatosSolicitante") as WizardStep).FindControl("tblRegistro") as Panel).Visible = true;
              this.PanelJuridicos.Visible = true;
              this.lblNameReq.Visible = true;
              this.lblApatReq.Visible = true;
              this.lblAmatReq.Visible = true;
              this.lblNrDocReq.Visible = true;
              this.lblTelReq.Visible = true;
              this.lblEntregaPropietario.Visible = true;
              this.autoSetRequester();
            }
            else
            {
              Panel control = (this.Wizard1.FindControl("WS_DatosSolicitante") as WizardStep).FindControl("tblDenuncia") as Panel;
              if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) != Convert.ToInt32((object) enmProccessType.Duplicado))
                control.Visible = false;
              else
                control.Visible = true;
              this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_DatosSolicitante);
              this.autoSetRequester();
            }
          }
          else if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Premium) || (int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.NotariaLiberada))
          {
            this.Session["PaymentAnswer"] = (object) enmAnswer.No;
            this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_DatosSolicitante);
            this.autoSetRequester();
          }
        }
        else
          Message.SetMessage(this.lblMessage, new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO));
        this.beginTitle();
        this.getDocumentType();
        this.getProofPayment();
        this.getLocation();
        this.getBank();
        this.ViewState["DataBank"] = (object) null;
        this.hidSunarpId.Value = "0";
        this.txtPlateNumber.Enabled = true;
        this.txtTitleNumber.Enabled = true;
        this.txtPlateNumber.Enabled = true;
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

    public void autoSetRequester()
    {
      try
      {
        this.objUserBE = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterRequirement.aspx");
        this.Session["EmailSoli"] = (object) this.objUserBE.v_Email.Trim();
        if (this.objUserBE == null)
          return;
        string userExtendedAction = new RequirementQueriesBL().GetSystemUserExtendedAction(this.objUserBE.i_SystemUserId, 1);
        if (userExtendedAction != "")
        {
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
        if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Regular))
          str = "Registro de Solicitud";
        else if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Premium))
          str = "Registro de Solicitud Premium";
        else if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.NotariaLiberada))
          str = "Registro de Solicitud de Notaría";
        this.Page.Title = str;
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
        DataTable documentType = new RequirementQueriesBL().GetDocumentType();
        if (documentType.Rows.Count > 0)
        {
          this.CboDocumentType.DataSource = (object) documentType;
          this.CboDocumentType.DataTextField = "v_Description";
          this.CboDocumentType.DataValueField = "i_ParameterId";
          this.CboDocumentType.DataBind();
          this.CboDocumentType.SelectedIndex = 0;
          if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Premium) || (int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.NotariaLiberada) || (int) this.ViewState["PublicUser"] >= 0)
          {
            this.cboRequesterTypeDoc.DataSource = (object) documentType;
            this.cboRequesterTypeDoc.DataTextField = "v_Description";
            this.cboRequesterTypeDoc.DataValueField = "i_ParameterId";
            this.cboRequesterTypeDoc.DataBind();
            this.cboRequesterTypeDoc.SelectedIndex = 1;
            this.cboRequesterTypeDoc.Items.FindByValue("4").Enabled = false;
          }
        }
        documentType.Dispose();
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
        DataTable proofPaymenType = new RequirementQueriesBL().GetProofPaymenType();
        if (proofPaymenType.Rows.Count > 0)
        {
          this.rdbProofPayment.DataSource = (object) proofPaymenType;
          this.rdbProofPayment.DataTextField = "v_Description";
          this.rdbProofPayment.DataValueField = "i_ParameterId";
          this.rdbProofPayment.DataBind();
          this.rdbProofPayment.SelectedIndex = 0;
          this.CboDocumentType.SelectedValue = enmDocumentPersonType.RegistroUnicoContribuyente.ToString();
        }
        proofPaymenType.Dispose();
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
        DataTable locationRequirement = new RequirementQueriesBL().GetLocationRequirement(this.objUserBE.i_SystemUserId);
        foreach (DataRow row in (InternalDataCollectionBase) locationRequirement.Rows)
        {
          if (row["i_LocationId"].ToString() == "17")
            row.Delete();
        }
        if (locationRequirement.Rows.Count > 0)
        {
          this.cboDeliveryPoint.DataSource = (object) locationRequirement;
          this.cboDeliveryPoint.DataTextField = "v_Description";
          this.cboDeliveryPoint.DataValueField = "i_LocationId";
          this.cboDeliveryPoint.DataBind();
          this.cboDeliveryPoint.SelectedIndex = 0;
        }
        locationRequirement.Dispose();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void getDistrictbyClasification(int i_VehicleClasification)
    {
      try
      {
        DataTable dataTable = new DataTable();
        DataTable districtByDeliveryPoint = new RequirementQueriesBL().GetDistrictByDeliveryPoint(Convert.ToInt32(this.Session["PuntoEntrega"]), i_VehicleClasification);
        if (districtByDeliveryPoint.Rows.Count <= 0)
          return;
        this.wddDistrict.DataSource = (object) districtByDeliveryPoint;
        this.wddDistrict.DataTextField = "v_Description";
        this.wddDistrict.DataValueField = "i_ParameterId";
        this.wddDistrict.DataBind();
        this.wddDistrict.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --", "0"));
        this.wddDistrict.SelectedIndex = 0;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string getBank()
    {
      try
      {
        string bank1 = "";
        DataTable bank2 = new RequirementQueriesBL().GetBank();
        this.cboBank.DataSource = (object) bank2;
        this.cboBank.DataTextField = "v_Description";
        this.cboBank.DataValueField = "i_ParameterId";
        this.cboBank.DataBind();
        this.cboBank.SelectedIndex = 0;
        this.cboPaymentDate.Value = DateTime.Now;
        this.txtTerminal.Text = "";
        this.txtUserBankCode.Text = "";
        bank2.Dispose();
        return bank1;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void validateRequesterData()
    {
      try
      {
        if (string.IsNullOrWhiteSpace(this.txtRequesterName.Text.Trim()))
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Confirma_Accion_Nombre);
        if (!this.IsValidName(this.txtRequesterName.Text.Trim()))
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Confirma_Cambios_Nombre);
        if (string.IsNullOrWhiteSpace(this.txtRequesterLast1.Text.Trim()))
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Confirma_Accion_ApellidoPaterno);
        if (!this.IsValidName(this.txtRequesterLast1.Text.Trim()))
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Confirma_Cambios_ApellidoPaterno);
        if (string.IsNullOrWhiteSpace(this.txtRequesterLast2.Text.Trim()))
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Confirma_Accion_ApellidoMaterno);
        if (!this.IsValidName(this.txtRequesterLast2.Text.Trim()))
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Confirma_Cambios_ApellidoMaterno);
        if (this.cboRequesterTypeDoc.SelectedIndex == 0)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Seleccione_Tipo_Documento);
        if (this.txtRequesterNumberDoc.Text.Trim() == "")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Ingrese_Documento);
        if (this.cboRequesterTypeDoc.SelectedValue == "1" && this.txtRequesterNumberDoc.Text.Trim().Length != 8)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_DNI_8);
        if (this.cboRequesterTypeDoc.SelectedValue == "2" && this.txtRequesterNumberDoc.Text.Trim().Length > 35)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_PASAPORTE_35 + " COMO MAXIMO");
        if (this.cboRequesterTypeDoc.SelectedValue == "3" && this.txtRequesterNumberDoc.Text.Trim().Length > 35)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_CAEXTRANJERIA_35 + " COMO MAXIMO");
        if (this.cboRequesterTypeDoc.SelectedValue == "4" && this.txtRequesterNumberDoc.Text.Trim().Length != 11)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_11);
        if (this.cboRequesterTypeDoc.SelectedValue == "4" && this.txtRequesterNumberDoc.Text.Trim().Substring(0, 1) != "1" && this.txtRequesterNumberDoc.Text.Trim().Substring(0, 1) != "2")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_Longitud);
        if (this.cboRequesterTypeDoc.SelectedValue == "4" && !this.ValidateRuc(this.txtRequesterNumberDoc.Text.Trim()))
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_Formato);
        if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.Duplicado) && this.txtRequesterPhone.Text.Trim() == "")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Ingrese_Telefono);
        if (this.Session["RequirementPlateType"] == null)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
        if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Regular))
        {
          this.redirectValidation(2);
        }
        else
        {
          if ((int) this.Session["RequirementPlateType"] != Convert.ToInt32((object) enmRequirementPlateType.Premium) && (int) this.Session["RequirementPlateType"] != Convert.ToInt32((object) enmRequirementPlateType.NotariaLiberada))
            return;
          if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.Inmatriculacion) || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 2 || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 3 || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 7)
            this.Wizard1.MoveTo((WizardStepBase) this.WS_ValidacionOrden);
          else
            this.Wizard1.MoveTo((WizardStepBase) this.WS_ValidacionPlaca);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void redirectValidation(int intAnswer)
    {
      try
      {
        this.Session["PaymentAnswer"] = (object) intAnswer;
        if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.Inmatriculacion) || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 2 || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 3 || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 7)
          this.Wizard1.MoveTo((WizardStepBase) this.WS_ValidacionOrden);
        else
          this.Wizard1.MoveTo((WizardStepBase) this.WS_ValidacionPlaca);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string ValidateExistRequirement(string strPlateNew, string strTitle)
    {
      try
      {
        int? nullable1 = new int?();
        string str = "";
        nullable1 = new int?(new RequirementQueriesBL().ValidateExistRequirementByPlateTitle(strPlateNew, strTitle));
        int? nullable2 = nullable1;
        if (nullable2.HasValue)
        {
          switch (nullable2.GetValueOrDefault())
          {
            case -4:
              str = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Cambio_Clase;
              break;
            case -3:
              str = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Transferencia_Placa_Nueva;
              break;
            case -2:
              str = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Tramite_Terminado;
              break;
            case -1:
              str = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Titulo_Tramite_Anterior;
              break;
            case 0:
              str = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Tramite_Curso;
              break;
          }
        }
        return str;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string ValidateSunarp(string strPlateNumber, string strTitle)
    {
      try
      {
        int? nullable1 = new int?();
        string str1 = "";
        nullable1 = new int?(new RequirementQueriesBL().ValidateExistSunarpByPlateTitle(strPlateNumber, strTitle));
        int? nullable2 = nullable1;
        if (nullable2.HasValue)
        {
          switch (nullable2.GetValueOrDefault())
          {
            case -4:
              this.lblMessage.Visible = true;
              str1 = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Gestor_Reclamo;
              break;
            case -3:
              this.lblMessage.Visible = true;
              DataTable activeByPlate = new RequirementClaimQueriesBL().RequirementClaimGetActiveByPlate(strPlateNumber);
              string str2 = activeByPlate.Rows[0]["v_ClaimCode"].ToString();
              str1 = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Reclamo + str2;
              activeByPlate.Dispose();
              break;
            case -2:
              this.lblMessage.Visible = true;
              str1 = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Registro_Encontrado;
              break;
            case -1:
              this.lblMessage.Visible = true;
              str1 = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Registro_No_Encontrado;
              break;
            case 0:
              this.lblMessage.Visible = true;
              str1 = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Titulo_Coinciden;
              break;
            case 1:
              this.lblMessage.Text = "";
              this.lblMessage.Visible = false;
              str1 = "";
              break;
          }
        }
        return str1;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void ShowVehicleData(string strPlateNumber, string strTitle)
    {
      try
      {
        string str = "";
        DataTable dataTable;
        if (this.isRegisterCall)
        {
          dataTable = new RequirementQueriesBL().SunarpDataRead(strPlateNumber, strTitle, this.isRegisterCall);
          if (this.StatusCallCenter == 2)
            this.Session["hdiPrice3"] = (object) new RequirementQueriesBL().SunarpDataRead(strPlateNumber, strTitle).Rows[0]["f_PriceSale"].ToString();
        }
        else
          dataTable = new RequirementQueriesBL().SunarpDataRead(strPlateNumber, strTitle);
        if (dataTable.Rows.Count > 0)
        {
          this.hidSunarpId.Value = dataTable.Rows[0][0].ToString();
          this.txtPlateNew.Text = dataTable.Rows[0]["v_PlateNew"].ToString();
          this.txtPlateTitle1.Text = dataTable.Rows[0]["v_TitleNumber"].ToString();
          this.txtPlateOld.Text = dataTable.Rows[0]["v_PlateOld"].ToString();
          this.txtBrand.Text = dataTable.Rows[0]["v_Brand"].ToString();
          this.txtModel.Text = dataTable.Rows[0]["v_Model"].ToString();
          this.txtRegistryZone.Text = dataTable.Rows[0]["registryZone"].ToString();
          this.txtRegistryOffice.Text = dataTable.Rows[0]["registryOffice"].ToString();
          this.txtUseType.Text = dataTable.Rows[0]["TypeUseDescrption"].ToString();
          this.Session["v_PlateNew"] = (object) dataTable.Rows[0]["v_PlateNew"].ToString();
          this.Session["ProcessId"] = (object) dataTable.Rows[0]["ProcessTypeId"].ToString();
          this.txtProcess.Text = dataTable.Rows[0]["ProcessType"].ToString();
          this.txtCategory.Text = dataTable.Rows[0]["SunarpCategory"].ToString();
          this.txtProduct.Text = dataTable.Rows[0]["v_Code"].ToString();
          this.txtPrice.Text = dataTable.Rows[0]["f_PriceSale"].ToString();
          this.txtSerialNumber.Text = dataTable.Rows[0]["v_SerialNumber"].ToString();
          this.txtDispatchDate.Text = Convert.ToDateTime(dataTable.Rows[0]["d_DispatchDate"].ToString()).ToString("dd/MM/yyyy");
          this.txtVehicleType.Text = dataTable.Rows[0]["v_VehicleType"].ToString();
          this.txtMotorNumber.Text = dataTable.Rows[0]["v_MotorNumber"].ToString();
          Decimal result = 0M;
          Decimal.TryParse(this.txtPrice.Text.ToString((IFormatProvider) CultureInfo.CurrentCulture), out result);
          this.ViewState["VehicleClassId"] = (object) dataTable.Rows[0]["ClassId"].ToString();
          this.ViewState["v_Igv"] = (object) dataTable.Rows[0]["v_Igv"].ToString();
          this.ViewState["f_PriceCost"] = (object) dataTable.Rows[0]["f_PriceCost"].ToString();
          this.ViewState["f_PriceCostF"] = (object) dataTable.Rows[0]["f_PriceCost"].ToString();
          this.hdiPrice.Value = result.ToString((IFormatProvider) CultureInfo.CurrentCulture);
          this.txtTotal.Text = result.ToString((IFormatProvider) CultureInfo.CurrentCulture);
          if (this.isRegisterCall && this.StatusCallCenter == 2)
            this.Session["f_PriceSale"] = (object) this.hdiPrice.Value;
          str = this.showAdditionalProducts(Convert.ToInt32(dataTable.Rows[0]["i_ProductId"].ToString()));
          DataTable ownersByIdSunarp = new RequirementQueriesBL().GetOwnersByIdSunarp(Convert.ToInt32(dataTable.Rows[0][0].ToString()));
          this.txtOwners.Text = "";
          this.txtDocumentType.Text = "";
          this.txtDocumentNumber.Text = "";
          if (ownersByIdSunarp.Rows.Count > 0)
          {
            for (int index = 0; index < ownersByIdSunarp.Rows.Count; ++index)
            {
              TextBox txtOwners = this.txtOwners;
              txtOwners.Text = txtOwners.Text + ownersByIdSunarp.Rows[index]["completeName"].ToString() + "\n";
              TextBox txtDocumentType = this.txtDocumentType;
              txtDocumentType.Text = txtDocumentType.Text + ownersByIdSunarp.Rows[index]["v_DocumentType"].ToString() + "\n";
              TextBox txtDocumentNumber = this.txtDocumentNumber;
              txtDocumentNumber.Text = txtDocumentNumber.Text + ownersByIdSunarp.Rows[index]["v_DocumentNumber"].ToString() + "\n";
            }
          }
          this.txtPlateNumber.Enabled = false;
          ownersByIdSunarp.Dispose();
        }
        dataTable.Dispose();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string showAdditionalProducts(int pintProductId)
    {
      try
      {
        string str = (string) null;
        int productCorrespondence = new RequirementQueriesBL().GetProductCorrespondence(pintProductId, 4);
        this.ViewState["intProductId"] = (object) productCorrespondence;
        if (productCorrespondence != 0)
          this.trAdditionalProducts.Visible = true;
        else
          this.trAdditionalProducts.Visible = false;
        this.Div2.Visible = true;
        return str;
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
        if (!(this.hidSunarpId.Value == "0"))
          return;
        string empty1 = string.Empty;
        string pstrVinSerie = string.Empty;
        string empty2 = string.Empty;
        string str;
        if (this.txtPlateNumber.Text.Replace("-", "") == "" || this.txtPlateNumber.Text.Replace("-", "") == string.Empty)
        {
          str = this.txtPlateNumber1.Text.Replace("-", "");
          pstrVinSerie = this.txtVinSerie.Text.ToString().ToUpper();
        }
        else
          str = this.txtPlateNumber.Text.Replace("-", "");
        if (string.IsNullOrEmpty(str))
          Message.SetMessage(this.lblMessage, new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Vacia));
        else if (string.IsNullOrEmpty(pstrVinSerie))
          Message.SetMessage(this.lblMessage, new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Vin_o_Serie_Vacio));
        else if (new RequirementQueriesBL().ValidateExistSunarpByPlateVinSerie(str, pstrVinSerie) == 1)
        {
          this.lblMessage.Text = "";
          this.lblMessage.Visible = false;
          switch (new RequirementQueriesBL().Verify3rdPlate(str))
          {
            case -2:
              if (this.Session["ProcessId"] != null)
              {
                if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) != Convert.ToInt32((object) enmProccessType.CambioUsoRectificacion))
                {
                  new RequirementClaimQueriesBL().RequirementClaimGetActiveByPlate(str).Rows[0]["v_ClaimCode"].ToString();
                  throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Reclamo);
                }
              }
              else
                this.Response.Write("No se cargo ningún tipo de Proceso");
              break;
            case -1:
              throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Tramite_Previo);
            case 0:
              throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Tramite_Curso);
          }
          if (this.Session["ProcessId"] != null)
          {
            if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.CambioUsoRectificacion))
            {
              DataTable dataTable = new RequirementClaimQueriesBL().ValidateRequirementClaimChangeUsebyPlate(str);
              switch (Convert.ToInt32(dataTable.Rows[0]["Result"].ToString()))
              {
                case 0:
                  throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Sin_Reclamo);
                case 1:
                  throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Reclamo_No_Aprobado);
                default:
                  this.Session["ClaimCode"] = (object) dataTable.Rows[0]["ClaimCode"].ToString();
                  this.Session["VehicleIdClaim"] = (object) dataTable.Rows[0]["VehicleId"].ToString();
                  dataTable.Dispose();
                  break;
              }
            }
          }
          else
            this.Response.Write("No se cargo ningún tipo de Proceso");
          int num = 1;
          if (this.Session["ProcessId"] != null)
          {
            int int16 = (int) Convert.ToInt16(this.Session["ProcessId"].ToString());
            if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.Duplicado3rd))
              num = new RequirementQueriesBL().VerifyProcess3rdPlate(str);
            if (num == 0)
              throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Sin_Tercera_Placa);
            this.ShowVehicleData3rd();
          }
          else
            this.Response.Write("No se cargo ningún tipo de Proceso");
        }
        else
        {
          this.lblMessage.Visible = true;
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_VinSerie_No_Coinciden);
        }
        if (this.Session["PaymentAnswer"] != null)
        {
          if ((int) this.Session["PaymentAnswer"] == Convert.ToInt32((object) enmAnswer.Yes) && (int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Regular))
            this.Wizard1.MoveTo((WizardStepBase) this.WS_AcreditacionPago);
          else
            this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosVehiculo2);
        }
        else
          this.Response.Write("No se cargo ningún tipo de Proceso");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void redirectPaymentAcreditation()
    {
      try
      {
        if (this.Session["ProcessId"] == null)
          throw new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
        if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.Inmatriculacion) || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 2 || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 3 || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 7)
          this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosVehiculo1);
        else
          this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosVehiculo2);
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
        string ErrorMessage = this.ValidateOwnersDocuments();
        if (this.Wizard1.ActiveStep == this.WS_DatosVehiculo1)
        {
          if (this.txtOwners.Text.Trim() == "")
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Propietario_vacio);
          if (this.txtDocumentType.Text.Trim() == "")
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_TDocumento_vacio);
          if (this.txtDocumentNumber.Text.Trim() == "")
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_NDocumento_vacio);
          if (this.txtBrand.Text.Trim() == "")
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Marca_vacio);
          if (this.txtModel.Text.Trim() == "")
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Modelo_vacio);
          if (this.txtSerialNumber.Text.Trim().Length <= 2)
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_NSerie_vacio);
          if (!this.chkAccept.Checked)
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Confirma_Accion);
          if (ErrorMessage.Length > 0)
            throw new HandledException(1, ErrorMessage);
        }
        if (this.Wizard1.ActiveStep == this.WS_DatosVehiculo2)
        {
          if (this.txtOwner3rd.Text.Trim() == "")
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Propietario_vacio);
          if (this.txtBrand3rd.Text.Trim() == "")
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Marca_vacio);
          if (this.txtModel3rd.Text.Trim() == "")
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Modelo_vacio);
          if (this.txtSerialNumber3rd.Text.Trim() == "")
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_NSerie_vacio);
          if (!this.chkAccept3rd.Checked)
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Confirma_Accion);
          if (ErrorMessage.Length > 0)
            throw new HandledException(1, ErrorMessage);
        }
        this.showOwnerData();
        if ((int) this.Session["RequirementPlateType"] != Convert.ToInt32((object) enmRequirementPlateType.Premium))
        {
          this.objUserBE = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterRequirement.aspx");
          if (this.objUserBE.i_RoleConfigId == 30)
            this.CheckhasnotEmail.Visible = false;
          else
            this.CheckhasnotEmail.Visible = true;
          this.Wizard1.MoveTo((WizardStepBase) this.WS_SeleccionComprobante);
        }
        else
        {
          this.showFinalData((int) Convert.ToInt16(this.Session["ProcessId"].ToString()), (int) Convert.ToInt16(this.ViewState["VehicleClassId"].ToString()));
          this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosFinales);
        }
        this.ShowIconValidateDocNumRUC();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string ValidateOwnersDocuments()
    {
      try
      {
        string str = "";
        DataTable ownersByIdSunarp = new RequirementQueriesBL().GetOwnersByIdSunarp(Convert.ToInt32(this.hidSunarpId.Value));
        if (ownersByIdSunarp.Rows.Count > 0)
        {
          for (int index = 0; index < ownersByIdSunarp.Rows.Count; ++index)
          {
            if ((Convert.ToInt32(ownersByIdSunarp.Rows[index]["v_DocumentTypeId"]) == 1 || Convert.ToInt32(ownersByIdSunarp.Rows[index]["v_DocumentTypeId"]) == 4) && !Regex.IsMatch(ownersByIdSunarp.Rows[index]["v_DocumentNumber"].ToString().Trim(), "^[0-9]+$"))
            {
              str = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Formato_incorrecto;
              break;
            }
            if (Convert.ToInt32(ownersByIdSunarp.Rows[index]["v_DocumentTypeId"]) == 1 && ownersByIdSunarp.Rows[index]["v_DocumentNumber"].ToString().Trim().Length != 8)
            {
              str = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Longitud_8_Incorrecta;
              break;
            }
            if (Convert.ToInt32(ownersByIdSunarp.Rows[index]["v_DocumentTypeId"]) == 4 && ownersByIdSunarp.Rows[index]["v_DocumentNumber"].ToString().Trim().Length != 11)
            {
              str = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Longitud_11_Incorrecta;
              break;
            }
          }
        }
        else
          str = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO;
        return str;
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
        DataTable ownersByIdSunarp = new RequirementQueriesBL().GetOwnersByIdSunarp(Convert.ToInt32(this.hidSunarpId.Value));
        this.ViewState["VehicleClassId"] = (object) ownersByIdSunarp.Rows[0]["ClassId"].ToString();
        if (ownersByIdSunarp.Rows.Count > 0)
        {
          this.txtBeneficiaryName.Text = ownersByIdSunarp.Rows[0]["CompleteName"].ToString();
          this.txtBeneficiaryMail.Text = this.Session["EmailSoli"].ToString();
          int int32 = Convert.ToInt32(ownersByIdSunarp.Rows[0]["v_DocumentTypeId"].ToString());
          if (int32 == 4)
          {
            this.rdbProofPayment.SelectedIndex = 0;
            if (this.CboDocumentType.Items.FindByValue(int32.ToString((IFormatProvider) CultureInfo.CurrentCulture)) != null)
            {
              this.CboDocumentType.SelectedIndex = Convert.ToInt32(this.CboDocumentType.Items.FindByValue(int32.ToString()).Value);
              this.CboDocumentType.Enabled = false;
            }
            this.TxtDocNumberProofPaper.Text = ownersByIdSunarp.Rows[0]["v_DocumentNumber"].ToString();
          }
          else
          {
            this.rdbProofPayment.SelectedIndex = 1;
            this.CboDocumentType.Enabled = true;
            this.CboDocumentType.Items.FindByValue("4").Enabled = false;
            this.CboDocumentType.SelectedIndex = 0;
            if ((int32 == 1 || int32 == 19) && this.CboDocumentType.Items.FindByValue(int32.ToString((IFormatProvider) CultureInfo.CurrentCulture)) != null)
            {
              this.CboDocumentType.SelectedIndex = Convert.ToInt32(this.CboDocumentType.Items.FindByValue(int32.ToString()).Value);
              if (this.CboDocumentType.SelectedIndex > 0)
                this.TxtDocNumberProofPaper.Text = ownersByIdSunarp.Rows[0]["v_DocumentNumber"].ToString();
            }
          }
          string str;
          if (this.rdbProofPayment.SelectedIndex == 0)
          {
            str = "4";
            this.CboDocumentType.Items.FindByValue("4").Enabled = true;
            this.CboDocumentType.SelectedValue = str;
          }
          else
          {
            str = "1";
            this.CboDocumentType.SelectedValue = str;
          }
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("<script language='javascript'>");
          stringBuilder.Append("index2='" + str + "';");
          stringBuilder.Append("</script>");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "JSCR", stringBuilder.ToString(), false);
        }
        ownersByIdSunarp.Dispose();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void Validate3rdData()
    {
      try
      {
        if (this.Session["ProcessId"] == null)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
        if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.CambioUso) && this.cboUseNew.SelectedIndex == 0)
          throw new HandledException(1, "NO ES UNA PLACA DE EMERGENCIA VALIDA.");
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
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Ingrese_Nombre_Razon);
        if (this.txtAddress.Text == "")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Ingrese_Direccion);
        if (this.CboDocumentType.DataTextField == "" || this.CboDocumentType.SelectedIndex == 0)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Seleccione_Tipo_Documento);
        if (this.TxtDocNumberProofPaper.Text.Trim() == "")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Ingrese_Documento);
        if (this.CboDocumentType.SelectedValue == "1" && this.TxtDocNumberProofPaper.Text.Trim().Length != 8)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_DNI_8);
        if (this.CboDocumentType.SelectedValue == "1" && this.TxtDocNumberProofPaper.Text.Trim() == "00000000")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_DNI_FORMATO_CORRECTO);
        if (this.CboDocumentType.SelectedValue == "3" && this.TxtDocNumberProofPaper.Text.Trim().Length < 9)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_DOC_9);
        if (this.CboDocumentType.SelectedValue == "4" && this.TxtDocNumberProofPaper.Text.Trim().Length != 11)
        {
          this.lblMessage1.Text = "";
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_11);
        }
        if (this.CboDocumentType.SelectedValue == "4" && this.TxtDocNumberProofPaper.Text.Trim().Substring(0, 1) != "1" && this.TxtDocNumberProofPaper.Text.Trim().Substring(0, 1) != "2")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_Longitud);
        if (this.CboDocumentType.SelectedValue == "4" && !this.ValidateRuc(this.TxtDocNumberProofPaper.Text.Trim()))
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_Formato);
        if (this.CboDocumentType.SelectedValue == "19" && this.TxtDocNumberProofPaper.Text.Trim().Length != 12)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_DOC_12);
        if (this.cboDeliveryPoint.SelectedIndex == 0)
        {
          if (this.RucValidation == 1 && this.CboDocumentType.SelectedValue == "4" && this.ViewState["ValidationRUC"] != null && (int) this.ViewState["ValidationRUC"] == 1)
            this.txtBeneficiaryName.Enabled = false;
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Seleccione_Punto_Entrega);
        }
        if (this.txtDocumentNumber.Text.Trim() == SIIV.SystemParameter.BL.Constants.GENERAL_RUC_AAP)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_Prohibido);
        if (!this.RestrictionRazonSocial())
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Razon_Social_Prohibido);
        if (this.CboDocumentType.SelectedValue == "4" && this.txtAddress.Text.Trim() == "")
        {
          if (this.RucValidation == 1 && this.ViewState["ValidationRUC"] != null && (int) this.ViewState["ValidationRUC"] == 1)
            this.txtBeneficiaryName.Enabled = false;
          throw new HandledException(1, "DEBE INGRESAR SU DIRECCIÓN");
        }
        if (!new Email().IsValidEmail(this.txtBeneficiaryMail.Text.Trim()) && !string.IsNullOrEmpty(this.txtBeneficiaryMail.Text.Trim()))
        {
          if (this.RucValidation == 1 && this.CboDocumentType.SelectedValue == "4" && this.ViewState["ValidationRUC"] != null && (int) this.ViewState["ValidationRUC"] == 1)
            this.txtBeneficiaryName.Enabled = false;
          throw new HandledException(1, "Debe Ingresar un Email Valido");
        }
        if ((this.CboDocumentType.SelectedValue == "1" || this.CboDocumentType.SelectedValue == "4") && !Regex.IsMatch(this.TxtDocNumberProofPaper.Text.Trim(), "^[0-9]+$"))
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_DNI_Numero);
        this.ShowIconValidateDocNumRUC();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void showFinalData(int pintProcessType, int VehicleClass)
    {
      try
      {
        if (pintProcessType == 5 || pintProcessType == 2 || pintProcessType == 3 || pintProcessType == 7 || pintProcessType == 6)
        {
          this.lblTitleRequirement.Text = SIIV.SystemParameter.BL.Constants.REQUIREMENT_TITLE_Nueva_Placa;
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
          this.lblEmail.Text = this.txtBeneficiaryMail.Text;
        }
        else
        {
          switch (pintProcessType)
          {
            case 1:
              this.lblTitleRequirement.Text = SIIV.SystemParameter.BL.Constants.REQUIREMENT_TITLE_Duplicado_Placa_Nuevo_Modelo;
              this.TagUseNew.Visible = false;
              break;
            case 4:
              this.lblTitleRequirement.Text = SIIV.SystemParameter.BL.Constants.REQUIREMENT_TITLE_Duplicado_Tercera_Placa;
              this.TagUseNew.Visible = false;
              break;
            case 6:
              this.lblTitleRequirement.Text = SIIV.SystemParameter.BL.Constants.REQUIREMENT_TITLE_Cambio_Uso_Nuevo_Modelo;
              this.TagUseNew.Visible = true;
              this.lblUseNew.Text = this.cboUseNew.SelectedItem.ToString();
              break;
            case 10:
              this.lblTitleRequirement.Text = SIIV.SystemParameter.BL.Constants.REQUIREMENT_TITLE_Cambio_Uso_Rectificacion_Sunarp;
              this.TagUseNew.Visible = true;
              this.lblUseNew.Text = this.txtUseNew.Text.Trim();
              break;
          }
          this.lblPlateNumber.Text = this.txtPlateNew3rd.Text;
          this.lblPlateNumberOld.Text = this.txtPlateOld3rd.Text;
          this.lblBrand.Text = this.txtBrand3rd.Text;
          this.lblModel.Text = this.txtModel3rd.Text;
          this.lblCategory.Text = this.txtCategory3rd.Text;
          this.lblUseType.Text = this.txtUseType3rd.Text;
          this.lblSerialNumber.Text = this.txtSerialNumber3rd.Text;
          this.lblEmail.Text = this.txtBeneficiaryMail.Text;
          DataTable ownersByIdSunarp = new RequirementQueriesBL().GetOwnersByIdSunarp(Convert.ToInt32(this.hidSunarpId.Value));
          this.lblOwner.Text = "";
          this.lblOwnerDocumentType.Text = "";
          this.lblOwnerDocumentNumber.Text = "";
          if (ownersByIdSunarp.Rows.Count > 0)
          {
            for (int index = 0; index < ownersByIdSunarp.Rows.Count; ++index)
            {
              Label lblOwner = this.lblOwner;
              lblOwner.Text = lblOwner.Text + ownersByIdSunarp.Rows[index]["completeName"].ToString() + "<br/>";
              Label ownerDocumentType = this.lblOwnerDocumentType;
              ownerDocumentType.Text = ownerDocumentType.Text + ownersByIdSunarp.Rows[index]["v_DocumentType"].ToString() + "<br/>";
              Label ownerDocumentNumber = this.lblOwnerDocumentNumber;
              ownerDocumentNumber.Text = ownerDocumentNumber.Text + ownersByIdSunarp.Rows[index]["v_DocumentNumber"].ToString() + "<br/>";
            }
          }
          ownersByIdSunarp.Dispose();
        }
        if ((int) this.Session["PaymentAnswer"] == 1)
          this.trAdditionalProducts.Visible = true;
        this.lblProofPaymentType.Text = this.rdbProofPayment.SelectedItem.Text;
        this.lblBeneficiary.Text = this.txtBeneficiaryName.Text;
        this.lblBeneficiaryDumentType.Text = this.CboDocumentType.SelectedItem.Text;
        this.lblbeneficiaryDocumentNumber.Text = this.TxtDocNumberProofPaper.Text;
        this.lblAddress.Text = this.txtAddress.Text;
        this.Session["ClientMailforVISA"] = (object) this.txtBeneficiaryMail.Text;
        this.Session["ClientTypeDocumentforVISA"] = Convert.ToInt32(this.CboDocumentType.SelectedValue) == 1 ? (object) "DNI" : (Convert.ToInt32(this.CboDocumentType.SelectedValue) == 2 ? (object) "PAS" : (Convert.ToInt32(this.CboDocumentType.SelectedValue) == 3 ? (object) "CEX" : (Convert.ToInt32(this.CboDocumentType.SelectedValue) == 4 ? (object) "RUC" : (object) "CEX")));
        this.Session["ClientNumberDocumentforVISA"] = (object) this.TxtDocNumberProofPaper.Text;
        this.objUserBE = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterRequirement.aspx");
        this.Session["ClientIdforVISA"] = (object) this.objUserBE.i_SystemUserId;
        this.Session["ClientTypeRegisterforVISA"] = (object) "Registrado";
        DateTime now = DateTime.Now;
        DateTime? dInsertDate = this.objUserBE.d_InsertDate;
        TimeSpan? nullable = dInsertDate.HasValue ? new TimeSpan?(now - dInsertDate.GetValueOrDefault()) : new TimeSpan?();
        ref TimeSpan? local = ref nullable;
        this.Session["ClientTotalDaysRegisterforVISA"] = (object) Convert.ToInt32((object) (local.HasValue ? new double?(local.GetValueOrDefault().TotalDays) : new double?()));
        if ((int) this.Session["RequirementPlateType"] == Convert.ToInt32((object) enmRequirementPlateType.Premium))
          this.cboDeliveryPoint.SelectedIndex = 1;
        this.lblDeliveryPoint.Text = this.cboDeliveryPoint.SelectedItem.Text;
        int int32_1 = Convert.ToInt32(this.cboDeliveryPoint.SelectedValue.ToString());
        this.chkPortaMoto.Checked = false;
        this.Session.Remove("PuntoEntrega");
        DataTable dataTable1 = new RequirementQueriesBL().ValidateDeliveryZone(Convert.ToInt32(int32_1));
        int int32_2 = Convert.ToInt32(dataTable1.Rows[0]["i_DeliveryReferenceId"]);
        int int32_3 = Convert.ToInt32(dataTable1.Rows[0]["i_DefaultServiceDeliveryAuto"]);
        int int32_4 = Convert.ToInt32(dataTable1.Rows[0]["i_DefaultServiceDeliveryMoto"]);
        int int32_5 = Convert.ToInt32(dataTable1.Rows[0]["i_ExistsServiceDeliveryAuto"]);
        int int32_6 = Convert.ToInt32(dataTable1.Rows[0]["i_ExistsServiceDeliveryMoto"]);
        int int32_7 = Convert.ToInt32(dataTable1.Rows[0]["i_MinServiceDeliveryAuto"]);
        int int32_8 = Convert.ToInt32(dataTable1.Rows[0]["i_MinServiceDeliveryMoto"]);
        this.Session["PuntoEntrega"] = (object) Convert.ToInt32(int32_1);
        this.LblDelivery.Text = new RequirementQueriesBL().DeliveryDireccion(Convert.ToInt32(int32_1));
        if (int32_2 > 0 && (VehicleClass != 5 && int32_5 == 1 || VehicleClass == 5 && int32_6 == 1))
        {
          if (VehicleClass != 5)
          {
            if (pintProcessType == 1 || pintProcessType == 2 || pintProcessType == 3 || pintProcessType == 5 || pintProcessType == 6 || pintProcessType == 7)
            {
              this.divDatosDelivery.Visible = true;
              this.chkConfirmPreview.Checked = false;
              this.chkYes.Checked = false;
              this.chkNo.Checked = false;
              this.chkDelivery1.Checked = false;
              this.chkPortaMoto.Checked = false;
              this.trAdditionalProducts.Visible = false;
              this.contAdd.Visible = false;
              this.divImgPortaplaca.Visible = false;
              this.divImgDelivery.Visible = false;
              this.divImgPortaPlacaMoto.Visible = false;
              this.DivDelivery.Visible = false;
              this.Div2.Visible = true;
              this.txtTotal.Text = this.hdiPrice.Value.ToString();
              this.getDistrictbyClasification(1);
              short int16 = Convert.ToInt16(this.Session["ProcessId"].ToString());
              DataTable dataTable2 = new DataTable();
              int productCorrespondence = new RequirementQueriesBL().GetProductCorrespondence(Convert.ToInt32((int16 != (short) 7 && int16 != (short) 1 && int16 != (short) 3 && (int) int16 != Convert.ToInt32((object) enmProccessType.Inmatriculacion) && int16 != (short) 2 ? ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) != Convert.ToInt32((object) enmProccessType.CambioUso) ? new RequirementQueriesBL().SunarpDataRead3rd(this.txtPlateNumber.Text.Trim()) : (!this.isRegisterCall ? new RequirementQueriesBL().SunarpDataReadChangeUse(this.txtPlateNumber.Text.Trim()) : new RequirementQueriesBL().SunarpDataReadChangeUse(this.txtPlateNumber.Text.Trim(), this.isRegisterCall))) : (!this.isRegisterCall ? new RequirementQueriesBL().SunarpDataReadbyId(Convert.ToInt32(this.hidSunarpId.Value)) : new RequirementQueriesBL().SunarpDataReadbyId(Convert.ToInt32(this.hidSunarpId.Value), this.isRegisterCall))).Rows[0]["i_ProductId"].ToString()), int32_3);
              this.ViewState["i_ProductId"] = (object) productCorrespondence;
              if (productCorrespondence != 0)
              {
                new RequirementQueriesBL().GetPriceServiceDelivery(Convert.ToInt32(this.ViewState["i_ProductId"]), int32_3);
                this.lblPriceDeliver.Text = int32_7.ToString();
              }
            }
            else
            {
              this.trAdditionalProducts.Visible = false;
              this.divDatosDelivery.Visible = false;
              this.contAdd.Visible = false;
              this.divImgPortaplaca.Visible = false;
              this.Div2.Visible = false;
            }
          }
          else if (pintProcessType == 1 || pintProcessType == 2 || pintProcessType == 3 || pintProcessType == 5 || pintProcessType == 6 || pintProcessType == 7)
          {
            this.trAdditionalProducts.Visible = false;
            this.divDatosDelivery.Visible = true;
            this.chkConfirmPreview.Checked = false;
            this.chkYes.Checked = false;
            this.chkNo.Checked = false;
            this.chkDelivery1.Checked = false;
            this.chkPortaMoto.Checked = false;
            this.contAddMoto.Visible = false;
            this.divImgPortaplaca.Visible = false;
            this.divImgDelivery.Visible = false;
            this.divImgPortaPlacaMoto.Visible = false;
            this.DivDelivery.Visible = false;
            this.Div2.Visible = true;
            this.txtTotal.Text = this.hdiPrice.Value.ToString();
            this.getDistrictbyClasification(2);
            short int16 = Convert.ToInt16(this.Session["ProcessId"].ToString());
            DataTable dataTable3 = new DataTable();
            int productCorrespondence = new RequirementQueriesBL().GetProductCorrespondence(Convert.ToInt32((int16 != (short) 7 && int16 != (short) 1 && int16 != (short) 3 && (int) int16 != Convert.ToInt32((object) enmProccessType.Inmatriculacion) && int16 != (short) 2 ? ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) != Convert.ToInt32((object) enmProccessType.CambioUso) ? new RequirementQueriesBL().SunarpDataRead3rd(this.txtPlateNumber.Text.Trim()) : (!this.isRegisterCall ? new RequirementQueriesBL().SunarpDataReadChangeUse(this.txtPlateNumber.Text.Trim()) : new RequirementQueriesBL().SunarpDataReadChangeUse(this.txtPlateNumber.Text.Trim(), this.isRegisterCall))) : (!this.isRegisterCall ? new RequirementQueriesBL().SunarpDataReadbyId(Convert.ToInt32(this.hidSunarpId.Value)) : new RequirementQueriesBL().SunarpDataReadbyId(Convert.ToInt32(this.hidSunarpId.Value), this.isRegisterCall))).Rows[0]["i_ProductId"].ToString()), int32_4);
            this.ViewState["i_ProductId"] = (object) productCorrespondence;
            if (productCorrespondence != 0)
            {
              new RequirementQueriesBL().GetPriceServiceDelivery(Convert.ToInt32(this.ViewState["i_ProductId"]), int32_4);
              this.lblPriceDeliver.Text = int32_8.ToString();
            }
          }
          else
          {
            this.contAddMoto.Visible = false;
            this.divImgPortaplaca.Visible = false;
            this.Div2.Visible = false;
            this.divDatosDelivery.Visible = false;
            this.trAdditionalProducts.Visible = false;
          }
        }
        else
        {
          this.trTorni.Visible = false;
          if (VehicleClass != 5)
            this.divImgPortaplaca.Visible = true;
          else
            this.divImgPortaPlacaMoto.Visible = true;
          this.DivDelivery.Visible = false;
          this.divImgDelivery.Visible = false;
          this.divDatosDelivery.Visible = false;
          this.CheckDelivery.Visible = false;
          this.chkYes.Checked = false;
          this.chkDelivery1.Visible = false;
          this.chkDelivery1.Checked = false;
          this.chkPorta.Checked = false;
          this.chkTorni.Checked = false;
          this.btnFinish = this.GetControlFromWizard(this.Wizard1, RegisterRequirement.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnFinish") as Button;
          this.btnFinish.Text = "  Siguiente";
          this.Div2.Visible = true;
        }
        if (Convert.ToInt32(this.ViewState["VehicleClassId"]) == 5)
        {
          if (pintProcessType == 4)
          {
            this.contAddMoto.Visible = false;
            this.contAdd.Visible = false;
            this.Div2.Visible = true;
            this.txtTotal.Text = this.hdiPrice.Value;
          }
          else if (int32_2 > 0 && Convert.ToInt32(this.ViewState["i_ProductId"]) != 0)
          {
            this.contAdd.Visible = false;
            this.contAddMoto.Visible = false;
            this.txtTotal.Text = this.hdiPrice.Value;
          }
          else
          {
            this.trAdditionalProducts.Visible = true;
            this.divDatosDelivery.Visible = false;
            this.chkYes.Checked = false;
            this.chkNo.Checked = false;
            this.chkDelivery1.Checked = false;
            this.contAddMoto.Visible = true;
            this.Div2.Visible = true;
            this.divImgPortaPlacaMoto.Visible = true;
            this.divImgDelivery.Visible = false;
            this.txtTotal.Text = this.hdiPrice.Value;
          }
        }
        else if (Convert.ToInt32(this.ViewState["intProductId"]) == 0)
        {
          this.contAdd.Visible = true;
          this.Div2.Visible = true;
          this.divImgPortaplaca.Visible = true;
          this.divImgPortaPlacaMoto.Visible = false;
          this.txtTotal.Text = this.hdiPrice.Value;
        }
        else if (pintProcessType == 4)
        {
          this.contAddMoto.Visible = false;
          this.contAdd.Visible = false;
          this.Div2.Visible = true;
          this.txtTotal.Text = this.hdiPrice.Value;
        }
        else if (int32_2 > 0 && Convert.ToInt32(this.ViewState["i_ProductId"]) != 0)
        {
          this.contAddMoto.Visible = false;
          this.contAdd.Visible = false;
          this.txtTotal.Text = this.hdiPrice.Value;
        }
        else
        {
          this.trAdditionalProducts.Visible = true;
          this.divDatosDelivery.Visible = false;
          this.chkYes.Checked = false;
          this.chkNo.Checked = false;
          this.chkDelivery1.Checked = false;
          this.contAdd.Visible = true;
          this.Div2.Visible = true;
          this.divImgPortaplaca.Visible = true;
          this.divImgDelivery.Visible = false;
          this.txtTotal.Text = this.hdiPrice.Value;
        }
        this.lblDeliveryPointAddress.Text = new RequirementQueriesBL().GetLocation(this.cboDeliveryPoint.SelectedItem.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture), "", "").Rows[1]["v_Address"].ToString();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void registerClaim_DataNotFound()
    {
      try
      {
        ClaimGenerator claimGenerator = new ClaimGenerator();
        this.objUserBE = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterRequirement.aspx");
        if (this.Session["ProcessId"] != null)
        {
          string pstrPlateNumber;
          string str;
          if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.Inmatriculacion) || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 2 || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 3 || Convert.ToInt16(this.Session["ProcessId"].ToString()) == (short) 7)
          {
            pstrPlateNumber = this.txtPlateNumber.Text.Trim();
            str = this.txtTitleNumber.Text.Trim();
          }
          else
          {
            pstrPlateNumber = this.txtPlateNumber.Text.Trim();
            str = "-";
          }
          if (new RequirementQueriesBL().ValidateExistSunarpByPlateTitle(pstrPlateNumber, str) == 1)
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Encontrada_No_Reclamo);
          int systemUserPublic = new RequirementQueriesBL().GetSystemUserPublic(this.objUserBE.i_SystemUserId, 1);
          string pstrRequesterFirstName = "";
          string pstrRequesterLastName = "";
          string pstrRequesterDocumentNumber = "";
          string pstrRequesterEmail = "";
          int num = -1;
          if (systemUserPublic == 1)
          {
            pstrRequesterFirstName = this.objUserBE.v_FirstName;
            pstrRequesterLastName = this.objUserBE.v_LastName;
            pstrRequesterDocumentNumber = this.objUserBE.v_DocumentNumber;
            num = this.objUserBE.i_DocumentTypeId.Value;
            pstrRequesterEmail = this.objUserBE.v_Email;
          }
          this.CreatePopUp(claimGenerator.GenerateClaim_DataNoFound_PopUp(pstrPlateNumber, str, pstrRequesterFirstName, pstrRequesterLastName, num.ToString((IFormatProvider) CultureInfo.CurrentCulture), pstrRequesterDocumentNumber, pstrRequesterEmail, ""), "Información No Encontrada", "575", "515");
        }
        else
          Message.SetMessage(this.lblMessage, new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void registerClaim_DataNoAgree()
    {
      try
      {
        ClaimGenerator claimGenerator = new ClaimGenerator();
        this.objUserBE = this.Session["SystemUser"] as SystemUser;
        DataTable dataTable1 = new DataTable();
        if (this.Session["ProcessId"] != null)
        {
          DataTable dataTable2 = (int) Convert.ToInt16(this.Session["ProcessId"].ToString()) != Convert.ToInt32((object) enmProccessType.Inmatriculacion) && Convert.ToInt16(this.Session["ProcessId"].ToString()) != (short) 2 && Convert.ToInt16(this.Session["ProcessId"].ToString()) != (short) 3 && Convert.ToInt16(this.Session["ProcessId"].ToString()) != (short) 7 ? ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) != Convert.ToInt32((object) enmProccessType.CambioUso) ? new RequirementQueriesBL().SunarpDataRead3rd(this.txtPlateNumber1.Text.Trim()) : (!this.isRegisterCall ? new RequirementQueriesBL().SunarpDataReadChangeUse(this.txtPlateNumber.Text.Trim()) : new RequirementQueriesBL().SunarpDataReadChangeUse(this.txtPlateNumber.Text.Trim(), this.isRegisterCall))) : (!this.isRegisterCall ? new RequirementQueriesBL().SunarpDataReadbyId(Convert.ToInt32(this.hidSunarpId.Value)) : new RequirementQueriesBL().SunarpDataReadbyId(Convert.ToInt32(this.hidSunarpId.Value), this.isRegisterCall));
          if (dataTable2.Rows.Count <= 0)
            throw new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
          string pstrPlateNumber = dataTable2.Rows[0]["v_platenew"].ToString();
          string pstrPlateOld = dataTable2.Rows[0]["v_plateold"].ToString();
          string pstrVehicleModel = dataTable2.Rows[0]["v_model"].ToString();
          string pstrVehicleBrand = dataTable2.Rows[0]["v_Brand"].ToString();
          string pstrVehicleSerial = dataTable2.Rows[0]["v_serialNumber"].ToString();
          string str1 = !(dataTable2.Rows[0]["CategoryId"].ToString() != "") ? (string) null : dataTable2.Rows[0]["CategoryId"].ToString();
          string pstrCategory = !(dataTable2.Rows[0]["Category"].ToString() != "") ? "-" : dataTable2.Rows[0]["Category"].ToString();
          string pstrTitleNumber = Convert.ToInt16(this.Session["ProcessId"].ToString()) != (short) 5 ? "-" : dataTable2.Rows[0]["v_titlenumber"].ToString();
          DataTable ownersByIdSunarp = new RequirementQueriesBL().GetOwnersByIdSunarp(Convert.ToInt32(this.hidSunarpId.Value));
          if (ownersByIdSunarp.Rows.Count <= 0)
            throw new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
          string str2 = "";
          string str3 = "";
          string str4 = "";
          string str5 = "";
          for (int index = 0; index < ownersByIdSunarp.Rows.Count; ++index)
          {
            str2 = str2 + "/" + ownersByIdSunarp.Rows[index]["CompleteName"].ToString();
            str3 = str3 + "/" + ownersByIdSunarp.Rows[index]["v_DocumentNumber"].ToString();
            str4 = str4 + "/" + ownersByIdSunarp.Rows[index]["v_DocumentTypeId"].ToString();
            str5 = str5 + "/" + ownersByIdSunarp.Rows[index]["i_Item"].ToString();
          }
          ownersByIdSunarp.Dispose();
          string pstrCategoryGroup = "204";
          str2.Substring(1, str2.Length - 1);
          str3.Substring(1, str3.Length - 1);
          str4.Substring(1, str4.Length - 1);
          str5.Substring(1, str5.Length - 1);
          int systemUserPublic = new RequirementQueriesBL().GetSystemUserPublic(this.objUserBE.i_SystemUserId, 1);
          string pstrRequesterFirstName = "";
          string pstrRequesterLastName = "";
          string pstrRequesterDocumentNumber = "";
          string pstrRequesterEmail = "";
          int num = -1;
          if (systemUserPublic == 1)
          {
            pstrRequesterFirstName = this.objUserBE.v_FirstName;
            pstrRequesterLastName = this.objUserBE.v_LastName;
            pstrRequesterDocumentNumber = this.objUserBE.v_DocumentNumber;
            num = this.objUserBE.i_DocumentTypeId.Value;
            pstrRequesterEmail = this.objUserBE.v_Email;
          }
          this.CreatePopUp(claimGenerator.GenerateClaim_DataNoAgree_PopUp(pstrPlateNumber, pstrTitleNumber, pstrVehicleModel, pstrVehicleBrand, pstrVehicleSerial, this.hidSunarpId.Value, "", "", "", pstrPlateOld, pstrRequesterFirstName, pstrRequesterLastName, num.ToString((IFormatProvider) CultureInfo.CurrentCulture), pstrRequesterDocumentNumber, pstrRequesterEmail, "", Convert.ToString(str1), pstrCategory, pstrCategoryGroup), "Información No Conforme", "740", "880");
        }
        else
          this.Response.Write(SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_Consulta_erronea);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void VerifyDataBank()
    {
      try
      {
        Button controlFromWizard = this.GetControlFromWizard(this.Wizard1, RegisterRequirement.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnNext") as Button;
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

    private Control GetControlFromWizard(
      Wizard wizard,
      RegisterRequirement.WizardNavigationTempContainer wzdTemplate,
      string controlName)
    {
      try
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append((object) wzdTemplate);
        stringBuilder.Append("$");
        stringBuilder.Append(controlName);
        return wizard.FindControl(stringBuilder.ToString());
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void btnJavaScriptResponse_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.Session["ClaimRegistered"] == null)
          throw new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
        if ((int) this.Session["ClaimRegistered"] != 1)
          return;
        this.Response.Redirect("~/Requirement/BeginRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"]));
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

    private void CreatePopUp(string url, string pstrtitle, string width, string height)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) url, (object) pstrtitle, (object) (width + "px"), (object) (height + "px"));
      this.Session["ClaimRegistered"] = (object) 0;
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }

    private bool ValidateRuc(string rucAValidar)
    {
      int num = 11 - (int.Parse(rucAValidar.Substring(0, 1)) * 5 + int.Parse(rucAValidar.Substring(1, 1)) * 4 + int.Parse(rucAValidar.Substring(2, 1)) * 3 + int.Parse(rucAValidar.Substring(3, 1)) * 2 + int.Parse(rucAValidar.Substring(4, 1)) * 7 + int.Parse(rucAValidar.Substring(5, 1)) * 6 + int.Parse(rucAValidar.Substring(6, 1)) * 5 + int.Parse(rucAValidar.Substring(7, 1)) * 4 + int.Parse(rucAValidar.Substring(8, 1)) * 3 + int.Parse(rucAValidar.Substring(9, 1)) * 2) % 11;
      return (int.Parse(rucAValidar.Length.ToString((IFormatProvider) CultureInfo.CurrentCulture)) != 11 ? 10 : int.Parse(rucAValidar.Substring(10, 1))) == (num != 10 ? 0 : 0) + (num != 11 ? 0 : 1) + (num >= 10 ? 0 : num);
    }

    private bool RestrictionRazonSocial()
    {
      string str = this.txtBeneficiaryName.Text.ToUpper(CultureInfo.CurrentCulture).Replace("À", "A").Replace("Á", "A").Replace("È", "E").Replace("É", "E").Replace("Ì", "I").Replace("Í", "I").Replace("Ó", "O").Replace("Ó", "O").Replace("Ù", "U").Replace("Ú", "U");
      return !str.Contains("ASOCIACION") || !str.Contains("AUTOMOTRIZ") || !str.Contains("PERU");
    }

    private void ShowVehicleData3rd()
    {
      try
      {
        this.pblUseNew.Visible = false;
        this.lblPrice.Visible = true;
        this.txtPrice3rd.Visible = true;
        this.txtOwner3rd.Text = string.Empty;
        string str = "";
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = new DataTable();
        if (this.Session["ProcessId"] != null)
        {
          if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.CambioUso))
          {
            dataTable1 = !this.isRegisterCall ? new RequirementQueriesBL().SunarpDataReadChangeUse(this.txtPlateNumber.Text.Trim()) : new RequirementQueriesBL().SunarpDataReadChangeUse(this.txtPlateNumber.Text.Trim(), this.isRegisterCall);
            this.lblPrice.Visible = false;
            this.txtPrice3rd.Visible = false;
            if (dataTable1.Rows.Count > 0)
            {
              this.pblUseNew.Visible = true;
              int pintCategoryId = dataTable1.Rows[0]["CategoryId"] == null || !(dataTable1.Rows[0]["CategoryId"].ToString() != "") ? -1 : Convert.ToInt32(dataTable1.Rows[0]["CategoryId"].ToString());
              DataTable changeUse = new RequirementQueriesBL().GetChangeUse(Convert.ToInt32(dataTable1.Rows[0]["UseTypeSunarp"].ToString()), pintCategoryId);
              if (changeUse.Rows.Count > 0)
              {
                this.cboUseNew.DataSource = (object) changeUse;
                this.cboUseNew.DataTextField = "v_Description";
                this.cboUseNew.DataValueField = "i_usetypetargetid";
                this.cboUseNew.DataBind();
                if (this.cboUseNew.Items.Count == 1)
                  Message.SetMessage(this.lblMessage, new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_No_Cambio_Uso));
              }
              changeUse.Dispose();
            }
            else
              Message.SetMessage(this.lblMessage, new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_No_Cambio_Uso));
          }
          else if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.CambioUsoRectificacion))
          {
            if (this.Session["VehicleIdClaim"] != null)
            {
              int int32 = Convert.ToInt32(this.Session["VehicleIdClaim"]);
              dataTable1 = new RequirementQueriesBL().SunarpDataReadChangeUseRectification(this.txtPlateNumber.Text.Trim(), int32);
              if (dataTable1.Rows.Count > 0)
              {
                this.trUseNew.Visible = true;
                this.txtUseNew.Text = dataTable1.Rows[0]["UseTypeSunarpDescriptionNew"].ToString();
              }
            }
            else
              Message.SetMessage(this.lblMessage, new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO));
          }
          else if (this.isRegisterCall)
          {
            dataTable1 = new RequirementQueriesBL().SunarpDataRead3rd(this.txtPlateNumber1.Text.Trim(), this.isRegisterCall);
            if (this.StatusCallCenter == 2)
              this.Session["hdiPrice3"] = (object) new RequirementQueriesBL().SunarpDataRead3rd(this.txtPlateNumber1.Text.Trim()).Rows[0]["f_PriceSale"].ToString();
          }
          else
            dataTable1 = new RequirementQueriesBL().SunarpDataRead3rd(this.txtPlateNumber1.Text.Trim());
        }
        else
          Message.SetMessage(this.lblMessage, new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO));
        this.hidSunarpId.Value = dataTable1.Rows[0][0].ToString();
        this.txtBrand3rd.Text = dataTable1.Rows[0]["v_Brand"].ToString();
        this.txtModel3rd.Text = dataTable1.Rows[0]["v_Model"].ToString();
        this.txtPlateNew3rd.Text = dataTable1.Rows[0]["v_PlateNew"].ToString();
        this.txtSerialNumber3rd.Text = dataTable1.Rows[0]["v_SerialNumber"].ToString();
        this.txtUseType3rd.Text = dataTable1.Rows[0]["TypeUseDescription"].ToString();
        this.txtCategory3rd.Text = dataTable1.Rows[0]["Category"].ToString();
        this.ViewState["v_Igv"] = (object) dataTable1.Rows[0]["v_Igv"].ToString();
        this.ViewState["f_PriceCost"] = (object) dataTable1.Rows[0]["f_PriceCost"].ToString();
        this.ViewState["f_PriceCostF"] = (object) dataTable1.Rows[0]["f_PriceCost"].ToString();
        DataTable ownersByIdSunarp = new RequirementQueriesBL().GetOwnersByIdSunarp(Convert.ToInt32(dataTable1.Rows[0][0].ToString()));
        object obj1 = JsonConvert.DeserializeObject<object>(JsonConvert.SerializeObject((object) ownersByIdSunarp, Formatting.Indented));
        bool flag1 = false;
        bool flag2 = false;
        List<object> objectList = JsonConvert.DeserializeObject<List<object>>(JsonConvert.SerializeObject((object) new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) 0,
          (object) 1501,
          (object) "1",
          (object) "1"
        })));
        // ISSUE: reference to a compiler-generated field
        if (RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__1 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "ToString", (IEnumerable<Type>) null, typeof (RegisterRequirement), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, Type, object, object> target1 = RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__1.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, Type, object, object>> p1 = RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__1;
        Type type1 = typeof (Convert);
        // ISSUE: reference to a compiler-generated field
        if (RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "v_Value", typeof (RegisterRequirement), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__0.Target((CallSite) RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__0, objectList[0]);
        object obj3 = target1((CallSite) p1, type1, obj2);
        // ISSUE: reference to a compiler-generated field
        if (RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (RegisterRequirement)));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        foreach (object obj4 in RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__7.Target((CallSite) RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__7, obj1))
        {
          // ISSUE: reference to a compiler-generated field
          if (RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__6 == null)
          {
            // ISSUE: reference to a compiler-generated field
            RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, System.Linq.Expressions.ExpressionType.IsTrue, typeof (RegisterRequirement), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, bool> target2 = RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__6.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, bool>> p6 = RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__6;
          // ISSUE: reference to a compiler-generated field
          if (RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__5 == null)
          {
            // ISSUE: reference to a compiler-generated field
            RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, System.Linq.Expressions.ExpressionType.Not, typeof (RegisterRequirement), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, object> target3 = RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__5.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, object>> p5 = RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__5;
          // ISSUE: reference to a compiler-generated field
          if (RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__4 == null)
          {
            // ISSUE: reference to a compiler-generated field
            RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Contains", (IEnumerable<Type>) null, typeof (RegisterRequirement), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, object, object> target4 = RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__4.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, object, object>> p4 = RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__4;
          object obj5 = obj3;
          // ISSUE: reference to a compiler-generated field
          if (RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__3 == null)
          {
            // ISSUE: reference to a compiler-generated field
            RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__3 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "ToString", (IEnumerable<Type>) null, typeof (RegisterRequirement), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target5 = RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__3.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p3 = RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__3;
          Type type2 = typeof (Convert);
          // ISSUE: reference to a compiler-generated field
          if (RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "v_DocumentTypeId", typeof (RegisterRequirement), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj6 = RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__2.Target((CallSite) RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__2, obj4);
          object obj7 = target5((CallSite) p3, type2, obj6);
          object obj8 = target4((CallSite) p4, obj5, obj7);
          object obj9 = target3((CallSite) p5, obj8);
          if (target2((CallSite) p6, obj9))
          {
            flag1 = true;
            break;
          }
        }
        if (!flag1)
        {
          // ISSUE: reference to a compiler-generated field
          if (RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__11 == null)
          {
            // ISSUE: reference to a compiler-generated field
            RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (RegisterRequirement)));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          foreach (object obj10 in RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__11.Target((CallSite) RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__11, obj1))
          {
            // ISSUE: reference to a compiler-generated field
            if (RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__10 == null)
            {
              // ISSUE: reference to a compiler-generated field
              RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, System.Linq.Expressions.ExpressionType.IsTrue, typeof (RegisterRequirement), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, bool> target6 = RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__10.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, bool>> p10 = RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__10;
            // ISSUE: reference to a compiler-generated field
            if (RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__9 == null)
            {
              // ISSUE: reference to a compiler-generated field
              RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, System.Linq.Expressions.ExpressionType.Equal, typeof (RegisterRequirement), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, string, object> target7 = RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__9.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, string, object>> p9 = RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__9;
            // ISSUE: reference to a compiler-generated field
            if (RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__8 == null)
            {
              // ISSUE: reference to a compiler-generated field
              RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "v_DocumentNumber", typeof (RegisterRequirement), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj11 = RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__8.Target((CallSite) RegisterRequirement.\u003C\u003Eo__79.\u003C\u003Ep__8, obj10);
            string text = this.txtRequesterNumberDoc.Text;
            object obj12 = target7((CallSite) p9, obj11, text);
            if (target6((CallSite) p10, obj12))
            {
              flag2 = true;
              break;
            }
          }
        }
        this.Session["HasLegalDocumentType"] = (object) flag1;
        this.Session["FoundOwner"] = (object) flag2;
        for (int index = 0; index < ownersByIdSunarp.Rows.Count; ++index)
        {
          TextBox txtOwner3rd = this.txtOwner3rd;
          txtOwner3rd.Text = txtOwner3rd.Text + ownersByIdSunarp.Rows[index]["completeName"].ToString() + "\n";
        }
        ownersByIdSunarp.Dispose();
        if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.Duplicado3rd))
        {
          string pstrPlateNumber = this.txtPlateNumber1.Text.Replace("-", "");
          DataTable dataTable3 = !this.isRegisterCall ? new RequirementQueriesBL().ValidateProductPrice3rd(pstrPlateNumber) : new RequirementQueriesBL().ValidateProductPrice3rd(pstrPlateNumber, this.isRegisterCall);
          this.ViewState["Product3rd"] = (object) dataTable3;
          this.txtPrice3rd.Text = dataTable3.Rows[0]["f_PriceSale"].ToString();
          this.ViewState["f_PriceCost"] = (object) dataTable3.Rows[0]["f_PriceCost"].ToString();
          this.ViewState["f_PriceCostF"] = (object) dataTable3.Rows[0]["f_PriceCost"].ToString();
          str = this.showAdditionalProducts(Convert.ToInt32(dataTable3.Rows[0]["i_ProductId"]));
          dataTable3.Dispose();
        }
        else
        {
          this.txtPrice3rd.Text = dataTable1.Rows[0]["f_PriceSale"].ToString();
          str = this.showAdditionalProducts(Convert.ToInt32(dataTable1.Rows[0]["i_ProductId"]));
        }
        this.hdiPrice.Value = this.txtPrice3rd.Text.ToString();
        dataTable1.Dispose();
        this.txtPlateOld3rd.Text = dataTable1.Rows[0]["v_PlateOld"].ToString();
        this.txtPlateNumber.Enabled = false;
        if (!this.isRegisterCall || this.StatusCallCenter != 2)
          return;
        this.Session["f_PriceSale"] = (object) this.hdiPrice.Value;
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
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Seleccione_Banco);
        if (this.cboPaymentDate.Text == "")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Ingrese_Fecha_Pago);
        if (this.txtTerminal.Text == "" && this.rdlPaymentType.SelectedIndex == 0)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Ingrese_Terminal_voucher);
        if (this.txtUserBankCode.Text == "")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Ingrese_Codigo_Voucher);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private DataTable validaTeWildCart2do3er(string idUser, DataTable dtSunarpData)
    {
      try
      {
        DataTable dataTable1 = (DataTable) null;
        if (this.Session["ProcessId"] == null)
          return (DataTable) null;
        string pstrProduct = (int) Convert.ToInt16(this.Session["ProcessId"].ToString()) != Convert.ToInt32((object) enmProccessType.Duplicado3rd) && (int) Convert.ToInt16(this.Session["ProcessId"].ToString()) != Convert.ToInt32((object) enmProccessType.CambioUso) ? dtSunarpData.Rows[0]["v_code"].ToString() : (this.ViewState["Product3rd"] as DataTable).Rows[0]["v_code"].ToString();
        int pintBankId = int.Parse(this.cboBank.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture));
        DateTime dateTime = DateTime.Parse(this.cboPaymentDate.Value.ToString());
        string pstrTerminal = this.txtTerminal.Text.Trim();
        if (((idUser.Substring(1, 1) == "I" || idUser.Substring(1, 1) == "L" ? 1 : (idUser.Substring(1, 1) == "1" ? 1 : 0)) & (idUser.Substring(2, 1) != "I" || idUser.Substring(2, 1) != "L" ? 1 : (idUser.Substring(2, 1) != "1" ? 1 : 0))) != 0)
        {
          try
          {
            idUser = idUser.Substring(0, 1) + "I" + idUser.Substring(2, idUser.Length - 2);
            dataTable1 = new DataBankQueriesBL().PaymentAcreditation(pintBankId, dateTime.ToString("yyyyMMdd"), pstrTerminal, idUser, pstrProduct, 1);
            if (dataTable1.Rows.Count > 0)
            {
              if (Convert.ToInt32(dataTable1.Rows[0]["idVoucher"]) != -1)
                return dataTable1;
              idUser = idUser.Substring(0, 1) + "1" + idUser.Substring(2, idUser.Length - 2);
              try
              {
                idUser = idUser.Substring(0, 1) + "1" + idUser.Substring(2, idUser.Length - 2);
                dataTable1 = new DataBankQueriesBL().PaymentAcreditation(pintBankId, dateTime.ToString("yyyyMMdd"), pstrTerminal, idUser, pstrProduct, 1);
                if (dataTable1.Rows.Count > 0)
                {
                  if (Convert.ToInt32(dataTable1.Rows[0]["idVoucher"]) != -1)
                    return dataTable1;
                  idUser = idUser.Substring(0, 1) + "L" + idUser.Substring(2, idUser.Length - 2);
                  try
                  {
                    dataTable1 = new DataBankQueriesBL().PaymentAcreditation(pintBankId, dateTime.ToString("yyyyMMdd"), pstrTerminal, idUser, pstrProduct, 1);
                    if (dataTable1.Rows.Count > 0)
                    {
                      if (Convert.ToInt32(dataTable1.Rows[0]["idVoucher"]) != -1)
                        return dataTable1;
                    }
                  }
                  catch
                  {
                    return (DataTable) null;
                  }
                }
              }
              catch
              {
                return (DataTable) null;
              }
            }
          }
          catch
          {
            dataTable1 = (DataTable) null;
          }
        }
        if (((idUser.Substring(1, 1) == "I" || idUser.Substring(1, 1) == "L" ? 1 : (idUser.Substring(1, 1) == "1" ? 1 : 0)) & (idUser.Substring(2, 1) != "I" || idUser.Substring(2, 1) != "L" ? 1 : (idUser.Substring(2, 1) != "1" ? 1 : 0))) != 0)
        {
          try
          {
            idUser = idUser.Substring(0, 1) + "II" + idUser.Substring(3, idUser.Length - 3);
            DataTable dataTable2 = new DataBankQueriesBL().PaymentAcreditation(pintBankId, dateTime.ToString("yyyyMMdd"), pstrTerminal, idUser, pstrProduct, 1);
            if (dataTable2.Rows.Count > 0)
            {
              if (Convert.ToInt32(dataTable2.Rows[0]["idVoucher"]) != -1)
                return dataTable2;
              idUser = idUser.Substring(0, 2) + "1" + idUser.Substring(3, idUser.Length - 3);
            }
            idUser = idUser.Substring(0, 1) + "I1" + idUser.Substring(3, idUser.Length - 3);
            DataTable dataTable3 = new DataBankQueriesBL().PaymentAcreditation(pintBankId, dateTime.ToString("yyyyMMdd"), pstrTerminal, idUser, pstrProduct, 1);
            if (dataTable3.Rows.Count > 0 && Convert.ToInt32(dataTable3.Rows[0]["idVoucher"]) != -1)
              return dataTable3;
            idUser = idUser.Substring(0, 1) + "IL" + idUser.Substring(3, idUser.Length - 3);
            DataTable dataTable4 = new DataBankQueriesBL().PaymentAcreditation(pintBankId, dateTime.ToString("yyyyMMdd"), pstrTerminal, idUser, pstrProduct, 1);
            if (dataTable4.Rows.Count > 0 && Convert.ToInt32(dataTable4.Rows[0]["idVoucher"]) != -1)
              return dataTable4;
            idUser = idUser.Substring(0, 1) + "1I" + idUser.Substring(3, idUser.Length - 3);
            DataTable dataTable5 = new DataBankQueriesBL().PaymentAcreditation(pintBankId, dateTime.ToString("yyyyMMdd"), pstrTerminal, idUser, pstrProduct, 1);
            if (dataTable5.Rows.Count > 0 && Convert.ToInt32(dataTable5.Rows[0]["idVoucher"]) != -1)
              return dataTable5;
            idUser = idUser.Substring(0, 1) + "11" + idUser.Substring(3, idUser.Length - 3);
            DataTable dataTable6 = new DataBankQueriesBL().PaymentAcreditation(pintBankId, dateTime.ToString("yyyyMMdd"), pstrTerminal, idUser, pstrProduct, 1);
            if (dataTable6.Rows.Count > 0 && Convert.ToInt32(dataTable6.Rows[0]["idVoucher"]) != -1)
              return dataTable6;
            idUser = idUser.Substring(0, 1) + "1L" + idUser.Substring(3, idUser.Length - 3);
            DataTable dataTable7 = new DataBankQueriesBL().PaymentAcreditation(pintBankId, dateTime.ToString("yyyyMMdd"), pstrTerminal, idUser, pstrProduct, 1);
            if (dataTable7.Rows.Count > 0 && Convert.ToInt32(dataTable7.Rows[0]["idVoucher"]) != -1)
              return dataTable7;
            idUser = idUser.Substring(0, 1) + "LI" + idUser.Substring(3, idUser.Length - 3);
            DataTable dataTable8 = new DataBankQueriesBL().PaymentAcreditation(pintBankId, dateTime.ToString("yyyyMMdd"), pstrTerminal, idUser, pstrProduct, 1);
            if (dataTable8.Rows.Count > 0 && Convert.ToInt32(dataTable8.Rows[0]["idVoucher"]) != -1)
              return dataTable8;
            idUser = idUser.Substring(0, 1) + "L1" + idUser.Substring(3, idUser.Length - 3);
            DataTable dataTable9 = new DataBankQueriesBL().PaymentAcreditation(pintBankId, dateTime.ToString("yyyyMMdd"), pstrTerminal, idUser, pstrProduct, 1);
            if (dataTable9.Rows.Count > 0 && Convert.ToInt32(dataTable9.Rows[0]["idVoucher"]) != -1)
              return dataTable9;
            idUser = idUser.Substring(0, 1) + "LL" + idUser.Substring(3, idUser.Length - 3);
            dataTable1 = new DataBankQueriesBL().PaymentAcreditation(pintBankId, dateTime.ToString("yyyyMMdd"), pstrTerminal, idUser, pstrProduct, 1);
            if (dataTable1.Rows.Count > 0)
            {
              if (Convert.ToInt32(dataTable1.Rows[0]["idVoucher"]) != -1)
                return dataTable1;
            }
          }
          catch
          {
            return (DataTable) null;
          }
        }
        if (((idUser.Substring(1, 1) != "I" || idUser.Substring(1, 1) != "L" ? 1 : (idUser.Substring(1, 1) != "1" ? 1 : 0)) & (idUser.Substring(2, 1) == "I" || idUser.Substring(2, 1) == "L" ? 1 : (idUser.Substring(2, 1) == "1" ? 1 : 0))) != 0)
        {
          try
          {
            idUser = idUser.Substring(0, 2) + "I" + idUser.Substring(3, idUser.Length - 3);
            DataTable dataTable10 = new DataBankQueriesBL().PaymentAcreditation(pintBankId, dateTime.ToString("yyyyMMdd"), pstrTerminal, idUser, pstrProduct, 1);
            if (dataTable10.Rows.Count > 0)
            {
              if (Convert.ToInt32(dataTable10.Rows[0]["idVoucher"]) != -1)
                return dataTable10;
              idUser = idUser.Substring(0, 2) + "1" + idUser.Substring(3, idUser.Length - 3);
            }
            idUser = idUser.Substring(0, 2) + "1" + idUser.Substring(3, idUser.Length - 3);
            DataTable dataTable11 = new DataBankQueriesBL().PaymentAcreditation(pintBankId, dateTime.ToString("yyyyMMdd"), pstrTerminal, idUser, pstrProduct, 1);
            if (dataTable11.Rows.Count > 0)
            {
              if (Convert.ToInt32(dataTable11.Rows[0]["idVoucher"]) != -1)
                return dataTable11;
              idUser = idUser.Substring(0, 2) + "L" + idUser.Substring(3, idUser.Length - 3);
            }
            dataTable1 = new DataBankQueriesBL().PaymentAcreditation(pintBankId, dateTime.ToString("yyyyMMdd"), pstrTerminal, idUser, pstrProduct, 1);
            if (Convert.ToInt32(dataTable1.Rows[0]["idVoucher"]) != -1)
              return dataTable1;
          }
          catch
          {
            return (DataTable) null;
          }
        }
        return dataTable1;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void CleanTextDelivery()
    {
      this.wddDeliveryDescription.SelectedValue = "0";
      this.txtMailDelivery.Text = "";
      this.txtTelephoneDelivery.Text = "";
      this.txtTelephoneMovilDelivery.Text = "";
      this.txtDeliveryDescription.Text = "";
      this.wddDeliveryDescription2.SelectedValue = "0";
      this.txtDeliveryDescription2.Text = "";
      this.wddDistrict.SelectedValue = "0";
      this.txtReference.Text = "";
    }

    private void SetDeliveryEnabled(
      bool IsMailDelivery,
      bool IsTelephoneDelivery,
      bool IsTelephoneMovilDelivery,
      bool IsDeliveryDescription,
      bool IsTxtDeliveryDescription,
      bool IsDeliveryDescription2,
      bool IsTxtDeliveryDescription2,
      bool IsDistrict,
      bool IsReference)
    {
      this.txtMailDelivery.Enabled = IsMailDelivery;
      this.txtTelephoneDelivery.Enabled = IsTelephoneDelivery;
      this.txtTelephoneMovilDelivery.Enabled = IsTelephoneMovilDelivery;
      this.wddDeliveryDescription.Enabled = IsDeliveryDescription;
      this.txtDeliveryDescription.Enabled = IsTxtDeliveryDescription;
      this.wddDeliveryDescription2.Enabled = IsDeliveryDescription2;
      this.txtDeliveryDescription2.Enabled = IsTxtDeliveryDescription2;
      this.wddDistrict.Enabled = IsDistrict;
      this.txtReference.Enabled = IsReference;
    }

    protected void rbTypePament_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.rbTypePayment.SelectedValue == "0")
      {
        this.divVisa.Visible = true;
        this.divVisa2.Visible = false;
      }
      else
      {
        this.divVisa.Visible = false;
        this.divVisa2.Visible = true;
        double num1 = Math.Round(Convert.ToDouble(this.ViewState["f_PriceCostF"]) * (1.0 + this.ComisionCanalAtencion), 2);
        double num2 = Math.Round(Math.Round(Convert.ToDouble(num1) * Convert.ToDouble(this.ViewState["v_Igv"]), 3), 2);
        double num3 = Convert.ToDouble(num1) + Convert.ToDouble(num2);
        this.lblComision.Text = Math.Round(num3 - Convert.ToDouble(this.ViewState["f_PriceTotal"]), 2).ToString();
        this.Session["TotalAPagar"] = (object) Math.Round(num3, 2).ToString();
        this.lblTotalAPagar.Text = Math.Round(num3, 2).ToString();
      }
    }

    private void PaymentVISA()
    {
      RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
      try
      {
        if (this.ViewState["ids"] == null)
        {
          this.Session["iLogId"] = (object) null;
          this.SaveRequirement((object) null, (EventArgs) null);
        }
        string[] strArray = this.ViewState["ids"].ToString().Split('|');
        this.Session["RequirementIds"] = this.ViewState["ids"];
        DataTable byPaymentCode = requirementQueriesBl.RequirementGetByPaymentCode("", Convert.ToInt32(strArray[0].ToString()));
        requirementQueriesBl.UpdateRequirementBoundVisa(Convert.ToInt32(strArray[0]));
        string empty = string.Empty;
        this.CreatePopUpServer("Pago OnLine - VISA", "../PaymentPOS/SendEticket.aspx?PaymentCode=" + byPaymentCode.Rows[0]["v_PaymentCode"].ToString(), "770px", "690px");
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(1, ex.Message));
      }
    }

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp2('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Script", script, true);
    }

    protected void btnJavaScriptCloseVISA_Click(object sender, EventArgs e)
    {
      if (Convert.ToInt32(this.Session["VisaPagoConforme"]) != 1)
        return;
      string[] strArray = this.ViewState["ids"].ToString().Split('|');
      this.Response.Redirect("~/Requirement/SuccessfulRegistration.aspx?RequirementId=" + strArray[0].ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&RequirementPlateId=" + strArray[1].ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&t=" + strArray[2].ToString() + "&mre=0", false);
      this.Session["SendEmail"] = (object) 1;
    }

    protected void btnFinish_Click(object sender, EventArgs e)
    {
      this.Session.Remove("VisaPagoConforme");
      if (this.rbTypePayment.SelectedIndex == 0)
      {
        RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
        if (this.ViewState["ids"] == null)
        {
          this.SaveRequirement((object) null, (EventArgs) null);
        }
        else
        {
          string[] strArray = this.ViewState["ids"].ToString().Split('|');
          requirementQueriesBl.UpdateRequirementBoundCash(Convert.ToInt32(strArray[0].ToString((IFormatProvider) CultureInfo.CurrentCulture)));
        }
        string[] strArray1 = this.ViewState["ids"].ToString().Split('|');
        this.Response.Redirect("~/Requirement/SuccessfulRegistration.aspx?RequirementId=" + strArray1[0].ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&RequirementPlateId=" + strArray1[1].ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&t=" + strArray1[2].ToString() + "&mre=0", false);
        this.Session["SendEmail"] = (object) 1;
      }
      else
        this.PaymentVISA();
    }

    protected void BtnValidate_Click(object sender, EventArgs e)
    {
      try
      {
        HttpContext current = HttpContext.Current;
        string str = this.TxtDocNumberProofPaper.Text.Trim();
        HttpWebRequest httpWebRequest = WebRequest.Create(ConfigurationManager.AppSettings["urlApiRuc"] + str) as HttpWebRequest;
        httpWebRequest.Method = ConfigurationManager.AppSettings["ApiMethod"].ToString();
        httpWebRequest.ContentType = "application/json";
        string end;
        try
        {
          StreamReader streamReader = new StreamReader((httpWebRequest.GetResponse() as HttpWebResponse).GetResponseStream());
          end = streamReader.ReadToEnd();
          streamReader.Close();
        }
        catch (WebException ex)
        {
          StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream(), true);
          end = streamReader.ReadToEnd();
          streamReader.Close();
        }
        RegisterRequirement.DataSunat dataSunat = new RegisterRequirement.DataSunat();
        RegisterRequirement.infosunat infosunat = JsonConvert.DeserializeObject<RegisterRequirement.infosunat>(end);
        if (infosunat.Content.success)
        {
          if (infosunat.Content.content.EstadoContribuyente != "ACTIVO")
          {
            this.txtBeneficiaryName.Text = "";
            this.txtAddress.Text = "";
            this.lblMessage1.Text = "";
            this.ViewState["ValidationRUC"] = (object) 2;
            throw new HandledException(1, "NUMERO DE RUC NO SE ENCUENTRA ACTIVO");
          }
          this.ViewState["ValidationRUC"] = (object) 1;
          this.ViewState["RUC"] = (object) str;
          this.txtBeneficiaryName.Text = infosunat.Content.content.RazónSocial;
          this.txtBeneficiaryName.Enabled = false;
          this.txtAddress.Text = infosunat.Content.content.Direccion;
          Message.SetMessage(this.lblMessage1, enmMessageType.Success, SIIV.SystemParameter.BL.Constants.OPERATIONRESULT_OK + "<br> Se valido correctame el numero de RUC");
        }
        else
        {
          this.ViewState["ValidationRUC"] = (object) 2;
          this.txtBeneficiaryName.Text = "";
          this.txtAddress.Text = "";
          this.lblMessage1.Text = "";
          throw new HandledException(1, infosunat.Content.msg);
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      finally
      {
        this.HidePopup();
      }
    }

    private bool IsValidName(string name)
    {
      string pattern = "^[a-zA-ZñÑáéíóúÁÉÍÓÚäëïöüÄËÏÖÜ' ]+$";
      return name.Length <= 350 && Regex.IsMatch(name, pattern) && !name.Contains("  ") && !name.StartsWith(" ") && !name.EndsWith(" ");
    }

    protected void chkVisa_CheckedChanged(object sender, EventArgs e)
    {
    }

    protected void CheckhasnotEmail_CheckedChanged(object sender, EventArgs e)
    {
      if (this.RucValidation == 1 && this.CboDocumentType.SelectedValue == "4" && this.ViewState["ValidationRUC"] != null && (int) this.ViewState["ValidationRUC"] == 1)
        this.txtBeneficiaryName.Enabled = false;
      if (this.CheckhasnotEmail.Checked)
      {
        this.txtBeneficiaryMail.ReadOnly = this.CheckhasnotEmail.Checked;
        this.txtBeneficiaryMail.Enabled = !this.CheckhasnotEmail.Checked;
        this.txtBeneficiaryMail.Text = ConfigurationManager.AppSettings["EmailDefault"].ToString();
      }
      else
      {
        this.txtBeneficiaryMail.ReadOnly = this.CheckhasnotEmail.Checked;
        this.txtBeneficiaryMail.Enabled = !this.CheckhasnotEmail.Checked;
        this.txtBeneficiaryMail.Text = string.Empty;
      }
    }

    protected void CboDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.TxtDocNumberProofPaper.Text = "";
    }

    protected void CboDocumentType_SelectedIndexChangedRequest(object sender, EventArgs e)
    {
      this.txtRequesterNumberDoc.Text = "";
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }

    protected void BtnValidate_ClickRuc(object sender, EventArgs e)
    {
      try
      {
        string ruc = this.TxtDocNumberProofPaper.Text.Trim();
        if (ruc.Length != 11)
          Message.SetMessage(this.lblMessage1, enmMessageType.Warning, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_11);
        else if (RegisterRequirement.ValidationRUC(ruc))
        {
          this.ViewState["ValidationRUC1"] = (object) 1;
          this.ViewState["RUC1"] = (object) ruc;
          Message.SetMessage(this.lblMessage1, enmMessageType.Success, SIIV.SystemParameter.BL.Constants.OPERATIONRESULT_OK + "<br> RUC válido");
          this.txtBeneficiaryName.Focus();
        }
        else
        {
          this.ViewState["ValidationRUC1"] = (object) 2;
          Message.SetMessage(this.lblMessage1, enmMessageType.Warning, SIIV.SystemParameter.BL.Constants.OPERATIONRESULT_Error + "<br> RUC inválido");
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      finally
      {
        this.HidePopup();
      }
    }

    public static bool ValidationRUC(string ruc)
    {
      bool flag = false;
      int int32_1 = Convert.ToInt32(ruc.Substring(0, 2));
      int num1;
      switch (int32_1)
      {
        case 10:
        case 15:
        case 17:
          num1 = 1;
          break;
        default:
          num1 = int32_1 == 20 ? 1 : 0;
          break;
      }
      if (num1 != 0)
        flag = true;
      if (!flag)
        return false;
      int num2 = Convert.ToInt32(ruc.Substring(0, 1)) * 5;
      int num3 = Convert.ToInt32(ruc.Substring(1, 1)) * 4;
      int num4 = Convert.ToInt32(ruc.Substring(2, 1)) * 3;
      int num5 = Convert.ToInt32(ruc.Substring(3, 1)) * 2;
      int num6 = Convert.ToInt32(ruc.Substring(4, 1)) * 7;
      int num7 = Convert.ToInt32(ruc.Substring(5, 1)) * 6;
      int num8 = Convert.ToInt32(ruc.Substring(6, 1)) * 5;
      int num9 = Convert.ToInt32(ruc.Substring(7, 1)) * 4;
      int num10 = Convert.ToInt32(ruc.Substring(8, 1)) * 3;
      int num11 = Convert.ToInt32(ruc.Substring(9, 1)) * 2;
      int int32_2 = Convert.ToInt32(ruc.Substring(10, 1));
      int num12 = num2 + num3 + num4 + num5 + num6 + num7 + num8 + num9 + num10 + num11;
      int num13 = num12 / 11;
      int num14 = 11 - (num12 - num13 * 11);
      int num15;
      switch (num14)
      {
        case 10:
          num15 = 0;
          break;
        case 11:
          num15 = 1;
          break;
        default:
          num15 = num14;
          break;
      }
      return int32_2 == num15;
    }

    private void ShowIconValidateDocNumRUC()
    {
      if (this.RucValidation == 1 && this.CboDocumentType.SelectedValue == "4")
      {
        if ((int) this.ViewState["ValidationRUC"] != 1)
          throw new HandledException(1, "Validar que el numero de RUC sea el correcto");
        if (this.ViewState["RUC"].ToString() != this.TxtDocNumberProofPaper.Text.Trim())
          throw new HandledException(1, "NUMERO DE RUC NO SE ENCUENTRA VALIDADO");
      }
      if (this.RucValidation != 2 || !(this.CboDocumentType.SelectedValue == "4"))
        return;
      if (this.ViewState["ValidationRUC1"] == null)
      {
        this.BtnValidate_ClickRuc((object) null, (EventArgs) null);
      }
      else
      {
        if ((int) this.ViewState["ValidationRUC1"] != 1)
          throw new HandledException(1, "Validar que el numero de RUC sea el correcto");
        if (this.ViewState["RUC1"].ToString() != this.TxtDocNumberProofPaper.Text.Trim())
          throw new HandledException(1, "NUMERO DE RUC NO SE ENCUENTRA VALIDADO");
      }
    }

    protected void rblPropApoderado_SelectedIndexChanged(object sender, EventArgs e)
    {
      if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) == Convert.ToInt32((object) enmProccessType.Duplicado))
        this.PanelDuplicado.Visible = true;
      if (this.rblPropApoderado.SelectedValue == "Propietario")
      {
        this.pnlMensajePropietario.Visible = true;
        this.pnlMensajeApoderado.Visible = false;
        this.btnNext = this.GetControlFromWizard(this.Wizard1, RegisterRequirement.WizardNavigationTempContainer.StartNavigationTemplateContainerID, "btnNext") as Button;
        this.btnNext.Visible = false;
        this.btnCancel = this.GetControlFromWizard(this.Wizard1, RegisterRequirement.WizardNavigationTempContainer.StartNavigationTemplateContainerID, "btnCancel") as Button;
        this.btnCancel.Visible = false;
        this.ViewState["TypeApplicant"] = (object) 1;
      }
      else
      {
        if (!(this.rblPropApoderado.SelectedValue == "Apoderado"))
          return;
        this.pnlMensajePropietario.Visible = false;
        this.pnlMensajeApoderado.Visible = true;
        this.ViewState["TypeApplicant"] = (object) 2;
        if (this.ValidateUserPermission())
        {
          this.btnNext = this.GetControlFromWizard(this.Wizard1, RegisterRequirement.WizardNavigationTempContainer.StartNavigationTemplateContainerID, "btnNext") as Button;
          this.btnNext.Visible = true;
          this.btnCancel = this.GetControlFromWizard(this.Wizard1, RegisterRequirement.WizardNavigationTempContainer.StartNavigationTemplateContainerID, "btnCancel") as Button;
          this.btnCancel.Visible = true;
        }
        else
        {
          this.btnCancel = this.GetControlFromWizard(this.Wizard1, RegisterRequirement.WizardNavigationTempContainer.StartNavigationTemplateContainerID, "btnCancel") as Button;
          this.btnCancel.Visible = true;
        }
      }
    }

    public void rblPropDenuncia_SelectedIndexChanged(object sender, EventArgs e)
    {
      Panel control = (this.Wizard1.FindControl("WS_DatosSolicitante") as WizardStep).FindControl("tblDenuncia") as Panel;
      if (this.rblDenuncia.SelectedValue == "DenunciaPolicial")
      {
        this.ViewState["RegistrationReason"] = (object) null;
        this.PanelDenuncia.Visible = true;
        this.PanelIntercambio.Visible = false;
        this.PanelDenunciaIntercambio.Visible = false;
        control.Visible = true;
        this.ViewState["RegistrationReason"] = (object) 1;
        this.TextBoxNumeroOrden.Text = string.Empty;
        this.TextBoxClave.Text = string.Empty;
        this.ViewState["FechaDenuncia"] = (object) null;
      }
      else if (this.rblDenuncia.SelectedValue == "Intercambio")
      {
        this.ViewState["RegistrationReason"] = (object) null;
        this.PanelIntercambio.Visible = true;
        this.PanelDenunciaIntercambio.Visible = false;
        this.PanelDenuncia.Visible = false;
        control.Visible = false;
        this.ViewState["RegistrationReason"] = (object) 2;
        this.TextBoxNumeroOrden.Text = string.Empty;
        this.TextBoxClave.Text = string.Empty;
        this.ViewState["FechaDenuncia"] = (object) null;
      }
      else
      {
        if (!(this.rblDenuncia.SelectedValue == "DenunciaIntercambio"))
          return;
        this.ViewState["RegistrationReason"] = (object) null;
        this.PanelDenunciaIntercambio.Visible = true;
        this.PanelDenuncia.Visible = false;
        this.PanelIntercambio.Visible = false;
        control.Visible = true;
        this.ViewState["RegistrationReason"] = (object) 3;
        this.TextBoxNumeroOrden.Text = string.Empty;
        this.TextBoxClave.Text = string.Empty;
        this.ViewState["FechaDenuncia"] = (object) null;
      }
    }

    protected void SaveDate(string datetime)
    {
      HttpContext.Current.Session["FechaDenuncia"] = (object) datetime;
    }

    private bool ValidateUserPermission()
    {
      List<object> objectList = JsonConvert.DeserializeObject<List<object>>(JsonConvert.SerializeObject((object) new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.AuthUsers,
        (object) "",
        (object) "1",
        (object) "1"
      })));
      this.objUserBE = this.Session["SystemUser"] as SystemUser;
      object obj1 = objectList.Find((Predicate<object>) (x =>
      {
        // ISSUE: reference to a compiler-generated field
        if (RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (RegisterRequirement)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target1 = RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__2.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p2 = RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__2;
        // ISSUE: reference to a compiler-generated field
        if (RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, System.Linq.Expressions.ExpressionType.Equal, typeof (RegisterRequirement), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, int, object> target2 = RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__1.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, int, object>> p1 = RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__1;
        // ISSUE: reference to a compiler-generated field
        if (RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "i_ParameterId", typeof (RegisterRequirement), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__0.Target((CallSite) RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__0, x);
        int iSystemUserId = this.objUserBE.i_SystemUserId;
        object obj3 = target2((CallSite) p1, obj2, iSystemUserId);
        return target1((CallSite) p2, obj3);
      }));
      // ISSUE: reference to a compiler-generated field
      if (RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, System.Linq.Expressions.ExpressionType.IsTrue, typeof (RegisterRequirement), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target = RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__4.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p4 = RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__4;
      // ISSUE: reference to a compiler-generated field
      if (RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, System.Linq.Expressions.ExpressionType.NotEqual, typeof (RegisterRequirement), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__3.Target((CallSite) RegisterRequirement.\u003C\u003Eo__106.\u003C\u003Ep__3, obj1, (object) null);
      return target((CallSite) p4, obj4);
    }

    [WebMethod]
    public static void GuardarVariableDeSesion(string valor)
    {
      HttpContext.Current.Session["FechaDenuncia"] = (object) valor;
      if (!(HttpContext.Current.Handler is RegisterRequirement handler))
        return;
      handler.SaveDate(valor);
    }

    public enum WizardNavigationTempContainer
    {
      StartNavigationTemplateContainerID = 1,
      StepNavigationTemplateContainerID = 2,
      FinishNavigationTemplateContainerID = 3,
    }

    public class infosunat
    {
      public RegisterRequirement.Content Content { get; set; }

      public bool Status { get; set; }

      public int ResultId { get; set; }

      public string Message { get; set; }
    }

    public class Content
    {
      public bool success { get; set; }

      public string msg { get; set; }

      public RegisterRequirement.content content { get; set; }
    }

    public class content
    {
      public int Id { get; set; }

      public string Ruc { get; set; }

      public string RazónSocial { get; set; }

      public string Ubigeo { get; set; }

      public string Direccion { get; set; }

      public string EstadoContribuyente { get; set; }

      public string CondiciónDomicilio { get; set; }
    }

    public class DataSunat
    {
      public string ruc { get; set; }

      public string razonsocial { get; set; }

      public string estado { get; set; }
    }
  }
}
