// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.ServiceSoftnet.Consultar_Estado_DocumentosCompletedEventArgs
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace SIIV.WebApp.ServiceSoftnet
{
  [GeneratedCode("System.Web.Services", "4.8.3752.0")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  public class Consultar_Estado_DocumentosCompletedEventArgs : AsyncCompletedEventArgs
  {
    private object[] results;

    internal Consultar_Estado_DocumentosCompletedEventArgs(
      object[] results,
      Exception exception,
      bool cancelled,
      object userState)
      : base(exception, cancelled, userState)
    {
      this.results = results;
    }

    public CPE_CABECERA_BE[] Result
    {
      get
      {
        this.RaiseExceptionIfNecessary();
        return (CPE_CABECERA_BE[]) this.results[0];
      }
    }
  }
}
