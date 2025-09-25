// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Requirement.SuccessfulSpecialRegistration
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using BarcodeLib;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.Requirement.BL;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Requirement
{
  public class SuccessfulSpecialRegistration : Page
  {
    private RequirementQueriesBL oRequirementBL;
    protected Label Label1;
    protected GridView gvList;
    protected Button Button2;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.refreshList();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("SpecialRequirement.aspx");
    }

    public void refreshList()
    {
      this.gvList.DataSource = (object) (this.Session["ListSunarpData"] as DataTable);
      this.gvList.DataBind();
    }

    protected void generateCUR(int index)
    {
      try
      {
        GridViewRow row = this.gvList.Rows[index];
        int int32 = Convert.ToInt32((this.Session["ids"] as DataTable).Rows[index][0].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        using (ReportDocument reportDocument = new ReportDocument())
        {
          this.oRequirementBL = new RequirementQueriesBL();
          string filename = this.Server.MapPath("../Requirement/ReportCur.rpt");
          reportDocument.Load(filename);
          DataTable cur = this.oRequirementBL.GenerateCUR(int32);
          cur.Columns.Add(new DataColumn()
          {
            ColumnName = "ImageBarPlate",
            DataType = typeof (byte[])
          });
          byte[] numArray1 = this.ImagenBarCode(cur.Rows[0]["v_PlateNew"].ToString());
          cur.Rows[0]["ImageBarPlate"] = (object) numArray1;
          cur.Columns.Add(new DataColumn()
          {
            ColumnName = "Requisite",
            DataType = typeof (string)
          });
          string requisitebyRequirement = new RequirementQueriesBL().GetRequisitebyRequirement(int32, Convert.ToInt32(cur.Rows[0]["i_ProcessTypeId"], (IFormatProvider) CultureInfo.CurrentCulture));
          cur.Rows[0]["Requisite"] = (object) requisitebyRequirement;
          cur.Columns.Add(new DataColumn()
          {
            ColumnName = "ImageBarCode",
            DataType = typeof (byte[])
          });
          byte[] numArray2 = this.ImagenBarCode(cur.Rows[0]["v_PaymentCode"].ToString());
          cur.Rows[0]["ImageBarCode"] = (object) numArray2;
          if (cur.Rows.Count > 1)
            cur.Rows.RemoveAt(1);
          reportDocument.SetDataSource(cur);
          reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "Certificado Unico de Registro");
          reportDocument.Close();
          ((Component) reportDocument).Dispose();
        }
      }
      catch (Exception ex)
      {
      }
    }

    private byte[] ImagenBarCode(string _NroPlaca)
    {
      if (!(_NroPlaca.Trim() != ""))
        return (byte[]) null;
      Barcode barcode = new Barcode();
      AlignmentPositions alignmentPositions = AlignmentPositions.CENTER;
      MemoryStream memoryStream = new MemoryStream();
      int int32_1 = Convert.ToInt32(300);
      int int32_2 = Convert.ToInt32(150);
      TYPE type = TYPE.CODE128;
      if (type != 0)
      {
        barcode.IncludeLabel = false;
        barcode.Alignment = alignmentPositions;
        barcode.Encode(type, _NroPlaca, Color.Black, Color.White, int32_1, int32_2);
        SaveTypes saveTypes = SaveTypes.JPG;
        barcode.SaveImage((Stream) memoryStream, saveTypes);
      }
      byte[] numArray = new byte[memoryStream.Length];
      return memoryStream.GetBuffer();
    }

    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      if (!(e.CommandName == "generateCUR"))
        return;
      this.generateCUR(Convert.ToInt32(e.CommandArgument));
    }
  }
}
