// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibicion.Operation.LockPlates
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.Exhibition.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibicion.Operation
{
  public class LockPlates : Page
  {
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtRazonSocial;
    protected TextBox txtCliente;
    protected TextBox txtPlaca;
    protected TextBox txtDias;
    protected TextBox txtDescargo;
    protected FilteredTextBoxExtender ftbeCorporate;
    protected TextBox txtObsDescargo;
    protected Label lblMessage;
    protected Button wibAceptar;
    protected Button wibCancelar;
    protected HiddenField HiddenField1;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.LoadData();
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

    private void LoadData()
    {
      try
      {
        if (this.Request.QueryString["i_RequirementPlateId"] == null)
          return;
        int int32_1 = Convert.ToInt32(this.Request.QueryString["i_RequirementPlateId"]);
        int int32_2 = Convert.ToInt32(this.Request.QueryString["i_vehicleMovementId"]);
        this.ViewState["i_RequirementPlateId"] = (object) int32_1;
        this.ViewState["i_vehicleMovementId"] = (object) int32_2;
        GridViewRow gridViewRow = (GridViewRow) this.Session["selectedRows"];
        this.txtPlaca.Text = gridViewRow.Cells[5].Text;
        this.txtRazonSocial.Text = gridViewRow.Cells[2].Text;
        this.txtCliente.Text = gridViewRow.Cells[4].Text;
        this.txtDias.Text = gridViewRow.Cells[6].Text;
        this.ViewState["v_Email"] = (object) this.Session["v_Email"].ToString();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void wibAceptar_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.txtDescargo.Text == "")
          throw new HandledException(1, "Debe el nro de descargo.");
        if (this.txtObsDescargo.Text == "")
          throw new HandledException(1, "Debe ingresar alguna observacion.");
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        string text1 = this.txtPlaca.Text;
        int int32_1 = Convert.ToInt32(this.ViewState["i_RequirementPlateId"]);
        int int32_2 = Convert.ToInt32(this.ViewState["i_vehicleMovementId"]);
        string v_numRelease = this.txtDescargo.Text.ToString();
        string text2 = this.txtObsDescargo.Text;
        int iSystemUserId = systemUser.i_SystemUserId;
        if (new BlockPlateBL().BlockPlateInsert(text1, int32_1, int32_2, v_numRelease, text2, iSystemUserId) > 0)
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Success, "Se registro con éxito el bloqueo de la placa.");
          this.wibAceptar.Enabled = false;
          this.SendEmail(text1);
        }
        else
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "No se pudo registrar el bloqueo.");
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

    public bool SendEmail(string v_PlateNew)
    {
      Email email = new Email();
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.SMTPServerConfiguration.ToString(),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string pstrSMTPServer = dataTable.Rows[0]["v_Value"].ToString();
      int pintSMTPPort = int.Parse(dataTable.Rows[1]["v_Value"].ToString());
      string str1 = dataTable.Rows[2]["v_Value"].ToString();
      string pstrSMTPPassword = dataTable.Rows[3]["v_Value"].ToString();
      bool boolean = Convert.ToBoolean(dataTable.Rows[4]["v_Value"].ToString());
      string pstrEmailSubject = "Envío notificación placa bloqueada";
      List<string> pstrEmailTo = new List<string>();
      string[] source = this.ViewState["v_Email"].ToString().Split('|');
      for (int index = 0; index < ((IEnumerable<string>) source).Count<string>(); ++index)
      {
        if (index > 0 && source[index].ToString() != "")
          pstrEmailTo.Add(source[index].ToString());
      }
      string str2 = "<p><p><strong> ATENCIÓN.-</strong><br><br>La placa Rotativa N° <strong> " + v_PlateNew + "</strong> ha sido bloqueada por exceder el plazo máximo de asignación específica según normativa 004-2014-MTC,<br>por favor comunicarse con el supervisor del área de placas rotavivas. <br><br>Atentamente, <br><br>Dpto. Placas Rotativas <br>Teléfono: 6403637 anexo 174 <br>AAP <br>";
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("<html><body>");
      stringBuilder.AppendLine(str2);
      stringBuilder.AppendLine("</body></html>");
      string pstrEmailBody = stringBuilder.ToString();
      List<string> pstrEmailCC = new List<string>();
      if (ConfigurationManager.AppSettings["EmailCopy_Rotate"] != null)
      {
        string str3 = ConfigurationManager.AppSettings["EmailCopy_Rotate"].ToString().Trim();
        char[] chArray = new char[1]{ '|' };
        foreach (string str4 in str3.Split(chArray))
        {
          if (!string.IsNullOrEmpty(str4))
            pstrEmailCC.Add(str4.ToString());
        }
      }
      return Email.SendEmail(str1, pstrSMTPPassword, pstrEmailSubject, pstrEmailBody, pstrEmailTo, pstrEmailCC, pstrSMTPServer, pintSMTPPort, str1, boolean);
    }

    protected void wibCancelar_Click(object sender, EventArgs e)
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }
  }
}
