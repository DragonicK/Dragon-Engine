Attribute VB_Name = "Heraldry_Window"
Option Explicit

Private WindowIndex As Long

Public Sub CreateWindow_Heraldry()
    Dim i As Long

    ' Create window
    CreateWindow "winHeraldry", "BRASÃO", zOrder_Win, 0, 0, 280, 350, 0, False, Fonts.FontRegular, , 2, 7, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, , , , , , , , , , , GetAddress(AddressOf RenderHeraldry)
    ' Centralise it
    CentraliseWindow WindowCount
    ' Set the index for spawning controls
    zOrder_Con = 1

    WindowIndex = WindowCount
    InitializeHeraldryPositions

    ' Close button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf btnMenu_Heraldry)

    For i = 1 To MaxPlayerHeraldry
        CreatePictureBox WindowCount, "picBlank", GetHeraldryPositionX(i), GetHeraldryPositionY(i), 34, 34, , , , , , , , , , , , GetAddress(AddressOf Heraldry_MouseMove), GetAddress(AddressOf Heraldry_MouseDown), GetAddress(AddressOf Heraldry_MouseMove)
    Next

End Sub

Private Sub SetHeraldryPosition(ByVal Index As Long, ByVal X As Long, ByVal Y As Long)
    Call SetHeraldryPositionX(Index, X)
    Call SetHeraldryPositionY(Index, Y)
End Sub

Private Sub InitializeHeraldryPositions()
    Dim X As Long, Y As Long, WindowIndex As Long

    WindowIndex = GetWindowIndex("winHeraldry")

    X = Windows(WindowIndex).Window.Left
    Y = Windows(WindowIndex).Window.Top

    ' Left Higher Top
    Call SetHeraldryPosition(1, 25, 60)
    ' Left Higher Bottom
    Call SetHeraldryPosition(2, 25, 280)
    ' Left Top
    Call SetHeraldryPosition(3, 38, 110)
    Call SetHeraldryPosition(4, 75, 65)
    ' Right top
    Call SetHeraldryPosition(5, 207, 110)
    Call SetHeraldryPosition(6, 168, 65)
    ' Left Middle Top
    Call SetHeraldryPosition(7, 84, 131)
    ' Right Middle Top
    Call SetHeraldryPosition(8, 162, 131)
    ' Middle
    Call SetHeraldryPosition(9, 123, 169)
    ' Left Middle Bottom
    Call SetHeraldryPosition(10, 84, 208)
    ' Right Middle Bottom
    Call SetHeraldryPosition(11, 162, 208)
    ' Left Bottom
    Call SetHeraldryPosition(12, 38, 230)
    Call SetHeraldryPosition(13, 75, 275)
    ' Right Bottom
    Call SetHeraldryPosition(14, 207, 230)
    Call SetHeraldryPosition(15, 168, 275)
    ' Right Higher Top
    Call SetHeraldryPosition(16, 220, 60)
    ' Right Higher Bottom
    Call SetHeraldryPosition(17, 220, 280)
End Sub

Private Sub Heraldry_MouseMove()
    Dim ItemIndex As Long
    Dim X As Long
    Dim Y As Long

    ItemIndex = GetHeraldrySlotFromPosition(Windows(WindowIndex).Window.Left, Windows(WindowIndex).Window.Top)

    If ItemIndex > 0 Then
        X = Windows(WindowIndex).Window.Left - Windows(GetWindowIndex("winDescription")).Window.Width - 2
        Y = Windows(WindowIndex).Window.Top

        ' offscreen?
        If X < 0 Then
            ' switch to right
            X = Windows(WindowIndex).Window.Left + Windows(WindowIndex).Window.Width + 2
        End If

        Call ShowHeraldryDescription(X, Y, GetHeraldry(ItemIndex), -1)
    End If

End Sub

Private Sub Heraldry_MouseDown()
    Dim ItemIndex As Long

    ItemIndex = GetHeraldrySlotFromPosition(Windows(WindowIndex).Window.Left, Windows(WindowIndex).Window.Top)

    ' Desequipa com o click direito.
    If (mouseClick(VK_RBUTTON) And lastMouseClick(VK_RBUTTON) = 0) Then
        If ItemIndex > 0 Then
            SendUnequipHeraldry ItemIndex
        End If

        Exit Sub
    End If

    If ItemIndex > 0 Then
        ' drag it
        With DragBox
            .Type = PartItem
            .Value = GetHeraldryItemId(ItemIndex)
            .Origin = OriginHeraldry
            .Slot = ItemIndex
        End With

        Dim WinIndex As Long

        WinIndex = GetWindowIndex("winDragBox")
        With Windows(WinIndex).Window
            .State = MouseDown
            .Left = lastMouseX - 16
            .Top = lastMouseY - 16
            .MovedX = clickedX - .Left
            .MovedY = clickedY - .Top
        End With

        ShowWindow WinIndex, , False
        ' stop dragging inventory
        Windows(GetWindowIndex("winHeraldry")).Window.State = Normal
    End If

End Sub

Private Function GetHeraldrySlotFromPosition(StartX As Long, StartY As Long) As Long
    Dim TempRec As RECT
    Dim i As Long

    For i = 1 To MaxPlayerHeraldry
        If GetHeraldryItemId(i) > 0 Then
            With TempRec
                .Top = StartY + GetHeraldryPositionY(i)
                .Bottom = .Top + PIC_Y
                .Left = StartX + GetHeraldryPositionX(i)
                .Right = .Left + PIC_X
            End With

            If currMouseX >= TempRec.Left And currMouseX <= TempRec.Right Then
                If currMouseY >= TempRec.Top And currMouseY <= TempRec.Bottom Then
                    GetHeraldrySlotFromPosition = i
                    Exit Function
                End If
            End If
        End If
    Next
End Function

Private Function IsMouseWithHeraldryItem() As Long
    Dim TempRec As RECT
    Dim i As Long, X As Long, Y As Long

    X = Windows(WindowIndex).Window.Left
    Y = Windows(WindowIndex).Window.Top

    For i = 1 To MaxPlayerHeraldry
        With TempRec
            .Top = Y + GetHeraldryPositionY(i)
            .Bottom = .Top + PIC_Y
            .Left = X + GetHeraldryPositionX(i)
            .Right = .Left + PIC_X
        End With

        If currMouseX >= TempRec.Left And currMouseX <= TempRec.Right Then
            If currMouseY >= TempRec.Top And currMouseY <= TempRec.Bottom Then
                IsMouseWithHeraldryItem = i
                Exit Function
            End If
        End If
    Next
End Function

Private Sub RenderHeraldry()
    Dim i As Long, IconId As Long
    Dim xO As Long, yO As Long

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top

    For i = 1 To MaxPlayerHeraldry
        RenderTexture Tex_GUI(27), xO + GetHeraldryPositionX(i), yO + GetHeraldryPositionY(i), 0, 0, 34, 34, 34, 34

        If GetHeraldryItemId(i) > 0 Then
            IconId = Item(GetHeraldryItemId(i)).IconId

            If Not (DragBox.Origin = OriginHeraldry And DragBox.Slot = i) Then
                If IconId > 0 Then
                    RenderTexture Tex_Item(IconId), xO + GetHeraldryPositionX(i) + 1, yO + GetHeraldryPositionY(i) + 2, 0, 0, 32, 32, 32, 32
                End If
            End If
        End If
    Next

End Sub

Public Sub btnMenu_Heraldry()
    If MyIndex > 0 Then
        If GetPlayerDead(MyIndex) Then Exit Sub
    End If

    If Windows(WindowIndex).Window.Visible Then
        HideWindow WindowIndex
    Else
        ShowWindow WindowIndex, , False
    End If
End Sub

Public Sub DragBox_CheckHeraldryToInventory()

    If DragBox.Slot > 0 Then
        Call SendUnequipHeraldry(DragBox.Slot)
    End If

End Sub

Public Sub DragBox_CheckInventoryToHeraldry()

    If DragBox.Origin = OriginInventory And DragBox.Type = PartItem Then
        If DragBox.Slot > 0 Then
            If DragBox.Value > 0 Then
                Dim HeraldrySlot As Long
                HeraldrySlot = IsMouseWithHeraldryItem

                If Item(DragBox.Value).Type = ItemType.ItemType_Heraldry Then
                    Call SendEquipHeraldryIndex(HeraldrySlot, DragBox.Slot)
                End If
            End If
        End If
    End If

End Sub


