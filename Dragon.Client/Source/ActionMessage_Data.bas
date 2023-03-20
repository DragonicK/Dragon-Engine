Attribute VB_Name = "ActionMessage_Data"
Option Explicit

Public ActionMsg(1 To MAX_BYTE) As ActionMsgRec

' Scrolling action message constants
Public Const ACTIONMsgSTATIC As Long = 0
Public Const ACTIONMsgSCROLL As Long = 1

Public Const ACTION_MSG_FONT_DAMAGE As Long = 0
Public Const ACTION_MSG_FONT_ALPHABET As Long = 1

Private Type ActionMsgRec
    Message As String
    Created As Long
    Type As Long
    Color As Long
    Scroll As Long
    X As Long
    Y As Long
    Timer As Long
    Alpha As Long
    FontType As Byte
    TickCount As Long
End Type

Public Type ChatBubbleRec
    Msg As String
    Colour As Long
    Target As Long
    TargetType As Byte
    Timer As Long
    Active As Boolean
End Type

Public Type TextColourRec
    Text As String
    Colour As Long
End Type

Public Sub ClearAnimation(ByVal Index As Long)
    Call ZeroMemory(ByVal VarPtr(Animation(Index)), LenB(Animation(Index)))
    Animation(Index).Sound = "None."
End Sub

Public Sub ClearAnimations()
    Dim i As Long

    For i = 1 To MAX_ANIMATIONS
        Call ClearAnimation(i)
    Next
End Sub

Public Sub LoadAnimations()
    Dim i As Long

    For i = 1 To MAX_ANIMATIONS
        Call ClearAnimation(i)

       ' If FileExist(App.Path & AnimationPath & "Animation" & i & ".dat") Then
            Call LoadAnimation(i)
       ' End If
    Next
End Sub

Public Sub LoadAnimation(ByVal Index As Long)
    Dim FileName As String, f As Long
    Dim i As Long, Layer As Long, Num As Long
    Dim ByteValue As Byte
    Dim Sound As String * NAME_LENGTH

    FileName = vbNullString 'App.Path & AnimationPath & "Animation" & Index & ".dat"

    f = FreeFile
    Open FileName For Binary As #f

    With Animation(Index)
        Get #f, , Num
        Get #f, , Sound

        Layer = 0

        Get #f, , .FrameCount(Layer)
        Get #f, , .Looptime(Layer)
        Get #f, , .OffsetX(Layer)
        Get #f, , .OffSetY(Layer)
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
        Get #f, , .OffSetY(Layer)
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

