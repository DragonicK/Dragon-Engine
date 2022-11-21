Attribute VB_Name = "Map_Packet"
Option Explicit

Public Sub SendPlayerRequestNewMap()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    Buffer.WriteLong CRequestNewMap
    Buffer.WriteLong GetPlayerDir(MyIndex)
    SendData Buffer.ToArray()
    Set Buffer = Nothing
End Sub
