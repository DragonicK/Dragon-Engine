Attribute VB_Name = "modClientTCP"
Option Explicit

' *****************************
' ** Outgoing Client Packets **
' *****************************

Public Sub SendPlayerMovement()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PPlayerMovement
    Buffer.WriteLong MyIndex
    Buffer.WriteLong GetPlayerDir(MyIndex)
    Buffer.WriteLong Player(MyIndex).Moving
    Buffer.WriteInteger Player(MyIndex).X
    Buffer.WriteInteger Player(MyIndex).Y
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendPlayerDirection()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PPlayerDirection
    Buffer.WriteLong MyIndex
    Buffer.WriteLong GetPlayerDir(MyIndex)
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendPlayerRequestNewMap()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    Buffer.WriteLong CRequestNewMap
    Buffer.WriteLong GetPlayerDir(MyIndex)
    SendData Buffer.ToArray()
    Set Buffer = Nothing
End Sub

Public Sub GetPing()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    PingStart = GetTickCount

    Buffer.WriteLong EnginePacket.PCheckPing

    SendData Buffer.ToArray()

    Set Buffer = Nothing
End Sub

Public Sub SendPlayerTarget(ByVal Target As Long, ByVal TargetType As Long)
    MyTarget = Target
    MyTargetType = TargetType

    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PTarget
    Buffer.WriteLong Target
    Buffer.WriteLong TargetType

    SendData Buffer.ToArray()

    Set Buffer = Nothing
End Sub
