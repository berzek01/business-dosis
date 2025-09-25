// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Warehouse.WastageProductsInsert
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Warehouse
{
  public class WastageProductsInsert : Page
  {
    private WarehouseExhibitionQueriesBL objWarehouseExhibitionQueriesBL;
    private int i_PlateTypeId = 0;
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtPlate;
    protected Button wibSearch;
    protected Button wibExport;
    protected Button wibDesechar;
    protected GridView wdgList;
    protected GridView wdgListNew;
    protected Label lblCount;
    protected Label lblMessage;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      DataTable dtResult = new DataTable("Datos");
      this.TableColumns(dtResult);
      DataRow row = dtResult.NewRow();
      dtResult.Rows.Add(row);
      this.wdgList.DataSource = (object) dtResult;
      this.wdgList.DataBind();
      this.wdgList.Rows[0].Visible = false;
      this.ViewState["i_PlateTypeId"] = (object) this.Request.QueryString["t"].ToString();
      this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
    }

    private void TableColumns(DataTable dtResult)
    {
      dtResult.Columns.Add("i_ProductWarehouseId", typeof (int));
      dtResult.Columns.Add("b_Status", typeof (bool));
      dtResult.Columns.Add("i_ProductId", typeof (int));
      dtResult.Columns.Add("v_Plate", typeof (string));
      dtResult.Columns.Add("d_UpdateDate", typeof (DateTime));
      dtResult.Columns.Add("v_Description", typeof (string));
      dtResult.Columns.Add("TipoIngreso", typeof (string));
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchWastageProducts();

    protected void wibExport_Click(object sender, EventArgs e)
    {
      try
      {
        this.ExportList();
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

    protected void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable objDataTable = (DataTable) this.Session["dtExport"];
        ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
        List<ClassColumns> classColumnsList = new List<ClassColumns>();
        foreach (DataControlField column in (StateManagedCollection) this.wdgListNew.Columns)
        {
          if (column.Visible && column.GetType().Name == "BoundField")
          {
            BoundField boundField = (BoundField) column;
            classColumnsList.Add(new ClassColumns(boundField.DataField, 1, (int) column.ControlStyle.Width.Value, boundField.HeaderText));
          }
        }
        exportToExcelDataGrid.clsTitle = classColumnsList;
        exportToExcelDataGrid.AgregarHojaLibro(objDataTable, "Listado Placas Mermadas");
        exportToExcelDataGrid.CerrarLibro();
        byte[] buffer = exportToExcelDataGrid.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=ListadoPlacasMermadas.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
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

    protected void wibDesechar_Click(object sender, EventArgs e)
    {
      try
      {
        int num = 0;
        DataTable dtproducts = new DTStockMovementDetail().DataTableStockMovementDetail();
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        DataTable dataTable = new DataTable();
        int int32 = Convert.ToInt32(new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(this.i_PlateTypeId).Rows[0]["i_WarehouseId"].ToString());
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WastageProductsInsert.aspx");
        List<ProductWarehouse> pobjlstProductWarehouse = new List<ProductWarehouse>();
        foreach (GridViewRow row in this.wdgList.Rows)
        {
          CheckBox control = (CheckBox) row.FindControl("chkItem");
          if (control != null && control.Checked)
          {
            pobjlstProductWarehouse.Add(new ProductWarehouse()
            {
              i_ProductWarehouseId = Convert.ToInt32(this.wdgList.DataKeys[row.RowIndex]["i_ProductWarehouseId"]),
              i_WarehouseId = new int?(int32),
              i_ProductId = new int?(Convert.ToInt32(this.wdgList.DataKeys[row.RowIndex]["i_ProductId"]))
            });
            DataRowCollection rows = dtproducts.Rows;
            object[] objArray = new object[22];
            objArray[3] = (object) Convert.ToInt32(this.wdgList.DataKeys[row.RowIndex]["i_ProductId"]);
            objArray[6] = (object) 1;
            objArray[20] = (object) row.Cells[2].Text.ToString();
            rows.Add(objArray);
            num = 1;
          }
        }
        StockMovement currentStockMovement = this.GetCurrentStockMovement();
        bool flag = new WarehouseExhibitionManagementBL().ExhibitioneWastagePlatInsert(pobjlstProductWarehouse, currentStockMovement, dtproducts);
        this.SearchWastageProducts();
        if (!flag)
          return;
        if (num > 0)
          Message.SetMessage(this.lblMessage, new HandledException(2, "Se desecharon correctamente las placas al Almacén de Desechos"));
        else
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se ha seleccionado ninguna placa"));
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

    private void SearchWastageProducts()
    {
      try
      {
        this.objWarehouseExhibitionQueriesBL = new WarehouseExhibitionQueriesBL();
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        DataTable dataTable = new DataTable();
        int int32 = Convert.ToInt32(new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(this.i_PlateTypeId).Rows[0]["i_WarehouseId"].ToString());
        DataTable all = this.objWarehouseExhibitionQueriesBL.PlateMovementWastageGetAll(this.txtPlate.Text.Trim(), int32);
        if (all == null || all.Rows.Count == 0)
          this.lblCount.Text = Constants.SEARCHRESULT_Empty;
        else
          this.wibExport.Enabled = true;
        this.wdgList.AutoGenerateColumns = false;
        this.wdgList.DataSource = (object) all;
        this.wdgList.DataBind();
        this.lblCount.Text = Constants.SEARCHRESULT_OK.Replace("XX", all.Rows.Count.ToString());
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

    private void ExportList()
    {
      try
      {
        this.lblCount.Text = "";
        this.i_PlateTypeId = Convert.ToInt32(this.Request.QueryString["t"].ToString());
        int int32 = Convert.ToInt32(new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(this.i_PlateTypeId).Rows[0]["i_WarehouseId"].ToString());
        DataTable dataTable = new DataTable();
        this.Session["dtExport"] = (object) new WarehouseExhibitionQueriesBL().PlateMovementWastageGetAll(this.txtPlate.Text.Trim(), int32);
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

    private StockMovement GetCurrentStockMovement()
    {
      SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
      int num = 0;
      if (systemUser != null)
        num = systemUser.i_SystemUserId;
      this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
      DataTable dataTable = new DataTable();
      int int32 = Convert.ToInt32(new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(this.i_PlateTypeId).Rows[0]["i_WastageWarehouseId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
      return new StockMovement()
      {
        i_StockMovementId = 0,
        i_WarehouseId = new int?(int32),
        i_MotiveMovementId = new int?(64),
        i_SupplierId = new int?(),
        i_DocumentTypeId = new int?(),
        v_DocumentNumber = "",
        i_UserId = new int?(num),
        b_Checked = new bool?(true),
        v_Observation = (string) null,
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
  }
}
