Attribute VB_Name = "Login_Window"
Public LoginToken As String

Private Const LogoWidth As Long = 512
Private Const LogoHeight As Long = 142

Private Const WindowWidth As Long = 512
Private Const WindowHeight As Long = 400
Private Const WindowPaddingTop As Long = 10
Private Const WindowPaddingLogo As Long = WindowPaddingTop + (LogoHeight + 80)

Private Const CenterLogo As Long = (WindowWidth / 2) - ((LogoWidth + 40) / 2)

Private Const WidgetWidth As Long = WindowWidth - (WindowWidth / 3)
Private Const WidgetCenter As Long = (WindowWidth / 2) - (WidgetWidth / 2)

Private Const ButtonWidth As Long = 170
Private Const ButtonCenter As Long = (WindowWidth / 2) - (ButtonWidth / 2)

Public Sub CreateWindow_Login()
    
    CreateWindow "winLogin", "", zOrder_Win, 0, 0, WindowWidth, WindowHeight, 0, , Fonts.FontRegular, , 3, 5, DesignTypes.DesignWindowWithoutBackground, DesignTypes.DesignWindowWithoutBackground, DesignTypes.DesignWindowWithoutBackground, , , , , , , , , False
    
    ' Center Window
    CentraliseWindow WindowCount

    ' Order of Controls
    zOrder_Con = 1
    
    ' Create Picture Logo
    CreatePictureBox WindowCount, "picLoginLogo", CenterLogo, 0, LogoWidth, LogoHeight, , , , , Tex_GUI(73), Tex_GUI(73), Tex_GUI(73)
    
    ' Label Username
    CreateLabel WindowCount, "lblUsername", WidgetCenter, WindowPaddingLogo, WidgetWidth, , "Usuário", FontRegular, White, Alignment.AlignCenter
    
    ' TextBox Username
    CreateTextbox WindowCount, "txtUser", WidgetCenter, WindowPaddingLogo + (WindowPaddingTop * 2), WidgetWidth, 30, Options.Username, Fonts.FontRegular, , Alignment.AlignCenter, , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , , , , , 0, 8
    
    ' Label Password
    CreateLabel WindowCount, "lblPassword", WidgetCenter, WindowPaddingLogo + (WindowPaddingTop * 6), WidgetWidth, , "Senha", FontRegular, White, Alignment.AlignCenter
    
    ' TextBox Password
    CreateTextbox WindowCount, "txtPass", WidgetCenter, WindowPaddingLogo + (WindowPaddingTop * 8), WidgetWidth, 30, vbNullString, Fonts.FontRegular, , Alignment.AlignCenter, , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , , , , , 0, 8, True, GetAddress(AddressOf btnLogin_Click)

    ' Login Buttom
    CreateButton WindowCount, "btnAccept", ButtonCenter, WindowPaddingLogo + (WindowPaddingTop * 14), ButtonWidth, 38, "Acessar", FontRegular, White, , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf btnLogin_Click)
    
    ' Checkbox
    'CreateCheckbox WindowCount, "chkSaveUser", 82, 129, 142, , Options.SaveUser, "Salvar Nome?", FontRegular, , , , , DesignTypes.DesignCheckBox, , , GetAddress(AddressOf chkSaveUser_Click)

    ' Set the active control
    If Not Len(Windows(GetWindowIndex("winLogin")).Controls(GetControlIndex("winLogin", "txtUser")).Text) > 0 Then
        SetActiveControl GetWindowIndex("winLogin"), GetControlIndex("winLogin", "txtUser")
    Else
        SetActiveControl GetWindowIndex("winLogin"), GetControlIndex("winLogin", "txtPass")
    End If
    
End Sub

Public Sub CreateWindow_LoginFooter()

    ' Create Window
    CreateWindow "winLoginFooter", "", zOrder_Win, 0, ScreenHeight - 20, ScreenWidth, 20, 0, , , , 3, 5, DesignTypes.DesignChatSmallShadow, DesignTypes.DesignChatSmallShadow, DesignTypes.DesignChatSmallShadow, , , , , , , , , False

    ' Order of Controls
    zOrder_Con = 1
    
    ' Center Window
    CentraliseWindow WindowCount, ScreenHeight - 35
    
    ' Label
    CreateLabel WindowCount, "lblCopy", 0, 0, ScreenWidth, 0, "Copyright 2022 - 2023 Julio Sperandio. Todos os direitos reservados.", FontRegular, White, Alignment.AlignCenter
    
End Sub

Private Sub btnLogin_Click()
    Dim user As String, pass As String

    With Windows(GetWindowIndex("winLogin"))
        user = .Controls(GetControlIndex("winLogin", "txtUser")).Text
        pass = .Controls(GetControlIndex("winLogin", "txtPass")).Text
    End With

    Login user, pass
End Sub

Private Sub chkSaveUser_Click()

    With Windows(GetWindowIndex("winLogin")).Controls(GetControlIndex("winLogin", "chkSaveUser"))
        If .Value = 0 Then    ' set as false
            Options.SaveUser = 0
            Options.Username = vbNullString
            SaveOptions
        Else
            Options.SaveUser = 1
            SaveOptions
        End If
    End With

End Sub

Public Sub Resize_WinLoginFooter()
    Dim WindowIndex As Long
    Dim ControlIndex As Long
    
    ' Get The Window
    WindowIndex = GetWindowIndex("winLoginFooter")
    
    ' Get The Widget
    ControlIndex = GetControlIndex("winLoginFooter", "lblCopy")
    
    ' Centralise Window
    CentraliseWindow WindowIndex, ScreenHeight - 35
      
    ' Reposition Window
    Windows(WindowIndex).Window.Left = 0
    
    ' Resize Window
    Windows(WindowIndex).Window.Width = ScreenWidth
    
    ' Resize Label Widget
    Windows(WindowIndex).Controls(ControlIndex).Width = ScreenWidth
    
    ' Reposition Window
    Windows(WindowIndex).Controls(ControlIndex).Left = 0
    Windows(WindowIndex).Controls(ControlIndex).Top = 0
    
    ' Realignment Label Widget
    Windows(WindowIndex).Controls(ControlIndex).Align = Alignment.AlignCenter
End Sub

Public Sub Login(Name As String, Password As String)
    TcpInit AUTH_SERVER_IP, AUTH_SERVER_PORT

    If ConnectToServer Then
    HideWindows
        Call SetStatus("Enviando informações de login.")
        Call SendAuthLogin(Name, Password)
        ' save details
        If Options.SaveUser Then Options.Username = Name Else Options.Username = vbNullString
        SaveOptions
        Exit Sub
    Else
        ShowWindow GetWindowIndex("winLogin")
        ShowWindow GetWindowIndex("winLoginFooter")
        ShowDialogue "Problema de Conexao", "Não pode conectar-se ao servidor de login.", "Tente novamente mais tarde.", DialogueTypeAlert
    End If

End Sub

Public Sub AttemptLogin()
    TcpInit GameServerIp, GameServerPort

    ' send login packet
    If ConnectToServer Then
        SendLogin Windows(GetWindowIndex("winLogin")).Controls(GetControlIndex("winLogin", "txtUser")).Text
        Exit Sub
    End If

    If Not IsConnected Then
        ShowWindow GetWindowIndex("winLogin")
        ShowWindow GetWindowIndex("winLoginFooter")
        ShowDialogue "Problema de Conexao", "Não pode conectar-se ao game server.", "Tente novamente depois.", DialogueTypeAlert
    End If
End Sub

Public Function IsLoginLegal(ByVal Username As String, ByVal Password As String) As Boolean

    If LenB(Trim$(Username)) >= 3 Then
        If LenB(Trim$(Password)) >= 3 Then
            IsLoginLegal = True
        End If
    End If

End Function

