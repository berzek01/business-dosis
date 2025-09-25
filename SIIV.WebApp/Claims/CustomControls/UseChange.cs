// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.CustomControls.UseChange
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.Claim.BL;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims.CustomControls
{
  public class UseChange : UserControl
  {
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtPlateNew;
    protected TextBox txtTitle;
    protected Label Label5;
    protected Button wibValidate;
    protected Label lblMessage;
    protected TextBox txtModel;
    protected Label Label1;
    protected TextBox txtBrand;
    protected Label Label2;
    protected TextBox txtSeries;
    protected Label Label3;
    protected TextBox txtPlateOld;
    protected Label Label4;
    protected TextBox txtCategory;

    public event UseChange.DatosOK OnDatosOK;

    public event UseChange.DatosWrong OnDatosWrong;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void wibValidate_Click(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.txtPlateNew.Text.Length == 0)
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>Debe ingresar un número de placa");
      else if (this.txtTitle.Text.Length == 0)
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>Debe ingresar un número de título");
      else
        this.Validated();
    }

    public string GetTexts()
    {
      return "" + this.txtPlateNew.Text + "|" + this.txtTitle.Text + "|" + this.txtModel.Text + "|" + this.txtBrand.Text + "|" + this.txtCategory.Text + "|" + this.txtPlateOld.Text + "|" + this.txtSeries.Text + "|" + this.ViewState["i_VehicleId"].ToString();
    }

    public void SetTexts(string strValues)
    {
      if (!(strValues != ""))
        return;
      string[] source = strValues.Split('|');
      if (((IEnumerable<string>) source).Count<string>() > 0)
        this.txtPlateNew.Text = source[0];
      if (((IEnumerable<string>) source).Count<string>() > 1)
        this.txtTitle.Text = source[1];
      if (((IEnumerable<string>) source).Count<string>() > 2)
        this.txtModel.Text = source[2];
      if (((IEnumerable<string>) source).Count<string>() > 3)
        this.txtBrand.Text = source[3];
      if (((IEnumerable<string>) source).Count<string>() > 4)
        this.txtCategory.Text = source[4];
      if (((IEnumerable<string>) source).Count<string>() > 5)
        this.txtPlateOld.Text = source[5];
      if (((IEnumerable<string>) source).Count<string>() > 6)
        this.txtSeries.Text = source[6];
      if (((IEnumerable<string>) source).Count<string>() > 7)
        this.Session["i_VehicleId"] = (object) source[7];
    }

    public void ClearControls()
    {
      this.txtPlateNew.Text = "";
      this.txtTitle.Text = "";
      this.txtModel.Text = "";
      this.txtBrand.Text = "";
      this.txtCategory.Text = "";
      this.txtPlateOld.Text = "";
      this.txtSeries.Text = "";
    }

    public void EnabledControls(bool habilita)
    {
      this.txtPlateNew.Enabled = true;
      this.txtTitle.Enabled = true;
      this.txtModel.Enabled = true;
      this.txtBrand.Enabled = true;
      this.txtCategory.Enabled = true;
      this.txtPlateOld.Enabled = true;
      this.txtSeries.Enabled = true;
    }

    private void Validated()
    {
      string text1 = this.txtPlateNew.Text;
      string text2 = this.txtTitle.Text;
      switch (new RequirementQueriesBL().Verify3rdPlate(text1))
      {
        case -2:
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>El número de placa ya presenta un reclamo en curso");
          if (this.OnDatosWrong == null)
            break;
          this.OnDatosWrong((object) this);
          break;
        case -1:
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>El número de placa no tiene un trámite de placa");
          if (this.OnDatosWrong == null)
            break;
          this.OnDatosWrong((object) this);
          break;
        case 0:
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>El número de placa tiene un trámite abierto");
          if (this.OnDatosWrong == null)
            break;
          this.OnDatosWrong((object) this);
          break;
        default:
          DataTable detailSunarp = new RequirementClaimQueriesBL().GetDetailSUNARP(text1, text2);
          if (detailSunarp == null || detailSunarp.Rows.Count == 0)
          {
            if (this.OnDatosWrong != null)
              this.OnDatosWrong((object) this);
            Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>No se encontró información con los datos ingresados");
            break;
          }
          if (this.OnDatosOK != null)
            this.OnDatosOK((object) this);
          Message.SetMessage(this.lblMessage, enmMessageType.Success, "***Advertencia</br>Dastos ingresados correctamente");
          this.SetearData(detailSunarp);
          break;
      }
    }

    private void SetearData(DataTable dt)
    {
      this.txtModel.Text = dt.Rows[0]["v_Model"].ToString();
      this.txtBrand.Text = dt.Rows[0]["v_Brand"].ToString();
      this.txtSeries.Text = dt.Rows[0]["v_SerialNumber"].ToString();
      this.txtPlateOld.Text = dt.Rows[0]["v_PlateOld"].ToString();
      this.txtCategory.Text = dt.Rows[0]["v_Category"].ToString();
      this.ViewState["i_VehicleId"] = (object) dt.Rows[0]["i_VehicleId"].ToString();
    }

    public delegate void DatosOK(object sender);

    public delegate void DatosWrong(object sender);
  }
}
