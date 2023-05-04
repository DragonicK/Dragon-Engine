Attribute VB_Name = "NetworkChat_Handle"
Option Explicit

Public Sub HandleChatPacket(ByRef Data() As Byte)
    Dim Buffer As clsBuffer
    Dim MsgType As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    If Buffer.ExecuteDecipher(ChatInstance) Then
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
    End If
End Sub
