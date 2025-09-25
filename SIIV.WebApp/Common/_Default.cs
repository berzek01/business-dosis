// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Common._Default
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using SIIV.SystemParameter.BL;
using SIIV.WebApp.ig_res.CustomControls;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Common
{
  public class _Default : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected Button btnJavaScriptResponse;
    protected HiddenField hdnCustomerTypeId;
    protected HiddenField hdnRenovationDate;
    protected Button btnOpenSucesfull;
    protected Panel PanLoad;
    protected ucProgress ucProgress1;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!this.Page.IsPostBack)
        return;
      this.Session["Indica"] = (object) null;
      this.btnJavaScriptResponse_Click((object) null, (EventArgs) null);
    }

    protected void btnJavaScriptResponse_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.Session["Indica"] != null)
          return;
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseControlListDetail.aspx");
        if (this.Session["i_CustomerTypeId"] != null)
        {
          this.CreatePopUpServer2("Cuenta Bloqueada", "../Exhibicion/Operation/AccountStatement.aspx?i_CustomerTypeId=" + this.Session["i_CustomerTypeId"].ToString(), "575px", "320px");
        }
        else
        {
          if (systemUser.i_AssociatedId > 0)
          {
            DataTable dataTable1 = new DataTable();
            AcquisitionQueriesBL acquisitionQueriesBl = new AcquisitionQueriesBL();
            string i_CustomerTypeId = systemUser.i_CustomerTypeId;
            DataTable dataTable2 = acquisitionQueriesBl.ExhibitionPlatesRenewalComunication(i_CustomerTypeId);
            if (i_CustomerTypeId == "7" || i_CustomerTypeId == "11" || i_CustomerTypeId == "6" || i_CustomerTypeId == "7|11")
            {
              switch (i_CustomerTypeId)
              {
                case "7":
                  i_CustomerTypeId = "7";
                  break;
                case "6":
                  i_CustomerTypeId = "6";
                  break;
                case "11":
                  i_CustomerTypeId = "11";
                  break;
                case "7|11":
                  i_CustomerTypeId = "7|11";
                  break;
              }
              this.hdnCustomerTypeId.Value = i_CustomerTypeId;
              if (dataTable2.Rows.Count > 0)
              {
                Convert.ToDateTime(dataTable2.Rows[0]["FecIni"].ToString());
                Convert.ToDateTime(dataTable2.Rows[0]["FecFin"].ToString());
                string str = dataTable2.Rows[0]["FecRenovation"].ToString();
                ConfigurationManager.AppSettings["heightAnnounchmentPlatesEspecials"].ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim().Split('|');
                DataTable dataTable3 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
                {
                  (object) SystemParameterGroups.SpecialPlateAlertDateValidation.ToString((IFormatProvider) CultureInfo.CurrentCulture),
                  (object) "",
                  (object) "1",
                  (object) "1"
                });
                Convert.ToDateTime(dataTable3.Rows[0]["d_StartDate"].ToString());
                Convert.ToDateTime(dataTable3.Rows[0]["d_FinishDate"].ToString());
                if (i_CustomerTypeId == "7")
                  this.CreatePopUpServer("", "../Exhibicion/Operation/GeneralStatement.aspx?i_CustomerTypeId=" + i_CustomerTypeId + "&i_param=1&fecRenovation=" + str, "582px", "392px");
                else
                  this.CreatePopUpServer("", "../Exhibicion/Operation/GeneralStatement4.aspx?i_CustomerTypeId=" + i_CustomerTypeId + "&fecRenovation=" + str + "&i_param=1", "455px", "560px");
              }
            }
          }
          else if (ConfigurationManager.AppSettings["ExhibitionPlate_UserWindow"] != null)
          {
            if (systemUser.i_StatusChange > 0)
            {
              if (systemUser.i_RoleConfigId == 48 || systemUser.i_RoleConfigId == 54)
                this.CreatePopUpServer5("Cambio de Contraseña", ConfigurationManager.AppSettings["ExhibitionPlate_UserWindow"].ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim(), "690px", "340px");
              else if (systemUser.i_RoleConfigId == 45 || systemUser.i_RoleConfigId == 51)
                this.CreatePopUpServer("Cambio de Contraseña", ConfigurationManager.AppSettings["ExhibitionPlate_UserWindow"].ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim(), "690px", "340px");
              else
                this.CreatePopUpServer("Cambio de Contraseña", ConfigurationManager.AppSettings["ExhibitionPlate_UserWindow"].ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim(), "690px", "340px");
            }
            else if (systemUser.i_RoleConfigId == 48 || systemUser.i_RoleConfigId == 54)
              this.CreatePopUpServerimg2("", "../web/images/popup_niubiz.jpg", "640px", "460px");
            else if (systemUser.i_RoleConfigId != 45 && systemUser.i_RoleConfigId != 51)
              this.CreatePopUpServerimg("", "../web/images/popup_niubiz.jpg", "640px", "460px");
          }
          this.Session["Indica"] = (object) 1;
        }
      }
      catch (HandledException ex)
      {
        this.Response.Redirect("~/index.aspx");
      }
      catch (Exception ex)
      {
        this.Response.Redirect("~/index.aspx");
      }
    }

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp2('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) (pstrWidth + "px"), (object) (pstrHeight + "px"));
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void CreatePopUpServer1(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void CreatePopUpServer2(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp3('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void CreatePopUpServer4(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp4('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void CreatePopUpServerimg(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUpimg('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void CreatePopUpServerimg3(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUpimg3('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void CreatePopUpServerimg2(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUpimg2('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void CreatePopUpServer5(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp5('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void btnOpenSucesfull_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("index.aspx");
    }
  }
}
