Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.EDI
  <DataObject()>
  Partial Public Class ediAFile
    Private Shared _RecordCount As Integer
    Private _t_drid As String = ""
    Private _t_dcid As String = ""
    Private _t_hndl As String = ""
    Private _t_indx As String = ""
    Private _t_prcd As String = ""
    Private _t_fnam As String = ""
    Private _t_lbcd As String = ""
    Private _t_atby As String = ""
    Private _t_aton As String = ""
    Private _t_Refcntd As Int32 = 0
    Private _t_Refcntu As Int32 = 0
    Public Property t_drid() As String
      Get
        Return _t_drid
      End Get
      Set(ByVal value As String)
        _t_drid = value
      End Set
    End Property
    Public Property t_dcid() As String
      Get
        Return _t_dcid
      End Get
      Set(ByVal value As String)
        _t_dcid = value
      End Set
    End Property
    Public Property t_hndl() As String
      Get
        Return _t_hndl
      End Get
      Set(ByVal value As String)
        _t_hndl = value
      End Set
    End Property
    Public Property t_indx() As String
      Get
        Return _t_indx
      End Get
      Set(ByVal value As String)
        _t_indx = value
      End Set
    End Property
    Public Property t_prcd() As String
      Get
        Return _t_prcd
      End Get
      Set(ByVal value As String)
        _t_prcd = value
      End Set
    End Property
    Public Property t_fnam() As String
      Get
        Return _t_fnam
      End Get
      Set(ByVal value As String)
        _t_fnam = value
      End Set
    End Property
    Public Property t_lbcd() As String
      Get
        Return _t_lbcd
      End Get
      Set(ByVal value As String)
        _t_lbcd = value
      End Set
    End Property
    Public Property t_atby() As String
      Get
        Return _t_atby
      End Get
      Set(ByVal value As String)
        _t_atby = value
      End Set
    End Property
    Public Property t_aton() As String
      Get
        If Not _t_aton = String.Empty Then
          Return Convert.ToDateTime(_t_aton).ToString("dd/MM/yyyy")
        End If
        Return _t_aton
      End Get
      Set(ByVal value As String)
        _t_aton = value
      End Set
    End Property
    Public Property t_Refcntd() As Int32
      Get
        Return _t_Refcntd
      End Get
      Set(ByVal value As Int32)
        _t_Refcntd = value
      End Set
    End Property
    Public Property t_Refcntu() As Int32
      Get
        Return _t_Refcntu
      End Get
      Set(ByVal value As Int32)
        _t_Refcntu = value
      End Set
    End Property
    Public Shared Function GetNextFileName(Optional ByVal Comp As String = "200") As String
      Dim UniqueFound As Boolean = False
      Dim tmpNextNo As String = (New Random(Guid.NewGuid().GetHashCode())).Next()
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Con.Open()
        Do While Not UniqueFound
          Using Cmd As SqlCommand = Con.CreateCommand()
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = "select isnull(count(*),0) from ttcisg132" & Comp & " where t_drid = '" & tmpNextNo & "'"
            Dim cnt As Integer = Cmd.ExecuteScalar()
            If cnt = 0 Then
              UniqueFound = True
              Exit Do
            End If
            tmpNextNo = (New Random(Guid.NewGuid().GetHashCode())).Next()
          End Using
        Loop
      End Using
      Return tmpNextNo
    End Function
    Public Shared Function ediAFileGetByHandleIndex(ByVal t_hndl As String, ByVal t_indx As String, Optional ByVal Comp As String = "200") As SIS.EDI.ediAFile
      Dim Results As SIS.EDI.ediAFile = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "Select top 1 * from ttcisg132" & Comp & " where t_hndl='" & t_hndl & "' and t_indx='" & t_indx & "'"
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          If Reader.Read() Then
            Results = New SIS.EDI.ediAFile(Reader)
          End If
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function InsertData(ByVal Record As SIS.EDI.ediAFile, Optional ByVal Comp As String = "200") As SIS.EDI.ediAFile
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "spediAFileInsert"
          If Comp <> "200" Then Cmd.CommandText = "spediAFileInsert" & Comp
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@t_dcid", SqlDbType.VarChar, 201, Record.t_dcid)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@t_hndl", SqlDbType.VarChar, 201, Record.t_hndl)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@t_indx", SqlDbType.VarChar, 51, Record.t_indx)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@t_prcd", SqlDbType.VarChar, 51, Record.t_prcd)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@t_fnam", SqlDbType.VarChar, 251, Record.t_fnam)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@t_lbcd", SqlDbType.VarChar, 51, Record.t_lbcd)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@t_atby", SqlDbType.VarChar, 51, Record.t_atby)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@t_aton", SqlDbType.DateTime, 21, Record.t_aton)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@t_Refcntd", SqlDbType.Int, 11, Record.t_Refcntd)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@t_Refcntu", SqlDbType.Int, 11, Record.t_Refcntu)
          Cmd.Parameters.Add("@Return_t_drid", SqlDbType.VarChar, 51)
          Cmd.Parameters("@Return_t_drid").Direction = ParameterDirection.Output
          Con.Open()
          Cmd.ExecuteNonQuery()
          Record.t_drid = Cmd.Parameters("@Return_t_drid").Value
        End Using
      End Using
      Return Record
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
