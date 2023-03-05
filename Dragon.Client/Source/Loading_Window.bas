Attribute VB_Name = "Loading_Window"

Public Sub CreateWindow_Loading()

    ' Create the window
    CreateWindow "winLoading", "", zOrder_Win, 0, 30, ScreenWidth, 60, 0, False, , , 0, 0, DesignTypes.DesignChatSmallShadow, DesignTypes.DesignChatSmallShadow, DesignTypes.DesignChatSmallShadow, , , , , , , , , False
    
    ' Set the index for spawning controls
    zOrder_Con = 1
    
    ' Label Information
    CreateLabel WindowCount, "lblLoading", 0, 20, ScreenWidth, , "Carregando dados do jogo ...", FontRegular, , Alignment.AlignCenter
End Sub

Public Sub Resize_WinLoading()
    Dim WindowIndex As Long
    Dim ControlIndex As Long
    
    ' Get The Window
    WindowIndex = GetWindowIndex("winLoading")
    
    ' Get The Widget
    ControlIndex = GetControlIndex("winLoading", "lblLoading")
    
    ' Centralise Window
    CentraliseWindow WindowIndex, 30
      
    ' Reposition Window
    Windows(WindowIndex).Window.Left = 0
    
    ' Resize Window
    Windows(WindowIndex).Window.Width = ScreenWidth
    
    ' Resize Label Widget
    Windows(WindowIndex).Controls(ControlIndex).Width = ScreenWidth
    
    ' Reposition Window
    Windows(WindowIndex).Controls(ControlIndex).Left = 0
    Windows(WindowIndex).Controls(ControlIndex).Top = 20
    
    ' Realignment Label Widget
    Windows(WindowIndex).Controls(ControlIndex).Align = Alignment.AlignCenter
End Sub
