Attribute VB_Name = "Dialogue_Window"
Option Explicit

Public Sub CreateWindow_Dialogue()
    ' Create black background
    'CreateWindow "winBlank", "", zOrder_Win, 0, 0, 800, 600, 0, , Fonts.FontRegular, , , , DesignTypes.DesignBlank, DesignTypes.DesignBlank, DesignTypes.DesignBlank, , , , , , , , , False, False
    
    ' Create dialogue window
    CreateWindow "winDialogue", "", zOrder_Win, 0, 0, 420, 180, 0, , Fonts.FontRegular, , 3, 5, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, , , , , , , , , True, False
    
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Labels
    CreateLabel WindowCount, "lblBody_1", 20, 65, 380, , "Invalid username or password.", FontRegular, , Alignment.AlignCenter
    CreateLabel WindowCount, "lblBody_2", 20, 90, 380, , "Please try again.", FontRegular, , Alignment.AlignCenter
    
    ' Buttons
    CreateButton WindowCount, "btnNo", 20, 130, 188, 30, "Não", FontRegular, , , False, , , , , DesignTypes.DesignRed, DesignTypes.DesignRedHover, DesignTypes.DesignRedClick, , , GetAddress(AddressOf Dialogue_No)
    CreateButton WindowCount, "btnYes", 212, 130, 188, 30, "Sim", FontRegular, , , False, , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf Dialogue_Yes)
    
    ' Button OK
    CreateButton WindowCount, "btnOkay", 20, 130, 380, 30, "Confirmar", FontRegular, , , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf Dialogue_Okay)
    
    ' Input
    CreateTextbox WindowCount, "txtInput", 20, 85, 380, 30, , FontRegular, White, Alignment.AlignCenter, , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , , , , , 0, 8
    
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

