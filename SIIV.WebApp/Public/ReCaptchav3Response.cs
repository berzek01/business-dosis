// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Public.ReCaptchav3Response
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

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
