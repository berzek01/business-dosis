// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Operation.AssociatedChildManagement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.Exhibition.BL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Operation
{
  public class AssociatedChildManagement : Page
  {
    private AssociatedManagementBL pobjAssociatedManagementBL;
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtCorporateName;
    protected Button wibSave;
    protected Label Label12;
    protected TextBox txtUserName;
    protected RequiredFieldValidator RequiredFieldValidator4;
    protected ValidatorCalloutExtender ValidatorCalloutExtender4;
    protected Label Label2;
    protected TextBox txtPassword;
    protected Label lblMessageChild;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.LoadDataAssociatedChild();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessageChild, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageChild, new HandledException(-100, ex));
      }
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        this.pobjAssociatedManagementBL = new AssociatedManagementBL();
        string v_Sede = "";
        this.ValidTransaction(this.pobjAssociatedManagementBL.AssociatedModifiedPass(systemUser.i_SystemUserId, Cryptography.GetHashMD5(this.txtPassword.Text.Trim()), systemUser.i_SystemUserId, DateTime.Now, v_Sede), "modificado");
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessageChild, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageChild, new HandledException(-100, ex));
      }
    }

    private void LoadDataAssociatedChild()
    {
      try
      {
        DataTable dataTable = this.Session["SystemUser"] != null ? new AssociatedQueriesBL().AssociatedRead(((SystemUser) this.Session["SystemUser"]).i_SystemUserId) : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - AssociatedChildManagement.aspx");
        if (dataTable == null || dataTable.Rows.Count == 0)
          return;
        DataRow row = dataTable.Rows[0];
        string str1 = row["v_ReasonSocial"].ToString();
        string str2 = row["v_UserName"].ToString();
        this.txtCorporateName.Text = str1;
        this.txtUserName.Text = str2;
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
          Message.SetMessage(this.lblMessageChild, new HandledException(2, "El alias de asociado hijo fue " + v_Action + " correctamente"));
        else
          Message.SetMessage(this.lblMessageChild, new HandledException(1, "Se encontró un problema en la actualización de los datos"));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
