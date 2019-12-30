Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.Script.Serialization
Imports Ionic
Imports Ionic.Zip
Partial Class mTDSAttachments1
  Inherits SIS.SYS.GridBase
  Private st As Long = HttpContext.Current.Server.ScriptTimeout
  Private Sub ODSdmisg121_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles ODSdmisg121.Selecting
    '1. Check To Search
    If e.InputParameters("SearchText") <> "" Then
      e.InputParameters("SearchState") = True
    End If
  End Sub
  Private Sub GVdmisg121_Init(sender As Object, e As EventArgs) Handles GVdmisg121.Init
    DataClassName = "tdmisg121200"
    SetGridView = GVdmisg121
  End Sub
  Private Sub TBLdmisg121200_Init(sender As Object, e As EventArgs) Handles TBLdmisg121200.Init
    SetToolBar = TBLdmisg121200
  End Sub

  Private Sub GVdmisg121_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GVdmisg121.RowCommand
    If e.CommandName.ToLower = "cmdDownload".ToLower Then
      Dim FinYear As String = GVdmisg121.DataKeys(e.CommandArgument).Values("FinYear")
      Dim CardNo As String = GVdmisg121.DataKeys(e.CommandArgument).Values("CardNo")
      Try
        DownloadAll(CardNo, FinYear)
      Catch ex As Exception
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" & New JavaScriptSerializer().Serialize(ex.Message) & "');", True)
      End Try
    End If
    If e.CommandName.ToLower = "initiatewf".ToLower Then
      Try
        Dim FinYear As String = GVdmisg121.DataKeys(e.CommandArgument).Values("FinYear")
        Dim CardNo As String = GVdmisg121.DataKeys(e.CommandArgument).Values("CardNo")
        SIS.TDS.tdsAttachmentUser.InitiateWF(FinYear, CardNo)
        GVdmisg121.DataBind()
      Catch ex As Exception
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", "alert('" & New JavaScriptSerializer().Serialize(ex.Message) & "');", True)
      End Try
    End If
  End Sub

  Private Sub GVdmisg121_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GVdmisg121.Sorting
    e.Cancel = True
  End Sub
  Protected Sub PageSizeChanged(ByVal s As Object, ByVal e As EventArgs)
    TBLdmisg121200.RecordsPerPage = CType(s, DropDownList).SelectedValue
    GVdmisg121.PageSize = CType(s, DropDownList).SelectedValue
    GVdmisg121.DataBind()
  End Sub

  Private Sub DownloadAll(ByVal CardNo As String, ByVal FinYear As String)
    HttpContext.Current.Server.ScriptTimeout = Integer.MaxValue
    Dim docHndl As String = "J_INVESTMENT_DECLARATION"
    Dim LibFolder As String = "attachmentlibrary1"
    Dim libPath As String = ""
    Dim filePath As String = ""
    Dim fileName As String = CardNo & "_" & FinYear & ".zip"
    Dim NeedsMapping As Boolean = False
    Dim Mapped As Boolean = False

    Dim UrlAuthority As String = HttpContext.Current.Request.Url.Authority
    If UrlAuthority.ToLower <> "192.9.200.146" Then
      UrlAuthority = "192.9.200.146"
      NeedsMapping = True
    End If
    libPath = "D:\" & LibFolder
    If NeedsMapping Then
      libPath = "\\" & UrlAuthority & "\" & LibFolder
      If ConnectToNetworkFunctions.connectToNetwork(libPath, "X:", "ISGECNET\adskvault", "adskvault@123") Then
        Mapped = True
      End If
    End If
    Dim tmpFilesToDelete As New ArrayList
    Response.Clear()
    Response.AppendHeader("content-disposition", "attachment; filename=" & fileName)
    Response.ContentType = SIS.SYS.Utilities.ApplicationSpacific.ContentType(fileName)
    Dim UserAtchs As List(Of SIS.TDS.tdsAttachment) = SIS.TDS.tdsAttachment.GetByUserFinYear(CardNo, FinYear)
    Using zip As New ZipFile
      zip.CompressionLevel = Zlib.CompressionLevel.Level5
      For Each tDoc As SIS.TDS.tdsAttachment In UserAtchs
        If tDoc IsNot Nothing Then
          filePath = libPath & "\" & tDoc.t_dcid
          fileName = tDoc.t_fnam
          '====================
          '===Just to remap====
          If Not IO.File.Exists(filePath) Then
            libPath = "D:\" & LibFolder
            If NeedsMapping Then
              libPath = "\\" & UrlAuthority & "\" & LibFolder
              If ConnectToNetworkFunctions.connectToNetwork(libPath, "X:", "ISGECNET\adskvault", "adskvault@123") Then
                Mapped = True
              End If
            End If
          End If
          '====================
          If IO.File.Exists(filePath) Then
            Dim tmpFile As String = Server.MapPath("~/..") & "App_Temp/" & fileName
            If IO.File.Exists(tmpFile) Then
              Try
                Dim fInfo As New FileInfo(tmpFile)
                fInfo.IsReadOnly = False
                IO.File.Delete(tmpFile)
              Catch ex As Exception
              End Try
            End If
            Try
              IO.File.Copy(filePath, tmpFile)
            Catch ex As Exception
            End Try
            zip.AddFile(tmpFile, "Files")
            tmpFilesToDelete.Add(tmpFile)
          End If
        End If
      Next
      zip.Save(Response.OutputStream)
    End Using
    For Each str As String In tmpFilesToDelete
      Try
        Dim fInfo As New FileInfo(str)
        fInfo.IsReadOnly = False
        IO.File.Delete(str)
      Catch ex As Exception
      End Try
    Next
    If Mapped Then
      ConnectToNetworkFunctions.disconnectFromNetwork("X:")
    End If
    Response.End()

  End Sub

  Private Sub mGF_dmisg121_Load(sender As Object, e As EventArgs) Handles Me.Load
    If Not Page.IsPostBack And Not Page.IsCallback Then
      GVdmisg121.PageSize = 25
      'CType(GVdmisg121.HeaderRow.FindControl("D_PageSize"), DropDownList).SelectedIndex = 1
    End If
    F_FinYear.Text = ConfigurationManager.AppSettings("TDSFinYear").ToString

  End Sub

  Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
    SIS.TDS.tdsAttachment.SelectRefresh()
  End Sub
End Class
