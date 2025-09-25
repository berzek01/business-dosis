// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Delivery.DeliveryZone
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Delivery
{
  public class DeliveryZone : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected GridView wddDistrict;
    protected Label lblCount;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.getDistrict(this.Request.QueryString["VehicleClassId"].ToString((IFormatProvider) CultureInfo.CurrentCulture));
    }

    public void getDistrict(string VehicleClassId)
    {
      try
      {
        int i_VehicleClasification = Convert.ToInt32(VehicleClassId) == 5 ? 2 : 1;
        DataTable dataTable = new DataTable();
        DataTable districtByDeliveryPoint = new RequirementQueriesBL().GetDistrictByDeliveryPoint(Convert.ToInt32(this.Session["PuntoEntrega"]), i_VehicleClasification, 1);
        if (districtByDeliveryPoint.Rows.Count <= 0)
          return;
        this.wddDistrict.DataSource = (object) districtByDeliveryPoint;
        this.wddDistrict.DataBind();
        this.lblCount.Text = Constants.SEARCHRESULT_OK.Replace("XX", this.wddDistrict.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture));
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblCount, enmMessageType.Error, Constants.REQUIREMENT_ERROR_Consulta_erronea + ex.Message);
      }
    }
  }
}
