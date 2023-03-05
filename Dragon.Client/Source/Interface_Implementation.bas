Attribute VB_Name = "Interface_Implementation"
Option Explicit

' Entity Types
Public Enum EntityTypes
    EntityLabel = 1
    EntityWindow
    EntityButton
    EntityTextBox
    EntityScrollBar
    EntityPictureBox
    EntityCheckBox
    EntityComboBox
    EntityComboMenu
End Enum

' Design Types
Public Enum DesignTypes

    ' Design Windows
    DesignWindowWithoutTopBar = 1
    DesignWindowWithTopBar
    DesignWindowWithTopBarAndNavBar
    DesignWindowWithTopBarAndDoubleNavBar
    DesignWindowWithoutBackground

    ' Design Player interaction Menu
    DesignPlayerInteractionHeader
    DesignPlayerInteractionHover
    DesignPlayerInteraction

    ' Design CheckBox
    DesignCheckBox
    DesignCheckBoxChat

    ' Design Buttons
    DesignGreen
    DesignGreenHover
    DesignGreenClick
    DesignRed
    DesignRedHover
    DesignRedClick
    DesignGrey
    DesignGreyHover
    DesignGreyClick

    ' Design Pictures
    DesignBackgroundOval

    ' Design TextBox
    DesignTextBox

    ' Design Chat Open Background
    DesignOpenChat

    ' Design Chat Small Background
    DesignChatSmallShadow

    ' Design Party Background
    DesignParty

    ' Design ComboBox
    DesignComboNormal
    DesignComboMenu
    
    ' Design Blank
    DesignBlank

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
    AlignLeft = 0
    AlignRight
    AlignCenter
End Enum

' Part Types
Public Enum PartType
    PartNone = 0
    PartItem
    PartSpell
End Enum

' Origins
Public Enum PartTypeOrigins
    OriginNone = 0
    OriginInventory
    OriginQuickSlot
    OriginSpells
    OriginEnchant
    OriginWarehouse
    OriginCraft
    OriginEquipment
    OriginHeraldry
    OriginUpgrade
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
    CanDrag As Boolean
    Max As Long
    Min As Long
    Value As Long
    Text As String
    image(0 To entStates.state_Count - 1) As Long
    Design(0 To entStates.state_Count - 1) As Long
    EntityCallBack(0 To entStates.state_Count - 1) As Long
    Alpha As Long
    ClickThrough As Boolean
    xOffset As Long
    yOffset As Long
    Align As Byte
    Font As Long
    TextColour As Long
    TextColourHover As Long
    TextColourClick As Long
    zChange As Byte
    onDraw As Long
    OrigLeft As Long
    OrigTop As Long
    Tooltip As String
    Group As Long
    List() As String
    Activated As Boolean
    LinkedToWin As Long
    LinkedToCon As Long
    TextLimit As Long
    AcceptOnlyNumbers As Boolean
    ' window
    Icon As Long
    ' textbox
    isCensor As Boolean
    ' temp
    State As entStates
    MovedX As Long
    MovedY As Long
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

' Actual GUI
Public Windows() As WindowRec
Public WindowCount As Long
Public ActiveWindow As Long

' GUI Parts
Public DragBox As EntityPartRec
' Used for automatically the zOrder
Public zOrder_Win As Long
Public zOrder_Con As Long

Public Sub CreateEntity(winNum As Long, zOrder As Long, Name As String, tType As EntityTypes, ByRef Design() As Long, ByRef image() As Long, ByRef EntityCallBack() As Long, _
                        Optional Left As Long, Optional Top As Long, Optional Width As Long, Optional Height As Long, Optional Visible As Boolean = True, Optional CanDrag As Boolean, Optional Max As Long, _
                        Optional Min As Long, Optional Value As Long, Optional Text As String, Optional Align As Byte, Optional Font As Long = Fonts.FontRegular, Optional TextColour As Long = White, _
                        Optional Alpha As Long = 255, Optional ClickThrough As Boolean, Optional xOffset As Long, Optional yOffset As Long, Optional zChange As Byte, Optional ByVal Icon As Long, _
                        Optional ByVal onDraw As Long, Optional isActive As Boolean, Optional isCensor As Boolean, Optional TextColourHover As Long, Optional TextColourClick As Long, _
                        Optional Tooltip As String, Optional Group As Long, Optional Multiline As Boolean)
    Dim i As Long

    ' Check if it's a legal number
    If winNum <= 0 Or winNum > WindowCount Then
        Exit Sub
    End If

    ' Re-dim the control array
    With Windows(winNum)
        .ControlCount = .ControlCount + 1
        ReDim Preserve .Controls(1 To .ControlCount) As EntityRec
    End With

    ' Set the new control values
    With Windows(winNum).Controls(Windows(winNum).ControlCount)
        .Name = Name
        .Type = tType

        ' Loop through states
        For i = 0 To entStates.state_Count - 1
            .Design(i) = Design(i)
            .image(i) = image(i)
            .EntityCallBack(i) = EntityCallBack(i)
        Next

        .Multiline = Multiline
        .Left = Left
        .Top = Top
        .OrigLeft = Left
        .OrigTop = Top
        .Width = Width
        .Height = Height
        .Visible = Visible
        .CanDrag = CanDrag
        .Max = Max
        .Min = Min
        .Value = Value
        .Text = Text
        .Align = Align
        .Font = Font
        .TextColour = TextColour
        .TextColourHover = TextColourHover
        .TextColourClick = TextColourClick
        .Alpha = Alpha
        .ClickThrough = ClickThrough
        .xOffset = xOffset
        .yOffset = yOffset
        .zChange = zChange
        .zOrder = zOrder
        .Enabled = True
        .Icon = Icon
        .onDraw = onDraw
        .isCensor = isCensor
        .Tooltip = Tooltip
        .Group = Group
        .TextLimit = 255
        ReDim .List(0 To 0) As String
    End With

    ' Set the active control
    If isActive Then Windows(winNum).activeControl = Windows(winNum).ControlCount

    ' Set the zOrder
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

    ' Don't render anything if we don't have any containers
    If WindowCount = 0 Then Exit Sub

    ' Reset zOrder
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

    ' Check if the window exists
    If winNum <= 0 Or winNum > WindowCount Then
        Exit Sub
    End If

    ' Check if the entity exists
    If entNum <= 0 Or entNum > Windows(winNum).ControlCount Then
        Exit Sub
    End If

    ' Check the container's position
    xO = Windows(winNum).Window.Left
    yO = Windows(winNum).Window.Top

    With Windows(winNum).Controls(entNum)

        ' Find the control type

        Select Case .Type

            ' Render PictureBox
        Case EntityTypes.EntityPictureBox

            ' Render specific designs
            If .Design(.State) > 0 Then RenderDesign .Design(.State), .Left + xO, .Top + yO, .Width, .Height, .Alpha

            ' Render Image
            If .image(.State) > 0 Then RenderTexture .image(.State), .Left + xO, .Top + yO, 0, 0, .Width, .Height, .Width, .Height, DX8Colour(White, .Alpha)

            ' Render TextBox
        Case EntityTypes.EntityTextBox

            ' Render specific designs
            If .Design(.State) > 0 Then RenderDesign .Design(.State), .Left + xO, .Top + yO, .Width, .Height, .Alpha

            ' Render Image
            If .image(.State) > 0 Then RenderTexture .image(.State), .Left + xO, .Top + yO, 0, 0, .Width, .Height, .Width, .Height, DX8Colour(White, .Alpha)

            ' Render text
            If ActiveWindow = winNum And Windows(winNum).activeControl = entNum Then taddText = chatShowLine

            If .Multiline Then

                'Left Alignment
                If .Align = Alignment.AlignLeft Then

                    ' Check if need to word wrap
                    If TextWidth(Font(.Font), .Text) > .Width Then

                        ' Wrap text
                        WordWrap_Array .Text, .Width, TextArray()

                        ' Render text
                        Count = UBound(TextArray)

                        For i = 1 To Count

                            If i = Count Then
                                RenderText Font(.Font), TextArray(i) & taddText, .Left + xO, .Top + yO + yOffset, .TextColour, .Alpha
                            Else
                                RenderText Font(.Font), TextArray(i), .Left + xO, .Top + yO + yOffset, .TextColour, .Alpha
                            End If

                            yOffset = yOffset + 14
                        Next
                    Else
                        ' Just one line
                        RenderText Font(.Font), .Text & taddText, .Left + xO, .Top + yO, .TextColour, .Alpha
                    End If

                End If

                'Right Alignment
                If .Align = Alignment.AlignRight Then

                    ' Check if need to word wrap
                    If TextWidth(Font(.Font), .Text) > .Width Then

                        ' Wrap text
                        WordWrap_Array .Text, .Width, TextArray()

                        ' Render text
                        Count = UBound(TextArray)

                        For i = 1 To Count
                            Left = .Left + .Width - TextWidth(Font(.Font), TextArray(i))

                            If i = Count Then
                                RenderText Font(.Font), TextArray(i) & taddText, Left + xO, .Top + yO + yOffset, .TextColour, .Alpha
                            Else
                                RenderText Font(.Font), TextArray(i), Left + xO, .Top + yO + yOffset, .TextColour, .Alpha
                            End If

                            yOffset = yOffset + 14
                        Next
                    Else
                        ' Just one line
                        Left = .Left + .Width - TextWidth(Font(.Font), .Text)
                        RenderText Font(.Font), .Text & taddText, Left + xO, .Top + yO, .TextColour, .Alpha
                    End If

                End If

                'Center Alignment
                If .Align = Alignment.AlignCenter Then

                    ' Check if need to word wrap
                    If TextWidth(Font(.Font), .Text) > .Width Then

                        ' Wrap text
                        WordWrap_Array .Text, .Width, TextArray()

                        ' Render text
                        Count = UBound(TextArray)

                        For i = 1 To Count
                            Left = .Left + (.Width \ 2) - (TextWidth(Font(.Font), TextArray(i)) \ 2)

                            If i = Count Then
                                RenderText Font(.Font), TextArray(i) & taddText, Left + xO, .Top + yO + yOffset, .TextColour, .Alpha
                            Else
                                RenderText Font(.Font), TextArray(i), Left + xO, .Top + yO + yOffset, .TextColour, .Alpha
                            End If

                            yOffset = yOffset + 14
                        Next
                    Else
                        ' Just one line
                        Left = .Left + (.Width \ 2) - (TextWidth(Font(.Font), .Text) \ 2)
                        RenderText Font(.Font), .Text & taddText, Left + xO, .Top + yO, .TextColour, .Alpha

                    End If

                End If
            End If

            ' Multiline Alignment
            If Not .Multiline Then
                If .Align = Alignment.AlignCenter Then

                    ' Calculate the horizontal centre
                    Width = TextWidth(Font(.Font), .Text)

                    If Width > .Width Then
                        hor_centre = .Left + xO    '+ xOffset
                    Else
                        hor_centre = .Left + xO + ((.Width - Width) \ 2)
                    End If

                    ' Left Side
                ElseIf .Align = Alignment.AlignLeft Then
                    hor_centre = .Left + xO + .xOffset
                End If

                ' If it's censored then render censored
                If Not .isCensor Then
                    RenderText Font(.Font), .Text & taddText, hor_centre, .Top + yO + .yOffset, .TextColour
                Else

                    If Alignment.AlignCenter Then
                        RenderText Font(.Font), CensorWord(.Text) & taddText, hor_centre, .Top + yO + .yOffset, .TextColour

                    ElseIf Alignment.AlignLeft Then
                        RenderText Font(.Font), CensorWord(.Text) & taddText, .Left + xO + .xOffset, .Top + yO + .yOffset, .TextColour

                    ElseIf Alignment.AlignRight Then
                        RenderText Font(.Font), CensorWord(.Text) & taddText, .Left + xO + .xOffset, .Top + yO + .yOffset, .TextColour
                    End If

                    'Else
                    'RenderText Font(.Font), CensorWord(.Text) & taddText, .Left + xO + .xOffset, .Top + yO + .yOffset, .TextColour

                End If

            End If

            ' Render Buttons
        Case EntityTypes.EntityButton

            ' Render specific designs
            If .Design(.State) > 0 Then
                If .Design(.State) > 0 Then
                    RenderDesign .Design(.State), .Left + xO, .Top + yO, .Width, .Height
                End If
            End If

            ' Render Image
            If .image(.State) > 0 Then
                If .image(.State) > 0 Then
                    RenderTexture .image(.State), .Left + xO, .Top + yO, 0, 0, .Width, .Height, .Width, .Height
                End If
            End If

            ' For changing the text space
            xOffset = Width

            ' Calculate the vertical centre
            Height = TextHeight(Font(Fonts.FontRegular))
            If Height > .Height Then
                ver_centre = .Top + yO
            Else
                ver_centre = .Top + yO + ((.Height - Height) \ 2) + 1
            End If

            ' Calculate the horizontal centre
            Width = TextWidth(Font(.Font), .Text)
            If Width > .Width Then
                hor_centre = .Left + xO + xOffset
            Else
                hor_centre = .Left + xO + xOffset + ((.Width - Width - xOffset) \ 2)
            End If

            ' Get the colour
            If .State = Hover Then
                Colour = .TextColourHover
            ElseIf .State = MouseDown Then
                Colour = .TextColourClick
            Else
                Colour = .TextColour
            End If

            RenderText Font(.Font), .Text, hor_centre, ver_centre, Colour

            ' Render Labels
        Case EntityTypes.EntityLabel

            If Len(.Text) > 0 Then

                Select Case .Align

                Case Alignment.AlignLeft

                    ' Check if need to word wrap
                    If TextWidth(Font(.Font), .Text) > .Width Then

                        ' Wrap text
                        WordWrap_Array .Text, .Width, TextArray()

                        ' Render text
                        Count = UBound(TextArray)

                        For i = 1 To Count
                            RenderText Font(.Font), TextArray(i), .Left + xO, .Top + yO + yOffset, .TextColour, .Alpha
                            yOffset = yOffset + 14
                        Next
                    Else
                        ' Just one line
                        RenderText Font(.Font), .Text, .Left + xO, .Top + yO, .TextColour, .Alpha
                    End If

                Case Alignment.AlignRight

                    ' Check if need to word wrap
                    If TextWidth(Font(.Font), .Text) > .Width Then

                        ' Wrap text
                        WordWrap_Array .Text, .Width, TextArray()

                        ' Render text
                        Count = UBound(TextArray)

                        For i = 1 To Count
                            Left = .Left + .Width - TextWidth(Font(.Font), TextArray(i))
                            RenderText Font(.Font), TextArray(i), Left + xO, .Top + yO + yOffset, .TextColour, .Alpha
                            yOffset = yOffset + 14
                        Next
                    Else
                        ' Just one line
                        Left = .Left + .Width - TextWidth(Font(.Font), .Text)
                        RenderText Font(.Font), .Text, Left + xO, .Top + yO, .TextColour, .Alpha
                    End If

                Case Alignment.AlignCenter

                    ' Check if need to word wrap
                    If TextWidth(Font(.Font), .Text) > .Width Then

                        ' Wrap text
                        WordWrap_Array .Text, .Width, TextArray()

                        ' Render text
                        Count = UBound(TextArray)

                        For i = 1 To Count
                            Left = .Left + (.Width \ 2) - (TextWidth(Font(.Font), TextArray(i)) \ 2)
                            RenderText Font(.Font), TextArray(i), Left + xO, .Top + yO + yOffset, .TextColour, .Alpha
                            yOffset = yOffset + 14
                        Next
                    Else
                        ' Just one line
                        Left = .Left + (.Width \ 2) - (TextWidth(Font(.Font), .Text) \ 2)
                        RenderText Font(.Font), .Text, Left + xO, .Top + yO, .TextColour, .Alpha
                    End If

                End Select
            End If

            ' Render Checkboxes
        Case EntityTypes.EntityCheckBox

            Select Case .Design(0)

            Case DesignTypes.DesignCheckBox

                ' Empty?
                If .Value = 0 Then texNum = Tex_GUI(1) Else texNum = Tex_GUI(2)

                ' Render box
                RenderTexture texNum, .Left + xO, .Top + yO, 0, 0, 18, 18, 18, 18

                ' Find text position
                Select Case .Align

                Case Alignment.AlignLeft
                    Left = .Left + 18 + xO

                Case Alignment.AlignRight
                    Left = .Left + 18 + (.Width - 18) - TextWidth(Font(.Font), .Text) + xO

                Case Alignment.AlignCenter
                    Left = .Left + 18 + ((.Width - 18) / 2) - (TextWidth(Font(.Font), .Text) / 2) + xO
                End Select

                ' Render text
                RenderText Font(.Font), .Text, Left, .Top + yO, .TextColour, .Alpha

            Case DesignTypes.DesignCheckBoxChat

                If .Value = 0 Then .Alpha = 150 Else .Alpha = 255

                ' Render box
                RenderTexture Tex_GUI(36), .Left + xO, .Top + yO, 0, 0, 49, 23, 49, 23, DX8Colour(White, .Alpha)

                ' Render text
                Left = .Left + (49 / 2) - (TextWidth(Font(.Font), .Text) / 2) + xO

                ' Render text
                RenderText Font(.Font), .Text, Left, .Top + yO + 4, .TextColour, .Alpha
            End Select

            ' Render ComboBoxes
        Case EntityTypes.EntityComboBox

            Select Case .Design(0)

            Case DesignTypes.DesignComboNormal

                ' Draw the background
                RenderDesign DesignTypes.DesignTextBox, .Left + xO, .Top + yO, .Width, .Height

                ' Render the text
                If .Value > 0 Then
                    If .Value <= UBound(.List) Then
                        RenderText Font(.Font), .List(.Value), .Left + xO + 5, .Top + yO + 3, White
                    End If
                End If

                ' Draw the little arow
                RenderTexture Tex_GUI(43), .Left + xO + .Width - 11, .Top + yO + 7, 0, 0, 5, 4, 5, 4

            End Select
        End Select

        ' Callback draw
        callBack = .onDraw

        If callBack <> 0 Then EntityCallBack callBack, winNum, entNum, 0, 0
    End With

End Sub

Public Sub RenderWindow(winNum As Long)
    Dim Width As Long, Height As Long, callBack As Long, X As Long, Y As Long, i As Long, Left As Long
    Dim Size As Long

    ' Check if the window exists
    If winNum <= 0 Or winNum > WindowCount Then
        Exit Sub
    End If

    With Windows(winNum).Window

        Select Case .Design(0)

        Case DesignTypes.DesignComboMenu
            RenderTexture Tex_Blank, .Left, .Top, 0, 0, .Width, .Height, 1, 1, DX8Colour(Black, 157)

            ' Text
            If UBound(.List) > 0 Then
                Y = .Top + 2
                X = .Left

                For i = 1 To UBound(.List)

                    ' Render select
                    If i = .Value Or i = .Group Then RenderTexture Tex_Blank, X, Y - 1, 0, 0, .Width, 15, 1, 1, DX8Colour(Black, 255)

                    ' Render text
                    Left = X + (.Width \ 2) - (TextWidth(Font(.Font), .List(i)) \ 2)

                    If i = .Value Or i = .Group Then
                        RenderText Font(.Font), .List(i), Left, Y, Yellow
                    Else
                        RenderText Font(.Font), .List(i), Left, Y, White
                    End If

                    Y = Y + 16
                Next

            End If
            Exit Sub
        End Select

        Select Case .Design(.State)

            ' Render Window Player Interaction
        Case DesignTypes.DesignPlayerInteraction

            ' Render Window
            RenderDesign DesignTypes.DesignPlayerInteraction, .Left, .Top, .Width, .Height

            ' Render Window Small Chat
        Case DesignChatSmallShadow

            ' Render Window
            RenderDesign DesignTypes.DesignChatSmallShadow, .Left, .Top, .Width, .Height

            ' Render Window Party
        Case DesignParty

            ' Render Window
            RenderDesign DesignTypes.DesignParty, .Left, .Top, .Width, .Height

            ' Render Window Without Top Bar
        Case DesignWindowWithoutTopBar

            ' Render Background
            RenderDesign DesignTypes.DesignWindowWithoutTopBar, .Left, .Top, .Width, .Height, 255

            ' Check for size
            Size = TextWidth(Font(.Font), Trim$(.Text))

            ' render the caption
            RenderText Font(.Font), Trim$(.Text), .Left + (.Width * 0.5) - (Size * 0.5), .Top + 15, .TextColour

            ' Render Window With Top Bar
        Case DesignWindowWithTopBar

            ' Render Background
            RenderDesign DesignTypes.DesignWindowWithTopBar, .Left, .Top, .Width, .Height, 255

            ' Check for size
            Size = TextWidth(Font(.Font), Trim$(.Text))

            ' render the caption
            RenderText Font(.Font), Trim$(.Text), .Left + (.Width * 0.5) - (Size * 0.5), .Top + 15, .TextColour

            ' Render Window With TopBar and NavBar
        Case DesignWindowWithTopBarAndNavBar

            ' Render Background
            RenderDesign DesignTypes.DesignWindowWithTopBarAndNavBar, .Left, .Top, .Width, .Height, 255

            ' Check for size
            Size = TextWidth(Font(.Font), Trim$(.Text))

            ' render the caption
            RenderText Font(.Font), Trim$(.Text), .Left + (.Width * 0.5) - (Size * 0.5), .Top + 15, .TextColour

            ' Render Window With Top Bar and Double NavBar
        Case DesignWindowWithTopBarAndDoubleNavBar

            ' Render Background
            RenderDesign DesignTypes.DesignWindowWithTopBarAndDoubleNavBar, .Left, .Top, .Width, .Height, 255

            ' Check for size
            Size = TextWidth(Font(.Font), Trim$(.Text))

            ' render the caption
            RenderText Font(.Font), Trim$(.Text), .Left + (.Width * 0.5) - (Size * 0.5), .Top + 15, .TextColour

            ' Render Window Without Background
        Case DesignWindowWithoutBackground

            ' Render Background
            RenderDesign DesignTypes.DesignWindowWithoutBackground, .Left, .Top, .Width, .Height, 255
            
        Case DesignBlank
            
            ' Render Background
            RenderDesign DesignTypes.DesignBlank, .Left, .Top, .Width, .Height, 220


        End Select

        ' OnDraw call back
        callBack = .onDraw

        If callBack <> 0 Then EntityCallBack callBack, winNum, 0, 0, 0
    End With

End Sub

Public Sub RenderDesign(Design As Long, Left As Long, Top As Long, Width As Long, Height As Long, Optional Alpha As Long = 255)
    Dim Colour As Long
    Dim BorderSide As Long

    Colour = DX8Colour(White, Alpha)

    Select Case Design

        ' Render Design (Button) Green
    Case DesignTypes.DesignGreen
        BorderSide = 2

        ' Render Green Border
        RenderEntity_Square TextureDesign(TextureDesignBorderGreen), Left, Top, Width, Height, BorderSide, Alpha

        ' Render Green Gradient Normal
        RenderTexture Tex_Gradient(1), Left + BorderSide, Top + BorderSide, 0, 0, Width - (BorderSide * 2), Height - (BorderSide * 2), 128, 128, Colour

        ' Render Design (Button) Green Hover
    Case DesignTypes.DesignGreenHover
        BorderSide = 2

        ' Render Green Border
        RenderEntity_Square TextureDesign(TextureDesignBorderGreen), Left, Top, Width, Height, BorderSide, Alpha

        ' Render Green Gradient Hover
        RenderTexture Tex_Gradient(2), Left + BorderSide, Top + BorderSide, 0, 0, Width - (BorderSide * 2), Height - (BorderSide * 2), 128, 128, Colour

        ' Render Design (Button) Green Click
    Case DesignTypes.DesignGreenClick
        BorderSide = 2

        ' Render Green Border
        RenderEntity_Square TextureDesign(TextureDesignBorderGreen), Left, Top, Width, Height, BorderSide, Alpha

        ' Render Green Gradient Click
        RenderTexture Tex_Gradient(3), Left + BorderSide, Top + BorderSide, 0, 0, Width - (BorderSide * 2), Height - (BorderSide * 2), 128, 128, Colour

        ' Render Design (Button) Red
    Case DesignTypes.DesignRed
        BorderSide = 2

        ' Render Red Border
        RenderEntity_Square TextureDesign(TextureDesignBorderRed), Left, Top, Width, Height, BorderSide, Alpha

        ' Render Red Gradient Normal
        RenderTexture Tex_Gradient(4), Left + BorderSide, Top + BorderSide, 0, 0, Width - (BorderSide * 2), Height - (BorderSide * 2), 128, 128, Colour

        ' Render Design (Button) Red Hover
    Case DesignTypes.DesignRedHover
        BorderSide = 2

        ' Render Red Border
        RenderEntity_Square TextureDesign(TextureDesignBorderRed), Left, Top, Width, Height, BorderSide, Alpha

        ' Render Red Gradient Hover
        RenderTexture Tex_Gradient(5), Left + BorderSide, Top + BorderSide, 0, 0, Width - (BorderSide * 2), Height - (BorderSide * 2), 128, 128, Colour

        ' Render Design (Button) Red Click
    Case DesignTypes.DesignRedClick
        BorderSide = 2

        ' Render Red Border Click
        RenderEntity_Square TextureDesign(TextureDesignBorderRed), Left, Top, Width, Height, BorderSide, Alpha

        ' Render Red Gradient
        RenderTexture Tex_Gradient(6), Left + BorderSide, Top + BorderSide, 0, 0, Width - (BorderSide * 2), Height - (BorderSide * 2), 128, 128, Colour

        ' Render Design (Button) Grey
    Case DesignTypes.DesignGrey
        BorderSide = 2

        ' Render Grey Border
        RenderEntity_Square TextureDesign(TextureDesignBorderGrey), Left, Top, Width, Height, BorderSide, Alpha

        ' Render Grey Gradient Normal
        RenderTexture Tex_Gradient(7), Left + BorderSide, Top + BorderSide, 0, 0, Width - (BorderSide * 2), Height - (BorderSide * 2), 128, 128, Colour

        ' Render Design (Button) Grey Hover
    Case DesignTypes.DesignGreyHover
        BorderSide = 2

        ' Render Grey Border Hover
        RenderEntity_Square TextureDesign(TextureDesignBorderGrey), Left, Top, Width, Height, BorderSide, Alpha

        ' Render Grey Gradient
        RenderTexture Tex_Gradient(8), Left + BorderSide, Top + BorderSide, 0, 0, Width - (BorderSide * 2), Height - (BorderSide * 2), 128, 128, Colour

        ' Render Design (Button) Grey Click
    Case DesignTypes.DesignGreyClick
        BorderSide = 2

        ' Render Grey Border
        RenderEntity_Square TextureDesign(TextureDesignBorderGrey), Left, Top, Width, Height, BorderSide, Alpha

        ' Render Grey Gradient Click
        RenderTexture Tex_Gradient(9), Left + BorderSide, Top + BorderSide, 0, 0, Width - (BorderSide * 2), Height - (BorderSide * 2), 128, 128, Colour

        ' Render Design Background Oval
    Case DesignTypes.DesignBackgroundOval
        BorderSide = 4

        ' Render the Background Oval Texture
        RenderEntity_Square TextureDesign(TextureDesignBackgroundOval), Left, Top, Width, Height, BorderSide, Alpha

        ' Render Design TextBox
    Case DesignTypes.DesignTextBox
        BorderSide = 2

        ' Render TextBox Texture
        RenderEntity_Square TextureDesign(TextureDesignTextBox), Left, Top, Width, Height, BorderSide, Alpha

        ' Render Design Player Interaction Header (Green)
    Case DesignTypes.DesignPlayerInteractionHeader
        RenderTexture Tex_Blank, Left, Top, 0, 0, Width, Height, 32, 32, D3DColorARGB(200, 47, 77, 29)

        ' Render Design Player Interaction (Grey)
    Case DesignTypes.DesignPlayerInteractionHover
        RenderTexture Tex_Blank, Left, Top, 0, 0, Width, Height, 32, 32, D3DColorARGB(200, 98, 98, 98)

        ' Render Design Player Interaction Window
    Case DesignTypes.DesignPlayerInteraction
        BorderSide = 8

        ' Render Background Texture
        RenderEntity_Square TextureDesign(TextureDesignWindowPlayerInteraction), Left, Top, Width, Height, BorderSide, Alpha

        ' Render Design Chat Open background
    Case DesignTypes.DesignOpenChat
        BorderSide = 8

        ' Render Background Texture
        RenderEntity_Square TextureDesign(TextureDesignWindowChatOpenBackground), Left, Top, Width, Height, BorderSide, Alpha

        ' Render Design Small Chat
    Case DesignTypes.DesignChatSmallShadow
        BorderSide = 35
        ' render the green box
        RenderEntity_Square TextureDesign(TextureDesignWindowChatSmallShadow), Left - BorderSide, Top - BorderSide, Width + (BorderSide * 2), Height + (BorderSide * 2), BorderSide, Alpha

        ' Render Design Party
    Case DesignTypes.DesignParty
        BorderSide = 12
        ' render black square
        RenderEntity_Square TextureDesign(TextureDesignWindowParty), Left, Top, Width, Height, BorderSide, Alpha

        ' Render Design Window Without TopBar
    Case DesignWindowWithoutTopBar
        BorderSide = 2

        ' Render background
        RenderEntity_Square TextureDesign(TextureDesignBackground), Left, Top, Width, Height, BorderSide, 230

        ' Render Border
        RenderEntity_Square TextureDesign(TextureDesignBorder), Left, Top, Width, Height, BorderSide, Alpha

        ' Render Design Window With TopBar
    Case DesignWindowWithTopBar
        BorderSide = 2

        ' Render background
        RenderEntity_Square TextureDesign(TextureDesignBackground), Left, Top + (BorderSide * 20), Width, Height - (BorderSide * 20), BorderSide, 230

        ' Render Border
        RenderEntity_Square TextureDesign(TextureDesignBorder), Left, Top, Width, Height, BorderSide, Alpha

        ' Render Top Bar
        RenderTexture TextureDesign(TextureDesignTopBar), Left + BorderSide, Top + BorderSide, 0, 0, Width - (BorderSide * 2), (BorderSide * 20), (BorderSide * 20), (BorderSide * 20), D3DColorARGB(240, 255, 255, 255)

        ' Render Separator
        RenderEntity_Square TextureDesign(TextureDesignWindowSeparator), Left + 2, Top + (BorderSide * 20), Width - 4, 2, BorderSide, Alpha

        ' Render Design Window Without TopBar and NavBar
    Case DesignWindowWithTopBarAndNavBar
        BorderSide = 2

        ' Render background
        RenderEntity_Square TextureDesign(TextureDesignBackground), Left, Top + (BorderSide * 20), Width, Height - (BorderSide * 20), BorderSide, 230

        ' Render Border
        RenderEntity_Square TextureDesign(TextureDesignBorder), Left, Top, Width, Height, BorderSide, Alpha

        ' Render Top Bar
        RenderTexture TextureDesign(TextureDesignTopBar), Left + BorderSide, Top + BorderSide, 0, 0, Width - (BorderSide * 2), (BorderSide * 20), (BorderSide * 20), (BorderSide * 20), D3DColorARGB(240, 255, 255, 255)

        ' Render Separator
        RenderEntity_Square TextureDesign(TextureDesignWindowSeparator), Left + 2, Top + (BorderSide * 20), Width - 4, 2, BorderSide, Alpha

        ' Render NavBar
        RenderEntity_Square TextureDesign(TextureDesignNavBar), Left + 2, Top + (BorderSide * 20) + 2, Width - 4, 24, BorderSide, 90

        ' Render Separator
        RenderEntity_Square TextureDesign(TextureDesignWindowSeparator), Left + 2, Top + (BorderSide * 32) + 2, Width - 4, 2, BorderSide, Alpha

        ' Render Design Window Without TopBar and Double NavBar
    Case DesignWindowWithTopBarAndDoubleNavBar
        BorderSide = 2

        ' Render background
        RenderEntity_Square TextureDesign(TextureDesignBackground), Left, Top + (BorderSide * 20), Width, Height - (BorderSide * 20), BorderSide, 230

        ' Render Border
        RenderEntity_Square TextureDesign(TextureDesignBorder), Left, Top, Width, Height, BorderSide, Alpha

        ' Render Top Bar
        RenderTexture TextureDesign(TextureDesignTopBar), Left + BorderSide, Top + BorderSide, 0, 0, Width - (BorderSide * 2), (BorderSide * 20), (BorderSide * 20), (BorderSide * 20), D3DColorARGB(240, 255, 255, 255)

        ' Render Separator
        RenderEntity_Square TextureDesign(TextureDesignWindowSeparator), Left + 2, Top + (BorderSide * 20), Width - 4, 2, BorderSide, Alpha

        ' Render NavBar
        RenderEntity_Square TextureDesign(TextureDesignNavBar), Left + 2, Top + (BorderSide * 20) + 2, Width - 4, 24, BorderSide, 90

        ' Render Separator
        RenderEntity_Square TextureDesign(TextureDesignWindowSeparator), Left + 2, Top + (BorderSide * 32) + 2, Width - 4, 2, BorderSide, Alpha

        ' Render NavBar
        RenderEntity_Square TextureDesign(TextureDesignNavBar), Left + 2, Top + (BorderSide * 34), Width - 4, 24, BorderSide, 90

        ' Render Separator
        RenderEntity_Square TextureDesign(TextureDesignWindowSeparator), Left + 2, Top + (BorderSide * 46), Width - 4, 2, BorderSide, Alpha

        ' Render Design Window Without Background
    Case DesignWindowWithoutBackground

        ' Render background
        RenderEntity_Square TextureDesign(TextureDesign_None), Left, Top, Width, Height, 0, 230
        
    Case DesignBlank
    
        'Render Background
        RenderTexture Tex_Blank, Left, Top, 0, 0, Width, Height, Width, Height, D3DColorARGB(Alpha, 10, 10, 10)


    End Select

End Sub

Public Sub RenderEntity_Square(TextureNum As Long, X As Long, Y As Long, Width As Long, Height As Long, BorderSize As Long, Optional Alpha As Long = 255)
    Dim BorderSide As Long, Colour As Long
    Colour = DX8Colour(White, Alpha)

    ' Set the border size
    BorderSide = BorderSize

    ' Draw centre
    RenderTexture TextureNum, X + BorderSide, Y + BorderSide, BorderSide + 1, BorderSide + 1, Width - (BorderSide * 2), Height - (BorderSide * 2), 1, 1, Colour

    ' Draw top side
    RenderTexture TextureNum, X + BorderSide, Y, BorderSide, 0, Width - (BorderSide * 2), BorderSide, 1, BorderSide, Colour

    ' Draw left side
    RenderTexture TextureNum, X, Y + BorderSide, 0, BorderSide, BorderSide, Height - (BorderSide * 2), BorderSide, 1, Colour

    ' Draw right side
    RenderTexture TextureNum, X + Width - BorderSide, Y + BorderSide, BorderSide + 3, BorderSide, BorderSide, Height - (BorderSide * 2), BorderSide, 1, Colour

    ' Draw bottom side
    RenderTexture TextureNum, X + BorderSide, Y + Height - BorderSide, BorderSide, BorderSide + 3, Width - (BorderSide * 2), BorderSide, 1, BorderSide, Colour

    ' Draw top left corner
    RenderTexture TextureNum, X, Y, 0, 0, BorderSide, BorderSide, BorderSide, BorderSide, Colour

    ' Draw top right corner
    RenderTexture TextureNum, X + Width - BorderSide, Y, BorderSide + 3, 0, BorderSide, BorderSide, BorderSide, BorderSide, Colour

    ' Draw bottom left corner
    RenderTexture TextureNum, X, Y + Height - BorderSide, 0, BorderSide + 3, BorderSide, BorderSide, BorderSide, BorderSide, Colour

    ' Draw bottom right corner
    RenderTexture TextureNum, X + Width - BorderSide, Y + Height - BorderSide, BorderSide + 3, BorderSide + 3, BorderSide, BorderSide, BorderSide, BorderSide, Colour
End Sub

Sub Combobox_AddItem(WinIndex As Long, ControlIndex As Long, Text As String)
    Dim Count As Long
    Count = UBound(Windows(WinIndex).Controls(ControlIndex).List)
    ReDim Preserve Windows(WinIndex).Controls(ControlIndex).List(0 To Count + 1)
    Windows(WinIndex).Controls(ControlIndex).List(Count + 1) = Text
End Sub

Public Sub CreateWindow(Name As String, caption As String, zOrder As Long, Left As Long, Top As Long, Width As Long, Height As Long, Icon As Long, _
                        Optional Visible As Boolean = True, Optional Font As Long = Fonts.FontRegular, Optional TextColour As Long = White, Optional xOffset As Long, _
                        Optional yOffset As Long, Optional DesignNormal As Long, Optional DesignHover As Long, Optional DesignMouseDown As Long, Optional ImageNormal As Long, _
                        Optional ImageHover As Long, Optional ImageMouseDown As Long, Optional EntityCallBackNormal As Long, Optional EntityCallBackHover As Long, Optional EntityCallBackMouseDown As Long, _
                        Optional EntityCallBackMouseMove As Long, Optional EntityCallBackDoubleClick As Long, Optional CanDrag As Boolean = True, Optional zChange As Byte = True, Optional ByVal onDraw As Long, _
                        Optional isActive As Boolean, Optional ClickThrough As Boolean)

    Dim i As Long
    Dim Design(0 To entStates.state_Count - 1) As Long
    Dim image(0 To entStates.state_Count - 1) As Long
    Dim EntityCallBack(0 To entStates.state_Count - 1) As Long

    ' Fill temp arrays
    Design(entStates.Normal) = DesignNormal
    Design(entStates.Hover) = DesignHover
    Design(entStates.MouseDown) = DesignMouseDown
    Design(entStates.DblClick) = DesignNormal
    Design(entStates.MouseUp) = DesignNormal
    image(entStates.Normal) = ImageNormal
    image(entStates.Hover) = ImageHover
    image(entStates.MouseDown) = ImageMouseDown
    image(entStates.DblClick) = ImageNormal
    image(entStates.MouseUp) = ImageNormal
    EntityCallBack(entStates.Normal) = EntityCallBackNormal
    EntityCallBack(entStates.Hover) = EntityCallBackHover
    EntityCallBack(entStates.MouseDown) = EntityCallBackMouseDown
    EntityCallBack(entStates.MouseMove) = EntityCallBackMouseMove
    EntityCallBack(entStates.DblClick) = EntityCallBackDoubleClick

    ' Redim the windows
    WindowCount = WindowCount + 1
    ReDim Preserve Windows(1 To WindowCount) As WindowRec

    ' Set the properties
    With Windows(WindowCount).Window
        .Name = Name
        .Type = EntityTypes.EntityWindow

        ' Loop through states
        For i = 0 To entStates.state_Count - 1
            .Design(i) = Design(i)
            .image(i) = image(i)
            .EntityCallBack(i) = EntityCallBack(i)
        Next

        .Left = Left
        .Top = Top
        .OrigLeft = Left
        .OrigTop = Top
        .Width = Width
        .Height = Height
        .Visible = Visible
        .CanDrag = CanDrag
        .Text = caption
        .Font = Font
        .TextColour = TextColour
        .xOffset = xOffset
        .yOffset = yOffset
        .Icon = Icon
        .Enabled = True
        .zChange = zChange
        .zOrder = zOrder
        .onDraw = onDraw
        .ClickThrough = ClickThrough

        ' Set active
        If .Visible Then ActiveWindow = WindowCount
    End With

    ' Set the zOrder
    zOrder_Win = zOrder_Win + 1
End Sub

Public Sub CreateTextbox(winNum As Long, Name As String, Left As Long, Top As Long, Width As Long, Height As Long, Optional Text As String, Optional Font As Long = Fonts.FontRegular, _
                         Optional TextColour As Long = White, Optional Align As Byte = Alignment.AlignLeft, Optional Visible As Boolean = True, Optional Alpha As Long = 255, Optional ImageNormal As Long, _
                         Optional ImageHover As Long, Optional ImageMouseDown As Long, Optional DesignNormal As Long, Optional DesignHover As Long, Optional DesignMouseDown As Long, _
                         Optional EntityCallBackNormal As Long, Optional EntityCallBackHover As Long, Optional EntityCallBackMouseDown As Long, Optional EntityCallBackMouseMove As Long, Optional EntityCallBackDoubleClick As Long, _
                         Optional isActive As Boolean, Optional xOffset As Long, Optional yOffset As Long, Optional isCensor As Boolean, Optional EntityCallBack_enter As Long, Optional Multiline As Boolean)
    Dim Design(0 To entStates.state_Count - 1) As Long
    Dim image(0 To entStates.state_Count - 1) As Long
    Dim EntityCallBack(0 To entStates.state_Count - 1) As Long

    ' Fill temp arrays
    Design(entStates.Normal) = DesignNormal
    Design(entStates.Hover) = DesignHover
    Design(entStates.MouseDown) = DesignMouseDown
    image(entStates.Normal) = ImageNormal
    image(entStates.Hover) = ImageHover
    image(entStates.MouseDown) = ImageMouseDown
    EntityCallBack(entStates.Normal) = EntityCallBackNormal
    EntityCallBack(entStates.Hover) = EntityCallBackHover
    EntityCallBack(entStates.MouseDown) = EntityCallBackMouseDown
    EntityCallBack(entStates.MouseMove) = EntityCallBackMouseMove
    EntityCallBack(entStates.DblClick) = EntityCallBackDoubleClick
    EntityCallBack(entStates.Enter) = EntityCallBack_enter

    ' Create the textbox
    CreateEntity winNum, zOrder_Con, Name, EntityTextBox, Design(), image(), EntityCallBack(), Left, Top, Width, Height, Visible, , , , , Text, Align, Font, TextColour, Alpha, , xOffset, yOffset, , , , isActive, isCensor, , , , , Multiline
End Sub

Public Sub CreatePictureBox(winNum As Long, Name As String, Left As Long, Top As Long, Width As Long, Height As Long, Optional Visible As Boolean = True, Optional CanDrag As Boolean, _
                            Optional Alpha As Long = 255, Optional ClickThrough As Boolean, Optional ImageNormal As Long, Optional ImageHover As Long, Optional ImageMouseDown As Long, Optional DesignNormal As Long, _
                            Optional DesignHover As Long, Optional DesignMouseDown As Long, Optional EntityCallBackNormal As Long, Optional EntityCallBackHover As Long, Optional EntityCallBackMouseDown As Long, _
                            Optional EntityCallBackMouseMove As Long, Optional EntityCallBackDoubleClick As Long, Optional onDraw As Long)
    Dim Design(0 To entStates.state_Count - 1) As Long
    Dim image(0 To entStates.state_Count - 1) As Long
    Dim EntityCallBack(0 To entStates.state_Count - 1) As Long

    ' Fill temp arrays
    Design(entStates.Normal) = DesignNormal
    Design(entStates.Hover) = DesignHover
    Design(entStates.MouseDown) = DesignMouseDown
    image(entStates.Normal) = ImageNormal
    image(entStates.Hover) = ImageHover
    image(entStates.MouseDown) = ImageMouseDown
    EntityCallBack(entStates.Normal) = EntityCallBackNormal
    EntityCallBack(entStates.Hover) = EntityCallBackHover
    EntityCallBack(entStates.MouseDown) = EntityCallBackMouseDown
    EntityCallBack(entStates.MouseMove) = EntityCallBackMouseMove
    EntityCallBack(entStates.DblClick) = EntityCallBackDoubleClick

    ' Create the box
    CreateEntity winNum, zOrder_Con, Name, EntityPictureBox, Design(), image(), EntityCallBack(), Left, Top, Width, Height, Visible, CanDrag, , , , , , , , Alpha, ClickThrough, , , , , onDraw
End Sub

Public Sub CreateButton(winNum As Long, Name As String, Left As Long, Top As Long, Width As Long, Height As Long, Optional Text As String, Optional Font As Fonts = Fonts.FontRegular, _
                        Optional TextColour As Long = White, Optional Icon As Long, Optional Visible As Boolean = True, Optional Alpha As Long = 255, Optional ImageNormal As Long, Optional ImageHover As Long, _
                        Optional ImageMouseDown As Long, Optional DesignNormal As Long, Optional DesignHover As Long, Optional DesignMouseDown As Long, Optional EntityCallBackNormal As Long, _
                        Optional EntityCallBackHover As Long, Optional EntityCallBackMouseDown As Long, Optional EntityCallBackMouseMove As Long, Optional EntityCallBackDoubleClick As Long, Optional xOffset As Long, _
                        Optional yOffset As Long, Optional TextColourHover As Long = -1, Optional TextColourClick As Long = -1, Optional Tooltip As String)
    Dim Design(0 To entStates.state_Count - 1) As Long
    Dim image(0 To entStates.state_Count - 1) As Long
    Dim EntityCallBack(0 To entStates.state_Count - 1) As Long

    ' Default the colours
    If TextColourHover = -1 Then TextColourHover = TextColour
    If TextColourClick = -1 Then TextColourClick = TextColour

    ' Fill temp arrays
    Design(entStates.Normal) = DesignNormal
    Design(entStates.Hover) = DesignHover
    Design(entStates.MouseDown) = DesignMouseDown
    image(entStates.Normal) = ImageNormal
    image(entStates.Hover) = ImageHover
    image(entStates.MouseDown) = ImageMouseDown
    EntityCallBack(entStates.Normal) = EntityCallBackNormal
    EntityCallBack(entStates.Hover) = EntityCallBackHover
    EntityCallBack(entStates.MouseDown) = EntityCallBackMouseDown
    EntityCallBack(entStates.MouseMove) = EntityCallBackMouseMove
    EntityCallBack(entStates.DblClick) = EntityCallBackDoubleClick

    ' Create the box
    CreateEntity winNum, zOrder_Con, Name, EntityButton, Design(), image(), EntityCallBack(), Left, Top, Width, Height, Visible, , , , , Text, , Font, TextColour, Alpha, , xOffset, yOffset, , Icon, , , , TextColourHover, TextColourClick, Tooltip
End Sub

Public Sub CreateLabel(winNum As Long, Name As String, Left As Long, Top As Long, Width As Long, Optional Height As Long, Optional Text As String, Optional Font As Fonts = Fonts.FontRegular, _
                       Optional TextColour As Long = White, Optional Align As Byte = Alignment.AlignLeft, Optional Visible As Boolean = True, Optional Alpha As Long = 255, Optional ClickThrough As Boolean, _
                       Optional EntityCallBackNormal As Long, Optional EntityCallBackHover As Long, Optional EntityCallBackMouseDown As Long, Optional EntityCallBackMouseMove As Long, Optional EntityCallBackDoubleClick As Long)
    Dim Design(0 To entStates.state_Count - 1) As Long
    Dim image(0 To entStates.state_Count - 1) As Long
    Dim EntityCallBack(0 To entStates.state_Count - 1) As Long

    ' Fill temp arrays
    EntityCallBack(entStates.Normal) = EntityCallBackNormal
    EntityCallBack(entStates.Hover) = EntityCallBackHover
    EntityCallBack(entStates.MouseDown) = EntityCallBackMouseDown
    EntityCallBack(entStates.MouseMove) = EntityCallBackMouseMove
    EntityCallBack(entStates.DblClick) = EntityCallBackDoubleClick

    ' Create the box
    CreateEntity winNum, zOrder_Con, Name, EntityLabel, Design(), image(), EntityCallBack(), Left, Top, Width, Height, Visible, , , , , Text, Align, Font, TextColour, Alpha, ClickThrough
End Sub

Public Sub CreateCheckbox(winNum As Long, Name As String, Left As Long, Top As Long, Width As Long, Optional Height As Long = 15, Optional Value As Long, Optional Text As String, _
                          Optional Font As Fonts = Fonts.FontRegular, Optional TextColour As Long = White, Optional Align As Byte = Alignment.AlignLeft, Optional Visible As Boolean = True, Optional Alpha As Long = 255, _
                          Optional theDesign As Long, Optional EntityCallBackNormal As Long, Optional EntityCallBackHover As Long, Optional EntityCallBackMouseDown As Long, Optional EntityCallBackMouseMove As Long, _
                          Optional EntityCallBackDoubleClick As Long, Optional Group As Long)
    Dim Design(0 To entStates.state_Count - 1) As Long
    Dim image(0 To entStates.state_Count - 1) As Long
    Dim EntityCallBack(0 To entStates.state_Count - 1) As Long

    ' Fill temp arrays
    EntityCallBack(entStates.Normal) = EntityCallBackNormal
    EntityCallBack(entStates.Hover) = EntityCallBackHover
    EntityCallBack(entStates.MouseDown) = EntityCallBackMouseDown
    EntityCallBack(entStates.MouseMove) = EntityCallBackMouseMove
    EntityCallBack(entStates.DblClick) = EntityCallBackDoubleClick

    ' Fill temp array
    Design(0) = theDesign

    ' Create the box
    CreateEntity winNum, zOrder_Con, Name, EntityCheckBox, Design(), image(), EntityCallBack(), Left, Top, Width, Height, Visible, , , , Value, Text, Align, Font, TextColour, Alpha, , , , , , , , , , , , Group
End Sub

Public Sub CreateComboBox(winNum As Long, Name As String, Left As Long, Top As Long, Width As Long, Height As Long, Design As Long, Optional Font As Fonts = Fonts.FontRegular)
    Dim theDesign(0 To entStates.state_Count - 1) As Long
    Dim image(0 To entStates.state_Count - 1) As Long
    Dim EntityCallBack(0 To entStates.state_Count - 1) As Long

    theDesign(0) = Design

    ' Create the box
    CreateEntity winNum, zOrder_Con, Name, EntityComboBox, theDesign(), image(), EntityCallBack(), Left, Top, Width, Height, , , , , , , , Font
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

' Make sure it's something which CAN be active
    Select Case Windows(curWindow).Controls(curControl).Type

    Case EntityTypes.EntityTextBox
        Windows(curWindow).activeControl = curControl
        SetActiveControl = True
    End Select

End Function

Public Sub CentraliseWindow(ByVal curWindow As Long, Optional ByVal FixedTop As Long = 0, Optional FixedLeft As Long = 0)
    With Windows(curWindow).Window

        If FixedLeft = 0 Then
            .Left = (ScreenWidth / 2) - (.Width / 2)
        Else
            .Left = FixedLeft
        End If

        If FixedTop = 0 Then
            .Top = (ScreenHeight / 2) - (.Height / 2)
        Else
            .Top = FixedTop
        End If

        .OrigLeft = .Left
        .OrigTop = .Top
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
        ActiveWindow = curWindow
    ElseIf Windows(curWindow).Window.zChange Then
        UpdateZOrder curWindow
        ActiveWindow = curWindow
    End If

    If resetPosition Then
        With Windows(curWindow).Window
            .Left = .OrigLeft
            .Top = .OrigTop
        End With
    End If
End Sub

Public Sub HideWindow(curWindow As Long)
    Dim i As Long

    If curWindow > 0 Then

        Windows(curWindow).Window.Visible = False

        ' Find next window to set as active
        For i = WindowCount To 1 Step -1
            If Windows(i).Window.Visible And Windows(i).Window.zChange Then

                'UpdateZOrder i
                ActiveWindow = i
                Exit Sub
            End If
        Next

    End If
End Sub

Public Function CensorWord(ByVal sString As String) As String
    CensorWord = String$(Len(sString), "*")
End Function

Public Sub CreateWindow_Combobox()

' Background window
    CreateWindow "winComboMenuBG", "ComboMenuBG", zOrder_Win, 0, 0, 800, 600, 0, , , , , , , , , , , , , , GetAddress(AddressOf CloseComboMenu), , , False, False

    ' Window
    CreateWindow "winComboMenu", "ComboMenu", zOrder_Win, 0, 0, 100, 100, 0, , Fonts.FontRegular, , , , DesignTypes.DesignComboMenu, , , , , , , , , , , False, False

    ' Centralise it
    CentraliseWindow WindowCount
End Sub

Public Sub CreateWindow_Bars()

' Create window
    CreateWindow "winBars", "", zOrder_Win, 10, 10, 239, 77, 0, , , , , , DesignTypes.DesignWindowWithoutTopBar, DesignTypes.DesignWindowWithoutTopBar, DesignTypes.DesignWindowWithoutTopBar, , , , , , , , , False, False

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Bars HP
    CreatePictureBox WindowCount, "picHP_Blank", 15, 15, 209, 13, , , , , Tex_GUI(18), Tex_GUI(18), Tex_GUI(18)

    ' Bar SP
    CreatePictureBox WindowCount, "picSP_Blank", 15, 32, 209, 13, , , , , Tex_GUI(20), Tex_GUI(20), Tex_GUI(20)

    ' Bar XP
    CreatePictureBox WindowCount, "picEXP_Blank", 15, 49, 209, 13, , , , , Tex_GUI(22), Tex_GUI(22), Tex_GUI(22)

    ' Draw the bars
    CreatePictureBox WindowCount, "picBlank", 0, 0, 0, 0, , , , , , , , , , , , , , , , GetAddress(AddressOf Bars_OnDraw)

    ' Label HP
    CreateLabel WindowCount, "lblHP", 15, 14, 209, 12, "0/0", FontRegular, White, Alignment.AlignCenter

    ' Label MP
    CreateLabel WindowCount, "lblMP", 15, 31, 209, 12, "0/0", FontRegular, White, Alignment.AlignCenter

    ' Label XP
    CreateLabel WindowCount, "lblEXP", 15, 48, 209, 12, "0/0", FontRegular, White, Alignment.AlignCenter
End Sub

Public Sub CreateWindow_QuickSlot()

' Create window
    CreateWindow "winHotbar", "", zOrder_Win, 372, 10, 418, 36, 0, , , , , , , , , , , , , GetAddress(AddressOf QuickSlot_MouseMove), GetAddress(AddressOf QuickSlot_MouseDown), GetAddress(AddressOf QuickSlot_MouseMove), GetAddress(AddressOf QuickSlot_DblClick), False, False, GetAddress(AddressOf DrawHotbar)
End Sub

Public Sub CreateWindow_Description()

' Create window
    CreateWindow "winDescription", "", zOrder_Win, 0, 0, 225, 320, 0, , , , , , DesignTypes.DesignWindowWithoutTopBar, DesignTypes.DesignWindowWithoutTopBar, DesignTypes.DesignWindowWithoutTopBar, , , , , , , , , False, , GetAddress(AddressOf Description_OnDraw)

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Name
    CreateLabel WindowCount, "lblName", 0, 10, 225, , "Name", FontRegular, BrightBlue, Alignment.AlignCenter

    ' Type
    CreateLabel WindowCount, "lblType", 0, 25, 225, , "Trade", FontRegular, White, Alignment.AlignCenter

    ' Price
    CreateLabel WindowCount, "lblPrice", 15, 25, 183, , "Price", FontRegular, White, Alignment.AlignLeft

    ' Description
    CreateLabel WindowCount, "lblDesc", 10, 100, 193, 350, "Description", FontRegular, White, Alignment.AlignCenter

End Sub

Public Sub CreateWindow_DragBox()

' Create window
    CreateWindow "winDragBox", "", zOrder_Win, 0, 0, 32, 32, 0, , , , , , , , , , , , GetAddress(AddressOf DragBox_Check), , , , , , , GetAddress(AddressOf DragBox_OnDraw)

    ' Need to set up unique mouseup event
    Windows(WindowCount).Window.EntityCallBack(entStates.MouseUp) = GetAddress(AddressOf DragBox_Check)
End Sub

Public Sub CreateWindow_RightClick()

' Create window
    CreateWindow "winRightClickBG", "", zOrder_Win, 0, 0, 800, 600, 0, , , , , , , , , , , , , , GetAddress(AddressOf RightClick_Close), , , False

    ' Centralise it
    CentraliseWindow WindowCount
End Sub

Public Sub CreateWindow_PlayerMenu()

' Create window
    CreateWindow "winPlayerMenu", "", zOrder_Win, 0, 0, 150, 150, 0, , , , , , DesignTypes.DesignPlayerInteraction, DesignTypes.DesignPlayerInteraction, DesignTypes.DesignPlayerInteraction, , , , , , GetAddress(AddressOf RightClick_Close), , , False

    ' Centralise it
    CentraliseWindow WindowCount

    ' Name
    CreateButton WindowCount, "btnName", 8, 8, 134, 18, "Name", FontRegular, White, , , , , , , DesignTypes.DesignPlayerInteractionHeader, DesignTypes.DesignPlayerInteractionHeader, DesignTypes.DesignPlayerInteractionHeader, , , GetAddress(AddressOf RightClick_Close)

    ' Option Create Group
    CreateButton WindowCount, "btnParty", 8, 26, 135, 18, "Convite para grupo", FontRegular, White, , , , , , , , DesignTypes.DesignPlayerInteractionHover, , , , GetAddress(AddressOf PlayerMenu_Party)

    ' Option Exit Group
    CreateButton WindowCount, "btnParty", 8, 44, 135, 18, "Sair do grupo", FontRegular, White, , , , , , , , DesignTypes.DesignPlayerInteractionHover, , , , GetAddress(AddressOf PlayerMenu_LeaveParty)

    ' Option Trade
    CreateButton WindowCount, "btnTrade", 8, 62, 135, 18, "Pedido de negociação", FontRegular, White, , , , , , , , DesignTypes.DesignPlayerInteractionHover, , , , GetAddress(AddressOf PlayerMenu_Trade)

    ' Option Guild
    CreateButton WindowCount, "btnGuild", 8, 80, 135, 18, "Convite para guild", FontRegular, White, , , , , , , , DesignTypes.DesignPlayerInteractionHover, , , , GetAddress(AddressOf PlayerMenu_Guild)

    ' Option Private Message
    CreateButton WindowCount, "btnPM", 8, 98, 135, 18, "Mensagem privada", FontRegular, White, , , , , , , , DesignTypes.DesignPlayerInteractionHover, , , , GetAddress(AddressOf PlayerMenu_PM)

    ' Option View Equipament
    CreateButton WindowCount, "btnViewEquipment", 8, 116, 135, 18, "Ver Equipamento", FontRegular, White, , , , , , , , DesignTypes.DesignPlayerInteractionHover, , , , GetAddress(AddressOf PlayerMenu_ViewEquipment)

End Sub

Public Sub CreateWindow_Invitations()

' Create Window Party
    CreateWindow "winInvite_Party", "", zOrder_Win, ScreenWidth - 234, ScreenHeight - 80, 223, 37, 0, , , , , , DesignTypes.DesignPlayerInteraction, DesignTypes.DesignPlayerInteraction, DesignTypes.DesignPlayerInteraction, , , , , , , , , False

    ' Button Invite Party
    CreateButton WindowCount, "btnInvite", 11, 12, 201, 14, ColourChar & White & "Richard " & ColourChar & "-1" & "has invited you to a party.", FontRegular, Grey, , , , , , , , , , , , GetAddress(AddressOf btnInvite_Party), , , , , Green

    ' Create Window Trade
    CreateWindow "winInvite_Trade", "", zOrder_Win, ScreenWidth - 234, ScreenHeight - 80, 223, 37, 0, , , , , , DesignTypes.DesignPlayerInteraction, DesignTypes.DesignPlayerInteraction, DesignTypes.DesignPlayerInteraction, , , , , , , , , False

    ' Button Invite Trade
    CreateButton WindowCount, "btnInvite", 11, 12, 201, 14, ColourChar & White & "Richard " & ColourChar & "-1" & "convidou voce para um grupo.", FontRegular, Grey, , , , , , , , , , , , 0, , , , , Green
End Sub

Public Sub CreateWindow_Target()

' Create window
    CreateWindow "winTarget", "", zOrder_Win, 255, 10, 239, 78, 0, True, Fonts.FontRegular, , 2, 7, DesignTypes.DesignWindowWithoutTopBar, DesignTypes.DesignWindowWithoutTopBar, DesignTypes.DesignWindowWithoutTopBar, , , , , , , , , , , GetAddress(AddressOf Target_OnDraw)

    zOrder_Con = 1

    ' Bar HP
    CreatePictureBox WindowCount, "picHP_Blank", 15, 30, 209, 13, , , , , Tex_GUI(18), Tex_GUI(18), Tex_GUI(18)

    ' Bar SP
    CreatePictureBox WindowCount, "picSP_Blank", 15, 47, 209, 13, , , , , Tex_GUI(20), Tex_GUI(20), Tex_GUI(20)

    ' Label HP
    CreateLabel WindowCount, "lblHP", 15, 29, 209, 12, "0/0", FontRegular, White, Alignment.AlignCenter

    ' Label MP
    CreateLabel WindowCount, "lblMP", 15, 46, 209, 12, "0/0", FontRegular, White, Alignment.AlignCenter

    ' Label Name
    CreateLabel WindowCount, "lblName", 0, 10, 239, 25, "Name", FontRegular, White, Alignment.AlignCenter

End Sub

' Rendering & Initialisation
Public Sub InitGUI()

' Starter values
    zOrder_Win = 1
    zOrder_Con = 1

    ' Menu
    CreateWindow_Login
    CreateWindow_LoginFooter
    CreateWindow_Models
    CreateWindow_ModelFooter
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
