// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Requirement.BeginRequirement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Requirement
{
  public class BeginRequirement : Page
  {
    private SystemUser objUserBE;
    private string url;
    protected HtmlTableRow tr_RequirementPlateSpace;
    protected HtmlTableRow tr_RequirementPlate;
    protected Label Label6;
    protected Image Image6;
    protected Button btnInmatriculacion;
    protected HtmlTableRow tr_RequirementPlaseMassiveSpace;
    protected HtmlTableRow tr_RequirementPlaseMassive;
    protected Label Label2;
    protected Image Image2;
    protected Button btnSolicitudMasiva;
    protected HtmlTableRow tr_Duplicate3raPlateSpace;
    protected HtmlTableRow tr_Duplicate3raPlate;
    protected Label Label7;
    protected Image Image10;
    protected Button btnTercera;
    protected HtmlTableRow tr_DUplicatePlateSpace;
    protected HtmlTableRow tr_DUplicatePlate;
    protected Label Label8;
    protected Image Image15;
    protected Button btnDuplicado;
    protected HtmlTableRow tr_ChangeUsePlateNewSpace;
    protected HtmlTableRow tr_ChangeUsePlateNew;
    protected Label Label9;
    protected Image Image11;
    protected Button btnCanbioUso;
    protected HtmlTableRow tr_ChangeUseRectificationSpace;
    protected HtmlTableRow tr_ChangeUseRectification;
    protected Label Label1;
    protected Image Image1;
    protected Button btnCmabioUsoRectificacion;
    protected HtmlTableRow tr_ServicePremiumPlatingSpace;
    protected HtmlTableRow tr_ServicePremiumPlating;
    protected Label Label3;
    protected Image Image3;
    protected Button btnServicePremiumPlating;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.objUserBE = this.Session["SystemUser"] as SystemUser;
      bool flag = false;
      string str1 = Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture);
      string str2 = ConfigurationManager.AppSettings["RolesGestorAutomotor"].ToString();
      string str3 = ConfigurationManager.AppSettings["RoleGestor2doTramites"].ToString();
      string str4 = str2;
      char[] chArray = new char[1]{ '|' };
      foreach (object obj in str4.Split(chArray))
      {
        if (obj.ToString() == this.objUserBE.i_RoleConfigId.ToString())
          flag = true;
      }
      if (str1 == "r" && !flag)
      {
        this.Session["RequirementPlateType"] = (object) Convert.ToInt32((object) enmRequirementPlateType.Regular);
        this.Page.Title = "Inicio Solicitudes Regulares";
        this.RequirementHide(true, true, true, true, true, false, false);
        this.ShowChangeUseRectification();
      }
      else if (str1 == "p" & flag)
      {
        this.Session["RequirementPlateType"] = (object) Convert.ToInt32((object) enmRequirementPlateType.Premium);
        this.Page.Title = "Inicio Solicitudes Premium";
        if (this.objUserBE.i_RoleConfigId == Convert.ToInt32(str3))
          this.RequirementHide(true, true, true, true, false, false, false);
        else
          this.RequirementHide(true, true, false, false, false, false, false);
      }
      else if (str1 == "rc" && !flag)
      {
        this.Session["RequirementPlateType"] = (object) Convert.ToInt32((object) enmRequirementPlateType.Reclamo);
        this.Page.Title = "Inicio Solicitudes de Reclamo";
        this.RequirementHide(true, false, true, true, true, false, false);
      }
      else if (str1 == "nl" && !flag)
      {
        this.Session["RequirementPlateType"] = (object) Convert.ToInt32((object) enmRequirementPlateType.NotariaLiberada);
        this.Page.Title = "Inicio Solicitudes de Notarias";
        this.RequirementHide(true, true, false, false, false, false, false);
      }
      else if (str1 == "AAP" && !flag)
      {
        this.Session["RequirementPlateType"] = (object) Convert.ToInt32((object) enmRequirementPlateType.Reclamo);
        this.Page.Title = "Inicio Solicitudes de Reclamo";
        this.RequirementHide(true, false, true, true, true, false, false);
      }
      else
        FormsAuthentication.RedirectToLoginPage();
    }

    protected void btnInmatriculacion_Click(object sender, EventArgs e)
    {
      this.Session["ProcessId"] = (object) Convert.ToInt32((object) enmProccessType.Inmatriculacion);
      if (Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) == "AAP")
      {
        string str = Convert.ToString(this.Request.QueryString["idDoc"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.url = "RegisterClaimRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) + "&idDoc=" + str;
      }
      else
        this.url = !(Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) != "rc") ? "RegisterClaimRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) : "RegisterRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.Response.Redirect(this.url);
    }

    protected void btnSolicitudMasiva_Click(object sender, EventArgs e)
    {
      string str = Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture);
      if (str == "r")
        this.Session["RequirementPlateType"] = (object) Convert.ToInt32((object) enmRequirementPlateType.Masiva);
      else if (str == "p")
        this.Session["RequirementPlateType"] = (object) Convert.ToInt32((object) enmRequirementPlateType.Premium);
      else if (str == "p")
        this.Session["RequirementPlateType"] = (object) Convert.ToInt32((object) enmRequirementPlateType.NotariaLiberada);
      this.url = "RegisterMassiveRequirement.aspx";
      this.Response.Redirect(this.url);
    }

    protected void btnTercera_Click(object sender, EventArgs e)
    {
      this.Session["ProcessId"] = (object) Convert.ToInt32((object) enmProccessType.Duplicado3rd);
      if (Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) == "AAP")
      {
        string str = Convert.ToString(this.Request.QueryString["idDoc"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.url = "RegisterClaimRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) + "&idDoc=" + str;
      }
      else
        this.url = !(Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) != "rc") ? "RegisterClaimRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) : "RegisterRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.Response.Redirect(this.url);
    }

    protected void btnDuplicado_Click(object sender, EventArgs e)
    {
      this.Session["ProcessId"] = (object) Convert.ToInt32((object) enmProccessType.Duplicado);
      if (Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) == "AAP")
      {
        string str = Convert.ToString(this.Request.QueryString["idDoc"], (IFormatProvider) CultureInfo.CurrentCulture);
        this.url = "RegisterClaimRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) + "&idDoc=" + str;
      }
      else
        this.url = !(Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) != "rc") ? "RegisterClaimRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) : "RegisterRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.Response.Redirect(this.url);
    }

    protected void btnCanbioUso_Click(object sender, EventArgs e)
    {
      this.Session["ProcessId"] = (object) Convert.ToInt32((object) enmProccessType.CambioUso);
      this.url = !(Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) != "rc") ? "RegisterClaimRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture) : "RegisterRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.Response.Redirect(this.url);
    }

    protected void btnCmabioUsoRectificacion_Click(object sender, EventArgs e)
    {
      this.Session["ProcessId"] = (object) Convert.ToInt32((object) enmProccessType.CambioUsoRectificacion);
      this.url = "RegisterRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.Response.Redirect(this.url);
    }

    protected void btnServicePremiumPlating_Click(object sender, EventArgs e)
    {
      this.Session["ProcessId"] = (object) Convert.ToInt32((object) enmProccessType.ServicioPremiumReplacamiento);
      this.url = "RegisterRequirement.aspx?t=" + Convert.ToString(this.Request.QueryString["t"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.Response.Redirect(this.url);
    }

    public DataTable getRequirementStages(int intProcessId)
    {
      return new RequirementQueriesBL().GetRequiredStageByIdProcess(intProcessId);
    }

    protected void RequirementHide(
      bool trRequirementPlate,
      bool trRequirementPlaseMassive,
      bool trDuplicate3raPlate,
      bool trDUplicatePlate,
      bool trChangeUsePlateNew,
      bool trChangeUseRectification,
      bool trServicePremiumPlating)
    {
      this.tr_RequirementPlate.Visible = this.tr_RequirementPlateSpace.Visible = trRequirementPlate;
      this.tr_RequirementPlaseMassive.Visible = this.tr_RequirementPlaseMassiveSpace.Visible = trRequirementPlaseMassive;
      this.tr_Duplicate3raPlate.Visible = this.tr_Duplicate3raPlateSpace.Visible = trDuplicate3raPlate;
      this.tr_DUplicatePlate.Visible = this.tr_DUplicatePlateSpace.Visible = trDUplicatePlate;
      this.tr_ChangeUsePlateNew.Visible = this.tr_ChangeUsePlateNewSpace.Visible = trChangeUsePlateNew;
      this.tr_ChangeUseRectification.Visible = this.tr_ChangeUseRectificationSpace.Visible = trChangeUseRectification;
      this.tr_ServicePremiumPlating.Visible = this.tr_ServicePremiumPlatingSpace.Visible = trServicePremiumPlating;
    }

    protected void RequirementHide(string objDiv)
    {
      string script = "<script type=text/javascript>var obj = document.getElementById('" + objDiv + "');obj.style.display = \"none\";</script>";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "JSCR", script, false);
    }

    public void ShowChangeUseRectification()
    {
      this.objUserBE = this.Session["SystemUser"] as SystemUser;
      try
      {
        string userExtendedAction = new RequirementQueriesBL().GetSystemUserExtendedAction(this.objUserBE.i_SystemUserId, 1);
        if (!(userExtendedAction != ""))
          return;
        if (Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("4", StringComparison.CurrentCulture))) == "4")
          this.tr_ChangeUseRectification.Visible = this.tr_ChangeUseRectificationSpace.Visible = true;
      }
      catch
      {
      }
    }
  }
}
