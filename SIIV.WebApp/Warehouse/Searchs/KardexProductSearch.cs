// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Searchs.KardexProductSearch
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Searchs
{
  public class KardexProductSearch : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected RadioButtonList rblCriterioFiltro;
    protected TextBox txtFilter;
    protected Button btnBusqueda;
    protected GridView wdgProductList;
    protected Label lblRecordCount1;
    protected GridView wdgProductsAdd;
    protected Button btnProductAdd;
    protected Button btnCancel;
    protected Label lblRecordCount;
    protected Label lblMsgError;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.SearchProduct();
    }

    protected void wdgProductList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (!(e.CommandName == "Select"))
        return;
      this.AddNewProduct(this.wdgProductList.Rows[Convert.ToInt32(e.CommandArgument)]);
    }

    protected void wdgProductList_PageIndexChanged(object sender, EventArgs e)
    {
      this.SearchProduct();
    }

    protected void btnSearch_Click(object sender, EventArgs e) => this.SearchProduct();

    protected void btnProductAdd_Click(object sender, EventArgs e)
    {
      this.SendInfoProductPopupClose();
    }

    protected void btnCancel_Click(object sender, EventArgs e) => this.PopupClose();

    private void SendInfoProductPopupClose()
    {
      DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
      string script = "SendInfoProductPopup();";
      if (dataTable == null || dataTable.Rows.Count == 0)
        script = "PopupClose();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void PopupClose()
    {
      string script = "PopupClose();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void SearchProduct()
    {
      string text = this.txtFilter.Text;
      int iLocationId = (this.Session["SystemUser"] as SystemUser).i_LocationId;
      DataTable warehouseKardexBy = new ProductWarehouseQueriesBL().GetProductWarehouseKardexBy(Convert.ToInt32(this.Session["i_WarehouseId"], (IFormatProvider) CultureInfo.CurrentCulture), iLocationId, text);
      warehouseKardexBy.Columns.Add("v_IsAdded", typeof (string));
      int count;
      if (this.Session["seProductList"] != null)
      {
        if (this.Session["sedtProductAdd"] is DataTable dataTable)
        {
          foreach (DataRow row1 in (InternalDataCollectionBase) dataTable.Rows)
          {
            foreach (DataRow row2 in new ArrayList((ICollection) warehouseKardexBy.Rows))
            {
              if (Convert.ToInt32(row2["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(row1["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture))
                warehouseKardexBy.Rows.Remove(row2);
            }
          }
          this.wdgProductsAdd.DataSource = (object) dataTable;
          this.wdgProductsAdd.DataBind();
          this.Session["sedtProductAdd"] = (object) dataTable;
        }
        Label lblRecordCount = this.lblRecordCount;
        string str;
        if (dataTable == null)
        {
          str = "0 Registros Seleccionados";
        }
        else
        {
          count = dataTable.Rows.Count;
          str = count.ToString((IFormatProvider) CultureInfo.CurrentCulture) + " Registros Seleccionados";
        }
        lblRecordCount.Text = str;
      }
      this.wdgProductList.DataSource = (object) warehouseKardexBy;
      this.wdgProductList.DataBind();
      this.Session["seProductList"] = (object) warehouseKardexBy;
      Label lblRecordCount1 = this.lblRecordCount1;
      count = warehouseKardexBy.Rows.Count;
      string str1 = count.ToString() + " Registros Disponibles";
      lblRecordCount1.Text = str1;
    }

    private void AddNewProduct(GridViewRow pobjSelectedRowsGrid)
    {
      DataTable dataTable1 = this.Session["seProductList"] as DataTable;
      DataTable dataTable2 = this.Session["sedtProductAdd"] as DataTable;
      LinkButton control = pobjSelectedRowsGrid.FindControl("lnkselect") as LinkButton;
      bool flag = false;
      if (dataTable2 != null)
      {
        for (int index = 0; index < dataTable2.Rows.Count; ++index)
        {
          if (Convert.ToInt32(dataTable2.Rows[index]["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_ProductId"].ToString()))
          {
            if (control.Text == "Agregar")
              this.lblMsgError.Text = "El producto ya esta agregado. Elija otro";
            control.Text = "Agregar";
            control.ForeColor = Color.Blue;
            flag = true;
            if (Convert.ToInt32(dataTable2.Rows[index]["b_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == 0)
            {
              dataTable2.Rows[index]["b_Status"] = (object) 1;
              break;
            }
            break;
          }
        }
      }
      if (!flag)
      {
        if (dataTable2 == null)
          dataTable2 = new DataTable()
          {
            Columns = {
              "i_ProductId",
              "v_Name",
              "b_Status",
              "v_IsAdded"
            }
          }.Clone();
        dataTable2.Rows.Add((object) this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_ProductId"].ToString(), (object) pobjSelectedRowsGrid.Cells[1].Text, (object) 1);
        control.Text = "Quitar";
        control.ForeColor = Color.Red;
      }
      int index1 = 0;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
      {
        if (row["v_IsAdded"].ToString() != "1" && Convert.ToInt32(row["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_ProductId"].ToString()))
        {
          row.BeginEdit();
          row["v_IsAdded"] = (object) "1";
          break;
        }
        ++index1;
      }
      dataTable1.Rows.RemoveAt(index1);
      this.wdgProductList.DataSource = (object) dataTable1;
      this.wdgProductList.DataBind();
      this.Session["seProductList"] = (object) dataTable1;
      this.wdgProductsAdd.DataSource = (object) dataTable2;
      this.wdgProductsAdd.DataBind();
      this.Session["sedtProductAdd"] = (object) dataTable2;
      this.lblRecordCount1.Text = dataTable1.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture) + " Registros Disponibles";
      this.lblRecordCount.Text = dataTable2.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture) + " Registros Seleccionados";
    }

    protected void wdgProductsAdd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (!(e.CommandName == "Quit"))
        return;
      this.QuitNewProduct(this.wdgProductsAdd.Rows[Convert.ToInt32(e.CommandArgument)]);
    }

    private void QuitNewProduct(GridViewRow pobjSelectedRowsGrid)
    {
      DataTable dataTable1 = this.Session["seProductList"] as DataTable;
      DataTable dataTable2 = this.Session["sedtProductAdd"] as DataTable;
      LinkButton control = pobjSelectedRowsGrid.FindControl("lnkquit") as LinkButton;
      int index = 0;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
      {
        if (Convert.ToInt32(row["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture) != Convert.ToInt32(this.wdgProductsAdd.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_ProductId"].ToString()))
          ++index;
        else
          break;
      }
      dataTable2.Rows.RemoveAt(index);
      DataRow row1 = dataTable1.NewRow();
      row1["i_productid"] = (object) this.wdgProductsAdd.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_ProductId"].ToString();
      row1["v_name"] = (object) pobjSelectedRowsGrid.Cells[1].Text;
      row1["b_status"] = (object) 1;
      row1["v_isadded"] = (object) "";
      dataTable1.Rows.Add(row1);
      control.Text = "Agregar";
      control.ForeColor = Color.Blue;
      this.wdgProductList.DataSource = (object) dataTable1;
      this.wdgProductList.DataBind();
      this.Session["seProductList"] = (object) dataTable1;
      this.wdgProductsAdd.DataSource = (object) dataTable2;
      this.wdgProductsAdd.DataBind();
      this.Session["sedtProductAdd"] = (object) dataTable2;
      this.lblRecordCount1.Text = dataTable1.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture) + " Registros Disponibles";
      this.lblRecordCount.Text = dataTable2.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture) + " Registros Seleccionados";
    }

    protected void wdgProductsAdd_PageIndexChanged(object sender, EventArgs e)
    {
      this.ShowProduct();
    }

    private void ShowProduct()
    {
      this.wdgProductsAdd.DataSource = (object) (this.Session["sedtProductAdd"] as DataTable);
      this.wdgProductsAdd.DataBind();
    }
  }
}
