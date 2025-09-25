// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.PaymentPOS.SendEticket
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Payment.BL;
using SIIV.Requirement.BL;
using SIIV.WebApp.UserControls;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.PaymentPOS
{
  public class SendEticket : Page
  {
    public string sessionToken;
    public string merchantId;
    public string buttonSize;
    public string buttonColor;
    public string merchantLogo;
    public string merchantName;
    public string formButtonColor;
    public string showAmount;
    public string respuestaNumPedido;
    public string channel;
    public string amountAux;
    public string cardholderName;
    public string cardholderlastName;
    public string cardholderEmail;
    public string recurrence;
    public string expirationminutes;
    public string timeouturl;
    public string showamount;
    public string usertoken;
    public string operacion;
    public string currency;
    public string accessKeyId;
    public string secretAccessKey;
    private RSAParameters publicKey;
    private RSAParameters privateKey;
    protected HtmlForm form1;
    protected Label lbltienda;
    protected Label lblTelefono;
    protected Label lblDireccionComercial;
    protected Label lblImporteTransaccion;
    protected Label lblTotalAPagar;
    protected Label lblMensajeVisa;

    public static string GetIP4Address()
    {
      string address = ConfigurationManager.AppSettings["UrlGetIpAddress"].ToString();
      try
      {
        return new WebClient().DownloadString(address);
      }
      catch (Exception ex)
      {
        return "127.0.0.1";
      }
    }

    protected void Page_Load(object s  ender, EventArgs e)
    {
      RequirementQueriesBL requirementQueriesBl = new RequirementQueriesBL();
      string str1;
      if (this.Session["RequirementIds"] == null)
        str1 = "-1";
      else
        str1 = this.Session["RequirementIds"].ToString().Split('|')[0];
      string str2 = str1;
      string str3 = string.Empty;
      string empty = string.Empty;
      string str4 = string.Empty;
      try
      {
        if (this.Page.IsPostBack || this.Request.QueryString["PaymentCode"] == null)
          return;
        string v_PaymentCode = this.Request.QueryString["PaymentCode"].ToString();
        this.lbltienda.Text = ConfigurationManager.AppSettings["NomTienda"].ToString();
        this.lblTelefono.Text = ConfigurationManager.AppSettings["TelTienda"].ToString();
        this.lblDireccionComercial.Text = ConfigurationManager.AppSettings["DirecTienda"].ToString();
        double num = Convert.ToDouble(new RequirementQueriesBL().GetPaymentDatabyPaymentCode(v_PaymentCode.Trim()).Rows[0]["f_PriceTotal"].ToString());
        this.lblTotalAPagar.Text = "S/. " + num.ToString();
        this.merchantId = ConfigurationManager.AppSettings["merchantId"].ToString();
        this.accessKeyId = ConfigurationManager.AppSettings["accessKeyId"].ToString();
        this.secretAccessKey = ConfigurationManager.AppSettings["secretAccessKey"].ToString();
        string base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes(this.accessKeyId + ":" + this.secretAccessKey));
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        ServicePointManager.DefaultConnectionLimit = 9999;
        this.lblImporteTransaccion.Text = num.ToString();
        HttpWebRequest httpWebRequest1 = WebRequest.Create(ConfigurationManager.AppSettings["urlSession"].ToString()) as HttpWebRequest;
        httpWebRequest1.Method = "POST";
        httpWebRequest1.Headers.Add("Authorization", "Basic " + base64String);
        new StreamWriter(httpWebRequest1.GetRequestStream()).Close();
        try
        {
          StreamReader streamReader1 = new StreamReader((httpWebRequest1.GetResponse() as HttpWebResponse).GetResponseStream());
          string end1 = streamReader1.ReadToEnd();
          streamReader1.Close();
          EntitiesPayment.MerchantDefineData merchantDefineData = new EntitiesPayment.MerchantDefineData();
          merchantDefineData.MDD4 = this.Session["ClientMailforVISA"].ToString();
          merchantDefineData.MDD32 = this.Session["ClientIdforVISA"].ToString();
          merchantDefineData.MDD33 = this.Session["ClientTypeDocumentforVISA"].ToString();
          merchantDefineData.MDD34 = this.Session["ClientNumberDocumentforVISA"].ToString();
          merchantDefineData.MDD75 = this.Session["ClientTypeRegisterforVISA"].ToString();
          merchantDefineData.MDD77 = Convert.ToInt32(this.Session["ClientTotalDaysRegisterforVISA"]);
          EntitiesPayment.Antifraud antifraud = new EntitiesPayment.Antifraud();
          string ip4Address = SendEticket.GetIP4Address();
          antifraud.clientIp = ip4Address;
          antifraud.merchantDefineData = merchantDefineData;
          string str5 = SendEticket.JsonSerializer<EntitiesPayment.SesionTokenRequest>(new EntitiesPayment.SesionTokenRequest()
          {
            channel = "web",
            amount = Convert.ToDouble(num),
            recurrenceMaxAmount = 0.0,
            antifraud = antifraud
          });
          HttpWebRequest httpWebRequest2 = WebRequest.Create(ConfigurationManager.AppSettings["urlCreateSessionTokenAPI"] + this.merchantId) as HttpWebRequest;
          httpWebRequest2.Method = "POST";
          httpWebRequest2.ContentType = "application/json";
          httpWebRequest2.Headers.Add("Authorization", end1);
          StreamWriter streamWriter = new StreamWriter(httpWebRequest2.GetRequestStream());
          streamWriter.Write(str5);
          streamWriter.Close();
          try
          {
            StreamReader streamReader2 = new StreamReader((httpWebRequest2.GetResponse() as HttpWebResponse).GetResponseStream());
            string end2 = streamReader2.ReadToEnd();
            streamReader2.Close();
            EntitiesPayment.SesionTokenResponse sesionTokenResponse = (EntitiesPayment.SesionTokenResponse) new DataContractJsonSerializer(typeof (EntitiesPayment.SesionTokenResponse)).ReadObject((Stream) new MemoryStream(Encoding.UTF8.GetBytes(end2)));
            this.respuestaNumPedido = DateTime.Now.Ticks.ToString().Substring(1, 12);
            requirementQueriesBl.UpdateETicketVisa(v_PaymentCode, "", this.respuestaNumPedido);
            str4 = this.respuestaNumPedido;
            str3 = v_PaymentCode;
            empty = num.ToString();
            this.sessionToken = sesionTokenResponse.sessionKey;
            this.channel = "web";
            this.buttonSize = "DEFAULT";
            this.buttonColor = "NAVY";
            this.merchantLogo = "data:image/png;base64,iVBORw==EXTEND>>>";
            this.merchantName = "ASOCIACIÓN AUTOMOTRÍZ DEL PERÚ";
            this.formButtonColor = "#D80000";
            this.cardholderName = "";
            this.cardholderlastName = "";
            this.cardholderEmail = "";
            this.recurrence = "FALSE";
            this.expirationminutes = ConfigurationManager.AppSettings["MinutesTimeOut"].ToString();
            this.timeouturl = ConfigurationManager.AppSettings["UrlRedirectTimeOut"].ToString();
            this.showamount = "true";
            this.usertoken = "";
            this.operacion = "true";
            this.currency = "PEN";
            string str6 = SendEticket.JsonSerializer<EntitiesPayment.Order>(new EntitiesPayment.Order()
            {
              purchaseNumber = this.respuestaNumPedido,
              amount = num,
              currency = this.currency
            });
            string str7 = this.Session["RequirementPlateType"].ToString();
            string str8 = Convert.ToInt32(str7) == Convert.ToInt32((object) enmRequirementPlateType.Masiva) ? num.ToString() : "";
            string str9 = Convert.ToInt32(str7) == Convert.ToInt32((object) enmRequirementPlateType.Masiva) ? "" : num.ToString();
            this.amountAux = num.ToString();
            this.form1.Action = ConfigurationManager.AppSettings["UrlRedirectResponseVisa"] + "?" + this.EncryptQueryString(string.Format("&quot;&quot;&quot;&quot;Token={0}&_order={1}&_operacion={2}&_merchantId={3}&RequirementPlateType={4}&vPaymentCode={5}&PriceTotalVisa={6}&TotalAPagar={7}&IP4Address={8}&RequirementId={9}", (object) end1, (object) str6, (object) this.operacion, (object) this.merchantId, (object) str7, (object) v_PaymentCode, (object) str8, (object) str9, (object) ip4Address, (object) str2));
            this.ManagementPaymentLog(new ArrayList()
            {
              (object) 0,
              (object) str2,
              (object) str3,
              (object) empty,
              (object) str4,
              (object) null,
              (object) null,
              (object) (this.Session["SystemUser"] == null ? 0 : (this.Session["SystemUser"] as SystemUser).i_SystemUserId),
              (object) DateTime.Now,
              (object) null,
              (object) null
            }, false);
          }
          catch (WebException ex)
          {
            ex.Message.ToString();
            this.lblMensajeVisa.Text = ex.Message.ToString();
            this.ManagementPaymentLog(new ArrayList()
            {
              (object) 0,
              (object) str2,
              (object) str3,
              (object) empty,
              (object) str4,
              (object) null,
              (object) ("Error : -> Message WebException : " + ex.Message + " | StackTrace" + ex.StackTrace),
              (object) (this.Session["SystemUser"] == null ? 0 : (this.Session["SystemUser"] as SystemUser).i_SystemUserId),
              (object) DateTime.Now,
              (object) null,
              (object) null
            }, true);
          }
        }
        catch (WebException ex)
        {
          ex.Message.ToString();
          this.lblMensajeVisa.Text = ex.Message.ToString();
        }
      }
      catch (Exception ex)
      {
        this.lblMensajeVisa.Text = "***ADVERTENCIA*** : SERVICIO NO DISPONIBLE DE VISA. VUELVA A INTENTARLO EN OTRO MOMENTO.";
      }
    }

    public int ManagementPaymentLog(ArrayList arrParametersLog, bool bException)
    {
      int int32 = this.Session["iLogId"] == null ? 0 : Convert.ToInt32(this.Session["iLogId"].ToString());
      DataTable dataTable = int32 == 0 ? new DataTable() : new RequirementQueriesBL().GetPaymentLogById(int32);
      int num;
      if (int32 == 0)
      {
        num = new RequirementQueriesBL().InsertPaymentLog(new ArrayList()
        {
          arrParametersLog[0],
          arrParametersLog[1],
          arrParametersLog[2],
          arrParametersLog[3],
          arrParametersLog[4],
          arrParametersLog[5],
          arrParametersLog[6],
          arrParametersLog[7],
          arrParametersLog[8],
          arrParametersLog[9],
          arrParametersLog[10]
        });
        this.Session["iLogId"] = (object) num;
      }
      else
        num = new RequirementQueriesBL().UpdatePaymentLog(new ArrayList()
        {
          (object) dataTable.Rows[0]["i_PaymentVisaLogId"].ToString(),
          (object) dataTable.Rows[0]["i_RequirementId"].ToString(),
          (object) dataTable.Rows[0]["v_PaymentCode"].ToString(),
          (object) dataTable.Rows[0]["f_PriceTotal"].ToString(),
          arrParametersLog[4],
          (object) null,
          bException ? arrParametersLog[6] : (object) null,
          (object) dataTable.Rows[0]["i_InsertUserId"].ToString(),
          (object) dataTable.Rows[0]["d_InsertDate"].ToString(),
          arrParametersLog[7],
          (object) DateTime.Now
        });
      return num;
    }

    public string EncryptQueryString(string strQueryString)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(strQueryString);
      byte[] hash;
      using (SHA256 shA256 = SHA256.Create())
        hash = shA256.ComputeHash(bytes);
      this.AssignNewKey();
      string base64String = Convert.ToBase64String(this.SignData(hash));
      return new Encryption().Encrypt(strQueryString, base64String) + "%" + base64String;
    }

    public void AssignNewKey()
    {
      using (RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider())
      {
        cryptoServiceProvider.PersistKeyInCsp = false;
        this.publicKey = cryptoServiceProvider.ExportParameters(false);
        this.privateKey = cryptoServiceProvider.ExportParameters(true);
      }
    }

    public byte[] SignData(byte[] hashOfDataToSign)
    {
      using (RSACryptoServiceProvider key = new RSACryptoServiceProvider())
      {
        key.PersistKeyInCsp = false;
        key.ImportParameters(this.privateKey);
        RSAPKCS1SignatureFormatter signatureFormatter = new RSAPKCS1SignatureFormatter((AsymmetricAlgorithm) key);
        signatureFormatter.SetHashAlgorithm("SHA256");
        return signatureFormatter.CreateSignature(hashOfDataToSign);
      }
    }

    public static string JsonSerializer<T>(T t)
    {
      DataContractJsonSerializer contractJsonSerializer = new DataContractJsonSerializer(typeof (T));
      MemoryStream memoryStream = new MemoryStream();
      contractJsonSerializer.WriteObject((Stream) memoryStream, (object) t);
      string input = Encoding.UTF8.GetString(memoryStream.ToArray());
      memoryStream.Close();
      string pattern = "\\\\/Date\\((\\d+)\\+\\d+\\)\\\\/";
      MatchEvaluator evaluator = new MatchEvaluator(SendEticket.ConvertJsonDateToDateString);
      return new Regex(pattern).Replace(input, evaluator);
    }

    private static string ConvertJsonDateToDateString(Match m)
    {
      string empty = string.Empty;
      DateTime dateTime = new DateTime(1970, 1, 1);
      dateTime = dateTime.AddMilliseconds((double) long.Parse(m.Groups[1].Value));
      dateTime = dateTime.ToLocalTime();
      return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
    }
  }
}
