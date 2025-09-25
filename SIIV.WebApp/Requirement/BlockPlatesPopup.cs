// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Requirement.BlockPlatesPopup
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Requirement
{
  public class BlockPlatesPopup : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected HtmlTableRow trEncabezado;
    protected Label lblMessage;
    protected Label lblPlate;
    protected TextBox txtPlate;
    protected FilteredTextBoxExtender txtPlateNew_FilteredTextBoxExtender;
    protected Label lblBlockId;
    protected Label lblRequirementPlateId;
    protected Label lblStatus;
    protected HtmlTableRow trfechaImp;
    protected Fecha wdpImpositionDate;
    protected HtmlTableRow trfechaUp;
    protected Label Label4;
    protected Fecha wdpUpDate;
    protected HtmlTableRow trActa;
    protected Label Label1;
    protected TextBox txtActa;
    protected HtmlTableRow trNroReso;
    protected Label Label2;
    protected TextBox txtResolucion;
    protected HtmlTableRow trRequest;
    protected Label Label3;
    protected TextBox txtRequest;
    protected Label lblObservation;
    protected TextBox txtObservation;
    protected Label lblEntity;
    protected TextBox txtEntity;
    protected HtmlTableRow trDepartment;
    protected Label lblDepartamento;
    protected DropDownList wdgDepartament2;
    protected Label lblMessage1;
    protected Button wibAceptar;
    protected Button wibCancelar;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      DataTable dataTable1 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.GroupDepartament.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      if (dataTable1 != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
          this.wdgDepartament2.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
      }
      DataTable dataTable2 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.RequerimentStatus.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      if (dataTable2 != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
          ;
      }
      string str1 = "";
      string str2 = "";
      string empty = string.Empty;
      string rawUrl = this.Request.RawUrl;
      string str3 = this.DecryptQueryString(rawUrl.Substring(rawUrl.IndexOf('?') + 1));
      int pinttotalRows = 1;
      string str4 = str3.Trim();
      this.Session["OpenSucesfull"] = (object) 0;
      this.txtPlate.Text = this.Request.QueryString["plate"].ToString();
      this.txtObservation.Text = this.Request.QueryString["Motive1"].ToString();
      if (str4 != "0")
      {
        DataTable dataTable3 = new RequirementQueriesBL().BlockPlatesUniversalQueryRead(Convert.ToInt32(str4), "", 1, 0, 1, 10, out pinttotalRows);
        string pstrPlate = dataTable3.Rows[0]["v_Plate"].ToString();
        string str5 = dataTable3.Rows[0]["i_RequirementPlateStatus"].ToString();
        str1 = dataTable3.Rows[0]["v_Entity"].ToString();
        str2 = dataTable3.Rows[0]["i_Departament"].ToString();
        if (pstrPlate != null && pstrPlate != "")
        {
          this.lblBlockId.Text = dataTable3.Rows[0]["i_BlockId"].ToString();
          this.txtPlate.Text = dataTable3.Rows[0]["v_Plate"].ToString();
          this.txtObservation.Text = dataTable3.Rows[0]["v_BlockObservation"].ToString();
          if (!(str5 != ""))
          {
            DataTable dataTable4 = new RequirementQueriesBL().GestPlatesRequirementStatus(pstrPlate);
            if (dataTable4.Rows.Count > 0)
            {
              this.lblRequirementPlateId.Text = dataTable4.Rows[0]["i_RequirementPlateId"].ToString();
              this.lblStatus.Text = dataTable4.Rows[0]["i_Status"].ToString();
              if (Convert.ToInt32(dataTable4.Rows[0]["i_Status"]) >= 0 && Convert.ToInt32(dataTable4.Rows[0]["i_Status"]) < 6)
              {
                this.trEncabezado.Visible = true;
                this.lblMessage.Text = "<h2 style='width:100%;margin-top: 0px;'>! La placa tiene una solicitud en curso con estado " + dataTable4.Rows[0]["v_Status"].ToString() + " !</h2>";
              }
            }
          }
        }
      }
      if (this.Request.QueryString["plate"] != null && this.Request.QueryString["plate"] != "")
      {
        DataTable dataTable5 = new RequirementQueriesBL().GestPlatesRequirementStatus(this.Request.QueryString["plate"]);
        if (dataTable5.Rows.Count > 0)
        {
          this.lblRequirementPlateId.Text = dataTable5.Rows[0]["i_RequirementPlateId"].ToString();
          this.lblStatus.Text = dataTable5.Rows[0]["i_Status"].ToString();
          if (Convert.ToInt32(dataTable5.Rows[0]["i_Status"]) >= 0 && Convert.ToInt32(dataTable5.Rows[0]["i_Status"]) < 6)
          {
            this.trEncabezado.Visible = true;
            this.lblMessage.Text = "<h2 style='width:100%;margin-top: 0px;'>! La placa tiene una solicitud en curso con estado " + dataTable5.Rows[0]["v_Status"].ToString() + " !</h2>";
          }
        }
      }
      if (this.lblBlockId.Text == "0")
      {
        this.trfechaImp.Visible = true;
        this.txtObservation.Focus();
        this.trActa.Visible = true;
      }
      else
      {
        this.txtObservation.Enabled = false;
        this.txtEntity.Enabled = false;
        this.wdgDepartament2.Enabled = false;
        this.txtEntity.Text = str1;
        this.wdgDepartament2.SelectedValue = str2;
        this.trfechaUp.Visible = true;
        this.trNroReso.Visible = true;
        this.trRequest.Visible = true;
        this.trDepartment.Visible = false;
      }
      if (this.txtPlate.Text == "")
      {
        this.txtPlate.Enabled = true;
        this.txtPlate.Focus();
      }
      DataTable dataTable6 = this.Session["SystemUser"] != null ? new RequirementQueriesBL().PermissionBlockPlates((this.Session["SystemUser"] as SystemUser).i_SystemUserId) : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
      foreach (DataColumn column in (InternalDataCollectionBase) dataTable6.Columns)
      {
        if (this.FindControl(dataTable6.Rows[0][column].ToString()) != null)
          this.FindControl(dataTable6.Rows[0][column].ToString()).Visible = false;
      }
    }

    public string DecryptQueryString(string strQueryString)
    {
      strQueryString = strQueryString.Split('&')[0];
      return strQueryString.Split('+')[1];
    }

    protected void wibAceptar_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.lblBlockId.Text != "0")
        {
          if (this.wdpUpDate.Text.ToString() == "" || this.wdpUpDate.Text.ToString() == "__/__/____")
          {
            Message.SetMessage(this.lblMessage1, new HandledException(1, "Debe ingresar un fecha de levantamiendo de medida preventiva."));
            return;
          }
          if (this.txtResolucion.Text.Trim() == "")
          {
            Message.SetMessage(this.lblMessage1, new HandledException(1, "Debe ingresar un Número de Resolución."));
            return;
          }
          if (this.txtRequest.Text.Trim() == "")
          {
            Message.SetMessage(this.lblMessage1, new HandledException(1, "Debe ingresar el Nombre de quien solicita el desbloqueo."));
            return;
          }
        }
        else
        {
          if (this.txtPlate.Text.Trim() == "")
          {
            Message.SetMessage(this.lblMessage1, new HandledException(1, "Debe ingresar un Número de Placa."));
            return;
          }
          if (this.wdpImpositionDate.Text.ToString() == "" || this.wdpImpositionDate.Text.ToString() == "__/__/____")
          {
            Message.SetMessage(this.lblMessage1, new HandledException(1, "Debe ingresar un fecha de Imposición."));
            return;
          }
          if (this.txtActa.Text.Trim() == "")
          {
            Message.SetMessage(this.lblMessage1, new HandledException(1, "Debe ingresar un Número de Acta."));
            return;
          }
          if (this.txtObservation.Text.Trim() == "")
          {
            Message.SetMessage(this.lblMessage1, new HandledException(1, "Debe de ingresar un motivo."));
            return;
          }
          if (this.txtEntity.Text.Trim() == "")
          {
            Message.SetMessage(this.lblMessage1, new HandledException(1, "Debe de ingresar una Entidad."));
            return;
          }
          if (new RequirementQueriesBL().BlockPlatesUniversalQueryRead(0, this.txtPlate.Text.Trim(), 1, -1, 0, 1, out int _).Rows.Count > 0)
          {
            Message.SetMessage(this.lblMessage1, new HandledException(1, "Esta placa ya se encuentra bloqueado"));
            return;
          }
        }
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
        if (!new RequirementManagementBL().BlockUnlockPlates(Convert.ToInt32(this.lblBlockId.Text.Trim()), this.txtPlate.Text.Trim().ToUpper(), this.txtActa.Text.Trim().ToUpper(), this.wdpImpositionDate.Value, this.txtResolucion.Text.Trim().ToUpper(), this.txtRequest.Text.Trim().ToUpper(), this.wdpUpDate.Value, this.lblBlockId.Text == "0" ? this.txtObservation.Text.Trim() : "", this.txtEntity.Text.Trim(), Convert.ToInt32(this.wdgDepartament2.SelectedValue), Convert.ToInt32(systemUser.i_SystemUserId), Convert.ToInt32(this.lblRequirementPlateId.Text.Trim()), Convert.ToInt32(this.lblStatus.Text), DateTime.Now, this.lblBlockId.Text == "0" ? 1 : 2))
        {
          Message.SetMessage(this.lblMessage1, new HandledException(-100, "Error al intentar bloquear la placa"));
        }
        else
        {
          this.txtPlate.Enabled = false;
          this.txtObservation.Enabled = false;
          this.txtEntity.Enabled = false;
          this.wdgDepartament2.Enabled = false;
          this.wibAceptar.Enabled = false;
          this.txtActa.Enabled = false;
          this.txtResolucion.Enabled = false;
          this.wdpImpositionDate.Enabled = false;
          this.wdpUpDate.Enabled = false;
          if (this.lblBlockId.Text != "0")
            Message.SetMessage(this.lblMessage1, enmMessageType.Success, "Se realizó el desbloqueo correctamente");
          else
            Message.SetMessage(this.lblMessage1, enmMessageType.Success, "Se realizó el bloqueo correctamente");
          this.Session["OpenSucesfull"] = (object) 1;
        }
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage1, enmMessageType.Error, ex.Message);
      }
    }

    protected void wibCancelar_Click(object sender, EventArgs e)
    {
      if (this.lblBlockId.Text != "0")
      {
        string script = "PopupClosed();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
      }
      else
      {
        string script = "PopupClosed();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
      }
    }
  }
}
