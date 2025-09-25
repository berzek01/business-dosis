// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Delivery.Query.BookQueryPlates
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Delivery.Query
{
  public class BookQueryPlates : Page
  {
    private RequirementQueriesBL ObjRequirementQueriesBL;
    private DataTable DtDetalleDelivery = new DataTable();
    private double VariableIgv = Convert.ToDouble(ConfigurationManager.AppSettings["Igv"]);
    private double ComisionCanalAtencion = Convert.ToDouble(ConfigurationManager.AppSettings["ComisionCanalAtencionRetail"]);
    private DataTable dtListOrder;
    private DataTable DtRequirements;
    private RequirementContributor objRequirementContributor;
    private RequirementContributor objContributorRequester;
    private RequirementManagementBL oRequirementManagement;
    private RequirementProgramation objRequirementProgramation;
    private RequirementPlate objRequirementPlate;
    private Payment objPayment;
    private SIIV.BE.Requirement objRequirement;
    protected UpdatePanel UpdatePanel1;
    protected HiddenField TabIndexNew;
    protected DropDownList wddLocationList;
    protected CheckBox chkGenerationBetween;
    protected Fecha wdpStartDate;
    protected Fecha wdpEndDate;
    protected DropDownList wddTramiteList;
    protected TextBox txtPlateNew;
    protected FilteredTextBoxExtender txtPlateNew_FilteredTextBoxExtender;
    protected TextBox txtClienteName;
    protected Button wibSearch;
    protected GridView wdgList;
    protected Pager custPagerClaimList;
    protected Label lblMessageSearch;
    protected UpdatePanel UpdatePanel2;
    protected Label Label5;
    protected TextBox txtPlate;
    protected Label Label3;
    protected TextBox TxtTitulo;
    protected Label Label4;
    protected TextBox txtPropietario;
    protected Label Label6;
    protected TextBox TxtTelefono;
    protected FilteredTextBoxExtender TxtTelefono_FilteredTextBoxExtender;
    protected Button wibNew;
    protected Button wibSaveTelefono;
    protected Button wibCancelTelefono;
    protected Label Label7;
    protected TextBox TxtMarca;
    protected Label Label8;
    protected TextBox TxtModelo;
    protected Label Label9;
    protected TextBox TxtCategoria;
    protected Label Label10;
    protected TextBox TxtTipoUso;
    protected GridView wdgHistoryCall;
    protected Label Label11;
    protected RadioButtonList rblWantPlateDelivery;
    protected HtmlTableRow trMotive;
    protected DropDownList wddMotive;
    protected HtmlTableRow trObservacion;
    protected Label lblObservación;
    protected TextBox TxtObservacion;
    protected Button wibAceptar;
    protected Button wibCancelar;
    protected Button btnReturnPopupConfirmation;
    protected Label lblMsgError;
    protected UpdatePanel UpdatePanel3;
    protected HtmlGenericControl DivDelivery;
    protected RadioButtonList rdbTypeDelivery;
    protected GridView wdgDetailDelivery;
    protected HtmlTableRow TrTotal;
    protected TextBox txtTotal;
    protected Button BtnActualizar;
    protected HtmlTableCell tdTitle;
    protected HtmlGenericControl DivDatosDelivery;
    protected DropDownList wddDeliveryDescription;
    protected TextBox txtDeliveryDescription;
    protected DropDownList wddDeliveryDescription2;
    protected TextBox txtDeliveryDescription2;
    protected DropDownList wddDistrict;
    protected TextBox txtReference;
    protected TextBox txtMailDelivery;
    protected FilteredTextBoxExtender FilteredTextBoxExtender2;
    protected RegularExpressionValidator RegularExpressionValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender7;
    protected TextBox txtTelephoneDelivery;
    protected TextBox txtTelephoneMovilDelivery;
    protected MaskedEditExtender MaskedEditExtender3;
    protected HtmlTableCell tdBoton;
    protected Button SaveDelivery;
    protected Button Regresar;
    protected Label LblMessage;
    protected UpdatePanel UpdatePanel4;
    protected HtmlTableRow EnunciadoDelivery;
    protected GridView gvSchedule;
    protected Button btnJavaScriptResponse;
    protected Button btnOpenSucesfull;
    protected Label lblMessageCalendar;

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
        string script = UtilDA.ActiveTabIndexByDelivery("tabs", 3, "0,1,2");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = (DataTable) this.ViewState["dt_Result"];
        this.gvSchedule.Visible = true;
        this.gvSchedule.DataSource = (object) dataTable2;
        this.gvSchedule.DataBind();
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
      if (Convert.ToInt32(this.Session["FromUniversalQuery"]) == 1)
      {
        if (systemUser.i_RoleConfigId == 30)
        {
          string script = UtilDA.ActiveTabIndexByDeliveryUserPublic("tabs", 2, "0,1,3");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        }
        else
        {
          string script = UtilDA.ActiveTabIndexByDelivery("tabs", 1, "0,2,3");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        }
        this.txtPlateNew.Text = this.Request.QueryString["v_PlateNew"].ToString();
        this.SetDatePicker();
        this.chkGenerationBetween.Checked = false;
        this.wdpStartDate.Enabled = false;
        this.wdpEndDate.Enabled = false;
        this.LoadParameters();
        this.InitialLoad(true);
        this.MoveNext(systemUser.i_RoleConfigId);
        this.Session["FromUniversalQuery"] = (object) 0;
      }
      else
      {
        if (systemUser.i_RoleConfigId == 30)
        {
          string script = UtilDA.OcultarTab("tabs", 1);
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        }
        this.SetDatePicker();
        this.LoadParameters();
        this.InitialLoad(true);
      }
    }

    public void MoveNext(int i_rolconfig)
    {
      if (this.wdgList.Rows.Count <= 0)
        return;
      this.Session.Remove("i_RequirementCallId");
      int int32_1 = Convert.ToInt32(this.wdgList.DataKeys[0]["i_RequirementPlateId"].ToString());
      int int32_2 = Convert.ToInt32(this.wdgList.DataKeys[0]["i_DeliveryPointId"].ToString());
      string str = this.wdgList.DataKeys[0]["v_PlateNew"].ToString();
      this.Session["PuntoEntrega"] = (object) int32_2;
      this.Session["i_RequirementPlateId"] = (object) int32_1;
      this.Session["v_PlateNew"] = (object) str;
      DataTable manufacturedByPlate = new RequirementQueriesBL().GetDataManufacturedByPlate(int32_1);
      if (manufacturedByPlate.Rows.Count > 0)
      {
        DataRow row = manufacturedByPlate.Rows[0];
        this.txtPlate.Text = row["v_PlateNew"].ToString();
        this.TxtTitulo.Text = row["v_TitleNumber"].ToString();
        this.txtPropietario.Text = row["v_CompleteNameOwner"].ToString();
        this.TxtMarca.Text = row["v_Brand"].ToString();
        this.TxtModelo.Text = row["v_Model"].ToString();
        this.TxtCategoria.Text = row["v_Category"].ToString();
        this.TxtTipoUso.Text = row["v_UseType"].ToString();
        this.TxtTelefono.Text = row["v_PhoneNumber"].ToString();
        this.Session["i_VehicleClasification"] = (object) row["i_VehicleClasification"].ToString();
        if (Convert.ToInt32(row["i_RequirementCallId"]) != 0)
        {
          this.wdgHistoryCall.AutoGenerateColumns = false;
          this.wdgHistoryCall.DataSource = (object) manufacturedByPlate;
          this.wdgHistoryCall.DataBind();
        }
        this.getDistrict(int32_2, Convert.ToInt32(this.Session["i_VehicleClasification"]));
        if (i_rolconfig != 30)
        {
          string script = UtilDA.ActiveTabIndexByDelivery("tabs", 1, "0,2,3");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        }
        else
        {
          this.LoadRbtnTypeDelivery();
          this.LoadProductsDelivery();
        }
      }
    }

    public void LoadParameters()
    {
      try
      {
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) (SystemParameterGroups.ProcessType.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ", " + SystemParameterGroups.GroupMotiveNegative.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
          (object) "",
          (object) "1",
          (object) "1"
        });
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["i_GroupId"].ToString() == SystemParameterGroups.ProcessType.ToString())
              this.wddTramiteList.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
            if (row["i_GroupId"].ToString() == SystemParameterGroups.GroupMotiveNegative.ToString() && Convert.ToInt32(row["i_ParameterId"]) != 1)
              this.wddMotive.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          }
        }
        this.wddTramiteList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Todos --", "0"));
        this.wddTramiteList.SelectedValue = "-1";
        this.wddMotive.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccionar --", "0"));
        this.wddMotive.SelectedValue = "-1";
        DataTable locationDelivery = new LocationQueriesBL().GetLocationDelivery();
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) locationDelivery.Rows)
            this.wddLocationList.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_LocationId"].ToString()));
        }
        this.wddLocationList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Todos --", "0"));
        this.wddLocationList.SelectedValue = "-1";
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.LblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.LblMessage, new HandledException(-100, ex));
      }
    }

    public void LoadRbtnTypeDelivery()
    {
      try
      {
        this.rdbTypeDelivery.ClearSelection();
        this.rdbTypeDelivery.Items.Clear();
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.GroupTypeDelivery.ToString((IFormatProvider) CultureInfo.CurrentCulture),
          (object) "",
          (object) "1",
          (object) "1"
        });
        string str = this.Session["i_VehicleClasification"].ToString();
        if (dataTable == null)
          return;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          string[] strArray = row["v_Value"].ToString().Split('.');
          if (strArray[1] == str)
          {
            this.rdbTypeDelivery.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["v_ReferenceId"].ToString()));
            this.Session["i_ProductDelivery"] = (object) strArray[0];
          }
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.LblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.LblMessage, new HandledException(-100, ex));
      }
    }

    private void InitialLoad(bool pboolLoadPager)
    {
      try
      {
        this.lblMessageSearch.Visible = false;
        this.lblMessageSearch.Text = "";
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlListDetail.aspx");
        int i_SystemUserId = 0;
        if (systemUser != null)
          i_SystemUserId = systemUser.i_SystemUserId;
        this.ObjRequirementQueriesBL = new RequirementQueriesBL();
        if (this.wdpEndDate.Value.Subtract(this.wdpStartDate.Value).Days > 30 && this.txtPlateNew.Text.Trim() == "" && this.txtClienteName.Text.Trim() == "")
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_UNIVERSAL_QUERY_ERROR_DATE);
        if (!this.wdpStartDate.Enabled && !this.wdpEndDate.Enabled && this.txtPlateNew.Text.Trim() == "" && this.txtClienteName.Text.Trim() == "")
          throw new HandledException(1, "Debe especificar el número de placa o cliente");
        int pintStartdate;
        int pintFinishdate;
        if (!this.wdpStartDate.Enabled && !this.wdpStartDate.Enabled)
        {
          pintStartdate = 0;
          pintFinishdate = 0;
        }
        else
        {
          DateTime dateTime = this.wdpStartDate.Value;
          pintStartdate = Convert.ToInt32(dateTime.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture));
          dateTime = this.wdpEndDate.Value;
          pintFinishdate = Convert.ToInt32(dateTime.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        }
        string v_platenumber = this.txtPlateNew.Text.Trim() == "" ? "" : this.txtPlateNew.Text.Replace("-", "");
        string text = this.txtClienteName.Text.Trim() == "" ? "" : this.txtClienteName.Text;
        int int32_1 = Convert.ToInt32(this.wddLocationList.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        int int32_2 = Convert.ToInt32(this.wddTramiteList.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerClaimList.CurrentPageNumber;
        int pintMaxRows = this.custPagerClaimList.CurrentPageSize == 0 ? 10 : this.custPagerClaimList.CurrentPageSize;
        int pintTotalRows;
        DataTable manufacturedPlates = this.ObjRequirementQueriesBL.GetManufacturedPlates(i_SystemUserId, int32_1, int32_2, pintStartdate, pintFinishdate, v_platenumber, text, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        int num = pintTotalRows;
        this.wdgList.AutoGenerateColumns = false;
        this.wdgList.DataSource = (object) manufacturedPlates;
        this.wdgList.DataBind();
        if (systemUser.i_RoleConfigId == 30)
        {
          this.wdgList.Columns[6].Visible = false;
          this.wdgList.Columns[7].Visible = false;
        }
        else
        {
          this.wdgList.Columns[6].Visible = true;
          this.wdgList.Columns[7].Visible = true;
        }
        this.custPagerClaimList.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
        this.custPagerClaimList.TotalRecordCount = pintTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerClaimList.LoadPager();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessageSearch, ex);
        this.HidePopup();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageSearch, new HandledException(-100, ex));
        this.HidePopup();
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SetDatePicker()
    {
      try
      {
        this.wdpStartDate.Value = DateTime.Now.AddDays(-30.0);
        this.wdpEndDate.Value = DateTime.Now;
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.LblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.LblMessage, new HandledException(-100, ex));
      }
    }

    protected void wddClaimTypeSearch_SelectionChanged(object sender, EventArgs e)
    {
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.InitialLoad(true);

    protected void chkQuitFilter_CheckedChanged(object sender, EventArgs e)
    {
    }

    protected void wdgList_InitializeRow(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowIndex < 0)
        return;
      SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlListDetail.aspx");
      ImageButton control = e.Row.Cells[0].Controls[0] as ImageButton;
      DataRow row = ((DataRowView) e.Row.DataItem).Row;
      if (Convert.ToInt32(systemUser.i_RoleConfigId) == 30)
        control.ImageUrl = "~/Images/Design/Buttons/Actions/shop-cart-apply.png";
      else
        control.ImageUrl = "~/Images/Design/Buttons/Actions/phone.png";
    }

    protected void wdgList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlListDetail.aspx");
      if (!e.CommandName.Equals("DetailCall", StringComparison.CurrentCulture) || this.wdgList.Rows.Count <= 0)
        return;
      this.Session.Remove("i_RequirementCallId");
      int int32_1 = Convert.ToInt32(e.CommandArgument);
      GridViewRow row1 = this.wdgList.Rows[int32_1];
      int int32_2 = Convert.ToInt32(this.wdgList.DataKeys[int32_1]["i_RequirementPlateId"].ToString());
      int int32_3 = Convert.ToInt32(this.wdgList.DataKeys[int32_1]["i_DeliveryPointId"].ToString());
      int int32_4 = Convert.ToInt32(this.wdgList.DataKeys[int32_1]["i_InsertUserId"].ToString());
      string str = this.wdgList.DataKeys[int32_1]["v_PlateNew"].ToString();
      this.Session["PuntoEntrega"] = (object) int32_3;
      this.Session["i_RequirementPlateId"] = (object) int32_2;
      this.Session["v_PlateNew"] = (object) str;
      this.Session["i_SystemUserId"] = (object) int32_4;
      DataTable manufacturedByPlate = new RequirementQueriesBL().GetDataManufacturedByPlate(int32_2);
      if (manufacturedByPlate.Rows.Count > 0)
      {
        DataRow row2 = manufacturedByPlate.Rows[0];
        this.txtPlate.Text = row2["v_PlateNew"].ToString();
        this.TxtTitulo.Text = row2["v_TitleNumber"].ToString();
        this.txtPropietario.Text = row2["v_CompleteNameOwner"].ToString();
        this.TxtMarca.Text = row2["v_Brand"].ToString();
        this.TxtModelo.Text = row2["v_Model"].ToString();
        this.TxtCategoria.Text = row2["v_Category"].ToString();
        this.TxtTipoUso.Text = row2["v_UseType"].ToString();
        this.TxtTelefono.Text = row2["v_PhoneNumber"].ToString();
        this.Session["i_VehicleClasification"] = (object) row2["i_VehicleClasification"].ToString();
        if (Convert.ToInt32(row2["i_RequirementCallId"]) != 0)
        {
          this.wdgHistoryCall.AutoGenerateColumns = false;
          this.wdgHistoryCall.DataSource = (object) manufacturedByPlate;
          this.wdgHistoryCall.DataBind();
        }
        this.getDistrict(int32_3, Convert.ToInt32(this.Session["i_VehicleClasification"]));
        if (systemUser.i_RoleConfigId != 30)
        {
          this.lblMsgError.Text = "";
          this.lblMsgError.Visible = false;
          this.trMotive.Visible = false;
          this.trObservacion.Visible = false;
          this.wddMotive.Enabled = false;
          this.TxtObservacion.Enabled = false;
          this.wddMotive.SelectedValue = "0";
          this.TxtObservacion.Text = "";
          this.rblWantPlateDelivery.SelectedIndex = -1;
          string script = UtilDA.ActiveTabIndexByDelivery("tabs", 1, "0,2,3");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        }
        else
        {
          this.LoadRbtnTypeDelivery();
          this.LoadProductsDelivery();
          string script = UtilDA.ActiveTabIndexByDelivery("tabs", 2, "0,1,3");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        }
      }
    }

    public void getDistrict(int i_DeliveryPointId, int i_VehicleClasification)
    {
      try
      {
        DataTable dataTable = new DataTable();
        DataTable districtByDeliveryPoint = new RequirementQueriesBL().GetDistrictByDeliveryPoint(i_DeliveryPointId, i_VehicleClasification);
        if (districtByDeliveryPoint.Rows.Count <= 0)
          return;
        this.wddDistrict.DataSource = (object) districtByDeliveryPoint;
        this.wddDistrict.DataTextField = "v_Description";
        this.wddDistrict.DataValueField = "i_ParameterId";
        this.wddDistrict.DataBind();
        this.wddDistrict.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --", "0"));
        this.wddDistrict.SelectedIndex = 0;
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.LblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.LblMessage, new HandledException(-100, ex));
      }
    }

    protected void wddDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        string text = this.wddDistrict.SelectedItem.Text;
        int int32 = Convert.ToInt32(this.wddDistrict.SelectedValue.ToString());
        int iVehicleClassId = 1;
        if (Convert.ToInt32(this.Session["i_VehicleClasification"]) == 2)
          iVehicleClassId = 5;
        DataTable district = new RequirementQueriesBL().GetDistrict(int32, iVehicleClassId);
        if (district.Rows.Count <= 0)
          return;
        this.ViewState["varZona"] = (object) district.Rows[0]["v_ReferenceId"].ToString();
        this.ViewState["varDistrito"] = (object) district.Rows[0]["i_ParameterId"].ToString();
        this.ViewState["intProducIdDelivery"] = (object) district.Rows[0]["i_ServiceDelivery"].ToString();
        this.setChangeDeliveryZone(Convert.ToInt32(this.ViewState["intProducIdDelivery"]));
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.LblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.LblMessage, new HandledException(-100, ex));
      }
    }

    private void CleanTextDelivery()
    {
      this.wddDeliveryDescription.SelectedValue = "0";
      this.txtMailDelivery.Text = "";
      this.txtTelephoneDelivery.Text = "";
      this.txtTelephoneMovilDelivery.Text = "";
      this.txtDeliveryDescription.Text = "";
      this.wddDeliveryDescription2.SelectedValue = "0";
      this.txtDeliveryDescription2.Text = "";
      this.wddDistrict.SelectedValue = "0";
      this.txtReference.Text = "";
    }

    protected void custPagerClaimList_OnPageChanged(object sender, CustomPageChangeArgs e)
    {
      this.InitialLoad(false);
    }

    protected void wddLocationList_SelectionChanged(object sender, EventArgs e)
    {
    }

    protected void wibApprove_Click(object sender, EventArgs e)
    {
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

    protected void wibAceptar_Click(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlListDetail.aspx");
        int num = 0;
        if (systemUser != null)
          num = systemUser.i_SystemUserId;
        if (this.txtPlate.Text.Trim() == "")
          throw new HandledException(1, "Debe seleccionar si desea o no el Servicio de Delivery");
        if (this.rblWantPlateDelivery.SelectedIndex == -1)
          throw new HandledException(1, "Debe seleccionar si desea o no el Servicio de Delivery");
        if (Convert.ToInt32(this.rblWantPlateDelivery.SelectedValue) == 2 && this.wddMotive.SelectedIndex == 0)
          throw new HandledException(1, "Debe seleccionar un Motivo de la Negativa");
        if (Convert.ToInt32(this.rblWantPlateDelivery.SelectedValue) == 1)
        {
          string empty = string.Empty;
          this.CreatePopUpServer("SIIV - Delivery - Venta de Placas Fabricadas", "../../UserControls/PopupConfirmation.aspx?MessageTypeId=2&MessageText=¿Ud. Registrara una Solicitud Delivery, Desea Continuar?", "360px", "190px");
        }
        else
        {
          string empty = string.Empty;
          this.CreatePopUpServer("SIIV - Delivery - Venta de Placas Fabricadas", "../../UserControls/PopupConfirmation.aspx?MessageTypeId=2&MessageText=¿Ud. Registrara como Negativa la Atención, Desea Continuar?", "360px", "190px");
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgError, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgError, new HandledException(-100, ex));
      }
    }

    protected void wibCancelar_Click(object sender, EventArgs e)
    {
      this.Session["i_RequirementPlateId"] = (object) null;
      string script = UtilDA.ActiveTabIndexByDelivery("tabs", 0, "1,2,3");
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      this.lblMsgError.Visible = false;
      this.lblMsgError.Text = "";
      this.wdgHistoryCall.DataSource = (object) null;
      this.wdgHistoryCall.DataBind();
      this.UpdatePanel1.Update();
      this.TxtTelefono.Enabled = false;
      this.TxtTelefono.ReadOnly = true;
      this.wibSaveTelefono.Visible = false;
      this.wibNew.Visible = true;
      this.Session.Remove("ValueInitial");
      if (this.Session["i_RequirementCallId"] == null)
        return;
      this.SaveResultCall(-2);
    }

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      try
      {
        this.SaveResultCall(-3);
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgError, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgError, new HandledException(-100, ex));
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

    private void setChangeDeliveryZone(int iProductIdDelivery)
    {
      try
      {
        this.ClearCheckDetails();
        int productCorrespondence = new RequirementQueriesBL().GetProductCorrespondence(Convert.ToInt32(this.Session["i_ProductDelivery"]), iProductIdDelivery);
        string[] strArray = (this.rdbTypeDelivery.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture).Split('|')[0] + "|" + productCorrespondence.ToString()).Split('|');
        DataTable dataSource = (DataTable) this.wdgDetailDelivery.DataSource;
        dataSource.Columns["isCheked"].ReadOnly = false;
        foreach (string str in strArray)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataSource.Rows)
          {
            if (row["i_ProductId"].ToString() == str.ToString())
              row["isCheked"] = (object) 1;
          }
        }
        this.Session["DtDetalleDelivery"] = (object) dataSource.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (p => p.Field<int>("isCheked") == 1)).ToList<DataRow>().AsEnumerable<DataRow>().CopyToDataTable<DataRow>();
        DataTable dataTable = dataSource.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (p => p.Field<int>("isCheked") == 1)).ToList<DataRow>().AsEnumerable<DataRow>().CopyToDataTable<DataRow>();
        string str1 = string.Empty;
        double num1 = dataTable.AsEnumerable().Sum<DataRow>((System.Func<DataRow, double>) (row => row.Field<double>("f_PriceSale")));
        double num2 = dataTable.AsEnumerable().Sum<DataRow>((System.Func<DataRow, double>) (row => row.Field<double>("f_Subtotal")));
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          str1 = str1 + dataTable.Rows[index]["v_Description"].ToString() + " ,";
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          row["v_Description"] = (object) (row.Field<string>("v_Product") + " (" + str1.Substring(0, str1.Length - 2) + ") ");
          row["f_PriceSale"] = (object) num1;
          row["f_Subtotal"] = (object) num2;
        }
        this.wdgDetailDelivery.DataSource = (object) dataTable.AsEnumerable().ToList<DataRow>().Take<DataRow>(1).CopyToDataTable<DataRow>();
        this.wdgDetailDelivery.DataBind();
        double num3 = num2;
        this.TrTotal.Visible = true;
        this.txtTotal.Text = num3.ToString("0.00");
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.LblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.LblMessage, new HandledException(-100, ex));
      }
    }

    private void SaveResultCall(int i_isCompleteId)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlListDetail.aspx");
        int num1 = 0;
        int num2 = 0;
        if (systemUser != null)
          num1 = systemUser.i_SystemUserId;
        int int32_1 = this.Session["i_RequirementCallId"] == null ? 0 : Convert.ToInt32(this.Session["i_RequirementCallId"]);
        int int32_2 = Convert.ToInt32(this.Session["i_RequirementPlateId"]);
        int int32_3 = Convert.ToInt32(this.rblWantPlateDelivery.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture));
        if (int32_3 != 2)
        {
          switch (i_isCompleteId)
          {
            case -3:
              int num3 = Convert.ToInt32(this.wddMotive.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture)) == 0 ? 1 : Convert.ToInt32(this.wddMotive.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture));
              string text1 = this.TxtObservacion.Text;
              num2 = new RequirementQueriesBL().RequirementCallInsert(new ArrayList()
              {
                (object) int32_1,
                (object) int32_2,
                (object) num3,
                (object) text1,
                (object) int32_3,
                (object) num1,
                (object) i_isCompleteId
              });
              break;
            case -2:
              int num4 = Convert.ToInt32(this.wddMotive.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture)) == 0 ? 1 : Convert.ToInt32(this.wddMotive.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture));
              string text2 = this.TxtObservacion.Text;
              num2 = new RequirementQueriesBL().RequirementCallInsert(new ArrayList()
              {
                (object) int32_1,
                (object) int32_2,
                (object) num4,
                (object) text2,
                (object) int32_3,
                (object) num1,
                (object) i_isCompleteId
              });
              break;
            case 1:
              int num5 = Convert.ToInt32(this.wddMotive.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture)) == 0 ? 1 : Convert.ToInt32(this.wddMotive.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture));
              string text3 = this.TxtObservacion.Text;
              num2 = new RequirementQueriesBL().RequirementCallInsert(new ArrayList()
              {
                (object) int32_1,
                (object) int32_2,
                (object) num5,
                (object) text3,
                (object) int32_3,
                (object) num1,
                (object) i_isCompleteId
              });
              break;
          }
        }
        else
        {
          int num6 = Convert.ToInt32(this.wddMotive.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture)) == 0 ? 1 : Convert.ToInt32(this.wddMotive.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture));
          string text4 = this.TxtObservacion.Text;
          num2 = new RequirementQueriesBL().RequirementCallInsert(new ArrayList()
          {
            (object) int32_1,
            (object) int32_2,
            (object) num6,
            (object) text4,
            (object) int32_3,
            (object) num1,
            (object) i_isCompleteId
          });
        }
        this.Session["i_RequirementCallId"] = num2 != 0 ? (object) num2 : throw new HandledException(5, "No Se pudo grabar la Operacion Correctamente");
        Message.SetMessage(this.lblMsgError, new HandledException(2, "Se grabó la Operacion exitosamente"));
        DataTable manufacturedByPlate = new RequirementQueriesBL().GetDataManufacturedByPlate(int32_2);
        if (manufacturedByPlate.Rows.Count != 0)
        {
          this.wdgHistoryCall.AutoGenerateColumns = false;
          this.wdgHistoryCall.DataSource = (object) manufacturedByPlate;
          this.wdgHistoryCall.DataBind();
        }
        if (Convert.ToInt32(this.rblWantPlateDelivery.SelectedValue) != 1)
          return;
        this.LoadRbtnTypeDelivery();
        this.LoadProductsDelivery();
        this.DivDatosDelivery.Visible = false;
        this.tdBoton.Visible = false;
        this.tdTitle.Visible = false;
        string script = UtilDA.ActiveTabIndexByDelivery("tabs", 2, "0,1,3");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgError, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgError, new HandledException(-100, ex));
      }
      finally
      {
      }
    }

    public void LoadProductsDelivery()
    {
      try
      {
        this.Session["dt_AllProducts"] = (object) new RequirementQueriesBL().GetDetailbyProductDelivery(Convert.ToInt32(this.Session["i_ProductDelivery"]));
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.LblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.LblMessage, new HandledException(-100, ex));
      }
    }

    private void SetDeliveryEnabled(
      bool IsMailDelivery,
      bool IsTelephoneDelivery,
      bool IsTelephoneMovilDelivery,
      bool IsDeliveryDescription,
      bool IsTxtDeliveryDescription,
      bool IsDeliveryDescription2,
      bool IsTxtDeliveryDescription2,
      bool IsDistrict,
      bool IsReference)
    {
      this.txtMailDelivery.Enabled = IsMailDelivery;
      this.txtTelephoneDelivery.Enabled = IsTelephoneDelivery;
      this.txtTelephoneMovilDelivery.Enabled = IsTelephoneMovilDelivery;
      this.wddDeliveryDescription.Enabled = IsDeliveryDescription;
      this.txtDeliveryDescription.Enabled = IsTxtDeliveryDescription;
      this.wddDeliveryDescription2.Enabled = IsDeliveryDescription2;
      this.txtDeliveryDescription2.Enabled = IsTxtDeliveryDescription2;
      this.wddDistrict.Enabled = IsDistrict;
      this.txtReference.Enabled = IsReference;
    }

    protected void rblWantPlateDelivery_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (Convert.ToInt32(this.rblWantPlateDelivery.SelectedValue) == 1)
      {
        this.trMotive.Visible = false;
        this.trObservacion.Visible = false;
        this.wddMotive.Enabled = false;
        this.TxtObservacion.Enabled = false;
        this.wddMotive.SelectedValue = "0";
        this.TxtObservacion.Text = "";
      }
      else
      {
        this.trMotive.Visible = true;
        this.trObservacion.Visible = true;
        this.wddMotive.Enabled = true;
        this.TxtObservacion.Enabled = true;
        this.TxtObservacion.Text = "";
      }
    }

    protected void rdbTypeDelivery_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.CleanTextDelivery();
        this.ClearControls();
        this.ClearCheckDetails();
        this.SetDeliveryEnabled(true, true, true, true, true, true, true, true, true);
        this.DivDatosDelivery.Visible = true;
        this.tdBoton.Visible = true;
        this.tdTitle.Visible = true;
        int productCorrespondence = new RequirementQueriesBL().GetProductCorrespondence(Convert.ToInt32(this.Session["i_ProductDelivery"]), 9);
        string[] strArray = this.rdbTypeDelivery.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture).Split('|');
        DataTable dataSource = (DataTable) this.wdgDetailDelivery.DataSource;
        dataSource.Columns["isCheked"].ReadOnly = false;
        foreach (string str in strArray)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataSource.Rows)
          {
            if (row["i_ProductId"].ToString() == str.ToString())
              row["isCheked"] = !(row["i_ProductId"].ToString() == productCorrespondence.ToString()) ? (object) 1 : (object) 0;
          }
        }
        this.Session["DtDetalleDelivery"] = (object) dataSource.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (p => p.Field<int>("isCheked") == 1)).ToList<DataRow>().AsEnumerable<DataRow>().CopyToDataTable<DataRow>();
        DataTable dataTable = dataSource.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (p => p.Field<int>("isCheked") == 1)).ToList<DataRow>().AsEnumerable<DataRow>().CopyToDataTable<DataRow>();
        string str1 = string.Empty;
        dataTable.AsEnumerable().Sum<DataRow>((System.Func<DataRow, double>) (row => row.Field<double>("f_PriceSale")));
        double num1 = dataTable.AsEnumerable().Sum<DataRow>((System.Func<DataRow, double>) (row => row.Field<double>("f_Subtotal")));
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          str1 = str1 + dataTable.Rows[index]["v_Description"].ToString() + " ,";
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          row["v_Description"] = (object) (row.Field<string>("v_Product") + " (" + str1.Substring(0, str1.Length - 2) + ") ");
          row["f_PriceSale"] = (object) 0.0;
          row["f_Subtotal"] = (object) num1;
        }
        this.wdgDetailDelivery.DataSource = (object) dataTable.AsEnumerable().ToList<DataRow>().Take<DataRow>(1).CopyToDataTable<DataRow>();
        this.wdgDetailDelivery.DataBind();
        double num2 = 0.0;
        this.TrTotal.Visible = true;
        this.txtTotal.Text = num2.ToString("0.00");
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.LblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.LblMessage, new HandledException(-100, ex));
      }
    }

    protected void ClearCheckDetails()
    {
      try
      {
        if (this.Session["dt_AllProducts"] == null)
          return;
        DataTable dataTable = (DataTable) this.Session["dt_AllProducts"];
        dataTable.Columns["isCheked"].ReadOnly = false;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (row["isCheked"].ToString() == "1")
            row["isCheked"] = (object) 0;
        }
        this.wdgDetailDelivery.DataSource = (object) dataTable;
        this.wdgDetailDelivery.DataBind();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgError, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgError, new HandledException(-100, ex));
      }
    }

    protected void ClearControls()
    {
      this.LblMessage.Text = "";
      this.LblMessage.Visible = false;
    }

    protected void BtnActualizar_Click(object sender, EventArgs e)
    {
      try
      {
        foreach (GridViewRow row1 in this.wdgDetailDelivery.Rows)
        {
          TextBox control1 = row1.FindControl("i_Quantity") as TextBox;
          if (control1.Text == "0")
          {
            Message.SetMessage(this.LblMessage, new HandledException(1, "la cantidad a comprar debe ser mayor a 0 . "));
            return;
          }
          if (control1.Text == "")
          {
            Message.SetMessage(this.LblMessage, new HandledException(1, "la cantidad a comprar debe ser diferente de vacio "));
            return;
          }
          double num1 = double.Parse(row1.Cells[6].Text);
          string text = row1.Cells[1].Text;
          TextBox control2 = row1.FindControl("i_Quantity") as TextBox;
          control2.ReadOnly = false;
          int num2 = int.Parse(control2.Text);
          double num3 = num1 * (double) num2;
          this.DtDetalleDelivery = (DataTable) this.Session["DtDetalleDelivery"];
          this.DtDetalleDelivery.Columns["i_Quantity"].ReadOnly = false;
          this.DtDetalleDelivery.Columns["f_Subtotal"].ReadOnly = false;
          foreach (DataRow row2 in (InternalDataCollectionBase) this.DtDetalleDelivery.Rows)
          {
            if (row2["i_ProductId"].ToString() == text)
            {
              row2["f_Subtotal"] = (object) num3;
              row2["i_Quantity"] = (object) num2;
            }
          }
        }
        this.UpdateDataGridView();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.LblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.LblMessage, new HandledException(-100, ex));
      }
    }

    public void UpdateDataGridView()
    {
      if (this.Session["DtDetalleDelivery"] == null)
        return;
      GridView gridView = new GridView();
      this.wdgDetailDelivery.DataSource = this.Session["DtDetalleDelivery"];
      this.wdgDetailDelivery.DataBind();
      this.txtTotal.Text = ((DataTable) this.Session["DtDetalleDelivery"]).AsEnumerable().Sum<DataRow>((System.Func<DataRow, double>) (row => row.Field<double>("f_Subtotal"))).ToString("0.00");
    }

    protected void wdgDetailDelivery_RowDataBound(object sender, EventArgs e)
    {
    }

    protected void wdgDetailDelivery_RowCommand(object sender, EventArgs e)
    {
    }

    protected void SaveDelivery_Click(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser1 = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlListDetail.aspx");
        SystemUser systemUser2 = (SystemUser) this.Session["SystemUser"];
        if (this.rdbTypeDelivery.SelectedIndex == -1)
          throw new HandledException(1, "Debe seleccionar un tipo de Delivery");
        if (this.wdgDetailDelivery.Rows.Count == 0)
          throw new HandledException(1, "Tipo de delivery no tiene productos Asociados");
        if (this.txtTotal.Text == "0" || this.txtTotal.Text == "0.00" || this.txtTotal.Text == "")
          throw new HandledException(1, "Monto total a Pagar es incorrecto");
        if (this.txtDeliveryDescription.Text.Trim() == "" || this.txtDeliveryDescription2.Text.Trim() == "" || this.wddDistrict.SelectedIndex == 0)
          throw new HandledException(1, "Debe ingresar todos los datos de la dirección");
        if (this.txtMailDelivery.Text.Trim() == "")
          throw new HandledException(1, "Debe ingresar el correo para el Servicio Delivery");
        if (!new Email().IsValidEmail(this.txtMailDelivery.Text.Trim()))
          throw new HandledException(1, "Debe Ingresar un Email Valido");
        if (this.txtTelephoneDelivery.Text.Trim() == "" && this.txtTelephoneMovilDelivery.Text.Trim() == "___-___-___")
          throw new HandledException(1, "Debe ingresar al menos un teléfono para el Servicio Delivery");
        using (TransactionScope transactionScope = new TransactionScope())
        {
          this.DtRequirements = this.RequirementPlateDataTableCreate();
          double num1 = 0.0;
          double num2 = 0.0;
          double num3 = 0.0;
          this.dtListOrder = (DataTable) this.Session["DtDetalleDelivery"];
          this.objRequirement = new SIIV.BE.Requirement();
          this.objRequirement.i_RequirementTypeId = new int?(4);
          this.objRequirement.f_Quantity = new double?((double) this.dtListOrder.Rows.Count);
          this.objRequirement.v_Observations = "";
          this.objRequirement.i_ProofPaymentTypeId = new int?();
          this.objRequirement.i_InsertUserId = new int?(systemUser2.i_SystemUserId);
          this.objRequirement.v_Ubigeo = "";
          this.objRequirement.i_Status = new int?(-4);
          this.objRequirementProgramation = new RequirementProgramation();
          this.objRequirementProgramation.v_PlateNew = this.Session["v_PlateNew"].ToString();
          this.objRequirementProgramation.i_ZoneReference = new int?(Convert.ToInt32(this.ViewState["varZona"].ToString()));
          this.objRequirementProgramation.i_DistrictReference = new int?(Convert.ToInt32(this.ViewState["varDistrito"].ToString()));
          this.objContributorRequester = new RequirementContributor();
          this.objContributorRequester.i_DocumentTypeId = systemUser1.i_DocumentTypeId;
          this.objContributorRequester.v_DocumentNumber = systemUser1.v_DocumentNumber;
          this.objContributorRequester.v_LastName = systemUser1.v_LastName;
          this.objContributorRequester.v_FirstName = systemUser1.v_FirstName;
          this.objContributorRequester.v_CompleteName = systemUser1.v_FirstName + " " + systemUser1.v_LastName;
          this.objContributorRequester.v_Address = "";
          this.objContributorRequester.v_AddressLocation = this.wddDeliveryDescription.SelectedItem.Text + " " + this.txtDeliveryDescription.Text + " " + this.wddDeliveryDescription2.SelectedItem.Text + " " + this.txtDeliveryDescription2.Text + " |" + this.txtReference.Text;
          this.objContributorRequester.v_Email = this.txtMailDelivery.Text.Trim();
          this.objContributorRequester.v_PhoneNumber = this.txtTelephoneDelivery.Text.Trim() + "|" + this.txtTelephoneMovilDelivery.Text.Trim();
          this.objContributorRequester.i_PersonTypeId = new int?();
          this.objRequirementContributor = new RequirementContributor();
          this.objRequirementContributor.i_DocumentTypeId = new int?();
          this.objRequirementContributor.v_DocumentNumber = (string) null;
          this.objRequirementContributor.v_LastName = "";
          this.objRequirementContributor.v_FirstName = "";
          this.objRequirementContributor.v_CompleteName = (string) null;
          this.objRequirementContributor.v_Email = (string) null;
          this.objRequirementContributor.v_Address = (string) null;
          this.oRequirementManagement = new RequirementManagementBL();
          this.objRequirementPlate = new RequirementPlate();
          this.objRequirementPlate.i_DeliveryPointId = new int?(Convert.ToInt32(new RequirementQueriesBL().ValidateDeliveryZone(Convert.ToInt32(this.Session["PuntoEntrega"])).Rows[0]["i_DeliveryReferenceId"]));
          this.objRequirementPlate.i_PlateTypeId = new int?(12);
          foreach (DataRow row1 in (InternalDataCollectionBase) this.dtListOrder.Rows)
          {
            int num4 = 12;
            int int32 = Convert.ToInt32((object) enmRequirementPlateType.Delivery);
            DataRow row2 = this.DtRequirements.NewRow();
            row2["i_DeliveryPointId"] = (object) this.objRequirementPlate.i_DeliveryPointId;
            row2["i_RegistrationTypeId"] = (object) -1;
            row2["i_RegistrationOfficeId"] = (object) -1;
            row2["i_RegistryZoneId"] = (object) -1;
            row2["i_VehicleCategoryId"] = (object) -1;
            row2["i_VehicleTypeUseId"] = (object) -1;
            row2["i_VehicleClassId"] = (object) -1;
            row2["v_CompleteNameOwner"] = (object) "";
            row2["i_requirementPlatetypeId"] = (object) int32;
            row2["i_SpecialPlateTypeid"] = (object) DBNull.Value;
            row2["i_ProductId"] = (object) Convert.ToInt32(row1["i_ProductId"].ToString());
            row2["i_VehicleId"] = (object) -1;
            row2["v_RegistrationCode"] = (object) "";
            row2["i_DataBankId"] = (object) DBNull.Value;
            row2["i_ProcessTypeId"] = (object) num4;
            row2["i_ContigencyTypeId"] = (object) DBNull.Value;
            row2["b_ContigencyDelivery"] = (object) DBNull.Value;
            row2["i_RegistrationUseTypeOldId"] = (object) DBNull.Value;
            row2["d_RegistrationDispatchDate"] = (object) DateTime.Now.ToString("yyyy-MM-dd");
            row2["i_PlateTypeId"] = (object) this.objRequirementPlate.i_PlateTypeId;
            row2["b_PendingConfirmation"] = (object) DBNull.Value;
            row2["b_Migrated"] = (object) DBNull.Value;
            row2["i_StatusRequirementPlate"] = (object) 0;
            row2["i_InsertUserId"] = (object) Convert.ToInt32(systemUser2.i_SystemUserId);
            row2["i_Quantity"] = (object) Convert.ToInt32(row1["i_Quantity"].ToString());
            row2["i_RequirementPlateRefId"] = (object) Convert.ToInt32(this.Session["i_RequirementPlateId"]);
            this.DtRequirements.Rows.Add(row2);
            num1 += Convert.ToDouble(row1["f_PriceCost"].ToString()) * (double) Convert.ToInt32(row1["i_Quantity"].ToString());
            num2 += Convert.ToDouble(row1["f_PriceTax"].ToString()) * (double) Convert.ToInt32(row1["i_Quantity"].ToString());
            num3 += Convert.ToDouble(row1["f_PriceSale"].ToString()) * (double) Convert.ToInt32(row1["i_Quantity"].ToString());
          }
          this.objPayment = new Payment();
          this.objPayment.f_PriceSale = new double?(Math.Round(Convert.ToDouble(num1), 2));
          this.objPayment.f_PriceTax = new double?(Math.Round(Convert.ToDouble(num2), 2));
          this.objPayment.f_PriceTotal = new double?(Math.Round(Convert.ToDouble(num3), 2));
          this.objPayment.i_Status = new int?(0);
          this.objPayment.i_PaymentTypeId = new int?(1);
          this.objPayment.i_BankId = new int?(0);
          this.objPayment.v_BankOperationNumber = (string) null;
          this.objPayment.v_BankOperationUser = (string) null;
          this.objPayment.v_BankOperationTerminal = (string) null;
          this.objPayment.i_AccountId = new int?();
          this.ViewState["f_PriceTotal"] = (object) this.objPayment.f_PriceTotal;
          int num5 = this.oRequirementManagement.RequirementDeliveryOrderInsert(this.objRequirement, this.objRequirementContributor, this.DtRequirements, this.objPayment, this.objContributorRequester, this.objRequirementProgramation);
          this.Session["Monto"] = this.ViewState["f_PriceTotal"];
          if (num5 != 0)
          {
            this.ViewState["i_RequirementId"] = (object) num5;
            string script = UtilDA.ActiveTabIndexByDelivery("tabs", 3, "0,1,2");
            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
            this.Session["OpenSucesfull"] = (object) 0;
            this.EnunciadoDelivery.Visible = true;
            this.LoadParametersCalendar();
            this.LoadCalendarByDelivery();
            this.Session.Remove("i_RequirementCallId");
          }
          transactionScope.Complete();
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.LblMessage, ex);
        this.HidePopup();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.LblMessage, new HandledException(-100, ex));
        this.HidePopup();
      }
      finally
      {
        this.HidePopup();
      }
    }

    private DataTable RequirementPlateDataTableCreate()
    {
      try
      {
        this.DtRequirements = new DataTable();
        this.DtRequirements.Columns.Add(new DataColumn("i_DetailID", typeof (int))
        {
          AllowDBNull = true,
          AutoIncrement = true,
          AutoIncrementSeed = 1L,
          AutoIncrementStep = 1L
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_DeliveryPointId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_RegistrationTypeId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_RegistrationOfficeId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_RegistryZoneId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_VehicleCategoryId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_VehicleTypeUseId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_VehicleClassId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("v_CompleteNameOwner", typeof (string))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_requirementPlatetypeId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_SpecialPlateTypeid", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_ProductId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_VehicleId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("v_RegistrationCode", typeof (string))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_DataBankId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_ProcessTypeId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_ContigencyTypeId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("b_ContigencyDelivery", typeof (bool))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_RegistrationUseTypeOldId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("d_RegistrationDispatchDate", typeof (DateTime))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_PlateTypeId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("b_PendingConfirmation", typeof (bool))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("b_Migrated", typeof (bool))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_StatusRequirementPlate", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_InsertUserId", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_Quantity", typeof (int))
        {
          AllowDBNull = true
        });
        this.DtRequirements.Columns.Add(new DataColumn("i_RequirementPlateRefId", typeof (int))
        {
          AllowDBNull = true
        });
        return this.DtRequirements;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadCalendarByDelivery()
    {
      this.gvSchedule.Visible = false;
      try
      {
        string str = this.Session["v_PlateNew"].ToString();
        this.ObjRequirementQueriesBL = new RequirementQueriesBL();
        DataTable dataTable = new DataTable();
        if (str == null || str == "")
        {
          this.lblMessageCalendar.Visible = true;
          Message.SetMessage(this.lblMessageCalendar, enmMessageType.Warning, "Ingrese placa");
        }
        else
        {
          DataTable requirementProgramation = this.ObjRequirementQueriesBL.GetRequirementProgramation(str);
          if (requirementProgramation.Rows.Count > 0)
          {
            if (Convert.ToInt16(requirementProgramation.Rows[0]["i_Status"].ToString()) == (short) 1 || Convert.ToInt16(requirementProgramation.Rows[0]["i_Status"].ToString()) == (short) 3)
              Message.SetMessage(this.lblMessageCalendar, enmMessageType.Warning, "La placa " + str + " tiene una programación registrada para el Distrito: " + requirementProgramation.Rows[0]["v_DistrictName"].ToString() + " el dia: " + requirementProgramation.Rows[0]["d_RegistrationDate"].ToString() + "  en el horario: " + requirementProgramation.Rows[0]["v_BlockTime"].ToString());
            else if (Convert.ToInt16(requirementProgramation.Rows[0]["i_Status"].ToString()) == (short) 4)
              Message.SetMessage(this.lblMessageCalendar, enmMessageType.Warning, "La placa " + str + "  se encuentra por recoger en AAP.");
            else if (Convert.ToInt16(requirementProgramation.Rows[0]["i_Status"].ToString()) == (short) 5)
            {
              Message.SetMessage(this.lblMessageCalendar, enmMessageType.Warning, "La placa " + str + "  ya fue entregada al cliente.");
            }
            else
            {
              this.ViewState["i_RequirementPlateIdOld"] = (object) requirementProgramation.Rows[0]["i_RequirementPlateId"].ToString();
              this.ViewState["i_RequirementIdOld"] = (object) requirementProgramation.Rows[0]["i_RequirementId"].ToString();
              this.ViewState["i_PlateTypeId"] = (object) requirementProgramation.Rows[0]["i_PlateTypeId"].ToString();
              this.ViewState["v_PlateNew"] = (object) requirementProgramation.Rows[0]["v_PlateNew"].ToString();
              this.ViewState["i_ZonaReference"] = (object) requirementProgramation.Rows[0]["i_ZoneReference"].ToString();
              this.ViewState["i_DistrictReference"] = (object) requirementProgramation.Rows[0]["i_DistrictReference"].ToString();
              this.ViewState["i_Status"] = (object) requirementProgramation.Rows[0]["i_Status"].ToString();
              Message.SetMessage(this.lblMessageCalendar, enmMessageType.Success, "Se validó la placa correctamente");
              this.LoadShedule(str);
            }
          }
          else
            Message.SetMessage(this.lblMessageCalendar, enmMessageType.Warning, "La placa " + str + " no puede ser programada, o ya tiene una programación pendiente");
        }
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageCalendar, enmMessageType.Error, ex.Message);
      }
    }

    protected void LoadShedule(string PlateNew)
    {
      this.lblMessageCalendar.Visible = false;
      try
      {
        this.ObjRequirementQueriesBL = new RequirementQueriesBL();
        DataTable dataTable = new DataTable();
        DataTable requirementSchedule = this.ObjRequirementQueriesBL.GetRequirementSchedule(PlateNew);
        this.Session["iReprogramation"] = (object) 0;
        this.ViewState["dt_Result"] = (object) requirementSchedule;
        this.gvSchedule.Visible = true;
        this.gvSchedule.DataSource = (object) requirementSchedule;
        this.gvSchedule.DataBind();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageCalendar, enmMessageType.Error, ex.Message);
      }
    }

    public void LoadParametersCalendar()
    {
      try
      {
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.BookQueryGroup.ToString(),
          (object) "",
          (object) "1",
          (object) "1"
        });
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (!(row["i_GroupId"].ToString() == SystemParameterGroups.BookQueryGroup.ToString()))
              ;
          }
        }
        this.ViewState["dtParametrosHorario"] = (object) dataTable;
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.LblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.LblMessage, new HandledException(-100, ex));
      }
    }

    private void CreatePopUpServer2(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp2('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void btnOpenSucesfull_Click(object sender, EventArgs e)
    {
      if (this.Session["OpenSucesfull"] == null || Convert.ToInt32(this.Session["OpenSucesfull"]) != 1)
        return;
      this.Response.Redirect("~/Delivery/Payment/MethodPaymentDelivery.aspx?RequirementId=" + this.ViewState["i_RequirementId"].ToString() + "&RequirementPlateIdOld=" + this.ViewState["i_RequirementPlateIdOld"].ToString() + "&v_PlateNew=" + this.ViewState["v_PlateNew"]?.ToString(), false);
    }

    protected void btnJavaScriptResponse_Click(object sender, EventArgs e)
    {
      if (Convert.ToInt32(this.Session["OpenSucesfull"]) == 1)
        this.ViewState["LoadNull"] = (object) 1;
      else
        this.ViewState["LoadNull"] = (object) null;
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
            child.ImageUrl = "../../Images/cancel.png";
            child.Enabled = false;
            child.CssClass = "disabledImageButton";
            break;
          case -1:
            child.ImageUrl = "../../Images/delete.png";
            child.Enabled = false;
            child.CssClass = "disabledImageButton";
            break;
          default:
            child.ImageUrl = "../../Images/add.png";
            break;
        }
        e.Row.Cells[index].Controls.Add((Control) child);
        if (Convert.ToInt32(this.ViewState["i_Status"]) != 0)
          child.Attributes.Add("onclick", string.Format("OpenPopUp2('{0}','{1}','{2}','{3}');", (object) ("../BookDelivery.aspx?i_RequirementPlateId=" + this.ViewState["i_RequirementPlateIdOld"].ToString() + "&Zone=" + this.ViewState["i_ZonaReference"].ToString() + "&i_DistrictReference=" + this.ViewState["i_DistrictReference"].ToString() + "&Schedule=" + str + "&date=" + this.ViewState["date" + index.ToString()].ToString() + "&vstate=" + int16.ToString()), (object) "Registro de Cita", (object) "500px", (object) "340px"));
        else
          child.Attributes.Add("onclick", string.Format("OpenPopUp2('{0}','{1}','{2}','{3}');", (object) ("../BookDelivery.aspx?i_RequirementPlateId=" + this.ViewState["i_RequirementPlateIdOld"].ToString() + "&Zone=" + this.ViewState["i_ZonaReference"].ToString() + "&i_DistrictReference=" + this.ViewState["i_DistrictReference"].ToString() + "&Schedule=" + str + "&date=" + this.ViewState["date" + index.ToString()].ToString() + "&vstate=" + int16.ToString() + "&RequirementId=" + this.ViewState["i_RequirementPlateIdOld"].ToString() + "&t=" + Convert.ToString(this.ViewState["i_PlateTypeId"].ToString())), (object) "Registro de Cita", (object) "500px", (object) "340px"));
      }
    }

    protected void Regresar_Click(object sender, EventArgs e)
    {
      if (((SystemUser) this.Session["SystemUser"]).i_RoleConfigId == 30)
      {
        string script = UtilDA.ActiveTabIndexByDelivery("tabs", 0, "1,2,3");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
      else
      {
        string script = UtilDA.ActiveTabIndexByDelivery("tabs", 1, "0,2,3");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
      this.SetDeliveryEnabled(false, false, false, false, false, false, false, false, false);
      this.DivDatosDelivery.Visible = false;
      this.tdBoton.Visible = false;
      this.tdTitle.Visible = false;
      this.Session.Remove("i_ProductDelivery");
      this.Session.Remove("dt_AllProducts");
      this.Session.Remove("DtDetalleDelivery");
      this.TrTotal.Visible = false;
      this.wdgDetailDelivery.DataSource = (object) null;
      this.wdgDetailDelivery.DataBind();
    }

    protected void chkGenerationBetween_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkGenerationBetween.Checked)
      {
        this.wdpStartDate.Enabled = true;
        this.wdpEndDate.Enabled = true;
        this.wdgList.DataSource = (object) null;
        this.wdgList.DataBind();
      }
      else
      {
        this.wdpStartDate.Enabled = false;
        this.wdpEndDate.Enabled = false;
        this.wdgList.DataSource = (object) null;
        this.wdgList.DataBind();
      }
    }

    protected void wibNew_Click(object sender, EventArgs e)
    {
      this.TxtTelefono.Enabled = true;
      this.TxtTelefono.ReadOnly = false;
      this.wibNew.Visible = false;
      this.wibSaveTelefono.Visible = true;
      this.Session["ValueInitial"] = (object) this.TxtTelefono.Text;
    }

    protected void wibSaveTelefono_Click(object sender, EventArgs e)
    {
      try
      {
        string v_PhoneNumberNew = this.TxtTelefono.Text.ToString().Trim();
        if (new RequirementQueriesBL().UpdatePhoneNumber(Convert.ToInt32(this.Session["i_RequirementPlateId"]), Convert.ToInt32(this.Session["i_SystemUserId"]), v_PhoneNumberNew) > 0)
        {
          this.TxtTelefono.Enabled = false;
          this.TxtTelefono.ReadOnly = true;
          this.wibSaveTelefono.Visible = false;
          this.wibNew.Visible = true;
          Message.SetMessage(this.lblMsgError, new HandledException(2, "Se Grabó la Operación exitosamente"));
          this.Session.Remove("ValueInitial");
        }
        else
          Message.SetMessage(this.lblMsgError, new HandledException(2, "No se pudo grabar la Operación Correctamente"));
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgError, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgError, new HandledException(-100, ex));
      }
    }

    protected void wibCancelTelefono_Click(object sender, EventArgs e)
    {
      this.TxtTelefono.Enabled = false;
      this.TxtTelefono.ReadOnly = true;
      this.wibSaveTelefono.Visible = false;
      this.wibNew.Visible = true;
      this.TxtTelefono.Text = Convert.ToString(this.Session["ValueInitial"]);
      this.Session.Remove("ValueInitial");
    }
  }
}
