Attribute VB_Name = "Dialogue_Implementation"
Option Explicit

Public Sub CloseDialogue()
    Call ClearDialogue

    HideWindow GetWindowIndex("winBlank")
    HideWindow GetWindowIndex("winDialogue")
End Sub

Public Sub ClearDialogue()
    With Dialogue
        .Index = 0
        .DataLong = 0
        .DataString = vbNullString
        .Style = 0
    End With
End Sub

Public Sub DialogueAlert(ByVal Index As AlertMessage)
    Dim Header As String, Body As String, Body2 As String

    ' find the body/header
    Select Case Index

    Case AlertMessage.Alert_Failed
        Header = "Falha"
        Body = "Falha na opera��o."
        Body2 = "Entre em contato com o administrador."

    Case AlertMessage.Alert_connection
        Header = "Problema de Conex�o"
        Body = "A conex�o foi o servidor foi perdida."
        Body2 = "Tente novamente mais tarde."

    Case AlertMessage.Alert_AccountIsBanned
        Header = "Banido"
        Body = "Voce foi banido do servidor."
        Body2 = "Entre em contato com o administrador."

    Case AlertMessage.Alert_Kicked
        Header = "Chutado"
        Body = "Voce foi chutado do servidor."
        Body2 = "Comporte-se."

    Case AlertMessage.Alert_VersionOutdated
        Header = "Versao Errada"
        Body = "A versao do jogo esta errada."
        Body2 = "Uma atualizacao � necess�ria."

    Case AlertMessage.Alert_StringLength
        Header = "Tamanho Inv�lido"
        Body = "Usu�rio ou senha est� pequeno ou grande demais."
        Body2 = "Insira um usu�rio e senha v�lidos."

    Case AlertMessage.Alert_IllegalName
        Header = "Caracteres Ilegais"
        Body = "Usu�rio ou senha cont�m caracteres ilegais."
        Body2 = "Insira um usu�rio e senha v�lidos"

    Case AlertMessage.Alert_Maintenance
        Header = "Manuten��o"
        Body = "O servidor est� em manuten��o"
        Body2 = "Tente novamente mais tarde"

    Case AlertMessage.Alert_NameTaken
        Header = "Nome Inv�lido"
        Body = "O nome est� sendo usado."
        Body2 = "Tente um nome diferente."

    Case AlertMessage.Alert_NameLength
        Header = "Nome Inv�lido"
        Body = "O nome � muito grande ou pequeno demais."
        Body2 = "Tente um nome diferente."

    Case AlertMessage.Alert_NameIllegal
        Header = "Nome Inv�lido"
        Body = "O nome cont�m caracteres ilegais."
        Body2 = "Use letras e n�meros somente."

    Case AlertMessage.Alert_Database
        Header = "Problema de Conex�o"
        Body = "N�o pode se conectar ao banco de dados."
        Body2 = "Tente novamente mais tarde."

    Case AlertMessage.Alert_WrongAccountData
        Header = "Login Inv�lido"
        Body = "Usuario ou senha inv�lidos."
        Body2 = "Tente novamente."

    Case AlertMessage.Alert_AccountIsNotActivated
        Header = "Usuario Desativado"
        Body = "O usu�rio n�o est� ativado."
        Body2 = "Ative sua conta e tente novamente."

    Case AlertMessage.Alert_CharacterIsDeleted
        Header = "Personagem Deletado"
        Body = "O personagem foi deletado."
        Body2 = "Fa�a o login para continuar jogando."

    Case AlertMessage.Alert_CharacterCreation
        Header = "Cria��o de Personagens"
        Body = "A cria��o de personagens esta desativada."
        Body2 = "Entre em contato com o Administrador."

    Case AlertMessage.Alert_CharacterDelete
        Header = "Exclus�o de Personagens"
        Body = "A exclus�o de personagens est� desativada."
        Body2 = "Entre em contato com o Administrador."

    Case AlertMessage.Alert_InvalidLevelDelete
        Header = "Exclus�o de Personagens"
        Body = "Este personagem n�o pode ser mais excluido."
        Body2 = "Entre em contato com o Administrador."

    Case AlertMessage.Alert_DuplicatedLogin
        Header = "Login Duplicado"
        Body = "Este usu�rio j� est� conectado."
        Body2 = "Entre em contato com o Administrador."

    Case AlertMessage.Alert_TryingToLogin
        Header = "Login Duplicado"
        Body = "Algu�m est� tentando acessar est� conta."
        Body2 = "Mude as suas op��es de seguran�a."

    Case AlertMessage.Alert_InvalidPacket
        Header = "Pacote Inv�lido"
        Body = "Houve um bloqueio nos dados transmitidos."
        Body2 = "Entre em contato com o Administrador."

    Case AlertMessage.Alert_InvalidRecipientName
        Header = "Nome Inv�lido"
        Body = "O nome do usu�rio � inv�lido."
        Body2 = "Verifique a ortografia."

    Case AlertMessage.Alert_InvalidItem
        Header = "Item Inv�lido"
        Body = "O item selecionado � inv�lido."
        Body2 = "Entre em contato com o Administrador."

    Case AlertMessage.Alert_NotEnoughCash
        Header = "Saldo Insuficiente"
        Body = "O saldo � insuficiente para realizar a compra."
        Body2 = "Fa�a uma recarga."

    Case AlertMessage.Alert_SuccessPurchase
        Header = "Sucesso na Opera��o"
        Body = "O processo de compra foi realizado com sucesso."
        Body2 = "Ser� enviado pelo correio para o destinat�rio."

    Case AlertMessage.Alert_NotEnoughCurrency
        Header = "Ouro insuficiente"
        Body = "N�o h� ouro suficiente para a opera��o."
        Body2 = "Entre em contato com o Administrador."

    End Select

    ' set the dialogue up!
    ShowDialogue Header, Body, Body2, DialogueTypeAlert
    
End Sub

Public Sub ShowDialogue(ByVal Header As String, ByVal Body As String, ByVal Body2 As String, ByVal Index As Long, Optional ByVal Style As DialogueStyle = 1, Optional ByVal Data1 As Long = 0)
    Dim WindowIndex As Long

    ' exit out if we've already got a dialogue open
    If Dialogue.Index > 0 Then Exit Sub

    WindowIndex = GetWindowIndex("winDialogue")

    ' set buttons
    With Windows(WindowIndex)
        If Style = DialogueStyleYesNo Then
            .Controls(GetControlIndex("winDialogue", "btnYes")).Visible = True
            .Controls(GetControlIndex("winDialogue", "btnNo")).Visible = True
            .Controls(GetControlIndex("winDialogue", "btnOkay")).Visible = False
            .Controls(GetControlIndex("winDialogue", "txtInput")).Visible = False
            .Controls(GetControlIndex("winDialogue", "lblBody_2")).Visible = True
        ElseIf Style = DialogueStyleOkay Then
            .Controls(GetControlIndex("winDialogue", "btnYes")).Visible = False
            .Controls(GetControlIndex("winDialogue", "btnNo")).Visible = False
            .Controls(GetControlIndex("winDialogue", "btnOkay")).Visible = True
            .Controls(GetControlIndex("winDialogue", "txtInput")).Visible = False
            .Controls(GetControlIndex("winDialogue", "lblBody_2")).Visible = True
        ElseIf Style = DialogueStyleInput Then
            .Controls(GetControlIndex("winDialogue", "btnYes")).Visible = False
            .Controls(GetControlIndex("winDialogue", "btnNo")).Visible = False
            .Controls(GetControlIndex("winDialogue", "btnOkay")).Visible = True
            .Controls(GetControlIndex("winDialogue", "txtInput")).Visible = True
            .Controls(GetControlIndex("winDialogue", "lblBody_2")).Visible = False
        End If

        ' set labels
        .Controls(GetControlIndex("winDialogue", "lblHeader")).Text = Header
        .Controls(GetControlIndex("winDialogue", "lblBody_1")).Text = Body
        .Controls(GetControlIndex("winDialogue", "lblBody_2")).Text = Body2
        .Controls(GetControlIndex("winDialogue", "txtInput")).Text = vbNullString
    End With

    Windows(WindowIndex).activeControl = GetControlIndex("winDialogue", "txtInput")

    ' set it all up
    Dialogue.Index = Index
    Dialogue.DataLong = Data1
    Dialogue.Style = Style

    ' make the windows visible
    ShowWindow GetWindowIndex("winBlank"), True
    ShowWindow GetWindowIndex("winDialogue"), True
End Sub

Public Sub DialogueHandler(ByVal Index As Long)
    Dim Value As Long, DiaInput As String

    Dim Buffer As New clsBuffer
    Set Buffer = New clsBuffer

    DiaInput = Trim$(Windows(GetWindowIndex("winDialogue")).Controls(GetControlIndex("winDialogue", "txtInput")).Text)

    ' find out which button
    If Index = DialogueButtonOkay Then

        Select Case Dialogue.Index

        Case DialogueTypeTradeAmount
            Value = Val(DiaInput)

            If Value > 0 Then
                Call SendTradeItem(Dialogue.DataLong, Value)
            End If

        Case DialogueTypeDepositItem
            Value = Val(DiaInput)

            If Value > 0 Then
                Call SendDepositItem(Dialogue.DataLong, Value)
            End If

        Case DialogueTypeWithdrawItem
            Value = Val(DiaInput)

            If Value > 0 Then
                Call SendWithdrawItem(Dialogue.DataLong, Value)
            End If

        Case DialogueTypeTradeGoldAmount
            Value = Val(DiaInput)

            If Value > 0 Then
                Call SendSelectTradeCurrency(Value)
            End If

        Case DialogueTypeSellAmount
            Value = Val(DiaInput)

            If Value > 0 Then
                Call SendSellItem(Dialogue.DataLong, Value)
            End If

        Case DialogueTypeAddMailCurrency
            Call AddSendMailCurrencyValue(Val(DiaInput))

        Case DialogueTypeAddMailAmount
            Call AddSendMailItemValue(Val(DiaInput))

        End Select

    ElseIf Index = DialogueButtonYes Then
        Select Case Dialogue.Index

        Case DialogueTypeDestroyItem
            Call SendDestroyItem(Dialogue.DataLong)

        Case DialogueTypeTrade
            Call SendAcceptTradeRequest

        Case DialogueTypeParty
            Call SendAcceptParty

        Case DialogueTypeLootItem
            ' send the packet
            'Player(MyIndex).MapGetTimer = GetTickCount
            'Buffer.WriteLong CMapGetItem
            'SendData Buffer.ToArray()

        Case DialogueTypeDeleteChar
            ' send the deletion
            Call SendDeleteChar(Dialogue.DataLong)

        Case DialogueTypeDeleteMail
            Call SendDeleteMail

        End Select

    ElseIf Index = DialogueButtonNo Then
        Select Case Dialogue.Index

        Case DialogueTypeTrade
            Call SendDeclineTradeRequest

        Case DialogueTypeParty
            Call SendDeclineParty

        End Select
    End If

    CloseDialogue
End Sub

