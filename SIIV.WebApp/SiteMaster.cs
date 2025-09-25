// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.SiteMaster
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using mtweb;
using SIIV.Common.Resource;
using SIIV.SystemParameter.BL;
using SIIV.SystemUser.BL;
using SIIV.WebApp.ig_res.CustomControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp
{
  public class SiteMaster : MasterPage
  {
    protected HtmlHead Head1;
    protected ContentPlaceHolder HeadContent;
    protected HtmlGenericControl body;
    protected HtmlForm form1;
    protected ToolkitScriptManager ScriptManager1;
    protected ModalPopupExtender ModalProgreso;
    protected UpdatePanel UpdatePanel1;
    protected Timer asptimer;
    protected Label lblTimer;
    protected Label lblTitlePage;
    protected SiteMapPath SiteMapPath1;
    protected Label lblUserName;
    protected Label lblRoleName;
    protected LinkButton lbCloseSession;
    protected Image imgCloseSession;
    protected HtmlTableRow trExhibicion;
    protected LinkButton LinkExhibicionConvenio;
    protected HyperLink LinkExhibicionUsuario;
    protected HyperLink LinkExhibicionNormaLegal;
    protected HyperLink LinkMTC;
    protected HtmlTableRow trRotativa;
    protected LinkButton LinkRotativaConvenio;
    protected HyperLink LinkRotativaUsuario;
    protected HyperLink LinkRotativaNormaLegal;
    protected LinkButton LinkRotativaConvenioEspecifico;
    protected AMenu Menu1;
    protected ContentPlaceHolder MainContent;
    protected Panel PanLoad;
    protected ucProgress ucProgress1;
    protected Label lblMsgError;

    protected void asptimer_tick(object sender, EventArgs e)
    {
      try
      {
        int num1 = 0;
        int num2 = 0;
        int num3 = (int) Convert.ToInt16(ConfigurationManager.AppSettings["SessionTimeoutMin"]) * 60;
        if (this.Session["SessionTimeoutMin"] == null)
        {
          int num4;
          if (num3 >= 60)
          {
            num1 = (int) Convert.ToInt16((Decimal) (num3 / 60));
            num4 = num3 - num1 * 60;
          }
          else
            num4 = num3;
          this.lblTimer.Text = num1.ToString("00", (IFormatProvider) CultureInfo.CurrentCulture) + ":" + num4.ToString("00", (IFormatProvider) CultureInfo.CurrentCulture);
          this.Session.Remove("SessionTimeoutMin");
          this.Session.Add("SessionTimeoutMin", (object) num3);
        }
        else
        {
          num2 = (int) Convert.ToInt16(this.Session["SessionTimeoutMin"], (IFormatProvider) CultureInfo.CurrentCulture) - 1;
          int num5;
          int num6;
          if (num2 >= 60)
          {
            num5 = (int) Convert.ToInt16((Decimal) (num2 / 60));
            num6 = num2 - num5 * 60;
          }
          else
          {
            num5 = 0;
            num6 = num2;
          }
          this.lblTimer.Text = num5.ToString("00", (IFormatProvider) CultureInfo.CurrentCulture) + ":" + num6.ToString("00", (IFormatProvider) CultureInfo.CurrentCulture);
          this.Session.Remove("SessionTimeoutMin");
          this.Session.Add("SessionTimeoutMin", (object) num2);
        }
        if (num2 < 0)
          throw new DataException("Fin de duración de Sesión");
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.Message);
        this.asptimer.Enabled = false;
        SIIV.Common.Resource.Message.SetMessage(this.lblMsgError, new HandledException(6, ex));
        FormsAuthentication.RedirectToLoginPage();
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Page.RegisterRedirectOnSessionEndScript();
      int int16 = (int) Convert.ToInt16(ConfigurationManager.AppSettings["ApplicationId"]);
      SIIV.BE.SystemUser systemUser;
      if (this.Session["SystemUser"] == null)
      {
        if (this.ViewState["SystemUser"] == null)
        {
          SIIV.Common.Resource.Message.SetMessage(this.lblMsgError, new HandledException(3, "Session SystemUser", "Error al cargar la session Systemuser el el Site.Master"));
          FormsAuthentication.RedirectToLoginPage();
          return;
        }
        this.Session["SystemUser"] = this.ViewState["SystemUser"];
        systemUser = (SIIV.BE.SystemUser) this.Session["SystemUser"];
        if (this.ViewState["SystemUser"] == null)
          this.ViewState["SystemUser"] = (object) systemUser;
      }
      else
      {
        systemUser = (SIIV.BE.SystemUser) this.Session["SystemUser"];
        if (this.ViewState["SystemUser"] == null)
          this.ViewState["SystemUser"] = (object) systemUser;
      }
      this.lblTitlePage.Text = HttpUtility.HtmlEncode(this.Page.Title);
      this.lblUserName.Text = systemUser.i_IsAssociatedAAP != -1 ? systemUser.v_FirstName.Trim() + "[" + systemUser.v_Alias.Trim() + "]" : systemUser.v_FirstName.Trim() + " " + systemUser.v_LastName.Trim();
      this.lblRoleName.Text = systemUser.v_RoleName.Trim();
      DataTable authorization;
      if (this.Session["dtUserRole"] != null)
      {
        authorization = (DataTable) this.Session["dtUserRole"];
        this.Session.Remove("dtUserRole");
        this.Session.Add("dtUserRole", (object) authorization);
      }
      else
      {
        authorization = new SystemUserQueriesBL().GetAuthorization(int16, systemUser.i_SystemUserId);
        this.Session.Add("dtUserRole", (object) authorization);
      }
      this.BuildMenu(authorization);
      if (this.Page.IsPostBack)
        return;
      try
      {
        string executionFilePath = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath;
        if (ConfigurationManager.AppSettings["StartPage"].ToString() != executionFilePath && new Authorization().ValidatePageAccess(authorization, executionFilePath) != 1)
        {
          SIIV.Common.Resource.Message.SetMessage(this.lblMsgError, new HandledException(6, SIIV.SystemParameter.BL.Constants.ERROR_NoAccess + " " + executionFilePath));
          FormsAuthentication.RedirectToLoginPage();
        }
        else
        {
          if (this.Session["dtExhibitionUser1"] != null)
          {
            this.Session.Add("dtExhibitionUser", (object) (DataTable) this.Session["dtExhibitionUser1"]);
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = (DataTable) this.Session["dtExhibitionUser1"];
            string str = dataTable2.Rows[0]["i_AssociatedFatherId"].ToString();
            this.ViewState["i_CustomerTypeId"] = (object) dataTable2.Rows[0]["i_CustomerTypeId"].ToString();
            if (str == "0")
            {
              if (this.ViewState["i_CustomerTypeId"].ToString() == "7")
                this.LinkExhibicionConvenio.Visible = true;
              else if (this.ViewState["i_CustomerTypeId"].ToString() == "11")
              {
                this.LinkRotativaConvenio.Visible = true;
              }
              else
              {
                this.LinkExhibicionConvenio.Visible = true;
                this.LinkRotativaConvenio.Visible = true;
              }
            }
          }
          int iAssociatedId = systemUser.i_AssociatedId;
          this.trExhibicion.Visible = false;
          this.trRotativa.Visible = false;
          if (iAssociatedId == 0)
          {
            this.LinkExhibicionNormaLegal.Visible = false;
            this.LinkRotativaNormaLegal.Visible = false;
          }
          else if (this.ViewState["i_CustomerTypeId"].ToString() == "7")
          {
            this.trExhibicion.Visible = true;
            this.LinkExhibicionUsuario.Visible = true;
            this.LinkExhibicionNormaLegal.Visible = true;
            this.LinkMTC.Visible = true;
          }
          else if (this.ViewState["i_CustomerTypeId"].ToString() == "11")
          {
            this.trRotativa.Visible = true;
            this.LinkRotativaUsuario.Visible = true;
            this.LinkRotativaNormaLegal.Visible = true;
            this.LinkRotativaConvenioEspecifico.Visible = true;
          }
          else if (this.ViewState["i_CustomerTypeId"].ToString() == "7|11")
          {
            this.trExhibicion.Visible = true;
            this.trRotativa.Visible = true;
            this.LinkExhibicionUsuario.Visible = true;
            this.LinkExhibicionNormaLegal.Visible = true;
            this.LinkMTC.Visible = true;
            this.LinkRotativaUsuario.Visible = true;
            this.LinkRotativaNormaLegal.Visible = true;
            this.LinkRotativaConvenioEspecifico.Visible = true;
          }
          this.PanLoad.Visible = true;
          this.asptimer.Enabled = false;
        }
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMsgError, new HandledException(-100, ex));
        FormsAuthentication.RedirectToLoginPage();
      }
      finally
      {
        GC.Collect();
      }
    }

    private void Page_Error(object sender, EventArgs e)
    {
      HandledException handledException = new HandledException(9, this.Server.GetLastError());
      this.Server.ClearError();
    }

    protected void lbCloseSession_Click(object sender, EventArgs e)
    {
      GC.Collect();
      FormsAuthentication.SignOut();
      this.Session.Abandon();
      FormsAuthentication.RedirectToLoginPage();
    }

    protected void LinkExhibicionConvenio_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.Session["dtExhibitionUser"] == null)
          return;
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = (DataTable) this.Session["dtExhibitionUser"];
        string[] strArray1 = dataTable2.Rows[0]["v_Document1"].ToString().Split('0');
        DataTable dataTable3 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.PersonDocumentType.ToString(),
          (object) "",
          (object) "1",
          (object) "1"
        });
        string str1 = "";
        List<string> stringList = new List<string>();
        foreach (string str2 in strArray1)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable3.Rows)
          {
            if ((str2[0].ToString() != "" || str2[0].ToString() != string.Empty) && str2[0].ToString() == row["i_ParameterId"].ToString())
              str1 = str1 + row["v_Description"].ToString() + "/";
          }
        }
        if (!dataTable2.Columns.Contains("v_Message"))
          dataTable2.Columns.Add("v_Message", Type.GetType("System.String"));
        if (!dataTable2.Columns.Contains("v_Firma"))
          dataTable2.Columns.Add("v_Firma", Type.GetType("System.String"));
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
        {
          string str3 = dataTable2.Rows[0]["v_RepresentativeName"].ToString();
          string str4 = dataTable2.Rows[0]["v_Charge"].ToString();
          string str5 = dataTable2.Rows[0]["v_RepresentativeDocumentNumber"].ToString();
          string str6 = dataTable2.Rows[0]["v_ReasonSocial"].ToString();
          string[] strArray2 = str3.Split('/');
          string[] strArray3 = str4.Split('/');
          string[] strArray4 = str5.Split('/');
          string[] strArray5 = str1.ToString().Split('/');
          StringBuilder stringBuilder1 = new StringBuilder();
          StringBuilder stringBuilder2 = new StringBuilder();
          if (strArray2.Length == 1)
          {
            stringBuilder1.AppendLine("debidamente representada por su " + strArray3[0] + ", el señor(a) " + strArray2[0] + ", identificados con " + strArray5[0] + " Nº " + strArray4[0]);
            stringBuilder2.AppendLine("……………………………………");
            stringBuilder2.AppendLine(strArray2[0]);
            stringBuilder2.AppendLine(strArray3[0]);
            stringBuilder2.AppendLine(str6);
          }
          if (strArray2.Length == 2)
          {
            if (strArray2[0] != "" && strArray2[1] == "")
            {
              stringBuilder1.AppendLine("debidamente representada por su " + strArray3[0] + ", el señor(a) " + strArray2[0] + ", identificados con " + strArray5[0] + " Nº " + strArray4[0]);
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[0]);
              stringBuilder2.AppendLine(strArray3[0]);
              stringBuilder2.AppendLine(str6);
            }
            else if (strArray2[0] != "" && strArray2[1] != "")
            {
              stringBuilder1.AppendLine("debidamente representada por su " + strArray3[0] + ", el señor(a) " + strArray2[0] + ", identificados con " + strArray5[0] + " Nº " + strArray4[0] + " y por su " + strArray3[1] + ", el señor(a) " + strArray2[1] + ", identificados con " + strArray5[1] + " Nº " + strArray4[1]);
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[0]);
              stringBuilder2.AppendLine(strArray3[0]);
              stringBuilder2.AppendLine(str6);
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[1]);
              stringBuilder2.AppendLine(strArray3[1]);
              stringBuilder2.AppendLine(str6);
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
              stringBuilder2.AppendLine(str6);
            }
            else if (strArray2[0] != "" && strArray2[1] != "" && strArray2[2] == "")
            {
              stringBuilder1.AppendLine("debidamente representada por su " + strArray3[0] + ", el señor(a) " + strArray2[0] + ", identificados con " + strArray5[0] + " Nº " + strArray4[0] + " y por su " + strArray3[1] + ", el señor(a) " + strArray2[1] + ", identificados con " + strArray5[1] + " Nº " + strArray4[1]);
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[0]);
              stringBuilder2.AppendLine(strArray3[0]);
              stringBuilder2.AppendLine(str6);
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[1]);
              stringBuilder2.AppendLine(strArray3[1]);
              stringBuilder2.AppendLine(str6);
            }
            else if (strArray2[0] != "" && strArray2[1] != "" && strArray2[2] != "")
            {
              stringBuilder1.AppendLine("debidamente representada por su " + strArray3[0] + ", el señor(a) " + strArray2[0] + ", identificados con " + strArray5[0] + " Nº " + strArray4[0] + " y por su " + strArray3[1] + ", el señor(a) " + strArray2[1] + ", identificados con " + strArray5[1] + " Nº " + strArray4[1] + " y por su " + strArray3[2] + ", el señor(a) " + strArray2[2] + ", identificados con " + strArray5[2] + " Nº " + strArray4[2]);
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[0]);
              stringBuilder2.AppendLine(strArray3[0]);
              stringBuilder2.AppendLine(str6);
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[1]);
              stringBuilder2.AppendLine(strArray3[1]);
              stringBuilder2.AppendLine(str6);
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[2]);
              stringBuilder2.AppendLine(strArray3[2]);
              stringBuilder2.AppendLine(str6);
            }
          }
          row["v_Message"] = (object) stringBuilder1.ToString();
          row["v_Firma"] = (object) stringBuilder2.ToString();
        }
        string str7 = dataTable2.Rows[0]["i_IsAssociatedAAP"].ToString();
        if (str7.Equals("1", StringComparison.CurrentCulture))
        {
          ReportDocument reportDocument = new ReportDocument();
          string filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportAssociatedAgreement.rpt";
          reportDocument.Load(filename);
          reportDocument.SetDataSource(dataTable2);
          reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "Asociado");
        }
        else if (str7.Equals("0", StringComparison.CurrentCulture))
        {
          ReportDocument reportDocument = new ReportDocument();
          string filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportNotAssociatedAgreement.rpt";
          reportDocument.Load(filename);
          reportDocument.SetDataSource(dataTable2);
          reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "NoAsociado");
        }
        else if (str7.Equals("2", StringComparison.CurrentCulture))
        {
          ReportDocument reportDocument = new ReportDocument();
          string filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportImporterAgreement.rpt";
          reportDocument.Load(filename);
          reportDocument.SetDataSource(dataTable2);
          reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "Importador");
        }
      }
      catch (Exception ex)
      {
        HandledException handledException = new HandledException(6, "error forzado: LinkExhibicion convenio", "se quiere ver con que frecuencia entra en este error");
        this.lblUserName.Text = ex.Message;
      }
    }

    protected void LinkRotativaConvenio_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.Session["dtExhibitionUser"] == null)
          return;
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = (DataTable) this.Session["dtExhibitionUser"];
        string[] strArray1 = dataTable2.Rows[0]["v_Document1"].ToString().Split('0');
        DataTable dataTable3 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.PersonDocumentType.ToString(),
          (object) "",
          (object) "1",
          (object) "1"
        });
        string str1 = "";
        List<string> stringList = new List<string>();
        foreach (string str2 in strArray1)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable3.Rows)
          {
            if ((str2[0].ToString() != "" || str2[0].ToString() != string.Empty) && str2[0].ToString() == row["i_ParameterId"].ToString())
              str1 = str1 + row["v_Description"].ToString() + "/";
          }
        }
        if (!dataTable2.Columns.Contains("v_Message"))
          dataTable2.Columns.Add("v_Message", Type.GetType("System.String"));
        if (!dataTable2.Columns.Contains("v_Firma"))
          dataTable2.Columns.Add("v_Firma", Type.GetType("System.String"));
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
        {
          string str3 = dataTable2.Rows[0]["v_RepresentativeName"].ToString();
          string str4 = dataTable2.Rows[0]["v_Charge"].ToString();
          string str5 = dataTable2.Rows[0]["v_RepresentativeDocumentNumber"].ToString();
          string str6 = dataTable2.Rows[0]["v_ReasonSocial"].ToString();
          string[] strArray2 = str3.Split('/');
          string[] strArray3 = str4.Split('/');
          string[] strArray4 = str5.Split('/');
          string[] strArray5 = str1.ToString().Split('/');
          StringBuilder stringBuilder1 = new StringBuilder();
          StringBuilder stringBuilder2 = new StringBuilder();
          if (strArray2.Length == 1)
          {
            stringBuilder1.AppendLine("debidamente representada por su " + strArray3[0] + ", el señor(a) " + strArray2[0] + ", identificados con " + strArray5[0] + " Nº " + strArray4[0]);
            stringBuilder2.AppendLine("……………………………………");
            stringBuilder2.AppendLine(strArray2[0]);
            stringBuilder2.AppendLine(strArray3[0]);
            stringBuilder2.AppendLine(str6);
          }
          if (strArray2.Length == 2)
          {
            if (strArray2[0] != "" && strArray2[1] == "")
            {
              stringBuilder1.AppendLine("debidamente representada por su " + strArray3[0] + ", el señor(a) " + strArray2[0] + ", identificados con " + strArray5[0] + " Nº " + strArray4[0]);
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[0]);
              stringBuilder2.AppendLine(strArray3[0]);
              stringBuilder2.AppendLine(str6);
            }
            else if (strArray2[0] != "" && strArray2[1] != "")
            {
              stringBuilder1.AppendLine("debidamente representada por su " + strArray3[0] + ", el señor(a) " + strArray2[0] + ", identificados con " + strArray5[0] + " Nº " + strArray4[0] + " y por su " + strArray3[1] + ", el señor(a) " + strArray2[1] + ", identificados con " + strArray5[1] + " Nº " + strArray4[1]);
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[0]);
              stringBuilder2.AppendLine(strArray3[0]);
              stringBuilder2.AppendLine(str6);
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[1]);
              stringBuilder2.AppendLine(strArray3[1]);
              stringBuilder2.AppendLine(str6);
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
              stringBuilder2.AppendLine(str6);
            }
            else if (strArray2[0] != "" && strArray2[1] != "" && strArray2[2] == "")
            {
              stringBuilder1.AppendLine("debidamente representada por su " + strArray3[0] + ", el señor(a) " + strArray2[0] + ", identificados con " + strArray5[0] + " Nº " + strArray4[0] + " y por su " + strArray3[1] + ", el señor(a) " + strArray2[1] + ", identificados con " + strArray5[1] + " Nº " + strArray4[1]);
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[0]);
              stringBuilder2.AppendLine(strArray3[0]);
              stringBuilder2.AppendLine(str6);
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[1]);
              stringBuilder2.AppendLine(strArray3[1]);
              stringBuilder2.AppendLine(str6);
            }
            else if (strArray2[0] != "" && strArray2[1] != "" && strArray2[2] != "")
            {
              stringBuilder1.AppendLine("debidamente representada por su " + strArray3[0] + ", el señor(a) " + strArray2[0] + ", identificados con " + strArray5[0] + " Nº " + strArray4[0] + " , por su " + strArray3[1] + ", el señor(a) " + strArray2[1] + ", identificados con " + strArray5[1] + " Nº " + strArray4[1] + " y por su " + strArray3[2] + ", el señor(a) " + strArray2[2] + ", identificados con " + strArray5[2] + " Nº " + strArray4[2]);
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[0]);
              stringBuilder2.AppendLine(strArray3[0]);
              stringBuilder2.AppendLine(str6);
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[1]);
              stringBuilder2.AppendLine(strArray3[1]);
              stringBuilder2.AppendLine(str6);
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("");
              stringBuilder2.AppendLine("……………………………………");
              stringBuilder2.AppendLine(strArray2[2]);
              stringBuilder2.AppendLine(strArray3[2]);
              stringBuilder2.AppendLine(str6);
            }
          }
          row["v_Message"] = (object) stringBuilder1.ToString();
          row["v_Firma"] = (object) stringBuilder2.ToString();
        }
        string str7 = dataTable2.Rows[0]["i_IsAssociatedAAP"].ToString();
        if (str7.Equals("1", StringComparison.CurrentCulture))
        {
          ReportDocument reportDocument = new ReportDocument();
          string filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportAssociatedAgreementRotate.rpt";
          reportDocument.Load(filename);
          reportDocument.SetDataSource(dataTable2);
          reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "Asociado");
        }
        else if (str7.Equals("0", StringComparison.CurrentCulture))
        {
          ReportDocument reportDocument = new ReportDocument();
          string filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportNotAssociatedAgreementRotate2.rpt";
          reportDocument.Load(filename);
          reportDocument.SetDataSource(dataTable2);
          reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "NoAsociado");
        }
        else if (str7.Equals("2", StringComparison.CurrentCulture))
        {
          ReportDocument reportDocument = new ReportDocument();
          string filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportImporterAgreementRotate.rpt";
          reportDocument.Load(filename);
          reportDocument.SetDataSource(dataTable2);
          reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "Importador");
        }
      }
      catch (Exception ex)
      {
        HandledException handledException = new HandledException(6, "error forzado: LinkRotativa convenio", "se quiere ver con que frecuencia entra en este error");
        this.lblUserName.Text = ex.Message;
      }
    }

    protected void LinkRotativaConvenioEspecifico_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.Session["dtExhibitionUser"] == null)
          return;
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = (DataTable) this.Session["dtExhibitionUser"];
        ReportDocument reportDocument = new ReportDocument();
        string filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportAssociatedAgreementRotateSpecific.rpt";
        reportDocument.Load(filename);
        reportDocument.SetDataSource(dataTable2);
        reportDocument.ExportToHttpResponse(ExportFormatType.WordForWindows, this.Response, true, "Asociado");
      }
      catch (Exception ex)
      {
        HandledException handledException = new HandledException(6, "error forzado: LinkRotativa convenio especifico", "se quiere ver con que frecuencia entra en este error");
        this.lblUserName.Text = ex.Message;
      }
    }

    private static AMenuSub GetMenuItemRecursive(MenuLink pmiItem, string pstrReferenceId)
    {
      if (!(((Control) pmiItem).ID.ToString((IFormatProvider) CultureInfo.CurrentCulture) == pstrReferenceId))
        return (AMenuSub) null;
      AMenuSub menuItemRecursive;
      if (pmiItem.Items.Count == 0)
      {
        AMenuSub amenuSub = new AMenuSub();
        ((Control) amenuSub).ID = ((Control) pmiItem).ID;
        menuItemRecursive = amenuSub;
        pmiItem.Items.Add((object) menuItemRecursive);
      }
      else
        menuItemRecursive = (AMenuSub) pmiItem.Items[0];
      return menuItemRecursive;
    }

    public void BuildMenu(DataTable dtOptions)
    {
      try
      {
        this.Menu1.Items.Clear();
        if (dtOptions == null || this.Menu1 == null)
          return;
        foreach (DataRow row in (InternalDataCollectionBase) dtOptions.Rows)
        {
          int int32 = Convert.ToInt32(row["i_ReferenceId"]);
          if (Convert.ToInt32(row["i_VisibleForm"]) == 1)
          {
            if (int32 == 0)
            {
              MenuLink menuLink1 = new MenuLink();
              ((Control) menuLink1).ID = row["i_SystemFormId"].ToString();
              menuLink1.IconImage = row["v_Image"].ToString();
              menuLink1.Text = row["v_Name"].ToString();
              menuLink1.CommandName = row["v_Path"].ToString();
              MenuLink menuLink2 = menuLink1;
              menuLink2.Click += new CommandEventHandler(this.OnMenuClick);
              this.Menu1.Items.Add((object) menuLink2);
            }
            else
            {
              foreach (MenuLink menuLink3 in this.Menu1.Items)
              {
                AMenuSub amenuSub1;
                if (menuLink3.Items.Count == 0)
                {
                  AMenuSub amenuSub2 = new AMenuSub();
                  ((Control) amenuSub2).ID = ((Control) menuLink3).ID;
                  amenuSub1 = amenuSub2;
                  menuLink3.Items.Add((object) amenuSub1);
                }
                else
                  amenuSub1 = (AMenuSub) menuLink3.Items[0];
                if (((Control) menuLink3).ID == int32.ToString())
                {
                  MenuLink menuLink4 = new MenuLink();
                  ((Control) menuLink4).ID = row["i_SystemFormId"].ToString();
                  menuLink4.IconImage = row["v_Image"].ToString();
                  menuLink4.Text = row["v_Name"].ToString();
                  menuLink4.CommandName = row["v_Path"].ToString();
                  MenuLink menuLink5 = menuLink4;
                  menuLink5.Click += new CommandEventHandler(this.OnMenuClick);
                  amenuSub1.Items.Add((object) menuLink5);
                  break;
                }
                if (menuLink3.Items.Count > 0)
                {
                  foreach (MenuLink pmiItem in ((AMenuSub) menuLink3.Items[0]).Items)
                  {
                    AMenuSub amenuSub3 = new AMenuSub();
                    AMenuSub menuItemRecursive = SiteMaster.GetMenuItemRecursive(pmiItem, int32.ToString());
                    if (menuItemRecursive != null)
                    {
                      MenuLink menuLink6 = new MenuLink();
                      ((Control) menuLink6).ID = row["i_SystemFormId"].ToString();
                      menuLink6.IconImage = row["v_Image"].ToString();
                      menuLink6.Text = row["v_Name"].ToString();
                      menuLink6.CommandName = row["v_Path"].ToString();
                      MenuLink menuLink7 = menuLink6;
                      menuLink7.Click += new CommandEventHandler(this.OnMenuClick);
                      menuItemRecursive.Items.Add((object) menuLink7);
                      pmiItem.Items.Add((object) menuItemRecursive);
                    }
                  }
                }
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        HandledException handledException = new HandledException(6, "error forzado: error al genera opciones del sistema");
        System.Diagnostics.Debug.WriteLine(ex.Message);
      }
    }

    protected void OnMenuClick(object sender, CommandEventArgs e)
    {
      try
      {
        string clientId = ((Control) sender).ClientID;
        string commandName = e.CommandName;
        string commandArgument = e.CommandArgument as string;
        this.Session["Order"] = (object) null;
        this.Response.Redirect(e.CommandName, false);
      }
      catch (Exception ex)
      {
        HandledException handledException = new HandledException(6, "error forzado: error al Abrir opciones del sistema");
      }
    }
  }
}
