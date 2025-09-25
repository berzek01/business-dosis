// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Delivery.ReportDeliveryPlateStatus
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
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
  public class ReportDeliveryPlateStatus : Page
  {
    private SystemUser objUserBE;
    protected UpdatePanel UpdatePanel1;
    protected Label Label3;
    protected TextBox txtRequirement;
    protected FilteredTextBoxExtender txtRequirementPlateId_FilteredTextBoxExtender1;
    protected Label Label1;
    protected TextBox txtPlaca;
    protected FilteredTextBoxExtender txtPlaca_FilteredTextBoxExtender;
    protected Label Label2;
    protected DropDownList wddStatusDelivery;
    protected HtmlTable TblFecha;
    protected CheckBox chkDate;
    protected Label Label5;
    protected Fecha wdpStartDate;
    protected Label Label8;
    protected Fecha wdpEndDate;
    protected Button wibSearch;
    protected Button wibExport;
    protected GridView wdgList;
    protected GridView wdgListNew;
    protected Pager custPagerAP;
    protected Label lblMessage;
    protected UpdatePanel UpdatePanel2;
    protected TextBox txtPlateNew;
    protected GridView wdgDetalle;
    protected Button wibReturn;
    protected Button Button1;
    protected UpdatePanel UpdatePanel3;
    protected HtmlTableCell tdTitle;
    protected HtmlGenericControl DivDatosDelivery;
    protected DropDownList wddDeliveryDescription;
    protected TextBox txtDeliveryDescription;
    protected DropDownList wddDeliveryDescription2;
    protected TextBox txtDeliveryDescription2;
    protected DropDownList wddDistrict;
    protected Label LblDistrictStatus;
    protected Label LblDistricMessage;
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
    protected Label lblMessageData;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadParameters();
      this.SetDatePicker();
      string script = UtilDA.ActiveTabIndex("tabs", 0, "1,2");
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void chkDate_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkDate.Checked)
      {
        this.wdpStartDate.Enabled = true;
        this.wdpEndDate.Enabled = true;
        this.SetDatePicker();
      }
      else
      {
        this.wdpStartDate.Enabled = false;
        this.wdpEndDate.Enabled = false;
      }
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchDeliveryPlates();

    protected void custPagerAP_PageChanged(object sender, CustomPageChangeArgs e)
    {
      this.objUserBE = this.Session["SystemUser"] as SystemUser;
      int i_RequirementPlate = -1;
      if (this.txtRequirement.Text.Trim() != "")
        i_RequirementPlate = Convert.ToInt32(this.txtRequirement.Text);
      string v_Plate = this.txtPlaca.Text.Trim();
      int int32 = Convert.ToInt32(this.wddStatusDelivery.SelectedValue);
      DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
      DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
      int i_Flag = 1;
      if (!this.chkDate.Checked)
        i_Flag = 0;
      this.SearchDeliveryPlatesList(this.objUserBE.i_SystemUserId, i_RequirementPlate, v_Plate, int32, dateTime1, dateTime2, i_Flag, false);
    }

    protected void wdgList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      int int32_1 = Convert.ToInt32(e.CommandArgument);
      GridViewRow row = this.wdgList.Rows[int32_1];
      int int32_2 = Convert.ToInt32(row.Cells[2].Text.ToString());
      int int32_3 = Convert.ToInt32(row.Cells[12].Text.ToString());
      int int32_4 = Convert.ToInt32(row.Cells[13].Text.ToString());
      string str1 = row.Cells[3].Text.ToString();
      string str2 = HttpUtility.HtmlDecode(row.Cells[8].Text.ToString());
      this.Session["i_RequiremetPlateDelivery"] = (object) int32_2;
      this.Session["i_RequirementProgramation"] = (object) int32_4;
      this.Session["v_PlateNew"] = (object) str1;
      if (e.CommandName.Equals("getData"))
      {
        if (row == null)
          throw new HandledException(4, "Error de selección.", "'wdgList' - ReportDeliveryPlateStatus.aspx");
        this.CargarDetalle(int32_2, int32_1);
        string script = UtilDA.ActiveTabIndex("tabs", 1, "0,2");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
      if (!e.CommandName.Equals("editData"))
        return;
      if (int32_3 == 5 || int32_3 == 4)
      {
        this.lblMessage.Visible = true;
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "El estado de la solicitud no permite editar los datos");
      }
      else if (str2 != " ")
      {
        this.lblMessage.Visible = true;
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "La solicitud no puede ser editada por que ya tiene asignada un courier");
      }
      else
      {
        this.CargarData(int32_2);
        this.SaveDelivery.Enabled = true;
        this.txtDeliveryDescription.Enabled = true;
        this.txtDeliveryDescription2.Enabled = true;
        this.txtReference.Enabled = true;
        this.txtMailDelivery.Enabled = true;
        this.txtTelephoneDelivery.Enabled = true;
        this.txtTelephoneMovilDelivery.Enabled = true;
        this.wddDeliveryDescription2.Enabled = true;
        this.wddDeliveryDescription.Enabled = true;
        this.wddDistrict.Enabled = true;
        string script = UtilDA.ActiveTabIndex("tabs", 2, "0,1");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
    }

    protected void wddDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.lblMessageData.Visible = false;
      int int32 = Convert.ToInt32(this.wddDistrict.SelectedValue.ToString());
      if (Convert.ToInt32(this.Session["i_Disctric"].ToString()) != int32)
      {
        this.LblDistricMessage.Visible = true;
        this.Session["i_statusDistrict"] = (object) 1;
      }
      else
      {
        this.LblDistricMessage.Visible = false;
        this.Session["i_statusDistrict"] = (object) 0;
      }
    }

    protected void wibReturn_Click(object sender, EventArgs e)
    {
      this.LimpiarCampos();
      this.lblMessage.Text = "";
      string script = UtilDA.ActiveTabIndex("tabs", 0, "1,2");
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void wibExport_Click(object sender, EventArgs e)
    {
      try
      {
        this.SearchDeliveryPlatesReport();
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

    protected void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable objDataTable = (DataTable) this.Session["dtExport"];
        ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
        List<ClassColumns> classColumnsList = new List<ClassColumns>();
        foreach (DataControlField column in (StateManagedCollection) this.wdgList.Columns)
        {
          if (column.Visible && column.HeaderText != "RequirementProgramation" && column.GetType().Name == "BoundField")
          {
            BoundField boundField = (BoundField) column;
            classColumnsList.Add(new ClassColumns(boundField.DataField, 1, (int) column.ControlStyle.Width.Value, boundField.HeaderText));
          }
        }
        exportToExcelDataGrid.clsTitle = classColumnsList;
        exportToExcelDataGrid.AgregarHojaLibro(objDataTable, "Reporte");
        exportToExcelDataGrid.CerrarLibro();
        byte[] buffer = exportToExcelDataGrid.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=Reporte.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
        this.Session.Remove("dtExport");
      }
      catch (Exception ex)
      {
      }
    }

    public void LoadParameters()
    {
      this.wddStatusDelivery.DataSource = (object) new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.SpecialStatusDelivery.ToString(),
        (object) "0,1,2,3,4,5",
        (object) "1",
        (object) "1"
      });
      this.wddStatusDelivery.DataTextField = "v_Description";
      this.wddStatusDelivery.DataValueField = "i_ParameterId";
      this.wddStatusDelivery.DataBind();
      this.wddStatusDelivery.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Todos --", "-1"));
      this.wddStatusDelivery.SelectedIndex = 0;
    }

    private void SetDatePicker()
    {
      this.wdpStartDate.Value = DateTime.Now.AddMonths(-1);
      this.wdpEndDate.Value = DateTime.Now;
    }

    private void SearchDeliveryPlates()
    {
      this.lblMessage.Visible = false;
      try
      {
        this.objUserBE = this.Session["SystemUser"] as SystemUser;
        int i_RequirementPlate = -1;
        if (this.txtRequirement.Text.Trim() != "")
          i_RequirementPlate = Convert.ToInt32(this.txtRequirement.Text.Trim(), (IFormatProvider) CultureInfo.CurrentCulture);
        string v_Plate = this.txtPlaca.Text.Trim();
        int int32 = Convert.ToInt32(this.wddStatusDelivery.SelectedValue);
        DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
        DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
        int i_Flag = 1;
        if (!this.chkDate.Checked)
          i_Flag = 0;
        this.SearchDeliveryPlatesList(this.objUserBE.i_SystemUserId, i_RequirementPlate, v_Plate, int32, dateTime1, dateTime2, i_Flag, true);
        this.HidePopup();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, ex.Message);
        this.HidePopup();
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SearchDeliveryPlatesList(
      int i_SystemUserId,
      int i_RequirementPlate,
      string v_Plate,
      int i_Status,
      DateTime d_StartDate,
      DateTime d_EndDate,
      int i_Flag,
      bool pboolLoadPager)
    {
      int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerAP.CurrentPageNumber;
      int pintMaxRows = this.custPagerAP.CurrentPageSize == 0 ? 10 : this.custPagerAP.CurrentPageSize;
      int pintTotalRows;
      DataTable dataTable = new RequirementQueriesBL().SearchStatusDelivery(i_SystemUserId, i_RequirementPlate, v_Plate, i_Status, d_StartDate, d_EndDate, i_Flag, pintStartRowIndex, pintMaxRows, out pintTotalRows);
      if (dataTable == null || dataTable.Rows.Count == 0)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
        this.HidePopup();
      }
      else
      {
        this.lblMessage.Visible = false;
        this.wibExport.Enabled = true;
      }
      int num = pintTotalRows;
      this.wdgList.DataSource = (object) dataTable;
      this.wdgList.DataBind();
      this.custPagerAP.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
      this.custPagerAP.TotalRecordCount = pintTotalRows;
      if (!pboolLoadPager)
        return;
      this.custPagerAP.LoadPager();
    }

    public void CargarDetalle(int i_RequiremetPlate, int index)
    {
      try
      {
        this.txtPlateNew.Text = (this.wdgList.Rows[index] ?? throw new HandledException(4, "Error de selección.", "'wdgList' - ReportDeliveryPlateStatus.aspx")).Cells[2].Text.ToString();
        DataTable dataTable = new DataTable();
        this.wdgDetalle.DataSource = (object) new RequirementQueriesBL().SearchStatusDeliveryDetail(i_RequiremetPlate);
        this.wdgDetalle.DataBind();
      }
      catch (Exception ex)
      {
        this.lblMessage.Visible = true;
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO + ex.Message);
      }
    }

    public void LimpiarCampos()
    {
      this.txtRequirement.Text = "";
      this.txtPlaca.Text = "";
      this.wddStatusDelivery.SelectedIndex = 0;
      this.chkDate.Checked = true;
      this.wdpStartDate.Enabled = true;
      this.wdpEndDate.Enabled = true;
    }

    private void SearchDeliveryPlatesReport()
    {
      this.lblMessage.Visible = false;
      try
      {
        this.objUserBE = this.Session["SystemUser"] as SystemUser;
        int i_RequirementPlate = -1;
        if (this.txtRequirement.Text.Trim() != "")
          i_RequirementPlate = Convert.ToInt32(this.txtRequirement.Text.Trim(), (IFormatProvider) CultureInfo.CurrentCulture);
        string v_Plate = this.txtPlaca.Text.Trim();
        int int32 = Convert.ToInt32(this.wddStatusDelivery.SelectedValue);
        DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
        DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
        int i_Flag = 1;
        if (!this.chkDate.Checked)
          i_Flag = 0;
        this.SearchDeliveryPlatesReport(this.objUserBE.i_SystemUserId, i_RequirementPlate, v_Plate, int32, dateTime1, dateTime2, i_Flag, true);
        this.HidePopup();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, ex.Message);
        this.HidePopup();
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SearchDeliveryPlatesReport(
      int i_SystemUserId,
      int i_RequirementPlate,
      string v_Plate,
      int i_Status,
      DateTime dt_Stardate,
      DateTime dt_Enddate,
      int i_Flag,
      bool v)
    {
      int currentPageNumber = this.custPagerAP.CurrentPageNumber;
      int currentPageSize = this.custPagerAP.CurrentPageSize;
      DataTable dataTable1 = new DataTable();
      DataTable dataTable2 = new RequirementQueriesBL().SearchStatusDelivery(i_SystemUserId, i_RequirementPlate, v_Plate, i_Status, dt_Stardate, dt_Enddate, i_Flag, currentPageNumber, currentPageSize, out int _);
      try
      {
        if (dataTable2 == null || dataTable2.Rows.Count == 0)
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
        this.Session["dtExport"] = (object) dataTable2;
        this.Export();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, ex.Message);
      }
    }

    public void getDistrict(
      int i_DeliveryPointId,
      int i_VehicleClasification,
      int i_distric,
      string v_price)
    {
      try
      {
        DataTable dataTable = new DataTable();
        List<DataRow> dataRowList = new List<DataRow>();
        bool flag = false;
        DataTable districtByDeliveryPoint = new RequirementQueriesBL().GetDistrictByDeliveryPoint(i_DeliveryPointId, i_VehicleClasification);
        if (districtByDeliveryPoint.Rows.Count <= 0)
          return;
        foreach (DataRow row in (InternalDataCollectionBase) districtByDeliveryPoint.Rows)
        {
          if (row["f_PriceSale"].ToString() != v_price)
            dataRowList.Add(row);
        }
        foreach (DataRow row in dataRowList)
          districtByDeliveryPoint.Rows.Remove(row);
        foreach (DataRow row in (InternalDataCollectionBase) districtByDeliveryPoint.Rows)
        {
          if (row["i_ParameterId"].ToString() == i_distric.ToString())
            flag = true;
        }
        this.wddDistrict.DataSource = (object) districtByDeliveryPoint;
        this.wddDistrict.DataTextField = "v_Description";
        this.wddDistrict.DataValueField = "i_ParameterId";
        this.wddDistrict.DataBind();
        if (flag)
        {
          this.Session["i_enableDistrict"] = (object) 0;
          this.wddDistrict.SelectedValue = i_distric.ToString();
          this.LblDistrictStatus.Visible = false;
        }
        else
        {
          this.Session["i_enableDistrict"] = (object) 1;
          this.wddDistrict.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --", "0"));
          this.wddDistrict.SelectedIndex = 0;
          this.LblDistrictStatus.Visible = true;
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

    public void CargarData(int i_RequiremetPlate)
    {
      try
      {
        this.lblMessageData.Visible = false;
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = new RequirementQueriesBL().SearchDeliveryData(i_RequiremetPlate);
        int int32_1 = Convert.ToInt32(dataTable2.Rows[0]["i_location"].ToString());
        int int32_2 = Convert.ToInt32(dataTable2.Rows[0]["i_VehicleClasification"].ToString());
        int int32_3 = Convert.ToInt32(dataTable2.Rows[0]["i_DistrictReference"].ToString());
        string v_price = dataTable2.Rows[0]["f_PriceSale"].ToString();
        this.getDistrict(int32_1, int32_2, int32_3, v_price);
        this.Session["i_Disctric"] = (object) int32_3;
        this.wddDistrict_SelectedIndexChanged((object) null, (EventArgs) null);
        this.wddDeliveryDescription.SelectedItem.Text = dataTable2.Rows[0]["v_address"].ToString().Substring(0, 4).Trim();
        this.txtDeliveryDescription.Text = dataTable2.Rows[0]["v_address"].ToString().Substring(4).Trim();
        this.txtDeliveryDescription2.Text = dataTable2.Rows[0]["v_address2"].ToString().Trim();
        this.txtReference.Text = dataTable2.Rows[0]["v_address3"].ToString().Trim();
        this.txtMailDelivery.Text = dataTable2.Rows[0]["v_Email"].ToString();
        this.txtTelephoneDelivery.Text = dataTable2.Rows[0]["v_Telefono"].ToString();
        this.txtTelephoneMovilDelivery.Text = dataTable2.Rows[0]["v_Celular"].ToString();
      }
      catch (Exception ex)
      {
        this.lblMessage.Visible = true;
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO + ex.Message);
      }
    }

    protected void SaveDelivery_Click(object sender, EventArgs e)
    {
      try
      {
        this.objUserBE = this.Session["SystemUser"] as SystemUser;
        int iSystemUserId = this.objUserBE.i_SystemUserId;
        if (this.txtDeliveryDescription.Text.Trim() == "" || this.txtDeliveryDescription2.Text.Trim() == "")
        {
          this.lblMessageData.Visible = true;
          Message.SetMessage(this.lblMessageData, enmMessageType.Warning, "Debe ingresar todos los datos de la dirección");
        }
        else
        {
          if ((int) this.Session["i_enableDistrict"] == 1)
          {
            if (this.wddDistrict.SelectedIndex == 0)
            {
              this.lblMessageData.Visible = true;
              Message.SetMessage(this.lblMessageData, enmMessageType.Warning, "Debe seleccionar un distrito");
              return;
            }
          }
          else
          {
            if (this.txtMailDelivery.Text.Trim() == "")
            {
              this.lblMessageData.Visible = true;
              Message.SetMessage(this.lblMessageData, enmMessageType.Warning, "Debe ingresar el correo para el Servicio Delivery");
              return;
            }
            if (!new Email().IsValidEmail(this.txtMailDelivery.Text.Trim()))
            {
              this.lblMessageData.Visible = true;
              Message.SetMessage(this.lblMessageData, enmMessageType.Warning, "Debe Ingresar un Email Valido");
              return;
            }
            if (this.txtTelephoneDelivery.Text.Trim() == "" && this.txtTelephoneMovilDelivery.Text.Trim() == "___-___-___")
            {
              this.lblMessageData.Visible = true;
              Message.SetMessage(this.lblMessageData, enmMessageType.Warning, "Debe ingresar al menos un teléfono para el Servicio Delivery");
              return;
            }
          }
          string v_Adrress = this.wddDeliveryDescription.SelectedItem.Text + " " + this.txtDeliveryDescription.Text + " " + this.wddDeliveryDescription2.SelectedItem.Text + " " + this.txtDeliveryDescription2.Text + " |" + this.txtReference.Text;
          string text = this.txtMailDelivery.Text;
          string v_Telephone = this.txtTelephoneDelivery.Text.Trim() + "|" + this.txtTelephoneMovilDelivery.Text.Trim();
          int int32_1 = Convert.ToInt32(this.wddDistrict.SelectedValue.ToString());
          int int32_2 = Convert.ToInt32(this.Session["i_statusDistrict"].ToString());
          int int32_3 = Convert.ToInt32(this.Session["i_RequiremetPlateDelivery"].ToString());
          int int32_4 = Convert.ToInt32(this.Session["i_RequirementProgramation"].ToString());
          string v_PlateNew = this.Session["v_PlateNew"].ToString();
          new RequirementManagementBL().UpdateDeleteDeliveryRequirement(int32_2, int32_3, int32_1, v_Adrress, text, v_Telephone, iSystemUserId, int32_4, v_PlateNew);
          Message.SetMessage(this.lblMessageData, enmMessageType.Success, "Se actualizo correctamente los datos de la solicitud delivery.");
          this.SearchDeliveryPlates();
          this.SaveDelivery.Enabled = false;
          this.txtDeliveryDescription.Enabled = false;
          this.txtDeliveryDescription2.Enabled = false;
          this.txtReference.Enabled = false;
          this.txtMailDelivery.Enabled = false;
          this.txtTelephoneDelivery.Enabled = false;
          this.txtTelephoneMovilDelivery.Enabled = false;
          this.wddDeliveryDescription2.Enabled = false;
          this.wddDeliveryDescription.Enabled = false;
          this.wddDistrict.Enabled = false;
        }
      }
      catch (Exception ex)
      {
        this.lblMessageData.Visible = true;
        Message.SetMessage(this.lblMessageData, enmMessageType.Warning, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ERROR_GENERICO + ex.Message);
      }
    }

    private void Export()
    {
      string script = "Export();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
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
