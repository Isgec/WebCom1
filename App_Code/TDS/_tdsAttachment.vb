Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports ejiVault
Namespace SIS.TDS
  <DataObject()>
  Partial Public Class tdsAttachment
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
    Public ReadOnly Property GetDownloadLink As String
      Get
        Dim Authority As String = HttpContext.Current.Request.Url.Authority
        'Commented as handled while download
        'If Authority.ToLower = "localhost" Then Authority = "192.9.200.146"
        Dim tmpURL As String = HttpContext.Current.Request.Url.Scheme & Uri.SchemeDelimiter & Authority & HttpContext.Current.Request.ApplicationPath
        'Return "javascript:window.open('" & tmpURL & "/DM_mMain/App_Downloads/download.aspx?doc=" & PrimaryKey & "', 'win" & t_docn & "', 'left=20,top=20,width=100,height=100,toolbar=1,resizable=1,scrollbars=1'); return false;"
        Return tmpURL & "/TDS/App_Downloads/download.aspx?doc=" & t_indx
      End Get
    End Property

    Public Property ForeColor As System.Drawing.Color
    Public ReadOnly Property CardNo As String
      Get
        Return t_indx.Split("_".ToCharArray)(0)
      End Get
    End Property
    Public ReadOnly Property EmployeeName As String
      Get
        Dim mRet As String = ""
        Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
          Using Cmd As SqlCommand = Con.CreateCommand()
            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = "Select isnull(employeename,'') from hrm_employees where cardno ='" & CardNo & "'"
            Con.Open()
            mRet = Cmd.ExecuteScalar()
          End Using
        End Using
        Return mRet
      End Get
    End Property
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
    Public Shared Function GetByIndex(ByVal t_indx As String, Optional ByVal Comp As String = "200") As eji.ediAFile
      Dim t_hndl As String = "J_INVESTMENT_DECLARATION"
      Dim Results As EJI.ediAFile = EJI.ediAFile.GetFileByHandleIndex(t_hndl, t_indx)
      'Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
      '  Using Cmd As SqlCommand = Con.CreateCommand()
      '    Cmd.CommandType = CommandType.Text
      '    Cmd.CommandText = "Select top 1 * from ttcisg132" & Comp & " where t_hndl='" & t_hndl & "' and t_indx='" & t_indx & "'"
      '    Con.Open()
      '    Dim Reader As SqlDataReader = Cmd.ExecuteReader()
      '    If Reader.Read() Then
      '      Results = New SIS.EDI.ediAFile(Reader)
      '    End If
      '    Reader.Close()
      '  End Using
      'End Using
      Return Results
    End Function
    Public Shared Function GetByUserFinYear(ByVal CardNo As String, ByVal FinYear As String) As List(Of SIS.TDS.tdsAttachment)
      Dim t_hndl As String = "J_INVESTMENT_DECLARATION"
      Dim Results As List(Of SIS.TDS.tdsAttachment) = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "Select * from ttcisg132200 where t_hndl='" & t_hndl & "' and t_indx='" & CardNo & "_" & FinYear & "'"
          Results = New List(Of SIS.TDS.tdsAttachment)()
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.TDS.tdsAttachment(Reader))
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function

    Public Shared Function SelectList(ByVal StartRowIndex As Integer, ByVal MaximumRows As Integer, ByVal OrderBy As String, ByVal SearchState As Boolean, ByVal SearchText As String) As List(Of SIS.TDS.tdsAttachment)
      Dim Results As List(Of SIS.TDS.tdsAttachment) = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          If SearchState Then
            Cmd.CommandText = "sptdsAttachmentSelectListSearch"
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@KeyWord", SqlDbType.NVarChar, 250, SearchText)
          Else
            Cmd.CommandText = "sptdsAttachmentSelectListFilteres"
          End If
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@StartRowIndex", SqlDbType.Int, -1, StartRowIndex)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@MaximumRows", SqlDbType.Int, -1, MaximumRows)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@FinYear", SqlDbType.NVarChar, 50, "2018-19")
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@OrderBy", SqlDbType.NVarChar, 50, OrderBy)
          Cmd.Parameters.Add("@RecordCount", SqlDbType.Int)
          Cmd.Parameters("@RecordCount").Direction = ParameterDirection.Output
          _RecordCount = -1
          Results = New List(Of SIS.TDS.tdsAttachment)()
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.TDS.tdsAttachment(Reader))
          End While
          Reader.Close()
          _RecordCount = Cmd.Parameters("@RecordCount").Value
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function SelectCount(ByVal SearchState As Boolean, ByVal SearchText As String) As Integer
      Return _RecordCount
    End Function
    Private Class tmpEmp
      Private _CardNo As String = ""
      Public Property CardNo As String
        Get
          Return _CardNo
        End Get
        Set(value As String)
          If Not Convert.IsDBNull(value) Then
            _CardNo = value.Split("_".ToCharArray)(0)
          End If
        End Set
      End Property
      Sub New(ByVal crd As String)
        CardNo = crd
      End Sub
    End Class
    Public Shared Function SelectRefresh() As Boolean
      Dim Results As Boolean = False
      Dim tmp As New List(Of tmpEmp)
      Dim FinYear As String = ConfigurationManager.AppSettings("TDSFinYear").ToString
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "select t_indx as CardNo from ttcisg132200 where t_hndl like 'J_INVESTMENT_DECLARATION' and right(rtrim(t_indx),9)='" & FinYear & "' COLLATE Latin1_General_BIN2"
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Dim x As tmpEmp = New tmpEmp(Reader("CardNo"))
            Dim y As tmpEmp = tmp.Find(Function(p) p.CardNo = x.CardNo)
            If y Is Nothing Then
              tmp.Add(x)
            End If
          End While
          Reader.Close()
        End Using
      End Using
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Con.Open()
        For Each s As tmpEmp In tmp
          Try
            Using Cmd As SqlCommand = Con.CreateCommand()
              Cmd.CommandType = CommandType.Text
              Cmd.CommandText = "Insert Into TDS_AttachmentUser (CardNo,FinYear) Values ('" & s.CardNo & "','" & FinYear & "')"
              Cmd.ExecuteNonQuery()
            End Using
          Catch ex As Exception
          End Try
        Next
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
