// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Searchs.ProductSearchStragglers
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

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
  public class ProductSearchStragglers : Page
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

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      this.SearchProduct();
      this.HidePopup();
    }

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
      SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
      DataTable productStragglersBy = new ProductWarehouseQueriesBL().GetProductStragglersBy(Convert.ToInt32(this.rblCriterioFiltro.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture), text);
      productStragglersBy.Columns.Add("v_IsAdded", typeof (string));
      int count;
      if (this.Session["seProductList"] != null)
      {
        if (this.Session["sedtProductAdd"] is DataTable dataTable)
        {
          foreach (DataRow row1 in (InternalDataCollectionBase) dataTable.Rows)
          {
            foreach (DataRow row2 in new ArrayList((ICollection) productStragglersBy.Rows))
            {
              if (Convert.ToInt32(row2["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(row1["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture) && Convert.ToInt32(row2["i_StragglersTypeId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(row1["i_StragglersTypeId"], (IFormatProvider) CultureInfo.CurrentCulture))
                productStragglersBy.Rows.Remove(row2);
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
      this.wdgProductList.DataSource = (object) productStragglersBy;
      this.wdgProductList.DataBind();
      this.Session["seProductList"] = (object) productStragglersBy;
      Label lblRecordCount1 = this.lblRecordCount1;
      count = productStragglersBy.Rows.Count;
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
          if (Convert.ToInt32(dataTable2.Rows[index]["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(pobjSelectedRowsGrid.Cells[0].Text) && Convert.ToInt32(dataTable2.Rows[index]["i_StragglersTypeId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(pobjSelectedRowsGrid.Cells[3].Text))
          {
            if (control.Text == "Agregar")
              this.lblMsgError.Text = "El producto ya esta agregado. Elija otro";
            control.Text = "Agregar";
            control.ForeColor = Color.Blue;
            flag = true;
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
              "i_StragglersTypeId",
              "v_IsAdded",
              "item"
            }
          }.Clone();
        dataTable2.Rows.Add((object) pobjSelectedRowsGrid.Cells[0].Text, (object) pobjSelectedRowsGrid.Cells[2].Text, (object) pobjSelectedRowsGrid.Cells[3].Text, (object) 1, (object) (dataTable2.Rows.Count == 0 ? 1 : Convert.ToInt32(dataTable2.Compute("max(item)", string.Empty), (IFormatProvider) CultureInfo.CurrentCulture) + 1));
        control.Text = "Quitar";
        control.ForeColor = Color.Red;
      }
      int index1 = 0;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
      {
        if (row["v_IsAdded"].ToString() != "1" && Convert.ToInt32(row["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(pobjSelectedRowsGrid.Cells[0].Text) && Convert.ToInt32(row["i_StragglersTypeId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(pobjSelectedRowsGrid.Cells[3].Text))
        {
          row.BeginEdit();
          row["v_IsAdded"] = (object) "1";
          break;
        }
        ++index1;
      }
      if (index1 < dataTable1.Rows.Count)
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
      DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
      int index = 0;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (Convert.ToInt32(row["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture) != Convert.ToInt32(pobjSelectedRowsGrid.Cells[0].Text) || Convert.ToInt32(row["i_StragglersTypeId"], (IFormatProvider) CultureInfo.CurrentCulture) != Convert.ToInt32(pobjSelectedRowsGrid.Cells[3].Text))
          ++index;
        else
          break;
      }
      if (index < dataTable.Rows.Count)
        dataTable.Rows.RemoveAt(index);
      this.SearchProduct();
      this.wdgProductsAdd.DataSource = (object) dataTable;
      this.wdgProductsAdd.DataBind();
      this.Session["sedtProductAdd"] = (object) dataTable;
      this.lblRecordCount.Text = dataTable.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture) + " Registros Seleccionados";
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
