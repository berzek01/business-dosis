// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Support.Conciliacion
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.SystemAudit.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Support
{
  public class Conciliacion : Page
  {
    protected UpdatePanel updatePanel;
    protected Label Label1;
    protected RadioButtonList rblTipoDato;
    protected Label lblPlateNew;
    protected TextBox txtDatoIngresado;
    protected FilteredTextBoxExtender txtDatoIngresado_FilteredTextBoxExtender;
    protected Button btnLimpiar;
    protected Button btnConciliar;
    protected Label lblMessage;
    protected Button btnReturnPopupConfirmation;
    protected HtmlInputText frmIdReqPlate;
    protected HtmlInputText frmCodPago;
    protected HtmlInputText frmIdReq;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      this.AssignRequirementPlateId(0);
      this.txtDatoIngresado.Text = "";
      this.rblTipoDato.SelectedValue = 1.ToString();
    }

    protected void rblTipoDato_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.AssignRequirementPlateId(0);
      this.txtDatoIngresado.Text = "";
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
      this.AssignRequirementPlateId(0);
      this.txtDatoIngresado.Text = "";
      this.rblTipoDato.SelectedValue = 1.ToString();
    }

    protected void btnConciliar_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.txtDatoIngresado.Text.Length <= 11 && !string.IsNullOrWhiteSpace(this.txtDatoIngresado.Text))
        {
          DataTable dtRequirementData = (DataTable) null;
          if (int.Parse(this.rblTipoDato.SelectedValue) == 1)
          {
            dtRequirementData = new RequirementQueriesBL().GetRequirementPlateDatabyId(Convert.ToInt32(this.txtDatoIngresado.Text, (IFormatProvider) CultureInfo.CurrentCulture));
          }
          else
          {
            IList list = new SystemAuditManagementBL().RequirementPlateListbyUniqueCode(this.txtDatoIngresado.Text);
            if (list != null)
              dtRequirementData = new RequirementQueriesBL().GetRequirementPlateDatabyId(Convert.ToInt32(list[0].ToString().Split('|')[0], (IFormatProvider) CultureInfo.CurrentCulture));
          }
          if (dtRequirementData != null && dtRequirementData.Rows.Count > 0)
          {
            int int32 = Convert.ToInt32(dtRequirementData.Rows[0]["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture);
            List<int> intList = new List<int>()
            {
              1,
              2,
              3,
              4,
              5,
              6
            };
            List<EstadoSolicitud> estadoSolicitudList = this.ObtenerEstados();
            if (!intList.Contains(Convert.ToInt32(dtRequirementData.Rows[0]["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture)))
            {
              this.lblMessage.Visible = true;
              Message.SetMessage(this.lblMessage, enmMessageType.Warning, string.Format("***ADVERTENCIA***</br>La solicitud {0} no puede ser conciliada porque su estado es '{1}'", (object) int32, (object) estadoSolicitudList.Find((Predicate<EstadoSolicitud>) (x => x.id == Convert.ToInt32(dtRequirementData.Rows[0]["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture))).descripcion));
            }
            else
            {
              string requirementPlateId = new RequirementQueriesBL().GetPlatebyRequirementPlateId(int32);
              if (string.IsNullOrWhiteSpace(requirementPlateId))
                throw new HandledException(1, string.Format("No se pudo encontrar la placa relacionada a la solicitud {0}.", (object) int32));
              if (!Convert.ToBoolean(new RequirementQueriesBL().GetConciliacionPlate(requirementPlateId, 1).Rows[0]["b_HasConciliation"]))
              {
                DataTable databyRequirement = new RequirementQueriesBL().GetPaymentDatabyRequirement(Convert.ToInt32(dtRequirementData.Rows[0]["i_RequirementId"], (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32((object) int32, (IFormatProvider) CultureInfo.CurrentCulture));
                string codPago = databyRequirement.Rows[0]["v_PaymentCode"].ToString();
                string str1 = databyRequirement.Rows[0]["Bank"].ToString();
                Decimal num = Convert.ToDecimal(databyRequirement.Rows[0]["f_PriceTotal"], (IFormatProvider) CultureInfo.CurrentCulture);
                this.AssignRequirementPlateId(int32, Convert.ToInt32(dtRequirementData.Rows[0]["i_RequirementId"], (IFormatProvider) CultureInfo.CurrentCulture), codPago);
                string str2 = string.IsNullOrWhiteSpace(codPago) ? "" : "con código de pago: B¡\"" + codPago + "\"B! ";
                string empty = string.Empty;
                this.CreatePopUpServer("Confirmar Conciliación", "../../UserControls/PopupConfirmationConciliacion.aspx?MessageTypeId=2&MessageText=¿Está seguro que desea conciliar la solicitud: B¡\"" + this.frmIdReqPlate.Value + "\"B! " + str2 + "acreditada por el banco: B¡\"" + str1 + "\"B! por el monto de: B¡\"" + num.ToString() + "\"B! soles?", "450px", "250px");
              }
              else
              {
                this.lblMessage.Visible = true;
                Message.SetMessage(this.lblMessage, enmMessageType.Warning, string.Format("***ADVERTENCIA***</br>La solicitud {0} ya se encuentra conciliada.", (object) int32));
              }
            }
          }
          else
          {
            this.lblMessage.Visible = true;
            Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***ADVERTENCIA***</br>El dato ingresado no está asociado a una solicitud en el sistema");
          }
        }
        else
        {
          this.lblMessage.Visible = true;
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***ADVERTENCIA***</br>El dato ingresado no puede ser vacío y debe tener como máximo 11 carácteres.");
        }
      }
      catch (HandledException ex)
      {
        new RequirementQueriesBL().RegisterConciliacionAudit(this.txtDatoIngresado.Text, ((SystemUser) this.Session["SystemUser"]).i_SystemUserId, "Error");
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        new RequirementQueriesBL().RegisterConciliacionAudit(this.txtDatoIngresado.Text, ((SystemUser) this.Session["SystemUser"]).i_SystemUserId, "Error");
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
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
        if (string.IsNullOrWhiteSpace(this.frmCodPago.Value))
        {
          DataTable codebyRequirementId = new RequirementQueriesBL().GeneratePaymentCodebyRequirementId(Convert.ToInt32(this.frmIdReq.Value, (IFormatProvider) CultureInfo.CurrentCulture));
          int int32 = Convert.ToInt32(codebyRequirementId.Rows[0]["i_Code"]);
          string str = codebyRequirementId.Rows[0]["v_Message"].ToString();
          if (int32 != 0)
            throw new HandledException(-200, "No se pudo conciliar la solicitud " + this.frmIdReqPlate.Value + ". Error: " + str);
        }
        DataTable dataTable = new RequirementQueriesBL().ConciliarRequirementPlatebyId(Convert.ToInt32(this.frmIdReqPlate.Value, (IFormatProvider) CultureInfo.CurrentCulture));
        int int32_1 = Convert.ToInt32(dataTable.Rows[0]["i_Code"]);
        string str1 = dataTable.Rows[0]["v_Message"].ToString();
        if (int32_1 != 0)
          throw new HandledException(-200, "No se pudo conciliar la solicitud " + this.frmIdReqPlate.Value + ". Error: " + str1);
        new RequirementQueriesBL().RegisterConciliacionAudit(this.txtDatoIngresado.Text, ((SystemUser) this.Session["SystemUser"]).i_SystemUserId, "Éxito");
        this.lblMessage.Visible = true;
        Message.SetMessage(this.lblMessage, enmMessageType.Success, "***ÉXITO***</br>La solicitud <b>" + this.frmIdReqPlate.Value + "</b> se ha conciliado satisfactoriamente.");
      }
      catch (HandledException ex)
      {
        new RequirementQueriesBL().RegisterConciliacionAudit(this.txtDatoIngresado.Text, ((SystemUser) this.Session["SystemUser"]).i_SystemUserId, "Error");
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        new RequirementQueriesBL().RegisterConciliacionAudit(this.txtDatoIngresado.Text, ((SystemUser) this.Session["SystemUser"]).i_SystemUserId, "Error");
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    public void AssignRequirementPlateId(int ReqPlateId, int ReqId = 0, string codPago = "")
    {
      this.frmIdReqPlate.Value = ReqPlateId.ToString();
      this.frmIdReq.Value = ReqId.ToString();
      this.frmCodPago.Value = codPago;
    }

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "HideModalPopup1('pload'); OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }

    private void CreatePopUpConciliation(
      string url,
      string pstrtitle,
      string width,
      string height)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUpConciliation('{0}','{1}','{2}','{3}');", (object) url, (object) pstrtitle, (object) (width + "px"), (object) (height + "px"));
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }

    private List<EstadoSolicitud> ObtenerEstados()
    {
      List<EstadoSolicitud> estadoSolicitudList = new List<EstadoSolicitud>();
      foreach (DataRow row in (InternalDataCollectionBase) new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.RequerimentStatus.ToString(),
        (object) "",
        (object) "1",
        (object) "1"
      }).Rows)
        estadoSolicitudList.Add(new EstadoSolicitud()
        {
          id = Convert.ToInt32(row["i_ParameterId"], (IFormatProvider) CultureInfo.CurrentCulture),
          descripcion = row["v_Description"].ToString()
        });
      return estadoSolicitudList;
    }

    private enum TiposDato
    {
      NroSolicitud = 1,
      CodPago = 2,
    }
  }
}
