// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.QueryETicket.WSConsultaEticketSoap
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System.CodeDom.Compiler;
using System.ServiceModel;

#nullable disable
namespace SIIV.WebApp.QueryETicket
{
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  [ServiceContract(Namespace = "https://www.multimerchantvisanet.com/ConsultaEnLineaEticket", ConfigurationName = "QueryETicket.WSConsultaEticketSoap")]
  public interface WSConsultaEticketSoap
  {
    [OperationContract(Action = "https://www.multimerchantvisanet.com/ConsultaEnLineaEticket/ConsultaEticket", ReplyAction = "*")]
    ConsultaEticketResponse ConsultaEticket(ConsultaEticketRequest request);
  }
}
