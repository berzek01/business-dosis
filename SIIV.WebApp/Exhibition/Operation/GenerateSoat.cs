// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Operation.GenerateSoat
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.BE.CustomCode;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Operation
{
  public class GenerateSoat : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected Label Label2;
    protected TextBox txtPlateSearch;
    protected FilteredTextBoxExtender txtPlateSearch_FilteredTextBoxExtender;
    protected Button wibSearch;
    protected Button wibBulkLoad;
    protected GridView wdgList;
    protected Button btnReturnPopupConfirmation;
    protected Pager custPagerSoat;
    protected Label lblMessage;
    protected UpdatePanel UpdatePanel2;
    protected Label Label3;
    protected TextBox txtPlateNewEdit;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected ValidatorCalloutExtender ValidatorCalloutExtender1;
    protected Label Label1;
    protected TextBox txtSoatNumberEdit;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected ValidatorCalloutExtender ValidatorCalloutExtender2;
    protected Label Label10;
    protected TextBox txtInsurancePolicyNumberEdit;
    protected RequiredFieldValidator RequiredFieldValidator3;
    protected ValidatorCalloutExtender ValidatorCalloutExtender3;
    protected Label Label6;
    protected Fecha wdpExpiredDate;
    protected Label Label12;
    protected DropDownList wddInsuraneEdit;
    protected RequiredFieldValidator RequiredFieldValidator4;
    protected ValidatorCalloutExtender ValidatorCalloutExtender4;
    protected Button wibSave;
    protected Button wibReturn;
    protected Label lblMessage1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      this.lblMessage1.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.ViewState["t"] = (object) Convert.ToString(this.Request.QueryString["t"]);
        this.Session["t"] = (object) Convert.ToString(this.Request.QueryString["t"]);
        this.LoadParameters();
        this.wdpExpiredDate.Value = DateTime.Now;
        this.wdpExpiredDate.dFechaFin = DateTime.Now.AddYears(1);
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
      dtResult.Columns.Add("v_SoatNumber", typeof (string));
      dtResult.Columns.Add("v_InsurancePolicyNumber", typeof (string));
      dtResult.Columns.Add("d_EndDateSoat", typeof (DateTime));
      dtResult.Columns.Add("i_Status", typeof (int));
      dtResult.Columns.Add("v_Description", typeof (string));
      dtResult.Columns.Add("i_RegistrationtId", typeof (int));
      dtResult.Columns.Add("i_ParameterId", typeof (int));
      dtResult.Columns.Add("i_SpecialSoatId", typeof (int));
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      try
      {
        this.SearchSoat();
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

    protected void wibBulkLoad_Click(object sender, EventArgs e)
    {
      this.CreatePopUpServer("Importar Excel", "BulkLoadSoat.aspx", "755px", "500px");
    }

    protected void wdgList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        if (e.CommandName == "EditImg")
        {
          int int32 = Convert.ToInt32(e.CommandArgument);
          GridViewRow row = this.wdgList.Rows[int32];
          if (row == null)
            throw new HandledException(4, "Error de selección.", "'wdgList' - GenerateSoat.aspx");
          string str1 = this.wdgList.DataKeys[int32]["i_Status"].ToString();
          if (str1 == "0" || str1 == "")
          {
            this.ClearControls();
            this.EnabledControls(true);
            this.wibSave.Enabled = true;
            this.lblMessage.Visible = false;
            this.lblMessage1.Visible = false;
          }
          else
          {
            this.EnabledControls(false);
            this.wibSave.Enabled = false;
          }
          this.txtPlateNewEdit.Text = row.Cells[2].Text;
          this.txtSoatNumberEdit.Text = Convert.ToString(this.Page.Server.HtmlDecode(row.Cells[3].Text));
          this.txtInsurancePolicyNumberEdit.Text = Convert.ToString(this.Page.Server.HtmlDecode(row.Cells[4].Text));
          if (row.Cells[5].Text.Length > 6)
            this.wdpExpiredDate.Value = Convert.ToDateTime(row.Cells[5].Text);
          this.ViewState["i_RegistrationtId"] = (object) this.wdgList.DataKeys[int32]["i_RegistrationtId"].ToString();
          string str2 = this.wdgList.DataKeys[int32]["i_ParameterId"].ToString();
          if (str2 == "")
            this.wddInsuraneEdit.SelectedIndex = 0;
          else
            this.wddInsuraneEdit.SelectedValue = str2;
          string script = UtilDA.ActiveTabIndex("tabs", 1, "0");
          System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        }
        else
        {
          int int32 = Convert.ToInt32(e.CommandArgument);
          if (this.wdgList.Rows[int32] == null)
            throw new HandledException(4, "Error de selección.", "'wdgList' - GenerateSoat.aspx");
          if (this.wdgList.DataKeys[int32]["i_SpecialSoatId"].ToString() != null)
          {
            string str = this.wdgList.DataKeys[int32]["i_SpecialSoatId"].ToString().Trim();
            this.Session["i_SpecialSoatId"] = (object) Convert.ToInt32(str == "" ? "0" : str);
            string empty = string.Empty;
            this.CreatePopUpServer("SIIV-Placas Especiales", "../../UserControls/PopupConfirmation.aspx?MessageTypeId=1&MessageText=¿Desea Eliminar el Registro?", "350px", "190px");
          }
        }
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

    protected void custPagerSoat_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        string pstrPlateNew = string.Empty;
        int int32 = Convert.ToInt32(this.ViewState["t"]);
        if (this.txtPlateSearch.Text != "")
          pstrPlateNew = this.txtPlateSearch.Text;
        this.SearchSoatList(pstrPlateNew, int32, false);
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
      try
      {
        DataTable dataTable = new DataTable();
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - GenerateSoat.aspx");
        SpecialPlateSoat pobjSpecialPlateSoat = new SpecialPlateSoat();
        PlateSoatQueriesBL plateSoatQueriesBl = new PlateSoatQueriesBL();
        int int32 = Convert.ToInt32(this.ViewState["t"]);
        if (plateSoatQueriesBl.PlateSoatValid(this.txtPlateNewEdit.Text.Trim(), this.txtInsurancePolicyNumberEdit.Text.Trim(), int32).Rows.Count > 0)
        {
          this.HidePopup();
          throw new HandledException(1, "Ya existe una asociación entre la placa y el número de póliza. </br> Verifíque los datos.");
        }
        if (this.txtInsurancePolicyNumberEdit.Text.Length != 8)
          throw new HandledException(1, "La Póliza " + this.txtInsurancePolicyNumberEdit.Text + " tiene una estructura NO Válida");
        if (this.wddInsuraneEdit.SelectedIndex == 0)
          throw new HandledException(1, "Debe seleccionar la aseguradora correspondiente.");
        if (this.wddInsuraneEdit.SelectedIndex == 1)
          throw new HandledException(1, "Actualmente no se dispone con la aseguradora Rimac.");
        pobjSpecialPlateSoat.i_RegistrationId = Convert.ToInt32(this.ViewState["i_RegistrationtId"].ToString());
        pobjSpecialPlateSoat.v_SoatNumber = this.txtSoatNumberEdit.Text;
        pobjSpecialPlateSoat.v_InsurancePolicyNumber = this.txtInsurancePolicyNumberEdit.Text;
        pobjSpecialPlateSoat.d_StartDateSoat = DateTime.Now;
        pobjSpecialPlateSoat.d_EndDateSoat = this.wdpExpiredDate.Text == "" ? new DateTime?() : new DateTime?(Convert.ToDateTime(this.wdpExpiredDate.Value));
        pobjSpecialPlateSoat.i_InsuranceId = Convert.ToInt32(this.wddInsuraneEdit.SelectedValue);
        pobjSpecialPlateSoat.i_Status = 1;
        pobjSpecialPlateSoat.i_InsertUserId = systemUser.i_SystemUserId;
        pobjSpecialPlateSoat.d_InsertDate = DateTime.Now;
        try
        {
          if (new PlateSoatManagementBL().SpecialPlateSoatInsert(pobjSpecialPlateSoat))
          {
            Message.SetMessage(this.lblMessage1, new HandledException(2, "Se registró correctamente el SOAT"));
            this.EnabledControls(false);
            this.wibSave.Enabled = false;
          }
          else
            Message.SetMessage(this.lblMessage1, new HandledException(1, "Se encontró un problema en el registro del SOAT "));
        }
        catch (Exception ex)
        {
          throw ex;
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

    protected void wibReturn_Click(object sender, EventArgs e)
    {
      try
      {
        if (!this.txtInsurancePolicyNumberEdit.Enabled && this.txtInsurancePolicyNumberEdit.Text != "")
          this.SearchSoat();
        string script = UtilDA.ActiveTabIndex("tabs", 0, "1");
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script, true);
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

    protected void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.Session["i_SpecialSoatId"] != null)
        {
          SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - GenerateSoat.aspx");
          if (new PlateSoatManagementBL().SpecialPlateSoatDelete(new SpecialPlateSoat()
          {
            i_SpecialSoatId = Convert.ToInt32(this.Session["i_SpecialSoatId"].ToString()),
            i_UpdateUserId = systemUser.i_SystemUserId,
            d_UpdateDate = DateTime.Now,
            i_Status = 0
          }))
          {
            Message.SetMessage(this.lblMessage, new HandledException(2, "Se eliminó correctamente el SOAT de la placa."));
            this.SearchSoat();
          }
          else
          {
            Message.SetMessage(this.lblMessage, new HandledException(1, "Se encontró un problema en la eliminación del SOAT de la placa."));
            Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Se encontró un problema en la eliminación del SOAT de la placa.");
          }
        }
        else
          Message.SetMessage(this.lblMessage, new HandledException(1, "Se encontró un problema. </br>Seleccione nuevamente."));
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

    private void LoadParameters()
    {
      try
      {
        SystemParameterManagementBL parameterManagementBl = new SystemParameterManagementBL();
        string str = SystemParameterGroups.SoatInsurance.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        Convert.ToInt32(this.ViewState["t"]);
        ArrayList pobj = new ArrayList()
        {
          (object) str,
          (object) "1,2",
          (object) "1",
          (object) "1"
        };
        List<SIIV.BE.SystemParameter> systemParameterList = parameterManagementBl.Get((object) pobj);
        this.wddInsuraneEdit.Items.Clear();
        foreach (SIIV.BE.SystemParameter systemParameter in systemParameterList)
          this.wddInsuraneEdit.Items.Add(new ListItem(systemParameter.v_Description, systemParameter.i_ParameterId.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
        this.wddInsuraneEdit.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
        this.wddInsuraneEdit.SelectedValue = "0";
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void SearchSoat()
    {
      try
      {
        string pstrPlateNew = string.Empty;
        int int32 = Convert.ToInt32(this.ViewState["t"]);
        if (this.txtPlateSearch.Text != "")
          pstrPlateNew = this.txtPlateSearch.Text;
        this.SearchSoatList(pstrPlateNew, int32, true);
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

    private void SearchSoatList(string pstrPlateNew, int i_PlateTypeId, bool pboolLoadPager)
    {
      try
      {
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerSoat.CurrentPageNumber;
        int pintMaxRows = this.custPagerSoat.CurrentPageSize == 0 ? 10 : this.custPagerSoat.CurrentPageSize;
        int pintTotalRows;
        DataTable all = new PlateSoatQueriesBL().PlateSoatGetAll(pstrPlateNew, i_PlateTypeId, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        if (all == null || all.Rows.Count == 0)
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
        int num = pintTotalRows;
        this.wdgList.DataSource = (object) all;
        this.wdgList.DataBind();
        this.custPagerSoat.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
        this.custPagerSoat.TotalRecordCount = pintTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerSoat.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ClearControls()
    {
      this.txtInsurancePolicyNumberEdit.Text = string.Empty;
      this.txtPlateNewEdit.Text = string.Empty;
      this.txtSoatNumberEdit.Text = string.Empty;
      this.wdpExpiredDate.Value = DateTime.Now;
      this.wdpExpiredDate.dFechaFin = DateTime.Now.AddYears(1);
      this.wddInsuraneEdit.SelectedValue = "0";
    }

    private void EnabledControls(bool enabled)
    {
      this.txtInsurancePolicyNumberEdit.Enabled = enabled;
      this.txtPlateNewEdit.Enabled = enabled;
      this.txtSoatNumberEdit.Enabled = enabled;
      this.wdpExpiredDate.Enabled = enabled;
      this.wddInsuraneEdit.Enabled = enabled;
    }

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void wdgList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    protected void wdgList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
  }
}
