Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.Script.Serialization
Imports Ionic
Imports Ionic.Zip
Imports ejiVault
Partial Class docdownload
  Inherits System.Web.UI.Page
  Private st As Long = HttpContext.Current.Server.ScriptTimeout
  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    HttpContext.Current.Server.ScriptTimeout = Integer.MaxValue
    Dim Value As String = ""
    If Request.QueryString("DownloadKey") IsNot Nothing Then
      Value = Request.QueryString("DownloadKey")
      DownloadSDoc(Value)
    End If
  End Sub
  Private Sub DownloadSDoc(ByVal dKey As String)
    Response.Clear()
    Dim docHndl As String = "DOCUMENTMASTERPDF_200"
    Dim oDoc As SIS.ERP.erpDCRDetail = SIS.ERP.erpDCRDetail.GetByDocumentKey(dKey)

    Dim docIndx As String = oDoc.DocumentID & "_" & oDoc.NextRevision
    Dim filePath As String = ""
    Dim fileName As String = ""
    Dim rDoc As EJI.ediAFile = EJI.ediAFile.GetFileByHandleIndex(docHndl, docIndx)
    If rDoc IsNot Nothing Then
      Dim rLib As EJI.ediALib = EJI.ediALib.GetLibraryByID(rDoc.t_lbcd)
      If rLib IsNot Nothing Then
        If Not EJI.DBCommon.IsLocalISGECVault Then
          EJI.ediALib.ConnectISGECVault(rLib)
        End If
        filePath = rLib.LibraryPath & "\" & rDoc.t_dcid
        fileName = rDoc.t_fnam
        Response.AppendHeader("content-disposition", "attachment; filename=" & fileName)
        Response.ContentType = SIS.SYS.Utilities.ApplicationSpacific.ContentType(fileName)
        If IO.File.Exists(filePath) Then
          Response.WriteFile(filePath)
        End If
        If Not EJI.DBCommon.IsLocalISGECVault Then
          EJI.ediALib.DisconnectISGECVault()
        End If
      End If
    End If
    Response.End()
  End Sub
End Class
