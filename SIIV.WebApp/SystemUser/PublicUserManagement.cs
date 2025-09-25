// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.SystemUser.PublicUserManagement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.SystemParameter.BL;
using SIIV.SystemUser.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.SystemUser
{
  public class PublicUserManagement : Page
  {
    private MaintenanceOperation currentOperation = MaintenanceOperation.None;
    private SIIV.BE.SystemUser objSystemUser = new SIIV.BE.SystemUser();
    protected HiddenField H1;
    protected UpdatePanel UpdatePanel1;
    protected Label Label23;
    protected RadioButtonList rblPersonTypeSearch;
    protected HtmlTable tagRequesterName;
    protected Label Label3;
    protected TextBox txtNameSearch;
    protected FilteredTextBoxExtender FilteredTextBoxExtender6;
    protected Label Label2;
    protected TextBox txtLastNameSearch;
    protected FilteredTextBoxExtender FilteredTextBoxExtender7;
    protected HtmlTable tagCompanyName;
    protected Label Label24;
    protected TextBox txtCompanySearch;
    protected Label Label16;
    protected TextBox txtNameUserSearch;
    protected Label Label17;
    protected DropDownList wddStatusSearch;
    protected Label Label18;
    protected DropDownList wddDocumentTypeSearch;
    protected Label Label19;
    protected TextBox txtDocumentSearch;
    protected FilteredTextBoxExtender FilteredTextBoxExtender5;
    protected Label Label20;
    protected TextBox TxtEmailSearch;
    protected FilteredTextBoxExtender FilteredTextBoxExtender9;
    protected Label Label21;
    protected DropDownList wddRegionSearch;
    protected Label Label22;
    protected DropDownList wddProvinceSearch;
    protected Label Label25;
    protected DropDownList wddDistrictSearch;
    protected Button WebImageButton4;
    protected Button btnReturnPopupConfirmation;
    protected GridView wdgList;
    protected Pager custPagerUserList;
    protected Label lblMessageUserList;
    protected UpdatePanel UpdatePanel2;
    protected HiddenField validator;
    protected Label lblAlias;
    protected TextBox txtAlias;
    protected HtmlTableRow trModifyPassword;
    protected LinkButton LinkButton1;
    protected HtmlTableRow tr1;
    protected Label lblContraseña;
    protected TextBox txtPassword;
    protected PasswordStrength txtPassword_PasswordStrength;
    protected RegularExpressionValidator RegularExpressionValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender6;
    protected RequiredFieldValidator RequiredFieldValidator3;
    protected ValidatorCalloutExtender ValidatorCalloutExtender4;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender3;
    protected RequiredFieldValidator RequiredFieldValidator4;
    protected ValidatorCalloutExtender ValidatorCalloutExtender1;
    protected HtmlTableRow tr2;
    protected Label lblRepeatContraseña;
    protected TextBox txtPasswordRepeat;
    protected RequiredFieldValidator RequiredFieldValidator5;
    protected ValidatorCalloutExtender ValidatorCalloutExtender5;
    protected CompareValidator CompareValidator1;
    protected ValidatorCalloutExtender CompareValidator1_ValidatorCalloutExtender;
    protected Label Label6;
    protected DropDownList wddDocumentType;
    protected Label Label7;
    protected TextBox txtDocumentNumber;
    protected FilteredTextBoxExtender txtDocumentNumber_FilteredTextBoxExtender;
    protected RequiredFieldValidator ValidatorDocumento;
    protected ValidatorCalloutExtender ValidatorDocumento_ValidatorCalloutExtender;
    protected Label Label1;
    protected RadioButtonList rblPersonType;
    protected HtmlTable tagRequesterName2;
    protected Label Label4;
    protected TextBox txtName;
    protected RequiredFieldValidator RequiredFieldValidator_txtName;
    protected ValidatorCalloutExtender ValidatorCalloutExtender7;
    protected FilteredTextBoxExtender FilteredTextBoxExtender2;
    protected Label Label8;
    protected TextBox txtLastName;
    protected RequiredFieldValidator RequiredFieldValidator_txtLastName;
    protected ValidatorCalloutExtender ValidatorCalloutExtender8;
    protected FilteredTextBoxExtender FilteredTextBoxExtender1;
    protected HtmlTable tagCompanyName2;
    protected Label Label9;
    protected TextBox txtCompany;
    protected RequiredFieldValidator RequiredFieldValidator_txtCompany;
    protected ValidatorCalloutExtender ValidatorCalloutExtender9;
    protected Label Label10;
    protected TextBox txtEmail;
    protected RequiredFieldValidator ValidatorEmail;
    protected ValidatorCalloutExtender ValidatorEmail_ValidatorCalloutExtender;
    protected RegularExpressionValidator RegularExpressionValidator1;
    protected ValidatorCalloutExtender RegularExpressionValidator1_ValidatorCalloutExtender;
    protected Label Label11;
    protected TextBox txtPhoneNumber;
    protected MaskedEditExtender txtPhoneNumber_MaskedEditExtender;
    protected RequiredFieldValidator ValidatorTelefono;
    protected ValidatorCalloutExtender ValidatorTelefono_ValidatorCalloutExtender;
    protected Label Label12;
    protected TextBox txtAddress;
    protected RequiredFieldValidator ValidatorAdress;
    protected ValidatorCalloutExtender ValidatorAdress_ValidatorCalloutExtender;
    protected DropDownList wddRegion;
    protected RequiredFieldValidator ValidatorDepartment;
    protected ValidatorCalloutExtender ValidatorDepartment_ValidatorCalloutExtender;
    protected Label Label13;
    protected DropDownList wddProvince;
    protected RequiredFieldValidator ValidatorProvince;
    protected ValidatorCalloutExtender ValidatorProvince_ValidatorCalloutExtender;
    protected Label Label15;
    protected DropDownList wddDistrict;
    protected RequiredFieldValidator ValidatorDistrict;
    protected ValidatorCalloutExtender ValidatorDistrict_ValidatorProvince_ValidatorCalloutExtender;
    protected Label Label14;
    protected DropDownList wddStatus;
    protected HtmlTableRow trManagementButtons;
    protected Button wibSave;
    protected Button wibCancel;
    protected Button btnJavaScriptResponse;
    protected Label lblMessageUser;
    protected HtmlTableRow trwibFinalze;
    protected Button wibFinalze;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.DataBind();
      this.Initialize();
      this.EditInitialize();
    }

    private void EditInitialize()
    {
      this.LoadWebDropDown(this.wddRegion, new UbigeoQueriesBL().GetRegion(), "v_description", "v_IdRegion", 0, 1);
      this.LoadWebDropDown(this.wddProvince, new UbigeoQueriesBL().GetProvince(this.wddRegion.SelectedValue), "v_description", "v_IdProvince", 0, 1);
      this.LoadWebDropDown(this.wddDistrict, new UbigeoQueriesBL().GetDistrict(this.wddRegion.SelectedValue, this.wddProvince.SelectedValue), "v_description", "v_IdDistrict", 0, 1);
      this.LoadWebDropDown(this.wddDocumentType, new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      }), "v_description", "i_ParameterId", 0, 1);
    }

    private void Initialize()
    {
      this.LoadWebDropDown(this.wddRegionSearch, new UbigeoQueriesBL().GetRegion(), "v_description", "v_IdRegion", 0, 0);
      this.LoadWebDropDown(this.wddProvinceSearch, new UbigeoQueriesBL().GetProvince(this.wddRegionSearch.SelectedValue), "v_description", "v_IdProvince", 0, 0);
      this.LoadWebDropDown(this.wddDistrictSearch, new UbigeoQueriesBL().GetDistrict(this.wddRegionSearch.SelectedValue, this.wddProvinceSearch.SelectedValue), "v_description", "v_IdDistrict", 0, 0);
      DataTable pdtDataSource = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      DataRow row = pdtDataSource.NewRow();
      row[0] = (object) 218;
      row[1] = (object) "Seleccione";
      row[2] = (object) -1;
      row[3] = (object) "Seleccione";
      row[4] = (object) "";
      row[5] = (object) "";
      row[6] = (object) 1;
      row[7] = (object) "";
      row[8] = (object) 1;
      row[9] = (object) 1;
      row[10] = (object) "";
      row[11] = (object) DateTime.Now;
      row[12] = (object) "";
      row[13] = (object) DateTime.Now;
      row[14] = (object) "";
      row[15] = (object) "";
      row[16] = (object) 0;
      pdtDataSource.Rows.InsertAt(row, 0);
      this.LoadWebDropDown(this.wddDocumentTypeSearch, pdtDataSource, "v_description", "i_ParameterId", 0, 0);
    }

    private void LoadWebDropDown(
      DropDownList pwddControl,
      DataTable pdtDataSource,
      string pstrTextField,
      string pstrValueField,
      int pintSelectIndex,
      int pintEdit)
    {
      pwddControl.DataSource = (object) pdtDataSource;
      pwddControl.DataTextField = pstrTextField;
      pwddControl.DataValueField = pstrValueField;
      pwddControl.DataBind();
      if (pintEdit == 0)
      {
        pwddControl.Items[0].Value = "";
        pwddControl.Items[0].Text = SIIV.SystemParameter.BL.Constants.OPCIONLISTA_Todos;
      }
      pwddControl.SelectedIndex = 0;
      pwddControl.SelectedIndex = pintSelectIndex;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.lblMessageUser.Text = "";
      this.lblMessageUser.Visible = false;
      if (this.tr1.Visible)
      {
        this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperation"];
        if (this.objSystemUser.v_Password == Cryptography.GetHashMD5(new SIIV.BE.SystemUser()
        {
          v_Password = this.txtPassword.Text.Trim()
        }.v_Password) && this.currentOperation == MaintenanceOperation.Edit)
        {
          this.RequiredFieldValidator3.IsValid = false;
          this.HidePopup();
          return;
        }
        if (this.txtPassword.Text.Trim() != this.txtPasswordRepeat.Text.Trim())
        {
          this.txtPassword.Text = "";
          this.txtPasswordRepeat.Text = "";
          this.CompareValidator1.IsValid = false;
          this.HidePopup();
          return;
        }
        if (this.txtPassword.Text.Trim() == "")
        {
          this.RequiredFieldValidator4.IsValid = false;
          this.HidePopup();
          return;
        }
        if (this.txtPasswordRepeat.Text.Trim() == "")
        {
          this.RequiredFieldValidator5.IsValid = false;
          this.HidePopup();
          return;
        }
        if (this.txtPassword.Text.Length <= 7)
        {
          this.RequiredFieldValidator2.IsValid = false;
          this.HidePopup();
          return;
        }
      }
      if (this.wddDocumentType.SelectedValue == "1" && (this.txtDocumentNumber.Text.Length < 8 || this.txtDocumentNumber.Text.Length > 8))
      {
        this.ValidatorDocumento.IsValid = false;
        this.ValidatorDocumento.ErrorMessage = "Ingrese su número de documento.";
        this.HidePopup();
      }
      else if (this.wddDocumentType.SelectedValue == "4" && (this.txtDocumentNumber.Text.Length < 10 || !Format.ValidateRUCstructure(this.txtDocumentNumber.Text)))
      {
        this.ValidatorDocumento.IsValid = false;
        this.ValidatorDocumento.ErrorMessage = "Ingrese un número de RUC Válido.";
        this.HidePopup();
      }
      else
      {
        if (this.rblPersonType.SelectedValue == "1")
        {
          if (this.txtName.Text.Trim() == "")
          {
            this.RequiredFieldValidator_txtName.IsValid = false;
            this.HidePopup();
            return;
          }
          if (this.txtLastName.Text.Trim() == "")
          {
            this.RequiredFieldValidator_txtLastName.IsValid = false;
            this.HidePopup();
            return;
          }
        }
        else if (this.txtCompany.Text.Trim() == "")
        {
          this.RequiredFieldValidator_txtCompany.IsValid = false;
          this.HidePopup();
          return;
        }
        if (this.txtEmail.Text.Trim() == "")
        {
          this.ValidatorEmail.IsValid = false;
          this.HidePopup();
        }
        else
        {
          this.RegularExpressionValidator1.Validate();
          if (!this.RegularExpressionValidator1.IsValid)
            this.HidePopup();
          else if (this.wddRegion.SelectedIndex == 0)
          {
            this.ValidatorDepartment.IsValid = false;
            this.HidePopup();
          }
          else if (this.wddProvince.SelectedIndex == 0)
          {
            this.ValidatorProvince.IsValid = false;
            this.HidePopup();
          }
          else if (this.wddDistrict.SelectedIndex == 0)
          {
            this.ValidatorDistrict.IsValid = false;
            this.HidePopup();
          }
          else if (this.txtAddress.Text.Trim() == string.Empty)
          {
            this.ValidatorAdress.IsValid = false;
            this.HidePopup();
          }
          else if (this.txtPhoneNumber.Text.Replace("-", "").Length != 9)
          {
            this.ValidatorTelefono.IsValid = false;
            this.HidePopup();
          }
          else if (!this.ValidateParameters())
          {
            this.HidePopup();
          }
          else
          {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
            {
              Timeout = new TimeSpan(0, 1, 1)
            }))
            {
              try
              {
                switch (this.currentOperation)
                {
                  case MaintenanceOperation.Edit:
                    new SystemUserQueriesBL().SystemUserPublicUpdate(this.GetCurrentUser());
                    Message.SetMessage(this.lblMessageUser, enmMessageType.Success, "El Usuario se modificó satisfactoriamente");
                    this.SearchSystemUsers();
                    this.trwibFinalze.Visible = true;
                    this.trManagementButtons.Visible = false;
                    break;
                }
                transactionScope.Complete();
              }
              catch (Exception ex)
              {
                Message.SetMessage(this.lblMessageUser, enmMessageType.Error, ex.Message);
                this.trwibFinalze.Visible = false;
                this.trManagementButtons.Visible = true;
              }
            }
            this.HidePopup();
          }
        }
      }
    }

    private SIIV.BE.SystemUser GetCurrentUser()
    {
      this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperation"];
      SIIV.BE.SystemUser systemUser = (SIIV.BE.SystemUser) this.Session["SystemUser"];
      SIIV.BE.SystemUser currentUser = new SIIV.BE.SystemUser();
      switch (this.currentOperation)
      {
        case MaintenanceOperation.AddNew:
          currentUser.i_SystemUserId = 0;
          currentUser.i_InsertUserId = new int?(systemUser.i_SystemUserId);
          break;
        case MaintenanceOperation.Edit:
          currentUser.i_SystemUserId = Convert.ToInt32(this.ViewState["i_SystemUserId"], (IFormatProvider) CultureInfo.CurrentCulture);
          currentUser.i_UpdateUserId = new int?(systemUser.i_SystemUserId);
          break;
      }
      currentUser.v_Alias = this.txtAlias.Text.TrimEnd();
      if (this.txtPassword.Text.Length > 0)
      {
        currentUser.v_Password = Cryptography.GetHashMD5(this.txtPassword.Text.TrimEnd());
        systemUser.v_Password = currentUser.v_Password;
      }
      currentUser.i_PersonTypeId = new int?(Convert.ToInt32(this.rblPersonType.SelectedValue));
      currentUser.i_DocumentTypeId = new int?(Convert.ToInt32(this.wddDocumentType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
      currentUser.v_DocumentNumber = this.txtDocumentNumber.Text.TrimEnd();
      currentUser.v_FirstName = this.rblPersonType.SelectedValue == "1" ? this.txtName.Text.TrimEnd() : this.txtCompany.Text.TrimEnd();
      currentUser.v_LastName = this.rblPersonType.SelectedValue == "1" ? this.txtLastName.Text.TrimEnd() : "";
      currentUser.v_Email = this.txtEmail.Text.TrimEnd();
      currentUser.v_Telephone = this.txtPhoneNumber.Text.TrimEnd();
      currentUser.v_Address = this.txtAddress.Text.TrimEnd();
      currentUser.v_Ubigeo = this.wddRegion.SelectedValue + this.wddProvince.SelectedValue + this.wddDistrict.SelectedValue;
      currentUser.i_Status = new int?(Convert.ToInt32(this.wddStatus.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
      currentUser.v_Question_Answer = "";
      currentUser.v_Code = "";
      return currentUser;
    }

    private bool ValidateParameters()
    {
      this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperation"];
      string pstrMessage = "";
      if (this.txtAlias.Text.Length == 0)
        pstrMessage = "Debe ingresar el alias";
      else if (this.txtPassword.Text.Length == 0 && MaintenanceOperation.AddNew == this.currentOperation)
        pstrMessage = "Debe ingresar la contraseña";
      else if (this.wddDocumentType.SelectedValue == "" || this.wddDocumentType.SelectedValue == "-1")
        pstrMessage = "Debe selecionar el tipo de documento";
      else if (this.txtDocumentNumber.Text.Length == 0)
        pstrMessage = "Debe ingresar el número de documento";
      else if (this.txtName.Text.Length == 0 && this.rblPersonType.SelectedValue == "1")
        pstrMessage = "Debe ingresar el nombre";
      else if (this.txtLastName.Text.Length == 0 && this.rblPersonType.SelectedValue == "1")
        pstrMessage = "Debe ingresar el apellido";
      else if (this.txtCompany.Text.Length == 0 && this.rblPersonType.SelectedValue == "2")
        pstrMessage = "Debe ingresar la razón social";
      else if (this.txtEmail.Text.Length == 0)
        pstrMessage = "Debe ingresar el Email";
      else if (this.txtAddress.Text.Length == 0)
        pstrMessage = "Debe ingresar la dirección";
      else if (this.wddRegion.SelectedValue == "" || this.wddRegion.SelectedValue == "-1")
        pstrMessage = "Debe seleccionar el estado";
      else if (this.wddProvince.SelectedValue == "" || this.wddProvince.SelectedValue == "-1")
        pstrMessage = "Debe seleccionar el estado";
      else if (this.wddDistrict.SelectedValue == "" || this.wddDistrict.SelectedValue == "-1")
        pstrMessage = "Debe seleccionar el estado";
      else if (this.wddStatus.SelectedValue == "" || this.wddStatus.SelectedValue == "-1")
        pstrMessage = "Debe seleccionar el estado";
      if (pstrMessage.Length <= 0)
        return true;
      Message.SetMessage(this.lblMessageUser, enmMessageType.Warning, pstrMessage);
      return false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      this.EnabledControls(MaintenanceOperation.None);
    }

    protected void wdgList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        int int32_1 = Convert.ToInt32(e.CommandArgument);
        if (this.wdgList.Rows[int32_1] == null)
          throw new HandledException(4, "Error de selección.", "'wdgList' - SystemUserManagement.aspx");
        if (e.CommandName.Equals("Edit", StringComparison.CurrentCulture))
        {
          if (Convert.ToInt32(this.wdgList.DataKeys[int32_1]["i_RoleConfigId"].ToString()) == 0)
            throw new HandledException(4, "Usuario no tiene rol asignado", "'wdgList' - SystemUserManagement.aspx");
          this.txtPhoneNumber.Text = "";
          this.currentOperation = MaintenanceOperation.Edit;
          this.ViewState["currentOperation"] = (object) this.currentOperation;
          this.EnabledControls(MaintenanceOperation.Edit);
          int int32_2 = Convert.ToInt32(this.wdgList.DataKeys[int32_1]["i_SystemUserId"].ToString());
          this.Session["SystemUserId2"] = (object) int32_2;
          this.SetearRecord(int32_2);
          this.lblMessageUser.Visible = false;
          this.trwibFinalze.Visible = false;
          this.trManagementButtons.Visible = true;
          this.trModifyPassword.Visible = true;
          this.tr1.Visible = false;
          this.tr2.Visible = false;
        }
        else
        {
          if (!e.CommandName.Equals("Delete", StringComparison.CurrentCulture))
            return;
          this.ViewState["DeleteSystemUserId"] = (object) this.wdgList.DataKeys[int32_1]["i_SystemUserId"].ToString();
          string empty = string.Empty;
          this.CreatePopUpServer("SIIV - Eliminación de usuario público", "../../UserControls/PopupConfirmation.aspx?MessageTypeId=2&MessageText=¿Está seguro de eliminar permanentemente el usuario : " + this.wdgList.DataKeys[int32_1]["v_Alias"].ToString() + "?", "360px", "190px");
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessageUserList, enmMessageType.Error, ex.ErrorMessage);
      }
    }

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
      {
        Timeout = new TimeSpan(1, 1, 1)
      }))
      {
        try
        {
          SIIV.BE.SystemUser pobjBE = new SIIV.BE.SystemUser();
          pobjBE.i_SystemUserId = Convert.ToInt32(this.ViewState["DeleteSystemUserId"]);
          int? iStatus = pobjBE.i_Status;
          int num = 0;
          if (iStatus.GetValueOrDefault() == num & iStatus.HasValue)
          {
            Message.SetMessage(this.lblMessageUserList, enmMessageType.Warning, "No se puede realizar la eliminación porque el usuario ya está eliminado");
            return;
          }
          new SystemUserQueriesBL().SystemUserDelete(pobjBE);
          transactionScope.Complete();
          Message.SetMessage(this.lblMessageUserList, enmMessageType.Success, "El usuario se eliminó satisfactoriamente");
        }
        catch (Exception ex)
        {
          Message.SetMessage(this.lblMessageUserList, enmMessageType.Error, ex.Message);
        }
      }
      this.SearchSystemUsers();
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

    private void SetearRecord(int i_SystemUserId)
    {
      this.ViewState[nameof (i_SystemUserId)] = (object) i_SystemUserId;
      DataTable dataTable = new SystemUserQueriesBL().SystemUserRead(i_SystemUserId);
      this.rblPersonType.SelectedValue = dataTable.Rows[0]["i_PersonTypeId"].ToString();
      this.rblPersonType_SelectedIndexChanged((object) null, (EventArgs) null);
      this.txtAlias.Text = dataTable.Rows[0]["v_Alias"].ToString();
      this.wddDocumentType.SelectedValue = dataTable.Rows[0]["i_DocumentTypeId"].ToString();
      this.wddDocumentType_SelectedIndexChanged((object) null, (EventArgs) null);
      this.txtDocumentNumber.Text = dataTable.Rows[0]["v_DocumentNumber"].ToString();
      this.txtName.Text = dataTable.Rows[0]["v_FirstName"].ToString();
      this.txtLastName.Text = dataTable.Rows[0]["v_LastName"].ToString();
      this.txtCompany.Text = dataTable.Rows[0]["v_FirstName"].ToString();
      this.txtEmail.Text = dataTable.Rows[0]["v_Email"].ToString();
      this.txtPhoneNumber.Text = dataTable.Rows[0]["v_Telephone"].ToString();
      this.txtAddress.Text = dataTable.Rows[0]["v_Address"].ToString();
      this.wddRegion.SelectedValue = dataTable.Rows[0]["v_Ubigeo"].ToString().Substring(0, 2);
      this.wddRegion_SelectedIndexChanged((object) null, (EventArgs) null);
      this.wddProvince.SelectedValue = dataTable.Rows[0]["v_Ubigeo"].ToString().Substring(2, 2);
      this.wddProvince_SelectedIndexChanged((object) null, (EventArgs) null);
      this.wddDistrict.SelectedValue = dataTable.Rows[0]["v_Ubigeo"].ToString().Substring(4, 2);
      this.wddStatus.SelectedValue = dataTable.Rows[0]["i_Status"].ToString();
    }

    private void EnabledControls(MaintenanceOperation penuCurrentOperation)
    {
      string str = this.H1.Value;
      if (penuCurrentOperation == MaintenanceOperation.Edit)
      {
        this.wddStatus.Enabled = true;
        if (str == "0")
        {
          string script1 = UtilDA.ActiveTabIndexUser("tabs", 1, "0");
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script1, true);
          string script2 = "TabIndex();";
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script2, true);
        }
        else
        {
          string script3 = UtilDA.ActiveTabIndexUser("tabs", 0, "1");
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script3, true);
          string script4 = "TabIndex();";
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script4, true);
        }
      }
      else
      {
        this.lblMessageUserList.Text = "";
        this.lblMessageUserList.Visible = false;
        string script5 = UtilDA.ActiveTabIndexUser("tabs", 0, "1,2,3,4");
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script5, true);
        string script6 = "TabIndex();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script6, true);
      }
    }

    protected void wdgList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      this.lblMessageUserList.Text = "";
      this.lblMessageUserList.Visible = false;
      this.SearchSystemUsers();
    }

    private void SearchSystemUsers()
    {
      try
      {
        string str = "";
        SIIV.BE.SystemUser objSystemUser = new SIIV.BE.SystemUser();
        if (this.wddRegionSearch.SelectedValue != "")
          str = this.wddRegionSearch.SelectedValue + (string.IsNullOrEmpty(this.wddProvinceSearch.SelectedValue) ? "__" : this.wddProvinceSearch.SelectedValue) + (string.IsNullOrEmpty(this.wddDistrictSearch.SelectedValue) ? "__" : this.wddDistrictSearch.SelectedValue);
        objSystemUser.i_PersonTypeId = new int?(Convert.ToInt32(this.rblPersonTypeSearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
        objSystemUser.v_FirstName = this.rblPersonTypeSearch.SelectedIndex == 0 ? this.txtNameSearch.Text : this.txtCompanySearch.Text;
        objSystemUser.v_LastName = this.rblPersonTypeSearch.SelectedIndex == 0 ? this.txtLastNameSearch.Text.Trim() : "";
        objSystemUser.v_Alias = this.txtNameUserSearch.Text;
        objSystemUser.i_Status = new int?(Convert.ToInt32(this.wddStatusSearch.SelectedValue));
        objSystemUser.i_DocumentTypeId = new int?(Convert.ToInt32(string.IsNullOrEmpty(this.wddDocumentTypeSearch.SelectedValue) ? "-1" : this.wddDocumentTypeSearch.SelectedValue));
        objSystemUser.v_DocumentNumber = this.txtDocumentSearch.Text;
        objSystemUser.v_Email = this.TxtEmailSearch.Text;
        objSystemUser.v_Ubigeo = str;
        this.SearchSystemUsersList(objSystemUser, true);
        this.HidePopup();
      }
      catch (Exception ex)
      {
        this.HidePopup();
        Message.SetMessage(this.lblMessageUser, enmMessageType.Error, "Error*****<br>" + ex.Message);
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private void SearchSystemUsersList(SIIV.BE.SystemUser objSystemUser, bool pboolLoadPager)
    {
      int pintstartRowIndex = pboolLoadPager ? 1 : this.custPagerUserList.CurrentPageNumber;
      int pintmaxRows = this.custPagerUserList.CurrentPageSize == 0 ? 10 : this.custPagerUserList.CurrentPageSize;
      int pinttotalRows;
      DataTable all = new SystemUserQueriesBL().UserPublicGetAll(objSystemUser, pintstartRowIndex, pintmaxRows, out pinttotalRows);
      int num = pinttotalRows;
      this.wdgList.DataSource = (object) all;
      this.wdgList.DataBind();
      this.custPagerUserList.TotalPages = num % pintmaxRows == 0 ? num / pintmaxRows : num / pintmaxRows + 1;
      this.custPagerUserList.TotalRecordCount = pinttotalRows;
      if (!pboolLoadPager)
        return;
      this.custPagerUserList.LoadPager();
    }

    protected void custPagerUserList_OnPageChanged(object sender, CustomPageChangeArgs e)
    {
      SIIV.BE.SystemUser objSystemUser = new SIIV.BE.SystemUser();
      string str = "";
      if (this.wddRegionSearch.SelectedValue != "")
        str = this.wddRegionSearch.SelectedValue + (string.IsNullOrEmpty(this.wddProvinceSearch.SelectedValue) ? "__" : this.wddProvinceSearch.SelectedValue) + (string.IsNullOrEmpty(this.wddDistrictSearch.SelectedValue) ? "__" : this.wddDistrictSearch.SelectedValue);
      objSystemUser.i_PersonTypeId = new int?(Convert.ToInt32(this.rblPersonTypeSearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
      objSystemUser.v_FirstName = this.rblPersonTypeSearch.SelectedIndex == 0 ? this.txtNameSearch.Text : this.txtCompanySearch.Text;
      objSystemUser.v_LastName = this.rblPersonTypeSearch.SelectedIndex == 0 ? this.txtLastNameSearch.Text.Trim() : "";
      objSystemUser.v_Alias = this.txtNameUserSearch.Text;
      objSystemUser.i_Status = new int?(Convert.ToInt32(this.wddStatusSearch.SelectedValue));
      objSystemUser.i_DocumentTypeId = new int?(Convert.ToInt32(string.IsNullOrEmpty(this.wddDocumentTypeSearch.SelectedValue) ? "-1" : this.wddDocumentTypeSearch.SelectedValue));
      objSystemUser.v_DocumentNumber = this.txtDocumentSearch.Text;
      objSystemUser.v_Email = this.TxtEmailSearch.Text;
      objSystemUser.v_Ubigeo = str;
      this.SearchSystemUsersList(objSystemUser, false);
    }

    protected void Unnamed2_Click(object sender, EventArgs e)
    {
      this.trModifyPassword.Visible = false;
      this.tr1.Visible = true;
      this.tr2.Visible = true;
    }

    protected void wddDocumentType_SelectedIndexChanged(object sender, EventArgs e)
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

    protected void btnJavaScriptResponse_Click(object sender, EventArgs e)
    {
    }

    protected void rblPersonTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.txtNameSearch.Text = "";
      this.txtLastNameSearch.Text = "";
      this.txtCompanySearch.Text = "";
      if (this.rblPersonTypeSearch.SelectedIndex == 0)
      {
        this.tagRequesterName.Visible = true;
        this.tagCompanyName.Visible = false;
        this.wddDocumentTypeSearch.SelectedIndex = 0;
        this.wddDocumentTypeSearch.Enabled = true;
        this.txtNameSearch.Focus();
      }
      else
      {
        if (this.rblPersonTypeSearch.SelectedIndex != 1)
          return;
        this.tagRequesterName.Visible = false;
        this.tagCompanyName.Visible = true;
        this.wddDocumentTypeSearch.SelectedValue = "4";
        this.wddDocumentTypeSearch.Enabled = false;
        this.txtCompanySearch.Focus();
      }
    }

    protected void rblPersonType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.rblPersonType.SelectedIndex == 0)
      {
        this.tagRequesterName2.Visible = true;
        this.tagCompanyName2.Visible = false;
        this.wddDocumentType.SelectedIndex = 0;
        this.wddDocumentType.Enabled = true;
      }
      else if (this.rblPersonType.SelectedIndex == 1)
      {
        this.tagRequesterName2.Visible = false;
        this.tagCompanyName2.Visible = true;
        this.wddDocumentType.SelectedValue = "4";
        this.wddDocumentType.Enabled = false;
      }
      this.wddDocumentType_SelectedIndexChanged((object) null, (EventArgs) null);
    }

    protected void wddRegionSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.LoadWebDropDown(this.wddProvinceSearch, new UbigeoQueriesBL().GetProvince(this.wddRegionSearch.SelectedValue), "v_description", "v_IdProvince", 0, 0);
      this.wddDistrictSearch.SelectedIndex = 0;
    }

    protected void wddProvinceSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.LoadWebDropDown(this.wddDistrictSearch, new UbigeoQueriesBL().GetDistrict(this.wddRegionSearch.SelectedValue, this.wddProvinceSearch.SelectedValue), "v_description", "v_IdDistrict", 0, 0);
    }

    protected void wddRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.LoadWebDropDown(this.wddProvince, new UbigeoQueriesBL().GetProvince(this.wddRegion.SelectedValue), "v_description", "v_IdProvince", 0, 1);
      this.wddDistrict.SelectedIndex = 0;
    }

    protected void wddProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.LoadWebDropDown(this.wddDistrict, new UbigeoQueriesBL().GetDistrict(this.wddRegion.SelectedValue, this.wddProvince.SelectedValue), "v_description", "v_IdDistrict", 0, 1);
    }
  }
}
