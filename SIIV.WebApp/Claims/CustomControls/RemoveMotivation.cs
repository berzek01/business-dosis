// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.CustomControls.RemoveMotivation
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
  public class RemoveMotivation : UserControl
  {
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtRequirementId;
    protected Button wibSearch;
    protected TextBox txtPlateNew;
    protected TextBox txtRequirementType;
    protected TextBox txtZona;
    protected TextBox txtDistrito;
    protected TextBox TxtCourrier;
    protected TextBox TxtMotivo;
    protected TextBox txtObservacion;
    protected TextBox txtMotivacionDate;
    protected Label lblMessage;

    public event RemoveMotivation.DatosOK OnDatosOK;

    public event RemoveMotivation.DatosWrong OnDatosWrong;

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
        DataTable removeMotivationBy = new PlateDeliverQueriesBL().GetClaimRemoveMotivationBy(Convert.ToInt32(this.txtRequirementId.Text, (IFormatProvider) CultureInfo.CurrentCulture));
        if (removeMotivationBy == null || removeMotivationBy.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>No se encontró información con el Número de Solicitud ingresado");
          if (this.OnDatosWrong == null)
            return;
          this.OnDatosWrong((object) this);
        }
        else
        {
          this.txtPlateNew.Text = removeMotivationBy.Rows[0]["v_PlateNew"].ToString();
          this.txtRequirementType.Text = removeMotivationBy.Rows[0]["v_RequirementType"].ToString();
          this.txtZona.Text = removeMotivationBy.Rows[0]["v_Zona"].ToString();
          this.txtDistrito.Text = removeMotivationBy.Rows[0]["v_Distrito"].ToString();
          this.TxtCourrier.Text = removeMotivationBy.Rows[0]["v_Courrier"].ToString();
          this.TxtMotivo.Text = removeMotivationBy.Rows[0]["v_Motive"].ToString();
          this.txtObservacion.Text = removeMotivationBy.Rows[0]["v_ObservationA"].ToString();
          this.txtMotivacionDate.Text = removeMotivationBy.Rows[0]["v_MotivationDate"].ToString();
          this.ViewState["i_RequirementProgramationId"] = removeMotivationBy.Rows[0]["i_RequirementProgramationId"];
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
        return this.txtRequirementId.Text.Length > 0 && this.txtPlateNew.Text.Length > 0 && this.txtRequirementType.Text.Length > 0 && this.txtZona.Text.Length > 0 && this.txtDistrito.Text.Length > 0 && this.TxtCourrier.Text.Length > 0 && this.TxtMotivo.Text.Length > 0 && this.txtObservacion.Text.Length > 0 && this.txtMotivacionDate.Text.Length > 0 && this.ViewState["i_RequirementProgramationId"] != null;
      }
    }

    public string GetTexts()
    {
      return "" + this.Session["i_RequirementPlateId"].ToString() + "|" + this.txtPlateNew.Text + "|" + this.txtRequirementType.Text + "|" + this.txtZona.Text + "|" + this.txtDistrito.Text + "|" + this.TxtCourrier.Text + "|" + this.TxtMotivo.Text + "|" + this.txtObservacion.Text + "|" + this.txtMotivacionDate.Text + "|" + this.ViewState["i_RequirementProgramationId"].ToString();
    }

    public void SetTexts(string strValues)
    {
      if (!(strValues != ""))
        return;
      string[] source = strValues.Split('|');
      if (((IEnumerable<string>) source).Count<string>() > 0)
        this.txtRequirementId.Text = source[0];
      if (((IEnumerable<string>) source).Count<string>() > 1)
        this.txtPlateNew.Text = source[1];
      if (((IEnumerable<string>) source).Count<string>() > 2)
        this.txtRequirementType.Text = source[2];
      if (((IEnumerable<string>) source).Count<string>() > 3)
        this.txtZona.Text = source[3];
      if (((IEnumerable<string>) source).Count<string>() > 4)
        this.txtDistrito.Text = source[4];
      if (((IEnumerable<string>) source).Count<string>() > 5)
        this.TxtCourrier.Text = source[5];
      if (((IEnumerable<string>) source).Count<string>() > 6)
        this.TxtMotivo.Text = source[6];
      if (((IEnumerable<string>) source).Count<string>() > 7)
        this.txtObservacion.Text = source[7];
      if (((IEnumerable<string>) source).Count<string>() > 8)
        this.txtMotivacionDate.Text = source[8];
      if (((IEnumerable<string>) source).Count<string>() > 9)
        this.ViewState["i_RequirementProgramationId"] = (object) source[9];
    }

    public void EnabledControls(bool habilita)
    {
      this.txtRequirementId.Enabled = habilita;
      this.wibSearch.Enabled = habilita;
    }

    public void ClearControls()
    {
      this.txtPlateNew.Text = "";
      this.txtRequirementType.Text = "";
      this.txtZona.Text = "";
      this.txtDistrito.Text = "";
      this.TxtCourrier.Text = "";
      this.TxtMotivo.Text = "";
      this.txtObservacion.Text = "";
      this.txtMotivacionDate.Text = "";
      this.txtPlateNew.Text = "";
      this.ViewState["i_RequirementProgramationId"] = (object) "";
    }

    public delegate void DatosOK(object sender);

    public delegate void DatosWrong(object sender);
  }
}
