// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.VerifyProductsWastageDetail
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using System;
using System.Data;
using System.Globalization;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class VerifyProductsWastageDetail : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected GridView wdgWastage;
    protected Pager custPagerWastageDetail;
    protected Label lblRecordCount;
    protected Label lblComentario;
    protected TextBox txtComments;
    protected Button wibSave;
    protected Button wibReturn;
    protected Label lblMsg;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.ViewState["sedtProducts"] = (object) new DTStockMovementDetail().DataTableStockMovementDetail();
      this.ViewState["i_DispatchId"] = (object) Convert.ToInt32(this.Session["i_DispatchId"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.ViewState["i_BatchId"] = (object) Convert.ToInt32(this.Session["i_BatchId"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.GetProducts();
    }

    protected void custPagerWastageDetail_PageChanged(object sender, CustomPageChangeArgs e)
    {
      int int32 = Convert.ToInt32(this.ViewState["i_DispatchId"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.GetProductList(Convert.ToInt32(this.ViewState["i_BatchId"], (IFormatProvider) CultureInfo.CurrentCulture), int32, false);
    }

    protected void wdgWastage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (!e.CommandName.Equals("Check", StringComparison.CurrentCulture))
        return;
      int int32_1 = Convert.ToInt32(e.CommandArgument);
      GridViewRow row1 = this.wdgWastage.Rows[int32_1];
      int int32_2 = Convert.ToInt32(this.wdgWastage.DataKeys[int32_1]["i_Item"]);
      DataTable dataTable = this.ViewState["sedtProducts"] as DataTable;
      foreach (DataRow row2 in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (Convert.ToInt32(row2["i_Item"], (IFormatProvider) CultureInfo.CurrentCulture) == int32_2)
        {
          row2["i_Status"] = Convert.ToInt32(row2["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) != 1 ? (object) 1 : (object) 0;
          break;
        }
      }
      this.ViewState["sedtProducts"] = (object) dataTable;
      this.wdgWastage.DataSource = (object) dataTable;
      this.wdgWastage.DataBind();
    }

    protected void wdgWastage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowIndex < 0)
        return;
      ImageButton control = e.Row.FindControl("ibtCheck") as ImageButton;
      DataRow row = ((DataRowView) e.Row.DataItem).Row;
      if (Convert.ToInt32(row["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == 0)
      {
        control.ImageUrl = "~/Images/Design/checkbox_unchecked_16.png";
      }
      else
      {
        if (Convert.ToInt32(row["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) != 1)
          return;
        control.ImageUrl = "~/Images/Design/checkbox_checked_16.png";
      }
    }

    protected void wdgWastage_PageIndexChanged(object sender, EventArgs e) => this.GetProducts();

    protected void wibSave_Click(object sender, EventArgs e)
    {
      this.lblMsg.Visible = false;
      DataTable pdtStockMovementDetail = this.ViewState["sedtProducts"] as DataTable;
      if (!this.ValidateChecks())
      {
        this.HidePopup();
        Message.SetMessage(this.lblMsg, enmMessageType.Warning, "No se puede registrar el movimiento porque debe seleccionar todos los productos...");
      }
      else
      {
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
        {
          Timeout = new TimeSpan(1, 1, 1)
        }))
        {
          try
          {
            new StockMovementManagementBL().StockMovementInsertInterchangeObject(this.GetCurrentStockMovement(), pdtStockMovementDetail);
            new WarehouseQueriesBL().WarehouseWastageUpdate(Convert.ToInt32(this.Session["i_BatchId"], (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.Session["i_DispatchId"], (IFormatProvider) CultureInfo.CurrentCulture));
            this.HidePopup();
            Message.SetMessage(this.lblMsg, enmMessageType.Success, "Se registró correctamente el movimiento.");
            this.txtComments.Text = "";
            DataTable dataTable = this.ViewState["sedtProducts"] as DataTable;
            dataTable.Clear();
            this.ViewState["sedtProducts"] = (object) dataTable;
            this.txtComments.Enabled = false;
            this.wibSave.Enabled = false;
            transactionScope.Complete();
          }
          catch (Exception ex)
          {
            this.HidePopup();
            Message.SetMessage(this.lblMsg, enmMessageType.Error, ex.Message);
          }
        }
      }
    }

    protected void wibReturn_Click(object sender, EventArgs e) => this.PopupClose();

    private void GetProducts()
    {
      try
      {
        int int32 = Convert.ToInt32(this.ViewState["i_DispatchId"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.GetProductList(Convert.ToInt32(this.ViewState["i_BatchId"], (IFormatProvider) CultureInfo.CurrentCulture), int32, true);
        this.HidePopup();
      }
      catch (Exception ex)
      {
        this.HidePopup();
        Message.SetMessage(this.lblMsg, enmMessageType.Error, "Error*****<br>" + ex.Message);
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void GetProductList(int pintBatchId, int pintDispatchId, bool pboolLoadPager)
    {
      int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerWastageDetail.CurrentPageNumber;
      int pintMaxRows = this.custPagerWastageDetail.CurrentPageSize == 0 ? 10 : this.custPagerWastageDetail.CurrentPageSize;
      int pintTotalRows;
      DataTable dataTable1 = new WarehouseQueriesBL().WarehouseWastageDetailRead(pintBatchId, pintDispatchId, pintStartRowIndex, pintMaxRows, out pintTotalRows);
      DataTable dataTable2 = this.ViewState["sedtProducts"] as DataTable;
      if (dataTable1 != null || dataTable1.Rows.Count > 0)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
          dataTable2.Rows.Add((object) 0, (object) 0, null, row["i_ProductId"], (object) 1, (object) 0.0, (object) 1, row["v_Description"], (object) 1, (object) 0, (object) 0, (object) 0.0f, (object) 0.0f, (object) 0.0f, (object) 0.0f, (object) 0.0f, row["i_Item"]);
      }
      int num = pintTotalRows;
      this.wdgWastage.DataSource = (object) null;
      this.wdgWastage.DataSource = (object) dataTable1;
      this.wdgWastage.DataBind();
      this.wibSave.Enabled = dataTable1 != null && dataTable1.Rows.Count != 0;
      this.custPagerWastageDetail.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
      this.custPagerWastageDetail.TotalRecordCount = pintTotalRows;
      if (!pboolLoadPager)
        return;
      this.custPagerWastageDetail.LoadPager();
    }

    private bool ValidateChecks()
    {
      int num = 0;
      DataTable dataTable = this.ViewState["sedtProducts"] as DataTable;
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        if (Convert.ToInt32(dataTable.Rows[index]["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == 1)
          ++num;
      }
      return num == this.wdgWastage.Rows.Count;
    }

    private StockMovement GetCurrentStockMovement()
    {
      SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
      int num = 0;
      if (systemUser != null)
        num = systemUser.i_SystemUserId;
      return new StockMovement()
      {
        i_StockMovementId = 0,
        i_WarehouseId = new int?(Convert.ToInt32(this.Session["i_WarehouseId"], (IFormatProvider) CultureInfo.CurrentCulture)),
        i_MotiveMovementId = new int?(Convert.ToInt32(this.Session["i_MotiveMovementId"], (IFormatProvider) CultureInfo.CurrentCulture)),
        i_SupplierId = new int?(),
        i_DocumentTypeId = new int?(),
        v_DocumentNumber = "",
        i_UserId = new int?(num),
        b_Checked = new bool?(true),
        v_Observation = this.txtComments.Text,
        d_InsertDate = new DateTime?(DateTime.Now),
        i_ProductionOrderId = new int?(),
        i_ShelfOnDemandId = -1
      };
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private void PopupClose()
    {
      string script = "SendInfoPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
