Attribute VB_Name = "modTypes"
Option Explicit

Public TempTile() As TempTileRec
Public Player(1 To MaxPlayers) As PlayerRec

Private Type PlayerRec
    ' General
    Name As String
    Class As Long
    Sprite As Long
    Level As Byte
    Access As Byte
    PK As Byte
    ActiveTitleNum As Long

    ' Vitals
    Vital(1 To Vitals.Vital_Count - 1) As Long
    MaxVital(1 To Vitals.Vital_Count - 1) As Long

    Icons(1 To MAX_PLAYER_ICON_EFFECT) As ActiveIconRec

    Dead As Boolean

    ' Position
    Map As Long
    X As Byte
    Y As Byte
    Dir As Byte

    ' Variables
    Variable(1 To MAX_BYTE) As Long

    ' Client use only
    xOffset As Integer
    yOffset As Integer
    Moving As Byte
    Attacking As Byte
    AttackTimer As Long

    ' Animação para morte.
    Dying As Byte

    ModelIndex As Long
    FrameIndex As Long
    FrameTick As Long
    AttackFrameIndex As Long

    SavedX As Long
    SavedY As Long
End Type

Private Type TempTileRec
    ' doors... obviously
    DoorOpen As Byte
    DoorFrame As Byte
    DoorTimer As Long
    DoorAnimate As Byte    ' 0 = nothing| 1 = opening | 2 = closing
    ' fading appear tiles
    IsFading(1 To MapLayer.Layer_Count - 1) As Boolean
    FadeAlpha(1 To MapLayer.Layer_Count - 1) As Long
    FadeTimer(1 To MapLayer.Layer_Count - 1) As Long
    FadeDir(1 To MapLayer.Layer_Count - 1) As Byte
End Type




