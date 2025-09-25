// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Public.CheckPlateNumber
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.Registration.BL;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Public
{
  public class CheckPlateNumber : Page
  {
    protected TextBox txtPlateNumber;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected ValidatorCalloutExtender RequiredFieldValidator1_ValidatorCalloutExtender;
    protected HtmlGenericControl WebCaptcha1;
    protected Image Image2;
    protected TextBox txtimgcode;
    protected HtmlTableCell tagSearch;
    protected Button wibSearch;
    protected Label Label1;
    protected TextBox txtPlateNew;
    protected Label Label2;
    protected TextBox txtPlateOld;
    protected HtmlTableCell tagNewSearch;
    protected Button wibNewSearch;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      (this.Master.FindControl("lblSubTitle") as Label).Text = "Datos Iniciales";
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      if (this.Session["CaptchaImageText"].ToString() != this.txtimgcode.Text.ToString())
      {
        this.txtimgcode.Text = string.Empty;
        this.txtimgcode.Focus();
      }
      else
      {
        string[] strArray = new VehicleRegistrationQueriesBL().PlateNumberCheck(this.txtPlateNumber.Text);
        this.txtPlateNew.Text = strArray[0];
        this.txtPlateOld.Text = strArray[1];
        this.txtPlateNumber.Enabled = false;
        this.WebCaptcha1.Visible = false;
        this.tagSearch.Visible = false;
        this.tagNewSearch.Visible = true;
      }
    }

    protected void wibNewSearch_Click(object sender, EventArgs e)
    {
      this.txtPlateNumber.Text = "";
      this.txtPlateNew.Text = "";
      this.txtPlateOld.Text = "";
      this.txtPlateNumber.Enabled = true;
      this.WebCaptcha1.Visible = true;
      this.tagSearch.Visible = true;
      this.tagNewSearch.Visible = false;
    }
  }
}
