Attribute VB_Name = "EscapeMenu_Window"
Option Explicit

Private Const Width As Long = 250
Private Const Height As Long = 210
Private Const ButtonWidth As Long = 240
Private Const ButtonHeight As Long = 30
Private Const PaddingTop As Long = 5
Private Const CenterLeft As Long = (Width / 2) - (ButtonWidth / 2)

Private WindowIndex As Long

Public Sub CreateWindow_EscMenu()

' Create window
    CreateWindow "winEscMenu", "", zOrder_Win, 0, 0, Width, Height, 0, , , , , , DesignTypes.DesignWindowWithoutBackground, DesignTypes.DesignWindowWithoutBackground, DesignTypes.DesignWindowWithoutBackground, , , , , , , , , False, True

    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Button Return
    CreateButton WindowCount, "btnReturn", CenterLeft, 25 + PaddingTop + (0 * 34), ButtonWidth, ButtonHeight, "Retornar ao jogo", FontRegular, , , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf ButtonReturn)

    ' Button Options
    CreateButton WindowCount, "btnOptions", CenterLeft, 25 + PaddingTop + (1 * 34), ButtonWidth, ButtonHeight, "Opções", FontRegular, , , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf ButtonOptions)

    ' Button Main Menu
    CreateButton WindowCount, "btnMainMenu", CenterLeft, 25 + PaddingTop + (2 * 34), ButtonWidth, ButtonHeight, "Voltar ao menu principal", FontRegular, , , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf ButtonMainMenu)

    ' Button Exit
    CreateButton WindowCount, "btnExit", CenterLeft, 25 + PaddingTop + (4 * 34), ButtonWidth, ButtonHeight, "Sair do jogo", FontRegular, , , , , , , , DesignTypes.DesignRed, DesignTypes.DesignRedHover, DesignTypes.DesignRedClick, , , GetAddress(AddressOf ButtonExit)

    WindowIndex = WindowCount
End Sub

Public Sub ShowEscapeMenu()
    Dim ControlIndex As Long
    Dim IsLoginVisible As Boolean
    Dim IsModelsVisible As Boolean
    Dim IsNewModelVisible As Boolean
    Dim IsClassesVisible As Boolean

    Dim IsOutsideGame As Boolean

    ControlIndex = GetControlIndex("winEscMenu", "btnReturn")

    IsLoginVisible = IsLoginWindowVisible()
    IsModelsVisible = IsModelsWindowVisible()
    IsNewModelVisible = IsNewModelWindowVisible()
    IsClassesVisible = IsClassesWindowVisible()

    IsOutsideGame = IsLoginVisible Or IsModelsVisible Or IsNewModelVisible Or IsClassesVisible

    Windows(WindowIndex).Controls(ControlIndex).Visible = Not IsOutsideGame

    ShowWindow GetWindowIndex("winBlank"), True
    ShowWindow GetWindowIndex("winEscMenu"), True
End Sub

Public Sub HideEscapeMenu()
' hide it
    HideWindow GetWindowIndex("winBlank")
    HideWindow GetWindowIndex("winEscMenu")
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

Private Function IsLoginWindowVisible() As Boolean
    Dim CurrentWindowIndex As Long

    CurrentWindowIndex = GetWindowIndex("winLogin")
    IsLoginWindowVisible = Windows(CurrentWindowIndex).Window.Visible

End Function

Private Function IsModelsWindowVisible() As Boolean
    Dim CurrentWindowIndex As Long

    CurrentWindowIndex = GetWindowIndex("winModels")
    IsModelsWindowVisible = Windows(CurrentWindowIndex).Window.Visible
End Function

Private Function IsNewModelWindowVisible() As Boolean
    Dim CurrentWindowIndex As Long

    CurrentWindowIndex = GetWindowIndex("winNewModel")
    IsNewModelWindowVisible = Windows(CurrentWindowIndex).Window.Visible
End Function

Private Function IsClassesWindowVisible() As Boolean
    Dim CurrentWindowIndex As Long

    CurrentWindowIndex = GetWindowIndex("winClasses")
    IsClassesWindowVisible = Windows(CurrentWindowIndex).Window.Visible
End Function
