// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibicion.Operation.GeneralStatement5
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibicion.Operation
{
  public class GeneralStatement5 : Page
  {
    protected HtmlHead Head1;
    protected Label lblStarDate;
    protected Label lblStarFinish;
    protected HtmlForm form1;
    protected Label lblNum;
    protected Label lblPlateType0;
    protected HtmlTableRow trRotativa;
    protected HtmlTableRow trExhibicion;
    protected HtmlTableRow trgeneral;

    protected void Page_Load(object sender, EventArgs e)
    {
      string str1 = Convert.ToString(this.Request.QueryString["i_CustomerTypeId"]);
      string str2 = Convert.ToString(this.Request.QueryString["i_param"]);
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.SpecialPlateQuarterlyPrice.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string str3 = dataTable.Rows[0]["v_Value"].ToString();
      string str4 = dataTable.Rows[1]["v_Value"].ToString();
      this.lblStarDate.Text = str3;
      this.lblStarFinish.Text = str4;
      this.lblNum.Text = !(str2 == "2") ? "1" : "2";
      switch (str1)
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
