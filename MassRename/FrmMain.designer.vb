<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMain
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()>
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing AndAlso components IsNot Nothing Then
      components.Dispose()
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()>
  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
    Me.SplInhoud = New System.Windows.Forms.SplitContainer()
    Me.TreeDir = New System.Windows.Forms.TreeView()
    Me.LstDir = New System.Windows.Forms.ListView()
    Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.TxtSelected = New System.Windows.Forms.TextBox()
    Me.BttnInsert = New System.Windows.Forms.Button()
    Me.TxtInsert = New System.Windows.Forms.TextBox()
    Me.LbStart = New System.Windows.Forms.Label()
    Me.TxtStart = New System.Windows.Forms.TextBox()
    Me.LbLengte = New System.Windows.Forms.Label()
    Me.TxtLength = New System.Windows.Forms.TextBox()
    Me.BttnDelete = New System.Windows.Forms.Button()
    Me.DgvActions = New System.Windows.Forms.DataGridView()
    Me.ClmAktie = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.ClmStart = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.ClmLengte = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.ClmWaarde = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.BttnApply = New System.Windows.Forms.Button()
    Me.GrpToepassen = New System.Windows.Forms.GroupBox()
    Me.RdoSel = New System.Windows.Forms.RadioButton()
    Me.RdoAll = New System.Windows.Forms.RadioButton()
    Me.BtnInit = New System.Windows.Forms.Button()
    Me.TmrRefresh = New System.Windows.Forms.Timer(Me.components)
    CType(Me.SplInhoud, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SplInhoud.Panel1.SuspendLayout()
    Me.SplInhoud.Panel2.SuspendLayout()
    Me.SplInhoud.SuspendLayout()
    CType(Me.DgvActions, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GrpToepassen.SuspendLayout()
    Me.SuspendLayout()
    '
    'SplInhoud
    '
    resources.ApplyResources(Me.SplInhoud, "SplInhoud")
    Me.SplInhoud.Name = "SplInhoud"
    '
    'SplInhoud.Panel1
    '
    Me.SplInhoud.Panel1.Controls.Add(Me.TreeDir)
    '
    'SplInhoud.Panel2
    '
    Me.SplInhoud.Panel2.Controls.Add(Me.LstDir)
    '
    'TreeDir
    '
    resources.ApplyResources(Me.TreeDir, "TreeDir")
    Me.TreeDir.Name = "TreeDir"
    '
    'LstDir
    '
    Me.LstDir.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
    resources.ApplyResources(Me.LstDir, "LstDir")
    Me.LstDir.FullRowSelect = True
    Me.LstDir.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
    Me.LstDir.HideSelection = False
    Me.LstDir.Name = "LstDir"
    Me.LstDir.UseCompatibleStateImageBehavior = False
    Me.LstDir.View = System.Windows.Forms.View.Details
    '
    'ColumnHeader1
    '
    resources.ApplyResources(Me.ColumnHeader1, "ColumnHeader1")
    '
    'TxtSelected
    '
    resources.ApplyResources(Me.TxtSelected, "TxtSelected")
    Me.TxtSelected.Name = "TxtSelected"
    '
    'BttnInsert
    '
    resources.ApplyResources(Me.BttnInsert, "BttnInsert")
    Me.BttnInsert.Name = "BttnInsert"
    Me.BttnInsert.UseVisualStyleBackColor = True
    '
    'TxtInsert
    '
    resources.ApplyResources(Me.TxtInsert, "TxtInsert")
    Me.TxtInsert.Name = "TxtInsert"
    '
    'LbStart
    '
    resources.ApplyResources(Me.LbStart, "LbStart")
    Me.LbStart.Name = "LbStart"
    '
    'TxtStart
    '
    resources.ApplyResources(Me.TxtStart, "TxtStart")
    Me.TxtStart.Name = "TxtStart"
    '
    'LbLengte
    '
    resources.ApplyResources(Me.LbLengte, "LbLengte")
    Me.LbLengte.Name = "LbLengte"
    '
    'TxtLength
    '
    resources.ApplyResources(Me.TxtLength, "TxtLength")
    Me.TxtLength.Name = "TxtLength"
    '
    'BttnDelete
    '
    resources.ApplyResources(Me.BttnDelete, "BttnDelete")
    Me.BttnDelete.Name = "BttnDelete"
    Me.BttnDelete.UseVisualStyleBackColor = True
    '
    'DgvActions
    '
    Me.DgvActions.AllowUserToAddRows = False
    Me.DgvActions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DgvActions.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ClmAktie, Me.ClmStart, Me.ClmLengte, Me.ClmWaarde})
    resources.ApplyResources(Me.DgvActions, "DgvActions")
    Me.DgvActions.MultiSelect = False
    Me.DgvActions.Name = "DgvActions"
    Me.DgvActions.ReadOnly = True
    Me.DgvActions.VirtualMode = True
    '
    'ClmAktie
    '
    resources.ApplyResources(Me.ClmAktie, "ClmAktie")
    Me.ClmAktie.Name = "ClmAktie"
    Me.ClmAktie.ReadOnly = True
    '
    'ClmStart
    '
    resources.ApplyResources(Me.ClmStart, "ClmStart")
    Me.ClmStart.Name = "ClmStart"
    Me.ClmStart.ReadOnly = True
    '
    'ClmLengte
    '
    resources.ApplyResources(Me.ClmLengte, "ClmLengte")
    Me.ClmLengte.Name = "ClmLengte"
    Me.ClmLengte.ReadOnly = True
    '
    'ClmWaarde
    '
    resources.ApplyResources(Me.ClmWaarde, "ClmWaarde")
    Me.ClmWaarde.Name = "ClmWaarde"
    Me.ClmWaarde.ReadOnly = True
    '
    'BttnApply
    '
    resources.ApplyResources(Me.BttnApply, "BttnApply")
    Me.BttnApply.Name = "BttnApply"
    Me.BttnApply.UseVisualStyleBackColor = True
    '
    'GrpToepassen
    '
    Me.GrpToepassen.Controls.Add(Me.RdoSel)
    Me.GrpToepassen.Controls.Add(Me.RdoAll)
    Me.GrpToepassen.Controls.Add(Me.BttnApply)
    resources.ApplyResources(Me.GrpToepassen, "GrpToepassen")
    Me.GrpToepassen.Name = "GrpToepassen"
    Me.GrpToepassen.TabStop = False
    '
    'RdoSel
    '
    resources.ApplyResources(Me.RdoSel, "RdoSel")
    Me.RdoSel.Checked = True
    Me.RdoSel.Name = "RdoSel"
    Me.RdoSel.TabStop = True
    Me.RdoSel.UseVisualStyleBackColor = True
    '
    'RdoAll
    '
    resources.ApplyResources(Me.RdoAll, "RdoAll")
    Me.RdoAll.Name = "RdoAll"
    Me.RdoAll.UseVisualStyleBackColor = True
    '
    'BtnInit
    '
    resources.ApplyResources(Me.BtnInit, "BtnInit")
    Me.BtnInit.Name = "BtnInit"
    Me.BtnInit.UseVisualStyleBackColor = True
    '
    'TmrRefresh
    '
    Me.TmrRefresh.Interval = 2000
    '
    'FrmMain
    '
    resources.ApplyResources(Me, "$this")
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.SplInhoud)
    Me.Controls.Add(Me.BtnInit)
    Me.Controls.Add(Me.GrpToepassen)
    Me.Controls.Add(Me.DgvActions)
    Me.Controls.Add(Me.BttnDelete)
    Me.Controls.Add(Me.TxtLength)
    Me.Controls.Add(Me.LbLengte)
    Me.Controls.Add(Me.TxtStart)
    Me.Controls.Add(Me.LbStart)
    Me.Controls.Add(Me.TxtInsert)
    Me.Controls.Add(Me.BttnInsert)
    Me.Controls.Add(Me.TxtSelected)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FrmMain"
    Me.ShowIcon = False
    Me.SplInhoud.Panel1.ResumeLayout(False)
    Me.SplInhoud.Panel2.ResumeLayout(False)
    CType(Me.SplInhoud, System.ComponentModel.ISupportInitialize).EndInit()
    Me.SplInhoud.ResumeLayout(False)
    CType(Me.DgvActions, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GrpToepassen.ResumeLayout(False)
    Me.GrpToepassen.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TreeDir As System.Windows.Forms.TreeView
  Friend WithEvents LstDir As System.Windows.Forms.ListView
  Friend WithEvents TxtSelected As System.Windows.Forms.TextBox
  Friend WithEvents BttnInsert As System.Windows.Forms.Button
  Friend WithEvents TxtInsert As System.Windows.Forms.TextBox
  Friend WithEvents LbStart As System.Windows.Forms.Label
  Friend WithEvents TxtStart As System.Windows.Forms.TextBox
  Friend WithEvents LbLengte As System.Windows.Forms.Label
  Friend WithEvents TxtLength As System.Windows.Forms.TextBox
  Friend WithEvents BttnDelete As System.Windows.Forms.Button
  Friend WithEvents DgvActions As System.Windows.Forms.DataGridView
  Friend WithEvents BttnApply As System.Windows.Forms.Button
  Friend WithEvents GrpToepassen As System.Windows.Forms.GroupBox
  Friend WithEvents RdoSel As System.Windows.Forms.RadioButton
  Friend WithEvents RdoAll As System.Windows.Forms.RadioButton
  Friend WithEvents BtnInit As System.Windows.Forms.Button
  Friend WithEvents SplInhoud As System.Windows.Forms.SplitContainer
  Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ClmAktie As Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents ClmStart As Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents ClmLengte As Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents ClmWaarde As Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents TmrRefresh As Windows.Forms.Timer
End Class
