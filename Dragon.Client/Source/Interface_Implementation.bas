Attribute VB_Name = "Interface_Implementation"
Option Explicit

' Entity Types
Public Enum EntityTypes
    entLabel = 1
    entWindow
    entButton
    entTextBox
    entScrollbar
    entPictureBox
    entCheckbox
    entCombobox
    entCombomenu
End Enum

' Design Types
Public Enum DesignTypes
    ' Boxes
    desWood = 1
    desWood_Small
    desWood_Empty
    desGreen
    desGreen_Hover
    desGreen_Click
    desRed
    desRed_Hover
    desRed_Click
    desBlue
    desBlue_Hover
    desBlue_Click
    desOrange
    desOrange_Hover
    desOrange_Click
    desGrey
    desDescPic
    desSteel
    desSteel_Hover
    desSteel_Click

    ' Windows
    desWin_Black
    desWin_Norm
    desWin_NoBar
    desWin_Empty
    desWin_Desc
    desWin_Shadow
    desWin_Party
    desWin_AincradNorm
    desWin_AincradHeader
    desWin_AincradNoBar    'janela sem barra
    desWin_AincradMenu

    ' Design Padrão nova interface
    desWin_Title
    desWin_Menu
    desWin_Window
    desWin_WindowNoBar
    desButton
    desButton_Hover
    desButton_Click
    desText
    desText_Hover
    desCheck

    ' Pictures
    desParchment
    desBlackOval
    ' Textboxes
    desTextBlack
    desTextWhite
    desTextBlack_Sq
    desTextAincrad
    ' Checkboxes
    desChkNorm
    desChkChat
    desChkCustom_Buying
    desChkCustom_Selling
    ' Right-click Menu
    desMenuHeader
    desMenuOption
    ' Comboboxes
    desComboNorm
    desComboMenuNorm
    ' tile Selection
    desTileBox

    desEmerald
    desEmerald_Hover
    desEmerald_Click


End Enum

' Button States
Public Enum entStates
    Normal = 0
    Hover
    MouseDown
    MouseMove
    MouseUp
    DblClick
    Enter
    KeyPress
    ' Count
    state_Count
End Enum

' Alignment
Public Enum Alignment
    alignLeft = 0
    alignRight
    alignCentre
End Enum

' Part Types
Public Enum PartType
    part_None = 0
    Part_Item
    Part_spell
End Enum

' Origins
Public Enum PartTypeOrigins
    origin_None = 0
    origin_Inventory
    origin_QuickSlot
    origin_Spells
    origin_Enchant
    origin_Warehouse
    origin_Craft
    origin_Equipment
    origin_Heraldry
    origin_Upgrade
End Enum

' Entity UDT
Public Type EntityRec
    ' constants
    Name As String
    ' values
Type As Byte
    Top As Long
    Left As Long
    Width As Long
    Height As Long
    Multiline As Boolean
    Enabled As Boolean
    Visible As Boolean
    canDrag As Boolean
    Max As Long
    Min As Long
    Value As Long
    Text As String
    image(0 To entStates.state_Count - 1) As Long
    design(0 To entStates.state_Count - 1) As Long
    entCallBack(0 To entStates.state_Count - 1) As Long
    Alpha As Long
    clickThrough As Boolean
    xOffset As Long
    yOffset As Long
    align As Byte
    Font As Long
    textColour As Long
    textColour_Hover As Long
    textColour_Click As Long
    zChange As Byte
    onDraw As Long
    origLeft As Long
    origTop As Long
    tooltip As String
    group As Long
    list() As String
    Activated As Boolean
    linkedToWin As Long
    linkedToCon As Long
    TextLimit As Long
    AcceptOnlyNumbers As Boolean
    ' window
    Icon As Long
    ' textbox
    isCensor As Boolean
    ' temp
    State As entStates
    movedX As Long
    movedY As Long
    zOrder As Long
End Type

' For small parts
Public Type EntityPartRec
    Type As PartType
    Origin As PartTypeOrigins
    Value As Long
    Slot As Long
End Type

' Window UDT
Public Type WindowRec
    Window As EntityRec
    Controls() As EntityRec
    ControlCount As Long
    activeControl As Long
End Type

' actual GUI
Public Windows() As WindowRec
Public WindowCount As Long
Public activeWindow As Long
' GUI parts
Public DragBox As EntityPartRec
' Used for automatically the zOrder
Public zOrder_Win As Long
Public zOrder_Con As Long

Public Sub CreateEntity(winNum As Long, zOrder As Long, Name As String, tType As EntityTypes, ByRef design() As Long, ByRef image() As Long, ByRef entCallBack() As Long, _
                        Optional Left As Long, Optional Top As Long, Optional Width As Long, Optional Height As Long, Optional Visible As Boolean = True, Optional canDrag As Boolean, Optional Max As Long, _
                        Optional Min As Long, Optional Value As Long, Optional Text As String, Optional align As Byte, Optional Font As Long = Fonts.OpenSans_Regular, Optional textColour As Long = White, _
                        Optional Alpha As Long = 255, Optional clickThrough As Boolean, Optional xOffset As Long, Optional yOffset As Long, Optional zChange As Byte, Optional ByVal Icon As Long, _
                        Optional ByVal onDraw As Long, Optional isActive As Boolean, Optional isCensor As Boolean, Optional textColour_Hover As Long, Optional textColour_Click As Long, _
                        Optional tooltip As String, Optional group As Long, Optional Multiline As Boolean)
    Dim i As Long

    ' check if it's a legal number
    If winNum <= 0 Or winNum > WindowCount Then
        Exit Sub
    End If

    ' re-dim the control array
    With Windows(winNum)
        .ControlCount = .ControlCount + 1
        ReDim Preserve .Controls(1 To .ControlCount) As EntityRec
    End With

    ' Set the new control values
    With Windows(winNum).Controls(Windows(winNum).ControlCount)
        .Name = Name
        .Type = tType

        ' loop through states
        For i = 0 To entStates.state_Count - 1
            .design(i) = design(i)
            .image(i) = image(i)
            .entCallBack(i) = entCallBack(i)
        Next
        
        .Multiline = Multiline
        .Left = Left
        .Top = Top
        .origLeft = Left
        .origTop = Top
        .Width = Width
        .Height = Height
        .Visible = Visible
        .canDrag = canDrag
        .Max = Max
        .Min = Min
        .Value = Value
        .Text = Text
        .align = align
        .Font = Font
        .textColour = textColour
        .textColour_Hover = textColour_Hover
        .textColour_Click = textColour_Click
        .Alpha = Alpha
        .clickThrough = clickThrough
        .xOffset = xOffset
        .yOffset = yOffset
        .zChange = zChange
        .zOrder = zOrder
        .Enabled = True
        .Icon = Icon
        .onDraw = onDraw
        .isCensor = isCensor
        .tooltip = tooltip
        .group = group
        .TextLimit = 255
        ReDim .list(0 To 0) As String
    End With

    ' set the active control
    If isActive Then Windows(winNum).activeControl = Windows(winNum).ControlCount

    ' set the zOrder
    zOrder_Con = zOrder_Con + 1
End Sub

Public Sub UpdateZOrder(winNum As Long, Optional Forced As Boolean = False)
    Dim i As Long
    Dim oldZOrder As Long

    With Windows(winNum).Window

        If Not Forced Then If .zChange = 0 Then Exit Sub
        If .zOrder = WindowCount Then Exit Sub
        oldZOrder = .zOrder

        For i = 1 To WindowCount

            If Windows(i).Window.zOrder > oldZOrder Then
                Windows(i).Window.zOrder = Windows(i).Window.zOrder - 1
            End If

        Next

        .zOrder = WindowCount
    End With

End Sub

Public Sub SortWindows()
    Dim tempWindow As WindowRec
    Dim i As Long, X As Long
    X = 1

    While X <> 0
        X = 0

        For i = 1 To WindowCount - 1

            If Windows(i).Window.zOrder > Windows(i + 1).Window.zOrder Then
                tempWindow = Windows(i)
                Windows(i) = Windows(i + 1)
                Windows(i + 1) = tempWindow
                X = 1
            End If

        Next

    Wend

End Sub

Public Sub RenderEntities()
    Dim i As Long, X As Long, curZOrder As Long

    ' don't render anything if we don't have any containers
    If WindowCount = 0 Then Exit Sub
    ' reset zOrder
    curZOrder = 1

    ' loop through windows
    Do While curZOrder <= WindowCount
        For i = 1 To WindowCount
            If curZOrder = Windows(i).Window.zOrder Then
                ' increment
                curZOrder = curZOrder + 1
                ' make sure it's visible
                If Windows(i).Window.Visible Then
                    ' render container
                    RenderWindow i

                    ' render controls
                    For X = 1 To Windows(i).ControlCount
                        If Windows(i).Controls(X).Visible Then RenderEntity i, X
                    Next
                End If
            End If
        Next
    Loop
End Sub

Public Sub RenderEntity(winNum As Long, entNum As Long)
    Dim xO As Long, yO As Long, hor_centre As Long, ver_centre As Long, Height As Long, Width As Long, Left As Long, texNum As Long, xOffset As Long
    Dim callBack As Long, taddText As String, Colour As Long, TextArray() As String, Count As Long, yOffset As Long, i As Long, Y As Long, X As Long

    ' check if the window exists
    If winNum <= 0 Or winNum > WindowCount Then
        Exit Sub
    End If

    ' check if the entity exists
    If entNum <= 0 Or entNum > Windows(winNum).ControlCount Then
        Exit Sub
    End If

    ' check the container's position
    xO = Windows(winNum).Window.Left
    yO = Windows(winNum).Window.Top

    With Windows(winNum).Controls(entNum)

        ' find the control type
        Select Case .Type
            ' picture box
        Case EntityTypes.entPictureBox
            ' render specific designs
            If .design(.State) > 0 Then RenderDesign .design(.State), .Left + xO, .Top + yO, .Width, .Height, .Alpha
            ' render image
            If .image(.State) > 0 Then RenderTexture .image(.State), .Left + xO, .Top + yO, 0, 0, .Width, .Height, .Width, .Height, DX8Colour(White, .Alpha)

            ' textbox
        Case EntityTypes.entTextBox
            ' render specific designs
            If .design(.State) > 0 Then RenderDesign .design(.State), .Left + xO, .Top + yO, .Width, .Height, .Alpha
            ' render image
            If .image(.State) > 0 Then RenderTexture .image(.State), .Left + xO, .Top + yO, 0, 0, .Width, .Height, .Width, .Height, DX8Colour(White, .Alpha)
            ' render text
            If activeWindow = winNum And Windows(winNum).activeControl = entNum Then taddText = chatShowLine

            If .Multiline Then
                If .align = Alignment.alignLeft Then
                    ' check if need to word wrap
                    If TextWidth(Font(.Font), .Text) > .Width Then
                        ' wrap text
                        WordWrap_Array .Text, .Width, TextArray()
                        ' render text
                        Count = UBound(TextArray)
                        For i = 1 To Count

                            If i = Count Then
                                RenderText Font(.Font), TextArray(i) & taddText, .Left + xO, .Top + yO + yOffset, .textColour, .Alpha
                            Else
                                RenderText Font(.Font), TextArray(i), .Left + xO, .Top + yO + yOffset, .textColour, .Alpha
                            End If

                            yOffset = yOffset + 14
                        Next
                    Else
                        ' just one line
                        RenderText Font(.Font), .Text & taddText, .Left + xO, .Top + yO, .textColour, .Alpha
                    End If
                End If

                If .align = Alignment.alignRight Then
                    ' check if need to word wrap
                    If TextWidth(Font(.Font), .Text) > .Width Then
                        ' wrap text
                        WordWrap_Array .Text, .Width, TextArray()
                        ' render text
                        Count = UBound(TextArray)
                        For i = 1 To Count
                            Left = .Left + .Width - TextWidth(Font(.Font), TextArray(i))

                            If i = Count Then
                                RenderText Font(.Font), TextArray(i) & taddText, Left + xO, .Top + yO + yOffset, .textColour, .Alpha
                            Else
                                RenderText Font(.Font), TextArray(i), Left + xO, .Top + yO + yOffset, .textColour, .Alpha
                            End If

                            yOffset = yOffset + 14
                        Next
                    Else
                        ' just one line
                        Left = .Left + .Width - TextWidth(Font(.Font), .Text)
                        RenderText Font(.Font), .Text & taddText, Left + xO, .Top + yO, .textColour, .Alpha
                    End If
                End If

                If .align = Alignment.alignCentre Then
                    ' check if need to word wrap
                    If TextWidth(Font(.Font), .Text) > .Width Then
                        ' wrap text
                        WordWrap_Array .Text, .Width, TextArray()
                        ' render text
                        Count = UBound(TextArray)
                        For i = 1 To Count
                            Left = .Left + (.Width \ 2) - (TextWidth(Font(.Font), TextArray(i)) \ 2)

                            If i = Count Then
                                RenderText Font(.Font), TextArray(i) & taddText, Left + xO, .Top + yO + yOffset, .textColour, .Alpha
                            Else
                                RenderText Font(.Font), TextArray(i), Left + xO, .Top + yO + yOffset, .textColour, .Alpha
                            End If

                            yOffset = yOffset + 14
                        Next
                    Else
                        ' just one line
                        Left = .Left + (.Width \ 2) - (TextWidth(Font(.Font), .Text) \ 2)
                        RenderText Font(.Font), .Text & taddText, Left + xO, .Top + yO, .textColour, .Alpha
                    End If
                End If
            End If

            If Not .Multiline Then
                If .align = Alignment.alignCentre Then
                    ' calculate the horizontal centre
                    Width = TextWidth(Font(.Font), .Text)

                    If Width > .Width Then
                        hor_centre = .Left + xO    '+ xOffset
                    Else
                        hor_centre = .Left + xO + ((.Width - Width) \ 2)
                    End If
                    'Left Side
                ElseIf .align = Alignment.alignLeft Then
                    hor_centre = .Left + xO + .xOffset
                End If

                ' if it's censored then render censored
                If Not .isCensor Then
                    ' RenderText font(.font), .Text & taddText, .Left + xO + .xOffset, .Top + yO + .yOffset, .textColour
                    RenderText Font(.Font), .Text & taddText, hor_centre, .Top + yO + .yOffset, .textColour
                Else
                    RenderText Font(.Font), CensorWord(.Text) & taddText, .Left + xO + .xOffset, .Top + yO + .yOffset, .textColour
                    RenderText Font(.Font), CensorWord(.Text) & taddText, hor_centre, .Top + yO + .yOffset, .textColour
                End If
            End If

            ' buttons
        Case EntityTypes.entButton
            ' render specific designs
            If .design(.State) > 0 Then
                If .design(.State) > 0 Then
                    RenderDesign .design(.State), .Left + xO, .Top + yO, .Width, .Height
                End If
            End If

            ' render image
            If .image(.State) > 0 Then
                If .image(.State) > 0 Then
                    RenderTexture .image(.State), .Left + xO, .Top + yO, 0, 0, .Width, .Height, .Width, .Height
                End If
            End If

            ' render icon
           ' If .Icon > 0 Then
           '     Width = mTexture(.Icon).W
           '     Height = mTexture(.Icon).H
           '     RenderTexture .Icon, .Left + xO + .xOffset, .Top + yO + .yOffset, 0, 0, Width, Height, Width, Height
           ' End If

            ' for changing the text space
            xOffset = Width

            ' calculate the vertical centre
            Height = TextHeight(Font(Fonts.OpenSans_Regular))
            If Height > .Height Then
                ver_centre = .Top + yO
            Else
                ver_centre = .Top + yO + ((.Height - Height) \ 2) + 1
            End If

            ' calculate the horizontal centre
            Width = TextWidth(Font(.Font), .Text)
            If Width > .Width Then
                hor_centre = .Left + xO + xOffset
            Else
                hor_centre = .Left + xO + xOffset + ((.Width - Width - xOffset) \ 2)
            End If

            ' get the colour
            If .State = Hover Then
                Colour = .textColour_Hover
            ElseIf .State = MouseDown Then
                Colour = .textColour_Click
            Else
                Colour = .textColour
            End If

            RenderText Font(.Font), .Text, hor_centre, ver_centre, Colour

            ' labels
        Case EntityTypes.entLabel
            If Len(.Text) > 0 Then
                Select Case .align
                Case Alignment.alignLeft
                    ' check if need to word wrap
                    If TextWidth(Font(.Font), .Text) > .Width Then
                        ' wrap text
                        WordWrap_Array .Text, .Width, TextArray()
                        ' render text
                        Count = UBound(TextArray)
                        For i = 1 To Count
                            RenderText Font(.Font), TextArray(i), .Left + xO, .Top + yO + yOffset, .textColour, .Alpha
                            yOffset = yOffset + 14
                        Next
                    Else
                        ' just one line
                        RenderText Font(.Font), .Text, .Left + xO, .Top + yO, .textColour, .Alpha
                    End If
                Case Alignment.alignRight
                    ' check if need to word wrap
                    If TextWidth(Font(.Font), .Text) > .Width Then
                        ' wrap text
                        WordWrap_Array .Text, .Width, TextArray()
                        ' render text
                        Count = UBound(TextArray)
                        For i = 1 To Count
                            Left = .Left + .Width - TextWidth(Font(.Font), TextArray(i))
                            RenderText Font(.Font), TextArray(i), Left + xO, .Top + yO + yOffset, .textColour, .Alpha
                            yOffset = yOffset + 14
                        Next
                    Else
                        ' just one line
                        Left = .Left + .Width - TextWidth(Font(.Font), .Text)
                        RenderText Font(.Font), .Text, Left + xO, .Top + yO, .textColour, .Alpha
                    End If
                Case Alignment.alignCentre
                    ' check if need to word wrap
                    If TextWidth(Font(.Font), .Text) > .Width Then
                        ' wrap text
                        WordWrap_Array .Text, .Width, TextArray()
                        ' render text
                        Count = UBound(TextArray)
                        For i = 1 To Count
                            Left = .Left + (.Width \ 2) - (TextWidth(Font(.Font), TextArray(i)) \ 2)
                            RenderText Font(.Font), TextArray(i), Left + xO, .Top + yO + yOffset, .textColour, .Alpha
                            yOffset = yOffset + 14
                        Next
                    Else
                        ' just one line
                        Left = .Left + (.Width \ 2) - (TextWidth(Font(.Font), .Text) \ 2)
                        RenderText Font(.Font), .Text, Left + xO, .Top + yO, .textColour, .Alpha
                    End If
                End Select
            End If

            ' checkboxes
        Case EntityTypes.entCheckbox

            Select Case .design(0)
            Case DesignTypes.desChkNorm
                ' empty?
                If .Value = 0 Then texNum = Tex_GUI(2) Else texNum = Tex_GUI(3)
                ' render box
                RenderTexture texNum, .Left + xO, .Top + yO, 0, 0, 18, 18, 18, 18
                ' find text position
                Select Case .align
                Case Alignment.alignLeft
                    Left = .Left + 18 + xO
                Case Alignment.alignRight
                    Left = .Left + 18 + (.Width - 18) - TextWidth(Font(.Font), .Text) + xO
                Case Alignment.alignCentre
                    Left = .Left + 18 + ((.Width - 18) / 2) - (TextWidth(Font(.Font), .Text) / 2) + xO
                End Select
                ' render text
                RenderText Font(.Font), .Text, Left, .Top + yO, .textColour, .Alpha
                
            Case DesignTypes.desChkChat
                If .Value = 0 Then .Alpha = 150 Else .Alpha = 255
                ' render box
                RenderTexture Tex_GUI(51), .Left + xO, .Top + yO, 0, 0, 49, 23, 49, 23, DX8Colour(White, .Alpha)
                ' render text
                Left = .Left + (49 / 2) - (TextWidth(Font(.Font), .Text) / 2) + xO
                ' render text
                RenderText Font(.Font), .Text, Left, .Top + yO + 4, .textColour, .Alpha
            Case DesignTypes.desChkCustom_Buying
                If .Value = 0 Then texNum = Tex_GUI(58) Else texNum = Tex_GUI(56)
                RenderTexture texNum, .Left + xO, .Top + yO, 0, 0, 49, 20, 49, 20
            Case DesignTypes.desChkCustom_Selling
                If .Value = 0 Then texNum = Tex_GUI(59) Else texNum = Tex_GUI(57)
                RenderTexture texNum, .Left + xO, .Top + yO, 0, 0, 49, 20, 49, 20
            End Select

            ' comboboxes
        Case EntityTypes.entCombobox
            Select Case .design(0)
            Case DesignTypes.desComboNorm
                ' draw the background
                RenderDesign DesignTypes.desTextBlack, .Left + xO, .Top + yO, .Width, .Height
                ' render the text
                If .Value > 0 Then
                    If .Value <= UBound(.list) Then
                        RenderText Font(.Font), .list(.Value), .Left + xO + 5, .Top + yO + 3, White
                    End If
                End If
                ' draw the little arow
                RenderTexture Tex_GUI(66), .Left + xO + .Width - 11, .Top + yO + 7, 0, 0, 5, 4, 5, 4
            End Select
        End Select

        ' callback draw
        callBack = .onDraw

        If callBack <> 0 Then entCallBack callBack, winNum, entNum, 0, 0
    End With

End Sub

Public Sub RenderWindow(winNum As Long)
    Dim Width As Long, Height As Long, callBack As Long, X As Long, Y As Long, i As Long, Left As Long
    Dim Size As Long

    ' check if the window exists
    If winNum <= 0 Or winNum > WindowCount Then
        Exit Sub
    End If

    With Windows(winNum).Window

        Select Case .design(0)
        Case DesignTypes.desComboMenuNorm
            RenderTexture Tex_Blank, .Left, .Top, 0, 0, .Width, .Height, 1, 1, DX8Colour(Black, 157)
            ' text
            If UBound(.list) > 0 Then
                Y = .Top + 2
                X = .Left
                For i = 1 To UBound(.list)
                    ' render select
                    If i = .Value Or i = .group Then RenderTexture Tex_Blank, X, Y - 1, 0, 0, .Width, 15, 1, 1, DX8Colour(Black, 255)
                    ' render text
                    Left = X + (.Width \ 2) - (TextWidth(Font(.Font), .list(i)) \ 2)
                    If i = .Value Or i = .group Then
                        RenderText Font(.Font), .list(i), Left, Y, Yellow
                    Else
                        RenderText Font(.Font), .list(i), Left, Y, White
                    End If
                    Y = Y + 16
                Next
            End If
            Exit Sub
        End Select

        Select Case .design(.State)

        Case DesignTypes.desWin_Black
            RenderTexture Tex_Fader, .Left, .Top, 0, 0, .Width, .Height, 32, 32, DX8Colour(Black, 190)

        Case DesignTypes.desWin_Norm
            ' render window
            RenderDesign DesignTypes.desWood, .Left, .Top, .Width, .Height
            RenderDesign DesignTypes.desGreen, .Left + 2, .Top + 2, .Width - 4, 21

            ' render the icon
            ' Width = mTexture(.Icon).W
            ' Height = mTexture(.Icon).H
            ' RenderTexture .Icon, .Left + .xOffset, .Top - (Width - 18) + .yOffset, 0, 0, Width, Height, Width, Height
            ' render the caption
            RenderText Font(.Font), Trim$(.Text), .Left + Height + 2, .Top + 5, .textColour

        Case DesignTypes.desWin_NoBar
            ' render window
            RenderDesign DesignTypes.desWood, .Left, .Top, .Width, .Height

        Case DesignTypes.desWin_Empty
            ' render window
            RenderDesign DesignTypes.desWood_Empty, .Left, .Top, .Width, .Height
            RenderDesign DesignTypes.desGreen, .Left + 2, .Top + 2, .Width - 4, 21
            ' render the icon
            'Width = mTexture(.Icon).W
            'Height = mTexture(.Icon).H
            'RenderTexture .Icon, .Left + .xOffset, .Top - (Width - 18) + .yOffset, 0, 0, Width, Height, Width, Height
            ' render the caption
            RenderText Font(.Font), Trim$(.Text), .Left + Height + 2, .Top + 5, .textColour

        Case DesignTypes.desWin_Desc
            ' render window
            RenderDesign DesignTypes.desWin_Desc, .Left, .Top, .Width, .Height

        Case desWin_Shadow
            ' render window
            RenderDesign DesignTypes.desWin_Shadow, .Left, .Top, .Width, .Height

        Case desWin_Party
            ' render window
            RenderDesign DesignTypes.desWin_Party, .Left, .Top, .Width, .Height

        Case desWin_AincradNorm
            RenderDesign DesignTypes.desWin_AincradNorm, .Left, .Top, .Width, .Height, 245
            RenderDesign DesignTypes.desWin_AincradNorm, .Left, .Top, .Width, 42

            RenderDesign DesignTypes.desWin_AincradHeader, .Left + 2, .Top + 2, .Width - 2, 40

            ' Check for size
            Size = TextWidth(Font(.Font), Trim$(.Text))

            ' render the caption
            RenderText Font(.Font), Trim$(.Text), .Left + (.Width * 0.5) - (Size * 0.5), .Top + 15, .textColour


        Case desWin_AincradNoBar
            RenderDesign DesignTypes.desWin_AincradNorm, .Left, .Top, .Width, .Height, 245



        ' Design Padrão nova interface

        Case desWin_Window

            ' Renderiza o Background da janela
            RenderDesign DesignTypes.desWin_Window, .Left, .Top, .Width, .Height, 255

            'Renderiza o Título
            RenderDesign DesignTypes.desWin_Title, .Left, .Top, .Width, 40

            ' Renderiza o Menu
            RenderDesign DesignTypes.desWin_Menu, .Left, .Top + 40, .Width, 26

            ' Check for size
            Size = TextWidth(Font(.Font), Trim$(.Text))

            ' render the caption
            RenderText Font(.Font), Trim$(.Text), .Left + (.Width * 0.5) - (Size * 0.5), .Top + 15, .textColour

        Case desWin_WindowNoBar

            ' Renderiza o Background da janela
            RenderDesign DesignTypes.desWin_Window, .Left, .Top, .Width, .Height, 255

            'Renderiza o Título
            RenderDesign DesignTypes.desWin_Title, .Left, .Top, .Width, 40

            ' Check for size
            Size = TextWidth(Font(.Font), Trim$(.Text))

            ' render the caption
            RenderText Font(.Font), Trim$(.Text), .Left + (.Width * 0.5) - (Size * 0.5), .Top + 15, .textColour

        End Select

        ' OnDraw call back
        callBack = .onDraw

        If callBack <> 0 Then entCallBack callBack, winNum, 0, 0, 0
    End With

End Sub

Public Sub RenderDesign(design As Long, Left As Long, Top As Long, Width As Long, Height As Long, Optional Alpha As Long = 255)
    Dim bs As Long, Colour As Long
    ' change colour for alpha
    Colour = DX8Colour(White, Alpha)

    Select Case design

    Case DesignTypes.desMenuHeader
        ' render the header
        RenderTexture Tex_Blank, Left, Top, 0, 0, Width, Height, 32, 32, D3DColorARGB(200, 47, 77, 29)

    Case DesignTypes.desMenuOption
        ' render the option
        RenderTexture Tex_Blank, Left, Top, 0, 0, Width, Height, 32, 32, D3DColorARGB(200, 98, 98, 98)

    Case DesignTypes.desWood
        bs = 4
        ' render the wood box
        RenderEntity_Square Tex_Design(TextureDesign_Wood), Left, Top, Width, Height, bs, Alpha
        ' render wood texture
        RenderTexture Tex_GUI(1), Left + bs, Top + bs, 100, 100, Width - (bs * 2), Height - (bs * 2), Width - (bs * 2), Height - (bs * 2), Colour

    Case DesignTypes.desWood_Small
        bs = 2
        ' render the wood box
        RenderEntity_Square Tex_Design(TextureDesign_WoodSmal), Left, Top, Width, Height, bs, Alpha
        ' render wood texture
        RenderTexture Tex_GUI(1), Left + bs, Top + bs, 100, 100, Width - (bs * 2), Height - (bs * 2), Width - (bs * 2), Height - (bs * 2), Colour

    Case DesignTypes.desWood_Empty
        bs = 4
        ' render the wood box
        RenderEntity_Square Tex_Design(TextureDesign_WoodEmpty), Left, Top, Width, Height, bs, Alpha

    Case DesignTypes.desGreen
        bs = 2
        ' render the green box
        RenderEntity_Square Tex_Design(TextureDesign_Green), Left, Top, Width, Height, bs, Alpha
        ' render green gradient overlay
        RenderTexture Tex_Gradient(2), Left + bs, Top + bs, 0, 0, Width - (bs * 2), Height - (bs * 2), 128, 128, Colour

    Case DesignTypes.desGreen_Hover
        bs = 2
        ' render the green box
        RenderEntity_Square Tex_Design(TextureDesign_Green), Left, Top, Width, Height, bs, Alpha
        ' render green gradient overlay
        RenderTexture Tex_Gradient(2), Left + bs, Top + bs, 0, 0, Width - (bs * 2), Height - (bs * 2), 128, 128, Colour

    Case DesignTypes.desGreen_Click
        bs = 2
        ' render the green box
        RenderEntity_Square Tex_Design(TextureDesign_Green), Left, Top, Width, Height, bs, Alpha
        ' render green gradient overlay
        RenderTexture Tex_Gradient(3), Left + bs, Top + bs, 0, 0, Width - (bs * 2), Height - (bs * 2), 128, 128, Colour

    Case DesignTypes.desEmerald
        bs = 2
        ' render the green box
        RenderEntity_Square Tex_Design(TextureDesign_Emerald), Left, Top, Width, Height, bs, Alpha
        ' render green gradient overlay
        RenderTexture Tex_Gradient(15), Left + bs, Top + bs, 0, 0, Width - (bs * 2), Height - (bs * 2), 128, 128, Colour

    Case DesignTypes.desEmerald_Hover
        bs = 2
        ' render the green box
        RenderEntity_Square Tex_Design(TextureDesign_Emerald), Left, Top, Width, Height, bs, Alpha
        ' render green gradient overlay
        RenderTexture Tex_Gradient(16), Left + bs, Top + bs, 0, 0, Width - (bs * 2), Height - (bs * 2), 128, 128, Colour

    Case DesignTypes.desEmerald_Click
        bs = 2
        ' render the green box
        RenderEntity_Square Tex_Design(TextureDesign_Emerald), Left, Top, Width, Height, bs, Alpha
        ' render green gradient overlay
        RenderTexture Tex_Gradient(3), Left + bs, Top + bs, 0, 0, Width - (bs * 2), Height - (bs * 2), 128, 128, Colour

    Case DesignTypes.desRed
        bs = 2
        ' render the red box
        RenderEntity_Square Tex_Design(TextureDesign_Red), Left, Top, Width, Height, bs, Alpha
        ' render red gradient overlay
        RenderTexture Tex_Gradient(4), Left + bs, Top + bs, 0, 0, Width - (bs * 2), Height - (bs * 2), 128, 128, Colour

    Case DesignTypes.desRed_Hover
        bs = 2
        ' render the red box
        RenderEntity_Square Tex_Design(TextureDesign_Red), Left, Top, Width, Height, bs, Alpha
        ' render red gradient overlay
        RenderTexture Tex_Gradient(5), Left + bs, Top + bs, 0, 0, Width - (bs * 2), Height - (bs * 2), 128, 128, Colour

    Case DesignTypes.desRed_Click
        bs = 2
        ' render the red box
        RenderEntity_Square Tex_Design(TextureDesign_Red), Left, Top, Width, Height, bs, Alpha
        ' render red gradient overlay
        RenderTexture Tex_Gradient(6), Left + bs, Top + bs, 0, 0, Width - (bs * 2), Height - (bs * 2), 128, 128, Colour

    Case DesignTypes.desBlue
        bs = 2
        ' render the Blue box
        RenderEntity_Square Tex_Design(TextureDesign_AincradGreenSquare), Left, Top, Width, Height, bs, Alpha
        ' render Blue gradient overlay
        RenderTexture Tex_Gradient(8), Left + bs, Top + bs, 0, 0, Width - (bs * 2), Height - (bs * 2), 128, 128, Colour

    Case DesignTypes.desBlue_Hover
        bs = 2
        ' render the Blue box
        RenderEntity_Square Tex_Design(TextureDesign_AincradGreenSquare), Left, Top, Width, Height, bs, Alpha
        ' render Blue gradient overlay
        RenderTexture Tex_Gradient(9), Left + bs, Top + bs, 0, 0, Width - (bs * 2), Height - (bs * 2), 128, 128, Colour

    Case DesignTypes.desBlue_Click
        bs = 2
        ' render the Blue box
        RenderEntity_Square Tex_Design(TextureDesign_AincradGreenSquare), Left, Top, Width, Height, bs, Alpha
        ' render Blue gradient overlay
        RenderTexture Tex_Gradient(10), Left + bs, Top + bs, 0, 0, Width - (bs * 2), Height - (bs * 2), 128, 128, Colour

    Case DesignTypes.desOrange
        bs = 2
        ' render the Orange box
        RenderEntity_Square Tex_Design(TextureDesign_Orange), Left, Top, Width, Height, bs, Alpha
        ' render Orange gradient overlay
        RenderTexture Tex_Gradient(11), Left + bs, Top + bs, 0, 0, Width - (bs * 2), Height - (bs * 2), 128, 128, Colour

    Case DesignTypes.desOrange_Hover
        bs = 2
        ' render the Orange box
        RenderEntity_Square Tex_Design(TextureDesign_Orange), Left, Top, Width, Height, bs, Alpha
        ' render Orange gradient overlay
        RenderTexture Tex_Gradient(12), Left + bs, Top + bs, 0, 0, Width - (bs * 2), Height - (bs * 2), 128, 128, Colour

    Case DesignTypes.desOrange_Click
        bs = 2
        ' render the Orange box
        RenderEntity_Square Tex_Design(TextureDesign_Orange), Left, Top, Width, Height, bs, Alpha
        ' render Orange gradient overlay
        RenderTexture Tex_Gradient(13), Left + bs, Top + bs, 0, 0, Width - (bs * 2), Height - (bs * 2), 128, 128, Colour

    Case DesignTypes.desGrey
        bs = 2
        ' render the Orange box
        RenderEntity_Square Tex_Design(TextureDesign_Grey), Left, Top, Width, Height, bs, Alpha
        ' render Orange gradient overlay
        RenderTexture Tex_Gradient(14), Left + bs, Top + bs, 0, 0, Width - (bs * 2), Height - (bs * 2), 128, 128, Colour

    Case DesignTypes.desSteel
        RenderTexture Tex_Design(TextureDesign_Steel), Left, Top, 0, 0, Width, Height, 16, 16, Colour

    Case DesignTypes.desSteel_Hover
        RenderTexture Tex_Design(TextureDesign_SteelHover), Left, Top, 0, 0, Width, Height, 16, 16, Colour

    Case DesignTypes.desSteel_Click
        RenderTexture Tex_Design(TextureDesign_SteelClick), Left, Top, 0, 0, Width, Height, 16, 16, Colour

    Case DesignTypes.desParchment
        bs = 20
        ' render the parchment box
        RenderEntity_Square Tex_Design(TextureDesign_Parchment), Left, Top, Width, Height, bs, Alpha

    Case DesignTypes.desBlackOval
        bs = 4
        ' render the black oval
        RenderEntity_Square Tex_Design(TextureDesign_BlackOval), Left, Top, Width, Height, bs, Alpha

    Case DesignTypes.desTextBlack
        bs = 5
        ' render the black oval
        RenderEntity_Square Tex_Design(TextureDesign_TextBlack), Left, Top, Width, Height, bs, Alpha

    Case DesignTypes.desTextWhite
        bs = 5
        ' render the black oval
        RenderEntity_Square Tex_Design(TextureDesign_TextWhite), Left, Top, Width, Height, bs, Alpha

    Case DesignTypes.desTextBlack_Sq
        bs = 4
        ' render the black oval
        RenderEntity_Square Tex_Design(TextureDesign_TextBlackSquare), Left, Top, Width, Height, bs, Alpha

    Case DesignTypes.desTextAincrad
        bs = 2
        ' render the black oval
        RenderEntity_Square Tex_Design(TextureDesign_TextAincrad), Left, Top, Width, Height, bs, Alpha

    Case DesignTypes.desWin_Desc
        bs = 8
        ' render black square
        RenderEntity_Square Tex_Design(TextureDesign_WindowDescription), Left, Top, Width, Height, bs, Alpha

    Case DesignTypes.desDescPic
        bs = 3
        ' render the green box
        RenderEntity_Square Tex_Design(TextureDesign_DescriptionPicture), Left, Top, Width, Height, bs, Alpha
        ' render green gradient overlay
        RenderTexture Tex_Gradient(7), Left + bs, Top + bs, 0, 0, Width - (bs * 2), Height - (bs * 2), 128, 128, Colour

    Case DesignTypes.desWin_Shadow
        bs = 35
        ' render the green box
        RenderEntity_Square Tex_Design(TextureDesign_WindowShadow), Left - bs, Top - bs, Width + (bs * 2), Height + (bs * 2), bs, Alpha

    Case DesignTypes.desWin_Party
        bs = 12
        ' render black square
        RenderEntity_Square Tex_Design(TextureDesign_WindowParty), Left, Top, Width, Height, bs, Alpha

    Case DesignTypes.desTileBox
        bs = 4
        ' render box
        RenderEntity_Square Tex_Design(TextureDesign_WindowShadow), Left, Top, Width, Height, bs, Alpha

    Case DesignTypes.desWin_AincradNorm
        bs = 2
        ' render the aincrad window base
        RenderEntity_Square Tex_Design(TextureDesign_TextAincrad), Left, Top, Width, Height, bs, Alpha

    Case DesignTypes.desWin_AincradHeader
        bs = 2
        RenderTexture Tex_Design(TextureDesign_HeaderAincrad), Left + bs, Top + bs, 0, 0, Width - (bs * 2), Height - (bs * 2), 40, 40, Colour

    Case DesignTypes.desWin_AincradMenu
        bs = 1
        RenderTexture Tex_Design(TextureDesign_MenuAincrad), Left + 1 + bs, Top + bs, 0, 0, Width - 6 + (bs * 2), Height - (bs * 2), 26, 26, Colour

        '###############################################################################################

    ' Design Window Definition
    
    Case DesignTypes.desWin_Title
        bs = 2
        RenderEntity_Square Tex_Design(TextureDesign_Title), Left, Top, Width, Height, bs, Alpha

    Case DesignTypes.desWin_Menu
        bs = 2
        RenderEntity_Square Tex_Design(TextureDesign_Menu), Left, Top, Width, Height, bs, Alpha

    Case DesignTypes.desWin_Window
        bs = 2
        RenderEntity_Square Tex_Design(TextureDesign_Window), Left, Top, Width, Height, bs, Alpha
    
    ' Buttons Definition
    
    Case DesignTypes.desButton
        RenderTexture Tex_Design(TextureDesign_Title), Left, Top, 0, 0, Width, Height, 16, 16, Colour

    Case DesignTypes.desButton_Hover
        RenderTexture Tex_Design(TextureDesign_Menu), Left, Top, 0, 0, Width, Height, 16, 16, Colour

    Case DesignTypes.desButton_Click
        RenderTexture Tex_Design(TextureDesign_Title), Left, Top, 0, 0, Width, Height, 16, 16, Colour
    
    ' TextBox Definition

    Case DesignTypes.desText
        bs = 2
        ' render the black oval
        RenderEntity_Square Tex_Design(TextureDesign_Title), Left, Top, Width, Height, bs, Alpha
    
    Case DesignTypes.desText_Hover
        bs = 2
        RenderEntity_Square Tex_Design(TextureDesign_Menu), Left, Top, Width, Height, bs, Alpha


    End Select

End Sub

Public Sub RenderEntity_Square(texNum As Long, X As Long, Y As Long, Width As Long, Height As Long, borderSize As Long, Optional Alpha As Long = 255)
    Dim bs As Long, Colour As Long
    ' change colour for alpha
    Colour = DX8Colour(White, Alpha)
    ' Set the border size
    bs = borderSize
    ' Draw centre
    RenderTexture texNum, X + bs, Y + bs, bs + 1, bs + 1, Width - (bs * 2), Height - (bs * 2), 1, 1, Colour
    ' Draw top side
    RenderTexture texNum, X + bs, Y, bs, 0, Width - (bs * 2), bs, 1, bs, Colour
    ' Draw left side
    RenderTexture texNum, X, Y + bs, 0, bs, bs, Height - (bs * 2), bs, 1, Colour
    ' Draw right side
    RenderTexture texNum, X + Width - bs, Y + bs, bs + 3, bs, bs, Height - (bs * 2), bs, 1, Colour
    ' Draw bottom side
    RenderTexture texNum, X + bs, Y + Height - bs, bs, bs + 3, Width - (bs * 2), bs, 1, bs, Colour
    ' Draw top left corner
    RenderTexture texNum, X, Y, 0, 0, bs, bs, bs, bs, Colour
    ' Draw top right corner
    RenderTexture texNum, X + Width - bs, Y, bs + 3, 0, bs, bs, bs, bs, Colour
    ' Draw bottom left corner
    RenderTexture texNum, X, Y + Height - bs, 0, bs + 3, bs, bs, bs, bs, Colour
    ' Draw bottom right corner
    RenderTexture texNum, X + Width - bs, Y + Height - bs, bs + 3, bs + 3, bs, bs, bs, bs, Colour
End Sub

Sub Combobox_AddItem(WinIndex As Long, ControlIndex As Long, Text As String)
    Dim Count As Long
    Count = UBound(Windows(WinIndex).Controls(ControlIndex).list)
    ReDim Preserve Windows(WinIndex).Controls(ControlIndex).list(0 To Count + 1)
    Windows(WinIndex).Controls(ControlIndex).list(Count + 1) = Text
End Sub

Public Sub CreateWindow(Name As String, caption As String, zOrder As Long, Left As Long, Top As Long, Width As Long, Height As Long, Icon As Long, _
                        Optional Visible As Boolean = True, Optional Font As Long = Fonts.OpenSans_Regular, Optional textColour As Long = White, Optional xOffset As Long, _
                        Optional yOffset As Long, Optional design_norm As Long, Optional design_hover As Long, Optional design_mousedown As Long, Optional image_norm As Long, _
                        Optional image_hover As Long, Optional image_mousedown As Long, Optional entCallBack_norm As Long, Optional entCallBack_hover As Long, Optional entCallBack_mousedown As Long, _
                        Optional entCallBack_mousemove As Long, Optional entCallBack_dblclick As Long, Optional canDrag As Boolean = True, Optional zChange As Byte = True, Optional ByVal onDraw As Long, _
                        Optional isActive As Boolean, Optional clickThrough As Boolean)

    Dim i As Long
    Dim design(0 To entStates.state_Count - 1) As Long
    Dim image(0 To entStates.state_Count - 1) As Long
    Dim entCallBack(0 To entStates.state_Count - 1) As Long

    ' fill temp arrays
    design(entStates.Normal) = design_norm
    design(entStates.Hover) = design_hover
    design(entStates.MouseDown) = design_mousedown
    design(entStates.DblClick) = design_norm
    design(entStates.MouseUp) = design_norm
    image(entStates.Normal) = image_norm
    image(entStates.Hover) = image_hover
    image(entStates.MouseDown) = image_mousedown
    image(entStates.DblClick) = image_norm
    image(entStates.MouseUp) = image_norm
    entCallBack(entStates.Normal) = entCallBack_norm
    entCallBack(entStates.Hover) = entCallBack_hover
    entCallBack(entStates.MouseDown) = entCallBack_mousedown
    entCallBack(entStates.MouseMove) = entCallBack_mousemove
    entCallBack(entStates.DblClick) = entCallBack_dblclick
    ' redim the windows
    WindowCount = WindowCount + 1
    ReDim Preserve Windows(1 To WindowCount) As WindowRec

    ' set the properties
    With Windows(WindowCount).Window
        .Name = Name
        .Type = EntityTypes.entWindow

        ' loop through states
        For i = 0 To entStates.state_Count - 1
            .design(i) = design(i)
            .image(i) = image(i)
            .entCallBack(i) = entCallBack(i)
        Next

        .Left = Left
        .Top = Top
        .origLeft = Left
        .origTop = Top
        .Width = Width
        .Height = Height
        .Visible = Visible
        .canDrag = canDrag
        .Text = caption
        .Font = Font
        .textColour = textColour
        .xOffset = xOffset
        .yOffset = yOffset
        .Icon = Icon
        .Enabled = True
        .zChange = zChange
        .zOrder = zOrder
        .onDraw = onDraw
        .clickThrough = clickThrough
        ' set active
        If .Visible Then activeWindow = WindowCount
    End With

    ' set the zOrder
    zOrder_Win = zOrder_Win + 1
End Sub

Public Sub CreateTextbox(winNum As Long, Name As String, Left As Long, Top As Long, Width As Long, Height As Long, Optional Text As String, Optional Font As Long = Fonts.OpenSans_Regular, _
                         Optional textColour As Long = White, Optional align As Byte = Alignment.alignLeft, Optional Visible As Boolean = True, Optional Alpha As Long = 255, Optional image_norm As Long, _
                         Optional image_hover As Long, Optional image_mousedown As Long, Optional design_norm As Long, Optional design_hover As Long, Optional design_mousedown As Long, _
                         Optional entCallBack_norm As Long, Optional entCallBack_hover As Long, Optional entCallBack_mousedown As Long, Optional entCallBack_mousemove As Long, Optional entCallBack_dblclick As Long, _
                         Optional isActive As Boolean, Optional xOffset As Long, Optional yOffset As Long, Optional isCensor As Boolean, Optional entCallBack_enter As Long, Optional Multiline As Boolean)
    Dim design(0 To entStates.state_Count - 1) As Long
    Dim image(0 To entStates.state_Count - 1) As Long
    Dim entCallBack(0 To entStates.state_Count - 1) As Long
    ' fill temp arrays
    design(entStates.Normal) = design_norm
    design(entStates.Hover) = design_hover
    design(entStates.MouseDown) = design_mousedown
    image(entStates.Normal) = image_norm
    image(entStates.Hover) = image_hover
    image(entStates.MouseDown) = image_mousedown
    entCallBack(entStates.Normal) = entCallBack_norm
    entCallBack(entStates.Hover) = entCallBack_hover
    entCallBack(entStates.MouseDown) = entCallBack_mousedown
    entCallBack(entStates.MouseMove) = entCallBack_mousemove
    entCallBack(entStates.DblClick) = entCallBack_dblclick
    entCallBack(entStates.Enter) = entCallBack_enter
    
    ' create the textbox
    CreateEntity winNum, zOrder_Con, Name, entTextBox, design(), image(), entCallBack(), Left, Top, Width, Height, Visible, , , , , Text, align, Font, textColour, Alpha, , xOffset, yOffset, , , , isActive, isCensor, , , , , Multiline
End Sub

Public Sub CreatePictureBox(winNum As Long, Name As String, Left As Long, Top As Long, Width As Long, Height As Long, Optional Visible As Boolean = True, Optional canDrag As Boolean, _
                            Optional Alpha As Long = 255, Optional clickThrough As Boolean, Optional image_norm As Long, Optional image_hover As Long, Optional image_mousedown As Long, Optional design_norm As Long, _
                            Optional design_hover As Long, Optional design_mousedown As Long, Optional entCallBack_norm As Long, Optional entCallBack_hover As Long, Optional entCallBack_mousedown As Long, _
                            Optional entCallBack_mousemove As Long, Optional entCallBack_dblclick As Long, Optional onDraw As Long)
    Dim design(0 To entStates.state_Count - 1) As Long
    Dim image(0 To entStates.state_Count - 1) As Long
    Dim entCallBack(0 To entStates.state_Count - 1) As Long

    ' fill temp arrays
    design(entStates.Normal) = design_norm
    design(entStates.Hover) = design_hover
    design(entStates.MouseDown) = design_mousedown
    image(entStates.Normal) = image_norm
    image(entStates.Hover) = image_hover
    image(entStates.MouseDown) = image_mousedown
    entCallBack(entStates.Normal) = entCallBack_norm
    entCallBack(entStates.Hover) = entCallBack_hover
    entCallBack(entStates.MouseDown) = entCallBack_mousedown
    entCallBack(entStates.MouseMove) = entCallBack_mousemove
    entCallBack(entStates.DblClick) = entCallBack_dblclick
    ' create the box
    CreateEntity winNum, zOrder_Con, Name, entPictureBox, design(), image(), entCallBack(), Left, Top, Width, Height, Visible, canDrag, , , , , , , , Alpha, clickThrough, , , , , onDraw
End Sub

Public Sub CreateButton(winNum As Long, Name As String, Left As Long, Top As Long, Width As Long, Height As Long, Optional Text As String, Optional Font As Fonts = Fonts.OpenSans_Regular, _
                        Optional textColour As Long = White, Optional Icon As Long, Optional Visible As Boolean = True, Optional Alpha As Long = 255, Optional image_norm As Long, Optional image_hover As Long, _
                        Optional image_mousedown As Long, Optional design_norm As Long, Optional design_hover As Long, Optional design_mousedown As Long, Optional entCallBack_norm As Long, _
                        Optional entCallBack_hover As Long, Optional entCallBack_mousedown As Long, Optional entCallBack_mousemove As Long, Optional entCallBack_dblclick As Long, Optional xOffset As Long, _
                        Optional yOffset As Long, Optional textColour_Hover As Long = -1, Optional textColour_Click As Long = -1, Optional tooltip As String)
    Dim design(0 To entStates.state_Count - 1) As Long
    Dim image(0 To entStates.state_Count - 1) As Long
    Dim entCallBack(0 To entStates.state_Count - 1) As Long
    ' default the colours
    If textColour_Hover = -1 Then textColour_Hover = textColour
    If textColour_Click = -1 Then textColour_Click = textColour
    ' fill temp arrays
    design(entStates.Normal) = design_norm
    design(entStates.Hover) = design_hover
    design(entStates.MouseDown) = design_mousedown
    image(entStates.Normal) = image_norm
    image(entStates.Hover) = image_hover
    image(entStates.MouseDown) = image_mousedown
    entCallBack(entStates.Normal) = entCallBack_norm
    entCallBack(entStates.Hover) = entCallBack_hover
    entCallBack(entStates.MouseDown) = entCallBack_mousedown
    entCallBack(entStates.MouseMove) = entCallBack_mousemove
    entCallBack(entStates.DblClick) = entCallBack_dblclick
    ' create the box
    CreateEntity winNum, zOrder_Con, Name, entButton, design(), image(), entCallBack(), Left, Top, Width, Height, Visible, , , , , Text, , Font, textColour, Alpha, , xOffset, yOffset, , Icon, , , , textColour_Hover, textColour_Click, tooltip
End Sub

Public Sub CreateLabel(winNum As Long, Name As String, Left As Long, Top As Long, Width As Long, Optional Height As Long, Optional Text As String, Optional Font As Fonts = Fonts.OpenSans_Regular, _
                       Optional textColour As Long = White, Optional align As Byte = Alignment.alignLeft, Optional Visible As Boolean = True, Optional Alpha As Long = 255, Optional clickThrough As Boolean, _
                       Optional entCallBack_norm As Long, Optional entCallBack_hover As Long, Optional entCallBack_mousedown As Long, Optional entCallBack_mousemove As Long, Optional entCallBack_dblclick As Long)
    Dim design(0 To entStates.state_Count - 1) As Long
    Dim image(0 To entStates.state_Count - 1) As Long
    Dim entCallBack(0 To entStates.state_Count - 1) As Long
    ' fill temp arrays
    entCallBack(entStates.Normal) = entCallBack_norm
    entCallBack(entStates.Hover) = entCallBack_hover
    entCallBack(entStates.MouseDown) = entCallBack_mousedown
    entCallBack(entStates.MouseMove) = entCallBack_mousemove
    entCallBack(entStates.DblClick) = entCallBack_dblclick
    ' create the box
    CreateEntity winNum, zOrder_Con, Name, entLabel, design(), image(), entCallBack(), Left, Top, Width, Height, Visible, , , , , Text, align, Font, textColour, Alpha, clickThrough
End Sub

Public Sub CreateCheckbox(winNum As Long, Name As String, Left As Long, Top As Long, Width As Long, Optional Height As Long = 15, Optional Value As Long, Optional Text As String, _
                          Optional Font As Fonts = Fonts.OpenSans_Regular, Optional textColour As Long = White, Optional align As Byte = Alignment.alignLeft, Optional Visible As Boolean = True, Optional Alpha As Long = 255, _
                          Optional theDesign As Long, Optional entCallBack_norm As Long, Optional entCallBack_hover As Long, Optional entCallBack_mousedown As Long, Optional entCallBack_mousemove As Long, _
                          Optional entCallBack_dblclick As Long, Optional group As Long)
    Dim design(0 To entStates.state_Count - 1) As Long
    Dim image(0 To entStates.state_Count - 1) As Long
    Dim entCallBack(0 To entStates.state_Count - 1) As Long
    ' fill temp arrays
    entCallBack(entStates.Normal) = entCallBack_norm
    entCallBack(entStates.Hover) = entCallBack_hover
    entCallBack(entStates.MouseDown) = entCallBack_mousedown
    entCallBack(entStates.MouseMove) = entCallBack_mousemove
    entCallBack(entStates.DblClick) = entCallBack_dblclick
    ' fill temp array
    design(0) = theDesign
    ' create the box
    CreateEntity winNum, zOrder_Con, Name, entCheckbox, design(), image(), entCallBack(), Left, Top, Width, Height, Visible, , , , Value, Text, align, Font, textColour, Alpha, , , , , , , , , , , , group
End Sub

Public Sub CreateComboBox(winNum As Long, Name As String, Left As Long, Top As Long, Width As Long, Height As Long, design As Long, Optional Font As Fonts = Fonts.OpenSans_Regular)
    Dim theDesign(0 To entStates.state_Count - 1) As Long
    Dim image(0 To entStates.state_Count - 1) As Long
    Dim entCallBack(0 To entStates.state_Count - 1) As Long
    
    theDesign(0) = design
    ' create the box
    CreateEntity winNum, zOrder_Con, Name, entCombobox, theDesign(), image(), entCallBack(), Left, Top, Width, Height, , , , , , , , Font
End Sub

Public Function GetWindowIndex(winName As String) As Long
    Dim i As Long

    For i = 1 To WindowCount
        If LCase$(Windows(i).Window.Name) = LCase$(winName) Then
            GetWindowIndex = i
            Exit Function
        End If
    Next

    GetWindowIndex = 0
End Function

Public Function GetControlIndex(winName As String, ControlName As String) As Long
    Dim i As Long, WinIndex As Long

    WinIndex = GetWindowIndex(winName)

    If Not WinIndex > 0 Or Not WinIndex <= WindowCount Then Exit Function

    For i = 1 To Windows(WinIndex).ControlCount
        If LCase$(Windows(WinIndex).Controls(i).Name) = LCase$(ControlName) Then
            GetControlIndex = i
            Exit Function
        End If
    Next

    GetControlIndex = 0
End Function

Public Function SetActiveControl(curWindow As Long, curControl As Long) As Boolean
' make sure it's something which CAN be active
    Select Case Windows(curWindow).Controls(curControl).Type
    Case EntityTypes.entTextBox
        Windows(curWindow).activeControl = curControl
        SetActiveControl = True
    End Select
End Function

Public Sub CentraliseWindow(curWindow As Long)
    With Windows(curWindow).Window
        .Left = (ScreenWidth / 2) - (.Width / 2)
        .Top = (ScreenHeight / 2) - (.Height / 2)
        .origLeft = .Left
        .origTop = .Top
    End With
End Sub

Public Sub HideWindows()
    Dim i As Long
    For i = 1 To WindowCount
        HideWindow i
    Next
End Sub

Public Sub ShowWindow(curWindow As Long, Optional Forced As Boolean, Optional resetPosition As Boolean = True)
    Windows(curWindow).Window.Visible = True

    If Forced Then
        UpdateZOrder curWindow, Forced
        activeWindow = curWindow
    ElseIf Windows(curWindow).Window.zChange Then
        UpdateZOrder curWindow
        activeWindow = curWindow
    End If

    If resetPosition Then
        With Windows(curWindow).Window
            .Left = .origLeft
            .Top = .origTop
        End With
    End If
End Sub

Public Sub HideWindow(curWindow As Long)
    Dim i As Long

    If curWindow > 0 Then

        Windows(curWindow).Window.Visible = False
        ' find next window to set as active
        For i = WindowCount To 1 Step -1
            If Windows(i).Window.Visible And Windows(i).Window.zChange Then
                'UpdateZOrder i
                activeWindow = i
                Exit Sub
            End If
        Next

    End If
End Sub

Public Sub CreateWindow_Loading()
' Create the window
    CreateWindow "winLoading", "Carregando", zOrder_Win, 0, 0, 278, 79, 0, True, Fonts.OpenSans_Regular, , 2, 7, DesignTypes.desWin_AincradNoBar, DesignTypes.desWin_AincradNoBar, DesignTypes.desWin_AincradNoBar, , , , , , , , , False
    ' Centralise it
    CentraliseWindow WindowCount
    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Text background
    CreatePictureBox WindowCount, "picRecess", 26, 23, 226, 26, , , , , , , , DesignTypes.desTextBlack, DesignTypes.desTextBlack, DesignTypes.desTextBlack
    ' Label
    CreateLabel WindowCount, "lblLoading", 6, 27, 266, , "Carregando dados do jogo ...", OpenSans_Regular, , Alignment.alignCentre
End Sub

Public Sub CreateWindow_EscMenu()
' Create window
    CreateWindow "winEscMenu", "", zOrder_Win, 0, 0, 210, 156, 0, , , , , , DesignTypes.desWin_NoBar, DesignTypes.desWin_NoBar, DesignTypes.desWin_NoBar, , , , , , , , , False, False
    ' Centralise it
    CentraliseWindow WindowCount

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Parchment
    CreatePictureBox WindowCount, "picParchment", 6, 6, 198, 144, , , , , , , , DesignTypes.desParchment, DesignTypes.desParchment, DesignTypes.desParchment
    ' Buttons
    CreateButton WindowCount, "btnReturn", 16, 16, 178, 28, "Retornar para o jogo (Esc)", OpenSans_Regular, , , , , , , , DesignTypes.desEmerald, DesignTypes.desEmerald_Hover, DesignTypes.desEmerald_Click, , , GetAddress(AddressOf btnEscMenu_Return)
    CreateButton WindowCount, "btnOptions", 16, 48, 178, 28, "Opções", OpenSans_Regular, , , , , , , , DesignTypes.desOrange, DesignTypes.desOrange_Hover, DesignTypes.desOrange_Click, , , GetAddress(AddressOf btnEscMenu_Options)
    CreateButton WindowCount, "btnMainMenu", 16, 80, 178, 28, "Voltar ao menu principal", OpenSans_Regular, , , , , , , , DesignTypes.desBlue, DesignTypes.desSteel_Hover, DesignTypes.desBlue_Click, , , GetAddress(AddressOf btnEscMenu_MainMenu)
    CreateButton WindowCount, "btnExit", 16, 112, 178, 28, "Sair do jogo", OpenSans_Regular, , , , , , , , DesignTypes.desRed, DesignTypes.desRed_Hover, DesignTypes.desRed_Click, , , GetAddress(AddressOf btnEscMenu_Exit)
End Sub

Public Sub CreateWindow_Bars()
' Create window
    CreateWindow "winBars", "", zOrder_Win, 10, 10, 239, 77, 0, , , , , , DesignTypes.desWin_AincradNoBar, DesignTypes.desWin_AincradNoBar, DesignTypes.desWin_AincradNoBar, , , , , , , , , False, False

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Parchment
    CreatePictureBox WindowCount, "picParchment", 6, 6, 227, 65, , , , , , , , DesignTypes.desParchment, DesignTypes.desParchment, DesignTypes.desParchment
    ' Blank Bars
    CreatePictureBox WindowCount, "picHP_Blank", 15, 15, 209, 13, , , , , Tex_GUI(24), Tex_GUI(24), Tex_GUI(24)
    CreatePictureBox WindowCount, "picSP_Blank", 15, 32, 209, 13, , , , , Tex_GUI(25), Tex_GUI(25), Tex_GUI(25)
    CreatePictureBox WindowCount, "picEXP_Blank", 15, 49, 209, 13, , , , , Tex_GUI(26), Tex_GUI(26), Tex_GUI(26)
    ' Draw the bars
    CreatePictureBox WindowCount, "picBlank", 0, 0, 0, 0, , , , , , , , , , , , , , , , GetAddress(AddressOf Bars_OnDraw)
    ' Bar Labels
    CreatePictureBox WindowCount, "picHealth", 16, 11, 44, 14, , , , , Tex_GUI(21), Tex_GUI(21), Tex_GUI(21)
    CreatePictureBox WindowCount, "picSpirit", 16, 28, 44, 14, , , , , Tex_GUI(22), Tex_GUI(22), Tex_GUI(22)
    CreatePictureBox WindowCount, "picExperience", 16, 45, 74, 14, , , , , Tex_GUI(23), Tex_GUI(23), Tex_GUI(23)
    ' Labels
    CreateLabel WindowCount, "lblHP", 15, 14, 209, 12, "999/999", OpenSans_Regular, White, Alignment.alignCentre
    CreateLabel WindowCount, "lblMP", 15, 31, 209, 12, "999/999", OpenSans_Regular, White, Alignment.alignCentre
    CreateLabel WindowCount, "lblEXP", 15, 48, 209, 12, "999/999", OpenSans_Regular, White, Alignment.alignCentre
End Sub

Public Sub CreateWindow_QuickSlot()
' Create window
    CreateWindow "winHotbar", "", zOrder_Win, 372, 10, 418, 36, 0, , , , , , , , , , , , , GetAddress(AddressOf QuickSlot_MouseMove), GetAddress(AddressOf QuickSlot_MouseDown), GetAddress(AddressOf QuickSlot_MouseMove), GetAddress(AddressOf QuickSlot_DblClick), False, False, GetAddress(AddressOf DrawHotbar)
End Sub

Public Sub CreateWindow_Description()
' Create window
    CreateWindow "winDescription", "", zOrder_Win, 0, 0, 225, 320, 0, , , , , , DesignTypes.desWin_AincradNoBar, DesignTypes.desWin_AincradNoBar, DesignTypes.desWin_AincradNoBar, , , , , , , , , False, , GetAddress(AddressOf Description_OnDraw)

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Name
    CreateLabel WindowCount, "lblName", 0, 10, 225, , "(SB) Flame Sword", OpenSans_Effect, BrightBlue, Alignment.alignCentre
    CreateLabel WindowCount, "lblType", 0, 25, 225, , "Sword", OpenSans_Effect, White, Alignment.alignCentre

    CreateLabel WindowCount, "lblPrice", 15, 25, 183, , "Sword", OpenSans_Regular, White, Alignment.alignLeft
    'CreateLabel WindowCount, "lblClass", 15, 40, 183, , "Sem Requiremento de classe", OpenSans_Regular, LightGreen, Alignment.alignLeft
    ' CreateLabel WindowCount, "lblLevel", 15, 55, 183, , "Level 20", OpenSans_Regular, BrightRed, Alignment.alignLeft
    ' CreateLabel WindowCount, "lblProficiency", 15, 70, 183, , "Tecido", OpenSans_Regular, BrightRed, Alignment.alignLeft
    ' CreateLabel WindowCount, "lblBind", 15, 85, 183, , "Bind On Equip", OpenSans_Regular, BrightRed, Alignment.alignLeft

    ' Sprite box
    ' CreatePictureBox WindowCount, "picSprite", 18, 32, 68, 68, , , , , , , , DesignTypes.desDescPic, DesignTypes.desDescPic, DesignTypes.desDescPic, , , , , , GetAddress(AddressOf Description_OnDraw)
    ' Sep
    '  CreatePictureBox WindowCount, "picSep", 96, 28, 1, 92, , , , , Tex_GUI(44), Tex_GUI(44), Tex_GUI(44)

    CreateLabel WindowCount, "lblDesc", 10, 100, 193, 350, "---", OpenSans_Regular, White, Alignment.alignCentre
    ' Bar
    '  CreatePictureBox WindowCount, "picBar", 19, 114, 66, 12, False, , , , Tex_GUI(45), Tex_GUI(45), Tex_GUI(45)
End Sub

Public Sub CreateWindow_DragBox()
' Create window
    CreateWindow "winDragBox", "", zOrder_Win, 0, 0, 32, 32, 0, , , , , , , , , , , , GetAddress(AddressOf DragBox_Check), , , , , , , GetAddress(AddressOf DragBox_OnDraw)
    ' Need to set up unique mouseup event
    Windows(WindowCount).Window.entCallBack(entStates.MouseUp) = GetAddress(AddressOf DragBox_Check)
End Sub

Public Sub CreateWindow_RightClick()
' Create window
    CreateWindow "winRightClickBG", "", zOrder_Win, 0, 0, 800, 600, 0, , , , , , , , , , , , , , GetAddress(AddressOf RightClick_Close), , , False
    ' Centralise it
    CentraliseWindow WindowCount
End Sub

Public Sub CreateWindow_PlayerMenu()
' Create window
    CreateWindow "winPlayerMenu", "", zOrder_Win, 0, 0, 150, 150, 0, , , , , , DesignTypes.desWin_Desc, DesignTypes.desWin_Desc, DesignTypes.desWin_Desc, , , , , , GetAddress(AddressOf RightClick_Close), , , False
    ' Centralise it
    CentraliseWindow WindowCount

    ' Name
    CreateButton WindowCount, "btnName", 8, 8, 134, 18, "[Nome]", OpenSans_Effect, White, , , , , , , DesignTypes.desMenuHeader, DesignTypes.desMenuHeader, DesignTypes.desMenuHeader, , , GetAddress(AddressOf RightClick_Close)
    ' Options
    CreateButton WindowCount, "btnParty", 8, 26, 135, 18, "Convite para grupo", OpenSans_Effect, White, , , , , , , , DesignTypes.desMenuOption, , , , GetAddress(AddressOf PlayerMenu_Party)
    CreateButton WindowCount, "btnParty", 8, 44, 135, 18, "Sair do grupo", OpenSans_Effect, White, , , , , , , , DesignTypes.desMenuOption, , , , GetAddress(AddressOf PlayerMenu_LeaveParty)
    CreateButton WindowCount, "btnTrade", 8, 62, 135, 18, "Pedido de negociação", OpenSans_Effect, White, , , , , , , , DesignTypes.desMenuOption, , , , GetAddress(AddressOf PlayerMenu_Trade)
    CreateButton WindowCount, "btnGuild", 8, 80, 135, 18, "Convite para guild", OpenSans_Effect, White, , , , , , , , DesignTypes.desMenuOption, , , , GetAddress(AddressOf PlayerMenu_Guild)
    CreateButton WindowCount, "btnPM", 8, 98, 135, 18, "Mensagem privada", OpenSans_Effect, White, , , , , , , , DesignTypes.desMenuOption, , , , GetAddress(AddressOf PlayerMenu_PM)
    CreateButton WindowCount, "btnViewEquipment", 8, 116, 135, 18, "Ver Equipamento", OpenSans_Effect, White, , , , , , , , DesignTypes.desMenuOption, , , , GetAddress(AddressOf PlayerMenu_ViewEquipment)
End Sub

Public Sub CreateWindow_Invitations()
' Create window
    CreateWindow "winInvite_Party", "", zOrder_Win, ScreenWidth - 234, ScreenHeight - 80, 223, 37, 0, , , , , , DesignTypes.desWin_Desc, DesignTypes.desWin_Desc, DesignTypes.desWin_Desc, , , , , , , , , False
    ' Button
    CreateButton WindowCount, "btnInvite", 11, 12, 201, 14, ColourChar & White & "Richard " & ColourChar & "-1" & "has invited you to a party.", OpenSans_Regular, Grey, , , , , , , , , , , , GetAddress(AddressOf btnInvite_Party), , , , , Green

    ' Create window
    CreateWindow "winInvite_Trade", "", zOrder_Win, ScreenWidth - 234, ScreenHeight - 80, 223, 37, 0, , , , , , DesignTypes.desWin_Desc, DesignTypes.desWin_Desc, DesignTypes.desWin_Desc, , , , , , , , , False
    ' Button
    CreateButton WindowCount, "btnInvite", 11, 12, 201, 14, ColourChar & White & "Richard " & ColourChar & "-1" & "convidou voce para um grupo.", OpenSans_Regular, Grey, , , , , , , , , , , , 0, , , , , Green
End Sub

Public Sub CreateWindow_Combobox()
' background window
    CreateWindow "winComboMenuBG", "ComboMenuBG", zOrder_Win, 0, 0, 800, 600, 0, , , , , , , , , , , , , , GetAddress(AddressOf CloseComboMenu), , , False, False

    ' window
    CreateWindow "winComboMenu", "ComboMenu", zOrder_Win, 0, 0, 100, 100, 0, , Fonts.OpenSans_Regular, , , , DesignTypes.desComboMenuNorm, , , , , , , , , , , False, False
    ' centralise it
    CentraliseWindow WindowCount
End Sub

Public Sub CreateWindow_Target()
    CreateWindow "winTarget", "", zOrder_Win, 255, 10, 239, 78, 0, True, Fonts.OpenSans_Regular, , 2, 7, DesignTypes.desWin_AincradNoBar, DesignTypes.desWin_AincradNoBar, DesignTypes.desWin_AincradNoBar, , , , , , , , , , , GetAddress(AddressOf Target_OnDraw)

    zOrder_Con = 1

    CreatePictureBox WindowCount, "picHP_Blank", 15, 30, 209, 13, , , , , Tex_GUI(24), Tex_GUI(24), Tex_GUI(24)
    CreatePictureBox WindowCount, "picSP_Blank", 15, 47, 209, 13, , , , , Tex_GUI(25), Tex_GUI(25), Tex_GUI(25)

    CreateLabel WindowCount, "lblHP", 15, 29, 209, 12, "0/0", OpenSans_Regular, White, Alignment.alignCentre
    CreateLabel WindowCount, "lblMP", 15, 46, 209, 12, "0/0", OpenSans_Regular, White, Alignment.alignCentre

    CreateLabel WindowCount, "lblName", 0, 10, 239, 25, "Lv. 15 Minotauro Homosexual", OpenSans_Regular, White, Alignment.alignCentre

End Sub

' Rendering & Initialisation
Public Sub InitGUI()

' Starter values
    zOrder_Win = 1
    zOrder_Con = 1

    ' Menu
    CreateWindow_Login
    CreateWindow_Models
    CreateWindow_Loading
    CreateWindow_Dialogue
    CreateWindow_Classes
    CreateWindow_NewChar

    ' Game
    CreateWindow_Combobox
    CreateWindow_EscMenu
    CreateWindow_Bars
    CreateWindow_QuickSlot
    CreateWindow_Inventory
    CreateWindow_Character
    CreateWindow_ViewEquipment
    CreateWindow_Description
    CreateWindow_DragBox
    CreateWindow_Skills
    CreateWindow_Chat
    CreateWindow_ChatSmall
    CreateWindow_Options
    CreateWindow_Shop
    CreateWindow_Party
    CreateWindow_Invitations
    CreateWindow_Trade
    CreateWindow_Warehouse
    CreateWindow_Craft
    CreateWindow_Title
    CreateWindow_Achievement
    CreateWindow_Conversation

    CreateWindow_Target
    CreateWindow_DeadPanel
    CreateWindow_Services
    CreateWindow_Heraldry
    CreateWindow_ItemUpgrade
    CreateWindow_Loot
    CreateWindow_LootDice
    CreateWindow_CashShop
    CreateWindow_Mail

    ' Menus
    CreateWindow_RightClick
    CreateWindow_PlayerMenu
End Sub
