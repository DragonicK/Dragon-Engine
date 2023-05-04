Attribute VB_Name = "NetworkGame_Client"
Option Explicit

' ******************************************
' ** Communcation to server, TCP          **
' ** Winsock Control (mswinsck.ocx)       **
' ** String packets (slow and big)        **
' ******************************************
Private PlayerBuffer As clsBuffer

Public Sub GameClientInit(ByVal IP As String, ByVal Port As Long)
    Set PlayerBuffer = Nothing
    Set PlayerBuffer = New clsBuffer

    ' connect
    frmMain.Socket.Close
    frmMain.Socket.RemoteHost = IP
    frmMain.Socket.RemotePort = Port
End Sub

Public Sub DestroyGameClient()
    frmMain.Socket.Close
End Sub

Public Sub ReceiveGameIncomingMessage(ByVal DataLength As Long)
    Dim Buffer() As Byte
    Dim pLength As Long

    frmMain.Socket.GetData Buffer, vbUnicode, DataLength

    PlayerBuffer.WriteBytes Buffer()

    If PlayerBuffer.Length >= 4 Then
        pLength = PlayerBuffer.ReadLong(False)
    End If

    Do While pLength > 0 And pLength <= PlayerBuffer.Length - 4

        If pLength <= PlayerBuffer.Length - 4 Then
            PlayerBuffer.ReadLong
            HandleGamePacket PlayerBuffer.ReadBytes(pLength)
        End If

        pLength = 0

        If PlayerBuffer.Length >= 4 Then
            pLength = PlayerBuffer.ReadLong(False)
        End If
    Loop

    PlayerBuffer.Trim

    DoEvents
End Sub

Public Function ConnectToGameServer() As Boolean
    Dim Wait As Long

    ' Check to see if we are already connected, if so just exit
    If IsGameConnected Then
        ConnectToGameServer = True
        Exit Function
    End If

    Wait = GetTickCount
    frmMain.Socket.Close
    frmMain.Socket.Connect

    HideWindows

    SetStatus "Conectando ao servidor."

    ' Wait until connected or 3 seconds have passed and report the server being down
    Do While (Not IsGameConnected) And (GetTickCount <= Wait + 3000)
       DoEvents
    Loop

    ConnectToGameServer = IsGameConnected
    
    HideWindows
    
    SetStatus vbNullString
End Function

Function IsGameConnected() As Boolean

    If frmMain.Socket.State = sckConnected Then
        IsGameConnected = True
    End If

End Function

Function IsPlaying(ByVal Index As Long) As Boolean

' if the player doesn't exist, the name will equal 0
    If LenB(GetPlayerName(Index)) > 0 Then
        IsPlaying = True
    End If

End Function

Public Sub SendGameMessage(ByRef Data() As Byte)
    If IsGameConnected Then
        Dim Length As Long
        Dim Ciphed() As Byte
           
        Ciphed = CreateEncryptedPacket(Data)
       
        Dim Buffer As clsBuffer
        Set Buffer = New clsBuffer
        
        Length = ((UBound(Ciphed) - LBound(Ciphed)) + 1)
               
        Buffer.PreAllocate Length + 4
        Buffer.WriteLong Length
        Buffer.WriteBytes Ciphed()
        
        frmMain.Socket.SendData Buffer.ToArray()

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

    Call AppendCheckSum(GameInstance, ByVal VarPtr(Buffer(0)), 0, BufferLength)

    Call Cipher(GameInstance, ByVal VarPtr(Buffer(0)), BufferLength)

    CreateEncryptedPacket = Buffer

End Function

Public Sub GetPing()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    PingStart = GetTickCount

    Buffer.WriteLong EnginePacket.PCheckPing

    SendGameMessage Buffer.ToArray()

    Set Buffer = Nothing
End Sub



