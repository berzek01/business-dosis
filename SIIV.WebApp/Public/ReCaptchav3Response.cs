// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Public.ReCaptchav3Response
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System;

#nullable disable
namespace SIIV.WebApp.Public
{
  public class ReCaptchav3Response
  {
    public bool success { get; set; }

    public DateTime challenge_ts { get; set; }

    public string hostname { get; set; }

    public float score { get; set; }
  }
}
