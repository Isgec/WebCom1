Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.ERP
  Partial Public Class erpDCRDetail
		Public Function GetColor() As System.Drawing.Color
			Dim mRet As System.Drawing.Color = Drawing.Color.Blue
			Return mRet
		End Function
		Public Function GetVisible() As Boolean
			Dim mRet As Boolean = True
			Return mRet
		End Function
		Public Function GetEnable() As Boolean
			Dim mRet As Boolean = True
			Return mRet
		End Function
    Public ReadOnly Property InitiateWFVisible() As Boolean
      Get
        Dim mRet As Boolean = True
        Try
					mRet = GetVisible()
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public ReadOnly Property InitiateWFEnable() As Boolean
      Get
        Dim mRet As Boolean = True
        Try
					mRet = GetEnable()
        Catch ex As Exception
        End Try
        Return mRet
      End Get
    End Property
    Public Shared Function InitiateWF(ByVal DCRNo As String, ByVal DocumentID As String, ByVal RevisionNo As String) As SIS.ERP.erpDCRDetail
      Dim Results As SIS.ERP.erpDCRDetail = erpDCRDetailGetByID(DCRNo, DocumentID, RevisionNo)
      Return Results
    End Function
    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function BaaNDCRDetailSelectList(ByVal DCRNo As String) As List(Of SIS.ERP.erpDCRDetail)
      Dim mSql As String = ""
      mSql = mSql & "select distinct "
      mSql = mSql & "dcrd.t_dcrn as DCRNo,"
      mSql = mSql & "dcrd.t_docd as DocumentID,"
      mSql = mSql & "dcrd.t_revn as RevisionNo,"
      mSql = mSql & "docm.t_dttl as DocumentTitle,"
      mSql = mSql & "reqc.t_rqno as IndentNo,"
      mSql = mSql & "reqc.t_pono as IndentLine,"
      mSql = mSql & "reql.t_item as LotItem,"
      mSql = mSql & "reql.t_nids as ItemDescription,"
      mSql = mSql & "reqh.t_remn as IndenterID, "
      mSql = mSql & "reqh.t_ccon as BuyerID,"
      mSql = mSql & "reqp.t_prno as OrderNo,"
      mSql = mSql & "reqp.t_ppon as OrderLine, "
      mSql = mSql & "ordh.t_otbp as SupplierID,"
      mSql = mSql & "ordh.t_ccon as BuyerIDinPO,    "
      mSql = mSql & "emp1.t_nama as IndenterName,"
      mSql = mSql & "bpe1.t_mail as IndenterEMail, "
      mSql = mSql & "emp2.t_nama as BuyerName,"
      mSql = mSql & "bpe2.t_mail as BuyerEMail, "
      mSql = mSql & "emp3.t_nama as BuyerIDinPOName,"
      mSql = mSql & "bpe3.t_mail as BuyerIDinPOEMail, "
      mSql = mSql & "bp01.t_nama as SupplierName "
      mSql = mSql & "from tdmisg115200 as dcrd "
      mSql = mSql & "left outer join tdmisg001200 as docm on (dcrd.t_docd = docm.t_docn and dcrd.t_revn = docm.t_revn) "
      mSql = mSql & "left outer join ttdisg003200 as reqc on (dcrd.t_docd = reqc.t_docn and dcrd.t_revn = reqc.t_revi) "
      mSql = mSql & "left outer join ttdpur201200 as reql on (reqc.t_rqno = reql.t_rqno and reqc.t_pono = reql.t_pono) "
      mSql = mSql & "left outer join ttdpur200200 as reqh on (reql.t_rqno = reqh.t_rqno) "
      mSql = mSql & "left outer join ttdpur202200 as reqp on (reqc.t_rqno = reqp.t_rqno and reqc.t_pono = reqp.t_pono ) "
      mSql = mSql & "left outer join ttdpur400200 as ordh on (reqp.t_prno = ordh.t_orno ) "
      mSql = mSql & "left outer join ttccom001200 as emp1 on reqh.t_remn=emp1.t_emno "
      mSql = mSql & "left outer join tbpmdm001200 as bpe1 on reqh.t_remn=bpe1.t_emno "
      mSql = mSql & "left outer join ttccom001200 as emp2 on reqh.t_ccon=emp2.t_emno "
      mSql = mSql & "left outer join tbpmdm001200 as bpe2 on reqh.t_ccon=bpe2.t_emno "
      mSql = mSql & "left outer join ttccom001200 as emp3 on ordh.t_ccon=emp3.t_emno "
      mSql = mSql & "left outer join tbpmdm001200 as bpe3 on ordh.t_ccon=bpe3.t_emno "
      mSql = mSql & "left outer join ttccom100200 as bp01 on ordh.t_otbp=bp01.t_bpid "
      mSql = mSql & "where dcrd.t_dcrn = '" & DCRNo & "'"

      Dim Results As List(Of SIS.ERP.erpDCRDetail) = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = mSql
          _RecordCount = -1
          Results = New List(Of SIS.ERP.erpDCRDetail)()
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.ERP.erpDCRDetail(Reader))
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function erpDCRDocument(ByVal DocumentID As String, ByVal RevisionNo As String) As SIS.ERP.erpDCRDetail
      Dim Results As SIS.ERP.erpDCRDetail = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.StoredProcedure
          Cmd.CommandText = "sperp_LG_DCRDocument"
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@DocumentID", SqlDbType.NVarChar, 30, DocumentID)
          SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@RevisionNo", SqlDbType.NVarChar, 5, RevisionNo)
          _RecordCount = -1
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results = New SIS.ERP.erpDCRDetail(Reader)
            Exit While
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Public ReadOnly Property GetSDownloadLink() As String
      Get
        Return "javascript:window.open('" & HttpContext.Current.Request.Url.Scheme & Uri.SchemeDelimiter & HttpContext.Current.Request.Url.Authority & HttpContext.Current.Request.ApplicationPath & "/DCR/download.aspx?DownloadKey=" & DownloadKey & "', 'win" & DocumentID & "', 'left=20,top=20,width=100,height=100,toolbar=1,resizable=1,scrollbars=1'); return false;"
      End Get
    End Property
    Public Property DownloadText As String = DocumentID & ".PDF Rev.:" & NextRevision
    Public Shared Function GetByDocumentKey(ByVal DownloadKey As String) As SIS.ERP.erpDCRDetail
      Dim Results As SIS.ERP.erpDCRDetail = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "select top 1 * from ERP_DCRDetail where DownloadKey='" & DownloadKey & "'"
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results = New SIS.ERP.erpDCRDetail(Reader)
            Exit While
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function

  End Class
End Namespace
