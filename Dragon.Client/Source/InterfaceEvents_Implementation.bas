Attribute VB_Name = "InterfaceEvents_Implementation"
Option Explicit
Public Declare Sub GetCursorPos Lib "user32" (lpPoint As POINTAPI)
Public Declare Function ScreenToClient Lib "user32" (ByVal hWnd As Long, lpPoint As POINTAPI) As Long
Public Declare Function EntityCallBack Lib "user32.dll" Alias "CallWindowProcA" (ByVal lpPrevWndFunc As Long, ByVal Window As Long, ByRef Control As Long, ByVal Forced As Long, ByVal lParam As Long) As Long

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

Public Function Clamp(ByVal Value As Long, ByVal Min As Long, ByVal Max As Long) As Long
    Clamp = Value

    If Value < Min Then Clamp = Min
    If Value > Max Then Clamp = Max
End Function

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
                            Call FindTarget
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
                        If .Design(0) = DesignTypes.DesignComboMenu Then
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
                    If .CanDrag Then
                        If .State = entStates.MouseDown Then
                            .Left = Clamp(.Left + ((currMouseX - .Left) - .MovedX), 0, ScreenWidth - .Width)
                            .Top = Clamp(.Top + ((currMouseY - .Top) - .MovedY), 0, ScreenHeight - .Height)
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
                        If .CanDrag Then
                            If .State = entStates.MouseDown Then
                                .Left = Clamp(.Left + ((currMouseX - .Left) - .MovedX), 0, Windows(curWindow).Window.Width - .Width)
                                .Top = Clamp(.Top + ((currMouseY - .Top) - .MovedY), 0, Windows(curWindow).Window.Height - .Height)
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
                    If .CanDrag Then
                        .MovedX = clickedX - .Left
                        .MovedY = clickedY - .Top
                    End If
                    ' toggle boxes
                    Select Case .Type
                    Case EntityTypes.EntityCheckBox
                        ' grouped boxes
                        If .Group > 0 Then
                            If .Value = 0 Then
                                For i = 1 To Windows(curWindow).ControlCount
                                    If Windows(curWindow).Controls(i).Type = EntityTypes.EntityCheckBox Then
                                        If Windows(curWindow).Controls(i).Group = .Group Then
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
                    Case EntityTypes.EntityComboBox
                        ShowComboMenu curWindow, curControl
                    End Select
                    ' set active input
                    SetActiveControl curWindow, curControl
                End If
                callBack = .EntityCallBack(entState)
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
                    If .CanDrag Then
                        .MovedX = clickedX - .Left
                        .MovedY = clickedY - .Top
                    End If
                End If
                callBack = .EntityCallBack(entState)
            End With
        End If
        ' bring to front
        If entState = entStates.MouseDown Then
            UpdateZOrder curWindow
            ActiveWindow = curWindow
        End If
        ' call back
        If callBack <> 0 Then EntityCallBack callBack, curWindow, curControl, 0, 0
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
            callBack = .Window.EntityCallBack(entStates.Normal)

            If callBack <> 0 Then EntityCallBack callBack, i, 0, 0, 0

            For X = 1 To .ControlCount
                .Controls(X).State = entStates.Normal
                callBack = .Controls(X).EntityCallBack(entStates.Normal)

                If callBack <> 0 Then EntityCallBack callBack, i, X, 0, 0
            Next

        End With

    Next

End Sub

Public Sub ShowComboMenu(curWindow As Long, curControl As Long)
    Dim Top As Long
    With Windows(curWindow).Controls(curControl)
        ' linked to
        Windows(GetWindowIndex("winComboMenu")).Window.LinkedToWin = curWindow
        Windows(GetWindowIndex("winComboMenu")).Window.LinkedToCon = curControl
        ' set the size
        Windows(GetWindowIndex("winComboMenu")).Window.Height = 2 + (UBound(.List) * 16)
        Windows(GetWindowIndex("winComboMenu")).Window.Left = Windows(curWindow).Window.Left + .Left + 2
        Top = Windows(curWindow).Window.Top + .Top + .Height
        If Top + Windows(GetWindowIndex("winComboMenu")).Window.Height > ScreenHeight Then Top = ScreenHeight - Windows(GetWindowIndex("winComboMenu")).Window.Height
        Windows(GetWindowIndex("winComboMenu")).Window.Top = Top
        Windows(GetWindowIndex("winComboMenu")).Window.Width = .Width - 4
        ' set the values
        Windows(GetWindowIndex("winComboMenu")).Window.List() = .List()
        Windows(GetWindowIndex("winComboMenu")).Window.Value = .Value
        Windows(GetWindowIndex("winComboMenu")).Window.Group = 0
        ' load the menu
        ShowWindow GetWindowIndex("winComboMenuBG"), True, False
        ShowWindow GetWindowIndex("winComboMenu"), True, False
    End With
End Sub

Public Sub ComboMenu_MouseMove(curWindow As Long)
    Dim Y As Long, i As Long
    With Windows(curWindow).Window
        Y = currMouseY - .Top
        ' find the option we're hovering over
        If UBound(.List) > 0 Then
            For i = 1 To UBound(.List)
                If Y >= (16 * (i - 1)) And Y <= (16 * (i)) Then
                    .Group = i
                End If
            Next
        End If
    End With
End Sub

Public Sub ComboMenu_MouseDown(curWindow As Long)
    Dim Y As Long, i As Long
    With Windows(curWindow).Window
        Y = currMouseY - .Top
        ' find the option we're hovering over
        If UBound(.List) > 0 Then
            For i = 1 To UBound(.List)
                If Y >= (16 * (i - 1)) And Y <= (16 * (i)) Then
                    Windows(.LinkedToWin).Controls(.LinkedToCon).Value = i
                    CloseComboMenu
                End If
            Next
        End If
    End With
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
    RenderTexture Tex_GUI(19), xO + 15, yO + 15, 0, 0, BarWidth_GuiHP, 13, BarWidth_GuiHP, 13
    RenderTexture Tex_GUI(21), xO + 15, yO + 32, 0, 0, BarWidth_GuiSP, 13, BarWidth_GuiSP, 13
    RenderTexture Tex_GUI(23), xO + 15, yO + 49, 0, 0, BarWidth_GuiEXP, 13, BarWidth_GuiEXP, 13
    ' xO + 15 + OffsetX
    ' Move a barra para a direita de acordo com a quantidade de pixels que foi retirado.

    ' OffSetX
    ' Posicao X para fazer a copia da textura.

    ' Width - OffsetX
    ' Tamanho que sera desenhado na tela.

    ' Width - OffsetX
    ' Comprimento que sera copiado da textura.

    '  RenderTexture Tex_GUI(28), xO + 15 + OffsetX, yO + 32, OffsetX, 0, Width - OffsetX, 13, Width - OffsetX, 13

    
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
        X = xO + (Width * 0.5) - (TextWidth(Font(Fonts.FontRegular), DescText(i).Text) * 0.5)

        RenderText Font(Fonts.FontRegular), DescText(i).Text, X, yO + Y, DescText(i).Colour
        Y = Y + 12
    Next

    ' close
    HideWindow GetWindowIndex("winDescription")
End Sub

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

