// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Delivery.BookAssignDelivery
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.Requirement.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Delivery
{
  public class BookAssignDelivery : Page
  {
    private RequirementQueriesBL ObjRequirementQueriesBL;
    private DataTable dtListDeliveryPlate;
    public SystemUser objUserBE;
    protected UpdatePanel UpdatePanel1;
    protected DropDownList ddlCourier;
    protected Label lblMessageError;
    protected Button wibSearch;
    protected CheckBox chkSearch;
    protected HtmlTableCell tdAddIds;
    protected TextBox txtIdsTR;
    protected FilteredTextBoxExtender txtIdsTR_FilteredTextBoxExtender;
    protected Button wibSearchTR;
    protected Button wibAddIds;
    protected Label lblMessageTR;
    protected GridView wdgListPlate;
    protected Label lblCount;
    protected HtmlTableCell tdReadIds;
    protected TextBox txtIdsOrder;
    protected FilteredTextBoxExtender txtIdsOrder_FilteredTextBoxExtender;
    protected Button wibReadIds;
    protected Label lblMessageOrder;
    protected HtmlTableRow trComent;
    protected Fecha Fecha1;
    protected TextBox txtObs;
    protected Button wibAceptar;
    protected Button wibImprimir;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadDeliveryCourier();
      this.ViewState["ListProgramation"] = (object) "";
      this.chkSearch.Visible = false;
      this.tdAddIds.Visible = false;
      this.tdReadIds.Visible = false;
      this.trComent.Visible = false;
      this.Fecha1.Value = DateTime.Now.AddDays(1.0);
      this.ViewState["IdOrder"] = (object) 1;
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        this.lblMessage.Visible = false;
        this.lblMessageError.Visible = false;
        this.lblMessageTR.Visible = false;
        this.lblMessageOrder.Visible = false;
        if (this.ddlCourier.SelectedValue == "-1")
        {
          SIIV.Common.Resource.Message.SetMessage(this.lblMessageError, enmMessageType.Warning, "Seleccione un Courier");
        }
        else
        {
          if (this.chkSearch.Checked)
          {
            this.tdAddIds.Visible = false;
            this.chkSearch.Checked = false;
            this.txtIdsTR.Text = string.Empty;
          }
          this.dtListDeliveryPlate = new DataTable();
          int i_RequirementPlateId = -1;
          int int32 = Convert.ToInt32(this.ddlCourier.SelectedValue);
          this.ObjRequirementQueriesBL = new RequirementQueriesBL();
          this.dtListDeliveryPlate = this.ObjRequirementQueriesBL.DeliveryListGetByIdZone(int32, i_RequirementPlateId);
          this.ViewState["dtListDeliveryPlate"] = (object) this.dtListDeliveryPlate;
          if (this.dtListDeliveryPlate.Rows.Count > 0)
          {
            this.chkSearch.Visible = true;
            this.tdReadIds.Visible = true;
            this.trComent.Visible = true;
            this.txtIdsOrder.Focus();
            this.wibAceptar.Enabled = true;
          }
          else
          {
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
            this.HidePopup();
          }
          this.wdgListPlate.DataSource = (object) this.dtListDeliveryPlate;
          this.wdgListPlate.DataBind();
          this.lblCount.Text = SIIV.SystemParameter.BL.Constants.SEARCHRESULT_OK.Replace("XX", this.dtListDeliveryPlate.Rows.Count.ToString());
        }
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessageError, enmMessageType.Error, ex.Message);
      }
    }

    protected void chkSearch_CheckedChanged(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      this.lblMessageError.Visible = false;
      if (this.ddlCourier.SelectedValue == "-1")
      {
        this.chkSearch.Checked = false;
        SIIV.Common.Resource.Message.SetMessage(this.lblMessageError, enmMessageType.Warning, "Seleccione un mensajero");
      }
      else if (this.chkSearch.Checked)
        this.tdAddIds.Visible = true;
      else
        this.tdAddIds.Visible = false;
    }

    protected void wibSearchTR_Click(object sender, EventArgs e)
    {
      try
      {
        this.dtListDeliveryPlate = new DataTable();
        string empty = string.Empty;
        int int32_1;
        if (this.txtIdsTR.Text.Trim().Length != 10 && this.txtIdsTR.Text.Trim() != "")
        {
          if (this.txtIdsTR.Text.Trim().Length >= 5 && this.txtIdsTR.Text.Trim().Length <= 8)
          {
            int32_1 = Convert.ToInt32(this.txtIdsTR.Text.Trim());
          }
          else
          {
            this.txtIdsTR.Text = string.Empty;
            this.txtIdsTR.Focus();
            throw new HandledException(1, "Ingrese una solicitud válida");
          }
        }
        else
          int32_1 = Convert.ToInt32(this.txtIdsTR.Text.Trim().Substring(2));
        DateTime dateTime = this.Fecha1.Value;
        this.ObjRequirementQueriesBL = new RequirementQueriesBL();
        this.dtListDeliveryPlate = this.ObjRequirementQueriesBL.DeliveryListGetByIdZoneOne(int32_1);
        if (this.dtListDeliveryPlate.Rows.Count > 0)
        {
          int int32_2 = Convert.ToInt32(this.dtListDeliveryPlate.Rows[0]["i_Status"].ToString());
          bool boolean = Convert.ToBoolean(this.dtListDeliveryPlate.Rows[0]["b_AsignationStatus"].ToString());
          if (int32_2 == 1 || int32_2 == 3)
          {
            if (!boolean)
            {
              this.ViewState["dtListDeliveryPlateTR"] = (object) this.dtListDeliveryPlate;
              this.wibAddIds.Enabled = true;
              this.trComent.Visible = true;
              SIIV.Common.Resource.Message.SetMessage(this.lblMessageTR, enmMessageType.Success, "Solicitud encontrada correctamente");
            }
            else
              SIIV.Common.Resource.Message.SetMessage(this.lblMessageTR, enmMessageType.Warning, "La solicitud ya tiene un mensajero asignado");
          }
          else
            SIIV.Common.Resource.Message.SetMessage(this.lblMessageTR, enmMessageType.Warning, "La solicitud " + int32_1.ToString() + " no puede ser programada, su estado es " + this.dtListDeliveryPlate.Rows[0]["Estado"].ToString() + ".");
        }
        else
          SIIV.Common.Resource.Message.SetMessage(this.lblMessageTR, enmMessageType.Warning, "Ingrese una solicitud válida");
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, ex.Message);
      }
    }

    protected void wibAddIds_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable dataTable1 = new DataTable();
        this.dtListDeliveryPlate = new DataTable();
        if (this.ViewState["dtListDeliveryPlateTR"] == null || this.ViewState["dtListDeliveryPlate"] == null)
          return;
        DataTable dataTable2 = (DataTable) this.ViewState["dtListDeliveryPlateTR"];
        this.dtListDeliveryPlate = (DataTable) this.ViewState["dtListDeliveryPlate"];
        this.TableReadOnly(this.dtListDeliveryPlate);
        for (int index1 = 0; index1 < this.dtListDeliveryPlate.Rows.Count; ++index1)
        {
          for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
          {
            if (dataTable2.Rows[index2]["i_RequirementPlateId"].ToString() == this.dtListDeliveryPlate.Rows[index1]["i_RequirementPlateId"].ToString())
            {
              SIIV.Common.Resource.Message.SetMessage(this.lblMessageTR, enmMessageType.Warning, "La solicitud " + dataTable2.Rows[index2]["i_RequirementPlateId"].ToString() + " ya fue agregada");
              this.txtIdsTR.Text = string.Empty;
              this.txtIdsTR.Focus();
              return;
            }
          }
        }
        DataRow row = this.dtListDeliveryPlate.NewRow();
        row["i_RequirementProgramationId"] = (object) dataTable2.Rows[0]["i_RequirementProgramationId"].ToString();
        row["i_RequirementPlateId"] = (object) dataTable2.Rows[0]["i_RequirementPlateId"].ToString();
        row["Placa"] = (object) dataTable2.Rows[0]["Placa"].ToString();
        row["Zona"] = (object) dataTable2.Rows[0]["Zona"].ToString();
        row["Distrito"] = (object) dataTable2.Rows[0]["Distrito"].ToString();
        row["Horario"] = (object) dataTable2.Rows[0]["Horario"].ToString();
        row["i_Status"] = (object) dataTable2.Rows[0]["i_Status"].ToString();
        row["Estado"] = (object) dataTable2.Rows[0]["Estado"].ToString();
        row["EstadoRP"] = (object) dataTable2.Rows[0]["EstadoRP"].ToString();
        row["Chk"] = (object) false;
        row["NumOrder"] = (object) "99";
        this.dtListDeliveryPlate.Rows.Add(row);
        this.ViewState["dtListDeliveryPlate"] = (object) this.dtListDeliveryPlate;
        this.wdgListPlate.DataSource = (object) this.dtListDeliveryPlate;
        this.wdgListPlate.DataBind();
        this.lblCount.Text = SIIV.SystemParameter.BL.Constants.SEARCHRESULT_OK.Replace("XX", this.dtListDeliveryPlate.Rows.Count.ToString());
        this.txtIdsTR.Text = string.Empty;
        this.txtIdsTR.Focus();
        this.wibAddIds.Enabled = false;
        this.lblMessageTR.Visible = false;
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, ex.Message);
      }
    }

    protected void wibAceptar_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.ViewState["dtListDeliveryPlate"] == null)
          return;
        DataTable dataTable1 = new DataTable();
        this.wdgListPlate.DataSource = (object) (DataTable) this.ViewState["dtListDeliveryPlate"];
        this.wdgListPlate.Dispose();
        short num = 0;
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        List<RequirementAsignationProgramation> asignationProgramationList = new List<RequirementAsignationProgramation>();
        foreach (GridViewRow row in this.wdgListPlate.Rows)
        {
          CheckBox control = (CheckBox) row.FindControl("chkItem");
          if (control != null && control.Checked)
          {
            ++num;
            RequirementAsignationProgramation asignationProgramation = new RequirementAsignationProgramation();
            asignationProgramation.i_RequirementProgramationId = Convert.ToInt32(this.wdgListPlate.DataKeys[row.RowIndex]["i_RequirementProgramationId"].ToString());
            asignationProgramation.v_PlateNew = row.Cells[3].Text.ToString();
            asignationProgramation.v_District = row.Cells[5].Text.ToString();
            asignationProgramation.i_CourierReference = new int?(Convert.ToInt32(this.ddlCourier.SelectedValue));
            asignationProgramation.i_CourierReferenceDescription = this.ddlCourier.SelectedItem.Text;
            asignationProgramation.d_AsignationDate = DateTime.Now;
            asignationProgramation.v_ObservationA = this.txtObs.Text;
            asignationProgramation.i_InsertUserId = new int?(systemUser.i_SystemUserId);
            asignationProgramation.v_CompleteName = this.wdgListPlate.DataKeys[row.RowIndex]["Propietario"].ToString();
            string str1 = this.wdgListPlate.DataKeys[row.RowIndex]["Direccion"].ToString();
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            string[] strArray1 = str1.Split('|');
            if (strArray1.Length == 1)
            {
              if (string.IsNullOrEmpty(strArray1[0].ToString()))
                strArray1[0] = "";
              else
                empty1 = strArray1[0].ToString();
            }
            if (strArray1.Length == 2)
            {
              if (string.IsNullOrEmpty(strArray1[0].ToString()))
                strArray1[0] = "";
              else
                empty1 = strArray1[0].ToString();
              if (string.IsNullOrEmpty(strArray1[1].ToString()))
                strArray1[1] = "";
              else
                empty2 = strArray1[1].ToString();
            }
            string str2 = this.wdgListPlate.DataKeys[row.RowIndex]["Telefono"].ToString();
            string empty3 = string.Empty;
            string empty4 = string.Empty;
            string[] strArray2 = str2.Split('|');
            if (strArray2.Length == 1)
            {
              if (string.IsNullOrEmpty(strArray2[0].ToString()))
                strArray2[0] = "";
              else
                empty3 = strArray2[0].ToString();
            }
            if (strArray2.Length == 2)
            {
              if (string.IsNullOrEmpty(strArray2[0].ToString()))
                strArray2[0] = "";
              else
                empty3 = strArray2[0].ToString();
              if (strArray2[1] == "___-___-___")
                strArray2[1] = "";
              else
                empty4 = strArray2[1].ToString();
            }
            string str3 = "";
            if (empty3 != "" && empty4 != "")
              str3 = empty3 + "/" + empty4;
            else if (empty3 != "" && empty4 == "")
              str3 = empty3;
            else if (empty3 == "" && empty4 != "")
              str3 = empty4;
            asignationProgramation.v_AddressLocation = empty1;
            asignationProgramation.v_Reference = empty2;
            asignationProgramation.v_Email = this.wdgListPlate.DataKeys[row.RowIndex]["Correo"].ToString();
            asignationProgramation.v_PhoneNumber = str3;
            asignationProgramation.v_PlateOld = this.wdgListPlate.DataKeys[row.RowIndex]["PlacaAntigua"].ToString();
            asignationProgramation.i_Order = (int) Convert.ToInt16(row.Cells[13].Text.ToString());
            asignationProgramationList.Add(asignationProgramation);
          }
        }
        DataTable dataTable2 = new DataTable();
        this.ViewState["ListProgramation"] = (object) asignationProgramationList.GetDataTableFromClass<RequirementAsignationProgramation>();
        if (num > (short) 0)
        {
          if (new RequirementManagementBL().AsignationProgramationInsert(asignationProgramationList))
          {
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Success, "Asignación realizada correctamente, cantidad de Placas Asignadas: " + num.ToString() + ".");
            int i_RequirementPlateId = -1;
            this.chkSearch.Visible = false;
            this.tdAddIds.Visible = false;
            this.tdReadIds.Visible = false;
            this.trComent.Visible = false;
            this.wibAceptar.Enabled = false;
            this.wibImprimir.Enabled = true;
            int int32 = Convert.ToInt32(this.ddlCourier.SelectedValue);
            this.ObjRequirementQueriesBL = new RequirementQueriesBL();
            DataTable byIdZone = this.ObjRequirementQueriesBL.DeliveryListGetByIdZone(int32, i_RequirementPlateId);
            this.wdgListPlate.DataSource = (object) byIdZone;
            this.wdgListPlate.DataBind();
            this.lblCount.Text = SIIV.SystemParameter.BL.Constants.SEARCHRESULT_OK.Replace("XX", byIdZone.Rows.Count.ToString());
            if (byIdZone.Rows.Count == 0)
            {
              this.chkSearch.Visible = false;
              this.tdAddIds.Visible = false;
              this.tdReadIds.Visible = false;
              this.trComent.Visible = false;
              this.wibAceptar.Enabled = false;
              this.wibImprimir.Enabled = false;
            }
            this.txtObs.Text = string.Empty;
          }
        }
        else
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Seleccionar por lo menos una placa de la lista");
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
    }

    protected void wibImprimir_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable dataTable = new DataTable();
        DataView defaultView = ((DataTable) this.ViewState["ListProgramation"]).DefaultView;
        defaultView.Sort = "i_Order asc";
        DataTable table = defaultView.ToTable();
        ReportDocument reportDocument = new ReportDocument();
        string filename = this.Server.MapPath("/") + "Delivery/Reports/ReportBookAssignDelivery.rpt";
        reportDocument.Load(filename);
        reportDocument.SetDataSource(table);
        reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "ReporteAsignacionPlaca");
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, ex.Message);
      }
    }

    protected void wibReadIds_Click(object sender, EventArgs e)
    {
      this.lblMessageOrder.Visible = false;
      string empty = string.Empty;
      string str;
      if (this.txtIdsOrder.Text.Trim().Length != 10 && this.txtIdsOrder.Text.Trim() != "")
      {
        if (this.txtIdsOrder.Text.Trim().Length >= 5 && this.txtIdsOrder.Text.Trim().Length <= 8)
        {
          str = this.txtIdsOrder.Text.Trim();
        }
        else
        {
          this.txtIdsOrder.Text = string.Empty;
          this.txtIdsOrder.Focus();
          throw new HandledException(1, "Ingrese una solicitud válida");
        }
      }
      else
        str = this.txtIdsOrder.Text.Trim().Substring(2);
      if (this.ViewState["dtListDeliveryPlate"] == null)
        return;
      DataTable dataTable = new DataTable();
      DataTable dtListDeliveryPlate = (DataTable) this.ViewState["dtListDeliveryPlate"];
      if (dtListDeliveryPlate.Rows.Count > 0)
      {
        for (int index = 0; index < dtListDeliveryPlate.Rows.Count; ++index)
        {
          if (Convert.ToInt32(dtListDeliveryPlate.Rows[index]["i_RequirementPlateId"].ToString()) == Convert.ToInt32(str))
          {
            if (!Convert.ToBoolean(dtListDeliveryPlate.Rows[index]["Chk"].ToString()))
            {
              this.TableReadOnly(dtListDeliveryPlate);
              dtListDeliveryPlate.Rows[index]["Chk"] = (object) true;
              dtListDeliveryPlate.Rows[index]["NumOrder"] = (object) Convert.ToInt16(this.ViewState["IdOrder"].ToString());
              this.ViewState["IdOrder"] = (object) ((int) Convert.ToInt16(this.ViewState["IdOrder"].ToString()) + 1);
              dtListDeliveryPlate.DefaultView.Sort = "NumOrder ASC";
              this.ViewState["dtListDeliveryPlate"] = (object) dtListDeliveryPlate;
              this.wdgListPlate.DataSource = (object) dtListDeliveryPlate;
              this.wdgListPlate.DataBind();
            }
            else
            {
              this.txtIdsOrder.Text = string.Empty;
              this.txtIdsOrder.Focus();
              SIIV.Common.Resource.Message.SetMessage(this.lblMessageOrder, enmMessageType.Warning, "La placa " + dtListDeliveryPlate.Rows[index]["Placa"].ToString() + " ya se agregó a la lista");
              return;
            }
          }
        }
        this.txtIdsOrder.Text = string.Empty;
        this.txtIdsOrder.Focus();
      }
      else
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No existe ningun registro en la lista");
    }

    protected void ddlCourier_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void LoadDeliveryCourier()
    {
      try
      {
        this.objUserBE = this.Session["SystemUser"] as SystemUser;
        int iLocationId = this.objUserBE.i_LocationId;
        this.ObjRequirementQueriesBL = new RequirementQueriesBL();
        DataTable deliveryCouriers = this.ObjRequirementQueriesBL.GetDeliveryCouriers(iLocationId);
        if (deliveryCouriers != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) deliveryCouriers.Rows)
            this.ddlCourier.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        }
        this.ddlCourier.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --", "-1"));
        this.ddlCourier.SelectedValue = "-1";
      }
      catch (Exception ex)
      {
        throw new Exception("OCURRIÓ UN ERROR AL INTENTAR CARGAR LOS COURIERS.");
      }
    }

    protected void TableReadOnly(DataTable dtListDeliveryPlate)
    {
      foreach (DataColumn column in (InternalDataCollectionBase) dtListDeliveryPlate.Columns)
        column.ReadOnly = false;
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
