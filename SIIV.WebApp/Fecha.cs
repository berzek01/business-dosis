// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Fecha
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp
{
  public class Fecha : UserControl
  {
    private DateTime _dFechaIni = Convert.ToDateTime("01/01/1950");
    private DateTime _dFechaFin = DateTime.Today;
    private bool _IsLimit = true;
    protected TextBox TxtFecha;
    protected Image btnFecha;
    protected CalendarExtender ceFecha;
    protected MaskedEditExtender meFecha;

    public event Fecha.TextChangedDelegate TextChanged;

    public DateTime dFechaIni
    {
      get => this._dFechaIni;
      set => this._dFechaIni = value;
    }

    public DateTime dFechaFin
    {
      get => this._dFechaFin;
      set => this._dFechaFin = value;
    }

    public string Text
    {
      set => this.TxtFecha.Text = value;
      get => this.TxtFecha.Text;
    }

    public DateTime Value
    {
      set => this.TxtFecha.Text = value.ToString("dd/MM/yyyy");
      get => this.TxtFecha.Text != "" ? Convert.ToDateTime(this.TxtFecha.Text) : DateTime.Today;
    }

    public string CssClass
    {
      set => this.TxtFecha.CssClass = value;
      get => this.TxtFecha.CssClass;
    }

    public void SetAtributtes(string evento, string funcion)
    {
      this.TxtFecha.Attributes.Add(evento, funcion);
    }

    public bool Enabled
    {
      set
      {
        this.btnFecha.Style["cursor"] = value ? "pointer" : "";
        this.ceFecha.PopupButtonID = value ? "btnFecha" : "";
        this.TxtFecha.Enabled = value;
        this.ceFecha.Enabled = value;
      }
      get => this.TxtFecha.Enabled;
    }

    public bool AutoPostBack
    {
      set => this.TxtFecha.AutoPostBack = value;
      get => this.TxtFecha.AutoPostBack;
    }

    public bool IsLimit
    {
      get => this._IsLimit;
      set => this._IsLimit = value;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this._IsLimit)
      {
        this.ceFecha.StartDate = new DateTime?(this._dFechaIni);
        this.ceFecha.EndDate = new DateTime?(this._dFechaFin);
      }
      this.ceFecha.EnableViewState = true;
      if (this.Page.IsPostBack)
        return;
      bool enabled = this.TxtFecha.Enabled;
      this.btnFecha.Style["cursor"] = enabled ? "pointer" : "";
      this.TxtFecha.Enabled = enabled;
      this.ceFecha.Enabled = enabled;
    }

    public bool IsValid()
    {
      DateTime dateTime = this.Value;
      return !(dateTime < this._dFechaIni) && !(dateTime > this._dFechaFin);
    }

    protected void TxtFecha_TextChanged(object sender, EventArgs e)
    {
      try
      {
        this.TextChanged(sender, e);
      }
      catch (Exception ex)
      {
      }
    }

    public CalendarExtender Calendario
    {
      get => this.ceFecha;
      set => this.ceFecha = value;
    }

    public MaskedEditExtender EdicionCalendario
    {
      get => this.meFecha;
      set => this.meFecha = value;
    }

    public delegate void TextChangedDelegate(object sender, EventArgs e);
  }
}
