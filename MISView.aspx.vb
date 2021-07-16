Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel

Partial Class MISView
  Inherits System.Web.UI.Page
  Private Sub RenderDS(ds As DataSet, BaseName As String, Optional InExcel As Boolean = False, Optional Index As Integer = 0)
    Dim cnt As Integer = 0
    For Each dt As DataTable In ds.Tables
      Dim hDiv As New HtmlGenericControl("div")
      Dim gv As New GridView
      cnt += 1
      gv.ID = BaseName & "Rep_" & cnt
      gv.CssClass = "mis-gv"
      With gv
        .HeaderStyle.BackColor = Drawing.ColorTranslator.FromHtml("#3AC0F2")
        .HeaderStyle.ForeColor = Drawing.Color.White
        .RowStyle.BackColor = Drawing.ColorTranslator.FromHtml("#dcf5f5")  '#A1DCF2
        .AlternatingRowStyle.BackColor = Drawing.Color.White
        .AlternatingRowStyle.ForeColor = Drawing.ColorTranslator.FromHtml("#000")
        .ShowFooter = True
        '.AllowPaging = True
        '.PageSize = 20
        '.PageIndex = 1
        'With .PagerSettings
        '  .Visible = True
        '  .Mode = PagerButtons.Numeric
        'End With
      End With
      If dt.TableName = "Table" And BaseName <> "BaaN" Then gv.CssClass = "mis-gv mis-count"
      gv.DataSource = dt
      gv.DataBind()
      If Not InExcel Then
        Dim bt As New Button
        With bt
          .Text = "Export to Excel"
          .CommandArgument = BaseName & "_" & cnt
          .CssClass = "nt-but-danger"
          AddHandler .Click, AddressOf abc
        End With
        gv.FooterRow.Cells(0).Controls.Add(bt)
        hDiv.Controls.Add(gv)
        mainContainer.Controls.Add(hDiv)
      Else
        If cnt = Index Then
          ExportToExcel(gv)
        End If
      End If
    Next
    If InExcel Then
    End If

  End Sub
  Private Sub LoadErpdata(Optional InExcel As Boolean = False, Optional Index As Integer = 0)
    Dim Comp As String = "200"
    Dim ds As New DataSet
    Dim Sql As String = ""
    Sql &= " declare @emp NVarChar(8)='1361' "
    Sql &= " declare @nemp int = convert(int,@emp) "
    '--Role in Design
    Sql &= " select t_rdes as [Role in Design] from ttiisg905200 where t_rlid=(select top 1 t_rlid from ttiisg906200 where t_emno=@nemp and t_stat=1) "
    '--Currently Working With Design Group
    Sql &= " select t_desc as [Working with Design Group] from ttiisg911200 where t_grcd = (select top 1 t_grcd from ttiisg917200 where t_emno=@nemp and GetDate() between t_gndt and t_rldt) "
    '--Have worked with [Last Five for frequent movers] 
    Sql &= " select top 5 gr.t_desc as [Have Worked with], "
    Sql &= " em.t_gndt as [From], "
    Sql &= " DateDiff(d,em.t_gndt,em.t_rldt) as [For Days] "
    Sql &= " from ttiisg917200 as em "
    Sql &= " inner join ttiisg911200 as gr on em.t_grcd = gr.t_grcd "
    Sql &= "  where em.t_emno=@nemp and GetDate() not between em.t_gndt and em.t_rldt "
    Sql &= "  order by em.t_gndt desc "

    '--Working On Projects since last 15 Days
    Sql &= " select distinct "
    Sql &= "  hr.t_cprj as ProjectID, "
    Sql &= "  hr.t_cspa as ElementID, "
    Sql &= "  ac.t_desc as [Activity Performed in Last 15 Days] "
    Sql &= "  from ttiisg910200 as hr "
    Sql &= "  left outer join ttiisg908200 as ac on hr.t_acid=ac.t_acid "
    Sql &= "  where hr.t_emno=@nemp "
    Sql &= "  and hr.t_tdat >= dateadd(d,-15,getdate()) "


    '--select * from ttiisg910200 where year(t_tdat)=2021
    '--select * from ttiisg917200 where t_emno='1361'
    Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetBaaNConnectionString())
      Using Cmd As SqlCommand = Con.CreateCommand()
        Cmd.CommandType = CommandType.Text
        Cmd.CommandText = Sql
        Con.Open()
        Dim da As New SqlDataAdapter
        da.SelectCommand = Cmd
        da.Fill(ds)
      End Using
    End Using
    RenderDS(ds, "BaaN", InExcel, Index)
  End Sub

  Private Sub Loaddata(Optional InExcel As Boolean = False, Optional Index As Integer = 0)
    Dim ds As New DataSet
    Dim Sql As String = ""
    Sql &= " declare @emp NVarChar(8)='3019' "

    '  - -Employee Details
    Sql &= " select * from HRM_EmployeeDetails where cardno=@emp "
    '--Reporting To As per HR
    Sql &= " select * from HRM_EmployeeDetails where cardno=(select (case when x.verifierid is not null then x.verifierid else x.Approverid end) as boss from HRM_Employees as x where x.cardno=@emp) "
    '--Reporting To Him
    Sql &= " select * from HRM_EmployeeDetails where cardno IN (select x.CardNo from HRM_Employees as x where @emp = (case when x.verifierid is not null then x.verifierid else x.Approverid end)) "
    '--Reporting To Him-All 
    Sql &= " select * from HRM_EmployeeDetails where cardno IN (select x.CardNo from HRM_Employees as x where x.verifierid=@emp or x.Approverid=@emp) "


    Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
      Using Cmd As SqlCommand = Con.CreateCommand()
        Cmd.CommandType = CommandType.Text
        Cmd.CommandText = Sql
        Con.Open()
        Dim da As New SqlDataAdapter
        da.SelectCommand = Cmd
        da.Fill(ds)
      End Using
    End Using
    RenderDS(ds, "Joomla", InExcel, Index)
  End Sub
  Protected Sub abc(s As Object, e As EventArgs)
    Dim ca() As String = CType(s, Button).CommandArgument.Split("_".ToCharArray)
    Select Case ca(0)
      Case "Joomla"
        Loaddata(True, ca(1))
      Case "BaaN"
        LoadErpData(True, ca(1))
    End Select
  End Sub
  Private Sub MISView_Load(sender As Object, e As EventArgs) Handles Me.Load
    Loaddata()
    LoadErpdata()
  End Sub

  Protected Sub ExportToExcel(GridView1 As GridView)
    Response.Clear()
    Response.Buffer = True
    Response.AddHeader("content-disposition", "attachment;filename=" & GridView1.ID & ".xls")
    Response.Charset = ""
    Response.ContentType = "application/vnd.ms-excel"
    Using sw As New IO.StringWriter()
      Dim hw As New HtmlTextWriter(sw)
      GridView1.HeaderRow.BackColor = Drawing.Color.White
      For Each cell As TableCell In GridView1.HeaderRow.Cells
        cell.BackColor = GridView1.HeaderStyle.BackColor
      Next
      For Each row As GridViewRow In GridView1.Rows
        row.BackColor = Drawing.Color.White
        For Each cell As TableCell In row.Cells
          If row.RowIndex Mod 2 = 0 Then
            cell.BackColor = GridView1.AlternatingRowStyle.BackColor
          Else
            cell.BackColor = GridView1.RowStyle.BackColor
          End If
          cell.CssClass = "textmode"
        Next
      Next
      GridView1.RenderControl(hw)
      'style to format numbers to string
      Dim style As String = "<style> .textmode { } </style>"
      Response.Write(style)
      Response.Output.Write(sw.ToString())
      Response.Flush()
      Response.[End]()
    End Using
  End Sub

  Public Overrides Sub VerifyRenderingInServerForm(control As Control)
    ' Verifies that the control is rendered
  End Sub
End Class
