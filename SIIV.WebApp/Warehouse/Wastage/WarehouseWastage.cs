// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Wastage.WarehouseWastage
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using System;
using System.Data;
using System.Globalization;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Wastage
{
  public class WarehouseWastage : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected Fecha wdpFecIni;
    protected Fecha wdpFecFin;
    protected HtmlGenericControl lblAutor;
    protected DropDownList cboUserRecepction;
    protected TextBox txt_BatchId;
    protected Button btnSearch;
    protected GridView wdgWarehouseWastage;
    protected CheckBox chkAccept;
    protected Button btnAcept;
    protected Label lblMsg;

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
      int intLocationId = 0;
      int num = 0;
      if (systemUser != null)
      {
        intLocationId = systemUser.i_LocationId;
        num = systemUser.i_SystemUserId;
      }
      DateTime dateTime1 = Convert.ToDateTime((object) this.wdpFecIni.Value, (IFormatProvider) CultureInfo.CurrentCulture);
      DateTime dateTime2 = Convert.ToDateTime((object) this.wdpFecFin.Value, (IFormatProvider) CultureInfo.CurrentCulture);
      int result;
      int.TryParse(this.cboUserRecepction.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), out result);
      this.wdgWarehouseWastage.DataSource = (object) new WarehouseControlQueriesBL().GetWarehouseWastageClaim(intLocationId, dateTime1.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture), dateTime2.ToString("yyyyMMdd", (IFormatProvider) CultureInfo.CurrentCulture), result, this.txt_BatchId.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(this.txt_BatchId.Text, (IFormatProvider) CultureInfo.CurrentCulture));
      this.wdgWarehouseWastage.DataBind();
      this.chkAccept.Checked = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMsg.Visible = false;
      if (this.Page.IsPostBack)
        return;
      this.wdpFecIni.Value = DateTime.Now;
      this.wdpFecFin.Value = DateTime.Now;
      SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
      string str = string.Empty;
      int intLocationId = 0;
      if (systemUser != null)
      {
        str = systemUser.v_FirstName + " " + systemUser.v_LastName;
        intLocationId = systemUser.i_LocationId;
      }
      this.lblAutor.InnerText = str;
      DataTable userReceptionClaim = new WarehouseControlQueriesBL().GetSystemUserReceptionClaim(intLocationId);
      if (userReceptionClaim != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) userReceptionClaim.Rows)
          this.cboUserRecepction.Items.Add(new ListItem(row["v_Alias"].ToString(), row["i_SystemUserId"].ToString()));
      }
      this.cboUserRecepction.Items.Insert(0, new ListItem("- Todos - ", "0"));
      this.cboUserRecepction.SelectedIndex = 0;
    }

    protected void wdgWarehouseWastage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void btnAcept_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.chkAccept.Checked)
        {
          SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
          int iSystemUserId = systemUser.i_SystemUserId;
          int iLocationId = systemUser.i_LocationId;
          DataTable dataTable1 = new DTStockMovementDetail().DataTableStockMovementDetail();
          DataTable dataTable2 = new DataTable();
          int num1 = -1;
          foreach (GridViewRow row in this.wdgWarehouseWastage.Rows)
          {
            CheckBox control = (CheckBox) row.FindControl("chkCheck");
            if (control != null)
            {
              num1 = Convert.ToInt32(row.Cells[2].Text, (IFormatProvider) CultureInfo.CurrentCulture);
              if (control.Checked)
              {
                DataRowCollection rows = dataTable1.Rows;
                object[] objArray = new object[22];
                objArray[3] = (object) Convert.ToInt32(this.wdgWarehouseWastage.DataKeys[row.RowIndex]["i_ProductId"].ToString());
                objArray[6] = (object) 1;
                objArray[7] = (object) row.Cells[4].Text;
                objArray[8] = (object) row.Cells[2].Text;
                objArray[20] = (object) row.Cells[3].Text;
                rows.Add(objArray);
              }
            }
          }
          DataTable correspondenceRead = new WarehouseQueriesBL().GetWarehouseCorrespondenceRead(iLocationId, 3);
          int num2 = 101;
          int int32 = Convert.ToInt32(correspondenceRead.Rows[0]["i_WarehouseIdCorrespondence"], (IFormatProvider) CultureInfo.CurrentCulture);
          if (dataTable1.Rows.Count > 0)
          {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
            {
              Timeout = new TimeSpan(0, 0, 5, 1)
            }))
            {
              StockMovementManagementBL movementManagementBl = new StockMovementManagementBL();
              StockMovement pobjStockMovement = new StockMovement();
              pobjStockMovement.i_WarehouseId = new int?(int32);
              pobjStockMovement.i_MotiveMovementId = new int?(num2);
              pobjStockMovement.i_UserId = new int?(iSystemUserId);
              pobjStockMovement.b_Checked = new bool?(false);
              pobjStockMovement.v_Observation = "Registro Automatico. Desecho por Reclamo.";
              pobjStockMovement.d_InsertDate = new DateTime?(DateTime.Now);
              pobjStockMovement.i_ProductionOrderId = new int?();
              DataTable pdtStockMovementDetail = dataTable1;
              dataTable2 = movementManagementBl.StockMovementInsertWaste(pobjStockMovement, pdtStockMovementDetail);
              transactionScope.Complete();
              Message.SetMessage(this.lblMsg, enmMessageType.Success, "Exito*****Operacion concretada con exito");
            }
            this.btnSearch_Click((object) null, (EventArgs) null);
          }
          else
            Message.SetMessage(this.lblMsg, enmMessageType.Warning, "Advertencia*****No ha seleccionado ningun Kit.");
        }
        else
          Message.SetMessage(this.lblMsg, enmMessageType.Warning, "Advertencia*****Debe Confirmar la Información.");
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, enmMessageType.Error, "Error*****<br>" + ex.Message);
      }
    }
  }
}
