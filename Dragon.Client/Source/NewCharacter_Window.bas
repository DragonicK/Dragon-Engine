Attribute VB_Name = "NewCharacter_Window"
Option Explicit

Public Sub CreateWindow_NewChar()
' Create window
    CreateWindow "winNewChar", "NOVO PERSONAGEM", zOrder_Win, 0, 0, 291, 200, 0, False, Fonts.OpenSans_Regular, , 2, 6, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, , , , , , , , False
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Close button
    CreateButton WindowCount, "ButtonClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf ButtonNewChar_Cancel)
    ' Name

    CreateLabel WindowCount, "lblName", 29, 49, 124, , "Name", OpenSans_Regular, White, Alignment.AlignCenter
    ' Textbox
    CreateTextbox WindowCount, "txtName", 29, 65, 124, 24, , Fonts.OpenSans_Regular, , Alignment.AlignLeft, , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , , , , , 5, 3
    ' Gender
    CreateLabel WindowCount, "lblGender", 29, 92, 124, , "Gender", OpenSans_Regular, White, Alignment.AlignCenter
    ' Checkboxes
    CreateCheckbox WindowCount, "CheckMale", 29, 113, 55, , 1, "Male", OpenSans_Regular, , Alignment.AlignCenter, , , DesignTypes.DesignCheckBox, , , GetAddress(AddressOf CheckNewChar_Male), , , 1
    CreateCheckbox WindowCount, "CheckFemale", 90, 113, 62, , 0, "Female", OpenSans_Regular, , Alignment.AlignCenter, , , DesignTypes.DesignCheckBox, , , GetAddress(AddressOf CheckNewChar_Female), , , 1
    ' Buttons
    CreateButton WindowCount, "ButtonAccept", 29, 137, 60, 26, "Accept", OpenSans_Regular, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ButtonNewChar_Accept)
    CreateButton WindowCount, "ButtonCancel", 93, 137, 60, 26, "Cancel", OpenSans_Regular, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ButtonNewChar_Cancel)
    ' Sprite
    CreatePictureBox WindowCount, "picShadow_3", 175, 52, 76, 9, , , , , , , , DesignTypes.DesignBackgroundOval, DesignTypes.DesignBackgroundOval, DesignTypes.DesignBackgroundOval
    CreateLabel WindowCount, "lblSprite", 175, 49, 76, , "Sprite", OpenSans_Regular, White, Alignment.AlignCenter
    ' Scene
    CreatePictureBox WindowCount, "picScene", 165, 65, 96, 96, , , , , Tex_GUI(11), Tex_GUI(11), Tex_GUI(11), , , , , , , , , GetAddress(AddressOf NewChar_OnDraw)
    ' Buttons
    CreateButton WindowCount, "ButtonLeft", 163, 50, 11, 13, , , , , , , Tex_GUI(12), Tex_GUI(14), Tex_GUI(16), , , , , , GetAddress(AddressOf ButtonNewChar_Left)
    CreateButton WindowCount, "ButtonRight", 252, 50, 11, 13, , , , , , , Tex_GUI(13), Tex_GUI(15), Tex_GUI(17), , , , , , GetAddress(AddressOf ButtonNewChar_Right)

    ' Set the active control
    SetActiveControl GetWindowIndex("winNewChar"), GetControlIndex("winNewChar", "txtName")
End Sub

Private Sub NewChar_OnDraw()
    Dim imageFace As Long, imageChar As Long, xO As Long, yO As Long

    xO = Windows(GetWindowIndex("winNewChar")).Window.Left
    yO = Windows(GetWindowIndex("winNewChar")).Window.Top


    If NewCharGender = SEX_MALE Then
        imageFace = Tex_Face(Class(NewCharClass).MaleSprite(NewCharSprite))
        ' imageChar = Tex_Char(Class(newCharClass).MaleSprite(newCharSprite))
    Else
        imageFace = Tex_Face(Class(NewCharClass).FemaleSprite(NewCharSprite))
        '   imageChar = Tex_Char(Class(newCharClass).FemaleSprite(newCharSprite))
    End If

    ' render face
    RenderTexture imageFace, xO + 166, yO + 66, 0, 0, 94, 94, 94, 94
    ' render char
    '  RenderTexture imageChar, xO + 166, yO + 126, 32, 0, 32, 32, 32, 32
End Sub

Private Sub ButtonNewChar_Left()
    Dim spriteCount As Long

    If NewCharGender = SEX_MALE Then
        spriteCount = UBound(Class(NewCharClass).MaleSprite)
    Else
        spriteCount = UBound(Class(NewCharClass).FemaleSprite)
    End If

    If NewCharSprite <= 0 Then
        NewCharSprite = spriteCount
    Else
        NewCharSprite = NewCharSprite - 1

        If NewCharSprite = 0 Then
            NewCharSprite = spriteCount
        End If
    End If
End Sub

Private Sub ButtonNewChar_Right()
    Dim spriteCount As Long

    If NewCharGender = SEX_MALE Then
        spriteCount = UBound(Class(NewCharClass).MaleSprite)
    Else
        spriteCount = UBound(Class(NewCharClass).FemaleSprite)
    End If

    If NewCharSprite >= spriteCount Then
        NewCharSprite = 1
    Else
        NewCharSprite = NewCharSprite + 1
    End If
End Sub

Private Sub CheckNewChar_Male()
    NewCharSprite = 1
    NewCharGender = SEX_MALE
End Sub

Private Sub CheckNewChar_Female()
    NewCharSprite = 1
    NewCharGender = SEX_FEMALE
End Sub

Private Sub ButtonNewChar_Cancel()
    Windows(GetWindowIndex("winNewChar")).Controls(GetControlIndex("winNewChar", "txtName")).Text = vbNullString
    Windows(GetWindowIndex("winNewChar")).Controls(GetControlIndex("winNewChar", "CheckMale")).Value = 1
    Windows(GetWindowIndex("winNewChar")).Controls(GetControlIndex("winNewChar", "CheckFemale")).Value = 0
    NewCharSprite = 1
    NewCharGender = SEX_MALE
    HideWindows
    ShowWindow GetWindowIndex("winClasses")
End Sub

Private Sub ButtonNewChar_Accept()
    Dim Name As String
    Name = Windows(GetWindowIndex("winNewChar")).Controls(GetControlIndex("winNewChar", "txtName")).Text

    HideWindows
    AddChar Name, NewCharGender, NewCharClass, NewCharSprite
End Sub
