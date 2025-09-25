// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.ClaimAssignedResponsibleCost
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Claim.BL;
using SIIV.Common.Resource;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims
{
  public class ClaimAssignedResponsibleCost : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected Label Label4;
    protected DropDownList wddClaimAssignedCostoTo;
    protected TextBox txtComments;
    protected Button wibSend;
    protected Button wibQuit;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      if (this.Request.QueryString["rcid"] != null)
        this.ViewState["i_requirementclaimid"] = (object) this.Request.QueryString["rcid"].ToString((IFormatProvider) CultureInfo.CurrentCulture);
      if (this.Request.QueryString["st"] != null)
        this.ViewState["claimstatus"] = (object) this.Request.QueryString["st"].ToString((IFormatProvider) CultureInfo.CurrentCulture);
      this.LoadParameters();
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
      if (!this.ValidateControls())
        return;
      int num1 = 0;
      if (this.Session["SystemUser"] != null)
        num1 = ((SystemUser) this.Session["SystemUser"]).i_SystemUserId;
      ClaimTracing pobjBE = new ClaimTracing();
      pobjBE.i_RequirementClaimId = Convert.ToInt32(this.ViewState["i_requirementclaimid"], (IFormatProvider) CultureInfo.CurrentCulture);
      pobjBE.i_ActionId = new int?(2);
      pobjBE.i_InsertUserId = new int?(num1);
      pobjBE.d_InsertDate = new DateTime?(DateTime.Now);
      pobjBE.i_Status = new int?(Convert.ToInt32(this.ViewState["claimstatus"], (IFormatProvider) CultureInfo.CurrentCulture));
      pobjBE.v_Comments = this.txtComments.Text.TrimEnd();
      RequirementClaim pobjRC = new RequirementClaim();
      pobjRC.i_RequirementClaimId = Convert.ToInt32(this.ViewState["i_requirementclaimid"], (IFormatProvider) CultureInfo.CurrentCulture);
      pobjRC.i_ClaimTypeId = new int?(Convert.ToInt32(this.ViewState["i_claimtypeid"], (IFormatProvider) CultureInfo.CurrentCulture));
      pobjRC.i_Status = new int?(-1);
      pobjRC.i_ClaimCostAssignedTo = new int?(Convert.ToInt32(this.wddClaimAssignedCostoTo.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
      pobjRC.d_UpdateDate = new DateTime?(DateTime.Now);
      int num2 = 0;
      using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
      {
        Timeout = new TimeSpan(1, 1, 1)
      }))
      {
        try
        {
          num2 = new RequirementClaimManagementBL().TracingClaim(pobjBE, ref pobjRC);
          transactionScope.Complete();
        }
        catch (Exception ex)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Error, Constants.REQUIREMENT_ERROR_GENERICO + ex.Message);
          return;
        }
      }
      if (num2 > 0)
      {
        this.wddClaimAssignedCostoTo.SelectedValue = "-1";
        this.txtComments.Text = "";
        this.wibSend.Enabled = false;
        Message.SetMessage(this.lblMessage, enmMessageType.Success, "Se registró correctamente el seguimiento del reclamo");
      }
      else
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Se encontró un problema en el registro del seguimiento");
    }

    protected void btnCancel_Click(object sender, EventArgs e) => this.PopupClose();

    private void LoadParameters()
    {
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.ClaimCompany.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      this.wddClaimAssignedCostoTo.Items.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (Convert.ToInt32(row["i_GroupId"], (IFormatProvider) CultureInfo.CurrentCulture) == SystemParameterGroups.ClaimCompany)
          this.wddClaimAssignedCostoTo.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
      }
      this.wddClaimAssignedCostoTo.Items.Insert(0, new ListItem(Constants.OPCIONLISTA_Seleccione, Constants.OPCIONLISTA_ValorSinSeleccion));
    }

    private bool ValidateControls()
    {
      if (this.wddClaimAssignedCostoTo.SelectedValue == "" || this.wddClaimAssignedCostoTo.SelectedValue == "")
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Seleccione el responsable del costo");
        return false;
      }
      this.lblMessage.Visible = false;
      return true;
    }

    private void PopupClose()
    {
      string script = "SendInfoPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }
  }
}
