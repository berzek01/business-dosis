// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Inventory.InventoryManagement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.Inventory.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Inventory
{
  public class InventoryManagement : Page
  {
    private string dateStartDate;
    private string dateEndDate;
    protected UpdatePanel UpdatePanel1;
    protected Label lblStatus;
    protected DropDownList wddStatus;
    protected Label lblUbicacion;
    protected DropDownList wddLocation;
    protected CheckBox chkFechas;
    protected Label lblFrom;
    protected TextBox txtStartDate;
    protected FilteredTextBoxExtender txtStarDate_FilteredTextBoxExtender;
    protected CalendarExtender txtStartDate_CalendarExtender;
    protected Label lblUntil;
    protected TextBox txtEndDate;
    protected FilteredTextBoxExtender txtEndDate_FilteredTextBoxExtender;
    protected CalendarExtender txtEndDate_CalendarExtender;
    protected Button wibSearch;
    protected GridView wdgInventoryHistoricList;
    protected Pager ucPagerInventoryHistoric;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.InicializarCombos();
      this.InicializarCampos();
      this.InventoryHistoricFill(true, Convert.ToInt32(this.chkFechas.Checked));
    }

    protected void chkFechas_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.chkFechas.Checked)
      {
        this.txtStartDate.Enabled = false;
        this.txtEndDate.Enabled = false;
        this.txtStartDate.Text = "";
        this.txtEndDate.Text = "";
      }
      else
      {
        this.txtStartDate.Enabled = true;
        this.txtEndDate.Enabled = true;
        this.txtStartDate.Text = DateTime.Now.ToString("d", (IFormatProvider) CultureInfo.CurrentCulture);
        this.txtEndDate.Text = DateTime.Now.ToString("d", (IFormatProvider) CultureInfo.CurrentCulture);
      }
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      this.InventoryHistoricFill(true, Convert.ToInt32(this.chkFechas.Checked));
      this.HidePopup();
    }

    protected void wdgInventoryHistoricList_InitializeRow(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowIndex < 0)
        return;
      (e.Row.FindControl("imgDetalle") as ImageButton).ImageUrl = "~/Images/search.gif";
    }

    protected void wdgInventoryHistoricList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      int int32 = Convert.ToInt32(e.CommandArgument);
      GridViewRow row = this.wdgInventoryHistoricList.Rows[int32];
      string str = this.wdgInventoryHistoricList.DataKeys[int32]["I_InventoryHistoricId"].ToString();
      if (!(e.CommandName == "view"))
        return;
      this.Response.Redirect("InventoryDetailsManagement.aspx?InventoryHistoricId=" + Convert.ToString(str, (IFormatProvider) CultureInfo.CurrentCulture), true);
    }

    protected void ucPagerInventoryHistoric_PageChanged(object sender, CustomPageChangeArgs e)
    {
      this.InventoryHistoricFill(false, Convert.ToInt32(this.chkFechas.Checked));
    }

    public void InicializarCombos()
    {
      DataTable synchronizationLocation = new ShiftingInventoryQueriesBL().GetSynchronizationLocation();
      this.wddLocation.Items.Clear();
      this.wddLocation.DataSource = (object) synchronizationLocation;
      this.wddLocation.DataTextField = "v_Description";
      this.wddLocation.DataValueField = "i_LocationId";
      this.wddLocation.DataBind();
      this.wddLocation.Items.Insert(0, new ListItem("-- Todos -- ", "-1"));
      this.wddLocation.SelectedValue = "-1";
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.InventoryHistoricStatus.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "1,2,3,4,5,6,7,8",
        (object) "1",
        (object) "1"
      });
      this.wddStatus.Items.Clear();
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (row["i_GroupId"].ToString() == SystemParameterGroups.InventoryHistoricStatus.ToString((IFormatProvider) CultureInfo.CurrentCulture))
            this.wddStatus.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        }
      }
      this.wddStatus.Items.Insert(0, new ListItem("-- Todos -- ", "-1"));
      this.wddStatus.SelectedValue = "-1";
    }

    public void InicializarCampos()
    {
      this.chkFechas.Checked = false;
      this.txtStartDate.Enabled = false;
      this.txtEndDate.Enabled = false;
    }

    private void InventoryHistoricFill(bool pboolLoadPager, int Choose)
    {
      int intStartRow = pboolLoadPager ? 1 : this.ucPagerInventoryHistoric.CurrentPageNumber;
      int intMaxRows = this.ucPagerInventoryHistoric.CurrentPageSize == 0 ? 10 : this.ucPagerInventoryHistoric.CurrentPageSize;
      int intTotalRows = 0;
      DataTable dataTable = new DataTable();
      ShiftingInventoryQueriesBL inventoryQueriesBl = new ShiftingInventoryQueriesBL();
      if (Choose == 0)
        dataTable = inventoryQueriesBl.InventoryHistoricList(Convert.ToInt32(this.wddLocation.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.wddStatus.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture), intStartRow, intMaxRows, out intTotalRows);
      else if (Choose == 1)
      {
        DateTime dateTime1 = Convert.ToDateTime(this.txtStartDate.Text, (IFormatProvider) CultureInfo.CurrentCulture);
        DateTime dateTime2 = Convert.ToDateTime(this.txtEndDate.Text, (IFormatProvider) CultureInfo.CurrentCulture);
        this.dateStartDate = dateTime1.ToString("yyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture);
        this.dateEndDate = dateTime2.ToString("yyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture);
        dataTable = inventoryQueriesBl.InventoryHistoricListRangeDate(Convert.ToInt32(this.wddLocation.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.wddStatus.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture), this.dateStartDate, this.dateEndDate, intStartRow, intMaxRows, out intTotalRows);
      }
      this.wdgInventoryHistoricList.DataSource = (object) dataTable;
      this.wdgInventoryHistoricList.DataBind();
      this.ucPagerInventoryHistoric.TotalPages = intTotalRows % intMaxRows == 0 ? intTotalRows / intMaxRows : intTotalRows / intMaxRows + 1;
      this.ucPagerInventoryHistoric.TotalRecordCount = intTotalRows;
      if (!pboolLoadPager)
        return;
      this.ucPagerInventoryHistoric.LoadPager();
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
