Public Class InterfaceVaporFuncGUI
	' V A P O R G U I | Callback functions ---------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub ClbVaporFunc_RestoreWindowFunc(ByVal width As Integer, ByVal height As Integer)
		MainScreen.Size = New Size(width, height)
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub ClbVaporFunc_LogoutFunc()
		StartScreen.Show()
		MainScreen.Close()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub ClbVaporFunc_HideKeyGestFunc()
		MainScreen.Hide()
		MainScreen.ShowInTaskbar = False
		MainScreen.Notify.Visible = True
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub ClbVaporFunc_ShowFormGestFunc()
		MainScreen.Show()
		MainScreen.WindowState = FormWindowState.Normal
		MainScreen.ShowInTaskbar = False
		MainScreen.Notify.Visible = False
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub ClbVaporFunc_NotifyIconUnreadFunc()
		Dim NotifyIcon As New Icon(VaporChat.ICONNMSG)
		MainScreen.Notify.Icon = NotifyIcon
		MainScreen.Notify.Visible = True
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub ClbVaporFunc_NotifyIconReadFunc()
		Dim NotifyIcon As New Icon(VaporChat.ICONPATH)
		MainScreen.Notify.Icon = NotifyIcon
		MainScreen.Notify.Visible = True
	End Sub
End Class
