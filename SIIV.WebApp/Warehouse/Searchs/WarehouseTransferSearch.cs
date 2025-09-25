// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Searchs.WarehouseTransferSearch
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
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
  public class WarehouseTransferSearch : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtTransferNumber;
    protected FilteredTextBoxExtender txtTransferNumber_FilteredTextBoxExtender;
    protected Button wibSearch;
    protected GridView wdgWarehouseTransferList;
    protected Pager custPagerBatch;
    protected Label lblRecordCount;
    protected Label lblMsgError;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.SearchWarehouseTransfer();
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchWarehouseTransfer();

    protected void wdgWarehouseTransferList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (!(e.CommandName == "Select"))
        return;
      this.AddNewProduct(this.wdgWarehouseTransferList.Rows[Convert.ToInt32(e.CommandArgument)]);
      this.PopupClose();
    }

    protected void wdgWarehouseTransferList_PageIndexChanged(object sender, EventArgs e)
    {
      this.SearchWarehouseTransfer();
    }

    protected void custPagerBatch_PageChanged(object sender, CustomPageChangeArgs e)
    {
      int iLocationId = (this.Session["SystemUser"] as SystemUser).i_LocationId;
      int pintWarehouseTransferId = 0;
      if (this.txtTransferNumber.Text != string.Empty)
        pintWarehouseTransferId = int.Parse(this.txtTransferNumber.Text, (IFormatProvider) CultureInfo.CurrentCulture);
      this.SearchWarehouseTransferList(pintWarehouseTransferId, 0, 0, 0, iLocationId, 0, false);
    }

    private void SearchWarehouseTransfer()
    {
      try
      {
        int iLocationId = (this.Session["SystemUser"] as SystemUser).i_LocationId;
        int pintWarehouseTransferId = 0;
        if (this.txtTransferNumber.Text != string.Empty)
          pintWarehouseTransferId = int.Parse(this.txtTransferNumber.Text, (IFormatProvider) CultureInfo.CurrentCulture);
        this.SearchWarehouseTransferList(pintWarehouseTransferId, 0, 0, 0, iLocationId, 0, true);
      }
      catch (Exception ex)
      {
        this.PopupClose();
        Message.SetMessage(this.lblRecordCount, enmMessageType.Error, "Error*****<br>" + ex.Message);
      }
    }

    private void SearchWarehouseTransferList(
      int pintWarehouseTransferId,
      int pintStockMovementId,
      int pintHomeWarehouseId,
      int pintHomeLocationId,
      int pintTargetLocationId,
      int pintMovementTypeId,
      bool pboolLoadPager)
    {
      int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerBatch.CurrentPageNumber;
      int pintMaxRows = this.custPagerBatch.CurrentPageSize == 0 ? 10 : this.custPagerBatch.CurrentPageSize;
      int pintTotalRows;
      DataTable warehouseTransferBy = new WarehouseTransferQueriesBL().GetWarehouseTransferBy(pintWarehouseTransferId, pintStockMovementId, pintHomeWarehouseId, pintHomeLocationId, pintTargetLocationId, pintMovementTypeId, pintStartRowIndex, pintMaxRows, out pintTotalRows);
      int num = pintTotalRows;
      this.wdgWarehouseTransferList.DataSource = (object) warehouseTransferBy;
      this.wdgWarehouseTransferList.DataBind();
      this.custPagerBatch.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
      this.custPagerBatch.TotalRecordCount = pintTotalRows;
      if (!pboolLoadPager)
        return;
      this.custPagerBatch.LoadPager();
    }

    private void AddNewProduct(GridViewRow pobjSelectedRowsGrid)
    {
      bool flag = false;
      DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
      int int32 = Convert.ToInt32(this.wdgWarehouseTransferList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_StockMovementId"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.Session["intTargetWarehouseId"] = (object) Convert.ToInt32(this.wdgWarehouseTransferList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_TargetWarehouseId"], (IFormatProvider) CultureInfo.CurrentCulture);
      DataTable movementDetailTransfer = new StockMovementQueriesBL().GetStockMovementDetailTransfer(0, int32);
      foreach (DataRow row in (InternalDataCollectionBase) movementDetailTransfer.Rows)
      {
        if (dataTable != null)
        {
          for (int index = 0; index < dataTable.Rows.Count; ++index)
          {
            if (Convert.ToString(dataTable.Rows[index]["v_Plate"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToString(row["v_Plate"], (IFormatProvider) CultureInfo.CurrentCulture))
            {
              this.lblMsgError.Text = "El producto ya fue agregado.";
              flag = true;
              if (Convert.ToInt32(dataTable.Rows[index]["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == 0)
              {
                dataTable.Rows[index]["i_Status"] = (object) 1;
                break;
              }
              break;
            }
          }
        }
      }
      if (!flag)
      {
        if (dataTable == null)
          dataTable = new DTStockMovementDetail().DataTableStockMovementDetail().Clone();
        foreach (DataRow row in (InternalDataCollectionBase) movementDetailTransfer.Rows)
          dataTable.Rows.Add(row["i_StockMovementDetailId"], row["i_StockMovementId"], row["i_LocationWarehouseId"], row["i_ProductId"], row["i_MovementCurrencyId"], row["f_SupplierPrice"], row["i_Quantity"], row["v_Description"], row["i_IdAssociated"], row["i_Balance"], row["i_Status"], row["f_ExchangeRate"], row["f_LocalSupplierPrice"], row["f_AdditionalAmount"], row["f_LocalFinalPrice"], row["f_SubTotal"], row["i_Item"], row["v_ObjectId"], null, null, row["v_Plate"], row["i_VehicleTypeUseId"]);
      }
      this.Session["sedtProductAdd"] = (object) dataTable;
    }

    private void PopupClose()
    {
      string script = "SendInfoWarehouseTransferPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }
  }
}
