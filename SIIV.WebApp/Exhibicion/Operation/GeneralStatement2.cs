// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibicion.Operation.GeneralStatement2
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System;
using System.Configuration;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibicion.Operation
{
  public class GeneralStatement2 : Page
  {
    protected HtmlForm form1;
    protected Label lblPlateType0;
    protected Label lblPlateType;
    protected Label lblFecRenovation;
    protected Label lblCount;
    protected Label lblPlateType1;
    protected Label lblPeriodo;
    protected HyperLink HyperLink2;
    protected Label textExh;
    protected HyperLink HyperLink1;
    protected Label textExh2;
    protected HtmlGenericControl br1;
    protected HtmlGenericControl br2;
    protected Label textExh3;
    protected Label txtPlate;
    protected Label lblPlateType2;
    protected Label lblTelf;
    protected Label lblAnexo;
    protected Label br;

    protected void Page_Load(object sender, EventArgs e)
    {
      string str1 = Convert.ToString(this.Request.QueryString["i_CustomerTypeId"]);
      string str2 = this.Request.QueryString["fecRenovation"];
      string appSetting = ConfigurationManager.AppSettings["UrlTutorialRenovacionesPlacasEspeciales"];
      this.lblFecRenovation.Text = str2;
      Label lblPeriodo = this.lblPeriodo;
      DateTime dateTime = DateTime.Now;
      dateTime = dateTime.AddYears(1);
      string str3 = dateTime.Year.ToString();
      lblPeriodo.Text = str3;
      switch (str1)
      {
        case "7":
          str1 = "7";
          break;
        case "6":
          str1 = "6";
          break;
        case "11":
          str1 = "11";
          break;
        case "7|11":
          str1 = "7|11";
          break;
      }
      switch (str1)
      {
        case "6":
          this.lblPlateType0.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(DateTime.Now.ToString("MM"))) + ", " + Convert.ToDateTime(DateTime.Now).ToString("yyyy");
          this.lblPlateType1.Text = "Rotativas, ";
          this.lblPlateType2.Text = "Rotativas";
          this.lblTelf.Text = "6403637";
          this.lblAnexo.Text = "174";
          break;
        case "7":
          this.lblPlateType0.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(DateTime.Now.ToString("MM"))) + "," + Convert.ToDateTime(DateTime.Now).ToString("yyyy");
          this.lblPlateType1.Text = "de Exhibición, ";
          this.lblPlateType2.Text = "de Exhibición";
          this.txtPlate.Text = "de Exhibición";
          this.lblPlateType.Text = "Exhibición";
          this.textExh2.Text = "";
          this.textExh3.Text = "Les recordamos que";
          this.textExh.Text = "Podrá acceder ingresando a la página web: <a target= \"_blank \" id = \"link \" href = \" https://www.placas.pe/Index.aspx\">www.placas.pe</a> con el usuario y contraseña asignados.<br />";
          this.br.Text = "  <br /><br />";
          this.lblTelf.Text = "942159836";
          this.lblAnexo.Text = "";
          this.lblCount.Text = "cuenta";
          this.HyperLink1.NavigateUrl = "https://www.youtube.com/watch?v=DLDM-8eOlZc";
          this.HyperLink2.NavigateUrl = "~/Exhibicion/Docs/PlacasEspeciales-Oficinas-2024.pdf";
          break;
        case "11":
          this.lblPlateType0.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(DateTime.Now.ToString("MM"))) + "," + Convert.ToDateTime(DateTime.Now).ToString("yyyy");
          this.lblPlateType1.Text = "Rotativas, ";
          this.lblPlateType2.Text = "Rotativas";
          this.lblPlateType.Text = "Rotativas";
          this.txtPlate.Text = "Rotativas";
          this.textExh.Text = "Podrá acceder ingresando a la página web: <a target= \"_blank \" id = \"link \" href = \" https://www.placas.pe/Index.aspx\">www.placas.pe</a> utilizando el usuario y contraseña asignados.";
          this.textExh2.Text = "Es necesario tomar las previsiones del caso para realizar las renovaciones a tiempo, ya que las asignaciones de placas rotativas tienen como plazo máximo 15 días calendario según normativa vigente, por lo cual se presentan asignaciones desde diciembre del 2024 hasta enero 2025.";
          this.textExh3.Text = "Recordarle que todas";
          this.lblTelf.Text = "965671192";
          this.lblAnexo.Text = "";
          this.lblCount.Text = "contará";
          this.br.Text = " ";
          this.HyperLink1.NavigateUrl = "https://www.youtube.com/watch?v=DLDM-8eOlZc";
          this.HyperLink2.NavigateUrl = "~/Exhibicion/Docs/PlacasEspeciales-Oficinas-2024.pdf";
          break;
        case "7|11":
          this.lblPlateType0.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(DateTime.Now.ToString("MM"))) + "," + Convert.ToDateTime(DateTime.Now).ToString("yyyy");
          this.lblPlateType1.Text = "Especiales, ";
          this.lblPlateType2.Text = "Especiales";
          this.lblTelf.Text = "6403636/6403637";
          this.lblAnexo.Text = "134/174";
          break;
      }
      if (str1 == "11")
      {
        this.textExh2.Visible = true;
        this.br2.Visible = false;
      }
      else
      {
        this.textExh2.Visible = false;
        this.br1.Visible = false;
        this.br2.Visible = false;
      }
    }
  }
}
