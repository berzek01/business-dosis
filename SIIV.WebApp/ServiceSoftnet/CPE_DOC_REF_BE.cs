// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.ServiceSoftnet.CPE_DOC_REF_BE
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
  public class CPE_DOC_REF_BE
  {
    private string nUM_LIN_REFField;
    private string cOD_TIP_DOC_REFField;
    private string nUM_SERIE_CPE_REFField;
    private string nUM_CORRE_CPE_REFField;
    private string fEC_DOC_REFField;
    private string cOD_TIP_OTR_DOC_REFField;
    private string nUM_OTR_DOC_REFField;
    private string sERIE_CORRE_CPE_REFField;

    public string NUM_LIN_REF
    {
      get => this.nUM_LIN_REFField;
      set => this.nUM_LIN_REFField = value;
    }

    public string COD_TIP_DOC_REF
    {
      get => this.cOD_TIP_DOC_REFField;
      set => this.cOD_TIP_DOC_REFField = value;
    }

    public string NUM_SERIE_CPE_REF
    {
      get => this.nUM_SERIE_CPE_REFField;
      set => this.nUM_SERIE_CPE_REFField = value;
    }

    public string NUM_CORRE_CPE_REF
    {
      get => this.nUM_CORRE_CPE_REFField;
      set => this.nUM_CORRE_CPE_REFField = value;
    }

    public string FEC_DOC_REF
    {
      get => this.fEC_DOC_REFField;
      set => this.fEC_DOC_REFField = value;
    }

    public string COD_TIP_OTR_DOC_REF
    {
      get => this.cOD_TIP_OTR_DOC_REFField;
      set => this.cOD_TIP_OTR_DOC_REFField = value;
    }

    public string NUM_OTR_DOC_REF
    {
      get => this.nUM_OTR_DOC_REFField;
      set => this.nUM_OTR_DOC_REFField = value;
    }

    public string SERIE_CORRE_CPE_REF
    {
      get => this.sERIE_CORRE_CPE_REFField;
      set => this.sERIE_CORRE_CPE_REFField = value;
    }
  }
}
