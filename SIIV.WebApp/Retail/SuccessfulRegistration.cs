// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Retail.SuccessfulRegistration
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using BarcodeLib;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.Common.Resource;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Retail
{
  public class SuccessfulRegistration : Page
  {
    private RequirementQueriesBL oRequirementBL;
    private double ComisionCanalAtencion = Convert.ToDouble(ConfigurationManager.AppSettings["ComisionCanalAtencionRetail"]);
    protected UpdatePanel updatepanel1;
    protected HtmlTableRow trPaymentCode;
    protected Label Label2;
    protected Label lblPaymentCode;
    protected HtmlTableRow trPrice;
    protected Label label6;
    protected Label lblPrice;
    protected Label Label1;
    protected HtmlGenericControl DiasPagoVisa;
    protected HtmlGenericControl divVisa;
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
        if (this.Session["MontoTotal"] != null)
        {
          this.ViewState["strMonto"] = (object) this.Session["MontoTotal"].ToString();
          this.Session.Remove("MontoTotal");
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
        SIIV.Common.Resource.Message.SetMessage(this.Label1, enmMessageType.Success, "Su Compra ha sido registrada correctamente.");
        this.lblPaymentCode.Text = this.ViewState["intRequirementId"].ToString();
        if (!string.IsNullOrEmpty(this.ViewState["strMonto"].ToString()))
          this.lblPrice.Text = this.Session["VisaPago"] != (object) "1" ? "S/. " + Math.Round(Convert.ToDouble(this.ViewState["strMonto"].ToString()), 2).ToString() : "S/. " + Math.Round(Convert.ToDouble(this.ViewState["strMonto"].ToString()), 2).ToString();
        else
          this.lblPrice.Visible = false;
        if (this.Session["Tipo"] == (object) "2")
          this.Button2.Text = "Nuevo Pago";
        this.Session.Remove("VisaPago");
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
      if (this.Session["Tipo"] == (object) "2")
      {
        this.Response.Redirect("RegisterPayment.aspx");
      }
      else
      {
        this.Session["Order"] = (object) null;
        this.Response.Redirect("RegisterRetail.aspx");
      }
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
        Log.WriteLog("Placas", "Requirement", "SuccessfulMassiveRegistration", nameof (btnSend_Click), "Email - CUR Generado", addresses + " -" + cur);
        Attachment attachment = new Attachment(cur);
        MailMessage message = new MailMessage();
        message.From = new MailAddress(address);
        message.To.Add(addresses);
        message.Subject = str2;
        message.Body = str3 + str4;
        message.IsBodyHtml = true;
        message.Priority = MailPriority.Normal;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
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
  }
}
