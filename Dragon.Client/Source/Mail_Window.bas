Attribute VB_Name = "Mail_Window"
Option Explicit

Public InMail As Boolean

Public WaitingMailResponse As Boolean
Public MailingWindowState As WindowMailState

Public Enum WindowMailState
    WindowMailState_Listing
    WindowMailState_Reading
    WindowMailState_Writing
End Enum

Private ReadingMailIndex As Long

Private WindowIndex As Long
Private ButtonReadIndex As Long
Private ButtonWriteIndex As Long

' Página atual da lista.
Private MailPage As Integer
' Quantidade máxima de páginas.
Private MailPageCount As Integer

' Índice do inventário selecionado para envio.
Public SendMailItemInventoryIndex As Long
' Quantidade selecionada do inventário para envio.
Public SendMailItemValue As Long
' Data de envio.
Private SendDate As String
' Quantidade de dinheiro selecionado.
Private SendMailCurrency As Long

' Quantidade máxima de items exibidos.
Private Const MaxMailList As Long = 9

Private Const PictureBackLeft As Long = 55
Private Const PictureBackTop As Long = 98
Private Const PictureBackOffSetY As Long = 30

Private Const CheckBoxLeft As Long = 20
Private Const CheckBoxTop As Long = 102
Private Const CheckBoxOffSetY As Long = 30

Private Const ReadingMailLeft As Long = 45

Private Const PictureSenderTop As Long = 80
Private Const PictureTitleTop As Long = 110
Private Const PictureTextTop As Long = 140
Private Const PictureSendDateTop As Long = 270
Private Const PictureExpireDateTop As Long = 300

Private Const PictureItemLeft As Long = 180
Private Const PictureItemTop As Long = 360

Private Const Expiration As String = "Data de Expiração: "
Private Const Received As String = "Data de Recebimento: "
Private Const Sended As String = "Data de Envio: "

Public Sub CreateWindow_Mail()
    Dim i As Long
    ' Create window
    CreateWindow "winMail", "CORREIO", zOrder_Win, 0, 0, 400, 460, 0, False, Fonts.FontRegular, , 2, 7, DesignTypes.DesignWindowWithTopBarAndNavBar, DesignTypes.DesignWindowWithTopBarAndNavBar, DesignTypes.DesignWindowWithTopBarAndNavBar, , , , , , GetAddress(AddressOf WindowMail_MouseDown), , , , , GetAddress(AddressOf Draw_Mail)
    ' Centralise it
    CentraliseWindow WindowCount
    ' Set the index for spawning controls
    zOrder_Con = 1

    ' Close button
    CreateButton WindowCount, "btnClose", Windows(WindowCount).Window.Width - 33, 11, 22, 22, , , , , , , Tex_GUI(TextureControl_CloseNormal), Tex_GUI(TextureControl_CloseHover), Tex_GUI(TextureControl_CloseClick), , , , , , GetAddress(AddressOf ButtonMenu_Mail)

    CreateButton WindowCount, "btnRead", 0, 42, 200, 26, "LER", FontRegular, Green, , , , , , , , , , , , GetAddress(AddressOf Button_ShowRead)
    CreateButton WindowCount, "btnWrite", 200, 42, 200, 26, "ESCREVER", FontRegular, , , , , , , , , , , , , GetAddress(AddressOf Button_ShowWrite)

    ' Read Mail
    CreatePictureBox WindowCount, "picSender", ReadingMailLeft, PictureSenderTop, 310, 25, False, , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox
    CreatePictureBox WindowCount, "picSubject", ReadingMailLeft, PictureTitleTop, 310, 25, False, , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox
    CreatePictureBox WindowCount, "picContent", ReadingMailLeft, PictureTextTop, 310, 122, False, , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox
    CreatePictureBox WindowCount, "picSendDate", ReadingMailLeft, PictureSendDateTop, 310, 25, False, , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox
    CreatePictureBox WindowCount, "picExpireDate", ReadingMailLeft, PictureExpireDateTop, 310, 25, False, , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox
    CreatePictureBox WindowCount, "picAttachCoin", ReadingMailLeft, 330, 310, 25, False, , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox
    CreatePictureBox WindowCount, "picAttachItem", PictureItemLeft, PictureItemTop, 40, 40, False, , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , , , , GetAddress(AddressOf Draw_ReceivedItem)

    CreateLabel WindowCount, "lblCurrency", ReadingMailLeft, 334, 310, 20, "Ouro: 0", FontRegular, White, Alignment.AlignCenter, False, , , , , GetAddress(AddressOf ReadMailCurrency_MouseDown)
    CreateLabel WindowCount, "lblText", ReadingMailLeft, PictureTextTop + 5, 310, 115, "", FontRegular, White, Alignment.AlignCenter, False
    CreateLabel WindowCount, "lblReceiveItem", PictureItemLeft, PictureItemTop, 40, 40, vbNullString, FontRegular, White, Alignment.AlignCenter, False, , , , GetAddress(AddressOf ReadMailItem_MouseMove), GetAddress(AddressOf ReadMailItem_MouseDown), GetAddress(AddressOf ReadMailItem_MouseMove)

    ' Write Mail
    CreateTextbox WindowCount, "txtReceiver", ReadingMailLeft, PictureSenderTop, 310, 25, "Destinatário", Fonts.FontRegular, , Alignment.AlignLeft, , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , , , , , 8, 5, , , False
    CreateTextbox WindowCount, "txtSubject", ReadingMailLeft, PictureTitleTop, 310, 25, "Título", Fonts.FontRegular, , Alignment.AlignLeft, , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , , , , , 8, 5, , , False
    CreateTextbox WindowCount, "txtContent", ReadingMailLeft, PictureTextTop, 310, 122, "Mensagem", Fonts.FontRegular, , Alignment.AlignCenter, , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , , , , , 8, 10, , , True

    CreatePictureBox WindowCount, "picSendAttachCoin", ReadingMailLeft, 330, 310, 25, False, , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox
    CreatePictureBox WindowCount, "picSendAttachItem", PictureItemLeft, PictureItemTop, 40, 40, True, , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , , , , GetAddress(AddressOf Draw_SendItem)

    CreateLabel WindowCount, "lblSendAttachCoin", ReadingMailLeft, 334, 310, 20, "Ouro: 0", FontRegular, White, Alignment.AlignCenter, False, , , , , GetAddress(AddressOf Button_AddSendMailCurrency)
    CreateLabel WindowCount, "lblWriteItem", PictureItemLeft, PictureItemTop, 40, 40, vbNullString, FontRegular, White, Alignment.AlignCenter, False, , , , GetAddress(AddressOf WriteMailItem_MouseMove), GetAddress(AddressOf WriteMailItem_MouseDown), GetAddress(AddressOf WriteMailItem_MouseMove)

    For i = 1 To MaxMailList
        CreateCheckbox WindowCount, "chkSelected" & i, CheckBoxLeft, CheckBoxTop + (CheckBoxOffSetY * (i - 1)), 25, , False, vbNullString, FontRegular, , , , , DesignTypes.DesignCheckBox
    Next

    ' List Mail
    ' Registra manualmente os eventos.
    CreatePictureBox WindowCount, "picBack1", PictureBackLeft, PictureBackTop + (PictureBackOffSetY * 0), 310, 25, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , GetAddress(AddressOf Button_ReadMail1)
    CreatePictureBox WindowCount, "picBack2", PictureBackLeft, PictureBackTop + (PictureBackOffSetY * 1), 310, 25, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , GetAddress(AddressOf Button_ReadMail2)
    CreatePictureBox WindowCount, "picBack3", PictureBackLeft, PictureBackTop + (PictureBackOffSetY * 2), 310, 25, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , GetAddress(AddressOf Button_ReadMail3)
    CreatePictureBox WindowCount, "picBack4", PictureBackLeft, PictureBackTop + (PictureBackOffSetY * 3), 310, 25, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , GetAddress(AddressOf Button_ReadMail4)
    CreatePictureBox WindowCount, "picBack5", PictureBackLeft, PictureBackTop + (PictureBackOffSetY * 4), 310, 25, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , GetAddress(AddressOf Button_ReadMail5)
    CreatePictureBox WindowCount, "picBack6", PictureBackLeft, PictureBackTop + (PictureBackOffSetY * 5), 310, 25, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , GetAddress(AddressOf Button_ReadMail6)
    CreatePictureBox WindowCount, "picBack7", PictureBackLeft, PictureBackTop + (PictureBackOffSetY * 6), 310, 25, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , GetAddress(AddressOf Button_ReadMail7)
    CreatePictureBox WindowCount, "picBack8", PictureBackLeft, PictureBackTop + (PictureBackOffSetY * 7), 310, 25, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , GetAddress(AddressOf Button_ReadMail8)
    CreatePictureBox WindowCount, "picBack9", PictureBackLeft, PictureBackTop + (PictureBackOffSetY * 8), 310, 25, , , , , , , , DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, DesignTypes.DesignTextBox, , , GetAddress(AddressOf Button_ReadMail9)

    ' Botões setas
    CreateLabel WindowCount, "lblPage", 150, 385, 120, 25, "Página: 1/1", FontRegular, White, Alignment.AlignCenter
    CreateButton WindowCount, "btnUp", 264, 385, 16, 16, , , , , , , Tex_GUI(44), Tex_GUI(45), Tex_GUI(46), , , , , , GetAddress(AddressOf Button_PageUp)
    CreateButton WindowCount, "btnDown", 139, 385, 16, 16, , , , , , , Tex_GUI(47), Tex_GUI(48), Tex_GUI(49), , , , , , GetAddress(AddressOf Button_PageDown)

    CreateButton WindowCount, "btnSelectAll", 47, 410, 150, 24, "SELECIONAR TODOS", FontRegular, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf SelectAll)
    CreateButton WindowCount, "btnDelete", 204, 410, 150, 24, "DELETAR", FontRegular, , , , , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf DeleteMail)
    CreateButton WindowCount, "btnSend", 125, 410, 150, 24, "ENVIAR", FontRegular, , , False, , , , , DesignTypes.DesignGrey, DesignTypes.DesignGreyHover, DesignTypes.DesignGreyClick, , , GetAddress(AddressOf SendNewMail)

    MailPage = 1
    WindowIndex = WindowCount
    ButtonReadIndex = GetControlIndex("winMail", "btnRead")
    ButtonWriteIndex = GetControlIndex("winMail", "btnWrite")

    Call UpdateMailList
End Sub

Private Sub WindowMail_MouseDown()
' Retira o cursor do textbox
    Windows(WindowIndex).activeControl = 0
End Sub

Public Sub UpdateMailList()
    ReadingMailIndex = 0
    Call SetMailPageCount
    Call CanEnableMailCheckBox
    Call ChangeWindowMailState(WindowMailState_Listing)
End Sub

Private Sub ReadMailCurrency_MouseDown()
    If WaitingMailResponse Then
        Exit Sub
    End If

    If ReadingMailIndex > 0 And ReadingMailIndex <= MaxPlayerMail Then
        If MailingWindowState = WindowMailState_Reading Then
            If Mail(ReadingMailIndex).AttachCurrency > 0 Then
                If Not Mail(ReadingMailIndex).AttachCurrencyReceiveFlag Then
                    Call SendReceiveMailCurrency(ReadingMailIndex)
                End If
            End If
        End If
    End If

End Sub

Private Sub WriteMailItem_MouseDown()
    ' Retira o cursor do textbox
    Windows(WindowIndex).activeControl = 0

    If (mouseClick(VK_LBUTTON) And lastMouseClick(VK_LBUTTON) = 0) Then
        Dim wIndex As Long

        wIndex = GetWindowIndex("winInventory")

        If Not Windows(wIndex).Window.Visible Then
            Windows(wIndex).Window.Visible = True
        End If

        Exit Sub
    End If

    If (mouseClick(VK_RBUTTON) And lastMouseClick(VK_RBUTTON) = 0) Then
        SendMailItemInventoryIndex = 0
        SendMailItemValue = 0
    End If
    
    WriteMailItem_MouseMove
End Sub

Private Sub WriteMailItem_MouseMove()

    If SendMailItemInventoryIndex > 0 Then
        Dim ItemId As Long

        ItemId = GetInventoryItemNum(SendMailItemInventoryIndex)

        If ItemId > 0 And ItemId <= MaximumItems Then
            Dim Inventory As InventoryRec
            Dim WinDescription As Long
            Dim X As Long, Y As Long

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

            Inventory.Num = GetInventoryItemNum(SendMailItemInventoryIndex)
            Inventory.Value = GetInventoryItemValue(SendMailItemInventoryIndex)
            Inventory.Level = GetInventoryItemLevel(SendMailItemInventoryIndex)
            Inventory.Bound = GetInventoryItemBound(SendMailItemInventoryIndex)
            Inventory.AttributeId = GetInventoryItemAttributeId(SendMailItemInventoryIndex)
            Inventory.UpgradeId = GetInventoryItemUpgradeId(SendMailItemInventoryIndex)

            If Item(ItemId).Type = ItemType.ItemType_Heraldry Then
                Call ShowHeraldryDescription(X, Y, Inventory, Item(ItemId).Price)
            Else
                ShowItemDesc X, Y, Inventory
            End If
        End If
    End If
    
End Sub

Private Sub ReadMailItem_MouseDown()
    If WaitingMailResponse Then
        Exit Sub
    End If

    If ReadingMailIndex > 0 And ReadingMailIndex <= MaxPlayerMail Then
        If MailingWindowState = WindowMailState_Reading Then
            If Mail(ReadingMailIndex).AttachItem.Num > 0 Then
                If Not Mail(ReadingMailIndex).AttachItemReceiveFlag Then
                    Call SendReceiveMailItem(ReadingMailIndex)
                End If
            End If
        End If
    End If

    ReadMailItem_MouseMove
End Sub

Private Sub ReadMailItem_MouseMove()
    If WaitingMailResponse Then
        Exit Sub
    End If

    If ReadingMailIndex > 0 Then
        Dim ItemId As Long

        ItemId = Mail(ReadingMailIndex).AttachItem.Num

        If ItemId > 0 And ItemId <= MaximumItems Then
            Dim Inventory As InventoryRec
            Dim WinDescription As Long
            Dim X As Long, Y As Long

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

            Inventory.Num = Mail(ReadingMailIndex).AttachItem.Num
            Inventory.Value = Mail(ReadingMailIndex).AttachItem.Value
            Inventory.Level = Mail(ReadingMailIndex).AttachItem.Level
            Inventory.Bound = Mail(ReadingMailIndex).AttachItem.Bound
            Inventory.AttributeId = Mail(ReadingMailIndex).AttachItem.AttributeId
            Inventory.UpgradeId = Mail(ReadingMailIndex).AttachItem.UpgradeId

            If Item(ItemId).Type = ItemType.ItemType_Heraldry Then
                Call ShowHeraldryDescription(X, Y, Inventory, Item(ItemId).Price)
            Else
                ShowItemDesc X, Y, Inventory
            End If
        End If
    End If

End Sub

Private Sub OpenReadMail(ByVal ControlIndex As Long)
    If WaitingMailResponse Then
        Exit Sub
    End If

    Dim Index As Long

    Index = ((MailPage - 1) * MaxMailList) + ControlIndex

    If Index <= MaxPlayerMail Then
        If LenB(Mail(Index).Subject) > 0 Then
            ReadingMailIndex = Index

            If Not Mail(ReadingMailIndex).ReadFlag Then
                Mail(ReadingMailIndex).ReadFlag = True
                Call SendUpdateMailReadFlag(ReadingMailIndex)
            End If

            Call UpdateOpenedMail
            Call ChangeWindowMailState(WindowMailState_Reading)

        End If
    End If
End Sub

Public Sub UpdateOpenedMail()
    If ReadingMailIndex > 0 And ReadingMailIndex <= MaxPlayerMail Then
        Windows(WindowIndex).Controls(GetControlIndex("winMail", "lblText")).Text = Trim$(Mail(ReadingMailIndex).Content)

        If Mail(ReadingMailIndex).AttachCurrencyReceiveFlag Then
            Windows(WindowIndex).Controls(GetControlIndex("winMail", "lblCurrency")).Text = "Ouro: 0"
        Else
            Windows(WindowIndex).Controls(GetControlIndex("winMail", "lblCurrency")).Text = "Ouro: " & Mail(ReadingMailIndex).AttachCurrency
        End If
    End If
End Sub

' Click
Private Sub Button_ReadMail1()
    Call OpenReadMail(1)
End Sub
Private Sub Button_ReadMail2()
    Call OpenReadMail(2)
End Sub
Private Sub Button_ReadMail3()
    Call OpenReadMail(3)
End Sub
Private Sub Button_ReadMail4()
    Call OpenReadMail(4)
End Sub
Private Sub Button_ReadMail5()
    Call OpenReadMail(5)
End Sub
Private Sub Button_ReadMail6()
    Call OpenReadMail(6)
End Sub
Private Sub Button_ReadMail7()
    Call OpenReadMail(7)
End Sub
Private Sub Button_ReadMail8()
    Call OpenReadMail(8)
End Sub
Private Sub Button_ReadMail9()
    Call OpenReadMail(9)
End Sub

Public Sub ButtonMenu_Mail()
    If MyIndex > 0 Then
        If GetPlayerDead(MyIndex) Then Exit Sub
    End If

    If Windows(WindowIndex).Window.Visible Then
        InMail = False
        HideWindow WindowIndex
        
        CanMoveNow = True

    Else
        InMail = True
        ReadingMailIndex = 0
        
        CanMoveNow = False
        
        Call SetMailPageCount
        Call CanEnableMailCheckBox
        Call ChangeWindowMailState(WindowMailState_Listing)
        
        ShowWindow WindowIndex, , False
    End If
End Sub

Private Sub Draw_SendItem()
    If MailingWindowState = WindowMailState.WindowMailState_Writing Then
        If SendMailItemInventoryIndex > 0 Then
            Dim IconId As Long, ItemId As Long
            Dim xO As Long, yO As Long
            Dim Amount As Long

            xO = Windows(WindowIndex).Window.Left
            yO = Windows(WindowIndex).Window.Top

            ItemId = GetInventoryItemNum(SendMailItemInventoryIndex)
            Amount = SendMailItemValue

            If ItemId > 0 And ItemId <= MaximumItems Then
                IconId = Item(ItemId).IconId

                If IconId > 0 Then
                    RenderTexture Tex_Item(IconId), xO + PictureItemLeft + 1, yO + PictureItemTop + 1, 0, 0, 38, 38, PIC_X, PIC_Y
                    
                    If Amount > 1 Then
                        RenderText Font(Fonts.FontRegular), ConvertCurrency(Amount), xO + PictureItemLeft, yO + PictureItemTop, GetCurrencyColor(Amount)
                    End If
                End If
            End If
        End If
    End If
End Sub

Private Sub Draw_ReceivedItem()
    If MailingWindowState = WindowMailState.WindowMailState_Reading Then
        If ReadingMailIndex > 0 Then
            Dim IconId As Long, ItemId As Long
            Dim xO As Long, yO As Long
            Dim Amount As Long

            xO = Windows(WindowIndex).Window.Left
            yO = Windows(WindowIndex).Window.Top

            ItemId = Mail(ReadingMailIndex).AttachItem.Num
            Amount = Mail(ReadingMailIndex).AttachItem.Value

            If ItemId > 0 And ItemId <= MaximumItems Then
                IconId = Item(ItemId).IconId

                If IconId > 0 Then
                    If Mail(ReadingMailIndex).AttachItemReceiveFlag Then
                        RenderTexture Tex_Item(IconId), xO + PictureItemLeft + 1, yO + PictureItemTop + 1, 0, 0, 38, 38, PIC_X, PIC_Y, DX8Colour(White, 120)

                        If Amount > 1 Then
                            RenderText Font(Fonts.FontRegular), ConvertCurrency(Amount), xO + PictureItemLeft + 1, yO + PictureItemTop + 1, GetCurrencyColor(Amount)
                        End If
                    Else
                        RenderTexture Tex_Item(IconId), xO + PictureItemLeft + 1, yO + PictureItemTop + 1, 0, 0, 38, 38, PIC_X, PIC_Y

                        If Amount > 1 Then
                            RenderText Font(Fonts.FontRegular), ConvertCurrency(Amount), xO + PictureItemLeft, yO + PictureItemTop, GetCurrencyColor(Amount)
                        End If
                    End If
                End If
            End If
        End If
    End If
End Sub

Private Sub Draw_Mail()
    Dim xO As Long, yO As Long, Width As Long, i As Long
    Dim MailIndex As Long

    xO = Windows(WindowIndex).Window.Left
    yO = Windows(WindowIndex).Window.Top
    Width = Windows(WindowIndex).Window.Width

    

    If MailingWindowState = WindowMailState.WindowMailState_Listing Then
        If MaxPlayerMail > 0 Then
            For i = 1 To MaxMailList
                MailIndex = ((MailPage - 1) * MaxMailList) + i

                If MailIndex <= MaxPlayerMail Then
                    If LenB(Mail(MailIndex).Subject) > 0 Then
                        If Mail(MailIndex).ReadFlag Then
                            RenderText Font(Fonts.FontRegular), Mail(MailIndex).Subject, xO + PictureBackLeft + 10, yO + PictureBackTop + 5 + (PictureBackOffSetY * (i - 1)), Grey
                        Else
                            RenderText Font(Fonts.FontRegular), Mail(MailIndex).Subject, xO + PictureBackLeft + 10, yO + PictureBackTop + 5 + (PictureBackOffSetY * (i - 1)), White
                        End If
                    End If
                End If
            Next
        End If
    End If

    If MailingWindowState = WindowMailState.WindowMailState_Reading Then
        If ReadingMailIndex > 0 Then
            RenderText Font(Fonts.FontRegular), Mail(ReadingMailIndex).SenderCharacterName, xO + ReadingMailLeft + 10, yO + PictureSenderTop + 5, White
            RenderText Font(Fonts.FontRegular), Mail(ReadingMailIndex).Subject, xO + ReadingMailLeft + 10, yO + PictureTitleTop + 5, White
            RenderText Font(Fonts.FontRegular), Received & Mail(ReadingMailIndex).SendDate, xO + ReadingMailLeft + 10, yO + PictureSendDateTop + 5, White
            RenderText Font(Fonts.FontRegular), Expiration & Mail(ReadingMailIndex).ExpireDate, xO + ReadingMailLeft + 10, yO + PictureExpireDateTop + 5, White
        End If
    End If

    If MailingWindowState = WindowMailState_Writing Then
        RenderText Font(Fonts.FontRegular), Sended & SendDate, xO + ReadingMailLeft + 10, yO + PictureSendDateTop + 5, White
        RenderText Font(Fonts.FontRegular), Expiration & "-", xO + ReadingMailLeft + 10, yO + PictureExpireDateTop + 5, White
    End If

End Sub

Private Sub ChangeListingPictureVisibility(ByVal Visible As Boolean)
    Dim ButtonSelectIndex As Long
    Dim ButtonDeleteIndex As Long
    Dim ControlIndex As Long
    Dim i As Long

    For i = 1 To MaxMailList
        ControlIndex = GetControlIndex("winMail", "chkSelected" & i)
        Windows(WindowIndex).Controls(ControlIndex).Visible = Visible

        ControlIndex = GetControlIndex("winMail", "picBack" & i)
        Windows(WindowIndex).Controls(ControlIndex).Visible = Visible
    Next

    Windows(WindowIndex).Controls(GetControlIndex("winMail", "lblPage")).Visible = Visible
    Windows(WindowIndex).Controls(GetControlIndex("winMail", "btnUp")).Visible = Visible
    Windows(WindowIndex).Controls(GetControlIndex("winMail", "btnDown")).Visible = Visible

    ButtonSelectIndex = GetControlIndex("winMail", "btnSelectAll")
    ButtonDeleteIndex = GetControlIndex("winMail", "btnDelete")

    Windows(WindowIndex).Controls(ButtonSelectIndex).Visible = Visible
    Windows(WindowIndex).Controls(ButtonDeleteIndex).Visible = Visible

    Windows(WindowIndex).Controls(ButtonSelectIndex).Left = 47
    Windows(WindowIndex).Controls(ButtonDeleteIndex).Left = 204
End Sub

Private Sub ChangeReadingPictureVisibility(ByVal Visible As Boolean)
    Dim ButtonSelectIndex As Long
    Dim ButtonDeleteIndex As Long

    Windows(WindowIndex).Controls(GetControlIndex("winMail", "picSender")).Visible = Visible
    Windows(WindowIndex).Controls(GetControlIndex("winMail", "picSubject")).Visible = Visible
    Windows(WindowIndex).Controls(GetControlIndex("winMail", "picContent")).Visible = Visible

    Windows(WindowIndex).Controls(GetControlIndex("winMail", "picSendDate")).Visible = Visible
    Windows(WindowIndex).Controls(GetControlIndex("winMail", "picExpireDate")).Visible = Visible
    Windows(WindowIndex).Controls(GetControlIndex("winMail", "picAttachCoin")).Visible = Visible
    Windows(WindowIndex).Controls(GetControlIndex("winMail", "picAttachItem")).Visible = Visible

    Windows(WindowIndex).Controls(GetControlIndex("winMail", "lblCurrency")).Visible = Visible
    Windows(WindowIndex).Controls(GetControlIndex("winMail", "lblText")).Visible = Visible
    Windows(WindowIndex).Controls(GetControlIndex("winMail", "lblReceiveItem")).Visible = Visible

    ButtonSelectIndex = GetControlIndex("winMail", "btnSelectAll")
    ButtonDeleteIndex = GetControlIndex("winMail", "btnDelete")

    Windows(WindowIndex).Controls(ButtonSelectIndex).Visible = Not Visible
    Windows(WindowIndex).Controls(ButtonDeleteIndex).Visible = Visible

    Windows(WindowIndex).Controls(ButtonDeleteIndex).Left = 125
End Sub

Private Sub ChangeWritingPictureVisibility(ByVal Visible As Boolean)
    Dim ButtonSelectIndex As Long
    Dim ButtonDeleteIndex As Long

    Windows(WindowIndex).Controls(GetControlIndex("winMail", "txtReceiver")).Visible = Visible
    Windows(WindowIndex).Controls(GetControlIndex("winMail", "txtSubject")).Visible = Visible
    Windows(WindowIndex).Controls(GetControlIndex("winMail", "txtContent")).Visible = Visible

    If Visible Then
        Windows(WindowIndex).Controls(GetControlIndex("winMail", "txtReceiver")).Text = "Destinatário"
        Windows(WindowIndex).Controls(GetControlIndex("winMail", "txtSubject")).Text = "Título"
        Windows(WindowIndex).Controls(GetControlIndex("winMail", "txtContent")).Text = "Mensagem"
    End If

    Windows(WindowIndex).Controls(GetControlIndex("winMail", "picSendDate")).Visible = Visible
    Windows(WindowIndex).Controls(GetControlIndex("winMail", "picExpireDate")).Visible = Visible
    Windows(WindowIndex).Controls(GetControlIndex("winMail", "picSendAttachCoin")).Visible = Visible
    Windows(WindowIndex).Controls(GetControlIndex("winMail", "picSendAttachItem")).Visible = Visible

    Windows(WindowIndex).Controls(GetControlIndex("winMail", "lblSendAttachCoin")).Visible = Visible
    Windows(WindowIndex).Controls(GetControlIndex("winMail", "lblWriteItem")).Visible = Visible

    Windows(WindowIndex).Controls(GetControlIndex("winMail", "btnSend")).Visible = Visible

    ButtonSelectIndex = GetControlIndex("winMail", "btnSelectAll")
    ButtonDeleteIndex = GetControlIndex("winMail", "btnDelete")

    Windows(WindowIndex).Controls(ButtonSelectIndex).Visible = False
    Windows(WindowIndex).Controls(ButtonDeleteIndex).Visible = False

End Sub

Private Sub ChangeWindowMailState(ByVal State As WindowMailState)
    MailingWindowState = State

    Call ChangeListingPictureVisibility(False)
    Call ChangeReadingPictureVisibility(False)
    Call ChangeWritingPictureVisibility(False)

    Select Case State
    Case WindowMailState.WindowMailState_Listing
        Call ChangeListingPictureVisibility(True)

    Case WindowMailState.WindowMailState_Reading
        Call ChangeReadingPictureVisibility(True)

    Case WindowMailState.WindowMailState_Writing
        Call ChangeWritingPictureVisibility(True)

    End Select

End Sub

Public Sub CanEnableMailCheckBox()
    Dim i As Long, MailIndex As Long
    Dim ControlIndex As Long

    For i = 1 To MaxMailList
        MailIndex = ((MailPage - 1) * MaxMailList) + i
        ControlIndex = GetControlIndex("winMail", "chkSelected" & i)

        Windows(WindowIndex).Controls(ControlIndex).Enabled = False
        Windows(WindowIndex).Controls(ControlIndex).Value = 0

        If MailIndex <= MaxPlayerMail Then
            If LenB(Mail(MailIndex).Subject) > 0 Then
                Windows(WindowIndex).Controls(ControlIndex).Enabled = True
            End If
        End If
    Next

End Sub

Private Sub Button_ShowRead()
' Não permite que nada seja alterado sem antes ter recebido a resposta do servidor.
    If WaitingMailResponse Then
        Exit Sub
    End If

    Windows(WindowIndex).Controls(ButtonReadIndex).TextColour = Green
    Windows(WindowIndex).Controls(ButtonReadIndex).TextColourHover = Green
    Windows(WindowIndex).Controls(ButtonReadIndex).TextColourClick = Green

    Windows(WindowIndex).Controls(ButtonWriteIndex).TextColour = White
    Windows(WindowIndex).Controls(ButtonWriteIndex).TextColourHover = White
    Windows(WindowIndex).Controls(ButtonWriteIndex).TextColourClick = White

    CanSwapInvItems = True
    ReadingMailIndex = 0
    Call ChangeWindowMailState(WindowMailState_Listing)
End Sub

Private Sub Button_ShowWrite()
' Não permite que nada seja alterado sem antes ter recebido a resposta do servidor.
    If WaitingMailResponse Then
        Exit Sub
    End If

    Windows(WindowIndex).Controls(ButtonReadIndex).TextColour = White
    Windows(WindowIndex).Controls(ButtonReadIndex).TextColourHover = White
    Windows(WindowIndex).Controls(ButtonReadIndex).TextColourClick = White

    Windows(WindowIndex).Controls(ButtonWriteIndex).TextColour = Green
    Windows(WindowIndex).Controls(ButtonWriteIndex).TextColourHover = Green
    Windows(WindowIndex).Controls(ButtonWriteIndex).TextColourClick = Green

    SendMailItemInventoryIndex = 0
    SendMailItemValue = 1

    CanSwapInvItems = False
    ReadingMailIndex = 0
    SendDate = Format(Date, "dd/mm/yyyy") & " " & Format(Time, "hh:mm")
    Call ChangeWindowMailState(WindowMailState_Writing)
    Call AddSendMailCurrencyValue(0)
End Sub

Private Sub SetMailPageCount()
    Dim Rest As Double

    MailPage = 1
    MailPageCount = CInt(MaxPlayerMail / MaxMailList)
    Rest = MaxPlayerMail Mod MaxMailList

    If Rest > 0 Then MailPageCount = MailPageCount + 1
    If MailPageCount = 0 Then MailPageCount = 1

    Windows(WindowIndex).Controls(GetControlIndex("winMail", "lblPage")).Text = "Página: " & MailPage & "/" & MailPageCount
End Sub

Private Sub Button_PageUp()
' Não permite que nada seja alterado sem antes ter recebido a resposta do servidor.
    If WaitingMailResponse Then
        Exit Sub
    End If

    If MailPage < MailPageCount Then
        MailPage = MailPage + 1
        Windows(WindowIndex).Controls(GetControlIndex("winMail", "lblPage")).Text = "Página: " & MailPage & "/" & MailPageCount

        CanEnableMailCheckBox
    End If

End Sub

Private Sub Button_PageDown()
' Não permite que nada seja alterado sem antes ter recebido a resposta do servidor.
    If WaitingMailResponse Then
        Exit Sub
    End If

    If MailPage > 1 Then
        MailPage = MailPage - 1
        Windows(WindowIndex).Controls(GetControlIndex("winMail", "lblPage")).Text = "Página: " & MailPage & "/" & MailPageCount

        CanEnableMailCheckBox
    End If

End Sub

Private Sub SelectAll()
' Não permite que nada seja alterado sem antes ter recebido a resposta do servidor.
    If WaitingMailResponse Then
        Exit Sub
    End If

    If MailingWindowState <> WindowMailState_Listing Then
        Exit Sub
    End If

    Dim i As Long, MailIndex As Long
    Dim ControlIndex As Long

    For i = 1 To MaxMailList
        MailIndex = ((MailPage - 1) * MaxMailList) + i
        ControlIndex = GetControlIndex("winMail", "chkSelected" & i)

        If MailIndex <= MaxPlayerMail Then
            If LenB(Mail(MailIndex).Subject) > 0 Then
                Windows(WindowIndex).Controls(ControlIndex).Value = 1
            End If
        End If
    Next
End Sub

Public Sub DeselectAll()
    Dim i As Long
    Dim ControlIndex As Long

    For i = 1 To MaxMailList
        ControlIndex = GetControlIndex("winMail", "chkSelected" & i)
        Windows(WindowIndex).Controls(ControlIndex).Value = 0
    Next

End Sub

Private Sub DeleteMail()
' Não permite que nada seja alterado sem antes ter recebido a resposta do servidor.
    If WaitingMailResponse Then
        Exit Sub
    End If

    DeletedMailCount = 0
    Erase DeletedMailIndex

    If MailingWindowState = WindowMailState_Listing Then
        Dim i As Long, MailIndex As Long
        Dim ControlIndex As Long

        For i = 1 To MaxMailList
            MailIndex = ((MailPage - 1) * MaxMailList) + i
            ControlIndex = GetControlIndex("winMail", "chkSelected" & i)

            If Windows(WindowIndex).Controls(ControlIndex).Value = 1 Then
                DeletedMailCount = DeletedMailCount + 1

                ReDim Preserve DeletedMailIndex(1 To DeletedMailCount)
                DeletedMailIndex(DeletedMailCount) = MailIndex
            End If
        Next
    End If

    If MailingWindowState = WindowMailState_Reading Then
        If ReadingMailIndex > 0 And ReadingMailIndex <= MaxPlayerMail Then
            DeletedMailCount = 1

            ReDim Preserve DeletedMailIndex(1 To DeletedMailCount)
            DeletedMailIndex(DeletedMailCount) = ReadingMailIndex
        End If
    End If

    If DeletedMailCount > 0 Then
        ShowDialogue "DELETAR", "Deseja realmente deletar a(s) correspondências?", "", DialogueTypeDeleteMail, DialogueStyleYesNo, 0
    End If

End Sub

Private Sub SendNewMail()
    If MailingWindowState <> WindowMailState_Writing Then
        Exit Sub
    End If

    Dim Mail As SendMailRec

    Mail.ReceiverCharacterName = Windows(WindowIndex).Controls(GetControlIndex("winMail", "txtReceiver")).Text
    Mail.Subject = Windows(WindowIndex).Controls(GetControlIndex("winMail", "txtSubject")).Text
    Mail.Content = Windows(WindowIndex).Controls(GetControlIndex("winMail", "txtContent")).Text
    Mail.AttachCurrency = SendMailCurrency
    Mail.AttachItemInventoryIndex = SendMailItemInventoryIndex
    Mail.AttachItemValue = SendMailItemValue

    If LenB(Mail.ReceiverCharacterName) = 0 Then
        AddText "Necessário adicionar um destinatário.", BrightRed
        Exit Sub
    End If

    WaitingMailResponse = True
    
    Call SendMail(Mail)

End Sub

Private Sub Button_AddSendMailCurrency()
    ShowDialogue "Inserir Ouro", "Insira a quantidade de ouro para enviar", "", DialogueTypeAddMailCurrency, DialogueStyleInput, 0
End Sub

Public Sub AddSendMailCurrencyValue(ByVal Value As Long)
' Retira o cursor do textbox
    Windows(WindowIndex).activeControl = 0

    If Value > GetPlayerCurrency(CurrencyType.Currency_Gold) Then
        Value = GetPlayerCurrency(CurrencyType.Currency_Gold)
    End If

    SendMailCurrency = Value
    Windows(WindowIndex).Controls(GetControlIndex("winMail", "lblSendAttachCoin")).Text = "Ouro: " & SendMailCurrency
End Sub

Public Sub AddSendMailItemValue(ByVal Value As Long)
    If Value < 0 Then Value = 0
    ' Retira o cursor do textbox
    Windows(WindowIndex).activeControl = 0

    If SendMailItemInventoryIndex > 0 Then

        If Value > GetInventoryItemValue(SendMailItemInventoryIndex) Then
            Value = GetInventoryItemValue(SendMailItemInventoryIndex)
        End If

        SendMailItemValue = Value

        If SendMailItemValue = 0 Then
            SendMailItemInventoryIndex = 0
        End If
    Else
        SendMailItemValue = 0
    End If
End Sub

' Recebe o item do inventário para a janela de correio.
Public Sub DragBox_CheckInventoryToMail()
    Dim InventoryIndex As Long
    Dim ItemId As Long

    ' Retira o cursor de qualquer texto.
    Windows(WindowIndex).activeControl = 0

    If DragBox.Origin = OriginInventory And DragBox.Type = PartItem Then
        If DragBox.Slot > 0 Then
            If DragBox.Value > 0 Then

                ItemId = DragBox.Value
                InventoryIndex = DragBox.Slot

                If GetInventoryItemBound(InventoryIndex) = InventoryBoundType_None Then
                    SendMailItemInventoryIndex = InventoryIndex

                    If Item(ItemId).Stackable Then
                        ShowDialogue "Anexar Item", "Insira a quantidade de para enviar", "", DialogueTypeAddMailAmount, DialogueStyleInput, 0
                    Else
                        SendMailItemValue = 1
                    End If
                End If

            End If
        End If
    End If

End Sub

