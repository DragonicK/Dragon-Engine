Attribute VB_Name = "Service_Implementation"
Option Explicit

' Quantidade de serviços.
Public ServiceCount As Long

Public ServerService As ServiceRec

Public Type ServiceRec
    Id As Long
    Name As String
    EndTime As String
    
    Character As Long
    Party As Long
    Guild As Long
    Skill As Long
    Craft As Long
    Quest As Long
    Pet As Long
    Talent As Long
    GoldChance As Long
    GoldIncrease As Long
    
    ItemDrop(0 To RarityType.RarityType_Count - 1) As Long
End Type

Private Const MaximumServices As Long = 25

Private Services(1 To MaximumServices) As ServiceRec
Private BonusTotal As ServiceRec

Public Function GetServiceCount() As Long
    Dim i As Long
    Dim Count As Long

    For i = 1 To MaximumServices
        If Services(i).Id > 0 Then
            Count = Count + 1
        End If
    Next

    GetServiceCount = Count
End Function

Public Function GetServiceIndexFromId(ByVal Id As Long)
    Dim i As Long

    For i = 1 To MaximumServices
        If Services(i).Id = Id Then
            GetServiceIndexFromId = i
            Exit Function
        End If
    Next

End Function

Public Function GetEmptyServiceIndex() As Long
    Dim i As Long

    For i = 1 To MaximumServices
        If LenB(Services(i).Name) = 0 And Services(i).Id = 0 Then
            GetEmptyServiceIndex = i
            Exit Function
        End If
    Next
End Function

Public Function GetService(ByVal Index As Long) As ServiceRec
    GetService = Services(Index)
End Function

Public Function GetTotalService() As ServiceRec
    GetTotalService = BonusTotal
End Function

Public Sub ClearServices()
    Dim i As Long

    For i = 1 To MaximumServices
        Call ClearService(i)
    Next
End Sub

Public Sub ClearService(ByVal Index As Long)
    Call ZeroMemory(ByVal VarPtr(Services(Index)), LenB(Services(Index)))
    Services(Index).Name = vbNullString
    Services(Index).EndTime = vbNullString
End Sub

Public Sub AllocateServiceRates()
    Dim i As Long, n As Long

    BonusTotal.Name = "BONUS TOTAL"
    BonusTotal.Character = 0
    BonusTotal.Party = 0
    BonusTotal.Guild = 0
    BonusTotal.Skill = 0
    BonusTotal.Talent = 0
    BonusTotal.Craft = 0
    BonusTotal.Quest = 0
    BonusTotal.Pet = 0
    BonusTotal.GoldChance = 0
    BonusTotal.GoldIncrease = 0

    For n = 0 To RarityType.RarityType_Count - 1
        BonusTotal.ItemDrop(n) = 0
    Next

    For i = 1 To MaximumServices
        If LenB(Services(i).Name) > 0 Then
            BonusTotal.Character = BonusTotal.Character + GetServiceCharacterExpRate(i)
            BonusTotal.Party = BonusTotal.Party + GetServicePartyExpRate(i)
            BonusTotal.Guild = BonusTotal.Guild + GetServiceGuildExpRate(i)
            BonusTotal.Skill = BonusTotal.Skill + GetServiceSkillExpRate(i)
            BonusTotal.Talent = BonusTotal.Talent + GetServiceTalentExpRate(i)
            BonusTotal.Craft = BonusTotal.Craft + GetServiceCraftExpRate(i)
            BonusTotal.Quest = BonusTotal.Quest + GetServiceQuestExpRate(i)
            BonusTotal.Pet = BonusTotal.Pet + GetServicePetExpRate(i)
            BonusTotal.GoldChance = BonusTotal.GoldChance + GetServiceGoldChanceRate(i)
            BonusTotal.GoldIncrease = BonusTotal.GoldIncrease + GetServiceGoldIncreaseRate(i)

            For n = 0 To RarityType.RarityType_Count - 1
                BonusTotal.ItemDrop(n) = BonusTotal.ItemDrop(n) + GetServiceItemDropRate(i, n)
            Next
        End If
    Next
End Sub

Public Function GetServiceId(ByVal Index As Long) As Long
    GetServiceId = Services(Index).Id
End Function
Public Sub SetServiceId(ByVal Index As Long, ByVal Id As Long)
    Services(Index).Id = Id
End Sub

Public Function GetServiceName(ByVal Index As Long) As String
    GetServiceName = Services(Index).Name
End Function
Public Sub SetServiceName(ByVal Index As Long, ByVal Name As String)
    Services(Index).Name = Name
End Sub

Public Function GetServiceEndTime(ByVal Index As Long) As String
    GetServiceEndTime = Services(Index).EndTime
End Function
Public Sub SetServiceEndTime(ByVal Index As Long, ByVal EndTime As String)
    Services(Index).EndTime = EndTime
End Sub

Public Function GetServiceItemDropRate(ByVal Index As Long, ByVal Rarity As RarityType) As Long
    GetServiceItemDropRate = Services(Index).ItemDrop(Rarity)
End Function
Public Sub SetServiceItemDropRate(ByVal Index As Long, ByVal Rarity As RarityType, ByVal Value As Long)
    Services(Index).ItemDrop(Rarity) = Value
End Sub

Public Function GetServiceCharacterExpRate(ByVal Index As Long) As Long
    GetServiceCharacterExpRate = Services(Index).Character
End Function
Public Sub SetServiceCharacterExpRate(ByVal Index As Long, ByVal Value As Long)
    Services(Index).Character = Value
End Sub

Public Function GetServicePartyExpRate(ByVal Index As Long) As Long
    GetServicePartyExpRate = Services(Index).Party
End Function
Public Sub SetServicePartyExpRate(ByVal Index As Long, ByVal Value As Long)
    Services(Index).Party = Value
End Sub

Public Function GetServiceGuildExpRate(ByVal Index As Long) As Long
    GetServiceGuildExpRate = Services(Index).Guild
End Function
Public Sub SetServiceGuildExpRate(ByVal Index As Long, ByVal Value As Long)
    Services(Index).Guild = Value
End Sub

Public Function GetServiceSkillExpRate(ByVal Index As Long) As Long
    GetServiceSkillExpRate = Services(Index).Skill
End Function
Public Sub SetServiceSkillExpRate(ByVal Index As Long, ByVal Value As Long)
    Services(Index).Skill = Value
End Sub

Public Function GetServiceTalentExpRate(ByVal Index As Long) As Long
    GetServiceTalentExpRate = Services(Index).Talent
End Function
Public Sub SetServiceTalentExpRate(ByVal Index As Long, ByVal Value As Long)
    Services(Index).Talent = Value
End Sub

Public Function GetServiceCraftExpRate(ByVal Index As Long) As Long
    GetServiceCraftExpRate = Services(Index).Craft
End Function
Public Sub SetServiceCraftExpRate(ByVal Index As Long, ByVal Value As Long)
    Services(Index).Craft = Value
End Sub

Public Function GetServiceQuestExpRate(ByVal Index As Long) As Long
    GetServiceQuestExpRate = Services(Index).Quest
End Function
Public Sub SetServiceQuestExpRate(ByVal Index As Long, ByVal Value As Long)
    Services(Index).Quest = Value
End Sub

Public Function GetServicePetExpRate(ByVal Index As Long) As Long
    GetServicePetExpRate = Services(Index).Pet
End Function
Public Sub SetServicePetExpRate(ByVal Index As Long, ByVal Value As Long)
    Services(Index).Pet = Value
End Sub

Public Function GetServiceGoldChanceRate(ByVal Index As Long) As Long
    GetServiceGoldChanceRate = Services(Index).GoldChance
End Function
Public Sub SetServiceGoldChanceRate(ByVal Index As Long, ByVal Value As Long)
    Services(Index).GoldChance = Value
End Sub

Public Function GetServiceGoldIncreaseRate(ByVal Index As Long) As Long
    GetServiceGoldIncreaseRate = Services(Index).GoldIncrease
End Function
Public Sub SetServiceGoldIncreaseRate(ByVal Index As Long, ByVal Value As Long)
    Services(Index).GoldIncrease = Value
End Sub
