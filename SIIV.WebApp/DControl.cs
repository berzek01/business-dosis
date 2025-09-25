// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.DControl
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp
{
  public class DControl : UserControl
  {
    protected HistoryEventArgs m_hea = (HistoryEventArgs) null;
    protected ParameterCollection m_pc = (ParameterCollection) null;

    public static T LoadUC<T>(string file, Page p) where T : DControl
    {
      T obj = (T) p.LoadControl(file);
      obj.ID = Path.GetFileName(file).Replace('.', '_');
      obj.UCPage = file;
      return obj;
    }

    public string UCPage
    {
      set
      {
        if (string.IsNullOrEmpty(value))
          return;
        this.ViewState[nameof (UCPage)] = (object) value;
      }
      get
      {
        string str = this.ViewState[nameof (UCPage)] as string;
        return string.IsNullOrEmpty(str) ? (string) null : str;
      }
    }

    public ParameterCollection Params
    {
      get
      {
        string json = this.ViewState[nameof (Params)] as string;
        return string.IsNullOrEmpty(json) ? (ParameterCollection) null : UtilMenu.DeserializePC(json);
      }
      set
      {
        if (value == null)
        {
          this.ViewState.Remove(nameof (Params));
        }
        else
        {
          this.m_pc = value;
          this.ViewState[nameof (Params)] = (object) UtilMenu.SerializePC(value);
        }
      }
    }

    public HistoryEventArgs UCHistoryArgs
    {
      set => this.m_hea = value;
      get => this.m_hea;
    }

    public DControl LoadContent(string file, HistoryEventArgs e) => (DControl) null;

    public DControl ReloadContent() => (DControl) null;
  }
}
