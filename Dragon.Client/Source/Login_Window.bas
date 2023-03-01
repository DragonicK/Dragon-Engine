Attribute VB_Name = "Login_Window"
Public LoginToken As String

Private Const Width As Long = 320
Private Const Height As Long = 200
Private Const PaddingTop As Long = 10

Private Const LogoSizeY As Long = 142
Private Const LogoSizeX As Long = 512
Private Const CenterLeft As Long = ((LogoSizeX + 40) / 2)

Public Sub CreateWindow_Login()
    
    CreateWindow "winLogin", "", zOrder_Win, (ScreenWidth / 2) - (Width / 2), 400, Width, Height, 0, , Fonts.OpenSans_Effect, , 3, 5, DesignTypes.DesignWindowWithoutBackground, DesignTypes.DesignWindowWithoutBackground, DesignTypes.DesignWindowWithoutBackground, , , , , , , , , False
    
    ' Center Window
    'CentraliseWindow WindowCount

    ' Order of Controls
    zOrder_Con = 1
    
    ' Label Username
    CreateLabel WindowCount, "lblUsername", 0, PaddingTop, Width, , "Usuário", OpenSans_Regular, White, Alignment.AlignCenter
    
    ' TextBox Username
    CreateTextbox WindowCount, "txtUser", 0, PaddingTop + (PaddingTop * 2), Width, 30, Options.Username, Fonts.OpenSans_Regular, , Alignment.AlignCenter, , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , , , , , 0, 8
    
    ' Label Password
    CreateLabel WindowCount, "lblPassword", 0, PaddingTop + (PaddingTop * 6), Width, , "Senha", OpenSans_Regular, White, Alignment.AlignCenter
    
    ' TextBox Password
    CreateTextbox WindowCount, "txtPass", 0, PaddingTop + (PaddingTop * 8), Width, 30, vbNullString, Fonts.OpenSans_Regular, , Alignment.AlignCenter, , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , , , , , 0, 8, True, GetAddress(AddressOf btnLogin_Click)

    ' Login Buttom
    CreateButton WindowCount, "btnAccept", (Width / 2) - (164 / 2), PaddingTop + (PaddingTop * 14), 164, 38, "Acessar", OpenSans_Effect, White, , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf btnLogin_Click)
    
    ' Checkbox
    'CreateCheckbox WindowCount, "chkSaveUser", 82, 129, 142, , Options.SaveUser, "Salvar Nome?", OpenSans_Regular, , , , , DesignTypes.DesignCheckBox, , , GetAddress(AddressOf chkSaveUser_Click)

    ' Set the active control
    If Not Len(Windows(GetWindowIndex("winLogin")).Controls(GetControlIndex("winLogin", "txtUser")).Text) > 0 Then
        SetActiveControl GetWindowIndex("winLogin"), GetControlIndex("winLogin", "txtUser")
    Else
        SetActiveControl GetWindowIndex("winLogin"), GetControlIndex("winLogin", "txtPass")
    End If
    
End Sub

Public Sub CreateWindow_LoginFooter()

    ' Create Window
    CreateWindow "winLoginBottomBar", "", zOrder_Win, 0, ScreenHeight - 20, ScreenWidth, 20, 0, , , , 3, 5, DesignTypes.DesignChatSmallShadow, DesignTypes.DesignChatSmallShadow, DesignTypes.DesignChatSmallShadow, , , , , , , , , False

    ' Order of Controls
    zOrder_Con = 1
    
    ' Label
    CreateLabel WindowCount, "lblCopy", 0, -6, ScreenWidth, 0, "Copyright 2022 - 2023 Julio Sperandio. Todos os direitos reservados.", OpenSans_Regular, White, Alignment.AlignCenter
    
End Sub

Public Sub CreateWindow_LoginLogo()

    ' Create Window
    CreateWindow "winLoginLogo", "", zOrder_Win, (ScreenWidth / 2) - CenterLeft, 200, LogoSizeX, LogoSizeY, 0, , , , 0, 0, DesignTypes.DesignWindowWithoutBackground, DesignTypes.DesignWindowWithoutBackground, DesignTypes.DesignWindowWithoutBackground, , , , , , , , , False

    ' Order of Controls
    zOrder_Con = 1
    
    ' Create Picture Logo
    CreatePictureBox WindowCount, "picLoginLogo", 0, 0, 512, 142, , , , , Tex_GUI(73), Tex_GUI(73), Tex_GUI(73)
    
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
