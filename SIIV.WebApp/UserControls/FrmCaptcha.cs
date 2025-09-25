// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.UserControls.FrmCaptcha
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.Drawing.Imaging;
using System.Web.UI;
using System.Web.UI.HtmlControls;

#nullable disable
namespace SIIV.WebApp.UserControls
{
  public class FrmCaptcha : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Session["CaptchaImageText"] = (object) this.GenerateRandomCode();
      Captcha captcha = new Captcha(this.Session["CaptchaImageText"].ToString(), 300, 75);
      this.Response.Clear();
      this.Response.ContentType = "image/jpeg";
      captcha.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);
      captcha.Dispose();
    }

    private string GenerateRandomCode()
    {
      Random random = new Random();
      string str = "";
      for (int index = 0; index < 5; ++index)
      {
        int num;
        switch (random.Next(3))
        {
          case 1:
            num = random.Next(0, 9);
            str += num.ToString();
            break;
          case 2:
            num = random.Next(65, 90);
            str += Convert.ToChar(num).ToString();
            break;
          case 3:
            num = random.Next(97, 122);
            str += Convert.ToChar(num).ToString();
            break;
          default:
            num = random.Next(97, 122);
            str += Convert.ToChar(num).ToString();
            break;
        }
        random.NextDouble();
        random.Next(100, 1999);
      }
      return str.ToUpper();
    }
  }
}
