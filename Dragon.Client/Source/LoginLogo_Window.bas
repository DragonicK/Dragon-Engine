Attribute VB_Name = "LoginLogo_Window"
Private Const LogoSizeY As Long = 142
Private Const LogoSizeX As Long = 512
Private Const CenterLeft As Long = ((LogoSizeX + 40) / 2)

Public Sub CreateWindow_LoginLogo()

    ' Create Window
    CreateWindow "winLoginLogo", "", zOrder_Win, (ScreenWidth / 2) - CenterLeft, 200, LogoSizeX, LogoSizeY, 0, , , , 0, 0, DesignTypes.DesignWindowWithoutBackground, DesignTypes.DesignWindowWithoutBackground, DesignTypes.DesignWindowWithoutBackground, , , , , , , , , False

    ' Order of Controls
    zOrder_Con = 1
    
    '
    CreatePictureBox WindowCount, "picLoginLogo", 0, 0, 512, 142, , , , , Tex_GUI(73), Tex_GUI(73), Tex_GUI(73)
    
End Sub

