Attribute VB_Name = "Service_Window"
Option Explicit

Private Const PageBonus = 1
Private Const PageServer = 2
Private Const PageService = 3

Private Const WindowWidth As Long = 220
Private Const OffSetY As Long = 85

Private CurrentService As Long
Private ShowPageIndex As Long

' Service Window Index.
Private WindowIndex As Long

Private Service As ServiceRec

' Descrição.
Private Const DescriptionX As Long = 110
Private Const DescriptionY As Long = 95

' Quantidade de efeitos exibidos na descrição.
Private Const MaxDescriptionTextLine As Long = 22

Private Const ExperienceText As String = "AUMENTO DE EXPERIÊNCIA"
Private Const ItemDropText As String = "AUMENTO DE DROP"
Private Const GoldRateText As String = "AUMENTO DE OURO"

Private DescriptionTextLine(0 To MaxDescriptionTextLine) As String

Public Function IsServiceVisible() As Boolean
    IsServiceVisible = Windows(WindowIndex).Window.Visible
End Function

Public Sub ShowService()
    Windows(WindowIndex).Window.Visible = True
End Sub

Public Sub HideService()
    Windows(WindowIndex).Window.Visible = False
End Sub

Public Sub CreateWindow_Services()
' Create window
    CreateWindow "winServices", "SERVIÇO PREMIUM", zOrder_Win, 0, 0, 220, 505, 0, False, Fonts.OpenSans_Effect, , 2, 7, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, , , , , , , , , , , GetAddress(AddressOf RenderServices)
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Close button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf ButtonMenu_Services)

    CreateButton WindowCount, "btnBonus", 0, 42, 80, 26, "BONUS", OpenSans_Effect, Green, , , , , , , , , , , , GetAddress(AddressOf ShowBonusRates)
    CreateButton WindowCount, "btnServer", 70, 42, 80, 26, "SERVIDOR", OpenSans_Effect, , , , , , , , , , , , , GetAddress(AddressOf ShowServerRate)
    CreateButton WindowCount, "btnService", 135, 42, 80, 26, "SERVIÇO", OpenSans_Effect, , , , , , , , , , , , , GetAddress(AddressOf ShowServiceRates)

    CreateLabel WindowCount, "lblName", 0, OffSetY - 10, 220, 15, "BONUS TOTAL", OpenSans_Effect, White, Alignment.AlignCenter
    CreateLabel WindowCount, "lblExpire", 0, OffSetY + 365, 220, 15, "TÉRMINO: 02/01/1995 23:52", OpenSans_Effect, Coral, Alignment.AlignCenter, False

    'Botões setas
    CreateLabel WindowCount, "lblPage", 0, OffSetY + 390, 220, 50, "1/1", OpenSans_Effect, White, Alignment.AlignCenter, False
    CreateButton WindowCount, "btnUp", 140, OffSetY + 390, 16, 16, , , , , False, , Tex_GUI(44), Tex_GUI(45), Tex_GUI(46), , , , , , GetAddress(AddressOf MoveUp)
    CreateButton WindowCount, "btnDown", 65, OffSetY + 390, 16, 16, , , , , False, , Tex_GUI(47), Tex_GUI(48), Tex_GUI(49), , , , , , GetAddress(AddressOf MoveDown)

    WindowIndex = WindowCount

    CurrentService = 0
    ServiceCount = 0

    ShowPageIndex = PageBonus

    UpdateServiceWindow
End Sub

Public Sub ButtonMenu_Services()
    If MyIndex > 0 Then
        If GetPlayerDead(MyIndex) Then Exit Sub
    End If

    Dim curWindow As Long
    curWindow = GetWindowIndex("winServices")

    If Windows(curWindow).Window.Visible Then
        HideWindow curWindow
    Else
        ShowWindow curWindow, , False
    End If
End Sub

Public Sub UpdateServiceWindow()

    If ShowPageIndex = PageBonus Then
        Call UpdateWindowControls(GetTotalService())
        
    ElseIf ShowPageIndex = PageServer Then
        Call UpdateWindowControls(ServerService)
        
    ElseIf ShowPageIndex = PageService Then
        Call UpdateWindowControls(GetService(CurrentService))
        
    End If
End Sub

Private Sub MoveUp()
    If CurrentService < ServiceCount Then
        CurrentService = CurrentService + 1
        Call UpdateServiceWindow
    End If
End Sub
Private Sub MoveDown()
    If CurrentService > 1 Then
        CurrentService = CurrentService - 1
        Call UpdateServiceWindow
    End If
End Sub

Private Sub ShowBonusRates()
    Dim BonusIndex As Long
    Dim ServiceIndex As Long
    Dim ServerIndex As Long

    BonusIndex = GetControlIndex("winServices", "btnBonus")
    ServiceIndex = GetControlIndex("winServices", "btnService")
    ServerIndex = GetControlIndex("winServices", "btnServer")

    Windows(WindowIndex).Controls(BonusIndex).TextColour = Green
    Windows(WindowIndex).Controls(BonusIndex).TextColourClick = Green
    Windows(WindowIndex).Controls(BonusIndex).TextColourHover = Green

    Windows(WindowIndex).Controls(ServiceIndex).TextColour = White
    Windows(WindowIndex).Controls(ServiceIndex).TextColourClick = White
    Windows(WindowIndex).Controls(ServiceIndex).TextColourHover = White

    Windows(WindowIndex).Controls(ServerIndex).TextColour = White
    Windows(WindowIndex).Controls(ServerIndex).TextColourClick = White
    Windows(WindowIndex).Controls(ServerIndex).TextColourHover = White

    Windows(WindowIndex).Controls(GetControlIndex("winServices", "lblExpire")).Visible = False
    Windows(WindowIndex).Controls(GetControlIndex("winServices", "lblPage")).Visible = False
    Windows(WindowIndex).Controls(GetControlIndex("winServices", "btnUp")).Visible = False
    Windows(WindowIndex).Controls(GetControlIndex("winServices", "btnDown")).Visible = False

    Windows(WindowIndex).Controls(GetControlIndex("winServices", "lblExpire")).Visible = False

    ShowPageIndex = PageBonus
    UpdateServiceWindow
End Sub

Private Sub ShowServerRate()
    Dim BonusIndex As Long
    Dim ServiceIndex As Long
    Dim ServerIndex As Long

    BonusIndex = GetControlIndex("winServices", "btnBonus")
    ServiceIndex = GetControlIndex("winServices", "btnService")
    ServerIndex = GetControlIndex("winServices", "btnServer")

    Windows(WindowIndex).Controls(BonusIndex).TextColour = White
    Windows(WindowIndex).Controls(BonusIndex).TextColourClick = White
    Windows(WindowIndex).Controls(BonusIndex).TextColourHover = White

    Windows(WindowIndex).Controls(ServiceIndex).TextColour = White
    Windows(WindowIndex).Controls(ServiceIndex).TextColourClick = White
    Windows(WindowIndex).Controls(ServiceIndex).TextColourHover = White

    Windows(WindowIndex).Controls(ServerIndex).TextColour = Green
    Windows(WindowIndex).Controls(ServerIndex).TextColourClick = Green
    Windows(WindowIndex).Controls(ServerIndex).TextColourHover = Green

    Windows(WindowIndex).Controls(GetControlIndex("winServices", "lblExpire")).Visible = False
    Windows(WindowIndex).Controls(GetControlIndex("winServices", "lblPage")).Visible = False
    Windows(WindowIndex).Controls(GetControlIndex("winServices", "btnUp")).Visible = False
    Windows(WindowIndex).Controls(GetControlIndex("winServices", "btnDown")).Visible = False

    Windows(WindowIndex).Controls(GetControlIndex("winServices", "lblExpire")).Visible = False

    ShowPageIndex = PageServer
    UpdateServiceWindow
End Sub

Private Sub ShowServiceRates()
    If ServiceCount = 0 Then
        Exit Sub
    End If

    Dim BonusIndex As Long
    Dim ServiceIndex As Long
    Dim ServerIndex As Long

    BonusIndex = GetControlIndex("winServices", "btnBonus")
    ServiceIndex = GetControlIndex("winServices", "btnService")
    ServerIndex = GetControlIndex("winServices", "btnServer")

    Windows(WindowIndex).Controls(BonusIndex).TextColour = White
    Windows(WindowIndex).Controls(BonusIndex).TextColourClick = White
    Windows(WindowIndex).Controls(BonusIndex).TextColourHover = White

    Windows(WindowIndex).Controls(ServiceIndex).TextColour = Green
    Windows(WindowIndex).Controls(ServiceIndex).TextColourClick = Green
    Windows(WindowIndex).Controls(ServiceIndex).TextColourHover = Green

    Windows(WindowIndex).Controls(ServerIndex).TextColour = White
    Windows(WindowIndex).Controls(ServerIndex).TextColourClick = White
    Windows(WindowIndex).Controls(ServerIndex).TextColourHover = White

    Windows(WindowIndex).Controls(GetControlIndex("winServices", "lblExpire")).Visible = True
    Windows(WindowIndex).Controls(GetControlIndex("winServices", "lblPage")).Visible = True
    Windows(WindowIndex).Controls(GetControlIndex("winServices", "btnUp")).Visible = True
    Windows(WindowIndex).Controls(GetControlIndex("winServices", "btnDown")).Visible = True

    Windows(WindowIndex).Controls(GetControlIndex("winServices", "lblExpire")).Visible = True

    CurrentService = 1
    
    ShowPageIndex = PageService
    UpdateServiceWindow
End Sub

Private Sub UpdateWindowControls(ByRef Service As ServiceRec)

    Windows(WindowIndex).Controls(GetControlIndex("winServices", "lblPage")).Text = CurrentService & "/" & ServiceCount
    Windows(WindowIndex).Controls(GetControlIndex("winServices", "lblName")).Text = Service.Name
    Windows(WindowIndex).Controls(GetControlIndex("winServices", "lblExpire")).Text = Service.EndTime

    DescriptionTextLine(0) = ExperienceText
    DescriptionTextLine(1) = "Personagem: " & Service.Character & "%"
    DescriptionTextLine(2) = "Grupo: " & Service.Party & "%"
    DescriptionTextLine(3) = "Clã: " & Service.Guild & "%"
    DescriptionTextLine(4) = "Habilidade: " & Service.Skill & "%"
    DescriptionTextLine(5) = "Produção: " & Service.Craft & "%"
    DescriptionTextLine(6) = "Missões: " & Service.Quest & "%"
    DescriptionTextLine(7) = "Pet: " & Service.Pet & "%"
    DescriptionTextLine(8) = "Talentos: " & Service.Talent & "%"
    DescriptionTextLine(9) = vbNullString
    DescriptionTextLine(10) = GoldRateText
    DescriptionTextLine(11) = "Chance de Drop: " & Service.GoldChance & "%"
    DescriptionTextLine(12) = "Aumento de Drop: " & Service.GoldIncrease & "%"
    DescriptionTextLine(13) = vbNullString
    DescriptionTextLine(14) = ItemDropText
    DescriptionTextLine(15) = "Comum: " & Service.ItemDrop(RarityType.RarityType_Common) & "%"
    DescriptionTextLine(16) = "Incomum: " & Service.ItemDrop(RarityType.RarityType_Uncommon) & "%"
    DescriptionTextLine(17) = "Raro: " & Service.ItemDrop(RarityType.RarityType_Rare) & "%"
    DescriptionTextLine(18) = "Épico: " & Service.ItemDrop(RarityType.RarityType_Epic) & "%"
    DescriptionTextLine(19) = "Mítico: " & Service.ItemDrop(RarityType.RarityType_Mythic) & "%"
    DescriptionTextLine(20) = "Antigo: " & Service.ItemDrop(RarityType.RarityType_Ancient) & "%"
    DescriptionTextLine(21) = "Lendário: " & Service.ItemDrop(RarityType.RarityType_Legendary) & "%"
    DescriptionTextLine(22) = "Etéreo: " & Service.ItemDrop(RarityType.RarityType_Ethereal) & "%"
End Sub

Private Sub RenderServices()
    Dim xO As Long, yO As Long
    Dim i As Long, SizeWidth As Long
    Dim Color As Long

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top

    'RenderDesign DesignTypes.desWin_AincradMenu, xO, yO + 40, WindowWidth, 30

    For i = 0 To MaxDescriptionTextLine
        SizeWidth = TextWidth(Font(Fonts.OpenSans_Regular), DescriptionTextLine(i))

        If DescriptionTextLine(i) = ExperienceText Then
            Color = Gold
        ElseIf DescriptionTextLine(i) = ItemDropText Then
            Color = Gold
        ElseIf DescriptionTextLine(i) = GoldRateText Then
            Color = Gold
        Else
            Color = White
        End If

        Call RenderText(Font(Fonts.OpenSans_Regular), DescriptionTextLine(i), xO + DescriptionX - (SizeWidth * 0.5), yO + DescriptionY + (i * 15), Color)
    Next

End Sub
