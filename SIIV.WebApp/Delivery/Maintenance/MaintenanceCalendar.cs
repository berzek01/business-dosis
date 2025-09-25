// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Delivery.Maintenance.MaintenanceCalendar
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
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Delivery.Maintenance
{
  public class MaintenanceCalendar : Page
  {
    public SystemUser objUserBE;
    protected UpdatePanel UpdatePanel1;
    protected Fecha wdpDate;
    protected DropDownList wddBlockShedule;
    protected DropDownList wddZone;
    protected Button wibSearch;
    protected GridView wdgList;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      this.LoadBlockShedule();
      this.wddZone.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
      this.wddZone.SelectedIndex = 0;
      this.wdpDate.Value = DateTime.Now;
    }

    protected void wddBlockShedule_SelectedIndexChanged1(object sender, EventArgs e)
    {
      try
      {
        DataTable blockShedule = new RequirementQueriesBL().GetBlockShedule(Convert.ToInt32(this.wddBlockShedule.SelectedValue.ToString()));
        if (blockShedule.Rows.Count <= 0)
          return;
        blockShedule.Rows[0]["v_ReferenceId"].ToString();
        this.LoadBlockSheduleZoneList(blockShedule.Rows[0]["v_ReferenceId"].ToString());
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

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        this.wdgList.DataSource = (object) null;
        this.wdgList.DataBind();
        if (this.wddBlockShedule.SelectedValue != "-1" && this.wddZone.SelectedValue != "0")
          this.SearchBlockSheduleZone();
        else
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Seleccionar un Bloque Horario y una Zona Horaria");
      }
      catch (HandledException ex)
      {
        this.HidePopup();
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        this.HidePopup();
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void wdgList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        int int32_1 = Convert.ToInt32(e.CommandArgument);
        GridViewRow row1 = this.wdgList.Rows[int32_1];
        if (row1 == null)
          throw new HandledException(4, "Error de selección.", "'wdgList' - MaintenanceCalendar.aspx");
        DateTime dateTime = Convert.ToDateTime(this.wdpDate.Value);
        int int32_2 = Convert.ToInt32(this.wdgList.DataKeys[int32_1]["i_BlockTimeReference"].ToString());
        int int32_3 = Convert.ToInt32(this.wdgList.DataKeys[int32_1]["i_ZoneReference"].ToString());
        int int32_4 = Convert.ToInt32(row1.Cells[7].Text.ToString());
        int int32_5 = Convert.ToInt32(row1.Cells[8].Text.ToString());
        int num1 = 0;
        if (this.ViewState["dtBlockSheduleZone"] != null)
        {
          foreach (DataRow row2 in (InternalDataCollectionBase) ((DataTable) this.ViewState["dtBlockSheduleZone"]).Rows)
          {
            if (Convert.ToInt32(row2["i_ParameterId"]) == int32_3)
              num1 = Convert.ToInt32(row2["v_OldValue"]);
          }
          DataTable dataTable = new DataTable();
          int num2 = 0;
          if (e.CommandName == "Habilitar")
          {
            if (int32_4 + int32_5 < num1)
            {
              Message.SetMessage(this.lblMessage, enmMessageType.Warning, "La fecha, bloque horario y zona ya se encuentran habilitados.");
            }
            else
            {
              if (int32_4 + int32_5 == num1)
              {
                if (int32_4 < num1 && int32_5 > 0)
                  num2 = new RequirementManagementBL().EnabledDisabledProgramation(dateTime, int32_2, int32_3, "01");
                else if (int32_4 == num1 && int32_5 == 0)
                {
                  Message.SetMessage(this.lblMessage, enmMessageType.Warning, "La fecha, bloque horario y zona no se puede habilitar ya que cuenta con todas las solicitudes programadas.");
                  return;
                }
              }
              if (num2 <= 0)
                return;
              this.wibSearch_Click((object) null, (EventArgs) null);
              Message.SetMessage(this.lblMessage, enmMessageType.Success, "La fecha, bloque horario y zona se habilitó satisfactoriamente.");
            }
          }
          else
          {
            if (!(e.CommandName == "DesHabilitar"))
              return;
            if (int32_4 + int32_5 == num1)
            {
              Message.SetMessage(this.lblMessage, enmMessageType.Warning, "La fecha, bloque horario y zona ya se encuentran deshabilitados.");
            }
            else
            {
              if (int32_4 + int32_5 < num1 && int32_4 < num1 && int32_5 == 0)
                num2 = new RequirementManagementBL().EnabledDisabledProgramation(dateTime, int32_2, int32_3, "02");
              if (num2 > 0)
              {
                this.wibSearch_Click((object) null, (EventArgs) null);
                Message.SetMessage(this.lblMessage, enmMessageType.Success, "La fecha, bloque horario y zona se deshabilitó satisfactoriamente.");
              }
            }
          }
        }
        else
          Message.SetMessage(this.lblMessage, enmMessageType.Error, "**** Error al cargar las zonas.");
      }
      catch (HandledException ex)
      {
        this.HidePopup();
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        this.HidePopup();
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void LoadBlockShedule()
    {
      try
      {
        this.objUserBE = this.Session["SystemUser"] as SystemUser;
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.BlockSchedule.ToString(),
          (object) "",
          (object) "1",
          (object) "1"
        });
        List<DataRow> dataRowList = new List<DataRow>();
        int iLocationId = this.objUserBE.i_LocationId;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (Convert.ToInt32(row["v_OldValue"]) != iLocationId)
            dataRowList.Add(row);
        }
        foreach (DataRow row in dataRowList)
          dataTable.Rows.Remove(row);
        this.wddBlockShedule.DataSource = (object) dataTable;
        this.wddBlockShedule.DataTextField = "v_Description";
        this.wddBlockShedule.DataValueField = "i_ParameterId";
        this.wddBlockShedule.DataBind();
        this.wddBlockShedule.Items.Insert(0, new ListItem("-- Seleccione --", "-1"));
        this.wddBlockShedule.SelectedIndex = 0;
      }
      catch (Exception ex)
      {
        throw new Exception("OCURRIÓ UN ERROR AL INTENTAR CARGAR LOS BLOQUES DE HORARIO.");
      }
    }

    private void LoadBlockSheduleZoneList(string v_ReferenceId)
    {
      try
      {
        DataTable dataTable = this.LoadBlockSheduleZone(v_ReferenceId);
        this.wddZone.DataSource = (object) dataTable;
        this.ViewState["dtBlockSheduleZone"] = (object) dataTable;
        DataRow row = dataTable.NewRow();
        row["i_ParameterId"] = (object) 0;
        row["v_Description"] = (object) "- Seleccione -";
        dataTable.Rows.InsertAt(row, 0);
        this.wddZone.DataTextField = "v_Description";
        this.wddZone.DataValueField = "i_ParameterId";
        this.wddZone.DataBind();
        this.wddZone.SelectedValue = "0";
      }
      catch (Exception ex)
      {
        throw new Exception("OCURRIÓ UN ERROR AL INTENTAR CARGAR LOS BLOQUES DE HORARIO.");
      }
    }

    private DataTable LoadBlockSheduleZone(string v_ReferenceId)
    {
      return new RequirementQueriesBL().GetBlockSheduleZone(v_ReferenceId);
    }

    private void SearchBlockSheduleZone()
    {
      try
      {
        this.SearchBlockSheduleZoneList(Convert.ToDateTime(this.wdpDate.Value), Convert.ToInt32(this.wddBlockShedule.SelectedValue.ToString()), Convert.ToInt32(this.wddZone.SelectedValue.ToString()));
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, ex.Message);
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SearchBlockSheduleZoneList(DateTime dt_Date, int i_BlockSheduleId, int i_ZoneId)
    {
      DataTable dataTable = new RequirementQueriesBL().SearchBlockSheduleZone(dt_Date, i_BlockSheduleId, i_ZoneId);
      if (dataTable == null || dataTable.Rows.Count == 0)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
        this.HidePopup();
      }
      else
        this.lblMessage.Visible = false;
      this.wdgList.DataSource = (object) dataTable;
      this.wdgList.DataBind();
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void wdgList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
  }
}
