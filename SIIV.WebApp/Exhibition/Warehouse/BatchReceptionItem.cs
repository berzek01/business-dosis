// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Warehouse.BatchReceptionItem
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using SIIV.Warehouse.BL;
using SIIV.WebApp.Claim;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Warehouse
{
  public class BatchReceptionItem : Page
  {
    private int i_PlateTypeId = 0;
    protected UpdatePanel UpdatePanel1;
    protected TextBox TxtIdLote;
    protected TextBox TxtIdDespacho;
    protected TextBox TxtFechaRecepcion;
    protected TextBox TxtFechaDespacho;
    protected TextBox TxtEstadoLote;
    protected TextBox TxtEstadoRecepcion;
    protected TextBox TxtItemsCaja;
    protected TextBox TxtItemsRecepcion;
    protected Label lblLeyendaSuperior;
    protected Label lblLeyendaSuperiorCantidad;
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
    protected Button wibRechazarLote;
    protected Button wibCancelarRecepcion;
    protected Button wibClainBatchComplete;
    protected HtmlTableRow trClainUnit;
    protected Button wibClain;
    protected Panel pnDatosAdicionales;
    protected TextBox TxtObservacion;
    protected HtmlTableRow trManagementButtons;
    protected Button wibAceptarPopup;
    protected Button wibCancelarPopup;
    protected Button btnRefreshParent;
    protected Label lblMsgError;
    protected HtmlTableRow trwibFinalize;
    protected Button wibFinalize;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.TxtVerificador.Focus();
      this.lblMsgError.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.ViewState["i_PlateTypeId"] = (object) this.Request.QueryString["i_PlateTypeId"].ToString();
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.ShowReceptionBatchInfo();
        this.SearchReceptionBatchDetail(int.Parse(this.TxtIdLote.Text.TrimEnd()), -1);
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

    protected void wdgBatchReceptionDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowIndex < 0)
          return;
        string text1 = e.Row.Cells[7].Text;
        string text2 = e.Row.Cells[8].Text;
        for (int index = 0; index < 7; ++index)
          e.Row.Cells[index].CssClass = "Test2";
        if (text2 == "1" || text2 == "3")
        {
          for (int index = 0; index < 7; ++index)
            e.Row.Cells[index].CssClass = "Test";
        }
        if (text1 == "2")
        {
          for (int index = 0; index < 7; ++index)
            e.Row.Cells[index].CssClass = "Test3";
        }
        if (!(text1 == "5"))
          return;
        for (int index = 0; index < 7; ++index)
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
      try
      {
        if (!(e.CommandName == "lnkClain"))
          return;
        int int32 = Convert.ToInt32(e.CommandArgument);
        if (this.wdgBatchReceptionDetail.Rows[int32] == null)
          throw new HandledException(4, "Error de selección.", "'wdgBatchReceptionDetail' - BatchReceptionItem.aspx");
        if (this.wdgBatchReceptionDetail.DataKeys[int32]["i_StatusSobreId"].ToString() == "1" || this.wdgBatchReceptionDetail.DataKeys[int32]["i_StatusSobreId"].ToString() == "2" || this.wdgBatchReceptionDetail.DataKeys[int32]["i_StatusSobreId"].ToString() == "5")
          return;
        this.trClainUnit.Visible = false;
        this.lblMsgError.Visible = false;
        string pstrRequirementPlateId = this.wdgBatchReceptionDetail.DataKeys[int32]["i_RequirementPlateId"].ToString();
        string pstrPlateNumber = this.wdgBatchReceptionDetail.DataKeys[int32]["v_PlateNew"].ToString();
        string text1 = this.TxtIdDespacho.Text;
        string text2 = this.TxtIdLote.Text;
        string pstrProcessTypeId = this.wdgBatchReceptionDetail.DataKeys[int32]["i_ProcessTypeId"].ToString();
        this.wdgBatchReceptionDetail.DataKeys[int32]["v_ProductName"].ToString();
        string pstrProductCurrent = this.wdgBatchReceptionDetail.DataKeys[int32]["i_ProductId"].ToString();
        this.CreatePopUp("Reclamo por Producto Inconforme", new ClaimGenerator().GenerateClaim_ProductNoAgree(pstrRequirementPlateId, pstrPlateNumber, pstrProcessTypeId, pstrProductCurrent, "", this.wdgBatchReceptionDetail.DataKeys[int32]["v_FirstName"].ToString(), this.wdgBatchReceptionDetail.DataKeys[int32]["v_LastName"].ToString(), this.wdgBatchReceptionDetail.DataKeys[int32]["i_DocumentTypeId"].ToString(), this.wdgBatchReceptionDetail.DataKeys[int32]["v_DocumentNumber"].ToString(), string.Empty, this.wdgBatchReceptionDetail.DataKeys[int32]["v_PhoneNumber"].ToString(), 0), "755px", "550px");
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

    protected void btnVerificar_Click(object sender, EventArgs e) => this.ChekSobre();

    protected void wibAceptarLote_Click(object sender, EventArgs e) => this.AcceptBatch();

    protected void wibRechazarLote_Click(object sender, EventArgs e) => this.RejectBatch();

    protected void wibCancelarRecepcion_Click(object sender, EventArgs e)
    {
      this.TxtVerificador.Text = "";
      this.Response.Redirect("BatchReceptionList.aspx?t=" + this.ViewState["i_PlateTypeId"].ToString());
    }

    protected void wibClainBatchComplete_Click(object sender, EventArgs e)
    {
      try
      {
        bool flag = false;
        if (this.Session["sBatch"] == null)
          throw new HandledException(3, "La sesión ha expirado.", "'sBatch' - BatchReceptionItem.aspx");
        if ((this.Session["sBatch"] as Batch).i_Status.GetValueOrDefault() == 5)
          throw new HandledException(1, "Lote Actual ya se encuentra en Reclamo.");
        foreach (GridViewRow row in this.wdgBatchReceptionDetail.Rows)
        {
          if (Convert.ToInt32(this.wdgBatchReceptionDetail.DataKeys[row.RowIndex]["i_IsPistol"]) == 1)
            flag = true;
        }
        if (flag)
          throw new HandledException(1, "Lote Actual ya se encuentra en Reclamo.");
        this.CreatePopUp("Reclamo por Lote No conforme", new ClaimGenerator().GenerateClaim_BatchNoAgree(this.TxtIdDespacho.Text, this.TxtIdLote.Text, 11.ToString((IFormatProvider) CultureInfo.CurrentCulture)), "700px", "370px");
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

    protected void wibClain_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable dataTable = new DataTable();
        string pstrRequirementPlateId = this.ViewState["NotFoundIds"].ToString();
        Convert.ToInt32(this.TxtIdLote.Text, (IFormatProvider) CultureInfo.CurrentCulture);
        DataTable receptionRequirementBy = new BatchReceptionQueriesBL().GetBatchReceptionRequirementBy(Convert.ToInt32(pstrRequirementPlateId, (IFormatProvider) CultureInfo.CurrentCulture));
        if (receptionRequirementBy == null || receptionRequirementBy.Rows.Count == 0)
          throw new HandledException(1, "<br>&nbsp;&nbsp; Adventencia: <br> •&nbsp;nbsp;No se encontraron los datos de la solicitud para generar un reclamo por excedente");
        string str = receptionRequirementBy.Rows[0]["i_ProductId"].ToString();
        string pstrPlateNumber = receptionRequirementBy.Rows[0]["v_PlateNew"].ToString();
        string pstrProcessTypeId = receptionRequirementBy.Rows[0]["i_ProcessTypeId"].ToString();
        string pstrRequesterFirstName = receptionRequirementBy.Rows[0]["v_FirstName"].ToString();
        string pstrRequesterLastName = receptionRequirementBy.Rows[0]["v_LastName"].ToString();
        string pstrRequesterDocumentTypeId = receptionRequirementBy.Rows[0]["i_DocumentTypeId"].ToString();
        string pstrRequesterDocumentNumber = receptionRequirementBy.Rows[0]["v_DocumentNumber"].ToString();
        string pstrRequesterTelephone = receptionRequirementBy.Rows[0]["v_PhoneNumber"].ToString();
        this.CreatePopUp("Reclamo por Sobre Inexistente", new ClaimGenerator().GenerateClaim_ProductNoAgree(pstrRequirementPlateId, pstrPlateNumber, pstrProcessTypeId, str, str, pstrRequesterFirstName, pstrRequesterLastName, pstrRequesterDocumentTypeId, pstrRequesterDocumentNumber, string.Empty, pstrRequesterTelephone, 1), "755px", "670px");
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

    protected void btnRefreshParent_Click(object sender, EventArgs e)
    {
      try
      {
        this.SearchReceptionBatchDetail(int.Parse(this.TxtIdLote.Text, (IFormatProvider) CultureInfo.CurrentCulture), -1);
        DataTable dt = this.ViewState["vsdtBatchReceptionDetail"] as DataTable;
        dt.Columns["i_IsPistol"].ReadOnly = false;
        if (this.Session["sePistoleados"] == null)
          return;
        DataTable dataTable = this.Session["sePistoleados"] as DataTable;
        foreach (DataRow row1 in (InternalDataCollectionBase) dt.Rows)
        {
          foreach (DataRow row2 in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (Convert.ToInt32(row1["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(row2["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture) && Convert.ToInt32(row2["i_IsPistol"], (IFormatProvider) CultureInfo.CurrentCulture) == 3 && Convert.ToInt32(row2["i_StatusSobreId"], (IFormatProvider) CultureInfo.CurrentCulture) == 4)
              row1["i_IsPistol"] = (object) 3;
          }
        }
        int num = 0;
        foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
        {
          if (Convert.ToInt32(row["i_IsPistol"], (IFormatProvider) CultureInfo.CurrentCulture) == 3 && Convert.ToInt32(row["i_StatusSobreId"], (IFormatProvider) CultureInfo.CurrentCulture) == 4)
            ++num;
        }
        this.ViewState["vsdtBatchReceptionDetail"] = (object) dt;
        this.lblLeyendaSuperior.Text = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.lblLeyendaInferior.Text = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.lblLeyendaSuperiorCantidad.Text = "/" + dt.Rows.Count.ToString();
        this.lblLeyendaInferiorCantidad.Text = "/" + dt.Rows.Count.ToString();
        this.wdgBatchReceptionDetail.DataSource = (object) dt;
        this.wdgBatchReceptionDetail.DataBind();
        this.SetProductImagen(dt, 100, 30f, 30f);
        this.ViewState["vsdtBatchReceptionDetail"] = (object) dt;
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

    protected void wibFinalize_Click(object sender, EventArgs e)
    {
      this.HidePopup();
      this.Response.Redirect("BatchReceptionList.aspx?t=" + this.ViewState["i_PlateTypeId"].ToString());
    }

    private void ShowReceptionBatchInfo()
    {
      try
      {
        Batch batch = this.Session["sBatch"] != null ? this.Session["sBatch"] as Batch : throw new HandledException(3, "La sesión ha expirado.", "'sBatch' - BatchReceptionItem.aspx");
        this.TxtIdLote.Text = batch.i_BatchId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        TextBox txtFechaRecepcion = this.TxtFechaRecepcion;
        DateTime? nullable = batch.d_ReceptionDate;
        string str1 = !nullable.HasValue ? string.Empty : Convert.ToDateTime((object) batch.d_ReceptionDate, (IFormatProvider) CultureInfo.CurrentCulture).ToShortDateString();
        txtFechaRecepcion.Text = str1;
        this.TxtEstadoLote.Text = Convert.ToString(this.Page.Server.HtmlDecode(batch.v_BatchStatus));
        this.TxtItemsCaja.Text = batch.v_ItemsMax;
        this.TxtIdDespacho.Text = batch.i_DispatchId.ToString();
        TextBox txtFechaDespacho = this.TxtFechaDespacho;
        nullable = batch.d_EntryToDispatch;
        string str2 = !nullable.HasValue ? string.Empty : Convert.ToDateTime((object) batch.d_EntryToDispatch, (IFormatProvider) CultureInfo.CurrentCulture).ToShortDateString();
        txtFechaDespacho.Text = str2;
        this.TxtEstadoRecepcion.Text = Convert.ToString(this.Page.Server.HtmlDecode(batch.v_ReceptionStatus));
        this.TxtItemsRecepcion.Text = batch.v_ChekItems;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchReceptionBatchDetail(int pintBatchId, int pintStatusSobreId)
    {
      try
      {
        DataTable receptionDetailBy = new BatchReceptionQueriesBL().GetBatchReceptionDetailBy(pintBatchId, pintStatusSobreId);
        this.wdgBatchReceptionDetail.DataSource = (object) receptionDetailBy;
        this.wdgBatchReceptionDetail.DataBind();
        int num = 0;
        foreach (DataRow row in (InternalDataCollectionBase) receptionDetailBy.Rows)
        {
          if (Convert.ToInt32(row["i_IsPistol"], (IFormatProvider) CultureInfo.CurrentCulture) == 1 && Convert.ToInt32(row["i_StatusSobreId"], (IFormatProvider) CultureInfo.CurrentCulture) != 5)
            ++num;
        }
        this.ViewState["vsdtBatchReceptionDetail"] = (object) receptionDetailBy;
        this.lblLeyendaSuperior.Text = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.lblLeyendaInferior.Text = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        Label superiorCantidad = this.lblLeyendaSuperiorCantidad;
        int count = receptionDetailBy.Rows.Count;
        string str1 = "/" + count.ToString();
        superiorCantidad.Text = str1;
        Label inferiorCantidad = this.lblLeyendaInferiorCantidad;
        count = receptionDetailBy.Rows.Count;
        string str2 = "/" + count.ToString();
        inferiorCantidad.Text = str2;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void AcceptBatch()
    {
      try
      {
        if ((this.ViewState["vsdtBatchReceptionDetail"] as DataTable).Rows.Count == int.Parse(this.lblLeyendaSuperior.Text, (IFormatProvider) CultureInfo.CurrentCulture))
          this.lblMsgError.Text = "ACEPTACIÓN TOTAL";
        else if (int.Parse(this.lblLeyendaSuperior.Text, (IFormatProvider) CultureInfo.CurrentCulture) > 0)
          this.lblMsgError.Text = "ACEPTACIÓN PARCIAL";
        else if (Convert.ToInt32(this.lblLeyendaSuperior.Text, (IFormatProvider) CultureInfo.CurrentCulture) == 0)
          throw new HandledException(1, "Debe verificar al menos un sobre para Aceptar el Lote.");
        this.lblMsgError.Visible = false;
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
        DataTable dataTable = this.ViewState["vsdtBatchReceptionDetail"] as DataTable;
        dataTable.Columns["i_StatusSobreId"].ReadOnly = false;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (Convert.ToInt32(row["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == 6)
            return;
        }
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          row["i_StatusSobreId"] = (object) 3;
        this.lblLeyendaSuperior.Text = "0";
        this.lblLeyendaInferior.Text = "0";
        this.wdgBatchReceptionDetail.DataSource = (object) dataTable;
        this.wdgBatchReceptionDetail.DataBind();
        this.ViewState["vsdtBatchReceptionDetail"] = (object) dataTable;
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
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - BatchReceptionItem.aspx");
        int iLocationId = systemUser.i_LocationId;
        Convert.ToInt32((object) systemUser.i_CompanyId, (IFormatProvider) CultureInfo.CurrentCulture);
        string vAlias = systemUser.v_Alias;
        int iSystemUserId = systemUser.i_SystemUserId;
        DataTable dataTable1 = this.ViewState["vsdtBatchReceptionDetail"] as DataTable;
        int num1 = int.Parse(this.TxtIdLote.Text, (IFormatProvider) CultureInfo.CurrentCulture);
        int? nullable = new int?();
        if (int.Parse(this.lblLeyendaSuperior.Text, (IFormatProvider) CultureInfo.CurrentCulture) == 0)
          nullable = new int?(3);
        else if (int.Parse(this.lblLeyendaSuperior.Text, (IFormatProvider) CultureInfo.CurrentCulture) < dataTable1.Rows.Count)
          nullable = new int?(1);
        else if (int.Parse(this.lblLeyendaSuperior.Text, (IFormatProvider) CultureInfo.CurrentCulture) >= dataTable1.Rows.Count)
          nullable = new int?(2);
        int num2 = 59;
        int int32_1 = Convert.ToInt32(this.Request.QueryString["i_PlateTypeId"]);
        DataTable dataTable2 = new DataTable();
        int int32_2 = Convert.ToInt32(new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(int32_1).Rows[0]["i_WarehouseId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
        {
          Timeout = new TimeSpan(0, 0, 5, 1)
        }))
        {
          DataTable dataTable3 = new DTStockMovementDetail().DataTableStockMovementDetail();
          DataTable dataTable4 = new DataTable();
          foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
          {
            int int32_3 = Convert.ToInt32(row["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture);
            int int32_4 = Convert.ToInt32(row["i_IsPistol"], (IFormatProvider) CultureInfo.CurrentCulture);
            int int32_5 = Convert.ToInt32(row["i_StatusSobreId"], (IFormatProvider) CultureInfo.CurrentCulture);
            switch (int32_4)
            {
              case 3:
                if (new BatchReceptionQueriesBL().VerifyReceptionPlate(iLocationId, Convert.ToInt32(this.TxtIdLote.Text, (IFormatProvider) CultureInfo.CurrentCulture), int32_3) == 0)
                {
                  DataRowCollection rows = dataTable3.Rows;
                  object[] objArray = new object[22];
                  objArray[3] = row["i_ProductId"];
                  objArray[6] = (object) 1;
                  objArray[7] = row["v_ProductName"];
                  objArray[8] = row["i_RequirementPlateId"];
                  objArray[20] = row["v_PlateNew"];
                  objArray[21] = row["i_VehicleTypeUseId"];
                  rows.Add(objArray);
                }
                if (int32_5 == 5)
                {
                  new BatchReceptionManagementBL().BatchReceptionDetailUpdateState(int32_3, 5, iSystemUserId, 1);
                  break;
                }
                if (int32_5 == 4)
                {
                  new BatchReceptionManagementBL().BatchReceptionDetailUpdateState(int32_3, 1, iSystemUserId, 1);
                  break;
                }
                break;
            }
          }
          if (dataTable3.Rows.Count > 0)
          {
            StockMovementManagementBL movementManagementBl = new StockMovementManagementBL();
            StockMovement pobjStockMovement = new StockMovement();
            pobjStockMovement.i_WarehouseId = new int?(int32_2);
            pobjStockMovement.i_MotiveMovementId = new int?(num2);
            pobjStockMovement.i_SupplierId = new int?();
            pobjStockMovement.i_DocumentTypeId = new int?();
            pobjStockMovement.v_DocumentNumber = (string) null;
            pobjStockMovement.i_UserId = new int?(iSystemUserId);
            pobjStockMovement.b_Checked = new bool?(false);
            pobjStockMovement.v_Observation = this.TxtObservacion.Text;
            pobjStockMovement.d_InsertDate = new DateTime?(DateTime.Now);
            pobjStockMovement.i_ProductionOrderId = new int?();
            DataTable pdtStockMovementDetail = dataTable3;
            DataTable dataTable5 = movementManagementBl.ExhibitionStockMovementInsert(pobjStockMovement, pdtStockMovementDetail);
            if (dataTable5.Rows.Count > 0)
            {
              foreach (DataRow row in (InternalDataCollectionBase) dataTable5.Rows)
                new BatchReceptionManagementBL().ProductionOrderDetailUpdateWarehousePosition(Convert.ToInt32(row["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(row["i_LocationWarehouseId"], (IFormatProvider) CultureInfo.CurrentCulture));
              if (dataTable5.Rows[0]["i_LocationWarehouseId"] == DBNull.Value)
                throw new HandledException(1, "El almacén no tiene posiciones disponibles, no se puede almacenar.");
            }
            new BatchReceptionManagementBL().BatchReceptionUpdateState(new Batch()
            {
              i_BatchId = num1,
              i_ReceptionStatus = nullable,
              v_Observations = this.TxtObservacion.Text,
              i_CheckItems = new int?(Convert.ToInt32(this.lblLeyendaSuperior.Text, (IFormatProvider) CultureInfo.CurrentCulture)),
              i_UserReceptionId = new int?(iSystemUserId)
            });
          }
          transactionScope.Complete();
        }
        Message.SetMessage(this.lblMsgError, new HandledException(2, "Exito*****<br>Operación concretada con exito"));
        this.pnDatosAdicionales.Visible = false;
        this.trManagementButtons.Visible = false;
        this.trwibFinalize.Visible = true;
        this.Session["sePistoleados"] = (object) null;
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
      this.pnVerificador.Visible = true;
      this.pnDatosAdicionales.Visible = false;
      this.trMaintenance1.Visible = true;
      this.HidePopup();
    }

    private void ChekSobre()
    {
      try
      {
        string empty = string.Empty;
        string str1;
        if (this.TxtVerificador.Text.Trim().Length != 10 && this.TxtVerificador.Text.Trim() != "")
        {
          if (this.TxtVerificador.Text.Trim().Length < 5 || this.TxtVerificador.Text.Trim().Length > 8)
            throw new HandledException(1, "Dato ingresado es incorrecto");
          str1 = this.TxtVerificador.Text.Trim();
        }
        else
        {
          if (this.TxtVerificador.Text.Trim() == "")
            throw new HandledException(1, "Adventencia:<br>•&nbsp;&nbsp;Dato ingresado es incorrecto");
          str1 = !(this.TxtVerificador.Text.Trim().Substring(2, 1) == "0") ? this.TxtVerificador.Text.Trim().Substring(2) : this.TxtVerificador.Text.Trim().Substring(3);
        }
        this.TxtVerificador.Text = "";
        this.TxtVerificador.Focus();
        string str2 = Convert.ToInt32(str1, (IFormatProvider) CultureInfo.CurrentCulture).ToString((IFormatProvider) CultureInfo.CurrentCulture);
        DataTable dataTable = this.ViewState["vsdtBatchReceptionDetail"] as DataTable;
        dataTable.Columns["i_IsPistol"].ReadOnly = false;
        bool flag = false;
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["i_RequirementPlateId"].ToString() == str2)
            {
              int int32 = Convert.ToInt32(row["i_IsPistol"], (IFormatProvider) CultureInfo.CurrentCulture);
              flag = true;
              if (Convert.ToInt32(row["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == 11)
              {
                this.TxtVerificador.Text = "";
                this.TxtVerificador.Focus();
                throw new HandledException(1, "Adventencia:<br>•&nbsp;&nbsp;Este Kit se encuentra actualmente en reclamo");
              }
              if (Convert.ToInt32(row["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) != 6)
              {
                if (int32 == 1)
                {
                  this.TxtVerificador.Text = "";
                  this.TxtVerificador.Focus();
                  break;
                }
                if (Convert.ToInt32(row["i_IsPistol"], (IFormatProvider) CultureInfo.CurrentCulture) != 3)
                {
                  int num = int.Parse(this.lblLeyendaSuperior.Text, (IFormatProvider) CultureInfo.CurrentCulture) + 1;
                  this.lblLeyendaSuperior.Text = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
                  this.lblLeyendaInferior.Text = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
                }
                row["i_IsPistol"] = (object) 3;
                this.lblMsgError.Visible = false;
                break;
              }
            }
            else
              this.ViewState["vsIdsNoExist"] = (object) str2;
          }
          this.wdgBatchReceptionDetail.DataSource = (object) dataTable;
        }
        this.wdgBatchReceptionDetail.DataBind();
        this.ViewState["vsdtBatchReceptionDetail"] = (object) dataTable;
        this.Session["sePistoleados"] = (object) dataTable;
        if (!flag)
        {
          Message.SetMessage(this.lblMsgError, new HandledException(2, "Adventencia:<br>•&nbsp;&nbsp;El sobre " + str2 + " No pertenece a este Lote"));
          this.ViewState["NotFoundIds"] = (object) str2;
          this.trClainUnit.Visible = true;
        }
        else
          this.trClainUnit.Visible = false;
        if (dataTable != null)
        {
          if (dataTable.Rows.Count == int.Parse(this.lblLeyendaSuperior.Text, (IFormatProvider) CultureInfo.CurrentCulture))
            this.lblMsgError.Text = "ACEPTACIÓN TOTAL";
          else if (int.Parse(this.lblLeyendaSuperior.Text, (IFormatProvider) CultureInfo.CurrentCulture) > 0)
            this.lblMsgError.Text = "ACEPTACIÓN PARCIAL";
        }
        this.TxtVerificador.Text = "";
        this.TxtVerificador.Focus();
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

    private void SetProductImagen(
      DataTable dt,
      int pintimagenAncho,
      float fltFuentePosX,
      float fltFuentePosY)
    {
      try
      {
        System.Web.UI.WebControls.Image image = new System.Web.UI.WebControls.Image();
        string empty = string.Empty;
        for (int index = 0; index < dt.Rows.Count; ++index)
        {
          string pstrPkImagen = this.ReturnPathImage(Convert.ToInt32(dt.Rows[index]["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture));
          ((System.Web.UI.WebControls.Image) this.wdgBatchReceptionDetail.DataKeys[index]["imgPlate"]).ImageUrl = "~\\UserControls\\GetImageText.ashx?" + this.getParameterRequest("imgPlate", pintimagenAncho.ToString((IFormatProvider) CultureInfo.CurrentCulture), dt.Rows[index]["v_PlateNew"].ToString(), "Verdana", "Black", "8", fltFuentePosX.ToString((IFormatProvider) CultureInfo.CurrentCulture), fltFuentePosY.ToString((IFormatProvider) CultureInfo.CurrentCulture), pstrPkImagen);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private string ShowBinaryImage(
      byte[] image,
      string strTitle,
      int pintimagenAncho,
      float fltFuentePosX,
      float fltFuentePosY)
    {
      System.Web.UI.WebControls.Image image1 = new System.Web.UI.WebControls.Image();
      Font font = new Font("Verdana", 8f, FontStyle.Bold);
      Brush black = Brushes.Black;
      ImageConverter imageConverter = new ImageConverter();
      using (MemoryStream memoryStream = new MemoryStream(image))
      {
        using (Bitmap bitmap = (Bitmap) System.Drawing.Image.FromStream((Stream) memoryStream))
        {
          int thumbHeight = bitmap.Height * pintimagenAncho / bitmap.Width;
          System.Drawing.Image thumbnailImage = bitmap.GetThumbnailImage(pintimagenAncho, thumbHeight, (System.Drawing.Image.GetThumbnailImageAbort) null, IntPtr.Zero);
          if (strTitle != string.Empty)
          {
            Graphics graphics = Graphics.FromImage(thumbnailImage);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawString(strTitle, font, black, fltFuentePosX, fltFuentePosY);
            graphics.Save();
          }
          return "data:image/jpg; base64," + Convert.ToBase64String((byte[]) imageConverter.ConvertTo((object) thumbnailImage, typeof (byte[])));
        }
      }
    }

    private string ReturnPathImage(int intProductId)
    {
      string empty = string.Empty;
      string str;
      switch (intProductId)
      {
        case 35:
          str = this.Server.MapPath("~/Images/Requirement/placas/auto_particular.png");
          break;
        case 36:
          str = this.Server.MapPath("~/Images/Requirement/placas/auto_taxi.png");
          break;
        case 37:
          str = this.Server.MapPath("~/Images/Requirement/placas/autobus_urbano.png");
          break;
        case 38:
          str = this.Server.MapPath("~/Images/Requirement/placas/autobus_interprovincial.png");
          break;
        case 39:
          str = this.Server.MapPath("~/Images/Requirement/placas/autobus_contingencia.png");
          break;
        case 40:
          str = this.Server.MapPath("~/Images/Requirement/placas/camion.png");
          break;
        case 41:
          str = this.Server.MapPath("~/Images/Requirement/placas/emergencia.png");
          break;
        case 49:
          str = this.Server.MapPath("~/Images/Requirement/placas/moto_particular.png");
          break;
        case 50:
          str = this.Server.MapPath("~/Images/Requirement/placas/moto_taxi.png");
          break;
        case 52:
          str = this.Server.MapPath("~/Images/Requirement/placas/policial.png");
          break;
        case 53:
          str = this.Server.MapPath("~/Images/Requirement/placas/remolque.png");
          break;
        default:
          str = this.Server.MapPath("~/Images/Requirement/placas/sinplaca.png");
          break;
      }
      return str;
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

    private void RefreshParent()
    {
      string script = "RefreshParent();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void PopupClose()
    {
      string script = "PopupClose();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    public byte[] ImageToBytes(System.Drawing.Image img)
    {
      return (byte[]) new ImageConverter().ConvertTo((object) img, typeof (byte[]));
    }

    private string getParameterRequest(
      string pstrType,
      string pstrWidht,
      string pstrMessage,
      string pstrFontFamily,
      string pstrFontColor,
      string pstrFontSize,
      string pstrPosX,
      string pstrPosY,
      string pstrPkImagen)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (pstrType.Length > 0)
      {
        stringBuilder.Append("type=");
        stringBuilder.Append(HttpUtility.UrlEncode(pstrType));
        stringBuilder.Append("&");
      }
      if (pstrPkImagen.Length > 0)
      {
        stringBuilder.Append("pkImagen=");
        stringBuilder.Append(HttpUtility.UrlEncode(pstrPkImagen));
        stringBuilder.Append("&");
      }
      if (pstrWidht.Length > 0)
      {
        stringBuilder.Append("ancho=");
        stringBuilder.Append(pstrWidht);
        stringBuilder.Append("&");
      }
      if (pstrMessage.Length > 0)
      {
        stringBuilder.Append("t=");
        stringBuilder.Append(HttpUtility.UrlEncode(pstrMessage));
        stringBuilder.Append("&");
      }
      if (pstrFontFamily.Length > 0)
      {
        stringBuilder.Append("ff=");
        stringBuilder.Append(HttpUtility.UrlEncode(pstrFontFamily));
        stringBuilder.Append("&");
      }
      if (pstrFontColor.Length > 0)
      {
        stringBuilder.Append("fc=");
        stringBuilder.Append(HttpUtility.UrlEncode(pstrFontColor));
        stringBuilder.Append("&");
      }
      if (pstrFontSize.Length > 0)
      {
        stringBuilder.Append("fs=");
        stringBuilder.Append(HttpUtility.UrlEncode(pstrFontSize));
        stringBuilder.Append("&");
      }
      if (pstrPosX.Length > 0)
      {
        stringBuilder.Append("fx=");
        stringBuilder.Append(HttpUtility.UrlEncode(pstrPosX));
        stringBuilder.Append("&");
      }
      if (pstrPosY.Length > 0)
      {
        stringBuilder.Append("fy=");
        stringBuilder.Append(HttpUtility.UrlEncode(pstrPosY));
        stringBuilder.Append("&");
      }
      return stringBuilder.ToString();
    }

    protected void wdgBatchReceptionDetail_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
    }

    protected void wdgBatchReceptionDetail_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
    }
  }
}
