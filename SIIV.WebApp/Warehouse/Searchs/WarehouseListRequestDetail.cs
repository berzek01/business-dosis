// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Searchs.WarehouseListRequestDetail
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.Warehouse.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Searchs
{
  public class WarehouseListRequestDetail : Page
  {
    private DataTable dtListTickets = new DataTable();
    private DataTable dtListTicketsDetail = new DataTable();
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected Fecha wdpDateIni;
    protected Fecha wdpDateFin;
    protected TextBox txtNumberTicket;
    protected FilteredTextBoxExtender txtNumberTicket_FilteredTextBoxExtender;
    protected TextBox txtCounter;
    protected FilteredTextBoxExtender txtCounter_FilteredTextBoxExtender;
    protected DropDownList wddEstado;
    protected Button wibSearchAvanzado;
    protected GridView wdgListTicket;
    protected HtmlTableRow Tr1;
    protected Label Label1;
    protected HtmlTableRow Tr2;
    protected GridView wdgListTicketDetails;
    protected Button wibExportar;
    protected Button wibCancel;
    protected Label lblMsg;
    protected Button ButtonExport;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.ViewState["vTypeImprimir"] = (object) this.Request.QueryString["sType"].ToString();
        if (this.ViewState["vTypeImprimir"].ToString() == "0")
          this.wibExportar.Visible = false;
        this.LoadStatus();
        this.SetDatePicker();
        this.InitialLoad();
        DataTable dtResult = new DataTable("Datos2");
        this.TableColumnsTicketDetails(dtResult);
        this.wdgListTicketDetails.DataSource = (object) dtResult;
        this.wdgListTicketDetails.DataBind();
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

    private void LoadStatus()
    {
      try
      {
        DataTable group = new RequirementQueriesBL().GetGroup("992");
        if (group != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) group.Rows)
          {
            if (Convert.ToInt32(row["i_ParameterId"]) != 5)
              this.wddEstado.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          }
        }
        this.wddEstado.Items.Insert(0, new ListItem("- Todos - ", "0"));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SetDatePicker()
    {
      try
      {
        DateTime.Now.AddMonths(-1);
        this.wdpDateIni.Value = DateTime.Now;
        this.wdpDateFin.Value = DateTime.Now;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void wibCancel_Click(object sender, EventArgs e)
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void wibExportar_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.wdgListTicket.Rows.Count == 0)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENTEBILLING_UNIVERSAL_QUERY_ERROR_EXPORT);
        this.ExportList();
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

    private void ExportList()
    {
      SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseListRequestDetail.aspx");
      this.txtNumberTicket.Text.Trim();
      DateTime dateTime1 = Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture);
      DateTime dateTime2 = Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture);
      this.txtCounter.Text.Trim();
      int iSystemUserId = systemUser.i_SystemUserId;
      Convert.ToInt32(this.ViewState["t"], (IFormatProvider) CultureInfo.CurrentCulture);
      Convert.ToInt32(this.ViewState["i_platetypeId"], (IFormatProvider) CultureInfo.CurrentCulture);
      int i_ReserveStockId = 0;
      WarehouseControlQueriesBL controlQueriesBl = new WarehouseControlQueriesBL();
      DataTable dataTable = new DataTable();
      DataTable listTicket = controlQueriesBl.GetListTicket(iSystemUserId, dateTime1, dateTime2, this.txtCounter.Text, Convert.ToInt32(this.wddEstado.SelectedValue), i_ReserveStockId);
      if (listTicket.Rows.Count == 0)
        throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENTEBILLING_UNIVERSAL_QUERY_ERROR_EXPORT);
      this.Session["dtExport"] = (object) listTicket;
      this.Export();
    }

    private void Export()
    {
      string script = "ExportExcelAll();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void TableColumnsTicket(DataTable dtResult)
    {
      dtResult.Columns.Add("Item", typeof (string));
      dtResult.Columns.Add("i_ReserveStockId", typeof (string));
      dtResult.Columns.Add("i_QuantityPlates", typeof (string));
      dtResult.Columns.Add("i_CantEntregadas", typeof (string));
      dtResult.Columns.Add("i_CantPendientes", typeof (string));
      dtResult.Columns.Add("d_InsertDate", typeof (string));
      dtResult.Columns.Add("v_CounterName", typeof (string));
      dtResult.Columns.Add("v_Status", typeof (string));
    }

    private void TableColumnsTicketDetails(DataTable dtResult)
    {
      dtResult.Columns.Add("Item", typeof (string));
      dtResult.Columns.Add("v_PlateNew", typeof (string));
      dtResult.Columns.Add("i_RequirementPlateId", typeof (string));
      dtResult.Columns.Add("v_Posicion", typeof (string));
      dtResult.Columns.Add("v_Solicitante", typeof (string));
      dtResult.Columns.Add("v_Status", typeof (string));
    }

    private void InitialLoad()
    {
      try
      {
        this.lblMsg.Visible = false;
        int i_ReserveStockId = 0;
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlListDetail.aspx");
        int i_SystemUserId = 0;
        if (systemUser != null)
          i_SystemUserId = systemUser.i_SystemUserId;
        DateTime dateTime1 = Convert.ToDateTime((object) this.wdpDateIni.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        DateTime dateTime2 = Convert.ToDateTime((object) this.wdpDateFin.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        WarehouseControlQueriesBL controlQueriesBl = new WarehouseControlQueriesBL();
        if (this.txtNumberTicket.Text != "")
          i_ReserveStockId = Convert.ToInt32(this.txtNumberTicket.Text);
        this.dtListTickets = controlQueriesBl.GetListTicket(i_SystemUserId, dateTime1, dateTime2, this.txtCounter.Text, Convert.ToInt32(this.wddEstado.SelectedValue), i_ReserveStockId);
        if (this.dtListTickets.Rows.Count > 0)
        {
          this.wdgListTicket.DataSource = (object) this.dtListTickets;
          this.wdgListTicket.DataBind();
        }
        else
        {
          DataTable dtResult1 = new DataTable("Datos");
          this.TableColumnsTicket(dtResult1);
          this.wdgListTicket.DataSource = (object) dtResult1;
          this.wdgListTicket.DataBind();
          DataTable dtResult2 = new DataTable("Datos2");
          this.TableColumnsTicketDetails(dtResult2);
          this.wdgListTicketDetails.DataSource = (object) dtResult2;
          this.wdgListTicketDetails.DataBind();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void wdgListTicket_SelectedIndexChanged(object sender, EventArgs e)
    {
      int int32 = Convert.ToInt32(this.wdgListTicket.DataKeys[this.wdgListTicket.SelectedIndex].Value);
      this.Session["i_ReserveStockId"] = (object) int32;
      this.dtListTicketsDetail = new WarehouseControlQueriesBL().GetListTicketDetail(int32);
      if (this.dtListTicketsDetail.Rows.Count > 0)
      {
        this.ViewState["dtListTicketsDetail"] = (object) this.dtListTicketsDetail;
        this.wdgListTicketDetails.DataSource = (object) this.dtListTicketsDetail;
        this.wdgListTicketDetails.DataBind();
      }
      else
      {
        DataTable dtResult = new DataTable("Datos2");
        this.TableColumnsTicket(dtResult);
        this.wdgListTicketDetails.DataSource = (object) dtResult;
        this.wdgListTicketDetails.DataBind();
      }
    }

    protected void wdgListTicket_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
        return;
      ((WebControl) e.Row.Cells[9].Controls[0]).ToolTip = "Reimprimir";
      if (!(this.ViewState["vTypeImprimir"].ToString() == "0"))
        return;
      e.Row.Cells[9].Visible = false;
      this.wibExportar.Visible = false;
    }

    protected void wibSearchAvanzado_Click(object sender, EventArgs e)
    {
      this.InitialLoad();
      if (this.wdgListTicket.Rows.Count != 0)
        return;
      Message.SetMessage(this.lblMsg, new HandledException(1, "No se encuentraron registros con los criterios de busqueda seleccionados"));
    }

    protected void wdgListTicket_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        this.lblMsg.Visible = false;
        if (!e.CommandName.Equals("PrintUri", StringComparison.CurrentCulture))
          return;
        int int32_1 = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = this.wdgListTicket.Rows[int32_1];
        int int32_2 = Convert.ToInt32(this.wdgListTicket.DataKeys[int32_1]["i_ReserveStockId"]);
        if (this.wdgListTicket.DataKeys[int32_1]["i_Status"].ToString() == "3")
        {
          new PlatePositionQueriesBL().WarehouseRequestPlatesInsert(int32_2);
          this.InitialLoad();
          Message.SetMessage(this.lblMsg, new HandledException(2, "Se realizo la operación exitosamente"));
        }
        else
          Message.SetMessage(this.lblMsg, new HandledException(1, "El registro no se encuentra en el estado correcto, para realizar esta operación"));
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

    protected void wdgListTicketDetails_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        if (!e.CommandName.Equals("Delete", StringComparison.CurrentCulture))
          return;
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - PlateDeliveryMassive.aspx");
        int i_SystemUserId = 0;
        if (systemUser != null)
          i_SystemUserId = systemUser.i_SystemUserId;
        DataTable dataTable = new DataTable();
        RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
        GridViewRow row = this.wdgListTicketDetails.Rows[Convert.ToInt32(e.CommandArgument)];
        if (row == null)
          throw new HandledException(4, "Error de selección.", "'wdgRequestPlateList' - WarehouseControlListDetail.aspx");
        int int32 = Convert.ToInt32(this.Session["i_ReserveStockId"]);
        int i_RequirementPlateId = Convert.ToInt32(row.Cells[2].Text);
        if (new WarehouseControlQueriesBL().DeleteRequirementOfTicket(i_RequirementPlateId, int32, i_SystemUserId) == -1)
          throw new HandledException(1, "No se pudo eliminar la placa reservada");
        this.InitialLoad();
        this.dtListTicketsDetail = (DataTable) this.ViewState["dtListTicketsDetail"];
        this.dtListTicketsDetail.Rows.Remove(this.dtListTicketsDetail.Rows.Cast<DataRow>().ToList<DataRow>().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (item => Convert.ToInt32(item["i_RequirementPlateId"]) == i_RequirementPlateId)));
        this.wdgListTicketDetails.DataSource = (object) this.dtListTicketsDetail;
        this.wdgListTicketDetails.DataBind();
        Message.SetMessage(this.lblMsg, new HandledException(2, "Se Eliminó la placa reservada exitosamente"));
        this.ViewState["dtListRequestPlatesDetail"] = (object) this.dtListTicketsDetail;
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

    protected void wdgListTicketDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgListTicketDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
        return;
      ((WebControl) e.Row.Cells[6].Controls[0]).ToolTip = "Eliminar";
      if (!(this.ViewState["vTypeImprimir"].ToString() != "0"))
        return;
      e.Row.Cells[6].Visible = false;
    }

    protected void ButtonExport_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable objDataTable = (DataTable) this.Session["dtExport"];
        ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
        List<ClassColumns> classColumnsList = new List<ClassColumns>();
        foreach (DataControlField column in (StateManagedCollection) this.wdgListTicket.Columns)
        {
          if (column.Visible && column.GetType().Name == "BoundField")
          {
            BoundField boundField = (BoundField) column;
            classColumnsList.Add(new ClassColumns(boundField.DataField, 1, (int) column.ControlStyle.Width.Value, boundField.HeaderText));
          }
        }
        exportToExcelDataGrid.clsTitle = classColumnsList;
        exportToExcelDataGrid.AgregarHojaLibro(objDataTable, "Libro1");
        exportToExcelDataGrid.CerrarLibro();
        byte[] buffer = exportToExcelDataGrid.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=TicketsReservados.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
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
  }
}
