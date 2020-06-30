Public Class StartScreen
  Private Sub CmdHideInPlainSight_Click(sender As Object, e As EventArgs) Handles cmdHideInPlainSight.Click
    If TxtPassword.Text = "" Then
      TxtPassword.Focus()
    ElseIf TxtPassword.Text = VaporChat.PASSCHAT Then
      My.Settings.LastTheme = "Hide"
      My.Settings.Save()
      HideMainScreen.Show()
      Close()
    Else
      Close()
    End If
  End Sub

  Private Sub CmdVapor_Click(sender As Object, e As EventArgs) Handles cmdVapor.Click
    If TxtPassword.Text = "" Then
      TxtPassword.Focus()
    ElseIf TxtPassword.Text = VaporChat.PASSCHAT Then
      My.Settings.LastTheme = "Vapor"
      My.Settings.Save()
      VaporMainScreen.Show()
      Close()
    Else
      Close()
    End If
  End Sub

  Private Sub StartScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    lblVaporChat2020Ver.Text = My.Settings.VaporChat2020Ver
    lblVaporChatVer.Text = My.Settings.VaporChatVer
    lblVaporFuncVer.Text = My.Settings.VaporFuncVer
    TxtPassword.Focus()
    CmbCloserTime.Text = My.Settings.Timeout / 1000
  End Sub

  Private Sub Lblkronelab_Click(sender As Object, e As EventArgs) Handles Lblkronelab.Click
    If My.Computer.Keyboard.CtrlKeyDown And
      My.Computer.Keyboard.ShiftKeyDown And
      My.Computer.Keyboard.AltKeyDown And
      My.Computer.Keyboard.CapsLock Then
      Dim password As String = InputBox("You shall insert a passcode:")
      If password = VaporChat.ADMINPASSW Then
        AdminPanel.Show()
        Close()
      End If
    End If
  End Sub

  Private Sub StartScreen_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
    If e.KeyValue = Keys.Enter Then
      Select Case My.Settings.LastTheme
        Case "Vapor"
          cmdVapor.PerformClick()
        Case "Hide"
          cmdHideInPlainSight.PerformClick()
        Case Else
      End Select
    Else
      TxtPassword.Focus()
    End If
  End Sub

  Private Sub CmbCloserTime_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbCloserTime.SelectedIndexChanged
    My.Settings.Timeout = CmbCloserTime.Text * 1000
    My.Settings.Save()
  End Sub
End Class


