// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Claims.ClaimBook.ClaimBookPublic
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIIV.BE;
using SIIV.Claim.BL;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Transactions;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Claims.ClaimBook
{
  public class ClaimBookPublic : Page
  {
    protected UpdatePanel UpdatePanel1;
    protected Label lblNReclamo2;
    protected TextBox txtClaimCode;
    protected Label lblFechaRegistro;
    protected Fecha wdpRegisterDate;
    protected Label Label1;
    protected DropDownList wddLocation;
    protected Label Label26;
    protected TextBox txtAddressLocation;
    protected HtmlGenericControl Datos;
    protected Label Label3;
    protected TextBox txtPlate;
    protected FilteredTextBoxExtender txtPlate_FilteredTextBoxExtender;
    protected RequiredFieldValidator ValidatorPlate;
    protected ValidatorCalloutExtender ValidatorPlate_ValidatorCalloutExtender;
    protected Label Label4;
    protected TextBox txtName;
    protected RequiredFieldValidator RequiredFieldValidator1;
    protected ValidatorCalloutExtender RequiredFieldValidator1_ValidatorCalloutExtender;
    protected Label Label5;
    protected TextBox txtLastName;
    protected RequiredFieldValidator RequiredFieldValidator2;
    protected ValidatorCalloutExtender RequiredFieldValidator2_ValidatorCalloutExtender;
    protected Label Label6;
    protected TextBox txtAddress;
    protected RequiredFieldValidator RequiredFieldValidator3;
    protected ValidatorCalloutExtender RequiredFieldValidator3_ValidatorCalloutExtender;
    protected Label Label7;
    protected DropDownList wddDocumentType;
    protected Label Label8;
    protected TextBox txtDocumentNumber;
    protected FilteredTextBoxExtender txtDocumentNumber_FilteredTextBoxExtender;
    protected RequiredFieldValidator ValidatorDocumento;
    protected ValidatorCalloutExtender ValidatorDocumento_ValidatorCalloutExtender;
    protected Label Label9;
    protected TextBox txtTelephoneNumber;
    protected FilteredTextBoxExtender txtTelephoneNumber_FilteredTextBoxExtender;
    protected Label Label10;
    protected TextBox txtEmail;
    protected RegularExpressionValidator RegularExpressionValidator1;
    protected ValidatorCalloutExtender RegularExpressionValidator1_ValidatorCalloutExtender;
    protected CheckBox chkSendEmail;
    protected CheckBox chkMinority;
    protected Panel pApod;
    protected Label Label11;
    protected TextBox txtAttorneyName;
    protected Label Label12;
    protected TextBox txtAttorneyAddress;
    protected Label Label13;
    protected TextBox txtAttorneyPhoneNumber;
    protected FilteredTextBoxExtender txtAttorneyPhoneNumber_FilteredTextBoxExtender;
    protected Label Label2;
    protected TextBox txtAttorneyEmail;
    protected RegularExpressionValidator RegularExpressionValidator2;
    protected ValidatorCalloutExtender RegularExpressionValidator2_ValidatorCalloutExtender;
    protected RadioButton rbService;
    protected RadioButton rbProduct;
    protected Label Label15;
    protected TextBox txtDescription;
    protected RadioButton rbClaim;
    protected RadioButton rbComplaint;
    protected Label Label16;
    protected TextBox txtComments;
    protected RegularExpressionValidator valcoments;
    protected ValidatorCalloutExtender ValidatorCalloutExtender1;
    protected Label lblNumChar;
    protected Label Label17;
    protected FileUpload FileUpload1;
    protected Label Label18;
    protected Label Label19;
    protected Label Label20;
    protected Label Label21;
    protected CheckBox chkAccept;
    protected HtmlTableCell loading;
    protected HtmlTableCell btnHidden;
    protected Button wibRegister;
    protected HtmlTableCell btnCapcha;
    protected Button wibPrint;
    protected Button wibInicio;
    protected Label lblMessageClaimBook;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.LoadParameters();
      (this.Master.FindControl("lblSubTitle") as Label).Text = "Libro Reclamaciones";
      this.NuevoRegistro();
      this.wddLocation.SelectedValue = "14";
      this.wddLocation_SelectionChanged((object) null, (EventArgs) null);
      this.txtPlate.Focus();
    }

    protected void wddLocation_SelectionChanged(object sender, EventArgs e)
    {
      DataTable dataTable = (DataTable) this.ViewState["dtLocation"];
      if (dataTable == null)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (row["i_LocationId"].ToString() == this.wddLocation.SelectedValue)
          this.txtAddressLocation.Text = row["v_Address"].ToString();
      }
    }

    protected void wddDocumentType_SelectionChanged(object sender, EventArgs e)
    {
      if (this.wddDocumentType.SelectedValue == "1")
      {
        this.txtDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Numbers;
        this.txtDocumentNumber.MaxLength = 8;
      }
      else if (this.wddDocumentType.SelectedValue == "4")
      {
        this.txtDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Numbers;
        this.txtDocumentNumber.MaxLength = 11;
      }
      else
      {
        this.txtDocumentNumber_FilteredTextBoxExtender.FilterType = FilterTypes.Custom;
        this.txtDocumentNumber_FilteredTextBoxExtender.ValidChars = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz1234567890";
        this.txtDocumentNumber.MaxLength = 20;
      }
    }

    protected void chkMinority_CheckedChanged(object sender, EventArgs e)
    {
      this.pApod.Visible = this.chkMinority.Checked;
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
      if (!this.ValidateParameters())
        return;
      using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
      {
        Timeout = new TimeSpan(1, 1, 1)
      }))
      {
        try
        {
          string pstrclaimcode = new RequirementClaimManagementBL().InsertClaimBook(this.GetObject());
          if (pstrclaimcode.Length > 0)
          {
            if (this.txtEmail.Text.Length > 0)
            {
              string str = string.Empty;
              string empty = string.Empty;
              if (this.FileUpload1.HasFile)
              {
                string extension = Path.GetExtension(this.FileUpload1.FileName);
                if (extension == ".docx" || extension == ".doc" || extension == ".pdf" || extension == ".jpg" || extension == ".jpeg")
                {
                  if (this.FileUpload1.PostedFile.ContentLength > 512000)
                  {
                    this.HidePopup();
                    SIIV.Common.Resource.Message.SetMessage(this.lblMessageClaimBook, enmMessageType.Warning, "El tamaño del archivo no debe exceder los 500MB");
                    return;
                  }
                  str = Path.Combine(this.Server.MapPath("../ClaimBook/Image"), pstrclaimcode + extension);
                  this.FileUpload1.SaveAs(str);
                }
                else
                {
                  this.HidePopup();
                  SIIV.Common.Resource.Message.SetMessage(this.lblMessageClaimBook, enmMessageType.Warning, "El archivo adjunto debe tener extención de tipo(.docx .doc .jpg .jpeg .pdf)");
                  return;
                }
              }
              if (this.SendEmail(pstrclaimcode, this.wdpRegisterDate.Value.ToString(), this.txtEmail.Text, str))
                SIIV.Common.Resource.Message.SetMessage(this.lblMessageClaimBook, enmMessageType.Warning, "Se encontró un problema en el envío de confirmación al correo indicado");
            }
            SIIV.Common.Resource.Message.SetMessage(this.lblMessageClaimBook, enmMessageType.Success, "El Reclamo se registro satisfactoriamente, CODIGO RECLAMO : " + pstrclaimcode);
            this.txtClaimCode.Text = pstrclaimcode;
            this.btnHidden.Style.Add("visibility", "hidden");
            this.btnCapcha.Style.Add("visibility", "visible");
            this.btnCapcha.Style.Add("display", "inline-flex");
            this.wibPrint.Enabled = true;
            this.SetearPrint();
            this.ClearControls();
          }
          else
          {
            SIIV.Common.Resource.Message.SetMessage(this.lblMessageClaimBook, enmMessageType.Warning, "Se encontró un problema en el registro del reclamo");
            this.wibPrint.Enabled = false;
          }
          transactionScope.Complete();
          this.Datos.Visible = false;
          this.btnHidden.Style.Add("visibility", "visible");
          this.btnCapcha.Style.Add("visibility", "hidden");
          this.btnCapcha.Style.Add("display", "none");
          this.wibRegister.Enabled = false;
          this.wibPrint.Enabled = true;
        }
        catch (Exception ex)
        {
          SIIV.Common.Resource.Message.SetMessage(this.lblMessageClaimBook, enmMessageType.Warning, ex.Message);
        }
      }
    }

    private bool ValidateCapcha()
    {
      string str = this.Request["g-recaptcha-response"].Replace(",", "");
      string appSetting = WebConfigurationManager.AppSettings["recaptchaPrivatekey"];
      bool flag = false;
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", (object) appSetting, (object) str));
      try
      {
        using (WebResponse response = httpWebRequest.GetResponse())
        {
          using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            flag = Convert.ToBoolean(new JavaScriptSerializer().Deserialize<ClaimBookPublic.MyObject>(streamReader.ReadToEnd()).success);
        }
        return flag;
      }
      catch (WebException ex)
      {
        throw ex;
      }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
      try
      {
        if (!(this.ViewState["dtPrint"] is DataTable dt))
          return;
        this.SetCrystalReport(dt);
      }
      catch (HandledException ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessageClaimBook, ex);
      }
      catch (Exception ex)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessageClaimBook, new HandledException(-100, ex));
      }
    }

    protected void wibInicio_Click(object sender, EventArgs e)
    {
      this.Response.Redirect("~/index.aspx");
    }

    private void LoadParameters()
    {
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.PersonDocumentType.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      this.wddDocumentType.Items.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (Convert.ToInt32(row["i_GroupId"], (IFormatProvider) CultureInfo.CurrentCulture) == SystemParameterGroups.PersonDocumentType)
          this.wddDocumentType.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Value"].ToString(), row["i_ParameterId"].ToString()));
      }
      this.wddDocumentType.Items.Insert(0, new System.Web.UI.WebControls.ListItem(SIIV.SystemParameter.BL.Constants.OPCIONLISTA_Seleccione, SIIV.SystemParameter.BL.Constants.OPCIONLISTA_ValorSinSeleccion));
      this.ViewState["dtLocation"] = (object) new SIIV.Warehouse.BL.LocationQueriesBL().GetLocationUserBy(2);
      this.wddLocation.DataSource = (object) (DataTable) this.ViewState["dtLocation"];
      this.wddLocation.DataTextField = "v_Description";
      this.wddLocation.DataValueField = "i_LocationId";
      this.wddLocation.DataBind();
      this.wddLocation.Items.Insert(0, new System.Web.UI.WebControls.ListItem(SIIV.SystemParameter.BL.Constants.OPCIONLISTA_Seleccione, SIIV.SystemParameter.BL.Constants.OPCIONLISTA_ValorSinSeleccion));
      this.wdpRegisterDate.Value = DateTime.Now;
    }

    private void NuevoRegistro()
    {
      this.btnHidden.Style.Add("visibility", "hidden");
      this.btnCapcha.Style.Add("visibility", "visible");
      this.btnCapcha.Style.Add("display", "inline-flex");
      this.wibRegister.Enabled = true;
      this.wibPrint.Enabled = false;
      this.ClearControls();
      this.EnabledControls(true);
      this.lblMessageClaimBook.Visible = false;
    }

    private void ClearControls()
    {
      this.wdpRegisterDate.Value = DateTime.Now;
      this.txtName.Text = "";
      this.txtLastName.Text = "";
      this.txtAddress.Text = "";
      this.txtDocumentNumber.Text = "";
      this.txtTelephoneNumber.Text = "";
      this.txtEmail.Text = "";
      this.chkSendEmail.Checked = true;
      if (this.chkMinority.Checked)
      {
        this.txtAttorneyName.Text = "";
        this.txtAttorneyAddress.Text = "";
        this.txtAttorneyPhoneNumber.Text = "";
        this.txtAttorneyEmail.Text = "";
      }
      this.chkMinority.Checked = false;
      this.rbService.Checked = false;
      this.rbProduct.Checked = false;
      this.txtDescription.Text = "";
      this.rbClaim.Checked = false;
      this.rbComplaint.Checked = false;
      this.txtComments.Text = "";
      this.chkAccept.Checked = false;
    }

    private void EnabledControls(bool enabled)
    {
      this.txtName.Enabled = enabled;
      this.txtLastName.Enabled = enabled;
      this.txtAddress.Enabled = enabled;
      this.wddDocumentType.Enabled = enabled;
      this.txtDocumentNumber.Enabled = enabled;
      this.txtTelephoneNumber.Enabled = enabled;
      this.txtEmail.Enabled = enabled;
      this.chkMinority.Enabled = enabled;
      this.txtAttorneyName.Enabled = enabled;
      this.txtAttorneyAddress.Enabled = enabled;
      this.txtAttorneyPhoneNumber.Enabled = enabled;
      this.txtAttorneyEmail.Enabled = enabled;
      this.rbService.Enabled = enabled;
      this.rbProduct.Enabled = enabled;
      this.txtDescription.Enabled = enabled;
      this.rbClaim.Enabled = enabled;
      this.rbComplaint.Enabled = enabled;
      this.txtComments.Enabled = enabled;
      this.FileUpload1.Enabled = enabled;
      this.chkAccept.Enabled = enabled;
    }

    private bool ValidateParameters()
    {
      string pstrMessage = "";
      if (this.txtPlate.Text.Length < 6)
      {
        this.ValidatorPlate.IsValid = false;
        this.ValidatorPlate.ErrorMessage = "Debe ingresar un número de placa válido";
      }
      if (this.wddLocation.SelectedValue == "")
        pstrMessage = "Debe seleccionar el establecimiento a Reclamar";
      else if (this.wddDocumentType.SelectedValue == "-1")
      {
        pstrMessage = "Debe ingresar el Tipo Documento del cliente";
      }
      else
      {
        if (this.wddDocumentType.SelectedValue == "1" && (this.txtDocumentNumber.Text.Length < 8 || this.txtDocumentNumber.Text.Length > 8))
        {
          this.ValidatorDocumento.IsValid = false;
          this.ValidatorDocumento.ErrorMessage = "Ingrese su número de documento válido.";
          return false;
        }
        if (this.wddDocumentType.SelectedValue == "2" && this.txtDocumentNumber.Text.Length < 6)
        {
          this.ValidatorDocumento.IsValid = false;
          this.ValidatorDocumento.ErrorMessage = "Ingrese un número de Pasaporte Válido.";
          return false;
        }
        if (this.wddDocumentType.SelectedValue == "3" && this.txtDocumentNumber.Text.Length < 8)
        {
          this.ValidatorDocumento.IsValid = false;
          this.ValidatorDocumento.ErrorMessage = "Ingrese un número de Carnet de Extranjeria Válido.";
          return false;
        }
        if (this.wddDocumentType.SelectedValue == "4")
        {
          if (this.txtDocumentNumber.Text.Length < 10 || !Format.ValidateRUCstructure(this.txtDocumentNumber.Text))
          {
            this.ValidatorDocumento.IsValid = false;
            this.ValidatorDocumento.ErrorMessage = "Ingrese un número de RUC Válido.";
            return false;
          }
        }
        else if (this.chkSendEmail.Checked && this.txtEmail.Text.Length == 0)
          pstrMessage = "Debe ingresar el E-mail del cliente";
        else if (this.chkMinority.Checked)
        {
          if (this.txtAttorneyName.Text.Length == 0)
            pstrMessage = "Debe ingresar el nombre del Apoderado";
          else if (this.txtAttorneyAddress.Text.Length == 0)
            pstrMessage = "Debe ingresar la dirección del Apoderado";
          else if (this.txtAttorneyPhoneNumber.Text.Length == 0)
            pstrMessage = "Debe ingresar el telefono del Apoderado";
          else if (this.txtAttorneyEmail.Text.Length == 0)
            pstrMessage = "Debe ingresar el E-mail del Apoderado";
        }
        else if (!this.rbService.Checked && !this.rbProduct.Checked)
          pstrMessage = "Debe especificar si el Reclamo/Queja se refiere a un Producto o Servicio";
        else if (this.txtDescription.Text.Length == 0)
          pstrMessage = "Debe indicar el detalle del Producto o Servicio";
        else if (!this.rbClaim.Checked && !this.rbComplaint.Checked)
        {
          pstrMessage = "Debe indicar si es un Reclamo o una Queja";
        }
        else
        {
          if (this.txtComments.Text.Length < 30)
          {
            this.valcoments.IsValid = false;
            this.valcoments.ErrorMessage = "Debe ingresar un comentario no mínimo de 30 caracteres";
            return false;
          }
          if (!this.chkAccept.Checked)
            pstrMessage = "Debe marcar la selección de Conformidad de los hechos descritos en el reclamo";
        }
      }
      if (!this.ValidateCapcha() && pstrMessage.Length == 0)
        pstrMessage = "Captcha ha fallado!! Por favor Intente nuevamente.";
      if (pstrMessage.Length > 0)
      {
        SIIV.Common.Resource.Message.SetMessage(this.lblMessageClaimBook, enmMessageType.Warning, pstrMessage);
        return false;
      }
      this.lblMessageClaimBook.Visible = false;
      return true;
    }

    private void SetearPrint()
    {
      DataTable allClaimBookPag = new RequirementClaimQueriesBL().GetAllClaimBookPag(0, 0, 0, 8, 0, 0, 0, -1, this.txtClaimCode.Text, new DateTime?(this.wdpRegisterDate.Value), new DateTime?(this.wdpRegisterDate.Value.AddDays(1.0)), 1, 10, out int _);
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("DateRegister", Type.GetType("System.DateTime"));
      dataTable.Columns.Add("LocationName", Type.GetType("System.String"));
      dataTable.Columns.Add("ClaimCode", Type.GetType("System.String"));
      dataTable.Columns.Add("CustomerName", Type.GetType("System.String"));
      dataTable.Columns.Add("CustomerLastName", Type.GetType("System.String"));
      dataTable.Columns.Add("CustomerAddress", Type.GetType("System.String"));
      dataTable.Columns.Add("CustomerDocumentType", Type.GetType("System.String"));
      dataTable.Columns.Add("CustomerDocumentNumber", Type.GetType("System.String"));
      dataTable.Columns.Add("CustomerTelephoneNumber", Type.GetType("System.String"));
      dataTable.Columns.Add("CustomerEmail", Type.GetType("System.String"));
      dataTable.Columns.Add("IsMinor", Type.GetType("System.Int32"));
      dataTable.Columns.Add("AttorneyName", Type.GetType("System.String"));
      dataTable.Columns.Add("AttorneyAddress", Type.GetType("System.String"));
      dataTable.Columns.Add("AttorneyTelephoneNumber", Type.GetType("System.String"));
      dataTable.Columns.Add("AttorneyEmail", Type.GetType("System.String"));
      dataTable.Columns.Add("IsService", Type.GetType("System.Int32"));
      dataTable.Columns.Add("IsProduct", Type.GetType("System.Int32"));
      dataTable.Columns.Add("Description", Type.GetType("System.String"));
      dataTable.Columns.Add("IsClaim", Type.GetType("System.Int32"));
      dataTable.Columns.Add("IsComplaint", Type.GetType("System.Int32"));
      dataTable.Columns.Add("ClaimDescription", Type.GetType("System.String"));
      dataTable.Columns.Add("IsAccept", Type.GetType("System.Int32"));
      DataRow row = dataTable.NewRow();
      row["DateRegister"] = (object) allClaimBookPag.Rows[0]["v_ClaimDate"].ToString();
      row["LocationName"] = (object) this.wddLocation.SelectedItem.ToString();
      row["ClaimCode"] = (object) this.txtClaimCode.Text;
      row["CustomerName"] = (object) this.txtName.Text;
      row["CustomerLastName"] = (object) this.txtLastName.Text;
      row["CustomerAddress"] = (object) this.txtAddress.Text;
      row["CustomerDocumentType"] = (object) this.wddDocumentType.SelectedItem.ToString();
      row["CustomerDocumentNumber"] = (object) this.txtDocumentNumber.Text;
      row["CustomerTelephoneNumber"] = (object) this.txtTelephoneNumber.Text;
      row["CustomerEmail"] = (object) this.txtEmail.Text;
      row["IsMinor"] = this.chkMinority.Checked ? (object) "1" : (object) "0";
      row["AttorneyName"] = (object) this.txtAttorneyName.Text;
      row["AttorneyAddress"] = (object) this.txtAttorneyAddress.Text;
      row["AttorneyTelephoneNumber"] = (object) this.txtAttorneyPhoneNumber.Text;
      row["AttorneyEmail"] = (object) this.txtAttorneyEmail.Text;
      row["IsService"] = this.rbService.Checked ? (object) "1" : (object) "0";
      row["IsProduct"] = this.rbProduct.Checked ? (object) "1" : (object) "0";
      row["Description"] = (object) this.txtDescription.Text;
      row["IsClaim"] = this.rbClaim.Checked ? (object) "1" : (object) "0";
      row["IsComplaint"] = this.rbComplaint.Checked ? (object) "1" : (object) "0";
      row["ClaimDescription"] = (object) this.txtComments.Text;
      row["IsAccept"] = this.chkAccept.Checked ? (object) "1" : (object) "0";
      dataTable.Rows.Add(row);
      this.ViewState["dtPrint"] = (object) dataTable;
    }

    private RequirementClaim GetObject()
    {
      return new RequirementClaim()
      {
        i_RequirementClaimId = 0,
        i_ClaimTypeId = new int?(8),
        v_ClaimDate = this.wdpRegisterDate.Value.ToString(),
        i_Status = new int?(1),
        i_Priority = new int?(0),
        v_Requester = this.GetDataCustomers(),
        i_LocationId = new int?(Convert.ToInt32(this.wddLocation.SelectedValue, (IFormatProvider) CultureInfo.CurrentCulture)),
        v_RequestValues = this.GetDataClaim(),
        i_AssignedUserId = new int?(0),
        d_AssignedDate = new DateTime?(Convert.ToDateTime((object) this.wdpRegisterDate.Value, (IFormatProvider) CultureInfo.CurrentCulture)),
        v_Comments = ""
      };
    }

    public string GetDataCustomers()
    {
      return "" + this.txtName.Text + "|" + this.txtLastName.Text + "|" + this.txtAddress.Text + "|" + this.wddDocumentType.SelectedValue + "|" + this.txtDocumentNumber.Text + "|" + this.txtTelephoneNumber.Text + "|" + this.txtEmail.Text + "|" + (this.chkMinority.Checked ? "1" : "0") + "|" + this.txtAttorneyName.Text + "|" + this.txtAttorneyAddress.Text + "|" + this.txtAttorneyPhoneNumber.Text + "|" + this.txtAttorneyEmail.Text;
    }

    public string GetDataClaim()
    {
      return "" + (this.rbService.Checked ? "1" : "2") + "|" + this.txtDescription.Text + "|" + (this.rbClaim.Checked ? "1" : "2") + "|" + this.txtPlate.Text + " - " + this.txtComments.Text + "|" + (this.chkAccept.Checked ? "1" : "0");
    }

    private void SetCrystalReport(DataTable dt)
    {
      ReportDocument reportDocument = new ReportDocument();
      string filename = this.Server.MapPath("../ClaimBook/RptClaimBook.rpt");
      reportDocument.Load(filename);
      reportDocument.SetDataSource(dt);
      reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "LibroReclamacion");
      ((Component) reportDocument).Dispose();
    }

    public bool SendEmail(
      string pstrclaimcode,
      string pstrregisterdate,
      string pstruseremail,
      string sFile)
    {
      Email email = new Email();
      bool flag = false;
      DataTable dataTable1 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.SMTPServerConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string pstrSMTPServer = dataTable1.Rows[0]["v_Value"].ToString();
      int pintSMTPPort = int.Parse(dataTable1.Rows[1]["v_Value"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
      string pstrSMTPUserName = dataTable1.Rows[2]["v_Value"].ToString();
      string pstrSMTPPassword = dataTable1.Rows[3]["v_Value"].ToString();
      bool pbEnabledSSL = bool.Parse(dataTable1.Rows[4]["v_Value"].ToString());
      DataTable dataTable2 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.ClaimCustomEmailConfiguration.ToString((IFormatProvider) CultureInfo.CurrentCulture),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string pstrEmailSubject = dataTable2.Rows[0]["v_Value"].ToString();
      string pstrEmailBody = string.Format(dataTable2.Rows[1]["v_Value"].ToString(), (object) pstrclaimcode, (object) pstrregisterdate);
      string pstrEmailFrom = dataTable2.Rows[2]["v_Value"].ToString();
      string pstrEmailTo = ConfigurationManager.AppSettings["ClaimBook_EmailCopy"].ToString((IFormatProvider) CultureInfo.CurrentCulture);
      flag = Email.SendEmail(pstrSMTPUserName, pstrSMTPPassword, pstrEmailSubject, pstrEmailBody, pstruseremail, pstrSMTPServer, pintSMTPPort, pstrEmailFrom, pbEnabledSSL, sFile);
      return Email.SendEmail(pstrSMTPUserName, pstrSMTPPassword, pstrEmailSubject, pstrEmailBody, pstrEmailTo, pstrSMTPServer, pintSMTPPort, pstrEmailFrom, pbEnabledSSL, sFile);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    public class MyObject
    {
      public string success { get; set; }
    }

    public class ReCaptchav3Response
    {
      public bool success { get; set; }

      public DateTime challenge_ts { get; set; }

      public string hostname { get; set; }

      public float score { get; set; }
    }
  }
}
