Attribute VB_Name = "Login_Window"
Public LoginToken As String

Public Sub CreateWindow_Login()
    CreateWindow "winLogin", "LOGIN", zOrder_Win, 0, 0, 300, 200, 0, , Fonts.OpenSans_Effect, , 3, 5, DesignTypes.desWin_AincradNorm, DesignTypes.desWin_AincradNorm, DesignTypes.desWin_AincradNorm, , , , , , , , , False
    ' Center Window
    CentraliseWindow WindowCount

    ' Order of Controls
    zOrder_Con = 1

    ' Close Button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf DestroyGame)

   ' Login Icon
    CreatePictureBox WindowCount, "iconLogin", 20, 60, 19, 23, , , , , Tex_GUI(151), Tex_GUI(151), Tex_GUI(151)
    CreatePictureBox WindowCount, "iconPass", 20, 95, 19, 23, , , , , Tex_GUI(152), Tex_GUI(152), Tex_GUI(152)
    
    ' Button definitions
    CreateButton WindowCount, "btnAccept", 40, 155, 100, 26, "ACEITAR", OpenSans_Effect, White, , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desGreen_Click, , , GetAddress(AddressOf btnLogin_Click)
    CreateButton WindowCount, "btnExit", 160, 155, 100, 26, "SAIR", OpenSans_Effect, White, , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desGreen_Click, , , GetAddress(AddressOf DestroyGame)
    
    ' Labels
    CreateLabel WindowCount, "lblUsername", 80, 49, 142, , "Login", OpenSans_Regular, White, Alignment.alignCentre
    CreateLabel WindowCount, "lblPassword", 80, 88, 142, , "Senha", OpenSans_Regular, White, Alignment.alignCentre
    
    ' Textboxes
    CreateTextbox WindowCount, "txtUser", 80, 65, 142, 24, Options.Username, Fonts.OpenSans_Regular, , Alignment.alignLeft, , , , , , DesignTypes.desTextAincrad, DesignTypes.desTextAincrad, DesignTypes.desTextAincrad, , , , , , , 8, 5
    CreateTextbox WindowCount, "txtPass", 80, 103, 142, 24, vbNullString, Fonts.OpenSans_Regular, , Alignment.alignLeft, , , , , , DesignTypes.desTextAincrad, DesignTypes.desTextAincrad, DesignTypes.desTextAincrad, , , , , , , 8, 5, True, GetAddress(AddressOf btnLogin_Click)
    
    ' Checkbox
    CreateCheckbox WindowCount, "chkSaveUser", 82, 129, 142, , Options.SaveUser, "Salvar Nome?", OpenSans_Regular, , , , , DesignTypes.desChkNorm, , , GetAddress(AddressOf chkSaveUser_Click)

   ' ' Checkbox
  '  CreateCheckbox WindowCount, "chkSaveUser", 55, 128, 142, , Options.SaveUser, "Salvar Nome?", OpenSans_Regular, , , , , DesignTypes.desCheck, , , GetAddress(AddressOf chkSaveUser_Click)

    ' Set the active control
    If Not Len(Windows(GetWindowIndex("winLogin")).Controls(GetControlIndex("winLogin", "txtUser")).Text) > 0 Then
        SetActiveControl GetWindowIndex("winLogin"), GetControlIndex("winLogin", "txtUser")
    Else
        SetActiveControl GetWindowIndex("winLogin"), GetControlIndex("winLogin", "txtPass")
    End If
    
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
