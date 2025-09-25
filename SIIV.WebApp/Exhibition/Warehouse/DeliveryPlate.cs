// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Warehouse.DeliveryPlate
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Warehouse
{
  public class DeliveryPlate : Page
  {
    private int i_PlateTypeId = 0;
    protected Label Label1;
    protected TextBox txtAssociatedNameSearch;
    protected TextBox txtPlateSearch;
    protected Button wibSearch;
    protected GridView wdgList;
    protected Pager custPagerDPlate;
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
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - DeliveryPlate.aspx");
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
      dtResult.Columns.Add("v_ReasonSocial", typeof (string));
      dtResult.Columns.Add("d_BankOperationDate", typeof (DateTime));
      dtResult.Columns.Add("f_Quantity", typeof (float));
      dtResult.Columns.Add("f_Delivery", typeof (float));
      dtResult.Columns.Add("v_PaymentCode", typeof (string));
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchDPlate();

    protected void wdgList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        int int32 = Convert.ToInt32(e.CommandArgument);
        if (this.wdgList.Rows[int32] == null)
          throw new HandledException(4, "Error de selección.", "'wdgList' - DeliveryPlate.aspx");
        if (!(e.CommandName == "Select"))
          return;
        string str = this.wdgList.DataKeys[int32]["i_RequirementId"].ToString();
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.Response.Redirect("DeliveryPlateItem.aspx?i_RequirementId=" + str + "&t=" + this.i_PlateTypeId.ToString());
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

    protected void custPagerDPlate_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        DataTable dataTable = new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(this.i_PlateTypeId);
        this.SearchDPlateList(this.txtAssociatedNameSearch.Text.TrimEnd(), Convert.ToInt32(dataTable.Rows[0]["i_WarehouseId"].ToString()), this.txtPlateSearch.Text.TrimEnd(), false);
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

    private void SearchDPlate()
    {
      try
      {
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        DataTable dataTable = new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(this.i_PlateTypeId);
        this.SearchDPlateList(this.txtAssociatedNameSearch.Text.TrimEnd(), Convert.ToInt32(dataTable.Rows[0]["i_WarehouseId"].ToString()), this.txtPlateSearch.Text.TrimEnd(), true);
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

    private void SearchDPlateList(
      string pv_AssociatedName,
      int pi_WarehouseId,
      string pv_Plate,
      bool pboolLoadPager)
    {
      try
      {
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerDPlate.CurrentPageNumber;
        int pintMaxRows = this.custPagerDPlate.CurrentPageSize == 0 ? 10 : this.custPagerDPlate.CurrentPageSize;
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        int i_ProductId = this.SetProductId(this.i_PlateTypeId);
        int pintTotalRows;
        DataTable all = new WarehouseExhibitionQueriesBL().SpecialPlateDeliveryPlateGetAll(pv_AssociatedName, i_ProductId, pi_WarehouseId, pv_Plate, this.i_PlateTypeId, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        if (all == null || all.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
          this.HidePopup();
        }
        int num = pintTotalRows;
        this.wdgList.DataSource = (object) all;
        this.wdgList.DataBind();
        this.custPagerDPlate.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
        this.custPagerDPlate.TotalRecordCount = pintTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerDPlate.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
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

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
