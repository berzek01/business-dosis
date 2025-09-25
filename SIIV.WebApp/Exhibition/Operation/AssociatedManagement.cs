// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Operation.AssociatedManagement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.BE.CustomCode;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.Exhibition.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Operation
{
  public class AssociatedManagement : Page
  {
    protected Label Label1;
    protected CheckBox chkLegal;
    protected CheckBox chkNatural;
    protected Label Label26;
    protected DropDownList wddTypeAssociated;
    protected TextBox txtCorporateName;
    protected Label Label3;
    protected DropDownList wddDocumentType;
    protected Label Label4;
    protected TextBox txtDocumentNumber;
    protected Label Label16;
    protected TextBox txtAddress;
    protected Label Label17;
    protected TextBox txtTelephone;
    protected Label Label9;
    protected TextBox txtFax;
    protected Label Label27;
    protected TextBox txtAcronym;
    protected Label Label12;
    protected TextBox txtUserName;
    protected HtmlTableRow trModifyPassword;
    protected LinkButton lnkChangePassword;
    protected HtmlTableRow trPassword;
    protected Label Label2;
    protected TextBox txtPassword;
    protected Label Label8;
    protected TextBox txtElectronicItemNumber;
    protected Label Label10;
    protected DropDownList wddRegistryZone;
    protected Label Label11;
    protected TextBox txtEmail;
    protected RegularExpressionValidator RegularExpressionValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender1;
    protected Label Label28;
    protected TextBox txtEmail2;
    protected RegularExpressionValidator RegularExpressionValidator3;
    protected ValidatorCalloutExtender ValidatorCalloutExtender2;
    protected Label Label7;
    protected TextBox txtWebSite;
    protected RegularExpressionValidator RegularExpressionValidator1;
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

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadParameters();
      this.LoadDataAssociated();
      this.trModifyPassword.Visible = true;
      this.trPassword.Visible = false;
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
      if ((this.txtRepresentativeName2.Text != "" || this.txtCharge2.Text != "" || this.wddDocumentTypeRepresentative2.SelectedIndex != 0 || this.txtDocumentNumberRepresentative2.Text != "") && (this.txtRepresentativeName2.Text == "" || this.txtCharge2.Text == "" || this.wddDocumentTypeRepresentative2.SelectedIndex == 0 || this.txtDocumentNumberRepresentative2.Text == ""))
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Debes ingresar todos los datos del representante.");
      else if ((this.txtRepresentativeName3.Text != "" || this.txtCharge3.Text != "" || this.wddDocumentTypeRepresentative3.SelectedIndex != 0 || this.txtDocumentNumberRepresentative3.Text != "") && (this.txtRepresentativeName3.Text == "" || this.txtCharge3.Text == "" || this.wddDocumentTypeRepresentative3.SelectedIndex == 0 || this.txtDocumentNumberRepresentative3.Text == ""))
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Debes ingresar todos los datos del representante.");
      else if ((this.txtRepresentativeName3.Text != "" || this.txtCharge3.Text != "" || this.wddDocumentTypeRepresentative3.SelectedIndex != 0 || this.txtDocumentNumberRepresentative3.Text != "") && (this.txtRepresentativeName2.Text == "" || this.txtCharge2.Text == "" || this.wddDocumentTypeRepresentative2.SelectedIndex == 0 || this.txtDocumentNumberRepresentative2.Text == ""))
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Debe llenar todos los datos del representante de la parte superior.");
      }
      else
      {
        this.lblMessage.Visible = false;
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        ExhibitionAssociated pobjExhibitionAssociated = new ExhibitionAssociated();
        pobjExhibitionAssociated.i_SystemUserId = systemUser.i_SystemUserId;
        pobjExhibitionAssociated.i_PersonTypeId = this.chkNatural.Checked ? 1 : 2;
        pobjExhibitionAssociated.v_ReasonSocial = this.txtCorporateName.Text;
        pobjExhibitionAssociated.v_UserName = this.txtUserName.Text;
        if (this.txtPassword.Text.Length > 0)
          pobjExhibitionAssociated.v_Password = Cryptography.GetHashMD5(this.txtPassword.Text.TrimEnd());
        pobjExhibitionAssociated.i_DocumentTypeId = Convert.ToInt32(this.wddDocumentType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        pobjExhibitionAssociated.v_DocumentNumber = this.txtDocumentNumber.Text;
        pobjExhibitionAssociated.v_Address = this.txtAddress.Text;
        pobjExhibitionAssociated.v_Telephone = this.txtTelephone.Text;
        pobjExhibitionAssociated.v_Fax = this.txtFax.Text;
        int int32 = Convert.ToInt32(this.wddTypeAssociated.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        pobjExhibitionAssociated.v_RepresentativeName = this.txtRepresentativeName.Text + "|" + this.txtRepresentativeName2.Text + "|" + this.txtRepresentativeName3.Text;
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
        string str3 = string.Concat(strArray);
        pobjExhibitionAssociated.i_RepresentativeDocumentTypeId = Convert.ToInt32(str3);
        pobjExhibitionAssociated.v_RepresentativeDocumentNumber = this.txtDocumentNumberRepresentative.Text + "|" + this.txtDocumentNumberRepresentative2.Text + "|" + this.txtDocumentNumberRepresentative3.Text;
        pobjExhibitionAssociated.v_ElectronicItemNumber = this.txtElectronicItemNumber.Text;
        pobjExhibitionAssociated.i_RegistryZoneId = Convert.ToInt32(this.wddRegistryZone.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        string v_Acronym = this.txtAcronym.Text.Trim();
        pobjExhibitionAssociated.v_Charge = this.txtCharge.Text + "|" + this.txtCharge2.Text + "|" + this.txtCharge3.Text;
        pobjExhibitionAssociated.v_Email = this.txtEmail.Text + "|" + this.txtEmail2.Text.Trim();
        pobjExhibitionAssociated.v_WebSite = this.txtWebSite.Text;
        try
        {
          if (new AssociatedManagementBL().AssociatedUpdate(pobjExhibitionAssociated, int32, v_Acronym) > 0)
            Message.SetMessage(this.lblMessage, enmMessageType.Success, "Los datos del asociado se registraron correctamente");
          else
            Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Se encontró un problema en la actualización de los datos");
        }
        catch (Exception ex)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Error, ex.Message);
        }
        this.wibSave.Enabled = false;
      }
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
            this.wddRegistryZone.Items.Add(new System.Web.UI.WebControls.ListItem(systemParameter.v_Description, systemParameter.i_ParameterId.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
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
        this.wddDocumentTypeRepresentative.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --"));
        this.wddDocumentTypeRepresentative2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --"));
        this.wddDocumentTypeRepresentative3.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --"));
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    private void LoadDataAssociated()
    {
      AssociatedQueriesBL associatedQueriesBl = new AssociatedQueriesBL();
      SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
      if (this.Session["ApplicationId"] != null)
      {
        int int32 = Convert.ToInt32(this.Session["ApplicationId"], (IFormatProvider) CultureInfo.CurrentCulture);
        string userExtendedAction = associatedQueriesBl.GetSystemUserExtendedAction(systemUser.i_SystemUserId, int32);
        if (userExtendedAction != "")
        {
          if (Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("7", StringComparison.CurrentCulture))) == "7")
          {
            this.txtCorporateName.Enabled = false;
            this.wddDocumentType.Enabled = false;
            this.txtDocumentNumber.Enabled = false;
          }
          else
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
          this.chkLegal.Enabled = false;
          this.chkNatural.Enabled = false;
          this.wddTypeAssociated.Enabled = false;
          this.txtUserName.Enabled = false;
        }
      }
      DataTable dtresult = new AssociatedQueriesBL().AssociatedRead(systemUser.i_SystemUserId);
      if (dtresult == null || dtresult.Rows.Count == 0)
        return;
      this.SetearData(dtresult);
    }

    private void SetearData(DataTable dtresult)
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
      int int32_4 = Convert.ToInt32(row["i_RegistryZoneId"], (IFormatProvider) CultureInfo.CurrentCulture);
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
      string str9 = row["v_UserName"].ToString();
      this.chkLegal.Checked = int32_1 == 2;
      this.chkNatural.Checked = int32_1 == 1;
      this.wddTypeAssociated.SelectedValue = int32_2.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      this.txtCorporateName.Text = str1;
      this.wddDocumentType.SelectedValue = int32_3.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      this.txtDocumentNumber.Text = str2;
      this.txtAddress.Text = str3;
      this.txtTelephone.Text = str4;
      this.txtFax.Text = str5;
      this.txtAcronym.Text = str6;
      this.txtEmail.Text = empty1;
      this.txtEmail2.Text = empty2;
      this.txtWebSite.Text = str7;
      this.wddRegistryZone.SelectedValue = int32_4.ToString();
      this.txtElectronicItemNumber.Text = str8;
      this.txtUserName.Text = str9;
    }
  }
}
