// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Pager
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp
{
  public class Pager : UserControl
  {
    private int _currentPageNumber;
    private bool _pageIsPostBack;
    private int _totalPages;
    private int _totalRecordCount;
    private int _currentPageSize;
    protected Label lblRecordCount;
    protected DropDownList ddlPageNumber;
    protected Label lblTotalPages;
    protected DropDownList ddlPageSize;

    public event CustomDelegateClass.PageChangedEventHandler PageChanged;

    public int CurrentPageNumber
    {
      get => this._currentPageNumber;
      set => this._currentPageNumber = value;
    }

    public bool pageIsPostBack
    {
      get => this._pageIsPostBack;
      set => this._pageIsPostBack = value;
    }

    public int TotalPages
    {
      get => this._totalPages;
      set => this._totalPages = value;
    }

    public int TotalRecordCount
    {
      get => this._totalRecordCount;
      set => this._totalRecordCount = value;
    }

    public int CurrentPageSize
    {
      get => this._currentPageSize;
      set => this._currentPageSize = value;
    }

    public void LoadPager()
    {
      this.ddlPageNumber.Items.Clear();
      this.lblTotalPages.Text = "0 ";
      this.lblRecordCount.Text = "0 ";
      if (this.TotalPages <= 0)
        return;
      for (int index = 1; index <= this.TotalPages; ++index)
        this.ddlPageNumber.Items.Add(index.ToString((IFormatProvider) CultureInfo.CurrentCulture));
      this.ddlPageNumber.Items[0].Selected = true;
      this.lblTotalPages.Text = string.Format((IFormatProvider) CultureInfo.CurrentCulture, " {0} ", new object[1]
      {
        (object) this.TotalPages.ToString()
      });
      this.lblRecordCount.Text = string.Format((IFormatProvider) CultureInfo.CurrentCulture, " {0} ", new object[1]
      {
        (object) this.TotalRecordCount.ToString()
      });
    }

    public void CleanPager()
    {
      this.CurrentPageSize = Convert.ToInt32(this.ddlPageSize.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture);
      this.CurrentPageNumber = Convert.ToInt32(this.ddlPageNumber.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      CustomPageChangeArgs e1 = new CustomPageChangeArgs();
      e1.CurrentPageSize = Convert.ToInt32(this.ddlPageSize.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture);
      e1.CurrentPageNumber = 1;
      e1.TotalPages = Convert.ToInt32(this.lblTotalPages.Text != "" ? this.lblTotalPages.Text : "0", (IFormatProvider) CultureInfo.CurrentCulture);
      this.CurrentPageSize = e1.CurrentPageSize;
      this.CurrentPageNumber = e1.CurrentPageNumber;
      this.Pager_PageChanged((object) this, e1);
      this.ddlPageNumber.Items.Clear();
      for (int index = 1; index <= this.TotalPages; ++index)
        this.ddlPageNumber.Items.Add(index.ToString((IFormatProvider) CultureInfo.CurrentCulture));
      if (this.ddlPageNumber.Items.Count > 0)
        this.ddlPageNumber.Items[0].Selected = true;
      Label lblTotalPages = this.lblTotalPages;
      CultureInfo currentCulture1 = CultureInfo.CurrentCulture;
      object[] objArray1 = new object[1];
      int num = this.TotalPages;
      objArray1[0] = (object) num.ToString();
      string str1 = string.Format((IFormatProvider) currentCulture1, " {0} ", objArray1);
      lblTotalPages.Text = str1;
      Label lblRecordCount = this.lblRecordCount;
      CultureInfo currentCulture2 = CultureInfo.CurrentCulture;
      object[] objArray2 = new object[1];
      num = this.TotalRecordCount;
      objArray2[0] = (object) num.ToString();
      string str2 = string.Format((IFormatProvider) currentCulture2, " {0} ", objArray2);
      lblRecordCount.Text = str2;
    }

    private void Pager_PageChanged(object sender, CustomPageChangeArgs e)
    {
      this.PageChanged((object) this, e);
    }

    protected void ddlPageNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
      CustomPageChangeArgs e1 = new CustomPageChangeArgs();
      e1.CurrentPageSize = Convert.ToInt32(this.ddlPageSize.SelectedItem.Value, (IFormatProvider) CultureInfo.CurrentCulture);
      e1.CurrentPageNumber = Convert.ToInt32(this.ddlPageNumber.SelectedItem.Text, (IFormatProvider) CultureInfo.CurrentCulture);
      e1.TotalPages = Convert.ToInt32(this.lblTotalPages.Text, (IFormatProvider) CultureInfo.CurrentCulture);
      e1.TotalRecordCount = Convert.ToInt32(this.lblRecordCount.Text, (IFormatProvider) CultureInfo.CurrentCulture);
      this.CurrentPageSize = e1.CurrentPageSize;
      this.CurrentPageNumber = e1.CurrentPageNumber;
      this.Pager_PageChanged((object) this, e1);
      this.lblTotalPages.Text = string.Format((IFormatProvider) CultureInfo.CurrentCulture, " {0} ", new object[1]
      {
        (object) e1.TotalPages.ToString()
      });
      this.lblRecordCount.Text = string.Format((IFormatProvider) CultureInfo.CurrentCulture, " {0} ", new object[1]
      {
        (object) e1.TotalRecordCount.ToString()
      });
    }
  }
}
