Attribute VB_Name = "InstanceEntity_Packet"
Option Explicit

Public Sub HandleInstanceEntities(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim i As Long, n As Long
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data()

    Npc_HighIndex = Buffer.ReadLong

    For i = 1 To Npc_HighIndex
        With MapNpc(i)
            Index = Buffer.ReadLong

            .Num = Buffer.ReadLong
            .X = Buffer.ReadLong
            .Y = Buffer.ReadLong
            .Dead = Buffer.ReadByte
            .Dir = Buffer.ReadLong

            n = Buffer.ReadLong

            For n = 1 To Vitals.Vital_Count - 1
                .Vital(n) = Buffer.ReadLong
            Next

            n = Buffer.ReadLong

            For n = 1 To Vitals.Vital_Count - 1
                .MaxVital(n) = Buffer.ReadLong
            Next

            .Dead = .Vital(HP) <= 0

        End With
    Next

End Sub

Public Sub HandleInstanceEntity(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim i As Long, n As Long
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data()

    Index = Buffer.ReadLong + 1

    With MapNpc(Index)
        .Num = Buffer.ReadLong
        .X = Buffer.ReadLong
        .Y = Buffer.ReadLong
        .Dead = Buffer.ReadByte
        .Dir = Buffer.ReadLong

        n = Buffer.ReadLong

        For n = 1 To Vitals.Vital_Count - 1
            .Vital(n) = Buffer.ReadLong
        Next

        n = Buffer.ReadLong

        For n = 1 To Vitals.Vital_Count - 1
            .MaxVital(n) = Buffer.ReadLong
        Next

        .Dead = .Vital(HP) <= 0

        ' Client use only
        .xOffset = 0
        .yOffset = 0
        .Moving = 0
    End With

    Set Buffer = Nothing

End Sub

Public Sub HandleInstanceEntityDirection(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim i As Long
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data()

    Index = Buffer.ReadLong + 1

    With MapNpc(Index)
        .Dir = Buffer.ReadLong
        .xOffset = 0
        .yOffset = 0
        .Moving = 0
    End With
    
    Set Buffer = Nothing
End Sub

Public Sub HandleInstanceEntityVital(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim i As Long
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data()

    Index = Buffer.ReadLong + 1

    With MapNpc(Index)
        i = Buffer.ReadLong

        For i = 1 To Vitals.Vital_Count - 1
            .Vital(i) = Buffer.ReadLong
        Next

        i = Buffer.ReadLong

        For i = 1 To Vitals.Vital_Count - 1
            .MaxVital(i) = Buffer.ReadLong
        Next

        .Dead = .Vital(HP) <= 0
        
        If .Dead Then
            Call ShouldCloseTargetWindow(TargetTypeNpc, Index)
        End If

    End With


    Set Buffer = Nothing
End Sub

Public Sub HandleInstanceEntityMove(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim X As Long
    Dim Y As Long
    Dim Dir As Long
    Dim Movement As Long

    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteBytes Data()

    Index = Buffer.ReadLong + 1
    X = Buffer.ReadLong
    Y = Buffer.ReadLong
    Dir = Buffer.ReadLong
    Movement = Buffer.ReadLong
    
    Set Buffer = Nothing

    With MapNpc(Index)
        .X = X
        .Y = Y
        .Dir = Dir
        .xOffset = 0
        .yOffset = 0
        .Moving = Movement

        Select Case .Dir

        Case DIR_UP
            .yOffset = PIC_Y

        Case DIR_DOWN
            .yOffset = PIC_Y * -1

        Case DIR_LEFT
            .xOffset = PIC_X

        Case DIR_RIGHT
            .xOffset = PIC_X * -1
            
        End Select
        
    End With

End Sub
