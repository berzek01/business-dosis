// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Reports.Plant.LoadProductStock
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.BE.CustomCode;
using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Reports.Plant
{
  public class LoadProductStock : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected Label Label1;
    protected FileUpload FileInputExcel;
    protected HiddenField HiddenField1;
    protected Button wibSearch;
    protected Label lblMessage;
    protected GridView wdgList;
    protected Label lblMessageUser;
    protected Button wibSave;
    protected Button wibClose;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        ;
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      DataTable dataTable = new DataTable();
      string str1 = this.Server.MapPath("");
      try
      {
        if (!this.FileInputExcel.HasFile)
          return;
        Path.GetExtension(this.FileInputExcel.FileName);
        string str2 = str1 + "\\" + this.FileInputExcel.FileName;
        if (OtherFormats.ValidateExtensionFile(str2, ".xlsx"))
        {
          if (File.Exists(str2))
            File.Delete(str2);
          this.FileInputExcel.SaveAs(str2);
          DataTable dtDatos = OtherFormats.LoadDataExcel(str2, "[Hoja1$]");
          for (int index = dtDatos.Rows.Count - 1; index >= 0; --index)
          {
            if (dtDatos.Rows[index]["Id_Producto"].ToString() == "")
              dtDatos.Rows.RemoveAt(index);
          }
          if (File.Exists(str2))
            File.Delete(str2);
          if (!this.ValidateData(dtDatos) || dtDatos == null)
            return;
          this.wdgList.DataSource = (object) dtDatos;
          this.wdgList.DataBind();
          this.ViewState["dtImport"] = (object) dtDatos;
        }
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, ex.Message);
      }
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      try
      {
        List<ProductStock> pobjlstSpecialPlateSoat = new List<ProductStock>();
        pobjlstSpecialPlateSoat.Clear();
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = (DataTable) this.ViewState["dtImport"];
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
          pobjlstSpecialPlateSoat.Add(new ProductStock()
          {
            i_Product = Convert.ToInt32(row["Id_Producto"].ToString()),
            i_StockMin = Convert.ToInt32(row["i_stockMinimo"].ToString()),
            i_StockCri = Convert.ToInt32(row["i_stockCritico"].ToString()),
            i_DaysStockSeg = Convert.ToInt32(row["i_DaysStockSeg"].ToString()),
            i_DaysDelivery = Convert.ToInt32(row["i_DaysDelivery"].ToString()),
            i_DaysFreOrd = Convert.ToInt32(row["i_DaysFreOrd"].ToString()),
            i_InsertUserId = systemUser.i_SystemUserId
          });
        if (!new ProductWarehouseQueriesBL().ProductStockInsert(pobjlstSpecialPlateSoat))
          return;
        Message.SetMessage(this.lblMessage, enmMessageType.Success, "Se importó correctamente los datos del Stock del Producto");
        this.wibSave.Enabled = false;
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, ex.Message);
      }
    }

    protected void wibClose_Click(object sender, EventArgs e) => this.PopupClose();

    public bool ValidateData(DataTable dtDatos)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      for (int index = 0; index < dtDatos.Rows.Count; ++index)
      {
        int int32_1 = Convert.ToInt32(dtDatos.Rows[index]["Id_Producto"].ToString());
        int int32_2 = Convert.ToInt32(dtDatos.Rows[index]["i_stockMinimo"].ToString());
        int int32_3 = Convert.ToInt32(dtDatos.Rows[index]["i_stockCritico"].ToString());
        int int32_4 = Convert.ToInt32(dtDatos.Rows[index]["i_DaysStockSeg"].ToString());
        int int32_5 = Convert.ToInt32(dtDatos.Rows[index]["i_DaysDelivery"].ToString());
        int int32_6 = Convert.ToInt32(dtDatos.Rows[index]["i_DaysFreOrd"].ToString());
        try
        {
          num1 = int.Parse(dtDatos.Rows[index]["Id_Producto"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        }
        catch
        {
          this.lblMessage.Visible = true;
          this.lblMessage.Text = "Producto " + int32_1.ToString() + " NO Válido";
          return false;
        }
        try
        {
          num2 = int.Parse(dtDatos.Rows[index]["i_stockMinimo"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        }
        catch
        {
          this.lblMessage.Visible = true;
          this.lblMessage.Text = "Stock Minimo " + int32_2.ToString() + " NO Válido";
          return false;
        }
        try
        {
          num3 = int.Parse(dtDatos.Rows[index]["i_stockCritico"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        }
        catch
        {
          this.lblMessage.Visible = true;
          this.lblMessage.Text = "Stock Critico " + int32_3.ToString() + " NO Válido";
          return false;
        }
        try
        {
          num4 = int.Parse(dtDatos.Rows[index]["i_DaysStockSeg"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        }
        catch
        {
          this.lblMessage.Visible = true;
          this.lblMessage.Text = "Dias " + int32_4.ToString() + " NO Válido";
          return false;
        }
        try
        {
          num5 = int.Parse(dtDatos.Rows[index]["i_DaysDelivery"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        }
        catch
        {
          this.lblMessage.Visible = true;
          this.lblMessage.Text = "Dias " + int32_5.ToString() + " NO Válido";
          return false;
        }
        try
        {
          num6 = int.Parse(dtDatos.Rows[index]["i_DaysFreOrd"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        }
        catch
        {
          this.lblMessage.Visible = true;
          this.lblMessage.Text = "Dias " + int32_6.ToString() + " NO Válido";
          return false;
        }
      }
      return true;
    }

    private void ClearGridView()
    {
      this.wdgList.DataSource = (object) null;
      this.wdgList.DataBind();
    }

    private void PopupClose()
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
