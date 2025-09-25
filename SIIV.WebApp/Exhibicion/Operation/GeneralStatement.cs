// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibicion.Operation.GeneralStatement
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
  public class GeneralStatement : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected Label lblNum;
    protected Label lblPlateType0;
    protected HtmlTableRow trRotativa;
    protected HtmlTableRow trExhibicion;
    protected Label lblPlateType3;

    protected void Page_Load(object sender, EventArgs e)
    {
      string str = Convert.ToString(this.Request.QueryString["i_CustomerTypeId"]);
      if (Convert.ToString(this.Request.QueryString["i_param"]) == "2")
      {
        this.lblNum.Text = "2";
      }
      else
      {
        this.lblNum.Text = "1";
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.SpecialPlateAlertDateValidation.ToString((IFormatProvider) CultureInfo.CurrentCulture),
          (object) "",
          (object) "1",
          (object) "1"
        });
        DateTime.Parse(dataTable.Rows[0]["d_StartDate"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        DateTime.Parse(dataTable.Rows[0]["d_FinishDate"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
      }
      switch (str)
      {
        case "6":
          this.lblPlateType0.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(DateTime.Now.ToString("MM"))) + ", " + Convert.ToDateTime(DateTime.Now).ToString("yyyy");
          this.lblPlateType3.Text = "Rotativas";
          break;
        case "7":
          this.lblPlateType0.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(DateTime.Now.ToString("MM"))) + ", " + Convert.ToDateTime(DateTime.Now).ToString("yyyy");
          this.lblPlateType3.Text = "Exhibición";
          this.trExhibicion.Visible = true;
          break;
        case "11":
          this.lblPlateType0.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(DateTime.Now.ToString("MM"))) + ", " + Convert.ToDateTime(DateTime.Now).ToString("yyyy");
          this.trRotativa.Visible = true;
          this.lblPlateType3.Text = "Rotativas";
          break;
        case "7|11":
          this.lblPlateType0.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(DateTime.Now.ToString("MM"))) + ", " + Convert.ToDateTime(DateTime.Now).ToString("yyyy");
          this.lblPlateType3.Text = "Especiales";
          break;
      }
    }
  }
}
