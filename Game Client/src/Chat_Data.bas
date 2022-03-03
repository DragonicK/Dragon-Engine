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
        GetSystemMessage = "Você não tem level suficiente."

    Case SystemMessage.SuccessToRevealItemAttribute
        GetSystemMessage = "Os atributos do item foram revelados."

    Case SystemMessage.FailedToRevealItemAttribute
        GetSystemMessage = "Houve uma falha ao revelar o atributo do item."

    Case SystemMessage.CannotRevealItemAttribute
        GetSystemMessage = "Os atributos do item não podem ser revelados."

    Case SystemMessage.InventoryFull
        GetSystemMessage = "O inventário está cheio."

    Case SystemMessage.PlayerIsNotOnline
        GetSystemMessage = "O jogador não está online."

    Case SystemMessage.NeedRevealItemAttribute
        GetSystemMessage = "Os atribuitos do item precisam ser revelados antes."

    Case SystemMessage.WarehouseFull
        GetSystemMessage = "O armazém está cheio."

    Case SystemMessage.InvalidProfession
        GetSystemMessage = "Esta receita não pode ser usada com sua profissão atual."

    Case SystemMessage.RecipeIsRegistered
        GetSystemMessage = "A receita está registrada."

    Case SystemMessage.RecipeListIsFull
        GetSystemMessage = "Você não pode aprender mais receitas."

    Case SystemMessage.YouDoNotHaveEnoughMaterial
        GetSystemMessage = "Você não tem materiais suficientes."

    Case SystemMessage.ItemCreated
        GetSystemMessage = "O item foi criado."

    Case SystemMessage.TheTargetIsInAnotherTrade
        GetSystemMessage = "O jogador está em outra negociação."

    Case SystemMessage.YouAreWaitingForConfirmation
        GetSystemMessage = "Você está esperando a confirmação da negociação."

    Case SystemMessage.YouAreInTrade
        GetSystemMessage = "Você já está em uma negociação."

    Case SystemMessage.PlayerIsNowDisconnected
        GetSystemMessage = "O jogador foi desconectado."

    Case SystemMessage.TradeAcceptTimeOut
        GetSystemMessage = "Um dos jogadores não aceitou o pedido de negociação."

    Case SystemMessage.ItemCannotBeTraded
        GetSystemMessage = "Este item não pode ser negociado."

    Case SystemMessage.DeclinedTradeRequest
        GetSystemMessage = "O jogador recusou o pedido de negociação."

    Case SystemMessage.YouAcceptedTrade
        GetSystemMessage = "Você aceitou a negociação."

    Case SystemMessage.YouConfirmedTrade
        GetSystemMessage = "Você confirmou a negociação."

    Case SystemMessage.DeclinedTrade
        GetSystemMessage = "A negociação foi recusada."

    Case SystemMessage.YouCantChangeItemWhenConfirmed
        GetSystemMessage = "Você não pode adicionar ou remover itens quando confirmar a negociação."

    Case SystemMessage.ThePlayerInventoryIsFull
        If ParamCount > 0 Then
            GetSystemMessage = Parameters(1) & " está com o inventário cheio."
        End If

    Case SystemMessage.TheCurrencyCannotBeSubtracted
        GetSystemMessage = "A moeda não pode ser retirada do jogador."

    Case SystemMessage.TheCurrencyCannotBeAdded
        GetSystemMessage = "A moeda não pode ser adicionada ao jogador."

    Case SystemMessage.TradeConcluded
        GetSystemMessage = "A negociação foi concluída."

    Case SystemMessage.ThisBoxIsEmpty
        GetSystemMessage = "Essa caixa está vazia."

    Case SystemMessage.YouAlreadyLearnedSkill
        GetSystemMessage = "Você já aprendeu essa habilidade."

    Case SystemMessage.YouLearnedSkill
        If ParamCount > 0 Then
            Id = Val(Parameters(1))

            If Id >= 1 And Id <= MaximumSkills Then
                GetSystemMessage = "Você aprendeu " & Skill(Id).Name & "."
            Else
                GetSystemMessage = "Você aprendeu uma habilidade."
            End If
        End If
    Case SystemMessage.YouObtainedItem
        If ParamCount >= 2 Then
            Id = Val(Parameters(1))
            Value = Val(Parameters(2))

            If Id >= 1 And Id <= MaximumItems Then
                If Value > 1 Then
                    GetSystemMessage = "Você obteve " & Value & " " & Item(Id).Name & "."
                Else
                    GetSystemMessage = "Você obteve " & Item(Id).Name & "."
                End If
            Else
                GetSystemMessage = "Você obteve " & Value & " itens."
            End If
        End If

    Case SystemMessage.OnlyClassCodeCanUseItem
        If ParamCount > 0 Then
            Id = Val(Parameters(1))

            If Id >= 1 And Id <= MaximumClasses Then
                GetSystemMessage = "Somente " & GetClassName(Id) & " (s) podem usar o item."
            End If
        Else
            GetSystemMessage = "Você não tem o requerimento de classe para usar o item."
        End If

    Case SystemMessage.InvalidTarget
        GetSystemMessage = "Alvo inválido."

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
        GetSystemMessage = "Você não é o lider do grupo."

    Case SystemMessage.PlayerIsAlreadyInParty
        GetSystemMessage = "O jogador já está em um grupo."

    Case SystemMessage.PlayerIsAlreadyInvited
        GetSystemMessage = "O jogador já foi convidado para um grupo."

    Case SystemMessage.PlayerFailedToAcceptParty
        GetSystemMessage = "O jogador não aceitou o pedido para o grupo."

    Case SystemMessage.YouFailedToAcceptParty
        GetSystemMessage = "Você não aceitou o pedido para o grupo."

    Case SystemMessage.PartyIsFull
        GetSystemMessage = "O grupo está cheio."

    Case SystemMessage.YouDeclinedPartyRequest
        GetSystemMessage = "Você recusou o pedido de grupo."

    Case SystemMessage.PlayerDeclinedPartyRequest
        If ParamCount > 0 Then
            Name = Parameters(1)

            GetSystemMessage = Name & " recusou o pedido de grupo."
        End If

    Case SystemMessage.PartyDisbanded
        GetSystemMessage = "O grupo foi dissolvido."

    Case SystemMessage.YouJoinedParty
        GetSystemMessage = "Você entrou no grupo."

    Case SystemMessage.PartyCreated
        GetSystemMessage = "O grupo foi criado."

    Case SystemMessage.YouLeftParty
        GetSystemMessage = "Você deixou o grupo."

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
        GetSystemMessage = "Você foi retirado do grupo."

    Case SystemMessage.ItemCannotBeUpgraded
        GetSystemMessage = "Este item não pode ser aprimorado."

    Case SystemMessage.UpgradeFailed
        GetSystemMessage = "O aprimoramento falhou."

    Case SystemMessage.UpgradeFailedButReduced
        GetSystemMessage = "O aprimoramento falhou e o nível do item foi reduzido."

    Case SystemMessage.UpgradeSuccess
        GetSystemMessage = "O item foi aprimorado."

    Case SystemMessage.UpgradeBreak
        GetSystemMessage = "O aprimoramento falhou e o item foi destruído."

    Case SystemMessage.UpgradeMaximumLevel
        GetSystemMessage = "Este item esta no level máximo e não pode mais ser aprimorado."

    Case SystemMessage.YouJustReceivedMail
        GetSystemMessage = "Você acabou de receber uma correspondência."

    Case SystemMessage.InsuficientCurrency
        GetSystemMessage = "Moeda insuficiente."

    Case SystemMessage.ItemCannotBeSold
        GetSystemMessage = "O item não pode ser vendido."

    Case SystemMessage.ItemHasBeenSold
        If ParamCount >= 2 Then
            Id = Val(Parameters(1))
            Value = Val(Parameters(2))

            If Id >= 1 And Id <= MaximumItems Then
                If Value > 1 Then
                    GetSystemMessage = "Você vendeu " & Value & " " & Item(Id).Name & "."
                Else
                    GetSystemMessage = "Você vendeu " & Item(Id).Name & "."
                End If
            Else
                GetSystemMessage = "Você vendeu " & Value & " itens."
            End If
        End If
    End Select

End Function


