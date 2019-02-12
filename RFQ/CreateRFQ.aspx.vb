Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel

Partial Class CreateRFQ
  Inherits System.Web.UI.Page
  Private IndentNo As String = ""
  Private Comp As String = ""
  Private Sub CreateRFQ_Load(sender As Object, e As EventArgs) Handles Me.Load
    If Request.QueryString("IndentNo") IsNot Nothing Then
      IndentNo = Request.QueryString("IndentNo")
    End If
    If IndentNo = "" Then Exit Sub
    Comp = SIS.RFQ.rfqGeneral.GetERPCompanyByIndentNo(IndentNo)
    '1. Get Indent From ERP
    Dim Indent As SIS.TDPUR.tdpur200 = SIS.TDPUR.tdpur200.tdpur200GetByID(IndentNo, Comp)
    If Indent.t_rqst <> 3 Then
      ShowMsg("Indent must be approved.")
      Exit Sub
    End If
    '2. Get Indent Lines From ERP
    Dim IndentLines As List(Of SIS.TDPUR.tdpur201) = SIS.TDPUR.tdpur201.GetByRQNo(IndentNo, Comp)
    '3. Check RFQ for All Indent Lines
    Dim CreateWFID As String = ""
    For Each IndentLine As SIS.TDPUR.tdpur201 In IndentLines
      Dim tmp As SIS.WF.wfPreOrder = SIS.WF.wfPreOrder.GetByIndentLine(IndentLine.t_rqno, IndentLine.t_pono)
      '==============Check, If there is child Items then only Create=================
      Dim tmpDocs As List(Of SIS.TDISG.tdisg003) = SIS.TDISG.tdisg003.GetDocument(IndentLine.t_rqno, IndentLine.t_pono)
      If tmpDocs.Count <= 0 Then
        Continue For
      End If
      '===============End Of Check===================================================
      If tmp Is Nothing Then
        '4. Create NEW WFID
        Dim newWF As New SIS.WF.wfPreOrder
        With newWF
          .Buyer = IIf(Indent.t_ccon.Length < 4, Right("0000" & Indent.t_ccon, 4), Indent.t_ccon)
          .DateTime = Now
          .Element = IndentLine.t_cspa & "-" & IndentLine.ElementName
          .IndentLine = IndentLine.t_pono
          .IndentNo = IndentLine.t_rqno
          .LotItem = IndentLine.t_item
          .Parent_WFID = 0
          .Project = IndentLine.t_cprj & "-" & IndentLine.ProjectName
          .SpecificationNo = IndentLine.t_nids
          .UserId = IIf(Indent.t_remn.Length < 4, Right("0000" & Indent.t_remn, 4), Indent.t_remn)
          .WF_Status = "Technical Specification Released"
          .PMDLDocNo = "Created From Indent/Line No.: " & IndentLine.t_rqno & "/" & IndentLine.t_pono
        End With
        tmp = SIS.WF.wfPreOrder.InsertData(newWF)
        '======Update CT======
        Insert168(newWF, Comp)
        '=====================
        CreateWFID &= IIf(CreateWFID = "", "", ", ")
        CreateWFID &= tmp.WFID
        '5. Create WF History
        Dim newWFH As New SIS.WF.wfPreOrderHistory
        With newWFH
          .WFID = tmp.WFID
          .WFID_SlNo = 1
          .Parent_WFID = 0
          .Element = tmp.Element
          .Project = tmp.Project
          .SpecificationNo = tmp.SpecificationNo
          .Buyer = tmp.Buyer
          .WF_Status = tmp.WF_Status
          .UserId = tmp.UserId
          .DateTime = tmp.DateTime
          .IndentLine = tmp.IndentLine
          .IndentNo = tmp.IndentNo
          .LotItem = tmp.LotItem
          .PMDLDocNo = tmp.PMDLDocNo
        End With
        newWFH = SIS.WF.wfPreOrderHistory.InsertData(newWFH)
        '=====Update CT========
        Insert169(newWFH, Comp)
        '======================
        '6. Create WF PMDL Docs
        'Dim tmpDocs As List(Of SIS.TDISG.tdisg003) = SIS.TDISG.tdisg003.GetDocument(IndentLine.t_rqno, IndentLine.t_pono)
        For Each doc As SIS.TDISG.tdisg003 In tmpDocs
          Dim newDoc As New SIS.WF.wfPreOrderPMDL
          With newDoc
            .WFID = tmp.WFID
            .PMDLDocNo = doc.t_docn
          End With
          Try
            SIS.WF.wfPreOrderPMDL.InsertData(newDoc)
            '=====Update CT========
            Insert167(newDoc, Comp)
            '======================
            '7. Copy Handle To WFID
            Dim aFile As SIS.EDI.ediAFile = SIS.EDI.ediAFile.ediAFileGetByHandleIndex("DOCUMENTMASTERPDF_" & Comp, doc.t_docn & "_" & doc.t_revi)
            If aFile IsNot Nothing Then
              aFile.t_hndl = "J_PREORDER_WORKFLOW"
              aFile.t_indx = tmp.WFID
            End If
            SIS.EDI.ediAFile.InsertData(aFile, Comp)
          Catch ex As Exception
          End Try
        Next
      End If
    Next
    If CreateWFID = "" Then
      ShowMsg("RFQ Workflow already created.")
    Else
      ShowMsg("RFQ Workflow: " & CreateWFID & " created.")
    End If
  End Sub
  Private Sub Insert168(ByVal pWF As SIS.WF.wfPreOrder, ByVal Comp As String)
    Dim Sql As String = ""
    Sql &= "   INSERT [tdmisg168" & Comp & "] "
    Sql &= "   ( "
    Sql &= "    [t_wfid] "
    Sql &= "   ,[t_pwfd] "
    Sql &= "   ,[t_cprj] "
    Sql &= "   ,[t_elem] "
    Sql &= "   ,[t_spec] "
    Sql &= "   ,[t_bpid] "
    Sql &= "   ,[t_stat] "
    Sql &= "   ,[t_user] "
    Sql &= "   ,[t_date] "
    Sql &= "   ,[t_supp] "
    Sql &= "   ,[t_snam] "
    Sql &= "   ,[t_rdno] "
    Sql &= "   ,[t_docn] "
    Sql &= "   ,[t_supc] "
    Sql &= "   ,[t_rcno] "
    Sql &= "   ,[t_mngr] "
    Sql &= "   ,[t_esub] "
    Sql &= "   ,[t_Refcntd] "
    Sql &= "   ,[t_Refcntu] "
    Sql &= "   ) "
    Sql &= "   VALUES "
    Sql &= "   ( "
    Sql &= "     " & pWF.WFID
    Sql &= "   , " & pWF.Parent_WFID
    Sql &= "   ,'" & pWF.Project.Substring(0, 6) & "'"
    Sql &= "   ,'" & pWF.Element.Substring(0, 8) & "'"
    Sql &= "   ,'" & pWF.SpecificationNo & "'"
    Sql &= "   ,'" & pWF.Buyer & "'"
    Sql &= "   ,'" & pWF.WF_Status & "'"
    Sql &= "   ,'" & pWF.UserId & "'"
    Sql &= "   ,'" & pWF.DateTime & "'"
    Sql &= "   ,'" & pWF.Supplier & "'"
    Sql &= "   ,'" & pWF.SupplierName & "'"
    Sql &= "   ,'" & pWF.RandomNo & "'"
    Sql &= "   ,'" & pWF.PMDLDocNo & "'"
    Sql &= "   ,'" & pWF.Manager & "'"
    Sql &= "   ,'" & pWF.ReceiptNo & "'"
    Sql &= "   ,'" & pWF.Manager & "'"
    Sql &= "   ,'" & pWF.EmailSubject & "'"
    Sql &= "   ,0"
    Sql &= "   ,0"
    Sql &= "   ) "
    Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
      Using Cmd As SqlCommand = Con.CreateCommand()
        Cmd.CommandType = CommandType.Text
        Cmd.CommandText = Sql
        Con.Open()
        Cmd.ExecuteNonQuery()
      End Using
    End Using
  End Sub
  Private Sub Insert169(ByVal pWFh As SIS.WF.wfPreOrderHistory, ByVal Comp As String)
    Dim Sql As String = ""
    Sql &= "   INSERT [tdmisg169" & Comp & "] "
    Sql &= "   ( "
    Sql &= "    [t_hwfd] "
    Sql &= "   ,[t_wfid] "
    Sql &= "   ,[t_slno] "
    Sql &= "   ,[t_pwfd] "
    Sql &= "   ,[t_cprj] "
    Sql &= "   ,[t_elem] "
    Sql &= "   ,[t_spec] "
    Sql &= "   ,[t_bpid] "
    Sql &= "   ,[t_stat] "
    Sql &= "   ,[t_user] "
    Sql &= "   ,[t_date] "
    Sql &= "   ,[t_supp] "
    Sql &= "   ,[t_snam] "
    Sql &= "   ,[t_note] "
    Sql &= "   ,[t_docn] "
    Sql &= "   ,[t_mngr] "
    Sql &= "   ,[t_Refcntd] "
    Sql &= "   ,[t_Refcntu] "
    Sql &= "   ) "
    Sql &= "   VALUES "
    Sql &= "   ( "
    Sql &= "     " & pWFh.WF_HistoryID
    Sql &= "   , " & pWFh.WFID
    Sql &= "   , " & pWFh.WFID_SlNo
    Sql &= "   , " & pWFh.Parent_WFID
    Sql &= "   ,'" & pWFh.Project.Substring(0, 6) & "'"
    Sql &= "   ,'" & pWFh.Element.Substring(0, 8) & "'"
    Sql &= "   ,'" & pWFh.SpecificationNo & "'"
    Sql &= "   ,'" & pWFh.Buyer & "'"
    Sql &= "   ,'" & pWFh.WF_Status & "'"
    Sql &= "   ,'" & pWFh.UserId & "'"
    Sql &= "   ,'" & pWFh.DateTime & "'"
    Sql &= "   ,'" & pWFh.Supplier & "'"
    Sql &= "   ,'" & pWFh.SupplierName & "'"
    Sql &= "   ,'" & pWFh.Notes & "'"
    Sql &= "   ,'" & pWFh.PMDLDocNo & "'"
    Sql &= "   ,'" & pWFh.Manager & "'"
    Sql &= "   ,0 "
    Sql &= "   ,0 "
    Sql &= "   ) "
    Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
      Using Cmd As SqlCommand = Con.CreateCommand()
        Cmd.CommandType = CommandType.Text
        Cmd.CommandText = Sql
        Con.Open()
        Cmd.ExecuteNonQuery()
      End Using
    End Using

  End Sub
  Private Sub Insert167(ByVal pWFpmdl As SIS.WF.wfPreOrderPMDL, ByVal Comp As String)
    Dim Sql As String = ""
    Sql &= "   INSERT [tdmisg167" & Comp & "] "
    Sql &= "   ( "
    Sql &= "    [t_wfid] "
    Sql &= "   ,[t_docn] "
    Sql &= "   ,[t_Refcntd] "
    Sql &= "   ,[t_Refcntu] "
    Sql &= "   ) "
    Sql &= "   VALUES "
    Sql &= "   ( "
    Sql &= "     " & pWFpmdl.WFID
    Sql &= "   ,UPPER('" & pWFpmdl.PMDLDocNo & "') "
    Sql &= "   ,0 "
    Sql &= "   ,0 "
    Sql &= "   ) "
    Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
      Using Cmd As SqlCommand = Con.CreateCommand()
        Cmd.CommandType = CommandType.Text
        Cmd.CommandText = Sql
        Con.Open()
        Cmd.ExecuteNonQuery()
      End Using
    End Using

  End Sub
  Private Sub ShowMsg(ByVal str As String)
    msg.InnerHtml = "<h2>" & str & "</h2>"
  End Sub
End Class
