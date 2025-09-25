// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Delivery.MaintenanceDelivery
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Delivery
{
  public class MaintenanceDelivery : Page
  {
    public SystemUser objUserBE;
    public MaintenanceOperation currentOperation = MaintenanceOperation.None;
    public int i_GroupId;
    protected UpdatePanel UpdatePanel3;
    protected Button BtnNewZone;
    protected Button BtnEditZone;
    protected Button BtnSaveZone;
    protected Button BtnDeleteZone;
    protected Button BtnCancelZone;
    protected HtmlTable TblZoneDescription;
    protected TextBox TxtDescription;
    protected TextBox TxtQuantity;
    protected GridView wdgZoneList;
    protected Pager custPagerManDelZone;
    protected Label lblMsg;
    protected Button btnReturnPopupConfirmationZone;
    protected UpdatePanel UpdatePanel4;
    protected Button BtnNewDistric;
    protected Button BtnEditDistric;
    protected Button BtnSaveDistric;
    protected Button BtnDeleteDistric;
    protected Button BtnCancelDistric;
    protected HtmlTable TblDistricDescription;
    protected DropDownList cboZona;
    protected TextBox TxtDescriptionDistric;
    protected HtmlTable TblDeliveryProduct;
    protected DropDownList CbAuto;
    protected DropDownList CbMoto;
    protected GridView wdgDistricList;
    protected Pager custPagerManDel;
    protected Label lblMsgDistic;
    protected Button btnReturnPopupConfirmationDistric;
    protected UpdatePanel UpdatePanel5;
    protected Button BtnNewCourier;
    protected Button BtnEditCourier;
    protected Button BtnSaveCourier;
    protected Button BtnDeleteCourier;
    protected Button BtnCancelCourier;
    protected HtmlTable TblCourierDescription;
    protected DropDownList cboZonaCourier;
    protected TextBox TxtDescriptionCourier;
    protected GridView wdgCourierList;
    protected Pager custPagerManDelCourier;
    protected Label lblMsgCourier;
    protected Button btnReturnPopupConfirmationCourier;
    protected UpdatePanel UpdatePanel6;
    protected Button BtnNewSchedule;
    protected Button BtnEditSchedule;
    protected Button BtnSaveSchedule;
    protected Button BtnDeleteSchedule;
    protected Button BtnCancelSchedule;
    protected HtmlTable TblScheduleDescription;
    protected TextBox TxtDescriptionSchedule;
    protected DropDownList Cbh1;
    protected DropDownList Cbh2;
    protected GridView wdgScheduleList;
    protected Pager custPagerManDelSchedule;
    protected Label lblMsgSchedule;
    protected Button btnReturnPopupConfirmationSchedule;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.ViewState["i_GroupIdSchedule"] = (object) null;
      this.ViewState["i_ParameterIdSchedule"] = (object) null;
      this.ViewState["i_GroupIdZona"] = (object) null;
      this.ViewState["i_ParameterIdZona"] = (object) null;
      this.ViewState["i_GroupIdDistric"] = (object) null;
      this.ViewState["i_ParameterIdDistric"] = (object) null;
      this.ViewState["i_GroupIdCourier"] = (object) null;
      this.ViewState["i_ParameterIdCourier"] = (object) null;
      this.ViewState["True"] = (object) null;
      this.LoadParameters();
      this.EnabledControls(MaintenanceOperation.None);
    }

    private void LoadParameters()
    {
      this.SearchMaintenanceDeliveryZone();
      this.SearchMaintenanceDeliveryDistric();
      this.SearchMaintenanceDeliveryCourier();
      this.SearchMaintenanceDeliverySchedule();
    }

    private void EnabledControls(MaintenanceOperation penuCurrentOperation)
    {
    }

    private void SearchMaintenanceDeliveryList(
      int i_GroupId,
      int i_GroupIdDetail,
      bool pboolLoadPager)
    {
      int pintTotalRows;
      if (i_GroupId == SystemParameterGroups.ZoneDelivery)
      {
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerManDelZone.CurrentPageNumber;
        int pintMaxRows = this.custPagerManDelZone.CurrentPageSize == 0 ? 10 : this.custPagerManDelZone.CurrentPageSize;
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilterWithPage(i_GroupId, systemUser.i_SystemUserId, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        int num = pintTotalRows;
        this.wdgZoneList.DataSource = (object) dataTable;
        this.wdgZoneList.DataBind();
        this.custPagerManDelZone.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
        this.custPagerManDelZone.TotalRecordCount = pintTotalRows;
        if (pboolLoadPager)
          this.custPagerManDelZone.LoadPager();
      }
      if (i_GroupId == SystemParameterGroups.District)
      {
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerManDel.CurrentPageNumber;
        int pintMaxRows = this.custPagerManDel.CurrentPageSize == 0 ? 10 : this.custPagerManDel.CurrentPageSize;
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilterWithPage(i_GroupId, systemUser.i_SystemUserId, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        int num = pintTotalRows;
        this.wdgDistricList.DataSource = (object) dataTable;
        this.wdgDistricList.DataBind();
        this.custPagerManDel.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
        this.custPagerManDel.TotalRecordCount = pintTotalRows;
        if (pboolLoadPager)
          this.custPagerManDel.LoadPager();
      }
      if (i_GroupId == SystemParameterGroups.DeliveryCouriers)
      {
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerManDelCourier.CurrentPageNumber;
        int pintMaxRows = this.custPagerManDelCourier.CurrentPageSize == 0 ? 10 : this.custPagerManDelCourier.CurrentPageSize;
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilterWithPage(i_GroupId, systemUser.i_LocationId, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        int num = pintTotalRows;
        this.wdgCourierList.DataSource = (object) dataTable;
        this.wdgCourierList.DataBind();
        this.custPagerManDelCourier.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
        this.custPagerManDelCourier.TotalRecordCount = pintTotalRows;
        if (pboolLoadPager)
          this.custPagerManDelCourier.LoadPager();
      }
      if (i_GroupId != SystemParameterGroups.BlockSchedule)
        return;
      SystemUser systemUser1 = (SystemUser) this.Session["SystemUser"];
      int pintStartRowIndex1 = pboolLoadPager ? 1 : this.custPagerManDelSchedule.CurrentPageNumber;
      int pintMaxRows1 = this.custPagerManDelSchedule.CurrentPageSize == 0 ? 10 : this.custPagerManDelSchedule.CurrentPageSize;
      DataTable dataTable1 = new SystemParameterQueriesBL().GetbyFilterWithPage(i_GroupId, systemUser1.i_LocationId, pintStartRowIndex1, pintMaxRows1, out pintTotalRows);
      int num1 = pintTotalRows;
      this.wdgScheduleList.DataSource = (object) dataTable1;
      this.wdgScheduleList.DataBind();
      this.custPagerManDelSchedule.TotalPages = num1 % pintMaxRows1 == 0 ? num1 / pintMaxRows1 : num1 / pintMaxRows1 + 1;
      this.custPagerManDelSchedule.TotalRecordCount = pintTotalRows;
      if (pboolLoadPager)
        this.custPagerManDelSchedule.LoadPager();
    }

    private void SearchMaintenanceDeliveryZone()
    {
      if (this.ViewState["currentOperationZone"] != null)
        this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperationZone"];
      if (this.currentOperation != MaintenanceOperation.Edit)
        this.TblZoneDescription.Visible = false;
      this.i_GroupId = SystemParameterGroups.ZoneDelivery;
      this.SearchMaintenanceDeliveryList(this.i_GroupId, 0, true);
    }

    protected void custPagerManDel_PageChangedZone(object sender, CustomPageChangeArgs e)
    {
      int zoneDelivery = SystemParameterGroups.ZoneDelivery;
      int i_GroupIdDetail = 0;
      int currentPageNumber = this.custPagerManDelZone.CurrentPageNumber;
      this.SearchMaintenanceDeliveryList(zoneDelivery, i_GroupIdDetail, false);
    }

    protected void wdgZoneList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
      try
      {
        this.BtnEditZone.Enabled = true;
        this.BtnDeleteZone.Enabled = true;
        this.TblZoneDescription.Visible = false;
        this.BtnCancelZone.Enabled = true;
        this.BtnSaveZone.Enabled = false;
        int int32 = Convert.ToInt32(e.NewSelectedIndex);
        GridViewRow row = this.wdgZoneList.Rows[e.NewSelectedIndex];
        this.ViewState["i_GroupIdZona"] = (object) Convert.ToInt32(this.wdgZoneList.DataKeys[int32]["i_GroupId"].ToString());
        this.ViewState["i_ParameterIdZona"] = (object) Convert.ToInt32(this.wdgZoneList.DataKeys[int32]["i_ParameterId"].ToString());
        this.TxtDescription.Text = HttpUtility.HtmlDecode(row.Cells[3].Text.Trim());
        this.TxtQuantity.Text = row.Cells[5].Text.Trim();
        this.lblMsg.Visible = false;
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

    protected void wibZoneEditClick(object sender, EventArgs e)
    {
      this.currentOperation = MaintenanceOperation.Edit;
      this.TblZoneDescription.Visible = true;
      this.BtnSaveZone.Enabled = true;
      this.ViewState["currentOperationZone"] = (object) this.currentOperation;
      this.lblMsg.Visible = false;
      this.BtnCancelZone.Enabled = true;
    }

    protected void wibZoneNewClick(object sender, EventArgs e)
    {
      this.currentOperation = MaintenanceOperation.AddNew;
      this.wdgZoneList.SelectedIndex = -1;
      this.TxtDescription.Text = "";
      this.TxtQuantity.Text = "";
      this.TblZoneDescription.Visible = true;
      this.BtnSaveZone.Enabled = true;
      this.BtnEditZone.Enabled = false;
      this.BtnDeleteZone.Enabled = false;
      this.BtnCancelZone.Enabled = true;
      this.ViewState["currentOperationZone"] = (object) this.currentOperation;
      this.lblMsg.Visible = false;
    }

    protected void wibZoneDeleteClick(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.CreatePopUpServer("SIIV - Mantenedores Delivery Zonas", "../../UserControls/PopupConfirmationDelivery.aspx?MessageTypeId=1&MessageText=¿Desea Eliminar el Registro?&Typemaintainer=1", "350px", "190px");
    }

    protected void wibZoneDeleteClick1(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        this.BtnEditZone.Enabled = false;
        new RequirementManagementBL().UpdateDeleteDeliverySystemParameter((int) this.ViewState["i_GroupIdZona"], (int) this.ViewState["i_ParameterIdZona"], "", "", "", systemUser.i_SystemUserId, 0, "02");
        Message.SetMessage(this.lblMsg, enmMessageType.Success, "• Se eliminó satisfactoriamente.");
        this.BtnDeleteZone.Enabled = false;
        this.BtnEditZone.Enabled = false;
        this.BtnSaveZone.Enabled = false;
        this.BtnCancelZone.Enabled = false;
        this.wdgZoneList.SelectedIndex = -1;
        this.TblZoneDescription.Visible = false;
        this.SearchMaintenanceDeliveryZone();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
    }

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel3, this.UpdatePanel3.GetType(), "Script", script, true);
    }

    protected void wibZoneSaveClick(object sender, EventArgs e)
    {
      try
      {
        if (this.TxtDescription.Text.Trim() == "" || this.TxtDescription.Text.Trim() == string.Empty)
        {
          this.lblMsg.Visible = true;
          Message.SetMessage(this.lblMsg, enmMessageType.Warning, "Ingrese el Nombre");
        }
        else if (this.TxtQuantity.Text.Trim() == "" || this.TxtQuantity.Text.Trim() == string.Empty)
        {
          this.lblMsg.Visible = true;
          Message.SetMessage(this.lblMsg, enmMessageType.Warning, "•Ingrese la Cantidad");
        }
        else
        {
          SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
          this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperationZone"];
          string text = this.TxtDescription.Text;
          string v_OldValue = this.TxtQuantity.Text.Trim();
          int i_Reference = 0;
          int zoneDelivery = SystemParameterGroups.ZoneDelivery;
          int i_ParameterId1 = 0;
          int num;
          switch (this.currentOperation)
          {
            case MaintenanceOperation.AddNew:
              num = new RequirementManagementBL().InsertDeliverySystemParameter(zoneDelivery, i_ParameterId1, text, text, v_OldValue, systemUser.i_SystemUserId);
              Message.SetMessage(this.lblMsg, enmMessageType.Success, "•Se creó satisfactoriamente.");
              break;
            case MaintenanceOperation.Edit:
              int i_ParameterId2 = (int) this.ViewState["i_ParameterIdZona"];
              num = new RequirementManagementBL().UpdateDeleteDeliverySystemParameter(zoneDelivery, i_ParameterId2, text, text, v_OldValue, systemUser.i_SystemUserId, i_Reference, "01");
              Message.SetMessage(this.lblMsg, enmMessageType.Success, "•Se actualizó satisfactoriamente.");
              break;
          }
          this.ViewState["currentOperationZone"] = (object) MaintenanceOperation.None;
          this.BtnDeleteZone.Enabled = false;
          this.BtnEditZone.Enabled = false;
          this.BtnSaveZone.Enabled = false;
          this.BtnCancelZone.Enabled = false;
          this.wdgZoneList.SelectedIndex = -1;
          this.TblZoneDescription.Visible = false;
          this.SearchMaintenanceDeliveryZone();
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
    }

    protected void wibZoneCancelClick(object sender, EventArgs e)
    {
      this.ViewState["i_GroupIdZona"] = (object) null;
      this.ViewState["i_ParameterIdZona"] = (object) null;
      this.ViewState["currentOperationZone"] = (object) MaintenanceOperation.None;
      this.BtnNewZone.Enabled = true;
      this.BtnDeleteZone.Enabled = false;
      this.BtnEditZone.Enabled = false;
      this.BtnSaveZone.Enabled = false;
      this.BtnCancelZone.Enabled = false;
      this.wdgZoneList.SelectedIndex = -1;
      this.TblZoneDescription.Visible = false;
      this.lblMsg.Visible = false;
      this.SearchMaintenanceDeliveryZone();
    }

    protected void custPagerManDel_PageChanged(object sender, CustomPageChangeArgs e)
    {
      int district = SystemParameterGroups.District;
      int i_GroupIdDetail = 0;
      int currentPageNumber = this.custPagerManDel.CurrentPageNumber;
      this.SearchMaintenanceDeliveryList(district, i_GroupIdDetail, false);
    }

    private void SearchMaintenanceDeliveryDistric()
    {
      if (this.ViewState["currentOperationDistric"] != null)
        this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperationDistric"];
      if (this.currentOperation != MaintenanceOperation.Edit)
      {
        this.TblDistricDescription.Visible = false;
        this.TblDeliveryProduct.Visible = false;
      }
      this.i_GroupId = SystemParameterGroups.District;
      this.SearchMaintenanceDeliveryList(this.i_GroupId, 0, true);
    }

    protected void wdgDistricList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
      try
      {
        int length = 3;
        this.BtnEditDistric.Enabled = true;
        this.BtnSaveDistric.Enabled = false;
        this.BtnDeleteDistric.Enabled = true;
        this.BtnCancelDistric.Enabled = true;
        this.lblMsgDistic.Visible = false;
        this.TblDistricDescription.Visible = false;
        this.TblDeliveryProduct.Visible = false;
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = new DataTable();
        DataTable dataTable3 = new DataTable();
        this.objUserBE = this.Session["SystemUser"] as SystemUser;
        List<DataRow> dataRowList1 = new List<DataRow>();
        List<DataRow> dataRowList2 = new List<DataRow>();
        List<DataRow> dataRowList3 = new List<DataRow>();
        int iLocationId = this.objUserBE.i_LocationId;
        int int32_1 = Convert.ToInt32(e.NewSelectedIndex);
        GridViewRow row1 = this.wdgDistricList.Rows[e.NewSelectedIndex];
        this.ViewState["i_GroupIdDistric"] = (object) Convert.ToInt32(this.wdgDistricList.DataKeys[int32_1]["i_GroupId"].ToString());
        this.ViewState["i_ParameterIdDistric"] = (object) Convert.ToInt32(this.wdgDistricList.DataKeys[int32_1]["i_ParameterId"].ToString());
        this.ViewState["v_OldValue1"] = (object) this.wdgDistricList.DataKeys[int32_1]["v_OldValue"].ToString();
        string str1 = this.wdgDistricList.DataKeys[int32_1]["v_ReferenceId"].ToString();
        this.TxtDescriptionDistric.Text = HttpUtility.HtmlDecode(row1.Cells[3].Text.Trim());
        this.TxtQuantity.Text = row1.Cells[5].Text.Trim();
        this.lblMsg.Visible = false;
        DataTable groupsDelivery = new RequirementQueriesBL().GetGroupsDelivery(this.i_GroupId, 0, "04");
        foreach (DataRow row2 in (InternalDataCollectionBase) groupsDelivery.Rows)
        {
          if (Convert.ToInt32(row2["v_OldValue"]) != iLocationId)
            dataRowList1.Add(row2);
        }
        foreach (DataRow row3 in dataRowList1)
          groupsDelivery.Rows.Remove(row3);
        if (str1.Length == 5)
          length = 1;
        if (str1.Length == 6)
          length = 2;
        string str2 = str1.Substring(4, length);
        this.cboZona.DataSource = (object) groupsDelivery;
        this.cboZona.DataTextField = "v_Description";
        this.cboZona.DataValueField = "i_ParameterId";
        this.cboZona.DataBind();
        this.cboZona.SelectedValue = str2;
        int int32_2 = Convert.ToInt32(this.wdgDistricList.DataKeys[int32_1]["i_ParameterId"].ToString());
        string productOfDistrict1 = new RequirementQueriesBL().GetDeliveryProductOfDistrict(int32_2, 1);
        DataTable deliveryProduct1 = new RequirementQueriesBL().GetDeliveryProduct();
        foreach (DataRow row4 in (InternalDataCollectionBase) deliveryProduct1.Rows)
        {
          if (Convert.ToInt32(row4["i_VehicleClasification"]) != 1)
            dataRowList3.Add(row4);
        }
        foreach (DataRow row5 in dataRowList3)
          deliveryProduct1.Rows.Remove(row5);
        this.CbAuto.DataSource = (object) deliveryProduct1;
        this.CbAuto.DataTextField = "v_Description";
        this.CbAuto.DataValueField = "i_ProductId";
        this.CbAuto.DataBind();
        this.CbAuto.Items.Insert(1, new ListItem("Ninguno", "-1"));
        this.CbAuto.SelectedValue = productOfDistrict1;
        string productOfDistrict2 = new RequirementQueriesBL().GetDeliveryProductOfDistrict(int32_2, 2);
        DataTable deliveryProduct2 = new RequirementQueriesBL().GetDeliveryProduct();
        foreach (DataRow row6 in (InternalDataCollectionBase) deliveryProduct2.Rows)
        {
          if (Convert.ToInt32(row6["i_VehicleClasification"]) != 2)
            dataRowList2.Add(row6);
        }
        foreach (DataRow row7 in dataRowList2)
          deliveryProduct2.Rows.Remove(row7);
        this.CbMoto.DataSource = (object) deliveryProduct2;
        this.CbMoto.DataTextField = "v_Description";
        this.CbMoto.DataValueField = "i_ProductId";
        this.CbMoto.DataBind();
        this.CbMoto.Items.Insert(1, new ListItem("Ninguno", "-1"));
        this.CbMoto.SelectedValue = productOfDistrict2;
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgDistic, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgDistic, new HandledException(-100, ex));
      }
    }

    protected void wibDistricEditClick(object sender, EventArgs e)
    {
      this.currentOperation = MaintenanceOperation.Edit;
      this.TblDistricDescription.Visible = true;
      this.TblDeliveryProduct.Visible = true;
      this.BtnSaveDistric.Enabled = true;
      this.BtnCancelDistric.Enabled = true;
      this.ViewState["currentOperationDistric"] = (object) this.currentOperation;
      this.lblMsgDistic.Visible = false;
    }

    protected void wibDistricNewClick(object sender, EventArgs e)
    {
      this.currentOperation = MaintenanceOperation.AddNew;
      this.objUserBE = this.Session["SystemUser"] as SystemUser;
      List<DataRow> dataRowList1 = new List<DataRow>();
      List<DataRow> dataRowList2 = new List<DataRow>();
      List<DataRow> dataRowList3 = new List<DataRow>();
      int iLocationId = this.objUserBE.i_LocationId;
      DataTable dataTable1 = new DataTable();
      DataTable dataTable2 = new DataTable();
      DataTable dataTable3 = new DataTable();
      this.wdgDistricList.SelectedIndex = -1;
      this.TxtDescriptionDistric.Text = "";
      this.TblDistricDescription.Visible = true;
      this.TblDeliveryProduct.Visible = true;
      this.BtnCancelDistric.Enabled = true;
      this.BtnSaveDistric.Enabled = true;
      this.BtnEditDistric.Enabled = false;
      this.BtnDeleteDistric.Enabled = false;
      this.ViewState["currentOperationDistric"] = (object) this.currentOperation;
      this.lblMsgDistic.Visible = false;
      DataTable groupsDelivery = new RequirementQueriesBL().GetGroupsDelivery(this.i_GroupId, 0, "04");
      foreach (DataRow row in (InternalDataCollectionBase) groupsDelivery.Rows)
      {
        if (Convert.ToInt32(row["v_OldValue"]) != iLocationId)
          dataRowList1.Add(row);
      }
      foreach (DataRow row in dataRowList1)
        groupsDelivery.Rows.Remove(row);
      this.cboZona.DataSource = (object) groupsDelivery;
      this.cboZona.DataTextField = "v_Description";
      this.cboZona.DataValueField = "i_ParameterId";
      this.cboZona.DataBind();
      this.cboZona.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
      this.cboZona.SelectedIndex = 0;
      DataTable deliveryProduct1 = new RequirementQueriesBL().GetDeliveryProduct();
      foreach (DataRow row in (InternalDataCollectionBase) deliveryProduct1.Rows)
      {
        if (Convert.ToInt32(row["i_VehicleClasification"]) != 1)
          dataRowList2.Add(row);
      }
      foreach (DataRow row in dataRowList2)
        deliveryProduct1.Rows.Remove(row);
      this.CbAuto.DataSource = (object) deliveryProduct1;
      this.CbAuto.DataTextField = "v_Description";
      this.CbAuto.DataValueField = "i_ProductId";
      this.CbAuto.DataBind();
      this.CbAuto.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
      this.CbAuto.Items.Insert(1, new ListItem("Ninguno", "-1"));
      this.CbAuto.SelectedIndex = 0;
      DataTable deliveryProduct2 = new RequirementQueriesBL().GetDeliveryProduct();
      foreach (DataRow row in (InternalDataCollectionBase) deliveryProduct2.Rows)
      {
        if (Convert.ToInt32(row["i_VehicleClasification"]) != 2)
          dataRowList3.Add(row);
      }
      foreach (DataRow row in dataRowList3)
        deliveryProduct2.Rows.Remove(row);
      this.CbMoto.DataSource = (object) deliveryProduct2;
      this.CbMoto.DataTextField = "v_Description";
      this.CbMoto.DataValueField = "i_ProductId";
      this.CbMoto.DataBind();
      this.CbMoto.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
      this.CbMoto.Items.Insert(1, new ListItem("Ninguno", "-1"));
      this.CbMoto.SelectedIndex = 0;
    }

    protected void wibDistricDeleteClick(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.CreatePopUpServerDistric("SIIV - Mantenedores Delivery Distritos", "../../UserControls/PopupConfirmationDelivery.aspx?MessageTypeId=1&MessageText=¿Desea Eliminar el Registro?&Typemaintainer=2", "350px", "190px");
    }

    protected void wibDistricDeleteClick1(object sender, EventArgs e)
    {
      try
      {
        new RequirementManagementBL().UpdateDeleteDeliverySystemParameter((int) this.ViewState["i_GroupIdDistric"], (int) this.ViewState["i_ParameterIdDistric"], "", "", "", ((SystemUser) this.Session["SystemUser"]).i_SystemUserId, 0, "02");
        Message.SetMessage(this.lblMsgDistic, enmMessageType.Success, "• Se eliminó satisfactoriamente.");
        this.BtnDeleteDistric.Enabled = false;
        this.BtnEditDistric.Enabled = false;
        this.BtnSaveDistric.Enabled = false;
        this.BtnCancelDistric.Enabled = false;
        this.TblDistricDescription.Visible = false;
        this.TblDeliveryProduct.Visible = false;
        this.wdgDistricList.SelectedIndex = -1;
        this.SearchMaintenanceDeliveryDistric();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgDistic, ex);
      }
    }

    private void CreatePopUpServerDistric(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel4, this.UpdatePanel4.GetType(), "Script", script, true);
    }

    protected void wibDistricSaveClick(object sender, EventArgs e)
    {
      try
      {
        if (this.TxtDescriptionDistric.Text.Trim() == "" || this.TxtDescriptionDistric.Text.Trim() == string.Empty)
        {
          this.lblMsgDistic.Visible = true;
          Message.SetMessage(this.lblMsgDistic, enmMessageType.Warning, "Ingrese el Distrito");
        }
        else if (this.cboZona.SelectedValue == "0")
        {
          this.lblMsg.Visible = true;
          Message.SetMessage(this.lblMsgDistic, enmMessageType.Warning, "•Seleccione la Zona");
        }
        else if (this.CbAuto.SelectedValue == "0" || this.CbMoto.SelectedValue == "0")
        {
          this.lblMsg.Visible = true;
          Message.SetMessage(this.lblMsgDistic, enmMessageType.Warning, "•Seleccione los dos servicios delivery ");
        }
        else
        {
          SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
          this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperationDistric"];
          string text = this.TxtDescriptionDistric.Text;
          int district = SystemParameterGroups.District;
          string v_Value = this.CbAuto.SelectedValue + "|" + this.CbMoto.SelectedValue;
          int num;
          switch (this.currentOperation)
          {
            case MaintenanceOperation.AddNew:
              int int32_1 = Convert.ToInt32(this.cboZona.SelectedValue);
              num = new RequirementManagementBL().InsertDeliverySystemParameter(district, int32_1, v_Value, text, systemUser.i_LocationId.ToString(), systemUser.i_SystemUserId);
              Message.SetMessage(this.lblMsgDistic, enmMessageType.Success, "•Se creó satisfactoriamente.");
              break;
            case MaintenanceOperation.Edit:
              string v_OldValue = this.ViewState["v_OldValue1"].ToString();
              int int32_2 = Convert.ToInt32(this.cboZona.SelectedValue);
              int i_ParameterId = (int) this.ViewState["i_ParameterIdDistric"];
              num = new RequirementManagementBL().UpdateDeleteDeliverySystemParameter(district, i_ParameterId, text, v_Value, v_OldValue, systemUser.i_SystemUserId, int32_2, "01");
              Message.SetMessage(this.lblMsgDistic, enmMessageType.Success, "•Se actualizó satisfactoriamente.");
              break;
          }
          this.ViewState["currentOperationDistric"] = (object) MaintenanceOperation.None;
          this.BtnDeleteDistric.Enabled = false;
          this.BtnEditDistric.Enabled = false;
          this.BtnSaveDistric.Enabled = false;
          this.BtnCancelDistric.Enabled = false;
          this.TblDistricDescription.Visible = false;
          this.TblDeliveryProduct.Visible = false;
          this.wdgDistricList.SelectedIndex = -1;
          this.SearchMaintenanceDeliveryDistric();
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgDistic, ex);
      }
    }

    protected void wibDistricCancelClick(object sender, EventArgs e)
    {
      this.ViewState["currentOperationDistric"] = (object) MaintenanceOperation.None;
      this.ViewState["i_GroupIdDistric"] = (object) null;
      this.ViewState["i_ParameterIdDistric"] = (object) null;
      this.BtnNewDistric.Enabled = true;
      this.BtnDeleteDistric.Enabled = false;
      this.BtnEditDistric.Enabled = false;
      this.BtnSaveDistric.Enabled = false;
      this.BtnCancelDistric.Enabled = false;
      this.TblDistricDescription.Visible = false;
      this.TblDeliveryProduct.Visible = false;
      this.wdgDistricList.SelectedIndex = -1;
      this.lblMsgDistic.Visible = false;
      this.SearchMaintenanceDeliveryDistric();
    }

    private void SearchMaintenanceDeliveryCourier()
    {
      if (this.ViewState["currentOperationCourier"] != null)
        this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperationCourier"];
      if (this.currentOperation != MaintenanceOperation.Edit)
        this.TblCourierDescription.Visible = false;
      this.i_GroupId = SystemParameterGroups.DeliveryCouriers;
      this.SearchMaintenanceDeliveryList(this.i_GroupId, 0, true);
    }

    protected void custPagerManDel_PageChangedCourier(object sender, CustomPageChangeArgs e)
    {
      int deliveryCouriers = SystemParameterGroups.DeliveryCouriers;
      int i_GroupIdDetail = 0;
      int currentPageNumber = this.custPagerManDelZone.CurrentPageNumber;
      this.SearchMaintenanceDeliveryList(deliveryCouriers, i_GroupIdDetail, false);
    }

    protected void wdgCourierList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
      try
      {
        int length = 3;
        this.BtnEditCourier.Enabled = true;
        this.BtnSaveCourier.Enabled = false;
        this.BtnDeleteCourier.Enabled = true;
        this.BtnCancelCourier.Enabled = true;
        this.TblCourierDescription.Visible = false;
        DataTable dataTable = new DataTable();
        this.objUserBE = this.Session["SystemUser"] as SystemUser;
        List<DataRow> dataRowList = new List<DataRow>();
        int iLocationId = this.objUserBE.i_LocationId;
        int int32 = Convert.ToInt32(e.NewSelectedIndex);
        GridViewRow row1 = this.wdgCourierList.Rows[e.NewSelectedIndex];
        this.ViewState["i_GroupIdCourier"] = (object) Convert.ToInt32(this.wdgCourierList.DataKeys[int32]["i_GroupId"].ToString());
        this.ViewState["i_ParameterIdCourier"] = (object) Convert.ToInt32(this.wdgCourierList.DataKeys[int32]["i_ParameterId"].ToString());
        this.ViewState["v_OldValue2"] = (object) this.wdgCourierList.DataKeys[int32]["v_OldValue"].ToString();
        string str1 = this.wdgCourierList.DataKeys[int32]["v_ReferenceId"].ToString();
        this.TxtDescriptionCourier.Text = HttpUtility.HtmlDecode(row1.Cells[3].Text.Trim());
        this.lblMsgCourier.Visible = false;
        DataTable groupsDelivery = new RequirementQueriesBL().GetGroupsDelivery(this.i_GroupId, 0, "04");
        foreach (DataRow row2 in (InternalDataCollectionBase) groupsDelivery.Rows)
        {
          if (Convert.ToInt32(row2["v_OldValue"]) != iLocationId)
            dataRowList.Add(row2);
        }
        foreach (DataRow row3 in dataRowList)
          groupsDelivery.Rows.Remove(row3);
        if (str1.Length == 5)
          length = 1;
        if (str1.Length == 6)
          length = 2;
        string str2 = str1.Substring(4, length);
        this.cboZonaCourier.DataSource = (object) groupsDelivery;
        this.cboZonaCourier.DataTextField = "v_Description";
        this.cboZonaCourier.DataValueField = "i_ParameterId";
        this.cboZonaCourier.DataBind();
        this.cboZonaCourier.SelectedValue = str2;
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgCourier, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgCourier, new HandledException(-100, ex));
      }
    }

    protected void wibCourierEditClick(object sender, EventArgs e)
    {
      this.currentOperation = MaintenanceOperation.Edit;
      this.TblCourierDescription.Visible = true;
      this.BtnSaveCourier.Enabled = true;
      this.BtnCancelCourier.Enabled = true;
      this.ViewState["currentOperationCourier"] = (object) this.currentOperation;
      this.lblMsgCourier.Visible = false;
    }

    protected void wibCourierNewClick(object sender, EventArgs e)
    {
      this.currentOperation = MaintenanceOperation.AddNew;
      this.objUserBE = this.Session["SystemUser"] as SystemUser;
      List<DataRow> dataRowList = new List<DataRow>();
      int iLocationId = this.objUserBE.i_LocationId;
      DataTable dataTable = new DataTable();
      this.wdgCourierList.SelectedIndex = -1;
      this.TblCourierDescription.Visible = true;
      this.TxtDescriptionCourier.Text = "";
      this.TxtDescriptionCourier.Visible = true;
      this.BtnSaveCourier.Enabled = true;
      this.BtnCancelCourier.Enabled = true;
      this.BtnEditCourier.Enabled = false;
      this.BtnDeleteCourier.Enabled = false;
      this.ViewState["currentOperationCourier"] = (object) this.currentOperation;
      this.lblMsgCourier.Visible = false;
      DataTable groupsDelivery = new RequirementQueriesBL().GetGroupsDelivery(this.i_GroupId, 0, "04");
      foreach (DataRow row in (InternalDataCollectionBase) groupsDelivery.Rows)
      {
        if (Convert.ToInt32(row["v_OldValue"]) != iLocationId)
          dataRowList.Add(row);
      }
      foreach (DataRow row in dataRowList)
        groupsDelivery.Rows.Remove(row);
      this.cboZonaCourier.DataSource = (object) groupsDelivery;
      this.cboZonaCourier.DataTextField = "v_Description";
      this.cboZonaCourier.DataValueField = "i_ParameterId";
      this.cboZonaCourier.DataBind();
      this.cboZonaCourier.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
      this.cboZonaCourier.SelectedIndex = 0;
    }

    protected void wibCourierDeleteClick(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.CreatePopUpServerCourier("SIIV - Mantenedores Delivery Mensajeros", "../../UserControls/PopupConfirmationDelivery.aspx?MessageTypeId=1&MessageText=¿Desea Eliminar el Registro?&Typemaintainer=3", "350px", "190px");
    }

    protected void wibCourierDeleteClick1(object sender, EventArgs e)
    {
      try
      {
        new RequirementManagementBL().UpdateDeleteDeliverySystemParameter((int) this.ViewState["i_GroupIdCourier"], (int) this.ViewState["i_ParameterIdCourier"], "", "", "", ((SystemUser) this.Session["SystemUser"]).i_SystemUserId, 0, "02");
        Message.SetMessage(this.lblMsgCourier, enmMessageType.Success, "• Se eliminó satisfactoriamente.");
        this.BtnDeleteCourier.Enabled = false;
        this.BtnEditCourier.Enabled = false;
        this.BtnCancelCourier.Enabled = false;
        this.BtnSaveCourier.Enabled = false;
        this.TblCourierDescription.Visible = false;
        this.wdgCourierList.SelectedIndex = -1;
        this.SearchMaintenanceDeliveryCourier();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgCourier, ex);
      }
    }

    private void CreatePopUpServerCourier(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel4, this.UpdatePanel4.GetType(), "Script", script, true);
    }

    protected void wibCourierSaveClick(object sender, EventArgs e)
    {
      try
      {
        if (this.TxtDescriptionCourier.Text.Trim() == "" || this.TxtDescriptionCourier.Text.Trim() == string.Empty)
        {
          this.lblMsgCourier.Visible = true;
          Message.SetMessage(this.lblMsgCourier, enmMessageType.Warning, "Ingrese el nombre del Mensajero");
        }
        else if (this.cboZonaCourier.SelectedValue == "0")
        {
          this.lblMsg.Visible = true;
          Message.SetMessage(this.lblMsgCourier, enmMessageType.Warning, "•Seleccione la Zona");
        }
        else
        {
          SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
          this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperationCourier"];
          string text = this.TxtDescriptionCourier.Text;
          int deliveryCouriers = SystemParameterGroups.DeliveryCouriers;
          int num;
          switch (this.currentOperation)
          {
            case MaintenanceOperation.AddNew:
              int int32_1 = Convert.ToInt32(this.cboZonaCourier.SelectedValue);
              num = new RequirementManagementBL().InsertDeliverySystemParameter(deliveryCouriers, int32_1, text, text, systemUser.i_LocationId.ToString(), systemUser.i_SystemUserId);
              Message.SetMessage(this.lblMsgCourier, enmMessageType.Success, "•Se creó satisfactoriamente.");
              break;
            case MaintenanceOperation.Edit:
              string v_OldValue = this.ViewState["v_OldValue2"].ToString();
              int int32_2 = Convert.ToInt32(this.cboZonaCourier.SelectedValue);
              int i_ParameterId = (int) this.ViewState["i_ParameterIdCourier"];
              num = new RequirementManagementBL().UpdateDeleteDeliverySystemParameter(deliveryCouriers, i_ParameterId, text, text, v_OldValue, systemUser.i_SystemUserId, int32_2, "01");
              Message.SetMessage(this.lblMsgCourier, enmMessageType.Success, "•Se actualizó satisfactoriamente.");
              break;
          }
          this.ViewState["currentOperationCourier"] = (object) MaintenanceOperation.None;
          this.BtnDeleteCourier.Enabled = false;
          this.BtnCancelCourier.Enabled = false;
          this.BtnEditCourier.Enabled = false;
          this.BtnSaveCourier.Enabled = false;
          this.TblCourierDescription.Visible = false;
          this.wdgCourierList.SelectedIndex = -1;
          this.SearchMaintenanceDeliveryCourier();
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgCourier, ex);
      }
    }

    protected void wibCourierCancelClick(object sender, EventArgs e)
    {
      this.ViewState["currentOperationCourier"] = (object) MaintenanceOperation.None;
      this.ViewState["i_GroupIdCourier"] = (object) null;
      this.ViewState["i_ParameterIdCourier"] = (object) null;
      this.BtnNewCourier.Enabled = true;
      this.BtnDeleteCourier.Enabled = false;
      this.BtnEditCourier.Enabled = false;
      this.BtnSaveCourier.Enabled = false;
      this.BtnCancelCourier.Enabled = false;
      this.TblCourierDescription.Visible = false;
      this.wdgCourierList.SelectedIndex = -1;
      this.lblMsgCourier.Visible = false;
      this.SearchMaintenanceDeliveryCourier();
    }

    private void SearchMaintenanceDeliverySchedule()
    {
      if (this.ViewState["currentOperationSchedule"] != null)
        this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperationSchedule"];
      if (this.currentOperation != MaintenanceOperation.Edit)
        this.TblScheduleDescription.Visible = false;
      this.i_GroupId = SystemParameterGroups.BlockSchedule;
      this.SearchMaintenanceDeliveryList(this.i_GroupId, 0, true);
    }

    protected void custPagerManDel_PageChangedSchedule(object sender, CustomPageChangeArgs e)
    {
      int blockSchedule = SystemParameterGroups.BlockSchedule;
      int i_GroupIdDetail = 0;
      int currentPageNumber = this.custPagerManDelSchedule.CurrentPageNumber;
      this.SearchMaintenanceDeliveryList(blockSchedule, i_GroupIdDetail, false);
    }

    protected void wdgScheduleList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
      try
      {
        this.BtnEditSchedule.Enabled = true;
        this.BtnSaveSchedule.Enabled = false;
        this.BtnCancelSchedule.Enabled = true;
        this.BtnDeleteSchedule.Enabled = true;
        this.TblScheduleDescription.Visible = false;
        DataTable dataTable = new DataTable();
        this.objUserBE = this.Session["SystemUser"] as SystemUser;
        int int32 = Convert.ToInt32(e.NewSelectedIndex);
        GridViewRow row = this.wdgScheduleList.Rows[e.NewSelectedIndex];
        this.ViewState["i_GroupIdSchedule"] = (object) Convert.ToInt32(this.wdgScheduleList.DataKeys[int32]["i_GroupId"].ToString());
        this.ViewState["i_ParameterIdSchedule"] = (object) Convert.ToInt32(this.wdgScheduleList.DataKeys[int32]["i_ParameterId"].ToString());
        this.ViewState["v_OldValue3"] = (object) this.wdgScheduleList.DataKeys[int32]["v_OldValue"].ToString();
        string str1 = this.wdgScheduleList.DataKeys[int32]["v_Value"].ToString().Replace(" ", string.Empty);
        this.TxtDescriptionSchedule.Text = HttpUtility.HtmlDecode(row.Cells[3].Text.Trim());
        this.lblMsgSchedule.Visible = false;
        string[] strArray = str1.Split('a');
        string str2 = strArray[0].ToString();
        string str3 = strArray[1].ToString();
        this.Cbh1.SelectedValue = str2.ToString();
        this.Cbh2.SelectedValue = str3.ToString();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgSchedule, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgSchedule, new HandledException(-100, ex));
      }
    }

    protected void wibScheduleEditClick(object sender, EventArgs e)
    {
      this.currentOperation = MaintenanceOperation.Edit;
      this.TblScheduleDescription.Visible = true;
      this.BtnSaveSchedule.Enabled = true;
      this.BtnCancelSchedule.Enabled = true;
      this.ViewState["currentOperationSchedule"] = (object) this.currentOperation;
      this.lblMsgSchedule.Visible = false;
    }

    protected void wibScheduleNewClick(object sender, EventArgs e)
    {
      this.currentOperation = MaintenanceOperation.AddNew;
      this.objUserBE = this.Session["SystemUser"] as SystemUser;
      DataTable dataTable = new DataTable();
      this.wdgScheduleList.SelectedIndex = -1;
      this.TblScheduleDescription.Visible = true;
      this.TxtDescriptionSchedule.Text = "";
      this.TxtDescriptionSchedule.Visible = true;
      this.BtnSaveSchedule.Enabled = true;
      this.BtnEditSchedule.Enabled = false;
      this.BtnCancelSchedule.Enabled = true;
      this.BtnDeleteSchedule.Enabled = false;
      this.ViewState["currentOperationSchedule"] = (object) this.currentOperation;
      this.lblMsgSchedule.Visible = false;
      this.Cbh1.SelectedIndex = 1;
      this.Cbh2.SelectedIndex = 5;
    }

    protected void wibScheduleDeleteClick(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.CreatePopUpServerSchedule("SIIV - Mantenedores Delivery Horario", "../../UserControls/PopupConfirmationDelivery.aspx?MessageTypeId=1&MessageText=¿Desea Eliminar el Registro?&Typemaintainer=4", "350px", "190px");
    }

    protected void wibScheduleDeleteClick1(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        new RequirementManagementBL().UpdateDeleteDeliverySystemParameter((int) this.ViewState["i_GroupIdSchedule"], (int) this.ViewState["i_ParameterIdSchedule"], "", "", systemUser.i_LocationId.ToString(), systemUser.i_SystemUserId, 0, "02");
        Message.SetMessage(this.lblMsgSchedule, enmMessageType.Success, "• Se eliminó satisfactoriamente.");
        this.BtnDeleteSchedule.Enabled = false;
        this.BtnEditSchedule.Enabled = false;
        this.BtnSaveSchedule.Enabled = false;
        this.BtnCancelSchedule.Enabled = false;
        this.TblScheduleDescription.Visible = false;
        this.wdgScheduleList.SelectedIndex = -1;
        this.SearchMaintenanceDeliverySchedule();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgSchedule, ex);
      }
    }

    private void CreatePopUpServerSchedule(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel4, this.UpdatePanel4.GetType(), "Script", script, true);
    }

    protected void wibScheduleSaveClick(object sender, EventArgs e)
    {
      try
      {
        if (this.TxtDescriptionSchedule.Text.Trim() == "" || this.TxtDescriptionSchedule.Text.Trim() == string.Empty)
        {
          this.lblMsgSchedule.Visible = true;
          Message.SetMessage(this.lblMsgSchedule, enmMessageType.Warning, "Ingrese el nombre del Horario");
        }
        else
        {
          SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
          this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperationSchedule"];
          string text = this.TxtDescriptionSchedule.Text;
          int num1 = 1;
          int blockSchedule = SystemParameterGroups.BlockSchedule;
          string v_Value = this.Cbh1.SelectedItem.Text + " a " + this.Cbh2.SelectedItem.Text;
          int num2;
          switch (this.currentOperation)
          {
            case MaintenanceOperation.AddNew:
              num2 = new RequirementManagementBL().InsertDeliverySystemParameter(blockSchedule, num1, v_Value, text, systemUser.i_LocationId.ToString(), systemUser.i_SystemUserId);
              Message.SetMessage(this.lblMsgSchedule, enmMessageType.Success, "•Se creó satisfactoriamente.");
              break;
            case MaintenanceOperation.Edit:
              this.ViewState["v_OldValue3"].ToString();
              int i_ParameterId = (int) this.ViewState["i_ParameterIdSchedule"];
              num2 = new RequirementManagementBL().UpdateDeleteDeliverySystemParameter(blockSchedule, i_ParameterId, text, v_Value, systemUser.i_LocationId.ToString(), systemUser.i_SystemUserId, num1, "01");
              Message.SetMessage(this.lblMsgSchedule, enmMessageType.Success, "•Se actualizó satisfactoriamente.");
              break;
          }
          this.ViewState["currentOperationSchedule"] = (object) MaintenanceOperation.None;
          this.BtnDeleteSchedule.Enabled = false;
          this.BtnEditSchedule.Enabled = false;
          this.BtnSaveSchedule.Enabled = false;
          this.BtnCancelSchedule.Enabled = false;
          this.TblScheduleDescription.Visible = false;
          this.wdgScheduleList.SelectedIndex = -1;
          this.SearchMaintenanceDeliverySchedule();
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgSchedule, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgSchedule, new HandledException(-100, ex));
      }
    }

    protected void wibScheduleCancelClick(object sender, EventArgs e)
    {
      this.ViewState["currentOperationSchedule"] = (object) MaintenanceOperation.None;
      this.ViewState["i_GroupIdSchedule"] = (object) null;
      this.ViewState["i_ParameterIdSchedule"] = (object) null;
      this.BtnDeleteSchedule.Enabled = false;
      this.BtnEditSchedule.Enabled = false;
      this.BtnSaveSchedule.Enabled = false;
      this.BtnCancelSchedule.Enabled = false;
      this.TblScheduleDescription.Visible = false;
      this.wdgScheduleList.SelectedIndex = -1;
      this.lblMsgSchedule.Visible = false;
      this.SearchMaintenanceDeliverySchedule();
    }
  }
}
