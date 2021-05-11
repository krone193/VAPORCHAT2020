Imports MQTTnet
Imports MQTTnet.Client
Imports MQTTnet.Client.Options
Imports MQTTnet.Client.Disconnecting
Imports MQTTnet.Client.Connecting
Imports MQTTnet.Client.Subscribing
Imports MQTTnet.Client.Publishing


Public Class VaporChat
	'--- V A P O R C H A T | Implementation --------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	Private ReadOnly Factory As New MqttFactory
	Private WithEvents MqttClient As MqttClient


	'--- V A P O R C H A T | Declarations ----------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
#Const VAPORCHAT_SWVER = "3.0.1.0"
#Const USE_SERVER = "MOSQUITTO"


	'--- V A P O R C H A T | Private Constants -----------------------------------------------------------------------------'
	' Chat -----------------------------------------------------------------------------------------------------------------'
	Private Const MAXMSGCH As UShort = 64
	Private Const MAXUSRCH As UShort = 10
	' MQTT -----------------------------------------------------------------------------------------------------------------'
	Private Const MQTTROOT As String = "kronelab/vaporchat/"
	Private Const MQTTCONF As String = "conf/"
	Private Const TIMPINGS As UShort = 60
	Private Const TSESSION As ULong = 604800
	Private Const TMESSAGE As ULong = 604800
#If USE_SERVER = "HIVEMQ" Then
  Private Const MQTTHOST As String = "broker.hivemq.com"
  Private Const MQTTUSER As String = ""
  Private Const MQTTPASS As String = ""
  Private Const MQTTPORT As UShort = 1883
	Private Const MQTTQOFS As Protocol.MqttQualityOfServiceLevel = Protocol.MqttQualityOfServiceLevel.AtMostOnce
#ElseIf USE_SERVER = "MOSQUITTO" Then
	Private Const MQTTHOST As String = "test.mosquitto.org"
	Private Const MQTTUSER As String = ""
	Private Const MQTTPASS As String = ""
	Private Const MQTTPORT As UShort = 1883
	Private Const MQTTQOFS As Protocol.MqttQualityOfServiceLevel = Protocol.MqttQualityOfServiceLevel.AtLeastOnce
#ElseIf USE_SERVER = "CLOUDMQTT" Then
	Private Const MQTTHOST As String = "m24.cloudmqtt.com"
  Private Const MQTTUSER As String = "lyomijtv"
  Private Const MQTTPASS As String = "HsCyFqrM3ghT"
  Private Const MQTTPORT As UShort = 12734
  Private Const MQTTQOFS As Protocol.MqttQualityOfServiceLevel = Protocol.MqttQualityOfServiceLevel.ExactlyOnce
#End If
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Const CRYPTERR As String = "void" & SEPTCHAR & "drink some T A S S O N I and try again"


	'--- V A P O R C H A T | Public ReadOnly -------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	Public ReadOnly NOFCLRS As UShort = 10
	Public ReadOnly ColorPool = New Color() {Color.Crimson, Color.HotPink, Color.Gold, Color.DarkOrchid, Color.Violet,
																		Color.DodgerBlue, Color.Teal, Color.Lime, Color.DarkOrange, Color.SpringGreen}
	' V A P O R C H A T 2 0 2 0 T H E M E S --------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	Public ReadOnly VAPOR_MAINWINTXT() As String = {
		"(っ◔◡◔)っ 【 ﻿Ｖ　Ａ　Ｐ　Ｏ　Ｒ　Ｃ　Ｈ　Ａ　Ｔ 】 (っ◔◡◔)っ",
		"(っ◔◡◔)っ 【 ﻿Ｖ　Ａ　Ｐ　Ｏ　Ｒ　Ｃ　Ｈ　Ａ　Ｔ 】 (っ◔◡◔)っ",
		"IOT demo service",
		"Λ░Ｄ░Ｍ░Ｉ░Ｎ░Ｐ░Λ░Ｎ░Ξ░Ｌ"
	}
	Public ReadOnly VAPOR_MAINBCKIMG() As Image = {Image.FromFile("Resources/Backgrounds/start_main.jpg"), Image.FromFile("Resources/Backgrounds/ondulvapor.jpg"), Nothing, Nothing}
	Public ReadOnly VAPOR_MAINBCKCLR() As Color = {Nothing, Color.FromArgb(40, 31, 51), SystemColors.Control, Nothing}
	Public ReadOnly VAPOR_CHATBCKCLR() As Color = {Nothing, Color.FromArgb(40, 31, 51), SystemColors.ControlLightLight, Nothing}
	Public ReadOnly VAPOR_CHATFRTCLR() As Color = {Nothing, Color.Gold, SystemColors.WindowText, Nothing}
	Public ReadOnly VAPOR_USERBCKCLR() As Color = {Nothing, Color.FromArgb(40, 31, 51), SystemColors.Control, Nothing}
	Public ReadOnly VAPOR_USERFRTCLR() As Color = {Nothing, SystemColors.Highlight, SystemColors.WindowText, Nothing}
	Public ReadOnly VAPOR_SENDBCKCLR() As Color = {Nothing, Color.FromArgb(40, 31, 51), SystemColors.Control, Nothing}
	Public ReadOnly VAPOR_SENDFRTCLR() As Color = {Nothing, Color.DarkOrchid, SystemColors.WindowText, Nothing}
	Public ReadOnly VAPOR_LBLLOGFTXT() As String = {Nothing, "Logs をノだ", "Logs", Nothing}
	Public ReadOnly VAPOR_LBLLOGFCLR() As Color = {Nothing, Color.HotPink, SystemColors.WindowText, Nothing}
	Public ReadOnly VAPOR_LBLLOGVCLR() As Color = {Nothing, Color.Pink, SystemColors.WindowText, Nothing}
	Public ReadOnly VAPOR_LBLUSRFTXT() As String = {Nothing, "Logged users 俺鉛プ", "Logged users", Nothing}
	Public ReadOnly VAPOR_LBLUSRFCLR() As Color = {Nothing, Color.Chartreuse, SystemColors.WindowText, Nothing}
	Public ReadOnly VAPOR_LBLUSRVCLR() As Color = {Nothing, Color.Lime, SystemColors.WindowText, Nothing}
	Public ReadOnly VAPOR_BTNFLSTYLE() As FlatStyle = {Nothing, FlatStyle.Standard, FlatStyle.System, Nothing}
	Public ReadOnly VAPOR_BTNLOGBCLR() As Color = {Nothing, Color.Orchid, SystemColors.ControlLight, Nothing}
	Public ReadOnly VAPOR_BTNLOGFCLR() As Color = {Nothing, Color.Aquamarine, SystemColors.ControlText, Nothing}
	Public ReadOnly VAPOR_BTNSNDBCLR() As Color = {Nothing, Color.Crimson, SystemColors.ControlLight, Nothing}
	Public ReadOnly VAPOR_BTNSNDFCLR() As Color = {Nothing, Color.Gold, SystemColors.ControlText, Nothing}
	Public ReadOnly VAPOR_BTNBCKBCLR() As Color = {Nothing, Color.DarkOrange, SystemColors.ControlLight, Nothing}
	Public ReadOnly VAPOR_BTNBCKFCLR() As Color = {Nothing, Color.CadetBlue, SystemColors.ControlText, Nothing}
	Public ReadOnly VAPOR_USRLSTBCLR() As Color = {Nothing, Color.DarkSlateGray, SystemColors.Window, Nothing}
	' V A P O R C H A T 2 0 2 0 P H R A S E S ------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	Public ReadOnly USRBOXVP() As String = {Nothing, "【﻿Ｃｏｎｎｅｃｔｅｄ　ｕｓｅｒｓ】", "Connected users", Nothing}
	Public ReadOnly JOINVAPO() As String = {Nothing, "(っ◔◡◔)っ ♥ Joins the chat ♥", "Joins the chat", Nothing}
	Public ReadOnly LEAVEVAP() As String = {Nothing, "see you space T A S S O N I ...", "see you space T A S S O N I ...", Nothing}
	Public ReadOnly LOGERROR() As String = {Nothing, "(っ◡.◡)っ e r r o r", "error", Nothing}
	Public ReadOnly SENDISOK() As String = {Nothing, "(っ◠◡◠)っ d o n e", "done", Nothing}
	Public ReadOnly SENDISKO() As String = {Nothing, "(っ◡.◡)っ e r r o r", "error", Nothing}
	Public ReadOnly LOGPROG01() As String = {Nothing, "(っ◑◡◑)っ p e n d i n g", ".", Nothing}
	Public ReadOnly LOGPROG02() As String = {Nothing, "(っ◒◡◒)っ p e n d i n g", "..", Nothing}
	Public ReadOnly LOGPROG03() As String = {Nothing, "(っ◐◡◐)っ p e n d i n g", "...", Nothing}
	Public ReadOnly LOGPROG04() As String = {Nothing, "(っ◓◡◓)っ p e n d i n g", "....", Nothing}


	'--- V A P O R C H A T | Public Constants ------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
#If LIMVIEW Then
	Public Const LIMVIEW As Boolean = False
	Public Const MAXROWS As UShort = 30
#End If
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Const PASSCHAT As String = "paint"
	Public Const SEPTCHAR As String = "ヿーニ"
	Public Const ITSMEMSG As String = "し゛ゐ"
	Public Const BLOCKEDU As String = "ops, seems like you're a nasty boi"
	Public Const FUNNYBOI As String = "oh Rob, u funny boi"
	Public Const COMERROR As String = "please log in"
	Public Const LOGNOERR As String = "online"
	Public Const DISCONNE As String = "offline"
	Public Const ICONPATH As String = "Resources/Ico/logo.ico"
	Public Const ICONNMSG As String = "Resources/Ico/logonewmsg.ico"
	Public Const SENDUKEY As Keys = Keys.Enter
	Public Const HIDEUKEY As Keys = Keys.F1
	Public Const SHOWUKEY As Keys = Keys.F2
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Const STARTWIDTH As UShort = 510
	Public Const STARTHEIGH As UShort = 400
	Public Const CHATWIDTH As UShort = 510
	Public Const CHATHEIGH As UShort = 560
	Public Const PASSWIDTH As UShort = 510
	Public Const PASSHEIGH As UShort = 100
	Public Const ADMNWIDTH As UShort = 510
	Public Const ADMNHEIGH As UShort = 130
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Const TSTOPPUB As UShort = 500
	Public Const TCHKMSGR As UShort = 200
	Public Const TUPDTGUI As UShort = 500
	'-----------------------------------------------------------------------------------------------------------------------' 
	' Command format >  /<command>:<target user>:<optional>
	Public Const ADMINUNAME As String = "V A P O R A D M I N"
	Public Const ADMINSPLIT As String = ":"
	Public Const ADMINPASSW As String = "tassorosso"
	Public Const ADMINMUTEU As String = "/mute"
	Public Const ADMINUMUTE As String = "/allow"
	Public Const ADMINLOBBY As String = "/lobby"
	'-----------------------------------------------------------------------------------------------------------------------' 
	Public Const TOKIDRIFT As String = "/tokidrift"
	Public Const VAPOCHESS As String = "/vaporchess"
	'-----------------------------------------------------------------------------------------------------------------------' 
	Public Const TOKIPATH As String = "..\T̵̞̜̲̻͛̓̏̇͑͐D̵̫̘̣̮̦̖͙͈̜͛͝\TokiDrift.exe"


	'--- V A P O R C H A T | Struct ----------------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	Structure MessageStruct
		Dim user As String
		Dim text As String
	End Structure
	'-----------------------------------------------------------------------------------------------------------------------'
	Structure ConfigStruct
		Dim user As String
		Dim text As String
	End Structure


	'--- V A P O R C H A T | Enum ------------------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Enum Themes
		Start   ' 0
		Vapor   ' 1
		Hide    ' 2
		Admin   ' 3
		NofElm
	End Enum
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Enum Panels
		Chat
		Pass
		Admin
	End Enum


	'--- V A P O R C H A T | Variables -------------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	ReadOnly message As New Queue(Of MessageStruct)
	ReadOnly config As New Queue(Of ConfigStruct)
	Private messagetopic As String = ""
	Private configstopic As String = ""
	'-----------------------------------------------------------------------------------------------------------------------'
	Public CurrentTheme As Themes


	'--- V A P O R C H A T | MQTT Service Functions ------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub HandleReceivedMessages(eventArgs As MqttApplicationMessageReceivedEventArgs)
		Dim payload As String()
		Dim trusted_payload As String
		Dim trusted_topic As String = Decrypt(eventArgs.ApplicationMessage.Topic, True)
		Select Case trusted_topic
			Case messagetopic
				Dim recv As MessageStruct
				trusted_payload = Decrypt(Text.Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload), False)
				payload = trusted_payload.Split(SEPTCHAR)
				recv.user = payload(0)
				recv.text = payload(1).Remove(0, SEPTCHAR.Length - 1)
				message.Enqueue(recv)
			Case configstopic
				Dim recv As ConfigStruct
				trusted_payload = Decrypt(Text.Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload), False)
				payload = trusted_payload.Split(SEPTCHAR)
				recv.user = payload(0)
				recv.text = payload(1).Remove(0, SEPTCHAR.Length - 1)
				config.Enqueue(recv)
		End Select
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub HandleConnection(eventArgs As MqttClientConnectedEventArgs)

	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Sub HandleDisconnection(eventArgs As MqttClientDisconnectedEventArgs)

	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Function MQTTConnectToServer(id As String, uri As String, user As String, pwd As String, port As String) As Boolean
		Dim result As MqttClientAuthenticateResult
		Dim cancel As New Threading.CancellationTokenSource
		Dim messageBuilder As New MqttClientOptionsBuilder
		Dim lastwill As New MqttApplicationMessage
		MqttClient = Factory.CreateMqttClient()
		MqttClient.UseApplicationMessageReceivedHandler(AddressOf HandleReceivedMessages)
		MqttClient.UseConnectedHandler(AddressOf HandleConnection)
		MqttClient.UseDisconnectedHandler(AddressOf HandleDisconnection)
		messageBuilder.WithProtocolVersion(Formatter.MqttProtocolVersion.V500)
		messageBuilder.WithClientId(id)
		messageBuilder.WithTcpServer(uri, CInt(port))
		messageBuilder.WithCleanSession(False)
		messageBuilder.WithKeepAlivePeriod(TimeSpan.FromSeconds(TIMPINGS))
		messageBuilder.WithCommunicationTimeout(TimeSpan.FromSeconds(120))
		messageBuilder.WithSessionExpiryInterval(TSESSION)
		If user <> "" And pwd <> "" Then
			messageBuilder.WithCredentials(user, pwd)
		End If
		Try
			result = MqttClient.ConnectAsync(messageBuilder.Build(), cancel.Token).Result
		Catch
			cancel.Cancel()
			Return False
		End Try
		If result.ResultCode = MqttClientConnectResultCode.Success Then
			Return True
		End If
		Return False
	End Function
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Async Sub MQTTDisconnectFromServer()
		Await MqttClient.DisconnectAsync()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Function MQTTSubscribe(ByVal topic As String, qos As Protocol.MqttQualityOfServiceLevel) As Boolean
		Dim result As MqttClientSubscribeResult
		Dim mqttTopicFilter As New MqttTopicFilterBuilder
		mqttTopicFilter.WithTopic(topic)
		mqttTopicFilter.WithQualityOfServiceLevel(qos)
		If MqttClient.IsConnected Then
			result = MqttClient.SubscribeAsync(mqttTopicFilter.Build()).Result
			Select Case qos
				Case Protocol.MqttQualityOfServiceLevel.AtMostOnce
					If result.Items(0).ResultCode = MqttClientSubscribeResultCode.GrantedQoS0 Then
						Return True
					End If
				Case Protocol.MqttQualityOfServiceLevel.AtLeastOnce
					If result.Items(0).ResultCode = MqttClientSubscribeResultCode.GrantedQoS1 Then
						Return True
					End If
				Case Protocol.MqttQualityOfServiceLevel.ExactlyOnce
					If result.Items(0).ResultCode = MqttClientSubscribeResultCode.GrantedQoS2 Then
						Return True
					End If
			End Select
		End If
		Return False
	End Function
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Function MQTTPublish(topic As String, payload As String, retainFlag As Boolean, qos As Protocol.MqttQualityOfServiceLevel) As Boolean
		Dim result As New MqttClientPublishResult
		Dim cancel As New Threading.CancellationTokenSource
		Dim publish As New MqttApplicationMessageBuilder
		publish.WithMessageExpiryInterval(TMESSAGE)
		publish.WithQualityOfServiceLevel(qos)
		publish.WithTopic(topic)
		publish.WithPayload(payload)
		publish.WithRetainFlag(retainFlag)
		Try
			result = MqttClient.PublishAsync(publish.Build(), cancel.Token).Result
		Catch
			cancel.Cancel()
			Return False
		End Try
		If result.ReasonCode = MqttClientPublishReasonCode.Success Then
			Return True
		End If
		Return False
	End Function


	'--- V A P O R C H A T | Private Functions -----------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Function Encrypt(ByVal encodedata As String, ByVal optopic As Boolean) As String
		Dim wrapper As New Simple3Des(My.Settings.Publisher)
		Dim cipherText As String = wrapper.EncryptData(encodedata)
		If optopic Then
			Return cipherText.Replace("+", "!").Replace("/", "?")
		Else
			Return cipherText
		End If
	End Function
	'-----------------------------------------------------------------------------------------------------------------------'
	Private Function Decrypt(ByVal decodedata As String, ByVal optopic As Boolean) As String
		Dim wrapper As New Simple3Des(My.Settings.Publisher)
		Try
			If optopic Then
				Return wrapper.DecryptData(decodedata.Replace("!", "+").Replace("?", "/"))
			Else
				Return wrapper.DecryptData(decodedata)
			End If
		Catch ex As Security.Cryptography.CryptographicException
			Return CRYPTERR
		End Try
	End Function


	'--- V A P O R C H A T | Public Functions ------------------------------------------------------------------------------'
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Function Connect(ByVal user As String, ByVal lobby As String) As Boolean
		Dim cryptMessageTopic As String
		Dim cryptConfigsTopic As String
		message.Clear()
		config.Clear()
		messagetopic = MQTTROOT & My.Settings.Lobby
		configstopic = MQTTROOT & MQTTCONF & lobby
		cryptMessageTopic = Encrypt(messagetopic, True)
		cryptConfigsTopic = Encrypt(configstopic, True)
		If MQTTConnectToServer(user, MQTTHOST, MQTTUSER, MQTTPASS, MQTTPORT) = False Then
			Return False
		End If
		If MQTTSubscribe(cryptMessageTopic, MQTTQOFS) = False Then
			Return False
		End If
		If MQTTSubscribe(cryptConfigsTopic, MQTTQOFS) = False Then
			Return False
		End If
		Return True
	End Function
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub Disconnect()
		MQTTDisconnectFromServer()
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Function SendMessage(ByRef progresser As ComponentModel.BackgroundWorker, ByVal user As String, ByVal text As String) As Boolean
		Dim res As Boolean
		If progresser.IsBusy = False Then
			progresser.RunWorkerAsync()
			res = MQTTPublish(Encrypt(messagetopic, True), Encrypt(user & SEPTCHAR & text, False), False, MQTTQOFS)
			progresser.Dispose()
		Else
			res = MQTTPublish(Encrypt(messagetopic, True), Encrypt(user & SEPTCHAR & text, False), False, MQTTQOFS)
		End If
		Return res
	End Function
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Function SendConfig(ByVal user As String, ByVal text As String) As Boolean
		Return MQTTPublish(Encrypt(configstopic, True), Encrypt(user & SEPTCHAR & text, False), False, MQTTQOFS)
	End Function
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Function CheckMessageRecv() As Boolean
		Return message.Count > 0
	End Function
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Function CheckConfigRecv() As Boolean
		Return config.Count > 0
	End Function
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub GetMessageUserAndText(ByRef user As String, ByRef text As String)
		Dim recv As MessageStruct
		recv = message.Dequeue()
		user = recv.user
		text = recv.text
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Sub GetConfigUserAndText(ByRef user As String, ByRef text As String)
		Dim recv As ConfigStruct
		recv = config.Dequeue()
		user = recv.user
		text = recv.text
	End Sub
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Function MaxUserLen() As UShort
		Return MAXUSRCH
	End Function
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Function MaxMessageLen() As UShort
		Return MAXMSGCH
	End Function
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Function GetMqttQoS() As Short
		Select Case MQTTQOFS
			Case Protocol.MqttQualityOfServiceLevel.AtMostOnce
				Return 0
			Case Protocol.MqttQualityOfServiceLevel.AtLeastOnce
				Return 1
			Case Protocol.MqttQualityOfServiceLevel.ExactlyOnce
				Return 2
		End Select
		Return -1
	End Function
	'-----------------------------------------------------------------------------------------------------------------------'
	Public Function IsOnline() As Boolean
		If MqttClient IsNot Nothing Then
			Return MqttClient.IsConnected
		End If
		Return False
	End Function
End Class
