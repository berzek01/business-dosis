// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Report.BillingExport
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.WebApp.ServiceSoftnet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Report
{
  public class BillingExport : Page
  {
    protected UpdatePanel updatePanel;
    protected Panel Panel1;
    protected Label lblEstado;
    protected DropDownList wddProofPaymentTypeId;
    protected CheckBox chkGenerationBetween;
    protected Fecha wdpStartDate;
    protected Fecha wdpEndDate;
    protected Label lblSerie;
    protected TextBox txtSerieDocument;
    protected FilteredTextBoxExtender txtSerieDocument_FilteredTextBoxExtender;
    protected Label lblTitle;
    protected TextBox txtClient;
    protected Label Label1;
    protected TextBox txtDesde;
    protected FilteredTextBoxExtender FilteredTextBoxExtender1;
    protected Label Label2;
    protected TextBox txtHasta;
    protected FilteredTextBoxExtender FilteredTextBoxExtender2;
    protected Button wibSearch;
    protected Label lblMessage;
    protected GridView wdgList;
    protected Pager custPagerUQR;
    protected Button btnPDF;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      this.BeginTitle();
      this.SetDatePicker();
    }

    public void BeginTitle()
    {
      try
      {
        this.Page.Title = "Exportar Pdf";
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

    protected void chkGenerationBetween_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkGenerationBetween.Checked)
      {
        this.wdpStartDate.Enabled = true;
        this.wdpEndDate.Enabled = true;
        this.txtClient.Text = "";
        this.txtSerieDocument.Text = "";
        this.wdgList.DataSource = (object) null;
        this.wdgList.DataBind();
      }
      else
      {
        this.wdpStartDate.Enabled = false;
        this.wdpEndDate.Enabled = false;
        this.txtClient.Text = "";
        this.txtSerieDocument.Text = "";
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

    private void SearchUniversal()
    {
      try
      {
        DateTime dateTime;
        if (this.chkGenerationBetween.Checked)
        {
          dateTime = this.wdpEndDate.Value;
          if (dateTime.Subtract(this.wdpStartDate.Value).Days > 30)
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.EBILLING_UNIVERSAL_QUERY_ADVERTENCIA_EXPORT);
        }
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
        string strClient = this.txtClient.Text.Trim();
        string strSerieDocument = this.txtSerieDocument.Text.Trim();
        string strDesde = this.txtDesde.Text.Trim();
        string strHasta = this.txtHasta.Text.Trim();
        string pstringProfType = "";
        if (this.wddProofPaymentTypeId.SelectedIndex != 0)
          pstringProfType = Convert.ToString(this.wddProofPaymentTypeId.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        this.SearchUniversalList(pstringProfType, pintStartdate, pintFinishdate, strClient, strDesde, strHasta, strSerieDocument, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchUniversalList(
      string pstringProfType,
      int pintStartdate,
      int pintFinishdate,
      string strClient,
      string strDesde,
      string strHasta,
      string strSerieDocument,
      bool pboolLoadPager)
    {
      try
      {
        int startRowIndex = pboolLoadPager ? 1 : this.custPagerUQR.CurrentPageNumber;
        int maxRows = this.custPagerUQR.CurrentPageSize == 0 ? 10 : this.custPagerUQR.CurrentPageSize;
        int pinttotalRows;
        DataTable dataTable = new RequirementQueriesBL().EBillingQueryExport(pstringProfType, pintStartdate, pintFinishdate, strClient, strDesde, strHasta, strSerieDocument, startRowIndex, maxRows, out pinttotalRows);
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
    }

    protected void wdgBatchReception_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgBatchReception_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void custPagerUQR_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        DateTime dateTime;
        if (this.chkGenerationBetween.Checked)
        {
          dateTime = this.wdpEndDate.Value;
          if (dateTime.Subtract(this.wdpStartDate.Value).Days > 30)
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.EBILLING_UNIVERSAL_QUERY_ADVERTENCIA_EXPORT);
        }
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
        string strClient = this.txtClient.Text.Trim();
        string strSerieDocument = this.txtSerieDocument.Text.Trim();
        string strDesde = this.txtDesde.Text.Trim();
        string strHasta = this.txtHasta.Text.Trim();
        string pstringProfType = "";
        if (this.wddProofPaymentTypeId.SelectedIndex != 0)
          pstringProfType = Convert.ToString(this.wddProofPaymentTypeId.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        this.SearchUniversalList(pstringProfType, pintStartdate, pintFinishdate, strClient, strDesde, strHasta, strSerieDocument, false);
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }

    protected void wdgList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    public static bool MergePDFs(List<string> InFiles, string OutFile)
    {
      bool flag = true;
      try
      {
        if (InFiles.Count > 0)
        {
          List<PdfReader> pdfReaderList = new List<PdfReader>();
          foreach (string inFile in InFiles)
          {
            PdfReader pdfReader = new PdfReader(inFile);
            pdfReaderList.Add(pdfReader);
          }
          Document document = new Document(PageSize.A4, 0.0f, 0.0f, 0.0f, 0.0f);
          PdfWriter instance = PdfWriter.GetInstance(document, (Stream) new FileStream(OutFile, FileMode.Create));
          document.Open();
          foreach (PdfReader reader in pdfReaderList)
          {
            PdfReader.unethicalreading = true;
            for (int pageNumber = 1; pageNumber <= reader.NumberOfPages; ++pageNumber)
            {
              PdfImportedPage importedPage = instance.GetImportedPage(reader, pageNumber);
              document.Add((IElement) iTextSharp.text.Image.GetInstance((PdfTemplate) importedPage));
            }
          }
          document.Close();
          foreach (PdfReader pdfReader in pdfReaderList)
            pdfReader.Close();
        }
        else
          flag = false;
      }
      catch (Exception ex)
      {
        flag = false;
      }
      return flag;
    }

    protected void wdgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowIndex < 0)
          return;
        if (e.Row.Cells[9].Text == "1")
        {
          for (int index = 0; index < 9; ++index)
            e.Row.Cells[index].CssClass = "Test2";
        }
        else
        {
          for (int index = 0; index < 9; ++index)
            e.Row.Cells[index].CssClass = "Test1";
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

    protected void btnPDF_Click(object sender, EventArgs e)
    {
      try
      {
        Random random = new Random();
        string str1 = "";
        string empty1 = string.Empty;
        string str2 = "";
        List<string> stringList1 = new List<string>();
        List<string> stringList2 = new List<string>();
        DataTable dataTable1 = new DataTable();
        int num = new Random().Next(0, 100);
        foreach (GridViewRow row in this.wdgList.Rows)
        {
          if (row.RowType == DataControlRowType.DataRow && (row.Cells[0].FindControl("chkRow") as CheckBox).Checked)
            str1 = str1 + (row.Cells[2].FindControl("IdEbilling") as Label).Text + "|";
        }
        DataTable dataTable2 = new RequirementManagementBL().EBillingExportMasiveUrl(str1.Substring(0, str1.Length - 1));
        for (int index = 0; index < dataTable2.Rows.Count; ++index)
        {
          if (Convert.ToInt32(dataTable2.Rows[index]["estado"]) != 100)
          {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            MercurioWS mercurioWs = new MercurioWS();
            CPE_DOC_TRIBUTARO_BE cpeDocTributaroBe1 = new CPE_DOC_TRIBUTARO_BE();
            CPE_DOC_TRIBUTARO_BE cpeDocTributaroBe2 = mercurioWs.ReadDocumentCPE(ConfigurationManager.AppSettings["UserServiceSofnet"].ToString(), ConfigurationManager.AppSettings["PasswordServiceSofnet"].ToString(), dataTable2.Rows[index]["serie"].ToString(), dataTable2.Rows[index]["correlativo"].ToString(), dataTable2.Rows[index]["tipoComprobante"].ToString(), dataTable2.Rows[index]["ruc"].ToString(), dataTable2.Rows[index]["tipodocumento"].ToString(), false, false, true);
            List<string> stringList3 = new List<string>();
            string empty2 = string.Empty;
            string tempPath = Path.GetTempPath();
            string str3 = dataTable2.Rows[index]["ruc"].ToString() + "-" + dataTable2.Rows[index]["tipoComprobante"].ToString() + "-" + dataTable2.Rows[index]["documento"].ToString() + ".pdf";
            string path = tempPath + str3;
            str2 = tempPath + num.ToString() + ".pdf";
            byte[] docTribPdf = cpeDocTributaroBe2.DOC_TRIB_PDF;
            if (System.IO.File.Exists(path))
              System.IO.File.Delete(path);
            using (FileStream fileStream = System.IO.File.Create(path))
              fileStream.Write(docTribPdf, 0, docTribPdf.Length);
            if (docTribPdf.Length > 1000)
              stringList1.Add(path.ToString());
            else
              stringList2.Add(dataTable2.Rows[index]["documento"].ToString());
          }
          if (index % 10 == 0 && index != 0)
            Thread.Sleep(random.Next(1, 5) * 1000);
        }
        if (dataTable2.Rows.Count != stringList1.Count)
        {
          DataTable dataTable3 = (DataTable) this.Session["UniversalList"];
          foreach (string str4 in stringList2)
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable3.Rows)
            {
              if (str4.ToString() == row["NumeroDocumento"].ToString())
                row["i_StateRow"] = (object) 0;
            }
          }
          this.wdgList.DataSource = (object) dataTable3;
          this.wdgList.DataBind();
          this.Session["UniversalList"] = (object) dataTable3;
          Message.SetMessage(this.lblMessage, new HandledException(1, "los registros en Rojo no tienen documento electronico, Verificar"));
        }
        this.ViewState["pathMergePDF"] = (object) str2;
        this.ViewState["InFiles"] = (object) stringList1;
        this.Export();
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

    private void Export()
    {
      string script = "ExportPDFAll();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      string str = this.ViewState["pathMergePDF"].ToString();
      List<string> stringList = new List<string>();
      if (!BillingExport.MergePDFs((List<string>) this.ViewState["InFiles"], str))
        return;
      byte[] buffer = System.IO.File.ReadAllBytes(str);
      this.Response.Clear();
      this.Response.AddHeader("content-disposition", "attachment; filename=reportecomprobantes.pdf");
      this.Response.BinaryWrite(buffer);
    }
  }
}
