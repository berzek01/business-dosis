// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Operation.ChangesAssociatedChild
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

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
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Operation
{
  public class ChangesAssociatedChild : Page
  {
    private ExhibitionAssociated pobjexhibitionassociated;
    private AssociatedQueriesBL pbojAssociatedQueriesBL;
    private string v_Option = "";
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected UpdatePanel UpdatePanel1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected TextBox txtCorporateName;
    protected TextBox txtRuc;
    protected TextBox txtSede;
    protected FilteredTextBoxExtender ftbeSede;
    protected Label Label12;
    protected TextBox txtUserName;
    protected Label Label2;
    protected TextBox txtPassword;
    protected Button wibSave;
    protected Button btnReturnPopupConfirmation;
    protected Button btnReturnPopupConfirmation2;
    protected Button wibEnable;
    protected Button wibReturn;
    protected Button wibDelete;
    protected Button wibDisable;
    protected Label lblMessageChild;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessageChild.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.Initialize();
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
        this.Validar();
        if (this.ViewState["Popup"] != null)
          return;
        if (this.Request.QueryString["i_AssociatedIdNew"] != null && this.Request.QueryString["i_SystemUserIdNew"] != null)
        {
          int int32_1 = Convert.ToInt32(this.Request.QueryString["i_SystemUserIdNew"].ToString());
          int int32_2 = Convert.ToInt32(this.Request.QueryString["i_AssociatedIdNew"].ToString());
          DataTable dataTable = new AssociatedQueriesBL().AssociatedRead(int32_1);
          if (dataTable == null || dataTable.Rows.Count == 0)
          {
            Message.SetMessage(this.lblMessageChild, new HandledException(1, "El usuario no se pudo crear, consultar con el Administrador del sistema."));
            this.txtCorporateName.Text = "";
            this.txtUserName.Text = "";
            this.txtPassword.Text = "";
          }
          else
          {
            this.pobjexhibitionassociated = new ExhibitionAssociated();
            SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
            DataRow row = dataTable.Rows[0];
            this.pobjexhibitionassociated.i_PersonTypeId = Convert.ToInt32(row["i_PersonTypeId"]);
            this.pobjexhibitionassociated.v_ReasonSocial = this.txtCorporateName.Text + " - " + this.txtSede.Text;
            this.pobjexhibitionassociated.v_UserName = this.txtUserName.Text;
            this.pobjexhibitionassociated.v_Password = Cryptography.GetHashMD5(this.txtPassword.Text.TrimEnd());
            this.pobjexhibitionassociated.i_DocumentTypeId = Convert.ToInt32(row["i_DocumentTypeId"]);
            this.pobjexhibitionassociated.v_DocumentNumber = row["v_DocumentNumber"].ToString();
            this.pobjexhibitionassociated.v_Address = row["v_Address"].ToString();
            this.pobjexhibitionassociated.v_Telephone = row["v_Telephone"].ToString();
            this.pobjexhibitionassociated.v_Fax = row["v_Fax"].ToString();
            int int32_3 = Convert.ToInt32(row["i_IsAssociatedAAP"].ToString());
            this.pobjexhibitionassociated.v_RepresentativeName = row["v_RepresentativeName"].ToString();
            this.pobjexhibitionassociated.i_RepresentativeDocumentTypeId = Convert.ToInt32(row["i_RepresentativeDocumentTypeId"]);
            this.pobjexhibitionassociated.v_RepresentativeDocumentNumber = row["v_RepresentativeDocumentNumber"].ToString();
            this.pobjexhibitionassociated.v_ElectronicItemNumber = row["v_ElectronicItemNumber"].ToString();
            this.pobjexhibitionassociated.i_RegistryZoneId = Convert.ToInt32(row["i_RegistryZoneId"].ToString());
            string v_Acronym = row["v_Acronym"].ToString();
            this.pobjexhibitionassociated.v_Charge = row["v_Charge"].ToString();
            this.pobjexhibitionassociated.v_Email = row["v_Email"].ToString();
            this.pobjexhibitionassociated.v_WebSite = row["v_WebSite"].ToString();
            int i_AssociatedFatherId = int32_2;
            int i_CompanyId = 2;
            int i_Status = 1;
            int iSystemUserId = systemUser.i_SystemUserId;
            DateTime now = DateTime.Now;
            int i_RoleConfigId = this.RoleConfiguration(Convert.ToInt32(this.ViewState["i_plateTypeId"].ToString()));
            int i_LocationId = 14;
            int i_Result;
            if (this.ViewState["i_CustomerTypeId"] != null)
            {
              string i_CustomerTypeId = this.ViewState["i_CustomerTypeId"].ToString();
              i_Result = new AssociatedManagementBL().AssociatedRolUpdate(Convert.ToInt32(this.ViewState["i_systemUserId"]), i_CustomerTypeId, i_RoleConfigId);
            }
            else
            {
              string i_CustomerTypeId = this.ViewState["i_plateTypeId"].ToString();
              i_Result = new AssociatedManagementBL().AssociatedCreate(this.pobjexhibitionassociated, int32_3, v_Acronym, i_AssociatedFatherId, i_CompanyId, i_Status, iSystemUserId, now, i_RoleConfigId, i_LocationId, i_CustomerTypeId);
            }
            if (i_Result == 0)
            {
              this.txtUserName.Text = "";
              throw new HandledException(1, "El nombre de usuario ya existe </br> Intente con otro nombre de usuario diferente");
            }
            string v_Action = "creado";
            this.ValidTransaction(i_Result, v_Action);
          }
        }
        else
        {
          if (this.Request.QueryString["i_AssociatedId"] == null || this.Request.QueryString["i_SystemUserId"] == null)
            return;
          int int32 = Convert.ToInt32(this.Request.QueryString["i_SystemUserId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
          SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
          string text = this.txtSede.Text;
          new AssociatedQueriesBL().AssociatedRead(int32);
          this.ValidTransaction(new AssociatedManagementBL().AssociatedModifiedPass(int32, Cryptography.GetHashMD5(this.txtPassword.Text.TrimEnd()), systemUser.i_SystemUserId, DateTime.Now, text), "modificado");
        }
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
      string pstrEmailFrom = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.ConfigurationExhibition.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      }).Rows[0]["v_Value"].ToString();
      string pstrEmailSubject = "Creación Usuario";
      string pstrEmailBody = "Se creo el usuario satisfactoriamente. <br> Usuario : " + this.txtUserName.Text;
      List<string> stringList = new List<string>();
      List<string> pstrEmailCC = new List<string>();
      switch (this.ViewState["i_plateTypeId"].ToString())
      {
        case "7":
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
        Message.SetMessage(this.lblMessageChild, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageChild, new HandledException(-100, ex));
      }
    }

    protected void wibReturn_Click(object sender, EventArgs e)
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void wibDelete_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.Request.QueryString["i_SystemUserId"] == null)
          return;
        int int32 = Convert.ToInt32(this.Request.QueryString["i_SystemUserId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        int i_Status = -1;
        string v_Action = "eliminado";
        if (new AssociatedManagementBL().AssociatedValidationPlate(int32).Rows.Count == 0)
          this.ChangesState(int32, i_Status, v_Action);
        else
          Message.SetMessage(this.lblMessageChild, new HandledException(1, "Este usuario no puede ser eliminado porque tiene placas asignadas"));
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
        Message.SetMessage(this.lblMessageChild, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageChild, new HandledException(-100, ex));
      }
    }

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      this.Label2.Visible = false;
      this.txtPassword.Visible = false;
      this.ViewState["SupraRotateHijo"] = (object) "YES";
    }

    protected void btnReturnPopupConfirmation2_Click(object sender, EventArgs e)
    {
      try
      {
        this.ViewState["SupraRotateHijo"] = (object) null;
        throw new HandledException(1, "Debe crear un alias diferente");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void Initialize()
    {
      try
      {
        this.ViewState["i_plateTypeId"] = (object) this.Request.QueryString["i_PlateTypeId"].ToString();
        this.ViewState["SupraRotateHijo"] = (object) null;
        if (this.Request.QueryString["i_AssociatedIdNew"] != null && this.Request.QueryString["i_SystemUserIdNew"] != null)
        {
          this.wibSave.Visible = true;
          this.wibReturn.Visible = true;
        }
        else
        {
          this.wibSave.Visible = true;
          this.wibEnable.Visible = true;
          this.wibDelete.Visible = true;
          this.wibDisable.Visible = true;
          this.wibReturn.Visible = true;
        }
        this.LoadDataAssociated();
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
        if (this.Request.QueryString["i_SystemUserId"] != null)
        {
          DataTable dtresult = new AssociatedQueriesBL().AssociatedRead(Convert.ToInt32(this.Request.QueryString["i_SystemUserId"].ToString()));
          if (dtresult == null || dtresult.Rows.Count == 0)
            return;
          this.SetAssociated(dtresult);
        }
        else
        {
          int int32 = Convert.ToInt32(this.Request.QueryString["i_SystemUserIdNew"].ToString());
          DataTable dtresult = new AssociatedQueriesBL().AssociatedRead(int32);
          if (dtresult == null || dtresult.Rows.Count == 0)
            return;
          this.SetAssociated2(dtresult, int32);
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
        string[] source = row["v_ReasonSocial"].ToString().Split('-');
        string str1 = source[0].ToString();
        string str2 = "";
        if (((IEnumerable<string>) source).Count<string>() == 2)
          str2 = !string.IsNullOrEmpty(source[1].ToString()) ? source[1].ToString() : "";
        string str3 = row["v_UserName"].ToString();
        string str4 = row["v_DocumentNumber"].ToString();
        this.txtCorporateName.Text = str1 + (!string.IsNullOrEmpty(str2) ? " - " + str2 : "");
        this.txtUserName.Text = str3;
        this.txtRuc.Text = str4;
        this.txtSede.Text = str2;
        this.txtCorporateName.Enabled = false;
        this.txtRuc.Enabled = false;
        this.txtUserName.Enabled = false;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SetAssociated2(DataTable dtresult, int i_SystemUserId)
    {
      try
      {
        string str1 = new AssociatedQueriesBL().UserChildCorrelative(i_SystemUserId);
        DataRow row = dtresult.Rows[0];
        string str2 = row["v_ReasonSocial"].ToString();
        string str3 = row["v_DocumentNumber"].ToString();
        row["v_UserName"].ToString();
        this.txtCorporateName.Text = str2;
        this.txtRuc.Text = str3;
        this.txtCorporateName.Enabled = false;
        this.txtRuc.Enabled = false;
        this.txtUserName.Enabled = false;
        this.txtUserName.Text = str1;
        this.txtPassword.Text = string.Empty;
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
        this.v_Option = "04";
        DataTable dataTable = this.pbojAssociatedQueriesBL.ExhibitionAssociatedVerifyAlias(this.txtUserName.Text, this.v_Option, this.ViewState["i_plateTypeId"].ToString());
        if (dataTable.Rows.Count <= 0)
          return;
        if (dataTable.Columns.Count == 1)
        {
          this.txtUserName.Text = string.Empty;
          this.txtUserName.Focus();
          this.ViewState["Popup"] = (object) "Yes";
          throw new HandledException(1, "El nombre de usuario ya existe </br> Intente con otro nombre de usuario diferente");
        }
        this.txtUserName.Text = string.Empty;
        this.txtUserName.Focus();
        this.ViewState["Popup"] = (object) "Yes";
        throw new HandledException(1, "El nombre de usuario ya existe </br> Intente con otro nombre de usuario diferente");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private int RoleConfiguration(int i_PlateTypeId)
    {
      try
      {
        this.v_Option = "01";
        if (this.ViewState["SupraRotateHijo"] != null)
          this.v_Option = "02";
        return Convert.ToInt32(new AssociatedManagementBL().SpecialPlateRoleChildConfigGet(i_PlateTypeId, this.v_Option).Rows[0]["i_RoleConfigId"].ToString());
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
          throw new HandledException(2, "El asociado fue " + v_Action + " correctamente");
        throw new HandledException(1, "Se encontró un problema en la actualización de los datos");
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
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - ChangesAssociatedChild.aspx");
        this.pobjexhibitionassociated = new ExhibitionAssociated();
        this.pobjexhibitionassociated.i_SystemUserId = i_SystemUserId;
        int iSystemUserId = systemUser.i_SystemUserId;
        DateTime now = DateTime.Now;
        this.ValidTransaction(new AssociatedManagementBL().AssociatedDDE(this.pobjexhibitionassociated, i_Status, iSystemUserId, now), v_Action);
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
      string script = string.Format("OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }
  }
}
