Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.TDISG
  <DataObject()> _
  Partial Public Class tdisg003
    Private Shared _RecordCount As Integer
    Private _t_rqno As String = ""
    Private _t_pono As Int32 = 0
    Private _t_item As String = ""
    Private _t_desc As String = ""
    Private _t_qnty As Double = 0
    Private _t_quom As String = ""
    Private _t_wght As Double = 0
    Private _t_slct As Int32 = 0
    Private _t_stat As Int32 = 0
    Private _t_pric As Double = 0
    Private _t_amnt As Double = 0
    Private _t_docn As String = ""
    Private _t_revi As String = ""
    Private _t_Refcntd As Int32 = 0
    Private _t_Refcntu As Int32 = 0
    Private _t_orno As String = ""
    Private _t_pldt As String = ""
    Public Property t_rqno() As String
      Get
        Return _t_rqno
      End Get
      Set(ByVal value As String)
        _t_rqno = value
      End Set
    End Property
    Public Property t_pono() As Int32
      Get
        Return _t_pono
      End Get
      Set(ByVal value As Int32)
        _t_pono = value
      End Set
    End Property
    Public Property t_item() As String
      Get
        Return _t_item
      End Get
      Set(ByVal value As String)
        _t_item = value
      End Set
    End Property
    Public Property t_desc() As String
      Get
        Return _t_desc
      End Get
      Set(ByVal value As String)
        _t_desc = value
      End Set
    End Property
    Public Property t_qnty() As Double
      Get
        Return _t_qnty
      End Get
      Set(ByVal value As Double)
        _t_qnty = value
      End Set
    End Property
    Public Property t_quom() As String
      Get
        Return _t_quom
      End Get
      Set(ByVal value As String)
        _t_quom = value
      End Set
    End Property
    Public Property t_wght() As Double
      Get
        Return _t_wght
      End Get
      Set(ByVal value As Double)
        _t_wght = value
      End Set
    End Property
    Public Property t_slct() As Int32
      Get
        Return _t_slct
      End Get
      Set(ByVal value As Int32)
        _t_slct = value
      End Set
    End Property
    Public Property t_stat() As Int32
      Get
        Return _t_stat
      End Get
      Set(ByVal value As Int32)
        _t_stat = value
      End Set
    End Property
    Public Property t_pric() As Double
      Get
        Return _t_pric
      End Get
      Set(ByVal value As Double)
        _t_pric = value
      End Set
    End Property
    Public Property t_amnt() As Double
      Get
        Return _t_amnt
      End Get
      Set(ByVal value As Double)
        _t_amnt = value
      End Set
    End Property
    Public Property t_docn() As String
      Get
        Return _t_docn
      End Get
      Set(ByVal value As String)
        _t_docn = value
      End Set
    End Property
    Public Property t_revi() As String
      Get
        Return _t_revi
      End Get
      Set(ByVal value As String)
        _t_revi = value
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
    Public Property t_orno() As String
      Get
        Return _t_orno
      End Get
      Set(ByVal value As String)
        _t_orno = value
      End Set
    End Property
    Public Property t_pldt() As String
      Get
        If Not _t_pldt = String.Empty Then
          Return Convert.ToDateTime(_t_pldt).ToString("dd/MM/yyyy")
        End If
        Return _t_pldt
      End Get
      Set(ByVal value As String)
         _t_pldt = value
      End Set
    End Property
    Public Readonly Property DisplayField() As String
      Get
        Return ""
      End Get
    End Property
    Public Readonly Property PrimaryKey() As String
      Get
        Return _t_rqno & "|" & _t_pono & "|" & _t_item
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
    Public Class PKtdisg003
      Private _t_rqno As String = ""
      Private _t_pono As Int32 = 0
      Private _t_item As String = ""
      Public Property t_rqno() As String
        Get
          Return _t_rqno
        End Get
        Set(ByVal value As String)
          _t_rqno = value
        End Set
      End Property
      Public Property t_pono() As Int32
        Get
          Return _t_pono
        End Get
        Set(ByVal value As Int32)
          _t_pono = value
        End Set
      End Property
      Public Property t_item() As String
        Get
          Return _t_item
        End Get
        Set(ByVal value As String)
          _t_item = value
        End Set
      End Property
    End Class
    Public Shared Function tdisg003GetByID(ByVal t_rqno As String, ByVal t_pono As Int32, ByVal t_item As String, Optional ByVal Comp As String = "200") As SIS.TDISG.tdisg003
      Dim Results As SIS.TDISG.tdisg003 = Nothing
      Dim Sql As String = ""
      Sql &= " select "
      Sql &= " * "
      Sql &= " From ttdisg003" & Comp
      Sql &= " where "
      Sql &= " t_rqno = '" & t_rqno & "'"
      Sql &= " and t_pono=" & t_pono
      Sql &= " and t_item='" & t_item & "'"
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          If Reader.Read() Then
            Results = New SIS.TDISG.tdisg003(Reader)
          End If
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function tdisg003SelectList(ByVal t_rqno As String, ByVal t_pono As Int32, Optional ByVal Comp As String = "200") As List(Of SIS.TDISG.tdisg003)
      Dim Results As List(Of SIS.TDISG.tdisg003) = Nothing
      Dim Sql As String = ""
      Sql &= " select "
      Sql &= " * "
      Sql &= " From ttdisg003" & Comp
      Sql &= " where "
      Sql &= " t_rqno = '" & t_rqno & "'"
      Sql &= " and t_pono=" & t_pono
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Results = New List(Of SIS.TDISG.tdisg003)()
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.TDISG.tdisg003(Reader))
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function GetDocument(ByVal t_rqno As String, ByVal t_pono As Int32, Optional ByVal Comp As String = "200") As List(Of SIS.TDISG.tdisg003)
      Dim Results As List(Of SIS.TDISG.tdisg003) = Nothing
      Dim Sql As String = ""
      Sql &= " select distinct t_docn, t_revi "
      Sql &= "  "
      Sql &= " From ttdisg003" & Comp
      Sql &= " where "
      Sql &= " t_rqno = '" & t_rqno & "'"
      Sql &= " and t_pono=" & t_pono
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Results = New List(Of SIS.TDISG.tdisg003)()
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.TDISG.tdisg003(Reader))
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
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
