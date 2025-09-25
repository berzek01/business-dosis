// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.BatchReceptionItem
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using SIIV.WebApp.Claim;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class BatchReceptionItem : Page
  {
    private clsReceptionPlateList clsBatchReceptionDetail;
    protected UpdatePanel UpdatePanel1;
    protected TextBox TxtIdLote;
    protected TextBox TxtIdDespacho;
    protected TextBox TxtFechaRecepcion;
    protected TextBox TxtFechaDespacho;
    protected TextBox TxtEstadoLote;
    protected TextBox TxtEstadoRecepcion;
    protected TextBox TxtItemsCaja;
    protected TextBox TxtItemsRecepcion;
    protected GridView wdgBatchReceptionDetail;
    protected HiddenField hfGridView1SV;
    protected HiddenField hfGridView1SH;
    protected Panel pnVerificador;
    protected TextBox TxtVerificador;
    protected FilteredTextBoxExtender TxtVerificador_FilteredTextBoxExtender;
    protected Button btnVerificar;
    protected Label lblLeyendaInferior;
    protected Label lblLeyendaInferiorCantidad;
    protected HtmlTableRow trMaintenance1;
    protected Button wibAceptarLote;
    protected Button wibCancelarRecepcion;
    protected Button wibClainBatchComplete;
    protected Button wibExportar;
    protected HtmlTableRow trClainUnit;
    protected Button wibClain;
    protected Panel pnDatosAdicionales;
    protected TextBox TxtObservacion;
    protected HtmlTableRow trManagementButtons;
    protected Button wibAceptarPopup;
    protected Button wibCancelarPopup;
    protected Button btnRefreshParent;
    protected Label lblMsgError;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMsgError.Visible = false;
      if (!this.Page.IsPostBack)
      {
        try
        {
          this.ViewState["i_PlateTypeId"] = (object) this.Request.QueryString["i_PlateTypeId"].ToString();
          this.ShowReceptionBatchInfo();
          this.SearchReceptionBatchDetail(int.Parse(this.TxtIdLote.Text, (IFormatProvider) CultureInfo.CurrentCulture), -1);
        }
        catch (HandledException ex)
        {
          Message.SetMessage(this.lblMsgError, ex);
        }
        catch (Exception ex)
        {
          Message.SetMessage(this.lblMsgError, new HandledException(-100, ex));
        }
      }
      this.TxtVerificador.Focus();
    }

    protected void wdgBatchReceptionDetail_InitializeRow(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowIndex < 0)
          return;
        int num = 8;
        for (int index = 2; index < num; ++index)
          e.Row.Cells[index].CssClass = "Test2";
        if (e.Row.Cells[1].Text.ToString() == "1" || e.Row.Cells[1].Text.ToString() == "3")
        {
          for (int index = 2; index < num; ++index)
            e.Row.Cells[index].CssClass = "Test";
        }
        if (e.Row.Cells[0].Text.ToString() == "2")
        {
          for (int index = 2; index < num; ++index)
            e.Row.Cells[index].CssClass = "Test3";
        }
        if (!(e.Row.Cells[0].Text.ToString() == "5"))
          return;
        for (int index = 2; index < num; ++index)
          e.Row.Cells[index].CssClass = "Test1";
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgError, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgError, new HandledException(-100, ex));
      }
    }

    protected void wdgBatchReceptionDetail_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      if (!(e.CommandName == "lnkClain"))
        return;
      try
      {
        int int32 = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = this.wdgBatchReceptionDetail.Rows[int32];
        if (row == null)
          throw new HandledException(4, "Error de Selección", "Error de seleccion 'wdgBatchReceptionDetail' en 'BatkReceptionList.aspx'");
        if (row.Cells[0].Text == "1")
          throw new HandledException(1, "Sobre Verificado.");
        if (row.Cells[0].Text == "2")
          throw new HandledException(1, "Sobre entregado.");
        if (row.Cells[0].Text == "5")
          throw new HandledException(1, "Sobre en reclamo.");
        this.trClainUnit.Visible = false;
        string text1 = row.Cells[2].Text;
        this.ViewState["strRequirementPlateId"] = (object) text1;
        string text2 = row.Cells[4].Text;
        string text3 = row.Cells[3].Text;
        string text4 = this.TxtIdDespacho.Text;
        string text5 = this.TxtIdLote.Text;
        string pstrProcessTypeId = this.wdgBatchReceptionDetail.DataKeys[int32]["i_ProcessTypeId"].ToString();
        string text6 = row.Cells[5].Text;
        string pstrProductCurrent = this.wdgBatchReceptionDetail.DataKeys[int32]["i_ProductId"].ToString();
        this.CreatePopUp("Reclamo por Producto Inconforme", new ClaimGenerator().GenerateClaim_ProductNoAgree(text1, text3, pstrProcessTypeId, pstrProductCurrent, "", this.wdgBatchReceptionDetail.DataKeys[int32]["v_FirstName"].ToString(), this.wdgBatchReceptionDetail.DataKeys[int32]["v_LastName"].ToString(), this.wdgBatchReceptionDetail.DataKeys[int32]["i_DocumentTypeId"].ToString(), this.wdgBatchReceptionDetail.DataKeys[int32]["v_DocumentNumber"].ToString(), string.Empty, this.wdgBatchReceptionDetail.DataKeys[int32]["v_PhoneNumber"].ToString(), 0), "755px", "550px");
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgError, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgError, new HandledException(-100, ex));
      }
    }

    protected void wibAceptarLote_Click(object sender, EventArgs e) => this.AcceptBatch();

    protected void wibRechazarLote_Click(object sender, EventArgs e) => this.RejectBatch();

    protected void wibCancelarRecepcion_Click(object sender, EventArgs e)
    {
      this.TxtVerificador.Text = "";
      this.TxtVerificador.Focus();
      this.Response.Redirect("BatchReceptionList.aspx?t=" + this.ViewState["i_PlateTypeId"].ToString());
    }

    protected void wibClainBatchComplete_Click(object sender, EventArgs e)
    {
      bool flag = false;
      try
      {
        if ((this.Session["sBatch"] as Batch).i_Status.GetValueOrDefault() == 5)
          throw new HandledException(1, "Lote Actual ya se encuentra en Reclamo.");
        foreach (GridViewRow row in this.wdgBatchReceptionDetail.Rows)
        {
          if (Convert.ToInt32(row.Cells[1].Text, (IFormatProvider) CultureInfo.CurrentCulture) == 1 || Convert.ToInt32(row.Cells[1].Text, (IFormatProvider) CultureInfo.CurrentCulture) == 3)
          {
            flag = true;
            break;
          }
        }
        if (flag)
          throw new HandledException(1, "No se puede generar el reclamo del lote, hay registros validados ó en reclamo.");
        this.CreatePopUp("Reclamo por Lote No conforme", new ClaimGenerator().GenerateClaim_BatchNoAgree(this.TxtIdDespacho.Text, this.TxtIdLote.Text, 11.ToString((IFormatProvider) CultureInfo.CurrentCulture)), "755px", "380px");
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgError, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgError, new HandledException(-100, ex));
      }
    }

    protected void wibExportar_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable receptionDetailBy = new BatchReceptionQueriesBL().GetBatchReceptionDetailBy(Convert.ToInt32(this.TxtIdLote.Text), -1);
        ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
        exportToExcelDataGrid.clsTitle = new List<ClassColumns>()
        {
          new ClassColumns("v_CompleteName", 1, 180, "Nombre"),
          new ClassColumns("v_PlateNew", 1, 100, "N° Placa"),
          new ClassColumns("v_ProductName", 1, 220, "Producto"),
          new ClassColumns("v_TypeProcessed", 1, 200, "Tipo de Proceso"),
          new ClassColumns("v_RequirementType", 1, 150, "Tipo Solicitud"),
          new ClassColumns("v_RequirementStatus", 1, 200, "Estado Solicitud"),
          new ClassColumns("i_BatchId", 1, 100, "Lote"),
          new ClassColumns("v_VehicleTypeUse", 1, 200, "Tipo de Uso")
        };
        exportToExcelDataGrid.AgregarHojaLibro(receptionDetailBy, "Hoja", "Reporte de Lote");
        exportToExcelDataGrid.CerrarLibro();
        byte[] buffer = exportToExcelDataGrid.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=ReporteLotes.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgError, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgError, new HandledException(-100, ex));
      }
    }

    protected void wibClain_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable dataTable = new DataTable();
        string pstrRequirementPlateId = this.ViewState["NotFoundIds"].ToString();
        Convert.ToInt32(this.TxtIdLote.Text, (IFormatProvider) CultureInfo.CurrentCulture);
        DataTable receptionRequirementBy = new BatchReceptionQueriesBL().GetBatchReceptionRequirementBy(Convert.ToInt32(pstrRequirementPlateId, (IFormatProvider) CultureInfo.CurrentCulture));
        if (receptionRequirementBy == null || receptionRequirementBy.Rows.Count == 0)
          throw new HandledException(1, "No se encontraron los datos de la solicitud para generar un reclamo por excedente");
        string str = receptionRequirementBy.Rows[0]["i_ProductId"].ToString();
        string pstrPlateNumber = receptionRequirementBy.Rows[0]["v_PlateNew"].ToString();
        string pstrProcessTypeId = receptionRequirementBy.Rows[0]["i_ProcessTypeId"].ToString();
        string pstrRequesterFirstName = receptionRequirementBy.Rows[0]["v_FirstName"].ToString();
        string pstrRequesterLastName = receptionRequirementBy.Rows[0]["v_LastName"].ToString();
        string pstrRequesterDocumentTypeId = receptionRequirementBy.Rows[0]["i_DocumentTypeId"].ToString();
        string pstrRequesterDocumentNumber = receptionRequirementBy.Rows[0]["v_DocumentNumber"].ToString();
        string pstrRequesterTelephone = receptionRequirementBy.Rows[0]["v_PhoneNumber"].ToString();
        this.CreatePopUp("Reclamo por Sobre Inexistente", new ClaimGenerator().GenerateClaim_ProductNoAgree(pstrRequirementPlateId, pstrPlateNumber, pstrProcessTypeId, str, str, pstrRequesterFirstName, pstrRequesterLastName, pstrRequesterDocumentTypeId, pstrRequesterDocumentNumber, string.Empty, pstrRequesterTelephone, 1), "755px", "550px");
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgError, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgError, new HandledException(-100, ex));
      }
    }

    protected void wibAceptarPopup_Click(object sender, EventArgs e) => this.AcceptPopupVerify();

    protected void wibCancelarPopup_Click(object sender, EventArgs e) => this.CancelPopupVerify();

    protected void btnVerificar_Click(object sender, EventArgs e) => this.ChekSobre();

    protected void btnRefreshParent_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.Session["PopupReturn"] == null)
          return;
        int int32 = Convert.ToInt32(this.Session["PopupReturn"]);
        if (int32 == 0)
          return;
        this.ShowReceptionBatchInfo();
        this.SearchReceptionBatchDetail(int.Parse(this.TxtIdLote.Text, (IFormatProvider) CultureInfo.CurrentCulture), -1);
        this.clsBatchReceptionDetail = this.ViewState["vsdtBatchReceptionDetail"] as clsReceptionPlateList;
        string str = "";
        if (int32 == 1)
          str = this.ViewState["strRequirementPlateId"] != null ? this.ViewState["strRequirementPlateId"].ToString() : throw new HandledException(3, "Error de Sesión", "Error de Sesion 'strRequirementPlateId' en BatchReceptionItem");
        if (int32 == 2)
        {
          this.wibAceptarLote.Enabled = false;
          this.wibClainBatchComplete.Enabled = false;
        }
        foreach (clsReceptionPlate element in this.clsBatchReceptionDetail.Elements)
        {
          if (int32 == 1 && element.i_RequirementPlateId.ToString() == str)
          {
            int iIsPistol = element.i_IsPistol;
            if (element.i_Status == 11)
              throw new HandledException(1, "Este Kit se encuentra actualmente en reclamo.");
            if (element.i_Status != 6)
            {
              if (iIsPistol != 1)
              {
                element.i_IsPistol = 3;
                element.i_StatusSobreId = 5;
                element.v_RequirementStatus = "En Reclamo";
                break;
              }
              break;
            }
          }
          if (int32 == 2)
          {
            element.i_IsPistol = 3;
            element.i_StatusSobreId = 5;
            element.v_RequirementStatus = "En Reclamo";
          }
        }
        int num = 0;
        foreach (clsReceptionPlate element in this.clsBatchReceptionDetail.Elements)
        {
          if (element.i_IsPistol == 3 && (element.i_StatusSobreId == 4 || element.i_StatusSobreId == 5))
            ++num;
        }
        this.lblLeyendaInferior.Text = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.lblLeyendaInferiorCantidad.Text = " / " + this.clsBatchReceptionDetail.Elements.Count.ToString();
        this.wdgBatchReceptionDetail.DataSource = (object) this.clsBatchReceptionDetail.Elements;
        this.wdgBatchReceptionDetail.DataBind();
        this.ViewState["vsdtBatchReceptionDetail"] = (object) this.clsBatchReceptionDetail;
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgError, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgError, new HandledException(-100, ex));
      }
      finally
      {
        this.TxtVerificador.Text = "";
        this.TxtVerificador.Focus();
      }
    }

    private void ShowReceptionBatchInfo()
    {
      try
      {
        Batch batch = this.Session["sBatch"] != null ? this.Session["sBatch"] as Batch : throw new HandledException(3, "Error de Sesión", "Error de Sesion 'sBatch' en BatchReceptionItem");
        this.TxtIdLote.Text = batch.i_BatchId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        TextBox txtFechaRecepcion = this.TxtFechaRecepcion;
        DateTime? nullable = batch.d_ReceptionDate;
        string str1 = !nullable.HasValue ? string.Empty : Convert.ToDateTime((object) batch.d_ReceptionDate, (IFormatProvider) CultureInfo.CurrentCulture).ToShortDateString();
        txtFechaRecepcion.Text = str1;
        this.TxtEstadoLote.Text = this.Page.Server.HtmlDecode(batch.v_BatchStatus);
        this.TxtItemsCaja.Text = batch.v_ItemsMax;
        this.TxtIdDespacho.Text = batch.i_DispatchId.ToString();
        TextBox txtFechaDespacho = this.TxtFechaDespacho;
        nullable = batch.d_EntryToDispatch;
        string str2 = !nullable.HasValue ? string.Empty : Convert.ToDateTime((object) batch.d_EntryToDispatch, (IFormatProvider) CultureInfo.CurrentCulture).ToShortDateString();
        txtFechaDespacho.Text = str2;
        this.TxtEstadoRecepcion.Text = this.Page.Server.HtmlDecode(batch.v_ReceptionStatus);
        this.TxtItemsRecepcion.Text = batch.v_ChekItems;
        int? iReceptionStatus = batch.i_ReceptionStatus;
        int num;
        if (iReceptionStatus.GetValueOrDefault() != 2)
        {
          iReceptionStatus = batch.i_ReceptionStatus;
          num = iReceptionStatus.GetValueOrDefault() == 2 ? 1 : 0;
        }
        else
          num = 1;
        if (num == 0)
          return;
        this.wibClainBatchComplete.Enabled = false;
        this.wibAceptarLote.Enabled = false;
        this.TxtVerificador.Enabled = false;
        this.btnVerificar.Enabled = false;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchReceptionBatchDetail(int pintBatchId, int pintStatusSobreId)
    {
      DataTable dataTable = new DataTable();
      try
      {
        this.clsBatchReceptionDetail = this.ViewState["vsdtBatchReceptionDetail"] != null ? (clsReceptionPlateList) this.ViewState["vsdtBatchReceptionDetail"] : new clsReceptionPlateList(new BatchReceptionQueriesBL().GetBatchReceptionDetailBy(pintBatchId, pintStatusSobreId));
        this.wdgBatchReceptionDetail.DataSource = (object) this.clsBatchReceptionDetail.Elements;
        this.wdgBatchReceptionDetail.DataBind();
        int num = 0;
        foreach (clsReceptionPlate element in this.clsBatchReceptionDetail.Elements)
        {
          if (element.i_IsPistol == 1 || element.i_IsPistol == 3)
            ++num;
        }
        this.ViewState["vsdtBatchReceptionDetail"] = (object) this.clsBatchReceptionDetail;
        this.lblLeyendaInferior.Text = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.lblLeyendaInferiorCantidad.Text = " / " + this.clsBatchReceptionDetail.Elements.Count.ToString();
      }
      catch (Exception ex)
      {
        throw ex;
      }
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

    private void AcceptBatch()
    {
      try
      {
        this.clsBatchReceptionDetail = this.ViewState["vsdtBatchReceptionDetail"] != null ? (clsReceptionPlateList) this.ViewState["vsdtBatchReceptionDetail"] : throw new HandledException(3, "Error de Sesión", "Error de Sesion 'vsdtBatchReceptionDetail' en 'BatchReceptionItem.aspx'");
        if (this.clsBatchReceptionDetail == null)
          return;
        if (this.clsBatchReceptionDetail.Elements.Count != int.Parse(this.lblLeyendaInferior.Text, (IFormatProvider) CultureInfo.CurrentCulture))
          throw new HandledException(1, "Debe verificar todos los sobres para aceptar el lote");
        this.pnDatosAdicionales.Visible = true;
        this.pnVerificador.Visible = false;
        this.TxtObservacion.Text = "";
        this.trMaintenance1.Visible = false;
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgError, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgError, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void RejectBatch()
    {
      try
      {
        clsReceptionPlateList receptionPlateList = this.ViewState["vsdtBatchReceptionDetail"] as clsReceptionPlateList;
        foreach (clsReceptionPlate element in receptionPlateList.Elements)
        {
          if (element.i_Status == 6)
            throw new HandledException(1, "No se puede Rechazar el lote, tiene placas entregadas. ");
        }
        foreach (clsReceptionPlate element in receptionPlateList.Elements)
          element.i_StatusSobreId = 3;
        this.lblLeyendaInferior.Text = "0";
        this.lblLeyendaInferiorCantidad.Text = "0";
        this.wdgBatchReceptionDetail.DataSource = (object) receptionPlateList.Elements;
        this.wdgBatchReceptionDetail.DataBind();
        this.ViewState["vsdtBatchReceptionDetail"] = (object) receptionPlateList;
        this.pnDatosAdicionales.Visible = true;
        this.pnVerificador.Visible = false;
        this.TxtObservacion.Text = "";
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgError, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgError, new HandledException(-100, ex));
      }
    }

    private void AcceptPopupVerify()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
        if (this.Session["SystemUser"] == null)
          throw new HandledException(3, "Error de Sesión", "Error de Sesion 'SystemUser' en 'BatchReceptionItem.aspx'");
        int iLocationId = systemUser.i_LocationId;
        Convert.ToInt32((object) systemUser.i_CompanyId, (IFormatProvider) CultureInfo.CurrentCulture);
        string vAlias = systemUser.v_Alias;
        int iSystemUserId = systemUser.i_SystemUserId;
        clsReceptionPlateList receptionPlateList = this.ViewState["vsdtBatchReceptionDetail"] as clsReceptionPlateList;
        int num = int.Parse(this.TxtIdLote.Text, (IFormatProvider) CultureInfo.CurrentCulture);
        int? nullable = new int?();
        if (int.Parse(this.lblLeyendaInferior.Text, (IFormatProvider) CultureInfo.CurrentCulture) == 0)
          nullable = new int?(3);
        else if (int.Parse(this.lblLeyendaInferior.Text, (IFormatProvider) CultureInfo.CurrentCulture) >= receptionPlateList.Elements.Count)
          nullable = new int?(2);
        int int32_1 = Convert.ToInt32(new WarehouseQueriesBL().GetWarehouseBy(0, "", iLocationId, 1).Rows[0]["i_WarehouseId"], (IFormatProvider) CultureInfo.CurrentCulture);
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
        {
          Timeout = new TimeSpan(0, 0, 5, 1)
        }))
        {
          DataTable dataTable1 = new DTStockMovementDetail().DataTableStockMovementDetail();
          DataTable dataTable2 = new DataTable();
          foreach (clsReceptionPlate element in receptionPlateList.Elements)
          {
            int int32_2 = Convert.ToInt32(element.i_RequirementPlateId);
            int iIsPistol = element.i_IsPistol;
            int iStatusSobreId = element.i_StatusSobreId;
            int vehicleTypeUseId = element.i_VehicleTypeUseId;
            switch (iIsPistol)
            {
              case 3:
                if (new BatchReceptionQueriesBL().VerifyReceptionPlate(iLocationId, Convert.ToInt32(this.TxtIdLote.Text, (IFormatProvider) CultureInfo.CurrentCulture), int32_2) == 0)
                {
                  DataRowCollection rows = dataTable1.Rows;
                  object[] objArray = new object[22];
                  objArray[3] = (object) element.i_ProductId;
                  objArray[6] = (object) 1;
                  objArray[7] = (object) element.v_ProductName;
                  objArray[8] = (object) element.i_RequirementPlateId;
                  objArray[20] = (object) element.v_PlateNew;
                  objArray[21] = (object) element.i_VehicleTypeUseId;
                  rows.Add(objArray);
                }
                if (iStatusSobreId == 5)
                {
                  new BatchReceptionManagementBL().BatchReceptionDetailUpdateState(int32_2, 5, iSystemUserId, 1);
                  break;
                }
                if (iStatusSobreId == 4)
                {
                  new BatchReceptionManagementBL().BatchReceptionDetailUpdateState(int32_2, 1, iSystemUserId, 1);
                  break;
                }
                break;
            }
          }
          if (dataTable1.Rows.Count > 0)
          {
            StockMovementManagementBL movementManagementBl = new StockMovementManagementBL();
            StockMovement pobjStockMovement = new StockMovement();
            pobjStockMovement.i_WarehouseId = new int?(int32_1);
            pobjStockMovement.i_MotiveMovementId = new int?(13);
            pobjStockMovement.i_UserId = new int?(iSystemUserId);
            pobjStockMovement.b_Checked = new bool?(false);
            pobjStockMovement.v_Observation = this.TxtObservacion.Text;
            pobjStockMovement.d_InsertDate = new DateTime?(DateTime.Now);
            pobjStockMovement.i_ProductionOrderId = new int?();
            DataTable pdtStockMovementDetail = dataTable1;
            dataTable2 = movementManagementBl.StockMovementInsertReceptionBatch(pobjStockMovement, pdtStockMovementDetail);
            new BatchReceptionManagementBL().BatchReceptionUpdateState(new Batch()
            {
              i_BatchId = num,
              i_ReceptionStatus = nullable,
              v_Observations = this.TxtObservacion.Text,
              i_CheckItems = new int?(Convert.ToInt32(this.lblLeyendaInferior.Text, (IFormatProvider) CultureInfo.CurrentCulture)),
              i_UserReceptionId = new int?(iSystemUserId)
            });
          }
          transactionScope.Complete();
        }
        this.wibAceptarPopup.Enabled = false;
        this.wdgBatchReceptionDetail.Enabled = false;
        Message.SetMessage(this.lblMsgError, new HandledException(2, "Operación concretada con éxito"));
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgError, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgError, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void CancelPopupVerify()
    {
      this.Response.Redirect("BatchReceptionList.aspx?t=" + this.ViewState["i_PlateTypeId"]?.ToString());
      this.HidePopup();
    }

    private void PopupClose()
    {
      string script = "PopupClose();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void ChekSobre()
    {
      try
      {
        string empty = string.Empty;
        string str;
        if (this.TxtVerificador.Text.Trim().Length != 10 && this.TxtVerificador.Text.Trim() != "")
        {
          if (this.TxtVerificador.Text.Trim().Length < 5 || this.TxtVerificador.Text.Trim().Length > 8)
            throw new HandledException(1, "Dato ingresado es incorrecto");
          str = this.TxtVerificador.Text.Trim();
        }
        else
          str = !(this.TxtVerificador.Text.Trim().Substring(2, 1) == "0") ? this.TxtVerificador.Text.Trim().Substring(2) : this.TxtVerificador.Text.Trim().Substring(3);
        bool flag = false;
        this.clsBatchReceptionDetail = this.ViewState["vsdtBatchReceptionDetail"] != null ? (clsReceptionPlateList) this.ViewState["vsdtBatchReceptionDetail"] : throw new HandledException(3, "Error de Sesión", "Error de Sesion 'vsdtBatchReceptionDetail' en 'BatchReceptionItem.aspx'");
        foreach (clsReceptionPlate element in this.clsBatchReceptionDetail.Elements)
        {
          if (element.i_RequirementPlateId.ToString() == str)
          {
            int iIsPistol = element.i_IsPistol;
            flag = true;
            if (element.i_Status == 11)
              throw new HandledException(1, "Este Kit se encuentra actualmente en reclamo.");
            if (element.i_Status == 6)
              throw new HandledException(1, "Este Kit se fue entregado");
            if (iIsPistol == 1 || iIsPistol == 3)
              throw new HandledException(1, "Este Kit ha sido seleccionado.");
            if (iIsPistol != 3)
            {
              this.lblLeyendaInferior.Text = (int.Parse(this.lblLeyendaInferior.Text, (IFormatProvider) CultureInfo.CurrentCulture) + 1).ToString((IFormatProvider) CultureInfo.CurrentCulture);
              element.i_IsPistol = 3;
              break;
            }
            break;
          }
        }
        this.wdgBatchReceptionDetail.DataSource = (object) this.clsBatchReceptionDetail.Elements;
        this.wdgBatchReceptionDetail.DataBind();
        this.ViewState["vsdtBatchReceptionDetail"] = (object) this.clsBatchReceptionDetail;
        if (this.clsBatchReceptionDetail.Elements.Count == int.Parse(this.lblLeyendaInferior.Text, (IFormatProvider) CultureInfo.CurrentCulture))
          this.lblMsgError.Text = " ACEPTACIÓN TOTAL ";
        if (!flag)
        {
          this.ViewState["NotFoundIds"] = (object) str;
          this.trClainUnit.Visible = true;
          throw new HandledException(1, "El SOBRE " + str + " NO PERTENECE A ESTE LOTE.");
        }
        this.trClainUnit.Visible = false;
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsgError, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsgError, new HandledException(-100, ex));
      }
      finally
      {
        this.TxtVerificador.Text = "";
        this.TxtVerificador.Focus();
      }
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
