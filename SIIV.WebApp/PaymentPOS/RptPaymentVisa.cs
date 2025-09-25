// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.PaymentPOS.RptPaymentVisa
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.ComponentModel;

#nullable disable
namespace SIIV.WebApp.PaymentPOS
{
  public class RptPaymentVisa : ReportClass
  {
    public override string ResourceName
    {
      get => "RptPaymentVisa.rpt";
      set
      {
      }
    }

    public override bool NewGenerator
    {
      get => true;
      set
      {
      }
    }

    public override string FullResourceName
    {
      get => "SIIV.WebApp.PaymentPOS.RptPaymentVisa.rpt";
      set
      {
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Section Section1 => this.ReportDefinition.Sections[0];

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Section Section2 => this.ReportDefinition.Sections[1];

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Section Section3 => this.ReportDefinition.Sections[2];

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Section Section4 => this.ReportDefinition.Sections[3];

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Section Section5 => this.ReportDefinition.Sections[4];

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IParameterField Parameter_TituloReporte
    {
      get => (IParameterField) this.DataDefinition.ParameterFields[0];
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IParameterField Parameter_NombreTienda
    {
      get => (IParameterField) this.DataDefinition.ParameterFields[1];
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IParameterField Parameter_Telefono
    {
      get => (IParameterField) this.DataDefinition.ParameterFields[2];
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IParameterField Parameter_DireccionTienda
    {
      get => (IParameterField) this.DataDefinition.ParameterFields[3];
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IParameterField Parameter_NumPedido
    {
      get => (IParameterField) this.DataDefinition.ParameterFields[4];
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IParameterField Parameter_NumTarjeta
    {
      get => (IParameterField) this.DataDefinition.ParameterFields[5];
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IParameterField Parameter_FechPedido
    {
      get => (IParameterField) this.DataDefinition.ParameterFields[6];
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IParameterField Parameter_ImporteTransac
    {
      get => (IParameterField) this.DataDefinition.ParameterFields[7];
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IParameterField Parameter_Moneda
    {
      get => (IParameterField) this.DataDefinition.ParameterFields[8];
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IParameterField Parameter_DescProducto
    {
      get => (IParameterField) this.DataDefinition.ParameterFields[9];
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IParameterField Parameter_NombComprador
    {
      get => (IParameterField) this.DataDefinition.ParameterFields[10];
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IParameterField Parameter_DescRespuesta
    {
      get => (IParameterField) this.DataDefinition.ParameterFields[11];
    }
  }
}
