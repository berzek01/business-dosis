// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Retail.RetailAnulate
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Retail
{
  public class RetailAnulate : Page
  {
    private DataTable T = new DataTable()
    {
      Columns = {
        "Id",
        "Name",
        "Email"
      }
    };
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtIds;
    protected TextBox txtComprobante;
    protected TextBox txtCliente;
    protected TextBox txtProducto;
    protected TextBox txtObservation;
    protected Label lblMessage1;
    protected Button wibAceptar;
    protected Button wibCancelar;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.Session["OpenSucesfull"] = (object) 0;
      if (this.Request.QueryString["i_RequirementId"] != null)
      {
        this.txtIds.Text = this.Request.QueryString["i_RequirementId"].ToString();
        this.txtCliente.Text = this.Request.QueryString["v_CompleteName"].ToString();
        this.txtProducto.Text = this.Request.QueryString["v_Product"].ToString();
        this.txtComprobante.Text = this.Request.QueryString["v_Comprobante"].ToString();
        this.txtObservation.Focus();
      }
    }

    protected void wibAceptar_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.txtObservation.Text.Trim() == "")
        {
          Message.SetMessage(this.lblMessage1, new HandledException(1, "Debe de ingresar un motivo."));
        }
        else
        {
          SystemUser systemUser = this.Session["SystemUser"] != null ? this.Session["SystemUser"] as SystemUser : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - UniversalQuery.aspx");
          if (!new RequirementManagementBL().RetailAnulateSoli(Convert.ToInt32(this.txtIds.Text.Trim()), this.txtObservation.Text.Trim(), Convert.ToInt32(systemUser.i_SystemUserId)))
          {
            Message.SetMessage(this.lblMessage1, new HandledException(-100, "Error al intentar anular la solicitud"));
          }
          else
          {
            Message.SetMessage(this.lblMessage1, enmMessageType.Success, "Se realizó la anulación correctamente");
            this.Session["OpenSucesfull"] = (object) 1;
            this.wibCancelar_Click((object) null, (EventArgs) null);
          }
        }
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage1, enmMessageType.Error, ex.Message);
      }
    }

    public bool SendEMail(List<string> EmailTo, ref string postrMessage, string sFile)
    {
      try
      {
        Email email = new Email();
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.SMTPServerConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture),
          (object) "",
          (object) "1",
          (object) "1"
        });
        string pstrSMTPServer = dataTable.Rows[0]["v_Value"].ToString();
        int pintSMTPPort = int.Parse(dataTable.Rows[1]["v_Value"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        string str = dataTable.Rows[2]["v_Value"].ToString();
        string pstrSMTPPassword = dataTable.Rows[3]["v_Value"].ToString();
        bool boolean = Convert.ToBoolean(dataTable.Rows[4]["v_Value"], (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrEmailSubject = ConfigurationManager.AppSettings["Retail_Email_title"].ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim();
        this.txtObservation.Text.Replace("\n", "<br>");
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("<html><body>");
        stringBuilder.AppendLine("Solicitud Anulado - Retail<br><br>");
        stringBuilder.AppendLine("<table border='1'>");
        stringBuilder.AppendLine("<tr><td><b>Id</b></td><td>" + this.txtIds.Text + "</td></tr>");
        stringBuilder.AppendLine("<tr><td><b>Comprobante</b><td>" + this.txtComprobante.Text + "</td></tr>");
        stringBuilder.AppendLine("<tr><td><b>Cliente</b><td>" + this.txtCliente.Text + "</td></tr>");
        stringBuilder.AppendLine("<tr><td><b>Producto</b><td>" + this.txtProducto.Text + "</td></tr>");
        stringBuilder.AppendLine("<tr><td><b>Motivo</b><td>" + this.txtObservation.Text + "</td></tr>");
        stringBuilder.AppendLine("</table><br><br>");
        stringBuilder.AppendLine("Atentamente, <br><br>Servicio Notificación AAP");
        stringBuilder.AppendLine("</body></html>");
        string pstrEmailBody = stringBuilder.ToString();
        List<string> stringList = new List<string>();
        List<string> pstrEmailCC = new List<string>();
        Math.Ceiling(Convert.ToDecimal(EmailTo.Count) / 25M);
        List<string> pstrEmailCCo = new List<string>();
        return Email.SendEmail(str, pstrSMTPPassword, pstrEmailSubject, pstrEmailBody, EmailTo, pstrEmailCC, pstrSMTPServer, pintSMTPPort, str, boolean, pstrEmailCCo, ref postrMessage, "");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void wibCancelar_Click(object sender, EventArgs e)
    {
      string script = "SendInfoPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    public void EmailValidated(
      DataTable dt_Result,
      out List<string> AssociatedMail,
      out List<string> AssociatedMailError)
    {
      try
      {
        List<string> stringList1 = new List<string>();
        List<string> stringList2 = new List<string>();
        stringList1.Clear();
        stringList2.Clear();
        string pattern = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        foreach (DataRow row in (InternalDataCollectionBase) dt_Result.Rows)
        {
          string str1 = row["Email"].ToString().Trim();
          char[] chArray = new char[1]{ '|' };
          foreach (string str2 in str1.Split(chArray))
          {
            try
            {
              if (!string.IsNullOrEmpty(str2))
              {
                if (!Regex.IsMatch(str2.ToString((IFormatProvider) CultureInfo.CurrentCulture), pattern))
                  throw new DataException("Dato incorrecto");
                stringList1.Add(str2.ToString((IFormatProvider) CultureInfo.CurrentCulture));
              }
            }
            catch (DataException ex)
            {
              stringList2.Add(str2.ToString((IFormatProvider) CultureInfo.CurrentCulture) + " -> " + ex.Message);
            }
          }
        }
        AssociatedMail = stringList1;
        AssociatedMailError = stringList2;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
