// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.SystemUser.UpdateAccount
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.SystemParameter.BL;
using SIIV.SystemUser.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.SystemUser
{
  public class UpdateAccount : Page
  {
    public int SystemUserId;
    protected UpdatePanel UpdatePanel1;
    protected HtmlTableRow trPassword;
    protected Label Label2;
    protected TextBox txtPassword;
    protected Label Label3;
    protected DropDownList wddDocumentType;
    protected Label Label4;
    protected TextBox txtDocumentNumber;
    protected FilteredTextBoxExtender txtDocumentNumber_FilteredTextBoxExtender;
    protected Label Label5;
    protected TextBox txtName;
    protected Label Label6;
    protected TextBox txtLastName;
    protected Label Label7;
    protected TextBox txtEmail;
    protected RegularExpressionValidator RegularExpressionValidator1;
    protected ValidatorCalloutExtender RegularExpressionValidator1_ValidatorCalloutExtender;
    protected Label Label8;
    protected TextBox txtPhoneNumber;
    protected FilteredTextBoxExtender txtPhoneNumber_FilteredTextBoxExtender;
    protected Label Label9;
    protected TextBox txtAddress;
    protected HtmlTableRow trManagementButtons;
    protected Button wibSave;
    protected Label lblMessageUser;

    private void LoadParameters()
    {
      try
      {
        SystemParameterManagementBL parameterManagementBl = new SystemParameterManagementBL();
        SIIV.BE.SystemUser systemUser = (SIIV.BE.SystemUser) this.Session["SystemUser"];
        ArrayList pobj = new ArrayList()
        {
          (object) ("" + SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
          (object) "",
          (object) "1",
          (object) "1"
        };
        List<SIIV.BE.SystemParameter> systemParameterList = parameterManagementBl.Get((object) pobj);
        this.wddDocumentType.Items.Clear();
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Id", Type.GetType("System.Int32"));
        dataTable.Columns.Add("Nombre", Type.GetType("System.String"));
        foreach (SIIV.BE.SystemParameter systemParameter in systemParameterList)
        {
          if (systemParameter.i_GroupId == SystemParameterGroups.PersonDocumentType)
            this.wddDocumentType.Items.Add(new System.Web.UI.WebControls.ListItem(systemParameter.v_Description, systemParameter.i_ParameterId.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
          else if (systemParameter.i_GroupId == SystemParameterGroups.ModelApplication)
          {
            DataRow row = dataTable.NewRow();
            row["Id"] = (object) systemParameter.i_ParameterId;
            row["Nombre"] = (object) systemParameter.v_Description;
            dataTable.Rows.Add(row);
          }
        }
        this.wddDocumentType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- Seleccione - ", "-1"));
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    private bool ValidateParameters()
    {
      string pstrMessage = "";
      if (this.wddDocumentType.SelectedValue == "" || this.wddDocumentType.SelectedValue == "-1")
        pstrMessage = "Debe selecionar el tipo de documento del usuario";
      else if (this.txtDocumentNumber.Text.Length == 0)
        pstrMessage = "Debe ingresar el numero de documento del usuario ";
      else if (this.txtName.Text.Length == 0)
        pstrMessage = "Debe ingresar el nombre del usuario ";
      else if (this.txtLastName.Text.Length == 0)
        pstrMessage = "Debe ingresar el Apellido del usuario ";
      else if (this.txtEmail.Text.Length == 0)
        pstrMessage = "Debe ingresar el Email del usuario ";
      if (pstrMessage.Length <= 0)
        return true;
      Message.SetMessage(this.lblMessageUser, enmMessageType.Warning, pstrMessage);
      return false;
    }

    private SIIV.BE.SystemUser GetCurrentUser()
    {
      SIIV.BE.SystemUser systemUser1 = (SIIV.BE.SystemUser) this.Session["SystemUser"];
      SIIV.BE.SystemUser currentUser = new SIIV.BE.SystemUser();
      currentUser.i_SystemUserId = Convert.ToInt32(this.ViewState["i_SystemUserId"], (IFormatProvider) CultureInfo.CurrentCulture);
      currentUser.i_UpdateUserId = new int?(systemUser1.i_SystemUserId);
      if (this.txtPassword.Text.Length > 0)
        currentUser.v_Password = Cryptography.GetHashMD5(this.txtPassword.Text.TrimEnd());
      currentUser.i_DocumentTypeId = new int?(Convert.ToInt32(this.wddDocumentType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
      currentUser.v_DocumentNumber = this.txtDocumentNumber.Text.TrimEnd();
      currentUser.v_FirstName = this.txtName.Text.TrimEnd();
      currentUser.v_LastName = this.txtLastName.Text.TrimEnd();
      currentUser.v_Email = this.txtEmail.Text.TrimEnd();
      currentUser.v_Telephone = this.txtPhoneNumber.Text.TrimEnd();
      currentUser.v_Address = this.txtAddress.Text.TrimEnd();
      if (this.Session["pobjSystemUser"] != null)
      {
        SIIV.BE.SystemUser systemUser2 = (SIIV.BE.SystemUser) this.Session["pobjSystemUser"];
        currentUser.i_SystemUserRefId = new int?(systemUser2.i_SystemUserId);
      }
      else
        currentUser.i_SystemUserRefId = new int?(0);
      currentUser.v_Question_Answer = "";
      currentUser.v_Code = "";
      return currentUser;
    }

    private void SetearRecord(int i_SystemUserId)
    {
      this.ViewState[nameof (i_SystemUserId)] = (object) i_SystemUserId;
      DataTable dataTable = new SystemUserQueriesBL().SystemUserRead(i_SystemUserId);
      this.wddDocumentType.SelectedValue = dataTable.Rows[0]["i_DocumentTypeId"].ToString();
      this.txtDocumentNumber.Text = dataTable.Rows[0]["v_DocumentNumber"].ToString();
      this.txtName.Text = dataTable.Rows[0]["v_FirstName"].ToString();
      this.txtLastName.Text = dataTable.Rows[0]["v_LastName"].ToString();
      this.txtEmail.Text = dataTable.Rows[0]["v_Email"].ToString();
      this.txtPhoneNumber.Text = dataTable.Rows[0]["v_Telephone"].ToString();
      this.txtAddress.Text = dataTable.Rows[0]["v_Address"].ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadParameters();
      this.SetearRecord(((SIIV.BE.SystemUser) this.Session["SystemUser"]).i_SystemUserId);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        if (!this.ValidateParameters())
          return;
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
        {
          Timeout = new TimeSpan(0, 1, 1)
        }))
        {
          try
          {
            new SystemUserQueriesBL().SystemUserUpdateAccount(this.GetCurrentUser());
            Message.SetMessage(this.lblMessageUser, enmMessageType.Success, "El Usuario se modifico satisfactoriamente");
            transactionScope.Complete();
          }
          catch (Exception ex)
          {
            Message.SetMessage(this.lblMessageUser, enmMessageType.Error, ex.Message);
            this.trManagementButtons.Visible = true;
          }
        }
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageUser, enmMessageType.Error, ex.Message);
      }
    }
  }
}
