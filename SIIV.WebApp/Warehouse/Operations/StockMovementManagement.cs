// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.StockMovementManagement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class StockMovementManagement : Page
  {
    protected HtmlForm form1;
    protected UpdatePanel UpdatePanel1;
    protected DropDownList wddWarehouse;
    protected TextBox txtSupplier;
    protected Button btnShowSupplier;
    protected DropDownList wddFlow;
    protected DropDownList wddMovementType;
    protected DropDownList wddDocumentType;
    protected Fecha wdpDate;
    protected TextBox txtPurchaseOrder;
    protected DropDownList wddCurrency;
    protected TextBox txtDocumentType;
    protected TextBox txtExchangeRate;
    protected TextBox TextBox1;
    protected Button btnAddProductList;
    protected Button btnDeleteProductList;
    protected GridView wdgStockMovementProduct;
    protected Button btnAcept;
    protected Button btnCancel;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadWarehouse();
      this.LoadMovementType(1);
      this.LoadParameters();
      this.btnShowSupplier.OnClientClick = this.CreatePopUp("Lista Proveedores", 1);
    }

    protected void wddFlow_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.LoadMovementType(Convert.ToInt32(this.wddFlow.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
    }

    protected void wddCurrency_SelectionChanged(object sender, EventArgs e)
    {
      if (!(this.wddCurrency.SelectedValue != "1"))
        return;
      this.LoadCurrency(Convert.ToInt32(this.wddCurrency.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), DateTime.Now.ToString("yyyy-MM-dd", (IFormatProvider) CultureInfo.CurrentCulture));
    }

    protected void btnShowSupplier_Click(object sender, EventArgs e)
    {
    }

    private void LoadWarehouse()
    {
      SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
      int iLocationId = systemUser.i_LocationId;
      int num = systemUser.i_CompanyId.Value;
      this.wddWarehouse.DataSource = (object) new WarehouseQueriesBL().GetWarehouseBy(0, "", iLocationId, -1);
      this.wddWarehouse.DataTextField = "v_Description";
      this.wddWarehouse.DataValueField = "i_WarehouseId";
      this.wddWarehouse.DataBind();
      this.wddWarehouse.Items.Insert(0, new ListItem("- Seleccione - ", "-1"));
      this.wddWarehouse.SelectedValue = "-1";
    }

    private void LoadMovementType(int pintflowId)
    {
      SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
      int iLocationId = systemUser.i_LocationId;
      int int32 = Convert.ToInt32((object) systemUser.i_CompanyId, (IFormatProvider) CultureInfo.CurrentCulture);
      this.wddMovementType.DataSource = (object) new MovementTypeQueriesBL().GetMovementTypeBy(0, "", pintflowId, -1, 0, int32);
      this.wddMovementType.DataTextField = "v_Description";
      this.wddMovementType.DataValueField = "i_WarehouseId";
      this.wddMovementType.DataBind();
      this.wddMovementType.Items.Insert(0, new ListItem("- Seleccione - ", "-1"));
      this.wddWarehouse.SelectedValue = "-1";
    }

    private void LoadCurrency(int pintCurrencyId, string strProcessDate)
    {
      this.txtExchangeRate.Text = new ExchangeRateQueriesBL().GetExchangeRateBy(pintCurrencyId, strProcessDate).Rows[0]["f_LocalValue"].ToString();
    }

    protected void LoadParameters()
    {
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) ("" + SystemParameterGroups.ModelCurrency.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
        (object) "",
        (object) "1",
        (object) "1"
      });
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (row["i_GroupId"].ToString() == SystemParameterGroups.ModelCurrency.ToString((IFormatProvider) CultureInfo.CurrentCulture))
            this.wddCurrency.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        }
      }
      this.wddCurrency.Items.Insert(0, new ListItem("- Seleccione - ", "-1"));
    }

    private string CreatePopUp(string pstrtitle, int pintSupplierId)
    {
      string empty = string.Empty;
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}'); return false;", (object) string.Format((IFormatProvider) CultureInfo.CurrentCulture, "StockMovementManagement.aspx?SupplierId={0}", new object[1]
      {
        (object) pintSupplierId
      }), (object) pstrtitle, (object) "1200px", (object) "600px");
    }
  }
}
