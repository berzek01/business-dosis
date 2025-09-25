// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.StockMovementFactoryList
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class StockMovementFactoryList : Page
  {
    private MaintenanceOperation currentOperation = MaintenanceOperation.None;
    protected HiddenField H1;
    protected UpdatePanel UpdatePanel1;
    protected DropDownList wddWarehouseList;
    protected Fecha wdpDateIni;
    protected Fecha wdpDateFin;
    protected DropDownList wddMotiveMovementList;
    protected Button wibSearch;
    protected DropDownList wddProduct;
    protected GridView wdgStockMovementProduct;
    protected Pager custPagerSMF;
    protected Label lblMessage1;
    protected Button btnMostrarKardex;
    protected Button wibNew;
    protected UpdatePanel UpdatePanel2;
    protected TextBox txtSupplier;
    protected Button btnShowSupplier;
    protected DropDownList wddWarehouse;
    protected Fecha wdpDate;
    protected DropDownList wddFlow;
    protected DropDownList wddMotiveMovement;
    protected DropDownList wddDocumentType;
    protected TextBox txtDocumentType;
    protected DropDownList wddCurrency;
    protected TextBox txtExchangeRate;
    protected ImageButton btCurrencyUpdates;
    protected TextBox txtObservation;
    protected Button wibAgregar;
    protected GridView wdgProductDetail;
    protected Button wibSave;
    protected Button wibCancel;
    protected Button btnReturnPopupConfirmation;
    protected Button btnReturnPopupConfirmationStock;
    protected HiddenField hfStockMovementId;
    protected HiddenField hfSupplierId;
    protected HiddenField hfSupplierName;
    protected Label lblMessage;
    protected Button btnJavaScriptResponse;
    protected Button btnAceptChangeQuantity;
    protected Button btnJavaScriptResponseEdit;
    protected Button wibAccept;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      this.lblMessage1.Visible = false;
      if (this.Page.IsPostBack)
        return;
      this.LoadWarehouse();
      this.LoadMotiveMovementList(Convert.ToInt32(this.wddWarehouseList.SelectedValue));
      this.LoadProduct(Convert.ToInt32(this.wddWarehouseList.SelectedValue));
      this.LoadParameters();
      this.SetDatePicker();
      this.BuildDTStockMovementDetail();
      this.BuilProductList();
      this.btnShowSupplier.OnClientClick = this.CreatePopUp("Lista Proveedores", "../Searchs/SupplierSearch.aspx", "800px", "650px");
      this.wibAgregar.OnClientClick = this.CreatePopUp("Lista Productos", "../Searchs/ProductSearch.aspx", "800px", "750px");
    }

    protected void wddWarehouseList_SelectionChanged(object sender, EventArgs e)
    {
      this.LoadMotiveMovementList(Convert.ToInt32(this.wddWarehouseList.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
      this.LoadProduct(Convert.ToInt32(this.wddWarehouseList.SelectedValue));
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchMovement();

    protected void custPagerSMF_PageChanged(object sender, CustomPageChangeArgs e)
    {
      SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
      int int32_1 = Convert.ToInt32(this.wddProduct.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      int int32_2 = Convert.ToInt32(this.wddWarehouseList.SelectedValue);
      int int32_3 = Convert.ToInt32(this.wddMotiveMovementList.SelectedValue);
      int iLocationId = systemUser.i_LocationId;
      int num = systemUser.i_CompanyId.Value;
      string pstrBeginDate = Convert.ToDateTime(this.wdpDateIni.Value).ToString("yyyyMMdd");
      string pstrEndDate = Convert.ToDateTime(this.wdpDateFin.Value).ToString("yyyyMMdd");
      this.SearchStockMovementList(0, int32_2, iLocationId, int32_3, int32_1, pstrBeginDate, pstrEndDate, false);
    }

    protected void wdgStockMovementProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        int int32 = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = this.wdgStockMovementProduct.Rows[int32];
        this.ViewState["indexWdgStockMovementProduct"] = (object) int32;
        if (e.CommandName == "Edit")
        {
          SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
          int iLocationId = systemUser.i_LocationId;
          int num = systemUser.i_CompanyId.Value;
          this.currentOperation = MaintenanceOperation.Edit;
          this.ShowStockMovementInfo(this.GetCurrentStockMovement(enmTypeLoadData.SelectedRowsGrid));
          DataTable movementDetailfBy = new StockMovementQueriesBL().GetStockMovementDetailfBy(0, int.Parse(row.Cells[0].Text));
          if (movementDetailfBy.Rows.Count == 0)
            return;
          this.wdgProductDetail.DataSource = (object) movementDetailfBy;
          this.wdgProductDetail.DataBind();
          try
          {
            this.wddCurrency.SelectedValue = movementDetailfBy.Rows[0]["i_MovementCurrencyId"].ToString() == "-1" ? "0" : movementDetailfBy.Rows[0]["i_MovementCurrencyId"].ToString();
          }
          catch
          {
            this.wddCurrency.SelectedValue = "0";
          }
          this.txtExchangeRate.Text = movementDetailfBy.Rows[0]["f_ExchangeRate"].ToString();
          DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
          this.Session["sedtProductAdd"] = (object) movementDetailfBy;
          this.EnabledControls(MaintenanceOperation.Edit);
          this.HidePopup();
        }
        else if (e.CommandName == "Delete")
        {
          this.currentOperation = MaintenanceOperation.Delete;
          this.ShowStockMovementInfo(this.GetCurrentStockMovement(enmTypeLoadData.SelectedRowsGrid));
          this.EnabledControls(MaintenanceOperation.Delete);
          this.wibSave.Enabled = true;
          this.HidePopup();
        }
        this.ViewState["currentOperation"] = (object) this.currentOperation;
      }
      catch (Exception ex)
      {
        this.HidePopup();
        Message.SetMessage(this.lblMessage, enmMessageType.Error, "Error******<br>" + ex.Message);
      }
    }

    protected void wdgStockMovementProduct_PageIndexChanged(object sender, EventArgs e)
    {
      this.SearchMovement();
    }

    protected void wibNew_Click(object sender, EventArgs e)
    {
      try
      {
        ProductWarehouseQueriesBL warehouseQueriesBl = new ProductWarehouseQueriesBL();
        DataTable dataTable = new DataTable();
        if (warehouseQueriesBl.ProductStockAlert(1).Rows.Count > 0)
        {
          string empty = string.Empty;
          this.CreatePopUpServer("Alerta de Stocks", "StockAlert.aspx", "710px", "550px");
        }
        else
          this.Load();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, "Error... " + ex.Message);
      }
    }

    protected void wddWarehouse_SelectionChanged(object sender, EventArgs e)
    {
      SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
      int iLocationId = systemUser.i_LocationId;
      int num = systemUser.i_CompanyId.Value;
      this.Session["sWarehouseId2"] = (object) this.wddWarehouse.SelectedValue;
      if (this.Session["sedtProductAdd"] != null)
      {
        DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
        dataTable.Rows.Clear();
        this.Session["sedtProductAdd"] = (object) dataTable;
      }
      this.wddFlow.SelectedValue = "0";
      this.wddFlow_SelectionChanged((object) null, (EventArgs) null);
      this.ButtonActive();
    }

    protected void wddFlow_SelectionChanged(object sender, EventArgs e)
    {
      SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
      int iLocationId = systemUser.i_LocationId;
      int num = systemUser.i_CompanyId.Value;
      this.Session["Type"] = (object) this.wddFlow.SelectedValue;
      this.LoadMotiveMovement(Convert.ToInt32(this.wddWarehouse.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.wddFlow.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
      this.wddMotiveMovement_SelectionChanged((object) null, (EventArgs) null);
      if (this.Session["sedtProductAdd"] != null)
      {
        DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
        dataTable.Rows.Clear();
        this.Session["sedtProductAdd"] = (object) dataTable;
      }
      this.wibSave.Enabled = this.wdgProductDetail.Rows.Count > 0;
      this.ButtonActive();
      this.HidePopup();
    }

    protected void wddMotiveMovement_SelectionChanged(object sender, EventArgs e)
    {
      SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
      int iLocationId = systemUser.i_LocationId;
      int num = systemUser.i_CompanyId.Value;
      if (this.Session["sedtProductAdd"] != null)
      {
        DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
        dataTable.Rows.Clear();
        this.Session["sedtProductAdd"] = (object) dataTable;
        this.wdgProductDetail.DataSource = (object) dataTable;
        this.wdgProductDetail.DataBind();
      }
      DataTable dataTable1 = this.Session["btMotiveMovementAAP"] as DataTable;
      this.Session["ShowColumns"] = (object) "0";
      foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
      {
        if (Convert.ToInt32(row["i_MotiveMovementId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(this.wddMotiveMovement.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
        {
          this.Session["ShowColumns"] = (object) Convert.ToInt32(row["i_ShowColumns"], (IFormatProvider) CultureInfo.CurrentCulture);
          break;
        }
      }
      if (Convert.ToInt32(this.Session["ShowColumns"]) != 1)
      {
        this.txtExchangeRate.Text = "0";
        this.txtExchangeRate.Enabled = false;
        this.wddCurrency.SelectedValue = "0";
        this.wddCurrency.Enabled = false;
        this.btCurrencyUpdates.Enabled = false;
      }
      else
      {
        this.txtExchangeRate.Text = "0";
        this.txtExchangeRate.Enabled = true;
        this.wddCurrency.SelectedValue = "0";
        this.wddCurrency.Enabled = true;
        this.btCurrencyUpdates.Enabled = true;
      }
      this.wibSave.Enabled = this.wdgProductDetail.Rows.Count > 0;
      this.ButtonActive();
    }

    protected void wddDocumentType_SelectionChanged(object sender, EventArgs e)
    {
      SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
      int iLocationId = systemUser.i_LocationId;
      int num = systemUser.i_CompanyId.Value;
      this.wibSave.Enabled = this.wdgProductDetail.Rows.Count > 0;
      this.ButtonActive();
    }

    protected void wddCurrency_SelectionChanged(object sender, EventArgs e)
    {
      SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
      int iLocationId = systemUser.i_LocationId;
      int num1 = systemUser.i_CompanyId.Value;
      if (Convert.ToInt32(this.wddCurrency.SelectedValue) <= 1)
      {
        this.txtExchangeRate.Text = "0";
        this.txtExchangeRate.Enabled = false;
      }
      else if (Convert.ToDouble(this.txtExchangeRate.Text.Trim() == "" ? "0" : this.txtExchangeRate.Text) == 0.0)
      {
        this.txtExchangeRate.Text = "0";
        this.txtExchangeRate.Enabled = true;
      }
      if (this.Session["sedtProductAdd"] != null)
      {
        DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          double num2 = row["f_AdditionalAmount"].ToString() != "" ? Convert.ToDouble(row["f_AdditionalAmount"]) : 0.0;
          double num3;
          if (this.wddCurrency.SelectedValue != "1")
          {
            num3 = row["f_SupplierPrice"].ToString() != "" ? Convert.ToDouble(row["f_SupplierPrice"]) * Convert.ToDouble(this.txtExchangeRate.Text) : 0.0;
            row["f_LocalSupplierPrice"] = (object) num3;
            row["f_ExchangeRate"] = (object) this.txtExchangeRate.Text;
          }
          else
          {
            num3 = row["f_SupplierPrice"].ToString() != "" ? Convert.ToDouble(row["f_SupplierPrice"]) : 0.0;
            row["f_LocalSupplierPrice"] = (object) num3;
            row["f_ExchangeRate"] = (object) 0.0;
          }
          double num4 = num3 + num2;
          row["f_LocalFinalPrice"] = (object) num4;
          row["f_SubTotal"] = (object) (Convert.ToDouble(row["f_LocalFinalPrice"]) * Convert.ToDouble(row["i_Quantity"]));
          row["i_MovementCurrencyId"] = (object) Convert.ToInt32(this.wddCurrency.SelectedValue);
        }
        this.wdgProductDetail.DataSource = (object) dataTable;
        this.wdgProductDetail.DataBind();
        this.Session["sedtProductAdd"] = (object) dataTable;
      }
      this.ButtonActive();
    }

    protected void btCurrencyUpdates_Click(object sender, ImageClickEventArgs e)
    {
      this.LoadGridDetailProduct();
    }

    protected void wdgProductDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      int int32 = Convert.ToInt32(e.CommandArgument);
      GridViewRow row = this.wdgProductDetail.Rows[int32];
      this.ViewState["indexWdgProductDetail"] = (object) int32;
      if (e.CommandName == "Edit")
      {
        this.Session["sEditProduct"] = (object) new List<string>()
        {
          row.Cells[6].Text,
          row.Cells[8].Text,
          row.Cells[9].Text,
          this.wdgProductDetail.DataKeys[int32]["i_ProductId"].ToString()
        };
        this.CreatePopUpServer("Editar Item", "../Searchs/ProductSearchEdit.aspx", "400px", "140px");
      }
      if (e.CommandName == "Delete")
        this.DeleteRowCesta(Convert.ToInt32(this.wdgProductDetail.DataKeys[int32]["i_ProductId"].ToString()));
      this.wibSave.Enabled = this.wdgProductDetail.Rows.Count > 0;
    }

    protected void wdgProductDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void wdgProductDetail_PageIndexChanged(object sender, EventArgs e)
    {
      this.SearchProduct();
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.CreatePopUpServer("SIIV-Almacen", "../../UserControls/PopupConfirmation.aspx?MessageTypeId=1&MessageText=¿Está seguro que desea grabar?", "350px", "190px");
    }

    protected void wibCancel_Click(object sender, EventArgs e)
    {
      this.EnabledControls(MaintenanceOperation.None);
      if (this.Session["sedtProductAdd"] != null)
      {
        DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
        dataTable.Rows.Clear();
        this.wdgProductDetail.DataSource = (object) dataTable;
        this.wdgProductDetail.DataBind();
        this.Session["sedtProductAdd"] = (object) null;
      }
      this.HidePopup();
    }

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
      using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
      {
        Timeout = new TimeSpan(1, 1, 1)
      }))
      {
        bool flag = false;
        try
        {
          this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperation"];
          DataTable pdtStockMovementDetail = this.Session["sedtProductAdd"] as DataTable;
          switch (this.currentOperation)
          {
            case MaintenanceOperation.AddNew:
              if (!this.IsValidInputData())
              {
                flag = true;
                break;
              }
              DataTable dataTable = this.Session["dtWarehouse"] as DataTable;
              string str = "";
              foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
              {
                if (Convert.ToInt32(row["i_WarehouseId"].ToString()) == Convert.ToInt32(this.wddWarehouse.SelectedValue))
                  str = row["v_WareHouseTypeUseFactory"].ToString();
              }
              StockMovement currentStockMovement1 = this.GetCurrentStockMovement(enmTypeLoadData.SelectedTextbox);
              if (str == "1")
              {
                int i_StockMovementIdIn = 0;
                new StockMovementManagementBL().StockMovementInsertFactory(currentStockMovement1, pdtStockMovementDetail, i_StockMovementIdIn, out int _);
                new ProductWarehouseQueriesBL().ProductStockUpdateStatus(0, 0, 2, systemUser.i_SystemUserId);
              }
              Message.SetMessage(this.lblMessage, enmMessageType.Success, "Correcto*****<br>El Movimiento se creó satisfactoriamente");
              this.ClearCesta();
              this.EnabledControls(MaintenanceOperation.AddNew);
              this.wibAgregar.Enabled = false;
              break;
            case MaintenanceOperation.Edit:
              if (!this.IsValidInputData())
              {
                flag = true;
                break;
              }
              StockMovement currentStockMovement2 = this.GetCurrentStockMovement(enmTypeLoadData.SelectedTextbox);
              GridViewRow row1 = this.wdgStockMovementProduct.Rows[Convert.ToInt32(this.ViewState["indexWdgStockMovementProduct"])];
              currentStockMovement2.i_StockMovementId = int.Parse(row1.Cells[0].Text);
              new StockMovementManagementBL().StockMovementUpdateFactory(currentStockMovement2, pdtStockMovementDetail);
              Message.SetMessage(this.lblMessage, enmMessageType.Success, "Correcto*****<br>El Movimiento se modificó satisfactoriamente");
              this.ClearCesta();
              this.EnabledControls(MaintenanceOperation.Edit);
              break;
            case MaintenanceOperation.Delete:
              new StockMovementManagementBL().StockMovementDelete(int.Parse(this.wdgStockMovementProduct.Rows[Convert.ToInt32(this.ViewState["indexWdgStockMovementProduct"])].Cells[0].Text));
              Message.SetMessage(this.lblMessage, enmMessageType.Success, "Correcto*****<br> El Movimiento se anuló satisfactoriamente");
              this.ClearCesta();
              this.EnabledControls(MaintenanceOperation.Delete);
              break;
          }
          if (flag)
            return;
          this.Session["sedtProductAdd"] = (object) null;
          this.lblMessage.Visible = true;
          this.txtSupplier.Enabled = false;
          this.txtObservation.Enabled = false;
          this.txtDocumentType.Enabled = false;
          this.txtExchangeRate.Enabled = false;
          this.wibSave.Visible = false;
          this.wibCancel.Visible = false;
          this.wibAccept.Visible = true;
          this.btnShowSupplier.Enabled = false;
          this.wddWarehouse.Enabled = false;
          this.wddFlow.Enabled = false;
          this.wddMotiveMovement.Enabled = false;
          this.wddDocumentType.Enabled = false;
          this.wddCurrency.Enabled = false;
          this.wdpDate.Enabled = false;
          transactionScope.Complete();
        }
        catch (Exception ex)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Error, "Error******<br>" + ex.Message);
        }
        finally
        {
          this.HidePopup();
        }
      }
    }

    protected void btnReturnPopupConfirmationStock_Click(object sender, EventArgs e) => this.Load();

    protected void btnJavaScriptResponse_Click(object sender, EventArgs e)
    {
      this.LoadGridDetailProduct();
    }

    protected void btnAceptChangeQuantity_Click(object sender, EventArgs e)
    {
      GridViewRow row1 = this.wdgProductDetail.Rows[Convert.ToInt32(this.ViewState["indexWdgProductDetail"])];
      int int32_1 = Convert.ToInt32(row1.Cells[2].Text);
      int int32_2 = Convert.ToInt32(row1.Cells[4].Text);
      if (this.Session["sedtProductAdd"] == null)
        return;
      DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
      foreach (DataRow row2 in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (Convert.ToInt32(row2["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture) == int32_2)
        {
          row2.BeginEdit();
          row2["i_Quantity"] = (object) int32_1;
          row2["f_SubTotal"] = (object) ((double) int32_1 * (Convert.ToDouble(row2["f_SupplierPrice"], (IFormatProvider) CultureInfo.CurrentCulture) + Convert.ToDouble(row2["f_AdditionalAmount"], (IFormatProvider) CultureInfo.CurrentCulture)));
        }
      }
      this.Session["sedtProductAdd"] = (object) dataTable;
      this.wdgProductDetail.DataSource = (object) dataTable;
      this.wdgProductDetail.DataBind();
    }

    protected void btnJavaScriptResponseEdit_Click(object sender, EventArgs e)
    {
      if (this.Session["sedtProductAdd"] != null)
      {
        DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
        List<string> stringList = this.Session["sEditProduct"] as List<string>;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (row["i_ProductId"].ToString() == stringList[3].ToString())
          {
            row["f_AdditionalAmount"] = (object) stringList[0].ToString();
            row["v_Initial"] = (object) stringList[1].ToString();
            row["v_Final"] = (object) stringList[2].ToString();
          }
        }
      }
      this.LoadGridDetailProduct();
    }

    protected void wibAccept_Click(object sender, EventArgs e)
    {
      string script = UtilDA.ActiveTabIndex("tabs", 0, "1");
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      this.HidePopup();
    }

    protected void btnMostrarKardex_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("~/Warehouse/Reports/WarehouseKardex.aspx");
    }

    private void LoadWarehouse()
    {
      SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
      int iLocationId = systemUser.i_LocationId;
      int num = systemUser.i_CompanyId.Value;
      DataTable dataTable = new DataTable();
      DataTable warehouseBy = new WarehouseQueriesBL().GetWarehouseBy(0, string.Empty, iLocationId, -1);
      this.Session["dtWarehouse"] = (object) warehouseBy;
      DataTable table = new DataView(warehouseBy, "ISNULL(b_IsReserved,0) = 0", "i_WarehouseId", DataViewRowState.CurrentRows).ToTable();
      DataRow row = table.NewRow();
      row["i_WarehouseId"] = (object) 0;
      row["v_Description"] = (object) "- Seleccione -";
      row["v_Address"] = (object) string.Empty;
      row["i_Status"] = (object) 1;
      row["i_LocationId"] = (object) 0;
      row["v_Description2"] = (object) string.Empty;
      row["i_UsePositionLogic"] = (object) 0;
      row["i_WarehouseTypeId"] = (object) 0;
      row["b_IsReserved"] = (object) false;
      row["i_ParentWarehouseId"] = (object) 0;
      row["v_KeyReference"] = (object) string.Empty;
      table.Rows.InsertAt(row, 0);
      this.wddWarehouseList.DataSource = (object) table;
      this.wddWarehouseList.DataTextField = "v_Description";
      this.wddWarehouseList.DataValueField = "i_WarehouseId";
      this.wddWarehouseList.DataBind();
      this.wddWarehouseList.SelectedValue = "0";
      this.wddWarehouse.DataSource = (object) table;
      this.wddWarehouse.DataTextField = "v_Description";
      this.wddWarehouse.DataValueField = "i_WarehouseId";
      this.wddWarehouse.DataBind();
      this.wddWarehouse.SelectedValue = "0";
    }

    private void LoadMotiveMovementList(int pinWarehouseId)
    {
      DataTable dataTable = this.LoadMotiveMovementFactory(pinWarehouseId, 0);
      this.wddMotiveMovementList.DataSource = (object) dataTable;
      DataRow row = dataTable.NewRow();
      row["i_MotiveMovementId"] = (object) 0;
      row["v_Description"] = (object) "- Todos -";
      dataTable.Rows.InsertAt(row, 0);
      this.wddMotiveMovementList.DataTextField = "v_Description";
      this.wddMotiveMovementList.DataValueField = "i_MotiveMovementId";
      this.wddMotiveMovementList.DataBind();
      this.wddMotiveMovementList.SelectedValue = "0";
    }

    private DataTable LoadMotiveMovementFactory(int pinWarehouseId, int pintFlowId)
    {
      return new MotiveMovementQueriesBL().GetMotiveMovementFactory(pinWarehouseId, pintFlowId);
    }

    private void LoadProduct(int pinWarehouseId)
    {
      DataTable dataTable = this.LoadProductMovementFactory(pinWarehouseId);
      this.wddProduct.DataSource = (object) dataTable;
      DataRow row = dataTable.NewRow();
      row["i_ProductId"] = (object) -1;
      row["v_Name"] = (object) "- Todos -";
      dataTable.Rows.InsertAt(row, 0);
      this.wddProduct.DataTextField = "v_Name";
      this.wddProduct.DataValueField = "i_ProductId";
      this.wddProduct.DataBind();
      this.wddProduct.SelectedValue = "-1";
    }

    private DataTable LoadProductMovementFactory(int pinWarehouseId)
    {
      return new MotiveMovementQueriesBL().GetProductMovementFactory(pinWarehouseId);
    }

    protected void LoadParameters()
    {
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) ("" + SystemParameterGroups.ModelCurrency.ToString() + ", " + "520"),
        (object) "",
        (object) "1",
        (object) "1"
      });
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (row["i_GroupId"].ToString() == SystemParameterGroups.ModelCurrency.ToString())
            this.wddCurrency.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          if (row["i_GroupId"].ToString() == "520")
            this.wddDocumentType.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        }
      }
      this.wddDocumentType.Items.Insert(0, new ListItem("- Seleccione - ", "0"));
      this.wddCurrency.Items.Insert(0, new ListItem("- Seleccione - ", "0"));
    }

    private void SetDatePicker()
    {
      this.wdpDateIni.Value = DateTime.Now.AddMonths(-1);
      this.wdpDateFin.Value = DateTime.Now;
      this.wdpDate.Value = DateTime.Now;
    }

    private void EnabledControls(MaintenanceOperation penuCurrentOperation)
    {
      string str = this.H1.Value;
      int num = (this.Session["SystemUser"] as SystemUser).i_CompanyId.Value;
      this.txtSupplier.BackColor = Color.White;
      this.wddMotiveMovement.BackColor = Color.White;
      this.lblMessage.Visible = false;
      switch (penuCurrentOperation)
      {
        case MaintenanceOperation.AddNew:
          this.txtSupplier.Enabled = true;
          this.txtDocumentType.Enabled = true;
          this.txtObservation.Enabled = true;
          this.txtExchangeRate.Enabled = true;
          this.txtSupplier.Text = "";
          this.txtDocumentType.Text = "";
          this.txtObservation.Text = "";
          this.txtExchangeRate.Text = "";
          this.wddFlow.Enabled = true;
          this.wddCurrency.Enabled = true;
          this.wddDocumentType.Enabled = true;
          this.wddWarehouse.Enabled = true;
          this.wddMotiveMovement.Enabled = true;
          this.btnShowSupplier.Enabled = true;
          this.wibAccept.Visible = false;
          this.wibAgregar.Visible = true;
          this.wibAgregar.Enabled = false;
          this.wibSave.Enabled = false;
          this.btCurrencyUpdates.Enabled = true;
          this.SetDatePicker();
          this.wdpDate.Enabled = true;
          this.wdgProductDetail.Enabled = true;
          this.wdgProductDetail.Columns[0].Visible = true;
          this.wdgProductDetail.Columns[1].Visible = false;
          if (str == "0")
          {
            string script1 = UtilDA.ActiveTabIndex("tabs", 1, "0");
            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script1, true);
            string script2 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script2, true);
            this.wibSave.Visible = true;
            this.wibCancel.Visible = true;
            this.wibAccept.Visible = false;
            this.lblMessage.Visible = false;
            break;
          }
          this.wibSave.Visible = false;
          this.wibCancel.Visible = false;
          this.wibAccept.Visible = false;
          break;
        case MaintenanceOperation.Edit:
          this.txtSupplier.Enabled = true;
          this.txtDocumentType.Enabled = true;
          this.txtObservation.Enabled = true;
          this.txtExchangeRate.Enabled = true;
          this.wddFlow.Enabled = false;
          this.wddCurrency.Enabled = false;
          this.wddDocumentType.Enabled = true;
          this.wddWarehouse.Enabled = false;
          this.wddMotiveMovement.Enabled = false;
          this.btnShowSupplier.Enabled = true;
          this.wibAccept.Visible = false;
          this.wibAgregar.Visible = false;
          this.wibAgregar.Enabled = false;
          this.wibSave.Enabled = false;
          this.btCurrencyUpdates.Enabled = true;
          this.wdpDate.Enabled = true;
          this.wdgProductDetail.Columns[0].Visible = false;
          if (this.Session["ShowColumns"] != null)
          {
            if (Convert.ToInt32(this.Session["ShowColumns"]) != 1)
            {
              this.txtExchangeRate.Text = "0";
              this.txtExchangeRate.Enabled = false;
              this.wddCurrency.SelectedValue = "0";
              this.wddCurrency.Enabled = false;
              this.btCurrencyUpdates.Enabled = false;
            }
            else if (Convert.ToInt32(this.wddCurrency.SelectedValue) == -1 || Convert.ToInt32(this.wddCurrency.SelectedValue) == 0)
            {
              this.txtExchangeRate.Text = "0";
              this.txtExchangeRate.Enabled = false;
              this.wddCurrency.SelectedValue = "0";
              this.wddCurrency.Enabled = false;
              this.btCurrencyUpdates.Enabled = false;
            }
            else
            {
              this.txtExchangeRate.Enabled = true;
              this.btCurrencyUpdates.Enabled = true;
            }
            this.wdgProductDetail.Columns[1].Visible = Convert.ToInt32(this.Session["ShowColumns"]) == 1;
          }
          if (str == "0")
          {
            string script3 = UtilDA.ActiveTabIndex("tabs", 1, "0");
            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script3, true);
            string script4 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script4, true);
            this.wibSave.Visible = true;
            this.wibCancel.Visible = true;
            this.wibAccept.Visible = false;
            this.lblMessage.Visible = false;
            break;
          }
          this.wibSave.Visible = false;
          this.wibCancel.Visible = false;
          this.wibAccept.Visible = false;
          break;
        case MaintenanceOperation.Delete:
          this.txtSupplier.Enabled = false;
          this.txtDocumentType.Enabled = false;
          this.txtObservation.Enabled = false;
          this.txtExchangeRate.Enabled = false;
          this.wddFlow.Enabled = false;
          this.wddCurrency.Enabled = false;
          this.wddDocumentType.Enabled = false;
          this.wddWarehouse.Enabled = false;
          this.wddMotiveMovement.Enabled = false;
          this.btnShowSupplier.Enabled = false;
          this.wibAccept.Visible = false;
          this.wibAgregar.Visible = false;
          this.wibAgregar.Enabled = false;
          this.wibSave.Enabled = false;
          this.btCurrencyUpdates.Enabled = false;
          this.wdpDate.Enabled = false;
          if (str == "0")
          {
            string script5 = UtilDA.ActiveTabIndex("tabs", 1, "0");
            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script5, true);
            string script6 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script6, true);
            break;
          }
          string script7 = UtilDA.ActiveTabIndex("tabs", 0, "1");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script7, true);
          string script8 = "TabIndex();";
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script8, true);
          break;
        default:
          string script9 = UtilDA.ActiveTabIndex("tabs", 0, "1");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script9, true);
          string script10 = "TabIndex();";
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script10, true);
          break;
      }
    }

    private void BuildDTStockMovementDetail()
    {
      this.Session["sedtProductAdd"] = (object) new DTStockMovementDetail().DataTableStockMovementDetail();
      DataTable dataTable = new DataTable();
      DataColumn column = new DataColumn("i_Item", typeof (int));
      column.Unique = true;
      column.AutoIncrement = true;
      column.AutoIncrementSeed = 1L;
      column.AutoIncrementStep = 1L;
      dataTable.Columns.Add("i_StockMovementDetailId", typeof (int));
      dataTable.Columns.Add("i_StockMovementId", typeof (int));
      dataTable.Columns.Add("i_LocationWarehouseId", typeof (int));
      dataTable.Columns.Add("i_ProductId", typeof (int));
      dataTable.Columns.Add("i_MovementCurrencyId", typeof (int));
      dataTable.Columns.Add("f_SupplierPrice", typeof (float));
      dataTable.Columns.Add("i_Quantity", typeof (int));
      dataTable.Columns.Add("v_Description", typeof (string));
      dataTable.Columns.Add("i_IdAssociated", typeof (int));
      dataTable.Columns.Add("i_Balance", typeof (int));
      dataTable.Columns.Add("i_Status", typeof (int));
      dataTable.Columns.Add("f_ExchangeRate", typeof (float));
      dataTable.Columns.Add("f_LocalSupplierPrice", typeof (float));
      dataTable.Columns.Add("f_AdditionalAmount", typeof (float));
      dataTable.Columns.Add("f_LocalFinalPrice", typeof (float));
      dataTable.Columns.Add("f_SubTotal", typeof (float));
      dataTable.Columns.Add(column);
      dataTable.Columns.Add("v_ObjectId", typeof (string));
      dataTable.Columns.Add("i_ObjectTypeId", typeof (int));
      dataTable.Columns.Add("i_ProofPaymentTypeId", typeof (int));
      dataTable.Columns.Add("v_Plate", typeof (string));
      dataTable.Columns.Add("i_ShelfTypeUseId", typeof (int));
      dataTable.Columns.Add("v_Initial", typeof (string));
      dataTable.Columns.Add("v_Final", typeof (string));
      dataTable.PrimaryKey = new DataColumn[1]
      {
        dataTable.Columns["i_Item"]
      };
      this.Session["sedtpruebaidentity"] = (object) dataTable;
    }

    private void BuilProductList()
    {
      this.Session["seProductList"] = (object) new DataTable()
      {
        Columns = {
          "i_ProductId",
          "v_Name",
          "v_IsAdded",
          "i_CurrentQuantity",
          "v_Plate"
        }
      };
    }

    private void SearchMovement()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
        int int32_1 = Convert.ToInt32(this.wddProduct.SelectedValue);
        int int32_2 = Convert.ToInt32(this.wddWarehouseList.SelectedValue);
        int int32_3 = Convert.ToInt32(this.wddMotiveMovementList.SelectedValue);
        int iLocationId = systemUser.i_LocationId;
        int num = systemUser.i_CompanyId.Value;
        DateTime dateTime = Convert.ToDateTime(this.wdpDateIni.Value);
        string pstrBeginDate = dateTime.ToString("yyyyMMdd");
        dateTime = Convert.ToDateTime(this.wdpDateFin.Value);
        string pstrEndDate = dateTime.ToString("yyyyMMdd");
        this.SearchStockMovementList(0, int32_2, iLocationId, int32_3, int32_1, pstrBeginDate, pstrEndDate, true);
        this.HidePopup();
      }
      catch (Exception ex)
      {
        this.HidePopup();
        Message.SetMessage(this.lblMessage1, enmMessageType.Error, "Error*****<br>" + ex.Message);
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SearchStockMovementList(
      int pintStockMovementId,
      int pintWarehouseId,
      int pintLocationId,
      int pintMovementTypeId,
      int pintProductId,
      string pstrBeginDate,
      string pstrEndDate,
      bool pboolLoadPager)
    {
      int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerSMF.CurrentPageNumber;
      int pintMaxRows = this.custPagerSMF.CurrentPageSize == 0 ? 10 : this.custPagerSMF.CurrentPageSize;
      int pintTotalRows;
      DataTable movementFactoryBy = new StockMovementQueriesBL().GetStockMovementFactoryBy(pintStockMovementId, pintWarehouseId, pintLocationId, pintMovementTypeId, pintProductId, pstrBeginDate, pstrEndDate, pintStartRowIndex, pintMaxRows, out pintTotalRows);
      if (movementFactoryBy == null || movementFactoryBy.Rows.Count == 0)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
        this.HidePopup();
      }
      else
        this.lblMessage.Visible = false;
      int num = pintTotalRows;
      this.wdgStockMovementProduct.DataSource = (object) movementFactoryBy;
      this.wdgStockMovementProduct.DataBind();
      this.custPagerSMF.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
      this.custPagerSMF.TotalRecordCount = pintTotalRows;
      if (!pboolLoadPager)
        return;
      this.custPagerSMF.LoadPager();
    }

    private void ShowStockMovementInfo(StockMovement pobjStockMovement)
    {
      this.txtSupplier.Text = this.Page.Server.HtmlDecode(pobjStockMovement.v_Name);
      this.txtDocumentType.Text = pobjStockMovement.v_DocumentNumber;
      this.wddWarehouse.SelectedValue = pobjStockMovement.i_WarehouseId.ToString();
      this.wddWarehouse_SelectionChanged((object) null, (EventArgs) null);
      this.wddFlow.SelectedValue = pobjStockMovement.i_FlowId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      this.wddFlow_SelectionChanged((object) null, (EventArgs) null);
      this.wddMotiveMovement.SelectedValue = pobjStockMovement.i_MotiveMovementId.ToString();
      this.wddMotiveMovement_SelectionChanged((object) null, (EventArgs) null);
      this.wddDocumentType.SelectedValue = pobjStockMovement.i_DocumentTypeId.ToString();
      this.hfStockMovementId.Value = pobjStockMovement.i_StockMovementId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    private StockMovement GetCurrentStockMovement(enmTypeLoadData penuTypeLoadData)
    {
      SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
      StockMovement currentStockMovement = new StockMovement();
      int int32 = Convert.ToInt32(this.ViewState["indexWdgStockMovementProduct"]);
      switch (penuTypeLoadData)
      {
        case enmTypeLoadData.SelectedRowsGrid:
          GridViewRow row = this.wdgStockMovementProduct.Rows[int32];
          currentStockMovement.i_StockMovementId = int.Parse(row.Cells[0].Text);
          currentStockMovement.v_Name = row.Cells[6].Text;
          currentStockMovement.i_WarehouseId = new int?(int.Parse(this.wdgStockMovementProduct.DataKeys[int32]["i_WarehouseId"].ToString()));
          currentStockMovement.i_FlowId = int.Parse(this.wdgStockMovementProduct.DataKeys[int32]["i_FlowId"].ToString());
          currentStockMovement.i_MotiveMovementId = new int?(int.Parse(this.wdgStockMovementProduct.DataKeys[int32]["i_MotiveMovementId"].ToString()));
          currentStockMovement.i_DocumentTypeId = this.wdgStockMovementProduct.DataKeys[int32]["i_DocumentTypeId"].ToString() != null ? new int?(Convert.ToInt32(this.wdgStockMovementProduct.DataKeys[int32]["i_DocumentTypeId"].ToString())) : new int?(-1);
          currentStockMovement.v_DocumentNumber = row.Cells[8].Text;
          this.hfSupplierId.Value = this.wdgStockMovementProduct.DataKeys[int32]["i_SupplierId"].ToString();
          break;
        case enmTypeLoadData.SelectedTextbox:
          if (!string.IsNullOrWhiteSpace(this.hfStockMovementId.Value))
            currentStockMovement.i_StockMovementId = Convert.ToInt32(this.hfStockMovementId.Value, (IFormatProvider) CultureInfo.CurrentCulture);
          currentStockMovement.i_WarehouseId = new int?(Convert.ToInt32(this.wddWarehouse.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture));
          currentStockMovement.i_MotiveMovementId = new int?(Convert.ToInt32(this.wddMotiveMovement.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture));
          currentStockMovement.i_SupplierId = this.hfSupplierId.Value == string.Empty ? new int?() : new int?(Convert.ToInt32(this.hfSupplierId.Value));
          currentStockMovement.i_DocumentTypeId = new int?(Convert.ToInt32(this.wddDocumentType.SelectedItem.Value));
          currentStockMovement.v_DocumentNumber = this.txtDocumentType.Text;
          currentStockMovement.i_UserId = new int?(systemUser.i_SystemUserId);
          currentStockMovement.b_Checked = new bool?(true);
          currentStockMovement.v_Observation = this.txtObservation.Text;
          currentStockMovement.d_InsertDate = new DateTime?(Convert.ToDateTime((object) this.wdpDate.Value, (IFormatProvider) CultureInfo.CurrentCulture));
          currentStockMovement.i_ProductionOrderId = new int?();
          currentStockMovement.i_ShelfOnDemandId = -1;
          break;
      }
      return currentStockMovement;
    }

    private void ButtonActive()
    {
      this.wibAgregar.Enabled = this.wddWarehouse.SelectedValue != "0" && this.wddFlow.SelectedValue != "0" && this.wddMotiveMovement.SelectedValue != "0";
    }

    private void LoadMotiveMovement(int pinWarehouseId, int pintFlowId)
    {
      DataTable dataTable = this.LoadMotiveMovementFactory(pinWarehouseId, pintFlowId == 0 ? -1 : pintFlowId);
      this.Session["btMotiveMovementAAP"] = (object) dataTable;
      DataRow row = dataTable.NewRow();
      row["i_MotiveMovementId"] = (object) 0;
      row["v_Description"] = (object) "- Seleccione -";
      row["i_ShowColumns"] = (object) 0;
      dataTable.Rows.InsertAt(row, 0);
      this.wddMotiveMovement.DataTextField = "v_Description";
      this.wddMotiveMovement.DataValueField = "i_MotiveMovementId";
      this.wddMotiveMovement.DataSource = (object) dataTable;
      this.wddMotiveMovement.DataBind();
      this.wddMotiveMovement.SelectedValue = "0";
    }

    private void LoadGridDetailProduct()
    {
      if (this.Session["sedtProductAdd"] == null)
        return;
      DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        double num1 = row["f_AdditionalAmount"].ToString() != "" ? Convert.ToDouble(row["f_AdditionalAmount"]) : 0.0;
        double num2;
        if (Convert.ToInt32(this.wddCurrency.SelectedValue) > 1)
        {
          num2 = row["f_SupplierPrice"].ToString() != "" ? Convert.ToDouble(row["f_SupplierPrice"]) * Convert.ToDouble(this.txtExchangeRate.Text) : 0.0;
          row["f_LocalSupplierPrice"] = (object) num2;
          row["f_ExchangeRate"] = (object) this.txtExchangeRate.Text;
        }
        else
        {
          num2 = row["f_SupplierPrice"].ToString() != "" ? Convert.ToDouble(row["f_SupplierPrice"]) : 0.0;
          row["f_LocalSupplierPrice"] = (object) num2;
          row["f_ExchangeRate"] = (object) 0.0;
        }
        double num3 = num2 + num1;
        row["f_LocalFinalPrice"] = (object) num3;
        row["f_SubTotal"] = (object) (Convert.ToDouble(row["f_LocalFinalPrice"]) * Convert.ToDouble(row["i_Quantity"]));
        row["i_MovementCurrencyId"] = (object) Convert.ToInt32(this.wddCurrency.SelectedValue);
      }
      string selectedValue = this.wddFlow.SelectedValue;
      this.wdgProductDetail.DataSource = (object) dataTable;
      this.wdgProductDetail.DataBind();
      this.wibSave.Enabled = this.wdgProductDetail.Rows.Count > 0;
    }

    private void DeleteRowCesta(int pintProductId)
    {
      if (this.Session["sedtProductAdd"] is DataTable dataTable)
      {
        foreach (DataRow row in new ArrayList((ICollection) dataTable.Rows))
        {
          if (Convert.ToInt32(row["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture) == pintProductId)
            dataTable.Rows.Remove(row);
        }
      }
      this.Session["sedtProductAdd"] = (object) dataTable;
      this.wdgProductDetail.DataSource = (object) dataTable;
      this.wdgProductDetail.DataBind();
    }

    private void SearchProduct()
    {
      if (this.Session["sedtProductAdd"] == null)
        return;
      this.wdgProductDetail.DataSource = (object) (this.Session["sedtProductAdd"] as DataTable);
      this.wdgProductDetail.DataBind();
    }

    private bool IsValidInputData()
    {
      string empty = string.Empty;
      bool boolean = Convert.ToBoolean(this.ViewState["vsblnIsIn"], (IFormatProvider) CultureInfo.CurrentCulture);
      SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
      int iLocationId = systemUser.i_LocationId;
      int num = systemUser.i_CompanyId.Value;
      Convert.ToInt32(this.wddFlow.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      if (boolean)
      {
        if (this.wdgProductDetail.Rows.Count == -1)
          empty += "<br>  •   &nbsp;&nbsp;&nbsp;Se debe agregar un producto para poder grabar";
      }
      else
      {
        this.txtSupplier.BackColor = Color.White;
        this.wddDocumentType.BackColor = Color.White;
        if (this.wdgProductDetail.Rows.Count == -1)
          empty += "<br>  •   &nbsp;&nbsp;&nbsp;Se debe agregar un Producto para poder grabar";
      }
      if (this.wddWarehouse.SelectedValue == "0")
      {
        this.wddWarehouse.BackColor = Color.FromArgb(236, 213, 213);
        empty += "<br>  •   &nbsp;&nbsp;&nbsp;Se debe seleccionar un Almacén";
      }
      else
        this.wddWarehouse.BackColor = Color.White;
      if (this.wddFlow.SelectedValue == "0")
      {
        this.wddFlow.BackColor = Color.FromArgb(236, 213, 213);
        empty += "<br>  •   &nbsp;&nbsp;&nbsp;Se debe seleccionar una Clase de Flujo";
      }
      else
        this.wddFlow.BackColor = Color.White;
      if (this.wddMotiveMovement.SelectedValue == "0")
      {
        this.wddMotiveMovement.BackColor = Color.FromArgb(236, 213, 213);
        empty += "<br>  •   &nbsp;&nbsp;&nbsp;Se debe seleccionar un Motivo de Movimiento";
      }
      else
        this.wddMotiveMovement.BackColor = Color.White;
      if (empty != string.Empty)
      {
        this.lblMessage.Text = empty;
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, empty);
        return false;
      }
      this.lblMessage.Visible = false;
      return true;
    }

    private void ClearCesta()
    {
      DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
      if (this.Session["sedtProductAdd"] != null)
      {
        dataTable.Rows.Clear();
        this.Session["sedtProductAdd"] = (object) dataTable;
      }
      this.wdgProductDetail.DataSource = (object) dataTable;
      this.wdgProductDetail.DataBind();
    }

    private void Load()
    {
      string str = this.H1.Value;
      this.ClearControls();
      this.LoadMotiveMovement(Convert.ToInt32(this.wddWarehouse.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.wddFlow.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
      this.currentOperation = MaintenanceOperation.AddNew;
      this.ViewState["currentOperation"] = (object) this.currentOperation;
      this.EnabledControls(MaintenanceOperation.AddNew);
      str = "0";
      if (this.Session["sedtProductAdd"] == null)
        this.BuildDTStockMovementDetail();
      this.Session["RotatingPlates"] = (object) null;
      this.txtSupplier.Focus();
      this.HidePopup();
    }

    private void ClearControls()
    {
      this.wddWarehouse.SelectedValue = "0";
      this.wddFlow.SelectedValue = "0";
      this.wddMotiveMovement.SelectedValue = "0";
      this.wddDocumentType.SelectedValue = "0";
      this.txtObservation.Text = "";
      this.txtSupplier.Text = "";
      this.txtDocumentType.Text = "";
    }

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format("OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private string CreatePopUp(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void wdgStockMovementProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgStockMovementProduct_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void wdgProductDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void wdgProductDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }
  }
}
