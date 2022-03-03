Attribute VB_Name = "Mail_Packet"
Option Explicit

Public Sub SendMail(ByRef Mail As SendMailRec)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PSendMail
    Buffer.WriteString Mail.ReceiverCharacterName
    Buffer.WriteString Mail.Subject
    Buffer.WriteString Mail.Content
    Buffer.WriteLong Mail.AttachCurrency
    Buffer.WriteLong Mail.AttachItemInventoryIndex
    Buffer.WriteLong Mail.AttachItemValue

    SendData Buffer.ToArray()

    Set Buffer = Nothing
End Sub

Public Sub SendUpdateMailReadFlag(ByVal MailIndex As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PUpdateMailReadFlag
    Buffer.WriteLong Mail(MailIndex).Id

    SendData Buffer.ToArray()

    Set Buffer = Nothing
End Sub

Public Sub SendDeleteMail()
' Se não houver nenhum dado selecionado.
    If DeletedMailCount = 0 Then
        Exit Sub
    End If

    Dim i As Long, Length As Long
    Dim Index As Long

    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PDeleteMail
    Buffer.WriteLong DeletedMailCount

    For i = 1 To DeletedMailCount
        Index = DeletedMailIndex(i)
        Buffer.WriteLong Mail(Index).Id
    Next

    SendData Buffer.ToArray()

    WaitingMailResponse = True

    Set Buffer = Nothing
End Sub

Public Sub SendReceiveMailCurrency(ByVal MailIndex As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PReiceveMailCurrency
    Buffer.WriteLong Mail(MailIndex).Id

    SendData Buffer.ToArray()

    Set Buffer = Nothing
End Sub

Public Sub SendReceiveMailItem(ByVal MailIndex As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PReceiveMailItem
    Buffer.WriteLong Mail(MailIndex).Id

    SendData Buffer.ToArray()

    Set Buffer = Nothing
End Sub

Public Sub HandleMailOperationResult(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim OperationCode As MailOperationCode
    Dim Message As String
    Dim Color As Long

    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data

    OperationCode = Buffer.ReadLong

    If OperationCode = MailOperationCode_Sended Then
        Message = "A correspondência foi enviada."
        Color = ColorType.BrightGreen

        AddText ColourChar & GetColStr(Color) & "[Sistema] : " & ColourChar & GetColStr(Grey) & Message, Grey, , ChatChannel.chGame

        Call UpdateMailList
    End If

    If OperationCode = MailOperationCode_AttachedNotReceived Then
        Message = "Algumas correspondência não foram deletadas, necessário retirar os itens antes."
        Color = ColorType.BrightRed
        
        AddText ColourChar & GetColStr(Color) & "[Sistema] : " & ColourChar & GetColStr(Grey) & Message, Grey, , ChatChannel.chGame

        Call UpdateMailList
    End If

    WaitingMailResponse = False
    CanSwapInvItems = True

    Set Buffer = Nothing

End Sub

Public Sub HandleUpdateMail(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Id As Long
    Dim i As Long

    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data

    Id = Buffer.ReadLong
    
    Index = FindMailIndex(Id)

    If Index > 0 Then
        Mail(Index).AttachCurrencyReceiveFlag = CBool(Buffer.ReadByte)
        Mail(Index).AttachItemReceiveFlag = CBool(Buffer.ReadByte)
    End If

    UpdateOpenedMail

    Set Buffer = Nothing
End Sub

Public Sub HandleDeletedMail(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Count As Long, Id As Long, i As Long
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data

    Count = Buffer.ReadLong

    If Count > 0 Then
        For i = 1 To Count
            Id = Buffer.ReadLong

            Index = FindMailIndex(Id)

            If Index > 0 Then
                Call ClearPlayerMail(Index)
            End If
        Next

        Call SortPlayerMail
    End If

    DeselectAll
    UpdateMailList

    ' 'Reativa' os controles
    WaitingMailResponse = False

    Set Buffer = Nothing
End Sub

Public Sub HandleAddMail(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data

    Index = FindFreeMailIndex()

    If Index > 0 Then
        Mail(Index).Id = Buffer.ReadLong
        Mail(Index).SenderCharacterName = Buffer.ReadString
        Mail(Index).Subject = Buffer.ReadString
        Mail(Index).Content = Buffer.ReadString
        Mail(Index).ReadFlag = CBool(Buffer.ReadByte)
        Mail(Index).AttachCurrency = Buffer.ReadLong
        Mail(Index).AttachCurrencyReceiveFlag = CBool(Buffer.ReadByte)
        Mail(Index).AttachItemReceiveFlag = CBool(Buffer.ReadByte)
        Mail(Index).SendDate = Buffer.ReadString
        Mail(Index).ExpireDate = Buffer.ReadString
        Mail(Index).AttachItem.Num = Buffer.ReadLong
        Mail(Index).AttachItem.Value = Buffer.ReadLong
        Mail(Index).AttachItem.Level = Buffer.ReadLong
        Mail(Index).AttachItem.Bound = CBool(Buffer.ReadByte)
        Mail(Index).AttachItem.AttributeId = Buffer.ReadLong
        Mail(Index).AttachItem.UpgradeId = Buffer.ReadLong

        Call UpdateMailList
    End If

    Set Buffer = Nothing
End Sub

Public Sub HandleMailing(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Count As Long, i As Long

    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data
    Count = Buffer.ReadLong
    
    ClearPlayerMails

    For i = 1 To Count
        Mail(i).Id = Buffer.ReadLong
        Mail(i).SenderCharacterName = Buffer.ReadString
        Mail(i).Subject = Buffer.ReadString
        Mail(i).Content = Buffer.ReadString
        Mail(i).ReadFlag = CBool(Buffer.ReadByte)
        Mail(i).AttachCurrency = Buffer.ReadLong
        Mail(i).AttachCurrencyReceiveFlag = CBool(Buffer.ReadByte)
        Mail(i).AttachItemReceiveFlag = CBool(Buffer.ReadByte)
        Mail(i).SendDate = Buffer.ReadString
        Mail(i).ExpireDate = Buffer.ReadString
        Mail(i).AttachItem.Num = Buffer.ReadLong
        Mail(i).AttachItem.Value = Buffer.ReadLong
        Mail(i).AttachItem.Level = Buffer.ReadLong
        Mail(i).AttachItem.Bound = CBool(Buffer.ReadByte)
        Mail(i).AttachItem.AttributeId = Buffer.ReadLong
        Mail(i).AttachItem.UpgradeId = Buffer.ReadLong
    Next

    UpdateMailList

    Set Buffer = Nothing

End Sub

