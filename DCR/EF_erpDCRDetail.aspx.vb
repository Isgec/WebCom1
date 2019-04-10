Imports System.Web.Script.Serialization
Partial Class EF_erpDCRDetail
  Inherits SIS.SYS.UpdateBase

  Private Sub EF_ediWTmtlH_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
    SIS.SYS.Utilities.SessionManager.InitializeEnvironment("0340")
  End Sub

  Private Sub EF_erpDCRDetail_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
    Dim DownloadKey As String = ""
    If Request.QueryString("DownloadKey") IsNot Nothing Then
      DownloadKey = Request.QueryString("downloadKey")
    End If
    If DownloadKey <> "" Then
      Dim doc As SIS.ERP.erpDCRDetail = SIS.ERP.erpDCRDetail.GetByDocumentKey(DownloadKey)
      If doc IsNot Nothing Then
        If doc.DownloadExpiresOn <> "" Then
          dt.Text = Convert.ToDateTime(doc.DownloadExpiresOn).ToString("dd/MM/yyyy")
          If Convert.ToDateTime(doc.DownloadExpiresOn) > Now Then
            With cmdDownload
              .Text = doc.DocumentID & ".PDF Rev.:" & doc.NextRevision
              .Attributes.Add("onclick", doc.GetSDownloadLink)
            End With
          End If
        Else
          With cmdDownload
            .Text = doc.DocumentID & ".PDF Rev.:" & doc.NextRevision
            .Attributes.Add("onclick", doc.GetSDownloadLink)
          End With
        End If
      End If
    End If
  End Sub
End Class
