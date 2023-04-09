Attribute VB_Name = "Target_Window"
Option Explicit

Public Sub CreateWindow_Target()

    ' Create window
    CreateWindow "winTarget", "", zOrder_Win, 255, 10, 239, 78, 0, True, Fonts.FontRegular, , 2, 7, DesignTypes.DesignWindowWithoutTopBar, DesignTypes.DesignWindowWithoutTopBar, DesignTypes.DesignWindowWithoutTopBar, , , , , , , , , , , GetAddress(AddressOf Target_OnDraw)

    zOrder_Con = 1

    ' Bar HP
    CreatePictureBox WindowCount, "picHP_Blank", 15, 30, 209, 13, , , , , Tex_GUI(18), Tex_GUI(18), Tex_GUI(18)

    ' Bar SP
    CreatePictureBox WindowCount, "picSP_Blank", 15, 47, 209, 13, , , , , Tex_GUI(20), Tex_GUI(20), Tex_GUI(20)

    ' Label HP
    CreateLabel WindowCount, "lblHP", 15, 29, 209, 12, "0/0", FontRegular, White, Alignment.AlignCenter

    ' Label MP
    CreateLabel WindowCount, "lblMP", 15, 46, 209, 12, "0/0", FontRegular, White, Alignment.AlignCenter

    ' Label Name
    CreateLabel WindowCount, "lblName", 0, 10, 239, 25, "Name", FontRegular, White, Alignment.AlignCenter

End Sub

Public Sub OpenTargetWindow()
    If Player(MyIndex).Dead Then Exit Sub

    Windows(GetWindowIndex("winTarget")).Window.Visible = True
End Sub

Public Sub CloseTargetWindow()
    Windows(GetWindowIndex("winTarget")).Window.Visible = False
End Sub

Private Sub Target_OnDraw()
    Dim xO As Long, yO As Long, Width As Long, WindowIndex As Long

    WindowIndex = GetWindowIndex("winTarget")

    If Windows(WindowIndex).Window.Visible = False Then Exit Sub

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top

    RenderTexture Tex_GUI(19), xO + 15, yO + 30, 0, 0, BarWidth_TargetHP, 13, BarWidth_TargetHP, 13
    RenderTexture Tex_GUI(21), xO + 15, yO + 47, 0, 0, BarWidth_TargetMP, 13, BarWidth_TargetMP, 13
End Sub
