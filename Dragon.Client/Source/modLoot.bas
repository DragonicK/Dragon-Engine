Attribute VB_Name = "modLoot"
Option Explicit

Private Const LIST_OFFSET_Y = 40
Private Const LIST_COUNT = 6

Private ItemLootCount As Long
Private ItemLoot() As ItemLootRec

Private WindowIndex As Long
Private ListIndex As Long
Private CanSendTakeItem As Boolean

Public Enum ItemLootType
    ItemLootType_Item
    ItemLootType_Currency
End Enum

Private Type ItemLootRec
    LootType As ItemLootType
    CurrencyType As Byte
    Num As Long
    Value As Long
    Level As Long
End Type

Public Sub CreateWindow_Loot()
    Dim i As Long
    ' Create window
    CreateWindow "winLoot", "ITEMS", zOrder_Win, 0, 0, 245, 340, 0, False, Fonts.FontRegular, , 2, 7, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar
    ' Centralise it
    CentraliseWindow WindowCount
    ' Set the index for spawning controls
    zOrder_Con = 1
    ' Close button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf btnMenu_Loot)

    CreatePictureBox WindowCount, "picIcon1", 15, 60 + (LIST_OFFSET_Y * 0), 32, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot1_MouseMove), GetAddress(AddressOf ItemSlot1_MouseDown), GetAddress(AddressOf ItemSlot1_MouseMove)
    CreatePictureBox WindowCount, "picIcon2", 15, 60 + (LIST_OFFSET_Y * 1), 32, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot2_MouseMove), GetAddress(AddressOf ItemSlot2_MouseDown), GetAddress(AddressOf ItemSlot2_MouseMove)
    CreatePictureBox WindowCount, "picIcon3", 15, 60 + (LIST_OFFSET_Y * 2), 32, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot3_MouseMove), GetAddress(AddressOf ItemSlot3_MouseDown), GetAddress(AddressOf ItemSlot3_MouseMove)
    CreatePictureBox WindowCount, "picIcon4", 15, 60 + (LIST_OFFSET_Y * 3), 32, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot4_MouseMove), GetAddress(AddressOf ItemSlot4_MouseDown), GetAddress(AddressOf ItemSlot4_MouseMove)
    CreatePictureBox WindowCount, "picIcon5", 15, 60 + (LIST_OFFSET_Y * 4), 32, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot5_MouseMove), GetAddress(AddressOf ItemSlot5_MouseDown), GetAddress(AddressOf ItemSlot5_MouseMove)
    CreatePictureBox WindowCount, "picIcon6", 15, 60 + (LIST_OFFSET_Y * 5), 32, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot6_MouseMove), GetAddress(AddressOf ItemSlot6_MouseDown), GetAddress(AddressOf ItemSlot6_MouseMove)

    For i = 1 To LIST_COUNT
        CreateLabel WindowCount, "lblItemName" & i, 55, 62 + (LIST_OFFSET_Y * (i - 1)), 160, 20, "", FontRegular, Coral, Alignment.AlignLeft
        CreateLabel WindowCount, "lblItemCount" & i, 55, 74 + (LIST_OFFSET_Y * (i - 1)), 160, 20, "", FontRegular, White, Alignment.AlignLeft
    Next

    CreatePictureBox WindowCount, "picName1", 47, 60 + (LIST_OFFSET_Y * 0), 160, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot1_MouseMove), GetAddress(AddressOf ItemSlot1_MouseDown), GetAddress(AddressOf ItemSlot1_MouseMove)
    CreatePictureBox WindowCount, "picName2", 47, 60 + (LIST_OFFSET_Y * 1), 160, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot2_MouseMove), GetAddress(AddressOf ItemSlot2_MouseDown), GetAddress(AddressOf ItemSlot2_MouseMove)
    CreatePictureBox WindowCount, "picName3", 47, 60 + (LIST_OFFSET_Y * 2), 160, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot3_MouseMove), GetAddress(AddressOf ItemSlot3_MouseDown), GetAddress(AddressOf ItemSlot3_MouseMove)
    CreatePictureBox WindowCount, "picName4", 47, 60 + (LIST_OFFSET_Y * 3), 160, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot4_MouseMove), GetAddress(AddressOf ItemSlot4_MouseDown), GetAddress(AddressOf ItemSlot4_MouseMove)
    CreatePictureBox WindowCount, "picName5", 47, 60 + (LIST_OFFSET_Y * 4), 160, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot5_MouseMove), GetAddress(AddressOf ItemSlot5_MouseDown), GetAddress(AddressOf ItemSlot5_MouseMove)
    CreatePictureBox WindowCount, "picName6", 47, 60 + (LIST_OFFSET_Y * 5), 160, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ItemSlot6_MouseMove), GetAddress(AddressOf ItemSlot6_MouseDown), GetAddress(AddressOf ItemSlot6_MouseMove)

    'Botões setas
    CreateButton WindowCount, "btnCraft", 215, 60, 15, 15, , , , , , , Tex_GUI(44), Tex_GUI(45), Tex_GUI(46), , , , , , GetAddress(AddressOf MoveListToUp)
    CreateButton WindowCount, "btnCraft", 215, 276, 15, 15, , , , , , , Tex_GUI(47), Tex_GUI(48), Tex_GUI(49), , , , , , GetAddress(AddressOf MoveListToDown)

    CreatePictureBox WindowCount, "picName", 15, 305, 215, 22, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox
    CreateLabel WindowCount, "lblItemCount", 15, 308, 215, 22, "Quantidade: 1", FontRegular, White, Alignment.AlignCenter

    WindowIndex = WindowCount
End Sub

Private Sub TakeItem(ByVal Index As Long)
    If Index > ItemLootCount Then
        Exit Sub
    End If

    Dim LootType As ItemLootType
    Dim CanTakeItem As Boolean

    LootType = ItemLoot(Index).LootType

    If LootType = ItemLootType_Item Then
        Dim ItemNum As Long

        ItemNum = ItemLoot(Index).Num
        If ItemNum > 0 Then CanTakeItem = True

    ElseIf LootType = ItemLootType_Currency Then
        Dim CurrencyValue As Long

        CurrencyValue = ItemLoot(Index).Value
        If CurrencyValue > 0 Then CanTakeItem = True
    End If

    ' Verifica se o comando já foi enviado.
    If CanTakeItem Then
        ' Permite que os drops de moeda sejam coletados sem restrições.
        If LootType = ItemLootType_Currency Then CanSendTakeItem = True

        If CanSendTakeItem Then
            Call SendTakeLootItem(Index)
            ' Somente permite que um item seja coletado por vez pela resposta do servidor.
            CanSendTakeItem = False
        End If
    End If

End Sub

Private Sub ItemSlot1_MouseDown()
    Call TakeItem(ListIndex + 1)

    Call ItemSlot1_MouseMove
End Sub
Private Sub ItemSlot2_MouseDown()
    Call TakeItem(ListIndex + 2)

    ItemSlot2_MouseMove
End Sub
Private Sub ItemSlot3_MouseDown()
    Call TakeItem(ListIndex + 3)

    ItemSlot3_MouseMove
End Sub
Private Sub ItemSlot4_MouseDown()
    Call TakeItem(ListIndex + 4)

    ItemSlot4_MouseMove
End Sub
Private Sub ItemSlot5_MouseDown()
    Call TakeItem(ListIndex + 5)

    ItemSlot5_MouseMove
End Sub
Private Sub ItemSlot6_MouseDown()
    Call TakeItem(ListIndex + 6)

    ItemSlot6_MouseMove
End Sub

Private Sub ShowLootDescription(ByVal Index As Long)
    If Index > ItemLootCount Then
        Exit Sub
    End If

    Dim LootType As Long
    Dim X As Long, Y As Long
    Dim WinDescription As Long

    WinDescription = GetWindowIndex("winDescription")

    LootType = ItemLoot(Index).LootType

    ' calc position
    X = Windows(WindowIndex).Window.Left - Windows(WinDescription).Window.Width - 2
    Y = Windows(WindowIndex).Window.Top

    ' offscreen?
    If X < 0 Then
        ' switch to right
        X = Windows(WindowIndex).Window.Left + Windows(WindowIndex).Window.Width + 2
    End If

    If Y + Windows(WinDescription).Window.Height >= ScreenHeight Then
        Y = ScreenHeight - Windows(WinDescription).Window.Height
    End If

    If LootType = ItemLootType.ItemLootType_Item Then
        Dim Inventory As InventoryRec
    
        Inventory.Num = ItemLoot(Index).Num
        Inventory.Level = ItemLoot(Index).Level

        Call ShowItemDesc(X, Y, Inventory)

    ElseIf LootType = ItemLootType.ItemLootType_Currency Then
        Dim CurrencyValue As Long, CurType As CurrencyType

        CurType = ItemLoot(Index).CurrencyType
        CurrencyValue = ItemLoot(Index).Value

        ShowCurrencyDesc X, Y, CurType, CurrencyValue
    End If

End Sub

Private Sub ItemSlot1_MouseMove()
    Call ShowLootDescription(ListIndex + 1)
End Sub

Private Sub ItemSlot2_MouseMove()
    Call ShowLootDescription(ListIndex + 2)
End Sub

Private Sub ItemSlot3_MouseMove()
    Call ShowLootDescription(ListIndex + 3)
End Sub

Private Sub ItemSlot4_MouseMove()
    Call ShowLootDescription(ListIndex + 4)
End Sub

Private Sub ItemSlot5_MouseMove()
    Call ShowLootDescription(ListIndex + 5)
End Sub

Private Sub ItemSlot6_MouseMove()
    Call ShowLootDescription(ListIndex + 6)
End Sub
Private Sub MoveListToUp()
    If ListIndex > 0 Then
        ListIndex = ListIndex - 1
        UpdateList
    End If
End Sub

Private Sub MoveListToDown()
    If ListIndex < (ItemLootCount - LIST_COUNT) Then
        ListIndex = ListIndex + 1
        UpdateList
    End If
End Sub

Private Sub btnMenu_Loot()
    If GetPlayerDead(MyIndex) Then Exit Sub

    Dim curWindow As Long

    curWindow = GetWindowIndex("winLoot")

    If Windows(curWindow).Window.Visible Then
        Call SendCloseLoot
        HideWindow curWindow
    End If

End Sub

Private Sub UpdateList()
    Dim i As Long
    Dim ControlNameIndex As Long
    Dim ControlCountIndex As Long
    Dim ItemNum As Long, Rarity As Long, ItemValue As Long, ItemLevel As Long
    Dim CurrencyType As Byte

    For i = 1 To LIST_COUNT
        ControlNameIndex = GetControlIndex("winLoot", "lblItemName" & i)
        ControlCountIndex = GetControlIndex("winLoot", "lblItemCount" & i)

        If (i + ListIndex) <= ItemLootCount Then
            ItemNum = ItemLoot(i + ListIndex).Num
            ItemValue = ItemLoot(i + ListIndex).Value
            ItemLevel = ItemLoot(i + ListIndex).Level
            CurrencyType = ItemLoot(i + ListIndex).CurrencyType

            If ItemLoot(i + ListIndex).LootType = ItemLootType.ItemLootType_Item Then
                Rarity = GetRarityColor(Item(ItemNum).Rarity)

                If ItemLevel > 0 Then
                    Windows(WindowIndex).Controls(ControlNameIndex).Text = Item(ItemNum).Name & " +" & ItemLevel
                Else
                    Windows(WindowIndex).Controls(ControlNameIndex).Text = Item(ItemNum).Name
                End If

                Windows(WindowIndex).Controls(ControlNameIndex).TextColour = Rarity
                Windows(WindowIndex).Controls(ControlNameIndex).TextColourClick = Rarity
                Windows(WindowIndex).Controls(ControlNameIndex).TextColourHover = Rarity

                Windows(WindowIndex).Controls(ControlCountIndex).Text = "Quantidade: " & ItemValue
                Call SetControlImage(i, Tex_Item(Item(ItemNum).IconId))

            ElseIf ItemLoot(i + ListIndex).LootType = ItemLootType.ItemLootType_Currency Then
               ' Dim CurRec As CurrencyRec
                
               ' CurRec = GetCurrencyData(CurrencyType, True)
                
               ' Windows(WindowIndex).Controls(ControlNameIndex).Text = CurRec.Name
                Windows(WindowIndex).Controls(ControlNameIndex).TextColour = White
                Windows(WindowIndex).Controls(ControlNameIndex).TextColourClick = White
                Windows(WindowIndex).Controls(ControlNameIndex).TextColourHover = White

                Windows(WindowIndex).Controls(ControlCountIndex).Text = "Quantidade: " & ItemValue
                'Call SetControlImage(i, Tex_Item(CurRec.IconId))
            End If
        Else
            Windows(WindowIndex).Controls(ControlNameIndex).Text = ""
            Windows(WindowIndex).Controls(ControlCountIndex).Text = ""

            Call SetControlImage(i, 0)
        End If
    Next

    Windows(WindowIndex).Controls(GetControlIndex("winLoot", "lblItemCount")).Text = "Quantidade: " & ItemLootCount

End Sub

Private Sub SetControlImage(ByVal Index As Long, ByVal TextureNum As Long)
    Dim i As Long, ControlIndex As Long

    ControlIndex = GetControlIndex("winLoot", "picIcon" & Index)

    For i = 0 To entStates.state_Count - 1
        Windows(WindowIndex).Controls(ControlIndex).image(i) = TextureNum
    Next

End Sub

Public Sub HandleCloseLoot(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    HideWindow WindowIndex
End Sub

Public Sub HandleEnableDropTakeItem(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data

    CanSendTakeItem = Buffer.ReadByte

    Set Buffer = Nothing
End Sub

Public Sub HandleOpenLoot(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, i As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    'Reseta a posição da lista.
    ListIndex = 0
    ItemLootCount = Buffer.ReadLong

    If ItemLootCount > 0 Then
        Erase ItemLoot
        ReDim ItemLoot(1 To ItemLootCount)

        For i = 1 To ItemLootCount
            ItemLoot(i).LootType = Buffer.ReadByte
            ItemLoot(i).CurrencyType = Buffer.ReadByte
            ItemLoot(i).Num = Buffer.ReadLong
            ItemLoot(i).Value = Buffer.ReadLong
            ItemLoot(i).Level = Buffer.ReadLong
        Next
    End If

    ' Libera o pacote para envio.
    CanSendTakeItem = True

    Call UpdateList
    ShowWindow GetWindowIndex("winLoot")

    Set Buffer = Nothing

End Sub

Public Sub HandleSortLootList(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim RemovedIndex As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    ' Aumenta em 1 para se adequar ao cliente.
    RemovedIndex = Buffer.ReadLong + 1

    Set Buffer = Nothing

    ' Atualiza a lista.
    Dim i As Long

    If ItemLootCount - 1 > 0 Then
        For i = RemovedIndex To ItemLootCount - 1
            ItemLoot(i) = ItemLoot(i + 1)
        Next

        ReDim Preserve ItemLoot(1 To ItemLootCount - 1)

        ItemLootCount = ItemLootCount - 1

        Call UpdateList
    End If

    ' Libera o pacote para envio.
    CanSendTakeItem = True

End Sub

Public Sub CheckForCloseLoot()
    If Windows(WindowIndex).Window.Visible Then
        HideWindow WindowIndex
        SendCloseLoot
    End If
End Sub

Public Sub SendCloseLoot()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong CCloseLoot

    SendData Buffer.ToArray()

    Set Buffer = Nothing
End Sub

Public Sub SendTakeLootItem(ByVal ItemIndex As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong CTakeLootItem
    Buffer.WriteLong ItemIndex

    SendData Buffer.ToArray()

    Set Buffer = Nothing
End Sub
