
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
    '2. Get Indent Lines From ERP
    Dim IndentLines As List(Of SIS.TDPUR.tdpur201) = SIS.TDPUR.tdpur201.GetByRQNo(IndentNo, Comp)
    '3. Check RFQ for All Indent Lines
    Dim CreateWFID As String = ""
    For Each IndentLine As SIS.TDPUR.tdpur201 In IndentLines
      Dim tmp As SIS.WF.wfPreOrder = SIS.WF.wfPreOrder.GetByIndentLine(IndentLine.t_rqno, IndentLine.t_pono)
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
        '6. Create WF PMDL Docs
        Dim tmpDocs As List(Of SIS.TDISG.tdisg003) = SIS.TDISG.tdisg003.GetDocument(IndentLine.t_rqno, IndentLine.t_pono)
        For Each doc As SIS.TDISG.tdisg003 In tmpDocs
          Dim newDoc As New SIS.WF.wfPreOrderPMDL
          With newDoc
            .WFID = tmp.WFID
            .PMDLDocNo = doc.t_docn
          End With
          Try
            SIS.WF.wfPreOrderPMDL.InsertData(newDoc)
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
  Private Sub ShowMsg(ByVal str As String)
    msg.InnerHtml = "<h2>" & str & "</h2>"
  End Sub
End Class
