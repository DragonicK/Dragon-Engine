Attribute VB_Name = "ViewEquipment_Window"
Option Explicit

Private EquipmentPosition(1 To PlayerEquipments.PlayerEquipment_Count - 1) As EquipmentPositionRec

Private WindowIndex As Long

Public Function IsViewEquipmentVisible() As Boolean
    IsViewEquipmentVisible = Windows(WindowIndex).Window.Visible
End Function

Public Sub ShowViewEquipment()
    Windows(WindowIndex).Window.Visible = True
End Sub

Public Sub HideViewEquipment()
    Windows(WindowIndex).Window.Visible = False
End Sub

Public Sub CreateWindow_ViewEquipment()
    Dim i As Long
    ' Create window
    CreateWindow "winViewEquipment", "INFORMAÇÃO", zOrder_Win, 0, 0, 260, 420, 0, False, Fonts.FontRegular, , 2, 6, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, , , , , GetAddress(AddressOf ViewEquipment_MouseMove), GetAddress(AddressOf ViewEquipment_MouseDown), GetAddress(AddressOf ViewEquipment_MouseMove), GetAddress(AddressOf ViewEquipment_DoubleClick), , , GetAddress(AddressOf RenderViewEquipment)
    ' Centralise it
    CentraliseWindow WindowCount, 0

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Close button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf ButtonMenu_CloseViewEquipment)

    CreateButton WindowCount, "btnCharacter", 25, 42, 100, 26, "PERSONAGEM", FontRegular, Green, , , , , , , , , , , , GetAddress(AddressOf ShowCharacter_Click)
    CreateButton WindowCount, "btnHeraldry", 135, 42, 100, 26, "BRASÃO", FontRegular, , , , , , , , , , , , , GetAddress(AddressOf ShowHeraldry_Click)

    ' Labels
    CreateLabel WindowCount, "lblName", 50, 90, 156, 16, "NOME LV. 50", FontRegular, White, Alignment.AlignCenter
    CreateLabel WindowCount, "lblClass", 50, 170, 156, 16, "PRIEST", FontRegular, White, Alignment.AlignCenter

    WindowIndex = WindowCount
    
    SetAllEquipmentPosition
End Sub

Private Sub SetAllEquipmentPosition()
    Dim BaseX As Long, BaseY As Long

    BaseX = 17
    BaseY = 90

    Call SetEquipmentPosition(EquipWeapon, BaseX + 75, BaseY + 100)
    Call SetEquipmentPosition(EquipShield, BaseX + 115, BaseY + 100)

    Call SetEquipmentPosition(EquipHelmet, BaseX, BaseY)
    Call SetEquipmentPosition(EquipNecklace, BaseX, BaseY + 40)
    Call SetEquipmentPosition(EquipShoulder, BaseX, BaseY + 80)
    Call SetEquipmentPosition(EquipArmor, BaseX, BaseY + 120)
    Call SetEquipmentPosition(EquipGloves, BaseX, BaseY + 160)
    Call SetEquipmentPosition(EquipBelt, BaseX, BaseY + 200)
    Call SetEquipmentPosition(EquipPants, BaseX, BaseY + 240)
    Call SetEquipmentPosition(EquipBoot, BaseX, BaseY + 280)

    Call SetEquipmentPosition(EquipBracelet_1, BaseX + 190, BaseY)
    Call SetEquipmentPosition(EquipBracelet_2, BaseX + 190, BaseY + 40)
    Call SetEquipmentPosition(EquipEarring_1, BaseX + 190, BaseY + 80)
    Call SetEquipmentPosition(EquipEarring_2, BaseX + 190, BaseY + 120)
    Call SetEquipmentPosition(EquipRing_1, BaseX + 190, BaseY + 160)
    Call SetEquipmentPosition(EquipRing_2, BaseX + 190, BaseY + 200)
    Call SetEquipmentPosition(EquipCostume, BaseX + 190, BaseY + 240)
    Call SetEquipmentPosition(EquipMount, BaseX + 190, BaseY + 280)

End Sub

Private Sub SetEquipmentPosition(ByVal Equip As PlayerEquipments, ByVal X As Long, ByVal Y As Long)
    EquipmentPosition(Equip).X = X
    EquipmentPosition(Equip).Y = Y
End Sub
    
Private Sub ShowCharacter_Click()
    Dim CharacterIndex As Long
    Dim HeraldryIndex As Long

    CharacterIndex = GetControlIndex("winViewEquipment", "btnCharacter")
    HeraldryIndex = GetControlIndex("winViewEquipment", "btnHeraldry")

    Windows(WindowIndex).Controls(CharacterIndex).TextColour = Green
    Windows(WindowIndex).Controls(CharacterIndex).TextColourClick = Green
    Windows(WindowIndex).Controls(CharacterIndex).TextColourHover = Green

    Windows(WindowIndex).Controls(HeraldryIndex).TextColour = White
    Windows(WindowIndex).Controls(HeraldryIndex).TextColourClick = White
    Windows(WindowIndex).Controls(HeraldryIndex).TextColourHover = White
End Sub

Private Sub ShowHeraldry_Click()
    Dim CharacterIndex As Long
    Dim HeraldryIndex As Long

    CharacterIndex = GetControlIndex("winViewEquipment", "btnCharacter")
    HeraldryIndex = GetControlIndex("winViewEquipment", "btnHeraldry")

    Windows(WindowIndex).Controls(CharacterIndex).TextColour = White
    Windows(WindowIndex).Controls(CharacterIndex).TextColourClick = White
    Windows(WindowIndex).Controls(CharacterIndex).TextColourHover = White

    Windows(WindowIndex).Controls(HeraldryIndex).TextColour = Green
    Windows(WindowIndex).Controls(HeraldryIndex).TextColourClick = Green
    Windows(WindowIndex).Controls(HeraldryIndex).TextColourHover = Green
End Sub

Private Sub ButtonMenu_CloseViewEquipment()
    Windows(WindowIndex).Window.Visible = False
    
    Call ClearViewEquipment
End Sub

Private Sub ViewEquipment_MouseDown()
    ViewEquipment_MouseMove
End Sub

Private Sub ViewEquipment_MouseMove()
    Dim EquipSlot As PlayerEquipments, X As Long, Y As Long

    ' exit out early if dragging
    If DragBox.Type <> PartNone Then Exit Sub

    EquipSlot = GetEquipmentSlotFromPosition(Windows(WindowIndex).Window.Left, Windows(WindowIndex).Window.Top)

    If EquipSlot <= 0 Or EquipSlot > PlayerEquipments.PlayerEquipment_Count - 1 Then
        Exit Sub
    End If

    ' calc position
    X = Windows(WindowIndex).Window.Left - Windows(GetWindowIndex("winDescription")).Window.Width - 2
    Y = Windows(WindowIndex).Window.Top

    ' offscreen?
    If X < 0 Then
        ' switch to right
        X = Windows(WindowIndex).Window.Left + Windows(WindowIndex).Window.Width + 2
    End If

    Dim vEquipment As PlayerEquipmentRec

    vEquipment = GetViewEquipment(EquipSlot)

    If vEquipment.Num > 0 Then
        Dim Inventory As InventoryRec

        Inventory.Num = vEquipment.Num
        Inventory.Value = 1
        Inventory.Level = vEquipment.Level
        Inventory.Bound = 0
        Inventory.AttributeId = vEquipment.AttributeId
        Inventory.UpgradeId = vEquipment.UpgradeId

        ShowViewEquipmentDescription X, Y, Inventory
    End If

End Sub

Private Sub ViewEquipment_DoubleClick()
    ViewEquipment_MouseMove
End Sub

Private Sub RenderViewEquipment()
    Dim xO As Long, yO As Long, Width As Long
    Dim i As Long, ItemNum As Long

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top
    Width = Windows(WindowIndex).Window.Width

    

    For i = 1 To PlayerEquipments.PlayerEquipment_Count - 1
        RenderTexture Tex_GUI(54 + i), xO + EquipmentPosition(i).X, yO + EquipmentPosition(i).Y, 0, 0, 34, 34, 34, 34

        ItemNum = GetViewEquipment(i).Num

        If ItemNum > 0 And ItemNum <= MaximumItems Then
            RenderTexture Tex_Item(Item(ItemNum).IconId), xO + EquipmentPosition(i).X - 1, yO + EquipmentPosition(i).Y - 1, 0, 0, PIC_X, PIC_Y, PIC_X, PIC_Y
        End If
    Next

End Sub

Private Function GetEquipmentSlotFromPosition(StartX As Long, StartY As Long) As Long
    Dim TempRec As RECT
    Dim i As Long

    For i = 1 To PlayerEquipments.PlayerEquipment_Count - 1
        If GetViewEquipment(i).Num > 0 Then
            With TempRec
                .Top = StartY + EquipmentPosition(i).Y
                .Bottom = .Top + PIC_Y
                .Left = StartX + EquipmentPosition(i).X
                .Right = .Left + PIC_X
            End With

            If currMouseX >= TempRec.Left And currMouseX <= TempRec.Right Then
                If currMouseY >= TempRec.Top And currMouseY <= TempRec.Bottom Then
                    GetEquipmentSlotFromPosition = i
                    Exit Function
                End If
            End If
        End If
    Next
End Function

Public Sub UpdateViewEquipmentWindow()
    Dim ControlIndex As Long
    
    Windows(WindowIndex).Window.Text = UCase$(ViewPlayerName) & "' INFORMAÇÃO"

    ControlIndex = GetControlIndex("winViewEquipment", "lblName")
    
    Windows(WindowIndex).Controls(ControlIndex).Text = UCase$(ViewPlayerName) & " Lv. " & ViewPlayerLevel

    ControlIndex = GetControlIndex("winViewEquipment", "lblClass")
    
    Windows(WindowIndex).Controls(ControlIndex).Text = UCase$(GetClassName(ViewPlayerClassCode))

End Sub
