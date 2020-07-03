Imports System.ComponentModel

Public Class HideMainScreen
  ReadOnly Vapor As New VaporFunc
  ReadOnly Theme As VaporFunc.Themes = VaporFunc.Themes.Hide


  Private Sub HideMainScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Vapor.FormLoadFunc(Me, txtChat, cmdSendMsg, cmdLogIn, txtMsg, txtUser, lblLog, LblNofUsers, TimerCheckMsg, TimerPubBlock, TimerGUI, TimerAutoCloser, Theme)
  End Sub

  Private Sub CmdLogIn_Click(sender As Object, e As EventArgs) Handles cmdLogIn.Click
    Vapor.LogInFunc()
  End Sub

  Private Sub CmdSendMsg_Click(sender As Object, e As EventArgs) Handles cmdSendMsg.Click
    Vapor.SendMsgFunc()
  End Sub

  Private Sub TimerCheckMsg_Tick(sender As Object, e As EventArgs) Handles TimerCheckMsg.Tick
    Vapor.TimerChkMsgFunc()
  End Sub

  Private Sub TxtMsg_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMsg.KeyDown
    Vapor.MsgBoxKeyDownFunc(e)
  End Sub

  Private Sub TxtUser_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles txtUser.KeyDown
    Vapor.UserBoxKeyDownFunc(e)
  End Sub

  Private Sub HideMainScreen_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
    Vapor.FormKeyDownFunc(e)
  End Sub

  Private Sub HideMainScreenvb_Resize(sender As Object, e As EventArgs) Handles Me.Resize
    If WindowState = FormWindowState.Minimized Then
      Vapor.MinimizeFormFunc(False)
    End If
  End Sub

  Private Sub HideMainScreen_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
    Vapor.ClosingFunc()
  End Sub

  Private Sub TxtChat_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles txtChat.ItemSelectionChanged
    Vapor.ForceSwitchOffFunc()
  End Sub

  Private Sub TimerGUI_Tick(sender As Object, e As EventArgs) Handles TimerGUI.Tick
    Vapor.UpdateGUIFunc()
  End Sub

  Private Sub LblNofUsers_Click(sender As Object, e As EventArgs) Handles LblNofUsers.Click
    Vapor.ShowUserListFunc()
  End Sub

  Private Sub TimerPubBlock_Tick(sender As Object, e As EventArgs) Handles TimerPubBlock.Tick
    Vapor.PubBlockTickFunc()
  End Sub

  Private Sub TxtChat_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles txtChat.MouseClick
    Vapor.CopyItemFunc(e)
  End Sub

  Private Sub VaporMainScreen_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
    Vapor.ForceSwitchOffFunc()
  End Sub

  Private Sub TimerAutoCloser_Tick(sender As Object, e As EventArgs) Handles TimerAutoCloser.Tick
    Vapor.MinimizeFormFunc(True)
  End Sub
End Class