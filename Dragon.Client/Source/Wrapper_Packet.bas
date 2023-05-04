Attribute VB_Name = "Wrapper_Packet"
Option Explicit

Public Sub HandleUpdateCipherKey(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, Count As Long
    Dim GameState As GameState
    Dim Key() As Byte

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    Count = Buffer.ReadLong

    Key = Buffer.ReadBytes(Count)
    GameState = Buffer.ReadLong

    If GameState = GameState_Login Then
        CopyMemory GameInstanceCipherKey(0), Key(0), CipherKeyLength
        
        Call UpdateKey(GameInstance, ByVal VarPtr(GameInstanceCipherKey(0)), CipherKeyLength)
        Call SendAuthLogin(Username, Passphrase)
        
    ElseIf GameState = GameState_Game Then
        CopyMemory GameInstanceCipherKey(0), Key(0), CipherKeyLength
        
        Call UpdateKey(GameInstance, ByVal VarPtr(GameInstanceCipherKey(0)), CipherKeyLength)
        Call SendGameLogin
        
    ElseIf GameState = GameState_Chat Then
        CopyMemory ChatInstanceCipherKey(0), Key(0), CipherKeyLength
        
        Call UpdateKey(ChatInstance, ByVal VarPtr(ChatInstanceCipherKey(0)), CipherKeyLength)
        Call SendChatLogin
    End If

End Sub
