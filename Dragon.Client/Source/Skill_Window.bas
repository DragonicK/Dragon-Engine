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
    CreateWindow "winSkills", "HABILIDADES", zOrder_Win, 0, 0, 300, 410, 0, False, Fonts.FontRegular, , 2, 7, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, , , , , GetAddress(AddressOf Skills_MouseMove), GetAddress(AddressOf Skills_MouseDown), GetAddress(AddressOf Skills_MouseMove), GetAddress(AddressOf Skills_DblClick), , , GetAddress(AddressOf DrawSkills)
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Close button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf btnMenu_Skills)

    CreateButton WindowCount, "btnActive", 38, 42, 100, 26, "ATIVA", FontRegular, Green, , , , , , , , , , , , GetAddress(AddressOf Button_ShowSkill)
    CreateButton WindowCount, "btnPassive", 173, 42, 100, 26, "PASSIVA", FontRegular, , , , , , , , , , , , , GetAddress(AddressOf Button_ShowPassive)
    
    
    CreateButton WindowCount, "btnUpAttributes", ListX * 27, ListY * 8, 15, 15, , , , , , , Tex_GUI(44), Tex_GUI(45), Tex_GUI(46), , , , , , GetAddress(AddressOf MovePageUp)
    CreateButton WindowCount, "btnDownAttributes", ListX * 27, (ListY * 36) + 28, 15, 15, , , , , , , Tex_GUI(47), Tex_GUI(48), Tex_GUI(49), , , , , , GetAddress(AddressOf MovePageDown)
    

    'Botões setas
    'CreateLabel WindowCount, "lblPage", 92, 350, 120, 50, "Página: 1/" & SkillPageCount, FontRegular, White, Alignment.AlignCenter

    WindowIndex = WindowCount
End Sub

Public Sub DrawSkills()
    Dim xO As Long, yO As Long, Width As Long, i As Long
    Dim SkillIndex As Long, SkillNum As Long, SkillLevel As Long, SkillIcon As Long
    ' Y As Long, SpellNum As Long, spellPic As Long, X As Long, top As Long, left As Long

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top
    Width = Windows(WindowIndex).Window.Width
    
    '

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
                    If Not (DragBox.Origin = OriginSpells And DragBox.Slot = SkillIndex) Then
                        SkillIcon = Skill(SkillNum).IconId

                        If SkillIcon > 0 And SkillIcon <= Count_Spellicon Then
                            RenderTexture Tex_Spellicon(SkillIcon), xO + SkillLeft, yO + ListY * 5 + (i * SkillOffsetY), 0, 0, 32, 32, 32, 32
                        End If
                    End If

                    RenderText Font(Fonts.FontRegular), Skill(SkillNum).Name, xO + ListX * 7, yO + (ListY * 5) + (i * SkillOffsetY) + 4, BrightGreen
                    RenderText Font(Fonts.FontRegular), "Level " & SkillLevel, xO + ListX * 7, yO + (ListY * 5) + (i * SkillOffsetY) + 18, White
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
                .Type = PartSpell
                .Value = PlayerSkill(SlotNum).Id
                .Origin = OriginSpells
                .Slot = SlotNum
            End With

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
    If DragBox.Type <> PartNone Then Exit Sub

    SlotNum = GetSkillSlotFromPosition(Windows(WindowIndex).Window.Left, Windows(WindowIndex).Window.Top)

    If SlotNum Then
        ' make sure we're not dragging the item
        If DragBox.Type = PartItem And DragBox.Value = SlotNum Then Exit Sub
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

    Windows(WindowIndex).Controls(BtnPassive).TextColour = White
    Windows(WindowIndex).Controls(BtnPassive).TextColourHover = White
    Windows(WindowIndex).Controls(BtnPassive).TextColourClick = White

    Windows(WindowIndex).Controls(BtnActive).TextColour = Gold
    Windows(WindowIndex).Controls(BtnActive).TextColourHover = Gold
    Windows(WindowIndex).Controls(BtnActive).TextColourClick = Gold
End Sub

Private Sub Button_ShowPassive()
    SkillPage = 1
    ShowPassiveList = True

    Dim BtnPassive As Long
    Dim BtnActive As Long

    BtnPassive = GetControlIndex("winSkills", "btnPassive")
    BtnActive = GetControlIndex("winSkills", "btnActive")

    Windows(WindowIndex).Controls(BtnActive).TextColour = White
    Windows(WindowIndex).Controls(BtnActive).TextColourHover = White
    Windows(WindowIndex).Controls(BtnActive).TextColourClick = White

    Windows(WindowIndex).Controls(BtnPassive).TextColour = Gold
    Windows(WindowIndex).Controls(BtnPassive).TextColourHover = Gold
    Windows(WindowIndex).Controls(BtnPassive).TextColourClick = Gold
End Sub

Private Sub MovePageUp()
    If SkillPage < SkillPageCount Then
        SkillPage = SkillPage + 1
        
      '  Windows(WindowIndex).Controls(GetControlIndex("winSkills", "lblPage")).Text = "Página: " & SkillPage & "/" & SkillPageCount
    End If
End Sub

Private Sub MovePageDown()
    If SkillPage > 1 Then
        SkillPage = SkillPage - 1
        
       ' Windows(WindowIndex).Controls(GetControlIndex("winSkills", "lblPage")).Text = "Página: " & SkillPage & "/" & SkillPageCount
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
