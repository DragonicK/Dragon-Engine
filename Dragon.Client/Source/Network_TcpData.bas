Attribute VB_Name = "Network_TcpData"
Option Explicit

' ******************************************
' ** Communcation to server, TCP          **
' ** Winsock Control (mswinsck.ocx)       **
' ** String packets (slow and big)        **
' ******************************************
Private PlayerBuffer As clsBuffer

Public Sub TcpInit(ByVal IP As String, ByVal Port As Long)
    Set PlayerBuffer = Nothing
    Set PlayerBuffer = New clsBuffer

    ' connect
    frmMain.Socket.Close
    frmMain.Socket.RemoteHost = IP
    frmMain.Socket.RemotePort = Port
End Sub

Public Sub DestroyTCP()
    frmMain.Socket.Close
End Sub

Public Sub IncomingData(ByVal DataLength As Long)
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
            HandleData PlayerBuffer.ReadBytes(pLength)
        End If

        pLength = 0

        If PlayerBuffer.Length >= 4 Then
            pLength = PlayerBuffer.ReadLong(False)
        End If
    Loop

    PlayerBuffer.Trim

    DoEvents
End Sub

Public Function ConnectToServer() As Boolean
    Dim Wait As Long

    ' Check to see if we are already connected, if so just exit
    If IsConnected Then
        ConnectToServer = True
        Exit Function
    End If

    Wait = GetTickCount
    frmMain.Socket.Close
    frmMain.Socket.Connect

    HideWindows

    SetStatus "Conectando ao servidor."

    ' Wait until connected or 3 seconds have passed and report the server being down
    Do While (Not IsConnected) And (GetTickCount <= Wait + 3000)
       DoEvents
    Loop

    ConnectToServer = IsConnected
    
    HideWindows
    
    SetStatus vbNullString
End Function

Function IsConnected() As Boolean

    If frmMain.Socket.State = sckConnected Then
        IsConnected = True
    End If

End Function

Function IsPlaying(ByVal Index As Long) As Boolean

' if the player doesn't exist, the name will equal 0
    If LenB(GetPlayerName(Index)) > 0 Then
        IsPlaying = True
    End If

End Function

Sub SendData(ByRef Data() As Byte)

    If IsConnected Then
        Dim Buffer As clsBuffer
        Set Buffer = New clsBuffer
                        
        Buffer.WriteLong (UBound(Data) - LBound(Data)) + 1
        Buffer.WriteBytes Data()

        'Dim TempBuffer() As Byte
        'Dim Key As Byte
        'Dim Iv As Byte
        'Dim KeyIndex As Long
        'Dim IvIndex As Long
        'Dim Success As Boolean
        'Dim ErrorDescription As String
        'Dim Length As Long

        'Key = Rand(0, 255)
        'KeyIndex = Rand(0, KeySize)
        'Iv = Rand(0, 255)
        'IvIndex = Rand(0, IvSize)

        'TempBuffer = ConnectionAES.Encrypt(Data, CreateKey(KeyType_Key, KeyIndex, Key), CreateKey(KeyType_Iv, IvIndex, Iv), Success)

        'If Not Success Then
        '    GoTo Error
        'End If

       ' Length = (UBound(TempBuffer) - LBound(TempBuffer)) + 1

      '  Buffer.PreAllocate Length + 12
      '  Buffer.WriteLong Length + 8
      '  Buffer.WriteLong Length
      '  Buffer.WriteByte Key
      '  Buffer.WriteByte KeyIndex
      '  Buffer.WriteByte Iv
      '  Buffer.WriteByte IvIndex
      '  Buffer.WriteBytes TempBuffer

        frmMain.Socket.SendData Buffer.ToArray()

        Set Buffer = Nothing
    End If
    Exit Sub

Error:
    MsgBox "Ocorreu um erro ao enviar os dados para o servidor."
    DestroyGame
End Sub

Public Sub GetPing()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    PingStart = GetTickCount

    Buffer.WriteLong EnginePacket.PCheckPing

    SendData Buffer.ToArray()

    Set Buffer = Nothing
End Sub



