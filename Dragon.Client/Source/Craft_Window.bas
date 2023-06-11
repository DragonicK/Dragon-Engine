Attribute VB_Name = "Craft_Window"
Option Explicit

Private Const RecipeLevelDifference As Byte = 10
' Quantidade máxima de items exibidos.
Private Const MaxCraftList As Long = 10
' Posição dos controles e da janela.
Private Const ListOffsetY As Long = 38
Private Const CraftOffsetY As Long = 25
Private Const WindowOffsetY As Long = 55

' Indica que a produção foi iniciada.
Private IsCraftStarted As Boolean
' Progesso em porcentagem.
Private CraftProgressPercentage As Byte
' Incremento do progesso.
Private CraftProgressStep As Byte
' Receita selecionada na lista.
Private SelectedRecipeNum As Long
' Botão da lista selecionado.
Private SelectedButtonList As Long
' Indice de movimento da lista.
Private CraftListIndex As Long
Private WindowIndex As Long

Public CurrentCraftName As String

Public Function IsCraftVisible() As Boolean
    IsCraftVisible = Windows(WindowIndex).Window.Visible
End Function

Public Sub ShowCraft()
    Windows(WindowIndex).Window.Visible = True
End Sub

Public Sub HideCraft()
    Windows(WindowIndex).Window.Visible = False
End Sub

Public Sub CreateWindow_Craft()
    Dim i As Long

    CreateWindow "winCraft", "PRODUÇÃO", zOrder_Win, 0, 0, 389, 408, 0, True, Fonts.FontRegular, , 2, 5, DesignTypes.DesignWindowWithTopBarAndNavBar, DesignTypes.DesignWindowWithTopBarAndNavBar, DesignTypes.DesignWindowWithTopBarAndNavBar, , , , , 0, 0, 0, 0, , , GetAddress(AddressOf DrawCraft)

    CentraliseWindow WindowCount
    ' Set the index for spawning controls
    zOrder_Con = 1

    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf ButtonMenu_Craft)

    CreatePictureBox WindowCount, "picList" & 1, 20, WindowOffsetY + 21 + (CraftOffsetY * 1), 130, 20, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , GetAddress(AddressOf PicList1_Click)
    CreatePictureBox WindowCount, "picList" & 2, 20, WindowOffsetY + 21 + (CraftOffsetY * 2), 130, 20, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , GetAddress(AddressOf PicList2_Click)
    CreatePictureBox WindowCount, "picList" & 3, 20, WindowOffsetY + 21 + (CraftOffsetY * 3), 130, 20, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , GetAddress(AddressOf PicList3_Click)
    CreatePictureBox WindowCount, "picList" & 4, 20, WindowOffsetY + 21 + (CraftOffsetY * 4), 130, 20, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , GetAddress(AddressOf PicList4_Click)
    CreatePictureBox WindowCount, "picList" & 5, 20, WindowOffsetY + 21 + (CraftOffsetY * 5), 130, 20, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , GetAddress(AddressOf PicList5_Click)
    CreatePictureBox WindowCount, "picList" & 6, 20, WindowOffsetY + 21 + (CraftOffsetY * 6), 130, 20, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , GetAddress(AddressOf PicList6_Click)
    CreatePictureBox WindowCount, "picList" & 7, 20, WindowOffsetY + 21 + (CraftOffsetY * 7), 130, 20, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , GetAddress(AddressOf PicList7_Click)
    CreatePictureBox WindowCount, "picList" & 8, 20, WindowOffsetY + 21 + (CraftOffsetY * 8), 130, 20, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , GetAddress(AddressOf PicList8_Click)
    CreatePictureBox WindowCount, "picList" & 9, 20, WindowOffsetY + 21 + (CraftOffsetY * 9), 130, 20, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , GetAddress(AddressOf PicList9_Click)
    CreatePictureBox WindowCount, "picList" & 9, 20, WindowOffsetY + 21 + (CraftOffsetY * 10), 130, 20, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , GetAddress(AddressOf PicList9_Click)

    For i = 1 To MaxRecipeRequiredItems
        CreateLabel WindowCount, "lblReqCount" & i, 193, WindowOffsetY + 58 + (CraftOffsetY * i), 175, 20, "", FontRegular, White, Alignment.AlignRight
        CreateLabel WindowCount, "lblReqItem" & i, 200, WindowOffsetY + 58 + (CraftOffsetY * i), 175, 20, "", FontRegular, White, Alignment.AlignLeft
    Next

    CreatePictureBox WindowCount, "picRequirement1", 193, WindowOffsetY + 55 + (CraftOffsetY * 1), 180, 20, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf Requirement1_MouseMove), GetAddress(AddressOf Requirement1_MouseMove), GetAddress(AddressOf Requirement1_MouseMove)
    CreatePictureBox WindowCount, "picRequirement2", 193, WindowOffsetY + 55 + (CraftOffsetY * 2), 180, 20, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf Requirement2_MouseMove), GetAddress(AddressOf Requirement2_MouseMove), GetAddress(AddressOf Requirement2_MouseMove)
    CreatePictureBox WindowCount, "picRequirement3", 193, WindowOffsetY + 55 + (CraftOffsetY * 3), 180, 20, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf Requirement3_MouseMove), GetAddress(AddressOf Requirement3_MouseMove), GetAddress(AddressOf Requirement3_MouseMove)
    CreatePictureBox WindowCount, "picRequirement4", 193, WindowOffsetY + 55 + (CraftOffsetY * 4), 180, 20, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf Requirement4_MouseMove), GetAddress(AddressOf Requirement4_MouseMove), GetAddress(AddressOf Requirement4_MouseMove)
    CreatePictureBox WindowCount, "picRequirement5", 193, WindowOffsetY + 55 + (CraftOffsetY * 5), 180, 20, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf Requirement5_MouseMove), GetAddress(AddressOf Requirement5_MouseMove), GetAddress(AddressOf Requirement5_MouseMove)

    'Nomes
    CreateLabel WindowCount, "lblResults", 208, WindowOffsetY + 31, 142, , "Resultado de Criação", FontRegular, White, Alignment.AlignCenter
    CreateLabel WindowCount, "lblSuplement", 208, WindowOffsetY + 204, 142, , "Progresso", FontRegular, White, Alignment.AlignCenter
    CreateLabel WindowCount, "lblProgress", 208, WindowOffsetY + 218, 142, , "Processando: 0%", FontRegular, White, Alignment.AlignCenter
    CreateLabel WindowCount, "lblSuccessRate", 185, WindowOffsetY + 258, 205, 25, "Chance de Sucesso: 100%", FontRegular, White, Alignment.AlignCenter, , , , , , , , 0
    CreateLabel WindowCount, "lblList", 20, WindowOffsetY + 31, 130, , "Receitas", FontRegular, White, Alignment.AlignCenter
    CreateLabel WindowCount, "lblExp", 90, WindowOffsetY - 5, 209, , GetCraftName(CraftType_None) & "Nenhum Lv. 0 0/0", FontRegular, Gold, Alignment.AlignCenter

    ' Result Item Slot
    CreatePictureBox WindowCount, "picItemResult", 268, WindowOffsetY + 45, 32, 32, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , GetAddress(AddressOf ResultItem_MouseMove), GetAddress(AddressOf ResultItem_MouseMove), GetAddress(AddressOf ResultItem_MouseMove)
 
    'Botões setas
    CreateButton WindowCount, "btnCraft", 155, WindowOffsetY + 22 + (CraftOffsetY * 1), 15, 15, , , , , , , Tex_GUI(44), Tex_GUI(45), Tex_GUI(46), , , , , , GetAddress(AddressOf MoveListToUp)
    CreateButton WindowCount, "btnCraft", 155, WindowOffsetY + 24 + (CraftOffsetY * 10), 15, 15, , , , , , , Tex_GUI(47), Tex_GUI(48), Tex_GUI(49), , , , , , GetAddress(AddressOf MoveListToDown)

    'Botões inferiores
    CreateButton WindowCount, "btnCraft", 90, WindowOffsetY + 310, 103, 24, "CRIAR", FontRegular, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf Button_Craft)
    CreateButton WindowCount, "btnCancel", 198, WindowOffsetY + 310, 103, 24, "CANCELAR", FontRegular, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf Button_Cancel)
    
    WindowIndex = WindowCount
End Sub

Private Sub DrawCraft()
    Dim xO As Long, yO As Long

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top

    ' Sai do metodo para evitar processamento quando não há produção.
    If GetCraftType = CraftType.CraftType_None Then
        Exit Sub
    End If

    Dim RecipeNum As Long
    Dim Color As Long, LevelDifference As Long
    Dim i As Long

    ' Recipe's name list
    For i = 1 To MaxCraftList
        RecipeNum = GetCraftRecipeNum(i + CraftListIndex)

        If RecipeNum > 0 Then
            LevelDifference = GetCraftLevel - Recipe(RecipeNum).Level
            ' -10 to 0
            If LevelDifference <= (-RecipeLevelDifference) Then
                Color = BrightRed
            Else
                Color = Gold
            End If

            ' 0 to 10
            If LevelDifference >= 0 And LevelDifference <= RecipeLevelDifference Then
                Color = Gold
            End If

            ' 11 to 20
            If LevelDifference > RecipeLevelDifference And LevelDifference <= (RecipeLevelDifference * 2) Then
                Color = Green
            End If

            ' 21+
            If LevelDifference > (RecipeLevelDifference * 2) Then
                Color = White
            End If

            RenderText Font(Fonts.FontRegular), Trim$(Recipe(RecipeNum).Name), xO + 25, WindowOffsetY + yO + 24 + (CraftOffsetY * i), Color
        End If
    Next

    If CraftProgressPercentage > 0 Then
        Windows(WindowIndex).Window.CanDrag = False
    Else
        Windows(WindowIndex).Window.CanDrag = True
    End If

    Dim Percentage As Single
    Percentage = CSng(CraftProgressPercentage / 100)

    RenderTexture Tex_GUI(50), xO + 193, WindowOffsetY + yO + 235, 0, 0, 180, 32, 180, 32
    RenderTexture Tex_GUI(51), xO + 193, WindowOffsetY + yO + 235, 0, 0, 180 * Percentage, 32, 180 * Percentage, 32

End Sub

Private Sub OpenCraftWindow()
    If GetCraftType > CraftType.CraftType_None Then
        Dim i As Long

        Windows(WindowIndex).Window.Visible = True

        For i = 1 To MaxRecipeRequiredItems
            Windows(WindowIndex).Controls(GetControlIndex("winCraft", "lblReqCount" & i)).Text = ""
            Windows(WindowIndex).Controls(GetControlIndex("winCraft", "lblReqItem" & i)).Text = ""
        Next

        Windows(WindowIndex).Controls(GetControlIndex("winCraft", "lblSuccessRate")).Text = "Chance de Sucesso: 0%"

        SelectedRecipeNum = 0
        CraftListIndex = 0
        SelectedButtonList = 0

        Call SetControlResultItem(0)
    End If
End Sub
Private Sub Button_Craft()
' Não permite iniciar outra receita enquanto a primeira não for terminada.
    If IsCraftStarted Then
        Exit Sub
    End If

    If GetCraftType > CraftType.CraftType_None Then
        If SelectedRecipeNum > 0 And SelectedRecipeNum <= MaxRecipes Then
            Call SendCraftItem(SelectedButtonList + CraftListIndex)
        End If
    End If
End Sub

Private Sub Button_Cancel()
    If IsCraftStarted Then
        CraftProgressPercentage = 0
        IsCraftStarted = False
        UpdateProcessText

        SendCraftStopProcess
    End If
End Sub

Private Sub MoveListToUp()
    If CraftListIndex > 0 Then
        If Not IsCraftStarted Then
            CraftListIndex = CraftListIndex - 1
        End If
    End If
End Sub
Private Sub MoveListToDown()
    If CraftListIndex < (MaxPlayerRecipes - MaxCraftList) Then
        If Not IsCraftStarted Then
            CraftListIndex = CraftListIndex + 1
        End If
    End If
End Sub

Public Sub UpdateCraftWindow()
    Dim ControlIndex As Long
    Dim Text As String

    ControlIndex = GetControlIndex("winCraft", "lblExp")
    Text = GetCraftName(GetCraftType()) & " Lv. " & GetCraftLevel & " " & GetCraftExp & "/" & GetCraftNextLevelExp
    
    Windows(WindowIndex).Controls(ControlIndex).Text = Text
End Sub

Public Sub SelectCraftRecipe()
' Sai do metodo se o jogador não tiver nenhum craft habilitado.
    If GetCraftType = CraftType.CraftType_None Then Exit Sub

    ' Se não houver nenhuma receita selecionada na lista, sai do método.
    If SelectedButtonList + CraftListIndex = 0 Then Exit Sub

    Dim ControlIndexReq As Long, ControlIndexItem As Long
    Dim Success As Integer, Critical As Integer
    Dim i As Long, ItemId As Long, ItemValue As Long, ItemLevel As Long, TextLevel As String

    If GetCraftType > CraftType.CraftType_None Then
        SelectedRecipeNum = GetCraftRecipeNum(SelectedButtonList + CraftListIndex)

        If SelectedRecipeNum > 0 Then
            ItemId = Recipe(SelectedRecipeNum).RewardItem.Num
            
            If ItemId > 0 Then
                Call SetControlResultItem(Item(ItemId).IconId)
            Else
                Call SetControlResultItem(0)
            End If

            For i = 1 To MaxRecipeRequiredItems
                ControlIndexReq = GetControlIndex("winCraft", "lblReqCount" & i)
                ControlIndexItem = GetControlIndex("winCraft", "lblReqItem" & i)

                ItemId = Recipe(SelectedRecipeNum).RequiredItem(i).Num

                If ItemId > 0 Then
                    ItemValue = Recipe(SelectedRecipeNum).RequiredItem(i).Value
                    ItemLevel = Recipe(SelectedRecipeNum).RequiredItem(i).Level
                    TextLevel = " "

                    If ItemLevel > 0 Then TextLevel = " +" & ItemLevel

                    Windows(WindowIndex).Controls(ControlIndexItem).Text = Trim$(Item(ItemId).Name) & TextLevel
                    Windows(WindowIndex).Controls(ControlIndexReq).Text = GetInvItemValue(ItemId, ItemLevel) & "/" & ItemValue
                Else
                    Windows(WindowIndex).Controls(ControlIndexItem).Text = ""
                    Windows(WindowIndex).Controls(ControlIndexReq).Text = ""
                End If
            Next

        Else
            For i = 1 To MaxRecipeRequiredItems
                ControlIndexReq = GetControlIndex("winCraft", "lblReqCount" & i)
                ControlIndexItem = GetControlIndex("winCraft", "lblReqItem" & i)

                Windows(WindowIndex).Controls(ControlIndexItem).Text = ""
                Windows(WindowIndex).Controls(ControlIndexReq).Text = ""
            Next

            Call SetControlResultItem(0)
        End If
    End If

    Success = 100

    Windows(WindowIndex).Controls(GetControlIndex("winCraft", "lblSuccessRate")).Text = "Chance de Sucesso: " & Success & "%"

End Sub

Private Sub SetControlResultItem(ByVal TextureNum As Long)
    Dim i As Long
    Dim ControlIndexItem As Long

    ControlIndexItem = GetControlIndex("winCraft", "picItemResult")

    For i = 0 To entStates.state_Count - 1
        Windows(WindowIndex).Controls(ControlIndexItem).Image(i) = Tex_Item(TextureNum)
    Next
End Sub

Private Function GetInvItemValue(ByVal ItemId As Long, ByVal ItemLevel As Long) As Long
    Dim i As Long
    Dim Count As Long

    For i = 1 To MaxInventories
        If GetInventoryItemNum(i) = ItemId And GetInventoryItemLevel(i) = ItemLevel Then
            Count = Count + GetInventoryItemValue(i)
        End If
    Next

    GetInvItemValue = Count
End Function

Public Sub ButtonMenu_Craft()
    If MyIndex > 0 Then
        If GetPlayerDead(MyIndex) Then Exit Sub
    End If

    ' Evita que a janela seja fechada.
    If IsCraftStarted Then
        AddText "A produção ainda está em progresso.", BrightRed
        Exit Sub
    End If

    If Windows(WindowIndex).Window.Visible Then
        HideWindow WindowIndex
        CanMoveNow = True
    Else
        OpenCraftWindow
    End If

End Sub

Private Sub ResultItem_MouseMove()
    If SelectedRecipeNum > 0 Then
        Dim ItemNum As Long
        Dim ItemLevel As Long
        Dim AttributeId As Long
        Dim UpgradeId As Long

        ItemNum = Recipe(SelectedRecipeNum).RewardItem.Num
        ItemLevel = Recipe(SelectedRecipeNum).RewardItem.Level

        If ItemNum > 0 Then
            Dim X As Long, Y As Long
            Dim Inventory As InventoryRec

            Call SetWinDescriptionPosition(X, Y)

            Inventory.Num = ItemNum
            Inventory.Level = ItemLevel
            Inventory.AttributeId = AttributeId
            Inventory.UpgradeId = UpgradeId

            If Inventory.Num > 0 And Inventory.Num <= MaximumItems Then
                If Item(Inventory.Num).Type = ItemType_Heraldry Then
                    Call ShowHeraldryDescription(X, Y, Inventory, Item(Inventory.Num).Price)
                Else
                    Call ShowItemDesc(X, Y, Inventory)
                End If
            End If

        End If
    End If
End Sub

Private Sub Requirement1_MouseMove()
    Dim X As Long, Y As Long

    Call SetWinDescriptionPosition(X, Y)
    Call ShowRequirementItemDesc(X, Y, 1)
End Sub
Private Sub Requirement2_MouseMove()
    Dim X As Long, Y As Long

    Call SetWinDescriptionPosition(X, Y)
    Call ShowRequirementItemDesc(X, Y, 2)
End Sub
Private Sub Requirement3_MouseMove()
    Dim X As Long, Y As Long

    Call SetWinDescriptionPosition(X, Y)
    Call ShowRequirementItemDesc(X, Y, 3)
End Sub
Private Sub Requirement4_MouseMove()
    Dim X As Long, Y As Long

    Call SetWinDescriptionPosition(X, Y)
    Call ShowRequirementItemDesc(X, Y, 4)
End Sub
Private Sub Requirement5_MouseMove()
    Dim X As Long, Y As Long

    Call SetWinDescriptionPosition(X, Y)
    Call ShowRequirementItemDesc(X, Y, 5)
End Sub

Private Sub SetWinDescriptionPosition(ByRef X As Long, ByRef Y As Long)
    Dim WinDescription As Long
    
    WinDescription = GetWindowIndex("winDescription")
    
    ' calc position
    X = Windows(WindowIndex).Window.Left - Windows(WinDescription).Window.Width - 2
    Y = Windows(WindowIndex).Window.Top

    ' offscreen?
    If X < 0 Then
        ' switch to right
        X = Windows(WindowIndex).Window.Left + Windows(WindowIndex).Window.Width + 2
    End If

    If Y + Windows(WinDescription).Window.Height >= ScreenHeight Then
        Y = ScreenHeight - Windows(WinDescription).Window.Height
    End If

End Sub

Private Sub ShowRequirementItemDesc(ByVal X As Long, ByVal Y As Long, ByVal RequirementIndex As Long)
    Dim ItemNum As Long, ItemLevel As Long

    If GetCraftType() > CraftType.CraftType_None Then
        If SelectedRecipeNum > 0 Then
            ItemNum = Recipe(SelectedRecipeNum).RequiredItem(RequirementIndex).Num

            If ItemNum > 0 Then
                Dim Inventory As InventoryRec
                
                Inventory.Num = ItemNum
                Inventory.Level = Recipe(SelectedRecipeNum).RequiredItem(RequirementIndex).Level

                Call ShowItemDesc(X, Y, Inventory)
            End If
        End If
    End If

End Sub

Public Sub ClearCraftWindow()
    Windows(WindowIndex).Controls(GetControlIndex("winCraft", "lblProgress")).Text = "Processando: 0%"
    Windows(WindowIndex).Controls(GetControlIndex("winCraft", "lblSuccessRate")).Text = "Chance de Sucesso: 0%"
    Windows(WindowIndex).Controls(GetControlIndex("winCraft", "lblExp")).Text = "Nenhum Lv. 0 0/0"
End Sub

Private Sub UpdateProcessText()
    Windows(WindowIndex).Controls(GetControlIndex("winCraft", "lblProgress")).Text = "Processando: " & CraftProgressPercentage & "%"
End Sub

Private Sub PicList1_Click()
    If GetCraftType > CraftType.CraftType_None Then
        If Not IsCraftStarted Then
            SelectedButtonList = 1
            SelectCraftRecipe
        End If
    End If
End Sub
Private Sub PicList2_Click()
    If GetCraftType > CraftType.CraftType_None Then
        If Not IsCraftStarted Then
            SelectedButtonList = 2
            SelectCraftRecipe
        End If
    End If
End Sub
Private Sub PicList3_Click()
    If GetCraftType > CraftType.CraftType_None Then
        If Not IsCraftStarted Then
            SelectedButtonList = 3
            SelectCraftRecipe
        End If
    End If
End Sub
Private Sub PicList4_Click()
    If GetCraftType > CraftType.CraftType_None Then
        If Not IsCraftStarted Then
            SelectedButtonList = 4
            SelectCraftRecipe
        End If
    End If
End Sub
Private Sub PicList5_Click()
    If GetCraftType > CraftType.CraftType_None Then
        If Not IsCraftStarted Then
            SelectedButtonList = 5
            SelectCraftRecipe
        End If
    End If
End Sub
Private Sub PicList6_Click()
    If GetCraftType > CraftType.CraftType_None Then
        If Not IsCraftStarted Then
            SelectedButtonList = 6
            SelectCraftRecipe
        End If
    End If
End Sub
Private Sub PicList7_Click()
    If GetCraftType > CraftType.CraftType_None Then
        If Not IsCraftStarted Then
            SelectedButtonList = 7
            SelectCraftRecipe
        End If
    End If
End Sub
Private Sub PicList8_Click()
    If GetCraftType > CraftType.CraftType_None Then
        If Not IsCraftStarted Then
            SelectedButtonList = 8
            SelectCraftRecipe
        End If
    End If
End Sub
Private Sub PicList9_Click()
    If GetCraftType > CraftType.CraftType_None Then
        If Not IsCraftStarted Then
            SelectedButtonList = 9
            SelectCraftRecipe
        End If
    End If
End Sub

Public Sub ProcessCraftItem()
    If IsCraftStarted Then
        If CraftProgressPercentage = 100 Then
            CraftProgressPercentage = 0
            IsCraftStarted = False
            CanMoveNow = True

            SendCraftProcessCompleted
        End If

        If CraftProgressPercentage < 100 And IsCraftStarted Then
            CraftProgressPercentage = CraftProgressPercentage + CraftProgressStep

            If CraftProgressPercentage > 100 Then
                CraftProgressPercentage = 100
            End If
        End If

        UpdateProcessText
    End If
End Sub

Public Sub StartCraft(ByVal ProgressStep As Long)
' Imobiliza o personagem.

    If Not IsCraftStarted Then

        CanMoveNow = False
        IsCraftStarted = True
        CraftProgressPercentage = 0
        CraftProgressStep = ProgressStep

        AddText ColourChar & GetColStr(ColorType.BrightGreen) & "[Sistema] : " & ColourChar & GetColStr(Grey) & "A produção foi iniciada.", Grey, , ChatChannel.ChannelGame
    End If

End Sub
