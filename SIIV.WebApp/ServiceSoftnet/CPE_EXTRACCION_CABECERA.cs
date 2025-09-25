// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.ServiceSoftnet.CPE_EXTRACCION_CABECERA
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#nullable disable
namespace SIIV.WebApp.ServiceSoftnet
{
  [GeneratedCode("System.Xml", "4.8.3752.0")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://tempuri.org/")]
  [Serializable]
  public class CPE_EXTRACCION_CABECERA
  {
    private string cod_companiaField;
    private string num_seccabField;
    private string num_movimiento_ventaField;
    private string num_nota_creditoField;
    private string num_nota_debitoField;
    private string cod_tipo_documentoField;
    private string num_documentoField;
    private string num_serieField;
    private string sec_comprobField;
    private string cod_localidadField;
    private string cod_monedaField;
    private string cod_clienteField;
    private string cod_termino_pagoField;
    private string fch_documentoField;
    private string num_dias_vencimientoField;
    private string imp_tipo_cambioField;
    private string imp_total_ventaField;
    private string imp_total_inafectoField;
    private string imp_total_impuestoField;
    private string cod_glosaField;
    private string dsc_conceptoField;
    private string dsc_observacionField;
    private string nom_clienteField;
    private string ruc_clienteField;
    private string tlf_clienteField;
    private string direc_clienteField;
    private string num_anexoField;
    private string cod_usu_auditField;
    private string fch_ultact_auditField;
    private string cod_tip_pedidoField;
    private string flg_fact_electronicaField;
    private string cod_hashField;
    private string cod_tipo_pedidoField;
    private string flg_fact_confirmadaField;
    private string dsc_distrito_locField;
    private string dsc_provincia_locField;
    private string dsc_departamento_locField;
    private string dsc_departamentoField;
    private string dsc_provinciaField;
    private string dsc_distritoField;
    private string imp_total_valorventaField;
    private string imp_total_exoneradoField;
    private string cod_tipo_doc_sunatField;
    private string dsc_monto_letrasField;
    private string tip_afectacion_igvField;
    private string dsc_financieroField;
    private string tip_motivo_ndField;
    private string tip_motivo_ncField;
    private byte[] dsc_anexo_pdfField;
    private string dsc_emailField;

    public string cod_compania
    {
      get => this.cod_companiaField;
      set => this.cod_companiaField = value;
    }

    public string num_seccab
    {
      get => this.num_seccabField;
      set => this.num_seccabField = value;
    }

    public string num_movimiento_venta
    {
      get => this.num_movimiento_ventaField;
      set => this.num_movimiento_ventaField = value;
    }

    public string num_nota_credito
    {
      get => this.num_nota_creditoField;
      set => this.num_nota_creditoField = value;
    }

    public string num_nota_debito
    {
      get => this.num_nota_debitoField;
      set => this.num_nota_debitoField = value;
    }

    public string cod_tipo_documento
    {
      get => this.cod_tipo_documentoField;
      set => this.cod_tipo_documentoField = value;
    }

    public string num_documento
    {
      get => this.num_documentoField;
      set => this.num_documentoField = value;
    }

    public string num_serie
    {
      get => this.num_serieField;
      set => this.num_serieField = value;
    }

    public string sec_comprob
    {
      get => this.sec_comprobField;
      set => this.sec_comprobField = value;
    }

    public string cod_localidad
    {
      get => this.cod_localidadField;
      set => this.cod_localidadField = value;
    }

    public string cod_moneda
    {
      get => this.cod_monedaField;
      set => this.cod_monedaField = value;
    }

    public string cod_cliente
    {
      get => this.cod_clienteField;
      set => this.cod_clienteField = value;
    }

    public string cod_termino_pago
    {
      get => this.cod_termino_pagoField;
      set => this.cod_termino_pagoField = value;
    }

    public string fch_documento
    {
      get => this.fch_documentoField;
      set => this.fch_documentoField = value;
    }

    public string num_dias_vencimiento
    {
      get => this.num_dias_vencimientoField;
      set => this.num_dias_vencimientoField = value;
    }

    public string imp_tipo_cambio
    {
      get => this.imp_tipo_cambioField;
      set => this.imp_tipo_cambioField = value;
    }

    public string imp_total_venta
    {
      get => this.imp_total_ventaField;
      set => this.imp_total_ventaField = value;
    }

    public string imp_total_inafecto
    {
      get => this.imp_total_inafectoField;
      set => this.imp_total_inafectoField = value;
    }

    public string imp_total_impuesto
    {
      get => this.imp_total_impuestoField;
      set => this.imp_total_impuestoField = value;
    }

    public string cod_glosa
    {
      get => this.cod_glosaField;
      set => this.cod_glosaField = value;
    }

    public string dsc_concepto
    {
      get => this.dsc_conceptoField;
      set => this.dsc_conceptoField = value;
    }

    public string dsc_observacion
    {
      get => this.dsc_observacionField;
      set => this.dsc_observacionField = value;
    }

    public string nom_cliente
    {
      get => this.nom_clienteField;
      set => this.nom_clienteField = value;
    }

    public string ruc_cliente
    {
      get => this.ruc_clienteField;
      set => this.ruc_clienteField = value;
    }

    public string tlf_cliente
    {
      get => this.tlf_clienteField;
      set => this.tlf_clienteField = value;
    }

    public string direc_cliente
    {
      get => this.direc_clienteField;
      set => this.direc_clienteField = value;
    }

    public string num_anexo
    {
      get => this.num_anexoField;
      set => this.num_anexoField = value;
    }

    public string cod_usu_audit
    {
      get => this.cod_usu_auditField;
      set => this.cod_usu_auditField = value;
    }

    public string fch_ultact_audit
    {
      get => this.fch_ultact_auditField;
      set => this.fch_ultact_auditField = value;
    }

    public string cod_tip_pedido
    {
      get => this.cod_tip_pedidoField;
      set => this.cod_tip_pedidoField = value;
    }

    public string flg_fact_electronica
    {
      get => this.flg_fact_electronicaField;
      set => this.flg_fact_electronicaField = value;
    }

    public string cod_hash
    {
      get => this.cod_hashField;
      set => this.cod_hashField = value;
    }

    public string cod_tipo_pedido
    {
      get => this.cod_tipo_pedidoField;
      set => this.cod_tipo_pedidoField = value;
    }

    public string flg_fact_confirmada
    {
      get => this.flg_fact_confirmadaField;
      set => this.flg_fact_confirmadaField = value;
    }

    public string dsc_distrito_loc
    {
      get => this.dsc_distrito_locField;
      set => this.dsc_distrito_locField = value;
    }

    public string dsc_provincia_loc
    {
      get => this.dsc_provincia_locField;
      set => this.dsc_provincia_locField = value;
    }

    public string dsc_departamento_loc
    {
      get => this.dsc_departamento_locField;
      set => this.dsc_departamento_locField = value;
    }

    public string dsc_departamento
    {
      get => this.dsc_departamentoField;
      set => this.dsc_departamentoField = value;
    }

    public string dsc_provincia
    {
      get => this.dsc_provinciaField;
      set => this.dsc_provinciaField = value;
    }

    public string dsc_distrito
    {
      get => this.dsc_distritoField;
      set => this.dsc_distritoField = value;
    }

    public string imp_total_valorventa
    {
      get => this.imp_total_valorventaField;
      set => this.imp_total_valorventaField = value;
    }

    public string imp_total_exonerado
    {
      get => this.imp_total_exoneradoField;
      set => this.imp_total_exoneradoField = value;
    }

    public string cod_tipo_doc_sunat
    {
      get => this.cod_tipo_doc_sunatField;
      set => this.cod_tipo_doc_sunatField = value;
    }

    public string dsc_monto_letras
    {
      get => this.dsc_monto_letrasField;
      set => this.dsc_monto_letrasField = value;
    }

    public string tip_afectacion_igv
    {
      get => this.tip_afectacion_igvField;
      set => this.tip_afectacion_igvField = value;
    }

    public string dsc_financiero
    {
      get => this.dsc_financieroField;
      set => this.dsc_financieroField = value;
    }

    public string tip_motivo_nd
    {
      get => this.tip_motivo_ndField;
      set => this.tip_motivo_ndField = value;
    }

    public string tip_motivo_nc
    {
      get => this.tip_motivo_ncField;
      set => this.tip_motivo_ncField = value;
    }

    [XmlElement(DataType = "base64Binary")]
    public byte[] dsc_anexo_pdf
    {
      get => this.dsc_anexo_pdfField;
      set => this.dsc_anexo_pdfField = value;
    }

    public string dsc_email
    {
      get => this.dsc_emailField;
      set => this.dsc_emailField = value;
    }
  }
}
