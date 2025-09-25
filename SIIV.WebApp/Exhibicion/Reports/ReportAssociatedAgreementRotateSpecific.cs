// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibicion.Reports.ReportAssociatedAgreementRotateSpecific
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using CrystalDecisions.CrystalReports.Engine;
using System.ComponentModel;

#nullable disable
namespace SIIV.WebApp.Exhibicion.Reports
{
  public class ReportAssociatedAgreementRotateSpecific : ReportClass
  {
    public override string ResourceName
    {
      get => "ReportAssociatedAgreementRotateSpecific.rpt";
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
      get => "SIIV.WebApp.Exhibicion.Reports.ReportAssociatedAgreementRotateSpecific.rpt";
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
    public Section DetailSection5 => this.ReportDefinition.Sections[3];

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Section DetailSection4 => this.ReportDefinition.Sections[4];

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Section DetailSection3 => this.ReportDefinition.Sections[5];

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Section DetailSection2 => this.ReportDefinition.Sections[6];

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Section DetailSection1 => this.ReportDefinition.Sections[7];

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Section Section4 => this.ReportDefinition.Sections[8];

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Section Section5 => this.ReportDefinition.Sections[9];
  }
}
