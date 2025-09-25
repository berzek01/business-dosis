// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Delivery.BookQuery
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
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
namespace SIIV.WebApp.Delivery
{
  public class BookQuery : Page
  {
    private RequirementQueriesBL ObjRequirementQueriesBL;
    private string ParametersZone;
    private SystemUser objUserBE;
    private int i_ZoneReference;
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtPlate;
    protected Button wibSearchPlate;
    protected Label lblMessage;
    protected HtmlTableRow EnunciadoDelivery;
    protected GridView gvSchedule;
    protected Button btnJavaScriptResponse;
    protected Button btnOpenSucesfull;
    protected Button btnReturnPopupConfirmation;

    protected void Page_PreRender(object sender, EventArgs e)
    {
      if (this.ViewState["LoadNull"] != null)
      {
        if (Convert.ToInt16(this.ViewState["LoadNull"].ToString()) != (short) 1)
          return;
        this.gvSchedule.DataSource = (object) null;
        this.gvSchedule.DataBind();
        this.ViewState.Remove("LoadNull");
      }
      else
      {
        if (this.ViewState["dt_Result"] == null)
          return;
        DataTable dataTable = new DataTable();
        this.gvSchedule.DataSource = (object) (DataTable) this.ViewState["dt_Result"];
        this.gvSchedule.DataBind();
        if (this.Session["OpenSucesfull"] != null && Convert.ToInt32(this.Session["OpenSucesfull"]) == 0)
        {
          this.lblMessage.Text = "";
          this.lblMessage.Visible = false;
        }
      }
    }

    public void LoadParameters(string strDeliveryid)
    {
      try
      {
        this.ViewState["dtParametrosHorario"] = (object) ((IEnumerable<DataRow>) new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.BookQueryGroup.ToString(),
          (object) "",
          (object) "1",
          (object) "1"
        }).Select("v_ReferenceId=" + strDeliveryid)).CopyToDataTable<DataRow>();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      if (this.Request.QueryString["Plate"] != null)
      {
        this.txtPlate.Text = this.Request.QueryString["Plate"].ToString();
        this.wibSearchPlate_Click((object) null, (EventArgs) null);
        this.EnunciadoDelivery.Visible = true;
      }
      this.Session["OpenSucesfull"] = (object) 0;
    }

    protected void wibSearchPlate_Click(object sender, EventArgs e)
    {
      this.gvSchedule.Visible = false;
      this.lblMessage.Visible = false;
      this.lblMessage.Text = "";
      this.Session["OpenSucesfull"] = (object) 0;
      this.Session["iReprogramation"] = (object) 0;
      try
      {
        this.ObjRequirementQueriesBL = new RequirementQueriesBL();
        DataTable dataTable = new DataTable();
        this.ViewState["dt_Result"] = (object) null;
        if (this.txtPlate.Text.Trim() == "" || this.txtPlate.Text.Trim() == string.Empty)
        {
          this.lblMessage.Visible = true;
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Ingrese placa");
        }
        else
        {
          DataTable requirementProgramation = this.ObjRequirementQueriesBL.GetRequirementProgramation(this.txtPlate.Text.Trim());
          if (requirementProgramation.Rows.Count > 0)
          {
            this.ViewState["i_RequirementPlateId"] = (object) requirementProgramation.Rows[0]["i_RequirementPlateId"].ToString();
            this.ViewState["i_RequirementId"] = (object) requirementProgramation.Rows[0]["i_RequirementId"].ToString();
            this.ViewState["i_PlateTypeId"] = (object) requirementProgramation.Rows[0]["i_PlateTypeId"].ToString();
            this.ViewState["i_ZonaReference"] = (object) requirementProgramation.Rows[0]["i_ZoneReference"].ToString();
            this.ViewState["i_DistrictReference"] = (object) requirementProgramation.Rows[0]["i_DistrictReference"].ToString();
            this.ViewState["i_Status"] = (object) requirementProgramation.Rows[0]["i_Status"].ToString();
            if (Convert.ToInt16(requirementProgramation.Rows[0]["i_Status"].ToString()) == (short) 1 || Convert.ToInt16(requirementProgramation.Rows[0]["i_Status"].ToString()) == (short) 3)
            {
              string empty = string.Empty;
              this.CreatePopUpServerAnnouncement("SIIV - Delivery - Reprogramaciones", "../../UserControls/PopupConfirmationMessage.aspx?MessageTypeId=2&Plate=" + this.txtPlate.Text + "&Distrito=" + requirementProgramation.Rows[0]["v_DistrictName"].ToString() + "&Dia=" + requirementProgramation.Rows[0]["d_RegistrationDate"].ToString() + "&Horario=" + requirementProgramation.Rows[0]["v_Turno"].ToString(), "485px", "280px");
            }
            else if (Convert.ToInt16(requirementProgramation.Rows[0]["i_Status"].ToString()) == (short) 4)
              Message.SetMessage(this.lblMessage, enmMessageType.Warning, "La placa " + this.txtPlate.Text + "  se encuentra por recoger en AAP.");
            else if (Convert.ToInt16(requirementProgramation.Rows[0]["i_Status"].ToString()) == (short) 5)
            {
              Message.SetMessage(this.lblMessage, enmMessageType.Warning, "La placa " + this.txtPlate.Text + "  ya fue entregada al cliente.");
            }
            else
            {
              Message.SetMessage(this.lblMessage, enmMessageType.Success, "Se validó la placa correctamente");
              this.LoadShedule();
            }
          }
          else
            Message.SetMessage(this.lblMessage, enmMessageType.Warning, "La placa " + this.txtPlate.Text + " no puede ser programada, o ya tiene una programación pendiente");
        }
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, ex.Message);
      }
    }

    protected void gvSchedule_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      e.Row.HorizontalAlign = HorizontalAlign.Center;
      DataTable dataTable1 = new DataTable();
      DataTable dataTable2 = (DataTable) this.ViewState["dt_Result"];
      int num = Convert.ToInt32(dataTable2.Columns.Count) - 2;
      if (e.Row.RowType == DataControlRowType.Header)
      {
        for (int index = 2; index <= num + 1; ++index)
        {
          e.Row.Cells[0].Visible = false;
          e.Row.Cells[index].Text = Convert.ToDateTime(e.Row.Cells[index].Text).ToString("dd/MM/yyyy");
          this.ViewState["date" + index.ToString()] = (object) e.Row.Cells[index].Text;
        }
      }
      if (e.Row.RowType != DataControlRowType.DataRow)
        return;
      e.Row.Cells[0].Visible = false;
      string str = ((GridView) sender).DataKeys[e.Row.RowIndex].Value.ToString();
      Convert.ToInt32(this.ViewState["i_DistrictReference"].ToString());
      Convert.ToInt32(this.ViewState["i_ZonaReference"].ToString());
      for (int index = 2; index <= num + 1; ++index)
      {
        ImageButton child = new ImageButton();
        ImageButton imageButton = new ImageButton();
        string columnName = dataTable2.Columns[index].ColumnName;
        int int16 = (int) Convert.ToInt16(e.Row.Cells[index].Text);
        switch (int16)
        {
          case -2:
            child.ImageUrl = "../Images/cancel.png";
            child.Enabled = false;
            child.CssClass = "disabledImageButton";
            break;
          case -1:
            child.ImageUrl = "../Images/delete.png";
            if (this.Session["iReprogramation"].ToString() == "0")
            {
              child.Enabled = false;
              child.CssClass = "disabledImageButton";
              break;
            }
            break;
          default:
            child.ImageUrl = "../Images/add.png";
            break;
        }
        e.Row.Cells[index].Controls.Add((Control) child);
        if (Convert.ToInt32(this.ViewState["i_Status"]) != 0)
          child.Attributes.Add("onclick", string.Format("OpenPopUp('{0}','{1}','{2}','{3}');", (object) ("BookDelivery.aspx?i_RequirementPlateId=" + this.ViewState["i_RequirementPlateId"].ToString() + "&Zone=" + this.ViewState["i_ZonaReference"].ToString() + "&i_DistrictReference=" + this.ViewState["i_DistrictReference"].ToString() + "&Schedule=" + str + "&date=" + this.ViewState["date" + index.ToString()].ToString() + "&vstate=" + int16.ToString()), (object) "Registro de Cita", (object) "500px", (object) "340px"));
        else
          child.Attributes.Add("onclick", string.Format("OpenPopUp('{0}','{1}','{2}','{3}');", (object) ("BookDelivery.aspx?i_RequirementPlateId=" + this.ViewState["i_RequirementPlateId"].ToString() + "&Zone=" + this.ViewState["i_ZonaReference"].ToString() + "&i_DistrictReference=" + this.ViewState["i_DistrictReference"].ToString() + "&Schedule=" + str + "&date=" + this.ViewState["date" + index.ToString()].ToString() + "&vstate=" + int16.ToString() + "&RequirementId=" + this.ViewState["i_RequirementId"].ToString() + "&t=" + Convert.ToString(this.ViewState["i_PlateTypeId"].ToString())), (object) "Registro de Cita", (object) "500px", (object) "340px"));
      }
    }

    protected void btnJavaScriptResponse_Click(object sender, EventArgs e)
    {
      this.txtPlate.Text = "";
      this.lblMessage.Text = "";
      this.ViewState["LoadNull"] = (object) 1;
      this.ViewState["dt_Result"] = (object) null;
      this.gvSchedule.Visible = false;
      if (Convert.ToInt32(this.Session["OpenSucesfull"]) != 2)
        return;
      Message.SetMessage(this.lblMessage, enmMessageType.Success, "Se realizaron los cambios correctamente");
    }

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      try
      {
        this.Session["iReprogramation"] = (object) 1;
        this.LoadShedule();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
      }
    }

    protected void btnOpenSucesfull_Click(object sender, EventArgs e)
    {
      if (this.Session["OpenSucesfull"] == null)
        return;
      if (Convert.ToInt32(this.Session["OpenSucesfull"]) == 1)
        this.Response.Redirect("~/PaymentPOS/MethodPayment.aspx?RequirementId=" + this.ViewState["i_RequirementId"].ToString() + "&RequirementPlateId=" + this.ViewState["i_RequirementPlateId"].ToString() + "&t=" + Convert.ToString(this.ViewState["i_PlateTypeId"].ToString()) + "&mre=0", false);
      else
        this.gvSchedule.Visible = false;
    }

    protected void LoadShedule()
    {
      this.lblMessage.Visible = false;
      try
      {
        this.ObjRequirementQueriesBL = new RequirementQueriesBL();
        DataTable dataTable = new DataTable();
        string empty = string.Empty;
        this.ViewState["dt_Result"] = (object) this.ObjRequirementQueriesBL.GetRequirementSchedule(this.txtPlate.Text.TrimEnd());
        this.gvSchedule.Visible = true;
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, ex.Message);
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

    private void CreatePopUpServerAnnouncement(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp2('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
