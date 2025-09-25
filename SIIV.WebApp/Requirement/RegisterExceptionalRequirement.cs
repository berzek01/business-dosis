// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Requirement.RegisterExceptionalRequirement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Requirement
{
  public class RegisterExceptionalRequirement : Page
  {
    private SystemUser objUserBE;
    private RequirementManagementBL objRequirementManagement;
    private RelatedDocument objRelatedDocument;
    private DataTable dtListGridview = new DataTable();
    private DataTable dtListCombo = new DataTable();
    protected UpdatePanel updatePanel;
    protected HtmlTableCell TagSolicitar;
    protected Label Label1;
    protected Image Image1;
    protected DropDownList cboRelatedDocuments;
    protected RadioButtonList rblVehicleType;
    protected Button btnNext;
    protected Button btnNewDocument;
    protected Button btnReturnPopupConfirmation;
    protected Label lblMessage;
    protected GridView wdgRelatedDocumentList;
    protected Pager custPagerSR;
    protected Button btnJavaScriptResponse;
    protected UpdatePanel UpdatePanel4;
    protected RadioButtonList rblProcess;
    protected Button btnContinue;
    protected UpdatePanel UpdatePanel1;
    protected Fecha dpDocumentDate;
    protected TextBox txtDocumentNumber;
    protected TextBox txtPetitioner;
    protected TextBox txtMaximunQuantity;
    protected FilteredTextBoxExtender ftMaximunQuantity;
    protected Label lblRelatedDocMessage;
    protected HtmlTableRow trMangement;
    protected Button btnSaveDocument;
    protected Button btnCancelDocument;
    protected HtmlTableRow trFinish;
    protected Button wibFinish;
    protected UpdatePanel UpdatePanel2;
    protected Fecha dpDocumentDateAap;
    protected TextBox txtOficio;
    protected TextBox txtSolicitante;
    protected TextBox txtMotivo;
    protected Label Label2;
    protected HtmlTableRow trMangementAAP;
    protected Button btnSave;
    protected Button btnCancel;
    protected HtmlTableRow trFinishAAP;
    protected Button btnFinish;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      this.lblRelatedDocMessage.Visible = false;
      try
      {
        if (!this.Page.IsPostBack)
          this.Initialize();
        else
          this.SearchRelatedDocuments();
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

    protected void custPagerSR_OnPageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        this.SearchRelatedDocumentsList(false);
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

    protected void btnNext_Click(object sender, EventArgs e)
    {
      try
      {
        string str = this.cboRelatedDocuments.SelectedItem.Text.ToString((IFormatProvider) CultureInfo.CurrentCulture).Substring(0, 3);
        if (this.rblVehicleType.SelectedIndex == 2 && str != "AAP")
          throw new HandledException(1, "El Nro de Oficio no corresponde al tipo seleccionado");
        if (this.rblVehicleType.SelectedIndex != 2 && str == "AAP")
          throw new HandledException(1, "El tipo seleccionado no corresponde al Nro de Oficio");
        if (this.cboRelatedDocuments.SelectedItem.Text.ToString((IFormatProvider) CultureInfo.CurrentCulture) == "")
          throw new HandledException(1, "Ya se ha superado el número de matrículas del oficio seleccionado");
        Convert.ToInt32(this.cboRelatedDocuments.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        if (Convert.ToInt32((this.ViewState["RelatedDocuments2"] as DataTable).Rows[this.cboRelatedDocuments.SelectedIndex]["i_ActualQuantity"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture) >= Convert.ToInt32((this.ViewState["RelatedDocuments2"] as DataTable).Rows[this.cboRelatedDocuments.SelectedIndex]["i_MaximumQuantity"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture))
          throw new HandledException(1, "Ya se ha superado el número de matrículas del oficio seleccionado");
        StringBuilder stringBuilder = new StringBuilder();
        if (this.rblVehicleType.SelectedIndex == 2)
        {
          this.Response.Redirect("~/Requirement/BeginRequirement.aspx?t=" + this.rblVehicleType.SelectedValue + "&idDoc=" + this.cboRelatedDocuments.SelectedValue, false);
        }
        else
        {
          stringBuilder.Append("<script language='javascript'>");
          stringBuilder.Append("$('#' + 'Div1').dialog({ autoOpen: false, bgiframe: false, modal: true, width: '650px', height: 'auto', title: 'SELECCIONAR TIPO DE TRAMITE',resizable: 'false' });");
          stringBuilder.Append("$('#' + 'Div1').dialog('open');");
          stringBuilder.Append("$('#' + 'Div1').parent().appendTo($('form:first'));");
          stringBuilder.Append("</script>");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "JSCR", stringBuilder.ToString(), false);
        }
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

    protected void btnContinue_Click(object sender, EventArgs e)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<script language='javascript'>");
      stringBuilder.Append("$('#' + 'Div1').dialog('close');");
      stringBuilder.Append("</script>");
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "JSCR", stringBuilder.ToString(), false);
      this.Response.Redirect("~/Requirement/RegisterRequirementSpecial.aspx?idDoc=" + this.cboRelatedDocuments.SelectedValue + "&veht=" + this.rblVehicleType.SelectedValue + "&Prot=" + this.rblProcess.SelectedValue, false);
    }

    protected void wdgRelatedDocumentList_PageIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.SearchRelatedDocuments();
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

    protected void btnJavaScriptResponse_Click(object sender, EventArgs e)
    {
      try
      {
        this.SearchRelatedDocuments();
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

    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.txtDocumentNumber.Text.Trim() == "")
          throw new HandledException(1, "Debe ingresar el Nro de Oficio");
        if (this.txtPetitioner.Text.Trim() == "")
          throw new HandledException(1, "Debe ingresar la Autoridad Firmante");
        if (this.txtMaximunQuantity.Text.Trim() == "")
          throw new HandledException(1, "Debe ingresar el Nro de Matrículas");
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - SpecialRequirement.aspx");
        this.objRelatedDocument = new RelatedDocument();
        this.objRelatedDocument.v_Description = this.txtDocumentNumber.Text.Trim();
        this.objRelatedDocument.v_Petitioner = this.txtPetitioner.Text.Trim();
        this.objRelatedDocument.v_Motive = string.Empty;
        this.objRelatedDocument.i_MaximumQuantity = new int?(Convert.ToInt32(this.txtMaximunQuantity.Text.Trim(), (IFormatProvider) CultureInfo.CurrentCulture));
        this.objRelatedDocument.i_InsertUserId = new int?(systemUser.i_SystemUserId);
        this.objRelatedDocument.d_CreationDate = new DateTime?(this.dpDocumentDate.Value);
        this.objRequirementManagement = new RequirementManagementBL();
        this.objRequirementManagement.RelatedDocumentInsert(this.objRelatedDocument);
        Message.SetMessage(this.lblMessage, new HandledException(2, "Los datos han sido registrados correctamente"));
        this.trFinish.Visible = true;
        this.trMangement.Visible = false;
        this.txtDocumentNumber.Enabled = false;
        this.txtPetitioner.Enabled = false;
        this.txtMaximunQuantity.Enabled = false;
        this.dpDocumentDate.Enabled = false;
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblRelatedDocMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblRelatedDocMessage, new HandledException(-100, ex));
      }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.txtOficio.Text.Trim() == "")
          throw new HandledException(1, "Debe ingresar el Nro de Oficio");
        if (this.txtSolicitante.Text.Trim() == "")
          throw new HandledException(1, "Debe ingresar el nombre del Solicitante");
        if (this.txtMotivo.Text.Trim() == "")
          throw new HandledException(1, "Debe ingresar el Motivo");
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - SpecialRequirement.aspx");
        this.objRelatedDocument = new RelatedDocument();
        this.objRelatedDocument.v_Description = this.txtOficio.Text.Trim();
        this.objRelatedDocument.v_Petitioner = this.txtSolicitante.Text.Trim();
        this.objRelatedDocument.v_Motive = this.txtMotivo.Text.Trim();
        this.objRelatedDocument.i_MaximumQuantity = new int?(1);
        this.objRelatedDocument.i_InsertUserId = new int?(systemUser.i_SystemUserId);
        this.objRelatedDocument.d_CreationDate = new DateTime?(this.dpDocumentDate.Value);
        this.objRequirementManagement = new RequirementManagementBL();
        this.objRequirementManagement.RelatedDocumentInsert(this.objRelatedDocument);
        Message.SetMessage(this.lblMessage, new HandledException(2, "Los datos han sido registrados correctamente"));
        this.trFinishAAP.Visible = true;
        this.trMangementAAP.Visible = false;
        this.txtOficio.Enabled = false;
        this.txtSolicitante.Enabled = false;
        this.txtMotivo.Enabled = false;
        this.dpDocumentDateAap.Enabled = false;
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblRelatedDocMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblRelatedDocMessage, new HandledException(-100, ex));
      }
    }

    protected void btnCancelDocument_Click(object sender, EventArgs e)
    {
      try
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<script language='javascript'>");
        stringBuilder.Append("$('#' + 'Div2').dialog('close');");
        stringBuilder.Append("</script>");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "JSCR", stringBuilder.ToString(), false);
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblRelatedDocMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblRelatedDocMessage, new HandledException(-100, ex));
      }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      try
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<script language='javascript'>");
        stringBuilder.Append("$('#' + 'Div3').dialog('close');");
        stringBuilder.Append("</script>");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "JSCR", stringBuilder.ToString(), false);
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblRelatedDocMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblRelatedDocMessage, new HandledException(-100, ex));
      }
    }

    protected void btnNewDocument_Click(object sender, EventArgs e)
    {
      try
      {
        this.txtDocumentNumber.Text = "";
        this.txtPetitioner.Text = "";
        this.txtMaximunQuantity.Text = "";
        this.txtSolicitante.Text = "";
        this.txtOficio.Text = "";
        this.txtMotivo.Text = "";
        this.dpDocumentDate.Value = DateTime.Now;
        this.dpDocumentDateAap.Value = DateTime.Now;
        this.trFinish.Visible = false;
        this.trFinishAAP.Visible = false;
        this.trMangement.Visible = true;
        this.trMangementAAP.Visible = true;
        this.txtDocumentNumber.Enabled = true;
        this.txtPetitioner.Enabled = true;
        this.txtSolicitante.Enabled = true;
        this.txtMaximunQuantity.Enabled = true;
        this.dpDocumentDate.Enabled = true;
        this.txtOficio.Enabled = true;
        this.txtMotivo.Enabled = true;
        StringBuilder stringBuilder = new StringBuilder();
        if (this.rblVehicleType.SelectedIndex == 2)
        {
          this.txtOficio.Text = new RequirementQueriesBL().CodeOfficeGenerate();
          this.txtOficio.Enabled = false;
          stringBuilder.Append("<script language='javascript'>");
          stringBuilder.Append("$('#' + 'Div3').dialog({ autoOpen: false, bgiframe: false, modal: true, width: '500px', height: 'auto', title: 'NUEVO OFICIO',resizable: 'false' });");
          stringBuilder.Append("$('#' + 'Div3').dialog('open');");
          stringBuilder.Append("$('#' + 'Div3').parent().appendTo($('form:first'));");
          stringBuilder.Append("</script>");
        }
        else
        {
          stringBuilder.Append("<script language='javascript'>");
          stringBuilder.Append("$('#' + 'Div2').dialog({ autoOpen: false, bgiframe: false, modal: true, width: '500px', height: 'auto', title: 'NUEVO OFICIO',resizable: 'false' });");
          stringBuilder.Append("$('#' + 'Div2').dialog('open');");
          stringBuilder.Append("$('#' + 'Div2').parent().appendTo($('form:first'));");
          stringBuilder.Append("</script>");
        }
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "JSCR", stringBuilder.ToString(), false);
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

    protected void wibFinish_Click(object sender, EventArgs e)
    {
      try
      {
        this.SearchRelatedDocuments();
        this.GetRelatedDocument();
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<script language='javascript'>");
        stringBuilder.Append("$('#' + 'Div2').dialog('close');");
        stringBuilder.Append("</script>");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "JSCR", stringBuilder.ToString(), false);
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

    protected void btnFinish_Click(object sender, EventArgs e)
    {
      try
      {
        this.SearchRelatedDocuments();
        this.GetRelatedDocument();
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<script language='javascript'>");
        stringBuilder.Append("$('#' + 'Div3').dialog('close');");
        stringBuilder.Append("</script>");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "JSCR", stringBuilder.ToString(), false);
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

    public void Initialize()
    {
      try
      {
        this.objUserBE = this.Session["SystemUser"] as SystemUser;
        string str1 = ConfigurationManager.AppSettings["RolesNuevoOficio"].ToString();
        char[] chArray = new char[1]{ '|' };
        foreach (string str2 in str1.Split(chArray))
        {
          if (this.objUserBE.i_RoleConfigId.ToString() == str2.ToString())
          {
            this.btnNewDocument.Visible = true;
            this.rblVehicleType.Items.Add(new ListItem("V. AAP", "AAP"));
            break;
          }
        }
        this.SearchRelatedDocuments();
        this.GetRelatedDocument();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchRelatedDocuments()
    {
      try
      {
        this.SearchRelatedDocumentsList(true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchRelatedDocumentsList(bool pboolLoadPager)
    {
      try
      {
        this.objUserBE = this.Session["SystemUser"] as SystemUser;
        int startRowIndex = pboolLoadPager ? 1 : this.custPagerSR.CurrentPageNumber;
        int maxRows = this.custPagerSR.CurrentPageSize == 0 ? 10 : this.custPagerSR.CurrentPageSize;
        int pintTotalRows;
        DataTable dataTable = new RequirementQueriesBL().RelatedDocumentRead(this.objUserBE.i_RoleConfigId, startRowIndex, maxRows, out pintTotalRows);
        if (dataTable == null || dataTable.Rows.Count == 0)
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
        this.ViewState["RelatedDocumentsRead"] = (object) dataTable;
        int num = pintTotalRows;
        this.wdgRelatedDocumentList.DataSource = (object) (this.ViewState["RelatedDocumentsRead"] as DataTable);
        this.wdgRelatedDocumentList.DataBind();
        this.custPagerSR.TotalPages = num % maxRows == 0 ? num / maxRows : num / maxRows + 1;
        this.custPagerSR.TotalRecordCount = pintTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerSR.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void GetRelatedDocument()
    {
      try
      {
        this.ViewState["RelatedDocuments2"] = this.Session["SystemUser"] != null ? (object) new RequirementQueriesBL().GetRelatedDocument((this.Session["SystemUser"] as SystemUser).i_RoleConfigId) : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - SpecialRequirement.aspx");
        this.cboRelatedDocuments.DataSource = (object) (this.ViewState["RelatedDocuments2"] as DataTable);
        this.cboRelatedDocuments.DataTextField = "RelatedDocument";
        this.cboRelatedDocuments.DataValueField = "i_RelatedDocumentId";
        this.cboRelatedDocuments.DataBind();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private string CreatePopUp(string pstrtitle, int pintSupplierId)
    {
      string empty = string.Empty;
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}'); return false;", (object) string.Format((IFormatProvider) CultureInfo.CurrentCulture, "RegisterRelatedDocument.aspx"), (object) pstrtitle, (object) "700px", (object) "350px");
    }

    protected void wdgRelatedDocumentList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        if (!e.CommandName.Equals("Delete", StringComparison.CurrentCulture))
          return;
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - PlateDeliveryMassive.aspx");
        int num = 0;
        if (systemUser != null)
          num = systemUser.i_SystemUserId;
        DataTable dataTable = new DataTable();
        RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
        int int32_1 = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = this.wdgRelatedDocumentList.Rows[int32_1];
        if (row == null)
          throw new HandledException(4, "Error de selección.", "'wdgRequestPlateList' - WarehouseControlListDetail.aspx");
        int int32_2 = Convert.ToInt32(this.wdgRelatedDocumentList.DataKeys[int32_1]["i_RelatedDocumentId"]);
        string text = row.Cells[0].Text;
        this.Session["i_RelatedDocumentId"] = (object) int32_2;
        string empty = string.Empty;
        this.CreatePopUpServer("SIIV - Solicitudes - Especiales", "../../UserControls/PopupConfirmation.aspx?MessageTypeId=2&MessageText=¿Desea Eliminar el Oficio Nro : " + text + " ?", "360px", "190px");
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

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      try
      {
        int i_SystemUserId = 0;
        int i_RelatedDocumentId = Convert.ToInt32(this.Session["i_RelatedDocumentId"]);
        switch (new RequirementQueriesBL().DeleteOffice(i_RelatedDocumentId, i_SystemUserId))
        {
          case 0:
            throw new HandledException(1, "No se pudo eliminar el Oficio, ya que Tiene Solicitudes Asignadas");
          case 1:
            this.dtListGridview = (DataTable) this.ViewState["RelatedDocumentsRead"];
            this.dtListGridview.Rows.Remove(this.dtListGridview.Rows.Cast<DataRow>().ToList<DataRow>().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (item => Convert.ToInt32(item["i_RelatedDocumentId"]) == i_RelatedDocumentId)));
            this.wdgRelatedDocumentList.DataSource = (object) this.dtListGridview;
            this.wdgRelatedDocumentList.DataBind();
            this.ViewState["RelatedDocumentsRead"] = (object) this.dtListGridview;
            this.dtListCombo = (DataTable) this.ViewState["RelatedDocuments2"];
            this.dtListCombo.Rows.Remove(this.dtListCombo.Rows.Cast<DataRow>().ToList<DataRow>().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (item => Convert.ToInt32(item["i_RelatedDocumentId"]) == i_RelatedDocumentId)));
            this.cboRelatedDocuments.DataSource = (object) this.dtListCombo;
            this.cboRelatedDocuments.DataTextField = "RelatedDocument";
            this.cboRelatedDocuments.DataValueField = "i_RelatedDocumentId";
            this.cboRelatedDocuments.DataBind();
            this.ViewState["RelatedDocuments2"] = (object) this.dtListCombo;
            this.Session["i_RelatedDocumentId"] = (object) null;
            throw new HandledException(2, "Se Eliminó El Oficio Seleccionado exitosamente");
          default:
            throw new HandledException(1, "No se pudo eliminar el Oficio seleccionado");
        }
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

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void wdgRelatedDocumentList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
        return;
      SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - PlateDeliveryMassive.aspx");
      if (((IEnumerable<string>) ConfigurationManager.AppSettings["RolesNuevoOficio"].ToString().Split('|')).Contains<string>(this.objUserBE.i_RoleConfigId.ToString()))
      {
        e.Row.Cells[2].Visible = true;
        e.Row.Cells[8].Visible = true;
        this.wdgRelatedDocumentList.Columns[2].Visible = true;
        ((WebControl) e.Row.Cells[8].Controls[0]).ToolTip = "Eliminar";
      }
      else
      {
        e.Row.Cells[2].Visible = false;
        this.wdgRelatedDocumentList.Columns[2].Visible = false;
        e.Row.Cells[8].Visible = false;
      }
    }

    protected void wdgRelatedDocumentList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }
  }
}
