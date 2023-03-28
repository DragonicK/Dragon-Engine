Attribute VB_Name = "Chat_Implementation"
Option Explicit

Public Sub DrawChatBubble(ByVal Index As Long)
    Dim theArray() As String, X As Long, Y As Long, i As Long, MaxWidth As Long, x2 As Long, y2 As Long, Colour As Long, tmpNum As Long

    With ChatBubble(Index)
        ' exit out early
        If .Target = 0 Then Exit Sub
        ' calculate position
        Select Case .TargetType
        Case TargetTypePlayer
            ' it's on our map - get co-ords
            X = ConvertMapX((Player(.Target).X * 32) + Player(.Target).xOffset) + 16
            Y = ConvertMapY((Player(.Target).Y * 32) + Player(.Target).yOffset) - 32
        Case Else
            Exit Sub
        End Select

        Colour = ChatBubble(Index).Colour

        ' word wrap
        WordWrap_Array .Msg, ChatBubbleWidth, theArray
        ' find max width
        tmpNum = UBound(theArray)

        For i = 1 To tmpNum
            If TextWidth(Font(Fonts.FontRegular), theArray(i)) > MaxWidth Then MaxWidth = TextWidth(Font(Fonts.FontRegular), theArray(i))
        Next

        ' calculate the new position
        x2 = X - (MaxWidth \ 2)
        y2 = Y - (UBound(theArray) * 12)
        ' render bubble - top left
        RenderTexture Tex_GUI(25), x2 - 9, y2 - 5, 0, 0, 9, 5, 9, 5
        ' top right
        RenderTexture Tex_GUI(25), x2 + MaxWidth, y2 - 5, 119, 0, 9, 5, 9, 5
        ' top
        RenderTexture Tex_GUI(25), x2, y2 - 5, 9, 0, MaxWidth, 5, 5, 5
        ' bottom left
        RenderTexture Tex_GUI(25), x2 - 9, Y, 0, 19, 9, 6, 9, 6
        ' bottom right
        RenderTexture Tex_GUI(25), x2 + MaxWidth, Y, 119, 19, 9, 6, 9, 6
        ' bottom - left half
        RenderTexture Tex_GUI(25), x2, Y, 9, 19, (MaxWidth \ 2) - 5, 6, 9, 6
        ' bottom - right half
        RenderTexture Tex_GUI(25), x2 + (MaxWidth \ 2) + 6, Y, 9, 19, (MaxWidth \ 2) - 5, 6, 9, 6
        ' left
        RenderTexture Tex_GUI(25), x2 - 9, y2, 0, 6, 9, (UBound(theArray) * 12), 9, 1
        ' right
        RenderTexture Tex_GUI(25), x2 + MaxWidth, y2, 119, 6, 9, (UBound(theArray) * 12), 9, 1
        ' center
        RenderTexture Tex_GUI(25), x2, y2, 9, 5, MaxWidth, (UBound(theArray) * 12), 1, 1
        ' little pointy bit
        RenderTexture Tex_GUI(25), X - 5, Y, 58, 19, 11, 11, 11, 11
        ' render each line centralised
        tmpNum = UBound(theArray)

        For i = 1 To tmpNum
            RenderText Font(Fonts.FontRegular), theArray(i), 4 + X - (TextWidth(Font(Fonts.FontRegular), theArray(i)) / 2), y2, Colour
            y2 = y2 + 12
        Next

        ' check if it's timed out - close it if so
        If .Timer + 5000 < GetTickCount Then
            .Active = False
        End If
    End With
End Sub
