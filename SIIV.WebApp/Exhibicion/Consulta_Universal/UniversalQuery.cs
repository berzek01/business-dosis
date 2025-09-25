// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibicion.Consulta_Universal.UniversalQuery
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibicion.Consulta_Universal
{
  public class UniversalQuery : Page
  {
    private AssociatedQueriesBL pobjAssociatedQueriesBL;
    private UniversalQueriesBL pobjUniversalQueriesBL;
    private AcquisitionQueriesBL pobjAcquisitionQueriesBL;
    private int i_PlateTypeId = 0;
    private int i_ProductId;
    protected UpdatePanel UpdatePanel1;
    protected Label Label1;
    protected DropDownList wddAssociated;
    protected Label Label2;
    protected TextBox txtPlate;
    protected Label Label6;
    protected TextBox txtCodPago;
    protected Label Label3;
    protected DropDownList wddRequirementType1;
    protected Label Label4;
    protected DropDownList wddStatus;
    protected CheckBox chkDate;
    protected Label Label5;
    protected Fecha wdpStartDate;
    protected Label Label8;
    protected Fecha wdpEndDate;
    protected Button wibRenewald;
    protected Button wibSearch;
    protected Button wibExcel;
    protected GridView wdgList;
    protected GridView wdgListNew;
    protected Pager custPagerUQ;
    protected Label lblMessage;
    protected Button Button1;
    protected Button Button2;
    protected UpdatePanel UpdatePanel2;
    protected TextBox txtRazonSocial;
    protected TextBox txtRuc;
    protected TextBox txtPlaca;
    protected TextBox txtEstadoPlaca;
    protected GridView wdgDetalle;
    protected Button wibReturn;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.ViewState["t"] = (object) Convert.ToString(this.Request.QueryString["t"].ToString());
        this.ViewState["i_PlateTypeId"] = (object) this.Request.QueryString["t"].ToString();
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
        this.pobjAssociatedQueriesBL = new AssociatedQueriesBL();
        if (this.Session["ApplicationId"] != null)
        {
          int int32 = Convert.ToInt32(ConfigurationManager.AppSettings["ApplicationId"]);
          string userExtendedAction = this.pobjAssociatedQueriesBL.GetSystemUserExtendedAction(systemUser.i_SystemUserId, int32);
          if (userExtendedAction != "")
          {
            if (Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("7", StringComparison.CurrentCulture))) == "7")
            {
              this.ViewState["i_SystemUserId"] = (object) 0;
              this.wibRenewald.Visible = false;
            }
            else
            {
              this.ViewState["i_SystemUserId"] = (object) Convert.ToInt32(systemUser.i_SystemUserId);
              this.ValidarRenovacion();
            }
          }
          else
          {
            this.ViewState["i_SystemUserId"] = (object) Convert.ToInt32(systemUser.i_SystemUserId);
            this.ValidarRenovacion();
          }
        }
        this.LoadParameters();
        this.SetDatePicker();
        this.chkDate.Checked = true;
        this.wdpStartDate.Enabled = true;
        this.wdpEndDate.Enabled = true;
        string script = UtilDA.ActiveTabIndex("tabs", 0, "1");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
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

    protected void chkDate_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkDate.Checked)
      {
        this.wdpStartDate.Enabled = true;
        this.wdpEndDate.Enabled = true;
        this.SetDatePicker();
      }
      else
      {
        this.wdpStartDate.Enabled = false;
        this.wdpEndDate.Enabled = false;
      }
    }

    protected void wibRenewald_Click(object sender, EventArgs e)
    {
      try
      {
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        if (this.ViewState["Imprimir"].ToString() == "Ok")
        {
          string script = "Print();";
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
        }
        else
          this.CreatePopUpServer("RENOVACIÓN DE PLACA", "../Operation/RenewalPlate.aspx?t=" + this.i_PlateTypeId.ToString(), "600px", "520px");
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

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchUniversal();

    protected void custPagerUQ_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        int int32_1 = Convert.ToInt32(this.wddAssociated.SelectedValue.ToString());
        string v_Plate = this.txtPlate.Text.TrimEnd();
        int int32_2 = Convert.ToInt32(this.wddRequirementType1.SelectedValue.ToString());
        int int32_3 = Convert.ToInt32(this.wddStatus.SelectedValue.ToString());
        string v_PaymentCode = this.txtCodPago.Text.TrimEnd();
        DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
        DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
        int i_flag = 1;
        if (!this.chkDate.Checked)
          i_flag = 0;
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.i_ProductId = this.SetProductId(this.i_PlateTypeId);
        this.SearchUniversalList(int32_1, v_Plate, int32_2, int32_3, dateTime1, dateTime2, this.i_PlateTypeId, this.i_ProductId, v_PaymentCode, i_flag, false);
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

    protected void wibExport_Click(object sender, EventArgs e)
    {
      string script = "ExportExcelAll();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void wibReturn_Click(object sender, EventArgs e)
    {
      this.LimpiarCampos();
      this.lblMessage.Text = "";
      string script = UtilDA.ActiveTabIndex("tabs", 0, "1");
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script, true);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        this.Export(this.ExportList());
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

    protected void Button2_Click(object sender, EventArgs e) => this.Print();

    public void ValidarRenovacion()
    {
      try
      {
        DataTable dataTable1 = new DataTable();
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
        this.ViewState["Imprimir"] = (object) "";
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.i_ProductId = this.SetProductId(this.i_PlateTypeId);
        this.pobjAcquisitionQueriesBL = new AcquisitionQueriesBL();
        DataTable dataTable2 = this.pobjAcquisitionQueriesBL.SpecialPlatesRenewalGet(systemUser.i_SystemUserId, this.i_ProductId, this.i_PlateTypeId);
        if (dataTable2.Rows.Count == 0)
        {
          this.wibRenewald.Visible = true;
          this.wibRenewald.Text = "Imprimir";
          this.wibRenewald.ToolTip = "Imprimir";
          this.ViewState["Imprimir"] = (object) "Ok";
        }
        else if (DateTime.Now > Convert.ToDateTime(dataTable2.Rows[0]["fecFin"].ToString()))
        {
          this.wibRenewald.Visible = false;
        }
        else
        {
          this.wibRenewald.Visible = true;
          this.wibRenewald.Text = "Renovar";
          this.wibRenewald.ToolTip = "Renovar";
          this.ViewState["Imprimir"] = (object) "";
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void LoadParameters()
    {
      try
      {
        DataTable dataTable1 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.SpecialRequirementType.ToString(),
          (object) "",
          (object) "1",
          (object) "1"
        });
        if (dataTable1 != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
          {
            if (row["i_GroupId"].ToString() == SystemParameterGroups.SpecialRequirementType.ToString())
              this.wddRequirementType1.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          }
        }
        this.wddRequirementType1.Items.Insert(0, new ListItem("-- Todos --", "0"));
        this.wddRequirementType1.SelectedValue = "-1";
        DataTable dataTable2 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.SpecialStatusRequirement.ToString(),
          (object) "",
          (object) "1",
          (object) "1"
        });
        if (dataTable1 != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
          {
            if (row["i_GroupId"].ToString() == SystemParameterGroups.SpecialStatusRequirement.ToString())
              this.wddStatus.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          }
        }
        this.wddStatus.Items.Insert(0, new ListItem("-- Todos --", "-2"));
        this.wddStatus.SelectedValue = "-2";
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
        this.pobjAssociatedQueriesBL = new AssociatedQueriesBL();
        if (this.Session["ApplicationId"] == null)
          return;
        int int32_1 = Convert.ToInt32(this.Session["ApplicationId"]);
        string userExtendedAction = this.pobjAssociatedQueriesBL.GetSystemUserExtendedAction(systemUser.i_SystemUserId, int32_1);
        DataTable dataTable3 = new DataTable();
        if (userExtendedAction != "")
        {
          int int32_2 = Convert.ToInt32(Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("7", StringComparison.CurrentCulture))));
          if (int32_2 == 7)
          {
            this.FillAssociatedWdd(this.pobjAssociatedQueriesBL.ExhibitionAssociatedListAll(systemUser.i_SystemUserId, int32_2, Convert.ToInt32(this.ViewState["t"].ToString())));
            this.wddAssociated.Items.Insert(0, new ListItem("-- Todos --", "0"));
            this.wddAssociated.SelectedIndex = 0;
          }
          else
          {
            DataTable dt_Result = this.pobjAssociatedQueriesBL.ExhibitionAssociatedListAll(systemUser.i_SystemUserId, 0, Convert.ToInt32(this.ViewState["t"].ToString()));
            if (dt_Result.Rows.Count == 1)
            {
              this.FillAssociatedWdd(dt_Result);
              this.wddAssociated.SelectedIndex = 0;
            }
            else
            {
              this.FillAssociatedWdd(dt_Result);
              this.wddAssociated.Items.Insert(0, new ListItem("-- Todos --", "0"));
              this.wddAssociated.SelectedIndex = 0;
            }
          }
        }
        else
        {
          this.FillAssociatedWdd(this.pobjAssociatedQueriesBL.ExhibitionAssociatedListAll(systemUser.i_SystemUserId, 0, Convert.ToInt32(this.ViewState["t"].ToString())));
          this.wddAssociated.SelectedIndex = 0;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void FillAssociatedWdd(DataTable dt_Result)
    {
      try
      {
        if (dt_Result == null || dt_Result.Rows.Count == 0)
          return;
        this.wddAssociated.DataSource = (object) dt_Result;
        this.wddAssociated.DataTextField = "v_Alias";
        this.wddAssociated.DataValueField = "i_AssociatedId";
        this.wddAssociated.DataBind();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SetDatePicker()
    {
      this.wdpStartDate.Value = DateTime.Now;
      this.wdpEndDate.Value = DateTime.Now;
    }

    private void SearchUniversal()
    {
      try
      {
        int int32_1 = Convert.ToInt32(this.wddAssociated.SelectedValue.ToString());
        string v_Plate = this.txtPlate.Text.TrimEnd();
        int int32_2 = Convert.ToInt32(this.wddRequirementType1.SelectedValue.ToString());
        int int32_3 = Convert.ToInt32(this.wddStatus.SelectedValue.ToString());
        string v_PaymentCode = this.txtCodPago.Text.TrimEnd();
        DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
        DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
        int i_flag = 1;
        if (!this.chkDate.Checked)
          i_flag = 0;
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.i_ProductId = this.SetProductId(this.i_PlateTypeId);
        this.SearchUniversalList(int32_1, v_Plate, int32_2, int32_3, dateTime1, dateTime2, this.i_PlateTypeId, this.i_ProductId, v_PaymentCode, i_flag, true);
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

    private void SearchUniversalList(
      int i_AssociatedId,
      string v_Plate,
      int i_TipoSolicitud,
      int i_Status,
      DateTime d_StartDate,
      DateTime d_EndDate,
      int i_PlateTypeId,
      int i_ProductId,
      string v_PaymentCode,
      int i_flag,
      bool pboolLoadPager)
    {
      try
      {
        int startRowIndex = pboolLoadPager ? 1 : this.custPagerUQ.CurrentPageNumber;
        int maxRows = this.custPagerUQ.CurrentPageSize == 0 ? 10 : this.custPagerUQ.CurrentPageSize;
        int pinttotalRows;
        DataTable dataTable = new UniversalQueriesBL().UniversalQuery(i_AssociatedId, v_Plate, i_TipoSolicitud, i_Status, d_StartDate, d_EndDate, i_PlateTypeId, i_ProductId, v_PaymentCode, i_flag, startRowIndex, maxRows, out pinttotalRows);
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
        }
        else
        {
          this.wibExcel.Enabled = true;
          this.wdgList.DataSource = (object) dataTable;
          this.wdgList.DataBind();
        }
        int num = pinttotalRows;
        this.custPagerUQ.TotalPages = num % maxRows == 0 ? num / maxRows : num / maxRows + 1;
        this.custPagerUQ.TotalRecordCount = pinttotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerUQ.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.HidePopup();
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

    private DataTable ExportList()
    {
      try
      {
        int int32_1 = Convert.ToInt32(this.wddAssociated.SelectedValue.ToString());
        string v_Plate = this.txtPlate.Text.TrimEnd();
        int int32_2 = Convert.ToInt32(this.wddRequirementType1.SelectedValue.ToString());
        int int32_3 = Convert.ToInt32(this.wddStatus.SelectedValue.ToString());
        string v_PaymentCode = this.txtCodPago.Text.TrimEnd();
        DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
        DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
        int i_flag = 1;
        if (!this.chkDate.Checked)
          i_flag = 0;
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.i_ProductId = this.SetProductId(this.i_PlateTypeId);
        DataTable dataTable = new DataTable();
        return new UniversalQueriesBL().UniversalQuery(int32_1, v_Plate, int32_2, int32_3, dateTime1, dateTime2, this.i_PlateTypeId, this.i_ProductId, v_PaymentCode, i_flag, 0, 0, out int _);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void Export(DataTable dt_Result)
    {
      try
      {
        DataTable dataTable = (DataTable) this.Session["dtExport"];
        ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
        List<ClassColumns> classColumnsList = new List<ClassColumns>();
        foreach (DataControlField column in (StateManagedCollection) this.wdgList.Columns)
        {
          if (column.Visible && column.GetType().Name == "BoundField")
          {
            BoundField boundField = (BoundField) column;
            classColumnsList.Add(new ClassColumns(boundField.DataField, 1, (int) column.ControlStyle.Width.Value, boundField.HeaderText));
          }
        }
        exportToExcelDataGrid.clsTitle = classColumnsList;
        exportToExcelDataGrid.AgregarHojaLibro(dt_Result, "Consulta Universal");
        exportToExcelDataGrid.CerrarLibro();
        byte[] buffer = exportToExcelDataGrid.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=ConsultaUniversal.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
      }
      catch (Exception ex)
      {
      }
    }

    private void Print()
    {
      try
      {
        DataTable dataTable1 = new DataTable();
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
        AcquisitionManagementBL acquisitionManagementBl = new AcquisitionManagementBL();
        int int32 = Convert.ToInt32(systemUser.i_IsAssociatedAAP.ToString());
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.i_ProductId = this.SetProductId(this.i_PlateTypeId);
        DataTable dataTable2 = acquisitionManagementBl.SpecialPlateReportPlatesRenewal(systemUser.i_SystemUserId, this.i_ProductId, this.i_PlateTypeId);
        if (dataTable2.Rows.Count == 0)
          throw new HandledException(1, "No existen pagos pendientes.");
        if (!dataTable2.Columns.Contains("v_Message"))
          dataTable2.Columns.Add("v_Message", Type.GetType("System.String"));
        ReportDocument reportDocument = new ReportDocument();
        string filename;
        if (this.i_PlateTypeId == 7)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
          {
            string empty = string.Empty;
            string str = ConfigurationManager.AppSettings["_strcadenaUniversalQueryExhibition"].ToString().Replace("\\n", Environment.NewLine);
            if (int32 != 1)
              str = ConfigurationManager.AppSettings["_strcadenaUniversalQueryExhibitionNoAsociado"].ToString().Replace("\\n", Environment.NewLine);
            row["v_Message"] = (object) str.ToString();
          }
          filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportExhibition.rpt";
        }
        else
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
          {
            string empty = string.Empty;
            string str = ConfigurationManager.AppSettings["_strcadenaUniversalQueryRotativas"].ToString().Replace("\\n", Environment.NewLine);
            if (int32 != 1)
              str = ConfigurationManager.AppSettings["_strcadenaUniversalQueryRotativasNoAsociado"].ToString().Replace("\\n", Environment.NewLine);
            row["v_Message"] = (object) str.ToString();
          }
          filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportRotate.rpt";
        }
        reportDocument.Load(filename);
        reportDocument.SetDataSource(dataTable2);
        reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "CodigoPago");
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

    public void CargarDetalle(string strplate)
    {
      try
      {
        this.pobjUniversalQueriesBL = new UniversalQueriesBL();
        DataTable dataTable1 = new DataTable();
        Convert.ToDateTime(this.wdpStartDate.Value);
        Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["t"]);
        this.i_ProductId = this.SetProductId(this.i_PlateTypeId);
        DataTable dataTable2 = this.pobjUniversalQueriesBL.UniversalQuery(0, strplate, 0, -2, Convert.ToDateTime("01/01/2010"), Convert.ToDateTime("31/12/9999"), this.i_PlateTypeId, this.i_ProductId, "", 0, 1, 10, out int _);
        this.txtRazonSocial.Text = dataTable2.Rows[0]["RazonSocial"].ToString();
        this.txtRuc.Text = dataTable2.Rows[0]["RUC"].ToString();
        this.txtPlaca.Text = dataTable2.Rows[0]["Placa"].ToString();
        this.txtEstadoPlaca.Text = dataTable2.Rows[0]["EstadoSolicitud"].ToString();
        DataTable dataTable3 = new DataTable();
        this.wdgDetalle.DataSource = (object) this.pobjUniversalQueriesBL.UniversalDetail(Convert.ToInt32(this.ViewState["i_SystemUserId"]), strplate, this.i_PlateTypeId, this.i_ProductId);
        this.wdgDetalle.DataBind();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void LimpiarCampos()
    {
      this.wddAssociated.SelectedIndex = 0;
      this.txtPlate.Text = "";
      this.wddRequirementType1.SelectedIndex = 0;
      this.wddStatus.SelectedValue = "-2";
      this.chkDate.Checked = true;
      this.wdpStartDate.Enabled = true;
      this.wdpEndDate.Enabled = true;
      this.SetDatePicker();
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
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void wdgList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void wdgList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        if (!e.CommandName.Equals("getData"))
          return;
        this.CargarDetalle((this.wdgList.Rows[Convert.ToInt32(e.CommandArgument)] ?? throw new HandledException(4, "Error de selección.", "'wdgList' - UniversalQuery.aspx")).Cells[3].Text);
        string script = UtilDA.ActiveTabIndex("tabs", 1, "0");
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
  }
}
