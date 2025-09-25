// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Production.SpecialProductionPlateDetail
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Common.DA.Utilities;
using SIIV.Common.Resource;
using SIIV.SpecialPlateProduction.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;

#nullable disable
namespace SIIV.WebApp.Exhibition.Production
{
  public class SpecialProductionPlateDetail : Page
  {
    private DataTable dtSpecialPlateProductionDetail = new DataTable();
    public int i_SpecialPlateProductionId;
    protected HtmlHead Head1;
    protected HtmlForm form1;
    protected HiddenField H1;
    protected UpdatePanel UpdatePanel1;
    protected System.Web.UI.ScriptManager ScriptManager1;
    protected Label Label1;
    protected TextBox txtId;
    protected DropDownList cboSpecialPlateMotive;
    protected DropDownList cboSpecialPlateClasification;
    protected TextBox txtObservacion;
    protected DropDownList cboSerie;
    protected TextBox txtSerie;
    protected HtmlGenericControl tbRenov;
    protected TextBox txtSerieIni;
    protected FilteredTextBoxExtender txtSerieIni_Validator;
    protected TextBox txtSerieFin;
    protected FilteredTextBoxExtender txtSerieFin_Validator;
    protected HtmlGenericControl tbInma;
    protected TextBox txtCantidad;
    protected FilteredTextBoxExtender txtCantidad_Validator;
    protected Button wibNew;
    protected HtmlTableCell tbExcel;
    protected Button ImpExcel;
    protected GridView wdgSpecialProductionPlateList;
    protected Button wibAceptar;
    protected Button wibCancelar;
    protected Label lblRecordCount;
    protected Label lblMsg;
    protected Label Label2;
    protected FileUpload FileInput;
    protected Button consultar;
    protected GridView wdgList;
    protected Label lblMessageUser;
    protected Button wibSave;
    protected Button wibClose;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMsg.Visible = false;
      this.lblMsg.Text = "";
      this.lblMessageUser.Visible = false;
      this.lblMessageUser.Text = "";
      if (this.Page.IsPostBack)
        return;
      try
      {
        if (Convert.ToInt32(this.cboSpecialPlateMotive.SelectedValue) == 1)
          this.tbInma.Visible = true;
        else
          this.tbRenov.Visible = false;
        this.LoadParameter();
        this.LoadSerie();
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }

    protected void wibNew_Click(object sender, EventArgs e)
    {
      try
      {
        if (Convert.ToInt32(this.cboSerie.SelectedValue) == -1)
          throw new HandledException(1, "Debe seleccionar la serie");
        if (Convert.ToInt32(this.cboSpecialPlateMotive.SelectedValue) == 1)
        {
          if (this.txtCantidad.Text == "" || this.txtCantidad.Text == "0")
            throw new HandledException(1, "Cantidad debe ser Mayor a 0");
        }
        else
        {
          if (this.txtSerieIni.Text == "" || this.txtSerieIni.Text == "0")
            throw new HandledException(1, "Número de Serie Inicial debe ser Mayor a 0");
          if (this.txtSerieFin.Text == "" || this.txtSerieFin.Text == "0")
            throw new HandledException(1, "Número de Serie Final debe ser Mayor a 0");
        }
        this.dtSpecialPlateProductionDetail = (DataTable) this.ViewState["dtSpecialPlateProductionDetail"];
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = new SpecialPlateProductionQueriesBL().ProductionCorrelative(this.cboSerie.SelectedItem.Text, Convert.ToInt32(this.Request.QueryString["SpecialPlateTypeId"].ToString()));
        int int32;
        int num;
        if (Convert.ToInt32(this.cboSpecialPlateMotive.SelectedValue) == 1)
        {
          if (Convert.ToInt32(dataTable2.Rows[0][0].ToString()) == 1000)
          {
            this.txtCantidad.Text = "";
            throw new HandledException(1, "La Serie seleccionada tiene el límite de placas asignadas");
          }
          if (Convert.ToInt32(dataTable2.Rows[0][0].ToString()) + Convert.ToInt32(this.txtCantidad.Text.TrimEnd()) > 1000)
          {
            this.txtCantidad.Text = "";
            throw new HandledException(1, "La cantidad ingresada debe ser menor, ya que sobrepasa las 1000 placas de la serie");
          }
          int32 = Convert.ToInt32(dataTable2.Rows[0][0].ToString());
          num = int32 + Convert.ToInt32(this.txtCantidad.Text.TrimEnd()) - 1;
        }
        else
        {
          int32 = Convert.ToInt32(this.txtSerieIni.Text);
          num = Convert.ToInt32(this.txtSerieFin.Text);
        }
        
        for (int index = int32; index <= num; ++index)
        {
          string str = this.cboSerie.SelectedItem.Text + index.ToString().PadLeft(3, '0');
          if (((IEnumerable<DataRow>) this.dtSpecialPlateProductionDetail.Select("v_Plate = '" + str + "'")).Count<DataRow>() > 0)
            throw new HandledException(1, "Número de Placa '" + str + "' ya se encuentra cargado");
        }
        for (int index = int32; index <= num; ++index)
        {
          this.dtSpecialPlateProductionDetail = (DataTable) this.ViewState["dtSpecialPlateProductionDetail"];
          DataRow row = this.dtSpecialPlateProductionDetail.NewRow();
          row["i_SpecialPlateProductionDetailId"] = (object) 0;
          row["v_Plate"] = (object) (this.cboSerie.SelectedItem.Text + index.ToString().PadLeft(3, '0'));
          row["i_Status"] = (object) 0;
          row["i_StateRow"] = (object) 0;
          row["v_Status"] = (object) "Pendiente";
          this.dtSpecialPlateProductionDetail.Rows.Add(row);
        }
        this.wdgSpecialProductionPlateList.DataSource = (object) this.dtSpecialPlateProductionDetail;
        this.wdgSpecialProductionPlateList.DataBind();
        this.lblRecordCount.Text = this.wdgSpecialProductionPlateList.Rows.Count <= 0 ? "0" : this.wdgSpecialProductionPlateList.Rows.Count.ToString();
        this.ViewState["dtSpecialPlateProductionDetail"] = (object) this.dtSpecialPlateProductionDetail;
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
        this.lblRecordCount.Text = "0";
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }

    protected void wdgSpecialProductionPlateList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowIndex < 0)
          return;
        if (e.Row.Cells[5].Text == "1")
        {
          for (int index = 0; index < 4; ++index)
            e.Row.Cells[index].CssClass = "Test1";
        }
        else
        {
          for (int index = 0; index < 4; ++index)
            e.Row.Cells[index].CssClass = "Test2";
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }

    protected void wdgSpecialProductionPlateList_ItemCommand(
      object sender,
      GridViewCommandEventArgs e)
    {
      try
      {
        if (!e.CommandName.Equals("Delete", StringComparison.CurrentCulture))
          return;
        string text = (this.wdgSpecialProductionPlateList.Rows[Convert.ToInt32(e.CommandArgument)] ?? throw new HandledException(4, "Error de selección.", "'wdgList' - SpecialProductionPlateDetail.aspx")).Cells[1].Text;
        this.dtSpecialPlateProductionDetail = (DataTable) this.ViewState["dtSpecialPlateProductionDetail"];
        foreach (DataRow row in (InternalDataCollectionBase) this.dtSpecialPlateProductionDetail.Rows)
        {
          if (row["v_Plate"].ToString() == text)
          {
            this.dtSpecialPlateProductionDetail.Rows.Remove(row);
            break;
          }
        }
        this.wdgSpecialProductionPlateList.DataSource = (object) this.dtSpecialPlateProductionDetail;
        this.wdgSpecialProductionPlateList.DataBind();
        this.lblRecordCount.Text = this.wdgSpecialProductionPlateList.Rows.Count <= 0 ? "0" : this.wdgSpecialProductionPlateList.Rows.Count.ToString();
        this.ViewState["dtSpecialPlateProductionDetail"] = (object) this.dtSpecialPlateProductionDetail;
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }

    protected void wibAceptar_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.txtObservacion.Text.Trim() == "")
          throw new HandledException(1, "Debe ingresar una Observación");
        this.i_SpecialPlateProductionId = Convert.ToInt32(this.Request.QueryString["SpecialPlateProductionId"].ToString());
        this.dtSpecialPlateProductionDetail = (DataTable) this.ViewState["dtSpecialPlateProductionDetail"];
        if (this.dtSpecialPlateProductionDetail.Rows.Count == 0)
          throw new HandledException(1, "Debe ingresar las Placas a Fabricar");

        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - SpecialProductionPlateDetail.aspx");
        XElement xelement = new XElement((XName) "SpecialPlateProduction", new object[8]
        {
          (object) new XElement((XName) "i_SpecialPlateProductionId", (object) this.i_SpecialPlateProductionId.ToString()),
          (object) new XElement((XName) "i_SpecialPlateProcessTypeId", (object) 5),
          (object) new XElement((XName) "i_SpecialPlateTypeId", (object) this.Request.QueryString["SpecialPlateTypeId"].ToString()),
          (object) new XElement((XName) "i_SpecialPlateClasificationId", (object) this.cboSpecialPlateClasification.SelectedItem.Value),
          (object) new XElement((XName) "i_MotiveId", (object) Convert.ToInt32(this.cboSpecialPlateMotive.SelectedValue)),
          (object) new XElement((XName) "V_Observation", (object) this.txtObservacion.Text),
          (object) new XElement((XName) "i_UserId", (object) systemUser.i_SystemUserId),
          (object) new XElement((XName) "SpecialPlateProductionDetails")
        });
        foreach (DataRow row in (InternalDataCollectionBase) this.dtSpecialPlateProductionDetail.Rows)
        {
          XElement content = new XElement((XName) "SpecialPlateProductionDetail", new object[2]
          {
            (object) new XElement((XName) "i_SpecialPlateProductionId", (object) this.i_SpecialPlateProductionId.ToString()),
            (object) new XElement((XName) "v_Plate", (object) row["v_Plate"].ToString())
          });
          xelement.Element((XName) "SpecialPlateProductionDetails").Add((object) content);
        }
        DataTable dataTable = new SpecialPlateProductionQueriesBL().SetSpecialPlateProduction(xelement.ToString());
        if (dataTable == null)
        {
          string script = "SendInfoPopup();";
          System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
        }
        else
        {
          this.dtSpecialPlateProductionDetail = (DataTable) this.ViewState["dtSpecialPlateProductionDetail"];
          foreach (DataRow row in (InternalDataCollectionBase) this.dtSpecialPlateProductionDetail.Rows)
            row["i_StateRow"] = (object) 0;
          foreach (DataRow row1 in (InternalDataCollectionBase) dataTable.Rows)
          {
            foreach (DataRow row2 in (InternalDataCollectionBase) this.dtSpecialPlateProductionDetail.Rows)
            {
              if (row1["v_Plate"].ToString() == row2["v_Plate"].ToString())
              {
                row2["i_StateRow"] = (object) 1;
                break;
              }
            }
          }
          if (this.cboSpecialPlateMotive.SelectedValue.ToString() == "2")
            Message.SetMessage(this.lblMsg, new HandledException(0, "Placas no disponibles para una Re-Emisión, Verificar los registros en Rojo"));
          else if (this.cboSpecialPlateMotive.SelectedValue.ToString() == "1")
            Message.SetMessage(this.lblMsg, new HandledException(0, "Placas Existentes o en Proceso de Producción, Verificar los Registros en Rojo"));
          this.wdgSpecialProductionPlateList.DataSource = (object) this.dtSpecialPlateProductionDetail;
          this.wdgSpecialProductionPlateList.DataBind();
          this.ViewState["dtSpecialPlateProductionDetail"] = (object) this.dtSpecialPlateProductionDetail;
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMsg, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMsg, new HandledException(-100, ex));
      }
    }

    protected void wibCancelar_Click(object sender, EventArgs e)
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    protected void cboSpecialPlateMotive_SelectionChanged(object sender, EventArgs e)
    {
      if (Convert.ToInt32(this.cboSpecialPlateMotive.SelectedValue) == 1)
      {
        this.tbInma.Visible = true;
        this.tbRenov.Visible = false;
        this.ImpExcel.Visible = false;
      }
      else
      {
        this.tbRenov.Visible = true;
        this.tbInma.Visible = false;
        this.ImpExcel.Visible = true;
      }
    }

    private void LoadParameter()
    {
      try
      {
        int int32 = Convert.ToInt32(this.Request.QueryString["SpecialPlateProductionId"].ToString());
        Convert.ToInt32(this.Request.QueryString["SpecialPlateTypeId"].ToString());
        this.dtSpecialPlateProductionDetail = new SpecialPlateProductionQueriesBL().GetSpecialPlateProductionDetail(int32);
        this.dtSpecialPlateProductionDetail.Columns["i_StateRow"].ReadOnly = false;
        this.ViewState["dtSpecialPlateProductionDetail"] = (object) this.dtSpecialPlateProductionDetail;
        this.wdgSpecialProductionPlateList.DataSource = (object) this.dtSpecialPlateProductionDetail;
        this.wdgSpecialProductionPlateList.DataBind();
        List<SIIV.BE.SystemParameter> systemParameterList = new SystemParameterManagementBL().Get((object) new ArrayList()
        {
          (object) "205",
          (object) "1,5",
          (object) "1",
          (object) "1"
        });
        this.cboSpecialPlateClasification.Items.Clear();
        foreach (SIIV.BE.SystemParameter systemParameter in systemParameterList)
          this.cboSpecialPlateClasification.Items.Add(new ListItem(systemParameter.v_Description, systemParameter.i_ParameterId.ToString()));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void LoadSerie()
    {
      try
      {
        string str = this.Request.QueryString["SpecialPlateTypeId"].ToString();
        List<SIIV.BE.SystemParameter> systemParameterList = new SystemParameterManagementBL().Get((object) new ArrayList()
        {
          (object) "265",
          (object) "",
          (object) "1",
          (object) "1"
        });
        this.cboSerie.Items.Clear();
        foreach (SIIV.BE.SystemParameter systemParameter in systemParameterList)
        {
          if (systemParameter.v_ReferenceId == str)
            this.cboSerie.Items.Add(new ListItem(systemParameter.v_Description, systemParameter.i_ParameterId.ToString()));
        }
        this.cboSerie.Items.Insert(0, new ListItem(SIIV.SystemParameter.BL.Constants.OPCIONLISTA_Seleccione, SIIV.SystemParameter.BL.Constants.OPCIONLISTA_ValorSinSeleccion));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    protected void wdgSpecialProductionPlateList_RowDeleting(
      object sender,
      GridViewDeleteEventArgs e)
    {
    }

    protected void wdgSpecialProductionPlateList_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }

    protected void ImpExcel_Click(object sender, EventArgs e)
    {
      try
      {
        this.wdgList.Visible = false;
        this.wdgList.DataSource = (object) null;
        this.wdgList.DataBind();
        string script1 = UtilDA.ActiveTabIndex("tabs", 1, "0");
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script1, true);
        string script2 = "TabIndex();";
        System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "ScriptIndex", script2, true);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    protected void consultar_Click(object sender, EventArgs e)
    {
      this.wdgList.DataSource = (object) null;
      this.wdgList.DataBind();
      bool flag = false;
      try
      {
        string path = ConfigurationManager.AppSettings["FilePath"].ToString();
        string empty = string.Empty;
        this.FileInput.FileName.ToString();
        if (this.FileInput.HasFile)
        {
          string[] source = new string[2]{ ".xls", ".xlsx" };
          string extension = Path.GetExtension(this.FileInput.PostedFile.FileName);
          if (!((IEnumerable<string>) source).Contains<string>(extension))
          {
            this.lblMessageUser.ForeColor = Color.Red;
            flag = true;
            throw new HandledException(1, "Por favor, sube solamente Archivos Excel.");
          }
          if (this.FileInput.PostedFile.ContentLength <= 1048576)
          {
            string fileName = Path.GetFileName(this.Server.MapPath(this.FileInput.FileName));
            this.FileInput.SaveAs(this.Server.MapPath(path) + fileName);
            string str = this.Server.MapPath(path) + fileName;
            OleDbConnection connection = (OleDbConnection) null;
            switch (extension)
            {
              case ".xls":
                connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + str + ";Extended Properties=Excel 8.0;");
                break;
              case ".xlsx":
                connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + str + ";Extended Properties=Excel 12.0;");
                break;
            }
            connection.Open();
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(new OleDbCommand("SELECT * FROM [" + connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, (object[]) null).Rows[0]["Table_Name"].ToString() + "]", connection));
            DataSet dataSet = new DataSet();
            oleDbDataAdapter.Fill(dataSet);
            connection.Close();
            this.Session["Table"] = (object) dataSet.Tables[0];
            this.wdgList.DataSource = (object) dataSet;
            this.wdgList.DataBind();
            string script = UtilDA.ActiveTabIndex("tabs", 1, "0");
            System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
            this.wdgList.Visible = true;
          }
          else
          {
            flag = true;
            throw new HandledException(1, "Tamaño del archivo adjunto no debe ser mayor que 1 MB.");
          }
        }
        else
        {
          flag = true;
          throw new HandledException(1, "Por favor, seleccione un archivo para cargar.");
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessageUser, ex);
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageUser, new HandledException(-100, ex));
        flag = true;
      }
      if (!flag)
        return;
      string script1 = UtilDA.ActiveTabIndex("tabs", 1, "0");
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script1, true);
    }

    protected void wibClose_Click(object sender, EventArgs e)
    {
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      bool flag = false;
      try
      {
        if (this.wdgList.Rows.Count <= 0)
          throw new HandledException(1, "Por Favor, Debe seleccionar un archivo Excel.");
        DataTable dataTable = (DataTable) this.Session["Table"];
        if (dataTable.Columns.Count != 1)
          throw new HandledException(1, "el archivo debe tener una columna");
        if (dataTable.Columns[0].ColumnName.ToUpper() != "PLACA")
          throw new HandledException(1, "el nombre de la columna debe ser 'Placa'");
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          string str1 = row[0].ToString();
          string text = str1.Length == 6 ? str1.Substring(0, 3) : throw new HandledException(1, "el numero de placa no tiene 6 caracteres");
          string str2 = str1.Substring(3, 3);
          Convert.ToInt32(this.Request.QueryString["SpecialPlateTypeId"].ToString());
          this.cboSerie.SelectedIndex = this.cboSerie.Items.IndexOf(this.cboSerie.Items.FindByText(text));
          this.txtSerieIni.Text = str2;
          this.txtSerieFin.Text = str2;
          this.wibNew_Click((object) null, (EventArgs) null);
        }
      }
      catch (HandledException ex)
      {
        Message.SetMessage(this.lblMessageUser, ex);
        flag = true;
      }
      catch (Exception ex)
      {
        Message.SetMessage(this.lblMessageUser, new HandledException(-100, ex));
        flag = true;
      }
      if (!flag)
        return;
      string script = UtilDA.ActiveTabIndex("tabs", 1, "0");
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }
  }
}
