Attribute VB_Name = "Dialogue_Window"
Option Explicit

Public Sub CreateWindow_Dialogue()
    ' Create black background
    CreateWindow "winBlank", "", zOrder_Win, 0, 0, 800, 600, 0, , Fonts.OpenSans_Effect, , , , DesignTypes.desWin_AincradNoBar, DesignTypes.desWin_AincradNoBar, DesignTypes.desWin_AincradNoBar, , , , , , , , , False, False
    ' Create dialogue window
    CreateWindow "winDialogue", "MENSAGEM", zOrder_Win, 0, 0, 348, 145, 0, , Fonts.OpenSans_Effect, , 3, 5, DesignTypes.desWin_AincradNorm, DesignTypes.desWin_AincradNorm, DesignTypes.desWin_AincradNorm, , , , , , , , , , False
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Close button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf ButtonDialogue_Close)
    ' Parchment
    CreateLabel WindowCount, "lblHeader", 103, 51, 144, , "Header", OpenSans_Effect, White, Alignment.alignCentre
    ' Labels
    CreateLabel WindowCount, "lblBody_1", 15, 70, 314, , "Invalid username or password.", OpenSans_Regular, , Alignment.alignCentre
    CreateLabel WindowCount, "lblBody_2", 15, 85, 314, , "Please try again.", OpenSans_Regular, , Alignment.alignCentre
    ' Buttons
    CreateButton WindowCount, "btnYes", 104, 108, 68, 26, "SIM", OpenSans_Regular, , , False, , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf Dialogue_Yes)
    CreateButton WindowCount, "btnNo", 180, 108, 68, 26, "NÃO", OpenSans_Regular, , , False, , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf Dialogue_No)
    CreateButton WindowCount, "btnOkay", 140, 108, 68, 26, "OK", OpenSans_Regular, , , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf Dialogue_Okay)
    ' Input
    CreateTextbox WindowCount, "txtInput", 93, 85, 162, 18, , OpenSans_Regular, White, Alignment.alignCentre, , , , , , DesignTypes.desTextBlack, DesignTypes.desTextBlack, DesignTypes.desTextBlack, , , , , , , 4, 2
    ' set active control
    SetActiveControl WindowCount, GetControlIndex("winDialogue", "txtInput")
End Sub

Private Sub ButtonDialogue_Close()
    If Dialogue.Style = DialogueStyle.DialogueStyleOkay Then
        Call DialogueHandler(DialogueButtonOkay)
    ElseIf Dialogue.Style = DialogueStyle.DialogueStyleYesNo Then
        Call DialogueHandler(DialogueButtonNo)
    End If

    CloseDialogue
End Sub

Private Sub Dialogue_Okay()
   Call DialogueHandler(DialogueButtonOkay)
End Sub

Private Sub Dialogue_Yes()
    Call DialogueHandler(DialogueButtonYes)
End Sub

Private Sub Dialogue_No()
    Call DialogueHandler(DialogueButtonNo)
End Sub

