// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Retail.RequirementData
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Retail
{
  public class RequirementData : Page
  {
    private MaintenanceOperation currentOperation = MaintenanceOperation.None;
    private DataTable dtRequirementData;
    private DataTable dtProofPaper;
    private DataTable dtBatch;
    private DataTable dtSunarp;
    private DataTable dtPayment;
    private DataTable dtReception;
    private DataTable dtDelivery;
    private DataTable dtDeliveryProgramation;
    private SystemUser objUserBE;
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected TextBox txtRequirementID;
    protected TextBox txtProfType;
    protected TextBox txtClientName;
    protected Label lblMessage;
    protected GridView wdgList;
    protected DataList dlRequirementData;
    protected HtmlTableCell td2;
    protected DataList dlSunarpData;
    protected HtmlTableCell td3;
    protected DataList dlPaymentData;
    protected Button wibCur;
    protected Button wibReturn;
    protected HtmlTableCell td4;
    protected DataList dlDispatchData;
    protected HtmlTableCell td5;
    protected DataList dlReceptionData;
    protected HtmlTableCell td6;
    protected DataList dlDeliveryData;
    protected HtmlTableCell td7;
    protected DataList dlProgramationDelivery;
    protected UpdatePanel UpdatePanel2;
    protected GridView wdgClaims;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.loadList();
      this.RefreshList();
      this.Initialize();
      this.ViewState["i_Requirement"] = (object) null;
    }

    public void loadList()
    {
      if (this.Request.QueryString["i_Requirement"] == null)
        return;
      this.Session["ListRequirements"] = (object) new RequirementQueriesBL().GetRequirementDatabyRetail(Convert.ToInt32(this.Request.QueryString["i_Requirement"].ToString((IFormatProvider) CultureInfo.CurrentCulture)));
      this.txtRequirementID.Text = (this.Session["ListRequirements"] as DataTable).Rows[0]["i_RequirementId"].ToString();
      this.txtProfType.Text = (this.Session["ListRequirements"] as DataTable).Rows[0]["v_Description"].ToString();
      this.txtClientName.Text = (this.Session["ListRequirements"] as DataTable).Rows[0]["v_CompleteName"].ToString();
    }

    protected void wdgList_PageIndexChanged(object sender, EventArgs e) => this.RefreshList();

    protected void wdgList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    public void RefreshList()
    {
      this.wdgList.DataSource = (object) (this.Session["ListRequirements"] as DataTable);
      this.wdgList.DataBind();
    }

    public void Initialize()
    {
    }

    protected void wdgList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      if (e.CommandName.Equals("getData", StringComparison.CurrentCulture))
      {
        this.lblMessage.Visible = false;
        this.currentOperation = MaintenanceOperation.AddNew;
        this.ViewState.Add("currentOperation", (object) this.currentOperation);
        this.EnableControls();
        int int32 = Convert.ToInt32(e.CommandArgument);
        this.ViewState["i_RequirementPlateId"] = (object) Convert.ToInt32(this.wdgList.Rows[int32].Cells[2].Text);
        this.ViewState["i_Requirement"] = (object) Convert.ToInt32(this.wdgList.DataKeys[int32]["i_RequirementId"].ToString());
        string script = UtilDA.ActiveTabIndex("SubTabs", 1, "0,2");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
      if (!e.CommandName.Equals("getEdit", StringComparison.CurrentCulture))
        return;
      this.lblMessage.Visible = false;
      int int32_1 = Convert.ToInt32(e.CommandArgument);
      GridViewRow row = this.wdgList.Rows[int32_1];
      if (Convert.ToInt32(this.wdgList.DataKeys[int32_1]["i_Status"].ToString()) != 0 && Convert.ToInt32(this.wdgList.DataKeys[int32_1]["i_Status"].ToString()) != 1)
      {
        this.lblMessage.Visible = true;
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>La solicitud ya no puede ser editada");
        string script = UtilDA.ActiveTabIndex("SubTabs", 0, "1,2");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
      else
      {
        this.currentOperation = MaintenanceOperation.Edit;
        this.ViewState.Add("currentOperation", (object) this.currentOperation);
        this.EnableControls();
        Convert.ToInt32(row.Cells[2].Text);
        Convert.ToInt32(this.wdgList.DataKeys[int32_1]["i_RequirementId"].ToString());
        string script = UtilDA.ActiveTabIndex("SubTabs", 2, "0,1");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
    }

    private void EnableControls()
    {
      switch (this.currentOperation)
      {
        case MaintenanceOperation.AddNew:
          string script1 = UtilDA.ActiveTabIndex("SubTabs", 1, "0,2");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script1, true);
          break;
        case MaintenanceOperation.Edit:
          string script2 = UtilDA.ActiveTabIndex("SubTabs", 2, "0,1");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script2, true);
          break;
      }
    }
  }
}
