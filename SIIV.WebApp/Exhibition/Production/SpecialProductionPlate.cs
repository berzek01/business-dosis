// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Production.SpecialProductionPlate
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.SpecialPlateProduction.BL;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

#nullable disable
namespace SIIV.WebApp.Exhibition.Production
{
  public class SpecialProductionPlate : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected Fecha dtFecIni;
    protected Fecha dtFecFin;
    protected Button wibSearch;
    protected Button btnReturnPopupConfirmation;
    protected Button btnManagementTemp;
    protected Button wibNew;
    protected GridView wdgSpecialProductionPlateList;
    protected Pager custPagerSPP;
    protected Label lblMessage;
    protected Button Button2;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      this.lblMessage.Text = "";
      if (this.Page.IsPostBack)
        return;
      try
      {
        DataTable dtResult = new DataTable("Datos");
        this.TableColumns(dtResult);
        DataRow row = dtResult.NewRow();
        dtResult.Rows.Add(row);
        this.wdgSpecialProductionPlateList.DataSource = (object) dtResult;
        this.wdgSpecialProductionPlateList.DataBind();
        this.wdgSpecialProductionPlateList.Rows[0].Visible = false;
        this.wdgSpecialProductionPlateList.Columns[7].Visible = false;
        string str = this.Session["SystemUser"] != null ? new RequirementQueriesBL().GetSystemUserExtendedAction(((SystemUser) this.Session["SystemUser"]).i_SystemUserId, 1) : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - SpecialProductionPlate.aspx");
        if (str != "")
        {
          if (Array.Find<string>(str.Split('|'), (Predicate<string>) (element => element.Equals("5", StringComparison.CurrentCulture))) == "5")
            this.wdgSpecialProductionPlateList.Columns[7].Visible = true;
        }
        this.dtFecIni.Value = DateTime.Now;
        this.dtFecFin.Value = DateTime.Now;
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    private void TableColumns(DataTable dtResult)
    {
      dtResult.Columns.Add("i_SpecialPlateProductionId", typeof (string));
      dtResult.Columns.Add("i_SpecialPlateTypeId", typeof (string));
      dtResult.Columns.Add("v_SpecialPlateType", typeof (string));
      dtResult.Columns.Add("i_SpecialPlateClasificationId", typeof (string));
      dtResult.Columns.Add("i_SpecialProductId", typeof (DateTime));
      dtResult.Columns.Add("i_SpecialPlateProcessTypeId", typeof (string));
      dtResult.Columns.Add("i_Status", typeof (string));
      dtResult.Columns.Add("v_SpecialPlateClasification", typeof (string));
      dtResult.Columns.Add("v_SpecialPlateProcessType", typeof (string));
      dtResult.Columns.Add("v_Observation", typeof (string));
      dtResult.Columns.Add("i_Quantity", typeof (string));
      dtResult.Columns.Add("v_PlateIni", typeof (string));
      dtResult.Columns.Add("v_PlateFin", typeof (string));
      dtResult.Columns.Add("d_InsertDate", typeof (DateTime));
      dtResult.Columns.Add("v_Status", typeof (string));
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchSPP();

    protected void custPagerSPP_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        this.SearchSPPList(Convert.ToInt32(this.Request.QueryString["SpecialPlateTypeId"].ToString()), this.dtFecIni.Text, this.dtFecFin.Text, false);
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void wibNew_Click(object sender, EventArgs e)
    {
      try
      {
        string str = this.Request.QueryString["SpecialPlateTypeId"].ToString();
        string empty = string.Empty;
        this.CreatePopUpServer("Registro de Rango de Placas - Emisión / Reemisión", "SpecialProductionPlateDetail.aspx?SpecialPlateTypeId=" + str + "&SpecialPlateProductionId=0", "540px", "630px");
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      try
      {
        int int32 = Convert.ToInt32(this.Session["i_SpecialPlateProductionId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        if (Convert.ToInt32(this.Session["MessageTypeId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture) == 1)
        {
          new SpecialPlateProductionQueriesBL().DelSpecialPlateProduction(int32);
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(2, "El registro fue anulado correctamente"));
        }
        else
        {
          int index = 0;
          if (this.ViewState["index"] != null)
            index = (int) Convert.ToInt16(this.ViewState["index"].ToString());
          if (this.wdgSpecialProductionPlateList.Rows[index] == null)
            throw new HandledException(4, "Error de selección.", "'wdgList' - SpecialProductionPlate.aspx");
          int content1 = int.Parse(this.wdgSpecialProductionPlateList.DataKeys[index]["i_SpecialPlateTypeId"].ToString());
          int content2 = int.Parse(this.wdgSpecialProductionPlateList.DataKeys[index]["i_SpecialPlateClasificationId"].ToString());
          int content3 = int.Parse(this.wdgSpecialProductionPlateList.DataKeys[index]["i_SpecialProductId"].ToString());
          int content4 = int.Parse(this.wdgSpecialProductionPlateList.DataKeys[index]["i_SpecialPlateProcessTypeId"].ToString());
          DataTable dataTable = new DataTable();
          DataTable productionDetail = new SpecialPlateProductionQueriesBL().GetSpecialPlateProductionDetail(int32);
          SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
          RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
          XElement xelement = new XElement((XName) "SpecialPlateProduction", new object[18]
          {
            (object) new XElement((XName) "i_SpecialPlateProductionId", (object) int32),
            (object) new XElement((XName) "i_SystemUserId", (object) systemUser.i_SystemUserId),
            (object) new XElement((XName) "i_DocumentTypeId", (object) systemUser.i_DocumentTypeId),
            (object) new XElement((XName) "v_DocumentNumber", (object) systemUser.v_DocumentNumber),
            (object) new XElement((XName) "v_LastName", (object) systemUser.v_LastName),
            (object) new XElement((XName) "v_FirstName", (object) systemUser.v_FirstName),
            (object) new XElement((XName) "v_CompleteName", (object) (systemUser.v_FirstName + " " + systemUser.v_LastName)),
            (object) new XElement((XName) "v_Address", (object) systemUser.v_Address),
            (object) new XElement((XName) "v_Ubigeo", (object) systemUser.v_Ubigeo),
            (object) new XElement((XName) "v_Telephone", (object) systemUser.v_Telephone),
            (object) new XElement((XName) "v_Email", (object) systemUser.v_Email),
            (object) new XElement((XName) "i_PersonTypeId", (object) systemUser.i_PersonTypeId),
            (object) new XElement((XName) "v_TitleNumber", (object) requirementQueriesBl.GetCorrelativeTitle()),
            (object) new XElement((XName) "i_SpecialPlateTypeId", (object) content1),
            (object) new XElement((XName) "i_SpecialPlateClasificationId", (object) content2),
            (object) new XElement((XName) "i_SpecialProductId", (object) content3),
            (object) new XElement((XName) "i_SpecialPlateProcessTypeId", (object) content4),
            (object) new XElement((XName) "SpecialPlateProductionDetails")
          });
          foreach (DataRow row in (InternalDataCollectionBase) productionDetail.Rows)
          {
            XElement content5 = new XElement((XName) "SpecialPlateProductionDetail", (object) new XElement((XName) "v_Plate", (object) row["v_Plate"].ToString()));
            xelement.Element((XName) "SpecialPlateProductionDetails").Add((object) content5);
          }
          new SpecialPlateProductionQueriesBL().SetConfirmationSpecialPlateProduction(xelement.ToString());
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(2, "El registro fue aprobado correctamente"));
        }
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.ViewState.Remove("index");
        this.SearchSPP();
      }
    }

    protected void btReporno_Click(object sender, EventArgs e) => this.SearchSPP();

    protected void wdgSpecialProductionPlateList_ItemCommand(
      object sender,
      GridViewCommandEventArgs e)
    {
      try
      {
        int int32 = Convert.ToInt32(e.CommandArgument);
        this.ViewState["index"] = (object) int32;
        if (this.wdgSpecialProductionPlateList.Rows[int32] == null)
          throw new HandledException(4, "Error de selección.", "'wdgList' - SpecialProductionPlate.aspx");
        int num1 = int.Parse(this.wdgSpecialProductionPlateList.DataKeys[int32]["i_SpecialPlateProductionId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        int num2 = int.Parse(this.wdgSpecialProductionPlateList.DataKeys[int32]["i_Status"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        if (e.CommandName.Equals("Edit"))
        {
          if (num2 == 0)
          {
            this.Session["i_SpecialPlateProductionId"] = (object) num1;
            this.Session["MessageTypeId"] = (object) 2;
            string empty = string.Empty;
            this.CreatePopUpServer("SIIV - Placas Especiales", "../../UserControls/PopupConfirmation.aspx?MessageTypeId=2&MessageText=¿Desea Confirmar el Registro?", "360px", "190px");
          }
          else
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "Registro, ya se encuentra aprobado"));
        }
        if (e.CommandName.Equals("Delete", StringComparison.CurrentCulture))
        {
          if (num2 == 0)
          {
            this.Session["i_SpecialPlateProductionId"] = (object) num1;
            this.Session["MessageTypeId"] = (object) 1;
            string empty = string.Empty;
            this.CreatePopUpServer("SIIV - Placas Especiales", "../../UserControls/PopupConfirmation.aspx?MessageTypeId=1&MessageText=¿Desea Eliminar el Registro?", "350px", "190px");
          }
          else
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "No se puede eliminar registro, ya se encuentra aprobado"));
        }
        if (!e.CommandName.Equals("Print"))
          return;
        this.ViewState["i_SpecialPlateProductionId"] = (object) Convert.ToInt32(this.wdgSpecialProductionPlateList.DataKeys[int32]["i_SpecialPlateProductionId"].ToString());
        this.ViewState["i_SpecialPlateTypeId"] = (object) Convert.ToInt32(this.Request.QueryString["SpecialPlateTypeId"].ToString());
        string script = "Exportpdf();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable dataTable = new SpecialPlateProductionQueriesBL().ProductionReport(Convert.ToInt32(this.ViewState["i_SpecialPlateProductionId"]), Convert.ToInt32(this.ViewState["i_SpecialPlateTypeId"].ToString()));
        ReportDocument reportDocument = new ReportDocument();
        string filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportSpecialPlateProduction.rpt";
        reportDocument.Load(filename);
        reportDocument.SetDataSource(dataTable);
        reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "ReporteOrderProduccion");
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    private void SearchSPP()
    {
      try
      {
        this.SearchSPPList(Convert.ToInt32(this.Request.QueryString["SpecialPlateTypeId"].ToString()), this.dtFecIni.Text, this.dtFecFin.Text, true);
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SearchSPPList(
      int intSpecialPlateTypeId,
      string dFecIni,
      string fFecFin,
      bool pboolLoadPager)
    {
      try
      {
        int pintstartRowIndex = pboolLoadPager ? 1 : this.custPagerSPP.CurrentPageNumber;
        int pintmaxRows = this.custPagerSPP.CurrentPageSize == 0 ? 10 : this.custPagerSPP.CurrentPageSize;
        int pinttotalRows;
        DataTable specialPlateProduction = new SpecialPlateProductionQueriesBL().GetSpecialPlateProduction(intSpecialPlateTypeId, dFecIni, fFecFin, pintstartRowIndex, pintmaxRows, out pinttotalRows);
        if (specialPlateProduction == null || specialPlateProduction.Rows.Count == 0)
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
        int num = pinttotalRows;
        this.wdgSpecialProductionPlateList.DataSource = (object) specialPlateProduction;
        this.wdgSpecialProductionPlateList.DataBind();
        this.custPagerSPP.TotalPages = num % pintmaxRows == 0 ? num / pintmaxRows : num / pintmaxRows + 1;
        this.custPagerSPP.TotalRecordCount = pinttotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerSPP.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
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

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void wdgSpecialProductionPlateList_RowDeleting(
      object sender,
      GridViewDeleteEventArgs e)
    {
    }

    protected void wdgSpecialProductionPlateList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
  }
}
