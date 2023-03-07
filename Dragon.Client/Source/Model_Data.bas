Attribute VB_Name = "Model_Data"
Option Explicit

Public Models() As ModelRec

' Quantidade de personagens carregados.
Public Model_Count As Long

Public Enum DirectionType
    DirectionType_Up
    DirectionType_Down
    DirectionType_Left
    DirectionType_Right
End Enum

Public Type FrameRec
    Texture As TextureStruct
    CanMove As Boolean
    SendAttack As Long
    CastSkillId As Long
    AnimationId As Long
    AnimationX As Long
    AnimationY As Long
    Height As Long
    Width As Long
End Type

Public Type MovementRec
    Count As Long
    Time As Long
    Continuously As Boolean
    WaitResponse As Boolean
    Frames() As FrameRec
End Type

Public Type DirectionRec
    Id As Long
    Up As MovementRec
    Down As MovementRec
    Left As MovementRec
    Right As MovementRec
End Type

Public Type ModelRec
    Id As Long
    Attack As DirectionRec
    Death As DirectionRec
    Ressurrection As DirectionRec
    Talk As DirectionRec
    Gathering As DirectionRec
    Walking As DirectionRec
    Running As DirectionRec
    Idle As DirectionRec

    SpecialCount As Long
    Special() As DirectionRec

    EmoteCount As Long
    Emote() As DirectionRec
End Type

Public Sub LoadModels()
    Dim File As String, i As Long, Count As Long

    Count = CountModelFiles

    ReDim Models(1 To Count)

    File = Dir$(App.Path & CHARACTER_PATH & "*.csc")

    Do While Len(File)
        i = i + 1

        Call ReadModel(Models(i), File)

        File = Dir$
    Loop

    Model_Count = Count
End Sub

Private Function CountModelFiles() As Long
    Dim File As String, Count As Long

    File = Dir$(App.Path & CHARACTER_PATH & "*.csc")

    Do While Len(File)
        File = Dir$

        Count = Count + 1
    Loop

    CountModelFiles = Count

End Function

Private Sub ReadModel(ByRef Model As ModelRec, ByVal File As String)
    Dim Index As Long
    Dim FileName As String
    Dim Name As String
    Dim Count As Long
    Dim i As Long

    FileName = App.Path & CHARACTER_PATH & File

    Index = GetFileHandler(FileName)

    If Index = 0 Then
        Model.Id = ReadInt32()

        Name = String(512, vbNullChar)
        Call ReadString(Name)

        Call ReadDirection(Model.Attack, Index)
        Call ReadDirection(Model.Death, Index)
        Call ReadDirection(Model.Ressurrection, Index)
        Call ReadDirection(Model.Talk, Index)
        Call ReadDirection(Model.Gathering, Index)
        Call ReadDirection(Model.Walking, Index)
        Call ReadDirection(Model.Running, Index)
        Call ReadDirection(Model.Idle, Index)

        Model.SpecialCount = ReadInt32()

        If Model.SpecialCount > 0 Then
            ReDim Model.Special(1 To Model.SpecialCount)

            For i = 1 To Count
                Call ReadDirection(Model.Special(i), Index)
            Next
        End If

        Model.EmoteCount = ReadInt32()

        If Model.EmoteCount > 0 Then
            ReDim Model.Emote(1 To Model.EmoteCount)

            For i = 1 To Count
                Call ReadDirection(Model.Emote(i), Index)
            Next
        End If
        
    End If

    Call CloseFileHandler

End Sub

Private Sub ReadDirection(ByRef Direction As DirectionRec, ByVal Index As Long)
    Dim Id As Long
    Dim Name As String
    
    Name = String(512, vbNullChar)
    
    Id = ReadInt32()
    Call ReadString(Name)
    
    Call ReadMovement(Direction.Up, Index)
    Call ReadMovement(Direction.Down, Index)
    Call ReadMovement(Direction.Left, Index)
    Call ReadMovement(Direction.Right, Index)
End Sub

Private Sub ReadMovement(ByRef Movement As MovementRec, ByVal Index As Long)
    Dim Name As String
    Dim i As Long

    Name = String(512, vbNullChar)

    Movement.Count = ReadInt32()
    Call ReadString(Name)
    Movement.Time = ReadInt32()
    Movement.Continuously = ReadBoolean()
    Movement.WaitResponse = ReadBoolean()

    If Movement.Count > 0 Then
        ReDim Movement.Frames(1 To Movement.Count)
        
        For i = 1 To Movement.Count
            Call ReadFrame(Movement.Frames(i), Index)
        Next
    End If
End Sub

Private Sub ReadFrame(ByRef Frame As FrameRec, ByVal Index As Long)
    Dim Settings As AesSettings
    Dim Password As String

    Password = "dwHandle"

    Settings.CipherMode = AesCipherMode.AesCipherMode_CBC
    Settings.PaddingMode = AesPaddingMode.AesPaddingMode_PKCS7
    Settings.KeyBitSize = AesBitSize.AesBitSize_128

    CreateKey Password, ByVal VarPtr(Settings.Key(0))
    CreateIv Password, ByVal VarPtr(Settings.IV(0))

    Dim i As Long
    Dim Name As String
    Dim Length As Long
    Dim Success As Boolean

    Name = String(512, vbNullChar)
    Call ReadString(Name)

    Length = ReadInt32()

    If Length > 0 Then
        Dim Buffer() As Byte

        ReDim Buffer(0 To Length - 1)

        Call ReadBytes(ByVal VarPtr(Buffer(0)), Length)

        Dim DecryptedLength As Long
        Dim Decrypted() As Byte

        ReDim Decrypted(0 To Length - 1)

        Success = Decrypt(ByVal VarPtr(Settings), ByVal VarPtr(Buffer(0)), Length, ByVal VarPtr(Decrypted(0)), ByVal VarPtr(DecryptedLength))

        If Success Then
            If DecryptedLength <> Length Then
                ReDim Preserve Decrypted(0 To DecryptedLength - 1)
            End If
            
            Call LoadParallaxTexture(Decrypted, Frame.Texture)
        End If
    End If

    Frame.Width = ReadInt32()
    Frame.Height = ReadInt32()
    Frame.CanMove = ReadBoolean()
    Frame.SendAttack = ReadInt32()
    Frame.CastSkillId = ReadInt32()
    Frame.AnimationId = ReadInt32()
    Frame.AnimationX = ReadInt32()
    Frame.AnimationY = ReadInt32()

End Sub
