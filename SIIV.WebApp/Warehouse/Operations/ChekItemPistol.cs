// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.ChekItemPistol
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class ChekItemPistol : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected GridView wdgPlacas;
    protected TextBox TxtVerificador;
    protected FilteredTextBoxExtender TxtVerificador_FilteredTextBoxExtender;
    protected Button btnVerificar;
    protected HtmlTableRow trMessage;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
    }
  }
}
