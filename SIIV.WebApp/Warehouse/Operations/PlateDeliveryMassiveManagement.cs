// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.PlateDeliveryMassiveManagement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using SIIV.WebApp.Claim;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class PlateDeliveryMassiveManagement : Page
  {
    private DataTable dtWastageControlOperation = new DataTable();
    protected UpdatePanel UpdatePanel1;
    protected HiddenField hdTramite;
    protected HiddenField hdIntercambio;
    protected HiddenField hdPropietario;
    protected HiddenField hdProcessTypeId;
    protected HiddenField hdUseCorrespondence;
    protected HiddenField hdddWareHouseControlDetailID;
    protected Label lblNumMov;
    protected HtmlTableRow trInsertData;
    protected TextBox txtFecha;
    protected TextBox txtTipo;
    protected TextBox txtTotalReg;
    protected HtmlTableRow tr1;
    protected RadioButtonList rblPersonTypeOwner;
    protected TextBox TxtVerificador;
    protected FilteredTextBoxExtender TxtVerificador_FilteredTextBoxExtender;
    protected Button btnVerificar;
    protected Label lblTotalHeader;
    protected Label lblDeliveryHeader;
    protected Label lblReturnHeader;
    protected HtmlGenericControl Div1;
    protected GridView wdgDeliveryMassive;
    protected Button btnManagementTemp;
    protected Button btnFinalizeTmp;
    protected Button btnClaimTmp;
    protected Label lblTotal;
    protected Label lblDelivery;
    protected Label lblReturn;
    protected Button wibSave;
    protected Button wibReturn;
    protected Button btnReturn;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.TxtVerificador.Attributes.Add("onkeypress", "return SiguienteFoco();");
        this.InitialPage();
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

    protected void btnVerificar_Click(object sender, EventArgs e)
    {
      try
      {
        this.lblMessage.Text = string.Empty;
        this.lblMessage.Style.Add("display", "none");
        if (this.TxtVerificador.Text.Trim() == "")
          return;
        this.VerificateRequirement();
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

    protected void wdgDeliveryMassive_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowIndex < 0)
          return;
        if (e.Row.Cells[5].Text.ToUpper() == "ENVIADO")
        {
          for (int index = 0; index < 10; ++index)
            e.Row.Cells[index].CssClass = "Test2";
        }
        else if (e.Row.Cells[5].Text.ToUpper() == "SELECCIONADO")
        {
          for (int index = 0; index < 10; ++index)
            e.Row.Cells[index].CssClass = "Test";
        }
        else if (e.Row.Cells[5].Text.ToUpper() == "ENTREGADO")
        {
          for (int index = 0; index < 10; ++index)
            e.Row.Cells[index].CssClass = "Test4";
        }
        else if (e.Row.Cells[5].Text.ToUpper() == "RETORNADO")
        {
          for (int index = 0; index < 10; ++index)
            e.Row.Cells[index].CssClass = "Test3";
        }
        else
        {
          for (int index = 0; index < 10; ++index)
            e.Row.Cells[index].CssClass = "Test1";
        }
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

    protected void wdgDeliveryMassive_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        int int32_1 = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = this.wdgDeliveryMassive.Rows[int32_1];
        if (e.CommandName == "Return")
        {
          this.Session["DeliveryMassive"] = row != null ? (object) row : throw new HandledException(4, "Error de selección.", "'wdgDeliveryMassive' - PlateDeliveryMassiveManagement.aspx");
          string upper = row.Cells[5].Text.ToUpper();
          int int32_2 = Convert.ToInt32(this.wdgDeliveryMassive.DataKeys[int32_1]["i_UserAuxId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
          string text = row.Cells[10].Text;
          bool flag = true;
          switch (upper)
          {
            case "RETORNADO":
              flag = false;
              break;
            case "EN RECLAMO":
              flag = true;
              break;
            case "ENTREGADO":
              flag = false;
              break;
          }
          if (!flag)
            return;
          string empty = string.Empty;
          this.CreatePopUpServer("Retornar Placa", "../../Warehouse/Operations/PlateDeliveryMassiveDetail.aspx?UserAuxId=" + int32_2.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&UserAux=" + text, "650px", "400px");
        }
        else
        {
          if (!(e.CommandName == "Claim"))
            return;
          string empty = string.Empty;
          string upper = row.Cells[5].Text.ToUpper();
          bool flag = true;
          switch (upper)
          {
            case "RETORNADO":
              flag = false;
              break;
            case "EN RECLAMO":
              flag = false;
              break;
            case "ENTREGADO":
              flag = false;
              break;
          }
          if (!flag)
            return;
          this.hdddWareHouseControlDetailID.Value = this.wdgDeliveryMassive.DataKeys[int32_1]["i_WarehouseControlDetailId"].ToString();
          this.CreatePopUpServer("Reclamo por producto", new ClaimGenerator().GenerateClaim_ProductNoAgree(row.Cells[2].Text, row.Cells[3].Text, this.wdgDeliveryMassive.DataKeys[int32_1]["i_ProcessTypeId"].ToString(), this.wdgDeliveryMassive.DataKeys[int32_1]["i_ProductId"].ToString(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0), "755px", "630px");
        }
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

    protected void btnManagementTemp_Click(object sender, EventArgs e)
    {
      try
      {
        this.RefreshParent();
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

    protected void btnFinalizeTmp_Click(object sender, EventArgs e)
    {
      try
      {
        this.FinalizeDeliver();
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

    protected void btnClaimTmp_Click(object sender, EventArgs e)
    {
      try
      {
        new PlateDeliverManagementBL().PlateDeliveryMassiveClaimUpdate(Convert.ToInt32(this.hdddWareHouseControlDetailID.Value, (IFormatProvider) CultureInfo.CurrentCulture), "*** Reclamo de Producto no Conforme");
        this.InitialPage();
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

    protected void wibSave_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.hdTramite.Value != string.Empty)
        {
          int result = 0;
          string str = this.hdUseCorrespondence.Value;
          int.TryParse(this.rblPersonTypeOwner.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), out result);
          string empty = string.Empty;
          this.CreatePopUpServer("Entrega Masiva", "../../Warehouse/Operations/PlateDeliveryMassiveClose.aspx?strUseCorrespondence=" + str + "&intTipPers=" + result.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&intTipProc=" + this.hdProcessTypeId.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture), "500px", "600px");
        }
        else
        {
          this.lblMessage.Style.Add("display", "");
          Message.SetMessage(this.lblMessage, new HandledException(1, "Debe seleccionar al menos un registro de la lista."));
        }
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

    protected void wibReturn_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("~/Warehouse/Operations/PlateDeliveryMassive.aspx", false);
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("~/Warehouse/Operations/PlateDeliveryMassive.aspx", false);
    }

    private void InitialPage()
    {
      try
      {
        int int32 = Convert.ToInt32(this.Request.QueryString["WarehouseControlId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        this.lblNumMov.Text = int32.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.txtFecha.Text = this.Request.QueryString["strFecha"].ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.txtTipo.Text = this.Request.QueryString["strTipo"].ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.dtWastageControlOperation = new PlateDeliverQueriesBL().GetMassiveDeliveryWarehouseControlDetail(int32);
        this.ViewState["dtWastageControlOperation"] = (object) this.dtWastageControlOperation;
        this.wdgDeliveryMassive.DataSource = (object) this.dtWastageControlOperation;
        this.wdgDeliveryMassive.DataBind();
        this.Session["dtWastageControlOperation"] = (object) this.dtWastageControlOperation;
        this.lblTotal.Text = "0/" + this.dtWastageControlOperation.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.lblDelivery.Text = "0/" + this.dtWastageControlOperation.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.lblReturn.Text = "0/" + this.dtWastageControlOperation.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.lblTotalHeader.Text = "0/" + this.dtWastageControlOperation.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.lblDeliveryHeader.Text = "0/" + this.dtWastageControlOperation.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.lblReturnHeader.Text = "0/" + this.dtWastageControlOperation.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.txtTotalReg.Text = this.dtWastageControlOperation.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.CalculateTotal();
        this.TxtVerificador.Focus();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void CalculateTotal()
    {
      try
      {
        DataTable dataTable = this.Session["dtWastageControlOperation"] as DataTable;
        int num1 = 0;
        int num2 = 0;
        int num3 = 0;
        if (dataTable == null)
          return;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (row["v_Status"].ToString().ToUpper(CultureInfo.CurrentCulture) == "SELECCIONADO")
            ++num3;
          else if (row["v_Status"].ToString().ToUpper(CultureInfo.CurrentCulture) == "RETORNADO")
            ++num2;
          else if (row["v_Status"].ToString().ToUpper(CultureInfo.CurrentCulture) == "ENTREGADO")
            ++num1;
        }
        this.lblTotal.Text = num3.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "/" + dataTable.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        Label lblDelivery = this.lblDelivery;
        string str1 = num1.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        int count = dataTable.Rows.Count;
        string str2 = count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        string str3 = str1 + "/" + str2;
        lblDelivery.Text = str3;
        Label lblReturn = this.lblReturn;
        string str4 = num2.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        count = dataTable.Rows.Count;
        string str5 = count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        string str6 = str4 + "/" + str5;
        lblReturn.Text = str6;
        Label lblTotalHeader = this.lblTotalHeader;
        string str7 = num3.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        count = dataTable.Rows.Count;
        string str8 = count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        string str9 = str7 + "/" + str8;
        lblTotalHeader.Text = str9;
        Label lblDeliveryHeader = this.lblDeliveryHeader;
        string str10 = num1.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        count = dataTable.Rows.Count;
        string str11 = count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        string str12 = str10 + "/" + str11;
        lblDeliveryHeader.Text = str12;
        Label lblReturnHeader = this.lblReturnHeader;
        string str13 = num2.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        count = dataTable.Rows.Count;
        string str14 = count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        string str15 = str13 + "/" + str14;
        lblReturnHeader.Text = str15;
        if (num3 == 0)
        {
          this.hdIntercambio.Value = string.Empty;
          this.hdPropietario.Value = string.Empty;
          this.hdTramite.Value = string.Empty;
          this.hdProcessTypeId.Value = string.Empty;
          this.rblPersonTypeOwner.Enabled = true;
        }
        else
          this.rblPersonTypeOwner.Enabled = false;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void VerificateRequirement()
    {
      try
      {
        string empty = string.Empty;
        string str;
        if (this.TxtVerificador.Text.Trim().Length != 10 && this.TxtVerificador.Text.Trim() != "")
        {
          if (this.TxtVerificador.Text.Trim().Length >= 5 && this.TxtVerificador.Text.Trim().Length <= 8)
          {
            str = this.TxtVerificador.Text.Trim();
          }
          else
          {
            this.TxtVerificador.Text = "";
            this.TxtVerificador.Focus();
            this.lblMessage.Style.Add("display", "");
            throw new HandledException(1, "Dato ingresado es incorrecto.");
          }
        }
        else
          str = Convert.ToInt32(this.TxtVerificador.Text.Trim().Substring(2), (IFormatProvider) CultureInfo.CurrentCulture).ToString((IFormatProvider) CultureInfo.CurrentCulture);
        DataTable dataTable = this.Session["dtWastageControlOperation"] != null ? this.Session["dtWastageControlOperation"] as DataTable : throw new HandledException(3, "La sesión ha expirado.", "'dtWastageControlOperation' - PlateDeliveryMassiveManagement.aspx");
        dataTable.Columns["v_Status"].ReadOnly = false;
        int num = 0;
        int result1 = 1;
        int result2 = 0;
        int.TryParse(this.rblPersonTypeOwner.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), out result1);
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["i_RequirementPlateId"].ToString() == str)
            {
              num = 1;
              if (row["v_Conciliar"].ToString() == "X")
              {
                this.lblMessage.Style.Add("display", "");
                this.TxtVerificador.Text = string.Empty;
                this.TxtVerificador.Focus();
                throw new HandledException(1, "La solicitud pertenece a un código de pago no conciliado.");
              }
              if (row["v_Status"].ToString().ToUpper(CultureInfo.CurrentCulture) == "SELECCIONADO")
              {
                row["v_Status"] = (object) "ENVIADO";
                this.lblMessage.Style.Add("display", "");
                Message.SetMessage(this.lblMessage, new HandledException(1, "La placa fue removida de la selección."));
                this.TxtVerificador.Text = string.Empty;
                this.TxtVerificador.Focus();
                break;
              }
              if (row["v_Status"].ToString().ToUpper(CultureInfo.CurrentCulture) == "ENTREGADO")
              {
                this.lblMessage.Style.Add("display", "");
                Message.SetMessage(this.lblMessage, new HandledException(1, "La placa fue Entregada, no puede ser seleccionada."));
                this.TxtVerificador.Text = string.Empty;
                this.TxtVerificador.Focus();
                break;
              }
              if (row["v_Status"].ToString().ToUpper(CultureInfo.CurrentCulture) == "EN RECLAMO")
              {
                this.lblMessage.Style.Add("display", "");
                Message.SetMessage(this.lblMessage, new HandledException(1, "La placa fue Reclamada, no puede ser seleccionada."));
                this.TxtVerificador.Text = string.Empty;
                this.TxtVerificador.Focus();
                break;
              }
              if (row["v_Status"].ToString().ToUpper(CultureInfo.CurrentCulture) == "RETORNADO")
              {
                this.lblMessage.Style.Add("display", "");
                Message.SetMessage(this.lblMessage, new HandledException(1, "La placa fue Retornada, no puede ser seleccionada."));
                this.TxtVerificador.Text = string.Empty;
                this.TxtVerificador.Focus();
                break;
              }
              if (result1 == 1)
              {
                if (this.hdTramite.Value == string.Empty)
                {
                  this.hdTramite.Value = row["v_ProcessType"].ToString();
                  this.hdProcessTypeId.Value = row["i_ProcessTypeId"].ToString();
                  this.hdUseCorrespondence.Value = row["v_UseCorrespondence"].ToString();
                  this.hdIntercambio.Value = row["i_TotBlank"].ToString();
                  row["v_Status"] = (object) "SELECCIONADO";
                  this.TxtVerificador.Text = string.Empty;
                  this.TxtVerificador.Focus();
                  break;
                }
                if (this.hdTramite.Value == row["v_ProcessType"].ToString())
                {
                  int.TryParse(row["i_TotBlank"].ToString(), out result2);
                  if (this.hdIntercambio.Value == result2.ToString((IFormatProvider) CultureInfo.CurrentCulture))
                  {
                    if (this.hdUseCorrespondence.Value == row["v_UseCorrespondence"].ToString())
                    {
                      row["v_Status"] = (object) "SELECCIONADO";
                      this.TxtVerificador.Text = string.Empty;
                      this.TxtVerificador.Focus();
                      break;
                    }
                    this.lblMessage.Style.Add("display", "");
                    this.TxtVerificador.Text = string.Empty;
                    this.TxtVerificador.Focus();
                    throw new HandledException(1, "Uso origen y destino no pueden ser diferentes.");
                  }
                  this.lblMessage.Style.Add("display", "");
                  this.TxtVerificador.Text = string.Empty;
                  this.TxtVerificador.Focus();
                  throw new HandledException(1, "La cantidad de objetos de intercambio no pueden ser diferentes.");
                }
                if (this.hdTramite.Value != row["v_ProcessType"].ToString())
                {
                  this.lblMessage.Style.Add("display", "");
                  this.TxtVerificador.Text = string.Empty;
                  this.TxtVerificador.Focus();
                  throw new HandledException(1, "Los trámites no pueden ser diferentes.");
                }
              }
              else
              {
                if (this.hdTramite.Value == string.Empty)
                {
                  this.hdTramite.Value = row["v_ProcessType"].ToString();
                  this.hdPropietario.Value = row["v_OwnerCompleteName"].ToString();
                  this.hdProcessTypeId.Value = row["i_ProcessTypeId"].ToString();
                  this.hdIntercambio.Value = row["i_TotBlank"].ToString();
                  row["v_Status"] = (object) "SELECCIONADO";
                  this.TxtVerificador.Text = string.Empty;
                  this.TxtVerificador.Focus();
                  break;
                }
                if (this.hdPropietario.Value == row["v_OwnerCompleteName"].ToString())
                {
                  if (this.hdTramite.Value == row["v_ProcessType"].ToString())
                  {
                    int.TryParse(row["i_TotBlank"].ToString(), out result2);
                    if (this.hdIntercambio.Value == result2.ToString((IFormatProvider) CultureInfo.CurrentCulture))
                    {
                      if (this.hdUseCorrespondence.Value == row["v_UseCorrespondence"].ToString())
                      {
                        row["v_Status"] = (object) "SELECCIONADO";
                        this.TxtVerificador.Text = string.Empty;
                        this.TxtVerificador.Focus();
                        break;
                      }
                      this.lblMessage.Style.Add("display", "");
                      this.TxtVerificador.Text = string.Empty;
                      this.TxtVerificador.Focus();
                      throw new HandledException(1, "Uso origen y destino no pueden ser diferentes.");
                    }
                    this.lblMessage.Style.Add("display", "");
                    this.TxtVerificador.Text = string.Empty;
                    this.TxtVerificador.Focus();
                    throw new HandledException(1, "La cantidad de objetos de intercambio no pueden ser diferentes.");
                  }
                  if (this.hdTramite.Value != row["v_ProcessType"].ToString())
                  {
                    this.lblMessage.Style.Add("display", "");
                    this.TxtVerificador.Text = string.Empty;
                    this.TxtVerificador.Focus();
                    throw new HandledException(1, "Los trámites no pueden ser diferentes.");
                  }
                }
                else if (this.hdPropietario.Value != row["v_OwnerCompleteName"].ToString())
                {
                  this.lblMessage.Style.Add("display", "");
                  this.TxtVerificador.Text = string.Empty;
                  this.TxtVerificador.Focus();
                  throw new HandledException(1, "El propietario debe ser el mismo.");
                }
              }
            }
          }
          this.CalculateTotal();
          this.wdgDeliveryMassive.DataSource = (object) dataTable;
        }
        this.wdgDeliveryMassive.DataBind();
        this.Session["dtWastageControlOperation"] = (object) dataTable;
        if (num != 0)
          return;
        this.lblMessage.Style.Add("display", "");
        Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró la solicitud pistoleada."));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void FinalizeDeliver()
    {
      try
      {
        this.wibSave.Enabled = false;
        this.wibReturn.Enabled = false;
        this.btnReturn.Visible = true;
        this.InitialPage();
        this.lblMessage.Style.Add("display", "");
        Message.SetMessage(this.lblMessage, new HandledException(2, "Proceso de Entrega Masiva finalizado de forma correcta."));
      }
      catch (Exception ex)
      {
        throw ex;
      }
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

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private void RefreshParent()
    {
      try
      {
        this.wdgDeliveryMassive.DataSource = (object) (this.Session["dtWastageControlOperation"] as DataTable);
        this.wdgDeliveryMassive.DataBind();
        this.CalculateTotal();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
