// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.ServiceSoftnet.CPE_EXTRACCION_DOC_REFERENCIA
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

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
  public class CPE_EXTRACCION_DOC_REFERENCIA
  {
    private string cod_companiaField;
    private string num_seccabField;
    private string num_secdetField;
    private string num_nota_creditoField;
    private string num_nota_debitoField;
    private string num_movimiento_ventaField;
    private string num_serieField;
    private string sec_comprobField;

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

    public string num_secdet
    {
      get => this.num_secdetField;
      set => this.num_secdetField = value;
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

    public string num_movimiento_venta
    {
      get => this.num_movimiento_ventaField;
      set => this.num_movimiento_ventaField = value;
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
  }
}
