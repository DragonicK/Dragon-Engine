Attribute VB_Name = "modGameLogic"
Option Explicit



Public Function ConvertCurrency(ByVal Amount As Long) As String

    If Int(Amount) < 10000 Then
        ConvertCurrency = Amount
    ElseIf Int(Amount) < 999999 Then
        ConvertCurrency = Int(Amount / 1000) & "k"
    ElseIf Int(Amount) < 999999999 Then
        ConvertCurrency = Int(Amount / 1000000) & "m"
    Else
        ConvertCurrency = Int(Amount / 1000000000) & "b"
    End If

End Function


Public Sub CheckAnimInstance(ByVal Index As Long)
    Dim Looptime As Long
    Dim Layer As Long
    Dim FrameCount As Long

    ' if doesn't exist then exit sub
    If AnimInstance(Index).Animation <= 0 Then Exit Sub
    If AnimInstance(Index).Animation >= MAX_ANIMATIONS Then Exit Sub

    For Layer = 0 To 1

        If AnimInstance(Index).Used(Layer) Then
            Looptime = Animation(AnimInstance(Index).Animation).Looptime(Layer)
            FrameCount = Animation(AnimInstance(Index).Animation).FrameCount(Layer)

            ' if zero'd then set so we don't have extra loop and/or frame
            If AnimInstance(Index).FrameIndex(Layer) = 0 Then AnimInstance(Index).FrameIndex(Layer) = 1
            If AnimInstance(Index).LoopIndex(Layer) = 0 Then AnimInstance(Index).LoopIndex(Layer) = 1

            ' check if frame timer is set, and needs to have a frame change
            If AnimInstance(Index).Timer(Layer) + Looptime <= GetTickCount Then

                ' check if out of range
                If AnimInstance(Index).FrameIndex(Layer) >= FrameCount Then
                    AnimInstance(Index).LoopIndex(Layer) = AnimInstance(Index).LoopIndex(Layer) + 1

                    If AnimInstance(Index).LoopIndex(Layer) > Animation(AnimInstance(Index).Animation).LoopCount(Layer) Then
                        AnimInstance(Index).Used(Layer) = False
                    Else
                        AnimInstance(Index).FrameIndex(Layer) = 1
                    End If

                Else
                    AnimInstance(Index).FrameIndex(Layer) = AnimInstance(Index).FrameIndex(Layer) + 1
                End If

                AnimInstance(Index).Timer(Layer) = GetTickCount
            End If
        End If
    Next

    ' if neither layer is used, clear
    If AnimInstance(Index).Used(0) = False And AnimInstance(Index).Used(1) = False Then ClearAnimInstance (Index)
End Sub


Public Sub AddChatBubble(ByVal Target As Long, ByVal TargetType As Byte, ByVal Msg As String, ByVal Colour As Long)
    Dim i As Long, Index As Long
    ' set the global index
    ChatBubbleIndex = ChatBubbleIndex + 1

    ' reset to yourself for eventing
    If TargetType = 0 Then
        TargetType = TargetTypePlayer
        If Target = 0 Then Target = MyIndex
    End If

    If ChatBubbleIndex < 1 Or ChatBubbleIndex > MAX_BYTE Then ChatBubbleIndex = 1
    ' default to new bubble
    Index = ChatBubbleIndex

    ' loop through and see if that player/npc already has a chat bubble
    For i = 1 To MAX_BYTE
        If ChatBubble(i).TargetType = TargetType Then
            If ChatBubble(i).Target = Target Then
                ' reset master index
                If ChatBubbleIndex > 1 Then ChatBubbleIndex = ChatBubbleIndex - 1
                ' we use this one now, yes?
                Index = i
                Exit For
            End If
        End If
    Next

    ' set the bubble up
    With ChatBubble(Index)
        .Target = Target
        .TargetType = TargetType
        .Msg = Msg
        .Colour = Colour
        .Timer = GetTickCount
        .Active = True
    End With
End Sub

Public Sub FindNearestTarget()
    Dim i As Long, X As Long, Y As Long, x2 As Long, y2 As Long, xDif As Long, yDif As Long
    Dim bestX As Long, bestY As Long, bestIndex As Long
    x2 = GetPlayerX(MyIndex)
    y2 = GetPlayerY(MyIndex)
    bestX = 255
    bestY = 255

    For i = 1 To Npc_HighIndex

        If MapNpc(i).Num > 0 Then
            If Not GetNpcDead(i) Then
                X = MapNpc(i).X
                Y = MapNpc(i).Y

                ' find the difference - x
                If X < x2 Then
                    xDif = x2 - X
                ElseIf X > x2 Then
                    xDif = X - x2
                Else
                    xDif = 0
                End If

                ' find the difference - y
                If Y < y2 Then
                    yDif = y2 - Y
                ElseIf Y > y2 Then
                    yDif = Y - y2
                Else
                    yDif = 0
                End If

                ' best so far?
                If (xDif + yDif) < (bestX + bestY) Then
                    bestX = xDif
                    bestY = yDif
                    bestIndex = i
                End If
            End If
        End If

    Next

    ' target the best
    If bestIndex > 0 And bestIndex <> MyTargetIndex Then
        SendPlayerTarget bestIndex, TargetTypeNpc
        Call OpenTargetWindow
    End If
End Sub

Public Sub FindTarget()
    Dim i As Long, X As Long, Y As Long

    ' check players
    For i = 1 To Player_HighIndex

        If IsPlaying(i) And GetPlayerMap(MyIndex) = GetPlayerMap(i) Then
            X = (GetPlayerX(i) * 32) + Player(i).xOffset + 32
            Y = (GetPlayerY(i) * 32) + Player(i).yOffset + 32

            If X >= GlobalX_Map And X <= GlobalX_Map + 32 Then
                If Y >= GlobalY_Map And Y <= GlobalY_Map + 32 Then

                    If MyTargetType = TargetTypeLoot And MyTargetIndex > 0 Then
                        Call SendCloseLoot
                    End If

                    ' found our target!
                    SendPlayerTarget i, TargetTypePlayer
                    Call OpenTargetWindow

                    Exit Sub
                End If
            End If
        End If

    Next

    ' check npcs
    For i = 1 To Npc_HighIndex

        If MapNpc(i).Num > 0 Then
            If Not GetNpcDead(i) Then
                X = (MapNpc(i).X * 32) + MapNpc(i).xOffset + 32
                Y = (MapNpc(i).Y * 32) + MapNpc(i).yOffset + 32

                If X >= GlobalX_Map And X <= GlobalX_Map + 32 Then
                    If Y >= GlobalY_Map And Y <= GlobalY_Map + 32 Then

                        If MyTargetType = TargetTypeLoot And MyTargetIndex > 0 Then
                            Call SendCloseLoot
                        End If

                        ' found our target!
                        SendPlayerTarget i, TargetTypeNpc
                        Call OpenTargetWindow
                        Exit Sub
                    End If
                End If
            End If
        End If
    Next

    For i = 1 To Corpse_HighIndex
        If Corpse(i).LootId > 0 Then
            X = Corpse(i).X + 32
            Y = Corpse(i).Y + 32

            If X >= GlobalX_Map And X <= GlobalX_Map + 32 Then
                If Y >= GlobalY_Map And Y <= GlobalY_Map + 32 Then

                    If MyTargetType = TargetTypeLoot And MyTargetIndex = Corpse(i).LootId Then
                        If Windows(GetWindowIndex("winLoot")).Window.Visible Then
                            Exit Sub
                        End If
                    ElseIf MyTargetType = TargetTypeLoot And MyTargetIndex <> Corpse(i).LootId Then
                        Call SendCloseLoot
                    End If

                    ' found our target!
                    SendPlayerTarget Corpse(i).LootId, TargetTypeLoot
                    Exit Sub
                End If
            End If

        End If
    Next

    ' case else, close if its open
    Call CloseTargetWindow

End Sub

Public Sub SetBarWidth(ByRef MaxWidth As Long, ByRef Width As Long)
    Dim barDifference As Long

    If MaxWidth < Width Then
        ' find out the amount to increase per loop
        barDifference = ((Width - MaxWidth) / 100) * 10

        ' if it's less than 1 then default to 1
        If barDifference < 1 Then barDifference = 1
        ' set the width
        Width = Width - barDifference
    ElseIf MaxWidth > Width Then
        ' find out the amount to increase per loop
        barDifference = ((MaxWidth - Width) / 100) * 10

        ' if it's less than 1 then default to 1
        If barDifference < 1 Then barDifference = 1
        ' set the width
        Width = Width + barDifference
    End If

End Sub

Public Sub AttemptLogin()
    TcpInit GameServerIp, GameServerPort

    ' send login packet
    If ConnectToServer Then
        SendLogin Windows(GetWindowIndex("winLogin")).Controls(GetControlIndex("winLogin", "txtUser")).Text
        Exit Sub
    End If

    If Not IsConnected Then
        ShowWindow GetWindowIndex("winLogin")
        ShowWindow GetWindowIndex("winLoginFooter")
        ShowDialogue "Problema de Conexao", "Não pode conectar-se ao game server.", "Tente novamente depois.", DialogueTypeAlert
    End If
End Sub

Public Function Clamp(ByVal Value As Long, ByVal Min As Long, ByVal Max As Long) As Long
    Clamp = Value

    If Value < Min Then Clamp = Min
    If Value > Max Then Clamp = Max
End Function


Public Sub ShowInvDesc(X As Long, Y As Long, InvNum As Long)

    If InvNum <= 0 Or InvNum > MaxInventory Then Exit Sub
    Dim ItemNum As Long

    ItemNum = GetInventoryItemNum(InvNum)

    If ItemNum > 0 Then
         Dim Inventory As InventoryRec
    
         Inventory.Num = GetInventoryItemNum(InvNum)
         Inventory.Value = GetInventoryItemValue(InvNum)
         Inventory.Level = GetInventoryItemLevel(InvNum)
         Inventory.Bound = GetInventoryItemBound(InvNum)
         Inventory.AttributeId = GetInventoryItemAttributeId(InvNum)
         Inventory.UpgradeId = GetInventoryItemUpgradeId(InvNum)
    
        If Item(ItemNum).Type = ItemType.ItemType_Heraldry Then
            Call ShowHeraldryDescription(X, Y, Inventory, Item(ItemNum).Price)
        Else
            ShowItemDesc X, Y, Inventory
        End If
    End If
End Sub

Public Sub ShowEqDesc(X As Long, Y As Long, eqNum As Long)
    If eqNum <= 0 Or eqNum > PlayerEquipments.PlayerEquipment_Count - 1 Then
        Exit Sub
    End If

    If GetPlayerEquipmentId(eqNum) Then
        Dim Inventory As InventoryRec
        
        Inventory.Num = GetPlayerEquipmentId(eqNum)
        Inventory.Value = 1
        Inventory.Level = GetPlayerEquipmentLevel(eqNum)
        Inventory.Bound = GetPlayerEquipmentBound(eqNum)
        Inventory.AttributeId = GetPlayerEquipmentAttributeId(eqNum)
        Inventory.UpgradeId = GetPlayerEquipmentUpgradeId(eqNum)

        ShowItemDesc X, Y, Inventory
    End If
End Sub

Public Sub AddDescInfo(Text As String, Optional Colour As Long = White)
    Dim Count As Long
    Count = UBound(DescText)
    ReDim Preserve DescText(1 To Count + 1) As TextColourRec
    DescText(Count + 1).Text = Text
    DescText(Count + 1).Colour = Colour
End Sub

Public Sub SwitchHotbar(OldSlot As Long, NewSlot As Long)
    If OldSlot < 1 Or OldSlot > MaximumQuickSlot Then
        Exit Sub
    End If

    If NewSlot < 1 Or NewSlot > MaximumQuickSlot Then
        Exit Sub
    End If

    Call SendSwapQuickSlot(OldSlot, NewSlot)
End Sub

Sub ShowPlayerMenu(Index As Long, X As Long, Y As Long)
    PlayerMenuIndex = Index
    If PlayerMenuIndex = 0 Then Exit Sub
    Windows(GetWindowIndex("winPlayerMenu")).Window.Left = X - 5
    Windows(GetWindowIndex("winPlayerMenu")).Window.Top = Y - 5
    Windows(GetWindowIndex("winPlayerMenu")).Controls(GetControlIndex("winPlayerMenu", "btnName")).Text = Trim$(GetPlayerName(PlayerMenuIndex))
    ShowWindow GetWindowIndex("winRightClickBG")
    ShowWindow GetWindowIndex("winPlayerMenu"), , False
End Sub

Public Function AryCount(ByRef Ary() As Byte) As Long
    On Error Resume Next

    AryCount = UBound(Ary) + 1
End Function

Public Function ByteToInt(ByVal B1 As Long, ByVal B2 As Long) As Long
    ByteToInt = B1 * 256 + B2
End Function

Sub UpdateStats_UI()
' set the bar labels
    With Windows(GetWindowIndex("winBars"))
        .Controls(GetControlIndex("winBars", "lblHP")).Text = GetPlayerVital(MyIndex, HP) & "/" & GetPlayerMaxVital(MyIndex, HP)
        .Controls(GetControlIndex("winBars", "lblMP")).Text = GetPlayerVital(MyIndex, MP) & "/" & GetPlayerMaxVital(MyIndex, MP)
        .Controls(GetControlIndex("winBars", "lblEXP")).Text = GetPlayerExp() & "/" & TNL
    End With

    ' update character screen
End Sub




