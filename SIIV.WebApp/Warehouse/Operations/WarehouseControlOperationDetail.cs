// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.WarehouseControlOperationDetail
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class WarehouseControlOperationDetail : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtId;
    protected TextBox txtPlaca;
    protected DropDownList cboEstado;
    protected DropDownList cboAuxiliar;
    protected TextBox txtObservacion;
    protected Button btnSave;
    protected Button btnClose;
    protected Label lblMsg;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadParameter();
    }

    protected void btnSave_Click(object sender, EventArgs e) => this.SaveRegister();

    protected void cboEstado_SelectionChanged(object sender, EventArgs e)
    {
      this.cboAuxiliar.Enabled = Convert.ToInt32(this.cboEstado.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture) == 3;
      if (Convert.ToInt32(this.cboEstado.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture) == 3)
        return;
      this.cboAuxiliar.SelectedValue = "-1";
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private void LoadParameter()
    {
      List<SIIV.BE.SystemParameter> systemParameterList = new SystemParameterManagementBL().Get((object) new ArrayList()
      {
        (object) "316",
        (object) "",
        (object) "1",
        (object) "1"
      });
      this.cboEstado.Items.Clear();
      foreach (SIIV.BE.SystemParameter systemParameter in systemParameterList)
      {
        if (systemParameter.i_GroupId == 316)
          this.cboEstado.Items.Add(new ListItem(systemParameter.v_Description, systemParameter.i_ParameterId.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
      }
      this.cboEstado.Items.Insert(0, new ListItem(Constants.OPCIONLISTA_Seleccione, Constants.OPCIONLISTA_ValorSinSeleccion));
      DataTable warehouseControlAuxiliar = new WarehouseControlQueriesBL().GetWarehouseControlAuxiliar(((SystemUser) this.Session["SystemUser"]).i_LocationId);
      this.cboAuxiliar.Items.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) warehouseControlAuxiliar.Rows)
        this.cboAuxiliar.Items.Add(new ListItem(row["v_Auxiliar"].ToString(), row["i_AuxiliarId"].ToString()));
      this.cboAuxiliar.Items.Insert(0, new ListItem(Constants.OPCIONLISTA_Seleccione, Constants.OPCIONLISTA_ValorSinSeleccion));
      GridViewRow gridViewRow = (GridViewRow) this.Session["WastageSelectedRowEdit"];
      this.txtId.Text = gridViewRow.Cells[0].Text;
      this.txtPlaca.Text = gridViewRow.Cells[1].Text;
      this.cboEstado.SelectedValue = gridViewRow.Cells[2].Text;
      this.cboAuxiliar.SelectedValue = gridViewRow.Cells[3].Text;
      this.txtObservacion.Text = gridViewRow.Cells[4].Text;
      this.cboAuxiliar.Enabled = Convert.ToInt32(this.cboEstado.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture) == 3;
    }

    private void SaveRegister()
    {
      this.Session["WastageSelectedRowEditArray"] = (object) new ArrayList()
      {
        (object) this.txtId.Text,
        Convert.ToInt32(this.cboEstado.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture) == -1 ? (object) "0" : (object) this.cboEstado.SelectedItem.Value,
        Convert.ToInt32(this.cboEstado.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture) == -1 ? (object) "" : (object) this.cboEstado.SelectedItem.Text,
        Convert.ToInt32(this.cboAuxiliar.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture) == -1 ? (object) "0" : (object) this.cboAuxiliar.SelectedItem.Value,
        Convert.ToInt32(this.cboAuxiliar.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture) == -1 ? (object) "" : (object) this.cboAuxiliar.SelectedItem.Text,
        (object) this.txtObservacion.Text
      };
      string script = "SendInfoPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }
  }
}
