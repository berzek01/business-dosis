// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Searchs.ProductSearch
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.SystemParameter.BL;
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
  public class ProductSearch : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected RadioButtonList rblCriterioFiltro;
    protected TextBox txtFilter;
    protected DropDownList wddProductUse;
    protected Button wibSearch;
    protected GridView wdgProductList;
    protected Pager custPagerPS;
    protected Button wibFinalize;
    protected Button wibCancel;
    protected Button wibDelete;
    protected Label lblCountProductAdded;
    protected Label lblRecordCount;
    protected Label lblMsgError;
    protected HiddenField hdfInd;
    protected Button btnReturnPopupConfirmation;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      if (this.Session["ShowColumns"] != null && Convert.ToInt32(this.Session["ShowColumns"], (IFormatProvider) CultureInfo.CurrentCulture) != 1)
      {
        this.wdgProductList.Columns[2].Visible = false;
        this.wdgProductList.Columns[4].Visible = false;
        this.wdgProductList.Columns[6].Visible = false;
        this.wdgProductList.Columns[7].Visible = false;
      }
      this.LoadParameters();
      this.hdfInd.Value = "1";
    }

    protected void rblCriterioFiltro_SelectedIndexChanged(object sender, EventArgs e)
    {
      switch (int.Parse(this.rblCriterioFiltro.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture))
      {
        case 0:
          this.txtFilter.Text = string.Empty;
          this.txtFilter.Visible = true;
          this.wddProductUse.Visible = false;
          this.txtFilter.Focus();
          break;
        case 1:
          this.wddProductUse.Visible = true;
          this.txtFilter.Visible = false;
          this.wddProductUse.Focus();
          break;
      }
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchProduct();

    protected void custPagerPS_PageChanged(object sender, CustomPageChangeArgs e)
    {
      string pstrDescription = string.Empty;
      int pintProductUseId = -1;
      switch (int.Parse(this.rblCriterioFiltro.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture))
      {
        case 0:
          pstrDescription = this.txtFilter.Text;
          break;
        case 1:
          pstrDescription = "";
          pintProductUseId = Convert.ToInt32(this.wddProductUse.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
          break;
      }
      int pintCompanyId = (this.Session["SystemUser"] as SystemUser).i_CompanyId.Value;
      this.SearchProductList(0, (int) Convert.ToInt16(this.Session["sWarehouseId2"], (IFormatProvider) CultureInfo.CurrentCulture), 0, 0, pstrDescription, 0, pintProductUseId, pintCompanyId, string.Empty, -1, false);
    }

    protected void wdgProductList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowIndex < 0)
        return;
      LinkButton control = e.Row.FindControl("lnkselect") as LinkButton;
      if (e.Row.Cells[8].Text == "1")
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
      int int32 = Convert.ToInt32(e.CommandArgument);
      GridViewRow row1 = this.wdgProductList.Rows[int32];
      if (!(e.CommandName == "Select"))
        return;
      ProductWarehouseQueriesBL warehouseQueriesBl = new ProductWarehouseQueriesBL();
      DataTable dataTable1 = new DataTable();
      bool flag = false;
      string str = "";
      TextBox control = row1.FindControl("txtwdgQuantity") as TextBox;
      DataTable dataTable2 = new DataTable();
      DataTable DTEstruct = this.DT();
      DTEstruct.Rows.Add((object) Convert.ToInt32(this.wdgProductList.DataKeys[int32]["i_ProductId"].ToString()), (object) row1.Cells[1].Text, (object) ((LinkButton) row1.FindControl("lnkselect")).Text, (object) ((TextBox) row1.FindControl("txtwdgUnitPrice")).Text, (object) ((TextBox) row1.FindControl("txtwdgQuantity")).Text, (object) ((TextBox) row1.FindControl("txtwdgIncrement")).Text, (object) Convert.ToDouble(row1.Cells[5].Text), (object) Convert.ToInt32(this.wdgProductList.DataKeys[int32]["i_Item"].ToString()), (object) ((TextBox) row1.FindControl("txtInitial")).Text, (object) ((TextBox) row1.FindControl("txtFinal")).Text);
      if (((LinkButton) row1.FindControl("lnkselect")).Text == "Agregar")
      {
        DataTable dataTable3 = warehouseQueriesBl.ProductStockAlert(2);
        if (this.Session["Type"] != null)
        {
          if (Convert.ToInt32(this.Session["Type"]) == 2)
          {
            foreach (DataRow row2 in (InternalDataCollectionBase) dataTable3.Rows)
            {
              if (Convert.ToInt32(row2["i_ProductId"].ToString()) == Convert.ToInt32(this.wdgProductList.DataKeys[int32]["i_ProductId"].ToString()))
              {
                int num = Convert.ToInt32(row2["i_StockAct"].ToString()) - Convert.ToInt32(control.Text);
                if (num >= Convert.ToInt32(row2["i_StockMin"].ToString()))
                  this.AddNewProduct(DTEstruct);
                else if (num < Convert.ToInt32(row2["i_StockMin"].ToString()) && num >= Convert.ToInt32(row2["i_StockCri"].ToString()))
                {
                  str = "ADVERTENCIA...!!! Ud. está sobrepasando el stock mínimo de este producto: " + row2["v_Description"]?.ToString();
                  flag = true;
                }
                else if (num < Convert.ToInt32(row2["i_StockCri"].ToString()))
                {
                  str = "ALERTA...!!! Ud. está sobrepasando el stock crítico de este producto: " + row2["v_Description"]?.ToString();
                  flag = true;
                }
              }
              if (flag)
              {
                string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "ShowAlert('{0}');", new object[1]
                {
                  (object) str
                });
                System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
                this.ViewState["DTEstruct"] = (object) DTEstruct;
              }
            }
          }
          else
            this.AddNewProduct(DTEstruct);
        }
      }
      else
        this.AddNewProduct(DTEstruct);
    }

    protected void wdgProductList_PageIndexChanged(object sender, EventArgs e)
    {
      this.SearchProduct();
    }

    protected void wibFinalize_Click(object sender, EventArgs e)
    {
      this.SendInfoProductPopupClose();
    }

    protected void wibCancel_Click(object sender, EventArgs e) => this.PopupClose();

    protected void wibDelete_Click(object sender, EventArgs e) => this.ClearCesta();

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      if (Convert.ToInt32(this.hdfInd.Value) == 0 || this.ViewState["DTEstruct"] == null)
        return;
      this.AddNewProduct((DataTable) this.ViewState["DTEstruct"]);
    }

    protected void LoadParameters()
    {
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) ("" + SystemParameterGroups.ProductUse.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
        (object) "",
        (object) "1",
        (object) "1"
      });
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (row["i_GroupId"].ToString() == SystemParameterGroups.ProductUse.ToString((IFormatProvider) CultureInfo.CurrentCulture))
            this.wddProductUse.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        }
      }
      this.wddProductUse.Items.Insert(0, new ListItem("- Todos - ", "-1"));
      this.wddProductUse.SelectedValue = "0";
    }

    private void SearchProduct()
    {
      string pstrDescription = string.Empty;
      int pintProductUseId = -1;
      switch (int.Parse(this.rblCriterioFiltro.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture))
      {
        case 0:
          pstrDescription = this.txtFilter.Text;
          break;
        case 1:
          pstrDescription = "";
          pintProductUseId = Convert.ToInt32(this.wddProductUse.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
          break;
      }
      int pintCompanyId = (this.Session["SystemUser"] as SystemUser).i_CompanyId.Value;
      this.SearchProductList(0, (int) Convert.ToInt16(this.Session["sWarehouseId2"], (IFormatProvider) CultureInfo.CurrentCulture), 0, 0, pstrDescription, 0, pintProductUseId, pintCompanyId, string.Empty, -1, true);
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
      int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerPS.CurrentPageNumber;
      int pintMaxRows = this.custPagerPS.CurrentPageSize == 0 ? 10 : this.custPagerPS.CurrentPageSize;
      int pintTotalRows;
      DataTable productWarehouseBy = new ProductWarehouseQueriesBL().GetProductWarehouseBy(pintProductWarehouseId, pintWarehouseId, pintLocationId, pintProductId, pstrDescription, pintFlowId, pintProductUseId, pintCompanyId, pstrPlate, pintIdAssociated, pintStartRowIndex, pintMaxRows, out pintTotalRows);
      productWarehouseBy.Columns.Add("v_IsAdded", typeof (string));
      if (this.Session["seProductList"] != null)
      {
        DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
        foreach (DataRow row1 in (InternalDataCollectionBase) productWarehouseBy.Rows)
        {
          if (dataTable != null)
          {
            foreach (DataRow row2 in (InternalDataCollectionBase) dataTable.Rows)
            {
              if (Convert.ToInt32(row1["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(row2["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture) && row1["v_IsAdded"].ToString() != "1" && Convert.ToInt32(row2["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == 1)
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
      if (productWarehouseBy == null || productWarehouseBy.Rows.Count == 0)
      {
        Message.SetMessage(this.lblMsgError, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
      }
      else
      {
        this.lblMsgError.Visible = false;
        int num = pintTotalRows;
        this.wdgProductList.DataSource = (object) productWarehouseBy;
        this.wdgProductList.DataBind();
        this.custPagerPS.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
        this.custPagerPS.TotalRecordCount = pintTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerPS.LoadPager();
      }
    }

    private DataTable DT()
    {
      return new DataTable()
      {
        Columns = {
          {
            "i_ProductId",
            typeof (int)
          },
          {
            "v_Name",
            typeof (string)
          },
          {
            "lnkselect",
            typeof (string)
          },
          {
            "txtwdgUnitPrice",
            typeof (string)
          },
          {
            "txtwdgQuantity",
            typeof (string)
          },
          {
            "txtwdgIncrement",
            typeof (string)
          },
          {
            "d_stock",
            typeof (int)
          },
          {
            "i_item",
            typeof (int)
          },
          {
            "txtInitial",
            typeof (string)
          },
          {
            "txtFinal",
            typeof (string)
          }
        }
      };
    }

    private void SendInfoProductPopupClose()
    {
      DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
      string script = "SendInfoProductPopup();";
      if (dataTable.Rows.Count == 0)
        script = "PopupClose();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void PopupClose()
    {
      string script = "PopupClose();";
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
          if (Convert.ToInt32(dataTable1.Rows[index1]["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(dataTable1.Rows[index2]["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture))
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
      foreach (Control row in this.wdgProductList.Rows)
      {
        LinkButton control = (LinkButton) row.FindControl("lnkselect");
        control.Text = "Agregar";
        control.ForeColor = Color.Blue;
      }
      this.Session["sedtProductAdd"] = (object) dataTable1;
      this.lblCountProductAdded.Text = dataTable1.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    private void AddNewProduct(DataTable DTEstruct)
    {
      LinkButton linkButton = new LinkButton();
      TextBox ptxtwdgUnitPrice = new TextBox();
      TextBox ptxtwdgQuantity = new TextBox();
      TextBox textBox1 = new TextBox();
      TextBox textBox2 = new TextBox();
      TextBox textBox3 = new TextBox();
      int int32_1 = Convert.ToInt32(DTEstruct.Rows[0]["i_ProductId"].ToString());
      string str1 = DTEstruct.Rows[0]["v_Name"].ToString();
      int int32_2 = Convert.ToInt32(DTEstruct.Rows[0]["d_stock"].ToString());
      int int32_3 = Convert.ToInt32(DTEstruct.Rows[0]["i_item"].ToString());
      linkButton.Text = DTEstruct.Rows[0]["lnkselect"].ToString();
      ptxtwdgUnitPrice.Text = DTEstruct.Rows[0]["txtwdgUnitPrice"].ToString();
      ptxtwdgQuantity.Text = DTEstruct.Rows[0]["txtwdgQuantity"].ToString();
      textBox1.Text = DTEstruct.Rows[0]["txtwdgIncrement"].ToString();
      textBox2.Text = DTEstruct.Rows[0]["txtInitial"].ToString();
      textBox3.Text = DTEstruct.Rows[0]["txtFinal"].ToString();
      DataTable dataTable1 = this.Session["seProductList"] as DataTable;
      DataTable dataTable2 = this.Session["sedtProductAdd"] as DataTable;
      if (!this.IsValidInputData(ptxtwdgQuantity, ptxtwdgUnitPrice, linkButton.Text, (double) int32_2))
        return;
      string str2 = string.IsNullOrEmpty(textBox1.Text) ? "0" : textBox1.Text;
      string str3 = string.IsNullOrEmpty(ptxtwdgUnitPrice.Text) ? "0" : ptxtwdgUnitPrice.Text;
      double num = Convert.ToDouble(string.IsNullOrEmpty(ptxtwdgQuantity.Text) ? "0" : ptxtwdgQuantity.Text) * (Convert.ToDouble(str3) + Convert.ToDouble(str2));
      bool flag = false;
      if (linkButton.Text == "Quitar")
      {
        for (int index = 0; index < dataTable2.Rows.Count; ++index)
        {
          if (Convert.ToInt32(dataTable2.Rows[index]["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture) == int32_1)
          {
            dataTable2.Rows.RemoveAt(index);
            linkButton.Text = "Agregar";
            linkButton.ForeColor = Color.Blue;
            flag = true;
          }
        }
      }
      else if (linkButton.Text == "Agregar" && dataTable2 != null)
      {
        for (int index = 0; index < dataTable2.Rows.Count; ++index)
        {
          if (Convert.ToInt32(dataTable2.Rows[index]["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture) == int32_1)
          {
            if (linkButton.Text == "Agregar")
              this.lblMsgError.Text = "El producto ya está agregado";
            linkButton.Text = "Agregar";
            linkButton.ForeColor = Color.Blue;
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
        dataTable2.Rows.Add((object) 0, (object) 1, null, (object) int32_1, (object) 1, (object) (ptxtwdgUnitPrice.Text.Length > 0 ? Convert.ToDouble(ptxtwdgUnitPrice.Text, (IFormatProvider) CultureInfo.CurrentCulture) : 0.0), (object) Convert.ToDouble(ptxtwdgQuantity.Text, (IFormatProvider) CultureInfo.CurrentCulture), (object) str1, (object) 1, (object) 0, (object) 1, (object) 0.0f, (object) 0.0f, (object) Convert.ToDouble(str2), (object) 0.0f, (object) num, (object) int32_3, null, null, null, null, null, (object) textBox2.Text, (object) textBox3.Text);
        linkButton.Text = "Quitar";
        linkButton.ForeColor = Color.Red;
      }
      foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
      {
        if (row["v_IsAdded"].ToString() != "1" && Convert.ToInt32(row["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture) == int32_1)
        {
          row.BeginEdit();
          row["v_IsAdded"] = (object) "1";
        }
        else if (Convert.ToInt32(row["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture) == int32_1)
        {
          row.BeginEdit();
          row["v_IsAdded"] = (object) "";
        }
      }
      this.wdgProductList.DataSource = (object) dataTable1;
      this.wdgProductList.DataBind();
      this.Session["seProductList"] = (object) dataTable1;
      this.Session["sedtProductAdd"] = (object) dataTable2;
      this.lblCountProductAdded.Text = dataTable2.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    private bool IsValidInputData(
      TextBox ptxtwdgQuantity,
      TextBox ptxtwdgUnitPrice,
      string strAction,
      double stock)
    {
      string str = string.Empty;
      if (strAction == "Agregar")
      {
        if (this.Session["ShowColumns"] != null && Convert.ToInt32(this.Session["ShowColumns"], (IFormatProvider) CultureInfo.CurrentCulture) == 1)
        {
          if (this.ValidarNumero(ptxtwdgUnitPrice.Text))
          {
            if (ptxtwdgUnitPrice.Text == "0")
            {
              ptxtwdgUnitPrice.BackColor = Color.FromArgb(236, 213, 213);
              str += "<br>  •   &nbsp;&nbsp;&nbsp;Debe ingresar un precio unitario mayor a cero [1-9]";
            }
            else
              ptxtwdgUnitPrice.BackColor = Color.White;
          }
          else
          {
            ptxtwdgUnitPrice.BackColor = Color.FromArgb(236, 213, 213);
            str += "<br>  •   &nbsp;&nbsp;&nbsp;El campo [Precio] debe ser un número";
          }
        }
        if (this.ValidarNumero(ptxtwdgQuantity.Text))
        {
          if (ptxtwdgQuantity.Text == "0")
          {
            ptxtwdgQuantity.BackColor = Color.FromArgb(236, 213, 213);
            str += "<br>  •   &nbsp;&nbsp;&nbsp;Debe ingresar una cantidad mayor a cero [1-9]";
          }
          else
            ptxtwdgQuantity.BackColor = Color.White;
          if (this.Session["ShowColumns"] != null && Convert.ToDouble(ptxtwdgQuantity.Text, (IFormatProvider) CultureInfo.CurrentCulture) > stock && Convert.ToInt32(this.Session["ShowColumns"], (IFormatProvider) CultureInfo.CurrentCulture) == 0)
            str = str + "<br>  •   &nbsp;&nbsp;&nbsp;La cantidad ingresada debe ser menor al stock [" + stock.ToString((IFormatProvider) CultureInfo.CurrentCulture) + " Productos]";
        }
        else
        {
          ptxtwdgQuantity.BackColor = Color.FromArgb(236, 213, 213);
          str += "<br>  •   &nbsp;&nbsp;&nbsp;El campo [Cantidad] debe ser un número";
        }
        if (!string.IsNullOrEmpty(str))
        {
          this.lblMsgError.Text = str;
          return false;
        }
        this.lblMsgError.Text = string.Empty;
      }
      return true;
    }

    private bool ValidarNumero(string text) => Regex.IsMatch(text, "^[+-]?\\d+(?:\\.\\d+)?$");
  }
}
