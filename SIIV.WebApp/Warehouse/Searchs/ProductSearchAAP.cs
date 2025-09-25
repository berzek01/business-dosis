// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Searchs.ProductSearchAAP
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Searchs
{
  public class ProductSearchAAP : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected RadioButtonList rblCriterioFiltro;
    protected Label lblDescriptionProduct;
    protected TextBox txtFilter;
    protected Button wibSearch;
    protected GridView wdgProductList;
    protected Pager custPagerBatch;
    protected Label lblRecordCount;
    protected Label lblCountProductAdded;
    protected Button wibFinalze;
    protected Button wibCancel;
    protected Button wibClearBasket;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      if (Convert.ToInt16(this.Session["seFlowId"], (IFormatProvider) CultureInfo.CurrentCulture) == (short) 1)
      {
        this.lblDescriptionProduct.Visible = true;
        this.rblCriterioFiltro.Visible = false;
        this.wdgProductList.Columns[2].Visible = true;
        this.wdgProductList.Columns[4].Visible = true;
        this.wdgProductList.Columns[5].Visible = false;
        this.wdgProductList.Columns[6].Visible = false;
        this.wdgProductList.Columns[7].Visible = false;
        this.wdgProductList.Columns[8].Visible = false;
        this.wdgProductList.Columns[9].Visible = false;
      }
      else
      {
        this.lblDescriptionProduct.Visible = false;
        this.rblCriterioFiltro.Visible = true;
        this.wdgProductList.Columns[2].Visible = false;
        this.wdgProductList.Columns[3].Visible = false;
        this.wdgProductList.Columns[4].Visible = false;
        this.wdgProductList.Columns[5].Visible = false;
        this.wdgProductList.Columns[6].Visible = true;
        this.wdgProductList.Columns[7].Visible = true;
        this.wdgProductList.Columns[8].Visible = true;
        this.wdgProductList.Columns[9].Visible = true;
      }
    }

    protected void rblCriterioFiltro_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.txtFilter.Text = string.Empty;
      this.txtFilter.Focus();
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchProduct();

    protected void wdgProductList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowIndex < 0)
        return;
      LinkButton control = e.Row.FindControl("lnkselect") as LinkButton;
      if (this.wdgProductList.DataKeys[e.Row.RowIndex]["v_IsAdded"].ToString() == "1")
      {
        control.Text = "Quitar";
        control.ForeColor = Color.Red;
      }
      else
      {
        control.Text = "Agregar";
        control.ForeColor = Color.Blue;
      }
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

    protected void custPagerBatch_PageChanged(object sender, CustomPageChangeArgs e)
    {
      string pstrPlate = string.Empty;
      int pintIdAssociated = -1;
      string pstrDescription = string.Empty;
      switch (int.Parse(this.rblCriterioFiltro.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture))
      {
        case 0:
          pstrPlate = string.IsNullOrEmpty(this.txtFilter.Text) ? "" : this.txtFilter.Text;
          break;
        case 1:
          pintIdAssociated = string.IsNullOrEmpty(this.txtFilter.Text) ? -1 : Convert.ToInt32(this.txtFilter.Text, (IFormatProvider) CultureInfo.CurrentCulture);
          break;
      }
      if (!this.rblCriterioFiltro.Visible)
        pstrDescription = string.IsNullOrEmpty(this.txtFilter.Text) ? "" : this.txtFilter.Text;
      short int16 = Convert.ToInt16(this.Session["sWarehouseId2"], (IFormatProvider) CultureInfo.CurrentCulture);
      int pintCompanyId = (this.Session["SystemUser"] as SystemUser).i_CompanyId.Value;
      this.SearchProductList(0, (int) int16, 0, 0, pstrDescription, Convert.ToInt32(this.Session["seFlowId"], (IFormatProvider) CultureInfo.CurrentCulture), -1, pintCompanyId, pstrPlate, pintIdAssociated, false);
    }

    protected void wibFinalze_Click(object sender, EventArgs e) => this.SendInfoProductPopupClose();

    protected void wibCancel_Click(object sender, EventArgs e) => this.PopupClose();

    protected void wibClearBasket_Click(object sender, EventArgs e) => this.ClearCesta();

    private void SearchProduct()
    {
      try
      {
        string pstrPlate = string.Empty;
        int pintIdAssociated = -1;
        string pstrDescription = string.Empty;
        switch (int.Parse(this.rblCriterioFiltro.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture))
        {
          case 0:
            pstrPlate = string.IsNullOrEmpty(this.txtFilter.Text) ? "" : this.txtFilter.Text;
            break;
          case 1:
            pintIdAssociated = string.IsNullOrEmpty(this.txtFilter.Text) ? -1 : Convert.ToInt32(this.txtFilter.Text, (IFormatProvider) CultureInfo.CurrentCulture);
            break;
        }
        if (!this.rblCriterioFiltro.Visible)
          pstrDescription = string.IsNullOrEmpty(this.txtFilter.Text) ? "" : this.txtFilter.Text;
        short int16 = Convert.ToInt16(this.Session["sWarehouseId2"], (IFormatProvider) CultureInfo.CurrentCulture);
        int pintCompanyId = (this.Session["SystemUser"] as SystemUser).i_CompanyId.Value;
        this.SearchProductList(0, (int) int16, 0, 0, pstrDescription, Convert.ToInt32(this.Session["seFlowId"], (IFormatProvider) CultureInfo.CurrentCulture), -1, pintCompanyId, pstrPlate, pintIdAssociated, true);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, "Error*****" + ex.Message);
      }
    }

    private void SearchProductList(
      int pintProductWarehouseId,
      int pintWarehouseId,
      int pintLocationId,
      int pintProductId,
      string pstrDescription,
      int pintFlowId,
      int pintProductUseId,
      int pintCompanyId,
      string pstrPlate,
      int pintIdAssociated,
      bool pboolLoadPager)
    {
      int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerBatch.CurrentPageNumber;
      int pintMaxRows = this.custPagerBatch.CurrentPageSize == 0 ? 10 : this.custPagerBatch.CurrentPageSize;
      int pintTotalRows;
      DataTable productWarehouseBy = new ProductWarehouseQueriesBL().GetProductWarehouseBy(pintProductWarehouseId, pintWarehouseId, pintLocationId, pintProductId, pstrDescription, pintFlowId, pintProductUseId, pintCompanyId, pstrPlate, pintIdAssociated, pintStartRowIndex, pintMaxRows, out pintTotalRows);
      productWarehouseBy.Columns.Add("v_IsAdded", typeof (string));
      short int16 = Convert.ToInt16(this.Session["seFlowId"], (IFormatProvider) CultureInfo.CurrentCulture);
      if (this.Session["seProductList"] != null)
      {
        DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
        foreach (DataRow row1 in (InternalDataCollectionBase) productWarehouseBy.Rows)
        {
          if (dataTable != null)
          {
            foreach (DataRow row2 in (InternalDataCollectionBase) dataTable.Rows)
            {
              if (int16 == (short) 1)
              {
                if (Convert.ToInt32(row1["i_StockMovementDetailId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(row2["i_StockMovementDetailId"], (IFormatProvider) CultureInfo.CurrentCulture) && row1["v_IsAdded"].ToString() != "1" && Convert.ToInt32(row2["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == 1)
                {
                  row1.BeginEdit();
                  row1["v_IsAdded"] = (object) "1";
                }
              }
              else if (Convert.ToInt32(row1["i_StockMovementDetailId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(row2["i_StockMovementDetailId"], (IFormatProvider) CultureInfo.CurrentCulture) && row1["v_IsAdded"].ToString() != "1" && Convert.ToInt32(row2["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == 1)
              {
                row1.BeginEdit();
                row1["v_IsAdded"] = (object) "1";
              }
            }
          }
        }
        this.lblCountProductAdded.Text = dataTable != null ? dataTable.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture) : "0";
      }
      this.Session["seProductList"] = (object) productWarehouseBy;
      int num = pintTotalRows;
      this.wdgProductList.DataSource = (object) productWarehouseBy;
      this.wdgProductList.DataBind();
      this.custPagerBatch.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
      this.custPagerBatch.TotalRecordCount = pintTotalRows;
      if (!pboolLoadPager)
        return;
      this.custPagerBatch.LoadPager();
    }

    private void AddNewProduct(GridViewRow pobjSelectedRowsGrid)
    {
      LinkButton control = (LinkButton) pobjSelectedRowsGrid.Cells[0].FindControl("lnkselect");
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      bool flag = false;
      short int16 = Convert.ToInt16(this.Session["seFlowId"], (IFormatProvider) CultureInfo.CurrentCulture);
      DataTable dataTable1 = this.Session["seProductList"] as DataTable;
      DataTable dataTable2 = this.Session["sedtProductAdd"] as DataTable;
      switch (int16)
      {
        case 1:
          string text1 = pobjSelectedRowsGrid.Cells[2].Text;
          string text2 = pobjSelectedRowsGrid.Cells[3].Text;
          string text3 = pobjSelectedRowsGrid.Cells[4].Text;
          double stock = Convert.ToDouble(pobjSelectedRowsGrid.Cells[5].Text, (IFormatProvider) CultureInfo.CurrentCulture);
          if (!this.IsValidInputData(text2, text1, control.Text, stock))
            return;
          string str1 = string.IsNullOrEmpty(text3) ? "0" : text3;
          string str2 = string.IsNullOrEmpty(text1) ? "0" : text1;
          double num = Convert.ToDouble(string.IsNullOrEmpty(text2) ? "0" : text2, (IFormatProvider) CultureInfo.CurrentCulture) * (Convert.ToDouble(str2, (IFormatProvider) CultureInfo.CurrentCulture) + Convert.ToDouble(str1, (IFormatProvider) CultureInfo.CurrentCulture));
          if (control.Text == "Quitar")
          {
            for (int index = 0; index < dataTable2.Rows.Count; ++index)
            {
              if (Convert.ToInt32(dataTable2.Rows[index]["i_StockMovementDetailId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_StockMovementDetailId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture))
              {
                dataTable2.Rows.RemoveAt(index);
                control.Text = "Agregar";
                control.ForeColor = Color.Blue;
                flag = true;
              }
            }
          }
          else if (control.Text == "Agregar" && dataTable2 != null)
          {
            for (int index = 0; index < dataTable2.Rows.Count; ++index)
            {
              if (Convert.ToInt32(dataTable2.Rows[index]["i_StockMovementDetailId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_StockMovementDetailId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture))
              {
                if (control.Text == "Agregar")
                  this.lblMessage.Text = "El producto ya está agregado.";
                control.Text = "Agregar";
                control.ForeColor = Color.Blue;
                flag = true;
                if (Convert.ToInt32(dataTable2.Rows[index]["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == 0)
                {
                  dataTable2.Rows[index]["i_Status"] = (object) 1;
                  break;
                }
                break;
              }
            }
          }
          if (!flag)
          {
            dataTable2.Rows.Add((object) this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_StockMovementDetailId"].ToString(), (object) this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_StockMovementId"].ToString(), (object) this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_LocationWarehouseId"].ToString(), (object) this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_ProductId"].ToString(), null, (object) (text1.Length > 0 ? Convert.ToDouble(text1, (IFormatProvider) CultureInfo.CurrentCulture) : 0.0), (object) Convert.ToInt32(text2, (IFormatProvider) CultureInfo.CurrentCulture), (object) pobjSelectedRowsGrid.Cells[1].Text, (object) this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_Item"].ToString(), (object) 0, (object) 1, (object) 0.0f, (object) 0.0f, (object) Convert.ToDouble(str1, (IFormatProvider) CultureInfo.CurrentCulture), (object) 0.0f, (object) num, (object) this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_IdAssociated"].ToString(), null, null, null, null, (object) this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_VehicleTypeUseId"].ToString());
            control.Text = "Quitar";
            control.ForeColor = Color.Red;
          }
          IEnumerator enumerator1 = dataTable1.Rows.GetEnumerator();
          try
          {
            while (enumerator1.MoveNext())
            {
              DataRow current = (DataRow) enumerator1.Current;
              if (current["v_IsAdded"].ToString() != "1" && Convert.ToInt32(current["i_StockMovementDetailId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_StockMovementDetailId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture))
              {
                current.BeginEdit();
                current["v_IsAdded"] = (object) "1";
              }
              else if (Convert.ToInt32(current["i_StockMovementDetailId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_StockMovementDetailId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture))
              {
                current.BeginEdit();
                current["v_IsAdded"] = (object) "";
              }
            }
            break;
          }
          finally
          {
            if (enumerator1 is IDisposable disposable)
              disposable.Dispose();
          }
        case 2:
          if (control.Text == "Quitar")
          {
            for (int index = 0; index < dataTable2.Rows.Count; ++index)
            {
              if (Convert.ToInt32(dataTable2.Rows[index]["i_StockMovementDetailId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_StockMovementDetailId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture))
              {
                dataTable2.Rows.RemoveAt(index);
                control.Text = "Agregar";
                control.ForeColor = Color.Blue;
                flag = true;
              }
            }
          }
          else if (control.Text == "Agregar" && dataTable2 != null)
          {
            for (int index = 0; index < dataTable2.Rows.Count; ++index)
            {
              if (Convert.ToInt32(dataTable2.Rows[index]["i_StockMovementDetailId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_StockMovementDetailId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture))
              {
                if (control.Text == "Agregar")
                  this.lblMessage.Text = "El producto ya está agregado. Elija otro";
                control.Text = "Agregar";
                control.ForeColor = Color.Blue;
                flag = true;
                if (Convert.ToInt32(dataTable2.Rows[index]["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == 0)
                {
                  dataTable2.Rows[index]["i_Status"] = (object) 1;
                  break;
                }
                break;
              }
            }
          }
          if (!flag)
          {
            dataTable2.Rows.Add((object) this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_StockMovementDetailId"].ToString(), (object) this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_StockMovementId"].ToString(), (object) this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_LocationWarehouseId"].ToString(), (object) this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_ProductId"].ToString(), null, null, (object) 1, (object) pobjSelectedRowsGrid.Cells[1].Text, (object) this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_IdAssociated"].ToString(), null, (object) 1, null, null, null, null, null, (object) this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_Item"].ToString(), null, null, null, (object) pobjSelectedRowsGrid.Cells[6].Text, (object) this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_VehicleTypeUseId"].ToString());
            control.Text = "Quitar";
            control.ForeColor = Color.Red;
          }
          IEnumerator enumerator2 = dataTable1.Rows.GetEnumerator();
          try
          {
            while (enumerator2.MoveNext())
            {
              DataRow current = (DataRow) enumerator2.Current;
              if (current["v_IsAdded"].ToString() != "1" && Convert.ToInt32(current["i_StockMovementDetailId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_StockMovementDetailId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture))
              {
                current.BeginEdit();
                current["v_IsAdded"] = (object) "1";
              }
              else if (Convert.ToInt32(current["i_StockMovementDetailId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(this.wdgProductList.DataKeys[pobjSelectedRowsGrid.RowIndex]["i_StockMovementDetailId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture))
              {
                current.BeginEdit();
                current["v_IsAdded"] = (object) "";
              }
            }
            break;
          }
          finally
          {
            if (enumerator2 is IDisposable disposable)
              disposable.Dispose();
          }
      }
      this.wdgProductList.DataSource = (object) dataTable1;
      this.wdgProductList.DataBind();
      this.Session["seProductList"] = (object) dataTable1;
      this.Session["sedtProductAdd"] = (object) dataTable2;
      this.lblCountProductAdded.Text = dataTable2.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    private void SendInfoProductPopupClose()
    {
      DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
      string script = "SendInfoProductPopup();";
      if (dataTable.Rows.Count == 0)
        script = "PopupClose();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void ClearCesta()
    {
      DataTable dataTable1 = this.Session["sedtProductAdd"] as DataTable;
      DataTable dataTable2 = this.Session["seProductList"] as DataTable;
      for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
      {
        for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
        {
          if (Convert.ToInt32(dataTable1.Rows[index1]["i_StockMovementDetailId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(dataTable1.Rows[index2]["i_StockMovementDetailId"], (IFormatProvider) CultureInfo.CurrentCulture))
          {
            dataTable2.Rows[index2]["v_IsAdded"] = (object) "";
            break;
          }
        }
      }
      dataTable1.Rows.Clear();
      this.wdgProductList.DataSource = (object) dataTable2;
      this.wdgProductList.DataBind();
      this.Session["seProductList"] = (object) dataTable2;
      foreach (TableRow row in this.wdgProductList.Rows)
      {
        LinkButton control = (LinkButton) row.Cells[0].FindControl("lnkselect");
        control.Text = "Agregar";
        control.ForeColor = Color.Blue;
      }
      this.Session["sedtProductAdd"] = (object) dataTable1;
      this.lblCountProductAdded.Text = dataTable1.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    private void PopupClose()
    {
      string script = "PopupClose();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    public void MessageBoxJS(string mensaje)
    {
      string script = "ShowAlert('" + mensaje + "');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private bool IsValidInputData(
      string ptxtwdgQuantity,
      string ptxtwdgUnitPrice,
      string strAction,
      double stock)
    {
      string str = string.Empty;
      if (strAction == "Agregar")
      {
        if (this.Session["seFlowId"] != null && Convert.ToInt32(this.Session["seFlowId"], (IFormatProvider) CultureInfo.CurrentCulture) == 1)
        {
          if (this.ValidarNumero(ptxtwdgUnitPrice))
          {
            if (ptxtwdgUnitPrice == "0")
              str += "<br>  •   &nbsp;&nbsp;&nbsp;Debe ingresar un precio unitario mayor a cero [1-9]";
          }
          else
            str += "<br>  •   &nbsp;&nbsp;&nbsp;El campo [Precio] debe ser un número";
        }
        if (this.ValidarNumero(ptxtwdgQuantity))
        {
          if (ptxtwdgQuantity == "0")
            str += "<br>  •   &nbsp;&nbsp;&nbsp;Debe ingresar una cantidad mayor a cero [1-9]";
          if (this.Session["seFlowId"] != null && Convert.ToDouble(ptxtwdgQuantity, (IFormatProvider) CultureInfo.CurrentCulture) > stock && Convert.ToInt32(this.Session["seFlowId"], (IFormatProvider) CultureInfo.CurrentCulture) == 2)
            str = str + "<br>  •   &nbsp;&nbsp;&nbsp;Cantidad ingresada debe ser menor al stock [" + stock.ToString((IFormatProvider) CultureInfo.CurrentCulture) + " Productos]";
        }
        else
          str += "<br>  •   &nbsp;&nbsp;&nbsp;El campo [Cantidad]  debe ser un número";
        if (!string.IsNullOrEmpty(str))
        {
          this.lblMessage.Text = str;
          return false;
        }
        this.lblMessage.Text = string.Empty;
      }
      return true;
    }

    private bool ValidarNumero(string text) => Regex.IsMatch(text, "^[+-]?\\d+(?:\\.\\d+)?$");
  }
}
