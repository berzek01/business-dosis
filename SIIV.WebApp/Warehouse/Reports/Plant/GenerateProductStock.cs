// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Reports.Plant.GenerateProductStock
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Reports.Plant
{
  public class GenerateProductStock : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected Label Label2;
    protected TextBox txtProductSearch;
    protected Button wibSearch;
    protected Button wibLoad;
    protected GridView wdgList;
    protected Button btnReturnPopupConfirmation;
    protected Pager custPager;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        ;
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.Search();

    protected void wibLoad_Click(object sender, EventArgs e)
    {
      this.CreatePopUpServer("Importar Excel", "LoadProductStock.aspx", "850px", "565px");
    }

    protected void custPager_PageChanged(object sender, CustomPageChangeArgs e)
    {
      string pstrProduct = string.Empty;
      if (this.txtProductSearch.Text != "")
        pstrProduct = this.txtProductSearch.Text;
      this.SearchList(pstrProduct, false);
    }

    private void Search()
    {
      this.lblMessage.Visible = false;
      string pstrProduct = string.Empty;
      try
      {
        if (this.txtProductSearch.Text != "")
          pstrProduct = this.txtProductSearch.Text;
        this.SearchList(pstrProduct, true);
        this.HidePopup();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, ex.Message);
        this.HidePopup();
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SearchList(string pstrProduct, bool pboolLoadPager)
    {
      int startRowIndex = pboolLoadPager ? 1 : this.custPager.CurrentPageNumber;
      int maxRows = this.custPager.CurrentPageSize == 0 ? 10 : this.custPager.CurrentPageSize;
      int pinttotalRows;
      DataTable dataTable = new ProductWarehouseQueriesBL().SearchProductStock(pstrProduct, startRowIndex, maxRows, out pinttotalRows);
      if (dataTable == null || dataTable.Rows.Count == 0)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
        this.HidePopup();
      }
      else
        this.lblMessage.Visible = false;
      int num = pinttotalRows;
      this.wdgList.DataSource = (object) dataTable;
      this.wdgList.DataBind();
      this.custPager.TotalPages = num % maxRows == 0 ? num / maxRows : num / maxRows + 1;
      this.custPager.TotalRecordCount = pinttotalRows;
      if (!pboolLoadPager)
        return;
      this.custPager.LoadPager();
    }

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
