// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Delivery.Maintenance.MaintenanceException
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Delivery.Maintenance
{
  public class MaintenanceException : Page
  {
    protected UpdatePanel UpdatePanel;
    protected Panel Panel2;
    protected Button BtnNuevo;
    protected Button BtnGrabar;
    protected Button BtnEliminar;
    protected Button btnReturnPopupConfirmation;
    protected DropDownList wddZone;
    protected DropDownList wddTipo;
    protected DropDownList wddDescripcion;
    protected TextBox txtMotivo;
    protected Fecha wdpDateIni;
    protected Fecha wdpDateFin;
    protected CheckBoxList chlDays;
    protected Panel Panel0;
    protected GridView wdgExceptionList;
    protected Panel Panel1;
    protected Pager custPagerExceptions;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.LoadParameters();
        this.LoadExceptionsbylocation();
        this.DisableControls(false);
        this.BtnGrabar.Enabled = false;
        this.BtnEliminar.Enabled = false;
        this.wdpDateIni.Value = Convert.ToDateTime(DateTime.Now);
        this.wdpDateFin.Value = Convert.ToDateTime(DateTime.Now);
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

    private void DisableControls(bool isDisable)
    {
      this.wddZone.Enabled = isDisable;
      this.wddTipo.Enabled = isDisable;
      this.wddDescripcion.Enabled = isDisable;
      this.txtMotivo.Enabled = isDisable;
      this.wdpDateIni.Enabled = isDisable;
      this.wdpDateFin.Enabled = isDisable;
      this.chlDays.Enabled = isDisable;
    }

    private void ClearControls()
    {
      this.wddZone.SelectedValue = "0";
      this.wddTipo.SelectedValue = "0";
      this.wddDescripcion.SelectedValue = "0";
      this.txtMotivo.Text = string.Empty;
      this.chlDays.Items[0].Selected = true;
      this.chlDays.Items[1].Selected = true;
      this.chlDays.Items[2].Selected = true;
      this.chlDays.Items[3].Selected = true;
      this.chlDays.Items[4].Selected = true;
      this.chlDays.Items[5].Selected = true;
      this.chlDays.Items[6].Selected = true;
    }

    private void LoadParameters()
    {
      try
      {
        string pintLocationId = this.Session["SystemUser"] != null ? Convert.ToString((this.Session["SystemUser"] as SystemUser).i_LocationId) : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - MotiveMovementList.aspx");
        SystemParameterQueriesBL parameterQueriesBl = new SystemParameterQueriesBL();
        DataTable dataTable1 = new RequirementQueriesBL().ZoneByLocationGet(pintLocationId);
        this.wddZone.Items.Insert(0, new ListItem("- Seleccione - ", "0"));
        foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
          this.wddZone.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        ArrayList arrFilter = new ArrayList()
        {
          (object) SystemParameterGroups.TypeExceptionDelivery.ToString((IFormatProvider) CultureInfo.CurrentCulture),
          (object) string.Empty,
          (object) 1,
          (object) 1
        };
        DataTable dataTable2 = parameterQueriesBl.GetbyFilter(arrFilter);
        this.wddTipo.Items.Insert(0, new ListItem("- Seleccione - ", "0"));
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
          this.wddTipo.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        this.wddDescripcion.Items.Insert(0, new ListItem("- Seleccione - ", "0"));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadExceptionsbylocation()
    {
      try
      {
        if (this.Session["SystemUser"] == null)
          throw new HandledException(3, "La sesión ha expirado.", "'LoadExceptionsbylocation' - MaintenanceException.aspx");
        this.SearchMotiveMovementList((this.Session["SystemUser"] as SystemUser).i_LocationId, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchMotiveMovementList(int pintLocationId, bool pboolLoadPager)
    {
      try
      {
        int intStartRowIndex = pboolLoadPager ? 1 : this.custPagerExceptions.CurrentPageNumber;
        int intMaxRows = this.custPagerExceptions.CurrentPageSize == 0 ? 10 : this.custPagerExceptions.CurrentPageSize;
        int intTotalRows;
        DataTable dataTable = new RequirementQueriesBL().SearchExceptionsByLocation(pintLocationId, intStartRowIndex, intMaxRows, out intTotalRows);
        int num = intTotalRows;
        if (dataTable.Rows.Count == 0)
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontro información con los criterios seleccionados."));
        this.wdgExceptionList.DataSource = (object) dataTable;
        this.wdgExceptionList.DataBind();
        this.custPagerExceptions.TotalPages = num % intMaxRows == 0 ? num / intMaxRows : num / intMaxRows + 1;
        this.custPagerExceptions.TotalRecordCount = intTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerExceptions.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void BtnGrabar_Click(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlListDetail.aspx");
        int iSystemUserId = systemUser.i_SystemUserId;
        int iLocationId = systemUser.i_LocationId;
        int int32_1 = Convert.ToInt32(this.wddZone.SelectedValue);
        int int32_2 = Convert.ToInt32(this.wddTipo.SelectedValue);
        int int32_3 = Convert.ToInt32(this.wddDescripcion.SelectedValue);
        string text = this.txtMotivo.Text;
        DateTime dt_DateInicio = this.wdpDateIni.Value;
        DateTime dt_DateFin = this.wdpDateFin.Value;
        int int32_4 = Convert.ToInt32(this.wdpDateIni.Value.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture));
        int int32_5 = Convert.ToInt32(this.wdpDateFin.Value.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture));
        int int32_6 = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture));
        string strvdays = "" + (this.chlDays.Items[0].Selected ? "1|" : "0|") + (this.chlDays.Items[1].Selected ? "1|" : "0|") + (this.chlDays.Items[2].Selected ? "1|" : "0|") + (this.chlDays.Items[3].Selected ? "1|" : "0|") + (this.chlDays.Items[4].Selected ? "1|" : "0|") + (this.chlDays.Items[5].Selected ? "1|" : "0|") + (this.chlDays.Items[6].Selected ? "1|" : "0|");
        if (this.wddZone.SelectedIndex == 0)
          throw new HandledException(1, "Debe seleccionar la zona Delivery.");
        if (this.wddTipo.SelectedIndex == 0)
          throw new HandledException(1, "Debe seleccionar el tipo de excepción Delivery.");
        if (this.wddDescripcion.SelectedIndex == 0)
          throw new HandledException(1, "Debe seleccionar la descripción Delivery.");
        if (this.txtMotivo.Text == string.Empty)
          throw new HandledException(1, "Debe Ingresar un Motivo de la excepción.");
        if (strvdays == "0|0|0|0|0|0|0|")
          throw new HandledException(1, "debe seleccionar los días a restringir");
        if (int32_4 < int32_6)
          throw new HandledException(1, "La fecha inicio de la excepción debe ser igual ó mayor al dia actual.");
        if (int32_5 < int32_6)
          throw new HandledException(1, "La fecha fin de la excepción debe ser igual ó mayor al dia actual.");
        if (int32_5 < int32_4)
          throw new HandledException(1, "La fecha fin de la excepción debe ser igual ó mayor a la fecha de inicio.");
        bool flag = false;
        flag = new RequirementManagementBL().ExceptionDeliveryRegister(int32_1, int32_2, int32_3, Convert.ToInt32(iSystemUserId), text, iLocationId, dt_DateInicio, dt_DateFin, strvdays);
        Message.SetMessage(this.lblMessage, new HandledException(2, "•&nbsp;Se registro la excepción delivery satisfactoriamente."));
        this.BtnGrabar.Enabled = false;
        this.ClearControls();
        this.DisableControls(false);
        this.LoadExceptionsbylocation();
        this.wdpDateIni.Value = Convert.ToDateTime(DateTime.Now);
        this.wdpDateFin.Value = Convert.ToDateTime(DateTime.Now);
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

    protected void custPagerExceptions_PageChanged(object sender, CustomPageChangeArgs e)
    {
      int pintLocationId = this.Session["SystemUser"] != null ? (this.Session["SystemUser"] as SystemUser).i_LocationId : throw new HandledException(3, "La sesión ha expirado.", "'LoadExceptionsbylocation' - MaintenanceException.aspx");
      int currentPageNumber = this.custPagerExceptions.CurrentPageNumber;
      this.SearchMotiveMovementList(pintLocationId, false);
    }

    protected void wdgExceptionList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
      int int32 = Convert.ToInt32(e.NewSelectedIndex);
      GridViewRow row = this.wdgExceptionList.Rows[e.NewSelectedIndex];
      this.ViewState["i_ExceptionDeliveryId"] = (object) Convert.ToInt32(this.wdgExceptionList.DataKeys[int32]["i_ExceptionDeliveryId"].ToString());
      this.BtnEliminar.Enabled = true;
      this.BtnGrabar.Enabled = false;
      this.ClearControls();
      this.DisableControls(false);
    }

    protected void BtnNuevo_Click(object sender, EventArgs e)
    {
      this.DisableControls(true);
      this.BtnGrabar.Enabled = true;
      this.ClearControls();
    }

    protected void BtnEliminar_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.Session["MessageTypeId"] = (object) 1;
      this.CreatePopUpServer("SIIV - Delivery - Excepciones", "../../UserControls/PopupConfirmation.aspx?MessageTypeId=2&MessageText=¿Desea Eliminar la Excepción delivery Seleccionada?", "360px", "190px");
    }

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel, this.UpdatePanel.GetType(), "Script", script, true);
    }

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.Session["SystemUser"] == null)
          throw new HandledException(3, "La sesión ha expirado.", "'LoadExceptionsbylocation' - MaintenanceException.aspx");
        new RequirementManagementBL().ExceptionDeliveryDelete((int) this.ViewState["i_ExceptionDeliveryId"], (this.Session["SystemUser"] as SystemUser).i_SystemUserId);
        this.wdgExceptionList.SelectedIndex = -1;
        Message.SetMessage(this.lblMessage, enmMessageType.Success, "• Se eliminó satisfactoriamente.");
        this.LoadExceptionsbylocation();
        this.DisableControls(false);
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

    protected void wddTipo_SelectionChanged(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - StockMovementAAPList.aspx");
        if (this.wddTipo.SelectedIndex == 0)
          throw new HandledException(1, "Debe seleccionar el tipo de excepción Delivery.");
        this.LoadDescriptionByType(Convert.ToInt32(this.wddTipo.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.wddZone.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
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

    protected void wddZone_SelectionChanged(object sender, EventArgs e)
    {
      this.wddTipo.SelectedValue = "0";
      this.wddDescripcion.Items.Clear();
      this.wddDescripcion.Items.Insert(0, new ListItem("- Seleccione - ", "0"));
      this.wddDescripcion.SelectedValue = "0";
    }

    private void LoadDescriptionByType(int pinTypeExceptionsId, int pinZoneId)
    {
      try
      {
        DataTable dataTable = this.LoadMotiveMovementAAP(pinTypeExceptionsId, pinZoneId);
        DataRow row = dataTable.NewRow();
        row["i_ParameterId"] = (object) 0;
        row["v_Description"] = (object) "- Seleccione -";
        dataTable.Rows.InsertAt(row, 0);
        this.wddDescripcion.DataTextField = "v_Description";
        this.wddDescripcion.DataValueField = "i_ParameterId";
        this.wddDescripcion.DataSource = (object) dataTable;
        this.wddDescripcion.DataBind();
        this.wddDescripcion.SelectedValue = "0";
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private DataTable LoadMotiveMovementAAP(int pinTypeExceptionsId, int pinZoneId)
    {
      try
      {
        return new RequirementQueriesBL().GetDescriptionExceptionsByType(pinTypeExceptionsId, pinZoneId);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
