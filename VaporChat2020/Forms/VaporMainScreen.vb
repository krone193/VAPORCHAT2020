Imports System.ComponentModel

Public Class VaporMainScreen
  ReadOnly Vapor As New VaporFunc

  Structure ChatTheme
    Dim BackImage As Image
    Dim BackColor As Color
    Dim LstChat As TxtTheme
    Dim TxtUser As TxtTheme
    Dim TxtMsg As TxtTheme
    Dim LblLogs As LblTheme
    Dim LblUser As LblTheme
  End Structure

  Structure LblTheme
    Dim FixText As String
    Dim FixColor As Color
    Dim VarColor As Color
  End Structure

  Structure TxtTheme
    Dim BackColor As Color
    Dim TextColor As Color
  End Structure



  ' V A P O R C H A T 2 0 2 0 T H E M E
  ReadOnly VAPOR_MAINBCKIMG As Image = Image.FromFile("Resources/ondulvapor.jpg")
  ReadOnly VAPOR_MAINBCKCLR As Color = Color.FromArgb(40, 31, 51)
  ReadOnly VAPOR_CHATBCKCLR As Color = Color.FromArgb(40, 31, 51)
  ReadOnly VAPOR_CHATFRTCLR As Color = Color.Gold
  ReadOnly VAPOR_USERBCKCLR As Color = Color.FromArgb(40, 31, 51)
  ReadOnly VAPOR_USERFRTCLR As Color = SystemColors.Highlight
  ReadOnly VAPOR_SENDBCKCLR As Color = Color.FromArgb(40, 31, 51)
  ReadOnly VAPOR_SENDFRTCLR As Color = Color.DarkOrchid
  ReadOnly VAPOR_LBLLOGFTXT As String = "Logs をノだ"
  ReadOnly VAPOR_LBLLOGFCLR As Color = Color.HotPink
  ReadOnly VAPOR_LBLLOGVCLR As Color = Color.Pink
  ReadOnly VAPOR_LBLUSRFTXT As String = "Logged users 俺鉛プ"
  ReadOnly VAPOR_LBLUSRFCLR As Color = Color.Chartreuse
  ReadOnly VAPOR_LBLUSRVCLR As Color = Color.Lime


  ' H I D E C H A T 2 0 2 0 T H E M E
  ReadOnly HIDE_MAINBCKIMG As Image
  ReadOnly HIDE_MAINBCKCLR As Color = SystemColors.Control
  ReadOnly HIDE_CHATBCKCLR As Color = SystemColors.ControlLightLight
  ReadOnly HIDE_CHATFRTCLR As Color = SystemColors.WindowText
  ReadOnly HIDE_USERBCKCLR As Color = SystemColors.Control
  ReadOnly HIDE_USERFRTCLR As Color = SystemColors.WindowText
  ReadOnly HIDE_SENDBCKCLR As Color = SystemColors.Control
  ReadOnly HIDE_SENDFRTCLR As Color = SystemColors.WindowText
  ReadOnly HIDE_LBLLOGFTXT As String = "Logs"
  ReadOnly HIDE_LBLLOGFCLR As Color = SystemColors.WindowText
  ReadOnly HIDE_LBLLOGVCLR As Color = SystemColors.WindowText
  ReadOnly HIDE_LBLUSRFTXT As String = "Logged users"
  ReadOnly HIDE_LBLUSRFCLR As Color = SystemColors.WindowText
  ReadOnly HIDE_LBLUSRVCLR As Color = SystemColors.WindowText


  Dim CurrentGUI As ChatTheme
  Dim CurrentTheme As VaporFunc.Themes

  Private Sub VapoMainScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Select Case My.Settings.LastTheme
      Case VaporFunc.Themes.Vapor
        CurrentTheme = VaporFunc.Themes.Vapor
        CurrentGUI.BackImage = VAPOR_MAINBCKIMG
        CurrentGUI.BackColor = VAPOR_MAINBCKCLR
        CurrentGUI.LstChat.BackColor = VAPOR_CHATBCKCLR
        CurrentGUI.LstChat.TextColor = VAPOR_CHATFRTCLR
        CurrentGUI.TxtUser.BackColor = VAPOR_USERBCKCLR
        CurrentGUI.TxtUser.TextColor = VAPOR_USERFRTCLR
        CurrentGUI.TxtMsg.BackColor = VAPOR_SENDBCKCLR
        CurrentGUI.TxtMsg.TextColor = VAPOR_SENDFRTCLR
        CurrentGUI.LblLogs.FixText = VAPOR_LBLLOGFTXT
        CurrentGUI.LblLogs.FixColor = VAPOR_LBLLOGFCLR
        CurrentGUI.LblLogs.VarColor = VAPOR_LBLLOGVCLR
        CurrentGUI.LblUser.FixText = VAPOR_LBLUSRFTXT
        CurrentGUI.LblUser.FixColor = VAPOR_LBLUSRFCLR
        CurrentGUI.LblUser.VarColor = VAPOR_LBLUSRVCLR
      Case VaporFunc.Themes.Hide
        CurrentTheme = VaporFunc.Themes.Hide
        CurrentGUI.BackImage = HIDE_MAINBCKIMG
        CurrentGUI.BackColor = HIDE_MAINBCKCLR
        CurrentGUI.LstChat.BackColor = HIDE_CHATBCKCLR
        CurrentGUI.LstChat.TextColor = HIDE_CHATFRTCLR
        CurrentGUI.TxtUser.BackColor = HIDE_USERBCKCLR
        CurrentGUI.TxtUser.TextColor = HIDE_USERFRTCLR
        CurrentGUI.TxtMsg.BackColor = HIDE_SENDBCKCLR
        CurrentGUI.TxtMsg.TextColor = HIDE_SENDFRTCLR
        CurrentGUI.LblLogs.FixText = HIDE_LBLLOGFTXT
        CurrentGUI.LblLogs.FixColor = HIDE_LBLLOGFCLR
        CurrentGUI.LblLogs.VarColor = HIDE_LBLLOGVCLR
        CurrentGUI.LblUser.FixText = HIDE_LBLUSRFTXT
        CurrentGUI.LblUser.FixColor = HIDE_LBLUSRFCLR
        CurrentGUI.LblUser.VarColor = HIDE_LBLUSRVCLR
      Case "null"
    End Select

    PnlVaporChat.BackgroundImage = CurrentGUI.BackImage
    PnlVaporChat.BackColor = CurrentGUI.BackColor
    PnlInsertPass.BackgroundImage = CurrentGUI.BackImage
    PnlInsertPass.BackColor = CurrentGUI.BackColor
    For i = 0 To 30
      LstChatVapo.Items.Item(i).BackColor = CurrentGUI.LstChat.BackColor
      LstChatVapo.Items.Item(i).ForeColor = CurrentGUI.LstChat.BackColor
    Next
    TxtUser.BackColor = CurrentGUI.TxtUser.BackColor
    TxtUser.ForeColor = CurrentGUI.TxtUser.TextColor
    TxtMsg.BackColor = CurrentGUI.TxtMsg.BackColor
    TxtMsg.ForeColor = CurrentGUI.TxtMsg.TextColor
    DskLblLogs.Text = CurrentGUI.LblLogs.FixText
    DskLblLogs.ForeColor = CurrentGUI.LblLogs.FixColor
    LblLog.ForeColor = CurrentGUI.LblLogs.VarColor
    DskLblUsers.Text = CurrentGUI.LblUser.FixText
    DskLblUsers.ForeColor = CurrentGUI.LblUser.FixColor
    LblUsers.ForeColor = CurrentGUI.LblUser.VarColor

    Vapor.FormLoadFunc(Me, LstChatVapo, BtnSend, BtnLogIn, TxtMsg, TxtUser, LblLog, LblUsers, TimerCheckMsg, TimerPubBlock, TimerGUI, TimerAutoCloser, CurrentTheme)
    PnlVaporChat.BringToFront()
  End Sub

  Private Sub CmdLogIn_Click(sender As Object, e As EventArgs) Handles BtnLogIn.Click
    Vapor.LogInFunc()
  End Sub

  Private Sub CmdSendMsg_Click(sender As Object, e As EventArgs) Handles BtnSend.Click
    Vapor.SendMsgFunc()
  End Sub

  Private Sub TimerCheckMsg_Tick(sender As Object, e As EventArgs) Handles TimerCheckMsg.Tick
    Vapor.TimerChkMsgFunc()
  End Sub

  Private Sub TxtMsg_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtMsg.KeyDown
    Vapor.MsgBoxKeyDownFunc(e)
  End Sub

  Private Sub TxtUser_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtUser.KeyDown
    Vapor.UserBoxKeyDownFunc(e)
  End Sub

  Private Sub VapoMainScreenvb_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
    Vapor.FormKeyDownFunc(e)
  End Sub

  Private Sub VapoMainScreenvb_Resize(sender As Object, e As EventArgs) Handles Me.Resize
    If WindowState = FormWindowState.Minimized Then
      Vapor.MinimizeFormFunc(False)
    End If
  End Sub

  Private Sub VapoMainScreenvb_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
    Vapor.ClosingFunc()
  End Sub

  Private Sub TxtChat_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles LstChatVapo.ItemSelectionChanged
    Vapor.ForceSwitchOffFunc()
  End Sub

  Private Sub TimerGUI_Tick(sender As Object, e As EventArgs) Handles TimerGUI.Tick
    Vapor.UpdateGUIFunc()
  End Sub

  Private Sub LblNofUsers_Click(sender As Object, e As EventArgs) Handles LblUsers.Click
    Vapor.ShowUserListFunc()
  End Sub

  Private Sub TimerPubBlock_Tick(sender As Object, e As EventArgs) Handles TimerPubBlock.Tick
    Vapor.PubBlockTickFunc()
  End Sub

  Private Sub TxtChat_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles LstChatVapo.MouseClick
    Vapor.CopyItemFunc(e)
  End Sub

  Private Sub VaporMainScreen_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
    Vapor.ForceSwitchOffFunc()
  End Sub

  Private Sub TimerAutoCloser_Tick(sender As Object, e As EventArgs) Handles TimerAutoCloser.Tick
    Vapor.MinimizeFormFunc(True)
  End Sub

  Private Sub TxtInsertPass_TextChanged(sender As Object, e As EventArgs) Handles TxtInsertPass.TextChanged
    If TxtInsertPass.Text = VaporChat.PASSCHAT Then
      TxtInsertPass.Text = ""
      PnlVaporChat.BringToFront()
    End If
  End Sub
End Class