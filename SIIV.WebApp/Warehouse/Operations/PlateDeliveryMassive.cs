// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.PlateDeliveryMassive
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class PlateDeliveryMassive : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected DropDownList wddTipeRequirement;
    protected Fecha wdpDateIni;
    protected Fecha wdpDateFin;
    protected DropDownList wddCounter;
    protected Button wibSearch;
    protected Button wibSearchAvanzado;
    protected GridView wdgDeliveryMassive;
    protected Pager custPagerClaimList;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.InitialPage();
        this.SetDatePicker();
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
        this.SearchPlateDelivery(true);
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

    protected void wdgDeliveryMassive_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        GridViewRow row = this.wdgDeliveryMassive.Rows[Convert.ToInt32(e.CommandArgument)];
        if (!e.CommandName.Equals("Edit", StringComparison.CurrentCulture))
          return;
        int num = int.Parse(row.Cells[0].Text);
        string text1 = row.Cells[3].Text;
        string text2 = row.Cells[4].Text;
        this.Response.Redirect("PlateDeliveryMassiveManagement.aspx?WarehouseControlId=" + Convert.ToString(num.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture) + "&strFecha=" + text1 + "&strTipo=" + text2, true);
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

    protected void custPagerClaimList_OnPageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        this.SearchPlateDelivery(false);
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

    private void InitialPage()
    {
      try
      {
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) "314",
          (object) "",
          (object) "1",
          (object) "1"
        });
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["i_GroupId"].ToString() == "314")
              this.wddTipeRequirement.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          }
        }
        this.wddTipeRequirement.Items.Insert(0, new ListItem("- Seleccione - ", "-1"));
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - PlateDeliveryMassive.aspx");
        RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
        int int32 = Convert.ToInt32(this.Session["ApplicationId"], (IFormatProvider) CultureInfo.CurrentCulture);
        string userExtendedAction = requirementQueriesBl.GetSystemUserExtendedAction(systemUser.i_SystemUserId, int32);
        string str = "";
        if (userExtendedAction != "")
          str = Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("8", StringComparison.CurrentCulture)));
        int intLocationId = 0;
        if (systemUser != null)
          intLocationId = systemUser.i_LocationId;
        DataTable warehouseControl = new WarehouseControlQueriesBL().GetSystemUserWarehouseControl(intLocationId);
        if (warehouseControl != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) warehouseControl.Rows)
          {
            if (str == "8")
              this.wddCounter.Items.Add(new ListItem(row["v_Alias"].ToString(), row["i_SystemUserId"].ToString()));
            else if (systemUser.i_SystemUserId == Convert.ToInt32(row["i_SystemUserId"], (IFormatProvider) CultureInfo.CurrentCulture))
              this.wddCounter.Items.Add(new ListItem(row["v_Alias"].ToString(), row["i_SystemUserId"].ToString()));
          }
        }
        if (!(str == "8"))
          return;
        this.wddCounter.Items.Insert(0, new ListItem("- Todos - ", "-1"));
        this.wddCounter.SelectedIndex = 0;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SetDatePicker()
    {
      this.wdpDateIni.Value = DateTime.Now;
      this.wdpDateFin.Value = DateTime.Now;
    }

    private void SearchPlateDelivery(bool pboolLoadPager)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - PlateDeliveryMassive.aspx");
        int num1 = 0;
        int intSystemUserId = 0;
        if (systemUser != null)
        {
          num1 = systemUser.i_LocationId;
          intSystemUserId = systemUser.i_SystemUserId;
        }
        int pintstartRowIndex = pboolLoadPager ? 1 : this.custPagerClaimList.CurrentPageNumber;
        int pintmaxRows = this.custPagerClaimList.CurrentPageSize == 0 ? 10 : this.custPagerClaimList.CurrentPageSize;
        int result1;
        int.TryParse(this.wddTipeRequirement.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), out result1);
        int result2;
        int.TryParse(this.wddCounter.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), out result2);
        DateTime dateTime1 = Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        DateTime dateTime2 = Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        int pinttotalRows;
        this.wdgDeliveryMassive.DataSource = (object) new PlateDeliverQueriesBL().GetMassiveDeliveryWarehouseControl(dateTime1.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture), dateTime2.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture), result2, result1, intSystemUserId, pintstartRowIndex, pintmaxRows, out pinttotalRows);
        this.wdgDeliveryMassive.DataBind();
        int num2 = pinttotalRows;
        this.custPagerClaimList.TotalPages = num2 % pintmaxRows == 0 ? num2 / pintmaxRows : num2 / pintmaxRows + 1;
        this.custPagerClaimList.TotalRecordCount = pinttotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerClaimList.LoadPager();
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
  }
}
