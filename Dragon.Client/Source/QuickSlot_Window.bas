Attribute VB_Name = "QuickSlot_Window"
Option Explicit

Public Sub CreateWindow_QuickSlot()

' Create window
    CreateWindow "winHotbar", "", zOrder_Win, 372, 10, 418, 36, 0, , , , , , , , , , , , , GetAddress(AddressOf QuickSlot_MouseMove), GetAddress(AddressOf QuickSlot_MouseDown), GetAddress(AddressOf QuickSlot_MouseMove), GetAddress(AddressOf QuickSlot_DblClick), False, False, GetAddress(AddressOf DrawHotbar)
End Sub

Public Sub DrawHotbar()
    Dim xO As Long, yO As Long, Width As Long, Height As Long, i As Long, t As Long, sS As String

    xO = Windows(GetWindowIndex("winHotbar")).Window.Left
    yO = Windows(GetWindowIndex("winHotbar")).Window.Top

    For i = 1 To MaximumQuickSlot
        xO = Windows(GetWindowIndex("winHotbar")).Window.Left + HotbarLeft + ((i - 1) * HotbarOffsetX)
        yO = Windows(GetWindowIndex("winHotbar")).Window.Top + HotbarTop
        Width = 36
        Height = 36

        ' render box
        RenderTexture Tex_GUI(24), xO - 2, yO - 2, 0, 0, Width, Height, Width, Height
        ' render icon
        If Not (DragBox.Origin = OriginQuickSlot And DragBox.Slot = i) Then
            Select Case QuickSlot(i).SType
            Case 1    ' inventory
                If QuickSlot(i).Slot > 0 Then
                    If Len(Item(QuickSlot(i).Slot).Name) > 0 And Item(QuickSlot(i).Slot).IconId > 0 Then
                        RenderTexture Tex_Item(Item(QuickSlot(i).Slot).IconId), xO, yO, 0, 0, 32, 32, 32, 32
                    End If
                End If
            Case 2    ' spell
                If Len(Skill(QuickSlot(i).Slot).Name) > 0 And Skill(QuickSlot(i).Slot).IconId > 0 Then
                    RenderTexture Tex_Spellicon(Skill(QuickSlot(i).Slot).IconId), xO, yO, 0, 0, 32, 32, 32, 32
                    For t = 1 To MaxPlayerSkill
                        If PlayerSkill(t).Id > 0 Then
                            If PlayerSkill(t).Id = QuickSlot(i).Slot And SpellCd(t) > 0 Then
                                RenderTexture Tex_Spellicon(Skill(QuickSlot(i).Slot).IconId), xO, yO, 0, 0, 32, 32, 32, 32, D3DColorARGB(255, 100, 100, 100)
                            End If
                        End If
                    Next
                End If
            End Select
        End If
        ' draw the numbers
        sS = Str(i)
        If i = 10 Then sS = "0"
        RenderText Font(Fonts.FontRegular), sS, xO + 4, yO + 19, White
    Next
End Sub

Public Function IsHotbar(StartX As Long, StartY As Long) As Long
    Dim TempRec As RECT
    Dim i As Long

    For i = 1 To MaximumQuickSlot
        If QuickSlot(i).Slot Then
            With TempRec
                .Top = StartY + HotbarTop
                .Bottom = .Top + PIC_Y
                .Left = StartX + HotbarLeft + ((i - 1) * HotbarOffsetX)
                .Right = .Left + PIC_X
            End With

            If currMouseX >= TempRec.Left And currMouseX <= TempRec.Right Then
                If currMouseY >= TempRec.Top And currMouseY <= TempRec.Bottom Then
                    IsHotbar = i
                    Exit Function
                End If
            End If
        End If
    Next
End Function

Public Sub SwitchHotbar(OldSlot As Long, NewSlot As Long)
    If OldSlot < 1 Or OldSlot > MaximumQuickSlot Then
        Exit Sub
    End If

    If NewSlot < 1 Or NewSlot > MaximumQuickSlot Then
        Exit Sub
    End If

    Call SendSwapQuickSlot(OldSlot, NewSlot)
End Sub
