// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibicion.Operation.EmailNotifications
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using Saplin.Controls;
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
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

#nullable disable
namespace SIIV.WebApp.Exhibicion.Operation
{
  public class EmailNotifications : Page
  {
    private AssociatedManagementBL pobjAssociatedManagementBL;
    private DataTable T = new DataTable()
    {
      Columns = {
        "Id",
        "Name",
        "Email"
      }
    };
    protected UpdatePanel updatePanel;
    protected DropDownCheckBoxes cboAsociated;
    protected TextBox txtTitle;
    protected Label Label17;
    protected FileUpload FileUpload1;
    protected TextBox txtMessage;
    protected Label lblMessage;
    protected Button wibAccept;
    protected Button wibCancel;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        return;
      try
      {
        this.ViewState["i_plateTypeId"] = (object) this.Request.QueryString["t"].ToString();
        this.wibCancel.Visible = false;
        this.LoadParameters();
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

    protected void wibAccept_Click(object sender, EventArgs e)
    {
      try
      {
        this.pobjAssociatedManagementBL = new AssociatedManagementBL();
        if (this.txtTitle.Text == "")
          throw new HandledException(1, "Debe ingresar un Asunto");
        if (this.txtMessage.Text == "")
          throw new HandledException(1, "Debe ingresar un Mensaje");
        this.wibAccept.Visible = false;
        this.wibCancel.Visible = true;
        DataTable dataTable = new DataTable();
        dataTable = (DataTable) this.ViewState["dt_Result"];
        string str1 = string.Empty;
        string empty = string.Empty;
        if (this.FileUpload1.HasFile)
        {
          string extension = Path.GetExtension(this.FileUpload1.FileName);
          if (!(extension == ".docx") && !(extension == ".doc") && !(extension == ".pdf") && !(extension == ".jpg") && !(extension == ".jpeg"))
            throw new HandledException(1, "El archivo adjunto debe tener extención de tipo(.docx .doc .jpg .jpeg .pdf)");
          if (this.FileUpload1.PostedFile.ContentLength > 1048576)
            throw new HandledException(1, "El tamaño del archivo no debe exceder el 1MB");
          str1 = Path.Combine(this.Server.MapPath("../Email/Docs"), this.FileUpload1.FileName);
          this.FileUpload1.SaveAs(str1);
        }
        string str2 = "";
        this.T.Rows.Clear();
        foreach (System.Web.UI.WebControls.ListItem listItem in ((ListControl) this.cboAsociated).Items)
        {
          if (listItem.Selected)
            this.T.Rows.Add((object) 1, (object) listItem.Text, (object) listItem.Value);
        }
        List<string> AssociatedMail;
        List<string> AssociatedMailError;
        this.EmailValidated(this.T, out AssociatedMail, out AssociatedMailError);
        if (AssociatedMail.Count > 0)
        {
          string postrMessage = "";
          if (this.SendEMail(AssociatedMail, ref postrMessage, str1))
          {
            str2 = "Correo(s) Enviado(s) Correctamente: (" + AssociatedMail.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ")<br>";
            if (AssociatedMailError.Count > 0)
            {
              str2 = str2 + "Emails(s) no tienen formato Correcto y No Enviados: (" + AssociatedMailError.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ")<br>";
              foreach (string str3 in AssociatedMailError)
                str2 = str2 + str3.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "<br>";
            }
          }
          else
            str2 = "Error en el envio DE Emails(s)";
        }
        else if (AssociatedMailError.Count > 0)
        {
          str2 = str2 + "Emails(s) no tienen formato Correcto y No Enviados: (" + AssociatedMailError.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ")<br>";
          foreach (string str4 in AssociatedMailError)
            str2 = str2 + str4.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "<br>";
        }
        string ErrorMessage = str2 + this.HistoryEmailInsert(this.txtTitle.Text.Trim(), this.txtMessage.Text.Trim(), AssociatedMail, AssociatedMailError);
        this.ClearControls();
        throw new HandledException(1, ErrorMessage);
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

    protected void wibCancel_Click(object sender, EventArgs e)
    {
      try
      {
        this.lblMessage.Text = string.Empty;
        this.ClearControls();
        this.wibAccept.Visible = true;
        this.wibCancel.Visible = false;
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

    protected void checkBoxes_SelcetedIndexChanged(object sender, EventArgs e)
    {
      int num = 0;
      DropDownCheckBoxes dropDownCheckBoxes = (DropDownCheckBoxes) sender;
      foreach (System.Web.UI.WebControls.ListItem listItem in ((ListControl) dropDownCheckBoxes).Items)
      {
        if (listItem.Selected)
          ++num;
      }
      dropDownCheckBoxes.Texts.SelectBoxCaption = num > 0 ? (num == ((ListControl) dropDownCheckBoxes).Items.Count ? "Todos" : "Selección Múltiple") : "Seleccione";
    }

    protected string checkBoxesSelected(DropDownCheckBoxes cboCheck, string Listname)
    {
      XElement xelement = new XElement((XName) Listname, (object) new XElement((XName) "Items"));
      int num = 0;
      foreach (System.Web.UI.WebControls.ListItem listItem in ((ListControl) cboCheck).Items)
      {
        if (listItem.Selected)
        {
          ++num;
          XElement content = new XElement((XName) "Item", new object[2]
          {
            (object) new XElement((XName) "Id", (object) listItem.Value),
            (object) new XElement((XName) "Value", (object) listItem.Text)
          });
          xelement.Element((XName) "Items").Add((object) content);
        }
      }
      return num > 0 ? "<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>\n" + xelement.ToString() : "";
    }

    public void LoadParameters()
    {
      try
      {
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - EmailNotifications.aspx");
        AssociatedQueriesBL associatedQueriesBl = new AssociatedQueriesBL();
        DataTable dataTable1 = new DataTable();
        string i_plateTypeId = this.ViewState["i_plateTypeId"].ToString();
        DataTable dataTable2 = associatedQueriesBl.ExhibitionAssociatedListEmail(i_plateTypeId).AsEnumerable().OrderBy<DataRow, object>((System.Func<DataRow, object>) (x => x["name"])).CopyToDataTable<DataRow>();
        ((BaseDataBoundControl) this.cboAsociated).DataSource = (object) dataTable2;
        ((Control) this.cboAsociated).DataBind();
        this.ViewState["dt_Result"] = (object) dataTable2;
      }
      catch (Exception ex)
      {
        throw ex;
      }
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

    public bool SendEMail(List<string> EmailTo, ref string postrMessage, string sFile)
    {
      try
      {
        Email email = new Email();
        bool flag = false;
        DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.SMTPServerConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture),
          (object) "",
          (object) "1",
          (object) "1"
        });
        string pstrSMTPServer = dataTable.Rows[0]["v_Value"].ToString();
        int pintSMTPPort = int.Parse(dataTable.Rows[1]["v_Value"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrSMTPUserName = dataTable.Rows[2]["v_Value"].ToString();
        string pstrSMTPPassword = dataTable.Rows[3]["v_Value"].ToString();
        bool boolean = Convert.ToBoolean(dataTable.Rows[4]["v_Value"], (IFormatProvider) CultureInfo.CurrentCulture);
        string pstrEmailFrom = "";
        string text = this.txtTitle.Text;
        string str1 = this.txtMessage.Text.Replace("\n", "<br>");
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("<html><body>");
        stringBuilder.AppendLine(str1);
        stringBuilder.AppendLine("</body></html>");
        string pstrEmailBody = stringBuilder.ToString();
        List<string> pstrEmailTo = new List<string>();
        List<string> pstrEmailCC = new List<string>();
        if (Convert.ToString(this.ViewState["i_plateTypeId"]) == "7")
        {
          pstrEmailFrom = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
          {
            (object) SystemParameterGroups.ConfigurationExhibition.ToString((IFormatProvider) CultureInfo.CurrentCulture),
            (object) "1",
            (object) "1",
            (object) "1"
          }).Rows[0]["v_Value"].ToString();
          if (ConfigurationManager.AppSettings["EmailCopy_Exhibition"] != null)
          {
            string str2 = ConfigurationManager.AppSettings["EmailCopy_Exhibition"].ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim();
            char[] chArray = new char[1]{ '|' };
            foreach (string str3 in str2.Split(chArray))
            {
              if (!string.IsNullOrEmpty(str3))
                pstrEmailCC.Add(str3.ToString((IFormatProvider) CultureInfo.CurrentCulture));
            }
          }
        }
        else if (Convert.ToString(this.ViewState["i_plateTypeId"]) == "11")
        {
          pstrEmailFrom = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
          {
            (object) SystemParameterGroups.ConfigurationRotate.ToString((IFormatProvider) CultureInfo.CurrentCulture),
            (object) "1",
            (object) "1",
            (object) "1"
          }).Rows[0]["v_Value"].ToString();
          if (ConfigurationManager.AppSettings["EmailCopy_Rotate"] != null)
          {
            string str4 = ConfigurationManager.AppSettings["EmailCopy_Rotate"].ToString((IFormatProvider) CultureInfo.CurrentCulture).Trim();
            char[] chArray = new char[1]{ '|' };
            foreach (string str5 in str4.Split(chArray))
            {
              if (!string.IsNullOrEmpty(str5))
                pstrEmailCC.Add(str5.ToString((IFormatProvider) CultureInfo.CurrentCulture));
            }
          }
        }
        Decimal num = Math.Ceiling(Convert.ToDecimal(EmailTo.Count) / (Decimal) EmailTo.Count);
        for (int index1 = 0; (Decimal) index1 < num; ++index1)
        {
          List<string> pstrEmailCCo = new List<string>();
          for (int index2 = 0; index2 < 25; ++index2)
          {
            int index3 = index1 * 25 + index2;
            if (index3 < EmailTo.Count)
              pstrEmailCCo.Add(EmailTo[index3]);
          }
          flag = Email.SendEmail(pstrSMTPUserName, pstrSMTPPassword, text, pstrEmailBody, pstrEmailTo, pstrEmailCC, pstrSMTPServer, pintSMTPPort, pstrEmailFrom, boolean, pstrEmailCCo, ref postrMessage, sFile);
        }
        return flag;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string HistoryEmailInsert(
      string v_Title,
      string v_Message,
      List<string> v_EmailCorrect,
      List<string> v_EmailFail)
    {
      try
      {
        AssociatedQueriesBL associatedQueriesBl = new AssociatedQueriesBL();
        SystemUser systemUser = (SystemUser) this.Session["SystemUser"];
        string v_EmailCorrect1 = string.Empty;
        string v_EmailFail1 = string.Empty;
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string v_From = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.ConfigurationExhibition.ToString((IFormatProvider) CultureInfo.CurrentCulture),
          (object) "",
          (object) "1",
          (object) "1"
        }).Rows[0]["v_Value"].ToString();
        foreach (string str in v_EmailCorrect)
          v_EmailCorrect1 = v_EmailCorrect1 + str.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ";";
        foreach (string str in v_EmailFail)
          v_EmailFail1 = v_EmailFail1 + str.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ";";
        string v_To = v_EmailCorrect1 + ";" + v_EmailFail1;
        string v_Result = "Enviados OK (" + v_EmailCorrect.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture) + "); No Enviados (" + v_EmailFail.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ")";
        this.pobjAssociatedManagementBL.SpecialPlateHistoryMailInsert(v_From, v_To, v_Title, v_Message, v_Result, v_EmailCorrect1, v_EmailFail1, systemUser.i_SystemUserId);
        return v_Result;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void ClearControls()
    {
      foreach (System.Web.UI.WebControls.ListItem listItem in ((ListControl) this.cboAsociated).Items)
        listItem.Selected = false;
      this.txtTitle.Text = string.Empty;
      this.txtMessage.Text = string.Empty;
    }
  }
}
