Attribute VB_Name = "DragBox_Implementation"
Option Explicit

Public Sub DragBox_OnDraw()
    Dim xO As Long, yO As Long, texNum As Long, WinIndex As Long

    WinIndex = GetWindowIndex("winDragBox")
    xO = Windows(WinIndex).Window.Left
    yO = Windows(WinIndex).Window.Top

    ' get texture num
    With DragBox
        Select Case .Type
        Case PartItem
            If .Value Then
                texNum = Tex_Item(Item(.Value).IconId)
            End If
        Case PartSpell
            If .Value Then
                texNum = Tex_Spellicon(Skill(.Value).IconId)
            End If
        End Select
    End With

    ' draw texture
    RenderTexture texNum, xO, yO, 0, 0, 32, 32, 32, 32
End Sub

Public Sub DragBox_Check()
    Dim WinIndex As Long, i As Long, curWindow As Long, curControl As Long, tmpRec As RECT

    WinIndex = GetWindowIndex("winDragBox")

    ' can't drag nuthin'
    If DragBox.Type = PartNone Then Exit Sub

    ' check for other windows
    For i = 1 To WindowCount
        With Windows(i).Window
            If .Visible Then
                ' can't drag to self
                If .Name <> "winDragBox" Then
                    If currMouseX >= .Left And currMouseX <= .Left + .Width Then
                        If currMouseY >= .Top And currMouseY <= .Top + .Height Then
                            If curWindow = 0 Then curWindow = i
                            If .zOrder > Windows(curWindow).Window.zOrder Then curWindow = i
                        End If
                    End If
                End If
            End If
        End With
    Next

    ' we have a window - check if we can drop
    If curWindow Then
        Select Case Windows(curWindow).Window.Name
        Case "winWarehouse"
            If DragBox.Origin = OriginWarehouse Then
                Call DragBox_WarehouseToWarehouse
            End If

            If DragBox.Origin = OriginInventory Then
                If DragBox.Type = PartItem Then

                    If Item(GetInventoryItemNum(DragBox.Slot)).Stackable = 0 Then
                        SendDepositItem DragBox.Slot, 1
                    Else
                        ShowDialogue "Depositar Item", "Insira a quantidade para deposito", "", DialogueTypeDepositItem, DialogueStyleInput, DragBox.Slot
                    End If

                End If
            End If

        Case "winInventory"
            ' Heraldry to Inventory
            If DragBox.Origin = OriginHeraldry Then
                Call DragBox_CheckHeraldryToInventory
            End If

            If DragBox.Origin = OriginUpgrade Then
                Call DragBox_CheckItemUpgradeToInventory
            End If

            If DragBox.Origin = OriginInventory Then
                ' it's from the inventory!
                If DragBox.Type = PartItem Then
                    ' find the slot to switch with
                    For i = 1 To MaxInventoryPerTab
                        With tmpRec
                            .Top = Windows(curWindow).Window.Top + InvTop + ((InvOffsetY + 32) * ((i - 1) \ InvColumns))
                            .Bottom = .Top + 32
                            .Left = Windows(curWindow).Window.Left + InvLeft + ((InvOffsetX + 32) * (((i - 1) Mod InvColumns)))
                            .Right = .Left + 32
                        End With

                        If currMouseX >= tmpRec.Left And currMouseX <= tmpRec.Right Then
                            If currMouseY >= tmpRec.Top And currMouseY <= tmpRec.Bottom Then
                                ' switch the slots
                                If DragBox.Slot <> i + (InventoryentoryTabIndex * MaxInventoryPerTab) And CanSwapInvItems Then SendSwapInventory DragBox.Slot, i + (InventoryentoryTabIndex * MaxInventoryPerTab)
                                Exit For
                            End If
                        End If
                    Next
                End If
            End If

            If DragBox.Origin = OriginWarehouse Then
                If DragBox.Type = PartItem Then
                    If Item(GetWarehouseItemNum(DragBox.Slot)).Stackable = 0 Then
                        SendWithdrawItem DragBox.Slot, 1
                    Else
                        ShowDialogue "Retirar Item", "Insira a quantidade que deseja retirar", "", DialogueTypeWithdrawItem, DialogueStyleInput, DragBox.Slot
                    End If
                End If
            End If

        Case "winHotbar"
            Call DragBox_CheckHotBar(curWindow)

        Case "winItemUpgrade"
            Call DragBox_CheckInventoryToItemUpgrade

        Case "winHeraldry"
            Call DragBox_CheckInventoryToHeraldry
            
        Case "winMail"
            Call DragBox_CheckInventoryToMail

        End Select
    Else
        ' no windows found - dropping on bare map
        Select Case DragBox.Origin
        Case PartTypeOrigins.OriginInventory
            ShowDialogue "DESTRUIR ITEM", "Deseja realmente destruir o item?", "", DialogueTypeDestroyItem, DialogueStyleYesNo, DragBox.Slot
            
        Case PartTypeOrigins.OriginSpells
            ' dialogue
            
        Case PartTypeOrigins.OriginQuickSlot
            SendQuickSlotChange 0, 0, DragBox.Slot
            
        End Select
    End If

    ' close window
    HideWindow WinIndex
    
    With DragBox
        .Type = PartNone
        .Slot = 0
        .Origin = OriginNone
        .Value = 0
    End With
End Sub

Private Sub DragBox_CheckHotBar(ByVal curWindow As Long)
    Dim i As Long
    Dim tmpRec As RECT

    If DragBox.Origin <> OriginNone Then
        If DragBox.Type <> PartNone Then
            ' find the slot
            For i = 1 To MaximumQuickSlot
                With tmpRec
                    .Top = Windows(curWindow).Window.Top + HotbarTop
                    .Bottom = .Top + 32
                    .Left = Windows(curWindow).Window.Left + HotbarLeft + ((i - 1) * HotbarOffsetX)
                    .Right = .Left + 32
                End With

                If currMouseX >= tmpRec.Left And currMouseX <= tmpRec.Right Then
                    If currMouseY >= tmpRec.Top And currMouseY <= tmpRec.Bottom Then
                        ' set the hotbar slot

                        If DragBox.Origin <> OriginQuickSlot Then
                            If DragBox.Type = PartItem Then
                                SendQuickSlotChange 1, DragBox.Slot, i
                            ElseIf DragBox.Type = PartSpell Then
                                SendQuickSlotChange 2, DragBox.Slot, i
                            End If
                        Else
                            ' SWITCH the hotbar slots
                            If DragBox.Slot <> i Then SwitchHotbar DragBox.Slot, i
                        End If
                        ' exit early
                        Exit For
                    End If
                End If
            Next
        End If
    End If
End Sub
