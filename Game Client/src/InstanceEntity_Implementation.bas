Attribute VB_Name = "InstanceEntity_Implementation"
Option Explicit

Function GetNpcDead(ByVal Index As Long) As Boolean
    If MapNpc(Index).Num = 0 Then
        MapNpc(Index).Dead = True
    End If

    GetNpcDead = MapNpc(Index).Dead
End Function
Sub SetNpcDead(ByVal Index As Long, ByVal Value As Boolean)
    MapNpc(Index).Dead = Value
End Sub


