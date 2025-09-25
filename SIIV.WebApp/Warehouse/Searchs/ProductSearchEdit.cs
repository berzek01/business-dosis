// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Searchs.ProductSearchEdit
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Searchs
{
  public class ProductSearchEdit : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtProductId;
    protected TextBox txtIncremento;
    protected TextBox txtInicial;
    protected TextBox txtFinal;
    protected Button btnProductAdd;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      List<string> stringList = this.Session["sEditProduct"] as List<string>;
      this.txtIncremento.Text = stringList[0].ToString((IFormatProvider) CultureInfo.CurrentCulture);
      this.txtInicial.Text = this.Page.Server.HtmlDecode(stringList[1].ToString((IFormatProvider) CultureInfo.CurrentCulture));
      this.txtFinal.Text = this.Page.Server.HtmlDecode(stringList[2].ToString((IFormatProvider) CultureInfo.CurrentCulture));
      this.txtProductId.Text = stringList[3].ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    protected void btnProductAdd_Click(object sender, EventArgs e)
    {
      this.SendInfoProductPopupClose();
    }

    private void SendInfoProductPopupClose()
    {
      this.Session["sEditProduct"] = (object) new List<string>()
      {
        this.txtIncremento.Text,
        this.txtInicial.Text,
        this.txtFinal.Text,
        this.txtProductId.Text
      };
      DataTable dataTable = this.Session["sedtProductAdd"] as DataTable;
      string script = "SendInfoProductPopup();";
      if (dataTable.Rows.Count == 0)
        script = "PopupClose();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }
  }
}
