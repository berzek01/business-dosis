// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.proxyServiceMTC.SunarpSoapChannel
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using System.ServiceModel.Channels;

#nullable disable
namespace SIIV.WebApp.proxyServiceMTC
{
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  public interface SunarpSoapChannel : 
    SunarpSoap,
    IClientChannel,
    IContextChannel,
    IChannel,
    ICommunicationObject,
    IExtensibleObject<IContextChannel>,
    IDisposable
  {
  }
}
