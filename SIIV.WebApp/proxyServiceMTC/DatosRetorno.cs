// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.proxyServiceMTC.DatosRetorno
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#nullable disable
namespace SIIV.WebApp.proxyServiceMTC
{
  [GeneratedCode("System.Xml", "4.8.9037.0")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://tempuri.org/")]
  [Serializable]
  public class DatosRetorno : INotifyPropertyChanged
  {
    private long tamanioOriginalField;
    private long tamanioComprimidoField;
    private byte[] arrayBytesField;

    [XmlElement(Order = 0)]
    public long TamanioOriginal
    {
      get => this.tamanioOriginalField;
      set
      {
        this.tamanioOriginalField = value;
        this.RaisePropertyChanged(nameof (TamanioOriginal));
      }
    }

    [XmlElement(Order = 1)]
    public long TamanioComprimido
    {
      get => this.tamanioComprimidoField;
      set
      {
        this.tamanioComprimidoField = value;
        this.RaisePropertyChanged(nameof (TamanioComprimido));
      }
    }

    [XmlElement(DataType = "base64Binary", Order = 2)]
    public byte[] ArrayBytes
    {
      get => this.arrayBytesField;
      set
      {
        this.arrayBytesField = value;
        this.RaisePropertyChanged(nameof (ArrayBytes));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
