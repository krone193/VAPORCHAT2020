<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StartScreen
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()>
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()>
  Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StartScreen))
		Me.BtnHide = New System.Windows.Forms.Button()
		Me.BtnVapor = New System.Windows.Forms.Button()
		Me.Lblkronelab = New System.Windows.Forms.Label()
		Me.lblVaporChat2020Ver = New System.Windows.Forms.Label()
		Me.TxtPassword = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.CmbCloserTime = New System.Windows.Forms.ComboBox()
		Me.SuspendLayout()
		'
		'BtnHide
		'
		Me.BtnHide.Location = New System.Drawing.Point(12, 12)
		Me.BtnHide.Name = "BtnHide"
		Me.BtnHide.Size = New System.Drawing.Size(427, 23)
		Me.BtnHide.TabIndex = 3
		Me.BtnHide.Text = "Ｈｉｄｅ　ｉｎ　ｐｌａｉｎ　ｓｉｇｈｔ　イネ苛ィ"
		Me.BtnHide.UseVisualStyleBackColor = True
		'
		'BtnVapor
		'
		Me.BtnVapor.BackColor = System.Drawing.SystemColors.ActiveCaptionText
		Me.BtnVapor.BackgroundImage = CType(resources.GetObject("BtnVapor.BackgroundImage"), System.Drawing.Image)
		Me.BtnVapor.ForeColor = System.Drawing.Color.HotPink
		Me.BtnVapor.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.BtnVapor.Location = New System.Drawing.Point(12, 41)
		Me.BtnVapor.Name = "BtnVapor"
		Me.BtnVapor.Size = New System.Drawing.Size(427, 23)
		Me.BtnVapor.TabIndex = 2
		Me.BtnVapor.Text = "Ｖ　Ａ　Ｐ　Ｏ　Ｒ　奥ケせふマ"
		Me.BtnVapor.UseVisualStyleBackColor = False
		'
		'Lblkronelab
		'
		Me.Lblkronelab.AutoSize = True
		Me.Lblkronelab.BackColor = System.Drawing.Color.Transparent
		Me.Lblkronelab.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Lblkronelab.Location = New System.Drawing.Point(398, 331)
		Me.Lblkronelab.Name = "Lblkronelab"
		Me.Lblkronelab.Size = New System.Drawing.Size(53, 12)
		Me.Lblkronelab.TabIndex = 4
		Me.Lblkronelab.Text = "ƙ ཞ ơ ŋ ɛ Ɩ ą ც"
		'
		'lblVaporChat2020Ver
		'
		Me.lblVaporChat2020Ver.AutoSize = True
		Me.lblVaporChat2020Ver.BackColor = System.Drawing.Color.Transparent
		Me.lblVaporChat2020Ver.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblVaporChat2020Ver.ForeColor = System.Drawing.Color.Cyan
		Me.lblVaporChat2020Ver.Location = New System.Drawing.Point(362, 67)
		Me.lblVaporChat2020Ver.Name = "lblVaporChat2020Ver"
		Me.lblVaporChat2020Ver.Size = New System.Drawing.Size(14, 15)
		Me.lblVaporChat2020Ver.TabIndex = 5
		Me.lblVaporChat2020Ver.Text = "-"
		'
		'TxtPassword
		'
		Me.TxtPassword.BackColor = System.Drawing.Color.Pink
		Me.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.TxtPassword.ForeColor = System.Drawing.Color.Crimson
		Me.TxtPassword.Location = New System.Drawing.Point(153, 70)
		Me.TxtPassword.Name = "TxtPassword"
		Me.TxtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(12543)
		Me.TxtPassword.Size = New System.Drawing.Size(141, 13)
		Me.TxtPassword.TabIndex = 1
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.BackColor = System.Drawing.Color.Transparent
		Me.Label1.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.ForeColor = System.Drawing.Color.Teal
		Me.Label1.Location = New System.Drawing.Point(179, 85)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(87, 14)
		Me.Label1.TabIndex = 9
		Me.Label1.Text = "パスワードを挿入"
		'
		'CmbCloserTime
		'
		Me.CmbCloserTime.BackColor = System.Drawing.Color.PaleTurquoise
		Me.CmbCloserTime.DropDownHeight = 100
		Me.CmbCloserTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CmbCloserTime.FlatStyle = System.Windows.Forms.FlatStyle.System
		Me.CmbCloserTime.ForeColor = System.Drawing.Color.Indigo
		Me.CmbCloserTime.FormattingEnabled = True
		Me.CmbCloserTime.IntegralHeight = False
		Me.CmbCloserTime.Items.AddRange(New Object() {"30", "35", "40", "45", "50", "55", "60"})
		Me.CmbCloserTime.Location = New System.Drawing.Point(300, 66)
		Me.CmbCloserTime.Name = "CmbCloserTime"
		Me.CmbCloserTime.Size = New System.Drawing.Size(37, 21)
		Me.CmbCloserTime.TabIndex = 10
		'
		'StartScreen
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.ControlDarkDark
		Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
		Me.ClientSize = New System.Drawing.Size(451, 354)
		Me.Controls.Add(Me.CmbCloserTime)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.TxtPassword)
		Me.Controls.Add(Me.lblVaporChat2020Ver)
		Me.Controls.Add(Me.Lblkronelab)
		Me.Controls.Add(Me.BtnHide)
		Me.Controls.Add(Me.BtnVapor)
		Me.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.KeyPreview = True
		Me.Name = "StartScreen"
		Me.Text = "(っ◔◡◔)っ 【 ﻿Ｖ　Ａ　Ｐ　Ｏ　Ｒ　Ｃ　Ｈ　Ａ　Ｔ 】 (っ◔◡◔)っ"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents BtnHide As Button
  Friend WithEvents BtnVapor As Button
  Friend WithEvents Lblkronelab As Label
  Friend WithEvents lblVaporChat2020Ver As Label
    Friend WithEvents TxtPassword As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents CmbCloserTime As ComboBox
End Class
