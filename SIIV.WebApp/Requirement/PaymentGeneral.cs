// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Requirement.PaymentGeneral
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Requirement
{
  public class PaymentGeneral : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtCodPayment;
    protected Button wibSearch;
    protected Label lblMessage;
    protected Label lblSolicitante;
    protected Label lblfecSoli;
    protected Label lblEstadoSol;
    protected Label lblEstado;
    protected GridView wdgList;
    protected Label lblCantidad;
    protected Label lblCantidadPlacas;
    protected Label lblMonto;
    protected HtmlTableCell tdCond;
    protected CheckBox chkDatos;
    protected Button btnAnular;

    protected void Page_Load(object sender, EventArgs e) => this.lblMessage.Visible = false;

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        this.tdCond.Visible = false;
        this.btnAnular.Enabled = false;
        if (this.txtCodPayment.Text == "" || this.txtCodPayment.Text == string.Empty)
          throw new HandledException(1, "Ingrese código de pago.");
        RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
        DataTable dataTable = new DataTable();
        DataTable byPaymentCode = requirementQueriesBl.RequirementGetByPaymentCode(this.txtCodPayment.Text.ToUpper());
        if (byPaymentCode.Rows.Count == 0)
        {
          this.txtCodPayment.Text = string.Empty;
          this.txtCodPayment.Focus();
          throw new HandledException(1, "Código de pago sin información.");
        }
        this.LoadData(byPaymentCode);
      }
      catch (HandledException ex)
      {
        this.HidePopup();
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        this.HidePopup();
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void btnAnular_Click(object sender, EventArgs e)
    {
      try
      {
        RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
        if (requirementQueriesBl.UpdateDeletePaymentRequirement(this.txtCodPayment.Text, systemUser.i_SystemUserId) > 0)
          Message.SetMessage(this.lblMessage, new HandledException(2, "Se procedio con la anulación del pago : " + this.txtCodPayment.Text));
        else
          Message.SetMessage(this.lblMessage, new HandledException(1, "La anulación fue incorrecta, favor de comunicarme con su Administrador Informático"));
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
        this.HidePopup();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
        this.HidePopup();
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void chkDatos_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkDatos.Checked)
        this.btnAnular.Enabled = true;
      else
        this.btnAnular.Enabled = false;
    }

    private void LoadData(DataTable dt_Result)
    {
      try
      {
        this.wdgList.DataSource = (object) dt_Result;
        this.wdgList.DataBind();
        this.lblSolicitante.Text = dt_Result.Rows[0]["RequesterCompleteName"].ToString();
        this.lblfecSoli.Text = dt_Result.Rows[0]["RegisterDate"].ToString() + " " + dt_Result.Rows[0]["RegisterTime"].ToString();
        this.lblEstadoSol.Text = dt_Result.Rows[0]["RpStatus"].ToString();
        if (dt_Result.Rows[0]["PaymentStatus"].ToString() == "0")
        {
          this.lblEstado.Text = "Pendiente de Pago";
          this.tdCond.Visible = true;
        }
        else
          this.lblEstado.Text = "Acreditado";
        this.lblCantidad.Text = Constants.SEARCHRESULT_OK.Replace("XX", dt_Result.Rows.Count.ToString());
        this.lblCantidadPlacas.Text = dt_Result.Rows.Count.ToString();
        this.lblMonto.Text = "S/ " + dt_Result.Rows[0]["f_PriceTotal"].ToString();
      }
      catch (Exception ex)
      {
        throw ex;
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
  }
}
