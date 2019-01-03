Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.TDPUR
  <DataObject()> _
  Partial Public Class tdpur200
    Private Shared _RecordCount As Integer
    Private _t_rqno As String = ""
    Private _t_orig As Int32 = 0
    Private _t_remn As String = ""
    Private _t_rdep As String = ""
    Private _t_cofc As String = ""
    Private _t_rdat As String = ""
    Private _t_aemn As String = ""
    Private _t_adep As String = ""
    Private _t_ltdt As String = ""
    Private _t_rqst As Int32 = 0
    Private _t_conv As Int32 = 0
    Private _t_cwar As String = ""
    Private _t_dadr As String = ""
    Private _t_dldt As String = ""
    Private _t_refa As String = ""
    Private _t_refb As String = ""
    Private _t_logn As String = ""
    Private _t_ccur As String = ""
    Private _t_ccon As String = ""
    Private _t_urgt As Int32 = 0
    Private _t_cnty As Int32 = 0
    Private _t_spap As Int32 = 0
    Private _t_rcod As String = ""
    Private _t_ccty As String = ""
    Private _t_txta As Int32 = 0
    Private _t_cdf_rodt As String = ""
    Private _t_Refcntd As Int32 = 0
    Private _t_Refcntu As Int32 = 0
    Public Property t_rqno() As String
      Get
        Return _t_rqno
      End Get
      Set(ByVal value As String)
        _t_rqno = value
      End Set
    End Property
    Public Property t_orig() As Int32
      Get
        Return _t_orig
      End Get
      Set(ByVal value As Int32)
        _t_orig = value
      End Set
    End Property
    Public Property t_remn() As String
      Get
        Return _t_remn
      End Get
      Set(ByVal value As String)
        _t_remn = value
      End Set
    End Property
    Public Property t_rdep() As String
      Get
        Return _t_rdep
      End Get
      Set(ByVal value As String)
        _t_rdep = value
      End Set
    End Property
    Public Property t_cofc() As String
      Get
        Return _t_cofc
      End Get
      Set(ByVal value As String)
        _t_cofc = value
      End Set
    End Property
    Public Property t_rdat() As String
      Get
        If Not _t_rdat = String.Empty Then
          Return Convert.ToDateTime(_t_rdat).ToString("dd/MM/yyyy")
        End If
        Return _t_rdat
      End Get
      Set(ByVal value As String)
         _t_rdat = value
      End Set
    End Property
    Public Property t_aemn() As String
      Get
        Return _t_aemn
      End Get
      Set(ByVal value As String)
        _t_aemn = value
      End Set
    End Property
    Public Property t_adep() As String
      Get
        Return _t_adep
      End Get
      Set(ByVal value As String)
        _t_adep = value
      End Set
    End Property
    Public Property t_ltdt() As String
      Get
        If Not _t_ltdt = String.Empty Then
          Return Convert.ToDateTime(_t_ltdt).ToString("dd/MM/yyyy")
        End If
        Return _t_ltdt
      End Get
      Set(ByVal value As String)
         _t_ltdt = value
      End Set
    End Property
    Public Property t_rqst() As Int32
      Get
        Return _t_rqst
      End Get
      Set(ByVal value As Int32)
        _t_rqst = value
      End Set
    End Property
    Public Property t_conv() As Int32
      Get
        Return _t_conv
      End Get
      Set(ByVal value As Int32)
        _t_conv = value
      End Set
    End Property
    Public Property t_cwar() As String
      Get
        Return _t_cwar
      End Get
      Set(ByVal value As String)
        _t_cwar = value
      End Set
    End Property
    Public Property t_dadr() As String
      Get
        Return _t_dadr
      End Get
      Set(ByVal value As String)
        _t_dadr = value
      End Set
    End Property
    Public Property t_dldt() As String
      Get
        If Not _t_dldt = String.Empty Then
          Return Convert.ToDateTime(_t_dldt).ToString("dd/MM/yyyy")
        End If
        Return _t_dldt
      End Get
      Set(ByVal value As String)
         _t_dldt = value
      End Set
    End Property
    Public Property t_refa() As String
      Get
        Return _t_refa
      End Get
      Set(ByVal value As String)
        _t_refa = value
      End Set
    End Property
    Public Property t_refb() As String
      Get
        Return _t_refb
      End Get
      Set(ByVal value As String)
        _t_refb = value
      End Set
    End Property
    Public Property t_logn() As String
      Get
        Return _t_logn
      End Get
      Set(ByVal value As String)
        _t_logn = value
      End Set
    End Property
    Public Property t_ccur() As String
      Get
        Return _t_ccur
      End Get
      Set(ByVal value As String)
        _t_ccur = value
      End Set
    End Property
    Public Property t_ccon() As String
      Get
        Return _t_ccon
      End Get
      Set(ByVal value As String)
        _t_ccon = value
      End Set
    End Property
    Public Property t_urgt() As Int32
      Get
        Return _t_urgt
      End Get
      Set(ByVal value As Int32)
        _t_urgt = value
      End Set
    End Property
    Public Property t_cnty() As Int32
      Get
        Return _t_cnty
      End Get
      Set(ByVal value As Int32)
        _t_cnty = value
      End Set
    End Property
    Public Property t_spap() As Int32
      Get
        Return _t_spap
      End Get
      Set(ByVal value As Int32)
        _t_spap = value
      End Set
    End Property
    Public Property t_rcod() As String
      Get
        Return _t_rcod
      End Get
      Set(ByVal value As String)
        _t_rcod = value
      End Set
    End Property
    Public Property t_ccty() As String
      Get
        Return _t_ccty
      End Get
      Set(ByVal value As String)
        _t_ccty = value
      End Set
    End Property
    Public Property t_txta() As Int32
      Get
        Return _t_txta
      End Get
      Set(ByVal value As Int32)
        _t_txta = value
      End Set
    End Property
    Public Property t_cdf_rodt() As String
      Get
        If Not _t_cdf_rodt = String.Empty Then
          Return Convert.ToDateTime(_t_cdf_rodt).ToString("dd/MM/yyyy")
        End If
        Return _t_cdf_rodt
      End Get
      Set(ByVal value As String)
         _t_cdf_rodt = value
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
    Public Readonly Property DisplayField() As String
      Get
        Return ""
      End Get
    End Property
    Public Readonly Property PrimaryKey() As String
      Get
        Return _t_rqno
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
    Public Class PKtdpur200
      Private _t_rqno As String = ""
      Public Property t_rqno() As String
        Get
          Return _t_rqno
        End Get
        Set(ByVal value As String)
          _t_rqno = value
        End Set
      End Property
    End Class
    Public Shared Function tdpur200GetByID(ByVal t_rqno As String, Optional ByVal Comp As String = "200") As SIS.TDPUR.tdpur200
      Dim Results As SIS.TDPUR.tdpur200 = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "select * from ttdpur200" & Comp & " where t_rqno='" & t_rqno & "'"
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          If Reader.Read() Then
            Results = New SIS.TDPUR.tdpur200(Reader)
          End If
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
