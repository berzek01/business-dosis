// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Operation.QuarterlyPriceExceptionPopup
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Operation
{
  public class QuarterlyPriceExceptionPopup : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected HtmlTableRow trDepartment;
    protected Label lblDepartamento;
    protected DropDownList wdgTypeVehicle;
    protected HtmlTableRow tr1;
    protected Label Label5;
    protected DropDownList wdgTypeTramite;
    protected TextBox txtPriceSale;
    protected TextBox txtPriceTax;
    protected TextBox txtPriceCost;
    protected Fecha wdpStartDate;
    protected Fecha wdpEndDate;
    protected Label lblMessage1;
    protected Button wibAceptar;
    protected Button wibCancelar;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      string str = "";
      this.ViewState["i_PlateTypeId"] = (object) this.Request.QueryString["SpecialPlateTypeId"].ToString();
      this.LoadParameters();
      this.wdpStartDate.Value = DateTime.Now;
      this.wdpEndDate.Value = new DateTime(DateTime.Now.Year, 12, 31);
      this.wdpEndDate.Enabled = false;
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) (str + SystemParameterGroups.SpecialRequirementType.ToString() + ", " + SystemParameterGroups.ModelTaxValue.ToString()),
        (object) "",
        (object) "1",
        (object) "1"
      });
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (Convert.ToInt32(row["i_GroupId"]) == SystemParameterGroups.SpecialRequirementType)
            this.wdgTypeTramite.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          else if (Convert.ToInt32(row["i_GroupId"]) == SystemParameterGroups.ModelTaxValue)
            this.ViewState["d_TaxValue"] = row["v_Value"];
        }
      }
      this.wdgTypeTramite.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
      this.wdgTypeTramite.SelectedIndex = 0;
      this.Session["OpenSucesfull"] = (object) 0;
    }

    private void LoadParameters()
    {
      try
      {
        int int32 = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = new AcquisitionQueriesBL().SpecialPlateClassVehicleForQuarterlyPricesGet(int32);
        this.wdgTypeVehicle.DataSource = (object) dataTable2;
        this.wdgTypeVehicle.DataTextField = "v_Description";
        this.wdgTypeVehicle.DataValueField = "i_ParameterId";
        this.wdgTypeVehicle.DataBind();
        this.wdgTypeVehicle.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        this.ViewState["dt_Result"] = (object) dataTable2;
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
        DateTime now = this.wdpStartDate.Value;
        int int32_1 = Convert.ToInt32(now.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture));
        now = this.wdpEndDate.Value;
        int int32_2 = Convert.ToInt32(now.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture));
        now = DateTime.Now;
        int int32_3 = Convert.ToInt32(now.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture));
        if (this.wdgTypeVehicle.SelectedIndex == 0)
          Message.SetMessage(this.lblMessage1, new HandledException(1, "Debe seleccionar un tipo de vehículo."));
        else if (this.wdgTypeTramite.SelectedIndex == 0)
          Message.SetMessage(this.lblMessage1, new HandledException(1, "Debe seleccionar un tipo de trámite."));
        else if (this.txtPriceSale.Text.Trim() == "")
          Message.SetMessage(this.lblMessage1, new HandledException(1, "Debe ingresar el precio de venta."));
        else if (this.txtPriceTax.Text.Trim() == "")
          Message.SetMessage(this.lblMessage1, new HandledException(1, "Debe ingresar el costo de impuesto."));
        else if (this.txtPriceCost.Text.Trim() == "")
          Message.SetMessage(this.lblMessage1, new HandledException(1, "Debe ingresar el precio del costo."));
        else if (this.wdpStartDate.Text.ToString() == "" || this.wdpStartDate.Text.ToString() == "__/__/____")
          Message.SetMessage(this.lblMessage1, new HandledException(1, "Debe ingresar un fecha de vigencia."));
        else if (this.wdpEndDate.Text.ToString() == "" || this.wdpEndDate.Text.ToString() == "__/__/____")
          Message.SetMessage(this.lblMessage1, new HandledException(1, "Debe ingresar un fecha de vemcimiento."));
        else if (int32_1 < int32_3)
          Message.SetMessage(this.lblMessage1, new HandledException(1, "La fecha inicio debe ser igual ó mayor al dia actual."));
        else if (int32_2 < int32_1)
        {
          Message.SetMessage(this.lblMessage1, new HandledException(1, "La fecha fin de la excepción debe ser igual ó mayor a la fecha de inicio."));
        }
        else
        {
          SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
          DataTable dataTable1 = new DataTable();
          DataTable dataTable2 = new DataTable();
          DataTable dataTable3 = ((DataTable) this.ViewState["dt_Result"]).AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => x["i_ParameterId"].ToString() == this.wdgTypeVehicle.SelectedValue.ToString())).CopyToDataTable<DataRow>();
          if (!new RequirementManagementBL().QuarterlyPriceExceptionRegister(Convert.ToInt32(this.wdgTypeVehicle.SelectedValue), Convert.ToInt32(this.wdgTypeTramite.SelectedValue), Convert.ToInt32(this.ViewState["i_PlateTypeId"]), Convert.ToInt32(dataTable3.Rows[0]["i_ProductId"].ToString()), Convert.ToDouble(this.txtPriceSale.Text), Convert.ToDouble(this.txtPriceTax.Text), Convert.ToDouble(this.txtPriceCost.Text), this.wdpStartDate.Value, this.wdpEndDate.Value, systemUser.i_SystemUserId))
          {
            Message.SetMessage(this.lblMessage1, new HandledException(-100, "Error al intentar realizar el registro"));
          }
          else
          {
            this.Session["OpenSucesfull"] = (object) 1;
            this.wibCancelar_Click((object) null, (EventArgs) null);
          }
        }
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage1, enmMessageType.Error, ex.Message);
      }
    }

    protected void wibCancelar_Click(object sender, EventArgs e)
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void txtPriceSale_TextChanged(object sender, EventArgs e) => this.CalculateCosts();

    private void CalculateCosts()
    {
      Decimal num1 = this.txtPriceSale.Text.Length == 0 ? 0M : Convert.ToDecimal(this.txtPriceSale.Text);
      this.txtPriceSale.Text = num1.ToString();
      Decimal num2 = Convert.ToDecimal(string.Format("{0:F2}", (object) (num1 / (1M + Convert.ToDecimal(this.ViewState["d_TaxValue"])))));
      this.txtPriceCost.Text = num2.ToString();
      this.txtPriceTax.Text = Convert.ToDecimal(string.Format("{0:F2}", (object) (num1 - num2))).ToString();
    }
  }
}
