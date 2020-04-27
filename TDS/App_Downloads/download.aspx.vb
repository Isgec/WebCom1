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
    If Request.QueryString("doc") IsNot Nothing Then
      Value = Request.QueryString("doc")
      DownloadDoc(Value)
    End If
  End Sub
  Private Sub DownloadDoc(ByVal pk As String)
    Dim docHndl As String = "J_INVESTMENT_DECLARATION"
    Dim docIndx As String = pk.Replace("|", "_")
    Dim libPath As String = ""
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
        If IO.File.Exists(filePath) Then
          Response.Clear()
          Response.AppendHeader("content-disposition", "attachment; filename=" & fileName)
          Response.ContentType = SIS.SYS.Utilities.ApplicationSpacific.ContentType(fileName)
          Response.WriteFile(filePath)
          Response.End()
        End If
        If Not EJI.DBCommon.IsLocalISGECVault Then
          EJI.ediALib.DisconnectISGECVault()
        End If
      End If
    End If
  End Sub

End Class
