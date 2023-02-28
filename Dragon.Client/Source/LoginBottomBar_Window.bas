Attribute VB_Name = "LoginBottomBar_Window"

Public Sub CreateWindow_LoginBottomBar()

    ' Create Window
    CreateWindow "winLoginBottomBar", "", zOrder_Win, 0, ScreenHeight - 20, ScreenWidth, 20, 0, , , , 3, 5, DesignTypes.DesignChatSmallShadow, DesignTypes.DesignChatSmallShadow, DesignTypes.DesignChatSmallShadow, , , , , , , , , False

    ' Order of Controls
    zOrder_Con = 1
    
    ' Label
    CreateLabel WindowCount, "lblCopy", 0, -6, ScreenWidth, 0, "Copyright 2022 - 2023 Julio Sperandio. Todos os direitos reservados.", OpenSans_Regular, White, Alignment.AlignCenter
    
End Sub
