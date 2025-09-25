// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.QueryETicket.ConsultaEticketResponseBody
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

#nullable disable
namespace SIIV.WebApp.QueryETicket
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  [DataContract(Namespace = "https://www.multimerchantvisanet.com/ConsultaEnLineaEticket")]
  public class ConsultaEticketResponseBody
  {
    [DataMember(EmitDefaultValue = false, Order = 0)]
    public string ConsultaEticketResult;

    public ConsultaEticketResponseBody()
    {
    }

    public ConsultaEticketResponseBody(string ConsultaEticketResult)
    {
      this.ConsultaEticketResult = ConsultaEticketResult;
    }
  }
}
