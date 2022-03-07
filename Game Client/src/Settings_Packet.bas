Attribute VB_Name = "Settings_Packet"
Option Explicit

Public Sub HandleSettings(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data

    With Windows(GetWindowIndex("winCharacter")).Controls(GetControlIndex("winCharacter", "chkView"))
        .Value = Buffer.ReadByte
    End With

    Set Buffer = Nothing
End Sub

