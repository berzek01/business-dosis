// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Warehouse.Searchs.ShelfTypeUseAssign
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.Common.Resource;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Warehouse.Searchs
{
  public class ShelfTypeUseAssign : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected RadioButtonList rblTypeUse;
    protected Button wibFinalze;
    protected Button wibRemoveTypeUse;
    protected HtmlTableRow trMessage;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadchklTypeUse();
    }

    protected void wibFinalze_Click(object sender, EventArgs e)
    {
      try
      {
        if (!this.IsValidData(1))
          return;
        ArrayList arrayList = new ArrayList();
        if (this.Session["lwlst"] != null)
          arrayList = (ArrayList) this.Session["lwlst"];
        foreach (string str in arrayList)
        {
          string[] strArray = str.Split('-');
          ShelfManagementBL shelfManagementBl = new ShelfManagementBL();
          short int16 = Convert.ToInt16(strArray[0], (IFormatProvider) CultureInfo.CurrentCulture);
          foreach (ListItem listItem in this.rblTypeUse.Items)
          {
            if (listItem.Selected)
            {
              int? pintShelfTypeUseId = new int?((int) Convert.ToInt16(listItem.Value, (IFormatProvider) CultureInfo.CurrentCulture));
              shelfManagementBl.ShelfPositionTypeUseUpdate((int) int16, pintShelfTypeUseId);
              break;
            }
          }
        }
        this.RefreshParent();
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, "Error****<br>" + ex.Message);
      }
    }

    protected void wibRemoveTypeUse_Click(object sender, EventArgs e)
    {
      ArrayList arrayList = new ArrayList();
      if (this.Session["lwlst"] != null)
        arrayList = (ArrayList) this.Session["lwlst"];
      foreach (string str in arrayList)
        new ShelfManagementBL().ShelfPositionTypeUseUpdate((int) Convert.ToInt16(str.Split('-')[0], (IFormatProvider) CultureInfo.CurrentCulture), new int?());
      this.RefreshParent();
    }

    private void LoadchklTypeUse()
    {
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) ("" + SystemParameterGroups.PublicRecordsUseType.ToString((IFormatProvider) CultureInfo.CurrentCulture)),
        (object) "",
        (object) "1",
        (object) "1"
      });
      if (dataTable == null)
        return;
      this.rblTypeUse.DataSource = (object) dataTable;
      this.rblTypeUse.DataTextField = "v_Description";
      this.rblTypeUse.DataValueField = "i_ParameterId";
      this.rblTypeUse.DataBind();
      ArrayList arrayList = new ArrayList();
      if (this.Session["lwlst"] != null)
        arrayList = (ArrayList) this.Session["lwlst"];
      if (arrayList.Count == 1)
      {
        string str = arrayList[0].ToString().Split('-')[1];
        if (str != "-1")
          this.rblTypeUse.SelectedValue = str;
      }
      else
        this.wibRemoveTypeUse.Style.Add("display", "none");
    }

    private void RefreshParent()
    {
      string script = "RefreshParent();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private bool IsValidData(int pintAction)
    {
      this.lblMessage.Visible = false;
      bool flag = false;
      string empty = string.Empty;
      foreach (ListItem listItem in this.rblTypeUse.Items)
      {
        if (listItem.Selected)
        {
          flag = true;
          break;
        }
      }
      if (flag)
        return true;
      this.trMessage.Visible = true;
      Message.SetMessage(this.lblMessage, enmMessageType.Warning, pintAction != 1 ? "Advertencia****<br>No existe tipo de uso a Eliminar." : "Advertencia****<br>Debe seleccionar un tipo de uso.");
      return false;
    }
  }
}
