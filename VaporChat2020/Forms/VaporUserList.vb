Public Class VaporUserList
  ReadOnly Vapor As New VaporFunc
  Private Sub VaporUserList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Vapor.ShowUserListFunc()
  End Sub
End Class