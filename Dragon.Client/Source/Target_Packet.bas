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

    SendData Buffer.ToArray()

    Set Buffer = Nothing
End Sub
