// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.PlateDeliveryManagement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class PlateDeliveryManagement : Page
  {
    private RequirementQueriesBL ObjRequirementQueriesBL;
    private int i_PlateTypeId;
    protected UpdatePanel UpdatePanel1;
    protected HiddenField hdiState;
    protected HiddenField hdiNumDoc;
    protected HiddenField hdiChek;
    protected HiddenField hdiFecIni;
    protected HiddenField hdiFenFin;
    protected TextBox txtBatchNumber;
    protected FilteredTextBoxExtender txtBatchNumber_FilteredTextBoxExtender;
    protected TextBox txtPlateId;
    protected FilteredTextBoxExtender txtPlateId_FilteredTextBoxExtender;
    protected TextBox txtRequirementId;
    protected FilteredTextBoxExtender txtRequirementId_FilteredTextBoxExtender;
    protected DropDownList wddStatusSobre;
    protected CheckBox chkDate;
    protected Fecha wdpDateIni;
    protected Fecha wdpDateFin;
    protected Button wibSearch;
    protected Button btnReturnPopupConfirmation;
    protected Button btnSecondPopup;
    protected GridView wdgPlateDeliverManagement;
    protected Pager custPagerPDM;
    protected Label lblMessage;

    private static int GetQuarter(int nMonth)
    {
      if (nMonth <= 3)
        return 1;
      if (nMonth <= 6)
        return 2;
      return nMonth <= 9 ? 3 : 4;
    }

    private static long Round(double dVal)
    {
      return dVal >= 0.0 ? (long) Math.Floor(dVal) : (long) Math.Ceiling(dVal);
    }

    public static long DateDiff(
      PlateDeliveryManagement.DateInterval interval,
      DateTime dt1,
      DateTime dt2,
      DayOfWeek eFirstDayOfWeek)
    {
      if (interval == PlateDeliveryManagement.DateInterval.Year)
        return (long) (dt2.Year - dt1.Year);
      if (interval == PlateDeliveryManagement.DateInterval.Month)
        return (long) (dt2.Month - dt1.Month + 12 * (dt2.Year - dt1.Year));
      TimeSpan timeSpan = dt2 - dt1;
      if (interval == PlateDeliveryManagement.DateInterval.Day || interval == PlateDeliveryManagement.DateInterval.DayOfYear)
        return PlateDeliveryManagement.Round(timeSpan.TotalDays);
      switch (interval)
      {
        case PlateDeliveryManagement.DateInterval.Hour:
          return PlateDeliveryManagement.Round(timeSpan.TotalHours);
        case PlateDeliveryManagement.DateInterval.Minute:
          return PlateDeliveryManagement.Round(timeSpan.TotalMinutes);
        case PlateDeliveryManagement.DateInterval.Quarter:
          double quarter = (double) PlateDeliveryManagement.GetQuarter(dt1.Month);
          return PlateDeliveryManagement.Round((double) PlateDeliveryManagement.GetQuarter(dt2.Month) - quarter + (double) (4 * (dt2.Year - dt1.Year)));
        case PlateDeliveryManagement.DateInterval.Second:
          return PlateDeliveryManagement.Round(timeSpan.TotalSeconds);
        case PlateDeliveryManagement.DateInterval.Weekday:
          return PlateDeliveryManagement.Round(timeSpan.TotalDays / 7.0);
        case PlateDeliveryManagement.DateInterval.WeekOfYear:
          while (dt2.DayOfWeek != eFirstDayOfWeek)
            dt2 = dt2.AddDays(-1.0);
          while (dt1.DayOfWeek != eFirstDayOfWeek)
            dt1 = dt1.AddDays(-1.0);
          return PlateDeliveryManagement.Round((dt2 - dt1).TotalDays / 7.0);
        default:
          return 0;
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.LoadParameters();
        this.SetDatePicker();
        if (this.Request.QueryString["t"].ToString() == "12")
          this.wdgPlateDeliverManagement.Columns[2].Visible = true;
        if (this.Request.QueryString["v_NumDoc"] != null)
        {
          int result1 = 0;
          int result2 = 0;
          string empty1 = string.Empty;
          string empty2 = string.Empty;
          string empty3 = string.Empty;
          string str1 = this.Request.QueryString["v_NumDoc"];
          if (str1 != string.Empty)
          {
            int.TryParse(this.Request.QueryString["i_status"], out result1);
            int.TryParse(this.Request.QueryString["i_chek"], out result2);
            string str2 = this.Request.QueryString["strFecIni"];
            string str3 = this.Request.QueryString["strFecFin"];
            this.hdiNumDoc.Value = str1;
            this.hdiState.Value = result1.ToString((IFormatProvider) CultureInfo.CurrentCulture);
            this.hdiChek.Value = result2.ToString((IFormatProvider) CultureInfo.CurrentCulture);
            this.hdiFecIni.Value = str2;
            this.hdiFenFin.Value = str3;
            this.wddStatusSobre.SelectedValue = result1.ToString((IFormatProvider) CultureInfo.CurrentCulture);
            this.wdpDateIni.Text = str2;
            this.wdpDateFin.Text = str3;
            if (result2 == 1)
            {
              this.chkDate.Checked = true;
              this.chkDate_CheckedChanged((object) null, (EventArgs) null);
            }
            else
            {
              this.chkDate.Checked = false;
              this.chkDate_CheckedChanged((object) null, (EventArgs) null);
            }
          }
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

    protected void chkDate_CheckedChanged(object sender, EventArgs e)
    {
      this.wdpDateIni.Enabled = this.chkDate.Checked;
      this.wdpDateFin.Enabled = this.chkDate.Checked;
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        this.SearchPDM();
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

    protected void custPagerClaimList_OnPageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        string empty1 = string.Empty;
        string pstrPlate = string.Empty;
        string empty2 = string.Empty;
        string pstrBatchNumber;
        if (this.txtBatchNumber.Text != string.Empty)
        {
          pstrBatchNumber = this.txtBatchNumber.Text.Trim().Length == 10 ? Convert.ToInt32(this.txtBatchNumber.Text.Trim().Substring(3), (IFormatProvider) CultureInfo.CurrentCulture).ToString((IFormatProvider) CultureInfo.CurrentCulture) : this.txtBatchNumber.Text.Trim();
          this.txtBatchNumber.Text = "";
          this.txtBatchNumber.Focus();
        }
        else
          pstrBatchNumber = "-1";
        if (this.txtPlateId.Text != string.Empty)
        {
          pstrPlate = this.txtPlateId.Text.Trim().Length == 9 ? this.txtPlateId.Text.Trim().Substring(3) : this.txtPlateId.Text.Trim();
          this.txtPlateId.Text = "";
          this.txtPlateId.Focus();
        }
        string pstrRequirementPlate;
        if (this.txtRequirementId.Text != string.Empty)
        {
          pstrRequirementPlate = this.txtRequirementId.Text.Trim().Length == 10 ? Convert.ToInt32(this.txtRequirementId.Text.Trim().Substring(2), (IFormatProvider) CultureInfo.CurrentCulture).ToString((IFormatProvider) CultureInfo.CurrentCulture) : this.txtRequirementId.Text.Trim();
          this.txtRequirementId.Text = "";
          this.txtRequirementId.Focus();
        }
        else
          pstrRequirementPlate = "-1";
        int pintBeginDate;
        int pintEndDate;
        if (this.chkDate.Checked)
        {
          DateTime dateTime = this.wdpDateIni.Value;
          pintBeginDate = Convert.ToInt32(dateTime.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture));
          dateTime = this.wdpDateFin.Value;
          pintEndDate = Convert.ToInt32(dateTime.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        }
        else
        {
          pintBeginDate = 0;
          pintEndDate = 0;
        }
        this.i_PlateTypeId = Convert.ToInt32(this.Request.QueryString["t"].ToString());
        int pintLocationId = this.Session["SystemUser"] != null ? (this.Session["SystemUser"] as SystemUser).i_LocationId : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - PlateDeliveryManagement.aspx");
        this.SearchPDMList(pstrBatchNumber, pstrPlate, pstrRequirementPlate, int.Parse(this.wddStatusSobre.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), pintBeginDate, pintEndDate, this.i_PlateTypeId, pintLocationId, false);
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

    public void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      this.PlateDeliverManagement(Convert.ToInt32(this.Session["index"]), this.Session["commandName"].ToString());
    }

    public void btnSecondPopup_Click(object sender, EventArgs e)
    {
      int int32 = Convert.ToInt32(this.Session["index"]);
      this.Session["commandName"].ToString();
      GridViewRow row = this.wdgPlateDeliverManagement.Rows[int32];
      string text1 = row.Cells[15].Text;
      string text2 = row.Cells[3].Text;
      this.CreatePopUpServerTwo("Observaciones", "../../UserControls/PopupIncident.aspx?RequirementId=" + Uri.EscapeDataString(text1) + "&RequirementPlateId=" + Uri.EscapeDataString(text2), "500px", "250");
    }

    protected void PlateDeliverManagement(int index, string commandName, bool oldrequirement = false)
    {
      try
      {
        int num1 = 0;
        int num2 = 0;
        int i_ActionId = 2;
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string empty3 = string.Empty;
        GridViewRow row = this.wdgPlateDeliverManagement.Rows[index];
        if (commandName.Equals("imgbtnDeliverProcess", StringComparison.CurrentCulture))
        {
          if (row == null)
            throw new HandledException(4, "Error de selección.", "'wdgPlateDeliverManagement' - PlateDeliveryManagement.aspx");
          int int32_1 = Convert.ToInt32(row.Cells[3].Text, (IFormatProvider) CultureInfo.CurrentCulture);
          string text = row.Cells[4].Text;
          int int32_2 = Convert.ToInt32(this.wdgPlateDeliverManagement.DataKeys[index]["i_StatusRequierementPlate"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
          int int32_3 = Convert.IsDBNull((object) this.wdgPlateDeliverManagement.DataKeys[index]["i_DeliveryStatus"].ToString()) ? 0 : Convert.ToInt32(this.wdgPlateDeliverManagement.DataKeys[index]["i_DeliveryStatus"].ToString());
          int num3 = 0;
          if (this.wdgPlateDeliverManagement.DataKeys[index]["i_ClaimStatusId"].ToString() != null && this.wdgPlateDeliverManagement.DataKeys[index]["i_ClaimStatusId"].ToString() != "")
            num3 = Convert.ToInt32(this.wdgPlateDeliverManagement.DataKeys[index]["i_ClaimStatusId"].ToString());
          DateTime dateTime = DateTime.Now;
          if (this.wdgPlateDeliverManagement.DataKeys[index]["d_DeliveryDate"].ToString() != null && this.wdgPlateDeliverManagement.DataKeys[index]["d_DeliveryDate"].ToString() != "")
            dateTime = Convert.ToDateTime(this.wdgPlateDeliverManagement.DataKeys[index]["d_DeliveryDate"].ToString());
          int int32_4 = Convert.ToInt32(this.Request.QueryString["t"].ToString());
          DataTable conciliacionPlate = new RequirementQueriesBL().GetConciliacionPlate(text, i_ActionId);
          if (int32_2 == 6)
            throw new HandledException(1, "No se puede realizar la entrega porque ya fue realizada anteriormente");
          if (!Convert.ToBoolean(conciliacionPlate.Rows[0]["b_HasConciliation"]) && !Convert.ToBoolean(conciliacionPlate.Rows[0]["b_HasPlateBlock"]))
          {
            string empty4 = string.Empty;
            this.CreatePopUpServer("SIIV - Placas sin Conciliación", "../../UserControls/PopupAnnouncement.aspx?MessageTypeId=1&MessageText=Nro de Placa no tiene Conciliación Bancaria.", "430px", "165px");
            throw new HandledException(1, "No se puede realizar la entrega de la placa ya que no tiene conciliación Bancaria");
          }
          if (!Convert.ToBoolean(conciliacionPlate.Rows[0]["b_HasConciliation"]) && Convert.ToBoolean(conciliacionPlate.Rows[0]["b_HasPlateBlock"]))
          {
            string empty5 = string.Empty;
            this.CreatePopUpServer("SIIV - Placas sin Conciliación", "../../UserControls/PopupAnnouncement.aspx?MessageTypeId=1&MessageText=Nro de Placa no tiene Conciliación Bancaria.{0}Nro de Placa se encuentra bloqueada.", "430px", "209px");
            throw new HandledException(1, "No se puede realizar la entrega de la placa por no tener conciliación Bancaria y encuentrarse bloqueada");
          }
          if (Convert.ToBoolean(conciliacionPlate.Rows[0]["b_HasConciliation"]) && Convert.ToBoolean(conciliacionPlate.Rows[0]["b_HasPlateBlock"]))
          {
            string empty6 = string.Empty;
            this.CreatePopUpServer("SIIV - Placas sin Conciliación", "../../UserControls/PopupAnnouncement.aspx?MessageTypeId=1&MessageText=Nro de Placa se encuentra bloqueada.", "430px", "165px");
            throw new HandledException(1, "No se puede realizar la entrega de la placa ya que se encuentra bloqueada.");
          }
          if (int32_2 != 6 && int32_3 != 2)
          {
            if (!oldrequirement)
              this.Response.Redirect("PlateDeliveryItem.aspx?Ids=" + int32_1.ToString() + "&i_IsClaim=0&d_DeliveryDate=" + dateTime.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&v_NumDoc=" + empty1 + "&i_status=" + num1.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&i_chek=" + num2.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&strFecIni=" + empty2 + "&strFecFin=" + empty3 + "&i_PlateTypeId=" + int32_4.ToString());
            else
              this.Response.Redirect("PlateDeliveryItemOld.aspx?Ids=" + int32_1.ToString() + "&i_IsClaim=0&d_DeliveryDate=" + dateTime.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&v_NumDoc=" + empty1 + "&i_status=" + num1.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&i_chek=" + num2.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&strFecIni=" + empty2 + "&strFecFin=" + empty3 + "&i_PlateTypeId=" + int32_4.ToString());
          }
          else
          {
            if (int32_2 != 6 || int32_3 != 2)
              return;
            if (num3 != 2)
              throw new HandledException(1, "No se puede realizar la entrega porque el reclamo no está APROBADO");
            this.Response.Redirect("PlateDeliveryItem.aspx?Ids=" + int32_1.ToString() + "&i_IsClaim=1&d_DeliveryDate=" + dateTime.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&v_NumDoc=" + empty1 + "&i_status=" + num1.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&i_chek=" + num2.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&strFecIni=" + empty2 + "&strFecFin=" + empty3 + "&i_PlateTypeId" + int32_4.ToString());
          }
        }
        else
        {
          if (commandName.Equals("imgbtnDataViewDeliver", StringComparison.CurrentCulture) || !commandName.Equals("imgbtnDeliverNotProcess", StringComparison.CurrentCulture))
            return;
          if (row == null)
            throw new HandledException(4, "Error de selección.", "'wdgPlateDeliverManagement' - PlateDeliveryManagement.aspx");
          int int32 = Convert.ToInt32(row.Cells[3].Text, (IFormatProvider) CultureInfo.CurrentCulture);
          if (Convert.ToInt32(this.wdgPlateDeliverManagement.DataKeys[index]["i_StatusRequierementPlate"].ToString()) == 6)
            throw new HandledException(1, "No se puede realizar la no entrega porque ya fue entregada anteriormente");
          this.ObjRequirementQueriesBL = new RequirementQueriesBL();
          DataTable dataTable = new DataTable();
          if (this.ObjRequirementQueriesBL.DeliverySendEmail(int32).Rows.Count <= 0)
            throw new HandledException(1, "No se puede realizar la no entrega porque ya fue realizada anteriormente");
          this.CreatePopUpServer("Procesar No Entrega", "../../Delivery/EmailNotificationDelivery.aspx?RequirementPlateId=" + int32.ToString(), "480px", "320px");
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
        this.HidePopup();
      }
    }

    protected void wdgPlateDeliverManagement_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      int int32 = Convert.ToInt32(e.CommandArgument);
      string commandName = e.CommandName.ToString();
      this.Session["index"] = (object) int32;
      this.Session["commandName"] = (object) commandName;
      string empty = string.Empty;
      GridViewRow row = this.wdgPlateDeliverManagement.Rows[int32];
      string text1 = row.Cells[11].Text;
      string text2 = row.Cells[12].Text;
      string text3 = row.Cells[13].Text;
      string text4 = row.Cells[14].Text;
      string text5 = row.Cells[16].Text;
      string text6 = row.Cells[18].Text;
      bool boolean = Convert.ToBoolean(row.Cells[19].Text);
      string text7 = row.Cells[20].Text;
      string text8 = row.Cells[21].Text;
      DateTime dateTime1 = Convert.ToDateTime(row.Cells[17].Text);
      if (text5 == "1" && text6 == "1")
      {
        if (!boolean)
        {
          DateTime dateTime2 = Convert.ToDateTime(ConfigDA.ReadConfig("DeployDate"));
          if (dateTime1 > dateTime2)
          {
            if (this.ValidateApplicant(row.Cells[8].Text))
            {
              if (commandName == "imgbtnDeliverNotProcess")
              {
                this.PlateDeliverManagement(int32, commandName);
              }
              else
              {
                string pstrUrl = "../../UserControls/PopupConfirmationData.aspx?MessageTypeId=2";
                this.Session["OwnerName"] = (object) text1;
                this.Session["NroDoc"] = (object) text2;
                this.Session["OrdenDenuncia"] = (object) text3;
                this.Session["ClaveDenuncia"] = (object) text4;
                this.Session["RegistrationReason"] = (object) text8;
                if (Convert.ToInt32(text8) == 2)
                  this.CreatePopUpServer("Verificacion de datos de Propietario", pstrUrl, "450px", "265px");
                else
                  this.CreatePopUpServer("Verificacion de datos de Propietario", pstrUrl, "450px", "250px");
              }
            }
            else
              this.PlateDeliverManagement(int32, commandName);
          }
          else
            this.PlateDeliverManagement(int32, commandName, true);
        }
        else
          Message.SetMessage(this.lblMessage, new HandledException(1, "La solicitud tiene un reclamo en curso con código: " + text7));
      }
      else
        this.PlateDeliverManagement(int32, commandName, true);
    }

    private bool ValidateApplicant(string applicant) => applicant == "Propietario";

    protected void LoadParameters()
    {
      try
      {
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) ("" + SystemParameterGroups.EnvelopeStatus.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
          (object) "",
          (object) "1",
          (object) "1"
        });
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["i_GroupId"].ToString() == SystemParameterGroups.EnvelopeStatus.ToString((IFormatProvider) CultureInfo.CurrentCulture))
              this.wddStatusSobre.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          }
        }
        this.wddStatusSobre.Items.Insert(0, new ListItem("- Todos - ", "-1"));
        this.wddStatusSobre.SelectedValue = "-1";
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SetDatePicker()
    {
      try
      {
        this.wdpDateIni.Value = DateTime.Now.AddDays(-1.0);
        this.wdpDateFin.Value = DateTime.Now;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchPDM()
    {
      try
      {
        string empty1 = string.Empty;
        string pstrPlate = string.Empty;
        string empty2 = string.Empty;
        this.IsValidInputDataToSearch();
        string pstrBatchNumber;
        if (this.txtBatchNumber.Text != string.Empty)
        {
          pstrBatchNumber = this.txtBatchNumber.Text.Trim().Length == 10 ? Convert.ToInt32(this.txtBatchNumber.Text.Trim().Substring(3), (IFormatProvider) CultureInfo.CurrentCulture).ToString((IFormatProvider) CultureInfo.CurrentCulture) : this.txtBatchNumber.Text.Trim();
          this.txtBatchNumber.Text = "";
          this.txtBatchNumber.Focus();
        }
        else
          pstrBatchNumber = "-1";
        if (this.txtPlateId.Text != string.Empty)
        {
          pstrPlate = this.txtPlateId.Text.Trim().Length == 9 ? this.txtPlateId.Text.Trim().Substring(3) : this.txtPlateId.Text.Trim();
          this.txtPlateId.Text = "";
          this.txtPlateId.Focus();
        }
        string pstrRequirementPlate;
        if (this.txtRequirementId.Text != string.Empty)
        {
          pstrRequirementPlate = this.txtRequirementId.Text.Trim().Length == 10 ? Convert.ToInt32(this.txtRequirementId.Text.Trim().Substring(2), (IFormatProvider) CultureInfo.CurrentCulture).ToString((IFormatProvider) CultureInfo.CurrentCulture) : this.txtRequirementId.Text.Trim();
          this.txtRequirementId.Text = "";
          this.txtRequirementId.Focus();
        }
        else
          pstrRequirementPlate = "-1";
        if (pstrRequirementPlate == "-1" && pstrPlate == "")
          throw new HandledException(1, "Se debe especificar Nro. Placa y/o ID Solicitud");
        int pintBeginDate;
        int pintEndDate;
        if (this.chkDate.Checked)
        {
          DateTime dateTime = this.wdpDateIni.Value;
          pintBeginDate = Convert.ToInt32(dateTime.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture));
          dateTime = this.wdpDateFin.Value;
          pintEndDate = Convert.ToInt32(dateTime.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        }
        else
        {
          pintBeginDate = 0;
          pintEndDate = 0;
        }
        this.i_PlateTypeId = Convert.ToInt32(this.Request.QueryString["t"].ToString());
        int pintLocationId = this.Session["SystemUser"] != null ? (this.Session["SystemUser"] as SystemUser).i_LocationId : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - PlateDeliveryManagement.aspx");
        this.SearchPDMList(pstrBatchNumber, pstrPlate, pstrRequirementPlate, int.Parse(this.wddStatusSobre.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), pintBeginDate, pintEndDate, this.i_PlateTypeId, pintLocationId, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchPDMList(
      string pstrBatchNumber,
      string pstrPlate,
      string pstrRequirementPlate,
      int pintStatusSobre,
      int pintBeginDate,
      int pintEndDate,
      int pintPlateTypeId,
      int pintLocationId,
      bool pboolLoadPager)
    {
      try
      {
        int pintstartRowIndex = pboolLoadPager ? 1 : this.custPagerPDM.CurrentPageNumber;
        int pintmaxRows = this.custPagerPDM.CurrentPageSize == 0 ? 10 : this.custPagerPDM.CurrentPageSize;
        int pinttotalRows;
        DataTable deliverPendingBy = new PlateDeliverQueriesBL().GetPlateDeliverPendingBy(int.Parse(pstrBatchNumber, (IFormatProvider) CultureInfo.CurrentCulture), pstrPlate, int.Parse(pstrRequirementPlate, (IFormatProvider) CultureInfo.CurrentCulture), pintStatusSobre, pintBeginDate, pintEndDate, pintPlateTypeId, pintLocationId, pintstartRowIndex, pintmaxRows, out pinttotalRows);
        if (deliverPendingBy == null || deliverPendingBy.Rows.Count == 0)
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
        int num = pinttotalRows;
        this.wdgPlateDeliverManagement.DataSource = (object) deliverPendingBy;
        this.wdgPlateDeliverManagement.DataBind();
        this.custPagerPDM.TotalPages = num % pintmaxRows == 0 ? num / pintmaxRows : num / pintmaxRows + 1;
        this.custPagerPDM.TotalRecordCount = pinttotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerPDM.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void IsValidInputDataToSearch()
    {
      try
      {
        DateTime dt1 = this.wdpDateIni.Value;
        DateTime dt2 = this.wdpDateFin.Value;
        if (!this.chkDate.Checked)
        {
          if (this.txtBatchNumber.Text.Trim() == "" && this.txtPlateId.Text == "" && this.txtRequirementId.Text == "")
            throw new HandledException(1, "Se debe especificar Nro. Placa y/o ID Solicitud");
        }
        else if (PlateDeliveryManagement.DateDiff(PlateDeliveryManagement.DateInterval.Day, dt1, dt2, DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek) > 15L && this.txtPlateId.Text.Trim() == string.Empty && this.txtRequirementId.Text.Trim() == string.Empty)
          throw new HandledException(1, "Para consultas mayores a 15 días, se debe ingresar Nro. de Placa o ID Solicitud");
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

    private void CreatePopUpServerTwo(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp2('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    public enum DateInterval
    {
      Day,
      DayOfYear,
      Hour,
      Minute,
      Month,
      Quarter,
      Second,
      Weekday,
      WeekOfYear,
      Year,
    }
  }
}
