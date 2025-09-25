// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.CustomControls.RequestData
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims.CustomControls
{
  public class RequestData : UserControl
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

    public void SetTextsUpdateApplicantData(string strValues)
    {
      if (!(strValues != ""))
        return;
      string[] source = strValues.Split('|');
      if (((IEnumerable<string>) source).Count<string>() > 0)
        this.txtRequest1.Text = source[0];
      if (((IEnumerable<string>) source).Count<string>() > 1)
        this.txtRequest2.Text = source[1];
      if (((IEnumerable<string>) source).Count<string>() > 2)
        this.Session["i_VehicleId"] = (object) source[2];
    }

    public void SetTexts(string strValues)
    {
      if (!(strValues != ""))
        return;
      string[] source = strValues.Split('|');
      if (((IEnumerable<string>) source).Count<string>() > 0)
        this.txtRequest1.Text = source[0];
      if (((IEnumerable<string>) source).Count<string>() > 1)
        this.txtRequest2.Text = source[1];
      if (((IEnumerable<string>) source).Count<string>() > 2)
        this.Session["i_VehicleId"] = (object) source[2];
    }

    public void ClearControls()
    {
      this.txtRequest1.Text = "";
      this.txtRequest2.Text = "";
    }

    public void EnableControls(bool habilita)
    {
      this.txtRequest1.Enabled = habilita;
      this.txtRequest2.Enabled = habilita;
    }
  }
}
