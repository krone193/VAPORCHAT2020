Imports System.ComponentModel

Public Class MainScreen
  Dim WithEvents Notify As New NotifyIcon
  ReadOnly Vapor As New VaporChat



  '-----------------------------------------------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
  '------------------------ V A P O R C H A T | V A P O R F U N C  S Y S T E M  F U N C T I O N S ------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'

  '--- V A P O R F U N C | Declarations ----------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
#Const SHOW_ITSME_MESSAGE = False


  '--- V A P O R F U N C | ReadOnly --------------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
  ReadOnly CONNSEC As UShort = 5
  ReadOnly NOFCLRS As UShort = 10
  ReadOnly ColorPool = New Color() {Color.Crimson, Color.HotPink, Color.Gold, Color.DarkOrchid, Color.Violet,
                                    Color.DodgerBlue, Color.Teal, Color.Lime, Color.DarkOrange, Color.SpringGreen}
  ReadOnly MAXROWS As UShort = 30
  ReadOnly UserList As New List(Of User)
  ReadOnly BannedText As New List(Of String)


  '--- V A P O R F U N C | Struct ----------------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
  Structure User
    Dim Name As String
    Dim Color As Color
  End Structure


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
    If HideStatus = True Then
      HideStatus = False
      NotifyIconRead()
      ShowFormGest()
      If ForcePass Then
        Size = New Size(VaporChat.PASSWIDTH, VaporChat.PASSHEIGH)
        TxtInsertPass.BackColor = CurrentGUI.TxtUser.BackColor
        TxtInsertPass.ForeColor = CurrentGUI.TxtUser.TextColor
        PnlInsertPass.BringToFront()
        TxtInsertPass.Focus()
      End If
    End If
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
    CallerForm.Show()
    CallerForm.WindowState = FormWindowState.Normal
    CallerForm.ShowInTaskbar = False
    Notify.Visible = False
    RefreshTimCloserFunc()
    CallerTimCloser.Enabled = True
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
  Private Sub MessageRecvFunc()
    Dim strdata As String = Date.Now().ToString()
    Dim copieditems As ListViewItem
    Dim show As Boolean = True
    Dim name As String = ""
    Dim message As String = ""
    Dim color As Color
    Dim index As Short
    Vapor.GetMessageUserAndText(name, message)
    Dim item As New ListViewItem(New String() {name, message, strdata})

    index = SearchNameInList(name) ' Search for new user
    If index < 0 Then
      AddUserToList(name)
      index = SearchNameInList(name)
    End If

    color = GetColorAtIndex(index) ' Color assign

    Select Case message
      Case VaporChat.JOINVAPO
        If CurrentTheme = VaporChat.Themes.Hide Then
          message = VaporChat.JOINHIDE
        End If
      Case VaporChat.ITSMEMSG
        show = False
    End Select

    MessageRxOn = True
    If show = True Then
      ' Date assign
      Select Case CurrentTheme
        Case VaporChat.Themes.Vapor
          item.ForeColor = color
        Case VaporChat.Themes.Hide
          item.ForeColor = SystemColors.WindowText
      End Select

      ' Protect a message if date is displayed
      ForceSwitchOffFunc()

      ' Copy items in list view
      For i As Byte = 0 To MAXROWS - 1
        copieditems = CallerListView.Items(i + 1).Clone
        CallerListView.Items(i) = copieditems
      Next

      ' Add item to ListView
      CallerListView.Items(MAXROWS) = item
      If NofMessages < MAXROWS Then
        NofMessages += 1
      End If

      ' Notify if minimized
      If HideStatus = True Then
        NotifyIconUnread()
      End If
    End If

    ' Check message type for Users management
    If name <> My.Settings.LastUser Then
      Select Case message
        Case VaporChat.LEAVEVAP
          RemoveUserFromList(name)
        Case VaporChat.JOINHIDE
          Vapor.SendMessage(My.Settings.LastUser, VaporChat.ITSMEMSG)
        Case VaporChat.JOINVAPO
          Vapor.SendMessage(My.Settings.LastUser, VaporChat.ITSMEMSG)
      End Select
    End If

    MessageRxOn = False
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub ConfigRecvFunc()
    Dim struser As String = ""
    Dim strtext As String = ""

    Vapor.GetConfigUserAndText(struser, strtext)

    Dim strdata() As String = strtext.Split(":")
    Dim confcommand As String = strdata(0)

    If struser = My.Settings.LastUser Then
      Select Case confcommand
        Case VaporChat.ADMINMUTEU
          My.Settings.Muted = True
          My.Settings.Save()
        Case VaporChat.ADMINUMUTE
          My.Settings.Muted = False
          My.Settings.Save()
        Case VaporChat.ADMINLOBBY
          My.Settings.Lobby = strdata(1)
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
  Public Sub FormLoadFunc(ByRef frame As Form, ByRef chat As ListView, ByRef send As Button, ByRef login As Button, ByRef message As TextBox, ByRef user As TextBox, ByRef log As Label, ByRef nuser As Label, ByRef timmsg As Timer, ByRef timblock As Timer, ByRef timgui As Timer, ByRef timcloser As Timer)
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
      CallerTextMessage.MaxLength = Vapor.MaxMessageLen()
      If Vapor.Connect(My.Settings.LastUser) Then
        AsyncOp = True
        Connected = True
        Select Case CurrentTheme
          Case VaporChat.Themes.Vapor
            Vapor.SendMessage(My.Settings.LastUser, VaporChat.JOINVAPO)
          Case VaporChat.Themes.Hide
            Vapor.SendMessage(My.Settings.LastUser, VaporChat.JOINHIDE)
        End Select
        If SearchNameInList(My.Settings.LastUser) < 0 Then
          AddUserToList(My.Settings.LastUser)
        End If
        CallerLabelLog.Text = VaporChat.LOGNOERR
        CallerTimCheck.Enabled = True
        CallerBtnSend.Enabled = True
        Select Case CurrentTheme
          Case VaporChat.Themes.Vapor
            CallerTextMessage.ForeColor = UserList(0).Color
          Case VaporChat.Themes.Hide
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
    If My.Settings.Muted = False Then
      If CallerBtnSend.Enabled = True Then
        CallerBtnSend.Enabled = False
        CallerTimBlock.Enabled = True
        If Connected = True Then
          AsyncOp = True
          If BannedText.Contains(CallerTextMessage.Text.ToLower().Replace(" ", "")) Then
            CallerLabelLog.Text = VaporChat.FUNNYBOI
            ClearTextBox(CallerTextMessage)
          ElseIf CallerTextMessage.Text = VaporChat.TOKIDRIFT Then
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
    Else
      CallerLabelLog.Text = VaporChat.BLOCKEDU
    End If
    RefreshTimCloserFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Sub SendCmdFunc(ByVal user As String, ByVal command As String)
    Vapor.SendConfig(user, command)
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Sub TimerChkMsgFunc()
    While Vapor.CheckMessageRecv()
      MessageRecvFunc()
    End While
    While Vapor.CheckConfigRecv()
      ConfigRecvFunc()
    End While
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
        If My.Computer.Keyboard.AltKeyDown Then
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
      If CurrentTheme <> VaporChat.Themes.Admin Then
        Vapor.SendMessage(My.Settings.LastUser, VaporChat.LEAVEVAP)
      End If
      Vapor.Disconnect()
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
        If Vapor.GetSubOngoing() = False And Vapor.GetPubOngoing() = False Then
          If CallerLabelLog.Text <> VaporChat.LOGERROR Then
            CallerLabelLog.Text = VaporChat.SENDISKO
            CallerBtnSend.Enabled = True
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
    Select Case CurrentTheme
      Case VaporChat.Themes.Vapor
        MsgBox(namelist, vbOKOnly, VaporChat.USRBOXVP)
      Case VaporChat.Themes.Hide
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



  '-----------------------------------------------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
  '-------------------------- V A P O R C H A T | G R A P H I C A L  U S E R  I N T E R F A C E --------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'

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


  '--- V A P O R G U I | Themes constants --------------------------------------------------------------------------------'
  ' V A P O R C H A T 2 0 2 0 T H E M E ----------------------------------------------------------------------------------'
  ReadOnly VAPOR_MAINWINTXT As String = "(っ◔◡◔)っ 【 ﻿Ｖ　Ａ　Ｐ　Ｏ　Ｒ　Ｃ　Ｈ　Ａ　Ｔ 】 (っ◔◡◔)っ"
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
  ' H I D E C H A T 2 0 2 0 T H E M E ------------------------------------------------------------------------------------'
  ReadOnly HIDE_MAINWINTXT As String = "IOT demo service"
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
  ' A D M I N P A N E L T H E M E ----------------------------------------------------------------------------------------'
  ReadOnly ADMIN_MAINWINTXT As String = "Λ░Ｄ░Ｍ░Ｉ░Ｎ░Ｐ░Λ░Ｎ░Ξ░Ｌ"


  '--- V A P O R G U I | Variables ---------------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
  Private CurrentGUI As ChatTheme
  Private CurrentTheme As VaporChat.Themes


  ' V A P O R G U I | Functions ------------------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub VapoMainScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Select Case My.Settings.LastTheme
      Case VaporChat.Themes.Vapor
        CurrentTheme = VaporChat.Themes.Vapor
        CurrentGUI.MainWinTxt = VAPOR_MAINWINTXT
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
      Case VaporChat.Themes.Hide
        CurrentTheme = VaporChat.Themes.Hide
        CurrentGUI.MainWinTxt = HIDE_MAINWINTXT
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
      Case VaporChat.Themes.Admin
        CurrentTheme = VaporChat.Themes.Admin
        CurrentGUI.MainWinTxt = ADMIN_MAINWINTXT
    End Select
    Text = CurrentGUI.MainWinTxt
    PnlVaporChat.BackgroundImage = CurrentGUI.BackImage
    PnlVaporChat.BackColor = CurrentGUI.BackColor
    PnlInsertPass.BackgroundImage = CurrentGUI.BackImage
    PnlInsertPass.BackColor = CurrentGUI.BackColor
    LstChatVapo.BackColor = CurrentGUI.LstChat.BackColor
    LstChatVapo.ForeColor = CurrentGUI.LstChat.TextColor
    For i = 0 To MAXROWS
      LstChatVapo.Items.Item(i).BackColor = CurrentGUI.LstChat.BackColor
      LstChatVapo.Items.Item(i).ForeColor = CurrentGUI.LstChat.TextColor
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

    FormLoadFunc(Me, LstChatVapo, BtnSend, BtnLogIn, TxtMsg, TxtUser, LblLog, LblUsers, TimerCheckMsg, TimerPubBlock, TimerGUI, TimerAutoCloser)
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub CmdLogIn_Click(sender As Object, e As EventArgs) Handles BtnLogIn.Click
    LogInFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub CmdSendMsg_Click(sender As Object, e As EventArgs) Handles BtnSend.Click
    SendMsgFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub TimerCheckMsg_Tick(sender As Object, e As EventArgs) Handles TimerCheckMsg.Tick
    TimerChkMsgFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub TxtMsg_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtMsg.KeyDown
    MsgBoxKeyDownFunc(e)
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub TxtUser_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtUser.KeyDown
    UserBoxKeyDownFunc(e)
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub VapoMainScreenvb_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
    FormKeyDownFunc(e)
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub VapoMainScreenvb_Resize(sender As Object, e As EventArgs) Handles Me.Resize
    If WindowState = FormWindowState.Minimized Then
      MinimizeFormFunc(False)
    End If
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub VapoMainScreenvb_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
    ClosingFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub TxtChat_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles LstChatVapo.ItemSelectionChanged
    ForceSwitchOffFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub TimerGUI_Tick(sender As Object, e As EventArgs) Handles TimerGUI.Tick
    UpdateGUIFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub LblNofUsers_Click(sender As Object, e As EventArgs) Handles LblUsers.Click
    ShowUserListFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub TimerPubBlock_Tick(sender As Object, e As EventArgs) Handles TimerPubBlock.Tick
    PubBlockTickFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub TxtChat_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles LstChatVapo.MouseClick
    CopyItemFunc(e)
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub VaporMainScreen_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
    ForceSwitchOffFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub TimerAutoCloser_Tick(sender As Object, e As EventArgs) Handles TimerAutoCloser.Tick
    MinimizeFormFunc(True)
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub TxtInsertPass_TextChanged(sender As Object, e As EventArgs) Handles TxtInsertPass.TextChanged
    If TxtInsertPass.Text = VaporChat.PASSCHAT Then
      TxtInsertPass.Text = ""
      Select Case CurrentTheme
        Case VaporChat.Themes.Vapor
          PnlVaporChat.BringToFront()
          Size = New Size(VaporChat.CHATWIDTH, VaporChat.CHATHEIGH)
        Case VaporChat.Themes.Hide
          PnlVaporChat.BringToFront()
          Size = New Size(VaporChat.CHATWIDTH, VaporChat.CHATHEIGH)
        Case VaporChat.Themes.Admin
          PnlAdmin.BringToFront()
          Size = New Size(VaporChat.ADMNWIDTH, VaporChat.ADMNHEIGH)
      End Select
      ForcePass = False
    End If
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub BtnAdminSend_Click(sender As Object, e As EventArgs) Handles BtnAdminSend.Click
    If TxtAdminCommand.Text <> "" Then
      If TxtAdminUser.Text <> "" Then
        If Vapor.Connect("V A P O R A D M I N") Then
          SendCmdFunc(TxtAdminUser.Text, TxtAdminCommand.Text)
          TxtAdminCommand.Text = ""
          TxtAdminUser.Text = ""
        End If
      End If
    End If
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub BtnAdminBackToStart_Click(sender As Object, e As EventArgs) Handles BtnAdminBackToStart.Click
    LogOutFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub BtnBackToStart_Click(sender As Object, e As EventArgs) Handles BtnBackToStart.Click
    LogOutFunc()
  End Sub
End Class