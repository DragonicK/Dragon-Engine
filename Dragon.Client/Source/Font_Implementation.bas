Attribute VB_Name = "Font_Implementation"
Option Explicit

'The size of a FVF vertex
Public Const FVF_Size As Long = 28
Public Const ColourChar As String * 1 = "½"

'Point API
Public Type POINTAPI
    X As Long
    Y As Long
End Type

Private Type CharVA
    Vertex(0 To 3) As Vertex
End Type

Private Type VFH
    BitmapWidth As Long
    BitmapHeight As Long
    CellWidth As Long
    CellHeight As Long
    BaseCharOffset As Byte
    CharWidth(0 To 255) As Byte
    CharVA(0 To 255) As CharVA
End Type

Public Type CustomFont
    HeaderInfo As VFH
    Texture As Direct3DTexture8
    RowPitch As Integer
    RowFactor As Single
    ColFactor As Single
    CharHeight As Byte
    TextureSize As POINTAPI
    xOffset As Long
    yOffset As Long
End Type

' Fonts
Public Enum Fonts
    FontRegular = 1
    OpenSans_Damage

    Fonts_Count
End Enum

' Store the fonts
Public Font() As CustomFont

Public Sub LoadFonts()
    
'Check if we have the device
    If D3DDevice.TestCooperativeLevel <> D3D_OK Then Exit Sub
    ' re-dim the fonts
    ReDim Font(1 To Fonts.Fonts_Count - 1)
    ' load the fonts
    SetFont Fonts.FontRegular, "regular", 256, 2, 2
    SetFont Fonts.OpenSans_Damage, "damage", 256, 2, 2
End Sub

Private Sub SetFont(ByVal FontNum As Long, ByVal texName As String, ByVal Size As Long, Optional ByVal xOffset As Long, Optional ByVal yOffset As Long)
    Dim Data() As Byte, f As Long, W As Long, H As Long, Path As String
    ' set the path
    Path = App.Path & Path_Font & texName & ".png"
    ' load the texture
    f = FreeFile
    Open Path For Binary As #f
    ReDim Data(0 To LOF(f) - 1)
    Get #f, , Data
    Close #f
    
    ' get size
    Font(FontNum).TextureSize.X = ByteToInt(Data(18), Data(19))
    Font(FontNum).TextureSize.Y = ByteToInt(Data(22), Data(23))
    
    ' set to struct
    Set Font(FontNum).Texture = D3DX.CreateTextureFromFileInMemoryEx(D3DDevice, Data(0), AryCount(Data), Font(FontNum).TextureSize.X, Font(FontNum).TextureSize.Y, D3DX_DEFAULT, 0, D3DFMT_A8R8G8B8, D3DPOOL_MANAGED, D3DX_FILTER_POINT, D3DX_FILTER_POINT, 0, ByVal 0, ByVal 0)
    
    Font(FontNum).xOffset = xOffset
    Font(FontNum).yOffset = yOffset
    
    LoadFontHeader Font(FontNum), texName & ".dat"
End Sub

Public Sub SaveFontHeader(ByVal theFont As Fonts, ByVal FileName As String)
    Dim f As Long
    'Save the header information
    f = FreeFile
    Open App.Path & Path_Font & FileName For Binary As #f
    Put #f, , Font(theFont).HeaderInfo
    Close #f
End Sub

Public Sub LoadFontHeader(ByRef theFont As CustomFont, ByVal FileName As String)
    Dim FileNum As Byte
    Dim LoopChar As Long
    Dim Row As Single
    Dim u As Single
    Dim v As Single

    'Load the header information
    FileNum = FreeFile
    Open App.Path & Path_Font & FileName For Binary As #FileNum
    Get #FileNum, , theFont.HeaderInfo
    Close #FileNum

    'Calculate some common values
    theFont.CharHeight = theFont.HeaderInfo.CellHeight - 4
    theFont.RowPitch = theFont.HeaderInfo.BitmapWidth \ theFont.HeaderInfo.CellWidth
    theFont.ColFactor = theFont.HeaderInfo.CellWidth / theFont.HeaderInfo.BitmapWidth
    theFont.RowFactor = theFont.HeaderInfo.CellHeight / theFont.HeaderInfo.BitmapHeight

    'Cache the verticies used to draw the character (only requires setting the color and adding to the X/Y values)
    For LoopChar = 0 To 255
        'tU and tV value (basically tU = BitmapXPosition / BitmapWidth, and height for tV)
        Row = (LoopChar - theFont.HeaderInfo.BaseCharOffset) \ theFont.RowPitch
        u = ((LoopChar - theFont.HeaderInfo.BaseCharOffset) - (Row * theFont.RowPitch)) * theFont.ColFactor
        v = Row * theFont.RowFactor

        'Set the verticies
        With theFont.HeaderInfo.CharVA(LoopChar)
            .Vertex(0).Colour = D3DColorARGB(255, 0, 0, 0)   'Black is the most common color
            .Vertex(0).RHW = 1
            .Vertex(0).tu = u
            .Vertex(0).tv = v
            .Vertex(0).X = 0
            .Vertex(0).Y = 0
            .Vertex(0).Z = 0
            .Vertex(1).Colour = D3DColorARGB(255, 0, 0, 0)
            .Vertex(1).RHW = 1
            .Vertex(1).tu = u + theFont.ColFactor
            .Vertex(1).tv = v
            .Vertex(1).X = theFont.HeaderInfo.CellWidth
            .Vertex(1).Y = 0
            .Vertex(1).Z = 0
            .Vertex(2).Colour = D3DColorARGB(255, 0, 0, 0)
            .Vertex(2).RHW = 1
            .Vertex(2).tu = u
            .Vertex(2).tv = v + theFont.RowFactor
            .Vertex(2).X = 0
            .Vertex(2).Y = theFont.HeaderInfo.CellHeight
            .Vertex(2).Z = 0
            .Vertex(3).Colour = D3DColorARGB(255, 0, 0, 0)
            .Vertex(3).RHW = 1
            .Vertex(3).tu = u + theFont.ColFactor
            .Vertex(3).tv = v + theFont.RowFactor
            .Vertex(3).X = theFont.HeaderInfo.CellWidth
            .Vertex(3).Y = theFont.HeaderInfo.CellHeight
            .Vertex(3).Z = 0
        End With
    Next LoopChar
End Sub

Public Function TextWidth(ByRef UseFont As CustomFont, ByVal Text As String) As Long
    Dim LoopI As Integer, tmpNum As Long, skipCount As Long

    'Make sure we have text
    If LenB(Text) = 0 Then Exit Function

    'Loop through the text
    tmpNum = Len(Text)
    For LoopI = 1 To tmpNum    'textwidth bug - 1?
        If Mid$(Text, LoopI, 1) = ColourChar Then skipCount = 3

        If skipCount > 0 Then
            skipCount = skipCount - 1
        Else
            TextWidth = TextWidth + UseFont.HeaderInfo.CharWidth(Asc(Mid$(Text, LoopI, 1)))
        End If
    Next LoopI
End Function

Public Function TextHeight(ByRef UseFont As CustomFont) As Long
    TextHeight = UseFont.HeaderInfo.CellHeight
End Function

Public Sub RenderText(ByRef UseFont As CustomFont, ByVal Text As String, ByVal X As Long, ByVal Y As Long, ByVal Color As Long, Optional ByVal Alpha As Long = 255, Optional Shadow As Boolean = True)
    Dim TempVA(0 To 3) As Vertex, TempStr() As String, Count As Long, Ascii() As Byte, i As Long, J As Long, TempColor As Long, yOffset As Single, ignoreChar As Long, resetColor As Long
    Dim tmpNum As Long ' LineCount As Long

    ' set the color
    Color = DX8Colour(Color, Alpha)

    'Check for valid text to render
    If LenB(Text) = 0 Then Exit Sub
    'Get the text into arrays (split by vbCrLf)
    TempStr = Split(Text, vbCrLf)
    'Set the temp color (or else the first character has no color)
    TempColor = Color
    resetColor = TempColor
    'Set the texture
    D3DDevice.SetTexture 0, UseFont.Texture
    CurrentTexture = -1
    ' set the position
    X = X - UseFont.xOffset
    Y = Y - UseFont.yOffset
    
    'Loop through each line if there are line breaks (vbCrLf)
    tmpNum = UBound(TempStr)
   ' LineCount = tmpNum
    
    For i = 0 To tmpNum
        If Len(TempStr(i)) > 0 Then
            yOffset = (i * UseFont.CharHeight) + (i * 3)
            Count = 0
            'Convert the characters to the ascii value
            Ascii() = StrConv(TempStr(i), vbFromUnicode, LOCALE_BRAZIL)
            'Loop through the characters
            tmpNum = Len(TempStr(i))

            For J = 1 To tmpNum
                ' check for colour change
                If Mid$(TempStr(i), J, 1) = ColourChar Then
                    Color = Val(Mid$(TempStr(i), J + 1, 2))
                    ' make sure the colour exists
                    If Color = -1 Then
                        TempColor = resetColor
                    Else
                        TempColor = DX8Colour(Color, Alpha)
                    End If

                    ignoreChar = 3
                End If

                ' check if we're ignoring this character
                If ignoreChar > 0 Then
                    ignoreChar = ignoreChar - 1
                Else
                    'Copy from the cached vertex array to the temp vertex array
                    Call CopyMemory(TempVA(0), UseFont.HeaderInfo.CharVA(Ascii(J - 1)).Vertex(0), FVF_Size * 4)

                    'Set up the verticies
                    TempVA(0).X = X + Count
                    TempVA(0).Y = Y + yOffset
                    TempVA(1).X = TempVA(1).X + X + Count
                    TempVA(1).Y = TempVA(0).Y
                    TempVA(2).X = TempVA(0).X
                    TempVA(2).Y = TempVA(2).Y + TempVA(0).Y
                    TempVA(3).X = TempVA(1).X
                    TempVA(3).Y = TempVA(2).Y

                    'Set the colors
                    TempVA(0).Colour = TempColor
                    TempVA(1).Colour = TempColor
                    TempVA(2).Colour = TempColor
                    TempVA(3).Colour = TempColor

                    'Draw the verticies
                    Call D3DDevice.DrawPrimitiveUP(D3DPT_TRIANGLESTRIP, 2, TempVA(0), FVF_Size)

                    'Shift over the the position to render the next character
                    Count = Count + UseFont.HeaderInfo.CharWidth(Ascii(J - 1))
                End If
            Next J
        End If
    Next i
   
End Sub

Public Sub WordWrap_Array(ByVal Text As String, ByVal MaxLineLen As Long, ByRef theArray() As String)
    Dim LineCount As Long, i As Long, Size As Long, lastSpace As Long, b As Long, tmpNum As Long

    'Too small of text
    If Len(Text) < 2 Then
        ReDim theArray(1 To 1) As String
        theArray(1) = Text
        Exit Sub
    End If

    ' default values
    b = 1
    lastSpace = 1
    Size = 0
    tmpNum = Len(Text)

    For i = 1 To tmpNum

        ' if it's a space, store it
        Select Case Mid$(Text, i, 1)
        Case " ": lastSpace = i
        End Select

        'Add up the size
        Size = Size + Font(Fonts.FontRegular).HeaderInfo.CharWidth(Asc(Mid$(Text, i, 1)))

        'Check for too large of a size
        If Size > MaxLineLen Then
            'Check if the last space was too far back
            If i - lastSpace > 12 Then
                'Too far away to the last space, so break at the last character
                LineCount = LineCount + 1
                ReDim Preserve theArray(1 To LineCount) As String
                theArray(LineCount) = Trim$(Mid$(Text, b, (i - 1) - b))
                b = i - 1
                Size = 0
            Else
                'Break at the last space to preserve the word
                LineCount = LineCount + 1
                ReDim Preserve theArray(1 To LineCount) As String
                theArray(LineCount) = Trim$(Mid$(Text, b, lastSpace - b))
                b = lastSpace + 1
                'Count all the words we ignored (the ones that weren't printed, but are before "i")
                Size = TextWidth(Font(Fonts.FontRegular), Mid$(Text, lastSpace, i - lastSpace))
            End If
        End If

        ' Remainder
        If i = Len(Text) Then
            If b <> i Then
                LineCount = LineCount + 1
                ReDim Preserve theArray(1 To LineCount) As String
                theArray(LineCount) = theArray(LineCount) & Mid$(Text, b, i)
            End If
        End If
    Next
End Sub

Public Function WordWrap(theFont As CustomFont, ByVal Text As String, ByVal MaxLineLen As Integer, Optional ByRef LineCount As Long) As String
    Dim TempSplit() As String, TSLoop As Long, lastSpace As Long, Size As Long, i As Long, b As Long, tmpNum As Long, skipCount As Long

    'Too small of text
    If Len(Text) < 2 Then
        WordWrap = Text
        Exit Function
    End If

    'Check if there are any line breaks - if so, we will support them
    TempSplit = Split(Text, vbNewLine)
    tmpNum = UBound(TempSplit)

    For TSLoop = 0 To tmpNum
        'Clear the values for the new line
        Size = 0
        b = 1
        lastSpace = 1

        'Add back in the vbNewLines
        If TSLoop < UBound(TempSplit()) Then TempSplit(TSLoop) = TempSplit(TSLoop) & vbNewLine

        'Only check lines with a space
        If InStr(1, TempSplit(TSLoop), " ") Then
            'Loop through all the characters
            tmpNum = Len(TempSplit(TSLoop))

            For i = 1 To tmpNum
                'If it is a space, store it so we can easily break at it
                Select Case Mid$(TempSplit(TSLoop), i, 1)
                Case " "
                    lastSpace = i
                Case ColourChar
                    skipCount = 3
                End Select

                If skipCount > 0 Then
                    skipCount = skipCount - 1
                Else
                    'Add up the size
                    Size = Size + theFont.HeaderInfo.CharWidth(Asc(Mid$(TempSplit(TSLoop), i, 1)))
                    'Check for too large of a size
                    If Size > MaxLineLen Then
                        'Check if the last space was too far back
                        If i - lastSpace > 12 Then
                            'Too far away to the last space, so break at the last character
                            WordWrap = WordWrap & Trim$(Mid$(TempSplit(TSLoop), b, (i - 1) - b)) & vbNewLine
                            LineCount = LineCount + 1
                            b = i - 1
                            Size = 0
                        Else
                            'Break at the last space to preserve the word
                            WordWrap = WordWrap & Trim$(Mid$(TempSplit(TSLoop), b, lastSpace - b)) & vbNewLine
                            LineCount = LineCount + 1
                            b = lastSpace + 1
                            'Count all the words we ignored (the ones that weren't printed, but are before "i")
                            Size = TextWidth(theFont, Mid$(TempSplit(TSLoop), lastSpace, i - lastSpace))
                        End If
                    End If

                    'This handles the remainder
                    If i = Len(TempSplit(TSLoop)) Then
                        If b <> i Then
                            WordWrap = WordWrap & Mid$(TempSplit(TSLoop), b, i)
                            LineCount = LineCount + 1
                        End If
                    End If
                End If
            Next i
        Else
            WordWrap = WordWrap & TempSplit(TSLoop)
        End If
    Next TSLoop
End Function

Function GetColStr(Colour As Long)
    If Colour < 10 Then
        GetColStr = "0" & Colour
    Else
        GetColStr = Colour
    End If
End Function

