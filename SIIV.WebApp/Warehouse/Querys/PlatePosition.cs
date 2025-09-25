// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Querys.PlatePosition
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Querys
{
  public class PlatePosition : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtPlate;
    protected FilteredTextBoxExtender txtPlate_FilteredTextBoxExtender;
    protected Button wibSearch;
    protected Label lblPlaca;
    protected Label Label2;
    protected Label lblDuplicate;
    protected TextBox txtOwner;
    protected TextBox txtPlateOld;
    protected TextBox txtPosition;
    protected TextBox txtStatus;
    protected TextBox txtTypeUse;
    protected TextBox txtProcessType;
    protected TextBox txtRequester;
    protected TextBox txtReception;
    protected TextBox txtProduct;
    protected TextBox txtLocation;
    protected Button wibSearchPlate;
    protected LinkButton ViewDetailReserved;
    protected Label lblCount;
    protected Button wibRequestPlates;
    protected Button wibReservePlates;
    protected Button btnReturnPopupConfirmation;
    protected Button btnRefreshParent;
    protected Label lblMsg;
    protected HtmlInputHidden d_InsertDateSoli;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMsg.Visible = false;
      if (this.Page.IsPostBack)
        ;
      this.SearchPerfilesCounter();
      this.txtPlate.Focus();
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        SystemUser pobjSystemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlListDetail.aspx");
        DataTable dataTable = new DataTable();
        DataTable platePositionBy = new PlatePositionQueriesBL().GetPlatePositionBy(this.txtPlate.Text);
        if (platePositionBy.Rows.Count > 0)
        {
          string str1 = platePositionBy.Rows[0]["v_Status"].ToString();
          string str2 = "SIN CONCILIACIÓN";
          string str3 = "BLOQUEADO";
          bool flag1 = str1.Contains(str2);
          bool flag2 = str1.Contains(str3);
          this.wibReservePlates.Enabled = true;
          if (flag1 && !flag2)
          {
            this.txtStatus.ForeColor = Color.Red;
            if (string.IsNullOrWhiteSpace(Array.Find<string>(ConfigurationManager.AppSettings["rolesRestrictedPopupConciliation"].Split('|'), (Predicate<string>) (element => element.Equals(Convert.ToString(pobjSystemUser.i_RoleConfigId), StringComparison.CurrentCulture)))))
            {
              string empty = string.Empty;
              this.CreatePopUpServer("SIIV - Placas sin Conciliación", "../../UserControls/PopupAnnouncement.aspx?MessageTypeId=1&MessageText=Nro de Placa no tiene Conciliación Bancaria.", "430px", "165px");
              if (Convert.ToInt32(platePositionBy.Rows[0]["i_flagCantRequestPlate"]) == 1)
                this.wibReservePlates.Enabled = false;
              else
                this.wibReservePlates.Enabled = true;
            }
          }
          else if (flag1 & flag2)
          {
            this.txtStatus.ForeColor = Color.Red;
            if (string.IsNullOrWhiteSpace(Array.Find<string>(ConfigurationManager.AppSettings["rolesRestrictedPopupConciliation"].Split('|'), (Predicate<string>) (element => element.Equals(Convert.ToString(pobjSystemUser.i_RoleConfigId), StringComparison.CurrentCulture)))))
            {
              string empty = string.Empty;
              this.CreatePopUpServer("SIIV - Advertencia", "../../UserControls/PopupAnnouncement.aspx?MessageTypeId=1&MessageText=Nro de Placa no tiene Conciliación Bancaria.{0}Nro de Placa se encuentra bloqueada.", "430px", "205px");
              if (Convert.ToInt32(platePositionBy.Rows[0]["i_flagCantRequestPlate"]) == 1)
                this.wibReservePlates.Enabled = false;
              else
                this.wibReservePlates.Enabled = true;
            }
            else if (string.IsNullOrWhiteSpace(Array.Find<string>(ConfigurationManager.AppSettings["rolesRestrictedPopupLook"].Split('|'), (Predicate<string>) (element => element.Equals(Convert.ToString(pobjSystemUser.i_RoleConfigId), StringComparison.CurrentCulture)))))
            {
              string empty = string.Empty;
              this.CreatePopUpServer("SIIV - Advertencia", "../../UserControls/PopupAnnouncement.aspx?MessageTypeId=1&MessageText=Nro de Placa no tiene Conciliación Bancaria.{0}Nro de Placa se encuentra bloqueada.", "430px", "205px");
              if (Convert.ToInt32(platePositionBy.Rows[0]["i_flagCantRequestPlate"]) == 1)
                this.wibReservePlates.Enabled = false;
              else
                this.wibReservePlates.Enabled = true;
            }
          }
          else if (!flag1 & flag2)
          {
            this.txtStatus.ForeColor = Color.Red;
            if (string.IsNullOrWhiteSpace(Array.Find<string>(ConfigurationManager.AppSettings["rolesRestrictedPopupLook"].Split('|'), (Predicate<string>) (element => element.Equals(Convert.ToString(pobjSystemUser.i_RoleConfigId), StringComparison.CurrentCulture)))))
            {
              string empty = string.Empty;
              this.CreatePopUpServer("SIIV - Bloqueo de Placas", "../../UserControls/PopupAnnouncement.aspx?MessageTypeId=1&MessageText=Nro de Placa se encuentra bloqueada.", "430px", "165px");
              if (Convert.ToInt32(platePositionBy.Rows[0]["i_flagCantRequestPlate"]) == 1)
                this.wibReservePlates.Enabled = false;
              else
                this.wibReservePlates.Enabled = true;
            }
          }
          else
            this.txtStatus.ForeColor = Color.Navy;
          this.lblPlaca.Text = this.txtPlate.Text;
          this.lblDuplicate.Text = platePositionBy.Rows[0]["v_DuplicatePlateNumber"].ToString();
          this.txtOwner.Text = platePositionBy.Rows[0]["v_OwnerCompleteName"].ToString();
          this.txtPosition.Text = platePositionBy.Rows[0]["v_Position"].ToString();
          this.txtStatus.Text = platePositionBy.Rows[0]["v_Status"].ToString();
          this.txtTypeUse.Text = platePositionBy.Rows[0]["v_VehicleTypeUse"].ToString();
          this.txtProcessType.Text = platePositionBy.Rows[0]["v_ProcessTypeSunarp"].ToString();
          this.txtPlateOld.Text = platePositionBy.Rows[0]["v_PlateOld"].ToString();
          this.txtProduct.Text = platePositionBy.Rows[0]["v_Product"].ToString();
          this.txtRequester.Text = platePositionBy.Rows[0]["v_Requester"].ToString();
          this.txtReception.Text = platePositionBy.Rows[0]["v_Reception"].ToString();
          this.txtLocation.Text = platePositionBy.Rows[0]["v_Location"].ToString();
          this.Session["i_LocationId"] = platePositionBy.Rows[0]["i_LocationId"];
          this.Session["i_RequirementPlateId"] = platePositionBy.Rows[0]["i_RequirementPlateId"];
          this.d_InsertDateSoli.Value = platePositionBy.Rows[0]["d_InsertDateSoli"].ToString();
          this.Session["v_Position"] = (object) platePositionBy.Rows[0]["v_Position"].ToString();
          this.Session["i_Status"] = (object) platePositionBy.Rows[0]["i_Status"].ToString();
          if (!(this.txtReception.Text.Trim() != ""))
            return;
          if (this.txtReception.Text.Trim().Substring(this.txtReception.Text.Trim().Length - 10, 10) == DateTime.Today.ToShortDateString())
            this.txtReception.ForeColor = Color.Red;
          else
            this.txtReception.ForeColor = Color.Black;
        }
        else
        {
          this.ClearControls();
          throw new HandledException(1, "Nro de Placa No Encontrada.");
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
      finally
      {
        this.txtPlate.Text = "";
        this.txtPlate.Focus();
        this.HidePopup();
      }
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
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

    protected void wibSearchPlate_Click(object sender, EventArgs e)
    {
      this.CreatePopUpServer("Visualizar", "../../Warehouse/Searchs/WarehouseControlSearchPlate.aspx?v_plateControl=" + this.lblPlaca.Text, "550px", "370px");
    }

    protected void wibReservePlates_Click(object sender, EventArgs e)
    {
      try
      {
        int int32_1 = Convert.ToInt32(this.Session["i_Status"]);
        int int32_2 = Convert.ToInt32(this.Session["i_LocationId"]);
        int int32_3 = Convert.ToInt32(this.Session["i_QuantityPlates"]);
        int int32_4 = Convert.ToInt32(ConfigurationManager.AppSettings["permissiblelimitRequest"]);
        if (this.Session["i_RequirementPlateId"] == null)
          throw new HandledException(1, "Debe Ingresar un número de Placa a reservar");
        if (int32_3 >= int32_4)
          throw new HandledException(1, "Ud. Alcanzo la cantidad Maxima de Placas Reservadas (" + int32_4.ToString() + ")");
        if (int32_1 != 4)
          throw new HandledException(1, "El Estado de la placa no Permite realizar una Reserva");
        if (int32_2 != 14)
          throw new HandledException(1, "La Reservas solo se pueden realizar para placas de Lima");
        if (this.txtPosition.Text.Trim() == "")
          throw new HandledException(1, "La placa no tiene posicion en un Anaquel");
        string empty = string.Empty;
        this.Session["MessageTypeId"] = (object) 1;
        this.CreatePopUpServer("SIIV - Almacen - Reserva de Placas", "../../UserControls/PopupConfirmation.aspx?MessageTypeId=2&MessageText=¿Desea Confirmar la Reserva de la Placa?", "360px", "190px");
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

    private void SaveReservedPlates()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlListDetail.aspx");
        int num1 = 0;
        if (systemUser != null)
          num1 = systemUser.i_SystemUserId;
        int int32_1 = Convert.ToInt32(this.Session["i_RequirementPlateId"]);
        int int32_2 = Convert.ToInt32(this.Session["i_QuantityPlates"]);
        int int32_3 = Convert.ToInt32(this.Session["i_ReserveStockId"]);
        string str = this.Session["v_Position"].ToString();
        int num2 = new PlatePositionQueriesBL().WarehouseReservedPlatesInsert(new ArrayList()
        {
          (object) num1,
          (object) int32_1,
          (object) int32_2,
          (object) int32_3,
          (object) str
        });
        switch (num2)
        {
          case -1:
            throw new HandledException(1, "La placa está ubicada en el Almacén de Abandono, Debe realizar la Transferencia hacia el almacén Principal");
          case 0:
            Message.SetMessage(this.lblMsg, new HandledException(1, "La placa ya cuenta con una Reserva, Favor Revisar"));
            break;
          default:
            this.lblCount.Text = num2.ToString();
            Message.SetMessage(this.lblMsg, new HandledException(2, "Se grabó la Reserva exitosamente"));
            this.ClearControls();
            break;
        }
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

    private void GetReservedPLatesCounter()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlListDetail.aspx");
        int i_SystemUserId = 0;
        if (systemUser != null)
          i_SystemUserId = systemUser.i_SystemUserId;
        DataTable dataTable = new DataTable();
        DataTable reservedPlates = new PlatePositionQueriesBL().GetReservedPlates(i_SystemUserId);
        if (reservedPlates.Rows.Count > 0)
        {
          this.Session["i_QuantityPlates"] = reservedPlates.Rows[0]["i_QuantityPlates"];
          this.Session["i_ReserveStockId"] = reservedPlates.Rows[0]["i_ReserveStockId"];
          this.lblCount.Text = this.Session["i_QuantityPlates"].ToString();
        }
        else
        {
          this.Session["i_QuantityPlates"] = (object) 0;
          this.Session["i_ReserveStockId"] = (object) 0;
          this.lblCount.Text = "0";
        }
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

    private void ClearControls()
    {
      this.lblPlaca.Text = "";
      this.lblDuplicate.Text = "";
      this.txtOwner.Text = string.Empty;
      this.txtPosition.Text = string.Empty;
      this.txtStatus.Text = string.Empty;
      this.txtTypeUse.Text = string.Empty;
      this.txtProcessType.Text = string.Empty;
      this.txtProduct.Text = string.Empty;
      this.txtPlateOld.Text = string.Empty;
      this.txtRequester.Text = string.Empty;
      this.txtReception.Text = string.Empty;
      this.txtLocation.Text = string.Empty;
      this.Session["i_RequirementPlateId"] = (object) null;
      this.Session["v_Position"] = (object) null;
      this.Session["i_LocationId"] = (object) null;
      this.Session["i_Status"] = (object) null;
      this.d_InsertDateSoli.Value = "";
      this.txtReception.BackColor = Color.White;
    }

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      try
      {
        if (Convert.ToInt32(this.Session["MessageTypeId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture) == 1)
          this.SaveReservedPlates();
        else
          this.SaveRequestPlates();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
      finally
      {
      }
    }

    protected void wibRequestPlates_Click(object sender, EventArgs e)
    {
      try
      {
        if (Convert.ToInt32(this.Session["i_QuantityPlates"]) == 0)
          throw new HandledException(1, "No tiene Placas Reservadas para Solicitar");
        string empty = string.Empty;
        this.Session["MessageTypeId"] = (object) 2;
        this.CreatePopUpServer("SIIV - Almacen - Solicitud de Placas", "../../UserControls/PopupConfirmation.aspx?MessageTypeId=2&MessageText=¿Desea Confirmar la Solicitud de las Placas?", "360px", "190px");
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

    private void SaveRequestPlates()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlListDetail.aspx");
        int num = 0;
        if (systemUser != null)
          num = systemUser.i_SystemUserId;
        if (new PlatePositionQueriesBL().WarehouseRequestPlatesInsert(Convert.ToInt32(this.Session["i_ReserveStockId"])) != 1)
          throw new HandledException(1, "No Se pudo grabar la Solicitud Correctamente");
        Message.SetMessage(this.lblMsg, new HandledException(2, "Se grabó la Solicitud exitosamente"));
        this.ClearControls();
        this.Session["i_QuantityPlates"] = (object) 0;
        this.Session["i_ReserveStockId"] = (object) 0;
        this.lblCount.Text = "0";
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

    protected void ViewDetailReserved_Click(object sender, EventArgs e)
    {
      this.CreatePopUpServer2("Visualizar", "../../Warehouse/Searchs/WarehouseListRequestDetail.aspx?sType=0", "770px", "600px");
    }

    private void CreatePopUpServer2(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp2('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void SearchPerfilesCounter()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlList.aspx");
        int iLocationId = systemUser.i_LocationId;
        int num = systemUser.i_CompanyId.Value;
        int iSystemUserId = systemUser.i_SystemUserId;
        DataTable warehouseControl = new WarehouseControlQueriesBL().GetSystemUserWarehouseControl(iLocationId);
        if (warehouseControl == null)
          return;
        foreach (DataRow row in (InternalDataCollectionBase) warehouseControl.Rows)
        {
          if ((long) iSystemUserId == (long) Convert.ToUInt32(row["i_SystemUserId"].ToString()))
          {
            this.ViewDetailReserved.Visible = true;
            this.lblCount.Visible = true;
            this.wibReservePlates.Visible = true;
            this.wibRequestPlates.Visible = true;
            this.GetReservedPLatesCounter();
            break;
          }
          this.ViewDetailReserved.Visible = false;
          this.lblCount.Visible = false;
          this.wibReservePlates.Visible = false;
          this.wibRequestPlates.Visible = false;
        }
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

    protected void btnRefreshParent_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.Session["SystemUser"] == null)
          throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlList.aspx");
        this.GetReservedPLatesCounter();
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
