Attribute VB_Name = "modEnumerations"
Option Explicit

' Stats used by Players, Npcs and Classes
Public Enum Stats
    Strength = 1
    Agility
    Constitution
    Intelligence
    Spirit
    Will
    ' Make sure Stat_Count is below everything else
    Stat_Count
End Enum

' Vitals used by Players, Npcs and Classes
Public Enum Vitals
    HP = 1
    MP
    SP
    ' Make sure Vital_Count is below everything else
    Vital_Count
End Enum

Public Enum Proficiencies
    None
    Cloth
    Leather
    Chain
    Plate
    Sword
    Dagger
    Mace
    Bow
    Axe
    Polearm
    Greatsword
    Staff
    Spellbook
    Orb
    Shield
    Proficiency_Count
End Enum

' Layers in a map
Public Enum MapLayer
    Ground = 1
    Mask
    Mask2
    Fringe
    Fringe2
    ' Make sure Layer_Count is below everything else
    Layer_Count
End Enum

' Sound entities
Public Enum SoundEntity
    SeAnimation = 1
    SeItem
    SeNpc
    SeResource
    SeSpell
    ' Make sure SoundEntity_Count is below everything else
    SoundEntity_Count
End Enum

' Menu
Public Enum MenuCount
    menuMain = 1
    menuLogin
    menuRegister
    menuCredits
    menuClass
    menuNewChar
    menuChars
    menuMerge
End Enum

' Chat channels
Public Enum ChatChannel
    chGame = 0
    chMap
    chGlobal
    chParty
    chGuild
    chPrivate
    ' last
    Channel_Count
End Enum

Public Enum DialogueType
    TypeName = 0
    TypeTRADE
    TypeFORGET
    TypePARTY
    TypeLOOTITEM
    TypeALERT
    TypeDELCHAR
    TypeDESTROYITEM
    TypeTRADEAMOUNT
    TypeDEPOSITITEM
    TypeWITHDRAWITEM
    TypeTRADEGOLDAMOUNT
    TypeSELLAMOUNT
    TypeDELETEMAIL
    TypeADDMAILCURRENCY
    TypeADDMAILAMOUNT
End Enum

Public Enum DialogueStyle
    StyleOKAY = 1
    StyleYESNO
    StyleINPUT
    StyleNoOperation
End Enum

Public Enum Elements
    Element_Neutral = 0
    Element_Fire = 1
    Element_Water
    Element_Earth
    Element_Wind
    Element_Light
    Element_Dark
    Element_Count
End Enum
