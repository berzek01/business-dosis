// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.SystemUser.UserSearch
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.Common.Resource.PagingClass;
using SIIV.Reports.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.SystemUser
{
  public class UserSearch : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected DropDownList wddCompanySearch;
    protected TextBox txtNameSearch;
    protected DropDownList wddStatusSearch;
    protected Button WebImageButton1;
    protected GridView wdgUser;
    protected Pager custPagerUserList;
    protected Button wibCancel;
    protected Label lblMessageUserList;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadParameters();
    }

    protected void btnSearch_Click(object sender, EventArgs e) => this.SearchUsers();

    protected void custPagerUserList_OnPageChanged(object sender, CustomPageChangeArgs e)
    {
      this.SearchUsersList(new UsersListPagingParameters()
      {
        Alias = this.txtNameSearch.Text.TrimEnd(),
        Nombres = string.Empty,
        Apellidos = string.Empty,
        Status = this.wddStatusSearch.SelectedValue == "" ? -1 : Convert.ToInt32(this.wddStatusSearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture),
        CompanyId = this.wddCompanySearch.SelectedValue == "" ? -1 : Convert.ToInt32(this.wddCompanySearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture),
        i_SystemUserRefId = 0
      }, false);
    }

    protected void wdgUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (!e.CommandName.Equals("Select"))
        return;
      int int32 = Convert.ToInt32(e.CommandArgument);
      GridViewRow row = this.wdgUser.Rows[int32];
      if (row == null)
        throw new HandledException(4, "Error de selección.", "'wdgUser' - UserSearch.aspx");
      string v_UserName = row.Cells[2].Text.ToString() + " " + row.Cells[3].Text.ToString();
      this.SendInfoProductPopupClose(Convert.ToInt32(this.wdgUser.DataKeys[int32]["i_SystemUserId"].ToString()), v_UserName);
    }

    protected void btnCancelar_Click(object sender, EventArgs e) => this.PopupClose();

    private void LoadParameters()
    {
      try
      {
        List<SIIV.BE.SystemParameter> systemParameterList = new SystemParameterManagementBL().Get((object) new ArrayList()
        {
          (object) ("" + SystemParameterGroups.ModelEntities.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
          (object) "",
          (object) "1",
          (object) "1"
        });
        this.wddCompanySearch.Items.Clear();
        foreach (SIIV.BE.SystemParameter systemParameter in systemParameterList)
        {
          if (systemParameter.i_GroupId == SystemParameterGroups.ModelEntities)
            this.wddCompanySearch.Items.Add(new ListItem(systemParameter.v_Description, systemParameter.i_ParameterId.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
        }
        this.wddCompanySearch.Items.Insert(0, new ListItem("- Todos - ", "-1"));
        this.wddStatusSearch.Items.Insert(0, new ListItem("- Todos - ", "-1"));
        this.wddStatusSearch.Items.Add(new ListItem("Activo", "1"));
        this.wddStatusSearch.Items.Add(new ListItem("Inactivo", "0"));
        this.wddStatusSearch.SelectedValue = "1";
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageUserList, enmMessageType.Error, ex.Message);
      }
    }

    private void SearchUsers()
    {
      try
      {
        this.SearchUsersList(new UsersListPagingParameters()
        {
          Alias = this.txtNameSearch.Text.TrimEnd(),
          Nombres = string.Empty,
          Apellidos = string.Empty,
          Status = this.wddStatusSearch.SelectedValue == "" ? -1 : Convert.ToInt32(this.wddStatusSearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture),
          CompanyId = this.wddCompanySearch.SelectedValue == "" ? -1 : Convert.ToInt32(this.wddCompanySearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture),
          i_SystemUserRefId = 0
        }, true);
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

    private void SearchUsersList(UsersListPagingParameters objParam, bool pboolLoadPager)
    {
      int pintstartRowIndex = pboolLoadPager ? 1 : this.custPagerUserList.CurrentPageNumber;
      int pintmaxRows = this.custPagerUserList.CurrentPageSize == 0 ? 10 : this.custPagerUserList.CurrentPageSize;
      int pinttotalRows;
      DataTable all = new ManagementPagingBL().UserReportGetAll(objParam, pintstartRowIndex, pintmaxRows, out pinttotalRows);
      int num = pinttotalRows;
      this.wdgUser.DataSource = (object) all;
      this.wdgUser.DataBind();
      this.custPagerUserList.TotalPages = num % pintmaxRows == 0 ? num / pintmaxRows : num / pintmaxRows + 1;
      this.custPagerUserList.TotalRecordCount = pinttotalRows;
      if (!pboolLoadPager)
        return;
      this.custPagerUserList.LoadPager();
    }

    private void SendInfoProductPopupClose(int i_SystemUserId, string v_UserName)
    {
      this.Session["pobjSystemUser"] = (object) new SIIV.BE.SystemUser()
      {
        i_SystemUserRefId = new int?(i_SystemUserId),
        v_Alias = v_UserName
      };
      string script = "SendInfoProductPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void PopupClose()
    {
      string script = "PopupClose();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
