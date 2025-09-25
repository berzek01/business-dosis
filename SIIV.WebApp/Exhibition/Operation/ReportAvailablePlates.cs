// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Operation.ReportAvailablePlates
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Operation
{
  public class ReportAvailablePlates : Page
  {
    private int i_PlateTypeId = 0;
    protected UpdatePanel UpdatePanel1;
    protected Label Label19;
    protected DropDownList wddAssociated;
    protected Label Label1;
    protected TextBox txtPlaca;
    protected FilteredTextBoxExtender txtPlaca_FilteredTextBoxExtender;
    protected Label Label2;
    protected DropDownList wddStatus;
    protected HtmlTable TblFecha;
    protected CheckBox chkDate;
    protected Label Label5;
    protected Fecha wdpStartDate;
    protected Label Label8;
    protected Fecha wdpEndDate;
    protected Button wibSearch;
    protected Button wibExport;
    protected GridView wdgList;
    protected GridView wdgListNew;
    protected Pager custPagerAP;
    protected Label lblCount;
    protected Label lblMessage;
    protected Button Button1;

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
        this.LoadParameters();
        this.SetDatePicker();
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
      dtResult.Columns.Add("v_AssociatedName", typeof (string));
      dtResult.Columns.Add("v_Plate", typeof (string));
      dtResult.Columns.Add("v_SpecialPlateType", typeof (string));
      dtResult.Columns.Add("v_Description", typeof (string));
      dtResult.Columns.Add("Date", typeof (DateTime));
      dtResult.Columns.Add("PendingRenovation", typeof (string));
    }

    protected void wddStatus_SelectionChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.wddStatus.SelectedValue != "")
        {
          if (Convert.ToInt16(this.wddStatus.SelectedValue) == (short) 4)
            this.TblFecha.Style.Add("display", "block");
          else
            this.TblFecha.Style.Add("display", "none");
        }
        else
          this.TblFecha.Style.Add("display", "none");
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

    protected void chkDate_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.chkDate.Checked)
        {
          this.wdpStartDate.Enabled = true;
          this.wdpEndDate.Enabled = true;
          this.SetDatePicker();
        }
        else
        {
          this.wdpStartDate.Enabled = false;
          this.wdpEndDate.Enabled = false;
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

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchAvailablePlates();

    protected void wibExport_Click(object sender, EventArgs e)
    {
      try
      {
        this.ExportList();
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

    protected void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable objDataTable = (DataTable) this.Session["dtExport"];
        ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
        List<ClassColumns> classColumnsList = new List<ClassColumns>();
        foreach (DataControlField column in (StateManagedCollection) this.wdgList.Columns)
        {
          if (column.Visible && column.GetType().Name == "BoundField")
          {
            BoundField boundField = (BoundField) column;
            classColumnsList.Add(new ClassColumns(boundField.DataField, 1, (int) column.ControlStyle.Width.Value, boundField.HeaderText));
          }
        }
        exportToExcelDataGrid.clsTitle = classColumnsList;
        exportToExcelDataGrid.AgregarHojaLibro(objDataTable, "Listado Placas");
        exportToExcelDataGrid.CerrarLibro();
        byte[] buffer = exportToExcelDataGrid.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=ListadoPlacas.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
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

    protected void custPagerAP_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        string v_Plate = this.txtPlaca.Text.Trim();
        int int32_1 = Convert.ToInt32(this.wddAssociated.SelectedValue);
        int int32_2 = Convert.ToInt32(this.wddStatus.SelectedValue);
        DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
        DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
        int i_Flag = 1;
        if (!this.chkDate.Checked)
          i_Flag = 0;
        this.i_PlateTypeId = Convert.ToInt32(this.Request.QueryString["t"].ToString());
        int int32_3 = Convert.ToInt32(new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(this.i_PlateTypeId).Rows[0]["i_WarehouseId"].ToString());
        this.SearchAvailablePlatesList(int32_1, v_Plate, int32_2, dateTime1, dateTime2, i_Flag, this.i_PlateTypeId, int32_3, false);
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

    private void LoadParameters()
    {
      try
      {
        SystemParameterManagementBL parameterManagementBl = new SystemParameterManagementBL();
        AssociatedQueriesBL associatedQueriesBl = new AssociatedQueriesBL();
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - ReportAvailablePlates.aspx");
        this.i_PlateTypeId = Convert.ToInt32(this.Request.QueryString["t"].ToString());
        DataTable dataTable = new DataTable();
        this.FillAssociatedWdd(associatedQueriesBl.AssociatedList(systemUser.i_SystemUserId, 7, this.i_PlateTypeId));
        this.wddAssociated.Items.Insert(0, new ListItem("-- Todos --", "0"));
        this.wddAssociated.SelectedIndex = 0;
        string str = "" + SystemParameterGroups.StatusAvailabilityPlates.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        ArrayList pobj;
        if (this.i_PlateTypeId == 7)
          pobj = new ArrayList()
          {
            (object) str,
            (object) "",
            (object) "1",
            (object) "1"
          };
        else
          pobj = new ArrayList()
          {
            (object) str,
            (object) "",
            (object) "1",
            (object) "1"
          };
        List<SIIV.BE.SystemParameter> systemParameterList = parameterManagementBl.Get((object) pobj);
        this.wddStatus.Items.Clear();
        foreach (SIIV.BE.SystemParameter systemParameter in systemParameterList)
        {
          if (systemParameter.i_GroupId == SystemParameterGroups.StatusAvailabilityPlates)
            this.wddStatus.Items.Add(new ListItem(systemParameter.v_Description, systemParameter.i_ParameterId.ToString()));
        }
        this.wddStatus.Items.Insert(0, new ListItem("-- Todos --", "0"));
        this.wddStatus.SelectedValue = "0";
        if (this.i_PlateTypeId == 7)
          this.wdgList.Columns[5].Visible = true;
        else
          this.wdgList.Columns[5].Visible = false;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void FillAssociatedWdd(DataTable dt_Result)
    {
      try
      {
        if (dt_Result == null || dt_Result.Rows.Count == 0)
          return;
        this.wddAssociated.DataSource = (object) dt_Result;
        this.wddAssociated.DataTextField = "v_Alias";
        this.wddAssociated.DataValueField = "i_SystemUserId";
        this.wddAssociated.DataBind();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SetDatePicker()
    {
      this.wdpStartDate.Value = DateTime.Now.AddMonths(-1);
      this.wdpEndDate.Value = DateTime.Now;
    }

    private void SearchAvailablePlates()
    {
      try
      {
        string v_Plate = this.txtPlaca.Text.Trim();
        int int32_1 = Convert.ToInt32(this.wddAssociated.SelectedValue);
        int int32_2 = Convert.ToInt32(this.wddStatus.SelectedValue);
        DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
        DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
        int i_Flag = 1;
        if (!this.chkDate.Checked)
          i_Flag = 0;
        this.i_PlateTypeId = Convert.ToInt32(this.Request.QueryString["t"].ToString());
        int int32_3 = Convert.ToInt32(new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(this.i_PlateTypeId).Rows[0]["i_WarehouseId"].ToString());
        this.SearchAvailablePlatesList(int32_1, v_Plate, int32_2, dateTime1, dateTime2, i_Flag, this.i_PlateTypeId, int32_3, true);
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

    private void SearchAvailablePlatesList(
      int i_SystemUserId,
      string v_Plate,
      int i_Status,
      DateTime d_StartDate,
      DateTime d_EndDate,
      int i_Flag,
      int i_PlateTypeId,
      int intWarehouseId,
      bool pboolLoadPager)
    {
      try
      {
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerAP.CurrentPageNumber;
        int pintMaxRows = this.custPagerAP.CurrentPageSize == 0 ? 10 : this.custPagerAP.CurrentPageSize;
        int pintTotalRows;
        DataTable dataTable = new WarehouseExhibitionQueriesBL().SpecialPlateAvailablePlatesGet(i_SystemUserId, v_Plate, i_Status, d_StartDate, d_EndDate, i_Flag, i_PlateTypeId, intWarehouseId, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
          this.HidePopup();
        }
        else
        {
          this.lblMessage.Visible = false;
          this.wibExport.Enabled = true;
        }
        this.ViewState["dtresult"] = (object) dataTable;
        int num = pintTotalRows;
        this.wdgList.DataSource = (object) dataTable;
        this.wdgList.DataBind();
        this.custPagerAP.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
        this.custPagerAP.TotalRecordCount = pintTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerAP.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ExportList()
    {
      try
      {
        string v_Plate = this.txtPlaca.Text.Trim();
        int int32_1 = Convert.ToInt32(this.wddAssociated.SelectedValue);
        int int32_2 = Convert.ToInt32(this.wddStatus.SelectedValue);
        DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
        DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
        int i_Flag = 1;
        if (!this.chkDate.Checked)
          i_Flag = 0;
        this.i_PlateTypeId = Convert.ToInt32(this.Request.QueryString["t"].ToString());
        int int32_3 = Convert.ToInt32(new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(this.i_PlateTypeId).Rows[0]["i_WarehouseId"].ToString());
        DataTable dataTable = new DataTable();
        this.Session["dtExport"] = (object) new WarehouseExhibitionQueriesBL().SpecialPlateAvailablePlatesGet(int32_1, v_Plate, int32_2, dateTime1, dateTime2, i_Flag, this.i_PlateTypeId, int32_3, 0, 0, out int _);
        this.Export();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void Export()
    {
      string script = "Export();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
