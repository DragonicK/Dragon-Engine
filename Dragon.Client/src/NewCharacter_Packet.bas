Attribute VB_Name = "NewCharacter_Packet"
Public Sub SendAddChar(ByVal Name As String, ByVal Sex As Long, ByVal ClassId As Long, ByVal Sprite As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PCharacterCreate
    Buffer.WriteString Name
    Buffer.WriteLong Sex
    Buffer.WriteLong ClassId
    Buffer.WriteLong Sprite
    Buffer.WriteLong CharNum
    
    SendData Buffer.ToArray()
    Set Buffer = Nothing
    
End Sub
