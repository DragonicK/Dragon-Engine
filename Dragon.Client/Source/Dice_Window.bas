Attribute VB_Name = "Dice_Window"
Option Explicit

Private Const ProgressBarSizeStep As Single = 7.3

Public Type RolledItemRec
    Id As Long
    Value As Long
    Level As Long
    Bound As Boolean
    AttributeId As Long
    UpgradeId As Long
End Type

Private Visible As Boolean
Private WindowIndex As Long
Private RemainingTime As Long
Private RolledItem As RolledItemRec
Private RemainingTimeControlIndex As Long

Public Sub CreateWindow_LootDice()
    Dim i As Long, Y As Long
    ' Create window
    CreateWindow "winDice", "LOTERIA", zOrder_Win, 0, 0, 252, 185, 0, False, Fonts.FontRegular, , 2, 7, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, , , , , , , , , , , GetAddress(AddressOf DrawLootDice)
    ' Centralise it
    CentraliseWindow WindowCount

    Windows(WindowCount).Window.Top = ScreenHeight - 300

    ' Set the index for spawning controls
    zOrder_Con = 1
    ' Close button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf btnMenu_LootDice)

    CreatePictureBox WindowCount, "picIcon", 15, 60, 32, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ShowLootDescription), , GetAddress(AddressOf ShowLootDescription)

    CreateLabel WindowCount, "lblItemName", 55, 62, 190, 20, "", FontRegular, Coral, Alignment.AlignLeft
    CreateLabel WindowCount, "lblItemCount", 55, 74, 190, 20, "", FontRegular, White, Alignment.AlignLeft

    CreatePictureBox WindowCount, "picName", 47, 60, 190, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ShowLootDescription), , GetAddress(AddressOf ShowLootDescription)

    CreateLabel WindowCount, "lblRemainingTime", 15, 95, 220, 20, "Tempo Restante: 180", FontRegular, White, Alignment.AlignRight

    CreateButton WindowCount, "btnRoll", 16, 145, 105, 20, "Rolar", FontRegular, White, , True, , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf Button_RollDice)
    CreateButton WindowCount, "btnPass", 131, 145, 105, 20, "Passar", FontRegular, White, , True, , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf Button_Pass)

    WindowIndex = WindowCount
    RemainingTime = -1

    RemainingTimeControlIndex = GetControlIndex("winDice", "lblRemainingTime")

End Sub

Private Sub DrawLootDice()
    Dim xO As Long, yO As Long

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top

    RenderTexture Tex_GUI(50), xO + 16, yO + 115, 0, 0, 220, 32, 180, 32

    If RemainingTime >= 0 Then
        RenderTexture Tex_GUI(51), xO + 16, yO + 115, 0, 0, CInt(ProgressBarSizeStep * RemainingTime), 32, 180, 32
    End If
End Sub

Private Sub Button_RollDice()
    Call SendRollDiceResult(True)
    
    RemainingTime = -1
    HideWindow WindowIndex
    Visible = False
End Sub

Private Sub Button_Pass()
    Call SendRollDiceResult(False)
    
    RemainingTime = -1
    HideWindow WindowIndex
    Visible = False
End Sub

Private Sub btnMenu_LootDice()
    Dim curWindow As Long

    curWindow = GetWindowIndex("winDice")

    If Windows(curWindow).Window.Visible Then
        Call SendRollDiceResult(False)
        HideWindow curWindow
    End If

End Sub

Private Sub ShowLootDescription()
    If RolledItem.Id <= 0 Then
        Exit Sub
    End If

    Dim X As Long, Y As Long
    Dim WinDescription As Long

    WinDescription = GetWindowIndex("winDescription")

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

    Dim Inventory As InventoryRec

    Inventory.Num = RolledItem.Id
    Inventory.Level = RolledItem.Level
    Inventory.Bound = RolledItem.Bound
    Inventory.UpgradeId = RolledItem.UpgradeId
    Inventory.AttributeId = RolledItem.AttributeId

    Call ShowItemDesc(X, Y, Inventory)

End Sub

Private Sub SetControlImage(ByVal TextureNum As Long)
    Dim i As Long, ControlIndex As Long

    ControlIndex = GetControlIndex("winDice", "picIcon")

    For i = 0 To entStates.state_Count - 1
        Windows(WindowIndex).Controls(ControlIndex).image(i) = TextureNum
    Next

End Sub

Public Sub SetRollDiceVisibility(ByVal IsVisible As Boolean)
    Visible = IsVisible
End Sub

Public Sub UpdateRolledItem(ByVal IncomingRemainingTime As Long, ByRef IncomingItem As RolledItemRec)
    Dim ControlNameIndex As Long
    Dim ControlCountIndex As Long
    Dim ItemNum As Long, ItemValue As Long, ItemLevel As Long, Rarity As Long

    RemainingTime = IncomingRemainingTime
    RolledItem = IncomingItem
    
    ControlNameIndex = GetControlIndex("winDice", "lblItemName")
    ControlCountIndex = GetControlIndex("winDice", "lblItemCount")
        
    ItemNum = IncomingItem.Id
    ItemValue = IncomingItem.Value
    ItemLevel = IncomingItem.Level
    
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
    
    Call SetControlImage(Tex_Item(Item(ItemNum).IconId))

End Sub

Public Sub ProcessRollDice()
    If Visible Then
        If RemainingTime >= 0 Then
            RemainingTime = RemainingTime - 1

            If RemainingTime < 0 Then
                Call SendRollDiceResult(False)
                HideWindow WindowIndex
                Visible = False
            End If

            Windows(WindowIndex).Controls(RemainingTimeControlIndex).Text = "Tempo Restante: " & RemainingTime
        End If
    End If
End Sub



