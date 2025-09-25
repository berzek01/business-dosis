// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Inventory.PlateDeliveryRegularization
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Inventory.BL;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using System;
using System.Data;
using System.Globalization;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Inventory
{
  public class PlateDeliveryRegularization : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected DropDownList wddLocation;
    protected TextBox txtIds;
    protected Button btnAdd;
    protected Label lblMsg;
    protected GridView gv_Detail;
    protected Label lblCount;
    protected Label lblComplait;
    protected TextBox txtComplait;
    protected TextBox txtcomis;
    protected Fecha Fecha1;
    protected TextBox txtRes;
    protected Button btnSave;
    protected Button btnNew;
    protected Label lblMsg1;

    protected void Page_PreRender(object sender, EventArgs e)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadLocation();
      this.txtIds.Focus();
      this.btnSave.Enabled = false;
      DataTable dt_Result = new DataTable("Datos");
      this.TableColumns(dt_Result);
      DataRow row = dt_Result.NewRow();
      dt_Result.Rows.Add(row);
      this.gv_Detail.DataSource = (object) dt_Result;
      this.gv_Detail.DataBind();
      this.gv_Detail.Rows[0].Visible = false;
      this.lblCount.Text = "Total de registros: 0";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
      try
      {
        this.lblMsg.Visible = false;
        this.lblMsg1.Visible = false;
        if (this.wddLocation.SelectedValue == "-1")
        {
          Message.SetMessage(this.lblMsg, enmMessageType.Warning, "Seleccione un punto de entrega.");
          this.wddLocation.Focus();
        }
        else if (this.txtIds.Text == null || this.txtIds.Text == "")
        {
          Message.SetMessage(this.lblMsg, enmMessageType.Warning, "Inserte Id Solicitud.");
          this.txtIds.Focus();
        }
        else
        {
          DataTable regularizationItemBy = new PlateDeliverQueriesBL().GetPlateDeliverRegularizationItemBy("", Convert.ToInt32(this.txtIds.Text), Convert.ToInt32(this.wddLocation.SelectedValue));
          DataTable dt_Result = new DataTable();
          if (regularizationItemBy == null || regularizationItemBy.Rows.Count == 0)
          {
            Message.SetMessage(this.lblMsg, enmMessageType.Warning, "No se encontró información para realizar la entrega.");
          }
          else
          {
            if (this.ViewState["dtPlateDeliverItemOld"] != null)
            {
              dt_Result = (DataTable) this.ViewState["dtPlateDeliverItemOld"];
              for (int index = 0; index < dt_Result.Rows.Count; ++index)
              {
                if (dt_Result.Rows[index]["i_RequirementPlateId"].ToString() == this.txtIds.Text.TrimEnd())
                {
                  Message.SetMessage(this.lblMsg, enmMessageType.Warning, "La placa de solicitud " + this.txtIds.Text + " ya fue agregada en la lista.");
                  this.txtIds.Text = string.Empty;
                  return;
                }
              }
            }
            else
              this.TableColumns(dt_Result);
            this.TableReadOnly(dt_Result);
            DataRow row = dt_Result.NewRow();
            row["i_RequirementPlateId"] = (object) regularizationItemBy.Rows[0]["i_RequirementPlateId"].ToString();
            row["v_PlateNew"] = (object) regularizationItemBy.Rows[0]["v_PlateNew"].ToString();
            row["v_ProductName"] = (object) regularizationItemBy.Rows[0]["v_ProductName"].ToString();
            row["LocationDesc"] = (object) regularizationItemBy.Rows[0]["LocationDesc"].ToString();
            row["v_TypeProcessed"] = (object) regularizationItemBy.Rows[0]["v_TypeProcessed"].ToString();
            row["v_RequirementType"] = (object) regularizationItemBy.Rows[0]["v_RequirementType"].ToString();
            row["v_CompleteName"] = (object) regularizationItemBy.Rows[0]["v_CompleteName"].ToString();
            row["v_RequirementStatus"] = (object) regularizationItemBy.Rows[0]["v_RequirementStatus"].ToString();
            dt_Result.Rows.Add(row);
            this.ViewState["dtPlateDeliverItemOld"] = (object) dt_Result;
            this.gv_Detail.DataSource = (object) dt_Result;
            this.gv_Detail.DataBind();
            if (dt_Result.Rows.Count > 0)
              this.btnSave.Enabled = true;
            this.txtIds.Text = string.Empty;
            this.lblCount.Text = Constants.SEARCHRESULT_OK.Replace("XX", this.gv_Detail.Rows.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture));
          }
        }
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, enmMessageType.Error, "****Error al consultar la placa. <br>" + ex.Message);
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void gv_Detail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        if (!e.CommandName.Equals("Delete", StringComparison.CurrentCulture))
          return;
        this.lblMsg1.Visible = false;
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - PlateDeliveryRegularization.aspx");
        string text = this.gv_Detail.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = (DataTable) this.ViewState["dtPlateDeliverItemOld"];
        for (int index = 0; index < dataTable2.Rows.Count; ++index)
        {
          if (dataTable2.Rows[index]["i_RequirementPlateId"].ToString() == text.TrimEnd())
          {
            dataTable2.Rows.RemoveAt(index);
            if (dataTable2.Rows.Count == 0)
            {
              DataRow row = dataTable2.NewRow();
              dataTable2.Rows.Add(row);
              this.gv_Detail.DataSource = (object) dataTable2;
              this.gv_Detail.DataBind();
              this.gv_Detail.Rows[0].Visible = false;
              this.ViewState.Remove("dtPlateDeliverItemOld");
            }
            else
            {
              this.gv_Detail.DataSource = (object) dataTable2;
              this.gv_Detail.DataBind();
            }
            Message.SetMessage(this.lblMsg1, enmMessageType.Success, "La solicitud " + text + " fue eliminada de la lista.");
          }
        }
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg1, enmMessageType.Error, "****Error al eliminar la solicitud de la lista. <br>" + ex.Message);
      }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
      this.lblMsg.Visible = false;
      this.lblMsg1.Visible = false;
      this.btnSave.Enabled = false;
      this.txtComplait.Text = string.Empty;
      this.txtcomis.Text = string.Empty;
      this.txtRes.Text = string.Empty;
      this.txtIds.Focus();
      DataTable dt_Result = new DataTable("Datos");
      this.TableColumns(dt_Result);
      DataRow row = dt_Result.NewRow();
      dt_Result.Rows.Add(row);
      this.gv_Detail.DataSource = (object) dt_Result;
      this.gv_Detail.DataBind();
      this.gv_Detail.Rows[0].Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable dataTable1 = new DataTable();
        SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
        int num = 0;
        if (this.txtComplait.Text == null || this.txtComplait.Text == "")
        {
          Message.SetMessage(this.lblMsg1, enmMessageType.Warning, "Ingrese número de denuncia.");
          this.txtComplait.Focus();
          this.HidePopup();
        }
        else if (this.txtcomis.Text == null || this.txtcomis.Text == "")
        {
          Message.SetMessage(this.lblMsg1, enmMessageType.Warning, "Ingrese dependencia policial.");
          this.txtcomis.Focus();
          this.HidePopup();
        }
        else
        {
          DateTime dateTime = this.Fecha1.Value;
          if (false)
          {
            Message.SetMessage(this.lblMsg1, enmMessageType.Warning, "Ingrese fecha de la denuncia.");
            this.HidePopup();
          }
          else if (this.txtRes.Text == null || this.txtRes.Text == "")
          {
            Message.SetMessage(this.lblMsg1, enmMessageType.Warning, "Ingrese el responsable.");
            this.txtRes.Focus();
            this.HidePopup();
          }
          else if (this.ViewState["dtPlateDeliverItemOld"] != null)
          {
            DataTable dataTable2 = (DataTable) this.ViewState["dtPlateDeliverItemOld"];
            for (int index = 0; index < dataTable2.Rows.Count; ++index)
            {
              StockMovementManagementBL movementManagementBl = new StockMovementManagementBL();
              TransactionOptions transactionOptions = new TransactionOptions()
              {
                Timeout = new TimeSpan(1, 1, 1, 1)
              };
              try
              {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
                {
                  num = movementManagementBl.DeliveryRegularization(Convert.ToInt32(dataTable2.Rows[index]["i_RequirementPlateId"].ToString()), systemUser.i_SystemUserId, this.txtComplait.Text.TrimEnd(), this.txtcomis.Text, this.Fecha1.Value.ToString("yyyyMMdd"), this.txtRes.Text);
                  transactionScope.Complete();
                }
              }
              catch (Exception ex)
              {
                Message.SetMessage(this.lblMsg1, enmMessageType.Error, "****Error al graba las entregas. <br>" + ex.Message);
                this.HidePopup();
                return;
              }
            }
            if (num <= 0)
              return;
            Message.SetMessage(this.lblMsg1, enmMessageType.Success, "Se realizaron las entregas con exito.");
            this.btnSave.Enabled = false;
            this.HidePopup();
          }
          else
          {
            Message.SetMessage(this.lblMsg1, enmMessageType.Error, "****No existen datos en la tabla.");
            this.HidePopup();
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.HidePopup();
      }
    }

    protected void gv_Detail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void gv_Detail_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
    }

    protected void TableColumns(DataTable dt_Result)
    {
      dt_Result.Columns.Add("i_RequirementPlateId", typeof (string));
      dt_Result.Columns.Add("v_PlateNew", typeof (string));
      dt_Result.Columns.Add("v_ProductName", typeof (string));
      dt_Result.Columns.Add("LocationDesc", typeof (string));
      dt_Result.Columns.Add("v_TypeProcessed", typeof (string));
      dt_Result.Columns.Add("v_RequirementType", typeof (string));
      dt_Result.Columns.Add("v_CompleteName", typeof (string));
      dt_Result.Columns.Add("v_RequirementStatus", typeof (string));
    }

    protected void TableReadOnly(DataTable dt_Result)
    {
      dt_Result.Columns["i_RequirementPlateId"].ReadOnly = false;
      dt_Result.Columns["v_PlateNew"].ReadOnly = false;
      dt_Result.Columns["v_ProductName"].ReadOnly = false;
      dt_Result.Columns["LocationDesc"].ReadOnly = false;
      dt_Result.Columns["v_TypeProcessed"].ReadOnly = false;
      dt_Result.Columns["v_RequirementType"].ReadOnly = false;
      dt_Result.Columns["v_CompleteName"].ReadOnly = false;
      dt_Result.Columns["v_RequirementStatus"].ReadOnly = false;
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    public void LoadLocation()
    {
      DataTable synchronizationLocation = new ShiftingInventoryQueriesBL().GetSynchronizationLocation();
      this.wddLocation.Items.Clear();
      this.wddLocation.DataSource = (object) synchronizationLocation;
      this.wddLocation.DataTextField = "v_Description";
      this.wddLocation.DataValueField = "i_LocationId";
      this.wddLocation.DataBind();
      this.wddLocation.Items.Insert(0, new ListItem("-- Todos -- ", "-1"));
      this.wddLocation.SelectedValue = "-1";
    }
  }
}
