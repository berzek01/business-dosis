// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.CustomControls.ClaimData
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims.CustomControls
{
  public class ClaimData : UserControl
  {
    protected Label lblCalimDate;
    protected Fecha wdpClaimDate;
    protected Label lblStatus;
    protected DropDownList wddClaimStatus;
    protected CheckBox chkOK;

    public DateTime ClaimDate
    {
      get
      {
        return Convert.ToDateTime((object) this.wdpClaimDate.Value, (IFormatProvider) CultureInfo.CurrentCulture);
      }
    }

    public int ClaimStatus
    {
      get
      {
        return Convert.ToInt32(this.wddClaimStatus.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
      }
    }

    public int According => Convert.ToInt32(this.chkOK.Checked);

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadParameters();
      this.SetDate();
    }

    private void LoadParameters()
    {
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.ClaimStatus.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      this.wddClaimStatus.Items.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (Convert.ToInt32(row["i_GroupId"], (IFormatProvider) CultureInfo.CurrentCulture) == SystemParameterGroups.ClaimStatus)
          this.wddClaimStatus.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
      }
      this.wddClaimStatus.Items.Insert(0, new ListItem(Constants.OPCIONLISTA_Seleccione, Constants.OPCIONLISTA_ValorSinSeleccion));
      this.wddClaimStatus.SelectedValue = 1.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    private void SetDate() => this.wdpClaimDate.Value = DateTime.Now;

    public void SetearData(RequirementClaim objParam)
    {
      this.LoadParameters();
      if (objParam.v_ClaimCode != null || objParam.v_ClaimCode != "")
        this.wdpClaimDate.Value = Convert.ToDateTime(objParam.v_ClaimDate);
      this.wddClaimStatus.SelectedValue = objParam.i_Status.ToString();
      this.chkOK.Checked = true;
    }

    public void EnableControls(bool habilita)
    {
      this.wdpClaimDate.Enabled = !habilita && habilita;
      this.chkOK.Enabled = habilita || habilita;
    }

    public void EnableControlsEdit(bool habilita) => this.chkOK.Enabled = habilita;

    public void ClearControls()
    {
      this.wdpClaimDate.Value = DateTime.Now;
      this.wddClaimStatus.SelectedValue = 1.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      this.chkOK.Checked = false;
    }
  }
}
