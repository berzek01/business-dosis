// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.VerifyProductsWastage
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
namespace SIIV.WebApp.Warehouse.Operations
{
  public class VerifyProductsWastage : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtBatch;
    protected TextBox txtDispachId;
    protected Fecha wdpDateIni;
    protected Fecha wdpDateFin;
    protected Button wibSearch;
    protected GridView wdgWastage;
    protected Pager custPagerBatch;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      this.SetDatePicker();
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        this.SearchProducts();
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

    protected void wdgWastage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        GridViewRow row = this.wdgWastage.Rows[Convert.ToInt32(e.CommandArgument)];
        if (!(e.CommandName == "Edit"))
          return;
        int num1 = int.Parse(row.Cells[1].Text);
        int num2 = int.Parse(row.Cells[2].Text);
        int num3 = int.Parse(this.wdgWastage.DataKeys[row.RowIndex]["i_WarehouseId"].ToString());
        int num4 = int.Parse(this.wdgWastage.DataKeys[row.RowIndex]["i_MotiveMovementId"].ToString());
        this.Session["i_DispatchId"] = (object) num1;
        this.Session["i_BatchId"] = (object) num2;
        this.Session["i_WarehouseId"] = (object) num3;
        this.Session["i_MotiveMovementId"] = (object) num4;
        string empty = string.Empty;
        this.CreatePopUpServer("Detalle del Reclamo", "../../Warehouse/Operations/VerifyProductsWastageDetail.aspx", "700px", "440px");
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

    protected void wdgWastage_PageIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.SearchProducts();
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

    protected void custPagerBatch_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        int pintBatchId = 0;
        int pintDispatchId = 0;
        if (!string.IsNullOrEmpty(this.txtBatch.Text))
          pintBatchId = Convert.ToInt32(this.txtBatch.Text, (IFormatProvider) CultureInfo.CurrentCulture);
        if (!string.IsNullOrEmpty(this.txtDispachId.Text))
          pintDispatchId = Convert.ToInt32(this.txtDispachId.Text, (IFormatProvider) CultureInfo.CurrentCulture);
        DateTime dateTime1 = Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        DateTime dateTime2 = Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        this.SearchProductsList(pintBatchId, pintDispatchId, dateTime1, dateTime2, false);
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

    private void SetDatePicker()
    {
      this.wdpDateIni.Value = DateTime.Now.AddMonths(-1);
      this.wdpDateFin.Value = DateTime.Now;
    }

    private void SearchProducts()
    {
      try
      {
        int pintBatchId = 0;
        int pintDispatchId = 0;
        if (!string.IsNullOrEmpty(this.txtBatch.Text))
          pintBatchId = Convert.ToInt32(this.txtBatch.Text, (IFormatProvider) CultureInfo.CurrentCulture);
        if (!string.IsNullOrEmpty(this.txtDispachId.Text))
          pintDispatchId = Convert.ToInt32(this.txtDispachId.Text, (IFormatProvider) CultureInfo.CurrentCulture);
        DateTime dateTime1 = Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        DateTime dateTime2 = Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        this.SearchProductsList(pintBatchId, pintDispatchId, dateTime1, dateTime2, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchProductsList(
      int pintBatchId,
      int pintDispatchId,
      DateTime pdStartDate,
      DateTime pdEndDate,
      bool pboolLoadPager)
    {
      try
      {
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerBatch.CurrentPageNumber;
        int pintMaxRows = this.custPagerBatch.CurrentPageSize == 0 ? 10 : this.custPagerBatch.CurrentPageSize;
        int pintTotalRows;
        DataTable dataTable = new WarehouseQueriesBL().WarehouseWastageRead(pintBatchId, pintDispatchId, pdStartDate, pdEndDate, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        int num = pintTotalRows;
        if (dataTable.Rows.Count == 0)
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
        this.wdgWastage.DataSource = (object) dataTable;
        this.wdgWastage.DataBind();
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

    protected void wdgWastage_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void wdgWastage_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }
  }
}
