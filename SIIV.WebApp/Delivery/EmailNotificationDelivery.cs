// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Delivery.EmailNotificationDelivery
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.Common.Resource;
using SIIV.Common.Resource.Utilities;
using SIIV.Requirement.BL;
using SIIV.SystemParameter.BL;
using SIIV.Warehouse.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Delivery
{
  public class EmailNotificationDelivery : Page
  {
    private RequirementQueriesBL ObjRequirementQueriesBL;
    protected HtmlForm form1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtPlaca;
    protected DropDownList wddMotive;
    protected TextBox txtObs;
    protected TextBox txtPlateStatusDelivery;
    protected Button btnSendEmail;
    protected Button btnClose;
    protected Label lblMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      this.Initialize();
      this.LoadParameters();
    }

    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.wddMotive.SelectedIndex == 0)
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Seleccione el motivo");
        else if (this.txtObs.Text == "")
        {
          Message.SetMessage(this.lblMessage, enmMessageType.Warning, "Ingrese la observación");
        }
        else
        {
          this.ObjRequirementQueriesBL = new RequirementQueriesBL();
          SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
          List<string> EmailTo1 = new List<string>();
          EmailTo1.Add(this.ViewState["v_Email"].ToString());
          string postrMessage = "";
          if (Convert.ToInt32(this.ViewState["i_status"]) == 3)
          {
            this.TransferWarehouse();
            List<string> EmailTo2 = new List<string>();
            if (ConfigurationManager.AppSettings["Email_TranferenceDelivery"] != null)
            {
              EmailTo2.Add(ConfigurationManager.AppSettings["Email_TranferenceDelivery"].ToString().Trim());
              this.ViewState["Placa"] = (object) this.txtPlaca.Text;
              this.SendEMailTransference(EmailTo2, ref postrMessage);
            }
          }
          int int32 = Convert.ToInt32(this.wddMotive.SelectedValue);
          string text = this.txtObs.Text;
          int num = this.ObjRequirementQueriesBL.RequirementProgramationUpdate(Convert.ToInt32(this.ViewState["i_RequirementPlate"]), int32, text, systemUser.i_SystemUserId);
          if (this.wddMotive.SelectedItem.Text != "Otros")
          {
            if (num <= 0)
              return;
            this.lblMessage.Text = !this.SendEMail(EmailTo1, ref postrMessage) ? "Error en el envío de correo" + postrMessage : "Correo enviado correctamente";
            this.btnSendEmail.Enabled = false;
          }
          else
            this.lblMessage.Text = "Se realizó el proceso correctamente";
        }
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessage, enmMessageType.Error, ex.Message);
      }
    }

    protected void btnClose_Click(object sender, EventArgs e) => this.PopupClose();

    public void Initialize()
    {
      this.ViewState["i_RequirementPlate"] = (object) "";
      this.ViewState["v_Email"] = (object) "";
      this.ViewState["i_status"] = (object) "";
      this.lblMessage.Text = "";
      this.wddMotive.SelectedIndex = 0;
      this.txtObs.Text = "";
      this.btnSendEmail.Enabled = true;
      int int32 = Convert.ToInt32(this.Request.QueryString["RequirementPlateId"].ToString());
      this.ViewState["i_RequirementPlate"] = (object) int32;
      this.ObjRequirementQueriesBL = new RequirementQueriesBL();
      DataTable dataTable1 = new DataTable();
      DataTable dataTable2 = this.ObjRequirementQueriesBL.DeliverySendEmail(int32);
      this.txtPlaca.Text = dataTable2.Rows[0]["v_platenew"].ToString();
      this.txtPlateStatusDelivery.Text = dataTable2.Rows[0]["v_Description"].ToString();
      this.ViewState["v_Email"] = (object) dataTable2.Rows[0]["v_Email"].ToString();
      this.ViewState["i_status"] = (object) dataTable2.Rows[0]["i_status"].ToString();
    }

    protected void LoadParameters()
    {
      DataTable dataTable = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) ("" + SystemParameterGroups.SpecialMotiveDelivery.ToString()),
        (object) "",
        (object) "1",
        (object) "1"
      });
      if (dataTable != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (row["i_GroupId"].ToString() == SystemParameterGroups.SpecialMotiveDelivery.ToString())
            this.wddMotive.Items.Add(new System.Web.UI.WebControls.ListItem(row["v_Description"].ToString(), row["i_ParameterId"].ToString()));
        }
      }
      this.wddMotive.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- Seleccione - ", "0"));
      this.wddMotive.SelectedValue = "0";
    }

    private void TransferWarehouse()
    {
      ProductWarehouseQueriesBL warehouseQueriesBl = new ProductWarehouseQueriesBL();
      DataTable warehouse = warehouseQueriesBl.GetWarehouse(this.txtPlaca.Text.TrimEnd());
      this.ViewState["i_WarehouseidIni"] = (object) warehouse.Rows[0]["i_Warehouseini"].ToString();
      this.ViewState["i_WarehouseidFin"] = (object) warehouse.Rows[0]["i_Warehousedes"].ToString();
      DataTable productWarehouseBy = warehouseQueriesBl.GetProductWarehouseBy(0, Convert.ToInt32(this.ViewState["i_WarehouseidIni"].ToString()), 0, 0, "", 2, -1, 2, this.txtPlaca.Text.TrimEnd(), Convert.ToInt32(this.ViewState["i_RequirementPlate"].ToString()), 0, 0, out int _);
      if (productWarehouseBy.Rows.Count == 0)
        return;
      DataTable pdtStockMovementDetail = new DTStockMovementDetail().DataTableStockMovementDetail();
      DataRowCollection rows = pdtStockMovementDetail.Rows;
      object[] objArray = new object[24];
      objArray[0] = (object) Convert.ToInt32(productWarehouseBy.Rows[0]["i_StockMovementDetailId"].ToString());
      objArray[1] = (object) Convert.ToInt32(productWarehouseBy.Rows[0]["i_StockMovementId"].ToString());
      objArray[3] = (object) Convert.ToInt32(productWarehouseBy.Rows[0]["i_ProductId"].ToString());
      objArray[6] = (object) 1;
      objArray[7] = (object) productWarehouseBy.Rows[0]["v_Description"].ToString();
      objArray[8] = (object) Convert.ToInt32(productWarehouseBy.Rows[0]["i_IdAssociated"].ToString());
      objArray[10] = (object) Convert.ToInt32(productWarehouseBy.Rows[0]["b_Status"].ToString());
      objArray[16] = (object) Convert.ToInt32(productWarehouseBy.Rows[0]["i_Item"].ToString());
      objArray[20] = (object) productWarehouseBy.Rows[0]["v_Plate"].ToString();
      objArray[21] = (object) 2;
      rows.Add(objArray);
      new StockMovementManagementBL().StockMovementInsertTransfer(this.GetCurrentStockMovement(), pdtStockMovementDetail);
    }

    private StockMovement GetCurrentStockMovement()
    {
      SystemUser systemUser = this.Session["SystemUser"] as SystemUser;
      return new StockMovement()
      {
        i_StockMovementId = 0,
        i_WarehouseId = new int?(Convert.ToInt32(this.ViewState["i_WarehouseidIni"].ToString())),
        i_MotiveMovementId = new int?(4),
        i_UserId = new int?(systemUser.i_SystemUserId),
        b_Checked = new bool?(true),
        v_Observation = "Transferencia automatica Delivery - AAP",
        d_InsertDate = new DateTime?(DateTime.Now),
        i_ProductionOrderId = new int?(),
        i_ShelfOnDemandId = -1,
        i_TargetWarehouseId = Convert.ToInt32(this.ViewState["i_WarehouseidFin"].ToString())
      };
    }

    public bool SendEMail(List<string> EmailTo, ref string postrMessage)
    {
      Email email = new Email();
      DataTable dataTable1 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.SMTPServerConfiguration.ToString(),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string pstrSMTPServer = dataTable1.Rows[0]["v_Value"].ToString();
      int pintSMTPPort = int.Parse(dataTable1.Rows[1]["v_Value"].ToString());
      string pstrSMTPUserName = dataTable1.Rows[2]["v_Value"].ToString();
      string pstrSMTPPassword = dataTable1.Rows[3]["v_Value"].ToString();
      bool boolean = Convert.ToBoolean(dataTable1.Rows[4]["v_Value"]);
      DataTable dataTable2 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.SpecialPlateDeliveryNotification.ToString(),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string pstrEmailSubject = dataTable2.Rows[0]["v_Value"].ToString();
      string str1 = dataTable2.Rows[1]["v_Value"].ToString();
      string pstrEmailFrom = dataTable2.Rows[2]["v_Value"].ToString();
      string str2 = dataTable2.Rows[3]["v_Value"].ToString();
      string str3 = "";
      string str4 = this.wddMotive.SelectedItem.Text + " : " + this.txtObs.Text;
      if (Convert.ToInt32(this.ViewState["i_status"]) == 1)
        str3 = str1 + " " + this.wddMotive.SelectedItem.Text + ", Por ello deberá llamar al 640-3636 para programar una segunda visita en un plazo no mayor de 10 días calendarios de haber recibido la constancia de visita.";
      else if (Convert.ToInt32(this.ViewState["i_status"]) == 3)
        str3 = str1 + " " + this.wddMotive.SelectedItem.Text + ", en tal sentido deberá acercarse a la AV. NICOLÁS ARRIOLA N° 304 - SANTA CATALINA - LA VICTORIA REF.: ESQUINA CON AV.CARLOS VILLARÁN, para el recojo de las placas.";
      string str5 = str4 + "<br><br>" + str3 + "<br>";
      List<string> stringList = new List<string>();
      List<string> pstrEmailCC = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("<html><body>");
      stringBuilder.AppendLine(str5);
      stringBuilder.AppendLine(str2);
      stringBuilder.AppendLine("</body></html>");
      if (ConfigurationManager.AppSettings["Email_Delivery"] != null)
      {
        string str6 = ConfigurationManager.AppSettings["Email_Delivery"].ToString().Trim();
        char[] chArray = new char[1]{ '|' };
        foreach (string str7 in str6.Split(chArray))
        {
          if (!string.IsNullOrEmpty(str7))
            EmailTo.Add(str7.ToString());
        }
      }
      return Email.SendEmail(pstrSMTPUserName, pstrSMTPPassword, pstrEmailSubject, stringBuilder.ToString(), EmailTo, pstrEmailCC, pstrSMTPServer, pintSMTPPort, pstrEmailFrom, boolean, EmailTo, ref postrMessage);
    }

    public bool SendEMailTransference(List<string> EmailTo, ref string postrMessage)
    {
      Email email = new Email();
      DataTable dataTable1 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.SMTPServerConfiguration.ToString(),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string pstrSMTPServer = dataTable1.Rows[0]["v_Value"].ToString();
      int pintSMTPPort = int.Parse(dataTable1.Rows[1]["v_Value"].ToString());
      string pstrSMTPUserName = dataTable1.Rows[2]["v_Value"].ToString();
      string pstrSMTPPassword = dataTable1.Rows[3]["v_Value"].ToString();
      bool boolean = Convert.ToBoolean(dataTable1.Rows[4]["v_Value"]);
      DataTable dataTable2 = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
      {
        (object) SystemParameterGroups.SpecialPlateDeliveryNotification.ToString(),
        (object) "",
        (object) "1",
        (object) "1"
      });
      string pstrEmailSubject = "Tranferencia de Placa Delivery";
      string pstrEmailFrom = dataTable2.Rows[2]["v_Value"].ToString();
      string str1 = "La placa " + this.ViewState["Placa"]?.ToString() + " ha sido transferida del almacén de Delivery al almacén de AAP";
      string str2 = dataTable2.Rows[3]["v_Value"].ToString();
      List<string> stringList = new List<string>();
      List<string> pstrEmailCC = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("<html><body>");
      stringBuilder.AppendLine(str1);
      stringBuilder.AppendLine(str2);
      stringBuilder.AppendLine("</body></html>");
      return Email.SendEmail(pstrSMTPUserName, pstrSMTPPassword, pstrEmailSubject, stringBuilder.ToString(), EmailTo, pstrEmailCC, pstrSMTPServer, pintSMTPPort, pstrEmailFrom, boolean, EmailTo, ref postrMessage);
    }

    private void PopupClose()
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
