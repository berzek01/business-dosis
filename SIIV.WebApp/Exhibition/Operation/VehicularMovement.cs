// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Operation.VehicularMovement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.BE;
using SIIV.BE.CustomCode;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Operation
{
  public class VehicularMovement : Page
  {
    private VehicleMovementQueriesBL objVehicleMovementQueriesBL;
    private int i_PlateTypeId = 0;
    private int i_ProductId;
    private int i_Opcion = 0;
    protected Label LTba2;
    protected UpdatePanel UpdatePanel1;
    protected Label Label19;
    protected DropDownList wddAssociated;
    protected Label Label1;
    protected TextBox txtPlateSearch;
    protected FilteredTextBoxExtender txtPlateSearch_FilteredTextBoxExtender;
    protected Label Label2;
    protected Fecha wdpStartDate;
    protected Label Label3;
    protected Fecha wdpEndDate;
    protected Button WibGenerate;
    protected Button wibSearch;
    protected Button wibImprimir;
    protected Button wibExcel;
    protected GridView wdgList;
    protected GridView wdgListNew;
    protected Button btnReturnPopupConfirmation;
    protected Pager custPagerVM;
    protected Label lblMessage;
    protected Button Button1;
    protected Button Button2;
    protected Button Button3;
    protected UpdatePanel UpdatePanel2;
    protected Label Label45;
    protected TextBox txtPlateNew;
    protected FilteredTextBoxExtender ftbeAlias;
    protected Button wibPlateValidate;
    protected Label lblMessageValidate;
    protected HiddenField HiddenField1;
    protected Label lblMov;
    protected HtmlGenericControl camposRotativa2;
    protected Label Label16;
    protected TextBox txtRazonSocial;
    protected RequiredFieldValidator RequiredFieldValidator10;
    protected ValidatorCalloutExtender ValidatorCalloutExtender12;
    protected Label Label27;
    protected TextBox txtDocumentNumber;
    protected RequiredFieldValidator RequiredFieldValidator12;
    protected ValidatorCalloutExtender ValidatorCalloutExtender13;
    protected Label lblDireccion;
    protected TextBox txtAddress;
    protected RequiredFieldValidator RequiredFieldValidator13;
    protected ValidatorCalloutExtender ValidatorCalloutExtender14;
    protected Label lblTelephoneRef;
    protected TextBox txtTelephoneRef;
    protected RequiredFieldValidator RequiredFieldValidator17;
    protected ValidatorCalloutExtender ValidatorCalloutExtender15;
    protected MaskedEditExtender txtTelephoneRef_MaskedEditExtender;
    protected Label Label28;
    protected Label lblEmail;
    protected TextBox txtEmail;
    protected RegularExpressionValidator RegularExpressionValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender16;
    protected Label Label42;
    protected TextBox txtBrand;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected ValidatorCalloutExtender ValidatorCalloutExtender1;
    protected Label Label9;
    protected TextBox txtModel;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender2;
    protected FilteredTextBoxExtender FilteredTextBoxExtender2;
    protected Label Label10;
    protected TextBox txtColor;
    protected RequiredFieldValidator RequiredFieldValidator3;
    protected ValidatorCalloutExtender ValidatorCalloutExtender3;
    protected FilteredTextBoxExtender FilteredTextBoxExtender1;
    protected Label Label11;
    protected TextBox txtVin;
    protected RequiredFieldValidator RequiredFieldValidator4;
    protected ValidatorCalloutExtender ValidatorCalloutExtender4;
    protected FilteredTextBoxExtender ftbeSerie;
    protected Label Label5;
    protected TextBox txtInsurancePolicyNumber;
    protected RequiredFieldValidator RequiredFieldValidator5;
    protected ValidatorCalloutExtender ValidatorCalloutExtender5;
    protected Label Label6;
    protected Fecha wdpExpirationDate;
    protected HtmlGenericControl camposExhibicion;
    protected Label Label7;
    protected Fecha wdpTransferDate;
    protected Label Label12;
    protected TextBox txtStartTime;
    protected RequiredFieldValidator RequiredFieldValidator6;
    protected ValidatorCalloutExtender ValidatorCalloutExtender6;
    protected Label Label4;
    protected TextBox txtStartMinute;
    protected RequiredFieldValidator RequiredFieldValidator7;
    protected ValidatorCalloutExtender ValidatorCalloutExtender7;
    protected Label Label53;
    protected Label Label20;
    protected Label Label68;
    protected TextBox txtOrigin;
    protected RequiredFieldValidator RequiredFieldValidator8;
    protected ValidatorCalloutExtender ValidatorCalloutExtender8;
    protected Label Label13;
    protected TextBox txtDestino;
    protected RequiredFieldValidator RequiredFieldValidator9;
    protected ValidatorCalloutExtender ValidatorCalloutExtender9;
    protected Label Label17;
    protected TextBox txtTelephone;
    protected RequiredFieldValidator RequiredFieldValidator11;
    protected ValidatorCalloutExtender ValidatorCalloutExtender10;
    protected Label Label14;
    protected TextBox txtEstimatedTime;
    protected RequiredFieldValidator RequiredFieldValidator14;
    protected ValidatorCalloutExtender RequiredFieldValidator14_ValidatorCalloutExtender;
    protected Label Label21;
    protected TextBox txtEstimatedMinute;
    protected RequiredFieldValidator RequiredFieldValidator15;
    protected ValidatorCalloutExtender RequiredFieldValidator15_ValidatorCalloutExtender;
    protected Label Label22;
    protected Label Label15;
    protected HtmlGenericControl camposRotativa;
    protected Label lblOriginalNrolabel;
    protected Label lblProvisionalNrolabel;
    protected TextBox txtTitle;
    protected RequiredFieldValidator RequiredFieldValidator16;
    protected ValidatorCalloutExtender ValidatorCalloutExtender11;
    protected FilteredTextBoxExtender FilteredTextBoxExtender3;
    protected Label lblOriginalFechalabel;
    protected Label lblProvisionalFechalabel;
    protected Fecha wdpTitleDate;
    protected Label Label23;
    protected Label lblDayMax;
    protected Label Label24;
    protected Label lblDateIni;
    protected Label Label25;
    protected Label lblDateFin;
    protected Label Label18;
    protected TextBox txtComments;
    protected HtmlGenericControl btnSaveExhibition;
    protected Button wibSaveExhibition;
    protected HtmlGenericControl btnSaveRotate;
    protected Button wibSaveRotate;
    protected Button wibCancel;
    protected Button wibPrintMovement;
    protected Button wibReturn;
    protected Label lblMessage0;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.ViewState["i_PlateTypeId"] = (object) this.Request.QueryString["t"].ToString();
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.ViewState["i_Opcion"] = (object) this.Request.QueryString["i_Opcion"].ToString();
        this.i_Opcion = Convert.ToInt32(this.ViewState["i_Opcion"].ToString());
        this.txtStartTime.Text = "0";
        this.txtStartMinute.Text = "0";
        this.txtEstimatedTime.Text = "0";
        this.txtEstimatedMinute.Text = "0";
        switch (this.i_PlateTypeId)
        {
          case 7:
            this.camposExhibicion.Visible = true;
            this.btnSaveExhibition.Visible = true;
            this.LTba2.Text = "Movimiento Vehicular";
            this.wdgList.Columns[3].Visible = false;
            this.wdgList.Columns[4].Visible = false;
            this.wdgList.Columns[5].Visible = false;
            this.wdgList.Columns[6].Visible = false;
            this.wdgList.Columns[7].Visible = false;
            this.wdgList.Columns[19].Visible = false;
            this.wdgList.Columns[20].Visible = false;
            this.wdgList.Columns[21].Visible = false;
            this.wibImprimir.Visible = true;
            this.lblMov.Text = "REGISTRO MOVIMIENTOS";
            break;
          case 11:
            SystemParameterManagementBL parameterManagementBl = new SystemParameterManagementBL();
            ArrayList pobj = new ArrayList()
            {
              (object) "526",
              (object) "",
              (object) "1",
              (object) "1"
            };
            foreach (SIIV.BE.SystemParameter systemParameter in parameterManagementBl.Get((object) pobj))
            {
              if (systemParameter.i_ParameterId == 3)
                this.ViewState["DayMax"] = (object) systemParameter.v_Value;
            }
            this.camposRotativa.Visible = true;
            this.camposRotativa2.Visible = true;
            this.btnSaveRotate.Visible = true;
            this.LTba2.Text = "Asignación Específica";
            this.wibImprimir.Visible = false;
            this.wdgList.Columns[1].Visible = false;
            this.wdgList.Columns[13].Visible = false;
            this.wdgList.Columns[14].Visible = false;
            this.wdgList.Columns[15].Visible = false;
            this.wdgList.Columns[16].Visible = false;
            this.wdgList.Columns[17].Visible = false;
            this.lblMov.Text = "DATOS DE PROPIETARIO - ASIGNACION ESPECÍFICA";
            if (Convert.ToInt32(ConfigurationManager.AppSettings["ActivateTagRotative"]) == 1)
            {
              this.lblOriginalNrolabel.Visible = false;
              this.lblOriginalFechalabel.Visible = false;
              this.lblProvisionalNrolabel.Visible = true;
              this.lblProvisionalFechalabel.Visible = true;
              this.RequiredFieldValidator16.ErrorMessage = "Ingrese Nro de comprobante de pago";
              this.FilteredTextBoxExtender3.ValidChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-";
              break;
            }
            this.lblOriginalNrolabel.Visible = true;
            this.lblOriginalFechalabel.Visible = true;
            this.lblProvisionalNrolabel.Visible = false;
            this.lblProvisionalFechalabel.Visible = false;
            this.RequiredFieldValidator16.ErrorMessage = "Ingrese Título";
            this.FilteredTextBoxExtender3.ValidChars = "0123456789";
            break;
        }
        this.LoadAssociated();
        string absoluteUri = this.Request.Url.AbsoluteUri;
        if (this.i_Opcion == 1)
        {
          string script = UtilDA.ActiveTabIndex("tabs", 0, "1");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        }
        else if (this.i_Opcion == 2)
        {
          string script = UtilDA.ActiveTabIndex("tabs", 1, "0");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        }
        this.wdpStartDate.Value = DateTime.Now;
        this.wdpEndDate.Value = DateTime.Now;
        this.wdpTitleDate.Text = "";
        this.wibImprimir.Enabled = false;
        this.wibExcel.Enabled = false;
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

    protected void wibGenerate_Click(object sender, EventArgs e)
    {
      try
      {
        string script = UtilDA.ActiveTabIndex("tabs", 1, "0");
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
        this.ClearControls();
        this.EnabledControls(false);
        this.txtPlateNew.Enabled = true;
        this.txtPlateNew.Text = "";
        this.wibPlateValidate.Enabled = true;
        this.wibSaveExhibition.Enabled = false;
        this.wibSaveRotate.Enabled = false;
        this.wibCancel.Enabled = false;
        this.lblMessageValidate.Visible = false;
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

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchVM();

    protected void custPagerVM_OnPageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        int int32 = Convert.ToInt32(this.wddAssociated.SelectedValue.ToString());
        string v_PlateNew = this.txtPlateSearch.Text.TrimEnd();
        DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
        DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.i_ProductId = this.SetProductId(this.i_PlateTypeId);
        this.SearchVMList(int32, dateTime1, dateTime2, v_PlateNew, this.i_ProductId, this.i_PlateTypeId, false);
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

    protected void wibImprimir_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.wddAssociated.SelectedItem.Text.Equals("-- Seleccione --", StringComparison.CurrentCulture) && this.wddAssociated.SelectedValue == "0")
          throw new HandledException(1, "Seleccione un Asociado");
        string script = "ExportpdfAll();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
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

    protected void wibExcel_Click(object sender, EventArgs e)
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

    protected void wdgList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        if (e.CommandName == "PrintImg")
        {
          int int32 = Convert.ToInt32(e.CommandArgument);
          if (this.wdgList.Rows[int32] == null)
            throw new HandledException(4, "Error de selección.", "'wdgList' - VehicularMovement.aspx");
          if (Convert.ToInt32(this.wdgList.DataKeys[int32]["Estado"].ToString()) == 0)
          {
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "Este movimiento ya fue eliminado, no se puede mostrar"));
          }
          else
          {
            this.ViewState["i_vehiclemovementid"] = (object) Convert.ToInt32(this.wdgList.DataKeys[int32]["i_VehicleMovementId"].ToString());
            string script = "Exportpdf();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
          }
        }
        if (!(e.CommandName == "DeleteImg"))
          return;
        int int32_1 = Convert.ToInt32(e.CommandArgument);
        if (this.wdgList.Rows[int32_1] == null)
          throw new HandledException(4, "Error de selección.", "'wdgList' - VehicularMovement.aspx");
        if (Convert.ToInt32(this.wdgList.DataKeys[int32_1]["Estado"].ToString()) == 0)
        {
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "Este movimiento ya fue eliminado, no se puede mostrar"));
        }
        else
        {
          this.Session["i_VehicleMovementId"] = (object) Convert.ToInt32(this.wdgList.DataKeys[int32_1]["i_VehicleMovementId"].ToString());
          string empty = string.Empty;
          this.CreatePopUpServer("SIIV-Exhibition", "../Operation/VehicularMovementDelete.aspx", "610px", "320px");
        }
      }
      catch (HandledException ex)
      {
        this.HidePopup();
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        this.HidePopup();
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      try
      {
        string v_Observation = "";
        if (this.Session["i_VehicleMovementId"] != null)
        {
          SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
          if (this.Session["Observation"] != null)
            v_Observation = this.Session["Observation"].ToString();
          this.objVehicleMovementQueriesBL = new VehicleMovementQueriesBL();
          if (this.objVehicleMovementQueriesBL.VehicleMovementDelete(Convert.ToInt32(this.Session["i_VehicleMovementId"].ToString()), systemUser.i_SystemUserId, DateTime.Now, 0, v_Observation) > 0)
          {
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(2, "Se eliminó correctamente la constancia complementaria"));
            this.SearchVM();
          }
        }
        else
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "Se encontró un problema. </br>Seleccione nuevamente"));
        this.Session.Remove("Observation");
        this.Session.Remove("i_VehicleMovementId");
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

    protected void wibPlateValidate_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.txtPlateNew.Text.Length == 0)
          throw new HandledException(1, "Debe ingresar una placa para realizar la validación");
        this.ValidatePlate();
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage0, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage0, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      try
      {
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.i_ProductId = Convert.ToInt32(this.ViewState["i_ProductId"].ToString());
        DataTable dataTable1 = new DataTable();
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - VehicularMovement.aspx");
        this.objVehicleMovementQueriesBL = new VehicleMovementQueriesBL();
        string v_Plate = this.txtPlateNew.Text.TrimEnd();
        string v_TransferDate = Convert.ToString(this.wdpTransferDate.Value.Year) + "/" + Convert.ToString(this.wdpTransferDate.Value.Month) + "/" + Convert.ToString(this.wdpTransferDate.Value.Day);
        int int32_1 = Convert.ToInt32(this.txtStartTime.Text);
        int int32_2 = Convert.ToInt32(this.txtStartMinute.Text);
        int int32_3 = Convert.ToInt32(this.txtEstimatedTime.Text);
        int int32_4 = Convert.ToInt32(this.txtEstimatedMinute.Text);
        string v_Vin = this.txtVin.Text.TrimEnd();
        if (this.i_PlateTypeId == Convert.ToInt32(7))
        {
          DataTable dataTable2 = this.objVehicleMovementQueriesBL.VehicleMovementValid(systemUser.i_SystemUserId, v_Plate, v_TransferDate, int32_1, int32_2, int32_3, int32_4, this.i_PlateTypeId, v_Vin);
          DateTime now1 = DateTime.Now;
          string[] strArray1 = new string[10];
          DateTime now2 = this.wdpTransferDate.Value;
          strArray1[0] = Convert.ToString(now2.Year);
          strArray1[1] = "/";
          now2 = this.wdpTransferDate.Value;
          strArray1[2] = Convert.ToString(now2.Month);
          strArray1[3] = "/";
          now2 = this.wdpTransferDate.Value;
          strArray1[4] = Convert.ToString(now2.Day);
          strArray1[5] = " ";
          strArray1[6] = Convert.ToString(int32_1);
          strArray1[7] = ":";
          strArray1[8] = Convert.ToString(int32_2);
          strArray1[9] = ":00";
          DateTime dateTime1 = Convert.ToDateTime(string.Concat(strArray1));
          string[] strArray2 = new string[10];
          now2 = DateTime.Now;
          strArray2[0] = Convert.ToString(now2.Year);
          strArray2[1] = "/";
          now2 = DateTime.Now;
          strArray2[2] = Convert.ToString(now2.Month);
          strArray2[3] = "/";
          now2 = DateTime.Now;
          strArray2[4] = Convert.ToString(now2.Day);
          strArray2[5] = " ";
          now2 = DateTime.Now;
          strArray2[6] = Convert.ToString(now2.Hour);
          strArray2[7] = ":";
          now2 = DateTime.Now;
          strArray2[8] = Convert.ToString(now2.Minute);
          strArray2[9] = ":00";
          DateTime dateTime2 = Convert.ToDateTime(string.Concat(strArray2));
          if (dateTime1.CompareTo(dateTime2) < 0)
          {
            this.wdpTransferDate.Text = "";
            throw new HandledException(1, "No puede realizar el movimiento, ya que la fecha de traslado es menor que la fecha actual");
          }
          if (dataTable2.Rows.Count > 0)
            throw new HandledException(1, "No puede realizar el movimiento en este intervalo de tiempo.</br>Ya tiene programado un traslado");
        }
        else if (this.i_PlateTypeId == Convert.ToInt32(11))
        {
          if (this.txtRazonSocial.Text.Trim() == "")
            throw new HandledException(1, "Ingresar Razón Social.");
          if (this.txtDocumentNumber.Text.Trim() == "")
            throw new HandledException(1, "Ingresar Documento.");
          if (this.txtAddress.Text.Trim() == "")
            throw new HandledException(1, "Ingresar Dirección.");
          if (this.txtTelephoneRef.Text == "___-___-___")
            throw new HandledException(1, "Ingresar el número de telefono ref.");
          if (this.txtBrand.Text.Trim() == "")
            throw new HandledException(1, "Ingresar Marca.");
          if (this.txtModel.Text.Trim() == "")
            throw new HandledException(1, "Ingresar Modelo.");
          if (this.txtColor.Text.Trim() == "")
            throw new HandledException(1, "Ingresar Color.");
          if (this.wdpExpirationDate.Text == "")
            throw new HandledException(1, "Ingresar la fecha de vencimiento.");
          if (this.txtInsurancePolicyNumber.Text.Trim() == "")
            throw new HandledException(1, "Ingresar Numero de Póliza.");
          if (this.txtVin.Text.TrimEnd().Length != 17)
            throw new HandledException(1, "No puede realizar el movimiento.</br>Debe ingresar un N° VIN/Serie de 17 caracteres");
          if (this.txtEmail.Text == "")
            throw new HandledException(1, "Ingresar el email");
          if (this.txtTitle.Text == "")
          {
            if (Convert.ToInt32(ConfigurationManager.AppSettings["ActivateTagRotative"]) == 1)
              throw new HandledException(1, "Ingresar el Nro. de Titulo");
            throw new HandledException(1, "Ingresar Título");
          }
          if (this.wdpTitleDate.Text == "")
          {
            if (Convert.ToInt32(ConfigurationManager.AppSettings["ActivateTagRotative"]) == 1)
              throw new HandledException(1, "Ingresar la fecha de titulo");
            throw new HandledException(1, "Ingresar la fecha de emisión del documento");
          }
          if (this.objVehicleMovementQueriesBL.VehicleMovementValid(systemUser.i_SystemUserId, v_Plate, "", 0, 0, 0, 0, this.i_PlateTypeId, v_Vin).Rows.Count > 0)
            throw new HandledException(1, "No puede realizar el movimiento.</br>Esta placa ya fue asignada a este Vehículo o la placa está en uso");
        }
        int num = 0;
        this.ViewState["i_vehiclemovementid"] = (object) 0;
        ExhibitionVehicleMovement pobjExhibitionVehicleMovement = new ExhibitionVehicleMovement();
        pobjExhibitionVehicleMovement.i_SystemUserId = systemUser.i_SystemUserId;
        pobjExhibitionVehicleMovement.v_Plate = this.txtPlateNew.Text;
        pobjExhibitionVehicleMovement.v_Brand = this.txtBrand.Text.TrimEnd();
        pobjExhibitionVehicleMovement.v_Model = this.txtModel.Text.TrimEnd();
        pobjExhibitionVehicleMovement.v_Color = this.txtColor.Text.TrimEnd();
        pobjExhibitionVehicleMovement.v_SerialNumber = this.txtVin.Text.TrimEnd();
        pobjExhibitionVehicleMovement.v_InsurancePolicyNumber = this.txtInsurancePolicyNumber.Text.TrimEnd();
        pobjExhibitionVehicleMovement.d_DateExpiresInsurancePolicy = Convert.ToDateTime(this.wdpExpirationDate.Value);
        switch (this.i_PlateTypeId)
        {
          case 7:
            pobjExhibitionVehicleMovement.d_TransferDate = Convert.ToDateTime(this.wdpTransferDate.Value);
            pobjExhibitionVehicleMovement.i_StartTime = Convert.ToInt32(this.txtStartTime.Text);
            pobjExhibitionVehicleMovement.i_StartMinute = Convert.ToInt32(this.txtStartMinute.Text);
            pobjExhibitionVehicleMovement.v_Origin = this.txtOrigin.Text.TrimEnd();
            pobjExhibitionVehicleMovement.v_Destination = this.txtDestino.Text.TrimEnd();
            pobjExhibitionVehicleMovement.v_Telephone = this.txtTelephone.Text.TrimEnd();
            pobjExhibitionVehicleMovement.i_EstimatedTime = Convert.ToInt32(this.txtEstimatedTime.Text);
            pobjExhibitionVehicleMovement.i_EstimatedMinute = Convert.ToInt32(this.txtEstimatedMinute.Text);
            pobjExhibitionVehicleMovement.v_Observation = this.txtComments.Text.TrimEnd();
            pobjExhibitionVehicleMovement.i_Status = 1;
            pobjExhibitionVehicleMovement.i_InsertUserId = systemUser.i_SystemUserId;
            pobjExhibitionVehicleMovement.v_ReasonSocial = "";
            pobjExhibitionVehicleMovement.v_DocumentNumber = "";
            pobjExhibitionVehicleMovement.v_Email = "";
            pobjExhibitionVehicleMovement.v_Ubigeo = "";
            pobjExhibitionVehicleMovement.v_TelephonerRef = "";
            pobjExhibitionVehicleMovement.d_TitleNumber = DateTime.Now;
            break;
          case 11:
            pobjExhibitionVehicleMovement.d_TransferDate = Convert.ToDateTime(this.lblDateIni.Text);
            pobjExhibitionVehicleMovement.i_StartTime = 0;
            pobjExhibitionVehicleMovement.i_StartMinute = 0;
            pobjExhibitionVehicleMovement.v_Origin = "";
            pobjExhibitionVehicleMovement.v_Destination = "";
            pobjExhibitionVehicleMovement.v_Telephone = "";
            pobjExhibitionVehicleMovement.i_EstimatedTime = Convert.ToInt32(this.txtEstimatedTime.Text);
            pobjExhibitionVehicleMovement.i_EstimatedMinute = Convert.ToInt32(this.txtEstimatedMinute.Text);
            pobjExhibitionVehicleMovement.v_Observation = this.txtComments.Text.TrimEnd();
            pobjExhibitionVehicleMovement.i_Status = 1;
            pobjExhibitionVehicleMovement.i_InsertUserId = systemUser.i_SystemUserId;
            pobjExhibitionVehicleMovement.v_ReasonSocial = this.txtRazonSocial.Text;
            pobjExhibitionVehicleMovement.v_DocumentNumber = this.txtDocumentNumber.Text;
            pobjExhibitionVehicleMovement.v_Email = this.txtEmail.Text.Trim();
            pobjExhibitionVehicleMovement.v_Ubigeo = this.txtAddress.Text;
            pobjExhibitionVehicleMovement.v_TelephonerRef = this.txtTelephoneRef.Text.TrimEnd();
            pobjExhibitionVehicleMovement.d_TitleNumber = Convert.ToDateTime(this.wdpTitleDate.Value);
            break;
        }
        pobjExhibitionVehicleMovement.i_ProductId = this.i_ProductId;
        pobjExhibitionVehicleMovement.i_SpecialPlateTypeId = this.i_PlateTypeId;
        pobjExhibitionVehicleMovement.v_TitleNumber = this.txtTitle.Text.TrimEnd();
        switch (this.i_PlateTypeId)
        {
          case 7:
            num = new VehicleMovementQueriesBL().VehicleMovementInsert(pobjExhibitionVehicleMovement);
            break;
          case 11:
            if (this.HiddenField1.Value == "1")
              return;
            num = new VehicleMovementQueriesBL().VehicleMovementInsert(pobjExhibitionVehicleMovement);
            break;
        }
        if (num > 0)
        {
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage0, new HandledException(2, "Se registró correctamente el movimiento vehicular"));
          this.ClearControls();
          this.EnabledControls(false);
          this.wibSaveExhibition.Enabled = false;
          this.wibSaveRotate.Enabled = false;
          this.wibCancel.Enabled = false;
          this.wibPrintMovement.Enabled = true;
          this.txtPlateNew.Enabled = true;
          this.wibPlateValidate.Enabled = true;
          this.lblMessageValidate.Visible = false;
          this.ViewState["i_vehiclemovementid"] = (object) num;
        }
        else
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage0, new HandledException(1, "Se encontró un problema en el registro del movimiento vehicular"));
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

    protected void wibCancel_Click(object sender, EventArgs e)
    {
      try
      {
        this.ClearControls();
        this.EnabledControls(false);
        this.txtPlateNew.Enabled = true;
        this.wibPlateValidate.Enabled = true;
        this.wibSaveExhibition.Enabled = false;
        this.wibSaveRotate.Enabled = false;
        this.wibCancel.Enabled = false;
        this.wibPrintMovement.Enabled = false;
        this.lblMessageValidate.Visible = false;
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

    protected void wibPrintMovement_Click(object sender, EventArgs e)
    {
      string script = "Exportpdf();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void wibReturn_Click(object sender, EventArgs e)
    {
      string script = UtilDA.ActiveTabIndex("tabs", 0, "1");
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script, true);
    }

    protected void Button1_Click(object sender, EventArgs e) => this.PrintReportAll();

    protected void Button2_Click(object sender, EventArgs e)
    {
      try
      {
        this.PrintReport(Convert.ToInt32(this.ViewState["i_vehiclemovementid"], (IFormatProvider) CultureInfo.CurrentCulture));
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

    protected void Button3_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable objDataTable = (DataTable) this.Session["dtExport"];
        ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
        List<ClassColumns> classColumnsList = new List<ClassColumns>();
        foreach (DataControlField column in (StateManagedCollection) this.wdgList.Columns)
        {
          if (column.Visible && column.GetType().Name == "BoundField")
          {
            BoundField boundField = (BoundField) column;
            classColumnsList.Add(new ClassColumns(boundField.DataField, 1, (int) column.ControlStyle.Width.Value, boundField.HeaderText));
          }
        }
        exportToExcelDataGrid.clsTitle = classColumnsList;
        exportToExcelDataGrid.AgregarHojaLibro(objDataTable, "Listado Movimientos");
        exportToExcelDataGrid.CerrarLibro();
        byte[] buffer = exportToExcelDataGrid.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=ListadoMovimientos.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
      }
      catch (Exception ex)
      {
      }
    }

    private void LoadAssociated()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - VehicularMovement.aspx");
        AssociatedQueriesBL associatedQueriesBl = new AssociatedQueriesBL();
        if (this.Session["ApplicationId"] == null)
          return;
        int int32_1 = Convert.ToInt32(this.Session["ApplicationId"], (IFormatProvider) CultureInfo.CurrentCulture);
        string userExtendedAction = associatedQueriesBl.GetSystemUserExtendedAction(systemUser.i_SystemUserId, int32_1);
        DataTable dataTable = new DataTable();
        if (userExtendedAction != "")
        {
          int int32_2 = Convert.ToInt32(Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("7", StringComparison.CurrentCulture))), (IFormatProvider) CultureInfo.CurrentCulture);
          this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
          if (int32_2 == 7)
          {
            this.FillAssociatedWdd(associatedQueriesBl.AssociatedList(systemUser.i_SystemUserId, int32_2, this.i_PlateTypeId));
            this.wddAssociated.Items.Insert(0, new ListItem("-- Todos --", "0"));
            this.wddAssociated.SelectedIndex = 0;
            this.WibGenerate.Enabled = false;
          }
          else
          {
            DataTable dt_Result = associatedQueriesBl.AssociatedList(systemUser.i_SystemUserId, 0, this.i_PlateTypeId);
            if (dt_Result.Rows.Count == 1)
            {
              this.FillAssociatedWdd(dt_Result);
              this.wddAssociated.SelectedIndex = 0;
            }
            else
            {
              this.FillAssociatedWdd(dt_Result);
              this.wddAssociated.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
              this.wddAssociated.SelectedIndex = 0;
            }
          }
        }
        else
        {
          this.FillAssociatedWdd(associatedQueriesBl.AssociatedList(systemUser.i_SystemUserId, 0, this.i_PlateTypeId));
          this.wddAssociated.SelectedIndex = 0;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void FillAssociatedWdd(DataTable dt_Result)
    {
      try
      {
        if (dt_Result == null || dt_Result.Rows.Count == 0)
          return;
        this.wddAssociated.DataSource = (object) dt_Result;
        this.wddAssociated.DataTextField = "v_Alias";
        this.wddAssociated.DataValueField = "i_SystemUserId";
        this.wddAssociated.DataBind();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ClearControls()
    {
      this.txtRazonSocial.Text = "";
      this.txtTelephoneRef.Text = "";
      this.txtDocumentNumber.Text = "";
      this.txtAddress.Text = "";
      this.txtEmail.Text = "";
      this.txtBrand.Text = "";
      this.txtModel.Text = "";
      this.txtColor.Text = "";
      this.txtVin.Text = "";
      this.txtInsurancePolicyNumber.Text = "";
      this.wdpExpirationDate.Text = "";
      this.txtTitle.Text = "";
      this.lblDayMax.Text = "";
      this.lblDateIni.Text = "";
      this.lblDateFin.Text = "";
      this.wdpTitleDate.Text = "";
      this.txtStartTime.Text = "0";
      this.txtStartMinute.Text = "0";
      this.txtOrigin.Text = "";
      this.txtDestino.Text = "";
      this.txtTelephone.Text = "";
      this.txtEstimatedTime.Text = "0";
      this.txtEstimatedMinute.Text = "0";
      this.txtComments.Text = "";
      this.wdpExpirationDate.Value = DateTime.Now;
      this.wdpTransferDate.Value = DateTime.Now;
    }

    private void EnabledControls(bool enabled)
    {
      this.txtRazonSocial.Enabled = enabled;
      this.txtTelephoneRef.Enabled = enabled;
      this.txtDocumentNumber.Enabled = enabled;
      this.txtAddress.Enabled = enabled;
      this.txtEmail.Enabled = enabled;
      this.txtBrand.Enabled = enabled;
      this.txtModel.Enabled = enabled;
      this.txtColor.Enabled = enabled;
      this.txtVin.Enabled = enabled;
      this.txtTitle.Enabled = enabled;
      this.wdpTitleDate.Enabled = enabled;
      this.txtStartTime.Enabled = enabled;
      this.txtStartMinute.Enabled = enabled;
      this.txtOrigin.Enabled = enabled;
      this.txtDestino.Enabled = enabled;
      this.txtTelephone.Enabled = enabled;
      this.txtEstimatedTime.Enabled = enabled;
      this.txtEstimatedMinute.Enabled = enabled;
      this.txtComments.Enabled = enabled;
      this.wdpExpirationDate.Enabled = enabled;
      this.wdpTransferDate.Enabled = enabled;
    }

    private void SearchVM()
    {
      try
      {
        this.wibImprimir.Enabled = false;
        this.wibExcel.Enabled = false;
        int int32 = Convert.ToInt32(this.wddAssociated.SelectedValue.ToString());
        DateTime dateTime1 = this.wdpEndDate.Value;
        if (dateTime1.Subtract(this.wdpStartDate.Value).Days > 360)
          throw new HandledException(1, "Si el intervalo de fechas excede a 360 días, debe especificar un número de placa");
        string v_PlateNew = this.txtPlateSearch.Text.TrimEnd();
        DateTime dateTime2 = Convert.ToDateTime(this.wdpStartDate.Value);
        dateTime1 = Convert.ToDateTime(this.wdpEndDate.Value);
        DateTime dateTime3 = Convert.ToDateTime(dateTime1.ToShortDateString());
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.i_ProductId = this.SetProductId(this.i_PlateTypeId);
        this.SearchVMList(int32, dateTime2, dateTime3, v_PlateNew, this.i_ProductId, this.i_PlateTypeId, true);
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

    private void SearchVMList(
      int i_SystemUserId,
      DateTime d_StartDate,
      DateTime d_EndDate,
      string v_PlateNew,
      int i_ProductId,
      int i_SpecialPlateTypeId,
      bool pboolLoadPager)
    {
      try
      {
        int startRowIndex = pboolLoadPager ? 1 : this.custPagerVM.CurrentPageNumber;
        int maxRows = this.custPagerVM.CurrentPageSize == 0 ? 10 : this.custPagerVM.CurrentPageSize;
        int pintTotalRows;
        DataTable all = new VehicleMovementQueriesBL().SpecialPlateVehicleMovementGetAll(i_SystemUserId, d_StartDate, d_EndDate, v_PlateNew, i_ProductId, this.i_PlateTypeId, startRowIndex, maxRows, out pintTotalRows);
        if (all == null || all.Rows.Count == 0)
        {
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
          this.HidePopup();
        }
        else
        {
          this.lblMessage.Visible = false;
          this.wibImprimir.Enabled = true;
          this.wibExcel.Enabled = true;
        }
        int num = pintTotalRows;
        this.wdgList.DataSource = (object) all;
        this.wdgList.DataBind();
        this.custPagerVM.TotalPages = num % maxRows == 0 ? num / maxRows : num / maxRows + 1;
        this.custPagerVM.TotalRecordCount = pintTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerVM.LoadPager();
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

    private void ValidatePlate()
    {
      try
      {
        DataTable dataTable = new DataTable();
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - VehicularMovement.aspx");
        string pintPlateNew = this.txtPlateNew.Text.TrimEnd();
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.i_ProductId = this.SetProductId(this.i_PlateTypeId);
        DataRow row = new VehicleMovementQueriesBL().SpecialPlateVehicleMovementValidatePlate(systemUser.i_SystemUserId, pintPlateNew, this.i_ProductId, this.i_PlateTypeId).Rows[0];
        this.ViewState["i_ProductId"] = (object) row[6].ToString();
        this.txtInsurancePolicyNumber.Text = row[1].ToString() != null || row[1].ToString() != "" ? row[3].ToString() : throw new HandledException(1, "Placa no registrada, verifique nuevamente");
        this.txtInsurancePolicyNumber.Enabled = false;
        if (row[4].ToString() != null && row[4].ToString() != "")
          this.wdpExpirationDate.Value = Convert.ToDateTime(row[4].ToString());
        this.txtTelephone.Text = row[5].ToString();
        this.txtTelephone.Enabled = false;
        if (this.i_PlateTypeId == 11)
        {
          this.lblDayMax.Text = this.ViewState["DayMax"].ToString();
          int num = Convert.ToInt32(this.ViewState["DayMax"].ToString()) - 1;
          Label lblDateIni = this.lblDateIni;
          DateTime dateTime = DateTime.Now;
          string shortDateString1 = dateTime.ToShortDateString();
          lblDateIni.Text = shortDateString1;
          Label lblDateFin = this.lblDateFin;
          dateTime = DateTime.Now;
          dateTime = dateTime.AddDays((double) num);
          string shortDateString2 = dateTime.ToShortDateString();
          lblDateFin.Text = shortDateString2;
        }
        SIIV.Common.Resource.Message.SetMessage(this.lblMessageValidate, new HandledException(2, "Se validó la placa correctamente"));
        this.EnabledControls(true);
        this.wdpExpirationDate.Enabled = false;
        this.wibSaveExhibition.Enabled = true;
        this.wibSaveRotate.Enabled = true;
        this.wibCancel.Enabled = true;
        this.wibPrintMovement.Enabled = false;
        this.txtPlateNew.Enabled = false;
        this.wibPlateValidate.Enabled = false;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void PrintReportAll()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - VehicuarMovement.aspx");
        int int32 = Convert.ToInt32(this.wddAssociated.SelectedValue.ToString());
        string v_Plate = this.txtPlateSearch.Text.TrimEnd();
        DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
        DateTime dateTime2 = Convert.ToDateTime(this.wdpEndDate.Value);
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        DataTable dataReport = new VehicleMovementQueriesBL().SpecialPlateVehicleMovementGetDataReport(int32, "", v_Plate, 0, dateTime1, dateTime2, this.i_PlateTypeId);
        ReportDocument reportDocument = new ReportDocument();
        string filename = this.Server.MapPath("../Reports/ReportVehicularMovement.rpt");
        reportDocument.Load(filename);
        reportDocument.SetDataSource(dataReport);
        reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "ReporteConstanciasComplementarias");
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

    private void PrintReport(int i_vehiclemovementid)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - VehicularMovement.aspx");
        string filename = "";
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        DataTable dataReport = new VehicleMovementQueriesBL().SpecialPlateVehicleMovementGetDataReport(0, "", "", i_vehiclemovementid, DateTime.Now, DateTime.Now, this.i_PlateTypeId);
        ReportDocument reportDocument = new ReportDocument();
        if (this.i_PlateTypeId == 7)
          filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportConstancyVehicle.rpt";
        else if (this.i_PlateTypeId == 11)
          filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportConstancyVehicleDealer.rpt";
        reportDocument.Load(filename);
        reportDocument.SetDataSource(dataReport);
        reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "ReporteConstanciaComplementaria");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ExportList()
    {
      try
      {
        int int32 = Convert.ToInt32(this.wddAssociated.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        string v_PlateNew = this.txtPlateSearch.Text.TrimEnd();
        DateTime dateTime1 = Convert.ToDateTime((object) this.wdpStartDate.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime((object) this.wdpEndDate.Value, (IFormatProvider) CultureInfo.CurrentCulture).ToShortDateString(), (IFormatProvider) CultureInfo.CurrentCulture);
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.i_ProductId = this.SetProductId(this.i_PlateTypeId);
        DataTable dataTable = new DataTable();
        this.Session["dtExport"] = (object) new VehicleMovementQueriesBL().SpecialPlateVehicleMovementGetAll(int32, dateTime1, dateTime2, v_PlateNew, this.i_ProductId, this.i_PlateTypeId, 0, 0, out int _);
        this.Export();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void Export()
    {
      string script = "ExportExcelAll();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
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

    protected void wdgList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
  }
}
