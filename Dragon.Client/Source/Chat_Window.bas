Attribute VB_Name = "Chat_Window"
Option Explicit

' Chatbox
Public Type ChatStruct
    Text As String
    Color As ColorType
    Visible As Boolean
    Timer As Long
    Channel As Byte
End Type

Public Const ChatDifferenceTimer As Long = 500
Public Const ChatLines As Long = 200
Public Const ChatWidth As Long = 316

Public Chat(1 To ChatLines) As ChatStruct
Public ChatLastRemove As Long
Public Chat_HighIndex As Long
Public ChatScroll As Long

Private WindowIndex As Long
Private ChatSmallWindowIndex As Long

Public Sub CreateWindow_ChatSmall()
    ' Create window
    CreateWindow "winChatSmall", "", zOrder_Win, 8, 438, 0, 0, 0, False, , , , , , , , , , , , , , , , False, , GetAddress(AddressOf OnDraw_ChatSmall), , True
    ' Set the index for spawning controls
    zOrder_Con = 1
    ' Chat Label
    CreateLabel WindowCount, "lblMsg", 12, 127, 286, 25, "Pressione 'Enter' para abrir o chat.", OpenSans_Regular, Grey
    
    ChatSmallWindowIndex = WindowCount
End Sub

Private Sub OnDraw_ChatSmall()
    Dim xO As Long, yO As Long
 
    If actChatWidth < 160 Then actChatWidth = 160
    If actChatHeight < 10 Then actChatHeight = 10

    xO = Windows(ChatSmallWindowIndex).Window.Left + 10
    yO = ScreenHeight - 16 - actChatHeight - 8

    ' draw the background
    RenderDesign DesignTypes.desWin_Shadow, xO, yO, actChatWidth, actChatHeight
    
    ' call the chat render
    RenderChat
End Sub

Public Sub CreateWindow_Chat()
' Create window
    CreateWindow "winChat", "", zOrder_Win, 8, 422, 352, 152, 0, False, , , , , , , , , , , , , , , , False

    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Channel boxes
    CreateCheckbox WindowCount, "chkGame", 10, 2, 49, 23, 1, "Jogo", OpenSans_Regular, , , , , DesignTypes.desChkChat, , , GetAddress(AddressOf CheckBoxChat_Game)
    CreateCheckbox WindowCount, "chkMap", 60, 2, 49, 23, 1, "Mapa", OpenSans_Regular, , , , , DesignTypes.desChkChat, , , GetAddress(AddressOf CheckBoxChat_Map)
    CreateCheckbox WindowCount, "chkGlobal", 110, 2, 49, 23, 1, "Global", OpenSans_Regular, , , , , DesignTypes.desChkChat, , , GetAddress(AddressOf CheckBoxChat_Global)
    CreateCheckbox WindowCount, "chkParty", 160, 2, 49, 23, 1, "Grupo", OpenSans_Regular, , , , , DesignTypes.desChkChat, , , GetAddress(AddressOf CheckBoxChat_Party)
    CreateCheckbox WindowCount, "chkGuild", 210, 2, 49, 23, 1, "Guild", OpenSans_Regular, , , , , DesignTypes.desChkChat, , , GetAddress(AddressOf CheckBoxChat_Guild)
    CreateCheckbox WindowCount, "chkPrivate", 260, 2, 49, 23, 1, "Privado", OpenSans_Regular, , , , , DesignTypes.desChkChat, , , GetAddress(AddressOf CheckBoxChat_Private)
    ' Blank picturebox - ondraw wrapper
    CreatePictureBox WindowCount, "picNull", 0, 0, 0, 0, , , , , , , , , , , , , , , , GetAddress(AddressOf OnDraw_Chat)
    ' Chat button
    CreateButton WindowCount, "btnChat", 296, 124 + 16, 48, 20, "Dizer", OpenSans_Regular, , , , , , , , DesignTypes.desSteel, DesignTypes.desSteel_Hover, DesignTypes.desSteel_Click, , , GetAddress(AddressOf ButtonSay_Click)
    ' Chat Textbox
    CreateTextbox WindowCount, "txtChat", 12, 127 + 16, 286, 25, , Fonts.OpenSans_Regular
    ' buttons
    CreateButton WindowCount, "btnUp", 328, 28, 11, 13, , , , , , , Tex_GUI(4), Tex_GUI(52), Tex_GUI(4), , , , , , GetAddress(AddressOf ButtonChat_Up)
    CreateButton WindowCount, "btnDown", 327, 122, 11, 13, , , , , , , Tex_GUI(5), Tex_GUI(53), Tex_GUI(5), , , , , , GetAddress(AddressOf ButtonChat_Down)

    ' Custom Handlers for mouse up
    Windows(WindowCount).Controls(GetControlIndex("winChat", "btnUp")).entCallBack(entStates.MouseUp) = GetAddress(AddressOf ButtonChat_Up_MouseUp)
    Windows(WindowCount).Controls(GetControlIndex("winChat", "btnDown")).entCallBack(entStates.MouseUp) = GetAddress(AddressOf ButtonChat_Down_MouseUp)

    ' Set the active control
    SetActiveControl WindowCount, GetControlIndex("winChat", "txtChat")
    
    Windows(WindowCount).Controls(GetControlIndex("winChat", "txtChat")).TextLimit = 50

    ' sort out the tabs
    With Windows(WindowCount)
        .Controls(GetControlIndex("winChat", "chkGame")).Value = Options.ChannelState(ChatChannel.ChannelGame)
        .Controls(GetControlIndex("winChat", "chkMap")).Value = Options.ChannelState(ChatChannel.ChannelMap)
        .Controls(GetControlIndex("winChat", "chkGlobal")).Value = Options.ChannelState(ChatChannel.ChannelGlobal)
        .Controls(GetControlIndex("winChat", "chkParty")).Value = Options.ChannelState(ChatChannel.ChannelParty)
        .Controls(GetControlIndex("winChat", "chkGuild")).Value = Options.ChannelState(ChatChannel.ChannelGuild)
        .Controls(GetControlIndex("winChat", "chkPrivate")).Value = Options.ChannelState(ChatChannel.ChannelPrivate)
    End With
    
    WindowIndex = WindowCount
End Sub

Private Sub RenderChat()
    Dim xO As Long, yO As Long, Colour As ColorType, yOffset As Long, rLines As Long, LineCount As Long
    Dim tmpText As String, i As Long, IsVisible As Boolean, topWidth As Long, tmpArray() As String, X As Long

    ' set the position
    xO = 19
    yO = ScreenHeight - 41    '545 + 14

    ' loop through chat
    rLines = 1
    
    i = 1 + ChatScroll
    
    Do While rLines <= 8
    
        If i > ChatLines Then Exit Do
        LineCount = 0
        
        ' exit out early if we come to a blank string
        If Len(Chat(i).Text) = 0 Then Exit Do
        
        ' get visible state
        IsVisible = True
        
        If inSmallChat Then
            If Not Chat(i).Visible Then IsVisible = False
        End If
        
        If Options.ChannelState(Chat(i).Channel) = 0 Then IsVisible = False
        ' make sure it's visible
        If IsVisible Then
            ' render line
            Colour = Chat(i).Color
            ' check if we need to word wrap
            If TextWidth(Font(Fonts.OpenSans_Regular), Chat(i).Text) > ChatWidth Then
                ' word wrap
                tmpText = WordWrap(Font(Fonts.OpenSans_Regular), Chat(i).Text, ChatWidth, LineCount)
                ' can't have it going offscreen.
                If rLines + LineCount > 9 Then Exit Do
                ' continue on
                yOffset = yOffset - (14 * LineCount)
                RenderText Font(Fonts.OpenSans_Regular), tmpText, xO, yO + yOffset, Colour
                rLines = rLines + LineCount
                ' set the top width
                tmpArray = Split(tmpText, vbNewLine)
                For X = 0 To UBound(tmpArray)
                    If TextWidth(Font(Fonts.OpenSans_Regular), tmpArray(X)) > topWidth Then topWidth = TextWidth(Font(Fonts.OpenSans_Regular), tmpArray(X))
                Next
            Else
                ' normal
                yOffset = yOffset - 14
                RenderText Font(Fonts.OpenSans_Regular), Chat(i).Text, xO, yO + yOffset, Colour
                rLines = rLines + 1
                ' set the top width
                If TextWidth(Font(Fonts.OpenSans_Regular), Chat(i).Text) > topWidth Then topWidth = TextWidth(Font(Fonts.OpenSans_Regular), Chat(i).Text)
            End If
        End If
        ' increment chat pointer
        i = i + 1
    Loop

    ' get the height of the small chat box
    SetChatHeight rLines * 14
    SetChatWidth topWidth
End Sub

Public Sub AddText(ByVal Text As String, ByVal Color As ColorType, Optional ByVal Alpha As Long = 255, Optional Channel As Long = 0)
    Dim i As Long

    Chat_HighIndex = 0
    ' Move the rest of it up
    For i = (ChatLines - 1) To 1 Step -1
        If Len(Chat(i).Text) > 0 Then
            If i > Chat_HighIndex Then Chat_HighIndex = i + 1
        End If
        Chat(i + 1) = Chat(i)
    Next

    Chat(1).Text = Text
    Chat(1).Color = Color
    Chat(1).Visible = True
    Chat(1).Timer = GetTickCount
    Chat(1).Channel = Channel
End Sub

Private Sub OnDraw_Chat()
    Dim xO As Long, yO As Long
 
    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top + 16

    ' draw the box
    RenderDesign DesignTypes.desWin_Desc, xO, yO, 352, 152
    ' draw the input box
    RenderTexture Tex_GUI(46), xO + 7, yO + 123, 0, 0, 171, 22, 171, 22
    RenderTexture Tex_GUI(46), xO + 174, yO + 123, 0, 22, 171, 22, 171, 22
    ' call the chat render
    RenderChat
End Sub

Private Sub CheckBoxChat_Game()
    Options.ChannelState(ChatChannel.ChannelGame) = Windows(WindowIndex).Controls(GetControlIndex("winChat", "chkGame")).Value
    UpdateChat
End Sub

Private Sub CheckBoxChat_Map()
    Options.ChannelState(ChatChannel.ChannelMap) = Windows(WindowIndex).Controls(GetControlIndex("winChat", "chkMap")).Value
    UpdateChat
End Sub

Private Sub CheckBoxChat_Global()
    Options.ChannelState(ChatChannel.ChannelGlobal) = Windows(WindowIndex).Controls(GetControlIndex("winChat", "chkGlobal")).Value
    UpdateChat
End Sub

Private Sub CheckBoxChat_Party()
    Options.ChannelState(ChatChannel.ChannelParty) = Windows(WindowIndex).Controls(GetControlIndex("winChat", "chkParty")).Value
    UpdateChat
End Sub

Private Sub CheckBoxChat_Guild()
    Options.ChannelState(ChatChannel.ChannelGuild) = Windows(WindowIndex).Controls(GetControlIndex("winChat", "chkGuild")).Value
    UpdateChat
End Sub

Private Sub CheckBoxChat_Private()
    Options.ChannelState(ChatChannel.ChannelPrivate) = Windows(WindowIndex).Controls(GetControlIndex("winChat", "chkPrivate")).Value
    UpdateChat
End Sub

Private Sub ButtonSay_Click()
    HandleKeyPresses vbKeyReturn
End Sub

Private Sub ButtonChat_Up()
    ChatButtonUp = True
End Sub

Private Sub ButtonChat_Down()
    ChatButtonDown = True
End Sub

Private Sub ButtonChat_Up_MouseUp()
    ChatButtonUp = False
End Sub

Private Sub ButtonChat_Down_MouseUp()
    ChatButtonDown = False
End Sub

Public Sub AddPlayerMessage(ByVal Text As String, ByRef MsgTo As String)
    If MsgTo = GetPlayerName(MyIndex) Then
        Exit Sub
    End If
    
    Dim Color As Long
    
    Color = BrightGreen

    If GetPlayerAccess(MyIndex) > 0 Then Color = Pink

    AddText ColourChar & GetColStr(Color) & "[Privado] Para " & MsgTo & ": " & ColourChar & GetColStr(Grey) & Text, Grey, , ChatChannel.ChannelPrivate
End Sub

