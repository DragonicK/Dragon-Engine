Attribute VB_Name = "modNpcCorpse"
Option Explicit

Public Const MAX_CORPSES As Long = 255

Public Corpse(1 To MAX_CORPSES) As CorpseRec
Public Corpse_HighIndex As Long

Private Type CorpseRec
    LootId As Long
    ' Posição no mapa
    X As Long
    Y As Long
    Dir As Byte
    Alpha As Long
    Sprite As Long
    PlayerIndex As Long
    LootEmpty As Boolean
    CanDrawCorpse As Boolean
End Type

' Adiciona uma nova informação de corpo e loot.
Public Sub AddNpcCorpse(ByVal MapNpcNum As Long)
    Dim i As Long, FreeSlot As Long

    ' Procura um slot vazio para adicionar o corpo de um npc e o loot.
    For i = 1 To MAX_CORPSES
        If Corpse(i).LootId = 0 And Corpse(i).PlayerIndex = 0 Then
            FreeSlot = i
            Exit For
        End If
    Next

    If FreeSlot > 0 Then
        Corpse(FreeSlot).CanDrawCorpse = True
        Corpse(FreeSlot).X = MapNpc(MapNpcNum).X * PIC_X + MapNpc(MapNpcNum).xOffset
        Corpse(FreeSlot).Y = MapNpc(MapNpcNum).Y * PIC_Y + MapNpc(MapNpcNum).yOffset
        Corpse(FreeSlot).LootId = MapNpc(MapNpcNum).CorpseId
        Corpse(FreeSlot).Dir = MapNpc(MapNpcNum).Dir
        Corpse(FreeSlot).Sprite = Npc(MapNpc(MapNpcNum).Num).ModelId
        ' Quando vazio, ativa a contagem do alfa e desaparece com o corpo.
        Corpse(FreeSlot).LootEmpty = MapNpc(MapNpcNum).CorpseEmpty
        Corpse(FreeSlot).Alpha = 255

        CalculateHighIndex
    End If

End Sub

Public Sub AddPlayerCorpse(ByVal Index As Long)
    Dim i As Long, FreeSlot As Long

    ' Procura um slot vazio para adicionar o corpo de um player sem loot.
    For i = 1 To MAX_CORPSES
        If Corpse(i).LootId = 0 And Corpse(i).PlayerIndex = 0 Then
            FreeSlot = i
            Exit For
        End If
    Next

    ' Se sim, insere as informações.
    If FreeSlot > 0 Then
        Corpse(FreeSlot).LootId = 0
        Corpse(FreeSlot).PlayerIndex = Index
        ' Não permite que o corpo seja desenhado, o desenho é feito apenas quando o jogador reviver.
        Corpse(FreeSlot).CanDrawCorpse = False

        Corpse(FreeSlot).X = Player(Index).X * PIC_X + Player(Index).xOffset
        Corpse(FreeSlot).Y = Player(Index).Y * PIC_Y + Player(Index).yOffset
        Corpse(FreeSlot).Dir = Player(Index).Dir
        Corpse(FreeSlot).Sprite = Player(Index).Class
        Corpse(FreeSlot).LootEmpty = False
        Corpse(FreeSlot).Alpha = 255

        CalculateHighIndex
    End If

End Sub

Public Sub UpdatePlayerCorpse(ByVal PlayerIndex As Long)
    Dim Index As Long

    Index = FindPlayerCorpseIndex(PlayerIndex)

    If Index > 0 Then
        Corpse(Index).CanDrawCorpse = True
        Corpse(Index).LootEmpty = True
    End If
End Sub

Public Sub UpdateNpcCorpse(ByVal LootId As Long)
    Dim Index As Long

    Index = FindNpcCorpseIndex(LootId)

    If Index > 0 Then
        Corpse(Index).LootEmpty = True
    End If
End Sub

Public Sub ClearCorpses()
    Dim i As Long

    For i = 1 To MAX_CORPSES
        ClearCorpse (i)
    Next

End Sub

Private Sub ClearCorpse(ByVal Index As Long)
    Corpse(Index).CanDrawCorpse = False
    Corpse(Index).X = 0
    Corpse(Index).Y = 0
    Corpse(Index).LootId = 0
    Corpse(Index).Sprite = 0
    Corpse(Index).Dir = 0
    Corpse(Index).Alpha = 255
    Corpse(Index).LootEmpty = False
    Corpse(Index).PlayerIndex = 0
End Sub

Public Sub ProcessCorpseFadeAlpha()
    Dim i As Long

    For i = 1 To Corpse_HighIndex
        If Corpse(i).LootId > 0 Or Corpse(i).PlayerIndex > 0 Then
            If Corpse(i).LootEmpty Then

                If Corpse(i).Alpha > 5 Then
                    Corpse(i).Alpha = Corpse(i).Alpha - 5
                Else
                    Corpse(i).Alpha = 0
                    Call ClearCorpse(i)
                End If

            End If
        End If
    Next

End Sub

Private Function FindNpcCorpseIndex(ByVal LootId As Long) As Long
    Dim i As Long

    For i = 1 To Corpse_HighIndex
        If Corpse(i).LootId = LootId And Corpse(i).PlayerIndex = 0 Then
            FindNpcCorpseIndex = i
            Exit Function
        End If
    Next

End Function

Private Function FindPlayerCorpseIndex(ByVal PlayerIndex As Long) As Long
    Dim i As Long

    For i = 1 To Corpse_HighIndex
        If Corpse(i).LootId = 0 And Corpse(i).PlayerIndex = PlayerIndex Then
            FindPlayerCorpseIndex = i
            Exit Function
        End If
    Next

End Function

Private Sub CalculateHighIndex()
    Dim i As Long, HighIndex As Long

    For i = MAX_CORPSES To 1 Step -1
        If Corpse(i).LootId > 0 Or Corpse(i).PlayerIndex > 0 Then
            HighIndex = i
            Exit For
        End If
    Next

    Corpse_HighIndex = HighIndex

End Sub

Public Sub DrawCorpseUpper(ByVal Index As Long)
    Dim Dir As Byte
    Dim FrameIndex As Long
    Dim ModelIndex As Long
    Dim X As Long
    Dim Y As Long
    Dim FadeAlpha As Long

    X = Corpse(Index).X - 64
    Y = Corpse(Index).Y - 68
    Dir = Corpse(Index).Dir
    ModelIndex = GetModelIndex(Corpse(Index).Sprite)
    FadeAlpha = Corpse(Index).Alpha

    If ModelIndex > 0 Then
        FrameIndex = Models(ModelIndex).Death.Up.Count
        Call RenderMovementUpper(Dir, FrameIndex, X, Y, Models(ModelIndex).Death, DX8Colour(White, FadeAlpha))
    End If

End Sub

Public Sub DrawCorpseLower(ByVal Index As Long)
    Dim Dir As Byte
    Dim FrameIndex As Long
    Dim ModelIndex As Long
    Dim X As Long
    Dim Y As Long
    Dim FadeAlpha As Long

    X = Corpse(Index).X - 64
    Y = Corpse(Index).Y - 68
    Dir = Corpse(Index).Dir
    ModelIndex = GetModelIndex(Corpse(Index).Sprite)
    FadeAlpha = Corpse(Index).Alpha

    If ModelIndex > 0 Then
        FrameIndex = Models(ModelIndex).Death.Up.Count
        Call RenderMovementLower(Dir, FrameIndex, X, Y, Models(ModelIndex).Death, DX8Colour(White, FadeAlpha))
    End If
End Sub

Public Sub HandleNpcDead(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim n As Long, LootId As Long, LootEmpty As Byte
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data()
    n = Buffer.ReadLong
    LootId = Buffer.ReadLong
    LootEmpty = Buffer.ReadByte

    If n > 0 Then
        With MapNpc(n)
            .Dying = True
            .Vital(HP) = 0
            .Vital(MP) = 0
            .MaxVital(HP) = 0
            .MaxVital(MP) = 0
            ' As informações são salvas para quando a animação de morte terminar,
            ' ser adicionado ao corpo.
            .CorpseId = LootId
            .CorpseEmpty = LootEmpty

            ' Client use only
            .xOffset = 0
            .yOffset = 0
            .Moving = 0
        End With

        Call StartNpcDeathMovement(n)

        If MyTargetType = TargetTypeNpc And MyTarget = n Then
            MyTarget = 0
            MyTargetType = TargetTypeNone
        End If
    End If

End Sub

Public Sub HandleRessurection(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim pIndex As Long
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    pIndex = Buffer.ReadLong
    Player(pIndex).Dead = False

    Set Buffer = Nothing

    ' Faz com que o desenho fique ativo e o fade alpha seja aplicado.
    Call UpdatePlayerCorpse(pIndex)

End Sub

Public Sub HandlePlayerDead(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim pIndex As Long
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    pIndex = Buffer.ReadLong
    Player(pIndex).Dead = True

    Set Buffer = Nothing

    ' Adiciona o corpo na lista mas não é desenhado.
    Call AddPlayerCorpse(pIndex)
    Call StartDeathMovement(pIndex)

End Sub

Public Sub HandleMapLoot(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer, Count As Long, i As Long, n As Long
    Dim FreeSlot As Long
    Dim LootId As Long, X As Long, Y As Long, Dir As Byte, Sprite As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    Count = Buffer.ReadLong

    For i = 1 To Count
        LootId = Buffer.ReadLong
        X = Buffer.ReadInteger
        Y = Buffer.ReadInteger
        Sprite = Buffer.ReadLong
        Dir = Buffer.ReadByte

        ' Procura um slot vazio para adicionar o corpo de um npc e o loot.
        For n = 1 To MAX_CORPSES
            If Corpse(n).LootId = 0 And Corpse(n).PlayerIndex = 0 Then
                FreeSlot = n
                Exit For
            End If
        Next

        If FreeSlot > 0 Then
            Corpse(FreeSlot).LootId = LootId
            Corpse(FreeSlot).X = (X * PIC_X)
            Corpse(FreeSlot).Y = (Y * PIC_Y)
            Corpse(FreeSlot).Sprite = Sprite
            Corpse(FreeSlot).Dir = Dir
            Corpse(FreeSlot).Alpha = 255
            Corpse(FreeSlot).LootEmpty = False
            Corpse(FreeSlot).CanDrawCorpse = True
        End If
    Next

    CalculateHighIndex

    Set Buffer = Nothing

End Sub

Public Sub HandleUpdateLootState(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim LootId As Long
    Dim LootEmpty As Byte
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    LootId = Buffer.ReadLong
    LootEmpty = Buffer.ReadByte

    Set Buffer = Nothing

    Call UpdateNpcCorpse(LootId)

End Sub
