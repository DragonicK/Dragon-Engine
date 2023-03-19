Attribute VB_Name = "Character_Implementation"
Option Explicit

Public Type EquipmentPositionRec
    X As Long
    Y As Long
End Type

' Equipment used by Players
Public Enum PlayerEquipments
    EquipWeapon = 1
    EquipShield
    EquipHelmet
    EquipArmor
    EquipShoulder
    EquipBelt
    EquipGloves
    EquipPants
    EquipBoot
    EquipNecklace
    EquipEarring_1
    EquipEarring_2
    EquipRing_1
    EquipRing_2
    EquipBracelet_1
    EquipBracelet_2
    EquipCostume
    EquipMount
    ' Make sure Equipment_Count is below everything else
    PlayerEquipment_Count
End Enum

Public MyAttributes As PlayerAttributesRec
Private Equipment(1 To PlayerEquipments.PlayerEquipment_Count - 1) As PlayerEquipmentRec

Public Type PlayerEquipmentRec
    Num As Long
    Bound As InventoryBoundType
    Level As Long
    AttributeId As Long
    UpgradeId As Long
End Type

Private Type PlayerAttributesRec
    Stat(1 To Stats.Stat_Count - 1) As Long
    StatPoints As Long
    TalentPoints As Long
    Experience As Long

    Attack As Long
    Defense As Long
    Accuracy As Long
    Evasion As Long
    Parry As Long
    Block As Long

    MagicAttack As Long
    MagicDefense As Long
    MagicAccuracy As Long
    MagicResist As Long
    Concentration As Long

    CritRate As Long
    CritDamage As Long
    ResistCritRate As Long
    ResistCritDamage As Long
    
    DamageSuppression As Long
    HealingPower As Long
    FinalDamage As Long
    Amplification As Long
    Enmity As Long
    AttackSpeed As Long
    CastSpeed As Long
    
    PveAttack As Long
    PveDefense As Long
    PvpAttack As Long
    PvpDefense As Long

    SilenceResistance As Long
    BlindResistance As Long
    StunResistance As Long
    StumbleResistance As Long

    ElementAttack(1 To Elements.Element_Count - 1) As Long
    ElementDefense(1 To Elements.Element_Count - 1) As Long
End Type

Function GetPlayerName(ByVal Index As Long) As String
    GetPlayerName = Trim$(Player(Index).Name)
End Function
Sub SetPlayerName(ByVal Index As Long, ByVal Name As String)
    Player(Index).Name = Name
End Sub
Function GetPlayerClass(ByVal Index As Long) As Long
    GetPlayerClass = Player(Index).Class
End Function
Sub SetPlayerClass(ByVal Index As Long, ByVal ClassId As Long)
    Player(Index).Class = ClassId
End Sub
Function GetPlayerSprite(ByVal Index As Long) As Long
    GetPlayerSprite = Player(Index).Sprite
End Function
Sub SetPlayerSprite(ByVal Index As Long, ByVal Sprite As Long)
    Player(Index).Sprite = Sprite
End Sub
Function GetPlayerLevel(ByVal Index As Long) As Long
    GetPlayerLevel = Player(Index).Level
End Function
Sub SetPlayerLevel(ByVal Index As Long, ByVal Level As Long)
    Player(Index).Level = Level
End Sub
Function GetPlayerAccess(ByVal Index As Long) As Long
    GetPlayerAccess = Player(Index).Access
End Function
Sub SetPlayerAccess(ByVal Index As Long, ByVal Access As Long)
    Player(Index).Access = Access
End Sub
Function GetPlayerPK(ByVal Index As Long) As Long
    GetPlayerPK = Player(Index).PK
End Function
Sub SetPlayerPK(ByVal Index As Long, ByVal PK As Long)
    Player(Index).PK = PK
End Sub
Function GetPlayerVital(ByVal Index As Long, ByVal Vital As Vitals) As Long
    GetPlayerVital = Player(Index).Vital(Vital)
End Function
Sub SetPlayerVital(ByVal Index As Long, ByVal Vital As Vitals, ByVal Value As Long)
    Player(Index).Vital(Vital) = Value

    If GetPlayerVital(Index, Vital) > GetPlayerMaxVital(Index, Vital) Then
        Player(Index).Vital(Vital) = GetPlayerMaxVital(Index, Vital)
    End If
End Sub
Function GetPlayerMaxVital(ByVal Index As Long, ByVal Vital As Vitals) As Long
    GetPlayerMaxVital = Player(Index).MaxVital(Vital)
End Function
Sub SetPlayerMaxVital(ByVal Index As Long, ByVal Vital As Vitals, ByVal Value As Long)
    Player(Index).MaxVital(Vital) = Value
End Sub
Function GetPlayerPoints() As Long
    GetPlayerPoints = MyAttributes.StatPoints
End Function
Sub SetPlayerPoints(ByVal Points As Long)
    MyAttributes.StatPoints = Points
End Sub
Function GetPlayerMap(ByVal Index As Long) As Long
    GetPlayerMap = Player(Index).Map
End Function
Sub SetPlayerMap(ByVal Index As Long, ByVal MapNum As Long)
    Player(Index).Map = MapNum
End Sub
Function GetPlayerX(ByVal Index As Long) As Long
    GetPlayerX = Player(Index).X
End Function
Sub SetPlayerX(ByVal Index As Long, ByVal X As Long)
    Player(Index).X = X
End Sub
Function GetPlayerY(ByVal Index As Long) As Long
    GetPlayerY = Player(Index).Y
End Function
Sub SetPlayerY(ByVal Index As Long, ByVal Y As Long)
    Player(Index).Y = Y
End Sub
Function GetPlayerDir(ByVal Index As Long) As Long
    GetPlayerDir = Player(Index).Dir
End Function
Sub SetPlayerDirection(ByVal Index As Long, ByVal Dir As Long)
    Player(Index).Dir = Dir
End Sub
'######################
'####### STATS ########
'######################
Function GetPlayerStat(ByVal StatType As Stats) As Long
    GetPlayerStat = MyAttributes.Stat(StatType)
End Function
Sub SetPlayerStat(ByVal StatType As Stats, ByVal Value As Long)
    MyAttributes.Stat(StatType) = Value
End Sub
Function GetPlayerAttack() As Long
    GetPlayerAttack = MyAttributes.Attack
End Function
Sub SetPlayerAttack(ByVal Value As Long)
    MyAttributes.Attack = Value
End Sub
Function GetPlayerDefense() As Long
    GetPlayerDefense = MyAttributes.Defense
End Function
Sub SetPlayerDefense(ByVal Value As Long)
    MyAttributes.Defense = Value
End Sub
Function GetPlayerAccuracy() As Long
    GetPlayerAccuracy = MyAttributes.Accuracy
End Function
Sub SetPlayerAccuracy(ByVal Value As Long)
    MyAttributes.Accuracy = Value
End Sub
Function GetPlayerEvasion() As Long
    GetPlayerEvasion = MyAttributes.Evasion
End Function
Sub SetPlayerEvasion(ByVal Value As Long)
    MyAttributes.Evasion = Value
End Sub
Function GetPlayerMagicAttack() As Long
    GetPlayerMagicAttack = MyAttributes.MagicAttack
End Function
Sub SetPlayerMagicAttack(ByVal Value As Long)
    MyAttributes.MagicAttack = Value
End Sub
Function GetPlayerMagicDefense() As Long
    GetPlayerMagicDefense = MyAttributes.MagicDefense
End Function
Function SetPlayerMagicDefense(ByVal Value As Long) As Long
    MyAttributes.MagicDefense = Value
End Function
Function GetPlayerMagicAccuracy() As Long
    GetPlayerMagicAccuracy = MyAttributes.MagicAccuracy
End Function
Function SetPlayerMagicAccuracy(ByVal Value As Long) As Long
    MyAttributes.MagicAccuracy = Value
End Function
Function GetPlayerMagicResist() As Long
    GetPlayerMagicResist = MyAttributes.MagicResist
End Function
Function SetPlayerMagicResist(ByVal Value As Long) As Long
    MyAttributes.MagicResist = Value
End Function
Function GetPlayerConcentration() As Long
    GetPlayerConcentration = MyAttributes.Concentration
End Function
Function SetPlayerConcentration(ByVal Value As Long) As Long
    MyAttributes.Concentration = Value
End Function
Function GetPlayerParry() As Long
    GetPlayerParry = MyAttributes.Parry
End Function
Function SetPlayerParry(ByVal Value As Long) As Long
    MyAttributes.Parry = Value
End Function
Function GetPlayerBlock() As Long
    GetPlayerBlock = MyAttributes.Block
End Function
Function SetPlayerBlock(ByVal Value As Long) As Long
    MyAttributes.Block = Value
End Function
Function GetPlayerResistCritRate() As Long
    GetPlayerResistCritRate = MyAttributes.ResistCritRate
End Function
Sub SetPlayerResistCritRate(ByVal Value As Long)
    MyAttributes.ResistCritRate = Value
End Sub
Function GetPlayerResistCritDamage() As Long
    GetPlayerResistCritDamage = MyAttributes.ResistCritDamage
End Function
Sub SetPlayerResistCritDamage(ByVal Value As Long)
    MyAttributes.ResistCritDamage = Value
End Sub
Function GetPlayerCritRate() As Long
    GetPlayerCritRate = MyAttributes.CritRate
End Function
Sub SetPlayerCritRate(ByVal Value As Long)
    MyAttributes.CritRate = Value
End Sub
Function GetPlayerCritDamage() As Long
    GetPlayerCritDamage = MyAttributes.CritDamage
End Function
Sub SetPlayerCritDamage(ByVal Value As Long)
    MyAttributes.CritDamage = Value
End Sub


Function GetPlayerPveAttack() As Long
    GetPlayerPveAttack = MyAttributes.PveAttack
End Function
Sub SetPlayerPveAttack(ByVal Value As Long)
    MyAttributes.PveAttack = Value
End Sub

Function GetPlayerPveDefense() As Long
    GetPlayerPveDefense = MyAttributes.PveDefense
End Function
Sub SetPlayerPveDefense(ByVal Value As Long)
    MyAttributes.PveDefense = Value
End Sub


Function GetPlayerPvpAttack() As Long
    GetPlayerPvpAttack = MyAttributes.PvpAttack
End Function
Sub SetPlayerPvpAttack(ByVal Value As Long)
    MyAttributes.PvpAttack = Value
End Sub

Function GetPlayerPvpDefense() As Long
    GetPlayerPvpDefense = MyAttributes.PvpDefense
End Function
Sub SetPlayerPvpDefense(ByVal Value As Long)
    MyAttributes.PvpDefense = Value
End Sub


Function GetPlayerElementAttack(ByVal ElementType As Elements) As Long
    GetPlayerElementAttack = MyAttributes.ElementAttack(ElementType)
End Function
Sub SetPlayerElementAttack(ByVal ElementType As Elements, ByVal Value As Long)
    MyAttributes.ElementAttack(ElementType) = Value
End Sub

Function GetPlayerElementDefense(ByVal ElementType As Elements) As Long
    GetPlayerElementDefense = MyAttributes.ElementDefense(ElementType)
End Function
Sub SetPlayerElementDefense(ByVal ElementType As Elements, ByVal Value As Long)
    MyAttributes.ElementDefense(ElementType) = Value
End Sub

Function GetPlayerDamageSuppression() As Long
    GetPlayerDamageSuppression = MyAttributes.DamageSuppression
End Function
Sub SetPlayerDamageSuppression(ByVal Value As Long)
    MyAttributes.DamageSuppression = Value
End Sub

Function GetPlayerHealingPower() As Long
    GetPlayerHealingPower = MyAttributes.HealingPower
End Function
Sub SetPlayerHealingPower(ByVal Value As Long)
    MyAttributes.HealingPower = Value
End Sub

Function GetPlayerFinalDamage() As Long
    GetPlayerFinalDamage = MyAttributes.FinalDamage
End Function
Sub SetPlayerFinalDamage(ByVal Value As Long)
    MyAttributes.FinalDamage = Value
End Sub

Function GetPlayerAmplification() As Long
    GetPlayerAmplification = MyAttributes.Amplification
End Function
Sub SetPlayerAmplification(ByVal Value As Long)
    MyAttributes.Amplification = Value
End Sub

Function GetPlayerEnmity() As Long
    GetPlayerEnmity = MyAttributes.Enmity
End Function
Sub SetPlayerEnmity(ByVal Value As Long)
    MyAttributes.Enmity = Value
End Sub

Function GetPlayerAttackSpeed() As Long
    GetPlayerAttackSpeed = MyAttributes.AttackSpeed
End Function
Sub SetPlayerAttackSpeed(ByVal Value As Long)
    MyAttributes.AttackSpeed = Value
End Sub

Function GetPlayerCastSpeed() As Long
    GetPlayerCastSpeed = MyAttributes.CastSpeed
End Function
Sub SetPlayerCastSpeed(ByVal Value As Long)
    MyAttributes.CastSpeed = Value
End Sub

Function GetPlayerStumble() As Long
    GetPlayerStumble = MyAttributes.StumbleResistance
End Function
Sub SetPlayerStumble(ByVal Value As Long)
    MyAttributes.StumbleResistance = Value
End Sub

Function GetPlayerStun() As Long
    GetPlayerStun = MyAttributes.StunResistance
End Function
Sub SetPlayerStun(ByVal Value As Long)
    MyAttributes.StunResistance = Value
End Sub

Function GetPlayerSilence() As Long
    GetPlayerSilence = MyAttributes.SilenceResistance
End Function
Sub SetPlayerSilence(ByVal Value As Long)
    MyAttributes.SilenceResistance = Value
End Sub

Function GetPlayerBlind() As Long
    GetPlayerBlind = MyAttributes.BlindResistance
End Function
Sub SetPlayerBlind(ByVal Value As Long)
    MyAttributes.BlindResistance = Value
End Sub

Function GetPlayerExp() As Long
    GetPlayerExp = MyAttributes.Experience
End Function
Sub SetPlayerExp(ByVal Exp As Long)
    MyAttributes.Experience = Exp
End Sub

'#####################
'######## DEAD #######
'#####################
Function GetPlayerDead(ByVal Index As Long) As Boolean
    GetPlayerDead = Player(Index).Dead
End Function
Sub SetPlayerDead(ByVal Index As Long, ByVal Dead As Boolean)
    Player(Index).Dead = Dead
End Sub


' ########################################
Public Function GetPlayerEquipmentId(ByVal EquipmentSlot As PlayerEquipments) As Long
    GetPlayerEquipmentId = Equipment(EquipmentSlot).Num
End Function

Public Function GetPlayerEquipmentLevel(ByVal EquipmentSlot As PlayerEquipments) As Long
    GetPlayerEquipmentLevel = Equipment(EquipmentSlot).Level
End Function

Public Function GetPlayerEquipmentBound(ByVal EquipmentSlot As PlayerEquipments) As InventoryBoundType
    GetPlayerEquipmentBound = Equipment(EquipmentSlot).Bound
End Function

Public Function GetPlayerEquipmentAttributeId(ByVal EquipmentSlot As PlayerEquipments) As Byte
    GetPlayerEquipmentAttributeId = Equipment(EquipmentSlot).AttributeId
End Function

Public Function GetPlayerEquipmentUpgradeId(ByVal EquipmentSlot As PlayerEquipments) As Long
    GetPlayerEquipmentUpgradeId = Equipment(EquipmentSlot).UpgradeId
End Function

Public Sub SetPlayerEquipment(ByRef Equip As PlayerEquipmentRec, ByVal EquipmentSlot As PlayerEquipments)
    Equipment(EquipmentSlot).Num = Equip.Num
    Equipment(EquipmentSlot).Level = Equip.Level
    Equipment(EquipmentSlot).Bound = Equip.Bound
    Equipment(EquipmentSlot).AttributeId = Equip.AttributeId
    Equipment(EquipmentSlot).UpgradeId = Equip.UpgradeId
End Sub

Public Sub UpdateTwoHandedWeaponInformation()
    Dim EquipmentId As Long
    Dim ItemId As Long
    Dim Index As PlayerEquipments

    Dim Data As EquipmentRec

    ItemId = Equipment(PlayerEquipments.EquipWeapon).Num

    If ItemId > 0 And ItemId <= MaximumItems Then
        EquipmentId = Item(ItemId).EquipmentId

        If EquipmentId > 0 And EquipmentId <= MaximumEquipments Then
            Data = GetEquipmentData(EquipmentId)

            If Data.Type = Weapon Then
                If Data.HandStyle = HandStyle_TwoHanded Then
                    Call SetPlayerEquipment(Equipment(EquipWeapon), EquipShield)
                Else
                    If HasEquipmentSlotTwoHandedStyle(PlayerEquipments.EquipShield) Then
                        Equipment(PlayerEquipments.EquipShield).Num = 0
                        Equipment(PlayerEquipments.EquipShield).Level = 0
                        Equipment(PlayerEquipments.EquipShield).Bound = 0
                        Equipment(PlayerEquipments.EquipShield).AttributeId = 0
                        Equipment(PlayerEquipments.EquipShield).UpgradeId = 0
                    End If
                End If
            End If
        End If
    End If
End Sub

Private Function HasEquipmentSlotTwoHandedStyle(ByVal Index As PlayerEquipments) As Boolean
    Dim EquipmentId As Long
    Dim ItemId As Long
    Dim Data As EquipmentRec

    ItemId = Equipment(Index).Num

    If ItemId > 0 And ItemId <= MaximumItems Then
        EquipmentId = Item(ItemId).EquipmentId

        If EquipmentId > 0 And EquipmentId <= MaximumEquipments Then
            Data = GetEquipmentData(EquipmentId)

            If Data.Type = Weapon Then
                HasEquipmentSlotTwoHandedStyle = Data.HandStyle = HandStyle_TwoHanded
            End If
        End If
    End If

End Function

Public Sub ClearData()
    Dim i As Long, n As Long

    For n = 1 To MaxPlayers
        For i = 1 To MAX_PLAYER_ICON_EFFECT
            Call SetPlayerIconId(n, i, 0)
            Call SetPlayerIconDuration(n, i, 0)
            Call SetPlayerIconLevel(n, i, 0)
        Next
    Next

    ClearTitles
    Call ClearTitleWindow
End Sub

 Public Sub ClearPlayer(ByVal Index As Long)
    Call ZeroMemory(ByVal VarPtr(Player(Index)), LenB(Player(Index)))
    Player(Index).Name = vbNullString
End Sub


