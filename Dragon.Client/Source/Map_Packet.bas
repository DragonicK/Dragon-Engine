Attribute VB_Name = "Map_Packet"
Option Explicit

Public Sub SendPlayerRequestNewMap()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    ' Buffer.WriteLong CRequestNewMap
    ' Buffer.WriteLong GetPlayerDir(MyIndex)
    SendGameMessage Buffer.ToArray()
    Set Buffer = Nothing
End Sub

Public Sub HandleLoadMap(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    GettingMap = True

    Dim Buffer As clsBuffer, MapNum As Long
    Dim Key() As Byte, IV() As Byte
    Dim Length As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    MapNum = Buffer.ReadLong

    Length = Buffer.ReadLong
    Key = Buffer.ReadBytes(Length)

    Length = Buffer.ReadLong
    IV = Buffer.ReadBytes(Length)

    Set Buffer = Nothing

    Call ClearMapNpcs
    Call ClearMap
    Call ClearChests

    Call CopyMapProperty(MapNum)
    Call LoadMapParallax(MapNum, Key, IV)

    Call HandleMapDone

    UpdateMapView
End Sub

Public Sub HandleGettingMap(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    
    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    GettingMap = Buffer.ReadByte
    
    Set Buffer = Nothing
End Sub

Private Sub HandleMapDone()
    Dim i As Long
    Dim MusicFile As String
    
    InWarehouse = False
    InTrade = False
    
    CloseShop

    ' clear the action msgs
    For i = 1 To MAX_BYTE
        ClearActionMsg (i)
    Next i

    Action_HighIndex = 1

    ' player music
    ' If InGame Then
    MusicFile = Trim$(CurrentMap.MapData.Music)

    If Not MusicFile = "None." Then
        Play_Music MusicFile
    Else
        Stop_Music
    End If

    ' now cache the positions
    GettingMap = False
    CanMoveNow = True
End Sub
