// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Operation.QuarterlyPriceException
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.SpecialPlateProduction.BL;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Operation
{
  public class QuarterlyPriceException : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected Fecha dtFecIni;
    protected Fecha dtFecFin;
    protected Button wibSearch;
    protected Button btnReturnPopupConfirmation;
    protected Button btnManagementTemp;
    protected Button wibNew;
    protected GridView wdgQuarterlyPriceExceptionList;
    protected Pager custPagerSPP;
    protected Label lblMessage;
    protected Button Button2;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      this.lblMessage.Text = "";
      if (this.Page.IsPostBack)
        return;
      try
      {
        DataTable dtResult = new DataTable("Datos");
        this.TableColumns(dtResult);
        DataRow row = dtResult.NewRow();
        dtResult.Rows.Add(row);
        this.ViewState["AutoApprove"] = (object) false;
        this.wdgQuarterlyPriceExceptionList.DataSource = (object) dtResult;
        this.wdgQuarterlyPriceExceptionList.DataBind();
        this.wdgQuarterlyPriceExceptionList.Rows[0].Visible = false;
        this.wdgQuarterlyPriceExceptionList.Columns[10].Visible = false;
        this.wdgQuarterlyPriceExceptionList.Columns[11].Visible = false;
        string str = this.Session["SystemUser"] != null ? new RequirementQueriesBL().GetSystemUserExtendedAction(((SystemUser) this.Session["SystemUser"]).i_SystemUserId, 1) : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - SpecialProductionPlate.aspx");
        if (str != "")
        {
          if (Array.Find<string>(str.Split('|'), (Predicate<string>) (element => element.Equals("5", StringComparison.CurrentCulture))) == "5")
          {
            this.ViewState["AutoApprove"] = (object) true;
            this.wdgQuarterlyPriceExceptionList.Columns[10].Visible = true;
            this.wdgQuarterlyPriceExceptionList.Columns[11].Visible = true;
          }
        }
        this.dtFecIni.Value = DateTime.Now;
        this.dtFecFin.Value = DateTime.Now;
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

    private void TableColumns(DataTable dtResult)
    {
      dtResult.Columns.Add("RowNumber", typeof (string));
      dtResult.Columns.Add("i_QuarterlyPriceException", typeof (string));
      dtResult.Columns.Add("v_TypeVehicle", typeof (string));
      dtResult.Columns.Add("v_TypeTramite", typeof (string));
      dtResult.Columns.Add("v_SpecialPlateType", typeof (string));
      dtResult.Columns.Add("f_PriceCost", typeof (string));
      dtResult.Columns.Add("f_PriceTax", typeof (string));
      dtResult.Columns.Add("f_PriceSale", typeof (string));
      dtResult.Columns.Add("d_StartDate", typeof (DateTime));
      dtResult.Columns.Add("d_FinishDate", typeof (DateTime));
      dtResult.Columns.Add("v_Status", typeof (string));
      dtResult.Columns.Add("v_InsertUserId", typeof (string));
      dtResult.Columns.Add("i_Status", typeof (string));
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchSPP();

    protected void custPagerSPP_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        this.SearchSPPList(Convert.ToInt32(this.Request.QueryString["SpecialPlateTypeId"].ToString()), this.dtFecIni.Text, this.dtFecFin.Text, false);
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

    protected void wibNew_Click(object sender, EventArgs e)
    {
      try
      {
        string str = this.Request.QueryString["SpecialPlateTypeId"].ToString();
        string empty = string.Empty;
        this.CreatePopUpServer("Registro de Precios Trimestrales", "QuarterlyPriceExceptionPopup.aspx?SpecialPlateTypeId=" + str, "370px", "340px");
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

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
        int int32 = Convert.ToInt32(this.Session["i_QuarterlyPriceExceptionId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        switch (Convert.ToInt32(this.Session["MessageTypeId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture))
        {
          case 1:
            if (Convert.ToBoolean(this.ViewState["AutoApprove"]))
            {
              new SpecialPlateProductionQueriesBL().UpdateQuarterlyPriceException(int32, systemUser.i_SystemUserId, 3);
              SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(2, "El registro fue rechazado correctamente"));
              break;
            }
            new SpecialPlateProductionQueriesBL().UpdateQuarterlyPriceException(int32, systemUser.i_SystemUserId, 4);
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(2, "El registro fue eliminado correctamente"));
            break;
          case 3:
            new SpecialPlateProductionQueriesBL().UpdateQuarterlyPriceException(int32, systemUser.i_SystemUserId, 5);
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(2, "El registro fue desactivado correctamente"));
            break;
          default:
            int index = 0;
            if (this.ViewState["index"] != null)
              index = (int) Convert.ToInt16(this.ViewState["index"].ToString());
            if (this.wdgQuarterlyPriceExceptionList.Rows[index] == null)
              throw new HandledException(4, "Error de selección.", "'wdgList' - SpecialProductionPlate.aspx");
            if (new SpecialPlateProductionQueriesBL().UpdateQuarterlyPriceException(int.Parse(this.wdgQuarterlyPriceExceptionList.DataKeys[index]["i_QuarterlyPriceException"].ToString()), systemUser.i_SystemUserId, 2))
              SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(2, "Se procedió correctamente con la activación de los precios trimestrales."));
            else
              SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(2, "Consulte con su Administrador Informático"));
            break;
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
        this.ViewState.Remove("index");
        this.SearchSPP();
      }
    }

    protected void btReporno_Click(object sender, EventArgs e) => this.SearchSPP();

    protected void wdgQuarterlyPriceExceptionList_ItemCommand(
      object sender,
      GridViewCommandEventArgs e)
    {
      try
      {
        int int32 = Convert.ToInt32(e.CommandArgument);
        this.ViewState["index"] = (object) int32;
        if (this.wdgQuarterlyPriceExceptionList.Rows[int32] == null)
          throw new HandledException(4, "Error de selección.", "'wdgList' - SpecialProductionPlate.aspx");
        int num1 = int.Parse(this.wdgQuarterlyPriceExceptionList.DataKeys[int32]["i_QuarterlyPriceException"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        int num2 = int.Parse(this.wdgQuarterlyPriceExceptionList.DataKeys[int32]["i_Status"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        if (e.CommandName.Equals("Edit"))
        {
          if (num2 == 1)
          {
            this.Session["i_QuarterlyPriceExceptionId"] = (object) num1;
            this.Session["MessageTypeId"] = (object) 2;
            string empty = string.Empty;
            this.CreatePopUpServer("SIIV - Placas Especiales", "../../UserControls/PopupConfirmation.aspx?MessageTypeId=2&MessageText=¿Desea Confirmar el Registro?", "360px", "190px");
          }
          else
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "Registro, con estado no permitido para su aprobación"));
        }
        if (e.CommandName.Equals("Delete", StringComparison.CurrentCulture))
        {
          if (num2 == 1)
          {
            this.Session["i_QuarterlyPriceExceptionId"] = (object) num1;
            this.Session["MessageTypeId"] = (object) 1;
            string empty = string.Empty;
            if (!Convert.ToBoolean(this.ViewState["AutoApprove"]))
              this.CreatePopUpServer("SIIV - Placas Especiales", "../../UserControls/PopupConfirmation.aspx?MessageTypeId=1&MessageText=¿Desea Eliminar el Registro?", "350px", "190px");
            else
              this.CreatePopUpServer("SIIV - Placas Especiales", "../../UserControls/PopupConfirmation.aspx?MessageTypeId=1&MessageText=¿Desea Rechazar el Registro?", "350px", "190px");
          }
          else
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "Registro, con estado no permitido para su eliminación"));
        }
        if (!e.CommandName.Equals("inactivate", StringComparison.CurrentCulture))
          return;
        if (num2 == 2)
        {
          this.Session["i_QuarterlyPriceExceptionId"] = (object) num1;
          this.Session["MessageTypeId"] = (object) 3;
          string empty = string.Empty;
          this.CreatePopUpServer("SIIV - Placas Especiales", "../../UserControls/PopupConfirmation.aspx?MessageTypeId=1&MessageText=¿Desea Desactivar el Registro?", "360px", "190px");
        }
        else
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "Registro, con estado no permitido para su Desactivación"));
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

    protected void Button2_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable dataTable = new SpecialPlateProductionQueriesBL().ProductionReport(Convert.ToInt32(this.ViewState["i_SpecialPlateProductionId"]), Convert.ToInt32(this.ViewState["i_SpecialPlateTypeId"].ToString()));
        ReportDocument reportDocument = new ReportDocument();
        string filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportSpecialPlateProduction.rpt";
        reportDocument.Load(filename);
        reportDocument.SetDataSource(dataTable);
        reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "ReporteOrderProduccion");
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

    private void SearchSPP()
    {
      try
      {
        this.SearchSPPList(Convert.ToInt32(this.Request.QueryString["SpecialPlateTypeId"].ToString()), this.dtFecIni.Text, this.dtFecFin.Text, true);
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

    private void SearchSPPList(
      int intSpecialPlateTypeId,
      string dFecIni,
      string fFecFin,
      bool pboolLoadPager)
    {
      try
      {
        int pintstartRowIndex = pboolLoadPager ? 1 : this.custPagerSPP.CurrentPageNumber;
        int pintmaxRows = this.custPagerSPP.CurrentPageSize == 0 ? 10 : this.custPagerSPP.CurrentPageSize;
        int pinttotalRows;
        DataTable quarterlyPriceException = new SpecialPlateProductionQueriesBL().GetQuarterlyPriceException(intSpecialPlateTypeId, dFecIni, fFecFin, pintstartRowIndex, pintmaxRows, out pinttotalRows);
        if (quarterlyPriceException == null || quarterlyPriceException.Rows.Count == 0)
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
        int num = pinttotalRows;
        this.wdgQuarterlyPriceExceptionList.DataSource = (object) quarterlyPriceException;
        this.wdgQuarterlyPriceExceptionList.DataBind();
        this.custPagerSPP.TotalPages = num % pintmaxRows == 0 ? num / pintmaxRows : num / pintmaxRows + 1;
        this.custPagerSPP.TotalRecordCount = pinttotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerSPP.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
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

    protected void wdgQuarterlyPriceExceptionList_RowDeleting(
      object sender,
      GridViewDeleteEventArgs e)
    {
    }

    protected void wdgQuarterlyPriceExceptionList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void wdgQuarterlyPriceExceptionList_InitializeRow(
      object sender,
      GridViewRowEventArgs e)
    {
      if (e.Row.RowIndex < 0)
        return;
      SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlListDetail.aspx");
      ImageButton control1 = e.Row.Cells[10].Controls[0] as ImageButton;
      ImageButton control2 = e.Row.Cells[11].Controls[0] as ImageButton;
      ImageButton control3 = e.Row.Cells[12].Controls[0] as ImageButton;
      DataRow row = ((DataRowView) e.Row.DataItem).Row;
      if (Convert.ToBoolean(this.ViewState["AutoApprove"]))
      {
        control1.ToolTip = "Aprobar solicitud de Precio";
        control2.ToolTip = "Desactivar Precio";
        control3.ToolTip = "Rechazar solicitud de Precio";
      }
      else
        control3.ToolTip = "Eliminar solicitud de Precio";
    }
  }
}
