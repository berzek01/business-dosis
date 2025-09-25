// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Retail.CashRegister.Ds_CashRegister
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
namespace SIIV.WebApp.Retail.CashRegister
{
  [DesignerCategory("code")]
  [ToolboxItem(true)]
  [XmlSchemaProvider("GetTypedDataSetSchema")]
  [XmlRoot("Ds_CashRegister")]
  [HelpKeyword("vs.data.DataSet")]
  [Serializable]
  public class Ds_CashRegister : DataSet
  {
    private Ds_CashRegister.usp_CashRegGenerateDataTable tableusp_CashRegGenerate;
    private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public Ds_CashRegister()
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
    protected Ds_CashRegister(SerializationInfo info, StreamingContext context)
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
          if (dataSet.Tables[nameof (usp_CashRegGenerate)] != null)
            base.Tables.Add((DataTable) new Ds_CashRegister.usp_CashRegGenerateDataTable(dataSet.Tables[nameof (usp_CashRegGenerate)]));
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
    public Ds_CashRegister.usp_CashRegGenerateDataTable usp_CashRegGenerate
    {
      get => this.tableusp_CashRegGenerate;
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
      Ds_CashRegister dsCashRegister = (Ds_CashRegister) base.Clone();
      dsCashRegister.InitVars();
      dsCashRegister.SchemaSerializationMode = this.SchemaSerializationMode;
      return (DataSet) dsCashRegister;
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
        if (dataSet.Tables["usp_CashRegGenerate"] != null)
          base.Tables.Add((DataTable) new Ds_CashRegister.usp_CashRegGenerateDataTable(dataSet.Tables["usp_CashRegGenerate"]));
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
      this.tableusp_CashRegGenerate = (Ds_CashRegister.usp_CashRegGenerateDataTable) base.Tables["usp_CashRegGenerate"];
      if (!initTable || this.tableusp_CashRegGenerate == null)
        return;
      this.tableusp_CashRegGenerate.InitVars();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void InitClass()
    {
      this.DataSetName = nameof (Ds_CashRegister);
      this.Prefix = "";
      this.Namespace = "http://tempuri.org/Ds_CashRegister.xsd";
      this.EnforceConstraints = true;
      this.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
      this.tableusp_CashRegGenerate = new Ds_CashRegister.usp_CashRegGenerateDataTable();
      base.Tables.Add((DataTable) this.tableusp_CashRegGenerate);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private bool ShouldSerializeusp_CashRegGenerate() => false;

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
      Ds_CashRegister dsCashRegister = new Ds_CashRegister();
      XmlSchemaComplexType typedDataSetSchema = new XmlSchemaComplexType();
      XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
      xmlSchemaSequence.Items.Add((XmlSchemaObject) new XmlSchemaAny()
      {
        Namespace = dsCashRegister.Namespace
      });
      typedDataSetSchema.Particle = (XmlSchemaParticle) xmlSchemaSequence;
      XmlSchema schemaSerializable = dsCashRegister.GetSchemaSerializable();
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
    public delegate void usp_CashRegGenerateRowChangeEventHandler(
      object sender,
      Ds_CashRegister.usp_CashRegGenerateRowChangeEvent e);

    [XmlSchemaProvider("GetTypedTableSchema")]
    [Serializable]
    public class usp_CashRegGenerateDataTable : 
      TypedTableBase<Ds_CashRegister.usp_CashRegGenerateRow>
    {
      private DataColumn columnf_AmountTotal;
      private DataColumn columnv_AdminRespon;
      private DataColumn columnv_Location;
      private DataColumn columnf_AmountStart;
      private DataColumn columnf_AmountVisa;
      private DataColumn columnf_AmountCash;
      private DataColumn columni_ProductId;
      private DataColumn columnv_Name;
      private DataColumn columnCantidad;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public usp_CashRegGenerateDataTable()
      {
        this.TableName = "usp_CashRegGenerate";
        this.BeginInit();
        this.InitClass();
        this.EndInit();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      internal usp_CashRegGenerateDataTable(DataTable table)
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
      protected usp_CashRegGenerateDataTable(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.InitVars();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn f_AmountTotalColumn => this.columnf_AmountTotal;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn v_AdminResponColumn => this.columnv_AdminRespon;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn v_LocationColumn => this.columnv_Location;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn f_AmountStartColumn => this.columnf_AmountStart;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn f_AmountVisaColumn => this.columnf_AmountVisa;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn f_AmountCashColumn => this.columnf_AmountCash;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn i_ProductIdColumn => this.columni_ProductId;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn v_NameColumn => this.columnv_Name;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataColumn CantidadColumn => this.columnCantidad;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [Browsable(false)]
      public int Count => this.Rows.Count;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public Ds_CashRegister.usp_CashRegGenerateRow this[int index]
      {
        get => (Ds_CashRegister.usp_CashRegGenerateRow) this.Rows[index];
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event Ds_CashRegister.usp_CashRegGenerateRowChangeEventHandler usp_CashRegGenerateRowChanging;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event Ds_CashRegister.usp_CashRegGenerateRowChangeEventHandler usp_CashRegGenerateRowChanged;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event Ds_CashRegister.usp_CashRegGenerateRowChangeEventHandler usp_CashRegGenerateRowDeleting;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public event Ds_CashRegister.usp_CashRegGenerateRowChangeEventHandler usp_CashRegGenerateRowDeleted;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Addusp_CashRegGenerateRow(Ds_CashRegister.usp_CashRegGenerateRow row)
      {
        this.Rows.Add((DataRow) row);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public Ds_CashRegister.usp_CashRegGenerateRow Addusp_CashRegGenerateRow(
        string f_AmountTotal,
        string v_AdminRespon,
        string v_Location,
        string f_AmountStart,
        string f_AmountVisa,
        string f_AmountCash,
        string i_ProductId,
        string v_Name,
        string Cantidad)
      {
        Ds_CashRegister.usp_CashRegGenerateRow row = (Ds_CashRegister.usp_CashRegGenerateRow) this.NewRow();
        object[] objArray = new object[9]
        {
          (object) f_AmountTotal,
          (object) v_AdminRespon,
          (object) v_Location,
          (object) f_AmountStart,
          (object) f_AmountVisa,
          (object) f_AmountCash,
          (object) i_ProductId,
          (object) v_Name,
          (object) Cantidad
        };
        row.ItemArray = objArray;
        this.Rows.Add((DataRow) row);
        return row;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public override DataTable Clone()
      {
        Ds_CashRegister.usp_CashRegGenerateDataTable generateDataTable = (Ds_CashRegister.usp_CashRegGenerateDataTable) base.Clone();
        generateDataTable.InitVars();
        return (DataTable) generateDataTable;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override DataTable CreateInstance()
      {
        return (DataTable) new Ds_CashRegister.usp_CashRegGenerateDataTable();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      internal void InitVars()
      {
        this.columnf_AmountTotal = this.Columns["f_AmountTotal"];
        this.columnv_AdminRespon = this.Columns["v_AdminRespon"];
        this.columnv_Location = this.Columns["v_Location"];
        this.columnf_AmountStart = this.Columns["f_AmountStart"];
        this.columnf_AmountVisa = this.Columns["f_AmountVisa"];
        this.columnf_AmountCash = this.Columns["f_AmountCash"];
        this.columni_ProductId = this.Columns["i_ProductId"];
        this.columnv_Name = this.Columns["v_Name"];
        this.columnCantidad = this.Columns["Cantidad"];
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      private void InitClass()
      {
        this.columnf_AmountTotal = new DataColumn("f_AmountTotal", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnf_AmountTotal);
        this.columnv_AdminRespon = new DataColumn("v_AdminRespon", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnv_AdminRespon);
        this.columnv_Location = new DataColumn("v_Location", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnv_Location);
        this.columnf_AmountStart = new DataColumn("f_AmountStart", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnf_AmountStart);
        this.columnf_AmountVisa = new DataColumn("f_AmountVisa", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnf_AmountVisa);
        this.columnf_AmountCash = new DataColumn("f_AmountCash", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnf_AmountCash);
        this.columni_ProductId = new DataColumn("i_ProductId", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columni_ProductId);
        this.columnv_Name = new DataColumn("v_Name", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnv_Name);
        this.columnCantidad = new DataColumn("Cantidad", typeof (string), (string) null, MappingType.Element);
        this.Columns.Add(this.columnCantidad);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public Ds_CashRegister.usp_CashRegGenerateRow Newusp_CashRegGenerateRow()
      {
        return (Ds_CashRegister.usp_CashRegGenerateRow) this.NewRow();
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
      {
        return (DataRow) new Ds_CashRegister.usp_CashRegGenerateRow(builder);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override Type GetRowType() => typeof (Ds_CashRegister.usp_CashRegGenerateRow);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowChanged(DataRowChangeEventArgs e)
      {
        base.OnRowChanged(e);
        if (this.usp_CashRegGenerateRowChanged == null)
          return;
        this.usp_CashRegGenerateRowChanged((object) this, new Ds_CashRegister.usp_CashRegGenerateRowChangeEvent((Ds_CashRegister.usp_CashRegGenerateRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowChanging(DataRowChangeEventArgs e)
      {
        base.OnRowChanging(e);
        if (this.usp_CashRegGenerateRowChanging == null)
          return;
        this.usp_CashRegGenerateRowChanging((object) this, new Ds_CashRegister.usp_CashRegGenerateRowChangeEvent((Ds_CashRegister.usp_CashRegGenerateRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowDeleted(DataRowChangeEventArgs e)
      {
        base.OnRowDeleted(e);
        if (this.usp_CashRegGenerateRowDeleted == null)
          return;
        this.usp_CashRegGenerateRowDeleted((object) this, new Ds_CashRegister.usp_CashRegGenerateRowChangeEvent((Ds_CashRegister.usp_CashRegGenerateRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      protected override void OnRowDeleting(DataRowChangeEventArgs e)
      {
        base.OnRowDeleting(e);
        if (this.usp_CashRegGenerateRowDeleting == null)
          return;
        this.usp_CashRegGenerateRowDeleting((object) this, new Ds_CashRegister.usp_CashRegGenerateRowChangeEvent((Ds_CashRegister.usp_CashRegGenerateRow) e.Row, e.Action));
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Removeusp_CashRegGenerateRow(Ds_CashRegister.usp_CashRegGenerateRow row)
      {
        this.Rows.Remove((DataRow) row);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
      {
        XmlSchemaComplexType typedTableSchema = new XmlSchemaComplexType();
        XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
        Ds_CashRegister dsCashRegister = new Ds_CashRegister();
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
          FixedValue = dsCashRegister.Namespace
        });
        typedTableSchema.Attributes.Add((XmlSchemaObject) new XmlSchemaAttribute()
        {
          Name = "tableTypeName",
          FixedValue = nameof (usp_CashRegGenerateDataTable)
        });
        typedTableSchema.Particle = (XmlSchemaParticle) xmlSchemaSequence;
        XmlSchema schemaSerializable = dsCashRegister.GetSchemaSerializable();
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

    public class usp_CashRegGenerateRow : DataRow
    {
      private Ds_CashRegister.usp_CashRegGenerateDataTable tableusp_CashRegGenerate;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      internal usp_CashRegGenerateRow(DataRowBuilder rb)
        : base(rb)
      {
        this.tableusp_CashRegGenerate = (Ds_CashRegister.usp_CashRegGenerateDataTable) this.Table;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string f_AmountTotal
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_CashRegGenerate.f_AmountTotalColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'f_AmountTotal' in table 'usp_CashRegGenerate' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_CashRegGenerate.f_AmountTotalColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string v_AdminRespon
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_CashRegGenerate.v_AdminResponColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'v_AdminRespon' in table 'usp_CashRegGenerate' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_CashRegGenerate.v_AdminResponColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string v_Location
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_CashRegGenerate.v_LocationColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'v_Location' in table 'usp_CashRegGenerate' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_CashRegGenerate.v_LocationColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string f_AmountStart
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_CashRegGenerate.f_AmountStartColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'f_AmountStart' in table 'usp_CashRegGenerate' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_CashRegGenerate.f_AmountStartColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string f_AmountVisa
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_CashRegGenerate.f_AmountVisaColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'f_AmountVisa' in table 'usp_CashRegGenerate' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_CashRegGenerate.f_AmountVisaColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string f_AmountCash
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_CashRegGenerate.f_AmountCashColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'f_AmountCash' in table 'usp_CashRegGenerate' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_CashRegGenerate.f_AmountCashColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string i_ProductId
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_CashRegGenerate.i_ProductIdColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'i_ProductId' in table 'usp_CashRegGenerate' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_CashRegGenerate.i_ProductIdColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string v_Name
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_CashRegGenerate.v_NameColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'v_Name' in table 'usp_CashRegGenerate' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_CashRegGenerate.v_NameColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public string Cantidad
      {
        get
        {
          try
          {
            return (string) this[this.tableusp_CashRegGenerate.CantidadColumn];
          }
          catch (InvalidCastException ex)
          {
            throw new StrongTypingException("The value for column 'Cantidad' in table 'usp_CashRegGenerate' is DBNull.", (Exception) ex);
          }
        }
        set => this[this.tableusp_CashRegGenerate.CantidadColumn] = (object) value;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isf_AmountTotalNull()
      {
        return this.IsNull(this.tableusp_CashRegGenerate.f_AmountTotalColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setf_AmountTotalNull()
      {
        this[this.tableusp_CashRegGenerate.f_AmountTotalColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isv_AdminResponNull()
      {
        return this.IsNull(this.tableusp_CashRegGenerate.v_AdminResponColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setv_AdminResponNull()
      {
        this[this.tableusp_CashRegGenerate.v_AdminResponColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isv_LocationNull() => this.IsNull(this.tableusp_CashRegGenerate.v_LocationColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setv_LocationNull()
      {
        this[this.tableusp_CashRegGenerate.v_LocationColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isf_AmountStartNull()
      {
        return this.IsNull(this.tableusp_CashRegGenerate.f_AmountStartColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setf_AmountStartNull()
      {
        this[this.tableusp_CashRegGenerate.f_AmountStartColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isf_AmountVisaNull()
      {
        return this.IsNull(this.tableusp_CashRegGenerate.f_AmountVisaColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setf_AmountVisaNull()
      {
        this[this.tableusp_CashRegGenerate.f_AmountVisaColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isf_AmountCashNull()
      {
        return this.IsNull(this.tableusp_CashRegGenerate.f_AmountCashColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setf_AmountCashNull()
      {
        this[this.tableusp_CashRegGenerate.f_AmountCashColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isi_ProductIdNull()
      {
        return this.IsNull(this.tableusp_CashRegGenerate.i_ProductIdColumn);
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Seti_ProductIdNull()
      {
        this[this.tableusp_CashRegGenerate.i_ProductIdColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool Isv_NameNull() => this.IsNull(this.tableusp_CashRegGenerate.v_NameColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void Setv_NameNull()
      {
        this[this.tableusp_CashRegGenerate.v_NameColumn] = Convert.DBNull;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public bool IsCantidadNull() => this.IsNull(this.tableusp_CashRegGenerate.CantidadColumn);

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public void SetCantidadNull()
      {
        this[this.tableusp_CashRegGenerate.CantidadColumn] = Convert.DBNull;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public class usp_CashRegGenerateRowChangeEvent : EventArgs
    {
      private Ds_CashRegister.usp_CashRegGenerateRow eventRow;
      private DataRowAction eventAction;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public usp_CashRegGenerateRowChangeEvent(
        Ds_CashRegister.usp_CashRegGenerateRow row,
        DataRowAction action)
      {
        this.eventRow = row;
        this.eventAction = action;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public Ds_CashRegister.usp_CashRegGenerateRow Row => this.eventRow;

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      public DataRowAction Action => this.eventAction;
    }
  }
}
