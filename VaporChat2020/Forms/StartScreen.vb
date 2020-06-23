Public Class StartScreen
  Private Sub CmdHideInPlainSight_Click(sender As Object, e As EventArgs) Handles cmdHideInPlainSight.Click
    HideMainScreen.Show()
    Close()
  End Sub

  Private Sub CmdVapor_Click(sender As Object, e As EventArgs) Handles cmdVapor.Click
    VaporMainScreen.Show()
    Close()
  End Sub

  Private Sub StartScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    lblVaporChat2020Ver.Text = My.Settings.VaporChat2020Ver
    lblVaporChatVer.Text = My.Settings.VaporChatVer
    lblVaporFuncVer.Text = My.Settings.VaporFuncVer
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
End Class


