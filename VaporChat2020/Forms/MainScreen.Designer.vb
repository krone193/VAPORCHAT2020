<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainScreen
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
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainScreen))
		Me.TimerCheckMsg = New System.Windows.Forms.Timer(Me.components)
		Me.TimerGUI = New System.Windows.Forms.Timer(Me.components)
		Me.TimerPubBlock = New System.Windows.Forms.Timer(Me.components)
		Me.TimerAutoCloser = New System.Windows.Forms.Timer(Me.components)
		Me.PnlVaporChat = New System.Windows.Forms.Panel()
		Me.TabElfa = New System.Windows.Forms.TabPage()
		Me.TabNew = New System.Windows.Forms.TabPage()
		Me.PnlInsertPass = New System.Windows.Forms.Panel()
		Me.TxtInsertPass = New System.Windows.Forms.TextBox()
		Me.PnlStartScreen = New System.Windows.Forms.Panel()
		Me.TxtPassword = New System.Windows.Forms.TextBox()
		Me.CmbCloserTime = New System.Windows.Forms.ComboBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.LblVaporChat2020Ver = New System.Windows.Forms.Label()
		Me.Lblkronelab = New System.Windows.Forms.Label()
		Me.BtnHide = New System.Windows.Forms.Button()
		Me.BtnVapor = New System.Windows.Forms.Button()
		Me.PnlAdmin = New System.Windows.Forms.Panel()
		Me.BtnAdminBackToStart = New System.Windows.Forms.Button()
		Me.BtnAdminSend = New System.Windows.Forms.Button()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.TxtAdminCommand = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.TxtAdminUser = New System.Windows.Forms.TextBox()
		Me.TabDel = New System.Windows.Forms.TabPage()
		Me.TxtLobby = New System.Windows.Forms.TextBox()
		Me.TxtUser = New System.Windows.Forms.TextBox()
		Me.PnlInsertPass.SuspendLayout()
		Me.PnlStartScreen.SuspendLayout()
		Me.PnlAdmin.SuspendLayout()
		Me.SuspendLayout()
		'
		'TimerCheckMsg
		'
		'
		'TimerGUI
		'
		Me.TimerGUI.Enabled = True
		Me.TimerGUI.Interval = 500
		'
		'TimerPubBlock
		'
		Me.TimerPubBlock.Interval = 500
		'
		'TimerAutoCloser
		'
		Me.TimerAutoCloser.Enabled = True
		Me.TimerAutoCloser.Interval = 30000
		'
		'PnlVaporChat
		'
		Me.PnlVaporChat.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.PnlVaporChat.BackColor = System.Drawing.SystemColors.Control
		Me.PnlVaporChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.PnlVaporChat.Location = New System.Drawing.Point(0, 0)
		Me.PnlVaporChat.Name = "PnlVaporChat"
		Me.PnlVaporChat.Size = New System.Drawing.Size(492, 522)
		Me.PnlVaporChat.TabIndex = 63
		'
		'TabElfa
		'
		Me.TabElfa.Location = New System.Drawing.Point(0, 0)
		Me.TabElfa.Name = "TabElfa"
		Me.TabElfa.Size = New System.Drawing.Size(200, 100)
		Me.TabElfa.TabIndex = 0
		'
		'TabNew
		'
		Me.TabNew.Location = New System.Drawing.Point(0, 0)
		Me.TabNew.Name = "TabNew"
		Me.TabNew.Size = New System.Drawing.Size(200, 100)
		Me.TabNew.TabIndex = 0
		'
		'PnlInsertPass
		'
		Me.PnlInsertPass.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.PnlInsertPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.PnlInsertPass.Controls.Add(Me.TxtInsertPass)
		Me.PnlInsertPass.Location = New System.Drawing.Point(0, 0)
		Me.PnlInsertPass.Name = "PnlInsertPass"
		Me.PnlInsertPass.Size = New System.Drawing.Size(492, 74)
		Me.PnlInsertPass.TabIndex = 64
		'
		'TxtInsertPass
		'
		Me.TxtInsertPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtInsertPass.Location = New System.Drawing.Point(11, 13)
		Me.TxtInsertPass.Name = "TxtInsertPass"
		Me.TxtInsertPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
		Me.TxtInsertPass.Size = New System.Drawing.Size(470, 20)
		Me.TxtInsertPass.TabIndex = 0
		'
		'PnlStartScreen
		'
		Me.PnlStartScreen.AllowDrop = True
		Me.PnlStartScreen.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.PnlStartScreen.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(51, Byte), Integer))
		Me.PnlStartScreen.BackgroundImage = Global.VaporChat2020Application.My.Resources.Resources.start_main
		Me.PnlStartScreen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.PnlStartScreen.Controls.Add(Me.TxtUser)
		Me.PnlStartScreen.Controls.Add(Me.TxtLobby)
		Me.PnlStartScreen.Controls.Add(Me.TxtPassword)
		Me.PnlStartScreen.Controls.Add(Me.CmbCloserTime)
		Me.PnlStartScreen.Controls.Add(Me.Label3)
		Me.PnlStartScreen.Controls.Add(Me.LblVaporChat2020Ver)
		Me.PnlStartScreen.Controls.Add(Me.Lblkronelab)
		Me.PnlStartScreen.Controls.Add(Me.BtnHide)
		Me.PnlStartScreen.Controls.Add(Me.BtnVapor)
		Me.PnlStartScreen.Location = New System.Drawing.Point(0, 0)
		Me.PnlStartScreen.Name = "PnlStartScreen"
		Me.PnlStartScreen.Size = New System.Drawing.Size(492, 522)
		Me.PnlStartScreen.TabIndex = 66
		'
		'TxtPassword
		'
		Me.TxtPassword.BackColor = System.Drawing.Color.Gold
		Me.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.TxtPassword.ForeColor = System.Drawing.Color.Crimson
		Me.TxtPassword.Location = New System.Drawing.Point(172, 99)
		Me.TxtPassword.Name = "TxtPassword"
		Me.TxtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(12543)
		Me.TxtPassword.Size = New System.Drawing.Size(141, 13)
		Me.TxtPassword.TabIndex = 1
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
		Me.CmbCloserTime.Location = New System.Drawing.Point(319, 67)
		Me.CmbCloserTime.Name = "CmbCloserTime"
		Me.CmbCloserTime.Size = New System.Drawing.Size(37, 21)
		Me.CmbCloserTime.TabIndex = 17
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.BackColor = System.Drawing.Color.Transparent
		Me.Label3.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.ForeColor = System.Drawing.Color.Teal
		Me.Label3.Location = New System.Drawing.Point(198, 115)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(87, 14)
		Me.Label3.TabIndex = 16
		Me.Label3.Text = "パスワードを挿入"
		'
		'LblVaporChat2020Ver
		'
		Me.LblVaporChat2020Ver.AutoSize = True
		Me.LblVaporChat2020Ver.BackColor = System.Drawing.Color.Transparent
		Me.LblVaporChat2020Ver.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LblVaporChat2020Ver.ForeColor = System.Drawing.Color.HotPink
		Me.LblVaporChat2020Ver.Location = New System.Drawing.Point(376, 201)
		Me.LblVaporChat2020Ver.Name = "LblVaporChat2020Ver"
		Me.LblVaporChat2020Ver.Size = New System.Drawing.Size(14, 15)
		Me.LblVaporChat2020Ver.TabIndex = 15
		Me.LblVaporChat2020Ver.Text = "-"
		'
		'Lblkronelab
		'
		Me.Lblkronelab.AutoSize = True
		Me.Lblkronelab.BackColor = System.Drawing.Color.Transparent
		Me.Lblkronelab.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Lblkronelab.ForeColor = System.Drawing.Color.HotPink
		Me.Lblkronelab.Location = New System.Drawing.Point(368, 238)
		Me.Lblkronelab.Name = "Lblkronelab"
		Me.Lblkronelab.Size = New System.Drawing.Size(53, 12)
		Me.Lblkronelab.TabIndex = 14
		Me.Lblkronelab.Text = "ƙ ཞ ơ ŋ ɛ Ɩ ą ც"
		'
		'BtnHide
		'
		Me.BtnHide.BackColor = System.Drawing.Color.DarkOrange
		Me.BtnHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.BtnHide.Location = New System.Drawing.Point(31, 13)
		Me.BtnHide.Name = "BtnHide"
		Me.BtnHide.Size = New System.Drawing.Size(427, 23)
		Me.BtnHide.TabIndex = 13
		Me.BtnHide.Text = "Ｈｉｄｅ　ｉｎ　ｐｌａｉｎ　ｓｉｇｈｔ　イネ苛ィ"
		Me.BtnHide.UseVisualStyleBackColor = False
		'
		'BtnVapor
		'
		Me.BtnVapor.BackColor = System.Drawing.SystemColors.ActiveCaptionText
		Me.BtnVapor.BackgroundImage = CType(resources.GetObject("BtnVapor.BackgroundImage"), System.Drawing.Image)
		Me.BtnVapor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.BtnVapor.ForeColor = System.Drawing.Color.HotPink
		Me.BtnVapor.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.BtnVapor.Location = New System.Drawing.Point(31, 42)
		Me.BtnVapor.Name = "BtnVapor"
		Me.BtnVapor.Size = New System.Drawing.Size(427, 23)
		Me.BtnVapor.TabIndex = 12
		Me.BtnVapor.Text = "Ｖ　Ａ　Ｐ　Ｏ　Ｒ　奥ケせふマ"
		Me.BtnVapor.UseVisualStyleBackColor = False
		'
		'PnlAdmin
		'
		Me.PnlAdmin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.PnlAdmin.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(51, Byte), Integer))
		Me.PnlAdmin.BackgroundImage = Global.VaporChat2020Application.My.Resources.Resources.ondulvapor
		Me.PnlAdmin.Controls.Add(Me.BtnAdminBackToStart)
		Me.PnlAdmin.Controls.Add(Me.BtnAdminSend)
		Me.PnlAdmin.Controls.Add(Me.Label2)
		Me.PnlAdmin.Controls.Add(Me.TxtAdminCommand)
		Me.PnlAdmin.Controls.Add(Me.Label1)
		Me.PnlAdmin.Controls.Add(Me.TxtAdminUser)
		Me.PnlAdmin.Location = New System.Drawing.Point(0, 0)
		Me.PnlAdmin.Name = "PnlAdmin"
		Me.PnlAdmin.Size = New System.Drawing.Size(492, 94)
		Me.PnlAdmin.TabIndex = 65
		'
		'BtnAdminBackToStart
		'
		Me.BtnAdminBackToStart.BackColor = System.Drawing.Color.HotPink
		Me.BtnAdminBackToStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.BtnAdminBackToStart.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.BtnAdminBackToStart.ForeColor = System.Drawing.Color.Yellow
		Me.BtnAdminBackToStart.Location = New System.Drawing.Point(118, 54)
		Me.BtnAdminBackToStart.Name = "BtnAdminBackToStart"
		Me.BtnAdminBackToStart.Size = New System.Drawing.Size(23, 23)
		Me.BtnAdminBackToStart.TabIndex = 10
		Me.BtnAdminBackToStart.Text = "<"
		Me.BtnAdminBackToStart.UseVisualStyleBackColor = False
		'
		'BtnAdminSend
		'
		Me.BtnAdminSend.BackColor = System.Drawing.Color.DarkSlateBlue
		Me.BtnAdminSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.BtnAdminSend.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.BtnAdminSend.ForeColor = System.Drawing.Color.Violet
		Me.BtnAdminSend.Location = New System.Drawing.Point(145, 54)
		Me.BtnAdminSend.Name = "BtnAdminSend"
		Me.BtnAdminSend.Size = New System.Drawing.Size(224, 23)
		Me.BtnAdminSend.TabIndex = 9
		Me.BtnAdminSend.Text = "前方に"
		Me.BtnAdminSend.UseVisualStyleBackColor = False
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.Label2.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.ForeColor = System.Drawing.Color.Yellow
		Me.Label2.Location = New System.Drawing.Point(118, 10)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(42, 14)
		Me.Label2.TabIndex = 8
		Me.Label2.Text = "コマンド"
		'
		'TxtAdminCommand
		'
		Me.TxtAdminCommand.BackColor = System.Drawing.Color.Orange
		Me.TxtAdminCommand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtAdminCommand.ForeColor = System.Drawing.Color.BlueViolet
		Me.TxtAdminCommand.Location = New System.Drawing.Point(180, 9)
		Me.TxtAdminCommand.Name = "TxtAdminCommand"
		Me.TxtAdminCommand.Size = New System.Drawing.Size(189, 20)
		Me.TxtAdminCommand.TabIndex = 7
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.Label1.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.ForeColor = System.Drawing.SystemColors.GradientActiveCaption
		Me.Label1.Location = New System.Drawing.Point(310, 34)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(59, 14)
		Me.Label1.TabIndex = 6
		Me.Label1.Text = "ユーザー名"
		'
		'TxtAdminUser
		'
		Me.TxtAdminUser.BackColor = System.Drawing.Color.LightSeaGreen
		Me.TxtAdminUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.TxtAdminUser.ForeColor = System.Drawing.Color.Gold
		Me.TxtAdminUser.Location = New System.Drawing.Point(118, 31)
		Me.TxtAdminUser.Name = "TxtAdminUser"
		Me.TxtAdminUser.Size = New System.Drawing.Size(189, 20)
		Me.TxtAdminUser.TabIndex = 5
		'
		'TabDel
		'
		Me.TabDel.Location = New System.Drawing.Point(4, 4)
		Me.TabDel.Name = "TabDel"
		Me.TabDel.Padding = New System.Windows.Forms.Padding(3)
		Me.TabDel.Size = New System.Drawing.Size(478, 523)
		Me.TabDel.TabIndex = 3
		Me.TabDel.Text = "-"
		Me.TabDel.UseVisualStyleBackColor = True
		'
		'TxtLobby
		'
		Me.TxtLobby.BackColor = System.Drawing.Color.Pink
		Me.TxtLobby.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.TxtLobby.ForeColor = System.Drawing.Color.Crimson
		Me.TxtLobby.Location = New System.Drawing.Point(172, 71)
		Me.TxtLobby.Name = "TxtLobby"
		Me.TxtLobby.PasswordChar = Global.Microsoft.VisualBasic.ChrW(12543)
		Me.TxtLobby.Size = New System.Drawing.Size(141, 13)
		Me.TxtLobby.TabIndex = 18
		'
		'TxtUser
		'
		Me.TxtUser.BackColor = System.Drawing.Color.PowderBlue
		Me.TxtUser.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.TxtUser.ForeColor = System.Drawing.Color.Yellow
		Me.TxtUser.Location = New System.Drawing.Point(172, 85)
		Me.TxtUser.Name = "TxtUser"
		Me.TxtUser.PasswordChar = Global.Microsoft.VisualBasic.ChrW(12543)
		Me.TxtUser.Size = New System.Drawing.Size(141, 13)
		Me.TxtUser.TabIndex = 19
		'
		'MainScreen
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.ClientSize = New System.Drawing.Size(494, 521)
		Me.Controls.Add(Me.PnlVaporChat)
		Me.Controls.Add(Me.PnlAdmin)
		Me.Controls.Add(Me.PnlStartScreen)
		Me.Controls.Add(Me.PnlInsertPass)
		Me.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.IsMdiContainer = True
		Me.KeyPreview = True
		Me.Name = "MainScreen"
		Me.ShowInTaskbar = False
		Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
		Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
		Me.Text = "Dummy text"
		Me.PnlInsertPass.ResumeLayout(False)
		Me.PnlInsertPass.PerformLayout()
		Me.PnlStartScreen.ResumeLayout(False)
		Me.PnlStartScreen.PerformLayout()
		Me.PnlAdmin.ResumeLayout(False)
		Me.PnlAdmin.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents TimerCheckMsg As Timer
	Friend WithEvents TimerGUI As Timer
	Friend WithEvents TimerPubBlock As Timer
	Friend WithEvents TimerAutoCloser As Timer
	Friend WithEvents PnlVaporChat As Panel
	Friend WithEvents PnlInsertPass As Panel
	Friend WithEvents TxtInsertPass As TextBox
	Friend WithEvents PnlAdmin As Panel
	Friend WithEvents BtnAdminSend As Button
	Friend WithEvents Label2 As Label
	Friend WithEvents TxtAdminCommand As TextBox
	Friend WithEvents Label1 As Label
	Friend WithEvents TxtAdminUser As TextBox
	Friend WithEvents BtnAdminBackToStart As Button
	Friend WithEvents PnlStartScreen As Panel
	Friend WithEvents CmbCloserTime As ComboBox
	Friend WithEvents Label3 As Label
	Friend WithEvents TxtPassword As TextBox
	Friend WithEvents LblVaporChat2020Ver As Label
	Friend WithEvents Lblkronelab As Label
	Friend WithEvents BtnHide As Button
	Friend WithEvents BtnVapor As Button
	Friend WithEvents TabElfa As TabPage
	Friend WithEvents TabNew As TabPage
	Friend WithEvents VaporChatControl1 As VaporChatControl
	Friend WithEvents TabDel As TabPage
	Friend WithEvents TxtUser As TextBox
	Friend WithEvents TxtLobby As TextBox
End Class
