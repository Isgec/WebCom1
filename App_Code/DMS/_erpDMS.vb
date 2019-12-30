Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Namespace SIS.DMS
  <DataObject()>
  Public Class erpDMSH
    Public Property t_type As Integer = 0
    Public Property t_cprj As String = ""
    Public Property t_rfld As String = ""
    Public Property t_rfid As Integer = 0
    Public Property t_pfld As String = ""
    Public Property t_pfid As Integer = 0
    Public Shared Function SelectList() As List(Of SIS.DMS.erpDMSH)
      Dim Results As New List(Of SIS.DMS.erpDMSH)
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "select * from tdmisg014200"
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.DMS.erpDMSH(Reader))
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function GetByID(t_type As Integer, t_cprj As String) As SIS.DMS.erpDMSH
      Dim Results As SIS.DMS.erpDMSH = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "select * from tdmisg014200 where t_type=" & t_type & " and t_cprj='" & t_cprj & "'"
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results = New SIS.DMS.erpDMSH(Reader)
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function Insert(obj As SIS.DMS.erpDMSH) As SIS.DMS.erpDMSH
      Dim Results As SIS.DMS.erpDMSH = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Con.Open()
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "insert tdmisg014200 (t_type,t_cprj,t_rfld,t_rfid,t_pfld,t_pfid,t_Refcntd,t_Refcntu) values(" & obj.t_type & ",'" & obj.t_cprj & "','" & obj.t_rfld & "'," & obj.t_rfid & ",'" & obj.t_pfld & "'," & obj.t_pfid & ",0,0" & ")"
          Cmd.ExecuteNonQuery()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function Update(obj As SIS.DMS.erpDMSH) As SIS.DMS.erpDMSH
      Dim Results As SIS.DMS.erpDMSH = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "update tdmisg014200 set t_rfld='" & obj.t_rfld & "', t_pfld='" & obj.t_pfld & "', t_rfid=" & obj.t_rfid & ",t_pfid=" & obj.t_pfid & " where t_type=" & obj.t_type & " and t_cprj='" & obj.t_cprj & "'"
          Con.Open()
          Cmd.ExecuteNonQuery()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function Delete(obj As SIS.DMS.erpDMSH) As SIS.DMS.erpDMSH
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Con.Open()
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "select isnull(count(t_type),0) as cnt from tdmisg015200 where t_type=" & obj.t_type & " and t_cprj='" & obj.t_cprj & "'"
          Dim mRet As Integer = Cmd.ExecuteScalar()
          If mRet > 0 Then
            Throw New Exception("Child folders exists, can not delete.")
          End If
        End Using
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "delete from tdmisg014200 where t_type=" & obj.t_type & " and t_cprj='" & obj.t_cprj & "'"
          Cmd.ExecuteNonQuery()
        End Using
      End Using
      Return obj
    End Function

    Public Sub New(ByVal Reader As SqlDataReader)
      Try
        For Each pi As System.Reflection.PropertyInfo In Me.GetType.GetProperties
          If pi.MemberType = Reflection.MemberTypes.Property Then
            Try
              Dim Found As Boolean = False
              For I As Integer = 0 To Reader.FieldCount - 1
                If Reader.GetName(I).ToLower = pi.Name.ToLower Then
                  Found = True
                  Exit For
                End If
              Next
              If Found Then
                If Convert.IsDBNull(Reader(pi.Name)) Then
                  Select Case Reader.GetDataTypeName(Reader.GetOrdinal(pi.Name))
                    Case "decimal"
                      CallByName(Me, pi.Name, CallType.Let, "0.00")
                    Case "bit"
                      CallByName(Me, pi.Name, CallType.Let, Boolean.FalseString)
                    Case Else
                      CallByName(Me, pi.Name, CallType.Let, String.Empty)
                  End Select
                Else
                  CallByName(Me, pi.Name, CallType.Let, Reader(pi.Name))
                End If
              End If
            Catch ex As Exception
            End Try
          End If
        Next
      Catch ex As Exception
      End Try
    End Sub
    Public Sub New()
    End Sub
  End Class
  <DataObject()>
  Public Class erpDMSD
    Public Property t_type As Integer = 0
    Public Property t_cprj As String = ""
    Public Property t_srno As Integer = 0
    Public Property t_tfld As String = ""
    Public Property t_tfid As Integer = 0
    Public Shared Function SelectList(t_type As Integer, t_cprj As String) As List(Of SIS.DMS.erpDMSD)
      Dim Results As New List(Of SIS.DMS.erpDMSD)
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "select * from tdmisg015200 where t_type=" & t_type & " and t_cprj='" & t_cprj & "' order by t_srno desc"
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results.Add(New SIS.DMS.erpDMSD(Reader))
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function GetByID(t_type As Integer, t_cprj As String, t_srno As Integer) As SIS.DMS.erpDMSD
      Dim Results As SIS.DMS.erpDMSD = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "select * from tdmisg015200 where t_type=" & t_type & " and t_cprj='" & t_cprj & "' and t_srno=" & t_srno
          Con.Open()
          Dim Reader As SqlDataReader = Cmd.ExecuteReader()
          While (Reader.Read())
            Results = New SIS.DMS.erpDMSD(Reader)
          End While
          Reader.Close()
        End Using
      End Using
      Return Results
    End Function
    Public Shared Function Insert(obj As SIS.DMS.erpDMSD) As SIS.DMS.erpDMSD
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = " insert tdmisg015200 (t_type,t_cprj,t_srno,t_tfld,t_tfid,t_Refcntd,t_Refcntu) values(" & obj.t_type & ",'" & obj.t_cprj & "'," & "(select (isnull(max(t_srno),0)) + 1 as sr from tdmisg015200 where t_type=" & obj.t_type & " and t_cprj='" & obj.t_cprj & "' )" & ",'" & obj.t_tfld & "'," & obj.t_tfid & ",0,0" & ")"
          Con.Open()
          Cmd.ExecuteNonQuery()
        End Using
      End Using
      Return obj
    End Function
    Public Shared Function Update(obj As SIS.DMS.erpDMSD) As SIS.DMS.erpDMSD
      Dim Results As SIS.DMS.erpDMSD = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "update tdmisg015200 set t_tfld='" & obj.t_tfld & "', t_tfid=" & obj.t_tfid & " where t_type=" & obj.t_type & " and t_cprj='" & obj.t_cprj & "' and t_srno=" & obj.t_srno
          Con.Open()
          Cmd.ExecuteNonQuery()
        End Using
      End Using
      Return obj
    End Function
    Public Shared Function Delete(obj As SIS.DMS.erpDMSD) As SIS.DMS.erpDMSD
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "delete from tdmisg015200 where t_type=" & obj.t_type & " and t_cprj='" & obj.t_cprj & "' and t_srno=" & obj.t_srno
          Con.Open()
          Cmd.ExecuteNonQuery()
        End Using
      End Using
      Return obj
    End Function

    Public Sub New(ByVal Reader As SqlDataReader)
      Try
        For Each pi As System.Reflection.PropertyInfo In Me.GetType.GetProperties
          If pi.MemberType = Reflection.MemberTypes.Property Then
            Try
              Dim Found As Boolean = False
              For I As Integer = 0 To Reader.FieldCount - 1
                If Reader.GetName(I).ToLower = pi.Name.ToLower Then
                  Found = True
                  Exit For
                End If
              Next
              If Found Then
                If Convert.IsDBNull(Reader(pi.Name)) Then
                  Select Case Reader.GetDataTypeName(Reader.GetOrdinal(pi.Name))
                    Case "decimal"
                      CallByName(Me, pi.Name, CallType.Let, "0.00")
                    Case "bit"
                      CallByName(Me, pi.Name, CallType.Let, Boolean.FalseString)
                    Case Else
                      CallByName(Me, pi.Name, CallType.Let, String.Empty)
                  End Select
                Else
                  CallByName(Me, pi.Name, CallType.Let, Reader(pi.Name))
                End If
              End If
            Catch ex As Exception
            End Try
          End If
        Next
      Catch ex As Exception
      End Try
    End Sub
    Public Sub New()
    End Sub

  End Class
End Namespace

