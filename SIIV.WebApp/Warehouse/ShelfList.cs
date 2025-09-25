// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.ShelfList
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse
{
  public class ShelfList : Page
  {
    private MaintenanceOperation currentOperation = MaintenanceOperation.None;
    protected HiddenField H1;
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtFilter;
    protected Button wibSearch;
    protected GridView wdgShelfList;
    protected Pager custPagerShelfList;
    protected Button wibNew;
    protected Label lblMessage;
    protected UpdatePanel UpdatePanel2;
    protected HtmlTableRow trPuntoEntrega;
    protected DropDownList wddLocation;
    protected TextBox txtDescription;
    protected FilteredTextBoxExtender ftbeDescription;
    protected TextBox txtCapacity;
    protected FilteredTextBoxExtender ftbeCapacity;
    protected TextBox txtPositionX;
    protected FilteredTextBoxExtender ftbePositionX;
    protected TextBox txtPositionY;
    protected FilteredTextBoxExtender ftbePositionY;
    protected TextBox txtShelfTypeUse;
    protected TextBox txtShelfTypeUseDesciption;
    protected Button btnGenerateShelf;
    protected HtmlTableRow trEstructuraAnaquel;
    protected HtmlTableRow trGridEstructuraAnaquel;
    protected Button btnAsignedTypeUse;
    protected Button btnDesasignedTypeUse;
    protected GridView wdgWarehouseLocationList;
    protected Pager custPagerBatch;
    protected HtmlTableRow trManagementButtons;
    protected Button wibSave;
    protected Button wibCancel;
    protected HtmlTableRow trwibFinalize;
    protected Button wibFinalize;
    protected HiddenField hidShelfId;
    protected HiddenField hdfcbGridValues;
    protected Button btnJavaScriptResponse;
    protected Label lblMessage1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.InitializeData();
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
        this.SearchShelves();
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

    protected void custPagerShelfList_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        this.SearchShelvesList(0, this.txtFilter.Text, (this.Session["SystemUser"] as SystemUser).i_LocationId, false);
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

    protected void wdgShelfList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        int int32 = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = this.wdgShelfList.Rows[int32];
        this.ViewState["wdgShelfListIndex"] = (object) int32;
        if (e.CommandName.Equals("Edit", StringComparison.CurrentCulture))
        {
          Shelf currentShelf = this.GetCurrentShelf(int32);
          this.ShowShelfInfo(currentShelf);
          this.currentOperation = MaintenanceOperation.Edit;
          this.ViewState.Add("currentOperation", (object) this.currentOperation);
          this.EnableControls();
          this.trEstructuraAnaquel.Visible = true;
          this.trGridEstructuraAnaquel.Visible = true;
          this.LoadShelfPositions(currentShelf.i_ShelfId, -1, true);
        }
        else
        {
          if (!e.CommandName.Equals("Delete", StringComparison.CurrentCulture))
            return;
          Shelf currentShelf = this.GetCurrentShelf(int32);
          int? iQuantity = currentShelf.i_Quantity;
          int num = 0;
          if (iQuantity.GetValueOrDefault() > num & iQuantity.HasValue)
            throw new HandledException(1, "Advertencia****<br>No es posible eliminar Anaquel " + currentShelf.v_Desciption + " está ocupado.");
          using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
          {
            Timeout = new TimeSpan(1, 1, 1)
          }))
          {
            new ShelfManagementBL().ShelfDelete(currentShelf.i_ShelfId);
            Message.SetMessage(this.lblMessage, new HandledException(2, "Correcto****<br> Anaquel " + currentShelf.v_Desciption + " fue eliminado con éxito."));
            transactionScope.Complete();
          }
          this.SearchShelves();
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

    protected void wdgShelfList_PageIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.SearchShelves();
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

    protected void wibNew_Click(object sender, EventArgs e)
    {
      try
      {
        this.ShowShelfInfo(new Shelf()
        {
          i_Quantity = new int?(),
          i_CapacityShelf = new int?(),
          i_PositionX = new int?(),
          i_PositionY = new int?(),
          v_ShelfTypeUseId = string.Empty,
          i_LocationId = new int?((this.Session["SystemUser"] as SystemUser).i_LocationId)
        });
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

    protected void btnGenerateShelf_Click(object sender, EventArgs e)
    {
      try
      {
        this.ValidateData();
        Shelf objShelf = this.ReadShelfInfo();
        if (this.CheckShelfNameAvailable(objShelf))
        {
          this.InsertNewShelf(objShelf);
          this.trEstructuraAnaquel.Visible = true;
          this.trGridEstructuraAnaquel.Visible = true;
          this.LoadShelfPositions(int.Parse(this.hidShelfId.Value, (IFormatProvider) CultureInfo.CurrentCulture), -1, true);
          this.txtDescription.Enabled = false;
          this.txtCapacity.Enabled = false;
          this.txtPositionX.Enabled = false;
          this.txtPositionY.Enabled = false;
          this.btnGenerateShelf.Enabled = false;
        }
        else
        {
          Message.SetMessage(this.lblMessage, new HandledException(1, "Ya existe un anaquel con el nombre especificado."));
          this.trwibFinalize.Visible = false;
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        this.trwibFinalize.Visible = false;
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void btnAsignedTypeUse_Click(object sender, EventArgs e)
    {
      try
      {
        ArrayList arrayList = new ArrayList();
        foreach (GridViewRow row in this.wdgWarehouseLocationList.Rows)
        {
          if (row.FindControl("AssignCheck") is CheckBox control && control.Checked)
          {
            int int32 = Convert.ToInt32(this.wdgWarehouseLocationList.DataKeys[row.RowIndex]["i_LocationWarehouseId"].ToString());
            int num = string.IsNullOrEmpty(this.wdgWarehouseLocationList.DataKeys[row.RowIndex]["i_ShelfTypeUseId"].ToString()) ? -1 : Convert.ToInt32(this.wdgWarehouseLocationList.DataKeys[row.RowIndex]["i_ShelfTypeUseId"].ToString());
            if (Convert.ToInt32(this.wdgWarehouseLocationList.DataKeys[row.RowIndex]["i_Status"].ToString()) != 0)
              arrayList.Add((object) (int32.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "-" + num.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
          }
        }
        if (arrayList == null || arrayList.Count <= 0)
          return;
        this.Session["lwlst"] = (object) arrayList;
        this.CreatePopUp("Asignación de Tipos de Uso", "Searchs/ShelfTypeUseAssign.aspx", "770px", "700px");
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

    protected void btnDesasignedTypeUse_Click(object sender, EventArgs e)
    {
      try
      {
        foreach (GridViewRow row in this.wdgWarehouseLocationList.Rows)
        {
          if (row.FindControl("AssignCheck") is CheckBox control && control.Checked)
          {
            int int32_1 = Convert.ToInt32(this.wdgWarehouseLocationList.DataKeys[row.RowIndex]["i_LocationWarehouseId"].ToString());
            int int32_2 = Convert.ToInt32(this.wdgWarehouseLocationList.DataKeys[row.RowIndex]["i_Status"].ToString());
            string str = row.Cells[4].Text + "-" + row.Cells[5].Text;
            if (int32_2 != 0)
              new ShelfManagementBL().ShelfPositionTypeUseUpdate((int) Convert.ToInt16(int32_1), new int?());
          }
        }
        this.LoadShelfPositions(int.Parse(this.hidShelfId.Value, (IFormatProvider) CultureInfo.CurrentCulture), -1, true);
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

    protected void wdgWarehouseLocationList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowIndex < 0)
          return;
        ImageButton control1 = e.Row.FindControl("ibtAdd") as ImageButton;
        CheckBox control2 = (CheckBox) e.Row.FindControl("chkCheck");
        int int32 = Convert.ToInt32(e.Row.Cells[7].Text);
        string str = this.wdgWarehouseLocationList.DataKeys[e.Row.RowIndex]["i_LocationWarehouseId"].ToString() + "-" + e.Row.Cells[5].Text;
        Convert.ToInt32(this.wdgWarehouseLocationList.DataKeys[e.Row.RowIndex]["i_LocationWarehouseId"].ToString());
        if (int32 > 0)
          control2.Enabled = false;
        else
          control2.Enabled = true;
        if (Convert.ToInt32(this.wdgWarehouseLocationList.DataKeys[e.Row.RowIndex]["i_ShelfTypeUseId"].ToString()) != 0)
          control1.ImageUrl = "~/Images/Design/Buttons/Actions/consult.gif";
        else
          control1.ImageUrl = "~/Images/Design/Buttons/Actions/add.gif";
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

    protected void wdgWarehouseLocationList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        int int32_1 = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = this.wdgWarehouseLocationList.Rows[int32_1];
        int int32_2 = Convert.ToInt32(this.wdgWarehouseLocationList.DataKeys[int32_1]["i_LocationWarehouseId"].ToString());
        int num = string.IsNullOrEmpty(this.wdgWarehouseLocationList.DataKeys[int32_1]["i_ShelfTypeUseId"].ToString()) ? -1 : Convert.ToInt32(this.wdgWarehouseLocationList.DataKeys[int32_1]["i_ShelfTypeUseId"].ToString());
        if (!e.CommandName.Equals("addShelfTypeUse", StringComparison.CurrentCulture))
          return;
        int int32_3 = Convert.ToInt32(this.wdgWarehouseLocationList.DataKeys[int32_1]["i_Status"].ToString());
        string str = row.Cells[4].Text + "-" + row.Cells[5].Text;
        if (int32_3 == 0)
          throw new HandledException(1, "No es posible asignar un tipo de uso, posición " + str + " está Inactiva.");
        ArrayList arrayList = new ArrayList();
        arrayList.Add((object) (int32_2.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "-" + num.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
        if (arrayList != null && arrayList.Count > 0)
        {
          this.Session["lwlst"] = (object) arrayList;
          this.CreatePopUp("Asignación de Tipos de Uso", "Searchs/ShelfTypeUseAssign.aspx", "770px", "700px");
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

    protected void custPagerBatch_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        this.RemeberOldValues();
        this.LoadShelfPositions(Convert.ToInt32(this.hidShelfId.Value, (IFormatProvider) CultureInfo.CurrentCulture), -1, false);
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
        this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperation"];
        Shelf shelf1 = new Shelf();
        switch (this.currentOperation)
        {
          case MaintenanceOperation.AddNew:
            this.ValidateData();
            Shelf shelf2 = this.ReadShelfInfo();
            if (this.CheckShelfNameAvailable(shelf2))
            {
              new ShelfManagementBL().ShelfUpdate(shelf2);
              this.ShelfTypeUseUpdate();
              this.txtFilter.Text = string.Empty;
              this.SearchShelves();
              Message.SetMessage(this.lblMessage1, new HandledException(2, "Anaquel generado correctamente."));
              this.currentOperation = MaintenanceOperation.None;
              this.ViewState.Add("currentOperation", (object) this.currentOperation);
              this.EnableControls();
              break;
            }
            Message.SetMessage(this.lblMessage1, new HandledException(1, "Ya existe un anaquel con el nombre especificado."));
            this.trwibFinalize.Visible = false;
            this.trManagementButtons.Visible = true;
            break;
          case MaintenanceOperation.Edit:
            this.ValidateData();
            Shelf shelf3 = this.ReadShelfInfo();
            if (this.CheckShelfNameAvailable(shelf3))
            {
              new ShelfManagementBL().ShelfUpdate(shelf3);
              this.ShelfTypeUseUpdate();
              this.txtFilter.Text = string.Empty;
              this.SearchShelves();
              Message.SetMessage(this.lblMessage1, new HandledException(2, "Anaquel actualizado satisfactoriamente."));
              this.currentOperation = MaintenanceOperation.None;
              this.ViewState.Add("currentOperation", (object) this.currentOperation);
              this.EnableControls();
              break;
            }
            Message.SetMessage(this.lblMessage1, new HandledException(1, "Ya existe un anaquel con el nombre especificado."));
            this.trwibFinalize.Visible = false;
            this.trManagementButtons.Visible = true;
            break;
          case MaintenanceOperation.Delete:
            this.txtFilter.Text = string.Empty;
            Message.SetMessage(this.lblMessage, new HandledException(2, "Anaquel eliminado satisfactoriamente."));
            this.currentOperation = MaintenanceOperation.None;
            this.ViewState.Add("currentOperation", (object) this.currentOperation);
            this.EnableControls();
            break;
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage1, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage1, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void wibCancel_Click(object sender, EventArgs e)
    {
      try
      {
        string script1 = UtilDA.ActiveTabIndex("tabs", 0, "1");
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script1, true);
        string script2 = "TabIndex();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "ScriptIndex", script2, true);
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage1, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage1, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void wibFinalize_Click(object sender, EventArgs e)
    {
      try
      {
        string script1 = UtilDA.ActiveTabIndex("tabs", 0, "1");
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script1, true);
        string script2 = "TabIndex();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "ScriptIndex", script2, true);
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage1, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage1, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void InitializeData()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - ShelfList.aspx");
        int iLocationId = systemUser.i_LocationId;
        if (systemUser.i_CompanyId.GetValueOrDefault() == 1)
          this.trPuntoEntrega.Visible = false;
        else
          this.trPuntoEntrega.Visible = true;
        this.wddLocation.DataSource = (object) new LocationQueriesBL().GetLocationBy("", string.Empty, string.Empty);
        this.wddLocation.DataBind();
        this.wddLocation.SelectedValue = iLocationId.ToString();
        this.Session["DataListApproved"] = (object) null;
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
            this.txtCapacity.Enabled = true;
            this.txtPositionX.Enabled = true;
            this.txtPositionY.Enabled = true;
            this.wddLocation.Enabled = false;
            this.lblMessage1.Visible = false;
            this.trManagementButtons.Visible = true;
            this.trwibFinalize.Visible = false;
            this.txtShelfTypeUseDesciption.Text = "";
            this.trEstructuraAnaquel.Visible = false;
            this.trGridEstructuraAnaquel.Visible = false;
            this.btnGenerateShelf.Enabled = true;
            string script1 = UtilDA.ActiveTabIndex("tabs", 1, "0");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script1, true);
            string script2 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script2, true);
            break;
          case MaintenanceOperation.Edit:
            this.txtDescription.Enabled = true;
            this.txtCapacity.Enabled = false;
            this.txtPositionX.Enabled = false;
            this.txtPositionY.Enabled = false;
            this.wddLocation.Enabled = false;
            this.lblMessage1.Visible = false;
            this.trManagementButtons.Visible = true;
            this.trwibFinalize.Visible = false;
            this.trEstructuraAnaquel.Visible = true;
            this.trGridEstructuraAnaquel.Visible = true;
            this.btnGenerateShelf.Enabled = false;
            string script3 = UtilDA.ActiveTabIndex("tabs", 1, "0");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script3, true);
            string script4 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script4, true);
            break;
          default:
            this.txtDescription.Enabled = false;
            this.txtCapacity.Enabled = false;
            this.txtPositionX.Enabled = false;
            this.txtPositionY.Enabled = false;
            this.wddLocation.Enabled = false;
            this.trwibFinalize.Visible = true;
            this.trManagementButtons.Visible = false;
            this.trEstructuraAnaquel.Visible = false;
            this.trGridEstructuraAnaquel.Visible = false;
            break;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchShelves()
    {
      try
      {
        if (this.Session["SystemUser"] == null)
          throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - ShelfList.aspx");
        this.SearchShelvesList(0, this.txtFilter.Text, (this.Session["SystemUser"] as SystemUser).i_LocationId, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchShelvesList(
      int pintShelfId,
      string pstrShelf,
      int pintLocationId,
      bool pboolLoadPager)
    {
      try
      {
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerShelfList.CurrentPageNumber;
        int pintMaxRows = this.custPagerShelfList.CurrentPageSize == 0 ? 10 : this.custPagerShelfList.CurrentPageSize;
        int pintTotalRows;
        DataTable shelfByPag = new ShelfQueriesBL().GetShelfByPag(pintShelfId, pstrShelf, pintLocationId, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        int num = pintTotalRows;
        if (shelfByPag.Rows.Count == 0)
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontro información con los criterios seleccionados."));
        this.wdgShelfList.DataSource = (object) shelfByPag;
        this.wdgShelfList.DataBind();
        this.custPagerShelfList.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
        this.custPagerShelfList.TotalRecordCount = pintTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerShelfList.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ShowShelfInfo(Shelf objShelf)
    {
      try
      {
        int? nullable = objShelf.i_LocationId;
        if (nullable.HasValue)
        {
          DropDownList wddLocation = this.wddLocation;
          nullable = objShelf.i_LocationId;
          string str = nullable.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture);
          wddLocation.SelectedValue = str;
        }
        else
          this.wddLocation.SelectedValue = "14";
        this.txtDescription.Text = objShelf.v_Desciption;
        TextBox txtCapacity = this.txtCapacity;
        nullable = objShelf.i_CapacityShelf;
        string str1 = nullable.ToString();
        txtCapacity.Text = str1;
        TextBox txtPositionX = this.txtPositionX;
        nullable = objShelf.i_PositionX;
        string str2 = nullable.ToString();
        txtPositionX.Text = str2;
        TextBox txtPositionY = this.txtPositionY;
        nullable = objShelf.i_PositionY;
        string str3 = nullable.ToString();
        txtPositionY.Text = str3;
        this.hidShelfId.Value = objShelf.i_ShelfId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.txtShelfTypeUse.Text = objShelf.v_ShelfTypeUseId == null ? string.Empty : objShelf.v_ShelfTypeUseId;
        if (this.wdgShelfList.Rows.Count <= 0)
          return;
        int int32 = Convert.ToInt32(this.ViewState["wdgShelfListIndex"]);
        GridViewRow row = this.wdgShelfList.Rows[int32];
        this.txtShelfTypeUseDesciption.Text = string.IsNullOrEmpty(this.wdgShelfList.DataKeys[int32]["v_ShelfTypeUse"].ToString()) ? "" : this.wdgShelfList.DataKeys[int32]["v_ShelfTypeUse"].ToString().Replace('|', '\n');
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadShelfPositions(int pintShelfId, int pintWarehouseId, bool pboolLoadPager)
    {
      try
      {
        int pintLocationId = this.Session["SystemUser"] != null ? (this.Session["SystemUser"] as SystemUser).i_LocationId : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - ShelfList.aspx");
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerBatch.CurrentPageNumber;
        int pintMaxRows = this.custPagerBatch.CurrentPageSize == 0 ? 10 : this.custPagerBatch.CurrentPageSize;
        int pintTotalRows;
        DataTable shelfPositionBy = new ShelfQueriesBL().GetShelfPositionBy(pintShelfId, pintWarehouseId, pintLocationId, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        if (shelfPositionBy.Rows.Count > 0)
        {
          int num = pintTotalRows;
          this.Session["DataListApproved"] = (object) shelfPositionBy;
          this.RemeberOldValues();
          this.wdgWarehouseLocationList.DataSource = (object) (this.Session["DataListApproved"] as DataTable);
          this.wdgWarehouseLocationList.DataBind();
          this.custPagerBatch.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
          this.custPagerBatch.TotalRecordCount = pintTotalRows;
          if (!pboolLoadPager)
            return;
          this.custPagerBatch.LoadPager();
        }
        else
          Message.SetMessage(this.lblMessage, new HandledException(1, "Advertencia****<br>No hay Registros que mostrar."));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void RemeberOldValues()
    {
      try
      {
        ArrayList arrayList1 = new ArrayList();
        ArrayList arrayList2 = new ArrayList();
        foreach (GridViewRow row in this.wdgWarehouseLocationList.Rows)
        {
          CheckBox control = (CheckBox) row.FindControl("chkCheck");
          if (control != null)
          {
            int int32 = Convert.ToInt32(this.wdgWarehouseLocationList.DataKeys[row.RowIndex]["i_LocationWarehouseId"].ToString());
            bool flag = control.Checked;
            if (this.Session["CHECKED_ITEMS"] != null)
              arrayList1 = (ArrayList) this.Session["CHECKED_ITEMS"];
            if (this.Session["NO_CHECKED_ITEMS"] != null)
              arrayList2 = (ArrayList) this.Session["NO_CHECKED_ITEMS"];
            if (flag)
            {
              if (!arrayList1.Contains((object) int32))
                arrayList1.Add((object) int32);
              arrayList2.Remove((object) int32);
            }
            else
            {
              arrayList1.Remove((object) int32);
              if (!arrayList2.Contains((object) int32))
                arrayList2.Add((object) int32);
            }
          }
        }
        if (arrayList1 != null && arrayList1.Count > 0)
          this.Session["CHECKED_ITEMS"] = (object) arrayList1;
        if (arrayList2 != null && arrayList2.Count > 0)
          this.Session["NO_CHECKED_ITEMS"] = (object) arrayList2;
        DataTable dataTable = this.Session["DataListApproved"] as DataTable;
        if (arrayList1 != null && arrayList1.Count > 0)
        {
          for (int index = 0; index < arrayList1.Count; ++index)
          {
            if (dataTable.Select("i_LocationWarehouseId=" + arrayList1[index]?.ToString()).Length != 0)
              dataTable.Select("i_LocationWarehouseId=" + arrayList1[index]?.ToString())[0]["boolColumn"] = (object) true;
          }
        }
        if (arrayList2 != null && arrayList2.Count > 0)
        {
          for (int index = 0; index < arrayList2.Count; ++index)
          {
            if (dataTable.Select("i_LocationWarehouseId=" + arrayList2[index]?.ToString()).Length != 0)
              dataTable.Select("i_LocationWarehouseId=" + arrayList2[index]?.ToString())[0]["boolColumn"] = (object) false;
          }
        }
        this.Session["DataListApproved"] = (object) dataTable;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private bool CheckShelfNameAvailable(Shelf objShelf)
    {
      try
      {
        return new ShelfQueriesBL().CheckUniqueShelfName(objShelf.i_ShelfId, objShelf.v_Desciption, objShelf.i_LocationId.Value) == 0;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void InsertNewShelf(Shelf objShelf)
    {
      try
      {
        string[] strArray = new string[27]
        {
          "A",
          "B",
          "C",
          "D",
          "E",
          "F",
          "G",
          "H",
          "I",
          "J",
          "K",
          "L",
          "M",
          "N",
          "Ñ",
          "O",
          "P",
          "Q",
          "R",
          "S",
          "T",
          "U",
          "V",
          "W",
          "X",
          "Y",
          "Z"
        };
        string empty = string.Empty;
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
        {
          Timeout = new TimeSpan(1, 1, 1)
        }))
        {
          int num1 = new ShelfManagementBL().ShelfInsert(objShelf);
          this.hidShelfId.Value = num1.ToString((IFormatProvider) CultureInfo.CurrentCulture);
          int index = 0;
          while (true)
          {
            int num2 = index;
            int? nullable = objShelf.i_PositionY;
            int valueOrDefault1 = nullable.GetValueOrDefault();
            if (num2 < valueOrDefault1 & nullable.HasValue)
            {
              string str = strArray[index].ToString((IFormatProvider) CultureInfo.CurrentCulture);
              int num3 = 0;
              while (true)
              {
                int num4 = num3;
                nullable = objShelf.i_PositionX;
                int valueOrDefault2 = nullable.GetValueOrDefault();
                if (num4 < valueOrDefault2 & nullable.HasValue)
                {
                  new ShelfManagementBL().ShelfInsertPosition(new Shelf()
                  {
                    i_ShelfId = num1,
                    i_PositionX = new int?(num3 + 1),
                    i_PositionY = new int?((int) Convert.ToChar(str, (IFormatProvider) CultureInfo.CurrentCulture)),
                    i_CapacityShelf = objShelf.i_CapacityShelf,
                    i_LocationId = objShelf.i_LocationId
                  });
                  ++num3;
                }
                else
                  break;
              }
              ++index;
            }
            else
              break;
          }
          transactionScope.Complete();
          Message.SetMessage(this.lblMessage, new HandledException(2, "Correcto*******<br>Anaquel generado correctamente."));
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private Shelf GetCurrentShelf(int index)
    {
      try
      {
        GridViewRow row = this.wdgShelfList.Rows[index];
        Shelf currentShelf = new Shelf();
        currentShelf.i_ShelfId = Convert.ToInt32(this.wdgShelfList.DataKeys[index]["i_ShelfId"].ToString());
        if (!string.IsNullOrEmpty(row.Cells[2].Text))
          currentShelf.v_Desciption = row.Cells[2].Text;
        if (!string.IsNullOrEmpty(this.wdgShelfList.DataKeys[index]["i_Quantity"].ToString()))
          currentShelf.i_Quantity = new int?(Convert.ToInt32(this.wdgShelfList.DataKeys[index]["i_Quantity"].ToString()));
        if (!string.IsNullOrEmpty(row.Cells[3].Text))
          currentShelf.i_CapacityShelf = new int?(Convert.ToInt32(row.Cells[3].Text));
        if (!string.IsNullOrEmpty(row.Cells[4].Text))
          currentShelf.i_PositionX = new int?(Convert.ToInt32(row.Cells[4].Text));
        if (!string.IsNullOrEmpty(row.Cells[5].Text))
          currentShelf.i_PositionY = new int?(Convert.ToInt32(row.Cells[5].Text));
        if (!string.IsNullOrEmpty(this.wdgShelfList.DataKeys[index]["i_LocationId"].ToString()))
          currentShelf.i_LocationId = new int?(Convert.ToInt32(this.wdgShelfList.DataKeys[index]["i_LocationId"].ToString()));
        if (!string.IsNullOrEmpty(row.Cells[6].Text))
          currentShelf.v_ShelfTypeUseId = row.Cells[6].Text;
        return currentShelf;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private Shelf ReadShelfInfo()
    {
      try
      {
        Shelf shelf = new Shelf();
        if (!string.IsNullOrWhiteSpace(this.hidShelfId.Value))
          shelf.i_ShelfId = Convert.ToInt32(this.hidShelfId.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        shelf.v_Desciption = this.txtDescription.Text;
        shelf.i_CapacityShelf = new int?(Convert.ToInt32(this.txtCapacity.Text, (IFormatProvider) CultureInfo.CurrentCulture));
        shelf.i_PositionX = new int?(Convert.ToInt32(this.txtPositionX.Text, (IFormatProvider) CultureInfo.CurrentCulture));
        shelf.i_PositionY = new int?(Convert.ToInt32(this.txtPositionY.Text, (IFormatProvider) CultureInfo.CurrentCulture));
        shelf.i_LocationId = new int?(Convert.ToInt32(this.wddLocation.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
        shelf.v_ShelfTypeUseId = this.txtShelfTypeUse.Text;
        return shelf;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ShelfTypeUseUpdate()
    {
      try
      {
        this.RemeberOldValues();
        string empty = string.Empty;
        this.RemeberOldValues();
        if (this.Session["CHECKED_ITEMS"] != null)
        {
          ArrayList arrayList = (ArrayList) this.Session["CHECKED_ITEMS"];
          for (int index = 0; index < arrayList.Count; ++index)
            new ShelfManagementBL().ShelfPositionUpdateStatus((int) arrayList[index], 1);
        }
        if (this.Session["NO_CHECKED_ITEMS"] == null)
          return;
        ArrayList arrayList1 = (ArrayList) this.Session["NO_CHECKED_ITEMS"];
        for (int index = 0; index < arrayList1.Count; ++index)
          new ShelfManagementBL().ShelfPositionUpdateStatus((int) arrayList1[index], 0);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void btnJavaScriptResponse_Click(object sender, EventArgs e)
    {
      try
      {
        this.LoadShelfPositions(int.Parse(this.hidShelfId.Value, (IFormatProvider) CultureInfo.CurrentCulture), -1, true);
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

    private void CreatePopUp(
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

    private void ValidateData()
    {
      try
      {
        if (this.txtDescription.Text == string.Empty)
          throw new HandledException(1, "Advertencia*****<br>Debe ingresar Nombre de Anaquel.");
        if (this.txtCapacity.Text == string.Empty)
          throw new HandledException(1, "Advertencia*****<br>Debe ingresar capacidad máxima de posición de Anaquel.");
        if (this.txtPositionX.Text == string.Empty)
          throw new HandledException(1, "Advertencia*****<br>Debe ingresar número de filas.");
        if (this.txtPositionY.Text == string.Empty)
          throw new HandledException(1, "Advertencia*****<br>Debe ingresar número de columnas.");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void wdgShelfList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgShelfList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
  }
}
