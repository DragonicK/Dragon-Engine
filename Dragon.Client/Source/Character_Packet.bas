Attribute VB_Name = "Character_Packet"
Option Explicit

Public Sub SendViewEquipmentVisibility(ByVal IsVisible As Boolean)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PViewEquipmentVisibility
    Buffer.WriteBoolean IsVisible

    SendData Buffer.ToArray()

    Set Buffer = Nothing
End Sub

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

Public Sub HandlePlayerData(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim i As Long, X As Long, Dead As Byte
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    i = Buffer.ReadLong

    Call SetPlayerName(i, Buffer.ReadString)
    Call SetPlayerAccess(i, Buffer.ReadLong)
    Call SetPlayerClass(i, Buffer.ReadLong)
    Call SetPlayerSprite(i, Buffer.ReadLong)
    Call SetPlayerLevel(i, Buffer.ReadLong)
    Call SetPlayerMap(i, Buffer.ReadLong)
    Call SetPlayerX(i, Buffer.ReadInteger)
    Call SetPlayerY(i, Buffer.ReadInteger)
    Call SetPlayerDirection(i, Buffer.ReadLong)
    Call SetPlayerTitle(i, Buffer.ReadLong)
    Call SetPlayerDead(i, Buffer.ReadByte)

    ' Check if the player is the client player
    If i = MyIndex Then
        ' Reset directions
        WDown = False
        ADown = False
        SDown = False
        DDown = False

        With Windows(GetWindowIndex("winCharacter"))
            .Controls(GetControlIndex("winCharacter", "lblName")).Text = UCase$(GetPlayerName(MyIndex)) & " LV. " & GetPlayerLevel(MyIndex)
            .Controls(GetControlIndex("winCharacter", "lblClass")).Text = UCase$(Class(GetPlayerClass(MyIndex)).Name)
        End With

        Call UpdateActiveTitle(GetPlayerTitle(MyIndex))
    End If

    ' Define a sprite do jogador.
    Call SetPlayerModelIndex(i)

    ' Make sure they aren't walking
    Player(i).Moving = 0
    Player(i).xOffset = 0
    Player(i).yOffset = 0
End Sub

Public Sub HandlePlayerHp(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, pIndex As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    pIndex = Buffer.ReadLong

    Call SetPlayerMaxVital(pIndex, Vitals.HP, Buffer.ReadLong)
    Call SetPlayerVital(pIndex, Vitals.HP, Buffer.ReadLong)

    If pIndex = MyIndex Then
        ' set max width
        If GetPlayerVital(MyIndex, Vitals.HP) > 0 Then
            BarWidth_GuiHP_Max = ((GetPlayerVital(MyIndex, Vitals.HP) / 209) / (GetPlayerMaxVital(MyIndex, Vitals.HP) / 209)) * 209
        Else
            BarWidth_GuiHP_Max = 0
        End If

        ' Update GUI
        UpdateStats_UI
    End If

End Sub

Public Sub HandlePlayerMp(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, pIndex As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data()

    pIndex = Buffer.ReadLong

    Call SetPlayerMaxVital(pIndex, Vitals.MP, Buffer.ReadLong)
    Call SetPlayerVital(pIndex, Vitals.MP, Buffer.ReadLong)

    If pIndex = MyIndex Then
        ' set max width
        If GetPlayerVital(MyIndex, Vitals.MP) > 0 Then
            BarWidth_GuiSP_Max = ((GetPlayerVital(MyIndex, Vitals.MP) / 209) / (GetPlayerMaxVital(MyIndex, Vitals.MP) / 209)) * 209
        Else
            BarWidth_GuiSP_Max = 0
        End If
        ' Update GUI
        UpdateStats_UI
    End If

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

Public Sub HandleClearPlayers(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim i As Long

    For i = 1 To MaxPlayers
        Call ClearPlayer(i)
    Next
End Sub

