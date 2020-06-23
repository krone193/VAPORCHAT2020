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
		Me.cmdHideInPlainSight = New System.Windows.Forms.Button()
		Me.cmdVapor = New System.Windows.Forms.Button()
		Me.Lblkronelab = New System.Windows.Forms.Label()
		Me.lblVaporChat2020Ver = New System.Windows.Forms.Label()
		Me.lblVaporChatVer = New System.Windows.Forms.Label()
		Me.lblVaporFuncVer = New System.Windows.Forms.Label()
		Me.SuspendLayout()
		'
		'cmdHideInPlainSight
		'
		Me.cmdHideInPlainSight.Location = New System.Drawing.Point(12, 12)
		Me.cmdHideInPlainSight.Name = "cmdHideInPlainSight"
		Me.cmdHideInPlainSight.Size = New System.Drawing.Size(427, 23)
		Me.cmdHideInPlainSight.TabIndex = 3
		Me.cmdHideInPlainSight.Text = "Ｈｉｄｅ　ｉｎ　ｐｌａｉｎ　ｓｉｇｈｔ　イネ苛ィ"
		Me.cmdHideInPlainSight.UseVisualStyleBackColor = True
		'
		'cmdVapor
		'
		Me.cmdVapor.BackColor = System.Drawing.SystemColors.ActiveCaptionText
		Me.cmdVapor.BackgroundImage = CType(resources.GetObject("cmdVapor.BackgroundImage"), System.Drawing.Image)
		Me.cmdVapor.ForeColor = System.Drawing.Color.HotPink
		Me.cmdVapor.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdVapor.Location = New System.Drawing.Point(12, 41)
		Me.cmdVapor.Name = "cmdVapor"
		Me.cmdVapor.Size = New System.Drawing.Size(427, 23)
		Me.cmdVapor.TabIndex = 2
		Me.cmdVapor.Text = "Ｖ　Ａ　Ｐ　Ｏ　Ｒ　奥ケせふマ"
		Me.cmdVapor.UseVisualStyleBackColor = False
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
		Me.lblVaporChat2020Ver.ForeColor = System.Drawing.Color.DarkMagenta
		Me.lblVaporChat2020Ver.Location = New System.Drawing.Point(362, 67)
		Me.lblVaporChat2020Ver.Name = "lblVaporChat2020Ver"
		Me.lblVaporChat2020Ver.Size = New System.Drawing.Size(14, 15)
		Me.lblVaporChat2020Ver.TabIndex = 5
		Me.lblVaporChat2020Ver.Text = "-"
		'
		'lblVaporChatVer
		'
		Me.lblVaporChatVer.AutoSize = True
		Me.lblVaporChatVer.BackColor = System.Drawing.Color.Transparent
		Me.lblVaporChatVer.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblVaporChatVer.ForeColor = System.Drawing.Color.DarkTurquoise
		Me.lblVaporChatVer.Location = New System.Drawing.Point(362, 82)
		Me.lblVaporChatVer.Name = "lblVaporChatVer"
		Me.lblVaporChatVer.Size = New System.Drawing.Size(14, 15)
		Me.lblVaporChatVer.TabIndex = 6
		Me.lblVaporChatVer.Text = "-"
		'
		'lblVaporFuncVer
		'
		Me.lblVaporFuncVer.AutoSize = True
		Me.lblVaporFuncVer.BackColor = System.Drawing.Color.Transparent
		Me.lblVaporFuncVer.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblVaporFuncVer.ForeColor = System.Drawing.Color.Yellow
		Me.lblVaporFuncVer.Location = New System.Drawing.Point(362, 97)
		Me.lblVaporFuncVer.Name = "lblVaporFuncVer"
		Me.lblVaporFuncVer.Size = New System.Drawing.Size(14, 15)
		Me.lblVaporFuncVer.TabIndex = 7
		Me.lblVaporFuncVer.Text = "-"
		'
		'StartScreen
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.ControlDarkDark
		Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
		Me.ClientSize = New System.Drawing.Size(451, 354)
		Me.Controls.Add(Me.lblVaporFuncVer)
		Me.Controls.Add(Me.lblVaporChatVer)
		Me.Controls.Add(Me.lblVaporChat2020Ver)
		Me.Controls.Add(Me.Lblkronelab)
		Me.Controls.Add(Me.cmdHideInPlainSight)
		Me.Controls.Add(Me.cmdVapor)
		Me.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "StartScreen"
		Me.Text = "(っ◔◡◔)っ 【﻿Ｖ　Ａ　Ｐ　Ｏ　Ｒ　Ｃ　Ｈ　Ａ　Ｔ】 (っ◔◡◔)っ"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents cmdHideInPlainSight As Button
  Friend WithEvents cmdVapor As Button
  Friend WithEvents Lblkronelab As Label
  Friend WithEvents lblVaporChat2020Ver As Label
  Friend WithEvents lblVaporChatVer As Label
  Friend WithEvents lblVaporFuncVer As Label
End Class
