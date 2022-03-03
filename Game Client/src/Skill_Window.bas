Attribute VB_Name = "Skill_Window"
Option Explicit

Public Const MaxSkillList As Long = 6

Private WindowIndex As Long
Private ShowPassiveList As Boolean
Private SkillPage As Integer
Private SkillPageCount As Integer

Private Const SkillTop As Long = 50
Private Const SkillLeft As Long = 25
Private Const SkillOffsetY As Long = 40
Private Const SkillOffsetX As Long = 6

Private Const ListX As Integer = 10
Private Const ListY As Integer = 10

Public Sub CreateWindow_Skills()
' Count pages
    SetSkillPageCount

    ' Create window
    CreateWindow "winSkills", "HABILIDADES", zOrder_Win, 0, 0, 300, 410, 0, False, Fonts.OpenSans_Effect, , 2, 7, DesignTypes.desWin_AincradNorm, DesignTypes.desWin_AincradNorm, DesignTypes.desWin_AincradNorm, , , , , GetAddress(AddressOf Skills_MouseMove), GetAddress(AddressOf Skills_MouseDown), GetAddress(AddressOf Skills_MouseMove), GetAddress(AddressOf Skills_DblClick), , , GetAddress(AddressOf DrawSkills)
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Close button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf btnMenu_Skills)

    CreateButton WindowCount, "btnActive", 38, 42, 100, 26, "ATIVA", OpenSans_Effect, Green, , , , , , , , , , , , GetAddress(AddressOf Button_ShowSkill)
    CreateButton WindowCount, "btnPassive", 173, 42, 100, 26, "PASSIVA", OpenSans_Effect, , , , , , , , , , , , , GetAddress(AddressOf Button_ShowPassive)
    
    
    CreateButton WindowCount, "btnUpAttributes", ListX * 27, ListY * 8, 15, 15, , , , , , , Tex_GUI(110), Tex_GUI(110), Tex_GUI(110), , , , , , GetAddress(AddressOf MovePageUp)
    CreateButton WindowCount, "btnDownAttributes", ListX * 27, (ListY * 36) + 28, 15, 15, , , , , , , Tex_GUI(111), Tex_GUI(111), Tex_GUI(111), , , , , , GetAddress(AddressOf MovePageDown)
    
    CreateButton WindowCount, "ScrollUpAttributes", (ListX * 27) + 2, (ListY * 8) + 16, 8, 138, , , , , , , , , , DesignTypes.desButton, DesignTypes.desButton_Hover, DesignTypes.desButton_Click, , , GetAddress(AddressOf MovePageUp)
    CreateButton WindowCount, "ScrollDownAttributes", (ListX * 27) + 2, (ListY * 22) + 14, 8, 146, , , , , , , , , , DesignTypes.desButton, DesignTypes.desButton_Hover, DesignTypes.desButton_Click, , , GetAddress(AddressOf MovePageDown)

    'Botões setas
    'CreateLabel WindowCount, "lblPage", 92, 350, 120, 50, "Página: 1/" & SkillPageCount, OpenSans_Effect, White, Alignment.alignCentre

    WindowIndex = WindowCount
End Sub

Public Sub DrawSkills()
    Dim xO As Long, yO As Long, Width As Long, i As Long
    Dim SkillIndex As Long, SkillNum As Long, SkillLevel As Long, SkillIcon As Long
    ' Y As Long, SpellNum As Long, spellPic As Long, X As Long, top As Long, left As Long

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top
    Width = Windows(WindowIndex).Window.Width
    
    RenderDesign DesignTypes.desWin_AincradMenu, xO, yO + 40, Width, 30

    For i = 1 To MaxSkillList
        SkillIndex = ((SkillPage - 1) * MaxSkillList) + i

        If SkillIndex <= MaxPlayerSkill Then

            If ShowPassiveList Then
                SkillNum = PlayerPassive(SkillIndex).Id
                SkillLevel = PlayerPassive(SkillIndex).Level
            Else
                SkillNum = PlayerSkill(SkillIndex).Id
                SkillLevel = PlayerSkill(SkillIndex).Level
            End If

            If SkillNum > 0 Then
                If SkillNum > 0 And SkillNum <= MaximumSkills Then
                    ' not dragging?
                    If Not (DragBox.Origin = origin_Spells And DragBox.Slot = SkillIndex) Then
                        SkillIcon = Skill(SkillNum).IconId

                        If SkillIcon > 0 And SkillIcon <= Count_Spellicon Then
                            RenderTexture Tex_Spellicon(SkillIcon), xO + SkillLeft, yO + ListY * 5 + (i * SkillOffsetY), 0, 0, 32, 32, 32, 32
                        End If
                    End If

                    RenderText Font(Fonts.OpenSans_Effect), Skill(SkillNum).Name, xO + ListX * 7, yO + (ListY * 5) + (i * SkillOffsetY) + 4, BrightGreen
                    RenderText Font(Fonts.OpenSans_Effect), "Level " & SkillLevel, xO + ListX * 7, yO + (ListY * 5) + (i * SkillOffsetY) + 18, White
                End If
            End If

        End If
    Next

End Sub

Public Sub Skills_MouseDown()
    Dim SlotNum As Long, WinIndex As Long

    If Not ShowPassiveList Then
        ' is there an item?
        SlotNum = GetSkillSlotFromPosition(Windows(WindowIndex).Window.Left, Windows(WindowIndex).Window.Top)

        If SlotNum Then
            With DragBox
                .Type = Part_spell
                .Value = PlayerSkill(SlotNum).Id
                .Origin = origin_Spells
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
            Windows(WindowIndex).Window.State = Normal
        End If

    End If

    ' show desc. if needed
    Skills_MouseMove
End Sub

Public Sub Skills_DblClick()
'Dim SlotNum As Long

' SlotNum = GetSkillSlotFromPosition(Windows(GetWindowIndex("winSkills")).Window.left, Windows(GetWindowIndex("winSkills")).Window.top)

'If SlotNum Then
'   CastSpell SlotNum
' End If

' show desc. if needed
    Skills_MouseMove
End Sub

Private Sub Skills_MouseMove()
    Dim SlotNum As Long, X As Long, Y As Long

    ' exit out early if dragging
    If DragBox.Type <> part_None Then Exit Sub

    SlotNum = GetSkillSlotFromPosition(Windows(WindowIndex).Window.Left, Windows(WindowIndex).Window.Top)

    If SlotNum Then
        ' make sure we're not dragging the item
        If DragBox.Type = Part_Item And DragBox.Value = SlotNum Then Exit Sub
        ' calc position
        X = Windows(WindowIndex).Window.Left - Windows(GetWindowIndex("winDescription")).Window.Width - 2
        Y = Windows(WindowIndex).Window.Top

        ' offscreen?
        If X < 0 Then
            ' switch to right
            X = Windows(WindowIndex).Window.Left + Windows(WindowIndex).Window.Width + 2
        End If

        ShowPlayerSkillDesc X, Y, SlotNum, ShowPassiveList
    End If
End Sub

Private Sub Button_ShowSkill()
    SkillPage = 1
    ShowPassiveList = False

    Dim BtnPassive As Long
    Dim BtnActive As Long

    BtnPassive = GetControlIndex("winSkills", "btnPassive")
    BtnActive = GetControlIndex("winSkills", "btnActive")

    Windows(WindowIndex).Controls(BtnPassive).textColour = White
    Windows(WindowIndex).Controls(BtnPassive).textColour_Hover = White
    Windows(WindowIndex).Controls(BtnPassive).textColour_Click = White

    Windows(WindowIndex).Controls(BtnActive).textColour = Gold
    Windows(WindowIndex).Controls(BtnActive).textColour_Hover = Gold
    Windows(WindowIndex).Controls(BtnActive).textColour_Click = Gold
End Sub

Private Sub Button_ShowPassive()
    SkillPage = 1
    ShowPassiveList = True

    Dim BtnPassive As Long
    Dim BtnActive As Long

    BtnPassive = GetControlIndex("winSkills", "btnPassive")
    BtnActive = GetControlIndex("winSkills", "btnActive")

    Windows(WindowIndex).Controls(BtnActive).textColour = White
    Windows(WindowIndex).Controls(BtnActive).textColour_Hover = White
    Windows(WindowIndex).Controls(BtnActive).textColour_Click = White

    Windows(WindowIndex).Controls(BtnPassive).textColour = Gold
    Windows(WindowIndex).Controls(BtnPassive).textColour_Hover = Gold
    Windows(WindowIndex).Controls(BtnPassive).textColour_Click = Gold
End Sub

Private Sub MovePageUp()
    If SkillPage < SkillPageCount Then
        SkillPage = SkillPage + 1
        'Windows(WindowIndex).Controls(GetControlIndex("winSkills", "lblPage")).Text = "Página: " & SkillPage & "/" & SkillPageCount
    End If
End Sub

Private Sub MovePageDown()
    If SkillPage > 1 Then
        SkillPage = SkillPage - 1
      '  Windows(WindowIndex).Controls(GetControlIndex("winSkills", "lblPage")).Text = "Página: " & SkillPage & "/" & SkillPageCount
    End If
End Sub

Private Sub SetSkillPageCount()
    Dim Rest As Double

    SkillPage = 1
    SkillPageCount = CInt(MaxPlayerSkill / MaxSkillList)
    Rest = MaxPlayerSkill Mod MaxSkillList

    If Rest > 0 Then SkillPageCount = SkillPageCount + 1
    If SkillPageCount = 0 Then SkillPageCount = 1
End Sub

Public Function GetSkillSlotFromPosition(StartX As Long, StartY As Long) As Long
    Dim TempRec As RECT
    Dim i As Long, SkillIndex As Long
    Dim SkillId As Long

    For i = 1 To MaxSkillList
        SkillIndex = ((SkillPage - 1) * MaxSkillList) + i

        If SkillIndex <= MaxPlayerSkill Then
            If ShowPassiveList Then
                SkillId = PlayerPassive(SkillIndex).Id
            Else
                SkillId = PlayerSkill(SkillIndex).Id
            End If

            If SkillId > 0 Then
                With TempRec
                    .Top = StartY + SkillTop + (i * SkillOffsetY)
                    .Bottom = .Top + PIC_Y
                    .Left = StartX + SkillLeft
                    .Right = .Left + PIC_X
                End With

                If currMouseX >= TempRec.Left And currMouseX <= TempRec.Right Then
                    If currMouseY >= TempRec.Top And currMouseY <= TempRec.Bottom Then
                        GetSkillSlotFromPosition = SkillIndex
                        Exit Function
                    End If
                End If
            End If

        End If
    Next
End Function
