// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibicion.Operation.GeneralStatement4
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

#nullable disable
namespace SIIV.WebApp.Exhibicion.Operation
{
  public class GeneralStatement4 : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;

    protected void Page_Load(object sender, EventArgs e)
    {
      Convert.ToString(this.Request.QueryString["i_CustomerTypeId"]);
      Convert.ToString(this.Request.QueryString["i_param"]);
    }
  }
}
