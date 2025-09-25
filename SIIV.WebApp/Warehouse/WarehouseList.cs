// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.WarehouseList
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Data;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse
{
  public class WarehouseList : Page
  {
    private MaintenanceOperation currentOperation = MaintenanceOperation.None;
    protected HiddenField H1;
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtFilter;
    protected Button wibSearch;
    protected GridView wdgWarehouseList;
    protected Pager custPagerBatch;
    protected Button wibNew;
    protected Label lblMessage;
    protected UpdatePanel UpdatePanel2;
    protected CheckBox ChkIsSubWarehouse;
    protected DropDownList wddParentWarehouse;
    protected CheckBox ChkIsPortaPlaca;
    protected HtmlTableRow trPuntoEntrega;
    protected DropDownList wddLocation;
    protected TextBox txtDescription;
    protected FilteredTextBoxExtender ftbeDescription;
    protected RequiredFieldValidator rfvDescription;
    protected ValidatorCalloutExtender ValidatorCalloutExtender1;
    protected TextBox txtAddress;
    protected FilteredTextBoxExtender ftbeAddress;
    protected RequiredFieldValidator rfvAddress;
    protected ValidatorCalloutExtender ValidatorCalloutExtender2;
    protected DropDownList wddWarehouseUse;
    protected TextBox txtKeyReference;
    protected Label lblSugerencia;
    protected CheckBox chkPositionLogic;
    protected CheckBox chkUseMethodPEPS;
    protected Button btnIndexar;
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
    protected Label message;
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
        this.EnableControls();
        this.custPagerClaimList.TotalPages = 1;
        this.custPagerClaimList.LoadPager();
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

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        this.SearchWarehouses();
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

    protected void custPagerBatch_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
        int iLocationId = systemUser.i_LocationId;
        int num = systemUser.i_CompanyId.Value;
        this.SearchWarehousesList(0, this.txtFilter.Text, iLocationId, -1, false);
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

    protected void wdgWarehouseList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        GridViewRow row = this.wdgWarehouseList.Rows[Convert.ToInt32(e.CommandArgument)];
        if (e.CommandName.Equals("Edit", StringComparison.CurrentCulture))
        {
          SIIV.BE.Warehouse currentWarehouse = this.GetCurrentWarehouse(row);
          this.hidWarehouseId.Value = currentWarehouse.i_WarehouseId.ToString();
          this.ShowWarehouseInfo(currentWarehouse);
          this.wibSave.Text = "Grabar";
          this.currentOperation = MaintenanceOperation.Edit;
          this.ViewState.Add("currentOperation", (object) this.currentOperation);
          this.EnableControls();
        }
        else
        {
          if (!e.CommandName.Equals("Delete", StringComparison.CurrentCulture))
            return;
          SIIV.BE.Warehouse currentWarehouse = this.GetCurrentWarehouse(row);
          this.hidWarehouseId.Value = currentWarehouse.i_WarehouseId.ToString();
          this.ShowWarehouseInfo(currentWarehouse);
          this.wibSave.Text = "Eliminar";
          this.currentOperation = MaintenanceOperation.Delete;
          this.ViewState.Add("currentOperation", (object) this.currentOperation);
          this.EnableControls();
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

    protected void wdgWarehouseList_PageIndexChanged(object sender, EventArgs e)
    {
      this.SearchWarehouses();
    }

    protected void wibNew_Click(object sender, EventArgs e)
    {
      try
      {
        SIIV.BE.Warehouse objWarehouse = new SIIV.BE.Warehouse();
        objWarehouse.i_LocationId = new int?((this.Session["SystemUser"] as SystemUser).i_LocationId);
        this.hidWarehouseId.Value = objWarehouse.i_WarehouseId.ToString();
        this.ShowWarehouseInfo(objWarehouse);
        this.wibSave.Text = "Grabar";
        this.currentOperation = MaintenanceOperation.AddNew;
        this.ViewState.Add("currentOperation", (object) this.currentOperation);
        this.EnableControls();
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

    protected void ChkIsSubWarehouse_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        this.wddParentWarehouse.Visible = this.ChkIsSubWarehouse.Checked;
        if (this.ChkIsSubWarehouse.Checked)
        {
          this.LoadParentWarehouseCombo();
        }
        else
        {
          this.txtDescription.Focus();
          this.txtAddress.Enabled = true;
          this.txtAddress.Text = "";
          this.wddParentWarehouse.SelectedValue = "0";
          this.wddWarehouseUse.Enabled = true;
          this.wddWarehouseUse.SelectedValue = "-1";
          this.lblMessage.Visible = false;
          this.message.Visible = false;
          this.txtShelf.Text = "";
          this.txtRow.Text = "";
          this.txtColumn.Text = "";
        }
        if (Convert.ToInt32(this.hidWarehouseId.Value) != 0)
          return;
        this.wdgWarehouseLocationList.DataSource = (object) null;
        this.wdgWarehouseLocationList.DataBind();
        this.pPositionLogicUse.Visible = false;
        this.chkPositionLogic.Checked = false;
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

    protected void ChkIsPortaPlaca_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.ChkIsPortaPlaca.Checked)
        {
          this.txtKeyReference.Text = "userportaplaca";
          this.txtKeyReference.Enabled = false;
          this.txtDescription.Focus();
        }
        else
        {
          this.txtKeyReference.Text = string.Empty;
          this.txtKeyReference.Enabled = true;
          this.txtDescription.Focus();
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.message, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.message, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void wddParentWarehouse_SelectionChanged(object sender, EventArgs e)
    {
      this.chkPositionLogic.Checked = false;
      this.pPositionLogicUse.Visible = false;
      if (!this.ChkIsSubWarehouse.Checked)
        return;
      if (this.wddParentWarehouse.SelectedValue == "0")
      {
        this.txtDescription.Focus();
        this.txtAddress.Enabled = true;
        this.txtAddress.Text = "";
        this.wddWarehouseUse.Enabled = true;
        this.wddWarehouseUse.SelectedValue = "-1";
      }
      else
      {
        this.txtDescription.Focus();
        int iLocationId = (this.Session["SystemUser"] as SystemUser).i_LocationId;
        DataTable warehouseBy = new WarehouseQueriesBL().GetWarehouseBy(Convert.ToInt32(this.wddParentWarehouse.SelectedValue), "", iLocationId, -1);
        this.txtAddress.Text = warehouseBy.Rows[0]["v_Address"].ToString();
        this.txtAddress.Enabled = false;
        this.wddWarehouseUse.SelectedValue = warehouseBy.Rows[0]["i_WarehouseTypeUseId"].ToString();
        this.wddWarehouseUse.Enabled = false;
      }
    }

    protected void chkPositionLogic_CheckedChanged(object sender, EventArgs e)
    {
      this.message.Text = string.Empty;
      this.message.Visible = false;
      if (this.ChkIsSubWarehouse.Checked)
      {
        if (this.wddParentWarehouse.SelectedValue == "0")
        {
          this.HidePopup();
          Message.SetMessage(this.message, enmMessageType.Warning, "Advertencia****<br> &nbsp;&nbsp; •&nbsp; Debe seleccionar Almacén Padre. <br> &nbsp;&nbsp;");
          return;
        }
        if (this.txtDescription.Text == string.Empty)
        {
          this.HidePopup();
          Message.SetMessage(this.message, enmMessageType.Warning, "Advertencia****<br> &nbsp;&nbsp; •&nbsp; Debe ingresar Nombre del Sub-Almacén para poder asignarle posiciones. <br> &nbsp;&nbsp;");
          return;
        }
      }
      else if (this.txtDescription.Text == string.Empty)
      {
        this.HidePopup();
        Message.SetMessage(this.message, enmMessageType.Warning, "Advertencia****<br> &nbsp;&nbsp; •&nbsp; Debe ingresar Nombre del Almacén para poder usar lógica de posicionamiento. <br> &nbsp;&nbsp;");
        return;
      }
      if (!this.chkPositionLogic.Checked)
      {
        if (this.ViewState["dtFilteredLocations"] is DataTable dataTable)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            row["i_isAssigned"] = (object) 0;
        }
        this.ViewState["dtFilteredLocations"] = (object) dataTable;
      }
      this.pPositionLogicUse.Visible = this.chkPositionLogic.Checked;
      this.lblMessage.Visible = false;
      this.message.Visible = false;
      this.Session["CHECKED_ITEMS"] = (object) null;
      this.Session["NO_CHECKED_ITEMS"] = (object) null;
      this.RefreshWarehouseLocations(true);
      this.HidePopup();
    }

    protected void btnIndexar_Click(object sender, EventArgs e)
    {
      this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperation"];
      if (this.currentOperation != MaintenanceOperation.AddNew)
        return;
      new WarehouseManagementBL().IndexPosition();
      this.HidePopup();
    }

    protected void wdgWarehouseLocationList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (!e.CommandName.Equals("Check", StringComparison.CurrentCulture) || this.currentOperation == MaintenanceOperation.Delete)
        return;
      int int32_1 = Convert.ToInt32(e.CommandArgument);
      GridViewRow row1 = this.wdgWarehouseLocationList.Rows[int32_1];
      DataTable dataTable = this.ViewState["dtFilteredLocations"] as DataTable;
      dataTable.Columns["b_IsReserved"].ReadOnly = false;
      int int32_2 = Convert.ToInt32(this.wdgWarehouseLocationList.DataKeys[int32_1]["i_LocationWarehouseId"].ToString());
      if (!this.ChkIsSubWarehouse.Checked)
      {
        int int32_3 = Convert.ToInt32(row1.Cells[6].Text);
        int num = 0;
        foreach (DataRow row2 in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (Convert.ToInt32(row2["i_LocationWarehouseId"]) == int32_2)
          {
            num = Convert.ToInt32(row2["i_isAssigned"]);
            if (num == 1)
            {
              if (int32_3 != 0)
                return;
              row2["i_isAssigned"] = (object) 0;
              break;
            }
            row2["i_isAssigned"] = (object) 1;
            break;
          }
        }
        this.RefreshWarehouseLocations();
        if (num != 1)
          ;
        this.wdgWarehouseLocationList.DataBind();
      }
      else
      {
        foreach (DataRow row3 in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (Convert.ToInt32(row3["i_LocationWarehouseId"]) == int32_2)
          {
            int int32_4 = Convert.ToInt32(row3["b_IsReserved"]);
            int int32_5 = Convert.ToInt32(row3["i_CurrentQuantity"]);
            if (int32_4 == 1)
            {
              if (int32_5 != 0)
                return;
              row3["b_IsReserved"] = (object) 0;
            }
            else
              row3["b_IsReserved"] = (object) 1;
          }
        }
        this.ViewState["dtFilteredLocations"] = (object) dataTable;
        this.RefreshWarehouseLocations();
      }
    }

    protected void wdgWarehouseLocationList_PageIndexChanged(object sender, EventArgs e)
    {
    }

    protected void wdgWarehouseLocationList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowIndex < 0)
        return;
      CheckBox control = (CheckBox) e.Row.FindControl("chkCheck");
      int rowIndex = e.Row.RowIndex;
      int int32 = Convert.ToInt32(e.Row.Cells[6].Text);
      string str1 = this.wdgWarehouseLocationList.DataKeys[rowIndex]["i_WarehouseId"].ToString() == "" ? "0" : this.wdgWarehouseLocationList.DataKeys[rowIndex]["i_WarehouseId"].ToString();
      string str2 = this.wdgWarehouseLocationList.DataKeys[rowIndex]["i_SubWarehouseId"].ToString() == "" ? "0" : this.wdgWarehouseLocationList.DataKeys[rowIndex]["i_SubWarehouseId"].ToString();
      string str3 = this.wdgWarehouseLocationList.DataKeys[rowIndex]["i_LocationWarehouseId"].ToString() == "" ? "0" : this.wdgWarehouseLocationList.DataKeys[rowIndex]["i_LocationWarehouseId"].ToString();
      ArrayList arrayList = (ArrayList) this.Session["CHECKED_ITEMS"];
      if (!this.ChkIsSubWarehouse.Checked)
      {
        if (int32 > 0 || control.Checked && Convert.ToInt32(str1) != Convert.ToInt32(this.hidWarehouseId.Value))
        {
          control.Enabled = false;
        }
        else
        {
          control.Enabled = true;
          if (control.Checked)
            e.Row.CssClass = "Test";
        }
      }
      else if (int32 > 0 || control.Checked && Convert.ToInt32(str2) != Convert.ToInt32(this.hidWarehouseId.Value))
      {
        control.Enabled = false;
      }
      else
      {
        control.Enabled = true;
        if (control.Checked)
          e.Row.CssClass = "Test";
      }
      if (arrayList == null)
        return;
      for (int index = 0; index < arrayList.Count; ++index)
      {
        if (str3 == arrayList[index].ToString())
        {
          control.Enabled = true;
          e.Row.CssClass = "Test";
        }
      }
    }

    protected void custPagerClaimList_OnPageChanged(object sender, CustomPageChangeArgs e)
    {
      this.RefreshWarehouseLocations();
    }

    protected void wibAllocate_Click(object sender, EventArgs e)
    {
      string strShelf = this.txtShelf.Text.Trim();
      string text1 = this.txtRow.Text;
      string text2 = this.txtColumn.Text;
      if (string.IsNullOrEmpty(strShelf))
      {
        Message.SetMessage(this.message, enmMessageType.Warning, "<br>&nbsp;&nbsp;Adventencia: <br> •&nbsp;Debe ingresar por lo menos un Anaquel para asignar.");
      }
      else
      {
        int num = this.Allocate(strShelf, text1, text2, true);
        if (num > 0)
        {
          this.RefreshWarehouseLocations();
          Message.SetMessage(this.message, enmMessageType.Success, "******* <br> Se asignaron " + num.ToString() + " posiciones.");
        }
        else if (num < 0 && num != -1)
          Message.SetMessage(this.message, enmMessageType.Warning, "<br>&nbsp;&nbsp;Adventencia: <br> •&nbsp;No se encontraron posiciones que coincidan con los criterios ingresados.");
      }
      this.HidePopup();
    }

    protected void wibDeallocate_Click(object sender, EventArgs e)
    {
      string strShelf = this.txtShelf.Text.Trim();
      string text1 = this.txtRow.Text;
      string text2 = this.txtColumn.Text;
      if (string.IsNullOrEmpty(strShelf))
      {
        Message.SetMessage(this.message, enmMessageType.Warning, "<br>&nbsp;&nbsp;Adventencia: <br> •&nbsp;Debe ingresar por lo menos un anaquel para liberar.");
        this.trwibFinalze.Visible = false;
        this.trManagementButtons.Visible = true;
      }
      else
      {
        int num = this.Allocate(strShelf, text1, text2, false);
        if (num > 0)
        {
          this.RefreshWarehouseLocations();
          Message.SetMessage(this.message, enmMessageType.Success, "******** <br>•&nbsp; Se liberaron" + num.ToString() + " posiciones.");
          this.trwibFinalze.Visible = false;
          this.trManagementButtons.Visible = true;
        }
        else if (num < 0 && num != -1)
        {
          Message.SetMessage(this.message, enmMessageType.Warning, "<br>&nbsp;&nbsp;Adventencia: <br> •&nbsp;No se encontraron posiciones que coincidan con los criterios ingresados.");
          this.trwibFinalze.Visible = false;
          this.trManagementButtons.Visible = true;
        }
      }
      this.HidePopup();
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperation"];
      SIIV.BE.Warehouse objWarehouse = this.ReadWarehouseInfo();
      switch (this.currentOperation)
      {
        case MaintenanceOperation.AddNew:
          if (this.CheckWarehouseNameAvailable(objWarehouse))
          {
            if (this.chkPositionLogic.Checked && !this.DataValidate(false))
            {
              this.HidePopup();
              Message.SetMessage(this.message, enmMessageType.Warning, "Advertencia*****<br>No se puede crear el almacén porque tiene lógica de posicionamiento y no tiene asignado anaqueles");
              this.trwibFinalze.Visible = false;
              this.trManagementButtons.Visible = true;
              return;
            }
            this.InsertNewWarehouse(objWarehouse);
            this.txtFilter.Text = string.Empty;
            this.SearchWarehouses();
            this.currentOperation = MaintenanceOperation.None;
            this.ViewState.Add("currentOperation", (object) this.currentOperation);
            this.EnableControls();
            this.wdgWarehouseLocationList.DataSource = (object) null;
            this.wdgWarehouseLocationList.DataBind();
            break;
          }
          Message.SetMessage(this.message, enmMessageType.Warning, "Advertencia*****<br>Ya existe un almacén con el nombre especificado.<br>&nbsp;&nbsp;");
          this.trwibFinalze.Visible = false;
          this.trManagementButtons.Visible = true;
          break;
        case MaintenanceOperation.Edit:
          if (this.CheckWarehouseNameAvailable(objWarehouse))
          {
            if (this.chkPositionLogic.Checked && !this.DataValidate(true))
            {
              this.HidePopup();
              Message.SetMessage(this.message, enmMessageType.Warning, "Advertencia*****<br>No se puede editar el almacen porque tiene logica de posicionamiento y no tiene asignado anaqueles.<br>&nbsp;&nbsp;");
              this.trwibFinalze.Visible = false;
              this.trManagementButtons.Visible = true;
              return;
            }
            this.UpdateWarehouse(objWarehouse);
            this.txtFilter.Text = string.Empty;
            this.SearchWarehouses();
            this.currentOperation = MaintenanceOperation.None;
            this.ViewState.Add("currentOperation", (object) this.currentOperation);
            this.EnableControls();
            this.wdgWarehouseLocationList.DataSource = (object) null;
            this.wdgWarehouseLocationList.DataBind();
            break;
          }
          Message.SetMessage(this.message, enmMessageType.Warning, "Advertencia*****<br>Ya existe un almacén con el nombre especificado.<br>&nbsp;&nbsp;");
          this.trwibFinalze.Visible = false;
          this.trManagementButtons.Visible = true;
          break;
        case MaintenanceOperation.Delete:
          this.DeleteWarehouse(objWarehouse);
          this.txtFilter.Text = string.Empty;
          this.SearchWarehouses();
          this.currentOperation = MaintenanceOperation.None;
          this.ViewState.Add("currentOperation", (object) this.currentOperation);
          this.EnableControls();
          this.wdgWarehouseLocationList.DataSource = (object) null;
          this.wdgWarehouseLocationList.DataBind();
          break;
      }
      this.HidePopup();
    }

    protected void wibCancel_Click(object sender, EventArgs e)
    {
      this.lblMessage1.Visible = false;
      this.rfvAddress.Enabled = false;
      this.rfvDescription.Enabled = false;
      this.txtDescription.Focus();
      this.txtAddress.Enabled = true;
      this.txtAddress.Text = "";
      this.wddParentWarehouse.Visible = false;
      this.wddParentWarehouse.SelectedValue = "0";
      this.wddWarehouseUse.Enabled = true;
      this.wddWarehouseUse.SelectedValue = "-1";
      this.lblMessage.Visible = false;
      this.message.Visible = false;
      this.txtShelf.Text = "";
      this.txtRow.Text = "";
      this.txtColumn.Text = "";
      this.ChkIsSubWarehouse.Checked = false;
      this.chkPositionLogic.Checked = false;
      this.pPositionLogicUse.Visible = false;
      string script1 = UtilDA.ActiveTabIndex("tabs", 0, "1");
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script1, true);
      string script2 = "TabIndex();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "ScriptIndex", script2, true);
      this.HidePopup();
      this.custPagerClaimList.CleanPager();
      this.Session.Remove("CHECKED_ITEMS");
      this.Session.Remove("NO_CHECKED_ITEMS");
      this.wdgWarehouseLocationList.DataSource = (object) (DataTable) null;
      this.wdgWarehouseLocationList.DataBind();
    }

    protected void wibFinalze_Click(object sender, EventArgs e)
    {
      string script1 = UtilDA.ActiveTabIndex("tabs", 0, "1");
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script1, true);
      string script2 = "TabIndex();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "ScriptIndex", script2, true);
      this.HidePopup();
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
        if (systemUser.i_LocationId != 33)
          this.chkUseMethodPEPS.Style.Add("display", "none");
        else
          this.chkPositionLogic.Style.Add("display", "none");
        this.Session["CHECKED_ITEMS"] = (object) null;
        this.Session["NO_CHECKED_ITEMS"] = (object) null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void EnableControls()
    {
      try
      {
        switch (this.currentOperation)
        {
          case MaintenanceOperation.AddNew:
            this.txtDescription.Enabled = true;
            this.rfvDescription.Enabled = true;
            this.txtAddress.Enabled = true;
            this.rfvAddress.Enabled = true;
            this.wddLocation.Enabled = false;
            this.txtShelf.Enabled = true;
            this.txtRow.Enabled = true;
            this.txtColumn.Enabled = true;
            this.wibAllocate.Enabled = true;
            this.wibDeallocate.Enabled = true;
            this.wdgWarehouseLocationList.Enabled = true;
            this.lblMessage1.Visible = false;
            this.message.Visible = false;
            this.trManagementButtons.Visible = true;
            this.trwibFinalze.Visible = false;
            this.ChkIsSubWarehouse.Enabled = true;
            this.ChkIsSubWarehouse.Checked = false;
            this.wddParentWarehouse.Enabled = true;
            this.btnIndexar.Enabled = true;
            this.wddWarehouseUse.Enabled = true;
            this.pPositionLogicUse.Visible = false;
            this.ChkIsPortaPlaca.Enabled = true;
            this.txtKeyReference.Enabled = true;
            this.chkPositionLogic.Enabled = true;
            this.txtDescription.Focus();
            string script1 = UtilDA.ActiveTabIndex("tabs", 1, "0");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script1, true);
            string script2 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script2, true);
            break;
          case MaintenanceOperation.Edit:
            this.txtDescription.Enabled = true;
            this.rfvDescription.Enabled = true;
            this.txtAddress.Enabled = true;
            this.rfvAddress.Enabled = true;
            this.wddLocation.Enabled = false;
            this.txtShelf.Enabled = true;
            this.txtRow.Enabled = true;
            this.txtColumn.Enabled = true;
            this.wibAllocate.Enabled = true;
            this.wibDeallocate.Enabled = true;
            this.wdgWarehouseLocationList.Enabled = true;
            this.lblMessage1.Visible = false;
            this.message.Visible = false;
            this.trManagementButtons.Visible = true;
            this.trwibFinalze.Visible = false;
            this.ChkIsSubWarehouse.Enabled = false;
            this.btnIndexar.Enabled = false;
            string script3 = UtilDA.ActiveTabIndex("tabs", 1, "0");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script3, true);
            string script4 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script4, true);
            break;
          case MaintenanceOperation.Delete:
            this.txtDescription.Enabled = false;
            this.rfvDescription.Enabled = false;
            this.txtAddress.Enabled = false;
            this.rfvAddress.Enabled = false;
            this.wddLocation.Enabled = false;
            this.txtShelf.Enabled = false;
            this.txtRow.Enabled = false;
            this.txtColumn.Enabled = false;
            this.wibAllocate.Enabled = false;
            this.wibDeallocate.Enabled = false;
            this.wdgWarehouseLocationList.Enabled = false;
            this.lblMessage1.Visible = false;
            this.message.Visible = false;
            this.trManagementButtons.Visible = true;
            this.trwibFinalze.Visible = false;
            this.ChkIsPortaPlaca.Enabled = false;
            this.txtKeyReference.Enabled = false;
            this.chkPositionLogic.Enabled = false;
            this.btnIndexar.Enabled = false;
            string script5 = UtilDA.ActiveTabIndex("tabs", 1, "0");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script5, true);
            string script6 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script6, true);
            break;
          default:
            this.txtDescription.Enabled = false;
            this.rfvDescription.Enabled = false;
            this.txtAddress.Enabled = false;
            this.rfvAddress.Enabled = false;
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

    private void SearchWarehouses()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
        int iLocationId = systemUser.i_LocationId;
        int num = systemUser.i_CompanyId.Value;
        this.SearchWarehousesList(0, this.txtFilter.Text, iLocationId, -1, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchWarehousesList(
      int pintWarehouseId,
      string pstrDescription,
      int pintLocationId,
      int pintWarehouseTypeUseId,
      bool pboolLoadPager)
    {
      try
      {
        if (!pboolLoadPager)
          this.custPagerBatch.CleanPager();
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerBatch.CurrentPageNumber;
        int pintMaxRows = this.custPagerBatch.CurrentPageSize == 0 ? 10 : this.custPagerBatch.CurrentPageSize;
        int pintTotalRows;
        DataTable warehouseByPag = new WarehouseQueriesBL().GetWarehouseByPag(pintWarehouseId, pstrDescription, pintLocationId, pintWarehouseTypeUseId, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        int num = pintTotalRows;
        this.wdgWarehouseList.DataSource = (object) warehouseByPag;
        this.wdgWarehouseList.DataBind();
        this.custPagerBatch.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
        this.custPagerBatch.TotalRecordCount = pintTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerBatch.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ShowWarehouseInfo(SIIV.BE.Warehouse objWarehouse)
    {
      try
      {
        int? nullable = objWarehouse.i_LocationId;
        if (nullable.HasValue)
        {
          DropDownList wddLocation = this.wddLocation;
          nullable = objWarehouse.i_LocationId;
          string str = nullable.Value.ToString();
          wddLocation.SelectedValue = str;
        }
        else
          this.wddLocation.SelectedValue = "0";
        this.txtDescription.Text = objWarehouse.v_Description;
        this.txtAddress.Text = objWarehouse.v_Address;
        CheckBox chkPositionLogic = this.chkPositionLogic;
        nullable = objWarehouse.i_UsePositionLogic;
        int num1 = nullable.GetValueOrDefault() == 1 ? 1 : 0;
        chkPositionLogic.Checked = num1 != 0;
        CheckBox chkUseMethodPeps = this.chkUseMethodPEPS;
        nullable = objWarehouse.i_WarehouseTypeId;
        int num2 = nullable.GetValueOrDefault() == 1 ? 1 : 0;
        chkUseMethodPeps.Checked = num2 != 0;
        try
        {
          DropDownList wddWarehouseUse = this.wddWarehouseUse;
          nullable = objWarehouse.i_WarehouseTypeUseId;
          string str = nullable.ToString();
          wddWarehouseUse.SelectedValue = str;
        }
        catch
        {
          this.wddWarehouseUse.SelectedValue = "-1";
        }
        this.ChkIsSubWarehouse.Checked = objWarehouse.b_IsReserved.HasValue && objWarehouse.b_IsReserved.Value;
        this.ChkIsSubWarehouse.Enabled = this.ChkIsSubWarehouse.Checked;
        this.wddParentWarehouse.Visible = this.ChkIsSubWarehouse.Checked;
        this.txtKeyReference.Text = objWarehouse.v_KeyReference;
        if (this.ChkIsSubWarehouse.Checked)
        {
          this.ChkIsSubWarehouse_CheckedChanged((object) null, (EventArgs) null);
          this.txtAddress.Enabled = false;
          this.wddParentWarehouse.Enabled = false;
          this.ChkIsSubWarehouse.Enabled = false;
          this.wddWarehouseUse.Enabled = false;
        }
        try
        {
          this.wddParentWarehouse.SelectedValue = objWarehouse.i_ParentWarehouseId.ToString();
        }
        catch
        {
          this.wddParentWarehouse.SelectedValue = "0";
        }
        this.txtShelf.Text = string.Empty;
        this.txtRow.Text = string.Empty;
        this.txtColumn.Text = string.Empty;
        if (objWarehouse.i_UsePositionLogic.GetValueOrDefault() != 1)
          return;
        this.chkPositionLogic_CheckedChanged((object) null, (EventArgs) null);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void RefreshWarehouseLocations(bool pboolLoadPager = false)
    {
      this.custPagerClaimList.CleanPager();
      int num1 = pboolLoadPager ? 1 : this.custPagerClaimList.CurrentPageNumber;
      int pintstartRowIndex = num1 == 0 ? 1 : num1;
      int pintmaxRows = this.custPagerClaimList.CurrentPageSize == 0 ? 10 : this.custPagerClaimList.CurrentPageSize;
      int iLocationId = (this.Session["SystemUser"] as SystemUser).i_LocationId;
      int pinttotalRows;
      DataTable dataTable = this.ChkIsSubWarehouse.Checked ? new SubWarehouseQueriesBL().GetSubWarehouseBy(Convert.ToInt32(this.wddParentWarehouse.SelectedValue), pintstartRowIndex, pintmaxRows, out pinttotalRows) : new ShelfQueriesBL().GetByShelfPositions(this.hidWarehouseId.Value == "0" ? -1 : Convert.ToInt32(this.hidWarehouseId.Value), iLocationId, pintstartRowIndex, pintmaxRows, out pinttotalRows);
      this.ViewState["dtFilteredLocations"] = (object) dataTable;
      this.RemeberOldValues();
      int num2 = pinttotalRows;
      this.wdgWarehouseLocationList.DataSource = (object) (this.ViewState["dtFilteredLocations"] as DataTable);
      this.wdgWarehouseLocationList.DataBind();
      this.custPagerClaimList.TotalPages = num2 % pintmaxRows == 0 ? num2 / pintmaxRows : num2 / pintmaxRows + 1;
      this.custPagerClaimList.TotalRecordCount = pinttotalRows;
      if (!pboolLoadPager || dataTable.Rows.Count <= 0)
        return;
      this.custPagerClaimList.LoadPager();
    }

    private void LoadParentWarehouseCombo()
    {
      try
      {
        DataTable dataTable = this.Session["SystemUser"] != null ? new DataView(new WarehouseQueriesBL().GetWarehouseBy(0, "", (this.Session["SystemUser"] as SystemUser).i_LocationId, -1), "ISNULL(b_IsReserved,0) = 0", "i_WarehouseId", DataViewRowState.CurrentRows).ToTable() : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
        DataRow row = dataTable.NewRow();
        row["i_WarehouseId"] = (object) 0;
        row["v_Description"] = (object) "- Seleccione Almacén Padre -";
        row["v_Address"] = (object) string.Empty;
        row["i_Status"] = (object) 1;
        row["i_LocationId"] = (object) 0;
        row["v_Description2"] = (object) string.Empty;
        row["i_UsePositionLogic"] = (object) 0;
        row["i_WarehouseTypeId"] = (object) 0;
        dataTable.Rows.InsertAt(row, 0);
        this.wddParentWarehouse.DataSource = (object) dataTable;
        this.wddParentWarehouse.DataTextField = "v_Description";
        this.wddParentWarehouse.DataValueField = "i_WarehouseId";
        this.wddParentWarehouse.SelectedValue = "0";
        this.wddParentWarehouse.DataBind();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private bool CheckWarehouseNameAvailable(SIIV.BE.Warehouse objWarehouse)
    {
      return new WarehouseQueriesBL().CheckUniqueWarehouseName(objWarehouse.i_WarehouseId, objWarehouse.v_Description, objWarehouse.i_LocationId.Value) == 0;
    }

    private void InsertNewWarehouse(SIIV.BE.Warehouse objWarehouse)
    {
      TransactionOptions transactionOptions = new TransactionOptions()
      {
        Timeout = new TimeSpan(0, 0, 5, 1)
      };
      WarehouseManagementBL warehouseManagementBl = new WarehouseManagementBL();
      try
      {
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
        {
          int intWarehouseId = warehouseManagementBl.WarehouseInsert(objWarehouse);
          if (this.chkPositionLogic.Checked)
            this.UpdateWarehouseLocations(intWarehouseId, true);
          transactionScope.Complete();
          Message.SetMessage(this.lblMessage, enmMessageType.Success, "Correcto******<br>Almacén creado satisfactoriamente");
        }
      }
      catch (Exception ex)
      {
        this.trManagementButtons.Visible = false;
        Message.SetMessage(this.lblMessage, enmMessageType.Error, "Error*****<br>" + ex.Message);
        this.trwibFinalze.Visible = true;
      }
    }

    private void UpdateWarehouse(SIIV.BE.Warehouse objWarehouse)
    {
      TransactionOptions transactionOptions = new TransactionOptions()
      {
        Timeout = new TimeSpan(0, 0, 5, 1)
      };
      WarehouseManagementBL warehouseManagementBl = new WarehouseManagementBL();
      try
      {
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
        {
          warehouseManagementBl.WarehouseUpdate(objWarehouse);
          if (this.chkPositionLogic.Checked)
            this.UpdateWarehouseLocations(objWarehouse.i_WarehouseId, true);
          transactionScope.Complete();
          Message.SetMessage(this.lblMessage, enmMessageType.Success, "<br> &nbsp;&nbsp;  Almacén actualizado satisfactoriamente. <br> &nbsp;&nbsp; ");
          this.trwibFinalze.Visible = true;
        }
      }
      catch (Exception ex)
      {
        this.trManagementButtons.Visible = false;
        Message.SetMessage(this.lblMessage, enmMessageType.Error, "<br>&nbsp;&nbsp;Error:<br>&nbsp;&nbsp; •&nbsp;" + ex.Message + "<br>&nbsp;&nbsp;");
        this.trwibFinalze.Visible = true;
      }
    }

    private void DeleteWarehouse(SIIV.BE.Warehouse objWarehouse)
    {
      TransactionOptions transactionOptions = new TransactionOptions()
      {
        Timeout = new TimeSpan(0, 0, 5, 1)
      };
      try
      {
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
        {
          new WarehouseManagementBL().WarehouseDelete(objWarehouse.i_WarehouseId);
          if (this.chkPositionLogic.Checked)
            this.UpdateWarehouseLocations(objWarehouse.i_WarehouseId, false);
          transactionScope.Complete();
          Message.SetMessage(this.lblMessage, enmMessageType.Success, "<br> &nbsp;&nbsp; •&nbsp;Almacén Eliminado satisfactoriamente. <br> &nbsp;&nbsp;");
          this.trwibFinalze.Visible = true;
        }
      }
      catch (Exception ex)
      {
        this.trManagementButtons.Visible = false;
        Message.SetMessage(this.lblMessage, enmMessageType.Error, "<br>&nbsp;&nbsp;Error:<br>&nbsp;&nbsp; •&nbsp;" + ex.Message + "<br>&nbsp;&nbsp;");
        this.trwibFinalze.Visible = true;
      }
    }

    private void UpdateWarehouseLocations(int intWarehouseId, bool bolInsertOrUpdate)
    {
      string empty = string.Empty;
      this.lblMessage.Visible = false;
      this.RemeberOldValues();
      WarehouseManagementBL warehouseManagementBl = new WarehouseManagementBL();
      if (this.Session["CHECKED_ITEMS"] != null)
      {
        ArrayList arrayList = (ArrayList) this.Session["CHECKED_ITEMS"];
        for (int index = 0; index < arrayList.Count; ++index)
          warehouseManagementBl.SetShelfByWarehouse((int) arrayList[index], intWarehouseId, bolInsertOrUpdate, this.ChkIsSubWarehouse.Checked, true);
      }
      if (this.Session["NO_CHECKED_ITEMS"] == null)
        return;
      ArrayList arrayList1 = (ArrayList) this.Session["NO_CHECKED_ITEMS"];
      for (int index = 0; index < arrayList1.Count; ++index)
        warehouseManagementBl.SetShelfByWarehouse((int) arrayList1[index], intWarehouseId, bolInsertOrUpdate, this.ChkIsSubWarehouse.Checked, false);
    }

    private void RemeberOldValues()
    {
      ArrayList arrayList1 = new ArrayList();
      ArrayList arrayList2 = new ArrayList();
      if (this.Session["CHECKED_ITEMS"] != null)
        arrayList1 = (ArrayList) this.Session["CHECKED_ITEMS"];
      if (this.Session["NO_CHECKED_ITEMS"] != null)
        arrayList2 = (ArrayList) this.Session["NO_CHECKED_ITEMS"];
      foreach (GridViewRow row in this.wdgWarehouseLocationList.Rows)
      {
        CheckBox control = (CheckBox) row.FindControl("chkCheck");
        if (control != null)
        {
          int int32 = Convert.ToInt32(this.wdgWarehouseLocationList.DataKeys[row.RowIndex]["i_LocationWarehouseId"].ToString());
          if (control.Checked)
          {
            if (!arrayList1.Contains((object) int32) && control.Enabled)
              arrayList1.Add((object) int32);
            arrayList2.Remove((object) int32);
          }
          else
          {
            arrayList1.Remove((object) int32);
            if (!arrayList2.Contains((object) int32) && control.Enabled)
              arrayList2.Add((object) int32);
          }
        }
      }
      if (arrayList1 != null && arrayList1.Count > 0)
        this.Session["CHECKED_ITEMS"] = (object) arrayList1;
      if (arrayList2 != null && arrayList2.Count > 0)
        this.Session["NO_CHECKED_ITEMS"] = (object) arrayList2;
      DataTable dataTable = this.ViewState["dtFilteredLocations"] as DataTable;
      if (arrayList1 != null && arrayList1.Count > 0)
      {
        for (int index = 0; index < arrayList1.Count; ++index)
        {
          if (dataTable.Select("i_LocationWarehouseId=" + arrayList1[index]?.ToString()).Length != 0)
            dataTable.Select("i_LocationWarehouseId=" + arrayList1[index]?.ToString())[0]["i_isAssigned"] = (object) true;
        }
      }
      if (arrayList2 != null && arrayList2.Count > 0)
      {
        for (int index = 0; index < arrayList2.Count; ++index)
        {
          if (dataTable.Select("i_LocationWarehouseId=" + arrayList2[index]?.ToString()).Length != 0)
            dataTable.Select("i_LocationWarehouseId=" + arrayList2[index]?.ToString())[0]["i_isAssigned"] = (object) false;
        }
      }
      this.ViewState["dtFilteredLocations"] = (object) dataTable;
    }

    private bool DataValidate(bool blEdit)
    {
      int num1 = 0;
      int num2 = 0;
      this.RemeberOldValues();
      if (this.Session["CHECKED_ITEMS"] != null)
        num1 = (this.Session["CHECKED_ITEMS"] as ArrayList).Count;
      if (this.Session["NO_CHECKED_ITEMS"] != null)
        num2 = (this.Session["NO_CHECKED_ITEMS"] as ArrayList).Count;
      bool flag = num1 > 0 || num2 > 0;
      return ((num1 > 0 ? 1 : (num2 > 0 ? 1 : 0)) | (blEdit ? 1 : 0)) != 0;
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private SIIV.BE.Warehouse GetCurrentWarehouse(GridViewRow _selectedrow)
    {
      try
      {
        SIIV.BE.Warehouse currentWarehouse = new SIIV.BE.Warehouse();
        currentWarehouse.i_WarehouseId = Convert.ToInt32(this.wdgWarehouseList.DataKeys[_selectedrow.RowIndex]["i_WarehouseId"].ToString());
        if (!string.IsNullOrEmpty(_selectedrow.Cells[2].Text))
          currentWarehouse.v_Description = _selectedrow.Cells[2].Text;
        if (!string.IsNullOrEmpty(_selectedrow.Cells[4].Text))
          currentWarehouse.v_Address = _selectedrow.Cells[4].Text;
        if (!string.IsNullOrEmpty(this.wdgWarehouseList.DataKeys[_selectedrow.RowIndex]["i_LocationId"].ToString()))
          currentWarehouse.i_LocationId = new int?(Convert.ToInt32(this.wdgWarehouseList.DataKeys[_selectedrow.RowIndex]["i_LocationId"].ToString()));
        if (!string.IsNullOrEmpty(this.wdgWarehouseList.DataKeys[_selectedrow.RowIndex]["i_UsePositionLogic"].ToString()))
          currentWarehouse.i_UsePositionLogic = new int?(Convert.ToInt32(this.wdgWarehouseList.DataKeys[_selectedrow.RowIndex]["i_UsePositionLogic"].ToString()));
        if (!string.IsNullOrEmpty(this.wdgWarehouseList.DataKeys[_selectedrow.RowIndex]["i_WarehouseTypeId"].ToString()))
          currentWarehouse.i_WarehouseTypeId = new int?(Convert.ToInt32(this.wdgWarehouseList.DataKeys[_selectedrow.RowIndex]["i_WarehouseTypeId"].ToString()));
        if (!string.IsNullOrEmpty(this.wdgWarehouseList.DataKeys[_selectedrow.RowIndex]["i_WarehouseTypeUseId"].ToString()))
          currentWarehouse.i_WarehouseTypeUseId = new int?(Convert.ToInt32(this.wdgWarehouseList.DataKeys[_selectedrow.RowIndex]["i_WarehouseTypeUseId"].ToString()));
        if (!string.IsNullOrEmpty(this.wdgWarehouseList.DataKeys[_selectedrow.RowIndex]["b_IsReserved"].ToString()))
          currentWarehouse.b_IsReserved = new bool?(Convert.ToBoolean(this.wdgWarehouseList.DataKeys[_selectedrow.RowIndex]["b_IsReserved"].ToString()));
        if (!string.IsNullOrEmpty(this.wdgWarehouseList.DataKeys[_selectedrow.RowIndex]["i_ParentWarehouseId"].ToString()))
          currentWarehouse.i_ParentWarehouseId = new int?(Convert.ToInt32(this.wdgWarehouseList.DataKeys[_selectedrow.RowIndex]["i_ParentWarehouseId"].ToString()));
        if (!string.IsNullOrEmpty(this.wdgWarehouseList.DataKeys[_selectedrow.RowIndex]["v_KeyReference"].ToString()))
          currentWarehouse.v_KeyReference = this.wdgWarehouseList.DataKeys[_selectedrow.RowIndex]["v_KeyReference"].ToString();
        return currentWarehouse;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private SIIV.BE.Warehouse ReadWarehouseInfo()
    {
      DataTable dataTable = this.Session["SystemUser"] as DataTable;
      SIIV.BE.Warehouse warehouse = new SIIV.BE.Warehouse();
      if (!string.IsNullOrWhiteSpace(this.hidWarehouseId.Value))
        warehouse.i_WarehouseId = Convert.ToInt32(this.hidWarehouseId.Value);
      warehouse.v_Description = this.txtDescription.Text;
      warehouse.v_Address = this.txtAddress.Text;
      warehouse.i_LocationId = new int?(Convert.ToInt32(this.wddLocation.SelectedValue));
      warehouse.i_WarehouseTypeId = new int?(Convert.ToInt32(this.chkUseMethodPEPS.Checked));
      warehouse.i_UsePositionLogic = new int?(Convert.ToInt32(this.chkPositionLogic.Checked));
      warehouse.i_WarehouseTypeUseId = new int?(Convert.ToInt32(this.wddWarehouseUse.SelectedValue));
      warehouse.b_IsReserved = new bool?(this.ChkIsSubWarehouse.Checked);
      warehouse.i_ParentWarehouseId = this.wddParentWarehouse.SelectedIndex != -1 ? new int?(Convert.ToInt32(this.wddParentWarehouse.SelectedValue)) : new int?();
      warehouse.v_KeyReference = this.txtKeyReference.Text != string.Empty ? this.txtKeyReference.Text : (string) null;
      return warehouse;
    }

    private int Allocate(string strShelf, string strRow, string strColumn, bool bolAllocate)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      int iLocationId = (this.Session["SystemUser"] as SystemUser).i_LocationId;
      int pinttotalRows;
      DataTable dataTable = this.ChkIsSubWarehouse.Checked ? new SubWarehouseQueriesBL().GetSubWarehouseBy(Convert.ToInt32(this.wddParentWarehouse.SelectedValue), 0, 0, out pinttotalRows) : new ShelfQueriesBL().GetByShelfPositions(this.hidWarehouseId.Value == "0" ? -1 : Convert.ToInt32(this.hidWarehouseId.Value), iLocationId, 0, 0, out pinttotalRows);
      dataTable.Columns["b_IsReserved"].ReadOnly = false;
      ArrayList arrayList1 = new ArrayList();
      if (dataTable != null)
      {
        char ch1;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (row["v_Desciption"].ToString().Trim().ToUpper().Equals(strShelf.ToUpper(), StringComparison.CurrentCulture))
          {
            if (string.IsNullOrEmpty(strRow) && string.IsNullOrEmpty(strColumn))
            {
              if (Convert.ToInt32(row["i_CurrentQuantity"]) != 0)
                ++num2;
            }
            else if (string.IsNullOrEmpty(strColumn))
            {
              if (row["i_PositionX"].ToString().Equals(strRow, StringComparison.CurrentCulture) && Convert.ToInt32(row["i_CurrentQuantity"]) != 0)
                ++num3;
            }
            else if (string.IsNullOrEmpty(strRow))
            {
              ch1 = Convert.ToChar(row["i_PositionY"]);
              if (ch1.ToString().Equals(strColumn, StringComparison.CurrentCulture) && Convert.ToInt32(row["i_CurrentQuantity"]) != 0)
                ++num4;
            }
            else
            {
              int num5;
              if (row["i_PositionX"].ToString().Trim().Equals(strRow, StringComparison.CurrentCulture))
              {
                ch1 = Convert.ToChar(row["i_PositionY"]);
                num5 = ch1.ToString().Equals(strColumn, StringComparison.CurrentCulture) ? 1 : 0;
              }
              else
                num5 = 0;
              if (num5 != 0 && Convert.ToInt32(row["i_CurrentQuantity"]) != 0)
              {
                Message.SetMessage(this.message, enmMessageType.Warning, "*****<BR> La posicion solicitada esta actualmente asignada a otro almacén.");
                return -1;
              }
            }
          }
        }
        if (num4 > 0 && !bolAllocate)
        {
          Message.SetMessage(this.message, enmMessageType.Warning, "*****<BR> No se puede liberar, existen posiciones ya asignadas.");
          return -1;
        }
        if (num4 > 0 && bolAllocate)
        {
          Message.SetMessage(this.message, enmMessageType.Warning, "*****<BR> No se puede asignar, existen posiciones ya asignadas.");
          return -1;
        }
        if (num3 > 0 && !bolAllocate)
        {
          Message.SetMessage(this.message, enmMessageType.Warning, "*****<BR> No se puede liberar, existen posiciones ya asignadas.");
          return -1;
        }
        if (num3 > 0 && bolAllocate)
        {
          Message.SetMessage(this.message, enmMessageType.Warning, "*****<BR> No se puede asignar, existen posiciones ya asignadas.");
          return -1;
        }
        if (num2 > 0 && !bolAllocate)
        {
          Message.SetMessage(this.message, enmMessageType.Warning, "*****<BR> No se puede liberar el anaquel, existen posiciones ya asignadas.");
          return -1;
        }
        if (num2 > 0 && bolAllocate)
        {
          Message.SetMessage(this.message, enmMessageType.Warning, "*****<BR> No se puede asignar el anaquel, existen posiciones ya asignadas.");
          return -1;
        }
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          int num6 = (int) row["i_isAssigned"];
          char ch2;
          if (row["v_Desciption"].ToString().Trim().ToUpper().Equals(strShelf.ToUpper(), StringComparison.CurrentCulture))
          {
            if (string.IsNullOrEmpty(strRow) && string.IsNullOrEmpty(strColumn))
            {
              if (!this.ChkIsSubWarehouse.Checked)
              {
                if (num6 == 1 && Convert.ToInt32(row["i_CurrentQuantity"]) != 0)
                {
                  Message.SetMessage(this.message, enmMessageType.Warning, "*****<BR> La posicion que intenta liberar ya esta siendo usada.");
                  return -1;
                }
                row["i_isAssigned"] = (object) (bolAllocate ? 1 : 0);
                arrayList1.Add(row["i_LocationWarehouseId"]);
                ++num1;
              }
              else
              {
                row["b_IsReserved"] = (object) (bolAllocate ? 1 : 0);
                arrayList1.Add(row["i_LocationWarehouseId"]);
                ++num1;
              }
            }
            else if (string.IsNullOrEmpty(strColumn))
            {
              if (row["i_PositionX"].ToString().Equals(strRow, StringComparison.CurrentCulture))
              {
                if (!this.ChkIsSubWarehouse.Checked)
                {
                  if (num6 == 1 && bolAllocate && Convert.ToInt32(row["i_CurrentQuantity"]) != 0)
                  {
                    Message.SetMessage(this.message, enmMessageType.Warning, "*****<BR> La posicion que intenta asignar ya esta siendo usada.");
                    return -1;
                  }
                  if (num6 == 1 && !bolAllocate && Convert.ToInt32(row["i_CurrentQuantity"]) != 0)
                  {
                    Message.SetMessage(this.message, enmMessageType.Warning, "*****<BR> La posicion que intenta liberar ya esta siendo usada.");
                    return -1;
                  }
                  row["i_isAssigned"] = (object) (bolAllocate ? 1 : 0);
                  arrayList1.Add(row["i_LocationWarehouseId"]);
                  ++num1;
                }
                else
                {
                  row["b_IsReserved"] = (object) (bolAllocate ? 1 : 0);
                  arrayList1.Add(row["i_LocationWarehouseId"]);
                  ++num1;
                }
              }
            }
            else if (string.IsNullOrEmpty(strRow))
            {
              ch2 = Convert.ToChar(row["i_PositionY"]);
              if (ch2.ToString().Equals(strColumn, StringComparison.CurrentCulture))
              {
                if (!this.ChkIsSubWarehouse.Checked)
                {
                  if (num6 == 1 && bolAllocate && Convert.ToInt32(row["i_CurrentQuantity"]) != 0)
                  {
                    Message.SetMessage(this.message, enmMessageType.Warning, "*****<BR> La posicion que intenta asignar ya esta siendo usada.");
                    return -1;
                  }
                  if (num6 == 1 && !bolAllocate && Convert.ToInt32(row["i_CurrentQuantity"]) != 0)
                  {
                    Message.SetMessage(this.message, enmMessageType.Warning, "*****<BR> La posicion que intenta liberar ya esta siendo usada.");
                    return -1;
                  }
                  row["i_isAssigned"] = (object) (bolAllocate ? 1 : 0);
                  arrayList1.Add(row["i_LocationWarehouseId"]);
                  ++num1;
                }
                else
                {
                  row["b_IsReserved"] = (object) (bolAllocate ? 1 : 0);
                  arrayList1.Add(row["i_LocationWarehouseId"]);
                  ++num1;
                }
              }
            }
            else
            {
              int num7;
              if (row["i_PositionX"].ToString().Equals(strRow, StringComparison.CurrentCulture))
              {
                ch2 = Convert.ToChar(row["i_PositionY"]);
                num7 = ch2.ToString().Equals(strColumn, StringComparison.CurrentCulture) ? 1 : 0;
              }
              else
                num7 = 0;
              if (num7 != 0)
              {
                if (!this.ChkIsSubWarehouse.Checked)
                {
                  if (num6 == 1 && bolAllocate && Convert.ToInt32(row["i_CurrentQuantity"]) != 0)
                  {
                    Message.SetMessage(this.message, enmMessageType.Warning, "*****<BR> La posicion que intenta asignar ya esta siendo usada.");
                    return -1;
                  }
                  if (num6 == 1 && !bolAllocate && Convert.ToInt32(row["i_CurrentQuantity"]) != 0)
                  {
                    Message.SetMessage(this.message, enmMessageType.Warning, "*****<BR> La posicion que intenta liberar ya esta siendo usada.");
                    return -1;
                  }
                  row["i_isAssigned"] = (object) (bolAllocate ? 1 : 0);
                  arrayList1.Add(row["i_LocationWarehouseId"]);
                  ++num1;
                }
                else
                {
                  row["b_IsReserved"] = (object) (bolAllocate ? 1 : 0);
                  arrayList1.Add(row["i_LocationWarehouseId"]);
                  ++num1;
                }
              }
            }
          }
        }
      }
      ArrayList arrayList2 = new ArrayList();
      ArrayList arrayList3 = new ArrayList();
      if (this.Session["CHECKED_ITEMS"] != null)
        arrayList2 = (ArrayList) this.Session["CHECKED_ITEMS"];
      if (this.Session["NO_CHECKED_ITEMS"] != null)
        arrayList3 = (ArrayList) this.Session["NO_CHECKED_ITEMS"];
      foreach (int num8 in arrayList1)
      {
        if (bolAllocate)
        {
          if (!arrayList2.Contains((object) num8))
            arrayList2.Add((object) num8);
          arrayList3.Remove((object) num8);
        }
        else
        {
          if (!arrayList3.Contains((object) num8))
            arrayList3.Add((object) num8);
          arrayList2.Remove((object) num8);
        }
      }
      if (arrayList2 != null && arrayList2.Count > 0)
        this.Session["CHECKED_ITEMS"] = (object) arrayList2;
      if (arrayList3 != null && arrayList3.Count > 0)
        this.Session["NO_CHECKED_ITEMS"] = (object) arrayList3;
      this.wdgWarehouseLocationList.DataSource = (object) null;
      this.wdgWarehouseLocationList.DataBind();
      return num1;
    }

    protected void wdgWarehouseList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgWarehouseList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void wdgWarehouseLocationList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgWarehouseLocationList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
  }
}
