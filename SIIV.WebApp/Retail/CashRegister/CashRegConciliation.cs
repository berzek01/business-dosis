// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Retail.CashRegister.CashRegConciliation
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Retail.CashRegister
{
  public class CashRegConciliation : Page
  {
    private SIIV.BE.Requirement objRequirement;
    private SystemUser objUserBE;
    private SIIV.BE.CashRegister ObjBox;
    private MaintenanceOperation currentOperation = MaintenanceOperation.None;
    protected HiddenField H1;
    protected UpdatePanel UpdatePanel1;
    protected DropDownList wddLocation;
    protected CheckBox chkGenerationBetween;
    protected Fecha wdpStartDate;
    protected Fecha wdpEndDate;
    protected TextBox txtFilter;
    protected Button wibSearch;
    protected Label lblMessage;
    protected GridView wdgCashRegisterList;
    protected Pager custPagerBatch;
    protected Button wibExcel;
    protected UpdatePanel UpdatePanel2;
    protected HtmlGenericControl idbox;
    protected TextBox txtConId;
    protected TextBox TxtCode;
    protected TextBox TxtMonto;
    protected TextBox txtOperacion;
    protected FilteredTextBoxExtender ftbeMac;
    protected RequiredFieldValidator rfvIdentification;
    protected ValidatorCalloutExtender ValidatorCalloutExtender1;
    protected TextBox txtObservation;
    protected FilteredTextBoxExtender txtObservation_FilteredTextBoxExtender;
    protected Panel pPositionLogicUse;
    protected TextBox txtShelf;
    protected FilteredTextBoxExtender ftbeAnaquel;
    protected TextBox txtRow;
    protected FilteredTextBoxExtender ftbeFila;
    protected TextBox txtColumn;
    protected FilteredTextBoxExtender ftbeColumna;
    protected Button wibAllocate;
    protected Button wibDeallocate;
    protected GridView wdgWarehouseLocationList;
    protected Pager custPagerClaimList;
    protected HtmlTableRow trManagementButtons;
    protected Button wibSave;
    protected Button wibCancel;
    protected HtmlTableRow trwibFinalze;
    protected Button wibFinalze;
    protected HiddenField hidWarehouseId;
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
        this.InitializeData();
        this.EnableControls(MaintenanceOperation.None);
        this.SetDatePicker();
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

    private void InitializeData()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
        int iLocationId = systemUser.i_LocationId;
        int? iCompanyId = systemUser.i_CompanyId;
        this.wddLocation.DataSource = (object) new LocationQueriesBL().GetLocationBy("", string.Empty, string.Empty);
        this.wddLocation.DataTextField = "v_Description";
        this.wddLocation.DataValueField = "i_LocationId";
        this.wddLocation.DataBind();
        this.wddLocation.SelectedValue = iLocationId.ToString();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        this.SearchCashRegister();
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

    private void SearchCashRegister()
    {
      try
      {
        DateTime dateTime;
        if (this.chkGenerationBetween.Checked)
        {
          dateTime = this.wdpEndDate.Value;
          if (dateTime.Subtract(this.wdpStartDate.Value).Days > 30 && this.txtFilter.Text.Trim() == "")
            throw new HandledException(1, "Si el intervalo de fechas excede a 30 dias, debe especificar la Caja");
        }
        if (!this.wdpStartDate.Enabled && !this.wdpEndDate.Enabled && this.txtFilter.Text.Trim() == "")
          throw new HandledException(1, "Debe especificar la Caja");
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
        int iLocationId = systemUser.i_LocationId;
        int num = systemUser.i_CompanyId.Value;
        string text = this.txtFilter.Text;
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
        this.SearchCashRegisterList(pintStartdate, pintFinishdate, 0, text, iLocationId, -1, 1, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchCashRegisterList(
      int pintStartdate,
      int pintFinishdate,
      int pintBoxId,
      string pstrDescription,
      int pintLocationId,
      int pintBoxStatus,
      int pintStatus,
      bool pboolLoadPager)
    {
      try
      {
        int startRowIndex = pboolLoadPager ? 1 : this.custPagerBatch.CurrentPageNumber;
        int maxRows = this.custPagerBatch.CurrentPageSize == 0 ? 10 : this.custPagerBatch.CurrentPageSize;
        int pinttotalRows;
        DataTable dataTable = new RequirementQueriesBL().CashRegConciliationQuery(pintStartdate, pintFinishdate, pintBoxId, pstrDescription, pintLocationId, pintBoxStatus, pintStatus, startRowIndex, maxRows, out pinttotalRows);
        int num = pinttotalRows;
        this.wdgCashRegisterList.DataSource = (object) dataTable;
        this.wdgCashRegisterList.DataBind();
        this.custPagerBatch.TotalPages = num % maxRows == 0 ? num / maxRows : num / maxRows + 1;
        this.custPagerBatch.TotalRecordCount = pinttotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerBatch.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void wibNew_Click(object sender, EventArgs e)
    {
      try
      {
        this.wibSave.Text = "Grabar";
        this.currentOperation = MaintenanceOperation.AddNew;
        this.ViewState.Add("currentOperation", (object) this.currentOperation);
        this.EnableControls(MaintenanceOperation.AddNew);
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

    private void EnableControls(MaintenanceOperation currentOperation)
    {
      try
      {
        switch (currentOperation)
        {
          case MaintenanceOperation.None:
            this.idbox.Visible = false;
            break;
          case MaintenanceOperation.AddNew:
            this.idbox.Visible = false;
            this.TxtMonto.Enabled = false;
            this.txtOperacion.Enabled = true;
            this.rfvIdentification.Enabled = true;
            this.wddLocation.Enabled = false;
            this.txtShelf.Enabled = true;
            this.txtRow.Enabled = true;
            this.txtColumn.Enabled = true;
            this.wibAllocate.Enabled = true;
            this.wibDeallocate.Enabled = true;
            this.wdgWarehouseLocationList.Enabled = true;
            this.lblMessage1.Visible = false;
            this.trManagementButtons.Visible = true;
            this.trwibFinalze.Visible = false;
            this.pPositionLogicUse.Visible = false;
            this.txtOperacion.Focus();
            string script1 = UtilDA.ActiveTabIndex("tabs", 1, "0");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script1, true);
            string script2 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script2, true);
            break;
          case MaintenanceOperation.Edit:
            this.idbox.Visible = false;
            this.txtOperacion.Enabled = true;
            this.rfvIdentification.Enabled = true;
            this.wddLocation.Enabled = false;
            this.txtShelf.Enabled = true;
            this.txtRow.Enabled = true;
            this.txtColumn.Enabled = true;
            this.wibAllocate.Enabled = true;
            this.wibDeallocate.Enabled = true;
            this.wdgWarehouseLocationList.Enabled = true;
            this.lblMessage1.Visible = false;
            this.trManagementButtons.Visible = true;
            this.trwibFinalze.Visible = false;
            this.wibSave.Enabled = true;
            string script3 = UtilDA.ActiveTabIndex("tabs", 1, "0");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script3, true);
            string script4 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script4, true);
            break;
          case MaintenanceOperation.Delete:
            this.idbox.Visible = false;
            this.txtOperacion.Enabled = false;
            this.rfvIdentification.Enabled = false;
            this.wddLocation.Enabled = false;
            this.txtShelf.Enabled = false;
            this.txtRow.Enabled = false;
            this.txtColumn.Enabled = false;
            this.wibAllocate.Enabled = false;
            this.wibDeallocate.Enabled = false;
            this.wdgWarehouseLocationList.Enabled = false;
            this.lblMessage1.Visible = false;
            this.trManagementButtons.Visible = true;
            this.trwibFinalze.Visible = false;
            string script5 = UtilDA.ActiveTabIndex("tabs", 1, "0");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script5, true);
            string script6 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script6, true);
            break;
          default:
            this.idbox.Visible = false;
            this.txtOperacion.Enabled = false;
            this.rfvIdentification.Enabled = false;
            this.wddLocation.Enabled = false;
            this.txtShelf.Enabled = false;
            this.txtRow.Enabled = false;
            this.txtColumn.Enabled = false;
            this.wibAllocate.Enabled = false;
            this.wibDeallocate.Enabled = false;
            this.wdgWarehouseLocationList.Enabled = false;
            this.trwibFinalze.Visible = true;
            this.trManagementButtons.Visible = false;
            this.pPositionLogicUse.Visible = false;
            string script7 = UtilDA.ActiveTabIndex("tabs", 0, "1");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script7, true);
            string script8 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script8, true);
            break;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperation"];
      SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
      switch (this.currentOperation)
      {
        case MaintenanceOperation.Edit:
          if (this.txtOperacion.Text.Trim() != "")
          {
            using (TransactionScope transactionScope = new TransactionScope())
            {
              new RequirementManagementBL().CashRegConciliation(Convert.ToInt32(this.txtConId.Text.Trim()), Convert.ToInt32(this.wddLocation.SelectedValue), this.txtOperacion.Text.Trim(), this.txtObservation.Text.Trim(), -1, Convert.ToInt32(systemUser.i_SystemUserId), 1);
              transactionScope.Complete();
            }
            this.txtFilter.Text = string.Empty;
            this.SearchCashRegister();
            this.currentOperation = MaintenanceOperation.Edit;
            this.ViewState.Add("currentOperation", (object) this.currentOperation);
            this.EnableControls(MaintenanceOperation.Edit);
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage1, new HandledException(2, "•&nbsp;Se registro la operacion satisfactoriamente."));
            this.wibSave.Enabled = false;
            break;
          }
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage1, enmMessageType.Warning, "Advertencia*****<br>Debe de Ingresar la Operacion.<br>&nbsp;&nbsp;");
          this.trwibFinalze.Visible = false;
          this.trManagementButtons.Visible = true;
          break;
      }
      this.HidePopup();
    }

    private SIIV.BE.CashRegister GetCurrentSupplier(enmTypeLoadData penuTypeLoadData)
    {
      try
      {
        SIIV.BE.CashRegister currentSupplier = new SIIV.BE.CashRegister();
        int int32 = Convert.ToInt32(this.ViewState["IndexWdgSupplierList"]);
        switch (penuTypeLoadData)
        {
          case enmTypeLoadData.SelectedRowsGrid:
            currentSupplier.i_Conid = this.wdgCashRegisterList.DataKeys[int32]["i_Conid"].ToString();
            currentSupplier.v_CashRegCode = this.wdgCashRegisterList.DataKeys[int32]["v_CashRegCode"].ToString();
            currentSupplier.v_AmountTotal = this.wdgCashRegisterList.DataKeys[int32]["f_AmountTotal"].ToString();
            currentSupplier.v_OperationNumber = this.wdgCashRegisterList.DataKeys[int32]["v_OperationNumber"].ToString();
            currentSupplier.v_Observation = this.wdgCashRegisterList.DataKeys[int32]["v_Observation"].ToString();
            break;
          case enmTypeLoadData.SelectedTextbox:
            if (!string.IsNullOrWhiteSpace(this.hidWarehouseId.Value))
              currentSupplier.i_Conid = this.hidWarehouseId.Value;
            currentSupplier.v_OperationNumber = this.txtOperacion.Text.Trim();
            currentSupplier.v_CashRegCode = this.TxtCode.Text.Trim();
            currentSupplier.v_AmountTotal = this.TxtMonto.Text.Trim();
            currentSupplier.i_Conid = this.txtConId.Text.Trim();
            currentSupplier.v_Observation = this.txtObservation.Text.Trim();
            break;
        }
        return currentSupplier;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ShowSupplierInfo(SIIV.BE.CashRegister objBox)
    {
      try
      {
        this.txtConId.Text = Convert.ToInt32(objBox.i_Conid).ToString();
        this.TxtCode.Text = objBox.v_CashRegCode;
        this.TxtMonto.Text = objBox.v_AmountTotal;
        this.txtOperacion.Text = objBox.v_OperationNumber;
        this.txtObservation.Text = objBox.v_Observation;
        this.hidWarehouseId.Value = objBox.i_Conid.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void wdgCashRegisterList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        int int32 = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = this.wdgCashRegisterList.Rows[int32];
        this.ViewState["IndexWdgSupplierList"] = (object) int32;
        if (e.CommandName == "Edit")
        {
          this.currentOperation = MaintenanceOperation.Edit;
          this.ShowSupplierInfo(this.GetCurrentSupplier(enmTypeLoadData.SelectedRowsGrid));
          this.EnableControls(MaintenanceOperation.Edit);
        }
        else if (e.CommandName == "PrintPDF")
        {
          this.ViewState["i_Conid"] = (object) this.wdgCashRegisterList.DataKeys[int32]["i_Conid"].ToString();
          string script = "ExportPDFAll();";
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        }
        this.ViewState["currentOperation"] = (object) this.currentOperation;
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

    protected void wdgCashRegisterList_PageIndexChanged(object sender, EventArgs e)
    {
    }

    protected void wdgCashRegisterList_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgCashRegisterList_RowEditing2(object sender, GridViewEditEventArgs e)
    {
    }

    protected void wdgSupplierList_PageIndexChanged(object sender, EventArgs e)
    {
    }

    protected void custPagerBatch_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        DateTime dateTime;
        if (this.chkGenerationBetween.Checked)
        {
          dateTime = this.wdpEndDate.Value;
          if (dateTime.Subtract(this.wdpStartDate.Value).Days > 30 && this.txtFilter.Text.Trim() == "")
            throw new HandledException(1, "Si el intervalo de fechas excede a 30 dias, debe especificar la Caja");
        }
        if (!this.wdpStartDate.Enabled && !this.wdpEndDate.Enabled && this.txtFilter.Text.Trim() == "")
          throw new HandledException(1, "Debe especificar la Caja");
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
        int iLocationId = systemUser.i_LocationId;
        int num = systemUser.i_CompanyId.Value;
        string text = this.txtFilter.Text;
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
        this.SearchCashRegisterList(pintStartdate, pintFinishdate, 0, text, iLocationId, -1, 1, false);
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

    protected void wibCancel_Click(object sender, EventArgs e)
    {
      try
      {
        this.SearchCashRegister();
        string script1 = UtilDA.ActiveTabIndex("tabs", 0, "1");
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script1, true);
        string script2 = "TabIndex();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "ScriptIndex", script2, true);
        this.EnableControls(MaintenanceOperation.None);
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage1, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage1, new HandledException(-100, ex));
      }
    }

    protected void chkGenerationBetween_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkGenerationBetween.Checked)
      {
        this.wdpStartDate.Enabled = true;
        this.wdpEndDate.Enabled = true;
        this.txtFilter.Text = "";
        this.wdgCashRegisterList.DataSource = (object) null;
        this.wdgCashRegisterList.DataBind();
      }
      else
      {
        this.wdpStartDate.Enabled = false;
        this.wdpEndDate.Enabled = false;
        this.txtFilter.Text = "";
        this.wdgCashRegisterList.DataSource = (object) null;
        this.wdgCashRegisterList.DataBind();
      }
    }

    protected void wibExcel_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.wdgCashRegisterList.Rows.Count == 0)
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

    private void ExportList()
    {
      try
      {
        DateTime dateTime;
        if (this.chkGenerationBetween.Checked)
        {
          dateTime = this.wdpEndDate.Value;
          if (dateTime.Subtract(this.wdpStartDate.Value).Days > 30 && this.txtFilter.Text.Trim() == "")
            throw new HandledException(1, "Si el intervalo de fechas excede a 30 dias, debe especificar la Caja");
        }
        if (!this.wdpStartDate.Enabled && !this.wdpEndDate.Enabled && this.txtFilter.Text.Trim() == "")
          throw new HandledException(1, "Debe especificar la Caja");
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
        int iLocationId = systemUser.i_LocationId;
        int num = systemUser.i_CompanyId.Value;
        string text = this.txtFilter.Text;
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
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = new RequirementQueriesBL().CashRegConciliationQuery(pintStartdate, pintFinishdate, 0, text, iLocationId, -1, 1, 0, 0, out int _);
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
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable objDataTable = (DataTable) this.Session["dtExport"];
        ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
        List<ClassColumns> classColumnsList = new List<ClassColumns>();
        foreach (DataControlField column in (StateManagedCollection) this.wdgCashRegisterList.Columns)
        {
          if (column.Visible && column.GetType().Name == "BoundField")
          {
            BoundField boundField = (BoundField) column;
            classColumnsList.Add(new ClassColumns(boundField.DataField, 1, (int) column.ControlStyle.Width.Value, boundField.HeaderText));
          }
        }
        exportToExcelDataGrid.clsTitle = classColumnsList;
        exportToExcelDataGrid.AgregarHojaLibro(objDataTable, "Consulta Conciliaciones");
        exportToExcelDataGrid.CerrarLibro();
        byte[] buffer = exportToExcelDataGrid.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=Conciliaciones.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
      }
      catch (Exception ex)
      {
      }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
      try
      {
        using (ReportDocument reportDocument = new ReportDocument())
        {
          Convert.ToInt32(this.ViewState["i_Requirement"], (IFormatProvider) CultureInfo.CurrentCulture);
          Convert.ToInt32(this.ViewState["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture);
          string filename = this.Server.MapPath("../CashRegister/ReportCashReg.rpt");
          reportDocument.Load(filename);
          DataTable cashRegisterList = new RequirementQueriesBL().GetCashRegisterList(Convert.ToInt32(this.ViewState["i_Conid"]), 0, "", "", 0, 3);
          reportDocument.SetDataSource(cashRegisterList);
          reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "Caja");
          reportDocument.Close();
          ((Component) reportDocument).Dispose();
        }
        GC.Collect();
      }
      catch (Exception ex)
      {
      }
    }
  }
}
