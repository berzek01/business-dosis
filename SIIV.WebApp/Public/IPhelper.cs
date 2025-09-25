// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Public.IPhelper
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

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
