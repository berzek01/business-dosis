// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Retail.CashRegister.CashRegOpenRead
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Retail.CashRegister
{
  public class CashRegOpenRead : Page
  {
    protected UpdatePanel UpdatePanel;
    protected Button BtnSincronization;
    protected Label LblCashRegId;
    protected HtmlImage ImgLoading;
    protected Timer Timer1;
    protected Panel Panel1;
    protected TextBox TxtCaja;
    protected TextBox TxtFecha;
    protected TextBox TxtEstado;
    protected TextBox TxtUsuario;
    protected TextBox TxtSalIni;
    protected TextBox TxtEfectivo;
    protected TextBox TxtVisa;
    protected TextBox TxtTotal;
    protected Button BtnConfig;
    protected Button BtnOpen;
    protected Button BtnClose;
    protected Button BtnPdf;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      System.Web.UI.ScriptManager.GetCurrent(this.Page).RegisterPostBackControl((Control) this.BtnPdf);
      if (this.IsPostBack)
        return;
      SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
      this.ViewState["i_SystemUserId"] = (object) systemUser.i_SystemUserId;
      this.ViewState["i_LocationId"] = (object) systemUser.i_LocationId;
    }

    private void OpenURI()
    {
      if (this.Session["SystemUser"] == null)
        throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
      string script = "EjecutarURI(" + (this.Session["SystemUser"] as SystemUser).i_SystemUserId.ToString() + ");";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private void OpenURI2()
    {
      string script = "EjecutarURI2('" + this.ViewState["v_CodStore"]?.ToString() + "','" + this.ViewState["v_CashRegCode"]?.ToString() + "');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void BtnSincronization_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.Session["SystemUser"] == null)
          throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
        if (!new RequirementManagementBL().RegisteUserQueryIdenti((this.Session["SystemUser"] as SystemUser).i_SystemUserId, 0, 0, 1))
          return;
        this.Session["iConta"] = (object) 0;
        this.Panel1.Visible = false;
        this.ImgLoading.Visible = true;
        this.OpenURI();
        this.Timer1.Enabled = true;
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
      this.Session["iConta"] = (object) (Convert.ToInt32(this.Session["iConta"]) + 1);
      DataTable proceesCashRegUser = new RequirementQueriesBL().GetActionProceesCashRegUser(Convert.ToInt32(this.ViewState["i_SystemUserId"]));
      try
      {
        if (proceesCashRegUser.Rows[0][0].ToString() == "101" && proceesCashRegUser.Rows[0][1].ToString() == "2")
        {
          this.ImgLoading.Visible = false;
          this.Timer1.Enabled = false;
          this.ViewState["Identification"] = (object) proceesCashRegUser.Rows[0]["v_Identification"].ToString();
          this.ObtenerDatos();
        }
        if (proceesCashRegUser.Rows[0][1].ToString() != "0" && proceesCashRegUser.Rows[0][1].ToString() != "2")
        {
          this.ImgLoading.Visible = false;
          this.Timer1.Enabled = false;
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(0, proceesCashRegUser.Rows[0][2].ToString()));
        }
        if (Convert.ToInt32(this.Session["iConta"]) != Convert.ToInt32(ConfigurationManager.AppSettings["EBillingWaitTime"]))
          return;
        this.ImgLoading.Visible = false;
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(0, "Error en Conexión"));
        this.Timer1.Enabled = false;
      }
      catch
      {
      }
      finally
      {
      }
    }

    protected void TxtUsuario_TextChanged(object sender, EventArgs e)
    {
    }

    private void ObtenerDatos()
    {
      DataTable cashRegisterList = new RequirementQueriesBL().GetCashRegisterList(-1, Convert.ToInt32(this.ViewState["i_LocationId"]), "", this.ViewState["Identification"].ToString(), 0, 1);
      if (cashRegisterList.Rows.Count > 0)
      {
        this.LblCashRegId.Text = cashRegisterList.Rows[0]["i_CashRegId"].ToString();
        this.TxtCaja.Text = cashRegisterList.Rows[0]["v_CashRegCode"].ToString();
        this.ViewState["i_CashRegId"] = (object) cashRegisterList.Rows[0]["i_CashRegId"].ToString();
        this.ViewState["v_CashRegCode"] = (object) cashRegisterList.Rows[0]["v_CashRegCode"].ToString();
        this.ViewState["v_CodStore"] = (object) cashRegisterList.Rows[0]["v_CodStore"].ToString();
        this.ViewState["CashRegStatus"] = (object) cashRegisterList.Rows[0]["CashRegStatus"].ToString();
        this.TxtEstado.Text = cashRegisterList.Rows[0]["i_CashRegStatus"].ToString();
        this.TxtFecha.Text = cashRegisterList.Rows[0]["d_CashRegDate"].ToString();
        this.TxtUsuario.Text = cashRegisterList.Rows[0]["v_User"].ToString();
        this.ViewState["i_CashRegId"] = (object) cashRegisterList.Rows[0]["i_CashRegId"].ToString();
        this.TxtSalIni.Text = cashRegisterList.Rows[0]["f_AmountStart"].ToString();
        this.TxtEfectivo.Text = cashRegisterList.Rows[0]["f_AmountCash"].ToString();
        this.TxtVisa.Text = cashRegisterList.Rows[0]["f_AmountVisa"].ToString();
        this.TxtTotal.Text = cashRegisterList.Rows[0]["f_AmountTotal"].ToString();
        this.ViewState["i_CashRegUserId"] = (object) cashRegisterList.Rows[0]["i_CashRegUserId"].ToString();
        this.Panel1.Visible = true;
      }
      else
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, "*****Advertencia*****<br>No existe información de caja<br>&nbsp;&nbsp;");
    }

    protected void BtnOpen_Click(object sender, EventArgs e)
    {
      try
      {
        if (Convert.ToInt32(this.ViewState["i_CashRegUserId"]) != -1 && Convert.ToInt32(this.ViewState["i_CashRegUserId"]) != Convert.ToInt32(this.ViewState["i_SystemUserId"]))
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, "*****Advertencia*****<br>Este Usuario no puede realizar esta operación<br>&nbsp;&nbsp;" + this.ViewState["i_CashRegUserId"]?.ToString() + "-" + this.ViewState["i_SystemUserId"]?.ToString());
        else if (Convert.ToInt32(this.ViewState["CashRegStatus"]) == 4)
        {
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, "*****Advertencia*****<br>No se puede abrir caja, revisar su estado<br>&nbsp;&nbsp;");
        }
        else
        {
          SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
          new RequirementManagementBL().RegisteCashRegister(Convert.ToInt32(this.LblCashRegId.Text.Trim()), -1, "", "", 0.0, 2, 1, Convert.ToInt32(systemUser.i_SystemUserId), 4);
          this.Session["i_CashRegId"] = this.ViewState["i_CashRegId"];
          this.Session["v_CashRegCode"] = this.ViewState["v_CashRegCode"];
          this.ObtenerDatos();
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(2, "•&nbsp;La Caja ser Abrio satisfactoriamente."));
        }
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void BtnClose_Click(object sender, EventArgs e)
    {
      try
      {
        if (Convert.ToInt32(this.ViewState["i_CashRegUserId"]) != -1 && Convert.ToInt32(this.ViewState["i_CashRegUserId"]) != Convert.ToInt32(this.ViewState["i_SystemUserId"]))
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, "*****Advertencia*****<br>Este Usuario no puede realizar esta operación<br>&nbsp;&nbsp;" + this.ViewState["i_CashRegUserId"]?.ToString() + "-" + this.ViewState["i_SystemUserId"]?.ToString());
        else if (Convert.ToInt32(this.ViewState["CashRegStatus"]) == 1 || Convert.ToInt32(this.ViewState["CashRegStatus"]) == 4)
        {
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, "*****Advertencia*****<br>No se puede abrir caja, revisar su estado<br>&nbsp;&nbsp;");
        }
        else
        {
          SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
          new RequirementManagementBL().RegisteCashRegister(Convert.ToInt32(this.LblCashRegId.Text.Trim()), -1, "", "", 0.0, 3, 1, Convert.ToInt32(systemUser.i_SystemUserId), 4);
          this.Session.Remove("i_CashRegId");
          this.Session.Remove("v_CashRegCode");
          this.ObtenerDatos();
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(2, "•&nbsp;La Caja ser Cerro satisfactoriamente."));
        }
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void BtnPdf_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.ViewState["i_CashRegId"] == null)
          return;
        using (ReportDocument reportDocument = new ReportDocument())
        {
          Convert.ToInt32(this.ViewState["i_Requirement"], (IFormatProvider) CultureInfo.CurrentCulture);
          Convert.ToInt32(this.ViewState["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture);
          string filename = this.Server.MapPath("../CashRegister/ReportCashReg.rpt");
          reportDocument.Load(filename);
          DataTable cashRegisterList = new RequirementQueriesBL().GetCashRegisterList(Convert.ToInt32(this.ViewState["i_CashRegId"]), Convert.ToInt32(this.ViewState["i_LocationId"]), "", "", 0, 2);
          reportDocument.SetDataSource(cashRegisterList);
          reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "Caja");
          reportDocument.Close();
          ((Component) reportDocument).Dispose();
        }
        GC.Collect();
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void BtnConfig_Click(object sender, EventArgs e)
    {
      try
      {
        this.OpenURI2();
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }
  }
}
