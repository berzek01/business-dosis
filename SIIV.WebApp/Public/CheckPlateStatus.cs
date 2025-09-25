// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Public.CheckPlateStatus
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using SIIV.Requirement.BL;
using SIIV.SystemUser.BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Public
{
  public class CheckPlateStatus : Page
  {
    private string strPlateNumber = "";
    private int intRequirementPlateId = 0;
    private DateTime dtstartdate;
    private DateTime dtenddate;
    protected UpdatePanel UpdatePanel;
    protected DropDownList wddTypePlate;
    protected TextBox txtPlateNumber;
    protected HtmlGenericControl divSol;
    protected TextBox txtRequirementPlateId;
    protected FilteredTextBoxExtender txtTitleNumber_FilteredTextBoxExtender;
    protected HtmlTable TblFecha;
    protected Fecha wdpDateIni;
    protected Fecha wdpDateFin;
    protected HtmlGenericControl WebCaptcha1;
    protected Image Image2;
    protected Button BtnRefresh;
    protected TextBox txtimgcode;
    protected HtmlTableCell tagSearch;
    protected Button wibSearch;
    protected HtmlTableCell tagDataVehicle;
    protected Label Label9;
    protected Label lblPlateNew;
    protected Label lblguion;
    protected Label lblDuplicate;
    protected Label Label7;
    protected Label lblPlatePrevious;
    protected Label Label1;
    protected Label lblStatusRequirement;
    protected Label Label11;
    protected Label lblDeliveryPoint;
    protected Label Label12;
    protected Label lblFechaInicio;
    protected Label Label10;
    protected Label lblFechaEntrega;
    protected Label Label3;
    protected Label lblSerialNumber;
    protected Label Label4;
    protected Label lblBrand;
    protected Label Label5;
    protected Label lblModel;
    protected Label Label6;
    protected Label lblOwnerCompleteName;
    protected Label Label2;
    protected Label lblTypeUse;
    protected Label Label8;
    protected Label lblRequirementPlateType;
    protected HtmlTableCell tagRecordPlate;
    protected HtmlGenericControl gridExhibition;
    protected GridView wdgExhibition;
    protected HtmlGenericControl gridRotate;
    protected GridView wdgRotate;
    protected GridView wdgListNew;
    protected Label lblmsn;
    protected Button wibExport;
    protected HtmlTableCell tagNewSearch;
    protected Button wibNewSearch;
    protected Label lblMessage;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      this.ViewState["t"] = (object) Convert.ToString(this.Request.QueryString["t"]);
      (this.Master.FindControl("lblSubTitle") as Label).Text = "Datos Iniciales";
      this.lblMessage.Visible = false;
      this.SetDatePicker();
    }

    protected void wddTypePlate_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (Convert.ToInt16(this.wddTypePlate.SelectedValue) == (short) 2)
      {
        this.divSol.Visible = false;
        this.TblFecha.Style.Add("display", "block");
      }
      else if (Convert.ToInt16(this.wddTypePlate.SelectedValue) == (short) 1)
      {
        this.divSol.Visible = false;
        this.TblFecha.Style.Add("display", "block");
      }
      else
      {
        this.divSol.Visible = true;
        this.TblFecha.Style.Add("display", "none");
      }
      System.Web.UI.ScriptManager.RegisterStartupScript((Page) this, this.GetType(), "CaptchaReload", "$.getScript(\"https://www.google.com/recaptcha/api.js\", function () {});", true);
    }

    private bool ValidateCapcha()
    {
      string str = this.Request["g-recaptcha-response"];
      bool flag = true;
      string userHostAddress = this.Request.UserHostAddress;
      int int16 = (int) Convert.ToInt16(ConfigurationManager.AppSettings["ApplicationId"], (IFormatProvider) CultureInfo.CurrentCulture);
      string score = "";
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(" https://www.google.com/recaptcha/api/siteverify?secret=6Ldeu48dAAAAAKhWRE38_1U2wKWNfetO8ejobNq-&response=" + str);
      try
      {
        using (WebResponse response = httpWebRequest.GetResponse())
        {
          using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
          {
            ReCaptchav3Response captchav3Response = new JavaScriptSerializer().Deserialize<ReCaptchav3Response>(streamReader.ReadToEnd());
            flag = Convert.ToBoolean(captchav3Response.success);
            score = Convert.ToString(captchav3Response.score);
            if (flag && (double) captchav3Response.score < 0.5)
              return flag = false;
          }
        }
        if (flag)
          new SystemUserQueriesBL().InsertPlateQueryAudit("true", userHostAddress, int16, score);
        else
          new SystemUserQueriesBL().InsertPlateQueryAudit("false", userHostAddress, int16, score);
        return flag;
      }
      catch (WebException ex)
      {
        throw ex;
      }
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        this.lblMessage.Visible = false;
        if (Convert.ToInt16(this.wddTypePlate.SelectedValue) == (short) 0)
        {
          if (this.txtPlateNumber.Text.Trim() == "" && this.txtRequirementPlateId.Text.Trim() == "")
          {
            Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Advertencia:<br> Ingrese un Número de Placa o Id Solicitud");
            return;
          }
        }
        else if ((Convert.ToInt16(this.wddTypePlate.SelectedValue) == (short) 1 || Convert.ToInt16(this.wddTypePlate.SelectedValue) == (short) 2) && this.txtPlateNumber.Text.Trim() == "")
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Advertencia:<br> Ingrese un Número de Placa");
          return;
        }
        if (this.Session["CaptchaImageText"].ToString() != this.txtimgcode.Text.ToString())
        {
          this.Image2.ImageUrl = "~/UserControls/FrmCaptcha.aspx";
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Advertencia:<br> Clave no válida.");
          this.txtimgcode.Text = string.Empty;
          this.txtimgcode.Focus();
        }
        else
        {
          if (this.txtPlateNumber.Text.Trim() != "")
            this.strPlateNumber = this.txtPlateNumber.Text.Trim();
          if (this.txtRequirementPlateId.Text.Trim() != "")
            this.intRequirementPlateId = Convert.ToInt32(this.txtRequirementPlateId.Text, (IFormatProvider) CultureInfo.CurrentCulture);
          string opstrStatusDescription = "";
          string opstrObservations = "";
          string opstrSerialNumber = "";
          string opstrBrand = "";
          string opstrModel = "";
          string opstrOwnerCompleteName = "";
          string opstrPlatePrevious = "";
          string opstrPlateNew = "";
          string opstrDescription = "";
          string opstrStatus = "";
          string opstrDeliveryPoint = "";
          string opstrStartDate = "";
          string opstrInsertDate = "";
          string opstrDuplicate = "";
          if (this.ValidateCapcha())
          {
            if (Convert.ToInt16(this.wddTypePlate.SelectedValue) == (short) 0)
            {
              new RequirementQueriesBL().GetRequirementStatus(this.strPlateNumber, this.intRequirementPlateId, out opstrStatusDescription, out opstrObservations, out opstrSerialNumber, out opstrBrand, out opstrModel, out opstrOwnerCompleteName, out opstrPlatePrevious, out opstrPlateNew, out opstrDuplicate, out opstrDescription, out opstrStatus, out opstrDeliveryPoint, out opstrStartDate, out opstrInsertDate);
              if (opstrSerialNumber != "")
              {
                this.lblStatusRequirement.Text = opstrStatusDescription;
                this.lblSerialNumber.Text = opstrSerialNumber;
                this.lblBrand.Text = opstrBrand;
                this.lblModel.Text = opstrModel;
                this.lblOwnerCompleteName.Text = opstrOwnerCompleteName;
                this.lblPlatePrevious.Text = opstrPlatePrevious;
                this.lblPlateNew.Text = opstrPlateNew;
                this.lblTypeUse.Text = opstrDescription;
                this.lblRequirementPlateType.Text = opstrStatus;
                this.lblDeliveryPoint.Text = opstrDeliveryPoint;
                this.lblFechaInicio.Text = opstrStartDate;
                this.lblFechaEntrega.Text = opstrInsertDate;
                this.lblDuplicate.Text = opstrDuplicate;
                this.wddTypePlate.Enabled = false;
                this.txtPlateNumber.Enabled = false;
                this.txtRequirementPlateId.Enabled = false;
                this.WebCaptcha1.Visible = false;
                this.tagSearch.Visible = false;
                this.tagNewSearch.Visible = true;
                this.tagDataVehicle.Visible = true;
                this.lblmsn.Text = "";
              }
              else
              {
                this.lblmsn.Text = "";
                this.wddTypePlate.Enabled = false;
                this.txtPlateNumber.Enabled = false;
                this.txtRequirementPlateId.Enabled = false;
                this.WebCaptcha1.Visible = false;
                this.tagSearch.Visible = false;
                this.tagNewSearch.Visible = true;
                this.tagDataVehicle.Visible = false;
                this.tagRecordPlate.Visible = true;
                this.gridExhibition.Visible = false;
                this.gridRotate.Visible = false;
                Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Advertencia:<br> No se encontró información de la Placa");
              }
            }
            else if (Convert.ToInt16(this.wddTypePlate.SelectedValue) == (short) 2)
            {
              if (Convert.ToInt32(this.wddTypePlate.SelectedValue) == 2)
              {
                this.dtstartdate = Convert.ToDateTime(this.wdpDateIni.Text.ToString());
                this.dtenddate = Convert.ToDateTime(this.wdpDateFin.Text.ToString());
                if (this.dtenddate.Subtract(this.dtstartdate).Days > 360)
                {
                  Message.SetMessage(this.lblMessage, enmMessageType.Warning, "El intervalo de fechas no puede exceder a 360 días");
                  return;
                }
                DataTable dataTable = new VehicleMovementQueriesBL().SpecialPlateVehicleMovementViewReportDealer(this.strPlateNumber, this.dtstartdate, this.dtenddate);
                if (dataTable.Rows.Count > 0)
                {
                  dataTable.Columns.Remove("v_TitleNumber");
                  dataTable.Columns.Remove("v_ReasonSocial");
                  dataTable.Columns.Remove("v_Ubigeo");
                  dataTable.Columns.Remove("v_DocumentNumber");
                  dataTable.Columns.Remove("v_TelephoneRef");
                  dataTable.Columns.Remove("v_Email");
                  dataTable.Columns.Remove("v_SerialNumber");
                  this.ViewState["dt_ResultRotativa"] = (object) dataTable;
                  this.gridExhibition.Visible = false;
                  this.gridRotate.Visible = true;
                  this.wdgRotate.DataSource = (object) dataTable;
                  this.wdgRotate.DataBind();
                  this.wdpDateIni.Enabled = false;
                  this.wdpDateFin.Enabled = false;
                }
                else
                {
                  Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Advertencia:<br> No se encontró información de la Placa");
                  return;
                }
              }
              this.lblmsn.Text = "";
              this.wddTypePlate.Enabled = false;
              this.txtPlateNumber.Enabled = false;
              this.txtRequirementPlateId.Enabled = false;
              this.WebCaptcha1.Visible = false;
              this.tagSearch.Visible = false;
              this.tagNewSearch.Visible = true;
              this.tagDataVehicle.Visible = false;
              this.tagRecordPlate.Visible = true;
            }
            else
            {
              if (Convert.ToInt16(this.wddTypePlate.SelectedValue) != (short) 1)
                return;
              this.dtstartdate = Convert.ToDateTime(this.wdpDateIni.Text.ToString());
              this.dtenddate = Convert.ToDateTime(this.wdpDateFin.Text.ToString());
              if (this.dtenddate.Subtract(this.dtstartdate).Days > 360)
              {
                Message.SetMessage(this.lblMessage, enmMessageType.Warning, "El intervalo de fechas no puede exceder a 360 días");
              }
              else
              {
                this.ViewState["msn"] = (object) "</br> ALERTA: ESTA PLACA NO PUEDE TRANSITAR";
                DataTable dataTable = new VehicleMovementQueriesBL().SpecialPlateUltimateMov(this.strPlateNumber, 7, this.dtstartdate, this.dtenddate);
                string str1 = "";
                string str2 = "";
                string str3 = "";
                if (dataTable.Rows.Count > 0)
                {
                  dataTable.Columns.Remove("v_DocumentNumber");
                  this.ViewState["dt_ResultExhibicion"] = (object) dataTable;
                  this.gridRotate.Visible = false;
                  this.gridExhibition.Visible = true;
                  this.wdgExhibition.DataSource = (object) dataTable;
                  this.wdgExhibition.DataBind();
                  this.wdpDateIni.Enabled = false;
                  this.wdpDateFin.Enabled = false;
                  DateTime dateTime = Convert.ToDateTime(dataTable.Rows[0]["d_TransferDate"], (IFormatProvider) CultureInfo.CurrentCulture);
                  str1 = dateTime.ToShortDateString();
                  str2 = dataTable.Rows[0]["v_StartTime"].ToString();
                  dateTime = Convert.ToDateTime(dataTable.Rows[0]["d_EstimatedTime"], (IFormatProvider) CultureInfo.CurrentCulture);
                  str3 = dateTime.ToString((IFormatProvider) CultureInfo.CurrentCulture);
                  this.ViewState["msn"] = (object) "";
                  this.wddTypePlate.Enabled = false;
                  this.txtPlateNumber.Enabled = false;
                  this.txtRequirementPlateId.Enabled = false;
                  this.WebCaptcha1.Visible = false;
                  this.tagSearch.Visible = false;
                  this.tagNewSearch.Visible = true;
                  this.tagDataVehicle.Visible = false;
                  this.tagRecordPlate.Visible = true;
                }
                else
                  Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Advertencia:<br> No se encontró información de la Placa");
              }
            }
          }
          else
          {
            this.Image2.ImageUrl = "~/UserControls/FrmCaptcha.aspx";
            Message.SetMessage(this.lblMessage, enmMessageType.Error, "ERROR:<br> Solicitud inválida.");
            this.txtimgcode.Text = string.Empty;
            this.txtimgcode.Focus();
          }
        }
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, ex.Message);
      }
    }

    protected void wibExport_Click(object sender, EventArgs e)
    {
      try
      {
        this.ExportData();
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

    private void ExportData()
    {
      DataTable dataTable = new DataTable();
      try
      {
        if (this.ViewState["dt_ResultExhibicion"] != null)
          dataTable = (DataTable) this.ViewState["dt_ResultExhibicion"];
        else if (this.ViewState["dt_ResultRotativa"] != null)
          dataTable = (DataTable) this.ViewState["dt_ResultRotativa"];
        if (dataTable == null || dataTable.Rows.Count == 0)
          throw new HandledException(1, "No se encontró información con los valores ingresados.");
        this.Session["dtExport"] = (object) dataTable;
        this.Export();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, ex.Message);
      }
    }

    private void Export()
    {
      string script = "ExportExcelAll();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel, this.UpdatePanel.GetType(), "Script", script, true);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable objDataTable = (DataTable) this.Session["dtExport"];
        ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
        List<ClassColumns> classColumnsList = new List<ClassColumns>();
        if (this.ViewState["dt_ResultExhibicion"] != null)
        {
          foreach (DataControlField column in (StateManagedCollection) this.wdgExhibition.Columns)
          {
            if (column.Visible && column.GetType().Name == "BoundField")
            {
              BoundField boundField = (BoundField) column;
              classColumnsList.Add(new ClassColumns(boundField.DataField, 1, (int) column.ControlStyle.Width.Value, boundField.HeaderText));
            }
          }
        }
        else if (this.ViewState["dt_ResultRotativa"] != null)
        {
          foreach (DataControlField column in (StateManagedCollection) this.wdgRotate.Columns)
          {
            if (column.Visible && column.GetType().Name == "BoundField")
            {
              BoundField boundField = (BoundField) column;
              classColumnsList.Add(new ClassColumns(boundField.DataField, 1, (int) column.ControlStyle.Width.Value, boundField.HeaderText));
            }
          }
        }
        exportToExcelDataGrid.clsTitle = classColumnsList;
        exportToExcelDataGrid.AgregarHojaLibro(objDataTable, "Consulta Placas");
        exportToExcelDataGrid.CerrarLibro();
        byte[] buffer = exportToExcelDataGrid.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=ConsultaPlacas.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
      }
      catch (Exception ex)
      {
      }
    }

    protected void wibNewSearch_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("~/Public/CheckPlateStatus.aspx", false);
    }

    private void SetDatePicker()
    {
      this.wdpDateIni.Text = DateTime.Now.AddDays(-15.0).ToString();
      this.wdpDateFin.Text = DateTime.Now.ToString();
    }

    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
      this.Image2.ImageUrl = "~/UserControls/FrmCaptcha.aspx";
    }
  }
}
