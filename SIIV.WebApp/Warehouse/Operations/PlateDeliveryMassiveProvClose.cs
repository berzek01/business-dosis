// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.PlateDeliveryMassiveProvClose
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Registration.BL;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class PlateDeliveryMassiveProvClose : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected HiddenField hdTipPer;
    protected HiddenField hdTramite;
    protected HiddenField hdQuantity;
    protected RadioButtonList rblTypeRecabante;
    protected RadioButtonList rblPersonTypeCollect;
    protected TextBox txtName;
    protected DropDownList wddDocumentDescription;
    protected TextBox txtNumeroRecabante;
    protected FilteredTextBoxExtender txtNumeroRecabante_FilteredTextBoxExtender;
    protected RequiredFieldValidator ValidatorDocumento;
    protected ValidatorCalloutExtender ValidatorDocumento_ValidatorCalloutExtender;
    protected HtmlTableRow trNotaria;
    protected TextBox txtNotariaRecabante;
    protected HtmlTableRow trPartida;
    protected TextBox txtPartidaRecabante;
    protected CheckBoxList chklRequisiteToDeliver;
    protected Button wibSave;
    protected Button wibCancel;
    protected Label lblMessage;
    protected Button Button1;
    protected Button Button2;
    protected Image ImgLoading2;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.LoadParameter();
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

    protected void wddDocumentDescription_SelectionChanged(object sender, EventArgs e)
    {
      if (this.wddDocumentDescription.SelectedValue == "1")
      {
        this.txtNumeroRecabante_FilteredTextBoxExtender.FilterType = FilterTypes.Numbers;
        this.txtNumeroRecabante.MaxLength = 8;
      }
      else if (this.wddDocumentDescription.SelectedValue == "4")
      {
        this.txtNumeroRecabante_FilteredTextBoxExtender.FilterType = FilterTypes.Numbers;
        this.txtNumeroRecabante.MaxLength = 11;
      }
      else
      {
        this.txtNumeroRecabante_FilteredTextBoxExtender.FilterType = FilterTypes.Custom;
        this.txtNumeroRecabante_FilteredTextBoxExtender.ValidChars = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz1234567890";
        this.txtNumeroRecabante.MaxLength = 20;
      }
      this.txtNumeroRecabante.Text = "";
      this.txtNumeroRecabante.Focus();
    }

    protected void rblPersonTypeCollect_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        int result1 = 0;
        int result2 = 0;
        int.TryParse(this.hdTramite.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture), out result1);
        int.TryParse(this.hdTipPer.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture), out result2);
        if (this.rblPersonTypeCollect.SelectedValue == "2")
        {
          this.trNotaria.Style.Add("display", "none");
          this.trPartida.Style.Add("display", "");
          this.LoadRequisiteToDeliver(result1, 3, 2, 1, "", 0);
        }
        else
        {
          this.trNotaria.Style.Add("display", "");
          this.trPartida.Style.Add("display", "");
          this.LoadRequisiteToDeliver(result1, 3, 2, 1, "", 2);
        }
        this.txtName.Text = "";
        this.txtNumeroRecabante.Text = "";
        this.txtNotariaRecabante.Text = "";
        this.txtPartidaRecabante.Text = "";
        this.txtName.Focus();
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

    protected void wibSave_Click(object sender, EventArgs e)
    {
      this.BlockUnblock(false);
      string script = "ShowModalPopUpEx(" + this.hdQuantity.Value + ");";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void wibCancel_Click(object sender, EventArgs e)
    {
      string script = "PopupClose();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private void LoadParameter()
    {
      try
      {
        int result = 0;
        int.TryParse(this.Request.QueryString["intTipPers"].ToString((IFormatProvider) CultureInfo.CurrentCulture), out result);
        this.hdTipPer.Value = result.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.hdTramite.Value = this.Request.QueryString["intTipProc"].ToString((IFormatProvider) CultureInfo.CurrentCulture);
        this.hdQuantity.Value = this.Request.QueryString["intQuantity"].ToString((IFormatProvider) CultureInfo.CurrentCulture);
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) ("" + SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
          (object) "1,4,3,2,32,19,33",
          (object) "",
          (object) ""
        });
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["i_GroupId"].ToString() == SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture))
              this.wddDocumentDescription.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          }
        }
        this.wddDocumentDescription.SelectedValue = "1";
        this.txtNumeroRecabante_FilteredTextBoxExtender.FilterType = FilterTypes.Numbers;
        this.txtNumeroRecabante.MaxLength = 8;
        if (result == 1)
        {
          this.rblPersonTypeCollect.Visible = false;
          if (this.Request.QueryString["intProSame"].ToString((IFormatProvider) CultureInfo.CurrentCulture) == "1")
          {
            this.rblTypeRecabante.Enabled = false;
            this.rblTypeRecabante.SelectedValue = "2";
          }
          else
            this.rblTypeRecabante.SelectedValue = "1";
          this.rblTypeRecabante_SelectedIndexChanged((object) null, (EventArgs) null);
        }
        else
        {
          this.rblTypeRecabante.Visible = false;
          this.rblPersonTypeCollect.SelectedValue = "2";
          this.rblPersonTypeCollect_SelectedIndexChanged((object) null, (EventArgs) null);
          this.trNotaria.Style.Add("display", "none");
          this.trPartida.Style.Add("display", "");
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void LoadRequisiteToDeliver(
      int pintProcessTypeId,
      int pintPersonTypeId,
      int pintCollectTypeId,
      int pintIsVisible,
      string pstrUseCorrespondence,
      int pintRepresentativeCollectTypeId)
    {
      try
      {
        DataTable forExchangeModeBy = new PlateDeliverQueriesBL().GetPlateDeliverRequisiteForExchangeModeBy(pintProcessTypeId, pintPersonTypeId, pintCollectTypeId, pintIsVisible, pstrUseCorrespondence, pintRepresentativeCollectTypeId);
        this.chklRequisiteToDeliver.DataSource = (object) forExchangeModeBy;
        this.chklRequisiteToDeliver.DataTextField = "v_RequisiteObjectName";
        this.chklRequisiteToDeliver.DataValueField = "i_ObjectId";
        this.chklRequisiteToDeliver.DataBind();
        this.ViewState["vsRequisiteForExchangeMode"] = (object) forExchangeModeBy;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void RegisterDeliveryProcess(
      int pintRequirementPlateID,
      int pintProductID,
      int pintPlateOldType,
      int pintTotBlank)
    {
      try
      {
        int num1 = pintRequirementPlateID;
        string empty = string.Empty;
        string str1 = string.Empty;
        string str2 = string.Empty;
        foreach (DataRow row in (InternalDataCollectionBase) new PlateDeliverQueriesBL().GetPlateDeliverProductConpositionBy(pintProductID).Rows)
          str1 = str1 + row["i_ComponentId"].ToString() + " / ";
        if (pintPlateOldType == 2 || pintPlateOldType == 1)
        {
          if (pintTotBlank == 2)
            empty += "1 / ";
          else if (pintTotBlank == 1)
            empty += "2 / ";
        }
        foreach (ListItem listItem in this.chklRequisiteToDeliver.Items)
        {
          if (listItem.Selected)
            str2 = str2 + listItem.Value + " / ";
        }
        if ((pintPlateOldType == 2 || pintPlateOldType == 1) && pintTotBlank == 2)
          str2 += "14 / 15";
        int? nullable1 = new int?();
        int? nullable2 = new int?();
        int? nullable3 = new int?();
        enmOwnerPersonType int32 = (enmOwnerPersonType) Convert.ToInt32(this.hdTipPer.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        string text1 = this.txtName.Text;
        nullable1 = new int?(Convert.ToInt32(this.wddDocumentDescription.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
        string text2 = this.txtNumeroRecabante.Text;
        int? nullable4 = int32 == enmOwnerPersonType.Natural ? new int?(1) : new int?(2);
        if (int32 == enmOwnerPersonType.Natural)
        {
          nullable2 = new int?(Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
        }
        else
        {
          nullable2 = new int?(Convert.ToInt32(this.rblPersonTypeCollect.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
          if (nullable2.GetValueOrDefault() == 1)
            nullable2 = new int?(2);
        }
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - PlateDeliveryMassiveClose.aspx");
        int iLocationId = systemUser.i_LocationId;
        int num2 = systemUser.i_CompanyId.Value;
        int iSystemUserId = systemUser.i_SystemUserId;
        DateTime? nullable5 = new DateTime?();
        DateTime? nullable6 = new DateTime?();
        string str3 = (string) null;
        string str4 = (string) null;
        new PlateDeliverManagementBL().PlateDeliverDataInsert(new PlateDeliverData()
        {
          v_ExchangeModeId = empty,
          v_ReferenceNumber = (string) null,
          d_ComplaintDate = new DateTime?(),
          v_ComplaintDependency = (string) null,
          v_ComplaintPartNumber = (string) null,
          v_CollectName = text1,
          i_CollectDocumentType = nullable1,
          v_CollectDocumentNumber = text2,
          v_CollectNotaryName = this.txtNotariaRecabante.Text == string.Empty ? (string) null : this.txtNotariaRecabante.Text,
          v_CollectDeparture = this.txtPartidaRecabante.Text == string.Empty ? (string) null : this.txtPartidaRecabante.Text,
          v_DocumentsRequiredId = str2,
          i_RequirementPlateId = new int?(num1),
          v_ProductCompositionId = str1,
          i_CollectTypeId = nullable2,
          i_CollectPersonTypeId = nullable4,
          i_RepresentativeCollectTypeId = !this.rblPersonTypeCollect.Visible ? (this.rblTypeRecabante.SelectedValue == "2" ? new int?(1) : new int?()) : new int?(Convert.ToInt32(this.rblPersonTypeCollect.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture)),
          i_InsertUserId = new int?(iSystemUserId),
          d_ConsMTCDate = nullable5,
          d_TachaSUNARPDate = nullable6,
          v_ReferenceNumberConsMTC = str3,
          v_ReferenceNumberTachaSUNARP = str4,
          v_PlateTacha = (string) null
        }, 0, 1);
        this.InterfaceWithWarehouse(pintRequirementPlateID, pintPlateOldType, pintTotBlank);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void InterfaceWithWarehouse(
      int pintRequirementPlateID,
      int pintPlateOldType,
      int pintTotBlank)
    {
      try
      {
        int int32_1 = Convert.ToInt32(pintRequirementPlateID);
        DataTable dataTable1 = this.ViewState["vsRequisiteForExchangeMode"] as DataTable;
        DataTable dataTable2 = this.ViewState["vsdtPlateDeliverItem"] as DataTable;
        SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - ProductStockGeneralReport.aspx");
        int iLocationId = systemUser.i_LocationId;
        Convert.ToInt32((object) systemUser.i_CompanyId, (IFormatProvider) CultureInfo.CurrentCulture);
        string vAlias = systemUser.v_Alias;
        int iSystemUserId = systemUser.i_SystemUserId;
        int int32_2 = Convert.ToInt32(new WarehouseQueriesBL().GetWarehouseBy(0, "", iLocationId, 10).Rows[0]["i_WarehouseId"], (IFormatProvider) CultureInfo.CurrentCulture);
        string str = string.Empty;
        DataTable dataTable3 = new DTStockMovementDetail().DataTableStockMovementDetail();
        foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
        {
          if (Convert.ToInt32(row["b_IsRequiered"], (IFormatProvider) CultureInfo.CurrentCulture) == 1)
            str = str + row["i_ObjectId"]?.ToString() + "/";
        }
        if ((pintPlateOldType == 2 || pintPlateOldType == 1) && pintTotBlank == 2)
          str += "14/15";
        string[] strArray = str.Split('/');
        for (int index = 0; index < strArray.Length; ++index)
        {
          if (strArray[index] != string.Empty)
          {
            DataRowCollection rows = dataTable3.Rows;
            object[] objArray = new object[22];
            objArray[3] = (object) strArray[index];
            objArray[6] = (object) 1;
            objArray[8] = (object) int32_1;
            objArray[17] = (object) strArray[index];
            objArray[20] = (object) dataTable2.Rows[0]["v_PlateNew"].ToString();
            rows.Add(objArray);
          }
        }
        if (dataTable3.Rows.Count > 0)
        {
          StockMovementManagementBL movementManagementBl = new StockMovementManagementBL();
          StockMovement pobjStockMovement = new StockMovement();
          pobjStockMovement.i_WarehouseId = new int?(int32_2);
          pobjStockMovement.i_MotiveMovementId = new int?(14);
          pobjStockMovement.i_SupplierId = new int?();
          pobjStockMovement.i_DocumentTypeId = new int?();
          pobjStockMovement.v_DocumentNumber = (string) null;
          pobjStockMovement.i_UserId = new int?(iSystemUserId);
          pobjStockMovement.b_Checked = new bool?(false);
          pobjStockMovement.v_Observation = string.Empty;
          pobjStockMovement.d_InsertDate = new DateTime?(DateTime.Now);
          pobjStockMovement.i_ProductionOrderId = new int?();
          DataTable pdtStockMovementDetail = dataTable3;
          movementManagementBl.StockMovementInsertInterchangeObject(pobjStockMovement, pdtStockMovementDetail);
        }
        dataTable3.Rows.Clear();
        int int32_3 = Convert.ToInt32(new WarehouseQueriesBL().GetWarehouseBy(0, "", iLocationId, 1).Rows[0]["i_WarehouseId"], (IFormatProvider) CultureInfo.CurrentCulture);
        int num = 49;
        int? nullable1 = dataTable2.Rows[0]["i_StockMovementDetailId"] is DBNull ? new int?() : new int?(Convert.ToInt32(dataTable2.Rows[0]["i_StockMovementDetailId"], (IFormatProvider) CultureInfo.CurrentCulture));
        int? nullable2 = dataTable2.Rows[0]["i_StockMovementId"] is DBNull ? new int?() : new int?(Convert.ToInt32(dataTable2.Rows[0]["i_StockMovementId"], (IFormatProvider) CultureInfo.CurrentCulture));
        int? nullable3 = dataTable2.Rows[0]["i_LocationWarehouseId"] is DBNull ? new int?() : new int?(Convert.ToInt32(dataTable2.Rows[0]["i_LocationWarehouseId"], (IFormatProvider) CultureInfo.CurrentCulture));
        int? nullable4 = dataTable2.Rows[0]["i_ProductId"] is DBNull ? new int?() : new int?(Convert.ToInt32(dataTable2.Rows[0]["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture));
        DataRowCollection rows1 = dataTable3.Rows;
        object[] objArray1 = new object[22];
        objArray1[0] = (object) nullable1;
        objArray1[1] = (object) nullable2;
        objArray1[2] = (object) nullable3;
        objArray1[3] = (object) nullable4;
        objArray1[6] = (object) 1;
        objArray1[8] = (object) int32_1;
        objArray1[20] = (object) dataTable2.Rows[0]["v_PlateNew"].ToString();
        rows1.Add(objArray1);
        StockMovementManagementBL movementManagementBl1 = new StockMovementManagementBL();
        StockMovement pobjStockMovement1 = new StockMovement();
        pobjStockMovement1.i_WarehouseId = new int?(int32_3);
        pobjStockMovement1.i_MotiveMovementId = new int?(num);
        pobjStockMovement1.i_SupplierId = new int?();
        pobjStockMovement1.i_DocumentTypeId = new int?();
        pobjStockMovement1.v_DocumentNumber = (string) null;
        pobjStockMovement1.i_UserId = new int?(iSystemUserId);
        pobjStockMovement1.b_Checked = new bool?(false);
        pobjStockMovement1.v_Observation = string.Empty;
        pobjStockMovement1.d_InsertDate = new DateTime?(DateTime.Now);
        pobjStockMovement1.i_ProductionOrderId = new int?();
        DataTable pdtStockMovementDetail1 = dataTable3;
        movementManagementBl1.StockMovementInsertDeliberyPlate(pobjStockMovement1, pdtStockMovementDetail1);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void UpdateOtherProcess(int pintRequirementPlateID)
    {
      try
      {
        int num = this.Session["SystemUser"] != null ? Convert.ToInt32(((SystemUser) this.Session["SystemUser"]).i_SystemUserId) : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - PlateDeliverMassiveClose.aspx");
        DataTable dataTable = this.ViewState["vsdtPlateDeliverItem"] as DataTable;
        string[] platePreviousOld = new VehicleRegistrationQueriesBL().GetPlatePreviousOld(new VehicleRegistration()
        {
          v_PlateCurrent = dataTable.Rows[0]["v_PlateNew"].ToString(),
          v_PlateQuestion = dataTable.Rows[0]["v_PlateOld"].ToString(),
          v_SerialNumber = dataTable.Rows[0]["v_SerialNumber"].ToString(),
          v_TitleNumber = dataTable.Rows[0]["v_TitleNumber"].ToString(),
          v_PlateOld = string.Empty,
          v_PlatePrevious = string.Empty
        });
        new VehicleRegistrationManagementBL().VehicleRegistrationInsert(new VehicleRegistration()
        {
          i_VehicleCategoryId = dataTable.Rows[0]["i_VehicleCategoryId"] is DBNull ? new int?() : (int?) dataTable.Rows[0]["i_VehicleCategoryId"],
          i_VehicleUseId = dataTable.Rows[0]["i_VehicleTypeUseId"] is DBNull ? new int?() : (int?) dataTable.Rows[0]["i_VehicleTypeUseId"],
          v_PlateNew = dataTable.Rows[0]["v_PlateNew"].ToString(),
          v_PlateOld = platePreviousOld[0],
          v_PlatePrevious = platePreviousOld[1],
          v_TitleNumber = dataTable.Rows[0]["v_TitleNumber"].ToString(),
          v_Brand = dataTable.Rows[0]["v_Brand"].ToString(),
          v_Model = dataTable.Rows[0]["v_Model"].ToString(),
          v_SerialNumber = dataTable.Rows[0]["v_SerialNumber"].ToString(),
          v_BlankCode1 = dataTable.Rows[0]["v_BlankCode1"].ToString(),
          v_BlankCode2 = dataTable.Rows[0]["v_BlankCode2"].ToString(),
          v_RFIDCode = dataTable.Rows[0]["v_RFIDCode"].ToString(),
          v_TIDCode = dataTable.Rows[0]["v_TIDCode"].ToString(),
          v_EPCCode = dataTable.Rows[0]["v_EPCCode"].ToString(),
          v_OptimalNumberCode = dataTable.Rows[0]["v_OptimalNumberCode"].ToString(),
          v_OwnerCompleteName = dataTable.Rows[0]["v_OwnerCompleteName"].ToString(),
          v_OwnerDocumentType = dataTable.Rows[0]["v_OwnerDocumentType"].ToString(),
          v_OwnerDocumentDescription = dataTable.Rows[0]["v_OwnerDocumentDescription"].ToString(),
          v_OwnerDocumentNumber = dataTable.Rows[0]["v_OwnerDocumentNumber"].ToString(),
          i_Status = new int?(1),
          i_InsertUserId = new int?(num),
          d_InsertDate = new DateTime?(DateTime.Now),
          i_UpdateUserId = new int?(num),
          d_UpdateDate = new DateTime?(DateTime.Now),
          i_UpdateAll = true
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SendInfoProductPopupClose()
    {
      try
      {
        string script = "RefreshParent();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void IsValidInputDataCollect()
    {
      try
      {
        string empty = string.Empty;
        Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        enmTypeCollect int32 = (enmTypeCollect) Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        switch ((enmOwnerPersonType) Convert.ToInt32(this.hdTipPer.Value, (IFormatProvider) CultureInfo.CurrentCulture))
        {
          case enmOwnerPersonType.Natural:
            switch (int32)
            {
              case enmTypeCollect.Propietario:
                if (this.txtName.Text == string.Empty)
                  empty += "<br> Debe ingresar Nombre del propietario.";
                if (this.wddDocumentDescription.SelectedValue == "-1")
                  empty += "<br> Debe seleccionar Tipo de documento.";
                if (this.txtNumeroRecabante.Text == string.Empty)
                {
                  empty += "<br> Debe ingresar Nro documento.";
                  break;
                }
                break;
              case enmTypeCollect.Apoderado:
                if (this.txtName.Text == string.Empty)
                  empty += "<br> Debe ingresar Nombre del apoderado.";
                if (this.wddDocumentDescription.SelectedValue == "-1")
                  empty += "<br> Debe seleccionar Tipo de documento.";
                if (this.txtNumeroRecabante.Text == string.Empty)
                  empty += "<br> Debe ingresar Nro documento.";
                if (this.txtNotariaRecabante.Text == string.Empty)
                  empty += "<br> Debe ingresar Notaria.";
                break;
            }
            break;
          case enmOwnerPersonType.Juridica:
            switch ((enmPersonTypecollect) Convert.ToInt32(this.rblPersonTypeCollect.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
            {
              case enmPersonTypecollect.Otros:
                if (this.txtName.Text == string.Empty)
                  empty += "<br> Debe ingresar Nombre del apoderado.";
                if (this.wddDocumentDescription.SelectedValue == "-1")
                  empty += "<br> Debe seleccionar Tipo de documento.";
                if (this.txtNumeroRecabante.Text == string.Empty)
                  empty += "<br> Debe ingresar Nro documento.";
                if (this.txtNotariaRecabante.Text == string.Empty)
                  empty += "<br> Debe ingresar Notaria.";
                if (this.txtPartidaRecabante.Text == string.Empty)
                  empty += "<br> Debe ingresar Partida.";
                break;
              case enmPersonTypecollect.RepresentanteLegal:
                if (this.txtName.Text == string.Empty)
                  empty += "<br> Debe ingresar Nombre del representante legal.";
                if (this.wddDocumentDescription.SelectedValue == "-1")
                  empty += "<br> Debe seleccionar Tipo de documento.";
                if (this.txtNumeroRecabante.Text == string.Empty)
                  empty += "<br> Debe ingresar Nro documento.";
                if (this.txtPartidaRecabante.Text == string.Empty)
                {
                  empty += "<br> Debe ingresar Partida.";
                  break;
                }
                break;
            }
            break;
        }
        if (empty != string.Empty)
          throw new HandledException(1, "Datos del Recabante : <br>______________________________________" + empty);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void IsValidRequiredDocuments(int pintProcessTypeId, int pintOwnerPersonType)
    {
      try
      {
        int int32_1 = Convert.ToInt32(this.rblTypeRecabante.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture);
        enmOwnerPersonType int32_2 = (enmOwnerPersonType) Convert.ToInt32(this.hdTipPer.Value, (IFormatProvider) CultureInfo.CurrentCulture);
        switch (pintOwnerPersonType)
        {
          case 1:
            if (int32_1 == 1)
            {
              if (this.chklRequisiteToDeliver.Items[0].Selected)
                break;
              throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de la Orden de Giro (OG)/Boleta Informativa.");
            }
            if (!this.chklRequisiteToDeliver.Items[0].Selected)
              throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de la Orden de Giro (OG)/Boleta Informativa.");
            if (!this.chklRequisiteToDeliver.Items[1].Selected)
              throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Carta Poder Juridica Legalizada.");
            break;
          case 2:
            switch ((enmPersonTypecollect) Convert.ToInt32(this.rblPersonTypeCollect.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture))
            {
              case enmPersonTypecollect.Otros:
                if (!this.chklRequisiteToDeliver.Items[0].Selected)
                  throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de la Orden de Giro (OG)/Boleta Informativa.");
                if (!this.chklRequisiteToDeliver.Items[1].Selected)
                  throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Carta Poder Juridica Legalizada.");
                if (!this.chklRequisiteToDeliver.Items[2].Selected)
                  throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Vigencia de Poder (Copia).");
                if (!this.chklRequisiteToDeliver.Items[3].Selected)
                  throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Vigencia de Poder (Original).");
                break;
              case enmPersonTypecollect.RepresentanteLegal:
                if (!this.chklRequisiteToDeliver.Items[0].Selected)
                  throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de la Orden de Giro (OG)/Boleta Informativa.");
                if (!this.chklRequisiteToDeliver.Items[1].Selected)
                  throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Vigencia de Poder (Copia).");
                if (!this.chklRequisiteToDeliver.Items[2].Selected)
                  throw new HandledException(1, "Documentos Requeridos <br>____________________<br> Por favor confirme recepción de Vigencia de Poder (Original).");
                break;
            }
            break;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private bool ValidateRuc(string rucAValidar)
    {
      int num = 11 - (int.Parse(rucAValidar.Substring(0, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 5 + int.Parse(rucAValidar.Substring(1, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 4 + int.Parse(rucAValidar.Substring(2, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 3 + int.Parse(rucAValidar.Substring(3, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 2 + int.Parse(rucAValidar.Substring(4, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 7 + int.Parse(rucAValidar.Substring(5, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 6 + int.Parse(rucAValidar.Substring(6, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 5 + int.Parse(rucAValidar.Substring(7, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 4 + int.Parse(rucAValidar.Substring(8, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 3 + int.Parse(rucAValidar.Substring(9, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 2) % 11;
      return (int.Parse(rucAValidar.Length.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture) != 11 ? 10 : int.Parse(rucAValidar.Substring(10, 1), (IFormatProvider) CultureInfo.CurrentCulture)) == (num != 10 ? 0 : 0) + (num != 11 ? 0 : 1) + (num >= 10 ? 0 : num);
    }

    protected void rblTypeRecabante_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        int result1 = 0;
        int result2 = 0;
        int.TryParse(this.hdTramite.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture), out result1);
        int.TryParse(this.hdTipPer.Value.ToString((IFormatProvider) CultureInfo.CurrentCulture), out result2);
        if (this.rblTypeRecabante.SelectedIndex == 0)
        {
          this.trNotaria.Style.Add("display", "none");
          this.trPartida.Style.Add("display", "none");
          this.LoadRequisiteToDeliver(result1, result2, 0, 1, "", 0);
        }
        else
        {
          this.trNotaria.Style.Add("display", "");
          this.trPartida.Style.Add("display", "none");
          this.LoadRequisiteToDeliver(result1, result2, 2, 1, "", 0);
        }
        this.txtName.Text = "";
        this.txtNumeroRecabante.Text = "";
        this.txtNotariaRecabante.Text = "";
        this.txtPartidaRecabante.Text = "";
        this.txtName.Focus();
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

    private void BlockUnblock(bool isEnabled)
    {
      if (this.Request.QueryString["intProSame"].ToString((IFormatProvider) CultureInfo.CurrentCulture) != "1")
        this.rblTypeRecabante.Enabled = isEnabled;
      this.rblPersonTypeCollect.Enabled = isEnabled;
      this.wibSave.Enabled = isEnabled;
      this.wibCancel.Enabled = isEnabled;
      this.txtName.Enabled = isEnabled;
      this.wddDocumentDescription.Enabled = isEnabled;
      this.txtNumeroRecabante.Enabled = isEnabled;
      this.txtPartidaRecabante.Enabled = isEnabled;
      this.txtNotariaRecabante.Enabled = isEnabled;
      this.ImgLoading2.Visible = !isEnabled;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.txtName.Text.Trim() != "" && this.txtName.Text.Trim().Length <= 3)
          throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_Ingrese_Nombre_Razon);
        if (this.txtName.Text.Trim() != "" && this.txtNumeroRecabante.Text.Trim() != "")
        {
          if (this.wddDocumentDescription.SelectedValue == "1" && this.txtNumeroRecabante.Text.Trim().Length != 8)
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_DNI_8);
          if (this.wddDocumentDescription.SelectedValue == "4" && this.txtNumeroRecabante.Text.Trim().Length != 11)
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_11);
          if (this.wddDocumentDescription.SelectedValue == "4" && this.txtNumeroRecabante.Text.Trim().Substring(0, 1) != "1" && this.txtNumeroRecabante.Text.Trim().Substring(0, 1) != "2")
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_Longitud);
          if (this.wddDocumentDescription.SelectedValue == "4" && !this.ValidateRuc(this.txtNumeroRecabante.Text.Trim()))
            throw new HandledException(1, SIIV.SystemParameter.BL.Constants.REQUIREMENT_ADVERTENCIA_RUC_Formato);
        }
        this.IsValidInputDataCollect();
        this.IsValidRequiredDocuments(Convert.ToInt32(this.hdTramite.Value, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToInt32(this.hdTipPer.Value, (IFormatProvider) CultureInfo.CurrentCulture));
        using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
        {
          Timeout = new TimeSpan(0, 0, 5, 1)
        }))
        {
          foreach (DataRow row in (InternalDataCollectionBase) (this.Session["dtDeliveryControlOperation"] as DataTable).Rows)
          {
            int int32_1 = Convert.ToInt32(row["i_RequirementPlateId"], (IFormatProvider) CultureInfo.CurrentCulture);
            int int32_2 = Convert.ToInt32(row["i_ProductId"], (IFormatProvider) CultureInfo.CurrentCulture);
            int int32_3 = Convert.ToInt32(row["i_PlateOldType"], (IFormatProvider) CultureInfo.CurrentCulture);
            int int32_4 = Convert.ToInt32(row["i_TotBlank"], (IFormatProvider) CultureInfo.CurrentCulture);
            DataTable plateDeliverItemBy = new PlateDeliverQueriesBL().GetPlateDeliverItemBy("", int32_1);
            this.ViewState["vsdtPlateDeliverItem"] = (object) plateDeliverItemBy;
            if (plateDeliverItemBy != null)
            {
              if (plateDeliverItemBy.Rows.Count <= 0)
                throw new HandledException(1, string.Format("No se pudo realizar la entrega de la solicitud {0}", (object) int32_1));
              this.RegisterDeliveryProcess(int32_1, int32_2, int32_3, int32_4);
            }
          }
          transactionScope.Complete();
        }
        this.SendInfoProductPopupClose();
      }
      catch (TransactionAbortedException ex)
      {
        this.BlockUnblock(true);
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex.Message));
      }
      catch (HandledException ex)
      {
        this.BlockUnblock(true);
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        this.BlockUnblock(true);
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void Button2_Click(object sender, EventArgs e) => this.BlockUnblock(true);
  }
}
