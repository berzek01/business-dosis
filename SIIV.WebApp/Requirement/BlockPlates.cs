// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Requirement.BlockPlates
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Requirement
{
  public class BlockPlates : Page
  {
    protected HiddenField H1;
    protected UpdatePanel updatePanel;
    protected Label lblPlateNew;
    protected TextBox txtPlateNew;
    protected FilteredTextBoxExtender txtPlateNew_FilteredTextBoxExtender;
    protected Button wibSearch;
    protected Label lblMessage;
    protected GridView wdgList;
    protected Pager custPagerUQR;
    protected Button wibNew;
    protected UpdatePanel UpdatePanel2;
    protected TextBox txtFilter;
    protected Button wibSearchPlates;
    protected Label lblMessage2;
    protected GridView wdgBlockUnlockPlates;
    protected Pager custPagerBatch;
    protected Button wibExcel;
    protected Button Button1;
    protected Button btnJavaScriptResponse;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        DataTable dataTable = this.Session["SystemUser"] != null ? new RequirementQueriesBL().PermissionBlockPlates((this.Session["SystemUser"] as SystemUser).i_SystemUserId) : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
        foreach (DataColumn column in (InternalDataCollectionBase) dataTable.Columns)
        {
          if (this.Master.FindControl("MainContent").FindControl(dataTable.Rows[0][column].ToString()) != null)
            this.Master.FindControl("MainContent").FindControl(dataTable.Rows[0][column].ToString()).Visible = false;
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

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.txtPlateNew.Text.Trim() == "")
          Message.SetMessage(this.lblMessage, new HandledException(1, "Debe de ingresar una placa"));
        else
          this.SearchUniversal();
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

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }

    private void SearchUniversal()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
        this.SearchUniversalList(this.txtPlateNew.Text.Trim(), 0, 0, "", "", -1, "", 0, 0, -3, "", "", systemUser.i_SystemUserId, 1, Convert.ToInt32(this.ViewState["i_platetypeId"], (IFormatProvider) CultureInfo.CurrentCulture), true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchUniversalList(
      string pstrPlateNew,
      int pintStartdate,
      int pintFinishdate,
      string pstrPlateOld,
      string pstrTitleNumber,
      int pintRequirementPlateId,
      string pstrOwnerName,
      int pintCategoryId,
      int pintProcessTypeId,
      int pintStatus,
      string pstrSerial,
      string pstrPaymentCode,
      int pintUserId,
      int pintQueryType,
      int pintiplateTypeId,
      bool pboolLoadPager)
    {
      try
      {
        int startRowIndex = pboolLoadPager ? 1 : this.custPagerUQR.CurrentPageNumber;
        int maxRows = this.custPagerUQR.CurrentPageSize == 0 ? 10 : this.custPagerUQR.CurrentPageSize;
        int pinttotalRows;
        DataTable dataTable = new RequirementQueriesBL().UniversalQueryRead(pstrPlateNew, pintStartdate, pintFinishdate, pstrPlateOld, pstrTitleNumber, pintRequirementPlateId, pstrOwnerName, pintCategoryId, pintProcessTypeId, pintStatus, pstrSerial, pstrPaymentCode, pintUserId, pintQueryType, pintiplateTypeId, startRowIndex, maxRows, out pinttotalRows);
        if (dataTable == null || dataTable.Rows.Count == 0)
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
        else
          this.Session["UniversalList"] = (object) dataTable;
        int num = pinttotalRows;
        this.wdgList.DataSource = (object) dataTable;
        this.wdgList.DataBind();
        this.custPagerUQR.TotalPages = num % maxRows == 0 ? num / maxRows : num / maxRows + 1;
        this.custPagerUQR.TotalRecordCount = pinttotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerUQR.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void wdgList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        if (e.CommandName.Equals("getData", StringComparison.CurrentCulture))
        {
          DataTable dataTable = this.Session["SystemUser"] != null ? new RequirementQueriesBL().PermissionBlockPlates((this.Session["SystemUser"] as SystemUser).i_SystemUserId) : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
          foreach (DataColumn column in (InternalDataCollectionBase) dataTable.Columns)
          {
            if (dataTable.Rows[0][column].ToString() != "getData")
            {
              Message.SetMessage(this.lblMessage, new HandledException(1, "Usted no tiene los permisos para realizar esta operacion."));
              return;
            }
          }
          ImageButton imageButton = sender as ImageButton;
          this.CreatePopUp("../Requirement/RequirementData.aspx?plate=" + this.wdgList.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text, "Datos de la Placa", "1100", "820");
        }
        if (!e.CommandName.Equals("DeleteSoli", StringComparison.CurrentCulture))
          return;
        string empty = string.Empty;
        GridViewRow row = this.wdgList.Rows[Convert.ToInt32(e.CommandArgument)];
        if (new RequirementQueriesBL().BlockPlatesUniversalQueryRead(0, row.Cells[2].Text.Trim(), 1, -1, 0, 1, out int _).Rows.Count > 0)
          Message.SetMessage(this.lblMessage, new HandledException(1, "Esta placa ya se encuentra bloqueado"));
        else
          this.CreatePopUp("../Requirement/BlockPlatesPopup.aspx?i_BlockId=+" + 0.ToString() + "&plate=" + row.Cells[2].Text + "&Motive1=&Motive2=&iRequirementPlateStatus= ", "SIIV - Bloquear Placa", "470px", "400px");
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

    private void CreatePopUp(string url, string pstrtitle, string width, string height)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) url, (object) pstrtitle, (object) (width + "px"), (object) (height + "px"));
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }

    protected void wdgList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
    }

    protected void wibExportExcel_Click(object sender, EventArgs e)
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
    }

    private void ExportList()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
        int iLocationId = systemUser.i_LocationId;
        int num = systemUser.i_CompanyId.Value;
        string text = this.txtFilter.Text;
        DataTable dataTable = new DataTable();
        this.Session["dtExport"] = (object) new RequirementQueriesBL().BlockPlatesUniversalQueryRead(0, "", -1, systemUser.i_SystemUserId, 0, 0, out int _);
        this.Export();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void Export()
    {
      string script = "ExportExcelAll();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }

    protected void custPagerUQR_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
        this.SearchUniversalList(this.txtPlateNew.Text.Trim(), 0, 0, "", "", -1, "", 0, 0, -3, "", "", systemUser.i_SystemUserId, Convert.ToInt32(this.ViewState["t"], (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.ViewState["i_platetypeId"], (IFormatProvider) CultureInfo.CurrentCulture), true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void custPagerBatch_PageChanged(object sender, CustomPageChangeArgs e)
    {
      SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
      int iLocationId = systemUser.i_LocationId;
      int num = systemUser.i_CompanyId.Value;
      this.SearchBlockPlatesList(0, this.txtFilter.Text, 1, false);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable objDataTable = (DataTable) this.Session["dtExport"];
        ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
        List<ClassColumns> classColumnsList = new List<ClassColumns>();
        foreach (DataControlField column in (StateManagedCollection) this.wdgBlockUnlockPlates.Columns)
        {
          if (column.Visible && column.GetType().Name == "BoundField")
          {
            BoundField boundField = (BoundField) column;
            classColumnsList.Add(new ClassColumns(boundField.DataField, 1, (int) column.ControlStyle.Width.Value, boundField.HeaderText));
          }
        }
        exportToExcelDataGrid.clsTitle = classColumnsList;
        exportToExcelDataGrid.AgregarHojaLibro(objDataTable, "Consulta Placas Bloqueados");
        exportToExcelDataGrid.CerrarLibro();
        byte[] buffer = exportToExcelDataGrid.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=PlacasBloqueados.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
      }
      catch (Exception ex)
      {
      }
    }

    protected void btnJavaScriptResponse_Click(object sender, EventArgs e)
    {
      if (this.Session["OpenSucesfull"] == null)
        return;
      this.wibSearchPlates_Click((object) null, (EventArgs) null);
    }

    protected void wibSearchPlates_Click(object sender, EventArgs e)
    {
      try
      {
        this.SearchBlockPlatesRegister();
        this.lblMessage2.Visible = false;
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage2, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage2, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SearchBlockPlatesRegister()
    {
      try
      {
        this.SearchBlockPlatesList(0, this.txtFilter.Text.Trim(), -1, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchBlockPlatesList(
      int pintBlockId,
      string pstrPlate,
      int pintStatus,
      bool pboolLoadPager)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
        int startRowIndex = pboolLoadPager ? 1 : this.custPagerBatch.CurrentPageNumber;
        int maxRows = this.custPagerBatch.CurrentPageSize == 0 ? 10 : this.custPagerBatch.CurrentPageSize;
        int pinttotalRows;
        DataTable dataTable = new RequirementQueriesBL().BlockPlatesUniversalQueryRead(pintBlockId, pstrPlate, pintStatus, systemUser.i_SystemUserId, startRowIndex, maxRows, out pinttotalRows);
        int num = pinttotalRows;
        this.wdgBlockUnlockPlates.DataSource = (object) dataTable;
        this.wdgBlockUnlockPlates.DataBind();
        this.custPagerBatch.TotalPages = num % maxRows == 0 ? num / maxRows : num / maxRows + 1;
        this.custPagerBatch.TotalRecordCount = pinttotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerBatch.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void wdgBlockUnlockPlates_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        int int32 = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = this.wdgBlockUnlockPlates.Rows[int32];
        if (!(e.CommandName == "Edit"))
          return;
        string empty = string.Empty;
        if (row.Cells[19].Text == "Bloqueado")
        {
          this.CreateUnlokPopUp("../Requirement/BlockPlatesPopup.aspx?i_BlockId=+" + this.wdgBlockUnlockPlates.DataKeys[int32]["i_BlockId"].ToString() + "&plate=&Motive1=&Motive2 ", "SIIV - Desbloquear Placa", "450px", "405px");
          this.lblMessage2.Visible = false;
        }
        else
          Message.SetMessage(this.lblMessage2, new HandledException(1, "La Placa ya se encuentra Desbloqueada."));
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

    private void CreateUnlokPopUp(string url, string pstrtitle, string width, string height)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) url, (object) pstrtitle, (object) (width + "px"), (object) (height + "px"));
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script, true);
    }

    protected void wdgBlockUnlockPlates_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void wibNew_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.CreatePopUp("../Requirement/BlockPlatesPopup.aspx?i_BlockId=+" + 0.ToString() + "&plate=&Motive1=&Motive2 ", "SIIV - Bloquear Placa", "520px", "350px");
    }

    protected void wdgBlockUnlockPlates_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
        return;
      e.Row.Cells[0].ToolTip = "Desbloquear";
    }

    protected void wdgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
        return;
      e.Row.Cells[1].ToolTip = "Bloquear";
    }
  }
}
