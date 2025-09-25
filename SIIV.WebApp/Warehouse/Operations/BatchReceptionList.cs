// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.BatchReceptionList
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class BatchReceptionList : Page
  {
    private int i_PlateTypeId = 0;
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtBatchNumber;
    protected FilteredTextBoxExtender txtBatchNumber_FilteredTextBoxExtender;
    protected TextBox txtDispatchNumber;
    protected FilteredTextBoxExtender txtDispatchNumber_FilteredTextBoxExtender;
    protected DropDownList wddBatchStatus;
    protected DropDownList wddReceptionStatus;
    protected Fecha wdpDateIni;
    protected Fecha wdpDateFin;
    protected DropDownList wddBatchType;
    protected Button wibSearch;
    protected GridView wdgBatchReception;
    protected Pager custPagerBatch;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.ViewState["i_PlateTypeId"] = (object) this.Request.QueryString["t"].ToString();
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.LoadParameters();
        this.LoadProductionBatchType();
        this.SetDatePicker();
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      string str = string.Empty;
      try
      {
        if (!string.IsNullOrEmpty(this.txtBatchNumber.Text))
        {
          if (this.txtBatchNumber.Text.Trim().Length != 10)
          {
            str = this.txtBatchNumber.Text.Trim();
            this.txtBatchNumber.Text = "";
            this.txtBatchNumber.Focus();
          }
          else
          {
            str = Convert.ToInt32(this.txtBatchNumber.Text.Trim().Substring(3), (IFormatProvider) CultureInfo.CurrentCulture).ToString((IFormatProvider) CultureInfo.CurrentCulture);
            this.txtBatchNumber.Text = "";
            this.txtBatchNumber.Focus();
          }
        }
        if (this.wdpDateFin.Value.Subtract(this.wdpDateIni.Value).Days > 30 && string.IsNullOrEmpty(this.txtBatchNumber.Text.Trim()))
          throw new HandledException(1, "Si el intervalo de fechas excede a 30 días </br> debe especificar un número de Lote");
        this.wddBatchStatus.SelectedItem.ToString();
        int num = this.Session["SystemUser"] != null ? (this.Session["SystemUser"] as SystemUser).i_LocationId : throw new HandledException(3, "Error de Sesión", "Error de Sesion 'SystemUser' en BatchReceptionList");
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        string pstrBatchId = str;
        string text = this.txtDispatchNumber.Text;
        int pintStatus = int.Parse(this.wddBatchStatus.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        int pintReceptionStatus = int.Parse(this.wddReceptionStatus.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        DateTime dateTime = Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrBeginDate = dateTime.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture);
        dateTime = Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrEndDate = dateTime.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture);
        int pintLocationId = num;
        int iPlateTypeId = this.i_PlateTypeId;
        int pintBatchTypeId = int.Parse(this.wddBatchType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        this.SearchBatchReceptionList(pstrBatchId, text, pintStatus, pintReceptionStatus, pstrBeginDate, pstrEndDate, pintLocationId, iPlateTypeId, pintBatchTypeId, true);
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
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
        string empty = string.Empty;
        string str = this.txtBatchNumber.Text.Trim();
        int num = this.Session["SystemUser"] != null ? (this.Session["SystemUser"] as SystemUser).i_LocationId : throw new HandledException(3, "Error de Sesión", "Error de Sesion 'SystemUser' en BatchReceptionList");
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        string pstrBatchId = str;
        string text = this.txtDispatchNumber.Text;
        int pintStatus = int.Parse(this.wddBatchStatus.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        int pintReceptionStatus = int.Parse(this.wddReceptionStatus.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        DateTime dateTime = Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrBeginDate = dateTime.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture);
        dateTime = Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrEndDate = dateTime.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture);
        int pintLocationId = num;
        int iPlateTypeId = this.i_PlateTypeId;
        int pintBatchTypeId = int.Parse(this.wddBatchType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        this.SearchBatchReceptionList(pstrBatchId, text, pintStatus, pintReceptionStatus, pstrBeginDate, pstrEndDate, pintLocationId, iPlateTypeId, pintBatchTypeId, false);
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void wdgBatchReception_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        Batch batch = new Batch();
        Batch currentBatch = this.GetCurrentBatch(Convert.ToInt32(e.CommandArgument));
        if (e.CommandName == "SelectBatch")
        {
          this.Session["sBatch"] = (object) currentBatch;
          if (currentBatch.i_BatchTypeId.GetValueOrDefault() == 1)
          {
            this.Response.Redirect("BatchReceptionItem.aspx?i_PlateTypeId=1", false);
          }
          else
          {
            int? iBatchTypeId = currentBatch.i_BatchTypeId;
            if (iBatchTypeId.GetValueOrDefault() == 2)
            {
              this.Response.Redirect("BatchReceptionItemClaim.aspx?i_PlateTypeId=1", false);
            }
            else
            {
              iBatchTypeId = currentBatch.i_BatchTypeId;
              if (iBatchTypeId.GetValueOrDefault() == 7)
              {
                this.Response.Redirect("BatchReceptionItem.aspx?i_PlateTypeId=12", false);
              }
              else
              {
                iBatchTypeId = currentBatch.i_BatchTypeId;
                if (iBatchTypeId.GetValueOrDefault() != 8)
                  return;
                this.Response.Redirect("BatchReceptionItemClaim.aspx?i_PlateTypeId=12", false);
              }
            }
          }
        }
        else if (e.CommandName == "SelectLocation")
        {
          using (ReportDocument reportDocument = new ReportDocument())
          {
            string filename = this.Server.MapPath("../Rpt/PositionBatchPlateReport.rpt");
            reportDocument.Load(filename);
            DataTable batchPlateReport = new ShelfQueriesBL().GetPositionBatchPlateReport(currentBatch.i_BatchId);
            if (batchPlateReport.Rows.Count <= 0)
              throw new HandledException(1, "No se encontró información");
            reportDocument.SetDataSource(batchPlateReport);
            reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "Posición de Placas");
          }
        }
        else
        {
          if (!(e.CommandName == "SelectLocationExcel"))
            return;
          using (ReportDocument reportDocument = new ReportDocument())
          {
            string filename = this.Server.MapPath("../Rpt/PositionBatchPlateReport.rpt");
            reportDocument.Load(filename);
            DataTable batchPlateReport = new ShelfQueriesBL().GetPositionBatchPlateReport(currentBatch.i_BatchId);
            if (batchPlateReport.Rows.Count <= 0)
              throw new HandledException(1, "No se encontró información");
            reportDocument.SetDataSource(batchPlateReport);
            reportDocument.ExportToHttpResponse(ExportFormatType.Excel, this.Response, true, "Posición de Placas");
          }
        }
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void lnkSelectBatchId_Click(object sender, EventArgs e) => this.HidePopup();

    protected void LoadParameters()
    {
      try
      {
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) ("" + SystemParameterGroups.BatchStatus.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ", " + SystemParameterGroups.ReceiptStatus.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
          (object) "",
          (object) "1",
          (object) "1"
        });
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["i_GroupId"].ToString() == SystemParameterGroups.BatchStatus.ToString((IFormatProvider) CultureInfo.CurrentCulture))
              this.wddBatchStatus.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
            if (row["i_GroupId"].ToString() == SystemParameterGroups.ReceiptStatus.ToString((IFormatProvider) CultureInfo.CurrentCulture))
              this.wddReceptionStatus.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          }
        }
        this.wddBatchStatus.Items.Insert(0, new ListItem("- Seleccione - ", "-1"));
        this.wddBatchStatus.SelectedValue = "-1";
        this.wddReceptionStatus.Items.Insert(0, new ListItem("- Seleccione - ", "-1"));
        this.wddReceptionStatus.SelectedValue = "-1";
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void LoadProductionBatchType()
    {
      try
      {
        SystemParameterQueriesBL parameterQueriesBl = new SystemParameterQueriesBL();
        ArrayList arrFilter = (ArrayList) null;
        string str = "" + SystemParameterGroups.ProductionBatchType.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        if (this.i_PlateTypeId == 1)
          arrFilter = new ArrayList()
          {
            (object) str,
            (object) "1,2",
            (object) "1",
            (object) "1"
          };
        else if (this.i_PlateTypeId == 12)
          arrFilter = new ArrayList()
          {
            (object) str,
            (object) "7,8",
            (object) "1",
            (object) "1"
          };
        DataTable dataTable = parameterQueriesBl.GetbyFilter(arrFilter);
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["i_GroupId"].ToString() == SystemParameterGroups.ProductionBatchType.ToString((IFormatProvider) CultureInfo.CurrentCulture))
              this.wddBatchType.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          }
        }
        this.wddBatchType.Items.Insert(0, new ListItem("- Seleccione - ", "-1"));
        this.wddBatchType.SelectedValue = "-1";
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SetDatePicker()
    {
      this.wdpDateIni.Value = DateTime.Now.AddDays(-29.0);
      this.wdpDateFin.Value = DateTime.Now;
    }

    private void SearchBatchReceptionList(
      string pstrBatchId,
      string pstrDispatchId,
      int pintStatus,
      int pintReceptionStatus,
      string pstrBeginDate,
      string pstrEndDate,
      int pintLocationId,
      int pintPlateTypeId,
      int pintBatchTypeId,
      bool pboolLoadPager)
    {
      try
      {
        pstrBatchId = string.IsNullOrEmpty(pstrBatchId) ? "-1" : pstrBatchId;
        pstrDispatchId = string.IsNullOrEmpty(pstrDispatchId) ? "-1" : this.txtDispatchNumber.Text;
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerBatch.CurrentPageNumber;
        int pintMaxRows = this.custPagerBatch.CurrentPageSize == 0 ? 10 : this.custPagerBatch.CurrentPageSize;
        int pintTotalRows;
        DataTable batchReceptionBy = new BatchReceptionQueriesBL().GetBatchReceptionBy(int.Parse(pstrBatchId, (IFormatProvider) CultureInfo.CurrentCulture), int.Parse(pstrDispatchId, (IFormatProvider) CultureInfo.CurrentCulture), pintStatus, pintReceptionStatus, pstrBeginDate, pstrEndDate, pintLocationId, pintPlateTypeId, pintBatchTypeId, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        if (batchReceptionBy == null || batchReceptionBy.Rows.Count == 0)
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados."));
        int num = pintTotalRows;
        this.wdgBatchReception.DataSource = (object) batchReceptionBy;
        this.wdgBatchReception.DataBind();
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

    private Batch GetCurrentBatch(int index)
    {
      try
      {
        Batch currentBatch = new Batch();
        int num1 = this.Session["SystemUser"] != null ? (this.Session["SystemUser"] as SystemUser).i_LocationId : throw new HandledException(3, "Error de Sesión", "Error de Sesion 'SystemUser' en BatchReceptionList");
        GridViewRow row = this.wdgBatchReception.Rows[index];
        if (row == null)
          throw new HandledException(4, "Error de Selección", "Error de seleccion 'wdgBatchReception' en BatchReceptionList");
        currentBatch.i_BatchId = int.Parse(this.wdgBatchReception.DataKeys[index]["i_BatchId"].ToString());
        int num2 = string.IsNullOrEmpty(row.Cells[7].Text) ? 1 : (row.Cells[7].Text.Length < 7 ? 1 : 0);
        currentBatch.d_ReceptionDate = num2 == 0 ? new DateTime?(Convert.ToDateTime(row.Cells[7].Text.ToString())) : new DateTime?();
        currentBatch.v_BatchStatus = row.Cells[5].Text.ToString();
        currentBatch.v_ItemsMax = row.Cells[3].Text.ToString();
        currentBatch.i_DispatchId = new int?(Convert.ToInt32(row.Cells[2].Text.ToString()));
        int num3 = string.IsNullOrEmpty(row.Cells[8].Text) ? 1 : (row.Cells[8].Text.Length > 7 ? 1 : 0);
        currentBatch.d_EntryToDispatch = num3 == 0 ? new DateTime?(Convert.ToDateTime(row.Cells[8].Text.ToString())) : new DateTime?();
        currentBatch.v_ReceptionStatus = row.Cells[6].Text.ToString();
        currentBatch.v_ChekItems = row.Cells[4].Text.ToString();
        currentBatch.i_BatchTypeId = new int?(Convert.ToInt32(this.wdgBatchReception.DataKeys[index]["i_BatchTypeId"].ToString()));
        currentBatch.i_LocationId = new int?(num1);
        currentBatch.i_Status = new int?(Convert.ToInt32(this.wdgBatchReception.DataKeys[index]["i_BatchStatus"].ToString()));
        currentBatch.i_ReceptionStatus = new int?(Convert.ToInt32(this.wdgBatchReception.DataKeys[index]["i_ReceptionStatus"].ToString()));
        return currentBatch;
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

    protected void wdgBatchReception_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgBatchReception_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
  }
}
