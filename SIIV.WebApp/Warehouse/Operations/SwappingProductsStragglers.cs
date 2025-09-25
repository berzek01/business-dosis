// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.SwappingProductsStragglers
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class SwappingProductsStragglers : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected DropDownList wddWarehouse2;
    protected Fecha wdpDate;
    protected DropDownList wddFlow;
    protected DropDownList wddMovementType2;
    protected Panel Panel1;
    protected DropDownList wddLocation;
    protected DropDownList wddTargetWarehouse;
    protected Button wibAgregar;
    protected GridView wdgProductDetail;
    protected Button wibSave;
    protected Button wibCancel;
    protected Label lblMessage;
    protected Button btnJavaScriptResponse;
    protected Button btnAceptChangeQuantity;
    protected Button wibAccept;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void wibAgregar_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.CreatePopUpServer("Editar", "../Searchs/ProductSearchStragglers.aspx", "815px", "800px");
    }

    protected void btnJavaScriptResponse_Click(object sender, EventArgs e)
    {
    }

    protected void wdgProductDetail_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
    }
  }
}
