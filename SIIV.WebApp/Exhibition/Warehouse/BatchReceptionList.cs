// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Warehouse.BatchReceptionList
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

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
namespace SIIV.WebApp.Exhibition.Warehouse
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
      try
      {
        string str = string.Empty;
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
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - BatchReceptionList.aspx");
        int iLocationId = systemUser.i_LocationId;
        Convert.ToInt32((object) systemUser.i_CompanyId);
        int int32 = Convert.ToInt32(this.Request.QueryString["t"].ToString());
        string pstrBatchId = str;
        string text = this.txtDispatchNumber.Text;
        int pintStatus = int.Parse(this.wddBatchStatus.SelectedValue);
        int pintReceptionStatus = int.Parse(this.wddReceptionStatus.SelectedValue);
        DateTime dateTime = Convert.ToDateTime(this.wdpDateIni.Value);
        string pstrBeginDate = dateTime.ToString("yyyyMMdd");
        dateTime = Convert.ToDateTime(this.wdpDateFin.Value);
        string pstrEndDate = dateTime.ToString("yyyyMMdd");
        int pintLocationId = iLocationId;
        int pintBatchTypeId = int.Parse(this.wddBatchType.SelectedValue);
        int i_SpecialPlateTypeId = int32;
        this.SearchBatchReceptionList(pstrBatchId, text, pintStatus, pintReceptionStatus, pstrBeginDate, pstrEndDate, pintLocationId, pintBatchTypeId, true, i_SpecialPlateTypeId);
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
        int num = this.Session["SystemUser"] != null ? (this.Session["SystemUser"] as SystemUser).i_LocationId : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - BatchReceptionList.aspx");
        string empty = string.Empty;
        string str = this.txtBatchNumber.Text.Trim();
        int int32 = Convert.ToInt32(this.Request.QueryString["t"].ToString());
        string pstrBatchId = str;
        string text = this.txtDispatchNumber.Text;
        int pintStatus = int.Parse(this.wddBatchStatus.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        int pintReceptionStatus = int.Parse(this.wddReceptionStatus.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        DateTime dateTime = Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrBeginDate = dateTime.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture);
        dateTime = Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrEndDate = dateTime.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture);
        int pintLocationId = num;
        int pintBatchTypeId = int.Parse(this.wddBatchType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        int i_SpecialPlateTypeId = int32;
        this.SearchBatchReceptionList(pstrBatchId, text, pintStatus, pintReceptionStatus, pstrBeginDate, pstrEndDate, pintLocationId, pintBatchTypeId, false, i_SpecialPlateTypeId);
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
          if (currentBatch.i_BatchTypeId.GetValueOrDefault() == 3)
          {
            this.Response.Redirect("~/Exhibicion/Warehouse/BatchReceptionItem.aspx?i_PlateTypeId=7", false);
          }
          else
          {
            int? iBatchTypeId = currentBatch.i_BatchTypeId;
            if (iBatchTypeId.GetValueOrDefault() == 4)
            {
              this.Response.Redirect("~/Exhibicion/Warehouse/BatchReceptionItemClaim.aspx", false);
            }
            else
            {
              iBatchTypeId = currentBatch.i_BatchTypeId;
              if (iBatchTypeId.GetValueOrDefault() == 5)
              {
                this.Response.Redirect("~/Exhibicion/Warehouse/BatchReceptionItem.aspx?i_PlateTypeId=11", false);
              }
              else
              {
                iBatchTypeId = currentBatch.i_BatchTypeId;
                if (iBatchTypeId.GetValueOrDefault() != 6)
                  return;
                this.Response.Redirect("~/Exhibicion/Warehouse/BatchReceptionItemClaim.aspx", false);
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
            if (batchPlateReport.Rows.Count > 0)
            {
              reportDocument.SetDataSource(batchPlateReport);
              reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "Posición de Placas");
            }
            else
              SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "Advertencia********<br>No se encontro Información"));
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
            if (batchPlateReport.Rows.Count > 0)
            {
              reportDocument.SetDataSource(batchPlateReport);
              reportDocument.ExportToHttpResponse(ExportFormatType.Excel, this.Response, true, "Posición de Placas");
            }
            else
              SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "Advertencia*****<br>No se encontro Información"));
          }
          this.HidePopup();
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
    }

    protected void lnkSelectBatchId_Click(object sender, EventArgs e) => this.HidePopup();

    protected void LoadParameters()
    {
      try
      {
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) ("" + SystemParameterGroups.BatchStatus.ToString() + ", " + SystemParameterGroups.ReceiptStatus.ToString()),
          (object) "",
          (object) "1",
          (object) "1"
        });
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["i_GroupId"].ToString() == SystemParameterGroups.BatchStatus.ToString())
              this.wddBatchStatus.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
            if (row["i_GroupId"].ToString() == SystemParameterGroups.ReceiptStatus.ToString())
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
        string str = "" + SystemParameterGroups.ProductionBatchType.ToString();
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        if (this.i_PlateTypeId == 7)
          arrFilter = new ArrayList()
          {
            (object) str,
            (object) "3,4",
            (object) "1",
            (object) "1"
          };
        else if (this.i_PlateTypeId == 11)
          arrFilter = new ArrayList()
          {
            (object) str,
            (object) "5,6",
            (object) "1",
            (object) "1"
          };
        DataTable dataTable = parameterQueriesBl.GetbyFilter(arrFilter);
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["i_GroupId"].ToString() == SystemParameterGroups.ProductionBatchType.ToString())
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
      int pintBatchTypeId,
      bool pboolLoadPager,
      int i_SpecialPlateTypeId)
    {
      try
      {
        pstrBatchId = pstrBatchId == string.Empty ? "-1" : pstrBatchId;
        pstrDispatchId = pstrDispatchId == string.Empty ? "-1" : this.txtDispatchNumber.Text;
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerBatch.CurrentPageNumber;
        int pintMaxRows = this.custPagerBatch.CurrentPageSize == 0 ? 10 : this.custPagerBatch.CurrentPageSize;
        if (this.Session["ApplicationId"] != null)
        {
          int int32 = Convert.ToInt32(this.Session["ApplicationId"]);
          int pintTotalRows;
          DataTable receptionByAplicationId = new BatchReceptionQueriesBL().GetBatchReceptionByAplicationId(int.Parse(pstrBatchId), int.Parse(pstrDispatchId), pintStatus, pintReceptionStatus, pstrBeginDate, pstrEndDate, pintLocationId, pintBatchTypeId, pintStartRowIndex, pintMaxRows, out pintTotalRows, int32, i_SpecialPlateTypeId);
          this.wdgBatchReception.DataSource = (object) receptionByAplicationId;
          this.wdgBatchReception.DataBind();
          int num = pintTotalRows;
          this.custPagerBatch.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
          this.custPagerBatch.TotalRecordCount = pintTotalRows;
          this.ViewState["dtexporta"] = (object) receptionByAplicationId;
        }
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
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - BatchReceptionList.aspx");
        int iLocationId = systemUser.i_LocationId;
        Convert.ToInt32((object) systemUser.i_CompanyId);
        GridViewRow row = this.wdgBatchReception.Rows[index];
        if (row == null)
          throw new HandledException(4, "Error de selección.", "'wdgBatchReception' - BatchReceptionList.aspx");
        currentBatch.i_BatchId = Convert.ToInt32(this.wdgBatchReception.DataKeys[index]["i_BatchId"].ToString());
        currentBatch.d_ReceptionDate = !string.IsNullOrEmpty(row.Cells[7].Text) ? new DateTime?(Convert.ToDateTime(row.Cells[7].Text)) : new DateTime?();
        currentBatch.v_BatchStatus = row.Cells[4].Text;
        currentBatch.v_ItemsMax = row.Cells[2].Text;
        currentBatch.i_DispatchId = new int?(Convert.ToInt32(row.Cells[1].Text));
        currentBatch.d_EntryToDispatch = !string.IsNullOrEmpty(row.Cells[6].Text) ? new DateTime?(Convert.ToDateTime(row.Cells[7].Text)) : new DateTime?();
        currentBatch.v_ReceptionStatus = row.Cells[5].Text;
        currentBatch.v_ChekItems = row.Cells[3].Text;
        currentBatch.i_BatchTypeId = new int?(Convert.ToInt32(this.wdgBatchReception.DataKeys[index]["i_BatchTypeId"].ToString()));
        currentBatch.i_LocationId = new int?(iLocationId);
        currentBatch.i_Status = new int?(Convert.ToInt32(this.wdgBatchReception.DataKeys[index]["i_BatchStatus"].ToString()));
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
  }
}
