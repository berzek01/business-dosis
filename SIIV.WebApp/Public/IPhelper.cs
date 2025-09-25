// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Public.IPhelper
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System.Web;

#nullable disable
namespace SIIV.WebApp.Public
{
  public static class IPhelper
  {
    public static string GetIPAddress(this HttpRequest Request)
    {
      if (Request.Headers["CF-CONNECTING-IP"] != null)
        return Request.Headers["CF-CONNECTING-IP"].ToString();
      return Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null ? Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString() : Request.UserHostAddress;
    }
  }
}
