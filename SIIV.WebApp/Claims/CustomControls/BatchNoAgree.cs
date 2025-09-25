// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.CustomControls.BatchNoAgree
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Claim.BL;
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
  public class BatchNoAgree : UserControl
  {
    protected Label lblDispatchNumber;
    protected TextBox txtDispatchNumber;
    protected Label lblBatchNumber;
    protected TextBox txtBatchNumber;
    protected Label Label1;
    protected DropDownList wddClaimMotive;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.LoadParameters();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int i_BatchId
    {
      get
      {
        return Convert.ToInt32(this.txtBatchNumber.Text, (IFormatProvider) CultureInfo.CurrentCulture);
      }
    }

    public int i_ClaimMotiveId
    {
      get
      {
        return this.wddClaimMotive.SelectedValue == "" ? 0 : Convert.ToInt32(this.wddClaimMotive.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      }
    }

    public bool CompletedData
    {
      get => this.txtDispatchNumber.Text.Length > 0 && this.txtBatchNumber.Text.Length > 0;
    }

    private void LoadParameters()
    {
      try
      {
        DataTable motive = new RequirementClaimQueriesBL().GetMotive(4);
        this.wddClaimMotive.Items.Clear();
        foreach (DataRow row in (InternalDataCollectionBase) motive.Rows)
          this.wddClaimMotive.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        this.wddClaimMotive.Items.Insert(0, new ListItem(Constants.OPCIONLISTA_Seleccione, Constants.OPCIONLISTA_ValorSinSeleccion));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void SetLabels(string strValues)
    {
      try
      {
        if (!(strValues != ""))
          return;
        this.LoadParameters();
        strValues.Split('|');
        this.LoadParameters();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void SetMotive(int i_claimmotiveid)
    {
      this.wddClaimMotive.SelectedValue = i_claimmotiveid.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    public string GetTexts() => "" + this.txtDispatchNumber.Text + "|" + this.txtBatchNumber.Text;

    public void SetTexts(string strValues)
    {
      try
      {
        this.LoadParameters();
        string[] source = strValues.Split('|');
        if (((IEnumerable<string>) source).Count<string>() > 0)
          this.txtDispatchNumber.Text = source[0];
        if (((IEnumerable<string>) source).Count<string>() <= 1)
          return;
        this.txtBatchNumber.Text = source[1];
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void EnabledControls(bool enabled)
    {
      this.txtDispatchNumber.Enabled = enabled;
      this.txtBatchNumber.Enabled = enabled;
      this.wddClaimMotive.Enabled = enabled;
    }

    public void ClearControls()
    {
      this.txtDispatchNumber.Text = "";
      this.txtBatchNumber.Text = "";
      this.wddClaimMotive.SelectedValue = "-1";
    }
  }
}
