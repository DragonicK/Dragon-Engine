Attribute VB_Name = "ActionMessage_Implementation"
Option Explicit

Private Const ACTION_MSG_TIME As Long = 45

Public Sub CreateActionMsg(ByVal Message As String, ByVal Color As Integer, ByVal MsgType As Byte, ByVal FontType As Byte, ByVal X As Long, ByVal Y As Long)
    Dim i As Long

    ActionMsgIndex = ActionMsgIndex + 1

    If ActionMsgIndex >= MAX_BYTE Then ActionMsgIndex = 1

    With ActionMsg(ActionMsgIndex)
        .Message = Message
        .Color = Color
        .Type = MsgType
        .Created = GetTickCount
        .Scroll = 1
        .X = X
        .Y = Y
        .FontType = FontType
        .Alpha = 255
        .TickCount = GetTickCount + ACTION_MSG_TIME

        If .Type = ACTIONMsgSCROLL Then
            .Y = .Y + Rand(-2, 6)
            .X = .X + Rand(-12, 12)
        End If
    End With

    ' find the new high index
    For i = MAX_BYTE To 1 Step -1
        If ActionMsg(i).Created > 0 Then
            Action_HighIndex = i + 1
            Exit For
        End If
    Next

    ' make sure we don't overflow
    If Action_HighIndex > MAX_BYTE Then Action_HighIndex = MAX_BYTE
End Sub

Public Sub ClearActionMsg(ByVal Index As Byte)
    Dim i As Long

    With ActionMsg(Index)
        .Message = vbNullString
        .Created = 0
        .Type = 0
        .Color = 0
        .Scroll = 0
        .X = 0
        .Y = 0
        .FontType = 0
        .TickCount = 0
    End With

    ' find the new high index
    For i = MAX_BYTE To 1 Step -1
        If ActionMsg(i).Created > 0 Then
            Action_HighIndex = i + 1
            Exit For
        End If
    Next

    ' make sure we don't overflow
    If Action_HighIndex > MAX_BYTE Then Action_HighIndex = MAX_BYTE
End Sub

Public Sub DrawActionMsg(ByVal Index As Integer)
    Dim X As Long, Y As Long, i As Long
    Dim LenMsg As Long

    With ActionMsg(Index)
        If .Message = vbNullString Then Exit Sub

        If GetTickCount >= .TickCount Then
            .TickCount = GetTickCount + ACTION_MSG_TIME

            Select Case .Type

            Case ACTIONMsgSTATIC
                If .FontType = ACTION_MSG_FONT_DAMAGE Then
                    LenMsg = TextWidth(Font(Fonts.OpenSans_Damage), Trim$(.Message))
                ElseIf ActionMsg(Index).FontType = ACTION_MSG_FONT_ALPHABET Then
                    LenMsg = TextWidth(Font(Fonts.FontRegular), Trim$(.Message))
                End If

                If .Y > 0 Then
                    X = .X + Int(PIC_X \ 2) - (LenMsg / 2)
                    Y = .Y + PIC_Y
                Else
                    X = .X + Int(PIC_X \ 2) - (LenMsg / 2)
                    Y = .Y - Int(PIC_Y \ 2) + 18
                End If

                .X = X
                .Y = Y

            Case ACTIONMsgSCROLL
                Y = .Y - Int(PIC_Y \ 2) + 13 + (.Scroll * 0.001)
                .Scroll = .Scroll + 1

                .Y = Y

                .Alpha = .Alpha - 7

                If .Alpha <= 0 Then
                    ClearActionMsg (Index)
                    Exit Sub
                End If

            End Select
        End If

        X = ConvertMapX(.X)
        Y = ConvertMapY(.Y)

        If .Created > 0 Then
            If .FontType = ACTION_MSG_FONT_DAMAGE Then
                RenderText Font(Fonts.OpenSans_Damage), .Message, X, Y, .Color, .Alpha
            ElseIf ActionMsg(Index).FontType = ACTION_MSG_FONT_ALPHABET Then
                RenderText Font(Fonts.FontRegular), .Message, X, Y, .Color, .Alpha
            End If
        End If

    End With

End Sub

