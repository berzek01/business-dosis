// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.QueryETicket.WSConsultaEticketSoapClient
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

#nullable disable
namespace SIIV.WebApp.QueryETicket
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  public class WSConsultaEticketSoapClient : ClientBase<WSConsultaEticketSoap>, WSConsultaEticketSoap
  {
    public WSConsultaEticketSoapClient()
    {
    }

    public WSConsultaEticketSoapClient(string endpointConfigurationName)
      : base(endpointConfigurationName)
    {
    }

    public WSConsultaEticketSoapClient(string endpointConfigurationName, string remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public WSConsultaEticketSoapClient(
      string endpointConfigurationName,
      EndpointAddress remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public WSConsultaEticketSoapClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    ConsultaEticketResponse WSConsultaEticketSoap.ConsultaEticket(ConsultaEticketRequest request)
    {
      return this.Channel.ConsultaEticket(request);
    }

    public string ConsultaEticket(string xmlIn)
    {
      ConsultaEticketRequest request = new ConsultaEticketRequest()
      {
        Body = new ConsultaEticketRequestBody()
      };
      request.Body.xmlIn = xmlIn;
      return ((WSConsultaEticketSoap) this).ConsultaEticket(request).Body.ConsultaEticketResult;
    }
  }
}
