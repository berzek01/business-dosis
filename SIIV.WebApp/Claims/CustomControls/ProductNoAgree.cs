// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.CustomControls.ProductNoAgree
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Claim.BL;
using SIIV.Product.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims.CustomControls
{
  public class ProductNoAgree : UserControl
  {
    protected Label lblRequirementPlateId;
    protected TextBox txtRequirementPlateId;
    protected Label lblPlateNumber;
    protected TextBox txtPlateNumber;
    protected Label lblProcessType;
    protected DropDownList wddProcessType;
    protected Label lblProductCurrent;
    protected DropDownList wddProductCurrent;
    protected Label lblProductNew;
    protected DropDownList wddProductNew;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        ;
    }

    public int i_RequirementPlateId
    {
      get
      {
        return Convert.ToInt32(this.txtRequirementPlateId.Text, (IFormatProvider) CultureInfo.CurrentCulture);
      }
    }

    public bool IsEnabledProductNew => Convert.ToBoolean(this.wddProductNew.Enabled);

    public string ProductNew => this.wddProductNew.SelectedValue;

    public bool CompletedData
    {
      get
      {
        return this.txtPlateNumber.Text.Length > 0 && this.txtRequirementPlateId.Text.Length > 0 && this.wddProcessType.SelectedValue != "-1" && this.wddProductCurrent.SelectedValue != "-1" && this.wddProductNew.SelectedValue != "-1";
      }
    }

    private void LoadParameters()
    {
      SystemParameterQueriesBL parameterQueriesBl = new SystemParameterQueriesBL();
      ArrayList arrFilter = new ArrayList()
      {
        (object) SystemParameterGroups.ProcessType.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      };
      foreach (DataRow row in (InternalDataCollectionBase) parameterQueriesBl.GetbyFilter(arrFilter).Rows)
      {
        if (Convert.ToInt32(row["i_GroupId"], (IFormatProvider) CultureInfo.CurrentCulture) == SystemParameterGroups.ProcessType)
          this.wddProcessType.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
      }
      this.wddProcessType.Items.Insert(0, new ListItem(Constants.OPCIONLISTA_Seleccione, Constants.OPCIONLISTA_ValorSinSeleccion));
      DataTable dataTable = new ProductQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) -1,
        (object) 3,
        (object) -1,
        (object) "",
        (object) "",
        (object) 1,
        (object) 1
      });
      this.wddProductCurrent.Items.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        this.wddProductCurrent.Items.Add(new ListItem(row["v_Name"].ToString(), row["i_ProductId"].ToString()));
      this.wddProductCurrent.Items.Insert(0, new ListItem(Constants.OPCIONLISTA_Seleccione, Constants.OPCIONLISTA_ValorSinSeleccion));
    }

    private void GetProductClaimCombination(int pintProductId)
    {
      DataTable claimCombination = new ClaimTracingQueriesBL().GetProductClaimCombination(pintProductId);
      if (claimCombination != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) claimCombination.Rows)
          this.wddProductNew.Items.Add(new ListItem(row["v_Name"].ToString(), row["i_RelatedProductId"].ToString()));
      }
      this.wddProductNew.Items.Insert(0, new ListItem(Constants.OPCIONLISTA_Seleccione, Constants.OPCIONLISTA_ValorSinSeleccion));
    }

    public void SetLabels(string strValues)
    {
      if (!(strValues != ""))
        return;
      this.LoadParameters();
      string[] source = strValues.Split('|');
      if (((IEnumerable<string>) source).Count<string>() > 1)
        this.lblPlateNumber.Text = source[1];
      if (((IEnumerable<string>) source).Count<string>() > 2)
        this.lblRequirementPlateId.Text = source[2];
      if (((IEnumerable<string>) source).Count<string>() > 3)
        this.lblProcessType.Text = source[3];
      if (((IEnumerable<string>) source).Count<string>() > 4)
        this.lblProductCurrent.Text = source[4];
      if (((IEnumerable<string>) source).Count<string>() > 5)
        this.lblProductNew.Text = source[5];
    }

    public string GetTexts()
    {
      return "" + this.txtPlateNumber.Text + "|" + this.txtRequirementPlateId.Text + "|" + this.wddProcessType.SelectedValue + "|" + this.wddProductCurrent.SelectedValue + "|" + this.wddProductNew.SelectedValue;
    }

    public void SetTexts(string strValues)
    {
      this.LoadParameters();
      string[] source = strValues.Split('|');
      this.GetProductClaimCombination(int.Parse(source[3], (IFormatProvider) CultureInfo.CurrentCulture));
      int int32 = this.wddProductCurrent.SelectedValue == "" ? 0 : Convert.ToInt32(this.wddProductCurrent.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      if (((IEnumerable<string>) source).Count<string>() > 0)
        this.txtRequirementPlateId.Text = source[0];
      if (((IEnumerable<string>) source).Count<string>() > 1)
        this.txtPlateNumber.Text = source[1];
      if (((IEnumerable<string>) source).Count<string>() > 2)
        this.wddProcessType.SelectedValue = source[2];
      if (((IEnumerable<string>) source).Count<string>() > 3)
        this.wddProductCurrent.SelectedValue = source[3];
      if (((IEnumerable<string>) source).Count<string>() <= 4)
        return;
      this.wddProductNew.SelectedValue = source[4];
    }

    public void EnabledControls(bool enabled)
    {
      this.txtPlateNumber.Enabled = enabled;
      this.txtRequirementPlateId.Enabled = enabled;
      this.wddProcessType.Enabled = enabled;
      this.wddProductCurrent.Enabled = enabled;
      this.wddProductNew.Enabled = enabled;
    }

    public void ClearControls()
    {
      this.txtPlateNumber.Text = "";
      this.txtRequirementPlateId.Text = "";
      this.wddProcessType.SelectedValue = "-1";
      this.wddProductCurrent.SelectedValue = "-1";
      this.wddProductNew.SelectedValue = "-1";
    }

    public void SetReadOnly(bool bolStatus)
    {
      this.txtRequirementPlateId.Enabled = !bolStatus;
      this.txtPlateNumber.Enabled = !bolStatus;
      this.wddProcessType.Enabled = !bolStatus;
      this.wddProductCurrent.Enabled = !bolStatus;
      this.wddProductNew.Enabled = !bolStatus;
    }
  }
}
