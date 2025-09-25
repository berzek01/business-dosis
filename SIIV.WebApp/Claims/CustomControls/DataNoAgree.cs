// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.CustomControls.DataNoAgree
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims.CustomControls
{
  public class DataNoAgree : UserControl
  {
    protected CheckBox chkItem1;
    protected TextBox txtValueOld1;
    protected TextBox txtValueNew1;
    protected CheckBox chkItem2;
    protected TextBox txtValueOld2;
    protected TextBox txtValueNew2;
    protected CheckBox chkItem3;
    protected TextBox txtValueOld3;
    protected TextBox txtValueNew3;
    protected CheckBox chkItem6;
    protected TextBox txtValueOld6;
    protected TextBox txtValueNew6;
    protected CheckBox chkItem7;
    protected TextBox txtValueOld7;
    protected DropDownList wddValueNew8;
    protected CheckBox chkDataOwner;
    protected RadioButton rbNat;
    protected RadioButton rbJur;
    protected GridView wdgList;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadDocumentType();
    }

    protected void chkItem1_CheckedChanged(object sender, EventArgs e)
    {
      this.txtValueNew1.Enabled = this.chkItem1.Checked;
    }

    protected void chkItem2_CheckedChanged(object sender, EventArgs e)
    {
      this.txtValueNew2.Enabled = this.chkItem2.Checked;
    }

    protected void chkItem3_CheckedChanged(object sender, EventArgs e)
    {
      this.txtValueNew3.Enabled = this.chkItem3.Checked;
    }

    protected void chkItem6_CheckedChanged(object sender, EventArgs e)
    {
      this.txtValueNew6.Enabled = this.chkItem6.Checked;
    }

    protected void chkItem7_CheckedChanged(object sender, EventArgs e)
    {
      this.wddValueNew8.Enabled = this.chkItem7.Checked;
    }

    protected void chkDataOwner_CheckedChanged(object sender, EventArgs e)
    {
      this.wdgList.Enabled = this.chkDataOwner.Checked;
    }

    protected void rbNat_CheckedChanged(object sender, EventArgs e)
    {
      this.wdgList.Columns[2].Visible = this.rbNat.Checked;
      this.wdgList.Columns[3].Visible = this.rbNat.Checked;
      this.wdgList.Columns[4].Visible = this.rbNat.Checked;
      this.wdgList.Columns[5].Visible = !this.rbNat.Checked;
    }

    protected void rbJur_CheckedChanged(object sender, EventArgs e)
    {
      this.wdgList.Columns[2].Visible = !this.rbJur.Checked;
      this.wdgList.Columns[3].Visible = !this.rbJur.Checked;
      this.wdgList.Columns[4].Visible = !this.rbJur.Checked;
      this.wdgList.Columns[5].Visible = this.rbJur.Checked;
    }

    protected void wdgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType != DataControlRowType.DataRow)
        return;
      DataTable table = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) ("" + SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
        (object) "",
        (object) "",
        (object) ""
      });
      DropDownList control1 = e.Row.FindControl("wddComboPrueba") as DropDownList;
      if (table == null || table.Rows.Count == 0)
        return;
      control1.DataSource = (object) new DataView(table)
      {
        RowFilter = "i_ParameterId In(1,2,3,4,32,19,33) "
      };
      control1.DataTextField = "v_Value";
      control1.DataValueField = "i_ParameterId";
      control1.DataBind();
      control1.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
      control1.SelectedValue = "0";
      CheckBox control2 = e.Row.FindControl("chkDataOk") as CheckBox;
      LinkButton control3 = e.Row.FindControl("lnkquit") as LinkButton;
      if (this.Session["dtowner"] is DataTable dataTable)
      {
        control2.Checked = dataTable.Rows.Count > 0 && Convert.ToBoolean(dataTable.Rows[e.Row.RowIndex]["chkDataOk"], (IFormatProvider) CultureInfo.CurrentCulture);
        control3.Text = dataTable.Rows.Count > 0 ? (dataTable.Rows[e.Row.RowIndex]["i_Status"].ToString() == "1" ? "Quitar" : "Revertir") : "Revertir";
        control3.ForeColor = control3.Text == "Quitar" ? Color.Blue : Color.Red;
      }
      this.Session["dtowner"] = (object) dataTable;
    }

    protected void wdgList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      GridViewRow row = this.wdgList.Rows[Convert.ToInt32(e.CommandArgument)];
      if (!(e.CommandName == "Quit"))
        return;
      this.QuitOwner(row);
    }

    public void GenerateDataTable(string datarequester)
    {
      string[] source1 = datarequester.Split('|');
      if (datarequester.Count<char>() == 0)
        return;
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("i_Item", Type.GetType("System.Int32"));
      dataTable.Columns.Add("v_Name", Type.GetType("System.String"));
      dataTable.Columns.Add("v_Description", Type.GetType("System.String"));
      dataTable.Columns.Add("v_DocumentNumber", Type.GetType("System.String"));
      dataTable.Columns.Add("chkDataOk", Type.GetType("System.Boolean"));
      dataTable.Columns.Add("v_LastName1", Type.GetType("System.String"));
      dataTable.Columns.Add("v_LastName2", Type.GetType("System.String"));
      dataTable.Columns.Add("v_Names", Type.GetType("System.String"));
      dataTable.Columns.Add("v_CompanyNew", Type.GetType("System.String"));
      dataTable.Columns.Add("v_DescriptionNew", Type.GetType("System.String"));
      dataTable.Columns.Add("v_DocumentNumberNew", Type.GetType("System.String"));
      dataTable.Columns.Add("i_Status", Type.GetType("System.Int32"));
      dataTable.Columns.Add("v_DescriptionTypeValue", Type.GetType("System.String"));
      string str1 = ((IEnumerable<string>) source1).Count<string>() > 3 ? source1[3] : "";
      string str2 = ((IEnumerable<string>) source1).Count<string>() > 4 ? source1[4] : "";
      string str3 = ((IEnumerable<string>) source1).Count<string>() > 6 ? source1[6] : "";
      string str4 = ((IEnumerable<string>) source1).Count<string>() > 7 ? source1[7] : "";
      string str5 = ((IEnumerable<string>) source1).Count<string>() > 8 ? source1[8] : "";
      string[] strArray1 = str1.Split('/');
      string[] strArray2 = str2.Split('/');
      string[] strArray3 = str3.Split('/');
      string[] source2 = str4.Split('/');
      string[] strArray4 = str5.Split('/');
      for (int index = 0; index < ((IEnumerable<string>) source2).Count<string>(); ++index)
      {
        DataRow row = dataTable.NewRow();
        row["i_Item"] = (object) source2[index];
        row["v_Name"] = (object) strArray1[index];
        row["v_Description"] = (object) strArray3[index];
        row["v_DocumentNumber"] = (object) strArray2[index];
        row["chkDataOk"] = (object) 0;
        row["i_Status"] = (object) 1;
        row["v_DescriptionTypeValue"] = (object) strArray4[index];
        dataTable.Rows.Add(row);
      }
      this.Session["dtowner"] = (object) dataTable;
      this.wdgList.DataSource = (object) dataTable;
      this.wdgList.DataBind();
    }

    public bool CompletedData
    {
      get
      {
        return this.chkItem1.Checked && this.txtValueOld1.Text.Length > 0 || this.chkItem2.Checked && this.txtValueOld2.Text.Length > 0 || this.chkItem3.Checked && this.txtValueOld3.Text.Length > 0 || this.chkDataOwner.Checked || this.chkItem6.Checked && this.txtValueOld6.Text.Length > 0 || this.chkItem7.Checked && this.txtValueOld7.Text.Length > 0;
      }
    }

    public void SetLabels(string strValues)
    {
      if (!(strValues != ""))
        return;
      string[] source = strValues.Split('|');
      if (((IEnumerable<string>) source).Count<string>() > 1)
        this.chkItem1.Text = source[1];
      if (((IEnumerable<string>) source).Count<string>() > 2)
        this.chkItem2.Text = source[2];
      if (((IEnumerable<string>) source).Count<string>() > 3)
        this.chkItem3.Text = source[3];
      if (((IEnumerable<string>) source).Count<string>() > 6)
        this.chkItem6.Text = source[6];
      if (((IEnumerable<string>) source).Count<string>() > 7)
        this.chkItem7.Text = source[7];
    }

    public string GetOldTexts()
    {
      return "" + this.txtValueOld1.Text + "|" + this.txtValueOld2.Text + "|" + this.txtValueOld3.Text + "|" + this.GetOwnerNameOld() + "|" + this.GetOwnerDocumentTypeOld() + "|" + this.GetOwnerDocumentNumberOld() + "|" + this.txtValueOld6.Text + "|" + this.txtValueOld7.Text;
    }

    private string GetOwnerDocumentNumberOld()
    {
      string documentNumberOld = "";
      foreach (TableRow row in this.wdgList.Rows)
      {
        string text = row.Cells[10].Text;
        documentNumberOld = documentNumberOld + text.ToString() + "/";
      }
      return documentNumberOld;
    }

    private string GetOwnerDocumentTypeOld()
    {
      string ownerDocumentTypeOld = "";
      foreach (TableRow row in this.wdgList.Rows)
      {
        string text = row.Cells[9].Text;
        ownerDocumentTypeOld = ownerDocumentTypeOld + text.ToString() + "/";
      }
      return ownerDocumentTypeOld;
    }

    private string GetOwnerNameOld()
    {
      string ownerNameOld = "";
      foreach (TableRow row in this.wdgList.Rows)
      {
        string text = row.Cells[8].Text;
        ownerNameOld = ownerNameOld + text.ToString() + "/";
      }
      return ownerNameOld;
    }

    public string GetNewTexts()
    {
      return "" + this.txtValueNew1.Text + "|" + this.txtValueNew2.Text + "|" + this.txtValueNew3.Text + "|" + this.GetOwnerNameNew() + "|" + this.GetOwnerDocumentTypeNew() + "|" + this.GetOwnerDocumentNumberNew() + "|" + this.txtValueNew6.Text + "|" + this.wddValueNew8.SelectedValue + "|" + this.GetOwnerStatusNew() + "|" + this.GetOwnerItemNew() + "|" + this.GetOwnerDataOk() + "|" + (this.rbNat.Checked ? "1" : "0");
    }

    private string GetOwnerDataOk()
    {
      string ownerDataOk = "";
      int num = 0;
      foreach (GridViewRow row in this.wdgList.Rows)
      {
        CheckBox control = row.FindControl("chkDataOk") as CheckBox;
        ownerDataOk = ownerDataOk + this.wdgList.DataKeys[row.RowIndex]["i_Item"].ToString() + "-" + (control.Checked ? "1" : "0") + "/";
        ++num;
      }
      return ownerDataOk;
    }

    private string GetOwnerItemNew()
    {
      string ownerItemNew = "";
      foreach (DataRow row in (InternalDataCollectionBase) (this.Session["dtowner"] as DataTable).Rows)
        ownerItemNew = ownerItemNew + row["i_Item"]?.ToString() + "/";
      return ownerItemNew;
    }

    private string GetOwnerStatusNew()
    {
      string ownerStatusNew = "";
      foreach (DataRow row in (InternalDataCollectionBase) (this.Session["dtowner"] as DataTable).Rows)
        ownerStatusNew = ownerStatusNew + row["i_Item"].ToString() + "-" + Convert.ToInt32(row["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture).ToString() + "/";
      return ownerStatusNew;
    }

    private string GetOwnerDocumentNumberNew()
    {
      string documentNumberNew = "";
      foreach (GridViewRow row in this.wdgList.Rows)
      {
        TextBox control = row.FindControl("v_DocumentNumberNew") as TextBox;
        documentNumberNew = documentNumberNew + this.wdgList.DataKeys[row.RowIndex]["i_Item"].ToString() + "-" + control.Text.ToString() + "/";
      }
      return documentNumberNew;
    }

    private string GetOwnerDocumentTypeNew()
    {
      string ownerDocumentTypeNew = "";
      foreach (GridViewRow row in this.wdgList.Rows)
      {
        DropDownList control = row.FindControl("wddComboPrueba") as DropDownList;
        ownerDocumentTypeNew = ownerDocumentTypeNew + this.wdgList.DataKeys[row.RowIndex]["i_Item"].ToString() + "-" + control.SelectedValue + "/";
      }
      return ownerDocumentTypeNew;
    }

    private void LoadDocumentType()
    {
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) ("" + SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
        (object) "",
        (object) "",
        (object) ""
      });
      if (dataTable != null && dataTable.Rows.Count != 0)
        ;
    }

    private void QuitOwner(GridViewRow _selectedRow)
    {
      LinkButton control = (LinkButton) _selectedRow.FindControl("lnkquit");
      DataTable dataTable = this.Session["dtowner"] as DataTable;
      int num = 0;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (Convert.ToInt32(row["i_Item"], (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(this.wdgList.DataKeys[_selectedRow.RowIndex]["i_Item"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture))
        {
          row["i_Status"] = (object) (Convert.ToInt32(row["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == 0 ? 1 : 0);
          num = Convert.ToInt32(row["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture);
          break;
        }
      }
      control.Text = num == 1 ? "Quitar" : "Revertir";
      control.ForeColor = num == 1 ? Color.Blue : Color.Red;
      this.Session["dtowner"] = (object) dataTable;
    }

    private string GetOwnerNameNew()
    {
      string ownerNameNew = "";
      foreach (GridViewRow row in this.wdgList.Rows)
      {
        TextBox control1 = row.FindControl("v_LastName1") as TextBox;
        TextBox control2 = row.FindControl("v_LastName2") as TextBox;
        TextBox control3 = row.FindControl("v_Names") as TextBox;
        TextBox textBox = new TextBox();
        if (!this.rbNat.Checked)
          textBox = row.FindControl("v_CompanyNew") as TextBox;
        if (this.rbNat.Checked)
          ownerNameNew = ownerNameNew + this.wdgList.DataKeys[row.RowIndex]["i_Item"].ToString() + "-" + control1.Text.ToString() + "," + control2.Text.ToString() + "," + control3.Text.ToString() + "/";
        else
          ownerNameNew = ownerNameNew + this.wdgList.DataKeys[row.RowIndex]["i_Item"].ToString() + "-" + textBox.Text.ToString() + "/";
      }
      return ownerNameNew;
    }

    public string ValidateDataEmpty()
    {
      string str = "";
      if (this.chkItem1.Checked && this.txtValueNew1.Text.Length == 0)
        str = "***Advertencia</br>Se debe ingresar el campo Modelo del Vehículo";
      if (this.chkItem2.Checked && this.txtValueNew2.Text.Length == 0 && str.Length == 0)
        str = "***Advertencia</br>Se debe ingresar el campo Marca del Vehículo";
      if (this.chkItem3.Checked && this.txtValueNew3.Text.Length == 0 && str.Length == 0)
        str = "***Advertencia</br>Se debe ingresar el campo Serie del Vehículo";
      if (this.chkDataOwner.Checked && str.Length == 0)
      {
        int index1 = 0;
        bool flag1 = false;
        foreach (GridViewRow row in this.wdgList.Rows)
        {
          TextBox control1 = row.FindControl("v_LastName1") as TextBox;
          TextBox control2 = row.FindControl("v_LastName2") as TextBox;
          TextBox control3 = row.FindControl("v_Names") as TextBox;
          TextBox control4 = row.FindControl("v_CompanyNew") as TextBox;
          DropDownList control5 = row.FindControl("wddComboPrueba") as DropDownList;
          TextBox control6 = row.FindControl("v_DocumentNumberNew") as TextBox;
          if (this.rbNat.Checked)
          {
            if (control1.Text != "")
              flag1 = true;
            if (control2.Text != "")
              flag1 = true;
            if (control3.Text != "")
              flag1 = true;
          }
          else if (control4.Text != "")
            flag1 = true;
          if (control5.SelectedValue != "0")
            flag1 = true;
          if (control6.Text != "")
            flag1 = true;
          if (((LinkButton) this.wdgList.Rows[index1].FindControl("lnkquit")).Text == "Revertir")
            flag1 = true;
          ++index1;
        }
        if (!flag1)
          str = "***Advertencia</br>Debe modificar algún dato del propietario o quitar el check [Datos del Propietario es correcto?]";
        if (flag1)
        {
          int index2 = 0;
          foreach (GridViewRow row in this.wdgList.Rows)
          {
            bool flag2 = false;
            TextBox control7 = row.FindControl("v_LastName1") as TextBox;
            TextBox control8 = row.FindControl("v_LastName2") as TextBox;
            TextBox control9 = row.FindControl("v_Names") as TextBox;
            TextBox control10 = row.FindControl("v_CompanyNew") as TextBox;
            DropDownList control11 = row.FindControl("wddComboPrueba") as DropDownList;
            TextBox control12 = row.FindControl("v_DocumentNumberNew") as TextBox;
            if (this.rbNat.Checked)
            {
              if (control7.Text != "")
                flag2 = true;
              if (control8.Text != "")
                flag2 = true;
              if (control9.Text != "")
                flag2 = true;
            }
            else if (control10.Text != "")
              flag2 = true;
            if (control11.SelectedValue != "0")
              flag2 = true;
            if (control12.Text != "")
              flag2 = true;
            CheckBox control13 = (CheckBox) this.wdgList.Rows[index2].FindControl("chkDataOk");
            LinkButton control14 = (LinkButton) this.wdgList.Rows[index2].FindControl("lnkquit");
            if (control13.Checked)
              flag2 = true;
            if (control14.Text == "Revertir")
              flag2 = true;
            if (!flag2)
            {
              str = "***Advertencia</br>En el " + (index2 + 1).ToString((IFormatProvider) CultureInfo.CurrentCulture) + "° Propietario debe especificar si los datos son correctos, quitar o modificar algun dato";
              break;
            }
            ++index2;
          }
        }
      }
      if (this.chkItem6.Checked && this.txtValueNew6.Text.Length == 0 && str.Length == 0)
        str = "***Advertencia</br>Se debe ingresar el campo Placa Antigua";
      if (this.chkItem7.Checked && this.wddValueNew8.SelectedValue == "-1" && str.Length == 0)
        str = "***Advertencia</br>Se debe ingresar el campo Categoría Vehicular";
      if (this.chkDataOwner.Checked && str.Length == 0)
      {
        DataTable dataTable = this.Session["dtowner"] as DataTable;
        int num = 0;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (Convert.ToInt32(row["i_Status"], (IFormatProvider) CultureInfo.CurrentCulture) == 0)
            ++num;
        }
        if (num == dataTable.Rows.Count)
          str = "***Advertencia</br>La placa debe tener al menos un propietario asociado";
      }
      return str;
    }

    public string ValidateDataEqual()
    {
      string str = "";
      if (this.chkItem1.Checked)
      {
        if (this.txtValueOld1.Text.Trim() == this.txtValueNew1.Text.Trim())
          str = "***Advertencia</br>El valor del campo Modelo del Vehículo [Dice] no debe ser igual al valor del campo Modelo del Vehículo [Debe decir]";
      }
      else if (this.chkItem2.Checked)
      {
        if (this.txtValueOld2.Text.Trim() == this.txtValueNew2.Text.Trim())
          str = "***Advertencia</br>El valor del campo Marca del Vehículo [Dice] no debe ser igual al valor del campo Marca del Vehículo [Debe decir]";
      }
      else if (this.chkItem3.Checked)
      {
        if (this.txtValueOld3.Text.Trim() == this.txtValueNew3.Text.Trim())
          str = "***Advertencia</br>El valor del campo Serie del Vehículo [Dice] no debe ser igual al valor del campo Serie del Vehículo [Debe decir]";
      }
      else if (this.chkItem6.Checked)
      {
        if (this.txtValueOld6.Text.Trim() == this.txtValueNew6.Text.Trim())
          str = "***Advertencia</br>El valor del campo Placa Antigua [Dice] no debe ser igual al valor del campo Placa Antigua [Debe decir]";
      }
      else if (this.chkItem7.Checked && this.wddValueNew8.SelectedValue == "-1" && this.txtValueOld7.Text.Trim() == this.wddValueNew8.SelectedValue)
        str = "***Advertencia</br>El valor del campo Categoría Vehicular [Dice] no debe ser igual al valor del campo Categoría Vehicular [Debe decir]";
      return str;
    }

    public bool ValidateCheckSome()
    {
      return this.chkItem1.Checked || this.chkItem2.Checked || this.chkItem3.Checked || this.chkDataOwner.Checked || this.chkItem6.Checked || this.chkItem7.Checked;
    }

    public void SetOldTexts(string strValues)
    {
      string[] source = strValues.Split('|');
      if (strValues.Count<char>() == 0)
        return;
      if (((IEnumerable<string>) source).Count<string>() > 0)
      {
        this.txtValueOld1.Text = ((IEnumerable<string>) source).Count<string>() > 0 ? source[0] : "";
        this.chkItem1.Checked = false;
      }
      if (((IEnumerable<string>) source).Count<string>() > 1)
      {
        this.txtValueOld2.Text = ((IEnumerable<string>) source).Count<string>() > 1 ? source[1] : "";
        this.chkItem2.Checked = false;
      }
      if (((IEnumerable<string>) source).Count<string>() > 2)
      {
        this.txtValueOld3.Text = ((IEnumerable<string>) source).Count<string>() > 2 ? source[2] : "";
        this.chkItem3.Checked = false;
      }
      if (((IEnumerable<string>) source).Count<string>() > 5)
      {
        this.txtValueOld6.Text = source[5];
        this.chkItem6.Checked = false;
      }
      if (((IEnumerable<string>) source).Count<string>() <= 7)
        return;
      this.txtValueOld7.Text = source[7];
      this.chkItem7.Checked = false;
    }

    public void SetNewTexts(string strValues, string readvalues)
    {
      string[] source1 = strValues.Split('|');
      if (((IEnumerable<string>) source1).Count<string>() == 0)
        return;
      this.SetNewListGroup("204");
      if (((IEnumerable<string>) source1).Count<string>() > 0)
      {
        this.txtValueNew1.Text = source1[0];
        this.chkItem1.Checked = source1[0] != "";
        this.txtValueNew1.Enabled = this.chkItem1.Checked;
      }
      if (((IEnumerable<string>) source1).Count<string>() > 1)
      {
        this.txtValueNew2.Text = source1[1];
        this.chkItem2.Checked = source1[1] != "";
        this.txtValueNew2.Enabled = this.chkItem2.Checked;
      }
      if (((IEnumerable<string>) source1).Count<string>() > 2)
      {
        this.txtValueNew3.Text = source1[2];
        this.chkItem3.Checked = source1[2] != "";
        this.txtValueNew3.Enabled = this.chkItem3.Checked;
      }
      if (readvalues.Length != 0)
      {
        string[] source2 = readvalues.Split('|');
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("i_Item", Type.GetType("System.Int32"));
        dataTable.Columns.Add("v_Name", Type.GetType("System.String"));
        dataTable.Columns.Add("v_Description", Type.GetType("System.String"));
        dataTable.Columns.Add("v_DocumentNumber", Type.GetType("System.String"));
        dataTable.Columns.Add("chkDataOk", Type.GetType("System.Boolean"));
        dataTable.Columns.Add("v_LastName1", Type.GetType("System.String"));
        dataTable.Columns.Add("v_LastName2", Type.GetType("System.String"));
        dataTable.Columns.Add("v_Names", Type.GetType("System.String"));
        dataTable.Columns.Add("v_CompanyNew", Type.GetType("System.String"));
        dataTable.Columns.Add("v_DescriptionNew", Type.GetType("System.String"));
        dataTable.Columns.Add("v_DocumentNumberNew", Type.GetType("System.String"));
        dataTable.Columns.Add("i_Status", Type.GetType("System.Int32"));
        dataTable.Columns.Add("v_DescriptionTypeValue", Type.GetType("System.String"));
        string str1 = ((IEnumerable<string>) source2).Count<string>() > 3 ? source2[3] : "";
        string str2 = ((IEnumerable<string>) source2).Count<string>() > 4 ? source2[4] : "";
        string str3 = ((IEnumerable<string>) source2).Count<string>() > 5 ? source2[5] : "";
        string str4 = ((IEnumerable<string>) source1).Count<string>() > 8 ? source1[8] : "";
        string str5 = ((IEnumerable<string>) source1).Count<string>() > 9 ? source1[9] : "";
        string str6 = ((IEnumerable<string>) source1).Count<string>() > 10 ? source1[10] : "";
        string str7 = ((IEnumerable<string>) source1).Count<string>() > 11 ? source1[11] : "";
        this.rbNat.Checked = str7 == "1";
        if (this.rbNat.Checked)
          this.rbNat_CheckedChanged((object) null, (EventArgs) null);
        this.rbJur.Checked = str7 == "0";
        if (this.rbJur.Checked)
          this.rbJur_CheckedChanged((object) null, (EventArgs) null);
        string[] source3 = str1.Split('/');
        string[] source4 = str3.Split('/');
        string[] source5 = str2.Split('/');
        string str8 = ((IEnumerable<string>) source1).Count<string>() > 3 ? source1[3] : "";
        string str9 = ((IEnumerable<string>) source1).Count<string>() > 4 ? source1[4] : "";
        string str10 = ((IEnumerable<string>) source1).Count<string>() > 5 ? source1[5] : "";
        string[] source6 = str8.Split('/');
        string[] source7 = str9.Split('/');
        string[] source8 = str10.Split('/');
        string[] source9 = str4.Split('/');
        string[] source10 = str5.Split('/');
        string[] source11 = str6.Split('/');
        for (int index = 0; index < ((IEnumerable<string>) source10).Count<string>(); ++index)
        {
          if (source10[index] != "")
          {
            DataRow row = dataTable.NewRow();
            row["i_Item"] = (object) source10[index];
            row["v_Name"] = ((IEnumerable<string>) source3).Count<string>() > index ? (object) source3[index] : (object) "";
            row["v_Description"] = ((IEnumerable<string>) source5).Count<string>() > index ? (object) source5[index] : (object) "";
            row["v_DocumentNumber"] = ((IEnumerable<string>) source4).Count<string>() > index ? (object) source4[index] : (object) "";
            row["v_DescriptionTypeValue"] = ((IEnumerable<string>) source5).Count<string>() > index ? (object) source5[index] : (object) "";
            string[] strArray1;
            if (((IEnumerable<string>) source11).Count<string>() <= index)
              strArray1 = new string[0];
            else
              strArray1 = source11[index].Split('-');
            string[] source12 = strArray1;
            row["chkDataOk"] = (object) (bool) (((IEnumerable<string>) source12).Count<string>() > 1 ? (source12[1] == "1" ? 1 : 0) : 0);
            string[] strArray2;
            if (((IEnumerable<string>) source6).Count<string>() <= index)
              strArray2 = new string[0];
            else
              strArray2 = source6[index].Split('-');
            string[] source13 = strArray2;
            string str11 = ((IEnumerable<string>) source13).Count<string>() > 0 ? source13[0] : "";
            if (str11 != "")
            {
              string[] source14 = source6[index].Substring(str11.Length + 1).Split(',');
              row["v_LastName1"] = this.rbNat.Checked ? (((IEnumerable<string>) source14).Count<string>() > 0 ? (object) source14[0] : (object) "") : (object) "";
              row["v_LastName2"] = this.rbNat.Checked ? (((IEnumerable<string>) source14).Count<string>() > 1 ? (object) source14[1] : (object) "") : (object) "";
              row["v_Names"] = this.rbNat.Checked ? (((IEnumerable<string>) source14).Count<string>() > 2 ? (object) source14[2] : (object) "") : (object) "";
              row["v_CompanyNew"] = this.rbJur.Checked ? (((IEnumerable<string>) source14).Count<string>() > 0 ? (object) source14[0] : (object) "") : (object) "";
            }
            else
            {
              row["v_LastName1"] = (object) "";
              row["v_LastName2"] = (object) "";
              row["v_Names"] = (object) "";
              row["v_CompanyNew"] = (object) "";
            }
            string[] strArray3;
            if (((IEnumerable<string>) source7).Count<string>() <= index)
              strArray3 = new string[0];
            else
              strArray3 = source7[index].Split('-');
            string[] source15 = strArray3;
            row["v_DescriptionNew"] = ((IEnumerable<string>) source15).Count<string>() > 1 ? (object) source15[1] : (object) "";
            string[] strArray4;
            if (((IEnumerable<string>) source8).Count<string>() <= index)
              strArray4 = new string[0];
            else
              strArray4 = source8[index].Split('-');
            string[] source16 = strArray4;
            row["v_DocumentNumberNew"] = ((IEnumerable<string>) source16).Count<string>() > 1 ? (object) source16[1] : (object) "";
            string[] strArray5;
            if (((IEnumerable<string>) source9).Count<string>() <= index)
              strArray5 = new string[0];
            else
              strArray5 = source9[index].Split('-');
            string[] source17 = strArray5;
            row["i_Status"] = ((IEnumerable<string>) source17).Count<string>() > 1 ? (source17[1] != "" ? (object) source17[1] : (object) "1") : (object) "1";
            dataTable.Rows.Add(row);
          }
        }
        this.Session["dtowner"] = (object) dataTable;
        this.wdgList.DataSource = (object) dataTable;
        this.wdgList.DataBind();
        foreach (GridViewRow row in this.wdgList.Rows)
        {
          if (this.rbNat.Checked)
          {
            string str12 = dataTable.Rows[row.RowIndex]["v_LastName1"].ToString();
            (row.FindControl("v_LastName1") as TextBox).Text = str12.ToString();
            string str13 = dataTable.Rows[row.RowIndex]["v_LastName2"].ToString();
            (row.FindControl("v_LastName2") as TextBox).Text = str13.ToString();
            string str14 = dataTable.Rows[row.RowIndex]["v_Names"].ToString();
            (row.FindControl("v_Names") as TextBox).Text = str14.ToString();
          }
          else
          {
            string str15 = dataTable.Rows[row.RowIndex]["v_CompanyNew"].ToString();
            (row.FindControl("v_CompanyNew") as TextBox).Text = str15.ToString();
          }
          string str16 = dataTable.Rows[row.RowIndex]["v_DescriptionNew"].ToString();
          (row.FindControl("wddComboPrueba") as DropDownList).SelectedValue = str16.ToString();
          string str17 = dataTable.Rows[row.RowIndex]["v_DocumentNumberNew"].ToString();
          (row.FindControl("v_DocumentNumberNew") as TextBox).Text = str17.ToString();
        }
        bool flag = false;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (row["v_LastName1"].ToString() != "")
            flag = true;
          if (row["v_LastName2"].ToString() != "")
            flag = true;
          if (row["v_Names"].ToString() != "")
            flag = true;
          if (row["v_CompanyNew"].ToString() != "")
            flag = true;
          if (row["v_DescriptionNew"].ToString() != "")
            flag = true;
          if (row["v_DocumentNumberNew"].ToString() != "")
            flag = true;
        }
        if (flag)
          this.chkDataOwner.Checked = true;
        this.wdgList.Enabled = this.chkDataOwner.Checked;
      }
      if (((IEnumerable<string>) source1).Count<string>() > 6)
      {
        this.txtValueNew6.Text = source1[6];
        this.chkItem6.Checked = source1[6] != "";
        this.txtValueNew6.Enabled = this.chkItem6.Checked;
      }
      if (((IEnumerable<string>) source1).Count<string>() <= 7)
        return;
      this.wddValueNew8.SelectedValue = source1[7];
      this.chkItem7.Checked = source1[7] != "" && source1[7] != "0";
      this.wddValueNew8.Enabled = this.chkItem7.Checked;
    }

    public void SetNewListGroup(string strValues)
    {
      DataTable group = new RequirementQueriesBL().GetGroup(strValues.Split('|')[0]);
      this.wddValueNew8.Items.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) group.Rows)
        this.wddValueNew8.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
      this.wddValueNew8.Items.Insert(0, new ListItem("- Seleccione - ", "0"));
    }

    public void ClearControls()
    {
      this.txtValueOld1.Text = "";
      this.txtValueOld2.Text = "";
      this.txtValueOld3.Text = "";
      this.txtValueOld6.Text = "";
      this.txtValueOld7.Text = "";
      this.txtValueNew1.Text = "";
      this.txtValueNew2.Text = "";
      this.txtValueNew3.Text = "";
      this.txtValueNew6.Text = "";
      this.wddValueNew8.SelectedValue = "0";
      this.chkItem1.Checked = false;
      this.chkItem2.Checked = false;
      this.chkItem3.Checked = false;
      this.chkItem6.Checked = false;
      this.chkItem7.Checked = false;
    }

    public void EnableControls(bool enabled)
    {
      this.txtValueOld1.Enabled = enabled;
      this.txtValueOld2.Enabled = enabled;
      this.txtValueOld3.Enabled = enabled;
      this.txtValueOld6.Enabled = enabled;
      this.txtValueOld7.Enabled = enabled;
      this.chkItem1.Enabled = enabled;
      this.chkItem2.Enabled = enabled;
      this.chkItem3.Enabled = enabled;
      this.chkItem6.Enabled = enabled;
      this.chkItem7.Enabled = enabled;
    }

    public void SetNewIdList(string strValues)
    {
      string[] source = strValues.Split('|');
      if (((IEnumerable<string>) source).Count<string>() <= 0)
        return;
      this.Session["strIds"] = (object) source[0];
    }

    public void SetNewTextsList(string strText)
    {
      string[] source = strText.Split('|');
      if (((IEnumerable<string>) source).Count<string>() <= 0)
        return;
      this.txtValueOld7.Text = source[0];
    }
  }
}
