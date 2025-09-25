// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Rpt.PositionBatchPlateReport
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.ComponentModel;

#nullable disable
namespace SIIV.WebApp.Warehouse.Rpt
{
  [Serializable]
  public class PositionBatchPlateReport : ReportClass
  {
    public override string ResourceName
    {
      get => "PositionBatchPlateReport.rpt";
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
      get => "SIIV.WebApp.Warehouse.Rpt.PositionBatchPlateReport.rpt";
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
    public IParameterField Parameter_i_BatchId
    {
      get => (IParameterField) this.DataDefinition.ParameterFields[0];
    }
  }
}
