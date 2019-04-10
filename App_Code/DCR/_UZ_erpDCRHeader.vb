Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.ERP
  Partial Public Class erpDCRHeader
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
    Public Shared Function InitiateWF(ByVal DCRNo As String) As SIS.ERP.erpDCRHeader
      Dim Results As SIS.ERP.erpDCRHeader = erpDCRHeaderGetByID(DCRNo)
      Return Results
    End Function
    <DataObjectMethod(DataObjectMethodType.Select)> _
    Public Shared Function BaaNDCRHeaderSelectList(ByVal DCRNo As String) As SIS.ERP.erpDCRHeader
      Dim mSql As String = ""
      mSql = mSql & "select "
      mSql = mSql & "dcrh.t_dcrn as DCRNo,"
      mSql = mSql & "dcrh.t_apdt as DCRDate, "
      mSql = mSql & "dcrh.t_dsca as DCRDescription,"
      mSql = mSql & "dcrh.t_crea as CreatedBy,"
      mSql = mSql & "dcrh.t_cprj as ProjectID,"
      mSql = mSql & "emp1.t_nama as CreatedName,"
      mSql = mSql & "bpe1.t_mail as CreatedEMail,"
      mSql = mSql & "adr1.t_dsca as ProjectDescription "
      mSql = mSql & "from tdmisg114200 dcrh "
      mSql = mSql & "left outer join ttccom001200 as emp1 on dcrh.t_crea=emp1.t_emno "
      mSql = mSql & "left outer join tbpmdm001200 as bpe1 on dcrh.t_crea=bpe1.t_emno "
      mSql = mSql & "left outer join ttcmcs052200 as adr1 on dcrh.t_cprj=adr1.t_cprj "
      mSql = mSql & "where dcrh.t_dcrn = '" & DCRNo & "'"
      Dim Results As SIS.ERP.erpDCRHeader = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = mSql
          _RecordCount = -1
          Results = New SIS.ERP.erpDCRHeader
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results = New SIS.ERP.erpDCRHeader(Reader)
            Exit While
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
  End Class
End Namespace
