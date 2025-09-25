// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.PlateDeliveryMassiveDetail
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class PlateDeliveryMassiveDetail : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected DropDownList cboAuxiliar;
    protected TextBox txtId;
    protected TextBox txtPlaca;
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

    protected void btnSave_Click(object sender, EventArgs e) => this.SaveProcess();

    protected void btnClose_Click(object sender, EventArgs e)
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private void LoadParameter()
    {
      this.cboAuxiliar.Items.Insert(0, new ListItem(Constants.OPCIONLISTA_Seleccione, Constants.OPCIONLISTA_ValorSinSeleccion));
      int int32 = Convert.ToInt32(this.Request.QueryString["UserAuxId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
      this.cboAuxiliar.Items.Add(new ListItem(this.Request.QueryString["UserAux"].ToString((IFormatProvider) CultureInfo.CurrentCulture), int32.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
      this.cboAuxiliar.SelectedIndex = 1;
      GridViewRow gridViewRow = (GridViewRow) this.Session["DeliveryMassive"];
      this.txtId.Text = gridViewRow.Cells[2].Text;
      this.txtPlaca.Text = gridViewRow.Cells[3].Text;
    }

    private void SaveProcess()
    {
      try
      {
        DataTable dataTable = this.Session["dtWastageControlOperation"] as DataTable;
        dataTable.Columns["v_Status"].ReadOnly = false;
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["i_RequirementPlateId"].ToString() == this.txtId.Text.ToString((IFormatProvider) CultureInfo.CurrentCulture))
            {
              row["v_Status"] = (object) "RETORNADO";
              row["i_ReturnUserAuxId"] = (object) this.cboAuxiliar.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture);
              row["v_Observation"] = (object) this.txtObservacion.Text.ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim();
              int result1;
              int.TryParse(((TableRow) this.Session["DeliveryMassive"]).Cells[11].Text, out result1);
              int result2;
              int.TryParse(this.cboAuxiliar.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), out result2);
              string pstrObservation = this.txtObservacion.Text.Trim();
              new PlateDeliverManagementBL().PlateDeliveryMassiveReturnUpdate(result1, pstrObservation, result2);
            }
          }
        }
        this.Session["dtWastageControlOperation"] = (object) dataTable;
        string script = "SendInfoPopup();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, enmMessageType.Error, ex.Message);
      }
    }
  }
}
