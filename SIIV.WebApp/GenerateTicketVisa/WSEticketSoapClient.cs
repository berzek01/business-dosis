// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.GenerateTicketVisa.WSEticketSoapClient
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

#nullable disable
namespace SIIV.WebApp.GenerateTicketVisa
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  public class WSEticketSoapClient : ClientBase<WSEticketSoap>, WSEticketSoap
  {
    public WSEticketSoapClient()
    {
    }

    public WSEticketSoapClient(string endpointConfigurationName)
      : base(endpointConfigurationName)
    {
    }

    public WSEticketSoapClient(string endpointConfigurationName, string remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public WSEticketSoapClient(string endpointConfigurationName, EndpointAddress remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public WSEticketSoapClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    GeneraEticketResponse WSEticketSoap.GeneraEticket(GeneraEticketRequest request)
    {
      return this.Channel.GeneraEticket(request);
    }

    public string GeneraEticket(string xmlIn)
    {
      GeneraEticketRequest request = new GeneraEticketRequest()
      {
        Body = new GeneraEticketRequestBody()
      };
      request.Body.xmlIn = xmlIn;
      return ((WSEticketSoap) this).GeneraEticket(request).Body.GeneraEticketResult;
    }
  }
}
