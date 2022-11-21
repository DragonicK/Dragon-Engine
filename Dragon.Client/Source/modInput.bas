Attribute VB_Name = "Input_Implementation"
Option Explicit

Public Enum MouseButtons
    MouseButtons_Left
    MouseButtons_Right
End Enum

' keyboard input
Public Declare Function GetAsyncKeyState Lib "user32" (ByVal VKey As Long) As Integer
Public Declare Function GetKeyState Lib "user32" (ByVal nVirtKey As Long) As Integer

' Actual input
Public Sub CheckKeys()
    ' exit out if dialogue
    If Dialogue.Index > 0 Then Exit Sub
    
    If GetAsyncKeyState(VK_W) >= 0 Then WDown = False
    If GetAsyncKeyState(VK_S) >= 0 Then SDown = False
    If GetAsyncKeyState(VK_A) >= 0 Then ADown = False
    If GetAsyncKeyState(VK_D) >= 0 Then DDown = False
    If GetAsyncKeyState(VK_CONTROL) >= 0 Then ControlDown = False
    If GetAsyncKeyState(VK_SHIFT) >= 0 Then ShiftDown = False
    If GetAsyncKeyState(VK_TAB) >= 0 Then TabDown = False

End Sub

Public Sub CheckInputKeys()

' exit out if dialogue
    If Dialogue.Index > 0 Then Exit Sub

    ' exit out if talking
    If Windows(GetWindowIndex("winChat")).Window.Visible Then Exit Sub

    ' continue
    If GetKeyState(vbKeyShift) < 0 Then
        ShiftDown = True
    Else
        ShiftDown = False
    End If

    If GetKeyState(vbKeySpace) < 0 Then
        ControlDown = True
    Else
        ControlDown = False
    End If

    If GetKeyState(vbKeyTab) < 0 Then
        TabDown = True
    Else
        TabDown = False
    End If

    'Move Up
    If Not chatOn Then
        If GetKeyState(vbKeySpace) < 0 Then

        End If

        ' move up
        If GetKeyState(vbKeyW) < 0 Then
            WDown = True
            SDown = False
            ADown = False
            DDown = False

            Exit Sub
        Else
            WDown = False
        End If

        'Move Right
        If GetKeyState(vbKeyD) < 0 Then
            WDown = False
            SDown = False
            ADown = False
            DDown = True

            Exit Sub
        Else
            DDown = False
        End If

        'Move down
        If GetKeyState(vbKeyS) < 0 Then
            WDown = False
            SDown = True
            ADown = False
            DDown = False
            
            Exit Sub
        Else
            SDown = False
        End If

        'Move left
        If GetKeyState(vbKeyA) < 0 Then
            WDown = False
            SDown = False
            ADown = True
            DDown = False
            
            Exit Sub
        Else
            ADown = False
        End If
    Else
        WDown = False
        SDown = False
        ADown = False
        DDown = False
    End If

End Sub

Public Sub HandleKeyPresses(ByVal KeyAscii As Integer)
    Dim chatText As String, Name As String, i As Long, n As Long, Command() As String, Buffer As clsBuffer, tmpNum As Long

    ' check if we're skipping video
    If KeyAscii = vbKeyEscape Then
        ' hide options screen
        HideWindow GetWindowIndex("winOptions")
        CloseComboMenu

        If Windows(GetWindowIndex("winEscMenu")).Window.Visible Then
            ' hide it
            HideWindow GetWindowIndex("winBlank")
            HideWindow GetWindowIndex("winEscMenu")
            Exit Sub
        Else
            ' show them
            ShowWindow GetWindowIndex("winBlank"), True
            ShowWindow GetWindowIndex("winEscMenu"), True
            Exit Sub
        End If
    End If

    If InGame Then
        chatText = Windows(GetWindowIndex("winChat")).Controls(GetControlIndex("winChat", "txtChat")).Text
    End If

    ' Do we have an active window
    If activeWindow > 0 Then
        ' make sure it's visible
        If Windows(activeWindow).Window.Visible Then
            ' Do we have an active control
            If Windows(activeWindow).activeControl > 0 Then
                ' Do our thing
                With Windows(activeWindow).Controls(Windows(activeWindow).activeControl)

                    If .Enabled Then

                        ' Handle input
                        Select Case KeyAscii
                        Case vbKeyBack
                            If LenB(.Text) > 0 Then
                                .Text = Left$(.Text, Len(.Text) - 1)
                            End If

                        Case vbKeyReturn
                            ' override for function callbacks
                            If .entCallBack(entStates.Enter) > 0 Then
                                entCallBack .entCallBack(entStates.Enter), activeWindow, Windows(activeWindow).activeControl, 0, 0
                                Exit Sub
                            Else
                                n = 0
                                For i = Windows(activeWindow).ControlCount To 1 Step -1
                                    If i > Windows(activeWindow).activeControl Then
                                        If SetActiveControl(activeWindow, i) Then n = i
                                    End If
                                Next
                                If n = 0 Then
                                    For i = Windows(activeWindow).ControlCount To 1 Step -1
                                        SetActiveControl activeWindow, i
                                    Next
                                End If
                            End If

                        Case vbKeyTab
                            n = 0
                            For i = Windows(activeWindow).ControlCount To 1 Step -1
                                If i > Windows(activeWindow).activeControl Then
                                    If SetActiveControl(activeWindow, i) Then n = i
                                End If
                            Next
                            If n = 0 Then
                                For i = Windows(activeWindow).ControlCount To 1 Step -1
                                    SetActiveControl activeWindow, i
                                Next
                            End If
                        Case Else
                            ' Limitar o texto do chat
                            If Len(.Text) >= .TextLimit Then Exit Sub

                            If .AcceptOnlyNumbers Then
                                If KeyAscii >= 48 And KeyAscii <= 57 Then
                                    .Text = .Text & ChrW$(KeyAscii)
                                End If
                            Else
                                .Text = .Text & ChrW$(KeyAscii)
                            End If

                        End Select

                        ' callback keyPress
                        Dim callBack As Long

                        callBack = .entCallBack(entStates.KeyPress)

                        If callBack <> 0 Then entCallBack callBack, activeWindow, Windows(activeWindow).activeControl, 0, 0

                        ' exit out early - if not chatting
                        If Windows(activeWindow).Window.Name <> "winChat" Then Exit Sub
                    End If
                End With
            End If
        End If
    End If

    ' exit out early if we're not ingame
    If Not InGame Then Exit Sub


    Select Case KeyAscii
    Case vbKeyEscape
        ' hide options screen
        HideWindow GetWindowIndex("winOptions")
        CloseComboMenu
        ' hide/show chat window
        If Windows(GetWindowIndex("winChat")).Window.Visible Then
            Windows(GetWindowIndex("winChat")).Controls(GetControlIndex("winChat", "txtChat")).Text = vbNullString
            HideChat
            inSmallChat = True
            Exit Sub
        End If

        If Windows(GetWindowIndex("winEscMenu")).Window.Visible Then

            ' hide when player is alive
            If GetPlayerDead(MyIndex) = False Then
                HideWindow GetWindowIndex("winBlank")
            End If

            HideWindow GetWindowIndex("winEscMenu")
        Else
            ' show them
            ShowWindow GetWindowIndex("winBlank"), True
            ShowWindow GetWindowIndex("winEscMenu"), True
        End If
        ' exit out early
        Exit Sub

    Case 67, 99
        ' hide/show character
        If Not Windows(GetWindowIndex("winChat")).Window.Visible Then ButtonMenu_Char
        Exit Sub

    Case 71, 103
        ' hide/show guild
        '   If Not Windows(GetWindowIndex("winChat")).Window.Visible Then btnMenu_Guild
        Exit Sub

    Case 73, 105
        ' hide/show inventory
        If Not Windows(GetWindowIndex("winChat")).Window.Visible Then btnMenu_Inv
        Exit Sub

    Case 74, 106    'J
        ' hide/show quest
        If Not Windows(GetWindowIndex("winChat")).Window.Visible Then btnMenu_Quest
        Exit Sub

    Case 75, 107    ' K
        ' hide/show craft list
        If Not Windows(GetWindowIndex("winChat")).Window.Visible Then ButtonMenu_Craft
        Exit Sub

    Case 76, 108
        ' hide/show enchant
        If Not Windows(GetWindowIndex("winChat")).Window.Visible Then btnMenu_ItemUpgrade
        Exit Sub

    Case 77, 109
        ' hide/show skills
        If Not Windows(GetWindowIndex("winChat")).Window.Visible Then btnMenu_Skills
        Exit Sub

        ' hide/show skill tree
    Case 78, 110
        'If Not Windows(GetWindowIndex("winChat")).Window.Visible Then btnMenu_SkillTree
        Exit Sub


    Case 79, 111    ' o
        If Not Windows(GetWindowIndex("winChat")).Window.Visible Then ButtonMenu_Mail
        Exit Sub

    Case 80, 112    ' p
        If Not Windows(GetWindowIndex("winChat")).Window.Visible Then ButtonMenu_Services
        Exit Sub

    Case 84, 116
        ' hide/show title
        If Not Windows(GetWindowIndex("winChat")).Window.Visible Then ButtonMenu_Title
        Exit Sub

    Case 85, 117    'u
        If Not Windows(GetWindowIndex("winChat")).Window.Visible Then btnMenu_Heraldry
        Exit Sub

    Case 86, 118    'v
        If Not Windows(GetWindowIndex("winChat")).Window.Visible Then ButtonMenu_CashShop
        Exit Sub

    Case 88, 120    'x
        ' If Not Windows(GetWindowIndex("winChat")).Window.Visible Then ButtonMenu_Mail
        Exit Sub

    Case 89, 121    'Y
        ' hide/show achievement
        If Not Windows(GetWindowIndex("winChat")).Window.Visible Then btnMenu_Achievement

        Exit Sub

    End Select

    ' handles hotbar
    If inSmallChat Then
        For i = 1 To 9
            If KeyAscii = 48 + i Then
                SendQuickSlotUse i
                Exit For
            End If
        Next

        If KeyAscii = 48 Then SendQuickSlotUse 10
    End If

    ' Handle when the player presses the return key
    If KeyAscii = vbKeyReturn Then
        If Windows(GetWindowIndex("winChatSmall")).Window.Visible Then
            ShowChat
            inSmallChat = False
            Exit Sub
        End If

        ' Broadcast message
        If Left$(chatText, 1) = "'" Then
            chatText = Mid$(chatText, 2, Len(chatText) - 1)

            If Len(chatText) > 0 Then
                Call SendBroadcastMessage(chatText)
            End If

            Windows(GetWindowIndex("winChat")).Controls(GetControlIndex("winChat", "txtChat")).Text = vbNullString
            HideChat
            Exit Sub
        End If

        ' Emote message
        If Left$(chatText, 1) = "-" Then
            chatText = Mid$(chatText, 2, Len(chatText) - 1)

            If Len(chatText) > 0 Then
                ' Call EmoteMsg(chatText)
            End If

            Windows(GetWindowIndex("winChat")).Controls(GetControlIndex("winChat", "txtChat")).Text = vbNullString
            HideChat
            Exit Sub
        End If

        ' Player message
        If Left$(chatText, 1) = "!" Then
            chatText = Mid$(chatText, 2, Len(chatText) - 1)
            Name = vbNullString
            ' Get the desired player from the user text
            tmpNum = Len(chatText)

            For i = 1 To tmpNum

                If Mid$(chatText, i, 1) <> Space$(1) Then
                    Name = Name & Mid$(chatText, i, 1)
                Else
                    Exit For
                End If

            Next

            chatText = Mid$(chatText, i, Len(chatText) - 1)

            ' Make sure they are actually sending something
            If Len(chatText) > 0 Then
                ' Send the message to the player
                Call AddPlayerMessage(Trim$(chatText), Name)
                Call SendPlayerMessage(Trim$(chatText), Name)
            Else
                Call AddText("Usage: !playername (message)", AlertColor)
            End If

            Windows(GetWindowIndex("winChat")).Controls(GetControlIndex("winChat", "txtChat")).Text = vbNullString
            HideChat
            Exit Sub
        End If

        If GetPlayerAccess(MyIndex) >= ACCESS_SUPERIOR Then
            If Left$(chatText, 1) = "." Then
                Call ParseAdministratorCommands(chatText)
            End If
        End If

        If Left$(chatText, 1) = "/" Then
            Command = Split(chatText, Space$(1))

            Select Case Command(0)

            Case "/help"
                Call AddText("Social Commands:", HelpColor)
                Call AddText("'msghere = Global Message", HelpColor)
                Call AddText("-msghere = Emote Message", HelpColor)
                Call AddText("!namehere msghere = Player Message", HelpColor)
                Call AddText("Available Commands: /who, /fps, /fpslock, /gui, /maps", HelpColor)

            Case "/gui"
                hideGUI = Not hideGUI

                ' Checking fps
            Case "/fps"
                BFPS = Not BFPS

                ' toggle fps lock
            Case "/fpslock"
                FPS_Lock = Not FPS_Lock

            Case "/loc"

                If GetPlayerAccess(MyIndex) < ACCESS_GAMEMASTER Then GoTo continue
                BLoc = Not BLoc

                ' Warping to a player
            Case "/warpmeto"

                If GetPlayerAccess(MyIndex) < ACCESS_GAMEMASTER Then GoTo continue
                If UBound(Command) < 1 Then
                    AddText "Usage: /warpmeto (name)", AlertColor
                    GoTo continue
                End If

                If IsNumeric(Command(1)) Then
                    AddText "Usage: /warpmeto (name)", AlertColor
                    GoTo continue
                End If

                Call SendWarpMeTo(Command(1))

                ' Warping a player to you
            Case "/warptome"

                If GetPlayerAccess(MyIndex) < ACCESS_GAMEMASTER Then GoTo continue
                If UBound(Command) < 1 Then
                    AddText "Usage: /warptome (name)", AlertColor
                    GoTo continue
                End If

                If IsNumeric(Command(1)) Then
                    AddText "Usage: /warptome (name)", AlertColor
                    GoTo continue
                End If

                Call SendWarpToMe(Command(1))

                ' Warping to a map
            Case "/warpto"

                If GetPlayerAccess(MyIndex) < ACCESS_GAMEMASTER Then GoTo continue
                If UBound(Command) < 1 Then
                    AddText "Usage: /warpto (map #)", AlertColor
                    GoTo continue
                End If

                If Not IsNumeric(Command(1)) Then
                    AddText "Usage: /warpto (map #)", AlertColor
                    GoTo continue
                End If

                n = CLng(Command(1))

                ' Check to make sure its a valid map #
                If n > 0 And n <= MaxMaps Then
                    Call SendWarpTo(n)
                Else
                    Call AddText("Invalid map number.", Red)
                End If

            Case "/kickplayer"
                If GetPlayerAccess(MyIndex) < ACCESS_ADMINISTRATOR Then GoTo continue
                Call SendKick(Command(1))

                ' Respawn request
                ' Case "/respawn"

                '  If GetPlayerAccess(MyIndex) < ACCESS_ADMINISTRATOR Then GoTo continue
                '  SendMapRespawn

                ' Packet debug mode
            Case "/debug"

                If GetPlayerAccess(MyIndex) < ACCESS_ADMINISTRATOR Then GoTo continue
                DEBUG_MODE = (Not DEBUG_MODE)

            Case "/invite"
                Call SendPartyRequest(Command(1))

            Case "/kick"
                If Party.Leader > 0 Then
                    Call SendPartyKick(FindPartyCharacterIndex(Command(1)))
                End If

            Case "/view"
                SendRequestViewEquipment Command(1)

            Case Else
                AddText "Não é um comando válido!", HelpColor
            End Select

            'continue label where we go instead of exiting the sub
continue:
            Windows(GetWindowIndex("winChat")).Controls(GetControlIndex("winChat", "txtChat")).Text = vbNullString
            HideChat
            Exit Sub
        End If

        ' Say message
        If Len(chatText) > 0 Then
            Call SendMapMessage(chatText)
        End If

        Windows(GetWindowIndex("winChat")).Controls(GetControlIndex("winChat", "txtChat")).Text = vbNullString

        ' hide/show chat window
        If Windows(GetWindowIndex("winChat")).Window.Visible Then HideChat
        Exit Sub
    End If

    ' hide/show chat window
    If Windows(GetWindowIndex("winChatSmall")).Window.Visible Then
        Exit Sub
    End If
End Sub

Public Function MouseButton_Clicked(ByVal Button As MouseButtons) As Boolean
    If Button = MouseButtons_Left Then
        If (mouseClick(VK_LBUTTON) And lastMouseClick(VK_LBUTTON) = 0) Then
            MouseButton_Clicked = True
        End If
    ElseIf Button = MouseButtons_Right Then
        If (mouseClick(VK_RBUTTON) And lastMouseClick(VK_RBUTTON) = 0) Then
            MouseButton_Clicked = True
        End If
    End If

End Function

