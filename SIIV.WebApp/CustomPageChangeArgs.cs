// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.CustomPageChangeArgs
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

#nullable disable
namespace SIIV.WebApp
{
  public class CustomPageChangeArgs
  {
    private int _currentPageNumber;
    private int _totalPages;
    private int _totalRecordCount;
    private int _currentPageSize;

    public int CurrentPageNumber
    {
      get => this._currentPageNumber;
      set => this._currentPageNumber = value;
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
  }
}
