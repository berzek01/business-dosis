// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.WarehouseControlOperation
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  public class WarehouseControlOperation : Page
  {
    private DataTable dtWastageControlOperation = new DataTable();
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtIds;
    protected Button wibNew;
    protected Label lblConciliation;
    protected Label lblCount;
    protected GridView wdgWarehouseControlOperation;
    protected Button btnManagementTemp;
    protected Button wibSave;
    protected Button wibCancel;
    protected Label lblMsg;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMsg.Visible = false;
      this.lblConciliation.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.InitialLoad();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }

    protected void wibNew_Click(object sender, EventArgs e)
    {
      try
      {
        this.AgreeRequirement();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }

    protected void wdgWarehouseControlOperation_RowCommand(
      object sender,
      GridViewCommandEventArgs e)
    {
      try
      {
        GridViewRow row1 = this.wdgWarehouseControlOperation.Rows[Convert.ToInt32(e.CommandArgument)];
        if (!e.CommandName.Equals("Delete", StringComparison.CurrentCulture))
          return;
        int num = int.Parse(row1.Cells[0].Text);
        this.dtWastageControlOperation = (DataTable) this.ViewState["dtWastageControlOperation"];
        foreach (DataRow row2 in (InternalDataCollectionBase) this.dtWastageControlOperation.Rows)
        {
          if (row2["i_RequirementPlateId"].ToString() == num.ToString((IFormatProvider) CultureInfo.CurrentCulture))
          {
            this.dtWastageControlOperation.Rows.Remove(row2);
            break;
          }
        }
        DataTable dataTable = this.dtWastageControlOperation.Clone();
        if (this.dtWastageControlOperation.Rows.Count > 0)
          dataTable = ((IEnumerable<DataRow>) this.dtWastageControlOperation.Select("", "i_Item Desc")).CopyToDataTable<DataRow>();
        this.wdgWarehouseControlOperation.DataSource = (object) dataTable;
        this.wdgWarehouseControlOperation.DataBind();
        this.ViewState["dtWastageControlOperation"] = (object) this.dtWastageControlOperation;
        this.lblCount.Text = this.dtWastageControlOperation.Rows.Count.ToString();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      try
      {
        int int32_1 = Convert.ToInt32(((TableRow) this.Session["WastageSelectedRow"]).Cells[11].Text, (IFormatProvider) CultureInfo.CurrentCulture);
        int int32_2 = Convert.ToInt32(ConfigurationManager.AppSettings["permissiblelimitRequest"]);
        if (this.wdgWarehouseControlOperation.Rows.Count > int32_2 && int32_1 == 2)
          throw new HandledException(1, "Máximo " + int32_2.ToString() + " solicitudes por usuario");
        if (this.wdgWarehouseControlOperation.Rows.Count <= 4 && int32_1 == 1)
          throw new HandledException(1, "Mínimo 5 solicitudes por Gestor");
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = (DataTable) this.ViewState["dtWastageControlOperation"];
        XElement xelement = new XElement((XName) "WareHouseControl", new object[3]
        {
          (object) new XElement((XName) "i_WareHouseControlId", (object) Convert.ToInt32(this.Request.QueryString["WarehouseControlId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture)),
          (object) new XElement((XName) "i_UserId", (object) ((SystemUser) this.Session["SystemUser"]).i_SystemUserId),
          (object) new XElement((XName) "WareHouseControlDetails")
        });
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
        {
          XElement content = new XElement((XName) "WareHouseControlDetail", new object[5]
          {
            (object) new XElement((XName) "i_RequirementPlateId", (object) Convert.ToInt32(row["i_RequirementPlateId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture)),
            (object) new XElement((XName) "i_DeliveryPlateTypeId", (object) Convert.ToInt32(row["i_DeliveryPlateTypeId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture)),
            (object) new XElement((XName) "i_StatusId", (object) Convert.ToInt32(row["i_StatusId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture)),
            (object) new XElement((XName) "i_ReturnUserAuxId", (object) Convert.ToInt32(row["i_ReturnUserAuxId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture)),
            (object) new XElement((XName) "v_Observation", (object) row["v_Observation"].ToString())
          });
          xelement.Element((XName) "WareHouseControlDetails").Add((object) content);
        }
        new WarehouseManagementBL().WarehouseDetailInsert(xelement.ToString());
        Message.SetMessage(this.lblMsg, new HandledException(2, "Se grabó exitosamente"));
        this.Response.Redirect("~/Warehouse/WarehouseControlList.aspx");
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
      finally
      {
        this.txtIds.Text = "";
        this.txtIds.Focus();
      }
    }

    protected void wibCancel_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("~/Warehouse/WarehouseControlList.aspx");
    }

    protected void btnManagementTemp_Click(object sender, EventArgs e)
    {
      try
      {
        ArrayList arrayList = (ArrayList) this.Session["WastageSelectedRowEditArray"];
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = (DataTable) this.ViewState["dtWastageControlOperation"];
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
        {
          if (row["i_RequirementPlateId"].ToString() == arrayList[0].ToString())
          {
            row["i_StatusId"] = (object) Convert.ToInt32(arrayList[1].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
            row["v_Status"] = (object) arrayList[2].ToString();
            row["i_ReturnUserAuxId"] = (object) Convert.ToInt32(arrayList[3].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
            row["v_ReturnUserAux"] = (object) arrayList[4].ToString();
            row["v_Observation"] = (object) arrayList[5].ToString();
            break;
          }
        }
        dataTable2.Clone();
        this.wdgWarehouseControlOperation.DataSource = (object) ((IEnumerable<DataRow>) dataTable2.Select("", "i_Item Desc")).CopyToDataTable<DataRow>();
        this.wdgWarehouseControlOperation.DataBind();
        this.ViewState["dtWastageControlOperation"] = (object) dataTable2;
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }

    private void InitialLoad()
    {
      try
      {
        int int32 = Convert.ToInt32(this.Request.QueryString["WarehouseControlId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        this.wibSave.Enabled = this.Request.QueryString["Edit"].ToString((IFormatProvider) CultureInfo.CurrentCulture) == "1";
        this.wdgWarehouseControlOperation.Columns[6].Visible = Convert.ToBoolean(this.Request.QueryString["Edit"].ToString((IFormatProvider) CultureInfo.CurrentCulture) == "1");
        this.dtWastageControlOperation = new WarehouseControlQueriesBL().GetWarehouseControlDetail(int32);
        this.dtWastageControlOperation.Columns.Add(new DataColumn("i_Item", typeof (int))
        {
          Unique = false,
          AutoIncrement = true,
          AutoIncrementSeed = 1L,
          AutoIncrementStep = 1L
        });
        int num = 0;
        foreach (DataRow row in (InternalDataCollectionBase) this.dtWastageControlOperation.Rows)
        {
          ++num;
          row["i_Item"] = (object) num;
        }
        this.ViewState["dtWastageControlOperation"] = (object) this.dtWastageControlOperation;
        this.lblCount.Text = this.dtWastageControlOperation.Rows.Count.ToString();
        this.wdgWarehouseControlOperation.DataSource = (object) this.dtWastageControlOperation;
        this.wdgWarehouseControlOperation.DataBind();
        this.txtIds.Text = "";
        this.txtIds.Focus();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void AgreeRequirement()
    {
      try
      {
        if (string.IsNullOrEmpty(this.txtIds.Text))
          throw new HandledException(1, "Ingrese el Número de Solicitud");
        this.lblConciliation.Visible = false;
        GridViewRow gridViewRow = this.Session["WastageSelectedRow"] != null ? (GridViewRow) this.Session["WastageSelectedRow"] : throw new HandledException(3, "Error de Sesión", "Error de Sesion 'WastageSelectedRow' en 'WarehouseCOntrolOperation.aspx'");
        string empty = string.Empty;
        string s;
        if (this.txtIds.Text.Trim().Length != 10 && this.txtIds.Text.Trim() != "")
        {
          if (this.txtIds.Text.Trim().Length < 5 || this.txtIds.Text.Trim().Length > 8)
            throw new HandledException(1, "Dato ingresado es incorrecto.");
          s = this.txtIds.Text.Trim();
        }
        else
          s = this.txtIds.Text.Trim().Substring(2);
        WarehouseControlQueriesBL controlQueriesBl = new WarehouseControlQueriesBL();
        int result;
        int.TryParse(s, out result);
        DataTable dataTable1 = result != 0 ? controlQueriesBl.GetWarehouseControlFindRequirementPlate(result, "") : throw new HandledException(1, "Dato ingresado es incorrecto.");
        DataTable dataTable2 = new DataTable();
        DataTable dataTable3 = (DataTable) this.ViewState["dtWastageControlOperation"];
        if (dataTable1 == null)
          throw new HandledException(1, "No existe registro para filtro ingresado");
        if (((IEnumerable<DataRow>) dataTable3.Select("i_RequirementPlateId = '" + dataTable1.Rows[0]["i_RequirementPlateId"].ToString() + "'")).Count<DataRow>() > 0)
          throw new HandledException(1, "Registro ya ingresado");
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        DataRow row = dataTable3.NewRow();
        row["i_WarehouseControlDetailId"] = (object) 0;
        row["i_RequirementPlateId"] = (object) dataTable1.Rows[0]["i_RequirementPlateId"].ToString();
        row["v_PlateNew"] = (object) dataTable1.Rows[0]["v_Plate"].ToString();
        row["v_UserCounter"] = (object) gridViewRow.Cells[1].Text;
        row["v_UserAux"] = (object) systemUser.v_Alias;
        row["v_ProcessType"] = (object) dataTable1.Rows[0]["v_ProcessType"].ToString();
        row["v_DeliveryPlateType"] = (object) "";
        row["v_Status"] = (object) "Enviado";
        row["v_ReturnUserAux"] = (object) "";
        row["i_UserCounterId"] = (object) int.Parse(gridViewRow.Cells[12].Text, (IFormatProvider) CultureInfo.CurrentCulture);
        row["i_UserAuxId"] = (object) systemUser.i_SystemUserId;
        row["i_ProcessTypeId"] = (object) int.Parse(dataTable1.Rows[0]["i_ProcessTypeId"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        row["i_DeliveryPlateTypeId"] = (object) int.Parse(gridViewRow.Cells[11].Text, (IFormatProvider) CultureInfo.CurrentCulture);
        row["i_StatusId"] = (object) 1;
        row["i_ReturnUserAuxId"] = (object) 0;
        row["v_Observation"] = (object) "";
        dataTable3.Rows.Add(row);
        dataTable3.Clone();
        this.wdgWarehouseControlOperation.DataSource = (object) ((IEnumerable<DataRow>) dataTable3.Select("", "i_Item Desc")).CopyToDataTable<DataRow>();
        this.wdgWarehouseControlOperation.DataBind();
        this.ViewState["dtWastageControlOperation"] = (object) dataTable3;
        this.lblCount.Text = dataTable3.Rows.Count.ToString();
        string ErrorMessage = dataTable1.Rows[0]["v_ConciliacionStatus"].ToString();
        if (!(ErrorMessage != ""))
          return;
        Message.SetMessage(this.lblConciliation, new HandledException(1, ErrorMessage));
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.txtIds.Text = "";
        this.txtIds.Focus();
      }
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

    protected void wdgWarehouseControlOperation_RowDeleting(
      object sender,
      GridViewDeleteEventArgs e)
    {
    }

    protected void wdgWarehouseControlOperation_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
  }
}
