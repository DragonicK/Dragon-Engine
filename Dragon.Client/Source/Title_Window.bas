Attribute VB_Name = "Title_Window"
Option Explicit

' Title Window Index.
Private WindowIndex As Long

' Quantidade de efeitos adicionados ao texto.
Private Counter As Long

' Índice de rolagem da lista.
Private TitleListIndex As Long

' Número do botão selecionado na lista.
Private SelectedButtonList As Long

' Número do título selecionado na lista.
Private SelectedTitleNum As Long

' Lista de títulos.
Private Const ListOffsetY As Integer = 35
Private Const ListX As Integer = 15
Private Const ListY As Integer = 75

' Janela de descrição do título.
Private Const DescriptionX As Integer = 194
Private Const DescriptionY As Integer = 73

' Quantidade máxima de items na lista.
Private Const MaxTitleList As Byte = 9

Public Function IsTitleVisible() As Boolean
    IsTitleVisible = Windows(WindowIndex).Window.Visible
End Function

Public Sub ShowTitle()
    Windows(WindowIndex).Window.Visible = True
End Sub

Public Sub HideTitle()
    Windows(WindowIndex).Window.Visible = False
End Sub

Public Sub CreateWindow_Title()
' Create the window
    CreateWindow "winTitle", "TÍTULOS", zOrder_Win, 0, 0, 208, 520, 0, , Fonts.FontRegular, , 3, 5, DesignTypes.DesignWindowWithTopBarAndDoubleNavBar, DesignTypes.DesignWindowWithTopBarAndDoubleNavBar, DesignTypes.DesignWindowWithTopBarAndDoubleNavBar, , , , , , , , , , , GetAddress(AddressOf RenderWindowTitle)

    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Close button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf Button_CloseTitle)

    ' Labels
    CreateLabel WindowCount, "lblTitleCount", 0, 49, 205, , "Títulos: 0/" & MaxPlayerTitles, FontRegular, Gold, Alignment.AlignCenter
    CreateLabel WindowCount, "lblTitleActivated", 0, 76, 205, , "Nenhum", FontRegular, ColorType.White, Alignment.AlignCenter

    ' PictureBox
    CreatePictureBox WindowCount, "picList" & 1, ListX, ListY + (ListOffsetY * 1), 154, 28, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf PicList1_MouseMove), GetAddress(AddressOf PicList1_Click), GetAddress(AddressOf PicList1_MouseMove)
    CreatePictureBox WindowCount, "picList" & 2, ListX, ListY + (ListOffsetY * 2), 154, 28, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf PicList2_MouseMove), GetAddress(AddressOf PicList2_Click), GetAddress(AddressOf PicList2_MouseMove)
    CreatePictureBox WindowCount, "picList" & 3, ListX, ListY + (ListOffsetY * 3), 154, 28, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf PicList3_MouseMove), GetAddress(AddressOf PicList3_Click), GetAddress(AddressOf PicList3_MouseMove)
    CreatePictureBox WindowCount, "picList" & 4, ListX, ListY + (ListOffsetY * 4), 154, 28, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf PicList4_MouseMove), GetAddress(AddressOf PicList4_Click), GetAddress(AddressOf PicList4_MouseMove)
    CreatePictureBox WindowCount, "picList" & 5, ListX, ListY + (ListOffsetY * 5), 154, 28, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf PicList5_MouseMove), GetAddress(AddressOf PicList5_Click), GetAddress(AddressOf PicList5_MouseMove)
    CreatePictureBox WindowCount, "picList" & 6, ListX, ListY + (ListOffsetY * 6), 154, 28, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf PicList6_MouseMove), GetAddress(AddressOf PicList6_Click), GetAddress(AddressOf PicList6_MouseMove)
    CreatePictureBox WindowCount, "picList" & 7, ListX, ListY + (ListOffsetY * 7), 154, 28, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf PicList7_MouseMove), GetAddress(AddressOf PicList7_Click), GetAddress(AddressOf PicList7_MouseMove)
    CreatePictureBox WindowCount, "picList" & 8, ListX, ListY + (ListOffsetY * 8), 154, 28, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf PicList8_MouseMove), GetAddress(AddressOf PicList8_Click), GetAddress(AddressOf PicList8_MouseMove)
    CreatePictureBox WindowCount, "picList" & 9, ListX, ListY + (ListOffsetY * 9), 154, 28, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf PicList9_MouseMove), GetAddress(AddressOf PicList9_Click), GetAddress(AddressOf PicList9_MouseMove)
    CreatePictureBox WindowCount, "picList" & 9, ListX, ListY + (ListOffsetY * 10), 154, 28, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf PicList10_MouseMove), GetAddress(AddressOf PicList10_Click), GetAddress(AddressOf PicList10_MouseMove)

    ' Invisible for draw
    CreatePictureBox WindowCount, "invisble", 0, 0, 0, 0, , , , , , , , , , , , , , , , GetAddress(AddressOf Draw_Title)
    
    ' Buttons
    CreateButton WindowCount, "btnActivate", ListX, ListY + 390, 80, 28, "ATIVAR", FontRegular, White, , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf Button_Activate)
    CreateButton WindowCount, "btnDisable", 110, ListY + 390, 80, 28, "DESATIVAR", FontRegular, White, , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf Button_Disable)

    'Arrow
    CreateButton WindowCount, "btnUp", 184, ListY + (ListOffsetY * 1), 15, 15, , , , , , , Tex_GUI(44), Tex_GUI(45), Tex_GUI(46), , , , , , GetAddress(AddressOf MoveListToUp)
    CreateButton WindowCount, "btnDown", 184, ListY + (ListOffsetY * 10 + 20), 15, 15, , , , , , , Tex_GUI(47), Tex_GUI(48), Tex_GUI(49), , , , , , GetAddress(AddressOf MoveListToDown)

    'Scroll
    CreateButton WindowCount, "ScrollUp", 186, ListY + (ListOffsetY * 1 + 15), 8, 157, , , , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf MoveListToUp)
    CreateButton WindowCount, "ScrollDown", 186, ListY + 206, 8, 157, , , , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf MoveListToDown)

    ' Set de WindowIndex variable to avoid search for index.
    WindowIndex = WindowCount
End Sub

' hide/show title
Private Sub Button_CloseTitle()
    Windows(WindowIndex).Window.Visible = False
End Sub

Private Sub Button_Activate()
    Dim Index As Long

    Index = SelectedButtonList + TitleListIndex

    If GetTitle(Index) > 0 Then
        Call SendSelectedTitle(Index)
    End If
End Sub

Private Sub Button_Disable()
    Call SendSelectedTitle(0)
End Sub

Private Sub RenderWindowTitle()
    Dim xO As Long, yO As Long, Width As Long
    Dim i As Long, ItemNum As Long

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top
    Width = Windows(WindowIndex).Window.Width

    
    'RenderDesign DesignTypes.desWin_AincradMenu, xO, yO + 70, Width, 30
End Sub

Private Sub Draw_Title()
    Dim xO As Long, yO As Long
    Dim i As Long
    Dim TitleNum As Long
    Dim Colour As Long
    Dim SizeWidth As Long

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top

    For i = 1 To MaxTitleList
        TitleNum = GetTitle(i + TitleListIndex)

        If TitleNum = SelectedTitleNum Then
            Colour = ColorType.Yellow
        Else
            Colour = ColorType.White
        End If

        If TitleNum > 0 And TitleNum <= MaximumTitles Then
            RenderText Font(Fonts.FontRegular), Title(TitleNum).Name, xO + ListX + 7, yO + ListY + 5 + (ListOffsetY * i), Colour
        End If
    Next

End Sub

Private Sub PicList1_Click()
    SelectedButtonList = 1
    SelectTitle

    PicList1_MouseMove
End Sub

Private Sub PicList1_MouseMove()
    Dim Id As Long

    Id = GetTitle(1 + TitleListIndex)

    If Id >= 1 And Id <= MaximumTitles Then
        Dim X As Long, Y As Long

        Call SetWinDescriptionPosition(X, Y)
        Call ShowTitleDesc(X, Y, Id)
    End If
End Sub
Private Sub PicList2_Click()
    SelectedButtonList = 2
    SelectTitle

    PicList2_MouseMove
End Sub

Private Sub PicList2_MouseMove()
    Dim Id As Long

    Id = GetTitle(2 + TitleListIndex)

    If Id >= 1 And Id <= MaximumTitles Then
        Dim X As Long, Y As Long

        Call SetWinDescriptionPosition(X, Y)
        Call ShowTitleDesc(X, Y, Id)
    End If
End Sub
Private Sub PicList3_Click()
    SelectedButtonList = 3
    SelectTitle

    PicList3_MouseMove
End Sub

Private Sub PicList3_MouseMove()
    Dim Id As Long

    Id = GetTitle(3 + TitleListIndex)

    If Id >= 1 And Id <= MaximumTitles Then
        Dim X As Long, Y As Long

        Call SetWinDescriptionPosition(X, Y)
        Call ShowTitleDesc(X, Y, Id)
    End If
End Sub

Private Sub PicList4_Click()
    SelectedButtonList = 4
    SelectTitle

    PicList4_MouseMove
End Sub

Private Sub PicList4_MouseMove()
    Dim Id As Long

    Id = GetTitle(4 + TitleListIndex)

    If Id >= 1 And Id <= MaximumTitles Then
        Dim X As Long, Y As Long

        Call SetWinDescriptionPosition(X, Y)
        Call ShowTitleDesc(X, Y, Id)
    End If
End Sub

Private Sub PicList5_Click()
    SelectedButtonList = 5
    SelectTitle

    PicList5_MouseMove
End Sub

Private Sub PicList5_MouseMove()
    Dim Id As Long

    Id = GetTitle(5 + TitleListIndex)

    If Id >= 1 And Id <= MaximumTitles Then
        Dim X As Long, Y As Long

        Call SetWinDescriptionPosition(X, Y)
        Call ShowTitleDesc(X, Y, Id)
    End If
End Sub

Private Sub PicList6_Click()
    SelectedButtonList = 6
    SelectTitle

    PicList6_MouseMove
End Sub

Private Sub PicList6_MouseMove()
    Dim Id As Long

    Id = GetTitle(6 + TitleListIndex)

    If Id >= 1 And Id <= MaximumTitles Then
        Dim X As Long, Y As Long

        Call SetWinDescriptionPosition(X, Y)
        Call ShowTitleDesc(X, Y, Id)
    End If
End Sub

Private Sub PicList7_Click()
    SelectedButtonList = 7
    SelectTitle

    PicList7_MouseMove
End Sub

Private Sub PicList7_MouseMove()
    Dim Id As Long

    Id = GetTitle(7 + TitleListIndex)

    If Id >= 1 And Id <= MaximumTitles Then
        Dim X As Long, Y As Long

        Call SetWinDescriptionPosition(X, Y)
        Call ShowTitleDesc(X, Y, Id)
    End If
End Sub

Private Sub PicList8_Click()
    SelectedButtonList = 8
    SelectTitle

    PicList8_MouseMove
End Sub

Private Sub PicList8_MouseMove()
    Dim Id As Long

    Id = GetTitle(8 + TitleListIndex)

    If Id >= 1 And Id <= MaximumTitles Then
        Dim X As Long, Y As Long

        Call SetWinDescriptionPosition(X, Y)
        Call ShowTitleDesc(X, Y, Id)
    End If
End Sub

Private Sub PicList9_Click()
    SelectedButtonList = 9
    SelectTitle

    PicList9_MouseMove
End Sub

Private Sub PicList9_MouseMove()
    Dim Id As Long

    Id = GetTitle(9 + TitleListIndex)

    If Id >= 1 And Id <= MaximumTitles Then
        Dim X As Long, Y As Long

        Call SetWinDescriptionPosition(X, Y)
        Call ShowTitleDesc(X, Y, Id)
    End If
End Sub

Private Sub PicList10_Click()
    SelectedButtonList = 10
    SelectTitle

    PicList10_MouseMove
End Sub

Private Sub PicList10_MouseMove()
    Dim Id As Long

    Id = GetTitle(10 + TitleListIndex)

    If Id >= 1 And Id <= MaximumTitles Then
        Dim X As Long, Y As Long

        Call SetWinDescriptionPosition(X, Y)
        Call ShowTitleDesc(X, Y, Id)
    End If
End Sub

Private Sub SetWinDescriptionPosition(ByRef X As Long, ByRef Y As Long)
' calc position
    X = Windows(WindowIndex).Window.Left - Windows(GetWindowIndex("winDescription")).Window.Width - 2
    Y = Windows(WindowIndex).Window.Top

    ' offscreen?
    If X < 0 Then
        ' switch to right
        X = Windows(WindowIndex).Window.Left + Windows(WindowIndex).Window.Width + 2
    End If
End Sub

Private Sub MoveListToUp()
    If TitleListIndex > 0 Then
        TitleListIndex = TitleListIndex - 1
    End If
End Sub

Private Sub MoveListToDown()
    If TitleListIndex < (MaxPlayerTitles - MaxTitleList) Then
        TitleListIndex = TitleListIndex + 1
    End If
End Sub

Private Sub SelectTitle()
' Se não houver nenhum título selecionado na lista, sai do método.
    If SelectedButtonList + TitleListIndex = 0 Then
        Exit Sub
    End If
    
    SelectedTitleNum = GetTitle(SelectedButtonList + TitleListIndex)

End Sub

Public Sub ClearTitleWindow()
    Dim i As Long

    TitleListIndex = 0
    SelectedButtonList = 0
    SelectedTitleNum = 0

    Call ClearTitles

    Windows(WindowIndex).Controls(GetControlIndex("winTitle", "lblTitleActivated")).Text = "Nenhum"
    Windows(WindowIndex).Controls(GetControlIndex("winTitle", "lblTitleActivated")).TextColour = White
    Windows(WindowIndex).Controls(GetControlIndex("winTitle", "lblTitleCount")).Text = "Títulos: 0/" & MaxPlayerTitles
End Sub

Public Sub UpdateTitleCount(ByVal TitleCount As Long)
    Windows(WindowIndex).Controls(GetControlIndex("winTitle", "lblTitleCount")).Text = "Títulos: " & TitleCount & "/" & MaxPlayerTitles
End Sub

Public Sub UpdateActiveTitle(ByVal TitleNum As Long)
    Dim ControlIndex As Long, Colour As Long, Name As String

    ControlIndex = GetControlIndex("winTitle", "lblTitleActivated")
    Colour = White

    If TitleNum >= 1 And TitleNum <= MaximumTitles Then
        Colour = ColorType.Gold
        Name = Title(TitleNum).Name
    Else
        Name = "Nenhum"
    End If

    Windows(WindowIndex).Controls(ControlIndex).Text = Name
    Windows(WindowIndex).Controls(ControlIndex).TextColour = Colour
    Windows(WindowIndex).Controls(ControlIndex).TextColourClick = Colour
    Windows(WindowIndex).Controls(ControlIndex).TextColourHover = Colour
End Sub

Public Sub ButtonMenu_Title()
    If MyIndex > 0 Then
        If GetPlayerDead(MyIndex) Then Exit Sub
    End If

    Dim curWindow As Long
    curWindow = GetWindowIndex("winTitle")

    If Windows(curWindow).Window.Visible Then
        HideWindow curWindow
    Else
        ShowWindow curWindow, , False
    End If
End Sub
