// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.CustomControls.LogicInconsistency
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims.CustomControls
{
  public class LogicInconsistency : UserControl
  {
    protected Label lblRequest1;
    protected TextBox txtRequest1;
    protected Label lblRequest2;
    protected TextBox txtRequest2;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public bool CompletedData
    {
      get => this.txtRequest1.Text.Length > 0 && this.txtRequest2.Text.Length > 0;
    }

    public void SetLabels(string strRequester)
    {
      string[] source = strRequester.Split('|');
      if (((IEnumerable<string>) source).Count<string>() > 1)
        this.lblRequest1.Text = source[1];
      if (((IEnumerable<string>) source).Count<string>() <= 2)
        return;
      this.lblRequest2.Text = source[2];
    }

    public string GetTexts() => "" + this.txtRequest1.Text + "|" + this.txtRequest2.Text;

    public void SetTexts(string strValues)
    {
      if (!(strValues != ""))
        return;
      string[] source = strValues.Split('|');
      if (((IEnumerable<string>) source).Count<string>() > 1)
        this.txtRequest1.Text = source[1];
      if (((IEnumerable<string>) source).Count<string>() > 2)
        this.txtRequest2.Text = source[2];
    }

    public void ClearControls()
    {
      this.txtRequest1.Text = "";
      this.txtRequest2.Text = "";
    }
  }
}
