Imports System.IO
Imports System.Windows.Forms
Imports System.Resources
Imports System.Collections.ObjectModel

Public Class FrmMain
  Private mResManager As ResourceManager
  Private mFileWatchers As New Collection(Of FileSystemWatcher)
  Private mDirWatcher As FileSystemWatcher

  Delegate Sub delReWorkDir(pPath As String)
  Delegate Sub delReWorkList()
  Private dReWorkDir As delReWorkDir
  Private dReWorkList As delReWorkList

  Private Const cOK As Integer = 0
  Private Const cError As Integer = 10

  Private mActions(-1) As Action
  Private mMaxAction As Integer = -1

  Private mPath As String = ""

  Private mInput As String = ""

  Public Sub New()
    InitializeComponent()
    mResManager = New ResourceManager("MassRename.MassRenameStrings", GetType(FrmMain).Assembly)
    dReWorkDir = New delReWorkDir(AddressOf hReworkDir)
    dReWorkList = New delReWorkList(AddressOf hReworkList)
    sReworkDrives()
    sSetupDirWatcher()
  End Sub

  Private Sub sSetupDirWatcher()
    mDirWatcher = New FileSystemWatcher()
    mDirWatcher.IncludeSubdirectories = False
    mDirWatcher.NotifyFilter = NotifyFilters.FileName
    AddHandler mDirWatcher.Created, AddressOf hOnFileChanged
    AddHandler mDirWatcher.Deleted, AddressOf hOnFileChanged
    AddHandler mDirWatcher.Renamed, AddressOf hOnFileRenamed
    mDirWatcher.EnableRaisingEvents = False
  End Sub

  Private Sub hOnChanged(source As Object, e As FileSystemEventArgs)
    Dim lParent As String

    'This listener runs on another thread, so it cannot access the controls in the UI thread
    'Use the invoke function to accomplish that
    lParent = sGetParentPath(e.FullPath)
    Me.Invoke(dReWorkDir, lParent)
  End Sub

  Private Sub hOnRenamed(source As Object, e As RenamedEventArgs)
    Dim lParentOld As String
    Dim lParentNew As String

    'This listener runs on another thread, so it cannot access the controls in the UI thread
    'Use the invoke function to accomplish that
    lParentOld = sGetParentPath(e.OldFullPath)
    lParentNew = sGetParentPath(e.FullPath)
    Me.Invoke(dReWorkDir, lParentOld)
    If lParentOld <> lParentNew Then
      Me.Invoke(dReWorkDir, lParentNew)
    End If
  End Sub

  Private Sub hOnFileChanged(source As Object, e As FileSystemEventArgs)
    'This listener runs on another thread, so it cannot access the controls in the UI thread
    'Use the invoke function to accomplish that
    Me.Invoke(dReWorkList)
  End Sub

  Private Sub hOnFileRenamed(source As Object, e As RenamedEventArgs)
    'This listener runs on another thread, so it cannot access the controls in the UI thread
    'Use the invoke function to accomplish that
    Me.Invoke(dReWorkList)
  End Sub

  Private Function sGetParentPath(pPath As String) As String
    Dim lCount As Integer
    Dim lEnd As Integer
    Dim lResult As String

    lEnd = 0
    For lCount = pPath.Length To 1 Step -1
      If Mid(pPath, lCount, 1) = "\" Then
        lEnd = lCount
        Exit For
      End If
    Next
    If lEnd > 0 Then
      lResult = Mid(pPath, 1, lEnd - 1)
    Else
      lResult = ""
    End If
    Return lResult
  End Function

  Private Sub hReworkDir(pPath As String)
    sReWorkNode(TreeDir.Nodes, pPath)
  End Sub

  Private Sub sReWorkNode(pNodes As TreeNodeCollection, pUri As String)
    Dim lNode As TreeNode = Nothing
    Dim lTag As String
    Dim lFound As Boolean
    Dim lCount As Integer
    Dim lTarget As String
    Dim lUriRest As String

    lTarget = pUri
    lUriRest = ""
    For lCount = 1 To pUri.Length
      If Mid(pUri, lCount, 1) = "\" Then
        lFound = True
        lTarget = Mid(pUri, 1, lCount - 1)
        lUriRest = Mid(pUri, lCount + 1)
        Exit For
      End If
    Next
    lFound = False
    For lCount = 0 To pNodes.Count - 1
      lNode = pNodes.Item(lCount)
      lTag = DirectCast(lNode.Tag, String)
      If lTag = lTarget Then
        lFound = True
        Exit For
      End If
    Next

    If lFound Then
      If lNode.IsExpanded Then
        If lUriRest = "" Then
          sProcessNode(lNode)
          sFillFileList()
        Else
          sReWorkNode(lNode.Nodes, lUriRest)
        End If
      Else
        sSetNodeExpand(lNode)
      End If
    End If
  End Sub

  Private Sub sSetNodeExpand(pNode As TreeNode)
    Dim lPath As String
    Dim lSubDirs() As String

    lPath = sDeterminePath(pNode)
    lSubDirs = sGetSubDirs(lPath)
    If lSubDirs.Length = 0 Then
      pNode.Nodes.Clear()
    Else
      If pNode.Nodes.Count = 0 Then
        pNode.Nodes.Add("x")
      End If
    End If
  End Sub

  Private Sub sProcessNode(pNode As TreeNode)
    Dim lPath As String
    Dim lSubDirs() As String
    Dim lCountDir As Integer
    Dim lCountNode As Integer
    Dim lValueDir As String
    Dim lValueNode As String
    Dim lValueSmall As String
    Dim cValueEnd As String = Chr(255)
    Dim lStatus As Integer
    Dim lDirNode As TreeNode

    lPath = sDeterminePath(pNode)
    lSubDirs = sGetSubDirs(lPath)
    If lSubDirs.Length > 0 Then
      lValueDir = lSubDirs(0)
    Else
      lValueDir = cValueEnd
    End If
    If pNode.Nodes.Count > 0 Then
      lValueNode = DirectCast(pNode.Nodes(0).Tag, String)
    Else
      lValueNode = cValueEnd
    End If
    lCountDir = 0
    lCountNode = 0
    Do While (True)
      If lValueDir < lValueNode Then
        lValueSmall = lValueDir
      Else
        lValueSmall = lValueNode
      End If
      If lValueSmall = cValueEnd Then
        Exit Do
      End If
      lStatus = 0
      If lValueDir = lValueSmall Then
        lStatus = 1
        lCountDir = lCountDir + 1
        If lCountDir < lSubDirs.Length Then
          lValueDir = lSubDirs(lCountDir)
        Else
          lValueDir = cValueEnd
        End If
      End If
      If lValueNode = lValueSmall Then
        If lStatus = 0 Then
          pNode.Nodes.RemoveAt(lCountNode)
          lCountNode = lCountNode - 1
        End If
        lStatus = 2
        lCountNode = lCountNode + 1
        If lCountNode < pNode.Nodes.Count Then
          lValueNode = DirectCast(pNode.Nodes(lCountNode).Tag, String)
        Else
          lValueNode = cValueEnd
        End If
      End If
      If lStatus = 1 Then
        lDirNode = New TreeNode
        lDirNode.Text = lValueSmall
        lDirNode.Tag = lValueSmall
        pNode.Nodes.Insert(lCountNode, lDirNode)
        lCountNode = lCountNode + 1
      End If
    Loop
  End Sub

  Private Sub sReworkDrives()
    Dim lStations() As DriveInfo
    Dim lCountDrive As Integer
    Dim lCountNode As Integer
    Dim lValueDrive As String
    Dim lValueNode As String
    Dim lValueSmall As String
    Dim cValueEnd As String = Chr(255)
    Dim lStatus As Integer
    Dim lDriveName As String = ""
    Dim lDriveNode As TreeNode
    Dim lWatcher As FileSystemWatcher

    TmrRefresh.Enabled = False

    lStations = DriveInfo.GetDrives
    If lStations.Length > 0 Then
      lValueDrive = sStripDrive(lStations(0))
    Else
      lValueDrive = cValueEnd
    End If
    If TreeDir.Nodes.Count > 0 Then
      lValueNode = DirectCast(TreeDir.Nodes(0).Tag, String)
    Else
      lValueNode = cValueEnd
    End If
    lCountDrive = 0
    lCountNode = 0
    Do While (True)
      If lValueDrive < lValueNode Then
        lValueSmall = lValueDrive
      Else
        lValueSmall = lValueNode
      End If
      If lValueSmall = cValueEnd Then
        Exit Do
      End If
      lStatus = 0
      If lValueDrive = lValueSmall Then
        If lStations(lCountDrive).IsReady Then
          lDriveName = lStations(lCountDrive).VolumeLabel
          lStatus = 1
        End If
        lCountDrive = lCountDrive + 1
        If lCountDrive < lStations.Length Then
          lValueDrive = sStripDrive(lStations(lCountDrive))
        Else
          lValueDrive = cValueEnd
        End If
      End If
      If lValueNode = lValueSmall Then
        If lStatus = 0 Then
          TreeDir.Nodes.RemoveAt(lCountNode)
          mFileWatchers(lCountNode).EnableRaisingEvents = False
          mFileWatchers(lCountNode).Dispose()
          mFileWatchers.RemoveAt(lCountNode)
          lCountNode = lCountNode - 1
        End If
        lStatus = 2
        lCountNode = lCountNode + 1
        If lCountNode < TreeDir.Nodes.Count Then
          lValueNode = DirectCast(TreeDir.Nodes(lCountNode).Tag, String)
        Else
          lValueNode = cValueEnd
        End If
      End If
      If lStatus = 1 Then
        lDriveNode = New TreeNode
        lDriveNode.Text = "(" & lDriveName & ") " & lValueSmall
        lDriveNode.Tag = lValueSmall
        TreeDir.Nodes.Insert(lCountNode, lDriveNode)
        If Not sEmptyNode(lDriveNode) Then
          lDriveNode.Nodes.Add("x")
        End If
        lWatcher = sSetupWatcher(lValueSmall & "\")
        mFileWatchers.Insert(lCountNode, lWatcher)
        lCountNode = lCountNode + 1
      End If
    Loop

    TmrRefresh.Enabled = True
  End Sub

  Private Function sStripDrive(pStation As DriveInfo) As String
    Dim lDriveTag As String

    lDriveTag = pStation.Name
    If Mid(lDriveTag, Len(lDriveTag), 1) = "\" Then
      lDriveTag = Mid(lDriveTag, 1, Len(lDriveTag) - 1)
    End If

    Return lDriveTag
  End Function

  Private Function sSetupWatcher(pPath As String) As FileSystemWatcher
    Dim lWatcher As FileSystemWatcher

    lWatcher = New FileSystemWatcher(pPath)
    lWatcher.IncludeSubdirectories = True
    lWatcher.NotifyFilter = NotifyFilters.DirectoryName
    AddHandler lWatcher.Created, AddressOf hOnChanged
    AddHandler lWatcher.Deleted, AddressOf hOnChanged
    AddHandler lWatcher.Renamed, AddressOf hOnRenamed
    lWatcher.EnableRaisingEvents = True

    Return lWatcher
  End Function

  Private Function sEmptyNode(pNode As TreeNode) As Boolean
    Dim lPath As String
    Dim lSubDirs() As String
    Dim lResult As Boolean

    lPath = sDeterminePath(pNode)
    lSubDirs = sGetSubDirs(lPath)
    If lSubDirs.Length > 0 Then
      lResult = False
    Else
      lResult = True
    End If

    Return lResult
  End Function

  Private Sub sFillNode(ByVal pPath As String, ByVal pStartNode As TreeNode)
    Dim lSubDirs() As String
    Dim lDirCount As Integer
    Dim lDirNode As TreeNode

    lSubDirs = sGetSubDirs(pPath)
    TreeDir.BeginUpdate()
    pStartNode.Nodes.Clear()
    For lDirCount = 0 To lSubDirs.Length - 1
      lDirNode = New TreeNode
      lDirNode.Text = lSubDirs(lDirCount)
      lDirNode.Tag = lSubDirs(lDirCount)
      pStartNode.Nodes.Add(lDirNode)
      If Not sEmptyNode(lDirNode) Then
        lDirNode.Nodes.Add("x")
      End If
    Next
    TreeDir.EndUpdate()
  End Sub

  Private Sub hTreeDir_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeDir.BeforeExpand
    Dim lSelNode As TreeNode
    Dim lPath As String

    lSelNode = e.Node
    lPath = sDeterminePath(lSelNode)
    sFillNode(lPath, lSelNode)
  End Sub

  Private Function sDeterminePath(ByVal pInNode As TreeNode) As String
    Dim lPath As String
    Dim lPosCount As Integer
    Dim lStartPos As Integer
    Dim lRemLength As Integer

    lPath = pInNode.FullPath
    If Mid(lPath, 1, 1) = "(" Then
      For lPosCount = 2 To Len(lPath)
        If Mid(lPath, lPosCount, 1) = ")" Then
          lStartPos = lPosCount + 2
          lRemLength = (Len(lPath) - lStartPos) + 1
          lPath = Mid(lPath, lStartPos, lRemLength)
          Exit For
        End If
      Next lPosCount
    End If
    lPath = lPath & "\"
    Return lPath
  End Function

  Private Function sGetSubDirs(ByVal pPath As String) As String()
    Dim lSubDirs() As String
    Dim lMainDir As DirectoryInfo
    Dim lDirs() As DirectoryInfo
    Dim lDirNumber As Integer
    Dim lDirCount As Integer

    lDirCount = 0
    lMainDir = New DirectoryInfo(pPath)
    Try
      lDirs = lMainDir.GetDirectories
      lDirNumber = lDirs.GetLength(0)
      ReDim lSubDirs(lDirNumber - 1)
      For Each lDir In lDirs
        lSubDirs(lDirCount) = lDir.Name
        lDirCount = lDirCount + 1
      Next
    Catch ex As Exception
      ReDim lSubDirs(-1)
    End Try
    Return lSubDirs
  End Function

  Private Sub hTreeDir_BeforeSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeDir.BeforeSelect
    Dim lSelNode As TreeNode

    lSelNode = e.Node
    mPath = sDeterminePath(lSelNode)
    sFillFileList()
    mDirWatcher.Path = mPath
    mDirWatcher.EnableRaisingEvents = True
  End Sub

  Private Sub hReworkList()
    sFillFileList()
  End Sub

  Private Sub sFillFileList()
    Dim lMainDir As DirectoryInfo

    If mPath <> "" Then
      lMainDir = New DirectoryInfo(mPath)
      If lMainDir.Exists Then
        sReWorkFileList(lMainDir)
      Else
        LstDir.Items.Clear()
        mInput = ""
        TxtSelected.Text = ""
        sSetSelection()
      End If
    End If
  End Sub

  Private Sub sReWorkFileList(pDir As DirectoryInfo)
    Dim lFiles() As FileInfo
    Dim lCountFile As Integer
    Dim lCountItem As Integer
    Dim lValueFile As String
    Dim lValueItem As String
    Dim lValueSmall As String
    Dim cValueEnd As String = Chr(255)
    Dim lStatus As Integer

    lFiles = pDir.GetFiles
    If lFiles.Length > 0 Then
      lValueFile = lFiles(0).Name
    Else
      lValueFile = cValueEnd
    End If
    If LstDir.Items.Count > 0 Then
      lValueItem = LstDir.Items(0).Text
    Else
      lValueItem = cValueEnd
    End If
    lCountFile = 0
    lCountItem = 0
    Do While (True)
      If lValueFile < lValueItem Then
        lValueSmall = lValueFile
      Else
        lValueSmall = lValueItem
      End If
      If lValueSmall = cValueEnd Then
        Exit Do
      End If
      lStatus = 0
      If lValueFile = lValueSmall Then
        lStatus = 1
        lCountFile = lCountFile + 1
        If lCountFile < lFiles.Length Then
          lValueFile = lFiles(lCountFile).Name
        Else
          lValueFile = cValueEnd
        End If
      End If
      If lValueItem = lValueSmall Then
        If lStatus = 0 Then
          LstDir.Items.RemoveAt(lCountItem)
          lCountItem = lCountItem - 1
        End If
        lStatus = 2
        lCountItem = lCountItem + 1
        If lCountItem < LstDir.Items.Count Then
          lValueItem = LstDir.Items(lCountItem).Text
        Else
          lValueItem = cValueEnd
        End If
      End If
      If lStatus = 1 Then
        LstDir.Items.Insert(lCountItem, lValueSmall)
        lCountItem = lCountItem + 1
      End If
    Loop

  End Sub

  Private Sub hLstDir_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles LstDir.SelectedIndexChanged
    Dim lRegel As ListViewItem

    If LstDir.SelectedItems.Count > 0 Then
      lRegel = LstDir.SelectedItems(0)
      mInput = lRegel.Text
      TxtSelected.Text = sRename(mInput)
    Else
      mInput = ""
      TxtSelected.Text = ""
    End If
    sSetSelection()
  End Sub

  Private Sub hBttnInsert_Click(sender As System.Object, e As System.EventArgs) Handles BttnInsert.Click
    Dim lPosition As Integer
    Dim lAction As Action

    lPosition = CInt(TxtStart.Text)
    lAction = New Action(Action.Insert, lPosition, TxtInsert.Text.Length, TxtInsert.Text)
    sNewAction(lAction)
    TxtSelected.Text = sRename(mInput)
  End Sub

  Private Sub hBttnDelete_Click(sender As System.Object, e As System.EventArgs) Handles BttnDelete.Click
    Dim lPosition As Integer
    Dim lLength As Integer
    Dim lAction As Action

    lPosition = CInt(TxtStart.Text)
    lLength = CInt(TxtLength.Text)
    lAction = New Action(Action.Delete, lPosition, lLength, "")
    sNewAction(lAction)
    TxtSelected.Text = sRename(mInput)
  End Sub

  Private Function sNewAction(pAction As Action) As Integer
    mMaxAction = mMaxAction + 1
    ReDim Preserve mActions(mMaxAction)
    mActions(mMaxAction) = pAction
    sInitGrid()
    sNewAction = cOK
  End Function

  Private Sub sSetSelection()
    TxtStart.Text = CStr(TxtSelected.SelectionStart)
    TxtLength.Text = CStr(TxtSelected.SelectionLength)
  End Sub

  Private Sub hTxtSelected_Leave(sender As System.Object, e As System.EventArgs) Handles TxtSelected.Leave
    sSetSelection()
  End Sub

  Private Sub hTxtSelected_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles TxtSelected.MouseUp
    sSetSelection()
  End Sub

  Private Sub hTxtSelected_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TxtSelected.KeyUp
    sSetSelection()
  End Sub

  Private Sub sInitGrid()
    DgvActions.RowCount = mMaxAction + 1
    DgvActions.Refresh()
  End Sub

  Private Sub hDgvActions_CellValueNeeded(sender As Object, e As DataGridViewCellValueEventArgs) Handles DgvActions.CellValueNeeded
    Dim lRow As Integer

    lRow = e.RowIndex
    Select Case e.ColumnIndex
      Case 0
        e.Value = mResManager.GetString(mActions(lRow).xAktieTekst)
      Case 1
        e.Value = mActions(lRow).xStart
      Case 2
        e.Value = mActions(lRow).xLength
      Case 3
        e.Value = mActions(lRow).xValue
    End Select
  End Sub

  Private Function sRename(pNameIn As String) As String
    Dim lNameIn As String
    Dim lNameOut As String

    lNameIn = pNameIn
    lNameOut = lNameIn
    For Each lAction In mActions
      lNameOut = lAction.xApply(lNameIn)
      lNameIn = lNameOut
    Next

    Return lNameOut
  End Function

  Private Sub BttnApply_Click(sender As Object, e As EventArgs) Handles BttnApply.Click
    Dim lFiles As String()
    Dim lLength As Integer
    Dim lCount As Integer
    Dim lItem As ListViewItem

    mDirWatcher.EnableRaisingEvents = False
    If RdoAll.Checked Then
      lLength = LstDir.Items.Count - 1
      ReDim lFiles(lLength)
      lCount = 0
      For Each lItem In LstDir.Items
        lFiles(lCount) = lItem.Text
        lCount = lCount + 1
      Next
    Else
      lLength = LstDir.SelectedItems.Count - 1
      ReDim lFiles(lLength)
      lCount = 0
      For Each lItem In LstDir.SelectedItems
        lFiles(lCount) = lItem.Text
        lCount = lCount + 1
      Next
    End If

    For Each lFile In lFiles
      sRenameFile(lFile)
    Next

    LstDir.Items.Clear()
    sFillFileList()
    mDirWatcher.EnableRaisingEvents = True
  End Sub

  Private Sub sRenameFile(pName As String)
    Dim lNameOut As String
    Dim lFileIn As String
    Dim lFileOut As String
    Dim lFile As FileInfo

    lNameOut = sRename(pName)
    lFileIn = mPath & pName
    lFileOut = mPath & lNameOut
    lFile = New FileInfo(lFileIn)
    lFile.MoveTo(lFileOut)
  End Sub

  Private Sub DgvActions_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles DgvActions.RowsRemoved
    Dim lIndex As Integer
    Dim lCount As Integer

    If e.RowCount > 0 Then
      lIndex = e.RowIndex
      For lCount = e.RowIndex To mActions.Length - 2
        mActions(lCount) = mActions(lCount + 1)
      Next
      mMaxAction = mMaxAction - 1
      ReDim Preserve mActions(mMaxAction)
      TxtSelected.Text = sRename(mInput)
    End If
  End Sub

  Private Sub TxtSelected_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtSelected.KeyPress
    e.Handled = True
  End Sub

  Private Sub TxtSelected_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtSelected.KeyDown
    If e.KeyCode = Keys.Delete Then
      e.Handled = True
    End If
  End Sub

  Private Sub BtnInit_Click(sender As Object, e As EventArgs) Handles BtnInit.Click
    DgvActions.RowCount = 0
    mMaxAction = -1
    ReDim mActions(mMaxAction)
    sInitGrid()
    TxtSelected.Text = sRename(mInput)
  End Sub

  Private Sub TmrRefresh_Tick(sender As Object, e As EventArgs) Handles TmrRefresh.Tick
    sReworkDrives()
  End Sub
End Class
