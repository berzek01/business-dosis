// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.ServiceSoftnet.CPE_RESPUESTA_BE
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
  public class CPE_RESPUESTA_BE
  {
    private string cODIGOField;
    private string dESCRIPCIONField;
    private string dETALLEField;
    private string nUM_CPEField;
    private string cOD_ESTD_SUNATField;
    private string iD_CABECERA_CLIENTEField;

    public string CODIGO
    {
      get => this.cODIGOField;
      set => this.cODIGOField = value;
    }

    public string DESCRIPCION
    {
      get => this.dESCRIPCIONField;
      set => this.dESCRIPCIONField = value;
    }

    public string DETALLE
    {
      get => this.dETALLEField;
      set => this.dETALLEField = value;
    }

    public string NUM_CPE
    {
      get => this.nUM_CPEField;
      set => this.nUM_CPEField = value;
    }

    public string COD_ESTD_SUNAT
    {
      get => this.cOD_ESTD_SUNATField;
      set => this.cOD_ESTD_SUNATField = value;
    }

    public string ID_CABECERA_CLIENTE
    {
      get => this.iD_CABECERA_CLIENTEField;
      set => this.iD_CABECERA_CLIENTEField = value;
    }
  }
}
