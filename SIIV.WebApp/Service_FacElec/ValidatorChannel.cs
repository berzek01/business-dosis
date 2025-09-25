// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Service_FacElec.ValidatorChannel
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using System.ServiceModel.Channels;

#nullable disable
namespace SIIV.WebApp.Service_FacElec
{
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  public interface ValidatorChannel : 
    Validator,
    IClientChannel,
    IContextChannel,
    IChannel,
    ICommunicationObject,
    IExtensibleObject<IContextChannel>,
    IDisposable
  {
  }
}
