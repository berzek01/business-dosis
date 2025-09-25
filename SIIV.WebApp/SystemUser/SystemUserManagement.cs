// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.SystemUser.SystemUserManagement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Common.Resource.PagingClass;
using SIIV.Common.Resource.Utilities;
using SIIV.Reports.BL;
using SIIV.SystemParameter.BL;
using SIIV.SystemUser.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.SystemUser
{
  public class SystemUserManagement : Page
  {
    public int SystemUserId;
    private MaintenanceOperation currentOperation = MaintenanceOperation.None;
    private SIIV.BE.SystemUser objSystemUser = new SIIV.BE.SystemUser();
    protected HiddenField H1;
    protected UpdatePanel UpdatePanel1;
    protected DropDownList wddCompanySearch;
    protected TextBox txtNameSearch;
    protected TextBox txtLastNameSearch;
    protected TextBox txtNameUserSearch;
    protected CheckBox chkStatusSearch;
    protected Button WebImageButton4;
    protected GridView wdgList;
    protected Pager custPagerUserList;
    protected Button wibNew;
    protected Button wibLineaCredito;
    protected Label lblMessageUserList;
    protected UpdatePanel UpdatePanel2;
    protected HiddenField validator;
    protected Label Label1;
    protected DropDownList wddCompany;
    protected Label lblAlias;
    protected TextBox txtAlias;
    protected HtmlTableRow trModifyPassword;
    protected LinkButton LinkButton1;
    protected HtmlTableRow trPassword;
    protected Label Label5;
    protected TextBox txtPassword2;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected ValidatorCalloutExtender ValidatorCalloutExtender2;
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
    protected Label Label15;
    protected RadioButtonList rblPersonType;
    protected HtmlTable tagRequesterName2;
    protected Label Label8;
    protected TextBox txtName;
    protected RequiredFieldValidator RequiredFieldValidator_txtName;
    protected ValidatorCalloutExtender ValidatorCalloutExtender7;
    protected FilteredTextBoxExtender FilteredTextBoxExtender2;
    protected Label Label9;
    protected TextBox txtLastName;
    protected RequiredFieldValidator RequiredFieldValidator_txtLastName;
    protected ValidatorCalloutExtender ValidatorCalloutExtender8;
    protected FilteredTextBoxExtender FilteredTextBoxExtender1;
    protected HtmlTable tagCompanyName2;
    protected Label Label16;
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
    protected Label Label13;
    protected TextBox txtUserRefId;
    protected Button wibAsiggnedUserRef;
    protected Button wibNoAsiggnedUserRef;
    protected Label Label14;
    protected DropDownList wddStatus;
    protected HtmlTableRow trManagementButtons;
    protected Button wibSave;
    protected Button wibCancel;
    protected Button btnJavaScriptResponse;
    protected Label lblMessageUser;
    protected HtmlTableRow trwibFinalze;
    protected Button wibFinalze;
    protected UpdatePanel UpdatePanel3;
    protected Label Label2;
    protected DropDownList wddRole;
    protected Label Label3;
    protected DropDownList wddLocation;
    protected Label Label4;
    protected DropDownList wddStatusRoleUser;
    protected CheckBox chkAutoApprovalRequirement;
    protected CheckBox chkValidateVoucher;
    protected Button wibNewRolUser;
    protected Button wibSaveRole;
    protected Button wibCancelRolUser;
    protected Button wibReturn;
    protected GridView wdgUserRoles;
    protected Label lblMessageRoleUser;
    protected UpdatePanel UpdatePanel4;
    protected Label lblLineaCreditoPadre;
    protected TextBox txtLinea0;
    protected Button wibConsultar;
    protected TextBox txtLinea;
    protected Fecha wddFechaIni;
    protected Fecha wddFechaFin;
    protected Label lblPassword;
    protected Button BtnGenerar;
    protected Button wibSaveRole0;
    protected Button wibCancelRolUser0;
    protected Button wibReturn0;
    protected GridView wdgList0;
    protected UpdatePanel UpdatePanel5;
    protected TextBox txtLinea1;
    protected Label lblCodigoPago;
    protected Button BtnCodigoPago;
    protected Button WebImageButton1;
    protected Button WebImageButton2;
    protected Button WebImageButton3;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.objSystemUser = (SIIV.BE.SystemUser) this.Session["SystemUser"];
      if (this.Page.IsPostBack)
        return;
      SIIV.BE.SystemUser systemUser = (SIIV.BE.SystemUser) this.Session["SystemUser"];
      if (this.objSystemUser.v_RoleName == "AAP Usuario Público Web")
      {
        this.txtAlias.Enabled = false;
        this.wddStatus.Visible = false;
        this.txtUserRefId.Visible = false;
        this.wibAsiggnedUserRef.Visible = false;
        this.Label13.Visible = false;
        this.Label14.Visible = false;
      }
      this.LoadParameters();
      this.Session["SystemUserId2"] = (object) null;
      this.ViewState["OnlyEdit"] = (object) "0";
      if (this.Request.QueryString["syu"] != null)
      {
        if (this.Request.QueryString["syu"].ToString((IFormatProvider) CultureInfo.CurrentCulture) == "1")
        {
          this.currentOperation = MaintenanceOperation.Edit;
          this.ViewState["currentOperation"] = (object) this.currentOperation;
          this.wddStatus.Enabled = true;
          this.SetearRecord(systemUser.i_SystemUserId);
          this.wibCancel.Visible = false;
          this.ViewState["OnlyEdit"] = (object) "1";
          this.trModifyPassword.Visible = true;
          this.trPassword.Visible = false;
          this.tr1.Visible = false;
          this.tr2.Visible = false;
        }
      }
      else
      {
        this.Session["pobjSystemUser"] = (object) systemUser;
        this.txtUserRefId.Text = systemUser.v_Alias;
      }
    }

    protected void btnSearch_Click(object sender, EventArgs e) => this.SearchSystemUsers();

    protected void custPagerUserList_OnPageChanged(object sender, CustomPageChangeArgs e)
    {
      UsersListPagingParameters objParam = new UsersListPagingParameters();
      objParam.Alias = this.txtNameUserSearch.Text.TrimEnd();
      objParam.Nombres = this.txtNameSearch.Text.TrimEnd();
      objParam.Apellidos = this.txtLastNameSearch.Text.TrimEnd();
      objParam.Status = this.chkStatusSearch.Checked ? 1 : 0;
      objParam.CompanyId = this.wddCompanySearch.SelectedValue == "" ? -1 : Convert.ToInt32(this.wddCompanySearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      SIIV.BE.SystemUser systemUser = this.Session["SystemUser"] as SIIV.BE.SystemUser;
      objParam.i_SystemUserRefId = systemUser.i_SystemUserId;
      this.SearchSystemUsersList(objParam, false);
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
      this.currentOperation = MaintenanceOperation.AddNew;
      this.ViewState["currentOperation"] = (object) this.currentOperation;
      this.EnabledControls(MaintenanceOperation.AddNew);
      this.wibSave.Enabled = true;
      this.wddStatus.SelectedValue = "1";
      this.wddStatus.Enabled = false;
      this.lblMessageUser.Visible = false;
      this.lblMessageRoleUser.Visible = false;
      this.trwibFinalze.Visible = false;
      this.trManagementButtons.Visible = true;
      this.ClearControls();
      this.trModifyPassword.Visible = false;
      this.trPassword.Visible = false;
      this.lblContraseña.Text = "Contraseña";
      this.lblRepeatContraseña.Text = "Repita Contraseña";
      this.wddCompany.SelectedValue = (this.Session["SystemUser"] as SIIV.BE.SystemUser).i_CompanyId.ToString();
      this.rblPersonType.SelectedValue = "1";
      this.rblPersonType_SelectedIndexChanged((object) null, (EventArgs) null);
      this.wddDocumentType_SelectedIndexChanged((object) null, (EventArgs) null);
    }

    protected void wdgList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      int int32_1 = Convert.ToInt32(e.CommandArgument);
      if (this.wdgList.Rows[int32_1] == null)
        throw new HandledException(4, "Error de selección.", "'wdgList' - SystemUserManagement.aspx");
      if (e.CommandName.Equals("Edit", StringComparison.CurrentCulture))
      {
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
        this.trPassword.Visible = false;
        this.tr1.Visible = false;
        this.tr2.Visible = false;
      }
      else if (e.CommandName.Equals("Delete", StringComparison.CurrentCulture))
      {
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
        {
          Timeout = new TimeSpan(1, 1, 1)
        }))
        {
          try
          {
            SIIV.BE.SystemUser pobjBE = new SIIV.BE.SystemUser();
            pobjBE.i_SystemUserId = Convert.ToInt32(this.wdgList.DataKeys[int32_1]["i_SystemUserId"].ToString());
            pobjBE.i_Status = new int?(Convert.ToInt32(this.wdgList.DataKeys[int32_1]["i_Status"].ToString()));
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
      else
      {
        if (!e.CommandName.Equals("Rol", StringComparison.CurrentCulture))
          return;
        int int32_3 = Convert.ToInt32(this.wdgList.DataKeys[int32_1]["i_SystemUserId"].ToString());
        this.ViewState["i_SystemUserId"] = (object) int32_3;
        if (!this.GetUserConfig(int32_3))
          return;
        this.EnabledControlsRoleUser(false);
        this.ClearControlsRoleUser();
        this.lblMessageRoleUser.Visible = false;
        this.wibSaveRole.Enabled = false;
        this.wibNewRolUser.Enabled = true;
        this.wibCancelRolUser.Enabled = false;
        string script1 = UtilDA.ActiveTabIndexUser("tabs", 2, "0,1,3,4");
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script1, true);
        string script2 = "TabIndex();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script2, true);
      }
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

    protected void Unnamed2_Click(object sender, EventArgs e)
    {
      this.trModifyPassword.Visible = false;
      this.trPassword.Visible = true;
      this.tr1.Visible = true;
      this.tr2.Visible = true;
      if (this.Session["SystemUserId2"] != null)
        this.Label5.Text = "Contraseña de Administrador";
      else
        this.Label5.Text = "Contraseña Actual:";
    }

    protected void wibAsiggnedUserRef_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.CreatePopUpServer("Búsqueda Usuarios", "UserSearch.aspx", "800px", "570px");
    }

    protected void wibNoAsiggnedUserRef_Click(object sender, EventArgs e)
    {
      this.wibNoAsiggnedUserRef.Enabled = false;
      this.Session["pobjSystemUser"] = (object) null;
      this.txtUserRefId.Text = "";
      this.wibNoAsiggnedUserRef.Enabled = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      if (this.tr1.Visible)
      {
        this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperation"];
        SIIV.BE.SystemUser systemUser = new SIIV.BE.SystemUser();
        systemUser.v_Password = this.txtPassword.Text.Trim();
        string vPassword = this.objSystemUser.v_Password;
        string pstrValue = this.txtPassword2.Text.Trim();
        if (vPassword == Cryptography.GetHashMD5(systemUser.v_Password) && this.currentOperation == MaintenanceOperation.Edit)
        {
          this.RequiredFieldValidator3.IsValid = false;
          return;
        }
        if (vPassword != Cryptography.GetHashMD5(pstrValue) && this.currentOperation == MaintenanceOperation.Edit)
        {
          this.RequiredFieldValidator1.IsValid = false;
          return;
        }
        if (this.txtPassword.Text.Trim() != this.txtPasswordRepeat.Text.Trim())
        {
          this.txtPassword.Text = "";
          this.txtPasswordRepeat.Text = "";
          this.CompareValidator1.IsValid = false;
          return;
        }
        if (this.txtPassword.Text.Trim() == "")
        {
          this.RequiredFieldValidator4.IsValid = false;
          return;
        }
        if (this.txtPasswordRepeat.Text.Trim() == "")
        {
          this.RequiredFieldValidator5.IsValid = false;
          return;
        }
        if (this.txtPassword.Text.Length <= 7)
        {
          this.RequiredFieldValidator2.IsValid = false;
          return;
        }
      }
      if (!this.ValidateParameters())
        return;
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
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
        {
          Timeout = new TimeSpan(0, 1, 1)
        }))
        {
          try
          {
            switch (this.currentOperation)
            {
              case MaintenanceOperation.AddNew:
                new SystemUserQueriesBL().SystemUserInsert(this.GetCurrentUser());
                Message.SetMessage(this.lblMessageUser, enmMessageType.Success, "El Usuario se creó satisfactoriamente");
                this.trwibFinalze.Visible = true;
                this.trManagementButtons.Visible = false;
                break;
              case MaintenanceOperation.Edit:
                this.objSystemUser = (SIIV.BE.SystemUser) this.Session["SystemUser"];
                DataTable dataTable = new SystemUserQueriesBL().SystemUserRead(Convert.ToInt32(this.ViewState["i_SystemUserId"]));
                dataTable.Rows[0]["i_PersonTypeId"].ToString();
                if (this.objSystemUser.v_RoleName == "AAP Usuario Público Web" && this.txtAlias.Text != dataTable.Rows[0]["v_Alias"].ToString())
                  this.txtAlias.Text = dataTable.Rows[0]["v_Alias"].ToString();
                new SystemUserQueriesBL().SystemUserUpdate(this.GetCurrentUser());
                if (Convert.ToString(this.objSystemUser.i_SystemUserId) == dataTable.Rows[0]["i_SystemUserId"].ToString())
                {
                  this.objSystemUser.v_Email = this.txtEmail.Text;
                  this.objSystemUser.v_FirstName = this.txtName.Text;
                  this.objSystemUser.v_LastName = this.txtLastName.Text;
                }
                Message.SetMessage(this.lblMessageUser, enmMessageType.Success, "<br/> El Usuario se modificó satisfactoriamente <br/>");
                if (Convert.ToInt16(this.ViewState["OnlyEdit"], (IFormatProvider) CultureInfo.CurrentCulture) != (short) 1)
                {
                  this.SearchSystemUsers();
                  this.trwibFinalze.Visible = true;
                  this.trManagementButtons.Visible = false;
                  break;
                }
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
      }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      this.Session["SystemUserId2"] = (object) null;
      this.EnabledControls(MaintenanceOperation.None);
    }

    protected void btnJavaScriptResponse_Click(object sender, EventArgs e)
    {
      if (this.Session["pobjSystemUser"] == null)
        return;
      this.txtUserRefId.Text = ((SIIV.BE.SystemUser) this.Session["pobjSystemUser"]).v_Alias;
    }

    protected void btnCancelRoleUser_Click(object sender, EventArgs e)
    {
      string script1 = UtilDA.ActiveTabIndexUser("tabs", 0, "1,2,3,4");
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel3, this.UpdatePanel3.GetType(), "Script", script1, true);
      string script2 = "TabIndex();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel3, this.UpdatePanel3.GetType(), "ScriptIndex", script2, true);
    }

    private void LoadParameters()
    {
      try
      {
        SystemParameterManagementBL parameterManagementBl = new SystemParameterManagementBL();
        SIIV.BE.SystemUser systemUser = (SIIV.BE.SystemUser) this.Session["SystemUser"];
        ArrayList pobj = new ArrayList()
        {
          (object) ("" + SystemParameterGroups.ModelEntities.ToString() + ", " + SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ", " + SystemParameterGroups.ModelApplication.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
          (object) "",
          (object) "1",
          (object) "1"
        };
        List<SIIV.BE.SystemParameter> systemParameterList = parameterManagementBl.Get((object) pobj);
        this.wddCompanySearch.Items.Clear();
        this.wddCompany.Items.Clear();
        this.wddDocumentType.Items.Clear();
        DataTable dataTable1 = new DataTable();
        dataTable1.Columns.Add("Id", Type.GetType("System.Int32"));
        dataTable1.Columns.Add("Nombre", Type.GetType("System.String"));
        int iParameterId;
        foreach (SIIV.BE.SystemParameter systemParameter in systemParameterList)
        {
          if (systemParameter.i_GroupId == SystemParameterGroups.ModelEntities)
          {
            ListItemCollection items1 = this.wddCompanySearch.Items;
            string vDescription1 = systemParameter.v_Description;
            iParameterId = systemParameter.i_ParameterId;
            string str1 = iParameterId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
            System.Web.UI.WebControls.ListItem listItem1 = new System.Web.UI.WebControls.ListItem(vDescription1, str1);
            items1.Add(listItem1);
            ListItemCollection items2 = this.wddCompany.Items;
            string vDescription2 = systemParameter.v_Description;
            iParameterId = systemParameter.i_ParameterId;
            string str2 = iParameterId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
            System.Web.UI.WebControls.ListItem listItem2 = new System.Web.UI.WebControls.ListItem(vDescription2, str2);
            items2.Add(listItem2);
          }
          else if (systemParameter.i_GroupId == SystemParameterGroups.PersonDocumentType)
          {
            ListItemCollection items = this.wddDocumentType.Items;
            string vDescription = systemParameter.v_Description;
            iParameterId = systemParameter.i_ParameterId;
            string str = iParameterId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
            System.Web.UI.WebControls.ListItem listItem = new System.Web.UI.WebControls.ListItem(vDescription, str);
            items.Add(listItem);
          }
          else if (systemParameter.i_GroupId == SystemParameterGroups.ModelApplication)
          {
            DataRow row = dataTable1.NewRow();
            row["Id"] = (object) systemParameter.i_ParameterId;
            row["Nombre"] = (object) systemParameter.v_Description;
            dataTable1.Rows.Add(row);
          }
        }
        this.wddCompanySearch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- Todos - ", "-1"));
        this.wddCompany.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- Seleccione - ", "-1"));
        this.wddDocumentType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- Seleccione - ", "-1"));
        this.wddStatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- Seleccione - ", "-1"));
        this.wddStatus.Items.Add(new System.Web.UI.WebControls.ListItem("Activo", "1"));
        this.wddStatus.Items.Add(new System.Web.UI.WebControls.ListItem("Inactivo", "0"));
        this.wddStatus.SelectedValue = "1";
        this.wddStatusRoleUser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- Seleccione - ", "-1"));
        this.wddStatusRoleUser.Items.Add(new System.Web.UI.WebControls.ListItem("Activo", "1"));
        this.wddStatusRoleUser.Items.Add(new System.Web.UI.WebControls.ListItem("Inactivo", "0"));
        this.wddStatusRoleUser.SelectedValue = "1";
        List<RoleConfig> roleConfigList = new RoleConfigManagementBL().Get((object) new ArrayList()
        {
          (object) "",
          (object) "1"
        });
        string inheritableRoles = systemUser.v_InheritableRoles;
        char[] chArray = new char[1]{ '|' };
        foreach (string str3 in inheritableRoles.Split(chArray))
        {
          foreach (RoleConfig roleConfig in roleConfigList)
          {
            if (str3 == roleConfig.i_RoleConfigId.ToString((IFormatProvider) CultureInfo.CurrentCulture))
            {
              string str4 = "";
              foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
              {
                int int32 = Convert.ToInt32(row["Id"], (IFormatProvider) CultureInfo.CurrentCulture);
                int? iApplicationId = roleConfig.i_ApplicationId;
                int valueOrDefault = iApplicationId.GetValueOrDefault();
                if (int32 == valueOrDefault & iApplicationId.HasValue)
                  str4 = row["Nombre"].ToString();
              }
              this.wddRole.Items.Add(new System.Web.UI.WebControls.ListItem(roleConfig.v_Name + (str4.Length > 0 ? "-->" + str4 : ""), roleConfig.i_RoleConfigId.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
            }
          }
        }
        this.wddRole.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- Seleccione - ", "-1"));
        List<Location> list = new LocationManagementBL().Get((object) new ArrayList()
        {
          (object) "",
          (object) ""
        });
        DataTable dataTable2 = new DataTable();
        DataView defaultView = list.GetDataTableFromClass<Location>().DefaultView;
        defaultView.Sort = "v_Description asc";
        foreach (DataRow row in (InternalDataCollectionBase) defaultView.ToTable().Rows)
          this.wddLocation.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_LocationId"].ToString()));
        this.wddLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- Seleccione - ", "-1"));
        if (systemUser == null)
          return;
        this.wddCompanySearch.SelectedValue = systemUser.i_CompanyId.ToString();
        this.wddCompany.SelectedValue = systemUser.i_CompanyId.ToString();
        this.wddCompanySearch.Enabled = false;
        this.wddCompany.Enabled = false;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    private void EnabledControls(MaintenanceOperation penuCurrentOperation)
    {
      string str = this.H1.Value;
      switch (penuCurrentOperation)
      {
        case MaintenanceOperation.AddNew:
          this.wddStatus.Enabled = false;
          if (str == "0")
          {
            string script1 = UtilDA.ActiveTabIndexUser("tabs", 1, "0,2,3,4");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script1, true);
            string script2 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script2, true);
            break;
          }
          string script3 = UtilDA.ActiveTabIndexUser("tabs", 0, "1,2,3,4");
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script3, true);
          string script4 = "TabIndex();";
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script4, true);
          break;
        case MaintenanceOperation.Edit:
          this.wddStatus.Enabled = true;
          if (str == "0")
          {
            string script5 = UtilDA.ActiveTabIndexUser("tabs", 1, "0,2,3,4");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script5, true);
            string script6 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script6, true);
            break;
          }
          string script7 = UtilDA.ActiveTabIndexUser("tabs", 0, "1,2,3,4");
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script7, true);
          string script8 = "TabIndex();";
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script8, true);
          break;
        case MaintenanceOperation.Delete:
          if (str == "0")
          {
            string script9 = UtilDA.ActiveTabIndexUser("tabs", 1, "0,2,3,4");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script9, true);
            string script10 = "TabIndex();";
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script10, true);
            break;
          }
          string script11 = UtilDA.ActiveTabIndexUser("tabs", 0, "1,2,3,4");
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script11, true);
          string script12 = "TabIndex();";
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script12, true);
          break;
        default:
          string script13 = UtilDA.ActiveTabIndexUser("tabs", 0, "1,2,3,4");
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script13, true);
          string script14 = "TabIndex();";
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script14, true);
          break;
      }
    }

    private void SetearRecord(int i_SystemUserId)
    {
      this.ViewState[nameof (i_SystemUserId)] = (object) i_SystemUserId;
      DataTable dataTable = new SystemUserQueriesBL().SystemUserRead(i_SystemUserId);
      if (dataTable.Rows[0]["i_PersonTypeId"].ToString() == "1" || dataTable.Rows[0]["i_PersonTypeId"].ToString() == "2")
        this.rblPersonType.SelectedValue = dataTable.Rows[0]["i_PersonTypeId"].ToString();
      else
        this.rblPersonType.SelectedValue = "1";
      this.rblPersonType_SelectedIndexChanged((object) null, (EventArgs) null);
      this.wddCompany.SelectedValue = dataTable.Rows[0]["i_CompanyId"].ToString();
      this.txtAlias.Text = dataTable.Rows[0]["v_Alias"].ToString();
      this.wddDocumentType.SelectedValue = dataTable.Rows[0]["i_DocumentTypeId"].ToString();
      this.txtDocumentNumber.Text = dataTable.Rows[0]["v_DocumentNumber"].ToString();
      this.txtName.Text = dataTable.Rows[0]["v_FirstName"].ToString();
      this.txtLastName.Text = dataTable.Rows[0]["v_LastName"].ToString();
      this.txtEmail.Text = dataTable.Rows[0]["v_Email"].ToString();
      this.txtPhoneNumber.Text = dataTable.Rows[0]["v_Telephone"].ToString();
      this.txtAddress.Text = dataTable.Rows[0]["v_Address"].ToString();
      this.wddStatus.SelectedValue = dataTable.Rows[0]["i_Status"].ToString();
      this.txtCompany.Text = dataTable.Rows[0]["v_FirstName"].ToString();
      if (!Convert.IsDBNull(dataTable.Rows[0]["i_SystemUserRefId"]))
      {
        this.Session["pobjSystemUser"] = (object) new SIIV.BE.SystemUser()
        {
          i_SystemUserId = Convert.ToInt32(dataTable.Rows[0]["i_SystemUserRefId"], (IFormatProvider) CultureInfo.CurrentCulture),
          v_Alias = dataTable.Rows[0]["v_SystemUserRefName"].ToString()
        };
        this.txtUserRefId.Text = dataTable.Rows[0]["v_SystemUserRefName"].ToString();
      }
      else
        this.txtUserRefId.Text = "";
    }

    private bool ValidateParameters()
    {
      this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperation"];
      string pstrMessage = "";
      if (this.wddCompany.SelectedValue == "" || this.wddCompany.SelectedValue == "-1")
        pstrMessage = "Debe selecionar la compañía a la que pertenece el usuario";
      else if (this.txtAlias.Text.Length == 0)
        pstrMessage = "Debe ingresar el alias";
      else if (this.txtPassword.Text.Length == 0 && MaintenanceOperation.AddNew == this.currentOperation)
        pstrMessage = "Debe ingresar la contraseña";
      else if (this.wddDocumentType.SelectedValue == "" || this.wddDocumentType.SelectedValue == "-1")
        pstrMessage = "Debe selecionar el tipo de documento";
      else if (this.txtDocumentNumber.Text.Length == 0 && this.wddDocumentType.SelectedValue == "-1")
        pstrMessage = "Debe ingresar el número de documento";
      else if (this.txtName.Text.Length == 0 && this.rblPersonType.SelectedValue == "1")
        pstrMessage = "Debe ingresar el nombre";
      else if (this.txtLastName.Text.Length == 0 && this.rblPersonType.SelectedValue == "1")
        pstrMessage = "Debe ingresar el apellido";
      else if (this.txtCompany.Text.Length == 0 && this.rblPersonType.SelectedValue == "2")
        pstrMessage = "Debe ingresar la razón social";
      else if (this.txtEmail.Text.Length == 0)
        pstrMessage = "Debe ingresar el Email";
      else if (this.wddStatus.SelectedValue == "" || this.wddStatus.SelectedValue == "-1")
        pstrMessage = "Debe seleccionar el estado";
      else if (this.wddDocumentType.SelectedValue == "4" && this.rblPersonType.SelectedValue == "1")
        pstrMessage = "Ingrese un tipo de documento correcto para el tipo de Entidad Natural.";
      if (pstrMessage.Length <= 0)
        return true;
      Message.SetMessage(this.lblMessageUser, enmMessageType.Warning, pstrMessage);
      return false;
    }

    private void SearchSystemUsers()
    {
      try
      {
        string script = "var exist = document.getElementById(\"MainContent_txtPassword_PasswordStrengthBar2\");if(exist);{document.getElementById(\"MainContent_txtPassword_PasswordStrengthBar2\").remove();}";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
        UsersListPagingParameters objParam = new UsersListPagingParameters();
        objParam.Alias = this.txtNameUserSearch.Text.TrimEnd();
        objParam.Nombres = this.txtNameSearch.Text.TrimEnd();
        objParam.Apellidos = this.txtLastNameSearch.Text.TrimEnd();
        objParam.Status = this.chkStatusSearch.Checked ? 1 : 0;
        objParam.CompanyId = this.wddCompanySearch.SelectedValue == "" ? -1 : Convert.ToInt32(this.wddCompanySearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        SIIV.BE.SystemUser systemUser = this.Session["SystemUser"] as SIIV.BE.SystemUser;
        objParam.i_SystemUserRefId = systemUser.i_SystemUserId;
        this.SearchSystemUsersList(objParam, true);
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

    private void SearchSystemUsersList(UsersListPagingParameters objParam, bool pboolLoadPager)
    {
      int pintstartRowIndex = pboolLoadPager ? 1 : this.custPagerUserList.CurrentPageNumber;
      int pintmaxRows = this.custPagerUserList.CurrentPageSize == 0 ? 10 : this.custPagerUserList.CurrentPageSize;
      int pinttotalRows;
      DataTable all = new ManagementPagingBL().UserReportGetAll(objParam, pintstartRowIndex, pintmaxRows, out pinttotalRows);
      int num = pinttotalRows;
      this.wdgList.DataSource = (object) all;
      this.wdgList.DataBind();
      this.custPagerUserList.TotalPages = num % pintmaxRows == 0 ? num / pintmaxRows : num / pintmaxRows + 1;
      this.custPagerUserList.TotalRecordCount = pinttotalRows;
      if (!pboolLoadPager)
        return;
      this.custPagerUserList.LoadPager();
    }

    private void ClearControls()
    {
      this.wddCompany.SelectedValue = "-1";
      this.txtAlias.Text = "";
      this.txtPassword.Text = "";
      this.wddDocumentType.SelectedValue = "-1";
      this.txtDocumentNumber.Text = "";
      this.txtName.Text = "";
      this.txtLastName.Text = "";
      this.txtEmail.Text = "";
      this.txtPhoneNumber.Text = "";
      this.txtAddress.Text = "";
    }

    public bool GetUserConfig(int i_SystemUserId)
    {
      try
      {
        this.wdgUserRoles.DataSource = (object) new UserConfigManagementBL().GetAll(i_SystemUserId);
        this.wdgUserRoles.DataBind();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageUserList, enmMessageType.Error, ex.Message);
        return false;
      }
      return true;
    }

    private SIIV.BE.SystemUser GetCurrentUser()
    {
      this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperation"];
      SIIV.BE.SystemUser systemUser1 = (SIIV.BE.SystemUser) this.Session["SystemUser"];
      SIIV.BE.SystemUser currentUser = new SIIV.BE.SystemUser();
      switch (this.currentOperation)
      {
        case MaintenanceOperation.AddNew:
          currentUser.i_SystemUserId = 0;
          currentUser.i_InsertUserId = new int?(systemUser1.i_SystemUserId);
          currentUser.v_Ubigeo = "";
          break;
        case MaintenanceOperation.Edit:
          DataTable dataTable = new SystemUserQueriesBL().SystemUserRead(Convert.ToInt32(this.ViewState["i_SystemUserId"]));
          string str = dataTable.Rows[0]["v_Ubigeo"].ToString();
          string source1 = dataTable.Rows[0]["v_Question_Answer"].ToString();
          string source2 = dataTable.Rows[0]["v_Code"].ToString();
          string source3 = dataTable.Rows[0]["d_InsertDate"].ToString();
          string source4 = dataTable.Rows[0]["i_InsertUserId"].ToString();
          currentUser.v_Ubigeo = str;
          currentUser.i_SystemUserId = Convert.ToInt32(this.ViewState["i_SystemUserId"], (IFormatProvider) CultureInfo.CurrentCulture);
          currentUser.i_UpdateUserId = new int?(systemUser1.i_SystemUserId);
          if (source1.Any<char>())
            currentUser.v_Question_Answer = source1;
          if (source2.Any<char>())
            currentUser.v_Code = source2;
          if (source4.Any<char>())
            currentUser.i_InsertUserId = new int?(Convert.ToInt32(source4));
          if (source3.Any<char>())
          {
            currentUser.d_InsertDate = new DateTime?(Convert.ToDateTime(source3));
            break;
          }
          break;
      }
      currentUser.i_CompanyId = new int?(Convert.ToInt32(this.wddCompany.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
      currentUser.v_Alias = this.txtAlias.Text.TrimEnd();
      if (this.txtPassword.Text.Length > 0)
      {
        currentUser.v_Password = Cryptography.GetHashMD5(this.txtPassword.Text.TrimEnd());
        systemUser1.v_Password = currentUser.v_Password;
      }
      currentUser.i_PersonTypeId = new int?(Convert.ToInt32(this.rblPersonType.SelectedValue));
      currentUser.i_DocumentTypeId = new int?(Convert.ToInt32(this.wddDocumentType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
      currentUser.v_DocumentNumber = this.txtDocumentNumber.Text.TrimEnd();
      currentUser.v_FirstName = this.rblPersonType.SelectedValue == "1" ? this.txtName.Text.TrimEnd() : this.txtCompany.Text.TrimEnd();
      currentUser.v_LastName = this.rblPersonType.SelectedValue == "1" ? this.txtLastName.Text.TrimEnd() : "";
      currentUser.v_Email = this.txtEmail.Text.TrimEnd();
      currentUser.v_Telephone = this.txtPhoneNumber.Text.TrimEnd();
      currentUser.v_Address = this.txtAddress.Text.TrimEnd();
      currentUser.i_Status = new int?(Convert.ToInt32(this.wddStatus.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
      SIIV.BE.SystemUser systemUser2 = (SIIV.BE.SystemUser) this.Session["pobjSystemUser"];
      int iSystemUserId = currentUser.i_SystemUserId;
      int? iUpdateUserId = currentUser.i_UpdateUserId;
      int valueOrDefault = iUpdateUserId.GetValueOrDefault();
      if (!(iSystemUserId == valueOrDefault & iUpdateUserId.HasValue))
      {
        int? iSystemUserRefId = systemUser2.i_SystemUserRefId;
        currentUser.i_SystemUserRefId = !iSystemUserRefId.HasValue ? new int?(systemUser1.i_SystemUserId) : systemUser2.i_SystemUserRefId;
      }
      else
        currentUser.i_SystemUserRefId = new int?(systemUser1.i_SystemUserId);
      return currentUser;
    }

    protected void wibNewRolUser_Click(object sender, EventArgs e)
    {
      this.ClearControlsRoleUser();
      this.EnabledControlsRoleUser(true);
      this.lblMessageRoleUser.Visible = false;
      this.wibSaveRole.Enabled = true;
      this.wibNewRolUser.Enabled = false;
      this.wibCancelRolUser.Enabled = true;
      this.ViewState["i_UserConfigId"] = (object) null;
    }

    private void ClearControlsRoleUser()
    {
      this.wddRole.SelectedIndex = 0;
      this.wddLocation.SelectedIndex = 0;
      this.wddStatusRoleUser.SelectedIndex = 0;
      this.chkAutoApprovalRequirement.Checked = false;
      this.chkValidateVoucher.Checked = false;
    }

    private void EnabledControlsRoleUser(bool enabled)
    {
      this.wddRole.Enabled = enabled;
      this.wddLocation.Enabled = enabled;
      this.wddStatusRoleUser.Enabled = enabled;
      this.chkAutoApprovalRequirement.Enabled = enabled;
      this.chkValidateVoucher.Enabled = enabled;
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
      this.lblMessageUser.Visible = false;
      this.wddDocumentType_SelectedIndexChanged((object) null, (EventArgs) null);
    }

    protected void wibSaveRole_Click(object sender, EventArgs e)
    {
      if (!this.ValidateControlsAssociate())
        return;
      this.SaveDataUserConfig();
    }

    private bool ValidateControlsAssociate()
    {
      string pstrMessage = "";
      this.lblMessageRoleUser.Visible = false;
      if (this.wddRole.SelectedValue == "-1")
      {
        pstrMessage = "Debe seleccionar un rol para poder realizar la asociación";
        Message.SetMessage(this.lblMessageRoleUser, enmMessageType.Warning, pstrMessage);
      }
      else if (this.wddLocation.SelectedValue == "-1")
      {
        pstrMessage = "Debe seleccionar un ubicacion para poder realizar la asociación";
        Message.SetMessage(this.lblMessageRoleUser, enmMessageType.Warning, pstrMessage);
      }
      else if (this.wddStatusRoleUser.SelectedValue == "-1")
      {
        pstrMessage = "Debe seleccionar el estado de la asociación";
        Message.SetMessage(this.lblMessageRoleUser, enmMessageType.Warning, pstrMessage);
      }
      return pstrMessage.Length <= 0;
    }

    public void SaveDataUserConfig()
    {
      using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
      {
        Timeout = new TimeSpan(1, 1, 1)
      }))
      {
        try
        {
          UserConfig pobjBE = new UserConfig();
          SIIV.BE.SystemUser systemUser = (SIIV.BE.SystemUser) this.Session["SystemUser"];
          pobjBE.i_UserConfigId = this.ViewState["i_UserConfigId"] == null ? 0 : Convert.ToInt32(this.ViewState["i_UserConfigId"], (IFormatProvider) CultureInfo.CurrentCulture);
          pobjBE.i_SystemUserId = new int?(Convert.ToInt32(this.ViewState["i_SystemUserId"], (IFormatProvider) CultureInfo.CurrentCulture));
          pobjBE.i_RoleConfigId = new int?(Convert.ToInt32(this.wddRole.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
          pobjBE.i_LocationId = new int?(Convert.ToInt32(this.wddLocation.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
          pobjBE.i_Status = new int?(Convert.ToInt32(this.wddStatusRoleUser.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
          pobjBE.i_InsertUserId = new int?(systemUser.i_SystemUserId);
          string str = (this.chkAutoApprovalRequirement.Checked ? "1" : "") + (!this.chkAutoApprovalRequirement.Checked || !this.chkValidateVoucher.Checked ? "" : "|") + (this.chkValidateVoucher.Checked ? "2" : "");
          pobjBE.v_ExtendedAction = this.chkAutoApprovalRequirement.Checked || this.chkValidateVoucher.Checked ? str : (string) null;
          if ((pobjBE.i_UserConfigId != 0 ? new UserConfigManagementBL().Update(pobjBE) : new UserConfigManagementBL().Insert(ref pobjBE)) == 1)
          {
            Message.SetMessage(this.lblMessageRoleUser, enmMessageType.Success, "Se asoció satisfactoriamente el Rol");
            this.ClearControlsRoleUser();
            this.EnabledControlsRoleUser(false);
            this.wibNewRolUser.Enabled = true;
            this.wibCancelRolUser.Enabled = false;
            this.wibSaveRole.Enabled = false;
            transactionScope.Complete();
          }
          else
          {
            Message.SetMessage(this.lblMessageRoleUser, enmMessageType.Warning, "Se encontró un problema en la asociación del Rol");
            this.wibSaveRole.Enabled = true;
            return;
          }
        }
        catch (Exception ex)
        {
          Message.SetMessage(this.lblMessageRoleUser, enmMessageType.Error, ex.Message);
          this.wibSaveRole.Enabled = true;
        }
      }
      this.GetUserConfig(Convert.ToInt32(this.ViewState["i_SystemUserId"], (IFormatProvider) CultureInfo.CurrentCulture));
    }

    protected void wibCancelRolUser_Click(object sender, EventArgs e)
    {
      this.ClearControlsRoleUser();
      this.lblMessageRoleUser.Visible = false;
      this.wibNewRolUser.Enabled = true;
      this.wibSaveRole.Enabled = false;
      this.wibCancelRolUser.Enabled = false;
      this.EnabledControlsRoleUser(false);
    }

    protected void wibReturn_Click(object sender, EventArgs e)
    {
      string script1 = UtilDA.ActiveTabIndexUser("tabs", 0, "1,2,3,4");
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel3, this.UpdatePanel3.GetType(), "Script", script1, true);
      string script2 = "TabIndex();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel3, this.UpdatePanel3.GetType(), "ScriptIndex", script2, true);
    }

    protected void wdgUserRoles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (!(e.CommandName == "Edit"))
        return;
      int int32 = Convert.ToInt32(e.CommandArgument);
      if (this.wdgUserRoles.Rows[int32] == null)
        throw new HandledException(4, "Error de selección.", "'wdgUserRoles' - SystemUserManagement.aspx");
      int num1 = int.Parse(this.wdgUserRoles.DataKeys[int32]["i_UserConfigId"].ToString());
      int.Parse(this.wdgUserRoles.DataKeys[int32]["i_RoleConfigId"].ToString());
      int num2 = this.wdgUserRoles.DataKeys[int32]["i_LocationId"].ToString() == "" ? 0 : int.Parse(this.wdgUserRoles.DataKeys[int32]["i_LocationId"].ToString());
      int num3 = int.Parse(this.wdgUserRoles.DataKeys[int32]["i_Status"].ToString());
      string str1 = this.wdgUserRoles.DataKeys[int32]["v_ExtendedAction"].ToString();
      int.Parse(this.wdgUserRoles.DataKeys[int32]["i_ApplicationId"].ToString());
      this.wddRole.SelectedIndex = 0;
      this.wddLocation.SelectedIndex = 0;
      this.wddStatusRoleUser.SelectedValue = num3.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      string str2 = str1;
      char[] chArray = new char[1]{ '|' };
      foreach (string str3 in str2.Split(chArray))
      {
        if (str3.Length > 0)
        {
          if (str3 == "1")
            this.chkAutoApprovalRequirement.Checked = true;
          if (str3 == "2")
            this.chkValidateVoucher.Checked = true;
        }
      }
      this.ViewState["i_UserConfigId"] = (object) num1;
      this.EnabledControlsRoleUser(true);
      this.lblMessageRoleUser.Visible = false;
      this.wibNewRolUser.Enabled = false;
      this.wibSaveRole.Enabled = true;
      this.wibCancelRolUser.Enabled = true;
    }

    private void CreatePopUp(string url, string pstrtitle, string width, string height)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) url, (object) pstrtitle, (object) (width + "px"), (object) (height + "px"));
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

    protected void wdgUserRoles_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgUserRoles_RowEditing(object sender, GridViewEditEventArgs e)
    {
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
