// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibicion.Operation.AssignedPlates
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibicion.Operation
{
  public class AssignedPlates : Page
  {
    private AcquisitionQueriesBL pobjAcquisitionQueriesBL;
    private int i_PlateTypeId = 0;
    private int i_Product;
    protected UpdatePanel UpdatePanel1;
    protected DropDownList wddAssociated;
    protected Button wibSearchPlate;
    protected Button wibExport;
    protected GridView wdgListPlate;
    protected Pager custPagerAsiPla;
    protected Label lblMessage;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.LlenaGrilla();
        this.ViewState["i_PlateTypeId"] = (object) this.Request.QueryString["t"].ToString();
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        switch (this.i_PlateTypeId)
        {
          case 7:
            this.wdgListPlate.Columns[1].Visible = true;
            break;
          case 11:
            this.wdgListPlate.Columns[1].Visible = false;
            break;
        }
        this.LoadAssociated();
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

    private void TableColumns(DataTable dtResult)
    {
      dtResult.Columns.Add("v_PlateNew", typeof (string));
      dtResult.Columns.Add("Status", typeof (string));
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchAsiPla();

    protected void custPagerAsiPla_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        int int32 = Convert.ToInt32(this.wddAssociated.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["t"]);
        this.i_Product = this.SetProductId(this.i_PlateTypeId);
        this.SearchAsiPlaList(int32, this.i_PlateTypeId, this.i_Product, false);
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

    protected void wibExport_Click(object sender, EventArgs e)
    {
      try
      {
        this.ExportData();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void LlenaGrilla()
    {
      DataTable dtResult = new DataTable("Datos");
      this.TableColumns(dtResult);
      DataRow row = dtResult.NewRow();
      dtResult.Rows.Add(row);
      this.wdgListPlate.DataSource = (object) dtResult;
      this.wdgListPlate.DataBind();
      this.wdgListPlate.Rows[0].Visible = false;
    }

    private void ExportData()
    {
      try
      {
        DataTable dataTable1 = new DataTable();
        if (this.wddAssociated.SelectedValue == null)
          throw new HandledException(1, "No ha seleccionado el asociado.");
        int int32 = Convert.ToInt32(this.wddAssociated.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["t"]);
        this.i_Product = this.SetProductId(this.i_PlateTypeId);
        this.pobjAcquisitionQueriesBL = new AcquisitionQueriesBL();
        DataTable dataTable2 = this.pobjAcquisitionQueriesBL.ExhibitionAcquisitionDetailGet(int32, this.i_PlateTypeId, this.i_Product, 0, 0, out int _);
        if (dataTable2 == null || dataTable2.Rows.Count == 0)
          throw new HandledException(1, "No se encontró información con los valores ingresados.");
        this.Session["dtExport"] = (object) dataTable2;
        this.Export();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void Export()
    {
      string script = "ExportExcelAll();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable objDataTable = (DataTable) this.Session["dtExport"];
        ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
        List<ClassColumns> classColumnsList = new List<ClassColumns>();
        foreach (DataControlField column in (StateManagedCollection) this.wdgListPlate.Columns)
        {
          if (column.Visible && column.GetType().Name == "BoundField")
          {
            BoundField boundField = (BoundField) column;
            classColumnsList.Add(new ClassColumns(boundField.DataField, 1, (int) column.ControlStyle.Width.Value, boundField.HeaderText));
          }
        }
        exportToExcelDataGrid.clsTitle = classColumnsList;
        exportToExcelDataGrid.AgregarHojaLibro(objDataTable, "Placas Asignadas");
        exportToExcelDataGrid.CerrarLibro();
        byte[] buffer = exportToExcelDataGrid.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=PlacasAsignadas.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
      }
      catch (Exception ex)
      {
      }
    }

    private void LoadAssociated()
    {
      try
      {
        this.ViewState["t"] = (object) Convert.ToString(this.Request.QueryString["t"]);
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["t"].ToString());
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - AssignedPlates.aspx");
        AssociatedQueriesBL associatedQueriesBl = new AssociatedQueriesBL();
        if (this.Session["ApplicationId"] == null)
          return;
        int int32_1 = Convert.ToInt32(this.Session["ApplicationId"], (IFormatProvider) CultureInfo.CurrentCulture);
        string userExtendedAction = associatedQueriesBl.GetSystemUserExtendedAction(systemUser.i_SystemUserId, int32_1);
        DataTable dataTable = new DataTable();
        if (userExtendedAction != "")
        {
          int int32_2 = Convert.ToInt32(Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("7", StringComparison.CurrentCulture))), (IFormatProvider) CultureInfo.CurrentCulture);
          if (int32_2 == 7)
          {
            this.FillAssociatedWdd(associatedQueriesBl.AssociatedList(systemUser.i_SystemUserId, int32_2, this.i_PlateTypeId));
            this.wddAssociated.Items.Insert(0, new ListItem("-- Todos --", "0"));
            this.wddAssociated.SelectedIndex = 0;
          }
          else
          {
            DataTable dt_Result = associatedQueriesBl.AssociatedList(systemUser.i_SystemUserId, 0, this.i_PlateTypeId);
            if (dt_Result.Rows.Count == 1)
            {
              this.FillAssociatedWdd(dt_Result);
              this.wddAssociated.SelectedIndex = 0;
            }
            else
            {
              this.FillAssociatedWdd(dt_Result);
              this.wddAssociated.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
              this.wddAssociated.SelectedIndex = 0;
            }
          }
        }
        else
        {
          this.FillAssociatedWdd(associatedQueriesBl.AssociatedList(systemUser.i_SystemUserId, 0, this.i_PlateTypeId));
          this.wddAssociated.SelectedIndex = 0;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void FillAssociatedWdd(DataTable dt_Result)
    {
      try
      {
        if (dt_Result == null || dt_Result.Rows.Count == 0)
          return;
        this.wddAssociated.DataSource = (object) dt_Result;
        this.wddAssociated.DataTextField = "v_Alias";
        this.wddAssociated.DataValueField = "i_SystemUserId";
        this.wddAssociated.DataBind();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchAsiPla()
    {
      try
      {
        int int32 = Convert.ToInt32(this.wddAssociated.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["t"]);
        this.i_Product = this.SetProductId(this.i_PlateTypeId);
        this.SearchAsiPlaList(int32, this.i_PlateTypeId, this.i_Product, true);
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SearchAsiPlaList(
      int i_ReceiverUserId,
      int i_PlateTypeId,
      int i_Product,
      bool pboolLoadPager)
    {
      try
      {
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerAsiPla.CurrentPageNumber;
        int pintMaxRows = this.custPagerAsiPla.CurrentPageSize == 0 ? 10 : this.custPagerAsiPla.CurrentPageSize;
        DataTable dataTable1 = new DataTable();
        this.pobjAcquisitionQueriesBL = new AcquisitionQueriesBL();
        int pintTotalRows;
        DataTable dataTable2 = new AcquisitionQueriesBL().ExhibitionAcquisitionDetailGet(i_ReceiverUserId, i_PlateTypeId, i_Product, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        if (dataTable2 == null || dataTable2.Rows.Count == 0)
        {
          this.LlenaGrilla();
          this.custPagerAsiPla.TotalPages = pintTotalRows % pintMaxRows == 0 ? pintTotalRows / pintMaxRows : pintTotalRows / pintMaxRows + 1;
          this.custPagerAsiPla.TotalRecordCount = pintTotalRows;
          if (pboolLoadPager)
            this.custPagerAsiPla.LoadPager();
          this.HidePopup();
          throw new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados");
        }
        this.lblMessage.Visible = false;
        int num = pintTotalRows;
        this.wdgListPlate.DataSource = (object) dataTable2;
        this.wdgListPlate.DataBind();
        this.custPagerAsiPla.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
        this.custPagerAsiPla.TotalRecordCount = pintTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerAsiPla.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private int SetProductId(int i_PlateTypeId)
    {
      try
      {
        int num = 0;
        WarehouseExhibitionQueriesBL exhibitionQueriesBl = new WarehouseExhibitionQueriesBL();
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = exhibitionQueriesBl.SpecialPlateWarehouseProductGet(i_PlateTypeId);
        switch (i_PlateTypeId)
        {
          case 6:
            num = 0;
            break;
          case 7:
            num = Convert.ToInt32(dataTable2.Rows[0]["i_Productid"].ToString());
            break;
          case 11:
            num = Convert.ToInt32(dataTable2.Rows[0]["i_Productid"].ToString());
            break;
        }
        return num;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
