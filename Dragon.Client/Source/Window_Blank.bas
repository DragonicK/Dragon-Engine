Attribute VB_Name = "Window_Blank"
Option Explicit

Public Sub CreateWindow_Blank()
    ' Create black background
    CreateWindow "winBlank", "", zOrder_Win, 0, 0, ScreenWidth, ScreenHeight, 0, , , , , , DesignTypes.DesignBlank, DesignTypes.DesignBlank, DesignTypes.DesignBlank, , , , , , , , , False, False
    
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1
End Sub
