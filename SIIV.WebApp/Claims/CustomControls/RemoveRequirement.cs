// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.CustomControls.RemoveRequirement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
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
  public class RemoveRequirement : UserControl
  {
    protected UpdatePanel UpdatePanel1;
    protected Label lblMotivo;
    protected DropDownList wddMotive;
    protected HtmlTableCell tagRequired;
    protected RadioButton rbAnular;
    protected RadioButton rbEliminar;
    protected TextBox txtPaymentCode;
    protected Button wibValidate;
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
    protected Label lblMessage;

    public event RemoveRequirement.DatosOK OnDatosOK;

    public event RemoveRequirement.DatosWrong OnDatosWrong;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadParameters();
      this.tagRequired.Visible = false;
      this.rbAnular.Visible = false;
      this.rbEliminar.Visible = false;
    }

    private void LoadParameters()
    {
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.CorrectionRequirementRegularizationMotive.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      this.wddMotive.Items.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (Convert.ToInt32(row["i_ParameterId"], (IFormatProvider) CultureInfo.CurrentCulture) == 1)
          this.wddMotive.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        else if (Convert.ToInt32(row["i_ParameterId"], (IFormatProvider) CultureInfo.CurrentCulture) == 3)
          this.wddMotive.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        else if (Convert.ToInt32(row["i_ParameterId"], (IFormatProvider) CultureInfo.CurrentCulture) == 9)
          this.wddMotive.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
      }
      this.wddMotive.Items.Insert(0, new ListItem(Constants.OPCIONLISTA_Seleccione, Constants.OPCIONLISTA_ValorSinSeleccion));
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
      try
      {
        this.lblMessage.Visible = false;
        if (this.wddMotive.SelectedValue == "-1")
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>Seleccione el Motivo de la regularización.");
        else if (!this.rbAnular.Checked && !this.rbEliminar.Checked)
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>Seleccione la acción a realizar.");
        else if (this.txtPaymentCode.Text.Length == 0)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>Debe ingresar un Código de Pago");
          if (this.OnDatosWrong == null)
            return;
          this.OnDatosWrong((object) this);
        }
        else
        {
          this.ClearControls();
          this.Validated();
        }
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

    private void Validated() => this.ShowPlateDeliverItem();

    public int i_ClaimMotiveId
    {
      get
      {
        return this.wddMotive.SelectedValue == "" ? 0 : Convert.ToInt32(this.wddMotive.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      }
    }

    public void SetMotive(int? i_claimmotiveid)
    {
      this.wddMotive.SelectedValue = i_claimmotiveid.ToString();
    }

    private void ShowPlateDeliverItem()
    {
      try
      {
        string pstrPaymentCode = Convert.ToString(this.txtPaymentCode.Text, (IFormatProvider) CultureInfo.CurrentCulture);
        int int32 = Convert.ToInt32(this.wddMotive.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        int num = this.rbAnular.Checked ? 1 : 0;
        DataTable removeRequirementBy = new PlateDeliverQueriesBL().GetRemoveRequirementBy(pstrPaymentCode, int32);
        if (removeRequirementBy == null || removeRequirementBy.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "***Advertencia</br>No se encontró información con el código de pago ingresado");
          if (this.OnDatosWrong == null)
            return;
          this.OnDatosWrong((object) this);
        }
        else
        {
          this.txtPlateNew.Text = removeRequirementBy.Rows[0]["v_PlateNew"].ToString();
          this.txtTitle.Text = removeRequirementBy.Rows[0]["v_TitleNumber"].ToString();
          this.TxtRequirement.Text = removeRequirementBy.Rows[0]["i_RequirementPlateId"].ToString();
          this.txtRequirementType.Text = removeRequirementBy.Rows[0]["v_RequirementType"].ToString();
          this.txtTramite.Text = removeRequirementBy.Rows[0]["v_TypeProcessed"].ToString();
          this.TxtPriceSale.Text = "S/." + Convert.ToDouble(removeRequirementBy.Rows[0]["f_PriceTotal"]).ToString("0.00");
          this.txtRegisterDate.Text = removeRequirementBy.Rows[0]["d_InsertDate"].ToString();
          this.txtExpirationDate.Text = removeRequirementBy.Rows[0]["d_ExpirationDate"].ToString();
          this.ViewState["i_RequirementPlateTypeId"] = removeRequirementBy.Rows[0]["i_RequirementPlateTypeId"];
          this.ViewState["f_Quantity"] = removeRequirementBy.Rows[0]["f_Quantity"];
          this.ViewState["i_RequirementId"] = removeRequirementBy.Rows[0]["i_RequirementId"];
          this.ViewState["i_Motive"] = (object) Convert.ToInt32(this.wddMotive.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
          this.ViewState["i_OperationType"] = (object) (this.rbAnular.Checked ? 1 : 0);
          this.Session["i_RequirementId"] = removeRequirementBy.Rows[0]["i_RequirementId"];
          if (Convert.ToInt32(removeRequirementBy.Rows[0]["i_RequirementPlateTypeId"]) != 1 && Convert.ToInt32(removeRequirementBy.Rows[0]["f_Quantity"]) > 1)
          {
            this.section1.Visible = false;
            this.section2.Visible = false;
          }
          else
          {
            this.section1.Visible = true;
            this.section2.Visible = true;
          }
          if (this.OnDatosOK != null)
            this.OnDatosOK((object) this);
          this.EnabledControlsRegister(false);
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
        return this.txtTitle.Text.Length > 0 && this.TxtRequirement.Text.Length > 0 && this.txtRequirementType.Text.Length > 0 && this.txtTramite.Text.Length > 0 && this.TxtPriceSale.Text.Length > 0 && this.txtRegisterDate.Text.Length > 0 && this.txtExpirationDate.Text.Length > 0 && this.txtPlateNew.Text.Length > 0 && this.ViewState["f_Quantity"] != null && this.ViewState["i_RequirementId"] != null && this.ViewState["i_Motive"] != null && this.ViewState["i_OperationType"] != null && this.ViewState["i_RequirementPlateTypeId"] != null;
      }
    }

    public string GetTexts()
    {
      return "" + this.txtPaymentCode.Text + "|" + this.TxtRequirement.Text + "|" + this.txtTramite.Text + "|" + this.txtPlateNew.Text + "|" + this.txtTitle.Text + "|" + this.txtRequirementType.Text + "|" + this.TxtPriceSale.Text + "|" + this.txtRegisterDate.Text + "|" + this.txtExpirationDate.Text + "|" + this.ViewState["i_RequirementPlateTypeId"]?.ToString() + "|" + this.ViewState["f_Quantity"]?.ToString() + "|" + this.ViewState["i_Motive"]?.ToString() + "|" + this.ViewState["i_OperationType"]?.ToString() + "|" + this.ViewState["i_RequirementId"].ToString();
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
        this.ViewState["i_RequirementPlateTypeId"] = (object) source[9];
      if (((IEnumerable<string>) source).Count<string>() > 10)
        this.ViewState["f_Quantity"] = (object) source[10];
      if (((IEnumerable<string>) source).Count<string>() > 11)
      {
        this.wddMotive.SelectedValue = source[11];
        this.tagRequired.Visible = true;
        if (this.wddMotive.SelectedValue == "1")
        {
          this.rbAnular.Visible = true;
          this.rbEliminar.Visible = false;
        }
        else
        {
          this.rbAnular.Visible = true;
          this.rbEliminar.Visible = true;
        }
      }
      if (((IEnumerable<string>) source).Count<string>() > 12)
      {
        this.rbAnular.Checked = source[12] == "1";
        this.rbEliminar.Checked = !(source[12] == "1");
      }
      if (((IEnumerable<string>) source).Count<string>() > 13)
        this.ViewState["i_RequirementId"] = (object) source[13];
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
      this.wibValidate.Enabled = habilita;
      this.wddMotive.Enabled = habilita;
      this.rbAnular.Enabled = habilita;
      this.rbEliminar.Enabled = habilita;
    }

    public void EnabledControlsRegister(bool habilita)
    {
      this.txtPaymentCode.Enabled = habilita;
      this.wddMotive.Enabled = habilita;
      this.rbAnular.Enabled = habilita;
      this.rbEliminar.Enabled = habilita;
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
      this.ViewState["i_RequirementPlateTypeId"] = (object) "";
      this.ViewState["f_Quantity"] = (object) "";
      this.ViewState["i_RequirementId"] = (object) "";
      this.ViewState["i_Motive"] = (object) "";
      this.ViewState["i_OperationType"] = (object) "";
    }

    protected void wddMotive_SelectionChanged(object sender, EventArgs e)
    {
      if (this.wddMotive.SelectedValue == "-1" || this.wddMotive.SelectedValue == "")
        return;
      if (this.wddMotive.SelectedValue == "1")
      {
        this.tagRequired.Visible = true;
        this.rbAnular.Visible = true;
        this.rbEliminar.Visible = false;
      }
      else
      {
        this.tagRequired.Visible = true;
        this.rbAnular.Visible = true;
        this.rbEliminar.Visible = true;
      }
    }

    public delegate void DatosOK(object sender);

    public delegate void DatosWrong(object sender);
  }
}
