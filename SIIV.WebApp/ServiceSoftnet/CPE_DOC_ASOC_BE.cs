// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.ServiceSoftnet.CPE_DOC_ASOC_BE
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
  public class CPE_DOC_ASOC_BE
  {
    private string sEQ_DOC_ASOCField;
    private string nUM_CPEField;
    private string tXT_DOC_ASOCField;
    private byte[] dOC_ASOCField;
    private string tXT_TAM_DOCField;
    private int fLG_ACTIVOField;
    private DateTime fEC_CREAField;

    public string SEQ_DOC_ASOC
    {
      get => this.sEQ_DOC_ASOCField;
      set => this.sEQ_DOC_ASOCField = value;
    }

    public string NUM_CPE
    {
      get => this.nUM_CPEField;
      set => this.nUM_CPEField = value;
    }

    public string TXT_DOC_ASOC
    {
      get => this.tXT_DOC_ASOCField;
      set => this.tXT_DOC_ASOCField = value;
    }

    [XmlElement(DataType = "base64Binary")]
    public byte[] DOC_ASOC
    {
      get => this.dOC_ASOCField;
      set => this.dOC_ASOCField = value;
    }

    public string TXT_TAM_DOC
    {
      get => this.tXT_TAM_DOCField;
      set => this.tXT_TAM_DOCField = value;
    }

    public int FLG_ACTIVO
    {
      get => this.fLG_ACTIVOField;
      set => this.fLG_ACTIVOField = value;
    }

    public DateTime FEC_CREA
    {
      get => this.fEC_CREAField;
      set => this.fEC_CREAField = value;
    }
  }
}
