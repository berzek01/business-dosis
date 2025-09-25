// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.SupplierList
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse
{
  public class SupplierList : Page
  {
    private MaintenanceOperation currentOperation = MaintenanceOperation.None;
    protected HiddenField H1;
    protected UpdatePanel UpdatePanel1;
    protected RadioButtonList rblCriterioFiltro;
    protected TextBox txtFilter;
    protected Button wibSearch;
    protected GridView wdgSupplierList;
    protected Pager custPagerBatch;
    protected Button wibNew;
    protected Label lblMessage;
    protected UpdatePanel UpdatePanel2;
    protected TextBox txtv_Name;
    protected RequiredFieldValidator rfvSupplier;
    protected ValidatorCalloutExtender ValidatorCalloutExtender1;
    protected TextBox txtv_Address;
    protected RequiredFieldValidator rfvAddress;
    protected ValidatorCalloutExtender ValidatorCalloutExtender2;
    protected TextBox txtv_Phone1;
    protected FilteredTextBoxExtender FilteredTextBoxExtender1;
    protected RequiredFieldValidator rfvPhone1;
    protected ValidatorCalloutExtender ValidatorCalloutExtender3;
    protected TextBox txtv_Phone2;
    protected FilteredTextBoxExtender FilteredTextBoxExtender2;
    protected TextBox txtv_OrganizationIdentifier;
    protected FilteredTextBoxExtender txtv_OrganizationIdentifier_FilteredTextBoxExtender;
    protected RequiredFieldValidator rfvOrganizationIdentifier;
    protected ValidatorCalloutExtender ValidatorCalloutExtender4;
    protected DropDownList wddCountry;
    protected RequiredFieldValidator rfvCountry;
    protected ValidatorCalloutExtender ValidatorCalloutExtender5;
    protected DropDownList wddCurrency;
    protected RequiredFieldValidator rfvCurrency;
    protected ValidatorCalloutExtender ValidatorCalloutExtender6;
    protected HtmlTableRow trManagementButtons;
    protected Button wibSave;
    protected Button wibCancel;
    protected HtmlTableRow trwibFinalize;
    protected Button wibFinalize;
    protected HiddenField hidSupplierId;
    protected Label lblMessage1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.LoadParameters();
        this.EnabledControls(MaintenanceOperation.None);
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

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        this.SearchSupplier();
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

    protected void wdgSupplierList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        int int32 = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = this.wdgSupplierList.Rows[int32];
        this.ViewState["IndexWdgSupplierList"] = (object) int32;
        if (e.CommandName == "Edit")
        {
          this.currentOperation = MaintenanceOperation.Edit;
          this.ShowSupplierInfo(this.GetCurrentSupplier(enmTypeLoadData.SelectedRowsGrid));
          this.EnabledControls(MaintenanceOperation.Edit);
        }
        else if (e.CommandName == "Delete")
        {
          this.currentOperation = MaintenanceOperation.Delete;
          this.ShowSupplierInfo(this.GetCurrentSupplier(enmTypeLoadData.SelectedRowsGrid));
          this.EnabledControls(MaintenanceOperation.Delete);
        }
        this.ViewState["currentOperation"] = (object) this.currentOperation;
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

    protected void wdgSupplierList_PageIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.SearchSupplier();
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

    protected void wibNew_Click(object sender, EventArgs e)
    {
      try
      {
        this.ClearControls();
        this.currentOperation = MaintenanceOperation.AddNew;
        this.ViewState["currentOperation"] = (object) this.currentOperation;
        this.EnabledControls(MaintenanceOperation.AddNew);
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

    protected void wibFinalize_Click(object sender, EventArgs e)
    {
      try
      {
        string script1 = UtilDA.ActiveTabIndex("tabs", 0, "1");
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script1, true);
        string script2 = "TabIndex();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "ScriptIndex", script2, true);
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage1, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage1, new HandledException(-100, ex));
      }
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      try
      {
        this.currentOperation = (MaintenanceOperation) this.ViewState["currentOperation"];
        switch (this.currentOperation)
        {
          case MaintenanceOperation.AddNew:
            this.ValidateInputData();
            new SupplierManagementBL().SupplierInsert(this.GetCurrentSupplier(enmTypeLoadData.SelectedTextbox));
            Message.SetMessage(this.lblMessage1, new HandledException(2, "•&nbsp;El Proveedor se creó satisfactoriamente."));
            this.txtFilter.Text = string.Empty;
            this.SearchSupplier();
            this.EnabledControls(MaintenanceOperation.AddNew);
            break;
          case MaintenanceOperation.Edit:
            this.ValidateInputData();
            new SupplierManagementBL().SupplierUpdate(this.GetCurrentSupplier(enmTypeLoadData.SelectedTextbox));
            Message.SetMessage(this.lblMessage1, new HandledException(2, "•&nbsp;El Proveedor se modificó satisfactoriamente."));
            this.txtFilter.Text = string.Empty;
            this.SearchSupplier();
            this.EnabledControls(MaintenanceOperation.Edit);
            break;
          case MaintenanceOperation.Delete:
            new SupplierManagementBL().SupplierDelete(Convert.ToInt32(this.hidSupplierId.Value, (IFormatProvider) CultureInfo.CurrentCulture));
            Message.SetMessage(this.lblMessage1, new HandledException(2, "•&nbsp;El Proveedor se eliminó satisfactoriamente."));
            this.txtFilter.Text = string.Empty;
            this.SearchSupplier();
            this.EnabledControls(MaintenanceOperation.Delete);
            break;
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage1, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage1, new HandledException(-100, ex));
      }
    }

    protected void wibCancel_Click(object sender, EventArgs e)
    {
      try
      {
        string script1 = UtilDA.ActiveTabIndex("tabs", 0, "1");
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script1, true);
        string script2 = "TabIndex();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "ScriptIndex", script2, true);
        this.EnabledControls(MaintenanceOperation.None);
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage1, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage1, new HandledException(-100, ex));
      }
    }

    protected void custPagerBatch_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        string pstrName = string.Empty;
        string pstrOrganizationIdentifier = string.Empty;
        switch (int.Parse(this.rblCriterioFiltro.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture))
        {
          case 0:
            pstrName = this.txtFilter.Text;
            break;
          case 1:
            pstrOrganizationIdentifier = this.txtFilter.Text;
            break;
        }
        this.SearchSupplierList(0, pstrName, pstrOrganizationIdentifier, false);
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

    private void ShowSupplierInfo(Supplier pobjSupplier)
    {
      try
      {
        this.txtv_Name.Text = pobjSupplier.v_Name;
        this.txtv_Address.Text = pobjSupplier.v_Address;
        this.txtv_Phone1.Text = pobjSupplier.v_Phone1;
        this.txtv_Phone2.Text = pobjSupplier.v_Phone2;
        this.txtv_OrganizationIdentifier.Text = pobjSupplier.v_OrganizationIdentifier;
        string empty = string.Empty;
        foreach (ListItem listItem in this.wddCountry.Items)
        {
          if (listItem.Text == this.Page.Server.HtmlDecode(pobjSupplier.v_Country))
            empty = listItem.Value;
        }
        this.wddCountry.SelectedValue = empty == string.Empty ? "-1" : empty;
        DropDownList wddCurrency = this.wddCurrency;
        int? iCurrencyId = pobjSupplier.i_CurrencyId;
        string str;
        if (!(iCurrencyId.ToString() == string.Empty))
        {
          iCurrencyId = pobjSupplier.i_CurrencyId;
          str = iCurrencyId.ToString();
        }
        else
          str = "-1";
        wddCurrency.SelectedValue = str;
        this.hidSupplierId.Value = pobjSupplier.i_SupplierId.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void LoadParameters()
    {
      try
      {
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) ("" + SystemParameterGroups.ModelCurrency.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ", " + SystemParameterGroups.Countries.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
          (object) "",
          (object) "1",
          (object) "1"
        });
        if (dataTable != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (row["i_GroupId"].ToString() == SystemParameterGroups.ModelCurrency.ToString((IFormatProvider) CultureInfo.CurrentCulture))
              this.wddCurrency.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
            if (row["i_GroupId"].ToString() == SystemParameterGroups.Countries.ToString((IFormatProvider) CultureInfo.CurrentCulture))
              this.wddCountry.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
          }
        }
        this.wddCurrency.Items.Insert(0, new ListItem("- Seleccione - ", "-1"));
        this.wddCountry.Items.Insert(0, new ListItem("- Seleccione - ", "-1"));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchSupplier()
    {
      try
      {
        string pstrName = string.Empty;
        string pstrOrganizationIdentifier = string.Empty;
        switch (int.Parse(this.rblCriterioFiltro.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture))
        {
          case 0:
            pstrName = this.txtFilter.Text;
            break;
          case 1:
            pstrOrganizationIdentifier = this.txtFilter.Text;
            break;
        }
        this.SearchSupplierList(0, pstrName, pstrOrganizationIdentifier, true);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchSupplierList(
      int pintSupplierId,
      string pstrName,
      string pstrOrganizationIdentifier,
      bool pboolLoadPager)
    {
      try
      {
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerBatch.CurrentPageNumber;
        int pintMaxRows = this.custPagerBatch.CurrentPageSize == 0 ? 10 : this.custPagerBatch.CurrentPageSize;
        int pintTotalRows;
        DataTable supplierByPag = new SupplierQueriesBL().GetSupplierByPag(pintSupplierId, pstrName, pstrOrganizationIdentifier, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        int num = pintTotalRows;
        if (supplierByPag.Rows.Count == 0)
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontro información con los criterios seleccionados."));
        this.wdgSupplierList.DataSource = (object) supplierByPag;
        this.wdgSupplierList.DataBind();
        this.custPagerBatch.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
        this.custPagerBatch.TotalRecordCount = pintTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerBatch.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ClearControls()
    {
      try
      {
        this.txtv_Name.Text = string.Empty;
        this.txtv_Address.Text = string.Empty;
        this.txtv_Phone1.Text = string.Empty;
        this.txtv_Phone2.Text = string.Empty;
        this.txtv_OrganizationIdentifier.Text = string.Empty;
        this.wddCurrency.SelectedValue = "-1";
        this.wddCountry.SelectedValue = "-1";
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void EnabledControls(MaintenanceOperation penuCurrentOperation)
    {
      try
      {
        string str = this.H1.Value;
        switch (penuCurrentOperation)
        {
          case MaintenanceOperation.AddNew:
            this.txtv_Name.Enabled = true;
            this.txtv_Address.Enabled = true;
            this.txtv_Phone1.Enabled = true;
            this.txtv_Phone2.Enabled = true;
            this.txtv_OrganizationIdentifier.Enabled = true;
            this.wddCurrency.Enabled = true;
            this.wddCountry.Enabled = true;
            this.rfvSupplier.Enabled = true;
            this.rfvAddress.Enabled = true;
            this.rfvPhone1.Enabled = true;
            this.rfvOrganizationIdentifier.Enabled = true;
            this.rfvCountry.Enabled = true;
            this.rfvCurrency.Enabled = true;
            this.wibSave.Text = "Grabar";
            this.txtv_Name.Focus();
            if (str == "0")
            {
              this.trManagementButtons.Visible = true;
              this.trwibFinalize.Visible = false;
              this.lblMessage1.Visible = false;
              string script1 = UtilDA.ActiveTabIndex("tabs", 1, "0");
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script1, true);
              string script2 = "TabIndex();";
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script2, true);
              break;
            }
            this.trManagementButtons.Visible = false;
            this.trwibFinalize.Visible = true;
            break;
          case MaintenanceOperation.Edit:
            this.txtv_Name.Enabled = true;
            this.txtv_Address.Enabled = true;
            this.txtv_Phone1.Enabled = true;
            this.txtv_Phone2.Enabled = true;
            this.txtv_OrganizationIdentifier.Enabled = true;
            this.wddCurrency.Enabled = true;
            this.wddCountry.Enabled = true;
            this.rfvSupplier.Enabled = true;
            this.rfvAddress.Enabled = true;
            this.rfvPhone1.Enabled = true;
            this.rfvOrganizationIdentifier.Enabled = true;
            this.rfvCountry.Enabled = true;
            this.rfvCurrency.Enabled = true;
            this.wibSave.Text = "Grabar";
            this.txtv_Name.Focus();
            if (str == "0")
            {
              this.trManagementButtons.Visible = true;
              this.trwibFinalize.Visible = false;
              this.lblMessage1.Visible = false;
              string script3 = UtilDA.ActiveTabIndex("tabs", 1, "0");
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script3, true);
              string script4 = "TabIndex();";
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script4, true);
              break;
            }
            this.trManagementButtons.Visible = false;
            this.trwibFinalize.Visible = true;
            break;
          case MaintenanceOperation.Delete:
            this.txtv_Name.Enabled = false;
            this.txtv_Address.Enabled = false;
            this.txtv_Phone1.Enabled = false;
            this.txtv_Phone2.Enabled = false;
            this.txtv_OrganizationIdentifier.Enabled = false;
            this.wddCurrency.Enabled = false;
            this.wddCountry.Enabled = false;
            this.rfvSupplier.Enabled = false;
            this.rfvAddress.Enabled = false;
            this.rfvPhone1.Enabled = false;
            this.rfvOrganizationIdentifier.Enabled = false;
            this.rfvCountry.Enabled = false;
            this.rfvCurrency.Enabled = false;
            this.wibSave.Text = "Eliminar";
            if (str == "0")
            {
              this.trManagementButtons.Visible = true;
              this.trwibFinalize.Visible = false;
              this.lblMessage1.Visible = false;
              string script5 = UtilDA.ActiveTabIndex("tabs", 1, "0");
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script5, true);
              string script6 = "TabIndex();";
              System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script6, true);
              break;
            }
            this.trManagementButtons.Visible = false;
            this.trwibFinalize.Visible = true;
            break;
          default:
            this.lblMessage.Visible = false;
            this.lblMessage1.Visible = false;
            this.rfvSupplier.Enabled = false;
            this.rfvAddress.Enabled = false;
            this.rfvPhone1.Enabled = false;
            this.rfvOrganizationIdentifier.Enabled = false;
            this.rfvCountry.Enabled = false;
            this.rfvCurrency.Enabled = false;
            this.wibSave.Text = "Grabar";
            break;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private Supplier GetCurrentSupplier(enmTypeLoadData penuTypeLoadData)
    {
      try
      {
        Supplier currentSupplier = new Supplier();
        int int32 = Convert.ToInt32(this.ViewState["IndexWdgSupplierList"]);
        switch (penuTypeLoadData)
        {
          case enmTypeLoadData.SelectedRowsGrid:
            GridViewRow row = this.wdgSupplierList.Rows[int32];
            currentSupplier.i_SupplierId = int.Parse(this.wdgSupplierList.DataKeys[int32]["i_SupplierId"].ToString());
            currentSupplier.v_Name = row.Cells[2].Text;
            currentSupplier.v_Address = this.Page.Server.HtmlDecode(row.Cells[3].Text);
            currentSupplier.v_Phone1 = row.Cells[4].Text;
            currentSupplier.v_Phone2 = row.Cells[5].Text;
            currentSupplier.v_OrganizationIdentifier = row.Cells[6].Text;
            currentSupplier.v_Country = row.Cells[7].Text;
            currentSupplier.i_CurrencyId = new int?(int.Parse(this.wdgSupplierList.DataKeys[int32]["i_CurrencyId"].ToString()));
            break;
          case enmTypeLoadData.SelectedTextbox:
            if (!string.IsNullOrWhiteSpace(this.hidSupplierId.Value))
              currentSupplier.i_SupplierId = Convert.ToInt32(this.hidSupplierId.Value, (IFormatProvider) CultureInfo.CurrentCulture);
            currentSupplier.v_Name = this.txtv_Name.Text;
            currentSupplier.v_Address = this.txtv_Address.Text;
            currentSupplier.v_Phone1 = this.txtv_Phone1.Text;
            currentSupplier.v_Phone2 = this.txtv_Phone2.Text;
            currentSupplier.v_OrganizationIdentifier = this.txtv_OrganizationIdentifier.Text;
            currentSupplier.v_Country = this.wddCountry.SelectedItem.Text;
            currentSupplier.i_CurrencyId = new int?(Convert.ToInt32(this.wddCurrency.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture));
            break;
        }
        return currentSupplier;
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

    private void ValidateInputData()
    {
      string empty = string.Empty;
      if (!this.ValidateRuc(this.txtv_OrganizationIdentifier.Text))
      {
        this.txtv_OrganizationIdentifier.BackColor = Color.FromArgb(236, 213, 213);
        empty += "<br>  •   &nbsp;&nbsp;&nbsp;ruc de proveedor inválido";
      }
      if (empty != string.Empty)
        throw new HandledException(0, empty);
    }

    private bool ValidateRuc(string pstrOrganizationIdentifier)
    {
      string str = pstrOrganizationIdentifier.Trim();
      int num = 11 - (int.Parse(str.Substring(0, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 5 + int.Parse(str.Substring(1, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 4 + int.Parse(str.Substring(2, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 3 + int.Parse(str.Substring(3, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 2 + int.Parse(str.Substring(4, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 7 + int.Parse(str.Substring(5, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 6 + int.Parse(str.Substring(6, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 5 + int.Parse(str.Substring(7, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 4 + int.Parse(str.Substring(8, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 3 + int.Parse(str.Substring(9, 1), (IFormatProvider) CultureInfo.CurrentCulture) * 2) % 11;
      return (int.Parse(str.Length.ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture) != 11 ? 10 : int.Parse(str.Substring(10, 1), (IFormatProvider) CultureInfo.CurrentCulture)) == (num != 10 ? 0 : 0) + (num != 11 ? 0 : 1) + (num >= 10 ? 0 : num);
    }

    protected void wdgSupplierList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgSupplierList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
  }
}
