// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Delivery.Query.SendEmail
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using BarcodeLib;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Delivery.Query
{
  public class SendEmail : Page
  {
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected RadioButtonList rdbTypeComprobantes;
    protected HtmlTableCell Td8;
    protected TextBox txtEmail;
    protected HtmlTableRow tr1;
    protected Button Enviar;
    protected Button volver;
    protected Label lblMensaje;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.ViewState["i_Requirementid"] = (object) this.Request.QueryString["i_Requirementid"].ToString();
      this.ViewState["v_beneficiaryEmail"] = (object) this.Request.QueryString["Email"].ToString();
      this.txtEmail.Text = (string) this.ViewState["v_beneficiaryEmail"];
    }

    protected void rdbTypeComprobantes_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.rdbTypeComprobantes.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture) == "1")
        {
          this.lblMensaje.Text = "";
          this.lblMensaje.Visible = false;
          this.SendMailComprobanteElectronico();
          this.txtEmail.Enabled = true;
        }
        else
        {
          this.lblMensaje.Text = "";
          this.lblMensaje.Visible = false;
          this.txtEmail.Enabled = true;
        }
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(-100, ex));
      }
    }

    public void SendMailComprobanteElectronico()
    {
      try
      {
        string v_Url = "";
        string v_Estado = "";
        string str = "";
        str = new RequirementManagementBL().EBillingUrl(Convert.ToInt32(this.ViewState["i_Requirementid"]), 0, out v_Url, out v_Estado);
        this.ViewState["EstadoDocument"] = (object) v_Estado;
        if (Convert.ToInt32(v_Estado) != 100)
        {
          this.txtEmail.Text = (string) this.ViewState["v_beneficiaryEmail"];
        }
        else
        {
          this.lblMensaje.Visible = true;
          SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(1, "Solicitud no cuenta con un documento Electronico Generado"));
        }
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(-100, ex));
      }
    }

    protected void Enviar_Click(object sender, EventArgs e)
    {
      this.lblMensaje.Visible = false;
      string str = this.rdbTypeComprobantes.SelectedValue.ToString((IFormatProvider) CultureInfo.CurrentCulture);
      if (this.rdbTypeComprobantes.SelectedIndex >= 0)
      {
        if (this.txtEmail.Text != "")
        {
          if (str == "1")
          {
            if (Convert.ToString(this.ViewState["EstadoDocument"]) != "C")
            {
              this.sendEmailComprobante();
            }
            else
            {
              this.lblMensaje.Visible = true;
              SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(1, "Solicitud no cuenta con un documento Electronico Generado"));
            }
          }
          else
          {
            this.lblMensaje.Text = "";
            this.lblMensaje.Visible = false;
            this.sendEmailCUR();
          }
        }
        else
        {
          this.lblMensaje.Visible = true;
          SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(1, "Debe ingresar una direccion de Email"));
        }
      }
      else
      {
        this.lblMensaje.Visible = true;
        SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(1, "Debe Seleccionar Un tipo de Documento a Enviar"));
      }
    }

    protected void sendEmailComprobante()
    {
      DataTable dataTable1 = new DataTable();
      try
      {
        ProductWarehouseQueriesBL warehouseQueriesBl = new ProductWarehouseQueriesBL();
        DataTable dataTable2 = new DataTable();
        DataTable dataTable3 = warehouseQueriesBl.RequirementSendMailEbillingDelivery(Convert.ToInt32(this.ViewState["i_Requirementid"], (IFormatProvider) CultureInfo.CurrentCulture));
        Email email = new Email();
        bool flag = false;
        DataTable dataTable4 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) "990",
          (object) "",
          (object) "1",
          (object) "1"
        });
        string str1 = dataTable4.Rows[0]["v_Value"].ToString();
        string str2 = dataTable4.Rows[1]["v_Value"].ToString();
        dataTable4.Rows[3]["v_Value"].ToString();
        string str3 = dataTable4.Rows[4]["v_Value"].ToString();
        string pstrSMTPServer = dataTable4.Rows[5]["v_Value"].ToString();
        int pintSMTPPort = int.Parse(dataTable4.Rows[6]["v_Value"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        string str4 = dataTable4.Rows[7]["v_Value"].ToString();
        string pstrSMTPPassword = dataTable4.Rows[8]["v_Value"].ToString();
        string str5 = dataTable4.Rows[9]["v_Value"].ToString();
        string str6 = dataTable4.Rows[10]["v_Value"].ToString();
        bool boolean1 = Convert.ToBoolean(dataTable4.Rows[11]["v_Value"], (IFormatProvider) CultureInfo.CurrentCulture);
        bool boolean2 = Convert.ToBoolean(dataTable4.Rows[12]["v_Value"], (IFormatProvider) CultureInfo.CurrentCulture);
        bool boolean3 = Convert.ToBoolean(dataTable4.Rows[13]["v_Value"], (IFormatProvider) CultureInfo.CurrentCulture);
        if (dataTable3 == null)
          return;
        dataTable1 = new DataTable();
        string pstrEmailSubject = str1;
        DataTable dataTable5 = dataTable3;
        if (dataTable5.Rows.Count > 0)
        {
          StringBuilder stringBuilder = new StringBuilder();
          string str7 = str2;
          string newValue1 = dataTable5.Rows[0]["Nombre"].ToString();
          string str8 = dataTable5.Rows[0]["TipoDocumento"].ToString();
          string newValue2 = dataTable5.Rows[0]["TipoDocumentoName"].ToString();
          string newValue3 = dataTable5.Rows[0]["DocumentoElectronico"].ToString();
          string newValue4 = dataTable5.Rows[0]["RazonSocial"].ToString();
          string str9 = dataTable5.Rows[0]["NumeroDocumento"].ToString();
          DateTime dateTime = Convert.ToDateTime(dataTable5.Rows[0]["FechaEmision"]);
          string newValue5 = dataTable5.Rows[0]["TotalMontoPagar"].ToString().Replace(",", ".");
          string str10 = dataTable5.Rows[0]["CodigoMoneda"].ToString();
          string str11 = this.txtEmail.Text.Trim();
          string address1 = str3 + str8 + "-" + newValue3 + "?ruc=" + str9 + "&fecha=" + dateTime.ToString("dd-MM-yyyy") + "&monto=" + newValue5 + "&moneda=" + str10;
          string address2 = ConfigurationManager.AppSettings["EBillingUrlXml"].ToString() + str8 + "-" + newValue3 + "?ruc=" + str9 + "&fecha=" + dateTime.ToString("dd-MM-yyyy") + "&monto=" + newValue5 + "&moneda=" + str10;
          string newValue6 = str6 + str8 + "-" + newValue3 + "?ruc=" + str9 + "&fecha=" + dateTime.ToString("dd-MM-yyyy") + "&monto=" + newValue5 + "&moneda=" + str10;
          string str12 = str7.Replace("#CLIENTENOMBRE#", newValue1).Replace("#FECHA#", dateTime.ToString("dd-MM-yyyy HH:mm")).Replace("#EMISOR#", newValue4).Replace("#TIPODOCUMENTO#", newValue2).Replace("#NRODOCUMENTO#", newValue3).Replace("#TOTAL#", newValue5).Replace("#URL#", newValue6);
          string tempPath = Path.GetTempPath();
          string str13 = str9 + "-" + str8 + "-" + newValue3 + ".xml";
          string str14 = str9 + "-" + str8 + "-" + newValue3 + ".pdf";
          string path1 = tempPath + str13;
          string path2 = tempPath + str14;
          List<string> plstAttachments = new List<string>();
          string empty = string.Empty;
          byte[] buffer1 = new WebClient().DownloadData(address2);
          if (!System.IO.File.Exists(path1))
          {
            using (FileStream fileStream = System.IO.File.Create(path1))
              fileStream.Write(buffer1, 0, buffer1.Length);
          }
          plstAttachments.Add(path1);
          byte[] buffer2 = new WebClient().DownloadData(address1);
          if (!System.IO.File.Exists(path2))
          {
            using (FileStream fileStream = System.IO.File.Create(path2))
              fileStream.Write(buffer2, 0, buffer2.Length);
          }
          plstAttachments.Add(path2);
          if (ConfigurationManager.AppSettings["File_Advertising"] != null)
          {
            string path3 = ConfigurationManager.AppSettings["File_Advertising"].ToString().Trim();
            if (System.IO.File.Exists(path3))
              plstAttachments.Add(path3);
          }
          stringBuilder.AppendLine(str12);
          int num = 0;
          List<string> pstrEmailTo = new List<string>();
          if (str11 != null)
          {
            string str15 = str11;
            char[] chArray = new char[1]{ ';' };
            foreach (string str16 in str15.Split(chArray))
            {
              if (!string.IsNullOrEmpty(str16))
              {
                string pattern = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                if (Regex.IsMatch(str16.ToString(), pattern))
                {
                  pstrEmailTo.Add(str16.ToString());
                }
                else
                {
                  SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(1, "El email" + str16.ToString() + "No tiene la estructura correcta"));
                  num = -1;
                }
              }
            }
          }
          string pstrEmailBody = stringBuilder.ToString();
          List<string> pstrEmailCC = new List<string>();
          if (str5 != null)
          {
            string str17 = str5;
            char[] chArray = new char[1]{ '|' };
            foreach (string str18 in str17.Split(chArray))
            {
              if (!string.IsNullOrEmpty(str18))
                pstrEmailCC.Add(str18.ToString());
            }
          }
          if (num != -1)
            flag = Mail.SendEmail(pstrSMTPServer, pintSMTPPort, str4, pstrSMTPPassword, boolean1, str4, pstrEmailTo, pstrEmailCC, (List<string>) null, pstrEmailSubject, pstrEmailBody, boolean2, boolean3, plstAttachments);
          if (!flag)
          {
            if (num == -1)
              SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(1, "El email No tiene la estructura correcta"));
            else
              SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(1, "Error al enviar en Documento Electronico"));
          }
          else
            SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(2, "El Documento Electronico fue enviado correctamente"));
        }
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(-100, ex));
      }
      finally
      {
      }
    }

    protected void sendEmailCUR()
    {
      DataTable dataTable1 = new DataTable();
      try
      {
        DataTable dataTable2 = new DataTable();
        string str1 = "";
        if (this.ViewState["i_Requirementid"] != null)
        {
          using (ReportDocument reportDocument = new ReportDocument())
          {
            int int32 = Convert.ToInt32(this.ViewState["i_Requirementid"], (IFormatProvider) CultureInfo.CurrentCulture);
            string filename = this.Server.MapPath("../Reports/ReportCurDelivery.rpt");
            reportDocument.Load(filename);
            dataTable2 = new RequirementQueriesBL().GenerateDeliveryCUR(int32);
            if (dataTable2 != null)
            {
              if (dataTable2.Rows.Count > 1)
                dataTable2.Rows.RemoveAt(1);
              dataTable2.Columns.Add(new DataColumn()
              {
                ColumnName = "ImageBarPlate",
                DataType = typeof (byte[])
              });
              byte[] numArray1 = this.ImagenBarCode(dataTable2.Rows[0]["v_PlateNew"].ToString());
              dataTable2.Rows[0]["ImageBarPlate"] = (object) numArray1;
              dataTable2.Columns.Add(new DataColumn()
              {
                ColumnName = "Requisite",
                DataType = typeof (string)
              });
              string requisitebyRequirement = new RequirementQueriesBL().GetRequisitebyRequirement(int32, Convert.ToInt32(dataTable2.Rows[0]["i_ProcessTypeId"], (IFormatProvider) CultureInfo.CurrentCulture));
              dataTable2.Rows[0]["Requisite"] = (object) requisitebyRequirement;
              dataTable2.Columns.Add(new DataColumn()
              {
                ColumnName = "ImageBarCode",
                DataType = typeof (byte[])
              });
              byte[] numArray2 = this.ImagenBarCode(dataTable2.Rows[0]["v_PaymentCode"].ToString());
              dataTable2.Rows[0]["ImageBarCode"] = (object) numArray2;
              reportDocument.SetDataSource(dataTable2);
              str1 = Path.GetTempPath() + Constants.ARCHIVO_NOMBRECUR + this.ViewState["i_Requirementid"].ToString() + ".pdf";
              reportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, str1);
            }
            reportDocument.Close();
            ((Component) reportDocument).Dispose();
          }
        }
        Email email = new Email();
        bool flag = false;
        DataTable dataTable3 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) "999",
          (object) "",
          (object) "1",
          (object) "1"
        });
        string str2 = dataTable3.Rows[0]["v_Value"].ToString();
        string str3 = dataTable3.Rows[1]["v_Value"].ToString();
        dataTable3.Rows[3]["v_Value"].ToString();
        string pstrSMTPServer = dataTable3.Rows[4]["v_Value"].ToString();
        int pintSMTPPort = int.Parse(dataTable3.Rows[5]["v_Value"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        string str4 = dataTable3.Rows[6]["v_Value"].ToString();
        string pstrSMTPPassword = dataTable3.Rows[7]["v_Value"].ToString();
        string str5 = dataTable3.Rows[8]["v_Value"].ToString();
        bool boolean1 = Convert.ToBoolean(dataTable3.Rows[9]["v_Value"], (IFormatProvider) CultureInfo.CurrentCulture);
        bool boolean2 = Convert.ToBoolean(dataTable3.Rows[10]["v_Value"], (IFormatProvider) CultureInfo.CurrentCulture);
        bool boolean3 = Convert.ToBoolean(dataTable3.Rows[11]["v_Value"], (IFormatProvider) CultureInfo.CurrentCulture);
        if (dataTable2 == null)
          return;
        dataTable1 = new DataTable();
        string pstrEmailSubject = str2;
        DataTable dataTable4 = dataTable2;
        if (dataTable4.Rows.Count > 0)
        {
          StringBuilder stringBuilder = new StringBuilder();
          string str6 = str3;
          string newValue1 = dataTable4.Rows[0]["v_OwnerCompleteName"].ToString();
          dataTable4.Rows[0]["v_OwnerDocumentType"].ToString();
          string newValue2 = dataTable4.Rows[0]["v_OwnerDocumentDescription"].ToString();
          string newValue3 = dataTable4.Rows[0]["v_OwnerDocumentNumber"].ToString();
          string newValue4 = dataTable4.Rows[0]["v_PlateNew"].ToString();
          string newValue5 = dataTable4.Rows[0]["i_RequirementId"].ToString();
          DateTime dateTime = Convert.ToDateTime(Convert.ToString(dataTable4.Rows[0]["RegisterDate"]) + " " + Convert.ToString(dataTable4.Rows[0]["RegisterTime"]));
          string str7 = this.txtEmail.Text.Trim();
          string str8 = str6.Replace("#CLIENTENOMBRE#", newValue1).Replace("#PLACA#", newValue4).Replace("#SOLICITUD#", newValue5).Replace("#FECHA#", dateTime.ToString("dd-MM-yyyy HH:mm")).Replace("#TIPODOCUMENTO#", newValue2).Replace("#NRODOCUMENTO#", newValue3);
          List<string> plstAttachments = new List<string>();
          string empty = string.Empty;
          if (System.IO.File.Exists(str1))
            plstAttachments.Add(str1);
          stringBuilder.AppendLine(str8);
          int num = 0;
          List<string> pstrEmailTo = new List<string>();
          if (str7 != null)
          {
            string str9 = str7;
            char[] chArray = new char[1]{ ';' };
            foreach (string str10 in str9.Split(chArray))
            {
              if (!string.IsNullOrEmpty(str10))
              {
                string pattern = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                if (Regex.IsMatch(str10.ToString(), pattern))
                {
                  pstrEmailTo.Add(str10.ToString());
                }
                else
                {
                  SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(1, "El email" + str10.ToString() + "No tiene la estructura correcta"));
                  num = -1;
                }
              }
            }
          }
          string pstrEmailBody = stringBuilder.ToString();
          List<string> pstrEmailCC = new List<string>();
          if (str5 != null)
          {
            string str11 = str5;
            char[] chArray = new char[1]{ '|' };
            foreach (string str12 in str11.Split(chArray))
            {
              if (!string.IsNullOrEmpty(str12))
                pstrEmailCC.Add(str12.ToString());
            }
          }
          if (num != -1)
            flag = Mail.SendEmail(pstrSMTPServer, pintSMTPPort, str4, pstrSMTPPassword, boolean1, str4, pstrEmailTo, pstrEmailCC, (List<string>) null, pstrEmailSubject, pstrEmailBody, boolean2, boolean3, plstAttachments);
          if (!flag)
          {
            if (num == -1)
              SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(1, "El email No tiene la estructura correcta"));
            else
              SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(1, "Error al enviar en Documento Electronico"));
          }
          else
            SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(2, "El Documento Electronico fue enviado correctamente"));
        }
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMensaje, new HandledException(-100, ex));
      }
      finally
      {
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

    protected void volver_Click(object sender, EventArgs e)
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
