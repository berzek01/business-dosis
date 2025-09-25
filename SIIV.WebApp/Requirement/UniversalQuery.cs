// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Requirement.UniversalQuery
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.WebApp.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Requirement
{
  public class UniversalQuery : Page
  {
    private DataTable dtRequirement;
    private RSAParameters publicKey;
    private RSAParameters privateKey;
    protected UpdatePanel updatePanel;
    protected Label lblPlateNew;
    protected TextBox txtPlateNew;
    protected FilteredTextBoxExtender txtPlateNew_FilteredTextBoxExtender;
    protected CheckBox chkGenerationBetween;
    protected Fecha wdpStartDate;
    protected Fecha wdpEndDate;
    protected Label lblPlateOld;
    protected TextBox txtPlateOld;
    protected FilteredTextBoxExtender txtPlateOld_FilteredTextBoxExtender1;
    protected Label lblTitle;
    protected TextBox txtTitleNumber;
    protected Label lblRequirementId;
    protected TextBox txtRequirementPlateId;
    protected FilteredTextBoxExtender txtRequirementPlateId_FilteredTextBoxExtender1;
    protected Label lblOwner;
    protected TextBox txtOwner;
    protected Label lblCategoria;
    protected DropDownList wddCategory;
    protected Label lblTramite;
    protected DropDownList wddProcesstype;
    protected Label lblEstado;
    protected DropDownList wddStatus;
    protected Label lblVIN;
    protected TextBox txtSerial;
    protected Label lblCodPago;
    protected TextBox txtPaymentCode;
    protected Button wibSearch;
    protected Label lblMessage;
    protected GridView wdgList;
    protected HiddenField hdnQueryString;
    protected Pager custPagerUQR;
    protected Button wibPdf;
    protected Button wibExcel;
    protected Button BtnReqDelivery;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      this.Initialize();
    }

    protected void chkGenerationBetween_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkGenerationBetween.Checked)
      {
        this.wdpStartDate.Enabled = true;
        this.wdpEndDate.Enabled = true;
      }
      else
      {
        this.wdpStartDate.Enabled = false;
        this.wdpEndDate.Enabled = false;
      }
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        this.SearchUniversal();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void BtnReqDelivery_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.Session["RequirementPlateId"] != null)
        {
          int int32 = Convert.ToInt32(this.Session["RequirementPlateId"]);
          string str = (this.Session["ListRequirements"] as DataTable).Rows[0]["v_PlateNew"].ToString();
          string ErrorMessage = this.ValidateExistRequirement(int32);
          if (ErrorMessage.Length == 0)
          {
            this.Session["FromUniversalQuery"] = (object) 1;
            this.Server.MapPath(this.Request.ApplicationPath);
            this.Response.Redirect("~/Delivery/Query/BookQueryPlates.aspx?v_PlateNew=" + str, false);
            this.Session["RequirementPlateId"] = (object) null;
          }
          else
          {
            this.Session["RequirementPlateId"] = (object) null;
            throw new HandledException(1, ErrorMessage);
          }
        }
        this.Session["RequirementPlateId"] = (object) null;
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

    public string ValidateExistRequirement(int i_RequirementPlateId)
    {
      try
      {
        int? nullable1 = new int?();
        string str = "";
        nullable1 = new int?(new RequirementQueriesBL().ValidateExistRequirementDelivery(i_RequirementPlateId));
        int? nullable2 = nullable1;
        if (nullable2.HasValue)
        {
          switch (nullable2.GetValueOrDefault())
          {
            case -5:
              str = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Tramite_Delivery_Placa_Fabricada_Location;
              break;
            case -4:
              str = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Tramite_Delivery_Placa_Fabricada_Type_Request;
              break;
            case -3:
              str = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Tramite_Delivery_Placa_Fabricada_Status;
              break;
            case -2:
              str = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Tramite_Delivery_Placa_Fabricada_Terminado;
              break;
            case -1:
              str = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Tramite_Delivery_Curso;
              break;
            case 0:
              str = SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Tramite_Delivery_Placa_Fabricada_Curso;
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

    protected void custPagerUQR_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
        string pstrPlateNew = this.txtPlateNew.Text.Trim();
        int pintStartdate;
        int pintFinishdate;
        if (!this.wdpStartDate.Enabled && !this.wdpStartDate.Enabled)
        {
          pintStartdate = 0;
          pintFinishdate = 0;
        }
        else
        {
          pintStartdate = Convert.ToInt32(this.wdpStartDate.Value.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture));
          pintFinishdate = Convert.ToInt32(this.wdpEndDate.Value.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        }
        string pstrPlateOld = this.txtPlateOld.Text.Trim();
        string pstrTitleNumber = this.txtTitleNumber.Text.Trim();
        int pintRequirementPlateId = -1;
        if (this.txtRequirementPlateId.Text.Trim() != "")
          pintRequirementPlateId = Convert.ToInt32(this.txtRequirementPlateId.Text.Trim(), (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrOwnerName = this.txtOwner.Text.Trim();
        int int32_1 = Convert.ToInt32(this.wddCategory.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        int int32_2 = Convert.ToInt32(this.wddProcesstype.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        int pintStatus = -3;
        if (this.wddStatus.SelectedIndex != 0)
          pintStatus = Convert.ToInt32(this.wddStatus.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrSerial = this.txtSerial.Text.Trim();
        string pstrPaymentCode = this.txtPaymentCode.Text.Trim();
        int iSystemUserId = systemUser.i_SystemUserId;
        int int32_3 = Convert.ToInt32(this.ViewState["t"], (IFormatProvider) CultureInfo.CurrentCulture);
        int int32_4 = Convert.ToInt32(this.ViewState["i_platetypeId"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.SearchUniversalList(pstrPlateNew, pintStartdate, pintFinishdate, pstrPlateOld, pstrTitleNumber, pintRequirementPlateId, pstrOwnerName, int32_1, int32_2, pintStatus, pstrSerial, pstrPaymentCode, iSystemUserId, int32_3, int32_4, false);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void wdgList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        if (!e.CommandName.Equals("getData", StringComparison.CurrentCulture))
          return;
        ImageButton imageButton = sender as ImageButton;
        string str1 = this.Request.QueryString["t"];
        string text = this.wdgList.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
        int i_ActionId = 1;
        string empty = string.Empty;
        DataTable conciliacionPlate = new RequirementQueriesBL().GetConciliacionPlate(text, i_ActionId);
        string str2 = this.EncryptQueryString(string.Format("&quot;&quot;&quot;&quot;&plate={0}&t={1}", (object) text, (object) str1));
        if (!Convert.ToBoolean(conciliacionPlate.Rows[0]["b_HasConciliation"]) && !Convert.ToBoolean(conciliacionPlate.Rows[0]["b_HasPlateBlock"]))
        {
          this.hdnQueryString.Value = str2;
          this.CreatePopUpConciliation("../../UserControls/PopupAnnouncement.aspx?MessageTypeId=1&MessageText=Nro de Placa no tiene Conciliación Bancaria.", "SIIV - Placas sin Conciliación", "430px", "165px");
        }
        else if (!Convert.ToBoolean(conciliacionPlate.Rows[0]["b_HasConciliation"]) && Convert.ToBoolean(conciliacionPlate.Rows[0]["b_HasPlateBlock"]))
        {
          this.hdnQueryString.Value = str2;
          this.CreatePopUpConciliation("../../UserControls/PopupAnnouncement.aspx?MessageTypeId=1&MessageText=Nro de Placa no tiene Conciliación Bancaria.{0}Nro de Placa se encuentra bloqueada.", "SIIV - Advertencia", "430px", "205px");
        }
        else if (Convert.ToBoolean(conciliacionPlate.Rows[0]["b_HasConciliation"]) && Convert.ToBoolean(conciliacionPlate.Rows[0]["b_HasPlateBlock"]))
        {
          this.hdnQueryString.Value = str2;
          this.CreatePopUpConciliation("../../UserControls/PopupAnnouncement.aspx?MessageTypeId=1&MessageText=Nro de Placa se encuentra bloqueada.", "SIIV - Bloqueo de Placas", "430px", "165px");
        }
        else
          this.CreatePopUp("../Requirement/RequirementData.aspx?" + str2, "Datos de la Placa", "1100", "820");
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

    public void AssignNewKey()
    {
      using (RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider())
      {
        cryptoServiceProvider.PersistKeyInCsp = false;
        this.publicKey = cryptoServiceProvider.ExportParameters(false);
        this.privateKey = cryptoServiceProvider.ExportParameters(true);
      }
    }

    public byte[] SignData(byte[] hashOfDataToSign)
    {
      using (RSACryptoServiceProvider key = new RSACryptoServiceProvider())
      {
        key.PersistKeyInCsp = false;
        key.ImportParameters(this.privateKey);
        RSAPKCS1SignatureFormatter signatureFormatter = new RSAPKCS1SignatureFormatter((AsymmetricAlgorithm) key);
        signatureFormatter.SetHashAlgorithm("SHA256");
        return signatureFormatter.CreateSignature(hashOfDataToSign);
      }
    }

    public string EncryptQueryString(string strQueryString)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(strQueryString);
      byte[] hash;
      using (SHA256 shA256 = SHA256.Create())
        hash = shA256.ComputeHash(bytes);
      this.AssignNewKey();
      string base64String = Convert.ToBase64String(this.SignData(hash));
      return new Encryption().Encrypt(strQueryString, base64String) + "%" + base64String;
    }

    protected void wdgList_PageIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.SearchUniversal();
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

    protected void wibExportExcel_Click(object sender, EventArgs e)
    {
      try
      {
        this.ExportList();
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

    protected void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable objDataTable = (DataTable) this.Session["dtExport"];
        ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
        List<ClassColumns> classColumnsList = new List<ClassColumns>();
        foreach (DataControlField column in (StateManagedCollection) this.wdgList.Columns)
        {
          if (column.Visible && column.GetType().Name == "BoundField")
          {
            BoundField boundField = (BoundField) column;
            classColumnsList.Add(new ClassColumns(boundField.DataField, 1, (int) column.ControlStyle.Width.Value, boundField.HeaderText));
          }
        }
        exportToExcelDataGrid.clsTitle = classColumnsList;
        exportToExcelDataGrid.AgregarHojaLibro(objDataTable, "Consulta Universal");
        exportToExcelDataGrid.CerrarLibro();
        byte[] buffer = exportToExcelDataGrid.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=ConsultaUniversal.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
      }
      catch (Exception ex)
      {
      }
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
      try
      {
        this.dtRequirement = this.Session["UniversalList"] != null ? this.Session["UniversalList"] as DataTable : throw new HandledException(0, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
        if (this.dtRequirement.Rows.Count == 0)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_No_Datos_Exportar);
        Document document = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 10f);
        string[] strArray = new string[5]
        {
          this.Server.MapPath("../Requirement/Temp/"),
          "Download.pdf_",
          null,
          null,
          null
        };
        DateTime now = DateTime.Now;
        strArray[2] = now.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture);
        now = DateTime.Now;
        strArray[3] = now.ToString("HHmmss", (IFormatProvider) CultureInfo.CurrentCulture);
        strArray[4] = ".pdf";
        string str = string.Concat(strArray);
        using (FileStream fileStream = new FileStream(str, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
        {
          PdfWriter.GetInstance(document, (Stream) fileStream);
          document.Open();
          this.GenerateDoc(document);
          document.Close();
        }
        this.Download(str, "DownLoadPDF.pdf");
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
        this.ViewState["t"] = (object) Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.ViewState["i_platetypeId"] = (object) Convert.ToString(this.Request.QueryString["i_platetypeId"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.BeginTitle();
        this.getStatus();
        this.getProcessType();
        this.getCategory();
        this.SetDatePicker();
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

    public void BeginTitle()
    {
      try
      {
        if (Convert.ToInt32(this.ViewState["t"], (IFormatProvider) CultureInfo.CurrentCulture) == 1)
          this.Page.Title = SIIV.SystemParameter.BL.Constants.REQUIREMENT_TITLE_UNIVERSAL_QUERY;
        else if (Convert.ToInt32(this.ViewState["t"], (IFormatProvider) CultureInfo.CurrentCulture) == 2)
          this.Page.Title = SIIV.SystemParameter.BL.Constants.REQUIREMENT_TITLE_GRUPAL_QUERY;
        else if (Convert.ToInt32(this.ViewState["t"], (IFormatProvider) CultureInfo.CurrentCulture) == 3)
        {
          this.Page.Title = SIIV.SystemParameter.BL.Constants.REQUIREMENT_TITLE_SELECTIVE_QUERY;
        }
        else
        {
          if (Convert.ToInt32(this.ViewState["t"], (IFormatProvider) CultureInfo.CurrentCulture) != 12)
            return;
          this.Page.Title = SIIV.SystemParameter.BL.Constants.REQUIREMENT_TITLE_DELIVERY_QUERY;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void getStatus()
    {
      try
      {
        this.wddStatus.DataSource = (object) new RequirementQueriesBL().GetRequirementPlateStatus();
        this.wddStatus.DataValueField = "i_ParameterId";
        this.wddStatus.DataTextField = "v_Description";
        this.wddStatus.DataBind();
        this.wddStatus.Items[0].Text = SIIV.SystemParameter.BL.Constants.OPCIONLISTA_Todos;
        this.wddStatus.SelectedIndex = 0;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void getProcessType()
    {
      try
      {
        this.wddProcesstype.DataSource = (object) new RequirementQueriesBL().GetProcess();
        this.wddProcesstype.DataValueField = "i_ParameterId";
        this.wddProcesstype.DataTextField = "v_Description";
        this.wddProcesstype.DataBind();
        this.wddProcesstype.Items[0].Text = SIIV.SystemParameter.BL.Constants.OPCIONLISTA_Todos;
        this.wddProcesstype.SelectedIndex = 0;
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
        this.wddCategory.DataSource = (object) new RequirementQueriesBL().GetCategory();
        this.wddCategory.DataValueField = "i_ParameterId";
        this.wddCategory.DataTextField = "v_Description";
        this.wddCategory.DataBind();
        this.wddCategory.Items[0].Text = SIIV.SystemParameter.BL.Constants.OPCIONLISTA_Todos;
        this.wddCategory.SelectedIndex = 0;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SetDatePicker()
    {
      this.wdpStartDate.Value = DateTime.Now;
      this.wdpEndDate.Value = DateTime.Now;
    }

    private void SearchUniversal()
    {
      try
      {
        DateTime dateTime = this.wdpEndDate.Value;
        if (dateTime.Subtract(this.wdpStartDate.Value).Days > 30 && this.txtPlateNew.Text.Trim() == "" && this.txtRequirementPlateId.Text.Trim() == "")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_UNIVERSAL_QUERY_ERROR_DATE);
        if (!this.wdpStartDate.Enabled && !this.wdpEndDate.Enabled && this.txtPlateNew.Text.Trim() == "" && this.txtPlateOld.Text.Trim() == "" && this.txtRequirementPlateId.Text.Trim() == "" && this.txtPaymentCode.Text.Trim() == "" && this.txtSerial.Text.Trim() == "")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_UNIVERSAL_QUERY_ADVERTENCIA_FieldBlankSearch);
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
        string pstrPlateNew = this.txtPlateNew.Text.Trim();
        int pintStartdate;
        int pintFinishdate;
        if (!this.wdpStartDate.Enabled && !this.wdpStartDate.Enabled)
        {
          pintStartdate = 0;
          pintFinishdate = 0;
        }
        else
        {
          dateTime = this.wdpStartDate.Value;
          pintStartdate = Convert.ToInt32(dateTime.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture));
          dateTime = this.wdpEndDate.Value;
          pintFinishdate = Convert.ToInt32(dateTime.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        }
        string pstrPlateOld = this.txtPlateOld.Text.Trim();
        string pstrTitleNumber = this.txtTitleNumber.Text.Trim();
        int pintRequirementPlateId = -1;
        if (this.txtRequirementPlateId.Text.Trim() != "")
          pintRequirementPlateId = Convert.ToInt32(this.txtRequirementPlateId.Text.Trim(), (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrOwnerName = this.txtOwner.Text.Trim();
        int int32_1 = Convert.ToInt32(this.wddCategory.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        int int32_2 = Convert.ToInt32(this.wddProcesstype.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        int pintStatus = -3;
        if (this.wddStatus.SelectedIndex != 0)
          pintStatus = Convert.ToInt32(this.wddStatus.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrSerial = this.txtSerial.Text.Trim();
        string pstrPaymentCode = this.txtPaymentCode.Text.Trim();
        int iSystemUserId = systemUser.i_SystemUserId;
        int int32_3 = Convert.ToInt32(this.ViewState["t"], (IFormatProvider) CultureInfo.CurrentCulture);
        int int32_4 = Convert.ToInt32(this.ViewState["i_platetypeId"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.SearchUniversalList(pstrPlateNew, pintStartdate, pintFinishdate, pstrPlateOld, pstrTitleNumber, pintRequirementPlateId, pstrOwnerName, int32_1, int32_2, pintStatus, pstrSerial, pstrPaymentCode, iSystemUserId, int32_3, int32_4, true);
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
        int startRowIndex = pboolLoadPager ? 1 : this.custPagerUQR.CurrentPageNumber;
        int maxRows = this.custPagerUQR.CurrentPageSize == 0 ? 10 : this.custPagerUQR.CurrentPageSize;
        int pinttotalRows;
        DataTable dataTable = new RequirementQueriesBL().UniversalQueryRead(pstrPlateNew, pintStartdate, pintFinishdate, pstrPlateOld, pstrTitleNumber, pintRequirementPlateId, pstrOwnerName, pintCategoryId, pintProcessTypeId, pintStatus, pstrSerial, pstrPaymentCode, pintUserId, pintQueryType, pintiplateTypeId, startRowIndex, maxRows, out pinttotalRows);
        if (dataTable == null || dataTable.Rows.Count == 0)
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
        else
          this.Session["UniversalList"] = (object) dataTable;
        int num = pinttotalRows;
        this.wdgList.DataSource = (object) dataTable;
        this.wdgList.DataBind();
        this.custPagerUQR.TotalPages = num % maxRows == 0 ? num / maxRows : num / maxRows + 1;
        this.custPagerUQR.TotalRecordCount = pinttotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerUQR.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ExportList()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
        string pstrPlateNew = this.txtPlateNew.Text.Trim();
        int pintStartdate;
        int pintFinishdate;
        if (!this.wdpStartDate.Enabled && !this.wdpStartDate.Enabled)
        {
          pintStartdate = 0;
          pintFinishdate = 0;
        }
        else
        {
          pintStartdate = Convert.ToInt32(this.wdpStartDate.Value.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture));
          pintFinishdate = Convert.ToInt32(this.wdpEndDate.Value.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        }
        string pstrPlateOld = this.txtPlateOld.Text.Trim();
        string pstrTitleNumber = this.txtTitleNumber.Text.Trim();
        int pintRequirementPlateId = -1;
        if (this.txtRequirementPlateId.Text.Trim() != "")
          pintRequirementPlateId = Convert.ToInt32(this.txtRequirementPlateId.Text.Trim(), (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrOwnerName = this.txtOwner.Text.Trim();
        int int32_1 = Convert.ToInt32(this.wddCategory.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        int int32_2 = Convert.ToInt32(this.wddProcesstype.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        int pintStatus = -3;
        if (this.wddStatus.SelectedIndex != 0)
          pintStatus = Convert.ToInt32(this.wddStatus.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrSerial = this.txtSerial.Text.Trim();
        string pstrPaymentCode = this.txtPaymentCode.Text.Trim();
        int iSystemUserId = systemUser.i_SystemUserId;
        int int32_3 = Convert.ToInt32(this.ViewState["t"], (IFormatProvider) CultureInfo.CurrentCulture);
        int int32_4 = Convert.ToInt32(this.ViewState["i_platetypeId"], (IFormatProvider) CultureInfo.CurrentCulture);
        DataTable dataTable = new DataTable();
        this.Session["dtExport"] = (object) new RequirementQueriesBL().UniversalQueryRead(pstrPlateNew, pintStartdate, pintFinishdate, pstrPlateOld, pstrTitleNumber, pintRequirementPlateId, pstrOwnerName, int32_1, int32_2, pintStatus, pstrSerial, pstrPaymentCode, iSystemUserId, int32_3, int32_4, 0, 0, out int _);
        this.Export();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void Export()
    {
      string script = "ExportExcelAll();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }

    public void GenerateDoc(Document document)
    {
      try
      {
        if (this.Session["UniversalList"] == null)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = this.Session["UniversalList"] as DataTable;
        DataTable _dtRequirement = dataTable2.Clone();
        for (int index = 0; index < dataTable2.Rows.Count; ++index)
          _dtRequirement.ImportRow(dataTable2.Rows[index]);
        string[] strArray = new string[this.wdgList.Columns.Count];
        List<string> stringList = new List<string>();
        List<float> floatList = new List<float>();
        for (int index = 0; index < this.wdgList.Columns.Count; ++index)
        {
          DataControlField column = this.wdgList.Columns[index];
          if (column.Visible && this.wdgList.Columns[index].HeaderText != "" && column.GetType().Name == "BoundField")
          {
            BoundField boundField = (BoundField) column;
            strArray[index] = boundField.DataField;
          }
        }
        int count = _dtRequirement.Columns.Count;
        for (int index1 = 0; index1 < count; ++index1)
        {
          int num = 0;
          for (int index2 = 0; index2 < strArray.Length; ++index2)
          {
            if (_dtRequirement.Columns[index1].ColumnName == strArray[index2])
            {
              num = 1;
              _dtRequirement.Columns[index1].ColumnName = this.wdgList.Columns[index2].HeaderText;
              DataControlField column = this.wdgList.Columns[index2];
              if (column.GetType().Name == "BoundField")
                floatList.Add((float) (int) column.ControlStyle.Width.Value);
            }
          }
          if (num == 0)
            stringList.Add(_dtRequirement.Columns[index1].ColumnName);
        }
        stringList.ForEach((Action<string>) (name => _dtRequirement.Columns.Remove(name)));
        PdfPTable pdfPtable = new PdfPTable(_dtRequirement.Columns.Count);
        pdfPtable.DefaultCell.Padding = 3f;
        float[] relativeWidths = new float[floatList.Count];
        for (int index = 0; index < floatList.Count; ++index)
          relativeWidths[index] = floatList[index];
        pdfPtable.SetWidths(relativeWidths);
        pdfPtable.WidthPercentage = 100f;
        pdfPtable.DefaultCell.BorderWidth = 2f;
        pdfPtable.DefaultCell.HorizontalAlignment = 1;
        for (int index = 0; index < _dtRequirement.Columns.Count; ++index)
          pdfPtable.AddCell(_dtRequirement.Columns[index].ColumnName);
        pdfPtable.HeaderRows = 1;
        pdfPtable.DefaultCell.BorderWidth = 1f;
        for (int index = 0; index < _dtRequirement.Rows.Count; ++index)
        {
          for (int columnIndex = 0; columnIndex < _dtRequirement.Columns.Count; ++columnIndex)
            pdfPtable.AddCell(_dtRequirement.Rows[index][columnIndex].ToString());
        }
        pdfPtable.CompleteRow();
        document.Add((IElement) pdfPtable);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void wdgList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    public void Download(string pstrPathFile, string pstrFileTarget)
    {
      try
      {
        new ExportFile().Download(pstrPathFile, pstrFileTarget);
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
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }

    private void CreatePopUpConciliation(
      string url,
      string pstrtitle,
      string width,
      string height)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUpConciliation('{0}','{1}','{2}','{3}');", (object) url, (object) pstrtitle, (object) (width + "px"), (object) (height + "px"));
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }
  }
}
