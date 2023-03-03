Attribute VB_Name = "Dialogue_Window"
Option Explicit

Public Sub CreateWindow_Dialogue()
    ' Create black background
    CreateWindow "winBlank", "", zOrder_Win, 0, 0, 800, 600, 0, , Fonts.FontRegular, , , , DesignTypes.DesignWindowWithoutTopBar, DesignTypes.DesignWindowWithoutTopBar, DesignTypes.DesignWindowWithoutTopBar, , , , , , , , , False, False
    ' Create dialogue window
    CreateWindow "winDialogue", "MENSAGEM", zOrder_Win, 0, 0, 348, 145, 0, , Fonts.FontRegular, , 3, 5, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, , , , , , , , , , False
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Close button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf ButtonDialogue_Close)
    ' Parchment
    CreateLabel WindowCount, "lblHeader", 103, 51, 144, , "Header", FontRegular, White, Alignment.AlignCenter
    ' Labels
    CreateLabel WindowCount, "lblBody_1", 15, 70, 314, , "Invalid username or password.", FontRegular, , Alignment.AlignCenter
    CreateLabel WindowCount, "lblBody_2", 15, 85, 314, , "Please try again.", FontRegular, , Alignment.AlignCenter
    ' Buttons
    CreateButton WindowCount, "btnYes", 104, 108, 68, 26, "SIM", FontRegular, , , False, , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf Dialogue_Yes)
    CreateButton WindowCount, "btnNo", 180, 108, 68, 26, "NÃO", FontRegular, , , False, , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf Dialogue_No)
    CreateButton WindowCount, "btnOkay", 140, 108, 68, 26, "OK", FontRegular, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf Dialogue_Okay)
    ' Input
    CreateTextbox WindowCount, "txtInput", 93, 85, 162, 18, , FontRegular, White, Alignment.AlignCenter, , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , , , , , 4, 2
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

