// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Retail.CashRegister.CashRegMaintenance
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Data;
using System.Globalization;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Retail.CashRegister
{
  public class CashRegMaintenance : Page
  {
    private SIIV.BE.Requirement objRequirement;
    private SystemUser objUserBE;
    private SIIV.BE.CashRegister ObjBox;
    private MaintenanceOperation currentOperation = MaintenanceOperation.None;
    protected HiddenField H1;
    protected UpdatePanel UpdatePanel1;
    protected DropDownList wddLocation;
    protected TextBox txtFilter;
    protected Button wibSearch;
    protected GridView wdgCashRegisterList;
    protected Pager custPagerBatch;
    protected Button wibNew;
    protected Label lblMessage;
    protected UpdatePanel UpdatePanel2;
    protected HtmlGenericControl idbox;
    protected TextBox txtBoxId;
    protected HtmlTableRow trPuntoEntrega;
    protected TextBox txtBoxCod;
    protected FilteredTextBoxExtender ftbeBoxCod;
    protected RequiredFieldValidator rfvBoxCod;
    protected ValidatorCalloutExtender ValidatorCalloutExtender3;
    protected TextBox txtIdentification;
    protected FilteredTextBoxExtender ftbeMac;
    protected RequiredFieldValidator rfvIdentification;
    protected ValidatorCalloutExtender ValidatorCalloutExtender1;
    protected TextBox txtMonto;
    protected FilteredTextBoxExtender ftbeMonto;
    protected RequiredFieldValidator rfvMonto;
    protected ValidatorCalloutExtender ValidatorCalloutExtender2;
    protected DropDownList wddBoxStatus;
    protected CheckBox chkStatus;
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

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.InitializeData();
        this.EnableControls(MaintenanceOperation.None);
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

    private void InitializeData()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
        int iLocationId = systemUser.i_LocationId;
        if (systemUser.i_CompanyId.GetValueOrDefault() == 1)
          this.trPuntoEntrega.Visible = false;
        else
          this.trPuntoEntrega.Visible = true;
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

    private void SearchCashRegister()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
        int iLocationId = systemUser.i_LocationId;
        int num = systemUser.i_CompanyId.Value;
        this.SearchCashRegisterList(0, this.txtFilter.Text, iLocationId, -1, 1, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchCashRegisterList(
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
        DataTable dataTable = new RequirementQueriesBL().CashRegisterUniversalQueryRead(pintBoxId, pstrDescription, pintLocationId, pintBoxStatus, pintStatus, startRowIndex, maxRows, out pinttotalRows);
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
        this.txtBoxCod.Text = "";
        this.txtBoxId.Text = "";
        this.txtIdentification.Text = "";
        this.txtMonto.Text = "0";
        this.chkStatus.Checked = true;
        this.wibSave.Text = "Grabar";
        this.currentOperation = MaintenanceOperation.AddNew;
        this.ViewState.Add("currentOperation", (object) this.currentOperation);
        this.EnableControls(MaintenanceOperation.AddNew);
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
            this.wibSave.Enabled = true;
            this.idbox.Visible = false;
            this.txtBoxCod.Enabled = true;
            this.rfvBoxCod.Enabled = true;
            this.txtIdentification.Enabled = true;
            this.rfvIdentification.Enabled = true;
            this.txtMonto.Enabled = true;
            this.rfvMonto.Enabled = true;
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
            this.wddBoxStatus.Enabled = true;
            this.pPositionLogicUse.Visible = false;
            this.wddBoxStatus.Enabled = false;
            this.wddBoxStatus.SelectedIndex = 4;
            this.txtBoxCod.Focus();
            string script1 = UtilDA.ActiveTabIndex("tabs", 1, "0");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script1, true);
            string script2 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script2, true);
            break;
          case MaintenanceOperation.Edit:
            this.wibSave.Enabled = true;
            this.idbox.Visible = false;
            this.txtBoxCod.Enabled = false;
            this.rfvBoxCod.Enabled = true;
            this.txtIdentification.Enabled = true;
            this.rfvIdentification.Enabled = true;
            this.txtMonto.Enabled = true;
            this.rfvMonto.Enabled = true;
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
            this.wddBoxStatus.Enabled = false;
            string script3 = UtilDA.ActiveTabIndex("tabs", 1, "0");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script3, true);
            string script4 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script4, true);
            break;
          case MaintenanceOperation.Delete:
            this.idbox.Visible = false;
            this.txtBoxCod.Enabled = false;
            this.rfvBoxCod.Enabled = false;
            this.txtIdentification.Enabled = false;
            this.rfvIdentification.Enabled = false;
            this.txtMonto.Enabled = false;
            this.rfvMonto.Enabled = false;
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
            this.wddBoxStatus.Enabled = false;
            this.wibSave.Enabled = true;
            string script5 = UtilDA.ActiveTabIndex("tabs", 1, "0");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script5, true);
            string script6 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script6, true);
            break;
          default:
            this.idbox.Visible = false;
            this.txtBoxCod.Enabled = false;
            this.rfvBoxCod.Enabled = false;
            this.txtIdentification.Enabled = false;
            this.rfvIdentification.Enabled = false;
            this.txtMonto.Enabled = false;
            this.rfvMonto.Enabled = false;
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
      bool flag;
      switch (this.currentOperation)
      {
        case MaintenanceOperation.AddNew:
          DataTable cashRegisterList1 = new RequirementQueriesBL().GetCashRegisterList(-1, systemUser.i_LocationId, this.txtBoxCod.Text.Trim(), this.txtIdentification.Text.Trim(), 0, 0);
          if (this.txtBoxCod.Text.Trim() != "" && this.txtIdentification.Text.Trim() != "" && this.txtMonto.Text.Trim() != "")
          {
            if (cashRegisterList1.Rows.Count == 0)
            {
              using (TransactionScope transactionScope = new TransactionScope())
              {
                flag = new RequirementManagementBL().RegisteCashRegister(0, Convert.ToInt32(this.wddLocation.SelectedValue), this.txtBoxCod.Text.Trim(), this.txtIdentification.Text.Trim(), Convert.ToDouble(this.txtMonto.Text.Trim()), Convert.ToInt32(this.wddBoxStatus.SelectedValue), 1, Convert.ToInt32(systemUser.i_SystemUserId), 1);
                transactionScope.Complete();
              }
              this.txtFilter.Text = string.Empty;
              this.SearchCashRegister();
              this.currentOperation = MaintenanceOperation.None;
              this.ViewState.Add("currentOperation", (object) this.currentOperation);
              this.EnableControls(MaintenanceOperation.AddNew);
              Message.SetMessage(this.lblMessage1, new HandledException(2, "•&nbsp;La Caja se Registro satisfactoriamente."));
              this.wibSave.Enabled = false;
              break;
            }
            Message.SetMessage(this.lblMessage1, enmMessageType.Warning, "Advertencia*****<br>Ya existe ese identificador registrado.<br>&nbsp;&nbsp;");
            this.trwibFinalze.Visible = false;
            this.trManagementButtons.Visible = true;
            break;
          }
          Message.SetMessage(this.lblMessage1, enmMessageType.Warning, "Advertencia*****<br>Debe ingresar todos los datos.<br>&nbsp;&nbsp;");
          this.trwibFinalze.Visible = false;
          this.trManagementButtons.Visible = true;
          break;
        case MaintenanceOperation.Edit:
          DataTable cashRegisterList2 = new RequirementQueriesBL().GetCashRegisterList(Convert.ToInt32(this.txtBoxId.Text.Trim()), systemUser.i_LocationId, this.txtBoxCod.Text.Trim(), this.txtIdentification.Text.Trim(), 0, 0);
          if (this.txtBoxCod.Text.Trim() != "" && this.txtIdentification.Text.Trim() != "" && this.txtMonto.Text.Trim() != "")
          {
            if (cashRegisterList2.Rows.Count == 0)
            {
              using (TransactionScope transactionScope = new TransactionScope())
              {
                flag = new RequirementManagementBL().RegisteCashRegister(Convert.ToInt32(this.txtBoxId.Text.Trim()), Convert.ToInt32(this.wddLocation.SelectedValue), this.txtBoxCod.Text.Trim(), this.txtIdentification.Text.Trim(), Convert.ToDouble(this.txtMonto.Text.Trim()), 0, 1, Convert.ToInt32(systemUser.i_SystemUserId), 2);
                transactionScope.Complete();
              }
              this.txtFilter.Text = string.Empty;
              this.SearchCashRegister();
              this.currentOperation = MaintenanceOperation.Edit;
              this.ViewState.Add("currentOperation", (object) this.currentOperation);
              this.EnableControls(MaintenanceOperation.Edit);
              Message.SetMessage(this.lblMessage1, new HandledException(2, "•&nbsp;La Caja se modificó satisfactoriamente."));
              this.wibSave.Enabled = false;
              break;
            }
            Message.SetMessage(this.lblMessage1, enmMessageType.Warning, "Advertencia*****<br>Ya existe ese identificador registrado.<br>&nbsp;&nbsp;");
            this.trwibFinalze.Visible = false;
            this.trManagementButtons.Visible = true;
            break;
          }
          Message.SetMessage(this.lblMessage1, enmMessageType.Warning, "Advertencia*****<br>Debe ingresar todos los datos.<br>&nbsp;&nbsp;");
          this.trwibFinalze.Visible = false;
          this.trManagementButtons.Visible = true;
          break;
        case MaintenanceOperation.Delete:
          using (TransactionScope transactionScope = new TransactionScope())
          {
            flag = new RequirementManagementBL().RegisteCashRegister(Convert.ToInt32(this.txtBoxId.Text.Trim()), Convert.ToInt32(this.wddLocation.SelectedValue), this.txtBoxCod.Text.Trim(), this.txtIdentification.Text.Trim(), Convert.ToDouble(this.txtMonto.Text.Trim()), 0, 1, Convert.ToInt32(systemUser.i_SystemUserId), 3);
            transactionScope.Complete();
          }
          this.txtFilter.Text = string.Empty;
          this.SearchCashRegister();
          this.currentOperation = MaintenanceOperation.None;
          this.ViewState.Add("currentOperation", (object) this.currentOperation);
          this.EnableControls(MaintenanceOperation.Delete);
          this.wibSave.Enabled = false;
          Message.SetMessage(this.lblMessage1, new HandledException(2, "•&nbsp;La Caja se modificó satisfactoriamente."));
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
            currentSupplier.i_idBoxCode = this.wdgCashRegisterList.DataKeys[int32]["i_CashRegId"].ToString();
            currentSupplier.v_BoxCode = this.wdgCashRegisterList.DataKeys[int32]["v_CashRegCode"].ToString();
            currentSupplier.v_Identification = this.wdgCashRegisterList.DataKeys[int32]["v_Identification"].ToString();
            currentSupplier.f_Money = double.Parse(this.wdgCashRegisterList.DataKeys[int32]["f_Money"].ToString());
            currentSupplier.i_BoxStatus = int.Parse(this.wdgCashRegisterList.DataKeys[int32]["i_CashRegStatus"].ToString());
            currentSupplier.v_Status = this.wdgCashRegisterList.DataKeys[int32]["i_Status"].ToString();
            if (this.wdgCashRegisterList.DataKeys[int32]["i_Status"].ToString().Equals("Activo"))
            {
              currentSupplier.i_Status = 1;
              break;
            }
            if (this.wdgCashRegisterList.DataKeys[int32]["i_Status"].ToString().Equals("Inactivo"))
            {
              currentSupplier.i_Status = 0;
              break;
            }
            break;
          case enmTypeLoadData.SelectedTextbox:
            if (!string.IsNullOrWhiteSpace(this.hidWarehouseId.Value))
              currentSupplier.v_BoxCode = this.hidWarehouseId.Value;
            currentSupplier.v_BoxCode = this.txtBoxCod.Text;
            currentSupplier.v_Identification = this.txtIdentification.Text;
            currentSupplier.f_Money = Convert.ToDouble(this.txtMonto.Text);
            currentSupplier.i_BoxStatus = Convert.ToInt32(this.wddBoxStatus.SelectedValue);
            currentSupplier.i_Status = !this.chkStatus.Checked ? 2 : 1;
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
        this.txtBoxId.Text = Convert.ToInt32(objBox.i_idBoxCode).ToString();
        this.txtBoxCod.Text = objBox.v_BoxCode;
        this.txtIdentification.Text = objBox.v_Identification;
        this.txtMonto.Text = Convert.ToDouble(objBox.f_Money).ToString();
        this.wddBoxStatus.SelectedValue = Convert.ToInt32(objBox.i_BoxStatus).ToString();
        if (objBox.i_Status == 1)
          this.chkStatus.Checked = true;
        this.hidWarehouseId.Value = objBox.v_BoxCode.ToString((IFormatProvider) CultureInfo.CurrentCulture);
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
        else if (e.CommandName == "Delete")
        {
          this.currentOperation = MaintenanceOperation.Delete;
          this.ShowSupplierInfo(this.GetCurrentSupplier(enmTypeLoadData.SelectedRowsGrid));
          this.EnableControls(MaintenanceOperation.Delete);
        }
        this.ViewState["currentOperation"] = (object) this.currentOperation;
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
    }

    protected void wibCancel_Click(object sender, EventArgs e)
    {
      try
      {
        string script1 = UtilDA.ActiveTabIndex("tabs", 0, "1");
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script1, true);
        string script2 = "TabIndex();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "ScriptIndex", script2, true);
        this.EnableControls(MaintenanceOperation.None);
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
  }
}
