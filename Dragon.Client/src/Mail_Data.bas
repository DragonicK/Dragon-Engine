Attribute VB_Name = "Mail_Data"
Option Explicit

' Quantidade de correspondências selecionadas para exclusão.
Public DeletedMailCount As Long
' Indice das correspondências selecionadas para exclusão.
Public DeletedMailIndex() As Long
' Quantidade máxima de correspondências do jogador.
Public MaxPlayerMail As Long

Public Mail() As MailRec

Public Enum MailOperationCode
    MailOperationCode_Sended
    MailOperationCode_Deleted
    MailOperationCode_Invalid
    MailOperationCode_InvalidItem
    MailOperationCode_InvalidReceiver
    MailOperationCode_ItemNotReceived
    MailOperationCode_CurrencyNotReceived
    MailOperationCode_CurrencyIsNotEnough
    MailOperationCode_AttachedNotReceived
End Enum

Public Type SendMailRec
    ReceiverCharacterName As String
    Subject As String
    Content As String
    AttachCurrency As Long
    AttachItemInventoryIndex As Long
    AttachItemValue As Long
End Type

Private Type MailRec
    Id As Long
    SenderCharacterName As String
    Subject As String
    Content As String
    ReadFlag As Boolean
    AttachCurrency As Long
    AttachCurrencyReceiveFlag As Boolean
    AttachItemReceiveFlag As Boolean
    AttachItem As InventoryRec
    SendDate As String
    ExpireDate As String
End Type

Public Sub SortPlayerMail()
    Dim TempMail() As MailRec
    Dim Count As Long, i As Long
    
    ReDim TempMail(1 To MaxPlayerMail)
    
    Count = 1

    For i = 1 To MaxPlayerMail
        If LenB(Mail(i).SenderCharacterName) > 0 Then
            Call CopyMemory(TempMail(Count), Mail(i), LenB(Mail(i)))
            Count = Count + 1
        End If
    Next
    
    Erase Mail
    
    Mail = TempMail
End Sub

Public Function FindFreeMailIndex() As Long
    Dim i As Long
    
    For i = 1 To MaxPlayerMail
        If LenB(Mail(i).SenderCharacterName) = 0 Then
            FindFreeMailIndex = i
            Exit Function
        End If
    Next

End Function

Public Function FindMailIndex(ByVal Id As Long) As Long
    Dim i As Long

    For i = 1 To MaxPlayerMail
        ' Somente verifica se há alguma correspondência adicionada.
        If LenB(Mail(i).SenderCharacterName) > 0 Then
            If Mail(i).Id = Id Then
                FindMailIndex = i
                Exit Function
            End If
        End If
    Next

End Function

Public Sub ClearPlayerMails()
    Dim i As Long

    For i = 1 To MaxPlayerMail
        Call ClearPlayerMail(i)
    Next
    
End Sub

Public Sub ClearPlayerMail(ByVal Index As Long)
    Call ZeroMemory(ByVal VarPtr(Mail(Index)), LenB(Mail(Index)))

    Mail(Index).SenderCharacterName = vbNullString
    Mail(Index).Subject = vbNullString
    Mail(Index).Content = vbNullString
    Mail(Index).SendDate = vbNullString
    Mail(Index).ExpireDate = vbNullString
End Sub

Public Sub InitializePlayerMails()
    ReDim Mail(1 To MaxPlayerMail)
End Sub



