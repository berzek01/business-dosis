// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.WarehouseControlListDetail
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;

#nullable disable
namespace SIIV.WebApp.Warehouse
{
  public class WarehouseControlListDetail : Page
  {
    private List<int> listRequest = new List<int>();
    private DataTable dtListRequestPlatesDetail = new DataTable();
    private string vTicket = "";
    private string scheck = "";
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected RadioButtonList rdblControlType;
    protected HtmlTableRow Gestor;
    protected DropDownList wddGestor;
    protected HtmlTableRow Usuario;
    protected DropDownList wddCounter;
    protected HtmlTableRow ticket;
    protected TextBox txtTicket;
    protected FilteredTextBoxExtender txtTicket_FilteredTextBoxExtender;
    protected Button wibSearch;
    protected HtmlTableRow Counter;
    protected TextBox txtCounter;
    protected HtmlTableRow ListPlates;
    protected GridView wdgRequestPlateList;
    protected HtmlTableRow Tr1;
    protected HtmlTableCell Items;
    protected Panel pnVerificador;
    protected TextBox TxtVerificador;
    protected FilteredTextBoxExtender TxtVerificador_FilteredTextBoxExtender;
    protected Button btnVerificar;
    protected Label lblLeyendaInferior;
    protected Label lblLeyendaInferiorCantidad;
    protected Button wibSave;
    protected Button wibCancel;
    protected Label lblConciliation;
    protected Label lblMsg;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMsg.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.Session["deleteRowGv"] = (object) false;
        this.vTicket = this.Request.QueryString["sTicket"].ToString();
        this.scheck = this.Request.QueryString["scheck"].ToString();
        this.rdblControlType.SelectedValue = this.scheck;
        if (Convert.ToInt32(this.rdblControlType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) == 1)
        {
          this.LoadGestor();
          this.LoadCounter();
          this.Gestor.Visible = true;
          this.Usuario.Visible = true;
          this.ticket.Visible = false;
          this.Counter.Visible = false;
          this.ListPlates.Visible = false;
          this.Items.Visible = false;
        }
        else
        {
          this.txtTicket.Text = this.vTicket;
          this.txtCounter.Text = "";
          this.txtTicket.Focus();
          DataTable dtResult = new DataTable("Datos");
          this.TableColumns(dtResult);
          this.wdgRequestPlateList.DataSource = (object) dtResult;
          this.wdgRequestPlateList.DataBind();
          this.Gestor.Visible = false;
          this.Usuario.Visible = false;
          this.ticket.Visible = true;
          this.Counter.Visible = true;
          this.ListPlates.Visible = true;
          this.Items.Visible = true;
          if (this.vTicket != "")
            this.SearchTicket();
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }

    protected void rdblControlType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (Convert.ToInt32(this.rdblControlType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) == 1)
      {
        this.LoadGestor();
        this.LoadCounter();
        this.wddGestor.SelectedValue = "-1";
        this.Gestor.Visible = true;
        this.Usuario.Visible = true;
        this.ticket.Visible = false;
        this.Counter.Visible = false;
        this.ListPlates.Visible = false;
        this.Items.Visible = false;
      }
      else
      {
        this.txtTicket.Text = "";
        this.txtCounter.Text = "";
        this.txtTicket.Focus();
        DataTable dtResult = new DataTable("Datos");
        this.TableColumns(dtResult);
        this.wdgRequestPlateList.DataSource = (object) dtResult;
        this.wdgRequestPlateList.DataBind();
        this.Gestor.Visible = false;
        this.Usuario.Visible = false;
        this.ticket.Visible = true;
        this.Counter.Visible = true;
        this.ListPlates.Visible = true;
        this.Items.Visible = true;
        this.TxtVerificador.Text = "";
        this.lblLeyendaInferior.Text = "0";
        this.lblLeyendaInferiorCantidad.Text = "";
      }
    }

    private void TableColumns(DataTable dtResult)
    {
      dtResult.Columns.Add("i_RequirementPlateId", typeof (string));
      dtResult.Columns.Add("v_PlateNew", typeof (string));
      dtResult.Columns.Add("v_Position", typeof (string));
      dtResult.Columns.Add("d_Fecha", typeof (DateTime));
      dtResult.Columns.Add("v_Status", typeof (string));
      dtResult.Columns.Add("i_UserCounterId", typeof (string));
      dtResult.Columns.Add("v_NameCounter", typeof (string));
      dtResult.Columns.Add("i_ReserveStockId", typeof (string));
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      try
      {
        if (Convert.ToInt32(this.rdblControlType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) == 1)
        {
          if (this.wddGestor.SelectedValue == "-1")
            throw new HandledException(1, "Seleccione Gestor");
          if (this.wddCounter.SelectedValue == "-1")
            throw new HandledException(1, "Seleccione Counter");
          this.SaveRegister();
        }
        else if ((bool) this.Session["deleteRowGv"] && this.wdgRequestPlateList.Rows.Count == 0)
          this.SaveRequestPlatesDetails();
        else if (this.wdgRequestPlateList.Rows.Count != 0)
          this.SaveRequestPlatesDetails();
        else
          Message.SetMessage(this.lblMsg, new HandledException(1, "No se encontraron registros a Grabar"));
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }

    protected void wibCancel_Click(object sender, EventArgs e)
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private void LoadGestor()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlListDetail.aspx");
        int iLocationId = systemUser.i_LocationId;
        int num = systemUser.i_CompanyId.Value;
        this.wddGestor.Items.Clear();
        DataTable warehouseControl = new WarehouseControlQueriesBL().GetSystemGestorWarehouseControl(iLocationId);
        if (warehouseControl != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) warehouseControl.Rows)
            this.wddGestor.Items.Add(new ListItem(row["v_Alias"].ToString(), row["i_SystemUserId"].ToString()));
        }
        this.wddGestor.Items.Insert(0, new ListItem("- Todos - ", "-1"));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadCounter()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlListDetail.aspx");
        int iLocationId = systemUser.i_LocationId;
        int num = systemUser.i_CompanyId.Value;
        this.wddCounter.Items.Clear();
        DataTable warehouseControl = new WarehouseControlQueriesBL().GetSystemUserWarehouseControl(iLocationId);
        if (warehouseControl != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) warehouseControl.Rows)
            this.wddCounter.Items.Add(new ListItem(row["v_Alias"].ToString(), row["i_SystemUserId"].ToString()));
        }
        this.wddCounter.Items.Insert(0, new ListItem("- Todos - ", "-1"));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SaveRegister()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlListDetail.aspx");
        int num1 = 0;
        int num2 = 0;
        if (systemUser != null)
          num1 = systemUser.i_SystemUserId;
        int int32_1 = Convert.ToInt32(this.rdblControlType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        string text = this.wddGestor.SelectedItem.Text;
        int int32_2 = Convert.ToInt32(this.wddCounter.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        int num3 = 0;
        num2 = new WarehouseControlManagementBL().WarehouseControlInsert(new ArrayList()
        {
          (object) int32_2,
          (object) num1,
          (object) text,
          (object) int32_1,
          (object) num3
        });
        string script = "SendInfoPopup();";
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchTicket();

    private void SearchTicket()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlListDetail.aspx");
        int num = 0;
        DataTable dataTable = new DataTable();
        this.lblConciliation.Visible = false;
        this.lblConciliation.Text = "";
        if (systemUser != null)
          num = systemUser.i_SystemUserId;
        if (this.txtTicket.Text.Length == 8)
        {
          dataTable = new WarehouseControlManagementBL().WarehouseGetPlatesRequest(Convert.ToInt32(this.txtTicket.Text.Trim()));
          if (dataTable.Rows.Count != 0)
          {
            this.ViewState["dtListRequestPlatesDetail"] = (object) dataTable;
            this.wdgRequestPlateList.DataSource = (object) (DataTable) this.ViewState["dtListRequestPlatesDetail"];
            this.wdgRequestPlateList.DataBind();
            this.txtCounter.Text = dataTable.Rows[0]["v_NameCounter"].ToString();
            this.TxtVerificador.Attributes.Add("onfocusin", " select();");
            this.TxtVerificador.Focus();
          }
          else
          {
            this.txtTicket.Text = "";
            this.txtCounter.Text = "";
            this.txtTicket.Focus();
            DataTable dtResult = new DataTable("Datos");
            this.TableColumns(dtResult);
            this.wdgRequestPlateList.DataSource = (object) dtResult;
            this.wdgRequestPlateList.DataBind();
            Message.SetMessage(this.lblMsg, new HandledException(1, "No se encontro informacion del ticket ingresado, verificar"));
          }
        }
        else
        {
          this.txtTicket.Text = "";
          this.txtCounter.Text = "";
          this.txtTicket.Focus();
          DataTable dtResult = new DataTable("Datos");
          this.TableColumns(dtResult);
          this.wdgRequestPlateList.DataSource = (object) dtResult;
          this.wdgRequestPlateList.DataBind();
          Message.SetMessage(this.lblMsg, new HandledException(1, " Debe ingresar un número de Ticket para la busqueda, Verificar."));
        }
        this.lblLeyendaInferiorCantidad.Text = dataTable.Rows.Count.ToString();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }

    protected void wdgRequestPlateList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        if (!e.CommandName.Equals("Delete", StringComparison.CurrentCulture))
          return;
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - PlateDeliveryMassive.aspx");
        RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
        int int32_1 = Convert.ToInt32(this.Session["ApplicationId"], (IFormatProvider) CultureInfo.CurrentCulture);
        string userExtendedAction = requirementQueriesBl.GetSystemUserExtendedAction(systemUser.i_SystemUserId, int32_1);
        string str1 = "";
        if (userExtendedAction != "")
          str1 = Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("8", StringComparison.CurrentCulture)));
        if (str1 == "8")
        {
          int int32_2 = Convert.ToInt32(e.CommandArgument);
          string i_RequirementPlateId = (this.wdgRequestPlateList.Rows[int32_2] ?? throw new HandledException(4, "Error de selección.", "'wdgRequestPlateList' - WarehouseControlListDetail.aspx")).Cells[0].Text;
          this.Session["i_ReserveStockId"] = (object) this.wdgRequestPlateList.DataKeys[int32_2]["i_ReserveStockId"].ToString();
          this.dtListRequestPlatesDetail = (DataTable) this.ViewState["dtListRequestPlatesDetail"];
          this.dtListRequestPlatesDetail.Rows.Remove(this.dtListRequestPlatesDetail.Rows.Cast<DataRow>().ToList<DataRow>().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (item => item["i_RequirementPlateId"].ToString() == i_RequirementPlateId)));
          List<string> list1 = this.wdgRequestPlateList.Rows.Cast<GridViewRow>().Where<GridViewRow>((System.Func<GridViewRow, bool>) (item => item.CssClass == "Test")).Select<GridViewRow, string>((System.Func<GridViewRow, string>) (item => item.Controls.Cast<DataControlFieldCell>().FirstOrDefault<DataControlFieldCell>().Text)).ToList<string>();
          List<string> list2 = this.wdgRequestPlateList.Rows.Cast<GridViewRow>().Where<GridViewRow>((System.Func<GridViewRow, bool>) (item => item.CssClass == "Test1")).Select<GridViewRow, string>((System.Func<GridViewRow, string>) (item => item.Controls.Cast<DataControlFieldCell>().FirstOrDefault<DataControlFieldCell>().Text)).ToList<string>();
          this.wdgRequestPlateList.DataSource = (object) this.dtListRequestPlatesDetail;
          this.wdgRequestPlateList.DataBind();
          foreach (string str2 in list1)
          {
            string row = str2;
            GridViewRow gridViewRow = this.wdgRequestPlateList.Rows.Cast<GridViewRow>().FirstOrDefault<GridViewRow>((System.Func<GridViewRow, bool>) (item => item.Controls.Cast<DataControlFieldCell>().ToList<DataControlFieldCell>().FirstOrDefault<DataControlFieldCell>().Text == row.Trim()));
            if (gridViewRow != null)
              gridViewRow.CssClass = "Test";
          }
          foreach (string str3 in list2)
          {
            string row = str3;
            GridViewRow gridViewRow = this.wdgRequestPlateList.Rows.Cast<GridViewRow>().FirstOrDefault<GridViewRow>((System.Func<GridViewRow, bool>) (item => item.Controls.Cast<DataControlFieldCell>().ToList<DataControlFieldCell>().FirstOrDefault<DataControlFieldCell>().Text == row.Trim()));
            if (gridViewRow != null)
              gridViewRow.CssClass = "Test1";
          }
          this.ViewState["dtListRequestPlatesDetail"] = (object) this.dtListRequestPlatesDetail;
          this.lblLeyendaInferior.Text = this.wdgRequestPlateList.Rows.Cast<GridViewRow>().Count<GridViewRow>((System.Func<GridViewRow, bool>) (item => item.CssClass == "Test")).ToString();
          this.lblLeyendaInferiorCantidad.Text = this.wdgRequestPlateList.Rows.Count.ToString();
          this.Session["deleteRowGv"] = (object) true;
        }
        else
          Message.SetMessage(this.lblMsg, new HandledException(1, "El Usuario no tiene permisos para eliminar el registro, Verificar"));
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }

    private void SaveRequestPlatesDetails()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlListDetail.aspx");
        int num1 = 0;
        int num2 = 0;
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = (DataTable) this.ViewState["dtListRequestPlatesDetail"];
        if (systemUser != null)
          num1 = systemUser.i_SystemUserId;
        if (dataTable2 == null)
          throw new HandledException(1, "Debe Ingresar el Número de Ticket.");
        if (Convert.ToInt32(this.lblLeyendaInferior.Text.Trim()) != Convert.ToInt32(this.lblLeyendaInferiorCantidad.Text.Trim()))
          throw new HandledException(1, "Las cantidad de solicitudes verificadas debe ser igual a la cantidad Total.");
        int int32_1 = Convert.ToInt32(this.rdblControlType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        string str = "- Todos -";
        int int32_2 = !(bool) this.Session["deleteRowGv"] || this.wdgRequestPlateList.Rows.Count != 0 ? Convert.ToInt32(dataTable2.Rows[0]["i_UserCounterId"]) : 0;
        int content1 = !(bool) this.Session["deleteRowGv"] || this.wdgRequestPlateList.Rows.Count != 0 ? Convert.ToInt32(dataTable2.Rows[0]["i_ReserveStockId"]) : Convert.ToInt32(this.Session["i_ReserveStockId"]);
        WarehouseControlManagementBL controlManagementBl = new WarehouseControlManagementBL();
        ArrayList arrFilter = new ArrayList()
        {
          (object) int32_2,
          (object) num1,
          (object) str,
          (object) int32_1,
          (object) content1
        };
        if ((bool) this.Session["deleteRowGv"] && this.wdgRequestPlateList.Rows.Count == 0)
        {
          int content2 = num2;
          string empty = string.Empty;
          new WarehouseManagementBL().WarehouseControlRequestPlatesInsert(new XElement((XName) "WareHouseControl", new object[4]
          {
            (object) new XElement((XName) "i_WareHouseControlId", (object) content2),
            (object) new XElement((XName) "i_UserId", (object) systemUser.i_SystemUserId),
            (object) new XElement((XName) "i_ReserveStockId", (object) content1),
            (object) new XElement((XName) "WareHouseControlDetails")
          }).ToString());
          Message.SetMessage(this.lblMsg, new HandledException(2, "Se grabó exitosamente"));
          this.wibSave.Enabled = false;
          string script = "SendInfoPopup();";
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        }
        else
        {
          int num3 = controlManagementBl.WarehouseControlInsert(arrFilter);
          if (num3 != 0)
          {
            int content3 = num3;
            string empty = string.Empty;
            XElement xelement = new XElement((XName) "WareHouseControl", new object[4]
            {
              (object) new XElement((XName) "i_WareHouseControlId", (object) content3),
              (object) new XElement((XName) "i_UserId", (object) systemUser.i_SystemUserId),
              (object) new XElement((XName) "i_ReserveStockId", (object) content1),
              (object) new XElement((XName) "WareHouseControlDetails")
            });
            foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
            {
              XElement content4 = new XElement((XName) "WareHouseControlDetail", new object[5]
              {
                (object) new XElement((XName) "i_RequirementPlateId", (object) Convert.ToInt32(row["i_RequirementPlateId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture)),
                (object) new XElement((XName) "i_DeliveryPlateTypeId", (object) Convert.ToInt32(int32_1)),
                (object) new XElement((XName) "i_StatusId", (object) 1),
                (object) new XElement((XName) "i_ReturnUserAuxId", (object) 0),
                (object) new XElement((XName) "v_Observation", (object) empty)
              });
              xelement.Element((XName) "WareHouseControlDetails").Add((object) content4);
            }
            new WarehouseManagementBL().WarehouseControlRequestPlatesInsert(xelement.ToString());
            Message.SetMessage(this.lblMsg, new HandledException(2, "Se grabó exitosamente"));
            this.wibSave.Enabled = false;
            string script = "SendInfoPopup();";
            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void wdgRequestPlateList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void btnVerificar_Click(object sender, EventArgs e) => this.Verificar();

    private void Verificar()
    {
      this.lblConciliation.Visible = false;
      this.lblConciliation.Text = "";
      string strSobreToVerify = string.Empty;
      string empty = string.Empty;
      try
      {
        DataTable dataTable = new DataTable();
        if (this.TxtVerificador.Text == "")
        {
          Message.SetMessage(this.lblMsg, new HandledException(1, "Debe ingresar un N° de Solicitud."));
        }
        else
        {
          if (this.TxtVerificador.Text.Trim().Length != 10 && this.TxtVerificador.Text.Trim() != "")
          {
            if (this.TxtVerificador.Text.Trim().Length >= 2 && this.TxtVerificador.Text.Trim().Length <= 8)
              strSobreToVerify = !(this.TxtVerificador.Text.Trim().Substring(0, 1) == "0") ? this.TxtVerificador.Text.Trim() : this.TxtVerificador.Text.Trim().Substring(3);
            else
              Message.SetMessage(this.lblMsg, new HandledException(1, "Dato ingresado es incorrecto."));
          }
          else
            strSobreToVerify = !(this.TxtVerificador.Text.Trim().Substring(2, 1) == "0") ? this.TxtVerificador.Text.Trim().Substring(2) : this.TxtVerificador.Text.Trim().Substring(3);
          if (this.wdgRequestPlateList.Rows.Count == 0)
            return;
          if (strSobreToVerify != "")
            this.listRequest.Add(Convert.ToInt32(strSobreToVerify));
          GridViewRow gridViewRow = this.wdgRequestPlateList.Rows.Cast<GridViewRow>().FirstOrDefault<GridViewRow>((System.Func<GridViewRow, bool>) (item => item.Controls.Cast<DataControlFieldCell>().ToList<DataControlFieldCell>().FirstOrDefault<DataControlFieldCell>().Text == strSobreToVerify));
          if (gridViewRow != null)
          {
            if (gridViewRow.CssClass == "Test" || gridViewRow.CssClass == "Test1")
            {
              Message.SetMessage(this.lblMsg, new HandledException(1, "La solicitud ya fue ingresada."));
            }
            else
            {
              DataTable requirementPlate = new WarehouseControlQueriesBL().GetWarehouseControlFindRequirementPlate(Convert.ToInt32(strSobreToVerify), "");
              int num;
              if (requirementPlate != null)
              {
                string ErrorMessage = requirementPlate.Rows[0]["v_ConciliacionStatus"].ToString();
                if (ErrorMessage != "")
                {
                  if (Convert.ToInt32(requirementPlate.Rows[0]["i_flagCantRequestPlate"]) == 1)
                  {
                    Message.SetMessage(this.lblConciliation, new HandledException(1, ErrorMessage));
                    gridViewRow.CssClass = "Test1";
                  }
                  else
                  {
                    gridViewRow.CssClass = "Test";
                    Message.SetMessage(this.lblConciliation, new HandledException(1, ErrorMessage));
                    Label lblLeyendaInferior = this.lblLeyendaInferior;
                    num = this.wdgRequestPlateList.Rows.Cast<GridViewRow>().Count<GridViewRow>((System.Func<GridViewRow, bool>) (item => item.CssClass == "Test"));
                    string str = num.ToString();
                    lblLeyendaInferior.Text = str;
                  }
                }
                else
                {
                  gridViewRow.CssClass = "Test";
                  Label lblLeyendaInferior = this.lblLeyendaInferior;
                  num = this.wdgRequestPlateList.Rows.Cast<GridViewRow>().Count<GridViewRow>((System.Func<GridViewRow, bool>) (item => item.CssClass == "Test"));
                  string str = num.ToString();
                  lblLeyendaInferior.Text = str;
                }
              }
              string text = this.lblLeyendaInferior.Text;
              num = this.wdgRequestPlateList.Rows.Count;
              string str1 = num.ToString();
              if (text == str1)
                this.wibSave.Attributes.Add("autofocus", "true");
            }
          }
          else
            Message.SetMessage(this.lblMsg, new HandledException(1, "La solicitud no existe en el N° de Ticket."));
        }
      }
      catch (HandledException ex)
      {
        GridViewRow gridViewRow = this.wdgRequestPlateList.Rows.Cast<GridViewRow>().FirstOrDefault<GridViewRow>((System.Func<GridViewRow, bool>) (item => item.Controls.Cast<DataControlFieldCell>().ToList<DataControlFieldCell>().FirstOrDefault<DataControlFieldCell>().Text == strSobreToVerify));
        Message.SetMessage(this.lblMsg, ex);
        gridViewRow.CssClass = "Test1";
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
      finally
      {
        this.TxtVerificador.Text = "";
        if (Convert.ToInt32(this.lblLeyendaInferior.Text.Trim()) != Convert.ToInt32(this.lblLeyendaInferiorCantidad.Text.Trim()))
          this.TxtVerificador.Focus();
        else
          this.wibSave.Focus();
      }
    }

    protected void wdgRequestPlateList_InitializeRow(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowIndex < 0)
          return;
        int num = 6;
        for (int index1 = 0; index1 < this.listRequest.Count; ++index1)
        {
          if (this.listRequest[index1] == Convert.ToInt32(e.Row.Cells[0].Text))
          {
            for (int index2 = 0; index2 < num; ++index2)
              e.Row.Cells[index2].CssClass = "Test";
          }
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }
  }
}
