// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.GenerateTicketVisa.GeneraEticketResponseBody
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

#nullable disable
namespace SIIV.WebApp.GenerateTicketVisa
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  [DataContract(Namespace = "https://www.multimerchantvisanet.com/solicitudtransaccion")]
  public class GeneraEticketResponseBody
  {
    [DataMember(EmitDefaultValue = false, Order = 0)]
    public string GeneraEticketResult;

    public GeneraEticketResponseBody()
    {
    }

    public GeneraEticketResponseBody(string GeneraEticketResult)
    {
      this.GeneraEticketResult = GeneraEticketResult;
    }
  }
}
