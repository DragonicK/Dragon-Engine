Attribute VB_Name = "NewCharacter_Window"
Option Explicit

Private WindowIndex As Long

Public Sub CreateWindow_NewChar()

    ' Create window
    CreateWindow "winNewModel", "Novo Personagem", zOrder_Win, 0, 0, 440, 224, 0, False, Fonts.FontRegular, , 2, 6, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, , , , , , , , , False
    
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Close button
    CreateButton WindowCount, "ButtonClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf ButtonNewChar_Cancel)
    
    ' Name Label
    CreateLabel WindowCount, "lblName", 128, 70, 40, , "Nome", FontRegular, White, Alignment.AlignLeft
    
    ' Textbox Name
    CreateTextbox WindowCount, "txtName", 180, 60, 236, 30, , Fonts.FontRegular, , Alignment.AlignLeft, , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , , , , , 6, 8
    
    ' Select Gender Button
    CreateButton WindowCount, "CheckMale", 128, 110, 11, 13, , , , , , , Tex_GUI(12), Tex_GUI(13), Tex_GUI(14), , , , , , GetAddress(AddressOf CheckNewChar_Male)
    CreateButton WindowCount, "CheckFemale", 408, 110, 11, 13, , , , , , , Tex_GUI(15), Tex_GUI(16), Tex_GUI(17), , , , , , GetAddress(AddressOf CheckNewChar_Female)
    ' Background Oval
    CreatePictureBox WindowCount, "picShadow_3", 146, 110, 252, 12, , , , , , , , DesignTypes.DesignBackgroundOval, DesignTypes.DesignBackgroundOval, DesignTypes.DesignBackgroundOval
    ' Sex Label
    CreateLabel WindowCount, "lblSex", 146, 110, 252, , "Masculino", FontRegular, White, Alignment.AlignCenter
    
    ' Sprite Button
    CreateButton WindowCount, "ButtonLeft", 128, 137, 11, 13, , , , , , , Tex_GUI(12), Tex_GUI(13), Tex_GUI(14), , , , , , GetAddress(AddressOf ButtonNewChar_Left)
    CreateButton WindowCount, "ButtonRight", 408, 137, 11, 13, , , , , , , Tex_GUI(15), Tex_GUI(16), Tex_GUI(17), , , , , , GetAddress(AddressOf ButtonNewChar_Right)
    ' Background Oval
    CreatePictureBox WindowCount, "picShadow_3", 146, 137, 252, 12, , , , , , , , DesignTypes.DesignBackgroundOval, DesignTypes.DesignBackgroundOval, DesignTypes.DesignBackgroundOval
    ' Sprite Label
    CreateLabel WindowCount, "lblSprite", 146, 137, 252, , "Sprite", FontRegular, White, Alignment.AlignCenter

    ' Acept Button
    CreateButton WindowCount, "ButtonAccept", 223, 174, 197, 30, "Criar", FontRegular, , , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf ButtonNewChar_Accept)
    
    ' Cancel Button
    CreateButton WindowCount, "ButtonCancel", 20, 174, 197, 30, "Cancelar", FontRegular, , , , , , , , DesignTypes.DesignRed, DesignTypes.DesignRedHover, DesignTypes.DesignRedClick, , , GetAddress(AddressOf ButtonNewChar_Cancel)
    
    ' Background Face
    CreatePictureBox WindowCount, "picScene", 20, 60, 96, 96, , , , , Tex_GUI(11), Tex_GUI(11), Tex_GUI(11), , , , , , , , , GetAddress(AddressOf NewChar_OnDraw)

    ' Set the active control
    SetActiveControl GetWindowIndex("winNewModel"), GetControlIndex("winNewModel", "txtName")
    
    WindowIndex = WindowCount
End Sub

Private Sub NewChar_OnDraw()
    Dim imageFace As Long, imageChar As Long, xO As Long, yO As Long

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top

    If NewCharGender = SEX_MALE Then
        imageFace = Tex_Face(Class(NewCharClass).MaleSprite(NewCharSprite))
    Else
        imageFace = Tex_Face(Class(NewCharClass).FemaleSprite(NewCharSprite))
    End If

    ' render face
    RenderTexture imageFace, xO + 21, yO + 61, 0, 0, 94, 94, 94, 94
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
    
    Windows(WindowIndex).Controls(GetControlIndex("winNewModel", "lblSex")).Text = "Masculino"
End Sub

Private Sub CheckNewChar_Female()
    NewCharSprite = 1
    NewCharGender = SEX_FEMALE
    
    Windows(WindowIndex).Controls(GetControlIndex("winNewModel", "lblSex")).Text = "Feminino"
End Sub

Private Sub ButtonNewChar_Cancel()
    Windows(WindowIndex).Controls(GetControlIndex("winNewModel", "txtName")).Text = vbNullString
    Windows(WindowIndex).Controls(GetControlIndex("winNewModel", "CheckMale")).Value = 1
    Windows(WindowIndex).Controls(GetControlIndex("winNewModel", "CheckFemale")).Value = 0
    
    NewCharSprite = 1
    NewCharGender = SEX_MALE
    
    HideWindows
    
    ShowWindow GetWindowIndex("winClasses")
End Sub

Private Sub ButtonNewChar_Accept()
    Dim Name As String
    Name = Windows(WindowIndex).Controls(GetControlIndex("winNewModel", "txtName")).Text
    
    If Len(Windows(WindowIndex).Controls(GetControlIndex("winNewModel", "txtName")).Text) >= 5 Then
        HideWindows
        AddChar Name, NewCharGender, NewCharClass, NewCharSprite
    Else
        ShowDialogue "Error!", "O Nome escolhido não atente aos critérios,", "Verifique o nome e tente novamente.", DialogueTypeAlert
    End If

End Sub

Private Sub AddChar(Name As String, Sex As Long, Class As Long, Sprite As Long)

    If ConnectToGameServer Then
        HideWindows
        Call SetStatus("Enviando informações do personagem.")
        Call SendAddChar(Name, Sex, Class, Sprite)
        Exit Sub
    Else
        ShowWindow GetWindowIndex("winLogin")
        ShowWindow GetWindowIndex("winLoginFooter")
        ShowDialogue "Problema de Conexao", "Não pode conectar-se ao servidor de jogo.", "", DialogueTypeAlert
    End If

End Sub

Public Function IsStringLegal(ByVal sInput As String) As Boolean
    Dim i As Long, tmpNum As Long
    ' Prevent high ascii chars
    tmpNum = Len(sInput)

    For i = 1 To tmpNum

        If Asc(Mid$(sInput, i, 1)) < vbKeySpace Or Asc(Mid$(sInput, i, 1)) > vbKeyF15 Then
            ShowDialogue "Models ilegais", "O nome contém caracteres não permitidos.", "", DialogueTypeAlert
            Exit Function
        End If

    Next

    IsStringLegal = True
End Function
