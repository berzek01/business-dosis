// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.WarehouseControlList
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.Warehouse.BL;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse
{
  public class WarehouseControlList : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtBatchNumber;
    protected FilteredTextBoxExtender txtBatchNumber_FilteredTextBoxExtender;
    protected TextBox txtTicketNumber;
    protected FilteredTextBoxExtender FilteredTextBoxExtender1;
    protected Button Button1;
    protected Fecha wdpDateIni;
    protected Fecha wdpDateFin;
    protected DropDownList wddCounter;
    protected Button wibSearch;
    protected Button wibSearchAvanzado;
    protected GridView wdgWarehouseControlList;
    protected Pager custPagerControlList;
    protected Button wibNew;
    protected Button wibViewDetailReserved;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.LoadCounter();
        this.SetDatePicker();
        this.txtTicketNumber.Focus();
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
        this.SearchControl();
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

    protected void custPagerControlList_OnPageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        string pstrBatchId = string.Empty;
        if (!string.IsNullOrEmpty(this.txtBatchNumber.Text))
        {
          if (this.txtBatchNumber.Text.Trim().Length != 10)
          {
            pstrBatchId = this.txtBatchNumber.Text.Trim();
            this.txtBatchNumber.Text = "";
            this.txtBatchNumber.Focus();
          }
          else
          {
            pstrBatchId = Convert.ToInt32(this.txtBatchNumber.Text.Trim().Substring(3), (IFormatProvider) CultureInfo.CurrentCulture).ToString((IFormatProvider) CultureInfo.CurrentCulture);
            this.txtBatchNumber.Text = "";
            this.txtBatchNumber.Focus();
          }
        }
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlList.aspx");
        int intLocationId = 0;
        int pintSystemUserId = 0;
        if (systemUser != null)
        {
          intLocationId = systemUser.i_LocationId;
          pintSystemUserId = systemUser.i_SystemUserId;
        }
        DateTime dateTime1 = Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        DateTime dateTime2 = Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
        int int32 = Convert.ToInt32(this.Session["ApplicationId"], (IFormatProvider) CultureInfo.CurrentCulture);
        string userExtendedAction = requirementQueriesBl.GetSystemUserExtendedAction(systemUser.i_SystemUserId, int32);
        string str = "";
        if (userExtendedAction != "")
          str = Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("8", StringComparison.CurrentCulture)));
        if (str == "8")
          pintSystemUserId = 0;
        int result;
        int.TryParse(this.wddCounter.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), out result);
        if (pstrBatchId == string.Empty)
          pstrBatchId = "0";
        this.SearchControlList(pstrBatchId, intLocationId, dateTime1.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture), dateTime2.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture), pintSystemUserId, result, false);
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

    protected void wibSearchAvanzado_Click(object sender, EventArgs e)
    {
      this.CreatePopUpServer("Búsqueda Avanzada", "../../Warehouse/Searchs/WarehouseControlSearchPlate.aspx?v_plateControl=", "550px", "370px");
    }

    protected void wdgWarehouseControlList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        if (!e.CommandName.Equals("Edit", StringComparison.CurrentCulture) && !e.CommandName.Equals("Delete", StringComparison.CurrentCulture))
          return;
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlList.aspx");
        GridViewRow row = this.wdgWarehouseControlList.Rows[Convert.ToInt32(e.CommandArgument)];
        int intWarehouseControlId = int.Parse(row.Cells[0].Text, (IFormatProvider) CultureInfo.CurrentCulture);
        int num1 = int.Parse(row.Cells[5].Text, (IFormatProvider) CultureInfo.CurrentCulture);
        RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
        int int32 = Convert.ToInt32(this.Session["ApplicationId"], (IFormatProvider) CultureInfo.CurrentCulture);
        string userExtendedAction = requirementQueriesBl.GetSystemUserExtendedAction(systemUser.i_SystemUserId, int32);
        string str = "";
        if (userExtendedAction != "")
          str = Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("8", StringComparison.CurrentCulture)));
        if (e.CommandName.Equals("Edit", StringComparison.CurrentCulture))
        {
          int num2 = 0;
          if (str == "8" || num1 == 0)
            num2 = 1;
          this.Session["WastageSelectedRow"] = (object) row;
          this.Response.Redirect("~/Warehouse/Operations/WarehouseControlOperation.aspx?WarehouseControlId=" + Convert.ToString(intWarehouseControlId.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture) + "&Edit=" + num2.ToString((IFormatProvider) CultureInfo.CurrentCulture), false);
        }
        else if (e.CommandName.Equals("Delete", StringComparison.CurrentCulture))
        {
          if (str != "8" && num1 > 0)
            throw new HandledException(1, "No se Tiene Privilegios para realizar esta acción.");
          this.Delete(intWarehouseControlId);
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

    protected void wdgWarehouseControlList_PageIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.SearchControl();
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

    protected void wibAutoclickNew_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.CreatePopUpServer("Registro", "../../Warehouse/WarehouseControlListDetail.aspx?sTicket=" + this.txtTicketNumber.Text + "&&scheck=2", "500px", "500px");
      this.txtTicketNumber.Attributes.Add("onfocusin", " select();");
      this.txtTicketNumber.Focus();
    }

    protected void wibNew_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.CreatePopUpServer("Registro", "../../Warehouse/WarehouseControlListDetail.aspx?sTicket=" + this.txtTicketNumber.Text + "&&scheck=1", "500px", "500px");
    }

    private void LoadCounter()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlList.aspx");
        int iLocationId = systemUser.i_LocationId;
        int num = systemUser.i_CompanyId.Value;
        DataTable warehouseControl = new WarehouseControlQueriesBL().GetSystemUserWarehouseControl(iLocationId);
        if (warehouseControl != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) warehouseControl.Rows)
            this.wddCounter.Items.Add(new ListItem(row["v_Alias"].ToString(), row["i_SystemUserId"].ToString()));
        }
        this.wddCounter.Items.Insert(0, new ListItem("- Todos - ", "0"));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SetDatePicker()
    {
      try
      {
        this.wdpDateIni.Value = DateTime.Now.AddMonths(-1);
        this.wdpDateFin.Value = DateTime.Now;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchControl()
    {
      try
      {
        string pstrBatchId = string.Empty;
        if (!string.IsNullOrEmpty(this.txtBatchNumber.Text))
        {
          if (this.txtBatchNumber.Text.Trim().Length != 10)
          {
            pstrBatchId = this.txtBatchNumber.Text.Trim();
            this.txtBatchNumber.Text = "";
            this.txtBatchNumber.Focus();
          }
          else
          {
            pstrBatchId = Convert.ToInt32(this.txtBatchNumber.Text.Trim().Substring(3), (IFormatProvider) CultureInfo.CurrentCulture).ToString((IFormatProvider) CultureInfo.CurrentCulture);
            this.txtBatchNumber.Text = "";
            this.txtBatchNumber.Focus();
          }
        }
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlList.aspx");
        int intLocationId = 0;
        int pintSystemUserId = 0;
        if (systemUser != null)
        {
          intLocationId = systemUser.i_LocationId;
          pintSystemUserId = systemUser.i_SystemUserId;
        }
        DateTime dateTime1 = Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        DateTime dateTime2 = Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
        int int32 = Convert.ToInt32(this.Session["ApplicationId"], (IFormatProvider) CultureInfo.CurrentCulture);
        string userExtendedAction = requirementQueriesBl.GetSystemUserExtendedAction(systemUser.i_SystemUserId, int32);
        string str = "";
        if (userExtendedAction != "")
          str = Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("8", StringComparison.CurrentCulture)));
        if (str == "8")
          pintSystemUserId = 0;
        int result;
        int.TryParse(this.wddCounter.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), out result);
        if (pstrBatchId == string.Empty)
          pstrBatchId = "0";
        this.SearchControlList(pstrBatchId, intLocationId, dateTime1.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture), dateTime2.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture), pintSystemUserId, result, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchControlList(
      string pstrBatchId,
      int intLocationId,
      string dFecIni,
      string dFecFin,
      int pintSystemUserId,
      int pintCounterId,
      bool pboolLoadPager)
    {
      try
      {
        pstrBatchId = string.IsNullOrEmpty(pstrBatchId) ? "-1" : pstrBatchId;
        int pintstartRowIndex = pboolLoadPager ? 1 : this.custPagerControlList.CurrentPageNumber;
        int pintmaxRows = this.custPagerControlList.CurrentPageSize == 0 ? 10 : this.custPagerControlList.CurrentPageSize;
        int pinttotalRows;
        DataTable warehouseControl = new WarehouseControlQueriesBL().GetWarehouseControl(int.Parse(pstrBatchId, (IFormatProvider) CultureInfo.CurrentCulture), intLocationId, dFecIni, dFecFin, pintSystemUserId, pintCounterId, pintstartRowIndex, pintmaxRows, out pinttotalRows);
        if (warehouseControl == null || warehouseControl.Rows.Count == 0)
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
        int num = pinttotalRows;
        this.wdgWarehouseControlList.DataSource = (object) warehouseControl;
        this.wdgWarehouseControlList.DataBind();
        this.custPagerControlList.TotalPages = num % pintmaxRows == 0 ? num / pintmaxRows : num / pintmaxRows + 1;
        this.custPagerControlList.TotalRecordCount = pinttotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerControlList.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void Delete(int intWarehouseControlId)
    {
      try
      {
        if (new WarehouseControlManagementBL().WarehouseControlDelete(intWarehouseControlId) > 0)
        {
          Message.SetMessage(this.lblMessage, new HandledException(2, "Se eliminó exitosamente"));
          this.SearchControl();
        }
        else
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se puede eliminar"));
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
      this.txtTicketNumber.Attributes.Add("onfocusin", " select();");
      this.txtTicketNumber.Focus();
    }

    protected void wdgWarehouseControlList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void wdgWarehouseControlList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wibViewDetailReserved_Click(object sender, EventArgs e)
    {
      SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlList.aspx");
      RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
      int int32 = Convert.ToInt32(this.Session["ApplicationId"], (IFormatProvider) CultureInfo.CurrentCulture);
      string userExtendedAction = requirementQueriesBl.GetSystemUserExtendedAction(systemUser.i_SystemUserId, int32);
      string str = "";
      if (userExtendedAction != "")
        str = Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("8", StringComparison.CurrentCulture)));
      int num = 0;
      if (str == "8")
        num = 1;
      this.CreatePopUpServer("Visualizar", "../../Warehouse/Searchs/WarehouseListRequestDetail.aspx?sType=" + num.ToString(), "770px", "600px");
    }
  }
}
