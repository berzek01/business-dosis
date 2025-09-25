// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Requirement.RequirementData
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using BarcodeLib;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.Claim.BL;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using SIIV.SystemUser.BL;
using SIIV.Warehouse.BL;
using SIIV.WebApp.ServiceSoftnet;
using SIIV.WebApp.UserControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Requirement
{
  public class RequirementData : Page
  {
    private MaintenanceOperation currentOperation = MaintenanceOperation.None;
    private DataTable dtRequirementData;
    private DataTable dtProofPaper;
    private DataTable dtBatch;
    private DataTable dtSunarp;
    private DataTable dtPayment;
    private DataTable dtReception;
    private DataTable dtDelivery;
    private DataTable dtDeliveryProgramation;
    private SIIV.BE.SystemUser objUserBE;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected TextBox txtPlateNew;
    protected TextBox txtPlateOld;
    protected TextBox txtOwner;
    protected Label lblMessage;
    protected GridView wdgList;
    protected DataList dlRequirementData;
    protected HtmlTableCell td2;
    protected DataList dlSunarpData;
    protected HtmlTableCell td3;
    protected DataList dlPaymentData;
    protected Button wibCur;
    protected Button wibEBilling;
    protected Button SendMail;
    protected Button SendCurByMail;
    protected HtmlGenericControl divButonDelivery;
    protected Button BtnDelivery;
    protected Button wibReturn;
    protected HtmlTableCell td4;
    protected DataList dlDispatchData;
    protected HtmlTableCell td5;
    protected DataList dlReceptionData;
    protected HtmlTableCell td6;
    protected DataList dlDeliveryData;
    protected HtmlTableCell td7;
    protected DataList dlProgramationDelivery;
    protected RadioButtonList rdbProofPayment;
    protected HtmlTableCell TagDatosComprobante;
    protected TextBox txtBeneficiaryName;
    protected FilteredTextBoxExtender txtBeneficiaryName_FilteredTextBoxExtender;
    protected DropDownList wddDocumentType;
    protected TextBox TxtDocNumberProofPaper;
    protected FilteredTextBoxExtender TxtDocNumberProofPaper_FilteredTextBoxExtender;
    protected TextBox txtAddress;
    protected FilteredTextBoxExtender txtAddress_FilteredTextBoxExtender;
    protected DropDownList wddDeliveryPoint;
    protected Label lblFinalMessage;
    protected HtmlTableRow trManagementButtons;
    protected Button wibEdit;
    protected Button wibCancelEdit;
    protected HtmlTableCell Td1;
    protected HiddenField HfRequirementId;
    protected HiddenField HfRequirementPlateId;
    protected HtmlTableRow trwibFinalze;
    protected Button wibBack;
    protected HtmlTableCell Td8;
    protected TextBox txtEmail;
    protected HtmlTableRow tr1;
    protected Button Enviar;
    protected Button EnviarCur;
    protected Button volver;
    protected Label lblMensaje;
    protected UpdatePanel UpdatePanel2;
    protected GridView wdgClaims;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.objUserBE = this.Session["SystemUser"] as SIIV.BE.SystemUser;
      if (new WarehouseControlQueriesBL().AccessByDeliveryControl(this.objUserBE.i_RoleConfigId) == 1)
        this.divButonDelivery.Visible = true;
      else
        this.divButonDelivery.Visible = false;
      this.loadList();
      this.RefreshList();
      this.Initialize();
      this.ViewState["i_Requirement"] = (object) null;
      this.Session["RequirementPlateId"] = (object) null;
    }

    protected void wdgList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName.Equals("getData", StringComparison.CurrentCulture))
      {
        this.lblMessage.Visible = false;
        this.currentOperation = MaintenanceOperation.AddNew;
        this.ViewState.Add("currentOperation", (object) this.currentOperation);
        this.EnableControls();
        int int32_1 = Convert.ToInt32(e.CommandArgument);
        int int32_2 = Convert.ToInt32(this.wdgList.Rows[int32_1].Cells[2].Text);
        this.ViewState["i_RequirementPlateId"] = (object) int32_2;
        this.ViewState["i_Requirement"] = (object) Convert.ToInt32(this.wdgList.DataKeys[int32_1]["i_RequirementId"].ToString());
        this.showRequirementDetail(int32_2);
        string script = UtilDA.ActiveTabIndex("SubTabs", 1, "0,2,3");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
      if (!e.CommandName.Equals("getEdit", StringComparison.CurrentCulture))
        return;
      this.lblMessage.Visible = false;
      int int32_3 = Convert.ToInt32(e.CommandArgument);
      GridViewRow row = this.wdgList.Rows[int32_3];
      if (Convert.ToInt32(this.wdgList.DataKeys[int32_3]["i_Status"].ToString()) >= 1 || Convert.ToInt32(this.wdgList.DataKeys[int32_3]["i_Status"].ToString()) == -1)
      {
        this.lblMessage.Visible = true;
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>La solicitud ya no puede ser editada.");
        string script = UtilDA.ActiveTabIndex("SubTabs", 0, "1,2,3");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
      else
      {
        this.currentOperation = MaintenanceOperation.Edit;
        this.ViewState.Add("currentOperation", (object) this.currentOperation);
        this.EnableControls();
        int int32_4 = Convert.ToInt32(row.Cells[2].Text);
        this.showProofPaperData(Convert.ToInt32(this.wdgList.DataKeys[int32_3]["i_RequirementId"].ToString()), int32_4);
        string script = UtilDA.ActiveTabIndex("SubTabs", 2, "0,1,3");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
    }

    protected void wdgList_PageIndexChanged(object sender, EventArgs e) => this.RefreshList();

    protected void wibReturn_Click(object sender, EventArgs e)
    {
      string script = UtilDA.ActiveTabIndex("SubTabs", 0, "1,2,3");
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private ReportDocument GenerateCurReport()
    {
      if (this.ViewState["i_Requirement"] == null || this.ViewState["i_RequirementPlateId"] == null)
        return (ReportDocument) null;
      ReportDocument curReport = new ReportDocument();
      int int32_1 = Convert.ToInt32(this.ViewState["i_Requirement"], (IFormatProvider) CultureInfo.CurrentCulture);
      int int32_2 = Convert.ToInt32(this.ViewState["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture);
      string filename = this.Server.MapPath("../Requirement/ReportCur.rpt");
      curReport.Load(filename);
      DataTable curByIds = new RequirementQueriesBL().GenerateCURByIds(int32_1, int32_2);
      curByIds.Columns.Add(new DataColumn("ImageBarPlate", typeof (byte[])));
      curByIds.Columns.Add(new DataColumn("Requisite", typeof (string)));
      curByIds.Columns.Add(new DataColumn("ImageBarCode", typeof (byte[])));
      int num = 0;
      string str = "";
      foreach (DataRow row in (InternalDataCollectionBase) curByIds.Rows)
      {
        row["ImageBarPlate"] = (object) this.ImagenBarCode(row["v_PlateNew"].ToString());
        row["Requisite"] = (object) new RequirementQueriesBL().GetRequisitebyRequirement(int32_1, Convert.ToInt32(row["i_ProcessTypeId"], (IFormatProvider) CultureInfo.CurrentCulture));
        row["ImageBarCode"] = (object) this.ImagenBarCode(row["v_PaymentCode"].ToString());
        if (str != row["i_RequirementId"].ToString().Trim())
          ++num;
        str = row["i_RequirementId"].ToString().Trim();
      }
      if (curByIds.Rows.Count > 1 && num > 1)
        curByIds.Rows.RemoveAt(1);
      curReport.SetDataSource(curByIds);
      return curReport;
    }

    protected void wibCur_Click(object sender, EventArgs e)
    {
      ReportDocument curReport = this.GenerateCurReport();
      if (curReport == null)
        return;
      curReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "Certificado_Unico_Registro");
      curReport.Close();
      ((Component) curReport).Dispose();
      GC.Collect();
    }

    protected void rdbProofPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.rdbProofPayment.SelectedIndex == 0)
      {
        this.wddDocumentType.Items.FindByValue("4").Enabled = true;
        this.wddDocumentType.SelectedValue = "4";
        this.wddDocumentType.Enabled = false;
        this.TxtDocNumberProofPaper.Text = "";
        this.TxtDocNumberProofPaper.MaxLength = 11;
        this.txtBeneficiaryName.Text = "";
        this.wddDeliveryPoint.SelectedIndex = 0;
      }
      else
      {
        this.wddDocumentType.SelectedValue = "1";
        this.wddDocumentType.Enabled = true;
        this.wddDocumentType.Items.FindByValue("4").Enabled = false;
        this.TxtDocNumberProofPaper.Text = "";
        this.TxtDocNumberProofPaper.MaxLength = 8;
      }
      string script = UtilDA.ActiveTabIndex("SubTabs", 2, "0,1");
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void wibEdit_Click(object sender, EventArgs e)
    {
      bool flag = this.ValidateProofPaymentData();
      if (!flag || !flag)
        return;
      this.SaveProofPaperData();
      this.currentOperation = MaintenanceOperation.None;
      this.ViewState.Add("currentOperation", (object) this.currentOperation);
      this.EnableControls();
    }

    protected void wibCancelEdit_Click(object sender, EventArgs e)
    {
      this.currentOperation = MaintenanceOperation.None;
      this.ViewState.Add("currentOperation", (object) this.currentOperation);
      this.EnableControls();
      string script = UtilDA.ActiveTabIndex("SubTabs", 0, "1,2,3");
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void wibBack_Click(object sender, EventArgs e)
    {
      this.currentOperation = MaintenanceOperation.None;
      this.ViewState.Add("currentOperation", (object) this.currentOperation);
      this.EnableControls();
      string script = UtilDA.ActiveTabIndex("SubTabs", 0, "1,2,3");
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    public void loadList()
    {
      string empty = string.Empty;
      string rawUrl = this.Request.RawUrl;
      string[] strArray1 = this.DecryptQueryString(rawUrl.Substring(rawUrl.IndexOf('?') + 1)).Split('&');
      string pstrPlate = "";
      string str1 = "";
      foreach (string str2 in strArray1)
      {
        char[] chArray = new char[1]{ '=' };
        string[] strArray2 = str2.Split(chArray);
        if (strArray2.Length == 2)
        {
          if (strArray2[0] == "plate")
            pstrPlate = strArray2[1];
          else if (strArray2[0] == "t")
            str1 = strArray2[1];
        }
      }
      if (Convert.ToInt32(str1) != 1)
        this.wdgList.Columns[5].Visible = false;
      if (!(pstrPlate != ""))
        return;
      this.Session["ListRequirements"] = (object) new RequirementQueriesBL().GetRequirementDatabyPlate(pstrPlate);
      this.txtPlateNew.Text = (this.Session["ListRequirements"] as DataTable).Rows[0]["v_PlateNew"].ToString();
      this.txtPlateOld.Text = (this.Session["ListRequirements"] as DataTable).Rows[0]["v_PlateOld"].ToString();
      this.txtOwner.Text = (this.Session["ListRequirements"] as DataTable).Rows[0]["v_CompleteNameOwner"].ToString();
      this.ViewState["Claims"] = (object) new RequirementClaimQueriesBL().RequirementClaimGetByPlateAll(pstrPlate);
      string str3 = (this.Session["ListRequirements"] as DataTable).Rows[0]["v_PlateNew"].ToString();
      string str4 = (this.Session["ListRequirements"] as DataTable).Rows[0]["v_PlateOld"].ToString();
      string str5 = "Bloqueado";
      bool flag1 = str3.Contains(str5);
      bool flag2 = str4.Contains(str5);
      if (flag1)
      {
        this.txtPlateNew.BackColor = Color.LightCoral;
        this.txtPlateNew.Font.Bold = true;
      }
      if (flag2)
      {
        this.txtPlateOld.BackColor = Color.LightCoral;
        this.txtPlateOld.Font.Bold = true;
      }
    }

    public string DecryptQueryString(string strQueryString)
    {
      string[] strArray = strQueryString.Split('%');
      strQueryString = strArray[0];
      string seguridad = strArray[1];
      return new Encryption().Decrypt(strQueryString, seguridad);
    }

    public void RefreshList()
    {
      this.wdgList.DataSource = (object) (this.Session["ListRequirements"] as DataTable);
      this.wdgList.DataBind();
      this.wdgClaims.DataSource = (object) (this.ViewState["Claims"] as DataTable);
      this.wdgClaims.DataBind();
    }

    public void Initialize()
    {
      string script = UtilDA.ActiveTabIndex("SubTabs", 0, "1,2,3");
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      this.getDocumentType();
      this.getLocation();
      this.getProofPayment();
    }

    public void getDocumentType()
    {
      this.wddDocumentType.DataSource = (object) new RequirementQueriesBL().GetDocumentType();
      this.wddDocumentType.DataTextField = "v_Description";
      this.wddDocumentType.DataValueField = "i_ParameterId";
      this.wddDocumentType.DataBind();
    }

    public void getLocation()
    {
      this.wddDeliveryPoint.DataSource = (object) new RequirementQueriesBL().GetLocationRequirement();
      this.wddDeliveryPoint.DataTextField = "v_Description";
      this.wddDeliveryPoint.DataValueField = "i_LocationId";
      this.wddDeliveryPoint.DataBind();
    }

    public void getProofPayment()
    {
      this.rdbProofPayment.DataSource = (object) new RequirementQueriesBL().GetProofPaymenType();
      this.rdbProofPayment.DataTextField = "v_Description";
      this.rdbProofPayment.DataValueField = "i_ParameterId";
      this.rdbProofPayment.DataBind();
    }

    private void EnableControls()
    {
      switch (this.currentOperation)
      {
        case MaintenanceOperation.AddNew:
          string script1 = UtilDA.ActiveTabIndex("SubTabs", 1, "0,2,3");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script1, true);
          break;
        case MaintenanceOperation.Edit:
          string script2 = UtilDA.ActiveTabIndex("SubTabs", 2, "0,1,3");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script2, true);
          this.rdbProofPayment.Enabled = true;
          this.txtBeneficiaryName.Enabled = true;
          this.TxtDocNumberProofPaper.Enabled = true;
          this.txtAddress.Enabled = true;
          this.wddDeliveryPoint.Enabled = true;
          this.trManagementButtons.Visible = true;
          this.trwibFinalze.Visible = false;
          break;
        default:
          this.rdbProofPayment.Enabled = false;
          this.txtBeneficiaryName.Enabled = false;
          this.wddDocumentType.Enabled = false;
          this.TxtDocNumberProofPaper.Enabled = false;
          this.txtAddress.Enabled = false;
          this.trwibFinalze.Visible = true;
          this.wddDeliveryPoint.Enabled = false;
          this.trManagementButtons.Visible = false;
          break;
      }
    }

    public void showRequirementDetail(int pintRequirementPlateId)
    {
      try
      {
        this.objUserBE = this.Session["SystemUser"] as SIIV.BE.SystemUser;
        this.dtRequirementData = new RequirementQueriesBL().GetRequirementPlateDatabyId(pintRequirementPlateId);
        this.setRoleAccess(ref this.dtRequirementData);
        this.ViewState["v_beneficiaryEmail"] = (object) this.dtRequirementData.Rows[0]["beneficiaryEmail"].ToString();
        this.dlRequirementData.DataSource = (object) this.dtRequirementData;
        this.dlRequirementData.DataBind();
        this.dtSunarp = new RequirementQueriesBL().GetSunarpDatabyId(!(this.dtRequirementData.Rows[0]["i_VehicleId"].ToString() != "") ? -1 : Convert.ToInt32(this.dtRequirementData.Rows[0]["i_VehicleId"], (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.dtRequirementData.Rows[0]["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture));
        if (this.dtSunarp.Rows.Count == 0)
        {
          this.td2.Visible = false;
        }
        else
        {
          this.td2.Visible = true;
          this.dlSunarpData.DataSource = (object) this.dtSunarp;
          this.dlSunarpData.DataBind();
        }
        this.dtPayment = new RequirementQueriesBL().GetPaymentDatabyRequirement(Convert.ToInt32(this.dtRequirementData.Rows[0]["i_RequirementId"], (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.dtRequirementData.Rows[0]["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture));
        if (this.dtPayment.Rows.Count == 0)
        {
          this.td3.Visible = false;
        }
        else
        {
          this.td3.Visible = true;
          this.dlPaymentData.DataSource = (object) this.dtPayment;
          this.dlPaymentData.DataBind();
        }
        this.dtBatch = new BatchReceptionQueriesBL().GetBatchbyRequirementPlateId(Convert.ToInt32(this.dtRequirementData.Rows[0]["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture), this.objUserBE.i_SystemUserId);
        if (this.dtBatch.Rows.Count == 0)
        {
          this.td4.Visible = false;
        }
        else
        {
          this.td4.Visible = true;
          this.dlDispatchData.DataSource = (object) this.dtBatch;
          this.dlDispatchData.DataBind();
        }
        this.dtReception = new BatchReceptionQueriesBL().GetReceptionbyRequirementPlateId(Convert.ToInt32(this.dtRequirementData.Rows[0]["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture), this.objUserBE.i_SystemUserId);
        if (this.dtReception.Rows.Count == 0)
        {
          this.td5.Visible = false;
        }
        else
        {
          this.td5.Visible = true;
          this.setRoleAccess(ref this.dtReception);
          this.dlReceptionData.DataSource = (object) this.dtReception;
          this.dlReceptionData.DataBind();
        }
        this.dtDelivery = new PlateDeliverQueriesBL().GetPlateDeliverbyRequirementPlate(Convert.ToInt32(this.dtRequirementData.Rows[0]["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture), this.objUserBE.i_SystemUserId);
        if (this.dtDelivery.Rows.Count == 0)
        {
          this.td6.Visible = false;
        }
        else
        {
          this.td6.Visible = true;
          this.setRoleAccess(ref this.dtDelivery);
          this.dlDeliveryData.DataSource = (object) this.dtDelivery;
          this.dlDeliveryData.DataBind();
        }
        this.dtDeliveryProgramation = new RequirementQueriesBL().GetRequirementProgrmationDelivery(Convert.ToInt32(this.dtRequirementData.Rows[0]["i_RequirementPlateId"]), this.objUserBE.i_SystemUserId);
        if (this.dtDeliveryProgramation.Rows.Count == 0)
        {
          this.td7.Visible = false;
        }
        else
        {
          this.td7.Visible = true;
          this.dlProgramationDelivery.DataSource = (object) this.dtDeliveryProgramation;
          this.dlProgramationDelivery.DataBind();
          if (this.dtDeliveryProgramation.Rows[0]["v_PaymentCodeNew"].ToString() == "")
          {
            this.dlProgramationDelivery.Controls[0].Controls[11].FindControl("CodPagoNewRequirement").Visible = false;
            this.dlProgramationDelivery.Controls[0].Controls[11].FindControl("Bank").Visible = false;
            this.dlProgramationDelivery.Controls[0].Controls[11].FindControl("PriceTotal").Visible = false;
            this.dlProgramationDelivery.Controls[0].Controls[11].FindControl("BankOperationDate").Visible = false;
            this.dlProgramationDelivery.Controls[0].Controls[11].FindControl("fechaRegistro").Visible = false;
            this.dlProgramationDelivery.Controls[0].Controls[11].FindControl("UsuarioRegistro").Visible = false;
          }
          else
          {
            this.dlProgramationDelivery.Controls[0].Controls[11].FindControl("CodPagoNewRequirement").Visible = true;
            this.dlProgramationDelivery.Controls[0].Controls[11].FindControl("Bank").Visible = true;
            this.dlProgramationDelivery.Controls[0].Controls[11].FindControl("PriceTotal").Visible = true;
            this.dlProgramationDelivery.Controls[0].Controls[11].FindControl("BankOperationDate").Visible = true;
            this.dlProgramationDelivery.Controls[0].Controls[11].FindControl("fechaRegistro").Visible = true;
            this.dlProgramationDelivery.Controls[0].Controls[11].FindControl("UsuarioRegistro").Visible = true;
          }
        }
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    public void setRoleAccess(ref DataTable dtRequirement)
    {
      dtRequirement.Columns.Add("Visible", Type.GetType("System.Boolean"));
      dtRequirement.Columns.Add("VisibleUpdated", Type.GetType("System.Boolean"));
      string str1 = ConfigDA.ReadConfig("RolesAccess");
      string str2 = ConfigDA.ReadConfig("RolesAccessUpdated");
      string[] strArray1 = str1.Split('|');
      string[] strArray2 = str2.Split('|');
      this.objUserBE = this.Session["SystemUser"] as SIIV.BE.SystemUser;
      int role = new SystemUserQueriesBL().SystemUserGetRole(this.objUserBE.i_SystemUserId, (int) Convert.ToInt16(ConfigurationManager.AppSettings["ApplicationId"], (IFormatProvider) CultureInfo.CurrentCulture));
      bool flag1 = false;
      for (int index = 0; index < strArray1.Length; ++index)
      {
        if (Convert.ToString(role, (IFormatProvider) CultureInfo.CurrentCulture) == strArray1[index])
        {
          flag1 = true;
          break;
        }
      }
      for (int index = 0; index < dtRequirement.Rows.Count; ++index)
        dtRequirement.Rows[index]["Visible"] = (object) true;
      if (!flag1)
      {
        for (int index = 0; index < dtRequirement.Rows.Count; ++index)
          dtRequirement.Rows[index]["Visible"] = (object) false;
      }
      bool flag2 = false;
      for (int index = 0; index < strArray2.Length; ++index)
      {
        if (Convert.ToString(role, (IFormatProvider) CultureInfo.CurrentCulture) == strArray2[index])
        {
          flag2 = true;
          break;
        }
      }
      for (int index = 0; index < dtRequirement.Rows.Count; ++index)
        dtRequirement.Rows[index]["VisibleUpdated"] = (object) true;
      if (flag2)
        return;
      for (int index = 0; index < dtRequirement.Rows.Count; ++index)
        dtRequirement.Rows[index]["VisibleUpdated"] = (object) false;
    }

    public void showProofPaperData(int Requirement, int RequirementPlateId)
    {
      this.HfRequirementId.Value = Convert.ToString(Requirement, (IFormatProvider) CultureInfo.CurrentCulture);
      this.HfRequirementPlateId.Value = Convert.ToString(RequirementPlateId, (IFormatProvider) CultureInfo.CurrentCulture);
      this.dtProofPaper = new RequirementQueriesBL().GetProofPaperDatabyRequirement(RequirementPlateId);
      this.TxtDocNumberProofPaper.Text = this.dtProofPaper.Rows[0]["v_DocumentNumber"].ToString();
      string str = this.dtProofPaper.Rows[0]["i_DeliveryPointId"].ToString();
      this.ViewState["i_DeliveryPointId"] = (object) str;
      if (str == "53")
      {
        this.wddDeliveryPoint.SelectedValue = "14";
        this.wddDeliveryPoint.Enabled = false;
      }
      if (this.dtProofPaper.Rows[0]["i_ProofPaymentTypeId"].ToString() != "0")
      {
        this.rdbProofPayment.SelectedValue = this.dtProofPaper.Rows[0]["i_ProofPaymentTypeId"].ToString();
      }
      else
      {
        this.rdbProofPayment.Enabled = false;
        this.TxtDocNumberProofPaper.Text = "0";
      }
      if (this.dtProofPaper.Rows[0]["i_DocumentTypeId"].ToString() != "0")
        this.wddDocumentType.SelectedValue = this.dtProofPaper.Rows[0]["i_DocumentTypeId"].ToString() == "" ? "4" : this.dtProofPaper.Rows[0]["i_DocumentTypeId"].ToString();
      else
        this.wddDocumentType.Enabled = false;
      if (this.wddDocumentType.SelectedValue != "4")
      {
        this.wddDocumentType.Enabled = true;
        this.wddDocumentType.Items.FindByValue("4").Enabled = false;
      }
      else
        this.wddDocumentType.Items.FindByValue("4").Enabled = true;
      this.txtBeneficiaryName.Text = this.dtProofPaper.Rows[0]["v_CompleteName"].ToString();
      this.txtAddress.Text = this.dtProofPaper.Rows[0]["v_Address"].ToString();
      this.lblFinalMessage.Text = "";
      this.lblFinalMessage.Visible = false;
    }

    private byte[] ImagenBarCode(string _NroPlaca)
    {
      if (!(_NroPlaca.Trim() != ""))
        return (byte[]) null;
      Barcode barcode = new Barcode();
      AlignmentPositions alignmentPositions = AlignmentPositions.CENTER;
      MemoryStream memoryStream = new MemoryStream();
      int int32_1 = Convert.ToInt32(300);
      int int32_2 = Convert.ToInt32(150);
      TYPE type = TYPE.CODE128;
      if (type != 0)
      {
        barcode.IncludeLabel = false;
        barcode.Alignment = alignmentPositions;
        barcode.Encode(type, _NroPlaca, Color.Black, Color.White, int32_1, int32_2);
        SaveTypes saveTypes = SaveTypes.JPG;
        barcode.SaveImage((Stream) memoryStream, saveTypes);
      }
      byte[] numArray = new byte[memoryStream.Length];
      return memoryStream.GetBuffer();
    }

    private bool ValidateProofPaymentData()
    {
      bool flag = true;
      this.lblFinalMessage.Visible = false;
      if (this.txtBeneficiaryName.Text == "")
      {
        this.lblFinalMessage.Visible = true;
        SIIV.Common.Resource.Message.SetMessage(this.lblFinalMessage, enmMessageType.Warning, "***Advertencia</br>INGRESE NOMBRES O RAZON SOCIAL DEL COMPROBANTE");
        flag = false;
      }
      else if (this.wddDocumentType.DataTextField == "")
      {
        this.lblFinalMessage.Visible = true;
        SIIV.Common.Resource.Message.SetMessage(this.lblFinalMessage, enmMessageType.Warning, "***Advertencia</br>SELECCIONE TIPO DE DOCUMENTO DE IDENTIDAD");
        flag = false;
      }
      else if (this.TxtDocNumberProofPaper.Text.Trim() == "")
      {
        this.lblFinalMessage.Visible = true;
        SIIV.Common.Resource.Message.SetMessage(this.lblFinalMessage, enmMessageType.Warning, "***Advertencia</br>INGRESE NUMERO DE DOCUMENTO DEL COMPROBANTE");
        flag = false;
      }
      else if (this.wddDocumentType.SelectedValue == "1" && this.TxtDocNumberProofPaper.Text.Trim().Length < 8)
      {
        this.lblFinalMessage.Visible = true;
        SIIV.Common.Resource.Message.SetMessage(this.lblFinalMessage, enmMessageType.Warning, "***Advertencia</br>DNI DEBE TENER 8 DIGITOS");
        flag = false;
      }
      else if (this.wddDocumentType.SelectedValue == "4" && this.TxtDocNumberProofPaper.Text.Trim().Length < 11)
      {
        this.lblFinalMessage.Visible = true;
        SIIV.Common.Resource.Message.SetMessage(this.lblFinalMessage, enmMessageType.Warning, "***Advertencia</br>RUC INGRESADO NO ES VALIDO LONGITUD INCORRECTA");
        flag = false;
      }
      else if (this.wddDocumentType.SelectedValue == "4" && this.TxtDocNumberProofPaper.Text.Trim().Substring(0, 1) != "1" && this.TxtDocNumberProofPaper.Text.Trim().Substring(0, 1) != "2")
      {
        this.lblFinalMessage.Visible = true;
        SIIV.Common.Resource.Message.SetMessage(this.lblFinalMessage, enmMessageType.Warning, "***Advertencia</br>RUC INGRESADO NO ES VALIDO DEBE INICIARCE EN 1 Ó 2");
        flag = false;
      }
      else if (this.wddDocumentType.SelectedValue == "4" && !this.ValidateRuc())
      {
        this.lblFinalMessage.Visible = true;
        SIIV.Common.Resource.Message.SetMessage(this.lblFinalMessage, enmMessageType.Warning, "***Advertencia</br>LA ESTRUCTURA DEL NUMERO DE R.U.C. INGRESADO NO ES CORRECTO");
        flag = false;
      }
      else if (this.wddDeliveryPoint.SelectedIndex == 0)
      {
        this.lblFinalMessage.Visible = true;
        SIIV.Common.Resource.Message.SetMessage(this.lblFinalMessage, enmMessageType.Warning, "***Advertencia</br>SELECCIONE PUNTO DE ENTREGA");
        flag = false;
      }
      else if (this.TxtDocNumberProofPaper.Text.Trim() == "20101973922")
      {
        this.lblFinalMessage.Visible = true;
        SIIV.Common.Resource.Message.SetMessage(this.lblFinalMessage, enmMessageType.Warning, "***Advertencia</br>NO SE PUEDE REALIZAR TRAMITES CON ESTE NUMERO DE R.U.C.");
        flag = false;
      }
      else if (!this.RestrictionRazonSocial())
      {
        this.lblFinalMessage.Visible = true;
        SIIV.Common.Resource.Message.SetMessage(this.lblFinalMessage, enmMessageType.Warning, "***Advertencia</br>ESTA RESTRINGIDA LA CREACION DE TRAMITES CON ESTA RAZON SOCIAL");
        flag = false;
      }
      else
      {
        this.dtRequirementData = new RequirementQueriesBL().GetRequirementPlateDatabyId(Convert.ToInt32(this.HfRequirementPlateId.Value, (IFormatProvider) CultureInfo.CurrentCulture));
        if (Convert.ToInt32(this.dtRequirementData.Rows[0]["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) != 0 && Convert.ToInt32(this.dtRequirementData.Rows[0]["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) != 1)
        {
          this.lblFinalMessage.Visible = true;
          SIIV.Common.Resource.Message.SetMessage(this.lblFinalMessage, enmMessageType.Warning, "***Advertencia</br>LA SOLICITUD YA NO PUEDE SER EDITADA");
          flag = false;
        }
      }
      return flag;
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

    public void SaveProofPaperData()
    {
      this.objUserBE = this.Session["SystemUser"] as SIIV.BE.SystemUser;
      int pintProofPaperTypeId = 0;
      if (this.rdbProofPayment.Enabled)
        pintProofPaperTypeId = Convert.ToInt32(this.rdbProofPayment.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      int int32 = Convert.ToInt32(this.wddDocumentType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      int pintDeliveryPoint = !(this.ViewState["i_DeliveryPointId"].ToString() == "53") ? Convert.ToInt32(this.wddDeliveryPoint.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) : Convert.ToInt32(this.ViewState["i_DeliveryPointId"].ToString());
      if (new RequirementManagementBL().UpdateProofPaperData(Convert.ToInt32(this.HfRequirementId.Value, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.HfRequirementPlateId.Value, (IFormatProvider) CultureInfo.CurrentCulture), pintProofPaperTypeId, "", int32, this.TxtDocNumberProofPaper.Text.Trim(), this.txtBeneficiaryName.Text.Trim(), this.txtAddress.Text.Trim(), pintDeliveryPoint, this.objUserBE.i_SystemUserId) == 1)
        SIIV.Common.Resource.Message.SetMessage(this.lblFinalMessage, enmMessageType.Success, "***Exito</br>La datos han sido registrados correctamente");
      else
        SIIV.Common.Resource.Message.SetMessage(this.lblFinalMessage, enmMessageType.Warning, "***Error</br>Ha ocurrido un error,Por favor intente nuevamente");
    }

    public string FormatDate(DateTime pdateData)
    {
      return pdateData.ToString("dd/MM/yyyy", (IFormatProvider) CultureInfo.CurrentCulture);
    }

    protected void wdgList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void wibEBilling_Click(object sender, EventArgs e)
    {
      string v_Url = "";
      string v_Estado = "";
      string str = "";
      string empty = string.Empty;
      str = new RequirementManagementBL().EBillingUrl(0, Convert.ToInt32(this.ViewState["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture), out v_Url, out v_Estado);
      if (int.TryParse(v_Estado, out int _))
      {
        if (Convert.ToInt32(v_Estado) != 100)
        {
          string script = "OpenUrl('" + ("../Public/EBilling.aspx?" + v_Url) + "');";
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        }
        else
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", "AlertaUrl();", true);
      }
      else if (v_Estado != "C")
      {
        string script = "OpenUrl('" + ("../Public/EBilling.aspx?Url=" + v_Url) + "');";
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
      else
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", "AlertaUrl();", true);
    }

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void volver_Click(object sender, EventArgs e)
    {
      string script = UtilDA.ActiveTabIndex("SubTabs", 1, "0,2,3");
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void Enviar_Click(object sender, EventArgs e)
    {
      this.lblMensaje.Visible = false;
      if (this.txtEmail.Text != "")
      {
        this.sendEmail();
        string script = UtilDA.ActiveTabIndex("SubTabs", 3, "0,1,2");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
      else
      {
        this.lblMensaje.Visible = true;
        SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(1, "Debe ingresar una direccion de Email"));
        string script = UtilDA.ActiveTabIndex("SubTabs", 3, "0,1,2");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
    }

    protected void EnviarCur_Click(object sender, EventArgs e)
    {
      this.lblMensaje.Visible = false;
      if (this.txtEmail.Text != "")
      {
        this.sendCurByEmail();
        string script = UtilDA.ActiveTabIndex("SubTabs", 3, "0,1,2");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
      else
      {
        this.lblMensaje.Visible = true;
        SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(1, "Debe ingresar una direccion de Email"));
        string script = UtilDA.ActiveTabIndex("SubTabs", 3, "0,1,2");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
    }

    protected void SendMail_Click(object sender, EventArgs e)
    {
      this.lblMensaje.Visible = false;
      string v_Url = "";
      string v_Estado = "";
      string str = "";
      str = new RequirementManagementBL().EBillingUrl(0, Convert.ToInt32(this.ViewState["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture), out v_Url, out v_Estado);
      bool flag = int.TryParse(v_Estado, out int _);
      this.EnviarCur.Visible = false;
      this.Enviar.Visible = true;
      if (flag)
      {
        if (Convert.ToInt32(v_Estado) != 100)
        {
          string script = UtilDA.ActiveTabIndex("SubTabs", 3, "0,1,2");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
          this.txtEmail.Text = (string) this.ViewState["v_beneficiaryEmail"];
        }
        else
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", "AlertaEmail();", true);
      }
      else if (v_Estado != "C")
      {
        string script = UtilDA.ActiveTabIndex("SubTabs", 3, "0,1,2");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        this.txtEmail.Text = (string) this.ViewState["v_beneficiaryEmail"];
      }
      else
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", "AlertaEmail();", true);
    }

    protected void SendCurByMail_Click(object sender, EventArgs e)
    {
      if (this.GenerateCurReport() == null)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(1, "No se pudo generar el CUR."));
      }
      else
      {
        this.EnviarCur.Visible = true;
        this.Enviar.Visible = false;
        string script = UtilDA.ActiveTabIndex("SubTabs", 3, "0,1,2");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        this.txtEmail.Text = (string) this.ViewState["v_beneficiaryEmail"];
      }
    }

    protected void sendCurByEmail()
    {
      string strFile = ConfigurationManager.AppSettings["HandledLog"] + "EmaiLog_" + DateTime.Today.ToString("yyyyMMdd") + ".log";
      bool boolean1 = Convert.ToBoolean(string.IsNullOrEmpty(ConfigurationManager.AppSettings["SaveLogEmail"]) ? "false" : (ConfigurationManager.AppSettings["SaveLogEmail"] == "1" ? "true" : "false"));
      if (boolean1)
        SIIV.Common.Resource.Utilities.Logging.WriteFileLog(strFile, "sendCurByEmail - Inicio de envío de correo", enmFileSection.Header);
      try
      {
        DataTable dataTable1 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.SMTPServerConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture),
          (object) "",
          (object) "1",
          (object) "1"
        });
        string str1 = dataTable1.Rows[0]["v_Value"].ToString();
        int num = int.Parse(dataTable1.Rows[1]["v_Value"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        string userName = dataTable1.Rows[2]["v_Value"].ToString();
        string password = dataTable1.Rows[3]["v_Value"].ToString();
        bool boolean2 = Convert.ToBoolean(dataTable1.Rows[4]["v_Value"], (IFormatProvider) CultureInfo.CurrentCulture);
        DataTable dataTable2 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.RequerimentCUREmailConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture),
          (object) "",
          (object) "1",
          (object) "1"
        });
        string str2 = dataTable2.Rows[0]["v_Value"].ToString();
        string str3 = dataTable2.Rows[1]["v_Value"].ToString();
        string address = dataTable2.Rows[2]["v_Value"].ToString();
        string str4 = dataTable2.Rows[3]["v_Value"].ToString();
        string addresses = this.txtEmail.Text.Trim();
        if (boolean1)
          SIIV.Common.Resource.Utilities.Logging.WriteFileLog(strFile, "sendCurByEmail - Enviando correo a: " + addresses, enmFileSection.Content);
        dataTable2.Dispose();
        ReportDocument curReport = this.GenerateCurReport();
        if (curReport == null)
        {
          SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(1, "No se pudo generar el CUR."));
        }
        else
        {
          string str5 = Path.Combine(Path.GetTempPath(), "Certificado_Unico_Registro.pdf");
          curReport.ExportToDisk(ExportFormatType.PortableDocFormat, str5);
          curReport.Close();
          ((Component) curReport).Dispose();
          Attachment attachment = new Attachment(str5);
          MailMessage message = new MailMessage();
          message.From = new MailAddress(address);
          message.To.Add(addresses);
          message.Subject = str2;
          message.Body = str3 + str4;
          message.IsBodyHtml = true;
          message.Priority = MailPriority.Normal;
          ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
          SmtpClient smtpClient = new SmtpClient()
          {
            Host = str1,
            UseDefaultCredentials = false,
            EnableSsl = boolean2,
            Port = num,
            Credentials = (ICredentialsByHost) new NetworkCredential(userName, password)
          };
          message.Attachments.Add(attachment);
          smtpClient.Send(message);
          SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(2, "El CUR fue enviado correctamente."));
          smtpClient.Dispose();
          message.Dispose();
          attachment.Dispose();
          System.IO.File.Delete(str5);
          if (!boolean1)
            return;
          SIIV.Common.Resource.Utilities.Logging.WriteFileLog(strFile, "sendCurByEmail - Correo enviado correctamente", enmFileSection.Content);
        }
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Utilities.Logging.WriteFileLog(strFile, "sendCurByEmail - Error en el envío - " + ex.Message, enmFileSection.Content);
        SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Utilities.Logging.WriteFileLog(strFile, "sendCurByEmail - Error inesperado - " + ex.Message, enmFileSection.Content);
        SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(-100, ex));
      }
    }

    protected void sendEmail()
    {
      DataTable dataTable1 = new DataTable();
      try
      {
        ProductWarehouseQueriesBL warehouseQueriesBl = new ProductWarehouseQueriesBL();
        DataTable dataTable2 = new DataTable();
        DataTable dataTable3 = warehouseQueriesBl.RequirementSendMailEbilling(Convert.ToInt32(this.ViewState["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture));
        Email email = new Email();
        bool flag = false;
        DataTable dataTable4 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) "990",
          (object) "",
          (object) "1",
          (object) "1"
        });
        string str1 = dataTable4.Rows[0]["v_Value"].ToString();
        string str2 = dataTable4.Rows[1]["v_Value"].ToString();
        dataTable4.Rows[3]["v_Value"].ToString();
        string str3 = dataTable4.Rows[4]["v_Value"].ToString();
        string pstrSMTPServer = dataTable4.Rows[5]["v_Value"].ToString();
        int pintSMTPPort = int.Parse(dataTable4.Rows[6]["v_Value"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        string str4 = dataTable4.Rows[7]["v_Value"].ToString();
        string pstrSMTPPassword = dataTable4.Rows[8]["v_Value"].ToString();
        string str5 = dataTable4.Rows[9]["v_Value"].ToString();
        string str6 = dataTable4.Rows[10]["v_Value"].ToString();
        bool boolean1 = Convert.ToBoolean(dataTable4.Rows[11]["v_Value"], (IFormatProvider) CultureInfo.CurrentCulture);
        bool boolean2 = Convert.ToBoolean(dataTable4.Rows[12]["v_Value"], (IFormatProvider) CultureInfo.CurrentCulture);
        bool boolean3 = Convert.ToBoolean(dataTable4.Rows[13]["v_Value"], (IFormatProvider) CultureInfo.CurrentCulture);
        if (dataTable3 == null)
          return;
        dataTable1 = new DataTable();
        string pstrEmailSubject = str1;
        DataTable dataTable5 = dataTable3;
        if (dataTable5.Rows.Count > 0)
        {
          StringBuilder stringBuilder = new StringBuilder();
          string str7 = str2;
          string newValue1 = dataTable5.Rows[0]["Nombre"].ToString();
          string TipoComprobante = dataTable5.Rows[0]["TipoDocumento"].ToString();
          string newValue2 = dataTable5.Rows[0]["TipoDocumentoName"].ToString();
          string newValue3 = dataTable5.Rows[0]["DocumentoElectronico"].ToString();
          string newValue4 = dataTable5.Rows[0]["RazonSocial"].ToString();
          string NumeroDocumentoIdentidad = dataTable5.Rows[0]["NumeroDocumento"].ToString();
          DateTime dateTime = Convert.ToDateTime(dataTable5.Rows[0]["FechaEmision"]);
          string newValue5 = dataTable5.Rows[0]["TotalMontoPagar"].ToString().Replace(",", ".");
          string str8 = dataTable5.Rows[0]["CodigoMoneda"].ToString();
          string str9 = this.txtEmail.Text.Trim();
          DateTime date1 = Convert.ToDateTime(dateTime.ToString("dd-MM-yyyy")).Date;
          DateTime date2 = Convert.ToDateTime(ConfigurationManager.AppSettings["DateMigrationSoftnet"].ToString()).Date;
          List<string> plstAttachments = new List<string>();
          string str10 = str7.Replace("#CLIENTENOMBRE#", newValue1).Replace("#FECHA#", dateTime.ToString("dd-MM-yyyy HH:mm")).Replace("#EMISOR#", newValue4).Replace("#TIPODOCUMENTO#", newValue2).Replace("#NRODOCUMENTO#", newValue3).Replace("#TOTAL#", newValue5);
          string tempPath = Path.GetTempPath();
          string str11 = NumeroDocumentoIdentidad + "-" + TipoComprobante + "-" + newValue3 + ".xml";
          string str12 = NumeroDocumentoIdentidad + "-" + TipoComprobante + "-" + newValue3 + ".pdf";
          string path1 = tempPath + str11;
          string path2 = tempPath + str12;
          string newValue6;
          if (date1 <= date2)
          {
            string address1 = str3 + TipoComprobante + "-" + newValue3 + "?ruc=" + NumeroDocumentoIdentidad + "&fecha=" + dateTime.ToString("dd-MM-yyyy") + "&monto=" + newValue5 + "&moneda=" + str8;
            string address2 = ConfigurationManager.AppSettings["EBillingUrlXml"].ToString() + TipoComprobante + "-" + newValue3 + "?ruc=" + NumeroDocumentoIdentidad + "&fecha=" + dateTime.ToString("dd-MM-yyyy") + "&monto=" + newValue5 + "&moneda=" + str8;
            newValue6 = str6 + TipoComprobante + "-" + newValue3 + "?ruc=" + NumeroDocumentoIdentidad + "&fecha=" + dateTime.ToString("dd-MM-yyyy") + "&monto=" + newValue5 + "&moneda=" + str8;
            byte[] buffer1 = new WebClient().DownloadData(address2);
            if (!System.IO.File.Exists(path1))
            {
              using (FileStream fileStream = System.IO.File.Create(path1))
                fileStream.Write(buffer1, 0, buffer1.Length);
            }
            plstAttachments.Add(path1);
            byte[] buffer2 = new WebClient().DownloadData(address1);
            if (!System.IO.File.Exists(path2))
            {
              using (FileStream fileStream = System.IO.File.Create(path2))
                fileStream.Write(buffer2, 0, buffer2.Length);
            }
            plstAttachments.Add(path2);
          }
          else
          {
            newValue6 = ConfigurationManager.AppSettings["EBillingUrlNewProvider"].ToString() + "serie=" + dataTable5.Rows[0]["serie"].ToString() + "&correlativo=" + dataTable5.Rows[0]["correlativo"].ToString() + "&tipoComprobante=" + TipoComprobante + "&ruc=" + NumeroDocumentoIdentidad + "&tipodocumento=" + dataTable5.Rows[0]["tipodocumentoemisor"].ToString();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            MercurioWS mercurioWs = new MercurioWS();
            CPE_DOC_TRIBUTARO_BE cpeDocTributaroBe1 = new CPE_DOC_TRIBUTARO_BE();
            CPE_DOC_TRIBUTARO_BE cpeDocTributaroBe2 = mercurioWs.ReadDocumentCPE(ConfigurationManager.AppSettings["UserServiceSofnet"].ToString(), ConfigurationManager.AppSettings["PasswordServiceSofnet"].ToString(), dataTable5.Rows[0]["serie"].ToString(), dataTable5.Rows[0]["correlativo"].ToString(), TipoComprobante, NumeroDocumentoIdentidad, dataTable5.Rows[0]["tipodocumentoemisor"].ToString(), true, false, true);
            if (!System.IO.File.Exists(path1))
              System.IO.File.WriteAllText(path1, cpeDocTributaroBe2.DOC_TRIB_XML_ENVIO, Encoding.GetEncoding(28591));
            plstAttachments.Add(path1);
            byte[] docTribPdf = cpeDocTributaroBe2.DOC_TRIB_PDF;
            if (!System.IO.File.Exists(path2))
            {
              using (FileStream fileStream = System.IO.File.Create(path2))
                fileStream.Write(docTribPdf, 0, docTribPdf.Length);
            }
            plstAttachments.Add(path2);
          }
          string str13 = str10.Replace("#URL#", newValue6);
          string empty = string.Empty;
          if (ConfigurationManager.AppSettings["File_Advertising"] != null)
          {
            string path3 = ConfigurationManager.AppSettings["File_Advertising"].ToString().Trim();
            if (System.IO.File.Exists(path3))
              plstAttachments.Add(path3);
          }
          stringBuilder.AppendLine(str13);
          int num = 0;
          List<string> pstrEmailTo = new List<string>();
          if (str9 != null)
          {
            string str14 = str9;
            char[] chArray = new char[1]{ ';' };
            foreach (string str15 in str14.Split(chArray))
            {
              if (!string.IsNullOrEmpty(str15))
              {
                string pattern = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                if (Regex.IsMatch(str15.ToString(), pattern))
                {
                  pstrEmailTo.Add(str15.ToString());
                }
                else
                {
                  SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(1, "El email" + str15.ToString() + "No tiene la estructura correcta"));
                  num = -1;
                }
              }
            }
          }
          string pstrEmailBody = stringBuilder.ToString();
          List<string> pstrEmailCC = new List<string>();
          if (str5 != null)
          {
            string str16 = str5;
            char[] chArray = new char[1]{ '|' };
            foreach (string str17 in str16.Split(chArray))
            {
              if (!string.IsNullOrEmpty(str17))
                pstrEmailCC.Add(str17.ToString());
            }
          }
          if (num != -1)
            flag = SIIV.Common.Resource.Utilities.Mail.SendEmail(pstrSMTPServer, pintSMTPPort, str4, pstrSMTPPassword, boolean1, str4, pstrEmailTo, pstrEmailCC, (List<string>) null, pstrEmailSubject, pstrEmailBody, boolean2, boolean3, plstAttachments);
          if (!flag)
          {
            if (num == -1)
              SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(1, "El email No tiene la estructura correcta"));
            else
              SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(1, "Error al enviar en Documento Electronico"));
          }
          else
            SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(2, "El Documento Electronico fue enviado correctamente"));
        }
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(-100, ex));
      }
      finally
      {
      }
    }

    protected void BtnDelivery_Click(object sender, EventArgs e)
    {
      this.Session["RequirementPlateId"] = (object) Convert.ToInt32(this.ViewState["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture);
    }
  }
}
