Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.RFQ
  Public Class rfqGeneral
    Public Shared Function GetERPCompanyByIndentNo(ByVal IndentNo As String) As String
      Dim mRet As String = 200
      Select Case IndentNo.Substring(0, 4).ToUpper
        Case "R101", "R201", "R250", "R301", "R401", "R501"
          mRet = "200"
      End Select
      Return mRet
    End Function
  End Class
End Namespace

