// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.ClaimBookManage
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Claim.BL;
using SIIV.Common.Resource;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims
{
  public class ClaimBookManage : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected Label lblNReclamo;
    protected TextBox txtClaimNumber;
    protected Label lblEstado;
    protected DropDownList wddStatusClaimManagament;
    protected GridView wdgTracing;
    protected Button wibApprove;
    protected Button wibEmail;
    protected Button wibAssigned;
    protected Button wibReject;
    protected Button wibClaimRaised;
    protected Button wibClose;
    protected Button wibCancelManagement;
    protected Button btnManagementTemp;
    protected Label lblMessageClaimManagement;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      int int32_1 = Convert.ToInt32(this.Session["i_RequirementClaimId"], (IFormatProvider) CultureInfo.CurrentCulture);
      int int32_2 = Convert.ToInt32(this.Session["i_ClaimTypeId"], (IFormatProvider) CultureInfo.CurrentCulture);
      this.LoadParameters();
      this.GetTracing(int32_1, int32_2);
      this.wddStatusClaimManagament.SelectedValue = this.Session["i_Status"].ToString();
    }

    protected void wibApprove_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      int num = 8;
      this.CreatePopUpServer("Aprobación Reclamo", "../../Claims/ClaimApprove.aspx?flag=1&rcid=" + this.Session["i_RequirementClaimId"].ToString() + "&ctm=" + num.ToString((IFormatProvider) CultureInfo.CurrentCulture), "755px", "250px");
    }

    protected void wibEmail_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.CreatePopUpServer("Envio Email", "../../Claims/ClaimEmailSend.aspx?rcid=" + this.Session["i_RequirementClaimId"].ToString() + "&st=" + this.wddStatusClaimManagament.SelectedValue, "755px", "670px");
    }

    protected void wibAssigned_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.CreatePopUpServer("Asignación Responsable Costo Reclamo", "../../Claims/ClaimAssignedResponsibleCost.aspx?rcid=" + this.Session["i_RequirementClaimId"].ToString() + "&st=" + this.wddStatusClaimManagament.SelectedValue, "755px", "320px");
    }

    protected void wibReject_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      int num = 8;
      this.CreatePopUpServer("Rechazar Reclamo", "../../Claims/ClaimApprove.aspx?flag=3&rcid=" + this.Session["i_RequirementClaimId"].ToString() + "&ctm=" + num.ToString((IFormatProvider) CultureInfo.CurrentCulture), "755px", "250px");
    }

    protected void wibClaimRaised_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      int num = 8;
      this.CreatePopUpServer("Levantar Reclamo", "../../Claims/ClaimApprove.aspx?flag=4&rcid=" + this.Session["i_RequirementClaimId"].ToString() + "&ctm=" + num.ToString((IFormatProvider) CultureInfo.CurrentCulture), "755px", "300px");
    }

    protected void wibClose_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      int num = 8;
      this.CreatePopUpServer("Cerrar Reclamo", "../../Claims/ClaimApprove.aspx?flag=2&rcid=" + this.Session["i_RequirementClaimId"].ToString() + "&ctm=" + num.ToString((IFormatProvider) CultureInfo.CurrentCulture), "755px", "250px");
    }

    protected void btnCancelManagement_Click(object sender, EventArgs e)
    {
      this.Session["Consult"] = (object) true;
      this.Response.Redirect("ClaimBookInbox.aspx");
    }

    protected void btnManagementTemp_Click(object sender, EventArgs e)
    {
      int i_ClaimTypeId = 8;
      this.GetTracing(Convert.ToInt32(this.Session["i_RequirementClaimId"], (IFormatProvider) CultureInfo.CurrentCulture), i_ClaimTypeId);
      try
      {
        DataTable allClaimBook = new RequirementClaimQueriesBL().GetAllClaimBook(Convert.ToInt32(this.Session["i_RequirementClaimId"], (IFormatProvider) CultureInfo.CurrentCulture), 0, 0, 0, 0, 0, 0, 0, "", new DateTime?(), new DateTime?());
        if (allClaimBook == null || allClaimBook.Rows.Count <= 0)
          return;
        this.wddStatusClaimManagament.SelectedValue = allClaimBook.Rows[0]["i_status"].ToString();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageClaimManagement, enmMessageType.Warning, ex.Message);
      }
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
      this.wddStatusClaimManagament.Items.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (Convert.ToInt32(row["i_GroupId"], (IFormatProvider) CultureInfo.CurrentCulture) == SystemParameterGroups.ClaimStatus)
          this.wddStatusClaimManagament.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
      }
    }

    private void GetTracing(int i_RequirementClaimId, int i_ClaimTypeId)
    {
      DataTable claimTracing = new ClaimTracingQueriesBL().GetClaimTracing(i_RequirementClaimId);
      if (claimTracing == null || claimTracing.Rows.Count == 0)
      {
        Message.SetMessage(this.lblMessageClaimManagement, enmMessageType.Warning, "No se encontraron seguimientos para el reclamo seleccionado");
      }
      else
      {
        this.txtClaimNumber.Text = claimTracing.Rows[0]["v_ClaimCode"].ToString();
        this.lblMessageClaimManagement.Visible = false;
        this.SetearList(claimTracing);
      }
    }

    private void SetearList(DataTable dt)
    {
      this.wdgTracing.DataSource = (object) null;
      this.wdgTracing.DataSource = (object) dt;
      this.wdgTracing.DataBind();
    }

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format("OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }
  }
}
