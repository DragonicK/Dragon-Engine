Attribute VB_Name = "InstanceEntity_Data"
Option Explicit

Public Const MaxMapNpcs As Long = 255
Public MapNpc(1 To MaxMapNpcs) As MapNpcRec

Public Type MapNpcRec
    Num As Long
    Target As Long
    TargetType As Long
    Vital(1 To Vitals.Vital_Count - 1) As Long
    MaxVital(1 To Vitals.Vital_Count - 1) As Long
    X As Long
    Y As Long
    Dir As Long
    
    ' Client use only
    xOffset As Long
    yOffset As Long
    Moving As Byte
    Attacking As Byte
    AttackTimer As Long
    Step As Byte
    Anim As Long
    AnimTimer As Long

    Dead As Boolean
    ' Id do loot.
    CorpseId As Long
    CorpseEmpty As Boolean

    ' Animação de morte
    Dying As Boolean
    ModelIndex As Integer
    FrameIndex As Integer
    FrameTick As Long
    AttackFrameIndex As Long

    SavedX As Long
    SavedY As Long
End Type

