// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibicion.Operation.UnLockPlates
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibicion.Operation
{
  public class UnLockPlates : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtRazonSocial;
    protected TextBox txtCliente;
    protected TextBox txtPlaca;
    protected TextBox txtDias;
    protected TextBox txtOperacion;
    protected FilteredTextBoxExtender FilteredTextBoxExtender1;
    protected TextBox txtBanco;
    protected Fecha wdpPaymentDate;
    protected TextBox txtCarta;
    protected FilteredTextBoxExtender ftbeCorporate;
    protected Fecha wdpCartDate;
    protected Label lblMessage;
    protected Button wibAceptar;
    protected Button wibCancelar;
    protected HiddenField HiddenField1;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.LoadData();
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

    private void LoadData()
    {
      try
      {
        if (this.Request.QueryString["i_RequirementPlateId"] == null)
          return;
        int int32_1 = Convert.ToInt32(this.Request.QueryString["i_RequirementPlateId"]);
        int int32_2 = Convert.ToInt32(this.Request.QueryString["i_vehicleMovementId"]);
        this.ViewState["i_RequirementPlateId"] = (object) int32_1;
        this.ViewState["i_vehicleMovementId"] = (object) int32_2;
        GridViewRow gridViewRow = (GridViewRow) this.Session["selectedRows"];
        this.txtPlaca.Text = gridViewRow.Cells[5].Text;
        this.txtRazonSocial.Text = gridViewRow.Cells[2].Text;
        this.txtCliente.Text = gridViewRow.Cells[4].Text;
        this.txtDias.Text = gridViewRow.Cells[6].Text;
        if (Convert.ToInt32(this.txtDias.Text) > 2)
        {
          this.txtOperacion.Enabled = true;
          this.txtBanco.Enabled = true;
          this.wdpPaymentDate.Enabled = true;
        }
        else
        {
          this.txtCarta.Enabled = true;
          this.wdpCartDate.Enabled = true;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void wibAceptar_Click(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        string text = this.txtPlaca.Text;
        int int32_1 = Convert.ToInt32(this.ViewState["i_RequirementPlateId"]);
        int int32_2 = Convert.ToInt32(this.ViewState["i_vehicleMovementId"]);
        string v_numOperation = "";
        string v_Bank = "";
        DateTime? d_PaymentDate = new DateTime?();
        string v_numCart = "";
        DateTime? d_CartDate = new DateTime?();
        if (Convert.ToInt32(this.txtDias.Text) > 2)
        {
          this.Validate("01");
          v_numOperation = this.txtOperacion.Text.ToString();
          v_Bank = this.txtBanco.Text;
          d_PaymentDate = this.wdpPaymentDate.Text != "" ? new DateTime?(this.wdpPaymentDate.Value) : new DateTime?();
        }
        else
        {
          this.Validate("02");
          v_numCart = this.txtCarta.Text.ToString();
          d_CartDate = this.wdpCartDate.Text != "" ? new DateTime?(this.wdpCartDate.Value) : new DateTime?();
        }
        int iSystemUserId = systemUser.i_SystemUserId;
        if (new BlockPlateBL().BlockPlateUpdate(int32_1, int32_2, text, v_numOperation, v_Bank, d_PaymentDate, v_numCart, d_CartDate, iSystemUserId) > 0)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Success, "Se desbloqueo con éxito la placa.");
          this.wibAceptar.Enabled = false;
        }
        else
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se pudo realizar el desbloqueo de la placa.");
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

    private new void Validate(string v_Option)
    {
      if (v_Option == "01")
      {
        if (this.txtOperacion.Text == "")
          throw new HandledException(1, "Debe ingresar el nro de operacion.");
        if (this.txtBanco.Text == "")
          throw new HandledException(1, "Debe ingresar el banco.");
        if (this.wdpPaymentDate.Text == "")
          throw new HandledException(1, "Debe ingresar la fecha de pago.");
      }
      else
      {
        if (this.txtCarta.Text == "")
          throw new HandledException(1, "Debe ingresar el nro de carta.");
        if (this.wdpCartDate.Text == "")
          throw new HandledException(1, "Debe ingresar la fecha de la carta.");
      }
    }

    protected void wibCancelar_Click(object sender, EventArgs e)
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }
  }
}
