// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Retail.UniversalQuery
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Retail
{
  public class UniversalQuery : Page
  {
    private DataTable dtRequirement;
    private int iConta = 0;
    protected UpdatePanel updatePanel;
    protected Panel Panel1;
    protected Label lblEstado;
    protected DropDownList wddProofPaymentTypeId;
    protected CheckBox chkGenerationBetween;
    protected Fecha wdpStartDate;
    protected Fecha wdpEndDate;
    protected Label lblDocument;
    protected TextBox txtProfDocument;
    protected FilteredTextBoxExtender txtProfDocument_FilteredTextBoxExtender;
    protected Label lblTitle;
    protected TextBox txtClient;
    protected Label lblRequirementId;
    protected TextBox txtRequirementId;
    protected FilteredTextBoxExtender txtRequirementId_FilteredTextBoxExtender;
    protected Label lblTitle0;
    protected TextBox txtCliDocument;
    protected Timer Timer1;
    protected Button wibSearch;
    protected HtmlTableCell img;
    protected Label lblMessage;
    protected GridView wdgList;
    protected Pager custPagerUQR;
    protected Button wibExcel;
    protected Button wibPdf;
    protected Button Button1;
    protected Button btnJavaScriptResponse;

    public void Initialize()
    {
      try
      {
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

    private void SetDatePicker()
    {
      this.wdpStartDate.Value = DateTime.Now;
      this.wdpEndDate.Value = DateTime.Now;
    }

    public void getStatus()
    {
      try
      {
        this.wddProofPaymentTypeId.DataSource = (object) new RequirementQueriesBL().GetEbillingProofPaymentType();
        this.wddProofPaymentTypeId.DataValueField = "i_ParameterId";
        this.wddProofPaymentTypeId.DataTextField = "v_Description";
        this.wddProofPaymentTypeId.DataBind();
        this.wddProofPaymentTypeId.Items[0].Text = SIIV.SystemParameter.BL.Constants.OPCIONLISTA_Todos;
        this.wddProofPaymentTypeId.SelectedIndex = 0;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void BeginTitle()
    {
      try
      {
        this.Page.Title = "Consulta Retail";
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      this.BeginTitle();
      this.ViewState["t"] = (object) Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.ViewState["i_platetypeId"] = (object) Convert.ToString(this.Request.QueryString["i_platetypeId"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.getStatus();
      this.SetDatePicker();
    }

    protected void chkGenerationBetween_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkGenerationBetween.Checked)
      {
        this.wdpStartDate.Enabled = true;
        this.wdpEndDate.Enabled = true;
        this.txtClient.Text = "";
        this.txtRequirementId.Text = "";
        this.txtProfDocument.Text = "";
        this.wdgList.DataSource = (object) null;
        this.wdgList.DataBind();
      }
      else
      {
        this.wdpStartDate.Enabled = false;
        this.wdpEndDate.Enabled = false;
        this.txtClient.Text = "";
        this.txtRequirementId.Text = "";
        this.txtProfDocument.Text = "";
        this.wdgList.DataSource = (object) null;
        this.wdgList.DataBind();
      }
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        this.wdgList.DataSource = (object) null;
        this.wdgList.DataBind();
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

    protected void custPagerUQR_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        DateTime dateTime;
        if (this.chkGenerationBetween.Checked)
        {
          dateTime = this.wdpEndDate.Value;
          if (dateTime.Subtract(this.wdpStartDate.Value).Days > 30 && this.txtRequirementId.Text.Trim() == "")
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENTEBILLING_UNIVERSAL_QUERY_ERROR_DATE);
        }
        if (!this.wdpStartDate.Enabled && !this.wdpEndDate.Enabled && this.txtRequirementId.Text.Trim() == "" && this.txtProfDocument.Text.Trim() == "" && this.txtClient.Text.Trim() == "")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENTEBILLING_UNIVERSAL_QUERY_ADVERTENCIA_FieldBlankSearch);
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
        string pstrProfDocument = this.txtProfDocument.Text.Trim();
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
        int pintRequirement = -1;
        if (this.txtRequirementId.Text.Trim() != "")
          pintRequirement = Convert.ToInt32(this.txtRequirementId.Text.Trim(), (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrClient = this.txtClient.Text.Trim();
        string pstrCliDocument = this.txtCliDocument.Text.Trim();
        int pintProfType = -3;
        if (this.wddProofPaymentTypeId.SelectedIndex != 0)
          pintProfType = Convert.ToInt32(this.wddProofPaymentTypeId.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        int iSystemUserId = systemUser.i_SystemUserId;
        int int32_1 = Convert.ToInt32(this.ViewState["t"], (IFormatProvider) CultureInfo.CurrentCulture);
        int int32_2 = Convert.ToInt32(this.ViewState["i_platetypeId"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.SearchUniversalList(pintProfType, pintStartdate, pintFinishdate, pstrProfDocument, pstrClient, pstrCliDocument, pintRequirement, iSystemUserId, int32_1, int32_2, false);
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
    }

    private void SearchUniversal()
    {
      try
      {
        DateTime dateTime;
        if (this.chkGenerationBetween.Checked)
        {
          dateTime = this.wdpEndDate.Value;
          if (dateTime.Subtract(this.wdpStartDate.Value).Days > 30 && this.txtRequirementId.Text.Trim() == "")
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENTEBILLING_UNIVERSAL_QUERY_ERROR_DATE);
        }
        if (!this.wdpStartDate.Enabled && !this.wdpEndDate.Enabled && this.txtRequirementId.Text.Trim() == "" && this.txtProfDocument.Text.Trim() == "" && this.txtClient.Text.Trim() == "")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENTEBILLING_UNIVERSAL_QUERY_ADVERTENCIA_FieldBlankSearch);
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
        string pstrProfDocument = this.txtProfDocument.Text.Trim();
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
        int pintRequirement = -1;
        if (this.txtRequirementId.Text.Trim() != "")
          pintRequirement = Convert.ToInt32(this.txtRequirementId.Text.Trim(), (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrClient = this.txtClient.Text.Trim();
        string pstrCliDocument = this.txtCliDocument.Text.Trim();
        int pintProfType = -3;
        if (this.wddProofPaymentTypeId.SelectedIndex != 0)
          pintProfType = Convert.ToInt32(this.wddProofPaymentTypeId.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        int iSystemUserId = systemUser.i_SystemUserId;
        int int32_1 = Convert.ToInt32(this.ViewState["t"], (IFormatProvider) CultureInfo.CurrentCulture);
        int int32_2 = Convert.ToInt32(this.ViewState["i_platetypeId"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.SearchUniversalList(pintProfType, pintStartdate, pintFinishdate, pstrProfDocument, pstrClient, pstrCliDocument, pintRequirement, iSystemUserId, int32_1, int32_2, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchUniversalList(
      int pintProfType,
      int pintStartdate,
      int pintFinishdate,
      string pstrProfDocument,
      string pstrClient,
      string pstrCliDocument,
      int pintRequirement,
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
        DataTable dataTable = new RequirementQueriesBL().EBillingUniversalQueryRead(pintProfType, pintStartdate, pintFinishdate, pstrProfDocument, pstrClient, pstrCliDocument, pintRequirement, pintUserId, pintQueryType, pintiplateTypeId, startRowIndex, maxRows, out pinttotalRows);
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

    protected void wdgList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        if (e.CommandName.Equals("PrintUri", StringComparison.CurrentCulture))
        {
          this.lblMessage.Visible = false;
          int int32 = Convert.ToInt32(e.CommandArgument);
          GridViewRow row = this.wdgList.Rows[int32];
          ImageButton imageButton = sender as ImageButton;
          CheckBox control = (CheckBox) this.wdgList.Rows[int32].FindControl("CheckBox2");
          bool flag = new RequirementManagementBL().EBillingStatusProcess(Convert.ToInt32(row.Cells[3].Text), 0, -1.0);
          this.Timer1.Enabled = true;
          this.Panel1.Enabled = false;
          if (flag)
          {
            this.ViewState["intRequirementID"] = (object) row.Cells[3].Text;
            this.Session["iConta"] = (object) 0;
            this.OpenURI();
          }
          else
            Message.SetMessage(this.lblMessage, new HandledException(0, "Error en Conexión"));
        }
        if (e.CommandName.Equals("DeleteSoli", StringComparison.CurrentCulture))
        {
          string empty = string.Empty;
          GridViewRow row = this.wdgList.Rows[Convert.ToInt32(e.CommandArgument)];
          if (row.Cells[6].Text.IndexOf("T") >= 0)
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "La serie T001 no puede ser anulado."));
            return;
          }
          if (row.Cells[10].Text == "ANULADO")
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "Esta solicitud ya fue anulado."));
            return;
          }
          this.ViewState["i_RequirementId"] = (object) row.Cells[3].Text;
          this.CreatePopUpServer("SIIV - Retail - Anular", "RetailAnulate.aspx?i_RequirementId=" + row.Cells[3].Text + "&v_CompleteName=" + HttpUtility.HtmlDecode(row.Cells[8].Text) + "&v_Product=" + HttpUtility.HtmlDecode(row.Cells[14].Text) + "&v_Comprobante=" + row.Cells[6].Text, "470px", "290px");
        }
        if (!e.CommandName.Equals("PrintEBilling", StringComparison.CurrentCulture))
          return;
        this.lblMessage.Visible = false;
        GridViewRow row1 = this.wdgList.Rows[Convert.ToInt32(e.CommandArgument)];
        ImageButton imageButton1 = sender as ImageButton;
        string v_Url = "";
        string v_Estado = "";
        string str = "";
        string empty1 = string.Empty;
        str = new RequirementManagementBL().EBillingUrl(Convert.ToInt32(row1.Cells[3].Text), 0, out v_Url, out v_Estado);
        if (Convert.ToInt32(v_Estado) != 100)
        {
          string script = "OpenUrl('" + ("../Public/EBilling.aspx?" + v_Url) + "');";
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        }
        else
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", "AlertaUrl();", true);
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

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }

    protected void btnJavaScriptResponse_Click(object sender, EventArgs e)
    {
      if (this.Session["OpenSucesfull"].ToString() == "1")
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Success, "Se realizó la Anulación Correctamente");
        this.wibSearch_Click((object) null, (EventArgs) null);
      }
      this.Session.Remove("OpenSucesfull");
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
      this.Panel1.Enabled = false;
      this.Session["iConta"] = (object) (Convert.ToInt32(this.Session["iConta"]) + 1);
      DataTable actionProceesEbilling = new RequirementQueriesBL().GetActionProceesEBilling(Convert.ToInt32(this.ViewState["intRequirementID"]));
      if (actionProceesEbilling.Rows[0][0].ToString() == ConfigurationManager.AppSettings["EBillingPayStatus"].ToString() && actionProceesEbilling.Rows[0][1].ToString() == "2")
      {
        this.HidePopup();
        this.Timer1.Enabled = false;
        this.img.Visible = false;
        this.Panel1.Enabled = true;
        Message.SetMessage(this.lblMessage, enmMessageType.Success, "Se realizó la impresión Correctamente");
        this.SearchUniversal();
      }
      if (actionProceesEbilling.Rows[0][1].ToString() != "0" && actionProceesEbilling.Rows[0][1].ToString() != "2")
      {
        Message.SetMessage(this.lblMessage, new HandledException(0, actionProceesEbilling.Rows[0][2].ToString()));
        this.HidePopup();
        this.Timer1.Enabled = false;
        this.img.Visible = false;
        this.Panel1.Enabled = true;
      }
      if (Convert.ToInt32(this.Session["iConta"]) != Convert.ToInt32(ConfigurationManager.AppSettings["EBillingWaitTime"]))
        return;
      this.HidePopup();
      Message.SetMessage(this.lblMessage, new HandledException(0, "Error en Conexion"));
      this.Timer1.Enabled = false;
      this.img.Visible = false;
      this.Panel1.Enabled = true;
    }

    private void OpenPopup()
    {
      string script = "ShowModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private void OpenURI()
    {
      string script = "EjecutarURI(" + this.ViewState["intRequirementID"]?.ToString() + ");";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      this.img.Visible = true;
    }

    private void CreatePopUp(string url, string pstrtitle, string width, string height)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) url, (object) pstrtitle, (object) (width + "px"), (object) (height + "px"));
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }

    protected void wibExcel_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.wdgList.Rows.Count == 0)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENTEBILLING_UNIVERSAL_QUERY_ERROR_EXPORT);
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

    private void ExportList()
    {
      try
      {
        DateTime dateTime = this.wdpEndDate.Value;
        if (dateTime.Subtract(this.wdpStartDate.Value).Days > 30 && this.txtRequirementId.Text.Trim() == "")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENTEBILLING_UNIVERSAL_QUERY_ERROR_DATE);
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
        string pstrProfDocument = this.txtProfDocument.Text.Trim();
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
        int pintRequirement = -1;
        if (this.txtRequirementId.Text.Trim() != "")
          pintRequirement = Convert.ToInt32(this.txtRequirementId.Text.Trim(), (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrClient = this.txtClient.Text.Trim();
        string pstrCliDocument = this.txtCliDocument.Text.Trim();
        int pintProfType = -3;
        if (this.wddProofPaymentTypeId.SelectedIndex != 0)
          pintProfType = Convert.ToInt32(this.wddProofPaymentTypeId.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        int iSystemUserId = systemUser.i_SystemUserId;
        int int32_1 = Convert.ToInt32(this.ViewState["t"], (IFormatProvider) CultureInfo.CurrentCulture);
        int int32_2 = Convert.ToInt32(this.ViewState["i_platetypeId"], (IFormatProvider) CultureInfo.CurrentCulture);
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = new RequirementQueriesBL().EBillingUniversalQueryRead(pintProfType, pintStartdate, pintFinishdate, pstrProfDocument, pstrClient, pstrCliDocument, pintRequirement, iSystemUserId, int32_1, int32_2, 0, 0, out int _);
        if (dataTable2.Rows.Count == 0)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENTEBILLING_UNIVERSAL_QUERY_ERROR_EXPORT);
        this.Session["dtExport"] = (object) dataTable2;
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

    protected void wibPdf_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.wdgList.Rows.Count == 0)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENTEBILLING_UNIVERSAL_QUERY_ERROR_EXPORT);
        this.dtRequirement = this.Session["UniversalList"] != null ? this.Session["UniversalList"] as DataTable : throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_No_Datos_Exportar);
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

    protected void wdgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
        return;
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) "994",
        (object) "",
        (object) "1",
        (object) "1"
      });
      SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
      int num = 0;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (row["v_value"].ToString() == systemUser.i_SystemUserId.ToString())
          num = 1;
      }
      if (num != 0)
        return;
      e.Row.Cells[2].Visible = false;
    }
  }
}
