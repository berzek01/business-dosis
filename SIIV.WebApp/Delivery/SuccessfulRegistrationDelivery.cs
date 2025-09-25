// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Delivery.SuccessfulRegistrationDelivery
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

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
namespace SIIV.WebApp.Delivery
{
  public class SuccessfulRegistrationDelivery : Page
  {
    private double ComisionCanalAtencion = Convert.ToDouble(ConfigurationManager.AppSettings[nameof (ComisionCanalAtencion)]);
    protected UpdatePanel updatepanel1;
    protected HtmlTableRow trPaymentCode;
    protected Label Label2;
    protected Label lblPaymentCode;
    protected HtmlTableRow trPlateRequirement;
    protected Label label5;
    protected Label lblPlateRequirement;
    protected HtmlTableRow trPrice;
    protected Label label6;
    protected Label lblPrice;
    protected Label Label1;
    protected HtmlGenericControl DiasPagoVisa;
    protected Label lblDias;
    protected HtmlGenericControl trDelivery;
    protected Button Button1;
    protected Button Button2;
    protected Button btnSendMail;
    protected Button btnFacElec;
    protected HtmlTableRow trMail;
    protected Label Label3;
    protected TextBox txtMail;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected ValidatorCalloutExtender ValidatorCalloutExtender1;
    protected RegularExpressionValidator RegularExpressionValidator1;
    protected ValidatorCalloutExtender RegularExpressionValidator1_ValidatorCalloutExtender;
    protected Button btnSend;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      try
      {
        SIIV.Common.Resource.Message.SetMessage(this.Label1, enmMessageType.Success, "Su solicitud ha sido registrada correctamente.");
        int int32 = Convert.ToInt32(this.Request.QueryString["RequirementId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
        this.lblMessage.Text = string.Empty;
        DataTable dataTable = new DataTable();
        DataTable deliveryCur = new RequirementQueriesBL().GenerateDeliveryCUR(int32);
        string str = deliveryCur.Rows[0]["i_PlateTypeId"].ToString();
        deliveryCur.Rows[0]["i_ProcessTypeId"].ToString();
        double num = Math.Round(deliveryCur.Rows[0]["f_PriceTotal"].ToString() != "" ? Convert.ToDouble(deliveryCur.Rows[0]["f_PriceTotal"]) : 0.0, 2);
        if (Convert.ToInt32(this.Session["VisaPagoConforme"]) == 1)
        {
          this.lblPaymentCode.Text = " - ";
          this.lblPlateRequirement.Text = this.Session["v_PlateNew"].ToString();
          num = Math.Round(Convert.ToDouble(num), 2);
          this.label6.Text = "Total Pagado: ";
          this.DiasPagoVisa.Visible = false;
          this.trPaymentCode.Visible = false;
          this.trPlateRequirement.Visible = true;
        }
        else
        {
          this.trPaymentCode.Visible = true;
          this.trPlateRequirement.Visible = true;
          this.lblPaymentCode.Text = deliveryCur.Rows[0]["v_PaymentCode"].ToString();
          this.lblPlateRequirement.Text = this.Session["v_PlateNew"].ToString();
          this.DiasPagoVisa.Visible = true;
        }
        this.lblPrice.Text = "S/ " + num.ToString("0.00");
        if (str == "12")
        {
          this.trDelivery.Visible = true;
          this.lblDias.Text = " 1 día ";
        }
        if (this.lblPaymentCode.Text.Trim() == "")
          this.trPaymentCode.Visible = false;
        this.Session.Remove("VisaPagoConforme");
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

    protected void Button1_Click(object sender, EventArgs e)
    {
      try
      {
        this.generateCur(1);
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
      try
      {
        this.Response.Redirect("~/Delivery/Query/BookQueryPlates.aspx");
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
        this.txtMail.Text = new RequirementQueriesBL().GetRequirementContributor(1, Convert.ToInt32(this.Request.QueryString["RequirementId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture)).Rows[0]["v_Email"].ToString();
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        this.txtMail.Text = "";
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
        dataTable2.Dispose();
        string cur = this.generateCur(2);
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
        SIIV.Common.Resource.Message.SetMessage(this.lblMessage, new HandledException(2, SIIV.SystemParameter.BL.Constants.REQUIREMENT_EXITO_SendMail));
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

    public string generateCur(int intType)
    {
      try
      {
        string cur = "";
        if (this.Request.QueryString["RequirementId"] != null)
        {
          string empty = string.Empty;
          using (ReportDocument reportDocument = new ReportDocument())
          {
            int int32 = Convert.ToInt32(this.Request.QueryString["RequirementId"].ToString((IFormatProvider) CultureInfo.CurrentCulture), (IFormatProvider) CultureInfo.CurrentCulture);
            string filename = this.Server.MapPath("../Reports/ReportCurDelivery.rpt");
            reportDocument.Load(filename);
            DataTable deliveryCur = new RequirementQueriesBL().GenerateDeliveryCUR(int32);
            if (deliveryCur != null)
            {
              if (deliveryCur.Rows.Count > 1)
                deliveryCur.Rows.RemoveAt(1);
              deliveryCur.Columns.Add(new DataColumn()
              {
                ColumnName = "ImageBarPlate",
                DataType = typeof (byte[])
              });
              byte[] numArray1 = this.ImagenBarCode(deliveryCur.Rows[0]["v_PlateNew"].ToString());
              deliveryCur.Rows[0]["ImageBarPlate"] = (object) numArray1;
              deliveryCur.Columns.Add(new DataColumn()
              {
                ColumnName = "Requisite",
                DataType = typeof (string)
              });
              string requisitebyRequirement = new RequirementQueriesBL().GetRequisitebyRequirement(int32, Convert.ToInt32(deliveryCur.Rows[0]["i_ProcessTypeId"], (IFormatProvider) CultureInfo.CurrentCulture));
              deliveryCur.Rows[0]["Requisite"] = (object) requisitebyRequirement;
              deliveryCur.Columns.Add(new DataColumn()
              {
                ColumnName = "ImageBarCode",
                DataType = typeof (byte[])
              });
              byte[] numArray2 = this.ImagenBarCode(deliveryCur.Rows[0]["v_PaymentCode"].ToString());
              deliveryCur.Rows[0]["ImageBarCode"] = (object) numArray2;
              reportDocument.SetDataSource(deliveryCur);
              if (intType == 1)
              {
                reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, SIIV.SystemParameter.BL.Constants.ARCHIVO_NOMBRECUR + int32.ToString());
                cur = "";
              }
              else
              {
                string fileName = this.Server.MapPath("../Requirement/Temp/") + SIIV.SystemParameter.BL.Constants.ARCHIVO_NOMBRECUR + int32.ToString() + ".pdf";
                reportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, fileName);
                cur = fileName;
              }
            }
            reportDocument.Close();
            ((Component) reportDocument).Dispose();
            deliveryCur.Dispose();
          }
        }
        return cur;
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

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.updatepanel1, this.updatepanel1.GetType(), "Script", script, true);
    }

    protected void btnFacElec_Click(object sender, EventArgs e)
    {
    }
  }
}
