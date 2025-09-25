// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.PaymentPOS.PaymentPOS
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.PaymentPOS
{
  public class PaymentPOS : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtCodPayment;
    protected Button wibSearch;
    protected Button btnReturnPopupConfirmation;
    protected Label lblMensage;
    protected Label lblSolicitante;
    protected Label lblfecSoli;
    protected Label lblEstado;
    protected GridView wdgList;
    protected Label lblCantidad;
    protected Label lblCantidadPlacas;
    protected Label lblMonto;
    protected HtmlTableCell tdCond;
    protected CheckBox chkDatos;
    protected Button btnPagar;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      this.lblMensage.Visible = false;
      this.tdCond.Visible = false;
      this.btnPagar.Enabled = false;
      if (this.txtCodPayment.Text == "" || this.txtCodPayment.Text == string.Empty)
      {
        Message.SetMessage(this.lblMensage, new HandledException(1, "Ingrese código de pago."));
        this.txtCodPayment.Focus();
      }
      else
      {
        RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
        DataTable dataTable = new DataTable();
        DataTable byPaymentCode = requirementQueriesBl.RequirementGetByPaymentCode(this.txtCodPayment.Text.ToUpper());
        if (byPaymentCode.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMensage, new HandledException(1, "Código de pago sin información."));
          this.txtCodPayment.Text = string.Empty;
          this.txtCodPayment.Focus();
        }
        else
          this.LoadData(byPaymentCode);
      }
    }

    protected void wdgList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void wdgList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void chkDatos_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkDatos.Checked)
        this.btnPagar.Enabled = true;
      else
        this.btnPagar.Enabled = false;
    }

    protected void btnPagar_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.CreatePopUpServer("Pago POS - VISA", "../PaymentPOS/PaymentVerifyPOS.aspx?CodPago=" + this.txtCodPayment.Text.TrimEnd() + "&Monto=" + this.lblMonto.Text, "540px", "330px");
    }

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
      DataTable dataTable = new DataTable();
      DataTable byPaymentCode = requirementQueriesBl.RequirementGetByPaymentCode(this.txtCodPayment.Text.ToUpper());
      if (byPaymentCode.Rows.Count <= 0 || byPaymentCode.Rows[0]["PaymentStatus"].ToString() == "0")
        return;
      this.LoadData(byPaymentCode);
      this.tdCond.Visible = false;
      this.btnPagar.Enabled = false;
    }

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void LoadData(DataTable dt_Result)
    {
      this.wdgList.DataSource = (object) dt_Result;
      this.wdgList.DataBind();
      this.lblSolicitante.Text = dt_Result.Rows[0]["RequesterCompleteName"].ToString();
      this.lblfecSoli.Text = dt_Result.Rows[0]["RegisterDate"].ToString() + " " + dt_Result.Rows[0]["RegisterTime"].ToString();
      if (dt_Result.Rows[0]["PaymentStatus"].ToString() == "0")
      {
        this.lblEstado.Text = "Pendiente de Pago";
        this.tdCond.Visible = true;
      }
      else
        this.lblEstado.Text = "Acreditado";
      Label lblCantidad = this.lblCantidad;
      string searchresultOk = Constants.SEARCHRESULT_OK;
      int count = dt_Result.Rows.Count;
      string newValue = count.ToString();
      string str1 = searchresultOk.Replace("XX", newValue);
      lblCantidad.Text = str1;
      Label lblCantidadPlacas = this.lblCantidadPlacas;
      count = dt_Result.Rows.Count;
      string str2 = count.ToString();
      lblCantidadPlacas.Text = str2;
      this.lblMonto.Text = "S/ " + dt_Result.Rows[0]["f_PriceTotal"].ToString();
    }
  }
}
