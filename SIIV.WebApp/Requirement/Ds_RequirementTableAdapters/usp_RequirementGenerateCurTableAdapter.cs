// Decompiled with JetBrains decompiler
// Type: SIIV.WebApp.Requirement.Ds_RequirementTableAdapters.usp_RequirementGenerateCurTableAdapter
// Assembly: SIIV.WebApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0F87038C-530E-41EF-B2A6-8BD0592819DD
// Assembly location: C:\Users\Cristofer\Downloads\20250923\Archivos\SIIV.WebApp.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;

#nullable disable
namespace SIIV.WebApp.Requirement.Ds_RequirementTableAdapters
{
  [DesignerCategory("code")]
  [ToolboxItem(true)]
  [DataObject(true)]
  [Designer("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  [HelpKeyword("vs.data.TableAdapter")]
  public class usp_RequirementGenerateCurTableAdapter : Component
  {
    private SqlDataAdapter _adapter;
    private SqlConnection _connection;
    private SqlTransaction _transaction;
    private SqlCommand[] _commandCollection;
    private bool _clearBeforeFill;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public usp_RequirementGenerateCurTableAdapter() => this.ClearBeforeFill = true;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected internal SqlDataAdapter Adapter
    {
      get
      {
        if (this._adapter == null)
          this.InitAdapter();
        return this._adapter;
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    internal SqlConnection Connection
    {
      get
      {
        if (this._connection == null)
          this.InitConnection();
        return this._connection;
      }
      set
      {
        this._connection = value;
        if (this.Adapter.InsertCommand != null)
          this.Adapter.InsertCommand.Connection = value;
        if (this.Adapter.DeleteCommand != null)
          this.Adapter.DeleteCommand.Connection = value;
        if (this.Adapter.UpdateCommand != null)
          this.Adapter.UpdateCommand.Connection = value;
        for (int index = 0; index < this.CommandCollection.Length; ++index)
        {
          if (this.CommandCollection[index] != null)
            this.CommandCollection[index].Connection = value;
        }
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    internal SqlTransaction Transaction
    {
      get => this._transaction;
      set
      {
        this._transaction = value;
        for (int index = 0; index < this.CommandCollection.Length; ++index)
          this.CommandCollection[index].Transaction = this._transaction;
        if (this.Adapter != null && this.Adapter.DeleteCommand != null)
          this.Adapter.DeleteCommand.Transaction = this._transaction;
        if (this.Adapter != null && this.Adapter.InsertCommand != null)
          this.Adapter.InsertCommand.Transaction = this._transaction;
        if (this.Adapter == null || this.Adapter.UpdateCommand == null)
          return;
        this.Adapter.UpdateCommand.Transaction = this._transaction;
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected SqlCommand[] CommandCollection
    {
      get
      {
        if (this._commandCollection == null)
          this.InitCommandCollection();
        return this._commandCollection;
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool ClearBeforeFill
    {
      get => this._clearBeforeFill;
      set => this._clearBeforeFill = value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void InitAdapter()
    {
      this._adapter = new SqlDataAdapter();
      this._adapter.TableMappings.Add((object) new DataTableMapping()
      {
        SourceTable = "Table",
        DataSetTable = "usp_RequirementGenerateCur",
        ColumnMappings = {
          {
            "v_PlateNew",
            "v_PlateNew"
          },
          {
            "v_PlateOld",
            "v_PlateOld"
          },
          {
            "i_RequirementPlateId",
            "i_RequirementPlateId"
          },
          {
            "i_RequirementId",
            "i_RequirementId"
          },
          {
            "RegisterDate",
            "RegisterDate"
          },
          {
            "RegisterTime",
            "RegisterTime"
          },
          {
            "v_OwnerCompleteName",
            "v_OwnerCompleteName"
          },
          {
            "v_OwnerDocumentType",
            "v_OwnerDocumentType"
          },
          {
            "v_OwnerDocumentNumber",
            "v_OwnerDocumentNumber"
          },
          {
            "i_LocationId",
            "i_LocationId"
          },
          {
            "LocationDescription",
            "LocationDescription"
          },
          {
            "LocationAddress",
            "LocationAddress"
          },
          {
            "v_Brand",
            "v_Brand"
          },
          {
            "v_Model",
            "v_Model"
          },
          {
            "v_SerialNumber",
            "v_SerialNumber"
          },
          {
            "v_Code",
            "v_Code"
          },
          {
            "ProductDescription",
            "ProductDescription"
          },
          {
            "UseTypeDescription",
            "UseTypeDescription"
          },
          {
            "v_AttentionSchedule",
            "v_AttentionSchedule"
          },
          {
            "ProofPaymentDescription",
            "ProofPaymentDescription"
          },
          {
            "BeneficiaryCompleteName",
            "BeneficiaryCompleteName"
          },
          {
            "BeneficiaryDocumentDescription",
            "BeneficiaryDocumentDescription"
          },
          {
            "BeneficiaryDocumentNumber",
            "BeneficiaryDocumentNumber"
          },
          {
            "beneficiaryAddress",
            "beneficiaryAddress"
          },
          {
            "RequesterCompleteName",
            "RequesterCompleteName"
          },
          {
            "i_ProcessTypeId",
            "i_ProcessTypeId"
          },
          {
            "ProcessTypeDescription",
            "ProcessTypeDescription"
          },
          {
            "v_OwnerDocumentDescription",
            "v_OwnerDocumentDescription"
          },
          {
            "i_PaymentId",
            "i_PaymentId"
          },
          {
            "v_PaymentCode",
            "v_PaymentCode"
          },
          {
            "v_ExpirationDate",
            "v_ExpirationDate"
          },
          {
            "v_TypeApplicant",
            "v_TypeApplicant"
          }
        }
      });
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void InitConnection()
    {
      this._connection = new SqlConnection();
      this._connection.ConnectionString = ConfigurationManager.ConnectionStrings["ConexionPlacas"].ConnectionString;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void InitCommandCollection()
    {
      this._commandCollection = new SqlCommand[1];
      this._commandCollection[0] = new SqlCommand();
      this._commandCollection[0].Connection = this.Connection;
      this._commandCollection[0].CommandText = "Requirement.usp_RequirementGenerateCur";
      this._commandCollection[0].CommandType = CommandType.StoredProcedure;
      this._commandCollection[0].Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, (byte) 10, (byte) 0, (string) null, DataRowVersion.Current, false, (object) null, "", "", ""));
      this._commandCollection[0].Parameters.Add(new SqlParameter("@i_RequirementId", SqlDbType.Int, 4, ParameterDirection.Input, (byte) 10, (byte) 0, (string) null, DataRowVersion.Current, false, (object) null, "", "", ""));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [HelpKeyword("vs.data.TableAdapter")]
    [DataObjectMethod(DataObjectMethodType.Fill, true)]
    public virtual int Fill(
      Ds_Requirement.usp_RequirementGenerateCurDataTable dataTable,
      int? i_RequirementId)
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      if (i_RequirementId.HasValue)
        this.Adapter.SelectCommand.Parameters[1].Value = (object) i_RequirementId.Value;
      else
        this.Adapter.SelectCommand.Parameters[1].Value = (object) DBNull.Value;
      if (this.ClearBeforeFill)
        dataTable.Clear();
      return this.Adapter.Fill((DataTable) dataTable);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [HelpKeyword("vs.data.TableAdapter")]
    [DataObjectMethod(DataObjectMethodType.Select, true)]
    public virtual Ds_Requirement.usp_RequirementGenerateCurDataTable GetData(int? i_RequirementId)
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      if (i_RequirementId.HasValue)
        this.Adapter.SelectCommand.Parameters[1].Value = (object) i_RequirementId.Value;
      else
        this.Adapter.SelectCommand.Parameters[1].Value = (object) DBNull.Value;
      Ds_Requirement.usp_RequirementGenerateCurDataTable data = new Ds_Requirement.usp_RequirementGenerateCurDataTable();
      this.Adapter.Fill((DataTable) data);
      return data;
    }
  }
}
