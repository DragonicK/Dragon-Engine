Attribute VB_Name = "Characters_Window"
Option Explicit

Public Sub CreateWindow_Models()
' Create the window
    CreateWindow "winModels", "Personagens", zOrder_Win, 0, 0, 360, 250, 0, False, Fonts.FontRegular, , 3, 5, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, , , , , , , , , False
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Close button
    CreateButton WindowCount, "ButtonClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf ButtonModels_Close)

    ' Names
    CreateLabel WindowCount, "lblCharName_1", 22, 50, 98, , "Slot Vazio", FontRegular, White, Alignment.AlignCenter
    CreateLabel WindowCount, "lblCharName_2", 132, 50, 98, , "Slot Vazio", FontRegular, White, Alignment.AlignCenter
    CreateLabel WindowCount, "lblCharName_3", 242, 50, 98, , "Slot Vazio", FontRegular, White, Alignment.AlignCenter
    
    ' Scenery Boxes
    CreatePictureBox WindowCount, "picScene_1", 23, 70, 96, 96, , , , , Tex_GUI(9), Tex_GUI(9), Tex_GUI(9)
    CreatePictureBox WindowCount, "picScene_2", 133, 70, 96, 96, , , , , Tex_GUI(10), Tex_GUI(10), Tex_GUI(10)
    CreatePictureBox WindowCount, "picScene_3", 243, 70, 96, 96, , , , , Tex_GUI(11), Tex_GUI(11), Tex_GUI(11), , , , , , , , , GetAddress(AddressOf Chars_DrawFace)

    ' Date
    CreateLabel WindowCount, "lblCharDate_1", 22, 150, 98, , "", FontRegular, White, Alignment.AlignCenter
    CreateLabel WindowCount, "lblCharDate_2", 132, 150, 98, , "", FontRegular, White, Alignment.AlignCenter
    CreateLabel WindowCount, "lblCharDate_3", 242, 150, 98, , "", FontRegular, White, Alignment.AlignCenter

    ' Create Buttons
    CreateButton WindowCount, "ButtonSelectChar_1", 22, 172, 98, 30, "Selecionar", FontRegular, , , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf ButtonAcceptChar_1)
    CreateButton WindowCount, "ButtonCreateChar_1", 22, 172, 98, 30, "Criar", FontRegular, , , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf ButtonCreateChar_1)
    CreateButton WindowCount, "ButtonDelChar_1", 22, 204, 98, 30, "Apagar", FontRegular, , , , , , , , DesignTypes.DesignRed, DesignTypes.DesignRedHover, DesignTypes.DesignRedClick, , , GetAddress(AddressOf ButtonDelChar_1)

    CreateButton WindowCount, "ButtonSelectChar_2", 132, 172, 98, 30, "Selecionar", FontRegular, , , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf ButtonAcceptChar_2)
    CreateButton WindowCount, "ButtonCreateChar_2", 132, 172, 98, 30, "Criar", FontRegular, , , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf ButtonCreateChar_2)
    CreateButton WindowCount, "ButtonDelChar_2", 132, 204, 98, 30, "Apagar", FontRegular, , , , , , , , DesignTypes.DesignRed, DesignTypes.DesignRedHover, DesignTypes.DesignRedClick, , , GetAddress(AddressOf ButtonDelChar_2)

    CreateButton WindowCount, "ButtonSelectChar_3", 242, 172, 98, 30, "Selecionar", FontRegular, , , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf ButtonAcceptChar_3)
    CreateButton WindowCount, "ButtonCreateChar_3", 242, 172, 98, 30, "Criar", FontRegular, , , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf ButtonCreateChar_3)
    CreateButton WindowCount, "ButtonDelChar_3", 242, 204, 98, 30, "Apagar", FontRegular, , , , , , , , DesignTypes.DesignRed, DesignTypes.DesignRedHover, DesignTypes.DesignRedClick, , , GetAddress(AddressOf ButtonDelChar_3)
    
End Sub

Public Sub CreateWindow_ModelFooter()

    ' Create Window
    CreateWindow "winModelFooter", "", zOrder_Win, 0, ScreenHeight - 35, ScreenWidth, 20, 0, , , , 3, 5, DesignTypes.DesignChatSmallShadow, DesignTypes.DesignChatSmallShadow, DesignTypes.DesignChatSmallShadow, , , , , , , , , False

    ' Order of Controls
    zOrder_Con = 1
    
    ' Center Window
    CentraliseWindow WindowCount, ScreenHeight - 35
    
    ' Label
    CreateButton WindowCount, "lblPremium", 0, 0, ScreenWidth, 20, "Você não possuí uma conta premium, adquirir agora mesmo!", FontRegular, White, , True, , , , , DesignBackgroundOval, , , , , , , , , , BrightGreen, Green, "Adquirir Premium"
    
End Sub

Private Sub Chars_DrawFace()
    Dim xO As Long, yO As Long, imageFace As Long, imageChar As Long, X As Long, i As Long

    xO = Windows(GetWindowIndex("winModels")).Window.Left
    yO = Windows(GetWindowIndex("winModels")).Window.Top

    X = xO + 24
    For i = 1 To MAX_CHARS
        If LenB(Trim$(CharName(i))) > 0 Then
            ' If CharSprite(i) > 0 Then
            If CharClass(i) > 0 Then
                '  If Not CharClass(i) > Count_Face Then
                imageFace = Tex_Face(CharClass(i))
                'imageChar = Tex_Char(CharSprite(i))
                RenderTexture imageFace, X, yO + 71, 0, 0, 94, 94, 94, 94
                'RenderTexture imageChar, X - 1, yO + 127, 32, 0, 32, 32, 32, 32
                ' End If
            End If
        End If

        X = X + 110
    Next
End Sub

Private Sub ButtonAcceptChar_1()
    If CharEnabled(1) Then SendUseChar 0
End Sub

Private Sub ButtonAcceptChar_2()
    If CharEnabled(2) Then SendUseChar 1
End Sub

Private Sub ButtonAcceptChar_3()
    If CharEnabled(3) Then SendUseChar 2
End Sub

Private Sub ButtonDelChar_1()
    If CharEnabled(1) Then
        ShowDialogue "Excluir Personagem", "A exclusao do personagem e permanente.", "Voce esta certo de excluir?", DialogueTypeDeleteChar, DialogueStyleYesNo, 0
    End If
End Sub

Private Sub ButtonDelChar_2()
    If CharEnabled(1) Then
        ShowDialogue "Excluir Personagem", "A exclusao do personagem e permanente.", "Voce esta certo de excluir?", DialogueTypeDeleteChar, DialogueStyleYesNo, 1
    End If
End Sub

Private Sub ButtonDelChar_3()
    If CharEnabled(3) Then
        ShowDialogue "Excluir Personagem", "A exclusao do personagem e permanente.", "Voce esta certo de excluir?", DialogueTypeDeleteChar, DialogueStyleYesNo, 2
    End If
End Sub

Private Sub ButtonCreateChar_1()
    CharNum = 0
    ShowClasses
End Sub

Private Sub ButtonCreateChar_2()
    CharNum = 1
    ShowClasses
End Sub

Private Sub ButtonCreateChar_3()
    CharNum = 2
    ShowClasses
End Sub

Private Sub ButtonModels_Close()
    Dim i As Long

    For i = 1 To MAX_CHARS
        CharPendingExclusion(i) = False
        CharHour(i) = 0
        CharMinutes(i) = 0
        CharSeconds(i) = 0
        CharDate(i) = 0
        CharEnabled(i) = True
    Next

    DestroyTCP
    HideWindows

    ShowWindow GetWindowIndex("winLogin")
    ShowWindow GetWindowIndex("winLoginFooter")
End Sub

Public Sub Resize_CharactersUI()
    Dim WindowIndex As Long
    Dim ControlIndex As Long

    CentraliseWindow GetWindowIndex("winModelFooter"), ScreenHeight - 35
    
    WindowIndex = GetWindowIndex("winModelFooter")
    ControlIndex = GetControlIndex("winModelFooter", "lblPremium")
      
    ' Characters Footer Resize Width
    Windows(WindowIndex).Window.Left = 0
    Windows(WindowIndex).Controls(ControlIndex).Width = ScreenWidth
    Windows(WindowIndex).Controls(ControlIndex).Left = 0
    Windows(WindowIndex).Controls(ControlIndex).Top = 0
    Windows(WindowIndex).Controls(ControlIndex).Align = Alignment.AlignCenter
End Sub
