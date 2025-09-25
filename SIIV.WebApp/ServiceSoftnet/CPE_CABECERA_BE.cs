// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.ServiceSoftnet.CPE_CABECERA_BE
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#nullable disable
namespace SIIV.WebApp.ServiceSoftnet
{
  [GeneratedCode("System.Xml", "4.8.3752.0")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://tempuri.org/")]
  [Serializable]
  public class CPE_CABECERA_BE
  {
    private string nUM_CPEField;
    private string cOD_GPO_ECONField;
    private string cOD_TIP_NIF_EMISField;
    private string cOD_SUCURSALField;
    private string nUM_NIF_EMISField;
    private string cOD_TIP_NIF_RECPField;
    private string nUM_NIF_RECPField;
    private string cOD_ESCN_CPEField;
    private string nUM_RESUMField;
    private string cOD_TIP_CPEField;
    private string fEC_EMISField;
    private string nOM_RZN_SOC_EMISField;
    private string nOM_COMER_EMISField;
    private string cOD_UBI_EMISField;
    private string tXT_DMCL_FISC_EMISField;
    private string tXT_URB_EMISField;
    private string tXT_DISTR_EMISField;
    private string tXT_PROV_EMISField;
    private string tXT_DPTO_EMISField;
    private string tXT_PAIS_EMISField;
    private string nUM_SERIE_CPEField;
    private string nUM_CORRE_CPEField;
    private string cOD_TIP_PAGOField;
    private string cOD_FRM_PAGOField;
    private string nOM_RZN_SOC_RECPField;
    private string tXT_DMCL_FISC_RECEPField;
    private string cOD_MNDField;
    private string cOD_IMPRE_DESTField;
    private string cOD_PRCD_CARGAField;
    private string mNT_TOT_GRAVADOField;
    private string mNT_TOT_INAFECTOField;
    private string mNT_TOT_EXONERADOField;
    private string mNT_TOT_GRATUITOField;
    private string mNT_TOT_DESCUENTOField;
    private string mNT_TOT_TRIB_IGVField;
    private string mNT_TOT_TRIB_ISCField;
    private string mNT_TOT_TRIB_OTRField;
    private string mNT_DSCTO_GLOBField;
    private string mNT_TOT_OTR_CGOField;
    private string mNT_TOTField;
    private string tIP_CAMBIOField;
    private string mNT_TOT_GRAVADO_NACField;
    private string mNT_TOT_INAFECTO_NACField;
    private string mNT_TOT_EXONERADO_NACField;
    private string mNT_TOT_GRATUITO_NACField;
    private string mNT_TOT_DESCUENTO_NACField;
    private string mNT_TOT_OTR_CGO_NACField;
    private string mNT_TOT_TRIB_IGV_NACField;
    private string mNT_TOT_TRIB_ISC_NACField;
    private string mNT_TOT_TRIB_OTR_NACField;
    private string mNT_DSCTO_GLOB_NACField;
    private string mNT_TOT_NACField;
    private string mNT_TOT_PERCEPCIONField;
    private string cOD_TIP_PERCEPCIONField;
    private string mNT_IMPTO_PERCEPCIONField;
    private string mNT_TOT_MAS_PERCEPCIONField;
    private string mNT_TOT_DETRACCIONField;
    private string cOD_TIP_DETRACCIONField;
    private string pOR_DETRACCIONField;
    private string cOD_TIP_NCField;
    private string cOD_TIP_NDField;
    private string tXT_DESC_MTVOField;
    private string tXT_VERS_UBLField;
    private string tXT_VERS_ESTRUCT_UBLField;
    private string fLG_IMPR_CPEField;
    private string fLG_PUB_CPEField;
    private string fLG_CORREO_CPEField;
    private string cOD_ESTD_SUNATField;
    private string cOD_ID_RECEP_SUNATField;
    private string fEC_RECEP_DOC_SUNATField;
    private string hOR_RECEP_DOC_SUNATField;
    private string fEC_GENE_DOC_SUNATField;
    private string hOR_GENE_DOC_SUNATField;
    private string cOD_RPTA_ENV_SUNATField;
    private string tXT_CORREO_ENVIOField;
    private string tXT_CORREO_COPIAField;
    private string tXT_CORREO_OCULTOField;
    private string cOD_FORM_IMPRField;
    private string fLG_TIP_CAMBIOField;
    private string fEC_INICIOField;
    private string nUM_INTENTOField;
    private string mSG_PROCESOField;
    private string eST_PROCESOField;
    private string nUM_RESUM_BOLField;
    private string nUM_RESUM_BAJField;
    private string nUM_INT_CORREOField;
    private string cOD_TIP_OPE_SUNATField;
    private string cOD_PTO_VENTAField;
    private string mNT_TOT_ANTCPField;
    private string cOD_UND_NEGField;
    private string mONTO_LETRASField;
    private string flagCondicionResumenField;
    private string dETALLECOUNTField;
    private string hORA_CREACIONField;
    private string fECHAVENCIMIENTOField;
    private string oRDENCOMPRAField;
    private string cOD_OPE_SUNAT_NEWField;
    private string pRECIO_TOTAL_LINEAField;
    private string mONTO_TOT_IGV_GRATField;
    private string mNT_TOT_VAL_VENTField;
    private string mNT_DSCTO_TOTALField;
    private string mNT_REDONDEO_CPEField;
    private string mNT_TOT_BASE_ISCField;
    private string mNT_TOT_TRIBUTOSField;
    private string mNT_TOT_TRIB_ICBPERField;

    public string NUM_CPE
    {
      get => this.nUM_CPEField;
      set => this.nUM_CPEField = value;
    }

    public string COD_GPO_ECON
    {
      get => this.cOD_GPO_ECONField;
      set => this.cOD_GPO_ECONField = value;
    }

    public string COD_TIP_NIF_EMIS
    {
      get => this.cOD_TIP_NIF_EMISField;
      set => this.cOD_TIP_NIF_EMISField = value;
    }

    public string COD_SUCURSAL
    {
      get => this.cOD_SUCURSALField;
      set => this.cOD_SUCURSALField = value;
    }

    public string NUM_NIF_EMIS
    {
      get => this.nUM_NIF_EMISField;
      set => this.nUM_NIF_EMISField = value;
    }

    public string COD_TIP_NIF_RECP
    {
      get => this.cOD_TIP_NIF_RECPField;
      set => this.cOD_TIP_NIF_RECPField = value;
    }

    public string NUM_NIF_RECP
    {
      get => this.nUM_NIF_RECPField;
      set => this.nUM_NIF_RECPField = value;
    }

    public string COD_ESCN_CPE
    {
      get => this.cOD_ESCN_CPEField;
      set => this.cOD_ESCN_CPEField = value;
    }

    public string NUM_RESUM
    {
      get => this.nUM_RESUMField;
      set => this.nUM_RESUMField = value;
    }

    public string COD_TIP_CPE
    {
      get => this.cOD_TIP_CPEField;
      set => this.cOD_TIP_CPEField = value;
    }

    public string FEC_EMIS
    {
      get => this.fEC_EMISField;
      set => this.fEC_EMISField = value;
    }

    public string NOM_RZN_SOC_EMIS
    {
      get => this.nOM_RZN_SOC_EMISField;
      set => this.nOM_RZN_SOC_EMISField = value;
    }

    public string NOM_COMER_EMIS
    {
      get => this.nOM_COMER_EMISField;
      set => this.nOM_COMER_EMISField = value;
    }

    public string COD_UBI_EMIS
    {
      get => this.cOD_UBI_EMISField;
      set => this.cOD_UBI_EMISField = value;
    }

    public string TXT_DMCL_FISC_EMIS
    {
      get => this.tXT_DMCL_FISC_EMISField;
      set => this.tXT_DMCL_FISC_EMISField = value;
    }

    public string TXT_URB_EMIS
    {
      get => this.tXT_URB_EMISField;
      set => this.tXT_URB_EMISField = value;
    }

    public string TXT_DISTR_EMIS
    {
      get => this.tXT_DISTR_EMISField;
      set => this.tXT_DISTR_EMISField = value;
    }

    public string TXT_PROV_EMIS
    {
      get => this.tXT_PROV_EMISField;
      set => this.tXT_PROV_EMISField = value;
    }

    public string TXT_DPTO_EMIS
    {
      get => this.tXT_DPTO_EMISField;
      set => this.tXT_DPTO_EMISField = value;
    }

    public string TXT_PAIS_EMIS
    {
      get => this.tXT_PAIS_EMISField;
      set => this.tXT_PAIS_EMISField = value;
    }

    public string NUM_SERIE_CPE
    {
      get => this.nUM_SERIE_CPEField;
      set => this.nUM_SERIE_CPEField = value;
    }

    public string NUM_CORRE_CPE
    {
      get => this.nUM_CORRE_CPEField;
      set => this.nUM_CORRE_CPEField = value;
    }

    public string COD_TIP_PAGO
    {
      get => this.cOD_TIP_PAGOField;
      set => this.cOD_TIP_PAGOField = value;
    }

    public string COD_FRM_PAGO
    {
      get => this.cOD_FRM_PAGOField;
      set => this.cOD_FRM_PAGOField = value;
    }

    public string NOM_RZN_SOC_RECP
    {
      get => this.nOM_RZN_SOC_RECPField;
      set => this.nOM_RZN_SOC_RECPField = value;
    }

    public string TXT_DMCL_FISC_RECEP
    {
      get => this.tXT_DMCL_FISC_RECEPField;
      set => this.tXT_DMCL_FISC_RECEPField = value;
    }

    public string COD_MND
    {
      get => this.cOD_MNDField;
      set => this.cOD_MNDField = value;
    }

    public string COD_IMPRE_DEST
    {
      get => this.cOD_IMPRE_DESTField;
      set => this.cOD_IMPRE_DESTField = value;
    }

    public string COD_PRCD_CARGA
    {
      get => this.cOD_PRCD_CARGAField;
      set => this.cOD_PRCD_CARGAField = value;
    }

    public string MNT_TOT_GRAVADO
    {
      get => this.mNT_TOT_GRAVADOField;
      set => this.mNT_TOT_GRAVADOField = value;
    }

    public string MNT_TOT_INAFECTO
    {
      get => this.mNT_TOT_INAFECTOField;
      set => this.mNT_TOT_INAFECTOField = value;
    }

    public string MNT_TOT_EXONERADO
    {
      get => this.mNT_TOT_EXONERADOField;
      set => this.mNT_TOT_EXONERADOField = value;
    }

    public string MNT_TOT_GRATUITO
    {
      get => this.mNT_TOT_GRATUITOField;
      set => this.mNT_TOT_GRATUITOField = value;
    }

    public string MNT_TOT_DESCUENTO
    {
      get => this.mNT_TOT_DESCUENTOField;
      set => this.mNT_TOT_DESCUENTOField = value;
    }

    public string MNT_TOT_TRIB_IGV
    {
      get => this.mNT_TOT_TRIB_IGVField;
      set => this.mNT_TOT_TRIB_IGVField = value;
    }

    public string MNT_TOT_TRIB_ISC
    {
      get => this.mNT_TOT_TRIB_ISCField;
      set => this.mNT_TOT_TRIB_ISCField = value;
    }

    public string MNT_TOT_TRIB_OTR
    {
      get => this.mNT_TOT_TRIB_OTRField;
      set => this.mNT_TOT_TRIB_OTRField = value;
    }

    public string MNT_DSCTO_GLOB
    {
      get => this.mNT_DSCTO_GLOBField;
      set => this.mNT_DSCTO_GLOBField = value;
    }

    public string MNT_TOT_OTR_CGO
    {
      get => this.mNT_TOT_OTR_CGOField;
      set => this.mNT_TOT_OTR_CGOField = value;
    }

    public string MNT_TOT
    {
      get => this.mNT_TOTField;
      set => this.mNT_TOTField = value;
    }

    public string TIP_CAMBIO
    {
      get => this.tIP_CAMBIOField;
      set => this.tIP_CAMBIOField = value;
    }

    public string MNT_TOT_GRAVADO_NAC
    {
      get => this.mNT_TOT_GRAVADO_NACField;
      set => this.mNT_TOT_GRAVADO_NACField = value;
    }

    public string MNT_TOT_INAFECTO_NAC
    {
      get => this.mNT_TOT_INAFECTO_NACField;
      set => this.mNT_TOT_INAFECTO_NACField = value;
    }

    public string MNT_TOT_EXONERADO_NAC
    {
      get => this.mNT_TOT_EXONERADO_NACField;
      set => this.mNT_TOT_EXONERADO_NACField = value;
    }

    public string MNT_TOT_GRATUITO_NAC
    {
      get => this.mNT_TOT_GRATUITO_NACField;
      set => this.mNT_TOT_GRATUITO_NACField = value;
    }

    public string MNT_TOT_DESCUENTO_NAC
    {
      get => this.mNT_TOT_DESCUENTO_NACField;
      set => this.mNT_TOT_DESCUENTO_NACField = value;
    }

    public string MNT_TOT_OTR_CGO_NAC
    {
      get => this.mNT_TOT_OTR_CGO_NACField;
      set => this.mNT_TOT_OTR_CGO_NACField = value;
    }

    public string MNT_TOT_TRIB_IGV_NAC
    {
      get => this.mNT_TOT_TRIB_IGV_NACField;
      set => this.mNT_TOT_TRIB_IGV_NACField = value;
    }

    public string MNT_TOT_TRIB_ISC_NAC
    {
      get => this.mNT_TOT_TRIB_ISC_NACField;
      set => this.mNT_TOT_TRIB_ISC_NACField = value;
    }

    public string MNT_TOT_TRIB_OTR_NAC
    {
      get => this.mNT_TOT_TRIB_OTR_NACField;
      set => this.mNT_TOT_TRIB_OTR_NACField = value;
    }

    public string MNT_DSCTO_GLOB_NAC
    {
      get => this.mNT_DSCTO_GLOB_NACField;
      set => this.mNT_DSCTO_GLOB_NACField = value;
    }

    public string MNT_TOT_NAC
    {
      get => this.mNT_TOT_NACField;
      set => this.mNT_TOT_NACField = value;
    }

    public string MNT_TOT_PERCEPCION
    {
      get => this.mNT_TOT_PERCEPCIONField;
      set => this.mNT_TOT_PERCEPCIONField = value;
    }

    public string COD_TIP_PERCEPCION
    {
      get => this.cOD_TIP_PERCEPCIONField;
      set => this.cOD_TIP_PERCEPCIONField = value;
    }

    public string MNT_IMPTO_PERCEPCION
    {
      get => this.mNT_IMPTO_PERCEPCIONField;
      set => this.mNT_IMPTO_PERCEPCIONField = value;
    }

    public string MNT_TOT_MAS_PERCEPCION
    {
      get => this.mNT_TOT_MAS_PERCEPCIONField;
      set => this.mNT_TOT_MAS_PERCEPCIONField = value;
    }

    public string MNT_TOT_DETRACCION
    {
      get => this.mNT_TOT_DETRACCIONField;
      set => this.mNT_TOT_DETRACCIONField = value;
    }

    public string COD_TIP_DETRACCION
    {
      get => this.cOD_TIP_DETRACCIONField;
      set => this.cOD_TIP_DETRACCIONField = value;
    }

    public string POR_DETRACCION
    {
      get => this.pOR_DETRACCIONField;
      set => this.pOR_DETRACCIONField = value;
    }

    public string COD_TIP_NC
    {
      get => this.cOD_TIP_NCField;
      set => this.cOD_TIP_NCField = value;
    }

    public string COD_TIP_ND
    {
      get => this.cOD_TIP_NDField;
      set => this.cOD_TIP_NDField = value;
    }

    public string TXT_DESC_MTVO
    {
      get => this.tXT_DESC_MTVOField;
      set => this.tXT_DESC_MTVOField = value;
    }

    public string TXT_VERS_UBL
    {
      get => this.tXT_VERS_UBLField;
      set => this.tXT_VERS_UBLField = value;
    }

    public string TXT_VERS_ESTRUCT_UBL
    {
      get => this.tXT_VERS_ESTRUCT_UBLField;
      set => this.tXT_VERS_ESTRUCT_UBLField = value;
    }

    public string FLG_IMPR_CPE
    {
      get => this.fLG_IMPR_CPEField;
      set => this.fLG_IMPR_CPEField = value;
    }

    public string FLG_PUB_CPE
    {
      get => this.fLG_PUB_CPEField;
      set => this.fLG_PUB_CPEField = value;
    }

    public string FLG_CORREO_CPE
    {
      get => this.fLG_CORREO_CPEField;
      set => this.fLG_CORREO_CPEField = value;
    }

    public string COD_ESTD_SUNAT
    {
      get => this.cOD_ESTD_SUNATField;
      set => this.cOD_ESTD_SUNATField = value;
    }

    public string COD_ID_RECEP_SUNAT
    {
      get => this.cOD_ID_RECEP_SUNATField;
      set => this.cOD_ID_RECEP_SUNATField = value;
    }

    public string FEC_RECEP_DOC_SUNAT
    {
      get => this.fEC_RECEP_DOC_SUNATField;
      set => this.fEC_RECEP_DOC_SUNATField = value;
    }

    public string HOR_RECEP_DOC_SUNAT
    {
      get => this.hOR_RECEP_DOC_SUNATField;
      set => this.hOR_RECEP_DOC_SUNATField = value;
    }

    public string FEC_GENE_DOC_SUNAT
    {
      get => this.fEC_GENE_DOC_SUNATField;
      set => this.fEC_GENE_DOC_SUNATField = value;
    }

    public string HOR_GENE_DOC_SUNAT
    {
      get => this.hOR_GENE_DOC_SUNATField;
      set => this.hOR_GENE_DOC_SUNATField = value;
    }

    public string COD_RPTA_ENV_SUNAT
    {
      get => this.cOD_RPTA_ENV_SUNATField;
      set => this.cOD_RPTA_ENV_SUNATField = value;
    }

    public string TXT_CORREO_ENVIO
    {
      get => this.tXT_CORREO_ENVIOField;
      set => this.tXT_CORREO_ENVIOField = value;
    }

    public string TXT_CORREO_COPIA
    {
      get => this.tXT_CORREO_COPIAField;
      set => this.tXT_CORREO_COPIAField = value;
    }

    public string TXT_CORREO_OCULTO
    {
      get => this.tXT_CORREO_OCULTOField;
      set => this.tXT_CORREO_OCULTOField = value;
    }

    public string COD_FORM_IMPR
    {
      get => this.cOD_FORM_IMPRField;
      set => this.cOD_FORM_IMPRField = value;
    }

    public string FLG_TIP_CAMBIO
    {
      get => this.fLG_TIP_CAMBIOField;
      set => this.fLG_TIP_CAMBIOField = value;
    }

    public string FEC_INICIO
    {
      get => this.fEC_INICIOField;
      set => this.fEC_INICIOField = value;
    }

    public string NUM_INTENTO
    {
      get => this.nUM_INTENTOField;
      set => this.nUM_INTENTOField = value;
    }

    public string MSG_PROCESO
    {
      get => this.mSG_PROCESOField;
      set => this.mSG_PROCESOField = value;
    }

    public string EST_PROCESO
    {
      get => this.eST_PROCESOField;
      set => this.eST_PROCESOField = value;
    }

    public string NUM_RESUM_BOL
    {
      get => this.nUM_RESUM_BOLField;
      set => this.nUM_RESUM_BOLField = value;
    }

    public string NUM_RESUM_BAJ
    {
      get => this.nUM_RESUM_BAJField;
      set => this.nUM_RESUM_BAJField = value;
    }

    public string NUM_INT_CORREO
    {
      get => this.nUM_INT_CORREOField;
      set => this.nUM_INT_CORREOField = value;
    }

    public string COD_TIP_OPE_SUNAT
    {
      get => this.cOD_TIP_OPE_SUNATField;
      set => this.cOD_TIP_OPE_SUNATField = value;
    }

    public string COD_PTO_VENTA
    {
      get => this.cOD_PTO_VENTAField;
      set => this.cOD_PTO_VENTAField = value;
    }

    public string MNT_TOT_ANTCP
    {
      get => this.mNT_TOT_ANTCPField;
      set => this.mNT_TOT_ANTCPField = value;
    }

    public string COD_UND_NEG
    {
      get => this.cOD_UND_NEGField;
      set => this.cOD_UND_NEGField = value;
    }

    public string MONTO_LETRAS
    {
      get => this.mONTO_LETRASField;
      set => this.mONTO_LETRASField = value;
    }

    public string FlagCondicionResumen
    {
      get => this.flagCondicionResumenField;
      set => this.flagCondicionResumenField = value;
    }

    public string DETALLECOUNT
    {
      get => this.dETALLECOUNTField;
      set => this.dETALLECOUNTField = value;
    }

    public string HORA_CREACION
    {
      get => this.hORA_CREACIONField;
      set => this.hORA_CREACIONField = value;
    }

    public string FECHAVENCIMIENTO
    {
      get => this.fECHAVENCIMIENTOField;
      set => this.fECHAVENCIMIENTOField = value;
    }

    public string ORDENCOMPRA
    {
      get => this.oRDENCOMPRAField;
      set => this.oRDENCOMPRAField = value;
    }

    public string COD_OPE_SUNAT_NEW
    {
      get => this.cOD_OPE_SUNAT_NEWField;
      set => this.cOD_OPE_SUNAT_NEWField = value;
    }

    public string PRECIO_TOTAL_LINEA
    {
      get => this.pRECIO_TOTAL_LINEAField;
      set => this.pRECIO_TOTAL_LINEAField = value;
    }

    public string MONTO_TOT_IGV_GRAT
    {
      get => this.mONTO_TOT_IGV_GRATField;
      set => this.mONTO_TOT_IGV_GRATField = value;
    }

    public string MNT_TOT_VAL_VENT
    {
      get => this.mNT_TOT_VAL_VENTField;
      set => this.mNT_TOT_VAL_VENTField = value;
    }

    public string MNT_DSCTO_TOTAL
    {
      get => this.mNT_DSCTO_TOTALField;
      set => this.mNT_DSCTO_TOTALField = value;
    }

    public string MNT_REDONDEO_CPE
    {
      get => this.mNT_REDONDEO_CPEField;
      set => this.mNT_REDONDEO_CPEField = value;
    }

    public string MNT_TOT_BASE_ISC
    {
      get => this.mNT_TOT_BASE_ISCField;
      set => this.mNT_TOT_BASE_ISCField = value;
    }

    public string MNT_TOT_TRIBUTOS
    {
      get => this.mNT_TOT_TRIBUTOSField;
      set => this.mNT_TOT_TRIBUTOSField = value;
    }

    public string MNT_TOT_TRIB_ICBPER
    {
      get => this.mNT_TOT_TRIB_ICBPERField;
      set => this.mNT_TOT_TRIB_ICBPERField = value;
    }
  }
}
