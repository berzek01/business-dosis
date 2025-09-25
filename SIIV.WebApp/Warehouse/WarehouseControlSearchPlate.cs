// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.WarehouseControlSearchPlate
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse
{
  public class WarehouseControlSearchPlate : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtPlacaBusca;
    protected Button wibSearch;
    protected TextBox txtId;
    protected TextBox txtAuxiliar;
    protected TextBox txtCounter;
    protected TextBox txtTipoRegistro;
    protected TextBox txtFecha;
    protected TextBox txtSolicitud;
    protected TextBox txtPlaca;
    protected TextBox txtEstado;
    protected Label lblMsg;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMsg.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.txtPlacaBusca.Text = Convert.ToString(this.Request.QueryString["v_plateControl"], (IFormatProvider) CultureInfo.CurrentCulture);
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.BuscarPlaca();

    protected void BuscarPlaca()
    {
      try
      {
        DataTable controlSearchPlate = new WarehouseControlQueriesBL().GetWarehouseControlSearchPlate(this.txtPlacaBusca.Text);
        if (controlSearchPlate == null)
          return;
        if (controlSearchPlate.Rows.Count > 0)
        {
          this.txtId.Text = controlSearchPlate.Rows[0]["i_WarehouseControlId"].ToString();
          this.txtAuxiliar.Text = controlSearchPlate.Rows[0]["v_UserAux"].ToString();
          this.txtCounter.Text = controlSearchPlate.Rows[0]["v_UserCounter"].ToString();
          this.txtTipoRegistro.Text = controlSearchPlate.Rows[0]["v_DeliveryPlateType"].ToString();
          this.txtFecha.Text = controlSearchPlate.Rows[0]["d_InsertDate"].ToString();
          this.txtSolicitud.Text = controlSearchPlate.Rows[0]["i_RequirementPlateId"].ToString();
          this.txtPlaca.Text = controlSearchPlate.Rows[0]["v_PlateNew"].ToString();
          this.txtEstado.Text = controlSearchPlate.Rows[0]["v_StatusWareHouseControl"].ToString();
        }
        else
        {
          this.txtId.Text = "";
          this.txtAuxiliar.Text = "";
          this.txtCounter.Text = "";
          this.txtTipoRegistro.Text = "";
          this.txtFecha.Text = "";
          this.txtSolicitud.Text = "";
          this.txtPlaca.Text = "";
          this.txtEstado.Text = "";
          throw new HandledException(1, "Nro de Placa No encontrada en el Control de Almacén.");
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }
  }
}
