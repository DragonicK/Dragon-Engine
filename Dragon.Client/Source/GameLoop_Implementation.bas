Attribute VB_Name = "GameLoop_Implementation"
Option Explicit

Public Sub GameLoop()
    Dim FrameTime As Long, Tick As Long, TickFPS As Long, FPS As Long, i As Long, WalkTimer As Long, X As Long, Y As Long
    Dim tmr25 As Long, tmr1000, tmr10000 As Long, tmr100 As Long, mapTimer As Long, chatTmr As Long, targetTmr As Long, fogTmr As Long, barTmr As Long
    Dim barDifference As Long, fadeTmr As Long

    ' *** Start GameLoop ***
    Do While InGame
        Tick = GetTickCount                            ' Set the inital tick
        ElapsedTime = Tick - FrameTime                 ' Set the time difference for time-based movement
        FrameTime = Tick                               ' Set the time second loop time to the first.

        ' handle input
        If GetForegroundWindow() = frmMain.hWnd Then
            HandleMouseInput
        End If

        ' * Check surface timers *
        ' Sprites
        If tmr10000 < Tick Then
            ' check ping
            Call GetPing
            tmr10000 = Tick + 10000
        End If

        If tmr25 < Tick Then
            InGame = IsConnected
            Call CheckKeys    ' Check to make sure they aren't trying to auto do anything

            If GetForegroundWindow() = frmMain.hWnd Then
                Call CheckInputKeys    ' Check which keys were pressed
            End If

            ' check if we need to end the CD icon
            If Count_Spellicon > 0 Then
                For i = 1 To MaxPlayerSkill
                    If PlayerSkill(i).Id > 0 Then
                        If SpellCd(i) > 0 Then
                            If SpellCd(i) + (Skill(PlayerSkill(i).Id).Cooldown * 1000) < Tick Then
                                SpellCd(i) = 0
                            End If
                        End If
                    End If
                Next
            End If

            ' check if we need to unlock the player's spell casting restriction
            If SpellBuffer > 0 Then
                If SpellBufferTimer + (Skill(PlayerSkill(SpellBuffer).Id).CastTime * 1000) < Tick Then
                    SpellBuffer = 0
                    SpellBufferTimer = 0
                End If
            End If

            If CanMoveNow Then
                Call CheckMovement    ' Check if player is trying to move
                Call CheckAttack   ' Check to see if player is trying to attack
            End If

            For i = 1 To MAX_BYTE
                CheckAnimInstance i
            Next

            tmr25 = Tick + 25
        End If

        ' targetting
        If targetTmr < Tick Then
            If TabDown Then
                FindNearestTarget
            End If

            targetTmr = Tick + 50
        End If

        ' chat timer
        If chatTmr < Tick Then
            ' scrolling
            If ChatButtonUp Then
                ScrollChatBox 0
            End If

            If ChatButtonDown Then
                ScrollChatBox 1
            End If

            ' remove messages
            If ChatLastRemove + ChatDifferenceTimer < GetTickCount Then
                ' remove timed out messages from chat
                For i = Chat_HighIndex To 1 Step -1
                    If Len(Chat(i).Text) > 0 Then
                        If Chat(i).Visible Then
                            If Chat(i).Timer + CHAT_TIMER < Tick Then
                                Chat(i).Visible = False
                                ChatLastRemove = GetTickCount
                                Exit For
                            End If
                        End If
                    End If
                Next
            End If

            chatTmr = Tick + 50
        End If

        ' fog scrolling
        If fogTmr < Tick Then
            ' move
            FogOffsetX = FogOffsetX - 1
            FogOffsetY = FogOffsetY - 1

            ' reset
            If FogOffsetX < -256 Then FogOffsetX = 0
            If FogOffsetY < -256 Then FogOffsetY = 0

            ' reset timer
            fogTmr = Tick + 20
        End If

        ' elastic bars
        If barTmr < Tick Then
            SetBarWidth BarWidth_GuiHP_Max, BarWidth_GuiHP
            SetBarWidth BarWidth_GuiSP_Max, BarWidth_GuiSP
            SetBarWidth BarWidth_GuiEXP_Max, BarWidth_GuiEXP

            SetBarWidth BarWidth_GuiGuildExp_Max, BarWidth_GuiGuildExp

            SetBarWidth BarWidth_TargetHP_Max, BarWidth_TargetHP
            SetBarWidth BarWidth_TargetMP_Max, BarWidth_TargetMP

            For i = 1 To Npc_HighIndex
                If Not MapNpc(i).Dead Then
                    If MapNpc(i).Num > 0 Then
                        SetBarWidth BarWidth_NpcHP_Max(i), BarWidth_NpcHP(i)
                    End If
                End If
            Next

            For i = 1 To Player_HighIndex
                If IsPlaying(i) And GetPlayerMap(i) = GetPlayerMap(MyIndex) Then
                    SetBarWidth BarWidth_PlayerHP_Max(i), BarWidth_PlayerHP(i)
                End If
            Next

            ' reset timer
            barTmr = Tick + 15
        End If

        ' Animations!
        If mapTimer < Tick Then
            ' animate textbox
            If chatShowLine = "|" Then
                chatShowLine = vbNullString
            Else
                chatShowLine = "|"
            End If

            ' re-set timer
            mapTimer = Tick + 500
        End If

        ' Process input before rendering, otherwise input will be behind by 1 frame
        If WalkTimer < Tick Then

            For i = 1 To Player_HighIndex
                If IsPlaying(i) Then
                    Call ProcessMovement(i)
                End If
            Next i

            ' Process npc movements (actually move them)
            For i = 1 To Npc_HighIndex
                If Not MapNpc(i).Dead Then
                    If MapNpc(i).Num > 0 Then
                        If Not MapNpc(i).Dying Then
                            Call ProcessNpcMovement(i)
                        End If
                    End If
                End If
            Next i

            WalkTimer = Tick + 30    ' edit this value to change WalkTimer
        End If

        If tmr1000 < Tick Then
            ' Check for player
            Call CalculatePlayerIconRemainTime
            Call CalculateNpcIconRemainTime
            Call CalculatePartyIconRemainTime

            ' Progress bar effect
            Call ProcessCraftItem
            Call ProcessUpgradeItem
            Call ProcessRollDice

            tmr1000 = Tick + 1000
        End If

        ' fading
        If fadeTmr < Tick Then
            ' Disappear corpse
            Call ProcessCorpseFadeAlpha

            fadeTmr = Tick + 45
        End If

        ' Change target frames (animation)
        Call UpdateTargetCalculation

        ' *********************
        ' ** Render Graphics **
        ' *********************
        Call Render_Graphics

        DoEvents

        ' Lock fps
        If Not FPS_Lock Then
            Sleep 1
        End If

        ' Calculate fps
        If TickFPS < Tick Then
            GameFPS = FPS
            TickFPS = Tick + 1000
            FPS = 0
        Else
            FPS = FPS + 1
        End If
    Loop

    frmMain.Visible = False

    If isLogging Then
        isLogging = False
        MenuLoop
        GettingMap = True
        Stop_Music
        Play_Music MenuMusic
    Else
        ' Shutdown the game
        Call SetStatus("Destruindo os dados do jogo.")
        Call DestroyGame
    End If

End Sub

Public Sub MenuLoop()
    Dim FrameTime As Long, Tick As Long, TickFPS As Long, FPS As Long, tmr500 As Long, fadeTmr As Long
    Dim tmr1000 As Long

    ' *** Start GameLoop ***
    Do While inMenu
        Tick = GetTickCount                            ' Set the inital tick
        ElapsedTime = Tick - FrameTime                 ' Set the time difference for time-based movement
        FrameTime = Tick                               ' Set the time second loop time to the first.

        ' handle input
        If GetForegroundWindow() = frmMain.hWnd Then
            HandleMouseInput
        End If

        ' Animations!
        If tmr500 < Tick Then
            ' animate textbox
            If chatShowLine = "|" Then
                chatShowLine = vbNullString
            Else
                chatShowLine = "|"
            End If

            ' re-set timer
            tmr500 = Tick + 500
        End If

        ' Process time in character screen.
        If tmr1000 < Tick Then
            Call ProcessCharacterDeleteDate

            tmr1000 = Tick + 999
        End If

        ' fading
        If fadeTmr < Tick Then
            If FadeAlpha > 5 Then
                ' lower fade
                FadeAlpha = FadeAlpha - 5
            Else
                FadeAlpha = 0
            End If

            fadeTmr = Tick + 1
        End If

        ' *********************
        ' ** Render Graphics **
        ' *********************
        Call Render_Menu

        ' do events
        DoEvents

        ' Lock fps
        If Not FPS_Lock Then
            Sleep 1
        End If

        ' Calculate fps
        If TickFPS < Tick Then
            GameFPS = FPS
            TickFPS = Tick + 1000
            FPS = 0
        Else
            FPS = FPS + 1
        End If
    Loop
End Sub
