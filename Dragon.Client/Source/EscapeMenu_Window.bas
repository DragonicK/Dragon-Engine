Attribute VB_Name = "EscapeMenu_Window"
Option Explicit

Private Const Width As Long = 250
Private Const Height As Long = 210
Private Const ButtonWidth As Long = 240
Private Const ButtonHeight As Long = 30
Private Const paddingTop As Long = 5
Private Const CenterLeft As Long = (Width / 2) - (ButtonWidth / 2)

Private WindowIndex As Long

Public Sub CreateWindow_EscMenu()

    ' Create window
    CreateWindow "winEscMenu", "", zOrder_Win, 0, 0, Width, Height, 0, , , , , , DesignTypes.DesignWindowWithoutBackground, DesignTypes.DesignWindowWithoutBackground, DesignTypes.DesignWindowWithoutBackground, , , , , , , , , False, True
    
    ' Centralise it
    CentraliseWindow WindowCount, 0

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Button Return
    CreateButton WindowCount, "btnReturn", CenterLeft, 25 + paddingTop + (0 * 34), ButtonWidth, ButtonHeight, "Retornar ao jogo", FontRegular, , , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf ButtonReturn)
    
    ' Button Options
    CreateButton WindowCount, "btnOptions", CenterLeft, 25 + paddingTop + (1 * 34), ButtonWidth, ButtonHeight, "Opções", FontRegular, , , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf ButtonOptions)
    
    ' Button Main Menu
    CreateButton WindowCount, "btnMainMenu", CenterLeft, 25 + paddingTop + (2 * 34), ButtonWidth, ButtonHeight, "Voltar ao menu principal", FontRegular, , , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf ButtonMainMenu)
    
    ' Button Exit
    CreateButton WindowCount, "btnExit", CenterLeft, 25 + paddingTop + (4 * 34), ButtonWidth, ButtonHeight, "Sair do jogo", FontRegular, , , , , , , , DesignTypes.DesignRed, DesignTypes.DesignRedHover, DesignTypes.DesignRedClick, , , GetAddress(AddressOf ButtonExit)

    WindowIndex = WindowCount
End Sub

Private Sub ButtonReturn()
    HideWindow GetWindowIndex("winBlank")
    HideWindow WindowIndex
End Sub

Private Sub ButtonOptions()
    HideWindow WindowIndex
    ShowWindow GetWindowIndex("winOptions"), True, True
End Sub

Private Sub ButtonMainMenu()
    HideWindows
    ShowWindow GetWindowIndex("winLogin")
    ShowWindow GetWindowIndex("winLoginFooter")
   
    Stop_Music
    ' play the menu music
    If Len(Trim$(MenuMusic)) > 0 Then Play_Music Trim$(MenuMusic)
    
    LogoutGame
End Sub

Private Sub ButtonExit()
    HideWindow GetWindowIndex("winBlank")
    HideWindow WindowIndex
    
    DestroyGame
End Sub
