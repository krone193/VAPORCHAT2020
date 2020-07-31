Public Class AdminPanel
  ReadOnly Vapor As New VaporFunc
  Private Sub CmdAdminSend_Click(sender As Object, e As EventArgs) Handles CmdAdminSend.Click
    If TxtAdminCommand.Text <> "" Then
      If TxtAdminUser.Text <> "" Then
        Vapor.SendCmdFunc(TxtAdminUser.Text, TxtAdminCommand.Text)
        TxtAdminCommand.Text = ""
        TxtAdminUser.Text = ""
      End If
    End If
  End Sub
End Class