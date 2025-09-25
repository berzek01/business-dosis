// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.CustomControls.CallCenter
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Claim.BL;
using SIIV.Common.Resource;
using SIIV.SystemParameter.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims.CustomControls
{
  public class CallCenter : UserControl
  {
    protected UpdatePanel UpdatePanel1;
    protected Label lblRequirement;
    protected TextBox txtRequirement;
    protected Label Label1;
    protected DropDownList wddClaimMotive;
    protected Label lblOperationType;
    protected TextBox txtPlateNumber;
    protected Label lblOperationDate;
    protected TextBox txtProcessed;
    protected Label lblTerminal;
    protected TextBox txtProduct;
    protected Label lblUserId;
    protected Fecha wdpRegisterDate;
    protected Button wibValidate;
    protected Label lblMessage;

    public event CallCenter.DatosOK OnDatosOK;

    public event CallCenter.DatosWrong OnDatosWrong;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      this.LoadParameters();
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.txtRequirement.Text.Length == 0)
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>Falta ingresar el Número de Solicitud");
      else if (this.wddClaimMotive.SelectedValue == "" || this.wddClaimMotive.SelectedValue == "-1")
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>Debe seleccionar el motivo del reclamo");
      else
        this.Validated();
    }

    public int i_ClaimMotiveId
    {
      get
      {
        return this.wddClaimMotive.SelectedValue == "" ? 0 : Convert.ToInt32(this.wddClaimMotive.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      }
    }

    private void LoadParameters()
    {
      DataTable motive = new RequirementClaimQueriesBL().GetMotive(7);
      this.wddClaimMotive.Items.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) motive.Rows)
        this.wddClaimMotive.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
      this.wddClaimMotive.Items.Insert(0, new ListItem(Constants.OPCIONLISTA_Seleccione, Constants.OPCIONLISTA_ValorSinSeleccion));
    }

    private void Validated()
    {
      DataTable detailRequirement = new RequirementClaimQueriesBL().GetDetailRequirement(Convert.ToInt32(this.txtRequirement.Text, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.wddClaimMotive.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
      if (this.ValidationMessage(detailRequirement))
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Success, "[Datos Correctos]");
        this.Session["i_RequirementPlateId"] = detailRequirement.Rows[0]["i_RequirementPlateId"];
        if (this.OnDatosOK != null)
          this.OnDatosOK((object) this);
        this.SetearData(detailRequirement);
      }
      else
      {
        if (this.OnDatosWrong == null)
          return;
        this.OnDatosWrong((object) this);
      }
    }

    private void SetearData(DataTable dt)
    {
      this.txtPlateNumber.Text = dt.Rows[0]["v_PlateNumber"].ToString();
      this.txtProcessed.Text = dt.Rows[0]["v_ProcessType"].ToString();
      this.txtProduct.Text = dt.Rows[0]["v_ProductName"].ToString();
      this.wdpRegisterDate.Value = Convert.IsDBNull(dt.Rows[0]["d_RegisterDate"]) ? DateTime.Now : Convert.ToDateTime(dt.Rows[0]["d_RegisterDate"]);
    }

    public bool CompletedData
    {
      get
      {
        return this.txtRequirement.Text.Length > 0 && this.txtPlateNumber.Text.Length > 0 && this.txtProcessed.Text.Length > 0 && this.txtProduct.Text.Length > 0 && this.wddClaimMotive.SelectedValue != "-1";
      }
    }

    private bool ValidationMessage(DataTable dt)
    {
      if (dt == null || dt.Rows.Count == 0)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>No se encontró ninguna Solicitud con el dato ingresado");
        return false;
      }
      int? nullable = Convert.IsDBNull(dt.Rows[0]["i_DataBankId"]) ? new int?() : new int?(Convert.ToInt32(dt.Rows[0]["i_DataBankId"], (IFormatProvider) CultureInfo.CurrentCulture));
      int int32_1 = Convert.ToInt32(dt.Rows[0]["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture);
      int int32_2 = Convert.ToInt32(dt.Rows[0]["i_StatusPayment"], (IFormatProvider) CultureInfo.CurrentCulture);
      if (!nullable.HasValue)
      {
        if (this.wddClaimMotive.SelectedValue == "19")
        {
          if (int32_1 == 0 && int32_2 != 0)
          {
            Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>La solicitud debe estar pendiente de acreditación para realizar este reclamo");
            return false;
          }
        }
        else if (int32_1 != 4)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>La solicitud debe estar con estado Por Recoger");
          return false;
        }
      }
      else if (nullable.HasValue)
      {
        if (this.wddClaimMotive.SelectedValue == "19")
        {
          if (int32_1 != 1)
          {
            Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>La solicitud debe estar acreditada para realizar este reclamo");
            return false;
          }
        }
        else if (int32_1 != 4)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>La solicitud debe estar con estado Por Recoger");
          return false;
        }
      }
      return true;
    }

    public string GetTexts()
    {
      return "" + this.txtRequirement.Text + "|" + this.txtPlateNumber.Text + "|" + this.txtProcessed.Text + "|" + this.txtProduct.Text + "|" + this.wdpRegisterDate.Value.ToString() + "|" + this.wddClaimMotive.SelectedValue;
    }

    public void SetTexts(string strValues)
    {
      string[] source = strValues.Split('|');
      if (((IEnumerable<string>) source).Count<string>() > 0)
        this.txtRequirement.Text = source[0];
      if (((IEnumerable<string>) source).Count<string>() > 1)
        this.txtPlateNumber.Text = source[1];
      if (((IEnumerable<string>) source).Count<string>() > 2)
        this.txtProcessed.Text = source[2];
      if (((IEnumerable<string>) source).Count<string>() > 3)
        this.txtProduct.Text = source[3];
      if (((IEnumerable<string>) source).Count<string>() > 4 && (source[4] != null || source[4] != ""))
        this.wdpRegisterDate.Value = Convert.ToDateTime(source[4]);
      if (((IEnumerable<string>) source).Count<string>() <= 5)
        return;
      this.wddClaimMotive.SelectedValue = source[5];
    }

    public void ClearControls()
    {
      this.txtRequirement.Text = "";
      this.txtPlateNumber.Text = "";
      this.txtProcessed.Text = "";
      this.txtProduct.Text = "";
      this.wdpRegisterDate.Value = DateTime.Now;
      this.wddClaimMotive.SelectedIndex = 0;
      this.lblMessage.Text = "";
    }

    public void EnabledControls(bool enabled)
    {
      this.txtRequirement.Enabled = enabled;
      this.txtPlateNumber.Enabled = enabled;
      this.txtProcessed.Enabled = enabled;
      this.txtProduct.Enabled = enabled;
      this.wdpRegisterDate.Enabled = enabled;
      this.wddClaimMotive.Enabled = enabled;
      this.wibValidate.Enabled = enabled;
    }

    public delegate void DatosOK(object sender);

    public delegate void DatosWrong(object sender);
  }
}
