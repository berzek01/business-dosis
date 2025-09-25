// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.VerifyBatchReceptionClaimDetail
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class VerifyBatchReceptionClaimDetail : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected GridView wdgWastage;
    protected Pager custPagerVerBatchRecClaimDetList;
    protected Label lblRecordCount;
    protected Button wibSave;
    protected Button wibReturn;
    protected Label lblMsg;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMsg.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.SearchDetail();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }

    protected void wdgWastage_InitializeRow(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowIndex < 0)
          return;
        ImageButton control = e.Row.Cells[2].FindControl("ibtCheck") as ImageButton;
        DataRow row = ((DataRowView) e.Row.DataItem).Row;
        for (int index = 0; index < 2; ++index)
          e.Row.Cells[index].CssClass = "Test2";
        if (Convert.ToInt32(row["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == 0)
        {
          control.ImageUrl = "~/Images/Design/checkbox_unchecked_16.png";
        }
        else
        {
          if (Convert.ToInt32(row["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) != 1)
            return;
          control.ImageUrl = "~/Images/Design/checkbox_checked_16.png";
          for (int index = 0; index < 2; ++index)
            e.Row.Cells[index].CssClass = "Test";
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }

    protected void wdgWastage_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      if (!e.CommandName.Equals("Check", StringComparison.CurrentCulture))
        return;
      try
      {
        int int32_1 = Convert.ToInt32(e.CommandArgument);
        GridViewRow row1 = this.wdgWastage.Rows[int32_1];
        int int32_2 = Convert.ToInt32(this.wdgWastage.DataKeys[int32_1]["i_Item"].ToString());
        DataTable dataTable = this.ViewState["vsProductConposition"] as DataTable;
        dataTable.Columns["i_Status"].ReadOnly = false;
        foreach (DataRow row2 in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (Convert.ToInt32(row2["i_Item"], (IFormatProvider) CultureInfo.CurrentCulture) == int32_2)
          {
            row2["i_Status"] = Convert.ToInt32(row2["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) != 1 ? (object) 1 : (object) 0;
            break;
          }
        }
        this.ViewState["vsProductConposition"] = (object) dataTable;
        this.wdgWastage.DataSource = (object) dataTable;
        this.wdgWastage.DataBind();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }

    protected void wdgWastage_PageIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.SearchDetail();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      try
      {
        this.ValidateChecks();
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = this.ViewState["vsProductConposition"] as DataTable;
        if (!(this.Session["ProductAddWastageClainDetail"] is DataTable source))
          source = this.BuilDTPRoductAddWastageClainDetail();
        else if (source.Rows.Count > 0)
        {
          int intIdsReclamo = Convert.ToInt32(this.Request.QueryString["ids"], (IFormatProvider) CultureInfo.CurrentCulture);
          source = source.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => x["i_RequirementPlateId"].ToString() != intIdsReclamo.ToString())).CopyToDataTable<DataRow>();
        }
        if (this.Request.QueryString["ProductId"] != null)
        {
          int int32_1 = Convert.ToInt32(this.Request.QueryString["ProductId"], (IFormatProvider) CultureInfo.CurrentCulture);
          int int32_2 = Convert.ToInt32(this.Request.QueryString["ids"], (IFormatProvider) CultureInfo.CurrentCulture);
          foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
          {
            if (Convert.ToInt16(row["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == (short) 1)
            {
              source.Rows.Add((object) int32_1, (object) Convert.ToInt16(row["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture), (object) row["v_Name"].ToString(), (object) int32_2);
              this.PopupClose();
            }
          }
        }
        this.Session["ProductAddWastageClainDetail"] = (object) source;
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }

    protected void wibReturn_Click(object sender, EventArgs e) => this.PopupClose();

    protected void custPagerVerBatchRecClaimDetList_PageChanged(
      object sender,
      CustomPageChangeArgs e)
    {
      if (this.Request.QueryString["ProductId"] == null)
        return;
      this.SearchDetailList(Convert.ToInt32(this.Request.QueryString["ids"], (IFormatProvider) CultureInfo.CurrentCulture), 5, false);
    }

    private void SearchDetail()
    {
      try
      {
        if (this.Request.QueryString["ProductId"] == null)
          return;
        this.SearchDetailList(Convert.ToInt32(this.Request.QueryString["ids"], (IFormatProvider) CultureInfo.CurrentCulture), 5, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchDetailList(
      int pintRequirementPlateId,
      int pintClaimTypeId,
      bool pboolLoadPager)
    {
      try
      {
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerVerBatchRecClaimDetList.CurrentPageNumber;
        int pintMaxRows = this.custPagerVerBatchRecClaimDetList.CurrentPageSize == 0 ? 10 : this.custPagerVerBatchRecClaimDetList.CurrentPageSize;
        int pintTotalRows;
        DataTable requirementPlateOriginalBy = new BatchReceptionClaimQueriesBL().GetProductCompositionByRequirementPlateOriginalBy(pintRequirementPlateId, pintClaimTypeId, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        int num = pintTotalRows;
        this.wdgWastage.DataSource = (object) requirementPlateOriginalBy;
        this.wdgWastage.DataBind();
        this.ViewState["vsProductConposition"] = (object) requirementPlateOriginalBy;
        this.custPagerVerBatchRecClaimDetList.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
        this.custPagerVerBatchRecClaimDetList.TotalRecordCount = pintTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerVerBatchRecClaimDetList.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ValidateChecks()
    {
      try
      {
        DataTable dataTable = this.ViewState["vsProductConposition"] as DataTable;
        bool flag = false;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (Convert.ToInt32(row["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == 1)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          throw new HandledException(1, "Debe elejir por lo menos un producto de intercambio");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private DataTable BuilDTPRoductAddWastageClainDetail()
    {
      return new DataTable()
      {
        Columns = {
          "i_ProductId",
          "i_ComponentId",
          "v_Description",
          "i_RequirementPlateId"
        }
      };
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private void PopupClose()
    {
      string script = "PopupClose();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }
  }
}
