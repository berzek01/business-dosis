// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Public.EBilling
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.WebApp.ServiceSoftnet;
using System;
using System.Configuration;
using System.Net;
using System.Web.UI;
using System.Web.UI.HtmlControls;

#nullable disable
namespace SIIV.WebApp.Public
{
  public class EBilling : Page
  {
    protected HtmlForm form1;
    protected HtmlGenericControl frame1;

    protected void Page_Load(object sender, EventArgs e)
    {
      HtmlControl control = (HtmlControl) this.FindControl("frame1");
      if (this.Request.QueryString["fecha"] != null)
      {
        string str = ConfigurationManager.AppSettings["EBillingUrl"].ToString() + this.Request.QueryString["Url"].ToString() + "&fecha=" + this.Request.QueryString["fecha"].ToString() + "&monto=" + this.Request.QueryString["monto"].ToString() + "&moneda=" + this.Request.QueryString["moneda"].ToString();
        control.Attributes["src"] = str;
      }
      else
      {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        MercurioWS mercurioWs = new MercurioWS();
        CPE_DOC_TRIBUTARO_BE cpeDocTributaroBe = new CPE_DOC_TRIBUTARO_BE();
        byte[] docTribPdf = mercurioWs.ReadDocumentCPE(ConfigurationManager.AppSettings["UserServiceSofnet"].ToString(), ConfigurationManager.AppSettings["PasswordServiceSofnet"].ToString(), this.Request.QueryString["serie"].ToString(), this.Request.QueryString["correlativo"].ToString(), this.Request.QueryString["tipoComprobante"].ToString(), this.Request.QueryString["ruc"].ToString(), this.Request.QueryString["tipodocumento"].ToString(), false, false, true).DOC_TRIB_PDF;
        this.Response.Clear();
        this.Response.ContentType = "application/pdf";
        this.Response.AppendHeader("Content-Disposition", "inline;filename=" + this.Request.QueryString["ruc"].ToString() + "-" + this.Request.QueryString["tipoComprobante"].ToString() + "-" + this.Request.QueryString["serie"].ToString() + "-" + this.Request.QueryString["correlativo"].ToString() + ".pdf");
        this.Response.BufferOutput = true;
        this.Response.AddHeader("Content-Length", docTribPdf.Length.ToString());
        this.Response.BinaryWrite(docTribPdf);
        this.Response.End();
      }
    }
  }
}
