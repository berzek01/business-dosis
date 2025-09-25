// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibicion.Operation.AccountStatement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibicion.Operation
{
  public class AccountStatement : Page
  {
    protected HtmlForm form1;
    protected Label lblTipoPlaca;
    protected Label lblPlateType2;
    protected Label lblTelf;
    protected Label lblAnexo;

    protected void Page_Load(object sender, EventArgs e)
    {
      string str = Convert.ToString(this.Request.QueryString["i_CustomerTypeId"]);
      switch (str)
      {
        case "7":
          str = "7";
          break;
        case "11":
          str = "11";
          break;
      }
      switch (str)
      {
        case "7":
          this.lblTipoPlaca.Text = "Exhibición";
          this.lblTelf.Text = " 6403637";
          this.lblAnexo.Text = " 134";
          break;
        case "11":
          this.lblTipoPlaca.Text = "placas Rotativas";
          this.lblTelf.Text = " 6403637";
          this.lblAnexo.Text = " 174";
          break;
      }
    }
  }
}
