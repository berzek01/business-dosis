// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Operation.ChangesAssociated1
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.BE;
using SIIV.BE.CustomCode;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.Exhibition.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Operation
{
  public class ChangesAssociated1 : Page
  {
    private ExhibitionAssociated pobjexhibitionassociated;
    private AssociatedQueriesBL pbojAssociatedQueriesBL;
    private string v_Option = "";
    protected UpdatePanel UpdatePanel2;
    protected TextBox txtAliasValidar;
    protected FilteredTextBoxExtender ftbeAlias;
    protected Button wibValidate;
    protected Button btnReturnPopupConfirmation;
    protected Button btnReturnPopupConfirmation2;
    protected Label lblMessageValidar;
    protected Label Label;
    protected CheckBox chkLegal;
    protected CheckBox chkNatural;
    protected Label Label26;
    protected DropDownList wddTypeAssociated;
    protected RequiredFieldValidator RequiredFieldValidator12;
    protected ValidatorCalloutExtender ValidatorCalloutExtender8;
    protected Label Label1;
    protected TextBox txtCorporateName;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected ValidatorCalloutExtender ValidatorCalloutExtender1;
    protected FilteredTextBoxExtender ftbeCorporate;
    protected Label Label3;
    protected DropDownList wddDocumentType;
    protected RequiredFieldValidator RequiredFieldValidator10;
    protected ValidatorCalloutExtender ValidatorCalloutExtender12;
    protected Label Label4;
    protected TextBox txtDocumentNumber;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender2;
    protected Label Label16;
    protected TextBox txtAddress;
    protected RequiredFieldValidator RequiredFieldValidator3;
    protected ValidatorCalloutExtender ValidatorCalloutExtender3;
    protected Label Label17;
    protected TextBox txtTelephone;
    protected RequiredFieldValidator RequiredFieldValidator4;
    protected ValidatorCalloutExtender ValidatorCalloutExtender4;
    protected MaskedEditExtender txtTelephone_MaskedEditExtender;
    protected Label Label25;
    protected Label Label9;
    protected TextBox txtFax;
    protected Label Label27;
    protected TextBox txtAcronym;
    protected Label Label12;
    protected TextBox txtUserName;
    protected RequiredFieldValidator RequiredFieldValidator5;
    protected ValidatorCalloutExtender ValidatorCalloutExtender5;
    protected HtmlTableRow trModifyPassword;
    protected LinkButton lnkChangePassword;
    protected HtmlTableRow trPassword;
    protected Label Label2;
    protected TextBox txtPassword;
    protected RequiredFieldValidator RequiredFieldValidator6;
    protected ValidatorCalloutExtender ValidatorCalloutExtender6;
    protected Label Label8;
    protected TextBox txtElectronicItemNumber;
    protected Label Label10;
    protected DropDownList wddRegistryZone;
    protected Label Label11;
    protected TextBox txtEmail;
    protected RequiredFieldValidator RequiredFieldValidator13;
    protected RegularExpressionValidator RegularExpressionValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender7;
    protected ValidatorCalloutExtender ValidatorCalloutExtender15;
    protected Label Label28;
    protected TextBox txtEmail2;
    protected RegularExpressionValidator RegularExpressionValidator1;
    protected ValidatorCalloutExtender ValidatorCalloutExtender14;
    protected Label Label7;
    protected TextBox txtWebSite;
    protected Label Label5;
    protected TextBox txtRepresentativeName;
    protected RequiredFieldValidator RequiredFieldValidator7;
    protected ValidatorCalloutExtender ValidatorCalloutExtender9;
    protected Label Label6;
    protected TextBox txtCharge;
    protected RequiredFieldValidator RequiredFieldValidator8;
    protected ValidatorCalloutExtender ValidatorCalloutExtender10;
    protected Label Label14;
    protected DropDownList wddDocumentTypeRepresentative;
    protected RequiredFieldValidator RequiredFieldValidator11;
    protected ValidatorCalloutExtender ValidatorCalloutExtender13;
    protected Label Label15;
    protected TextBox txtDocumentNumberRepresentative;
    protected RequiredFieldValidator RequiredFieldValidator9;
    protected ValidatorCalloutExtender ValidatorCalloutExtender11;
    protected Label Label13;
    protected TextBox txtRepresentativeName2;
    protected Label Label18;
    protected TextBox txtCharge2;
    protected Label Label19;
    protected DropDownList wddDocumentTypeRepresentative2;
    protected Label Label20;
    protected TextBox txtDocumentNumberRepresentative2;
    protected Label Label21;
    protected TextBox txtRepresentativeName3;
    protected Label Label22;
    protected TextBox txtCharge3;
    protected Label Label23;
    protected DropDownList wddDocumentTypeRepresentative3;
    protected Label Label24;
    protected TextBox txtDocumentNumberRepresentative3;
    protected Label lblMessage;
    protected Button wibSave;
    protected Button wibDelete;
    protected Button wibEnable;
    protected Button wibDisable;
    protected Button wibReturn;
    protected Button wibAgreement;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.Initialize();
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

    protected void wibValidate_Click(object sender, EventArgs e)
    {
      try
      {
        if (!(this.txtAliasValidar.Text != ""))
          throw new HandledException(1, "Debe ingresar un usuario para realizar la validación");
        this.Validar();
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessageValidar, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessageValidar, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void chkLegal_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.chkLegal.Checked)
        return;
      this.chkNatural.Checked = false;
    }

    protected void chkNatural_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.chkNatural.Checked)
        return;
      this.chkLegal.Checked = false;
    }

    protected void lnkChangePassword_Click(object sender, EventArgs e)
    {
      this.trModifyPassword.Visible = false;
      this.trPassword.Visible = true;
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      try
      {
        this.lblMessageValidar.Text = "";
        if ((this.txtRepresentativeName2.Text != "" || this.txtCharge2.Text != "" || this.wddDocumentTypeRepresentative2.SelectedIndex != 0 || this.txtDocumentNumberRepresentative2.Text != "") && (this.txtRepresentativeName2.Text == "" || this.txtCharge2.Text == "" || this.wddDocumentTypeRepresentative2.SelectedIndex == 0 || this.txtDocumentNumberRepresentative2.Text == ""))
          throw new HandledException(1, "Debe completar los datos de los representantes.");
        if ((this.txtRepresentativeName3.Text != "" || this.txtCharge3.Text != "" || this.wddDocumentTypeRepresentative3.SelectedIndex != 0 || this.txtDocumentNumberRepresentative3.Text != "") && (this.txtRepresentativeName3.Text == "" || this.txtCharge3.Text == "" || this.wddDocumentTypeRepresentative3.SelectedIndex == 0 || this.txtDocumentNumberRepresentative3.Text == ""))
          throw new HandledException(1, "Debe competar los datos de los representantes.");
        if ((this.txtRepresentativeName3.Text != "" || this.txtCharge3.Text != "" || this.wddDocumentTypeRepresentative3.SelectedIndex != 0 || this.txtDocumentNumberRepresentative3.Text != "") && (this.txtRepresentativeName2.Text == "" || this.txtCharge2.Text == "" || this.wddDocumentTypeRepresentative2.SelectedIndex == 0 || this.txtDocumentNumberRepresentative2.Text == ""))
          throw new HandledException(1, "Debe llenar todos los datos de los representantes.");
        if (this.Request.QueryString["i_SystemUserId"] != null)
        {
          int int32_1 = Convert.ToInt32(this.Request.QueryString["i_SystemUserId"].ToString());
          this.pobjexhibitionassociated = new ExhibitionAssociated();
          this.pobjexhibitionassociated.i_SystemUserId = int32_1;
          this.pobjexhibitionassociated.i_PersonTypeId = this.chkNatural.Checked ? 1 : 2;
          this.pobjexhibitionassociated.v_ReasonSocial = this.txtCorporateName.Text;
          this.pobjexhibitionassociated.v_UserName = this.txtUserName.Text;
          if (this.txtPassword.Text.Length > 0)
            this.pobjexhibitionassociated.v_Password = Cryptography.GetHashMD5(this.txtPassword.Text.TrimEnd());
          this.pobjexhibitionassociated.i_DocumentTypeId = Convert.ToInt32(this.wddDocumentType.SelectedValue);
          this.pobjexhibitionassociated.v_DocumentNumber = this.txtDocumentNumber.Text;
          this.pobjexhibitionassociated.v_Address = this.txtAddress.Text;
          this.pobjexhibitionassociated.v_Telephone = this.txtTelephone.Text;
          this.pobjexhibitionassociated.v_Fax = this.txtFax.Text;
          int int32_2 = Convert.ToInt32(this.wddTypeAssociated.SelectedValue.ToString());
          this.pobjexhibitionassociated.v_RepresentativeName = this.txtRepresentativeName.Text + "|" + this.txtRepresentativeName2.Text + "|" + this.txtRepresentativeName3.Text;
          string[] strArray = new string[5];
          int selectedIndex = this.wddDocumentTypeRepresentative.SelectedIndex;
          strArray[0] = selectedIndex.ToString();
          strArray[1] = "0";
          selectedIndex = this.wddDocumentTypeRepresentative2.SelectedIndex;
          string str1;
          if (!(selectedIndex.ToString() == "0"))
          {
            selectedIndex = this.wddDocumentTypeRepresentative2.SelectedIndex;
            str1 = selectedIndex.ToString();
          }
          else
            str1 = "9";
          strArray[2] = str1;
          strArray[3] = "0";
          selectedIndex = this.wddDocumentTypeRepresentative3.SelectedIndex;
          string str2;
          if (!(selectedIndex.ToString() == "0"))
          {
            selectedIndex = this.wddDocumentTypeRepresentative3.SelectedIndex;
            str2 = selectedIndex.ToString();
          }
          else
            str2 = "9";
          strArray[4] = str2;
          this.pobjexhibitionassociated.i_RepresentativeDocumentTypeId = Convert.ToInt32(string.Concat(strArray));
          this.pobjexhibitionassociated.v_RepresentativeDocumentNumber = this.txtDocumentNumberRepresentative.Text + "|" + this.txtDocumentNumberRepresentative2.Text + "|" + this.txtDocumentNumberRepresentative3.Text;
          this.pobjexhibitionassociated.v_ElectronicItemNumber = this.txtElectronicItemNumber.Text;
          this.pobjexhibitionassociated.i_RegistryZoneId = Convert.ToInt32(this.wddRegistryZone.SelectedValue);
          string v_Acronym = this.txtAcronym.Text.Trim();
          this.pobjexhibitionassociated.v_Charge = this.txtCharge.Text + "|" + this.txtCharge2.Text + "|" + this.txtCharge3.Text;
          this.pobjexhibitionassociated.v_Email = this.txtEmail.Text.Trim() + "|" + this.txtEmail2.Text.Trim();
          this.pobjexhibitionassociated.v_WebSite = this.txtWebSite.Text;
          this.ValidTransaction(new AssociatedManagementBL().AssociatedUpdate(this.pobjexhibitionassociated, int32_2, v_Acronym), "modificado");
        }
        else
        {
          SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - ChangesAssociated1.aspx");
          this.pobjexhibitionassociated = new ExhibitionAssociated();
          this.pobjexhibitionassociated.i_PersonTypeId = this.chkNatural.Checked ? 1 : 2;
          this.pobjexhibitionassociated.v_ReasonSocial = this.txtCorporateName.Text;
          this.pobjexhibitionassociated.v_UserName = this.txtUserName.Text;
          if (this.txtPassword.Text.Length > 0)
            this.pobjexhibitionassociated.v_Password = Cryptography.GetHashMD5(this.txtPassword.Text.TrimEnd());
          this.pobjexhibitionassociated.i_DocumentTypeId = Convert.ToInt32(this.wddDocumentType.SelectedValue);
          this.pobjexhibitionassociated.v_DocumentNumber = this.txtDocumentNumber.Text;
          this.pobjexhibitionassociated.v_Address = this.txtAddress.Text;
          this.pobjexhibitionassociated.v_Telephone = this.txtTelephone.Text;
          this.pobjexhibitionassociated.v_Fax = this.txtFax.Text;
          int int32 = Convert.ToInt32(this.wddTypeAssociated.SelectedValue.ToString());
          this.pobjexhibitionassociated.v_RepresentativeName = this.txtRepresentativeName.Text + "|" + this.txtRepresentativeName2.Text + "|" + this.txtRepresentativeName3.Text;
          string[] strArray = new string[5];
          int selectedIndex = this.wddDocumentTypeRepresentative.SelectedIndex;
          strArray[0] = selectedIndex.ToString();
          strArray[1] = "0";
          selectedIndex = this.wddDocumentTypeRepresentative2.SelectedIndex;
          string str3;
          if (!(selectedIndex.ToString() == "0"))
          {
            selectedIndex = this.wddDocumentTypeRepresentative2.SelectedIndex;
            str3 = selectedIndex.ToString();
          }
          else
            str3 = "9";
          strArray[2] = str3;
          strArray[3] = "0";
          selectedIndex = this.wddDocumentTypeRepresentative3.SelectedIndex;
          string str4;
          if (!(selectedIndex.ToString() == "0"))
          {
            selectedIndex = this.wddDocumentTypeRepresentative3.SelectedIndex;
            str4 = selectedIndex.ToString();
          }
          else
            str4 = "9";
          strArray[4] = str4;
          this.pobjexhibitionassociated.i_RepresentativeDocumentTypeId = Convert.ToInt32(string.Concat(strArray));
          this.pobjexhibitionassociated.v_RepresentativeDocumentNumber = this.txtDocumentNumberRepresentative.Text + "|" + this.txtDocumentNumberRepresentative2.Text + "|" + this.txtDocumentNumberRepresentative3.Text;
          this.pobjexhibitionassociated.v_ElectronicItemNumber = this.txtElectronicItemNumber.Text;
          this.pobjexhibitionassociated.i_RegistryZoneId = Convert.ToInt32(this.wddRegistryZone.SelectedValue);
          string v_Acronym = this.txtAcronym.Text.Trim();
          this.pobjexhibitionassociated.v_Charge = this.txtCharge.Text + "|" + this.txtCharge2.Text + "|" + this.txtCharge3.Text;
          this.pobjexhibitionassociated.v_Email = this.txtEmail.Text.Trim() + "|" + this.txtEmail2.Text.Trim();
          this.pobjexhibitionassociated.v_WebSite = this.txtWebSite.Text;
          int i_AssociatedFatherId = 0;
          int i_CompanyId = 2;
          int i_Status = 1;
          int iSystemUserId = systemUser.i_SystemUserId;
          DateTime now = DateTime.Now;
          int i_RoleConfigId = this.RoleConfiguration(Convert.ToInt32(this.Request.QueryString["i_PlateTypeId"]));
          int i_LocationId = 14;
          this.pbojAssociatedQueriesBL = new AssociatedQueriesBL();
          this.v_Option = "03";
          if (this.pbojAssociatedQueriesBL.ExhibitionAssociatedVerifyAlias(this.txtUserName.Text, this.v_Option, this.ViewState["i_plateTypeId"].ToString()).Rows.Count > 0)
            throw new HandledException(1, "Nombre de Usuario, ya Existe");
          string i_CustomerTypeId = this.ViewState["i_plateTypeId"].ToString();
          int i_Result = new AssociatedManagementBL().AssociatedCreate(this.pobjexhibitionassociated, int32, v_Acronym, i_AssociatedFatherId, i_CompanyId, i_Status, iSystemUserId, now, i_RoleConfigId, i_LocationId, i_CustomerTypeId);
          if (i_Result > 0)
          {
            List<string> EmailTo = new List<string>();
            EmailTo.Add(this.txtEmail.Text.ToString());
            string postrMessage = "";
            this.SendEMail(EmailTo, ref postrMessage);
            string v_Action = "creado";
            this.ValidTransaction(i_Result, v_Action);
          }
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

    protected void wibDelete_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.Request.QueryString["i_SystemUserId"] == null)
          return;
        this.ChangesState(Convert.ToInt32(this.Request.QueryString["i_SystemUserId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture), -1, "eliminado");
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

    protected void wibEnable_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.Request.QueryString["i_SystemUserId"] == null)
          return;
        this.ChangesState(Convert.ToInt32(this.Request.QueryString["i_SystemUserId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture), 1, "habilitado");
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

    protected void wibDisable_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.Request.QueryString["i_SystemUserId"] == null)
          return;
        this.ChangesState(Convert.ToInt32(this.Request.QueryString["i_SystemUserId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture), -2, "deshabilitado");
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

    protected void wibReturn_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("RequirementAssociated.aspx?t=" + Convert.ToInt32(this.ViewState["i_plateTypeId"]).ToString());
    }

    protected void wibAgreement_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable dataTable1 = new DataTable();
        string str1 = this.ViewState["i_plateTypeId"].ToString();
        if (this.Request.QueryString["i_SystemUserId"] == null)
          return;
        DataTable dataTable2 = new AssociatedQueriesBL().ExhibitionAssociatedAgreement(Convert.ToInt32(this.Request.QueryString["i_SystemUserId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture));
        string[] strArray1 = dataTable2.Rows[0]["v_Document1"].ToString().Split('0');
        DataTable dataTable3 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.PersonDocumentType.ToString(),
          (object) "",
          (object) "1",
          (object) "1"
        });
        string str2 = "";
        List<string> stringList = new List<string>();
        foreach (string str3 in strArray1)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable3.Rows)
          {
            char ch = str3[0];
            int num;
            if (!(ch.ToString() != ""))
            {
              ch = str3[0];
              num = ch.ToString() != string.Empty ? 1 : 0;
            }
            else
              num = 1;
            if (num != 0)
            {
              ch = str3[0];
              if (ch.ToString() == row["i_ParameterId"].ToString())
                str2 = str2 + row["v_Description"].ToString() + "/";
            }
          }
        }
        if (!dataTable2.Columns.Contains("v_Message"))
          dataTable2.Columns.Add("v_Message", Type.GetType("System.String"));
        if (!dataTable2.Columns.Contains("v_Firma"))
          dataTable2.Columns.Add("v_Firma", Type.GetType("System.String"));
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
        {
          string str4 = dataTable2.Rows[0]["v_RepresentativeName"].ToString();
          string str5 = dataTable2.Rows[0]["v_Charge"].ToString();
          string str6 = dataTable2.Rows[0]["v_RepresentativeDocumentNumber"].ToString();
          string str7 = dataTable2.Rows[0]["v_ReasonSocial"].ToString();
          string[] strArray2 = str4.Split('/');
          string[] strArray3 = str5.Split('/');
          string[] strArray4 = str6.Split('/');
          string[] strArray5 = str2.ToString().Split('/');
          StringBuilder stringBuilder1 = new StringBuilder();
          StringBuilder stringBuilder2 = new StringBuilder();
          if (strArray2.Length == 1)
          {
            stringBuilder1.AppendLine("debidamente representada por su " + strArray3[0] + ", el señor(a) " + strArray2[0] + ", identificados con " + strArray5[0] + " Nº " + strArray4[0]);
            stringBuilder2.AppendLine("……………………………………");
            stringBuilder2.AppendLine(strArray2[0]);
            stringBuilder2.AppendLine(strArray3[0]);
            stringBuilder2.AppendLine(str7);
          }
          if (strArray2.Length == 2)
          {
            if (strArray2[0] != "" && strArray2[1] == "")
            {
              stringBuilder1.AppendLine("debidamente representada por su " + strArray3[0] + ", el señor(a) " + strArray2[0] + ", identificados con " + strArray5[0] + " Nº " + strArray4[0]);
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[0]);
              stringBuilder2.AppendLine(strArray3[0]);
              stringBuilder2.AppendLine(str7);
            }
            else if (strArray2[0] != "" && strArray2[1] != "")
            {
              stringBuilder1.AppendLine("debidamente representada por su " + strArray3[0] + ", el señor(a) " + strArray2[0] + ", identificados con " + strArray5[0] + " Nº " + strArray4[0] + " y por su " + strArray3[1] + ", el señor(a) " + strArray2[1] + ", identificados con " + strArray5[1] + " Nº " + strArray4[1]);
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[0]);
              stringBuilder2.AppendLine(strArray3[0]);
              stringBuilder2.AppendLine(str7);
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[1]);
              stringBuilder2.AppendLine(strArray3[1]);
              stringBuilder2.AppendLine(str7);
            }
          }
          if (strArray2.Length == 3)
          {
            if (strArray2[0] != "" && strArray2[1] == "" && strArray2[2] == "")
            {
              stringBuilder1.AppendLine("debidamente representada por su " + strArray3[0] + ", el señor(a) " + strArray2[0] + ", identificados con " + strArray5[0] + " Nº " + strArray4[0]);
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[0]);
              stringBuilder2.AppendLine(strArray3[0]);
              stringBuilder2.AppendLine(str7);
            }
            else if (strArray2[0] != "" && strArray2[1] != "" && strArray2[2] == "")
            {
              stringBuilder1.AppendLine("debidamente representada por su " + strArray3[0] + ", el señor(a) " + strArray2[0] + ", identificados con " + strArray5[0] + " Nº " + strArray4[0] + " y por su " + strArray3[1] + ", el señor(a) " + strArray2[1] + ", identificados con " + strArray5[1] + " Nº " + strArray4[1]);
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[0]);
              stringBuilder2.AppendLine(strArray3[0]);
              stringBuilder2.AppendLine(str7);
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[1]);
              stringBuilder2.AppendLine(strArray3[1]);
              stringBuilder2.AppendLine(str7);
            }
            else if (strArray2[0] != "" && strArray2[1] != "" && strArray2[2] != "")
            {
              stringBuilder1.AppendLine("debidamente representada por su " + strArray3[0] + ", el señor(a) " + strArray2[0] + ", identificados con " + strArray5[0] + " Nº " + strArray4[0] + " , por su " + strArray3[1] + ", el señor(a) " + strArray2[1] + ", identificados con " + strArray5[1] + " Nº " + strArray4[1] + " y por su " + strArray3[2] + ", el señor(a) " + strArray2[2] + ", identificados con " + strArray5[2] + " Nº " + strArray4[2]);
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[0]);
              stringBuilder2.AppendLine(strArray3[0]);
              stringBuilder2.AppendLine(str7);
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[1]);
              stringBuilder2.AppendLine(strArray3[1]);
              stringBuilder2.AppendLine(str7);
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[2]);
              stringBuilder2.AppendLine(strArray3[2]);
              stringBuilder2.AppendLine(str7);
            }
          }
          row["v_Message"] = (object) stringBuilder1.ToString();
          row["v_Firma"] = (object) stringBuilder2.ToString();
        }
        if (dataTable2.Rows.Count <= 0)
          throw new HandledException(1, "Debe tener por lo menos una solicitud de Placas Especiales para imprimir el Convenio");
        string str8 = dataTable2.Rows[0]["i_IsAssociatedAAP"].ToString();
        switch (str1)
        {
          case "7":
            if (str8.Equals("1", StringComparison.CurrentCulture))
            {
              ReportDocument reportDocument = new ReportDocument();
              string filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportAssociatedAgreement.rpt";
              reportDocument.Load(filename);
              reportDocument.SetDataSource(dataTable2);
              reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "Asociado");
              break;
            }
            if (str8.Equals("0", StringComparison.CurrentCulture))
            {
              ReportDocument reportDocument = new ReportDocument();
              string filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportNotAssociatedAgreement.rpt";
              reportDocument.Load(filename);
              reportDocument.SetDataSource(dataTable2);
              reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "NoAsociado");
              break;
            }
            if (str8.Equals("2", StringComparison.CurrentCulture))
            {
              ReportDocument reportDocument = new ReportDocument();
              string filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportImporterAgreement.rpt";
              reportDocument.Load(filename);
              reportDocument.SetDataSource(dataTable2);
              reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "Importador");
              break;
            }
            break;
          case "11":
            if (str8.Equals("1", StringComparison.CurrentCulture))
            {
              ReportDocument reportDocument = new ReportDocument();
              string filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportAssociatedAgreementRotate.rpt";
              reportDocument.Load(filename);
              reportDocument.SetDataSource(dataTable2);
              reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "Asociado");
            }
            else if (str8.Equals("0", StringComparison.CurrentCulture))
            {
              ReportDocument reportDocument = new ReportDocument();
              string filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportNotAssociatedAgreementRotate2.rpt";
              reportDocument.Load(filename);
              reportDocument.SetDataSource(dataTable2);
              reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "NoAsociado");
            }
            else if (str8.Equals("2", StringComparison.CurrentCulture))
            {
              ReportDocument reportDocument = new ReportDocument();
              string filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportImporterAgreementRotate.rpt";
              reportDocument.Load(filename);
              reportDocument.SetDataSource(dataTable2);
              reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "Importador");
            }
            break;
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

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      try
      {
        this.lblMessage.Text = "";
        this.lblMessageValidar.Text = "";
        this.LoadUser();
        this.DisableText(true);
        this.wibSave.Enabled = true;
        this.txtPassword.Visible = true;
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

    protected void btnReturnPopupConfirmation2_Click(object sender, EventArgs e)
    {
      try
      {
        this.ViewState["SupraRotate"] = (object) null;
        this.txtAliasValidar.Text = "";
        this.DisableText(true);
        this.wibSave.Enabled = true;
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessageValidar, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessageValidar, new HandledException(-100, ex));
      }
    }

    public void Initialize()
    {
      try
      {
        this.ViewState["i_plateTypeId"] = (object) this.Request.QueryString["i_PlateTypeId"].ToString();
        this.ViewState["SupraRotate"] = (object) null;
        this.Label2.Visible = true;
        this.txtPassword.Visible = true;
        if (this.Request.QueryString["i_SystemUserId"] != null)
        {
          this.DisableText(true);
          this.txtAliasValidar.Visible = false;
          this.wibValidate.Visible = false;
        }
        else
        {
          this.DisableText(false);
          this.txtAliasValidar.Visible = true;
          this.wibValidate.Visible = true;
        }
        this.LoadParameters();
        this.LoadDataAssociated();
        this.ConfigureDropDownList();
        if (this.Request.QueryString["i_Status"] == null)
          return;
        this.DisableControls(Convert.ToInt32(this.Request.QueryString["i_Status"].ToString()));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ConfigureDropDownList()
    {
      int currentUserRoleId = this.GetCurrentUserRoleId();
      this.wddTypeAssociated.Items.Clear();
      System.Web.UI.WebControls.ListItem listItem1 = new System.Web.UI.WebControls.ListItem("Asociado AAP", "1");
      System.Web.UI.WebControls.ListItem listItem2 = new System.Web.UI.WebControls.ListItem("No Asociado", "0");
      System.Web.UI.WebControls.ListItem listItem3 = new System.Web.UI.WebControls.ListItem("Importador", "2");
      switch (currentUserRoleId)
      {
        case 45:
          this.wddTypeAssociated.Items.Add(listItem1);
          this.wddTypeAssociated.Items.Add(listItem2);
          this.wddTypeAssociated.Items.Add(listItem3);
          this.wddTypeAssociated.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --"));
          break;
        case 51:
          this.wddTypeAssociated.Items.Add(listItem1);
          this.wddTypeAssociated.Items.Add(listItem2);
          this.wddTypeAssociated.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --"));
          break;
      }
      if (string.IsNullOrEmpty(this.Request.QueryString["i_SystemUserId"]))
        return;
      DataTable dtresult = new AssociatedQueriesBL().AssociatedRead(Convert.ToInt32(this.Request.QueryString["i_SystemUserId"]));
      this.SetAssociated(dtresult);
      this.wddTypeAssociated.SelectedValue = Convert.ToInt32(dtresult.Rows[0]["i_IsAssociatedAAP"]).ToString();
    }

    private int GetCurrentUserRoleId()
    {
      return this.Session["SystemUser"] != null ? ((SystemUser) this.Session["SystemUser"]).i_RoleConfigId : -1;
    }

    private void CleanText()
    {
      this.txtCorporateName.Text = "";
      this.txtUserName.Text = "";
      this.txtPassword.Text = "";
      this.txtDocumentNumber.Text = "";
      this.txtAddress.Text = "";
      this.txtTelephone.Text = "";
      this.txtFax.Text = "";
      this.txtRepresentativeName.Text = "";
      this.txtRepresentativeName2.Text = "";
      this.txtRepresentativeName3.Text = "";
      this.txtDocumentNumberRepresentative.Text = "";
      this.txtDocumentNumberRepresentative2.Text = "";
      this.txtDocumentNumberRepresentative3.Text = "";
      this.txtElectronicItemNumber.Text = "";
      this.txtAcronym.Text = "";
      this.txtCharge.Text = "";
      this.txtCharge2.Text = "";
      this.txtCharge3.Text = "";
      this.txtEmail.Text = "";
      this.txtEmail2.Text = "";
      this.txtWebSite.Text = "";
    }

    private void DisableText(bool Enab)
    {
      this.chkNatural.Enabled = Enab;
      this.txtCorporateName.Enabled = Enab;
      this.txtUserName.Enabled = Enab;
      this.txtPassword.Enabled = Enab;
      this.wddDocumentType.Enabled = Enab;
      this.txtDocumentNumber.Enabled = Enab;
      this.txtAddress.Enabled = Enab;
      this.txtTelephone.Enabled = Enab;
      this.txtFax.Enabled = Enab;
      this.wddTypeAssociated.Enabled = Enab;
      this.txtRepresentativeName.Enabled = Enab;
      this.txtRepresentativeName2.Enabled = Enab;
      this.txtRepresentativeName3.Enabled = Enab;
      this.wddDocumentTypeRepresentative.Enabled = Enab;
      this.wddDocumentTypeRepresentative2.Enabled = Enab;
      this.wddDocumentTypeRepresentative3.Enabled = Enab;
      this.txtDocumentNumberRepresentative.Enabled = Enab;
      this.txtDocumentNumberRepresentative2.Enabled = Enab;
      this.txtDocumentNumberRepresentative3.Enabled = Enab;
      this.txtElectronicItemNumber.Enabled = Enab;
      this.wddRegistryZone.Enabled = Enab;
      this.txtAcronym.Enabled = Enab;
      this.txtCharge.Enabled = Enab;
      this.txtCharge2.Enabled = Enab;
      this.txtCharge3.Enabled = Enab;
      this.txtEmail.Enabled = Enab;
      this.txtEmail2.Enabled = Enab;
      this.txtWebSite.Enabled = Enab;
    }

    private void LoadParameters()
    {
      try
      {
        List<SIIV.BE.SystemParameter> systemParameterList = new SystemParameterManagementBL().Get((object) new ArrayList()
        {
          (object) ("" + SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ", " + SystemParameterGroups.RegistrationZone.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
          (object) "",
          (object) "1",
          (object) "1"
        });
        this.wddDocumentTypeRepresentative.Items.Clear();
        this.wddDocumentTypeRepresentative2.Items.Clear();
        this.wddDocumentTypeRepresentative3.Items.Clear();
        this.wddRegistryZone.Items.Clear();
        this.wddDocumentType.Items.Clear();
        foreach (SIIV.BE.SystemParameter systemParameter in systemParameterList)
        {
          if (systemParameter.i_GroupId == SystemParameterGroups.RegistrationZone)
            this.wddRegistryZone.Items.Add(new System.Web.UI.WebControls.ListItem(systemParameter.v_Description, systemParameter.i_ParameterId.ToString()));
          else if (systemParameter.i_GroupId == SystemParameterGroups.PersonDocumentType)
          {
            this.wddDocumentType.Items.Add(new System.Web.UI.WebControls.ListItem(systemParameter.v_Description, systemParameter.i_ParameterId.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
            ListItemCollection items1 = this.wddDocumentTypeRepresentative.Items;
            string vDescription1 = systemParameter.v_Description;
            int iParameterId = systemParameter.i_ParameterId;
            string str1 = iParameterId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
            System.Web.UI.WebControls.ListItem listItem1 = new System.Web.UI.WebControls.ListItem(vDescription1, str1);
            items1.Add(listItem1);
            ListItemCollection items2 = this.wddDocumentTypeRepresentative2.Items;
            string vDescription2 = systemParameter.v_Description;
            iParameterId = systemParameter.i_ParameterId;
            string str2 = iParameterId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
            System.Web.UI.WebControls.ListItem listItem2 = new System.Web.UI.WebControls.ListItem(vDescription2, str2);
            items2.Add(listItem2);
            this.wddDocumentTypeRepresentative3.Items.Add(new System.Web.UI.WebControls.ListItem(systemParameter.v_Description, systemParameter.i_ParameterId.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
          }
        }
        this.wddDocumentType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --"));
        this.wddDocumentTypeRepresentative.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --"));
        this.wddDocumentTypeRepresentative2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --"));
        this.wddDocumentTypeRepresentative3.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --"));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadDataAssociated()
    {
      try
      {
        AssociatedQueriesBL associatedQueriesBl = new AssociatedQueriesBL();
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - ChangesAssociated1.aspx");
        if (this.Session["ApplicationId"] != null)
        {
          int int32 = Convert.ToInt32(this.Session["ApplicationId"]);
          string userExtendedAction = associatedQueriesBl.GetSystemUserExtendedAction(systemUser.i_SystemUserId, int32);
          if (userExtendedAction != "")
          {
            if (!(Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("7"))) == "7"))
            {
              this.txtCorporateName.Enabled = false;
              this.wddDocumentType.Enabled = false;
              this.txtDocumentNumber.Enabled = false;
            }
          }
          else
          {
            this.txtCorporateName.Enabled = false;
            this.wddDocumentType.Enabled = false;
            this.txtDocumentNumber.Enabled = false;
          }
        }
        if (this.Request.QueryString["i_SystemUserId"] != null)
        {
          DataTable dtresult = new AssociatedQueriesBL().AssociatedRead(Convert.ToInt32(this.Request.QueryString["i_SystemUserId"].ToString()));
          if (dtresult == null || dtresult.Rows.Count == 0)
            return;
          this.SetAssociated(dtresult);
          this.trModifyPassword.Visible = true;
          this.trPassword.Visible = false;
        }
        else
        {
          this.trModifyPassword.Visible = false;
          this.trPassword.Visible = true;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SetAssociated(DataTable dtresult)
    {
      try
      {
        DataRow row = dtresult.Rows[0];
        int int32_1 = Convert.ToInt32(row["i_PersonTypeId"], (IFormatProvider) CultureInfo.CurrentCulture);
        int int32_2 = Convert.ToInt32(row["i_IsAssociatedAAP"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        string str1 = row["v_ReasonSocial"].ToString();
        int int32_3 = Convert.ToInt32(row["i_DocumentTypeId"], (IFormatProvider) CultureInfo.CurrentCulture);
        string str2 = row["v_DocumentNumber"].ToString();
        string str3 = row["v_Address"].ToString();
        string str4 = row["v_Telephone"].ToString();
        string str5 = row["v_Fax"].ToString();
        string str6 = row["v_Acronym"].ToString();
        string[] strArray1 = row["v_RepresentativeName"].ToString().Split('|');
        if (strArray1.Length == 1)
        {
          if (string.IsNullOrEmpty(strArray1[0].ToString()))
            strArray1[0] = "";
          else
            this.txtRepresentativeName.Text = strArray1[0].ToString();
        }
        if (strArray1.Length == 2)
        {
          if (string.IsNullOrEmpty(strArray1[0].ToString()))
            strArray1[0] = "";
          else
            this.txtRepresentativeName.Text = strArray1[0].ToString();
          if (string.IsNullOrEmpty(strArray1[1].ToString()))
            strArray1[1] = "";
          else
            this.txtRepresentativeName2.Text = strArray1[1].ToString();
        }
        if (strArray1.Length == 3)
        {
          if (string.IsNullOrEmpty(strArray1[0].ToString()))
            strArray1[0] = "";
          else
            this.txtRepresentativeName.Text = strArray1[0].ToString();
          if (string.IsNullOrEmpty(strArray1[1].ToString()))
            strArray1[1] = "";
          else
            this.txtRepresentativeName2.Text = strArray1[1].ToString();
          if (string.IsNullOrEmpty(strArray1[2].ToString()))
            strArray1[2] = "";
          else
            this.txtRepresentativeName3.Text = strArray1[2].ToString();
        }
        string[] strArray2 = row["v_Charge"].ToString().Split('|');
        if (strArray2.Length == 1)
        {
          if (string.IsNullOrEmpty(strArray2[0].ToString()))
            strArray2[0] = "";
          else
            this.txtCharge.Text = strArray2[0].ToString();
        }
        if (strArray2.Length == 2)
        {
          if (string.IsNullOrEmpty(strArray2[0].ToString()))
            strArray2[0] = "";
          else
            this.txtCharge.Text = strArray2[0].ToString();
          if (string.IsNullOrEmpty(strArray2[1].ToString()))
            strArray2[1] = "";
          else
            this.txtCharge2.Text = strArray2[1].ToString();
        }
        if (strArray2.Length == 3)
        {
          if (string.IsNullOrEmpty(strArray2[0].ToString()))
            strArray2[0] = "";
          else
            this.txtCharge.Text = strArray2[0].ToString();
          if (string.IsNullOrEmpty(strArray2[1].ToString()))
            strArray2[1] = "";
          else
            this.txtCharge2.Text = strArray2[1].ToString();
          if (string.IsNullOrEmpty(strArray2[2].ToString()))
            strArray2[2] = "";
          else
            this.txtCharge3.Text = strArray2[2].ToString();
        }
        string[] strArray3 = row["v_RepresentativeDocumentNumber"].ToString().Split('|');
        if (strArray3.Length == 1)
        {
          if (string.IsNullOrEmpty(strArray3[0].ToString()))
            strArray3[0] = "";
          else
            this.txtDocumentNumberRepresentative.Text = strArray3[0].ToString();
        }
        if (strArray3.Length == 2)
        {
          if (string.IsNullOrEmpty(strArray3[0].ToString()))
            strArray3[0] = "";
          else
            this.txtDocumentNumberRepresentative.Text = strArray3[0].ToString();
          if (string.IsNullOrEmpty(strArray3[1].ToString()))
            strArray3[1] = "";
          else
            this.txtDocumentNumberRepresentative2.Text = strArray3[1].ToString();
        }
        if (strArray3.Length == 3)
        {
          if (string.IsNullOrEmpty(strArray3[0].ToString()))
            strArray3[0] = "";
          else
            this.txtDocumentNumberRepresentative.Text = strArray3[0].ToString();
          if (string.IsNullOrEmpty(strArray3[1].ToString()))
            strArray3[1] = "";
          else
            this.txtDocumentNumberRepresentative2.Text = strArray3[1].ToString();
          if (string.IsNullOrEmpty(strArray3[2].ToString()))
            strArray3[2] = "";
          else
            this.txtDocumentNumberRepresentative3.Text = strArray3[2].ToString();
        }
        string[] strArray4 = Convert.ToInt32(row["i_RepresentativeDocumentTypeId"], (IFormatProvider) CultureInfo.CurrentCulture).ToString().Split('0');
        if (strArray4.Length == 1)
        {
          if (string.IsNullOrEmpty(strArray4[0].ToString()))
            strArray4[0] = "";
          else
            this.wddDocumentTypeRepresentative.SelectedValue = strArray4[0].ToString();
        }
        if (strArray4.Length == 2)
        {
          if (string.IsNullOrEmpty(strArray4[0].ToString()))
            strArray4[0] = "";
          else
            this.wddDocumentTypeRepresentative.SelectedValue = strArray4[0].ToString();
          if (string.IsNullOrEmpty(strArray4[1].ToString()))
            strArray4[1] = "";
          else
            this.wddDocumentTypeRepresentative2.SelectedValue = strArray4[1].ToString();
        }
        if (strArray4.Length == 3)
        {
          if (string.IsNullOrEmpty(strArray4[0].ToString()))
            strArray4[0] = "";
          else
            this.wddDocumentTypeRepresentative.SelectedValue = strArray4[0].ToString();
          if (string.IsNullOrEmpty(strArray4[1].ToString()))
            strArray4[1] = "";
          else
            this.wddDocumentTypeRepresentative2.SelectedValue = strArray4[1].ToString();
          if (string.IsNullOrEmpty(strArray4[2].ToString()))
            strArray4[2] = "";
          else
            this.wddDocumentTypeRepresentative3.SelectedValue = strArray4[2].ToString();
        }
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string[] strArray5 = row["v_Email"].ToString().Split('|');
        if (strArray5.Length == 1)
        {
          if (string.IsNullOrEmpty(strArray5[0].ToString((IFormatProvider) CultureInfo.CurrentCulture)))
            strArray5[0] = "";
          else
            empty1 = strArray5[0].ToString((IFormatProvider) CultureInfo.CurrentCulture);
        }
        if (strArray5.Length == 2)
        {
          if (string.IsNullOrEmpty(strArray5[0].ToString((IFormatProvider) CultureInfo.CurrentCulture)))
            strArray5[0] = "";
          else
            empty1 = strArray5[0].ToString((IFormatProvider) CultureInfo.CurrentCulture);
          if (string.IsNullOrEmpty(strArray5[1].ToString((IFormatProvider) CultureInfo.CurrentCulture)))
            strArray5[1] = "";
          else
            empty2 = strArray5[1].ToString((IFormatProvider) CultureInfo.CurrentCulture);
        }
        string str7 = row["v_WebSite"].ToString();
        string str8 = row["v_ElectronicItemNumber"].ToString();
        int int32_4 = Convert.ToInt32(row["i_RegistryZoneId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        string str9 = row["v_UserName"].ToString();
        this.chkLegal.Checked = int32_1 == 2;
        this.chkNatural.Checked = int32_1 == 1;
        this.wddTypeAssociated.SelectedValue = int32_2.ToString();
        this.txtCorporateName.Text = str1;
        this.wddDocumentType.SelectedValue = int32_3.ToString();
        this.txtDocumentNumber.Text = str2;
        this.txtAddress.Text = str3;
        this.txtTelephone.Text = str4;
        this.txtFax.Text = str5;
        this.txtAcronym.Text = str6;
        this.txtEmail.Text = empty1;
        this.txtEmail2.Text = empty2;
        this.txtWebSite.Text = str7;
        this.txtElectronicItemNumber.Text = str8;
        this.wddRegistryZone.SelectedValue = int32_4.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.txtUserName.Text = str9;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void setScriptEvents()
    {
    }

    private void DisableControls(int i_Status)
    {
      try
      {
        switch (i_Status)
        {
          case -2:
            this.wibSave.Enabled = true;
            this.wibDelete.Enabled = true;
            this.wibEnable.Enabled = true;
            this.wibDisable.Enabled = false;
            break;
          case -1:
            this.wibSave.Enabled = true;
            this.wibDelete.Enabled = false;
            this.wibEnable.Enabled = true;
            this.wibDisable.Enabled = true;
            break;
          case 0:
            this.wibSave.Enabled = false;
            this.wibDelete.Enabled = false;
            this.wibEnable.Enabled = false;
            this.wibDisable.Enabled = false;
            break;
          case 1:
            this.wibSave.Enabled = true;
            this.wibDelete.Enabled = true;
            this.wibEnable.Enabled = false;
            this.wibDisable.Enabled = true;
            break;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void Validar()
    {
      try
      {
        this.pbojAssociatedQueriesBL = new AssociatedQueriesBL();
        this.v_Option = "01";
        DataTable dataTable = this.pbojAssociatedQueriesBL.ExhibitionAssociatedVerifyAlias(this.txtAliasValidar.Text, this.v_Option, this.ViewState["i_plateTypeId"].ToString());
        if (dataTable.Rows.Count > 0)
        {
          if (dataTable.Columns.Count == 1)
          {
            string empty = string.Empty;
            this.CreatePopUpServer("SIIV - Placas Especiales", "../../UserControls/PopupConfirmationUser.aspx?MessageTypeId=1&MessageText=Este usuario ya EXISTE, desea reutilizar o crear un nuevo usuario", "550px", "190px");
          }
          else
          {
            this.txtUserName.Text = string.Empty;
            this.txtUserName.Focus();
            this.HidePopup();
            throw new HandledException(1, "El nombre de usuario ya existe, Intente con otro nombre de usuario diferente");
          }
        }
        else
        {
          this.CleanText();
          this.DisableText(true);
          this.HidePopup();
          this.wibSave.Enabled = true;
          throw new HandledException(2, "El nombre de usuario está disponible");
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ValidTransaction(int i_Result, string v_Action)
    {
      try
      {
        if (i_Result > 0)
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(2, "El asociado fue " + v_Action + " correctamente"));
        else
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "Se encontró un problema en la actualización de los datos"));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private int RoleConfiguration(int i_PlateTypeId)
    {
      this.v_Option = "01";
      if (this.ViewState["SupraRotate"] != null)
        this.v_Option = "02";
      return Convert.ToInt32(new AssociatedManagementBL().SpecialPlateRoleConfigGet(i_PlateTypeId, this.v_Option).Rows[0]["i_RoleConfigId"].ToString());
    }

    public bool SendEMail(List<string> EmailTo, ref string postrMessage)
    {
      Email email = new Email();
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.SMTPServerConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string pstrSMTPServer = dataTable.Rows[0]["v_Value"].ToString();
      int pintSMTPPort = int.Parse(dataTable.Rows[1]["v_Value"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
      string pstrSMTPUserName = dataTable.Rows[2]["v_Value"].ToString();
      string pstrSMTPPassword = dataTable.Rows[3]["v_Value"].ToString();
      bool boolean = Convert.ToBoolean(dataTable.Rows[4]["v_Value"], (IFormatProvider) CultureInfo.CurrentCulture);
      string pstrEmailFrom = "";
      string pstrEmailSubject = "Creación Usuario";
      string pstrEmailBody = "Se creó el usuario satisfactoriamente. </br> Usuario : " + this.txtUserName.Text;
      List<string> stringList = new List<string>();
      List<string> pstrEmailCC = new List<string>();
      switch (this.ViewState["i_plateTypeId"].ToString())
      {
        case "7":
          pstrEmailFrom = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
          {
            (object) SystemParameterGroups.ConfigurationExhibition.ToString((IFormatProvider) CultureInfo.CurrentCulture),
            (object) "1",
            (object) "1",
            (object) "1"
          }).Rows[0]["v_Value"].ToString();
          if (ConfigurationManager.AppSettings["EmailCopy_Exhibition"] != null)
          {
            string str1 = ConfigurationManager.AppSettings["EmailCopy_Exhibition"].ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim();
            char[] chArray = new char[1]{ '|' };
            foreach (string str2 in str1.Split(chArray))
            {
              if (!string.IsNullOrEmpty(str2))
                pstrEmailCC.Add(str2.ToString((IFormatProvider) CultureInfo.CurrentCulture));
            }
            break;
          }
          break;
        case "11":
          pstrEmailFrom = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
          {
            (object) SystemParameterGroups.ConfigurationRotate.ToString((IFormatProvider) CultureInfo.CurrentCulture),
            (object) "2",
            (object) "1",
            (object) "1"
          }).Rows[0]["v_Value"].ToString();
          if (ConfigurationManager.AppSettings["EmailCopy_Rotate"] != null)
          {
            string str3 = ConfigurationManager.AppSettings["EmailCopy_Rotate"].ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim();
            char[] chArray = new char[1]{ '|' };
            foreach (string str4 in str3.Split(chArray))
            {
              if (!string.IsNullOrEmpty(str4))
                pstrEmailCC.Add(str4.ToString((IFormatProvider) CultureInfo.CurrentCulture));
            }
          }
          break;
      }
      return Email.SendEmail(pstrSMTPUserName, pstrSMTPPassword, pstrEmailSubject, pstrEmailBody, EmailTo, pstrEmailCC, pstrSMTPServer, pintSMTPPort, pstrEmailFrom, boolean, EmailTo, ref postrMessage);
    }

    private void LoadUser()
    {
      try
      {
        this.pbojAssociatedQueriesBL = new AssociatedQueriesBL();
        this.v_Option = "02";
        DataTable dataTable = this.pbojAssociatedQueriesBL.ExhibitionAssociatedVerifyAlias(this.txtAliasValidar.Text, this.v_Option, this.ViewState["i_plateTypeId"].ToString());
        if (dataTable == null)
          return;
        this.ValidateCustomer();
        this.ViewState["i_systemUserId"] = (object) Convert.ToInt32(dataTable.Rows[0]["i_SystemUserId"]);
        this.chkNatural.Checked = Convert.ToBoolean(Convert.ToInt32(dataTable.Rows[0]["i_PersonTypeId"]));
        this.txtCorporateName.Text = dataTable.Rows[0]["v_ReasonSocial"].ToString();
        this.txtUserName.Text = dataTable.Rows[0]["v_Alias"].ToString();
        this.wddDocumentType.SelectedValue = dataTable.Rows[0]["i_DocumentTypeId"].ToString();
        this.txtDocumentNumber.Text = dataTable.Rows[0]["v_DocumentNumber"].ToString();
        this.txtAddress.Text = dataTable.Rows[0]["v_Address"].ToString();
        this.txtTelephone.Text = dataTable.Rows[0]["v_Telephone"].ToString();
        this.txtFax.Text = dataTable.Rows[0]["v_Fax"].ToString();
        this.wddTypeAssociated.SelectedValue = Convert.ToInt32(dataTable.Rows[0]["i_IsAssociatedAAP"]).ToString();
        this.txtElectronicItemNumber.Text = dataTable.Rows[0]["v_ElectronicItemNumber"].ToString();
        this.wddRegistryZone.SelectedValue = Convert.ToInt32(dataTable.Rows[0]["i_RegistryZoneId"]).ToString();
        this.txtAcronym.Text = dataTable.Rows[0]["v_Acronym"].ToString();
        string[] strArray1 = dataTable.Rows[0]["v_RepresentativeName"].ToString().Split('|');
        if (strArray1.Length == 1)
        {
          if (string.IsNullOrEmpty(strArray1[0].ToString()))
            strArray1[0] = "";
          else
            this.txtRepresentativeName.Text = strArray1[0].ToString();
        }
        if (strArray1.Length == 2)
        {
          if (string.IsNullOrEmpty(strArray1[0].ToString()))
            strArray1[0] = "";
          else
            this.txtRepresentativeName.Text = strArray1[0].ToString();
          if (string.IsNullOrEmpty(strArray1[1].ToString()))
            strArray1[1] = "";
          else
            this.txtRepresentativeName2.Text = strArray1[1].ToString();
        }
        if (strArray1.Length == 3)
        {
          if (string.IsNullOrEmpty(strArray1[0].ToString()))
            strArray1[0] = "";
          else
            this.txtRepresentativeName.Text = strArray1[0].ToString();
          if (string.IsNullOrEmpty(strArray1[1].ToString()))
            strArray1[1] = "";
          else
            this.txtRepresentativeName2.Text = strArray1[1].ToString();
          if (string.IsNullOrEmpty(strArray1[2].ToString()))
            strArray1[2] = "";
          else
            this.txtRepresentativeName3.Text = strArray1[2].ToString();
        }
        string[] strArray2 = dataTable.Rows[0]["v_Charge"].ToString().Split('|');
        if (strArray2.Length == 1)
        {
          if (string.IsNullOrEmpty(strArray2[0].ToString()))
            strArray2[0] = "";
          else
            this.txtCharge.Text = strArray2[0].ToString();
        }
        if (strArray2.Length == 2)
        {
          if (string.IsNullOrEmpty(strArray2[0].ToString()))
            strArray2[0] = "";
          else
            this.txtCharge.Text = strArray2[0].ToString();
          if (string.IsNullOrEmpty(strArray2[1].ToString()))
            strArray2[1] = "";
          else
            this.txtCharge2.Text = strArray2[1].ToString();
        }
        if (strArray2.Length == 3)
        {
          if (string.IsNullOrEmpty(strArray2[0].ToString()))
            strArray2[0] = "";
          else
            this.txtCharge.Text = strArray2[0].ToString();
          if (string.IsNullOrEmpty(strArray2[1].ToString()))
            strArray2[1] = "";
          else
            this.txtCharge2.Text = strArray2[1].ToString();
          if (string.IsNullOrEmpty(strArray2[2].ToString()))
            strArray2[2] = "";
          else
            this.txtCharge3.Text = strArray2[2].ToString();
        }
        string[] strArray3 = dataTable.Rows[0]["v_RepresentativeDocumentNumber"].ToString().Split('|');
        if (strArray3.Length == 1)
        {
          if (string.IsNullOrEmpty(strArray3[0].ToString()))
            strArray3[0] = "";
          else
            this.txtDocumentNumberRepresentative.Text = strArray3[0].ToString();
        }
        if (strArray3.Length == 2)
        {
          if (string.IsNullOrEmpty(strArray3[0].ToString()))
            strArray3[0] = "";
          else
            this.txtDocumentNumberRepresentative.Text = strArray3[0].ToString();
          if (string.IsNullOrEmpty(strArray3[1].ToString()))
            strArray3[1] = "";
          else
            this.txtDocumentNumberRepresentative2.Text = strArray3[1].ToString();
        }
        if (strArray3.Length == 3)
        {
          if (string.IsNullOrEmpty(strArray3[0].ToString()))
            strArray3[0] = "";
          else
            this.txtDocumentNumberRepresentative.Text = strArray3[0].ToString();
          if (string.IsNullOrEmpty(strArray3[1].ToString()))
            strArray3[1] = "";
          else
            this.txtDocumentNumberRepresentative2.Text = strArray3[1].ToString();
          if (string.IsNullOrEmpty(strArray3[2].ToString()))
            strArray3[2] = "";
          else
            this.txtDocumentNumberRepresentative3.Text = strArray3[2].ToString();
        }
        string[] strArray4 = Convert.ToInt32(dataTable.Rows[0]["i_RepresentativeDocumentTypeId"]).ToString().Split('0');
        if (strArray4.Length == 1)
        {
          if (string.IsNullOrEmpty(strArray4[0].ToString()))
            strArray4[0] = "";
          else
            this.wddDocumentTypeRepresentative.SelectedValue = strArray4[0].ToString();
        }
        if (strArray4.Length == 2)
        {
          if (string.IsNullOrEmpty(strArray4[0].ToString()))
            strArray4[0] = "";
          else
            this.wddDocumentTypeRepresentative.SelectedValue = strArray4[0].ToString();
          if (string.IsNullOrEmpty(strArray4[1].ToString()))
            strArray4[1] = "";
          else if (strArray4[1] == "9")
            strArray4[1] = "";
          else
            this.wddDocumentTypeRepresentative2.SelectedValue = strArray4[1].ToString();
        }
        if (strArray4.Length == 3)
        {
          if (string.IsNullOrEmpty(strArray4[0].ToString()))
            strArray4[0] = "";
          else
            this.wddDocumentTypeRepresentative.SelectedValue = strArray4[0].ToString();
          if (string.IsNullOrEmpty(strArray4[1].ToString()))
            strArray4[1] = "";
          else if (strArray4[1] == "9")
            strArray4[1] = "";
          else
            this.wddDocumentTypeRepresentative2.SelectedValue = strArray4[1].ToString();
          if (string.IsNullOrEmpty(strArray4[2].ToString()))
            strArray4[2] = "";
          else if (strArray4[2] == "9")
            strArray4[2] = "";
          else
            this.wddDocumentTypeRepresentative3.SelectedValue = strArray4[2].ToString();
        }
        string[] source = dataTable.Rows[0]["v_Email"].ToString().Split('|');
        this.txtEmail.Text = source[0].ToString();
        this.txtEmail2.Text = ((IEnumerable<string>) source).Count<string>() <= 1 ? "" : source[1].ToString();
        this.txtWebSite.Text = dataTable.Rows[0]["v_WebSite"].ToString();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ValidateCustomer()
    {
      try
      {
        this.pbojAssociatedQueriesBL = new AssociatedQueriesBL();
        this.v_Option = "03";
        DataTable dataTable = this.pbojAssociatedQueriesBL.ExhibitionAssociatedVerifyAlias(this.txtAliasValidar.Text, this.v_Option, this.ViewState["i_plateTypeId"].ToString());
        if (dataTable.Rows.Count <= 0)
          throw new HandledException(1, "Perfil Bloqueado o Deshabilitado");
        switch (dataTable.Rows[0]["i_CustomerTypeId"].ToString())
        {
          case "7":
            this.ViewState["i_CustomerTypeId"] = (object) "11";
            break;
          case "11":
            this.ViewState["i_CustomerTypeId"] = (object) "7";
            break;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ChangesState(int i_SystemUserId, int i_Status, string v_Action)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - ChangesAssociated1.aspx");
        this.pobjexhibitionassociated = new ExhibitionAssociated();
        this.pobjexhibitionassociated.i_SystemUserId = i_SystemUserId;
        int iSystemUserId = systemUser.i_SystemUserId;
        DateTime now = DateTime.Now;
        this.ValidTransaction(new AssociatedManagementBL().AssociatedDDE(this.pobjexhibitionassociated, i_Status, iSystemUserId, now), v_Action);
        this.DisableControls(i_Status);
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
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
