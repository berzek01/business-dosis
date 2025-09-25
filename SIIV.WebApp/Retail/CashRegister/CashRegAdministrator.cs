// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Retail.CashRegister.CashRegAdministrator
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Retail.CashRegister
{
  public class CashRegAdministrator : Page
  {
    protected UpdatePanel UpdatePanel;
    protected Panel Panel0;
    protected GridView wdgCashRegisterList;
    protected Panel Panel1;
    protected TextBox TxtMountStart;
    protected FilteredTextBoxExtender ftbeMonto;
    protected Button BtnGrabar;
    protected Button BtnVolver;
    protected Panel Panel2;
    protected Button BtnStart;
    protected Button BtnFinish;
    protected Button BtnPDF;
    protected Button BtnRefresh;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      System.Web.UI.ScriptManager.GetCurrent(this.Page).RegisterPostBackControl((Control) this.BtnPDF);
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.ViewState["i_LocationId"] = this.Session["SystemUser"] != null ? (object) (this.Session["SystemUser"] as SystemUser).i_LocationId : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
        DataTable cashRegisterList = new RequirementQueriesBL().GetCashRegisterList(-1, Convert.ToInt32(this.ViewState["i_LocationId"]), "", "", 0, 1);
        this.wdgCashRegisterList.DataSource = (object) cashRegisterList;
        this.wdgCashRegisterList.DataBind();
        if (cashRegisterList.Rows.Count == 0)
        {
          this.Panel2.Visible = false;
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, "*****Advertencia*****<br>No existen cajas registradas.<br>&nbsp;&nbsp;");
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

    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
      try
      {
        this.wdgCashRegisterList.DataSource = (object) new RequirementQueriesBL().GetCashRegisterList(-1, Convert.ToInt32(this.ViewState["i_LocationId"]), "", "", 0, 1);
        this.wdgCashRegisterList.DataBind();
        this.wdgCashRegisterList.SelectedIndex = -1;
        this.ViewState["i_CashRegStatus"] = (object) null;
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

    protected void BtnGrabar_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.TxtMountStart.Text.Trim() != "")
        {
          if (Convert.ToInt32(this.TxtMountStart.Text.Trim()) != 0)
          {
            bool flag = false;
            SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
            flag = new RequirementManagementBL().CashRegisterMovement(Convert.ToInt32(this.ViewState["i_CashRegId"]), 1, 1, Convert.ToInt32(systemUser.i_SystemUserId), "", Convert.ToDouble(this.TxtMountStart.Text.Trim()));
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(2, "•&nbsp;La Caja ser Inicio satisfactoriamente."));
            this.BtnGrabar.Enabled = false;
          }
          else
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, "*****Advertencia*****<br>Debe ingresar un monto valido.<br>&nbsp;&nbsp;");
        }
        else
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, "*****Advertencia*****<br>Debe ingresar datos validos.<br>&nbsp;&nbsp;");
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

    protected void BtnStart_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.ViewState["AmountStar"] == null)
          return;
        if (Convert.ToDouble(this.ViewState["AmountStar"]) <= 0.0)
        {
          this.BtnGrabar.Enabled = true;
          this.Panel1.Visible = true;
          this.Panel2.Visible = false;
          this.Panel0.Visible = false;
        }
        else
        {
          this.Panel1.Visible = false;
          this.Panel2.Visible = true;
          this.Panel0.Visible = true;
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

    protected void wdgSupplierList_PageIndexChanged(object sender, EventArgs e)
    {
    }

    protected void wdgCashRegisterList_SelectedIndexChanging(
      object sender,
      GridViewSelectEventArgs e)
    {
      try
      {
        GridViewRow row = this.wdgCashRegisterList.Rows[e.NewSelectedIndex];
        this.ViewState["i_CashRegId"] = (object) row.Cells[2].Text;
        this.ViewState["i_CashRegStatus"] = (object) row.Cells[10].Text;
        this.ViewState["AmountStar"] = (object) row.Cells[5].Text.Trim();
        this.ViewState["AmountTotal"] = (object) row.Cells[8].Text.Trim();
        this.ViewState["AmountIni"] = (object) row.Cells[11].Text.Trim();
        this.TxtMountStart.Text = row.Cells[11].Text.Trim();
        this.lblMessage.Visible = false;
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

    protected void BtnFinish_Click(object sender, EventArgs e)
    {
      try
      {
        this.BtnFinish.Enabled = false;
        if (this.ViewState["i_CashRegStatus"] != null)
        {
          if (Convert.ToInt32(this.ViewState["i_CashRegStatus"]) == 3 || Convert.ToInt32(this.ViewState["i_CashRegStatus"]) == 1)
          {
            bool flag = false;
            SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - WarehouseList.aspx");
            flag = new RequirementManagementBL().CashRegisterMovement(Convert.ToInt32(this.ViewState["i_CashRegId"]), 2, 4, Convert.ToInt32(systemUser.i_SystemUserId), "", Convert.ToDouble(this.ViewState["AmountTotal"]));
            this.BtnRefresh_Click((object) null, (EventArgs) null);
            this.wdgCashRegisterList.SelectedIndex = -1;
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(2, "•&nbsp;La Caja ser Cerro satisfactoriamente."));
            this.BtnFinish.Enabled = true;
            this.ViewState["i_CashRegStatus"] = (object) null;
          }
          else
          {
            this.BtnFinish.Enabled = true;
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, "*****Advertencia*****<br>No puede cerrar esta caja, en el estado en que se encuentran.<br>&nbsp;&nbsp;");
          }
        }
        else
        {
          this.BtnFinish.Enabled = true;
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, "*****Advertencia*****<br>Debe seleccionar un registro.<br>&nbsp;&nbsp;");
        }
      }
      catch (HandledException ex)
      {
        this.BtnFinish.Enabled = true;
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        this.BtnFinish.Enabled = true;
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void BtnPDF_Click(object sender, EventArgs e)
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

    protected void BtnVolver_Click(object sender, EventArgs e)
    {
      try
      {
        this.Panel1.Visible = false;
        this.Panel2.Visible = true;
        this.Panel0.Visible = true;
        this.BtnRefresh_Click((object) null, (EventArgs) null);
        this.lblMessage.Visible = false;
        this.wdgCashRegisterList.SelectedIndex = -1;
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
