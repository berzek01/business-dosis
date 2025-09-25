// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Reports.TitlesReport
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.Reports.BL;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Reports
{
  public class TitlesReport : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtPlateNumber;
    protected TextBox txtTitleNumber;
    protected Button wibSearch;
    protected GridView wdgTitles;
    protected Pager custPagerBank;
    protected Label lblMsgError;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      if (this.txtPlateNumber.Text.Length == 0 && this.txtTitleNumber.Text.Length == 0)
      {
        Message.SetMessage(this.lblMsgError, enmMessageType.Warning, "Debe ingresar el número de placa o título");
        this.HidePopup();
      }
      else
      {
        this.lblMsgError.Visible = false;
        this.SearchTitles();
      }
    }

    protected void wdgTitles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      string str = e.CommandName.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      string platenumber = this.txtPlateNumber.Text.TrimEnd();
      if (str == "chksol")
      {
        DataTable detailsRequest = new ReportManagementBL().GetDetailsRequest(platenumber);
        if (detailsRequest == null || detailsRequest.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMsgError, enmMessageType.Warning, "No se encontró información del detalle de la solicitud");
        }
        else
        {
          this.Session["DetailsRequest"] = (object) detailsRequest;
          this.CreatePopUp("Detalle Solicitud", "../Reports/TitleDetail.aspx", "515px", "280px");
        }
      }
      else
      {
        DataTable detailsPayment = new ReportManagementBL().GetDetailsPayment(platenumber);
        if (detailsPayment == null || detailsPayment.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMsgError, enmMessageType.Warning, "No se encontró información del detalle del Pago");
        }
        else
        {
          this.Session["DetailsRequest"] = (object) detailsPayment;
          this.CreatePopUp("Detalle Pago", "../Reports/TitleDetail.aspx", "515px", "280px");
        }
      }
    }

    protected void custPagerBank_PageChanged(object sender, CustomPageChangeArgs e)
    {
      this.SearchTitlesList(this.txtPlateNumber.Text.TrimEnd(), this.txtTitleNumber.Text.TrimEnd(), false);
    }

    private void SearchTitles()
    {
      this.lblMsgError.Visible = false;
      try
      {
        this.SearchTitlesList(this.txtPlateNumber.Text.TrimEnd(), this.txtTitleNumber.Text.TrimEnd(), true);
        this.HidePopup();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgError, enmMessageType.Warning, ex.Message);
        this.HidePopup();
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SearchTitlesList(
      string pv_platenumber,
      string pv_titlenumber,
      bool pboolLoadPager)
    {
      int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerBank.CurrentPageNumber;
      int pintMaxRows = this.custPagerBank.CurrentPageSize == 0 ? 10 : this.custPagerBank.CurrentPageSize;
      int pintTotalRows;
      DataTable titleReport = new ReportManagementBL().GetTitleReport(pv_platenumber, pv_titlenumber, pintStartRowIndex, pintMaxRows, out pintTotalRows);
      if (titleReport == null || titleReport.Rows.Count == 0)
      {
        Message.SetMessage(this.lblMsgError, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
        this.HidePopup();
      }
      else
        this.lblMsgError.Visible = false;
      int num = pintTotalRows;
      this.wdgTitles.DataSource = (object) titleReport;
      this.wdgTitles.DataBind();
      this.custPagerBank.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
      this.custPagerBank.TotalRecordCount = pintTotalRows;
      if (!pboolLoadPager)
        return;
      this.custPagerBank.LoadPager();
    }

    private void CreatePopUp(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void wdgTitles_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgTitles_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
  }
}
