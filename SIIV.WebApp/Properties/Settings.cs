// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Properties.Settings
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace SIIV.WebApp.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        Settings defaultInstance = Settings.defaultInstance;
        return defaultInstance;
      }
    }

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [SpecialSetting(SpecialSetting.WebServiceUrl)]
    [DefaultSettingValue("http://www.fedtt-peru.com/Mercurio_AAP_WS/MercuryWebService.asmx")]
    public string SIIV_WebApp_ServiceSoftnet_MercurioWS
    {
      get => (string) this[nameof (SIIV_WebApp_ServiceSoftnet_MercurioWS)];
    }
  }
}
