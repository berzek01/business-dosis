// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Requirement.RegisterMassiveRequirement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using Newtonsoft.Json;
using SIIV.BE;
using SIIV.Claim.BL;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
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
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Requirement
{
  public class RegisterMassiveRequirement : Page
  {
    private static DataColumn[] colVideos = new DataColumn[4];
    private DataTable dtClaim;
    private DataTable dtDocument;
    private DataTable dtIDS;
    private DataTable DtProduct;
    private DataTable dtListOwners;
    private DataTable dtListVehicleData;
    private DataTable dtOwners;
    private DataTable DtRequirements;
    private DataTable dtVehicleData;
    private RequirementContributor objContributorRequester;
    private SIIV.BE.Requirement objRequirement;
    private RequirementContributor objRequirementContributor;
    private Payment objPayment;
    private RequirementQueriesBL oRequirementQueriesBL;
    private RequirementManagementBL oRequirementManagement;
    private Button btnFinishTemplate;
    private Button btnPreviousTemplate;
    private Button btnClaim;
    private Button btnCancelTemplate;
    private double ComisionCanalAtencion = Convert.ToDouble(ConfigurationManager.AppSettings[nameof (ComisionCanalAtencion)]);
    private bool isRegisterCall = false;
    private int StatusCallCenter;
    protected UpdatePanel updatePanel;
    protected HtmlTable tbTable;
    protected GridView gvList;
    protected Label lblCount;
    protected Label lblMonto;
    protected HtmlTableCell TagResumen;
    protected Button btnAdd;
    protected Button btnFinish;
    protected Button btnCancel;
    protected RadioButtonList RadioButtonList1;
    protected HtmlTableRow tagRequesterData;
    protected HtmlTableRow TagName1;
    protected HtmlTableCell Td2;
    protected HtmlTableCell Td3;
    protected TextBox txtRequesterName;
    protected HtmlTableRow TagName2;
    protected HtmlTableCell Td4;
    protected HtmlTableCell Td5;
    protected TextBox txtRequesterLast1;
    protected TextBoxWatermarkExtender txtRequesterLast1_TextBoxWatermarkExtender;
    protected HtmlTableCell Td6;
    protected TextBox txtRequesterLast2;
    protected TextBoxWatermarkExtender TextBoxWatermarkExtender1;
    protected HtmlTableCell Td7;
    protected RequiredFieldValidator RequiredFieldValidator5;
    protected ValidatorCalloutExtender ValidatorCalloutExtender5;
    protected RequiredFieldValidator RequiredFieldValidator6;
    protected ValidatorCalloutExtender ValidatorCalloutExtender6;
    protected DropDownList cboRequesterTypeDoc;
    protected TextBox txtRequesterNumberDoc;
    protected TextBox txtRequesterPhone;
    protected MaskedEditExtender txtUserPhone_MaskedEditExtender;
    protected RequiredFieldValidator RequiredFieldValidator9;
    protected ValidatorCalloutExtender ValidatorCalloutExtender9;
    protected Label Label25;
    protected TextBox txtRequesterMail;
    protected FilteredTextBoxExtender txtRequesterMail_FilteredTextBoxExtender1;
    protected RegularExpressionValidator RegularExpressionValidator1;
    protected ValidatorCalloutExtender RegularExpressionValidator1_ValidatorCalloutExtender;
    protected Label lblRequesterMessage;
    protected HtmlTableRow tagContributorData;
    protected RadioButtonList rdbProofPayment;
    protected DropDownList CboDocumentType;
    protected TextBox TxtDocNumberProofPaper;
    protected Label lblMessage1;
    protected TextBox txtBeneficiaryName;
    protected FilteredTextBoxExtender txtBeneficiaryName_FilteredTextBoxExtender;
    protected TextBox txtAddress;
    protected FilteredTextBoxExtender txtAddress_FilteredTextBoxExtender;
    protected TextBox txtBeneficiaryMail;
    protected CheckBox CheckhasnotEmail;
    protected Label Label3;
    protected Button BtnValidate;
    protected Button BtnValidateRuc;
    protected Panel pnRegistration;
    protected Wizard Wizard1;
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
    protected CheckBox chkAccept;
    protected CustomValidator CustomValidator2;
    protected Label lblData1Message2;
    protected HtmlTableCell tdProcAdd;
    protected HtmlGenericControl divImgPortaPlaca;
    protected HtmlGenericControl divImgPortaPlacaMoto;
    protected HtmlGenericControl contAdd;
    protected CheckBox chkPorta;
    protected HtmlTableRow trTorni;
    protected HtmlTableCell Td1;
    protected CheckBox chkTorni;
    protected HtmlTableRow tr4;
    protected CheckBox chkServi3;
    protected Label lblServiceDescription1;
    protected Label lblPriceCallCenter1;
    protected HtmlGenericControl contAddMoto;
    protected CheckBox chkPortaMoto;
    protected HtmlTableRow tr3;
    protected CheckBox chkServi4;
    protected Label lblServiceDescription;
    protected Label lblPriceCallCenter;
    protected HtmlTableRow tr2;
    protected HiddenField hdiPrice;
    protected HtmlTableCell Td8;
    protected HtmlTableCell Td9;
    protected HtmlTableCell Td10;
    protected WizardStep WS_SeleccionComprobante;
    protected DropDownList cboDeliveryPoint;
    protected Button btnReturnPopupConfirmation;
    protected TextBox txtObservation;
    protected CheckBox chkConfirmPreview;
    protected WizardStep WS_ValidacionPlaca;
    protected TextBox txtPlateNumber1;
    protected RequiredFieldValidator RequiredFieldValidator16;
    protected ValidatorCalloutExtender ValidatorCalloutExtender16;
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
    protected TextBox txtOwner3rd;
    protected CheckBox chkAccept3rd;
    protected CustomValidator CustomValidator1;
    protected Label lblData2Message2;
    protected WizardStep WS_SeleccionMedioPago;
    protected HtmlGenericControl divTipoPago;
    protected RadioButtonList rbTypePayment;
    protected HtmlGenericControl divVisa;
    protected Button btnContinuar;
    protected HtmlGenericControl divVisa2;
    protected Image Image7;
    protected CheckBox chkVisa;
    protected HyperLink PoliticaDevolucion;
    protected Label lblComision;
    protected Label lblTotalAPagar;
    protected Button btnPaymentVisa;
    protected Label lblMessage;
    protected Button btnJavaScriptResponse;
    protected Button btnJavaScriptCloseVISA;

    protected void Page_Load(object sender, EventArgs e)
    {
      SystemUser systemUser1 = (SystemUser) this.Session["SystemUser"];
      this.btnFinishTemplate = this.GetControlFromWizard(this.Wizard1, RegisterMassiveRequirement.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnFinish") as Button;
      if (this.btnFinishTemplate.Text != "Grabar")
        this.gvList.Columns[0].Visible = true;
      string str = systemUser1.i_RoleConfigId.ToString();
      this.StatusCallCenter = int.Parse(ConfigurationManager.AppSettings["ServiceCallCenter"]);
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.RoleCall.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      DataTable priceServiceDelivery = new RequirementQueriesBL().GetPriceServiceDelivery(471, 0);
      this.isRegisterCall = ((IEnumerable<string>) dataTable.Rows[0]["v_Value"].ToString().Split('|')).Contains<string>(str);
      if (this.StatusCallCenter == 0)
        this.isRegisterCall = false;
      if (this.isRegisterCall)
      {
        this.chkServi3.Checked = true;
        this.chkServi3.Enabled = false;
        this.tr4.Visible = true;
        this.lblServiceDescription1.Text = priceServiceDelivery.Rows[0]["v_Description"].ToString();
        this.lblPriceCallCenter1.Text = priceServiceDelivery.Rows[0]["f_PriceProduct"].ToString();
        this.tr3.Visible = true;
        this.chkServi4.Checked = true;
        this.chkServi4.Enabled = false;
        this.lblPriceCallCenter.Text = priceServiceDelivery.Rows[0]["f_PriceProduct"].ToString();
        this.lblServiceDescription.Text = priceServiceDelivery.Rows[0]["v_Description"].ToString();
      }
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        DataTable dtResult = new DataTable("Datos");
        this.TableColumns(dtResult);
        DataRow row = dtResult.NewRow();
        dtResult.Rows.Add(row);
        this.gvList.DataSource = (object) dtResult;
        this.gvList.DataBind();
        this.gvList.Rows[0].Visible = false;
        SystemUser systemUser2 = (SystemUser) this.Session["SystemUser"];
        this.ViewState["SystemUser"] = (object) systemUser2;
        if (ConfigurationManager.AppSettings["RolConfigId_WebPublic"] == systemUser2.i_RoleConfigId.ToString())
          this.ViewState["PublicUser"] = (object) 1;
        else
          this.ViewState["PublicUser"] = (object) 0;
        this.autoSetRequester();
        this.tagRequesterData.Visible = false;
        if (this.Session["RequirementPlateType"] != null)
        {
          this.ViewState["vwRequirementPlateType"] = (object) Convert.ToInt32(this.Session["RequirementPlateType"]);
        }
        else
        {
          this.ViewState["vwRequirementPlateType"] = (object) Convert.ToInt32((object) enmRequirementPlateType.Masiva);
          this.Session["RequirementPlateType"] = (object) Convert.ToInt32((object) enmRequirementPlateType.Masiva);
        }
        this.Initialize();
        this.ViewState["Count"] = (object) 0;
        this.Session.Remove("ETicketM");
        this.Wizard1.ActiveStepIndex = 0;
        this.BeginList();
        this.ViewState["ListSunarpDataM"] = (object) null;
        this.BeginTitle();
        this.refreshList();
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

    protected void CboDocumentType_SelectedIndexChangedRequest(object sender, EventArgs e)
    {
      this.txtRequesterNumberDoc.Text = "";
    }

    private void TableColumns(DataTable dtResult)
    {
      dtResult.Columns.Add("i_VehicleId", typeof (int));
      dtResult.Columns.Add("d_DispatchDate", typeof (DateTime));
      dtResult.Columns.Add("v_PlateNew", typeof (string));
      dtResult.Columns.Add("v_TitleNumber", typeof (string));
      dtResult.Columns.Add("v_PlateOld", typeof (string));
      dtResult.Columns.Add("v_Brand", typeof (string));
      dtResult.Columns.Add("v_Model", typeof (string));
      dtResult.Columns.Add("SunarpCategory", typeof (string));
      dtResult.Columns.Add("v_SerialNumber", typeof (string));
    }

    protected void gvList_PageIndexChanged(object sender, EventArgs e) => this.refreshList();

    protected void btnAdd_Click(object sender, EventArgs e)
    {
      this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_ValidacionOrden);
      this.chkAccept.Checked = false;
      this.pnRegistration.Visible = true;
      this.btnAdd.Enabled = false;
      this.btnCancel.Enabled = false;
      this.btnFinish.Enabled = false;
      this.txtPlateNumber.Enabled = true;
      this.txtTitleNumber.Enabled = true;
      this.txtPlateNumber1.Enabled = true;
      this.txtPlateNumber.Text = "";
      this.txtTitleNumber.Text = "";
      this.txtPlateNumber1.Text = "";
      this.chkPorta_CheckedChanged((object) null, (EventArgs) null);
      this.trTorni.Visible = false;
    }

    protected void btnFinish_Click(object sender, EventArgs e)
    {
      if (this.ViewState["ListSunarpDataM"] == null)
        return;
      int int32 = Convert.ToInt32(this.Session["RequirementPlateType"]);
      this.pnRegistration.Visible = true;
      this.btnAdd.Enabled = false;
      this.btnCancel.Enabled = false;
      this.btnFinish.Enabled = false;
      this.tagRequesterData.Visible = true;
      if (int32 == Convert.ToInt32((object) enmRequirementPlateType.Premium))
      {
        this.tagContributorData.Visible = false;
      }
      else
      {
        if (this.Session["SystemUser"] == null)
          throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterRequirement.aspx");
        if ((this.Session["SystemUser"] as SystemUser).i_RoleConfigId == 30)
          this.CheckhasnotEmail.Visible = false;
        else
          this.CheckhasnotEmail.Visible = true;
        this.tagContributorData.Visible = true;
      }
      this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_SeleccionComprobante);
      this.txtRequesterMail.Text = this.Session["EmailSoli"].ToString();
      this.txtBeneficiaryMail.Text = this.Session["EmailSoli"].ToString();
      this.getLocation(int32);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      try
      {
        string str = "";
        if (this.ViewState["vwRequirementPlateType"] != null)
        {
          int int32 = Convert.ToInt32(this.Session["RequirementPlateType"]);
          str = int32 != Convert.ToInt32((object) enmRequirementPlateType.Regular) ? (int32 != Convert.ToInt32((object) enmRequirementPlateType.Premium) ? (int32 != Convert.ToInt32((object) enmRequirementPlateType.Reclamo) ? (int32 != Convert.ToInt32((object) enmRequirementPlateType.NotariaLiberada) ? "?t=r" : "?t=nl") : "?t=rc") : "?t=p") : "?t=r";
        }
        this.Response.Redirect("BeginRequirement.aspx" + str);
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
      string str = "";
      switch (this.rdbProofPayment.SelectedIndex)
      {
        case 0:
          str = "4";
          this.CboDocumentType.Items.FindByValue("4").Enabled = true;
          this.CboDocumentType.SelectedValue = str;
          this.CboDocumentType.Enabled = false;
          this.TxtDocNumberProofPaper.Text = "";
          this.txtBeneficiaryName.Text = "";
          this.cboDeliveryPoint.SelectedIndex = 0;
          this.TxtDocNumberProofPaper.Focus();
          break;
        case 1:
          str = "1";
          this.Label3.Visible = false;
          this.lblMessage1.Text = "";
          this.txtBeneficiaryName.Enabled = true;
          this.CboDocumentType.SelectedValue = str;
          this.CboDocumentType.Enabled = true;
          this.TxtDocNumberProofPaper.Text = "";
          this.txtBeneficiaryName.Text = "";
          this.CboDocumentType.Items.FindByValue("4").Enabled = false;
          this.TxtDocNumberProofPaper.Focus();
          break;
      }
      if (this.rdbProofPayment.SelectedIndex <= -1)
        return;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<script language='javascript'>");
      stringBuilder.Append("index2='" + str + "';");
      stringBuilder.Append("</script>");
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "JSCR", stringBuilder.ToString(), false);
    }

    protected void chkPorta_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkPorta.Checked)
      {
        this.chkTorni.Checked = false;
        this.trTorni.Visible = true;
      }
      else
        this.trTorni.Visible = false;
    }

    protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
    {
      if (this.Wizard1.ActiveStep == this.WS_SeleccionComprobante)
      {
        this.btnClaim = this.GetControlFromWizard(this.Wizard1, RegisterMassiveRequirement.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnClaim") as Button;
        this.btnFinishTemplate = this.GetControlFromWizard(this.Wizard1, RegisterMassiveRequirement.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnFinish") as Button;
        this.btnPreviousTemplate = this.GetControlFromWizard(this.Wizard1, RegisterMassiveRequirement.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnPrevious") as Button;
        if (this.btnFinishTemplate != null)
        {
          this.btnFinishTemplate.Text = "Grabar";
          this.btnPreviousTemplate.Visible = false;
        }
        if (this.btnClaim == null)
          return;
        this.btnClaim.Visible = false;
      }
      else if (this.Wizard1.ActiveStep == this.WS_DatosVehiculo1 || this.Wizard1.ActiveStep == this.WS_DatosVehiculo2)
      {
        this.btnClaim = this.GetControlFromWizard(this.Wizard1, RegisterMassiveRequirement.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnClaim") as Button;
        this.btnFinishTemplate = this.GetControlFromWizard(this.Wizard1, RegisterMassiveRequirement.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnFinish") as Button;
        this.btnPreviousTemplate = this.GetControlFromWizard(this.Wizard1, RegisterMassiveRequirement.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnPrevious") as Button;
        if (this.btnFinishTemplate != null)
        {
          this.btnFinishTemplate.Text = "Agregar";
          this.btnPreviousTemplate.Visible = true;
        }
        if (this.btnClaim == null)
          return;
        this.btnClaim.Visible = true;
      }
      else if (this.Wizard1.ActiveStep == this.WS_SeleccionMedioPago)
      {
        this.btnClaim = this.GetControlFromWizard(this.Wizard1, RegisterMassiveRequirement.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnClaim") as Button;
        this.btnCancelTemplate = this.GetControlFromWizard(this.Wizard1, RegisterMassiveRequirement.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnCancel") as Button;
        this.btnPreviousTemplate = this.GetControlFromWizard(this.Wizard1, RegisterMassiveRequirement.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnPrevious") as Button;
        this.btnFinishTemplate = this.GetControlFromWizard(this.Wizard1, RegisterMassiveRequirement.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnNext") as Button;
        if (this.btnClaim != null)
          this.btnClaim.Visible = false;
        if (this.btnCancelTemplate != null)
          this.btnCancelTemplate.Visible = false;
        if (this.btnPreviousTemplate != null)
          this.btnPreviousTemplate.Visible = false;
        if (this.btnFinishTemplate == null)
          return;
        this.btnFinishTemplate.Visible = false;
      }
      else
      {
        this.btnClaim = this.GetControlFromWizard(this.Wizard1, RegisterMassiveRequirement.WizardNavigationTempContainer.StartNavigationTemplateContainerID, "btnClaim") as Button;
        if (this.btnClaim != null)
        {
          if (this.Wizard1.ActiveStep == this.WS_ValidacionPlaca || this.Wizard1.ActiveStep == this.WS_ValidacionOrden || this.Wizard1.ActiveStep == this.WS_DatosVehiculo1 || this.Wizard1.ActiveStep == this.WS_DatosVehiculo2)
            this.btnClaim.Visible = true;
          else
            this.btnClaim.Visible = false;
        }
      }
    }

    protected void ReturnPage(object sender, EventArgs e)
    {
      this.Wizard1.ActiveStepIndex = 0;
      this.pnRegistration.Visible = false;
      this.txtPlateNumber.Text = "";
      this.txtTitleNumber.Text = "";
      this.btnAdd.Enabled = true;
      this.btnCancel.Enabled = true;
      this.btnFinish.Enabled = true;
    }

    protected void registerClaim(object sender, EventArgs e)
    {
      try
      {
        string pstrPlate = "";
        if (this.Wizard1.ActiveStep == this.WS_ValidacionOrden || this.Wizard1.ActiveStep == this.WS_DatosVehiculo1)
        {
          pstrPlate = this.txtPlateNumber.Text.Trim().Replace("-", "");
          if (pstrPlate == "")
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Vacia);
          if (pstrPlate.Length < 6)
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Longitud6_Incorrecta);
        }
        else if (this.Wizard1.ActiveStep == this.WS_ValidacionPlaca || this.Wizard1.ActiveStep == this.WS_DatosVehiculo2)
        {
          pstrPlate = this.txtPlateNumber1.Text.Trim().Replace("-", "");
          if (pstrPlate == "")
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Vacia);
          if (pstrPlate.Length < 6)
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Longitud6_Incorrecta);
        }
        this.dtClaim = new RequirementClaimQueriesBL().RequirementClaimGetActiveByPlate(pstrPlate);
        if (this.Wizard1.ActiveStepIndex == this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_ValidacionOrden) || this.Wizard1.ActiveStepIndex == this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_ValidacionPlaca))
        {
          if (this.dtClaim.Rows.Count > 0)
          {
            string str = this.dtClaim.Rows[0]["v_ClaimCode"].ToString();
            if (this.Wizard1.ActiveStep == this.WS_ValidacionOrden)
            {
              Message.SetMessage(this.lblMessage, new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Reclamo + str));
            }
            else
            {
              if (this.Wizard1.ActiveStep != this.WS_ValidacionPlaca)
                return;
              Message.SetMessage(this.lblMessage, new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Reclamo + str));
            }
          }
          else
            this.registerClaim_DataNotFound();
        }
        else
        {
          if (this.Wizard1.ActiveStepIndex != this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_DatosVehiculo1) && this.Wizard1.ActiveStepIndex != this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_DatosVehiculo2))
            return;
          if (this.dtClaim.Rows.Count > 0)
          {
            string str = this.dtClaim.Rows[0]["v_ClaimCode"].ToString();
            if (this.Wizard1.ActiveStep == this.WS_DatosVehiculo1)
              Message.SetMessage(this.lblMessage, new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Reclamo + str));
            else if (this.Wizard1.ActiveStep == this.WS_DatosVehiculo2)
              Message.SetMessage(this.lblMessage, new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Reclamo + str));
          }
          else
          {
            this.Session["i_VehicleId"] = (this.ViewState["SunarpData"] as DataTable).Rows[0]["i_VehicleId"];
            this.registerClaim_DataNoAgree();
          }
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

    protected void MoveNext(object sender, EventArgs e)
    {
      try
      {
        if (this.Wizard1.ActiveStep == this.WS_ValidacionOrden)
          this.ValidateSunarp();
        else if (this.Wizard1.ActiveStep == this.WS_ValidacionPlaca)
        {
          this.ValidatePlate();
        }
        else
        {
          if (this.Wizard1.ActiveStep != this.WS_SeleccionMedioPago)
            return;
          this.Session.Remove("VisaPagoConforme");
          string url = string.Format("~/Requirement/SuccessfulMassiveRegistration.aspx?id={0}", (object) Convert.ToInt32(this.ViewState["intRequirementID"]));
          this.Session["SendEmail"] = (object) 1;
          this.Response.Redirect(url, false);
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

    protected void ReturnAdd(object sender, EventArgs e)
    {
      this.Wizard1.ActiveStepIndex = 0;
      this.pnRegistration.Visible = false;
      this.tagRequesterData.Visible = false;
      this.tagContributorData.Visible = false;
      this.cboDeliveryPoint.SelectedIndex = -1;
      this.txtObservation.Text = "";
      this.btnAdd.Enabled = true;
      this.btnCancel.Enabled = true;
      this.btnFinish.Enabled = true;
    }

    protected void SaveRequirement(object sender, EventArgs e)
    {
      try
      {
        if (this.Wizard1.ActiveStepIndex == this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_DatosVehiculo1) || this.Wizard1.ActiveStepIndex == this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_DatosVehiculo2))
        {
          this.AddRequirement();
        }
        else
        {
          this.ValidateRequirementData();
          new ValidatorRegularExpressionProofPaymentBL().ProofPaymentData(this.CboDocumentType.Text.Trim(), this.TxtDocNumberProofPaper.Text.Trim(), this.txtBeneficiaryName.Text.Trim(), this.txtAddress.Text.Trim(), this.txtBeneficiaryMail.Text.Trim());
          this.tagRequesterData.Visible = false;
          this.tagContributorData.Visible = false;
          if ((Convert.ToInt32(this.ViewState["vwRequirementPlateType"]) == Convert.ToInt32((object) enmRequirementPlateType.Premium) || ConfigurationManager.AppSettings["Habilitar"].ToString() == "0") && this.rbTypePayment.Items.Count > 1)
            this.rbTypePayment.Items.RemoveAt(1);
          int int32 = Convert.ToInt32(this.cboDeliveryPoint.SelectedValue);
          if (int.Parse(ConfigurationManager.AppSettings["LocationAnnouncement"]) == 1)
          {
            if (int32 != 14)
            {
              string empty = string.Empty;
              this.CreatePopUpServer("SIIV - Anuncio", "../../UserControls/PopupConfirmationRequirement.aspx?", "370px", "250px");
            }
            else
              this.Wizard1.MoveTo((WizardStepBase) this.WS_SeleccionMedioPago);
          }
          else
            this.Wizard1.MoveTo((WizardStepBase) this.WS_SeleccionMedioPago);
        }
        this.divImgPortaPlaca.Visible = false;
        this.divImgPortaPlacaMoto.Visible = false;
        this.btnFinishTemplate = this.GetControlFromWizard(this.Wizard1, RegisterMassiveRequirement.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnFinish") as Button;
        if (!(this.btnFinishTemplate.Text == "Grabar"))
          return;
        this.gvList.Columns[0].Visible = false;
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
      this.Wizard1.MoveTo((WizardStepBase) this.WS_SeleccionMedioPago);
    }

    private Control GetControlFromWizard(
      Wizard wizard,
      RegisterMassiveRequirement.WizardNavigationTempContainer wzdTemplate,
      string controlName)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append((object) wzdTemplate);
      stringBuilder.Append("$");
      stringBuilder.Append(controlName);
      return wizard.FindControl(stringBuilder.ToString());
    }

    public void autoSetRequester()
    {
      try
      {
        SystemUser systemUser = this.ViewState["SystemUser"] != null ? (SystemUser) this.ViewState["SystemUser"] : throw new DataException("Sin variables de sesión");
        this.Session["EmailSoli"] = (object) systemUser.v_Email.Trim();
        this.oRequirementQueriesBL = new RequirementQueriesBL();
        string userExtendedAction = this.oRequirementQueriesBL.GetSystemUserExtendedAction(systemUser.i_SystemUserId, 1);
        if (!(userExtendedAction != ""))
          return;
        if (Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("3"))) == "3")
        {
          this.txtRequesterName.Text = systemUser.v_FirstName;
          string[] strArray = systemUser.v_LastName.Trim().Split(' ');
          this.txtRequesterLast1.Text = "-";
          this.txtRequesterLast2.Text = "-";
          if (strArray[0] != "")
            this.txtRequesterLast1.Text = strArray[0];
          if (strArray.Length > 1)
          {
            for (int index = 1; index < strArray.Length; ++index)
              this.txtRequesterLast2.Text += strArray[index];
          }
          this.txtRequesterMail.Text = systemUser.v_Email.Trim();
          this.txtRequesterPhone.Text = systemUser.v_Telephone.Trim();
          this.cboRequesterTypeDoc.SelectedValue = systemUser.i_DocumentTypeId.ToString();
          this.txtRequesterNumberDoc.Text = systemUser.v_DocumentNumber.Trim();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void Initialize()
    {
      this.getDocumentType();
      this.getProofPayment();
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
        this.CboDocumentType.SelectedIndex = 1;
        this.cboRequesterTypeDoc.DataSource = (object) this.dtDocument;
        this.cboRequesterTypeDoc.DataTextField = "v_Description";
        this.cboRequesterTypeDoc.DataValueField = "i_ParameterId";
        this.cboRequesterTypeDoc.DataBind();
        this.cboRequesterTypeDoc.SelectedIndex = 1;
        this.cboRequesterTypeDoc.Items.FindByValue("4").Enabled = false;
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

    public void BeginList()
    {
      try
      {
        this.lblCount.Text = "0";
        this.lblMonto.Text = "S/ 0";
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void BeginTitle() => this.Page.Title = "Solicitudes Masivas";

    public void refreshList()
    {
      try
      {
        if (this.ViewState["ListSunarpDataM"] == null)
          return;
        DataTable dataTable = this.ViewState["ListSunarpDataM"] as DataTable;
        this.gvList.DataSource = (object) dataTable;
        this.gvList.DataBind();
        this.lblCount.Text = this.gvList.Rows.Count.ToString();
        if (dataTable.Rows.Count != 0)
        {
          this.lblMonto.Text = "S/ " + Math.Round(Convert.ToDouble(dataTable.Compute("Sum(f_PriceSale)", "")), 2).ToString();
        }
        else
        {
          DataTable dtResult = new DataTable("Datos");
          this.TableColumns(dtResult);
          DataRow row = dtResult.NewRow();
          dtResult.Rows.Add(row);
          this.gvList.DataSource = (object) dtResult;
          this.gvList.DataBind();
          this.gvList.Rows[0].Visible = false;
          this.lblMonto.Text = "S/ 0";
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void getLocation(int i_requirementPlateTypeId)
    {
      try
      {
        if (this.Session["SystemUser"] == null)
          throw new HandledException(3, "La sesión ha expirado.", "'RegisterMassiveRequirement' - UniversalQuery.aspx");
        DataTable locationRequirement = new RequirementQueriesBL().GetLocationRequirement(((SystemUser) this.ViewState["SystemUser"]).i_SystemUserId);
        foreach (DataRow row in (InternalDataCollectionBase) locationRequirement.Rows)
        {
          if (row["i_LocationId"].ToString() == "17")
            row.Delete();
        }
        this.cboDeliveryPoint.DataSource = (object) locationRequirement;
        this.cboDeliveryPoint.DataTextField = "v_Description";
        this.cboDeliveryPoint.DataValueField = "i_LocationId";
        this.cboDeliveryPoint.DataBind();
        if (i_requirementPlateTypeId == Convert.ToInt32((object) enmRequirementPlateType.Premium))
          this.cboDeliveryPoint.SelectedIndex = 1;
        else
          this.cboDeliveryPoint.SelectedIndex = 0;
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
        SystemUser systemUser = (SystemUser) this.ViewState["SystemUser"];
        string pstrPlateNumber = this.txtPlateNumber.Text.Trim();
        string str = this.txtTitleNumber.Text.Trim();
        if (new RequirementQueriesBL().ValidateExistSunarpByPlateTitle(pstrPlateNumber, str) == 1)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Encontrada_No_Reclamo);
        int num1 = (int) this.ViewState["PublicUser"];
        string pstrRequesterFirstName = "";
        string pstrRequesterLastName = "";
        string pstrRequesterDocumentNumber = "";
        string pstrRequesterEmail = "";
        int num2 = -1;
        if (num1 == 1)
        {
          pstrRequesterFirstName = systemUser.v_FirstName;
          pstrRequesterLastName = systemUser.v_LastName;
          pstrRequesterDocumentNumber = systemUser.v_DocumentNumber;
          num2 = systemUser.i_DocumentTypeId.Value;
          pstrRequesterEmail = systemUser.v_Email;
        }
        this.CreatePopUp(claimGenerator.GenerateClaim_DataNoFound_PopUp(pstrPlateNumber, str, pstrRequesterFirstName, pstrRequesterLastName, num2.ToString(), pstrRequesterDocumentNumber, pstrRequesterEmail, ""), "Informacion No Encontrada", "575", "515");
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
        SystemUser systemUser = (SystemUser) this.ViewState["SystemUser"];
        this.oRequirementQueriesBL = new RequirementQueriesBL();
        this.dtVehicleData = this.oRequirementQueriesBL.SunarpDataReadbyId(Convert.ToInt32(this.Session["i_VehicleId"]));
        string pstrPlateNumber = this.dtVehicleData.Rows[0]["v_platenew"].ToString();
        string pstrPlateOld = this.dtVehicleData.Rows[0]["v_plateold"].ToString();
        string pstrVehicleModel = this.dtVehicleData.Rows[0]["v_model"].ToString();
        string pstrVehicleBrand = this.dtVehicleData.Rows[0]["v_Brand"].ToString();
        string pstrVehicleSerial = this.dtVehicleData.Rows[0]["v_serialNumber"].ToString();
        string str1 = !(this.dtVehicleData.Rows[0]["CategoryId"].ToString() != "") ? (string) null : this.dtVehicleData.Rows[0]["CategoryId"].ToString();
        string pstrCategory = !(this.dtVehicleData.Rows[0]["Category"].ToString() != "") ? "-" : this.dtVehicleData.Rows[0]["Category"].ToString();
        string pstrTitleNumber = this.dtVehicleData.Rows[0]["v_titlenumber"].ToString();
        this.dtOwners = this.oRequirementQueriesBL.GetOwnersByIdSunarp(Convert.ToInt32(this.Session["i_VehicleId"]));
        this.ViewState["VehicleClassId"] = (object) this.dtOwners.Rows[0]["ClassId"].ToString();
        string str2 = "";
        string str3 = "";
        string str4 = "";
        string str5 = "";
        for (int index = 0; index < this.dtOwners.Rows.Count; ++index)
        {
          str2 = str2 + "/" + this.dtOwners.Rows[index]["CompleteName"].ToString();
          str3 = str3 + "/" + this.dtOwners.Rows[index]["v_DocumentNumber"].ToString();
          str4 = str4 + "/" + this.dtOwners.Rows[index]["v_DocumentTypeId"].ToString();
          str5 = str5 + "/" + this.dtOwners.Rows[index]["i_Item"].ToString();
        }
        string pstrCategoryGroup = "204";
        str2.Substring(1, str2.Length - 1);
        str3.Substring(1, str3.Length - 1);
        str4.Substring(1, str4.Length - 1);
        str5.Substring(1, str5.Length - 1);
        int num1 = (int) this.ViewState["PublicUser"];
        string pstrRequesterFirstName = "";
        string pstrRequesterLastName = "";
        string pstrRequesterDocumentNumber = "";
        string pstrRequesterEmail = "";
        int num2 = -1;
        if (num1 == 1)
        {
          pstrRequesterFirstName = systemUser.v_FirstName;
          pstrRequesterLastName = systemUser.v_LastName;
          pstrRequesterDocumentNumber = systemUser.v_DocumentNumber;
          num2 = systemUser.i_DocumentTypeId.Value;
          pstrRequesterEmail = systemUser.v_Email;
        }
        this.CreatePopUp(claimGenerator.GenerateClaim_DataNoAgree_PopUp(pstrPlateNumber, pstrTitleNumber, pstrVehicleModel, pstrVehicleBrand, pstrVehicleSerial, this.Session["i_VehicleId"].ToString(), "", "", "", pstrPlateOld, pstrRequesterFirstName, pstrRequesterLastName, num2.ToString(), pstrRequesterDocumentNumber, pstrRequesterEmail, "", Convert.ToString(str1), pstrCategory, pstrCategoryGroup), "Informacion No Conforme", "740", "925");
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
        string str1 = this.txtPlateNumber.Text.ToUpper().Trim().Replace("-", "");
        string str2 = this.txtTitleNumber.Text.ToUpper().Trim().Replace("-", "");
        if (str1 == "")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Vacia);
        if (str2 == "")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Titulo_Vacio);
        if (str1.Length < 6)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Longitud6_Incorrecta);
        if (this.ViewState["ListSunarpDataM"] != null)
        {
          for (int index = 0; index < (this.ViewState["ListSunarpDataM"] as DataTable).Rows.Count; ++index)
          {
            if ((this.ViewState["ListSunarpDataM"] as DataTable).Rows[index]["v_PlateNew"].ToString().ToUpper().Replace("-", "") == this.txtPlateNumber.Text.ToUpper().Trim())
              throw new HandledException(1, "EL NUMERO DE PLACA YA FUE INGRESADA");
          }
        }
        this.oRequirementQueriesBL = new RequirementQueriesBL();
        nullable1 = new int?(this.oRequirementQueriesBL.ValidateExistRequirementByPlateTitle(str1, str2));
        int? nullable2 = nullable1.GetValueOrDefault() != -1 ? nullable1 : throw new HandledException(1, "EL NUMERO DE PLACA Y EL NUMERO DE TITULO FUERON TOMADOS EN UN TRAMITE ANTERIOR");
        int num1 = 0;
        if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
          throw new HandledException(1, "EL NUMERO DE PLACA TIENE UN TRAMITE EN CURSO, DEBE CULMINAR EL TRAMITE PARA INICIAR UNO NUEVO");
        if (nullable1.GetValueOrDefault() == -2)
          throw new HandledException(1, "EL NUMERO DE PLACA TERMINO UN TRAMITE DE PLACA (ENTREGADA)");
        if (nullable1.GetValueOrDefault() == -3)
          throw new HandledException(1, "EL NUMERO DE PLACA Y TITULO DETERMINAN UNA TRANFERENCIA DE PLACA NUEVA, SOLO ES UN REGISTRO INFORMATIVO PARA TRAMITES COMO DUPLICADO CON PLACA NUEVA, CAMBIO DE USO o DUPLICADO DE TERCERA PLACA");
        if (nullable1.GetValueOrDefault() == -4)
          throw new HandledException(1, "EL NUMERO DE PLACA Y TITULO DETERMINAN UNA CAMBIO DE CLASE/CARACTERISTICAS DE PLACA NUEVA, SOLO ES UN REGISTRO INFORMATIVO PARA TRAMITES COMO DUPLICADO CON PLACA NUEVA, CAMBIO DE USO O DUPLICADO DE TERCERA PLACA");
        nullable1 = new int?(this.oRequirementQueriesBL.ValidateExistSunarpByPlateTitle(str1, str2));
        if (nullable1.GetValueOrDefault() == 1)
        {
          this.lblMessage.Text = "";
          this.ShowVehicleData(str1, str2);
        }
        else
        {
          if (nullable1.GetValueOrDefault() == -3)
          {
            this.dtClaim = new RequirementClaimQueriesBL().RequirementClaimGetActiveByPlate(str1);
            string str3 = this.dtClaim.Rows[0]["v_ClaimCode"].ToString();
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Reclamo + str3);
          }
          nullable2 = nullable1;
          int num2 = 0;
          if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
            throw new HandledException(1, "EL NUMERO DE PLACA Y TITULO NO COINCIDEN, POR FAVOR REINTENTE");
          if (nullable1.GetValueOrDefault() == -1)
            throw new HandledException(1, "SU REGISTRO NO HA SIDO ENCONTRADO, POR FAVOR ESPERE 24 HORAS Y VUELVA A INTENTAR. SI EL PROBLEMA PERSISTE COMUNIQUESE CON SUNARP");
          if (nullable1.GetValueOrDefault() == -2)
            throw new HandledException(1, "SU REGISTRO HA SIDO ENCONTRADO, PERO EL TIPO DE TRAMITE REPORTADO NO HA SIDO HOMOLOGADO CON NUESTRO SISTEMA, POR FAVOR REINTENTE EN 48 HORAS. DISCULPE LAS MOLESTIAS");
        }
        if (this.ViewState["VehicleClassId"] != null || this.ViewState["VehicleClassId"].ToString() != "")
        {
          if (Convert.ToInt32(this.ViewState["VehicleClassId"].ToString()) != 5)
            this.divImgPortaPlaca.Visible = true;
          else
            this.divImgPortaPlacaMoto.Visible = true;
        }
        this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_DatosVehiculo1);
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
        this.dtVehicleData = !this.isRegisterCall ? this.oRequirementQueriesBL.SunarpDataRead(strPlateNumber, strTitle) : this.oRequirementQueriesBL.SunarpDataRead(strPlateNumber, strTitle, this.isRegisterCall);
        this.ViewState["SunarpData"] = (object) this.dtVehicleData;
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
        this.txtSerialNumber.Text = this.dtVehicleData.Rows[0]["v_SerialNumber"].ToString();
        this.txtDispatchDate.Text = Convert.ToDateTime(this.dtVehicleData.Rows[0]["d_DispatchDate"].ToString()).ToString("dd/MM/yyyy");
        this.dtOwners = this.oRequirementQueriesBL.GetOwnersByIdSunarp(Convert.ToInt32(this.dtVehicleData.Rows[0][0].ToString()));
        this.ViewState["VehicleClassId"] = (object) this.dtOwners.Rows[0]["ClassId"].ToString();
        this.ViewState["v_Igv"] = (object) this.dtVehicleData.Rows[0]["v_Igv"].ToString();
        this.ViewState["f_PriceCost"] = (object) this.dtVehicleData.Rows[0]["f_PriceCost"].ToString();
        this.ViewState["f_PriceCostF"] = (object) this.dtVehicleData.Rows[0]["f_PriceCost"].ToString();
        this.ViewState["Owners"] = (object) this.dtOwners;
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
        int productCorrespondence = new RequirementQueriesBL().GetProductCorrespondence((int) this.dtVehicleData.Rows[0]["i_ProductId"], 4);
        if (productCorrespondence > 0)
        {
          this.tdProcAdd.Visible = true;
          this.chkPorta.Checked = false;
          this.chkTorni.Checked = false;
        }
        this.txtPlateNumber.Enabled = false;
        this.txtTitleNumber.Enabled = false;
        if (productCorrespondence == 375 || productCorrespondence == 376 || productCorrespondence == 460 || productCorrespondence == 461 || productCorrespondence == 414 || productCorrespondence == 478 || productCorrespondence == 477 || productCorrespondence == 509 || productCorrespondence == 514 || productCorrespondence == 523 || productCorrespondence == 525 || productCorrespondence == 524 || productCorrespondence == 526)
        {
          this.contAddMoto.Visible = true;
          this.contAdd.Visible = false;
          this.chkPortaMoto.Checked = false;
        }
        else
        {
          this.contAddMoto.Visible = false;
          this.contAdd.Visible = true;
          this.chkPortaMoto.Checked = false;
        }
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
        if (string.IsNullOrEmpty(this.txtPlateNumber1.Text))
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Vacia);
        if (this.ViewState["ListSunarpDataM"] != null)
        {
          for (int index = 0; index < (this.ViewState["ListSunarpDataM"] as DataTable).Rows.Count; ++index)
          {
            if ((this.ViewState["ListSunarpDataM"] as DataTable).Rows[index]["v_PlateNew"].ToString().ToUpper().Replace("-", "") == this.txtPlateNumber1.Text.ToUpper().Trim().Replace("-", ""))
              throw new HandledException(1, "EL NUMERO DE PLACA YA FUE INGRESADA");
          }
        }
        string empty = string.Empty;
        string pstrPlateNumber = this.txtPlateNumber1.Text.Replace("-", "");
        this.oRequirementQueriesBL = new RequirementQueriesBL();
        switch (this.oRequirementQueriesBL.Verify3rdPlate(pstrPlateNumber))
        {
          case -2:
            this.dtClaim = new RequirementClaimQueriesBL().RequirementClaimGetActiveByPlate(this.txtPlateNumber1.Text.Trim().Replace("-", ""));
            string str = this.dtClaim.Rows[0]["v_ClaimCode"].ToString();
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Placa_Reclamo + str);
          case -1:
            throw new HandledException(1, "EL NUMERO DE PLACA INGRESADO NO HA REGISTRADO UN TRAMITE PREVIO DE PLACA NUEVA");
          case 0:
            throw new HandledException(1, "EL NUMERO DE PLACA TIENE UN TRAMITE EN CURSO, DEBE CULMINAR EL TRAMITE PARA INICIAR UNO NUEVO");
          default:
            if (false)
              throw new HandledException(1, "LA PLACA INGRESADA PERTENECE A UNA CLASE VEHICULAR QUE NO PERMITE TERCERA PLACA");
            this.ShowVehicleData3rd();
            if (this.ViewState["VehicleClassId"] != null || this.ViewState["VehicleClassId"] != (object) "")
            {
              if (Convert.ToInt32(this.ViewState["VehicleClassId"].ToString()) != 5)
                this.divImgPortaPlaca.Visible = true;
              else
                this.divImgPortaPlacaMoto.Visible = true;
            }
            this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosVehiculo2);
            break;
        }
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
        this.lblPrice.Visible = true;
        this.txtPrice3rd.Visible = true;
        this.dtVehicleData = this.oRequirementQueriesBL.SunarpDataRead3rd(this.txtPlateNumber1.Text.Trim());
        this.ViewState["SunarpData"] = (object) this.dtVehicleData;
        this.txtBrand3rd.Text = this.dtVehicleData.Rows[0]["v_Brand"].ToString();
        this.txtModel3rd.Text = this.dtVehicleData.Rows[0]["v_Model"].ToString();
        this.txtPlateNew3rd.Text = this.dtVehicleData.Rows[0]["v_PlateNew"].ToString();
        this.txtSerialNumber3rd.Text = this.dtVehicleData.Rows[0]["v_SerialNumber"].ToString();
        this.txtUseType3rd.Text = this.dtVehicleData.Rows[0]["TypeUseDescription"].ToString();
        this.txtCategory3rd.Text = this.dtVehicleData.Rows[0]["Category"].ToString();
        this.dtOwners = this.oRequirementQueriesBL.GetOwnersByIdSunarp(Convert.ToInt32(this.dtVehicleData.Rows[0][0].ToString()));
        this.ViewState["VehicleClassId"] = (object) this.dtOwners.Rows[0]["ClassId"].ToString();
        this.ViewState["Owners"] = (object) this.dtOwners;
        for (int index = 0; index < this.dtOwners.Rows.Count; ++index)
          this.txtOwner3rd.Text = this.dtOwners.Rows[index]["completeName"].ToString();
        string empty = string.Empty;
        this.DtProduct = this.oRequirementQueriesBL.ValidateProductPrice3rd(this.txtPlateNumber1.Text.Replace("-", ""));
        this.ViewState["Product3rd"] = (object) this.DtProduct;
        this.txtPrice3rd.Text = this.DtProduct.Rows[0]["f_PriceSale"].ToString();
        this.txtPlateOld3rd.Text = this.dtVehicleData.Rows[0]["v_PlateOld"].ToString();
        this.txtPlateNumber1.Enabled = false;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void AddRequirement()
    {
      try
      {
        if (!this.chkAccept.Checked)
          throw new HandledException(1, "DEBE CONFIRMAR A LA ASOCIACIÓN AUTOMOTRIZ DEL PERU QUE LA INFORMACION MOSTRADA ES LA CORRECTA");
        this.ValidateData();
        this.InsertListGroup(this.txtPlateNumber.Text.Trim().ToUpper(), this.txtTitleNumber.Text.Trim().ToUpper());
        this.lblMessage.Text = "";
        this.chkAccept.Checked = false;
        this.pnRegistration.Visible = false;
        this.txtPlateNumber.Text = "";
        this.txtTitleNumber.Text = "";
        this.btnAdd.Enabled = true;
        this.btnCancel.Enabled = true;
        this.btnFinish.Enabled = true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ValidateData()
    {
      if (this.txtOwners.Text.Trim() == "")
        throw new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados");
      if (this.txtModel.Text.Trim() == "")
        throw new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados");
      if (this.txtBrand.Text.Trim() == "")
        throw new HandledException(1, "LA MARCA NO PUEDE SER VACIO");
      if (this.txtSerialNumber.Text.Trim() == "")
        throw new HandledException(1, "EL NÚMERO SERIE NO PUEDE SER VACIO");
      if (this.txtSerialNumber.Text.Trim().Length <= 2)
        throw new HandledException(1, "La cantidad de caracteres del numero de serie no puede ser menor a dos");
    }

    private void InsertListGroup(string _Plate, string _Title)
    {
      try
      {
        string empty = string.Empty;
        string str1 = this.txtPlateNumber.Text.ToUpper().Trim().Replace("-", "");
        int pintProductId = 0;
        Decimal f_PriceCost = 0M;
        Decimal f_PriceTax = 0M;
        Decimal f_PriceSale = 0M;
        this.dtListVehicleData = this.ViewState["ListSunarpDataM"] as DataTable;
        if (this.dtListVehicleData == null)
          this.dtListVehicleData = (this.ViewState["SunarpData"] as DataTable).Clone();
        foreach (DataRow row in (InternalDataCollectionBase) (this.ViewState["SunarpData"] as DataTable).Rows)
        {
          this.dtListVehicleData.ImportRow(row);
          if (row["v_PlateNew"].ToString() == str1)
          {
            if (this.chkPorta.Checked)
            {
              this.oRequirementQueriesBL = new RequirementQueriesBL();
              pintProductId = !this.chkTorni.Checked ? new RequirementQueriesBL().GetProductCorrespondence((int) row["i_ProductId"], 4) : new RequirementQueriesBL().GetProductCorrespondence((int) row["i_ProductId"], 5);
            }
            if (this.chkPortaMoto.Checked)
            {
              this.oRequirementQueriesBL = new RequirementQueriesBL();
              pintProductId = new RequirementQueriesBL().GetProductCorrespondence((int) row["i_ProductId"], 4);
            }
            if (pintProductId > 0)
            {
              new RequirementQueriesBL().RequirementGetPrice(pintProductId, out f_PriceCost, out f_PriceTax, out f_PriceSale);
              this.dtListVehicleData.Columns["i_ProductId"].ReadOnly = false;
              this.dtListVehicleData.Columns["f_PriceCost"].ReadOnly = false;
              this.dtListVehicleData.Columns["f_PriceTax"].ReadOnly = false;
              this.dtListVehicleData.Columns["f_PriceSale"].ReadOnly = false;
              this.dtListVehicleData.Rows[this.dtListVehicleData.Rows.Count - 1]["i_ProductId"] = (object) pintProductId;
              this.dtListVehicleData.Rows[this.dtListVehicleData.Rows.Count - 1]["f_PriceCost"] = (object) f_PriceCost;
              this.dtListVehicleData.Rows[this.dtListVehicleData.Rows.Count - 1]["f_PriceTax"] = (object) f_PriceTax;
              this.dtListVehicleData.Rows[this.dtListVehicleData.Rows.Count - 1]["f_PriceSale"] = (object) f_PriceSale;
            }
          }
        }
        this.ViewState["ListSunarpDataM"] = (object) this.dtListVehicleData;
        this.dtOwners = this.ViewState["Owners"] as DataTable;
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
        string str6 = str2.Substring(1, str2.Length - 1);
        string str7 = str3.Substring(1, str3.Length - 1);
        string str8 = str4.Substring(1, str4.Length - 1);
        string str9 = str5.Substring(1, str5.Length - 1);
        this.dtListOwners = this.Session["ListOwners"] as DataTable;
        if (this.dtListOwners == null)
        {
          RegisterMassiveRequirement.colVideos[0] = new DataColumn("strCompleteName");
          RegisterMassiveRequirement.colVideos[1] = new DataColumn("strDocumentType");
          RegisterMassiveRequirement.colVideos[2] = new DataColumn("strDocumentDescription");
          RegisterMassiveRequirement.colVideos[3] = new DataColumn("strDocumentNumber");
          this.dtListOwners = new DataTable();
          this.dtListOwners.Columns.AddRange(RegisterMassiveRequirement.colVideos);
        }
        this.dtListOwners.Rows.Add((object) str6, (object) str7, (object) str8, (object) str9);
        this.Session["ListOwners"] = (object) this.dtListOwners;
        this.gvList.DataSource = (object) (this.ViewState["ListSunarpDataM"] as DataTable);
        this.gvList.DataBind();
        this.lblCount.Text = this.gvList.Rows.Count.ToString();
        this.lblMonto.Text = "S/ " + Math.Round(Convert.ToDouble(this.dtListVehicleData.Compute("Sum(f_PriceSale)", "")), 2).ToString();
        this.tdProcAdd.Visible = false;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void FinishSpecialRequirement()
    {
      try
      {
        SystemUser systemUser1 = this.ViewState["SystemUser"] != null ? (SystemUser) this.ViewState["SystemUser"] : throw new DataException("Sin variables de sesión");
        this.dtIDS = new DataTable();
        this.dtIDS.Columns.Add("i_RequirementId", typeof (int));
        this.dtIDS.Columns.Add("i_RequirementPlateId", typeof (int));
        this.dtIDS.Columns.Add("i_Success", typeof (int));
        Convert.ToInt32(this.Request.QueryString["idDoc"]);
        bool flag = true;
        string ErrorMessage = "";
        this.oRequirementQueriesBL = new RequirementQueriesBL();
        for (int index = 0; index < (this.ViewState["ListSunarpDataM"] as DataTable).Rows.Count; ++index)
        {
          int num;
          try
          {
            num = this.oRequirementQueriesBL.ValidateExistRequirementByPlateTitle((this.ViewState["ListSunarpDataM"] as DataTable).Rows[index]["v_platenew"].ToString(), (this.ViewState["ListSunarpDataM"] as DataTable).Rows[index]["v_titlenumber"].ToString());
          }
          catch (HandledException ex)
          {
            num = 1;
          }
          switch (num)
          {
            case -4:
              flag = false;
              ErrorMessage = ErrorMessage + "EL NUMERO DE PLACA " + (this.ViewState["ListSunarpDataM"] as DataTable).Rows[index]["v_platenew"].ToString() + "Y TITULO DETERMINAN UNA CAMBIO DE CLASE/CARACTERISTICAS DE PLACA NUEVA, SOLO ES UN REGISTRO INFORMATIVO PARA TRAMITES COMO DUPLICADO CON PLACA NUEVA, CAMBIO DE USO O DUPLICADO DE TERCERA PLACA</br>";
              break;
            case -3:
              flag = false;
              ErrorMessage = ErrorMessage + "EL NUMERO DE PLACA " + (this.ViewState["ListSunarpDataM"] as DataTable).Rows[index]["v_platenew"].ToString() + "Y TITULO DETERMINAN UNA TRANFERENCIA DE PLACA NUEVA, SOLO ES UN REGISTRO INFORMATIVO PARA TRAMITES COMO DUPLICADO CON PLACA NUEVA, CAMBIO DE USO o DUPLICADO DE TERCERA PLACA</br>";
              break;
            case -2:
              flag = false;
              ErrorMessage = ErrorMessage + "EL NUMERO DE PLACA " + (this.ViewState["ListSunarpDataM"] as DataTable).Rows[index]["v_platenew"].ToString() + " TERMINO UN TRAMITE DE PLACA (ENTREGADA)</br>";
              break;
            case -1:
              flag = false;
              ErrorMessage = ErrorMessage + "EL NUMERO DE PLACA " + (this.ViewState["ListSunarpDataM"] as DataTable).Rows[index]["v_platenew"].ToString() + " Y EL NUMERO DE TITULO FUERON TOMADOS EN UN TRAMITE ANTERIOR</br>";
              break;
            case 0:
              flag = false;
              ErrorMessage = ErrorMessage + "EL NUMERO DE PLACA " + (this.ViewState["ListSunarpDataM"] as DataTable).Rows[index]["v_platenew"].ToString() + " TIENE UN TRAMITE EN CURSO, DEBE CULMINAR EL TRAMITE PARA INICIAR UNO NUEVO</br>";
              break;
          }
        }
        if (!flag)
          throw new HandledException(1, ErrorMessage);
        using (TransactionScope transactionScope = new TransactionScope())
        {
          this.DtRequirements = this.RequirementPlateDataTableCreate();
          double num1 = 0.0;
          double num2 = 0.0;
          double num3 = 0.0;
          this.dtListVehicleData = (DataTable) this.ViewState["ListSunarpDataM"];
          this.objRequirement = new SIIV.BE.Requirement();
          this.objRequirement.i_RequirementTypeId = new int?(1);
          this.objRequirement.f_Quantity = new double?((double) this.dtListVehicleData.Rows.Count);
          this.objRequirement.v_Observations = "";
          this.objRequirement.i_ProofPaymentTypeId = new int?(Convert.ToInt32(this.rdbProofPayment.SelectedValue.ToString()));
          this.objRequirement.i_InsertUserId = new int?(systemUser1.i_SystemUserId);
          this.objRequirement.v_Ubigeo = "";
          this.objRequirement.i_Status = new int?(1);
          this.objContributorRequester = new RequirementContributor();
          if ((int) this.ViewState["PublicUser"] == 1)
          {
            this.objContributorRequester.i_DocumentTypeId = systemUser1.i_DocumentTypeId;
            this.objContributorRequester.v_DocumentNumber = systemUser1.v_DocumentNumber;
            this.objContributorRequester.v_LastName = systemUser1.v_LastName;
            this.objContributorRequester.v_FirstName = systemUser1.v_FirstName;
            this.objContributorRequester.v_CompleteName = systemUser1.v_FirstName + " " + systemUser1.v_LastName;
            this.objContributorRequester.v_Address = systemUser1.v_Address;
            this.objContributorRequester.v_AddressLocation = systemUser1.v_Ubigeo;
            this.objContributorRequester.v_PhoneNumber = (string) null;
            this.objContributorRequester.v_Email = systemUser1.v_Email;
            this.objContributorRequester.i_PersonTypeId = new int?();
          }
          else
          {
            this.objContributorRequester.i_DocumentTypeId = new int?(Convert.ToInt32(this.cboRequesterTypeDoc.SelectedValue.ToString()));
            this.objContributorRequester.v_DocumentNumber = this.txtRequesterNumberDoc.Text.Trim();
            this.objContributorRequester.v_LastName = this.txtRequesterLast1.Text.Trim() + " " + this.txtRequesterLast2.Text.Trim();
            this.objContributorRequester.v_FirstName = this.txtRequesterName.Text.Trim();
            this.objContributorRequester.v_CompleteName = this.txtRequesterName.Text.Trim() + " " + this.txtRequesterLast1.Text.Trim() + " " + this.txtRequesterLast2.Text.Trim();
            this.objContributorRequester.v_Address = (string) null;
            this.objContributorRequester.v_AddressLocation = (string) null;
            this.objContributorRequester.v_PhoneNumber = this.txtRequesterPhone.Text.Trim();
            this.objContributorRequester.v_Email = this.txtRequesterMail.Text.Trim();
            this.objContributorRequester.i_PersonTypeId = new int?(1);
          }
          this.objRequirementContributor = new RequirementContributor();
          this.objRequirementContributor.i_DocumentTypeId = new int?(Convert.ToInt32(this.CboDocumentType.SelectedValue.ToString()));
          this.objRequirementContributor.v_DocumentNumber = this.TxtDocNumberProofPaper.Text.Trim();
          this.objRequirementContributor.v_LastName = "";
          this.objRequirementContributor.v_FirstName = "";
          this.objRequirementContributor.v_CompleteName = this.txtBeneficiaryName.Text.Trim();
          this.objRequirementContributor.v_Address = this.txtAddress.Text.Trim();
          this.objRequirementContributor.v_Email = this.txtBeneficiaryMail.Text.Trim();
          this.Session["ClientMailforVISA"] = (object) this.txtBeneficiaryMail.Text;
          this.Session["ClientTypeDocumentforVISA"] = Convert.ToInt32(this.CboDocumentType.SelectedValue) == 1 ? (object) "DNI" : (Convert.ToInt32(this.CboDocumentType.SelectedValue) == 2 ? (object) "PAS" : (Convert.ToInt32(this.CboDocumentType.SelectedValue) == 3 ? (object) "CEX" : (Convert.ToInt32(this.CboDocumentType.SelectedValue) == 4 ? (object) "RUC" : (object) "CEX")));
          this.Session["ClientNumberDocumentforVISA"] = (object) this.TxtDocNumberProofPaper.Text;
          SystemUser systemUser2 = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterRequirement.aspx");
          this.Session["ClientIdforVISA"] = (object) systemUser2.i_SystemUserId;
          this.Session["ClientTypeRegisterforVISA"] = (object) "Registrado";
          DateTime now = DateTime.Now;
          DateTime? dInsertDate = systemUser2.d_InsertDate;
          TimeSpan? nullable = dInsertDate.HasValue ? new TimeSpan?(now - dInsertDate.GetValueOrDefault()) : new TimeSpan?();
          ref TimeSpan? local = ref nullable;
          this.Session["ClientTotalDaysRegisterforVISA"] = (object) Convert.ToInt32((object) (local.HasValue ? new double?(local.GetValueOrDefault().TotalDays) : new double?()));
          this.oRequirementManagement = new RequirementManagementBL();
          foreach (DataRow row1 in (InternalDataCollectionBase) this.dtListVehicleData.Rows)
          {
            DataRow row2 = this.DtRequirements.NewRow();
            row2["i_DeliveryPoINTId"] = (object) Convert.ToInt32(this.cboDeliveryPoint.SelectedValue);
            row2["i_RegistrationTypeId"] = (object) Convert.ToInt32(row1["i_VehicleRegistrationId"].ToString());
            row2["i_RegistrationOfficeId"] = (object) Convert.ToInt32(row1["RegistryOfficeId"].ToString());
            row2["i_RegistryZoneId"] = (object) Convert.ToInt32(row1["RegistryZoneId"].ToString());
            row2["i_VehicleCategoryId"] = (object) Convert.ToInt32(row1["CategoryId"].ToString() == "" ? (string) null : row1["CategoryId"].ToString());
            row2["i_VehicleTypeUseId"] = (object) Convert.ToInt32(row1["UseTypeSunarp"].ToString());
            row2["i_VehicleClassId"] = (object) Convert.ToInt32(row1["Classid"].ToString());
            row2["v_CompleteNameOwner"] = (object) row1["v_OwnerCompleteName"].ToString();
            row2["i_requirementPlatetypeId"] = (object) Convert.ToInt32(this.ViewState["vwRequirementPlateType"]);
            row2["i_SpecialPlateTypeid"] = (object) DBNull.Value;
            row2["i_ProductId"] = (object) Convert.ToInt32(row1["i_ProductId"].ToString());
            row2["i_VehicleId"] = (object) Convert.ToInt32(row1["i_VehicleId"].ToString());
            row2["v_RegistrationCode"] = (object) "";
            row2["i_DataBankId"] = (object) DBNull.Value;
            row2["i_ProcessTypeId"] = (object) Convert.ToInt32(row1["ProcessTypeId"].ToString());
            row2["i_ContigencyTypeId"] = (object) DBNull.Value;
            row2["b_ContigencyDelivery"] = (object) DBNull.Value;
            row2["i_RegistrationUseTypeOldId"] = (object) DBNull.Value;
            row2["d_RegistrationDispatchDate"] = (object) Convert.ToDateTime(row1["d_DispatchDate"].ToString());
            row2["i_PlateTypeId"] = (object) 1;
            row2["b_PendingConfirmation"] = (object) DBNull.Value;
            row2["b_Migrated"] = (object) DBNull.Value;
            row2["i_StatusRequirementPlate"] = (object) 0;
            row2["i_InsertUserId"] = (object) systemUser2.i_SystemUserId;
            this.DtRequirements.Rows.Add(row2);
            num1 += Convert.ToDouble(row1["f_PriceCost"].ToString());
            num2 += Convert.ToDouble(row1["f_PriceTax"].ToString());
            num3 += Convert.ToDouble(row1["f_PriceSale"].ToString());
          }
          this.objPayment = new Payment();
          if (Convert.ToInt32(this.ViewState["vwRequirementPlateType"]) == Convert.ToInt32((object) enmRequirementPlateType.Premium) || Convert.ToInt32(this.ViewState["vwRequirementPlateType"]) == Convert.ToInt32((object) enmRequirementPlateType.NotariaLiberada))
          {
            this.objPayment.f_PriceSale = new double?();
            this.objPayment.f_PriceTax = new double?();
            this.objPayment.f_PriceTotal = new double?();
            this.objRequirement.i_Status = new int?(1);
            this.objPayment.i_Status = new int?();
            this.objPayment.i_PaymentTypeId = new int?();
            this.objPayment.i_BankId = new int?();
            this.objPayment.v_BankOperationNumber = (string) null;
            this.objPayment.v_BankOperationUser = (string) null;
            this.objPayment.v_BankOperationTerminal = (string) null;
            this.objPayment.i_AccountId = new int?();
            this.objPayment.d_BankOperationDate = new DateTime?();
            foreach (DataRow row in (InternalDataCollectionBase) this.DtRequirements.Rows)
              row["i_StatusRequirementPlate"] = (object) 1;
          }
          else
          {
            this.objPayment.f_PriceSale = new double?(Math.Round(num1, 2));
            this.objPayment.f_PriceTax = new double?(Math.Round(num2, 2));
            this.objPayment.f_PriceTotal = new double?(Math.Round(num3, 2));
            this.objPayment.i_Status = new int?(0);
            this.objPayment.i_PaymentTypeId = new int?(1);
            this.objPayment.i_BankId = new int?();
            this.objPayment.v_BankOperationNumber = (string) null;
            this.objPayment.v_BankOperationUser = (string) null;
            this.objPayment.v_BankOperationTerminal = (string) null;
            this.objPayment.i_AccountId = new int?();
          }
          this.ViewState["f_PriceTotal"] = (object) this.objPayment.f_PriceTotal;
          int num4 = this.oRequirementManagement.RequirementMassiveInsert(this.objRequirement, this.objRequirementContributor, this.DtRequirements, this.objPayment, this.objContributorRequester, this.Request.UserHostAddress);
          transactionScope.Complete();
          this.Session["Monto"] = (object) this.lblMonto.Text;
          this.ViewState["intRequirementID"] = (object) num4;
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

    private void ValidateRequirementData()
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
        if (this.txtRequesterPhone.Text.Trim() == "")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Ingrese_Telefono);
        if (this.cboRequesterTypeDoc.SelectedValue == "1" && this.txtRequesterNumberDoc.Text.Trim().Length != 8)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_DNI_8);
        if (this.cboRequesterTypeDoc.SelectedValue == "2" && this.txtRequesterNumberDoc.Text.Trim().Length > 12)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_PASAPORTE_12 + " COMO MAXIMO");
        if (this.cboRequesterTypeDoc.SelectedValue == "3" && this.txtRequesterNumberDoc.Text.Trim().Length > 12)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_CAEXTRANJERIA_12 + " COMO MAXIMO");
        if (this.cboRequesterTypeDoc.SelectedValue == "4" && this.txtRequesterNumberDoc.Text.Trim().Length != 11)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_11);
        if (this.cboRequesterTypeDoc.SelectedValue == "4" && this.txtRequesterNumberDoc.Text.Trim().Substring(0, 1) != "1" && this.txtRequesterNumberDoc.Text.Trim().Substring(0, 1) != "2")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_Longitud);
        if (this.cboRequesterTypeDoc.SelectedValue == "4" && !this.ValidateRuc(this.txtRequesterNumberDoc.Text.Trim()))
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_Formato);
        if (Convert.ToInt32(this.Session["RequirementPlateType"]) != Convert.ToInt32((object) enmRequirementPlateType.Premium))
        {
          if (this.txtBeneficiaryName.Text == "")
            throw new HandledException(1, "INGRESE NOMBRES O RAZON SOCIAL DEL COMPROBANTE");
          if (this.CboDocumentType.DataTextField == "" || this.CboDocumentType.SelectedIndex == 0)
            throw new HandledException(1, "SELECCIONE TIPO DE DOCUMENTO DE IDENTIDAD");
          if (this.TxtDocNumberProofPaper.Text.Trim() == "")
            throw new HandledException(1, "INGRESE NUMERO DE DOCUMENTO DEL COMPROBANTE");
          if (this.CboDocumentType.SelectedValue == "1" && this.TxtDocNumberProofPaper.Text.Trim().Length != 8)
            throw new HandledException(1, "DNI DEBE TENER 8 DIGITOS");
          if (this.CboDocumentType.SelectedValue == "1" && this.TxtDocNumberProofPaper.Text.Trim() == "00000000")
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_DNI_FORMATO_CORRECTO);
          if (this.CboDocumentType.SelectedValue == "2" && this.TxtDocNumberProofPaper.Text.Trim() == "00000000000")
            throw new HandledException(1, "NÚMERO DE PASAPORTE NO TIENE UN FORMATO NUMERICO CORRECTO");
          if (this.CboDocumentType.SelectedValue == "2" && this.TxtDocNumberProofPaper.Text.Trim().Length > 12)
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_PASAPORTE_12);
          if (this.CboDocumentType.SelectedValue == "3" && this.TxtDocNumberProofPaper.Text.Trim() == "00000000000")
            throw new HandledException(1, "NÚMERO DE CARNET DE EXTRANJERIA NO TIENE UN FORMATO NUMERICO CORRECTO");
          if (this.CboDocumentType.SelectedValue == "3" && this.TxtDocNumberProofPaper.Text.Trim().Length > 12)
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_CAEXTRANJERIA_12);
          if (this.CboDocumentType.SelectedValue == "4" && this.TxtDocNumberProofPaper.Text.Trim().Length != 11)
            throw new HandledException(1, "RUC INGRESADO NO ES VALIDO LONGITUD INCORRECTA");
          if (this.CboDocumentType.SelectedValue == "4" && this.TxtDocNumberProofPaper.Text.Trim().Substring(0, 1) != "1" && this.TxtDocNumberProofPaper.Text.Trim().Substring(0, 1) != "2")
            throw new HandledException(1, "RUC INGRESADO NO ES VALIDO DEBE INICIARCE EN 1 Ó 2");
          if (this.CboDocumentType.SelectedValue == "4" && !this.ValidateRuc(this.TxtDocNumberProofPaper.Text.Trim()))
            throw new HandledException(1, "LA ESTRUCTURA DEL NUMERO DE RUC INGRESADO NO ES CORRECTO");
          if (this.cboDeliveryPoint.SelectedIndex == 0 || this.cboDeliveryPoint.SelectedIndex <= 0)
            throw new HandledException(1, "SELECCIONE PUNTO DE ENTREGA");
          if (this.txtDocumentNumber.Text.Trim() == "20101973922")
            throw new HandledException(1, "NO SE PUEDE REALIZAR TRAMITES CON ESTE NUMERO DE RUC");
          if (!this.RestrictionRazonSocial())
            throw new HandledException(1, "ESTA RESTRINGIDA LA CREACION DE TRAMITES CON ESTA RAZON SOCIAL");
          if (this.CboDocumentType.SelectedValue == "4" && this.txtAddress.Text.Trim() == "")
            throw new HandledException(1, "DEBE INGRESAR SU DIRECCIÓN");
          if (!new Email().IsValidEmail(this.txtBeneficiaryMail.Text.Trim()) && !string.IsNullOrEmpty(this.txtBeneficiaryMail.Text.Trim()))
            throw new HandledException(1, "Debe Ingresar un Email Valido");
          if ((this.CboDocumentType.SelectedValue == "1" || this.CboDocumentType.SelectedValue == "4") && !Regex.IsMatch(this.TxtDocNumberProofPaper.Text.Trim(), "^[0-9]+$"))
            throw new HandledException(1, "EL NUMERO DE DOCUMENTO DEBE CONTENER SOLO NUMEROS");
          int num = int.Parse(ConfigurationManager.AppSettings["RucValidation"]);
          if (num == 1 && this.CboDocumentType.SelectedValue == "4")
          {
            if ((int) this.ViewState["ValidationRUC"] != 1)
              throw new HandledException(1, "Validar que el numero de RUC sea el correcto");
            if (this.ViewState["RUC"].ToString() != this.TxtDocNumberProofPaper.Text.Trim())
              throw new HandledException(1, "NUMERO DE RUC NO SE ENCUENTRA VALIDADO");
          }
          if (num == 2 && this.CboDocumentType.SelectedValue == "4")
          {
            if (this.ViewState["ValidationRUC1"] == null)
              this.BtnValidate_ClickRuc((object) null, (EventArgs) null);
            if ((int) this.ViewState["ValidationRUC1"] != 1)
              throw new HandledException(1, "Validar que el numero de RUC sea el correcto");
            if (this.ViewState["RUC1"].ToString() != this.TxtDocNumberProofPaper.Text.Trim())
              throw new HandledException(1, "NUMERO DE RUC NO SE ENCUENTRA VALIDADO");
          }
        }
        if (!this.chkConfirmPreview.Checked)
          throw new HandledException(1, "DEBE CONFIRMAR LOS DATOS MOSTRADOS");
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
        string str = this.txtBeneficiaryName.Text.ToUpper().Replace("À", "A").Replace("Á", "A").Replace("È", "E").Replace("É", "E").Replace("Ì", "I").Replace("Í", "I").Replace("Ó", "O").Replace("Ó", "O").Replace("Ù", "U").Replace("Ú", "U");
        return !str.Contains("ASOCIACION") || !str.Contains("AUTOMOTRIZ") || !str.Contains("PERU");
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
        int num = 11 - (int.Parse(rucAValidar.Substring(0, 1)) * 5 + int.Parse(rucAValidar.Substring(1, 1)) * 4 + int.Parse(rucAValidar.Substring(2, 1)) * 3 + int.Parse(rucAValidar.Substring(3, 1)) * 2 + int.Parse(rucAValidar.Substring(4, 1)) * 7 + int.Parse(rucAValidar.Substring(5, 1)) * 6 + int.Parse(rucAValidar.Substring(6, 1)) * 5 + int.Parse(rucAValidar.Substring(7, 1)) * 4 + int.Parse(rucAValidar.Substring(8, 1)) * 3 + int.Parse(rucAValidar.Substring(9, 1)) * 2) % 11;
        return (int.Parse(rucAValidar.Length.ToString()) != 11 ? 10 : int.Parse(rucAValidar.Substring(10, 1))) == (num != 10 ? 0 : 0) + (num != 11 ? 0 : 1) + (num >= 10 ? 0 : num);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private DataTable RequirementPlateDataTableCreate()
    {
      try
      {
        this.DtRequirements = new DataTable();
        this.DtRequirements.Columns.Add(new DataColumn("i_DetailID", typeof (int))
        {
          AllowDBNull = true,
          AutoIncrement = true,
          AutoIncrementSeed = 1L,
          AutoIncrementStep = 1L
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_DeliveryPoINTId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_RegistrationTypeId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_RegistrationOfficeId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_RegistryZoneId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_VehicleCategoryId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_VehicleTypeUseId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_VehicleClassId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("v_CompleteNameOwner", typeof (string))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_requirementPlatetypeId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_SpecialPlateTypeid", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_ProductId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_VehicleId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("v_RegistrationCode", typeof (string))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_DataBankId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_ProcessTypeId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_ContigencyTypeId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("b_ContigencyDelivery", typeof (bool))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_RegistrationUseTypeOldId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("d_RegistrationDispatchDate", typeof (DateTime))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_PlateTypeId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("b_PendingConfirmation", typeof (bool))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("b_Migrated", typeof (bool))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_StatusRequirementPlate", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_InsertUserId", typeof (int))
        {
          AllowDBNull = true
        });
        return this.DtRequirements;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void setScriptEvents()
    {
    }

    protected void btnJavaScriptResponse_Click(object sender, EventArgs e)
    {
      try
      {
        if ((int) this.Session["ClaimRegistered"] != 1)
          return;
        this.Wizard1.ActiveStepIndex = 0;
        this.pnRegistration.Visible = false;
        this.txtPlateNumber.Text = "";
        this.txtTitleNumber.Text = "";
        this.btnAdd.Enabled = true;
        this.btnCancel.Enabled = true;
        this.btnFinish.Enabled = true;
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
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp2('{0}','{1}','{2}','{3}');", (object) url, (object) pstrtitle, (object) width, (object) height);
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Script", script, true);
    }

    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        int int32_1 = Convert.ToInt32(e.CommandArgument);
        if (this.gvList.Rows[int32_1] == null)
          throw new HandledException(4, "Error de selección.", "'gvList' - RegisterMassiveRequirement.aspx");
        int int32_2 = Convert.ToInt32(this.gvList.DataKeys[int32_1]["i_VehicleId"].ToString());
        this.dtListVehicleData = this.ViewState["ListSunarpDataM"] as DataTable;
        for (int index = 0; index < this.dtListVehicleData.Rows.Count; ++index)
        {
          if (Convert.ToInt32(this.dtListVehicleData.Rows[index]["i_VehicleId"].ToString()) == int32_2)
          {
            this.dtListVehicleData.Rows.RemoveAt(index);
            break;
          }
        }
        this.ViewState["ListSunarpDataM"] = (object) this.dtListVehicleData;
        this.refreshList();
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

    protected void chkPortaMoto_CheckedChanged(object sender, EventArgs e)
    {
    }

    protected void btnPaymentVisa_Click(object sender, EventArgs e)
    {
      this.Session.Remove("VisaPagoConforme");
      RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
      try
      {
        if (this.ViewState["intRequirementID"] == null)
        {
          this.Session["iLogId"] = (object) null;
          this.FinishSpecialRequirement();
        }
        this.Session["RequirementIds"] = this.ViewState["intRequirementID"];
        DataTable byPaymentCode = requirementQueriesBl.RequirementGetByPaymentCode("", Convert.ToInt32(this.ViewState["intRequirementID"]));
        requirementQueriesBl.UpdateRequirementBoundVisa(Convert.ToInt32(this.ViewState["intRequirementID"]));
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
        double num1 = 0.0;
        double num2 = 0.0;
        double num3 = 0.0;
        for (int index = 0; index < (this.ViewState["ListSunarpDataM"] as DataTable).Rows.Count; ++index)
        {
          double num4 = Math.Round(Convert.ToDouble((this.ViewState["ListSunarpDataM"] as DataTable).Rows[index]["f_PriceCost"].ToString()) * (1.0 + this.ComisionCanalAtencion), 2);
          double num5 = Math.Round(Math.Round(Convert.ToDouble(num4) * Convert.ToDouble(this.ViewState["v_Igv"]), 3), 2);
          num3 += Convert.ToDouble((this.ViewState["ListSunarpDataM"] as DataTable).Rows[index]["f_PriceSale"].ToString());
          num2 += num4;
          num1 += num4 + num5;
        }
        this.ViewState["f_PriceTotal"] = (object) num1;
        this.ViewState["f_PriceTotalVisa"] = (object) num1;
        this.Session["PriceTotalVisa"] = (object) num1;
        double num6 = Convert.ToDouble(num1);
        double num7 = num6 - num3;
        Label lblComision = this.lblComision;
        double num8 = Math.Round(num7, 2);
        string str1 = num8.ToString();
        lblComision.Text = str1;
        Label lblTotalApagar = this.lblTotalAPagar;
        num8 = Math.Round(num6, 2);
        string str2 = num8.ToString();
        lblTotalApagar.Text = str2;
      }
    }

    protected void btnJavaScriptCloseVISA_Click(object sender, EventArgs e)
    {
      try
      {
        if (Convert.ToInt32(this.Session["VisaPagoConforme"]) != 1)
          return;
        string url = string.Format("~/Requirement/SuccessfulMassiveRegistration.aspx?id={0}", (object) Convert.ToInt32(this.ViewState["intRequirementID"]));
        this.Session["SendEmail"] = (object) 1;
        this.Response.Redirect(url, false);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(1, ex.Message));
      }
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
      this.Session.Remove("VisaPagoConforme");
      RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
      if (this.ViewState["intRequirementID"] == null)
        this.FinishSpecialRequirement();
      else
        requirementQueriesBl.UpdateRequirementBoundCash(Convert.ToInt32(this.ViewState["intRequirementID"]));
      if (this.lblMessage.Visible)
        return;
      string url = string.Format("~/Requirement/SuccessfulMassiveRegistration.aspx?id={0}", (object) Convert.ToInt32(this.ViewState["intRequirementID"]));
      this.Session["SendEmail"] = (object) 1;
      this.Response.Redirect(url, false);
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
        RegisterMassiveRequirement.DataSunat dataSunat = new RegisterMassiveRequirement.DataSunat();
        RegisterMassiveRequirement.infosunat infosunat = JsonConvert.DeserializeObject<RegisterMassiveRequirement.infosunat>(end);
        if (infosunat.Content.success)
        {
          if (infosunat.Content.content.EstadoContribuyente != "ACTIVO")
          {
            this.txtBeneficiaryName.Text = "";
            this.txtAddress.Text = "";
            this.lblMessage1.Text = "";
            this.txtBeneficiaryName.Enabled = true;
            this.ViewState["ValidationRUC"] = (object) 2;
            throw new HandledException(1, "NUMERO DE RUC NO SE ENCUENTRA ACTIVO");
          }
          this.ViewState["ValidationRUC"] = (object) 1;
          this.ViewState["RUC"] = (object) str;
          this.txtBeneficiaryName.Text = infosunat.Content.content.RazónSocial;
          this.txtBeneficiaryName.Enabled = false;
          this.txtAddress.Text = infosunat.Content.content.Direccion;
          this.Label3.Visible = false;
          Message.SetMessage(this.lblMessage1, enmMessageType.Success, SIIV.SystemParameter.BL.Constants.OPERATIONRESULT_OK + "<br> Se valido correctame el numero de RUC");
        }
        else
        {
          this.ViewState["ValidationRUC"] = (object) 2;
          this.txtBeneficiaryName.Text = "";
          this.txtAddress.Text = "";
          this.lblMessage1.Text = "";
          this.txtBeneficiaryName.Enabled = true;
          Message.SetMessage(this.Label3, new HandledException(1, infosunat.Content.msg));
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

    protected void CheckhasnotEmail_CheckedChanged(object sender, EventArgs e)
    {
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

    protected void txtRequesterPhone_TextChanged(object sender, EventArgs e)
    {
    }

    protected void BtnValidate_ClickRuc(object sender, EventArgs e)
    {
      try
      {
        string ruc = this.TxtDocNumberProofPaper.Text.Trim();
        if (ruc.Length != 11)
          Message.SetMessage(this.lblMessage1, enmMessageType.Warning, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_11);
        else if (RegisterMassiveRequirement.ValidationRUC(ruc))
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

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }

    public enum WizardNavigationTempContainer
    {
      StartNavigationTemplateContainerID = 1,
      StepNavigationTemplateContainerID = 2,
      FinishNavigationTemplateContainerID = 3,
    }

    public class infosunat
    {
      public RegisterMassiveRequirement.Content Content { get; set; }

      public bool Status { get; set; }

      public int ResultId { get; set; }

      public string Message { get; set; }
    }

    public class Content
    {
      public bool success { get; set; }

      public string msg { get; set; }

      public RegisterMassiveRequirement.content content { get; set; }
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
