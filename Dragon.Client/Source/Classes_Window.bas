Attribute VB_Name = "Classes_Window"
Public Sub CreateWindow_Classes()

    ' Create window
    CreateWindow "winClasses", "Seleção de Classe", zOrder_Win, 0, 0, 440, 250, 0, False, Fonts.FontRegular, , 2, 6, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, , , , , , , , , False
    
    ' Centralise it
    CentraliseWindow WindowCount, 0

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Close button
    CreateButton WindowCount, "ButtonClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf ButtonClasses_Close)
    
    ' Background Face
    CreatePictureBox WindowCount, "picScene_1", 20, 80, 96, 96, , , , , Tex_GUI(9), Tex_GUI(9), Tex_GUI(9), , , , , , , , , GetAddress(AddressOf Classes_DrawFace)
    
    ' Background Oval
    CreatePictureBox WindowCount, "picShadow", 48, 55, 342, 12, , , , , , , , DesignTypes.DesignBackgroundOval, DesignTypes.DesignBackgroundOval, DesignTypes.DesignBackgroundOval
    
    ' Class Name
    CreateLabel WindowCount, "lblClassName", 48, 55, 342, , "Warrior", FontRegular, White, Alignment.AlignCenter
    
    ' Button Left
    CreateButton WindowCount, "ButtonLeft", 20, 55, 11, 13, , , , , , , Tex_GUI(12), Tex_GUI(13), Tex_GUI(14), , , , , , GetAddress(AddressOf ButtonClasses_Left)
    
    ' Button Right
    CreateButton WindowCount, "ButtonRight", 410, 55, 11, 13, , , , , , , Tex_GUI(15), Tex_GUI(16), Tex_GUI(17), , , , , , GetAddress(AddressOf ButtonClasses_Right)
    
    ' Accept Button
    CreateButton WindowCount, "ButtonAccept", 20, 200, 400, 30, "Selecionar Classe", FontRegular, , , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf ButtonClasses_Accept)
    
    ' Overlay
    CreatePictureBox WindowCount, "picOverlay", 0, 0, 0, 0, , , , , , , , , , , , , , , , GetAddress(AddressOf Classes_DrawText)
    
    
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
    RenderTexture imageFace, xO + 21, yO + 81, 0, 0, 94, 94, 94, 94
    
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
    WordWrap_Array Text, 280, TextArray()
    
    ' render text
    Count = UBound(TextArray)
    
    Y = yO + 80
    
    For i = 1 To Count
        X = xO + 132
        
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

