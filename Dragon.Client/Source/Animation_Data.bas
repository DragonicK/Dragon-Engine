Attribute VB_Name = "Animation_Data"
Option Explicit

Public MaximumAnimations As Long

Public Animation() As AnimationRec
Public AnimInstance(1 To MAX_BYTE) As AnimInstanceRec

Private Const LowerFrameIndex As Long = 0
Private Const UpperFrameIndex As Long = 1

Public Type AnimationRec
    Id As Long
    Sound As String
    Sprite(0 To 1) As Long
    FrameCount(0 To 1) As Long
    LoopCount(0 To 1) As Long
    Looptime(0 To 1) As Long
    OffsetX(0 To 1) As Long
    OffSetY(0 To 1) As Long
    DestWidth(0 To 1) As Long
    DestHeight(0 To 1) As Long
End Type

Public Type AnimInstanceRec
    Animation As Long
    X As Long
    Y As Long
    ' used for locking to players/npcs
    LockIndex As Long
    LockType As Byte
    IsCasting As Byte
    ' timing
    Timer(0 To 1) As Long
    ' rendering check
    Used(0 To 1) As Boolean
    ' counting the loop
    LoopIndex(0 To 1) As Long
    FrameIndex(0 To 1) As Long
    LastX As Long
    LastY As Long
End Type

Public Sub ClearAnimation(ByVal Index As Long)
    Call ZeroMemory(ByVal VarPtr(Animation(Index)), LenB(Animation(Index)))
    Animation(Index).Sound = "None."
End Sub

Public Sub ClearAnimations()
    Dim i As Long

    For i = 1 To MaximumAnimations
        Call ClearAnimation(i)
    Next
End Sub

Public Sub LoadAnimations()
    Dim Index As Long
    Dim i As Long, n As Long
    Dim Name As String
    Dim Sound As String

    If Not FileExist(App.Path & "\Data Files\Data\Animations.dat") Then
        MsgBox ("\Data Files\Data\Animations.dat not found.")
        
        Exit Sub
    End If
    
    Index = GetFileHandler(App.Path & "\Data Files\Data\Animations.dat")

    If Index = 0 Then
        MaximumAnimations = ReadInt32()

        If MaximumAnimations > 0 Then
            ReDim Animation(1 To MaximumAnimations)

            For i = 1 To MaximumAnimations
                With Animation(i)
                    .Id = ReadInt32()

                    Name = String(255, vbNullChar)
                    Sound = String(255, vbNullChar)

                    Call ReadString(Name)
                    Call ReadString(Sound)

                    .Sound = Replace(Sound, vbNullChar, vbNullString)

                    .Sprite(LowerFrameIndex) = ReadInt32()
                    .FrameCount(LowerFrameIndex) = ReadInt32()
                    .LoopCount(LowerFrameIndex) = ReadInt32()
                    .Looptime(LowerFrameIndex) = ReadInt32()
                    .OffsetX(LowerFrameIndex) = ReadInt32()
                    .OffSetY(LowerFrameIndex) = ReadInt32()
                    .DestWidth(LowerFrameIndex) = ReadInt32()
                    .DestHeight(LowerFrameIndex) = ReadInt32()

                    .Sprite(UpperFrameIndex) = ReadInt32()
                    .FrameCount(UpperFrameIndex) = ReadInt32()
                    .LoopCount(UpperFrameIndex) = ReadInt32()
                    .Looptime(UpperFrameIndex) = ReadInt32()
                    .OffsetX(UpperFrameIndex) = ReadInt32()
                    .OffSetY(UpperFrameIndex) = ReadInt32()
                    .DestWidth(UpperFrameIndex) = ReadInt32()
                    .DestHeight(UpperFrameIndex) = ReadInt32()

                End With
            Next
        End If
    End If

    Call CloseFileHandler

End Sub
