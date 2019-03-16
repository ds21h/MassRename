Imports System.IO
Imports System.Windows.Forms

Public Class FrmMain
  Private Const cOK As Integer = 0
  Private Const cFout As Integer = 10

  Private mAkties(-1) As Action
  Private mMaxAktie As Integer = -1

  Private mPad As String = ""

  Private mInput As String = ""

  Public Sub New()
    InitializeComponent()
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
    mPad = sDeterminePath(lSelNode)
    sListFiles()
  End Sub

  Private Sub sListFiles()
    Dim lMainDir As DirectoryInfo
    Dim lFiles() As FileInfo
    Dim lLine As ListViewItem

    lMainDir = New DirectoryInfo(mPad)
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
    TxtSelected.Text = sHernoem(mInput)
    sZetSelectie()
  End Sub

  Private Sub hBttnInvoegen_Click(sender As System.Object, e As System.EventArgs) Handles BttnInvoegen.Click
    sInvoegen(Action.Invoegen)
  End Sub

  Private Sub BttnInvoegen1_Click(sender As Object, e As EventArgs) Handles BttnInvoegen1.Click
    sInvoegen(Action.Invoegen1)
  End Sub

  Private Sub sInvoegen(pAktieCode As Integer)
    Dim lPositie As Integer
    Dim lAktie As Action

    lPositie = CInt(TxtStart.Text)
    lAktie = New Action(pAktieCode, lPositie, TxtInvoegen.Text.Length, TxtInvoegen.Text)
    sNieuweAktie(lAktie)
    TxtSelected.Text = sHernoem(mInput)
  End Sub

  Private Sub hBttnVerwijder_Click(sender As System.Object, e As System.EventArgs) Handles BttnVerwijder.Click
    Dim lPositie As Integer
    Dim lLengte As Integer
    Dim lAktie As Action

    lPositie = CInt(TxtStart.Text)
    lLengte = CInt(TxtLengte.Text)
    lAktie = New Action(Action.Verwijderen, lPositie, lLengte, "")
    sNieuweAktie(lAktie)
    TxtSelected.Text = sHernoem(mInput)
  End Sub

  Private Sub BttnVerhoog_Click(sender As Object, e As EventArgs) Handles BttnVerhoog.Click
    Dim lPositie As Integer
    Dim lLengte As Integer
    Dim lAktie As Action

    lPositie = CInt(TxtStart.Text)
    lLengte = CInt(TxtLengte.Text)
    lAktie = New Action(Action.Verhoog, lPositie, lLengte, "")
    sNieuweAktie(lAktie)
    TxtSelected.Text = sHernoem(mInput)
  End Sub

  Private Sub BttnVerlaag_Click(sender As Object, e As EventArgs) Handles BttnVerlaag.Click
    Dim lPositie As Integer
    Dim lLengte As Integer
    Dim lAktie As Action

    lPositie = CInt(TxtStart.Text)
    lLengte = CInt(TxtLengte.Text)
    lAktie = New Action(Action.Verlaag, lPositie, lLengte, "")
    sNieuweAktie(lAktie)
    TxtSelected.Text = sHernoem(mInput)
  End Sub

  Private Function sNieuweAktie(pAktie As Action) As Integer
    mMaxAktie = mMaxAktie + 1
    ReDim Preserve mAkties(mMaxAktie)
    mAkties(mMaxAktie) = pAktie
    sInitGrid()
    sNieuweAktie = cOK
  End Function

  Private Sub sZetSelectie()
    TxtStart.Text = CStr(TxtSelected.SelectionStart)
    TxtLengte.Text = CStr(TxtSelected.SelectionLength)
  End Sub

  Private Sub hTxtSelected_Leave(sender As System.Object, e As System.EventArgs) Handles TxtSelected.Leave
    sZetSelectie()
  End Sub

  Private Sub hTxtSelected_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles TxtSelected.MouseUp
    sZetSelectie()
  End Sub

  Private Sub hTxtSelected_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles TxtSelected.KeyUp
    sZetSelectie()
  End Sub

  Private Sub sInitGrid()
    DgvAkties.RowCount = mMaxAktie + 1
    DgvAkties.Refresh()
  End Sub

  Private Sub hDgvAkties_CellValueNeeded(sender As Object, e As DataGridViewCellValueEventArgs) Handles DgvAkties.CellValueNeeded
    Dim lRij As Integer

    lRij = e.RowIndex
    Select Case e.ColumnIndex
      Case 0
        e.Value = mAkties(lRij).xAktieTekst
      Case 1
        e.Value = mAkties(lRij).xStart
      Case 2
        e.Value = mAkties(lRij).xLengte
      Case 3
        e.Value = mAkties(lRij).xWaarde
    End Select
  End Sub

  Private Function sHernoem(pNaamIn As String) As String
    Dim lNaamIn As String
    Dim lNaamUit As String

    lNaamIn = pNaamIn
    lNaamUit = lNaamIn
    For Each lAktie In mAkties
      lNaamUit = lAktie.xPasToe(lNaamIn)
      lNaamIn = lNaamUit
    Next

    sHernoem = lNaamUit
  End Function

  Private Sub BttnToepassen_Click(sender As Object, e As EventArgs) Handles BttnToepassen.Click
    Dim lBestanden As String()
    Dim lLengte As Integer
    Dim lTeller As Integer
    Dim lItem As ListViewItem

    If RdoAlles.Checked Then
      lLengte = LstDir.Items.Count - 1
      ReDim lBestanden(lLengte)
      lTeller = 0
      For Each lItem In LstDir.Items
        lBestanden(lTeller) = lItem.Text
        lTeller = lTeller + 1
      Next
    Else
      lLengte = LstDir.SelectedItems.Count - 1
      ReDim lBestanden(lLengte)
      lTeller = 0
      For Each lItem In LstDir.SelectedItems
        lBestanden(lTeller) = lItem.Text
        lTeller = lTeller + 1
      Next
    End If

    For Each lBestand In lBestanden
      sHerNoemBestand(lBestand)
    Next

    sListFiles()
  End Sub

  Private Sub sHerNoemBestand(pNaam As String)
    Dim lNaamUit As String
    Dim lBestandIn As String
    Dim lBestandUit As String
    Dim lBestand As FileInfo

    lNaamUit = sHernoem(pNaam)
    lBestandIn = mPad & pNaam
    lBestandUit = mPad & lNaamUit
    lBestand = New FileInfo(lBestandIn)
    lBestand.MoveTo(lBestandUit)
  End Sub

  Private Sub DgvAkties_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles DgvAkties.RowsRemoved
    Dim lIndex As Integer

    If e.RowCount > 0 Then
      lIndex = e.RowIndex
      For lTeller = e.RowIndex To mAkties.Length - 2
        mAkties(lTeller) = mAkties(lTeller + 1)
      Next
      mMaxAktie = mMaxAktie - 1
      ReDim Preserve mAkties(mMaxAktie)
      TxtSelected.Text = sHernoem(mInput)
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
    DgvAkties.RowCount = 0
    mMaxAktie = -1
    ReDim mAkties(mMaxAktie)
    sInitGrid()
    TxtSelected.Text = sHernoem(mInput)
  End Sub
End Class
