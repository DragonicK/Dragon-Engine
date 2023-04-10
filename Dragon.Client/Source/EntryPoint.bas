Attribute VB_Name = "EntryPoint"
Option Explicit

' Halts thread of execution
Public Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)
' Get system uptime in milliseconds
Public Declare Function GetTickCount Lib "kernel32" () As Long
' For Clear functions
Public Declare Sub ZeroMemory Lib "kernel32.dll" Alias "RtlZeroMemory" (Destination As Any, ByVal Length As Long)

Public Sub Main()
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

    LoadAnimations
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
    LoadConversations
    LoadQuests

    InitPlayerAchievementArray
    AllocateAllAchievements

    InitializeDefaultCurrency
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
    ShowWindow GetWindowIndex("winLoginFooter")
    
    inSmallChat = True
    ' Set the loop going
    FadeAlpha = 255

    ' play the menu music
    If Len(Trim$(MenuMusic)) > 0 Then Play_Music Trim$(MenuMusic)

    MenuLoop
End Sub

