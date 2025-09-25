// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.SSS.Projection.Projection1
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.SSS.BL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.SSS.Projection
{
  public class Projection1 : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected Fecha wdpDateFin;
    protected Button wibSearch;
    protected Button wibExport;
    protected GridView wdgProjectionList;
    protected Label lblMessageProjection;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.SetDatePicker();
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        this.SearchProjection();
      }
      catch (Exception ex)
      {
        throw;
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void wibExport_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable objDataTable = (DataTable) this.ViewState["Projection"];
        ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
        exportToExcelDataGrid.AgregarHojaLibro(objDataTable, "Hoja");
        exportToExcelDataGrid.CerrarLibro();
        byte[] buffer = exportToExcelDataGrid.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=Reporte.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessageProjection, ex);
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SetDatePicker() => this.wdpDateFin.Value = DateTime.Now;

    private void SearchProjection()
    {
      this.lblMessageProjection.Visible = false;
      DataTable projection = new SSSQueriesBL().GetProjection(Convert.ToDateTime(this.wdpDateFin.Value));
      if (projection == null || projection.Rows.Count == 0)
        Message.SetMessage(this.lblMessageProjection, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
      else
        this.lblMessageProjection.Visible = false;
      this.wdgProjectionList.DataSource = (object) projection;
      this.wdgProjectionList.DataBind();
      this.ViewState["Projection"] = (object) projection;
      this.ViewState["ColumnsCountProjection"] = (object) projection.Columns.Count;
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
