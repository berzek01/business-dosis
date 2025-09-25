// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Account.RegisterPublicUser
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.SystemParameter.BL;
using SIIV.SystemUser.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Account
{
  public class RegisterPublicUser : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager WebScriptManager1;
    protected HiddenField validator;
    protected Label lblTituloPage;
    protected Label Label1;
    protected Label Label12;
    protected Label Label23;
    protected RadioButtonList rblPersonType;
    protected HtmlTable tagRequesterName;
    protected Label Label3;
    protected TextBox txtUserName;
    protected TextBoxWatermarkExtender txtUserName_TextBoxWatermarkExtender;
    protected RequiredFieldValidator ValidatorName;
    protected ValidatorCalloutExtender ValidatorName_ValidatorCalloutExtender;
    protected TextBox txtUserLastName01;
    protected TextBoxWatermarkExtender txtUserLastName01_TextBoxWatermarkExtender;
    protected RequiredFieldValidator ValidatorFirstName;
    protected ValidatorCalloutExtender ValidatorFirstName_ValidatorCalloutExtender;
    protected TextBox txtUserLastName02;
    protected TextBoxWatermarkExtender txtUserLastName02_TextBoxWatermarkExtender;
    protected RequiredFieldValidator ValidatorlastName;
    protected ValidatorCalloutExtender ValidatorlastName_ValidatorCalloutExtender;
    protected HtmlTable tagCompanyName;
    protected Label Label24;
    protected TextBox txtCompanyName;
    protected RequiredFieldValidator ValidatorCompany;
    protected ValidatorCalloutExtender ValidatorCompany_ValidatorCalloutExtender;
    protected Label Lbl;
    protected DropDownList wddCountry;
    protected Label Label6;
    protected DropDownList wddRegion;
    protected RequiredFieldValidator ValidatorDepartment;
    protected ValidatorCalloutExtender ValidatorDepartment_ValidatorCalloutExtender;
    protected Label Label5;
    protected DropDownList wddProvince;
    protected RequiredFieldValidator ValidatorProvince;
    protected ValidatorCalloutExtender ValidatorProvince_ValidatorCalloutExtender;
    protected Label Label7;
    protected DropDownList wddDistrict;
    protected RequiredFieldValidator ValidatorDistrict;
    protected ValidatorCalloutExtender ValidatorDistrict_ValidatorProvince_ValidatorCalloutExtender;
    protected Label Label8;
    protected TextBox txtUserAdress;
    protected RequiredFieldValidator ValidatorAdress;
    protected ValidatorCalloutExtender ValidatorAdress_ValidatorCalloutExtender;
    protected Label Label9;
    protected TextBox txtUserPhone;
    protected MaskedEditExtender txtUserPhone_MaskedEditExtender;
    protected RequiredFieldValidator ValidatorTelefono;
    protected ValidatorCalloutExtender ValidatorTelefono_ValidatorCalloutExtender;
    protected Label Label25;
    protected Label Label10;
    protected DropDownList wddUserDocumentType;
    protected TextBox txtUserDocumentNumber;
    protected FilteredTextBoxExtender txtUserDocumentNumber_FilteredTextBoxExtender;
    protected RequiredFieldValidator ValidatorDocumento;
    protected ValidatorCalloutExtender ValidatorDocumento_ValidatorCalloutExtender;
    protected Label Label15;
    protected Label Label16;
    protected TextBox txtUserAccount;
    protected RequiredFieldValidator ValidatorUserName;
    protected ValidatorCalloutExtender ValidatorUserName_ValidatorCalloutExtender;
    protected TextBox txtPassword;
    protected RequiredFieldValidator ValidatorPassword;
    protected ValidatorCalloutExtender ValidatorPassword_ValidatorCalloutExtender;
    protected PasswordStrength txtPassword_PasswordStrength;
    protected RegularExpressionValidator RegularExpressionValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender5;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender3;
    protected TextBox txtPasswordRepeat;
    protected CompareValidator CompareValidator1;
    protected ValidatorCalloutExtender CompareValidator1_ValidatorCalloutExtender;
    protected Label lblMessagePasword;
    protected Label Label17;
    protected TextBox txtEmail;
    protected TextBoxWatermarkExtender txtEmail_TextBoxWatermarkExtender;
    protected RequiredFieldValidator ValidatorEmail;
    protected ValidatorCalloutExtender ValidatorEmail_ValidatorCalloutExtender;
    protected RegularExpressionValidator RegularExpressionValidator1;
    protected ValidatorCalloutExtender RegularExpressionValidator1_ValidatorCalloutExtender;
    protected Label Label18;
    protected TextBox txtEmailRepeat;
    protected CompareValidator CompareValidator2;
    protected ValidatorCalloutExtender CompareValidator2_ValidatorCalloutExtender;
    protected Label Label19;
    protected Label Label20;
    protected TextBox txtSecretQuestion;
    protected RequiredFieldValidator ValidatorQuestion;
    protected ValidatorCalloutExtender ValidatorQuestion_ValidatorCalloutExtender;
    protected Label Label21;
    protected TextBox txtAnswer;
    protected RequiredFieldValidator ValidatorAnswer;
    protected ValidatorCalloutExtender ValidatorAnswer_ValidatorCalloutExtender;
    protected CheckBox chkAccept;
    protected Label Lblcheck;
    protected TextBox txtCheck;
    protected RequiredFieldValidator ValidatorCheck;
    protected ValidatorCalloutExtender ValidatorCheck_ValidatorCalloutExtender;
    protected Image Image2;
    protected Button btnRefreshCatcha;
    protected TextBox txtimgcode;
    protected RequiredFieldValidator ValidatorCatcha;
    protected ValidatorCalloutExtender ValidatorCatcha_ValidatorCalloutExtender;
    protected Label lblMessage;
    protected Button wibVolver;
    protected Button wibSendUser;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.DataBind();
      this.Initialize();
    }

    protected void wddRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.LoadWebDropDown(this.wddProvince, new UbigeoQueriesBL().GetProvince(this.wddRegion.SelectedValue), "v_description", "v_IdProvince", 0);
      this.wddDistrict.SelectedIndex = 0;
    }

    protected void wddProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.LoadWebDropDown(this.wddDistrict, new UbigeoQueriesBL().GetDistrict(this.wddRegion.SelectedValue, this.wddProvince.SelectedValue), "v_description", "v_IdDistrict", 0);
    }

    protected void rblPersonType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.rblPersonType.SelectedIndex == 0)
      {
        this.tagRequesterName.Visible = true;
        this.tagCompanyName.Visible = false;
        this.wddUserDocumentType.SelectedIndex = 0;
        this.wddUserDocumentType.Enabled = true;
      }
      else if (this.rblPersonType.SelectedIndex == 1)
      {
        this.tagRequesterName.Visible = false;
        this.tagCompanyName.Visible = true;
        this.wddUserDocumentType.SelectedValue = "4";
        this.wddUserDocumentType.Enabled = false;
      }
      this.wddUserDocumentType_SelectedIndexChanged((object) null, (EventArgs) null);
    }

    protected void wddUserDocumentType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.wddUserDocumentType.SelectedValue == "1")
      {
        this.txtUserDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Numbers;
        this.txtUserDocumentNumber.MaxLength = 8;
      }
      else if (this.wddUserDocumentType.SelectedValue == "4")
      {
        this.txtUserDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Numbers;
        this.txtUserDocumentNumber.MaxLength = 11;
      }
      else
      {
        this.txtUserDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Custom;
        this.txtUserDocumentNumber_FilteredTextBoxExtender.ValidChars = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz1234567890";
        this.txtUserDocumentNumber.MaxLength = 20;
      }
    }

    protected void wibSendUser_Click(object sender, EventArgs e)
    {
      int num = 0;
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      if (this.rblPersonType.SelectedIndex == 0 && this.txtUserName.Text.Trim() == string.Empty)
      {
        this.ValidatorName.IsValid = false;
        this.txtimgcode.Text = string.Empty;
      }
      else if (this.rblPersonType.SelectedIndex == 0 && this.txtUserLastName01.Text.Trim() == string.Empty)
      {
        this.ValidatorFirstName.IsValid = false;
        this.txtimgcode.Text = string.Empty;
      }
      else if (this.rblPersonType.SelectedIndex == 0 && this.txtUserLastName02.Text.Trim() == string.Empty)
      {
        this.ValidatorlastName.IsValid = false;
        this.txtimgcode.Text = string.Empty;
      }
      else if (this.rblPersonType.SelectedIndex == 1 && this.txtCompanyName.Text.Trim() == string.Empty)
      {
        this.ValidatorCompany.IsValid = false;
        this.txtimgcode.Text = string.Empty;
      }
      else if (this.wddRegion.SelectedIndex == 0)
      {
        this.ValidatorDepartment.IsValid = false;
        this.txtimgcode.Text = string.Empty;
      }
      else if (this.wddProvince.SelectedIndex == 0)
      {
        this.ValidatorProvince.IsValid = false;
        this.txtimgcode.Text = string.Empty;
      }
      else if (this.wddDistrict.SelectedIndex == 0)
      {
        this.ValidatorDistrict.IsValid = false;
        this.txtimgcode.Text = string.Empty;
      }
      else if (this.txtUserAdress.Text.Trim() == string.Empty)
      {
        this.ValidatorAdress.IsValid = false;
        this.txtimgcode.Text = string.Empty;
      }
      else if (this.txtUserPhone.Text.Replace("-", "").Length != 9)
      {
        this.ValidatorTelefono.IsValid = false;
        this.txtimgcode.Text = string.Empty;
      }
      else if (this.wddUserDocumentType.SelectedValue == "1" && (this.txtUserDocumentNumber.Text.Length < 8 || this.txtUserDocumentNumber.Text.Length > 8))
      {
        this.ValidatorDocumento.IsValid = false;
        this.ValidatorDocumento.ErrorMessage = "Ingrese su número de documento.";
        this.txtimgcode.Text = string.Empty;
      }
      else if (this.wddUserDocumentType.SelectedValue == "4" && (this.txtUserDocumentNumber.Text.Length < 10 || !Format.ValidateRUCstructure(this.txtUserDocumentNumber.Text)))
      {
        this.ValidatorDocumento.IsValid = false;
        this.ValidatorDocumento.ErrorMessage = "Ingrese un número de RUC Válido.";
        this.txtimgcode.Text = string.Empty;
      }
      else if (this.txtUserAccount.Text.Trim() == string.Empty)
      {
        this.ValidatorUserName.IsValid = false;
        this.ValidatorUserName.ErrorMessage = "Debe ingresar un nombre de Usuario.";
        this.txtimgcode.Text = string.Empty;
      }
      else if (this.txtPassword.Text.Trim() == string.Empty)
      {
        this.ValidatorPassword.IsValid = false;
        this.txtimgcode.Text = string.Empty;
      }
      else if (this.txtPasswordRepeat.Text.Trim() == string.Empty)
      {
        this.CompareValidator1.IsValid = false;
        this.txtPasswordRepeat.Focus();
        this.txtimgcode.Text = string.Empty;
      }
      else if (this.txtEmail.Text.Trim() == string.Empty)
      {
        this.ValidatorEmail.IsValid = false;
        this.ValidatorEmail.ErrorMessage = "Ingrese su cuenta de correo electrónico.";
        this.txtimgcode.Text = string.Empty;
      }
      else if (this.txtEmailRepeat.Text.Trim() == string.Empty)
      {
        this.CompareValidator2.IsValid = false;
        this.txtEmailRepeat.Focus();
        this.txtimgcode.Text = string.Empty;
      }
      else if (this.txtSecretQuestion.Text.Trim() == string.Empty)
      {
        this.ValidatorQuestion.IsValid = false;
        this.txtimgcode.Text = string.Empty;
      }
      else if (this.txtAnswer.Text.Trim() == string.Empty)
      {
        this.ValidatorAnswer.IsValid = false;
        this.txtimgcode.Text = string.Empty;
      }
      else if (!this.chkAccept.Checked)
      {
        this.ValidatorCheck.IsValid = false;
        this.txtimgcode.Text = string.Empty;
      }
      else if (this.txtPassword.Text.Length <= 7)
        this.RequiredFieldValidator2.IsValid = false;
      else if (this.txtimgcode.Text != string.Empty)
      {
        if (this.Session["CaptchaImageText"].ToString() != this.txtimgcode.Text.ToString())
        {
          this.ValidatorCatcha.IsValid = false;
          this.ValidatorCatcha.ErrorMessage = "Los digitos ingresados no concuerdan con la imagen mostrada.";
          this.txtimgcode.Text = string.Empty;
        }
        else
        {
          SystemUserQueriesBL systemUserQueriesBl = new SystemUserQueriesBL();
          SIIV.BE.SystemUser objSystemUser = new SIIV.BE.SystemUser();
          objSystemUser.v_Alias = this.txtUserAccount.Text;
          objSystemUser.v_DocumentNumber = this.txtUserDocumentNumber.Text;
          objSystemUser.v_Email = this.txtEmail.Text;
          ref int local1 = ref num;
          ref string local2 = ref empty1;
          systemUserQueriesBl.UserValidateRegister(objSystemUser, ref local1, ref local2);
          switch (num)
          {
            case -3:
              this.ValidatorDocumento.IsValid = false;
              this.ValidatorDocumento.ErrorMessage = empty1;
              this.txtimgcode.Text = string.Empty;
              break;
            case -2:
              this.ValidatorUserName.IsValid = false;
              this.ValidatorUserName.ErrorMessage = empty1;
              this.txtimgcode.Text = string.Empty;
              break;
            case -1:
              this.ValidatorEmail.IsValid = false;
              this.ValidatorEmail.ErrorMessage = empty1;
              this.txtimgcode.Text = string.Empty;
              break;
            default:
              string str = this.wddRegion.SelectedValue + this.wddProvince.SelectedValue + this.wddDistrict.SelectedValue;
              try
              {
                SIIV.BE.SystemUser pobjBE = new SIIV.BE.SystemUser();
                pobjBE.i_SystemUserId = 0;
                pobjBE.i_CompanyId = new int?(2);
                pobjBE.v_Alias = this.txtUserAccount.Text;
                pobjBE.v_Password = this.txtPassword.Text;
                pobjBE.v_LastName = this.rblPersonType.SelectedIndex == 0 ? this.txtUserLastName01.Text.Trim() + " " + this.txtUserLastName02.Text.Trim() : "";
                pobjBE.v_FirstName = this.rblPersonType.SelectedIndex == 0 ? this.txtUserName.Text : this.txtCompanyName.Text;
                pobjBE.i_DocumentTypeId = new int?(Convert.ToInt32(this.wddUserDocumentType.SelectedValue));
                pobjBE.v_DocumentNumber = this.txtUserDocumentNumber.Text;
                pobjBE.v_Email = this.txtEmail.Text;
                pobjBE.v_Telephone = this.txtUserPhone.Text;
                pobjBE.v_Address = this.txtUserAdress.Text;
                pobjBE.v_Ubigeo = str;
                pobjBE.v_Question_Answer = this.txtSecretQuestion.Text + "|" + this.txtAnswer.Text;
                pobjBE.i_SystemUserRefId = new int?(0);
                pobjBE.i_Status = new int?(0);
                pobjBE.i_InsertUserId = new int?(0);
                pobjBE.d_InsertDate = new DateTime?(DateTime.Today);
                pobjBE.i_PersonTypeId = new int?(Convert.ToInt32(this.rblPersonType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
                if (new SystemUserManagementBL().Insert(ref pobjBE) <= 0)
                  throw new Exception("*** Problemas al enviar solicitud : <br> Por favor intente mas tarde. <br><br> La administración");
                if (new UserConfigManagementBL().Insert(ref new UserConfig()
                {
                  i_UserConfigId = 0,
                  i_SystemUserId = new int?(pobjBE.i_SystemUserId),
                  i_RoleConfigId = new int?(Convert.ToInt32(ConfigDA.ReadConfig("RolepublicUserId"), (IFormatProvider) CultureInfo.CurrentCulture)),
                  i_LocationId = new int?(0),
                  i_Status = new int?(1),
                  i_InsertUserId = new int?(0),
                  d_InsertDate = new DateTime?(DateTime.Today)
                }) > 0)
                {
                  this.Session.Add("RegisterSystemUser", (object) pobjBE);
                  this.Response.Redirect("~/Account/RegisterMessage.aspx", false);
                  break;
                }
                new SystemUserManagementBL().DeleteObject(pobjBE.i_SystemUserId);
                throw new Exception("*** Problemas al enviar solicitud : <br> Por favor intente mas tarde. <br><br> La administración");
              }
              catch (Exception ex)
              {
                Message.SetMessage(this.lblMessage, enmMessageType.Error, ex.Message);
                break;
              }
          }
        }
      }
      else
      {
        this.ValidatorCatcha.IsValid = false;
        this.ValidatorCatcha.ErrorMessage = "Los digitos ingresados no concuerdan con la imagen mostrada.";
        this.txtimgcode.Text = string.Empty;
      }
    }

    private void Initialize()
    {
      this.LoadWebDropDown(this.wddRegion, new UbigeoQueriesBL().GetRegion(), "v_description", "v_IdRegion", 0);
      this.LoadWebDropDown(this.wddProvince, new UbigeoQueriesBL().GetProvince(this.wddRegion.SelectedValue), "v_description", "v_IdProvince", 0);
      this.LoadWebDropDown(this.wddDistrict, new UbigeoQueriesBL().GetDistrict(this.wddRegion.SelectedValue, this.wddProvince.SelectedValue), "v_description", "v_IdDistrict", 0);
      this.LoadWebDropDown(this.wddUserDocumentType, new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      }), "v_description", "i_ParameterId", 0);
    }

    private void LoadWebDropDown(
      DropDownList pwddControl,
      DataTable pdtDataSource,
      string pstrTextField,
      string pstrValueField,
      int pintSelectIndex)
    {
      pwddControl.DataSource = (object) pdtDataSource;
      pwddControl.DataTextField = pstrTextField;
      pwddControl.DataValueField = pstrValueField;
      pwddControl.DataBind();
      pwddControl.SelectedIndex = pintSelectIndex;
    }

    protected void btnRefreshCatcha_Click(object sender, EventArgs e)
    {
      new Image().ImageUrl = "~/UserControls/FrmCaptcha.aspx";
    }

    protected void wibVolver_Click(object sender, EventArgs e)
    {
      this.txtimgcode.Text = string.Empty;
      this.Response.Redirect("~/index.aspx");
    }

    public enum PasswordScore
    {
      VeryPoor,
      Weak,
      Average,
      Strong,
      Excellent,
    }
  }
}
