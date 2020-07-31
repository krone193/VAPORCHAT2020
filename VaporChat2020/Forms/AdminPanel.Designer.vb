<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminPanel
  Inherits System.Windows.Forms.Form

  'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Richiesto da Progettazione Windows Form
  Private components As System.ComponentModel.IContainer

  'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
  'Può essere modificata in Progettazione Windows Form.  
  'Non modificarla mediante l'editor del codice.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdminPanel))
        Me.TxtAdminUser = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtAdminCommand = New System.Windows.Forms.TextBox()
        Me.CmdAdminSend = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TxtAdminUser
        '
        Me.TxtAdminUser.BackColor = System.Drawing.Color.LightSeaGreen
        Me.TxtAdminUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAdminUser.ForeColor = System.Drawing.Color.Gold
        Me.TxtAdminUser.Location = New System.Drawing.Point(12, 31)
        Me.TxtAdminUser.Name = "TxtAdminUser"
        Me.TxtAdminUser.Size = New System.Drawing.Size(189, 20)
        Me.TxtAdminUser.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Label1.Location = New System.Drawing.Point(204, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "ユーザー名"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label2.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Yellow
        Me.Label2.Location = New System.Drawing.Point(12, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "コマンド"
        '
        'TxtAdminCommand
        '
        Me.TxtAdminCommand.BackColor = System.Drawing.Color.Orange
        Me.TxtAdminCommand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAdminCommand.ForeColor = System.Drawing.Color.BlueViolet
        Me.TxtAdminCommand.Location = New System.Drawing.Point(74, 9)
        Me.TxtAdminCommand.Name = "TxtAdminCommand"
        Me.TxtAdminCommand.Size = New System.Drawing.Size(189, 20)
        Me.TxtAdminCommand.TabIndex = 2
        '
        'CmdAdminSend
        '
        Me.CmdAdminSend.BackColor = System.Drawing.Color.DarkSlateBlue
        Me.CmdAdminSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CmdAdminSend.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdminSend.ForeColor = System.Drawing.Color.Violet
        Me.CmdAdminSend.Location = New System.Drawing.Point(12, 54)
        Me.CmdAdminSend.Name = "CmdAdminSend"
        Me.CmdAdminSend.Size = New System.Drawing.Size(251, 23)
        Me.CmdAdminSend.TabIndex = 4
        Me.CmdAdminSend.Text = "前方に"
        Me.CmdAdminSend.UseVisualStyleBackColor = False
        '
        'AdminPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(275, 82)
        Me.Controls.Add(Me.CmdAdminSend)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtAdminCommand)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtAdminUser)
        Me.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "AdminPanel"
        Me.Text = "管理パネル"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtAdminUser As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtAdminCommand As TextBox
    Friend WithEvents CmdAdminSend As Button
End Class
