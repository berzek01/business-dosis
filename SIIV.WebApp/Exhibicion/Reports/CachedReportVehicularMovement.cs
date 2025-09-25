// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibicion.Reports.CachedReportVehicularMovement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

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
  public class CachedReportVehicularMovement : Component, ICachedReport
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
      ReportVehicularMovement report = new ReportVehicularMovement();
      ((Component) report).Site = this.Site;
      return (ReportDocument) report;
    }

    public virtual string GetCustomizedCacheKey(RequestContext request) => (string) null;
  }
}
