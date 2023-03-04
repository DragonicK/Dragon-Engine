Attribute VB_Name = "Loading_Window"

Public Sub CreateWindow_Loading()

    ' Create the window
    CreateWindow "winLoading", "", zOrder_Win, 0, 30, ScreenWidth, 60, 0, False, , , 0, 0, DesignTypes.DesignChatSmallShadow, DesignTypes.DesignChatSmallShadow, DesignTypes.DesignChatSmallShadow, , , , , , , , , False
    
    ' Set the index for spawning controls
    zOrder_Con = 1
    
    ' Label Information
    CreateLabel WindowCount, "lblLoading", (ScreenWidth / 2) - (300 / 2), 20, 300, , "Carregando dados do jogo ...", FontRegular, , Alignment.AlignCenter
End Sub
