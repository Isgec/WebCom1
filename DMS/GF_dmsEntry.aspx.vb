
Imports System.Web.Script.Serialization
Partial Class DMS_GF_dmsEntry
  Inherits System.Web.UI.Page

  Private Sub msg(str As String)
    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", String.Format("alert({0});", New JavaScriptSerializer().Serialize(str)), True)
  End Sub
  Private Sub cmdInsertFolder_ServerClick(sender As Object, e As EventArgs) Handles cmdInsertFolder.ServerClick
    If F_t_type.Text <> "" AndAlso F_t_cprj.Text <> "" AndAlso X_t_tfld.Value <> "" Then
      Dim tmp As New SIS.DMS.erpDMSD
      With tmp
        .t_type = F_t_type.Text
        .t_cprj = F_t_cprj.Text
        .t_tfld = X_t_tfld.Value
      End With
      SIS.DMS.erpDMSD.Insert(tmp)
      gvDmsD.DataBind()
    Else
      msg("Select Project and Enter Transmittal Folder first to add record.")
    End If
  End Sub

  Private Sub cmdInsertProject_ServerClick(sender As Object, e As EventArgs) Handles cmdInsertProject.ServerClick
    If X_t_cprj.Value <> "" AndAlso X_t_type.Value <> "" Then
      Dim tmp As New SIS.DMS.erpDMSH
      With tmp
        .t_type = X_t_type.Value
        .t_cprj = X_t_cprj.Value
      End With
      Try
        SIS.DMS.erpDMSH.Insert(tmp)
        gvDmsH.DataBind()
      Catch ex As Exception
        msg(ex.Message)
      End Try
    Else
      msg("Project is Blanck.")
    End If
  End Sub
  Private Sub gvDmsH_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvDmsH.SelectedIndexChanged
    Dim tType As String = gvDmsH.SelectedDataKey("t_type")
    Dim tCprj As String = gvDmsH.SelectedDataKey("t_cprj")
    F_t_type.Text = tType
    F_t_cprj.Text = tCprj
    gvDmsD.DataBind()
  End Sub

  Private Sub gvDmsH_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvDmsH.RowCommand
    If e.CommandName.ToUpper = "UpdateInDMS".ToUpper Then
      Dim tType As String = gvDmsH.DataKeys(e.CommandArgument).Values("t_type")
      Dim tCprj As String = gvDmsH.DataKeys(e.CommandArgument).Values("t_cprj")
      Dim prj As SIS.DMS.erpDMSH = SIS.DMS.erpDMSH.GetByID(tType, tCprj)
      Dim prjfs As List(Of SIS.DMS.erpDMSD) = SIS.DMS.erpDMSD.SelectList(tType, tCprj)
      '***if id is 0, else find by ID and update description in all cases***
      'If root folder found then assign ID else create root folder and multiItem created with loggedin user
      'If project folder found under root folder then assign id else create project folder under root folder
      Try
        SIS.QCM.qcmProjects.GetProjectFromERP(prj.t_cprj)
      Catch ex As Exception
      End Try
      Dim itm As SIS.DMS.UI.apiItem = Nothing
      '1. Root Folder
      If prj.t_rfid > 0 Then
        itm = SIS.DMS.UI.GetItem(prj.t_rfid)
        If itm IsNot Nothing Then
          itm.Description = prj.t_rfld
          itm.FullDescription = prj.t_rfld
          SIS.DMS.UI.apiItem.UpdateData(itm)
        Else
          prj.t_rfid = 0
        End If
      End If
      If prj.t_rfid = 0 Then
        itm = SIS.DMS.UI.GetItem(prj.t_rfld, 1, "")
        If itm Is Nothing Then
          itm = New SIS.DMS.UI.apiItem
          With itm
            .Description = prj.t_rfld
            .FullDescription = prj.t_rfld
            .ItemTypeID = 1 'Folder
            .RevisionNo = "00"
            .StatusID = 6 'Published
            .CreatedBy = UserID
            .CreatedOn = Now
            .CreateFolder = 3
            .DeleteFolder = 3
            .RenameFolder = 3
            .CreateFile = 3
            .DeleteFile = 3
            .GrantAuthorization = 3
            .Publish = 3
            .ShowInList = 3
            .BrowseList = 3
            .ProjectID = prj.t_cprj
          End With
          itm = SIS.DMS.UI.apiItem.InsertData(itm)
        End If
        prj.t_rfid = itm.ItemID
      End If
      SIS.DMS.UI.LinkToUser(itm.ItemID, UserID)
      '2. Project Folder
      If prj.t_pfid > 0 Then
        itm = SIS.DMS.UI.GetItem(prj.t_pfid)
        If itm IsNot Nothing Then
          itm.Description = prj.t_pfld
          itm.FullDescription = prj.t_pfld
          SIS.DMS.UI.apiItem.UpdateData(itm)
        Else
          prj.t_pfid = 0
        End If
      End If
      If prj.t_pfid = 0 Then
        itm = SIS.DMS.UI.GetItem(prj.t_pfld, 1, prj.t_rfid)
        If itm Is Nothing Then
          itm = New SIS.DMS.UI.apiItem
          With itm
            .ParentItemID = prj.t_rfid
            .Description = prj.t_pfld
            .FullDescription = prj.t_pfld
            .ItemTypeID = 1 'Folder
            .RevisionNo = "00"
            .StatusID = 6 'Published
            .CreatedBy = UserID
            .CreatedOn = Now
            .CreateFolder = 3
            .DeleteFolder = 3
            .RenameFolder = 3
            .CreateFile = 3
            .DeleteFile = 3
            .GrantAuthorization = 3
            .Publish = 3
            .ShowInList = 3
            .BrowseList = 3
            .ProjectID = prj.t_cprj
          End With
          itm = SIS.DMS.UI.apiItem.InsertData(itm)
        End If
        prj.t_pfid = itm.ItemID
      End If
      SIS.DMS.erpDMSH.Update(prj)
      For Each prjf As SIS.DMS.erpDMSD In prjfs
        'if transmittal folder found under project folder then assign id else create transmittal folder under project folder
        If prjf.t_tfid > 0 Then
          itm = SIS.DMS.UI.GetItem(prjf.t_tfid)
          If itm IsNot Nothing Then
            itm.Description = prjf.t_tfld
            itm.FullDescription = prjf.t_tfld
            SIS.DMS.UI.apiItem.UpdateData(itm)
          Else
            prjf.t_tfid = 0
          End If
        End If
        If prjf.t_tfid = 0 Then
          itm = SIS.DMS.UI.GetItem(prjf.t_tfld, 1, prj.t_pfid)
          If itm Is Nothing Then
            itm = New SIS.DMS.UI.apiItem
            With itm
              .ParentItemID = prj.t_pfid
              .Description = prjf.t_tfld
              .FullDescription = prjf.t_tfld
              .ItemTypeID = 1 'Folder
              .RevisionNo = "00"
              .StatusID = 6 'Published
              .CreatedBy = UserID
              .CreatedOn = Now
              .CreateFolder = 3
              .DeleteFolder = 3
              .RenameFolder = 3
              .CreateFile = 3
              .DeleteFile = 3
              .GrantAuthorization = 3
              .Publish = 3
              .ShowInList = 3
              .BrowseList = 3
              .ProjectID = prj.t_cprj
            End With
            itm = SIS.DMS.UI.apiItem.InsertData(itm)
          End If
          prjf.t_tfid = itm.ItemID
          SIS.DMS.erpDMSD.Update(prjf)
        End If
      Next
      gvDmsD.DataBind()
      gvDmsH.DataBind()
    End If
  End Sub
  Private UserID As String = ""
  Private Sub DMS_GF_dmsEntry_Load(sender As Object, e As EventArgs) Handles Me.Load
    If Request.QueryString("UserID") IsNot Nothing Then
      UserID = Request.QueryString("UserID")
    End If
    If UserID = "" Then UserID = "0340"
  End Sub
End Class
