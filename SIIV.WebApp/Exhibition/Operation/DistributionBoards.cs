// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Operation.DistributionBoards
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Operation
{
  public class DistributionBoards : Page
  {
    private AssociatedQueriesBL pobjAssociatedQueriesBL;
    private AcquisitionQueriesBL pobjAcquisitionQueriesBL;
    private AcquisitionManagementBL pobjAcquisitionManagementBL;
    private int i_Product;
    protected UpdatePanel UpdatePanel1;
    protected Label Label12;
    protected DropDownList wddChildrenUsers;
    protected Button wibSearchPlate;
    protected GridView wdgListPlate;
    protected Label lblCount;
    protected Label Label2;
    protected DropDownList wddTargetUser;
    protected Button wibAssigned;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.LoadUsers();
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

    protected void wibSearchPlate_Click(object sender, EventArgs e)
    {
      try
      {
        int int32_1 = Convert.ToInt32(this.wddChildrenUsers.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        int int32_2 = Convert.ToInt32(this.ViewState["t"]);
        this.i_Product = this.SetProductId(int32_2);
        this.ListPlate(int32_1, int32_2, this.i_Product);
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

    protected void wibAssigned_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.wddChildrenUsers.SelectedValue == this.wddTargetUser.SelectedValue)
          throw new HandledException(1, "Debe seleccionar un asociado diferente");
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - DistributionBoards.aspx");
        this.pobjAcquisitionManagementBL = new AcquisitionManagementBL();
        int num1 = 0;
        foreach (GridViewRow row in this.wdgListPlate.Rows)
        {
          CheckBox control = (CheckBox) row.FindControl("chkItem");
          if (control != null && control.Checked)
            new BlockPlateBL().ValidateBlockPlate(Convert.ToInt32(row.Cells[2].Text.ToString()));
        }
        foreach (GridViewRow row in this.wdgListPlate.Rows)
        {
          CheckBox control1 = (CheckBox) row.FindControl("chkItem");
          if (control1 != null && control1.Checked)
          {
            ++num1;
            Label control2 = (Label) row.FindControl("v_PlateNew");
            int num2 = this.pobjAcquisitionManagementBL.ExhibitionDistributionPlatesInsert(Convert.ToInt32(row.Cells[2].Text.ToString()), "4|5|6|7", Convert.ToInt32(this.wddChildrenUsers.SelectedValue.ToString()), Convert.ToInt32(this.wddTargetUser.SelectedValue.ToString()), string.Empty, systemUser.i_SystemUserId, Convert.ToInt32(this.ViewState["t"]));
            try
            {
              if (num2 <= 0)
                throw new HandledException(1, "Se encontró un problema en la actualización de los datos");
              Message.SetMessage(this.lblMessage, new HandledException(2, "Se ha distribuído la(s) Placa(s) correctamente"));
            }
            catch (Exception ex)
            {
              throw ex;
            }
          }
        }
        if (num1 == 0)
          throw new HandledException(1, "Seleccione al menos una placa");
        int int32_1 = Convert.ToInt32(this.wddChildrenUsers.SelectedValue.ToString());
        int int32_2 = Convert.ToInt32(this.ViewState["t"]);
        this.i_Product = this.SetProductId(int32_2);
        this.ListPlate(int32_1, int32_2, this.i_Product);
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

    private void LoadUsers()
    {
      try
      {
        this.ViewState["t"] = (object) Convert.ToString(this.Request.QueryString["t"]);
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - DistributionBoards.aspx");
        if (systemUser == null)
          return;
        this.pobjAssociatedQueriesBL = new AssociatedQueriesBL();
        DataTable dataTable = this.pobjAssociatedQueriesBL.ExhibitionAssociatedList(systemUser.i_SystemUserId);
        if (dataTable == null || dataTable.Rows.Count == 0)
          return;
        this.wddChildrenUsers.DataSource = (object) dataTable;
        this.wddChildrenUsers.DataTextField = "v_Alias";
        this.wddChildrenUsers.DataValueField = "i_SystemUserId";
        this.wddChildrenUsers.DataBind();
        this.wddChildrenUsers.SelectedIndex = 0;
        this.wddTargetUser.DataSource = (object) dataTable;
        this.wddTargetUser.DataTextField = "v_Alias";
        this.wddTargetUser.DataValueField = "i_SystemUserId";
        this.wddTargetUser.DataBind();
        this.wddTargetUser.SelectedIndex = 0;
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

    private void ListPlate(int i_ReceiverUserId, int i_PlateTypeId, int i_Product)
    {
      try
      {
        DataTable dataTable1 = new DataTable();
        this.pobjAcquisitionQueriesBL = new AcquisitionQueriesBL();
        DataTable dataTable2 = this.pobjAcquisitionQueriesBL.SpecialPlateDistributionGet(i_ReceiverUserId, i_PlateTypeId, i_Product);
        this.lblCount.Text = dataTable2 != null && dataTable2.Rows.Count != 0 ? Constants.SEARCHRESULT_OK.Replace("XX", dataTable2.Rows.Count.ToString()) : Constants.SEARCHRESULT_Empty;
        this.wdgListPlate.AutoGenerateColumns = false;
        this.wdgListPlate.DataSource = (object) dataTable2;
        this.wdgListPlate.DataBind();
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
  }
}
