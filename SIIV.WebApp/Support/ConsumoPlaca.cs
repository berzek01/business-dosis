// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Support.ConsumoPlaca
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using AjaxControlToolkit;
using SIIV.BE;
using SIIV.Claim.BL;
using SIIV.Common.Resource;
using SIIV.Process;
using SIIV.Registration.BL;
using SIIV.SystemParameter.BL;
using SIIV.WebApp.proxyServiceMTC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

#nullable disable
namespace SIIV.WebApp.Support
{
  public class ConsumoPlaca : Page
  {
    private string _strDataRaw;
    private List<SUNARPVehicle> lstSUNARPVehicle;
    protected UpdatePanel UpdatePanel1;
    protected TextBox txtDatoIngresado;
    protected FilteredTextBoxExtender txtDatoIngresado_FilteredTextBoxExtender;
    protected Button btnConsultar;
    protected GridView gridDatosVehiculo;
    protected Label lblRecordCountDV;
    protected GridView gridDatosPropietario;
    protected Label lblRecordCountDO;
    protected Button Button1;
    protected Button Limpiar;
    protected Label lblMessage;
    protected Button btnReturnPopupConfirmation;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.IsPostBack)
        return;
      this.InitialDatosVehiculo();
      this.InitialDatosPropietario();
    }

    protected void btnLimpiar_Click(object sender, EventArgs e) => this.CleanForm();

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
      string placaNueva = this.txtDatoIngresado.Text.Trim();
      this.txtDatoIngresado.Enabled = false;
      try
      {
        int int16 = (int) Convert.ToInt16(new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.MaximumLicensePlateCharacters.ToString(),
          (object) "",
          (object) "1",
          (object) "1"
        }).Select("i_ParameterId = '27'")[0]["v_Value"]);
        if (placaNueva.Length > int16 || this.txtDatoIngresado.Text.Trim().Length == 0)
        {
          this.ShowWarningMessage(string.Format("***ADVERTENCIA***</br>El dato ingresado no puede ser vacío y debe tener como máximo <b>{0}</b> carácteres.", (object) int16));
        }
        else
        {
          try
          {
            string endpointConfigurationName = "SunarpSoap";
            DatosRetorno datosRetorno1 = new DatosRetorno();
            SunarpSoapClient sunarpSoapClient = new SunarpSoapClient(endpointConfigurationName);
            sunarpSoapClient.Open();
            DatosRetorno datosRetorno2 = sunarpSoapClient.DatosH_VehiculoSUNARPxPlaca(placaNueva);
            sunarpSoapClient.Close();
            this._strDataRaw = new UTF8Encoding().GetString(ServiceOperation.GetDecompressedData(datosRetorno2.ArrayBytes, datosRetorno2.TamanioOriginal, 100));
            this.ViewState["StrDataRaw"] = (object) this._strDataRaw;
            if (placaNueva.Trim() == string.Empty || datosRetorno2.TamanioOriginal <= 35L)
            {
              this.txtDatoIngresado.Text = string.Empty;
              this.ViewState["StrDataRaw"] = (object) string.Empty;
              this.InitialDatosVehiculo();
              this.InitialDatosPropietario();
              this.ShowWarningMessage("***ADVERTENCIA***</br>No existe información de la placa ingresada en el servicio web del MTC.");
            }
            else
            {
              LoadingProcess pobjLoadingProcess = new LoadingProcess();
              string strSUNARPString1 = this._strDataRaw.Trim().Replace("a.m.", "AM").Replace("p.m.", "PM");
              List<string> StructureErrors1 = new List<string>();
              this.lstSUNARPVehicle = new ServiceOperation().Mapping(strSUNARPString1, ref StructureErrors1, pobjLoadingProcess);
              string[] strArray1 = strSUNARPString1.Split(new string[1]
              {
                "DATOS PROPIETARIO"
              }, StringSplitOptions.None);
              string strSUNARPString2 = strArray1[0] + "DATOS PROPIETARIO";
              string strSUNARPString3 = "DATOS VEHICULODATOS PROPIETARIO" + strArray1[1];
              List<string> StructureErrors2 = new List<string>();
              new ServiceOperation().Mapping(strSUNARPString2, ref StructureErrors2, pobjLoadingProcess);
              List<string> StructureErrors3 = new List<string>();
              new ServiceOperation().Mapping(strSUNARPString3, ref StructureErrors3, pobjLoadingProcess);
              if (StructureErrors1.Count > 0)
              {
                this.txtDatoIngresado.Text = string.Empty;
                this.ViewState["StrDataRaw"] = (object) string.Empty;
                this.InitialDatosVehiculo();
                this.InitialDatosPropietario();
                if (StructureErrors2.Count > 0 && StructureErrors3.Count > 0)
                {
                  this.ShowWarningMessage(string.Format("***ADVERTENCIA***</br>Se ha detectado <b>{0}</b> registro(s) con estructura incorrecta en los <b>DATOS DEL VEHICULO</b> y <b>{1}</b> registro(s) con estructura incorrecta en los <b>DATOS DEL PROPIETARIO</b>, la información no se puede procesar. Por favor contáctese con el MTC para verificar la información de la placa <b>{2}</b>", (object) StructureErrors2.Count, (object) StructureErrors3.Count, (object) placaNueva));
                }
                else
                {
                  if (StructureErrors2.Count > 0)
                    this.ShowWarningMessage(string.Format("***ADVERTENCIA***</br>Se ha detectado <b>{0}</b> registro(s) con estructura incorrecta en los <b>DATOS DEL VEHICULO</b>, la información no se puede procesar. Por favor contáctese con el MTC para verificar la información de la placa <b>{1}</b>", (object) StructureErrors2.Count, (object) placaNueva));
                  if (StructureErrors3.Count > 0)
                    this.ShowWarningMessage(string.Format("***ADVERTENCIA***</br>Se ha detectado <b>{0}</b> registro(s) con estructura incorrecta en los <b>DATOS DEL PROPIETARIO</b>, la información no se puede procesar. Por favor contáctese con el MTC para verificar la información de la placa <b>{1}</b>", (object) StructureErrors3.Count, (object) placaNueva));
                }
              }
              else
              {
                try
                {
                  this.lblMessage.Visible = false;
                  string[] strArray2 = this._strDataRaw.Split(new string[1]
                  {
                    "\r\n"
                  }, StringSplitOptions.RemoveEmptyEntries);
                  int idv = Array.IndexOf<string>(strArray2, "DATOS VEHICULO");
                  int idp = Array.IndexOf<string>(strArray2, "DATOS PROPIETARIO");
                  this.gridDatosVehiculo.DataSource = (object) this.GetDatosVehiculo(idv, idp, strArray2);
                  this.gridDatosVehiculo.DataBind();
                  this.gridDatosPropietario.DataSource = (object) this.GetDatosPropietario(idp, strArray2);
                  this.gridDatosPropietario.DataBind();
                  this.HidePopup();
                }
                catch (Exception ex)
                {
                  throw new HandledException(5, ex.ToString(), "btnConsultar_Click - ConsumoPlaca.aspx");
                }
              }
            }
          }
          catch (Exception ex)
          {
            this.HidePopup();
            this.lblMessage.Visible = true;
            Message.SetMessage(this.lblMessage, new HandledException(-200, ex));
          }
        }
      }
      catch (Exception ex)
      {
        this.HidePopup();
        this.lblMessage.Visible = true;
        Message.SetMessage(this.lblMessage, new HandledException(-300, ex));
      }
    }

    public void btnReturnPopupConfirmation_Click(object sender, EventArgs e)
    {
      string licencePlate = this.txtDatoIngresado.Text.Trim();
      try
      {
        string strSUNARPString = (this.ViewState["StrDataRaw"] as string).Trim().Replace("a.m.", "AM").Replace("p.m.", "PM");
        LoadingProcess objLoadingProcess = new LoadingProcess();
        if (string.IsNullOrEmpty(strSUNARPString))
          return;
        List<string> StructureErrors = new List<string>();
        this.lstSUNARPVehicle = new ServiceOperation().Mapping(strSUNARPString, ref StructureErrors, objLoadingProcess);
        PlateClaim plateClaim = this.ValidateEnteredLicensePlate(this.lstSUNARPVehicle, licencePlate);
        if (!plateClaim.b_IsClaim)
        {
          UseTypeVerification typeVerification = this.ValidateUseType(this.lstSUNARPVehicle);
          if (typeVerification.b_ValidUseType)
          {
            int iVehicleId = 0;
            string vPlaca = "";
            ServiceOperation.InsertData(this.lstSUNARPVehicle, ref objLoadingProcess, ref iVehicleId, ref vPlaca);
            this.txtDatoIngresado.Text = string.Empty;
            this.txtDatoIngresado.Enabled = true;
            this.ViewState["StrDataRaw"] = (object) string.Empty;
            this.InitialDatosVehiculo();
            this.InitialDatosPropietario();
            this.lblMessage.Visible = true;
            Message.SetMessage(this.lblMessage, enmMessageType.Success, "***ÉXITO***</br>Los registros de la placa <b>" + licencePlate + "</b> se han almacenado exitosamente en el sistema.");
            this.HidePopup();
          }
          else
          {
            this.CleanForm();
            this.ShowWarningMessage("***ADVERTENCIA***</br>El tipo de uso no existe para la placa <b>" + typeVerification.v_Plate + "</b> con título: <b>" + typeVerification.v_Title + "</b>. Por favor contáctese con el MTC para verificar la información de la placa <b>" + typeVerification.v_Plate + "</b>");
          }
        }
        else
        {
          this.CleanForm();
          this.ShowWarningMessage("***ADVERTENCIA***</br>Se generó el reclamo <b>" + plateClaim.v_ClaimCode + "</b>  ya que, la información proporcionada por el MTC no coincide con el nro. de Placa ingresada");
        }
      }
      catch (Exception ex)
      {
        throw new HandledException(5, ex.ToString(), "btnReturnPopupConfirmation_Click - ConsumoPlaca.aspx");
      }
    }

    private PlateClaim ValidateEnteredLicensePlate(
      List<SUNARPVehicle> lstSUNARPVehicle,
      string licencePlate)
    {
      PlateClaim plateClaim = new PlateClaim();
      foreach (SUNARPVehicle sunarpVehicle in lstSUNARPVehicle)
      {
        if (sunarpVehicle.v_PlateNew == licencePlate)
        {
          plateClaim.b_IsClaim = false;
        }
        else
        {
          string logicalInconsistencies = this.GenerateClaim_LogicalInconsistencies(licencePlate);
          plateClaim.b_IsClaim = true;
          plateClaim.v_ClaimCode = logicalInconsistencies;
          return plateClaim;
        }
      }
      return plateClaim;
    }

    private UseTypeVerification ValidateUseType(List<SUNARPVehicle> lstSUNARPVehicle)
    {
      UseTypeVerification typeVerification = new UseTypeVerification();
      DataTable all = new DistributionPlateNumberQueriesBL().GetAll();
      foreach (SUNARPVehicle sunarpVehicle in lstSUNARPVehicle)
      {
        string filterExpression1 = "v_SUNARPUseTypeId = '" + sunarpVehicle.v_UseType + "' AND v_RegistryZoneId = '" + sunarpVehicle.v_RegistryZoneId + "'";
        DataRow[] dataRowArray = all.Select(filterExpression1);
        if (dataRowArray.Length == 0)
        {
          string filterExpression2 = "v_SUNARPUseTypeId = '" + sunarpVehicle.v_UseType + "'";
          dataRowArray = all.Select(filterExpression2);
        }
        if (dataRowArray.Length == 0)
        {
          typeVerification.b_ValidUseType = false;
          typeVerification.v_Plate = sunarpVehicle.v_PlateNew;
          typeVerification.v_Title = sunarpVehicle.v_TitleNumber;
          return typeVerification;
        }
        typeVerification.b_ValidUseType = true;
      }
      return typeVerification;
    }

    private void CreatePopUpServer(
      string pstrtitle,
      string pstrUrl,
      string pstrWidth,
      string pstrHeight)
    {
      string script = string.Format((IFormatProvider) CultureInfo.CurrentCulture, "HideModalPopup1('pload'); OpenPopUp('{0}','{1}','{2}','{3}');", (object) pstrUrl, (object) pstrtitle, (object) pstrWidth, (object) pstrHeight);
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    private void HidePopup()
    {
      string script = "HideModalPopup1('pload');";
      System.Web.UI.ScriptManager.RegisterStartupScript((Control) this.UpdatePanel1, this.UpdatePanel1.GetType(), "Script", script, true);
    }

    public void InitialDatosVehiculo()
    {
      this.gridDatosVehiculo.DataSource = (object) new List<DatosVehiculo>()
      {
        new DatosVehiculo()
        {
          v_TitleNumber = "",
          v_PlateNew = "",
          v_PlateOld = "",
          v_PlateMotive = "",
          v_Category = "",
          v_Brand = "",
          v_Model = "",
          v_SerialNumber = "",
          v_UseType = "",
          v_RegistryZoneId = "",
          v_RegistryOfficeId = "",
          d_DispatchDate = "",
          i_MTCVehicleId = ""
        }
      };
      this.gridDatosVehiculo.DataBind();
    }

    public void InitialDatosPropietario()
    {
      this.gridDatosPropietario.DataSource = (object) new List<DatosPropietario>()
      {
        new DatosPropietario()
        {
          v_TitleNumber = "",
          v_CompanyName = "",
          v_LastName1 = "",
          v_LastName2 = "",
          v_Name = "",
          v_DocumentType = "",
          v_DocumentNumber = "",
          v_RegistryZoneId = "",
          v_RegistryOfficeId = "",
          v_Address = "",
          i_MTCOwnerId = "",
          i_MTCVehicleId = ""
        }
      };
      this.gridDatosPropietario.DataBind();
    }

    public List<DatosVehiculo> GetDatosVehiculo(int idv, int idp, string[] formatData)
    {
      List<DatosVehiculo> datosVehiculo = new List<DatosVehiculo>();
      for (int index = idv + 1; index < idp; ++index)
      {
        string[] strArray = formatData[index].Split(new string[1]
        {
          ","
        }, StringSplitOptions.RemoveEmptyEntries);
        datosVehiculo.Add(new DatosVehiculo()
        {
          v_TitleNumber = strArray[0],
          v_PlateNew = strArray[1],
          v_PlateOld = strArray[2],
          v_PlateMotive = this.GetTipoTratime(strArray[3]),
          v_Category = this.GetCategoria(strArray[4]),
          v_Brand = strArray[5],
          v_Model = strArray[6],
          v_SerialNumber = strArray[7],
          v_UseType = this.GetTipoUso(strArray[8]),
          v_RegistryZoneId = this.GetZonaRegistral(strArray[9]),
          v_RegistryOfficeId = this.GetOficinaRegistral(strArray[10], strArray[9]),
          d_DispatchDate = strArray[11],
          i_MTCVehicleId = strArray[12].Substring(0, strArray[12].Length - 1)
        });
      }
      this.lblRecordCountDV.Text = datosVehiculo.Count.ToString();
      return datosVehiculo;
    }

    public List<DatosPropietario> GetDatosPropietario(int idp, string[] formatData)
    {
      List<DatosPropietario> datosPropietario = new List<DatosPropietario>();
      for (int index = idp + 1; index < formatData.Length; ++index)
      {
        string[] strArray = formatData[index].Split(new string[1]
        {
          ","
        }, StringSplitOptions.RemoveEmptyEntries);
        datosPropietario.Add(new DatosPropietario()
        {
          v_TitleNumber = strArray[0],
          v_CompanyName = strArray[1],
          v_LastName1 = strArray[2],
          v_LastName2 = strArray[3],
          v_Name = strArray[4],
          v_DocumentType = this.GetTipoDocumento(strArray[5]),
          v_DocumentNumber = strArray[6].Trim(),
          v_RegistryZoneId = this.GetZonaRegistral(strArray[7]),
          v_RegistryOfficeId = this.GetOficinaRegistral(strArray[8], strArray[7]),
          v_Address = strArray[9],
          i_MTCOwnerId = strArray[11],
          i_MTCVehicleId = strArray[12].Substring(0, strArray[12].Length - 1)
        });
      }
      this.lblRecordCountDO.Text = datosPropietario.Count.ToString();
      return datosPropietario;
    }

    public string GetTipoTratime(string v_PlateMotive)
    {
      if (!int.TryParse(v_PlateMotive, out int _))
      {
        switch (v_PlateMotive)
        {
          case "29":
            v_PlateMotive = "01";
            break;
          case "11":
            v_PlateMotive = "01";
            break;
          case "CP":
            v_PlateMotive = "03";
            break;
          case "RP":
            v_PlateMotive = "03";
            break;
          case "RA":
            v_PlateMotive = "03";
            break;
          case "99":
            v_PlateMotive = "01";
            break;
          default:
            v_PlateMotive = "123321";
            break;
        }
      }
      string str = "v_OldValue";
      try
      {
        DataRow[] source = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.ProcessType.ToString(),
          (object) "",
          (object) "1",
          (object) "1"
        }).Select(string.Format("{0} = '{1}'", (object) str, (object) Convert.ToInt32(v_PlateMotive)));
        return ((IEnumerable<DataRow>) source).Count<DataRow>() > 0 ? source[0]["v_Description"].ToString().ToUpper() : "NO DETERMINADO";
      }
      catch (Exception ex)
      {
        throw new HandledException(-300, ex.ToString(), "GetTipoTratime - ConsumoPlaca.aspx");
      }
    }

    public string GetTipoUso(string v_UseType)
    {
      string str = "v_OldValue";
      try
      {
        DataRow[] source = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.PublicRecordsUseType.ToString(),
          (object) "",
          (object) "1",
          (object) "1"
        }).Select(str + " = '" + v_UseType + "'");
        return ((IEnumerable<DataRow>) source).Count<DataRow>() > 0 ? source[0]["v_Description"].ToString().ToUpper() : "NO DETERMINADO";
      }
      catch (Exception ex)
      {
        throw new HandledException(-300, ex.ToString(), "GetTipoUso - ConsumoPlaca.aspx");
      }
    }

    public string GetCategoria(string v_Category)
    {
      string str = "v_Description";
      try
      {
        DataRow[] source = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.VehicleCategory.ToString(),
          (object) "",
          (object) "1",
          (object) "1"
        }).Select(str + " = '" + v_Category + "'");
        return ((IEnumerable<DataRow>) source).Count<DataRow>() > 0 ? source[0]["v_Description"].ToString().ToUpper() : "NO DETERMINADO";
      }
      catch (Exception ex)
      {
        throw new HandledException(-300, ex.ToString(), "GetCategoria - ConsumoPlaca.aspx");
      }
    }

    public string GetZonaRegistral(string v_RegistryZoneId)
    {
      string str = "v_OldValue";
      try
      {
        DataRow[] source = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.RegistrationZone.ToString(),
          (object) "",
          (object) "1",
          (object) "1"
        }).Select(str + " = '" + v_RegistryZoneId + "'");
        return ((IEnumerable<DataRow>) source).Count<DataRow>() > 0 ? source[0]["v_Description"].ToString().ToUpper() : "NO DETERMINADO";
      }
      catch (Exception ex)
      {
        throw new HandledException(-300, ex.ToString(), "GetZonaRegistral - ConsumoPlaca.aspx");
      }
    }

    public string GetOficinaRegistral(string v_RegistryOfficeId, string v_RegistryZoneId)
    {
      string str1 = "v_OldValue";
      string str2 = "v_ReferenceId";
      try
      {
        DataRow[] source = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.RegistrationOffice.ToString(),
          (object) "",
          (object) "1",
          (object) "1"
        }).Select(str1 + " = '" + v_RegistryOfficeId + "' AND " + str2 + " = '" + v_RegistryZoneId + "'");
        return ((IEnumerable<DataRow>) source).Count<DataRow>() > 0 ? source[0]["v_Description"].ToString().ToUpper() : "NO DETERMINADO";
      }
      catch (Exception ex)
      {
        throw new HandledException(-300, ex.ToString(), "GetOficinaRegistral - ConsumoPlaca.aspx");
      }
    }

    public string GetTipoDocumento(string v_DocumentType)
    {
      string str = "v_OldValue";
      try
      {
        DataRow[] source = new SystemParameterQueriesBL().GetbyFilter(new ArrayList()
        {
          (object) SystemParameterGroups.PersonDocumentType.ToString(),
          (object) "",
          (object) "1",
          (object) "1"
        }).Select(str + " = '" + v_DocumentType + "'");
        return ((IEnumerable<DataRow>) source).Count<DataRow>() > 0 ? source[0]["v_Description"].ToString().ToUpper() : "NO DETERMINADO";
      }
      catch (Exception ex)
      {
        throw new HandledException(-300, ex.ToString(), "GetTipoDocumento - ConsumoPlaca.aspx");
      }
    }

    private void ShowWarningMessage(string message)
    {
      this.txtDatoIngresado.Text = "";
      this.lblMessage.Visible = true;
      Message.SetMessage(this.lblMessage, enmMessageType.Warning, message);
      this.HidePopup();
    }

    public string GenerateClaim_LogicalInconsistencies(string plate)
    {
      try
      {
        RequirementClaim pobjBE = new RequirementClaim();
        pobjBE.i_ClaimTypeId = new int?(1);
        pobjBE.v_RequestValues = plate;
        pobjBE.v_Comments = "Nomenclatura de la Placa no coincide con lo establecido por la Norma";
        pobjBE.i_Priority = new int?(1);
        pobjBE.i_Status = new int?(1);
        pobjBE.i_InsertUserId = new int?(0);
        new RequirementClaimManagementBL().InsertClaim(ref pobjBE);
        return pobjBE.v_ClaimCode;
      }
      catch (Exception ex)
      {
        throw new HandledException(-300, ex.ToString(), "GenerateClaim_LogicalInconsistencies - ConsumoPlaca.aspx");
      }
    }

    public void CleanForm()
    {
      this.txtDatoIngresado.Text = "";
      this.txtDatoIngresado.Enabled = true;
      this.lblRecordCountDO.Text = "0";
      this.lblRecordCountDV.Text = "0";
      this.InitialDatosVehiculo();
      this.InitialDatosPropietario();
    }
  }
}
