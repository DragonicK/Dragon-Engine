Attribute VB_Name = "EscapeMenu_Window"
Option Explicit

Private WindowIndex As Long

Public Sub CreateWindow_EscMenu()
    ' Create window
    CreateWindow "winEscMenu", "", zOrder_Win, 0, 0, 210, 156, 0, , , , , , DesignTypes.desWin_NoBar, DesignTypes.desWin_NoBar, DesignTypes.desWin_NoBar, , , , , , , , , False, False
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Parchment
    CreatePictureBox WindowCount, "picParchment", 6, 6, 198, 144, , , , , , , , DesignTypes.desParchment, DesignTypes.desParchment, DesignTypes.desParchment
    ' Buttons
    CreateButton WindowCount, "btnReturn", 16, 16, 178, 28, "Retornar para o jogo (Esc)", OpenSans_Regular, , , , , , , , DesignTypes.desEmerald, DesignTypes.desEmerald_Hover, DesignTypes.desEmerald_Click, , , GetAddress(AddressOf ButtonReturn)
    CreateButton WindowCount, "btnOptions", 16, 48, 178, 28, "Opções", OpenSans_Regular, , , , , , , , DesignTypes.desOrange, DesignTypes.desOrange_Hover, DesignTypes.desOrange_Click, , , GetAddress(AddressOf ButtonOptions)
    CreateButton WindowCount, "btnMainMenu", 16, 80, 178, 28, "Voltar ao menu principal", OpenSans_Regular, , , , , , , , DesignTypes.desBlue, DesignTypes.desSteel_Hover, DesignTypes.desBlue_Click, , , GetAddress(AddressOf ButtonMainMenu)
    CreateButton WindowCount, "btnExit", 16, 112, 178, 28, "Sair do jogo", OpenSans_Regular, , , , , , , , DesignTypes.desRed, DesignTypes.desRed_Hover, DesignTypes.desRed_Click, , , GetAddress(AddressOf ButtonExit)

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
