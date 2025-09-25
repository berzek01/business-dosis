// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Reports.CredencialesReporting
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using Microsoft.Reporting.WebForms;
using System.Net;
using System.Security.Principal;

#nullable disable
namespace SIIV.WebApp.Reports
{
  public class CredencialesReporting : IReportServerCredentials
  {
    private string _usuario;
    private string _password;
    private string _dominio;

    public CredencialesReporting(string userName, string password, string domain)
    {
      this._usuario = userName;
      this._password = password;
      this._dominio = domain;
    }

    public WindowsIdentity ImpersonationUser => (WindowsIdentity) null;

    public ICredentials NetworkCredentials
    {
      get => (ICredentials) new NetworkCredential(this._usuario, this._password, this._dominio);
    }

    public bool GetFormsCredentials(
      out Cookie authCoki,
      out string userName,
      out string password,
      out string authority)
    {
      userName = this._usuario;
      password = this._password;
      authority = this._dominio;
      authCoki = new Cookie(".ASPXAUTH", ".ASPXAUTH", "/", "Domain");
      return true;
    }
  }
}
