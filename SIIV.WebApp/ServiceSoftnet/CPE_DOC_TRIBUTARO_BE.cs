// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.ServiceSoftnet.CPE_DOC_TRIBUTARO_BE
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
  public class CPE_DOC_TRIBUTARO_BE
  {
    private long? nUM_CPEField;
    private long? nUM_RESUMField;
    private string dOC_TRIB_XML_ENVIOField;
    private string dOC_TRIB_XML_RPTAField;
    private byte[] dOC_TRIB_PDFField;
    private string nOM_ARCHIVO_XMLField;
    private string tIP_DOCUMENTOField;

    [XmlElement(IsNullable = true)]
    public long? NUM_CPE
    {
      get => this.nUM_CPEField;
      set => this.nUM_CPEField = value;
    }

    [XmlElement(IsNullable = true)]
    public long? NUM_RESUM
    {
      get => this.nUM_RESUMField;
      set => this.nUM_RESUMField = value;
    }

    public string DOC_TRIB_XML_ENVIO
    {
      get => this.dOC_TRIB_XML_ENVIOField;
      set => this.dOC_TRIB_XML_ENVIOField = value;
    }

    public string DOC_TRIB_XML_RPTA
    {
      get => this.dOC_TRIB_XML_RPTAField;
      set => this.dOC_TRIB_XML_RPTAField = value;
    }

    [XmlElement(DataType = "base64Binary")]
    public byte[] DOC_TRIB_PDF
    {
      get => this.dOC_TRIB_PDFField;
      set => this.dOC_TRIB_PDFField = value;
    }

    public string NOM_ARCHIVO_XML
    {
      get => this.nOM_ARCHIVO_XMLField;
      set => this.nOM_ARCHIVO_XMLField = value;
    }

    public string TIP_DOCUMENTO
    {
      get => this.tIP_DOCUMENTOField;
      set => this.tIP_DOCUMENTOField = value;
    }
  }
}
