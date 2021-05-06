Imports System.ComponentModel


Public Class MainScreen
	'--- Class Imports -----------------------------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	ReadOnly VaporChat As New VaporChat
	ReadOnly VaporFunc As New VaporFunc


	'--- V A P O R G U I | Structures --------------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	Structure ChatTheme
		Dim MainWinTxt As String
		Dim BackImage As Image
		Dim BackColor As Color
		Dim LstChat As TxtTheme
		Dim TxtUser As TxtTheme
		Dim TxtMsg As TxtTheme
		Dim LblLogs As LblTheme
		Dim LblUser As LblTheme
		Dim BtnLog As BtnTheme
		Dim BtnSend As BtnTheme
		Dim BtnBack As BtnTheme
	End Structure
	'-----------------------------------------------------------------------------------------------------------------------'
	Structure LblTheme
		Dim FixText As String
		Dim FixColor As Color
		Dim VarColor As Color
	End Structure
	'-----------------------------------------------------------------------------------------------------------------------'
	Structure TxtTheme
		Dim BackColor As Color
		Dim TextColor As Color
	End Structure
	'-----------------------------------------------------------------------------------------------------------------------'
	Structure BtnTheme
		Dim BackColor As Color
		Dim ForeColor As Color
		Dim FlatStyle As FlatStyle
	End Structure


	'--- V A P O R G U I | Variables ---------------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	Public WithEvents Notify As New NotifyIcon
	Private PNLUSR_MouseIsDown As Boolean = False
	Private PNLUSR_MouseIsDownLoc As Point = Nothing


	' V A P O R G U I | Functions ------------------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub AssignVaporTheme(ByVal theme As VaporChat.Themes)
		If theme = VaporChat.Themes.Vapor Or theme = VaporChat.Themes.Hide Or theme = VaporChat.Themes.Admin Then
			VaporChat.CurrentTheme = theme
			Text = VaporChat.VAPOR_MAINWINTXT(VaporChat.CurrentTheme)
			PnlVaporChat.BackgroundImage = VaporChat.VAPOR_MAINBCKIMG(VaporChat.CurrentTheme)
			PnlInsertPass.BackgroundImage = VaporChat.VAPOR_MAINBCKIMG(VaporChat.CurrentTheme)
			PnlVaporChat.BackColor = VaporChat.VAPOR_MAINBCKCLR(VaporChat.CurrentTheme)
			PnlInsertPass.BackColor = VaporChat.VAPOR_MAINBCKCLR(VaporChat.CurrentTheme)
			TxtInsertPass.BackColor = VaporChat.VAPOR_USERBCKCLR(VaporChat.CurrentTheme)
			TxtInsertPass.ForeColor = VaporChat.VAPOR_USERFRTCLR(VaporChat.CurrentTheme)
			LstChatVapo.BackColor = VaporChat.VAPOR_CHATBCKCLR(VaporChat.CurrentTheme)
			LstChatVapo.ForeColor = VaporChat.VAPOR_CHATFRTCLR(VaporChat.CurrentTheme)
			TxtMsg.BackColor = VaporChat.VAPOR_SENDBCKCLR(VaporChat.CurrentTheme)
			TxtMsg.ForeColor = VaporChat.VAPOR_SENDFRTCLR(VaporChat.CurrentTheme)
			DskLblLogs.Text = VaporChat.VAPOR_LBLLOGFTXT(VaporChat.CurrentTheme)
			DskLblLogs.ForeColor = VaporChat.VAPOR_LBLLOGFCLR(VaporChat.CurrentTheme)
			LblLog.ForeColor = VaporChat.VAPOR_LBLLOGVCLR(VaporChat.CurrentTheme)
			DskLblUsers.Text = VaporChat.VAPOR_LBLUSRFTXT(VaporChat.CurrentTheme)
			DskLblUsers.ForeColor = VaporChat.VAPOR_LBLUSRFCLR(VaporChat.CurrentTheme)
			LblUsers.ForeColor = VaporChat.VAPOR_LBLUSRVCLR(VaporChat.CurrentTheme)
			BtnSend.FlatStyle = VaporChat.VAPOR_BTNFLSTYLE(VaporChat.CurrentTheme)
			BtnSend.BackColor = VaporChat.VAPOR_BTNSNDBCLR(VaporChat.CurrentTheme)
			BtnSend.ForeColor = VaporChat.VAPOR_BTNSNDFCLR(VaporChat.CurrentTheme)
			BtnBackToStart.FlatStyle = VaporChat.VAPOR_BTNFLSTYLE(VaporChat.CurrentTheme)
			BtnBackToStart.BackColor = VaporChat.VAPOR_BTNBCKBCLR(VaporChat.CurrentTheme)
			BtnBackToStart.ForeColor = VaporChat.VAPOR_BTNBCKFCLR(VaporChat.CurrentTheme)
			LstUsersList.BackColor = VaporChat.VAPOR_USRLSTBCLR(VaporChat.CurrentTheme)
#If LIMVIEW Then
				For i = 0 To VaporChat.MAXROWS
					Dim LstItem As New ListViewItem With {
						.BackColor = VaporChat.VAPOR_CHATBCKCLR(VaporChat.CurrentTheme),
						.ForeColor = VaporChat.VAPOR_CHATFRTCLR(VaporChat.CurrentTheme)
						}
					LstChatVapo.Items.Insert(i, LstItem)
				Next
#End If
		End If
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub VapoMainScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Dim NotifyIcon As New Icon(VaporChat.ICONPATH)
		Notify.Icon = NotifyIcon
		Notify.Visible = False
		PnlUsersList.SendToBack()
		PnlStartScreen.BringToFront()
		VaporChat.CurrentTheme = VaporChat.Themes.Start
		Size = New Size(VaporChat.STARTWIDTH, VaporChat.STARTHEIGH)
		Text = VaporChat.VAPOR_MAINWINTXT(VaporChat.CurrentTheme)
		VaporFunc.AssignMainFormGUIFunc(Me, ProgressOps)
		VaporFunc.AssignStartScreenPanelGUIFunc(PnlStartScreen, BtnHide, BtnVapor, TxtLobby, TxtUser, TxtPassword, CmbCloserTime, LblVaporChat2020Ver, Lblkronelab)
		VaporFunc.AssignVaporChatPanelGUIFunc(PnlVaporChat, LstChatVapo, TxtMsg, LblLog, LblUsers, BtnSend, BtnBackToStart)
		VaporFunc.AssignUserListPanelGUIFunc(PnlUsersList, LstUsersList, StsUsersList)
		VaporFunc.AssignPasswordPanelGUIFunc(PnlInsertPass, TxtInsertPass)
		VaporFunc.AssignAdminPanelGUIFunc(PnlAdmin, TxtAdminUser, TxtAdminCommand)
		VaporFunc.AssignVaporChatComponentsFunc(TimerCheckMsg, TimerGUI, TimerPubBlock, TimerAutoCloser)
		VaporFunc.StartScreenLoadFunc()
		VaporFunc.FormLoadFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub Notify_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles Notify.DoubleClick
		VaporFunc.NotifyDoubleClickFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub CmdSendMsg_Click(sender As Object, e As EventArgs) Handles BtnSend.Click
		VaporFunc.SendMsgFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub TimerCheckMsg_Tick(sender As Object, e As EventArgs) Handles TimerCheckMsg.Tick
		VaporFunc.TimerChkMsgFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub TxtMsg_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtMsg.KeyDown
		VaporFunc.MsgBoxKeyDownFunc(e)
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub TxtUser_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtUser.KeyDown
		VaporFunc.UserBoxKeyDownFunc(e)
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub VapoMainScreenvb_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
		VaporFunc.FormKeyDownFunc(e)
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub VapoMainScreenvb_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
		If Me.WindowState = FormWindowState.Minimized Then
			VaporFunc.MinimizeFormFunc(False)
		End If
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub VapoMainScreenvb_Closing(sender As Object, e As CancelEventArgs) Handles MyBase.Closing
		VaporFunc.ClosingFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub LstChatVapo_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles LstChatVapo.ItemSelectionChanged
		VaporFunc.ForceSwitchOffFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub TimerGUI_Tick(sender As Object, e As EventArgs) Handles TimerGUI.Tick
		VaporFunc.UpdateGUIFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub LblNofUsers_Click(sender As Object, e As EventArgs) Handles LblUsers.Click
		VaporFunc.ShowUserListFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub TimerPubBlock_Tick(sender As Object, e As EventArgs) Handles TimerPubBlock.Tick
		VaporFunc.PubBlockTickFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub LstChatVapo_MouseClick(sender As Object, e As MouseEventArgs) Handles LstChatVapo.MouseClick
		VaporFunc.CopyItemFunc(LstChatVapo, e)
		VaporFunc.ShowDateFunc(LstChatVapo, e)
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub LstUsersList_MouseClick(sender As Object, e As MouseEventArgs) Handles LstUsersList.MouseClick
		VaporFunc.CopyItemFunc(LstUsersList, e)
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub VaporMainScreen_LostFocus(sender As Object, e As EventArgs) Handles MyBase.LostFocus
		VaporFunc.ForceSwitchOffFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub TimerAutoCloser_Tick(sender As Object, e As EventArgs) Handles TimerAutoCloser.Tick
		VaporFunc.MinimizeFormFunc(True)
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub TxtInsertPass_TextChanged(sender As Object, e As EventArgs) Handles TxtInsertPass.TextChanged
		VaporFunc.PasswordTextChangedFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub BtnAdminSend_Click(sender As Object, e As EventArgs) Handles BtnAdminSend.Click
		VaporFunc.SendCmdFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub BtnAdminBackToStart_Click(sender As Object, e As EventArgs) Handles BtnAdminBackToStart.Click
		VaporFunc.LogOutFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub BtnBackToStart_Click(sender As Object, e As EventArgs) Handles BtnBackToStart.Click
		VaporFunc.LogOutFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub BtnCloseUsersList_Click(sender As Object, e As EventArgs) Handles BtnCloseUsersList.Click
		VaporFunc.CloseUserListFunc()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub StsUsersList_MouseMove(sender As Object, e As MouseEventArgs) Handles StsUsersList.MouseMove
		VaporFunc.MoveItemFunc(PnlUsersList, PNLUSR_MouseIsDown, PNLUSR_MouseIsDownLoc, e)
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub StsUsersList_MouseUp(sender As Object, e As MouseEventArgs) Handles StsUsersList.MouseUp
		VaporFunc.ItemMouseUpFunc(PNLUSR_MouseIsDown)
	End Sub

	Private Sub BtnHide_Click(sender As Object, e As EventArgs) Handles BtnHide.Click
		VaporFunc.ShowHideChatFunc()
	End Sub

	Private Sub BtnVapor_Click(sender As Object, e As EventArgs) Handles BtnVapor.Click
		VaporFunc.ShowVaporChatFunc()
	End Sub

	Private Sub CmbCloserTime_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbCloserTime.SelectedIndexChanged
		VaporFunc.UpdateCloseTimeFunc()
	End Sub

	Private Sub Lblkronelab_Click(sender As Object, e As EventArgs) Handles Lblkronelab.Click
		VaporFunc.AccessAdminPanelFunc(e)
	End Sub

	Private Sub ProgressOps_DoWork(sender As Object, e As DoWorkEventArgs) Handles ProgressOps.DoWork
		VaporFunc.ShowOpProgressFunc()
	End Sub
End Class
