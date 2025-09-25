// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.CustomControls.RequirementData
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.Claim.BL;
using SIIV.Common.Resource;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims.CustomControls
{
  public class RequirementData : UserControl
  {
    protected UpdatePanel UpdatePanel1;
    protected Label lblRequirement;
    protected TextBox txtRequirement;
    protected Label Label1;
    protected DropDownList wddClaimMotive;
    protected Label lblOperationType;
    protected TextBox txtPlateNumber;
    protected Label lblOperationDate;
    protected TextBox txtProcessed;
    protected Label lblTerminal;
    protected TextBox txtProduct;
    protected Label lblUserId;
    protected Fecha wdpRegisterDate;
    protected Label Label2;
    protected TextBox txtnombresolicitante;
    protected Label Label3;
    protected TextBox txtapellidossolicitante;
    protected Label lblDocument;
    protected TextBox txtDocumentType;
    protected TextBox txtDocumentNumber;
    protected Label Label5;
    protected TextBox txtemail;
    protected Label Label6;
    protected TextBox txttel;
    protected Button wibValidate;
    protected Label lblMessage;
    protected Label lblTipoSolicitante;
    protected RadioButtonList rblTipoSolicitante;
    protected Label lblFirstName;
    protected TextBox txtFirstName;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected ValidatorCalloutExtender RequiredFieldValidator1_ValidatorCalloutExtender;
    protected RegularExpressionValidator RegularExpressionValidator1;
    protected ValidatorCalloutExtender RegularExpressionValidator1_ValidatorCalloutExtender;
    protected Label lblLastName;
    protected TextBox txtLastName;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected ValidatorCalloutExtender RequiredFieldValidator2_ValidatorCalloutExtender;
    protected Label Label4;
    protected DropDownList ddlDocumentType;
    protected TextBox txtDocumentNumberNew;
    protected FilteredTextBoxExtender txtDocumentNumber_FilteredTextBoxExtender;
    protected RequiredFieldValidator RequiredFieldValidator3;
    protected ValidatorCalloutExtender RequiredFieldValidator3_ValidatorCalloutExtender;
    protected Label lblEmail;
    protected TextBox txtEmailNew;
    protected Label lblTelephone;
    protected TextBox txtTelephone;
    protected FilteredTextBoxExtender txtTelephone_FilteredTextBoxExtender;

    public event RequirementData.DatosOK OnDatosOK;

    public event RequirementData.DatosWrong OnDatosWrong;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      this.wdpRegisterDate.Value = DateTime.Now;
      this.LoadParameters();
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.txtRequirement.Text.Length == 0)
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>Falta ingresar el Número de Solicitud");
      else if (this.wddClaimMotive.SelectedValue == "" || this.wddClaimMotive.SelectedValue == "-1")
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>Debe seleccionar el motivo del reclamo");
      else
        this.Validated();
    }

    public int i_ClaimMotiveId
    {
      get
      {
        return this.wddClaimMotive.SelectedValue == "" ? 0 : Convert.ToInt32(this.wddClaimMotive.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      }
    }

    protected void wddDocumentType_SelectionChanged(object sender, EventArgs e)
    {
      if (this.ddlDocumentType.SelectedValue == "1")
      {
        this.txtDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Numbers;
        this.txtDocumentNumberNew.MaxLength = 8;
      }
      else if (this.ddlDocumentType.SelectedValue == "4")
      {
        this.txtDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Numbers;
        this.txtDocumentNumberNew.MaxLength = 11;
      }
      else
      {
        this.txtDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Custom;
        this.txtDocumentNumber_FilteredTextBoxExtender.ValidChars = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz1234567890";
        this.txtDocumentNumberNew.MaxLength = 20;
      }
    }

    protected void txtDocumentNumberNew_TextChanged(object sender, EventArgs e)
    {
      string validCharacters = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz1234567890";
      if (!this.txtDocumentNumberNew.Text.All<char>((System.Func<char, bool>) (c => validCharacters.Contains<char>(c))))
      {
        this.lblMessage.Text = "El documento solo debe contener caracteres alfanuméricos únicos permitidos.";
        this.lblMessage.ForeColor = Color.Red;
        this.lblMessage.Visible = true;
        this.txtDocumentNumberNew.Text = "";
      }
      else
        this.lblMessage.Visible = false;
    }

    private void LoadParameters()
    {
      DataTable motive = new RequirementClaimQueriesBL().GetMotive(7);
      this.wddClaimMotive.Items.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) motive.Rows)
        this.wddClaimMotive.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
      this.wddClaimMotive.Items.Insert(0, new ListItem(SIIV.SystemParameter.BL.Constants.OPCIONLISTA_Seleccione, SIIV.SystemParameter.BL.Constants.OPCIONLISTA_ValorSinSeleccion));
      SystemParameterQueriesBL parameterQueriesBl = new SystemParameterQueriesBL();
      string str = "";
      string[] source = "1,2,3,4,32,19,33".Split(',');
      ArrayList arrFilter = new ArrayList()
      {
        (object) (str + SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
        (object) "",
        (object) "",
        (object) ""
      };
      DataTable dataTable = parameterQueriesBl.GetbyFilter(arrFilter);
      this.ddlDocumentType.Items.Clear();
      if (dataTable.Rows.Count <= 0)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        DataRow item = row;
        if (item["i_GroupId"].ToString() == SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture) && ((IEnumerable<string>) source).Where<string>((System.Func<string, bool>) (x => x == item["i_ParameterId"].ToString())).Count<string>() == 1)
          this.ddlDocumentType.Items.Add(new ListItem(item["v_Value"].ToString(), item["i_ParameterId"].ToString()));
      }
      this.ddlDocumentType.SelectedValue = "1";
    }

    private void Validated()
    {
      DataTable requirementApplicant = new RequirementClaimQueriesBL().GetDetailRequirementApplicant(Convert.ToInt32(this.txtRequirement.Text, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.wddClaimMotive.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
      if (this.ValidationMessage(requirementApplicant))
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Success, "[Datos Correctos]");
        this.Session["i_RequirementPlateId"] = requirementApplicant.Rows[0]["i_RequirementPlateId"];
        if (this.OnDatosOK != null)
          this.OnDatosOK((object) this);
        this.txtFirstName.Enabled = true;
        this.txtLastName.Enabled = true;
        this.rblTipoSolicitante.Enabled = true;
        this.ddlDocumentType.Enabled = true;
        this.txtDocumentNumberNew.Enabled = true;
        this.txtEmailNew.Enabled = true;
        this.txtTelephone.Enabled = true;
        this.SetearData(requirementApplicant);
      }
      else
      {
        if (this.OnDatosWrong != null)
          this.OnDatosWrong((object) this);
        this.ClearControls();
      }
    }

    private void SetearData(DataTable dt)
    {
      this.txtPlateNumber.Text = dt.Rows[0]["v_PlateNumber"].ToString();
      this.txtProcessed.Text = dt.Rows[0]["v_ProcessType"].ToString();
      this.txtProduct.Text = dt.Rows[0]["v_ProductName"].ToString();
      this.wdpRegisterDate.Value = Convert.IsDBNull(dt.Rows[0]["d_RegisterDate"]) ? DateTime.Now : Convert.ToDateTime(dt.Rows[0]["d_RegisterDate"]);
      this.txtnombresolicitante.Text = dt.Rows[0]["v_FirstName"].ToString();
      this.txtapellidossolicitante.Text = dt.Rows[0]["v_LastName"].ToString();
      this.txtDocumentType.Text = dt.Rows[0]["v_DocumentType"].ToString();
      this.txtDocumentNumber.Text = dt.Rows[0]["v_DocumentNumber"].ToString();
      this.txtemail.Text = dt.Rows[0]["v_Email"].ToString();
      this.txttel.Text = dt.Rows[0]["v_PhoneNumber"].ToString();
    }

    public bool CompletedData
    {
      get
      {
        return this.rblTipoSolicitante.SelectedIndex != -1 && this.txtFirstName.Text.Length > 0 && this.txtLastName.Text.Length > 0 && this.txtLastName.Text.Length > 0 && this.ddlDocumentType.SelectedIndex != -1 && this.txtDocumentNumberNew.Text.Length > 0 && this.txtEmailNew.Text.Length > 0 && this.txtTelephone.Text.Length > 0;
      }
    }

    private bool ValidationMessage(DataTable dt)
    {
      if (dt == null || dt.Rows.Count == 0)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>No se encontró ninguna Solicitud con el dato ingresado.");
        return false;
      }
      int? nullable = Convert.IsDBNull(dt.Rows[0]["i_DataBankId"]) ? new int?() : new int?(Convert.ToInt32(dt.Rows[0]["i_DataBankId"], (IFormatProvider) CultureInfo.CurrentCulture));
      int int32 = Convert.ToInt32(dt.Rows[0]["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture);
      Convert.ToInt32(dt.Rows[0]["i_StatusPayment"], (IFormatProvider) CultureInfo.CurrentCulture);
      string str = Convert.ToString(dt.Rows[0]["v_ProcessType"], (IFormatProvider) CultureInfo.CurrentCulture);
      bool boolean = Convert.ToBoolean(dt.Rows[0]["b_HasProcess"], (IFormatProvider) CultureInfo.CurrentCulture);
      if (!nullable.HasValue)
      {
        if (int32 != 4)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>La solicitud debe estar con estado Por Recoger.");
          return false;
        }
      }
      else if (nullable.HasValue && int32 != 4)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>La solicitud debe estar con estado Por Recoger.");
        return false;
      }
      if (str != "DUPLICADO DE PLACA")
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>La solicitud no pertenece al trámite de Duplicado de Placa. No es posible realizar este tipo de reclamo.");
        return false;
      }
      if (!boolean)
        return true;
      Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>No es posible generar un reclamo para esta solicitud. La solicitud ya cuenta con un reclamo en curso.");
      return false;
    }

    public string ValidateDataRequester()
    {
      string str = "";
      if (!this.txtEmailNew.Text.Contains("@"))
        str = "***Advertencia</br>Debe ingresar un correo electrónico válido";
      return str;
    }

    public string GetRequestTexts()
    {
      return "" + this.txtRequirement.Text + "|" + this.wddClaimMotive.SelectedValue + "|" + this.txtPlateNumber.Text + "|" + this.txtProcessed.Text + "|" + this.txtProduct.Text + "|" + this.wdpRegisterDate.Value.ToString() + "|" + this.txtnombresolicitante.Text + "|" + this.txtapellidossolicitante.Text + "|" + this.txtDocumentType.Text + "|" + this.txtDocumentNumber.Text + "|" + this.txtemail.Text + "|" + this.txttel.Text;
    }

    public string GetRequesterTexts()
    {
      return "" + this.rblTipoSolicitante.SelectedValue + "|" + this.txtFirstName.Text + "|" + this.txtLastName.Text + "|" + this.ddlDocumentType.SelectedValue + "|" + this.txtDocumentNumberNew.Text + "|" + this.txtEmailNew.Text + "|" + this.txtTelephone.Text + "|" + this.txtRequirement.Text;
    }

    public void SetTextsUpdateApplicantData(string strValues)
    {
      if (!(strValues != ""))
        return;
      string[] source = strValues.Split('|');
      if (((IEnumerable<string>) source).Count<string>() > 0)
        this.rblTipoSolicitante.SelectedValue = source[0];
      if (((IEnumerable<string>) source).Count<string>() > 1)
        this.txtFirstName.Text = source[1];
      if (((IEnumerable<string>) source).Count<string>() > 2)
        this.txtLastName.Text = source[2];
      if (((IEnumerable<string>) source).Count<string>() > 3)
        this.ddlDocumentType.SelectedValue = source[3];
      if (((IEnumerable<string>) source).Count<string>() > 4)
        this.txtDocumentNumberNew.Text = source[4];
      if (((IEnumerable<string>) source).Count<string>() > 5)
        this.txtEmailNew.Text = source[5];
      if (((IEnumerable<string>) source).Count<string>() > 6)
        this.txtTelephone.Text = source[6];
    }

    public void SetTexts(string strValues)
    {
      string[] source = strValues.Split('|');
      if (((IEnumerable<string>) source).Count<string>() > 0)
        this.txtRequirement.Text = source[0];
      if (((IEnumerable<string>) source).Count<string>() > 1)
        this.wddClaimMotive.SelectedValue = source[1];
      if (((IEnumerable<string>) source).Count<string>() > 2)
        this.txtPlateNumber.Text = source[2];
      if (((IEnumerable<string>) source).Count<string>() > 3)
        this.txtProcessed.Text = source[3];
      if (((IEnumerable<string>) source).Count<string>() > 4)
        this.txtProduct.Text = source[4];
      if (((IEnumerable<string>) source).Count<string>() > 5)
        this.wdpRegisterDate.Text = source[5];
      if (((IEnumerable<string>) source).Count<string>() > 6)
        this.txtnombresolicitante.Text = source[6];
      if (((IEnumerable<string>) source).Count<string>() > 7)
        this.txtapellidossolicitante.Text = source[7];
      if (((IEnumerable<string>) source).Count<string>() > 8)
        this.txtDocumentType.Text = source[8];
      if (((IEnumerable<string>) source).Count<string>() > 9)
        this.txtDocumentNumber.Text = source[9];
      if (((IEnumerable<string>) source).Count<string>() > 10)
        this.txtemail.Text = source[10];
      if (((IEnumerable<string>) source).Count<string>() <= 11)
        return;
      this.txttel.Text = source[11];
    }

    public void ClearControls()
    {
      this.txtRequirement.Text = "";
      this.txtPlateNumber.Text = "";
      this.txtProcessed.Text = "";
      this.txtProduct.Text = "";
      this.wdpRegisterDate.Value = DateTime.Now;
      this.wddClaimMotive.SelectedIndex = 0;
      this.txtnombresolicitante.Text = "";
      this.txtapellidossolicitante.Text = "";
      this.txtDocumentType.Text = "";
      this.txtDocumentNumber.Text = "";
      this.txtemail.Text = "";
      this.txttel.Text = "";
      this.txtFirstName.Enabled = false;
      this.txtLastName.Enabled = false;
      this.rblTipoSolicitante.Enabled = false;
      this.ddlDocumentType.Enabled = false;
      this.txtDocumentNumberNew.Enabled = false;
      this.txtEmailNew.Enabled = false;
      this.txtTelephone.Enabled = false;
      this.txtFirstName.Text = "";
      this.txtLastName.Text = "";
      this.rblTipoSolicitante.SelectedIndex = -1;
      this.ddlDocumentType.SelectedIndex = -1;
      this.txtDocumentNumberNew.Text = "";
      this.txtEmailNew.Text = "";
      this.txtTelephone.Text = "";
    }

    public void EnabledControls(bool enabled)
    {
      this.txtRequirement.Enabled = enabled;
      this.txtPlateNumber.Enabled = enabled;
      this.txtProcessed.Enabled = enabled;
      this.txtProduct.Enabled = enabled;
      this.wdpRegisterDate.Enabled = enabled;
      this.wddClaimMotive.Enabled = enabled;
      this.wibValidate.Enabled = enabled;
    }

    public void EnableControlsView(bool enabled)
    {
      this.txtRequirement.Enabled = enabled;
      this.txtPlateNumber.Enabled = enabled;
      this.txtProcessed.Enabled = enabled;
      this.txtProduct.Enabled = enabled;
      this.txtnombresolicitante.Enabled = enabled;
      this.txtapellidossolicitante.Enabled = enabled;
      this.txtDocumentType.Enabled = enabled;
      this.txtDocumentNumber.Enabled = enabled;
      this.txtemail.Enabled = enabled;
      this.txttel.Enabled = enabled;
      this.txtFirstName.Enabled = enabled;
      this.txtLastName.Enabled = enabled;
      this.txtDocumentNumberNew.Enabled = enabled;
      this.txtEmailNew.Enabled = enabled;
      this.txtTelephone.Enabled = enabled;
      this.wddClaimMotive.Enabled = enabled;
      this.ddlDocumentType.Enabled = enabled;
      this.rblTipoSolicitante.Enabled = enabled;
      this.wibValidate.Enabled = enabled;
    }

    public delegate void DatosOK(object sender);

    public delegate void DatosWrong(object sender);
  }
}
