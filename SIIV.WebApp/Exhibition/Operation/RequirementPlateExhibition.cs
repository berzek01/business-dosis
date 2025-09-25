// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Operation.RequirementPlateExhibition
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Operation
{
  public class RequirementPlateExhibition : Page
  {
    private AcquisitionManagementBL objAcquisitionManagementBL;
    private int i_PlateTypeId = 0;
    private int i_ProductId;
    protected Label LTab1;
    protected Label LTab2;
    protected UpdatePanel UpdatePanel1;
    protected Label Label4;
    protected TextBox txtPlaca;
    protected Label Label1;
    protected Fecha wdpStartDate;
    protected Label Label2;
    protected Fecha wdpEndDate;
    protected Button wibSearch;
    protected Button wibAcquisition;
    protected GridView wdgList;
    protected Pager custPagerRSP;
    protected Label lblCount;
    protected Label lblMessage;
    protected UpdatePanel UpdatePanel2;
    protected Label Label30;
    protected DropDownList wddVehicleType;
    protected Label Label3;
    protected TextBox txtPlateNumber;
    protected Button wibAssigned;
    protected Button wibPrint;
    protected Label Label5;
    protected TextBox txtAmount;
    protected Label Label6;
    protected GridView wdgPlateList;
    protected Button btnReturnPopupConfirmation;
    protected Label lblCountAcquisition;
    protected Button wibReturn;
    protected Button btExport;
    protected HiddenField HiddenField1;
    protected Label lblMessage0;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        DataTable dt_Result1 = new DataTable("Datos");
        this.TableColumns(dt_Result1);
        DataRow row1 = dt_Result1.NewRow();
        dt_Result1.Rows.Add(row1);
        this.wdgList.DataSource = (object) dt_Result1;
        this.wdgList.DataBind();
        this.wdgList.Rows[0].Visible = false;
        DataTable dt_Result2 = new DataTable("Datos2");
        this.TableColumns2(dt_Result2);
        DataRow row2 = dt_Result2.NewRow();
        dt_Result2.Rows.Add(row2);
        this.wdgPlateList.DataSource = (object) dt_Result2;
        this.wdgPlateList.DataBind();
        this.wdgPlateList.Rows[0].Visible = false;
        this.ViewState["i_PlateTypeId"] = (object) this.Request.QueryString["t"].ToString();
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.LoadParameters();
        this.LoadTabTitle(this.i_PlateTypeId);
        string script = UtilDA.ActiveTabIndex("tabs", 1, "0");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        DataTable dataTable = new AcquisitionQueriesBL().ExhibitionPlatesRenewalComunication(this.ViewState["i_PlateTypeId"].ToString());
        Convert.ToDateTime(dataTable.Rows[0]["FecIni"].ToString());
        Convert.ToDateTime(dataTable.Rows[0]["FecFin"].ToString());
        this.SetEnabled(true, true, true, true, true, true);
        if (this.Session["SystemUser"] == null)
          throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RequirementPlateExhibition.aspx");
        if (new AcquisitionQueriesBL().SpecialPlateReportPlatesAcquisition(((SystemUser) this.Session["SystemUser"]).i_AssociatedId, 0, this.i_PlateTypeId).Rows.Count > 0)
          this.wibPrint.Enabled = true;
        this.wdpStartDate.Value = DateTime.Now;
        this.wdpEndDate.Value = DateTime.Now;
        this.wibAssigned.Visible = true;
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

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchSpecialPlate();

    protected void TableColumns(DataTable dt_Result)
    {
      dt_Result.Columns.Add("i_RequirementId", typeof (int));
      dt_Result.Columns.Add("d_RegisterDate", typeof (DateTime));
      dt_Result.Columns.Add("v_UniqueCode", typeof (string));
      dt_Result.Columns.Add("f_Quantity", typeof (float));
      dt_Result.Columns.Add("i_Status", typeof (int));
      dt_Result.Columns.Add("v_Status", typeof (string));
    }

    protected void TableColumns2(DataTable dt_Result)
    {
      dt_Result.Columns.Add("v_PlateNew", typeof (string));
      dt_Result.Columns.Add("v_PaymentCode", typeof (string));
      dt_Result.Columns.Add("v_Description", typeof (string));
      dt_Result.Columns.Add("i_RequirementId", typeof (int));
      dt_Result.Columns.Add("i_RequirementPlateId", typeof (int));
    }

    protected void wibAcquisition_Click(object sender, EventArgs e)
    {
      try
      {
        string script = UtilDA.ActiveTabIndex("tabs", 1, "0");
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
        this.wibAssigned.Enabled = true;
        this.LoadParameters();
        this.ClearControls();
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        if (this.Session["SystemUser"] == null)
          throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RequirementPlateExhibition.aspx");
        if (new AcquisitionQueriesBL().SpecialPlateReportPlatesAcquisition(((SystemUser) this.Session["SystemUser"]).i_AssociatedId, 0, this.i_PlateTypeId).Rows.Count != 0)
          return;
        this.wibPrint.Enabled = false;
        this.lblCountAcquisition.Text = Constants.SEARCHRESULT_OK.Replace("XX", "0");
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

    protected void wdgList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        int int32_1 = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Read")
        {
          int int32_2 = Convert.ToInt32((this.wdgList.Rows[int32_1] ?? throw new HandledException(4, "Error de selección.", "'wdgList' - RequirementPlateExhibition.aspx")).Cells[2].Text);
          this.i_ProductId = this.SetProductId(this.i_PlateTypeId);
          this.ViewState["i_RequirementId"] = (object) int32_2;
          DataTable detail = new AcquisitionQueriesBL().SpecialPlateAcquisitionGetDetail(int32_2, this.i_ProductId, this.i_PlateTypeId);
          if (detail == null || detail.Rows.Count == 0)
            throw new HandledException(1, "No se encontraron registros.");
          this.ListPlateDetail(detail);
        }
        if (!(e.CommandName == "DeleteSol"))
          return;
        this.objAcquisitionManagementBL = new AcquisitionManagementBL();
        if (this.Request.Form["confirm_value"] == "Yes")
        {
          GridViewRow row = this.wdgList.Rows[int32_1];
          if (row == null)
            throw new HandledException(4, "Error de selección.", "'wdgList' - RequirementPlateExhibition.aspx");
          SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RequirementPlateExhibition.aspx");
          int int32_3 = Convert.ToInt32(row.Cells[2].Text);
          int i_RequirementPlateId = 0;
          int i_Status = -1;
          int iSystemUserId = systemUser.i_SystemUserId;
          DateTime now = DateTime.Now;
          if (this.objAcquisitionManagementBL.SpecialPlateAcquisitionDetailDelete(int32_3, i_RequirementPlateId, i_Status, iSystemUserId, now) > 0)
          {
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(2, "Solicitud " + int32_3.ToString() + " fue anulada correctamente."));
            this.SearchSpecialPlate();
          }
          else
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "Solicitud " + int32_3.ToString() + " no pudo ser anulada."));
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

    protected void txtPlateNumber_TextChanged(object sender, EventArgs e)
    {
      if (!(this.txtPlateNumber.Text != ""))
        return;
      this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
      Convert.ToInt32(new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(this.i_PlateTypeId).Rows[0]["i_WarehouseId"].ToString());
      DataTable dataTable1 = new DataTable();
      DataTable dataTable2 = new DataTable();
      double pdecPriceTotal;
      new AcquisitionQueriesBL().SpecialPlateAcquisitionGetPrice(Convert.ToInt32(((DataTable) this.ViewState["dt_Result"]).AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => x["i_ParameterId"].ToString() == this.wddVehicleType.SelectedValue.ToString())).CopyToDataTable<DataRow>().Rows[0]["i_ProductId"].ToString()), out pdecPriceTotal);
      this.ViewState["f_PriceTotal"] = (object) Math.Round(pdecPriceTotal, 2);
      Decimal num = Convert.ToDecimal(this.ViewState["f_PriceTotal"]);
      this.txtAmount.Text = (Convert.ToDecimal(this.txtPlateNumber.Text) * num).ToString("0.00");
    }

    protected void wibAssigned_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.wddVehicleType.SelectedValue.ToString() == "0")
          throw new HandledException(1, "Seleccione un tipo de placa a solicitar. </br> RECUERDE: 'Liviano / Pesado' son para vehiculos de 4 ruedas(Auto, Camión, etc). </br> 'Menor' son para vehiculos de 2,3 ruedas(Moto, Mototaxi)");
        if (this.txtPlateNumber.Text == "")
          throw new HandledException(1, "</br>Ingrese cantidad a adquirir");
        if (this.HiddenField1.Value == "1")
          return;
        List<SIIV.BE.SystemParameter> systemParameterList = new SystemParameterManagementBL().Get((object) new ArrayList()
        {
          (object) ("" + SystemParameterGroups.ConfigurationExhibitionDate.ToString()),
          (object) "",
          (object) "1",
          (object) "1"
        });
        DateTime dateTime1 = DateTime.Now;
        DateTime dateTime2 = DateTime.Now;
        foreach (SIIV.BE.SystemParameter systemParameter in systemParameterList)
        {
          if (systemParameter.i_ParameterId == 1)
          {
            string str1 = systemParameter.v_Value.Substring(0, 4).ToString();
            string str2 = systemParameter.v_Value.Substring(4, 2).ToString();
            dateTime1 = Convert.ToDateTime(systemParameter.v_Value.Substring(6, 2).ToString() + "/" + str2 + "/" + str1);
          }
          if (systemParameter.i_ParameterId == 3)
          {
            string str3 = systemParameter.v_Value.Substring(0, 4).ToString();
            string str4 = systemParameter.v_Value.Substring(4, 2).ToString();
            dateTime2 = Convert.ToDateTime(systemParameter.v_Value.Substring(6, 2).ToString() + "/" + str4 + "/" + str3);
          }
        }
        if (dateTime1 < DateTime.Now && dateTime2 > DateTime.Now)
        {
          string empty = string.Empty;
          this.CreatePopUpServer("SIIV-Exhibition", "../../UserControls/PopupConfirmation.aspx?MessageTypeId=1&MessageText=  ******* IMPORTANTE *******  La adquisición de la placa(s) es hasta 31/12/" + DateTime.Now.Year.ToString(), "350px", "190px");
          this.Session["i_quantityplate"] = (object) Convert.ToInt32(this.txtPlateNumber.Text);
          this.Session["i_vehicletypeid"] = (object) Convert.ToInt32(this.wddVehicleType.SelectedValue);
        }
        else
        {
          Convert.ToInt32(this.txtPlateNumber.Text);
          Convert.ToInt32(this.wddVehicleType.SelectedValue);
          this.AssignedPlate(Convert.ToInt32(this.txtPlateNumber.Text), Convert.ToInt32(this.wddVehicleType.SelectedValue));
        }
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage0, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage0, new HandledException(-100, ex));
      }
    }

    protected void btExport_Click(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        DataTable dataTable = new AcquisitionQueriesBL().SpecialPlateReportPlatesAcquisition(systemUser.i_AssociatedId, 0, this.i_PlateTypeId);
        Convert.ToDateTime(dataTable.Rows[0]["d_PaymentDeadline"]);
        int int32 = Convert.ToInt32(systemUser.i_IsAssociatedAAP.ToString());
        if (!dataTable.Columns.Contains("v_Message"))
          dataTable.Columns.Add("v_Message", Type.GetType("System.String"));
        ReportDocument reportDocument = new ReportDocument();
        string filename;
        if (this.i_PlateTypeId == 7)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            string empty = string.Empty;
            string str = int32 == 2 ? ConfigurationManager.AppSettings["_strcadenaAdquisicionExhibitionImportador"].ToString().Replace("\\n", Environment.NewLine) : ConfigurationManager.AppSettings["_strcadenaAdquisicionExhibition"].ToString().Replace("\\n", Environment.NewLine);
            row["v_Message"] = (object) str.ToString();
          }
          filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportExhibition.rpt";
        }
        else
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            string empty = string.Empty;
            string str = int32 == 2 ? ConfigurationManager.AppSettings["_strcadenaAdquisicionRotativasImportador"].ToString().Replace("\\n", Environment.NewLine) : ConfigurationManager.AppSettings["_strcadenaAdquisicionRotativas"].ToString().Replace("\\n", Environment.NewLine);
            row["v_Message"] = (object) str.ToString();
          }
          filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportRotate.rpt";
        }
        reportDocument.Load(filename);
        reportDocument.SetDataSource(dataTable);
        reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "CodigoPago");
      }
      catch (Exception ex)
      {
        string message = ex.Message;
      }
    }

    protected void wdgPlateList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void wibReturn_Click(object sender, EventArgs e)
    {
      string script = UtilDA.ActiveTabIndex("tabs", 0, "1");
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script, true);
    }

    protected void custPagerRSP_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        DateTime dateTime = Convert.ToDateTime(this.wdpStartDate.Value);
        string shortDateString1 = dateTime.ToShortDateString();
        dateTime = Convert.ToDateTime(this.wdpEndDate.Value);
        string shortDateString2 = dateTime.ToShortDateString();
        this.i_ProductId = this.SetProductId(this.i_PlateTypeId);
        this.SearchSpecialPlateList(shortDateString1, shortDateString2, this.txtPlaca.Text.Trim(), systemUser.i_AssociatedId, this.i_ProductId, false);
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

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      try
      {
        this.objAcquisitionManagementBL = new AcquisitionManagementBL();
        if (this.Session["v_plate"] != null && this.Session["i_RequirementId"] != null && this.Session["i_RequirementPlateId"] != null)
        {
          SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
          this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
          string str = this.Session["v_plate"].ToString();
          int int32_1 = Convert.ToInt32(this.Session["i_RequirementId"].ToString());
          int int32_2 = Convert.ToInt32(this.Session["i_RequirementPlateId"].ToString());
          int i_Status = -1;
          int iSystemUserId = systemUser.i_SystemUserId;
          DateTime now = DateTime.Now;
          this.i_ProductId = this.SetProductId(this.i_PlateTypeId);
          if (this.objAcquisitionManagementBL.SpecialPlateAcquisitionDetailDelete(int32_1, int32_2, i_Status, iSystemUserId, now) > 0)
          {
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Success, "Placa " + str + " fue anulada correctamente");
            this.ListPlateDetail(new AcquisitionQueriesBL().SpecialPlateAcquisitionGetDetail(int32_1, this.i_ProductId, this.i_PlateTypeId));
            this.LoadParameters();
          }
          else
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Placa " + str + " no pudo ser anulada");
          this.Session.Remove("v_plate");
          this.Session.Remove("i_RequirementId");
          this.Session.Remove("i_RequirementPlateId");
        }
        if (this.Session["i_quantityplate"] == null || this.Session["i_vehicletypeid"] == null)
          return;
        this.AssignedPlate(Convert.ToInt32(this.Session["i_quantityplate"]), Convert.ToInt32(this.Session["i_vehicletypeid"]));
        this.LoadParameters();
        this.Session.Remove("i_quantityplate");
        this.Session.Remove("i_vehicletypeid");
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

    private void SetEnabled(
      bool IsTypeVehicle,
      bool IsCant,
      bool IsTotal,
      bool IsAdquisicion,
      bool IsPrint,
      bool IsBack)
    {
      this.wddVehicleType.Enabled = IsTypeVehicle;
      this.txtPlateNumber.Enabled = IsCant;
      this.txtAmount.Enabled = IsTotal;
      this.wibAssigned.Enabled = IsAdquisicion;
      this.wibPrint.Enabled = IsPrint;
      this.wibReturn.Enabled = IsBack;
    }

    private void LoadParameters()
    {
      try
      {
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        int int32 = Convert.ToInt32(new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(this.i_PlateTypeId).Rows[0]["i_WarehouseId"].ToString());
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = new AcquisitionQueriesBL().SpecialPlateClassVehicleGet(this.i_PlateTypeId, int32);
        this.wddVehicleType.DataSource = (object) dataTable2;
        this.wddVehicleType.DataTextField = "v_Description";
        this.wddVehicleType.DataValueField = "i_ParameterId";
        this.wddVehicleType.DataBind();
        this.wddVehicleType.SelectedIndex = 0;
        this.wddVehicleType.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        this.ViewState["dt_Result"] = (object) dataTable2;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchSpecialPlate()
    {
      try
      {
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.i_ProductId = this.SetProductId(this.i_PlateTypeId);
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        DateTime dateTime = Convert.ToDateTime(this.wdpStartDate.Value);
        string shortDateString1 = dateTime.ToShortDateString();
        dateTime = Convert.ToDateTime(this.wdpEndDate.Value);
        string shortDateString2 = dateTime.ToShortDateString();
        this.SearchSpecialPlateList(shortDateString1, shortDateString2, this.txtPlaca.Text.Trim(), systemUser.i_AssociatedId, this.i_ProductId, true);
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

    private void SearchSpecialPlateList(
      string pdtStartDate,
      string pdtEndDate,
      string pv_Plate,
      int pintAssociatedId,
      int i_ProductId,
      bool pboolLoadPager)
    {
      try
      {
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerRSP.CurrentPageNumber;
        int pintMaxRows = this.custPagerRSP.CurrentPageSize == 0 ? 10 : this.custPagerRSP.CurrentPageSize;
        int pintTotalRows;
        DataTable all = new AcquisitionQueriesBL().SpecialPlateAcquisitionGetAll(pdtStartDate, pdtEndDate, pv_Plate, pintAssociatedId, i_ProductId, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        if (all == null || all.Rows.Count == 0)
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
        int num = pintTotalRows;
        this.wdgList.DataSource = (object) all;
        this.wdgList.DataBind();
        this.custPagerRSP.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
        this.custPagerRSP.TotalRecordCount = pintTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerRSP.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ClearControls()
    {
      this.Session["ListPlate"] = (object) null;
      this.wddVehicleType.Enabled = true;
      this.txtPlateNumber.Text = string.Empty;
      this.txtAmount.Text = string.Empty;
      this.txtPlateNumber.Enabled = true;
      this.txtAmount.Enabled = true;
      this.wdgPlateList.AutoGenerateColumns = false;
      DataTable dataTable = new DataTable();
      this.wdgPlateList.DataSource = (object) (DataTable) this.Session["ListPlate"];
      this.wdgPlateList.DataBind();
    }

    private void AssignedPlate(int i_quantityplate, int i_vehicletypeid)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RequirementPlateExhibition.aspx");
        int i_ProcessTypeId = 1;
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        int int32 = Convert.ToInt32(new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(this.i_PlateTypeId).Rows[0]["i_WarehouseId"].ToString());
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = new AcquisitionManagementBL().SpecialPlateAcquisitionAssignedPlate(i_vehicletypeid, systemUser.i_AssociatedId, DateTime.Now, i_quantityplate, systemUser.i_SystemUserId, i_ProcessTypeId, this.i_PlateTypeId, this.i_PlateTypeId, int32);
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(2, "Se realizó correctamente la adquisición de las placas. </br> FAVOR DE IMPRIMIR EL REPORTE DE CÓDIGOS DE PAGO"));
        this.wdgPlateList.AutoGenerateColumns = false;
        this.wdgPlateList.DataSource = (object) dataTable2;
        this.wdgPlateList.DataBind();
        this.lblCountAcquisition.Text = Constants.SEARCHRESULT_OK.Replace("XX", dataTable2.Rows.Count.ToString());
        this.wibPrint.Enabled = true;
        this.Session["ListPlate"] = (object) dataTable2;
        this.txtPlateNumber.Text = "";
        this.txtAmount.Text = "";
        this.LoadParameters();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ListPlateDetail(DataTable dtresult)
    {
      try
      {
        if (dtresult.Rows.Count > 0)
        {
          this.wddVehicleType.SelectedValue = dtresult.Rows[0]["i_VehicleClassId"].ToString();
          this.txtPlateNumber.Text = dtresult.Rows[0]["i_NumberPlates"].ToString();
          this.txtAmount.Text = dtresult.Rows[0]["f_TotalPrice"].ToString();
          this.wibAssigned.Enabled = false;
          this.wddVehicleType.Enabled = false;
          this.txtPlateNumber.Enabled = false;
          this.txtAmount.Enabled = false;
        }
        else
        {
          this.wddVehicleType.Enabled = false;
          this.txtPlateNumber.Text = string.Empty;
          this.txtAmount.Text = string.Empty;
          this.txtPlateNumber.Enabled = false;
          this.txtAmount.Enabled = false;
        }
        this.wdgPlateList.AutoGenerateColumns = false;
        this.wdgPlateList.DataSource = (object) dtresult;
        this.wdgPlateList.DataBind();
        this.lblCountAcquisition.Text = Constants.SEARCHRESULT_OK.Replace("XX", dtresult.Rows.Count.ToString());
        string script = UtilDA.ActiveTabIndex("tabs", 1, "");
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
        this.Session["ListPlate"] = (object) dtresult;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadTabTitle(int i_PlateTypeId)
    {
      try
      {
        switch (i_PlateTypeId)
        {
          case 6:
            this.LTab1.Text = "Lista Solicitudes";
            this.LTab2.Text = "Solicitud Rotativa Aduana";
            break;
          case 7:
            this.LTab1.Text = "Lista Solicitudes";
            this.LTab2.Text = "Solicitud Exhibición";
            break;
          case 11:
            this.LTab1.Text = "Lista Solicitudes";
            this.LTab2.Text = "Solicitud Rotativa Dealer";
            break;
        }
      }
      catch (Exception ex)
      {
        throw ex;
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

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format("OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script, true);
    }

    private void CreatePopUpServer1(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format("OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
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

    protected void wddVehicleType_SelectionChanged(object sender, EventArgs e)
    {
      if (this.wddVehicleType.SelectedValue == "-1" || this.wddVehicleType.SelectedValue == "0")
        return;
      this.txtPlateNumber.Text = string.Empty;
      this.txtAmount.Text = string.Empty;
    }
  }
}
