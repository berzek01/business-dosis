// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.CustomControls.EnablePaymentCode
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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims.CustomControls
{
  public class EnablePaymentCode : UserControl
  {
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtPaymentCode;
    protected Button wibSearch;
    protected HtmlTableRow section1;
    protected TextBox txtPlateNew;
    protected TextBox txtTitle;
    protected HtmlTableRow section2;
    protected TextBox TxtRequirement;
    protected TextBox txtTramite;
    protected TextBox txtRequirementType;
    protected TextBox TxtPriceSale;
    protected TextBox txtRegisterDate;
    protected TextBox txtExpirationDate;
    protected TextBox txtObservacion;
    protected Label lblMessage;

    public event EnablePaymentCode.DatosOK OnDatosOK;

    public event EnablePaymentCode.DatosWrong OnDatosWrong;

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
      if (this.txtPaymentCode.Text.Length == 0)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>Debe ingresar un Código de Pago");
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
        DataTable enablePaymentCodeBy = new PlateDeliverQueriesBL().GetEnablePaymentCodeBy(Convert.ToString(this.txtPaymentCode.Text, (IFormatProvider) CultureInfo.CurrentCulture));
        if (enablePaymentCodeBy == null || enablePaymentCodeBy.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>No se encontró información con el Número de Solicitud ingresado");
          if (this.OnDatosWrong == null)
            return;
          this.OnDatosWrong((object) this);
        }
        else
        {
          this.txtPlateNew.Text = enablePaymentCodeBy.Rows[0]["v_PlateNew"].ToString();
          this.txtTitle.Text = enablePaymentCodeBy.Rows[0]["v_TitleNumber"].ToString();
          this.TxtRequirement.Text = enablePaymentCodeBy.Rows[0]["i_RequirementPlateId"].ToString();
          this.txtRequirementType.Text = enablePaymentCodeBy.Rows[0]["v_RequirementType"].ToString();
          this.txtTramite.Text = enablePaymentCodeBy.Rows[0]["v_TypeProcessed"].ToString();
          this.TxtPriceSale.Text = "S/." + Convert.ToDouble(enablePaymentCodeBy.Rows[0]["f_PriceTotal"]).ToString("0.00");
          this.txtRegisterDate.Text = enablePaymentCodeBy.Rows[0]["d_InsertDate"].ToString();
          this.txtExpirationDate.Text = enablePaymentCodeBy.Rows[0]["d_ExpirationDate"].ToString();
          this.txtObservacion.Text = enablePaymentCodeBy.Rows[0]["v_Observations"].ToString();
          this.ViewState["i_RequirementPlateTypeId"] = enablePaymentCodeBy.Rows[0]["i_RequirementPlateTypeId"];
          this.ViewState["f_Quantity"] = enablePaymentCodeBy.Rows[0]["f_Quantity"];
          this.ViewState["i_RequirementId"] = enablePaymentCodeBy.Rows[0]["i_RequirementId"];
          this.Session["i_RequirementId"] = enablePaymentCodeBy.Rows[0]["i_RequirementId"];
          if (Convert.ToInt32(enablePaymentCodeBy.Rows[0]["i_RequirementPlateTypeId"]) != 1 && Convert.ToInt32(enablePaymentCodeBy.Rows[0]["f_Quantity"]) > 1)
          {
            this.section1.Visible = false;
            this.section2.Visible = false;
          }
          else
          {
            this.section1.Visible = true;
            this.section2.Visible = true;
          }
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
        return this.txtTitle.Text.Length > 0 && this.TxtRequirement.Text.Length > 0 && this.txtRequirementType.Text.Length > 0 && this.txtTramite.Text.Length > 0 && this.TxtPriceSale.Text.Length > 0 && this.txtRegisterDate.Text.Length > 0 && this.txtExpirationDate.Text.Length > 0 && this.txtObservacion.Text.Length > 0 && this.txtPlateNew.Text.Length > 0 && this.ViewState["f_Quantity"] != null && this.ViewState["i_RequirementId"] != null && this.ViewState["i_RequirementPlateTypeId"] != null;
      }
    }

    public string GetTexts()
    {
      return "" + this.txtPaymentCode.Text + "|" + this.TxtRequirement.Text + "|" + this.txtTramite.Text + "|" + this.txtPlateNew.Text + "|" + this.txtTitle.Text + "|" + this.txtRequirementType.Text + "|" + this.TxtPriceSale.Text + "|" + this.txtRegisterDate.Text + "|" + this.txtExpirationDate.Text + "|" + this.txtObservacion.Text + "|" + this.ViewState["i_RequirementPlateTypeId"]?.ToString() + "|" + this.ViewState["f_Quantity"]?.ToString() + "|" + this.ViewState["i_RequirementId"].ToString();
    }

    public void SetTexts(string strValues)
    {
      if (!(strValues != ""))
        return;
      string[] source = strValues.Split('|');
      if (((IEnumerable<string>) source).Count<string>() > 0)
        this.txtPaymentCode.Text = source[0];
      if (((IEnumerable<string>) source).Count<string>() > 1)
        this.TxtRequirement.Text = source[1];
      if (((IEnumerable<string>) source).Count<string>() > 2)
        this.txtTramite.Text = source[2];
      if (((IEnumerable<string>) source).Count<string>() > 3)
        this.txtPlateNew.Text = source[3];
      if (((IEnumerable<string>) source).Count<string>() > 4)
        this.txtTitle.Text = source[4];
      if (((IEnumerable<string>) source).Count<string>() > 5)
        this.txtRequirementType.Text = source[5];
      if (((IEnumerable<string>) source).Count<string>() > 6)
        this.TxtPriceSale.Text = source[6];
      if (((IEnumerable<string>) source).Count<string>() > 7)
        this.txtRegisterDate.Text = source[7];
      if (((IEnumerable<string>) source).Count<string>() > 8)
        this.txtExpirationDate.Text = source[8];
      if (((IEnumerable<string>) source).Count<string>() > 9)
        this.txtObservacion.Text = source[9];
      if (((IEnumerable<string>) source).Count<string>() > 10)
        this.ViewState["i_RequirementPlateTypeId"] = (object) source[10];
      if (((IEnumerable<string>) source).Count<string>() > 11)
        this.ViewState["f_Quantity"] = (object) source[11];
      if (((IEnumerable<string>) source).Count<string>() > 12)
        this.ViewState["i_RequirementId"] = (object) source[12];
      if (Convert.ToInt32(this.ViewState["i_RequirementPlateTypeId"]) != 1 && Convert.ToInt32(this.ViewState["f_Quantity"]) > 1)
      {
        this.section1.Visible = false;
        this.section2.Visible = false;
      }
      else
      {
        this.section1.Visible = true;
        this.section2.Visible = true;
      }
    }

    public void EnabledControls(bool habilita)
    {
      this.txtPaymentCode.Enabled = habilita;
      this.wibSearch.Enabled = habilita;
    }

    public void ClearControls()
    {
      this.txtTramite.Text = "";
      this.txtPlateNew.Text = "";
      this.txtTitle.Text = "";
      this.TxtRequirement.Text = "";
      this.txtRequirementType.Text = "";
      this.TxtPriceSale.Text = "";
      this.txtRegisterDate.Text = "";
      this.txtExpirationDate.Text = "";
      this.txtObservacion.Text = "";
      this.ViewState["i_RequirementPlateTypeId"] = (object) "";
      this.ViewState["f_Quantity"] = (object) "";
      this.ViewState["i_RequirementId"] = (object) "";
      this.section1.Visible = false;
      this.section2.Visible = false;
    }

    public delegate void DatosOK(object sender);

    public delegate void DatosWrong(object sender);
  }
}
