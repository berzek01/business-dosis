// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Operation.RefundMovementVehicle
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.Exhibition.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Operation
{
  public class RefundMovementVehicle : Page
  {
    private int i_PlateTypeId;
    private string strEventLogFile = ConfigDA.ReadConfig("EventLogFile") + "_SpecialPlate_" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
    public static string PlateNew = "";
    public static string CorrelativeCode = "";
    protected UpdatePanel UpdatePanel1;
    protected Label Label2;
    protected TextBox txtPlateSearch;
    protected FilteredTextBoxExtender txtPlateSearch_FilteredTextBoxExtender;
    protected Button wibSearch;
    protected GridView wdgList;
    protected Pager custPagerRMV;
    protected Label lblMessage0;
    protected UpdatePanel UpdatePanel2;
    protected Label Label3;
    protected TextBox txtPlateNewEdit;
    protected Label Label1;
    protected TextBox txtCodDev;
    protected Label Label4;
    protected TextBox txtFechaDev;
    protected TextBox txtObserv;
    protected Button wibSave;
    protected Button wibReturn;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      if (this.Page.IsPostBack)
        ;
    }

    protected void wibSearch_Click(object sender, EventArgs e) => this.SearchRMV();

    protected void custPagerRMV_PageChanged(object sender, CustomPageChangeArgs e)
    {
      try
      {
        string v_Plate = this.txtPlateSearch.Text.TrimEnd();
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        int i_ProductId = this.SetProductId(this.i_PlateTypeId);
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RefundMovementVehicle.aspx");
        this.SearchRMVList(v_Plate, i_ProductId, this.i_PlateTypeId, systemUser.i_SystemUserId, false);
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

    protected void wdgList_ItemCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        if (!(e.CommandName == "Select"))
          return;
        int int32 = Convert.ToInt32(e.CommandArgument);
        if (this.wdgList.Rows[int32] == null)
          throw new HandledException(4, "Error de selección.", "'wdgList' - RefundMovementVehicle.aspx");
        RefundMovementVehicle.PlateNew = this.wdgList.DataKeys[int32]["v_PlateNew"].ToString();
        this.ViewState["i_VehicleMovementId"] = (object) this.wdgList.DataKeys[int32]["i_VehicleMovementId"].ToString();
        this.txtPlateNewEdit.Text = RefundMovementVehicle.PlateNew;
        this.txtFechaDev.Text = DateTime.Now.ToString();
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = new RefundVehicleMovementBL().ListPlatesMovementVehicleCorrelative();
        this.txtCodDev.Text = dataTable2.Rows[0]["v_CorrelativeR"].ToString();
        RefundMovementVehicle.CorrelativeCode = this.txtCodDev.Text;
        Logging.WriteFileLog(this.strEventLogFile, string.Format("Se generó correlativo '{0}' para la placa '{1}' - {2}", (object) dataTable2.Rows[0]["v_CorrelativeR"].ToString(), (object) RefundMovementVehicle.PlateNew, (object) DateTime.Now), enmFileSection.Content);
        string script = UtilDA.ActiveTabIndex("tabs", 1, "0");
        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
        this.txtPlateNewEdit.Attributes.Add("readonly", "readonly");
        this.txtCodDev.Attributes.Add("readonly", "readonly");
        this.txtFechaDev.Attributes.Add("readonly", "readonly");
        this.wibSave.Enabled = true;
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

    protected void wibSave_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.ViewState["i_VehicleMovementId"] == null)
          return;
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RefundMovementVehicle.aspx");
        int int32 = Convert.ToInt32(this.ViewState["i_VehicleMovementId"].ToString());
        this.ViewState["Dias"] = (object) "";
        RefundVehicleMovementBL vehicleMovementBl = new RefundVehicleMovementBL();
        DateTime dateTime = Convert.ToDateTime(this.txtFechaDev.Text);
        string text = this.txtPlateNewEdit.Text;
        if (RefundMovementVehicle.PlateNew != text || this.txtCodDev.Text != RefundMovementVehicle.CorrelativeCode)
        {
          Logging.WriteFileLog(this.strEventLogFile, string.Format("Se intentó modificar la devolución de la placa '{0}' - {1}", (object) RefundMovementVehicle.PlateNew, (object) DateTime.Now), enmFileSection.Content);
          throw new HandledException(0, "Los datos no coinciden");
        }
        DataTable dataTable = new BlockPlateBL().SearchBlockPlate(text, 1, 10, out int _);
        Logging.WriteFileLog(this.strEventLogFile, string.Format("Se consultó el bloqueo de placa para la placa '{0}' - {1}", (object) text, (object) DateTime.Now), enmFileSection.Content);
        if (dataTable.Rows.Count > 0 && (Convert.ToInt32(dataTable.Rows[0]["MaxDias"]) == Convert.ToInt32(dataTable.Rows[0]["Dia16"]) || Convert.ToInt32(dataTable.Rows[0]["MaxDias"]) == Convert.ToInt32(dataTable.Rows[0]["Dia17"]) || Convert.ToInt32(dataTable.Rows[0]["MaxDias"]) == Convert.ToInt32(dataTable.Rows[0]["Dia18"])))
          this.ViewState["Dias"] = (object) dataTable.Rows[0]["MaxDias"].ToString();
        Tuple<int, string> tuple = vehicleMovementBl.RefundPlatesMovementVehicle(int32, this.txtObserv.Text, this.txtCodDev.Text, dateTime, systemUser.i_SystemUserId);
        switch (tuple.Item1)
        {
          case 0:
            this.wibSave.Enabled = false;
            Logging.WriteFileLog(this.strEventLogFile, string.Format("Se guardó correctamente la placa '{0}' con el correlativo '{1}' con fecha de devolución '{2}' y fecha de base de datos {3}", (object) text, (object) this.txtCodDev.Text, (object) dateTime, (object) DateTime.Now), enmFileSection.Content);
            if (this.ViewState["Dias"].ToString() != "")
              this.SendEmail(text);
            throw new HandledException(2, tuple.Item2);
          case 1:
            this.wibSave.Enabled = false;
            Logging.WriteFileLog(this.strEventLogFile, string.Format("La placa con el correlativo '{0}' ya cuenta con una devolución a nivel de usuario - {1}", (object) text, (object) DateTime.Now), enmFileSection.Content);
            throw new HandledException(1, tuple.Item2);
          default:
            Logging.WriteFileLog(this.strEventLogFile, string.Format("Hubo un error al guardar la placa '{0}' con el correlativo '{1}' - {2}", (object) text, (object) this.txtCodDev.Text, (object) DateTime.Now), enmFileSection.Content);
            throw new HandledException(0, "Consulte con su administrador");
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    public bool SendEmail(string v_PlateNew)
    {
      Email email = new Email();
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.SMTPServerConfiguration.ToString(),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string pstrSMTPServer = dataTable.Rows[0]["v_Value"].ToString();
      int pintSMTPPort = int.Parse(dataTable.Rows[1]["v_Value"].ToString());
      string str1 = dataTable.Rows[5]["v_Value"].ToString();
      string pstrSMTPPassword = dataTable.Rows[6]["v_Value"].ToString();
      bool boolean = Convert.ToBoolean(dataTable.Rows[4]["v_Value"].ToString());
      string pstrEmailSubject = "Envío notificación placa devuelta";
      List<string> pstrEmailTo = new List<string>();
      string str2 = "<p><p><strong> ATENCIÓN.-</strong><br><br>La placa Rotativa N° <strong> " + v_PlateNew + "</strong> ha excedido el plazo máximo de asignación específica y registra devolución a nivel usuario el día N° " + this.ViewState["Dias"].ToString() + ".<br><br>Atentamente, <br><br>Dpto. Placas Rotativas <br>Teléfono: 6403637 anexo 174 <br>AAP <br>";
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("<html><body>");
      stringBuilder.AppendLine(str2);
      stringBuilder.AppendLine("</body></html>");
      string pstrEmailBody = stringBuilder.ToString();
      List<string> pstrEmailCC = new List<string>();
      if (ConfigurationManager.AppSettings["EmailCopy_Rotate"] != null)
      {
        string str3 = ConfigurationManager.AppSettings["EmailCopy_Rotate"].ToString().Trim();
        char[] chArray = new char[1]{ '|' };
        foreach (string str4 in str3.Split(chArray))
        {
          if (!string.IsNullOrEmpty(str4))
            pstrEmailCC.Add(str4.ToString());
        }
      }
      return Email.SendEmail(str1, pstrSMTPPassword, pstrEmailSubject, pstrEmailBody, pstrEmailTo, pstrEmailCC, pstrSMTPServer, pintSMTPPort, str1, boolean);
    }

    protected void wibReturn_Click(object sender, EventArgs e)
    {
      try
      {
        string script = UtilDA.ActiveTabIndex("tabs", 0, "1");
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel2, this.UpdatePanel2.GetType(), "Script", script, true);
        this.txtObserv.Text = "";
        this.wibSearch_Click((object) null, (EventArgs) null);
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

    private void SearchRMV()
    {
      try
      {
        string v_Plate = this.txtPlateSearch.Text.TrimEnd();
        this.ViewState["i_PlateTypeId"] = (object) this.Request.QueryString["t"].ToString();
        this.i_PlateTypeId = Convert.ToInt32(this.ViewState["i_PlateTypeId"].ToString());
        int i_ProductId = this.SetProductId(this.i_PlateTypeId);
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - RefundMovementVehicle.aspx");
        if (string.IsNullOrEmpty(v_Plate))
        {
          this.SearchRMVList(v_Plate, i_ProductId, this.i_PlateTypeId, systemUser.i_SystemUserId, true);
          Logging.WriteFileLog(this.strEventLogFile, string.Format("Se hizo una búsqueda general de placas pendientes para devolver del usuario '{0}' - {1}", (object) systemUser.i_SystemUserId, (object) DateTime.Now), enmFileSection.Content);
        }
        else
        {
          this.SearchRMVList(v_Plate, i_ProductId, this.i_PlateTypeId, systemUser.i_SystemUserId, true);
          Logging.WriteFileLog(this.strEventLogFile, string.Format("Se realizó la búsqueda de la placa '{0}' - {1}", (object) v_Plate, (object) DateTime.Now), enmFileSection.Content);
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessage0, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage0, new HandledException(-100, ex));
      }
      finally
      {
        this.HidePopup();
      }
    }

    private void SearchRMVList(
      string v_Plate,
      int i_ProductId,
      int i_SpecialPlateTypeId,
      int i_SystemUser,
      bool pboolLoadPager)
    {
      try
      {
        int pintStartRowIndex = pboolLoadPager ? 1 : this.custPagerRMV.CurrentPageNumber;
        int pintMaxRows = this.custPagerRMV.CurrentPageSize == 0 ? 10 : this.custPagerRMV.CurrentPageSize;
        int pintTotalRows;
        DataTable dataTable = new RefundVehicleMovementBL().ListPlatesMovementVehicle(v_Plate, i_ProductId, i_SpecialPlateTypeId, i_SystemUser, pintStartRowIndex, pintMaxRows, out pintTotalRows);
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
          Message.SetMessage(this.lblMessage, new HandledException(1, "No se encontró información con los criterios de búsqueda seleccionados"));
          this.HidePopup();
        }
        else
          this.lblMessage.Visible = false;
        int num = pintTotalRows;
        this.wdgList.DataSource = (object) dataTable;
        this.wdgList.DataBind();
        this.custPagerRMV.TotalPages = num % pintMaxRows == 0 ? num / pintMaxRows : num / pintMaxRows + 1;
        this.custPagerRMV.TotalRecordCount = pintTotalRows;
        if (!pboolLoadPager)
          return;
        this.custPagerRMV.LoadPager();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private int SetProductId(int i_PlateTypeId)
    {
      try
      {
        int num = 0;
        WarehouseExhibitionQueriesBL exhibitionQueriesBl = new WarehouseExhibitionQueriesBL();
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = exhibitionQueriesBl.SpecialPlateWarehouseProductGet(i_PlateTypeId);
        switch (i_PlateTypeId)
        {
          case 6:
            num = 0;
            break;
          case 7:
            num = Convert.ToInt32(dataTable2.Rows[0]["i_Productid"].ToString());
            break;
          case 11:
            num = Convert.ToInt32(dataTable2.Rows[0]["i_Productid"].ToString());
            break;
        }
        return num;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
