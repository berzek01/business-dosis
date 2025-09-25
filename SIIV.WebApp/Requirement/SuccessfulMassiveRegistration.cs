// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Requirement.SuccessfulMassiveRegistration
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using BarcodeLib;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Requirement
{
  public class SuccessfulMassiveRegistration : Page
  {
    private RequirementQueriesBL oRequirementBL;
    private double ComisionCanalAtencion = Convert.ToDouble(ConfigurationManager.AppSettings[nameof (ComisionCanalAtencion)]);
    private bool b_ResultSend = false;
    protected UpdatePanel updatepanel1;
    protected HtmlTableRow trPaymentCode;
    protected Label Label2;
    protected Label lblPaymentCode;
    protected HtmlTableRow trPrice;
    protected Label label6;
    protected Label lblPrice;
    protected Label Label1;
    protected HtmlGenericControl DiasPagoVisa;
    protected Label lblDias;
    protected HtmlGenericControl divVisa;
    protected Label Label4;
    protected HtmlGenericControl accordion1;
    protected Button Button1;
    protected Button Button2;
    protected Button btnSendMail;
    protected HtmlTableRow trMail;
    protected Label Label3;
    protected TextBox txtMail;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected ValidatorCalloutExtender ValidatorCalloutExtender1;
    protected RegularExpressionValidator RegularExpressionValidator1;
    protected ValidatorCalloutExtender RegularExpressionValidator1_ValidatorCalloutExtender;
    protected Button btnSend;
    protected Label lblMessage;

    private void Initialize()
    {
      try
      {
        this.oRequirementBL = new RequirementQueriesBL();
        if (this.Request.QueryString["id"] != null)
          this.ViewState["intRequirementId"] = (object) Convert.ToInt32(this.Request.QueryString["id"], (IFormatProvider) CultureInfo.CurrentCulture);
        if (this.Session["Monto"] != null)
        {
          this.ViewState["strMonto"] = (object) this.Session["Monto"].ToString();
          this.Session.Remove("Monto");
        }
        else
          this.ViewState["strMonto"] = (object) "";
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.Initialize();
        SIIV.Common.Resource.Message.SetMessage(this.Label1, enmMessageType.Success, "Su solicitud ha sido registrada correctamente.");
        if (this.ViewState["intRequirementId"] != null)
        {
          int int32 = Convert.ToInt32(this.ViewState["intRequirementId"], (IFormatProvider) CultureInfo.CurrentCulture);
          DataTable cur = this.oRequirementBL.GenerateCUR(int32);
          this.lblPaymentCode.Text = Convert.ToInt32(this.Session["VisaPagoConforme"]) != 1 ? cur.Rows[0]["v_PaymentCode"].ToString() : " - ";
          this.lblDias.Text = string.Format(" {0} días ", (object) new SystemParameterManagementBL().Get((object) new ArrayList()
          {
            (object) SystemParameterGroups.ExpirationTime.ToString(),
            (object) "4",
            (object) "1",
            (object) "1"
          })[0].v_Value);
          if (this.Session["SendEmail"] != null)
          {
            DataTable trazabilityMasiveId = new RequirementQueriesBL().GetDataTrazabilityMasiveId(int32);
            if (trazabilityMasiveId.Rows.Count > 0)
            {
              foreach (DataRow row in (InternalDataCollectionBase) trazabilityMasiveId.Rows)
                this.SendEmailTrazability(row);
            }
          }
        }
        if (!string.IsNullOrEmpty(this.ViewState["strMonto"].ToString()))
          this.lblPrice.Text = "S/. " + Math.Round(Convert.ToDouble(this.ViewState["strMonto"].ToString().Substring(3)), 2).ToString();
        else
          this.lblPrice.Visible = false;
        if (Convert.ToInt32(this.Session["VisaPagoConforme"]) == 1)
        {
          this.lblPrice.Text = this.ViewState["intRequirementId"] == null ? "S/ " + Math.Round(Convert.ToDouble(this.ViewState["strMonto"].ToString().Substring(3)) * (1.0 + this.ComisionCanalAtencion), 2).ToString() : "S/ " + Math.Round(Convert.ToDouble(this.oRequirementBL.GenerateCUR(Convert.ToInt32(this.ViewState["intRequirementId"], (IFormatProvider) CultureInfo.CurrentCulture)).Rows[0]["f_PriceTotal"].ToString()), 2).ToString();
          this.label6.Text = "Total Pagado: ";
          this.accordion1.Visible = false;
          this.divVisa.Visible = false;
          this.DiasPagoVisa.Visible = false;
        }
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("BeginRequirement.aspx");
    }

    protected string generateCUR(int intType)
    {
      try
      {
        string cur1 = "";
        if (Convert.ToInt32(this.ViewState["intRequirementId"]) <= 0)
          throw new DataException("No hay Solicitud Id");
        using (ReportDocument reportDocument = new ReportDocument())
        {
          this.oRequirementBL = new RequirementQueriesBL();
          string filename = this.Server.MapPath("../Requirement/ReportCur.rpt");
          reportDocument.Load(filename);
          DataTable cur2 = this.oRequirementBL.GenerateCUR(Convert.ToInt32(this.ViewState["intRequirementId"]));
          cur2.Columns.Add(new DataColumn()
          {
            ColumnName = "ImageBarPlate",
            DataType = typeof (byte[])
          });
          cur2.Columns.Add(new DataColumn()
          {
            ColumnName = "Requisite",
            DataType = typeof (string)
          });
          cur2.Columns.Add(new DataColumn()
          {
            ColumnName = "ImageBarCode",
            DataType = typeof (byte[])
          });
          foreach (DataRow row in (InternalDataCollectionBase) cur2.Rows)
          {
            byte[] numArray1 = this.ImagenBarCode(row["v_PlateNew"].ToString());
            row["ImageBarPlate"] = (object) numArray1;
            string requisitebyRequirement = new RequirementQueriesBL().GetRequisitebyRequirement(Convert.ToInt32(this.ViewState["intRequirementId"]), Convert.ToInt32(row["i_ProcessTypeId"], (IFormatProvider) CultureInfo.CurrentCulture));
            row["Requisite"] = (object) requisitebyRequirement;
            byte[] numArray2 = this.ImagenBarCode(row["v_PaymentCode"].ToString());
            row["ImageBarCode"] = (object) numArray2;
          }
          reportDocument.SetDataSource(cur2);
          if (intType == 1)
          {
            reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "CERTIFICADO_UNICO_REGISTRO");
            reportDocument.Close();
            ((Component) reportDocument).Dispose();
            cur1 = "";
          }
          else
          {
            string fileName = this.Server.MapPath("../Requirement/Temp/") + "CERTIFICADO_UNICO_REGISTRO_" + Convert.ToInt32(this.ViewState["intRequirementId"]).ToString((IFormatProvider) CultureInfo.CurrentCulture) + ".pdf";
            reportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, fileName);
            cur1 = fileName;
          }
        }
        return cur1;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private byte[] ImagenBarCode(string _NroPlaca)
    {
      if (!(_NroPlaca.Trim() != ""))
        return (byte[]) null;
      Barcode barcode = new Barcode();
      AlignmentPositions alignmentPositions = AlignmentPositions.CENTER;
      MemoryStream memoryStream = new MemoryStream();
      int int32_1 = Convert.ToInt32(300);
      int int32_2 = Convert.ToInt32(150);
      TYPE type = TYPE.CODE128;
      if (type != 0)
      {
        barcode.IncludeLabel = false;
        barcode.Alignment = alignmentPositions;
        barcode.Encode(type, _NroPlaca, Color.Black, Color.White, int32_1, int32_2);
        SaveTypes saveTypes = SaveTypes.JPG;
        barcode.SaveImage((Stream) memoryStream, saveTypes);
      }
      byte[] numArray = new byte[memoryStream.Length];
      return memoryStream.GetBuffer();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        this.generateCUR(1);
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
      try
      {
        this.trMail.Visible = true;
        this.txtMail.Text = new RequirementQueriesBL().GetRequirementContributor(1, Convert.ToInt32(this.Request.QueryString["id"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture)).Rows[0]["v_Email"].ToString();
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
      try
      {
        DataTable dataTable1 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.SMTPServerConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture),
          (object) "",
          (object) "1",
          (object) "1"
        });
        string str1 = dataTable1.Rows[0]["v_Value"].ToString();
        int num = int.Parse(dataTable1.Rows[1]["v_Value"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        string userName = dataTable1.Rows[2]["v_Value"].ToString();
        string password = dataTable1.Rows[3]["v_Value"].ToString();
        bool boolean = Convert.ToBoolean(dataTable1.Rows[4]["v_Value"], (IFormatProvider) CultureInfo.CurrentCulture);
        DataTable dataTable2 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.RequerimentCUREmailConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture),
          (object) "",
          (object) "1",
          (object) "1"
        });
        string str2 = dataTable2.Rows[0]["v_Value"].ToString();
        string str3 = dataTable2.Rows[1]["v_Value"].ToString();
        string address = dataTable2.Rows[2]["v_Value"].ToString();
        string str4 = dataTable2.Rows[3]["v_Value"].ToString();
        string addresses = this.txtMail.Text.Trim();
        string cur = this.generateCUR(2);
        Log.WriteLog("Placas", "Requirement", nameof (SuccessfulMassiveRegistration), nameof (btnSend_Click), "Email - CUR Generado", addresses + " -" + cur);
        Attachment attachment = new Attachment(cur);
        MailMessage message = new MailMessage();
        message.From = new MailAddress(address);
        message.To.Add(addresses);
        message.Subject = str2;
        message.Body = str3 + str4;
        message.IsBodyHtml = true;
        message.Priority = MailPriority.Normal;
        SmtpClient smtpClient = new SmtpClient();
        message.Attachments.Add(attachment);
        smtpClient.Host = str1;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.EnableSsl = boolean;
        smtpClient.Port = num;
        smtpClient.Credentials = (ICredentialsByHost) new NetworkCredential(userName, password);
        smtpClient.Send(message);
        this.lblMessage.Visible = true;
        smtpClient.Dispose();
        message.Dispose();
        attachment.Dispose();
        this.btnSendMail.Enabled = false;
        System.IO.File.Delete(cur);
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(2, "***EXITO</br>El correo ha sido enviado correctamente"));
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
    }

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatepanel1, this.updatepanel1.GetType(), "Script", script, true);
    }

    public StringBuilder TramaSendEmail(
      string strBodyEstado,
      string strCabecera,
      string strCuerpoEmail,
      string strPieEmail1,
      string strPieEmail2,
      string strPlaca,
      string strSolicitud,
      string strTipoSolicitud,
      string strFechaSolicitud,
      string strPuntoEntrega,
      string strCliente,
      string strCodigoPago,
      string strTotal,
      string strBanco,
      string strFechaPago,
      string strLinkServidor,
      int estado,
      string LinkHidden,
      string[] SMPTags)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string str1 = string.Empty;
      string str2 = string.Empty;
      switch (estado)
      {
        case 0:
          strBodyEstado = "<img src=\"" + strLinkServidor + "/Images/NotificationEmail/pendienteA.png\">";
          str1 = "<table style =\"width:100%;font-family:Helvetica;font-size:14pt;color:#70706E;text-align:left;\"><tbody><tr><td><span class=\"Table\" style=\"font-size:14pt;\">" + strPieEmail1 + "</span><br><br><table style=\"margin-right: auto;width: 100%;height:100px;text-align:center;\" cellpadding=\"0\" cellspacing=\"0\" class=\"TblUpDown001\"><tbody><tr><td><a href=\"https://www.viabcp.com\" target=\"_blank\"><img src=\"" + strLinkServidor + "/Images/BCP.PNG\" style=\"height: 40px; width: 80px\" border=\"0\"></a></td><td><a href=\"http://www.scotiabank.com.pe\" target=\"_blank\"><img src=\"" + strLinkServidor + "/Images/SCOTIA.PNG\" style=\"height: 40px; width: 90px\" border=\"0\"></a></td><td><a href=\"https://www.banbif.com.pe\" target=\"_blank\"><img src=\"" + strLinkServidor + "/Images/BIF.PNG\" style=\"height: 40px; width: 80px\" border=\"0\"></a></td><td><a href=\"https://www.bbvacontinental.pe\" target=\"_blank\"><img src=\"" + strLinkServidor + "/Images/BBVA.PNG\" border=\"0\" style=\"height: 35px; width: 100px\"></a></td><td><a href=\"https://www.interbank.com.pe\" target=\"_blank\"><img src=\"" + strLinkServidor + "/Images/interbank.PNG\" border=\"0\" style=\"height: 35px; width: 100px\"></a></td></tr></tbody></table><br><span>" + strPieEmail2 + "</span></td></tr></tbody></table></div></div></td></tr></tbody></table>";
          break;
        case 1:
          strBodyEstado = "<img src=\"" + strLinkServidor + "/Images/NotificationEmail/EnProcesoA.png\">";
          str2 = "<tr><td><span style=\"font-size:14pt;font-weight: bold;\">" + SMPTags[5] + "</span><span class=\"Table\" style=\"font-size:14pt;\">" + strBanco + "</span></td></tr><tr><td><span style=\"font-size:14pt;font-weight: bold;\">" + SMPTags[6] + "</span><span class=\"Table\" style=\"font-size:14pt;\">" + strFechaPago + "</span>";
          break;
      }
      stringBuilder.AppendLine("<html><body style=\"width:500px;height:600px;\"><table style=\"margin-left:auto;margin-right:auto;width:900px;\" cellpadding=\"0\" cellspacing=\"0\"><tbody><tr><td class=\"Table\" colspan=\"6\" style=\"height:150px\"><table style=\"width: 100%;border:none;text-align:left;margin-left:50px;margin-right:50px;\"><tbody><tr><td data-align=\"left\" class=\"Table\"><input type=\"image\" src=\"" + strLinkServidor + "/Images/Design/header2.jpg\" name=\"MPW0006IMAGE1\" alt=\"\" class=\"Image\"></td></tr></tbody></table><br><table style=\"height:33px; width:100%;color:#70706E;text-align:center;\"><tbody><tr><td colspan=\"2\" rowspan=\"2\"><span style=\"font-size:20pt;font-weight:bold;\">" + strCabecera + "</span><br><br></td></tr></tbody></table><table style=\"width:100%;font-family:Helvetica;font-size:14pt;color:#70706E;text-align:left;font-weight:normal;font-style:normal;padding-left:50px;padding-right:50px;\"><tbody><tr><td><span style=\"font-size:14pt;font-weight: bold;\">" + SMPTags[0] + "</span><span class=\"Table\" style=\"font-size:14pt;\">" + strCliente + "</span><br></td></tr><tr><td><span class=\"Table\" style=\"font-size:14pt;\">" + strCuerpoEmail + "</span><br><br></td></tr><tr><td><span style=\"font-size:14pt;font-weight: bold;\">" + SMPTags[1] + "</span><span class=\"Table\" style=\"font-size:14pt;\">" + strPlaca + "</span></td></tr><tr><td><span style=\"font-size:14pt;font-weight: bold;\">" + SMPTags[2] + "</span><span class=\"Table\" style=\"font-size:14pt;\">" + strSolicitud + "</span></td></tr><tr><td><span style=\"font-size:14pt;font-weight: bold;\">" + SMPTags[3] + "</span><span class=\"Table\" style=\"font-size:14pt;\">" + strFechaSolicitud + "</span></td></tr><tr><td><span style=\"font-size:14pt;font-weight: bold;\">" + SMPTags[11] + "</span><span class=\"Table\" style=\"font-size:14pt;\">" + strPuntoEntrega + "</span><br><br></td></tr><tr><td><span style=\"font-size:14pt;font-weight: bold;\">" + SMPTags[4] + "</span><span class=\"Table\" style=\"font-size:14pt;\">" + strCodigoPago + "</span>");
      stringBuilder.AppendLine(str2);
      stringBuilder.AppendLine("<br><br></td></tr></tbody></table><table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" style=\"padding-left:50px;\"><tr><td><div style=\"max-height:0;overflow:visible;\">");
      stringBuilder.AppendLine(strBodyEstado);
      stringBuilder.AppendLine("</div><div style=\"max-height:124px;max-width:0px;overflow:visible;\"><span style=\"width:100px;height:100px;margin-top:45px;margin-left:125px;display:inline-block;text-align:center;line-height:100px;font-size:11pt;font-family:HelveticaLTStd-Roman,Arial;font-weight:bold\"></span></div></td></tr></table></td></tr></tbody></table><table class=\"Table\" style=\"height:50%;width:950px;margin-left:50px;margin-top:25px;\" cellpadding=\"0\" cellspacing=\"0\"><tbody><tr><td data-align=\"center\" class=\"Table\" colspan=\"6\"><div><div><br>");
      stringBuilder.AppendLine(str1);
      stringBuilder.AppendLine(LinkHidden);
      stringBuilder.AppendLine("</body></html>");
      return stringBuilder;
    }

    public void SendEmailTrazability(DataRow dr)
    {
      try
      {
        RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
        List<string> pstruseremail = new List<string>();
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.TrazabilityNotification.ToString(),
          (object) "",
          (object) "1",
          (object) "1"
        });
        string strSMTPServer = dataTable.Rows[0]["v_Value"].ToString();
        int intSMTPPort = int.Parse(dataTable.Rows[1]["v_Value"].ToString());
        string strSMTPUserName = dataTable.Rows[2]["v_Value"].ToString();
        string strSMTPPassword = dataTable.Rows[3]["v_Value"].ToString();
        bool boolean = Convert.ToBoolean(dataTable.Rows[4]["v_Value"]);
        string strEmailSubject = dataTable.Rows[5]["v_Value"].ToString();
        string str1 = dataTable.Rows[11]["v_Value"].ToString();
        string str2 = dataTable.Rows[12]["v_Value"].ToString();
        string strLinkServidor = dataTable.Rows[13]["v_Value"].ToString();
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string empty3 = string.Empty;
        string empty4 = string.Empty;
        string str3 = "1";
        int int32_1 = Convert.ToInt32(dr["i_EmailNotificationId"]);
        string strPlaca = dr["v_PlateNew"].ToString();
        string strSolicitud = dr["i_RequirementPlateId"].ToString();
        string strTipoSolicitud = Convert.ToInt32(dr["i_RequirementPlateTypeId"]) == 9 ? "Solicitud Masiva" : "Solicitud Regular";
        string strFechaSolicitud = dr["fecha_solicitud"].ToString();
        string strPuntoEntrega = dr["v_DeliveryPointId"].ToString();
        string strCliente = dr["v_CompleteName"].ToString();
        string strCodigoPago = dr["codigo_Pago"].ToString();
        string strTotal = dr["precio"].ToString();
        string str4 = dr["v_Email"].ToString();
        int int32_2 = Convert.ToInt32(dr["i_StatusRequirement"]);
        string LinkHidden = "<img height=1 src=\"" + str2 + "i_TypeNotificationId=" + str3 + "&i_EmailNotificationId=" + int32_1.ToString() + "\" width=1>";
        switch (int32_2)
        {
          case 0:
            empty2 = dataTable.Rows[6]["v_Value"].ToString();
            break;
          case 1:
            empty2 = dataTable.Rows[7]["v_Value"].ToString();
            empty3 = dr["banco"].ToString();
            empty4 = dr["fecha_Pago"].ToString();
            break;
        }
        string[] strArray1 = empty2.Split('|');
        string strCabecera = strArray1[0];
        string strCuerpoEmail = strArray1[1];
        string empty5 = string.Empty;
        string empty6 = string.Empty;
        if (strArray1[2] != "")
        {
          string[] strArray2 = strArray1[2].Split(';');
          empty5 = strArray2[0];
          empty6 = strArray2[1];
        }
        string[] SMPTags = str1.Split('|');
        StringBuilder stringBuilder = this.TramaSendEmail(empty1, strCabecera, strCuerpoEmail, empty5, empty6, strPlaca, strSolicitud, strTipoSolicitud, strFechaSolicitud, strPuntoEntrega, strCliente, strCodigoPago, strTotal, empty3, empty4, strLinkServidor, int32_2, LinkHidden, SMPTags);
        string[] source = str4.Split(';');
        for (int index = 0; index < ((IEnumerable<string>) source).Count<string>(); ++index)
        {
          if (source[index].ToString() != "")
            pstruseremail.Add(source[index].ToString().Trim());
        }
        this.b_ResultSend = this.SendEmail(strSMTPServer, intSMTPPort, strSMTPUserName, strSMTPPassword, boolean, strEmailSubject, pstruseremail, stringBuilder.ToString());
        if (!this.b_ResultSend)
        {
          string v_Observation = "Mensaje no Enviado";
          requirementQueriesBl.UpdateConfirmationSendEmail(int32_1, this.b_ResultSend, v_Observation);
          this.Session["SendEmail"] = (object) null;
        }
        else
        {
          string v_Observation = "Mensaje Enviado";
          requirementQueriesBl.UpdateConfirmationSendEmail(int32_1, this.b_ResultSend, v_Observation);
          this.Session["SendEmail"] = (object) null;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public bool SendEmail(
      string strSMTPServer,
      int intSMTPPort,
      string strSMTPUserName,
      string strSMTPPassword,
      bool bSMTPEnabledSSL,
      string strEmailSubject,
      List<string> pstruseremail,
      string strBody)
    {
      List<string> pstrEmailCC = new List<string>();
      return Email.SendEmail(strSMTPUserName, strSMTPPassword, strEmailSubject, strBody, pstruseremail, pstrEmailCC, strSMTPServer, intSMTPPort, strSMTPUserName, bSMTPEnabledSSL);
    }
  }
}
