Attribute VB_Name = "Audio_Implementation"
Option Explicit

' FMOD
Public Enum FSOUND_INITMODES
    FSOUND_INIT_USEDEFAULTMIDISYNTH = &H1
End Enum

Public Enum FSOUND_MODES
    FSOUND_LOOP_OFF = &H1
    FSOUND_LOOP_NORMAL = &H2
    FSOUND_16BITS = &H10
    FSOUND_MONO = &H20
    FSOUND_SIGNED = &H100
    FSOUND_NORMAL = FSOUND_16BITS Or FSOUND_SIGNED Or FSOUND_MONO
End Enum

Public Enum FSOUND_CHANNELSAMPLEMODE
    FSOUND_FREE = -1
    FSOUND_STEREOPAN = -1
End Enum

Public Declare Function FSOUND_Init Lib "fmod.dll" Alias "_FSOUND_Init@12" (ByVal mixrate As Long, ByVal maxchannels As Long, ByVal flags As FSOUND_INITMODES) As Byte
Public Declare Function FSOUND_Close Lib "fmod.dll" Alias "_FSOUND_Close@0" () As Long
Public Declare Function FMUSIC_LoadSong Lib "fmod.dll" Alias "_FMUSIC_LoadSong@4" (ByVal Name As String) As Long
Public Declare Function FMUSIC_PlaySong Lib "fmod.dll" Alias "_FMUSIC_PlaySong@4" (ByVal Module As Long) As Byte
Public Declare Function FMUSIC_SetMasterVolume Lib "fmod.dll" Alias "_FMUSIC_SetMasterVolume@8" (ByVal Module As Long, ByVal Volume As Long) As Byte
Public Declare Function FSOUND_Stream_Open Lib "fmod.dll" Alias "_FSOUND_Stream_Open@16" (ByVal FileName As String, ByVal Mode As FSOUND_MODES, ByVal offset As Long, ByVal Length As Long) As Long
Public Declare Function FSOUND_Stream_Play Lib "fmod.dll" Alias "_FSOUND_Stream_Play@8" (ByVal Channel As Long, ByVal Stream As Long) As Long
Public Declare Function FSOUND_SetVolume Lib "fmod.dll" Alias "_FSOUND_SetVolume@8" (ByVal Channel As Long, ByVal Vol As Long) As Byte
Public Declare Function FSOUND_Stream_Stop Lib "fmod.dll" Alias "_FSOUND_Stream_Stop@4" (ByVal Stream As Long) As Byte
Public Declare Function FSOUND_Stream_Close Lib "fmod.dll" Alias "_FSOUND_Stream_Close@4" (ByVal Stream As Long) As Byte
Public Declare Function FMUSIC_StopSong Lib "fmod.dll" Alias "_FMUSIC_StopSong@4" (ByVal Module As Long) As Byte
Public Declare Function FMUSIC_FreeSong Lib "fmod.dll" Alias "_FMUSIC_FreeSong@4" (ByVal Module As Long) As Byte
Public Declare Function FSOUND_Sample_SetDefaults Lib "fmod.dll" Alias "_FSOUND_Sample_SetDefaults@20" (ByVal sptr As Long, ByVal deffreq As Long, ByVal defvol As Long, ByVal defpan As Long, ByVal defpri As Long) As Byte
Public Declare Function FSOUND_PlaySound Lib "fmod.dll" Alias "_FSOUND_PlaySound@8" (ByVal Channel As Long, ByVal sptr As Long) As Long
Public Declare Function FSOUND_Sample_Load Lib "fmod.dll" Alias "_FSOUND_Sample_Load@20" (ByVal Index As Long, ByVal Name As String, ByVal Mode As FSOUND_MODES, ByVal offset As Long, ByVal Length As Long) As Long

' Maximum sounds
Private MaxSounds As Long
Private Sounds() As String

' Hardcoded sound effects
Public Const Sound_ButtonHover As String = "Cursor1.wav"
Public Const Sound_ButtonClick As String = "Decision1.wav"

' Last sounds played
Public LastNpcChatsound As Long

' Init status
Public bInit_Music As Boolean
Public CurSong As String

' Music Handlers
Private SongHandle As Long
Private StreamHandle As Long

' Sound pointer array
Private SoundHandle() As Long

Public Function Init_Music() As Boolean
    Dim result As Boolean

    If inDevelopment Then Exit Function

    On Error GoTo ErrorHandler

    ' init music engine
    result = FSOUND_Init(44100, 32, FSOUND_INIT_USEDEFAULTMIDISYNTH)

    If Not result Then GoTo ErrorHandler
    ' return positive
    Init_Music = True
    bInit_Music = True
    Exit Function
ErrorHandler:
    Init_Music = False
    bInit_Music = False
End Function

Public Sub Destroy_Music()
' destroy music engine
    Stop_Music
    FSOUND_Close
    bInit_Music = False
    CurSong = vbNullString
End Sub

Public Sub Play_Music(ByVal song As String)

    On Error GoTo ErrorHandler

    If Not bInit_Music Then Exit Sub

    ' exit out early if we have the system turned off
    If Options.Music = 0 Then Exit Sub

    ' does it exist?
    If Not FileExist(App.Path & MUSIC_PATH & song) Then Exit Sub

    ' don't re-start currently playing songs
    If CurSong = song Then Exit Sub
    ' stop the existing music
    Stop_Music

    ' find the extension
    Select Case Right$(song, 4)

    Case ".mid", ".s3m", ".mod"
        ' open the song
        SongHandle = FMUSIC_LoadSong(App.Path & MUSIC_PATH & song)
        ' play it
        FMUSIC_PlaySong SongHandle
        ' set volume
        FMUSIC_SetMasterVolume SongHandle, 150

    Case ".wav", ".mp3", ".ogg", ".wma"
        ' open the stream
        StreamHandle = FSOUND_Stream_Open(App.Path & MUSIC_PATH & song, FSOUND_LOOP_NORMAL, 0, 0)
        ' play it
        FSOUND_Stream_Play FSOUND_FREE, StreamHandle
        ' set volume
        FSOUND_SetVolume StreamHandle, 150

    Case Else
        Exit Sub
    End Select

    ' new current song
    CurSong = song
    Exit Sub
ErrorHandler:
    Destroy_Music
End Sub

Public Sub Stop_Music()

    On Error GoTo ErrorHandler

    If Not StreamHandle = 0 Then
        ' stop stream
        FSOUND_Stream_Stop StreamHandle
        ' destroy
        FSOUND_Stream_Close StreamHandle
        StreamHandle = 0
    End If

    If Not SongHandle = 0 Then
        ' stop song
        FMUSIC_StopSong SongHandle
        ' destroy
        FMUSIC_FreeSong SongHandle
        SongHandle = 0
    End If

    ' no music
    CurSong = vbNullString
    Exit Sub
ErrorHandler:
    Destroy_Music
End Sub

Public Sub Play_Sound(ByRef Sound As String, Optional ByVal X As Long = -1, Optional ByVal Y As Long = -1)
    Dim dX As Long, dY As Long, Volume As Long, distance As Long
    Dim Index As Long

    On Error GoTo ErrorHandler

    If Not bInit_Music Then Exit Sub

    ' exit out early if we have the system turned off
    If Options.Sound = 0 Then Exit Sub
    If X > -1 And Y > -1 Then

        ' x
        If X < GetPlayerX(MyIndex) Then
            dX = GetPlayerX(MyIndex) - X
        ElseIf X > GetPlayerX(MyIndex) Then
            dX = X - GetPlayerX(MyIndex)
        End If

        ' y
        If Y < GetPlayerY(MyIndex) Then
            dY = GetPlayerY(MyIndex) - Y
        ElseIf Y > GetPlayerY(MyIndex) Then
            dY = Y - GetPlayerY(MyIndex)
        End If

        ' distance
        distance = dX ^ 2 + dY ^ 2
        Volume = 150 - (distance / 2)
    Else
        Volume = 150
    End If

    ' cap the volume
    If Volume < 0 Then Volume = 0
    If Volume > 256 Then Volume = 256

    Index = GetSoundIndex(Sound)

    If Index > 0 Then

        If SoundHandle(Index) = 0 Then
            ' load the sound
            Load_Sound Sound, Index
        End If

        FSOUND_Sample_SetDefaults SoundHandle(Index), -1, Volume, FSOUND_STEREOPAN, -1
        ' play it
        FSOUND_PlaySound FSOUND_FREE, SoundHandle(Index)

    End If

    Exit Sub
ErrorHandler:
    Destroy_Music
End Sub

Public Sub Load_Sound(ByVal Sound As String, ByVal Index As Long)

    On Error GoTo ErrorHandler
    
    ' load the sound
    SoundHandle(Index) = FSOUND_Sample_Load(FSOUND_FREE, App.Path & SOUND_PATH & Sound, FSOUND_NORMAL, 0, 0)
    
    Exit Sub
ErrorHandler:
    Destroy_Music
End Sub

Public Sub LoadSounds()
    Dim File As String
    Dim Counter As Long
       
    File = Dir(App.Path & SOUND_PATH)
    
    Do While LenB(File)
        Counter = Counter + 1
        
        ReDim Preserve Sounds(1 To Counter)
        
        Sounds(Counter) = LCase$(File)
    
        File = Dir
    Loop
    
    MaxSounds = Counter
    
    ReDim SoundHandle(1 To Counter)
    
End Sub

Private Function GetSoundIndex(ByRef Sound As String) As Long
    Dim i As Long
    
    If Sound = "None." Then Exit Function
    
    For i = 1 To MaxSounds
        If Sounds(i) = Sound Then
            GetSoundIndex = i
            Exit Function
        End If
    Next

End Function

Public Sub PlayMapSound(ByVal X As Long, ByVal Y As Long, ByVal EntityType As SoundEntity, ByVal EntityNum As Long)
    Dim soundName As String

    If EntityNum <= 0 Then Exit Sub

    ' find the sound
    Select Case EntityType

    Case SoundEntity.SeAnimation
        If EntityNum > MAX_ANIMATIONS Then Exit Sub
        soundName = Animation(EntityNum).Sound

    Case SoundEntity.SeItem
        If EntityNum > MaximumItems Then Exit Sub
        soundName = Item(EntityNum).Sound

    Case SoundEntity.SeNpc
        If EntityNum > MaximumNpcs Then Exit Sub
        soundName = Npc(EntityNum).Sound

    Case SoundEntity.SeResource
        ' If entityNum > MAX_RESOURCES Then Exit Sub
        ' soundName = Trim$(Resource(entityNum).Sound)

    Case SoundEntity.SeSpell
        If EntityNum > MaximumSkills Then Exit Sub
        soundName = Skill(EntityNum).Sound

    Case Else
        Exit Sub
    End Select

    ' exit out if it's not set
    If Trim$(soundName) = "None." Then
        Exit Sub
    End If

    ' play the sound
    If X > 0 And Y > 0 Then
        Call Play_Sound(soundName, X, Y)
    End If
End Sub
