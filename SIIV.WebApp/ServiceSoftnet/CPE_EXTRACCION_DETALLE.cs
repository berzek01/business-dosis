// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.ServiceSoftnet.CPE_EXTRACCION_DETALLE
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
  public class CPE_EXTRACCION_DETALLE
  {
    private string cod_companiaField;
    private string num_seccabField;
    private string num_secitemField;
    private string num_movimiento_ventaField;
    private string num_nota_creditoField;
    private string num_nota_debitoField;
    private string cod_articuloField;
    private string dsc_articuloField;
    private string ctd_cantidadField;
    private string imp_precio_origenField;
    private string imp_porc_impuestoField;
    private string cod_motivo_notaField;
    private string dsc_motivo_notaField;
    private string imp_subtotal_itemField;
    private string imp_precio_unitario_igvField;

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

    public string num_secitem
    {
      get => this.num_secitemField;
      set => this.num_secitemField = value;
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

    public string cod_articulo
    {
      get => this.cod_articuloField;
      set => this.cod_articuloField = value;
    }

    public string dsc_articulo
    {
      get => this.dsc_articuloField;
      set => this.dsc_articuloField = value;
    }

    public string ctd_cantidad
    {
      get => this.ctd_cantidadField;
      set => this.ctd_cantidadField = value;
    }

    public string imp_precio_origen
    {
      get => this.imp_precio_origenField;
      set => this.imp_precio_origenField = value;
    }

    public string imp_porc_impuesto
    {
      get => this.imp_porc_impuestoField;
      set => this.imp_porc_impuestoField = value;
    }

    public string cod_motivo_nota
    {
      get => this.cod_motivo_notaField;
      set => this.cod_motivo_notaField = value;
    }

    public string dsc_motivo_nota
    {
      get => this.dsc_motivo_notaField;
      set => this.dsc_motivo_notaField = value;
    }

    public string imp_subtotal_item
    {
      get => this.imp_subtotal_itemField;
      set => this.imp_subtotal_itemField = value;
    }

    public string imp_precio_unitario_igv
    {
      get => this.imp_precio_unitario_igvField;
      set => this.imp_precio_unitario_igvField = value;
    }
  }
}
