﻿Imports System.Web.Services
Imports System.IO
Imports System.Xml
Imports System.Security
Partial Class bs
  Inherits System.Web.UI.MasterPage
  Protected Sub ToolkitScriptManager1_AsyncPostBackError(ByVal sender As Object, ByVal e As System.Web.UI.AsyncPostBackErrorEventArgs) Handles ToolkitScriptManager1.AsyncPostBackError
    If (e.Exception.Data("ExtraInfo") <> Nothing) Then
      ToolkitScriptManager1.AsyncPostBackErrorMessage = e.Exception.Data("ExtraInfo").ToString()
    Else
      ToolkitScriptManager1.AsyncPostBackErrorMessage = e.Exception.Message
    End If
  End Sub
End Class

