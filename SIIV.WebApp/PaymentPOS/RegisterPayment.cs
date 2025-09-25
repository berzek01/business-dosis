// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.PaymentPOS.RegisterPayment
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.PaymentPOS
{
  public class RegisterPayment : Page
  {
    private MaintenanceOperation currentOperation = MaintenanceOperation.None;
    private DataTable dtRequirementData;
    private DataTable dtSunarp;
    private DataTable dtPayment;
    private double ComisionCanalAtencion = Convert.ToDouble(ConfigurationManager.AppSettings["ComisionCanalAtencionRetail"]);
    private double VariableIgv = Convert.ToDouble(ConfigurationManager.AppSettings["Igv"]);
    private Button btnNext;
    private Button btnPrevious;
    private Button btnFinish;
    private Button btnCancel;
    protected UpdatePanel UpdatePanel;
    protected Panel Panel3;
    protected TextBox TxtSearch;
    protected ImageButton BtnSearch;
    protected Label lblMessage;
    protected Panel Panel1;
    protected Label lblSubTotal;
    protected Button wibPay;
    protected GridView wdgList;
    protected Panel Panel2;
    protected Wizard Wizard1;
    protected WizardStep WS_SeleccionComprobante;
    protected Label LblCashRegCode2;
    protected RadioButtonList rdbProofPayment;
    protected HtmlTableCell TagDatosComprobante;
    protected DropDownList CboDocumentType;
    protected TextBox TxtDocNumberProofPaper;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender2;
    protected TextBox txtBeneficiaryName;
    protected FilteredTextBoxExtender txtBeneficiaryName_FilteredTextBoxExtender;
    protected TextBox txtAddress;
    protected FilteredTextBoxExtender txtAddress_FilteredTextBoxExtender;
    protected DropDownList cboDeliveryPoint;
    protected WizardStep WS_SeleccionMedioPago;
    protected HtmlGenericControl divTipoPago;
    protected RadioButtonList rbTypePayment;
    protected HtmlGenericControl divVisa;
    protected Label lblTotalAPagar2;
    protected TextBox TxtPayTotal;
    protected FilteredTextBoxExtender ftbeMac;
    protected Image ImgLoading2;
    protected HtmlGenericControl divVisa2;
    protected Image Image7;
    protected CheckBox chkVisa;
    protected Label LblRequirement;
    protected Label lblComision;
    protected Label lblTotalAPagar;
    protected Image ImgLoading;
    protected Label lblMessage2;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.IsPostBack)
        ;
    }

    protected void BtnSearch_Click(object sender, ImageClickEventArgs e)
    {
      try
      {
        if (this.TxtSearch.Text.Trim() == "")
        {
          Message.SetMessage(this.lblMessage, new HandledException(1, "Ingrese un Código de Pago"));
          this.HidePopup();
        }
        else
        {
          this.Panel1.Visible = false;
          this.Panel2.Visible = false;
          this.lblMessage.Visible = false;
          this.Session["ListRequirements"] = (object) new RequirementQueriesBL().GetRequirementDatabyPaymentCode(this.TxtSearch.Text.Trim());
          if ((this.Session["ListRequirements"] as DataTable).Rows.Count < 1)
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información para el Código de Pago consultado."));
          }
          else
          {
            this.Session["Monto"] = (object) (this.Session["ListRequirements"] as DataTable).Rows[0]["f_PriceTotal"].ToString();
            this.ViewState["RequirementMonto"] = (object) (this.Session["ListRequirements"] as DataTable).Rows[0]["f_PriceRequi"].ToString();
            this.lblSubTotal.Text = this.Session["Monto"].ToString();
            if (Convert.ToInt32((this.Session["ListRequirements"] as DataTable).Rows[0]["i_BankId"].ToString()) != 0)
              Message.SetMessage(this.lblMessage, new HandledException(1, "Este Código de Pago ya fue Cancelado"));
            if (!this.lblMessage.Visible)
            {
              this.Panel1.Visible = true;
              this.Panel2.Visible = false;
              this.SearchUniversal();
            }
          }
          this.HidePopup();
        }
      }
      catch
      {
        this.HidePopup();
        Message.SetMessage(this.lblMessage, new HandledException(-100, "Error en Conexión"));
      }
    }

    private void SearchUniversal()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
        this.SearchUniversalList("", 0, 0, "", "", -1, "", 0, 0, -3, "", this.TxtSearch.Text.Trim(), systemUser.i_SystemUserId, 1, 1, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchUniversalList(
      string pstrPlateNew,
      int pintStartdate,
      int pintFinishdate,
      string pstrPlateOld,
      string pstrTitleNumber,
      int pintRequirementPlateId,
      string pstrOwnerName,
      int pintCategoryId,
      int pintProcessTypeId,
      int pintStatus,
      string pstrSerial,
      string pstrPaymentCode,
      int pintUserId,
      int pintQueryType,
      int pintiplateTypeId,
      bool pboolLoadPager)
    {
      try
      {
        int startRowIndex = 1;
        int maxRows = 100;
        DataTable dataTable = new RequirementQueriesBL().UniversalQueryRead(pstrPlateNew, pintStartdate, pintFinishdate, pstrPlateOld, pstrTitleNumber, pintRequirementPlateId, pstrOwnerName, pintCategoryId, pintProcessTypeId, pintStatus, pstrSerial, pstrPaymentCode, pintUserId, pintQueryType, pintiplateTypeId, startRowIndex, maxRows, out int _);
        if (dataTable == null || dataTable.Rows.Count == 0)
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información para el Código de Pago consultado."));
        else
          this.Session["UniversalList"] = (object) dataTable;
        this.wdgList.DataSource = (object) dataTable;
        this.wdgList.DataBind();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void CreatePopUp(string url, string pstrtitle, string width, string height)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) url, (object) pstrtitle, (object) (width + "px"), (object) (height + "px"));
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel, this.UpdatePanel.GetType(), "Script", script, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel, this.UpdatePanel.GetType(), "Script", script, true);
    }

    protected void ReturnPage(object sender, EventArgs e)
    {
      try
      {
        this.Panel1.Visible = false;
        this.Panel2.Visible = false;
        this.Panel3.Visible = true;
        this.TxtSearch.Text = "";
        this.TxtSearch.Focus();
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
        this.btnPrevious = this.GetControlFromWizard(this.Wizard1, RegisterPayment.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnPrevious") as Button;
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
        if (this.Wizard1.ActiveStep != this.WS_SeleccionComprobante)
          return;
        this.btnPrevious = this.GetControlFromWizard(this.Wizard1, RegisterPayment.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnPrevious") as Button;
        this.ValidateProofPaymentData();
        this.Wizard1.MoveTo((WizardStepBase) this.WS_SeleccionMedioPago);
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

    protected void btnFinish_Click(object sender, EventArgs e)
    {
      try
      {
        this.lblMessage2.Visible = false;
        bool flag = false;
        if (this.rbTypePayment.SelectedIndex == 0)
        {
          if (this.TxtPayTotal.Text.Trim() == "")
          {
            Message.SetMessage(this.lblMessage2, new HandledException(0, "Ingrese una cantidad correcta"));
            return;
          }
          if (Convert.ToDouble(this.TxtPayTotal.Text.Trim()) == 0.0)
          {
            Message.SetMessage(this.lblMessage2, new HandledException(0, "Ingrese una cantidad correcta"));
            return;
          }
        }
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        this.btnFinish = this.GetControlFromWizard(this.Wizard1, RegisterPayment.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnFinish") as Button;
        this.btnPrevious = this.GetControlFromWizard(this.Wizard1, RegisterPayment.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnPrevious") as Button;
        this.btnCancel = this.GetControlFromWizard(this.Wizard1, RegisterPayment.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnCancel") as Button;
        this.btnFinish.Enabled = false;
        this.btnCancel.Enabled = false;
        this.ViewState["intRequirementID"] = (object) (this.Session["ListRequirements"] as DataTable).Rows[0]["i_RequirementId"].ToString();
        if (this.rbTypePayment.SelectedIndex == 0)
          flag = new RequirementManagementBL().EBillingCashProcess(Convert.ToInt32(this.ViewState["intRequirementID"]), 1);
        if (!flag)
          return;
        this.Response.Redirect(string.Format("~/PaymentPOS/SuccessfulRegistration.aspx?id={0}", (object) Convert.ToInt32((this.Session["ListRequirements"] as DataTable).Rows[0]["i_RequirementId"].ToString())), false);
      }
      catch (Exception ex)
      {
        this.HidePopup();
        Message.SetMessage(this.lblMessage2, new HandledException(-100, ex));
        this.ImgLoading2.Visible = false;
        this.ImgLoading.Visible = false;
        this.btnFinish.Enabled = true;
        this.btnCancel.Enabled = true;
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void SaveRequirement(object sender, EventArgs e)
    {
      try
      {
        bool flag = false;
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new DataException("Sin variables de sesión");
        using (TransactionScope transactionScope = new TransactionScope())
        {
          int int32 = Convert.ToInt32((this.Session["ListRequirements"] as DataTable).Rows[0]["i_RequirementId"].ToString());
          this.ViewState["intRequirementID"] = (object) int32;
          flag = new RequirementManagementBL().RegisterPaymentCode(int32, Convert.ToInt32((object) systemUser.i_InsertUserId));
          transactionScope.Complete();
        }
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
        if (this.CboDocumentType.SelectedValue == "1" && this.TxtDocNumberProofPaper.Text.Trim().Length != 8)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_DNI_8);
        if (this.CboDocumentType.SelectedValue == "3" && this.TxtDocNumberProofPaper.Text.Trim().Length != 12)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_DOC_12);
        if (this.CboDocumentType.SelectedValue == "4" && this.TxtDocNumberProofPaper.Text.Trim().Length != 11)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_11);
        if (this.CboDocumentType.SelectedValue == "4" && this.TxtDocNumberProofPaper.Text.Trim().Substring(0, 1) != "1" && this.TxtDocNumberProofPaper.Text.Trim().Substring(0, 1) != "2")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_Longitud);
        if (this.CboDocumentType.SelectedValue == "4" && !this.ValidateRuc(this.TxtDocNumberProofPaper.Text.Trim()))
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_Formato);
        if (this.CboDocumentType.SelectedValue == "19" && this.TxtDocNumberProofPaper.Text.Trim().Length != 12)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_DOC_12);
        if (this.cboDeliveryPoint.SelectedIndex == 0)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Seleccione_Punto_Entrega);
        if (!this.RestrictionRazonSocial())
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Razon_Social_Prohibido);
        if ((this.CboDocumentType.SelectedValue == "1" || this.CboDocumentType.SelectedValue == "4") && !Regex.IsMatch(this.TxtDocNumberProofPaper.Text.Trim(), "^[0-9]+$"))
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_DNI_Numero);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private bool RestrictionRazonSocial()
    {
      string str = this.txtBeneficiaryName.Text.ToUpper(CultureInfo.CurrentCulture).Replace("À", "A").Replace("Á", "A").Replace("È", "E").Replace("É", "E").Replace("Ì", "I").Replace("Í", "I").Replace("Ó", "O").Replace("Ó", "O").Replace("Ù", "U").Replace("Ú", "U");
      return !str.Contains("ASOCIACION") || !str.Contains("AUTOMOTRIZ") || !str.Contains("PERU");
    }

    private bool ValidateRuc(string rucAValidar)
    {
      int num = 11 - (int.Parse(rucAValidar.Substring(0, 1)) * 5 + int.Parse(rucAValidar.Substring(1, 1)) * 4 + int.Parse(rucAValidar.Substring(2, 1)) * 3 + int.Parse(rucAValidar.Substring(3, 1)) * 2 + int.Parse(rucAValidar.Substring(4, 1)) * 7 + int.Parse(rucAValidar.Substring(5, 1)) * 6 + int.Parse(rucAValidar.Substring(6, 1)) * 5 + int.Parse(rucAValidar.Substring(7, 1)) * 4 + int.Parse(rucAValidar.Substring(8, 1)) * 3 + int.Parse(rucAValidar.Substring(9, 1)) * 2) % 11;
      return (int.Parse(rucAValidar.Length.ToString((IFormatProvider) CultureInfo.CurrentCulture)) != 11 ? 10 : int.Parse(rucAValidar.Substring(10, 1))) == (num != 10 ? 0 : 0) + (num != 11 ? 0 : 1) + (num >= 10 ? 0 : num);
    }

    private Control GetControlFromWizard(
      Wizard wizard,
      RegisterPayment.WizardNavigationTempContainer wzdTemplate,
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
        this.btnNext = this.GetControlFromWizard(this.Wizard1, RegisterPayment.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnNext") as Button;
        this.btnPrevious = this.GetControlFromWizard(this.Wizard1, RegisterPayment.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnPrevious") as Button;
        this.btnCancel = this.GetControlFromWizard(this.Wizard1, RegisterPayment.WizardNavigationTempContainer.StepNavigationTemplateContainerID, "btnCancel") as Button;
        if (this.btnPrevious != null)
          this.btnPrevious.Visible = true;
        if (this.Wizard1.ActiveStep != this.WS_SeleccionMedioPago)
          ;
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
      if (this.rbTypePayment.SelectedValue == "0")
      {
        this.divVisa.Visible = true;
        this.divVisa2.Visible = false;
        double num = Convert.ToDouble(this.Session["Monto"]);
        this.Session["MontoTotal"] = (object) num;
        this.lblTotalAPagar2.Text = num.ToString();
        this.TxtPayTotal.Text = num.ToString();
        this.lblMessage2.Visible = false;
      }
      else
      {
        this.divVisa.Visible = false;
        this.divVisa2.Visible = true;
        double num1 = Convert.ToDouble(this.ViewState["RequirementMonto"]);
        double num2 = num1 - Convert.ToDouble(this.Session["Monto"]);
        this.Session["MontoTotal"] = (object) num1;
        this.lblComision.Text = Math.Round(num2, 2).ToString();
        this.lblTotalAPagar.Text = Math.Round(num1, 2).ToString();
        this.lblMessage2.Visible = false;
      }
    }

    protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
    }

    protected void wibPay_Click(object sender, EventArgs e)
    {
      this.rbTypePament_SelectedIndexChanged((object) null, (EventArgs) null);
      this.btnPrevious = this.GetControlFromWizard(this.Wizard1, RegisterPayment.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnPrevious") as Button;
      this.btnPrevious.Visible = false;
      this.HidePopup();
      this.lblMessage2.Visible = false;
      this.Panel1.Visible = false;
      this.Panel2.Visible = true;
      this.Panel3.Visible = false;
      if (this.Session["v_CashRegCode"] == null && this.Session["i_CashRegId"] == null)
        return;
      this.LblCashRegCode2.Text = this.Session["v_CashRegCode"].ToString();
    }

    public enum WizardNavigationTempContainer
    {
      StartNavigationTemplateContainerID = 1,
      StepNavigationTemplateContainerID = 2,
      FinishNavigationTemplateContainerID = 3,
    }
  }
}
