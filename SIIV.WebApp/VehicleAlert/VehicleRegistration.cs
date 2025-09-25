// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.VehicleAlert.VehicleRegistration
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Registration.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.VehicleAlert
{
  public class VehicleRegistration : Page
  {
    private MaintenanceOperation currentOperation = MaintenanceOperation.None;
    private SystemUser objUserBE;
    protected HiddenField H1;
    protected UpdatePanel UpdatePanel2;
    protected GridView wdgList;
    protected Pager custPagerUserList;
    protected Button wibNew;
    protected Button btnReturnPopupConfirmationDelete;
    protected Label lblMessage;
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtPlateNumber;
    protected FilteredTextBoxExtender txtPlateNumber_FilteredTextBoxExtender;
    protected RequiredFieldValidator RequiredFieldValidator14;
    protected ValidatorCalloutExtender ValidatorCalloutExtender14;
    protected TextBox txtTitleNumber;
    protected RequiredFieldValidator RequiredFieldValidator15;
    protected ValidatorCalloutExtender ValidatorCalloutExtender15;
    protected FilteredTextBoxExtender txtTitleNumber_FilteredTextBoxExtender;
    protected CheckBox Check_Notification;
    protected Image Image1;
    protected HtmlTableRow trManagementButtons;
    protected Button wibSave;
    protected Button btnEdit;
    protected Button btnCancel;
    protected HtmlTableRow trwibFinalze;
    protected Button wibFinalze;
    protected Button btnReturnPopupConfirmation;
    protected Button btnReturnPopupConfirmationEdit;
    protected Button btnReturnPopupConfirmationCancel;
    protected Label lblMessageUserList;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadData();
      this.txtTitleNumber.Text = "";
      this.txtPlateNumber.Text = "";
    }

    private void LoadData()
    {
      int pintstartRowIndex = 1;
      int pintmaxRows = this.custPagerUserList.CurrentPageSize == 0 ? 10 : this.custPagerUserList.CurrentPageSize;
      this.objUserBE = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterRequirement.aspx");
      int iSystemUserId = this.objUserBE.i_SystemUserId;
      DataTable dataTable1 = new DataTable();
      int pinttotalRows;
      DataTable dataTable2 = new VehicleAlertQueriesBL().GetbyAlertVehicle(iSystemUserId, pintstartRowIndex, pintmaxRows, out pinttotalRows);
      int num = pinttotalRows;
      this.wdgList.DataSource = (object) dataTable2;
      this.wdgList.DataBind();
      this.custPagerUserList.TotalPages = num % pintmaxRows == 0 ? num / pintmaxRows : num / pintmaxRows + 1;
      this.custPagerUserList.TotalRecordCount = pinttotalRows;
      GridViewRowCollection rows = this.wdgList.Rows;
      this.custPagerUserList.LoadPager();
    }

    private void LoadDataTable()
    {
      int currentPageNumber = this.custPagerUserList.CurrentPageNumber;
      int pintmaxRows = this.custPagerUserList.CurrentPageSize == 0 ? 10 : this.custPagerUserList.CurrentPageSize;
      this.objUserBE = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterRequirement.aspx");
      int iSystemUserId = this.objUserBE.i_SystemUserId;
      DataTable dataTable1 = new DataTable();
      int pinttotalRows;
      DataTable dataTable2 = new VehicleAlertQueriesBL().GetbyAlertVehicle(iSystemUserId, currentPageNumber, pintmaxRows, out pinttotalRows);
      int num = pinttotalRows;
      this.wdgList.DataSource = (object) dataTable2;
      this.wdgList.DataBind();
      this.custPagerUserList.TotalPages = num % pintmaxRows == 0 ? num / pintmaxRows : num / pintmaxRows + 1;
      this.custPagerUserList.TotalRecordCount = pinttotalRows;
      GridViewRowCollection rows = this.wdgList.Rows;
    }

    protected void custPagerWHUser_PageChanged(object sender, CustomPageChangeArgs e)
    {
      this.LoadDataTable();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      GridViewRow row = e.Row;
      if (row.RowIndex <= -1)
        return;
      for (int index = 0; index < row.Cells.Count; ++index)
        row.Cells[index].Visible = false;
    }

    protected void ReturnPage(object sender, EventArgs e)
    {
      try
      {
        string empty = string.Empty;
        this.CreatePopUpServerCancel("SIIV - Cancelar Operación", "../../UserControls/PopupConfirmationCancelAlert.aspx?MessageTypeId=2&MessageText=¿Está seguro de cancelar la operación?", "360px", "190px");
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

    private void CreatePopUpServerDelete(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp2('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void CreatePopUpServerEdit(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp3('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void CreatePopUpServerCancel(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp4('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void registrymantenance(object sender, GridViewCommandEventArgs e)
    {
      int int32 = Convert.ToInt32(e.CommandArgument);
      if (this.wdgList.Rows[int32] == null)
        throw new HandledException(4, "Error de selección.", "wdgList");
      if (e.CommandName.Equals("Edit", StringComparison.CurrentCulture))
      {
        this.currentOperation = MaintenanceOperation.Edit;
        this.ViewState["currentOperation"] = (object) this.currentOperation;
        this.ViewState["i_AlertVehicleId"] = (object) this.wdgList.DataKeys[int32]["i_AlertVehicleId"].ToString();
        this.ViewState["v_Placa"] = (object) this.wdgList.DataKeys[int32]["v_Placa"].ToString();
        this.ViewState["v_Titulo"] = (object) this.wdgList.DataKeys[int32]["v_Titulo"].ToString();
        this.EnabledControls(MaintenanceOperation.Edit);
        this.SetearRecord(Convert.ToInt32(this.wdgList.DataKeys[int32]["i_AlertVehicleId"].ToString()));
        this.Check_Notification.Visible = true;
        this.lblMessageUserList.Visible = false;
        this.trwibFinalze.Visible = false;
        this.trManagementButtons.Visible = true;
      }
      else
      {
        if (!e.CommandName.Equals("Delete", StringComparison.CurrentCulture))
          return;
        Convert.ToInt32(this.wdgList.DataKeys[int32]["i_AlertVehicleId"].ToString());
        this.ViewState["i_AlertVehicleId"] = (object) this.wdgList.DataKeys[int32]["i_AlertVehicleId"].ToString();
        string empty = string.Empty;
        this.CreatePopUpServerDelete("SIIV - Eliminar registro", "../../UserControls/PopupConfirmationDeleteAlert.aspx?MessageTypeId=2&MessageText=¿Está seguro de eliminar la placa " + this.wdgList.DataKeys[int32]["v_Placa"].ToString() + " y título " + this.wdgList.DataKeys[int32]["v_Titulo"].ToString() + " de la alerta vehicular?", "360px", "215px");
      }
    }

    private void SetearRecord(int i_AlertVehicleId)
    {
      this.ViewState[nameof (i_AlertVehicleId)] = (object) i_AlertVehicleId;
      DataTable byRegistration = new VehicleAlertQueriesBL().GetByRegistration(i_AlertVehicleId);
      string str = !(byRegistration.Rows[0]["i_NotificationStatus"].ToString() == "1") ? "false" : "true";
      this.txtPlateNumber.Text = byRegistration.Rows[0]["v_Placa"].ToString();
      this.txtTitleNumber.Text = byRegistration.Rows[0]["v_Titulo"].ToString();
      this.Check_Notification.Checked = Convert.ToBoolean(str);
    }

    private void EnabledControls(MaintenanceOperation penuCurrentOperation)
    {
      string str = this.H1.Value;
      if (penuCurrentOperation == MaintenanceOperation.Edit)
      {
        this.wibSave.Visible = false;
        this.btnEdit.Visible = true;
        string script1 = UtilDA.ActiveTabIndexUser("tabs", 1, "0");
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script1, true);
        string script2 = "TabIndex();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script2, true);
      }
      else
      {
        this.lblMessageUserList.Text = "";
        this.lblMessageUserList.Visible = false;
        string script3 = UtilDA.ActiveTabIndexUser("tabs", 0, "1,2,3,4");
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script3, true);
        string script4 = "TabIndex();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script4, true);
      }
    }

    protected void SaveMessage(object sender, EventArgs e)
    {
      try
      {
        string str1 = this.txtTitleNumber.Text.ToString();
        string str2 = this.txtPlateNumber.Text.ToString();
        string empty = string.Empty;
        this.CreatePopUpServer("SIIV - Nuevo Registro", "../../UserControls/PopupConfirmationSaveAlert.aspx?MessageTypeId=2&MessageText=¿Está seguro de registrar la placa " + str2.ToString() + " y título " + str1.ToString() + "?", "360px", "190px");
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, ex.ErrorMessage);
      }
    }

    protected void EditMessage(object sender, EventArgs e)
    {
      try
      {
        this.txtTitleNumber.Text.ToString();
        this.txtPlateNumber.Text.ToString();
        string empty = string.Empty;
        this.CreatePopUpServerEdit("SIIV - Editar registro", "../../UserControls/PopupConfirmationEditAlert.aspx?MessageTypeId=2&MessageText=¿Está seguro de actualizar los datos de la alerta vehicular?", "360px", "190px");
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, ex.ErrorMessage);
      }
    }

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      try
      {
        string vTtitulo = this.txtTitleNumber.Text.ToString();
        string vPlate = this.txtPlateNumber.Text.ToString();
        int iAction = 1;
        int pintstartRowIndex = 1;
        int pintmaxRows = this.custPagerUserList.CurrentPageSize == 0 ? 10 : this.custPagerUserList.CurrentPageSize;
        this.objUserBE = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterRequirement.aspx");
        int iSystemUserId = this.objUserBE.i_SystemUserId;
        switch (new VehicleAlertQueriesBL().ValidatePlateTitle(0, vPlate, vTtitulo, iSystemUserId, iAction))
        {
          case -1:
            Message.SetMessage(this.lblMessageUserList, enmMessageType.Warning, "ADVERTENCIA </br> Aún no hemos recibido información del registro vehicular de su placa por parte del MTC y/o SUNARP.");
            break;
          case 0:
            Message.SetMessage(this.lblMessageUserList, enmMessageType.Warning, "ADVERTENCIA </br> El Nro. de placa " + vPlate.ToString() + " ya se encuentra registrada en Alerta Vehicular.");
            break;
          case 1:
            string str = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
            {
              (object) SystemParameterGroups.LimitVehicleAlert.ToString(),
              (object) "",
              (object) "1",
              (object) "1"
            }).Rows[0]["v_Value"].ToString();
            int pinttotalRows;
            new VehicleAlertQueriesBL().GetbyAlertVehicle(iSystemUserId, pintstartRowIndex, pintmaxRows, out pinttotalRows);
            int num = pinttotalRows;
            if (Convert.ToInt32(str) == 0 || num < Convert.ToInt32(str))
            {
              new VehicleAlertQueriesBL().AlertVehicleMaintenance(0, iSystemUserId, vPlate, vTtitulo, 1, iSystemUserId, 1, 1);
              this.LoadData();
              this.trwibFinalze.Visible = true;
              this.trManagementButtons.Visible = false;
              this.txtPlateNumber.Enabled = false;
              this.txtTitleNumber.Enabled = false;
              Message.SetMessage(this.lblMessageUserList, enmMessageType.Success, "Suscripción exitosa. </br> Las notificaciones le van a llegar al correo electrónico con el cual se registró: " + this.objUserBE.v_Email.ToString().Split('|')[0] + ". Si desea recibir las notificaciones a un correo diferente puede cambiar su correo en: Seguridad ➜ Cuenta del Usuario.");
              break;
            }
            Message.SetMessage(this.lblMessageUserList, enmMessageType.Warning, "ADVERTENCIA </br> Excedió el límite de registros (" + str + ") para alerta vehícular.");
            break;
        }
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageUserList, enmMessageType.Error, ex.Message);
      }
    }

    private void Search()
    {
      try
      {
        this.LoadData();
        this.HidePopup();
      }
      catch (Exception ex)
      {
        this.HidePopup();
        Message.SetMessage(this.lblMessageUserList, enmMessageType.Error, "Error*****<br>" + ex.Message);
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void wibFinalze_Click(object sender, EventArgs e)
    {
      string script = UtilDA.ActiveTabIndex("tabs", 0, "1");
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void btnReturnPopupConfirmationDelete_Click(object sender, EventArgs e)
    {
      int iAction = 3;
      if (this.Session["SystemUser"] == null)
        throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterRequirement.aspx");
      int int32 = Convert.ToInt32(this.ViewState["i_AlertVehicleId"]);
      this.objUserBE = this.Session["SystemUser"] as SystemUser;
      int iSystemUserId = this.objUserBE.i_SystemUserId;
      new VehicleAlertQueriesBL().AlertVehicleMaintenance(int32, iSystemUserId, "", "", 1, iSystemUserId, -1, iAction);
      this.LoadData();
      Message.SetMessage(this.lblMessage, enmMessageType.Success, "Eliminación exitosa. </br> </br>");
    }

    protected void btnReturnPopupConfirmationEdit_Click(object sender, EventArgs e)
    {
      if (this.Session["SystemUser"] == null)
        throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RegisterRequirement.aspx");
      int int32 = Convert.ToInt32(this.ViewState["i_AlertVehicleId"]);
      this.objUserBE = this.Session["SystemUser"] as SystemUser;
      int iAction = 2;
      int iNotificationStatus = 0;
      int iSystemUserId = this.objUserBE.i_SystemUserId;
      string vTtitulo = this.txtTitleNumber.Text.ToString();
      string vPlate = this.txtPlateNumber.Text.ToString();
      bool flag = this.Check_Notification.Checked;
      this.ViewState["v_Placa"].ToString();
      this.ViewState["v_Titulo"].ToString();
      switch (Convert.ToString(flag))
      {
        case "True":
          iNotificationStatus = 1;
          break;
        case "False":
          iNotificationStatus = 0;
          break;
      }
      switch (new VehicleAlertQueriesBL().ValidatePlateTitle(int32, vPlate, vTtitulo, iSystemUserId, iAction))
      {
        case -2:
          Message.SetMessage(this.lblMessageUserList, enmMessageType.Warning, "ADVERTENCIA <br> La placa " + vPlate + " ya se encuentra registrada en alerta vehicular.");
          break;
        case -1:
          Message.SetMessage(this.lblMessageUserList, enmMessageType.Warning, "ADVERTENCIA <br> Aún no hemos recibido información del registro vehicular de su placa por parte del MTC y/o SUNARP.");
          break;
        case 1:
          new VehicleAlertQueriesBL().AlertVehicleMaintenance(int32, iSystemUserId, vPlate, vTtitulo, iNotificationStatus, iSystemUserId, 1, iAction);
          this.LoadData();
          this.trwibFinalze.Visible = true;
          this.trManagementButtons.Visible = false;
          this.txtTitleNumber.Enabled = false;
          this.txtPlateNumber.Enabled = false;
          this.Check_Notification.Enabled = false;
          Message.SetMessage(this.lblMessageUserList, enmMessageType.Success, "Edición exitosa. </br> Si tiene activado el envío de notificaciones, las alertas le llegaran al correo electrónico con el cual se registró: " + this.objUserBE.v_Email.ToString().Split('|')[0] + ". Si desea recibir las notificaciones a un correo diferente puede cambiar su correo en: Seguridad ➜ Cuenta del Usuario.");
          break;
      }
    }

    protected void btnReturnPopupConfirmationCancel_Click(object sender, EventArgs e)
    {
      this.LoadData();
      string script = UtilDA.ActiveTabIndex("tabs", 0, "1");
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
      this.txtPlateNumber.Text = "";
      this.txtTitleNumber.Text = "";
      this.lblMessageUserList.Text = "";
      this.lblMessage.Text = "";
      this.lblMessageUserList.Visible = false;
      this.trwibFinalze.Visible = false;
      this.trManagementButtons.Visible = true;
      this.wibSave.Visible = true;
      this.btnEdit.Visible = false;
      this.lblMessage.Visible = false;
      this.Check_Notification.Visible = false;
      this.txtPlateNumber.Enabled = true;
      this.txtTitleNumber.Enabled = true;
      string script1 = UtilDA.ActiveTabIndexUser("tabs", 1, "0,2,3,4");
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script1, true);
      string script2 = "TabIndex();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script2, true);
    }

    protected void wdgList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgList_RowEditing(object sender, GridViewEditEventArgs e)
    {
      this.txtTitleNumber.Enabled = true;
      this.txtPlateNumber.Enabled = true;
      this.Check_Notification.Enabled = true;
    }
  }
}
