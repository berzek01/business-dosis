// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.ClaimInbox
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Claim.BL;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.Product.BL;
using SIIV.SystemParameter.BL;
using SIIV.WebApp.Claims.CustomControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims
{
  public class ClaimInbox : Page
  {
    private RequestData ucRequestData1;
    private RequestData ucRequestData2;
    private DataNoAgree ucDataNoAgree1;
    private BatchNoAgree ucBatchNoAgree1;
    private ProductNoAgree ucProductNoAgree1;
    private ReimbursementPayment ucReimbursementPayment1;
    private CallCenter ucCallCenter1;
    private RequesterData ucRequesterDataTmp;
    private LogicInconsistency ucLogicInconsistency;
    private UseChange ucUseChange1;
    private ChangeDeliveryPlate ucChangeDeliveryPlate1;
    private RemoveMotivation ucRemoveMotivation;
    private EnablePaymentCode ucEnablePaymentCode;
    private RemoveRequirement ucRemoveRequirement;
    private RegulariceMovementForDeliver ucRegulariceMovementForDeliver;
    private RequirementData RequirementData1;
    protected UpdatePanel UpdatePanel1;
    protected DropDownList wddClaimTypeSearch;
    protected DropDownList wddClaimStatusSearch;
    protected Label Label1;
    protected Fecha wdpStartDate;
    protected Label Label2;
    protected Fecha wdpEndDate;
    protected TextBox txtClaimCode;
    protected TextBox txtSearchString;
    protected Button wibSearch;
    protected CheckBox chkQuitFilter;
    protected Button wibAdvancedSearch;
    protected GridView wdgList;
    protected Pager custPagerClaimList;
    protected Label lblMessageClaimInbox;
    protected Button wibManage;
    protected Button wibExport;
    protected UpdatePanel UpdatePanel2;
    protected Label Label3;
    protected DropDownList wddClaimType;
    protected Panel pControls;
    protected HtmlGenericControl spanRequestData1;
    protected HtmlGenericControl spanRequestData2;
    protected HtmlGenericControl spanLogicInconsistency;
    protected HtmlGenericControl spanDataNoAgree1;
    protected HtmlGenericControl spanBatchNoAgree1;
    protected HtmlGenericControl spanProductNoAgree1;
    protected HtmlGenericControl spanReimbursementPayment1;
    protected HtmlGenericControl spanCallCenter1;
    protected HtmlGenericControl spanUseChange1;
    protected HtmlGenericControl spanChangeDeliveryPlate1;
    protected HtmlGenericControl spanRemoveMotivation;
    protected HtmlGenericControl spanEnablePaymentCode;
    protected HtmlGenericControl spanRemoveRequirement;
    protected HtmlGenericControl spanRegulariceMovementForDeliver;
    protected HtmlGenericControl spanRequirementData1;
    protected Panel Panel2;
    protected HtmlGenericControl spanRequesterDataTmp;
    protected ClaimData ClaimData1;
    protected Label Label4;
    protected TextBox txtComents;
    protected Label lblMsgError;
    protected Button wibSave;
    protected Button wibCancel;
    protected UpdatePanel UpdatePanel3;
    protected Label Label5;
    protected TextBox txtClaimNumber;
    protected Label Label6;
    protected DropDownList wddClaimTypeManagement;
    protected Label Label7;
    protected DropDownList wddStatusClaimManagament;
    protected GridView wdgTracing;
    protected Button wibApprove;
    protected Button wibEmail;
    protected Button wibAssigned;
    protected Button wibReject;
    protected Button wibClose;
    protected Button wibCancelManagement;
    protected Button btnManagementTemp;
    protected Button btnAdvancedSearchTemp;
    protected Label lblMessageClaimManagement;

    protected void Page_PreInit(object sender, EventArgs e)
    {
      this.ucRequestData1 = (RequestData) this.LoadControl("~/Claims/CustomControls/RequestData.ascx");
      this.ucRequestData2 = (RequestData) this.LoadControl("~/Claims/CustomControls/RequestData.ascx");
      this.ucDataNoAgree1 = (DataNoAgree) this.LoadControl("~/Claims/CustomControls/DataNoAgree.ascx");
      this.ucBatchNoAgree1 = (BatchNoAgree) this.LoadControl("~/Claims/CustomControls/BatchNoAgree.ascx");
      this.ucProductNoAgree1 = (ProductNoAgree) this.LoadControl("~/Claims/CustomControls/ProductNoAgree.ascx");
      this.ucRequesterDataTmp = (RequesterData) this.LoadControl("~/Claims/CustomControls/RequesterData.ascx");
      this.ucLogicInconsistency = (LogicInconsistency) this.LoadControl("~/Claims/CustomControls/LogicInconsistency.ascx");
      this.ucUseChange1 = (UseChange) this.LoadControl("~/Claims/CustomControls/UseChange.ascx");
      this.ucChangeDeliveryPlate1 = (ChangeDeliveryPlate) this.LoadControl("~/Claims/CustomControls/ChangeDeliveryPlate.ascx");
      this.ucRemoveMotivation = (RemoveMotivation) this.LoadControl("~/Claims/CustomControls/RemoveMotivation.ascx");
      this.ucEnablePaymentCode = (EnablePaymentCode) this.LoadControl("~/Claims/CustomControls/EnablePaymentCode.ascx");
      this.ucRemoveRequirement = (RemoveRequirement) this.LoadControl("~/Claims/CustomControls/RemoveRequirement.ascx");
      this.ucRegulariceMovementForDeliver = (RegulariceMovementForDeliver) this.LoadControl("~/Claims/CustomControls/RegulariceMovementForDeliver.ascx");
      this.ucReimbursementPayment1 = (ReimbursementPayment) this.LoadControl("~/Claims/CustomControls/ReimbursementPayment.ascx");
      this.ucReimbursementPayment1.OnDatosOK += new ReimbursementPayment.DatosOK(this.ucReimbursementPayment1_OnDatosOK);
      this.ucReimbursementPayment1.OnDatosWrong += new ReimbursementPayment.DatosWrong(this.ucReimbursementPayment1_OnDatosWrong);
      this.ucCallCenter1 = (CallCenter) this.LoadControl("~/Claims/CustomControls/CallCenter.ascx");
      this.ucCallCenter1.OnDatosOK += new CallCenter.DatosOK(this.ucCallCenter1_OnDatosOK);
      this.ucCallCenter1.OnDatosWrong += new CallCenter.DatosWrong(this.ucCallCenter1_OnDatosWrong);
      this.ucUseChange1.OnDatosOK += new UseChange.DatosOK(this.ucUseChange1_OnDatosOK);
      this.ucUseChange1.OnDatosWrong += new UseChange.DatosWrong(this.ucUseChange1_OnDatosWrong);
      this.RequirementData1 = (RequirementData) this.LoadControl("~/Claims/CustomControls/RequirementData.ascx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.spanRequestData1.Controls.Add((Control) this.ucRequestData1);
      this.spanRequestData2.Controls.Add((Control) this.ucRequestData2);
      this.spanLogicInconsistency.Controls.Add((Control) this.ucLogicInconsistency);
      this.spanDataNoAgree1.Controls.Add((Control) this.ucDataNoAgree1);
      this.spanBatchNoAgree1.Controls.Add((Control) this.ucBatchNoAgree1);
      this.spanProductNoAgree1.Controls.Add((Control) this.ucProductNoAgree1);
      this.spanReimbursementPayment1.Controls.Add((Control) this.ucReimbursementPayment1);
      this.spanCallCenter1.Controls.Add((Control) this.ucCallCenter1);
      this.spanRequesterDataTmp.Controls.Add((Control) this.ucRequesterDataTmp);
      this.spanUseChange1.Controls.Add((Control) this.ucUseChange1);
      this.spanChangeDeliveryPlate1.Controls.Add((Control) this.ucChangeDeliveryPlate1);
      this.spanRemoveMotivation.Controls.Add((Control) this.ucRemoveMotivation);
      this.spanEnablePaymentCode.Controls.Add((Control) this.ucEnablePaymentCode);
      this.spanRemoveRequirement.Controls.Add((Control) this.ucRemoveRequirement);
      this.spanRegulariceMovementForDeliver.Controls.Add((Control) this.ucRegulariceMovementForDeliver);
      this.spanRequirementData1.Controls.Add((Control) this.RequirementData1);
      if (this.Page.IsPostBack)
        return;
      this.LoadParameters();
      this.ucRequesterDataTmp.EnabledControls(false);
      this.ClaimData1.EnableControls(false);
      this.spanRequestData1.Style.Add("display", "none");
      this.spanRequestData2.Style.Add("display", "none");
      this.spanLogicInconsistency.Style.Add("display", "none");
      this.spanDataNoAgree1.Style.Add("display", "none");
      this.spanBatchNoAgree1.Style.Add("display", "none");
      this.spanProductNoAgree1.Style.Add("display", "none");
      this.spanReimbursementPayment1.Style.Add("display", "none");
      this.spanCallCenter1.Style.Add("display", "none");
      this.spanRequesterDataTmp.Style.Add("display", "none");
      this.spanUseChange1.Style.Add("display", "none");
      this.spanChangeDeliveryPlate1.Style.Add("display", "none");
      this.spanRemoveMotivation.Style.Add("display", "none");
      this.spanEnablePaymentCode.Style.Add("display", "none");
      this.spanRemoveRequirement.Style.Add("display", "none");
      this.spanRegulariceMovementForDeliver.Style.Add("display", "none");
      this.spanRequirementData1.Style.Add("display", "none");
      this.LoadList(true);
    }

    protected void wddClaimTypeSearch_SelectionChanged(object sender, EventArgs e)
    {
      this.wibExport.Enabled = false;
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.LoadList(true);

    protected void chkQuitFilter_CheckedChanged(object sender, EventArgs e)
    {
      this.wibAdvancedSearch.Enabled = this.chkQuitFilter.Checked;
      if (this.chkQuitFilter.Checked)
        return;
      this.Session["v_advancedSearch"] = (object) null;
    }

    protected void wibAdvancedSearch_Click(object sender, EventArgs e)
    {
      this.CreatePopUpServer("Búsqueda Avanzada", "AdvancedSearch.aspx", "620px", "400px");
    }

    protected void wdgList_InitializeRow(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowIndex < 0)
        return;
      ImageButton control = e.Row.Cells[2].FindControl("ibtStatus") as ImageButton;
      DataRow row = ((DataRowView) e.Row.DataItem).Row;
      if (Convert.ToInt32(row["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == 1)
        control.ImageUrl = "~/Images/Order.png";
      else if (Convert.ToInt32(row["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == 2)
        control.ImageUrl = "~/Images/Design/Buttons/Actions/accept.gif";
      else if (Convert.ToInt32(row["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == 3)
        control.ImageUrl = "~/Images/Design/Delete.gif";
      else if (Convert.ToInt32(row["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == 5)
      {
        control.ImageUrl = "~/Images/cerrado.png";
      }
      else
      {
        if (Convert.ToInt32(row["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) != 6)
          return;
        control.ImageUrl = "~/Images/Design/Buttons/Actions/expirado.png";
      }
    }

    protected void wdgList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      int int32_1 = Convert.ToInt32(e.CommandArgument);
      GridViewRow row = this.wdgList.Rows[int32_1];
      if (e.CommandName == "Read")
      {
        RequirementClaim currentRow = this.GetCurrentRow(enmTypeLoadData.SelectedRowsGrid, int32_1);
        this.SetearControls(currentRow);
        this.ClaimData1.EnableControlsEdit(true);
        string script = UtilDA.ActiveTabIndex("tabs", 1, "0,2");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        this.wibSave.Enabled = currentRow.i_Status.GetValueOrDefault() != 6 && currentRow.i_Status.GetValueOrDefault() != 5 && currentRow.i_Status.GetValueOrDefault() != 3;
        if (currentRow.i_ClaimTypeId.GetValueOrDefault() == 15)
          this.wibSave.Enabled = false;
      }
      if (!(e.CommandName == "Manage"))
        return;
      int int32_2 = Convert.ToInt32(this.wdgList.DataKeys[int32_1]["i_RequirementClaimId"]);
      short int16 = Convert.ToInt16(this.wdgList.DataKeys[int32_1]["i_ClaimTypeId"]);
      int int32_3 = Convert.ToInt32(this.wdgList.DataKeys[int32_1]["i_Status"]);
      string text = row.Cells[4].Text;
      this.GetTracing(int32_2, (int) int16, text);
      string script1 = UtilDA.ActiveTabIndex("tabs", 2, "0,1");
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script1, true);
      this.lblMsgError.Visible = false;
      this.wddStatusClaimManagament.SelectedValue = int32_3.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      this.UpdatePanel3.Update();
    }

    protected void custPagerClaimList_OnPageChanged(object sender, CustomPageChangeArgs e)
    {
      this.LoadList(false);
    }

    private RequirementClaim GetCurrentRow(enmTypeLoadData penuTypeLoadData, int index)
    {
      RequirementClaim currentRow = new RequirementClaim();
      if (this.wdgList.Rows.Count > 0)
      {
        GridViewRow row1 = this.wdgList.Rows[index];
        DataRow row2 = new RequirementClaimQueriesBL().Read(Convert.ToInt32(this.wdgList.DataKeys[index]["i_RequirementClaimId"].ToString())).Rows[0];
        currentRow.i_RequirementClaimId = Convert.ToInt32(row2["i_RequirementClaimId"], (IFormatProvider) CultureInfo.CurrentCulture);
        currentRow.i_ClaimTypeId = new int?(Convert.ToInt32(row2["i_ClaimTypeId"], (IFormatProvider) CultureInfo.CurrentCulture));
        currentRow.v_ClaimDate = row2["v_ClaimDate"].ToString();
        currentRow.i_Status = Convert.IsDBNull(row2["i_Status"]) ? new int?() : new int?(Convert.ToInt32(row2["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture));
        currentRow.v_ListRequirementId = row2["v_ListRequirementId"].ToString();
        currentRow.v_Requester = row2["v_Requester"].ToString();
        currentRow.v_RequestValues = row2["v_RequestValues"].ToString();
        currentRow.v_InputValues = row2["v_InputValues"].ToString();
        currentRow.v_ReadValues = row2["v_ReadValues"].ToString();
        currentRow.v_Comments = row2["v_Comments"].ToString();
        currentRow.i_ClaimMotiveId = Convert.IsDBNull(row2["i_ClaimMotiveId"]) ? new int?() : new int?(Convert.ToInt32(row2["i_ClaimMotiveId"], (IFormatProvider) CultureInfo.CurrentCulture));
      }
      return currentRow;
    }

    protected void btnManage_Click(object sender, EventArgs e)
    {
    }

    protected void wibExport_Click(object sender, EventArgs e)
    {
      this.lblMessageClaimInbox.Visible = false;
      if (this.wddClaimTypeSearch.SelectedValue == "" || this.wddClaimTypeSearch.SelectedValue == "-1")
        Message.SetMessage(this.lblMessageClaimInbox, enmMessageType.Warning, "Debe seleccionar un tipo de reclamo");
      else
        this.ExcelExport();
    }

    protected void wddClaimType_SelectionChanged(object sender, EventArgs e) => this.LoadPanel();

    protected void btnSave_Click(object sender, EventArgs e)
    {
      if (!this.ValidateParameters())
        return;
      using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
      {
        Timeout = new TimeSpan(1, 1, 1)
      }))
      {
        try
        {
          if (new RequirementClaimManagementBL().UpdateClaim(this.GetObject()) > 0)
          {
            Message.SetMessage(this.lblMsgError, enmMessageType.Success, "El Reclamo se modificó satisfactoriamente");
            this.ClearControls();
          }
          else
            Message.SetMessage(this.lblMsgError, enmMessageType.Warning, "Se encontró un problema en la modificación del reclamo");
          transactionScope.Complete();
        }
        catch (Exception ex)
        {
          Message.SetMessage(this.lblMsgError, enmMessageType.Error, ex.Message);
        }
      }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      string script = UtilDA.ActiveTabIndex("tabs", 0, "1,2");
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      this.lblMsgError.Visible = false;
      this.UpdatePanel1.Update();
    }

    protected void wibApprove_Click(object sender, EventArgs e)
    {
      if (this.Session["i_RequirementClaimId"] != null)
      {
        if (!this.ValidateStatus(Convert.ToInt32(this.Session["i_RequirementClaimId"], (IFormatProvider) CultureInfo.CurrentCulture), 2))
          return;
        string empty = string.Empty;
        if (Convert.ToInt32(this.wddClaimTypeManagement.SelectedValue) == 13)
          this.Session["pObjRequirementClaim"] = (object) new RequirementClaimQueriesBL().Read(Convert.ToInt32(this.Session["i_RequirementClaimId"], (IFormatProvider) CultureInfo.CurrentCulture)).Rows[0];
        else
          this.Session["pObjRequirementClaim"] = (object) null;
        this.CreatePopUpServer("Aprobación Reclamo", "ClaimApprove.aspx?flag=1&rcid=" + this.Session["i_RequirementClaimId"].ToString() + "&ctm=" + this.wddClaimTypeManagement.SelectedValue, "755px", "290px");
      }
      else
      {
        this.Session.Remove("i_RequirementClaimId");
        Message.SetMessage(this.lblMsgError, enmMessageType.Error, Constants.REQUIREMENT_ERROR_GENERICO + "Regrese a la búsqueda principal e intente nuevamente");
      }
    }

    protected void wibEmail_Click(object sender, EventArgs e)
    {
      if (this.Session["i_RequirementClaimId"] != null)
      {
        this.lblMessageClaimManagement.Visible = false;
        string empty = string.Empty;
        this.CreatePopUpServer("Envío Email", "ClaimEmailSend.aspx?rcid=" + this.Session["i_RequirementClaimId"].ToString() + "&st=" + this.wddStatusClaimManagament.SelectedValue, "755px", "670px");
      }
      else
      {
        this.Session.Remove("i_RequirementClaimId");
        Message.SetMessage(this.lblMsgError, enmMessageType.Error, Constants.REQUIREMENT_ERROR_GENERICO + "Regrese a la búsqueda principal e intente nuevamente");
      }
    }

    protected void wibAssigned_Click(object sender, EventArgs e)
    {
      if (this.Session["i_RequirementClaimId"] != null)
      {
        if (!this.ValidateStatus(Convert.ToInt32(this.Session["i_RequirementClaimId"], (IFormatProvider) CultureInfo.CurrentCulture), -1))
          return;
        string empty = string.Empty;
        this.CreatePopUpServer("Asignación Responsable Costo Reclamo", "ClaimAssignedResponsibleCost.aspx?rcid=" + this.Session["i_RequirementClaimId"].ToString() + "&st=" + this.wddStatusClaimManagament.SelectedValue, "755px", "320px");
      }
      else
      {
        this.Session.Remove("i_RequirementClaimId");
        Message.SetMessage(this.lblMsgError, enmMessageType.Error, Constants.REQUIREMENT_ERROR_GENERICO + "Regrese a la búsqueda principal e intente nuevamente");
      }
    }

    protected void wibReject_Click(object sender, EventArgs e)
    {
      if (this.Session["i_RequirementClaimId"] != null)
      {
        if (!this.ValidateStatus(Convert.ToInt32(this.Session["i_RequirementClaimId"], (IFormatProvider) CultureInfo.CurrentCulture), 3))
          return;
        string empty = string.Empty;
        this.Session["pObjRequirementClaim"] = (object) null;
        this.CreatePopUpServer("Rechazar Reclamo", "ClaimApprove.aspx?flag=3&rcid=" + this.Session["i_RequirementClaimId"].ToString() + "&ctm=" + this.wddClaimTypeManagement.SelectedValue, "755px", "260px");
      }
      else
      {
        this.Session.Remove("i_RequirementClaimId");
        Message.SetMessage(this.lblMsgError, enmMessageType.Error, Constants.REQUIREMENT_ERROR_GENERICO + "Regrese a la búsqueda principal e intente nuevamente");
      }
    }

    protected void wibClose_Click(object sender, EventArgs e)
    {
      if (this.Session["i_RequirementClaimId"] != null)
      {
        if (!this.ValidateStatus(Convert.ToInt32(this.Session["i_RequirementClaimId"], (IFormatProvider) CultureInfo.CurrentCulture), 5))
          return;
        string empty = string.Empty;
        this.Session["pObjRequirementClaim"] = (object) new RequirementClaimQueriesBL().Read(Convert.ToInt32(this.Session["i_RequirementClaimId"], (IFormatProvider) CultureInfo.CurrentCulture)).Rows[0];
        this.CreatePopUpServer("Cerrar Reclamo", "ClaimApprove.aspx?flag=2&rcid=" + this.Session["i_RequirementClaimId"].ToString() + "&ctm=" + this.wddClaimTypeManagement.SelectedValue, "755px", "280px");
      }
      else
      {
        this.Session.Remove("i_RequirementClaimId");
        Message.SetMessage(this.lblMsgError, enmMessageType.Error, Constants.REQUIREMENT_ERROR_GENERICO + "Regrese a la búsqueda principal e intente nuevamente");
      }
    }

    protected void btnCancelManagement_Click(object sender, EventArgs e)
    {
      string script = UtilDA.ActiveTabIndex("tabs", 0, "1,2");
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      this.lblMessageClaimManagement.Visible = false;
      this.wibSearch_Click((object) null, (EventArgs) null);
      this.UpdatePanel1.Update();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
      this.Session["RequirementClaim"] = (object) null;
      this.Response.Redirect("ClaimRegister.aspx");
    }

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void btnManagementTemp_Click(object sender, EventArgs e)
    {
      this.GetTracing(Convert.ToInt32(this.Session["i_RequirementClaimId"], (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.wddClaimTypeManagement.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), this.txtClaimNumber.Text);
      try
      {
        DataTable dataTable = new RequirementClaimQueriesBL().Read(Convert.ToInt32(this.Session["i_RequirementClaimId"], (IFormatProvider) CultureInfo.CurrentCulture));
        if (dataTable == null || dataTable.Rows.Count <= 0)
          return;
        this.wddStatusClaimManagament.SelectedValue = dataTable.Rows[0]["i_status"].ToString();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageClaimManagement, enmMessageType.Warning, ex.Message);
      }
    }

    protected void btnAdvancedSearchTemp_Click(object sender, EventArgs e)
    {
      if (this.Session["v_advancedSearch"] == null)
        return;
      this.chkQuitFilter.Visible = this.Session["v_advancedSearch"].ToString().Length > 0;
    }

    private void LoadParameters()
    {
      DataTable dataTable1 = new DataTable();
      SystemParameterQueriesBL parameterQueriesBl = new SystemParameterQueriesBL();
      ArrayList arrFilter = new ArrayList()
      {
        (object) (SystemParameterGroups.ClaimTypes.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ", " + SystemParameterGroups.ClaimStatus.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ", " + SystemParameterGroups.ClaimActions.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
        (object) "",
        (object) "1",
        (object) "1"
      };
      DataTable dataTable2;
      try
      {
        dataTable2 = parameterQueriesBl.GetbyFilter(arrFilter);
      }
      catch (Exception ex)
      {
        throw;
      }
      this.wddClaimTypeSearch.Items.Clear();
      this.wddClaimTypeManagement.Items.Clear();
      this.wddClaimStatusSearch.Items.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
      {
        if (Convert.ToInt32(row["i_GroupId"], (IFormatProvider) CultureInfo.CurrentCulture) == SystemParameterGroups.ClaimTypes)
        {
          this.wddClaimTypeSearch.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          this.wddClaimType.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          this.wddClaimTypeManagement.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        }
        if (Convert.ToInt32(row["i_GroupId"], (IFormatProvider) CultureInfo.CurrentCulture) == SystemParameterGroups.ClaimStatus)
        {
          this.wddClaimStatusSearch.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          this.wddStatusClaimManagament.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        }
      }
      this.wddClaimType.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Constants.OPCIONLISTA_Todos, Constants.OPCIONLISTA_ValorSinSeleccion));
      this.wddClaimTypeSearch.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Constants.OPCIONLISTA_Todos, Constants.OPCIONLISTA_ValorSinSeleccion));
      this.wddClaimTypeManagement.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Constants.OPCIONLISTA_Seleccione, Constants.OPCIONLISTA_ValorSinSeleccion));
      this.wddClaimStatusSearch.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Constants.OPCIONLISTA_Todos, Constants.OPCIONLISTA_ValorSinSeleccion));
      this.wdpStartDate.Value = DateTime.Now;
      this.wdpEndDate.Value = DateTime.Now;
    }

    private void LoadList(bool pboolLoadPager)
    {
      this.lblMessageClaimInbox.Visible = false;
      RequirementClaimQueriesBL requirementClaimQueriesBl = new RequirementClaimQueriesBL();
      short int16_1 = this.wddClaimTypeSearch.SelectedValue == "" ? (short) 0 : Convert.ToInt16(this.wddClaimTypeSearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      short int16_2 = this.wddClaimStatusSearch.SelectedValue == "" ? (short) 0 : Convert.ToInt16(this.wddClaimStatusSearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      string text1 = this.txtClaimCode.Text;
      string text2 = this.txtSearchString.Text;
      string pstrPlateNumber = this.Session["v_advancedSearch"] != null ? this.Session["v_advancedSearch"].ToString() : "";
      DateTime dateTime1 = Convert.ToDateTime((object) this.wdpStartDate.Value, (IFormatProvider) CultureInfo.CurrentCulture);
      DateTime dateTime2 = Convert.ToDateTime((object) this.wdpEndDate.Value, (IFormatProvider) CultureInfo.CurrentCulture);
      int pintstartRowIndex = pboolLoadPager ? 1 : this.custPagerClaimList.CurrentPageNumber;
      int pintmaxRows = this.custPagerClaimList.CurrentPageSize == 0 ? 10 : this.custPagerClaimList.CurrentPageSize;
      try
      {
        int pinttotalRows;
        DataTable all = requirementClaimQueriesBl.GetAll((int) int16_1, (int) int16_2, text1, text2, pstrPlateNumber, dateTime1, dateTime2, pintstartRowIndex, pintmaxRows, out pinttotalRows);
        int num = pinttotalRows;
        if (int16_1 == (short) 7 || int16_1 == (short) -1)
        {
          all.Columns.Add("v_Motive", typeof (string));
          this.wdgList.Columns[10].Visible = true;
          foreach (DataRow row in (InternalDataCollectionBase) all.Rows)
          {
            if (row["v_Comments"].ToString().Contains("|"))
            {
              string[] strArray = row["v_Comments"].ToString().Split('|');
              row["v_Motive"] = (object) strArray[0].ToString();
              row["v_Comments"] = (object) strArray[1].ToString();
            }
          }
        }
        else
          this.wdgList.Columns[10].Visible = false;
        this.wdgList.AutoGenerateColumns = false;
        this.wdgList.DataSource = (object) all;
        this.wdgList.DataBind();
        this.custPagerClaimList.TotalPages = num % pintmaxRows == 0 ? num / pintmaxRows : num / pintmaxRows + 1;
        this.custPagerClaimList.TotalRecordCount = pinttotalRows;
        if (pboolLoadPager)
          this.custPagerClaimList.LoadPager();
        this.wibExport.Enabled = all != null && all.Rows.Count != 0;
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageClaimInbox, enmMessageType.Error, Constants.REQUIREMENT_ERROR_GENERICO + ex.Message);
        this.HidePopup();
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SetearControls(RequirementClaim objParam)
    {
      this.wddClaimType.SelectedValue = objParam.i_ClaimTypeId.ToString();
      this.wddClaimType.Enabled = false;
      this.Session["i_RequirementClaimId"] = (object) objParam.i_RequirementClaimId;
      int num1;
      if (!objParam.i_AssignedUserId.HasValue)
      {
        int? iAssignedUserId = objParam.i_AssignedUserId;
        int num2 = 0;
        num1 = !(iAssignedUserId.GetValueOrDefault() == num2 & iAssignedUserId.HasValue) ? 1 : 0;
      }
      else
        num1 = 1;
      if (num1 != 0)
        this.Session["pobjSystemUser"] = (object) new SystemUser()
        {
          i_SystemUserId = Convert.ToInt32((object) objParam.i_AssignedUserId, (IFormatProvider) CultureInfo.CurrentCulture)
        };
      this.pControls.Visible = true;
      this.Panel2.Visible = false;
      this.spanRequestData1.Style.Add("display", "none");
      this.spanRequestData2.Style.Add("display", "none");
      this.spanLogicInconsistency.Style.Add("display", "none");
      this.spanDataNoAgree1.Style.Add("display", "none");
      this.spanBatchNoAgree1.Style.Add("display", "none");
      this.spanProductNoAgree1.Style.Add("display", "none");
      this.spanReimbursementPayment1.Style.Add("display", "none");
      this.spanCallCenter1.Style.Add("display", "none");
      this.spanRequesterDataTmp.Style.Add("display", "none");
      this.spanUseChange1.Style.Add("display", "none");
      this.spanChangeDeliveryPlate1.Style.Add("display", "none");
      this.spanRemoveMotivation.Style.Add("display", "none");
      this.spanEnablePaymentCode.Style.Add("display", "none");
      this.spanRemoveRequirement.Style.Add("display", "none");
      this.spanRegulariceMovementForDeliver.Style.Add("display", "none");
      this.spanRequirementData1.Style.Add("display", "none");
      switch (Convert.ToInt32(this.wddClaimType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
      {
        case 1:
          this.spanLogicInconsistency.Style.Add("display", "");
          this.spanRequesterDataTmp.Style.Add("display", "");
          if (objParam.v_RequestValues != "")
            this.ucLogicInconsistency.SetTexts(objParam.v_RequestValues);
          objParam.v_Requester = "[Sistemas]|[Sistemas]";
          this.ucRequesterDataTmp.SetTexts(objParam.v_Requester);
          this.Panel2.Visible = true;
          break;
        case 2:
          if (objParam.v_RequestValues != "")
            this.ucRequestData1.SetTexts(objParam.v_RequestValues);
          this.spanRequestData1.Style.Add("display", "");
          this.spanRequesterDataTmp.Style.Add("display", "");
          this.ucRequestData1.EnableControls(false);
          this.ucRequesterDataTmp.EnabledControls(true);
          this.ucRequesterDataTmp.SetTexts(objParam.v_Requester);
          this.Panel2.Visible = true;
          break;
        case 3:
          if (objParam.v_RequestValues != "")
            this.ucRequestData2.SetTexts(objParam.v_RequestValues);
          if (objParam.v_ReadValues != "")
            this.ucDataNoAgree1.SetOldTexts(objParam.v_ReadValues);
          if (objParam.v_InputValues != "")
            this.ucDataNoAgree1.SetNewTexts(objParam.v_InputValues, objParam.v_ReadValues);
          this.spanRequestData2.Style.Add("display", "");
          this.spanDataNoAgree1.Style.Add("display", "");
          this.spanRequesterDataTmp.Style.Add("display", "");
          this.ucRequestData2.EnableControls(false);
          this.ucRequesterDataTmp.EnabledControls(true);
          this.ucRequesterDataTmp.SetTexts(objParam.v_Requester);
          this.Panel2.Visible = true;
          break;
        case 4:
          if (objParam.v_RequestValues != "")
            this.ucBatchNoAgree1.SetTexts(objParam.v_RequestValues);
          this.ucBatchNoAgree1.SetMotive(Convert.ToInt32((object) objParam.i_ClaimMotiveId, (IFormatProvider) CultureInfo.CurrentCulture));
          this.spanBatchNoAgree1.Style.Add("display", "");
          this.ucBatchNoAgree1.EnabledControls(false);
          break;
        case 5:
          if (objParam.v_RequestValues != "")
            this.ucProductNoAgree1.SetTexts(objParam.v_RequestValues);
          this.spanProductNoAgree1.Style.Add("display", "");
          this.spanRequesterDataTmp.Style.Add("display", "");
          this.ucProductNoAgree1.EnabledControls(false);
          this.ucRequesterDataTmp.EnabledControls(true);
          this.ucRequesterDataTmp.SetTexts(objParam.v_Requester);
          this.Panel2.Visible = true;
          break;
        case 6:
          if (objParam.v_RequestValues != "")
          {
            this.ucReimbursementPayment1.SetTexts(objParam.v_RequestValues);
            this.ucReimbursementPayment1.SetTextsRequester(objParam.v_Requester);
          }
          this.ucReimbursementPayment1.SetMotive(new int?(Convert.ToInt32((object) objParam.i_ClaimMotiveId, (IFormatProvider) CultureInfo.CurrentCulture)));
          this.spanReimbursementPayment1.Style.Add("display", "");
          this.ucReimbursementPayment1.EnabledControls(false);
          this.ucReimbursementPayment1.EnabledControlsTitular(false);
          break;
        case 7:
          if (objParam.v_RequestValues != "")
            this.ucCallCenter1.SetTexts(objParam.v_RequestValues);
          this.spanCallCenter1.Style.Add("display", "");
          this.spanRequesterDataTmp.Style.Add("display", "");
          this.ucCallCenter1.EnabledControls(false);
          this.ucRequesterDataTmp.EnabledControls(true);
          this.ucRequesterDataTmp.SetTexts(objParam.v_Requester);
          this.Panel2.Visible = true;
          break;
        case 9:
          if (objParam.v_RequestValues != "")
            this.ucUseChange1.SetTexts(objParam.v_RequestValues);
          this.spanUseChange1.Style.Add("display", "");
          this.spanRequesterDataTmp.Style.Add("display", "");
          this.ucUseChange1.EnabledControls(false);
          this.ucRequesterDataTmp.EnabledControls(true);
          this.ucRequesterDataTmp.SetTexts(objParam.v_Requester);
          this.Panel2.Visible = true;
          break;
        case 10:
          if (objParam.v_RequestValues != "")
            this.ucChangeDeliveryPlate1.SetTexts(objParam.v_RequestValues);
          this.spanChangeDeliveryPlate1.Style.Add("display", "");
          this.ucChangeDeliveryPlate1.EnabledControls(false);
          this.Panel2.Visible = true;
          break;
        case 11:
          if (objParam.v_RequestValues != "")
            this.ucRemoveMotivation.SetTexts(objParam.v_RequestValues);
          this.spanRemoveMotivation.Style.Add("display", "");
          this.ucRemoveMotivation.EnabledControls(false);
          this.Panel2.Visible = true;
          break;
        case 12:
          if (objParam.v_RequestValues != "")
            this.ucEnablePaymentCode.SetTexts(objParam.v_RequestValues);
          this.spanEnablePaymentCode.Style.Add("display", "");
          this.ucEnablePaymentCode.EnabledControls(false);
          this.Panel2.Visible = true;
          break;
        case 13:
          if (objParam.v_RequestValues != "")
            this.ucRemoveRequirement.SetTexts(objParam.v_RequestValues);
          this.spanRemoveRequirement.Style.Add("display", "");
          this.ucRemoveRequirement.EnabledControls(false);
          this.Panel2.Visible = true;
          break;
        case 14:
          if (objParam.v_RequestValues != "")
            this.ucRegulariceMovementForDeliver.SetTexts(objParam.v_RequestValues);
          this.spanRegulariceMovementForDeliver.Style.Add("display", "");
          this.ucRegulariceMovementForDeliver.EnabledControls(false);
          this.Panel2.Visible = true;
          break;
        case 15:
          this.spanRequirementData1.Style.Add("display", "");
          if (objParam.v_RequestValues != "")
            this.RequirementData1.SetTexts(objParam.v_RequestValues);
          this.RequirementData1.EnableControlsView(false);
          this.txtComents.Enabled = false;
          this.wibSave.Enabled = false;
          this.Panel2.Visible = true;
          this.RequirementData1.SetTextsUpdateApplicantData(objParam.v_Requester);
          this.wibSave.Enabled = false;
          break;
      }
      this.ClaimData1.SetearData(objParam);
      this.txtComents.Text = objParam.v_Comments;
    }

    private void ClearControls()
    {
      switch (Convert.ToInt32(this.wddClaimType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
      {
        case 2:
          this.ucRequestData1.ClearControls();
          this.ucRequesterDataTmp.ClearControls();
          this.ucRequesterDataTmp.EnabledControls(false);
          this.spanRequestData1.Style.Add("display", "none");
          break;
        case 3:
          this.ucRequestData2.ClearControls();
          this.spanRequestData2.Style.Add("display", "none");
          this.ucDataNoAgree1.ClearControls();
          this.ucRequesterDataTmp.ClearControls();
          this.ucRequesterDataTmp.EnabledControls(false);
          this.spanDataNoAgree1.Style.Add("display", "none");
          break;
        case 4:
          this.ucBatchNoAgree1.ClearControls();
          this.spanBatchNoAgree1.Style.Add("display", "none");
          break;
        case 5:
          this.ucProductNoAgree1.ClearControls();
          this.ucRequesterDataTmp.ClearControls();
          this.ucRequesterDataTmp.EnabledControls(false);
          this.spanProductNoAgree1.Style.Add("display", "none");
          break;
        case 6:
          this.ucReimbursementPayment1.ClearControls();
          this.spanReimbursementPayment1.Style.Add("display", "none");
          break;
        case 7:
          this.ucCallCenter1.ClearControls();
          this.ucRequesterDataTmp.ClearControls();
          this.ucRequesterDataTmp.EnabledControls(false);
          this.spanCallCenter1.Style.Add("display", "none");
          break;
        case 9:
          this.ucUseChange1.ClearControls();
          this.ucRequesterDataTmp.ClearControls();
          this.ucRequesterDataTmp.EnabledControls(false);
          this.spanUseChange1.Style.Add("display", "none");
          break;
        case 10:
          this.ucChangeDeliveryPlate1.ClearControls();
          this.spanChangeDeliveryPlate1.Style.Add("display", "none");
          break;
        case 11:
          this.ucRemoveMotivation.ClearControls();
          this.spanRemoveMotivation.Style.Add("display", "none");
          break;
        case 12:
          this.ucEnablePaymentCode.ClearControls();
          this.spanEnablePaymentCode.Style.Add("display", "none");
          break;
        case 13:
          this.ucRemoveRequirement.ClearControls();
          this.spanRemoveRequirement.Style.Add("display", "none");
          break;
        case 14:
          this.ucRegulariceMovementForDeliver.ClearControls();
          this.spanRegulariceMovementForDeliver.Style.Add("display", "none");
          break;
      }
      this.wddClaimType.SelectedValue = "-1";
      this.ClaimData1.ClearControls();
      this.ClaimData1.EnableControls(false);
      this.txtComents.Text = "";
    }

    private RequirementClaim GetObject()
    {
      RequirementClaim requirementClaim = new RequirementClaim();
      requirementClaim.i_RequirementClaimId = Convert.ToInt32(this.Session["i_RequirementClaimId"], (IFormatProvider) CultureInfo.CurrentCulture);
      requirementClaim.i_ClaimTypeId = new int?((int) Convert.ToInt16(this.wddClaimType.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture));
      requirementClaim.v_ClaimDate = this.ClaimData1.ClaimDate.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      requirementClaim.i_Status = new int?(this.ClaimData1.ClaimStatus);
      requirementClaim.i_Priority = new int?(0);
      switch (Convert.ToInt16(this.wddClaimType.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture))
      {
        case 2:
          requirementClaim.v_RequestValues = this.ucRequestData1.GetTexts();
          requirementClaim.v_Requester = this.ucRequesterDataTmp.GetTexts();
          break;
        case 3:
          requirementClaim.v_Requester = this.ucRequesterDataTmp.GetTexts();
          requirementClaim.v_RequestValues = this.ucRequestData2.GetTexts();
          requirementClaim.v_RequestValues = requirementClaim.v_RequestValues + "|" + this.Session["i_VehicleId"].ToString();
          requirementClaim.v_ReadValues = this.ucDataNoAgree1.GetOldTexts();
          requirementClaim.v_InputValues = this.ucDataNoAgree1.GetNewTexts();
          break;
        case 4:
          requirementClaim.v_RequestValues = this.ucBatchNoAgree1.GetTexts();
          requirementClaim.i_ClaimMotiveId = new int?(this.ucBatchNoAgree1.i_ClaimMotiveId);
          break;
        case 5:
          requirementClaim.v_RequestValues = this.ucProductNoAgree1.GetTexts();
          requirementClaim.v_Requester = this.ucRequesterDataTmp.GetTexts();
          break;
        case 6:
          requirementClaim.v_RequestValues = this.ucReimbursementPayment1.GetTexts();
          requirementClaim.i_ClaimMotiveId = new int?(this.ucReimbursementPayment1.i_ClaimMotiveId);
          requirementClaim.v_Requester = this.ucReimbursementPayment1.GetTextsRequester();
          break;
        case 7:
          requirementClaim.v_RequestValues = this.ucCallCenter1.GetTexts();
          requirementClaim.i_RequirementPlateRefId = new int?(Convert.ToInt32(this.Session["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture));
          requirementClaim.v_Requester = this.ucRequesterDataTmp.GetTexts();
          break;
        case 9:
          requirementClaim.v_RequestValues = this.ucUseChange1.GetTexts();
          requirementClaim.v_Requester = this.ucRequesterDataTmp.GetTexts();
          break;
        case 10:
          requirementClaim.v_RequestValues = this.ucChangeDeliveryPlate1.GetTexts();
          requirementClaim.i_RequirementPlateRefId = new int?(Convert.ToInt32(this.Session["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture));
          break;
        case 11:
          requirementClaim.v_RequestValues = this.ucRemoveMotivation.GetTexts();
          requirementClaim.i_RequirementPlateRefId = new int?(Convert.ToInt32(this.Session["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture));
          break;
        case 12:
          requirementClaim.v_RequestValues = this.ucEnablePaymentCode.GetTexts();
          requirementClaim.i_RequirementPlateId = new int?(Convert.ToInt32(this.Session["i_RequirementId"], (IFormatProvider) CultureInfo.CurrentCulture));
          break;
        case 13:
          requirementClaim.v_RequestValues = this.ucRemoveRequirement.GetTexts();
          requirementClaim.i_RequirementPlateId = new int?(Convert.ToInt32(this.Session["i_RequirementId"], (IFormatProvider) CultureInfo.CurrentCulture));
          break;
        case 14:
          requirementClaim.v_RequestValues = this.ucRegulariceMovementForDeliver.GetTexts();
          requirementClaim.i_RequirementPlateRefId = new int?(Convert.ToInt32(this.Session["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture));
          break;
      }
      if (this.Session["SystemUser"] != null)
      {
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        requirementClaim.i_UpdateUserId = new int?(systemUser.i_SystemUserId);
      }
      requirementClaim.d_AssignedDate = new DateTime?(this.ClaimData1.ClaimDate);
      requirementClaim.v_Comments = this.txtComents.Text.TrimEnd();
      requirementClaim.i_AssignedUserId = new int?(0);
      return requirementClaim;
    }

    private void GetTracing(int i_RequirementClaimId, int i_ClaimTypeId, string v_claimcode)
    {
      try
      {
        this.Session[nameof (i_RequirementClaimId)] = (object) i_RequirementClaimId;
        this.txtClaimNumber.Text = v_claimcode;
        this.wddClaimTypeManagement.SelectedValue = i_ClaimTypeId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        DataTable claimTracing = new ClaimTracingQueriesBL().GetClaimTracing(i_RequirementClaimId);
        if (claimTracing == null || claimTracing.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMessageClaimManagement, enmMessageType.Warning, "No se encontraron seguimientos para el reclamo seleccionado");
          this.wdgTracing.DataSource = (object) null;
          this.wdgTracing.DataBind();
        }
        else
          this.SetearList(claimTracing);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageClaimManagement, enmMessageType.Warning, ex.Message);
      }
    }

    private void SetearList(DataTable dt)
    {
      this.wdgTracing.DataSource = (object) null;
      this.wdgTracing.DataSource = (object) dt;
      this.wdgTracing.DataBind();
    }

    public void ExcelExport()
    {
      DataTable dataTable1 = new DataTable();
      short int16_1 = this.wddClaimTypeSearch.SelectedValue == "" ? (short) 0 : Convert.ToInt16(this.wddClaimTypeSearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      short int16_2 = this.wddClaimStatusSearch.SelectedValue == "" ? (short) 0 : Convert.ToInt16(this.wddClaimStatusSearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      string text = this.txtClaimCode.Text;
      string pstrPlateNumber = "";
      DateTime dateTime1 = Convert.ToDateTime((object) this.wdpStartDate.Value, (IFormatProvider) CultureInfo.CurrentCulture);
      DateTime dateTime2 = Convert.ToDateTime((object) this.wdpEndDate.Value, (IFormatProvider) CultureInfo.CurrentCulture);
      try
      {
        dataTable1 = new RequirementClaimQueriesBL().GetAllExport((int) int16_1, (int) int16_2, text, pstrPlateNumber, dateTime1, dateTime2);
        if (dataTable1 == null || dataTable1.Rows.Count == 0)
        {
          this.HidePopup();
          Message.SetMessage(this.lblMessageClaimInbox, enmMessageType.Warning, "No se encontró ninguna información");
          return;
        }
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageClaimInbox, enmMessageType.Error, ex.Message);
        this.HidePopup();
      }
      finally
      {
        this.HidePopup();
      }
      SystemParameterQueriesBL parameterQueriesBl = new SystemParameterQueriesBL();
      string str1 = "";
      DataTable dataTable2 = new DataTable();
      DataTable dataTable3 = new DataTable();
      DataTable dataTable4 = new DataTable();
      DataTable dataTable5 = new DataTable();
      DataTable dataTable6 = new DataTable();
      DataTable dataTable7 = new DataTable();
      if (dataTable1 == null || dataTable1.Rows.Count == 0)
        return;
      string str2 = "GestionReclamos";
      DateTime now = DateTime.Now;
      string[] strArray1 = new string[8];
      strArray1[0] = str2;
      strArray1[1] = now.Day.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      int num = now.Month;
      strArray1[2] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      num = now.Year;
      strArray1[3] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      num = now.Hour;
      strArray1[4] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      num = now.Minute;
      strArray1[5] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      num = now.Second;
      strArray1[6] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      strArray1[7] = ".xls";
      string pstrFileTarget = string.Concat(strArray1);
      DataTable datos = new DataTable();
      datos.Columns.Add("TipoReclamo", Type.GetType("System.String"));
      datos.Columns.Add("CodigoReclamo", Type.GetType("System.String"));
      datos.Columns.Add("FechaReclamo", Type.GetType("System.String"));
      if ((short) 1 == Convert.ToInt16(this.wddClaimTypeSearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
      {
        datos.Columns.Add("Placa", Type.GetType("System.String"));
        datos.Columns.Add("Titulo", Type.GetType("System.String"));
      }
      else if ((short) 2 == Convert.ToInt16(this.wddClaimTypeSearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
      {
        datos.Columns.Add("Placa", Type.GetType("System.String"));
        datos.Columns.Add("Titulo", Type.GetType("System.String"));
      }
      else if ((short) 3 == Convert.ToInt16(this.wddClaimTypeSearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
      {
        datos.Columns.Add("Placa", Type.GetType("System.String"));
        datos.Columns.Add("Titulo", Type.GetType("System.String"));
        datos.Columns.Add("Modelo[Dice]", Type.GetType("System.String"));
        datos.Columns.Add("Marca[Dice]", Type.GetType("System.String"));
        datos.Columns.Add("Serie[Dice]", Type.GetType("System.String"));
        datos.Columns.Add("NombrePropietario[Dice]", Type.GetType("System.String"));
        datos.Columns.Add("TipoDocumentoPropietario[Dice]", Type.GetType("System.String"));
        datos.Columns.Add("NroDocumentoPropietario[Dice]", Type.GetType("System.String"));
        datos.Columns.Add("PlacaAntigua[Dice]", Type.GetType("System.String"));
        datos.Columns.Add("Categoria[Dice]", Type.GetType("System.String"));
        datos.Columns.Add("Modelo[DebeDecir]", Type.GetType("System.String"));
        datos.Columns.Add("Marca[DebeDecir]", Type.GetType("System.String"));
        datos.Columns.Add("Serie[DebeDecir]", Type.GetType("System.String"));
        datos.Columns.Add("NombrePropietario[DebeDecir]", Type.GetType("System.String"));
        datos.Columns.Add("TipoDocumentoPropietario[DebeDecir]", Type.GetType("System.String"));
        datos.Columns.Add("NroDocumentoPropietario[DebeDecir]", Type.GetType("System.String"));
        datos.Columns.Add("PlacaAntigua[DebeDecir]", Type.GetType("System.String"));
        datos.Columns.Add("Categoria[DebeDecir]", Type.GetType("System.String"));
        str1 = "";
        ArrayList arrFilter = new ArrayList()
        {
          (object) SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture),
          (object) "",
          (object) "",
          (object) ""
        };
        this.ViewState["pobjTD"] = (object) parameterQueriesBl.GetbyFilter(arrFilter);
      }
      else if ((short) 5 == Convert.ToInt16(this.wddClaimTypeSearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
      {
        datos.Columns.Add("Placa", Type.GetType("System.String"));
        datos.Columns.Add("NroSolicitud", Type.GetType("System.String"));
        datos.Columns.Add("TipoTramite", Type.GetType("System.String"));
        datos.Columns.Add("ProductoActual", Type.GetType("System.String"));
        datos.Columns.Add("ProductoReemplazo", Type.GetType("System.String"));
        datos.Columns.Add("NuevaNroSolicitud", Type.GetType("System.String"));
        str1 = "";
        ArrayList arrFilter = new ArrayList()
        {
          (object) SystemParameterGroups.ProcessType.ToString((IFormatProvider) CultureInfo.CurrentCulture),
          (object) "",
          (object) "1",
          (object) "1"
        };
        dataTable2 = parameterQueriesBl.GetbyFilter(arrFilter);
        dataTable6 = new ProductQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) -1,
          (object) 3,
          (object) -1,
          (object) "",
          (object) "",
          (object) 1,
          (object) 1
        });
        dataTable7 = new ClaimTracingQueriesBL().GetProductClaimCombination(0);
      }
      else if ((short) 4 == Convert.ToInt16(this.wddClaimTypeSearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
      {
        datos.Columns.Add("NroDespacho", Type.GetType("System.String"));
        datos.Columns.Add("NroLote", Type.GetType("System.String"));
      }
      else if ((short) 7 == Convert.ToInt16(this.wddClaimTypeSearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
      {
        datos.Columns.Add("NroSolicitud", Type.GetType("System.String"));
        datos.Columns.Add("Placa", Type.GetType("System.String"));
        datos.Columns.Add("TipoTramite", Type.GetType("System.String"));
        datos.Columns.Add("NombreProducto", Type.GetType("System.String"));
        datos.Columns.Add("FechaRegistro", Type.GetType("System.String"));
      }
      else if ((short) 6 == Convert.ToInt16(this.wddClaimTypeSearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
      {
        datos.Columns.Add("Banco", Type.GetType("System.String"));
        datos.Columns.Add("TipoOperacion", Type.GetType("System.String"));
        datos.Columns.Add("FechaOperacion", Type.GetType("System.String"));
        datos.Columns.Add("Terminal", Type.GetType("System.String"));
        datos.Columns.Add("NroExpediente", Type.GetType("System.String"));
        datos.Columns.Add("Monto", Type.GetType("System.String"));
        datos.Columns.Add("NombreTitularCta", Type.GetType("System.String"));
        datos.Columns.Add("CtaBancaria", Type.GetType("System.String"));
        datos.Columns.Add("NroCtaBancaria", Type.GetType("System.String"));
        datos.Columns.Add("PuntoEntrega", Type.GetType("System.String"));
        ArrayList arrFilter = new ArrayList()
        {
          (object) ("" + SystemParameterGroups.AffiliatedBank.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
          (object) "",
          (object) "1",
          (object) "1"
        };
        dataTable3 = parameterQueriesBl.GetbyFilter(arrFilter);
        dataTable4 = new RequirementClaimQueriesBL().GetDeliveryPoint();
      }
      else if ((short) 9 == Convert.ToInt16(this.wddClaimTypeSearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
      {
        datos.Columns.Add("Placa", Type.GetType("System.String"));
        datos.Columns.Add("Titulo", Type.GetType("System.String"));
        datos.Columns.Add("Modelo", Type.GetType("System.String"));
        datos.Columns.Add("Marca", Type.GetType("System.String"));
        datos.Columns.Add("Categoria", Type.GetType("System.String"));
        datos.Columns.Add("PlacaAntigua", Type.GetType("System.String"));
        datos.Columns.Add("NumeroSerial", Type.GetType("System.String"));
      }
      else if ((short) 10 == Convert.ToInt16(this.wddClaimTypeSearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
      {
        datos.Columns.Add("Placa", Type.GetType("System.String"));
        datos.Columns.Add("Titulo", Type.GetType("System.String"));
      }
      datos.Columns.Add("UsuarioRegistro", Type.GetType("System.String"));
      datos.Columns.Add("UsuarioActualizacion", Type.GetType("System.String"));
      datos.Columns.Add("MotivoReclamo", Type.GetType("System.String"));
      datos.Columns.Add("Comentario", Type.GetType("System.String"));
      datos.Columns.Add("EstadoReclamo", Type.GetType("System.String"));
      foreach (DataRow row1 in (InternalDataCollectionBase) dataTable1.Rows)
      {
        DataRow row2 = datos.NewRow();
        row2["TipoReclamo"] = row1["v_ClaimType"];
        row2["CodigoReclamo"] = row1["v_ClaimCode"];
        row2["FechaReclamo"] = row1["v_ClaimDate"];
        if (row1["v_Comments"].ToString().Contains("|"))
        {
          string[] strArray2 = row1["v_Comments"].ToString().Split('|');
          row2["MotivoReclamo"] = (object) strArray2[0].ToString();
          row2["Comentario"] = (object) strArray2[1].ToString();
        }
        else
          row2["Comentario"] = row1["v_Comments"];
        row2["UsuarioRegistro"] = row1["v_InsertUserName"];
        row2["UsuarioActualizacion"] = row1["v_UpdateUserName"];
        row2["EstadoReclamo"] = row1["v_status"];
        if (enmClaimType.LogicInconsistency == (enmClaimType) row1["i_ClaimTypeId"])
        {
          string[] source = row1["v_RequestValues"].ToString().Split('|');
          row2["Placa"] = ((IEnumerable<string>) source).Count<string>() > 1 ? (object) source[1].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["Titulo"] = ((IEnumerable<string>) source).Count<string>() > 2 ? (object) source[2].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
        }
        else if (enmClaimType.DataNoFound == (enmClaimType) row1["i_ClaimTypeId"])
        {
          string[] source = row1["v_RequestValues"].ToString().Split('|');
          row2["Placa"] = ((IEnumerable<string>) source).Count<string>() > 0 ? (object) source[0].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["Titulo"] = ((IEnumerable<string>) source).Count<string>() > 1 ? (object) source[1].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
        }
        else if (enmClaimType.DataNoAgree == (enmClaimType) row1["i_ClaimTypeId"])
        {
          string[] source1 = row1["v_RequestValues"].ToString().Split('|');
          row2["Placa"] = ((IEnumerable<string>) source1).Count<string>() > 0 ? (object) source1[0].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["Titulo"] = ((IEnumerable<string>) source1).Count<string>() > 1 ? (object) source1[1].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          string[] source2 = row1["v_ReadValues"].ToString().Split('|');
          row2["Modelo[Dice]"] = ((IEnumerable<string>) source2).Count<string>() > 0 ? (object) source2[0].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["Marca[Dice]"] = ((IEnumerable<string>) source2).Count<string>() > 1 ? (object) source2[1].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["Serie[Dice]"] = ((IEnumerable<string>) source2).Count<string>() > 2 ? (object) source2[2].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["NombrePropietario[Dice]"] = ((IEnumerable<string>) source2).Count<string>() > 3 ? (object) source2[3].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["TipoDocumentoPropietario[Dice]"] = ((IEnumerable<string>) source2).Count<string>() > 4 ? (object) this.ConcatenarTD(source2[4].ToString((IFormatProvider) CultureInfo.CurrentCulture)) : (object) "";
          row2["NroDocumentoPropietario[Dice]"] = ((IEnumerable<string>) source2).Count<string>() > 5 ? (object) source2[5].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["PlacaAntigua[Dice]"] = ((IEnumerable<string>) source2).Count<string>() > 6 ? (object) source2[6].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["Categoria[Dice]"] = ((IEnumerable<string>) source2).Count<string>() > 7 ? (object) source2[7].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          string[] source3 = row1["v_Inputvalues"].ToString().Split('|');
          row2["Modelo[DebeDecir]"] = ((IEnumerable<string>) source3).Count<string>() > 0 ? (object) source3[0].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["Marca[DebeDecir]"] = ((IEnumerable<string>) source3).Count<string>() > 1 ? (object) source3[1].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["Serie[DebeDecir]"] = ((IEnumerable<string>) source3).Count<string>() > 2 ? (object) source3[2].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["NombrePropietario[DebeDecir]"] = ((IEnumerable<string>) source3).Count<string>() > 3 ? (object) this.Concatenar(source3[3].ToString((IFormatProvider) CultureInfo.CurrentCulture)) : (object) "";
          row2["TipoDocumentoPropietario[DebeDecir]"] = ((IEnumerable<string>) source3).Count<string>() > 4 ? (object) this.ConcatenarTDI(source3[4].ToString((IFormatProvider) CultureInfo.CurrentCulture)) : (object) "";
          row2["NroDocumentoPropietario[DebeDecir]"] = ((IEnumerable<string>) source3).Count<string>() > 5 ? (object) this.Concatenar(source3[5].ToString((IFormatProvider) CultureInfo.CurrentCulture)) : (object) "";
          row2["PlacaAntigua[DebeDecir]"] = ((IEnumerable<string>) source3).Count<string>() > 6 ? (object) source3[6].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["Categoria[DebeDecir]"] = ((IEnumerable<string>) source3).Count<string>() > 7 ? (object) source3[7].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
        }
        else if (enmClaimType.ProductNoAgree == (enmClaimType) row1["i_ClaimTypeId"])
        {
          string[] source = row1["v_RequestValues"].ToString().Split('|');
          row2["Placa"] = ((IEnumerable<string>) source).Count<string>() > 0 ? (object) source[0].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["NroSolicitud"] = ((IEnumerable<string>) source).Count<string>() > 1 ? (object) source[1].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          string str3 = ((IEnumerable<string>) source).Count<string>() > 2 ? source[2].ToString((IFormatProvider) CultureInfo.CurrentCulture) : "";
          string str4 = "";
          foreach (DataRow row3 in (InternalDataCollectionBase) dataTable2.Rows)
          {
            if (row3["i_ParameterId"].ToString() == str3)
            {
              str4 = row3["v_Description"].ToString();
              break;
            }
          }
          row2["TipoTramite"] = (object) str4;
          string str5 = ((IEnumerable<string>) source).Count<string>() > 3 ? source[3].ToString((IFormatProvider) CultureInfo.CurrentCulture) : "";
          string str6 = "";
          foreach (DataRow row4 in (InternalDataCollectionBase) dataTable6.Rows)
          {
            if (row4["i_ProductId"].ToString() == str5)
            {
              str6 = row4["v_Name"].ToString();
              break;
            }
          }
          row2["ProductoActual"] = (object) str6;
          string str7 = ((IEnumerable<string>) source).Count<string>() > 4 ? source[4].ToString((IFormatProvider) CultureInfo.CurrentCulture) : "";
          string str8 = "";
          foreach (DataRow row5 in (InternalDataCollectionBase) dataTable7.Rows)
          {
            if (row5["i_RelatedProductId"].ToString() == str7)
            {
              str8 = row5["v_Name"].ToString();
              break;
            }
          }
          row2["ProductoReemplazo"] = (object) str8;
          row2["NuevaNroSolicitud"] = row1["i_requirementplateid"];
        }
        else if (enmClaimType.BatchNoAgree == (enmClaimType) row1["i_ClaimTypeId"])
        {
          string[] source = row1["v_RequestValues"].ToString().Split('|');
          row2["NroDespacho"] = ((IEnumerable<string>) source).Count<string>() > 0 ? (object) source[0].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["NroLote"] = ((IEnumerable<string>) source).Count<string>() > 1 ? (object) source[1].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
        }
        else if (enmClaimType.CallCenter == (enmClaimType) row1["i_ClaimTypeId"])
        {
          string[] source = row1["v_RequestValues"].ToString().Split('|');
          row2["NroSolicitud"] = ((IEnumerable<string>) source).Count<string>() > 0 ? (object) source[0].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["Placa"] = ((IEnumerable<string>) source).Count<string>() > 1 ? (object) source[1].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["TipoTramite"] = ((IEnumerable<string>) source).Count<string>() > 2 ? (object) source[2].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["NombreProducto"] = ((IEnumerable<string>) source).Count<string>() > 3 ? (object) source[3].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["FechaRegistro"] = ((IEnumerable<string>) source).Count<string>() > 4 ? (object) source[4].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
        }
        else if (enmClaimType.ReimbursementPayment == (enmClaimType) row1["i_ClaimTypeId"])
        {
          string[] source4 = row1["v_RequestValues"].ToString().Split('|');
          string str9 = ((IEnumerable<string>) source4).Count<string>() > 0 ? source4[0].ToString((IFormatProvider) CultureInfo.CurrentCulture) : "";
          string str10 = "";
          foreach (DataRow row6 in (InternalDataCollectionBase) dataTable3.Rows)
          {
            if (row6["i_ParameterId"].ToString() == str9)
            {
              str10 = row6["v_Description"].ToString();
              break;
            }
          }
          row2["Banco"] = (object) str10;
          row2["TipoOperacion"] = ((IEnumerable<string>) source4).Count<string>() > 1 ? (source4[1].ToString((IFormatProvider) CultureInfo.CurrentCulture) == "1" ? (object) "Ventanilla" : (object) "Otros") : (object) "";
          row2["FechaOperacion"] = ((IEnumerable<string>) source4).Count<string>() > 2 ? (object) source4[2].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["Terminal"] = ((IEnumerable<string>) source4).Count<string>() > 3 ? (object) source4[3].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["NroExpediente"] = ((IEnumerable<string>) source4).Count<string>() > 4 ? (object) source4[4].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["Monto"] = ((IEnumerable<string>) source4).Count<string>() > 6 ? (object) source4[6].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          string[] source5 = row1["v_Requester"].ToString().Split('|');
          string str11 = ((IEnumerable<string>) source5).Count<string>() > 9 ? source5[9].ToString((IFormatProvider) CultureInfo.CurrentCulture) : "";
          string str12 = "";
          foreach (DataRow row7 in (InternalDataCollectionBase) dataTable4.Rows)
          {
            if (row7["i_LocationId"].ToString() == str11)
            {
              str12 = row7["v_Description"].ToString();
              break;
            }
          }
          row2["NombreTitularCta"] = ((IEnumerable<string>) source5).Count<string>() > 6 ? (object) source5[6].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["CtaBancaria"] = ((IEnumerable<string>) source5).Count<string>() > 7 ? (object) source5[7].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["NroCtaBancaria"] = ((IEnumerable<string>) source5).Count<string>() > 8 ? (object) source5[8].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["PuntoEntrega"] = (object) str12;
        }
        else if (enmClaimType.UseChange == (enmClaimType) row1["i_ClaimTypeId"])
        {
          string[] source = row1["v_RequestValues"].ToString().Split('|');
          row2["Placa"] = ((IEnumerable<string>) source).Count<string>() > 0 ? (object) source[0].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["Titulo"] = ((IEnumerable<string>) source).Count<string>() > 1 ? (object) source[1].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["Modelo"] = ((IEnumerable<string>) source).Count<string>() > 2 ? (object) source[2].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["Marca"] = ((IEnumerable<string>) source).Count<string>() > 3 ? (object) source[3].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["Categoria"] = ((IEnumerable<string>) source).Count<string>() > 4 ? (object) source[4].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["PlacaAntigua"] = ((IEnumerable<string>) source).Count<string>() > 5 ? (object) source[5].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["NumeroSerial"] = ((IEnumerable<string>) source).Count<string>() > 6 ? (object) source[6].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
        }
        datos.Rows.Add(row2);
      }
      ArrayList titulos = new ArrayList();
      DataTable dataTable8 = new DataTable();
      string str13 = this.Server.MapPath("../Claims/") + pstrFileTarget;
      OtherFormats otherFormats = new OtherFormats(str13);
      for (int index = 0; index < datos.Columns.Count; ++index)
        titulos.Add((object) datos.Columns[index].ColumnName);
      otherFormats.ExportClaimBook("Gestión Reclamos", titulos, datos);
      new ExportFile().Download(str13, pstrFileTarget);
    }

    private void LoadPanel()
    {
      if (this.wddClaimType.SelectedValue == "-1" || this.wddClaimType.SelectedValue == "")
        return;
      enmClaimType int32 = (enmClaimType) Convert.ToInt32(this.wddClaimType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      this.spanRequestData1.Style.Add("display", "none");
      this.spanRequestData2.Style.Add("display", "none");
      this.spanDataNoAgree1.Style.Add("display", "none");
      this.spanBatchNoAgree1.Style.Add("display", "none");
      this.spanProductNoAgree1.Style.Add("display", "none");
      this.spanReimbursementPayment1.Style.Add("display", "none");
      this.spanCallCenter1.Style.Add("display", "none");
      this.spanRequesterDataTmp.Style.Add("display", "none");
      this.spanUseChange1.Style.Add("display", "none");
      this.spanChangeDeliveryPlate1.Style.Add("display", "none");
      this.pControls.Visible = true;
      this.Panel2.Visible = false;
      switch (int32)
      {
        case enmClaimType.LogicInconsistency:
          this.spanRequestData1.Style.Add("display", "");
          this.spanRequesterDataTmp.Style.Add("display", "");
          this.Panel2.Visible = true;
          break;
        case enmClaimType.DataNoFound:
          this.spanRequestData1.Style.Add("display", "");
          this.spanRequesterDataTmp.Style.Add("display", "");
          this.Panel2.Visible = true;
          break;
        case enmClaimType.DataNoAgree:
          this.spanRequestData2.Style.Add("display", "");
          this.spanDataNoAgree1.Style.Add("display", "");
          this.spanRequesterDataTmp.Style.Add("display", "");
          this.Panel2.Visible = true;
          break;
        case enmClaimType.BatchNoAgree:
          this.spanBatchNoAgree1.Style.Add("display", "");
          break;
        case enmClaimType.ProductNoAgree:
          this.spanProductNoAgree1.Style.Add("display", "");
          this.spanRequesterDataTmp.Style.Add("display", "");
          this.Panel2.Visible = true;
          break;
        case enmClaimType.ReimbursementPayment:
          this.spanReimbursementPayment1.Style.Add("display", "");
          break;
        case enmClaimType.CallCenter:
          this.spanCallCenter1.Style.Add("display", "");
          this.spanRequesterDataTmp.Style.Add("display", "");
          this.Panel2.Visible = true;
          break;
        case enmClaimType.UseChange:
          this.spanUseChange1.Style.Add("display", "");
          this.spanRequesterDataTmp.Style.Add("display", "");
          this.Panel2.Visible = true;
          break;
        case enmClaimType.ChangeDeliveryPlate:
          this.spanChangeDeliveryPlate1.Style.Add("display", "");
          this.Panel2.Visible = true;
          break;
      }
    }

    private bool ValidateParameters()
    {
      string pstrMessage = "";
      this.lblMsgError.Visible = false;
      if (this.wddClaimType.SelectedValue == "-1" || this.wddClaimType.SelectedValue == "")
      {
        Message.SetMessage(this.lblMsgError, enmMessageType.Warning, "***Advertencia</br>Debe seleccionar un tipo de reclamo");
        return false;
      }
      if (this.wddClaimType.SelectedValue != "-1")
      {
        enmClaimType int32 = (enmClaimType) Convert.ToInt32(this.wddClaimType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        switch (int32)
        {
          case enmClaimType.DataNoFound:
            if (!this.ucRequestData1.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos de solicitud";
              break;
            }
            break;
          case enmClaimType.DataNoAgree:
            if (!this.ucRequestData2.CompletedData)
              pstrMessage = "***Advertencia</br>Faltan completar datos de solicitud";
            if (!this.ucDataNoAgree1.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
          case enmClaimType.BatchNoAgree:
            if (!this.ucBatchNoAgree1.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
          case enmClaimType.ProductNoAgree:
            if (!this.ucProductNoAgree1.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
          case enmClaimType.ReimbursementPayment:
            if (!this.ucReimbursementPayment1.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
          case enmClaimType.CallCenter:
            if (!this.ucCallCenter1.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
          case enmClaimType.ChangeDeliveryPlate:
            if (!this.ucChangeDeliveryPlate1.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
          case enmClaimType.RemoveMotivation:
            if (!this.ucRemoveMotivation.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
          case enmClaimType.EnablePaymentCode:
            if (!this.ucEnablePaymentCode.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
          case enmClaimType.RemoveRequirement:
            if (!this.ucRemoveRequirement.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
          case enmClaimType.RegulariceMovementForDeliver:
            if (!this.ucRegulariceMovementForDeliver.CompletedData)
            {
              pstrMessage = "***Advertencia</br>Faltan completar datos del reclamo";
              break;
            }
            break;
        }
        if (pstrMessage.Length > 0)
        {
          Message.SetMessage(this.lblMsgError, enmMessageType.Warning, pstrMessage);
          return false;
        }
        switch (int32)
        {
          case enmClaimType.DataNoFound:
            pstrMessage = this.ValidateDataRequester();
            break;
          case enmClaimType.DataNoAgree:
            pstrMessage = this.ValidateDataRequester();
            break;
          case enmClaimType.ProductNoAgree:
            pstrMessage = this.ValidateDataRequester();
            break;
          case enmClaimType.ReimbursementPayment:
            pstrMessage = this.ucReimbursementPayment1.ValidateDataRequester();
            break;
          case enmClaimType.CallCenter:
            pstrMessage = this.ValidateDataRequester();
            break;
          case enmClaimType.UseChange:
            pstrMessage = this.ValidateDataRequester();
            break;
        }
        if (pstrMessage.Length > 0)
        {
          Message.SetMessage(this.lblMsgError, enmMessageType.Warning, pstrMessage);
          return false;
        }
      }
      return true;
    }

    private string ValidateDataRequester()
    {
      string str = "";
      if (this.ucRequesterDataTmp.DocumentType == "4")
      {
        if (this.ucRequesterDataTmp.DocumentNumber.Length < 10 || !Format.ValidateRUCstructure(this.ucRequesterDataTmp.DocumentNumber))
          str = "***Advertencia</br>El N° de RUC ingresado no es válido";
      }
      else if (this.ucRequesterDataTmp.DocumentType == "1" && (this.ucRequesterDataTmp.DocumentNumber.Length < 8 || this.ucRequesterDataTmp.DocumentNumber.Length > 8))
        str = "***Advertencia</br>El N° de DNI ingresado no es válido";
      else if (this.ucRequesterDataTmp.TelephoneNumber.Length == 0 && this.ucRequesterDataTmp.Email.Length == 0)
        str = "***Advertencia</br>Debe especificar un Email o número de teléfono";
      else if (this.ucRequesterDataTmp.TelephoneNumber.Length > 0 && this.ucRequesterDataTmp.TelephoneNumber.Length > 0 && this.ucRequesterDataTmp.TelephoneNumber.Length != 9)
        str = "***Advertencia</br>Debe especificar un número de teléfono o celular válido";
      return str;
    }

    private bool ValidateStatus(int i_requirementclaimid, int i_status)
    {
      this.lblMessageClaimManagement.Visible = false;
      string str = "";
      RequirementClaim pobjRC = new RequirementClaim();
      pobjRC.i_RequirementClaimId = i_requirementclaimid;
      pobjRC.i_Status = new int?(i_status);
      try
      {
        str = new RequirementClaimManagementBL().RequirementClaimValidateState(pobjRC);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageClaimManagement, enmMessageType.Warning, ex.Message);
      }
      if (str.Length > 0)
        Message.SetMessage(this.lblMessageClaimManagement, enmMessageType.Warning, "Advertencia : <br>" + str);
      return str.Length == 0;
    }

    private string CreatePopUp(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}'); return false;", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private void ucUseChange1_OnDatosWrong(object sender)
    {
      this.ucRequesterDataTmp.EnabledControls(false);
      this.ClaimData1.EnableControls(false);
    }

    private void ucUseChange1_OnDatosOK(object sender)
    {
      this.ucRequesterDataTmp.EnabledControls(true);
      this.ClaimData1.EnableControls(true);
    }

    private void ucReimbursementPayment1_OnDatosOK(object sender)
    {
      this.ucRequesterDataTmp.EnabledControls(true);
      this.ClaimData1.EnableControls(true);
    }

    private void ucCallCenter1_OnDatosWrong(object sender)
    {
      this.ucRequesterDataTmp.EnabledControls(false);
      this.ClaimData1.EnableControls(false);
    }

    private void ucCallCenter1_OnDatosOK(object sender)
    {
      this.ucRequesterDataTmp.EnabledControls(true);
      this.ClaimData1.EnableControls(true);
    }

    private void ucReimbursementPayment1_OnDatosWrong(object sender)
    {
      this.ucRequesterDataTmp.EnabledControls(false);
      this.ClaimData1.EnableControls(false);
    }

    private string ConcatenarTD(string valuestipdoc)
    {
      string[] strArray = valuestipdoc.Split('/');
      string str1 = "";
      DataTable dataTable = (DataTable) this.ViewState["pobjTD"];
      foreach (string str2 in strArray)
      {
        string str3 = "";
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (row["i_ParameterId"].ToString() == str2)
          {
            str3 = row["v_Value"].ToString();
            break;
          }
        }
        if (str3.Length > 0)
          str1 = str1 + str3 + "/";
      }
      return str1.Length > 0 ? str1.Substring(0, str1.Length - 1) : str1;
    }

    private string ConcatenarTDI(string valuestipdoc)
    {
      string[] strArray = valuestipdoc.Split('/');
      string str1 = "";
      DataTable dataTable = (DataTable) this.ViewState["pobjTD"];
      foreach (string str2 in strArray)
      {
        if (str2.Length > 2)
        {
          string str3 = str2.Substring(2, str2.Length - 2);
          string str4 = "";
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["i_ParameterId"].ToString() == str3)
            {
              str4 = row["v_Value"].ToString();
              break;
            }
          }
          if (str4.Length > 0)
            str1 = str1 + str4 + "/";
        }
      }
      return str1.Length > 0 ? str1.Substring(0, str1.Length - 1) : str1;
    }

    public string Concatenar(string values)
    {
      string[] strArray = values.Split('/');
      string str1 = "";
      foreach (string str2 in strArray)
      {
        if (str2.Length > 2)
          str1 = str1 + str2.Substring(2, str2.Length - 2).Replace(',', ' ') + "/";
      }
      return str1.Length > 0 ? str1.Substring(0, str1.Length - 1) : str1;
    }
  }
}
