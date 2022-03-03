Attribute VB_Name = "modGeneral"
Option Explicit
' halts thread of execution
Public Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)
' get system uptime in milliseconds
Public Declare Function GetTickCount Lib "kernel32" () As Long
'For Clear functions
Public Declare Sub ZeroMemory Lib "kernel32.dll" Alias "RtlZeroMemory" (Destination As Any, ByVal Length As Long)

Public Sub Main()

'Dim MD5Value As String
'MD5Value = GetFileMD5(App.path & "\Client.exe")
'MsgBox MD5Value

    MaxPlayerMail = 20
    InitializePlayerMails

    ReDim DescText(1 To 1)

    LoadMachineId

    ' Load Options
    LoadOptions
    ' check the resolution
    CheckResolution
    ' load maps
    LoadMapsProperties

    ClearServices

    LoadNotificationIcons
    LoadTitles
    LoadTitleAttributes
    LoadRecipes
    LoadAchievements
    LoadAchievementAttributes
    LoadEquipments
    LoadEquipmentAttributes
    LoadEquipmentEnhancements
    LoadEquipmentSets
    LoadEquipmentSetAttributes
    LoadHeraldryEnhancements
    LoadHeraldryAttributes
    LoadNpcs
    LoadAttributeEffects
    LoadAttributeEffectAttributes
    LoadAttributeEffectEnhancements
    LoadSkills
    LoadPassives
    LoadPassiveAttributes
    LoadPassiveEnhancements
    LoadItems
    ' LoadAnimations
    LoadConversations

    InitPlayerAchievementArray
    AllocateAllAchievements

    InitializeCurrency
    InitializeClasses

    ' load dx8
    If Options.Fullscreen Then
        frmMain.BorderStyle = 0
        frmMain.caption = frmMain.caption
    End If

    frmMain.Show
    InitDX8 frmMain.hWnd
    DoEvents

    LoadTarget
    LoadModels
    LoadTextures
    LoadFonts

    ' Initialise the gui
    InitGUI
    ' Resize the GUI to screen size
    ResizeGUI
    ' Initialise sound & music engines
    Init_Music
    LoadSounds

    ' Load the main game (and by extension, pre-load DD7)
    GettingMap = True
    vbQuote = ChrW(34)

    ' Update the form with the game's name before it's loaded
    frmMain.caption = GAME_NAME
    ' Randomize rnd's seed
    Randomize

    Call TcpInit(AUTH_SERVER_IP, AUTH_SERVER_PORT)
    Call InitMessages

    ' Reset values
    Ping = -1

    ' set the paperdoll order
    ReDim PaperdollOrder(1 To Equipments.Equipment_Count - 1) As Long
    PaperdollOrder(1) = Equipments.Armor
    PaperdollOrder(2) = Equipments.Helmet
    PaperdollOrder(3) = Equipments.Shield
    PaperdollOrder(4) = Equipments.Weapon
    
    HideWindows
    
    ' show the main menu
    frmMain.Show
    inMenu = True

    ' show login window
    ShowWindow GetWindowIndex("winLogin")

    inSmallChat = True
    ' Set the loop going
    FadeAlpha = 255

    ' play the menu music
    If Len(Trim$(MenuMusic)) > 0 Then Play_Music Trim$(MenuMusic)

    MenuLoop
End Sub

Public Sub AddChar(Name As String, Sex As Long, Class As Long, Sprite As Long)

    If ConnectToServer Then
        HideWindows
        Call SetStatus("Enviando informações do personagem.")
        Call SendAddChar(Name, Sex, Class, Sprite)
        Exit Sub
    Else
        ShowWindow GetWindowIndex("winLogin")
        Dialogue "Problema de Conexao", "Não pode conectar-se ao servidor de jogo.", "", TypeALERT
    End If

End Sub

Public Sub Login(Name As String, Password As String)
    TcpInit AUTH_SERVER_IP, AUTH_SERVER_PORT

    If ConnectToServer Then
    HideWindows
        Call SetStatus("Enviando informações de login.")
        Call SendAuthLogin(Name, Password)
        ' save details
        If Options.SaveUser Then Options.Username = Name Else Options.Username = vbNullString
        SaveOptions
        Exit Sub
    Else
        ShowWindow GetWindowIndex("winLogin")
        Dialogue "Problema de Conexao", "Não pode conectar-se ao servidor de login.", "Tente novamente mais tarde.", TypeALERT
    End If

End Sub

Public Sub LogoutGame()
    Dim i As Long
    isLogging = True
    InGame = False

    ClearData
    ClearParty
    ClearServices
    ClearCraft
    ClearCorpses
    ClearInventories

    DestroyTCP

    ' destroy the animations loaded
    For i = 1 To MAX_BYTE
        Call ClearAnimInstance(i)
    Next

    For i = 1 To MAX_CHARS
        CharPendingExclusion(i) = False
        CharHour(i) = 0
        CharMinutes(i) = 0
        CharSeconds(i) = 0
        CharDate(i) = 0
    Next

    ' destroy temp values
    MyIndex = 0
    InventoryItemSelected = 0
    InventoryentoryTabIndex = 0
    SpellBuffer = 0
    SpellBufferTimer = 0

    ' unload editors
    Unload frmFont_Editor

    ' clear chat
    For i = 1 To ChatLines
        Chat(i).Text = vbNullString
    Next

    inMenu = True

    Erase GroundParallax
    Erase FringeParallax

    MenuLoop
End Sub

Public Sub DestroyGame()

    If Not CanDestroyGame Then
        CanDestroyGame = True

        DestroyDX8

        Call DestroyTCP

        ' destroy music & sound engines
        Destroy_Music

        Call UnloadAllForms

        End

    End If
End Sub

Public Sub UnloadAllForms()
    Dim frm As Form

    For Each frm In VB.Forms
        Unload frm
    Next

End Sub

Public Sub SetStatus(ByVal caption As String)

    
    If Len(Trim$(caption)) > 0 Then
        ShowWindow GetWindowIndex("winLoading")
        Windows(GetWindowIndex("winLoading")).Controls(GetControlIndex("winLoading", "lblLoading")).Text = caption
    Else
        HideWindow GetWindowIndex("winLoading")
        Windows(GetWindowIndex("winLoading")).Controls(GetControlIndex("winLoading", "lblLoading")).Text = vbNullString
    End If
End Sub

Public Function Rand(ByVal Low As Long, ByVal High As Long) As Long
    Rand = Int((High - Low + 1) * Rnd) + Low
End Function

Public Function isLoginLegal(ByVal Username As String, ByVal Password As String) As Boolean

    If LenB(Trim$(Username)) >= 3 Then
        If LenB(Trim$(Password)) >= 3 Then
            isLoginLegal = True
        End If
    End If

End Function

Public Function IsStringLegal(ByVal sInput As String) As Boolean
    Dim i As Long, tmpNum As Long
    ' Prevent high ascii chars
    tmpNum = Len(sInput)

    For i = 1 To tmpNum

        If Asc(Mid$(sInput, i, 1)) < vbKeySpace Or Asc(Mid$(sInput, i, 1)) > vbKeyF15 Then
            Dialogue "Models ilegais", "O nome contém caracteres não permitidos.", "", TypeALERT
            Exit Function
        End If

    Next

    IsStringLegal = True
End Function

Public Sub PopulateLists()
    Dim strLoad As String, i As Long
    ' Cache music list
    strLoad = Dir$(App.Path & MUSIC_PATH & "*.*")
    i = 1

    Do While strLoad > vbNullString
        ReDim Preserve MusicCache(1 To i) As String
        MusicCache(i) = strLoad
        strLoad = Dir
        i = i + 1
    Loop

    ' Cache sound list
    strLoad = Dir$(App.Path & SOUND_PATH & "*.*")
    i = 1

    Do While strLoad > vbNullString
        ReDim Preserve SoundCache(1 To i) As String
        SoundCache(i) = strLoad
        strLoad = Dir
        i = i + 1
    Loop

End Sub

Private Function GetFileMD5(ByVal FileName As String) As String
    Dim MD5 As New clsMd5, f As Long, Buff() As Byte
    Const BuffSize As Long = 2 ^ 16    ' (64 KBytes)

    ' On Error GoTo ErrExit
    f = FreeFile

    Open FileName For Binary Access Read As f
    MD5.MD5Init

    Do Until Loc(f) >= LOF(f)
        If Loc(f) + BuffSize > LOF(f) Then
            ReDim Buff(LOF(f) - Loc(f) - 1)
        Else
            ReDim Buff(BuffSize - 1)
        End If

        Get f, , Buff
        MD5.MD5Update UBound(Buff) + 1, Buff
    Loop

    MD5.MD5Final
    GetFileMD5 = MD5.GetValues

    Close f

    Exit Function

ErrExit:
    Err.Clear
    GetFileMD5 = vbNullString
End Function
