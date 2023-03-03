Attribute VB_Name = "EscapeMenu_Window"
Option Explicit

Private WindowIndex As Long

Public Sub CreateWindow_EscMenu()
    ' Create window
    CreateWindow "winEscMenu", "", zOrder_Win, 0, 0, 210, 156, 0, , , , , , DesignTypes.DesignWindowWithoutTopBar, DesignTypes.DesignWindowWithoutTopBar, DesignTypes.DesignWindowWithoutTopBar, , , , , , , , , False, False
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Buttons
    CreateButton WindowCount, "btnReturn", 16, 16, 178, 28, "Retornar para o jogo (Esc)", FontRegular, , , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf ButtonReturn)
    CreateButton WindowCount, "btnOptions", 16, 48, 178, 28, "Opções", FontRegular, , , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf ButtonOptions)
    CreateButton WindowCount, "btnMainMenu", 16, 80, 178, 28, "Voltar ao menu principal", FontRegular, , , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf ButtonMainMenu)
    CreateButton WindowCount, "btnExit", 16, 112, 178, 28, "Sair do jogo", FontRegular, , , , , , , , DesignTypes.DesignRed, DesignTypes.DesignRedHover, DesignTypes.DesignRedClick, , , GetAddress(AddressOf ButtonExit)

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
    ShowWindow GetWindowIndex("winLoginLogo")
   
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
