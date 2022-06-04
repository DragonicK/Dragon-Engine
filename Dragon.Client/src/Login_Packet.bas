Attribute VB_Name = "Login_Packet"
Public Sub SendLogin(ByVal Name As String)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PGameServerLogin
    Buffer.WriteString LoginToken
    
    SendData Buffer.ToArray()
    Set Buffer = Nothing
End Sub

Public Sub SendAuthLogin(ByVal Name As String, ByVal Password As String)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PAuthentication
    Buffer.WriteString Name
    Buffer.WriteString Password
    Buffer.WriteLong CLIENT_MAJOR
    Buffer.WriteLong CLIENT_MINOR
    Buffer.WriteLong CLIENT_REVISION
    Buffer.WriteString MachineId
    Buffer.WriteString BIOSId
    Buffer.WriteString HardDiskId
    Buffer.WriteString CPUId
    Buffer.WriteString VideoId
    Buffer.WriteString MacAddressId
    Buffer.WriteString MotherboardId
     
    SendData Buffer.ToArray()
    Set Buffer = Nothing
End Sub

Public Sub HandleAuthenticationResult(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()
    
    GameServerIp = Buffer.ReadString
    GameServerPort = Buffer.ReadLong
    LoginToken = Buffer.ReadString
    
    Set Buffer = Nothing
    
    ' try and login to game server
    AttemptLogin
End Sub
