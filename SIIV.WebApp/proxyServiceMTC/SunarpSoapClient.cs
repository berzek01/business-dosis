// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.proxyServiceMTC.SunarpSoapClient
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

#nullable disable
namespace SIIV.WebApp.proxyServiceMTC
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  public class SunarpSoapClient : ClientBase<SunarpSoap>, SunarpSoap
  {
    public SunarpSoapClient()
    {
    }

    public SunarpSoapClient(string endpointConfigurationName)
      : base(endpointConfigurationName)
    {
    }

    public SunarpSoapClient(string endpointConfigurationName, string remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public SunarpSoapClient(string endpointConfigurationName, EndpointAddress remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public SunarpSoapClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    public DatosRetorno DatosH_VehiculoSUNARP(DateTime fechadespacho)
    {
      return this.Channel.DatosH_VehiculoSUNARP(fechadespacho);
    }

    public DatosRetorno DatosH_VehiculoSUNARPxPlaca(string placaNueva)
    {
      return this.Channel.DatosH_VehiculoSUNARPxPlaca(placaNueva);
    }
  }
}
