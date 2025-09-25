// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.SSS.Production.ProductionPortPlate
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using BarcodeLib;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.SSS.BL;
using SIIV.Warehouse.BL;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.SSS.Production
{
  public class ProductionPortPlate : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected Fecha wdpDateIni;
    protected Fecha wdpDateFin;
    protected Button wibSearch;
    protected GridView wdgPortPlateList;
    protected Pager custPagerPPP;
    protected Button wibNew;
    protected HiddenField HiddenField1;
    protected HiddenField HiddenField2;
    protected HiddenField HiddenField3;
    protected Label lblMessage;
    protected Button btnReturnPopupConfirmation;
    protected Button Button2;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      this.lblMessage.Text = "";
      if (this.Page.IsPostBack)
        return;
      this.SetDatePicker();
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchPP();

    protected void wdgPortPlateList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        GridViewRow row1 = this.wdgPortPlateList.Rows[Convert.ToInt32(e.CommandArgument)];
        int int32 = Convert.ToInt32(row1.Cells[0].Text);
        StockMovementManagementBL movementManagementBl = new StockMovementManagementBL();
        StockMovement pobjStockMovement = this.ObjStockMovement(row1);
        SSSQueriesBL sssQueriesBl = new SSSQueriesBL();
        SystemUser systemUser = new SystemUser();
        if (e.CommandName.Equals("Send", StringComparison.CurrentCulture))
        {
          if (this.HiddenField1.Value == "1")
            return;
          DataTable dataTable1 = new DataTable();
          DataTable dataTable2 = new DTStockMovementDetail().DataTableStockMovementDetail();
          DataTable byId = sssQueriesBl.ProductPortPlateDetailGetById(int32);
          if (Convert.ToInt32(byId.Rows[0]["i_ProductionStatus"].ToString()) != 0)
          {
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Error, "la solicitud ya se encuentra en serigrafiado.");
            return;
          }
          foreach (DataRow row2 in (InternalDataCollectionBase) byId.Rows)
          {
            DataRowCollection rows = dataTable2.Rows;
            object[] objArray = new object[22];
            objArray[1] = (object) 1;
            objArray[3] = (object) 27;
            objArray[4] = (object) 1;
            objArray[6] = (object) Convert.ToInt32(row2["i_QuantityT"]);
            objArray[7] = row2["v_Description"];
            objArray[10] = (object) 1;
            rows.Add(objArray);
          }
          int i_StockMovementIdIn = 0;
          int i_StockMovementIdOut;
          movementManagementBl.StockMovementInsertFactory(pobjStockMovement, dataTable2, i_StockMovementIdIn, out i_StockMovementIdOut);
          if (i_StockMovementIdOut > 0)
          {
            if (!sssQueriesBl.ProductPortPlateUpdateStockId(int32, i_StockMovementIdOut, dataTable2, 1, Convert.ToInt32(systemUser.i_SystemUserId)))
            {
              SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Error, "Error al enviar a serigrafiar");
              return;
            }
            this.SearchPP();
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Success, "Envio a serigrafiado realizado correctamente.");
          }
        }
        if (e.CommandName.Equals("Report", StringComparison.CurrentCulture))
        {
          this.ViewState["Id_ProductioPorta"] = (object) int32;
          string script = "Exportpdf();";
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
          this.HidePopup();
        }
        if (this.HiddenField2.Value == "1")
          return;
        if (e.CommandName.Equals("Refund", StringComparison.CurrentCulture))
        {
          DataTable dataTable = new DataTable();
          DataTable byId = sssQueriesBl.ProductPortPlateDetailGetById(int32);
          if (Convert.ToInt32(byId.Rows[0]["i_ProductionStatus"].ToString()) != 31)
          {
            if (Convert.ToInt32(byId.Rows[0]["i_ProductionStatus"].ToString()) == 1 || Convert.ToInt32(byId.Rows[0]["i_ProductionStatus"].ToString()) == 21)
            {
              string empty = string.Empty;
              this.CreatePopUpServer("Registro de Ingreso a Almacén", "ProductionPortPlateRefund.aspx?Id_ProductioPorta=" + int32.ToString(), "550px", "550px");
            }
            else
            {
              SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Error, "No puede realizar el retorno, Verificar el estado de la solicitud");
              return;
            }
          }
          else
          {
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Error, "No puede realizar el retorno, Producto ya ha sido retornado totalmente");
            return;
          }
        }
        if (this.HiddenField3.Value == "1" || !e.CommandName.Equals("Sell", StringComparison.CurrentCulture))
          return;
        DataTable dataTable3 = new DataTable();
        DataTable byId1 = sssQueriesBl.ProductPortPlateDetailGetById(int32);
        if (Convert.ToInt32(byId1.Rows[0]["i_ProductionStatus"].ToString()) == 2 || Convert.ToInt32(byId1.Rows[0]["i_ProductionStatus"].ToString()) == 31)
        {
          string empty = string.Empty;
          this.CreatePopUpServer("Registro de Salida por Venta AAP", "ProductionPortPlateSell.aspx?Id_ProductioPorta=" + int32.ToString(), "550px", "580px");
        }
        else
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Error, "No puede realizar la venta, Verificar el estado de la solicitud");
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Error, "Error*****<br>" + ex.Message);
      }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
      try
      {
        SSSQueriesBL sssQueriesBl = new SSSQueriesBL();
        DataTable dataTable1 = new DataTable();
        int int32 = Convert.ToInt32(this.ViewState["Id_ProductioPorta"].ToString());
        DataTable dataTable2 = sssQueriesBl.ProductPortPlateReport(int32, 1);
        if (dataTable2.Rows.Count > 0)
        {
          if (Convert.ToInt32(dataTable2.Rows[0]["i_StockMovementId"].ToString()) == 0)
          {
            SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se puede mostrar el reporte, esta solicitud aun no se envia a serigrafiar");
          }
          else
          {
            using (ReportDocument reportDocument = new ReportDocument())
            {
              string filename = this.Server.MapPath("../Reports/ReportPortPlateProduction.rpt");
              reportDocument.Load(filename);
              dataTable2.Columns.Add(new DataColumn()
              {
                ColumnName = "b_Image",
                DataType = typeof (byte[])
              });
              byte[] numArray = this.ImagenBarCode(int32.ToString());
              dataTable2.Rows[0]["b_Image"] = (object) numArray;
              reportDocument.SetDataSource(dataTable2);
              reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "Reporte de Produccion Porta Placa");
              reportDocument.Close();
              ((Component) reportDocument).Dispose();
              dataTable2.Dispose();
            }
          }
        }
        else
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se puede mostrar el reporte, esta solicitud aun no se envia a serigrafiar");
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Error, "Error al exportar a PDF.");
      }
    }

    protected void wibNew_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.CreatePopUpServer("Registro de PortaPlacas", "ProductionPortPlateAdd.aspx", "480px", "550px");
    }

    protected void custPagerPPP_PageChanged(object sender, CustomPageChangeArgs e)
    {
      this.SearchPPList(this.wdpDateIni.Text, this.wdpDateFin.Text, false);
    }

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e) => this.SearchPP();

    private void SetDatePicker()
    {
      this.wdpDateIni.Value = DateTime.Now;
      this.wdpDateFin.Value = DateTime.Now;
    }

    private void SearchPP()
    {
      this.lblMessage.Visible = false;
      try
      {
        this.SearchPPList(this.wdpDateIni.Text, this.wdpDateFin.Text, true);
        this.HidePopup();
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, ex.Message);
        this.HidePopup();
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SearchPPList(string dFecIni, string dFecFin, bool pboolLoadPager)
    {
      try
      {
        int pintstartRowIndex = pboolLoadPager ? 1 : this.custPagerPPP.CurrentPageNumber;
        int pintmaxRows = this.custPagerPPP.CurrentPageSize == 0 ? 10 : this.custPagerPPP.CurrentPageSize;
        int pinttotalRows;
        DataTable dataTable = new SSSQueriesBL().SearchProductPortPlate(dFecIni, dFecFin, pintstartRowIndex, pintmaxRows, out pinttotalRows);
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
          SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
          this.HidePopup();
        }
        else
          this.lblMessage.Visible = false;
        int num = pinttotalRows;
        this.wdgPortPlateList.DataSource = (object) dataTable;
        this.wdgPortPlateList.DataBind();
        this.custPagerPPP.TotalPages = num % pintmaxRows == 0 ? num / pintmaxRows : num / pintmaxRows + 1;
        this.custPagerPPP.TotalRecordCount = pinttotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerPPP.LoadPager();
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, enmMessageType.Error, "Error... Consulte con el Administrador. " + ex.Message);
      }
    }

    private byte[] ImagenBarCode(string Id_Production)
    {
      if (!(Id_Production.Trim() != ""))
        return (byte[]) null;
      Barcode barcode = new Barcode();
      AlignmentPositions alignmentPositions = AlignmentPositions.CENTER;
      MemoryStream memoryStream = new MemoryStream();
      int int32_1 = Convert.ToInt32(300);
      int int32_2 = Convert.ToInt32(150);
      TYPE type = TYPE.CODE128;
      if (type != 0)
      {
        barcode.IncludeLabel = false;
        barcode.Alignment = alignmentPositions;
        barcode.Encode(type, Id_Production, Color.Black, Color.White, int32_1, int32_2);
        SaveTypes saveTypes = SaveTypes.JPG;
        barcode.SaveImage((Stream) memoryStream, saveTypes);
      }
      byte[] numArray = new byte[memoryStream.Length];
      return memoryStream.GetBuffer();
    }

    private StockMovement ObjStockMovement(GridViewRow _selectedrow)
    {
      StockMovement stockMovement = new StockMovement();
      SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
      stockMovement.i_WarehouseId = new int?(38);
      stockMovement.i_MotiveMovementId = new int?(55);
      stockMovement.i_SupplierId = new int?();
      stockMovement.i_DocumentTypeId = new int?();
      stockMovement.v_DocumentNumber = (string) null;
      stockMovement.i_UserId = new int?(systemUser.i_SystemUserId);
      stockMovement.b_Checked = new bool?(true);
      stockMovement.v_Observation = _selectedrow.Cells[4].Text;
      stockMovement.d_InsertDate = new DateTime?(DateTime.Now);
      stockMovement.i_ProductionOrderId = new int?();
      return stockMovement;
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
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
