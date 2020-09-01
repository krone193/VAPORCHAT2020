Imports MQTTnet
Imports MQTTnet.Client.Options
Imports MQTTnet.Server
Imports MQTTnet.Client.Receiving
Imports MQTTnet.Client
Public Class VaporChat
  '--- V A P O R C H A T | Implementation --------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
  Implements IMqttApplicationMessageReceivedHandler
  Private ReadOnly Factory As New MqttFactory
  Private WithEvents MqttClient As MqttClient


  '--- V A P O R C H A T | Declarations ----------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
#Const USE_SERVER = "MOSQUITTO"


  '--- V A P O R C H A T | Private Constants -----------------------------------------------------------------------------'
  ' Chat -----------------------------------------------------------------------------------------------------------------'
  Private Const MAXMSGCH As UShort = 64
  Private Const MAXUSRCH As UShort = 10
  ' MQTT -----------------------------------------------------------------------------------------------------------------'
  Private Const MQTTROOT As String = "kronelab/vaporchat/"
  Private Const MQTTCONF As String = "conf/"
#If USE_SERVER = "HIVEMQ" Then
  'Private Const MQTTHOST As String = "broker.hivemq.com"
  Private Const MQTTUSER As String = ""
  Private Const MQTTPASS As String = ""
  Private Const MQTTPORT As UShort = 1883
  'Private Const MQTTQOFS As Protocol.MqttQualityOfServiceLevel = Protocol.MqttQualityOfServiceLevel.AtMostOnce
#ElseIf USE_SERVER = "MOSQUITTO" Then
  Private Const MQTTHOST As String = "test.mosquitto.org"
  Private Const MQTTUSER As String = ""
  Private Const MQTTPASS As String = ""
  Private Const MQTTPORT As UShort = 1883
  Private Const MQTTQOFS As Protocol.MqttQualityOfServiceLevel = Protocol.MqttQualityOfServiceLevel.ExactlyOnce
#ElseIf USE_SERVER = "CLOUDMQTT" Then
  Private Const MQTTHOST As String = "m24.cloudmqtt.com"
  Private Const MQTTUSER As String = "lyomijtv"
  Private Const MQTTPASS As String = "HsCyFqrM3ghT"
  Private Const MQTTPORT As UShort = 12734
  Private Const MQTTQOFS As Protocol.MqttQualityOfServiceLevel = Protocol.MqttQualityOfServiceLevel.ExactlyOnce
#End If
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Const CRYPTERR As String = "void" & SEPTCHAR & "drink some T A S S O N I and try again"


  '--- V A P O R C H A T | Public Constants ------------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Const PASSCHAT As String = "paint"
  Public Const SEPTCHAR As String = "ヿーニ"
  Public Const ITSMEMSG As String = "し゛ゐ"
  Public Const USRBOXVP As String = "【﻿Ｃｏｎｎｅｃｔｅｄ　ｕｓｅｒｓ】"
  Public Const USRBOXHI As String = "Connected users"
  Public Const JOINVAPO As String = "(っ◔◡◔)っ ♥ Joins the chat ♥"
  Public Const LEAVEVAP As String = "see you space T A S S O N I ..."
  Public Const JOINHIDE As String = "Joins the chat"
  Public Const BLOCKEDU As String = "ops, seems like you're a nasty boi"
  Public Const FUNNYBOI As String = "oh Rob, u funny boi"
  Public Const COMERROR As String = "please log in"
  Public Const LOGERROR As String = "connection error, please log again"
  Public Const LOGNOERR As String = "connected"
  Public Const SENDISOK As String = "message forwarded"
  Public Const SENDISKO As String = "message not forwarded"
  Public Const ICONPATH As String = "Resources/logo.ico"
  Public Const ICONNMSG As String = "Resources/logonewmsg.ico"
  Public Const SENDUKEY As Keys = Keys.Enter
  Public Const HIDEUKEY As Keys = Keys.F1
  Public Const SHOWUKEY As Keys = Keys.F2
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Const CHATWIDTH As UShort = 497
  Public Const CHATHEIGH As UShort = 570
  Public Const PASSWIDTH As UShort = 497
  Public Const PASSHEIGH As UShort = 100
  Public Const ADMNWIDTH As UShort = 497
  Public Const ADMNHEIGH As UShort = 130
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Const TSTOPPUB As UShort = 500
  Public Const TCHKMSGR As UShort = 200
  Public Const TUPDTGUI As UShort = 500
  '-----------------------------------------------------------------------------------------------------------------------' 
  ' Command format >  /<command>:<target user>:<optional>
  Public Const ADMINPASSW As String = "tassorosso"
  Public Const ADMINMUTEU As String = "/mute"
  Public Const ADMINUMUTE As String = "/allow"
  Public Const ADMINLOBBY As String = "/lobby"
  '-----------------------------------------------------------------------------------------------------------------------' 
  Public Const TOKIDRIFT As String = "/tokidrift"


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
    Vapor   ' 0
    Hide    ' 1
    Admin   ' 2
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
  Private conongoing As Boolean = False
  Private conok As Boolean = True
  Private pubongoing As Boolean = False
  Private pubok As Boolean = True
  Private subongoing As Boolean = False
  Private subok As Boolean = True
  Private messagetopic As String = ""
  Private configstopic As String = ""


  '--- V A P O R C H A T | MQTT Service Functions ------------------------------------------------------------------------'
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Function HandleApplicationMessageReceivedAsync(eventArgs As MqttApplicationMessageReceivedEventArgs) As Task Implements IMqttApplicationMessageReceivedHandler.HandleApplicationMessageReceivedAsync
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
    Return Nothing
  End Function
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Async Sub MQTTConnectToServer(id As String, uri As String, user As String, pwd As String, port As String)
    Dim messageBuilder As New MqttClientOptionsBuilder
    Dim options As New MqttClientOptions
    Dim cancellationToken As Threading.CancellationToken
    Dim lastwill As New MqttApplicationMessage
    MqttClient = Factory.CreateMqttClient()
    lastwill.Topic = messagetopic
    lastwill.Payload = Text.Encoding.ASCII.GetBytes(LEAVEVAP)
    lastwill.QualityOfServiceLevel = MQTTQOFS
    lastwill.Retain = False
    messageBuilder.WithClientId(id)
    messageBuilder.WithCredentials(user, pwd)
    messageBuilder.WithTcpServer(uri, CInt(port))
    messageBuilder.WithCleanSession(True)
    messageBuilder.Build()
    messageBuilder.WithKeepAlivePeriod(TimeSpan.FromSeconds(30))
    messageBuilder.WithKeepAliveSendInterval(TimeSpan.FromSeconds(30))
    messageBuilder.WithWillMessage(lastwill)
    conongoing = True
    Try
      Await MqttClient.ConnectAsync(messageBuilder.Build(), cancellationToken)
    Catch ex As Exception
      conok = False
    Finally
      conongoing = False
    End Try
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Async Sub MQTTDisconnectFromServer()
    Await MqttClient.DisconnectAsync()
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Async Sub MQTTSubscribe(ByVal topic As String, qos As Protocol.MqttQualityOfServiceLevel)
    Dim mqttTopicFilterBuilder As New TopicFilterBuilder
    mqttTopicFilterBuilder.WithTopic(topic)
    mqttTopicFilterBuilder.WithQualityOfServiceLevel(qos)
    mqttTopicFilterBuilder.Build()
    subongoing = True
    Try
      Await MqttClient.SubscribeAsync(mqttTopicFilterBuilder.Build())
    Catch ex As Exception
      subok = False
    Finally
      subongoing = False
    End Try
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Private Async Sub MQTTPublish(topic As String, payload As String, retainFlag As Boolean, qos As Protocol.MqttQualityOfServiceLevel)
    Dim mqttMessageBulder As New MqttApplicationMessageBuilder
    Dim mqttMessage As MqttApplicationMessage
    Dim cancellationToken As Threading.CancellationToken
    mqttMessageBulder.WithTopic(topic)
    mqttMessageBulder.WithPayload(payload)
    mqttMessageBulder.WithQualityOfServiceLevel(qos)
    mqttMessageBulder.WithRetainFlag(retainFlag)
    mqttMessage = mqttMessageBulder.Build()
    pubongoing = True
    Try
      Await MqttClient.PublishAsync(mqttMessage, cancellationToken)
    Catch ex As Exception
      pubok = False
    Finally
      pubongoing = False
    End Try
  End Sub


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
  Public Function Connect(ByVal user As String) As Boolean
    Dim timeout As Date = Date.Now
    timeout = timeout.AddSeconds(10)
    messagetopic = MQTTROOT & My.Settings.Lobby
    configstopic = MQTTROOT & MQTTCONF & My.Settings.Lobby
    message.Clear()
    config.Clear()
    MQTTConnectToServer(user, MQTTHOST, MQTTUSER, MQTTPASS, MQTTPORT)
    MqttClient.ApplicationMessageReceivedHandler = Me
    While Not MqttClient.IsConnected
      If Date.Now >= timeout Then
        Return False
      End If
    End While
    MQTTSubscribe(Encrypt(messagetopic, True), MQTTQOFS)
    MQTTSubscribe(Encrypt(configstopic, True), MQTTQOFS)
    Return True
  End Function
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Sub Disconnect()
    MQTTDisconnectFromServer()
    MqttClient.ApplicationMessageReceivedHandler = Me
  End Sub
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Function SendMessage(ByVal user As String, ByVal text As String) As Boolean
    MQTTPublish(Encrypt(messagetopic, True), Encrypt(user & SEPTCHAR & text, False), False, MQTTQOFS)
    Return True
  End Function
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Function SendConfig(ByVal user As String, ByVal text As String) As Boolean
    MQTTPublish(Encrypt(configstopic, True), Encrypt(user & SEPTCHAR & text, False), False, MQTTQOFS)
    Return True
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
  Public Function GetConState() As Boolean
    If conok <> True Then
      conok = True
      Return False
    End If
    Return conok
  End Function
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Function GetPubState() As Boolean
    If pubok <> True Then
      pubok = True
      Return False
    End If
    Return pubok
  End Function
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Function GetSubState() As Boolean
    If subok <> True Then
      subok = True
      Return False
    End If
    Return subok
  End Function
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Function GetConOngoing() As Boolean
    Return subongoing
  End Function
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Function GetPubOngoing() As Boolean
    Return pubongoing
  End Function
  '-----------------------------------------------------------------------------------------------------------------------'
  Public Function GetSubOngoing() As Boolean
    Return subongoing
  End Function
End Class
