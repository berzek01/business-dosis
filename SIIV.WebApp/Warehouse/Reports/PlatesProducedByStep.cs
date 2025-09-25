// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Reports.PlatesProducedByStep
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Reports
{
  public class PlatesProducedByStep : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected Fecha wdpDateIni;
    protected Fecha wdpDateFin;
    protected RadioButton rdSunarp;
    protected DropDownList ddlTramiteSunarp;
    protected RadioButton rdAAP;
    protected DropDownList ddlTramiteAAP;
    protected Button wibSearch;
    protected Button wibExport;
    protected GridView wdgList;
    protected GridView wdgListNew;
    protected Pager custPagerPP;
    protected Label lblMessage;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadParameters();
      this.SetDatePicker();
    }

    protected void rdSunarp_CheckedChanged(object sender, EventArgs e)
    {
      string str = "";
      if (this.rdSunarp.Checked)
      {
        this.rdAAP.Checked = false;
        this.ddlTramiteSunarp.Enabled = true;
        this.ddlTramiteAAP.Enabled = false;
        str = "01";
      }
      this.ViewState["option"] = (object) str;
    }

    protected void rdAAP_CheckedChanged(object sender, EventArgs e)
    {
      string str = "";
      if (this.rdAAP.Checked)
      {
        this.rdSunarp.Checked = false;
        this.ddlTramiteAAP.Enabled = true;
        this.ddlTramiteSunarp.Enabled = false;
        str = "02";
      }
      this.ViewState["option"] = (object) str;
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchPP();

    protected void wibExport_Click(object sender, EventArgs e)
    {
      string script = "Export();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        this.Export(this.ExportList());
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

    protected void custPager_PageChanged(object sender, CustomPageChangeArgs e)
    {
      this.SearchPPList(Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.ddlTramiteSunarp.SelectedValue.ToString()), Convert.ToInt32(this.ddlTramiteAAP.SelectedValue.ToString()), this.ViewState["option"].ToString(), false);
    }

    public void LoadParameters()
    {
      DataTable dataTable1 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.IssuingReason.ToString(),
        (object) "",
        (object) "",
        (object) "1"
      });
      if (dataTable1 != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
        {
          if (row["i_GroupId"].ToString() == SystemParameterGroups.IssuingReason.ToString())
            this.ddlTramiteSunarp.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        }
      }
      this.ddlTramiteSunarp.Items.Insert(0, new ListItem("-- Todos --", "0"));
      this.ddlTramiteSunarp.SelectedValue = "-1";
      DataTable dataTable2 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.ProcessType.ToString(),
        (object) "",
        (object) "1",
        (object) "1"
      });
      if (dataTable2 != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
        {
          if (row["i_GroupId"].ToString() == SystemParameterGroups.ProcessType.ToString())
            this.ddlTramiteAAP.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        }
      }
      this.ddlTramiteAAP.Items.Insert(0, new ListItem("-- Todos --", "0"));
      this.ddlTramiteAAP.SelectedValue = "-1";
    }

    private void SetDatePicker()
    {
      this.wdpDateIni.Value = DateTime.Now;
      this.wdpDateFin.Value = DateTime.Now;
    }

    private void SearchPP()
    {
      this.lblMessage.Visible = false;
      if (!this.rdSunarp.Checked && !this.rdAAP.Checked)
      {
        this.HidePopup();
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Debe seleccionar un tipo de trámite");
      }
      else
      {
        try
        {
          this.SearchPPList(Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.ddlTramiteSunarp.SelectedValue.ToString()), Convert.ToInt32(this.ddlTramiteAAP.SelectedValue.ToString()), this.ViewState["option"].ToString(), true);
          this.HidePopup();
        }
        catch (Exception ex)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, ex.Message);
          this.HidePopup();
        }
        finally
        {
          this.HidePopup();
        }
      }
    }

    private void SearchPPList(
      DateTime d_FecIni,
      DateTime d_FecFin,
      int i_DocumentTypeSunarp,
      int i_DocumentTypeAAP,
      string v_Option,
      bool pboolLoadPager)
    {
      try
      {
        int startRowIndex = pboolLoadPager ? 1 : this.custPagerPP.CurrentPageNumber;
        int maxRows = this.custPagerPP.CurrentPageSize == 0 ? 10 : this.custPagerPP.CurrentPageSize;
        int pinttotalRows;
        DataTable processesTypeWeb = new ProductWarehouseQueriesBL().GetProcessedbyProcessesTypeWeb(d_FecIni, d_FecFin, i_DocumentTypeSunarp, i_DocumentTypeAAP, v_Option, startRowIndex, maxRows, out pinttotalRows);
        if (processesTypeWeb == null || processesTypeWeb.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
          this.HidePopup();
          this.wibExport.Enabled = false;
        }
        else
        {
          this.ViewState["dtResult"] = (object) processesTypeWeb;
          this.lblMessage.Visible = false;
          this.wibExport.Enabled = true;
        }
        int num = pinttotalRows;
        this.wdgList.DataSource = (object) processesTypeWeb;
        this.wdgList.DataBind();
        this.custPagerPP.TotalPages = num % maxRows == 0 ? num / maxRows : num / maxRows + 1;
        this.custPagerPP.TotalRecordCount = pinttotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerPP.LoadPager();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, "Error... Consulte con el Administrador. " + ex.Message);
      }
    }

    private DataTable ExportList()
    {
      DataTable dataTable = new DataTable();
      return (DataTable) this.ViewState["dtResult"];
    }

    private void Export(DataTable dt_Result)
    {
      DataTable objDataTable = dt_Result;
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
      exportToExcelDataGrid.AgregarHojaLibro(objDataTable, "PlacasProducidasTramite");
      exportToExcelDataGrid.CerrarLibro();
      byte[] buffer = exportToExcelDataGrid.DownloadByte();
      this.Response.Clear();
      this.Response.AddHeader("content-disposition", "attachment; filename=PlacasProducidas.xls");
      this.Response.BinaryWrite(buffer);
      this.Response.End();
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
