// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Searchs.SupplierSearch
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Searchs
{
  public class SupplierSearch : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected RadioButtonList rblCriterioFiltro;
    protected TextBox txtFilter;
    protected Button wibSearch;
    protected GridView wdgSupplierList;
    protected Pager custPagerBatch;
    protected Label lblRecordCount;
    protected HiddenField hidSupplierName;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.SearchSupplier();
    }

    protected void rblCriterioFiltro_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.txtFilter.Text = string.Empty;
      this.txtFilter.Focus();
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchSupplier();

    protected void wdgSupplierList_RowSelectionChanged(object sender, EventArgs e)
    {
    }

    protected void wdgSupplierList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      if (!(e.CommandName == "Select"))
        return;
      int int32 = Convert.ToInt32(e.CommandArgument);
      GridViewRow row = this.wdgSupplierList.Rows[int32];
      this.PopupClose(string.Format((IFormatProvider) CultureInfo.CurrentCulture, "{0},{1}", new object[2]
      {
        (object) this.wdgSupplierList.DataKeys[int32]["i_SupplierId"].ToString(),
        (object) this.Page.Server.HtmlDecode(row.Cells[2].Text)
      }));
    }

    protected void wdgSupplierList_PageIndexChanged(object sender, EventArgs e)
    {
      this.SearchSupplier();
    }

    protected void custPagerBatch_PageChanged(object sender, CustomPageChangeArgs e)
    {
      string pstrName = string.Empty;
      string pstrOrganizationIdentifier = string.Empty;
      switch (int.Parse(this.rblCriterioFiltro.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture))
      {
        case 0:
          pstrName = this.txtFilter.Text;
          break;
        case 1:
          pstrOrganizationIdentifier = this.txtFilter.Text;
          break;
      }
      this.SearchSupplierList(0, pstrName, pstrOrganizationIdentifier, false);
    }

    private void PopupClose(string pstrValuesPopup)
    {
      string script = "SendInfoSupplierPopup('" + pstrValuesPopup + "');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void SearchSupplier()
    {
      try
      {
        string pstrName = string.Empty;
        string pstrOrganizationIdentifier = string.Empty;
        switch (int.Parse(this.rblCriterioFiltro.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture))
        {
          case 0:
            pstrName = string.IsNullOrEmpty(this.txtFilter.Text) ? "" : this.txtFilter.Text;
            break;
          case 1:
            pstrOrganizationIdentifier = string.IsNullOrEmpty(this.txtFilter.Text) ? "" : this.txtFilter.Text;
            break;
        }
        this.SearchSupplierList(0, pstrName, pstrOrganizationIdentifier, true);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, "Error*****" + ex.Message);
      }
    }

    private void SearchSupplierList(
      int pintSupplierId,
      string pstrName,
      string pstrOrganizationIdentifier,
      bool pboolLoadPager)
    {
      int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerBatch.CurrentPageNumber;
      int pintMaxRows = this.custPagerBatch.CurrentPageSize == 0 ? 10 : this.custPagerBatch.CurrentPageSize;
      int pintTotalRows;
      DataTable supplierByPag = new SupplierQueriesBL().GetSupplierByPag(pintSupplierId, pstrName, pstrOrganizationIdentifier, pintStartRowIndex, pintMaxRows, out pintTotalRows);
      int num = pintTotalRows;
      this.wdgSupplierList.DataSource = (object) supplierByPag;
      this.wdgSupplierList.DataBind();
      this.custPagerBatch.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
      this.custPagerBatch.TotalRecordCount = pintTotalRows;
      if (!pboolLoadPager)
        return;
      this.custPagerBatch.LoadPager();
    }

    protected void wdgSupplierList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgSupplierList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
  }
}
