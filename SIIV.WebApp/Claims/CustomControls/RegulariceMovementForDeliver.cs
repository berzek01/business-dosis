// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.CustomControls.RegulariceMovementForDeliver
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

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
  public class RegulariceMovementForDeliver : UserControl
  {
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtRequirementId;
    protected Button wibSearch;
    protected TextBox txtContributor;
    protected TextBox txtPlateNew;
    protected TextBox txtTitle;
    protected TextBox txtTramite;
    protected TextBox txtRequirementType;
    protected TextBox txtRegisterDate;
    protected Label lblMessage;

    public event RegulariceMovementForDeliver.DatosOK OnDatosOK;

    public event RegulariceMovementForDeliver.DatosWrong OnDatosWrong;

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
        DataTable forDeliverItemBy = new PlateDeliverQueriesBL().GetClaimRegulariceMovementForDeliverItemBy(Convert.ToInt32(this.txtRequirementId.Text, (IFormatProvider) CultureInfo.CurrentCulture));
        if (forDeliverItemBy == null || forDeliverItemBy.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>No se encontró información con el Número de Solicitud ingresado");
          if (this.OnDatosWrong == null)
            return;
          this.OnDatosWrong((object) this);
        }
        else
        {
          this.txtContributor.Text = forDeliverItemBy.Rows[0]["v_CompleteName"].ToString();
          this.txtPlateNew.Text = forDeliverItemBy.Rows[0]["v_PlateNew"].ToString();
          this.txtTitle.Text = forDeliverItemBy.Rows[0]["v_TitleNumber"].ToString();
          this.txtRequirementType.Text = forDeliverItemBy.Rows[0]["v_RequirementType"].ToString();
          this.txtTramite.Text = forDeliverItemBy.Rows[0]["v_TypeProcessed"].ToString();
          this.txtRegisterDate.Text = forDeliverItemBy.Rows[0]["d_InsertDate"].ToString();
          this.ViewState["i_ProductId"] = forDeliverItemBy.Rows[0]["i_ProductId"];
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
        return this.txtRequirementId.Text.Length > 0 && this.txtContributor.Text.Length > 0 && this.txtTramite.Text.Length > 0 && this.txtPlateNew.Text.Length > 0 && this.ViewState["i_ProductId"] != null;
      }
    }

    public string GetTexts()
    {
      return "" + this.txtRequirementId.Text + "|" + this.txtContributor.Text + "|" + this.txtTramite.Text + "|" + this.txtPlateNew.Text + "|" + this.txtTitle.Text + "|" + this.txtRequirementType.Text + "|" + this.txtRegisterDate.Text + "|" + this.ViewState["i_ProductId"].ToString();
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
        this.txtRegisterDate.Text = source[6];
      if (((IEnumerable<string>) source).Count<string>() > 7)
        this.ViewState["i_ProductId"] = (object) source[7];
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
      this.txtRegisterDate.Text = "";
      this.ViewState["i_ProductId"] = (object) "";
    }

    public delegate void DatosOK(object sender);

    public delegate void DatosWrong(object sender);
  }
}
