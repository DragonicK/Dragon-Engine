Attribute VB_Name = "Conversation_Window"
Option Explicit

Private Const OptionsY As Long = 120
Private Const OptionOffsetY As Long = 28

' Posição do texto principal.
Private Const MainTextX As Long = 30
Private Const MainTextY As Long = 90
Private Const MainTextWidth As Long = 340

' Quantidade máxima de opções na janela.
Private Const MaxOptions As Long = 8

Private WindowIndex As Long
Private TextLineCount As Long

' Página da lista.
Private PageIndex As Long
Private PageCount As Long

Public Sub CreateWindow_Conversation()
' Create the window
    CreateWindow "winConversation", "CONVERSAÇÃO", zOrder_Win, 0, 0, 420, 400, 0, , Fonts.OpenSans_Effect, , 3, 5, DesignTypes.desWin_AincradNorm, DesignTypes.desWin_AincradNorm, DesignTypes.desWin_AincradNorm, , , , , , , , , , , GetAddress(AddressOf RenderConversation)

    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Close button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf HideConversation)

    ' Reply buttons
    CreateButton WindowCount, "btnOpt1", 30, OptionsY + (OptionOffsetY * 0), 340, 22, "[Text]", OpenSans_Effect, White, , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf Button_Option1), , , , , ColorType.Gold
    CreateButton WindowCount, "btnOpt2", 30, OptionsY + (OptionOffsetY * 1), 340, 22, "[Text]", OpenSans_Effect, White, , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf Button_Option2), , , , , ColorType.Gold
    CreateButton WindowCount, "btnOpt3", 30, OptionsY + (OptionOffsetY * 2), 340, 22, "[Text]", OpenSans_Effect, White, , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf Button_Option3), , , , , ColorType.Gold
    CreateButton WindowCount, "btnOpt4", 30, OptionsY + (OptionOffsetY * 3), 340, 22, "[Text]", OpenSans_Effect, White, , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf Button_Option4), , , , , ColorType.Gold
    CreateButton WindowCount, "btnOpt5", 30, OptionsY + (OptionOffsetY * 4), 340, 22, "[Text]", OpenSans_Effect, White, , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf Button_Option5), , , , , ColorType.Gold
    CreateButton WindowCount, "btnOpt6", 30, OptionsY + (OptionOffsetY * 5), 340, 22, "[Text]", OpenSans_Effect, White, , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf Button_Option6), , , , , ColorType.Gold
    CreateButton WindowCount, "btnOpt7", 30, OptionsY + (OptionOffsetY * 6), 340, 22, "[Text]", OpenSans_Effect, White, , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf Button_Option7), , , , , ColorType.Gold
    CreateButton WindowCount, "btnOpt8", 30, OptionsY + (OptionOffsetY * 7), 340, 22, "[Text]", OpenSans_Effect, White, , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf Button_Option8), , , , , ColorType.Gold

    'Arrow
    CreateButton WindowCount, "btnUp", 380, 90, 15, 15, , , , , , , Tex_GUI(110), Tex_GUI(110), Tex_GUI(110), , , , , , GetAddress(AddressOf Button_MoveUp)
    CreateButton WindowCount, "btnDown", 380, 360, 15, 15, , , , , , , Tex_GUI(111), Tex_GUI(111), Tex_GUI(111), , , , , , GetAddress(AddressOf Button_MoveDown)

    'Scroll
    CreateButton WindowCount, "ScrollUp", 382, 100, 9, 130, , , , , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf Button_MoveUp)
    CreateButton WindowCount, "ScrollDown", 382, 230, 9, 130, , , , , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf Button_MoveDown)

    ' Set de WindowIndex variable to avoid search for index.
    WindowIndex = WindowCount

    PageIndex = 1
End Sub

Private Sub Button_Option1()
    Dim Index As Long
    Dim Id As Long

    If Not ConversationView.IsInConversation Then
        Index = ((PageIndex * MaxOptions) - MaxOptions) + 1

        Id = ConversationView.Conversations(Index)

        Call SendConversationOption(Id, 1)
    Else
        Id = ConversationView.ConversationId
        Index = ConversationView.ChatIndex

        If Id > 0 Then
            Call SendConversationOption(Id, Index, 1)
        End If
    End If
End Sub
Private Sub Button_Option2()
    Dim Index As Long
    Dim Id As Long

    If Not ConversationView.IsInConversation Then
        Index = ((PageIndex * MaxOptions) - MaxOptions) + 2

        Id = ConversationView.Conversations(Index)

        Call SendConversationOption(Id, 1)
    Else
        Id = ConversationView.ConversationId
        Index = ConversationView.ChatIndex

        If Id > 0 Then
            Call SendConversationOption(Id, Index, 2)
        End If
    End If
End Sub
Private Sub Button_Option3()
    Dim Index As Long
    Dim Id As Long

    If Not ConversationView.IsInConversation Then
        Index = ((PageIndex * MaxOptions) - MaxOptions) + 3

        Id = ConversationView.Conversations(Index)

        Call SendConversationOption(Id, 1)
    Else
        Id = ConversationView.ConversationId
        Index = ConversationView.ChatIndex

        If Id > 0 Then
            Call SendConversationOption(Id, Index, 3)
        End If

    End If
End Sub
Private Sub Button_Option4()
    Dim Index As Long
    Dim Id As Long

    If Not ConversationView.IsInConversation Then
        Index = ((PageIndex * MaxOptions) - MaxOptions) + 4

        Id = ConversationView.Conversations(Index)

        Call SendConversationOption(Id, 1)
    Else
        Id = ConversationView.ConversationId
        Index = ConversationView.ChatIndex

        If Id > 0 Then
            Call SendConversationOption(Id, Index, 4)
        End If
    End If
End Sub
Private Sub Button_Option5()
    Dim Index As Long
    Dim Id As Long

    If Not ConversationView.IsInConversation Then
        Index = ((PageIndex * MaxOptions) - MaxOptions) + 5

        Id = ConversationView.Conversations(Index)

        Call SendConversationOption(Id, 1)
    Else
        Id = ConversationView.ConversationId
        Index = ConversationView.ChatIndex

        If Id > 0 Then
            Call SendConversationOption(Id, Index, 5)
        End If
    End If
End Sub
Private Sub Button_Option6()
    Dim Index As Long
    Dim Id As Long

    If Not ConversationView.IsInConversation Then
        Index = ((PageIndex * MaxOptions) - MaxOptions) + 6

        Id = ConversationView.Conversations(Index)

        Call SendConversationOption(Id, 1)
    Else
        Id = ConversationView.ConversationId
        Index = ConversationView.ChatIndex

        If Id > 0 Then
            Call SendConversationOption(Id, Index, 6)
        End If
    End If
End Sub
Private Sub Button_Option7()
    Dim Index As Long
    Dim Id As Long

    If Not ConversationView.IsInConversation Then
        Index = ((PageIndex * MaxOptions) - MaxOptions) + 7

        Id = ConversationView.Conversations(Index)

        Call SendConversationOption(Id, 1)
    Else
        Id = ConversationView.ConversationId
        Index = ConversationView.ChatIndex

        If Id > 0 Then
            Call SendConversationOption(Id, Index, 7)
        End If
    End If
End Sub

Private Sub Button_Option8()
    Dim Index As Long
    Dim Id As Long

    If Not ConversationView.IsInConversation Then
        Index = ((PageIndex * MaxOptions) - MaxOptions) + 8

        Id = ConversationView.Conversations(Index)

        Call SendConversationOption(Id, 1)
    Else
        Id = ConversationView.ConversationId
        Index = ConversationView.ChatIndex

        If Id > 0 Then
            Call SendConversationOption(Id, Index, 8)
        End If
    End If
End Sub

Public Function IsConversationVisible() As Boolean
    IsConversationVisible = Windows(WindowIndex).Window.Visible
End Function

Public Sub ShowConversation()
    PageIndex = 1
    Windows(WindowIndex).Window.Visible = True
End Sub

Public Sub HideConversation()
    PageIndex = 1
    Windows(WindowIndex).Window.Visible = False
End Sub

Private Sub Button_MoveUp()
    If Not ConversationView.IsInConversation Then
        If PageIndex > 1 Then
            PageIndex = PageIndex - 1

            Call ListConversations
        End If
    End If
End Sub

Private Sub Button_MoveDown()
    If Not ConversationView.IsInConversation Then
        If PageIndex < PageCount Then
            PageIndex = PageIndex + 1

            Call ListConversations
        End If
    End If
End Sub

Private Sub RenderConversation()
    If Not ConversationView.IsInConversation Then
        Call RenderHeader(GetNpcName(ConversationView.NpcId))
        Call RenderGreetings(ConversationView.NpcId)
    Else
        If ConversationView.ConversationId > 0 Then
            Dim ChatIndex As Long
            Dim ConversationId As Long

            ConversationId = ConversationView.ConversationId
            ChatIndex = ConversationView.ChatIndex

            Call RenderHeader(GetConversationName(ConversationId))
            Call RenderMainText(Conversations(ConversationId).Chats(ChatIndex).Text)
        End If
    End If
End Sub

Public Sub RenderGreetings(ByVal NpcId As Long)
    If NpcId > 0 And NpcId <= MaximumNpcs Then
        Call RenderMainText(Npc(NpcId).Greetings)
    End If
End Sub

Public Sub AllocateGreetingsLineCount(ByVal NpcId As Long)
    If NpcId > 0 And NpcId <= MaximumNpcs Then
        Dim TextArray() As String
        Dim Width As Long

        Width = MainTextWidth

        WordWrap_Array Npc(NpcId).Greetings, Width, TextArray()

        TextLineCount = UBound(TextArray)
    End If
End Sub

Public Sub AllocateMainTextLineCount()
    If ConversationView.ConversationId > 0 Then
        Dim ChatIndex As Long
        Dim ConversationId As Long
        Dim TextArray() As String
        Dim Width As Long
        Dim Text As String

        ConversationId = ConversationView.ConversationId
        ChatIndex = ConversationView.ChatIndex

        If ChatIndex <= Conversations(ConversationId).ChatCount Then

            Text = Conversations(ConversationId).Chats(ChatIndex).Text

            Width = MainTextWidth

            WordWrap_Array Text, Width, TextArray()

            TextLineCount = UBound(TextArray)
        End If
    End If
End Sub

Private Sub RenderHeader(ByRef Text As String)
    Dim xO As Long, yO As Long, Width As Long

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top
    Width = Windows(WindowIndex).Window.Width

    RenderDesign DesignTypes.desWin_AincradMenu, xO, yO + 40, Width, 30

    Width = (Width / 2) - (TextWidth(Font(Fonts.OpenSans_Effect), Text) / 2)

    RenderText Font(Fonts.OpenSans_Effect), Text, xO + Width, yO + 48, White
End Sub

Private Sub RenderMainText(ByRef Text As String)
    If LenB(Text) > 0 Then
        Dim Width As Long, X As Long, Y As Long
        Dim tWidth As Long

        Width = MainTextWidth
        X = Windows(WindowIndex).Window.Left + MainTextX
        Y = Windows(WindowIndex).Window.Top + MainTextY

        tWidth = TextWidth(Font(Fonts.OpenSans_Effect), Text)

        If tWidth > Width Then
            Dim TextArray() As String
            Dim Count As Long
            Dim i As Long, yOffset As Long
            Dim Peido As String

            WordWrap_Array Text, Width, TextArray()

            TextLineCount = UBound(TextArray)

            For i = 1 To TextLineCount
                tWidth = TextWidth(Font(Fonts.OpenSans_Effect), TextArray(i))

                RenderText Font(Fonts.OpenSans_Effect), TextArray(i), X + (Width * 0.5) - (tWidth * 0.5), Y + yOffset, White
                yOffset = yOffset + 14
            Next
        Else
            tWidth = TextWidth(Font(Fonts.OpenSans_Effect), Text)

            RenderText Font(Fonts.OpenSans_Effect), Text, X + (Width * 0.5) - (tWidth * 0.5), Y, White
        End If
    End If

End Sub

Private Function GetNpcName(ByVal NpcId As Long) As String
    If NpcId > 0 And NpcId <= MaximumNpcs Then
        GetNpcName = Npc(NpcId).Name
    End If
End Function

Private Function GetConversationName(ByVal ConversationId As Long) As String
    If ConversationId > 0 And ConversationId <= MaxConversations Then
        GetConversationName = Conversations(ConversationId).Name
    End If
End Function

' Lista todas as conversas.
Public Sub ListConversations()
    If Not ConversationView.IsInConversation Then
        Dim ControlIndex As Long
        Dim Count As Long
        Dim i As Long
        Dim Minimum As Long
        Dim Maximum As Long
        Dim Index As Long
        Dim ConversationId As Long
        Dim ModResult As Long

        Count = ConversationView.Count

        PageCount = Count \ MaxOptions
        ModResult = Count Mod MaxOptions

        If ModResult > 0 Then
            PageCount = PageCount + 1
        End If

        Maximum = PageIndex * MaxOptions
        Minimum = ((PageIndex * MaxOptions) - MaxOptions)

        If Count <= Maximum Then
            Maximum = Count
        End If

        For i = 1 To MaxOptions
            ControlIndex = GetControlIndex("winConversation", "btnOpt" & i)

            If i + Minimum <= Maximum Then
                ConversationId = ConversationView.Conversations(i + Minimum)

                Windows(WindowIndex).Controls(ControlIndex).Text = GetConversationName(ConversationId)

                Windows(WindowIndex).Controls(ControlIndex).Top = 100 + TextLineCount * 15 + (i - 1) * OptionOffsetY

                Windows(WindowIndex).Controls(ControlIndex).Visible = True
            Else
                Windows(WindowIndex).Controls(ControlIndex).Visible = False
            End If
        Next
    End If
End Sub

Public Sub ListConversationOptions()
    Dim i As Long
    Dim Id As Long
    Dim Text As String
    Dim ChatCount As Long
    Dim ChatIndex As Long
    Dim OptionCount As Long
    Dim ControlIndex As Long

    Id = ConversationView.ConversationId
    ChatIndex = ConversationView.ChatIndex

    For i = 1 To MaxOptions
        ControlIndex = GetControlIndex("winConversation", "btnOpt" & i)

        Windows(WindowIndex).Controls(ControlIndex).Top = 130 + TextLineCount * 16 + (i - 1) * OptionOffsetY
        Windows(WindowIndex).Controls(ControlIndex).Visible = False

        If Id > 0 Then
            ChatCount = Conversations(Id).ChatCount

            If ChatCount > 0 And ChatIndex <= ChatCount Then
                OptionCount = Conversations(Id).Chats(ChatIndex).OptionCount

                If i <= OptionCount Then
                    Text = Conversations(Id).Chats(ChatIndex).Reply(i).Text

                    If LenB(Text) > 0 Then
                        Windows(WindowIndex).Controls(ControlIndex).Text = Text
                        Windows(WindowIndex).Controls(ControlIndex).Visible = True
                    End If
                End If
            End If
        End If
    Next

End Sub
