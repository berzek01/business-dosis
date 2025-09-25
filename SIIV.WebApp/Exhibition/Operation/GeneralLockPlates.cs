// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Operation.GeneralLockPlates
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Operation
{
  public class GeneralLockPlates : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected Label Label1;
    protected TextBox txtPlaca;
    protected FilteredTextBoxExtender txtPlaca_FilteredTextBoxExtender;
    protected Button wibSearchBlockPlate;
    protected Button wibExport;
    protected GridView wdgListPlate;
    protected Pager custPagerGRP;
    protected Label lblMessage;
    protected Button Button1;
    protected Button btnOpenSucesfull;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
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

    protected void wdgListPlate_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        int int32_1 = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = this.wdgListPlate.Rows[int32_1];
        if (row == null)
          throw new HandledException(4, "Error de selección.", "'wdgListPlate' - GeneralLockPlates.aspx");
        int int32_2 = Convert.ToInt32(this.wdgListPlate.DataKeys[int32_1]["i_RequirementPlateId"].ToString());
        int int32_3 = Convert.ToInt32(this.wdgListPlate.DataKeys[int32_1]["i_vehicleMovementId"].ToString());
        string text = row.Cells[5].Text;
        this.Session["selectedRows"] = (object) row;
        this.Session["v_Email"] = (object) this.wdgListPlate.DataKeys[int32_1]["v_EmailConce"].ToString();
        if (e.CommandName == "Block")
        {
          new BlockPlateBL().ups_ValidateBlockPlateStatus(text, int32_2, "01");
          this.CreatePopUpServer("Bloqueo", "LockPlates.aspx?i_RequirementPlateId=" + int32_2.ToString() + "&i_vehicleMovementId=" + int32_3.ToString(), "600px", "410px");
        }
        else
        {
          if (!(e.CommandName == "UnLock"))
            return;
          new BlockPlateBL().ups_ValidateBlockPlateStatus(text, int32_2, "02");
          this.CreatePopUpServer("Desbloqueo", "UnLockPlates.aspx?i_RequirementPlateId=" + int32_2.ToString() + "&i_vehicleMovementId=" + int32_3.ToString(), "600px", "450px");
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

    protected void wibSearchBlockPlate_Click(object sender, EventArgs e)
    {
      try
      {
        this.SearchBlockPlate(this.txtPlaca.Text, true);
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

    private void SearchBlockPlate(string v_Plate, bool pboolLoadPager)
    {
      try
      {
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerGRP.CurrentPageNumber;
        int pintMaxRows = this.custPagerGRP.CurrentPageSize == 0 ? 10 : this.custPagerGRP.CurrentPageSize;
        int pintTotalRows;
        DataTable dataTable = new BlockPlateBL().SearchBlockPlate(v_Plate, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        if (dataTable == null || dataTable.Rows.Count == 0)
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
        int num = pintTotalRows;
        this.wdgListPlate.DataSource = (object) dataTable;
        this.wdgListPlate.DataBind();
        this.custPagerGRP.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
        this.custPagerGRP.TotalRecordCount = pintTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerGRP.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void custPagerGRP_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        this.SearchBlockPlate(this.txtPlaca.Text.TrimEnd(), false);
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

    protected void wibExportPDF_Click(object sender, EventArgs e)
    {
      string script = "ExportPDF();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
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

    private void ExportList()
    {
      try
      {
        DataTable dataTable = new DataTable();
        this.Session["dtExport"] = (object) new BlockPlateBL().SearchBlockPlate("", 0, 0, out int _);
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

    protected void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable objDataTable = (DataTable) this.Session["dtExport"];
        ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
        List<ClassColumns> classColumnsList = new List<ClassColumns>();
        foreach (DataControlField column in (StateManagedCollection) this.wdgListPlate.Columns)
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

    protected void btnOpenSucesfull_Click(object sender, EventArgs e)
    {
      this.wibSearchBlockPlate_Click((object) null, (EventArgs) null);
    }

    protected void wdgListEdit_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgListEdit_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void wdgListPlate_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgListPlate_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
  }
}
