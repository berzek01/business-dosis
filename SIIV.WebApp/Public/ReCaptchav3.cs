// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Public.ReCaptchav3
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

#nullable disable
namespace SIIV.WebApp.Public
{
  public class ReCaptchav3
  {
    public string Secret { get; set; }

    public string SiteKey { get; set; }

    public ReCaptchav3()
    {
      this.SiteKey = "6Ldeu48dAAAAAMEg9DDRybxrYbZezAj9bEVvVtiF";
      this.Secret = "6Ldeu48dAAAAAKhWRE38_1U2wKWNfetO8ejobNq-";
    }
  }
}
