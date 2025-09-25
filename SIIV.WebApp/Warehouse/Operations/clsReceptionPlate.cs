// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.clsReceptionPlate
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.Data;
using System.Runtime.Serialization;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  [DataContract(IsReference = true)]
  [Serializable]
  public class clsReceptionPlate
  {
    private long _inti_itemnumber;
    private string _stri_requirementplateid;
    private int _inti_productid;
    private string _strv_completename;
    private string _strv_platenew;
    private string _strv_productname;
    private string _strv_typeprocessed;
    private string _strv_requirementstatus;
    private int _inti_statussobreid;
    private int _inti_status;
    private int _inti_processtypeid;
    private int _inti_vehicletypeuseid;
    private int _inti_documenttypeid;
    private string _strv_documentnumber;
    private string _strv_firstname;
    private string _strv_lastname;
    private string _strv_phonenumber;
    private int _inti_ispistol;

    public long i_ItemNumber
    {
      get => this._inti_itemnumber;
      set => this._inti_itemnumber = value;
    }

    public string i_RequirementPlateId
    {
      get => this._stri_requirementplateid;
      set => this._stri_requirementplateid = value;
    }

    public int i_ProductId
    {
      get => this._inti_productid;
      set => this._inti_productid = value;
    }

    public string v_CompleteName
    {
      get => this._strv_completename;
      set => this._strv_completename = value;
    }

    public string v_PlateNew
    {
      get => this._strv_platenew;
      set => this._strv_platenew = value;
    }

    public string v_ProductName
    {
      get => this._strv_productname;
      set => this._strv_productname = value;
    }

    public string v_TypeProcessed
    {
      get => this._strv_typeprocessed;
      set => this._strv_typeprocessed = value;
    }

    public string v_RequirementStatus
    {
      get => this._strv_requirementstatus;
      set => this._strv_requirementstatus = value;
    }

    public int i_StatusSobreId
    {
      get => this._inti_statussobreid;
      set => this._inti_statussobreid = value;
    }

    public int i_Status
    {
      get => this._inti_status;
      set => this._inti_status = value;
    }

    public int i_ProcessTypeId
    {
      get => this._inti_processtypeid;
      set => this._inti_processtypeid = value;
    }

    public int i_VehicleTypeUseId
    {
      get => this._inti_vehicletypeuseid;
      set => this._inti_vehicletypeuseid = value;
    }

    public int i_DocumentTypeId
    {
      get => this._inti_documenttypeid;
      set => this._inti_documenttypeid = value;
    }

    public string v_DocumentNumber
    {
      get => this._strv_documentnumber;
      set => this._strv_documentnumber = value;
    }

    public string v_FirstName
    {
      get => this._strv_firstname;
      set => this._strv_firstname = value;
    }

    public string v_LastName
    {
      get => this._strv_lastname;
      set => this._strv_lastname = value;
    }

    public string v_PhoneNumber
    {
      get => this._strv_phonenumber;
      set => this._strv_phonenumber = value;
    }

    public int i_IsPistol
    {
      get => this._inti_ispistol;
      set => this._inti_ispistol = value;
    }

    public clsReceptionPlate()
    {
    }

    public clsReceptionPlate(DataRow dr)
    {
      if (dr == null)
        return;
      this.i_ItemNumber = !dr.Table.Columns.Contains("i_itemnumber") ? 0L : Convert.ToInt64(dr["i_itemnumber"]);
      this.i_RequirementPlateId = !dr.Table.Columns.Contains("i_requirementplateid") ? "" : dr["i_requirementplateid"].ToString();
      this.i_ProductId = !dr.Table.Columns.Contains("i_productid") ? 0 : Convert.ToInt32(dr["i_productid"]);
      this.v_CompleteName = !dr.Table.Columns.Contains("v_completename") ? "" : dr["v_completename"].ToString();
      this.v_PlateNew = !dr.Table.Columns.Contains("v_platenew") ? "" : dr["v_platenew"].ToString();
      this.v_ProductName = !dr.Table.Columns.Contains("v_productname") ? "" : dr["v_productname"].ToString();
      this.v_TypeProcessed = !dr.Table.Columns.Contains("v_typeprocessed") ? "" : dr["v_typeprocessed"].ToString();
      this.v_RequirementStatus = !dr.Table.Columns.Contains("v_requirementstatus") ? "" : dr["v_requirementstatus"].ToString();
      this.i_StatusSobreId = !dr.Table.Columns.Contains("i_statussobreid") ? 0 : Convert.ToInt32(dr["i_statussobreid"]);
      this.i_Status = !dr.Table.Columns.Contains("i_status") ? 0 : Convert.ToInt32(dr["i_status"]);
      this.i_ProcessTypeId = !dr.Table.Columns.Contains("i_processtypeid") ? 0 : Convert.ToInt32(dr["i_processtypeid"]);
      this.i_VehicleTypeUseId = !dr.Table.Columns.Contains(nameof (i_VehicleTypeUseId)) ? 0 : Convert.ToInt32(dr[nameof (i_VehicleTypeUseId)]);
      this.i_DocumentTypeId = !dr.Table.Columns.Contains("i_documenttypeid") ? 0 : Convert.ToInt32(dr["i_documenttypeid"]);
      this.v_DocumentNumber = !dr.Table.Columns.Contains("v_documentnumber") ? "" : dr["v_documentnumber"].ToString();
      this.v_FirstName = !dr.Table.Columns.Contains("v_firstname") ? "" : dr["v_firstname"].ToString();
      this.v_LastName = !dr.Table.Columns.Contains("v_lastname") ? "" : dr["v_lastname"].ToString();
      this.v_PhoneNumber = !dr.Table.Columns.Contains("v_phonenumber") ? "" : dr["v_phonenumber"].ToString();
      this.i_IsPistol = !dr.Table.Columns.Contains("i_ispistol") ? 0 : Convert.ToInt32(dr["i_ispistol"]);
    }
  }
}
