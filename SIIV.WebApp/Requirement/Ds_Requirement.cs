// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Requirement.Ds_Requirement
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7AA326CF-267A-4A0D-8292-415B064D285A
// Assembly location: D:\SGPR\BK-Placas\Placas\bin\SIIV.WebApp.dll

using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

#nullable disable
namespace SIIV.WebApp.Requirement
{
  [DesignerCategory("code")]
  [ToolboxItem(true)]
  [XmlSchemaProvider("GetTypedDataSetSchema")]
  [XmlRoot("Ds_Requirement")]
  [HelpKeyword("vs.data.DataSet")]
  [Serializable]
  public class Ds_Requirement : DataSet
  {
    private Ds_Requirement.usp_RequirementGenerateCurDataTable tableusp_RequirementGenerateCur;
    private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Ds_Requirement()
    {
      this.BeginInit();
      this.InitClass();
      CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
      base.Tables.CollectionChanged += changeEventHandler;
      base.Relations.CollectionChanged += changeEventHandler;
      this.EndInit();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected Ds_Requirement(SerializationInfo info, StreamingContext context)
      : base(info, context, false)
    {
      if (this.IsBinarySerialized(info, context))
      {
        this.InitVars(false);
        CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
        this.Tables.CollectionChanged += changeEventHandler;
        this.Relations.CollectionChanged += changeEventHandler;
      }
      else
      {
        string s = (string) info.GetValue("XmlSchema", typeof (string));
        if (this.DetermineSchemaSerializationMode(info, context) == SchemaSerializationMode.IncludeSchema)
        {
          DataSet dataSet = new DataSet();
          dataSet.ReadXmlSchema((XmlReader) new XmlTextReader((TextReader) new StringReader(s)));
          if (dataSet.Tables[nameof (usp_RequirementGenerateCur)] != null)
            base.Tables.Add((DataTable) new Ds_Requirement.usp_RequirementGenerateCurDataTable(dataSet.Tables[nameof (usp_RequirementGenerateCur)]));
          this.DataSetName = dataSet.DataSetName;
          this.Prefix = dataSet.Prefix;
          this.Namespace = dataSet.Namespace;
          this.Locale = dataSet.Locale;
          this.CaseSensitive = dataSet.CaseSensitive;
          this.EnforceConstraints = dataSet.EnforceConstraints;
          this.Merge(dataSet, false, MissingSchemaAction.Add);
          this.InitVars();
        }
        else
          this.ReadXmlSchema((XmlReader) new XmlTextReader((TextReader) new StringReader(s)));
        this.GetSerializationData(info, context);
        CollectionChangeEventHandler changeEventHandler = new CollectionChangeEventHandler(this.SchemaChanged);
        base.Tables.CollectionChanged += changeEventHandler;
        this.Relations.CollectionChanged += changeEventHandler;
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public Ds_Requirement.usp_RequirementGenerateCurDataTable usp_RequirementGenerateCur
    {
      get => this.tableusp_RequirementGenerateCur;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override SchemaSerializationMode SchemaSerializationMode
    {
      get => this._schemaSerializationMode;
      set => this._schemaSerializationMode = value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataTableCollection Tables => base.Tables;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new DataRelationCollection Relations => base.Relations;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override void InitializeDerivedDataSet()
    {
      this.BeginInit();
      this.InitClass();
      this.EndInit();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public override DataSet Clone()
    {
      Ds_Requirement dsRequirement = (Ds_Requirement) base.Clone();
      dsRequirement.InitVars();
      dsRequirement.SchemaSerializationMode = this.SchemaSerializationMode;
      return (DataSet) dsRequirement;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override bool ShouldSerializeTables() => false;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override bool ShouldSerializeRelations() => false;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override void ReadXmlSerializable(XmlReader reader)
    {
      if (this.DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema)
      {
        this.Reset();
        DataSet dataSet = new DataSet();
        int num = (int) dataSet.ReadXml(reader);
        if (dataSet.Tables["usp_RequirementGenerateCur"] != null)
          base.Tables.Add((DataTable) new Ds_Requirement.usp_RequirementGenerateCurDataTable(dataSet.Tables["usp_RequirementGenerateCur"]));
        this.DataSetName = dataSet.DataSetName;
        this.Prefix = dataSet.Prefix;
        this.Namespace = dataSet.Namespace;
        this.Locale = dataSet.Locale;
        this.CaseSensitive = dataSet.CaseSensitive;
        this.EnforceConstraints = dataSet.EnforceConstraints;
        this.Merge(dataSet, false, MissingSchemaAction.Add);
        this.InitVars();
      }
      else
      {
        int num = (int) this.ReadXml(reader);
        this.InitVars();
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected override XmlSchema GetSchemaSerializable()
    {
      MemoryStream memoryStream = new MemoryStream();
      this.WriteXmlSchema((XmlWriter) new XmlTextWriter((Stream) memoryStream, (Encoding) null));
      memoryStream.Position = 0L;
      return XmlSchema.Read((XmlReader) new XmlTextReader((Stream) memoryStream), (ValidationEventHandler) null);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    internal void InitVars() => this.InitVars(true);

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    internal void InitVars(bool initTable)
    {
      this.tableusp_RequirementGenerateCur = (Ds_Requirement.usp_RequirementGenerateCurDataTable) base.Tables["usp_RequirementGenerateCur"];
      if (!initTable || this.tableusp_RequirementGenerateCur == null)
        return;
      this.tableusp_RequirementGenerateCur.InitVars();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void InitClass()
    {
      this.DataSetName = nameof (Ds_Requirement);
      this.Prefix = "";
      this.Namespace = "http://tempuri.org/Ds_Requirement.xsd";
      this.EnforceConstraints = true;
      this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
      this.tableusp_RequirementGenerateCur = new Ds_Requirement.usp_RequirementGenerateCurDataTable();
      base.Tables.Add((DataTable) this.tableusp_RequirementGenerateCur);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private bool ShouldSerializeusp_RequirementGenerateCur() => false;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void SchemaChanged(object sender, CollectionChangeEventArgs e)
    {
      if (e.Action != CollectionChangeAction.Remove)
        return;
      this.InitVars();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
    {
      Ds_Requirement dsRequirement = new Ds_Requirement();
      XmlSchemaComplexType typedDataSetSchema = new XmlSchemaComplexType();
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      xmlSchemaSequence.Items.Add((XmlSchemaObject) new XmlSchemaAny()
      {
        Namespace = dsRequirement.Namespace
      });
      typedDataSetSchema.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      XmlSchema schemaSerializable = dsRequirement.GetSchemaSerializable();
      if (xs.Contains(schemaSerializable.TargetNamespace))
      {
        MemoryStream memoryStream1 = new MemoryStream();
        MemoryStream memoryStream2 = new MemoryStream();
        try
        {
          schemaSerializable.Write((Stream) memoryStream1);
          IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
          while (enumerator.MoveNext())
          {
            XmlSchema current = (XmlSchema) enumerator.Current;
            memoryStream2.SetLength(0L);
            current.Write((Stream) memoryStream2);
            if (memoryStream1.Length == memoryStream2.Length)
            {
              memoryStream1.Position = 0L;
              memoryStream2.Position = 0L;
              do
                ;
              while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
              if (memoryStream1.Position == memoryStream1.Length)
                return typedDataSetSchema;
            }
          }
        }
        finally
        {
          memoryStream1?.Close();
          memoryStream2?.Close();
        }
      }
      xs.Add(schemaSerializable);
      return typedDataSetSchema;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public delegate void usp_RequirementGenerateCurRowChangeEventHandler(
      object sender,
      Ds_Requirement.usp_RequirementGenerateCurRowChangeEvent e);

    [XmlSchemaProvider("GetTypedTableSchema")]
    [Serializable]
    public class usp_RequirementGenerateCurDataTable : 
      TypedTableBase<Ds_Requirement.usp_RequirementGenerateCurRow>
    {
      private DataColumn columnv_PlateNew;
      private DataColumn columnv_PlateOld;
      private DataColumn columni_RequirementPlateId;
      private DataColumn columni_RequirementId;
      private DataColumn columnRegisterDate;
      private DataColumn columnRegisterTime;
      private DataColumn columnv_OwnerCompleteName;
      private DataColumn columnv_OwnerDocumentType;
      private DataColumn columnv_OwnerDocumentNumber;
      private DataColumn columni_LocationId;
      private DataColumn columnLocationDescription;
      private DataColumn columnLocationAddress;
      private DataColumn columnv_Brand;
      private DataColumn columnv_Model;
      private DataColumn columnv_SerialNumber;
      private DataColumn columnv_Code;
      private DataColumn columnProductDescription;
      private DataColumn columnUseTypeDescription;
      private DataColumn columnv_AttentionSchedule;
      private DataColumn columnProofPaymentDescription;
      private DataColumn columnBeneficiaryCompleteName;
      private DataColumn columnBeneficiaryDocumentDescription;
      private DataColumn columnBeneficiaryDocumentNumber;
      private DataColumn columnbeneficiaryAddress;
      private DataColumn columnRequesterCompleteName;
      private DataColumn columni_ProcessTypeId;
      private DataColumn columnProcessTypeDescription;
      private DataColumn columnv_OwnerDocumentDescription;
      private DataColumn columni_PaymentId;
      private DataColumn columnv_PaymentCode;
      private DataColumn columnRequisite;
      private DataColumn columnImageBarPlate;
      private DataColumn columnImageBarCode;
      private DataColumn columnv_ExpirationDate;
      private DataColumn columni_PlateTypeId;
      private DataColumn columnf_PriceTotal;
      private DataColumn columnv_TypeApplicant;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public usp_RequirementGenerateCurDataTable()
      {
        this.TableName = "usp_RequirementGenerateCur";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      internal usp_RequirementGenerateCurDataTable(DataTable table)
      {
        this.TableName = table.TableName;
        if (table.CaseSensitive != table.DataSet.CaseSensitive)
          this.CaseSensitive = table.CaseSensitive;
        if (table.Locale.ToString() != table.DataSet.Locale.ToString())
          this.Locale = table.Locale;
        if (table.Namespace != table.DataSet.Namespace)
          this.Namespace = table.Namespace;
        this.Prefix = table.Prefix;
        this.MinimumCapacity = table.MinimumCapacity;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected usp_RequirementGenerateCurDataTable(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn v_PlateNewColumn => this.columnv_PlateNew;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn v_PlateOldColumn => this.columnv_PlateOld;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn i_RequirementPlateIdColumn => this.columni_RequirementPlateId;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn i_RequirementIdColumn => this.columni_RequirementId;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn RegisterDateColumn => this.columnRegisterDate;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn RegisterTimeColumn => this.columnRegisterTime;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn v_OwnerCompleteNameColumn => this.columnv_OwnerCompleteName;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn v_OwnerDocumentTypeColumn => this.columnv_OwnerDocumentType;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn v_OwnerDocumentNumberColumn => this.columnv_OwnerDocumentNumber;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn i_LocationIdColumn => this.columni_LocationId;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn LocationDescriptionColumn => this.columnLocationDescription;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn LocationAddressColumn => this.columnLocationAddress;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn v_BrandColumn => this.columnv_Brand;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn v_ModelColumn => this.columnv_Model;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn v_SerialNumberColumn => this.columnv_SerialNumber;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn v_CodeColumn => this.columnv_Code;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn ProductDescriptionColumn => this.columnProductDescription;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn UseTypeDescriptionColumn => this.columnUseTypeDescription;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn v_AttentionScheduleColumn => this.columnv_AttentionSchedule;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn ProofPaymentDescriptionColumn => this.columnProofPaymentDescription;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn BeneficiaryCompleteNameColumn => this.columnBeneficiaryCompleteName;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn BeneficiaryDocumentDescriptionColumn
      {
        get => this.columnBeneficiaryDocumentDescription;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn BeneficiaryDocumentNumberColumn => this.columnBeneficiaryDocumentNumber;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn beneficiaryAddressColumn => this.columnbeneficiaryAddress;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn RequesterCompleteNameColumn => this.columnRequesterCompleteName;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn i_ProcessTypeIdColumn => this.columni_ProcessTypeId;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn ProcessTypeDescriptionColumn => this.columnProcessTypeDescription;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn v_OwnerDocumentDescriptionColumn => this.columnv_OwnerDocumentDescription;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn i_PaymentIdColumn => this.columni_PaymentId;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn v_PaymentCodeColumn => this.columnv_PaymentCode;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn RequisiteColumn => this.columnRequisite;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn ImageBarPlateColumn => this.columnImageBarPlate;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn ImageBarCodeColumn => this.columnImageBarCode;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn v_ExpirationDateColumn => this.columnv_ExpirationDate;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn i_PlateTypeIdColumn => this.columni_PlateTypeId;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn f_PriceTotalColumn => this.columnf_PriceTotal;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn v_TypeApplicantColumn => this.columnv_TypeApplicant;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [Browsable(false)]
      public int Count => this.Rows.Count;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public Ds_Requirement.usp_RequirementGenerateCurRow this[int index]
      {
        get => (Ds_Requirement.usp_RequirementGenerateCurRow) this.Rows[index];
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event Ds_Requirement.usp_RequirementGenerateCurRowChangeEventHandler usp_RequirementGenerateCurRowChanging;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event Ds_Requirement.usp_RequirementGenerateCurRowChangeEventHandler usp_RequirementGenerateCurRowChanged;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event Ds_Requirement.usp_RequirementGenerateCurRowChangeEventHandler usp_RequirementGenerateCurRowDeleting;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event Ds_Requirement.usp_RequirementGenerateCurRowChangeEventHandler usp_RequirementGenerateCurRowDeleted;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Addusp_RequirementGenerateCurRow(Ds_Requirement.usp_RequirementGenerateCurRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public Ds_Requirement.usp_RequirementGenerateCurRow Addusp_RequirementGenerateCurRow(
        string v_PlateNew,
        string v_PlateOld,
        int i_RequirementPlateId,
        int i_RequirementId,
        string RegisterDate,
        string RegisterTime,
        string v_OwnerCompleteName,
        string v_OwnerDocumentType,
        string v_OwnerDocumentNumber,
        int i_LocationId,
        string LocationDescription,
        string LocationAddress,
        string v_Brand,
        string v_Model,
        string v_SerialNumber,
        string v_Code,
        string ProductDescription,
        string UseTypeDescription,
        string v_AttentionSchedule,
        string ProofPaymentDescription,
        string BeneficiaryCompleteName,
        string BeneficiaryDocumentDescription,
        string BeneficiaryDocumentNumber,
        string beneficiaryAddress,
        string RequesterCompleteName,
        int i_ProcessTypeId,
        string ProcessTypeDescription,
        string v_OwnerDocumentDescription,
        string v_PaymentCode,
        string Requisite,
        byte[] ImageBarPlate,
        byte[] ImageBarCode,
        string v_ExpirationDate,
        int i_PlateTypeId,
        string f_PriceTotal,
        string v_TypeApplicant)
      {
        Ds_Requirement.usp_RequirementGenerateCurRow row = (Ds_Requirement.usp_RequirementGenerateCurRow) this.NewRow();
        object[] objArray = new object[37]
        {
          (object) v_PlateNew,
          (object) v_PlateOld,
          (object) i_RequirementPlateId,
          (object) i_RequirementId,
          (object) RegisterDate,
          (object) RegisterTime,
          (object) v_OwnerCompleteName,
          (object) v_OwnerDocumentType,
          (object) v_OwnerDocumentNumber,
          (object) i_LocationId,
          (object) LocationDescription,
          (object) LocationAddress,
          (object) v_Brand,
          (object) v_Model,
          (object) v_SerialNumber,
          (object) v_Code,
          (object) ProductDescription,
          (object) UseTypeDescription,
          (object) v_AttentionSchedule,
          (object) ProofPaymentDescription,
          (object) BeneficiaryCompleteName,
          (object) BeneficiaryDocumentDescription,
          (object) BeneficiaryDocumentNumber,
          (object) beneficiaryAddress,
          (object) RequesterCompleteName,
          (object) i_ProcessTypeId,
          (object) ProcessTypeDescription,
          (object) v_OwnerDocumentDescription,
          null,
          (object) v_PaymentCode,
          (object) Requisite,
          (object) ImageBarPlate,
          (object) ImageBarCode,
          (object) v_ExpirationDate,
          (object) i_PlateTypeId,
          (object) f_PriceTotal,
          (object) v_TypeApplicant
        };
        row.ItemArray = objArray;
        this.Rows.Add((DataRow) row);
        return row;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public override DataTable Clone()
      {
        Ds_Requirement.usp_RequirementGenerateCurDataTable generateCurDataTable = (Ds_Requirement.usp_RequirementGenerateCurDataTable) base.Clone();
        generateCurDataTable.InitVars();
        return (DataTable) generateCurDataTable;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new Ds_Requirement.usp_RequirementGenerateCurDataTable();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      internal void InitVars()
      {
        this.columnv_PlateNew = this.Columns["v_PlateNew"];
        this.columnv_PlateOld = this.Columns["v_PlateOld"];
        this.columni_RequirementPlateId = this.Columns["i_RequirementPlateId"];
        this.columni_RequirementId = this.Columns["i_RequirementId"];
        this.columnRegisterDate = this.Columns["RegisterDate"];
        this.columnRegisterTime = this.Columns["RegisterTime"];
        this.columnv_OwnerCompleteName = this.Columns["v_OwnerCompleteName"];
        this.columnv_OwnerDocumentType = this.Columns["v_OwnerDocumentType"];
        this.columnv_OwnerDocumentNumber = this.Columns["v_OwnerDocumentNumber"];
        this.columni_LocationId = this.Columns["i_LocationId"];
        this.columnLocationDescription = this.Columns["LocationDescription"];
        this.columnLocationAddress = this.Columns["LocationAddress"];
        this.columnv_Brand = this.Columns["v_Brand"];
        this.columnv_Model = this.Columns["v_Model"];
        this.columnv_SerialNumber = this.Columns["v_SerialNumber"];
        this.columnv_Code = this.Columns["v_Code"];
        this.columnProductDescription = this.Columns["ProductDescription"];
        this.columnUseTypeDescription = this.Columns["UseTypeDescription"];
        this.columnv_AttentionSchedule = this.Columns["v_AttentionSchedule"];
        this.columnProofPaymentDescription = this.Columns["ProofPaymentDescription"];
        this.columnBeneficiaryCompleteName = this.Columns["BeneficiaryCompleteName"];
        this.columnBeneficiaryDocumentDescription = this.Columns["BeneficiaryDocumentDescription"];
        this.columnBeneficiaryDocumentNumber = this.Columns["BeneficiaryDocumentNumber"];
        this.columnbeneficiaryAddress = this.Columns["beneficiaryAddress"];
        this.columnRequesterCompleteName = this.Columns["RequesterCompleteName"];
        this.columni_ProcessTypeId = this.Columns["i_ProcessTypeId"];
        this.columnProcessTypeDescription = this.Columns["ProcessTypeDescription"];
        this.columnv_OwnerDocumentDescription = this.Columns["v_OwnerDocumentDescription"];
        this.columni_PaymentId = this.Columns["i_PaymentId"];
        this.columnv_PaymentCode = this.Columns["v_PaymentCode"];
        this.columnRequisite = this.Columns["Requisite"];
        this.columnImageBarPlate = this.Columns["ImageBarPlate"];
        this.columnImageBarCode = this.Columns["ImageBarCode"];
        this.columnv_ExpirationDate = this.Columns["v_ExpirationDate"];
        this.columni_PlateTypeId = this.Columns["i_PlateTypeId"];
        this.columnf_PriceTotal = this.Columns["f_PriceTotal"];
        this.columnv_TypeApplicant = this.Columns["v_TypeApplicant"];
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      private void InitClass()
      {
        this.columnv_PlateNew = new DataColumn("v_PlateNew", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnv_PlateNew);
        this.columnv_PlateOld = new DataColumn("v_PlateOld", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnv_PlateOld);
        this.columni_RequirementPlateId = new DataColumn("i_RequirementPlateId", typeof (int), (string) null, MappingType.Element);
        this.Columns.Add(this.columni_RequirementPlateId);
        this.columni_RequirementId = new DataColumn("i_RequirementId", typeof (int), (string) null, MappingType.Element);
        this.Columns.Add(this.columni_RequirementId);
        this.columnRegisterDate = new DataColumn("RegisterDate", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnRegisterDate);
        this.columnRegisterTime = new DataColumn("RegisterTime", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnRegisterTime);
        this.columnv_OwnerCompleteName = new DataColumn("v_OwnerCompleteName", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnv_OwnerCompleteName);
        this.columnv_OwnerDocumentType = new DataColumn("v_OwnerDocumentType", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnv_OwnerDocumentType);
        this.columnv_OwnerDocumentNumber = new DataColumn("v_OwnerDocumentNumber", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnv_OwnerDocumentNumber);
        this.columni_LocationId = new DataColumn("i_LocationId", typeof (int), (string) null, MappingType.Element);
        this.Columns.Add(this.columni_LocationId);
        this.columnLocationDescription = new DataColumn("LocationDescription", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnLocationDescription);
        this.columnLocationAddress = new DataColumn("LocationAddress", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnLocationAddress);
        this.columnv_Brand = new DataColumn("v_Brand", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnv_Brand);
        this.columnv_Model = new DataColumn("v_Model", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnv_Model);
        this.columnv_SerialNumber = new DataColumn("v_SerialNumber", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnv_SerialNumber);
        this.columnv_Code = new DataColumn("v_Code", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnv_Code);
        this.columnProductDescription = new DataColumn("ProductDescription", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnProductDescription);
        this.columnUseTypeDescription = new DataColumn("UseTypeDescription", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnUseTypeDescription);
        this.columnv_AttentionSchedule = new DataColumn("v_AttentionSchedule", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnv_AttentionSchedule);
        this.columnProofPaymentDescription = new DataColumn("ProofPaymentDescription", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnProofPaymentDescription);
        this.columnBeneficiaryCompleteName = new DataColumn("BeneficiaryCompleteName", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnBeneficiaryCompleteName);
        this.columnBeneficiaryDocumentDescription = new DataColumn("BeneficiaryDocumentDescription", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnBeneficiaryDocumentDescription);
        this.columnBeneficiaryDocumentNumber = new DataColumn("BeneficiaryDocumentNumber", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnBeneficiaryDocumentNumber);
        this.columnbeneficiaryAddress = new DataColumn("beneficiaryAddress", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnbeneficiaryAddress);
        this.columnRequesterCompleteName = new DataColumn("RequesterCompleteName", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnRequesterCompleteName);
        this.columni_ProcessTypeId = new DataColumn("i_ProcessTypeId", typeof (int), (string) null, MappingType.Element);
        this.Columns.Add(this.columni_ProcessTypeId);
        this.columnProcessTypeDescription = new DataColumn("ProcessTypeDescription", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnProcessTypeDescription);
        this.columnv_OwnerDocumentDescription = new DataColumn("v_OwnerDocumentDescription", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnv_OwnerDocumentDescription);
        this.columni_PaymentId = new DataColumn("i_PaymentId", typeof (long), (string) null, MappingType.Element);
        this.Columns.Add(this.columni_PaymentId);
        this.columnv_PaymentCode = new DataColumn("v_PaymentCode", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnv_PaymentCode);
        this.columnRequisite = new DataColumn("Requisite", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnRequisite);
        this.columnImageBarPlate = new DataColumn("ImageBarPlate", typeof (byte[]), (string) null, MappingType.Element);
        this.Columns.Add(this.columnImageBarPlate);
        this.columnImageBarCode = new DataColumn("ImageBarCode", typeof (byte[]), (string) null, MappingType.Element);
        this.Columns.Add(this.columnImageBarCode);
        this.columnv_ExpirationDate = new DataColumn("v_ExpirationDate", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnv_ExpirationDate);
        this.columni_PlateTypeId = new DataColumn("i_PlateTypeId", typeof (int), (string) null, MappingType.Element);
        this.Columns.Add(this.columni_PlateTypeId);
        this.columnf_PriceTotal = new DataColumn("f_PriceTotal", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnf_PriceTotal);
        this.columnv_TypeApplicant = new DataColumn("v_TypeApplicant", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnv_TypeApplicant);
        this.columnv_PlateNew.MaxLength = 10;
        this.columnv_PlateOld.MaxLength = 10;
        this.columni_RequirementPlateId.AllowDBNull = false;
        this.columni_RequirementId.AllowDBNull = false;
        this.columnRegisterDate.ReadOnly = true;
        this.columnRegisterDate.MaxLength = 30;
        this.columnRegisterTime.ReadOnly = true;
        this.columnRegisterTime.MaxLength = 30;
        this.columnv_OwnerCompleteName.MaxLength = int.MaxValue;
        this.columnv_OwnerDocumentType.MaxLength = int.MaxValue;
        this.columnv_OwnerDocumentNumber.MaxLength = int.MaxValue;
        this.columni_LocationId.AllowDBNull = false;
        this.columnLocationDescription.MaxLength = 200;
        this.columnLocationAddress.ReadOnly = true;
        this.columnLocationAddress.MaxLength = 200;
        this.columnv_Brand.MaxLength = 50;
        this.columnv_Model.MaxLength = 50;
        this.columnv_SerialNumber.MaxLength = 50;
        this.columnv_Code.MaxLength = 100;
        this.columnProductDescription.ReadOnly = true;
        this.columnProductDescription.MaxLength = 644;
        this.columnUseTypeDescription.MaxLength = 100;
        this.columnv_AttentionSchedule.MaxLength = 150;
        this.columnProofPaymentDescription.MaxLength = 100;
        this.columnBeneficiaryCompleteName.MaxLength = 600;
        this.columnBeneficiaryDocumentDescription.MaxLength = 100;
        this.columnBeneficiaryDocumentNumber.MaxLength = 200;
        this.columnbeneficiaryAddress.MaxLength = 500;
        this.columnRequesterCompleteName.MaxLength = 600;
        this.columnProcessTypeDescription.MaxLength = 100;
        this.columnv_OwnerDocumentDescription.MaxLength = int.MaxValue;
        this.columni_PaymentId.AutoIncrement = true;
        this.columni_PaymentId.AutoIncrementSeed = -1L;
        this.columni_PaymentId.AutoIncrementStep = -1L;
        this.columni_PaymentId.ReadOnly = true;
        this.columnv_PaymentCode.MaxLength = 14;
        this.columnv_ExpirationDate.ReadOnly = true;
        this.columnv_ExpirationDate.MaxLength = 10;
        this.columnv_TypeApplicant.MaxLength = 40;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public Ds_Requirement.usp_RequirementGenerateCurRow Newusp_RequirementGenerateCurRow()
      {
        return (Ds_Requirement.usp_RequirementGenerateCurRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new Ds_Requirement.usp_RequirementGenerateCurRow(builder);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override Type GetRowType() => typeof (Ds_Requirement.usp_RequirementGenerateCurRow);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.usp_RequirementGenerateCurRowChanged == null)
          return;
        this.usp_RequirementGenerateCurRowChanged((object) this, new Ds_Requirement.usp_RequirementGenerateCurRowChangeEvent((Ds_Requirement.usp_RequirementGenerateCurRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.usp_RequirementGenerateCurRowChanging == null)
          return;
        this.usp_RequirementGenerateCurRowChanging((object) this, new Ds_Requirement.usp_RequirementGenerateCurRowChangeEvent((Ds_Requirement.usp_RequirementGenerateCurRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.usp_RequirementGenerateCurRowDeleted == null)
          return;
        this.usp_RequirementGenerateCurRowDeleted((object) this, new Ds_Requirement.usp_RequirementGenerateCurRowChangeEvent((Ds_Requirement.usp_RequirementGenerateCurRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.usp_RequirementGenerateCurRowDeleting == null)
          return;
        this.usp_RequirementGenerateCurRowDeleting((object) this, new Ds_Requirement.usp_RequirementGenerateCurRowChangeEvent((Ds_Requirement.usp_RequirementGenerateCurRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Removeusp_RequirementGenerateCurRow(
        Ds_Requirement.usp_RequirementGenerateCurRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType typedTableSchema = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        Ds_Requirement dsRequirement = new Ds_Requirement();
        XmlSchemaAny xmlSchemaAny1 = new XmlSchemaAny();
        xmlSchemaAny1.Namespace = "http://www.w3.org/2001/XMLSchema";
        xmlSchemaAny1.MinOccurs = 0M;
        xmlSchemaAny1.MaxOccurs = Decimal.MaxValue;
        xmlSchemaAny1.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny1);
        XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
        xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
        xmlSchemaAny2.MinOccurs = 1M;
        xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
        xmlSchemaSequence.Items.Add((XmlSchemaObject) xmlSchemaAny2);
        typedTableSchema.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "namespace",
          FixedValue = dsRequirement.Namespace
        });
        typedTableSchema.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = nameof (usp_RequirementGenerateCurDataTable)
        });
        typedTableSchema.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = dsRequirement.GetSchemaSerializable();
        if (xs.Contains(schemaSerializable.TargetNamespace))
        {
          MemoryStream memoryStream1 = new MemoryStream();
          MemoryStream memoryStream2 = new MemoryStream();
          try
          {
            schemaSerializable.Write((Stream) memoryStream1);
            IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
            while (enumerator.MoveNext())
            {
              XmlSchema current = (XmlSchema) enumerator.Current;
              memoryStream2.SetLength(0L);
              current.Write((Stream) memoryStream2);
              if (memoryStream1.Length == memoryStream2.Length)
              {
                memoryStream1.Position = 0L;
                memoryStream2.Position = 0L;
                do
                  ;
                while (memoryStream1.Position != memoryStream1.Length && memoryStream1.ReadByte() == memoryStream2.ReadByte());
                if (memoryStream1.Position == memoryStream1.Length)
                  return typedTableSchema;
              }
            }
          }
          finally
          {
            memoryStream1?.Close();
            memoryStream2?.Close();
          }
        }
        xs.Add(schemaSerializable);
        return typedTableSchema;
      }
    }

    public class usp_RequirementGenerateCurRow : DataRow
    {
      private Ds_Requirement.usp_RequirementGenerateCurDataTable tableusp_RequirementGenerateCur;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      internal usp_RequirementGenerateCurRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableusp_RequirementGenerateCur = (Ds_Requirement.usp_RequirementGenerateCurDataTable) this.Table;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string v_PlateNew
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.v_PlateNewColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'v_PlateNew' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.v_PlateNewColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string v_PlateOld
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.v_PlateOldColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'v_PlateOld' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.v_PlateOldColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public int i_RequirementPlateId
      {
        get => (int) this[this.tableusp_RequirementGenerateCur.i_RequirementPlateIdColumn];
        set
        {
          this[this.tableusp_RequirementGenerateCur.i_RequirementPlateIdColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public int i_RequirementId
      {
        get => (int) this[this.tableusp_RequirementGenerateCur.i_RequirementIdColumn];
        set => this[this.tableusp_RequirementGenerateCur.i_RequirementIdColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string RegisterDate
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.RegisterDateColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'RegisterDate' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.RegisterDateColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string RegisterTime
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.RegisterTimeColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'RegisterTime' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.RegisterTimeColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string v_OwnerCompleteName
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.v_OwnerCompleteNameColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'v_OwnerCompleteName' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableusp_RequirementGenerateCur.v_OwnerCompleteNameColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string v_OwnerDocumentType
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.v_OwnerDocumentTypeColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'v_OwnerDocumentType' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableusp_RequirementGenerateCur.v_OwnerDocumentTypeColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string v_OwnerDocumentNumber
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.v_OwnerDocumentNumberColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'v_OwnerDocumentNumber' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableusp_RequirementGenerateCur.v_OwnerDocumentNumberColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public int i_LocationId
      {
        get => (int) this[this.tableusp_RequirementGenerateCur.i_LocationIdColumn];
        set => this[this.tableusp_RequirementGenerateCur.i_LocationIdColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string LocationDescription
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.LocationDescriptionColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'LocationDescription' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableusp_RequirementGenerateCur.LocationDescriptionColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string LocationAddress
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.LocationAddressColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'LocationAddress' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.LocationAddressColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string v_Brand
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.v_BrandColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'v_Brand' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.v_BrandColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string v_Model
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.v_ModelColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'v_Model' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.v_ModelColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string v_SerialNumber
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.v_SerialNumberColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'v_SerialNumber' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.v_SerialNumberColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string v_Code
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.v_CodeColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'v_Code' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.v_CodeColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string ProductDescription
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.ProductDescriptionColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'ProductDescription' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.ProductDescriptionColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string UseTypeDescription
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.UseTypeDescriptionColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'UseTypeDescription' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.UseTypeDescriptionColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string v_AttentionSchedule
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.v_AttentionScheduleColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'v_AttentionSchedule' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableusp_RequirementGenerateCur.v_AttentionScheduleColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string ProofPaymentDescription
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.ProofPaymentDescriptionColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'ProofPaymentDescription' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableusp_RequirementGenerateCur.ProofPaymentDescriptionColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string BeneficiaryCompleteName
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.BeneficiaryCompleteNameColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'BeneficiaryCompleteName' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableusp_RequirementGenerateCur.BeneficiaryCompleteNameColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string BeneficiaryDocumentDescription
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.BeneficiaryDocumentDescriptionColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'BeneficiaryDocumentDescription' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableusp_RequirementGenerateCur.BeneficiaryDocumentDescriptionColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string BeneficiaryDocumentNumber
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.BeneficiaryDocumentNumberColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'BeneficiaryDocumentNumber' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableusp_RequirementGenerateCur.BeneficiaryDocumentNumberColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string beneficiaryAddress
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.beneficiaryAddressColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'beneficiaryAddress' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.beneficiaryAddressColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string RequesterCompleteName
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.RequesterCompleteNameColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'RequesterCompleteName' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableusp_RequirementGenerateCur.RequesterCompleteNameColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public int i_ProcessTypeId
      {
        get
        {
          try
          {
            return (int) this[this.tableusp_RequirementGenerateCur.i_ProcessTypeIdColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'i_ProcessTypeId' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.i_ProcessTypeIdColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string ProcessTypeDescription
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.ProcessTypeDescriptionColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'ProcessTypeDescription' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableusp_RequirementGenerateCur.ProcessTypeDescriptionColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string v_OwnerDocumentDescription
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.v_OwnerDocumentDescriptionColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'v_OwnerDocumentDescription' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set
        {
          this[this.tableusp_RequirementGenerateCur.v_OwnerDocumentDescriptionColumn] = (object) value;
        }
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public long i_PaymentId
      {
        get
        {
          try
          {
            return (long) this[this.tableusp_RequirementGenerateCur.i_PaymentIdColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'i_PaymentId' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.i_PaymentIdColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string v_PaymentCode
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.v_PaymentCodeColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'v_PaymentCode' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.v_PaymentCodeColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string Requisite
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.RequisiteColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'Requisite' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.RequisiteColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public byte[] ImageBarPlate
      {
        get
        {
          try
          {
            return (byte[]) this[this.tableusp_RequirementGenerateCur.ImageBarPlateColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'ImageBarPlate' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.ImageBarPlateColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public byte[] ImageBarCode
      {
        get
        {
          try
          {
            return (byte[]) this[this.tableusp_RequirementGenerateCur.ImageBarCodeColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'ImageBarCode' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.ImageBarCodeColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string v_ExpirationDate
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.v_ExpirationDateColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'v_ExpirationDate' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.v_ExpirationDateColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public int i_PlateTypeId
      {
        get
        {
          try
          {
            return (int) this[this.tableusp_RequirementGenerateCur.i_PlateTypeIdColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'i_PlateTypeId' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.i_PlateTypeIdColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string f_PriceTotal
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.f_PriceTotalColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'f_PriceTotal' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.f_PriceTotalColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string v_TypeApplicant
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_RequirementGenerateCur.v_TypeApplicantColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'v_TypeApplicant' in table 'usp_RequirementGenerateCur' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_RequirementGenerateCur.v_TypeApplicantColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isv_PlateNewNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.v_PlateNewColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setv_PlateNewNull()
      {
        this[this.tableusp_RequirementGenerateCur.v_PlateNewColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isv_PlateOldNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.v_PlateOldColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setv_PlateOldNull()
      {
        this[this.tableusp_RequirementGenerateCur.v_PlateOldColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsRegisterDateNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.RegisterDateColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetRegisterDateNull()
      {
        this[this.tableusp_RequirementGenerateCur.RegisterDateColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsRegisterTimeNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.RegisterTimeColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetRegisterTimeNull()
      {
        this[this.tableusp_RequirementGenerateCur.RegisterTimeColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isv_OwnerCompleteNameNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.v_OwnerCompleteNameColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setv_OwnerCompleteNameNull()
      {
        this[this.tableusp_RequirementGenerateCur.v_OwnerCompleteNameColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isv_OwnerDocumentTypeNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.v_OwnerDocumentTypeColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setv_OwnerDocumentTypeNull()
      {
        this[this.tableusp_RequirementGenerateCur.v_OwnerDocumentTypeColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isv_OwnerDocumentNumberNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.v_OwnerDocumentNumberColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setv_OwnerDocumentNumberNull()
      {
        this[this.tableusp_RequirementGenerateCur.v_OwnerDocumentNumberColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsLocationDescriptionNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.LocationDescriptionColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetLocationDescriptionNull()
      {
        this[this.tableusp_RequirementGenerateCur.LocationDescriptionColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsLocationAddressNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.LocationAddressColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetLocationAddressNull()
      {
        this[this.tableusp_RequirementGenerateCur.LocationAddressColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isv_BrandNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.v_BrandColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setv_BrandNull()
      {
        this[this.tableusp_RequirementGenerateCur.v_BrandColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isv_ModelNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.v_ModelColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setv_ModelNull()
      {
        this[this.tableusp_RequirementGenerateCur.v_ModelColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isv_SerialNumberNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.v_SerialNumberColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setv_SerialNumberNull()
      {
        this[this.tableusp_RequirementGenerateCur.v_SerialNumberColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isv_CodeNull() => this.IsNull(this.tableusp_RequirementGenerateCur.v_CodeColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setv_CodeNull()
      {
        this[this.tableusp_RequirementGenerateCur.v_CodeColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsProductDescriptionNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.ProductDescriptionColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetProductDescriptionNull()
      {
        this[this.tableusp_RequirementGenerateCur.ProductDescriptionColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsUseTypeDescriptionNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.UseTypeDescriptionColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetUseTypeDescriptionNull()
      {
        this[this.tableusp_RequirementGenerateCur.UseTypeDescriptionColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isv_AttentionScheduleNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.v_AttentionScheduleColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setv_AttentionScheduleNull()
      {
        this[this.tableusp_RequirementGenerateCur.v_AttentionScheduleColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsProofPaymentDescriptionNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.ProofPaymentDescriptionColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetProofPaymentDescriptionNull()
      {
        this[this.tableusp_RequirementGenerateCur.ProofPaymentDescriptionColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsBeneficiaryCompleteNameNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.BeneficiaryCompleteNameColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetBeneficiaryCompleteNameNull()
      {
        this[this.tableusp_RequirementGenerateCur.BeneficiaryCompleteNameColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsBeneficiaryDocumentDescriptionNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.BeneficiaryDocumentDescriptionColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetBeneficiaryDocumentDescriptionNull()
      {
        this[this.tableusp_RequirementGenerateCur.BeneficiaryDocumentDescriptionColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsBeneficiaryDocumentNumberNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.BeneficiaryDocumentNumberColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetBeneficiaryDocumentNumberNull()
      {
        this[this.tableusp_RequirementGenerateCur.BeneficiaryDocumentNumberColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsbeneficiaryAddressNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.beneficiaryAddressColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetbeneficiaryAddressNull()
      {
        this[this.tableusp_RequirementGenerateCur.beneficiaryAddressColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsRequesterCompleteNameNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.RequesterCompleteNameColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetRequesterCompleteNameNull()
      {
        this[this.tableusp_RequirementGenerateCur.RequesterCompleteNameColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isi_ProcessTypeIdNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.i_ProcessTypeIdColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Seti_ProcessTypeIdNull()
      {
        this[this.tableusp_RequirementGenerateCur.i_ProcessTypeIdColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsProcessTypeDescriptionNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.ProcessTypeDescriptionColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetProcessTypeDescriptionNull()
      {
        this[this.tableusp_RequirementGenerateCur.ProcessTypeDescriptionColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isv_OwnerDocumentDescriptionNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.v_OwnerDocumentDescriptionColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setv_OwnerDocumentDescriptionNull()
      {
        this[this.tableusp_RequirementGenerateCur.v_OwnerDocumentDescriptionColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isi_PaymentIdNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.i_PaymentIdColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Seti_PaymentIdNull()
      {
        this[this.tableusp_RequirementGenerateCur.i_PaymentIdColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isv_PaymentCodeNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.v_PaymentCodeColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setv_PaymentCodeNull()
      {
        this[this.tableusp_RequirementGenerateCur.v_PaymentCodeColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsRequisiteNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.RequisiteColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetRequisiteNull()
      {
        this[this.tableusp_RequirementGenerateCur.RequisiteColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsImageBarPlateNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.ImageBarPlateColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetImageBarPlateNull()
      {
        this[this.tableusp_RequirementGenerateCur.ImageBarPlateColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsImageBarCodeNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.ImageBarCodeColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetImageBarCodeNull()
      {
        this[this.tableusp_RequirementGenerateCur.ImageBarCodeColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isv_ExpirationDateNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.v_ExpirationDateColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setv_ExpirationDateNull()
      {
        this[this.tableusp_RequirementGenerateCur.v_ExpirationDateColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isi_PlateTypeIdNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.i_PlateTypeIdColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Seti_PlateTypeIdNull()
      {
        this[this.tableusp_RequirementGenerateCur.i_PlateTypeIdColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isf_PriceTotalNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.f_PriceTotalColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setf_PriceTotalNull()
      {
        this[this.tableusp_RequirementGenerateCur.f_PriceTotalColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isv_TypeApplicantNull()
      {
        return this.IsNull(this.tableusp_RequirementGenerateCur.v_TypeApplicantColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setv_TypeApplicantNull()
      {
        this[this.tableusp_RequirementGenerateCur.v_TypeApplicantColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public class usp_RequirementGenerateCurRowChangeEvent : EventArgs
    {
      private Ds_Requirement.usp_RequirementGenerateCurRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public usp_RequirementGenerateCurRowChangeEvent(
        Ds_Requirement.usp_RequirementGenerateCurRow row,
        DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public Ds_Requirement.usp_RequirementGenerateCurRow Row => this.eventRow;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataRowAction Action => this.eventAction;
    }
  }
}
