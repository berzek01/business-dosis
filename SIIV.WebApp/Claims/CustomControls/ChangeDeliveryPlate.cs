// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.CustomControls.ChangeDeliveryPlate
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
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
  public class ChangeDeliveryPlate : UserControl
  {
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtRequirementId;
    protected Button wibSearch;
    protected TextBox txtContributor;
    protected TextBox txtTramite;
    protected TextBox txtPlateNew;
    protected TextBox txtTitle;
    protected TextBox txtRequirementType;
    protected TextBox txtDeliveryDate;
    protected Label lblMessage;

    public event ChangeDeliveryPlate.DatosOK OnDatosOK;

    public event ChangeDeliveryPlate.DatosWrong OnDatosWrong;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        this.ClearControls();
        this.Validated();
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

    private void Validated()
    {
      this.lblMessage.Visible = false;
      if (this.txtRequirementId.Text.Length == 0)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>Debe ingresar el Número de Solicitud");
        if (this.OnDatosWrong == null)
          return;
        this.OnDatosWrong((object) this);
      }
      else
        this.ShowPlateDeliverItem();
    }

    private void ShowPlateDeliverItem()
    {
      try
      {
        DataTable plateDeliverItemBy = new PlateDeliverQueriesBL().GetClaimPlateDeliverItemBy(Convert.ToInt32(this.txtRequirementId.Text, (IFormatProvider) CultureInfo.CurrentCulture));
        if (plateDeliverItemBy == null || plateDeliverItemBy.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>No se encontró información con el Número de Solicitud ingresado");
          if (this.OnDatosWrong == null)
            return;
          this.OnDatosWrong((object) this);
        }
        else
        {
          this.txtContributor.Text = plateDeliverItemBy.Rows[0]["v_CompleteName"].ToString();
          this.txtPlateNew.Text = plateDeliverItemBy.Rows[0]["v_PlateNew"].ToString();
          this.txtTitle.Text = plateDeliverItemBy.Rows[0]["v_TitleNumber"].ToString();
          this.txtRequirementType.Text = plateDeliverItemBy.Rows[0]["v_RequirementType"].ToString();
          this.txtTramite.Text = plateDeliverItemBy.Rows[0]["v_ProcessTypeSunarp"].ToString();
          this.txtDeliveryDate.Text = plateDeliverItemBy.Rows[0]["d_InsertDate"].ToString();
          this.ViewState["i_DeliveryDataId"] = plateDeliverItemBy.Rows[0]["i_DeliveryDataId"];
          this.Session["i_RequirementPlateId"] = (object) this.txtRequirementId.Text;
          if (this.OnDatosOK == null)
            return;
          this.OnDatosOK((object) this);
        }
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, "<br>" + ex.Message + "<br>&nbsp;&nbsp;");
      }
    }

    public bool CompletedData
    {
      get
      {
        return this.txtRequirementId.Text.Length > 0 && this.txtContributor.Text.Length > 0 && this.txtTramite.Text.Length > 0 && this.txtPlateNew.Text.Length > 0 && this.ViewState["i_DeliveryDataId"] != null;
      }
    }

    public string GetTexts()
    {
      return "" + this.txtRequirementId.Text + "|" + this.txtContributor.Text + "|" + this.txtTramite.Text + "|" + this.txtPlateNew.Text + "|" + this.txtTitle.Text + "|" + this.txtRequirementType.Text + "|" + this.txtDeliveryDate.Text + "|" + this.ViewState["i_DeliveryDataId"].ToString();
    }

    public void SetTexts(string strValues)
    {
      if (!(strValues != ""))
        return;
      string[] source = strValues.Split('|');
      if (((IEnumerable<string>) source).Count<string>() > 0)
        this.txtRequirementId.Text = source[0];
      if (((IEnumerable<string>) source).Count<string>() > 1)
        this.txtContributor.Text = source[1];
      if (((IEnumerable<string>) source).Count<string>() > 2)
        this.txtTramite.Text = source[2];
      if (((IEnumerable<string>) source).Count<string>() > 3)
        this.txtPlateNew.Text = source[3];
      if (((IEnumerable<string>) source).Count<string>() > 4)
        this.txtTitle.Text = source[4];
      if (((IEnumerable<string>) source).Count<string>() > 5)
        this.txtRequirementType.Text = source[5];
      if (((IEnumerable<string>) source).Count<string>() > 6)
        this.txtDeliveryDate.Text = source[6];
      if (((IEnumerable<string>) source).Count<string>() > 7)
        this.ViewState["i_DeliveryDataId"] = (object) source[7];
    }

    public void EnabledControls(bool habilita)
    {
      this.txtRequirementId.Enabled = habilita;
      this.wibSearch.Enabled = habilita;
    }

    public void ClearControls()
    {
      this.txtContributor.Text = "";
      this.txtTramite.Text = "";
      this.txtPlateNew.Text = "";
      this.txtTitle.Text = "";
      this.txtRequirementType.Text = "";
      this.txtDeliveryDate.Text = "";
      this.ViewState["i_DeliveryDataId"] = (object) "";
    }

    public delegate void DatosOK(object sender);

    public delegate void DatosWrong(object sender);
  }
}
