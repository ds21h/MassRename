Imports System.IO

Public Class FrmHoofdscherm
  Private Const cOK As Integer = 0
  Private Const cFout As Integer = 10

  Private mAkties(-1) As Aktie
  Private mMaxAktie As Integer = -1

  Private mPad As String = ""

  Private mInput As String = ""

  Public Sub New()
    InitializeComponent()
    sVulDrives()
  End Sub

  Private Sub sVulDrives()
    Dim lStations() As DriveInfo
    Dim lDriveNaam As String
    Dim lDriveTeller As Integer
    Dim lDriveNode As TreeNode

    TreeDir.BeginUpdate()
    TreeDir.Nodes.Clear()
    lDriveTeller = 0
    lStations = DriveInfo.GetDrives
    For Each lStation In lStations
      lDriveNaam = lStation.Name
      If Mid(lDriveNaam, Len(lDriveNaam), 1) = "\" Then
        lDriveNaam = Mid(lDriveNaam, 1, Len(lDriveNaam) - 1)
      End If
      If lStation.IsReady Then
        lDriveNaam = "(" & lStation.VolumeLabel & ") " & lDriveNaam
      End If

      lDriveNode = New TreeNode
      lDriveNode.Text = lDriveNaam
      TreeDir.Nodes.Add(lDriveNode)
      If lStation.IsReady Then
        TreeDir.Nodes(lDriveTeller).Nodes.Add("x")
      End If
      lDriveTeller = lDriveTeller + 1
    Next lStation
    TreeDir.EndUpdate()
  End Sub

  Private Sub sVulSubDir(ByVal pPad As String, ByVal pKnoopNode As TreeNode)
    Dim lSubDirs() As String
    Dim lDirTeller As Integer

    lSubDirs = sHaalSubDirs(pPad)
    lDirTeller = 0
    TreeDir.BeginUpdate()
    pKnoopNode.Nodes.Clear()
    For Each lSubDir In lSubDirs
      pKnoopNode.Nodes.Add(lSubDir)
      pKnoopNode.Nodes(lDirTeller).Nodes.Add("x")
      lDirTeller = lDirTeller + 1
    Next
    TreeDir.EndUpdate()
  End Sub

  Private Sub hTreeDir_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeDir.BeforeExpand
    Dim lSelNode As TreeNode
    Dim lPad As String

    lSelNode = e.Node
    lPad = sMaakPad(lSelNode)
    sVulSubDir(lPad, lSelNode)
  End Sub

  Private Function sMaakPad(ByVal pInNode As TreeNode) As String
    Dim lPad As String
    Dim lPosTeller As Integer
    Dim lStartPos As Integer
    Dim lRestLengte As Integer

    lPad = pInNode.FullPath
    If Mid(lPad, 1, 1) = "(" Then
      For lPosTeller = 2 To Len(lPad)
        If Mid(lPad, lPosTeller, 1) = ")" Then
          lStartPos = lPosTeller + 2
          lRestLengte = (Len(lPad) - lStartPos) + 1
          lPad = Mid(lPad, lStartPos, lRestLengte)
          Exit For
        End If
      Next lPosTeller
    End If
    lPad = lPad & "\"
    Return lPad
  End Function

  Private Function sHaalSubDirs(ByVal pPad As String) As String()
    Dim lSubDirs() As String
    Dim lHoofdBieb As DirectoryInfo
    Dim lBiebs() As DirectoryInfo
    Dim lAantDir As Integer
    Dim lDirTeller As Integer

    lDirTeller = 0
    lHoofdBieb = New DirectoryInfo(pPad)
    Try
      lBiebs = lHoofdBieb.GetDirectories
      lAantDir = lBiebs.GetLength(0)
      ReDim lSubDirs(lAantDir - 1)
      For Each lBieb In lBiebs
        lSubDirs(lDirTeller) = lBieb.Name
        lDirTeller = lDirTeller + 1
      Next
    Catch ex As Exception
      ReDim lSubDirs(-1)
    End Try
    Return lSubDirs
  End Function

  Private Sub hTreeDir_BeforeSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeDir.BeforeSelect
    Dim lSelNode As TreeNode

    lSelNode = e.Node
    mPad = sMaakPad(lSelNode)
    sVulInhoud()
  End Sub

  Private Sub sVulInhoud()
    Dim lHoofdBieb As DirectoryInfo
    Dim lBestanden() As FileInfo
    Dim lRegel As ListViewItem

    lHoofdBieb = New DirectoryInfo(mPad)
    DirInhoud.BeginUpdate()
    DirInhoud.Items.Clear()
    Try
      lBestanden = lHoofdBieb.GetFiles
      For Each lBestand In lBestanden
        lRegel = New ListViewItem
        lRegel.Text = lBestand.Name
        'lRegel.Text = Convert.ToString(lBestand.Name)
        DirInhoud.Items.Add(lRegel)
      Next
    Catch ex As Exception
    End Try
    DirInhoud.EndUpdate()
    mInput = ""
    TxtSelected.Text = ""
  End Sub

  Private Sub hDirInhoud_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles DirInhoud.SelectedIndexChanged
    Dim lRegel As ListViewItem

    If DirInhoud.SelectedItems.Count > 0 Then
      lRegel = DirInhoud.SelectedItems(0)
      mInput = lRegel.Text
    Else
      mInput = ""
    End If
    TxtSelected.Text = sHernoem(mInput)
    sZetSelectie()
  End Sub

  Private Sub hBttnInvoegen_Click(sender As System.Object, e As System.EventArgs) Handles BttnInvoegen.Click
    sInvoegen(Aktie.Invoegen)
  End Sub

  Private Sub BttnInvoegen1_Click(sender As Object, e As EventArgs) Handles BttnInvoegen1.Click
    sInvoegen(Aktie.Invoegen1)
  End Sub

  Private Sub sInvoegen(pAktieCode As Integer)
    Dim lPositie As Integer
    Dim lAktie As Aktie

    lPositie = CInt(TxtStart.Text)
    lAktie = New Aktie(pAktieCode, lPositie, TxtInvoegen.Text.Length, TxtInvoegen.Text)
    sNieuweAktie(lAktie)
    TxtSelected.Text = sHernoem(mInput)
  End Sub

  Private Sub hBttnVerwijder_Click(sender As System.Object, e As System.EventArgs) Handles BttnVerwijder.Click
    Dim lPositie As Integer
    Dim lLengte As Integer
    Dim lAktie As Aktie

    lPositie = CInt(TxtStart.Text)
    lLengte = CInt(TxtLengte.Text)
    lAktie = New Aktie(Aktie.Verwijderen, lPositie, lLengte, "")
    sNieuweAktie(lAktie)
    TxtSelected.Text = sHernoem(mInput)
  End Sub

  Private Sub BttnVerhoog_Click(sender As Object, e As EventArgs) Handles BttnVerhoog.Click
    Dim lPositie As Integer
    Dim lLengte As Integer
    Dim lAktie As Aktie

    lPositie = CInt(TxtStart.Text)
    lLengte = CInt(TxtLengte.Text)
    lAktie = New Aktie(Aktie.Verhoog, lPositie, lLengte, "")
    sNieuweAktie(lAktie)
    TxtSelected.Text = sHernoem(mInput)
  End Sub

  Private Sub BttnVerlaag_Click(sender As Object, e As EventArgs) Handles BttnVerlaag.Click
    Dim lPositie As Integer
    Dim lLengte As Integer
    Dim lAktie As Aktie

    lPositie = CInt(TxtStart.Text)
    lLengte = CInt(TxtLengte.Text)
    lAktie = New Aktie(Aktie.Verlaag, lPositie, lLengte, "")
    sNieuweAktie(lAktie)
    TxtSelected.Text = sHernoem(mInput)
  End Sub

  Private Function sNieuweAktie(pAktie As Aktie) As Integer
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
      lLengte = DirInhoud.Items.Count - 1
      ReDim lBestanden(lLengte)
      lTeller = 0
      For Each lItem In DirInhoud.Items
        lBestanden(lTeller) = lItem.text
        lTeller = lTeller + 1
      Next
    Else
      lLengte = DirInhoud.SelectedItems.Count - 1
      ReDim lBestanden(lLengte)
      lTeller = 0
      For Each lItem In DirInhoud.SelectedItems
        lBestanden(lTeller) = lItem.text
        lTeller = lTeller + 1
      Next
    End If

    For Each lBestand In lBestanden
      sHerNoemBestand(lBestand)
    Next

    sVulInhoud()
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
