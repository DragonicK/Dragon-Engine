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
    CreateWindow "winChatSmall", "", zOrder_Win, 12, 438, 0, 0, 0, False, , , , , , , , , , , , , , , , False, , GetAddress(AddressOf OnDraw_ChatSmall), , True
    
    ' Set the index for spawning controls
    zOrder_Con = 1
    
    ' Chat Label
    CreateLabel WindowCount, "lblMsg", 12, 127, 286, 25, "Pressione 'Enter' para abrir o chat.", FontRegular, Grey
    
    ChatSmallWindowIndex = WindowCount
End Sub

Private Sub OnDraw_ChatSmall()
    Dim xO As Long, yO As Long
 
    If actChatWidth < 160 Then actChatWidth = 240
    If actChatHeight < 10 Then actChatHeight = 10

    xO = Windows(ChatSmallWindowIndex).Window.Left + 10
    yO = ScreenHeight - 16 - actChatHeight - 8

    ' Draw the background
    RenderDesign DesignTypes.DesignChatSmallShadow, xO, yO, actChatWidth, actChatHeight
    
    ' call the chat render
    RenderChat
End Sub

Public Sub CreateWindow_Chat()
    Dim xO As Long
    Dim winWidth As Long
    Dim winHeight As Long
    Dim Padding As Long
    
' Create window
    CreateWindow "winChat", "", zOrder_Win, 10, ScreenHeight - 194, 368, 182, 0, False, , , , , , , , , , , , , , , , False

    ' Set the index for spawning controls
    zOrder_Con = 1
    
    xO = Windows(WindowCount).Window.Left
    winWidth = Windows(WindowCount).Window.Width
    winHeight = Windows(WindowCount).Window.Height
    Padding = 28
    
    ' Bakground Chat
    CreatePictureBox WindowCount, "", 0, Padding, winWidth, winHeight - Padding - 30, , , , , , , , DesignTypes.DesignWindowWithoutTopBar, DesignTypes.DesignWindowWithoutTopBar, DesignTypes.DesignWindowWithoutTopBar

    ' Channel Buttons
    CreateCheckbox WindowCount, "chkGame", (72 + 2) * 0, 0, 72, 26, 1, "Jogo", FontRegular, , , , , DesignTypes.DesignCheckBoxChat, , , GetAddress(AddressOf CheckBoxChat_Game)
    CreateCheckbox WindowCount, "chkMap", (72 + 2) * 1, 0, 72, 26, 1, "Mapa", FontRegular, , , , , DesignTypes.DesignCheckBoxChat, , , GetAddress(AddressOf CheckBoxChat_Map)
    CreateCheckbox WindowCount, "chkGlobal", (72 + 2) * 2, 0, 72, 26, 1, "Global", FontRegular, , , , , DesignTypes.DesignCheckBoxChat, , , GetAddress(AddressOf CheckBoxChat_Global)
    CreateCheckbox WindowCount, "chkParty", (72 + 2) * 3, 0, 72, 26, 1, "Grupo", FontRegular, , , , , DesignTypes.DesignCheckBoxChat, , , GetAddress(AddressOf CheckBoxChat_Party)
    'CreateCheckbox WindowCount, "chkGuild", 74, 0, 73, 26, 1, "Guild", FontRegular, , , , , DesignTypes.DesignCheckBoxChat, , , GetAddress(AddressOf CheckBoxChat_Guild)
    CreateCheckbox WindowCount, "chkPrivate", (72 + 2) * 4, 0, 72, 26, 1, "Privado", FontRegular, , , , , DesignTypes.DesignCheckBoxChat, , , GetAddress(AddressOf CheckBoxChat_Private)
    
    ' Text
    CreatePictureBox WindowCount, "picNull", 0, 0, 0, 0, , , , , , , , , , , , , , , , GetAddress(AddressOf OnDraw_Chat)
    
    ' Chat Textbox
    CreateTextbox WindowCount, "txtChat", 0, winHeight - 30, winWidth, 30, , Fonts.FontRegular, , Alignment.AlignLeft, , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , , , , , 8, 8
    
    ' Chat Button
    CreateButton WindowCount, "btnChat", 272, winHeight - 28, 94, 26, "Dizer", FontRegular, , , , , , , , DesignTypes.DesignGreen, DesignTypes.DesignGreenHover, DesignTypes.DesignGreenClick, , , GetAddress(AddressOf ButtonSay_Click)
    
    ' Scrool Buttons
    CreateButton WindowCount, "btnUp", winWidth - 24, 38, 12, 13, , , , , , , Tex_GUI(3), Tex_GUI(4), Tex_GUI(5), , , , , , GetAddress(AddressOf ButtonChat_Up)
    CreateButton WindowCount, "btnDown", winWidth - 24, 128, 12, 13, , , , , , , Tex_GUI(6), Tex_GUI(7), Tex_GUI(8), , , , , , GetAddress(AddressOf ButtonChat_Down)

    ' Custom Handlers for mouse up
    Windows(WindowCount).Controls(GetControlIndex("winChat", "btnUp")).EntityCallBack(entStates.MouseUp) = GetAddress(AddressOf ButtonChat_Up_MouseUp)
    Windows(WindowCount).Controls(GetControlIndex("winChat", "btnDown")).EntityCallBack(entStates.MouseUp) = GetAddress(AddressOf ButtonChat_Down_MouseUp)

    ' Set the active control
    SetActiveControl WindowCount, GetControlIndex("winChat", "txtChat")
    Windows(WindowCount).Controls(GetControlIndex("winChat", "txtChat")).TextLimit = 35

    ' sort out the tabs
    With Windows(WindowCount)
        .Controls(GetControlIndex("winChat", "chkGame")).Value = Options.ChannelState(ChatChannel.ChannelGame)
        .Controls(GetControlIndex("winChat", "chkMap")).Value = Options.ChannelState(ChatChannel.ChannelMap)
        .Controls(GetControlIndex("winChat", "chkGlobal")).Value = Options.ChannelState(ChatChannel.ChannelGlobal)
        .Controls(GetControlIndex("winChat", "chkParty")).Value = Options.ChannelState(ChatChannel.ChannelParty)
        '.Controls(GetControlIndex("winChat", "chkGuild")).Value = Options.ChannelState(ChatChannel.ChannelGuild)
        .Controls(GetControlIndex("winChat", "chkPrivate")).Value = Options.ChannelState(ChatChannel.ChannelPrivate)
    End With
    
    WindowIndex = WindowCount
End Sub

Private Sub RenderChat()
    Dim xO As Long, yO As Long, Colour As ColorType, yOffset As Long, rLines As Long, LineCount As Long
    Dim tmpText As String, i As Long, IsVisible As Boolean, topWidth As Long, tmpArray() As String, X As Long

    ' set the position
    xO = 19
    yO = ScreenHeight - 46

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
            If TextWidth(Font(Fonts.FontRegular), Chat(i).Text) > ChatWidth Then
                ' word wrap
                tmpText = WordWrap(Font(Fonts.FontRegular), Chat(i).Text, ChatWidth, LineCount)
                ' can't have it going offscreen.
                If rLines + LineCount > 9 Then Exit Do
                ' continue on
                yOffset = yOffset - (14 * LineCount)
                RenderText Font(Fonts.FontRegular), tmpText, xO, yO + yOffset, Colour
                rLines = rLines + LineCount
                ' set the top width
                tmpArray = Split(tmpText, vbNewLine)
                For X = 0 To UBound(tmpArray)
                    If TextWidth(Font(Fonts.FontRegular), tmpArray(X)) > topWidth Then topWidth = TextWidth(Font(Fonts.FontRegular), tmpArray(X))
                Next
            Else
                ' normal
                yOffset = yOffset - 14
                RenderText Font(Fonts.FontRegular), Chat(i).Text, xO, yO + yOffset, Colour
                rLines = rLines + 1
                ' set the top width
                If TextWidth(Font(Fonts.FontRegular), Chat(i).Text) > topWidth Then topWidth = TextWidth(Font(Fonts.FontRegular), Chat(i).Text)
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

Public Sub ShowChat()
    ShowWindow GetWindowIndex("winChat"), , False
    HideWindow GetWindowIndex("winChatSmall")
    ' Set the active control
    ActiveWindow = GetWindowIndex("winChat")
    SetActiveControl GetWindowIndex("winChat"), GetControlIndex("winChat", "txtChat")
    inSmallChat = False
    ChatScroll = 0
End Sub

Public Sub HideChat()
    ShowWindow GetWindowIndex("winChatSmall"), , False
    HideWindow GetWindowIndex("winChat")
    inSmallChat = True
    ChatScroll = 0
End Sub

Public Sub SetChatHeight(Height As Long)
    actChatHeight = Height
End Sub

Public Sub SetChatWidth(Width As Long)
    actChatWidth = Width
End Sub

Public Sub UpdateChat()
    SaveOptions
End Sub

Public Sub ScrollChatBox(ByVal Direction As Byte)
    If Direction = 0 Then    ' up
        If ChatScroll < ChatLines Then
            ChatScroll = ChatScroll + 1
        End If
    Else
        If ChatScroll > 0 Then
            ChatScroll = ChatScroll - 1
        End If
    End If
End Sub

Public Sub Resize_WinChat()
    Dim WindowChatIndex As Long
    Dim WindowSmallChatIndex As Long
    
    ' Get The Window
    WindowChatIndex = GetWindowIndex("winChat")
    WindowSmallChatIndex = GetWindowIndex("winChatSmall")
    
    ' Position Chat
    Windows(WindowChatIndex).Window.Top = ScreenHeight - 194
    
    ' Postition Chat Small
    Windows(WindowSmallChatIndex).Window.Top = ScreenHeight - 162
    
End Sub

