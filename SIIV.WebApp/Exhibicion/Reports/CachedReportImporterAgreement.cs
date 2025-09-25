// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibicion.Reports.CachedReportImporterAgreement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System;
using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace SIIV.WebApp.Exhibicion.Reports
{
  [ToolboxBitmap(typeof (ExportOptions), "report.bmp")]
  public class CachedReportImporterAgreement : Component, ICachedReport
  {
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool IsCacheable
    {
      get => true;
      set
      {
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool ShareDBLogonInfo
    {
      get => false;
      set
      {
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual TimeSpan CacheTimeOut
    {
      get => (TimeSpan) CachedReportConstants.DEFAULT_TIMEOUT;
      set
      {
      }
    }

    public virtual ReportDocument CreateReport()
    {
      ReportImporterAgreement report = new ReportImporterAgreement();
      ((Component) report).Site = this.Site;
      return (ReportDocument) report;
    }

    public virtual string GetCustomizedCacheKey(RequestContext request) => (string) null;
  }
}
