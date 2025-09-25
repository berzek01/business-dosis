// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.CustomControls.RequesterData
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims.CustomControls
{
  public class RequesterData : UserControl
  {
    protected Label lblFirstName;
    protected TextBox txtFirstName;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected ValidatorCalloutExtender RequiredFieldValidator1_ValidatorCalloutExtender;
    protected Label lblLastName;
    protected TextBox txtLastName;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected ValidatorCalloutExtender RequiredFieldValidator2_ValidatorCalloutExtender;
    protected Label lblDocument;
    protected DropDownList wddDocumentType;
    protected TextBox txtDocumentNumber;
    protected FilteredTextBoxExtender txtDocumentNumber_FilteredTextBoxExtender;
    protected RequiredFieldValidator RequiredFieldValidator3;
    protected ValidatorCalloutExtender RequiredFieldValidator3_ValidatorCalloutExtender;
    protected Label lblEmail;
    protected TextBox txtEmail;
    protected RegularExpressionValidator RegularExpressionValidator1;
    protected ValidatorCalloutExtender RegularExpressionValidator1_ValidatorCalloutExtender;
    protected Label lblTelephone;
    protected TextBox txtTelephone;
    protected FilteredTextBoxExtender txtTelephone_FilteredTextBoxExtender;

    public string DocumentNumber => this.txtDocumentNumber.Text;

    public string Email => this.txtEmail.Text;

    public string TelephoneNumber => this.txtTelephone.Text;

    public string DocumentType => this.wddDocumentType.SelectedValue;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadParameters();
      this.txtDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Numbers;
      this.txtDocumentNumber.MaxLength = 8;
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

    private void LoadParameters()
    {
      SystemParameterQueriesBL parameterQueriesBl = new SystemParameterQueriesBL();
      string str = "";
      string[] source = "1,2,3,4,32,19,33".Split(',');
      ArrayList arrFilter = new ArrayList()
      {
        (object) (str + SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
        (object) "",
        (object) "",
        (object) ""
      };
      DataTable dataTable = parameterQueriesBl.GetbyFilter(arrFilter);
      this.wddDocumentType.Items.Clear();
      if (dataTable.Rows.Count <= 0)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        DataRow item = row;
        if (item["i_GroupId"].ToString() == SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture) && ((IEnumerable<string>) source).Where<string>((System.Func<string, bool>) (x => x == item["i_ParameterId"].ToString())).Count<string>() == 1)
          this.wddDocumentType.Items.Add(new ListItem(item["v_Value"].ToString(), item["i_ParameterId"].ToString()));
      }
      this.wddDocumentType.SelectedValue = "1";
    }

    public void SetLabels(string strRequester)
    {
      this.LoadParameters();
      string[] source = strRequester.Split('|');
      if (((IEnumerable<string>) source).Count<string>() > 1)
        this.lblFirstName.Text = source[1];
      if (((IEnumerable<string>) source).Count<string>() > 2)
        this.lblLastName.Text = source[2];
      if (((IEnumerable<string>) source).Count<string>() > 3)
        this.lblDocument.Text = source[3];
      if (((IEnumerable<string>) source).Count<string>() > 4)
        this.lblEmail.Text = source[4];
      if (((IEnumerable<string>) source).Count<string>() <= 5)
        return;
      this.lblTelephone.Text = source[5];
    }

    public string GetTexts()
    {
      return "" + this.txtFirstName.Text + "|" + this.txtLastName.Text + "|" + this.wddDocumentType.SelectedValue + "|" + this.txtDocumentNumber.Text + "|" + this.txtEmail.Text + "|" + this.txtTelephone.Text;
    }

    public void SetTexts(string strValues)
    {
      if (!(strValues != ""))
        return;
      this.LoadParameters();
      string[] source = strValues.Split('|');
      if (((IEnumerable<string>) source).Count<string>() > 0)
        this.txtFirstName.Text = source[0];
      if (((IEnumerable<string>) source).Count<string>() > 1)
        this.txtLastName.Text = source[1];
      if (((IEnumerable<string>) source).Count<string>() > 2)
        this.wddDocumentType.SelectedValue = source[2];
      if (((IEnumerable<string>) source).Count<string>() > 3)
        this.txtDocumentNumber.Text = source[3];
      if (((IEnumerable<string>) source).Count<string>() > 4)
        this.txtEmail.Text = source[4];
      if (((IEnumerable<string>) source).Count<string>() > 5)
        this.txtTelephone.Text = source[5];
    }

    public void SetReadOnly(bool bolStatus)
    {
      this.txtFirstName.Enabled = !bolStatus;
      this.txtLastName.Enabled = !bolStatus;
      this.wddDocumentType.Enabled = !bolStatus;
      this.txtDocumentNumber.Enabled = !bolStatus;
      this.txtEmail.Enabled = !bolStatus;
      this.txtTelephone.Enabled = !bolStatus;
    }

    public void ClearControls()
    {
      this.txtFirstName.Text = "";
      this.txtLastName.Text = "";
      this.txtDocumentNumber.Text = "";
      this.txtEmail.Text = "";
      this.txtTelephone.Text = "";
    }

    public void EnabledControls(bool enable)
    {
      this.txtFirstName.Enabled = enable;
      this.txtLastName.Enabled = enable;
      this.wddDocumentType.Enabled = enable;
      this.txtDocumentNumber.Enabled = enable;
      this.txtEmail.Enabled = enable;
      this.txtTelephone.Enabled = enable;
    }
  }
}
