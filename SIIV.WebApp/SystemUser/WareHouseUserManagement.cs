// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.SystemUser.WareHouseUserManagement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.SystemUser.BL;
using SIIV.Warehouse.BL;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.SystemUser
{
  public class WareHouseUserManagement : Page
  {
    private MaintenanceOperation currentOperation = MaintenanceOperation.None;
    protected HiddenField H1;
    protected UpdatePanel UpdatePanel1;
    protected DropDownList wddTypeWareHouseUser;
    protected Button wibSearch;
    protected GridView wdgList;
    protected Pager custPagerWHUser;
    protected Label lblMessageUserList;
    protected Button wibNew;
    protected UpdatePanel UpdatePanel2;
    protected DropDownList wddTypeWareHouseUserNew;
    protected TextBox txtAlias;
    protected HtmlTableRow trManagementButtons;
    protected Button wibSave;
    protected Button wibCancel;
    protected Label lblMessageUser;
    protected HtmlTableRow trwibFinalze;
    protected Button wibFinalze;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadParameters();
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.Search();

    protected void wdgList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      if (!e.CommandName.Equals("Delete", StringComparison.CurrentCulture))
        return;
      int int32 = Convert.ToInt32(e.CommandArgument);
      new SystemUserQueriesBL().WareHouseUserDelete(this.wdgList.Rows[int32].Cells[3].Text.ToString(), Convert.ToInt32(this.wdgList.DataKeys[int32]["i_TypeWareHouseUserId"].ToString()));
      this.Search();
    }

    protected void custPagerWHUser_PageChanged(object sender, CustomPageChangeArgs e)
    {
      this.SearchWHUserList(Convert.ToInt32(this.wddTypeWareHouseUser.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), false);
    }

    protected void wibNew_Click(object sender, EventArgs e)
    {
      this.currentOperation = MaintenanceOperation.AddNew;
      this.ViewState["currentOperation"] = (object) this.currentOperation;
      this.EnabledControls(MaintenanceOperation.AddNew);
      this.wibSave.Enabled = true;
      this.lblMessageUser.Visible = false;
      this.txtAlias.Text = "";
      this.trwibFinalze.Visible = false;
      this.trManagementButtons.Visible = true;
      SIIV.BE.SystemUser systemUser = (SIIV.BE.SystemUser) this.Session["SystemUser"];
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      int int32 = Convert.ToInt32(this.wddTypeWareHouseUserNew.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      if (this.txtAlias.Text != "")
      {
        int pintResult;
        new SystemUserQueriesBL().WareHouseUserInsert(this.txtAlias.Text, int32, out pintResult);
        if (pintResult == 0)
        {
          Message.SetMessage(this.lblMessageUser, enmMessageType.Warning, "Nombre de Usuario no es válido.");
        }
        else
        {
          this.Search();
          this.trwibFinalze.Visible = true;
          this.trManagementButtons.Visible = false;
        }
      }
      else
        Message.SetMessage(this.lblMessageUser, enmMessageType.Warning, "Ingrese un Alias");
    }

    protected void wibCancel_Click(object sender, EventArgs e)
    {
      this.EnabledControls(MaintenanceOperation.None);
    }

    protected void wibFinalze_Click(object sender, EventArgs e)
    {
      string script = UtilDA.ActiveTabIndex("tabs", 0, "1");
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      this.HidePopup();
    }

    private void LoadParameters()
    {
    }

    private void EnabledControls(MaintenanceOperation penuCurrentOperation)
    {
      string str = this.H1.Value;
      if (penuCurrentOperation == MaintenanceOperation.AddNew)
      {
        if (str == "0")
        {
          string script1 = UtilDA.ActiveTabIndex("tabs", 1, "0");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script1, true);
          string script2 = "TabIndex();";
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script2, true);
        }
        else
        {
          string script3 = UtilDA.ActiveTabIndex("tabs", 0, "1");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script3, true);
          string script4 = "TabIndex();";
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script4, true);
        }
      }
      else
      {
        string script5 = UtilDA.ActiveTabIndex("tabs", 0, "1");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script5, true);
        string script6 = "TabIndex();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script6, true);
      }
    }

    private void Search()
    {
      try
      {
        this.SearchWHUserList(Convert.ToInt32(this.wddTypeWareHouseUser.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), true);
        this.HidePopup();
      }
      catch (Exception ex)
      {
        this.HidePopup();
        Message.SetMessage(this.lblMessageUserList, enmMessageType.Error, "Error*****<br>" + ex.Message);
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SearchWHUserList(int pintTypeWareHouseUserId, bool pboolLoadPager)
    {
      int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerWHUser.CurrentPageNumber;
      int pintMaxRows = this.custPagerWHUser.CurrentPageSize == 0 ? 10 : this.custPagerWHUser.CurrentPageSize;
      int pintTotalRows;
      DataTable systemUserWarehouse = new WarehouseControlQueriesBL().GetSystemUserWarehouse(pintTypeWareHouseUserId, pintStartRowIndex, pintMaxRows, out pintTotalRows);
      int num = pintTotalRows;
      this.wdgList.DataSource = (object) systemUserWarehouse;
      this.wdgList.DataBind();
      this.custPagerWHUser.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
      this.custPagerWHUser.TotalRecordCount = pintTotalRows;
      if (!pboolLoadPager)
        return;
      this.custPagerWHUser.LoadPager();
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void wdgList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
  }
}
