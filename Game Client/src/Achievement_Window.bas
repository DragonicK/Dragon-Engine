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

Private WindowIndex As Long

Private Type RequirementDescriptionRec
    LineCount As Long
    LineColor(1 To MaxDescriptionLines) As Long
    Lines(1 To MaxDescriptionLines) As String
    LineIndex As Long
End Type

Private Descriptions As RequirementDescriptionRec

Public Sub CreateWindow_Achievement()
    Dim i As Long

    CreateWindow "winAchievement", "CONQUISTAS", zOrder_Win, 0, 0, 630, 360, 0, False, Fonts.OpenSans_Effect, , 2, 7, DesignTypes.desWin_AincradNorm, DesignTypes.desWin_AincradNorm, DesignTypes.desWin_AincradNorm, , , , , , , , , , , GetAddress(AddressOf RenderAchievement)
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf btnMenu_Achievement)

    CreateButton WindowCount, "btnSummary", 20, 87, 120, 26, "RESUMO", OpenSans_Effect, , , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf ChangeCategorySummary)
    CreateButton WindowCount, "btnCharacter", 20, 119, 120, 26, "PERSONAGEM", OpenSans_Effect, , , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf ChangeCategoryCharacter)
    CreateButton WindowCount, "btnQuest", 20, 151, 120, 26, "MISSÕES", OpenSans_Effect, , , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf ChangeCategoryQuest)
    CreateButton WindowCount, "btnReputation", 20, 183, 120, 26, "REPUTAÇÕES", OpenSans_Effect, , , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf ChangeCategoryReputation)
    CreateButton WindowCount, "btnDungeon", 20, 215, 120, 26, "MASMORRAS", OpenSans_Effect, , , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf ChangeCategoryDungeon)
    CreateButton WindowCount, "btnProfession", 20, 247, 120, 26, "PROFISSÕES", OpenSans_Effect, , , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf ChangeCategoryProfession)
    CreateButton WindowCount, "btnExplore", 20, 279, 120, 26, "EXPLORAÇÃO", OpenSans_Effect, , , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf ChangeCategoryExploration)
    CreateButton WindowCount, "btnPvp", 20, 311, 120, 26, "PVP", OpenSans_Effect, , , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf ChangeCategoryPvp)

    CreateLabel WindowCount, "lblProgress", 170, 90, 220, 50, "RESUMO: 0/30", OpenSans_Effect, White, Alignment.alignCentre

    CreateLabel WindowCount, "lblReq", 410, 90, 200, 50, "REQUERIMENTOS", OpenSans_Effect, White, Alignment.alignCentre

    For i = 1 To MaxAchievementList
        CreatePictureBox WindowCount, "picWhiteBox", 170, 70 + AchievementY + ((i - 1) * 32), 220, 26, , , , , , , , DesignTypes.desTextAincrad, DesignTypes.desTextAincrad, DesignTypes.desTextAincrad
    Next

    CreateLabel WindowCount, "lblName1", 170, 75 + AchievementY + (0 * 32), 220, 26, "", OpenSans_Regular, White, Alignment.alignCentre, , , , , GetAddress(AddressOf List1_MouseMove), GetAddress(AddressOf List1_MouseDown), GetAddress(AddressOf List1_MouseMove)
    CreateLabel WindowCount, "lblName2", 170, 75 + AchievementY + (1 * 32), 220, 26, "", OpenSans_Regular, White, Alignment.alignCentre, , , , , GetAddress(AddressOf List2_MouseMove), GetAddress(AddressOf List2_MouseDown), GetAddress(AddressOf List2_MouseMove)
    CreateLabel WindowCount, "lblName3", 170, 75 + AchievementY + (2 * 32), 220, 26, "", OpenSans_Regular, White, Alignment.alignCentre, , , , , GetAddress(AddressOf List3_MouseMove), GetAddress(AddressOf List3_MouseDown), GetAddress(AddressOf List3_MouseMove)
    CreateLabel WindowCount, "lblName4", 170, 75 + AchievementY + (3 * 32), 220, 26, "", OpenSans_Regular, White, Alignment.alignCentre, , , , , GetAddress(AddressOf List4_MouseMove), GetAddress(AddressOf List4_MouseDown), GetAddress(AddressOf List4_MouseMove)
    CreateLabel WindowCount, "lblName5", 170, 75 + AchievementY + (4 * 32), 220, 26, "", OpenSans_Regular, White, Alignment.alignCentre, , , , , GetAddress(AddressOf List5_MouseMove), GetAddress(AddressOf List5_MouseDown), GetAddress(AddressOf List5_MouseMove)
    CreateLabel WindowCount, "lblName6", 170, 75 + AchievementY + (5 * 32), 220, 26, "", OpenSans_Regular, White, Alignment.alignCentre, , , , , GetAddress(AddressOf List6_MouseMove), GetAddress(AddressOf List6_MouseDown), GetAddress(AddressOf List6_MouseMove)

    'Botões setas
    CreateLabel WindowCount, "lblPage", 170, 315, 220, 50, "Página: 0/0", OpenSans_Effect, White, Alignment.alignCentre
    CreateButton WindowCount, "btnUp", 335, 315, 15, 15, , , , , , , Tex_GUI(79), Tex_GUI(80), Tex_GUI(81), , , , , , GetAddress(AddressOf MovePageUp)
    CreateButton WindowCount, "btnDown", 210, 315, 15, 15, , , , , , , Tex_GUI(82), Tex_GUI(83), Tex_GUI(84), , , , , , GetAddress(AddressOf MovePageDown)

    'Arrow
    CreateButton WindowCount, "btnScrollUp", 608, 80, 15, 15, , , , , , , Tex_GUI(110), Tex_GUI(110), Tex_GUI(110), , , , , , GetAddress(AddressOf Scroll_RequirementUp)
    CreateButton WindowCount, "btnScrollDown", 608, 330, 15, 15, , , , , , , Tex_GUI(111), Tex_GUI(111), Tex_GUI(111), , , , , , GetAddress(AddressOf Scroll_RequirementDown)

    'Scroll
    CreateButton WindowCount, "ScrollUp", 610, 98, 8, 110, , , , , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf Scroll_RequirementUp)
    CreateButton WindowCount, "ScrollDown", 610, 208, 8, 110, , , , , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf Scroll_RequirementDown)

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
    Dim x As Long, Y As Long

    ItemIndex = ((PageIndex - 1) * MaxAchievementList) + 1

    If CanDrawPageItem(CategoryIndex, ItemIndex) Then
        AchievementId = GetAchievementByCategory(CategoryIndex, ItemIndex)

        Call SetWinDescriptionPosition(x, Y)
        Call ShowAchievementDesc(x, Y, AchievementId, GetPlayerAchievement(AchievementId))
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
    Dim x As Long, Y As Long

    ItemIndex = ((PageIndex - 1) * MaxAchievementList) + 2

    If CanDrawPageItem(CategoryIndex, ItemIndex) Then
        AchievementId = GetAchievementByCategory(CategoryIndex, ItemIndex)

        Call SetWinDescriptionPosition(x, Y)
        Call ShowAchievementDesc(x, Y, AchievementId, GetPlayerAchievement(AchievementId))
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
    Dim x As Long, Y As Long

    ItemIndex = ((PageIndex - 1) * MaxAchievementList) + 3

    If CanDrawPageItem(CategoryIndex, ItemIndex) Then
        AchievementId = GetAchievementByCategory(CategoryIndex, ItemIndex)

        Call SetWinDescriptionPosition(x, Y)
        Call ShowAchievementDesc(x, Y, AchievementId, GetPlayerAchievement(AchievementId))
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
    Dim x As Long, Y As Long

    ItemIndex = ((PageIndex - 1) * MaxAchievementList) + 4

    If CanDrawPageItem(CategoryIndex, ItemIndex) Then
        AchievementId = GetAchievementByCategory(CategoryIndex, ItemIndex)

        Call SetWinDescriptionPosition(x, Y)
        Call ShowAchievementDesc(x, Y, AchievementId, GetPlayerAchievement(AchievementId))
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
    Dim x As Long, Y As Long

    ItemIndex = ((PageIndex - 1) * MaxAchievementList) + 5

    If CanDrawPageItem(CategoryIndex, ItemIndex) Then
        AchievementId = GetAchievementByCategory(CategoryIndex, ItemIndex)

        Call SetWinDescriptionPosition(x, Y)
        Call ShowAchievementDesc(x, Y, AchievementId, GetPlayerAchievement(AchievementId))
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
    Dim x As Long, Y As Long

    ItemIndex = ((PageIndex - 1) * MaxAchievementList) + 6

    If CanDrawPageItem(CategoryIndex, ItemIndex) Then
        AchievementId = GetAchievementByCategory(CategoryIndex, ItemIndex)

        Call SetWinDescriptionPosition(x, Y)
        Call ShowAchievementDesc(x, Y, AchievementId, GetPlayerAchievement(AchievementId))
    End If
End Sub

Private Sub SetWinDescriptionPosition(ByRef x As Long, ByRef Y As Long)
' calc position
    x = Windows(WindowIndex).Window.Left - Windows(GetWindowIndex("winDescription")).Window.Width - 2
    Y = Windows(WindowIndex).Window.Top

    ' offscreen?
    If x < 0 Then
        ' switch to right
        x = Windows(WindowIndex).Window.Left + Windows(WindowIndex).Window.Width + 2
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
    Dim Width As Long
    Dim StringWidth As Long
    Dim xO As Long, yO As Long
    Dim i As Long, Y As Long

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top
    Width = Windows(WindowIndex).Window.Width

    StringWidth = TextWidth(Font(Fonts.OpenSans_Effect), "PONTOS DE CONQUISTA: " & GetPlayerAchievementPoints())

    RenderDesign DesignTypes.desTextAincrad, xO + 150, yO + 87, 2, 250
    RenderDesign DesignTypes.desTextAincrad, xO + 410, yO + 87, 2, 250
    RenderDesign DesignTypes.desWin_AincradMenu, xO, yO + 40, Width, 30

    RenderText Font(Fonts.OpenSans_Effect), "PONTOS DE CONQUISTA: " & GetPlayerAchievementPoints(), xO + (Width * 0.5) - (StringWidth * 0.5), yO + 48, Gold

    RenderText Font(Fonts.OpenSans_Effect), "XOXOTA", xO + (Width * 0.9) - (StringWidth * 0.5), yO + 48, Gold

    For i = Descriptions.LineIndex To Descriptions.LineCount
        RenderText Font(Fonts.OpenSans_Effect), Descriptions.Lines(i), xO + (Width * 0.8) - (StringWidth * 0.5), yO + 115 + Y, Descriptions.LineColor(i)
        Y = Y + 20
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

            Windows(WindowIndex).Controls(ControlIndex).textColour = ColorName
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
            For i = 1 To RewardCount
                With Achievement(Id).Rewards(i)
                    Select Case .Type
                    Case AchievementRewardType_Item
                    Case AchievementRewardType_Title
                    Case AchievementRewardType_Currency
                    End Select
                End With
            Next
        End If
    End If

End Sub

Private Sub DrawRequirement_Location(ByRef LineCount As Long, ByVal Id As Long, ByVal RequirementIndex As Long)
    With Achievement(Id).Requirements(RequirementIndex)

        If .Id > 0 Then
            Descriptions.Lines(LineCount) = "Ir para o mapa " & .Id
            Descriptions.LineColor(LineCount) = ColorType.Gold

            LineCount = LineCount + 1

            Call AddRequirementDescription(LineCount, .Description)
        End If

    End With
End Sub

Private Sub DrawRequirement_Kill(ByRef LineCount As Long, ByVal Id As Long, ByVal RequirementIndex As Long)
    With Achievement(Id).Requirements(RequirementIndex)

        If .Id > 0 Then

            Select Case .SecondaryType
            Case AchievementSecondaryRequirement_DestroyNpc
                Descriptions.Lines(LineCount) = "Matar ... "
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

            Case AchievementSecondaryRequirement_DestroyObject
                Descriptions.Lines(LineCount) = "Destruir ... "
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

            Case AchievementSecondaryRequirement_DestroyPlayer
                Descriptions.Lines(LineCount) = "Matar ... "
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

            End Select

        End If

    End With
End Sub

Private Sub DrawRequirement_Quest(ByRef LineCount As Long, ByVal Id As Long, ByVal RequirementIndex As Long)
    With Achievement(Id).Requirements(RequirementIndex)

        If .Id > 0 Then

            Select Case .SecondaryType
            Case AchievementSecondaryRequirement_QuestDoneById
                Descriptions.Lines(LineCount) = "Concluir missão ... "
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

            End Select

        End If

    End With
End Sub


Private Sub DrawRequirement_LevelUp(ByRef LineCount As Long, ByVal Id As Long, ByVal RequirementIndex As Long)
    With Achievement(Id).Requirements(RequirementIndex)

        If .Id > 0 Then

            Select Case .SecondaryType
            Case AchievementSecondaryRequirement_LevelUpByCharacter
                Descriptions.Lines(LineCount) = "Atingir level ... "
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

            Case AchievementSecondaryRequirement_LevelUpBySkill
                Descriptions.Lines(LineCount) = "Atingir level ... "
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

            Case AchievementSecondaryRequirement_LevelUpByParty
                Descriptions.Lines(LineCount) = "Atingir level ... "
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

            Case AchievementSecondaryRequirement_LevelUpByCraft
                Descriptions.Lines(LineCount) = "Atingir level ... "
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

            End Select

        End If

    End With
End Sub

Private Sub DrawRequirement_Instance(ByRef LineCount As Long, ByVal Id As Long, ByVal RequirementIndex As Long)
    With Achievement(Id).Requirements(RequirementIndex)

        If .Id > 0 Then

            Select Case .SecondaryType
            Case AchievementSecondaryRequirement_InstanceEnter
                Descriptions.Lines(LineCount) = "Entrar na dungeon ... "
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

            Case AchievementSecondaryRequirement_InstanceCompleted
                Descriptions.Lines(LineCount) = "Entrar na dungeon ... "
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

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

                    Descriptions.Lines(LineCount) = "Coletar item ... " & Text
                    Descriptions.LineColor(LineCount) = ColorType.Gold

                    LineCount = LineCount + 1

                    Call AddRequirementDescription(LineCount, .Description)

                End If

            Case AchievementSecondaryRequirement_AcquireCurrency
                Descriptions.Lines(LineCount) = "Coletar moeda ... "
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

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
                Descriptions.Lines(LineCount) = "Usar item ... " & Text
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)
            End Select

        End If

    End With
End Sub

Private Sub DrawRequirement_Casting(ByRef LineCount As Long, ByVal Id As Long, ByVal RequirementIndex As Long)
    With Achievement(Id).Requirements(RequirementIndex)
        If .Id > 0 And .Id <= MaximumSkills Then
            Dim Text As String
            
            Text = Skill(.Id).Name
            
            Descriptions.Lines(LineCount) = "Usar habilidade ... " & Text
            Descriptions.LineColor(LineCount) = ColorType.Gold

            LineCount = LineCount + 1

            Call AddRequirementDescription(LineCount, .Description)
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
                Descriptions.Lines(LineCount) = "Falhar em aprimorar ... " & Text
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

            Case AchievementSecondaryRequirement_ItemUpgradeById
                Descriptions.Lines(LineCount) = "Sucesso em aprimorar ... " & Text
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

            Case AchievementSecondaryRequirement_ItemUpgradeByLevel
                Descriptions.Lines(LineCount) = "Sucesso em aprimorar ... " & Text
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

            Case AchievementSecondaryRequirement_ItemUpgradeByRarity
                Descriptions.Lines(LineCount) = "Sucesso em aprimorar ... " & Text
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

            Case AchievementSecondaryRequirement_ItemUpgradeByType
                Descriptions.Lines(LineCount) = "Sucesso em aprimorar ... " & Text
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

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
                Descriptions.Lines(LineCount) = "Equipar item " & Text
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

            Case AchievementSecondaryRequirement_EquipItemByLevel
                Descriptions.Lines(LineCount) = "Equipar item " & Text
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

            Case AchievementSecondaryRequirement_EquipItemRarity
                Descriptions.Lines(LineCount) = "Equipar item " & Text
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

            Case AchievementSecondaryRequirement_EquipItemByType
                Descriptions.Lines(LineCount) = "Equipar item " & Text
                Descriptions.LineColor(LineCount) = ColorType.Gold

                LineCount = LineCount + 1

                Call AddRequirementDescription(LineCount, .Description)

            End Select

        End If

    End With
End Sub

Private Sub AddRequirementDescription(ByRef LineCount As Long, ByVal Description As String)
    If LenB(Description) > 0 Then
        Dim TextArray() As String
        Dim Width As Long
        Dim Count As Long
        Dim i As Long

        Width = 200

        WordWrap_Array Description, Width, TextArray()

        Count = UBound(TextArray)

        For i = 1 To Count
            Descriptions.Lines(LineCount) = TextArray(i)
            Descriptions.LineColor(LineCount) = ColorType.White

            LineCount = LineCount + 1
        Next
    End If
End Sub

Private Sub ClearDescriptions()
    Dim i As Long

    Descriptions.LineCount = 1
    Descriptions.LineIndex = 1

    For i = 1 To MaxDescriptionLines
        Descriptions.Lines(i) = vbNullString
        Descriptions.LineColor(i) = ColorType.White
    Next

End Sub
