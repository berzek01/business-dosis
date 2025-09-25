// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.ServiceSoftnet.MercurioWS
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.WebApp.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

#nullable disable
namespace SIIV.WebApp.ServiceSoftnet
{
  [GeneratedCode("System.Web.Services", "4.8.3752.0")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [WebServiceBinding(Name = "MercurioWSSoap", Namespace = "http://tempuri.org/")]
  public class MercurioWS : SoapHttpClientProtocol
  {
    private SendOrPostCallback SendCPEOperationCompleted;
    private SendOrPostCallback SendCPE_APOperationCompleted;
    private SendOrPostCallback SendCPE_APRegOperationCompleted;
    private SendOrPostCallback ReadCPEOperationCompleted;
    private SendOrPostCallback LowCPEOperationCompleted;
    private SendOrPostCallback ReadDocumentCPEOperationCompleted;
    private SendOrPostCallback LowCPEGenerateOperationCompleted;
    private SendOrPostCallback ResumBolCPEGenerateOperationCompleted;
    private SendOrPostCallback SendCPE_ExtraccionOperationCompleted;
    private SendOrPostCallback SendCPE_Extraccion_MemoriaOperationCompleted;
    private SendOrPostCallback SendCpe_AdjuntoOperationCompleted;
    private SendOrPostCallback Consultar_Estado_DocumentosOperationCompleted;
    private bool useDefaultCredentialsSetExplicitly;

    public MercurioWS()
    {
      this.Url = Settings.Default.SIIV_WebApp_ServiceSoftnet_MercurioWS;
      if (this.IsLocalFileSystemWebService(this.Url))
      {
        this.UseDefaultCredentials = true;
        this.useDefaultCredentialsSetExplicitly = false;
      }
      else
        this.useDefaultCredentialsSetExplicitly = true;
    }

    public new string Url
    {
      get => base.Url;
      set
      {
        if (this.IsLocalFileSystemWebService(base.Url) && !this.useDefaultCredentialsSetExplicitly && !this.IsLocalFileSystemWebService(value))
          base.UseDefaultCredentials = false;
        base.Url = value;
      }
    }

    public new bool UseDefaultCredentials
    {
      get => base.UseDefaultCredentials;
      set
      {
        base.UseDefaultCredentials = value;
        this.useDefaultCredentialsSetExplicitly = true;
      }
    }

    public event SendCPECompletedEventHandler SendCPECompleted;

    public event SendCPE_APCompletedEventHandler SendCPE_APCompleted;

    public event SendCPE_APRegCompletedEventHandler SendCPE_APRegCompleted;

    public event ReadCPECompletedEventHandler ReadCPECompleted;

    public event LowCPECompletedEventHandler LowCPECompleted;

    public event ReadDocumentCPECompletedEventHandler ReadDocumentCPECompleted;

    public event LowCPEGenerateCompletedEventHandler LowCPEGenerateCompleted;

    public event ResumBolCPEGenerateCompletedEventHandler ResumBolCPEGenerateCompleted;

    public event SendCPE_ExtraccionCompletedEventHandler SendCPE_ExtraccionCompleted;

    public event SendCPE_Extraccion_MemoriaCompletedEventHandler SendCPE_Extraccion_MemoriaCompleted;

    public event SendCpe_AdjuntoCompletedEventHandler SendCpe_AdjuntoCompleted;

    public event Consultar_Estado_DocumentosCompletedEventHandler Consultar_Estado_DocumentosCompleted;

    [SoapDocumentMethod("http://tempuri.org/SendCPE", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
    public CPE_RESPUESTA_BE SendCPE(
      string Usuario,
      string Password,
      CPE_CABECERA_BE Cabecera,
      CPE_DETALLE_BE[] Detalles,
      CPE_DAT_ADIC_BE[] DatosAdicionales,
      CPE_DOC_REF_BE[] DocumentosReferenciados,
      CPE_ANTICIPO_BE[] Anticipos,
      CPE_FAC_GUIA_BE[] FacGuia)
    {
      return (CPE_RESPUESTA_BE) this.Invoke(nameof (SendCPE), new object[8]
      {
        (object) Usuario,
        (object) Password,
        (object) Cabecera,
        (object) Detalles,
        (object) DatosAdicionales,
        (object) DocumentosReferenciados,
        (object) Anticipos,
        (object) FacGuia
      })[0];
    }

    public void SendCPEAsync(
      string Usuario,
      string Password,
      CPE_CABECERA_BE Cabecera,
      CPE_DETALLE_BE[] Detalles,
      CPE_DAT_ADIC_BE[] DatosAdicionales,
      CPE_DOC_REF_BE[] DocumentosReferenciados,
      CPE_ANTICIPO_BE[] Anticipos,
      CPE_FAC_GUIA_BE[] FacGuia)
    {
      this.SendCPEAsync(Usuario, Password, Cabecera, Detalles, DatosAdicionales, DocumentosReferenciados, Anticipos, FacGuia, (object) null);
    }

    public void SendCPEAsync(
      string Usuario,
      string Password,
      CPE_CABECERA_BE Cabecera,
      CPE_DETALLE_BE[] Detalles,
      CPE_DAT_ADIC_BE[] DatosAdicionales,
      CPE_DOC_REF_BE[] DocumentosReferenciados,
      CPE_ANTICIPO_BE[] Anticipos,
      CPE_FAC_GUIA_BE[] FacGuia,
      object userState)
    {
      if (this.SendCPEOperationCompleted == null)
        this.SendCPEOperationCompleted = new SendOrPostCallback(this.OnSendCPEOperationCompleted);
      this.InvokeAsync("SendCPE", new object[8]
      {
        (object) Usuario,
        (object) Password,
        (object) Cabecera,
        (object) Detalles,
        (object) DatosAdicionales,
        (object) DocumentosReferenciados,
        (object) Anticipos,
        (object) FacGuia
      }, this.SendCPEOperationCompleted, userState);
    }

    private void OnSendCPEOperationCompleted(object arg)
    {
      if (this.SendCPECompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.SendCPECompleted((object) this, new SendCPECompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapDocumentMethod("http://tempuri.org/SendCPE_AP", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
    public CPE_RESPUESTA_BE SendCPE_AP(
      string Usuario,
      string Password,
      string tipoReg,
      CPE_CABECERA_BE Cabecera,
      CPE_DETALLE_BE[] Detalles,
      CPE_DAT_ADIC_BE[] DatosAdicionales,
      CPE_DOC_REF_BE[] DocumentosReferenciados,
      CPE_ANTICIPO_BE[] Anticipos,
      CPE_FAC_GUIA_BE[] FacGuia,
      CPE_DOC_ASOC_BE[] ListCpe_Doc,
      TipoSEND_CPE oTipoSendCPE,
      TipoCambio oTipoCambio)
    {
      return (CPE_RESPUESTA_BE) this.Invoke(nameof (SendCPE_AP), new object[12]
      {
        (object) Usuario,
        (object) Password,
        (object) tipoReg,
        (object) Cabecera,
        (object) Detalles,
        (object) DatosAdicionales,
        (object) DocumentosReferenciados,
        (object) Anticipos,
        (object) FacGuia,
        (object) ListCpe_Doc,
        (object) oTipoSendCPE,
        (object) oTipoCambio
      })[0];
    }

    public void SendCPE_APAsync(
      string Usuario,
      string Password,
      string tipoReg,
      CPE_CABECERA_BE Cabecera,
      CPE_DETALLE_BE[] Detalles,
      CPE_DAT_ADIC_BE[] DatosAdicionales,
      CPE_DOC_REF_BE[] DocumentosReferenciados,
      CPE_ANTICIPO_BE[] Anticipos,
      CPE_FAC_GUIA_BE[] FacGuia,
      CPE_DOC_ASOC_BE[] ListCpe_Doc,
      TipoSEND_CPE oTipoSendCPE,
      TipoCambio oTipoCambio)
    {
      this.SendCPE_APAsync(Usuario, Password, tipoReg, Cabecera, Detalles, DatosAdicionales, DocumentosReferenciados, Anticipos, FacGuia, ListCpe_Doc, oTipoSendCPE, oTipoCambio, (object) null);
    }

    public void SendCPE_APAsync(
      string Usuario,
      string Password,
      string tipoReg,
      CPE_CABECERA_BE Cabecera,
      CPE_DETALLE_BE[] Detalles,
      CPE_DAT_ADIC_BE[] DatosAdicionales,
      CPE_DOC_REF_BE[] DocumentosReferenciados,
      CPE_ANTICIPO_BE[] Anticipos,
      CPE_FAC_GUIA_BE[] FacGuia,
      CPE_DOC_ASOC_BE[] ListCpe_Doc,
      TipoSEND_CPE oTipoSendCPE,
      TipoCambio oTipoCambio,
      object userState)
    {
      if (this.SendCPE_APOperationCompleted == null)
        this.SendCPE_APOperationCompleted = new SendOrPostCallback(this.OnSendCPE_APOperationCompleted);
      this.InvokeAsync("SendCPE_AP", new object[12]
      {
        (object) Usuario,
        (object) Password,
        (object) tipoReg,
        (object) Cabecera,
        (object) Detalles,
        (object) DatosAdicionales,
        (object) DocumentosReferenciados,
        (object) Anticipos,
        (object) FacGuia,
        (object) ListCpe_Doc,
        (object) oTipoSendCPE,
        (object) oTipoCambio
      }, this.SendCPE_APOperationCompleted, userState);
    }

    private void OnSendCPE_APOperationCompleted(object arg)
    {
      if (this.SendCPE_APCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.SendCPE_APCompleted((object) this, new SendCPE_APCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapDocumentMethod("http://tempuri.org/SendCPE_APReg", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
    public CPE_RESPUESTA_BE SendCPE_APReg(
      string Usuario,
      string Password,
      string tipoReg,
      CPE_CABECERA_BE Cabecera,
      CPE_DETALLE_BE[] Detalles,
      CPE_DAT_ADIC_BE[] DatosAdicionales,
      CPE_DOC_REF_BE[] DocumentosReferenciados,
      CPE_ANTICIPO_BE[] Anticipos,
      CPE_FAC_GUIA_BE[] FacGuia,
      CPE_DOC_ASOC_BE[] ListCpe_Doc,
      TipoSEND_CPE oTipoSendCPE,
      TipoCambio oTipoCambio)
    {
      return (CPE_RESPUESTA_BE) this.Invoke(nameof (SendCPE_APReg), new object[12]
      {
        (object) Usuario,
        (object) Password,
        (object) tipoReg,
        (object) Cabecera,
        (object) Detalles,
        (object) DatosAdicionales,
        (object) DocumentosReferenciados,
        (object) Anticipos,
        (object) FacGuia,
        (object) ListCpe_Doc,
        (object) oTipoSendCPE,
        (object) oTipoCambio
      })[0];
    }

    public void SendCPE_APRegAsync(
      string Usuario,
      string Password,
      string tipoReg,
      CPE_CABECERA_BE Cabecera,
      CPE_DETALLE_BE[] Detalles,
      CPE_DAT_ADIC_BE[] DatosAdicionales,
      CPE_DOC_REF_BE[] DocumentosReferenciados,
      CPE_ANTICIPO_BE[] Anticipos,
      CPE_FAC_GUIA_BE[] FacGuia,
      CPE_DOC_ASOC_BE[] ListCpe_Doc,
      TipoSEND_CPE oTipoSendCPE,
      TipoCambio oTipoCambio)
    {
      this.SendCPE_APRegAsync(Usuario, Password, tipoReg, Cabecera, Detalles, DatosAdicionales, DocumentosReferenciados, Anticipos, FacGuia, ListCpe_Doc, oTipoSendCPE, oTipoCambio, (object) null);
    }

    public void SendCPE_APRegAsync(
      string Usuario,
      string Password,
      string tipoReg,
      CPE_CABECERA_BE Cabecera,
      CPE_DETALLE_BE[] Detalles,
      CPE_DAT_ADIC_BE[] DatosAdicionales,
      CPE_DOC_REF_BE[] DocumentosReferenciados,
      CPE_ANTICIPO_BE[] Anticipos,
      CPE_FAC_GUIA_BE[] FacGuia,
      CPE_DOC_ASOC_BE[] ListCpe_Doc,
      TipoSEND_CPE oTipoSendCPE,
      TipoCambio oTipoCambio,
      object userState)
    {
      if (this.SendCPE_APRegOperationCompleted == null)
        this.SendCPE_APRegOperationCompleted = new SendOrPostCallback(this.OnSendCPE_APRegOperationCompleted);
      this.InvokeAsync("SendCPE_APReg", new object[12]
      {
        (object) Usuario,
        (object) Password,
        (object) tipoReg,
        (object) Cabecera,
        (object) Detalles,
        (object) DatosAdicionales,
        (object) DocumentosReferenciados,
        (object) Anticipos,
        (object) FacGuia,
        (object) ListCpe_Doc,
        (object) oTipoSendCPE,
        (object) oTipoCambio
      }, this.SendCPE_APRegOperationCompleted, userState);
    }

    private void OnSendCPE_APRegOperationCompleted(object arg)
    {
      if (this.SendCPE_APRegCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.SendCPE_APRegCompleted((object) this, new SendCPE_APRegCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapDocumentMethod("http://tempuri.org/ReadCPE", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
    public CPE_RESPUESTA_BE ReadCPE(
      string Usuario,
      string Password,
      string Serie,
      string Correlativo,
      string TipoComprobante,
      string NumeroDocumentoIdentidad,
      string TipoDocumentoIdentidad)
    {
      return (CPE_RESPUESTA_BE) this.Invoke(nameof (ReadCPE), new object[7]
      {
        (object) Usuario,
        (object) Password,
        (object) Serie,
        (object) Correlativo,
        (object) TipoComprobante,
        (object) NumeroDocumentoIdentidad,
        (object) TipoDocumentoIdentidad
      })[0];
    }

    public void ReadCPEAsync(
      string Usuario,
      string Password,
      string Serie,
      string Correlativo,
      string TipoComprobante,
      string NumeroDocumentoIdentidad,
      string TipoDocumentoIdentidad)
    {
      this.ReadCPEAsync(Usuario, Password, Serie, Correlativo, TipoComprobante, NumeroDocumentoIdentidad, TipoDocumentoIdentidad, (object) null);
    }

    public void ReadCPEAsync(
      string Usuario,
      string Password,
      string Serie,
      string Correlativo,
      string TipoComprobante,
      string NumeroDocumentoIdentidad,
      string TipoDocumentoIdentidad,
      object userState)
    {
      if (this.ReadCPEOperationCompleted == null)
        this.ReadCPEOperationCompleted = new SendOrPostCallback(this.OnReadCPEOperationCompleted);
      this.InvokeAsync("ReadCPE", new object[7]
      {
        (object) Usuario,
        (object) Password,
        (object) Serie,
        (object) Correlativo,
        (object) TipoComprobante,
        (object) NumeroDocumentoIdentidad,
        (object) TipoDocumentoIdentidad
      }, this.ReadCPEOperationCompleted, userState);
    }

    private void OnReadCPEOperationCompleted(object arg)
    {
      if (this.ReadCPECompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.ReadCPECompleted((object) this, new ReadCPECompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapDocumentMethod("http://tempuri.org/LowCPE", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
    public CPE_RESPUESTA_BE[] LowCPE(string Usuario, string Password, CPE_DOC_BAJA[] Documentos)
    {
      return (CPE_RESPUESTA_BE[]) this.Invoke(nameof (LowCPE), new object[3]
      {
        (object) Usuario,
        (object) Password,
        (object) Documentos
      })[0];
    }

    public void LowCPEAsync(string Usuario, string Password, CPE_DOC_BAJA[] Documentos)
    {
      this.LowCPEAsync(Usuario, Password, Documentos, (object) null);
    }

    public void LowCPEAsync(
      string Usuario,
      string Password,
      CPE_DOC_BAJA[] Documentos,
      object userState)
    {
      if (this.LowCPEOperationCompleted == null)
        this.LowCPEOperationCompleted = new SendOrPostCallback(this.OnLowCPEOperationCompleted);
      this.InvokeAsync("LowCPE", new object[3]
      {
        (object) Usuario,
        (object) Password,
        (object) Documentos
      }, this.LowCPEOperationCompleted, userState);
    }

    private void OnLowCPEOperationCompleted(object arg)
    {
      if (this.LowCPECompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.LowCPECompleted((object) this, new LowCPECompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapDocumentMethod("http://tempuri.org/ReadDocumentCPE", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
    public CPE_DOC_TRIBUTARO_BE ReadDocumentCPE(
      string Usuario,
      string Password,
      string Serie,
      string Correlativo,
      string TipoComprobante,
      string NumeroDocumentoIdentidad,
      string TipoDocumentoIdentidad,
      bool XmlEnvio,
      bool XmlRespuesta,
      bool Impreso)
    {
      return (CPE_DOC_TRIBUTARO_BE) this.Invoke(nameof (ReadDocumentCPE), new object[10]
      {
        (object) Usuario,
        (object) Password,
        (object) Serie,
        (object) Correlativo,
        (object) TipoComprobante,
        (object) NumeroDocumentoIdentidad,
        (object) TipoDocumentoIdentidad,
        (object) XmlEnvio,
        (object) XmlRespuesta,
        (object) Impreso
      })[0];
    }

    public void ReadDocumentCPEAsync(
      string Usuario,
      string Password,
      string Serie,
      string Correlativo,
      string TipoComprobante,
      string NumeroDocumentoIdentidad,
      string TipoDocumentoIdentidad,
      bool XmlEnvio,
      bool XmlRespuesta,
      bool Impreso)
    {
      this.ReadDocumentCPEAsync(Usuario, Password, Serie, Correlativo, TipoComprobante, NumeroDocumentoIdentidad, TipoDocumentoIdentidad, XmlEnvio, XmlRespuesta, Impreso, (object) null);
    }

    public void ReadDocumentCPEAsync(
      string Usuario,
      string Password,
      string Serie,
      string Correlativo,
      string TipoComprobante,
      string NumeroDocumentoIdentidad,
      string TipoDocumentoIdentidad,
      bool XmlEnvio,
      bool XmlRespuesta,
      bool Impreso,
      object userState)
    {
      if (this.ReadDocumentCPEOperationCompleted == null)
        this.ReadDocumentCPEOperationCompleted = new SendOrPostCallback(this.OnReadDocumentCPEOperationCompleted);
      this.InvokeAsync("ReadDocumentCPE", new object[10]
      {
        (object) Usuario,
        (object) Password,
        (object) Serie,
        (object) Correlativo,
        (object) TipoComprobante,
        (object) NumeroDocumentoIdentidad,
        (object) TipoDocumentoIdentidad,
        (object) XmlEnvio,
        (object) XmlRespuesta,
        (object) Impreso
      }, this.ReadDocumentCPEOperationCompleted, userState);
    }

    private void OnReadDocumentCPEOperationCompleted(object arg)
    {
      if (this.ReadDocumentCPECompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.ReadDocumentCPECompleted((object) this, new ReadDocumentCPECompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapDocumentMethod("http://tempuri.org/LowCPEGenerate", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
    public CPE_RESPUESTA_BE LowCPEGenerate(
      string Usuario,
      string Password,
      string Nif,
      string FecResumen)
    {
      return (CPE_RESPUESTA_BE) this.Invoke(nameof (LowCPEGenerate), new object[4]
      {
        (object) Usuario,
        (object) Password,
        (object) Nif,
        (object) FecResumen
      })[0];
    }

    public void LowCPEGenerateAsync(
      string Usuario,
      string Password,
      string Nif,
      string FecResumen)
    {
      this.LowCPEGenerateAsync(Usuario, Password, Nif, FecResumen, (object) null);
    }

    public void LowCPEGenerateAsync(
      string Usuario,
      string Password,
      string Nif,
      string FecResumen,
      object userState)
    {
      if (this.LowCPEGenerateOperationCompleted == null)
        this.LowCPEGenerateOperationCompleted = new SendOrPostCallback(this.OnLowCPEGenerateOperationCompleted);
      this.InvokeAsync("LowCPEGenerate", new object[4]
      {
        (object) Usuario,
        (object) Password,
        (object) Nif,
        (object) FecResumen
      }, this.LowCPEGenerateOperationCompleted, userState);
    }

    private void OnLowCPEGenerateOperationCompleted(object arg)
    {
      if (this.LowCPEGenerateCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.LowCPEGenerateCompleted((object) this, new LowCPEGenerateCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapDocumentMethod("http://tempuri.org/ResumBolCPEGenerate", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
    public CPE_RESPUESTA_BE ResumBolCPEGenerate(
      string Usuario,
      string Password,
      string Nif,
      string FecEmisResum,
      string FecGenResum,
      TipoCambio oTipoCambio)
    {
      return (CPE_RESPUESTA_BE) this.Invoke(nameof (ResumBolCPEGenerate), new object[6]
      {
        (object) Usuario,
        (object) Password,
        (object) Nif,
        (object) FecEmisResum,
        (object) FecGenResum,
        (object) oTipoCambio
      })[0];
    }

    public void ResumBolCPEGenerateAsync(
      string Usuario,
      string Password,
      string Nif,
      string FecEmisResum,
      string FecGenResum,
      TipoCambio oTipoCambio)
    {
      this.ResumBolCPEGenerateAsync(Usuario, Password, Nif, FecEmisResum, FecGenResum, oTipoCambio, (object) null);
    }

    public void ResumBolCPEGenerateAsync(
      string Usuario,
      string Password,
      string Nif,
      string FecEmisResum,
      string FecGenResum,
      TipoCambio oTipoCambio,
      object userState)
    {
      if (this.ResumBolCPEGenerateOperationCompleted == null)
        this.ResumBolCPEGenerateOperationCompleted = new SendOrPostCallback(this.OnResumBolCPEGenerateOperationCompleted);
      this.InvokeAsync("ResumBolCPEGenerate", new object[6]
      {
        (object) Usuario,
        (object) Password,
        (object) Nif,
        (object) FecEmisResum,
        (object) FecGenResum,
        (object) oTipoCambio
      }, this.ResumBolCPEGenerateOperationCompleted, userState);
    }

    private void OnResumBolCPEGenerateOperationCompleted(object arg)
    {
      if (this.ResumBolCPEGenerateCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.ResumBolCPEGenerateCompleted((object) this, new ResumBolCPEGenerateCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapDocumentMethod("http://tempuri.org/SendCPE_Extraccion", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
    public CPE_RESPUESTA_BE SendCPE_Extraccion(
      string Usuario,
      string Password,
      string sNif,
      CPE_EXTRACCION_CABECERA[] ListaCabecera,
      CPE_EXTRACCION_DETALLE[] ListaDetalle,
      CPE_EXTRACCION_DOC_REFERENCIA[] ListaReferencia)
    {
      return (CPE_RESPUESTA_BE) this.Invoke(nameof (SendCPE_Extraccion), new object[6]
      {
        (object) Usuario,
        (object) Password,
        (object) sNif,
        (object) ListaCabecera,
        (object) ListaDetalle,
        (object) ListaReferencia
      })[0];
    }

    public void SendCPE_ExtraccionAsync(
      string Usuario,
      string Password,
      string sNif,
      CPE_EXTRACCION_CABECERA[] ListaCabecera,
      CPE_EXTRACCION_DETALLE[] ListaDetalle,
      CPE_EXTRACCION_DOC_REFERENCIA[] ListaReferencia)
    {
      this.SendCPE_ExtraccionAsync(Usuario, Password, sNif, ListaCabecera, ListaDetalle, ListaReferencia, (object) null);
    }

    public void SendCPE_ExtraccionAsync(
      string Usuario,
      string Password,
      string sNif,
      CPE_EXTRACCION_CABECERA[] ListaCabecera,
      CPE_EXTRACCION_DETALLE[] ListaDetalle,
      CPE_EXTRACCION_DOC_REFERENCIA[] ListaReferencia,
      object userState)
    {
      if (this.SendCPE_ExtraccionOperationCompleted == null)
        this.SendCPE_ExtraccionOperationCompleted = new SendOrPostCallback(this.OnSendCPE_ExtraccionOperationCompleted);
      this.InvokeAsync("SendCPE_Extraccion", new object[6]
      {
        (object) Usuario,
        (object) Password,
        (object) sNif,
        (object) ListaCabecera,
        (object) ListaDetalle,
        (object) ListaReferencia
      }, this.SendCPE_ExtraccionOperationCompleted, userState);
    }

    private void OnSendCPE_ExtraccionOperationCompleted(object arg)
    {
      if (this.SendCPE_ExtraccionCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.SendCPE_ExtraccionCompleted((object) this, new SendCPE_ExtraccionCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapDocumentMethod("http://tempuri.org/SendCPE_Extraccion_Memoria", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
    public CPE_RESPUESTA_BE[] SendCPE_Extraccion_Memoria(
      string Usuario,
      string Password,
      string sNif,
      CPE_EXTRACCION_CABECERA[] ListaCabecera,
      CPE_EXTRACCION_DETALLE[] ListaDetalle,
      CPE_EXTRACCION_DOC_REFERENCIA[] ListaReferencia)
    {
      return (CPE_RESPUESTA_BE[]) this.Invoke(nameof (SendCPE_Extraccion_Memoria), new object[6]
      {
        (object) Usuario,
        (object) Password,
        (object) sNif,
        (object) ListaCabecera,
        (object) ListaDetalle,
        (object) ListaReferencia
      })[0];
    }

    public void SendCPE_Extraccion_MemoriaAsync(
      string Usuario,
      string Password,
      string sNif,
      CPE_EXTRACCION_CABECERA[] ListaCabecera,
      CPE_EXTRACCION_DETALLE[] ListaDetalle,
      CPE_EXTRACCION_DOC_REFERENCIA[] ListaReferencia)
    {
      this.SendCPE_Extraccion_MemoriaAsync(Usuario, Password, sNif, ListaCabecera, ListaDetalle, ListaReferencia, (object) null);
    }

    public void SendCPE_Extraccion_MemoriaAsync(
      string Usuario,
      string Password,
      string sNif,
      CPE_EXTRACCION_CABECERA[] ListaCabecera,
      CPE_EXTRACCION_DETALLE[] ListaDetalle,
      CPE_EXTRACCION_DOC_REFERENCIA[] ListaReferencia,
      object userState)
    {
      if (this.SendCPE_Extraccion_MemoriaOperationCompleted == null)
        this.SendCPE_Extraccion_MemoriaOperationCompleted = new SendOrPostCallback(this.OnSendCPE_Extraccion_MemoriaOperationCompleted);
      this.InvokeAsync("SendCPE_Extraccion_Memoria", new object[6]
      {
        (object) Usuario,
        (object) Password,
        (object) sNif,
        (object) ListaCabecera,
        (object) ListaDetalle,
        (object) ListaReferencia
      }, this.SendCPE_Extraccion_MemoriaOperationCompleted, userState);
    }

    private void OnSendCPE_Extraccion_MemoriaOperationCompleted(object arg)
    {
      if (this.SendCPE_Extraccion_MemoriaCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.SendCPE_Extraccion_MemoriaCompleted((object) this, new SendCPE_Extraccion_MemoriaCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapDocumentMethod("http://tempuri.org/SendCpe_Adjunto", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
    public CPE_RESPUESTA_BE SendCpe_Adjunto(
      string Usuario,
      string Password,
      string sNif,
      [XmlElement(DataType = "base64Binary")] byte[] Pdf,
      string Num_cpe)
    {
      return (CPE_RESPUESTA_BE) this.Invoke(nameof (SendCpe_Adjunto), new object[5]
      {
        (object) Usuario,
        (object) Password,
        (object) sNif,
        (object) Pdf,
        (object) Num_cpe
      })[0];
    }

    public void SendCpe_AdjuntoAsync(
      string Usuario,
      string Password,
      string sNif,
      byte[] Pdf,
      string Num_cpe)
    {
      this.SendCpe_AdjuntoAsync(Usuario, Password, sNif, Pdf, Num_cpe, (object) null);
    }

    public void SendCpe_AdjuntoAsync(
      string Usuario,
      string Password,
      string sNif,
      byte[] Pdf,
      string Num_cpe,
      object userState)
    {
      if (this.SendCpe_AdjuntoOperationCompleted == null)
        this.SendCpe_AdjuntoOperationCompleted = new SendOrPostCallback(this.OnSendCpe_AdjuntoOperationCompleted);
      this.InvokeAsync("SendCpe_Adjunto", new object[5]
      {
        (object) Usuario,
        (object) Password,
        (object) sNif,
        (object) Pdf,
        (object) Num_cpe
      }, this.SendCpe_AdjuntoOperationCompleted, userState);
    }

    private void OnSendCpe_AdjuntoOperationCompleted(object arg)
    {
      if (this.SendCpe_AdjuntoCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.SendCpe_AdjuntoCompleted((object) this, new SendCpe_AdjuntoCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapDocumentMethod("http://tempuri.org/Consultar_Estado_Documentos", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
    public CPE_CABECERA_BE[] Consultar_Estado_Documentos(
      string Usuario,
      string Contrasena,
      string estado_sunat,
      string Nif)
    {
      return (CPE_CABECERA_BE[]) this.Invoke(nameof (Consultar_Estado_Documentos), new object[4]
      {
        (object) Usuario,
        (object) Contrasena,
        (object) estado_sunat,
        (object) Nif
      })[0];
    }

    public void Consultar_Estado_DocumentosAsync(
      string Usuario,
      string Contrasena,
      string estado_sunat,
      string Nif)
    {
      this.Consultar_Estado_DocumentosAsync(Usuario, Contrasena, estado_sunat, Nif, (object) null);
    }

    public void Consultar_Estado_DocumentosAsync(
      string Usuario,
      string Contrasena,
      string estado_sunat,
      string Nif,
      object userState)
    {
      if (this.Consultar_Estado_DocumentosOperationCompleted == null)
        this.Consultar_Estado_DocumentosOperationCompleted = new SendOrPostCallback(this.OnConsultar_Estado_DocumentosOperationCompleted);
      this.InvokeAsync("Consultar_Estado_Documentos", new object[4]
      {
        (object) Usuario,
        (object) Contrasena,
        (object) estado_sunat,
        (object) Nif
      }, this.Consultar_Estado_DocumentosOperationCompleted, userState);
    }

    private void OnConsultar_Estado_DocumentosOperationCompleted(object arg)
    {
      if (this.Consultar_Estado_DocumentosCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.Consultar_Estado_DocumentosCompleted((object) this, new Consultar_Estado_DocumentosCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    public new void CancelAsync(object userState) => base.CancelAsync(userState);

    private bool IsLocalFileSystemWebService(string url)
    {
      if (url == null || url == string.Empty)
        return false;
      Uri uri = new Uri(url);
      return uri.Port >= 1024 && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0;
    }
  }
}
