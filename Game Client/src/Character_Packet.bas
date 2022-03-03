Attribute VB_Name = "Character_Packet"
Option Explicit

Public Sub SendUnequip(ByVal eqNum As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PUnequipItem
    Buffer.WriteLong eqNum
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendUseAttributePoint(ByVal StatNum As Byte)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PUseAttributePoint
    Buffer.WriteLong StatNum
    
    SendData Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub HandlePlayerModel(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim Model As Long
    Set Buffer = New clsBuffer
    
    Index = Buffer.ReadLong
    Model = Buffer.ReadLong
    
    Call SetPlayerSprite(Index, Model)
    
    Set Buffer = Nothing
End Sub

Public Sub HandleEquipment(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, i As Long, Count As Long
    Dim Equip As PlayerEquipmentRec

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Count = Buffer.ReadLong

    For i = 1 To Count
        Index = Buffer.ReadLong

        Equip.Num = Buffer.ReadLong
        Equip.Level = Buffer.ReadLong
        Equip.Bound = Buffer.ReadByte
        Equip.AttributeId = Buffer.ReadLong
        Equip.UpgradeId = Buffer.ReadLong

        Call SetPlayerEquipment(Equip, Index)
        
        Call UpdateTwoHandedWeaponInformation
    Next

    Set Buffer = Nothing
End Sub

Public Sub HandleEquipmentUpdate(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, Value As Long
    Dim Equip As PlayerEquipmentRec

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Index = Buffer.ReadLong

    Equip.Num = Buffer.ReadLong
    Equip.Level = Buffer.ReadLong
    Equip.Bound = Buffer.ReadByte
    Equip.AttributeId = Buffer.ReadLong
    Equip.UpgradeId = Buffer.ReadLong

    Call SetPlayerEquipment(Equip, Index)

    Call UpdateTwoHandedWeaponInformation

    Set Buffer = Nothing
End Sub

Public Sub HandleAttributePoint(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim i As Long
    Dim WindowIndex As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Call SetPlayerPoints(Buffer.ReadLong)

    Set Buffer = Nothing

    WindowIndex = GetWindowIndex("winCharacter")

    With Windows(WindowIndex)
        .Controls(GetControlIndex("winCharacter", "lblPoints")).Text = "DISPONÍVEL: " & GetPlayerPoints()

        For i = 1 To Stats.Stat_Count - 1
            .Controls(GetControlIndex("winCharacter", "lblStat_" & i)).Text = GetPlayerStat(i)
        Next

        ' grey out buttons
        If GetPlayerPoints() = 0 Then
            For i = 1 To Stats.Stat_Count - 1
                .Controls(GetControlIndex("winCharacter", "btnGreyStat_" & i)).Visible = True
            Next
        Else
            For i = 1 To Stats.Stat_Count - 1
                .Controls(GetControlIndex("winCharacter", "btnGreyStat_" & i)).Visible = False
            Next
        End If

    End With

End Sub

Public Sub HandlePlayerStats(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim i As Long, WindowIndex As Long, WindowIndex2 As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Call SetPlayerPoints(Buffer.ReadLong)

    For i = 1 To Stats.Stat_Count - 1
        Call SetPlayerStat(i, Buffer.ReadLong)
    Next

    Call SetPlayerAttack(Buffer.ReadLong)
    Call SetPlayerDefense(Buffer.ReadLong)
    Call SetPlayerAccuracy(Buffer.ReadLong)
    Call SetPlayerEvasion(Buffer.ReadLong)
    Call SetPlayerParry(Buffer.ReadLong)
    Call SetPlayerBlock(Buffer.ReadLong)

    Call SetPlayerMagicAttack(Buffer.ReadLong)
    Call SetPlayerMagicDefense(Buffer.ReadLong)
    Call SetPlayerMagicAccuracy(Buffer.ReadLong)
    Call SetPlayerMagicResist(Buffer.ReadLong)

    Call SetPlayerConcentration(Buffer.ReadLong)

    Call SetPlayerCritRate(Buffer.ReadLong)
    Call SetPlayerCritDamage(Buffer.ReadLong)
    Call SetPlayerResistCritRate(Buffer.ReadLong)
    Call SetPlayerResistCritDamage(Buffer.ReadLong)

    Call SetPlayerDamageSuppression(Buffer.ReadLong)
    Call SetPlayerHealingPower(Buffer.ReadLong)
    Call SetPlayerFinalDamage(Buffer.ReadLong)
    Call SetPlayerAmplification(Buffer.ReadLong)
    Call SetPlayerEnmity(Buffer.ReadLong)
    Call SetPlayerAttackSpeed(Buffer.ReadLong)
    Call SetPlayerCastSpeed(Buffer.ReadLong)

    Call SetPlayerSilence(Buffer.ReadLong)
    Call SetPlayerBlind(Buffer.ReadLong)
    Call SetPlayerStun(Buffer.ReadLong)
    Call SetPlayerStumble(Buffer.ReadLong)

    Call SetPlayerPveAttack(Buffer.ReadLong)
    Call SetPlayerPveDefense(Buffer.ReadLong)
    Call SetPlayerPvpAttack(Buffer.ReadLong)
    Call SetPlayerPvpDefense(Buffer.ReadLong)

    For i = 1 To Elements.Element_Count - 1
        Call SetPlayerElementAttack(i, Buffer.ReadLong)
        Call SetPlayerElementDefense(i, Buffer.ReadLong)
    Next

    Set Buffer = Nothing

    WindowIndex2 = GetWindowIndex("winCharacter")

    With Windows(WindowIndex2)
        .Controls(GetControlIndex("winCharacter", "lblPoints")).Text = "DISPONÍVEL: " & GetPlayerPoints()

        For i = 1 To Stats.Stat_Count - 1
            .Controls(GetControlIndex("winCharacter", "lblStat_" & i)).Text = GetPlayerStat(i)
        Next

        ' grey out buttons
        If GetPlayerPoints() = 0 Then
            For i = 1 To Stats.Stat_Count - 1
                .Controls(GetControlIndex("winCharacter", "btnGreyStat_" & i)).Visible = True
            Next
        Else
            For i = 1 To Stats.Stat_Count - 1
                .Controls(GetControlIndex("winCharacter", "btnGreyStat_" & i)).Visible = False
            Next
        End If

    End With


End Sub

Public Sub HandleExperience(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    Call SetPlayerExp(Buffer.ReadLong)
    TNL = Buffer.ReadLong

    ' set max width
    If GetPlayerLevel(MyIndex) <= MAX_LEVELS Then
        If GetPlayerExp() > 0 Then
            BarWidth_GuiEXP_Max = ((GetPlayerExp() / 209) / (TNL / 209)) * 209
        Else
            BarWidth_GuiEXP_Max = 0
        End If
    Else
        BarWidth_GuiEXP_Max = 209
    End If

    ' Update GUI
    UpdateStats_UI
End Sub
