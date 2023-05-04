Attribute VB_Name = "NetworkChat_Client"
Option Explicit

' ******************************************
' ** Communcation to server, TCP          **
' ** Winsock Control (mswinsck.ocx)       **
' ** String packets (slow and big)        **
' ******************************************
Private PlayerBuffer As clsBuffer

Public Sub ChatClientInit(ByVal IpAddress As String, ByVal Port As Long)
    Set PlayerBuffer = Nothing
    Set PlayerBuffer = New clsBuffer

    ' Connect
    frmMain.ChatSocket.Close
    frmMain.ChatSocket.RemoteHost = IpAddress
    frmMain.ChatSocket.RemotePort = Port
End Sub

Public Sub DestroyGhatClient()
    frmMain.ChatSocket.Close
End Sub

Public Sub ReceiveChatIncomingMessage(ByVal DataLength As Long)
    Dim Buffer() As Byte
    Dim pLength As Long

    frmMain.ChatSocket.GetData Buffer, vbUnicode, DataLength

    PlayerBuffer.WriteBytes Buffer()

    If PlayerBuffer.Length >= 4 Then
        pLength = PlayerBuffer.ReadLong(False)
    End If

    Do While pLength > 0 And pLength <= PlayerBuffer.Length - 4

        If pLength <= PlayerBuffer.Length - 4 Then
            PlayerBuffer.ReadLong
            HandleChatPacket PlayerBuffer.ReadBytes(pLength)
        End If

        pLength = 0

        If PlayerBuffer.Length >= 4 Then
            pLength = PlayerBuffer.ReadLong(False)
        End If
    Loop

    PlayerBuffer.Trim

    DoEvents
End Sub

Public Function ConnectToChatServer() As Boolean
    Dim Wait As Long

    ' Check to see if we are already connected, if so just exit
    If IsChatConnected Then
        ConnectToChatServer = True
        Exit Function
    End If

    Wait = GetTickCount
    
    frmMain.ChatSocket.Close
    frmMain.ChatSocket.Connect

    ' Wait until connected or 3 seconds have passed and report the server being down
    Do While (Not IsChatConnected) And (GetTickCount <= Wait + 2000)
       DoEvents
    Loop

    ConnectToChatServer = IsChatConnected
End Function

Function IsChatConnected() As Boolean
    If frmMain.ChatSocket.State = sckConnected Then
        IsChatConnected = True
    End If
End Function

Public Sub SendChatMessage(ByRef Data() As Byte)
    If IsChatConnected Then
        Dim Length As Long
        Dim Ciphed() As Byte
           
        Ciphed = CreateEncryptedPacket(Data)
       
        Dim Buffer As clsBuffer
        Set Buffer = New clsBuffer
        
        Length = ((UBound(Ciphed) - LBound(Ciphed)) + 1)
               
        Buffer.PreAllocate Length + 4
        Buffer.WriteLong Length
        Buffer.WriteBytes Ciphed()
        
        frmMain.ChatSocket.SendData Buffer.ToArray()

        Set Buffer = Nothing
    End If
    Exit Sub

Error:
    MsgBox "Ocorreu um erro ao enviar os dados para o servidor."
    DestroyGame
End Sub

Private Function CreateEncryptedPacket(ByRef Data() As Byte) As Byte()
    Dim Buffer() As Byte
    Dim Length As Long
    Dim BufferLength As Long

    Length = (UBound(Data) - LBound(Data)) + 1
    
    BufferLength = ((UBound(Data) - LBound(Data)) + 1) + 4
    BufferLength = BufferLength + 8 - BufferLength Mod 8

    ReDim Buffer(0 To BufferLength - 1)

    CopyMemory Buffer(0), Data(0), Length

    Call AppendCheckSum(ChatInstance, ByVal VarPtr(Buffer(0)), 0, BufferLength)

    Call Cipher(ChatInstance, ByVal VarPtr(Buffer(0)), BufferLength)

    CreateEncryptedPacket = Buffer

End Function

Public Sub SendChatLogin()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PChatServerLogin
    Buffer.WriteString ChatToken

    SendChatMessage Buffer.ToArray()
    Set Buffer = Nothing
End Sub

Public Sub ChatServerAttemptLogin()
    Call ChatClientInit(ChatServerIp, ChatServerPort)
    Call ConnectToChatServer
End Sub



