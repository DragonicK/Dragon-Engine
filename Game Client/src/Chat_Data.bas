Attribute VB_Name = "Chat_Data"
Public Enum SystemMessage
    YouDoNotHaveRequiredLevel
    SuccessToRevealItemAttribute
    FailedToRevealItemAttribute
    CannotRevealItemAttribute
    InventoryFull
    PlayerIsNotOnline
    NeedRevealItemAttribute
    WarehouseFull
    InvalidProfession
    RecipeIsRegistered
    RecipeListIsFull
    YouDoNotHaveEnoughMaterial
    ItemCreated
    TheTargetIsInAnotherTrade
    YouAreWaitingForConfirmation
    YouAreInTrade
    PlayerIsNowDisconnected
    TradeAcceptTimeOut
    ItemCannotBeTraded
    DeclinedTradeRequest
    YouAcceptedTrade
    YouConfirmedTrade
    DeclinedTrade
    YouCantChangeItemWhenConfirmed
    ThePlayerInventoryIsFull
    TheCurrencyCannotBeSubtracted
    TheCurrencyCannotBeAdded
    TradeConcluded
    ThisBoxIsEmpty
    YouAlreadyLearnedSkill
    YouLearnedSkill
    YouObtainedItem
    OnlyClassCodeCanUseItem
    InvalidTarget
    InvalidRange
    InsuficientMana
    YouAreNotLeader
    PlayerIsAlreadyInParty
    PlayerIsAlreadyInvited
    PlayerFailedToAcceptParty
    YouFailedToAcceptParty
    PartyIsFull
    YouDeclinedPartyRequest
    PlayerDeclinedPartyRequest
    PartyDisbanded
    YouJoinedParty
    PartyCreated
    YouLeftParty
    PlayerLeftParty
    PlayerPartyReconnected
    PlayerKickedFromParty
    YouKickedFromParty
    ItemCannotBeUpgraded
    UpgradeFailed
    UpgradeFailedButReduced
    UpgradeSuccess
    UpgradeBreak
    UpgradeMaximumLevel
    YouJustReceivedMail
    InsuficientCurrency
    ItemCannotBeSold
    ItemHasBeenSold
End Enum

Public Function GetSystemMessage(ByVal Header As SystemMessage, ByVal ParamCount As Long, ByRef Parameters() As String) As String
    Dim Id As Long
    Dim Value As Long
    Dim Name As String

    Select Case Header

    Case SystemMessage.YouDoNotHaveRequiredLevel
        GetSystemMessage = "Voc� n�o tem level suficiente."

    Case SystemMessage.SuccessToRevealItemAttribute
        GetSystemMessage = "Os atributos do item foram revelados."

    Case SystemMessage.FailedToRevealItemAttribute
        GetSystemMessage = "Houve uma falha ao revelar o atributo do item."

    Case SystemMessage.CannotRevealItemAttribute
        GetSystemMessage = "Os atributos do item n�o podem ser revelados."

    Case SystemMessage.InventoryFull
        GetSystemMessage = "O invent�rio est� cheio."

    Case SystemMessage.PlayerIsNotOnline
        GetSystemMessage = "O jogador n�o est� online."

    Case SystemMessage.NeedRevealItemAttribute
        GetSystemMessage = "Os atribuitos do item precisam ser revelados antes."

    Case SystemMessage.WarehouseFull
        GetSystemMessage = "O armaz�m est� cheio."

    Case SystemMessage.InvalidProfession
        GetSystemMessage = "Esta receita n�o pode ser usada com sua profiss�o atual."

    Case SystemMessage.RecipeIsRegistered
        GetSystemMessage = "A receita est� registrada."

    Case SystemMessage.RecipeListIsFull
        GetSystemMessage = "Voc� n�o pode aprender mais receitas."

    Case SystemMessage.YouDoNotHaveEnoughMaterial
        GetSystemMessage = "Voc� n�o tem materiais suficientes."

    Case SystemMessage.ItemCreated
        GetSystemMessage = "O item foi criado."

    Case SystemMessage.TheTargetIsInAnotherTrade
        GetSystemMessage = "O jogador est� em outra negocia��o."

    Case SystemMessage.YouAreWaitingForConfirmation
        GetSystemMessage = "Voc� est� esperando a confirma��o da negocia��o."

    Case SystemMessage.YouAreInTrade
        GetSystemMessage = "Voc� j� est� em uma negocia��o."

    Case SystemMessage.PlayerIsNowDisconnected
        GetSystemMessage = "O jogador foi desconectado."

    Case SystemMessage.TradeAcceptTimeOut
        GetSystemMessage = "Um dos jogadores n�o aceitou o pedido de negocia��o."

    Case SystemMessage.ItemCannotBeTraded
        GetSystemMessage = "Este item n�o pode ser negociado."

    Case SystemMessage.DeclinedTradeRequest
        GetSystemMessage = "O jogador recusou o pedido de negocia��o."

    Case SystemMessage.YouAcceptedTrade
        GetSystemMessage = "Voc� aceitou a negocia��o."

    Case SystemMessage.YouConfirmedTrade
        GetSystemMessage = "Voc� confirmou a negocia��o."

    Case SystemMessage.DeclinedTrade
        GetSystemMessage = "A negocia��o foi recusada."

    Case SystemMessage.YouCantChangeItemWhenConfirmed
        GetSystemMessage = "Voc� n�o pode adicionar ou remover itens quando confirmar a negocia��o."

    Case SystemMessage.ThePlayerInventoryIsFull
        If ParamCount > 0 Then
            GetSystemMessage = Parameters(1) & " est� com o invent�rio cheio."
        End If

    Case SystemMessage.TheCurrencyCannotBeSubtracted
        GetSystemMessage = "A moeda n�o pode ser retirada do jogador."

    Case SystemMessage.TheCurrencyCannotBeAdded
        GetSystemMessage = "A moeda n�o pode ser adicionada ao jogador."

    Case SystemMessage.TradeConcluded
        GetSystemMessage = "A negocia��o foi conclu�da."

    Case SystemMessage.ThisBoxIsEmpty
        GetSystemMessage = "Essa caixa est� vazia."

    Case SystemMessage.YouAlreadyLearnedSkill
        GetSystemMessage = "Voc� j� aprendeu essa habilidade."

    Case SystemMessage.YouLearnedSkill
        If ParamCount > 0 Then
            Id = Val(Parameters(1))

            If Id >= 1 And Id <= MaximumSkills Then
                GetSystemMessage = "Voc� aprendeu " & Skill(Id).Name & "."
            Else
                GetSystemMessage = "Voc� aprendeu uma habilidade."
            End If
        End If
    Case SystemMessage.YouObtainedItem
        If ParamCount >= 2 Then
            Id = Val(Parameters(1))
            Value = Val(Parameters(2))

            If Id >= 1 And Id <= MaximumItems Then
                If Value > 1 Then
                    GetSystemMessage = "Voc� obteve " & Value & " " & Item(Id).Name & "."
                Else
                    GetSystemMessage = "Voc� obteve " & Item(Id).Name & "."
                End If
            Else
                GetSystemMessage = "Voc� obteve " & Value & " itens."
            End If
        End If

    Case SystemMessage.OnlyClassCodeCanUseItem
        If ParamCount > 0 Then
            Id = Val(Parameters(1))

            If Id >= 1 And Id <= MaximumClasses Then
                GetSystemMessage = "Somente " & GetClassName(Id) & " (s) podem usar o item."
            End If
        Else
            GetSystemMessage = "Voc� n�o tem o requerimento de classe para usar o item."
        End If

    Case SystemMessage.InvalidTarget
        GetSystemMessage = "Alvo inv�lido."

    Case SystemMessage.InvalidRange
        GetSystemMessage = "Sem alcance."

    Case SystemMessage.InsuficientMana
        GetSystemMessage = "Energia insuficiente."

        If ParamCount > 0 Then
            Id = Val(Parameters(1))

            If Id >= 1 And Id <= MaximumSkills Then
                For i = 1 To 1
                    If Skill(i).CostType = SkillCostType_HP Then

                        If Skill(i).CostType = SkillCostType_HP Then
                            GetSystemMessage = "Vida insuficiente."
                        ElseIf Skill(i).CostType = SkillCostType_MP Then
                            GetSystemMessage = "Mana insuficiente."
                        ElseIf Skill(i).CostType = SkillCostType_Special Then
                            GetSystemMessage = "Stamina insuficiente."
                        End If

                        Exit For
                    End If
                Next
            End If
        End If

    Case SystemMessage.YouAreNotLeader
        GetSystemMessage = "Voc� n�o � o lider do grupo."

    Case SystemMessage.PlayerIsAlreadyInParty
        GetSystemMessage = "O jogador j� est� em um grupo."

    Case SystemMessage.PlayerIsAlreadyInvited
        GetSystemMessage = "O jogador j� foi convidado para um grupo."

    Case SystemMessage.PlayerFailedToAcceptParty
        GetSystemMessage = "O jogador n�o aceitou o pedido para o grupo."

    Case SystemMessage.YouFailedToAcceptParty
        GetSystemMessage = "Voc� n�o aceitou o pedido para o grupo."

    Case SystemMessage.PartyIsFull
        GetSystemMessage = "O grupo est� cheio."

    Case SystemMessage.YouDeclinedPartyRequest
        GetSystemMessage = "Voc� recusou o pedido de grupo."

    Case SystemMessage.PlayerDeclinedPartyRequest
        If ParamCount > 0 Then
            Name = Parameters(1)

            GetSystemMessage = Name & " recusou o pedido de grupo."
        End If

    Case SystemMessage.PartyDisbanded
        GetSystemMessage = "O grupo foi dissolvido."

    Case SystemMessage.YouJoinedParty
        GetSystemMessage = "Voc� entrou no grupo."

    Case SystemMessage.PartyCreated
        GetSystemMessage = "O grupo foi criado."

    Case SystemMessage.YouLeftParty
        GetSystemMessage = "Voc� deixou o grupo."

    Case SystemMessage.PlayerLeftParty
        If ParamCount > 0 Then
            Name = Parameters(1)

            GetSystemMessage = Name & " saiu do grupo."
        End If

    Case SystemMessage.PlayerPartyReconnected
        If ParamCount > 0 Then
            Name = Parameters(1)

            GetSystemMessage = Name & " reconectou ao grupo."
        End If

    Case SystemMessage.PlayerKickedFromParty
        If ParamCount > 0 Then
            Name = Parameters(1)

            GetSystemMessage = Name & " foi retirado do grupo."
        End If

    Case SystemMessage.YouKickedFromParty
        GetSystemMessage = "Voc� foi retirado do grupo."

    Case SystemMessage.ItemCannotBeUpgraded
        GetSystemMessage = "Este item n�o pode ser aprimorado."

    Case SystemMessage.UpgradeFailed
        GetSystemMessage = "O aprimoramento falhou."

    Case SystemMessage.UpgradeFailedButReduced
        GetSystemMessage = "O aprimoramento falhou e o n�vel do item foi reduzido."

    Case SystemMessage.UpgradeSuccess
        GetSystemMessage = "O item foi aprimorado."

    Case SystemMessage.UpgradeBreak
        GetSystemMessage = "O aprimoramento falhou e o item foi destru�do."

    Case SystemMessage.UpgradeMaximumLevel
        GetSystemMessage = "Este item esta no level m�ximo e n�o pode mais ser aprimorado."

    Case SystemMessage.YouJustReceivedMail
        GetSystemMessage = "Voc� acabou de receber uma correspond�ncia."

    Case SystemMessage.InsuficientCurrency
        GetSystemMessage = "Moeda insuficiente."

    Case SystemMessage.ItemCannotBeSold
        GetSystemMessage = "O item n�o pode ser vendido."

    Case SystemMessage.ItemHasBeenSold
        If ParamCount >= 2 Then
            Id = Val(Parameters(1))
            Value = Val(Parameters(2))

            If Id >= 1 And Id <= MaximumItems Then
                If Value > 1 Then
                    GetSystemMessage = "Voc� vendeu " & Value & " " & Item(Id).Name & "."
                Else
                    GetSystemMessage = "Voc� vendeu " & Item(Id).Name & "."
                End If
            Else
                GetSystemMessage = "Voc� vendeu " & Value & " itens."
            End If
        End If
    End Select

End Function


