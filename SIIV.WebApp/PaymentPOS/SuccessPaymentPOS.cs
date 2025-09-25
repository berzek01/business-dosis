// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.PaymentPOS.SuccessPaymentPOS
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Newtonsoft.Json;
using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Payment.BL;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using SIIV.WebApp.UserControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.PaymentPOS
{
  public class SuccessPaymentPOS : Page
  {
    public string CODPAGO = "";
    public string endpointurl;
    public string url;
    public string merchantId;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected Image Image1;
    protected HtmlGenericControl DivRespuesta;
    protected Label lbltienda;
    protected Label lbltelefono;
    protected Label lbldireccion;
    protected Label lbldominio;
    protected Label lblNumpedido;
    protected Label lblNumtarjeta;
    protected Label lblFechaHora;
    protected Label lblimorteTrans;
    protected Label lblMoneda;
    protected Label lblProducto;
    protected Label lblPropietario;
    protected Label lblRespuesta;
    protected HtmlGenericControl DivRespuestaFalse;
    protected Label Label5;
    protected Label Label7;
    protected Label CodRespuesta;
    protected Label Label12;
    protected HtmlGenericControl DivRespuestaDen;
    protected HtmlGenericControl DivCorreo;
    protected TextBox txtCorreo;
    protected Button btnSendEmail;
    protected Button btnFinishPayout;
    protected Button btnPrintPayout;
    protected Label lblMensage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      string empty1 = string.Empty;
      try
      {
        string empty2 = string.Empty;
        string rawUrl = this.Request.RawUrl;
        string[] strArray = this.DecryptQueryString(rawUrl.Substring(rawUrl.IndexOf('?') + 1)).Replace("&quot;&quot;&quot;&quot;", "").Split('&');
        string str1 = ((IEnumerable<string>) strArray[0].ToString().Trim().Split('=')).ToArray<string>()[1].ToString();
        string s = ((IEnumerable<string>) strArray[1].ToString().Trim().Split('=')).ToArray<string>()[1].ToString();
        string str2 = ((IEnumerable<string>) strArray[2].ToString().Trim().Split('=')).ToArray<string>()[1].ToString();
        string str3 = ((IEnumerable<string>) strArray[3].ToString().Trim().Split('=')).ToArray<string>()[1].ToString();
        string str4 = ((IEnumerable<string>) strArray[4].ToString().Trim().Split('=')).ToArray<string>()[1].ToString();
        string sPaymentCode = ((IEnumerable<string>) strArray[5].ToString().Trim().Split('=')).ToArray<string>()[1].ToString();
        string str5 = ((IEnumerable<string>) strArray[6].ToString().Trim().Split('=')).ToArray<string>()[1].ToString();
        string str6 = ((IEnumerable<string>) strArray[7].ToString().Trim().Split('=')).ToArray<string>()[1].ToString();
        string str7 = ((IEnumerable<string>) strArray[8].ToString().Trim().Split('=')).ToArray<string>()[1].ToString();
        string sRequirementId = ((IEnumerable<string>) strArray[9].ToString().Trim().Split('=')).ToArray<string>()[1].ToString();
        int iSystemUserId = this.Session["SystemUser"] == null ? 0 : (this.Session["SystemUser"] as SystemUser).i_SystemUserId;
        DataTable dataTable1 = new DataTable();
        RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
        string str8 = this.Request.Form["transactionToken"];
        string str9 = this.Request.Form["channel"];
        SIIV.Common.Resource.Utilities.Logging.WriteFileLog(ConfigDA.ReadConfig("EventLogFile") + "__context_Response_Visa_" + DateTime.Today.ToString("yyyyMMdd") + ".txt", "sessionToken: " + str1 + "|transactionToken: " + str8 + "|channel: " + str9 + "|_order: " + s + "|_operacion: " + str2, enmFileSection.Content);
        EntitiesPayment.Order order1 = (EntitiesPayment.Order) new DataContractJsonSerializer(typeof (EntitiesPayment.Order)).ReadObject((Stream) new MemoryStream(Encoding.UTF8.GetBytes(s)));
        EntitiesPayment.Order order2 = new EntitiesPayment.Order();
        order2.tokenId = str8;
        order2.purchaseNumber = order1.purchaseNumber;
        order2.amount = order1.amount;
        order2.currency = order1.currency;
        EntitiesPayment.Data_Request_Aut dataRequestAut = new EntitiesPayment.Data_Request_Aut()
        {
          transactionToken = str8,
          sessionToken = str1
        };
        string strMensaje = ValidationBL.JsonSerializer<EntitiesPayment.AuthorizationRequest>(new EntitiesPayment.AuthorizationRequest()
        {
          channel = str9,
          captureType = "manual",
          countable = str2 == "true",
          order = order2
        });
        string str10 = ConfigDA.ReadConfig("EventLogFile");
        DateTime today = DateTime.Today;
        string str11 = today.ToString("yyyyMMdd");
        SIIV.Common.Resource.Utilities.Logging.WriteFileLog(str10 + "_Context_Response_Visa_" + str11 + ".txt", strMensaje, enmFileSection.Content);
        HttpWebRequest httpWebRequest = WebRequest.Create(ConfigurationManager.AppSettings["urlAuthorize"] + str3) as HttpWebRequest;
        httpWebRequest.Method = "POST";
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Headers.Add("Authorization", str1);
        StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
        streamWriter.Write(strMensaje);
        streamWriter.Close();
        string end;
        try
        {
          StreamReader streamReader = new StreamReader((httpWebRequest.GetResponse() as HttpWebResponse).GetResponseStream());
          end = streamReader.ReadToEnd();
          streamReader.Close();
          string str12 = ConfigDA.ReadConfig("EventLogFile");
          today = DateTime.Today;
          string str13 = today.ToString("yyyyMMdd");
          SIIV.Common.Resource.Utilities.Logging.WriteFileLog(str12 + "__context_Response_Visa_" + str13 + ".txt", "respuestaAutorizacion : " + end, enmFileSection.Content);
        }
        catch (WebException ex)
        {
          StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream(), true);
          end = streamReader.ReadToEnd();
          streamReader.Close();
        }
        EntitiesPaymentResponse.Rootobject eResponse = JsonConvert.DeserializeObject<EntitiesPaymentResponse.Rootobject>(end);
        string str14 = ConfigDA.ReadConfig("EventLogFile");
        DateTime dateTime = DateTime.Today;
        string str15 = dateTime.ToString("yyyyMMdd");
        SIIV.Common.Resource.Utilities.Logging.WriteFileLog(str14 + "__context_Response_Visa_" + str15 + ".txt", "postStreamWriterAutorizacion : " + end, enmFileSection.Content);
        this.RegisterResponseVisa(eResponse, end, sPaymentCode, order2.purchaseNumber, sRequirementId, iSystemUserId);
        if (eResponse.dataMap != null)
        {
          string idUnico = eResponse.dataMap.ID_UNICO;
          if (eResponse.dataMap.STATUS == "Authorized")
          {
            requirementQueriesBl.UpdateETicketVisa("", eResponse.dataMap.AUTHORIZATION_CODE, eResponse.order.purchaseNumber);
            this.DivRespuesta.Visible = true;
            DataTable dataTable2 = requirementQueriesBl.ETicketVisaGet(eResponse.dataMap.AUTHORIZATION_CODE);
            requirementQueriesBl.AccreditPayment(eResponse.order.purchaseNumber, 0, eResponse.dataMap.ID_UNICO);
            if (dataTable2.Rows.Count > 0)
            {
              this.lbldominio.Text = "";
              this.lbltienda.Text = ConfigurationManager.AppSettings["NomTienda"].ToString();
              this.lbltelefono.Text = ConfigurationManager.AppSettings["TelTienda"].ToString();
              this.lbldireccion.Text = ConfigurationManager.AppSettings["DirecTienda"].ToString();
              this.lblNumpedido.Text = eResponse.order.purchaseNumber;
              this.lblNumtarjeta.Text = eResponse.dataMap.CARD;
              this.lblFechaHora.Text = dataTable2.Rows[0]["RegisterDate"].ToString() + " " + dataTable2.Rows[0]["RegisterTime"].ToString();
              this.Label12.Text = eResponse.dataMap.ACTION_DESCRIPTION;
              this.lblRespuesta.Text = eResponse.dataMap.ACTION_DESCRIPTION;
              this.lblimorteTrans.Text = (Convert.ToInt32(str4) != Convert.ToInt32((object) enmRequirementPlateType.Masiva) ? Convert.ToDouble(str6) : Convert.ToDouble(str5)).ToString();
              this.lblMoneda.Text = dataTable2.Rows[0]["v_CoinDescr"].ToString();
              if (Convert.ToInt32(dataTable2.Rows[0]["i_RequirementTypeId"]) == 4)
              {
                this.lblProducto.Text = "Producto Delivery";
                this.lblPropietario.Text = dataTable2.Rows[0]["v_OwnerCompleteName"].ToString();
              }
              else
              {
                this.lblProducto.Text = dataTable2.Rows.Count > 1 ? "Producto Masivo" : dataTable2.Rows[0]["v_Description"].ToString();
                this.lblPropietario.Text = dataTable2.Rows.Count > 1 ? "Registro Masivo" : dataTable2.Rows[0]["v_OwnerCompleteName"].ToString();
              }
              this.txtCorreo.Text = this.Request.Form["customerEmail"];
              this.Session["VisaPagoConforme"] = (object) 1;
              SIIV.Common.Resource.Message.SetMessage(this.lblMensage, new HandledException(2, "¡ ENHORABUENA ! <br> Su pago se realizó sactisfactoriamente."));
              this.DivRespuestaFalse.Visible = false;
              this.UpdatePaymentLog(idUnico, "MESSAGE : -> Code : " + eResponse.dataMap.ACTION_CODE + " | Description : " + eResponse.dataMap.ACTION_DESCRIPTION, false);
            }
            else
            {
              this.Session["VisaPagoConforme"] = (object) 0;
              SIIV.Common.Resource.Message.SetMessage(this.lblMensage, new HandledException(1, "No Existe información del Eticket en el SIIV."));
              this.UpdatePaymentLog(string.Empty, "MESSAGE APP : No Existe información del Eticket en el SIIV. | MESSAGE VISA : -> Code : " + eResponse.dataMap.ACTION_CODE + " | Description : " + eResponse.dataMap.ACTION_DESCRIPTION, false);
            }
          }
          else
          {
            this.DivRespuestaDen.Visible = true;
            this.DivRespuesta.Visible = false;
            this.DivRespuestaFalse.Visible = true;
            this.Session["VisaPagoConforme"] = (object) 0;
            if (this.Session["ETicket"] != null)
              this.Session.Remove("ETicket");
            if (this.Session["ETicketD"] != null)
              this.Session.Remove("ETicketD");
            if (this.Session["EticketM"] != null)
              this.Session.Remove("EticketM");
            this.DivCorreo.Visible = false;
            this.btnPrintPayout.Visible = false;
            this.Label5.Text = order2.purchaseNumber;
            Label label7 = this.Label7;
            dateTime = DateTime.Now;
            string str16 = dateTime.ToString();
            label7.Text = str16;
            this.CodRespuesta.Text = eResponse.dataMap.ACTION_CODE;
            this.Label12.Text = eResponse.dataMap.ACTION_DESCRIPTION;
            this.lblRespuesta.Text = eResponse.dataMap.ACTION_DESCRIPTION;
            SIIV.Common.Resource.Message.SetMessage(this.lblMensage, new HandledException(1, eResponse.dataMap.ACTION_DESCRIPTION, "Codigo Pago : " + sPaymentCode + " | Numero Orden : " + order2.purchaseNumber + " | Code Unique : " + eResponse.dataMap.ID_UNICO + " | Code Denegacion : " + eResponse.dataMap.ACTION_CODE + " | ECI : " + eResponse.dataMap.ECI + " | ECI Description: " + eResponse.dataMap.ECI_DESCRIPTION + " | Client IP : " + str7));
            this.UpdatePaymentLog(idUnico, "MESSAGE : -> Code : " + eResponse.dataMap.ACTION_CODE + " | Description : " + eResponse.dataMap.ACTION_DESCRIPTION, false);
          }
        }
        else
        {
          this.DivRespuestaDen.Visible = true;
          this.DivRespuesta.Visible = false;
          this.DivRespuestaFalse.Visible = true;
          this.Session["VisaPagoConforme"] = (object) 0;
          if (this.Session["ETicket"] != null)
            this.Session.Remove("ETicket");
          if (this.Session["ETicketD"] != null)
            this.Session.Remove("ETicketD");
          if (this.Session["EticketM"] != null)
            this.Session.Remove("EticketM");
          this.DivCorreo.Visible = false;
          this.btnPrintPayout.Visible = false;
          this.Label5.Text = order2.purchaseNumber;
          Label label7 = this.Label7;
          dateTime = DateTime.Now;
          string str17 = dateTime.ToString();
          label7.Text = str17;
          this.CodRespuesta.Text = eResponse.data.ACTION_CODE;
          this.Label12.Text = eResponse.data.ACTION_DESCRIPTION;
          this.lblRespuesta.Text = eResponse.data.ACTION_DESCRIPTION;
          this.UpdatePaymentLog(empty1, "MESSAGE : -> Code : " + eResponse.data.ACTION_CODE + " | Description : " + eResponse.data.ACTION_DESCRIPTION + " | Error Code : " + eResponse.errorCode.ToString() + " | Error Message : " + eResponse.errorMessage, false);
          SIIV.Common.Resource.Message.SetMessage(this.lblMensage, new HandledException(1, eResponse.data.ACTION_DESCRIPTION, "Codigo Pago : " + sPaymentCode + " | Numero Orden : " + order2.purchaseNumber + " | Code Unique : " + eResponse.data.ID_UNICO + " | Code Denegacion : " + eResponse.data.ACTION_CODE + " | ECI : " + eResponse.data.ECI + " | ECI Description : " + eResponse.data.ECI_DESCRIPTION + " | Client IP : " + str7));
        }
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMensage, new HandledException(1, ex.Message));
      }
    }

    public void RegisterResponseVisa(
      EntitiesPaymentResponse.Rootobject eResponse,
      string sResultResponse,
      string sPaymentCode,
      string sNumberOrder,
      string sRequirementId,
      int iSystemUserId)
    {
      try
      {
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string transactionId;
        string actionCode;
        if (eResponse.dataMap != null)
        {
          if (eResponse.dataMap.STATUS == "Authorized")
          {
            transactionId = eResponse.dataMap.TRANSACTION_ID;
            actionCode = eResponse.dataMap.ACTION_CODE;
          }
          else
          {
            transactionId = eResponse.dataMap.TRANSACTION_ID;
            actionCode = eResponse.dataMap.ACTION_CODE;
          }
        }
        else
        {
          transactionId = eResponse.data.TRANSACTION_ID;
          actionCode = eResponse.data.ACTION_CODE;
        }
        new RequirementQueriesBL().InsertServiceVisaLog(new ArrayList()
        {
          (object) 0,
          (object) sRequirementId,
          (object) sPaymentCode,
          (object) sNumberOrder,
          (object) transactionId,
          (object) actionCode,
          (object) sResultResponse,
          (object) iSystemUserId,
          (object) DateTime.Now
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string DecryptQueryString(string strQueryString)
    {
      string[] strArray = strQueryString.Split('%');
      strQueryString = strArray[0];
      string seguridad = strArray[1];
      return new Encryption().Decrypt(strQueryString, seguridad);
    }

    protected void btn_aceptar_Click(object sender, EventArgs e)
    {
    }

    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      new List<string>() { this.txtCorreo.Text };
      this.SendEMail();
    }

    private void SendEMail()
    {
      try
      {
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.SMTPServerConfiguration.ToString(),
          (object) "",
          (object) "1",
          (object) "1"
        });
        string str1 = dataTable.Rows[0]["v_Value"].ToString();
        int num = int.Parse(dataTable.Rows[1]["v_Value"].ToString());
        string userName = dataTable.Rows[2]["v_Value"].ToString();
        string password = dataTable.Rows[3]["v_Value"].ToString();
        bool boolean = Convert.ToBoolean(dataTable.Rows[4]["v_Value"]);
        string str2 = "Constancia de Pago VISA";
        string str3 = "Estimado,  <br><br>Gracias por su compra a través de VISA. <br>Para cualquier consulta o reclamo llamar al centro de atención de la AAP. <br><br><strong>Datos de la Compra</strong> <br><br>Nombre de Tienda:              " + this.lbltienda.Text + "<br>Teléfono:                      " + this.lbltelefono.Text + "<br>Dirección Comercial:           " + this.lbldireccion.Text + "<br>Número de Pedido:              " + this.lblNumpedido.Text + "<br>Número de Tarjeta Enmascarada: " + this.lblNumtarjeta.Text + "<br>Fecha y Hora de Pedido:        " + this.lblFechaHora.Text + "<br>Importe de Transacción:        " + this.lblimorteTrans.Text + "<br>Moneda:                        " + this.lblMoneda.Text + "<br>Descripción del Producto:      " + this.lblProducto.Text + "<br>Nombre del Propietario:        " + this.lblPropietario.Text + "<br>Descripción de Respuesta:      " + this.lblRespuesta.Text + "<br><br><strong>Dpto. Atención al cliente</strong> <br><strong>En Lima al 640-3637 opción 1 </strong> <br><strong>En Provincia al 0800-7-1111 opción 1 </strong> <br><strong>Atentamente AAP </strong><br>";
        string address = "soporte@sssdeperu.com";
        string addresses = this.txtCorreo.Text.Trim();
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("<html><body>");
        stringBuilder.AppendLine(str3);
        stringBuilder.AppendLine("</body></html>");
        MailMessage message = new MailMessage();
        message.From = new MailAddress(address);
        message.To.Add(addresses);
        message.Subject = str2;
        message.Body = stringBuilder.ToString();
        message.IsBodyHtml = true;
        message.Priority = MailPriority.Normal;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Host = str1;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.EnableSsl = boolean;
        smtpClient.Port = num;
        smtpClient.Credentials = (ICredentialsByHost) new NetworkCredential(userName, password);
        smtpClient.Send(message);
        smtpClient.Dispose();
        message.Dispose();
        SIIV.Common.Resource.Message.SetMessage(this.lblMensage, new HandledException(2, Constants.REQUIREMENT_EXITO_SendMail));
      }
      catch (HandledException ex)
      {
      }
      catch (Exception ex)
      {
      }
    }

    protected void btnPrint_Click(object sender, EventArgs e) => this.SetCrystalReport();

    protected void btnFinishPayout_Click(object sender, EventArgs e)
    {
    }

    private void PopupClose()
    {
      string script = "SendInfoPopup();";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void SetCrystalReport()
    {
      ReportDocument reportDocument = new ReportDocument();
      string filename = this.Server.MapPath("../PaymentPOS/RptPaymentVisa.rpt");
      reportDocument.Load(filename);
      reportDocument.SetParameterValue("TituloReporte", (object) "COMPROBANTE PAGO VISA");
      reportDocument.SetParameterValue("NombreTienda", (object) this.lbltienda.Text);
      reportDocument.SetParameterValue("Telefono", (object) this.lbltelefono.Text);
      reportDocument.SetParameterValue("DireccionTienda", (object) this.lbldireccion.Text);
      reportDocument.SetParameterValue("NumPedido", (object) this.lblNumpedido.Text);
      reportDocument.SetParameterValue("NumTarjeta", (object) this.lblNumtarjeta.Text);
      reportDocument.SetParameterValue("FechPedido", (object) this.lblFechaHora.Text);
      reportDocument.SetParameterValue("ImporteTransac", (object) this.lblimorteTrans.Text);
      reportDocument.SetParameterValue("Moneda", (object) this.lblMoneda.Text);
      reportDocument.SetParameterValue("DescProducto", (object) this.lblProducto.Text);
      reportDocument.SetParameterValue("NombComprador", (object) this.lblPropietario.Text);
      reportDocument.SetParameterValue("DescRespuesta", (object) this.lblRespuesta.Text);
      reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "PagoVisa");
      reportDocument.Close();
      ((Component) reportDocument).Dispose();
    }

    protected void Terms_Conditions_Click(object sender, EventArgs e)
    {
      this.CreatePopUpServer("Visualizar", "../../Warehouse/Searchs/WarehouseListRequestDetail.aspx", "770px", "470px");
    }

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    public int UpdatePaymentLog(string vTransactionVisaCode, string vMessage, bool bException)
    {
      DataTable dataTable = new DataTable();
      RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
      int int32 = this.Session["iLogId"] != null ? Convert.ToInt32(this.Session["iLogId"].ToString()) : 0;
      try
      {
        DataTable paymentLogById = new RequirementQueriesBL().GetPaymentLogById(int32);
        return requirementQueriesBl.UpdatePaymentLog(new ArrayList()
        {
          (object) paymentLogById.Rows[0]["i_PaymentVisaLogId"].ToString(),
          (object) paymentLogById.Rows[0]["i_RequirementId"].ToString(),
          (object) paymentLogById.Rows[0]["v_PaymentCode"].ToString(),
          (object) paymentLogById.Rows[0]["f_PriceTotal"].ToString(),
          (object) paymentLogById.Rows[0]["v_OrderNumber"].ToString(),
          (object) vTransactionVisaCode,
          (object) vMessage,
          (object) paymentLogById.Rows[0]["i_InsertUserId"].ToString(),
          (object) paymentLogById.Rows[0]["d_InsertDate"].ToString(),
          (object) paymentLogById.Rows[0]["i_InsertUserId"].ToString(),
          (object) DateTime.Now
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
