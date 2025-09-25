// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.UserControls.PopupIncident
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE.CustomCode;
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
namespace SIIV.WebApp.UserControls
{
  public class PopupIncident : Page
  {
    public string strfnscript;
    public int i_RequirementId;
    public int i_RequirementPlateId;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected Label lblMensaje;
    protected DropDownList wddIncidentType;
    protected Label lblMessage;
    protected Button btnYes;
    protected Button btnCerrar;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.IsPostBack)
        return;
      this.Session["i_RequirementId"] = (object) Convert.ToString(this.Request.QueryString["RequirementId"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.Session["i_RequirementPlateId"] = (object) Convert.ToString(this.Request.QueryString["RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.lblMensaje.Text = "Seleccione el motivo por el cual la placa no fue entregada: ";
      this.LoadIncidents();
    }

    protected void LoadIncidents()
    {
      try
      {
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) ("" + SystemParameterGroups.IncidentsType.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
          (object) "",
          (object) "1",
          (object) "1"
        });
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["i_GroupId"].ToString() == SystemParameterGroups.IncidentsType.ToString((IFormatProvider) CultureInfo.CurrentCulture))
              this.wddIncidentType.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          }
        }
        this.wddIncidentType.Items.Insert(0, new ListItem("- Seleccione - ", "-1"));
        this.wddIncidentType.SelectedValue = "-1";
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void Yes_Click(object sender, EventArgs e)
    {
      if (this.wddIncidentType.SelectedValue == "-1")
      {
        this.lblMessage.Visible = true;
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Debe ingresar una opción de la lista.");
      }
      else
      {
        this.lblMessage.Visible = false;
        this.lblMessage.Text = "";
        this.i_RequirementId = Convert.ToInt32(this.Session["i_RequirementId"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.i_RequirementPlateId = Convert.ToInt32(this.Session["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture);
        new RequirementManagementBL().IncidentRequirementInsert(new Incident()
        {
          i_Reason = Convert.ToInt32(this.wddIncidentType.SelectedValue),
          i_RequirementId = this.i_RequirementId,
          i_RequirementPlateId = this.i_RequirementPlateId,
          d_IncidentDate = DateTime.Now
        });
        this.btnYes.Visible = false;
        this.btnCerrar.Visible = true;
        this.wddIncidentType.Enabled = false;
        this.lblMessage.Visible = true;
        Message.SetMessage(this.lblMessage, enmMessageType.Success, "***ÉXITO***</br>El motivo de no entrega ha sido guardado con éxito.");
      }
    }

    protected void Close_Click(object sender, EventArgs e)
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
