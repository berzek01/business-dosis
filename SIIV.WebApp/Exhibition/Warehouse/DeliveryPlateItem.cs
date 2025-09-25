// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Warehouse.DeliveryPlateItem
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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Warehouse
{
  public class DeliveryPlateItem : Page
  {
    private DataTable dt_Result;
    private WarehouseExhibitionQueriesBL ObjWarehouseExhibitionQueriesBL;
    private int i_PlateTypeId = 0;
    protected Label Label1;
    protected TextBox txtAssoaciatedName;
    protected Label Label3;
    protected DropDownList wddDocumentType;
    protected Label Label2;
    protected TextBox txtDocumentNumber;
    protected CheckBox chkAll;
    protected GridView wdgListPlate;
    protected Label lblCount;
    protected RadioButtonList rblCollectType;
    protected Label Label4;
    protected TextBox txtCollectName;
    protected Label Label5;
    protected DropDownList wddCollectDocumentType;
    protected Label Label6;
    protected TextBox txtCollectDocumentNumber;
    protected TextBox txtComments;
    protected CheckBox chkOK;
    protected Label Label10;
    protected Label Label9;
    protected Button wibSave;
    protected Button wibImprimir;
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
        this.LoadDocumentType();
        if (this.Request.QueryString["i_RequirementId"] != null)
        {
          this.ViewState["i_PlateTypeId"] = (object) this.Request.QueryString["t"].ToString();
          this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
          int int32 = Convert.ToInt32(this.Request.QueryString["i_RequirementId"].ToString());
          this.ViewState["i_RequirementId"] = (object) int32;
          this.SetControls(int32);
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

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkAll != null && this.chkAll.Checked)
        this.SelectChecks(true);
      else
        this.SelectChecks(false);
      this.HidePopup();
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      try
      {
        short num = 0;
        this.ObjWarehouseExhibitionQueriesBL = new WarehouseExhibitionQueriesBL();
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - DeliveryPlateItem.aspx");
        if (this.ValidatedControls())
          throw new HandledException(1, "Debe ingresar todos los datos del representante");
        if (!this.chkOK.Checked)
          throw new HandledException(1, "Debe marcar la conformidad de los requisitos presentados en el proceso de entrega");
        DataTable dtproducts = new DTStockMovementDetail().DataTableStockMovementDetail();
        List<PlateMovement> pobjlstPlateMovement = new List<PlateMovement>();
        foreach (GridViewRow row in this.wdgListPlate.Rows)
        {
          CheckBox control = (CheckBox) row.FindControl("chkItem");
          if (control != null && control.Checked)
          {
            ++num;
            if (Convert.ToInt32(this.wdgListPlate.DataKeys[row.RowIndex]["i_Status"]) == 1)
            {
              pobjlstPlateMovement.Add(new PlateMovement()
              {
                i_RequirementPlateId = Convert.ToInt32(this.wdgListPlate.DataKeys[row.RowIndex]["i_RequirementPlateId"]),
                i_MovementTypeId = 2,
                v_InterchangeObjects = "4|5|6|7",
                v_InterchangeData = string.Empty,
                i_SenderUserId = systemUser.i_SystemUserId,
                i_ReceiverUserId = Convert.ToInt32(this.wdgListPlate.DataKeys[row.RowIndex]["i_SystemUserId"]),
                v_ReceiverData = this.rblCollectType.SelectedValue.ToString() + "|" + this.txtCollectName.Text.Trim() + "|" + this.wddCollectDocumentType.SelectedValue + "|" + this.txtCollectDocumentNumber.Text.Trim(),
                d_DateRegister = new DateTime?(DateTime.Now),
                v_Observations = this.txtComments.Text.TrimEnd(),
                i_Status = 1,
                i_InsertUserId = systemUser.i_SystemUserId,
                d_InsertDate = new DateTime?(DateTime.Now),
                i_TypeRefund = 0
              });
              DataRowCollection rows = dtproducts.Rows;
              object[] objArray = new object[22];
              objArray[3] = (object) Convert.ToInt32(this.wdgListPlate.DataKeys[row.RowIndex]["i_ProductId"]);
              objArray[6] = (object) 1;
              objArray[20] = (object) row.Cells[3].Text.ToString();
              rows.Add(objArray);
            }
          }
        }
        if (num > (short) 0)
        {
          StockMovement currentStockMovement = this.GetCurrentStockMovement();
          if (!new WarehouseExhibitionManagementBL().ExhibitionDeliveryPlateInsert(pobjlstPlateMovement, currentStockMovement, dtproducts))
            return;
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(2, "Las Placas fueron entregadas correctamente. <br> Entregas: " + num.ToString()));
          this.SetControls(Convert.ToInt32(this.Request.QueryString["i_RequirementId"].ToString()));
          this.wibSave.Enabled = false;
          this.wibCancel.Enabled = false;
          this.wibImprimir.Enabled = true;
        }
        else
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(1, "Debe seleccionar por lo menos una Placa"));
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

    protected void wibImprimir_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable dataTable1 = new DataTable();
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        int int32_1 = Convert.ToInt32(this.ViewState["i_RequirementId"]);
        string filename = "";
        this.ObjWarehouseExhibitionQueriesBL = new WarehouseExhibitionQueriesBL();
        DataTable dataTable2 = new DataTable();
        int int32_2 = Convert.ToInt32(new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(this.i_PlateTypeId).Rows[0]["i_WarehouseId"].ToString());
        if (this.ValidatedControls())
          throw new HandledException(1, "Debe ingresar todos los datos del representante");
        if (!this.chkOK.Checked)
          throw new HandledException(1, "Debe marcar la conformidad de los requisitos presentados en el proceso de entrega");
        string v_Option = "print";
        DataTable byId = this.ObjWarehouseExhibitionQueriesBL.SpecialPlateDeliveryPlateGetById(int32_1, int32_2, v_Option);
        ReportDocument reportDocument = new ReportDocument();
        if (this.i_PlateTypeId == 7)
          filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportDeliveryPlateExhibition.rpt";
        else if (this.i_PlateTypeId == 11)
          filename = this.Server.MapPath("/") + "Exhibicion/Reports/ReportDeliveryPlate.rpt";
        reportDocument.Load(filename);
        reportDocument.SetDataSource(byId);
        reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "ReporteEntregaPlaca");
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

    protected void wibCancel_Click(object sender, EventArgs e) => this.ClearControls();

    protected void wibReturn_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("DeliveryPlate.aspx?t=" + this.ViewState["i_PlateTypeId"].ToString());
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

    private void SetControls(int i_RequirementId)
    {
      try
      {
        this.dt_Result = new DataTable();
        this.ObjWarehouseExhibitionQueriesBL = new WarehouseExhibitionQueriesBL();
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        DataTable dataTable = new DataTable();
        int int32 = Convert.ToInt32(new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(this.i_PlateTypeId).Rows[0]["i_WarehouseId"].ToString());
        string v_Option = "save";
        this.dt_Result = this.ObjWarehouseExhibitionQueriesBL.SpecialPlateDeliveryPlateGetById(i_RequirementId, int32, v_Option);
        if (this.dt_Result.Rows.Count > 0)
        {
          this.txtAssoaciatedName.Text = this.dt_Result.Rows[0]["v_ReasonSocial"].ToString();
          this.wddDocumentType.SelectedValue = this.dt_Result.Rows[0]["i_DocumentTypeId"].ToString();
          this.txtDocumentNumber.Text = this.dt_Result.Rows[0]["v_DocumentNumber"].ToString();
          this.lblCount.Text = Constants.SEARCHRESULT_OK.Replace("XX", this.dt_Result.Rows.Count.ToString());
        }
        else
          this.lblCount.Text = Constants.SEARCHRESULT_Empty;
        this.wdgListPlate.AutoGenerateColumns = false;
        this.wdgListPlate.DataSource = (object) this.dt_Result;
        this.wdgListPlate.DataBind();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SelectChecks(bool chk)
    {
      foreach (Control row in this.wdgListPlate.Rows)
        ((CheckBox) row.FindControl("chkItem")).Checked = chk;
    }

    private bool ValidatedControls()
    {
      bool flag = false;
      if (this.txtCollectName.Text == string.Empty || this.txtCollectName.Text == "" || this.txtCollectDocumentNumber.Text == string.Empty || this.txtCollectDocumentNumber.Text == "")
        flag = true;
      return flag;
    }

    private StockMovement GetCurrentStockMovement()
    {
      SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - DeliveryPlateItem.aspx");
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
        i_MotiveMovementId = new int?(60),
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

    private void ClearControls()
    {
      this.txtCollectName.Text = "";
      this.wddCollectDocumentType.SelectedIndex = -1;
      this.txtCollectDocumentNumber.Text = "";
      this.txtComments.Text = "";
      this.chkOK.Checked = false;
      this.rblCollectType.Items[0].Selected = false;
      this.rblCollectType.Items[1].Selected = false;
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
