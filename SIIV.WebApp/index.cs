// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.index
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.Exhibition.BL;
using SIIV.SystemParameter.BL;
using SIIV.SystemUser.BL;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp
{
  public class index : Page
  {
    protected HtmlForm form1;
    protected UpdatePanel UpdatePanel1;
    protected Label Label1;
    protected Label Label2;
    protected TextBox txtAlias;
    protected Label Label3;
    protected TextBox txtPassword;
    protected Button wibLogin;
    protected HyperLink HyperLink1;
    protected HyperLink HyperLink2;
    protected Label lblMsg;
    protected HyperLink HyperLink100;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.OpenPopUpValidation();
    }

    private void OpenPopUpValidation()
    {
      try
      {
        DataTable dataTable1 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) "1300",
          (object) "1",
          (object) "1",
          (object) "1"
        });
        DateTime dateTime1 = DateTime.Parse(dataTable1.Rows[0]["v_StartDate"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        DateTime dateTime2 = DateTime.Parse(dataTable1.Rows[0]["v_FinishDate"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        if (!(dateTime1 <= DateTime.Today) || !(DateTime.Today <= dateTime2))
          return;
        DataTable dataTable2 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) "1300",
          (object) "2",
          (object) "1",
          (object) "1"
        });
        if (dataTable2.Rows.Count > 0)
        {
          string empty = dataTable2.Rows[0]["v_Value"].ToString();
          if (string.IsNullOrEmpty(empty))
            empty = string.Empty;
          this.Session["LinkWhatsUp"] = (object) empty;
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", "OpenModal();", true);
        }
      }
      catch (Exception ex)
      {
        this.lblMsg.Text = ex.Message;
      }
    }

    protected void wibLogin_Click(object sender, EventArgs e)
    {
      string pstrAlias = this.txtAlias.Text.Trim();
      string pstrPassword = this.txtPassword.Text.Trim();
      string pstrMensaje = "";
      SIIV.BE.SystemUser systemUser = new SIIV.BE.SystemUser();
      try
      {
        if (!(pstrAlias != "") || !(pstrPassword != ""))
          throw new DataException(Constants.SYSTEMUSER_NoData);
        SystemUserQueriesBL systemUserQueriesBl = new SystemUserQueriesBL();
        int int16 = (int) Convert.ToInt16(ConfigurationManager.AppSettings["ApplicationId"], (IFormatProvider) CultureInfo.CurrentCulture);
        DataTable dataTable1 = systemUserQueriesBl.SpecialUserValidate(pstrAlias, pstrPassword);
        string userHostAddress = this.Request.UserHostAddress;
        SIIV.BE.SystemUser authentication = systemUserQueriesBl.GetAuthentication(int16, pstrAlias, pstrPassword, out pstrMensaje, userHostAddress);
        if (authentication == null)
          throw new DataException(pstrMensaje);
        this.Session.Clear();
        this.Session.Add("SystemUser", (object) authentication);
        this.Session.Add("ApplicationId", (object) int16);
        if (dataTable1.Rows.Count > 0)
          this.Session["i_CustomerTypeId"] = (object) dataTable1.Rows[0]["i_CustomerTypeId"].ToString();
        if (this.Session["dtExhibitionUser1"] == null)
        {
          DataTable dataTable2 = new DataTable();
          DataTable dataTable3 = new AssociatedQueriesBL().ExhibitionAssociatedAgreement(authentication.i_SystemUserId);
          if (dataTable3.Rows.Count > 0)
            this.Session["dtExhibitionUser1"] = (object) dataTable3;
          else
            this.Session["dtExhibitionUser1"] = (object) null;
        }
        this.Response.Redirect(ConfigurationManager.AppSettings["StartPage"].ToString((IFormatProvider) CultureInfo.CurrentCulture));
      }
      catch (Exception ex)
      {
        this.lblMsg.Text = ex.Message;
      }
    }
  }
}
