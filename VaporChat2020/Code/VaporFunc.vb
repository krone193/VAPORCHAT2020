Public Class VaporFunc
  Dim WithEvents Notify As New NotifyIcon


  '--- V A P O R F U N C | Declarations ----------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
#Const SHOW_ITSME_MESSAGE = False


  '--- V A P O R F U N C | ReadOnly --------------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
  ReadOnly Vapor As New VaporChat
  ReadOnly CONNSEC As UShort = 5
  ReadOnly MAXROWS As UShort = 30
  ReadOnly NOFCLRS As UShort = 10
  ReadOnly ColorPool = New Color() {Color.Crimson, Color.HotPink, Color.Gold, Color.DarkOrchid, Color.Violet,
                                    Color.DodgerBlue, Color.Teal, Color.Lime, Color.DarkOrange, Color.SpringGreen}
  ReadOnly UserList As New List(Of User)
  ReadOnly BannedText As New List(Of String)


  '--- V A P O R F U N C | Struct ----------------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
  Structure User
    Dim Name As String
    Dim Color As Color
  End Structure


  '--- V A P O R F U N C | Enum ------------------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Enum Themes
    Vapor   ' 0
    Hide    ' 1
    NofElm
  End Enum


  '--- V A P O R F U N C | Variables -------------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
  Private CallerForm As Form
  Private CallerListView As ListView
  Private CallerBtnSend As Button
  Private CallerBtnLogin As Button
  Private CallerTextMessage As TextBox
  Private CallerTextUser As TextBox
  Private CallerLabelLog As Label
  Private CallerLabelUsers As Label
  Private CallerTimCheck As Timer
  Private CallerTimBlock As Timer
  Private CallerTimGui As Timer
  Private CallerTimCloser As Timer
  '-----------------------------------------------------------------------------------------------------------------------'
  Private NofUsers As UShort = 0
  Private ThisTheme As Themes = Themes.NofElm
  Private HideStatus As Boolean = False
  Private Connected As Boolean = False
  Private TaskBarHid As Boolean = False
  Private SwitchText As String
  Private SwitchOn As Boolean = False
  Private SwitchIndex As Short = -1
  Private NofMessages As UShort = 0
  Private AsyncOp As Boolean = False
  Private MessageRxOn As Boolean = False
  Private DotIndex As UShort = 0
  Private ForcePass As Boolean = False


  '--- V A P O R F U N C | Private Functions -----------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub Notify_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles Notify.DoubleClick
    NotifyIconRead()
    ShowFormGest()
    Vapor.Connect(My.Settings.LastUser)
    HideStatus = False
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub ClearTextBox(ByRef box As TextBox)
    box.Text = ""
    box.Focus()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub AddUserToList(ByVal name As String)
    Dim tmpUser As User
    NofUsers += 1
    tmpUser.Name = name
    tmpUser.Color = GetRandomColor()
    UserList.Add(tmpUser)
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub RemoveUserFromList(ByVal user As String)
    Dim index As Short = SearchNameInList(user)
    UserList.RemoveAt(index)
    NofUsers -= 1
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Function GetRandomColor() As Color
    Dim rand As New Random()
    Dim clrs As Color
    Dim reso As Boolean = False
    If NofUsers >= NOFCLRS Then
      clrs = ColorPool(rand.Next(0, NOFCLRS - 1))
    Else
      While reso = False
        clrs = ColorPool(rand.Next(0, NOFCLRS - 1))
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
    Return UserList(index).Color
  End Function
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub HideKeyGest()
    HideStatus = True
    CallerForm.Hide()
    CallerForm.ShowInTaskbar = False
    Notify.Visible = True
    CallerTimCloser.Enabled = False
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub ShowFormGest()
    If ForcePass = True Then
      Dim password As String = InputBox(" ン ウ ハ 何 ベ ", "(っ◔◡◔)っ")
      If password <> VaporChat.PASSCHAT Then
        ClosingFunc()
        CallerForm.Close()
      Else
        CallerForm.Show()
        CallerForm.WindowState = FormWindowState.Normal
        CallerForm.ShowInTaskbar = False
        Notify.Visible = False
        RefreshTimCloserFunc()
        CallerTimCloser.Enabled = True
      End If
    Else
      CallerForm.Show()
      CallerForm.WindowState = FormWindowState.Normal
      CallerForm.ShowInTaskbar = False
      Notify.Visible = False
      RefreshTimCloserFunc()
      CallerTimCloser.Enabled = True
    End If
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub NotifyIconUnread()
    Dim NotifyIcon As New Icon(VaporChat.ICONNMSG)
    Notify.Icon = NotifyIcon
    Notify.Visible = True
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub NotifyIconRead()
    Dim NotifyIcon As New Icon(VaporChat.ICONPATH)
    Notify.Icon = NotifyIcon
    Notify.Visible = True
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub MessageRecvFunc(ByRef chat As ListView, ByRef user As User, ByVal message As String)
    Dim strdata As String = Date.Now().ToString()
    Dim item As New ListViewItem(New String() {user.Name, message, strdata})
    Dim copieditems As ListViewItem

    MessageRxOn = True

    ' Date assign
    Select Case ThisTheme
      Case Themes.Vapor
        item.ForeColor = user.Color
      Case Themes.Hide
        item.ForeColor = SystemColors.WindowText
    End Select

    ' Protect a message if date is displayed
    ForceSwitchOffFunc()

    ' Copy items in list view
    For i As Byte = 0 To MAXROWS - 1
      copieditems = chat.Items(i + 1).Clone
      chat.Items(i) = copieditems
    Next

    ' Add item to ListView
    chat.Items(MAXROWS) = item
    If NofMessages < MAXROWS Then
      NofMessages += 1
    End If

    ' Restore date
    ' TBI

    ' Notify if minimized
    If HideStatus = True Then
      NotifyIconUnread()
    End If

    MessageRxOn = False
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub ConfigRecvFunc()
    Dim struser As String = Vapor.GetConfigUser()
    Dim strtext As String = Vapor.GetConfigText()
    Dim strdata() As String = strtext.Split(":")
    Dim confcommand As String = strdata(0)
    Dim confuserdes As String = strdata(1)

    Vapor.CleanConfigRecv()

    If confuserdes = My.Settings.LastUser Then
      Select Case confcommand
        Case VaporChat.ADMINMUTEU
          My.Settings.Muted = True
          My.Settings.Save()
        Case VaporChat.ADMINUMUTE
          My.Settings.Muted = False
          My.Settings.Save()
        Case VaporChat.ADMINLOBBY
          My.Settings.Lobby = strdata(2)
          My.Settings.Save()
          Vapor.Disconnect()
          Vapor.Connect(My.Settings.LastUser)
      End Select
    End If
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub RefreshTimCloserFunc()
    Static modder As Boolean = True
    If modder = True Then
      modder = False
      CallerTimCloser.Interval = My.Settings.Timeout - 1
    Else
      modder = True
      CallerTimCloser.Interval = My.Settings.Timeout
    End If
  End Sub


  '--- V A P O R F U N C | Public Functions ------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Sub FormLoadFunc(ByRef frame As Form, ByRef chat As ListView, ByRef send As Button, ByRef login As Button, ByRef message As TextBox, ByRef user As TextBox, ByRef log As Label, ByRef nuser As Label, ByRef timmsg As Timer, ByRef timblock As Timer, ByRef timgui As Timer, ByRef timcloser As Timer, ByVal theme As Themes)
    Dim NotifyIcon As New Icon(VaporChat.ICONPATH)
    Control.CheckForIllegalCrossThreadCalls = False
    ' Associations
    CallerForm = frame
    CallerListView = chat
    CallerBtnSend = send
    CallerBtnLogin = login
    CallerTextMessage = message
    CallerTextUser = user
    CallerLabelLog = log
    CallerLabelUsers = nuser
    CallerTimCheck = timmsg
    CallerTimBlock = timblock
    CallerTimGui = timgui
    CallerTimCloser = timcloser
    ThisTheme = theme
    ' Init timers
    CallerTimCheck.Interval = VaporChat.TCHKMSGR
    CallerTimBlock.Interval = VaporChat.TSTOPPUB
    CallerTimGui.Interval = VaporChat.TUPDTGUI
    CallerTimCloser.Interval = My.Settings.Timeout
    ' Init
    NofUsers = 0
    Notify.Icon = NotifyIcon
    Notify.Visible = False
    TaskBarHid = False
    Connected = False
    NofMessages = 0
    BannedText.Add(VaporChat.SEPTCHAR.ToLower().Replace(" ", ""))
    BannedText.Add(VaporChat.JOINVAPO.ToLower().Replace(" ", ""))
    BannedText.Add(VaporChat.LEAVEVAP.ToLower().Replace(" ", ""))
    BannedText.Add(VaporChat.JOINHIDE.ToLower().Replace(" ", ""))
    BannedText.Add(VaporChat.ITSMEMSG.ToLower().Replace(" ", ""))
    CallerTextUser.MaxLength = Vapor.MaxUserLen()
    CallerTextUser.Text = My.Settings.LastUser
    CallerTextUser.Focus()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Sub LogInFunc()
    If CallerBtnLogin.Text = "Log in" Then
      My.Settings.LastUser = CallerTextUser.Text
      My.Settings.Save()
      CallerBtnLogin.Text = "Log out"
      CallerTextUser.Enabled = False
      Vapor.CleanMessageRecv()
      Vapor.CleanConfigRecv()
      CallerTextMessage.MaxLength = Vapor.MaxMessageLen()
      If Vapor.Connect(My.Settings.LastUser) Then
        AsyncOp = True
        Connected = True
        Select Case ThisTheme
          Case VaporFunc.Themes.Vapor
            Vapor.SendMessage(My.Settings.LastUser, VaporChat.JOINVAPO)
          Case VaporFunc.Themes.Hide
            Vapor.SendMessage(My.Settings.LastUser, VaporChat.JOINHIDE)
        End Select
        If SearchNameInList(My.Settings.LastUser) < 0 Then
          AddUserToList(My.Settings.LastUser)
        End If
        CallerLabelLog.Text = VaporChat.LOGNOERR
        CallerTimCheck.Enabled = True
        CallerBtnSend.Enabled = True
        Select Case ThisTheme
          Case Themes.Vapor
            CallerTextMessage.ForeColor = UserList(0).Color
          Case Themes.Hide
        End Select
        ClearTextBox(CallerTextMessage)
      Else
        CallerLabelLog.Text = VaporChat.LOGERROR
      End If
      RefreshTimCloserFunc()
    Else
      LogOutFunc()
    End If
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Sub SendMsgFunc()
    If CallerBtnSend.Enabled = True Then
      CallerBtnSend.Enabled = False
      CallerTimBlock.Enabled = True
      If Connected = True Then
        AsyncOp = True
        If BannedText.Contains(CallerTextMessage.Text.ToLower().Replace(" ", "")) Then
          CallerLabelLog.Text = VaporChat.FUNNYBOI
          ClearTextBox(CallerTextMessage)
        Else
          If Vapor.SendMessage(My.Settings.LastUser, CallerTextMessage.Text) Then
            CallerLabelLog.Text = VaporChat.SENDISOK
            ClearTextBox(CallerTextMessage)
          Else
            CallerLabelLog.Text = VaporChat.LOGERROR
          End If
        End If
      Else
        CallerLabelLog.Text = VaporChat.COMERROR
      End If
    End If
    RefreshTimCloserFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Sub TimerChkMsgFunc()
    If Vapor.CheckMessageRecv() Then
      Dim show As Boolean = True
      Dim user As String = Vapor.GetMessageUser
      Dim message As String = Vapor.GetMessageText()
      Dim color As Color
      Dim index As Short
      Vapor.CleanMessageRecv()

      ' Search for new user
      index = SearchNameInList(user)
      If index < 0 Then
        AddUserToList(user)
        index = SearchNameInList(user)
      End If
      ' Color assign
      color = GetColorAtIndex(index)

      Select Case message
        Case VaporChat.JOINVAPO
          If ThisTheme = Themes.Hide Then
            message = VaporChat.JOINHIDE
          End If
        Case VaporChat.ITSMEMSG
#If SHOW_ITSME_MESSAGE = True Then
          show = True
#Else
          show = False
#End If
      End Select

      ' Message receive function
      If show Then
        MessageRecvFunc(CallerListView, UserList(index), message)
      End If

      ' Check message type for Users management
      If user <> My.Settings.LastUser Then
        Select Case message
          Case VaporChat.LEAVEVAP
            RemoveUserFromList(user)
          Case VaporChat.JOINHIDE
            Vapor.SendMessage(My.Settings.LastUser, VaporChat.ITSMEMSG)
          Case VaporChat.JOINVAPO
            Vapor.SendMessage(My.Settings.LastUser, VaporChat.ITSMEMSG)
        End Select
      End If
    ElseIf Vapor.CheckConfigRecv() Then
        ConfigRecvFunc()
    End If
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Sub MsgBoxKeyDownFunc(ByRef e As KeyEventArgs)
    Select Case e.KeyCode
      Case VaporChat.SENDUKEY
        If CallerBtnSend.Enabled Then
          CallerBtnSend.PerformClick()
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
        If CallerBtnLogin.Enabled Then
          CallerBtnLogin.PerformClick()
        End If
    End Select
    RefreshTimCloserFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Sub LogOutFunc()
    StartScreen.Show()
    CallerForm.Close()
    ClosingFunc()
    HideStatus = True
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Sub FormKeyDownFunc(ByRef e As KeyEventArgs)
    Select Case e.KeyCode
      Case VaporChat.HIDEUKEY
        If My.Computer.Keyboard.CtrlKeyDown Then
          ForcePass = True
        Else
          ForcePass = False
        End If
        HideKeyGest()
          Case VaporChat.SHOWUKEY
        ShowFormGest()
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
      Vapor.SendMessage(My.Settings.LastUser, VaporChat.LEAVEVAP)
      Connected = False
    End If
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Sub ForceSwitchOffFunc()
    If SwitchOn = True And SwitchIndex >= 0 Then
      SwitchOn = False
      CallerListView.Items(SwitchIndex).SubItems(1).Text = SwitchText
    End If
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Sub UpdateGUIFunc()
    CallerLabelUsers.Text = NofUsers.ToString()
    If AsyncOp Then
      If Vapor.GetSubOngoing() Or Vapor.GetPubOngoing() Or Vapor.GetConOngoing() Then
        Select Case DotIndex
          Case 0
            CallerLabelLog.Text = "."
            DotIndex += 1
          Case 1
            CallerLabelLog.Text = ".."
            DotIndex += 1
          Case 2
            CallerLabelLog.Text = "..."
            DotIndex += 1
          Case 3
            CallerLabelLog.Text = "...."
            DotIndex = 0
        End Select
      Else
        AsyncOp = False
        DotIndex = 0
        If CallerLabelLog.Text <> VaporChat.FUNNYBOI Then
          CallerLabelLog.Text = VaporChat.LOGNOERR
        End If
      End If
    Else
      If Vapor.GetSubState() = False Then
        If Vapor.GetSubOngoing() = False Then
          CallerLabelLog.Text = VaporChat.LOGERROR
        End If
      End If
      If Vapor.GetPubState() = False Then
        CallerBtnLogin.Enabled = True
        CallerBtnSend.Enabled = False
        If Vapor.GetSubOngoing() = False And Vapor.GetPubOngoing() = False Then
          If CallerLabelLog.Text <> VaporChat.LOGERROR Then
            CallerLabelLog.Text = VaporChat.SENDISKO
          End If
        End If
      End If
    End If
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Sub ShowUserListFunc()
    Dim namelist As String = ""
    For Each user As User In UserList
      namelist = namelist & user.Name.Trim() & vbCrLf
    Next
    Select Case ThisTheme
      Case Themes.Vapor
        MsgBox(namelist, vbOKOnly, VaporChat.USRBOXVP)
      Case Themes.Hide
        MsgBox(namelist, vbOKOnly, VaporChat.USRBOXHI)
    End Select
    RefreshTimCloserFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Sub PubBlockTickFunc()
    CallerBtnSend.Enabled = True
    CallerTimBlock.Enabled = False
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Sub CopyItemFunc(ByVal e As MouseEventArgs)
    If e.Button = MouseButtons.Left And My.Computer.Keyboard.CtrlKeyDown Then
      If CallerListView.SelectedIndices(0) > MAXROWS - NofMessages Then
        If CallerListView.Items(CallerListView.SelectedIndices(0)).SubItems.Count > 0 Then
          If CallerListView.Items(CallerListView.SelectedIndices(0)).SubItems(1).Text <> "" Then
            Clipboard.SetText(CallerListView.Items(CallerListView.SelectedIndices(0)).SubItems(1).Text)
            CallerListView.Focus()
          End If
        End If
      End If
    ElseIf e.Button = MouseButtons.Right Then
      If MessageRxOn = False Then
        If CallerListView.SelectedIndices(0) > MAXROWS - NofMessages Then
          If SwitchOn = False Then
            SwitchIndex = CallerListView.SelectedIndices(0)
            SwitchText = CallerListView.Items(SwitchIndex).SubItems(1).Text
            CallerListView.Items(SwitchIndex).SubItems(1).Text = CallerListView.Items(SwitchIndex).SubItems(2).Text
            SwitchOn = True
          Else
            CallerListView.Items(SwitchIndex).SubItems(1).Text = SwitchText
            SwitchOn = False
            SwitchIndex = -1
          End If
        End If
      End If
    End If
    RefreshTimCloserFunc()
  End Sub
End Class