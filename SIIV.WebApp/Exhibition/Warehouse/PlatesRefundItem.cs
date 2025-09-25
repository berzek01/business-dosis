// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Warehouse.PlatesRefundItem
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.BE.CustomCode;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Warehouse
{
  public class PlatesRefundItem : Page
  {
    private DataTable dt_Result;
    private WarehouseExhibitionQueriesBL ObjWarehouseExhibitionQueriesBL;
    private int i_PlateTypeId = 0;
    protected UpdatePanel UpdatePanel1;
    protected Label Label8;
    protected TextBox txtAssoaciatedName;
    protected Label Label9;
    protected DropDownList wddDocumentType;
    protected Label Label10;
    protected TextBox txtDocumentNumber;
    protected Label Label3;
    protected TextBox txtProductName;
    protected Label Label7;
    protected TextBox txtPlate;
    protected RadioButtonList rblCollectType;
    protected Label Label4;
    protected TextBox txtCollectName;
    protected Label Label5;
    protected DropDownList wddCollectDocumentType;
    protected Label Label6;
    protected TextBox txtCollectDocumentNumber;
    protected RadioButtonList rblAccion;
    protected CheckBoxList chkPlacas;
    protected HtmlGenericControl divDatosDenuncia;
    protected Fecha wdpDateDenuncia;
    protected TextBox txtDependency;
    protected TextBox txtParte;
    protected HtmlGenericControl divDatosPago;
    protected Label Label2;
    protected TextBox txtVoucher;
    protected Label Label1;
    protected TextBox txtComments;
    protected Button wibSave;
    protected Button wibCancel;
    protected Button wibReturn;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        if (this.Request.QueryString["i_RequirementPlateId"] != null)
        {
          this.ViewState["i_PlateTypeId"] = (object) this.Request.QueryString["t"].ToString();
          this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
          this.ObjWarehouseExhibitionQueriesBL = new WarehouseExhibitionQueriesBL();
          this.dt_Result = new DataTable();
          this.dt_Result = this.ObjWarehouseExhibitionQueriesBL.SpecialPlateRefundPlateGetAllById(Convert.ToInt32(this.Request.QueryString["i_RequirementPlateId"].ToString()), Convert.ToInt32(new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(this.i_PlateTypeId).Rows[0]["i_WarehouseId"].ToString()));
          if (this.dt_Result.Rows.Count > 0)
          {
            this.LoadDocumentType();
            this.SetControls(this.dt_Result);
            this.ViewState["i_ProductId"] = (object) this.dt_Result.Rows[0]["i_ProductId"].ToString();
            this.ViewState["i_SenderUserId"] = (object) this.dt_Result.Rows[0]["i_SystemUserId"].ToString();
          }
        }
        this.rblAccion_SelectedIndexChanged((object) null, (EventArgs) null);
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void rblAccion_SelectedIndexChanged(object sender, EventArgs e)
    {
      int int32 = Convert.ToInt32(this.ViewState["i_ProductId"]);
      this.divDatosDenuncia.Visible = false;
      switch (Convert.ToInt16(this.rblAccion.SelectedValue))
      {
        case 1:
          if (int32 == 43 || int32 == 348)
          {
            this.chkPlacasEnabled(true, false, false, false, false);
            this.chkPlacasSelected(false, false, false, true, false);
          }
          else if (int32 == 347)
          {
            this.chkPlacasEnabled(true, false, false, false, false);
            this.chkPlacasSelected(false, false, false, true, false);
          }
          else
          {
            this.chkPlacasEnabled(false, true, false, false, false);
            this.chkPlacasSelected(false, false, false, true, false);
          }
          this.divDatosPago.Visible = false;
          break;
        case 2:
          if (int32 == 43 || int32 == 348)
          {
            this.chkPlacasEnabled(true, false, false, false, true);
            this.chkPlacasSelected(false, false, false, false, false);
            break;
          }
          if (int32 == 347)
          {
            this.chkPlacasEnabled(true, false, false, false, true);
            this.chkPlacasSelected(false, false, false, false, false);
            break;
          }
          this.chkPlacasEnabled(false, true, false, false, true);
          this.chkPlacasSelected(false, false, false, false, false);
          break;
        case 3:
          if (int32 == 43 || int32 == 348)
          {
            this.chkPlacasEnabled(true, true, false, false, false);
            this.chkPlacasSelected(false, false, true, false, true);
          }
          else if (int32 == 347)
          {
            this.chkPlacasEnabled(true, true, false, false, false);
            this.chkPlacasSelected(false, false, true, false, true);
          }
          else
          {
            this.chkPlacasEnabled(false, true, false, false, false);
            this.chkPlacasSelected(false, false, true, false, true);
          }
          this.chkPlacasExchangeMode_SelectedIndexChanged((object) null, (EventArgs) null);
          break;
        case 4:
          if (int32 == 43 || int32 == 348)
          {
            this.chkPlacasEnabled(true, true, false, false, false);
            this.chkPlacasSelected(false, false, true, false, false);
          }
          else
          {
            int num;
            switch (int32)
            {
              case 44:
                num = 1;
                break;
              case 347:
                this.chkPlacasEnabled(true, true, false, false, false);
                this.chkPlacasSelected(false, false, false, false, false);
                goto label_27;
              default:
                num = int32 == 345 ? 1 : 0;
                break;
            }
            if (num != 0)
            {
              this.chkPlacasEnabled(false, true, false, false, true);
              this.chkPlacasSelected(false, false, true, false, false);
            }
            else
            {
              this.chkPlacasEnabled(true, true, false, false, true);
              this.chkPlacasSelected(false, false, true, false, false);
            }
          }
label_27:
          this.divDatosPago.Visible = false;
          break;
      }
    }

    protected void chkPlacasExchangeMode_SelectedIndexChanged(object sender, EventArgs e)
    {
      int int32 = Convert.ToInt32(this.ViewState["i_ProductId"]);
      if (this.chkPlacas.Items[2].Selected && Convert.ToInt16(this.rblAccion.SelectedValue) != (short) 4)
      {
        this.divDatosDenuncia.Visible = true;
        this.txtDependency.Text = string.Empty;
        this.txtParte.Text = string.Empty;
        this.txtDependency.Focus();
      }
      if (this.chkPlacas.Items[0].Selected)
        this.chkPlacas.Items[1].Enabled = false;
      else if ((Convert.ToInt16(this.rblAccion.SelectedValue) == (short) 3 || Convert.ToInt16(this.rblAccion.SelectedValue) == (short) 4) && !this.chkPlacas.Items[1].Selected)
      {
        this.chkPlacas.Items[1].Selected = false;
        this.chkPlacas.Items[1].Enabled = true;
      }
      if (this.chkPlacas.Items[1].Selected)
        this.chkPlacas.Items[0].Enabled = false;
      else if ((Convert.ToInt16(this.rblAccion.SelectedValue) == (short) 3 || Convert.ToInt16(this.rblAccion.SelectedValue) == (short) 4) && !this.chkPlacas.Items[0].Selected)
      {
        this.chkPlacas.Items[0].Selected = false;
        this.chkPlacas.Items[0].Enabled = true;
      }
      if ((Convert.ToInt16(this.rblAccion.SelectedValue) == (short) 3 || Convert.ToInt16(this.rblAccion.SelectedValue) == (short) 4) && !this.chkPlacas.Items[0].Selected && (int32 == 44 || int32 == 345))
      {
        this.chkPlacas.Items[0].Selected = false;
        this.chkPlacas.Items[0].Enabled = false;
      }
      if (this.chkPlacas.Items[4].Selected)
      {
        this.divDatosPago.Visible = true;
        this.txtVoucher.Text = string.Empty;
        this.txtVoucher.Focus();
      }
      else
        this.divDatosPago.Visible = false;
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      try
      {
        if (!this.validateRefund())
          return;
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - PlatesRefundItem.aspx");
        DataTable dtproducts = new DTStockMovementDetail().DataTableStockMovementDetail();
        string str1 = string.Empty;
        List<PlateMovement> pobjlstPlateMovement = new List<PlateMovement>();
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        DataTable dataTable = new DataTable();
        Convert.ToInt32(new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(this.i_PlateTypeId).Rows[0]["i_WarehouseId"].ToString());
        if (this.Request.QueryString["i_RequirementPlateId"] == null)
          return;
        int int32 = Convert.ToInt32(this.Request.QueryString["i_RequirementPlateId"].ToString());
        PlateMovement plateMovement = new PlateMovement();
        plateMovement.i_RequirementPlateId = int32;
        plateMovement.i_MovementTypeId = 1;
        switch (Convert.ToInt16(this.rblAccion.SelectedValue))
        {
          case 1:
            if (Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 43 || Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 348 || Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 347)
            {
              str1 = "4|5";
              break;
            }
            if (Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 44 || Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 345)
            {
              str1 = "4";
              break;
            }
            break;
          case 2:
            if (Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 43 || Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 348 || Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 347)
            {
              str1 = "4|5";
              break;
            }
            if (Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 44 || Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 345)
            {
              str1 = "4";
              break;
            }
            break;
          case 3:
            if (Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 43 || Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 348 || Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 347)
            {
              if (this.chkPlacas.Items[0].Value == "1")
                str1 = "4|5";
              if (this.chkPlacas.Items[0].Value == "2")
              {
                str1 = "4";
                break;
              }
              break;
            }
            if (Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 44 || Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 345)
            {
              str1 = "4";
              break;
            }
            break;
          case 4:
            if (Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 43 || Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 348 || Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 347)
            {
              if (this.chkPlacas.Items[0].Value == "1")
                str1 = "4|5";
              if (this.chkPlacas.Items[0].Value == "2")
              {
                str1 = "4";
                break;
              }
              break;
            }
            if (Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 44 || Convert.ToInt32(this.ViewState["i_ProductId"].ToString()) == 345)
            {
              str1 = "4";
              break;
            }
            break;
        }
        plateMovement.v_InterchangeObjects = str1;
        string empty = string.Empty;
        string str2 = !(this.wdpDateDenuncia.Text == "") && !(this.wdpDateDenuncia.Text == string.Empty) ? Convert.ToDateTime(this.wdpDateDenuncia.Value).ToShortDateString() : string.Empty;
        plateMovement.v_InterchangeData = str2 + "|" + this.txtDependency.Text + "|" + this.txtParte.Text.Trim() + "|" + this.txtVoucher.Text.Trim();
        plateMovement.i_SenderUserId = Convert.ToInt32(this.ViewState["i_SenderUserId"].ToString());
        plateMovement.i_ReceiverUserId = systemUser.i_SystemUserId;
        plateMovement.v_ReceiverData = this.rblCollectType.SelectedValue.ToString() + "|" + this.txtCollectName.Text.Trim() + "|" + this.wddCollectDocumentType.SelectedValue + "|" + this.txtCollectDocumentNumber.Text.Trim();
        plateMovement.d_DateRegister = new DateTime?(DateTime.Now);
        plateMovement.v_Observations = this.txtComments.Text;
        plateMovement.i_Status = 1;
        plateMovement.i_InsertUserId = systemUser.i_SystemUserId;
        plateMovement.d_InsertDate = new DateTime?(DateTime.Now);
        plateMovement.i_TypeRefund = this.rblAccion.Items[0].Selected ? 1 : (this.rblAccion.Items[1].Selected ? 5 : (this.rblAccion.Items[2].Selected ? 4 : 9));
        pobjlstPlateMovement.Add(plateMovement);
        DataRowCollection rows = dtproducts.Rows;
        object[] objArray = new object[22];
        objArray[3] = this.ViewState["i_ProductId"];
        objArray[6] = (object) 1;
        objArray[20] = (object) this.txtPlate.Text.TrimEnd();
        rows.Add(objArray);
        StockMovement currentStockMovement = this.GetCurrentStockMovement(this.rblAccion.Items[0].Selected ? 61 : (this.rblAccion.Items[1].Selected ? 62 : (this.rblAccion.Items[2].Selected ? 63 : 62)));
        if (new WarehouseExhibitionManagementBL().ExhibitionDeliveryPlateInsert(pobjlstPlateMovement, currentStockMovement, dtproducts))
        {
          Message.SetMessage(this.lblMessage, new HandledException(2, "Se registró correctamente la devolución de la placa"));
          this.ClearControls();
          this.wibSave.Enabled = false;
          this.rblCollectType.Enabled = false;
          this.txtCollectName.Enabled = false;
          this.wddCollectDocumentType.Enabled = false;
          this.txtCollectDocumentNumber.Enabled = false;
          this.rblAccion.Enabled = false;
          this.chkPlacas.Enabled = false;
          this.txtComments.Enabled = false;
          this.wibCancel.Enabled = false;
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void wibCancel_Click(object sender, EventArgs e) => this.ClearControls();

    protected void wibReturn_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("PlatesRefund.aspx?t=" + this.ViewState["i_PlateTypeId"].ToString());
    }

    private void LoadDocumentType()
    {
      try
      {
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) ("" + SystemParameterGroups.PersonDocumentType.ToString()),
          (object) "",
          (object) "",
          (object) ""
        });
        if (dataTable == null || dataTable.Rows.Count == 0)
          return;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          this.wddDocumentType.Items.Add(new ListItem(row["v_Value"].ToString(), row["i_ParameterId"].ToString()));
          this.wddCollectDocumentType.Items.Add(new ListItem(row["v_Value"].ToString(), row["i_ParameterId"].ToString()));
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SetControls(DataTable dt)
    {
      try
      {
        this.txtAssoaciatedName.Text = dt.Rows[0]["v_ReasonSocial"].ToString();
        this.wddDocumentType.SelectedValue = dt.Rows[0]["i_DocumentTypeId"].ToString();
        this.txtDocumentNumber.Text = dt.Rows[0]["v_DocumentNumber"].ToString();
        this.txtPlate.Text = dt.Rows[0]["v_PlateNew"].ToString();
        this.txtProductName.Text = dt.Rows[0]["v_Name"].ToString();
        this.ViewState["i_ProductId"] = (object) dt.Rows[0]["i_ProductId"].ToString();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void chkPlacasEnabled(
      bool Plate2,
      bool Plate1,
      bool DenPol,
      bool Reasignable,
      bool PagPen)
    {
      this.chkPlacas.Items[0].Enabled = Plate2;
      this.chkPlacas.Items[1].Enabled = Plate1;
      this.chkPlacas.Items[2].Enabled = DenPol;
      this.chkPlacas.Items[3].Enabled = Reasignable;
      this.chkPlacas.Items[4].Enabled = PagPen;
    }

    private void chkPlacasSelected(
      bool Plate2,
      bool Plate1,
      bool DenPol,
      bool Reasignable,
      bool PagPen)
    {
      this.chkPlacas.Items[0].Selected = Plate2;
      this.chkPlacas.Items[1].Selected = Plate1;
      this.chkPlacas.Items[2].Selected = DenPol;
      this.chkPlacas.Items[3].Selected = Reasignable;
      this.chkPlacas.Items[4].Selected = PagPen;
    }

    private bool validateRefund()
    {
      try
      {
        if (!this.chkPlacas.Items[0].Selected && !this.chkPlacas.Items[1].Selected)
        {
          Message.SetMessage(this.lblMessage, new HandledException(1, "Debe seleccionar las Placas"));
          return false;
        }
        if (Convert.ToInt16(this.rblAccion.SelectedValue) == (short) 3)
        {
          if (this.wdpDateDenuncia.Text == string.Empty)
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "Datos de Denuncia: <br>_______________<br> Debe ingresar una fecha válida"));
            return false;
          }
          if (this.txtDependency.Text == string.Empty)
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "Datos de Denuncia: <br>_______________<br> Debe ingresar una dependencia válida"));
            return false;
          }
          if (this.txtParte.Text == string.Empty)
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "Datos de Denuncia: <br>_______________<br> Debe ingresar Nro de parte válido"));
            return false;
          }
          if (this.chkPlacas.Items[4].Selected && this.txtVoucher.Text == string.Empty)
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "Datos de Pago Penalidad: <br>_______________<br> Debe ingresar Nro Baucher del Pago de Penalidad."));
            return false;
          }
        }
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private StockMovement GetCurrentStockMovement(int i_MotiveMovementId)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - PlatesRefundItem.aspx");
        int num = 0;
        if (systemUser != null)
          num = systemUser.i_SystemUserId;
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        DataTable dataTable = new DataTable();
        int int32 = Convert.ToInt32(new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(this.i_PlateTypeId).Rows[0]["i_WarehouseId"].ToString());
        return new StockMovement()
        {
          i_StockMovementId = 0,
          i_WarehouseId = new int?(int32),
          i_MotiveMovementId = new int?(i_MotiveMovementId),
          i_SupplierId = new int?(),
          i_DocumentTypeId = new int?(),
          v_DocumentNumber = "",
          i_UserId = new int?(num),
          b_Checked = new bool?(true),
          v_Observation = this.txtComments.Text,
          d_InsertDate = new DateTime?(DateTime.Now),
          i_ProductionOrderId = new int?(),
          i_ShelfOnDemandId = -1
        };
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ClearControls()
    {
      this.txtCollectName.Text = "";
      this.wddCollectDocumentType.SelectedIndex = -1;
      this.txtCollectDocumentNumber.Text = "";
      this.txtComments.Text = "";
      this.txtPlate.Text = "";
      this.txtProductName.Text = "";
      this.rblCollectType.Items[0].Selected = false;
      this.rblCollectType.Items[1].Selected = false;
      this.rblAccion.Items[0].Selected = true;
      this.chkPlacas.Items[0].Selected = true;
      this.chkPlacas.Items[1].Selected = true;
      this.chkPlacas.Items[2].Selected = false;
      this.chkPlacas.Items[3].Selected = false;
      this.chkPlacas.Items[2].Enabled = false;
      this.chkPlacas.Items[3].Enabled = false;
    }
  }
}
