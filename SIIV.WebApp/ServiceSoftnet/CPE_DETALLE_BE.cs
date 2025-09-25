// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.ServiceSoftnet.CPE_DETALLE_BE
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
  public class CPE_DETALLE_BE
  {
    private string nUM_CPEField;
    private string nUM_SEQField;
    private string nUM_LIN_ITEMField;
    private string cOD_UNID_ITEMField;
    private string cANT_UNID_ITEMField;
    private string vAL_VTA_ITEMField;
    private string pRC_VTA_UNIT_ITEMField;
    private string cOD_TIP_PRC_VTAField;
    private string cOD_TRIB_IGV_ITEMField;
    private string pOR_IGV_ITEMField;
    private string mNT_IGV_ITEMField;
    private string cOD_TIP_AFECT_IGV_ITEMField;
    private string cOD_TRIB_ISC_ITEMField;
    private string pOR_ISC_ITEMField;
    private string mNT_ISC_ITEMField;
    private string mNT_ICBPER_ITEMField;
    private string cANT_ICBPER_ITEMField;
    private string iMP_ICBPER_ITEMField;
    private string cOD_TIP_SIST_ISCField;
    private string mNT_OTR_ITEMField;
    private string pOR_OTR_ITEMField;
    private string tXT_DESC_ITEMField;
    private string tXT_DESC_ADIC_ITEMField;
    private string cOD_ITEMField;
    private string vAL_UNIT_ITEMField;
    private string vAL_UNIT_ITEMTempField;
    private string mNT_DSCTO_ITEMField;
    private string mNT_BASE_ISCField;
    private string mNT_BASE_IGVField;
    private string mNT_DETRAC_ITEMField;
    private string mNT_RECGO_ITEMField;
    private string nOM_TRIB_IGV_ITEMField;
    private string cOD_INTER_IGV_ITEMField;
    private string nOM_TRIB_ISC_ITEMField;
    private string cOD_INTER_ISC_ITEMField;
    private string dET_VAL_ADIC01Field;
    private string dET_VAL_ADIC02Field;
    private string dET_VAL_ADIC03Field;
    private string dET_VAL_ADIC04Field;
    private string dET_VAL_ADIC05Field;
    private string dET_VAL_ADIC06Field;
    private string dET_VAL_ADIC07Field;
    private string dET_VAL_ADIC08Field;
    private string dET_VAL_ADIC09Field;
    private string dET_VAL_ADIC10Field;
    private string dET_DES_COMPTAField;
    private string cOD_PROD_SUNATField;
    private string pRECIO_VENTA_X_CANTIDADField;
    private string pRECIO_VENTA_UNITARIO_GRATUITOField;
    private string pRECIO_VENTA_UNITARIO_EXONERADOField;

    public string NUM_CPE
    {
      get => this.nUM_CPEField;
      set => this.nUM_CPEField = value;
    }

    public string NUM_SEQ
    {
      get => this.nUM_SEQField;
      set => this.nUM_SEQField = value;
    }

    public string NUM_LIN_ITEM
    {
      get => this.nUM_LIN_ITEMField;
      set => this.nUM_LIN_ITEMField = value;
    }

    public string COD_UNID_ITEM
    {
      get => this.cOD_UNID_ITEMField;
      set => this.cOD_UNID_ITEMField = value;
    }

    public string CANT_UNID_ITEM
    {
      get => this.cANT_UNID_ITEMField;
      set => this.cANT_UNID_ITEMField = value;
    }

    public string VAL_VTA_ITEM
    {
      get => this.vAL_VTA_ITEMField;
      set => this.vAL_VTA_ITEMField = value;
    }

    public string PRC_VTA_UNIT_ITEM
    {
      get => this.pRC_VTA_UNIT_ITEMField;
      set => this.pRC_VTA_UNIT_ITEMField = value;
    }

    public string COD_TIP_PRC_VTA
    {
      get => this.cOD_TIP_PRC_VTAField;
      set => this.cOD_TIP_PRC_VTAField = value;
    }

    public string COD_TRIB_IGV_ITEM
    {
      get => this.cOD_TRIB_IGV_ITEMField;
      set => this.cOD_TRIB_IGV_ITEMField = value;
    }

    public string POR_IGV_ITEM
    {
      get => this.pOR_IGV_ITEMField;
      set => this.pOR_IGV_ITEMField = value;
    }

    public string MNT_IGV_ITEM
    {
      get => this.mNT_IGV_ITEMField;
      set => this.mNT_IGV_ITEMField = value;
    }

    public string COD_TIP_AFECT_IGV_ITEM
    {
      get => this.cOD_TIP_AFECT_IGV_ITEMField;
      set => this.cOD_TIP_AFECT_IGV_ITEMField = value;
    }

    public string COD_TRIB_ISC_ITEM
    {
      get => this.cOD_TRIB_ISC_ITEMField;
      set => this.cOD_TRIB_ISC_ITEMField = value;
    }

    public string POR_ISC_ITEM
    {
      get => this.pOR_ISC_ITEMField;
      set => this.pOR_ISC_ITEMField = value;
    }

    public string MNT_ISC_ITEM
    {
      get => this.mNT_ISC_ITEMField;
      set => this.mNT_ISC_ITEMField = value;
    }

    public string MNT_ICBPER_ITEM
    {
      get => this.mNT_ICBPER_ITEMField;
      set => this.mNT_ICBPER_ITEMField = value;
    }

    public string CANT_ICBPER_ITEM
    {
      get => this.cANT_ICBPER_ITEMField;
      set => this.cANT_ICBPER_ITEMField = value;
    }

    public string IMP_ICBPER_ITEM
    {
      get => this.iMP_ICBPER_ITEMField;
      set => this.iMP_ICBPER_ITEMField = value;
    }

    public string COD_TIP_SIST_ISC
    {
      get => this.cOD_TIP_SIST_ISCField;
      set => this.cOD_TIP_SIST_ISCField = value;
    }

    public string MNT_OTR_ITEM
    {
      get => this.mNT_OTR_ITEMField;
      set => this.mNT_OTR_ITEMField = value;
    }

    public string POR_OTR_ITEM
    {
      get => this.pOR_OTR_ITEMField;
      set => this.pOR_OTR_ITEMField = value;
    }

    public string TXT_DESC_ITEM
    {
      get => this.tXT_DESC_ITEMField;
      set => this.tXT_DESC_ITEMField = value;
    }

    public string TXT_DESC_ADIC_ITEM
    {
      get => this.tXT_DESC_ADIC_ITEMField;
      set => this.tXT_DESC_ADIC_ITEMField = value;
    }

    public string COD_ITEM
    {
      get => this.cOD_ITEMField;
      set => this.cOD_ITEMField = value;
    }

    public string VAL_UNIT_ITEM
    {
      get => this.vAL_UNIT_ITEMField;
      set => this.vAL_UNIT_ITEMField = value;
    }

    public string VAL_UNIT_ITEMTemp
    {
      get => this.vAL_UNIT_ITEMTempField;
      set => this.vAL_UNIT_ITEMTempField = value;
    }

    public string MNT_DSCTO_ITEM
    {
      get => this.mNT_DSCTO_ITEMField;
      set => this.mNT_DSCTO_ITEMField = value;
    }

    public string MNT_BASE_ISC
    {
      get => this.mNT_BASE_ISCField;
      set => this.mNT_BASE_ISCField = value;
    }

    public string MNT_BASE_IGV
    {
      get => this.mNT_BASE_IGVField;
      set => this.mNT_BASE_IGVField = value;
    }

    public string MNT_DETRAC_ITEM
    {
      get => this.mNT_DETRAC_ITEMField;
      set => this.mNT_DETRAC_ITEMField = value;
    }

    public string MNT_RECGO_ITEM
    {
      get => this.mNT_RECGO_ITEMField;
      set => this.mNT_RECGO_ITEMField = value;
    }

    public string NOM_TRIB_IGV_ITEM
    {
      get => this.nOM_TRIB_IGV_ITEMField;
      set => this.nOM_TRIB_IGV_ITEMField = value;
    }

    public string COD_INTER_IGV_ITEM
    {
      get => this.cOD_INTER_IGV_ITEMField;
      set => this.cOD_INTER_IGV_ITEMField = value;
    }

    public string NOM_TRIB_ISC_ITEM
    {
      get => this.nOM_TRIB_ISC_ITEMField;
      set => this.nOM_TRIB_ISC_ITEMField = value;
    }

    public string COD_INTER_ISC_ITEM
    {
      get => this.cOD_INTER_ISC_ITEMField;
      set => this.cOD_INTER_ISC_ITEMField = value;
    }

    public string DET_VAL_ADIC01
    {
      get => this.dET_VAL_ADIC01Field;
      set => this.dET_VAL_ADIC01Field = value;
    }

    public string DET_VAL_ADIC02
    {
      get => this.dET_VAL_ADIC02Field;
      set => this.dET_VAL_ADIC02Field = value;
    }

    public string DET_VAL_ADIC03
    {
      get => this.dET_VAL_ADIC03Field;
      set => this.dET_VAL_ADIC03Field = value;
    }

    public string DET_VAL_ADIC04
    {
      get => this.dET_VAL_ADIC04Field;
      set => this.dET_VAL_ADIC04Field = value;
    }

    public string DET_VAL_ADIC05
    {
      get => this.dET_VAL_ADIC05Field;
      set => this.dET_VAL_ADIC05Field = value;
    }

    public string DET_VAL_ADIC06
    {
      get => this.dET_VAL_ADIC06Field;
      set => this.dET_VAL_ADIC06Field = value;
    }

    public string DET_VAL_ADIC07
    {
      get => this.dET_VAL_ADIC07Field;
      set => this.dET_VAL_ADIC07Field = value;
    }

    public string DET_VAL_ADIC08
    {
      get => this.dET_VAL_ADIC08Field;
      set => this.dET_VAL_ADIC08Field = value;
    }

    public string DET_VAL_ADIC09
    {
      get => this.dET_VAL_ADIC09Field;
      set => this.dET_VAL_ADIC09Field = value;
    }

    public string DET_VAL_ADIC10
    {
      get => this.dET_VAL_ADIC10Field;
      set => this.dET_VAL_ADIC10Field = value;
    }

    public string DET_DES_COMPTA
    {
      get => this.dET_DES_COMPTAField;
      set => this.dET_DES_COMPTAField = value;
    }

    public string COD_PROD_SUNAT
    {
      get => this.cOD_PROD_SUNATField;
      set => this.cOD_PROD_SUNATField = value;
    }

    public string PRECIO_VENTA_X_CANTIDAD
    {
      get => this.pRECIO_VENTA_X_CANTIDADField;
      set => this.pRECIO_VENTA_X_CANTIDADField = value;
    }

    public string PRECIO_VENTA_UNITARIO_GRATUITO
    {
      get => this.pRECIO_VENTA_UNITARIO_GRATUITOField;
      set => this.pRECIO_VENTA_UNITARIO_GRATUITOField = value;
    }

    public string PRECIO_VENTA_UNITARIO_EXONERADO
    {
      get => this.pRECIO_VENTA_UNITARIO_EXONERADOField;
      set => this.pRECIO_VENTA_UNITARIO_EXONERADOField = value;
    }
  }
}
