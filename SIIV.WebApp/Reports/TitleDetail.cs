// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Reports.TitleDetail
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Reports
{
  public class TitleDetail : Page
  {
    protected HtmlForm form1;
    protected TextBox lblValue1;
    protected TextBox txtValue1;
    protected TextBox lblValue2;
    protected TextBox txtValue2;
    protected TextBox lblValue3;
    protected TextBox txtValue3;
    protected TextBox lblValue4;
    protected TextBox txtValue4;
    protected TextBox lblValue5;
    protected TextBox txtValue5;

    protected void Page_Load(object sender, EventArgs e) => this.SetValues();

    private void SetValues()
    {
      if (!(this.Session["DetailsRequest"] is DataTable dataTable) || dataTable.Rows.Count <= 0)
        return;
      DataRow row = dataTable.Rows[0];
      this.lblValue1.Text = dataTable.Columns[0].ColumnName;
      this.lblValue2.Text = dataTable.Columns[1].ColumnName;
      this.lblValue3.Text = dataTable.Columns[2].ColumnName;
      this.lblValue4.Text = dataTable.Columns[3].ColumnName;
      this.lblValue5.Text = dataTable.Columns[4].ColumnName;
      this.txtValue1.Text = row[0].ToString();
      this.txtValue2.Text = row[1].ToString();
      this.txtValue3.Text = row[2].ToString();
      this.txtValue4.Text = row[3].ToString();
      this.txtValue5.Text = row[4].ToString();
    }
  }
}
