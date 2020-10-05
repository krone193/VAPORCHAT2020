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
    Select Case theme
      Case VaporChat.Themes.Vapor
        VaporChat.CurrentTheme = VaporChat.Themes.Vapor
        Text = VaporChat.VAPOR_MAINWINTXT
        PnlVaporChat.BackgroundImage = VaporChat.VAPOR_MAINBCKIMG
        PnlVaporChat.BackColor = VaporChat.VAPOR_MAINBCKCLR
        PnlInsertPass.BackgroundImage = VaporChat.VAPOR_MAINBCKIMG
        PnlInsertPass.BackColor = VaporChat.VAPOR_MAINBCKCLR
        TxtInsertPass.BackColor = VaporChat.VAPOR_USERBCKCLR
        TxtInsertPass.ForeColor = VaporChat.VAPOR_USERFRTCLR
        LstChatVapo.BackColor = VaporChat.VAPOR_CHATBCKCLR
        LstChatVapo.ForeColor = VaporChat.VAPOR_CHATFRTCLR
        For i = 0 To VaporChat.MAXROWS
          LstChatVapo.Items.Item(i).BackColor = VaporChat.VAPOR_CHATBCKCLR
          LstChatVapo.Items.Item(i).ForeColor = VaporChat.VAPOR_CHATFRTCLR
        Next
        TxtUser.BackColor = VaporChat.VAPOR_USERBCKCLR
        TxtUser.ForeColor = VaporChat.VAPOR_USERFRTCLR
        TxtMsg.BackColor = VaporChat.VAPOR_SENDBCKCLR
        TxtMsg.ForeColor = VaporChat.VAPOR_SENDFRTCLR
        DskLblLogs.Text = VaporChat.VAPOR_LBLLOGFTXT
        DskLblLogs.ForeColor = VaporChat.VAPOR_LBLLOGFCLR
        LblLog.ForeColor = VaporChat.VAPOR_LBLLOGVCLR
        DskLblUsers.Text = VaporChat.VAPOR_LBLUSRFTXT
        DskLblUsers.ForeColor = VaporChat.VAPOR_LBLUSRFCLR
        LblUsers.ForeColor = VaporChat.VAPOR_LBLUSRVCLR
        BtnLogIn.FlatStyle = VaporChat.VAPOR_BTNFLSTYLE
        BtnLogIn.BackColor = VaporChat.VAPOR_BTNLOGBCLR
        BtnLogIn.ForeColor = VaporChat.VAPOR_BTNLOGFCLR
        BtnSend.FlatStyle = VaporChat.VAPOR_BTNFLSTYLE
        BtnSend.BackColor = VaporChat.VAPOR_BTNSNDBCLR
        BtnSend.ForeColor = VaporChat.VAPOR_BTNSNDFCLR
        BtnBackToStart.FlatStyle = VaporChat.VAPOR_BTNFLSTYLE
        BtnBackToStart.BackColor = VaporChat.VAPOR_BTNBCKBCLR
        BtnBackToStart.ForeColor = VaporChat.VAPOR_BTNBCKFCLR
        LstUsersList.BackColor = VaporChat.VAPOR_USRLSTBCLR
      Case VaporChat.Themes.Hide
        VaporChat.CurrentTheme = VaporChat.Themes.Hide
        Text = VaporChat.HIDE_MAINWINTXT
        PnlVaporChat.BackgroundImage = VaporChat.HIDE_MAINBCKIMG
        PnlVaporChat.BackColor = VaporChat.HIDE_MAINBCKCLR
        PnlInsertPass.BackgroundImage = VaporChat.HIDE_MAINBCKIMG
        PnlInsertPass.BackColor = VaporChat.HIDE_MAINBCKCLR
        TxtInsertPass.BackColor = VaporChat.HIDE_USERBCKCLR
        TxtInsertPass.ForeColor = VaporChat.HIDE_USERFRTCLR
        LstChatVapo.BackColor = VaporChat.HIDE_CHATBCKCLR
        LstChatVapo.ForeColor = VaporChat.HIDE_CHATFRTCLR
        For i = 0 To VaporChat.MAXROWS
          LstChatVapo.Items.Item(i).BackColor = VaporChat.HIDE_CHATBCKCLR
          LstChatVapo.Items.Item(i).ForeColor = VaporChat.HIDE_CHATFRTCLR
        Next
        TxtUser.BackColor = VaporChat.HIDE_USERBCKCLR
        TxtUser.ForeColor = VaporChat.HIDE_USERFRTCLR
        TxtMsg.BackColor = VaporChat.HIDE_SENDBCKCLR
        TxtMsg.ForeColor = VaporChat.HIDE_SENDFRTCLR
        DskLblLogs.Text = VaporChat.HIDE_LBLLOGFTXT
        DskLblLogs.ForeColor = VaporChat.HIDE_LBLLOGFCLR
        LblLog.ForeColor = VaporChat.HIDE_LBLLOGVCLR
        DskLblUsers.Text = VaporChat.HIDE_LBLUSRFTXT
        DskLblUsers.ForeColor = VaporChat.HIDE_LBLUSRFCLR
        LblUsers.ForeColor = VaporChat.HIDE_LBLUSRVCLR
        BtnLogIn.FlatStyle = VaporChat.HIDE_BTNFLSTYLE
        BtnLogIn.BackColor = VaporChat.HIDE_BTNLOGBCLR
        BtnLogIn.ForeColor = VaporChat.HIDE_BTNLOGFCLR
        BtnSend.FlatStyle = VaporChat.HIDE_BTNFLSTYLE
        BtnSend.BackColor = VaporChat.HIDE_BTNSNDBCLR
        BtnSend.ForeColor = VaporChat.HIDE_BTNSNDFCLR
        BtnBackToStart.FlatStyle = VaporChat.HIDE_BTNFLSTYLE
        BtnBackToStart.BackColor = VaporChat.HIDE_BTNBCKBCLR
        BtnBackToStart.ForeColor = VaporChat.HIDE_BTNBCKFCLR
        LstUsersList.BackColor = VaporChat.HIDE_USRLSTBCLR
      Case VaporChat.Themes.Admin
        VaporChat.CurrentTheme = VaporChat.Themes.Admin
        Text = VaporChat.ADMIN_MAINWINTXT
    End Select
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub VapoMainScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Dim NotifyIcon As New Icon(VaporChat.ICONPATH)
    Notify.Icon = NotifyIcon
    Notify.Visible = False
    TxtPassword.Focus()
    PnlUsersList.SendToBack()
    PnlStartScreen.BringToFront()
    VaporChat.CurrentTheme = VaporChat.Themes.Start
    Size = New Size(VaporChat.STARTWIDTH, VaporChat.STARTHEIGH)
    VaporFunc.AssignMainFormGUIFunc(Me)
    VaporFunc.AssignStartScreenPanelGUIFunc(PnlStartScreen, BtnHide, BtnVapor, TxtPassword, CmbCloserTime, LblVaporChat2020Ver, Lblkronelab)
    VaporFunc.AssignVaporChatPanelGUIFunc(PnlVaporChat, LstChatVapo, TxtUser, TxtMsg, LblLog, LblUsers, BtnLogIn, BtnSend, BtnBackToStart)
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
  Private Sub CmdLogIn_Click(sender As Object, e As EventArgs) Handles BtnLogIn.Click
    VaporFunc.LogInFunc()
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
  Private Sub VapoMainScreenvb_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
    VaporFunc.FormKeyDownFunc(e)
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub VapoMainScreenvb_Resize(sender As Object, e As EventArgs) Handles Me.Resize
    If Me.WindowState = FormWindowState.Minimized Then
      VaporFunc.MinimizeFormFunc(False)
    End If
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub VapoMainScreenvb_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
    VaporFunc.ClosingFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub TxtChat_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles LstChatVapo.ItemSelectionChanged
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
  Private Sub VaporMainScreen_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
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

  Private Sub TxtPassword_TextChanged(sender As Object, e As EventArgs) Handles TxtPassword.TextChanged

  End Sub

  Private Sub CmbCloserTime_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbCloserTime.SelectedIndexChanged
    VaporFunc.UpdateCloseTimeFunc()
  End Sub

  Private Sub Lblkronelab_Click(sender As Object, e As EventArgs) Handles Lblkronelab.Click
    VaporFunc.AccessAdminPanelFunc(e)
  End Sub

End Class
