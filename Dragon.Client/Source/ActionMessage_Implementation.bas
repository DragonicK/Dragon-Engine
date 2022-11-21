Attribute VB_Name = "ActionMessage_Implementation"
Option Explicit

Public Sub DrawActionMsg(ByVal Index As Integer)
    Dim X As Long, Y As Long, i As Long
    Dim LenMsg As Long

    If ActionMsg(Index).Message = vbNullString Then Exit Sub

    ' how long we want each message to appear
    Select Case ActionMsg(Index).Type

    Case ACTIONMsgSTATIC
        If ActionMsg(Index).FontType = ACTION_MSG_FONT_DAMAGE Then
            LenMsg = TextWidth(Font(Fonts.OpenSans_Damage), Trim$(ActionMsg(Index).Message))
        ElseIf ActionMsg(Index).FontType = ACTION_MSG_FONT_ALPHABET Then
            LenMsg = TextWidth(Font(Fonts.OpenSans_Effect), Trim$(ActionMsg(Index).Message))
        End If

        If ActionMsg(Index).Y > 0 Then
            X = ActionMsg(Index).X + Int(PIC_X \ 2) - (LenMsg / 2)
            Y = ActionMsg(Index).Y + PIC_Y
        Else
            X = ActionMsg(Index).X + Int(PIC_X \ 2) - (LenMsg / 2)
            Y = ActionMsg(Index).Y - Int(PIC_Y \ 2) + 18
        End If

    Case ACTIONMsgSCROLL
        If ActionMsg(Index).Y > 0 Then
            X = ActionMsg(Index).X + Int(PIC_X \ 2) - ((Len(Trim$(ActionMsg(Index).Message)) \ 2) * 8)
            Y = ActionMsg(Index).Y - Int(PIC_Y \ 2) - 2 - (ActionMsg(Index).Scroll * 0.7)
            ActionMsg(Index).Scroll = ActionMsg(Index).Scroll + 1
        Else
            X = ActionMsg(Index).X + Int(PIC_X \ 2) - ((Len(Trim$(ActionMsg(Index).Message)) \ 2) * 8)
            Y = ActionMsg(Index).Y - Int(PIC_Y \ 2) + 18 + (ActionMsg(Index).Scroll * 0.001)
            ActionMsg(Index).Scroll = ActionMsg(Index).Scroll + 1
        End If

        ActionMsg(Index).Alpha = ActionMsg(Index).Alpha - 2

        If ActionMsg(Index).Alpha <= 0 Then ClearActionMsg Index: Exit Sub

    Case ACTIONMsgSCREEN
        ' This will kill any action screen messages that there in the system
        For i = MAX_BYTE To 1 Step -1

            If ActionMsg(i).Type = ACTIONMsgSCREEN Then
                If i <> Index Then
                    ClearActionMsg Index
                    Index = i
                End If
            End If

        Next

        If ActionMsg(Index).FontType = ACTION_MSG_FONT_DAMAGE Then
            X = (400) - ((TextWidth(Font(Fonts.OpenSans_Damage), Trim$(ActionMsg(Index).Message)) \ 2))
        ElseIf ActionMsg(Index).FontType = ACTION_MSG_FONT_ALPHABET Then
            X = (400) - ((TextWidth(Font(Fonts.OpenSans_Effect), Trim$(ActionMsg(Index).Message)) \ 2))
        End If

        Y = 24
    End Select

    X = ConvertMapX(X)
    Y = ConvertMapY(Y)

    If ActionMsg(Index).Created > 0 Then
        If ActionMsg(Index).FontType = ACTION_MSG_FONT_DAMAGE Then
            RenderText Font(Fonts.OpenSans_Damage), ActionMsg(Index).Message, X, Y, ActionMsg(Index).Color, ActionMsg(Index).Alpha
        ElseIf ActionMsg(Index).FontType = ACTION_MSG_FONT_ALPHABET Then
            RenderText Font(Fonts.OpenSans_Effect), ActionMsg(Index).Message, X, Y, ActionMsg(Index).Color, ActionMsg(Index).Alpha
        End If
    End If
End Sub

Public Sub DrawPlayerName(ByVal Index As Long)
    Dim TextX As Long, TextY As Long, Text As String, TextSize As Long, Colour As Long
    Dim i As Long

    Text = Trim$(GetPlayerName(Index))
    TextSize = TextWidth(Font(Fonts.OpenSans_Effect), Text)
    Colour = White

    If Index = MyIndex Then
        If Party.Leader > 0 Then
            Colour = Blue
        End If
    Else
        Colour = White

        If Party.Leader > 0 Then
            For i = 1 To Party.MemberCount
                If Party.Member(i).Name = GetPlayerName(Index) Then
                    Colour = Blue
                    Exit For
                End If
            Next
        End If
    End If

    If GetPlayerPK(Index) > 0 Then Colour = BrightRed

    TextX = Player(Index).X * PIC_X + Player(Index).xOffset + (PIC_X \ 2) - (TextSize \ 2) - 3
    TextY = Player(Index).Y * PIC_Y + Player(Index).yOffset - 32

    Call RenderText(Font(Fonts.OpenSans_Effect), Text, ConvertMapX(TextX), ConvertMapY(TextY), Colour)

    If GetPlayerTitle(Index) > 0 And GetPlayerTitle(Index) <= MaximumTitles Then
        Text = Trim$(Title(GetPlayerTitle(Index)).Name)
        TextSize = TextWidth(Font(Fonts.OpenSans_Effect), Text)

        Colour = GetRarityColor(Title(GetPlayerTitle(Index)).Rarity)

        TextX = Player(Index).X * PIC_X + Player(Index).xOffset + (PIC_X \ 2) - (TextSize \ 2)
        TextY = Player(Index).Y * PIC_Y + Player(Index).yOffset - 45

        Call RenderText(Font(Fonts.OpenSans_Effect), Text, ConvertMapX(TextX), ConvertMapY(TextY), Colour)
    End If

End Sub

Public Sub DrawNpcName(ByVal Index As Long)
    If MapNpc(Index).Vital(HP) <= 0 Then
        Exit Sub
    End If

    Dim TextX As Long, TextY As Long, Text As String, TextSize As Long, NpcNum As Long, Colour As Long
    NpcNum = MapNpc(Index).Num
    Text = Npc(NpcNum).Name

    TextSize = TextWidth(Font(Fonts.OpenSans_Effect), Text)

    If Npc(NpcNum).Behaviour = NPC_BEHAVIOUR_MONSTER Or Npc(NpcNum).Behaviour = NPC_BEHAVIOUR_BOSS Then
        ' get the colour
        If Npc(NpcNum).Level > GetPlayerLevel(MyIndex) Then
            Colour = BrightRed
        End If

        If Npc(NpcNum).Level = GetPlayerLevel(MyIndex) Then
            Colour = Gold
        End If

        If Npc(NpcNum).Level < GetPlayerLevel(MyIndex) Then
            Colour = Grey
        End If
    Else
        Colour = White
    End If

    TextX = MapNpc(Index).X * PIC_X + MapNpc(Index).xOffset + (PIC_X \ 2) - (TextSize \ 2) - 3
    TextY = MapNpc(Index).Y * PIC_Y + MapNpc(Index).yOffset - 32

    Call RenderText(Font(Fonts.OpenSans_Effect), Text, ConvertMapX(TextX), ConvertMapY(TextY), Colour)

End Sub


