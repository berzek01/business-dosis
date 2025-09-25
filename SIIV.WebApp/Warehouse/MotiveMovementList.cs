// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.MotiveMovementList
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse
{
  public class MotiveMovementList : Page
  {
    private MaintenanceOperation currentOperation = MaintenanceOperation.None;
    protected HiddenField H1;
    protected UpdatePanel UpdatePanel1;
    protected DropDownList wddWareHouseTypeUse;
    protected Button wibSearch;
    protected GridView wdgMotiveMovementList;
    protected Pager custPagerBatch;
    protected Button wibNew;
    protected Label lblMessage;
    protected UpdatePanel UpdatePanel2;
    protected DropDownList cboWareHouseTypeUse;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected ValidatorCalloutExtender ValidatorCalloutExtender6;
    protected DropDownList cboFlow;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender1;
    protected TextBox txtMotive;
    protected DropDownList cboMotiveSunat;
    protected DropDownList cboTypeMotive;
    protected RequiredFieldValidator RequiredFieldValidator3;
    protected ValidatorCalloutExtender ValidatorCalloutExtender2;
    protected HtmlTableRow trManagementButtons;
    protected Button wibSave;
    protected Button wibCancel;
    protected HtmlTableRow trwibFinalize;
    protected Button wibFinalize;
    protected HiddenField hidMotiveMovementId;
    protected Label lblMessage1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.LoadParameters();
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
        this.SearchMotiveMovement();
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

    protected void wdgMotiveMovementList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        this.ViewState["indexwdgMotiveMovementList"] = (object) Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Edit")
        {
          this.currentOperation = MaintenanceOperation.Edit;
          this.ShowMotiveMovementInfo(this.GetCurrentMotiveMovement(enmTypeLoadData.SelectedRowsGrid));
          this.EnabledControls(MaintenanceOperation.Edit);
        }
        else if (e.CommandName == "Delete")
        {
          this.currentOperation = MaintenanceOperation.Delete;
          this.ShowMotiveMovementInfo(this.GetCurrentMotiveMovement(enmTypeLoadData.SelectedRowsGrid));
          this.EnabledControls(MaintenanceOperation.Delete);
        }
        this.ViewState["currentOperation"] = (object) this.currentOperation;
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

    protected void wibNew_Click(object sender, EventArgs e)
    {
      try
      {
        this.ClearControls();
        this.hidMotiveMovementId.Value = string.Empty;
        this.currentOperation = MaintenanceOperation.AddNew;
        this.ViewState["currentOperation"] = (object) this.currentOperation;
        this.EnabledControls(MaintenanceOperation.AddNew);
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

    protected void wibFinalize_Click(object sender, EventArgs e)
    {
      try
      {
        string script1 = UtilDA.ActiveTabIndex("tabs", 0, "1");
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script1, true);
        string script2 = "TabIndex();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "ScriptIndex", script2, true);
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage1, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage1, new HandledException(-100, ex));
      }
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      try
      {
        this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperation"];
        switch (this.currentOperation)
        {
          case MaintenanceOperation.AddNew:
            new MotiveMovementManagementBL().MotiveMovementInsertUpdate(this.GetCurrentMotiveMovement(enmTypeLoadData.SelectedTextbox));
            Message.SetMessage(this.lblMessage1, new HandledException(2, "<br>•&nbsp;El Motivo se creo satisfactoriamente."));
            this.SearchMotiveMovement();
            this.EnabledControls(MaintenanceOperation.AddNew);
            break;
          case MaintenanceOperation.Edit:
            new MotiveMovementManagementBL().MotiveMovementInsertUpdate(this.GetCurrentMotiveMovement(enmTypeLoadData.SelectedTextbox));
            Message.SetMessage(this.lblMessage1, new HandledException(2, "<br>•&nbsp;El Motivo se Modifico satisfactoriamente."));
            this.SearchMotiveMovement();
            this.EnabledControls(MaintenanceOperation.Edit);
            break;
          case MaintenanceOperation.Delete:
            new MotiveMovementManagementBL().MotiveMovementDelete(Convert.ToInt32(this.hidMotiveMovementId.Value, (IFormatProvider) CultureInfo.CurrentCulture));
            Message.SetMessage(this.lblMessage1, new HandledException(2, "<br>•&nbsp;El Motivo se Elimino satisfactoriamente."));
            this.SearchMotiveMovement();
            this.EnabledControls(MaintenanceOperation.Delete);
            break;
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage1, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage1, new HandledException(-100, ex));
      }
    }

    protected void wibCancel_Click(object sender, EventArgs e)
    {
      try
      {
        this.EnabledControls(MaintenanceOperation.None);
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage1, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage1, new HandledException(-100, ex));
      }
    }

    protected void wdgMotiveMovementList_PageIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.SearchMotiveMovement();
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

    protected void custPagerBatch_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        this.SearchMotiveMovementList(0, Convert.ToInt32(this.wddWareHouseTypeUse.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), "", 0, Convert.ToInt32((object) (this.Session["SystemUser"] as SystemUser).i_CompanyId, (IFormatProvider) CultureInfo.CurrentCulture), false);
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

    private void ShowMotiveMovementInfo(MovementMotive pobjMotiveMovement)
    {
      try
      {
        this.hidMotiveMovementId.Value = pobjMotiveMovement.i_MotiveMovementId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        DropDownList wareHouseTypeUse = this.cboWareHouseTypeUse;
        int? nullable = pobjMotiveMovement.i_wareHouseTypeUseId;
        string str1;
        if (!(nullable.ToString() == string.Empty))
        {
          nullable = pobjMotiveMovement.i_wareHouseTypeUseId;
          str1 = nullable.ToString();
        }
        else
          str1 = "-1";
        wareHouseTypeUse.SelectedValue = str1;
        DropDownList cboFlow = this.cboFlow;
        nullable = pobjMotiveMovement.i_FlowId;
        string str2;
        if (!(nullable.ToString() == string.Empty))
        {
          nullable = pobjMotiveMovement.i_FlowId;
          str2 = nullable.ToString();
        }
        else
          str2 = "-1";
        cboFlow.SelectedValue = str2;
        this.txtMotive.Text = pobjMotiveMovement.v_Motive;
        try
        {
          DropDownList cboMotiveSunat = this.cboMotiveSunat;
          nullable = pobjMotiveMovement.i_MotiveMovementSunatId;
          string str3;
          if (!(nullable.ToString() == string.Empty))
          {
            nullable = pobjMotiveMovement.i_MotiveMovementSunatId;
            str3 = nullable.ToString();
          }
          else
            str3 = "-1";
          cboMotiveSunat.SelectedValue = str3;
        }
        catch
        {
          this.cboMotiveSunat.SelectedValue = "-1";
        }
        string empty = string.Empty;
        foreach (ListItem listItem in this.cboTypeMotive.Items)
        {
          if (listItem.Text == Convert.ToString(pobjMotiveMovement.v_TypeMotive))
            empty = listItem.Value;
        }
        this.cboTypeMotive.SelectedValue = empty == string.Empty ? "-1" : empty;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private MovementMotive GetCurrentMotiveMovement(enmTypeLoadData penuTypeLoadData)
    {
      try
      {
        MovementMotive currentMotiveMovement = new MovementMotive();
        int int32 = Convert.ToInt32(this.ViewState["indexwdgMotiveMovementList"]);
        switch (penuTypeLoadData)
        {
          case enmTypeLoadData.SelectedRowsGrid:
            GridViewRow row = this.wdgMotiveMovementList.Rows[int32];
            currentMotiveMovement.i_MotiveMovementId = int.Parse(this.wdgMotiveMovementList.DataKeys[int32]["i_MotiveMovementId"].ToString());
            currentMotiveMovement.i_wareHouseTypeUseId = new int?(int.Parse(this.wdgMotiveMovementList.DataKeys[int32]["i_WareHouseTypeuseId"].ToString()));
            currentMotiveMovement.i_FlowId = new int?(int.Parse(this.wdgMotiveMovementList.DataKeys[int32]["i_flowId"].ToString()));
            currentMotiveMovement.v_Motive = row.Cells[4].Text;
            currentMotiveMovement.i_MotiveMovementSunatId = new int?(int.Parse(this.wdgMotiveMovementList.DataKeys[int32]["i_MotiveMovementSunatId"].ToString()));
            currentMotiveMovement.v_TypeMotive = row.Cells[6].Text;
            break;
          case enmTypeLoadData.SelectedTextbox:
            if (!string.IsNullOrWhiteSpace(this.hidMotiveMovementId.Value))
              currentMotiveMovement.i_MotiveMovementId = Convert.ToInt32(this.hidMotiveMovementId.Value, (IFormatProvider) CultureInfo.CurrentCulture);
            currentMotiveMovement.i_wareHouseTypeUseId = new int?(Convert.ToInt32(this.cboWareHouseTypeUse.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
            currentMotiveMovement.i_FlowId = new int?(Convert.ToInt32(this.cboFlow.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
            currentMotiveMovement.v_Motive = this.txtMotive.Text;
            currentMotiveMovement.i_MotiveMovementSunatId = new int?(Convert.ToInt32(this.cboMotiveSunat.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
            currentMotiveMovement.v_TypeMotive = this.cboTypeMotive.SelectedItem.Text;
            break;
        }
        return currentMotiveMovement;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadParameters()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - MotiveMovementList.aspx");
        SystemParameterQueriesBL parameterQueriesBl = new SystemParameterQueriesBL();
        ArrayList arrFilter1 = new ArrayList()
        {
          (object) "320",
          (object) string.Empty,
          (object) 1,
          (object) 1
        };
        DataTable dataTable = parameterQueriesBl.GetbyFilter(arrFilter1);
        this.wddWareHouseTypeUse.DataSource = (object) dataTable;
        this.wddWareHouseTypeUse.DataBind();
        this.wddWareHouseTypeUse.SelectedIndex = 0;
        this.cboWareHouseTypeUse.DataSource = (object) dataTable;
        this.cboWareHouseTypeUse.DataBind();
        this.cboWareHouseTypeUse.SelectedIndex = 0;
        ArrayList arrFilter2 = new ArrayList()
        {
          (object) "518",
          (object) string.Empty,
          (object) 1,
          (object) 1
        };
        foreach (DataRow row in (InternalDataCollectionBase) parameterQueriesBl.GetbyFilter(arrFilter2).Rows)
          this.cboMotiveSunat.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        this.cboWareHouseTypeUse.Items.Insert(0, new ListItem("- Seleccione - ", "-1"));
        this.cboMotiveSunat.Items.Insert(0, new ListItem("- Seleccione - ", "-1"));
        this.wddWareHouseTypeUse.Items.Insert(0, new ListItem("- Seleccione -", "-1"));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchMotiveMovement()
    {
      try
      {
        this.SearchMotiveMovementList(0, Convert.ToInt32(this.wddWareHouseTypeUse.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), "", 0, Convert.ToInt32((object) (this.Session["SystemUser"] as SystemUser).i_CompanyId, (IFormatProvider) CultureInfo.CurrentCulture), true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchMotiveMovementList(
      int pintMotiveMovementId,
      int pintWareHouseTypeUseId,
      string strDescription,
      int pintLocationId,
      int pintCompanyId,
      bool pboolLoadPager)
    {
      try
      {
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerBatch.CurrentPageNumber;
        int pintMaxRows = this.custPagerBatch.CurrentPageSize == 0 ? 10 : this.custPagerBatch.CurrentPageSize;
        int pintTotalRows;
        DataTable motiveMovementByPag = new MotiveMovementQueriesBL().GetMotiveMovementByPag(pintMotiveMovementId, pintWareHouseTypeUseId, strDescription, pintLocationId, pintCompanyId, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        int num = pintTotalRows;
        if (motiveMovementByPag.Rows.Count == 0)
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontro información con los criterios seleccionados."));
        this.wdgMotiveMovementList.DataSource = (object) motiveMovementByPag;
        this.wdgMotiveMovementList.DataBind();
        this.custPagerBatch.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
        this.custPagerBatch.TotalRecordCount = pintTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerBatch.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ClearControls()
    {
      try
      {
        this.cboWareHouseTypeUse.SelectedValue = "-1";
        this.cboFlow.SelectedValue = "-1";
        this.txtMotive.Text = string.Empty;
        this.cboMotiveSunat.SelectedValue = "-1";
        this.cboTypeMotive.SelectedValue = "-1";
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void EnabledControls(MaintenanceOperation penuCurrentOperation)
    {
      try
      {
        string str = this.H1.Value;
        switch (penuCurrentOperation)
        {
          case MaintenanceOperation.AddNew:
            this.cboWareHouseTypeUse.Enabled = true;
            this.RequiredFieldValidator1.Enabled = true;
            this.cboFlow.Enabled = true;
            this.RequiredFieldValidator2.Enabled = true;
            this.txtMotive.Enabled = true;
            this.cboMotiveSunat.Enabled = true;
            this.cboTypeMotive.Enabled = true;
            this.RequiredFieldValidator3.Enabled = true;
            this.wibSave.Text = "Grabar";
            this.cboWareHouseTypeUse.Focus();
            if (str == "0")
            {
              this.trManagementButtons.Visible = true;
              this.trwibFinalize.Visible = false;
              this.lblMessage1.Visible = false;
              string script1 = UtilDA.ActiveTabIndex("tabs", 1, "0");
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script1, true);
              string script2 = "TabIndex();";
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script2, true);
              break;
            }
            this.trManagementButtons.Visible = false;
            this.trwibFinalize.Visible = true;
            break;
          case MaintenanceOperation.Edit:
            this.cboWareHouseTypeUse.Enabled = true;
            this.RequiredFieldValidator1.Enabled = true;
            this.cboFlow.Enabled = true;
            this.RequiredFieldValidator2.Enabled = true;
            this.txtMotive.Enabled = true;
            this.cboMotiveSunat.Enabled = true;
            this.cboTypeMotive.Enabled = true;
            this.RequiredFieldValidator3.Enabled = true;
            this.wibSave.Text = "Grabar";
            this.cboWareHouseTypeUse.Focus();
            if (str == "0")
            {
              this.trManagementButtons.Visible = true;
              this.trwibFinalize.Visible = false;
              this.lblMessage1.Visible = false;
              string script3 = UtilDA.ActiveTabIndex("tabs", 1, "0");
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script3, true);
              string script4 = "TabIndex();";
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script4, true);
              break;
            }
            this.trManagementButtons.Visible = false;
            this.trwibFinalize.Visible = true;
            break;
          case MaintenanceOperation.Delete:
            this.cboWareHouseTypeUse.Enabled = false;
            this.RequiredFieldValidator1.Enabled = false;
            this.cboFlow.Enabled = false;
            this.RequiredFieldValidator2.Enabled = false;
            this.txtMotive.Enabled = false;
            this.cboMotiveSunat.Enabled = false;
            this.cboTypeMotive.Enabled = false;
            this.RequiredFieldValidator3.Enabled = false;
            this.wibSave.Text = "Eliminar";
            if (str == "0")
            {
              this.trManagementButtons.Visible = true;
              this.trwibFinalize.Visible = false;
              this.lblMessage1.Visible = false;
              string script5 = UtilDA.ActiveTabIndex("tabs", 1, "0");
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script5, true);
              string script6 = "TabIndex();";
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script6, true);
              break;
            }
            this.trManagementButtons.Visible = false;
            this.trwibFinalize.Visible = true;
            break;
          default:
            this.lblMessage.Visible = false;
            this.RequiredFieldValidator1.Enabled = false;
            this.RequiredFieldValidator2.Enabled = false;
            this.RequiredFieldValidator3.Enabled = false;
            this.wibSave.Text = "Grabar";
            string script7 = UtilDA.ActiveTabIndex("tabs", 0, "1");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script7, true);
            string script8 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script8, true);
            break;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void wdgMotiveMovementList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgMotiveMovementList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
  }
}
