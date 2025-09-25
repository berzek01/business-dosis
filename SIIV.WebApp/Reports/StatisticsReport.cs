// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Reports.StatisticsReport
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.Reports.BL;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Reports
{
  public class StatisticsReport : Page
  {
    protected Button wibSearch;
    protected GridView wdgStatistics;
    protected Chart UltraChart1;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadData();
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      this.LoadData();
      this.HidePopup();
    }

    private void LoadData()
    {
      DataTable data = this.GetData();
      string[] strArray = new string[6]
      {
        "DATOS SUNARP",
        "DATOS WEB",
        "DATOS BANCO",
        "DATOS PRE PRODUCCION",
        "PLACAS EN PTO ENTREGA",
        "PLACAS RECEPCIONADAS"
      };
      int[] numArray = new int[6]
      {
        Convert.ToInt32(data.Rows[0]["DATOS SUNARP"].ToString()),
        Convert.ToInt32(data.Rows[0]["DATOS WEB"].ToString()),
        Convert.ToInt32(data.Rows[0]["DATOS BANCO"].ToString()),
        Convert.ToInt32(data.Rows[0]["DATOS PRE PRODUCCION"].ToString()),
        Convert.ToInt32(data.Rows[0]["PLACAS EN PTO ENTREGA"].ToString()),
        Convert.ToInt32(data.Rows[0]["PLACAS RECEPCIONADAS"].ToString())
      };
      for (int index = 0; index < strArray.Length; ++index)
      {
        DataPoint dataPoint = new DataPoint();
        dataPoint.SetValueXY((object) strArray[index], (object) numArray[index]);
        this.UltraChart1.Series["DATOS SUNARP"].Points.Add(dataPoint);
      }
      this.UltraChart1.Legends[0].Enabled = false;
      this.UltraChart1.DataBind();
      this.LoadList(data);
    }

    private DataTable GetData()
    {
      DataTable data = new DataTable();
      try
      {
        data = new ReportManagementBL().GetStatisticsReport();
        if (data == null || data.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
          this.HidePopup();
        }
        else
          this.lblMessage.Visible = false;
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, ex.Message);
      }
      return data;
    }

    private void LoadList(DataTable dt)
    {
      this.lblMessage.Visible = false;
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("s_Estadisticas", Type.GetType("System.String"));
      dataTable.Columns.Add("d_Total", Type.GetType("System.Double"));
      DataRow row1 = dataTable.NewRow();
      row1["s_Estadisticas"] = (object) "Registros Obtenidos del WEB SERVICE del MTC";
      row1["d_Total"] = (object) Convert.ToDouble(dt.Rows[0][0], (IFormatProvider) CultureInfo.CurrentCulture);
      dataTable.Rows.Add(row1);
      DataRow row2 = dataTable.NewRow();
      row2["s_Estadisticas"] = (object) "Solicitudes de Expedicion de Placas Acreditadas por el usuario en el Sistema";
      row2["d_Total"] = (object) Convert.ToDouble(dt.Rows[0][1], (IFormatProvider) CultureInfo.CurrentCulture);
      dataTable.Rows.Add(row2);
      DataRow row3 = dataTable.NewRow();
      row3["s_Estadisticas"] = (object) "Pagos Reportados por Bancos";
      row3["d_Total"] = (object) Convert.ToDouble(dt.Rows[0][2], (IFormatProvider) CultureInfo.CurrentCulture);
      dataTable.Rows.Add(row3);
      DataRow row4 = dataTable.NewRow();
      row4["s_Estadisticas"] = (object) "Solicitudes de Expedicion de Placas Enviadas a Produccion";
      row4["d_Total"] = (object) Convert.ToDouble(dt.Rows[0][3], (IFormatProvider) CultureInfo.CurrentCulture);
      dataTable.Rows.Add(row4);
      DataRow row5 = dataTable.NewRow();
      row5["s_Estadisticas"] = (object) "Placas Despachadas a Punto de Entrega";
      row5["d_Total"] = (object) Convert.ToDouble(dt.Rows[0][4], (IFormatProvider) CultureInfo.CurrentCulture);
      dataTable.Rows.Add(row5);
      DataRow row6 = dataTable.NewRow();
      row6["s_Estadisticas"] = (object) "Placas Recibidas en Punto de Entrega";
      row6["d_Total"] = (object) Convert.ToDouble(dt.Rows[0][5], (IFormatProvider) CultureInfo.CurrentCulture);
      dataTable.Rows.Add(row6);
      this.wdgStatistics.DataSource = (object) dataTable;
      this.wdgStatistics.DataBind();
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
