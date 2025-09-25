// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Requirement.ApproveRequirement
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
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Requirement
{
  public class ApproveRequirement : Page
  {
    private int intType;
    private int intProcess;
    private int intStatus;
    private int intStarDate;
    private int intFinishDate;
    private string strInsertUser;
    private SystemUser objUserBE;
    protected UpdatePanel updatePanel;
    protected HtmlTable tbTable;
    protected DropDownList cboType;
    protected DropDownList cboStatus;
    protected Fecha cboStartDate;
    protected DropDownList cboProcess;
    protected TextBox txtInsertUser;
    protected Fecha cboFinishDate;
    protected TextBox TxtOficio;
    protected Button btnSeach;
    protected GridView gvList;
    protected Label lblCount;
    protected Label lblMessage;
    protected TextBox txtObservations;
    protected Button WebImageButton1;
    protected Button WebImageButton2;
    protected Button WebImageButton3;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      this.Initialize();
      this.lblMessage.Visible = false;
    }

    protected void btnSeach_Click(object sender, EventArgs e)
    {
      try
      {
        string ErrorMessage = this.SearchRequirement();
        if (ErrorMessage.Length > 0)
          throw new HandledException(1, ErrorMessage);
        this.RefreshList();
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
        this.HidePopup();
      }
    }

    protected void gvList_PageIndexChanged(object sender, EventArgs e)
    {
      this.RemeberOldValues();
      this.RefreshList();
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
      try
      {
        RequirementManagementBL requirementManagementBl = new RequirementManagementBL();
        this.RemeberOldValues();
        this.objUserBE = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - ApproveRequirement.aspx");
        DataTable dataTable = this.FilterData("boolColumn=True");
        if (dataTable.Rows.Count <= 0)
          throw new HandledException(1, Constants.REQUIREMENT_ADVERTENCIA_Seleccionar_Solicitudes);
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (Convert.ToInt32(row["type"], (IFormatProvider) CultureInfo.CurrentCulture) == 1)
            requirementManagementBl.UpdateSpecialRequirmentStatus(Convert.ToInt32(row["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(row["ID"], (IFormatProvider) CultureInfo.CurrentCulture), -1, -1, this.txtObservations.Text.Trim(), this.objUserBE.i_SystemUserId);
          else if (Convert.ToInt32(row["type"], (IFormatProvider) CultureInfo.CurrentCulture) == 2)
            requirementManagementBl.UpdateExceptionalRequirmentStatus(Convert.ToInt32(row["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(row["ID"], (IFormatProvider) CultureInfo.CurrentCulture), -1, -1, this.txtObservations.Text.Trim(), this.objUserBE.i_SystemUserId);
        }
        string str = this.SearchRequirement();
        if (str.Length != 0)
          throw new HandledException(1, Constants.REQUIREMENT_ERROR_GENERICO + str);
        this.RefreshList();
        this.txtObservations.Text = "";
        this.lblMessage.Text = "";
        Message.SetMessage(this.lblMessage, new HandledException(2, Constants.REQUIREMENT_EXITO_Cambios_Actualizados));
        dataTable.Dispose();
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
        this.HidePopup();
      }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        RequirementManagementBL requirementManagementBl = new RequirementManagementBL();
        this.RemeberOldValues();
        this.objUserBE = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - ApproveRequirement.aspx");
        DataTable dataTable = this.FilterData("boolColumn=True");
        if (dataTable.Rows.Count <= 0)
          throw new HandledException(1, Constants.REQUIREMENT_ADVERTENCIA_Seleccionar_Solicitudes);
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (Convert.ToInt32(row["type"], (IFormatProvider) CultureInfo.CurrentCulture) == 1)
            requirementManagementBl.UpdateSpecialRequirmentStatus(Convert.ToInt32(row["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(row["ID"], (IFormatProvider) CultureInfo.CurrentCulture), 1, 1, this.txtObservations.Text.Trim(), this.objUserBE.i_SystemUserId);
          else if (Convert.ToInt32(row["type"], (IFormatProvider) CultureInfo.CurrentCulture) == 2)
            requirementManagementBl.UpdateExceptionalRequirmentStatus(Convert.ToInt32(row["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(row["ID"], (IFormatProvider) CultureInfo.CurrentCulture), 1, 1, this.txtObservations.Text.Trim(), this.objUserBE.i_SystemUserId);
        }
        string str = this.SearchRequirement();
        if (str.Length != 0)
          throw new HandledException(1, Constants.REQUIREMENT_ERROR_GENERICO + str);
        this.RefreshList();
        this.txtObservations.Text = "";
        this.lblMessage.Text = "";
        Message.SetMessage(this.lblMessage, new HandledException(2, Constants.REQUIREMENT_EXITO_Cambios_Actualizados));
        dataTable.Dispose();
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
        this.HidePopup();
      }
    }

    public void Initialize()
    {
      try
      {
        this.getProccess();
        this.cboStartDate.Value = DateTime.Now;
        this.cboFinishDate.Value = DateTime.Now;
        this.SearchRequirement();
        this.RefreshList();
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

    public string SearchRequirement()
    {
      try
      {
        string str = "";
        this.intType = Convert.ToInt32(this.cboType.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        this.intProcess = Convert.ToInt32(this.cboProcess.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        this.intFinishDate = Convert.ToInt32(this.cboFinishDate.Value.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        this.intStarDate = Convert.ToInt32(this.cboStartDate.Value.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        this.intStatus = Convert.ToInt32(this.cboStatus.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        this.strInsertUser = this.txtInsertUser.Text.Trim();
        string strOficio = this.TxtOficio.Text.Trim();
        this.Session["DataListApproved"] = (object) null;
        this.Session["CHECKED_ITEMS"] = (object) null;
        DataTable approvedRequirement = new RequirementQueriesBL().GetApprovedRequirement(this.intType, this.intStarDate, this.intFinishDate, this.intStatus, this.intProcess, this.strInsertUser, strOficio);
        approvedRequirement.Columns.Add(new DataColumn("boolColumn", typeof (bool))
        {
          DefaultValue = (object) false
        });
        this.Session["DataListApproved"] = (object) approvedRequirement;
        return str;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void RefreshList()
    {
      try
      {
        this.gvList.DataSource = this.Session["DataListApproved"] != null ? (object) (this.Session["DataListApproved"] as DataTable) : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - ApproveRequirement.aspx");
        this.gvList.DataBind();
        Message.SetMessage(this.lblMessage, new HandledException(2, Constants.SEARCHRESULT_OK.Replace("XX", (this.Session["DataListApproved"] as DataTable).Rows.Count.ToString())));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void getProccess()
    {
      try
      {
        this.cboProcess.DataSource = (object) new RequirementQueriesBL().GetProcess();
        this.cboProcess.DataValueField = "i_ParameterId";
        this.cboProcess.DataTextField = "v_Description";
        this.cboProcess.DataBind();
        this.cboProcess.SelectedIndex = 0;
        this.cboProcess.Enabled = true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void gvList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        if (e.CommandName.Equals("Check", StringComparison.CurrentCulture))
        {
          ImageButton imageButton = sender as ImageButton;
          int int32 = Convert.ToInt32(this.gvList.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text, (IFormatProvider) CultureInfo.CurrentCulture);
          foreach (DataRow row in (InternalDataCollectionBase) (this.Session["DataListApproved"] as DataTable).Rows)
          {
            if (Convert.ToInt32(row["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture) == int32)
            {
              if (Convert.ToBoolean(row["boolColumn"], (IFormatProvider) CultureInfo.CurrentCulture))
              {
                row["boolColumn"] = (object) false;
                imageButton.ImageUrl = "~/Images/Design/checkbox_unchecked_16.png";
                break;
              }
              row["boolColumn"] = (object) true;
              imageButton.ImageUrl = "~/Images/Design/checkbox_checked_16.png";
              break;
            }
          }
          this.RefreshList();
        }
        else if (!e.CommandName.Equals("chkCheck", StringComparison.CurrentCulture))
          ;
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

    public DataTable FilterData(string filter)
    {
      try
      {
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = this.Session["DataListApproved"] as DataTable;
        DataTable dataTable3 = dataTable2.Clone();
        foreach (DataRow row in dataTable2.Select(filter))
          dataTable3.ImportRow(row);
        dataTable2.Dispose();
        return dataTable3;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void RemeberOldValues()
    {
      try
      {
        ArrayList arrayList = new ArrayList();
        foreach (GridViewRow row in this.gvList.Rows)
        {
          CheckBox control = (CheckBox) row.FindControl("chkCheck");
          if (control != null)
          {
            int int32 = Convert.ToInt32(this.gvList.Rows[row.RowIndex].Cells[1].Text);
            bool flag = control.Checked;
            if (this.Session["CHECKED_ITEMS"] != null)
              arrayList = (ArrayList) this.Session["CHECKED_ITEMS"];
            if (flag)
            {
              if (!arrayList.Contains((object) int32))
                arrayList.Add((object) int32);
            }
            else
              arrayList.Remove((object) int32);
          }
        }
        if (arrayList != null && arrayList.Count > 0)
          this.Session["CHECKED_ITEMS"] = (object) arrayList;
        DataTable dataTable = this.Session["DataListApproved"] as DataTable;
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          dataTable.Rows[index]["boolColumn"] = (object) false;
        if (arrayList != null && arrayList.Count > 0)
        {
          for (int index = 0; index < arrayList.Count; ++index)
            dataTable.Select("i_RequirementPlateId=" + arrayList[index]?.ToString())[0]["boolColumn"] = (object) true;
        }
        this.Session["DataListApproved"] = (object) dataTable;
        dataTable.Dispose();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void RePopulateValues()
    {
      ArrayList arrayList = (ArrayList) this.Session["CHECKED_ITEMS"];
      if (arrayList == null || arrayList.Count <= 0)
        return;
      foreach (GridViewRow row in this.gvList.Rows)
      {
        int int32 = Convert.ToInt32(this.gvList.Rows[row.RowIndex].Cells[1].Text);
        if (arrayList.Contains((object) int32))
          ((CheckBox) row.FindControl("chkCheck")).Checked = true;
      }
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatePanel, this.updatePanel.GetType(), "Script", script, true);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("../default.aspx");
    }
  }
}
