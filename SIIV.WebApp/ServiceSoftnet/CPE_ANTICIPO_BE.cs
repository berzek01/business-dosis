// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.ServiceSoftnet.CPE_ANTICIPO_BE
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
  public class CPE_ANTICIPO_BE
  {
    private string nUM_LIN_ANTCPField;
    private string mNT_ANTCPField;
    private string cOD_TIP_DOC_ANTCPField;
    private string nUM_SERIE_CPE_ANTCPField;
    private string nUM_CORRE_CPE_ANTCPField;

    public string NUM_LIN_ANTCP
    {
      get => this.nUM_LIN_ANTCPField;
      set => this.nUM_LIN_ANTCPField = value;
    }

    public string MNT_ANTCP
    {
      get => this.mNT_ANTCPField;
      set => this.mNT_ANTCPField = value;
    }

    public string COD_TIP_DOC_ANTCP
    {
      get => this.cOD_TIP_DOC_ANTCPField;
      set => this.cOD_TIP_DOC_ANTCPField = value;
    }

    public string NUM_SERIE_CPE_ANTCP
    {
      get => this.nUM_SERIE_CPE_ANTCPField;
      set => this.nUM_SERIE_CPE_ANTCPField = value;
    }

    public string NUM_CORRE_CPE_ANTCP
    {
      get => this.nUM_CORRE_CPE_ANTCPField;
      set => this.nUM_CORRE_CPE_ANTCPField = value;
    }
  }
}
