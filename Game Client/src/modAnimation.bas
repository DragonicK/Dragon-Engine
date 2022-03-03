Attribute VB_Name = "modAnimation"
Public Animation(1 To MAX_ANIMATIONS) As AnimationRec
Public AnimInstance(1 To MAX_BYTE) As AnimInstanceRec

Private Const AnimationPath As String = "\Data Files\Data\Animations\"

Private Type AnimationFrameRec
    AttackFrame As Byte
    DamageFrame As Byte
End Type

Private Type AnimationRec
    Sound As String
    Sprite(0 To 1) As Long
    FrameCount(0 To 1) As Long
    LoopCount(0 To 1) As Long
    Looptime(0 To 1) As Long
    OffsetX(0 To 1) As Long
    OffsetY(0 To 1) As Long
    W(0 To 1) As Long
    H(0 To 1) As Long
    LowerFrames() As AnimationFrameRec
    UpperFrames() As AnimationFrameRec
End Type

Private Type AnimInstanceRec
    Animation As Long
    X As Long
    Y As Long
    ' used for locking to players/npcs
    lockindex As Long
    LockType As Byte
    isCasting As Byte
    ' timing
    Timer(0 To 1) As Long
    ' rendering check
    Used(0 To 1) As Boolean
    ' counting the loop
    LoopIndex(0 To 1) As Long
    FrameIndex(0 To 1) As Long
End Type

Sub ClearAnimation(ByVal Index As Long)
    Call ZeroMemory(ByVal VarPtr(Animation(Index)), LenB(Animation(Index)))
    Animation(Index).Sound = "None."
End Sub

Sub ClearAnimations()
    Dim i As Long

    For i = 1 To MAX_ANIMATIONS
        Call ClearAnimation(i)
    Next

End Sub

Public Sub LoadAnimations()
    Dim i As Long

    For i = 1 To MAX_ANIMATIONS
        Call ClearAnimation(i)

        If FileExist(App.Path & AnimationPath & "Animation" & i & ".dat") Then
            Call LoadAnimation(i)
        End If
    Next

End Sub

Private Sub LoadAnimation(ByVal Index As Long)
    Dim Filename As String, f As Long
    Dim i As Long, Layer As Long, Num As Long
    Dim ByteValue As Byte
    Dim Sound As String * NAME_LENGTH

    Filename = App.Path & AnimationPath & "Animation" & Index & ".dat"

    f = FreeFile
    Open Filename For Binary As #f

    With Animation(Index)
        Get #f, , Num
        Get #f, , Sound

        Layer = 0

        Get #f, , .FrameCount(Layer)
        Get #f, , .Looptime(Layer)
        Get #f, , .OffsetX(Layer)
        Get #f, , .OffsetY(Layer)
        Get #f, , .W(Layer)
        Get #f, , .H(Layer)
        Get #f, , .Sprite(Layer)
        Get #f, , .LoopCount(Layer)

        If .FrameCount(Layer) > 0 Then
            ReDim .LowerFrames(1 To .FrameCount(Layer))

            For i = 1 To .FrameCount(Layer)
                Get #f, , ByteValue
                .LowerFrames(i).AttackFrame = ByteValue

                Get #f, , ByteValue
                .LowerFrames(i).DamageFrame = ByteValue
            Next
        End If

        Layer = 1

        Get #f, , .FrameCount(Layer)
        Get #f, , .Looptime(Layer)
        Get #f, , .OffsetX(Layer)
        Get #f, , .OffsetY(Layer)
        Get #f, , .W(Layer)
        Get #f, , .H(Layer)
        Get #f, , .Sprite(Layer)
        Get #f, , .LoopCount(Layer)

        If .FrameCount(Layer) > 0 Then
            ReDim .UpperFrames(1 To .FrameCount(Layer))

            For i = 1 To .FrameCount(Layer)
                Get #f, , ByteValue
                .UpperFrames(i).AttackFrame = ByteValue

                Get #f, , ByteValue
                .UpperFrames(i).DamageFrame = ByteValue
            Next
        End If

        .Sound = Trim$(Sound)

    End With

    Close #f
End Sub
