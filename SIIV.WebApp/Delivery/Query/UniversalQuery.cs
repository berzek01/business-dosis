// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Delivery.Query.UniversalQuery
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using BarcodeLib;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Delivery.Query
{
  public class UniversalQuery : Page
  {
    private DataTable dtRequirement;
    private int iConta = 0;
    protected UpdatePanel updatePanel;
    protected Panel Panel1;
    protected Label lblPlateNew;
    protected TextBox txtPlateNew;
    protected FilteredTextBoxExtender txtPlateNew_FilteredTextBoxExtender;
    protected CheckBox chkGenerationBetween;
    protected Fecha wdpStartDate;
    protected Fecha wdpEndDate;
    protected Label lblRequirementId;
    protected TextBox txtRequirementId;
    protected FilteredTextBoxExtender txtRequirementPlateId_FilteredTextBoxExtender1;
    protected Label lblOwner;
    protected TextBox txtOwner;
    protected Label lblEstado;
    protected DropDownList wddStatus;
    protected Label Label1;
    protected TextBox txtPaymentCode;
    protected Button wibSearch;
    protected Label lblMessage;
    protected GridView wdgList;
    protected Pager custPagerUQR;
    protected Button wibExcel;
    protected Button BtnPdfCUR;
    protected Button Button1;
    protected Button btnJavaScriptResponse;

    public void Initialize()
    {
      try
      {
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
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
        foreach (DataRow row in (InternalDataCollectionBase) new RequirementQueriesBL().GetRequirementPlateStatus().Rows)
        {
          if (Convert.ToInt32(row["i_ParameterId"]) != -3 && Convert.ToInt32(row["i_ParameterId"]) != -2 && Convert.ToInt32(row["i_ParameterId"]) != -1 && Convert.ToInt32(row["i_ParameterId"]) != 2 && Convert.ToInt32(row["i_ParameterId"]) != 5 && Convert.ToInt32(row["i_ParameterId"]) != 7 && Convert.ToInt32(row["i_ParameterId"]) != 10 && Convert.ToInt32(row["i_ParameterId"]) != 11 && Convert.ToInt32(row["i_ParameterId"]) != 12 && Convert.ToInt32(row["i_ParameterId"]) != 13 && Convert.ToInt32(row["i_ParameterId"]) != 14 && Convert.ToInt32(row["i_ParameterId"]) != 15 && Convert.ToInt32(row["i_ParameterId"]) != 16 && Convert.ToInt32(row["i_ParameterId"]) != 99)
            this.wddStatus.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        }
        this.wddStatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Todos --", "0"));
        this.wddStatus.SelectedValue = "-3";
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
        this.wdgList.DataSource = (object) null;
        this.wdgList.DataBind();
      }
      else
      {
        this.wdpStartDate.Enabled = false;
        this.wdpEndDate.Enabled = false;
        this.wdgList.DataSource = (object) null;
        this.wdgList.DataBind();
      }
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        this.lblMessage.Text = "";
        this.lblMessage.Visible = false;
        this.wdgList.DataSource = (object) null;
        this.wdgList.DataBind();
        this.SearchUniversal();
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
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
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
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
        int pintRequirementId = -1;
        if (this.txtRequirementId.Text.Trim() != "")
          pintRequirementId = Convert.ToInt32(this.txtRequirementId.Text.Trim(), (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrOwnerName = this.txtOwner.Text.Trim();
        string pstrPaymentCode = this.txtPaymentCode.Text.Trim();
        string pstrPlateNew = this.txtPlateNew.Text.Trim();
        int pintStatus = -3;
        if (this.wddStatus.SelectedIndex != 0)
          pintStatus = Convert.ToInt32(this.wddStatus.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        int iSystemUserId = systemUser.i_SystemUserId;
        this.SearchUniversalList(pstrPlateNew, pintStartdate, pintFinishdate, pintRequirementId, pstrOwnerName, pintStatus, pstrPaymentCode, iSystemUserId, false);
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
    }

    private void SearchUniversal()
    {
      try
      {
        DateTime dateTime = this.wdpEndDate.Value;
        if (dateTime.Subtract(this.wdpStartDate.Value).Days > 30 && this.txtPlateNew.Text.Trim() == "" && this.txtRequirementId.Text.Trim() == "")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_UNIVERSAL_QUERY_ERROR_DATE);
        if (!this.wdpStartDate.Enabled && !this.wdpEndDate.Enabled && this.txtPlateNew.Text.Trim() == "" && this.txtRequirementId.Text.Trim() == "" && this.txtPaymentCode.Text.Trim() == "")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_UNIVERSAL_QUERY_ADVERTENCIA_FieldBlankSearch);
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
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
        int pintRequirementId = -1;
        if (this.txtRequirementId.Text.Trim() != "")
          pintRequirementId = Convert.ToInt32(this.txtRequirementId.Text.Trim(), (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrOwnerName = this.txtOwner.Text.Trim();
        string pstrPaymentCode = this.txtPaymentCode.Text.Trim();
        string pstrPlateNew = this.txtPlateNew.Text.Trim();
        int pintStatus = -3;
        if (this.wddStatus.SelectedIndex != 0)
          pintStatus = Convert.ToInt32(this.wddStatus.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        int iSystemUserId = systemUser.i_SystemUserId;
        this.SearchUniversalList(pstrPlateNew, pintStartdate, pintFinishdate, pintRequirementId, pstrOwnerName, pintStatus, pstrPaymentCode, iSystemUserId, true);
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
      int pintRequirementId,
      string pstrOwnerName,
      int pintStatus,
      string pstrPaymentCode,
      int pintUserId,
      bool pboolLoadPager)
    {
      try
      {
        int startRowIndex = pboolLoadPager ? 1 : this.custPagerUQR.CurrentPageNumber;
        int maxRows = this.custPagerUQR.CurrentPageSize == 0 ? 10 : this.custPagerUQR.CurrentPageSize;
        int pinttotalRows;
        DataTable dataTable = new RequirementQueriesBL().DeliveryUniversalQueryRead(pstrPlateNew, pintStartdate, pintFinishdate, pintRequirementId, pstrOwnerName, pintStatus, pstrPaymentCode, pintUserId, startRowIndex, maxRows, out pinttotalRows);
        if (dataTable == null || dataTable.Rows.Count == 0)
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
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
        if (e.CommandName.Equals("PrintEBilling", StringComparison.CurrentCulture))
        {
          this.lblMessage.Visible = false;
          GridViewRow row = this.wdgList.Rows[Convert.ToInt32(e.CommandArgument)];
          ImageButton imageButton = sender as ImageButton;
          string v_Url = "";
          string v_Estado = "";
          string str = "";
          string empty = string.Empty;
          str = new RequirementManagementBL().EBillingUrl(Convert.ToInt32(row.Cells[3].Text), 0, out v_Url, out v_Estado);
          if (Convert.ToInt32(v_Estado) != 100)
          {
            string script = "OpenUrl('" + ("../../Public/EBilling.aspx?" + v_Url) + "');";
            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
          }
          else
            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", "AlertaUrl();", true);
        }
        if (e.CommandName.Equals("PrintReportCurPDF", StringComparison.CurrentCulture))
        {
          this.ViewState["i_RequirementId"] = (object) Convert.ToInt32(this.wdgList.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
          this.ExportPDFCUR();
        }
        if (!e.CommandName.Equals("SendEmail", StringComparison.CurrentCulture))
          return;
        GridViewRow row1 = this.wdgList.Rows[Convert.ToInt32(e.CommandArgument)];
        this.CreatePopUpServer("SIIV - Delivery - Envio de Comprobante Electronico y/o PDF CUR", "../../Delivery/Query/SendEmail.aspx?i_Requirementid=" + row1.Cells[3].Text + "&Email=" + row1.Cells[14].Text, "750px", "370px");
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    private byte[] ImagenBarCode(string _NroPlaca)
    {
      if (!(_NroPlaca.Trim() != ""))
        return (byte[]) null;
      BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();
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
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Success, "Se realizó la Anulación Correctamente");
        this.wibSearch_Click((object) null, (EventArgs) null);
      }
      this.Session.Remove("OpenSucesfull");
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
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void BtnPdfCUR_Click(object sender, EventArgs e)
    {
      ReportDocument reportDocument = new ReportDocument();
      string filename = this.Server.MapPath("../Reports/ReportCurDelivery.rpt");
      reportDocument.Load(filename);
      DataTable deliveryCur = new RequirementQueriesBL().GenerateDeliveryCUR(Convert.ToInt32(this.ViewState["i_RequirementId"]));
      deliveryCur.Columns.Add(new DataColumn()
      {
        ColumnName = "ImageBarPlate",
        DataType = typeof (byte[])
      });
      deliveryCur.Columns.Add(new DataColumn()
      {
        ColumnName = "Requisite",
        DataType = typeof (string)
      });
      deliveryCur.Columns.Add(new DataColumn()
      {
        ColumnName = "ImageBarCode",
        DataType = typeof (byte[])
      });
      int num = 0;
      string str = "";
      if (deliveryCur.Rows.Count > 1)
        deliveryCur.Rows.RemoveAt(1);
      foreach (DataRow row in (InternalDataCollectionBase) deliveryCur.Rows)
      {
        byte[] numArray1 = this.ImagenBarCode(row["v_PlateNew"].ToString());
        row["ImageBarPlate"] = (object) numArray1;
        string requisitebyRequirement = new RequirementQueriesBL().GetRequisitebyRequirement(Convert.ToInt32(this.ViewState["i_RequirementId"]), Convert.ToInt32(row["i_ProcessTypeId"], (IFormatProvider) CultureInfo.CurrentCulture));
        row["Requisite"] = (object) requisitebyRequirement;
        byte[] numArray2 = this.ImagenBarCode(row["v_PaymentCode"].ToString());
        row["ImageBarCode"] = (object) numArray2;
        if (str != row["i_RequirementId"].ToString().Trim())
          ++num;
        str = row["i_RequirementId"].ToString().Trim();
      }
      reportDocument.SetDataSource(deliveryCur);
      reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "Certificado_Unico_Delivery");
      reportDocument.Close();
      ((Component) reportDocument).Dispose();
      deliveryCur.Dispose();
      GC.Collect();
    }

    private void ExportList()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
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
        int pintRequirementId = -1;
        if (this.txtRequirementId.Text.Trim() != "")
          pintRequirementId = Convert.ToInt32(this.txtRequirementId.Text.Trim(), (IFormatProvider) CultureInfo.CurrentCulture);
        int pintStatus = -3;
        if (this.wddStatus.SelectedIndex != 0)
          pintStatus = Convert.ToInt32(this.wddStatus.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrOwnerName = this.txtOwner.Text.Trim();
        string pstrPaymentCode = this.txtPaymentCode.Text.Trim();
        string pstrPlateNew = this.txtPlateNew.Text.Trim();
        int iSystemUserId = systemUser.i_SystemUserId;
        DataTable dataTable = new DataTable();
        this.Session["dtExport"] = (object) new RequirementQueriesBL().DeliveryUniversalQueryRead(pstrPlateNew, pintStartdate, pintFinishdate, pintRequirementId, pstrOwnerName, pintStatus, pstrPaymentCode, iSystemUserId, 0, 0, out int _);
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

    private void ExportPDFCUR()
    {
      string script = "ExportPDFCURAll();";
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

    public void Download(string pstrPathFile, string pstrFileTarget)
    {
      try
      {
        new ExportFile().Download(pstrPathFile, pstrFileTarget);
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
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
  }
}
