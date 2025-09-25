// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Retail.OrderDetail
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Common.Resource;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Retail
{
  public class OrderDetail : Page
  {
    private double VariableIgv = Convert.ToDouble(ConfigurationManager.AppSettings["Igv"]);
    private DataTable DtCar = new DataTable();
    private int index;
    protected UpdatePanel UpdatePanel1;
    protected Label LblCashRegCode;
    protected Button wibSearch;
    protected Button BtnProduct;
    protected Button BtnActualizar;
    protected Button BtnComprar;
    protected Label lblMessage;
    protected Label lblSubTotal;
    protected Label lblIGV;
    protected Label lblTotal;
    protected GridView GridView1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      this.lblMessage.Text = "";
      if (!this.Page.IsPostBack)
        this.cargarcarrito();
      if (this.Session["v_CashRegCode"] == null && this.Session["v_CashRegId"] == null)
        return;
      this.LblCashRegCode.Text = this.Session["v_CashRegCode"].ToString();
    }

    public void cargarcarrito()
    {
      GridView gridView = new GridView();
      this.GridView1.DataSource = this.Session["Order"];
      this.GridView1.DataBind();
      this.Totales();
    }

    protected void Totales()
    {
      try
      {
        if (this.Session["Order"] != null)
        {
          double num1 = 0.0;
          foreach (DataRow row in (InternalDataCollectionBase) ((DataTable) this.Session["Order"]).Rows)
            num1 += Convert.ToDouble(row["f_PriceCost"]) * (double) Convert.ToInt32(row["i_Quantity"]);
          double num2 = Math.Round(Math.Round(num1, 2) * this.VariableIgv, 2);
          double num3 = Math.Round(num1, 2) + Math.Round(num2, 2);
          this.lblIGV.Text = num2.ToString("0.00");
          this.lblSubTotal.Text = num1.ToString("0.00");
          this.lblTotal.Text = num3.ToString("0.00");
        }
        else
        {
          this.lblIGV.Text = "0.00";
          this.lblSubTotal.Text = "0.00";
          this.lblTotal.Text = "0.00";
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
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      int int32 = Convert.ToInt32(e.RowIndex);
      DataTable dataTable1 = new DataTable();
      DataTable dataTable2 = (DataTable) this.Session["Order"];
      dataTable2.Rows[int32].Delete();
      this.GridView1.DataSource = (object) dataTable2;
      this.GridView1.DataBind();
      this.cargarcarrito();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("~/Retail/RegisterRetail.aspx");
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
        ;
    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
      try
      {
        foreach (GridViewRow row1 in this.GridView1.Rows)
        {
          TextBox control = row1.FindControl("i_Quantity") as TextBox;
          if (control.Text == "0")
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "la cantidad a comprar debe ser mayor a 0 . "));
            return;
          }
          if (control.Text == "")
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "la cantidad a comprar debe ser diferente de vacio "));
            return;
          }
          int num1 = 0;
          double num2 = double.Parse(row1.Cells[3].Text);
          string text = row1.Cells[0].Text;
          int num3 = int.Parse((row1.FindControl("i_Quantity") as TextBox).Text);
          double num4 = num2 * (double) num3;
          this.DtCar = (DataTable) this.Session["Order"];
          foreach (DataRow row2 in (InternalDataCollectionBase) this.DtCar.Rows)
          {
            if (row2["i_ProductId"].ToString() == text)
            {
              row2["f_Subtotal"] = (object) num4;
              row2["i_Quantity"] = (object) num3;
              num1 = 1;
            }
          }
        }
        this.cargarcarrito();
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

    protected void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        this.BtnActualizar_Click((object) null, (EventArgs) null);
        if (this.Session["Order"] == null)
          this.CargarDetalle();
        DataTable dataTable = (DataTable) this.Session["Order"];
        if (dataTable.Rows.Count > 0)
        {
          for (int index = 0; index < dataTable.Rows.Count; ++index)
          {
            if (dataTable.Rows[0]["i_Quantity"] == (object) "0")
            {
              Message.SetMessage(this.lblMessage, new HandledException(1, "La cantidad minima de productos a comprar debe ser 1 ."));
              return;
            }
            if (dataTable.Rows[0]["i_Quantity"] == (object) "")
            {
              Message.SetMessage(this.lblMessage, new HandledException(1, "Debe ingresar la cantidad a comprar ."));
              return;
            }
          }
          if (this.lblMessage.Visible)
            return;
          this.Session["RequirementPlateTypeRetail"] = (object) Convert.ToInt32((object) enmRequirementPlateType.Retail);
          this.Session["ProcessIdRetail"] = (object) Convert.ToInt32((object) enmProccessType.Retail);
          this.Response.Redirect("~/Retail/RegisterRequirementOrderRetail.aspx");
        }
        else
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró productos para Comprar"));
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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      this.GridView1.PageIndex = e.NewPageIndex;
      this.cargarcarrito();
    }

    public void CargarDetalle()
    {
      this.DtCar = new DataTable("Carrito");
      this.DtCar.Columns.Add("i_ProductId", Type.GetType("System.String"));
      this.DtCar.Columns.Add("v_Name", Type.GetType("System.String"));
      this.DtCar.Columns.Add("f_PriceCost", Type.GetType("System.Double"));
      this.DtCar.Columns.Add("f_PriceTax", Type.GetType("System.Double"));
      this.DtCar.Columns.Add("f_PriceSale", Type.GetType("System.Double"));
      this.DtCar.Columns.Add("i_Quantity", Type.GetType("System.Int32"));
      this.DtCar.Columns.Add("f_Subtotal", Type.GetType("System.Double"));
      this.DtCar.Columns.Add("g_Image", Type.GetType("System.String"));
      this.Session["Order"] = (object) this.DtCar;
    }
  }
}
