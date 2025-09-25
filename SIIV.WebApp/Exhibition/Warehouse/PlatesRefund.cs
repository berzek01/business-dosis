// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Warehouse.PlatesRefund
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Warehouse
{
  public class PlatesRefund : Page
  {
    private int i_PlateTypeId = 0;
    protected UpdatePanel UpdatePanel1;
    protected Label Label1;
    protected TextBox txtAssociatedNameSearch;
    protected TextBox txtPlateSearch;
    protected Button wibSearch;
    protected GridView wdgList;
    protected Pager custPagerPRefund;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
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
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    private void TableColumns(DataTable dtResult)
    {
      dtResult.Columns.Add("i_RequirementId", typeof (int));
      dtResult.Columns.Add("i_RequirementPlateId", typeof (int));
      dtResult.Columns.Add("i_ProductId", typeof (int));
      dtResult.Columns.Add("v_PlateNew", typeof (string));
      dtResult.Columns.Add("v_ReasonSocial", typeof (string));
      dtResult.Columns.Add("v_Name", typeof (string));
      dtResult.Columns.Add("v_Status", typeof (string));
      dtResult.Columns.Add("d_DateRegister", typeof (DateTime));
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchDRefund();

    protected void wdgList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        int int32 = Convert.ToInt32(e.CommandArgument);
        if (this.wdgList.Rows[int32] == null)
          throw new HandledException(4, "Error de selección.", "'wdgList' - PlatesRefund.aspx");
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        if (!(e.CommandName == "Select"))
          return;
        this.Response.Redirect("PlatesRefundItem.aspx?i_RequirementPlateId=" + this.wdgList.DataKeys[int32]["i_RequirementPlateId"].ToString() + "&t=" + this.i_PlateTypeId.ToString());
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

    protected void custPagerPRefund_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        DataTable dataTable = new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(this.i_PlateTypeId);
        this.SearchDRefundList(this.txtAssociatedNameSearch.Text.TrimEnd(), Convert.ToInt32(dataTable.Rows[0]["i_WarehouseId"].ToString()), this.txtPlateSearch.Text.TrimEnd(), false);
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

    private void SearchDRefund()
    {
      try
      {
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        DataTable dataTable = new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(this.i_PlateTypeId);
        this.SearchDRefundList(this.txtAssociatedNameSearch.Text.TrimEnd(), Convert.ToInt32(dataTable.Rows[0]["i_WarehouseId"].ToString()), this.txtPlateSearch.Text.TrimEnd(), true);
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

    private void SearchDRefundList(
      string pv_AssociatedName,
      int pi_WarehouseId,
      string pv_Plate,
      bool pboolLoadPager)
    {
      try
      {
        int startRowIndex = pboolLoadPager ? 1 : this.custPagerPRefund.CurrentPageNumber;
        int maxRows = this.custPagerPRefund.CurrentPageSize == 0 ? 10 : this.custPagerPRefund.CurrentPageSize;
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        int i_ProductId = this.SetProductId(this.i_PlateTypeId);
        int totalRows;
        DataTable all = new WarehouseExhibitionQueriesBL().SpecialPlateRefundPlateGetAll(pv_AssociatedName, i_ProductId, pi_WarehouseId, pv_Plate, this.i_PlateTypeId, startRowIndex, maxRows, out totalRows);
        if (all == null || all.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
          this.HidePopup();
        }
        int num = totalRows;
        this.wdgList.DataSource = (object) all;
        this.wdgList.DataBind();
        this.custPagerPRefund.TotalPages = num % maxRows == 0 ? num / maxRows : num / maxRows + 1;
        this.custPagerPRefund.TotalRecordCount = totalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerPRefund.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private int SetProductId(int i_PlateTypeId)
    {
      try
      {
        int num = 0;
        WarehouseExhibitionQueriesBL exhibitionQueriesBl = new WarehouseExhibitionQueriesBL();
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = exhibitionQueriesBl.SpecialPlateWarehouseProductGet(i_PlateTypeId);
        switch (i_PlateTypeId)
        {
          case 6:
            num = 0;
            break;
          case 7:
            num = Convert.ToInt32(dataTable2.Rows[0]["i_Productid"].ToString());
            break;
          case 11:
            num = Convert.ToInt32(dataTable2.Rows[0]["i_Productid"].ToString());
            break;
        }
        return num;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
