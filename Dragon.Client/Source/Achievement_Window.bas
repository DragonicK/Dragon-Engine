Attribute VB_Name = "Achievement_Window"
Option Explicit

Private GeneralCount As Integer
Private SummaryCount As Integer
Private CharacterCount As Integer
Private QuestCount As Integer
Private ReputationCount As Integer
Private DungeonCount As Integer
Private ProfessionCount As Integer
Private ExplorationCount As Integer
Private PvpCount As Integer

Private PlayerGeneralCount As Integer
Private PlayerSummaryCount As Integer
Private PlayerCharacterCount As Integer
Private PlayerQuestCount As Integer
Private PlayerReputationCount As Integer
Private PlayerDungeonCount As Integer
Private PlayerProfessionCount As Integer
Private PlayerExplorationCount As Integer
Private PlayerPvpCount As Integer

Private GeneralAchievement As Collection
Private SummaryAchievement As Collection
Private CharacterAchievement As Collection
Private QuestAchievement As Collection
Private ReputationAchievement As Collection
Private DungeonAchievement As Collection
Private ProfessionAchievement As Collection
Private ExplorationAchievement As Collection
Private PvpAchievement As Collection

Private CategoryIndex As AchievementCategory
Private PageIndex As Long
Private PageCount As Long
Private SelectedAchievementId As Long

' Achievement UI
Private Const MaxAchievementList As Long = 6
Private Const AchievementY As Long = 45
Private Const MaxDescriptionLines As Long = 200
Private Const DescriptionEnd As String = "#END#"

Private Const ICON_ACHIEVEMENT_WIDTH = 25
Private Const ICON_ACHIEVEMENT_HEIGHT = 25

Private WindowIndex As Long

Private Type RequirementDescriptionRec
    LineCount As Long
    LineColor(1 To MaxDescriptionLines) As Long
    Lines(1 To MaxDescriptionLines) As String
    Reward(1 To MaxDescriptionLines) As AchievementRewardRec
    LineIndex As Long
End Type

Private Descriptions As RequirementDescriptionRec

Public Sub CreateWindow_Achievement()
    Dim i As Long

    CreateWindow "winAchievement", "CONQUISTAS", zOrder_Win, 0, 0, 630, 360, 0, False, Fonts.FontRegular, , 2, 7, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, DesignTypes.DesignWindowWithTopBar, , , , , , , , , , , GetAddress(AddressOf RenderAchievement)
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf btnMenu_Achievement)

    CreateButton WindowCount, "btnSummary", 20, 87, 120, 26, "RESUMO", FontRegular, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ChangeCategorySummary)
    CreateButton WindowCount, "btnCharacter", 20, 119, 120, 26, "PERSONAGEM", FontRegular, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ChangeCategoryCharacter)
    CreateButton WindowCount, "btnQuest", 20, 151, 120, 26, "MISSÕES", FontRegular, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ChangeCategoryQuest)
    CreateButton WindowCount, "btnReputation", 20, 183, 120, 26, "REPUTAÇÕES", FontRegular, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ChangeCategoryReputation)
    CreateButton WindowCount, "btnDungeon", 20, 215, 120, 26, "MASMORRAS", FontRegular, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ChangeCategoryDungeon)
    CreateButton WindowCount, "btnProfession", 20, 247, 120, 26, "PROFISSÕES", FontRegular, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ChangeCategoryProfession)
    CreateButton WindowCount, "btnExplore", 20, 279, 120, 26, "EXPLORAÇÃO", FontRegular, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ChangeCategoryExploration)
    CreateButton WindowCount, "btnPvp", 20, 311, 120, 26, "PVP", FontRegular, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf ChangeCategoryPvp)

    CreateLabel WindowCount, "lblProgress", 170, 90, 220, 50, "RESUMO: 0/30", FontRegular, White, Alignment.AlignCenter

    CreateLabel WindowCount, "lblReq", 430, 90, 200, 50, "REQUERIMENTOS", FontRegular, ColorType.Gold, Alignment.AlignLeft

    For i = 1 To MaxAchievementList
        CreatePictureBox WindowCount, "picWhiteBox", 170, 70 + AchievementY + ((i - 1) * 32), 220, 26, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox
    Next

    CreateLabel WindowCount, "lblName1", 170, 75 + AchievementY + (0 * 32), 220, 26, "", FontRegular, White, Alignment.AlignCenter, , , , , GetAddress(AddressOf List1_MouseMove), GetAddress(AddressOf List1_MouseDown), GetAddress(AddressOf List1_MouseMove)
    CreateLabel WindowCount, "lblName2", 170, 75 + AchievementY + (1 * 32), 220, 26, "", FontRegular, White, Alignment.AlignCenter, , , , , GetAddress(AddressOf List2_MouseMove), GetAddress(AddressOf List2_MouseDown), GetAddress(AddressOf List2_MouseMove)
    CreateLabel WindowCount, "lblName3", 170, 75 + AchievementY + (2 * 32), 220, 26, "", FontRegular, White, Alignment.AlignCenter, , , , , GetAddress(AddressOf List3_MouseMove), GetAddress(AddressOf List3_MouseDown), GetAddress(AddressOf List3_MouseMove)
    CreateLabel WindowCount, "lblName4", 170, 75 + AchievementY + (3 * 32), 220, 26, "", FontRegular, White, Alignment.AlignCenter, , , , , GetAddress(AddressOf List4_MouseMove), GetAddress(AddressOf List4_MouseDown), GetAddress(AddressOf List4_MouseMove)
    CreateLabel WindowCount, "lblName5", 170, 75 + AchievementY + (4 * 32), 220, 26, "", FontRegular, White, Alignment.AlignCenter, , , , , GetAddress(AddressOf List5_MouseMove), GetAddress(AddressOf List5_MouseDown), GetAddress(AddressOf List5_MouseMove)
    CreateLabel WindowCount, "lblName6", 170, 75 + AchievementY + (5 * 32), 220, 26, "", FontRegular, White, Alignment.AlignCenter, , , , , GetAddress(AddressOf List6_MouseMove), GetAddress(AddressOf List6_MouseDown), GetAddress(AddressOf List6_MouseMove)

    'Botões setas
    CreateLabel WindowCount, "lblPage", 170, 315, 220, 50, "Página: 0/0", FontRegular, White, Alignment.AlignCenter
    CreateButton WindowCount, "btnUp", 335, 315, 15, 15, , , , , , , Tex_GUI(44), Tex_GUI(45), Tex_GUI(46), , , , , , GetAddress(AddressOf MovePageUp)
    CreateButton WindowCount, "btnDown", 210, 315, 15, 15, , , , , , , Tex_GUI(47), Tex_GUI(48), Tex_GUI(49), , , , , , GetAddress(AddressOf MovePageDown)

    'Arrow
    CreateButton WindowCount, "btnScrollUp", 608, 80, 15, 15, , , , , , , Tex_GUI(44), Tex_GUI(45), Tex_GUI(46), , , , , , GetAddress(AddressOf Scroll_RequirementUp)
    CreateButton WindowCount, "btnScrollDown", 608, 330, 15, 15, , , , , , , Tex_GUI(47), Tex_GUI(48), Tex_GUI(49), , , , , , GetAddress(AddressOf Scroll_RequirementDown)

    'Scroll
    CreateButton WindowCount, "ScrollUp", 610, 98, 8, 110, , , , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf Scroll_RequirementUp)
    CreateButton WindowCount, "ScrollDown", 610, 208, 8, 110, , , , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf Scroll_RequirementDown)

    ' Set the default values
    CategoryIndex = AchievementCategory_Summary
    PageIndex = 1

    Descriptions.LineIndex = 1
    Descriptions.LineCount = 1

    WindowIndex = WindowCount
End Sub

Private Sub Scroll_RequirementUp()
    If Descriptions.LineIndex > 1 Then
        Descriptions.LineIndex = Descriptions.LineIndex - 1
    End If
End Sub

Private Sub Scroll_RequirementDown()
    If Descriptions.LineIndex < Descriptions.LineCount Then
        Descriptions.LineIndex = Descriptions.LineIndex + 1
    End If
End Sub

Private Sub List1_MouseDown()
    Dim ItemIndex As Long

    ItemIndex = ((PageIndex - 1) * MaxAchievementList) + 1

    If CanDrawPageItem(CategoryIndex, ItemIndex) Then
        SelectedAchievementId = GetAchievementByCategory(CategoryIndex, ItemIndex)
        Call BuildAchievementRequirementDescription(SelectedAchievementId)
    End If
End Sub

Private Sub List1_MouseMove()
    Dim ItemIndex As Long
    Dim AchievementId As Long
    Dim X As Long, Y As Long

    ItemIndex = ((PageIndex - 1) * MaxAchievementList) + 1

    If CanDrawPageItem(CategoryIndex, ItemIndex) Then
        AchievementId = GetAchievementByCategory(CategoryIndex, ItemIndex)

        Call SetWinDescriptionPosition(X, Y)
        Call ShowAchievementDesc(X, Y, AchievementId, GetPlayerAchievement(AchievementId))
    End If
End Sub

Private Sub List2_MouseDown()
    Dim ItemIndex As Long

    ItemIndex = ((PageIndex - 1) * MaxAchievementList) + 2

    If CanDrawPageItem(CategoryIndex, ItemIndex) Then
        SelectedAchievementId = GetAchievementByCategory(CategoryIndex, ItemIndex)
        Call BuildAchievementRequirementDescription(SelectedAchievementId)
    End If

End Sub

Private Sub List2_MouseMove()
    Dim ItemIndex As Long
    Dim AchievementId As Long
    Dim X As Long, Y As Long

    ItemIndex = ((PageIndex - 1) * MaxAchievementList) + 2

    If CanDrawPageItem(CategoryIndex, ItemIndex) Then
        AchievementId = GetAchievementByCategory(CategoryIndex, ItemIndex)

        Call SetWinDescriptionPosition(X, Y)
        Call ShowAchievementDesc(X, Y, AchievementId, GetPlayerAchievement(AchievementId))
    End If
End Sub

Private Sub List3_MouseDown()
    Dim ItemIndex As Long

    ItemIndex = ((PageIndex - 1) * MaxAchievementList) + 3

    If CanDrawPageItem(CategoryIndex, ItemIndex) Then
        SelectedAchievementId = GetAchievementByCategory(CategoryIndex, ItemIndex)
        Call BuildAchievementRequirementDescription(SelectedAchievementId)
    End If
End Sub

Private Sub List3_MouseMove()
    Dim ItemIndex As Long
    Dim AchievementId As Long
    Dim X As Long, Y As Long

    ItemIndex = ((PageIndex - 1) * MaxAchievementList) + 3

    If CanDrawPageItem(CategoryIndex, ItemIndex) Then
        AchievementId = GetAchievementByCategory(CategoryIndex, ItemIndex)

        Call SetWinDescriptionPosition(X, Y)
        Call ShowAchievementDesc(X, Y, AchievementId, GetPlayerAchievement(AchievementId))
    End If
End Sub

Private Sub List4_MouseDown()
    Dim ItemIndex As Long

    ItemIndex = ((PageIndex - 1) * MaxAchievementList) + 4

    If CanDrawPageItem(CategoryIndex, ItemIndex) Then
        SelectedAchievementId = GetAchievementByCategory(CategoryIndex, ItemIndex)
        Call BuildAchievementRequirementDescription(SelectedAchievementId)
    End If
End Sub

Private Sub List4_MouseMove()
    Dim ItemIndex As Long
    Dim AchievementId As Long
    Dim X As Long, Y As Long

    ItemIndex = ((PageIndex - 1) * MaxAchievementList) + 4

    If CanDrawPageItem(CategoryIndex, ItemIndex) Then
        AchievementId = GetAchievementByCategory(CategoryIndex, ItemIndex)

        Call SetWinDescriptionPosition(X, Y)
        Call ShowAchievementDesc(X, Y, AchievementId, GetPlayerAchievement(AchievementId))
    End If
End Sub

Private Sub List5_MouseDown()
    Dim ItemIndex As Long

    ItemIndex = ((PageIndex - 1) * MaxAchievementList) + 5

    If CanDrawPageItem(CategoryIndex, ItemIndex) Then
        SelectedAchievementId = GetAchievementByCategory(CategoryIndex, ItemIndex)
        Call BuildAchievementRequirementDescription(SelectedAchievementId)
    End If
End Sub

Private Sub List5_MouseMove()
    Dim ItemIndex As Long
    Dim AchievementId As Long
    Dim X As Long, Y As Long

    ItemIndex = ((PageIndex - 1) * MaxAchievementList) + 5

    If CanDrawPageItem(CategoryIndex, ItemIndex) Then
        AchievementId = GetAchievementByCategory(CategoryIndex, ItemIndex)

        Call SetWinDescriptionPosition(X, Y)
        Call ShowAchievementDesc(X, Y, AchievementId, GetPlayerAchievement(AchievementId))
    End If
End Sub

Private Sub List6_MouseDown()
    Dim ItemIndex As Long

    ItemIndex = ((PageIndex - 1) * MaxAchievementList) + 6

    If CanDrawPageItem(CategoryIndex, ItemIndex) Then
        SelectedAchievementId = GetAchievementByCategory(CategoryIndex, ItemIndex)
        Call BuildAchievementRequirementDescription(SelectedAchievementId)
    End If
End Sub

Private Sub List6_MouseMove()
    Dim ItemIndex As Long
    Dim AchievementId As Long
    Dim X As Long, Y As Long

    ItemIndex = ((PageIndex - 1) * MaxAchievementList) + 6

    If CanDrawPageItem(CategoryIndex, ItemIndex) Then
        AchievementId = GetAchievementByCategory(CategoryIndex, ItemIndex)

        Call SetWinDescriptionPosition(X, Y)
        Call ShowAchievementDesc(X, Y, AchievementId, GetPlayerAchievement(AchievementId))
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

Private Sub ChangeCategoryGeneral()
    CategoryIndex = AchievementCategory_General
    Call SetPageIndexAndPageCount(GeneralCount)
    CheckAchievement
    ClearDescriptions
End Sub
Private Sub ChangeCategorySummary()
    CategoryIndex = AchievementCategory_Summary
    Call SetPageIndexAndPageCount(SummaryCount)
    CheckAchievement
    ClearDescriptions
End Sub
Private Sub ChangeCategoryCharacter()
    CategoryIndex = AchievementCategory_Character
    Call SetPageIndexAndPageCount(CharacterCount)
    CheckAchievement
    ClearDescriptions
End Sub
Private Sub ChangeCategoryQuest()
    CategoryIndex = AchievementCategory_Quest
    Call SetPageIndexAndPageCount(QuestCount)
    CheckAchievement
    ClearDescriptions
End Sub
Private Sub ChangeCategoryReputation()
    CategoryIndex = AchievementCategory_Reputation
    Call SetPageIndexAndPageCount(ReputationCount)
    CheckAchievement
    ClearDescriptions
End Sub
Private Sub ChangeCategoryDungeon()
    CategoryIndex = AchievementCategory_Dungeon
    Call SetPageIndexAndPageCount(DungeonCount)
    CheckAchievement
    ClearDescriptions
End Sub
Private Sub ChangeCategoryProfession()
    CategoryIndex = AchievementCategory_Profession
    Call SetPageIndexAndPageCount(ProfessionCount)
    CheckAchievement
    ClearDescriptions
End Sub
Private Sub ChangeCategoryExploration()
    CategoryIndex = AchievementCategory_Exploration
    Call SetPageIndexAndPageCount(ExplorationCount)
    CheckAchievement
    ClearDescriptions
End Sub
Private Sub ChangeCategoryPvp()
    CategoryIndex = AchievementCategory_Pvp
    Call SetPageIndexAndPageCount(PvpCount)
    CheckAchievement
    ClearDescriptions
End Sub

Private Sub SetPageIndexAndPageCount(ByVal Count As Long)
    PageIndex = 1

    Dim Rest As Double
    PageCount = CInt(Count / MaxAchievementList)
    Rest = Count Mod MaxAchievementList
    If Rest > 0 Then PageCount = PageCount + 1
    If PageCount = 0 Then PageCount = 1
End Sub

Private Sub MovePageUp()
    If PageIndex < PageCount Then
        PageIndex = PageIndex + 1

        Call CheckAchievement
        Call ClearDescriptions
    End If
End Sub
Private Sub MovePageDown()
    If PageIndex > 1 Then
        PageIndex = PageIndex - 1

        Call CheckAchievement
        Call ClearDescriptions
    End If
End Sub

Public Sub AllocateAllAchievements()
    Dim i As Long

    Set GeneralAchievement = New Collection
    Set SummaryAchievement = New Collection
    Set CharacterAchievement = New Collection
    Set QuestAchievement = New Collection
    Set ReputationAchievement = New Collection
    Set DungeonAchievement = New Collection
    Set ProfessionAchievement = New Collection
    Set ExplorationAchievement = New Collection
    Set PvpAchievement = New Collection

    For i = 1 To MaxAchievements
        Select Case Achievement(i).Category
        Case AchievementCategory_General
            GeneralCount = GeneralCount + 1
            Call GeneralAchievement.Add(i)

        Case AchievementCategory_Summary
            SummaryCount = SummaryCount + 1
            Call SummaryAchievement.Add(i)

        Case AchievementCategory_Character
            CharacterCount = CharacterCount + 1
            Call CharacterAchievement.Add(i)

        Case AchievementCategory_Quest
            QuestCount = QuestCount + 1
            Call QuestAchievement.Add(i)

        Case AchievementCategory_Reputation
            ReputationCount = ReputationCount + 1
            Call ReputationAchievement.Add(i)

        Case AchievementCategory_Dungeon
            DungeonCount = DungeonCount + 1
            Call DungeonAchievement.Add(i)

        Case AchievementCategory_Profession
            ProfessionCount = ProfessionCount + 1
            Call ProfessionAchievement.Add(i)

        Case AchievementCategory_Exploration
            ExplorationCount = ExplorationCount + 1
            Call ExplorationAchievement.Add(i)

        Case AchievementCategory_Pvp
            PvpCount = PvpCount + 1
            Call PvpAchievement.Add(i)
        End Select
    Next

End Sub

Public Sub CountPlayerAchievements()
    Dim i As Long

    PlayerGeneralCount = 0
    PlayerSummaryCount = 0
    PlayerCharacterCount = 0
    PlayerQuestCount = 0
    PlayerReputationCount = 0
    PlayerDungeonCount = 0
    PlayerProfessionCount = 0
    PlayerExplorationCount = 0
    PlayerPvpCount = 0

    For i = 1 To MaxAchievements
        If GetPlayerAchievement(i) > 0 Then
            Select Case Achievement(i).Category
            Case AchievementCategory_General
                PlayerGeneralCount = PlayerGeneralCount + 1
            Case AchievementCategory_Summary
                PlayerSummaryCount = PlayerSummaryCount + 1
            Case AchievementCategory_Character
                PlayerCharacterCount = PlayerCharacterCount + 1
            Case AchievementCategory_Quest
                PlayerQuestCount = PlayerQuestCount + 1
            Case AchievementCategory_Reputation
                PlayerReputationCount = PlayerReputationCount + 1
            Case AchievementCategory_Dungeon
                PlayerDungeonCount = PlayerDungeonCount + 1
            Case AchievementCategory_Profession
                PlayerProfessionCount = PlayerProfessionCount + 1
            Case AchievementCategory_Exploration
                PlayerExplorationCount = PlayerExplorationCount + 1
            Case AchievementCategory_Pvp
                PlayerPvpCount = PlayerPvpCount + 1
            End Select
        End If
    Next

End Sub

Private Function GetProgressText(ByVal Category As AchievementCategory) As String
    Select Case Category
    Case AchievementCategory_General
        GetProgressText = "GERAL: " & PlayerGeneralCount & "/" & GeneralCount

    Case AchievementCategory_Summary
        GetProgressText = "RESUMO: " & PlayerSummaryCount & "/" & SummaryCount

    Case AchievementCategory_Character
        GetProgressText = "PERSONAGEM: " & PlayerCharacterCount & "/" & CharacterCount

    Case AchievementCategory_Quest
        GetProgressText = "MISSÕES: " & PlayerQuestCount & "/" & QuestCount

    Case AchievementCategory_Reputation
        GetProgressText = "REPUTAÇÕES: " & PlayerReputationCount & "/" & ReputationCount

    Case AchievementCategory_Dungeon
        GetProgressText = "MASMORRAS: " & PlayerDungeonCount & "/" & DungeonCount

    Case AchievementCategory_Profession
        GetProgressText = "PROFISSÕES: " & PlayerProfessionCount & "/" & ProfessionCount

    Case AchievementCategory_Exploration
        GetProgressText = "EXPLORAÇÃO: " & PlayerExplorationCount & "/" & ExplorationCount

    Case AchievementCategory_Pvp
        GetProgressText = "PVP: " & PvpCount & "/" & PlayerPvpCount
    End Select

End Function

Private Sub RenderAchievement()
    Dim Width As Long, Height As Long
    Dim StringWidth As Long
    Dim xO As Long, yO As Long
    Dim i As Long, Y As Long
    Dim Color As ColorType

    Dim CurrentX As Long
    Dim CurrentY As Long

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top
    Width = Windows(WindowIndex).Window.Width
    Height = Windows(WindowIndex).Window.Height

    StringWidth = TextWidth(Font(Fonts.FontRegular), "PONTOS DE CONQUISTA: " & GetPlayerAchievementPoints())

    RenderDesign DesignTypes.DesignTextBox, xO + 150, yO + 87, 2, 250
    RenderDesign DesignTypes.DesignTextBox, xO + 410, yO + 87, 2, 250
    

    RenderText Font(Fonts.FontRegular), "PONTOS DE CONQUISTA: " & GetPlayerAchievementPoints(), xO + (Width * 0.5) - (StringWidth * 0.5), yO + 48, Gold

    For i = Descriptions.LineIndex To Descriptions.LineCount
        With Descriptions
            ' Se chegar ao limite, retorna para evitar processamento.
            If .Lines(i) = DescriptionEnd Then
                Exit Sub
            End If

            ' Limite de linhas na tela.
            If Y <= 200 Then
                Dim IconId As Long

                If .Reward(i).Type = AchievementRewardType_Item Then
                    If .Reward(i).Id > 0 And .Reward(i).Id <= MaximumItems Then
                        IconId = Item(.Reward(i).Id).IconId

                        CurrentX = xO + (Width * 0.68)
                        CurrentY = yO + 108 + Y

                        RenderTexture Tex_Item(IconId), CurrentX, CurrentY, 0, 0, ICON_ACHIEVEMENT_WIDTH, ICON_ACHIEVEMENT_HEIGHT, PIC_X, PIC_Y
                        RenderText Font(Fonts.FontRegular), .Lines(i), CurrentX + PIC_X, yO + 115 + Y, .LineColor(i)

                        Call ShowRewardItemDescription(CurrentX, CurrentY, .Reward(i))

                        Y = Y + ICON_ACHIEVEMENT_WIDTH
                    End If
                ElseIf .Reward(i).Type = AchievementRewardType_Title Then
                    If .Reward(i).Id > 0 And .Reward(i).Id <= MaximumTitles Then
                        IconId = 1

                        CurrentX = xO + (Width * 0.68)
                        CurrentY = yO + 108 + Y

                        RenderTexture Tex_Spellicon(IconId), CurrentX, CurrentY, 0, 0, ICON_ACHIEVEMENT_WIDTH, ICON_ACHIEVEMENT_HEIGHT, PIC_X, PIC_Y
                        RenderText Font(Fonts.FontRegular), .Lines(i), CurrentX + PIC_X, yO + 115 + Y, .LineColor(i)

                        Call ShowRewardTitleDescription(CurrentX, CurrentY, .Reward(i))

                        Y = Y + ICON_ACHIEVEMENT_WIDTH
                    End If
                ElseIf .Reward(i).Type = AchievementRewardType_Currency Then
                    If .Reward(i).Id >= 0 And .Reward(i).Id < CurrencyType.Currency_Count Then
                        Dim CurData As CurrencyRec

                        CurData = GetCurrencyData(.Reward(i).Id)

                        CurrentX = xO + (Width * 0.68)
                        CurrentY = yO + 108 + Y

                        RenderTexture Tex_Item(CurData.IconId), CurrentX, CurrentY, 0, 0, ICON_ACHIEVEMENT_WIDTH, ICON_ACHIEVEMENT_HEIGHT, PIC_X, PIC_Y
                        RenderText Font(Fonts.FontRegular), .Lines(i), CurrentX + PIC_X, yO + 115 + Y, .LineColor(i)

                        Call ShowRewardCurrencyDescription(CurrentX, CurrentY, .Reward(i))

                        Y = Y + ICON_ACHIEVEMENT_WIDTH
                    End If

                ElseIf .Reward(i).Type = AchievementRewardType_None Then
                    RenderText Font(Fonts.FontRegular), .Lines(i), xO + (Width * 0.8) - (StringWidth * 0.5), yO + 115 + Y, .LineColor(i)
                    Y = Y + 20
                End If

            End If
        End With
    Next

End Sub

Public Sub CheckAchievement()
    Dim i As Long
    Dim ControlIndex As Long
    Dim ColorName As Long
    Dim AchievementId As Long, ItemIndex As Long

    Windows(WindowIndex).Controls(GetControlIndex("winAchievement", "lblProgress")).Text = GetProgressText(CategoryIndex)
    Windows(WindowIndex).Controls(GetControlIndex("winAchievement", "lblPage")).Text = "Página: " & PageIndex & "/" & PageCount

    For i = 1 To MaxAchievementList
        ColorName = Grey
        ItemIndex = ((PageIndex - 1) * MaxAchievementList) + i

        ControlIndex = GetControlIndex("winAchievement", "lblName" & i)

        If CanDrawPageItem(CategoryIndex, ItemIndex) Then
            AchievementId = GetAchievementByCategory(CategoryIndex, ItemIndex)

            If GetPlayerAchievement(AchievementId) > 0 Then
                ColorName = Gold
            End If

            Windows(WindowIndex).Controls(ControlIndex).TextColour = ColorName
            Windows(WindowIndex).Controls(ControlIndex).Text = Achievement(AchievementId).Name
        Else
            Windows(WindowIndex).Controls(ControlIndex).Text = vbNullString
        End If
    Next

End Sub
Private Function GetAchievementByCategory(ByVal Category As AchievementCategory, ByVal ItemIndex As Long) As Long
    Select Case Category
    Case AchievementCategory_General
        GetAchievementByCategory = GeneralAchievement.Item(ItemIndex)
    Case AchievementCategory_Summary
        GetAchievementByCategory = SummaryAchievement.Item(ItemIndex)
    Case AchievementCategory_Character
        GetAchievementByCategory = CharacterAchievement.Item(ItemIndex)
    Case AchievementCategory_Quest
        GetAchievementByCategory = QuestAchievement.Item(ItemIndex)
    Case AchievementCategory_Reputation
        GetAchievementByCategory = ReputationAchievement.Item(ItemIndex)
    Case AchievementCategory_Dungeon
        GetAchievementByCategory = DungeonAchievement.Item(ItemIndex)
    Case AchievementCategory_Profession
        GetAchievementByCategory = ProfessionAchievement.Item(ItemIndex)
    Case AchievementCategory_Exploration
        GetAchievementByCategory = ExplorationAchievement.Item(ItemIndex)
    Case AchievementCategory_Pvp
        GetAchievementByCategory = PvpAchievement.Item(ItemIndex)
    End Select
End Function

Private Function CanDrawPageItem(ByVal Category As AchievementCategory, ByVal ItemIndex As Long) As Boolean
    Select Case Category
    Case AchievementCategory_General
        If ItemIndex <= GeneralCount Then CanDrawPageItem = True
    Case AchievementCategory_Summary
        If ItemIndex <= SummaryCount Then CanDrawPageItem = True
    Case AchievementCategory_Character
        If ItemIndex <= CharacterCount Then CanDrawPageItem = True
    Case AchievementCategory_Quest
        If ItemIndex <= QuestCount Then CanDrawPageItem = True
    Case AchievementCategory_Reputation
        If ItemIndex <= ReputationCount Then CanDrawPageItem = True
    Case AchievementCategory_Dungeon
        If ItemIndex <= DungeonCount Then CanDrawPageItem = True
    Case AchievementCategory_Profession
        If ItemIndex <= ProfessionCount Then CanDrawPageItem = True
    Case AchievementCategory_Exploration
        If ItemIndex <= ExplorationCount Then CanDrawPageItem = True
    Case AchievementCategory_Pvp
        If ItemIndex <= PvpCount Then CanDrawPageItem = True
    End Select

End Function

Private Sub BuildAchievementRequirementDescription(ByVal Id As Long)
    Dim i As Long
    Dim RequirementCount As Long
    Dim RewardCount As Long

    Call ClearDescriptions

    If Id > 0 And Id <= MaxAchievements Then
        RequirementCount = Achievement(Id).RequirementCount
        RewardCount = Achievement(Id).RewardCount

        If RequirementCount > 0 Then
            For i = 1 To RequirementCount
                With Achievement(Id).Requirements(i)

                    Select Case .PrimaryType
                    Case AchievementPrimaryRequirement_Location
                        Call DrawRequirement_Location(Descriptions.LineCount, Id, i)

                    Case AchievementPrimaryRequirement_Kill
                        Call DrawRequirement_Kill(Descriptions.LineCount, Id, i)

                    Case AchievementPrimaryRequirement_Quest
                        Call DrawRequirement_Quest(Descriptions.LineCount, Id, i)

                    Case AchievementPrimaryRequirement_LevelUp
                        Call DrawRequirement_LevelUp(Descriptions.LineCount, Id, i)

                    Case AchievementPrimaryRequirement_Instance
                        Call DrawRequirement_Instance(Descriptions.LineCount, Id, i)

                    Case AchievementPrimaryRequirement_Acquire
                        Call DrawRequirement_Acquire(Descriptions.LineCount, Id, i)

                    Case AchievementPrimaryRequirement_UseItem
                        Call DrawRequirement_UseItem(Descriptions.LineCount, Id, i)

                    Case AchievementPrimaryRequirement_EquipItem
                        Call DrawRequirement_EquipItem(Descriptions.LineCount, Id, i)

                    Case AchievementPrimaryRequirement_ItemUpgrade
                        Call DrawRequirement_ItemUpgrade(Descriptions.LineCount, Id, i)

                    Case AchievementPrimaryRequirement_Casting
                        Call DrawRequirement_Casting(Descriptions.LineCount, Id, i)

                    End Select
                End With
            Next
        End If

        If RewardCount > 0 Then
            Call AddRequirementDescription(Descriptions.LineCount, vbNullString, ColorType.White)
            Call AddRequirementDescription(Descriptions.LineCount, "RECOMPENSAS", ColorType.Gold)

            For i = 1 To RewardCount
                With Achievement(Id).Rewards(i)
                    If .Type > AchievementRewardType_None Then
                        Call AddRewardDescription(Descriptions.LineCount, Achievement(Id).Rewards(i))
                    End If
                End With
            Next
        End If

        ' Indica que finalizou as descrições.
        Call AddRequirementDescription(Descriptions.LineCount, DescriptionEnd, ColorType.White)
    End If

End Sub

Private Sub DrawRequirement_Location(ByRef LineCount As Long, ByVal Id As Long, ByVal RequirementIndex As Long)
    With Achievement(Id).Requirements(RequirementIndex)
        If .Id > 0 Then
            Call AddRequirementDescription(LineCount, "Ir para o mapa ...", ColorType.Coral)
            Call AddRequirementDescription(LineCount, .Description, ColorType.White)
        End If
    End With
End Sub

Private Sub DrawRequirement_Kill(ByRef LineCount As Long, ByVal Id As Long, ByVal RequirementIndex As Long)
    With Achievement(Id).Requirements(RequirementIndex)
        If .Id > 0 Then
            Select Case .SecondaryType
            Case AchievementSecondaryRequirement_DestroyNpc
                Call AddRequirementDescription(LineCount, "Matar ...", ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)

            Case AchievementSecondaryRequirement_DestroyObject
                Call AddRequirementDescription(LineCount, "Destruir ...", ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)

            Case AchievementSecondaryRequirement_DestroyPlayer
                Call AddRequirementDescription(LineCount, "Matar jogador ...", ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)
            End Select
        End If
    End With
End Sub

Private Sub DrawRequirement_Quest(ByRef LineCount As Long, ByVal Id As Long, ByVal RequirementIndex As Long)
    With Achievement(Id).Requirements(RequirementIndex)
        If .Id > 0 Then
            Select Case .SecondaryType
            Case AchievementSecondaryRequirement_QuestDoneById
                Call AddRequirementDescription(LineCount, "Concluir missão", ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)
            End Select
        End If
    End With
End Sub

Private Sub DrawRequirement_LevelUp(ByRef LineCount As Long, ByVal Id As Long, ByVal RequirementIndex As Long)
    With Achievement(Id).Requirements(RequirementIndex)
        If .Id > 0 Then
            Select Case .SecondaryType
            Case AchievementSecondaryRequirement_LevelUpByCharacter
                Call AddRequirementDescription(LineCount, "Atingir level ...", ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)

            Case AchievementSecondaryRequirement_LevelUpBySkill
                Call AddRequirementDescription(LineCount, "Atingir level da habilidade ...", ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)

            Case AchievementSecondaryRequirement_LevelUpByParty
                Call AddRequirementDescription(LineCount, "Atingir level do grupo ...", ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)

            Case AchievementSecondaryRequirement_LevelUpByCraft
                Call AddRequirementDescription(LineCount, "Atingir level de produção ...", ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)
            End Select
        End If
    End With
End Sub

Private Sub DrawRequirement_Instance(ByRef LineCount As Long, ByVal Id As Long, ByVal RequirementIndex As Long)
    With Achievement(Id).Requirements(RequirementIndex)
        If .Id > 0 Then
            Select Case .SecondaryType
            Case AchievementSecondaryRequirement_InstanceEnter
                Call AddRequirementDescription(LineCount, "Entrar na masmorra", ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)

            Case AchievementSecondaryRequirement_InstanceCompleted
                Call AddRequirementDescription(LineCount, "Completar masmorra", ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)

            End Select
        End If
    End With
End Sub

Private Sub DrawRequirement_Acquire(ByRef LineCount As Long, ByVal Id As Long, ByVal RequirementIndex As Long)
    With Achievement(Id).Requirements(RequirementIndex)
        If .Id > 0 Then
            Select Case .SecondaryType
            Case AchievementSecondaryRequirement_AcquireItem
                If .Id > 0 And .Id <= MaximumItems Then
                    Dim Text As String

                    Text = Item(.Id).Name

                    Call AddRequirementDescription(LineCount, "Coletar Item " & Text, ColorType.Coral)
                    Call AddRequirementDescription(LineCount, .Description, ColorType.White)
                End If

            Case AchievementSecondaryRequirement_AcquireCurrency
                Call AddRequirementDescription(LineCount, "Coletar Moeda ...", ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)

            End Select
        End If
    End With
End Sub

Private Sub DrawRequirement_UseItem(ByRef LineCount As Long, ByVal Id As Long, ByVal RequirementIndex As Long)
    With Achievement(Id).Requirements(RequirementIndex)

        If .Id > 0 And .Id <= MaximumItems Then
            Dim Text As String

            Text = Item(.Id).Name

            Select Case .SecondaryType
            Case AchievementSecondaryRequirement_UseItemById
                Call AddRequirementDescription(LineCount, "Usar o item " & Text, ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)
            End Select

        End If

    End With
End Sub

Private Sub DrawRequirement_Casting(ByRef LineCount As Long, ByVal Id As Long, ByVal RequirementIndex As Long)
    With Achievement(Id).Requirements(RequirementIndex)
        If .Id > 0 And .Id <= MaximumSkills Then
            Dim Text As String

            Text = Skill(.Id).Name

            Call AddRequirementDescription(LineCount, "Usar a habilidade " & Text, ColorType.Coral)
            Call AddRequirementDescription(LineCount, .Description, ColorType.White)
        End If
    End With
End Sub

Private Sub DrawRequirement_ItemUpgrade(ByRef LineCount As Long, ByVal Id As Long, ByVal RequirementIndex As Long)
    With Achievement(Id).Requirements(RequirementIndex)
        If .Id > 0 And .Id <= MaximumItems Then
            Dim Text As String

            Text = Item(.Id).Name

            Select Case .SecondaryType
            Case AchievementSecondaryRequirement_ItemUpgradeByFailed
                Call AddRequirementDescription(LineCount, "Falhar em aprimorar " & Text, ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)

            Case AchievementSecondaryRequirement_ItemUpgradeById
                Call AddRequirementDescription(LineCount, "Sucesso em aprimorar " & Text, ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)

            Case AchievementSecondaryRequirement_ItemUpgradeByLevel
                Call AddRequirementDescription(LineCount, "Sucesso em aprimorar " & Text, ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)

            Case AchievementSecondaryRequirement_ItemUpgradeByRarity
                Call AddRequirementDescription(LineCount, "Sucesso em aprimorar " & Text, ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)

            Case AchievementSecondaryRequirement_ItemUpgradeByType
                Call AddRequirementDescription(LineCount, "Sucesso em aprimorar " & Text, ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)

            End Select
        End If
    End With
End Sub

Private Sub DrawRequirement_EquipItem(ByRef LineCount As Long, ByVal Id As Long, ByVal RequirementIndex As Long)
    With Achievement(Id).Requirements(RequirementIndex)
        If .Id > 0 And .Id <= MaximumItems Then
            Dim Text As String

            Text = Item(.Id).Name

            Select Case .SecondaryType
            Case AchievementSecondaryRequirement_EquipItemById
                Call AddRequirementDescription(LineCount, "Equipar o item " & Text, ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)

            Case AchievementSecondaryRequirement_EquipItemByLevel
                Call AddRequirementDescription(LineCount, "Equipar o item " & Text, ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)

            Case AchievementSecondaryRequirement_EquipItemRarity
                Call AddRequirementDescription(LineCount, "Equipar o item " & Text, ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)

            Case AchievementSecondaryRequirement_EquipItemByType
                Call AddRequirementDescription(LineCount, "Equipar o item " & Text, ColorType.Coral)
                Call AddRequirementDescription(LineCount, .Description, ColorType.White)

            End Select
        End If
    End With
End Sub

Private Sub AddRequirementDescription(ByRef LineCount As Long, ByVal Description As String, ByVal Color As ColorType)
    Dim TextArray() As String
    Dim Width As Long
    Dim Count As Long
    Dim i As Long

    If Description <> vbNullString Then
        Width = 200

        WordWrap_Array Description, Width, TextArray()

        Count = UBound(TextArray)

        For i = 1 To Count
            Descriptions.Lines(LineCount) = TextArray(i)
            Descriptions.LineColor(LineCount) = Color

            LineCount = LineCount + 1
        Next
    Else
        Descriptions.Lines(LineCount) = vbNullString
        Descriptions.LineColor(LineCount) = Color

        LineCount = LineCount + 1
    End If
End Sub

Private Sub AddRewardDescription(ByRef LineCount As Long, ByRef Reward As AchievementRewardRec)
    With Reward
        Descriptions.Reward(LineCount).Type = .Type
        Descriptions.Reward(LineCount).Id = .Id
        Descriptions.Reward(LineCount).Value = .Value
        Descriptions.Reward(LineCount).Level = .Level
        Descriptions.Reward(LineCount).Bound = .Bound
        Descriptions.Reward(LineCount).AttributeId = .AttributeId
        Descriptions.Reward(LineCount).UpgradeId = .UpgradeId

        Select Case .Type
        Case AchievementRewardType_Item
            If .Id > 0 And .Id <= MaximumItems Then
                Descriptions.LineColor(LineCount) = GetRarityColor(Item(.Id).Rarity)
                Descriptions.Lines(LineCount) = Item(.Id).Name

                LineCount = LineCount + 1
            End If

        Case AchievementRewardType_Title
            If .Id > 0 And .Id <= MaximumTitles Then
                Descriptions.LineColor(LineCount) = GetRarityColor(Title(.Id).Rarity)
                Descriptions.Lines(LineCount) = Title(.Id).Name

                LineCount = LineCount + 1
            End If

        Case AchievementRewardType_Currency
            If .Id >= 0 And .Id < CurrencyType.Currency_Count Then
                Dim CurData As CurrencyRec

                CurData = GetCurrencyData(.Id)

                Descriptions.LineColor(LineCount) = ColorType.Gold
                Descriptions.Lines(LineCount) = CurData.Name & ": " & .Value

                LineCount = LineCount + 1
            End If

        End Select
    End With
End Sub

Private Sub ClearDescriptions()
    Dim i As Long

    Descriptions.LineCount = 1
    Descriptions.LineIndex = 1

    For i = 1 To MaxDescriptionLines
        Descriptions.Lines(i) = vbNullString
        Descriptions.LineColor(i) = ColorType.White

        With Descriptions.Reward(i)
            .Type = AchievementRewardType_None
            .Id = 0
            .Value = 0
            .Level = 0
            .Bound = 0
            .AttributeId = 0
            .UpgradeId = 0
        End With
    Next

End Sub

Private Sub ShowRewardItemDescription(ByVal CurrentX As Long, ByVal CurrentY As Long, ByRef Reward As AchievementRewardRec)
    Dim Right As Long, Bottom As Long
    
    Right = CurrentX + ICON_ACHIEVEMENT_WIDTH
    Bottom = CurrentY + ICON_ACHIEVEMENT_HEIGHT

    If currMouseX >= CurrentX And currMouseX <= Right Then
        If currMouseY >= CurrentY And currMouseY <= Bottom Then

            Dim Inventory As InventoryRec
            Dim ItemId As Long
            Dim X As Long, Y As Long

            ItemId = Reward.Id

            Inventory.Num = Reward.Id
            Inventory.Value = Reward.Value
            Inventory.Level = Reward.Level
            Inventory.Bound = Reward.Bound
            Inventory.AttributeId = Reward.AttributeId
            Inventory.UpgradeId = Reward.UpgradeId

            Call SetWinDescriptionPosition(X, Y)

            If Item(ItemId).Type = ItemType.ItemType_Heraldry Then
                Call ShowHeraldryDescription(X, Y, Inventory, Item(ItemId).Price)
            Else
                ShowItemDesc X, Y, Inventory
            End If

        End If
    End If
End Sub

Private Sub ShowRewardTitleDescription(ByVal CurrentX As Long, ByVal CurrentY As Long, ByRef Reward As AchievementRewardRec)
    Dim Right As Long, Bottom As Long

    Right = CurrentX + ICON_ACHIEVEMENT_WIDTH
    Bottom = CurrentY + ICON_ACHIEVEMENT_HEIGHT

    If currMouseX >= CurrentX And currMouseX <= Right Then
        If currMouseY >= CurrentY And currMouseY <= Bottom Then
            Dim X As Long, Y As Long

            Call SetWinDescriptionPosition(X, Y)
            Call ShowTitleDesc(X, Y, Reward.Id)
        End If
    End If
End Sub

Private Sub ShowRewardCurrencyDescription(ByVal CurrentX As Long, ByVal CurrentY As Long, ByRef Reward As AchievementRewardRec)
    Dim Right As Long, Bottom As Long

    Right = CurrentX + ICON_ACHIEVEMENT_WIDTH
    Bottom = CurrentY + ICON_ACHIEVEMENT_HEIGHT

    If currMouseX >= CurrentX And currMouseX <= Right Then
        If currMouseY >= CurrentY And currMouseY <= Bottom Then
            Dim X As Long, Y As Long
            Dim CurData As CurrencyRec

            CurData = GetCurrencyData(Reward.Id)

            Call SetWinDescriptionPosition(X, Y)
            Call ShowCurrencyDesc(X, Y, CurData.Id, Reward.Value)
        End If
    End If
End Sub
