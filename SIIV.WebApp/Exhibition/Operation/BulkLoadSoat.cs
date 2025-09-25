// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Exhibition.Operation.BulkLoadSoat
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using SIIV.BE;
using SIIV.BE.CustomCode;
using SIIV.Common.Resource;
using SIIV.Exhibition.BL;
using SIIV.SystemParameter.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Exhibition.Operation
{
  public class BulkLoadSoat : Page
  {
    protected HtmlForm form1;
    protected FileUpload FileInputExcel;
    protected HiddenField HiddenField1;
    protected Button wibSearch;
    protected Label lblMessage;
    protected GridView wdgList;
    protected Label lblMessageUser;
    protected Button wibSave;
    protected Button wibClose;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.lblMessage.Visible = false;
      try
      {
        if (this.Page.IsPostBack)
          return;
        this.HiddenField1.Value = this.Session["t"].ToString();
        this.ViewState["metodo"] = (object) this.HiddenField1.Value;
        this.LoadParameters();
        Message.SetMessage(this.lblMessageUser, new HandledException(1, "Tener en cuenta el formato correcto del archivo excel a cargar"));
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

    public void Buscar()
    {
      string str = this.Request.Form["confirm_value"];
    }

    protected void wibSearch_Click(object sender, EventArgs e)
    {
      string str1 = this.ViewState["metodo"].ToString();
      this.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "<script>javascript:confirmation('" + str1 + "');</script>");
      try
      {
        DataTable dataTable1 = new DataTable();
        Convert.ToInt32(this.ViewState["metodo"].ToString());
        string str2 = this.Server.MapPath("");
        if (this.FileInputExcel.HasFile)
        {
          Path.GetExtension(this.FileInputExcel.FileName);
          string str3 = str2 + "\\" + this.FileInputExcel.FileName;
          if (OtherFormats.ValidateExtensionFile(str3, ".xlsx"))
          {
            if (File.Exists(str3))
              File.Delete(str3);
            this.FileInputExcel.SaveAs(str3);
            DataTable dtDatos = OtherFormats.LoadDataExcel(str3, "[Hoja1$]");
            if (!dtDatos.Columns.Contains("Soat"))
              throw new HandledException(1, "No se ha encontrado la columna 'Soat'");
            if (!dtDatos.Columns.Contains("Nro_Poliza"))
              throw new HandledException(1, "No se ha encontrado la columna 'Nro_Poliza'");
            if (!dtDatos.Columns.Contains("Placa"))
              throw new HandledException(1, "No se ha encontrado la columna 'Placa'");
            if (!dtDatos.Columns.Contains("Fecha_Inicial"))
              throw new HandledException(1, "No se ha encontrado la columna 'Fecha_Inicial'");
            if (!dtDatos.Columns.Contains("Fecha_Final"))
              throw new HandledException(1, "No se ha encontrado la columna 'Fecha_Final'");
            if (!dtDatos.Columns.Contains("IdAseguradora"))
              throw new HandledException(1, "No se ha encontrado la columna 'IdAseguradora'");
            for (int index = dtDatos.Rows.Count - 1; index >= 0; --index)
            {
              if (dtDatos.Rows[index]["Nro_Poliza"].ToString() == "")
                dtDatos.Rows.RemoveAt(index);
              else if (dtDatos.Rows[index]["IdAseguradora"].ToString() != "2")
                throw new HandledException(1, "El numero de la IdAseguradora debe ser 2");
            }
            if (File.Exists(str3))
              File.Delete(str3);
            if (!this.ValidateData(dtDatos))
              return;
            dtDatos.Columns.Add("Aseguradora", Type.GetType("System.String"));
            if (dtDatos == null)
              return;
            DataTable dataTable2 = (DataTable) this.ViewState["dtResult"];//lista de aseguraforas
            for (int index1 = 0; index1 < dtDatos.Rows.Count; ++index1)
            {
              for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
              {
                if (int.Parse(dtDatos.Rows[index1]["IdAseguradora"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture) == Convert.ToInt32(dataTable2.Rows[index2]["Id"], (IFormatProvider) CultureInfo.CurrentCulture))
                  dtDatos.Rows[index1]["Aseguradora"] = (object) dataTable2.Rows[index2]["Name"].ToString();
              }
            }
            this.wdgList.DataSource = (object) dtDatos;
            this.wdgList.DataBind();
            this.ViewState["dtImport"] = (object) dtDatos;
          }
          else
          {
            Message.SetMessage(this.lblMessage, new HandledException(0, "Archivo seleccionado NO es Válido"));
            this.ClearGridView();
          }
        }
        else
        {
          Message.SetMessage(this.lblMessage, new HandledException(0, "Seleccione el archivo que contiene los datos del SOAT"));
          this.ClearGridView();
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
    }

    protected void wibSave_Click(object sender, EventArgs e)
    {
      try
      {
        List<SpecialPlateSoat> pobjlstSpecialPlateSoat = new List<SpecialPlateSoat>();
        pobjlstSpecialPlateSoat.Clear();
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = (DataTable) this.ViewState["dtImport"];
        SystemUser systemUser = this.Session["SystemUser"] != null ? (SystemUser) this.Session["SystemUser"] : throw new HandledException(3, "La sesión ha expirado.", "'SystemUser' - BulkLoadSoat.aspx");
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
        {
          SpecialPlateSoat specialPlateSoat = new SpecialPlateSoat();
          PlateSoatQueriesBL plateSoatQueriesBl = new PlateSoatQueriesBL();
          specialPlateSoat.v_SoatNumber = row["Soat"].ToString();
          specialPlateSoat.v_InsurancePolicyNumber = row["Nro_Poliza"].ToString().Trim();
          specialPlateSoat.d_StartDateSoat = Convert.ToDateTime(row["Fecha_Inicial"]);
          specialPlateSoat.d_EndDateSoat = new DateTime?(Convert.ToDateTime(row["Fecha_Final"]));
          specialPlateSoat.v_Plate = row["Placa"].ToString().Trim();
          specialPlateSoat.i_InsuranceId = Convert.ToInt32(row["IdAseguradora"]);
          specialPlateSoat.i_Status = 1;
          specialPlateSoat.i_InsertUserId = systemUser.i_SystemUserId;
          specialPlateSoat.d_InsertDate = DateTime.Now;
          pobjlstSpecialPlateSoat.Add(specialPlateSoat);
        }
        if (new PlateSoatManagementBL().SpecialPlateSoatInsert(pobjlstSpecialPlateSoat))
        {
          this.wibSave.Enabled = false;
          throw new HandledException(2, "Se importó correctamente los datos del SOAT");
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
    }

    protected void wibClose_Click(object sender, EventArgs e) => this.PopupClose();

    private void LoadParameters()
    {
      try
      {
        SystemParameterManagementBL parameterManagementBl = new SystemParameterManagementBL();
        string str = SystemParameterGroups.SoatInsurance.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        Convert.ToInt32(this.ViewState["metodo"].ToString());
        ArrayList pobj = new ArrayList()
        {
          (object) str,
          (object) "1,2",
          (object) "1",
          (object) "1"
        };
        List<SIIV.BE.SystemParameter> systemParameterList = parameterManagementBl.Get((object) pobj);
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Id", Type.GetType("System.Int32"));
        dataTable.Columns.Add("Name", Type.GetType("System.String"));
        for (int index = 0; index < systemParameterList.Count; ++index)
        {
          DataRow row = dataTable.NewRow();
          row["Id"] = (object) systemParameterList[index].i_ParameterId;
          row["Name"] = (object) systemParameterList[index].v_Description;
          dataTable.Rows.Add(row);
        }
        this.ViewState["dtResult"] = (object) dataTable;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void ClearGridView()
    {
      this.wdgList.DataSource = (object) null;
      this.wdgList.DataBind();
    }

    public bool ValidateData(DataTable dtDatos)
    {
      try
      {
        List<SIIV.BE.SystemParameter> source = new SystemParameterManagementBL().Get((object) new ArrayList()
        {
          (object) "265",
          (object) "",
          (object) "1",
          (object) "1"
        });
        List<string> list1 = source.Where<SIIV.BE.SystemParameter>((System.Func<SIIV.BE.SystemParameter, bool>) (x => x.v_ReferenceId == "7")).Select<SIIV.BE.SystemParameter, string>((System.Func<SIIV.BE.SystemParameter, string>) (y => y.v_Description)).ToList<string>();
        List<string> list2 = source.Where<SIIV.BE.SystemParameter>((System.Func<SIIV.BE.SystemParameter, bool>) (x => x.v_ReferenceId == "11")).Select<SIIV.BE.SystemParameter, string>((System.Func<SIIV.BE.SystemParameter, string>) (y => y.v_Description)).ToList<string>();
        string str1 = "";
        DateTime now1 = DateTime.Now;
        DateTime now2 = DateTime.Now;
        int num = 0;
        int int32 = Convert.ToInt32(this.ViewState["metodo"].ToString());
        for (int index = 0; index < dtDatos.Rows.Count; ++index)
        {
          str1 = dtDatos.Rows[index]["Soat"].ToString();
          string str2 = dtDatos.Rows[index]["Nro_Poliza"].ToString().Replace(" ", "").Trim();
          string str3 = dtDatos.Rows[index]["Placa"].ToString().Replace("-", "").Trim();
          try
          {
            now1 = DateTime.Parse(dtDatos.Rows[index]["Fecha_Inicial"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
          }
          catch
          {
            throw new HandledException(1, "Fecha Inicial " + now1.ToString((IFormatProvider) CultureInfo.CurrentCulture) + " NO Válida");
          }
          try
          {
            now2 = DateTime.Parse(dtDatos.Rows[index]["Fecha_Final"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
          }
          catch
          {
            throw new HandledException(1, "Fecha Final " + now2.ToString((IFormatProvider) CultureInfo.CurrentCulture) + " NO Válida");
          }
          try
          {
            num = int.Parse(dtDatos.Rows[index]["IdAseguradora"].ToString(), (IFormatProvider) CultureInfo.CurrentCulture);
          }
          catch
          {
            throw new HandledException(1, "Aseguradora " + num.ToString() + " NO Válido");
          }
          if (str3.Length != 6)
            throw new HandledException(1, "La Placa " + str3 + " tiene una longitud NO Válida");
          if (str2.Length != 8)
            throw new HandledException(1, "La Póliza " + str2 + " tiene una estructura NO Válida");
          switch (int32)
          {
            case 7:
              if (!list1.Contains(str3.ToUpper(CultureInfo.CurrentCulture).Substring(0, 3)))
                throw new HandledException(1, "La Placa de Exhibición " + str3 + " tiene una estructura NO Válida");
              break;
            case 11:
              if (!list2.Contains(str3.ToUpper(CultureInfo.CurrentCulture).Substring(0, 3)))
                throw new HandledException(1, "La Placa Rotativa " + str3 + " tiene una estructura NO Válida");
              break;
          }
        }
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }

    private void PopupClose()
    {
      string script = "PopupClosed();";
      System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Script", script, true);
    }
  }
}
