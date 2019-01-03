Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.WF
  <DataObject()> _
  Partial Public Class wfPreOrderHistory
    Private Shared _RecordCount As Integer
    Private _WF_HistoryID As Int32 = 0
    Private _WFID As Int32 = 0
    Private _WFID_SlNo As Int32 = 0
    Private _Parent_WFID As String = ""
    Private _Project As String = ""
    Private _Element As String = ""
    Private _SpecificationNo As String = ""
    Private _Buyer As String = ""
    Private _WF_Status As String = ""
    Private _UserId As String = ""
    Private _DateTime As String = ""
    Private _Supplier As String = ""
    Private _SupplierName As String = ""
    Private _Notes As String = ""
    Private _PMDLDocNo As String = ""
    Private _Manager As String = ""
    Private _IndentNo As String = ""
    Private _IndentLine As String = ""
    Private _LotItem As String = ""
    Private _WF1_PreOrder1_SpecificationNo As String = ""
    Private _FK__WF1_PreOrd__WFID__3895A3CC As SIS.WF.wfPreOrder = Nothing
    Public Property WF_HistoryID() As Int32
      Get
        Return _WF_HistoryID
      End Get
      Set(ByVal value As Int32)
        _WF_HistoryID = value
      End Set
    End Property
    Public Property WFID() As Int32
      Get
        Return _WFID
      End Get
      Set(ByVal value As Int32)
        _WFID = value
      End Set
    End Property
    Public Property WFID_SlNo() As Int32
      Get
        Return _WFID_SlNo
      End Get
      Set(ByVal value As Int32)
        _WFID_SlNo = value
      End Set
    End Property
    Public Property Parent_WFID() As String
      Get
        Return _Parent_WFID
      End Get
      Set(ByVal value As String)
         If Convert.IsDBNull(Value) Then
           _Parent_WFID = ""
         Else
           _Parent_WFID = value
         End If
      End Set
    End Property
    Public Property Project() As String
      Get
        Return _Project
      End Get
      Set(ByVal value As String)
        _Project = value
      End Set
    End Property
    Public Property Element() As String
      Get
        Return _Element
      End Get
      Set(ByVal value As String)
        _Element = value
      End Set
    End Property
    Public Property SpecificationNo() As String
      Get
        Return _SpecificationNo
      End Get
      Set(ByVal value As String)
        _SpecificationNo = value
      End Set
    End Property
    Public Property Buyer() As String
      Get
        Return _Buyer
      End Get
      Set(ByVal value As String)
        _Buyer = value
      End Set
    End Property
    Public Property WF_Status() As String
      Get
        Return _WF_Status
      End Get
      Set(ByVal value As String)
        _WF_Status = value
      End Set
    End Property
    Public Property UserId() As String
      Get
        Return _UserId
      End Get
      Set(ByVal value As String)
        _UserId = value
      End Set
    End Property
    Public Property DateTime() As String
      Get
        If Not _DateTime = String.Empty Then
          Return Convert.ToDateTime(_DateTime).ToString("dd/MM/yyyy")
        End If
        Return _DateTime
      End Get
      Set(ByVal value As String)
         _DateTime = value
      End Set
    End Property
    Public Property Supplier() As String
      Get
        Return _Supplier
      End Get
      Set(ByVal value As String)
         If Convert.IsDBNull(Value) Then
           _Supplier = ""
         Else
           _Supplier = value
         End If
      End Set
    End Property
    Public Property SupplierName() As String
      Get
        Return _SupplierName
      End Get
      Set(ByVal value As String)
         If Convert.IsDBNull(Value) Then
           _SupplierName = ""
         Else
           _SupplierName = value
         End If
      End Set
    End Property
    Public Property Notes() As String
      Get
        Return _Notes
      End Get
      Set(ByVal value As String)
         If Convert.IsDBNull(Value) Then
           _Notes = ""
         Else
           _Notes = value
         End If
      End Set
    End Property
    Public Property PMDLDocNo() As String
      Get
        Return _PMDLDocNo
      End Get
      Set(ByVal value As String)
         If Convert.IsDBNull(Value) Then
           _PMDLDocNo = ""
         Else
           _PMDLDocNo = value
         End If
      End Set
    End Property
    Public Property Manager() As String
      Get
        Return _Manager
      End Get
      Set(ByVal value As String)
         If Convert.IsDBNull(Value) Then
           _Manager = ""
         Else
           _Manager = value
         End If
      End Set
    End Property
    Public Property IndentNo() As String
      Get
        Return _IndentNo
      End Get
      Set(ByVal value As String)
         If Convert.IsDBNull(Value) Then
           _IndentNo = ""
         Else
           _IndentNo = value
         End If
      End Set
    End Property
    Public Property IndentLine() As String
      Get
        Return _IndentLine
      End Get
      Set(ByVal value As String)
         If Convert.IsDBNull(Value) Then
           _IndentLine = ""
         Else
           _IndentLine = value
         End If
      End Set
    End Property
    Public Property LotItem() As String
      Get
        Return _LotItem
      End Get
      Set(ByVal value As String)
         If Convert.IsDBNull(Value) Then
           _LotItem = ""
         Else
           _LotItem = value
         End If
      End Set
    End Property
    Public Property WF1_PreOrder1_SpecificationNo() As String
      Get
        Return _WF1_PreOrder1_SpecificationNo
      End Get
      Set(ByVal value As String)
        _WF1_PreOrder1_SpecificationNo = value
      End Set
    End Property
    Public Readonly Property DisplayField() As String
      Get
        Return ""
      End Get
    End Property
    Public Readonly Property PrimaryKey() As String
      Get
        Return _WF_HistoryID
      End Get
    End Property
    Public Shared Property RecordCount() As Integer
      Get
        Return _RecordCount
      End Get
      Set(ByVal value As Integer)
        _RecordCount = value
      End Set
    End Property
    Public Class PKwfPreOrderHistory
      Private _WF_HistoryID As Int32 = 0
      Public Property WF_HistoryID() As Int32
        Get
          Return _WF_HistoryID
        End Get
        Set(ByVal value As Int32)
          _WF_HistoryID = value
        End Set
      End Property
    End Class
    Public ReadOnly Property FK__WF1_PreOrd__WFID__3895A3CC() As SIS.WF.wfPreOrder
      Get
        If _FK__WF1_PreOrd__WFID__3895A3CC Is Nothing Then
          _FK__WF1_PreOrd__WFID__3895A3CC = SIS.WF.wfPreOrder.wfPreOrderGetByID(_WFID)
        End If
        Return _FK__WF1_PreOrd__WFID__3895A3CC
      End Get
    End Property
    Public Shared Function wfPreOrderHistoryGetByID(ByVal WF_HistoryID As Int32) As SIS.WF.wfPreOrderHistory
      Dim Results As SIS.WF.wfPreOrderHistory = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "spwfPreOrderHistorySelectByID"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@WF_HistoryID", SqlDbType.Int, WF_HistoryID.ToString.Length, WF_HistoryID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NVarChar, 9, HttpContext.Current.Session("LoginID"))
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          If Reader.Read() Then
            Results = New SIS.WF.wfPreOrderHistory(Reader)
          End If
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function GetByWFID(ByVal WFID As Integer) As List(Of SIS.WF.wfPreOrderHistory)
      Dim Results As List(Of SIS.WF.wfPreOrderHistory) = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "select * from WF1_PreOrder_History where wfid=" & WFID
          Results = New List(Of SIS.WF.wfPreOrderHistory)()
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.WF.wfPreOrderHistory(Reader))
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function InsertData(ByVal Record As SIS.WF.wfPreOrderHistory) As SIS.WF.wfPreOrderHistory
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "spwfPreOrderHistoryInsert"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@WF_HistoryID", SqlDbType.Int, 11, Record.WF_HistoryID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@WFID", SqlDbType.Int, 11, Record.WFID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@WFID_SlNo", SqlDbType.Int, 11, Record.WFID_SlNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Parent_WFID", SqlDbType.Int, 11, IIf(Record.Parent_WFID = "", Convert.DBNull, Record.Parent_WFID))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Project", SqlDbType.VarChar, 51, Record.Project)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Element", SqlDbType.VarChar, 51, Record.Element)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@SpecificationNo", SqlDbType.VarChar, 101, Record.SpecificationNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Buyer", SqlDbType.VarChar, 9, Record.Buyer)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@WF_Status", SqlDbType.VarChar, 101, Record.WF_Status)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@UserId", SqlDbType.VarChar, 9, Record.UserId)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@DateTime", SqlDbType.DateTime, 21, Record.DateTime)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Supplier", SqlDbType.VarChar, 51, IIf(Record.Supplier = "", Convert.DBNull, Record.Supplier))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@SupplierName", SqlDbType.VarChar, 101, IIf(Record.SupplierName = "", Convert.DBNull, Record.SupplierName))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Notes", SqlDbType.VarChar, 501, IIf(Record.Notes = "", Convert.DBNull, Record.Notes))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@PMDLDocNo", SqlDbType.VarChar, 101, IIf(Record.PMDLDocNo = "", Convert.DBNull, Record.PMDLDocNo))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Manager", SqlDbType.VarChar, 9, IIf(Record.Manager = "", Convert.DBNull, Record.Manager))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IndentNo", SqlDbType.VarChar, 10, IIf(Record.IndentNo = "", Convert.DBNull, Record.IndentNo))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IndentLine", SqlDbType.Int, 11, IIf(Record.IndentLine = "", Convert.DBNull, Record.IndentLine))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LotItem", SqlDbType.VarChar, 48, IIf(Record.LotItem = "", Convert.DBNull, Record.LotItem))
          Cmd.Parameters.Add("@Return_WF_HistoryID", SqlDbType.Int, 11)
          Cmd.Parameters("@Return_WF_HistoryID").Direction = ParameterDirection.Output
          Con.Open()
          Cmd.ExecuteNonQuery()
          Record.WF_HistoryID = Cmd.Parameters("@Return_WF_HistoryID").Value
        End Using
      End Using
      Return Record
    End Function
    Public Shared Function UpdateData(ByVal Record As SIS.WF.wfPreOrderHistory) As SIS.WF.wfPreOrderHistory
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "spwfPreOrderHistoryUpdate"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Original_WF_HistoryID", SqlDbType.Int, 11, Record.WF_HistoryID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@WF_HistoryID", SqlDbType.Int, 11, Record.WF_HistoryID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@WFID", SqlDbType.Int, 11, Record.WFID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@WFID_SlNo", SqlDbType.Int, 11, Record.WFID_SlNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Parent_WFID", SqlDbType.Int, 11, IIf(Record.Parent_WFID = "", Convert.DBNull, Record.Parent_WFID))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Project", SqlDbType.VarChar, 51, Record.Project)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Element", SqlDbType.VarChar, 51, Record.Element)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@SpecificationNo", SqlDbType.VarChar, 101, Record.SpecificationNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Buyer", SqlDbType.VarChar, 9, Record.Buyer)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@WF_Status", SqlDbType.VarChar, 101, Record.WF_Status)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@UserId", SqlDbType.VarChar, 9, Record.UserId)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@DateTime", SqlDbType.DateTime, 21, Record.DateTime)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Supplier", SqlDbType.VarChar, 51, IIf(Record.Supplier = "", Convert.DBNull, Record.Supplier))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@SupplierName", SqlDbType.VarChar, 101, IIf(Record.SupplierName = "", Convert.DBNull, Record.SupplierName))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Notes", SqlDbType.VarChar, 501, IIf(Record.Notes = "", Convert.DBNull, Record.Notes))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@PMDLDocNo", SqlDbType.VarChar, 101, IIf(Record.PMDLDocNo = "", Convert.DBNull, Record.PMDLDocNo))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Manager", SqlDbType.VarChar, 9, IIf(Record.Manager = "", Convert.DBNull, Record.Manager))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IndentNo", SqlDbType.VarChar, 10, IIf(Record.IndentNo = "", Convert.DBNull, Record.IndentNo))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IndentLine", SqlDbType.Int, 11, IIf(Record.IndentLine = "", Convert.DBNull, Record.IndentLine))
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LotItem", SqlDbType.VarChar, 48, IIf(Record.LotItem = "", Convert.DBNull, Record.LotItem))
          Cmd.Parameters.Add("@RowCount", SqlDbType.Int)
          Cmd.Parameters("@RowCount").Direction = ParameterDirection.Output
          _RecordCount = -1
          Con.Open()
          Cmd.ExecuteNonQuery()
          _RecordCount = Cmd.Parameters("@RowCount").Value
        End Using
      End Using
      Return Record
    End Function
    <DataObjectMethod(DataObjectMethodType.Delete, True)> _
    Public Shared Function wfPreOrderHistoryDelete(ByVal Record As SIS.WF.wfPreOrderHistory) As Int32
      Dim _Result as Integer = 0
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "spwfPreOrderHistoryDelete"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Original_WF_HistoryID",SqlDbType.Int,Record.WF_HistoryID.ToString.Length, Record.WF_HistoryID)
          Cmd.Parameters.Add("@RowCount", SqlDbType.Int)
          Cmd.Parameters("@RowCount").Direction = ParameterDirection.Output
          _RecordCount = -1
          Con.Open()
          Cmd.ExecuteNonQuery()
          _RecordCount = Cmd.Parameters("@RowCount").Value
        End Using
      End Using
      Return _RecordCount
    End Function
    Public Sub New(ByVal Reader As SqlDataReader)
      Try
        For Each pi As System.Reflection.PropertyInfo In Me.GetType.GetProperties
          If pi.MemberType = Reflection.MemberTypes.Property Then
            Try
              Dim Found As Boolean = False
              For I As Integer = 0 To Reader.FieldCount - 1
                If Reader.GetName(I).ToLower = pi.Name.ToLower Then
                  Found = True
                  Exit For
                End If
              Next
              If Found Then
                If Convert.IsDBNull(Reader(pi.Name)) Then
                  Select Case Reader.GetDataTypeName(Reader.GetOrdinal(pi.Name))
                    Case "decimal"
                      CallByName(Me, pi.Name, CallType.Let, "0.00")
                    Case "bit"
                      CallByName(Me, pi.Name, CallType.Let, Boolean.FalseString)
                    Case Else
                      CallByName(Me, pi.Name, CallType.Let, String.Empty)
                  End Select
                Else
                  CallByName(Me, pi.Name, CallType.Let, Reader(pi.Name))
                End If
              End If
            Catch ex As Exception
            End Try
          End If
        Next
      Catch ex As Exception
      End Try
    End Sub
    Public Sub New()
    End Sub
  End Class
End Namespace
