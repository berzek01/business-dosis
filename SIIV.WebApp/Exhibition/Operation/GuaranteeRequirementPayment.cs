// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Operation.GuaranteeRequirementPayment
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Operation
{
  public class GuaranteeRequirementPayment : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected Label Label1;
    protected TextBox txtAssociatedName;
    protected Label Label2;
    protected TextBox txtPlateNew;
    protected FilteredTextBoxExtender txtPlateNew_FilteredTextBoxExtender;
    protected Button wibSearchPlate;
    protected Button btnReturnPopupConfirmation;
    protected Button wibExport;
    protected Button wibExportPDF;
    protected GridView wdgListPlate;
    protected Pager custPagerGRP;
    protected Label lblMessage;
    protected UpdatePanel UpdatePanel2;
    protected HtmlTable Table1;
    protected Label Label6;
    protected TextBox txtAssociatedNameEdit1;
    protected Label Label10;
    protected TextBox txtPlateNewEdit1;
    protected Button wibSearch;
    protected GridView wdgListEdit;
    protected Label Label12;
    protected TextBox txtAssociatedNameEdit;
    protected Label Label13;
    protected TextBox txtPlateNewEdit;
    protected Label Label14;
    protected DropDownList wddBank;
    protected Label Label15;
    protected Fecha wdpPaymentDate;
    protected Label Label17;
    protected TextBox txtOperationNumber;
    protected Label Label18;
    protected TextBox txtAmount;
    protected Label Label19;
    protected TextBox txtObservations;
    protected Button wibSave;
    protected Button wibCancel;
    protected Button wibReturn;
    protected Label lblMessage1;
    protected Button Button1;
    protected Button Button2;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        DataTable dtResult = new DataTable("Datos");
        this.TableColumns(dtResult);
        DataRow row = dtResult.NewRow();
        dtResult.Rows.Add(row);
        this.wdgListPlate.DataSource = (object) dtResult;
        this.wdgListPlate.DataBind();
        this.wdgListPlate.Rows[0].Visible = false;
        this.ViewState["t"] = (object) Convert.ToString(this.Request.QueryString["t"]);
        this.LoadBanks();
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

    private void TableColumns(DataTable dtResult)
    {
      dtResult.Columns.Add("i_ExhibitionWarrantyId", typeof (int));
      dtResult.Columns.Add("i_ExhibitionDetailId", typeof (int));
      dtResult.Columns.Add("v_AssociatedName", typeof (string));
      dtResult.Columns.Add("v_PlateNew", typeof (string));
      dtResult.Columns.Add("v_Status", typeof (string));
      dtResult.Columns.Add("Monto", typeof (int));
    }

    protected void wibSearchPlate_Click(object sender, EventArgs e)
    {
      try
      {
        this.SearchGuaranteeRequirement("Search");
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

    protected void wibExport_Click(object sender, EventArgs e)
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
      finally
      {
        this.HidePopup();
      }
    }

    protected void wdgListPlate_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        if (e.CommandName == "SelectImg")
        {
          GridViewRow row = this.wdgListPlate.Rows[Convert.ToInt32(e.CommandArgument)];
          this.txtAssociatedNameEdit1.Text = Convert.ToString(this.Page.Server.HtmlDecode(row.Cells[2].Text));
          this.txtPlateNewEdit1.Text = row.Cells[3].Text;
          if (row.Cells[4].Text != "Devuelto")
          {
            this.wibSearch_Click((object) null, (EventArgs) null);
            this.ClearControls();
            this.EnabledControls(false);
            string script = UtilDA.ActiveTabIndex("tabs", 1, "0");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script, true);
          }
          else
            Message.SetMessage(this.lblMessage, new HandledException(1, "Esta placa no se puede editar ya que ha sido devuelta"));
        }
        else
        {
          if (!(e.CommandName == "SelectDevolution"))
            return;
          int int32 = Convert.ToInt32(e.CommandArgument);
          string text = this.wdgListPlate.Rows[int32].Cells[4].Text;
          this.ViewState["i_ExhibitionWarrantyId"] = (object) Convert.ToInt32(this.wdgListPlate.DataKeys[int32]["i_ExhibitionWarrantyId"].ToString());
          if (text == "Registrado")
          {
            string empty = string.Empty;
            this.CreatePopUpServer("SIIV - Placas Especiales", "../../UserControls/PopupConfirmation.aspx?MessageTypeId=1&MessageText=¿Desea devolver la placa?", "350px", "190px");
          }
          else
            Message.SetMessage(this.lblMessage, new HandledException(1, "No se puede realizar la devolución porque esta placa no esta Registrada o ya fue Devuelta"));
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
      finally
      {
        this.HidePopup();
      }
    }

    protected void custPagerGRP_PageChanged(object sender, CustomPageChangeArgs e)
    {
      this.SearchGuaranteeRequirementList(this.txtAssociatedName.Text.TrimEnd(), this.txtPlateNew.Text.TrimEnd(), Convert.ToInt32(this.ViewState["t"]), "Search", false);
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      this.SearchGuaranteeRequirement("Edit");
    }

    protected void wdgListEdit_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        if (!(e.CommandName == "SelectImg"))
          return;
        this.ClearControls();
        this.EnabledControls(true);
        this.wibSearch.Enabled = false;
        this.wibSave.Enabled = true;
        this.wibCancel.Enabled = true;
        this.txtPlateNewEdit.Enabled = false;
        this.txtPlateNewEdit1.Enabled = false;
        this.txtAssociatedNameEdit.Enabled = false;
        this.txtAssociatedNameEdit1.Enabled = false;
        int int32_1 = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = this.wdgListEdit.Rows[int32_1];
        int int32_2 = Convert.ToInt32(this.wdgListEdit.DataKeys[int32_1]["i_ExhibitionWarrantyId"].ToString());
        int int32_3 = Convert.ToInt32(this.wdgListEdit.DataKeys[int32_1]["i_ExhibitionDetailId"].ToString());
        this.ViewState["i_ExhibitionWarrantyId"] = (object) int32_2;
        this.ViewState["i_ExhibitionDetailId"] = (object) int32_3;
        if (int32_2 > 0)
          this.GuaranteeRequirementGet(int32_2);
        this.txtAssociatedNameEdit.Text = row.Cells[1].Text;
        this.txtPlateNewEdit.Text = row.Cells[2].Text;
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

    protected void wibSave_Click(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        SIIV.BE.CustomCode.GuaranteeRequirementPayment pobjGuaranteeRequirementPayment = new SIIV.BE.CustomCode.GuaranteeRequirementPayment();
        pobjGuaranteeRequirementPayment.i_ExhibitionWarrantyId = Convert.ToInt32(this.ViewState["i_ExhibitionWarrantyId"], (IFormatProvider) CultureInfo.CurrentCulture);
        pobjGuaranteeRequirementPayment.i_ExhibitionDetailId = Convert.ToInt32(this.ViewState["i_ExhibitionDetailId"], (IFormatProvider) CultureInfo.CurrentCulture);
        pobjGuaranteeRequirementPayment.d_PaymentDate = Convert.ToDateTime((object) this.wdpPaymentDate.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        pobjGuaranteeRequirementPayment.i_BankTypeId = Convert.ToInt32(this.wddBank.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        pobjGuaranteeRequirementPayment.v_OperationNumber = this.txtOperationNumber.Text;
        pobjGuaranteeRequirementPayment.f_Amount = Convert.ToDecimal(this.txtAmount.Text, (IFormatProvider) CultureInfo.CurrentCulture);
        pobjGuaranteeRequirementPayment.v_Observations = this.txtObservations.Text;
        pobjGuaranteeRequirementPayment.i_Status = 1;
        pobjGuaranteeRequirementPayment.i_InsertUserId = systemUser.i_SystemUserId;
        pobjGuaranteeRequirementPayment.d_InsertDate = DateTime.Now;
        pobjGuaranteeRequirementPayment.i_UpdateUserId = systemUser.i_SystemUserId;
        pobjGuaranteeRequirementPayment.d_UpdateDate = DateTime.Now;
        string text = this.txtPlateNewEdit.Text;
        if (new GuaranteeRequirementManagementBL().GuaranteeRequirementInsert(pobjGuaranteeRequirementPayment, text))
        {
          Message.SetMessage(this.lblMessage, new HandledException(2, "Se registró correctamente la Garantía de la Placa"));
          this.ClearControls();
          this.EnabledControls(false);
          this.wibSave.Enabled = false;
          this.wibCancel.Enabled = false;
          this.wibSearchPlate.Enabled = true;
          this.txtPlateNew.Enabled = true;
          this.txtAssociatedName.Enabled = true;
          this.SearchGuaranteeRequirement("Edit");
        }
        else
          Message.SetMessage(this.lblMessage1, new HandledException(1, "Se encontró un problema en el registro de la Garantía de las Placa"));
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage1, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage1, new HandledException(-100, ex));
      }
    }

    protected void wibCancel_Click(object sender, EventArgs e)
    {
      this.ClearControls();
      this.EnabledControls(false);
      this.txtPlateNewEdit1.Enabled = true;
      this.txtAssociatedNameEdit1.Enabled = true;
      this.wibSearch.Enabled = true;
      this.wibSave.Enabled = false;
      this.wibCancel.Enabled = false;
    }

    protected void wibReturn_Click(object sender, EventArgs e)
    {
      string script = UtilDA.ActiveTabIndex("tabs", 0, "1");
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script, true);
    }

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      try
      {
        if (!new GuaranteeRequirementManagementBL().GuaranteeRequirementDevolution(Convert.ToInt32(this.ViewState["i_ExhibitionWarrantyId"])))
          return;
        Message.SetMessage(this.lblMessage, new HandledException(2, "Se registró correctamente la Devolución de la Placa"));
        this.wibSearchPlate_Click((object) null, (EventArgs) null);
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
        foreach (DataControlField column in (StateManagedCollection) this.wdgListPlate.Columns)
        {
          if (column.Visible && column.GetType().Name == "BoundField")
          {
            BoundField boundField = (BoundField) column;
            classColumnsList.Add(new ClassColumns(boundField.DataField, 1, (int) column.ControlStyle.Width.Value, boundField.HeaderText));
          }
        }
        exportToExcelDataGrid.clsTitle = classColumnsList;
        exportToExcelDataGrid.AgregarHojaLibro(objDataTable, "Listado Garantias");
        exportToExcelDataGrid.CerrarLibro();
        byte[] buffer = exportToExcelDataGrid.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=ListadoGarantias.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
      }
      catch (Exception ex)
      {
      }
    }

    public void LoadBanks()
    {
      try
      {
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) ("" + SystemParameterGroups.AffiliatedBank.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
          (object) "",
          (object) "1",
          (object) "1"
        });
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            this.wddBank.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        }
        this.wddBank.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- Seleccione - ", "0"));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchGuaranteeRequirement(string pstrPage)
    {
      try
      {
        string pstrAssociatedName = "";
        string pstrPlateNew = "";
        int int32 = Convert.ToInt32(this.ViewState["t"]);
        switch (pstrPage)
        {
          case "Search":
            pstrAssociatedName = this.txtAssociatedName.Text.TrimEnd();
            pstrPlateNew = this.txtPlateNew.Text.TrimEnd();
            break;
          case "Edit":
            pstrAssociatedName = this.txtAssociatedNameEdit1.Text.TrimEnd();
            pstrPlateNew = this.txtPlateNewEdit1.Text.TrimEnd();
            break;
        }
        this.SearchGuaranteeRequirementList(pstrAssociatedName, pstrPlateNew, int32, pstrPage, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SearchGuaranteeRequirementList(
      string pstrAssociatedName,
      string pstrPlateNew,
      int i_PlateTypeId,
      string pstrPage,
      bool pboolLoadPager)
    {
      try
      {
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerGRP.CurrentPageNumber;
        int pintMaxRows = this.custPagerGRP.CurrentPageSize == 0 ? 10 : this.custPagerGRP.CurrentPageSize;
        int pintTotalRows;
        DataTable all = new GuaranteeRequirementPaymentBL().GuaranteeRequirementGetAll(pstrAssociatedName, pstrPlateNew, i_PlateTypeId, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        if (all == null || all.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
          this.HidePopup();
        }
        int num = pintTotalRows;
        switch (pstrPage)
        {
          case "Search":
            this.wdgListPlate.DataSource = (object) all;
            this.wdgListPlate.DataBind();
            this.custPagerGRP.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
            this.custPagerGRP.TotalRecordCount = pintTotalRows;
            if (!pboolLoadPager)
              break;
            this.custPagerGRP.LoadPager();
            break;
          case "Edit":
            this.wdgListEdit.DataSource = (object) all;
            this.wdgListEdit.DataBind();
            break;
        }
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
        string text1 = this.txtAssociatedName.Text;
        string text2 = this.txtPlateNew.Text;
        int int32 = Convert.ToInt32(this.ViewState["t"]);
        DataTable dataTable = new DataTable();
        this.Session["dtExport"] = (object) new GuaranteeRequirementPaymentBL().GuaranteeRequirementGetAll(text1, text2, int32, 0, 0, out int _);
        this.Export();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void Export()
    {
      string script = "Export();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void ClearControls()
    {
      this.txtAssociatedNameEdit.Text = "";
      this.txtPlateNewEdit.Text = "";
      this.wddBank.SelectedIndex = -1;
      this.wdpPaymentDate.Value = DateTime.Now;
      this.txtOperationNumber.Text = "";
      this.txtAmount.Text = "";
      this.txtObservations.Text = "";
    }

    private void EnabledControls(bool enabled)
    {
      this.txtAssociatedNameEdit.Enabled = enabled;
      this.txtPlateNewEdit.Enabled = enabled;
      this.wddBank.Enabled = enabled;
      this.wdpPaymentDate.Enabled = enabled;
      this.txtOperationNumber.Enabled = enabled;
      this.txtAmount.Enabled = enabled;
      this.txtObservations.Enabled = enabled;
    }

    private void GuaranteeRequirementGet(int i_ExhibitionWarrantyId)
    {
      try
      {
        DataTable dataTable = new GuaranteeRequirementPaymentBL().GuaranteeRequirementGet(i_ExhibitionWarrantyId);
        if (dataTable == null || dataTable.Rows.Count == 0)
          return;
        this.wddBank.SelectedValue = dataTable.Rows[0]["i_BankTypeId"].ToString();
        this.wdpPaymentDate.Value = Convert.ToDateTime(dataTable.Rows[0]["d_PaymentDate"].ToString());
        this.txtOperationNumber.Text = dataTable.Rows[0]["v_OperationNumber"].ToString();
        this.txtAmount.Text = dataTable.Rows[0]["f_Amount"].ToString();
        this.txtObservations.Text = dataTable.Rows[0]["v_Observations"].ToString();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void wibExportPDF_Click(object sender, EventArgs e)
    {
      string script = "ExportPDF();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable dataTable = new DataTable();
        DataTable all = new GuaranteeRequirementPaymentBL().GuaranteeRequirementGetAll(this.txtAssociatedName.Text.TrimEnd(), this.txtPlateNew.Text.TrimEnd(), Convert.ToInt32(this.ViewState["t"]), 0, 0, out int _);
        this.ViewState["dt"] = (object) all;
        if (all == null)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
        if (all.Rows.Count == 0)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_No_Datos_Exportar);
        Document document = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 10f);
        string str = this.Server.MapPath("../Docs/") + "Download.pdf_" + DateTime.Now.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture) + DateTime.Now.ToString("HHmmss", (IFormatProvider) CultureInfo.CurrentCulture) + ".pdf";
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
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void GenerateDoc(Document document)
    {
      try
      {
        if (this.ViewState["dt"] == null)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO);
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = (DataTable) this.ViewState["dt"];
        DataTable _dtRequirement = dataTable2.Clone();
        for (int index = 0; index < dataTable2.Rows.Count; ++index)
          _dtRequirement.ImportRow(dataTable2.Rows[index]);
        string[] strArray = new string[this.wdgListPlate.Columns.Count];
        List<string> stringList = new List<string>();
        List<float> floatList = new List<float>();
        for (int index = 0; index < this.wdgListPlate.Columns.Count; ++index)
        {
          DataControlField column = this.wdgListPlate.Columns[index];
          if (column.Visible && this.wdgListPlate.Columns[index].HeaderText != "" && column.GetType().Name == "BoundField")
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
              _dtRequirement.Columns[index1].ColumnName = this.wdgListPlate.Columns[index2].HeaderText;
              DataControlField column = this.wdgListPlate.Columns[index2];
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

    protected void wdgListEdit_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgListEdit_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void wdgListPlate_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgListPlate_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
  }
}
