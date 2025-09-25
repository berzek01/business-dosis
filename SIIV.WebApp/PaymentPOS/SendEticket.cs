// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.PaymentPOS.SendEticket
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

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

    protected void Page_Load(object sender, EventArgs e)
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
            this.merchantLogo = "data:image/png;base64,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U31MrD/788Y3vjF69atfHb3+9a/3tTvhhBN87RgT72Ac2267re8BjbVljdhT2le/+lW/j3czLvp529veFj388MM+byFIJAIaSXLxcWy22WbeN3vJ+LmH649//KP385rXvMb3ZuONN46eeOKJaNmyZdGb3vSmCFishQkRBn8/nxdddJGv5+abbx7NnTvX77/hhhscfi688EJfY/oFXun717/+tf/G2IFdxsT7PvnJT/r8aD//+c+jnXfe2ccQ1vNLX/qSzy38ewwQmvijaU561llnmRbJ7r77bnvggQfGDBPXXHONff/73zcBsAk57bOf/axTGbjEFVdcYQIMBR10Gc//13/9l3/HJxQRavX73//ePvGJT5iAwn75y1/aH/7wB3v7299ukydPNjgzHCRwArjNJZdcYkJyu+uuu+zee+/137D+CrHs3e9+t/9G/3/605/sG9/4hnPIQP0ZP5RWQGbf+ta37P3vf79dfvnl9oEPfMB++tOfmoDbuQJiK1Sevs4///wxqsr7AqfRXvgaMCY4O/PjWcQh1ieI5IsWLXKOHfQoPh966CHje+YoQLY99tjDnn76aZsyZYr/LSDzMX/3u9+1MxQz/e1vf9vHIQSxr3zlK3bdddd5/0gdSCg8KyD376D0Z555pnMDfgvSj5DdRDB9rf/+97/b1KlT7Qc/+IFdf/31JgJg++67r2200Ua+x+wf+ykC5tyD77jnJz/5iYmo+H5fcMEFLjGxP7yHJgJpH/rQh/w+EXP/njnRD2vF/YxPSGNCEl+zz33uc8413/Wud9m//vUvvw/uhxQkJLAjjzzS95EC2mICvuf8FvRC9iHYRPiE27MeSEesGbABXPAdXJTfhaS+/2IG9rrXvc73jf1AGjjvvPNMhNjOPfdc+/rXvz7GzVnrsPeB2zKWoL6tFW7aBII7JdQGRkIgp7DHHHOMU0WagMi539VXXz32CgGiU7q3vvWtTrGhgIETChGdK9x+++3OueBwUK1aygh1gqK+973vdcquRfG+eYb7v/e970VbbLGFcz8a93/qU59yKiig8e/C+BjLVVddFW284UbRoocfiXq7e6LNN90seu97DtLh1FE0PCgqrM+Pf/zj3jdjYr5wFqg8Y9UGRxIRndMzdxr9hnbbbbf5s3AkKLgQ3n9nXIcddlgkojNGcRkXlDpwH+YNNxOSRAJY75K1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";
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
