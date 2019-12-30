Imports System
Imports System.Web
Imports System.Xml
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Web.Script.Serialization
Namespace SIS.DMS
  <DataObject()>
  Partial Public Class UI


    <DataObject()>
    Partial Public Class apiItem
      Public Property IsAdmin As Boolean = False
      Public Property ChildCount As Integer = 0


#Region "********Properties**********"
      Public Property ItemID As Int32 = 0
      Public Property InheritFromParent As Boolean = False
      Public Property UserID As String = ""
      Public Property Description As String = ""
      Public Property RevisionNo As String = ""
      Public Property ItemTypeID As Int32 = 0
      Public Property StatusID As Int32 = 0
      Public Property StatusID_Description As String = ""
      Public Property CreatedBy As String = ""
      Private _CreatedOn As String = ""
      Public Property MaintainAllLog As Boolean = False
      Public Property BackwardLinkedItemID As String = ""
      Public Property MaintainVersions As Boolean = False
      Public Property MaintainStatusLog As Boolean = False
      Public Property LinkedItemID As String = ""
      Public Property LinkedItemTypeID As String = ""
      Public Property BackwardLinkedItemTypeID As String = ""
      Public Property IsMultiBackward As Boolean = False
      Public Property IsgecVaultID As String = ""
      Public Property DeleteFile As Integer = 1
      Public Property CreateFile As Integer = 1
      Public Property BrowseList As Integer = 1
      Public Property GrantAuthorization As Integer = 1
      Public Property CreateFolder As Integer = 1
      Public Property Publish As Integer = 1
      Public Property DeleteFolder As Integer = 1
      Public Property RenameFolder As Integer = 1
      Public Property ShowInList As Integer = 1
      Public Property CompanyID As String = ""
      Public Property ChildItemID As String = ""
      Public Property DepartmentID As String = ""
      Public Property DivisionID As String = ""
      Public Property IsMultiParent As Boolean = False
      Public Property ConvertedStatusID As String = ""
      Public Property IsMultiChild As Boolean = False
      Public Property ParentItemID As String = ""
      Public Property ProjectID As String = ""
      Public Property ForwardLinkedItemTypeID As String = ""
      Public Property IsMultiForward As Boolean = False
      Public Property IsMultiLinked As Boolean = False
      Public Property ForwardLinkedItemID As String = ""
      Public Property KeyWords As String = ""
      Public Property WBSID As String = ""
      Public Property FullDescription As String = ""
      Public Property EMailID As String = ""
      Public Property SearchInParent As Boolean = False
      Public Property Approved As Boolean = False
      Public Property Rejected As Boolean = False
      Public Property ActionRemarks As String = ""
      Public Property ActionBy As String = ""
      Private _ActionOn As String = ""
      Public Property IsError As String = ""
      Public Property ErrorMessage As String = ""
      Public Property CreatedOn() As String
        Get
          If Not _CreatedOn = String.Empty Then
            Return Convert.ToDateTime(_CreatedOn).ToString("dd/MM/yyyy HH:mm")
          End If
          Return _CreatedOn
        End Get
        Set(ByVal value As String)
          _CreatedOn = value
        End Set
      End Property
      Public Property ActionOn() As String
        Get
          If Not _ActionOn = String.Empty Then
            Return Convert.ToDateTime(_ActionOn).ToString("dd/MM/yyyy")
          End If
          Return _ActionOn
        End Get
        Set(ByVal value As String)
          If Convert.IsDBNull(value) Then
            _ActionOn = ""
          Else
            _ActionOn = value
          End If
        End Set
      End Property

#End Region
      Public Shared Function GetItem(data As String) As SIS.DMS.UI.apiItem
        Dim mRet As SIS.DMS.UI.apiItem = Nothing
        If data <> "" Then
          mRet = New SIS.DMS.UI.apiItem
          mRet = (New JavaScriptSerializer).Deserialize(data, GetType(SIS.DMS.UI.apiItem))
        End If
        Return mRet
      End Function

#Region "********* INSERT / UPDATE ***********"
      Public Shared Function InsertData(ByVal Record As SIS.DMS.UI.apiItem) As SIS.DMS.UI.apiItem
        Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
          Using Cmd As SqlCommand = Con.CreateCommand()
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandText = "spdmsItemsInsert"
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@InheritFromParent", SqlDbType.Bit, 3, Record.InheritFromParent)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@UserID", SqlDbType.NVarChar, 9, IIf(Record.UserID = "", Convert.DBNull, Record.UserID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Description", SqlDbType.NVarChar, 251, Record.Description)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@RevisionNo", SqlDbType.NVarChar, 51, Record.RevisionNo)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ItemTypeID", SqlDbType.Int, 11, Record.ItemTypeID)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@StatusID", SqlDbType.Int, 11, Record.StatusID)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@CreatedBy", SqlDbType.NVarChar, 9, Record.CreatedBy)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@CreatedOn", SqlDbType.DateTime, 21, Record.CreatedOn)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@MaintainAllLog", SqlDbType.Bit, 3, Record.MaintainAllLog)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@BackwardLinkedItemID", SqlDbType.Int, 11, IIf(Record.BackwardLinkedItemID = "", Convert.DBNull, Record.BackwardLinkedItemID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@MaintainVersions", SqlDbType.Bit, 3, Record.MaintainVersions)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@MaintainStatusLog", SqlDbType.Bit, 3, Record.MaintainStatusLog)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LinkedItemID", SqlDbType.Int, 11, IIf(Record.LinkedItemID = "", Convert.DBNull, Record.LinkedItemID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LinkedItemTypeID", SqlDbType.Int, 11, IIf(Record.LinkedItemTypeID = "", Convert.DBNull, Record.LinkedItemTypeID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@BackwardLinkedItemTypeID", SqlDbType.Int, 11, IIf(Record.BackwardLinkedItemTypeID = "", Convert.DBNull, Record.BackwardLinkedItemTypeID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IsMultiBackward", SqlDbType.Bit, 3, Record.IsMultiBackward)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IsgecVaultID", SqlDbType.NVarChar, 51, IIf(Record.IsgecVaultID = "", Convert.DBNull, Record.IsgecVaultID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@DeleteFile", SqlDbType.Int, 11, Record.DeleteFile)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@CreateFile", SqlDbType.Int, 11, Record.CreateFile)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@BrowseList", SqlDbType.Int, 11, Record.BrowseList)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@GrantAuthorization", SqlDbType.Int, 11, Record.GrantAuthorization)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@CreateFolder", SqlDbType.Int, 11, Record.CreateFolder)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Publish", SqlDbType.Int, 11, Record.Publish)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@DeleteFolder", SqlDbType.Int, 11, Record.DeleteFolder)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@RenameFolder", SqlDbType.Int, 11, Record.RenameFolder)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ShowInList", SqlDbType.Int, 11, Record.ShowInList)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@CompanyID", SqlDbType.NVarChar, 7, IIf(Record.CompanyID = "", Convert.DBNull, Record.CompanyID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ChildItemID", SqlDbType.Int, 11, IIf(Record.ChildItemID = "", Convert.DBNull, Record.ChildItemID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@DepartmentID", SqlDbType.NVarChar, 7, IIf(Record.DepartmentID = "", Convert.DBNull, Record.DepartmentID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@DivisionID", SqlDbType.NVarChar, 7, IIf(Record.DivisionID = "", Convert.DBNull, Record.DivisionID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IsMultiParent", SqlDbType.Bit, 3, Record.IsMultiParent)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ConvertedStatusID", SqlDbType.Int, 11, IIf(Record.ConvertedStatusID = "", Convert.DBNull, Record.ConvertedStatusID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IsMultiChild", SqlDbType.Bit, 3, Record.IsMultiChild)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ParentItemID", SqlDbType.Int, 11, IIf(Record.ParentItemID = "", Convert.DBNull, Record.ParentItemID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ProjectID", SqlDbType.NVarChar, 7, IIf(Record.ProjectID = "", Convert.DBNull, Record.ProjectID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ForwardLinkedItemTypeID", SqlDbType.Int, 11, IIf(Record.ForwardLinkedItemTypeID = "", Convert.DBNull, Record.ForwardLinkedItemTypeID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IsMultiForward", SqlDbType.Bit, 3, Record.IsMultiForward)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IsMultiLinked", SqlDbType.Bit, 3, Record.IsMultiLinked)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ForwardLinkedItemID", SqlDbType.Int, 11, IIf(Record.ForwardLinkedItemID = "", Convert.DBNull, Record.ForwardLinkedItemID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@KeyWords", SqlDbType.NVarChar, 251, IIf(Record.KeyWords = "", Convert.DBNull, Record.KeyWords))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@WBSID", SqlDbType.NVarChar, 9, IIf(Record.WBSID = "", Convert.DBNull, Record.WBSID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@FullDescription", SqlDbType.NVarChar, 1001, IIf(Record.FullDescription = "", Convert.DBNull, Record.FullDescription))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@EMailID", SqlDbType.NVarChar, 251, IIf(Record.EMailID = "", Convert.DBNull, Record.EMailID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@SearchInParent", SqlDbType.Bit, 3, Record.SearchInParent)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Approved", SqlDbType.Bit, 3, Record.Approved)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Rejected", SqlDbType.Bit, 3, Record.Rejected)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ActionRemarks", SqlDbType.NVarChar, 251, IIf(Record.ActionRemarks = "", Convert.DBNull, Record.ActionRemarks))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ActionBy", SqlDbType.NVarChar, 9, IIf(Record.ActionBy = "", Convert.DBNull, Record.ActionBy))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ActionOn", SqlDbType.DateTime, 21, IIf(Record.ActionOn = "", Convert.DBNull, Record.ActionOn))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IsError", SqlDbType.Bit, 3, IIf(Record.IsError = "", Convert.DBNull, Record.IsError))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ErrorMessage", SqlDbType.NVarChar, 501, IIf(Record.ErrorMessage = "", Convert.DBNull, Record.ErrorMessage))
            Cmd.Parameters.Add("@Return_ItemID", SqlDbType.Int, 11)
            Cmd.Parameters("@Return_ItemID").Direction = ParameterDirection.Output
            Con.Open()
            Cmd.ExecuteNonQuery()
            Record.ItemID = Cmd.Parameters("@Return_ItemID").Value
          End Using
        End Using
        Return Record
      End Function
      Public Shared Function UpdateData(ByVal Record As SIS.DMS.UI.apiItem) As SIS.DMS.UI.apiItem
        Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
          Using Cmd As SqlCommand = Con.CreateCommand()
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandText = "spdmsItemsUpdate"
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Original_ItemID", SqlDbType.Int, 11, Record.ItemID)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@InheritFromParent", SqlDbType.Bit, 3, Record.InheritFromParent)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@UserID", SqlDbType.NVarChar, 9, IIf(Record.UserID = "", Convert.DBNull, Record.UserID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Description", SqlDbType.NVarChar, 251, Record.Description)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@RevisionNo", SqlDbType.NVarChar, 51, Record.RevisionNo)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ItemTypeID", SqlDbType.Int, 11, Record.ItemTypeID)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@StatusID", SqlDbType.Int, 11, Record.StatusID)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@CreatedBy", SqlDbType.NVarChar, 9, Record.CreatedBy)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@CreatedOn", SqlDbType.DateTime, 21, Record.CreatedOn)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@MaintainAllLog", SqlDbType.Bit, 3, Record.MaintainAllLog)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@BackwardLinkedItemID", SqlDbType.Int, 11, IIf(Record.BackwardLinkedItemID = "", Convert.DBNull, Record.BackwardLinkedItemID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@MaintainVersions", SqlDbType.Bit, 3, Record.MaintainVersions)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@MaintainStatusLog", SqlDbType.Bit, 3, Record.MaintainStatusLog)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LinkedItemID", SqlDbType.Int, 11, IIf(Record.LinkedItemID = "", Convert.DBNull, Record.LinkedItemID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@LinkedItemTypeID", SqlDbType.Int, 11, IIf(Record.LinkedItemTypeID = "", Convert.DBNull, Record.LinkedItemTypeID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@BackwardLinkedItemTypeID", SqlDbType.Int, 11, IIf(Record.BackwardLinkedItemTypeID = "", Convert.DBNull, Record.BackwardLinkedItemTypeID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IsMultiBackward", SqlDbType.Bit, 3, Record.IsMultiBackward)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IsgecVaultID", SqlDbType.NVarChar, 51, IIf(Record.IsgecVaultID = "", Convert.DBNull, Record.IsgecVaultID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@DeleteFile", SqlDbType.Int, 11, Record.DeleteFile)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@CreateFile", SqlDbType.Int, 11, Record.CreateFile)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@BrowseList", SqlDbType.Int, 11, Record.BrowseList)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@GrantAuthorization", SqlDbType.Int, 11, Record.GrantAuthorization)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@CreateFolder", SqlDbType.Int, 11, Record.CreateFolder)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Publish", SqlDbType.Int, 11, Record.Publish)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@DeleteFolder", SqlDbType.Int, 11, Record.DeleteFolder)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@RenameFolder", SqlDbType.Int, 11, Record.RenameFolder)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ShowInList", SqlDbType.Int, 11, Record.ShowInList)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@CompanyID", SqlDbType.NVarChar, 7, IIf(Record.CompanyID = "", Convert.DBNull, Record.CompanyID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ChildItemID", SqlDbType.Int, 11, IIf(Record.ChildItemID = "", Convert.DBNull, Record.ChildItemID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@DepartmentID", SqlDbType.NVarChar, 7, IIf(Record.DepartmentID = "", Convert.DBNull, Record.DepartmentID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@DivisionID", SqlDbType.NVarChar, 7, IIf(Record.DivisionID = "", Convert.DBNull, Record.DivisionID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IsMultiParent", SqlDbType.Bit, 3, Record.IsMultiParent)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ConvertedStatusID", SqlDbType.Int, 11, IIf(Record.ConvertedStatusID = "", Convert.DBNull, Record.ConvertedStatusID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IsMultiChild", SqlDbType.Bit, 3, Record.IsMultiChild)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ParentItemID", SqlDbType.Int, 11, IIf(Record.ParentItemID = "", Convert.DBNull, Record.ParentItemID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ProjectID", SqlDbType.NVarChar, 7, IIf(Record.ProjectID = "", Convert.DBNull, Record.ProjectID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ForwardLinkedItemTypeID", SqlDbType.Int, 11, IIf(Record.ForwardLinkedItemTypeID = "", Convert.DBNull, Record.ForwardLinkedItemTypeID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IsMultiForward", SqlDbType.Bit, 3, Record.IsMultiForward)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IsMultiLinked", SqlDbType.Bit, 3, Record.IsMultiLinked)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ForwardLinkedItemID", SqlDbType.Int, 11, IIf(Record.ForwardLinkedItemID = "", Convert.DBNull, Record.ForwardLinkedItemID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@KeyWords", SqlDbType.NVarChar, 251, IIf(Record.KeyWords = "", Convert.DBNull, Record.KeyWords))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@WBSID", SqlDbType.NVarChar, 9, IIf(Record.WBSID = "", Convert.DBNull, Record.WBSID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@FullDescription", SqlDbType.NVarChar, 1001, IIf(Record.FullDescription = "", Convert.DBNull, Record.FullDescription))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@EMailID", SqlDbType.NVarChar, 251, IIf(Record.EMailID = "", Convert.DBNull, Record.EMailID))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@SearchInParent", SqlDbType.Bit, 3, Record.SearchInParent)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Approved", SqlDbType.Bit, 3, Record.Approved)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@Rejected", SqlDbType.Bit, 3, Record.Rejected)
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ActionRemarks", SqlDbType.NVarChar, 251, IIf(Record.ActionRemarks = "", Convert.DBNull, Record.ActionRemarks))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ActionBy", SqlDbType.NVarChar, 9, IIf(Record.ActionBy = "", Convert.DBNull, Record.ActionBy))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ActionOn", SqlDbType.DateTime, 21, IIf(Record.ActionOn = "", Convert.DBNull, Record.ActionOn))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@IsError", SqlDbType.Bit, 3, IIf(Record.IsError = "", Convert.DBNull, Record.IsError))
            SIS.SYS.SQLDatabase.DBCommon.AddDBParameter(Cmd, "@ErrorMessage", SqlDbType.NVarChar, 501, IIf(Record.ErrorMessage = "", Convert.DBNull, Record.ErrorMessage))
            Cmd.Parameters.Add("@RowCount", SqlDbType.Int)
            Cmd.Parameters("@RowCount").Direction = ParameterDirection.Output
            Con.Open()
            Cmd.ExecuteNonQuery()
          End Using
        End Using
        Return Record
      End Function

#End Region
      Sub New(rd As SqlDataReader)
        SIS.DMS.UI.NewObj(Me, rd)
      End Sub
      Sub New()
        'dummy
      End Sub
    End Class


    Public Shared Function Stringify(ctl As Control) As String
      Dim sb As StringBuilder = New StringBuilder()
      Dim sw As IO.StringWriter = New IO.StringWriter(sb)
      Dim hw As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(sw)
      ctl.RenderControl(hw)
      Return sb.ToString
    End Function
    Public Shared Function GetItem(ByVal ItemID As String) As SIS.DMS.UI.apiItem
      Dim Results As apiItem = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Dim Sql As String = ""
        Sql &= "   select dms_items.*,  "
        Sql &= "   dms_states.Description as StatusID_Description "
        Sql &= "   from dms_items "
        Sql &= "   inner join dms_states on dms_states.statusid = dms_items.statusid "
        Sql &= " WHERE "
        Sql &= " [DMS_Items].[ItemID]=" & ItemID
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Dim rd As SqlDataReader = Cmd.ExecuteReader()
          If rd.Read() Then
            Results = New apiItem(rd)
          End If
          rd.Close()
        End Using
      End Using
      Return Results
    End Function

    Public Shared Function GetItem(Description As String, ItemType As String, Parent As String) As SIS.DMS.UI.apiItem
      Dim Results As apiItem = Nothing
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Dim Sql As String = ""
        Sql &= "   select *  "
        Sql &= "   from dms_items "
        Sql &= "   WHERE "
        Sql &= "   lower(description)=lower('" & Description & "')"
        If ItemType <> "" Then
          Sql &= "   and itemtypeid=" & ItemType
        End If
        If Parent <> "" Then
          Sql &= "   and ParentItemID=" & Parent
        Else
          Sql &= "   and ParentItemID is null "
        End If
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Dim rd As SqlDataReader = Cmd.ExecuteReader()
          If rd.Read() Then
            Results = New apiItem(rd)
          End If
          rd.Close()
        End Using
      End Using
      Return Results
    End Function

    Public Shared Function LinkToUser(ItemID As String, UserID As String) As Boolean
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Dim Sql As String = ""
        Sql &= "   declare @uitm int, @cnt int  "
        Sql &= "   select @uitm=itemid from dms_items where itemtypeid=5 and UserID='" & UserID & "' "
        Sql &= "   select @cnt= isnull(count(*),0) from dms_multiitems where itemid=@uitm and multitypeid=11 and multiItemtypeid=1 and multiitemid=" & ItemID
        Sql &= "   if (@cnt=0)  "
        Sql &= "     insert dms_multiitems (itemid, multitypeid, multiitemtypeid, multiItemid, multisequence, CreatedBy, CreatedOn) values ( "
        Sql &= "     @uitm,11,1," & ItemID & ",0, @uitm, GetDate())"
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = Sql
          Con.Open()
          Cmd.ExecuteNonQuery()
        End Using
      End Using
      Return True
    End Function

    Public Shared Function DirectDeleteItem(ItemID As Integer) As Boolean
      Dim mRet As Boolean = True
      Using Con As SqlConnection = New SqlConnection(SIS.SYS.SQLDatabase.DBCommon.GetConnectionString())
        Con.Open()
        Using Cmd As SqlCommand = Con.CreateCommand()
          Cmd.CommandType = CommandType.Text
          Cmd.CommandText = "delete dms_Items where ItemID=" & ItemID
          Cmd.ExecuteNonQuery()
        End Using
      End Using
      Return mRet
    End Function


    Public Shared Function NewObj(this As Object, Reader As SqlDataReader) As Object
      Try
        For Each pi As System.Reflection.PropertyInfo In this.GetType.GetProperties
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
                      CallByName(this, pi.Name, CallType.Let, "0.00")
                    Case "bit"
                      CallByName(this, pi.Name, CallType.Let, Boolean.FalseString)
                    Case Else
                      CallByName(this, pi.Name, CallType.Let, String.Empty)
                  End Select
                Else
                  CallByName(this, pi.Name, CallType.Let, Reader(pi.Name))
                End If
              End If
            Catch ex As Exception
            End Try
          End If
        Next
      Catch ex As Exception
        Return Nothing
      End Try
      Return this
    End Function


  End Class

End Namespace
