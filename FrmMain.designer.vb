<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmHoofdscherm
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
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
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
    Me.TreeDir = New System.Windows.Forms.TreeView()
    Me.DirInhoud = New System.Windows.Forms.ListView()
    Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.TxtSelected = New System.Windows.Forms.TextBox()
    Me.BttnInvoegen = New System.Windows.Forms.Button()
    Me.TxtInvoegen = New System.Windows.Forms.TextBox()
    Me.LbStart = New System.Windows.Forms.Label()
    Me.TxtStart = New System.Windows.Forms.TextBox()
    Me.LbLengte = New System.Windows.Forms.Label()
    Me.TxtLengte = New System.Windows.Forms.TextBox()
    Me.BttnVerwijder = New System.Windows.Forms.Button()
    Me.DgvAkties = New System.Windows.Forms.DataGridView()
    Me.ClmAktie = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.ClmStart = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.ClmLengte = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.ClmWaarde = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.BttnToepassen = New System.Windows.Forms.Button()
    Me.GrpToepassen = New System.Windows.Forms.GroupBox()
    Me.RdoSel = New System.Windows.Forms.RadioButton()
    Me.RdoAlles = New System.Windows.Forms.RadioButton()
    Me.BtnInit = New System.Windows.Forms.Button()
    Me.SplInhoud = New System.Windows.Forms.SplitContainer()
    Me.BttnInvoegen1 = New System.Windows.Forms.Button()
    Me.BttnVerhoog = New System.Windows.Forms.Button()
    Me.BttnVerlaag = New System.Windows.Forms.Button()
    CType(Me.DgvAkties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GrpToepassen.SuspendLayout()
    CType(Me.SplInhoud, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SplInhoud.Panel1.SuspendLayout()
    Me.SplInhoud.Panel2.SuspendLayout()
    Me.SplInhoud.SuspendLayout()
    Me.SuspendLayout()
    '
    'TreeDir
    '
    Me.TreeDir.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TreeDir.Location = New System.Drawing.Point(0, 0)
    Me.TreeDir.Name = "TreeDir"
    Me.TreeDir.Size = New System.Drawing.Size(192, 201)
    Me.TreeDir.TabIndex = 0
    '
    'DirInhoud
    '
    Me.DirInhoud.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
    Me.DirInhoud.Dock = System.Windows.Forms.DockStyle.Fill
    Me.DirInhoud.FullRowSelect = True
    Me.DirInhoud.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
    Me.DirInhoud.HideSelection = False
    Me.DirInhoud.Location = New System.Drawing.Point(0, 0)
    Me.DirInhoud.Name = "DirInhoud"
    Me.DirInhoud.Size = New System.Drawing.Size(192, 322)
    Me.DirInhoud.TabIndex = 0
    Me.DirInhoud.UseCompatibleStateImageBehavior = False
    Me.DirInhoud.View = System.Windows.Forms.View.Details
    '
    'ColumnHeader1
    '
    Me.ColumnHeader1.Width = 256
    '
    'TxtSelected
    '
    Me.TxtSelected.Location = New System.Drawing.Point(214, 12)
    Me.TxtSelected.Name = "TxtSelected"
    Me.TxtSelected.Size = New System.Drawing.Size(381, 20)
    Me.TxtSelected.TabIndex = 1
    '
    'BttnInvoegen
    '
    Me.BttnInvoegen.Location = New System.Drawing.Point(520, 38)
    Me.BttnInvoegen.Name = "BttnInvoegen"
    Me.BttnInvoegen.Size = New System.Drawing.Size(75, 23)
    Me.BttnInvoegen.TabIndex = 2
    Me.BttnInvoegen.Text = "Invoegen"
    Me.BttnInvoegen.UseVisualStyleBackColor = True
    '
    'TxtInvoegen
    '
    Me.TxtInvoegen.Location = New System.Drawing.Point(414, 40)
    Me.TxtInvoegen.Name = "TxtInvoegen"
    Me.TxtInvoegen.Size = New System.Drawing.Size(100, 20)
    Me.TxtInvoegen.TabIndex = 3
    '
    'LbStart
    '
    Me.LbStart.AutoSize = True
    Me.LbStart.Location = New System.Drawing.Point(211, 43)
    Me.LbStart.Name = "LbStart"
    Me.LbStart.Size = New System.Drawing.Size(29, 13)
    Me.LbStart.TabIndex = 4
    Me.LbStart.Text = "Start"
    '
    'TxtStart
    '
    Me.TxtStart.Location = New System.Drawing.Point(257, 40)
    Me.TxtStart.Name = "TxtStart"
    Me.TxtStart.Size = New System.Drawing.Size(61, 20)
    Me.TxtStart.TabIndex = 5
    '
    'LbLengte
    '
    Me.LbLengte.AutoSize = True
    Me.LbLengte.Location = New System.Drawing.Point(211, 72)
    Me.LbLengte.Name = "LbLengte"
    Me.LbLengte.Size = New System.Drawing.Size(40, 13)
    Me.LbLengte.TabIndex = 6
    Me.LbLengte.Text = "Lengte"
    '
    'TxtLengte
    '
    Me.TxtLengte.Location = New System.Drawing.Point(257, 69)
    Me.TxtLengte.Name = "TxtLengte"
    Me.TxtLengte.Size = New System.Drawing.Size(61, 20)
    Me.TxtLengte.TabIndex = 7
    '
    'BttnVerwijder
    '
    Me.BttnVerwijder.Location = New System.Drawing.Point(520, 89)
    Me.BttnVerwijder.Name = "BttnVerwijder"
    Me.BttnVerwijder.Size = New System.Drawing.Size(75, 23)
    Me.BttnVerwijder.TabIndex = 8
    Me.BttnVerwijder.Text = "Verwijderen"
    Me.BttnVerwijder.UseVisualStyleBackColor = True
    '
    'DgvAkties
    '
    Me.DgvAkties.AllowUserToAddRows = False
    Me.DgvAkties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DgvAkties.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ClmAktie, Me.ClmStart, Me.ClmLengte, Me.ClmWaarde})
    Me.DgvAkties.Location = New System.Drawing.Point(214, 199)
    Me.DgvAkties.MultiSelect = False
    Me.DgvAkties.Name = "DgvAkties"
    Me.DgvAkties.ReadOnly = True
    Me.DgvAkties.Size = New System.Drawing.Size(381, 185)
    Me.DgvAkties.TabIndex = 9
    Me.DgvAkties.VirtualMode = True
    '
    'ClmAktie
    '
    Me.ClmAktie.HeaderText = "Aktie"
    Me.ClmAktie.Name = "ClmAktie"
    Me.ClmAktie.ReadOnly = True
    Me.ClmAktie.Width = 80
    '
    'ClmStart
    '
    Me.ClmStart.HeaderText = "Start"
    Me.ClmStart.Name = "ClmStart"
    Me.ClmStart.ReadOnly = True
    Me.ClmStart.Width = 50
    '
    'ClmLengte
    '
    Me.ClmLengte.HeaderText = "Lengte"
    Me.ClmLengte.Name = "ClmLengte"
    Me.ClmLengte.ReadOnly = True
    Me.ClmLengte.Width = 50
    '
    'ClmWaarde
    '
    Me.ClmWaarde.HeaderText = "Waarde"
    Me.ClmWaarde.Name = "ClmWaarde"
    Me.ClmWaarde.ReadOnly = True
    Me.ClmWaarde.Width = 158
    '
    'BttnToepassen
    '
    Me.BttnToepassen.Location = New System.Drawing.Point(6, 19)
    Me.BttnToepassen.Name = "BttnToepassen"
    Me.BttnToepassen.Size = New System.Drawing.Size(75, 40)
    Me.BttnToepassen.TabIndex = 10
    Me.BttnToepassen.Text = "Toepassen"
    Me.BttnToepassen.UseVisualStyleBackColor = True
    '
    'GrpToepassen
    '
    Me.GrpToepassen.Controls.Add(Me.RdoSel)
    Me.GrpToepassen.Controls.Add(Me.RdoAlles)
    Me.GrpToepassen.Controls.Add(Me.BttnToepassen)
    Me.GrpToepassen.Location = New System.Drawing.Point(214, 446)
    Me.GrpToepassen.Name = "GrpToepassen"
    Me.GrpToepassen.Size = New System.Drawing.Size(181, 73)
    Me.GrpToepassen.TabIndex = 11
    Me.GrpToepassen.TabStop = False
    '
    'RdoSel
    '
    Me.RdoSel.AutoSize = True
    Me.RdoSel.Checked = True
    Me.RdoSel.Location = New System.Drawing.Point(87, 42)
    Me.RdoSel.Name = "RdoSel"
    Me.RdoSel.Size = New System.Drawing.Size(88, 17)
    Me.RdoSel.TabIndex = 12
    Me.RdoSel.TabStop = True
    Me.RdoSel.Text = "Geselecteerd"
    Me.RdoSel.UseVisualStyleBackColor = True
    '
    'RdoAlles
    '
    Me.RdoAlles.AutoSize = True
    Me.RdoAlles.Location = New System.Drawing.Point(87, 19)
    Me.RdoAlles.Name = "RdoAlles"
    Me.RdoAlles.Size = New System.Drawing.Size(47, 17)
    Me.RdoAlles.TabIndex = 11
    Me.RdoAlles.Text = "Alles"
    Me.RdoAlles.UseVisualStyleBackColor = True
    '
    'BtnInit
    '
    Me.BtnInit.Location = New System.Drawing.Point(214, 383)
    Me.BtnInit.Name = "BtnInit"
    Me.BtnInit.Size = New System.Drawing.Size(381, 23)
    Me.BtnInit.TabIndex = 12
    Me.BtnInit.Text = "Alle akties verwijderen"
    Me.BtnInit.UseVisualStyleBackColor = True
    '
    'SplInhoud
    '
    Me.SplInhoud.Dock = System.Windows.Forms.DockStyle.Left
    Me.SplInhoud.Location = New System.Drawing.Point(0, 0)
    Me.SplInhoud.Name = "SplInhoud"
    Me.SplInhoud.Orientation = System.Windows.Forms.Orientation.Horizontal
    '
    'SplInhoud.Panel1
    '
    Me.SplInhoud.Panel1.Controls.Add(Me.TreeDir)
    '
    'SplInhoud.Panel2
    '
    Me.SplInhoud.Panel2.Controls.Add(Me.DirInhoud)
    Me.SplInhoud.Size = New System.Drawing.Size(192, 527)
    Me.SplInhoud.SplitterDistance = 201
    Me.SplInhoud.TabIndex = 13
    '
    'BttnInvoegen1
    '
    Me.BttnInvoegen1.Location = New System.Drawing.Point(520, 63)
    Me.BttnInvoegen1.Name = "BttnInvoegen1"
    Me.BttnInvoegen1.Size = New System.Drawing.Size(75, 23)
    Me.BttnInvoegen1.TabIndex = 14
    Me.BttnInvoegen1.Text = "Invoegen(n)"
    Me.BttnInvoegen1.UseVisualStyleBackColor = True
    '
    'BttnVerhoog
    '
    Me.BttnVerhoog.Location = New System.Drawing.Point(520, 128)
    Me.BttnVerhoog.Name = "BttnVerhoog"
    Me.BttnVerhoog.Size = New System.Drawing.Size(75, 23)
    Me.BttnVerhoog.TabIndex = 15
    Me.BttnVerhoog.Text = "Verhoog(+1)"
    Me.BttnVerhoog.UseVisualStyleBackColor = True
    '
    'BttnVerlaag
    '
    Me.BttnVerlaag.Location = New System.Drawing.Point(520, 157)
    Me.BttnVerlaag.Name = "BttnVerlaag"
    Me.BttnVerlaag.Size = New System.Drawing.Size(75, 23)
    Me.BttnVerlaag.TabIndex = 16
    Me.BttnVerlaag.Text = "Verlaag(-1)"
    Me.BttnVerlaag.UseVisualStyleBackColor = True
    '
    'FrmHoofdscherm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(610, 527)
    Me.Controls.Add(Me.BttnVerlaag)
    Me.Controls.Add(Me.BttnVerhoog)
    Me.Controls.Add(Me.BttnInvoegen1)
    Me.Controls.Add(Me.SplInhoud)
    Me.Controls.Add(Me.BtnInit)
    Me.Controls.Add(Me.GrpToepassen)
    Me.Controls.Add(Me.DgvAkties)
    Me.Controls.Add(Me.BttnVerwijder)
    Me.Controls.Add(Me.TxtLengte)
    Me.Controls.Add(Me.LbLengte)
    Me.Controls.Add(Me.TxtStart)
    Me.Controls.Add(Me.LbStart)
    Me.Controls.Add(Me.TxtInvoegen)
    Me.Controls.Add(Me.BttnInvoegen)
    Me.Controls.Add(Me.TxtSelected)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FrmHoofdscherm"
    Me.ShowIcon = False
    Me.Text = "Hernoem"
    CType(Me.DgvAkties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.GrpToepassen.ResumeLayout(False)
    Me.GrpToepassen.PerformLayout()
    Me.SplInhoud.Panel1.ResumeLayout(False)
    Me.SplInhoud.Panel2.ResumeLayout(False)
    CType(Me.SplInhoud, System.ComponentModel.ISupportInitialize).EndInit()
    Me.SplInhoud.ResumeLayout(False)
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents TreeDir As System.Windows.Forms.TreeView
	Friend WithEvents DirInhoud As System.Windows.Forms.ListView
	Friend WithEvents TxtSelected As System.Windows.Forms.TextBox
	Friend WithEvents BttnInvoegen As System.Windows.Forms.Button
	Friend WithEvents TxtInvoegen As System.Windows.Forms.TextBox
	Friend WithEvents LbStart As System.Windows.Forms.Label
	Friend WithEvents TxtStart As System.Windows.Forms.TextBox
	Friend WithEvents LbLengte As System.Windows.Forms.Label
	Friend WithEvents TxtLengte As System.Windows.Forms.TextBox
	Friend WithEvents BttnVerwijder As System.Windows.Forms.Button
	Friend WithEvents DgvAkties As System.Windows.Forms.DataGridView
	Friend WithEvents BttnToepassen As System.Windows.Forms.Button
	Friend WithEvents GrpToepassen As System.Windows.Forms.GroupBox
	Friend WithEvents RdoSel As System.Windows.Forms.RadioButton
	Friend WithEvents RdoAlles As System.Windows.Forms.RadioButton
	Friend WithEvents BtnInit As System.Windows.Forms.Button
	Friend WithEvents SplInhoud As System.Windows.Forms.SplitContainer
	Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
	Friend WithEvents ClmAktie As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents ClmStart As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents ClmLengte As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents ClmWaarde As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents BttnInvoegen1 As Button
  Friend WithEvents BttnVerhoog As Button
  Friend WithEvents BttnVerlaag As Button
End Class
