// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Operations.Handlers.GetRequirementPlate
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using Newtonsoft.Json;
using SIIV.BE;
using SIIV.BE.CustomCode;
using SIIV.Warehouse.BL;
using System;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.SessionState;

#nullable disable
namespace SIIV.WebApp.Warehouse.Operations.Handlers
{
  public class GetRequirementPlate : IHttpHandler, IRequiresSessionState
  {
    public void ProcessRequest(HttpContext context)
    {
      context.Response.ContentType = "application/json";
      WarehouseInventoryQueriesBL inventoryQueriesBl = new WarehouseInventoryQueriesBL();
      int int32_1 = Convert.ToInt32(context.Request.Params["InventoryID"], (IFormatProvider) CultureInfo.CurrentCulture);
      int int32_2 = Convert.ToInt32(context.Request.Params["RequirementID"], (IFormatProvider) CultureInfo.CurrentCulture);
      bool boolean = Convert.ToBoolean(context.Request.Params["Validate"], (IFormatProvider) CultureInfo.CurrentCulture);
      SystemUser systemUser = context.Session["SystemUser"] as SystemUser;
      DataTable pdtInventoryDetail = context.Session["dtInventoryDetail"] as DataTable;
      bool flag = this.SearchID(pdtInventoryDetail, int32_2);
      int pintValidationResult = -3;
      string pstrResult = "Ya se agrego el sobre con ID : " + int32_2.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      DataTable dataTable = new DataTable();
      if (!flag)
        dataTable = inventoryQueriesBl.GetRequirementData(int32_1, systemUser.i_LocationId, int32_2, boolean, out pintValidationResult, out pstrResult);
      InventoryJsonResult inventoryJsonResult = new InventoryJsonResult();
      inventoryJsonResult.dtResult = dataTable;
      inventoryJsonResult.iValidationResult = pintValidationResult;
      inventoryJsonResult.strValidationResult = pstrResult;
      if (!flag && pintValidationResult != -2 && pintValidationResult != -4)
        this.AddRow(inventoryJsonResult.dtResult.Rows[0], inventoryJsonResult.iValidationResult, pdtInventoryDetail);
      string s = JsonConvert.SerializeObject((object) inventoryJsonResult);
      context.Response.Write(s);
    }

    private bool SearchID(DataTable pdtInventoryDetail, int pintRequirementPlate)
    {
      bool flag = false;
      if (pdtInventoryDetail.Rows.Find((object) pintRequirementPlate) != null)
        flag = true;
      return flag;
    }

    private DataTable AddRow(DataRow row, int iValidationResult, DataTable pdtInventoryDetail)
    {
      DataTable dataTable = pdtInventoryDetail;
      DataRow row1 = dataTable.NewRow();
      row1["i_RequirementPlateID"] = row["i_RequirementPlateId"];
      row1["v_PlateNumber"] = row["v_PlateNew"];
      row1["i_Status"] = (object) iValidationResult;
      dataTable.Rows.Add(row1);
      return dataTable;
    }

    public bool IsReusable => false;
  }
}
