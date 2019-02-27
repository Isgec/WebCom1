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
    Dim LineNo As Integer = 0
    Try
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
        LineNo = IndentLine.t_pono
        Dim tmp As SIS.WF.wfPreOrder = SIS.WF.wfPreOrder.GetByIndentLine(IndentLine.t_rqno, IndentLine.t_pono)
        '==============Check, If there is child Items then only Create=================
        Dim tmpDocs As List(Of SIS.TDISG.tdisg003) = SIS.TDISG.tdisg003.GetDocument(IndentLine.t_rqno, IndentLine.t_pono)
        If tmpDocs.Count <= 0 Then
          Continue For
        End If
        '===============End Of Check===================================================
        If tmp Is Nothing Then
          '=======================LOG============================
          LogIt(IndentNo, IndentLine.t_pono, logStates.Create)
          '======================================================
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
            .PMDLDocNo = "Indent/Line No.: " & IndentLine.t_rqno & "/" & IndentLine.t_pono
          End With
          tmp = SIS.WF.wfPreOrder.InsertData(newWF)
          '=======================LOG============================
          'LogIt(IndentNo, IndentLine.t_pono, logStates.PreOrder)
          '======================================================
          '=======================LOG============================
          LogIt(IndentNo, IndentLine.t_pono, logStates.WFID, tmp.WFID)
          '======================================================
          '======Update CT======
          Insert168(newWF, Comp)
          '=====================
          '=======================LOG============================
          LogIt(IndentNo, IndentLine.t_pono, logStates.CT168)
          '======================================================
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
          '=======================LOG============================
          LogIt(IndentNo, IndentLine.t_pono, logStates.History)
          '======================================================
          '=====Update CT========
          Insert169(newWFH, Comp)
          '======================
          '=======================LOG============================
          LogIt(IndentNo, IndentLine.t_pono, logStates.CT169)
          '======================================================
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
              '=======================LOG============================
              LogIt(IndentNo, IndentLine.t_pono, logStates.PMDL, doc.t_docn)
              '======================================================
              '=====Update CT========
              Insert167(newDoc, Comp)
              '======================
              '=======================LOG============================
              LogIt(IndentNo, IndentLine.t_pono, logStates.CT167, "CT")
              '======================================================
              '7. Copy Handle To WFID
              Dim aFile As SIS.EDI.ediAFile = SIS.EDI.ediAFile.ediAFileGetByHandleIndex("DOCUMENTMASTERPDF_" & Comp, doc.t_docn & "_" & doc.t_revi)
              If aFile IsNot Nothing Then
                aFile.t_hndl = "J_PREORDER_WORKFLOW"
                aFile.t_indx = tmp.WFID
              End If
              SIS.EDI.ediAFile.InsertData(aFile, Comp)
              '=======================LOG============================
              LogIt(IndentNo, IndentLine.t_pono, logStates.HNDL)
              '======================================================
            Catch ex As Exception
              '=======================LOG============================
              LogIt(IndentNo, IndentLine.t_pono, logStates.Err, "At Doc: " & doc.t_docn & ":" & ex.Message)
              '======================================================
            End Try
          Next
        End If
      Next
      If CreateWFID = "" Then
        ShowMsg("RFQ Workflow already created.")
      Else
        ShowMsg("RFQ Workflow: " & CreateWFID & " created.")
      End If
    Catch ex As Exception
      '=======================LOG============================
      LogIt(IndentNo, LineNo, logStates.Err, "Any Where: " & ex.Message)
      '======================================================
      ShowMsg("Err: " & ex.Message)
    End Try
  End Sub
  Private Function Insert168(ByVal pWF As SIS.WF.wfPreOrder, ByVal Comp As String) As String
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
    Sql &= "   ,convert(datetime,'" & pWF.DateTime & "',103)"
    Sql &= "   ,'" & pWF.Supplier & "'"
    Sql &= "   ,'" & pWF.SupplierName & "'"
    Sql &= "   ,'" & pWF.RandomNo & "'"
    Sql &= "   ,'" & pWF.PMDLDocNo & "'"
    Sql &= "   ,'" & pWF.LotItem & "'"
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
        Try
          Cmd.ExecuteNonQuery()
        Catch ex As Exception
          Throw New Exception(sql)
        End Try
      End Using
    End Using
    Return ""
  End Function
  Private Function Insert169(ByVal pWFh As SIS.WF.wfPreOrderHistory, ByVal Comp As String) As String
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
    Sql &= "   ,convert(datetime,'" & pWFh.DateTime & "',103)"
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
        Try
          Cmd.ExecuteNonQuery()
        Catch ex As Exception
          Throw New Exception(Sql)
        End Try
      End Using
    End Using
    Return ""
  End Function
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

  Private Enum logStates
    Create = 1
    WFID = 2
    PreOrder = 3
    CT168 = 4
    History = 5
    CT169 = 6
    PMDL = 7
    CT167 = 8
    HNDL = 9
    Err = 0
  End Enum
  Private Sub LogIt(ByVal IndentNo As String, ByVal IndentLine As Integer, ByVal LogFor As logStates, Optional ByVal str As String = "")
    Dim Sql As String = ""

    Select Case LogFor
      Case logStates.Create
        Sql = "Insert WF1_Log (IndentNo,IndentLine,CreatedOn) Values ('" & IndentNo & "'," & IndentLine & ",GetDate())"
      Case logStates.WFID
        Sql = "Update WF1_Log set WFID=" & str & ",WFCreated=1 where IndentNo='" & IndentNo & "' and IndentLine=" & IndentLine
      Case logStates.PreOrder
        Sql = "Update WF1_Log set WFCreated=1 where IndentNo='" & IndentNo & "' and IndentLine=" & IndentLine
      Case logStates.CT168
        Sql = "Update WF1_Log set CT168Created=1 where IndentNo='" & IndentNo & "' and IndentLine=" & IndentLine
      Case logStates.History
        Sql = "Update WF1_Log set HistoryCreated=1 where IndentNo='" & IndentNo & "' and IndentLine=" & IndentLine
      Case logStates.CT169
        Sql = "Update WF1_Log set CT169Created=1 where IndentNo='" & IndentNo & "' and IndentLine=" & IndentLine
      Case logStates.PMDL
        Sql = "Update WF1_Log set PMDLCreated=1,ErrorMsg=ErrorMsg+'|" & str & "' where IndentNo='" & IndentNo & "' and IndentLine=" & IndentLine
      Case logStates.CT167
        Sql = "Update WF1_Log set CT167Created=1,ErrorMsg=ErrorMsg+'|" & str & "' where IndentNo='" & IndentNo & "' and IndentLine=" & IndentLine
      Case logStates.HNDL
        Sql = "Update WF1_Log set HNDLCreated=1 where IndentNo='" & IndentNo & "' and IndentLine=" & IndentLine
      Case logStates.Err
        Sql = "Update WF1_Log set ErrorMsg=ErrorMsg+'|" & str & "' where IndentNo='" & IndentNo & "' and IndentLine=" & IndentLine
    End Select
    Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
      Using Cmd As SqlCommand = Con.CreateCommand()
        Cmd.CommandType = CommandType.Text
        Cmd.CommandText = Sql
        Con.Open()
        Try
          Cmd.ExecuteNonQuery()
        Catch ex As Exception
          Throw New Exception(Sql)
        End Try
      End Using
    End Using
  End Sub
  'Private Sub cmdImport_Click(sender As Object, e As EventArgs) Handles cmdImport.Click
  '  Dim wfs As New List(Of SIS.WF.wfPreOrder)
  '  Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
  '    Using Cmd As SqlCommand = Con.CreateCommand()
  '      Cmd.CommandType = CommandType.Text
  '      Cmd.CommandText = "SELECT * FROM WF1_PreOrder WHERE (LEFT(Project, 6) IN ('JB1123', 'JB1124', 'JB1126', 'JB1130')) and wfid not in (2971,	2970,	2969,	2968,	2967,	2966,	2965,	2964,	2963,	2962,	2961,	2960,	2959,	2958,	2957,	2956,	2955,	2954,	2953,	2952,	2951,	2950,	2949,	2948,	2935,	2934,	2933,	2932,	2931,	2930,	2929,	2892,	2891,	2890,	2889,	2888,	2887,	2886,	2885,	2884,	2784,	2783,	2782,	2781,	2743,	2718,	2717,	2716,	2715,	2714,	2713,	2712,	2711,	2710,	2697,	2696,	2695,	2694,	2690,	2663,	2656,	2655,	2654,	2652,	2651,	2650,	2649,	2645,	2640,	2637,	2634,	2631,	2627,	2602,	2601,	2600,	2599,	2598,	2597,	2596,	2595,	2594,	2593,	2592,	2591,	2204,	2203,	2202,	2201,	2200,	2199,	2198,	2196,	2188,	2187,	2186,	2185,	2184,	2183,	2182,	2181,	2180,	2179)"
  '      Con.Open()
  '      Dim Reader As SqlDataReader = Cmd.ExecuteReader()
  '      While Reader.Read()
  '        wfs.Add(New SIS.WF.wfPreOrder(Reader))
  '      End While
  '      Reader.Close()
  '    End Using
  '  End Using
  '  For Each wf As SIS.WF.wfPreOrder In wfs
  '    Try
  '      Insert168(wf, "200")
  '      Dim wfhs As List(Of SIS.WF.wfPreOrderHistory) = SIS.WF.wfPreOrderHistory.GetByWFID(wf.WFID)
  '      For Each wfh As SIS.WF.wfPreOrderHistory In wfhs
  '        Try
  '          Insert169(wfh, "200")
  '        Catch ex As Exception
  '          Dim x As String = ex.Message
  '        End Try
  '      Next
  '    Catch ex As Exception
  '      Dim x As String = ex.Message
  '    End Try
  '  Next
  'End Sub
End Class
