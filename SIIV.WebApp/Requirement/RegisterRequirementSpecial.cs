// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Requirement.RegisterRequirementSpecial
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Claim.BL;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using SIIV.WebApp.Claim;
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
  public class RegisterRequirementSpecial : Page
  {
    private SIIV.BE.Requirement objRequirement;
    private RequirementContributor objRequirementContributor;
    private VehicleRegistrationDetail objVehicleRegistrationDetail;
    private RequirementPlate objRequirementPlate;
    private RequirementContributor objContributorRequester;
    private Payment objPayment;
    private RequirementProgramation objRequirementProgramation;
    private SpecialRequirement objSpecialRequirement;
    private RequirementQueriesBL oRequirementQueriesBL;
    private RequirementManagementBL oRequirementManagement;
    private DataTable dtRelatedDocument;
    private DataTable dtVehicleData;
    private DataTable dtOwners;
    private DataTable dtListVehicleData;
    private DataTable dtListOwners;
    private DataTable dtIDS;
    private DataTable DtProduct;
    private DataTable dtClaim;
    private DataRow drVehicleData;
    private DataRow drOwners;
    private static DataColumn[] colVideos = new DataColumn[4];
    private Button btnFinishTemplate;
    private Button btnPreviousTemplate;
    private Button btnClaim;
    protected UpdatePanel updatePanel;
    protected HtmlTable tbTable;
    protected GridView gvList;
    protected HtmlTableCell TagResumen;
    protected Button btnAdd;
    protected Button btnFinish;
    protected Button btnCancel;
    protected Panel pnRegistration;
    protected Wizard Wizard1;
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
    protected TextBox txtOwner3rd;
    protected CheckBox chkAccept3rd;
    protected CustomValidator CustomValidator1;
    protected Label lblData2Message2;
    protected WizardStep WS_SeleccionComprobante;
    protected RadioButtonList rdbProofPayment;
    protected HtmlTableCell TagDatosComprobante;
    protected TextBox txtBeneficiaryName;
    protected FilteredTextBoxExtender txtBeneficiaryName_FilteredTextBoxExtender;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected ValidatorCalloutExtender RequiredFieldValidator1_ValidatorCalloutExtender;
    protected DropDownList CboDocumentType;
    protected TextBox TxtDocNumberProofPaper;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender2;
    protected TextBox txtAddress;
    protected FilteredTextBoxExtender txtAddress_FilteredTextBoxExtender;
    protected TextBox txtBeneficiaryMail;
    protected RequiredFieldValidator RequiredFieldValidator33;
    protected FilteredTextBoxExtender FilteredTextBoxExtender3;
    protected RegularExpressionValidator RegularExpressionValidator3;
    protected ValidatorCalloutExtender ValidatorCalloutExtender1;
    protected ValidatorCalloutExtender ValidatorCalloutExtender33;
    protected DropDownList cboDeliveryPoint;
    protected TextBox txtObservation;
    protected HtmlTableCell Td1;
    protected CheckBox chkConfirmPreview;
    protected Label lblMessage;
    protected Button btnJavaScriptResponse;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (!this.Page.IsPostBack)
      {
        try
        {
          this.Wizard1.ActiveStepIndex = 0;
          this.BeginList();
          this.Session["ListSunarpData"] = (object) null;
          this.BeginTitle();
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
      this.refreshList();
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

    protected void ReturnAdd(object sender, EventArgs e)
    {
      this.Wizard1.ActiveStepIndex = 0;
      this.pnRegistration.Visible = false;
      this.txtBeneficiaryName.Text = "";
      this.txtAddress.Text = "";
      this.TxtDocNumberProofPaper.Text = "";
      this.TxtDocNumberProofPaper.Text = "";
      this.cboDeliveryPoint.SelectedIndex = 0;
      this.txtObservation.Text = "";
      this.btnAdd.Enabled = true;
      this.btnCancel.Enabled = true;
      this.btnFinish.Enabled = true;
    }

    protected void MoveNext(object sender, EventArgs e)
    {
      try
      {
        if (this.Wizard1.ActiveStep == this.WS_ValidacionOrden)
        {
          this.ValidateSunarp();
        }
        else
        {
          if (this.Wizard1.ActiveStep != this.WS_ValidacionPlaca)
            return;
          this.ValidatePlate();
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
      try
      {
        this.dtRelatedDocument = new RequirementQueriesBL().GetRelatedDocumentbyId(Convert.ToInt32(this.Request.QueryString["idDoc"], (IFormatProvider) CultureInfo.CurrentCulture));
        if (Convert.ToInt32(this.dtRelatedDocument.Rows[0]["i_MaximumQuantity"], (IFormatProvider) CultureInfo.CurrentCulture) - Convert.ToInt32(this.dtRelatedDocument.Rows[0]["i_ActualQuantity"], (IFormatProvider) CultureInfo.CurrentCulture) == this.gvList.Rows.Count)
          throw new HandledException(1, "YA SE HA SUPERADO EL NRO DE MATRICULAS DEL OFICIO");
        if (Convert.ToString(this.Request.QueryString["Prot"], (IFormatProvider) CultureInfo.CurrentCulture) == "I")
        {
          this.chkAccept.Checked = false;
          this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_ValidacionOrden);
        }
        else
        {
          this.chkAccept3rd.Checked = false;
          this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_ValidacionPlaca);
        }
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
        if (this.Wizard1.ActiveStepIndex == this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_DatosVehiculo1) || this.Wizard1.ActiveStepIndex == this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_DatosVehiculo2))
          this.AddRequirement();
        else
          this.FinishSpecialRequirement();
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

    protected void registerClaim(object sender, EventArgs e)
    {
      try
      {
        string pstrPlate = "";
        if (this.Wizard1.ActiveStep == this.WS_ValidacionOrden || this.Wizard1.ActiveStep == this.WS_DatosVehiculo1)
        {
          pstrPlate = this.txtPlateNumber.Text.Trim().Replace("-", "");
          if (pstrPlate == "")
            throw new HandledException(1, "DEBE INGRESAR UN NUMERO DE PLACA");
        }
        else if (this.Wizard1.ActiveStep == this.WS_ValidacionPlaca || this.Wizard1.ActiveStep == this.WS_DatosVehiculo2)
        {
          pstrPlate = this.txtPlateNumber1.Text.Trim().Replace("-", "");
          if (pstrPlate == "")
            throw new HandledException(1, "DEBE INGRESAR UN NUMERO DE PLACA");
        }
        this.dtClaim = new RequirementClaimQueriesBL().RequirementClaimGetActiveByPlate(pstrPlate);
        if (this.Wizard1.ActiveStepIndex == this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_ValidacionOrden) || this.Wizard1.ActiveStepIndex == this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_ValidacionPlaca))
        {
          if (this.dtClaim.Rows.Count > 0)
          {
            string str = this.dtClaim.Rows[0]["v_ClaimCode"].ToString();
            if (this.Wizard1.ActiveStep == this.WS_ValidacionOrden)
            {
              Message.SetMessage(this.lblMessage, new HandledException(1, "EL NUMERO DE PLACA TIENE UN RECLAMO EN CURSO</BR>EL NUMERO DE RECLAMO ES " + str));
            }
            else
            {
              if (this.Wizard1.ActiveStep != this.WS_ValidacionPlaca)
                return;
              Message.SetMessage(this.lblMessage, new HandledException(1, "EL NUMERO DE PLACA TIENE UN RECLAMO EN CURSO</BR>EL NUMERO DE RECLAMO ES " + str));
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
              Message.SetMessage(this.lblMessage, new HandledException(1, "EL NUMERO DE PLACA TIENE UN RECLAMO EN CURSO</BR>EL NUMERO DE RECLAMO ES " + str));
            else if (this.Wizard1.ActiveStep == this.WS_DatosVehiculo2)
              Message.SetMessage(this.lblMessage, new HandledException(1, "EL NUMERO DE PLACA TIENE UN RECLAMO EN CURSO</BR>EL NUMERO DE RECLAMO ES " + str));
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

    protected void btnFinish_Click(object sender, EventArgs e)
    {
      try
      {
        this.pnRegistration.Visible = true;
        this.btnAdd.Enabled = false;
        this.btnCancel.Enabled = false;
        this.btnFinish.Enabled = false;
        this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_SeleccionComprobante);
        this.getProofPayment();
        this.getDocumentType();
        this.getLocation();
        this.showOwnerData();
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("SpecialRequirement.aspx");
    }

    protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
    {
      if (this.Wizard1.ActiveStep == this.WS_SeleccionComprobante)
      {
        this.btnClaim = this.GetControlFromWizard(this.Wizard1, RegisterRequirementSpecial.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnClaim") as Button;
        this.btnFinishTemplate = this.GetControlFromWizard(this.Wizard1, RegisterRequirementSpecial.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnFinish") as Button;
        this.btnPreviousTemplate = this.GetControlFromWizard(this.Wizard1, RegisterRequirementSpecial.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnPrevious") as Button;
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
        this.btnClaim = this.GetControlFromWizard(this.Wizard1, RegisterRequirementSpecial.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnClaim") as Button;
        this.btnFinishTemplate = this.GetControlFromWizard(this.Wizard1, RegisterRequirementSpecial.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnFinish") as Button;
        this.btnPreviousTemplate = this.GetControlFromWizard(this.Wizard1, RegisterRequirementSpecial.WizardNavigationTempContainer.FinishNavigationTemplateContainerID, "btnPrevious") as Button;
        if (this.btnFinishTemplate != null)
        {
          this.btnFinishTemplate.Text = "Agregar";
          this.btnPreviousTemplate.Visible = true;
        }
        if (this.btnClaim == null)
          return;
        this.btnClaim.Visible = true;
      }
      else
      {
        this.btnClaim = this.GetControlFromWizard(this.Wizard1, RegisterRequirementSpecial.WizardNavigationTempContainer.StartNavigationTemplateContainerID, "btnClaim") as Button;
        string str = Convert.ToString(this.Request.QueryString["Prot"], (IFormatProvider) CultureInfo.CurrentCulture);
        if (this.btnClaim != null)
        {
          if (this.Wizard1.ActiveStep == this.WS_ValidacionPlaca && str == "I" || this.Wizard1.ActiveStep == this.WS_ValidacionOrden && str == "I" || this.Wizard1.ActiveStep == this.WS_DatosVehiculo1 || this.Wizard1.ActiveStep == this.WS_DatosVehiculo2)
            this.btnClaim.Visible = true;
          else
            this.btnClaim.Visible = false;
        }
      }
    }

    public void getProofPayment()
    {
      try
      {
        this.oRequirementQueriesBL = new RequirementQueriesBL();
        this.rdbProofPayment.DataSource = (object) this.oRequirementQueriesBL.GetProofPaymenTypeSpecial();
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
        this.oRequirementQueriesBL = new RequirementQueriesBL();
        this.CboDocumentType.DataSource = (object) this.oRequirementQueriesBL.GetDocumentType();
        this.CboDocumentType.DataTextField = "v_Description";
        this.CboDocumentType.DataValueField = "i_ParameterId";
        this.CboDocumentType.DataBind();
        this.CboDocumentType.SelectedIndex = 4;
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

    public void AddRequirement()
    {
      try
      {
        string str = Convert.ToString(this.Request.QueryString["Prot"], (IFormatProvider) CultureInfo.CurrentCulture);
        if (!this.chkAccept.Checked && str == "I")
          throw new HandledException(1, "DEBE CONFIRMAR A LA ASOCIACIÓN AUTOMOTRIZ DEL PERU QUE LA INFORMACION MOSTRADA ES LA CORRECTA");
        if (!this.chkAccept3rd.Checked && str != "I")
          throw new HandledException(1, "DEBE CONFIRMAR A LA ASOCIACIÓN AUTOMOTRIZ DEL PERU QUE LA INFORMACION MOSTRADA ES LA CORRECTA");
        if (str == "I")
        {
          this.ValidateData();
          this.InsertListGroup(this.txtPlateNumber.Text.Trim().ToUpper(CultureInfo.CurrentCulture), this.txtTitleNumber.Text.Trim().ToUpper(CultureInfo.CurrentCulture));
        }
        else
          this.InsertListGroup(this.txtPlateNumber1.Text.Trim().ToUpper(CultureInfo.CurrentCulture), "");
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

    public void registerClaim_DataNotFound()
    {
      try
      {
        ClaimGenerator claimGenerator = new ClaimGenerator();
        SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
        string pstrPlateNumber;
        string pstrTitleNumber;
        if (Convert.ToString(this.Request.QueryString["Prot"], (IFormatProvider) CultureInfo.CurrentCulture) == "I")
        {
          pstrPlateNumber = this.txtPlateNumber.Text.Trim();
          pstrTitleNumber = this.txtTitleNumber.Text.Trim();
        }
        else
        {
          pstrPlateNumber = this.txtPlateNumber1.Text.Trim();
          pstrTitleNumber = "-";
        }
        int systemUserPublic = new RequirementQueriesBL().GetSystemUserPublic(systemUser.i_SystemUserId, 1);
        string pstrRequesterFirstName = "";
        string pstrRequesterLastName = "";
        string pstrRequesterDocumentNumber = "";
        string pstrRequesterEmail = "";
        int num = -1;
        if (systemUserPublic == 1)
        {
          pstrRequesterFirstName = systemUser.v_FirstName;
          pstrRequesterLastName = systemUser.v_LastName;
          pstrRequesterDocumentNumber = systemUser.v_DocumentNumber;
          num = systemUser.i_DocumentTypeId.Value;
          pstrRequesterEmail = systemUser.v_Email;
        }
        this.CreatePopUp(claimGenerator.GenerateClaim_DataNoFound_PopUp(pstrPlateNumber, pstrTitleNumber, pstrRequesterFirstName, pstrRequesterLastName, num.ToString((IFormatProvider) CultureInfo.CurrentCulture), pstrRequesterDocumentNumber, pstrRequesterEmail, ""), "Informacion No Encontrada", "575", "515");
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
        SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
        this.oRequirementQueriesBL = new RequirementQueriesBL();
        string str1 = Convert.ToString(this.Request.QueryString["Prot"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.dtVehicleData = !(str1 == "I") ? this.oRequirementQueriesBL.SunarpDataRead3rd(this.txtPlateNumber1.Text.Trim()) : this.oRequirementQueriesBL.SunarpDataReadbyId(Convert.ToInt32(this.Session["i_VehicleId"], (IFormatProvider) CultureInfo.CurrentCulture));
        string pstrPlateNumber = this.dtVehicleData.Rows[0]["v_platenew"].ToString();
        string pstrPlateOld = this.dtVehicleData.Rows[0]["v_plateold"].ToString();
        string pstrVehicleModel = this.dtVehicleData.Rows[0]["v_model"].ToString();
        string pstrVehicleBrand = this.dtVehicleData.Rows[0]["v_Brand"].ToString();
        string pstrVehicleSerial = this.dtVehicleData.Rows[0]["v_serialNumber"].ToString();
        string str2 = !(this.dtVehicleData.Rows[0]["CategoryId"].ToString() != "") ? (string) null : this.dtVehicleData.Rows[0]["CategoryId"].ToString();
        string pstrCategory = !(this.dtVehicleData.Rows[0]["Category"].ToString() != "") ? "-" : this.dtVehicleData.Rows[0]["Category"].ToString();
        string pstrTitleNumber = !(str1 == "I") ? "-" : this.dtVehicleData.Rows[0]["v_titlenumber"].ToString();
        this.dtOwners = this.oRequirementQueriesBL.GetOwnersByIdSunarp(Convert.ToInt32(this.Session["i_VehicleId"], (IFormatProvider) CultureInfo.CurrentCulture));
        string str3 = "";
        string str4 = "";
        string str5 = "";
        string str6 = "";
        for (int index = 0; index < this.dtOwners.Rows.Count; ++index)
        {
          str3 = str3 + "/" + this.dtOwners.Rows[index]["CompleteName"].ToString();
          str4 = str4 + "/" + this.dtOwners.Rows[index]["v_DocumentNumber"].ToString();
          str5 = str5 + "/" + this.dtOwners.Rows[index]["v_DocumentTypeId"].ToString();
          str6 = str6 + "/" + this.dtOwners.Rows[index]["i_Item"].ToString();
        }
        string pstrCategoryGroup = "204";
        str3.Substring(1, str3.Length - 1);
        str4.Substring(1, str4.Length - 1);
        str5.Substring(1, str5.Length - 1);
        str6.Substring(1, str6.Length - 1);
        int systemUserPublic = new RequirementQueriesBL().GetSystemUserPublic(systemUser.i_SystemUserId, 1);
        string pstrRequesterFirstName = "";
        string pstrRequesterLastName = "";
        string pstrRequesterDocumentNumber = "";
        string pstrRequesterEmail = "";
        int num = -1;
        if (systemUserPublic == 1)
        {
          pstrRequesterFirstName = systemUser.v_FirstName;
          pstrRequesterLastName = systemUser.v_LastName;
          pstrRequesterDocumentNumber = systemUser.v_DocumentNumber;
          num = systemUser.i_DocumentTypeId.Value;
          pstrRequesterEmail = systemUser.v_Email;
        }
        this.CreatePopUp(claimGenerator.GenerateClaim_DataNoAgree_PopUp(pstrPlateNumber, pstrTitleNumber, pstrVehicleModel, pstrVehicleBrand, pstrVehicleSerial, this.Session["i_VehicleId"].ToString(), "", "", "", pstrPlateOld, pstrRequesterFirstName, pstrRequesterLastName, num.ToString((IFormatProvider) CultureInfo.CurrentCulture), pstrRequesterDocumentNumber, pstrRequesterEmail, "", Convert.ToString(str2, (IFormatProvider) CultureInfo.CurrentCulture), pstrCategory, pstrCategoryGroup), "Informacion No Conforme", "740", "825");
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

    public void FinishSpecialRequirement()
    {
      try
      {
        this.ValidateProofPaymentData();
        new ValidatorRegularExpressionProofPaymentBL().ProofPaymentData(this.CboDocumentType.Text.Trim(), this.TxtDocNumberProofPaper.Text.Trim(), this.txtBeneficiaryName.Text.Trim(), this.txtAddress.Text.Trim(), this.txtBeneficiaryMail.Text.Trim());
        if (!this.chkConfirmPreview.Checked)
          throw new HandledException(1, "DEBE CONFIRMAR LOS DATOS MOSTRADOS");
        using (TransactionScope transactionScope = new TransactionScope())
        {
          SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterRequirementSpecial.aspx");
          string str1 = this.Request.QueryString["Prot"].ToString((IFormatProvider) CultureInfo.CurrentCulture);
          this.dtIDS = new DataTable();
          this.dtIDS.Columns.Add("i_RequirementId", typeof (int));
          this.dtIDS.Columns.Add("i_RequirementPlateId", typeof (int));
          this.dtIDS.Columns.Add("i_Success", typeof (int));
          int int32 = Convert.ToInt32(this.Request.QueryString["idDoc"], (IFormatProvider) CultureInfo.CurrentCulture);
          bool flag = true;
          string ErrorMessage = "";
          this.oRequirementQueriesBL = new RequirementQueriesBL();
          for (int index = 0; index < (this.Session["ListSunarpData"] as DataTable).Rows.Count; ++index)
          {
            if (str1 == "I")
            {
              switch (this.oRequirementQueriesBL.ValidateExistRequirementByPlateTitle((this.Session["ListSunarpData"] as DataTable).Rows[index]["v_platenew"].ToString(), (this.Session["ListSunarpData"] as DataTable).Rows[index]["v_titlenumber"].ToString()))
              {
                case -4:
                  flag = false;
                  ErrorMessage = ErrorMessage + "EL NUMERO DE PLACA " + (this.Session["ListSunarpData"] as DataTable).Rows[index]["v_platenew"].ToString() + "Y TITULO DETERMINAN UNA CAMBIO DE CLASE/CARACTERISTICAS DE PLACA NUEVA, SOLO ES UN REGISTRO INFORMATIVO PARA TRAMITES COMO DUPLICADO CON PLACA NUEVA, CAMBIO DE USO O DUPLICADO DE TERCERA PLACA</br>";
                  continue;
                case -3:
                  flag = false;
                  ErrorMessage = ErrorMessage + "EL NUMERO DE PLACA " + (this.Session["ListSunarpData"] as DataTable).Rows[index]["v_platenew"].ToString() + "Y TITULO DETERMINAN UNA TRANFERENCIA DE PLACA NUEVA, SOLO ES UN REGISTRO INFORMATIVO PARA TRAMITES COMO DUPLICADO CON PLACA NUEVA, CAMBIO DE USO o DUPLICADO DE TERCERA PLACA</br>";
                  continue;
                case -2:
                  flag = false;
                  ErrorMessage = ErrorMessage + "EL NUMERO DE PLACA " + (this.Session["ListSunarpData"] as DataTable).Rows[index]["v_platenew"].ToString() + " TERMINO UN TRAMITE DE PLACA ( ENTREGADA )</br>";
                  continue;
                case -1:
                  flag = false;
                  ErrorMessage = ErrorMessage + "EL NUMERO DE PLACA " + (this.Session["ListSunarpData"] as DataTable).Rows[index]["v_platenew"].ToString() + " Y EL NUMERO DE TITULO FUERON TOMADOS EN UN TRAMITE ANTERIOR</br>";
                  continue;
                case 0:
                  flag = false;
                  ErrorMessage = ErrorMessage + "EL NUMERO DE PLACA " + (this.Session["ListSunarpData"] as DataTable).Rows[index]["v_platenew"].ToString() + " TIENE UN TRAMITE EN CURSO DEBE CULMINAR EL TRAMITE PARA INICIAR UNO NUEVO</br>";
                  continue;
                default:
                  continue;
              }
            }
            else
            {
              switch (this.oRequirementQueriesBL.Verify3rdPlate((this.Session["ListSunarpData"] as DataTable).Rows[index]["v_platenew"].ToString()))
              {
                case -2:
                  flag = false;
                  this.dtClaim = new RequirementClaimQueriesBL().RequirementClaimGetActiveByPlate((this.Session["ListSunarpData"] as DataTable).Rows[index]["v_platenew"].ToString());
                  string str2 = this.dtClaim.Rows[0]["v_ClaimCode"].ToString();
                  ErrorMessage = ErrorMessage + "LA PLACA " + (this.Session["ListSunarpData"] as DataTable).Rows[index]["v_platenew"].ToString() + " TIENE UN RECLAMO EN CURSO, POR FAVOR COMUNIQUESE CON CASOS ESPECIALES.</br>EL NUMERO DE RECLAMO ES " + str2;
                  break;
                case -1:
                  flag = false;
                  ErrorMessage = ErrorMessage + "EL NUMERO DE PLACA " + (this.Session["ListSunarpData"] as DataTable).Rows[index]["v_platenew"].ToString() + " INGRESADO NO AH REGISTRADO UN TRAMITE PREVIO DE PLACA NUEVA</br>";
                  break;
                case 0:
                  flag = false;
                  ErrorMessage = ErrorMessage + "EL NUMERO DE PLACA " + (this.Session["ListSunarpData"] as DataTable).Rows[index]["v_platenew"].ToString() + " TIENE UN TRAMITE EN CURSO DEBE CULMINAR EL TRAMITE PARA INICIAR UNO NUEVO</br>";
                  break;
              }
            }
          }
          if (!flag)
            throw new HandledException(1, ErrorMessage);
          this.oRequirementManagement = new RequirementManagementBL();
          for (int index = 0; index < (this.Session["ListSunarpData"] as DataTable).Rows.Count; ++index)
          {
            this.drVehicleData = (this.Session["ListSunarpData"] as DataTable).Rows[index];
            this.objRequirement = new SIIV.BE.Requirement();
            this.objRequirement.i_RequirementTypeId = new int?(1);
            this.objRequirement.f_Quantity = new double?(1.0);
            this.objRequirement.v_Observations = "";
            this.objRequirement.i_ProofPaymentTypeId = new int?(Convert.ToInt32(this.rdbProofPayment.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture));
            this.objRequirement.i_InsertUserId = new int?(systemUser.i_SystemUserId);
            this.objRequirement.v_Ubigeo = "";
            this.objVehicleRegistrationDetail = new VehicleRegistrationDetail();
            this.objVehicleRegistrationDetail.i_RegistrationTypeId = new int?(2);
            this.objVehicleRegistrationDetail.i_RegistryOfficeId = new int?(Convert.ToInt32(this.drVehicleData["RegistryOfficeId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
            this.objVehicleRegistrationDetail.i_RegistryZoneId = new int?(Convert.ToInt32(this.drVehicleData["RegistryZoneId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
            this.objVehicleRegistrationDetail.i_VehicleCategoryId = !(this.drVehicleData["CategoryId"].ToString() != "") ? new int?() : new int?(Convert.ToInt32(this.drVehicleData["CategoryId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
            this.objVehicleRegistrationDetail.v_PlateNew = this.drVehicleData["v_platenew"].ToString();
            this.objVehicleRegistrationDetail.v_PlateOld = this.drVehicleData["v_plateold"].ToString();
            this.objVehicleRegistrationDetail.v_Brand = this.drVehicleData["v_brand"].ToString();
            this.objVehicleRegistrationDetail.v_Model = this.drVehicleData["v_model"].ToString();
            this.objVehicleRegistrationDetail.v_SerialNumber = this.drVehicleData["v_serialnumber"].ToString();
            this.objVehicleRegistrationDetail.v_CompleteNameOwner = this.drVehicleData["v_OwnerCompleteName"].ToString();
            this.objRequirementPlate = new RequirementPlate();
            this.objRequirementPlate.i_RegistrationUseTypeOldId = new int?();
            int num = 0;
            switch (str1)
            {
              case "I":
                num = 5;
                break;
              case "D":
                num = 1;
                break;
              case "T":
                num = 4;
                break;
            }
            this.objRequirementPlate.i_ProcessTypeId = new int?(num);
            this.objVehicleRegistrationDetail.i_VehicleClassId = new int?(Convert.ToInt32(this.drVehicleData["Classid"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
            this.objVehicleRegistrationDetail.i_VehicleTypeUseId = new int?(Convert.ToInt32(this.drVehicleData["UseTypeSunarp"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
            this.objVehicleRegistrationDetail.i_RegistrationTypeId = new int?(Convert.ToInt32(this.drVehicleData["i_VehicleRegistrationId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
            string str3 = this.Request.QueryString["veht"];
            if (str3.ToUpper(CultureInfo.CurrentCulture) == "E")
              this.objVehicleRegistrationDetail.i_SpecialPlateTypeId = new int?(2);
            else if (str3.ToUpper(CultureInfo.CurrentCulture) == "P")
              this.objVehicleRegistrationDetail.i_SpecialPlateTypeId = new int?(1);
            switch (str1)
            {
              case "I":
                this.objRequirementPlate.i_ProductId = new int?(Convert.ToInt32(this.drVehicleData["i_ProductId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
                this.objVehicleRegistrationDetail.v_TitleNumber = this.drVehicleData["v_titlenumber"].ToString();
                goto label_37;
              case "T":
                this.objRequirementPlate.i_ProductId = new int?(Convert.ToInt32((this.ViewState["Product3rd"] as DataTable).Rows[0]["i_ProductId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
                break;
              default:
                this.objRequirementPlate.i_ProductId = new int?(Convert.ToInt32(this.drVehicleData["i_ProductId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
                break;
            }
            this.objVehicleRegistrationDetail.v_TitleNumber = this.oRequirementQueriesBL.GetCorrelativeTitle();
label_37:
            this.objRequirementPlate.i_ProductId = new int?(new RequirementQueriesBL().GetProductCorrespondence(this.objRequirementPlate.i_ProductId.Value, 2));
            this.objRequirementPlate.i_RequirementPlateTypeId = new int?(4);
            this.objRequirementPlate.i_VehicleId = new int?(Convert.ToInt32(this.drVehicleData["i_VehicleId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
            this.objRequirementPlate.v_RegistrationCode = "";
            this.objRequirementPlate.i_ContingencyTypeId = new int?();
            this.objRequirementPlate.b_ContingencyDelivery = new bool?();
            this.objRequirementPlate.d_RegistrationDispatchDate = new DateTime?(Convert.ToDateTime(this.drVehicleData["d_DispatchDate"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
            this.objRequirementPlate.i_DeliveryPointId = new int?(Convert.ToInt32(this.cboDeliveryPoint.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
            this.objRequirementPlate.i_PlateTypeId = new int?(1);
            this.objRequirementPlate.b_PendingConfirmation = new bool?();
            this.objRequirementPlate.b_Migrated = new bool?();
            this.objRequirementContributor = new RequirementContributor();
            this.objRequirementContributor.i_DocumentTypeId = new int?(Convert.ToInt32(this.CboDocumentType.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture));
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
            this.objContributorRequester.v_Address = systemUser.v_Address;
            this.objContributorRequester.v_AddressLocation = systemUser.v_Ubigeo;
            this.objContributorRequester.v_PhoneNumber = (string) null;
            this.drOwners = (this.Session["ListOwners"] as DataTable).Rows[index];
            this.objRequirementPlate.v_OwnerCompleteName = this.drOwners["strCompleteName"].ToString();
            this.objRequirementPlate.v_OwnerDocumentType = this.drOwners["strDocumentType"].ToString();
            this.objRequirementPlate.v_OwnerDocumentDescription = this.drOwners["strDocumentDescription"].ToString();
            this.objRequirementPlate.v_OwnerDocumentNumber = this.drOwners["strDocumentNumber"].ToString();
            this.objPayment = new Payment();
            this.objPayment.f_PriceSale = new double?(0.0);
            this.objPayment.f_PriceTax = new double?(0.0);
            this.objPayment.f_PriceTotal = new double?(0.0);
            this.objRequirement.i_Status = new int?(1);
            this.objRequirementPlate.i_Status = new int?(0);
            this.objPayment.i_Status = new int?();
            this.objPayment.i_PaymentTypeId = new int?();
            this.objPayment.i_BankId = new int?();
            this.objPayment.v_BankOperationNumber = (string) null;
            this.objPayment.v_BankOperationUser = (string) null;
            this.objPayment.v_BankOperationTerminal = (string) null;
            this.objPayment.i_AccountId = new int?();
            this.objRequirementPlate.i_DataBankId = (string) null;
            this.objRequirementProgramation = new RequirementProgramation();
            this.objRequirementProgramation.i_ZoneReference = new int?();
            this.objRequirementProgramation.i_DistrictReference = new int?();
            int[] numArray = this.oRequirementManagement.RequirementInsertOne(this.objRequirement, this.objVehicleRegistrationDetail, this.objRequirementPlate, this.objRequirementContributor, this.objContributorRequester, this.objPayment, this.objRequirementProgramation, this.Request.UserHostAddress);
            DataRow row = this.dtIDS.NewRow();
            row["i_RequirementId"] = (object) Convert.ToInt32(numArray[0].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
            row["i_RequirementPlateId"] = (object) Convert.ToInt32(numArray[1].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
            this.dtIDS.Rows.Add(row);
            this.oRequirementManagement.IncreaseRelatedDocumentQuantity(int32, systemUser.i_SystemUserId);
            this.objSpecialRequirement = new SpecialRequirement();
            this.objSpecialRequirement.i_RequirementPlateId = new int?(Convert.ToInt32(row[1].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
            this.objSpecialRequirement.i_RelatedDocumentId = new int?(int32);
            this.objSpecialRequirement.v_Observations = this.txtObservation.Text.Trim();
            this.objSpecialRequirement.i_InsertUserId = new int?(systemUser.i_SystemUserId);
            this.oRequirementManagement.SpecialRequirementInsert(this.objSpecialRequirement);
          }
          transactionScope.Complete();
          this.Session["ids"] = (object) this.dtIDS;
          this.Response.Redirect("~/Requirement/SuccessfulSpecialRegistration.aspx");
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
        string str1 = this.Request.QueryString["veht"];
        if (this.txtPlateNumber.Text.ToUpper(CultureInfo.CurrentCulture).Substring(0, 2) != "EU" && str1.ToUpper(CultureInfo.CurrentCulture) == "E")
          throw new HandledException(1, "NO ES UNA PLACA DE EMRGENCIA VALIDA.");
        if (this.txtPlateNumber.Text.ToUpper(CultureInfo.CurrentCulture).Substring(0, 2) != "EP" && str1.ToUpper(CultureInfo.CurrentCulture) == "P")
          throw new HandledException(1, "NO ES UNA PLACA POLICIAL VALIDA.");
        int? nullable1 = new int?();
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string str2 = this.txtPlateNumber.Text.ToUpper(CultureInfo.CurrentCulture).Trim().Replace("-", "");
        string str3 = this.txtTitleNumber.Text.ToUpper(CultureInfo.CurrentCulture).Trim().Replace("-", "");
        if (str2 == "")
          throw new HandledException(1, "POR FAVOR INGRESE SU NUMERO DE PLACA");
        if (str3 == "")
          throw new HandledException(1, "POR FAVOR INGRESE SU NUMERO DE TITULO");
        if (this.Session["ListSunarpData"] != null)
        {
          for (int index = 0; index < (this.Session["ListSunarpData"] as DataTable).Rows.Count; ++index)
          {
            if ((this.Session["ListSunarpData"] as DataTable).Rows[index]["v_PlateNew"].ToString().ToUpper(CultureInfo.CurrentCulture).Replace("-", "") == this.txtPlateNumber.Text.ToUpper(CultureInfo.CurrentCulture).Trim())
              throw new HandledException(1, "EL NUMERO DE PLACA ES UNA DE LAS INGRESADAS A LA LISTA.");
          }
        }
        this.oRequirementQueriesBL = new RequirementQueriesBL();
        nullable1 = new int?(this.oRequirementQueriesBL.ValidateExistRequirementByPlateTitle(str2, str3));
        int? nullable2 = nullable1.GetValueOrDefault() != -1 ? nullable1 : throw new HandledException(1, "EL NUMERO DE PLACA Y EL NUMERO DE TITULO FUERON TOMADOS EN UN TRAMITE ANTERIOR");
        int num1 = 0;
        if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
          throw new HandledException(1, "EL NUMERO DE PLACA TIENE UN TRAMITE EN CURSO DEBE CULMINAR EL TRAMITE PARA INICIAR UNO NUEVO");
        if (nullable1.GetValueOrDefault() == -2)
          throw new HandledException(1, "EL NUMERO DE PLACA TERMINO UN TRAMITE DE PLACA ( ENTREGADA )");
        if (nullable1.GetValueOrDefault() == -3)
          throw new HandledException(1, "EL NUMERO DE PLACA Y TITULO DETERMINAN UNA TRANFERENCIA DE PLACA NUEVA, SOLO ES UN REGISTRO INFORMATIVO PARA TRAMITES COMO DUPLICADO CON PLACA NUEVA, CAMBIO DE USO o DUPLICADO DE TERCERA PLACA");
        if (nullable1.GetValueOrDefault() == -4)
          throw new HandledException(1, "EL NUMERO DE PLACA Y TITULO DETERMINAN UNA CAMBIO DE CLASE/CARACTERISTICAS DE PLACA NUEVA, SOLO ES UN REGISTRO INFORMATIVO PARA TRAMITES COMO DUPLICADO CON PLACA NUEVA, CAMBIO DE USO O DUPLICADO DE TERCERA PLACA");
        nullable1 = new int?(this.oRequirementQueriesBL.ValidateExistSunarpByPlateTitle(str2, str3));
        if (nullable1.GetValueOrDefault() == 1)
        {
          this.lblMessage.Text = "";
          this.ShowVehicleData(str2, str3);
        }
        else
        {
          if (nullable1.GetValueOrDefault() == -3)
          {
            this.dtClaim = new RequirementClaimQueriesBL().RequirementClaimGetActiveByPlate(str2);
            throw new HandledException(1, "SU PLACA TIENE UN RECLAMO EN CURSO, POR FAVOR COMUNIQUESE CON CASOS ESPECIALES.</br>EL NUMERO DE RECLAMO ES " + this.dtClaim.Rows[0]["v_ClaimCode"].ToString());
          }
          nullable2 = nullable1;
          int num2 = 0;
          if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
            throw new HandledException(1, "EL NUMERO DE PLACA Y TITULO NO COINCIDEN POR FAVOR REINTENTE");
          if (nullable1.GetValueOrDefault() == -1)
            throw new HandledException(1, "SU REGISTRO NO HA SIDO ENCONTRADO. POR FAVOR ESPERE 24 HORAS Y VUELVA A INTENTAR. SI EL PROBLEMA PERSISTE COMUNIQUESE CON SUNARP");
          if (nullable1.GetValueOrDefault() == -2)
            throw new HandledException(1, "SU REGISTRO HA SIDO ENCONTRADO. PERO EL TIPO DE TRAMITE REPORTADO NO AH SIDO HOMOLOGADO CON NUESTRO SISTEMA, POR FAVOR REINTENTE EN 48 HORAS. DISCULPE LAS MOLESTIAS");
        }
        this.Wizard1.ActiveStepIndex = this.Wizard1.WizardSteps.IndexOf((WizardStepBase) this.WS_DatosVehiculo1);
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
        string str1 = Convert.ToString(this.Request.QueryString["Prot"], (IFormatProvider) CultureInfo.CurrentCulture);
        if (string.IsNullOrEmpty(this.txtPlateNumber1.Text))
          throw new HandledException(1, "POR FAVOR INGRESE SU NUMERO DE PLACA");
        if (this.Session["ListSunarpData"] != null)
        {
          for (int index = 0; index < (this.Session["ListSunarpData"] as DataTable).Rows.Count; ++index)
          {
            if ((this.Session["ListSunarpData"] as DataTable).Rows[index]["v_PlateNew"].ToString().ToUpper(CultureInfo.CurrentCulture).Replace("-", "") == this.txtPlateNumber1.Text.ToUpper(CultureInfo.CurrentCulture).Trim().Replace("-", ""))
              throw new HandledException(1, "EL NRO DE PLACA ES UNA DE LAS INGRESADAS A LA LISTA.");
          }
        }
        string str2 = this.Request.QueryString["veht"];
        if (this.txtPlateNumber1.Text.ToUpper(CultureInfo.CurrentCulture).Substring(0, 2) != "EU" && str2.ToUpper(CultureInfo.CurrentCulture) == "E")
          throw new HandledException(1, "NO ES UNA PLACA DE EMRGENCIA VALIDA.");
        if (this.txtPlateNumber1.Text.ToUpper(CultureInfo.CurrentCulture).Substring(0, 2) != "EP" && str2.ToUpper(CultureInfo.CurrentCulture) == "P")
          throw new HandledException(1, "NO ES UNA PLACA POLICIAL VALIDA.");
        if (string.IsNullOrEmpty(this.txtPlateNumber1.Text))
          throw new HandledException(1, "POR FAVOR INGRESE SU NUMERO DE PLACA");
        string empty = string.Empty;
        string pstrPlateNumber = this.txtPlateNumber1.Text.Replace("-", "");
        this.oRequirementQueriesBL = new RequirementQueriesBL();
        switch (this.oRequirementQueriesBL.Verify3rdPlate(pstrPlateNumber))
        {
          case -2:
            this.dtClaim = new RequirementClaimQueriesBL().RequirementClaimGetActiveByPlate(this.txtPlateNumber1.Text.Trim().Replace("-", ""));
            throw new HandledException(1, "SU PLACA TIENE UN RECLAMO EN CURSO, POR FAVOR COMUNIQUESE CON CASOS ESPECIALES.</br>EL NUMERO DE RECLAMO ES " + this.dtClaim.Rows[0]["v_ClaimCode"].ToString());
          case -1:
            throw new HandledException(1, "EL NUMERO DE PLACA INGRESADO NO HA REGISTRADO UN TRAMITE PREVIO DE PLACA NUEVA");
          case 0:
            throw new HandledException(1, "EL NUMERO DE PLACA TIENE UN TRAMITE EN CURSO DEBE CULMINAR EL TRAMITE PARA INICIAR UNO NUEVO");
          default:
            int num = 1;
            if (str1 == "T")
              num = this.oRequirementQueriesBL.VerifyProcess3rdPlate(pstrPlateNumber);
            if (num == 0)
              throw new HandledException(1, "LA PLACA INGRESADA PERTENECE A UNA CLASE VEHICULAR QUE NO PERMITE TERCERA PLACA");
            this.ShowVehicleData3rd();
            this.Wizard1.MoveTo((WizardStepBase) this.WS_DatosVehiculo2);
            break;
        }
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
        this.txtDispatchDate.Text = Convert.ToDateTime(this.dtVehicleData.Rows[0]["d_DispatchDate"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture).ToString("dd/MM/yyyy", (IFormatProvider) CultureInfo.CurrentCulture);
        this.dtOwners = this.oRequirementQueriesBL.GetOwnersByIdSunarp(Convert.ToInt32(this.dtVehicleData.Rows[0][0].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
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
        this.txtPlateNumber.Enabled = false;
        this.txtTitleNumber.Enabled = false;
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
        this.oRequirementQueriesBL = new RequirementQueriesBL();
        this.lblPrice.Visible = true;
        this.txtPrice3rd.Visible = true;
        string str = Convert.ToString(this.Request.QueryString["Prot"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.dtVehicleData = this.oRequirementQueriesBL.SunarpDataRead3rd(this.txtPlateNumber1.Text.Trim());
        this.ViewState["SunarpData"] = (object) this.dtVehicleData;
        this.txtBrand3rd.Text = this.dtVehicleData.Rows[0]["v_Brand"].ToString();
        this.txtModel3rd.Text = this.dtVehicleData.Rows[0]["v_Model"].ToString();
        this.txtPlateNew3rd.Text = this.dtVehicleData.Rows[0]["v_PlateNew"].ToString();
        this.txtSerialNumber3rd.Text = this.dtVehicleData.Rows[0]["v_SerialNumber"].ToString();
        this.txtUseType3rd.Text = this.dtVehicleData.Rows[0]["TypeUseDescription"].ToString();
        this.txtCategory3rd.Text = this.dtVehicleData.Rows[0]["Category"].ToString();
        this.dtOwners = this.oRequirementQueriesBL.GetOwnersByIdSunarp(Convert.ToInt32(this.dtVehicleData.Rows[0][0].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
        this.ViewState["Owners"] = (object) this.dtOwners;
        for (int index = 0; index < this.dtOwners.Rows.Count; ++index)
          this.txtOwner3rd.Text = this.dtOwners.Rows[index]["completeName"].ToString();
        if (str == "T")
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

    private void ValidateData()
    {
      try
      {
        if (this.txtOwners.Text.Trim() == "")
          throw new HandledException(1, "PROPIETARIO NO PUEDE SER VACIO");
        if (this.txtModel.Text.Trim() == "")
          throw new HandledException(1, "MODELO NO PUEDE SER VACIO");
        if (this.txtBrand.Text.Trim() == "")
          throw new HandledException(1, "MARCA NO PUEDE SER VACIO");
        if (this.txtSerialNumber.Text.Trim() == "")
          throw new HandledException(1, "NÚMERO SERIE NO PUEDE SER VACIO");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void InsertListGroup(string _Plate, string _Title)
    {
      try
      {
        this.dtListVehicleData = this.Session["ListSunarpData"] as DataTable;
        if (this.dtListVehicleData == null)
          this.dtListVehicleData = (this.ViewState["SunarpData"] as DataTable).Clone();
        foreach (DataRow row in (InternalDataCollectionBase) (this.ViewState["SunarpData"] as DataTable).Rows)
          this.dtListVehicleData.ImportRow(row);
        this.Session["ListSunarpData"] = (object) this.dtListVehicleData;
        this.dtOwners = this.ViewState["Owners"] as DataTable;
        string str1 = "";
        string str2 = "";
        string str3 = "";
        string str4 = "";
        for (int index = 0; index < this.dtOwners.Rows.Count; ++index)
        {
          str1 = str1 + "/" + this.dtOwners.Rows[index]["CompleteName"].ToString();
          str2 = str2 + "/" + this.dtOwners.Rows[index]["v_DocumentTypeId"].ToString();
          str3 = str3 + "/" + this.dtOwners.Rows[index]["v_DocumentType"].ToString();
          str4 = str4 + "/" + this.dtOwners.Rows[index]["v_DocumentNumber"].ToString();
        }
        string str5 = str1.Substring(1, str1.Length - 1);
        string str6 = str2.Substring(1, str2.Length - 1);
        string str7 = str3.Substring(1, str3.Length - 1);
        string str8 = str4.Substring(1, str4.Length - 1);
        this.dtListOwners = this.Session["ListOwners"] as DataTable;
        if (this.dtListOwners == null)
        {
          RegisterRequirementSpecial.colVideos[0] = new DataColumn("strCompleteName");
          RegisterRequirementSpecial.colVideos[1] = new DataColumn("strDocumentType");
          RegisterRequirementSpecial.colVideos[2] = new DataColumn("strDocumentDescription");
          RegisterRequirementSpecial.colVideos[3] = new DataColumn("strDocumentNumber");
          this.dtListOwners = new DataTable();
          this.dtListOwners.Columns.AddRange(RegisterRequirementSpecial.colVideos);
        }
        this.dtListOwners.Rows.Add((object) str5, (object) str6, (object) str7, (object) str8);
        this.Session["ListOwners"] = (object) this.dtListOwners;
        this.gvList.DataSource = (object) (this.Session["ListSunarpData"] as DataTable);
        this.gvList.DataBind();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void refreshList()
    {
      if (this.Session["ListSunarpData"] == null)
        return;
      this.gvList.DataSource = (object) (this.Session["ListSunarpData"] as DataTable);
      this.gvList.DataBind();
    }

    protected void gvList_PageIndexChanged(object sender, EventArgs e) => this.refreshList();

    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        if (!(e.CommandName == "getCheck"))
          return;
        int int32_1 = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = this.gvList.Rows[int32_1];
        int int32_2 = Convert.ToInt32(this.gvList.DataKeys[int32_1]["i_VehicleId"].ToString());
        this.dtListVehicleData = this.Session["ListSunarpData"] as DataTable;
        for (int index = 0; index < this.dtListVehicleData.Rows.Count; ++index)
        {
          if (Convert.ToInt32(this.dtListVehicleData.Rows[index]["i_VehicleId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture) == int32_2)
          {
            this.dtListVehicleData.Rows.RemoveAt(index);
            break;
          }
        }
        this.Session["ListSunarpData"] = (object) this.dtListVehicleData;
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

    public void BeginList()
    {
      try
      {
        this.gvList.DataSource = (object) new RequirementQueriesBL().SunarpDataRead3rd("");
        this.gvList.DataBind();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private Control GetControlFromWizard(
      Wizard wizard,
      RegisterRequirementSpecial.WizardNavigationTempContainer wzdTemplate,
      string controlName)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append((object) wzdTemplate);
      stringBuilder.Append("$");
      stringBuilder.Append(controlName);
      return wizard.FindControl(stringBuilder.ToString());
    }

    public void BeginTitle()
    {
      try
      {
        switch (this.Request.QueryString["Prot"].ToString((IFormatProvider) CultureInfo.CurrentCulture))
        {
          case "I":
            this.Page.Title = "Solicitudes Especiales(INMATRICULACION)";
            break;
          case "D":
            this.Page.Title = "Solicitudes Especiales(DUPLICADO DE PLACA)";
            break;
          case "T":
            this.Page.Title = "Solicitudes Especiales(DUPLICADO TERCERA PLACA)";
            break;
        }
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
          throw new HandledException(1, "INGRESE NOMBRES O RAZON SOCIAL DEL COMPROBANTE");
        if (this.CboDocumentType.DataTextField == "")
          throw new HandledException(1, "SELECCIONE TIPO DE DOCUMENTO DE IDENTIDAD");
        if (this.CboDocumentType.SelectedIndex == 0)
          throw new HandledException(1, "SELECCIONE TIPO DE DOCUMENTO DE IDENTIDAD");
        if (this.TxtDocNumberProofPaper.Text.Trim() == "")
          throw new HandledException(1, "INGRESE NUMERO DE DOCUMENTO DEL COMPROBANTE");
        if (this.CboDocumentType.SelectedValue == "1" && this.TxtDocNumberProofPaper.Text.Trim().Length < 8)
          throw new HandledException(1, "DNI DEBE TENER 8 DIGITOS");
        if (this.CboDocumentType.SelectedValue == "4" && this.TxtDocNumberProofPaper.Text.Trim().Length < 11)
          throw new HandledException(1, "RUC INGRESADO NO ES VALIDO LONGITUD INCORRECTA");
        if (this.CboDocumentType.SelectedValue == "4" && this.TxtDocNumberProofPaper.Text.Trim().Substring(0, 1) != "1" && this.TxtDocNumberProofPaper.Text.Trim().Substring(0, 1) != "2")
          throw new HandledException(1, "RUC INGRESADO NO ES VALIDO DEBE INICIARCE EN 1 Ó 2");
        if (this.CboDocumentType.SelectedValue == "4" && !this.ValidateRuc(this.TxtDocNumberProofPaper.Text.Trim()))
          throw new HandledException(1, "LA ESTRUCTURA DEL NUMERO DE R.U.C. INGRESADO NO ES CORRECTO");
        if (this.cboDeliveryPoint.SelectedIndex == 0)
          throw new HandledException(1, "SELECCIONE PUNTO DE ENTREGA");
        if (this.txtDocumentNumber.Text.Trim() == "20101973922")
          throw new HandledException(1, "NO SE PUEDE REALIZAR TRAMITES CON ESTE NUMERO DE R.U.C.");
        if (!this.RestrictionRazonSocial())
          throw new HandledException(1, "ESTA RESTRINGIDA LA CREACION DE TRAMITES CON ESTA RAZON SOCIAL");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private bool ValidateRuc(string rucAValidar)
    {
      int num = 11 - (int.Parse(rucAValidar.Substring(0, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 5 + int.Parse(rucAValidar.Substring(1, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 4 + int.Parse(rucAValidar.Substring(2, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 3 + int.Parse(rucAValidar.Substring(3, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 2 + int.Parse(rucAValidar.Substring(4, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 7 + int.Parse(rucAValidar.Substring(5, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 6 + int.Parse(rucAValidar.Substring(6, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 5 + int.Parse(rucAValidar.Substring(7, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 4 + int.Parse(rucAValidar.Substring(8, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 3 + int.Parse(rucAValidar.Substring(9, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 2) % 11;
      return (int.Parse(rucAValidar.Length.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture) != 11 ? 10 : int.Parse(rucAValidar.Substring(10, 1), (IFormatProvider) CultureInfo.CurrentCulture)) == (num != 10 ? 0 : 0) + (num != 11 ? 0 : 1) + (num >= 10 ? 0 : num);
    }

    private bool RestrictionRazonSocial()
    {
      string str = this.txtBeneficiaryName.Text.ToUpper(CultureInfo.CurrentCulture).Replace("À", "A").Replace("Á", "A").Replace("È", "E").Replace("É", "E").Replace("Ì", "I").Replace("Í", "I").Replace("Ó", "O").Replace("Ó", "O").Replace("Ù", "U").Replace("Ú", "U");
      return !str.Contains("ASOCIACION") || !str.Contains("AUTOMOTRIZ") || !str.Contains("PERU");
    }

    public void showOwnerData()
    {
      try
      {
        this.dtOwners = new RequirementQueriesBL().GetOwnersByIdSunarp(Convert.ToInt32((this.ViewState["SunarpData"] as DataTable).Rows[0]["i_VehicleId"], (IFormatProvider) CultureInfo.CurrentCulture));
        this.txtBeneficiaryName.Text = this.dtOwners.Rows[0]["CompleteName"].ToString();
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
