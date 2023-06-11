Attribute VB_Name = "Title_Packet"
Option Explicit

Public Sub SendSelectedTitle(ByVal Index As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PSelectedTitle
    Buffer.WriteLong Index
    
    SendGameMessage Buffer.ToArray()

    Set Buffer = Nothing
End Sub

Public Sub HandlePlayerTitles(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    If MaxPlayerTitles < 1 Then
        Exit Sub
    End If
    
    Call ClearTitles

    Dim i As Long, TitleCount As Long
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data
    
    TitleCount = Buffer.ReadLong

    For i = 1 To TitleCount
        Call SetTitle(i, Buffer.ReadLong)
    Next
    
    Buffer.Flush
    Set Buffer = Nothing

    Call UpdateTitleCount(TitleCount)
    Call AllocateTitleAttributeDescription
End Sub

Public Sub HandleTitle(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim Id As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    Index = Buffer.ReadLong
    Id = Buffer.ReadLong
    
    Call SetPlayerTitle(Index, Id)

    Set Buffer = Nothing

    If Index = MyIndex Then
        Call UpdateActiveTitle(GetPlayerTitle(MyIndex))
    End If
End Sub
