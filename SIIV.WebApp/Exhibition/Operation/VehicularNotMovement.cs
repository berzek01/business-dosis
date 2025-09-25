// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Operation.VehicularNotMovement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

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
namespace SIIV.WebApp.Exhibition.Operation
{
  public class VehicularNotMovement : Page
  {
    private int i_PlateTypeId = 0;
    private int i_ProductId;
    protected UpdatePanel UpdatePanel1;
    protected Label Label19;
    protected DropDownList wddAssociated;
    protected Label Label1;
    protected TextBox txtPlateSearch;
    protected Label Label2;
    protected Fecha wdpStartDate;
    protected Label Label3;
    protected Fecha wdpEndDate;
    protected Button wibSearch;
    protected Button wibExcel;
    protected GridView wdgList;
    protected GridView wdgListNew;
    protected Pager custPagerVNM;
    protected Label lblMessage;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        DataTable dtResult = new DataTable("Datos");
        this.TableColumns(dtResult);
        DataRow row = dtResult.NewRow();
        dtResult.Rows.Add(row);
        this.wdgList.DataSource = (object) dtResult;
        this.wdgList.DataBind();
        this.wdgList.Rows[0].Visible = false;
        this.ViewState["i_PlateTypeId"] = (object) this.Request.QueryString["t"].ToString();
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.wibExcel.Enabled = false;
        this.LoadAssociated();
        this.SetDatePicker();
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
      dtResult.Columns.Add("v_ReasonSocial", typeof (string));
      dtResult.Columns.Add("v_PlateNew", typeof (string));
      dtResult.Columns.Add("d_InsertDate", typeof (DateTime));
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchVNM();

    protected void custPagerVNM_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        int int32 = Convert.ToInt32(this.wddAssociated.SelectedValue.ToString());
        string v_PlateNew = this.txtPlateSearch.Text.TrimEnd();
        DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
        DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.i_ProductId = this.SetProductId(this.i_PlateTypeId);
        this.SearchVNMList(int32, dateTime1, dateTime2, v_PlateNew, this.i_ProductId, this.i_PlateTypeId, false);
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

    protected void wibExcel_Click(object sender, EventArgs e)
    {
      try
      {
        this.ExportList();
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

    protected void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable objDataTable = (DataTable) this.Session["dtExport"];
        ClsExportToExcelDataGrid exportToExcelDataGrid = new ClsExportToExcelDataGrid();
        List<ClassColumns> classColumnsList = new List<ClassColumns>();
        foreach (DataControlField column in (StateManagedCollection) this.wdgList.Columns)
        {
          if (column.Visible && column.GetType().Name == "BoundField")
          {
            BoundField boundField = (BoundField) column;
            classColumnsList.Add(new ClassColumns(boundField.DataField, 1, (int) column.ControlStyle.Width.Value, boundField.HeaderText));
          }
        }
        exportToExcelDataGrid.clsTitle = classColumnsList;
        exportToExcelDataGrid.AgregarHojaLibro(objDataTable, "Listado Placas Sin Movimientos");
        exportToExcelDataGrid.CerrarLibro();
        byte[] buffer = exportToExcelDataGrid.DownloadByte();
        this.Response.Clear();
        this.Response.AddHeader("content-disposition", "attachment; filename=ListadoPlacasSinMovimientos.xls");
        this.Response.BinaryWrite(buffer);
        this.Response.End();
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

    private void LoadAssociated()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - VehicularNotMovement.aspx");
        AssociatedQueriesBL associatedQueriesBl = new AssociatedQueriesBL();
        if (this.Session["ApplicationId"] == null)
          return;
        int int32_1 = Convert.ToInt32(this.Session["ApplicationId"], (IFormatProvider) CultureInfo.CurrentCulture);
        string userExtendedAction = associatedQueriesBl.GetSystemUserExtendedAction(systemUser.i_SystemUserId, int32_1);
        DataTable dataTable = new DataTable();
        if (userExtendedAction != "")
        {
          int int32_2 = Convert.ToInt32(Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("7", StringComparison.CurrentCulture))), (IFormatProvider) CultureInfo.CurrentCulture);
          this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
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
              this.wddAssociated.Items.Insert(0, new ListItem("-- Todos --", "0"));
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

    private void SetDatePicker()
    {
      this.wdpStartDate.Value = DateTime.Now;
      this.wdpEndDate.Value = DateTime.Now;
    }

    private void SearchVNM()
    {
      try
      {
        this.wibExcel.Enabled = false;
        if (this.wddAssociated.SelectedItem.Text.Equals("-- Todos --") && this.wddAssociated.SelectedValue == "0" && this.txtPlateSearch.Text == "")
          throw new HandledException(1, "Seleccione un Asociado o una Placa.");
        if (this.wdpEndDate.Value.Subtract(this.wdpStartDate.Value).Days > 360)
          throw new HandledException(1, "Si el intervalo de fechas excede a 360 días, debe especificar un número de placa");
        int int32 = Convert.ToInt32(this.wddAssociated.SelectedValue.ToString());
        string v_PlateNew = this.txtPlateSearch.Text.TrimEnd();
        DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
        DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.i_ProductId = this.SetProductId(this.i_PlateTypeId);
        this.SearchVNMList(int32, dateTime1, dateTime2, v_PlateNew, this.i_ProductId, this.i_PlateTypeId, true);
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

    private void SearchVNMList(
      int i_SystemUserId,
      DateTime d_StartDate,
      DateTime d_EndDate,
      string v_PlateNew,
      int i_ProductId,
      int i_SpecialPlateTypeId,
      bool pboolLoadPager)
    {
      try
      {
        int startRowIndex = pboolLoadPager ? 1 : this.custPagerVNM.CurrentPageNumber;
        int maxRows = this.custPagerVNM.CurrentPageSize == 0 ? 10 : this.custPagerVNM.CurrentPageSize;
        int pintTotalRows;
        DataTable all = new VehicleMovementQueriesBL().SpecialPlateVehicleNotMovementGetAll(i_SystemUserId, d_StartDate, d_EndDate, v_PlateNew, i_ProductId, i_SpecialPlateTypeId, startRowIndex, maxRows, out pintTotalRows);
        if (all == null || all.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
          this.HidePopup();
        }
        else
        {
          this.lblMessage.Visible = false;
          this.wibExcel.Enabled = true;
        }
        int num = pintTotalRows;
        this.wdgList.DataSource = (object) all;
        this.wdgList.DataBind();
        this.custPagerVNM.TotalPages = num % maxRows == 0 ? num / maxRows : num / maxRows + 1;
        this.custPagerVNM.TotalRecordCount = pintTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerVNM.LoadPager();
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

    private void ExportList()
    {
      try
      {
        int int32 = Convert.ToInt32(this.wddAssociated.SelectedValue.ToString());
        string v_PlateNew = this.txtPlateSearch.Text.TrimEnd();
        DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
        DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        this.i_ProductId = this.SetProductId(this.i_PlateTypeId);
        DataTable dataTable = new DataTable();
        this.Session["dtExport"] = (object) new VehicleMovementQueriesBL().SpecialPlateVehicleNotMovementGetAll(int32, dateTime1, dateTime2, v_PlateNew, this.i_ProductId, this.i_PlateTypeId, 0, 0, out int _);
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

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
