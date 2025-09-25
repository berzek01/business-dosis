// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.ServiceSoftnet.SendCPE_Extraccion_MemoriaCompletedEventArgs
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

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
  public class SendCPE_Extraccion_MemoriaCompletedEventArgs : AsyncCompletedEventArgs
  {
    private object[] results;

    internal SendCPE_Extraccion_MemoriaCompletedEventArgs(
      object[] results,
      Exception exception,
      bool cancelled,
      object userState)
      : base(exception, cancelled, userState)
    {
      this.results = results;
    }

    public CPE_RESPUESTA_BE[] Result
    {
      get
      {
        this.RaiseExceptionIfNecessary();
        return (CPE_RESPUESTA_BE[]) this.results[0];
      }
    }
  }
}
