Public Class VaporFunc
	'--- Class Imports -----------------------------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	ReadOnly VaporChat As New VaporChat
	ReadOnly Callback As New InterfaceVaporFuncGUI


	'--- V A P O R F U N C | Declarations ----------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
#Const VAPORFUNC_SWVER = "1.1.0.0"
#Const SHOW_ITSME_MESSAGE = False


	'--- V A P O R F U N C | ReadOnly --------------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	ReadOnly CONNSEC As UShort = 5
	Public ReadOnly UserList As New List(Of User)
	ReadOnly BannedText As New List(Of String)


	'--- V A P O R F U N C | Struct ----------------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	Structure User
		Dim Name As String
		Dim Color As Color
	End Structure


	'--- V A P O R F U N C | GUI components --------------------------------------------------------------------------------'
	' VaporChat form -------------------------------------------------------------------------------------------------------'
	Private CallerForm As Form
	' VaporChat start screen -----------------------------------------------------------------------------------------------'
	Private PnlStartScreen As Panel
	Private BtnHide As Button
	Private BtnVapor As Button
	Private TxtPassword As TextBox
	Private CmbCloserTime As ComboBox
	Private LblVaporChat2020Ver As Label
	Private Lblkronelab As Label
	' VaporChat main panel -------------------------------------------------------------------------------------------------'
	Private PnlVaporChat As Panel
	Private LstChatVapo As ListView
	Private TxtLobby As TextBox
	Private TxtUser As TextBox
	Private TxtMsg As TextBox
	Private LblLog As Label
	Private LblUsers As Label
	Private BtnSend As Button
	Private BtnBackToStart As Button
	' VaporChat users list -------------------------------------------------------------------------------------------------'
	Private PnlUsersList As Panel
	Private LstUsersList As ListView
	' VaporChat password panel ---------------------------------------------------------------------------------------------'
	Private PnlInsertPass As Panel
	Private TxtInsertPass As TextBox
	' VaporChat admin panel ------------------------------------------------------------------------------------------------'
	Private PnlAdmin As Panel
	Private TxtAdminCommand As TextBox
	Private TxtAdminUser As TextBox
	' VaporChat components -------------------------------------------------------------------------------------------------'
	Private TimerCheckMsg As Timer
	Private TimerGUI As Timer
	Private TimerPubBlock As Timer
	Private TimerAutoCloser As Timer
	Private StsUsersList As StatusStrip


	'--- V A P O R F U N C | Variables -------------------------------------------------------------------------------------'
	' Private --------------------------------------------------------------------------------------------------------------'
	Private HideStatus As Boolean = False
	Private Connected As Boolean = False
	Private TaskBarHid As Boolean = False
	Private SwitchText As String
	Private SwitchOn As Boolean = False
	Private SwitchIndex As Short = -1
#If LIMVIEW Then
	Private NofMessages As UShort = 0
#End If
	Private AsyncOp As Boolean = False
	Private MessageRxOn As Boolean = False
	Private DotIndex As UShort = 0
	' Public ---------------------------------------------------------------------------------------------------------------'
	Public ForcePass As Boolean = False
	Public UsersListOn As Boolean = False


	'--- V A P O R F U N C | Private Functions -----------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub NotifyDoubleClickFunc()
		If HideStatus = True Then
			HideStatus = False
			Callback.ClbVaporFunc_NotifyIconReadFunc()
			ShowFormGest()
			If ForcePass Then
				PnlInsertPass.BringToFront()
				Callback.ClbVaporFunc_RestoreWindowFunc(VaporChat.PASSWIDTH, VaporChat.PASSHEIGH)
				TxtInsertPass.Focus()
			End If
		End If
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub ClearTextBox(ByRef box As TextBox)
		box.Text = ""
		box.Focus()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub AddUserToList(ByVal name As String)
		Dim tmpUser As User
		tmpUser.Name = name
		tmpUser.Color = GetRandomColor()
		UserList.Add(tmpUser)
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub RemoveUserFromList(ByVal user As String)
		Dim index As Short = SearchNameInList(user)
		UserList.RemoveAt(index)
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Function GetRandomColor() As Color
		Dim rand As New Random()
		Dim clrs As Color
		Dim reso As Boolean = False
		If UserList.Count >= VaporChat.NOFCLRS Then
			clrs = VaporChat.ColorPool(rand.Next(0, VaporChat.NOFCLRS - 1))
		Else
			While reso = False
				clrs = VaporChat.ColorPool(rand.Next(0, VaporChat.NOFCLRS - 1))
				If SearchColorInList(clrs) < 0 Then
					reso = True
				End If
			End While
		End If
		Return clrs
	End Function
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Function SearchNameInList(ByVal name As String) As Short
		Dim ret As Short = -1
		Dim idx As UShort = 0
		For Each user In UserList
			If user.Name = name Then
				ret = idx
				Exit For
			End If
			idx += 1
		Next
		Return ret
	End Function
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Function SearchColorInList(ByVal color As Color) As Short
		Dim ret As Short = -1
		Dim idx As UShort = 0
		For Each user In UserList
			If user.Color = color Then
				ret = idx
				Exit For
			End If
			idx += 1
		Next
		Return ret
	End Function
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Function GetNameAtIndex(ByVal index As UShort) As String
		Return UserList(index).Name
	End Function
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Function GetColorAtIndex(ByVal index As UShort) As Color
		If VaporChat.CurrentTheme = VaporChat.Themes.Vapor Then
			Return UserList(index).Color
		Else
			Return SystemColors.WindowText
		End If
	End Function


	'--- V A P O R F U N C | Public Functions ------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub AssignMainFormGUIFunc(ByRef _form As Form)
		CallerForm = _form
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub AssignStartScreenPanelGUIFunc(ByRef _pnlstartscreen As Panel, ByRef _btnhide As Button, ByRef _btnvapor As Button, ByRef _txtlobby As TextBox, ByRef _txtuser As TextBox, ByRef _txtpassword As TextBox, ByRef _cmbclosertime As ComboBox, ByRef _lblvaporchatver As Label, ByRef _lblkronelab As Label)
		PnlStartScreen = _pnlstartscreen
		BtnHide = _btnhide
		BtnVapor = _btnvapor
		TxtLobby = _txtlobby
		TxtUser = _txtuser
		TxtPassword = _txtpassword
		CmbCloserTime = _cmbclosertime
		LblVaporChat2020Ver = _lblvaporchatver
		Lblkronelab = _lblkronelab
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub AssignVaporChatPanelGUIFunc(ByRef _pnlchat As Panel, ByRef _lstchat As ListView, ByRef _txtmsg As TextBox, ByRef _lbllog As Label, ByRef _lbluser As Label, ByRef _btnsend As Button, ByRef _btnbacktostart As Button)
		PnlVaporChat = _pnlchat
		LstChatVapo = _lstchat
		TxtMsg = _txtmsg
		LblLog = _lbllog
		LblUsers = _lbluser
		BtnSend = _btnsend
		BtnBackToStart = _btnbacktostart
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub AssignUserListPanelGUIFunc(ByRef _pnluserslist As Panel, ByRef _lstuserslist As ListView, ByRef _stsuserslist As StatusStrip)
		PnlUsersList = _pnluserslist
		LstUsersList = _lstuserslist
		StsUsersList = _stsuserslist
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub AssignPasswordPanelGUIFunc(ByRef _pnlinsertpass As Panel, ByRef _txtinsertpass As TextBox)
		PnlInsertPass = _pnlinsertpass
		TxtInsertPass = _txtinsertpass
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub AssignAdminPanelGUIFunc(ByRef _pnladmin As Panel, ByRef _txtadminuser As TextBox, ByRef _txtadmincommand As TextBox)
		PnlAdmin = _pnladmin
		TxtAdminCommand = _txtadmincommand
		TxtAdminUser = _txtadminuser
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub AssignVaporChatComponentsFunc(ByRef _timercheckmsg As Timer, ByRef _timergui As Timer, ByRef _timerpubblock As Timer, ByRef _timerautocloser As Timer)
		TimerCheckMsg = _timercheckmsg
		TimerGUI = _timergui
		TimerPubBlock = _timerpubblock
		TimerAutoCloser = _timerautocloser
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub HideKeyGest()
		Callback.ClbVaporFunc_HideKeyGestFunc()
		HideStatus = True
		TimerAutoCloser.Enabled = False
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub ShowFormGest()
		Callback.ClbVaporFunc_ShowFormGestFunc()
		RefreshTimCloserFunc()
		TimerAutoCloser.Enabled = True
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub MessageRecvFunc()
		Dim strdata As String = Date.Now().ToString()
		Dim show As Boolean = True
		Dim name As String = ""
		Dim message As String = ""
		Dim index As Short
#If LIMITVIEW Then
		Dim copieditems As ListViewItem
#End If
		VaporChat.GetMessageUserAndText(name, message)
		If message = VaporChat.LEAVEVAP And name = My.Settings.LastUser Then
			Exit Sub
		End If
		Dim item As New ListViewItem(New String() {name, message, strdata})
		index = SearchNameInList(name) ' Search for new user
		If index < 0 Then
			AddUserToList(name)
			index = SearchNameInList(name)
		End If
		Select Case message
			Case VaporChat.JOINVAPO
				If VaporChat.CurrentTheme = VaporChat.Themes.Hide Then
					message = VaporChat.JOINHIDE
				End If
			Case VaporChat.ITSMEMSG
				show = False
		End Select
		MessageRxOn = True
		If show = True Then
			' Color assign to new item
			item.ForeColor = GetColorAtIndex(index)
			' Protect a message if date is displayed
			ForceSwitchOffFunc()
			' Copy items in list view
#If LIMITVIEW Then
			For i As UShort = 0 To VaporChat.MAXROWS - 1
			copieditems = LstChatVapo.Items(i + 1).Clone
			LstChatVapo.Items(i) = copieditems
			Next
			' Add item to ListView
			LstChatVapo.Items(VaporChat.MAXROWS) = item
			If NofMessages < VaporChat.MAXROWS Then
			NofMessages += 1
			End If
#Else
			LstChatVapo.Items.Insert(LstChatVapo.Items.Count, item)
			LstChatVapo.Items(LstChatVapo.Items.Count - 1).EnsureVisible()
#End If
			' Notify if minimized
			If HideStatus = True Then
				Callback.ClbVaporFunc_NotifyIconUnreadFunc()
			End If
		End If
		' Check message type for Users management
		If name <> My.Settings.LastUser Then
			Select Case message
				Case VaporChat.LEAVEVAP
					RemoveUserFromList(name)
				Case VaporChat.JOINHIDE
					VaporChat.SendMessage(My.Settings.LastUser, VaporChat.ITSMEMSG)
				Case VaporChat.JOINVAPO
					VaporChat.SendMessage(My.Settings.LastUser, VaporChat.ITSMEMSG)
			End Select
		End If
		MessageRxOn = False
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub ConfigRecvFunc()
		Dim struser As String = ""
		Dim strtext As String = ""
		VaporChat.GetConfigUserAndText(struser, strtext)
		Dim strdata() As String = strtext.Split(VaporChat.ADMINSPLIT)
		Dim confcommand As String = strdata(0)
		If struser = My.Settings.LastUser Then
			Select Case confcommand
				Case VaporChat.ADMINMUTEU
					My.Settings.Muted = True
					My.Settings.Save()
				Case VaporChat.ADMINUMUTE
					My.Settings.Muted = False
					My.Settings.Save()
				Case VaporChat.ADMINLOBBY
					My.Settings.Lobby = strdata(1)
					My.Settings.Save()
					VaporChat.Disconnect()
					VaporChat.Connect(My.Settings.LastUser, My.Settings.Lobby)
			End Select
		End If
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------' 
	Public Sub RefreshTimCloserFunc()
		Static modder As Boolean = True
		If modder = True Then
			modder = False
			TimerAutoCloser.Interval = My.Settings.Timeout - 1
		Else
			modder = True
			TimerAutoCloser.Interval = My.Settings.Timeout
		End If
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub FormLoadFunc()
		Control.CheckForIllegalCrossThreadCalls = False
		' Init timers 
		TimerCheckMsg.Interval = VaporChat.TCHKMSGR
		TimerPubBlock.Interval = VaporChat.TSTOPPUB
		TimerGUI.Interval = VaporChat.TUPDTGUI
		TimerAutoCloser.Interval = My.Settings.Timeout
		' Init
		UserList.Clear()
		TaskBarHid = False
		Connected = False
#If LIMVIEW Then
		NofMessages = 0
#End If
		BannedText.Add(VaporChat.SEPTCHAR.ToLower().Replace(" ", ""))
		BannedText.Add(VaporChat.JOINVAPO.ToLower().Replace(" ", ""))
		BannedText.Add(VaporChat.LEAVEVAP.ToLower().Replace(" ", ""))
		BannedText.Add(VaporChat.JOINHIDE.ToLower().Replace(" ", ""))
		BannedText.Add(VaporChat.ITSMEMSG.ToLower().Replace(" ", ""))
		TxtUser.MaxLength = VaporChat.MaxUserLen()
		TxtUser.Text = My.Settings.LastUser
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub LogInFunc()
		My.Settings.Lobby = TxtLobby.Text
		My.Settings.LastUser = TxtUser.Text
		My.Settings.Save()
		TxtMsg.MaxLength = VaporChat.MaxMessageLen()
		If VaporChat.Connect(My.Settings.LastUser, My.Settings.Lobby) Then
			AsyncOp = True
			Connected = True
			Select Case VaporChat.CurrentTheme
				Case VaporChat.Themes.Vapor
					VaporChat.SendMessage(My.Settings.LastUser, VaporChat.JOINVAPO)
				Case VaporChat.Themes.Hide
					VaporChat.SendMessage(My.Settings.LastUser, VaporChat.JOINHIDE)
			End Select
			If SearchNameInList(My.Settings.LastUser) < 0 Then
				AddUserToList(My.Settings.LastUser)
			End If
			LblLog.Text = VaporChat.LOGNOERR
			TimerCheckMsg.Enabled = True
			BtnSend.Enabled = True
			Select Case VaporChat.CurrentTheme
				Case VaporChat.Themes.Vapor
					TxtMsg.ForeColor = UserList(0).Color
				Case VaporChat.Themes.Hide
			End Select
			ClearTextBox(TxtMsg)
		Else
			LblLog.Text = VaporChat.LOGERROR
		End If
		RefreshTimCloserFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub SendMsgFunc()
		If TxtMsg.Text = VaporChat.TOKIDRIFT Then
			ClearTextBox(TxtMsg)
		End If
		If My.Settings.Muted = False Then
			If BtnSend.Enabled = True Then
				BtnSend.Enabled = False
				TimerPubBlock.Enabled = True
				If Connected = True Then
					AsyncOp = True
					If BannedText.Contains(TxtMsg.Text.ToLower().Replace(" ", "")) Then
						LblLog.Text = VaporChat.FUNNYBOI
						ClearTextBox(TxtMsg)
					ElseIf TxtMsg.Text = VaporChat.TOKIDRIFT Then
						ClearTextBox(TxtMsg)
					Else
						If VaporChat.SendMessage(My.Settings.LastUser, TxtMsg.Text) Then
							LblLog.Text = VaporChat.SENDISOK
							ClearTextBox(TxtMsg)
						Else
							LblLog.Text = VaporChat.LOGERROR
						End If
					End If
				Else
					LblLog.Text = VaporChat.COMERROR
				End If
			End If
		Else
			LblLog.Text = VaporChat.BLOCKEDU
		End If
		RefreshTimCloserFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub SendCmdFunc()
		If TxtAdminCommand.Text <> "" Then
			If TxtAdminUser.Text <> "" Then
				If VaporChat.Connect(VaporChat.ADMINUNAME, My.Settings.Lobby) Then
					VaporChat.SendConfig(TxtAdminUser.Text, TxtAdminCommand.Text)
					TxtAdminCommand.Text = ""
					TxtAdminUser.Text = ""
				End If
			End If
		End If
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub TimerChkMsgFunc()
		While VaporChat.CheckMessageRecv()
			MessageRecvFunc()
		End While
		While VaporChat.CheckConfigRecv()
			ConfigRecvFunc()
		End While
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub MsgBoxKeyDownFunc(ByRef e As KeyEventArgs)
		Select Case e.KeyCode
			Case VaporChat.SENDUKEY
				If BtnSend.Enabled Then
					BtnSend.PerformClick()
					e.Handled = True
					e.SuppressKeyPress = True
				End If
		End Select
		RefreshTimCloserFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub UserBoxKeyDownFunc(ByRef e As KeyEventArgs)
		Select Case e.KeyCode
			Case VaporChat.SENDUKEY
		End Select
		RefreshTimCloserFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub LogOutFunc()
		ClosingFunc()
		VaporChat.CurrentTheme = VaporChat.Themes.Start
		PnlStartScreen.BringToFront()
		TxtPassword.Focus()
		Callback.ClbVaporFunc_LogoutFunc()
		HideStatus = True
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub FormKeyDownFunc(ByRef e As KeyEventArgs)
		Select Case VaporChat.CurrentTheme
			Case VaporChat.Themes.Start
				If e.KeyValue = Keys.Enter Then
					Select Case My.Settings.LastTheme
						Case VaporChat.Themes.Vapor
							BtnVapor.PerformClick()
						Case VaporChat.Themes.Hide
							BtnHide.PerformClick()
					End Select
				Else

				End If
			Case Else
				Select Case e.KeyCode
					Case VaporChat.HIDEUKEY
						If My.Computer.Keyboard.AltKeyDown Then
							ForcePass = True
						Else
							ForcePass = False
						End If
						HideKeyGest()
					Case VaporChat.SHOWUKEY
						ShowFormGest()
				End Select
		End Select
		RefreshTimCloserFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub MinimizeFormFunc(ByVal forcepassword As Boolean)
		ForcePass = forcepassword
		HideKeyGest()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub ClosingFunc()
		If Connected = True Then
			If VaporChat.CurrentTheme <> VaporChat.Themes.Admin Then
				VaporChat.SendMessage(My.Settings.LastUser, VaporChat.LEAVEVAP)
			End If
			VaporChat.Disconnect()
			Connected = False
		End If
		UserList.Clear()
		LstChatVapo.Items.Clear()
		TxtPassword.Text = ""
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub ForceSwitchOffFunc()
		If SwitchOn = True And SwitchIndex >= 0 Then
			SwitchOn = False
			LstChatVapo.Items(SwitchIndex).SubItems(1).Text = SwitchText
		End If
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub UpdateGUIFunc()
		LblUsers.Text = UserList.Count.ToString()
		If AsyncOp Then
			If VaporChat.GetSubOngoing() Or VaporChat.GetPubOngoing() Or VaporChat.GetConOngoing() Then
				Select Case DotIndex
					Case 0
						LblLog.Text = VaporChat.LOGPROG01
						DotIndex += 1
					Case 1
						LblLog.Text = VaporChat.LOGPROG02
						DotIndex += 1
					Case 2
						LblLog.Text = VaporChat.LOGPROG03
						DotIndex = 0
				End Select
			Else
				AsyncOp = False
				DotIndex = 0
				If LblLog.Text <> VaporChat.FUNNYBOI Then
					LblLog.Text = VaporChat.LOGNOERR
				End If
			End If
		Else
			If VaporChat.GetSubState() = False Then
				If VaporChat.GetSubOngoing() = False Then
					LblLog.Text = VaporChat.LOGERROR
				End If
			End If
			If VaporChat.GetPubState() = False Then
				If VaporChat.GetSubOngoing() = False And VaporChat.GetPubOngoing() = False Then
					If LblLog.Text <> VaporChat.LOGERROR Then
						LblLog.Text = VaporChat.SENDISKO
						BtnSend.Enabled = True
					End If
				End If
			End If
		End If
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub ShowUserListFunc()
		UsersListOn = True
		Select Case VaporChat.CurrentTheme
			Case VaporChat.Themes.Vapor
				StsUsersList.Text = VaporChat.USRBOXVP
			Case VaporChat.Themes.Hide
				StsUsersList.Text = VaporChat.USRBOXHI
		End Select
		PnlUsersList.BringToFront()
		LstUsersList.Clear()
		For Each user As User In UserList
			If VaporChat.CurrentTheme = VaporChat.Themes.Vapor Then
				Dim item As New ListViewItem(New String() {user.Name}) With {.ForeColor = user.Color}
				LstUsersList.Items.Add(item)
			Else
				Dim item As New ListViewItem(New String() {user.Name}) With {.ForeColor = VaporChat.HIDE_CHATFRTCLR}
				LstUsersList.Items.Add(item)
			End If
		Next
		RefreshTimCloserFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub PubBlockTickFunc()
		BtnSend.Enabled = True
		TimerPubBlock.Enabled = False
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub CopyItemFunc(ByRef objLst As ListView, e As MouseEventArgs)
		If e.Button = MouseButtons.Left And My.Computer.Keyboard.CtrlKeyDown Then
			Select Case objLst.Name
				Case LstChatVapo.Name
#If LIMVIEW Then
						If objLst.SelectedIndices(0) < LstChatVapo.Items.Count Then
							If objLst.Items(objLst.SelectedIndices(0)).SubItems.Count > 0 Then
								If objLst.Items(objLst.SelectedIndices(0)).SubItems(1).Text <> "" Then
									Clipboard.Clear()
									Clipboard.SetText(objLst.Items(objLst.SelectedIndices(0)).SubItems(1).Text)
								End If
							End If
						End If
#Else
					If objLst.SelectedIndices(0) < LstChatVapo.Items.Count Then
						If objLst.Items(objLst.SelectedIndices(0)).SubItems.Count > 0 Then
							If objLst.Items(objLst.SelectedIndices(0)).SubItems(1).Text <> "" Then
								Clipboard.Clear()
								Clipboard.SetText(objLst.Items(objLst.SelectedIndices(0)).SubItems(1).Text)
							End If
						End If
					End If
#End If
				Case LstUsersList.Name
					If objLst.Items(objLst.SelectedIndices(0)).SubItems.Count > 0 Then
						If objLst.Items(objLst.SelectedIndices(0)).SubItems(0).Text <> "" Then
							Clipboard.Clear()
							Clipboard.SetText(objLst.Items(objLst.SelectedIndices(0)).SubItems(0).Text)
							objLst.Focus()
						End If
					End If
			End Select
		End If
		RefreshTimCloserFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub ShowDateFunc(ByRef objLst As ListView, e As MouseEventArgs)
		If e.Button = MouseButtons.Right Then
			Select Case objLst.Name
				Case LstChatVapo.Name
					If MessageRxOn = False Then
						If objLst.SelectedIndices(0) < LstChatVapo.Items.Count Then
							If SwitchOn = False Then
								SwitchIndex = objLst.SelectedIndices(0)
								SwitchText = objLst.Items(SwitchIndex).SubItems(1).Text
								objLst.Items(SwitchIndex).SubItems(1).Text = objLst.Items(SwitchIndex).SubItems(2).Text
								SwitchOn = True
							Else
								objLst.Items(SwitchIndex).SubItems(1).Text = SwitchText
								SwitchOn = False
								SwitchIndex = -1
							End If
						End If
					End If
				Case LstUsersList.Name

			End Select
		End If
		RefreshTimCloserFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub ItemMouseUpFunc(ByRef item_mousedown As Boolean)
		item_mousedown = False
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub MoveItemFunc(ByRef item As Object, ByRef item_mousedown As Boolean, ByRef item_mousepos As Point, e As MouseEventArgs)
		If e.Button = MouseButtons.Left Then
			If item_mousedown = False Then
				item_mousedown = True
				item_mousepos = New Point(e.X, e.Y)
			End If
			item.Location = New Point(item.Location.X + e.X - item_mousepos.X, item.Location.Y + e.Y - item_mousepos.Y)
		End If
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub PasswordTextChangedFunc()
		If TxtInsertPass.Text = VaporChat.PASSCHAT Then
			TxtInsertPass.Text = ""
			Select Case VaporChat.CurrentTheme
				Case VaporChat.Themes.Start
					PnlStartScreen.BringToFront()
					Callback.ClbVaporFunc_RestoreWindowFunc(VaporChat.STARTWIDTH, VaporChat.STARTHEIGH)
					TxtPassword.Focus()
				Case VaporChat.Themes.Vapor
					PnlVaporChat.BringToFront()
					Callback.ClbVaporFunc_RestoreWindowFunc(VaporChat.CHATWIDTH, VaporChat.CHATHEIGH)
				Case VaporChat.Themes.Hide
					PnlVaporChat.BringToFront()
					Callback.ClbVaporFunc_RestoreWindowFunc(VaporChat.CHATWIDTH, VaporChat.CHATHEIGH)
				Case VaporChat.Themes.Admin
					PnlAdmin.BringToFront()
					Callback.ClbVaporFunc_RestoreWindowFunc(VaporChat.ADMNWIDTH, VaporChat.ADMNHEIGH)
			End Select
			ForcePass = False
		End If
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub CloseUserListFunc()
		PnlUsersList.SendToBack()
		UsersListOn = False
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub StartScreenLoadFunc()
		VaporChat.CurrentTheme = VaporChat.Themes.Start
		LblVaporChat2020Ver.Text = My.Settings.VaporChat2020Ver
		TxtPassword.Focus()
		CmbCloserTime.Text = My.Settings.Timeout / 1000
		TxtUser.Text = My.Settings.LastUser
		TxtLobby.Text = My.Settings.Lobby
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub ShowHideChatFunc()
		If TxtPassword.Text = "" Then
			TxtPassword.Focus()
		ElseIf TxtPassword.Text = VaporChat.PASSCHAT Then
			My.Settings.LastTheme = VaporChat.Themes.Hide
			My.Settings.Save()
			VaporChat.CurrentTheme = VaporChat.Themes.Hide
			PnlVaporChat.BringToFront()
			Callback.ClbVaporFunc_InitChatGUIFunc()
			LogInFunc()
		Else
			CallerForm.Close()
		End If
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub ShowVaporChatFunc()
		If TxtPassword.Text = "" Then
			TxtPassword.Focus()
		ElseIf TxtPassword.Text = VaporChat.PASSCHAT Then
			My.Settings.LastTheme = VaporChat.Themes.Vapor
			My.Settings.Save()
			VaporChat.CurrentTheme = VaporChat.Themes.Vapor
			PnlVaporChat.BringToFront()
			Callback.ClbVaporFunc_InitChatGUIFunc()
			LogInFunc()
		Else
			CallerForm.Close()
		End If
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub AccessAdminPanelFunc(e As EventArgs)
		If My.Computer.Keyboard.CtrlKeyDown And
			My.Computer.Keyboard.ShiftKeyDown And
			My.Computer.Keyboard.AltKeyDown And
			My.Computer.Keyboard.CapsLock Then
			Dim password As String = InputBox("You shall insert a passcode:")
			If password = VaporChat.ADMINPASSW Then
				My.Settings.LastTheme = VaporChat.Themes.Admin
				My.Settings.Save()
				VaporChat.CurrentTheme = VaporChat.Themes.Admin
				PnlAdmin.BringToFront()
				Callback.ClbVaporFunc_InitAdminGUIFunc()
			End If
		End If
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub UpdateCloseTimeFunc()
		My.Settings.Timeout = CmbCloserTime.Text * 1000
		My.Settings.Save()
	End Sub
End Class
