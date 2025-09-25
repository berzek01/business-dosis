// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.ClaimManage
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Claim.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims
{
  public class ClaimManage : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected Label Label1;
    protected TextBox txtClaimNumber;
    protected Label Label2;
    protected DropDownList wddClaimType;
    protected Label Label3;
    protected DropDownList wddStatus;
    protected TextBox txtObservations;
    protected Button btnOK;
    protected GridView wdgTracing;
    protected Button btnCancel;

    private void LoadParameters()
    {
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) (SystemParameterGroups.ClaimTypes.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ", " + SystemParameterGroups.ClaimStatus.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
        (object) "",
        (object) "1",
        (object) "1"
      });
      this.wddStatus.Items.Clear();
      this.wddClaimType.Items.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (Convert.ToInt32(row["i_GroupId"], (IFormatProvider) CultureInfo.CurrentCulture) == SystemParameterGroups.ClaimTypes)
          this.wddClaimType.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        if (Convert.ToInt32(row["i_GroupId"], (IFormatProvider) CultureInfo.CurrentCulture) == SystemParameterGroups.ClaimStatus)
          this.wddStatus.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
      }
      this.wddStatus.Items[0].Enabled = false;
      this.wddClaimType.Items.Insert(0, new ListItem(Constants.OPCIONLISTA_Seleccione, Constants.OPCIONLISTA_ValorSinSeleccion));
      this.wddStatus.Items.Insert(0, new ListItem(Constants.OPCIONLISTA_Seleccione, Constants.OPCIONLISTA_ValorSinSeleccion));
    }

    private void GetTracing(int i_RequirementClaimId, int i_ClaimTypeId)
    {
      this.txtClaimNumber.Text = i_RequirementClaimId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      this.wddClaimType.SelectedValue = i_ClaimTypeId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      DataTable claimTracing = new ClaimTracingQueriesBL().GetClaimTracing(i_RequirementClaimId);
      if (claimTracing == null || claimTracing.Rows.Count == 0)
        this.ShowAlert("No se encontraron seguimientos para el reclamo seleccionado");
      else
        this.SetearList(claimTracing);
    }

    private void SetearList(DataTable dt)
    {
      this.wdgTracing.DataSource = (object) null;
      this.wdgTracing.DataSource = (object) dt;
      this.wdgTracing.DataBind();
    }

    private void PopupClose()
    {
      string script = "PopupClose();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void ShowAlert(string Msg)
    {
      string script = "ShowAlert('" + Msg + "');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      int int32_1 = Convert.ToInt32(this.Request.QueryString["i_RequirementClaimId"], (IFormatProvider) CultureInfo.CurrentCulture);
      int int32_2 = Convert.ToInt32(this.Request.QueryString["i_ClaimTypeId"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.LoadParameters();
      this.GetTracing(int32_1, int32_2);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      this.Session["Consult"] = (object) true;
      this.Response.Redirect("ClaimInbox.aspx");
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
      if (this.wddStatus.SelectedValue == "" || this.wddStatus.SelectedValue == "-1" || this.txtObservations.Text.Length == 0)
      {
        this.ShowAlert("Faltan ingresar datos para registrar el seguimiento");
      }
      else
      {
        ClaimTracing pobjBE = new ClaimTracing();
        pobjBE.i_RequirementClaimId = Convert.ToInt32(this.txtClaimNumber.Text, (IFormatProvider) CultureInfo.CurrentCulture);
        pobjBE.i_ActionId = new int?(2);
        if (this.Session["SystemUser"] != null)
        {
          SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
          pobjBE.i_InsertUserId = new int?(systemUser.i_SystemUserId);
        }
        else
          pobjBE.i_InsertUserId = new int?(0);
        pobjBE.d_InsertDate = new DateTime?(DateTime.Now);
        pobjBE.i_Status = new int?(Convert.ToInt32(this.wddStatus.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
        pobjBE.v_Comments = this.txtObservations.Text.TrimEnd();
        RequirementClaim pobjRC = new RequirementClaim();
        pobjRC.i_RequirementClaimId = Convert.ToInt32(this.txtClaimNumber.Text, (IFormatProvider) CultureInfo.CurrentCulture);
        pobjRC.i_ClaimTypeId = new int?(Convert.ToInt32(this.wddClaimType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
        pobjRC.i_Status = new int?(Convert.ToInt32(this.wddStatus.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
        pobjRC.d_UpdateDate = new DateTime?(DateTime.Now);
        int num = 0;
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
        {
          Timeout = new TimeSpan(1, 1, 1)
        }))
        {
          try
          {
            num = new RequirementClaimManagementBL().TracingClaim(pobjBE, ref pobjRC);
            transactionScope.Complete();
          }
          catch (Exception ex)
          {
            this.ShowAlert(ex.Message);
          }
        }
        if (num > 0)
        {
          this.GetTracing(pobjBE.i_RequirementClaimId, Convert.ToInt32(this.wddClaimType.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
          this.wddStatus.SelectedValue = "-1";
          this.txtObservations.Text = "";
        }
        else
          this.ShowAlert("Se encontro un problema en el registro del seguimiento");
      }
    }
  }
}
