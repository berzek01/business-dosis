// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.clsReceptionPlateList
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations
{
  [DataContract(IsReference = true)]
  [Serializable]
  public class clsReceptionPlateList
  {
    private List<clsReceptionPlate> _objelements = new List<clsReceptionPlate>();

    public List<clsReceptionPlate> Elements
    {
      get => this._objelements;
      set => this._objelements = value;
    }

    public clsReceptionPlateList()
    {
    }

    public clsReceptionPlateList(DataTable entidad)
    {
      if (entidad == null || entidad.Rows.Count == 0)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) entidad.Rows)
        this.Elements.Add(new clsReceptionPlate(row));
    }
  }
}
