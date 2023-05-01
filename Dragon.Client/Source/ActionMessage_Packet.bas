Attribute VB_Name = "ActionMessage_Packet"
Option Explicit

Public Sub HandleActionMessage(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim X As Long, Y As Long, Message As Long, Color As Long, tmpType As Long, FontType As Byte

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    tmpType = Buffer.ReadLong
    FontType = Buffer.ReadLong
    Color = Buffer.ReadLong
    X = Buffer.ReadLong * PIC_X
    Y = Buffer.ReadLong * PIC_Y
    Message = Buffer.ReadLong
    
    X = Rand(X - 8, X + 8)
    Y = Rand(Y - 16, Y)

    Set Buffer = Nothing

    CreateActionMsg Message, Color, tmpType, FontType, X, Y
End Sub
