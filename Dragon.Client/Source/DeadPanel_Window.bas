Attribute VB_Name = "DeadPanel_Window"
Option Explicit

Private DeadPanelTime As Long
Private DeadPanelTick As Long
Private DeadPanelVisible As Boolean

Private WindowIndex As Long

Public Enum RessurrectionConfirmation
    RessurrectionConfirmation_Accept
    RessurrectionConfirmation_Decline
End Enum

Public Enum RessurrectionType
    RessurrectionType_Item
    RessurrectionType_BindPoint
End Enum

Public Enum DeadPanelOperationType
    DeadPanelOperationType_Open
    DeadPanelOperationType_Close
    DeadPanelOperationType_RessurrectionByPlayer
    DeadPanelOperationType_Decline
End Enum

Public Sub SetDeadPanelTick(ByVal Tick As Long)
    DeadPanelTick = Tick
End Sub

Public Sub SetDeadPanelTime(ByVal Time As Long)
    DeadPanelTime = Time
End Sub

Public Function IsDeadPanelVisible() As Boolean
    IsDeadPanelVisible = Windows(WindowIndex).Window.Visible
    DeadPanelVisible = IsDeadPanelVisible
End Function

Public Sub ShowDeadPanel()
    Windows(WindowIndex).Window.Visible = True
    DeadPanelVisible = True
End Sub

Public Sub HideDeadPanel()
    Windows(WindowIndex).Window.Visible = False
    DeadPanelVisible = False
End Sub

Public Sub CreateWindow_DeadPanel()
    Dim Width As Long, Heigth As Long, X As Long, Y As Long
    Call GetResolutionSize(Options.Resolution, Width, Heigth)

    X = (Width / 2) - 119
    Y = (Heigth / 2) - 300

    CreateWindow "winDeadPanel", "", zOrder_Win, X, Y, 239, 150, 0, True, Fonts.FontRegular, , 2, 7, DesignTypes.DesignWindowWithoutTopBar, DesignTypes.DesignWindowWithoutTopBar, DesignTypes.DesignWindowWithoutTopBar, , , , , , , , , False, , GetAddress(AddressOf Target_OnDraw)

    zOrder_Con = 1

    CreateLabel WindowCount, "lblTime", 0, 15, 239, 25, "Tempo: 360 segundo(s)", FontRegular, White, Alignment.AlignCenter
    CreateLabel WindowCount, "lblMsg", 20, 40, 200, , "Voce morreu. Escolha o modo de ressureição.", FontRegular, White, Alignment.AlignCenter

    CreateButton WindowCount, "btnYes", 35, 85, 75, 24, "SIM", FontRegular, , , False, , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf AcceptRessurect)
    CreateButton WindowCount, "btnNo", 125, 85, 75, 24, "NÃO", FontRegular, , , False, , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf DeclineRessurect)

    CreateButton WindowCount, "btnRessurrect", 20, 75, 195, 24, "REVIVER AQUI", FontRegular, , , True, , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf RessurectHere)
    CreateButton WindowCount, "btnBindPoint", 20, 105, 195, 24, "ÚLTIMO PONTO SALVO", FontRegular, , , True, , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf RessurectBindPoint)

    Windows(WindowCount).Window.Top = 100
    WindowIndex = WindowCount
End Sub

Public Sub OpenDeadPanel(ByVal YouAreDead As Boolean, ByVal CharacterName As String)

    Dim ControlRessurrection As Integer, ControlBind As Integer
    Dim ControlMsg As Integer, ControlTime As Integer
    Dim ControlYes As Integer, ControlNo As Integer

    ControlYes = GetControlIndex("winDeadPanel", "btnYes")
    ControlNo = GetControlIndex("winDeadPanel", "btnNo")

    ControlRessurrection = GetControlIndex("winDeadPanel", "btnRessurrect")
    ControlBind = GetControlIndex("winDeadPanel", "btnBindPoint")

    ControlTime = GetControlIndex("winDeadPanel", "lblTime")
    ControlMsg = GetControlIndex("winDeadPanel", "lblMsg")

    Windows(WindowIndex).Controls(ControlTime).Text = "Tempo: " & DeadPanelTime & " segundo(s)"

    If YouAreDead Then
        Windows(WindowIndex).Controls(ControlMsg).Text = "Voce morreu, escolha o modo de ressureicao."
    Else
        Windows(WindowIndex).Controls(ControlMsg).Text = "Voce foi revivido por " & CharacterName & ". Aceitar?"
    End If

    Windows(WindowIndex).Controls(ControlRessurrection).Visible = YouAreDead
    Windows(WindowIndex).Controls(ControlBind).Visible = YouAreDead

    Windows(WindowIndex).Controls(ControlYes).Visible = Not YouAreDead
    Windows(WindowIndex).Controls(ControlNo).Visible = Not YouAreDead

    Call ShowDeadPanel
    Call CloseAllWindows
End Sub

Private Sub CloseAllWindows()
    HideWindow GetWindowIndex("winCharacter")
    HideWindow GetWindowIndex("winStatus")
    HideWindow GetWindowIndex("winInventory")
    HideWindow GetWindowIndex("winItemUpgrade")
    HideWindow GetWindowIndex("winTitle")
    HideWindow GetWindowIndex("winAchievement")
    HideWindow GetWindowIndex("winSkills")
    HideWindow GetWindowIndex("winWarehouse")
    HideWindow GetWindowIndex("winHeraldry")
    HideWindow GetWindowIndex("winServices")

    SendCloseWarehouse

    CanSwapInvItems = True
End Sub

Public Sub UpdateDeadPanel()
    Dim ControlTime As Integer

    If DeadPanelVisible Then
        ControlTime = GetControlIndex("winDeadPanel", "lblTime")
        Windows(WindowIndex).Controls(ControlTime).Text = "Tempo: " & DeadPanelTime & " segundo(s)"

        If DeadPanelTime <= 0 Then
            Call HideDeadPanel
            Call RessurectBindPoint
        End If

        If GetTickCount >= DeadPanelTick + 1000 Then
            DeadPanelTick = GetTickCount
            DeadPanelTime = DeadPanelTime - 1
        End If
    End If

End Sub

Private Sub RessurectHere()
    If Player(MyIndex).Dead Then
        Call SelfRessurect(RessurrectionType.RessurrectionType_Item)
    End If
End Sub

Private Sub RessurectBindPoint()
    If Player(MyIndex).Dead Then
        Call SelfRessurect(RessurrectionType.RessurrectionType_BindPoint)
    End If
End Sub

Private Sub AcceptRessurect()
    If Player(MyIndex).Dead Then
        Call PlayerRessurrect(RessurrectionConfirmation.RessurrectionConfirmation_Accept)
    End If
End Sub

Private Sub DeclineRessurect()
    If Player(MyIndex).Dead Then
        Call PlayerRessurrect(RessurrectionConfirmation.RessurrectionConfirmation_Decline)
    End If
End Sub
