// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Delivery.BookDelivery
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Delivery
{
  public class BookDelivery : Page
  {
    private string ParametersZone;
    private string ParametersDistrict;
    private string ParametersZoneReferenceId;
    private RequirementQueriesBL ObjRequirementQueriesBL;
    private RequirementProgramation ObjRequirementProgramation;
    private RequirementManagementBL ObjRequirementManagementBL;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtIds;
    protected DropDownList ddlDistrict;
    protected Fecha Fecha1;
    protected DropDownList ddlHorario;
    protected HiddenField HiddenField1;
    protected Label lblMessage1;
    protected Label lblReprogramation;
    protected Button wibNew;
    protected Button wibAceptar;
    protected Button wibCancelar;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.ParametersZoneReferenceId = string.Empty;
      this.LoadSchedule(this.ParametersZoneReferenceId);
      this.ParametersDistrict = string.Empty;
      this.LoadDistrict(this.ParametersDistrict);
      if (this.Request.QueryString["i_RequirementPlateId"] != null && this.Request.QueryString["i_DistrictReference"] != null && this.Request.QueryString["Zone"] != null && this.Request.QueryString["Schedule"] != null && this.Request.QueryString["date"] != null)
      {
        this.txtIds.Text = this.Request.QueryString["i_RequirementPlateId"].ToString();
        this.ddlDistrict.SelectedValue = this.Request.QueryString["i_DistrictReference"].ToString();
        this.ViewState["Zone"] = (object) this.Request.QueryString["Zone"].ToString();
        string str = this.Request.QueryString["Schedule"].ToString();
        this.Fecha1.Value = Convert.ToDateTime(this.Request.QueryString["date"].ToString());
        this.ddlHorario.SelectedValue = str;
      }
      if (this.Session["iReprogramation"].ToString() == "1" && this.Request.QueryString["vstate"] == "-1")
      {
        this.lblReprogramation.Visible = true;
        Message.SetMessage(this.lblReprogramation, enmMessageType.Warning, this.lblReprogramation.Text);
      }
      else
        this.lblReprogramation.Visible = false;
      this.ParametersZone = string.Empty;
    }

    protected void wibAceptar_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.HiddenField1.Value == "1")
          return;
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        int i_StatusProgramationOut = 0;
        this.ObjRequirementProgramation = new RequirementProgramation();
        this.ObjRequirementManagementBL = new RequirementManagementBL();
        this.ObjRequirementProgramation.i_RequirementPlateId = Convert.ToInt32(this.txtIds.Text.TrimEnd());
        this.ObjRequirementProgramation.i_BlockTimeReference = new int?(Convert.ToInt32(this.ddlHorario.SelectedValue));
        this.ObjRequirementProgramation.d_RegistrationDate = new DateTime?(this.Fecha1.Value);
        this.ObjRequirementProgramation.i_UpdateUserId = new int?(systemUser.i_SystemUserId);
        int num = this.ObjRequirementManagementBL.RequirementProgramationUpdate(this.ObjRequirementProgramation, out i_StatusProgramationOut);
        if (num > 0)
        {
          Message.SetMessage(this.lblMessage1, enmMessageType.Success, "Se realizó correctamente la cita");
          if (num == 1)
            this.Session["OpenSucesfull"] = (object) 1;
          if (num == 2)
            this.Session["OpenSucesfull"] = (object) 2;
          this.wibCancelar_Click((object) null, (EventArgs) null);
        }
        else
          Message.SetMessage(this.lblMessage1, enmMessageType.Warning, "No se pudo registrar la cita");
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage1, enmMessageType.Error, ex.Message);
      }
    }

    protected void wibCancelar_Click(object sender, EventArgs e)
    {
      string script = "SendInfoPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void wibNew_Click(object sender, EventArgs e)
    {
      this.lblMessage1.Visible = false;
      this.txtIds.Text = string.Empty;
      this.ddlDistrict.SelectedValue = "-1";
      this.Fecha1.Text = string.Empty;
      this.Fecha1.Enabled = false;
      this.ddlHorario.SelectedValue = "-1";
      this.ddlHorario.Enabled = false;
      this.wibAceptar.Enabled = false;
    }

    protected void LoadDistrict(string ParametersZone)
    {
      try
      {
        DataTable byReference = new SystemParameterQueriesBL().GetByReference(new ArrayList()
        {
          (object) ("" + SystemParameterGroups.District.ToString()),
          (object) ParametersZone,
          (object) "",
          (object) ""
        });
        if (byReference != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) byReference.Rows)
          {
            if (row["i_GroupId"].ToString() == SystemParameterGroups.District.ToString())
              this.ddlDistrict.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          }
        }
        this.ddlDistrict.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
        this.ddlDistrict.SelectedValue = "-1";
        this.ddlDistrict.EnableViewState = true;
      }
      catch (Exception ex)
      {
        throw new Exception("OCURRIÓ UN ERROR AL INTENTAR CARGAR LOS DISTRITOS.");
      }
    }

    protected void LoadSchedule(string ParametersZoneReferenceId)
    {
      try
      {
        this.ddlHorario.Items.Clear();
        DataTable byReference = new SystemParameterQueriesBL().GetByReference(new ArrayList()
        {
          (object) ("" + SystemParameterGroups.BlockSchedule.ToString()),
          (object) ParametersZoneReferenceId,
          (object) "",
          (object) ""
        });
        if (byReference != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) byReference.Rows)
          {
            if (row["i_GroupId"].ToString() == SystemParameterGroups.BlockSchedule.ToString())
              this.ddlHorario.Items.Add(new ListItem(row["v_Description"].ToString() + " " + row["v_Value"].ToString(), row["i_ParameterId"].ToString()));
          }
        }
        this.ddlHorario.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
        this.ddlHorario.SelectedValue = "-1";
      }
      catch (Exception ex)
      {
        throw new Exception("OCURRIÓ UN ERROR AL INTENTAR CARGAR LOS HORARIOS.");
      }
    }
  }
}
