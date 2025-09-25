// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Retail.RegisterRequirementOrderRetail
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

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
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Retail
{
  public class RegisterRequirementOrderRetail : Page
  {
    private SIIV.BE.Requirement objRequirement;
    private SystemUser objUserBE;
    private RequirementContributor objRequirementContributor;
    private RequirementContributor objContributorRequester;
    private Payment objPayment;
    private RequirementManagementBL oRequirementManagement;
    private double ComisionCanalAtencion = Convert.ToDouble(ConfigurationManager.AppSettings["ComisionCanalAtencionRetail"]);
    private double VariableIgv = Convert.ToDouble(ConfigurationManager.AppSettings["Igv"]);
    private Button btnNext;
    private Button btnPrevious;
    private Button btnPrevious2;
    private Button btnFinish;
    private Button btnCancel;
    private int RucValidation;
    private DataTable dtListOrder;
    private DataTable DtRequirements;
    protected UpdatePanel updatePanel;
    protected HiddenField hdiPrice;
    protected System.Web.UI.Timer Timer1;
    protected Wizard Wizard1;
    protected WizardStep WS_SeleccionComprobante;
    protected Label LblCashRegCode;
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
    protected TextBox txtEmail;
    protected CheckBox CheckhasnotEmail;
    protected DropDownList cboDeliveryPoint;
    protected WizardStep WS_SeleccionMedioPago;
    protected Label LblCashRegCode2;
    protected HtmlGenericControl divTipoPago;
    protected RadioButtonList rbTypePayment;
    protected HtmlGenericControl divVisa;
    protected Label lblTotalAPagar2;
    protected TextBox TxtPayTotal;
    protected FilteredTextBoxExtender ftbeMac;
    protected HtmlTableCell ImgLoading2;
    protected HtmlGenericControl divVisa2;
    protected Image Image7;
    protected CheckBox chkVisa;
    protected Label LblRequirement;
    protected Label lblComision;
    protected Label lblTotalAPagar;
    protected HtmlTableCell ImgLoading;
    protected Label lblMessage;
    protected Button BtnValidate;
    protected Button BtnValidateRuc;
    protected Button btnJavaScriptResponse;
    protected Button btnJavaScriptCloseVISA;
    protected HiddenField hidSunarpId;

    public void setScriptEvents()
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      this.txtBeneficiaryName.Enabled = true;
      this.RucValidation = int.Parse(ConfigurationManager.AppSettings["RucValidation"]);
      if (this.RucValidation == 1 && this.CboDocumentType.SelectedValue == "4" && this.ViewState["ValidationRUC"] != null && (int) this.ViewState["ValidationRUC"] == 1)
        this.txtBeneficiaryName.Enabled = false;
      if (this.IsPostBack)
        return;
      if (this.Session["v_CashRegCode"] != null || this.Session["v_CashRegId"] != null)
        this.LblCashRegCode.Text = this.Session["v_CashRegCode"].ToString();
      this.setScriptEvents();
      this.Initialize();
    }

    public void Initialize()
    {
      try
      {
        this.objUserBE = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterRequirement.aspx");
        this.ViewState["PublicUser"] = (object) new RequirementQueriesBL().GetSystemUserPublic(this.objUserBE.i_SystemUserId, 1);
        this.btnPrevious = this.GetControlFromWizard(this.Wizard1, RegisterRequirementOrderRetail.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnPrevious") as Button;
        this.beginTitle();
        this.getDocumentType();
        this.getProofPayment();
        this.getLocation();
        this.ViewState["DataBank"] = (object) null;
        this.hidSunarpId.Value = "0";
        this.btnPrevious.Visible = false;
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

    protected void ReturnPage(object sender, EventArgs e)
    {
      try
      {
        this.Response.Redirect("~/Retail/OrderDetail.aspx");
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
        this.btnPrevious = this.GetControlFromWizard(this.Wizard1, RegisterRequirementOrderRetail.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnPrevious") as Button;
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

    protected void MoveNext(object sender, EventArgs e)
    {
      try
      {
        if (this.Wizard1.ActiveStep == this.WS_SeleccionComprobante)
        {
          this.btnPrevious = this.GetControlFromWizard(this.Wizard1, RegisterRequirementOrderRetail.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnPrevious") as Button;
          this.btnPrevious2 = this.GetControlFromWizard(this.Wizard1, RegisterRequirementOrderRetail.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnPrevious") as Button;
          this.ValidateProofPaymentData();
          new ValidatorRegularExpressionProofPaymentBL().ProofPaymentData(this.CboDocumentType.Text.Trim(), this.TxtDocNumberProofPaper.Text.Trim(), this.txtBeneficiaryName.Text.Trim(), this.txtAddress.Text.Trim(), this.txtEmail.Text.Trim());
          this.Wizard1.MoveTo((WizardStepBase) this.WS_SeleccionMedioPago);
          this.btnPrevious.Visible = true;
          this.btnPrevious2.Visible = false;
          this.lblMessage.Visible = false;
          this.rbTypePayment.SelectedIndex = 1;
          if (this.Session["v_CashRegCode"] == null && this.Session["v_CashRegId"] == null)
            return;
          this.LblCashRegCode2.Text = this.Session["v_CashRegCode"].ToString();
        }
        else
        {
          if (this.Wizard1.ActiveStep != this.WS_SeleccionMedioPago)
            return;
          this.SaveRequirement((object) null, (EventArgs) null);
          System.Web.UI.ScriptManager.RegisterStartupScript((Page) this, typeof (Page), "alerta", "<script type='text/javascript'>\r\n                            alerta();\r\n                        </script>", false);
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

    protected void SaveRequirement(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new DataException("Sin variables de sesión");
        using (TransactionScope transactionScope = new TransactionScope())
        {
          this.DtRequirements = this.RequirementPlateDataTableCreate();
          double num1 = 0.0;
          double num2 = 0.0;
          double num3 = 0.0;
          this.dtListOrder = (DataTable) this.Session["Order"];
          this.objRequirement = new SIIV.BE.Requirement();
          this.objRequirement.i_RequirementTypeId = new int?(3);
          this.objRequirement.f_Quantity = new double?((double) this.dtListOrder.Rows.Count);
          this.objRequirement.v_Observations = "";
          this.objRequirement.i_ProofPaymentTypeId = new int?(Convert.ToInt32(this.rdbProofPayment.SelectedValue.ToString()));
          this.objRequirement.i_InsertUserId = new int?(systemUser.i_SystemUserId);
          this.objRequirement.v_Ubigeo = "";
          this.objRequirement.i_Status = new int?(-4);
          this.objRequirement.i_CashRegId = new int?(Convert.ToInt32(this.Session["i_CashRegId"].ToString()));
          this.objContributorRequester = new RequirementContributor();
          this.objContributorRequester.i_DocumentTypeId = new int?(Convert.ToInt32(this.CboDocumentType.SelectedValue.ToString()));
          this.objContributorRequester.v_DocumentNumber = this.TxtDocNumberProofPaper.Text.Trim();
          this.objContributorRequester.v_LastName = "";
          this.objContributorRequester.v_FirstName = "";
          this.objContributorRequester.v_CompleteName = this.txtBeneficiaryName.Text.Trim();
          this.objContributorRequester.v_Address = this.txtAddress.Text.Trim();
          this.objContributorRequester.v_AddressLocation = "";
          this.objContributorRequester.v_PhoneNumber = (string) null;
          this.objContributorRequester.v_Email = this.txtEmail.Text.Trim();
          this.objContributorRequester.i_PersonTypeId = new int?();
          this.objRequirementContributor = new RequirementContributor();
          this.objRequirementContributor.i_DocumentTypeId = new int?(Convert.ToInt32(this.CboDocumentType.SelectedValue.ToString()));
          this.objRequirementContributor.v_DocumentNumber = this.TxtDocNumberProofPaper.Text.Trim();
          this.objRequirementContributor.v_LastName = "";
          this.objRequirementContributor.v_FirstName = "";
          this.objRequirementContributor.v_CompleteName = this.txtBeneficiaryName.Text.Trim();
          this.objRequirementContributor.v_Email = this.txtEmail.Text.Trim();
          this.objRequirementContributor.v_Address = this.txtAddress.Text.Trim();
          this.oRequirementManagement = new RequirementManagementBL();
          foreach (DataRow row1 in (InternalDataCollectionBase) this.dtListOrder.Rows)
          {
            int num4 = 11;
            DataRow row2 = this.DtRequirements.NewRow();
            row2["i_DeliveryPoINTId"] = (object) Convert.ToInt32(this.cboDeliveryPoint.SelectedValue);
            row2["i_RegistrationTypeId"] = (object) -1;
            row2["i_RegistrationOfficeId"] = (object) -1;
            row2["i_RegistryZoneId"] = (object) -1;
            row2["i_VehicleCategoryId"] = (object) -1;
            row2["i_VehicleTypeUseId"] = (object) -1;
            row2["i_VehicleClassId"] = (object) -1;
            row2["v_CompleteNameOwner"] = (object) "";
            row2["i_requirementPlatetypeId"] = this.Session["RequirementPlateTypeRetail"];
            row2["i_SpecialPlateTypeid"] = (object) DBNull.Value;
            row2["i_ProductId"] = (object) Convert.ToInt32(row1["i_ProductId"].ToString());
            row2["i_VehicleId"] = (object) Convert.ToInt32(row1["i_Quantity"].ToString());
            row2["v_RegistrationCode"] = (object) "";
            row2["i_DataBankId"] = (object) DBNull.Value;
            row2["i_ProcessTypeId"] = (object) num4;
            row2["i_ContigencyTypeId"] = (object) DBNull.Value;
            row2["b_ContigencyDelivery"] = (object) DBNull.Value;
            row2["i_RegistrationUseTypeOldId"] = (object) DBNull.Value;
            row2["d_RegistrationDispatchDate"] = (object) DateTime.Now.ToString("yyyy-MM-dd");
            row2["i_PlateTypeId"] = (object) 1;
            row2["b_PendingConfirmation"] = (object) DBNull.Value;
            row2["b_Migrated"] = (object) DBNull.Value;
            row2["i_StatusRequirementPlate"] = (object) 0;
            row2["i_InsertUserId"] = (object) systemUser.i_SystemUserId;
            this.DtRequirements.Rows.Add(row2);
            num1 += Convert.ToDouble(row1["f_PriceCost"].ToString()) * (double) Convert.ToInt32(row1["i_Quantity"].ToString());
            num2 += Convert.ToDouble(row1["f_PriceTax"].ToString()) * (double) Convert.ToInt32(row1["i_Quantity"].ToString());
            num3 += Convert.ToDouble(row1["f_PriceSale"].ToString()) * (double) Convert.ToInt32(row1["i_Quantity"].ToString());
          }
          this.objPayment = new Payment();
          this.objPayment.f_PriceSale = new double?(Math.Round(num1, 2));
          this.objPayment.f_PriceTax = new double?(Math.Round(num1 * this.VariableIgv, 2));
          this.objPayment.f_PriceTotal = new double?(Math.Round(Convert.ToDouble((object) this.objPayment.f_PriceSale) + Convert.ToDouble((object) this.objPayment.f_PriceTax), 2));
          this.objPayment.i_Status = new int?(0);
          this.objPayment.i_PaymentTypeId = new int?(1);
          this.objPayment.i_BankId = this.rbTypePayment.SelectedIndex != 0 ? new int?(7) : new int?(8);
          this.objPayment.v_BankOperationNumber = (string) null;
          this.objPayment.v_BankOperationUser = (string) null;
          this.objPayment.v_BankOperationTerminal = (string) null;
          this.objPayment.i_AccountId = new int?();
          this.ViewState["f_PriceTotal"] = (object) this.objPayment.f_PriceTotal;
          int num5 = this.oRequirementManagement.RequirementRetailOrderInsert(this.objRequirement, this.objRequirementContributor, this.DtRequirements, this.objPayment, this.objContributorRequester);
          transactionScope.Complete();
          this.Session["Monto"] = this.ViewState["f_PriceTotal"];
          this.ViewState["intRequirementID"] = (object) num5;
        }
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
        if (!new Email().IsValidEmail(this.txtEmail.Text.Trim()))
        {
          if (!string.IsNullOrEmpty(this.txtEmail.Text.Trim()))
          {
            if (this.RucValidation == 1 && this.CboDocumentType.SelectedValue == "4" && this.ViewState["ValidationRUC"] != null && (int) this.ViewState["ValidationRUC"] == 1)
              this.txtBeneficiaryName.Enabled = false;
            throw new HandledException(1, "Debe Ingresar un Email Valido");
          }
        }
        else
        {
          if (this.cboDeliveryPoint.SelectedIndex == 0)
          {
            if (this.RucValidation == 1 && this.CboDocumentType.SelectedValue == "4" && this.ViewState["ValidationRUC"] != null && (int) this.ViewState["ValidationRUC"] == 1)
              this.txtBeneficiaryName.Enabled = false;
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Seleccione_Punto_Entrega);
          }
          if (this.CboDocumentType.SelectedValue == "4" && this.txtAddress.Text.Trim() == "")
          {
            if (this.RucValidation == 1 && this.ViewState["ValidationRUC"] != null && (int) this.ViewState["ValidationRUC"] == 1)
              this.txtBeneficiaryName.Enabled = false;
            throw new HandledException(1, "DEBE INGRESAR SU DIRECCIÓN");
          }
          if (!this.RestrictionRazonSocial())
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Razon_Social_Prohibido);
        }
        if ((this.CboDocumentType.SelectedValue == "1" || this.CboDocumentType.SelectedValue == "4") && !Regex.IsMatch(this.TxtDocNumberProofPaper.Text.Trim(), "^[0-9]+$"))
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_DNI_Numero);
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
          this.BtnValidate_ClickRuc((object) null, (EventArgs) null);
        if ((int) this.ViewState["ValidationRUC1"] != 1)
          throw new HandledException(1, "Validar que el numero de RUC sea el correcto");
        if (this.ViewState["RUC1"].ToString() != this.TxtDocNumberProofPaper.Text.Trim())
          throw new HandledException(1, "NUMERO DE RUC NO SE ENCUENTRA VALIDADO");
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
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        this.lblMessage.Visible = false;
        if (this.rbTypePayment.SelectedIndex == 0)
        {
          if (this.TxtPayTotal.Text.Trim() == "")
          {
            Message.SetMessage(this.lblMessage, new HandledException(0, "Ingrese una cantidad correcta"));
            return;
          }
          if (Convert.ToDouble(this.TxtPayTotal.Text.Trim()) == 0.0)
          {
            Message.SetMessage(this.lblMessage, new HandledException(0, "Ingrese una cantidad correcta"));
            return;
          }
        }
        bool flag = false;
        this.btnFinish = this.GetControlFromWizard(this.Wizard1, RegisterRequirementOrderRetail.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnFinish") as Button;
        this.btnPrevious = this.GetControlFromWizard(this.Wizard1, RegisterRequirementOrderRetail.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnPrevious") as Button;
        this.btnCancel = this.GetControlFromWizard(this.Wizard1, RegisterRequirementOrderRetail.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnCancel") as Button;
        this.Session["iConta"] = (object) 0;
        this.btnCancel.Enabled = false;
        this.btnPrevious.Enabled = false;
        this.btnFinish.Enabled = false;
        if (this.ViewState["intRequirementID"] == null)
        {
          this.SaveRequirement((object) null, (EventArgs) null);
          Convert.ToInt32(this.ViewState["intRequirementID"]);
          if (this.rbTypePayment.SelectedIndex == 0)
          {
            flag = new RequirementManagementBL().EBillingStatusProcess(Convert.ToInt32(this.ViewState["intRequirementID"]), 99, Convert.ToDouble(this.TxtPayTotal.Text.Trim()));
            new RequirementManagementBL().EBillingCashProcess(Convert.ToInt32(this.ViewState["intRequirementID"]), 1, systemUser.i_SystemUserId);
          }
        }
        else
          flag = this.rbTypePayment.SelectedIndex != 0 ? new RequirementManagementBL().EBillingStatusProcess(Convert.ToInt32(this.ViewState["intRequirementID"]), 0, Convert.ToDouble(this.TxtPayTotal.Text.Trim())) : new RequirementManagementBL().EBillingStatusProcess(Convert.ToInt32(this.ViewState["intRequirementID"]), 99, Convert.ToDouble(this.TxtPayTotal.Text.Trim()));
        if (this.rbTypePayment.SelectedIndex == 0)
          this.ImgLoading2.Visible = true;
        else
          this.ImgLoading.Visible = true;
        this.rbTypePayment.Enabled = false;
        this.OpenURI();
        this.Timer1.Enabled = true;
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

    protected void Timer1_Tick(object sender, EventArgs e)
    {
      this.Session["iConta"] = (object) (Convert.ToInt32(this.Session["iConta"]) + 1);
      DataTable actionProceesEbilling = new RequirementQueriesBL().GetActionProceesEBilling(Convert.ToInt32(this.ViewState["intRequirementID"]));
      try
      {
        if (actionProceesEbilling.Rows[0][0].ToString() == ConfigurationManager.AppSettings["EBillingPayStatus"].ToString() && actionProceesEbilling.Rows[0][1].ToString() == "2")
        {
          this.Timer1.Enabled = false;
          this.HidePopup();
          this.Session["Tipo"] = (object) "1";
          this.Response.Redirect(string.Format("~/Retail/SuccessfulRegistration.aspx?id={0}", (object) Convert.ToInt32(this.ViewState["intRequirementID"])), false);
        }
        if (actionProceesEbilling.Rows[0][1].ToString() != "0" && actionProceesEbilling.Rows[0][1].ToString() != "2")
        {
          this.btnFinish = this.GetControlFromWizard(this.Wizard1, RegisterRequirementOrderRetail.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnFinish") as Button;
          this.btnPrevious = this.GetControlFromWizard(this.Wizard1, RegisterRequirementOrderRetail.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnPrevious") as Button;
          this.btnCancel = this.GetControlFromWizard(this.Wizard1, RegisterRequirementOrderRetail.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnCancel") as Button;
          this.btnCancel.Enabled = true;
          this.btnPrevious.Enabled = true;
          this.btnFinish.Enabled = true;
          this.ImgLoading.Visible = false;
          this.ImgLoading2.Visible = false;
          this.rbTypePayment.Enabled = true;
          this.Timer1.Enabled = false;
          this.HidePopup();
          Message.SetMessage(this.lblMessage, new HandledException(0, actionProceesEbilling.Rows[0][2].ToString()));
        }
        if (Convert.ToInt32(this.Session["iConta"]) != Convert.ToInt32(ConfigurationManager.AppSettings["EBillingWaitTime"]))
          return;
        this.btnFinish = this.GetControlFromWizard(this.Wizard1, RegisterRequirementOrderRetail.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnFinish") as Button;
        this.btnPrevious = this.GetControlFromWizard(this.Wizard1, RegisterRequirementOrderRetail.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnPrevious") as Button;
        this.btnCancel = this.GetControlFromWizard(this.Wizard1, RegisterRequirementOrderRetail.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnCancel") as Button;
        this.btnCancel.Enabled = true;
        this.btnPrevious.Enabled = true;
        this.btnFinish.Enabled = true;
        this.ImgLoading.Visible = false;
        this.ImgLoading2.Visible = false;
        this.rbTypePayment.Enabled = true;
        this.HidePopup();
        Message.SetMessage(this.lblMessage, new HandledException(0, "Error en Conexión"));
        this.Timer1.Enabled = false;
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

    private void OpenURI()
    {
      string script = "EjecutarURI(" + this.ViewState["intRequirementID"]?.ToString() + ");";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void btnJavaScriptCloseVISA_Click(object sender, EventArgs e)
    {
      if (Convert.ToInt32(this.Session["VisaPagoConforme"]) != 1)
        return;
      string[] strArray = this.ViewState["ids"].ToString().Split('|');
      this.Response.Redirect("~/Requirement/SuccessfulRegistration.aspx?RequirementId=" + strArray[0].ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&RequirementPlateId=" + strArray[1].ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&t=" + strArray[2].ToString() + "&mre=0", false);
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

    public void getLocation()
    {
      try
      {
        DataTable locationRequirement = new RequirementQueriesBL().GetLocationRequirement(this.objUserBE.i_SystemUserId);
        if (locationRequirement.Rows.Count > 0)
        {
          this.cboDeliveryPoint.DataSource = (object) locationRequirement;
          this.cboDeliveryPoint.DataTextField = "v_Description";
          this.cboDeliveryPoint.DataValueField = "i_LocationId";
          this.cboDeliveryPoint.DataBind();
          this.cboDeliveryPoint.SelectedValue = this.objUserBE.i_LocationId.ToString();
        }
        locationRequirement.Dispose();
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

    public void beginTitle()
    {
      try
      {
        string str = "";
        if ((int) this.Session["RequirementPlateTypeRetail"] == Convert.ToInt32((object) enmRequirementPlateType.Retail))
          str = "Compras Retail";
        this.Page.Title = str;
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

    protected void rbTypePament_SelectedIndexChanged(object sender, EventArgs e)
    {
      double num1 = 0.0;
      double num2 = 0.0;
      double num3 = 0.0;
      DataTable dataTable = (DataTable) this.Session["Order"];
      if (this.rbTypePayment.SelectedIndex == 0)
      {
        this.divVisa.Visible = true;
        this.divVisa2.Visible = false;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          num2 += Convert.ToDouble(row["f_PriceCost"]) * Convert.ToDouble(row["i_Quantity"]);
        this.Session["VisaPago"] = (object) "1";
        double num4 = Math.Round(Math.Round(num2, 2) * this.VariableIgv, 2);
        double num5 = Math.Round(num2, 2) + Math.Round(num4, 2);
        this.Session["MontoTotal"] = (object) num5;
        this.lblTotalAPagar2.Text = Math.Round(num5, 2).ToString();
        this.lblMessage.Visible = false;
      }
      else
      {
        this.divVisa.Visible = false;
        this.divVisa2.Visible = true;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          num1 += Convert.ToDouble(row["f_PriceCost"]) * Convert.ToDouble(row["i_Quantity"]);
          num3 += Convert.ToDouble(row["f_PriceSale"]) * Convert.ToDouble(row["i_Quantity"]);
        }
        this.Session["VisaPago"] = (object) "1";
        double num6 = Math.Round(num1 * (1.0 + this.ComisionCanalAtencion), 2);
        double num7 = Math.Round(Math.Round(Convert.ToDouble(num6) * this.VariableIgv, 3), 2);
        double num8 = Convert.ToDouble(num6) + Convert.ToDouble(num7);
        double num9 = num8 - num3;
        this.Session["MontoTotal"] = (object) num8;
        this.lblComision.Text = Math.Round(num9, 2).ToString();
        this.lblTotalAPagar.Text = Math.Round(num8, 2).ToString();
        this.lblMessage.Visible = false;
      }
    }

    private Control GetControlFromWizard(
      Wizard wizard,
      RegisterRequirementOrderRetail.WizardNavigationTempContainer wzdTemplate,
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
        this.btnNext = this.GetControlFromWizard(this.Wizard1, RegisterRequirementOrderRetail.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnNext") as Button;
        this.btnPrevious = this.GetControlFromWizard(this.Wizard1, RegisterRequirementOrderRetail.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnPrevious") as Button;
        this.btnCancel = this.GetControlFromWizard(this.Wizard1, RegisterRequirementOrderRetail.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnCancel") as Button;
        if (this.btnPrevious != null)
          this.btnPrevious.Visible = true;
        int num = this.Session["ProcessIdRetail"] != null ? (int) Convert.ToInt16(this.Session["ProcessIdRetail"].ToString()) : throw new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
        if (this.Wizard1.ActiveStep != this.WS_SeleccionMedioPago)
          return;
        this.rbTypePayment.SelectedIndex = 1;
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
        RegisterRequirementOrderRetail.DataSunat dataSunat = new RegisterRequirementOrderRetail.DataSunat();
        RegisterRequirementOrderRetail.infosunat infosunat = JsonConvert.DeserializeObject<RegisterRequirementOrderRetail.infosunat>(end);
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

    protected void CheckhasnotEmail_CheckedChanged(object sender, EventArgs e)
    {
      if (this.RucValidation == 1 && this.CboDocumentType.SelectedValue == "4" && this.ViewState["ValidationRUC"] != null && (int) this.ViewState["ValidationRUC"] == 1)
        this.txtBeneficiaryName.Enabled = false;
      if (this.CheckhasnotEmail.Checked)
      {
        this.txtEmail.ReadOnly = this.CheckhasnotEmail.Checked;
        this.txtEmail.Enabled = !this.CheckhasnotEmail.Checked;
        this.txtEmail.Text = ConfigurationManager.AppSettings["EmailDefault"].ToString();
      }
      else
      {
        this.txtEmail.ReadOnly = this.CheckhasnotEmail.Checked;
        this.txtEmail.Enabled = !this.CheckhasnotEmail.Checked;
        this.txtEmail.Text = string.Empty;
      }
    }

    protected void BtnValidate_ClickRuc(object sender, EventArgs e)
    {
      try
      {
        string ruc = this.TxtDocNumberProofPaper.Text.Trim();
        if (ruc.Length != 11)
          Message.SetMessage(this.lblMessage1, enmMessageType.Warning, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_11);
        else if (RegisterRequirementOrderRetail.ValidationRUC(ruc))
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

    public enum WizardNavigationTempContainer
    {
      StartNavigationTemplateContainerID = 1,
      StepNavigationTemplateContainerID = 2,
      FinishNavigationTemplateContainerID = 3,
    }

    public class infosunat
    {
      public RegisterRequirementOrderRetail.Content Content { get; set; }

      public bool Status { get; set; }

      public int ResultId { get; set; }

      public string Message { get; set; }
    }

    public class Content
    {
      public bool success { get; set; }

      public string msg { get; set; }

      public RegisterRequirementOrderRetail.content content { get; set; }
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
