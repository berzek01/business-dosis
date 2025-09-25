// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Reports.DataBankReport
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.Reports.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Reports
{
  public class DataBankReport : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected DropDownList wddBank;
    protected Fecha wdpStartDate;
    protected Fecha wdpEndDate;
    protected TextBox txtPlateNumber;
    protected TextBox txtTerminal;
    protected Button wibSearch;
    protected GridView wdgOperations;
    protected Pager custPagerBank;
    protected Fecha wdpFilterDate;
    protected Label lblBankQuantity;
    protected Label lblTotalQuantity;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadBanks();
      this.LoadDate();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
      if (this.txtPlateNumber.Text.Length == 0)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Debe ingresar el número de placa o título");
        this.HidePopup();
      }
      else if (this.CharacterCount() > 1)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Debe ingresar solo un comodín(%) en el número de placa o título");
        this.HidePopup();
      }
      else
      {
        this.lblMessage.Visible = false;
        this.SearchDataBank();
      }
    }

    protected void wdpFilterDate_ValueChanged(object sender, EventArgs e) => this.SetearQuantity();

    protected void custPagerBank_PageChanged(object sender, CustomPageChangeArgs e)
    {
      this.SearchDataBankList(Convert.ToInt32(this.wddBank.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToDateTime((object) this.wdpStartDate.Value, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToDateTime((object) this.wdpEndDate.Value, (IFormatProvider) CultureInfo.CurrentCulture), this.txtPlateNumber.Text.TrimEnd(), this.txtTerminal.Text.TrimEnd(), false);
    }

    private void LoadBanks()
    {
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) ("" + SystemParameterGroups.AffiliatedBank.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
        (object) "",
        (object) "1",
        (object) "1"
      });
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          this.wddBank.Items.Add(new ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
      }
      this.wddBank.Items.Insert(0, new ListItem("- Todos - ", "0"));
    }

    private void LoadDate()
    {
      this.wdpStartDate.Value = DateTime.Now.AddMonths(-1);
      this.wdpEndDate.Value = DateTime.Now;
      this.wdpFilterDate.Value = DateTime.Now;
    }

    private void SearchDataBank()
    {
      try
      {
        this.SearchDataBankList(Convert.ToInt32(this.wddBank.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToDateTime((object) this.wdpStartDate.Value, (IFormatProvider) CultureInfo.CurrentCulture), Convert.ToDateTime((object) this.wdpEndDate.Value, (IFormatProvider) CultureInfo.CurrentCulture), this.txtPlateNumber.Text.TrimEnd(), this.txtTerminal.Text.TrimEnd(), true);
        this.HidePopup();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, ex.Message);
        this.HidePopup();
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SearchDataBankList(
      int pi_BankId,
      DateTime pd_StartDate,
      DateTime pd_EndDate,
      string pv_platenumber,
      string pv_BankOperationTerminal,
      bool pboolLoadPager)
    {
      int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerBank.CurrentPageNumber;
      int pintMaxRows = this.custPagerBank.CurrentPageSize == 0 ? 10 : this.custPagerBank.CurrentPageSize;
      int pintTotalRows;
      DataTable detailsBank = new ReportManagementBL().GetDetailsBank(pi_BankId, pd_StartDate, pd_EndDate, pv_platenumber, pv_BankOperationTerminal, pintStartRowIndex, pintMaxRows, out pintTotalRows);
      if (detailsBank == null || detailsBank.Rows.Count == 0)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se encontró información con los criterios de búsqueda seleccionados");
        this.HidePopup();
      }
      else
        this.lblMessage.Visible = false;
      int num = pintTotalRows;
      this.wdgOperations.DataSource = (object) detailsBank;
      this.wdgOperations.DataBind();
      this.Session["dtFull"] = (object) detailsBank;
      this.SetearQuantity();
      this.custPagerBank.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
      this.custPagerBank.TotalRecordCount = pintTotalRows;
      if (!pboolLoadPager)
        return;
      this.custPagerBank.LoadPager();
    }

    private void SetearQuantity()
    {
      if (!(this.Session["dtFull"] is DataTable table1) || table1.Rows.Count == 0)
        return;
      DateTime dateTime1 = Convert.ToDateTime((object) this.wdpFilterDate.Value, (IFormatProvider) CultureInfo.CurrentCulture);
      DateTime dateTime2 = new DateTime(dateTime1.Year, dateTime1.Month, dateTime1.Day, 0, 0, 0);
      DateTime dateTime3 = new DateTime(dateTime1.Year, dateTime1.Month, dateTime1.Day, 23, 59, 59);
      DataTable table2 = new DataView(table1)
      {
        RowFilter = ("d_OperationDate >= '" + dateTime2.ToString("dd/MM/yyyy", (IFormatProvider) CultureInfo.CurrentCulture) + "' and d_OperationDate <= '" + dateTime3.ToString("dd/MM/yyyy", (IFormatProvider) CultureInfo.CurrentCulture) + "'")
      }.ToTable();
      List<string> stringList = new List<string>();
      foreach (DataRow row in (InternalDataCollectionBase) table1.Rows)
      {
        bool flag = false;
        foreach (string str in stringList)
        {
          if (row["v_BankName"].ToString() == str)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          stringList.Add(row["v_BankName"].ToString());
      }
      this.lblBankQuantity.Text = "";
      foreach (string str in stringList)
      {
        DataView dataView = new DataView(table2);
        dataView.RowFilter = "v_BankName = '" + str + "'";
        DataTable table3 = dataView.ToTable();
        this.lblBankQuantity.Text = this.lblBankQuantity.Text + " TOTAL DÍA " + dateTime2.ToString("dd/MM/yyyy", (IFormatProvider) CultureInfo.CurrentCulture) + " " + str + " : " + table3.Rows.Count.ToString() + "                 ";
        dataView.RowFilter = "";
      }
      this.lblTotalQuantity.Text = "";
      this.lblTotalQuantity.Text = "TOTALES DÍA " + dateTime2.ToString("dd/MM/yyyy", (IFormatProvider) CultureInfo.CurrentCulture) + " : " + table2.Rows.Count.ToString();
      this.lblTotalQuantity.Text = this.lblTotalQuantity.Text + " REEMBOLSO DÍA " + dateTime2.ToString("dd/MM/yyyy", (IFormatProvider) CultureInfo.CurrentCulture) + " : " + new DataView(table2)
      {
        RowFilter = ("d_RepaymentDate = '" + dateTime2.ToString("dd/MM/yyyy", (IFormatProvider) CultureInfo.CurrentCulture) + "'")
      }.ToTable().Rows.Count.ToString();
      Label lblTotalQuantity1 = this.lblTotalQuantity;
      string text1 = this.lblTotalQuantity.Text;
      int count = table1.Rows.Count;
      string str1 = count.ToString();
      string str2 = text1 + " TOTAL GENERAL  : " + str1;
      lblTotalQuantity1.Text = str2;
      DataView dataView1 = new DataView(table2);
      dataView1.RowFilter = "d_RepaymentDate is not null";
      Label lblTotalQuantity2 = this.lblTotalQuantity;
      string text2 = this.lblTotalQuantity.Text;
      count = dataView1.ToTable().Rows.Count;
      string str3 = count.ToString();
      string str4 = text2 + " TOTAL REEMBOLSADOS   : " + str3;
      lblTotalQuantity2.Text = str4;
    }

    private int CharacterCount()
    {
      string str = this.txtPlateNumber.Text.TrimEnd();
      int num = 0;
      for (int startIndex = 0; startIndex < str.Length; ++startIndex)
      {
        if (str.Substring(startIndex, 1) == "%")
          ++num;
      }
      return num;
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }
  }
}
