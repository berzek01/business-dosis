// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Retail.RegisterRetail
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Retail
{
  public class RegisterRetail : Page
  {
    private DataTable dtb;
    private DataTable DtCar = new DataTable();
    private double dTotal = 0.0;
    private int iCantCar = 0;
    private int pintTotalRows = 1;
    private int intStartRowIndex;
    private int intMaxRows;
    protected UpdatePanel UpdatePanel1;
    protected Label LblCashRegCode;
    protected TextBox TxtSearch;
    protected ImageButton BtnSearch;
    protected ImageButton btnOrderDeta;
    protected Label LblTotal;
    protected ImageButton btnClear;
    protected Label LblCar;
    protected HtmlTableRow tr_RequirementPlate;
    protected DataList DtlProducts;
    protected Pager custPagerUQR;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!this.Page.IsPostBack)
      {
        if (this.Session["Order"] == null)
        {
          this.CargarDetalle();
        }
        else
        {
          foreach (DataRow row in (InternalDataCollectionBase) ((DataTable) this.Session["Order"]).Rows)
          {
            this.dTotal += double.Parse(row["f_Subtotal"].ToString());
            this.dTotal = double.Parse(this.dTotal.ToString("0.00"));
            this.iCantCar += int.Parse(row["i_Quantity"].ToString());
          }
          this.LblCar.Text = this.iCantCar.ToString();
          this.LblTotal.Text = "S/ " + this.dTotal.ToString("0.00");
        }
        this.SearchSPPList(0, 1, 2, 3, this.TxtSearch.Text, this.intStartRowIndex, this.intMaxRows, out this.pintTotalRows, true);
      }
      if (this.Session["v_CashRegCode"] == null && this.Session["v_CashRegId"] == null)
        return;
      this.LblCashRegCode.Text = this.Session["v_CashRegCode"].ToString();
    }

    private void SearchSPPList(
      int i_ProductId,
      int i_ProductTypeId,
      int i_ProductUseId,
      int i_CategoryId,
      string v_Description,
      int intStartRowIndex,
      int intMaxRows,
      out int totalRows,
      bool pboolLoadPager)
    {
      intStartRowIndex = pboolLoadPager ? 1 : this.custPagerUQR.CurrentPageNumber;
      intMaxRows = this.custPagerUQR.CurrentPageSize == 0 ? 10 : this.custPagerUQR.CurrentPageSize;
      this.DtlProducts.DataSource = (object) new RequirementQueriesBL().RetailGetByProduct(0, 1, 2, 16, this.TxtSearch.Text, intStartRowIndex, intMaxRows, out totalRows);
      this.DtlProducts.DataBind();
      int pintTotalRows = this.pintTotalRows;
      this.custPagerUQR.TotalPages = pintTotalRows % intMaxRows == 0 ? pintTotalRows / intMaxRows : pintTotalRows / intMaxRows + 1;
      this.custPagerUQR.TotalRecordCount = this.pintTotalRows;
      if (!pboolLoadPager)
        return;
      this.custPagerUQR.LoadPager();
    }

    protected void DtlProductos_ItemCommand(object source, DataListCommandEventArgs e)
    {
      try
      {
        if (!(e.CommandName == "Seleccionar"))
          return;
        this.DtlProducts.SelectedIndex = e.Item.ItemIndex;
        if (((TextBox) this.DtlProducts.SelectedItem.FindControl("TxtQuantity")).Text.Length > 3)
        {
          this.DtlProducts.SelectedItem.FindControl("lblMsg").Visible = true;
          Message.SetMessage((Label) this.DtlProducts.SelectedItem.FindControl("lblMsg"), new HandledException(1, "La cantidad maxima de caracteres permitidos es 3 ."));
        }
        else if (((TextBox) this.DtlProducts.SelectedItem.FindControl("TxtQuantity")).Text == "0")
        {
          this.DtlProducts.SelectedItem.FindControl("lblMsg").Visible = true;
          Message.SetMessage((Label) this.DtlProducts.SelectedItem.FindControl("lblMsg"), new HandledException(1, "La cantidad ingresada debe ser mayor a 0 ."));
        }
        else if (((TextBox) this.DtlProducts.SelectedItem.FindControl("TxtQuantity")).Text == "")
        {
          this.DtlProducts.SelectedItem.FindControl("lblMsg").Visible = true;
          Message.SetMessage((Label) this.DtlProducts.SelectedItem.FindControl("lblMsg"), new HandledException(1, "Debe ingresar cantidad de produtos a comprar ."));
        }
        else
        {
          string text1 = ((Label) this.DtlProducts.SelectedItem.FindControl("LblProductId")).Text;
          string text2 = ((Label) this.DtlProducts.SelectedItem.FindControl("LblImage")).Text;
          string text3 = ((Label) this.DtlProducts.SelectedItem.FindControl("LblName")).Text;
          double pricecost = double.Parse(((Label) this.DtlProducts.SelectedItem.FindControl("LblPriceCost")).Text);
          double pricetaxt = double.Parse(((Label) this.DtlProducts.SelectedItem.FindControl("LblPriceTax")).Text);
          double priceSale = double.Parse(((Label) this.DtlProducts.SelectedItem.FindControl("LblPriceSale")).Text);
          double price = double.Parse(((Label) this.DtlProducts.SelectedItem.FindControl("LblPrice")).Text);
          int cant = int.Parse(((TextBox) this.DtlProducts.SelectedItem.FindControl("TxtQuantity")).Text);
          this.AddItem(text1, text3, cant, price, pricecost, pricetaxt, priceSale, text2);
          this.LblCar.Text = Convert.ToString((int) Convert.ToInt16(this.LblCar.Text) + cant);
          this.DtlProducts.SelectedItem.FindControl("lblMsg").Visible = false;
        }
      }
      catch (HandledException ex)
      {
        this.HidePopup();
        Message.SetMessage((Label) this.DtlProducts.SelectedItem.FindControl("lblMsg"), ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage((Label) this.DtlProducts.SelectedItem.FindControl("lblMsg"), new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    public void CargarDetalle()
    {
      this.dtb = new DataTable("Carrito");
      this.dtb.Columns.Add("i_ProductId", Type.GetType("System.String"));
      this.dtb.Columns.Add("v_Name", Type.GetType("System.String"));
      this.dtb.Columns.Add("f_PriceCost", Type.GetType("System.Double"));
      this.dtb.Columns.Add("f_PriceTax", Type.GetType("System.Double"));
      this.dtb.Columns.Add("f_PriceSale", Type.GetType("System.Double"));
      this.dtb.Columns.Add("i_Quantity", Type.GetType("System.Int32"));
      this.dtb.Columns.Add("f_Subtotal", Type.GetType("System.Double"));
      this.dtb.Columns.Add("g_Image", Type.GetType("System.String"));
      this.Session["Order"] = (object) this.dtb;
    }

    public void AddItem(
      string cod,
      string name,
      int cant,
      double price,
      double pricecost,
      double pricetaxt,
      double priceSale,
      string image)
    {
      int num1 = 0;
      double num2 = price * (double) cant;
      this.LblTotal.Text = "S/ " + (num2 + double.Parse(this.LblTotal.Text.Substring(3, this.LblTotal.Text.Length - 3))).ToString("0.00");
      if (this.Session["Order"] == null)
        this.CargarDetalle();
      this.DtCar = (DataTable) this.Session["Order"];
      foreach (DataRow row in (InternalDataCollectionBase) this.DtCar.Rows)
      {
        if (row["i_ProductId"].ToString() == cod.ToString())
        {
          row["f_Subtotal"] = (object) (double.Parse(row["f_Subtotal"].ToString()) + num2);
          row["i_Quantity"] = (object) (int.Parse(row["i_Quantity"].ToString()) + cant);
          num1 = 1;
        }
      }
      if (num1 == 0)
      {
        DataRow row = this.DtCar.NewRow();
        row[0] = (object) cod;
        row[1] = (object) name;
        row[2] = (object) Math.Round(pricecost, 2);
        row[3] = (object) Math.Round(pricetaxt, 2);
        row[4] = (object) Math.Round(priceSale, 2);
        row[5] = (object) cant;
        row[6] = (object) num2;
        row[7] = (object) image;
        this.DtCar.Rows.Add(row);
      }
      this.Session["Order"] = (object) this.DtCar;
    }

    protected void btnOrderDeta_Click(object sender, ImageClickEventArgs e)
    {
      if (this.Session["i_CashRegId"] != null)
        this.Response.Redirect("~/Retail/OrderDetail.aspx");
      else
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", "AlertaUrl();", true);
    }

    protected void BtnSearch_Click(object sender, ImageClickEventArgs e)
    {
      try
      {
        this.SearchSPPList(0, 1, 2, 3, this.TxtSearch.Text, this.intStartRowIndex, this.intMaxRows, out this.pintTotalRows, true);
      }
      catch (Exception ex)
      {
        this.HidePopup();
        throw ex;
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void custPagerUQR_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        this.SearchSPPList(0, 1, 2, 3, this.TxtSearch.Text, this.intStartRowIndex, this.intMaxRows, out this.pintTotalRows, false);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void btnClear_Click(object sender, ImageClickEventArgs e)
    {
      this.HidePopup();
      if (this.Session["Order"] == null)
        return;
      this.Session.Remove("Order");
      this.LblTotal.Text = "S/ 0.00";
      this.LblCar.Text = "0";
    }
  }
}
