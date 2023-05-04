Attribute VB_Name = "Target_Packet"
Option Explicit

Public Sub SendPlayerTarget(ByVal Target As Long, ByVal TargetType As Long)
    MyTargetIndex = Target
    MyTargetType = TargetType

    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PTarget
    Buffer.WriteLong Target
    Buffer.WriteLong TargetType

    SendGameMessage Buffer.ToArray()

    Set Buffer = Nothing
End Sub

Public Sub HandleTarget(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteBytes Data()
    
    MyTargetIndex = Buffer.ReadLong
    MyTargetType = Buffer.ReadLong
    
    Set Buffer = Nothing

    If MyTargetIndex <= 0 Then
        Call CloseTargetWindow
    Else
        Call OpenTargetWindow
    End If
End Sub
