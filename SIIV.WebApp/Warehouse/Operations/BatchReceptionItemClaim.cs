// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.BatchReceptionItemClaim
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class BatchReceptionItemClaim : Page
  {
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
    protected Panel Panel1;
    protected Label lblLeyendaInferior;
    protected Label lblLeyendaInferiorCantidad;
    protected Panel Panel2;
    protected TextBox TxtObservacion;
    protected HtmlTableRow Tr1;
    protected Button wibAceptarPopup;
    protected Button wibCancelarPopup;
    protected Label lblMsgError;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMsgError.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.ViewState["i_PlateTypeId"] = (object) this.Request.QueryString["i_PlateTypeId"].ToString();
        this.ShowReceptionBatchInfo();
        this.SearchReceptionBatchDetail(int.Parse(this.TxtIdLote.Text, (IFormatProvider) CultureInfo.CurrentCulture), -1);
        if (this.Session["ProductAddWastageClainDetail"] != null)
          this.Session["ProductAddWastageClainDetail"] = (object) null;
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
        int num = 7;
        ImageButton control1 = e.Row.FindControl("ibtCheck") as ImageButton;
        CheckBox control2 = e.Row.FindControl("chkVerifyChange") as CheckBox;
        for (int index = 2; index < num; ++index)
          e.Row.Cells[index].CssClass = "Test2";
        if (e.Row.Cells[1].Text == "3")
        {
          for (int index = 2; index < num; ++index)
            e.Row.Cells[index].CssClass = "Test";
          control1.ImageUrl = "~/Images/Design/checkbox_checked_16.png";
        }
        if (e.Row.Cells[1].Text == "1")
        {
          for (int index = 2; index < num; ++index)
            e.Row.Cells[index].CssClass = "Test";
          control1.ImageUrl = "~/Images/Design/checkbox_checked_16.png";
        }
        if (e.Row.Cells[1].Text == "1" && e.Row.Cells[0].Text == "4")
          control2.Enabled = true;
        else if (e.Row.Cells[1].Text == "0" && e.Row.Cells[0].Text == "4")
          control2.Enabled = false;
        else if (e.Row.Cells[1].Text == "1" && e.Row.Cells[0].Text == "1")
        {
          control2.Checked = true;
          control2.Enabled = false;
        }
        else
        {
          if (!(e.Row.Cells[1].Text == "3") || !(e.Row.Cells[0].Text == "4"))
            return;
          control2.Enabled = false;
        }
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

    protected void wdgBatchReceptionDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      int int32 = Convert.ToInt32(e.CommandArgument);
      GridViewRow row = this.wdgBatchReceptionDetail.Rows[int32];
      if (!e.CommandName.Equals("Check", StringComparison.CurrentCulture))
        return;
      try
      {
        this.ChekSobreClaim(Convert.ToInt32(this.wdgBatchReceptionDetail.DataKeys[int32]["i_RequirementPlateId"].ToString()));
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

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
      CheckBox checkBox = (CheckBox) sender;
      if (!checkBox.Checked)
        return;
      GridViewRow parent = (sender as CheckBox).Parent.Parent as GridViewRow;
      this.CreatePopUp("Lista Objetos de Intercambio", "VerifyBatchReceptionClaimDetail.aspx?ProductId=" + checkBox.ToolTip + "&ids=" + parent.Cells[3].Text, "700px", "450px");
    }

    protected void wibAceptarPopup_Click(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "Error de Sesión", "Error de Sesion 'Objeto' en 'BatchReceptionItemClaim.aspx'");
        int iSystemUserId1 = systemUser.i_SystemUserId;
        int iSystemUserId2 = systemUser.i_SystemUserId;
        Batch batch = this.Session["sBatch"] != null ? this.Session["sBatch"] as Batch : throw new HandledException(3, "Error de Sesión", "Error de Sesion 'sBatch' en 'BatchReceptionItemClaim.aspx'");
        int index = 0;
        DataTable dataTable = this.ViewState["vsdtBatchReceptionDetail"] != null ? (DataTable) this.ViewState["vsdtBatchReceptionDetail"] : throw new HandledException(3, "Error de Sesión", "Error de Sesion 'vsdtBatchReceptionDetail' en 'BatchReceptionItemClaim.aspx'");
        if (dataTable == null)
          throw new HandledException(1, "No Hay Items a Verificar");
        if (dataTable.Rows.Count != int.Parse(this.lblLeyendaInferior.Text, (IFormatProvider) CultureInfo.CurrentCulture))
          throw new HandledException(1, "Debe verificar todos los sobres para aceptar el lote");
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
        {
          Timeout = new TimeSpan(0, 0, 5, 1)
        }))
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            CheckBox control = this.wdgBatchReceptionDetail.Rows[index].FindControl("chkVerifyChange") as CheckBox;
            int int32_1 = Convert.ToInt32(row["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture);
            int int32_2 = Convert.ToInt32(row["i_IsPistol"], (IFormatProvider) CultureInfo.CurrentCulture);
            int int32_3 = Convert.ToInt32(row["i_StatusSobreId"], (IFormatProvider) CultureInfo.CurrentCulture);
            switch (int32_2)
            {
              case 0:
                new BatchReceptionManagementBL().BatchReceptionDetailUpdateState(int32_1, 4, iSystemUserId2, 0);
                break;
              case 1:
                if (int32_3 != 4 || !control.Checked)
                  throw new HandledException(1, "No ha Seleccionado Objetos de Intercambio para Solicitud:" + int32_1.ToString());
                new BatchReceptionManagementBL().BatchReceptionDetailUpdateState(int32_1, 1, iSystemUserId2, 1);
                break;
              case 3:
                new BatchReceptionManagementBL().BatchReceptionDetailUpdateState(int32_1, 4, iSystemUserId2, 1);
                break;
            }
            ++index;
          }
          new BatchReceptionManagementBL().BatchReceptionUpdateState(new Batch()
          {
            i_BatchId = batch.i_BatchId,
            i_ReceptionStatus = new int?(2),
            v_Observations = this.TxtObservacion.Text,
            i_CheckItems = new int?(Convert.ToInt32(this.lblLeyendaInferior.Text, (IFormatProvider) CultureInfo.CurrentCulture)),
            i_UserReceptionId = new int?(iSystemUserId2)
          });
          if (this.Session["ProductAddWastageClainDetail"] != null)
          {
            foreach (DataRow row in (InternalDataCollectionBase) (this.Session["ProductAddWastageClainDetail"] as DataTable).Rows)
              new WarehouseWastageManagementBL().WarehouseWastageInsert(new WarehouseWastage()
              {
                i_LocationId = batch.i_LocationId,
                i_WarehouseId = new int?(43),
                i_DispatchId = batch.i_DispatchId,
                i_BatchId = new int?(batch.i_BatchId),
                i_RequirementPlateId = new int?(Convert.ToInt32(row["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture)),
                i_MotiveMovementId = new int?(33),
                i_ProductId = new int?(Convert.ToInt32(row["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture)),
                i_ComponentId = new int?(Convert.ToInt32(row["i_ComponentId"], (IFormatProvider) CultureInfo.CurrentCulture)),
                i_InsertUserId = new int?(iSystemUserId1)
              });
            this.Session["ProductAddWastageClainDetail"] = (object) null;
          }
          transactionScope.Complete();
        }
        Message.SetMessage(this.lblMsgError, new HandledException(2, "Operación concretada con exito"));
        this.wibAceptarPopup.Enabled = false;
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

    protected void wibCancelarPopup_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("BatchReceptionList.aspx?t=" + this.ViewState["i_PlateTypeId"]?.ToString());
    }

    private void ShowReceptionBatchInfo()
    {
      try
      {
        Batch batch = this.Session["sBatch"] != null ? this.Session["sBatch"] as Batch : throw new HandledException(3, "Error de Sesión", "Error de Sesion 'sBatch' en 'BatchReceptionItemClaim.aspx'");
        this.TxtIdLote.Text = batch.i_BatchId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        TextBox txtFechaRecepcion = this.TxtFechaRecepcion;
        DateTime? nullable = batch.d_ReceptionDate;
        string str1 = !nullable.HasValue ? string.Empty : Convert.ToDateTime((object) batch.d_ReceptionDate, (IFormatProvider) CultureInfo.CurrentCulture).ToShortDateString();
        txtFechaRecepcion.Text = str1;
        this.TxtEstadoLote.Text = batch.v_BatchStatus;
        this.TxtItemsCaja.Text = batch.v_ItemsMax;
        this.TxtIdDespacho.Text = batch.i_DispatchId.ToString();
        TextBox txtFechaDespacho = this.TxtFechaDespacho;
        nullable = batch.d_EntryToDispatch;
        string str2 = !nullable.HasValue ? string.Empty : Convert.ToDateTime((object) batch.d_EntryToDispatch, (IFormatProvider) CultureInfo.CurrentCulture).ToShortDateString();
        txtFechaDespacho.Text = str2;
        this.TxtEstadoRecepcion.Text = this.Page.Server.HtmlDecode(batch.v_ReceptionStatus);
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
          if (Convert.ToInt32(row["i_IsPistol"], (IFormatProvider) CultureInfo.CurrentCulture) == 1)
            ++num;
        }
        this.ViewState["vsdtBatchReceptionDetail"] = (object) receptionDetailBy;
        this.lblLeyendaInferior.Text = num.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.lblLeyendaInferiorCantidad.Text = "/" + receptionDetailBy.Rows.Count.ToString();
        this.SetProductImagen(receptionDetailBy, 100, 30f, 30f);
      }
      catch (Exception ex)
      {
        throw ex;
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
          byte[] bytes = this.ImageToBytes(System.Drawing.Image.FromFile(this.ReturnPathImage(Convert.ToInt32(dt.Rows[index]["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture))));
          ((WebControl) this.wdgBatchReceptionDetail.Rows[index].FindControl("imgPlate")).Attributes["src"] = this.ShowBinaryImage(bytes, dt.Rows[index]["v_PlateNew"].ToString(), pintimagenAncho, fltFuentePosX, fltFuentePosY);
        }
      }
      catch (Exception ex)
      {
        throw ex;
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

    private string ShowBinaryImage(
      byte[] image,
      string strTitle,
      int pintimagenAncho,
      float fltFuentePosX,
      float fltFuentePosY)
    {
      try
      {
        Font font = new Font("Verdana", 8f, FontStyle.Bold);
        Brush black = Brushes.Black;
        ImageConverter imageConverter = new ImageConverter();
        using (MemoryStream memoryStream = new MemoryStream(image))
        {
          using (Bitmap bitmap = (Bitmap) System.Drawing.Image.FromStream((Stream) memoryStream))
          {
            int thumbHeight = bitmap.Height * pintimagenAncho / bitmap.Width;
            System.Drawing.Image thumbnailImage = bitmap.GetThumbnailImage(pintimagenAncho, thumbHeight, (System.Drawing.Image.GetThumbnailImageAbort) null, IntPtr.Zero);
            if (!string.IsNullOrEmpty(strTitle))
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
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ChekSobreClaim(int pintRequirementPlateId)
    {
      try
      {
        DataTable dt = this.ViewState["vsdtBatchReceptionDetail"] != null ? this.ViewState["vsdtBatchReceptionDetail"] as DataTable : throw new HandledException(3, "Error de Sesión", "Error de Sesion 'vsdtBatchReceptionDetail' en 'BatchRecepctionItemClaim.aspx'");
        dt.Columns["i_IsPistol"].ReadOnly = false;
        foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
        {
          if (Convert.ToInt32(row["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture) == pintRequirementPlateId)
          {
            int num;
            switch (Convert.ToInt32(row["i_StatusSobreId"], (IFormatProvider) CultureInfo.CurrentCulture))
            {
              case 1:
                throw new HandledException(1, "El sobre ya está Receptionado");
              case 4:
                num = row["i_IsPistol"].ToString() == "1" ? 1 : (row["i_IsPistol"].ToString() == "3" ? 1 : 0);
                break;
              default:
                num = 0;
                break;
            }
            if (num != 0)
              throw new HandledException(1, "El sobre ya está verificado");
            this.lblLeyendaInferior.Text = (int.Parse(this.lblLeyendaInferior.Text, (IFormatProvider) CultureInfo.CurrentCulture) + 1).ToString((IFormatProvider) CultureInfo.CurrentCulture);
            row["i_IsPistol"] = (object) 3;
            break;
          }
        }
        this.ViewState["vsdtBatchReceptionDetail"] = (object) dt;
        this.wdgBatchReceptionDetail.DataSource = (object) dt;
        this.wdgBatchReceptionDetail.DataBind();
        this.SetProductImagen(dt, 100, 30f, 30f);
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
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    public byte[] ImageToBytes(System.Drawing.Image img)
    {
      return (byte[]) new ImageConverter().ConvertTo((object) img, typeof (byte[]));
    }
  }
}
