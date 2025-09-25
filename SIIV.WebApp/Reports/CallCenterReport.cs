// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Reports.CallCenterReport
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.Common.Resource;
using SIIV.Reports.BL;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Reports
{
  public class CallCenterReport : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtPlateNumber;
    protected FilteredTextBoxExtender txtPlateNumber_FilteredTextBoxExtender;
    protected TextBox txtApplicationNumber;
    protected Button wibSearch;
    protected Button wibRefresh;
    protected Label lblMessage;
    protected TextBox txtPlateNew;
    protected TextBox txtPlateOld;
    protected TextBox txtTitleNumber;
    protected TextBox txtOwner;
    protected TextBox txtDocumentOwner;
    protected TextBox txtMark;
    protected TextBox txtModel;
    protected TextBox txtCategory;
    protected Fecha wdpDispatchDate;
    protected TextBox txtRegistryZone;
    protected TextBox txtRegistryOffice;
    protected TextBox txtTypeVehicleUse;
    protected TextBox txtChassisCode;
    protected Label Label5;
    protected TextBox txtBeginProcessed;
    protected Label Label1;
    protected TextBox txtApplicant;
    protected Label Label2;
    protected TextBox txtDocumentApplicant;
    protected TextBox txtBeneficiary;
    protected TextBox txtDocumentTypeBeneficiary;
    protected TextBox txtDocumentBeneficiary;
    protected TextBox txtAddressBeneficiary;
    protected Fecha wdpRegisterDate;
    protected Fecha wdpDischargeDate;
    protected TextBox txtContingency;
    protected TextBox txtProcessed;
    protected TextBox txtDeliveryPoint;
    protected TextBox txtProduct;
    protected TextBox TxtEmail;
    protected TextBox txtBank;
    protected Fecha wdpPaymentDate;
    protected TextBox txtUserCode;
    protected Label Label6;
    protected TextBox txtPaymentCode;
    protected TextBox txtBatch;
    protected Label Label3;
    protected Fecha wdpEntryDate;
    protected TextBox txtDocumentNumber;
    protected TextBox txtDispatch;
    protected Fecha wdpBatchDispatchDate;
    protected Label Label4;
    protected TextBox txtGuide;
    protected Label Label7;
    protected TextBox txtBlankCode1;
    protected Label Label8;
    protected TextBox txtBlankCode2;
    protected Label lblMessageError;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnSearch_Click(object sender, EventArgs e) => this.LoadReport();

    protected void btnRefresh_Click(object sender, EventArgs e) => this.LoadReport();

    private void LoadReport()
    {
      if (this.txtPlateNumber.Text.Length == 0)
      {
        Message.SetMessage(this.lblMessageError, enmMessageType.Warning, "Debe ingresar el número de placa");
        this.HidePopup();
      }
      else
      {
        string v_platenumber = this.txtPlateNumber.Text.TrimEnd();
        string v_ApplicationNumber = this.txtApplicationNumber.Text.TrimEnd();
        DataTable dt = new DataTable();
        try
        {
          dt = new ReportManagementBL().GetCallCenterReport(v_platenumber, v_ApplicationNumber);
        }
        catch (Exception ex)
        {
          Message.SetMessage(this.lblMessageError, enmMessageType.Warning, ex.Message);
          this.HidePopup();
        }
        finally
        {
          this.HidePopup();
        }
        this.SetearControls(dt);
      }
    }

    private void SetearControls(DataTable dt)
    {
      this.lblMessageError.Visible = false;
      if (dt == null || dt.Rows.Count == 0)
      {
        Message.SetMessage(this.lblMessageError, enmMessageType.Warning, "No existe información con los datos ingresados");
        this.Clear();
        this.HidePopup();
      }
      else
      {
        DateTime? nullable = new DateTime?(new DateTime());
        this.lblMessage.Text = "Estado Solicitud: " + dt.Rows[0]["v_Message"].ToString();
        this.txtPlateNew.Text = dt.Rows[0]["v_PlateNew"].ToString();
        this.txtPlateOld.Text = dt.Rows[0]["v_PlateOld"].ToString();
        this.txtTitleNumber.Text = dt.Rows[0]["v_TitleNumber"].ToString();
        this.txtOwner.Text = dt.Rows[0]["v_OwnerCompleteName"].ToString();
        this.txtDocumentOwner.Text = dt.Rows[0]["v_OwnerDocumentNumber"].ToString();
        this.txtMark.Text = dt.Rows[0]["v_Brand"].ToString();
        this.txtModel.Text = dt.Rows[0]["v_Model"].ToString();
        this.txtCategory.Text = dt.Rows[0]["v_Category"].ToString();
        this.wdpDispatchDate.Value = Convert.ToDateTime(dt.Rows[0]["d_DispatchDate"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.txtRegistryZone.Text = dt.Rows[0]["v_RegistryZone"].ToString();
        this.txtRegistryOffice.Text = dt.Rows[0]["v_RegistryOffice"].ToString();
        this.txtTypeVehicleUse.Text = dt.Rows[0]["v_VehicleUseType"].ToString();
        this.txtChassisCode.Text = dt.Rows[0]["v_VinNumber"].ToString();
        this.txtBeginProcessed.Text = dt.Rows[0]["v_BeginProcessed"].ToString();
        this.txtApplicant.Text = dt.Rows[0]["v_ApplicantName"].ToString();
        this.txtDocumentApplicant.Text = dt.Rows[0]["v_ApplicantDocument"].ToString();
        this.txtBeneficiary.Text = dt.Rows[0]["v_NameBeneficiary"].ToString();
        this.txtDocumentTypeBeneficiary.Text = dt.Rows[0]["v_BeneficiaryDocumentType"].ToString();
        this.txtDocumentBeneficiary.Text = dt.Rows[0]["v_BeneficiaryDocumentNumber"].ToString();
        this.txtAddressBeneficiary.Text = dt.Rows[0]["v_BeneficiaryAddres"].ToString();
        this.wdpRegisterDate.Value = Convert.ToDateTime(dt.Rows[0]["d_RegisterDate"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.wdpDischargeDate.Value = Convert.IsDBNull(dt.Rows[0]["d_DischargeDate"]) ? nullable.Value : Convert.ToDateTime(dt.Rows[0]["d_DischargeDate"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.txtContingency.Text = dt.Rows[0]["v_Contingency"].ToString();
        this.txtProcessed.Text = dt.Rows[0]["v_Processed"].ToString();
        this.txtDeliveryPoint.Text = dt.Rows[0]["v_DeliveryPoint"].ToString();
        this.txtProduct.Text = dt.Rows[0]["v_ProductName"].ToString();
        this.TxtEmail.Text = dt.Rows[0]["v_Email"].ToString();
        this.txtBank.Text = dt.Rows[0]["v_BankName"].ToString();
        this.wdpPaymentDate.Value = Convert.IsDBNull(dt.Rows[0]["d_PaymentDate"]) ? nullable.Value : Convert.ToDateTime(dt.Rows[0]["d_PaymentDate"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.txtUserCode.Text = dt.Rows[0]["v_BankOperationUser"].ToString();
        this.txtPaymentCode.Text = dt.Rows[0]["v_PaymentCode"].ToString();
        this.txtBatch.Text = dt.Rows[0]["i_BatchId"].ToString();
        this.wdpEntryDate.Value = Convert.IsDBNull(dt.Rows[0]["d_BatchEntryDate"]) ? nullable.Value : Convert.ToDateTime(dt.Rows[0]["d_BatchEntryDate"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.txtDocumentNumber.Text = dt.Rows[0]["v_DocumentNumber"].ToString();
        this.txtDispatch.Text = dt.Rows[0]["i_DispatchId"].ToString();
        this.wdpBatchDispatchDate.Value = Convert.IsDBNull(dt.Rows[0]["d_BatchDispatchDate"]) ? nullable.Value : Convert.ToDateTime(dt.Rows[0]["d_BatchDispatchDate"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.txtGuide.Text = dt.Rows[0]["v_GuideNumber"].ToString();
        this.txtBlankCode1.Text = dt.Rows[0]["v_BlankCode1"].ToString();
        this.txtBlankCode2.Text = dt.Rows[0]["v_BlankCode2"].ToString();
      }
    }

    private void Clear()
    {
      DateTime? nullable = new DateTime?(new DateTime());
      this.lblMessage.Text = "";
      this.txtPlateNew.Text = "";
      this.txtPlateOld.Text = "";
      this.txtTitleNumber.Text = "";
      this.txtOwner.Text = "";
      this.txtDocumentOwner.Text = "";
      this.txtMark.Text = "";
      this.txtModel.Text = "";
      this.txtCategory.Text = "";
      this.wdpDispatchDate.Value = nullable.Value;
      this.txtRegistryZone.Text = "";
      this.txtRegistryOffice.Text = "";
      this.txtTypeVehicleUse.Text = "";
      this.txtChassisCode.Text = "";
      this.txtBeginProcessed.Text = "";
      this.txtApplicant.Text = "";
      this.txtDocumentApplicant.Text = "";
      this.txtBeneficiary.Text = "";
      this.txtDocumentTypeBeneficiary.Text = "";
      this.txtDocumentBeneficiary.Text = "";
      this.txtAddressBeneficiary.Text = "";
      this.wdpRegisterDate.Value = nullable.Value;
      this.wdpDischargeDate.Value = nullable.Value;
      this.txtContingency.Text = "";
      this.txtProcessed.Text = "";
      this.txtDeliveryPoint.Text = "";
      this.txtProduct.Text = "";
      this.TxtEmail.Text = "";
      this.txtBank.Text = "";
      this.wdpPaymentDate.Value = nullable.Value;
      this.txtUserCode.Text = "";
      this.txtPaymentCode.Text = "";
      this.txtBatch.Text = "";
      this.wdpEntryDate.Value = nullable.Value;
      this.txtDocumentNumber.Text = "";
      this.txtDispatch.Text = "";
      this.wdpBatchDispatchDate.Value = nullable.Value;
      this.txtGuide.Text = "";
      this.txtBlankCode1.Text = "";
      this.txtBlankCode2.Text = "";
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }
  }
}
