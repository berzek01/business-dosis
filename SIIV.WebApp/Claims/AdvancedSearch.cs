// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.AdvancedSearch
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.Common.Resource;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims
{
  public class AdvancedSearch : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected Label Label1;
    protected TextBox txtPlateNew;
    protected Label Label2;
    protected TextBox txtTitleNumber;
    protected Label Label3;
    protected TextBox txtRequirementPlateId;
    protected Label lblFirstName;
    protected TextBox txtFirstName;
    protected Label lblLastName;
    protected TextBox txtLastName;
    protected Label lblDocument;
    protected DropDownList wddDocumentType;
    protected TextBox txtDocumentNumber;
    protected FilteredTextBoxExtender txtDocumentNumber_FilteredTextBoxExtender;
    protected Label lblEmail;
    protected TextBox txtEmail;
    protected Label lblTelephone;
    protected TextBox txtTelephone;
    protected Button wibAccept;
    protected Button wibCancel;
    protected Label lblMsg;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadParameters();
      this.SetearControls();
    }

    protected void wddDocumentType_SelectionChanged(object sender, EventArgs e)
    {
      if (this.wddDocumentType.SelectedValue == "1")
      {
        this.txtDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Numbers;
        this.txtDocumentNumber.MaxLength = 8;
      }
      else if (this.wddDocumentType.SelectedValue == "4")
      {
        this.txtDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Numbers;
        this.txtDocumentNumber.MaxLength = 11;
      }
      else
      {
        this.txtDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Custom;
        this.txtDocumentNumber_FilteredTextBoxExtender.ValidChars = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz1234567890";
        this.txtDocumentNumber.MaxLength = 20;
      }
    }

    protected void wibAccept_Click(object sender, EventArgs e)
    {
      if (!this.ValidateParamters())
        return;
      this.Session["v_advancedSearch"] = (object) (this.txtPlateNew.Text + "|" + this.txtTitleNumber.Text + "|" + this.txtRequirementPlateId.Text + "|" + this.txtFirstName.Text + "|" + this.txtLastName.Text + "|" + this.wddDocumentType.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "|" + this.txtDocumentNumber.Text + "|" + this.txtEmail.Text + "|" + this.txtTelephone.Text);
      this.SendInformation();
    }

    protected void wibCancel_Click(object sender, EventArgs e)
    {
      if (this.Session["v_advancedSearch"] != null && this.lblMsg.Text.Length == 0)
        this.Session["v_advancedSearch"] = (object) (this.txtPlateNew.Text + "|" + this.txtTitleNumber.Text + "|" + this.txtRequirementPlateId.Text + "|" + this.txtFirstName.Text + "|" + this.txtLastName.Text + "|" + this.wddDocumentType.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "|" + this.txtDocumentNumber.Text + "|" + this.txtEmail.Text + "|" + this.txtTelephone.Text);
      this.PopupClose();
    }

    private void LoadParameters()
    {
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) ("" + SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
        (object) "",
        (object) "1",
        (object) "1"
      });
      this.wddDocumentType.Items.Clear();
      if (dataTable.Rows.Count <= 0)
        return;
      this.wddDocumentType.Items.Add(new ListItem("--", ""));
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (row["i_GroupId"].ToString() == SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture))
          this.wddDocumentType.Items.Add(new ListItem(row["v_Value"].ToString(), row["i_ParameterId"].ToString()));
      }
    }

    private void SetearControls()
    {
      if (this.Session["v_advancedSearch"] == null)
        return;
      string[] source = this.Session["v_advancedSearch"].ToString().Split('|');
      this.txtPlateNew.Text = ((IEnumerable<string>) source).Count<string>() > 0 ? source[0] : "";
      this.txtTitleNumber.Text = ((IEnumerable<string>) source).Count<string>() > 1 ? source[1] : "";
      this.txtRequirementPlateId.Text = ((IEnumerable<string>) source).Count<string>() > 2 ? source[2] : "";
      this.txtFirstName.Text = ((IEnumerable<string>) source).Count<string>() > 3 ? source[3] : "";
      this.txtLastName.Text = ((IEnumerable<string>) source).Count<string>() > 4 ? source[4] : "";
      this.wddDocumentType.SelectedValue = ((IEnumerable<string>) source).Count<string>() > 5 ? source[5] : "0";
      this.txtDocumentNumber.Text = ((IEnumerable<string>) source).Count<string>() > 6 ? source[6] : "";
      this.txtEmail.Text = ((IEnumerable<string>) source).Count<string>() > 7 ? source[7] : "";
      this.txtTelephone.Text = ((IEnumerable<string>) source).Count<string>() > 8 ? source[8] : "";
    }

    private bool ValidateParamters()
    {
      bool flag = false;
      if (this.txtPlateNew.Text.Length > 0)
        flag = true;
      else if (this.txtTitleNumber.Text.Length > 0)
        flag = true;
      else if (this.txtRequirementPlateId.Text.Length > 0)
        flag = true;
      else if (this.txtFirstName.Text.Length > 0)
        flag = true;
      else if (this.txtLastName.Text.Length > 0)
        flag = true;
      else if (this.txtDocumentNumber.Text.Length > 0)
        flag = true;
      else if (this.txtEmail.Text.Length > 0)
        flag = true;
      else if (this.txtTelephone.Text.Length > 0)
        flag = true;
      if (flag)
        return true;
      Message.SetMessage(this.lblMsg, enmMessageType.Warning, "Debe seleccionar un tipo de filtro");
      return false;
    }

    private void SendInformation()
    {
      string script = "SendInfoPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void PopupClose()
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }
  }
}
