// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Operation.RequirementAssociated
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Operation
{
  public class RequirementAssociated : Page
  {
    private AssociatedQueriesBL pobjAssociatedQueriesBL;
    protected UpdatePanel UpdatePanel1;
    protected Label Label3;
    protected DropDownList wddTypeAssociated;
    protected DropDownList DropDownList1;
    protected Label Label1;
    protected TextBox txtNumberDocument;
    protected Button wibSearch;
    protected Button wibNew;
    protected Label Label2;
    protected TextBox txtAssociated;
    protected Button wibExport;
    protected GridView wdgListAssociated;
    protected GridView wdgListAssociatedNew;
    protected Pager custPagerReqAso;
    protected Label lblCount;
    protected Button btnJavaScriptResponse;
    protected Label lblMessage;
    protected Button Button1;
    protected Button Button2;
    protected UpdatePanel UpdatePanel2;
    protected Button wibReturn;
    protected Button wibSearchUser;
    protected Button wibNewChild;
    protected GridView wdgListAssociatedChild;
    protected Label lblCountChild;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.ViewState["t"] = (object) Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.pobjAssociatedQueriesBL = new AssociatedQueriesBL();
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RequirementAssociated.aspx");
        this.ViewState["userRole"] = (object) systemUser.i_RoleConfigId;
        if (this.Session["ApplicationId"] != null)
        {
          int i_ApplicationId = this.Session["ApplicationId"] != null ? Convert.ToInt32(this.Session["ApplicationId"], (IFormatProvider) CultureInfo.CurrentCulture) : throw new HandledException(3, "La sesión ha expirado.", "'ApplicationId' - RequirementAssociated.aspx");
          string userExtendedAction = this.pobjAssociatedQueriesBL.GetSystemUserExtendedAction(systemUser.i_SystemUserId, i_ApplicationId);
          if (userExtendedAction != "")
          {
            if (Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("7", StringComparison.CurrentCulture))) == "7")
            {
              string script = UtilDA.ActiveTabIndex("tabs", 0, "1");
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
              this.wibNewChild.Enabled = false;
              this.wibNew.Enabled = true;
              this.wibSearchUser.Visible = false;
              this.wibReturn.Visible = true;
            }
            else
            {
              string script = UtilDA.ActiveTabIndex("tabs", 1, "0");
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script, true);
              if (Convert.ToInt32(this.ViewState["userRole"]) == 45)
              {
                this.wddTypeAssociated.Enabled = true;
                this.txtAssociated.Enabled = true;
                this.txtNumberDocument.Enabled = true;
              }
              else
                this.DropDownList1.Enabled = true;
              this.wibNewChild.Enabled = true;
              this.wibNew.Enabled = false;
              this.wibSearchUser.Visible = true;
              this.wibReturn.Visible = false;
            }
          }
          else
          {
            string script = UtilDA.ActiveTabIndex("tabs", 1, "0");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script, true);
            if (Convert.ToInt32(this.ViewState["userRole"]) == 45)
            {
              this.wddTypeAssociated.Enabled = true;
              this.txtAssociated.Enabled = true;
              this.txtNumberDocument.Enabled = true;
            }
            this.wibNew.Enabled = false;
            this.wibSearchUser.Visible = true;
            this.wibReturn.Visible = false;
          }
          if (systemUser.i_AssociatedId != 0)
            this.ListAssociatedChild(systemUser.i_AssociatedId);
        }
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    private void TableColumns1(DataTable dtResult)
    {
      dtResult.Columns.Add("i_SystemUserId", typeof (int));
      dtResult.Columns.Add("i_AssociatedId", typeof (int));
      dtResult.Columns.Add("i_Status", typeof (int));
      dtResult.Columns.Add("v_DocumentNumber", typeof (string));
      dtResult.Columns.Add("v_ReasonSocial", typeof (string));
      dtResult.Columns.Add("v_Telephone", typeof (string));
      dtResult.Columns.Add("v_Address", typeof (string));
      dtResult.Columns.Add("v_Email", typeof (string));
      dtResult.Columns.Add("i_StatusDesc", typeof (int));
      dtResult.Columns.Add("i_NumberPlates", typeof (int));
      dtResult.Columns.Add("i_StatusSlow", typeof (int));
      dtResult.Columns.Add("f_Amount", typeof (int));
      dtResult.Columns.Add("i_StatusMovement", typeof (int));
      dtResult.Columns.Add("i_StatusMovementCamceled", typeof (int));
    }

    private void TableColumns2(DataTable dtResult)
    {
      dtResult.Columns.Add("i_SystemUserId", typeof (int));
      dtResult.Columns.Add("v_Alias", typeof (string));
      dtResult.Columns.Add("v_ReasonSocial", typeof (string));
      dtResult.Columns.Add("v_RepresentativeName", typeof (string));
      dtResult.Columns.Add("v_Charge", typeof (string));
      dtResult.Columns.Add("i_Status", typeof (int));
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchReqAso();

    protected void wibNew_Click(object sender, EventArgs e)
    {
      try
      {
        this.Response.Redirect("ChangesAssociated1.aspx?i_Status=" + "0" + "&i_PlateTypeId=" + Convert.ToInt32(this.ViewState["t"]).ToString());
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void wibExport_Click(object sender, EventArgs e)
    {
      try
      {
        this.ExportList();
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void wdgListAssociated_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        int int32_1 = Convert.ToInt32(e.CommandArgument);
        if (this.wdgListAssociated.Rows[int32_1] == null)
          throw new HandledException(4, "Error de selección.", "'wdgListAssociated' - RequirementAssociated.aspx");
        if (e.CommandName.Equals("Edit", StringComparison.CurrentCulture))
        {
          this.Response.Redirect("ChangesAssociated1.aspx?i_SystemUserId=" + this.wdgListAssociated.DataKeys[int32_1]["i_SystemUserId"].ToString() + "&i_Status=" + this.wdgListAssociated.DataKeys[int32_1]["i_Status"].ToString() + "&i_PlateTypeId=" + Convert.ToInt32(this.ViewState["t"]).ToString());
          this.ViewState["i_AssociatedId"] = (object) null;
        }
        else if (e.CommandName.Equals("View", StringComparison.CurrentCulture))
        {
          int int32_2 = Convert.ToInt32(this.wdgListAssociated.DataKeys[int32_1]["i_AssociatedId"].ToString());
          this.ViewState["i_AssociatedId"] = (object) int32_2;
          this.ViewState["i_SystemUserId"] = (object) this.wdgListAssociated.DataKeys[int32_1]["i_SystemUserId"].ToString();
          this.ListAssociatedChild(int32_2);
          string script = UtilDA.ActiveTabIndex("tabs", 1, "");
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
        }
        else
        {
          if (!e.CommandName.Equals("pdf", StringComparison.CurrentCulture))
            return;
          this.ViewState["i_SystemUserId"] = (object) this.wdgListAssociated.DataKeys[int32_1]["i_SystemUserId"].ToString();
          string script = "Exportpdf();";
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
          this.HidePopup();
        }
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void custPagerReqAso_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        this.pobjAssociatedQueriesBL = new AssociatedQueriesBL();
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RequirementAssociated.aspx");
        if (this.Session["ApplicationId"] == null)
          return;
        int int32_1 = Convert.ToInt32(this.Session["ApplicationId"], (IFormatProvider) CultureInfo.CurrentCulture);
        int i_IsAssociatedAAP = systemUser.i_RoleConfigId == 45 ? Convert.ToInt32(this.wddTypeAssociated.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) : (systemUser.i_RoleConfigId == 51 ? Convert.ToInt32(this.DropDownList1.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) : 0);
        int int32_2 = Convert.ToInt32(this.ViewState["t"]);
        int i_Product = this.SetProductId(int32_2);
        string userExtendedAction = this.pobjAssociatedQueriesBL.GetSystemUserExtendedAction(systemUser.i_SystemUserId, int32_1);
        if (userExtendedAction != "")
        {
          string v_ExtendedAction = Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("7", StringComparison.CurrentCulture)));
          if (v_ExtendedAction == "7")
          {
            this.SearchReqAsoList(i_IsAssociatedAAP, this.txtNumberDocument.Text.Trim(), this.txtAssociated.Text, systemUser.i_SystemUserId, systemUser.i_RoleConfigId, v_ExtendedAction, int32_2, i_Product, false);
          }
          else
          {
            this.SearchReqAsoList(i_IsAssociatedAAP, this.txtNumberDocument.Text.Trim(), this.txtAssociated.Text, systemUser.i_SystemUserId, systemUser.i_RoleConfigId, "0", int32_2, i_Product, false);
            this.DisableControls();
          }
        }
        else
        {
          this.SearchReqAsoList(i_IsAssociatedAAP, this.txtNumberDocument.Text.Trim(), this.txtAssociated.Text, systemUser.i_SystemUserId, systemUser.i_RoleConfigId, "0", int32_2, i_Product, true);
          this.DisableControls();
        }
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void btnJavaScriptResponse_Click(object sender, EventArgs e)
    {
      this.pobjAssociatedQueriesBL = new AssociatedQueriesBL();
      SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RequirementAssociated.aspx");
      if (this.Session["ApplicationId"] != null)
      {
        int int32_1 = Convert.ToInt32(this.Session["ApplicationId"], (IFormatProvider) CultureInfo.CurrentCulture);
        int i_IsAssociatedAAP = systemUser.i_RoleConfigId == 45 ? Convert.ToInt32(this.wddTypeAssociated.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) : (systemUser.i_RoleConfigId == 51 ? Convert.ToInt32(this.DropDownList1.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) : 0);
        int int32_2 = Convert.ToInt32(this.ViewState["t"]);
        int i_Product = this.SetProductId(int32_2);
        string userExtendedAction = this.pobjAssociatedQueriesBL.GetSystemUserExtendedAction(systemUser.i_SystemUserId, int32_1);
        if (userExtendedAction != "")
        {
          string v_ExtendedAction = Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("7", StringComparison.CurrentCulture)));
          if (v_ExtendedAction == "7")
          {
            this.SearchReqAsoList(i_IsAssociatedAAP, this.txtNumberDocument.Text.Trim(), this.txtAssociated.Text, systemUser.i_SystemUserId, systemUser.i_RoleConfigId, v_ExtendedAction, int32_2, i_Product, false);
            this.HidePopup();
          }
          else
          {
            this.SearchReqAsoList(i_IsAssociatedAAP, this.txtNumberDocument.Text.Trim(), this.txtAssociated.Text, systemUser.i_SystemUserId, systemUser.i_RoleConfigId, "0", int32_2, i_Product, false);
            this.DisableControls();
            this.HidePopup();
          }
        }
        else
        {
          this.SearchReqAsoList(i_IsAssociatedAAP, this.txtNumberDocument.Text.Trim(), this.txtAssociated.Text, systemUser.i_SystemUserId, systemUser.i_RoleConfigId, "0", int32_2, i_Product, true);
          this.DisableControls();
          this.HidePopup();
        }
      }
      if (this.ViewState["i_AssociatedId"] == null)
        return;
      this.ListAssociatedChild(Convert.ToInt32(this.ViewState["i_AssociatedId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture));
    }

    protected void wibReturn_Click(object sender, EventArgs e)
    {
      string script = UtilDA.ActiveTabIndex("tabs", 0, "");
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script, true);
    }

    protected void wibSearchUser_Click(object sender, EventArgs e)
    {
      this.wibSearch_Click((object) null, (EventArgs) null);
      string script = UtilDA.ActiveTabIndex("tabs", 0, "1");
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script, true);
    }

    protected void wibNewChild_Click(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RequirementAssociated.aspx");
        if (systemUser == null)
          return;
        int iAssociatedId = systemUser.i_AssociatedId;
        int iSystemUserId = systemUser.i_SystemUserId;
        int int32 = Convert.ToInt32(this.ViewState["t"]);
        this.CreatePopUpServer("Registro de Sub Asociado", "ChangesAssociatedChild.aspx?i_AssociatedIdNew=" + iAssociatedId.ToString() + "&i_SystemUserIdNew=" + iSystemUserId.ToString() + "&i_PlateTypeId=" + int32.ToString(), "620px", "310px");
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void wdgListAssociatedChild_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        int int32_1 = Convert.ToInt32(e.CommandArgument);
        if (this.wdgListAssociatedChild.Rows[int32_1] == null)
          throw new HandledException(4, "Error de selección.", "'wdgListAssociatedChild' - RequirementAssociated.aspx");
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RequirementAssociated.aspx");
        if (!e.CommandName.Equals("View", StringComparison.CurrentCulture))
          return;
        int iAssociatedId = systemUser.i_AssociatedId;
        string str = this.wdgListAssociatedChild.DataKeys[int32_1]["i_SystemUserId"].ToString();
        int int32_2 = Convert.ToInt32(this.ViewState["t"]);
        this.CreatePopUpServer("Modificar Sub Asociado", "ChangesAssociatedChild.aspx?i_AssociatedId=" + iAssociatedId.ToString() + "&i_SystemUserId=" + str + "&i_PlateTypeId=" + int32_2.ToString(), "620px", "310px");
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable objDataTable = (DataTable) this.Session["dtExport"];
        ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
        List<ClassColumns> classColumnsList = new List<ClassColumns>();
        foreach (DataControlField column in (StateManagedCollection) this.wdgListAssociated.Columns)
        {
          if (column.Visible && column.GetType().Name == "BoundField")
          {
            BoundField boundField = (BoundField) column;
            classColumnsList.Add(new ClassColumns(boundField.DataField, 1, (int) column.ControlStyle.Width.Value, boundField.HeaderText));
          }
        }
        exportToExcelDataGrid.clsTitle = classColumnsList;
        exportToExcelDataGrid.AgregarHojaLibro(objDataTable, "Lista Asociados");
        exportToExcelDataGrid.CerrarLibro();
        byte[] buffer = exportToExcelDataGrid.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=ListadoAsociados.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.ViewState["i_SystemUserId"] == null)
          return;
        DataTable dataTable1 = new DataTable();
        this.pobjAssociatedQueriesBL = new AssociatedQueriesBL();
        int int32_1 = Convert.ToInt32(this.ViewState["i_SystemUserId"].ToString());
        int int32_2 = Convert.ToInt32(this.ViewState["t"]);
        int i_Product = this.SetProductId(int32_2);
        DataTable dataTable2 = this.pobjAssociatedQueriesBL.ReportAssociated(int32_1, int32_2, i_Product);
        if (dataTable2.Rows.Count == 0)
          throw new HandledException(1, "Advertencia </br> No se pudo mostrar el reporte debido a que no contiene un tramite.");
        ReportDocument reportDocument = new ReportDocument();
        string filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportAssociated.rpt";
        reportDocument.Load(filename);
        reportDocument.SetDataSource(dataTable2);
        reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "ReportAssociated");
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    private int SetProductId(int i_PlateTypeId)
    {
      try
      {
        int num = 0;
        WarehouseExhibitionQueriesBL exhibitionQueriesBl = new WarehouseExhibitionQueriesBL();
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = exhibitionQueriesBl.SpecialPlateWarehouseProductGet(i_PlateTypeId);
        switch (i_PlateTypeId)
        {
          case 6:
            num = 0;
            break;
          case 7:
            num = Convert.ToInt32(dataTable2.Rows[0]["i_Productid"].ToString());
            break;
          case 11:
            num = Convert.ToInt32(dataTable2.Rows[0]["i_Productid"].ToString());
            break;
        }
        return num;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchReqAso()
    {
      try
      {
        this.pobjAssociatedQueriesBL = new AssociatedQueriesBL();
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RequirementAssociated.aspx");
        if (this.Session["ApplicationId"] == null)
          return;
        int int32_1 = Convert.ToInt32(this.Session["ApplicationId"], (IFormatProvider) CultureInfo.CurrentCulture);
        int i_IsAssociatedAAP = systemUser.i_RoleConfigId == 45 ? Convert.ToInt32(this.wddTypeAssociated.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) : (systemUser.i_RoleConfigId == 51 ? Convert.ToInt32(this.DropDownList1.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) : 0);
        int int32_2 = Convert.ToInt32(this.ViewState["t"]);
        int i_Product = this.SetProductId(int32_2);
        string userExtendedAction = this.pobjAssociatedQueriesBL.GetSystemUserExtendedAction(systemUser.i_SystemUserId, int32_1);
        if (userExtendedAction != "")
        {
          string v_ExtendedAction = Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("7", StringComparison.CurrentCulture)));
          if (v_ExtendedAction == "7")
          {
            this.SearchReqAsoList(i_IsAssociatedAAP, this.txtNumberDocument.Text.Trim(), this.txtAssociated.Text, systemUser.i_SystemUserId, systemUser.i_RoleConfigId, v_ExtendedAction, int32_2, i_Product, true);
            this.HidePopup();
          }
          else
          {
            this.SearchReqAsoList(i_IsAssociatedAAP, this.txtNumberDocument.Text.Trim(), this.txtAssociated.Text, systemUser.i_SystemUserId, systemUser.i_RoleConfigId, "0", int32_2, i_Product, true);
            this.DisableControls();
            this.HidePopup();
          }
        }
        else
        {
          this.SearchReqAsoList(i_IsAssociatedAAP, this.txtNumberDocument.Text.Trim(), this.txtAssociated.Text, systemUser.i_SystemUserId, systemUser.i_RoleConfigId, "0", int32_2, i_Product, true);
          this.DisableControls();
          this.HidePopup();
        }
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SearchReqAsoList(
      int i_IsAssociatedAAP,
      string v_DocumentNumber,
      string v_ReasonSocial,
      int i_SystemUserId,
      int i_RoleConfigId,
      string v_ExtendedAction,
      int i_PlateTypeId,
      int i_Product,
      bool pboolLoadPager)
    {
      try
      {
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerReqAso.CurrentPageNumber;
        int pintMaxRows = this.custPagerReqAso.CurrentPageSize == 0 ? 10 : this.custPagerReqAso.CurrentPageSize;
        int pintTotalRows;
        DataTable all = new AssociatedQueriesBL().AssociatedGetAll(i_IsAssociatedAAP, v_DocumentNumber, v_ReasonSocial, i_SystemUserId, i_RoleConfigId, v_ExtendedAction, i_PlateTypeId, i_Product, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        if (all == null || all.Rows.Count == 0)
        {
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
          this.HidePopup();
          if (v_ExtendedAction == "0")
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "Advertencia </br> Primero debe generar una solicitud para visualizar su usuario."));
        }
        else
          this.lblMessage.Visible = false;
        int num = pintTotalRows;
        this.wdgListAssociated.DataSource = (object) all;
        this.wdgListAssociated.DataBind();
        this.custPagerReqAso.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
        this.custPagerReqAso.TotalRecordCount = pintTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerReqAso.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void DisableControls()
    {
      this.txtAssociated.Enabled = false;
      this.txtNumberDocument.Enabled = false;
      this.wibSearch.Enabled = false;
      this.wibNew.Enabled = false;
    }

    private void ExportList()
    {
      try
      {
        this.pobjAssociatedQueriesBL = new AssociatedQueriesBL();
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RequirementAssociated.aspx");
        DataTable dataTable = new DataTable();
        if (this.Session["ApplicationId"] != null)
        {
          int int32_1 = Convert.ToInt32(this.Session["ApplicationId"], (IFormatProvider) CultureInfo.CurrentCulture);
          int i_IsAssociatedAAP = systemUser.i_RoleConfigId == 45 ? Convert.ToInt32(this.wddTypeAssociated.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) : (systemUser.i_RoleConfigId == 51 ? Convert.ToInt32(this.DropDownList1.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) : 0);
          int int32_2 = Convert.ToInt32(this.ViewState["t"]);
          int i_Product = this.SetProductId(int32_2);
          string userExtendedAction = this.pobjAssociatedQueriesBL.GetSystemUserExtendedAction(systemUser.i_SystemUserId, int32_1);
          if (userExtendedAction != "")
          {
            string v_ExtendedAction = Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("7", StringComparison.CurrentCulture)));
            if (v_ExtendedAction == "7")
              dataTable = new AssociatedQueriesBL().AssociatedGetAll(i_IsAssociatedAAP, this.txtNumberDocument.Text.Trim(), this.txtAssociated.Text, systemUser.i_SystemUserId, systemUser.i_RoleConfigId, v_ExtendedAction, int32_2, i_Product, 0, 0, out int _);
            else
              dataTable = new AssociatedQueriesBL().AssociatedGetAll(i_IsAssociatedAAP, this.txtNumberDocument.Text.Trim(), this.txtAssociated.Text, systemUser.i_SystemUserId, systemUser.i_RoleConfigId, "0", int32_2, i_Product, 0, 0, out int _);
          }
          else
            dataTable = new AssociatedQueriesBL().AssociatedGetAll(i_IsAssociatedAAP, this.txtNumberDocument.Text.Trim(), this.txtAssociated.Text, systemUser.i_SystemUserId, systemUser.i_RoleConfigId, "0", int32_2, i_Product, 0, 0, out int _);
        }
        this.Session["dtExport"] = (object) dataTable;
        this.Export();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void Export()
    {
      string script = "Export();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    public void ListAssociatedChild(int i_AssociatedId)
    {
      try
      {
        int int32 = Convert.ToInt32(this.ViewState["t"]);
        DataTable all = new AssociatedQueriesBL().AssociatedChildGetAll(i_AssociatedId, int32);
        if (all == null || all.Rows.Count == 0)
        {
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
          this.lblCountChild.Text = Constants.SEARCHRESULT_Empty;
        }
        this.wdgListAssociatedChild.AutoGenerateColumns = false;
        this.wdgListAssociatedChild.DataSource = (object) all;
        this.wdgListAssociatedChild.DataBind();
        this.lblCountChild.Text = Constants.SEARCHRESULT_OK.Replace("XX", all.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture));
      }
      catch (Exception ex)
      {
        throw ex;
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

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
