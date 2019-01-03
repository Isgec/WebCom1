Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.TDPUR
  <DataObject()> _
  Partial Public Class tdpur201
    Private Shared _RecordCount As Integer
    Private _t_rqno As String = ""
    Private _t_pono As Int32 = 0
    Private _t_item As String = ""
    Private _t_nids As String = ""
    Private _t_effn As Int32 = 0
    Private _t_crrf As Int32 = 0
    Private _t_citt As String = ""
    Private _t_crit As String = ""
    Private _t_mpnr As String = ""
    Private _t_cmnf As String = ""
    Private _t_mitm As String = ""
    Private _t_qoor As Double = 0
    Private _t_cuqp As String = ""
    Private _t_cvqp As Double = 0
    Private _t_leng As Double = 0
    Private _t_widt As Double = 0
    Private _t_thic As Double = 0
    Private _t_otbp As String = ""
    Private _t_nsds As String = ""
    Private _t_dldt As String = ""
    Private _t_pric As Double = 0
    Private _t_cupp As String = ""
    Private _t_cvpp As Double = 0
    Private _t_oamt As Double = 0
    Private _t_cwar As String = ""
    Private _t_cadr As String = ""
    Private _t_cprj As String = ""
    Private _t_cspa As String = ""
    Private _t_cact As String = ""
    Private _t_cstl As String = ""
    Private _t_ccco As String = ""
    Private _t_glco As String = ""
    Private _t_rejc As Int32 = 0
    Private _t_rcod As String = ""
    Private _t_urgt As Int32 = 0
    Private _t_cnty As Int32 = 0
    Private _t_bgxc As Int32 = 0
    Private _t_cpla As String = ""
    Private _t_txta As Int32 = 0
    Private _t_cdf_ccty As String = ""
    Private _t_cdf_cdat As String = ""
    Private _t_cdf_cvat As String = ""
    Private _t_cdf_emer As Int32 = 0
    Private _t_cdf_mfgc As Int32 = 0
    Private _t_cdf_norm As Int32 = 0
    Private _t_cdf_pddt As String = ""
    Private _t_cdf_pref As String = ""
    Private _t_cdf_rodt As String = ""
    Private _t_cdf_tvet As Int32 = 0
    Private _t_Refcntd As Int32 = 0
    Private _t_Refcntu As Int32 = 0
    Public Property ProjectName As String = ""
    Public Property ElementName As String = ""
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
    Public Property t_nids() As String
      Get
        Return _t_nids
      End Get
      Set(ByVal value As String)
        _t_nids = value
      End Set
    End Property
    Public Property t_effn() As Int32
      Get
        Return _t_effn
      End Get
      Set(ByVal value As Int32)
        _t_effn = value
      End Set
    End Property
    Public Property t_crrf() As Int32
      Get
        Return _t_crrf
      End Get
      Set(ByVal value As Int32)
        _t_crrf = value
      End Set
    End Property
    Public Property t_citt() As String
      Get
        Return _t_citt
      End Get
      Set(ByVal value As String)
        _t_citt = value
      End Set
    End Property
    Public Property t_crit() As String
      Get
        Return _t_crit
      End Get
      Set(ByVal value As String)
        _t_crit = value
      End Set
    End Property
    Public Property t_mpnr() As String
      Get
        Return _t_mpnr
      End Get
      Set(ByVal value As String)
        _t_mpnr = value
      End Set
    End Property
    Public Property t_cmnf() As String
      Get
        Return _t_cmnf
      End Get
      Set(ByVal value As String)
        _t_cmnf = value
      End Set
    End Property
    Public Property t_mitm() As String
      Get
        Return _t_mitm
      End Get
      Set(ByVal value As String)
        _t_mitm = value
      End Set
    End Property
    Public Property t_qoor() As Double
      Get
        Return _t_qoor
      End Get
      Set(ByVal value As Double)
        _t_qoor = value
      End Set
    End Property
    Public Property t_cuqp() As String
      Get
        Return _t_cuqp
      End Get
      Set(ByVal value As String)
        _t_cuqp = value
      End Set
    End Property
    Public Property t_cvqp() As Double
      Get
        Return _t_cvqp
      End Get
      Set(ByVal value As Double)
        _t_cvqp = value
      End Set
    End Property
    Public Property t_leng() As Double
      Get
        Return _t_leng
      End Get
      Set(ByVal value As Double)
        _t_leng = value
      End Set
    End Property
    Public Property t_widt() As Double
      Get
        Return _t_widt
      End Get
      Set(ByVal value As Double)
        _t_widt = value
      End Set
    End Property
    Public Property t_thic() As Double
      Get
        Return _t_thic
      End Get
      Set(ByVal value As Double)
        _t_thic = value
      End Set
    End Property
    Public Property t_otbp() As String
      Get
        Return _t_otbp
      End Get
      Set(ByVal value As String)
        _t_otbp = value
      End Set
    End Property
    Public Property t_nsds() As String
      Get
        Return _t_nsds
      End Get
      Set(ByVal value As String)
        _t_nsds = value
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
    Public Property t_pric() As Double
      Get
        Return _t_pric
      End Get
      Set(ByVal value As Double)
        _t_pric = value
      End Set
    End Property
    Public Property t_cupp() As String
      Get
        Return _t_cupp
      End Get
      Set(ByVal value As String)
        _t_cupp = value
      End Set
    End Property
    Public Property t_cvpp() As Double
      Get
        Return _t_cvpp
      End Get
      Set(ByVal value As Double)
        _t_cvpp = value
      End Set
    End Property
    Public Property t_oamt() As Double
      Get
        Return _t_oamt
      End Get
      Set(ByVal value As Double)
        _t_oamt = value
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
    Public Property t_cadr() As String
      Get
        Return _t_cadr
      End Get
      Set(ByVal value As String)
        _t_cadr = value
      End Set
    End Property
    Public Property t_cprj() As String
      Get
        Return _t_cprj
      End Get
      Set(ByVal value As String)
        _t_cprj = value
      End Set
    End Property
    Public Property t_cspa() As String
      Get
        Return _t_cspa
      End Get
      Set(ByVal value As String)
        _t_cspa = value
      End Set
    End Property
    Public Property t_cact() As String
      Get
        Return _t_cact
      End Get
      Set(ByVal value As String)
        _t_cact = value
      End Set
    End Property
    Public Property t_cstl() As String
      Get
        Return _t_cstl
      End Get
      Set(ByVal value As String)
        _t_cstl = value
      End Set
    End Property
    Public Property t_ccco() As String
      Get
        Return _t_ccco
      End Get
      Set(ByVal value As String)
        _t_ccco = value
      End Set
    End Property
    Public Property t_glco() As String
      Get
        Return _t_glco
      End Get
      Set(ByVal value As String)
        _t_glco = value
      End Set
    End Property
    Public Property t_rejc() As Int32
      Get
        Return _t_rejc
      End Get
      Set(ByVal value As Int32)
        _t_rejc = value
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
    Public Property t_bgxc() As Int32
      Get
        Return _t_bgxc
      End Get
      Set(ByVal value As Int32)
        _t_bgxc = value
      End Set
    End Property
    Public Property t_cpla() As String
      Get
        Return _t_cpla
      End Get
      Set(ByVal value As String)
        _t_cpla = value
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
    Public Property t_cdf_ccty() As String
      Get
        Return _t_cdf_ccty
      End Get
      Set(ByVal value As String)
        _t_cdf_ccty = value
      End Set
    End Property
    Public Property t_cdf_cdat() As String
      Get
        If Not _t_cdf_cdat = String.Empty Then
          Return Convert.ToDateTime(_t_cdf_cdat).ToString("dd/MM/yyyy")
        End If
        Return _t_cdf_cdat
      End Get
      Set(ByVal value As String)
         _t_cdf_cdat = value
      End Set
    End Property
    Public Property t_cdf_cvat() As String
      Get
        Return _t_cdf_cvat
      End Get
      Set(ByVal value As String)
        _t_cdf_cvat = value
      End Set
    End Property
    Public Property t_cdf_emer() As Int32
      Get
        Return _t_cdf_emer
      End Get
      Set(ByVal value As Int32)
        _t_cdf_emer = value
      End Set
    End Property
    Public Property t_cdf_mfgc() As Int32
      Get
        Return _t_cdf_mfgc
      End Get
      Set(ByVal value As Int32)
        _t_cdf_mfgc = value
      End Set
    End Property
    Public Property t_cdf_norm() As Int32
      Get
        Return _t_cdf_norm
      End Get
      Set(ByVal value As Int32)
        _t_cdf_norm = value
      End Set
    End Property
    Public Property t_cdf_pddt() As String
      Get
        If Not _t_cdf_pddt = String.Empty Then
          Return Convert.ToDateTime(_t_cdf_pddt).ToString("dd/MM/yyyy")
        End If
        Return _t_cdf_pddt
      End Get
      Set(ByVal value As String)
         _t_cdf_pddt = value
      End Set
    End Property
    Public Property t_cdf_pref() As String
      Get
        Return _t_cdf_pref
      End Get
      Set(ByVal value As String)
        _t_cdf_pref = value
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
    Public Property t_cdf_tvet() As Int32
      Get
        Return _t_cdf_tvet
      End Get
      Set(ByVal value As Int32)
        _t_cdf_tvet = value
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
        Return _t_rqno & "|" & _t_pono
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
    Public Class PKtdpur201
      Private _t_rqno As String = ""
      Private _t_pono As Int32 = 0
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
    End Class
    Public Shared Function tdpur201GetByID(ByVal t_rqno As String, ByVal t_pono As Int32, Optional ByVal Comp As String = "200") As SIS.TDPUR.tdpur201
      Dim Results As SIS.TDPUR.tdpur201 = Nothing
      Dim Sql As String = ""
      Sql &= " select "
      Sql &= " aa.*, "
      Sql &= " prj.t_dsca as ProjectName, "
      Sql &= " ele.t_desc as ElementName  "
      Sql &= " From ttdpur201" & Comp & " as aa "
      Sql &= " inner join ttcmcs052" & Comp & " as prj on prj.t_cprj=aa.t_cprj "
      Sql &= " inner join ttpptc100" & Comp & " as ele on ele.t_cprj=aa.t_cprj and ele.t_cspa=aa.t_cspa "
      Sql &= " where "
      Sql &= " aa.t_rqno = '" & t_rqno & "'"
      Sql &= " and aa.t_pono=" & t_pono
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          If Reader.Read() Then
            Results = New SIS.TDPUR.tdpur201(Reader)
          End If
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function GetByRQNo(ByVal t_rqno As String, Optional ByVal Comp As String = "200") As List(Of SIS.TDPUR.tdpur201)
      Dim Results As List(Of SIS.TDPUR.tdpur201) = Nothing
      Dim Sql As String = ""
      Sql &= " select "
      Sql &= " aa.*, "
      Sql &= " prj.t_dsca as ProjectName, "
      Sql &= " ele.t_desc as ElementName  "
      Sql &= " From ttdpur201" & Comp & " as aa "
      Sql &= " inner join ttcmcs052" & Comp & " as prj on prj.t_cprj=aa.t_cprj "
      Sql &= " inner join ttpptc100" & Comp & " as ele on ele.t_cprj=aa.t_cprj and ele.t_cspa=aa.t_cspa "
      Sql &= " where "
      Sql &= " aa.t_rqno = '" & t_rqno & "'"
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Results = New List(Of SIS.TDPUR.tdpur201)()
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.TDPUR.tdpur201(Reader))
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
