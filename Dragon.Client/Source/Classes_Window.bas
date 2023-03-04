Attribute VB_Name = "Classes_Window"
Public Sub CreateWindow_Classes()
' Create window
    CreateWindow "winClasses", "SELEÇÃO DE CLASSE", zOrder_Win, 0, 0, 364, 249, 0, False, Fonts.FontRegular, , 2, 6, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, , , , , , , , , False, , GetAddress(AddressOf Classes_DrawFace)
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Close button
    CreateButton WindowCount, "ButtonClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf ButtonClasses_Close)
    ' Class Name
    CreatePictureBox WindowCount, "picShadow", 183, 52, 98, 9, , , , , , , , DesignTypes.DesignBackgroundOval, DesignTypes.DesignBackgroundOval, DesignTypes.DesignBackgroundOval
    CreateLabel WindowCount, "lblClassName", 183, 49, 98, , "Warrior", FontRegular, White, Alignment.AlignCenter
    ' Select Buttons
    CreateButton WindowCount, "ButtonLeft", 171, 50, 11, 13, , , , , , , Tex_GUI(12), Tex_GUI(13), Tex_GUI(14), , , , , , GetAddress(AddressOf ButtonClasses_Left)
    CreateButton WindowCount, "ButtonRight", 282, 50, 11, 13, , , , , , , Tex_GUI(15), Tex_GUI(16), Tex_GUI(17), , , , , , GetAddress(AddressOf ButtonClasses_Right)
    ' Accept Button
    CreateButton WindowCount, "ButtonAccept", 183, 205, 98, 26, "ACEITAR", FontRegular, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ButtonClasses_Accept)
    ' Text background
    CreatePictureBox WindowCount, "picBackground", 127, 65, 210, 134, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox
    ' Overlay
    CreatePictureBox WindowCount, "picOverlay", 6, 36, 0, 0, , , , , , , , , , , , , , , , GetAddress(AddressOf Classes_DrawText)
End Sub

Public Sub ShowClasses()
    HideWindows
    NewCharClass = 1
    NewCharSprite = 1
    NewCharGender = SEX_MALE
    Windows(GetWindowIndex("winClasses")).Controls(GetControlIndex("winClasses", "lblClassName")).Text = Trim$(Class(NewCharClass).Name)
    Windows(GetWindowIndex("winNewChar")).Controls(GetControlIndex("winNewChar", "txtName")).Text = vbNullString
    Windows(GetWindowIndex("winNewChar")).Controls(GetControlIndex("winNewChar", "CheckMale")).Value = 1
    Windows(GetWindowIndex("winNewChar")).Controls(GetControlIndex("winNewChar", "CheckFemale")).Value = 0
    ShowWindow GetWindowIndex("winClasses")
End Sub

Private Sub Classes_DrawFace()
    Dim imageFace As Long, xO As Long, yO As Long

    xO = Windows(GetWindowIndex("winClasses")).Window.Left
    yO = Windows(GetWindowIndex("winClasses")).Window.Top

    If NewCharClass = 0 Then NewCharClass = 1

    Select Case NewCharClass
    Case 1    ' Warrior
        imageFace = Tex_Face(1)
    Case 2    ' Wizard
        imageFace = Tex_Face(2)
    Case 3    ' Whisperer
        imageFace = Tex_Face(3)
    End Select

    ' render face
    RenderTexture imageFace, xO + 14, yO + 50, 0, 0, 96, 96, 96, 96
End Sub

Private Sub Classes_DrawText()
    Dim image As Long, Text As String, xO As Long, yO As Long, TextArray() As String, i As Long, Count As Long, Y As Long, X As Long

    xO = Windows(GetWindowIndex("winClasses")).Window.Left
    yO = Windows(GetWindowIndex("winClasses")).Window.Top

    Select Case NewCharClass
    Case 1    ' Warrior
        Text = Class(1).Description
    Case 2    ' Wizard
        Text = Class(2).Description
    Case 3    ' Priest
        Text = Class(3).Description
    End Select

    ' wrap text
    WordWrap_Array Text, 200, TextArray()
    
    ' render text
    Count = UBound(TextArray)
    
    Y = yO + 70
    
    For i = 1 To Count
        X = xO + 132 + (200 \ 2) - (TextWidth(Font(Fonts.FontRegular), TextArray(i)) \ 2)
        
        RenderText Font(Fonts.FontRegular), TextArray(i), X, Y, White
        
        Y = Y + 14
    Next
End Sub

Private Sub ButtonClasses_Left()
    If NewCharClass > 1 Then
        NewCharClass = NewCharClass - 1
    End If

    Windows(GetWindowIndex("winClasses")).Controls(GetControlIndex("winClasses", "lblClassName")).Text = UCase$(Class(NewCharClass).Name)
End Sub

Private Sub ButtonClasses_Right()
    If NewCharClass < MaximumClasses Then
        NewCharClass = NewCharClass + 1
    End If

    Windows(GetWindowIndex("winClasses")).Controls(GetControlIndex("winClasses", "lblClassName")).Text = UCase$(Class(NewCharClass).Name)
End Sub

Private Sub ButtonClasses_Accept()
    HideWindow GetWindowIndex("winClasses")
    ShowWindow GetWindowIndex("winNewChar")
End Sub

Private Sub ButtonClasses_Close()
    HideWindows
    ShowWindow GetWindowIndex("winModels")
    ShowWindow GetWindowIndex("winModelFooter")
End Sub

