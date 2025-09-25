// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Public.CheckClaimStatus
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.Claim.BL;
using SIIV.Common.Resource;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Public
{
  public class CheckClaimStatus : Page
  {
    protected Image Image1;
    protected TextBox txtPlateNumber;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected ValidatorCalloutExtender RequiredFieldValidator1_ValidatorCalloutExtender;
    protected TextBox txtClaimCode;
    protected HtmlGenericControl WebCaptcha1;
    protected Image Image2;
    protected Button BtnRefresh;
    protected TextBox txtimgcode;
    protected HtmlTableCell tagSearch;
    protected Button wibSearch;
    protected HtmlTableCell tagDataVehicle;
    protected Label Label1;
    protected Label lblClaimTypeName;
    protected Label Label3;
    protected Label lblClaimDate;
    protected Label Label2;
    protected Label lblStatusName;
    protected Label Label4;
    protected Label lblComments;
    protected Label Label5;
    protected Label lblDateClaimStatus;
    protected HtmlTableCell tagNewSearch;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      (this.Master.FindControl("lblSubTitle") as Label).Text = "Datos Iniciales";
      this.lblMessage.Visible = false;
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      if (this.Session["CaptchaImageText"].ToString() != this.txtimgcode.Text.ToString())
      {
        this.Image2.ImageUrl = "~/UserControls/FrmCaptcha.aspx";
        this.lblMessage.Text = "Clave no válida";
        this.txtimgcode.Text = string.Empty;
        this.txtimgcode.Focus();
      }
      else if (this.txtClaimCode.Text.Length == 0 && this.txtPlateNumber.Text.Length == 0)
      {
        this.txtimgcode.Text = "";
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Advertencia:<br> ingrese un numero de Placa o Codigo Reclamo.");
      }
      else
        this.GetClaim();
    }

    private void GetClaim()
    {
      this.lblMessage.Visible = false;
      RequirementClaimQueriesBL requirementClaimQueriesBl = new RequirementClaimQueriesBL();
      string text1 = this.txtClaimCode.Text;
      string text2 = this.txtPlateNumber.Text;
      try
      {
        DataTable requirementClaimQuery = new RequirementClaimQueriesBL().GetRequirementClaimQuery(text2, text1);
        if (requirementClaimQuery == null || requirementClaimQuery.Rows.Count == 0)
        {
          this.lblClaimTypeName.Text = "";
          this.lblClaimDate.Text = "";
          this.lblStatusName.Text = "";
          this.lblComments.Text = "";
          this.lblDateClaimStatus.Text = "";
          this.lblMessage.Text = "No se encontro informacion con los datos ingresados.";
          this.tagDataVehicle.Visible = false;
          this.lblMessage.Visible = true;
        }
        else
        {
          this.lblClaimTypeName.Text = requirementClaimQuery.Rows[0]["v_ClaimTypeName"].ToString();
          this.lblClaimDate.Text = requirementClaimQuery.Rows[0]["v_ClaimDate"].ToString();
          this.lblStatusName.Text = requirementClaimQuery.Rows[0]["v_StatusName"].ToString();
          this.lblComments.Text = requirementClaimQuery.Rows[0]["v_Comments"].ToString();
          this.lblDateClaimStatus.Text = requirementClaimQuery.Rows[0]["d_DateClaimStatus"].ToString();
          this.tagDataVehicle.Visible = true;
        }
      }
      catch (Exception ex)
      {
      }
    }

    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
      this.Image2.ImageUrl = "~/UserControls/FrmCaptcha.aspx";
    }
  }
}
