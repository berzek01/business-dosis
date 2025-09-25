// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.ServiceSoftnet.LowCPEGenerateCompletedEventArgs
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
  public class LowCPEGenerateCompletedEventArgs : AsyncCompletedEventArgs
  {
    private object[] results;

    internal LowCPEGenerateCompletedEventArgs(
      object[] results,
      Exception exception,
      bool cancelled,
      object userState)
      : base(exception, cancelled, userState)
    {
      this.results = results;
    }

    public CPE_RESPUESTA_BE Result
    {
      get
      {
        this.RaiseExceptionIfNecessary();
        return (CPE_RESPUESTA_BE) this.results[0];
      }
    }
  }
}
