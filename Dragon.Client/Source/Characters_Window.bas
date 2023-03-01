Attribute VB_Name = "Characters_Window"
Option Explicit

Public Sub CreateWindow_Models()
' Create the window
    CreateWindow "winModels", "PERSONAGENS", zOrder_Win, 0, 0, 364, 250, 0, False, Fonts.OpenSans_Effect, , 3, 5, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, , , , , , , , , False
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Close button
    CreateButton WindowCount, "ButtonClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf ButtonModels_Close)

    ' Names
    CreateLabel WindowCount, "lblCharName_1", 22, 47, 98, , "Slot Vazio", OpenSans_Effect, White, Alignment.AlignCenter
    CreateLabel WindowCount, "lblCharName_2", 132, 47, 98, , "Slot Vazio", OpenSans_Effect, White, Alignment.AlignCenter
    CreateLabel WindowCount, "lblCharName_3", 242, 47, 98, , "Slot Vazio", OpenSans_Effect, White, Alignment.AlignCenter
    ' Scenery Boxes
    CreatePictureBox WindowCount, "picScene_1", 23, 65, 96, 96, , , , , Tex_GUI(9), Tex_GUI(9), Tex_GUI(9)
    CreatePictureBox WindowCount, "picScene_2", 133, 65, 96, 96, , , , , Tex_GUI(10), Tex_GUI(10), Tex_GUI(10)
    CreatePictureBox WindowCount, "picScene_3", 243, 65, 96, 96, , , , , Tex_GUI(11), Tex_GUI(11), Tex_GUI(11), , , , , , , , , GetAddress(AddressOf Chars_DrawFace)

    ' Date
    CreateLabel WindowCount, "lblCharDate_1", 22, 163, 98, , "ue", OpenSans_Effect, White, Alignment.AlignCenter
    CreateLabel WindowCount, "lblCharDate_2", 132, 163, 98, , "", OpenSans_Effect, White, Alignment.AlignCenter
    CreateLabel WindowCount, "lblCharDate_3", 242, 163, 98, , "", OpenSans_Effect, White, Alignment.AlignCenter

    ' Create Buttons
    CreateButton WindowCount, "ButtonSelectChar_1", 22, 180, 98, 26, "SELECIONAR", OpenSans_Effect, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ButtonAcceptChar_1)
    CreateButton WindowCount, "ButtonCreateChar_1", 22, 170, 98, 26, "CRIAR", OpenSans_Effect, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ButtonCreateChar_1)
    CreateButton WindowCount, "ButtonDelChar_1", 22, 208, 98, 26, "DELETAR", OpenSans_Effect, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ButtonDelChar_1)

    CreateButton WindowCount, "ButtonSelectChar_2", 132, 180, 98, 26, "SELECIONAR", OpenSans_Effect, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ButtonAcceptChar_2)
    CreateButton WindowCount, "ButtonCreateChar_2", 132, 170, 98, 26, "CRIAR", OpenSans_Effect, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ButtonCreateChar_2)
    CreateButton WindowCount, "ButtonDelChar_2", 132, 208, 98, 26, "DELETAR", OpenSans_Effect, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ButtonDelChar_2)

    CreateButton WindowCount, "ButtonSelectChar_3", 242, 180, 98, 26, "SELECIONAR", OpenSans_Effect, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ButtonAcceptChar_3)
    CreateButton WindowCount, "ButtonCreateChar_3", 242, 170, 98, 26, "CRIAR", OpenSans_Effect, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ButtonCreateChar_3)
    CreateButton WindowCount, "ButtonDelChar_3", 242, 208, 98, 26, "DELETAR", OpenSans_Effect, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ButtonDelChar_3)
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
                RenderTexture imageFace, X, yO + 66, 0, 0, 94, 94, 94, 94
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
    ShowWindow GetWindowIndex("winLoginLogo")
End Sub
