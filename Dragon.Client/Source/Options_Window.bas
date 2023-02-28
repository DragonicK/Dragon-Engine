Attribute VB_Name = "Options_Window"
Option Explicit

Private WindowIndex As Long

Public Sub CreateWindow_Options()
' Create window
    CreateWindow "winOptions", "", zOrder_Win, 0, 0, 210, 212, 0, , , , , , DesignTypes.DesignWindowWithoutTopBar, DesignTypes.DesignWindowWithoutTopBar, DesignTypes.DesignWindowWithoutTopBar, , , , , , , , , False, False
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' General
    CreatePictureBox WindowCount, "picBlank", 35, 25, 140, 9, , , , , , , , DesignTypes.DesignBackgroundOval, DesignTypes.DesignBackgroundOval, DesignTypes.DesignBackgroundOval
    CreateLabel WindowCount, "lblBlank", 35, 22, 140, , "Opcoes Gerais", OpenSans_Regular, White, Alignment.AlignCenter
    ' Check boxes
    CreateCheckbox WindowCount, "chkMusic", 35, 40, 80, , , "Musica", OpenSans_Regular, , , , , DesignTypes.DesignCheckBox
    CreateCheckbox WindowCount, "chkSound", 115, 40, 80, , , "Som", OpenSans_Regular, , , , , DesignTypes.DesignCheckBox

    CreateCheckbox WindowCount, "chkFullscreen", 115, 60, 80, , , "Tela Cheia", OpenSans_Regular, , , , , DesignTypes.DesignCheckBox
    ' Resolution
    CreatePictureBox WindowCount, "picBlank", 35, 85, 140, 9, , , , , , , , DesignTypes.DesignBackgroundOval, DesignTypes.DesignBackgroundOval, DesignTypes.DesignBackgroundOval
    CreateLabel WindowCount, "lblBlank", 35, 82, 140, , "Selecionar Resolucao", OpenSans_Regular, White, Alignment.AlignCenter
    ' combobox
    CreateComboBox WindowCount, "cmbRes", 30, 100, 150, 18, DesignTypes.DesignComboNormal, OpenSans_Regular
    ' Renderer
    CreatePictureBox WindowCount, "picBlank", 35, 125, 140, 9, , , , , , , , DesignTypes.DesignBackgroundOval, DesignTypes.DesignBackgroundOval, DesignTypes.DesignBackgroundOval
    CreateLabel WindowCount, "lblBlank", 35, 122, 140, , "Modo de DirectX", OpenSans_Regular, White, Alignment.AlignCenter
    ' Check boxes
    CreateComboBox WindowCount, "cmbRender", 30, 140, 150, 18, DesignTypes.DesignComboNormal, OpenSans_Regular
    ' Button
    CreateButton WindowCount, "btnConfirm", 65, 168, 80, 22, "CONFIRMAR", OpenSans_Effect, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ButtonConfirm)
    
    WindowIndex = WindowCount

    ' Populate the options screen
    SetOptionsScreen
End Sub

Private Sub SetOptionsScreen()
    ' clear the combolists
    Erase Windows(WindowIndex).Controls(GetControlIndex("winOptions", "cmbRes")).list
    ReDim Windows(WindowIndex).Controls(GetControlIndex("winOptions", "cmbRes")).list(0)
    Erase Windows(WindowIndex).Controls(GetControlIndex("winOptions", "cmbRender")).list
    ReDim Windows(WindowIndex).Controls(GetControlIndex("winOptions", "cmbRender")).list(0)

    ' Resolutions
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRes"), "1920x1080"
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRes"), "1680x1050"
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRes"), "1600x900"
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRes"), "1440x900"
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRes"), "1440x1050"
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRes"), "1366x768"
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRes"), "1360x1024"
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRes"), "1360x768"
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRes"), "1280x1024"
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRes"), "1280x800"
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRes"), "1280x768"
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRes"), "1280x720"
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRes"), "1024x768"
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRes"), "1024x576"
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRes"), "800x600"
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRes"), "800x450"

    ' Render Options
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRender"), "Automatic"
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRender"), "Hardware"
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRender"), "Mixed"
    Combobox_AddItem WindowIndex, GetControlIndex("winOptions", "cmbRender"), "Software"

    ' fill the options screen
    With Windows(WindowIndex)
        .Controls(GetControlIndex("winOptions", "chkMusic")).Value = Options.Music
        .Controls(GetControlIndex("winOptions", "chkSound")).Value = Options.Sound
        .Controls(GetControlIndex("winOptions", "chkFullscreen")).Value = Options.Fullscreen
        .Controls(GetControlIndex("winOptions", "cmbRes")).Value = Options.Resolution
        .Controls(GetControlIndex("winOptions", "cmbRender")).Value = Options.Render + 1
    End With
End Sub

Private Sub ButtonClose()
    HideWindow WindowIndex
    ShowWindow GetWindowIndex("winEscMenu")
End Sub

Private Sub ButtonConfirm()
    Dim i As Long, Value As Long, Width As Long, Height As Long, Message As Boolean, MusicFile As String

    ' music
    Value = Windows(WindowIndex).Controls(GetControlIndex("winOptions", "chkMusic")).Value
    If Options.Music <> Value Then
        Options.Music = Value
        ' let them know
        If Value = 0 Then
            AddText "Musica desativada.", BrightGreen
            Stop_Music
        Else
            AddText "Musica ativada.", BrightGreen
            ' play music
            If InGame Then
                MusicFile = Trim$(CurrentMap.MapData.Music)
            Else
                MusicFile = Trim$(MenuMusic)
            End If

            If Not MusicFile = "None." Then
                Play_Music MusicFile
            Else
                Stop_Music
            End If
        End If
    End If

    ' sound
    Value = Windows(WindowIndex).Controls(GetControlIndex("winOptions", "chkSound")).Value
    If Options.Sound <> Value Then
        Options.Sound = Value
        ' let them know
        If Value = 0 Then
            AddText "Som desativado.", BrightGreen
        Else
            AddText "Som ativado.", BrightGreen
        End If
    End If

    ' fullscreen
    Value = Windows(WindowIndex).Controls(GetControlIndex("winOptions", "chkFullscreen")).Value
    If Options.Fullscreen <> Value Then
        Options.Fullscreen = Value
        Message = True
    End If

    ' resolution
    With Windows(WindowIndex).Controls(GetControlIndex("winOptions", "cmbRes"))
        If .Value > 0 And .Value <= RES_COUNT Then
            If Options.Resolution <> .Value Then
                Options.Resolution = .Value
                If Not isFullscreen Then
                    SetResolution
                Else
                    Message = True
                End If
            End If
        End If
    End With

    ' render
    With Windows(WindowIndex).Controls(GetControlIndex("winOptions", "cmbRender"))
        If .Value > 0 And .Value <= 3 Then
            If Options.Render <> .Value - 1 Then
                Options.Render = .Value - 1
                Message = True
            End If
        End If
    End With

    ' save options
    Call SaveOptions

    ' let them know
    If InGame Then
        If Message Then AddText "Algumas modificações somente terão efeito na próxima vez que você carregar o jogo.", BrightGreen
    End If

    ' close
    Call ButtonClose
End Sub
