// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibicion.Production.SpecialProduccionForReplace
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.BE.CustomCode;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using SIIV.SpecialPlateProduction.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;

#nullable disable
namespace SIIV.WebApp.Exhibicion.Production
{
  public class SpecialProduccionForReplace : Page
  {
    private AssociatedQueriesBL pobjAssociatedQueriesBL;
    protected UpdatePanel UpdatePanel1;
    protected DropDownList wddAssociated;
    protected DropDownList cboSpecialPlateClasification;
    protected DropDownList wddStatus;
    protected TextBox txtPlateNew;
    protected CheckBox chkDate;
    protected Label Label5;
    protected Fecha wdpStartDate;
    protected Fecha wdpEndDate;
    protected Button wibSearch;
    protected Button wibExport;
    protected HtmlTableCell tdSelect;
    protected CheckBox chkAll;
    protected GridView wdgList;
    protected Label lblCount;
    protected Button btnReemitir;
    protected Button btnChange;
    protected Label lblMessage;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      this.tdSelect.Visible = false;
      this.LoadParameters();
      this.wdpStartDate.Value = DateTime.Now;
      this.wdpEndDate.Value = DateTime.Now;
      this.chkDate.Checked = true;
    }

    public void Search(bool pboolLoadPager)
    {
      try
      {
        if (Convert.ToInt32(this.cboSpecialPlateClasification.SelectedValue) == 0)
        {
          Message.SetMessage(this.lblMessage, new HandledException(1, "Seleccion clasificación de vehículo."));
        }
        else
        {
          int int32 = Convert.ToInt32(new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(7).Rows[0]["i_WarehouseId"].ToString());
          PlateReplaceBL plateReplaceBl = new PlateReplaceBL();
          DataTable dataTable1 = new DataTable();
          int i_ProductId = this.SetProductId(7);
          string v_PlateNew = this.txtPlateNew.Text.Trim();
          DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
          DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
          int i_Flag = 1;
          if (!this.chkDate.Checked)
            i_Flag = 0;
          DataTable dataTable2 = plateReplaceBl.ups_PlateInUse(Convert.ToInt32(this.wddAssociated.SelectedValue), v_PlateNew, i_ProductId, int32, 7, Convert.ToInt32(this.cboSpecialPlateClasification.SelectedValue), dateTime1, dateTime2, Convert.ToInt32(this.wddStatus.SelectedValue), i_Flag);
          if (dataTable2.Rows.Count > 0)
          {
            this.wdgList.DataSource = (object) dataTable2;
            this.wdgList.DataBind();
            this.tdSelect.Visible = true;
            this.lblCount.Text = "Total registros encontrados: " + dataTable2.Rows.Count.ToString();
          }
          else
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
            this.wdgList.DataSource = (object) null;
            this.wdgList.DataBind();
            this.lblCount.Visible = false;
          }
        }
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

    protected void wibSearch_Click(object sender, EventArgs e) => this.Search(true);

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkAll != null && this.chkAll.Checked)
        this.SelectChecks(true);
      else
        this.SelectChecks(false);
      this.HidePopup();
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
      try
      {
        short num = 0;
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - SpecialProductionPlateDetail.aspx");
        List<PlateInUse> pobjlstPlateInUse = new List<PlateInUse>();
        foreach (GridViewRow row in this.wdgList.Rows)
        {
          CheckBox control = (CheckBox) row.FindControl("chkItem");
          if (control != null && control.Checked)
          {
            ++num;
            pobjlstPlateInUse.Add(new PlateInUse()
            {
              v_PlateNew = row.Cells[1].Text.ToString(),
              i_RequirementPlateId = Convert.ToInt32(this.wdgList.DataKeys[row.RowIndex]["i_RequirementPlateIdPU"]),
              i_Status = Convert.ToInt32(this.wdgList.DataKeys[row.RowIndex]["i_Status"]),
              i_SystemUserId = systemUser.i_SystemUserId
            });
          }
        }
        if (num > (short) 0)
        {
          if (new SpecialPlateProductionQueriesBL().UpdatePlateInUse(pobjlstPlateInUse))
            Message.SetMessage(this.lblMessage, new HandledException(2, "Se procedio correctamente con el intercambio de la(s) placas(s)."));
          else
            Message.SetMessage(this.lblMessage, new HandledException(2, "Consulte con su Administrador Informático"));
        }
        this.HidePopup();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
        this.HidePopup();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
        this.HidePopup();
      }
      finally
      {
        this.Search(true);
      }
    }

    protected void btnReemitir_Click(object sender, EventArgs e)
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - SpecialProductionPlateDetail.aspx");
        DataTable dtResult = new DataTable();
        this.TableColumns(dtResult);
        foreach (GridViewRow row1 in this.wdgList.Rows)
        {
          CheckBox control = (CheckBox) row1.FindControl("chkItem");
          if (control != null && control.Checked)
          {
            string str = row1.Cells[1].Text.ToString();
            int int32 = Convert.ToInt32(this.wdgList.DataKeys[row1.RowIndex]["i_Status"]);
            if (int32 == 1 || int32 == 2)
              throw new HandledException(1, "La placa " + str + " no está disponible para una remisión.");
            DataRow row2 = dtResult.NewRow();
            row2["i_SpecialPlateProductionId"] = (object) 0;
            row2["v_Plate"] = (object) row1.Cells[1].Text;
            dtResult.Rows.Add(row2);
          }
        }
        if (dtResult.Rows.Count > 0)
        {
          int num = 0;
          XElement xelement = new XElement((XName) "SpecialPlateProduction", new object[8]
          {
            (object) new XElement((XName) "i_SpecialPlateProductionId", (object) num.ToString()),
            (object) new XElement((XName) "i_SpecialPlateProcessTypeId", (object) 1),
            (object) new XElement((XName) "i_SpecialPlateTypeId", (object) 7),
            (object) new XElement((XName) "i_SpecialPlateClasificationId", (object) this.cboSpecialPlateClasification.SelectedItem.Value),
            (object) new XElement((XName) "i_MotiveId", (object) 3),
            (object) new XElement((XName) "V_Observation", (object) "Reemisión Masiva"),
            (object) new XElement((XName) "i_UserId", (object) systemUser.i_SystemUserId),
            (object) new XElement((XName) "SpecialPlateProductionDetails")
          });
          foreach (DataRow row in (InternalDataCollectionBase) dtResult.Rows)
          {
            XElement content = new XElement((XName) "SpecialPlateProductionDetail", new object[2]
            {
              (object) new XElement((XName) "i_SpecialPlateProductionId", (object) num.ToString()),
              (object) new XElement((XName) "v_Plate", (object) row["v_Plate"].ToString())
            });
            xelement.Element((XName) "SpecialPlateProductionDetails").Add((object) content);
          }
          if (new SpecialPlateProductionQueriesBL().SetSpecialPlateProduction(xelement.ToString()) == null)
            Message.SetMessage(this.lblMessage, new HandledException(2, "Placas remitidas correctamente."));
        }
        this.HidePopup();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
        this.HidePopup();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
        this.HidePopup();
      }
      finally
      {
        this.Search(true);
      }
    }

    public void LoadParameters()
    {
      try
      {
        SystemParameterManagementBL parameterManagementBl = new SystemParameterManagementBL();
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
        this.pobjAssociatedQueriesBL = new AssociatedQueriesBL();
        if (this.Session["ApplicationId"] != null)
        {
          int int32_1 = Convert.ToInt32(this.Session["ApplicationId"]);
          string userExtendedAction = this.pobjAssociatedQueriesBL.GetSystemUserExtendedAction(systemUser.i_SystemUserId, int32_1);
          DataTable dataTable = new DataTable();
          if (userExtendedAction != "")
          {
            int int32_2 = Convert.ToInt32(Array.Find<string>(userExtendedAction.Split('|'), (Predicate<string>) (element => element.Equals("7", StringComparison.CurrentCulture))));
            if (int32_2 == 7)
            {
              this.FillAssociatedWdd(this.pobjAssociatedQueriesBL.ExhibitionAssociatedListAll(systemUser.i_SystemUserId, int32_2, 7));
            }
            else
            {
              DataTable dt_Result = this.pobjAssociatedQueriesBL.ExhibitionAssociatedListAll(systemUser.i_SystemUserId, 0, 7);
              if (dt_Result.Rows.Count == 1)
              {
                this.FillAssociatedWdd(dt_Result);
                this.wddAssociated.SelectedIndex = 0;
              }
              else
                this.FillAssociatedWdd(dt_Result);
            }
          }
          else
          {
            this.FillAssociatedWdd(this.pobjAssociatedQueriesBL.ExhibitionAssociatedListAll(systemUser.i_SystemUserId, 0, 7));
            this.wddAssociated.SelectedIndex = 0;
          }
        }
        ArrayList pobj1 = new ArrayList()
        {
          (object) "205",
          (object) "1,5",
          (object) "1",
          (object) "1"
        };
        List<SIIV.BE.SystemParameter> systemParameterList1 = parameterManagementBl.Get((object) pobj1);
        this.cboSpecialPlateClasification.Items.Clear();
        foreach (SIIV.BE.SystemParameter systemParameter in systemParameterList1)
          this.cboSpecialPlateClasification.Items.Add(new ListItem(systemParameter.v_Description, systemParameter.i_ParameterId.ToString()));
        this.cboSpecialPlateClasification.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        this.cboSpecialPlateClasification.SelectedIndex = 0;
        ArrayList pobj2 = new ArrayList()
        {
          (object) "851",
          (object) "1,2",
          (object) "1",
          (object) "1"
        };
        List<SIIV.BE.SystemParameter> systemParameterList2 = parameterManagementBl.Get((object) pobj2);
        this.wddStatus.Items.Clear();
        foreach (SIIV.BE.SystemParameter systemParameter in systemParameterList2)
          this.wddStatus.Items.Add(new ListItem(systemParameter.v_Description, systemParameter.i_ParameterId.ToString()));
        this.wddStatus.Items.Insert(0, new ListItem("-- Todos --", "0"));
        this.wddStatus.SelectedIndex = 0;
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
        this.wddAssociated.DataValueField = "i_AssociatedId";
        this.wddAssociated.DataBind();
        this.wddAssociated.Items.Insert(0, new ListItem("-- Todos --", "0"));
        this.wddAssociated.SelectedIndex = 0;
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

    private void SelectChecks(bool chk)
    {
      foreach (Control row in this.wdgList.Rows)
        ((CheckBox) row.FindControl("chkItem")).Checked = chk;
    }

    private void TableColumns(DataTable dtResult)
    {
      dtResult.Columns.Add("i_SpecialPlateProductionId", typeof (int));
      dtResult.Columns.Add("v_Plate", typeof (string));
      foreach (DataColumn column in (InternalDataCollectionBase) dtResult.Columns)
        column.ReadOnly = true;
    }

    protected void chkDate_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkDate.Checked)
      {
        this.wdpStartDate.Enabled = true;
        this.wdpEndDate.Enabled = true;
        this.SetDatePicker();
      }
      else
      {
        this.wdpStartDate.Enabled = false;
        this.wdpEndDate.Enabled = false;
      }
    }

    private void SetDatePicker()
    {
      this.wdpStartDate.Value = DateTime.Now;
      this.wdpEndDate.Value = DateTime.Now;
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

    private void ExportData()
    {
      try
      {
        DataTable dataTable1 = new DataTable();
        int int32 = Convert.ToInt32(new WarehouseExhibitionQueriesBL().SpecialPlateWarehouseProductGet(7).Rows[0]["i_WarehouseId"].ToString());
        DataTable dataTable2 = new DataTable();
        int i_ProductId = this.SetProductId(7);
        string v_PlateNew = this.txtPlateNew.Text.Trim();
        DateTime dateTime1 = Convert.ToDateTime(this.wdpStartDate.Value);
        DateTime dateTime2 = Convert.ToDateTime(Convert.ToDateTime(this.wdpEndDate.Value).ToShortDateString());
        PlateReplaceBL plateReplaceBl = new PlateReplaceBL();
        int i_Flag = 1;
        if (!this.chkDate.Checked)
          i_Flag = 0;
        DataTable dataTable3 = plateReplaceBl.ups_PlateInUse(Convert.ToInt32(this.wddAssociated.SelectedValue), v_PlateNew, i_ProductId, int32, 7, Convert.ToInt32(this.cboSpecialPlateClasification.SelectedValue), dateTime1, dateTime2, Convert.ToInt32(this.wddStatus.SelectedValue), i_Flag);
        if (dataTable3 == null || dataTable3.Rows.Count == 0)
          throw new HandledException(1, "No se encontró información con los valores ingresados.");
        this.Session["dtExport"] = (object) dataTable3;
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
        foreach (DataControlField column in (StateManagedCollection) this.wdgList.Columns)
        {
          if (column.Visible && column.GetType().Name == "BoundField")
          {
            BoundField boundField = (BoundField) column;
            classColumnsList.Add(new ClassColumns(boundField.DataField, 1, (int) column.ControlStyle.Width.Value, boundField.HeaderText));
          }
        }
        exportToExcelDataGrid.clsTitle = classColumnsList;
        exportToExcelDataGrid.AgregarHojaLibro(objDataTable, "Placas En Uso");
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
  }
}
