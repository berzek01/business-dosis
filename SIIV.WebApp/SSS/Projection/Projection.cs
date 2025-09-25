// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.SSS.Projection.Projection
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.SSS.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Data;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.SSS.Projection
{
  public class Projection : Page
  {
    protected HiddenField HiddenField1;
    protected UpdatePanel UpdatePanel1;
    protected Label Label1;
    protected Fecha wdpStartDate;
    protected Label Label2;
    protected Fecha wdpEndDate;
    protected Button wibSearch0;
    protected GridView wdgList;
    protected Pager custPagerPO;
    protected Label lblMessage;
    protected UpdatePanel UpdatePanel2;
    protected CheckBox chkEdit;
    protected Fecha wdpDateEdit;
    protected GridView wdgListDetail;
    protected Label lblCountDetail;
    protected Button wibSave;
    protected Button wibReturn;
    protected Label lblMessageDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.SetDatePicker();
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchPO();

    protected void custPagerPO_PageChanged(object sender, CustomPageChangeArgs e)
    {
      this.SearchPOList(this.wdpStartDate.Text, this.wdpEndDate.Text, false);
    }

    protected void wdgList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      GridViewRow row = this.wdgList.Rows[Convert.ToInt32(e.CommandArgument)];
      if (!(e.CommandName == "Read"))
        return;
      string date = Convert.ToDateTime(row.Cells[1].Text).ToString("yyyyMMdd");
      SSSQueriesBL sssQueriesBl = new SSSQueriesBL();
      DataTable dataTable = new DataTable();
      DataTable byDate = sssQueriesBl.ProjectionGetByDate(date);
      if (byDate.Rows.Count > 0)
      {
        this.wdgListDetail.DataSource = (object) byDate;
        this.wdgListDetail.DataBind();
        this.wdpDateEdit.Value = Convert.ToDateTime(row.Cells[1].Text);
        this.wdpDateEdit.Enabled = false;
        this.lblMessage.Visible = false;
        this.lblCountDetail.Text = Constants.SEARCHRESULT_OK.Replace("XX", byDate.Rows.Count.ToString());
        string script = UtilDA.ActiveTabIndex("tabs", 1, "0");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        this.lblMessageDetail.Visible = false;
        this.chkEdit.Checked = false;
      }
      else
        Message.SetMessage(this.lblMessageDetail, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
    }

    protected void chkEdit_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkEdit.Checked)
        this.wdpDateEdit.Enabled = true;
      else
        this.wdpDateEdit.Enabled = false;
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      bool flag = false;
      using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
      {
        Timeout = new TimeSpan(1, 1, 1)
      }))
      {
        foreach (GridViewRow row in this.wdgListDetail.Rows)
          new SSSManagementBL().ProjectionUpdateOrder(Convert.ToInt32(this.wdgListDetail.DataKeys[row.RowIndex]["i_ProductId"].ToString()), Convert.ToInt32(((TextBox) row.FindControl("txtCant")).Text), !this.chkEdit.Checked ? Convert.ToDateTime(this.wdgListDetail.Rows[row.RowIndex].Cells[2].Text).ToString("yyyyMMdd") : Convert.ToDateTime(this.wdpDateEdit.Value).ToString("yyyyMMdd"), Convert.ToDateTime(this.wdgListDetail.Rows[row.RowIndex].Cells[2].Text).ToString("yyyyMMdd"));
        transactionScope.Complete();
        flag = true;
      }
      if (!flag)
        return;
      Message.SetMessage(this.lblMessageDetail, enmMessageType.Success, "Se registró correctamente el pedido");
    }

    protected void wibReturn_Click(object sender, EventArgs e)
    {
      string script = UtilDA.ActiveTabIndex("tabs", 0, "1");
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      this.wibSearch_Click((object) null, (EventArgs) null);
    }

    private void SetDatePicker()
    {
      this.wdpStartDate.Value = DateTime.Now;
      this.wdpEndDate.Value = DateTime.Now;
    }

    private void SearchPO()
    {
      this.lblMessage.Visible = false;
      try
      {
        this.SearchPOList(this.wdpStartDate.Text, this.wdpEndDate.Text, true);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, ex.Message);
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SearchPOList(string dFecIni, string dFecFin, bool pboolLoadPager)
    {
      try
      {
        int pintstartRowIndex = pboolLoadPager ? 1 : this.custPagerPO.CurrentPageNumber;
        int pintmaxRows = this.custPagerPO.CurrentPageSize == 0 ? 10 : this.custPagerPO.CurrentPageSize;
        int pinttotalRows;
        DataTable all = new SSSQueriesBL().ProjectionGetAll(dFecIni, dFecFin, pintstartRowIndex, pintmaxRows, out pinttotalRows);
        if (all == null || all.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
          this.HidePopup();
        }
        else
          this.lblMessage.Visible = false;
        int num = pinttotalRows;
        this.wdgList.DataSource = (object) all;
        this.wdgList.DataBind();
        this.custPagerPO.TotalPages = num % pintmaxRows == 0 ? num / pintmaxRows : num / pintmaxRows + 1;
        this.custPagerPO.TotalRecordCount = pinttotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerPO.LoadPager();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, "Error... Consulte con el Administrador. " + ex.Message);
      }
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
