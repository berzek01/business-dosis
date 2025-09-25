// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.StockMovementAAPList
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class StockMovementAAPList : Page
  {
    private MaintenanceOperation currentOperation = MaintenanceOperation.None;
    protected HiddenField H1;
    protected UpdatePanel UpdatePanel1;
    protected DropDownList wddWarehouseList;
    protected Fecha wdpDateIni;
    protected Fecha wdpDateFin;
    protected DropDownList wddMotiveMovementList;
    protected Button wibSearch;
    protected Button btnExport;
    protected Button btnExportExcel;
    protected GridView wdgStockMovementProduct;
    protected Pager custPagerBatch;
    protected Label lblRecordCount;
    protected Button btnMostrarKardex;
    protected Button wibNew;
    protected Label lblMessage;
    protected UpdatePanel UpdatePanel2;
    protected DropDownList wddWarehouse;
    protected Fecha wdpDate;
    protected DropDownList wddFlow;
    protected DropDownList wddMotiveMovement;
    protected TextBox txtObservation;
    protected Panel Panel1;
    protected DropDownList wddLocation;
    protected DropDownList wddTargetWarehouse;
    protected Button wibAgregar;
    protected GridView wdgProductDetail;
    protected Button wibSave;
    protected Button wibCancel;
    protected HiddenField hfStockMovementId;
    protected Button btnJavaScriptResponse;
    protected Button btnAceptChangeQuantity;
    protected Button wibAccept;
    protected Label lblMessage1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.LoadWarehouse();
        this.LoadMotiveMovementList(Convert.ToInt32(this.wddWarehouseList.SelectedValue));
        this.SetDatePicker();
        this.BuildDTStockMovementDetail();
        this.BuilProductList();
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
        this.SearchMovement();
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

    protected void btnExport_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable datos = (DataTable) this.Session["dtExport"];
        this.Session["ExportType"].ToString();
        string str1 = "";
        DateTime now = DateTime.Now;
        string pstrFileTarget = str1 + now.Day.ToString((IFormatProvider) CultureInfo.CurrentCulture) + now.Month.ToString((IFormatProvider) CultureInfo.CurrentCulture) + now.Year.ToString((IFormatProvider) CultureInfo.CurrentCulture) + now.Hour.ToString((IFormatProvider) CultureInfo.CurrentCulture) + now.Minute.ToString((IFormatProvider) CultureInfo.CurrentCulture) + now.Second.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ".xls";
        ArrayList titulos = new ArrayList();
        DataTable dataTable = new DataTable();
        string str2 = this.Server.MapPath("../") + pstrFileTarget;
        OtherFormats otherFormats = new OtherFormats(str2);
        string str3 = "Detalle";
        for (int index = 0; index < this.wdgStockMovementProduct.Columns.Count; ++index)
        {
          if (this.wdgStockMovementProduct.Columns[index].HeaderText != str3 && this.wdgStockMovementProduct.Columns[index].Visible)
            titulos.Add((object) this.wdgStockMovementProduct.Columns[index].HeaderText);
        }
        otherFormats.ExportClaimBook("Listado de Movimientos - del " + this.wdpDateIni.Text + " al " + this.wdpDateFin.Text, titulos, datos);
        new ExportFile().Download(str2, pstrFileTarget);
        if (!File.Exists(str2))
          return;
        File.Delete(str2);
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

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
      try
      {
        this.ExportData("Excel");
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

    protected void wddWarehouseList_SelectionChanged(object sender, EventArgs e)
    {
      try
      {
        this.LoadMotiveMovementList(Convert.ToInt32(this.wddWarehouseList.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
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
        this.ClearControls();
        this.LoadMotiveMovement(Convert.ToInt32(this.wddWarehouse.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.wddFlow.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
        this.currentOperation = MaintenanceOperation.AddNew;
        this.ViewState["currentOperation"] = (object) this.currentOperation;
        this.EnabledControls(MaintenanceOperation.AddNew);
        if (this.Session["sedtProductAdd"] == null)
          this.BuildDTStockMovementDetail();
        this.Session["RotatingPlates"] = (object) null;
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

    protected void wdgStockMovementProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        int int32 = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = this.wdgStockMovementProduct.Rows[int32];
        this.ViewState["wdgStockMovementProductIndex"] = (object) int32;
        if (e.CommandName == "Edit")
        {
          SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
          int iLocationId = systemUser.i_LocationId;
          int num = systemUser.i_CompanyId.Value;
          this.currentOperation = MaintenanceOperation.Edit;
          this.ShowStockMovementInfo(this.GetCurrentStockMovement(enmTypeLoadData.SelectedRowsGrid));
          this.EnabledControls(MaintenanceOperation.Edit);
          DataTable movementDetailfBy = new StockMovementQueriesBL().GetStockMovementDetailfBy(0, int.Parse(row.Cells[2].Text, (IFormatProvider) CultureInfo.CurrentCulture));
          if (movementDetailfBy.Rows.Count == 0)
            return;
          this.wdgProductDetail.DataSource = (object) movementDetailfBy;
          this.wdgProductDetail.DataBind();
          DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
          this.Session["sedtProductAdd"] = (object) movementDetailfBy;
          this.wddWarehouse.Enabled = false;
          this.wdpDate.Enabled = false;
          this.wddFlow.Enabled = false;
          this.wddMotiveMovement.Enabled = false;
          this.txtObservation.Enabled = false;
          this.wibAgregar.Enabled = false;
          this.wibSave.Enabled = false;
        }
        else if (e.CommandName == "Delete")
        {
          this.currentOperation = MaintenanceOperation.Delete;
          this.ShowStockMovementInfo(this.GetCurrentStockMovement(enmTypeLoadData.SelectedRowsGrid));
          this.EnabledControls(MaintenanceOperation.Delete);
          this.wibSave.Enabled = true;
        }
        this.ViewState["currentOperation"] = (object) this.currentOperation;
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

    protected void wdgStockMovementProduct_PageIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.SearchMovement();
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

    protected void wddWarehouse_SelectionChanged(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - StockMovementAAPList.aspx");
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
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void wddFlow_SelectionChanged(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - StockMovementAAPList.aspx");
        int iLocationId = systemUser.i_LocationId;
        int num = systemUser.i_CompanyId.Value;
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

    protected void wddMotiveMovement_SelectionChanged(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - StockMovementAPPList.aspx");
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
        this.Panel1.Visible = Convert.ToInt32(this.Session["ShowColumns"], (IFormatProvider) CultureInfo.CurrentCulture) != 0;
        if (this.Panel1.Visible)
        {
          this.LoadLocation();
          this.wddLocation.SelectedValue = systemUser.i_LocationId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
          this.wddLocation.Enabled = Convert.ToInt32(this.Session["ShowColumns"], (IFormatProvider) CultureInfo.CurrentCulture) != 1;
          this.LoadTargetWarehouse((int) Convert.ToInt16(this.wddLocation.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
        }
        this.Session["seFlowId"] = (object) this.wddFlow.SelectedValue;
        this.wibAgregar.OnClientClick = Convert.ToInt32(this.Session["seFlowId"], (IFormatProvider) CultureInfo.CurrentCulture) != 1 ? this.CreatePopUp("Lista Productos", "../Searchs/ProductSearchAAP.aspx", "800px", "700px") : this.CreatePopUp("Lista Transferencias Pendientes", "../Searchs/WarehouseTransferSearch.aspx", "1050px", "530px");
        this.wibSave.Enabled = this.wdgProductDetail.Rows.Count > 0;
        this.ButtonActive();
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

    private void ButtonActive()
    {
      this.wibAgregar.Enabled = this.wddWarehouse.SelectedValue != "0" && this.wddFlow.SelectedValue != "0" && this.wddMotiveMovement.SelectedValue != "0";
    }

    protected void wddLocation_SelectionChanged(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - StockMovementAAPList.aspx");
        int iLocationId = systemUser.i_LocationId;
        int num = systemUser.i_CompanyId.Value;
        this.LoadTargetWarehouse((int) Convert.ToInt16(this.wddLocation.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
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

    protected void wdgProductDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        int int32 = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = this.wdgProductDetail.Rows[int32];
        this.ViewState["wdgProductDetailIndex"] = (object) int32;
        if (e.CommandName == "Delete")
          this.DeleteRowCesta(Convert.ToInt32(this.wdgProductDetail.DataKeys[int32]["i_StockMovementDetailId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
        this.wibSave.Enabled = this.wdgProductDetail.Rows.Count > 0;
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

    protected void wdgProductDetail_PageIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.SearchProduct();
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
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
        {
          Timeout = new TimeSpan(1, 1, 1)
        }))
        {
          bool flag = false;
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
              new StockMovementManagementBL().StockMovementInsertTransfer(this.GetCurrentStockMovement(enmTypeLoadData.SelectedTextbox), pdtStockMovementDetail);
              Message.SetMessage(this.lblMessage1, new HandledException(2, "El Movimiento se creó satisfactoriamente"));
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
              new StockMovementManagementBL().StockMovementUpdate(this.GetCurrentStockMovement(enmTypeLoadData.SelectedTextbox), pdtStockMovementDetail);
              Message.SetMessage(this.lblMessage, new HandledException(2, "El Movimiento se modificó satisfactoriamente"));
              this.ClearCesta();
              this.EnabledControls(MaintenanceOperation.Edit);
              break;
            case MaintenanceOperation.Delete:
              new StockMovementManagementBL().StockMovementDelete(int.Parse(this.wdgStockMovementProduct.Rows[Convert.ToInt32(this.ViewState["wdgStockMovementProductIndex"])].Cells[2].Text));
              Message.SetMessage(this.lblMessage, new HandledException(2, "El Movimiento se anuló satisfactoriamente."));
              this.ClearCesta();
              this.EnabledControls(MaintenanceOperation.Delete);
              break;
          }
          if (flag)
            return;
          this.Session["sedtProductAdd"] = (object) null;
          this.lblMessage.Visible = true;
          this.txtObservation.Enabled = false;
          this.wibSave.Visible = false;
          this.wibCancel.Visible = false;
          this.wibAccept.Visible = true;
          this.wddWarehouse.Enabled = false;
          this.wddFlow.Enabled = false;
          this.wddMotiveMovement.Enabled = false;
          this.wdpDate.Enabled = false;
          transactionScope.Complete();
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

    protected void wibAccept_Click(object sender, EventArgs e)
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

    protected void btnJavaScriptResponse_Click(object sender, EventArgs e)
    {
      try
      {
        this.LoadGridDetailProduct();
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

    protected void btnAceptChangeQuantity_Click(object sender, EventArgs e)
    {
      try
      {
        int int32_1 = Convert.ToInt32(this.ViewState["wdgProductDetailIndex"]);
        GridViewRow row1 = this.wdgProductDetail.Rows[int32_1];
        int int32_2 = Convert.ToInt32(this.wdgProductDetail.DataKeys[int32_1]["i_Item"].ToString());
        int int32_3 = Convert.ToInt32(row1.Cells[1].Text, (IFormatProvider) CultureInfo.CurrentCulture);
        if (this.Session["sedtProductAdd"] == null)
          return;
        DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
        foreach (DataRow row2 in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (Convert.ToInt32(row2["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture) == int32_3)
          {
            row2.BeginEdit();
            row2["i_Quantity"] = (object) int32_2;
            row2["f_SubTotal"] = (object) ((double) int32_2 * (Convert.ToDouble(row2["f_SupplierPrice"], (IFormatProvider) CultureInfo.CurrentCulture) + Convert.ToDouble(row2["f_AdditionalAmount"], (IFormatProvider) CultureInfo.CurrentCulture)));
          }
        }
        this.Session["sedtProductAdd"] = (object) dataTable;
        this.wdgProductDetail.DataSource = (object) dataTable;
        this.wdgProductDetail.DataBind();
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

    protected void wibCancel_Click(object sender, EventArgs e)
    {
      try
      {
        this.EnabledControls(MaintenanceOperation.None);
        if (this.Session["sedtProductAdd"] == null)
          return;
        DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
        dataTable.Rows.Clear();
        this.wdgProductDetail.DataSource = (object) dataTable;
        this.wdgProductDetail.DataBind();
        this.Session["sedtProductAdd"] = (object) null;
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

    protected void btnMostrarKardex_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("~/Warehouse/Reports/WarehouseKardex.aspx");
    }

    protected void wddWarehouse2_SelectionChanged(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - StockMovementAAPList.aspx");
        int iLocationId = systemUser.i_LocationId;
        int pintCompanyId = systemUser.i_CompanyId.Value;
        this.Session["sWarehouseId2"] = (object) this.wddWarehouse.SelectedValue;
        if (this.Session["sedtProductAdd"] != null)
        {
          DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
          dataTable.Rows.Clear();
          this.Session["sedtProductAdd"] = (object) dataTable;
          this.wdgProductDetail.DataSource = (object) dataTable;
          this.wdgProductDetail.DataBind();
        }
        this.wibSave.Enabled = this.wdgProductDetail.Rows.Count > 0;
        this.ButtonActive(pintCompanyId);
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

    private void ButtonActive(int pintCompanyId)
    {
      try
      {
        switch (pintCompanyId)
        {
          case 1:
            this.wibAgregar.Enabled = this.wddFlow.SelectedValue != "0" && this.wddWarehouse.SelectedValue != "0" && this.wddMotiveMovement.SelectedValue != "0";
            break;
          case 2:
            this.wibAgregar.Enabled = this.wddFlow.SelectedValue != "0" && this.wddWarehouse.SelectedValue != "0" && this.wddMotiveMovement.SelectedValue != "0";
            break;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void custPagerBatch_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - StockMovementAAPList.aspx");
        int int32_1 = Convert.ToInt32(this.wddMotiveMovementList.SelectedValue);
        int int32_2 = Convert.ToInt32(this.wddWarehouseList.SelectedValue);
        int iLocationId = systemUser.i_LocationId;
        int num = systemUser.i_CompanyId.Value;
        string pstrBeginDate = Convert.ToDateTime(this.wdpDateIni.Value).ToString("yyyyMMdd");
        string pstrEndDate = Convert.ToDateTime(this.wdpDateFin.Value).ToString("yyyyMMdd");
        this.SearchStockMovementList(0, int32_2, iLocationId, int32_1, pstrBeginDate, pstrEndDate, false);
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

    private void LoadWarehouse()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - StockMovementAAPList.aspx");
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
        this.wdpDate.Value = DateTime.Now;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void EnabledControls(MaintenanceOperation penuCurrentOperation)
    {
      try
      {
        string str = this.H1.Value;
        if (this.Session["SystemUser"] == null)
          throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - StockMovementAAPList.aspx");
        int num = (this.Session["SystemUser"] as SystemUser).i_CompanyId.Value;
        this.wddMotiveMovement.BackColor = Color.White;
        switch (penuCurrentOperation)
        {
          case MaintenanceOperation.AddNew:
            this.txtObservation.Enabled = true;
            this.wddFlow.Enabled = true;
            this.wddWarehouse.Enabled = true;
            this.wddMotiveMovement.Enabled = true;
            this.Panel1.Visible = false;
            this.wibAccept.Visible = false;
            this.wibAgregar.Enabled = false;
            this.wibSave.Enabled = false;
            this.SetDatePicker();
            this.wdpDate.Enabled = true;
            this.wdgProductDetail.Columns[0].Visible = true;
            this.wdgProductDetail.Enabled = true;
            if (str == "0")
            {
              this.wibSave.Visible = true;
              this.wibCancel.Visible = true;
              this.wibAccept.Visible = false;
              this.lblMessage1.Visible = false;
              string script1 = UtilDA.ActiveTabIndex("tabs", 1, "0");
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script1, true);
              string script2 = "TabIndex();";
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script2, true);
              break;
            }
            this.wibSave.Visible = false;
            this.wibCancel.Visible = false;
            this.wibAccept.Visible = false;
            break;
          case MaintenanceOperation.Edit:
            this.txtObservation.Enabled = true;
            this.wddFlow.Enabled = true;
            this.wddWarehouse.Enabled = true;
            this.wddMotiveMovement.Enabled = true;
            this.wdgProductDetail.Columns[0].Visible = false;
            if (str == "0")
            {
              this.wibSave.Visible = true;
              this.wibCancel.Visible = true;
              this.wibAccept.Visible = false;
              this.lblMessage1.Visible = false;
              string script3 = UtilDA.ActiveTabIndex("tabs", 1, "0");
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script3, true);
              string script4 = "TabIndex();";
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script4, true);
              break;
            }
            this.wibSave.Visible = false;
            this.wibCancel.Visible = false;
            this.wibAccept.Visible = false;
            break;
          case MaintenanceOperation.Delete:
            this.txtObservation.Enabled = false;
            this.wddFlow.Enabled = false;
            this.wddWarehouse.Enabled = false;
            this.wddMotiveMovement.Enabled = false;
            if (str == "0")
            {
              string script5 = UtilDA.ActiveTabIndex("tabs", 1, "0");
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script5, true);
              string script6 = "TabIndex();";
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script6, true);
              break;
            }
            string script7 = UtilDA.ActiveTabIndex("tabs", 0, "1");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script7, true);
            string script8 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script8, true);
            break;
          default:
            string script9 = UtilDA.ActiveTabIndex("tabs", 0, "1");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script9, true);
            string script10 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script10, true);
            break;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void BuildDTStockMovementDetail()
    {
      try
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
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void BuilProductList()
    {
      try
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
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchMovement()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - StockMovementAAPList.aspx");
        int int32_1 = Convert.ToInt32(this.wddMotiveMovementList.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        int int32_2 = Convert.ToInt32(this.wddWarehouseList.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        int iLocationId = systemUser.i_LocationId;
        int num = systemUser.i_CompanyId.Value;
        string pstrBeginDate = Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture).ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrEndDate = Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture).ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture);
        this.SearchStockMovementList(0, int32_2, iLocationId, int32_1, pstrBeginDate, pstrEndDate, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchStockMovementList(
      int pintStockMovementId,
      int pintWarehouseId,
      int pintLocationId,
      int pintMovementTypeId,
      string pstrBeginDate,
      string pstrEndDate,
      bool pboolLoadPager)
    {
      try
      {
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerBatch.CurrentPageNumber;
        int pintMaxRows = this.custPagerBatch.CurrentPageSize == 0 ? 10 : this.custPagerBatch.CurrentPageSize;
        int pintTotalRows;
        DataTable stockMovementAapBy = new StockMovementQueriesBL().GetStockMovementAAPBy(pintStockMovementId, pintWarehouseId, pintLocationId, pintMovementTypeId, pstrBeginDate, pstrEndDate, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        int num = pintTotalRows;
        if (stockMovementAapBy == null || stockMovementAapBy.Rows.Count == 0)
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
        this.wdgStockMovementProduct.DataSource = (object) stockMovementAapBy;
        this.wdgStockMovementProduct.DataBind();
        this.custPagerBatch.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
        this.custPagerBatch.TotalRecordCount = pintTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerBatch.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ClearControls()
    {
      try
      {
        this.wddWarehouse.SelectedValue = "0";
        this.wddFlow.SelectedValue = "0";
        this.wddMotiveMovement.SelectedValue = "0";
        this.txtObservation.Text = "";
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadMotiveMovementList(int pinWarehouseId)
    {
      try
      {
        DataTable dataTable = this.LoadMotiveMovementAAP(pinWarehouseId, 0);
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
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private DataTable LoadMotiveMovementAAP(int pinWarehouseId, int pintFlowId)
    {
      try
      {
        return new MotiveMovementQueriesBL().GetMotiveMovementAAP(pinWarehouseId, pintFlowId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadMotiveMovement(int pinWarehouseId, int pintFlowId)
    {
      try
      {
        DataTable dataTable = this.LoadMotiveMovementAAP(pinWarehouseId, pintFlowId == 0 ? -1 : pintFlowId);
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
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadLocation()
    {
      try
      {
        DataTable locationBy = new LocationQueriesBL().GetLocationBy("", string.Empty, string.Empty);
        DataRow row = locationBy.NewRow();
        row["i_LocationId"] = (object) 0;
        row["i_CompanyId"] = (object) 0;
        row["i_LocationTypeId"] = (object) 1;
        row["i_LocationRoleId"] = (object) 1;
        row["v_Description"] = (object) "- Seleccione -";
        row["v_Address"] = (object) string.Empty;
        row["v_AttentionSchedule"] = (object) string.Empty;
        row["i_Status"] = (object) 1;
        locationBy.Rows.InsertAt(row, 0);
        this.wddLocation.DataSource = (object) locationBy;
        this.wddLocation.DataTextField = "v_Description";
        this.wddLocation.DataValueField = "i_LocationId";
        this.wddLocation.DataBind();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadTargetWarehouse(int pintLocationId)
    {
      try
      {
        DataTable masterByLocationId = new WarehouseQueriesBL().GetWarehouseMasterByLocationId(pintLocationId);
        DataRow row = masterByLocationId.NewRow();
        row["i_WarehouseId"] = (object) 0;
        row["v_Description"] = (object) "- Seleccione -";
        masterByLocationId.Rows.InsertAt(row, 0);
        this.wddTargetWarehouse.DataSource = (object) masterByLocationId;
        this.wddTargetWarehouse.DataTextField = "v_Description";
        this.wddTargetWarehouse.DataValueField = "i_WarehouseId";
        this.wddTargetWarehouse.DataBind();
        this.wddTargetWarehouse.SelectedIndex = -1;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void DeleteRowCesta(int pintProductId)
    {
      try
      {
        if (this.Session["sedtProductAdd"] is DataTable dataTable)
        {
          foreach (DataRow row in new ArrayList((ICollection) dataTable.Rows))
          {
            if (Convert.ToInt32(row["i_StockMovementDetailId"], (IFormatProvider) CultureInfo.CurrentCulture) == pintProductId)
              dataTable.Rows.Remove(row);
          }
        }
        this.Session["sedtProductAdd"] = (object) dataTable;
        this.wdgProductDetail.DataSource = (object) dataTable;
        this.wdgProductDetail.DataBind();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchProduct()
    {
      try
      {
        if (this.Session["sedtProductAdd"] == null)
          return;
        this.wdgProductDetail.DataSource = (object) (this.Session["sedtProductAdd"] as DataTable);
        this.wdgProductDetail.DataBind();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private bool IsValidInputData()
    {
      string empty = string.Empty;
      Convert.ToBoolean(this.ViewState["vsblnIsIn"], (IFormatProvider) CultureInfo.CurrentCulture);
      SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
      int iLocationId = systemUser.i_LocationId;
      int num = systemUser.i_CompanyId.Value;
      Convert.ToInt32(this.wddFlow.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      this.wddLocation.BackColor = Color.White;
      this.wddTargetWarehouse.BackColor = Color.White;
      if (this.wdgProductDetail.Rows.Count == -1)
        empty += "<br>  •   &nbsp;&nbsp;&nbsp;Debe agregar un Producto para poder grabar";
      if (this.wddLocation.SelectedValue == "0")
      {
        this.wddLocation.BackColor = Color.FromArgb(236, 213, 213);
        empty += "<br>  •   &nbsp;&nbsp;&nbsp;Debe ingresar un Punto de Entrega Destino";
      }
      else
        this.wddLocation.BackColor = Color.White;
      if (this.wddFlow.SelectedValue == "2")
      {
        if (this.wddTargetWarehouse.SelectedValue == "0")
        {
          this.wddTargetWarehouse.BackColor = Color.FromArgb(236, 213, 213);
          empty += "<br>  •   &nbsp;&nbsp;&nbsp;Debe ingresar un Almacén Destino";
        }
        else
          this.wddTargetWarehouse.BackColor = Color.White;
      }
      if (this.wddWarehouse.SelectedValue == "0")
      {
        this.wddWarehouse.BackColor = Color.FromArgb(236, 213, 213);
        empty += "<br>  •   &nbsp;&nbsp;&nbsp;Debe elegir un Almacén";
      }
      else
        this.wddWarehouse.BackColor = Color.White;
      if (this.wddFlow.SelectedValue == "0")
      {
        this.wddFlow.BackColor = Color.FromArgb(236, 213, 213);
        empty += "<br>  •   &nbsp;&nbsp;&nbsp;Debe elegir una Clase de Flujo";
      }
      else
        this.wddFlow.BackColor = Color.White;
      if (this.wddMotiveMovement.SelectedValue == "0")
      {
        this.wddMotiveMovement.BackColor = Color.FromArgb(236, 213, 213);
        empty += "<br>  •   &nbsp;&nbsp;&nbsp;Debe elegir un Motivo de Movimiento";
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

    private StockMovement GetCurrentStockMovement(enmTypeLoadData penuTypeLoadData)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - StockMovementAAPList.aspx");
        StockMovement currentStockMovement = new StockMovement();
        switch (penuTypeLoadData)
        {
          case enmTypeLoadData.SelectedRowsGrid:
            GridViewRow row = this.wdgStockMovementProduct.Rows[Convert.ToInt32(this.ViewState["wdgStockMovementProductIndex"])];
            currentStockMovement.i_StockMovementId = int.Parse(row.Cells[2].Text);
            currentStockMovement.v_Name = row.Cells[4].Text;
            currentStockMovement.i_WarehouseId = new int?(int.Parse(this.wdgStockMovementProduct.DataKeys[row.RowIndex]["i_WarehouseId"].ToString()));
            currentStockMovement.i_FlowId = int.Parse(this.wdgStockMovementProduct.DataKeys[row.RowIndex]["i_Flow"].ToString());
            currentStockMovement.i_MotiveMovementId = new int?(int.Parse(this.wdgStockMovementProduct.DataKeys[row.RowIndex]["i_MotiveMovementId"].ToString()));
            break;
          case enmTypeLoadData.SelectedTextbox:
            if (!string.IsNullOrWhiteSpace(this.hfStockMovementId.Value))
              currentStockMovement.i_StockMovementId = Convert.ToInt32(this.hfStockMovementId.Value, (IFormatProvider) CultureInfo.CurrentCulture);
            currentStockMovement.i_WarehouseId = new int?(Convert.ToInt32(this.wddWarehouse.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture));
            currentStockMovement.i_MotiveMovementId = new int?(Convert.ToInt32(this.wddMotiveMovement.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture));
            currentStockMovement.i_UserId = new int?(systemUser.i_SystemUserId);
            currentStockMovement.b_Checked = new bool?(true);
            currentStockMovement.v_Observation = this.txtObservation.Text;
            currentStockMovement.d_InsertDate = new DateTime?(Convert.ToDateTime((object) this.wdpDate.Value, (IFormatProvider) CultureInfo.CurrentCulture));
            currentStockMovement.i_ProductionOrderId = new int?();
            currentStockMovement.i_ShelfOnDemandId = -1;
            if (this.Panel1.Visible)
            {
              currentStockMovement.i_TargetWarehouseId = Convert.ToInt32(this.wddTargetWarehouse.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture);
              break;
            }
            break;
        }
        return currentStockMovement;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ClearCesta()
    {
      try
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
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadGridDetailProduct()
    {
      try
      {
        if (this.Session["sedtProductAdd"] == null)
          return;
        DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
        string selectedValue = this.wddFlow.SelectedValue;
        this.wdgProductDetail.DataSource = (object) dataTable;
        this.wdgProductDetail.DataBind();
        this.wibSave.Enabled = this.wdgProductDetail.Rows.Count > 0;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ShowStockMovementInfo(StockMovement pobjStockMovement)
    {
      try
      {
        try
        {
          this.wddWarehouse.SelectedValue = pobjStockMovement.i_WarehouseId.ToString();
        }
        catch (Exception ex)
        {
          this.wddWarehouse.SelectedValue = "0";
        }
        this.wddWarehouse_SelectionChanged((object) null, (EventArgs) null);
        this.wddFlow.SelectedValue = pobjStockMovement.i_FlowId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.wddFlow_SelectionChanged((object) null, (EventArgs) null);
        try
        {
          this.wddMotiveMovement.SelectedValue = pobjStockMovement.i_MotiveMovementId.ToString();
        }
        catch (Exception ex)
        {
          this.wddMotiveMovement.SelectedValue = "0";
        }
        this.hfStockMovementId.Value = pobjStockMovement.i_StockMovementId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable SearchField(string strField, int intValueToSearch)
    {
      DataTable dataTable1 = this.ViewState["vsdtMovementType"] as DataTable;
      DataTable dataTable2 = new DataTable();
      DataTable dataTable3 = dataTable1.Clone();
      foreach (DataRow row in dataTable1.Select(strField + " =" + intValueToSearch.ToString()))
        dataTable3.ImportRow(row);
      return dataTable3;
    }

    protected void wddDocumentType_SelectionChanged(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - StockMovementAAPList.aspx");
        int iLocationId = systemUser.i_LocationId;
        int pintCompanyId = systemUser.i_CompanyId.Value;
        this.wibSave.Enabled = this.wdgProductDetail.Rows.Count > 0;
        this.ButtonActive(pintCompanyId);
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

    private void ExportData(string exporttype)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - StockMovementAAPList.aspx");
        int int32_1 = Convert.ToInt32(this.wddMotiveMovementList.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        int int32_2 = Convert.ToInt32(this.wddWarehouseList.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        int iLocationId = systemUser.i_LocationId;
        int num = systemUser.i_CompanyId.Value;
        string pstrBeginDate = Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture).ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrEndDate = Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture).ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture);
        this.Session["dtExport"] = (object) new StockMovementQueriesBL().GetStockMovementAAPByExport(0, int32_2, iLocationId, int32_1, pstrBeginDate, pstrEndDate);
        this.Session["ExportType"] = (object) exporttype;
        this.Export();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void Export()
    {
      string script = "Export();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private string CreatePopUp(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
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
