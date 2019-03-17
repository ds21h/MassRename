Imports System.IO
Imports System.Windows.Forms
Imports System.Resources

Public Class FrmMain
  Private mResManager As ResourceManager

  Private Const cOK As Integer = 0
  Private Const cError As Integer = 10

  Private mActions(-1) As Action
  Private mMaxAction As Integer = -1

  Private mPath As String = ""

  Private mInput As String = ""

  Public Sub New()
    InitializeComponent()
    mResManager = New ResourceManager("MassRename.MassRenameStrings", GetType(FrmMain).Assembly)
    sMakeDrives()
  End Sub

  Private Sub sMakeDrives()
    Dim lStations() As DriveInfo
    Dim lDriveName As String
    Dim lDriveCount As Integer
    Dim lDriveNode As TreeNode

    TreeDir.BeginUpdate()
    TreeDir.Nodes.Clear()
    lDriveCount = 0
    lStations = DriveInfo.GetDrives
    For Each lStation In lStations
      lDriveName = lStation.Name
      If Mid(lDriveName, Len(lDriveName), 1) = "\" Then
        lDriveName = Mid(lDriveName, 1, Len(lDriveName) - 1)
      End If
      If lStation.IsReady Then
        lDriveName = "(" & lStation.VolumeLabel & ") " & lDriveName
      End If

      lDriveNode = New TreeNode
      lDriveNode.Text = lDriveName
      TreeDir.Nodes.Add(lDriveNode)
      If lStation.IsReady Then
        TreeDir.Nodes(lDriveCount).Nodes.Add("x")
      End If
      lDriveCount = lDriveCount + 1
    Next lStation
    TreeDir.EndUpdate()
  End Sub

  Private Sub sFillNode(ByVal pPath As String, ByVal pStartNode As TreeNode)
    Dim lSubDirs() As String
    Dim lDirCount As Integer

    lSubDirs = sGetSubDirs(pPath)
    lDirCount = 0
    TreeDir.BeginUpdate()
    pStartNode.Nodes.Clear()
    For Each lSubDir In lSubDirs
      pStartNode.Nodes.Add(lSubDir)
      pStartNode.Nodes(lDirCount).Nodes.Add("x")
      lDirCount = lDirCount + 1
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
    sListFiles()
  End Sub

  Private Sub sListFiles()
    Dim lMainDir As DirectoryInfo
    Dim lFiles() As FileInfo
    Dim lLine As ListViewItem

    lMainDir = New DirectoryInfo(mPath)
    LstDir.BeginUpdate()
    LstDir.Items.Clear()
    Try
      lFiles = lMainDir.GetFiles
      For Each lFile In lFiles
        lLine = New ListViewItem
        lLine.Text = lFile.Name
        LstDir.Items.Add(lLine)
      Next
    Catch ex As Exception
    End Try
    LstDir.EndUpdate()
    mInput = ""
    TxtSelected.Text = ""
  End Sub

  Private Sub hLstDir_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles LstDir.SelectedIndexChanged
    Dim lRegel As ListViewItem

    If LstDir.SelectedItems.Count > 0 Then
      lRegel = LstDir.SelectedItems(0)
      mInput = lRegel.Text
    Else
      mInput = ""
    End If
    TxtSelected.Text = sRename(mInput)
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

    sRename = lNameOut
  End Function

  Private Sub BttnApply_Click(sender As Object, e As EventArgs) Handles BttnApply.Click
    Dim lFiles As String()
    Dim lLength As Integer
    Dim lCount As Integer
    Dim lItem As ListViewItem

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

    sListFiles()
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
End Class
