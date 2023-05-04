Attribute VB_Name = "NetworkGame_Handle"
Option Explicit

Public Sub HandleGamePacket(ByRef Data() As Byte)
    Dim Buffer As clsBuffer
    Dim MsgType As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    If Buffer.ExecuteDecipher(GameInstance) Then
        MsgType = Buffer.ReadLong

        If MsgType < 0 Then
            DestroyGame
            Exit Sub
        End If

        If MsgType >= EnginePacket.PPacketCount Then
            DestroyGame
            Exit Sub
        End If

        CallWindowProc HandleDataSub(MsgType), 1, Buffer.ReadBytes(Buffer.Length), 0, 0
        Exit Sub

    Else
        MsgBox "Ocorreu um erro ao receber e processar os dados."

        DestroyGame
    End If

Error:
    MsgBox "Ocorreu um erro ao receber e processar os dados."
    DestroyGame
End Sub

Public Sub HandleHighIndex(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    Player_HighIndex = Buffer.ReadLong

    Set Buffer = Nothing
End Sub

Public Sub HandleSetPlayerIndex(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    MyIndex = Buffer.ReadLong

    Set Buffer = Nothing

End Sub

Public Sub HandleInGame(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    HideWindows

    InGame = True
    inMenu = False

    CanMoveNow = True
    GettingMap = False

    ' show gui
    ShowWindow GetWindowIndex("winBars"), , False
    ' ShowWindow GetWindowIndex("winMenu"), , False
    ShowWindow GetWindowIndex("winHotbar"), , False
    ShowWindow GetWindowIndex("winChatSmall"), , False

    If Party.Leader > 0 Then
        ShowWindow GetWindowIndex("winParty"), , False
    End If

    ' enter loop
    GameLoop
End Sub

Public Sub HandleAttack(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim i As Long
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()
    i = Buffer.ReadLong

    ' Set player to attacking
    Player(i).Attacking = 1
    Player(i).AttackTimer = GetTickCount
    Player(i).AttackFrameIndex = 1
End Sub

Public Sub HandleNpcAttack(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim i As Long
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()
    i = Buffer.ReadLong

    ' Set player to attacking
    MapNpc(i).Attacking = 1
    MapNpc(i).AttackTimer = GetTickCount
    MapNpc(i).AttackFrameIndex = 1
    MapNpc(i).FrameTick = GetTickCount
End Sub

Public Sub HandlePlayerLeft(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, pIndex As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    pIndex = Buffer.ReadLong

    Set Buffer = Nothing

    Call ClearPlayer(pIndex)
End Sub

Public Sub HandleExitGame(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, pIndex As Long
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    pIndex = Buffer.ReadLong

    Call ClearPlayer(pIndex)

    Set Buffer = Nothing
End Sub

Public Sub HandleSendPing(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    PingEnd = GetTickCount
    Ping = PingEnd - PingStart
End Sub

Public Sub HandleStunned(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()
    StunDuration = Buffer.ReadLong
    Set Buffer = Nothing
End Sub

Public Sub HandleSound(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim X As Long, Y As Long, entityType As Long, entityNum As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    X = Buffer.ReadInteger
    Y = Buffer.ReadInteger
    entityType = Buffer.ReadByte
    entityNum = Buffer.ReadLong

    Set Buffer = Nothing

    PlayMapSound X, Y, entityType, entityNum
End Sub

Public Sub HandleChatUpdate(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, NpcNum As Long
    Dim ConversationNum As Long, CurrentChat As Long
    'mT As String, o(1 To 4) As String, i As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    NpcNum = Buffer.ReadLong
    ConversationNum = Buffer.ReadLong
    CurrentChat = Buffer.ReadLong

    Set Buffer = Nothing

    ' if npcNum is 0, exit the chat system
    If NpcNum = 0 Then
        inChat = False
        HideWindow GetWindowIndex("winNpcChat")
        Exit Sub
    End If

    If ConversationNum > 0 And CurrentChat > 0 Then
       ' OpenNpcChat NpcNum, Conv(ConversationNum).Conv(CurrentChat).Conv, Conv(ConversationNum).Conv(CurrentChat).rText
    End If
End Sub

Public Sub HandleCheckPing(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim ClientRequest As Byte
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data
    ClientRequest = Buffer.ReadByte

    If ClientRequest = 1 Then
        PingEnd = GetTickCount
        Ping = PingEnd - PingStart
    End If
End Sub






