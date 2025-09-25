// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Public.QueryEBilling
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Public
{
  public class QueryEBilling : Page
  {
    protected HtmlGenericControl table;
    protected HtmlGenericControl divComprobanteElectronico;
    protected Button btnHasta;
    protected Button btnDesde;
    protected PlaceHolder iframeDiv;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnHasta_Click(object sender, EventArgs e)
    {
      this.iframeDiv.Controls.Add((Control) new LiteralControl("<iframe style=\"border:none;height: 750px; width: 100%\" src=\"" + ConfigurationManager.AppSettings["EBillingQueryUrl"].ToString() + "\"></iframe><br />"));
      this.divComprobanteElectronico.Visible = false;
      this.table.Visible = false;
    }

    protected void btnDesde_Click(object sender, EventArgs e)
    {
      this.iframeDiv.Controls.Add((Control) new LiteralControl("<iframe style=\"border:none;height: 750px; width: 100%\" src=\"" + ConfigurationManager.AppSettings["EBillingQueryUrlSoftnet"].ToString() + "\"></iframe><br />"));
      this.divComprobanteElectronico.Visible = false;
      this.table.Visible = false;
    }
  }
}
