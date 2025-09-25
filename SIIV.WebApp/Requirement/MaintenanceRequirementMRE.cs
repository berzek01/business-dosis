// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Requirement.MaintenanceRequirementMRE
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Requirement
{
  public class MaintenanceRequirementMRE : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected CheckBox chkDate;
    protected Label Label5;
    protected Fecha wdpStartDate;
    protected Label Label8;
    protected Fecha wdpEndDate;
    protected Label Label2;
    protected TextBox txtPlate;
    protected Label Label4;
    protected DropDownList wddStatus;
    protected Button wibSearch;
    protected GridView wdgList;
    protected Pager custPagerUQ;
    protected Button wibNew;
    protected Button wibExcel;
    protected Label lblMessage;
    protected Button Button1;
    protected HiddenField HiddenField1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.LoadParameters();
        this.SetDatePicker();
        this.chkDate.Checked = true;
        this.wdpStartDate.Enabled = true;
        this.wdpEndDate.Enabled = true;
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

    public void LoadParameters()
    {
      try
      {
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.RequirementStatusMRE.ToString(),
          (object) "",
          (object) "1",
          (object) "1"
        });
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["i_GroupId"].ToString() == SystemParameterGroups.RequirementStatusMRE.ToString())
              this.wddStatus.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          }
        }
        this.wddStatus.Items.Insert(0, new ListItem("-- Todos --", "-2"));
        this.wddStatus.SelectedValue = "-2";
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        this.Search();
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

    private void Search()
    {
      try
      {
        string v_Plate = this.txtPlate.Text.TrimEnd();
        int int32 = Convert.ToInt32(this.wddStatus.SelectedValue.ToString());
        DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
        DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
        int i_flag = 1;
        if (!this.chkDate.Checked)
          i_flag = 0;
        this.SearchMRE(v_Plate, int32, dateTime1, dateTime2, i_flag, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchMRE(
      string v_Plate,
      int i_Status,
      DateTime d_StartDate,
      DateTime d_EndDate,
      int i_flag,
      bool pboolLoadPager)
    {
      try
      {
        int startRowIndex = pboolLoadPager ? 1 : this.custPagerUQ.CurrentPageNumber;
        int maxRows = this.custPagerUQ.CurrentPageSize == 0 ? 10 : this.custPagerUQ.CurrentPageSize;
        int pinttotalRows;
        DataTable dataTable = new RequirementManagementBL().SearchMRE(v_Plate, i_Status, d_StartDate, d_EndDate, i_flag, startRowIndex, maxRows, out pinttotalRows);
        if (dataTable == null || dataTable.Rows.Count == 0)
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
        else
          this.wibExcel.Enabled = true;
        int num = pinttotalRows;
        this.wdgList.DataSource = (object) dataTable;
        this.wdgList.DataBind();
        this.custPagerUQ.TotalPages = num % maxRows == 0 ? num / maxRows : num / maxRows + 1;
        this.custPagerUQ.TotalRecordCount = pinttotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerUQ.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void custPagerUQ_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        string v_Plate = this.txtPlate.Text.TrimEnd();
        int int32 = Convert.ToInt32(this.wddStatus.SelectedValue.ToString());
        DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
        DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
        int i_flag = 1;
        if (!this.chkDate.Checked)
          i_flag = 0;
        this.SearchMRE(v_Plate, int32, dateTime1, dateTime2, i_flag, false);
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

    private void SetDatePicker()
    {
      this.wdpStartDate.Value = DateTime.Now;
      this.wdpEndDate.Value = DateTime.Now;
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void wibNew_Click(object sender, EventArgs e)
    {
      try
      {
        string text = this.txtPlate.Text;
        this.CreatePopUpServer("REGISTRO PLACAS MRE", "RequirementNewMRE.aspx", "460px", "260px");
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

    public void LimpiarCampos()
    {
      this.txtPlate.Text = "";
      this.wddStatus.SelectedValue = "-2";
      this.chkDate.Checked = true;
      this.wdpStartDate.Enabled = true;
      this.wdpEndDate.Enabled = true;
      this.SetDatePicker();
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

    protected void wdgList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        if (!(e.CommandName == "Delete") || !(this.HiddenField1.Value == "Yes"))
          return;
        string text = this.wdgList.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
        SystemUser systemUser = new SystemUser();
        if (new RequirementManagementBL().CreateUpdatePlateMRE(text, systemUser.i_SystemUserId, "02") > 0)
        {
          Message.SetMessage(this.lblMessage, new HandledException(1, "Placa " + text + " fue anulada correctamente."));
          this.Search();
        }
        else
          Message.SetMessage(this.lblMessage, new HandledException(1, "Placa " + text + " no pudo ser anulada."));
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

    private DataTable ExportList()
    {
      try
      {
        string v_Plate = this.txtPlate.Text.TrimEnd();
        int int32 = Convert.ToInt32(this.wddStatus.SelectedValue.ToString());
        DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
        DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
        int i_flag = 1;
        if (!this.chkDate.Checked)
          i_flag = 0;
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = new RequirementManagementBL().SearchMRE(v_Plate, int32, dateTime1, dateTime2, i_flag, 0, 0, out int _);
        this.Session["dtExport"] = (object) dataTable2;
        this.Export();
        return dataTable2;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void Export()
    {
      string script = "ExportExcelAll();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void wdgList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
  }
}
