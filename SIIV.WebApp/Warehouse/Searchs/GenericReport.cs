// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Searchs.GenericReport
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using CrystalDecisions.Shared;
using SIIV.Common.Resource;
using SIIV.WebApp.Warehouse.Rpt;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Searchs
{
  public class GenericReport : Page
  {
    protected HtmlForm form1;

    protected void Page_Load(object sender, EventArgs e)
    {
      DataTable datos = (DataTable) this.Session["dtExport"];
      string str1 = this.Session["ExportType"].ToString();
      try
      {
        switch (str1)
        {
          case "Pdf":
            ReportKardex reportKardex = new ReportKardex();
            reportKardex.SetDataSource(datos);
            reportKardex.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "KardexValorizado");
            reportKardex.Close();
            ((Component) reportKardex).Dispose();
            break;
          case "Excel":
            string str2 = "KardexValorizado";
            DateTime now = DateTime.Now;
            string[] strArray = new string[8];
            strArray[0] = str2;
            strArray[1] = now.Day.ToString((IFormatProvider) CultureInfo.CurrentCulture);
            int num = now.Month;
            strArray[2] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
            num = now.Year;
            strArray[3] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
            num = now.Hour;
            strArray[4] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
            num = now.Minute;
            strArray[5] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
            num = now.Second;
            strArray[6] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
            strArray[7] = ".xls";
            string pstrFileTarget = string.Concat(strArray);
            ArrayList titulos = new ArrayList();
            string str3 = this.Server.MapPath("../") + pstrFileTarget;
            OtherFormats otherFormats = new OtherFormats(str3);
            if (datos.Columns.Contains("orden"))
              datos.Columns.Remove("orden");
            if (datos.Columns.Contains("linea"))
              datos.Columns.Remove("linea");
            for (int index = 0; index < datos.Columns.Count; ++index)
              titulos.Add((object) datos.Columns[index].ColumnName);
            otherFormats.ExportClaimBook("Reporte Kardex Valorizado", titulos, datos);
            new ExportFile().Download(str3, pstrFileTarget);
            if (File.Exists(str3))
              File.Delete(str3);
            break;
        }
      }
      catch (Exception ex)
      {
      }
    }
  }
}
