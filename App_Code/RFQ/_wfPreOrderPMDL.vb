Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.WF
  <DataObject()> _
  Partial Public Class wfPreOrderPMDL
    Private Shared _RecordCount As Integer
    Private _WFID As Int32 = 0
    Private _PMDLDocNo As String = ""
    Public Property WFID() As Int32
      Get
        Return _WFID
      End Get
      Set(ByVal value As Int32)
        _WFID = value
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
    Public Readonly Property DisplayField() As String
      Get
        Return ""
      End Get
    End Property
    Public Readonly Property PrimaryKey() As String
      Get
        Return _WFID & "|" & _PMDLDocNo
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
    Public Class PKwfPreOrderPMDL
      Private _WFID As Int32 = 0
      Private _PMDLDocNo As String = ""
      Public Property WFID() As Int32
        Get
          Return _WFID
        End Get
        Set(ByVal value As Int32)
          _WFID = value
        End Set
      End Property
      Public Property PMDLDocNo() As String
        Get
          Return _PMDLDocNo
        End Get
        Set(ByVal value As String)
          _PMDLDocNo = value
        End Set
      End Property
    End Class
    Public Shared Function wfPreOrderPMDLGetByID(ByVal WFID As Int32, ByVal PMDLDocNo As String) As SIS.WF.wfPreOrderPMDL
      Dim Results As SIS.WF.wfPreOrderPMDL = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "spwfPreOrderPMDLSelectByID"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@WFID", SqlDbType.Int, WFID.ToString.Length, WFID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@PMDLDocNo", SqlDbType.VarChar, PMDLDocNo.ToString.Length, PMDLDocNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LoginID", SqlDbType.NVarChar, 9, HttpContext.Current.Session("LoginID"))
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          If Reader.Read() Then
            Results = New SIS.WF.wfPreOrderPMDL(Reader)
          End If
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function GetByWFID(ByVal WFID As Integer) As List(Of SIS.WF.wfPreOrderPMDL)
      Dim Results As List(Of SIS.WF.wfPreOrderPMDL) = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "select * from WF1_PreOrderPMDL where wfid=" & WFID
          Results = New List(Of SIS.WF.wfPreOrderPMDL)()
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.WF.wfPreOrderPMDL(Reader))
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function InsertData(ByVal Record As SIS.WF.wfPreOrderPMDL) As SIS.WF.wfPreOrderPMDL
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "spwfPreOrderPMDLInsert"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@WFID", SqlDbType.Int, 11, Record.WFID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@PMDLDocNo", SqlDbType.VarChar, 101, IIf(Record.PMDLDocNo = "", Convert.DBNull, Record.PMDLDocNo))
          Cmd.Parameters.Add("@Return_WFID", SqlDbType.Int, 11)
          Cmd.Parameters("@Return_WFID").Direction = ParameterDirection.Output
          Cmd.Parameters.Add("@Return_PMDLDocNo", SqlDbType.VarChar, 101)
          Cmd.Parameters("@Return_PMDLDocNo").Direction = ParameterDirection.Output
          Con.Open()
          Cmd.ExecuteNonQuery()
          Record.WFID = Cmd.Parameters("@Return_WFID").Value
          Record.PMDLDocNo = Cmd.Parameters("@Return_PMDLDocNo").Value
        End Using
      End Using
      Return Record
    End Function
    Public Shared Function UpdateData(ByVal Record As SIS.WF.wfPreOrderPMDL) As SIS.WF.wfPreOrderPMDL
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "spwfPreOrderPMDLUpdate"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Original_WFID", SqlDbType.Int, 11, Record.WFID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Original_PMDLDocNo", SqlDbType.VarChar, 101, Record.PMDLDocNo)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@WFID", SqlDbType.Int, 11, Record.WFID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@PMDLDocNo", SqlDbType.VarChar, 101, IIf(Record.PMDLDocNo = "", Convert.DBNull, Record.PMDLDocNo))
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
    Public Shared Function wfPreOrderPMDLDelete(ByVal Record As SIS.WF.wfPreOrderPMDL) As Int32
      Dim _Result as Integer = 0
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "spwfPreOrderPMDLDelete"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Original_WFID",SqlDbType.Int,Record.WFID.ToString.Length, Record.WFID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Original_PMDLDocNo",SqlDbType.VarChar,Record.PMDLDocNo.ToString.Length, Record.PMDLDocNo)
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
