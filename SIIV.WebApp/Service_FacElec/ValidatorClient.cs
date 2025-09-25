// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Service_FacElec.ValidatorClient
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

#nullable disable
namespace SIIV.WebApp.Service_FacElec
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  public class ValidatorClient : ClientBase<Validator>, Validator
  {
    public ValidatorClient()
    {
    }

    public ValidatorClient(string endpointConfigurationName)
      : base(endpointConfigurationName)
    {
    }

    public ValidatorClient(string endpointConfigurationName, string remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public ValidatorClient(string endpointConfigurationName, EndpointAddress remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public ValidatorClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    validateResponse Validator.validate(validateRequest request) => this.Channel.validate(request);

    public string validate(string xml)
    {
      validateRequest request = new validateRequest()
      {
        Body = new validateRequestBody()
      };
      request.Body.xml = xml;
      return ((Validator) this).validate(request).Body.validateReturn;
    }
  }
}
