// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.ClaimBookInbox
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.BE;
using SIIV.Claim.BL;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims
{
  public class ClaimBookInbox : Page
  {
    protected UpdatePanel UpdatePanel2;
    protected Label lblNReclamo;
    protected TextBox txtClaimCodeSearch;
    protected Label lblEstado;
    protected DropDownList wddClaimStatusSearch;
    protected Label lblFechaDesde;
    protected Fecha wdpStartDate;
    protected Label lblFechaHasta;
    protected Fecha wdpEndDate;
    protected Button wibSearch;
    protected Button wibExportPdf;
    protected Button wibExportExcel;
    protected GridView wdgList;
    protected Pager custPagerBatch;
    protected Label lblRecordCount;
    protected Button wibManagement;
    protected UpdatePanel UpdatePanel1;
    protected Label lblNReclamo2;
    protected TextBox txtClaimCode;
    protected Label lblFechaRegistro;
    protected Fecha wdpRegisterDate;
    protected Label Label1;
    protected DropDownList wddLocation;
    protected Label Label3;
    protected TextBox txtPlate;
    protected RequiredFieldValidator RequiredFieldValidator4;
    protected ValidatorCalloutExtender ValidatorCalloutExtender1;
    protected Label Label4;
    protected TextBox txtName;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected ValidatorCalloutExtender RequiredFieldValidator1_ValidatorCalloutExtender;
    protected Label Label5;
    protected TextBox txtLastName;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected ValidatorCalloutExtender RequiredFieldValidator2_ValidatorCalloutExtender;
    protected Label Label6;
    protected TextBox txtAddress;
    protected RequiredFieldValidator RequiredFieldValidator3;
    protected ValidatorCalloutExtender RequiredFieldValidator3_ValidatorCalloutExtender;
    protected Label Label7;
    protected DropDownList wddDocumentType;
    protected Label Label8;
    protected TextBox txtDocumentNumber;
    protected FilteredTextBoxExtender txtDocumentNumber_FilteredTextBoxExtender;
    protected Label Label9;
    protected TextBox txtTelephoneNumber;
    protected FilteredTextBoxExtender txtTelephoneNumber_FilteredTextBoxExtender;
    protected Label Label10;
    protected TextBox txtEmail;
    protected RegularExpressionValidator RegularExpressionValidator1;
    protected ValidatorCalloutExtender RegularExpressionValidator1_ValidatorCalloutExtender;
    protected CheckBox chkSendEmail;
    protected CheckBox chkMinority;
    protected Panel pApod;
    protected Label Label11;
    protected TextBox txtAttorneyName;
    protected Label Label12;
    protected TextBox txtAttorneyAddress;
    protected Label Label13;
    protected TextBox txtAttorneyPhoneNumber;
    protected FilteredTextBoxExtender txtAttorneyPhoneNumber_FilteredTextBoxExtender;
    protected Label Label2;
    protected TextBox txtAttorneyEmail;
    protected RegularExpressionValidator RegularExpressionValidator2;
    protected ValidatorCalloutExtender RegularExpressionValidator2_ValidatorCalloutExtender;
    protected RadioButton rbService;
    protected RadioButton rbProduct;
    protected Label Label15;
    protected TextBox txtDescription;
    protected RadioButton rbClaim;
    protected RadioButton rbComplaint;
    protected Label Label16;
    protected TextBox txtComments;
    protected Label Label17;
    protected HtmlInputFile UploadFile;
    protected Button wibExport;
    protected Label lblmsn;
    protected Label Label18;
    protected Label Label19;
    protected Label Label20;
    protected Label Label21;
    protected CheckBox chkAccept;
    protected Button wibPrint;
    protected Button wibCancel;
    protected Label lblMessageClaimBook;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadParameters();
      this.SetDatePicker();
      if (this.Session["Consult"] != null && Convert.ToBoolean(this.Session["Consult"], (IFormatProvider) CultureInfo.CurrentCulture))
        this.SearchBookInbox();
    }

    protected void btnSearch_Click(object sender, EventArgs e) => this.SearchBookInbox();

    protected void btnExportPdf_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable dt = new DataTable();
        dt.Columns.Add("DateRegister", Type.GetType("System.DateTime"));
        dt.Columns.Add("LocationName", Type.GetType("System.String"));
        dt.Columns.Add("ClaimCode", Type.GetType("System.String"));
        dt.Columns.Add("NroPlate", Type.GetType("System.String"));
        dt.Columns.Add("CustomerName", Type.GetType("System.String"));
        dt.Columns.Add("CustomerLastName", Type.GetType("System.String"));
        dt.Columns.Add("CustomerAddress", Type.GetType("System.String"));
        dt.Columns.Add("CustomerDocumentType", Type.GetType("System.String"));
        dt.Columns.Add("CustomerDocumentNumber", Type.GetType("System.String"));
        dt.Columns.Add("CustomerTelephoneNumber", Type.GetType("System.String"));
        dt.Columns.Add("CustomerEmail", Type.GetType("System.String"));
        dt.Columns.Add("IsMinor", Type.GetType("System.Int32"));
        dt.Columns.Add("AttorneyName", Type.GetType("System.String"));
        dt.Columns.Add("AttorneyAddress", Type.GetType("System.String"));
        dt.Columns.Add("AttorneyTelephoneNumber", Type.GetType("System.String"));
        dt.Columns.Add("AttorneyEmail", Type.GetType("System.String"));
        dt.Columns.Add("IsService", Type.GetType("System.Int32"));
        dt.Columns.Add("IsProduct", Type.GetType("System.Int32"));
        dt.Columns.Add("Description", Type.GetType("System.String"));
        dt.Columns.Add("IsClaim", Type.GetType("System.Int32"));
        dt.Columns.Add("IsComplaint", Type.GetType("System.Int32"));
        dt.Columns.Add("ClaimDescription", Type.GetType("System.String"));
        dt.Columns.Add("IsAccept", Type.GetType("System.Int32"));
        foreach (DataRow row1 in (InternalDataCollectionBase) ((DataTable) this.ViewState["dtResult"]).Rows)
        {
          DataRow row2 = dt.NewRow();
          row2["DateRegister"] = row1["d_InsertDate"];
          row2["LocationName"] = row1["v_LocationName"];
          row2["ClaimCode"] = row1["v_ClaimCode"];
          string[] source1 = row1["v_Requester"].ToString().Split('|');
          row2["CustomerName"] = ((IEnumerable<string>) source1).Count<string>() > 0 ? (object) source1[0] : (object) "";
          row2["CustomerLastName"] = ((IEnumerable<string>) source1).Count<string>() > 1 ? (object) source1[1] : (object) "";
          row2["CustomerAddress"] = ((IEnumerable<string>) source1).Count<string>() > 2 ? (object) source1[2] : (object) "";
          row2["CustomerDocumentType"] = ((IEnumerable<string>) source1).Count<string>() > 3 ? (object) source1[3] : (object) "";
          row2["CustomerDocumentNumber"] = ((IEnumerable<string>) source1).Count<string>() > 4 ? (object) source1[4] : (object) "";
          row2["CustomerTelephoneNumber"] = ((IEnumerable<string>) source1).Count<string>() > 5 ? (object) source1[5] : (object) "";
          row2["CustomerEmail"] = ((IEnumerable<string>) source1).Count<string>() > 6 ? (object) source1[6] : (object) "";
          row2["IsMinor"] = ((IEnumerable<string>) source1).Count<string>() > 7 ? (object) source1[7] : (object) "";
          row2["AttorneyName"] = ((IEnumerable<string>) source1).Count<string>() > 8 ? (object) source1[8] : (object) "";
          row2["AttorneyAddress"] = ((IEnumerable<string>) source1).Count<string>() > 9 ? (object) source1[9] : (object) "";
          row2["AttorneyTelephoneNumber"] = ((IEnumerable<string>) source1).Count<string>() > 10 ? (object) source1[10] : (object) "";
          row2["AttorneyEmail"] = ((IEnumerable<string>) source1).Count<string>() > 11 ? (object) source1[11] : (object) "";
          string[] source2 = row1["v_RequestValues"].ToString().Split('|');
          string[] strArray = source2[3].Split('-');
          row2["IsService"] = ((IEnumerable<string>) source2).Count<string>() > 0 ? (source2[0] == "1" ? (object) "1" : (object) "0") : (object) "0";
          row2["IsProduct"] = ((IEnumerable<string>) source2).Count<string>() > 0 ? (source2[0] == "2" ? (object) "1" : (object) "0") : (object) "0";
          row2["Description"] = ((IEnumerable<string>) source2).Count<string>() > 1 ? (object) source2[1] : (object) "";
          row2["IsClaim"] = (object) (bool) (((IEnumerable<string>) source2).Count<string>() > 2 ? (source2[2] == "1" ? 1 : 0) : 0);
          row2["IsComplaint"] = (object) (bool) (((IEnumerable<string>) source2).Count<string>() > 2 ? (source2[2] == "2" ? 1 : 0) : 0);
          row2["NroPlate"] = strArray.ToString().Trim().Count<char>() > 3 ? (object) strArray[0].ToString().Trim() : (object) "";
          row2["ClaimDescription"] = strArray.ToString().Trim().Count<char>() > 3 ? (object) strArray[1].ToString().Trim() : (object) "";
          row2["IsAccept"] = (object) (bool) (((IEnumerable<string>) source2).Count<string>() > 4 ? (source2[4] == "1" ? 1 : 0) : 0);
          dt.Rows.Add(row2);
        }
        this.SetCrystalReport(dt);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e) => this.ExcelExport();

    protected void wdgList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      int int32_1 = Convert.ToInt32(e.CommandArgument);
      if (e.CommandName == "Read")
      {
        this.lblmsn.Text = string.Empty;
        RequirementClaim currentRow = this.GetCurrentRow(int32_1);
        this.EnabledControls(false);
        this.SetControls(currentRow);
        this.wibPrint.Enabled = true;
        string script = UtilDA.ActiveTabIndex("tabs", 1, "0");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
      }
      if (!(e.CommandName == "Manage"))
        return;
      GridViewRow row = this.wdgList.Rows[int32_1];
      int int32_2 = Convert.ToInt32(this.wdgList.DataKeys[int32_1]["i_RequirementClaimId"]);
      short int16 = Convert.ToInt16(this.wdgList.DataKeys[int32_1]["i_ClaimTypeId"]);
      string str1 = this.wdgList.DataKeys[int32_1]["v_Requester"].ToString();
      string str2 = this.wdgList.DataKeys[int32_1]["i_Status"].ToString();
      this.Session["i_RequirementClaimId"] = (object) int32_2;
      this.Session["i_ClaimTypeId"] = (object) int16;
      this.Session["v_Requester"] = (object) str1;
      this.Session["i_Status"] = (object) str2;
      this.Response.Redirect("ClaimBookManage.aspx");
    }

    protected void custPagerBatch_PageChanged(object sender, CustomPageChangeArgs e)
    {
      this.SearchBookInboxList(0, 0, 0, 8, 0, 0, 0, this.wddClaimStatusSearch.SelectedValue != "" ? (int) Convert.ToInt16(this.wddClaimStatusSearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) : 0, this.txtClaimCodeSearch.Text, new DateTime?(Convert.ToDateTime((object) this.wdpStartDate.Value, (IFormatProvider) CultureInfo.CurrentCulture)), new DateTime?(Convert.ToDateTime((object) this.wdpEndDate.Value.AddDays(1.0), (IFormatProvider) CultureInfo.CurrentCulture)), false);
    }

    protected void btnManagement_Click(object sender, EventArgs e)
    {
    }

    protected void wddDocumentType_SelectionChanged(object sender, EventArgs e)
    {
      if (this.wddDocumentType.SelectedValue == "1")
      {
        this.txtDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Numbers;
        this.txtDocumentNumber.MaxLength = 8;
      }
      else if (this.wddDocumentType.SelectedValue == "4")
      {
        this.txtDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Numbers;
        this.txtDocumentNumber.MaxLength = 11;
      }
      else
      {
        this.txtDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Custom;
        this.txtDocumentNumber_FilteredTextBoxExtender.ValidChars = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz1234567890";
        this.txtDocumentNumber.MaxLength = 20;
      }
    }

    protected void chkMinority_CheckedChanged(object sender, EventArgs e)
    {
      this.pApod.Visible = this.chkMinority.Checked;
    }

    protected void wibExport_Click(object sender, EventArgs e)
    {
      string path1 = this.Server.MapPath("../ClaimBook/Image");
      string empty = string.Empty;
      string[] files = Directory.GetFiles(path1);
      string path2 = string.Empty;
      string str1 = string.Empty;
      foreach (string str2 in files)
      {
        if (str2.Contains(this.txtClaimCode.Text))
        {
          path2 = str2;
          string path3 = str2.ToString((IFormatProvider) CultureInfo.CurrentCulture);
          Path.GetFileName(path3);
          str1 = Path.GetExtension(path3);
          break;
        }
      }
      if (path2 != "")
      {
        byte[] buffer = File.ReadAllBytes(path2);
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=archivo" + str1);
        this.Response.BinaryWrite(buffer);
        this.Response.End();
      }
      else
        this.lblmsn.Text = "No hay archivo adjunto para este reclamo";
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
      DataTable dt = new DataTable();
      dt.TableName = "Tabla";
      dt.Columns.Add("DateRegister", Type.GetType("System.DateTime"));
      dt.Columns.Add("LocationName", Type.GetType("System.String"));
      dt.Columns.Add("ClaimCode", Type.GetType("System.String"));
      dt.Columns.Add("NroPlate", Type.GetType("System.String"));
      dt.Columns.Add("CustomerName", Type.GetType("System.String"));
      dt.Columns.Add("CustomerLastName", Type.GetType("System.String"));
      dt.Columns.Add("CustomerAddress", Type.GetType("System.String"));
      dt.Columns.Add("CustomerDocumentType", Type.GetType("System.String"));
      dt.Columns.Add("CustomerDocumentNumber", Type.GetType("System.String"));
      dt.Columns.Add("CustomerTelephoneNumber", Type.GetType("System.String"));
      dt.Columns.Add("CustomerEmail", Type.GetType("System.String"));
      dt.Columns.Add("IsMinor", Type.GetType("System.Int32"));
      dt.Columns.Add("AttorneyName", Type.GetType("System.String"));
      dt.Columns.Add("AttorneyAddress", Type.GetType("System.String"));
      dt.Columns.Add("AttorneyTelephoneNumber", Type.GetType("System.String"));
      dt.Columns.Add("AttorneyEmail", Type.GetType("System.String"));
      dt.Columns.Add("IsService", Type.GetType("System.Int32"));
      dt.Columns.Add("IsProduct", Type.GetType("System.Int32"));
      dt.Columns.Add("Description", Type.GetType("System.String"));
      dt.Columns.Add("IsClaim", Type.GetType("System.Int32"));
      dt.Columns.Add("IsComplaint", Type.GetType("System.Int32"));
      dt.Columns.Add("ClaimDescription", Type.GetType("System.String"));
      dt.Columns.Add("IsAccept", Type.GetType("System.Int32"));
      DataRow row = dt.NewRow();
      row["DateRegister"] = this.Session["vClaimDateTime"];
      row["LocationName"] = (object) this.wddLocation.SelectedItem.ToString();
      row["ClaimCode"] = (object) this.txtClaimCode.Text;
      row["NroPlate"] = (object) this.txtPlate.Text;
      row["CustomerName"] = (object) this.txtName.Text;
      row["CustomerLastName"] = (object) this.txtLastName.Text;
      row["CustomerAddress"] = (object) this.txtAddress.Text;
      row["CustomerDocumentType"] = (object) this.wddDocumentType.SelectedItem.ToString();
      row["CustomerDocumentNumber"] = (object) this.txtDocumentNumber.Text;
      row["CustomerTelephoneNumber"] = (object) this.txtTelephoneNumber.Text;
      row["CustomerEmail"] = (object) this.txtEmail.Text;
      row["IsMinor"] = this.chkMinority.Checked ? (object) "1" : (object) "0";
      row["AttorneyName"] = (object) this.txtAttorneyName.Text;
      row["AttorneyAddress"] = (object) this.txtAttorneyAddress.Text;
      row["AttorneyTelephoneNumber"] = (object) this.txtAttorneyPhoneNumber.Text;
      row["AttorneyEmail"] = (object) this.txtAttorneyEmail.Text;
      row["IsService"] = this.rbService.Checked ? (object) "1" : (object) "0";
      row["IsProduct"] = this.rbProduct.Checked ? (object) "1" : (object) "0";
      row["Description"] = (object) this.txtDescription.Text;
      row["IsClaim"] = this.rbClaim.Checked ? (object) "1" : (object) "0";
      row["IsComplaint"] = this.rbComplaint.Checked ? (object) "1" : (object) "0";
      row["ClaimDescription"] = (object) this.txtComments.Text;
      row["IsAccept"] = this.chkAccept.Checked ? (object) "1" : (object) "0";
      dt.Rows.Add(row);
      this.SetCrystalReport(dt);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      this.wibPrint.Enabled = false;
      this.EnabledControls(false);
      this.ClearControls();
      this.lblMessageClaimBook.Visible = false;
      string script = UtilDA.ActiveTabIndex("tabs", 0, "1");
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void LoadParameters()
    {
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) (SystemParameterGroups.ClaimStatus.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ", " + SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
        (object) "",
        (object) "1",
        (object) "1"
      });
      this.wddClaimStatusSearch.Items.Clear();
      this.wddDocumentType.Items.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (Convert.ToInt32(row["i_GroupId"], (IFormatProvider) CultureInfo.CurrentCulture) == SystemParameterGroups.ClaimStatus)
          this.wddClaimStatusSearch.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        if (Convert.ToInt32(row["i_GroupId"], (IFormatProvider) CultureInfo.CurrentCulture) == SystemParameterGroups.PersonDocumentType)
          this.wddDocumentType.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Value"].ToString(), row["i_ParameterId"].ToString()));
      }
      this.wddClaimStatusSearch.Items.Insert(0, new System.Web.UI.WebControls.ListItem(SIIV.SystemParameter.BL.Constants.OPCIONLISTA_Todos, SIIV.SystemParameter.BL.Constants.OPCIONLISTA_ValorSinSeleccion));
      this.wddDocumentType.Items.Insert(0, new System.Web.UI.WebControls.ListItem(SIIV.SystemParameter.BL.Constants.OPCIONLISTA_Seleccione, SIIV.SystemParameter.BL.Constants.OPCIONLISTA_ValorSinSeleccion));
      this.ViewState["dtLocation"] = (object) new SIIV.Warehouse.BL.LocationQueriesBL().GetLocationUserBy(2);
      this.wddLocation.DataSource = (object) (DataTable) this.ViewState["dtLocation"];
      this.wddLocation.DataTextField = "v_Description";
      this.wddLocation.DataValueField = "i_LocationId";
      this.wddLocation.DataBind();
      this.wddLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem(SIIV.SystemParameter.BL.Constants.OPCIONLISTA_Seleccione, SIIV.SystemParameter.BL.Constants.OPCIONLISTA_ValorSinSeleccion));
      this.wdpRegisterDate.Value = DateTime.Now;
    }

    private void SetDatePicker()
    {
      this.wdpStartDate.Value = DateTime.Now;
      this.wdpEndDate.Value = DateTime.Now;
    }

    private void SearchBookInbox()
    {
      try
      {
        this.SearchBookInboxList(0, 0, 0, 8, 0, 0, 0, this.wddClaimStatusSearch.SelectedValue != "" ? (int) Convert.ToInt16(this.wddClaimStatusSearch.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture) : 0, this.txtClaimCodeSearch.Text, new DateTime?(Convert.ToDateTime((object) this.wdpStartDate.Value, (IFormatProvider) CultureInfo.CurrentCulture)), new DateTime?(Convert.ToDateTime((object) this.wdpEndDate.Value.AddDays(1.0), (IFormatProvider) CultureInfo.CurrentCulture)), true);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblRecordCount, enmMessageType.Error, "Error*****<br>" + ex.Message);
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SearchBookInboxList(
      int pintRequirementClaimId,
      int pintRequirementPlateId,
      int pintRequirementPlateRefId,
      int pintClaimTypeId,
      int pintAssignedUserId,
      int pintPriority,
      int pintUserId,
      int pintStatus,
      string pstrclaimcode,
      DateTime? pdtstartdate,
      DateTime? pdtenddate,
      bool pboolLoadPager)
    {
      int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerBatch.CurrentPageNumber;
      int pintMaxRows = this.custPagerBatch.CurrentPageSize == 0 ? 10 : this.custPagerBatch.CurrentPageSize;
      int pintTotalRows;
      DataTable allClaimBookPag = new RequirementClaimQueriesBL().GetAllClaimBookPag(pintRequirementClaimId, pintRequirementPlateId, pintRequirementPlateRefId, pintClaimTypeId, pintAssignedUserId, pintPriority, pintUserId, pintStatus, pstrclaimcode, pdtstartdate, pdtenddate, pintStartRowIndex, pintMaxRows, out pintTotalRows);
      int num = pintTotalRows;
      this.wdgList.DataSource = (object) allClaimBookPag;
      this.wdgList.DataBind();
      this.ViewState["dtResult"] = (object) allClaimBookPag;
      this.wibExportExcel.Enabled = allClaimBookPag != null && allClaimBookPag.Rows.Count > 0;
      this.wibExportPdf.Enabled = allClaimBookPag != null && allClaimBookPag.Rows.Count > 0;
      this.custPagerBatch.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
      this.custPagerBatch.TotalRecordCount = pintTotalRows;
      if (!pboolLoadPager)
        return;
      this.custPagerBatch.LoadPager();
    }

    private void SetCrystalReport(DataTable dt)
    {
      ReportDocument reportDocument = new ReportDocument();
      string filename = this.Server.MapPath("../ClaimBook/RptClaimBook.rpt");
      reportDocument.Load(filename);
      reportDocument.SetDataSource(dt);
      reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "LibroReclamacion");
      reportDocument.Close();
      ((Component) reportDocument).Dispose();
    }

    public void ExcelExport()
    {
      try
      {
        DataTable dataTable1 = (DataTable) this.ViewState["dtResult"];
        if (dataTable1 == null || dataTable1.Rows.Count == 0)
          return;
        string str1 = "LibroReclamacion";
        DateTime now = DateTime.Now;
        string[] strArray1 = new string[8];
        strArray1[0] = str1;
        int num = now.Day;
        strArray1[1] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        num = now.Month;
        strArray1[2] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        num = now.Year;
        strArray1[3] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        num = now.Hour;
        strArray1[4] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        num = now.Minute;
        strArray1[5] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        num = now.Second;
        strArray1[6] = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        strArray1[7] = ".xls";
        string pstrFileTarget = string.Concat(strArray1);
        DataTable datos = new DataTable();
        datos.Columns.Add("Fecha", Type.GetType("System.String"));
        datos.Columns.Add("Ubicacion", Type.GetType("System.String"));
        datos.Columns.Add("CodigoReclamo", Type.GetType("System.String"));
        datos.Columns.Add("NroPlate", Type.GetType("System.String"));
        datos.Columns.Add("NombreSolicitante", Type.GetType("System.String"));
        datos.Columns.Add("ApellidoSolicitante", Type.GetType("System.String"));
        datos.Columns.Add("DireccionSolicitante", Type.GetType("System.String"));
        datos.Columns.Add("DocIdentidadSolicitante", Type.GetType("System.String"));
        datos.Columns.Add("NroDocIdentidadSolicitante", Type.GetType("System.String"));
        datos.Columns.Add("TelefonoSolicitante", Type.GetType("System.String"));
        datos.Columns.Add("EmailSolicitante", Type.GetType("System.String"));
        datos.Columns.Add("EsMenorEdad", Type.GetType("System.String"));
        datos.Columns.Add("NombreApoderado", Type.GetType("System.String"));
        datos.Columns.Add("DireccionApoderado", Type.GetType("System.String"));
        datos.Columns.Add("TelefonoApoderado", Type.GetType("System.String"));
        datos.Columns.Add("EmailApoderado", Type.GetType("System.String"));
        datos.Columns.Add("EsServicioProducto", Type.GetType("System.String"));
        datos.Columns.Add("DescripcionBienContratado", Type.GetType("System.String"));
        datos.Columns.Add("EsReclamoQueja", Type.GetType("System.String"));
        datos.Columns.Add("DescripcionReclamo", Type.GetType("System.String"));
        datos.Columns.Add("EstadoReclamo", Type.GetType("System.String"));
        datos.Columns.Add("FecUltRespuesta", Type.GetType("System.String"));
        datos.Columns.Add("RespuestaReclamo", Type.GetType("System.String"));
        foreach (DataRow row1 in (InternalDataCollectionBase) dataTable1.Rows)
        {
          DataRow row2 = datos.NewRow();
          row2["Fecha"] = row1["v_ClaimDate"];
          row2["Ubicacion"] = row1["v_LocationName"];
          row2["CodigoReclamo"] = row1["v_ClaimCode"];
          string[] source1 = row1["v_Requester"].ToString().Split('|');
          row2["NombreSolicitante"] = ((IEnumerable<string>) source1).Count<string>() > 0 ? (object) source1[0].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["ApellidoSolicitante"] = ((IEnumerable<string>) source1).Count<string>() > 1 ? (object) source1[1].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["DireccionSolicitante"] = ((IEnumerable<string>) source1).Count<string>() > 2 ? (object) source1[2].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["DocIdentidadSolicitante"] = ((IEnumerable<string>) source1).Count<string>() > 3 ? (object) source1[3].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["NroDocIdentidadSolicitante"] = ((IEnumerable<string>) source1).Count<string>() > 4 ? (object) source1[4].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["TelefonoSolicitante"] = ((IEnumerable<string>) source1).Count<string>() > 5 ? (object) source1[5].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["EmailSolicitante"] = ((IEnumerable<string>) source1).Count<string>() > 6 ? (object) source1[6].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["EsMenorEdad"] = ((IEnumerable<string>) source1).Count<string>() > 7 ? (source1[7].ToString((IFormatProvider) CultureInfo.CurrentCulture) == "1" ? (object) "Si" : (object) "No") : (object) "No";
          row2["NombreApoderado"] = ((IEnumerable<string>) source1).Count<string>() > 8 ? (object) source1[8].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["DireccionApoderado"] = ((IEnumerable<string>) source1).Count<string>() > 9 ? (object) source1[9].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["TelefonoApoderado"] = ((IEnumerable<string>) source1).Count<string>() > 10 ? (object) source1[10].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["EmailApoderado"] = ((IEnumerable<string>) source1).Count<string>() > 11 ? (object) source1[11].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          string[] source2 = row1["v_RequestValues"].ToString().Split('|');
          string[] strArray2 = source2[3].Split('-');
          row2["EsServicioProducto"] = ((IEnumerable<string>) source2).Count<string>() > 0 ? (source2[0].ToString((IFormatProvider) CultureInfo.CurrentCulture) == "1" ? (object) "Servicio" : (object) "Producto") : (object) "Ninguno";
          row2["DescripcionBienContratado"] = ((IEnumerable<string>) source2).Count<string>() > 1 ? (object) source2[1].ToString((IFormatProvider) CultureInfo.CurrentCulture) : (object) "";
          row2["EsReclamoQueja"] = ((IEnumerable<string>) source2).Count<string>() > 2 ? (source2[2].ToString((IFormatProvider) CultureInfo.CurrentCulture) == "1" ? (object) "Reclamo" : (object) "Queja") : (object) "Ninguno";
          row2["NroPlate"] = strArray2.ToString().Trim().Count<char>() > 3 ? (object) strArray2[0].ToString().Trim() : (object) "";
          row2["DescripcionReclamo"] = strArray2.ToString().Trim().Count<char>() > 3 ? (object) strArray2[1].ToString().Trim() : (object) "";
          row2["EstadoReclamo"] = row1["v_Status"];
          row2["FecUltRespuesta"] = row1["d_ResponseDate"];
          row2["RespuestaReclamo"] = row1["v_ResponseComments"];
          datos.Rows.Add(row2);
        }
        ArrayList titulos = new ArrayList();
        DataTable dataTable2 = new DataTable();
        string str2 = this.Server.MapPath("../ClaimBook/") + pstrFileTarget;
        OtherFormats otherFormats = new OtherFormats(str2);
        for (int index = 0; index < datos.Columns.Count; ++index)
          titulos.Add((object) datos.Columns[index].ColumnName);
        otherFormats.ExportClaimBook("Libro Reclamaciones", titulos, datos);
        new ExportFile().Download(str2, pstrFileTarget);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private RequirementClaim GetCurrentRow(int index)
    {
      RequirementClaim currentRow = new RequirementClaim();
      if (this.wdgList.Rows.Count > 0)
      {
        GridViewRow row = this.wdgList.Rows[index];
        currentRow.i_RequirementClaimId = Convert.ToInt32(this.wdgList.DataKeys[index]["i_RequirementClaimId"].ToString());
        currentRow.i_ClaimTypeId = new int?((int) Convert.ToInt16(this.wdgList.DataKeys[index]["i_ClaimTypeId"].ToString()));
        currentRow.v_ClaimDate = this.wdgList.Rows[index].Cells[2].Text;
        this.Session["vClaimDateTime"] = (object) this.wdgList.Rows[index].Cells[2].Text;
        currentRow.i_Status = Convert.IsDBNull(this.wdgList.DataKeys[index]["i_Status"]) ? new int?() : new int?((int) Convert.ToInt16(this.wdgList.DataKeys[index]["i_Status"]));
        currentRow.i_AssignedUserId = Convert.IsDBNull(this.wdgList.DataKeys[index]["i_AssignedUserId"]) ? new int?() : new int?((int) Convert.ToInt16(this.wdgList.DataKeys[index]["i_AssignedUserId"]));
        currentRow.v_Requester = this.wdgList.DataKeys[index]["v_Requester"].ToString();
        currentRow.v_RequestValues = this.wdgList.DataKeys[index]["v_RequestValues"].ToString();
        currentRow.v_Comments = row.Cells[5].Text;
        currentRow.i_LocationId = Convert.IsDBNull(this.wdgList.DataKeys[index]["i_LocationId"]) ? new int?() : new int?(Convert.ToInt32(this.wdgList.DataKeys[index]["i_LocationId"]));
        currentRow.v_ClaimCode = row.Cells[3].Text;
      }
      return currentRow;
    }

    private void EnabledControls(bool enabled)
    {
      this.wddLocation.Enabled = enabled;
      this.txtPlate.Enabled = enabled;
      this.txtName.Enabled = enabled;
      this.txtLastName.Enabled = enabled;
      this.txtAddress.Enabled = enabled;
      this.wddDocumentType.Enabled = enabled;
      this.txtDocumentNumber.Enabled = enabled;
      this.txtTelephoneNumber.Enabled = enabled;
      this.txtEmail.Enabled = enabled;
      this.chkMinority.Enabled = enabled;
      this.txtAttorneyName.Enabled = enabled;
      this.txtAttorneyAddress.Enabled = enabled;
      this.txtAttorneyPhoneNumber.Enabled = enabled;
      this.txtAttorneyEmail.Enabled = enabled;
      this.rbService.Enabled = enabled;
      this.rbProduct.Enabled = enabled;
      this.rbClaim.Enabled = enabled;
      this.rbComplaint.Enabled = enabled;
      this.UploadFile.Disabled = !enabled;
      this.chkAccept.Enabled = enabled;
    }

    private void SetControls(RequirementClaim objParam)
    {
      this.wdpRegisterDate.Value = Convert.ToDateTime(objParam.v_ClaimDate, (IFormatProvider) CultureInfo.CurrentCulture);
      this.txtClaimCode.Text = objParam.v_ClaimCode;
      this.wddLocation.SelectedValue = objParam.i_LocationId.ToString();
      string[] source1 = objParam.v_Requester.Split('|');
      this.txtName.Text = ((IEnumerable<string>) source1).Count<string>() > 0 ? source1[0] : "";
      this.txtLastName.Text = ((IEnumerable<string>) source1).Count<string>() > 1 ? source1[1] : "";
      this.txtAddress.Text = ((IEnumerable<string>) source1).Count<string>() > 2 ? source1[2] : "";
      this.wddDocumentType.SelectedValue = ((IEnumerable<string>) source1).Count<string>() > 3 ? source1[3] : "";
      this.txtDocumentNumber.Text = ((IEnumerable<string>) source1).Count<string>() > 4 ? source1[4] : "";
      this.txtTelephoneNumber.Text = ((IEnumerable<string>) source1).Count<string>() > 5 ? source1[5] : "";
      this.txtEmail.Text = ((IEnumerable<string>) source1).Count<string>() > 6 ? source1[6] : "";
      this.chkSendEmail.Checked = this.txtEmail.Text.Length == 0;
      this.chkMinority.Checked = ((IEnumerable<string>) source1).Count<string>() > 7 && source1[7] == "1";
      if (this.chkMinority.Checked)
      {
        this.txtAttorneyName.Text = ((IEnumerable<string>) source1).Count<string>() > 8 ? source1[8] : "";
        this.txtAttorneyAddress.Text = ((IEnumerable<string>) source1).Count<string>() > 9 ? source1[9] : "";
        this.txtAttorneyPhoneNumber.Text = ((IEnumerable<string>) source1).Count<string>() > 10 ? source1[10] : "";
        this.txtAttorneyEmail.Text = ((IEnumerable<string>) source1).Count<string>() > 11 ? source1[11] : "";
      }
      string[] source2 = objParam.v_RequestValues.Split('|');
      string[] strArray = source2[3].Split('-');
      this.rbService.Checked = ((IEnumerable<string>) source2).Count<string>() > 0 && source2[0] == "1";
      this.rbProduct.Checked = ((IEnumerable<string>) source2).Count<string>() > 0 && source2[0] == "2";
      this.txtDescription.Text = ((IEnumerable<string>) source2).Count<string>() > 1 ? source2[1] : "";
      this.rbClaim.Checked = ((IEnumerable<string>) source2).Count<string>() > 2 && source2[2] == "1";
      this.rbComplaint.Checked = ((IEnumerable<string>) source2).Count<string>() > 2 && source2[2] == "2";
      this.txtPlate.Text = strArray.ToString().Trim().Count<char>() > 3 ? strArray[0].ToString().Trim() : "";
      this.txtComments.Text = strArray.ToString().Trim().Count<char>() > 3 ? strArray[1].ToString().Trim() : "";
      this.chkAccept.Checked = ((IEnumerable<string>) source2).Count<string>() > 4 && source2[4] == "1";
    }

    private RequirementClaim GetObject()
    {
      RequirementClaim requirementClaim = new RequirementClaim();
      requirementClaim.i_RequirementClaimId = 0;
      requirementClaim.i_ClaimTypeId = new int?(8);
      requirementClaim.v_ClaimDate = this.wdpRegisterDate.Value.ToString();
      requirementClaim.i_Status = new int?(1);
      requirementClaim.i_Priority = new int?(0);
      requirementClaim.v_Requester = this.GetDataCustomers();
      requirementClaim.i_LocationId = new int?(Convert.ToInt32(this.wddLocation.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
      requirementClaim.v_RequestValues = this.GetDataClaim();
      requirementClaim.i_AssignedUserId = this.AssignedUser(Convert.ToInt32(this.wddLocation.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
      requirementClaim.d_AssignedDate = new DateTime?(Convert.ToDateTime((object) this.wdpRegisterDate.Value, (IFormatProvider) CultureInfo.CurrentCulture));
      requirementClaim.v_Comments = "";
      if (this.UploadFile.PostedFile != null)
      {
        Stream inputStream = this.UploadFile.PostedFile.InputStream;
        int contentLength = this.UploadFile.PostedFile.ContentLength;
        string contentType = this.UploadFile.PostedFile.ContentType;
        byte[] buffer = new byte[contentLength];
        inputStream.Read(buffer, 0, contentLength);
        requirementClaim.g_Image = buffer;
      }
      return requirementClaim;
    }

    private int? AssignedUser(int i_locationid)
    {
      int num = 0;
      if (this.ViewState["dtLocation"] is DataTable dataTable)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (i_locationid == Convert.ToInt32(row["i_LocationId"], (IFormatProvider) CultureInfo.CurrentCulture))
          {
            num = Convert.IsDBNull(row["i_SystemUserId"]) ? 0 : Convert.ToInt32(row["i_SystemUserId"], (IFormatProvider) CultureInfo.CurrentCulture);
            break;
          }
        }
      }
      return new int?(num);
    }

    private bool ValidateParameters()
    {
      string pstrMessage = "";
      if (this.wddLocation.SelectedValue == "")
        pstrMessage = "Debe seleccionar el establecimiento a Reclamar";
      else if (this.wddDocumentType.SelectedValue == "-1")
        pstrMessage = "Debe ingresar el Tipo Documento del cliente";
      else if (this.txtDocumentNumber.Text.Length == 0)
        pstrMessage = "Debe ingresar el Número Documento del cliente";
      else if (this.txtEmail.Text.Length == 0)
        pstrMessage = "Debe ingresar el E-mail del cliente";
      else if (this.chkMinority.Checked)
      {
        if (this.txtAttorneyName.Text.Length == 0)
          pstrMessage = "Debe ingresar el nombre del Apoderado";
        else if (this.txtAttorneyAddress.Text.Length == 0)
          pstrMessage = "Debe ingresar la dirección del Apoderado";
        else if (this.txtAttorneyPhoneNumber.Text.Length == 0)
          pstrMessage = "Debe ingresar el telefono del Apoderado";
        else if (this.txtAttorneyEmail.Text.Length == 0)
          pstrMessage = "Debe ingresar el E-mail del Apoderado";
      }
      else if (!this.rbService.Checked && !this.rbProduct.Checked)
        pstrMessage = "Debe especificar si el Reclamo/Queja se refiere a un Producto o Servicio";
      else if (this.txtDescription.Text.Length == 0)
        pstrMessage = "Debe indicar el detalle del Producto o Servicio";
      else if (!this.rbClaim.Checked && !this.rbComplaint.Checked)
        pstrMessage = "Debe indicar si es un Reclamo o una Queja";
      else if (this.txtComments.Text.Length == 0)
        pstrMessage = "Debe indicar el detalle del reclamo";
      else if (!this.chkAccept.Checked)
        pstrMessage = "Debe marcar la selección de Conformidad de los hechos descritos en el reclamo";
      if (pstrMessage.Length > 0)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessageClaimBook, enmMessageType.Warning, pstrMessage);
        return false;
      }
      this.lblMessageClaimBook.Visible = false;
      return true;
    }

    private bool ValidarEmail(string text)
    {
      return Regex.IsMatch(text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
    }

    public string GetDataCustomers()
    {
      return "" + this.txtPlate.Text + "|" + this.txtName.Text + "|" + this.txtLastName.Text + "|" + this.txtAddress.Text + "|" + this.wddDocumentType.SelectedValue + "|" + this.txtDocumentNumber.Text + "|" + this.txtTelephoneNumber.Text + "|" + this.txtEmail.Text + "|" + (this.chkMinority.Checked ? "1" : "0") + "|" + this.txtAttorneyName.Text + "|" + this.txtAttorneyAddress.Text + "|" + this.txtAttorneyPhoneNumber.Text + "|" + this.txtAttorneyEmail.Text;
    }

    public string GetDataClaim()
    {
      return "" + (this.rbService.Checked ? "1" : "2") + "|" + this.txtDescription.Text + "|" + (this.rbClaim.Checked ? "1" : "2") + "|" + this.txtComments.Text + "|" + (this.chkAccept.Checked ? "1" : "0");
    }

    private void ClearControls()
    {
      this.wdpRegisterDate.Value = DateTime.Now;
      this.txtClaimCode.Text = "";
      this.wddLocation.SelectedValue = "-1";
      this.txtPlate.Text = "";
      this.txtName.Text = "";
      this.txtLastName.Text = "";
      this.txtAddress.Text = "";
      this.txtDocumentNumber.Text = "";
      this.txtTelephoneNumber.Text = "";
      this.txtEmail.Text = "";
      if (this.chkMinority.Checked)
      {
        this.txtAttorneyName.Text = "";
        this.txtAttorneyAddress.Text = "";
        this.txtAttorneyPhoneNumber.Text = "";
        this.txtAttorneyEmail.Text = "";
      }
      this.chkMinority.Checked = false;
      this.rbService.Checked = false;
      this.rbProduct.Checked = false;
      this.txtDescription.Text = "";
      this.rbClaim.Checked = false;
      this.rbComplaint.Checked = false;
      this.txtComments.Text = "";
      this.chkAccept.Checked = false;
    }

    public bool SendEmail(string pstrclaimcode, string pstrregisterdate, string pstruseremail)
    {
      Email email = new Email();
      DataTable dataTable1 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.SMTPServerConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string pstrSMTPServer = dataTable1.Rows[0]["v_Value"].ToString();
      int pintSMTPPort = int.Parse(dataTable1.Rows[1]["v_Value"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
      string pstrSMTPUserName = dataTable1.Rows[2]["v_Value"].ToString();
      string pstrSMTPPassword = dataTable1.Rows[3]["v_Value"].ToString();
      DataTable dataTable2 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.ClaimCustomEmailConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string pstrEmailSubject = dataTable2.Rows[0]["v_Value"].ToString();
      string pstrEmailBody = string.Format(dataTable2.Rows[1]["v_Value"].ToString(), (object) pstrclaimcode, (object) pstrregisterdate);
      string pstrEmailFrom = dataTable2.Rows[2]["v_Value"].ToString();
      return Email.SendEmail(pstrSMTPUserName, pstrSMTPPassword, pstrEmailSubject, pstrEmailBody, pstruseremail, pstrSMTPServer, pintSMTPPort, pstrEmailFrom, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
