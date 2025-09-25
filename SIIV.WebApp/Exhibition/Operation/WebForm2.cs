// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Operation.WebForm2
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.BE;
using SIIV.BE.CustomCode;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Operation
{
  public class WebForm2 : Page
  {
    private AcquisitionManagementBL pobjEAcquisitionManagementBL;
    private AcquisitionQueriesBL pobjAcquisitionQueriesBL;
    private List<ExhibitionAcquisitionRenewal> pobjLstExhibitionAcquisitionRenewal;
    private int i_PlateTypeId;
    private int i_ProductId;
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected CheckBox chkAll;
    protected GridView wdgPlates;
    protected Label lblCount;
    protected Button wibSave;
    protected Button wibPrint;
    protected HtmlTableRow TdRenovar;
    protected HtmlTableRow TdInfo;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.Initialize();
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.chkAll.Checked)
        {
          foreach (Control row in this.wdgPlates.Rows)
            ((CheckBox) row.FindControl("chkItem")).Checked = true;
        }
        else
        {
          foreach (Control row in this.wdgPlates.Rows)
            ((CheckBox) row.FindControl("chkItem")).Checked = false;
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
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      try
      {
        int num = 0;
        this.pobjLstExhibitionAcquisitionRenewal = new List<ExhibitionAcquisitionRenewal>();
        this.pobjEAcquisitionManagementBL = new AcquisitionManagementBL();
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RenewalPlate.aspx");
        int i_ProcessTypeId = 2;
        int int32 = Convert.ToInt32(this.Request.QueryString["t"].ToString());
        DataTable t_Detail = this.DataTableRenewal();
        foreach (GridViewRow row in this.wdgPlates.Rows)
        {
          CheckBox control = (CheckBox) row.FindControl("chkItem");
          if (control != null && control.Checked)
          {
            ++num;
            string str1 = row.Cells[1].Text.ToString();
            string str2 = this.wdgPlates.DataKeys[row.RowIndex]["i_RegistrationDetailId"].ToString();
            string str3 = this.wdgPlates.DataKeys[row.RowIndex]["i_VehicleClassId"].ToString();
            string str4 = this.wdgPlates.DataKeys[row.RowIndex]["i_RequirementPlateId"].ToString();
            string str5 = this.wdgPlates.DataKeys[row.RowIndex]["i_ProductId"].ToString();
            t_Detail.Rows.Add(new object[9]
            {
              null,
              (object) str1,
              (object) str4,
              null,
              (object) str2,
              (object) str3,
              (object) 0,
              null,
              (object) str5
            });
          }
        }
        if (this.pobjEAcquisitionManagementBL.RenewalInsert(systemUser.i_AssociatedId, systemUser.i_SystemUserId, i_ProcessTypeId, int32, t_Detail))
        {
          this.Initialize();
          throw new HandledException(2, "Se renovó las placas con éxito");
        }
        if (num == 0)
          throw new HandledException(1, "Seleccione al menos una placa");
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

    protected void wibPrint_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable dataTable1 = new DataTable();
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RenewalPlate.aspx");
        AcquisitionManagementBL acquisitionManagementBl = new AcquisitionManagementBL();
        int int32 = Convert.ToInt32(systemUser.i_IsAssociatedAAP.ToString());
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.i_ProductId = this.SetProductId(this.i_PlateTypeId);
        try
        {
          DataTable dataTable2 = acquisitionManagementBl.SpecialPlateReportPlatesRenewal(systemUser.i_SystemUserId, this.i_ProductId, this.i_PlateTypeId);
          if (!dataTable2.Columns.Contains("v_Message"))
            dataTable2.Columns.Add("v_Message", Type.GetType("System.String"));
          ReportDocument reportDocument = new ReportDocument();
          string filename;
          if (this.i_PlateTypeId == 7)
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
            {
              string empty = string.Empty;
              string str = int32 == 2 ? ConfigurationManager.AppSettings["_strcadenaRenovacionExhibitionImportador"].ToString().Replace("\\n", Environment.NewLine) : ConfigurationManager.AppSettings["_strcadenaRenovacionExhibition"].ToString().Replace("\\n", Environment.NewLine);
              row["v_Message"] = (object) str.ToString();
            }
            filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportExhibition.rpt";
          }
          else
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
            {
              string empty = string.Empty;
              string str = int32 == 2 ? ConfigurationManager.AppSettings["_strcadenaRenovacionRotativasImportador"].ToString().Replace("\\n", Environment.NewLine) : ConfigurationManager.AppSettings["_strcadenaRenovacionRotativas"].ToString().Replace("\\n", Environment.NewLine);
              row["v_Message"] = (object) str.ToString();
            }
            filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportRotate.rpt";
          }
          reportDocument.Load(filename);
          reportDocument.SetDataSource(dataTable2);
          reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "CodigoPago");
        }
        catch (Exception ex)
        {
          throw new HandledException(0, "***Error... Comunicarce con el administrador de sistema.");
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
    }

    private void Initialize()
    {
      try
      {
        DataTable dataTable1 = new DataTable();
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RenewalPlate.aspx");
        this.ViewState["i_PlateTypeId"] = (object) this.Request.QueryString["t"].ToString();
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.i_ProductId = this.SetProductId(this.i_PlateTypeId);
        DataTable dataTable2 = new DataTable();
        this.pobjAcquisitionQueriesBL = new AcquisitionQueriesBL();
        this.wdgPlates.DataSource = (object) this.pobjAcquisitionQueriesBL.SpecialPlatesRenewalGet(systemUser.i_SystemUserId, this.i_ProductId, this.i_PlateTypeId);
        this.wdgPlates.DataBind();
        this.lblCount.Text = Constants.SEARCHRESULT_OK.Replace("XX", this.wdgPlates.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture));
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

    private int SetProductId(int i_PlateTypeId)
    {
      try
      {
        int num = 0;
        WarehouseExhibitionQueriesBL exhibitionQueriesBl = new WarehouseExhibitionQueriesBL();
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = exhibitionQueriesBl.SpecialPlateWarehouseProductGet(i_PlateTypeId);
        switch (i_PlateTypeId)
        {
          case 6:
            num = 0;
            break;
          case 7:
            num = Convert.ToInt32(dataTable2.Rows[0]["i_Productid"].ToString());
            break;
          case 11:
            num = Convert.ToInt32(dataTable2.Rows[0]["i_Productid"].ToString());
            break;
        }
        return num;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public DataTable DataTableRenewal()
    {
      try
      {
        return new DataTable()
        {
          Columns = {
            "i_RegistrationtId",
            "v_PlateNew",
            "i_RequirementPlateId",
            "v_PaymentCode",
            "i_RegistrationDetailId",
            "i_VehicleClassId",
            "Proceso",
            "i_Status",
            "i_ProductId"
          }
        };
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
