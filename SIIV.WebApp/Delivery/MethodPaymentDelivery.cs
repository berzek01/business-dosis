// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Delivery.MethodPaymentDelivery
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using Newtonsoft.Json;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Delivery
{
  public class MethodPaymentDelivery : Page
  {
    private SIIV.BE.Requirement objRequirement;
    private SystemUser objUserBE;
    private RequirementContributor objRequirementContributor;
    private RequirementContributor objContributorRequester;
    private Payment objPayment;
    private RequirementManagementBL oRequirementManagement;
    private double ComisionCanalAtencion = Convert.ToDouble(ConfigurationManager.AppSettings[nameof (ComisionCanalAtencion)]);
    private double VariableIgv = Convert.ToDouble(ConfigurationManager.AppSettings["Igv"]);
    private Button btnNext;
    private Button btnPrevious;
    private Button btnPrevious2;
    private Button btnFinish;
    private Button btnCancel;
    private int RucValidation;
    private DataTable DtRequirements;
    protected UpdatePanel updatePanel;
    protected HiddenField hdiPrice;
    protected Wizard Wizard1;
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
    protected CheckBox CheckBoxDocumentElectro;
    protected CustomValidator CustomValidator4;
    protected WizardStep WS_SeleccionMedioPago;
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
    protected Button btnJavaScriptCloseVISA;
    protected HiddenField hidSunarpId;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      this.txtBeneficiaryName.Enabled = true;
      this.RucValidation = int.Parse(ConfigurationManager.AppSettings["RucValidation"]);
      if (this.RucValidation == 1 && this.CboDocumentType.SelectedValue == "4" && this.ViewState["ValidationRUC"] != null && (int) this.ViewState["ValidationRUC"] == 1)
        this.txtBeneficiaryName.Enabled = false;
      if (this.IsPostBack)
        return;
      if (this.Session["v_CashRegCode"] == null && this.Session["v_CashRegId"] == null)
        ;
      this.Initialize();
    }

    public void Initialize()
    {
      try
      {
        this.objUserBE = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterRequirement.aspx");
        this.btnPrevious = this.GetControlFromWizard(this.Wizard1, MethodPaymentDelivery.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnPrevious") as Button;
        this.btnCancel = this.GetControlFromWizard(this.Wizard1, MethodPaymentDelivery.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnCancel") as Button;
        RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
        int int32_1 = Convert.ToInt32(this.Request.QueryString["RequirementId"].ToString());
        int int32_2 = Convert.ToInt32(this.Request.QueryString["RequirementPlateIdOld"].ToString());
        string str = this.Request.QueryString["v_PlateNew"].ToString();
        this.ViewState["i_SystemUserId"] = (object) Convert.ToInt32(this.objUserBE.i_SystemUserId);
        this.ViewState["d_InsertDate"] = (object) Convert.ToDateTime((object) this.objUserBE.d_InsertDate);
        this.ViewState["arr"] = (object) (int32_1.ToString() + "|" + int32_2.ToString());
        DataTable deliveryCur = requirementQueriesBl.GenerateDeliveryCUR(Convert.ToInt32(int32_1.ToString()));
        this.ViewState["PaymentCode"] = (object) deliveryCur.Rows[0]["v_PaymentCode"].ToString();
        this.ViewState["f_PriceTotal"] = (object) deliveryCur.Rows[0]["f_PriceTotal"].ToString();
        this.ViewState["i_RequirementId"] = (object) int32_1;
        this.ViewState["v_PlateNew"] = (object) str;
        this.getDocumentType();
        this.getProofPayment();
        this.showOwnerData();
        this.ViewState["DataBank"] = (object) null;
        this.hidSunarpId.Value = "0";
        this.btnPrevious.Visible = false;
        this.btnCancel.Visible = false;
        if (this.objUserBE.i_RoleConfigId == 30)
          this.CheckhasnotEmail.Visible = false;
        else
          this.CheckhasnotEmail.Visible = true;
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

    public void showOwnerData()
    {
      try
      {
        DataTable dataTable = new DataTable();
        this.hidSunarpId.Value = new RequirementQueriesBL().SunarpDataReadChangeUse(this.ViewState["v_PlateNew"].ToString()).Rows[0][0].ToString();
        DataTable ownersByIdSunarp = new RequirementQueriesBL().GetOwnersByIdSunarp(Convert.ToInt32(this.hidSunarpId.Value));
        this.ViewState["VehicleClassId"] = (object) ownersByIdSunarp.Rows[0]["ClassId"].ToString();
        if (ownersByIdSunarp.Rows.Count > 0)
        {
          this.txtBeneficiaryName.Text = ownersByIdSunarp.Rows[0]["CompleteName"].ToString();
          this.txtBeneficiaryMail.Text = "";
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
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("<script language='javascript'>");
          stringBuilder.Append("index2='" + int32.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "';");
          stringBuilder.Append("</script>");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "JSCR", stringBuilder.ToString(), false);
        }
        ownersByIdSunarp.Dispose();
        this.ShowIconValidateDocNumRUC();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void MoveNext(object sender, EventArgs e)
    {
      try
      {
        if (this.Wizard1.ActiveStep != this.WS_SeleccionComprobante)
          return;
        this.btnPrevious = this.GetControlFromWizard(this.Wizard1, MethodPaymentDelivery.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnPrevious") as Button;
        this.btnPrevious2 = this.GetControlFromWizard(this.Wizard1, MethodPaymentDelivery.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnPrevious") as Button;
        this.ValidateProofPaymentData();
        new ValidatorRegularExpressionProofPaymentBL().ProofPaymentData(this.CboDocumentType.Text.Trim(), this.TxtDocNumberProofPaper.Text.Trim(), this.txtBeneficiaryName.Text.Trim(), this.txtAddress.Text.Trim(), this.txtBeneficiaryMail.Text.Trim());
        this.Wizard1.MoveTo((WizardStepBase) this.WS_SeleccionMedioPago);
        this.btnPrevious.Visible = true;
        this.btnPrevious2.Visible = true;
        this.lblMessage.Visible = false;
        this.rbTypePayment.SelectedIndex = 0;
        this.Session["EmailSoli"] = (object) this.txtBeneficiaryMail.Text;
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
        if (new RequirementQueriesBL().UpdateRequirementCancel(Convert.ToInt32(this.ViewState["arr"].ToString().Split('|')[0].ToString((IFormatProvider) CultureInfo.CurrentCulture))) <= 0)
          return;
        this.Response.Redirect("~/Delivery/Query/BookQueryPlates.aspx");
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
        this.btnPrevious = this.GetControlFromWizard(this.Wizard1, MethodPaymentDelivery.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnPrevious") as Button;
        this.Wizard1.MoveTo((WizardStepBase) this.WS_SeleccionComprobante);
        this.btnPrevious.Visible = false;
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

    private void SaveRequirement()
    {
      try
      {
        this.objRequirement = new SIIV.BE.Requirement();
        this.objRequirement.i_RequirementId = Convert.ToInt32(this.ViewState["i_RequirementId"]);
        this.objRequirement.i_ProofPaymentTypeId = new int?(Convert.ToInt32(this.rdbProofPayment.SelectedValue.ToString()));
        this.objRequirement.i_Status = new int?(1);
        this.objRequirement.i_InsertUserId = new int?(Convert.ToInt32(this.ViewState["i_SystemUserId"]));
        this.objRequirementContributor = new RequirementContributor();
        this.objRequirementContributor.i_DocumentTypeId = new int?(Convert.ToInt32(this.CboDocumentType.SelectedValue.ToString()));
        this.objRequirementContributor.v_DocumentNumber = this.TxtDocNumberProofPaper.Text.Trim();
        this.objRequirementContributor.v_LastName = "";
        this.objRequirementContributor.v_FirstName = "";
        this.objRequirementContributor.v_CompleteName = this.txtBeneficiaryName.Text.Trim();
        this.objRequirementContributor.v_Email = this.txtBeneficiaryMail.Text.Trim();
        this.objRequirementContributor.v_Address = this.txtAddress.Text.Trim();
        this.oRequirementManagement = new RequirementManagementBL();
        this.Session["ClientMailforVISA"] = (object) this.txtBeneficiaryMail.Text;
        this.Session["ClientTypeDocumentforVISA"] = Convert.ToInt32(this.CboDocumentType.SelectedValue) == 1 ? (object) "DNI" : (Convert.ToInt32(this.CboDocumentType.SelectedValue) == 2 ? (object) "PAS" : (Convert.ToInt32(this.CboDocumentType.SelectedValue) == 3 ? (object) "CEX" : (Convert.ToInt32(this.CboDocumentType.SelectedValue) == 4 ? (object) "RUC" : (object) "CEX")));
        this.Session["ClientNumberDocumentforVISA"] = (object) this.TxtDocNumberProofPaper.Text;
        this.Session["ClientIdforVISA"] = (object) Convert.ToInt32(this.ViewState["i_SystemUserId"]);
        this.Session["ClientTypeRegisterforVISA"] = (object) "Registrado";
        this.Session["ClientTotalDaysRegisterforVISA"] = (object) Convert.ToInt32((DateTime.Now - Convert.ToDateTime(this.ViewState["d_InsertDate"])).TotalDays);
        this.oRequirementManagement.RequirementDeliveryFinishUpdate(this.objRequirement, this.objRequirementContributor);
      }
      catch (Exception ex)
      {
        throw ex;
      }
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

    private void ValidateProofPaymentData()
    {
      try
      {
        if (this.txtBeneficiaryName.Text == "")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Ingrese_Nombre_Razon);
        if (this.CboDocumentType.DataTextField == "" || this.CboDocumentType.SelectedIndex == 0)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Seleccione_Tipo_Documento);
        if (this.TxtDocNumberProofPaper.Text.Trim() == "")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Ingrese_Documento);
        if (this.CboDocumentType.SelectedValue == "1" && this.TxtDocNumberProofPaper.Text.Trim() == "00000000")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_DNI_FORMATO_CORRECTO);
        if (this.CboDocumentType.SelectedValue == "1" && this.TxtDocNumberProofPaper.Text.Trim().Length != 8)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_DNI_8);
        if (this.CboDocumentType.SelectedValue == "2" && this.TxtDocNumberProofPaper.Text.Trim() == "00000000000")
          throw new HandledException(1, "NÚMERO DE PASAPORTE NO TIENE UN FORMATO NUMERICO CORRECTO");
        if (this.CboDocumentType.SelectedValue == "2" && this.TxtDocNumberProofPaper.Text.Trim().Length > 12)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_PASAPORTE_12);
        if (this.CboDocumentType.SelectedValue == "3" && this.TxtDocNumberProofPaper.Text.Trim() == "00000000000")
          throw new HandledException(1, "NÚMERO DE CARNET DE EXTRANJERIA NO TIENE UN FORMATO NUMERICO CORRECTO");
        if (this.CboDocumentType.SelectedValue == "3" && this.TxtDocNumberProofPaper.Text.Trim().Length > 12)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_CAEXTRANJERIA_12);
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
        if (!new Email().IsValidEmail(this.txtBeneficiaryMail.Text.Trim()))
        {
          if (!string.IsNullOrEmpty(this.txtBeneficiaryMail.Text.Trim()))
          {
            if (this.RucValidation == 1 && this.CboDocumentType.SelectedValue == "4" && this.ViewState["ValidationRUC"] != null && (int) this.ViewState["ValidationRUC"] == 1)
              this.txtBeneficiaryName.Enabled = false;
            throw new HandledException(1, "Debe Ingresar un Email Valido");
          }
        }
        else
        {
          if (!this.CheckBoxDocumentElectro.Checked)
            throw new HandledException(1, "Debe Aceptar recibir un Comprobante Electrónico al Email");
          if (!this.RestrictionRazonSocial())
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Razon_Social_Prohibido);
          if (this.CboDocumentType.SelectedValue == "4" && this.txtAddress.Text.Trim() == "")
          {
            if (this.RucValidation == 1 && this.ViewState["ValidationRUC"] != null && (int) this.ViewState["ValidationRUC"] == 1)
              this.txtBeneficiaryName.Enabled = false;
            throw new HandledException(1, "DEBE INGRESAR SU DIRECCIÓN");
          }
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

    protected void btnFinish_Click(object sender, EventArgs e)
    {
      try
      {
        this.SaveRequirement();
        if (this.rbTypePayment.SelectedIndex == 0)
        {
          RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
          string[] strArray = this.ViewState["arr"].ToString().Split('|');
          requirementQueriesBl.UpdateRequirementBoundCash(Convert.ToInt32(strArray[0].ToString((IFormatProvider) CultureInfo.CurrentCulture)));
          this.Response.Redirect("~/Delivery/Query/SuccessfulRegistrationDelivery.aspx?RequirementId=" + strArray[0].ToString(), false);
        }
        else
          this.PaymentVISA();
      }
      catch
      {
        this.HidePopup();
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void PaymentVISA()
    {
      RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
      try
      {
        this.ViewState["PaymentCode"].ToString();
        string[] strArray = this.ViewState["arr"].ToString().Split('|');
        this.Session["iLogId"] = this.Session["iLogId"] == null ? (object) (string) null : (object) this.Session["iLogId"].ToString();
        this.Session["RequirementIds"] = (object) this.ViewState["arr"].ToString();
        requirementQueriesBl.UpdateRequirementBoundVisa(Convert.ToInt32(strArray[0]));
        string empty = string.Empty;
        this.Session["RequirementPlateType"] = (object) Convert.ToInt32((object) enmQuery.Delivery);
        this.CreatePopUpServer("Pago OnLine - VISA", "../../PaymentPOS/SendEticket.aspx?PaymentCode=" + this.ViewState["PaymentCode"].ToString(), "770px", "690px");
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
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Script", script, true);
    }

    protected void btnJavaScriptCloseVISA_Click(object sender, EventArgs e)
    {
      if (Convert.ToInt32(this.Session["VisaPagoConforme"]) != 1)
        return;
      this.Response.Redirect("~/Delivery/Query/SuccessfulRegistrationDelivery.aspx?RequirementId=" + this.ViewState["arr"].ToString().Split('|')[0].ToString(), false);
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
          this.CboDocumentType.SelectedValue = "4";
        }
        proofPaymenType.Dispose();
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
        }
        documentType.Dispose();
      }
      catch (Exception ex)
      {
        throw ex;
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
        }
        else
        {
          str = "1";
          this.CboDocumentType.SelectedValue = str;
          this.CboDocumentType.Enabled = true;
          this.TxtDocNumberProofPaper.Text = "";
          this.CboDocumentType.Items.FindByValue("4").Enabled = false;
          this.lblMessage1.Text = "";
          this.txtBeneficiaryName.Text = "";
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

    protected void CboDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.TxtDocNumberProofPaper.Text = "";
    }

    protected void rbTypePament_SelectedIndexChanged(object sender, EventArgs e)
    {
      double num1 = 0.0;
      double num2 = 0.0;
      DataTable dataTable = (DataTable) this.Session["DtDetalleDelivery"];
      if (this.rbTypePayment.SelectedIndex == 0)
      {
        this.divVisa.Visible = true;
        this.divVisa2.Visible = false;
      }
      else
      {
        this.divVisa.Visible = false;
        this.divVisa2.Visible = true;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          num1 += Convert.ToDouble(row["f_PriceCost"]) * Convert.ToDouble(row["i_Quantity"]);
          num2 += Convert.ToDouble(row["f_PriceSale"]) * Convert.ToDouble(row["i_Quantity"]);
        }
        this.Session["VisaPago"] = (object) "1";
        double num3 = Math.Round(num1 * (1.0 + this.ComisionCanalAtencion), 2);
        double num4 = Math.Round(Math.Round(Convert.ToDouble(num3) * this.VariableIgv, 3), 2);
        double num5 = Convert.ToDouble(num3) + Convert.ToDouble(num4);
        double num6 = num5 - num2;
        this.Session["TotalAPagar"] = (object) num5;
        Label lblComision = this.lblComision;
        double num7 = Math.Round(num6, 2);
        string str1 = num7.ToString();
        lblComision.Text = str1;
        Label lblTotalApagar = this.lblTotalAPagar;
        num7 = Math.Round(num5, 2);
        string str2 = num7.ToString();
        lblTotalApagar.Text = str2;
        this.lblMessage.Visible = false;
      }
    }

    private Control GetControlFromWizard(
      Wizard wizard,
      MethodPaymentDelivery.WizardNavigationTempContainer wzdTemplate,
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

    protected void Wizard1_ActiveStepChanged1(object sender, EventArgs e)
    {
      try
      {
        this.btnNext = this.GetControlFromWizard(this.Wizard1, MethodPaymentDelivery.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnNext") as Button;
        this.btnPrevious = this.GetControlFromWizard(this.Wizard1, MethodPaymentDelivery.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnPrevious") as Button;
        this.btnCancel = this.GetControlFromWizard(this.Wizard1, MethodPaymentDelivery.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnCancel") as Button;
        if (this.btnPrevious != null)
          this.btnPrevious.Visible = true;
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

    private void OpenPopup()
    {
      string script = "ShowModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
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
        MethodPaymentDelivery.DataSunat dataSunat = new MethodPaymentDelivery.DataSunat();
        MethodPaymentDelivery.infosunat infosunat = JsonConvert.DeserializeObject<MethodPaymentDelivery.infosunat>(end);
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

    protected void BtnValidate_ClickRuc(object sender, EventArgs e)
    {
      try
      {
        string ruc = this.TxtDocNumberProofPaper.Text.Trim();
        if (ruc.Length != 11)
          Message.SetMessage(this.lblMessage1, enmMessageType.Warning, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_11);
        else if (MethodPaymentDelivery.ValidationRUC(ruc))
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

    public enum WizardNavigationTempContainer
    {
      StartNavigationTemplateContainerID = 1,
      StepNavigationTemplateContainerID = 2,
      FinishNavigationTemplateContainerID = 3,
    }

    public class infosunat
    {
      public MethodPaymentDelivery.Content Content { get; set; }

      public bool Status { get; set; }

      public int ResultId { get; set; }

      public string Message { get; set; }
    }

    public class Content
    {
      public bool success { get; set; }

      public string msg { get; set; }

      public MethodPaymentDelivery.content content { get; set; }
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
