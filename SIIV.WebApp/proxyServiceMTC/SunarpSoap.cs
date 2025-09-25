// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.proxyServiceMTC.SunarpSoap
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

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
