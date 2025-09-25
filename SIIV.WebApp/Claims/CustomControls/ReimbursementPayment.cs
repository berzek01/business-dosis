// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.CustomControls.ReimbursementPayment
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.Claim.BL;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims.CustomControls
{
  public class ReimbursementPayment : UserControl
  {
    protected UpdatePanel UpdatePanel1;
    protected Label lblMotivo;
    protected DropDownList wddMotive;
    protected Label lblbank;
    protected DropDownList wddBank;
    protected Label lblOperationType;
    protected RadioButton rbWindows;
    protected RadioButton rbOthers;
    protected Label lblOperationDate;
    protected Fecha wdpOperationDate;
    protected Label lblTerminal;
    protected TextBox txtTerminal;
    protected Label lblUserId;
    protected TextBox txtUserId;
    protected Label Label1;
    protected TextBox txtPrice;
    protected Button wibValidate;
    protected Label lblMessage;
    protected Label lblFirstName;
    protected TextBox txtFirstName;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected Label lblLastName;
    protected TextBox txtLastName;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected ValidatorCalloutExtender RequiredFieldValidator2_ValidatorCalloutExtender;
    protected Label lblDocument;
    protected DropDownList wddDocumentType;
    protected TextBox txtDocumentNumber;
    protected FilteredTextBoxExtender txtDocumentNumber_FilteredTextBoxExtender;
    protected RequiredFieldValidator RequiredFieldValidator3;
    protected ValidatorCalloutExtender RequiredFieldValidator3_ValidatorCalloutExtender;
    protected Label lblEmail;
    protected TextBox txtEmail;
    protected RegularExpressionValidator RegularExpressionValidator1;
    protected ValidatorCalloutExtender RegularExpressionValidator1_ValidatorCalloutExtender;
    protected Label lblTelephone;
    protected TextBox txtTelephone;
    protected FilteredTextBoxExtender txtTelephone_FilteredTextBoxExtender;
    protected Label lblPuntoE;
    protected DropDownList wddLocation;
    protected Label Label3;
    protected TextBox txtHolderName;
    protected RequiredFieldValidator RequiredFieldValidator4;
    protected Label Label6;
    protected TextBox txtHolderBankName;
    protected Label Label7;
    protected TextBox txtCtaDocumentNumber;

    public event ReimbursementPayment.DatosOK OnDatosOK;

    public event ReimbursementPayment.DatosWrong OnDatosWrong;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadParameters();
      this.LoadBanks();
      this.LoadParametersDocumentType();
      this.LoadLocation();
      this.txtDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Numbers;
      this.txtDocumentNumber.MaxLength = 8;
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      int num;
      if (!(this.wddBank.SelectedValue == "") && (this.rbWindows.Checked || this.rbOthers.Checked))
      {
        DateTime dateTime = this.wdpOperationDate.Value;
        if (this.txtUserId.Text.Length != 0)
        {
          num = this.txtPrice.Text.Length == 0 ? 1 : 0;
          goto label_4;
        }
      }
      num = 1;
label_4:
      if (num != 0)
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>Faltan ingresar datos para la validación del VOUCHER");
      else
        this.Validated();
    }

    protected void wddDocumentType_SelectionChanged(object sender, EventArgs e)
    {
      if (this.wddDocumentType.SelectedValue == "1")
      {
        this.txtDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Numbers;
        this.txtDocumentNumber.MaxLength = 8;
      }
      else if (this.wddDocumentType.SelectedValue == "4")
      {
        this.txtDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Numbers;
        this.txtDocumentNumber.MaxLength = 11;
      }
      else
      {
        this.txtDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Custom;
        this.txtDocumentNumber_FilteredTextBoxExtender.ValidChars = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz1234567890";
        this.txtDocumentNumber.MaxLength = 20;
      }
    }

    public int i_ClaimMotiveId
    {
      get
      {
        return this.wddMotive.SelectedValue == "" ? 0 : Convert.ToInt32(this.wddMotive.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      }
    }

    private void LoadParameters()
    {
      DataTable motive = new RequirementClaimQueriesBL().GetMotive(6);
      this.wddMotive.Items.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) motive.Rows)
        this.wddMotive.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
      this.wddMotive.Items.Insert(0, new System.Web.UI.WebControls.ListItem(SIIV.SystemParameter.BL.Constants.OPCIONLISTA_Seleccione, SIIV.SystemParameter.BL.Constants.OPCIONLISTA_ValorSinSeleccion));
    }

    public void LoadBanks()
    {
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) ("" + SystemParameterGroups.AffiliatedBank.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
        (object) "",
        (object) "1",
        (object) "1"
      });
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          this.wddBank.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
      }
      this.wddBank.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- Seleccione - ", "0"));
    }

    private void LoadLocation()
    {
      DataTable deliveryPoint = new RequirementClaimQueriesBL().GetDeliveryPoint();
      this.wddLocation.Items.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) deliveryPoint.Rows)
        this.wddLocation.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_LocationId"].ToString()));
      this.wddLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem(SIIV.SystemParameter.BL.Constants.OPCIONLISTA_Seleccione, SIIV.SystemParameter.BL.Constants.OPCIONLISTA_ValorSinSeleccion));
    }

    private void LoadParametersDocumentType()
    {
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) ("" + SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
        (object) "",
        (object) "1",
        (object) "1"
      });
      this.wddDocumentType.Items.Clear();
      if (dataTable.Rows.Count <= 0)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (row["i_GroupId"].ToString() == SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture))
          this.wddDocumentType.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Value"].ToString(), row["i_ParameterId"].ToString()));
      }
      this.wddDocumentType.SelectedValue = "1";
    }

    private void Validated()
    {
      if (this.ValidationMessage(new RequirementClaimQueriesBL().GetDetailVoucher(Convert.ToInt32(this.wddBank.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), this.rbWindows.Checked ? 1 : 0, Convert.ToDateTime((object) this.wdpOperationDate.Value, (IFormatProvider) CultureInfo.CurrentCulture), this.txtTerminal.Text.TrimEnd(), this.txtUserId.Text.TrimEnd(), Convert.ToDecimal(this.txtPrice.Text, (IFormatProvider) CultureInfo.CurrentCulture))))
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Success, "[Datos Correctos]");
        this.EnabledControlsTitular(true);
        if (this.OnDatosOK == null)
          return;
        this.OnDatosOK((object) this);
      }
      else
      {
        this.EnabledControlsTitular(false);
        if (this.OnDatosWrong != null)
          this.OnDatosWrong((object) this);
      }
    }

    public void EnabledControlsTitular(bool enabled)
    {
      this.txtFirstName.Enabled = enabled;
      this.txtLastName.Enabled = enabled;
      this.wddDocumentType.Enabled = enabled;
      this.txtDocumentNumber.Enabled = enabled;
      this.txtEmail.Enabled = enabled;
      this.txtTelephone.Enabled = enabled;
      this.txtHolderName.Enabled = enabled;
      this.txtHolderBankName.Enabled = enabled;
      this.txtCtaDocumentNumber.Enabled = enabled;
      this.wddLocation.Enabled = enabled;
    }

    public string ValidateDataRequester()
    {
      string str = "";
      if (this.wddDocumentType.SelectedValue == "4")
      {
        if (this.txtDocumentNumber.Text.Length < 10 || !Format.ValidateRUCstructure(this.txtDocumentNumber.Text))
          str = "***Advertencia</br>El N° de RUC ingresado no es válido";
      }
      else if (this.wddDocumentType.SelectedValue == "1" && (this.txtDocumentNumber.Text.Length < 8 || this.txtDocumentNumber.Text.Length > 8))
        str = "***Advertencia</br>El N° de DNI ingresado no es válido";
      else if (this.txtTelephone.Text.Length == 0 && this.txtEmail.Text.Length == 0)
        str = "***Advertencia</br>Debe ingresar un Email o Número de Teléfono";
      else if (this.txtTelephone.Text.Length > 0 && this.txtTelephone.Text.Length > 0 && this.txtTelephone.Text.Length != 9)
        str = "***Advertencia</br>Debe válido un Número de Teléfono o Celular válido";
      return str;
    }

    public bool CompletedData
    {
      get
      {
        return this.wddBank.SelectedValue != "0" && (this.rbWindows.Checked || this.rbOthers.Checked) && this.wddMotive.SelectedValue != "-1" && this.wddMotive.SelectedValue != "" && this.txtUserId.Text.Length > 0;
      }
    }

    private bool ValidationMessage(DataTable dt)
    {
      if (Convert.ToInt32(dt.Rows[0]["idVoucher"], (IFormatProvider) CultureInfo.CurrentCulture) == 0)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>EL VOUCHER HA SIDO ENCONTRATO PERO EL MONTO NO COINCIDE CON EL DATO INGRESADO");
        return false;
      }
      if (Convert.ToInt32(dt.Rows[0]["idVoucher"], (IFormatProvider) CultureInfo.CurrentCulture) == -1)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>NO SE HA ENCONTRADO NINGUN VOUCHER CON LOS DATOS INGRESADO");
        return false;
      }
      if (Convert.ToInt32(dt.Rows[0]["IdVoucher"], (IFormatProvider) CultureInfo.CurrentCulture) == -2)
      {
        int int32 = Convert.ToInt32(dt.Rows[0]["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture);
        string str = dt.Rows[0]["v_PlateNew"].ToString();
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>Voucher ha sido acreditado con solicitud # " + int32.ToString() + " - Placa " + str);
        return false;
      }
      if (Convert.ToInt32(dt.Rows[0]["IdVoucher"], (IFormatProvider) CultureInfo.CurrentCulture) == -3)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>SU PAGO NO PUEDE SER VALIDADO POR TENER UNA ANTIGUEDAD MAYOR A 60 DIAS, COMUNIQUESE CON LA APP.");
        return false;
      }
      if (Convert.ToInt32(dt.Rows[0]["IdVoucher"], (IFormatProvider) CultureInfo.CurrentCulture) == -4)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>EL PAGO NO SE PUEDE ACREDITAR, HA SIDO REEMBOLSADO.");
        return false;
      }
      if (Convert.ToInt32(dt.Rows[0]["IdVoucher"], (IFormatProvider) CultureInfo.CurrentCulture) == -5)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>EL PAGO NO SE PUEDE ACREDITAR, HA SIDO OBSERVADO POR AAP. POR FAVOR COMUNIQUESE CON CASOS ESPECIALES.");
        return false;
      }
      if (Convert.ToInt32(dt.Rows[0]["IdVoucher"], (IFormatProvider) CultureInfo.CurrentCulture) == -7)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>EL VOUCHER HA SIDO OBTENIDO CON ERROR");
        return false;
      }
      if (Convert.ToInt32(dt.Rows[0]["IdVoucher"], (IFormatProvider) CultureInfo.CurrentCulture) == -8)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>EL VOUCHER SE ENCUENTRA EN UN PROCESO DE RECLAMO");
        return false;
      }
      this.ViewState["i_IdBanco"] = dt.Rows[0]["IdVoucher"];
      return true;
    }

    private void ShowAlert(string Msg)
    {
      string script = "ShowAlert('" + Msg + "');";
      ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    public string GetTexts()
    {
      return "" + this.wddBank.SelectedValue + "|" + (this.rbWindows.Checked ? "1" : "0") + "|" + this.wdpOperationDate.Value.ToString() + "|" + this.txtTerminal.Text + "|" + this.txtUserId.Text + "|" + (this.ViewState["i_IdBanco"] != null ? this.ViewState["i_IdBanco"].ToString() : "") + "|" + this.txtPrice.Text;
    }

    public string GetTextsRequester()
    {
      return "" + this.txtFirstName.Text + "|" + this.txtLastName.Text + "|" + this.wddDocumentType.SelectedValue + "|" + this.txtDocumentNumber.Text + "|" + this.txtEmail.Text + "|" + this.txtTelephone.Text + "|" + this.txtHolderName.Text + "|" + this.txtHolderBankName.Text + "|" + this.txtCtaDocumentNumber.Text + "|" + this.wddLocation.SelectedValue;
    }

    public void SetTexts(string strValues)
    {
      this.LoadBanks();
      this.LoadParameters();
      if (strValues.Length <= 0)
        return;
      string[] source = strValues.Split('|');
      if (((IEnumerable<string>) source).Count<string>() > 0)
        this.wddBank.SelectedValue = source[0];
      if (((IEnumerable<string>) source).Count<string>() > 1)
      {
        this.rbWindows.Checked = source[1] == "1";
        this.rbOthers.Checked = !(source[1] == "1");
      }
      if (((IEnumerable<string>) source).Count<string>() > 2)
        this.wdpOperationDate.Value = Convert.ToDateTime(source[2], (IFormatProvider) CultureInfo.CurrentCulture);
      if (((IEnumerable<string>) source).Count<string>() > 3)
        this.txtTerminal.Text = source[3];
      if (((IEnumerable<string>) source).Count<string>() > 4)
        this.txtUserId.Text = source[4];
      if (((IEnumerable<string>) source).Count<string>() > 6)
        this.txtPrice.Text = source[6];
    }

    public void SetTextsRequester(string strValues)
    {
      if (strValues.Length <= 0)
        return;
      string[] source = strValues.Split('|');
      if (((IEnumerable<string>) source).Count<string>() > 0)
        this.txtFirstName.Text = source[0];
      if (((IEnumerable<string>) source).Count<string>() > 1)
        this.txtLastName.Text = source[1];
      if (((IEnumerable<string>) source).Count<string>() > 2)
        this.wddDocumentType.SelectedValue = source[2];
      if (((IEnumerable<string>) source).Count<string>() > 3)
        this.txtDocumentNumber.Text = source[3];
      if (((IEnumerable<string>) source).Count<string>() > 4)
        this.txtEmail.Text = source[4];
      if (((IEnumerable<string>) source).Count<string>() > 5)
        this.txtTelephone.Text = source[5];
      if (((IEnumerable<string>) source).Count<string>() > 6)
        this.txtHolderName.Text = source[6];
      if (((IEnumerable<string>) source).Count<string>() > 7)
        this.txtHolderBankName.Text = source[7];
      if (((IEnumerable<string>) source).Count<string>() > 8)
        this.txtCtaDocumentNumber.Text = source[8];
      if (((IEnumerable<string>) source).Count<string>() > 9)
        this.wddLocation.SelectedValue = source[9];
    }

    public void SetMotive(int? i_claimmotiveid)
    {
      this.wddMotive.SelectedValue = i_claimmotiveid.ToString();
    }

    public void ClearControls()
    {
      this.wddBank.SelectedValue = "0";
      this.wddMotive.SelectedValue = "-1";
      this.rbWindows.Checked = false;
      this.rbOthers.Checked = false;
      this.wdpOperationDate.Value = DateTime.Now;
      this.txtTerminal.Text = "";
      this.txtUserId.Text = "";
      this.lblMessage.Text = "";
      this.txtHolderName.Text = "";
      this.txtHolderBankName.Text = "";
      this.txtCtaDocumentNumber.Text = "";
      this.wddLocation.SelectedValue = "-1";
    }

    public void EnabledControls(bool enabled)
    {
      this.wddBank.Enabled = enabled;
      this.wddMotive.Enabled = enabled;
      this.rbWindows.Enabled = enabled;
      this.rbOthers.Enabled = enabled;
      this.wdpOperationDate.Enabled = enabled;
      this.txtTerminal.Enabled = enabled;
      this.txtUserId.Enabled = enabled;
      this.txtPrice.Enabled = enabled;
      this.wibValidate.Enabled = enabled;
    }

    public delegate void DatosOK(object sender);

    public delegate void DatosWrong(object sender);
  }
}
