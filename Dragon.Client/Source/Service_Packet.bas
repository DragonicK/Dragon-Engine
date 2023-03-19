Attribute VB_Name = "Service_Packet"
Option Explicit

Private Const ServerId As Long = 9999999
Private Const ServerText As String = "SERVIDOR"

Public Sub HandleServerRates(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, i As Long, n As Long
    Dim Count As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    ServerService.Id = 0
    ServerService.Name = ServerText
    ServerService.EndTime = vbNullString

    ServerService.Character = Buffer.ReadLong
    ServerService.Talent = Buffer.ReadLong
    ServerService.Party = Buffer.ReadLong
    ServerService.Guild = Buffer.ReadLong
    ServerService.Skill = Buffer.ReadLong
    ServerService.Craft = Buffer.ReadLong
    ServerService.Quest = Buffer.ReadLong
    ServerService.Pet = Buffer.ReadLong
    ServerService.GoldChance = Buffer.ReadLong
    ServerService.GoldIncrease = Buffer.ReadLong

    Count = Buffer.ReadLong

    For n = 0 To Count - 1
        ServerService.ItemDrop(n) = Buffer.ReadLong
    Next

    Call AllocateServiceRates
    Call UpdateServiceWindow

End Sub

Public Sub HandlePremiumService(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, i As Long, n As Long
    Dim Count As Long, Id As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data
     
    Id = Buffer.ReadLong

    Index = GetServiceIndexFromId(Id)

    If Index = 0 Then
        Index = GetEmptyServiceIndex()
    End If

    If Index > 0 Then
        Call SetServiceId(Index, Id)
        Call SetServiceName(Index, Buffer.ReadString)
        Call SetServiceEndTime(Index, Buffer.ReadString)

        Call SetServiceTalentExpRate(Index, 0)

        Call SetServiceCharacterExpRate(Index, Buffer.ReadLong)
        Call SetServicePartyExpRate(Index, Buffer.ReadLong)
        Call SetServiceGuildExpRate(Index, Buffer.ReadLong)
        Call SetServiceSkillExpRate(Index, Buffer.ReadLong)
        Call SetServiceCraftExpRate(Index, Buffer.ReadLong)
        Call SetServiceQuestExpRate(Index, Buffer.ReadLong)
        Call SetServicePetExpRate(Index, Buffer.ReadLong)
        Call SetServiceGoldChanceRate(Index, Buffer.ReadLong)
        Call SetServiceGoldIncreaseRate(Index, Buffer.ReadLong)

        Count = Buffer.ReadLong

        For n = 0 To Count - 1
            Call SetServiceItemDropRate(Index, n, Buffer.ReadLong)
        Next
    End If
    
    ServiceCount = GetServiceCount

    Call AllocateServiceRates
    Call UpdateServiceWindow
End Sub

