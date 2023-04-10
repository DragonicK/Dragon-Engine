Attribute VB_Name = "ChestLoot_Packet"
Option Explicit

Public Sub SendCloseChest()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong PCloseChest

    SendData Buffer.ToArray()

    Set Buffer = Nothing
End Sub

Public Sub SendTakeItemFromChest(ByVal ItemIndex As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong PTakeItemFromChest
    Buffer.WriteLong ItemIndex

    SendData Buffer.ToArray()

    Set Buffer = Nothing
End Sub

Public Sub HandleCloseChest(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    HideWindow GetWindowIndex("winChestItem")
End Sub

Public Sub HandleEnableChestTakeItem(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    CanSendTakeItemFromChest = True
End Sub

Public Sub HandleChestItemList(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, i As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    'Reseta a posição da lista.
    ChestItemListIndex = 0
    ChestItemCount = Buffer.ReadLong

    If ChestItemCount > 0 Then
        Erase ChestItem
        ReDim ChestItem(1 To ChestItemCount)

        For i = 1 To ChestItemCount
            ChestItem(i).ItemType = Buffer.ReadLong
            ChestItem(i).CurrencyType = Buffer.ReadLong
            ChestItem(i).Num = Buffer.ReadLong
            ChestItem(i).Value = Buffer.ReadLong
            ChestItem(i).Level = Buffer.ReadLong
            ChestItem(i).UpgradeId = Buffer.ReadLong
            ChestItem(i).AttributeId = Buffer.ReadLong
            ChestItem(i).Bound = Buffer.ReadByte
        Next
    End If

    ' Libera o pacote para envio.
    CanSendTakeItemFromChest = True

    Call UpdateChestItemList
    
    ShowWindow GetWindowIndex("winChestItem")

    Set Buffer = Nothing

End Sub

Public Sub HandleSortChestItemList(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim RemovedIndex As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    ' Aumenta em 1 para se adequar ao cliente.
    RemovedIndex = Buffer.ReadLong + 1

    Set Buffer = Nothing

    ' Atualiza a lista.
    Dim i As Long

    If ChestItemCount - 1 > 0 Then
        For i = RemovedIndex To ChestItemCount - 1
            ChestItem(i) = ChestItem(i + 1)
        Next

        ReDim Preserve ChestItem(1 To ChestItemCount - 1)

        ChestItemCount = ChestItemCount - 1

        Call UpdateChestItemList
    End If

    ' Libera o pacote para envio.
    CanSendTakeItemFromChest = True

End Sub

Public Sub HandleUpdateChestItemList(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim WindowIndex As Long

    WindowIndex = GetWindowIndex("winChestItem")

    If Windows(WindowIndex).Window.Visible Then
        Dim Buffer As clsBuffer

        Set Buffer = New clsBuffer
        Buffer.WriteBytes Data

        Index = Buffer.ReadLong + 1

        If Index > 0 Then
            ChestItem(Index).ItemType = Buffer.ReadLong
            ChestItem(Index).CurrencyType = Buffer.ReadLong
            ChestItem(Index).Num = Buffer.ReadLong
            ChestItem(Index).Value = Buffer.ReadLong
            ChestItem(Index).Level = Buffer.ReadLong
            ChestItem(Index).UpgradeId = Buffer.ReadLong
            ChestItem(Index).AttributeId = Buffer.ReadLong
            ChestItem(Index).Bound = Buffer.ReadByte
        End If

        Set Buffer = Nothing

        ' Libera o pacote para envio.
        CanSendTakeItemFromChest = True

        Call UpdateChestItemList
    End If


End Sub

