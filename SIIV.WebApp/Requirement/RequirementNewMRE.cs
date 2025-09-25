// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Requirement.RequirementNewMRE
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Requirement
{
  public class RequirementNewMRE : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected Label Label2;
    protected TextBox txtPlate;
    protected Button wibSave;
    protected Button wibCancel;
    protected HtmlTableRow TdInfo;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.txtPlate.Text = "CD";
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      try
      {
        string text = this.txtPlate.Text;
        SystemUser systemUser = new SystemUser();
        if (text.ToUpper(CultureInfo.CurrentCulture).Substring(0, 2) != "CD")
          throw new HandledException(1, "La Placa MRE " + text + " tiene una estructura NO Válida");
        if (text.Length != 5)
          throw new HandledException(1, "Debe ingresar 5 caracteres.");
        if (new RequirementManagementBL().CreateUpdatePlateMRE("E" + text, systemUser.i_SystemUserId, "01") == 1)
        {
          Message.SetMessage(this.lblMessage, new HandledException(2, "Se inserto la placa con éxito."));
          this.txtPlate.Text = "CD";
        }
        else
          Message.SetMessage(this.lblMessage, new HandledException(1, "La placa ya existe en el sistema"));
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void wibCancel_Click(object sender, EventArgs e) => this.PopupClose();

    private void PopupClose()
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
