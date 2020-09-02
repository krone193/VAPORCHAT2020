Imports System.ComponentModel


Public Class MainScreen
  '--- Class Imports -----------------------------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
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
  Private WithEvents Notify As New NotifyIcon
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
  Private UsersListOn As Boolean = False


  '--- V A P O R F U N C | Private Functions -----------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub Notify_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles Notify.DoubleClick
    If HideStatus = True Then
      HideStatus = False
      NotifyIconRead()
      ShowFormGest()
      If ForcePass Then
        Size = New Size(VaporChat.PASSWIDTH, VaporChat.PASSHEIGH)
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
    If NofUsers >= Vapor.NOFCLRS Then
      clrs = Vapor.ColorPool(rand.Next(0, Vapor.NOFCLRS - 1))
    Else
      While reso = False
        clrs = Vapor.ColorPool(rand.Next(0, Vapor.NOFCLRS - 1))
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
    Hide()
    ShowInTaskbar = False
    Notify.Visible = True
    TimerAutoCloser.Enabled = False
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub ShowFormGest()
    Show()
    WindowState = FormWindowState.Normal
    ShowInTaskbar = False
    Notify.Visible = False
    RefreshTimCloserFunc()
    TimerAutoCloser.Enabled = True
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
      For i As Byte = 0 To VaporChat.MAXROWS - 1
        copieditems = LstChatVapo.Items(i + 1).Clone
        LstChatVapo.Items(i) = copieditems
      Next

      ' Add item to ListView
      LstChatVapo.Items(VaporChat.MAXROWS) = item
      If NofMessages < VaporChat.MAXROWS Then
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
    Dim strdata() As String = strtext.Split(VaporChat.ADMINSPLIT)
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
      TimerAutoCloser.Interval = My.Settings.Timeout - 1
    Else
      modder = True
      TimerAutoCloser.Interval = My.Settings.Timeout
    End If
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub FormLoadFunc(ByRef frame As Form, ByRef chat As ListView, ByRef send As Button, ByRef login As Button, ByRef message As TextBox, ByRef user As TextBox, ByRef log As Label, ByRef nuser As Label, ByRef timmsg As Timer, ByRef timblock As Timer, ByRef timgui As Timer, ByRef timcloser As Timer)
    Dim NotifyIcon As New Icon(VaporChat.ICONPATH)
    Control.CheckForIllegalCrossThreadCalls = False
    ' Init timers 
    TimerCheckMsg.Interval = VaporChat.TCHKMSGR
    TimerPubBlock.Interval = VaporChat.TSTOPPUB
    TimerGUI.Interval = VaporChat.TUPDTGUI
    TimerAutoCloser.Interval = My.Settings.Timeout
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
    TxtUser.MaxLength = Vapor.MaxUserLen()
    TxtUser.Text = My.Settings.LastUser
    TxtUser.Focus()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub LogInFunc()
    If BtnLogIn.Text = VaporChat.LOGINBTXT Then
      My.Settings.LastUser = TxtUser.Text
      My.Settings.Save()
      BtnLogIn.Text = VaporChat.LOGOUBTXT
      TxtUser.Enabled = False
      TxtMsg.MaxLength = Vapor.MaxMessageLen()
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
        LblLog.Text = VaporChat.LOGNOERR
        TimerCheckMsg.Enabled = True
        BtnSend.Enabled = True
        Select Case CurrentTheme
          Case VaporChat.Themes.Vapor
            TxtMsg.ForeColor = UserList(0).Color
          Case VaporChat.Themes.Hide
        End Select
        ClearTextBox(TxtMsg)
      Else
        LblLog.Text = VaporChat.LOGERROR
      End If
      RefreshTimCloserFunc()
    Else
      LogOutFunc()
    End If
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub SendMsgFunc()
    If TxtMsg.Text = VaporChat.TOKIDRIFT Then
      ClearTextBox(TxtMsg)
    End If
    If My.Settings.Muted = False Then
      If BtnSend.Enabled = True Then
        BtnSend.Enabled = False
        TimerPubBlock.Enabled = True
        If Connected = True Then
          AsyncOp = True
          If BannedText.Contains(TxtMsg.Text.ToLower().Replace(" ", "")) Then
            LblLog.Text = VaporChat.FUNNYBOI
            ClearTextBox(TxtMsg)
          ElseIf TxtMsg.Text = VaporChat.TOKIDRIFT Then
            ClearTextBox(TxtMsg)
          Else
            If Vapor.SendMessage(My.Settings.LastUser, TxtMsg.Text) Then
              LblLog.Text = VaporChat.SENDISOK
              ClearTextBox(TxtMsg)
            Else
              LblLog.Text = VaporChat.LOGERROR
            End If
          End If
        Else
          LblLog.Text = VaporChat.COMERROR
        End If
      End If
    Else
      LblLog.Text = VaporChat.BLOCKEDU
    End If
    RefreshTimCloserFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub SendCmdFunc(ByVal user As String, ByVal command As String)
    Vapor.SendConfig(user, command)
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub TimerChkMsgFunc()
    While Vapor.CheckMessageRecv()
      MessageRecvFunc()
    End While
    While Vapor.CheckConfigRecv()
      ConfigRecvFunc()
    End While
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub MsgBoxKeyDownFunc(ByRef e As KeyEventArgs)
    Select Case e.KeyCode
      Case VaporChat.SENDUKEY
        If BtnSend.Enabled Then
          BtnSend.PerformClick()
          e.Handled = True
          e.SuppressKeyPress = True
        End If
    End Select
    RefreshTimCloserFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub UserBoxKeyDownFunc(ByRef e As KeyEventArgs)
    Select Case e.KeyCode
      Case VaporChat.SENDUKEY
        If BtnLogIn.Enabled Then
          BtnLogIn.PerformClick()
        End If
    End Select
    RefreshTimCloserFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub LogOutFunc()
    StartScreen.Show()
    Close()
    ClosingFunc()
    HideStatus = True
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub FormKeyDownFunc(ByRef e As KeyEventArgs)
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
  Private Sub MinimizeFormFunc(ByVal forcepassword As Boolean)
    ForcePass = forcepassword
    HideKeyGest()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub ClosingFunc()
    If Connected = True Then
      If CurrentTheme <> VaporChat.Themes.Admin Then
        Vapor.SendMessage(My.Settings.LastUser, VaporChat.LEAVEVAP)
      End If
      Vapor.Disconnect()
      Connected = False
    End If
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub ForceSwitchOffFunc()
    If SwitchOn = True And SwitchIndex >= 0 Then
      SwitchOn = False
      LstChatVapo.Items(SwitchIndex).SubItems(1).Text = SwitchText
    End If
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub UpdateGUIFunc()
    LblUsers.Text = NofUsers.ToString()
    If AsyncOp Then
      If Vapor.GetSubOngoing() Or Vapor.GetPubOngoing() Or Vapor.GetConOngoing() Then
        Select Case DotIndex
          Case 0
            LblLog.Text = VaporChat.LOGPROG01
            DotIndex += 1
          Case 1
            LblLog.Text = VaporChat.LOGPROG02
            DotIndex += 1
          Case 2
            LblLog.Text = VaporChat.LOGPROG03
            DotIndex = 0
        End Select
      Else
        AsyncOp = False
        DotIndex = 0
        If LblLog.Text <> VaporChat.FUNNYBOI Then
          LblLog.Text = VaporChat.LOGNOERR
        End If
      End If
    Else
      If Vapor.GetSubState() = False Then
        If Vapor.GetSubOngoing() = False Then
          LblLog.Text = VaporChat.LOGERROR
          BtnLogIn.Text = VaporChat.LOGINBTXT
        End If
      End If
      If Vapor.GetPubState() = False Then
        If Vapor.GetSubOngoing() = False And Vapor.GetPubOngoing() = False Then
          If LblLog.Text <> VaporChat.LOGERROR Then
            LblLog.Text = VaporChat.SENDISKO
            BtnSend.Enabled = True
          End If
        End If
      End If
    End If
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub ShowUserListFunc()
    UsersListOn = True
    Select Case CurrentTheme
      Case VaporChat.Themes.Vapor
        StsUsersList.Text = VaporChat.USRBOXVP
      Case VaporChat.Themes.Hide
        StsUsersList.Text = VaporChat.USRBOXHI
    End Select
    PnlUsersList.BringToFront()
    LstUsersList.Clear()
    For Each user As User In UserList
      If CurrentTheme = VaporChat.Themes.Vapor Then
        Dim item As New ListViewItem(New String() {user.Name}) With {.ForeColor = user.Color}
        LstUsersList.Items.Add(item)
      Else
        Dim item As New ListViewItem(New String() {user.Name}) With {.ForeColor = Vapor.HIDE_CHATFRTCLR}
        LstUsersList.Items.Add(item)
      End If
    Next
    RefreshTimCloserFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub PubBlockTickFunc()
    BtnSend.Enabled = True
    TimerPubBlock.Enabled = False
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub CopyItemFunc(ByRef objLst As ListView)
    Select Case objLst.Name
      Case LstChatVapo.Name
        If objLst.SelectedIndices(0) > VaporChat.MAXROWS - NofMessages Then
          If objLst.Items(objLst.SelectedIndices(0)).SubItems.Count > 0 Then
            If objLst.Items(objLst.SelectedIndices(0)).SubItems(1).Text <> "" Then
              Clipboard.Clear()
              Clipboard.SetText(objLst.Items(objLst.SelectedIndices(0)).SubItems(1).Text)
            End If
          End If
        End If
      Case LstUsersList.Name
        If objLst.Items(objLst.SelectedIndices(0)).SubItems.Count > 0 Then
          If objLst.Items(objLst.SelectedIndices(0)).SubItems(0).Text <> "" Then
            Clipboard.Clear()
            Clipboard.SetText(objLst.Items(objLst.SelectedIndices(0)).SubItems(0).Text)
            objLst.Focus()
          End If
        End If
    End Select
    RefreshTimCloserFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub ShowDateFunc(ByRef objLst As ListView)
    Select Case objLst.Name
      Case LstChatVapo.Name
        If MessageRxOn = False Then
          If objLst.SelectedIndices(0) > VaporChat.MAXROWS - NofMessages Then
            If SwitchOn = False Then
              SwitchIndex = objLst.SelectedIndices(0)
              SwitchText = objLst.Items(SwitchIndex).SubItems(1).Text
              objLst.Items(SwitchIndex).SubItems(1).Text = objLst.Items(SwitchIndex).SubItems(2).Text
              SwitchOn = True
            Else
              objLst.Items(SwitchIndex).SubItems(1).Text = SwitchText
              SwitchOn = False
              SwitchIndex = -1
            End If
          End If
        End If
      Case LstUsersList.Name

    End Select
    RefreshTimCloserFunc()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub ItemMouseUpFunc(ByRef item_mousedown As Boolean)
    item_mousedown = False
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub MoveItemFunc(ByRef item As Object, ByRef item_mousedown As Boolean, ByRef item_mousepos As Point, e As MouseEventArgs)
    If e.Button = MouseButtons.Left Then
      If item_mousedown = False Then
        item_mousedown = True
        item_mousepos = New Point(e.X, e.Y)
      End If
      item.Location = New Point(item.Location.X + e.X - item_mousepos.X, item.Location.Y + e.Y - item_mousepos.Y)
    End If
  End Sub


  '--- V A P O R F U N C | Public Functions ------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'



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
  Private CurrentTheme As VaporChat.Themes
  Private PNLUSR_MouseIsDown As Boolean = False
  Private PNLUSR_MouseIsDownLoc As Point = Nothing


  ' V A P O R G U I | Functions ------------------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub VapoMainScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    PnlUsersList.SendToBack()
    Select Case My.Settings.LastTheme
      Case VaporChat.Themes.Vapor
        CurrentTheme = VaporChat.Themes.Vapor
        Text = Vapor.VAPOR_MAINWINTXT
        PnlVaporChat.BackgroundImage = Vapor.VAPOR_MAINBCKIMG
        PnlVaporChat.BackColor = Vapor.VAPOR_MAINBCKCLR
        PnlInsertPass.BackgroundImage = Vapor.VAPOR_MAINBCKIMG
        PnlInsertPass.BackColor = Vapor.VAPOR_MAINBCKCLR
        TxtInsertPass.BackColor = Vapor.VAPOR_USERBCKCLR
        TxtInsertPass.ForeColor = Vapor.VAPOR_USERFRTCLR
        LstChatVapo.BackColor = Vapor.VAPOR_CHATBCKCLR
        LstChatVapo.ForeColor = Vapor.VAPOR_CHATFRTCLR
        For i = 0 To VaporChat.MAXROWS
          LstChatVapo.Items.Item(i).BackColor = Vapor.VAPOR_CHATBCKCLR
          LstChatVapo.Items.Item(i).ForeColor = Vapor.VAPOR_CHATFRTCLR
        Next
        TxtUser.BackColor = Vapor.VAPOR_USERBCKCLR
        TxtUser.ForeColor = Vapor.VAPOR_USERFRTCLR
        TxtMsg.BackColor = Vapor.VAPOR_SENDBCKCLR
        TxtMsg.ForeColor = Vapor.VAPOR_SENDFRTCLR
        DskLblLogs.Text = Vapor.VAPOR_LBLLOGFTXT
        DskLblLogs.ForeColor = Vapor.VAPOR_LBLLOGFCLR
        LblLog.ForeColor = Vapor.VAPOR_LBLLOGVCLR
        DskLblUsers.Text = Vapor.VAPOR_LBLUSRFTXT
        DskLblUsers.ForeColor = Vapor.VAPOR_LBLUSRFCLR
        LblUsers.ForeColor = Vapor.VAPOR_LBLUSRVCLR
        BtnLogIn.FlatStyle = Vapor.VAPOR_BTNFLSTYLE
        BtnLogIn.BackColor = Vapor.VAPOR_BTNLOGBCLR
        BtnLogIn.ForeColor = Vapor.VAPOR_BTNLOGFCLR
        BtnSend.FlatStyle = Vapor.VAPOR_BTNFLSTYLE
        BtnSend.BackColor = Vapor.VAPOR_BTNSNDBCLR
        BtnSend.ForeColor = Vapor.VAPOR_BTNSNDFCLR
        BtnBackToStart.FlatStyle = Vapor.VAPOR_BTNFLSTYLE
        BtnBackToStart.BackColor = Vapor.VAPOR_BTNBCKBCLR
        BtnBackToStart.ForeColor = Vapor.VAPOR_BTNBCKFCLR
        LstUsersList.BackColor = Vapor.VAPOR_USRLSTBCLR
      Case VaporChat.Themes.Hide
        CurrentTheme = VaporChat.Themes.Hide
        Text = Vapor.HIDE_MAINWINTXT
        PnlVaporChat.BackgroundImage = Vapor.HIDE_MAINBCKIMG
        PnlVaporChat.BackColor = Vapor.HIDE_MAINBCKCLR
        PnlInsertPass.BackgroundImage = Vapor.HIDE_MAINBCKIMG
        PnlInsertPass.BackColor = Vapor.HIDE_MAINBCKCLR
        TxtInsertPass.BackColor = Vapor.HIDE_USERBCKCLR
        TxtInsertPass.ForeColor = Vapor.HIDE_USERFRTCLR
        LstChatVapo.BackColor = Vapor.HIDE_CHATBCKCLR
        LstChatVapo.ForeColor = Vapor.HIDE_CHATFRTCLR
        For i = 0 To VaporChat.MAXROWS
          LstChatVapo.Items.Item(i).BackColor = Vapor.HIDE_CHATBCKCLR
          LstChatVapo.Items.Item(i).ForeColor = Vapor.HIDE_CHATFRTCLR
        Next
        TxtUser.BackColor = Vapor.HIDE_USERBCKCLR
        TxtUser.ForeColor = Vapor.HIDE_USERFRTCLR
        TxtMsg.BackColor = Vapor.HIDE_SENDBCKCLR
        TxtMsg.ForeColor = Vapor.HIDE_SENDFRTCLR
        DskLblLogs.Text = Vapor.HIDE_LBLLOGFTXT
        DskLblLogs.ForeColor = Vapor.HIDE_LBLLOGFCLR
        LblLog.ForeColor = Vapor.HIDE_LBLLOGVCLR
        DskLblUsers.Text = Vapor.HIDE_LBLUSRFTXT
        DskLblUsers.ForeColor = Vapor.HIDE_LBLUSRFCLR
        LblUsers.ForeColor = Vapor.HIDE_LBLUSRVCLR
        BtnLogIn.FlatStyle = Vapor.HIDE_BTNFLSTYLE
        BtnLogIn.BackColor = Vapor.HIDE_BTNLOGBCLR
        BtnLogIn.ForeColor = Vapor.HIDE_BTNLOGFCLR
        BtnSend.FlatStyle = Vapor.HIDE_BTNFLSTYLE
        BtnSend.BackColor = Vapor.HIDE_BTNSNDBCLR
        BtnSend.ForeColor = Vapor.HIDE_BTNSNDFCLR
        BtnBackToStart.FlatStyle = Vapor.HIDE_BTNFLSTYLE
        BtnBackToStart.BackColor = Vapor.HIDE_BTNBCKBCLR
        BtnBackToStart.ForeColor = Vapor.HIDE_BTNBCKFCLR
        LstUsersList.BackColor = Vapor.HIDE_USRLSTBCLR
      Case VaporChat.Themes.Admin
        CurrentTheme = VaporChat.Themes.Admin
        Text = Vapor.ADMIN_MAINWINTXT
    End Select

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
  Private Sub LstChatVapo_MouseClick(sender As Object, e As MouseEventArgs) Handles LstChatVapo.MouseClick
    If e.Button = MouseButtons.Left And My.Computer.Keyboard.CtrlKeyDown Then
      CopyItemFunc(LstChatVapo)
    ElseIf e.Button = MouseButtons.Right Then
      ShowDateFunc(LstChatVapo)
    End If
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub LstUsersList_MouseClick(sender As Object, e As MouseEventArgs) Handles LstUsersList.MouseClick
    If e.Button = MouseButtons.Left And My.Computer.Keyboard.CtrlKeyDown Then
      CopyItemFunc(LstUsersList)
    End If
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
        If Vapor.Connect(VaporChat.ADMINUNAME) Then
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
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub BtnCloseUsersList_Click(sender As Object, e As EventArgs) Handles BtnCloseUsersList.Click
    PnlUsersList.SendToBack()
    UsersListOn = False
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub StsUsersList_MouseMove(sender As Object, e As MouseEventArgs) Handles StsUsersList.MouseMove
    MoveItemFunc(PnlUsersList, PNLUSR_MouseIsDown, PNLUSR_MouseIsDownLoc, e)
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Sub StsUsersList_MouseUp(sender As Object, e As MouseEventArgs) Handles StsUsersList.MouseUp
    ItemMouseUpFunc(PNLUSR_MouseIsDown)
  End Sub
End Class
