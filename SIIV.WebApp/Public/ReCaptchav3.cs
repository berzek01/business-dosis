// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Public.ReCaptchav3
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

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
