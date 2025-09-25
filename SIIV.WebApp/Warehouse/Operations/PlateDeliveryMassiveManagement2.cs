// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.PlateDeliveryMassiveManagement2
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class PlateDeliveryMassiveManagement2 : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected HiddenField hdTramite;
    protected HiddenField hdPropietario;
    protected HiddenField hdddWareHouseControlDetailID;
    protected HtmlTableRow tr1;
    protected RadioButtonList rblPersonTypeOwner;
    protected TextBox TxtVerificador;
    protected FilteredTextBoxExtender TxtVerificador_FilteredTextBoxExtender;
    protected Button btnVerificar;
    protected Button btnFinalizeTmp;
    protected Button btnReturnPopupConfirmation;
    protected GridView wdgDeliveryMassive;
    protected Label lblTotal;
    protected Button btnProcess;
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

    protected void btnFinalizeTmp_Click(object sender, EventArgs e)
    {
      try
      {
        this.InitialPage();
        Message.SetMessage(this.lblMessage, new HandledException(2, "Proceso de Entrega Masivo finalizado de forma correcta."));
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
        if (this.TxtVerificador.Text.Trim() == "")
          return;
        if (this.lblTotal.Text.Trim() == "20")
          throw new HandledException(1, "Solo se pueden agregar un máximo de 20 solicitudes.");
        if (Convert.ToInt64(this.TxtVerificador.Text.Trim()) > (long) int.MaxValue)
          throw new HandledException(1, "La solicitud no existe.");
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - PlateDeliveryMassiveManagement2.aspx");
        string empty = string.Empty;
        string str1 = "";
        string str2 = this.TxtVerificador.Text.Trim();
        if (this.TxtVerificador.Text.Trim().Length > 3)
          str1 = this.TxtVerificador.Text.Trim().Substring(0, 2);
        if (str1 == "03" || str1 == "04" || str1 == "05")
          str2 = this.TxtVerificador.Text.Trim().Substring(2);
        PlateDeliverManagementBL deliverManagementBl = new PlateDeliverManagementBL();
        int result = 1;
        int.TryParse(this.rblPersonTypeOwner.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), out result);
        DataTable dataTable1 = deliverManagementBl.PlateDeliveryRequirementPlateGet(result, str2 == "" ? 0 : Convert.ToInt32(str2, (IFormatProvider) CultureInfo.CurrentCulture), systemUser.i_LocationId);
        DataTable dataTable2 = new DataTable();
        DataTable dataTable3 = this.Session["dtDeliveryControlOperation"] as DataTable;
        if (((IEnumerable<DataRow>) dataTable3.Select("i_RequirementPlateId = '" + dataTable1.Rows[0]["i_RequirementPlateId"].ToString() + "'")).Count<DataRow>() > 0)
        {
          this.lblMessage.Text = "Registro Existe.";
          this.TxtVerificador.Focus();
          this.TxtVerificador.Attributes.Add("onfocusin", " select();");
          throw new HandledException(1, this.lblMessage.Text);
        }
        DataRow row = dataTable3.NewRow();
        row["v_OwnerCompleteName"] = (object) dataTable1.Rows[0]["v_OwnerCompleteName"].ToString();
        row["i_RequirementPlateId"] = dataTable1.Rows[0]["i_RequirementPlateId"];
        row["v_PlateNew"] = (object) dataTable1.Rows[0]["v_PlateNew"].ToString();
        row["i_ProcessTypeId"] = dataTable1.Rows[0]["i_ProcessTypeId"];
        row["v_ProcessType"] = (object) dataTable1.Rows[0]["v_ProcessType"].ToString();
        row["i_StatusId"] = dataTable1.Rows[0]["i_StatusId"];
        row["v_Status"] = (object) dataTable1.Rows[0]["v_Status"].ToString();
        row["i_ProductId"] = dataTable1.Rows[0]["i_ProductId"];
        row["v_ProductName"] = (object) dataTable1.Rows[0]["v_ProductName"].ToString();
        row["i_TotBlank"] = dataTable1.Rows[0]["i_TotBlank"];
        row["i_PlateOldType"] = dataTable1.Rows[0]["i_PlateOldType"];
        dataTable3.Columns["v_Status"].ReadOnly = false;
        if (dataTable1 != null && row["i_RequirementPlateId"].ToString() == str2)
        {
          if (result == 1)
          {
            if (this.hdTramite.Value == string.Empty)
              this.hdTramite.Value = row["v_ProcessType"].ToString();
          }
          else if (this.hdTramite.Value == string.Empty)
          {
            this.hdTramite.Value = row["v_ProcessType"].ToString();
            this.hdPropietario.Value = row["v_OwnerCompleteName"].ToString();
          }
          else if (this.hdPropietario.Value != row["v_OwnerCompleteName"].ToString())
          {
            this.TxtVerificador.Focus();
            this.TxtVerificador.Attributes.Add("onfocusin", " select();");
            throw new HandledException(1, "El propietario debe ser el mismo.");
          }
        }
        this.TxtVerificador.Text = string.Empty;
        this.TxtVerificador.Focus();
        dataTable3.Rows.Add(row);
        this.wdgDeliveryMassive.DataSource = (object) dataTable3;
        this.wdgDeliveryMassive.DataBind();
        this.lblTotal.Text = dataTable3.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.Session["dtDeliveryControlOperation"] = (object) dataTable3;
      }
      catch (HandledException ex)
      {
        this.TxtVerificador.Focus();
        this.TxtVerificador.Attributes.Add("onfocusin", " select();");
        Message.SetMessage(this.lblMessage, new HandledException(1, ex.ErrorMessage));
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void btnProcess_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable dataTable = this.Session["dtDeliveryControlOperation"] as DataTable;
        if (dataTable.Rows.Count < 2)
        {
          Message.SetMessage(this.lblMessage, new HandledException(1, "Debe ingresar, al menos 2 registros a la lista."));
        }
        else
        {
          DataTable table = dataTable.DefaultView.ToTable(true, "v_OwnerCompleteName");
          string str = "0";
          string pstrHeight = "460px";
          if (table.Rows.Count > 1)
            str = "1";
          int result = 0;
          int.TryParse(this.rblPersonTypeOwner.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), out result);
          string empty = string.Empty;
          string pstrUrl = "../../Warehouse/Operations/PlateDeliveryMassiveProvClose.aspx?intTipPers=" + result.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "&intTipProc=5&intProSame=" + str + "&intQuantity=" + dataTable.Rows.Count.ToString();
          if (result == 1)
            pstrHeight = "395px";
          this.CreatePopUpServer("Entrega Masiva", pstrUrl, "490px", pstrHeight);
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

    private void InitialPage()
    {
      try
      {
        this.hdddWareHouseControlDetailID.Value = string.Empty;
        this.hdPropietario.Value = string.Empty;
        this.hdTramite.Value = string.Empty;
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("v_OwnerCompleteName", Type.GetType("System.String"));
        dataTable.Columns.Add("i_RequirementPlateId", Type.GetType("System.Int32"));
        dataTable.Columns.Add("v_PlateNew", Type.GetType("System.String"));
        dataTable.Columns.Add("v_PlateOld", Type.GetType("System.String"));
        dataTable.Columns.Add("i_ProcessTypeId", Type.GetType("System.Int32"));
        dataTable.Columns.Add("v_ProcessType", Type.GetType("System.String"));
        dataTable.Columns.Add("i_StatusId", Type.GetType("System.Int32"));
        dataTable.Columns.Add("v_Status", Type.GetType("System.String"));
        dataTable.Columns.Add("i_ProductId", Type.GetType("System.Int32"));
        dataTable.Columns.Add("v_ProductName", Type.GetType("System.String"));
        dataTable.Columns.Add("i_TotBlank", Type.GetType("System.Int32"));
        dataTable.Columns.Add("i_PlateOldType", Type.GetType("System.Int32"));
        this.wdgDeliveryMassive.DataSource = (object) dataTable;
        this.wdgDeliveryMassive.DataBind();
        this.Session["dtDeliveryControlOperation"] = (object) dataTable;
        this.lblTotal.Text = "0";
        this.TxtVerificador.Text = "";
        this.TxtVerificador.Focus();
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

    protected void rblPersonTypeOwner_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.TxtVerificador.Text = "";
      this.TxtVerificador.Focus();
      if (this.wdgDeliveryMassive.Rows.Count <= 0)
        return;
      if (this.rblPersonTypeOwner.SelectedIndex == 0)
        this.rblPersonTypeOwner.SelectedIndex = 1;
      else
        this.rblPersonTypeOwner.SelectedIndex = 0;
      string empty = string.Empty;
      this.CreatePopUpServer("SIIV - Entrega Masiva Provincia", "../../../UserControls/PopupConfirmation.aspx?MessageTypeId=2&MessageText=¿Se limpiará las solicitudes de placas añadidas en la lista, está seguro?", "360px", "200px");
    }

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      if (this.rblPersonTypeOwner.SelectedIndex == 0)
        this.rblPersonTypeOwner.SelectedIndex = 1;
      else
        this.rblPersonTypeOwner.SelectedIndex = 0;
      this.InitialPage();
    }

    protected void wdgDeliveryMassive_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        int int32 = Convert.ToInt32(e.CommandArgument);
        if (this.wdgDeliveryMassive.Rows[int32] == null)
          throw new HandledException(4, "Error de selección.", "'wdgList' - SystemUserManagement.aspx");
        if (!e.CommandName.Equals("Delete", StringComparison.CurrentCulture))
          return;
        DataTable dataTable = this.Session["dtDeliveryControlOperation"] as DataTable;
        dataTable.Rows[int32].Delete();
        this.wdgDeliveryMassive.DataSource = (object) dataTable;
        this.wdgDeliveryMassive.DataBind();
        this.lblTotal.Text = dataTable.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.Session["dtDeliveryControlOperation"] = (object) dataTable;
        if (dataTable.Rows.Count < 1)
          this.InitialPage();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, ex.ErrorMessage);
      }
    }

    protected void wdgDeliveryMassive_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgDeliveryMassive_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
  }
}
