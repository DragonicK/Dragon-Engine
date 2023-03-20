Attribute VB_Name = "Animation_Data"
Option Explicit

Public Animation(1 To MAX_ANIMATIONS) As AnimationRec
Public AnimInstance(1 To MAX_BYTE) As AnimInstanceRec

Private Const AnimationPath As String = "\Data Files\Data\Animations\"

'  Index = GetFileHandler(App.Path & "\Data Files\Data\Achievements.dat")

Public Type AnimationFrameRec
    AttackFrame As Byte
    DamageFrame As Byte
End Type

Public Type AnimationRec
    Sound As String
    Sprite(0 To 1) As Long
    FrameCount(0 To 1) As Long
    LoopCount(0 To 1) As Long
    Looptime(0 To 1) As Long
    OffsetX(0 To 1) As Long
    OffSetY(0 To 1) As Long
    W(0 To 1) As Long
    H(0 To 1) As Long
    LowerFrames() As AnimationFrameRec
    UpperFrames() As AnimationFrameRec
End Type

Public Type AnimInstanceRec
    Animation As Long
    X As Long
    Y As Long
    ' used for locking to players/npcs
    lockIndex As Long
    LockType As Byte
    IsCasting As Byte
    ' timing
    Timer(0 To 1) As Long
    ' rendering check
    Used(0 To 1) As Boolean
    ' counting the loop
    LoopIndex(0 To 1) As Long
    FrameIndex(0 To 1) As Long
End Type
