// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.PaymentPOS.MethodPayment
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.PaymentPOS
{
  public class MethodPayment : Page
  {
    private double ComisionCanalAtencion = Convert.ToDouble(ConfigurationManager.AppSettings[nameof (ComisionCanalAtencion)]);
    protected UpdatePanel updatepanel1;
    protected HtmlGenericControl divTipoPago;
    protected RadioButtonList rbTypePayment;
    protected HtmlGenericControl divVisa;
    protected Panel PanelMensajeDuplicado;
    protected Button btnContinuar;
    protected HtmlGenericControl divVisa2;
    protected Image Image7;
    protected CheckBox chkVisa;
    protected HyperLink PoliticaDevolucion;
    protected Label lblComision;
    protected Label lblTotalAPagar;
    protected Panel PanelMensajeDuplicadoVisa;
    protected Button btnPaymentVisa;
    protected Label lblMessage;
    protected Button btnJavaScriptCloseVISA;

    protected void Page_Load(object sender, EventArgs e)
    {
      try
      {
        if (!this.Page.IsPostBack)
          this.ViewState["Count"] = (object) 0;
        RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
        int int32_1 = Convert.ToInt32(this.Request.QueryString["RequirementId"].ToString());
        int int32_2 = Convert.ToInt32(this.Request.QueryString["RequirementPlateId"].ToString());
        string str = this.Request.QueryString["t"].ToString();
        this.ViewState["arr"] = (object) (int32_1.ToString() + "|" + int32_2.ToString() + "|" + str.ToString());
        DataTable cur = requirementQueriesBl.GenerateCUR(Convert.ToInt32(int32_1.ToString()));
        this.ViewState["PaymentCode"] = (object) cur.Rows[0]["v_PaymentCode"].ToString();
        this.ViewState["f_PriceTotal"] = (object) cur.Rows[0]["f_PriceTotal"].ToString();
        if (ConfigurationManager.AppSettings["Habilitar"].ToString() == "1")
          this.divTipoPago.Visible = true;
        else
          this.divTipoPago.Visible = false;
        if ((int) Convert.ToInt16(this.Session["ProcessId"].ToString()) != Convert.ToInt32((object) enmProccessType.Duplicado))
          return;
        this.PanelMensajeDuplicado.Visible = true;
        this.PanelMensajeDuplicadoVisa.Visible = true;
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void rbTypePament_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.rbTypePayment.SelectedValue == "0")
      {
        this.divVisa.Visible = true;
        this.divVisa2.Visible = false;
      }
      else
      {
        this.divVisa.Visible = false;
        this.divVisa2.Visible = true;
        DataTable databyPaymentCode = new RequirementQueriesBL().GetPaymentDatabyPaymentCode(this.ViewState["PaymentCode"].ToString());
        double num1 = Convert.ToDouble(this.ViewState["f_PriceTotal"]);
        double num2 = Convert.ToDouble(databyPaymentCode.Rows[0]["f_PriceTotal"].ToString());
        double num3 = num2 - num1;
        Label lblComision = this.lblComision;
        double num4 = Math.Round(num3, 2);
        string str1 = num4.ToString();
        lblComision.Text = str1;
        Label lblTotalApagar = this.lblTotalAPagar;
        num4 = Math.Round(num2, 2);
        string str2 = num4.ToString();
        lblTotalApagar.Text = str2;
      }
    }

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format("OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatepanel1, this.updatepanel1.GetType(), "Script", script, true);
    }

    protected void btnPaymentVisa_Click(object sender, EventArgs e)
    {
      RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
      try
      {
        this.ViewState["PaymentCode"].ToString();
        string[] strArray = this.ViewState["arr"].ToString().Split('|');
        this.Session["iLogId"] = (object) null;
        this.Session["RequirementIds"] = (object) strArray[0].ToString();
        DataTable byPaymentCode = requirementQueriesBl.RequirementGetByPaymentCode("", Convert.ToInt32(strArray[0].ToString()));
        double num = Convert.ToDouble(byPaymentCode.Rows[0]["f_PriceTotal"].ToString());
        if (this.rbTypePayment.SelectedValue == "1")
          this.Session["TotalAPagar"] = (object) Math.Round(num * (1.0 + this.ComisionCanalAtencion), 2);
        requirementQueriesBl.UpdateRequirementBoundVisa(Convert.ToInt32(strArray[0]));
        string empty = string.Empty;
        this.Session["RequirementPlateType"] = (object) Convert.ToInt32((object) enmQuery.Delivery);
        this.CreatePopUpServer("Pago OnLine - VISA", "../PaymentPOS/SendEticket.aspx?PaymentCode=" + byPaymentCode.Rows[0]["v_PaymentCode"].ToString(), "770px", "690px");
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(1, ex.Message));
      }
    }

    protected void btnJavaScriptCloseVISA_Click(object sender, EventArgs e)
    {
      if (Convert.ToInt32(this.Session["VisaPagoConforme"]) != 1)
        return;
      string[] strArray = this.ViewState["arr"].ToString().Split('|');
      this.Response.Redirect("~/Requirement/SuccessfulRegistration.aspx?RequirementId=" + strArray[0].ToString() + "&RequirementPlateId=" + strArray[1].ToString() + "&t=" + strArray[2].ToString() + "&mre=0", false);
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
      try
      {
        RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
        string[] strArray = this.ViewState["arr"].ToString().Split('|');
        requirementQueriesBl.UpdateRequirementBoundCash(Convert.ToInt32(strArray[0].ToString((IFormatProvider) CultureInfo.CurrentCulture)));
        this.Response.Redirect("~/Requirement/SuccessfulRegistration.aspx?RequirementId=" + strArray[0].ToString() + "&RequirementPlateId=" + strArray[1].ToString() + "&t=" + strArray[2].ToString() + "&mre=0", false);
        this.Session["SendEmail"] = (object) 1;
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(1, ex.Message));
      }
    }

    protected void chkVisa_CheckedChanged(object sender, EventArgs e)
    {
    }
  }
}
