Attribute VB_Name = "InterfaceEvents_Implementation"
Option Explicit
Public Declare Sub GetCursorPos Lib "user32" (lpPoint As POINTAPI)
Public Declare Function ScreenToClient Lib "user32" (ByVal hWnd As Long, lpPoint As POINTAPI) As Long
Public Declare Function entCallBack Lib "user32.dll" Alias "CallWindowProcA" (ByVal lpPrevWndFunc As Long, ByVal Window As Long, ByRef Control As Long, ByVal Forced As Long, ByVal lParam As Long) As Long
Public Const VK_LBUTTON = &H1
Public Const VK_RBUTTON = &H2
Public lastMouseX As Long, lastMouseY As Long
Attribute lastMouseY.VB_VarUserMemId = 1073741824
Public currMouseX As Long, currMouseY As Long
Attribute currMouseX.VB_VarUserMemId = 1073741826
Attribute currMouseY.VB_VarUserMemId = 1073741826
Public clickedX As Long, clickedY As Long
Attribute clickedX.VB_VarUserMemId = 1073741828
Attribute clickedY.VB_VarUserMemId = 1073741828
Public mouseClick(1 To 2) As Long
Attribute mouseClick.VB_VarUserMemId = 1073741830
Public lastMouseClick(1 To 2) As Long
Attribute lastMouseClick.VB_VarUserMemId = 1073741831

Public Function MouseX(Optional ByVal hWnd As Long) As Long
    Dim lpPoint As POINTAPI
    GetCursorPos lpPoint

    If hWnd Then ScreenToClient hWnd, lpPoint
    MouseX = lpPoint.X
End Function

Public Function MouseY(Optional ByVal hWnd As Long) As Long
    Dim lpPoint As POINTAPI
    GetCursorPos lpPoint

    If hWnd Then ScreenToClient hWnd, lpPoint
    MouseY = lpPoint.Y
End Function

Public Sub HandleMouseInput()
    Dim entState As entStates, i As Long, X As Long
    
    ' set values
    lastMouseX = currMouseX
    lastMouseY = currMouseY
    currMouseX = MouseX(frmMain.hWnd)
    currMouseY = MouseY(frmMain.hWnd)
    GlobalX = currMouseX
    GlobalY = currMouseY
    lastMouseClick(VK_LBUTTON) = mouseClick(VK_LBUTTON)
    lastMouseClick(VK_RBUTTON) = mouseClick(VK_RBUTTON)
    mouseClick(VK_LBUTTON) = GetAsyncKeyState(VK_LBUTTON)
    mouseClick(VK_RBUTTON) = GetAsyncKeyState(VK_RBUTTON)

    ' Hover
    entState = entStates.Hover

    ' MouseDown
    If (mouseClick(VK_LBUTTON) And lastMouseClick(VK_LBUTTON) = 0) Or (mouseClick(VK_RBUTTON) And lastMouseClick(VK_RBUTTON) = 0) Then
        clickedX = currMouseX
        clickedY = currMouseY
        entState = entStates.MouseDown
        ' MouseUp
    ElseIf (mouseClick(VK_LBUTTON) = 0 And lastMouseClick(VK_LBUTTON)) Or (mouseClick(VK_RBUTTON) = 0 And lastMouseClick(VK_RBUTTON)) Then
        entState = entStates.MouseUp
        ' MouseMove
    ElseIf (currMouseX <> lastMouseX) Or (currMouseY <> lastMouseY) Then
        entState = entStates.MouseMove
    End If

    ' Handle everything else
    If Not HandleGuiMouse(entState) Then
        ' reset /all/ control mouse events
        For i = 1 To WindowCount
            For X = 1 To Windows(i).ControlCount
                Windows(i).Controls(X).State = Normal
            Next
        Next
        
        If InGame Then
            If entState = entStates.MouseDown Then
                ' Handle events
                If currMouseX >= 0 And currMouseX <= frmMain.ScaleWidth Then
                    If currMouseY >= 0 And currMouseY <= frmMain.ScaleHeight Then
                        ' left click
                        If (mouseClick(VK_LBUTTON) And lastMouseClick(VK_LBUTTON) = 0) Then
                            ' targetting
                            FindTarget
                            ' right click
                        ElseIf (mouseClick(VK_RBUTTON) And lastMouseClick(VK_RBUTTON) = 0) Then
                            If ShiftDown Then
                                ' admin warp if we're pressing shift and right clicking
                                If GetPlayerAccess(MyIndex) >= 2 Then SendAdminWarp CurX, CurY
                                Exit Sub
                            End If

                            ' right-click menu
                            For i = 1 To Player_HighIndex
                                If IsPlaying(i) Then
                                    If GetPlayerMap(i) = GetPlayerMap(MyIndex) Then
                                        If GetPlayerX(i) = CurX And GetPlayerY(i) = CurY Then
                                            ShowPlayerMenu i, currMouseX, currMouseY
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If
            ElseIf entState = entStates.MouseMove Then
                GlobalX_Map = GlobalX + (TileView.Left * PIC_X) + Camera.Left
                GlobalY_Map = GlobalY + (TileView.Top * PIC_Y) + Camera.Top
                ' Handle the events
                CurX = TileView.Left + ((currMouseX + Camera.Left) \ PIC_X)
                CurY = TileView.Top + ((currMouseY + Camera.Top) \ PIC_Y)
            End If
        End If
    End If
End Sub

Public Function HandleGuiMouse(entState As entStates) As Boolean
    Dim i As Long, curWindow As Long, curControl As Long, callBack As Long, X As Long

    ' if hiding gui
    If hideGUI = True Then Exit Function

    ' Find the container
    For i = 1 To WindowCount
        With Windows(i).Window
            If .Enabled And .Visible Then
                If .State <> entStates.MouseDown Then .State = entStates.Normal
                If currMouseX >= .Left And currMouseX <= .Width + .Left Then
                    If currMouseY >= .Top And currMouseY <= .Height + .Top Then
                        ' set the combomenu
                        If .design(0) = DesignTypes.desComboMenuNorm Then
                            ' set the hover menu
                            If entState = MouseMove Or entState = Hover Then
                                ComboMenu_MouseMove i
                            ElseIf entState = MouseDown Then
                                ComboMenu_MouseDown i
                            End If
                        End If
                        ' everything else
                        If curWindow = 0 Then curWindow = i
                        If .zOrder > Windows(curWindow).Window.zOrder Then curWindow = i
                    End If
                End If
                If entState = entStates.MouseMove Then
                    If .canDrag Then
                        If .State = entStates.MouseDown Then
                            .Left = Clamp(.Left + ((currMouseX - .Left) - .movedX), 0, ScreenWidth - .Width)
                            .Top = Clamp(.Top + ((currMouseY - .Top) - .movedY), 0, ScreenHeight - .Height)
                        End If
                    End If
                End If
            End If
        End With
    Next

    ' Handle any controls first
    If curWindow Then
        ' reset /all other/ control mouse events
        For i = 1 To WindowCount
            If i <> curWindow Then
                For X = 1 To Windows(i).ControlCount
                    Windows(i).Controls(X).State = Normal
                Next
            End If
        Next
        For i = 1 To Windows(curWindow).ControlCount
            With Windows(curWindow).Controls(i)
                If .Enabled And .Visible Then
                    If .State <> entStates.MouseDown Then .State = entStates.Normal
                    If currMouseX >= .Left + Windows(curWindow).Window.Left And currMouseX <= .Left + .Width + Windows(curWindow).Window.Left Then
                        If currMouseY >= .Top + Windows(curWindow).Window.Top And currMouseY <= .Top + .Height + Windows(curWindow).Window.Top Then
                            If curControl = 0 Then curControl = i
                            If .zOrder > Windows(curWindow).Controls(curControl).zOrder Then curControl = i
                        End If
                    End If
                    If entState = entStates.MouseMove Then
                        If .canDrag Then
                            If .State = entStates.MouseDown Then
                                .Left = Clamp(.Left + ((currMouseX - .Left) - .movedX), 0, Windows(curWindow).Window.Width - .Width)
                                .Top = Clamp(.Top + ((currMouseY - .Top) - .movedY), 0, Windows(curWindow).Window.Height - .Height)
                            End If
                        End If
                    End If
                End If
            End With
        Next
        ' Handle control
        If curControl Then
            HandleGuiMouse = True
            With Windows(curWindow).Controls(curControl)
                If .State <> entStates.MouseDown Then
                    If entState <> entStates.MouseMove Then
                        .State = entState
                    Else
                        .State = entStates.Hover
                    End If
                End If
                If entState = entStates.MouseDown Then
                    If .canDrag Then
                        .movedX = clickedX - .Left
                        .movedY = clickedY - .Top
                    End If
                    ' toggle boxes
                    Select Case .Type
                    Case EntityTypes.entCheckbox
                        ' grouped boxes
                        If .group > 0 Then
                            If .Value = 0 Then
                                For i = 1 To Windows(curWindow).ControlCount
                                    If Windows(curWindow).Controls(i).Type = EntityTypes.entCheckbox Then
                                        If Windows(curWindow).Controls(i).group = .group Then
                                            Windows(curWindow).Controls(i).Value = 0
                                        End If
                                    End If
                                Next
                                .Value = 1
                            End If
                        Else
                            If .Value = 0 Then
                                .Value = 1
                            Else
                                .Value = 0
                            End If
                        End If
                    Case EntityTypes.entCombobox
                        ShowComboMenu curWindow, curControl
                    End Select
                    ' set active input
                    SetActiveControl curWindow, curControl
                End If
                callBack = .entCallBack(entState)
            End With
        Else
            ' Handle container
            With Windows(curWindow).Window
                HandleGuiMouse = True
                If .State <> entStates.MouseDown Then
                    If entState <> entStates.MouseMove Then
                        .State = entState
                    Else
                        .State = entStates.Hover
                    End If
                End If
                If entState = entStates.MouseDown Then
                    If .canDrag Then
                        .movedX = clickedX - .Left
                        .movedY = clickedY - .Top
                    End If
                End If
                callBack = .entCallBack(entState)
            End With
        End If
        ' bring to front
        If entState = entStates.MouseDown Then
            UpdateZOrder curWindow
            activeWindow = curWindow
        End If
        ' call back
        If callBack <> 0 Then entCallBack callBack, curWindow, curControl, 0, 0
    End If

    ' Reset
    If entState = entStates.MouseUp Then ResetMouseDown
End Function

Public Sub ResetGUI()
    Dim i As Long, X As Long

    For i = 1 To WindowCount

        If Windows(i).Window.State <> MouseDown Then Windows(i).Window.State = Normal

        For X = 1 To Windows(i).ControlCount

            If Windows(i).Controls(X).State <> MouseDown Then Windows(i).Controls(X).State = Normal
        Next
    Next

End Sub

Public Sub ResetMouseDown()
    Dim callBack As Long
    Dim i As Long, X As Long

    For i = 1 To WindowCount

        With Windows(i)
            .Window.State = entStates.Normal
            callBack = .Window.entCallBack(entStates.Normal)

            If callBack <> 0 Then entCallBack callBack, i, 0, 0, 0

            For X = 1 To .ControlCount
                .Controls(X).State = entStates.Normal
                callBack = .Controls(X).entCallBack(entStates.Normal)

                If callBack <> 0 Then entCallBack callBack, i, X, 0, 0
            Next

        End With

    Next

End Sub


' ##############
' ## Esc Menu ##
' ##############

Public Sub btnEscMenu_Return()
    HideWindow GetWindowIndex("winBlank")
    HideWindow GetWindowIndex("winEscMenu")
End Sub

Public Sub btnEscMenu_Options()
    HideWindow GetWindowIndex("winEscMenu")
    ShowWindow GetWindowIndex("winOptions"), True, True
End Sub

Public Sub btnEscMenu_MainMenu()
    HideWindows
    ShowWindow GetWindowIndex("winLogin")
    Stop_Music
    ' play the menu music
    If Len(Trim$(MenuMusic)) > 0 Then Play_Music Trim$(MenuMusic)
    LogoutGame
End Sub

Public Sub btnEscMenu_Exit()
    HideWindow GetWindowIndex("winBlank")
    HideWindow GetWindowIndex("winEscMenu")
    DestroyGame
End Sub

' ##########
' ## Bars ##
' ##########

Public Sub Bars_OnDraw()
    Dim xO As Long, yO As Long, Width As Long
    Dim OffsetX As Long, Percentage As Long

    xO = Windows(GetWindowIndex("winBars")).Window.Left
    yO = Windows(GetWindowIndex("winBars")).Window.Top

    ' Tamanho Total da Barra
    ' Width = 209

    ' Obtem em forma de porcentagem a barra que esta sendo desenhada.
    '  Percentage = (BarWidth_GuiSP / Width) * 100
    ' Subtrai por 100 para o obter o outro lado que nao esta sendo desenhado.
    ' Percentage = 100 - Percentage

    ' Evita erro por divisao por 0.
    ' If Percentage <> 0 Then
    ' Pega o tamanho que nao esta sendo desenhado.
    '      OffsetX = (Width * Percentage) / 100
    ' End If

    ' Bars
    RenderTexture Tex_GUI(27), xO + 15, yO + 15, 0, 0, BarWidth_GuiHP, 13, BarWidth_GuiHP, 13
    RenderTexture Tex_GUI(28), xO + 15, yO + 32, 0, 0, BarWidth_GuiSP, 13, BarWidth_GuiSP, 13

    ' xO + 15 + OffsetX
    ' Move a barra para a direita de acordo com a quantidade de pixels que foi retirado.

    ' OffSetX
    ' Posicao X para fazer a copia da textura.

    ' Width - OffsetX
    ' Tamanho que sera desenhado na tela.

    ' Width - OffsetX
    ' Comprimento que sera copiado da textura.

    '  RenderTexture Tex_GUI(28), xO + 15 + OffsetX, yO + 32, OffsetX, 0, Width - OffsetX, 13, Width - OffsetX, 13

    RenderTexture Tex_GUI(29), xO + 15, yO + 49, 0, 0, BarWidth_GuiEXP, 13, BarWidth_GuiEXP, 13
End Sub

' ############
' ## Target ##
' ############
Public Sub Target_OnDraw()
    Dim xO As Long, yO As Long, Width As Long, WindowIndex As Long

    WindowIndex = GetWindowIndex("winTarget")

    If Windows(WindowIndex).Window.Visible = False Then Exit Sub

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top

    RenderTexture Tex_GUI(27), xO + 15, yO + 30, 0, 0, BarWidth_TargetHP, 13, BarWidth_TargetHP, 13
    RenderTexture Tex_GUI(28), xO + 15, yO + 47, 0, 0, BarWidth_TargetMP, 13, BarWidth_TargetMP, 13
End Sub

' ##########
' ## Menu ##
' ##########



Public Sub btnMenu_Inv()
    If GetPlayerDead(MyIndex) Then Exit Sub

    Dim curWindow As Long
    Dim curWindow2 As Long

    curWindow = GetWindowIndex("winInventory")
    ' curWindow2 = GetWindowIndex("winCurrency")

    If Windows(curWindow).Window.Visible Then
        HideWindow curWindow
        '  HideWindow curWindow2
    Else
        ShowWindow curWindow, , False
    End If
End Sub

Public Sub btnMenu_Achievement()
    If MyIndex > 0 Then
        If GetPlayerDead(MyIndex) Then Exit Sub
    End If

    Dim curWindow As Long
    curWindow = GetWindowIndex("winAchievement")

    If Windows(curWindow).Window.Visible Then
        HideWindow curWindow
    Else
        Call CheckAchievement
        ShowWindow curWindow, , False
    End If

End Sub

Public Sub btnMenu_Quest()
    If MyIndex > 0 Then
        If GetPlayerDead(MyIndex) Then Exit Sub
    End If

    Dim curWindow As Long
    'curWindow = GetWindowIndex("winQuest")

    ' If Windows(curWindow).Window.Visible Then
    '     HideWindow curWindow
    ' Else
    '     ShowWindow curWindow, , False
    ' End If

End Sub

Public Sub btnMenu_Skills()
    If MyIndex > 0 Then
        If GetPlayerDead(MyIndex) Then Exit Sub
    End If

    Dim curWindow As Long
    curWindow = GetWindowIndex("winSkills")

    If Windows(curWindow).Window.Visible Then
        HideWindow curWindow
    Else
        ShowWindow curWindow, , False
    End If
End Sub

Public Sub btnMenu_Map()
'Windows(GetWindowIndex("winCharacter")).Window.visible = Not Windows(GetWindowIndex("winCharacter")).Window.visible
End Sub
'
'End Sub


Public Sub btnMenu_Currency()
    If MyIndex > 0 Then
        If GetPlayerDead(MyIndex) Then Exit Sub
    End If

    Dim WindowIndex As Integer

    WindowIndex = GetWindowIndex("winCurrency")

    If Not Windows(WindowIndex).Window.Visible Then
        Dim xO As Long, yO As Long

        WindowIndex = GetWindowIndex("winInventory")

        xO = Windows(WindowIndex).Window.Left + Windows(WindowIndex).Window.Width
        yO = Windows(WindowIndex).Window.Top

        WindowIndex = GetWindowIndex("winCurrency")

        Windows(WindowIndex).Window.Left = xO
        Windows(WindowIndex).Window.Top = yO
        Windows(WindowIndex).Window.Visible = True
    Else
        Windows(WindowIndex).Window.Visible = False
    End If

End Sub

' ###############
' ## Character ##
' ###############


' #################
' ## Description ##
' #################

Public Sub Description_OnDraw()
    Dim xO As Long, yO As Long, Y As Long, i As Long, Count As Long, X As Long
    Dim Width As Long, WindowIndex As Long

    ' exit out if we don't have a num
    If DescItem = 0 Or DescType = 0 Then Exit Sub

    WindowIndex = GetWindowIndex("winDescription")

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top
    Width = Windows(WindowIndex).Window.Width

    ' Render text array
    Y = 26
    Count = UBound(DescText)
    For i = 1 To Count
        X = xO + (Width * 0.5) - (TextWidth(Font(Fonts.OpenSans_Regular), DescText(i).Text) * 0.5)

        RenderText Font(Fonts.OpenSans_Regular), DescText(i).Text, X, yO + Y, DescText(i).Colour
        Y = Y + 12
    Next

    ' close
    HideWindow GetWindowIndex("winDescription")
End Sub

' ##############
' ## Drag Box ##
' ##############

Public Sub DragBox_OnDraw()
    Dim xO As Long, yO As Long, texNum As Long, WinIndex As Long

    WinIndex = GetWindowIndex("winDragBox")
    xO = Windows(WinIndex).Window.Left
    yO = Windows(WinIndex).Window.Top

    ' get texture num
    With DragBox
        Select Case .Type
        Case Part_Item
            If .Value Then
                texNum = Tex_Item(Item(.Value).IconId)
            End If
        Case Part_spell
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
    If DragBox.Type = part_None Then Exit Sub

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
            If DragBox.Origin = origin_Warehouse Then
                Call DragBox_WarehouseToWarehouse
            End If

            If DragBox.Origin = origin_Inventory Then
                If DragBox.Type = Part_Item Then

                    If Item(GetInventoryItemNum(DragBox.Slot)).Stackable = 0 Then
                        SendDepositItem DragBox.Slot, 1
                    Else
                        ShowDialogue "Depositar Item", "Insira a quantidade para deposito", "", DialogueTypeDepositItem, DialogueStyleInput, DragBox.Slot
                    End If

                End If
            End If

        Case "winInventory"
            ' Heraldry to Inventory
            If DragBox.Origin = origin_Heraldry Then
                Call DragBox_CheckHeraldryToInventory
            End If

            If DragBox.Origin = origin_Upgrade Then
                Call DragBox_CheckItemUpgradeToInventory
            End If

            If DragBox.Origin = origin_Inventory Then
                ' it's from the inventory!
                If DragBox.Type = Part_Item Then
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

            If DragBox.Origin = origin_Warehouse Then
                If DragBox.Type = Part_Item Then
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
        Case PartTypeOrigins.origin_Inventory
            ShowDialogue "DESTRUIR ITEM", "Deseja realmente destruir o item?", "", DialogueTypeDestroyItem, DialogueStyleYesNo, DragBox.Slot
            
        Case PartTypeOrigins.origin_Spells
            ' dialogue
            
        Case PartTypeOrigins.origin_QuickSlot
            SendQuickSlotChange 0, 0, DragBox.Slot
            
        End Select
    End If

    ' close window
    HideWindow WinIndex
    
    With DragBox
        .Type = part_None
        .Slot = 0
        .Origin = origin_None
        .Value = 0
    End With
End Sub

Private Sub DragBox_CheckHotBar(ByVal curWindow As Long)
    Dim i As Long
    Dim tmpRec As RECT

    If DragBox.Origin <> origin_None Then
        If DragBox.Type <> part_None Then
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

                        If DragBox.Origin <> origin_QuickSlot Then
                            If DragBox.Type = Part_Item Then
                                SendQuickSlotChange 1, DragBox.Slot, i
                            ElseIf DragBox.Type = Part_spell Then
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

' ############
' ## QuickSlot ##
' ############

Public Sub QuickSlot_MouseDown()
    Dim SlotNum As Long, WinIndex As Long

    ' is there an item?
    SlotNum = IsHotbar(Windows(GetWindowIndex("winHotbar")).Window.Left, Windows(GetWindowIndex("winHotbar")).Window.Top)

    If SlotNum Then
        With DragBox
            If QuickSlot(SlotNum).SType = 1 Then    ' inventory
                .Type = Part_Item
            ElseIf QuickSlot(SlotNum).SType = 2 Then    ' spell
                .Type = Part_spell
            End If
            .Value = QuickSlot(SlotNum).Slot
            .Origin = origin_QuickSlot
            .Slot = SlotNum
        End With

        WinIndex = GetWindowIndex("winDragBox")
        With Windows(WinIndex).Window
            .State = MouseDown
            .Left = lastMouseX - 16
            .Top = lastMouseY - 16
            .movedX = clickedX - .Left
            .movedY = clickedY - .Top
        End With
        ShowWindow WinIndex, , False

        ' stop dragging inventory
        Windows(GetWindowIndex("winHotbar")).Window.State = Normal
    End If

    ' show desc. if needed
    QuickSlot_MouseMove
End Sub

Public Sub QuickSlot_DblClick()
    Dim SlotNum As Long

    SlotNum = IsHotbar(Windows(GetWindowIndex("winHotbar")).Window.Left, Windows(GetWindowIndex("winHotbar")).Window.Top)

    If SlotNum Then
        SendQuickSlotUse SlotNum
    End If

    ' show desc. if needed
    QuickSlot_MouseMove
End Sub

Public Sub QuickSlot_MouseMove()
    Dim SlotNum As Long, X As Long, Y As Long

    ' exit out early if dragging
    If DragBox.Type <> part_None Then Exit Sub

    SlotNum = IsHotbar(Windows(GetWindowIndex("winHotbar")).Window.Left, Windows(GetWindowIndex("winHotbar")).Window.Top)

    If SlotNum Then
        ' make sure we're not dragging the item
        If DragBox.Origin = origin_QuickSlot And DragBox.Slot = SlotNum Then Exit Sub

        Dim WinDescription As Long
        Dim Inventory As InventoryRec

        WinDescription = GetWindowIndex("winDescription")

        ' calc position
        X = Windows(GetWindowIndex("winHotbar")).Window.Left - Windows(WinDescription).Window.Width
        Y = Windows(GetWindowIndex("winHotbar")).Window.Top - 4

        ' offscreen?
        If X < 0 Then
            ' switch to right
            X = Windows(GetWindowIndex("winHotbar")).Window.Left + Windows(GetWindowIndex("winHotbar")).Window.Width
        End If

        If Y + Windows(WinDescription).Window.Height >= ScreenHeight Then
            Y = ScreenHeight - Windows(WinDescription).Window.Height
        End If

        ' go go go
        Select Case QuickSlot(SlotNum).SType
        Case 1    ' inventory
            Inventory.Num = QuickSlot(SlotNum).Slot

            ShowItemDesc X, Y, Inventory
        Case 2    ' spells
            ShowSkillDesc X, Y, FindSkillSlot(QuickSlot(SlotNum).Slot), QuickSlot(SlotNum).Slot, FindSkillLevel(QuickSlot(SlotNum).Slot)
        End Select
    End If
End Sub

Private Function FindSkillSlot(ByVal SkillNum As Long) As Long
    FindSkillSlot = 0
    If SkillNum <= 0 Then Exit Function

    Dim i As Long

    For i = 1 To MaxPlayerSkill
        If PlayerSkill(i).Id = SkillNum Then
            FindSkillSlot = i
            Exit Function
        End If
    Next
End Function

Private Function FindSkillLevel(ByVal SkillNum As Long) As Long
    FindSkillLevel = 0
    If SkillNum <= 0 Then Exit Function

    Dim i As Long

    For i = 1 To MaxPlayerSkill
        If PlayerSkill(i).Id = SkillNum Then
            FindSkillLevel = PlayerSkill(i).Level
            Exit Function
        End If
    Next

End Function

' Right Click Menu
Sub RightClick_Close()
' close all menus
    HideWindow GetWindowIndex("winRightClickBG")
    HideWindow GetWindowIndex("winPlayerMenu")
End Sub

' Player Menu
Sub PlayerMenu_Party()
    RightClick_Close

    If PlayerMenuIndex = 0 Or PlayerMenuIndex = MyIndex Then
        Exit Sub
    End If

    SendPartyRequest GetPlayerName(PlayerMenuIndex)
End Sub

Sub PlayerMenu_LeaveParty()
    RightClick_Close

    If PlayerMenuIndex <> MyIndex Then
        Exit Sub
    End If

    SendPartyLeave
End Sub

Sub PlayerMenu_Trade()
    RightClick_Close
    If PlayerMenuIndex = 0 Or PlayerMenuIndex = MyIndex Then
        Exit Sub
    End If

    SendTradeRequest PlayerMenuIndex
End Sub

Sub PlayerMenu_ViewEquipment()
    RightClick_Close

    If PlayerMenuIndex = 0 Or PlayerMenuIndex = MyIndex Then
        Exit Sub
    End If

    SendRequestViewEquipment GetPlayerName(PlayerMenuIndex)
End Sub

Sub PlayerMenu_Guild()
    RightClick_Close
    If PlayerMenuIndex = 0 Then Exit Sub
    AddText "System not yet in place.", BrightRed
End Sub

Sub PlayerMenu_PM()
    RightClick_Close
    If PlayerMenuIndex = 0 Then Exit Sub
    AddText "System not yet in place.", BrightRed
End Sub

' Invitations
Sub btnInvite_Party()
    HideWindow GetWindowIndex("winInvite_Party")
    Windows(GetWindowIndex("winInvite_Trade")).Window.Top = ScreenHeight - 80
    ShowDialogue "Party Invitation", Dialogue.DataString & " convidou você para um grupo.", "Voce gostaria de aceitar?", DialogueTypeParty, DialogueStyleYesNo
End Sub

' combobox
Sub CloseComboMenu()
    HideWindow GetWindowIndex("winComboMenuBG")
    HideWindow GetWindowIndex("winComboMenu")
End Sub

Public Sub OpenTargetWindow()
    If Player(MyIndex).Dead Then Exit Sub

    Windows(GetWindowIndex("winTarget")).Window.Visible = True
End Sub

Public Sub CloseTargetWindow()
    Windows(GetWindowIndex("winTarget")).Window.Visible = False
End Sub

Public Sub UpdateTargetWindow()
' Se nao ha alvo, fecha a janela e sai do metodo.
    If MyTargetIndex <= 0 Or MyTargetType = TargetTypeLoot Then
        Call CloseTargetWindow
        Exit Sub
    End If

    Dim WindowIndex As Long, ControlHPIndex As Long, ControlSPIndex As Long, ControlNameIndex As Long
    Dim Percentage As Single, Width As Long

    WindowIndex = GetWindowIndex("winTarget")
    ControlHPIndex = GetControlIndex("winTarget", "lblHP")
    ControlSPIndex = GetControlIndex("winTarget", "lblMP")
    ControlNameIndex = GetControlIndex("winTarget", "lblName")

    If MyTargetType = TargetTypeNpc Then
        Dim NpcNum As Long
        NpcNum = MapNpc(MyTargetIndex).Num

        If NpcNum > 0 Then
            Windows(WindowIndex).Controls(ControlNameIndex).Text = "Lv. " & Npc(NpcNum).Level & " " & Trim$(Npc(NpcNum).Name)
            Windows(WindowIndex).Controls(ControlHPIndex).Text = MapNpc(MyTargetIndex).Vital(HP) & "/" & MapNpc(MyTargetIndex).MaxVital(HP)
            Windows(WindowIndex).Controls(ControlSPIndex).Text = MapNpc(MyTargetIndex).Vital(MP) & "/" & MapNpc(MyTargetIndex).MaxVital(MP)

            If MapNpc(MyTargetIndex).Vital(HP) > 0 Then
                Percentage = CSng(MapNpc(MyTargetIndex).Vital(HP) / MapNpc(MyTargetIndex).MaxVital(HP))
                Width = 209 * Percentage

                BarWidth_TargetHP_Max = Width
            Else
                BarWidth_TargetHP_Max = 0
            End If

            If MapNpc(MyTargetIndex).Vital(MP) > 0 Then
                Percentage = CSng(MapNpc(MyTargetIndex).Vital(MP) / MapNpc(MyTargetIndex).MaxVital(MP))
                Width = 209 * Percentage

                BarWidth_TargetMP_Max = Width
            Else
                BarWidth_TargetMP_Max = 0
            End If

        End If

    ElseIf MyTargetType = TargetTypePlayer Then

        Windows(WindowIndex).Controls(ControlNameIndex).Text = "Lv. " & Player(MyTargetIndex).Level & " " & Trim$(Player(MyTargetIndex).Name)
        Windows(WindowIndex).Controls(ControlHPIndex).Text = Player(MyTargetIndex).Vital(HP) & "/" & Player(MyTargetIndex).MaxVital(HP)
        Windows(WindowIndex).Controls(ControlSPIndex).Text = Player(MyTargetIndex).Vital(MP) & "/" & Player(MyTargetIndex).MaxVital(MP)

        If GetPlayerVital(MyTargetIndex, Vitals.HP) > 0 Then
            Percentage = CSng(GetPlayerVital(MyTargetIndex, Vitals.HP) / GetPlayerMaxVital(MyTargetIndex, Vitals.HP))
            Width = 209 * Percentage

            BarWidth_TargetHP_Max = Width
        Else
            BarWidth_TargetHP_Max = 0
        End If

        If GetPlayerVital(MyTargetIndex, Vitals.MP) > 0 Then
            Percentage = CSng(GetPlayerVital(MyTargetIndex, Vitals.MP) / GetPlayerMaxVital(MyTargetIndex, Vitals.MP))
            Width = 209 * Percentage

            BarWidth_TargetMP_Max = Width
        Else
            BarWidth_TargetMP_Max = 0
        End If
    End If

    Call DrawTargetActiveIcons

End Sub
