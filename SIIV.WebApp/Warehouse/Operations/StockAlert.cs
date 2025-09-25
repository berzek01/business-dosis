// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.StockAlert
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class StockAlert : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected GridView wdgProductDetail;
    protected CheckBox chkconfir;
    protected Button wibAceptar;
    protected Label lblmsn;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.wibAceptar.Enabled = false;
      ProductWarehouseQueriesBL warehouseQueriesBl = new ProductWarehouseQueriesBL();
      DataTable dataTable1 = new DataTable();
      DataTable dataTable2 = new DataTable();
      DataTable dataTable3 = warehouseQueriesBl.ProductStockAlert(1);
      this.ViewState["dt_StockProdDetail"] = (object) dataTable3;
      this.wdgProductDetail.DataSource = (object) dataTable3;
      this.wdgProductDetail.DataBind();
    }

    protected void chkconfir_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkconfir.Checked)
        this.wibAceptar.Enabled = true;
      else
        this.wibAceptar.Enabled = false;
    }

    protected void wibAceptar_Click(object sender, EventArgs e)
    {
      try
      {
        Email email = new Email();
        DataTable dataTable1 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.SMTPServerConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture),
          (object) "",
          (object) "1",
          (object) "1"
        });
        string pstrSMTPServer = dataTable1.Rows[0]["v_Value"].ToString();
        int pintSMTPPort = int.Parse(dataTable1.Rows[1]["v_Value"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        string str1 = dataTable1.Rows[2]["v_Value"].ToString();
        string pstrSMTPPassword = dataTable1.Rows[3]["v_Value"].ToString();
        bool boolean = Convert.ToBoolean(dataTable1.Rows[4]["v_Value"], (IFormatProvider) CultureInfo.CurrentCulture);
        if (this.ViewState["dt_StockProdDetail"] != null)
        {
          ProductWarehouseQueriesBL warehouseQueriesBl = new ProductWarehouseQueriesBL();
          DataTable dataTable2 = new DataTable();
          SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
          StringBuilder stringBuilder = new StringBuilder();
          string pstrEmailSubject = "Alertas de Stock por Producto";
          DataTable dataTable3 = (DataTable) this.ViewState["dt_StockProdDetail"];
          if (dataTable3.Rows.Count > 0)
          {
            stringBuilder.AppendLine("<html><body>");
            stringBuilder.AppendLine("A continuación se muestran los productos que sobrepasan el stock mínimo y stock crítico:<br><br>");
            stringBuilder.AppendLine("<table border='1'>");
            stringBuilder.AppendLine("<tr><td><b>Producto</b></td><td><b>Stock Actual</b></td><td><b>Stock Mínimo</b></td><td><b>Stock Crítico</b></td><td><b>Estado</b></td></tr>");
            foreach (DataRow row in (InternalDataCollectionBase) dataTable3.Rows)
            {
              warehouseQueriesBl.ProductStockUpdateStatus(Convert.ToInt32(row["i_ProductStockId"].ToString()), Convert.ToInt32(row["i_ProductStockStatusNew"].ToString()), 1, systemUser.i_SystemUserId);
              stringBuilder.AppendLine("<tr><td>" + row["Producto"].ToString() + "</td>");
              stringBuilder.AppendLine("<td>" + row["Stock"].ToString() + "</td>");
              stringBuilder.AppendLine("<td>" + row["StockMin"].ToString() + "</td>");
              stringBuilder.AppendLine("<td>" + row["StockCrit"].ToString() + "</td>");
              stringBuilder.AppendLine("<td>" + row["Message"].ToString() + "</td></tr>");
            }
            stringBuilder.AppendLine("</table>");
            stringBuilder.AppendLine("</body></html>");
            List<string> pstrEmailTo = new List<string>();
            if (ConfigurationManager.AppSettings["Email_UserAlertStock"] != null)
            {
              string str2 = ConfigurationManager.AppSettings["Email_UserAlertStock"].ToString().Trim();
              char[] chArray = new char[1]{ '|' };
              foreach (string str3 in str2.Split(chArray))
              {
                if (!string.IsNullOrEmpty(str3))
                  pstrEmailTo.Add(str3.ToString());
              }
            }
            string pstrEmailBody = stringBuilder.ToString();
            List<string> pstrEmailCC = new List<string>();
            if (!Email.SendEmail(str1, pstrSMTPPassword, pstrEmailSubject, pstrEmailBody, pstrEmailTo, pstrEmailCC, pstrSMTPServer, pintSMTPPort, str1, boolean))
              Message.SetMessage(this.lblmsn, enmMessageType.Error, "Error al enviar el mail...");
            else
              Message.SetMessage(this.lblmsn, enmMessageType.Success, "Mail enviado correctamente...");
          }
        }
        string script = "SendInfoPopup();";
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
      catch (Exception ex)
      {
        throw;
      }
      finally
      {
        this.ViewState.Remove("dt_StockProdDetail");
      }
    }

    private DataTable DT()
    {
      return new DataTable()
      {
        Columns = {
          {
            "ProductStockId",
            typeof (int)
          },
          {
            "IdProducto",
            typeof (int)
          },
          {
            "Producto",
            typeof (string)
          },
          {
            "Stock",
            typeof (int)
          },
          {
            "StockMin",
            typeof (int)
          },
          {
            "StockCrit",
            typeof (int)
          },
          {
            "Message",
            typeof (string)
          },
          {
            "ProductStockStatus",
            typeof (int)
          }
        }
      };
    }
  }
}
