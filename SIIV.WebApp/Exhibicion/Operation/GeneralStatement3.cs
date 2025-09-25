// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibicion.Operation.GeneralStatement3
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibicion.Operation
{
  public class GeneralStatement3 : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected Label lblNum;
    protected Label lblPlateType0;
    protected HtmlTableRow trRotativa;
    protected HtmlTableRow trExhibicion;
    protected HtmlTableRow trgeneral;

    protected void Page_Load(object sender, EventArgs e)
    {
      string str = Convert.ToString(this.Request.QueryString["i_CustomerTypeId"]);
      this.lblNum.Text = !(Convert.ToString(this.Request.QueryString["i_param"]) == "2") ? "1" : "2";
      switch (str)
      {
        case "6":
          this.lblPlateType0.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(DateTime.Now.ToString("MM"))) + ", " + Convert.ToDateTime(DateTime.Now).ToString("yyyy");
          break;
        case "7":
          this.lblPlateType0.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(DateTime.Now.ToString("MM"))) + ", " + Convert.ToDateTime(DateTime.Now).ToString("yyyy");
          this.trExhibicion.Visible = true;
          break;
        case "11":
          this.lblPlateType0.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(DateTime.Now.ToString("MM"))) + ", " + Convert.ToDateTime(DateTime.Now).ToString("yyyy");
          this.trRotativa.Visible = true;
          break;
        case "7|11":
          this.lblPlateType0.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(DateTime.Now.ToString("MM"))) + ", " + Convert.ToDateTime(DateTime.Now).ToString("yyyy");
          break;
        default:
          this.lblPlateType0.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(DateTime.Now.ToString("MM"))) + ", " + Convert.ToDateTime(DateTime.Now).ToString("yyyy");
          this.trgeneral.Visible = true;
          break;
      }
    }
  }
}
