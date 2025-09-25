// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.ServiceSoftnet.CPE_DOC_BAJA
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
  public class CPE_DOC_BAJA
  {
    private string nUM_NIF_EMISField;
    private string cOD_TIP_NIF_EMISField;
    private string cOD_TIP_CPEField;
    private string fEC_EMISField;
    private string nUM_SERIE_CPEField;
    private string nUM_CORRE_CPEField;
    private string tXT_MTVO_BAJAField;
    private string fEC_RECEP_DOC_SUNATField;
    private string hOR_RECEP_DOC_SUNATField;

    public string NUM_NIF_EMIS
    {
      get => this.nUM_NIF_EMISField;
      set => this.nUM_NIF_EMISField = value;
    }

    public string COD_TIP_NIF_EMIS
    {
      get => this.cOD_TIP_NIF_EMISField;
      set => this.cOD_TIP_NIF_EMISField = value;
    }

    public string COD_TIP_CPE
    {
      get => this.cOD_TIP_CPEField;
      set => this.cOD_TIP_CPEField = value;
    }

    public string FEC_EMIS
    {
      get => this.fEC_EMISField;
      set => this.fEC_EMISField = value;
    }

    public string NUM_SERIE_CPE
    {
      get => this.nUM_SERIE_CPEField;
      set => this.nUM_SERIE_CPEField = value;
    }

    public string NUM_CORRE_CPE
    {
      get => this.nUM_CORRE_CPEField;
      set => this.nUM_CORRE_CPEField = value;
    }

    public string TXT_MTVO_BAJA
    {
      get => this.tXT_MTVO_BAJAField;
      set => this.tXT_MTVO_BAJAField = value;
    }

    public string FEC_RECEP_DOC_SUNAT
    {
      get => this.fEC_RECEP_DOC_SUNATField;
      set => this.fEC_RECEP_DOC_SUNATField = value;
    }

    public string HOR_RECEP_DOC_SUNAT
    {
      get => this.hOR_RECEP_DOC_SUNATField;
      set => this.hOR_RECEP_DOC_SUNATField = value;
    }
  }
}
