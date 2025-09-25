// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.proxyServiceMTC.SunarpSoap
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.CodeDom.Compiler;
using System.ServiceModel;

#nullable disable
namespace SIIV.WebApp.proxyServiceMTC
{
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  [ServiceContract(ConfigurationName = "proxyServiceMTC.SunarpSoap")]
  public interface SunarpSoap
  {
    [OperationContract(Action = "http://tempuri.org/DatosH_VehiculoSUNARP", ReplyAction = "*")]
    [XmlSerializerFormat(SupportFaults = true)]
    DatosRetorno DatosH_VehiculoSUNARP(DateTime fechadespacho);

    [OperationContract(Action = "http://tempuri.org/DatosH_VehiculoSUNARPxPlaca", ReplyAction = "*")]
    [XmlSerializerFormat(SupportFaults = true)]
    DatosRetorno DatosH_VehiculoSUNARPxPlaca(string placaNueva);
  }
}
