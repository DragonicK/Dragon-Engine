Attribute VB_Name = "Constants_Implementation"
Option Explicit

Public Const LOCALE_BRAZIL As Long = 1046
Public Const LOCALE_USA As Long = 1033

' String constants
Public Const NAME_LENGTH As Byte = 120
Public Const DESCRIPTION_LENGTH As Byte = 255

' in development? [turn off music]
Public Const inDevelopment As Boolean = True

' Version constants
Public Const CLIENT_MAJOR As Byte = 2
Public Const CLIENT_MINOR As Byte = 0
Public Const CLIENT_REVISION As Byte = 0

' Connection details
Public Const AUTH_SERVER_IP As String = "127.0.0.1"    '"177.54.146.249"
Public Const AUTH_SERVER_PORT As Long = 7001  '2  ' the port used for people to connect to auth server

' Codigo de autorizacao.
Public Const AUTH_CODE As String = "a51e12214a"

' Resolution count
Public Const RES_COUNT As Long = 16
' Music
Public Const MenuMusic As String = "_menu.mid"
' GUI
Public Const ChatBubbleWidth As Long = 200
Public Const CHAT_TIMER As Long = 20000

' Currency
Public Const CurrencyLabelX As Byte = 15
Public Const CurrencyLabelY As Byte = 40
Public Const CurrencyLabelOffsetY As Byte = 35

' QuickSlot constants
Public Const HotbarTop As Long = 0
Public Const HotbarLeft As Long = 8
Public Const HotbarOffsetX As Long = 41
' Shop constants

' API Declares
Public Declare Sub CopyMemory Lib "kernel32.dll" Alias "RtlMoveMemory" (Destination As Any, Source As Any, ByVal Length As Long)
Public Declare Function CallWindowProc Lib "user32" Alias "CallWindowProcA" (ByVal lpPrevWndFunc As Long, ByVal hWnd As Long, ByRef Msg() As Byte, ByVal wParam As Long, ByVal lParam As Long) As Long
Public Declare Function GetForegroundWindow Lib "user32" () As Long
' Animation
Public Const AnimColumns As Long = 5
' values
Public Const MAX_BYTE As Byte = 255
Public Const MAX_INTEGER As Integer = 32767
Public Const MAX_LONG As Long = 2147483647
' path constants
Public Const SOUND_PATH As String = "\Data Files\Sound\"
Public Const MUSIC_PATH As String = "\Data Files\Music\"

' Map Path and variables
Public Const MAP_PATH As String = "\Data Files\Maps\"
Public Const CHARACTER_PATH As String = "\Data Files\Models\"

' Key constants
Public Const VK_A As Long = &H41
Public Const VK_D As Long = &H44
Public Const VK_S As Long = &H53
Public Const VK_W As Long = &H57
Public Const VK_SHIFT As Long = &H10
Public Const VK_RETURN As Long = &HD
Public Const VK_CONTROL As Long = &H11
Public Const VK_TAB As Long = &H9
Public Const VK_LEFT As Long = &H25
Public Const VK_UP As Long = &H26
Public Const VK_RIGHT As Long = &H27
Public Const VK_DOWN As Long = &H28

' Speed moving vars
Public Const WALK_SPEED As Byte = 2
Public Const RUN_SPEED As Byte = 4

' Tile size constants
Public Const PIC_X As Long = 32
Public Const PIC_Y As Long = 32
' ********************************************************
' * The values below must match with the server's values *
' ********************************************************
' General constants
Public Const MaxPlayers As Long = 200
Public Const MAX_LEVELS As Byte = 25

Public Const MAX_CONVS As Byte = 100
Public Const MAX_CHARS As Byte = 3
Public Const MAX_PLAYER_ICON_EFFECT As Long = 30

' Website
Public Const GAME_NAME As String = "Twilight Online"
Public Const GAME_WEBSITE As String = "http://www.twilightonline.com"

' Sex constants
Public Const SEX_MALE As Byte = 0
Public Const SEX_FEMALE As Byte = 1

' Tile consants
Public Const TILE_TYPE_WALKABLE As Byte = 0
Public Const TILE_TYPE_BLOCKED As Byte = 1
Public Const TILE_TYPE_WARP As Byte = 2
Public Const TILE_TYPE_ITEM As Byte = 3
Public Const TILE_TYPE_NPCAVOID As Byte = 4
Public Const TILE_TYPE_KEY As Byte = 5
Public Const TILE_TYPE_KEYOPEN As Byte = 6
Public Const TILE_TYPE_RESOURCE As Byte = 7
Public Const TILE_TYPE_DOOR As Byte = 8
Public Const TILE_TYPE_NPCSPAWN As Byte = 9
Public Const TILE_TYPE_SHOP As Byte = 10
Public Const TILE_TYPE_BANK As Byte = 11
Public Const TILE_TYPE_HEAL As Byte = 12
Public Const TILE_TYPE_TRAP As Byte = 13
Public Const TILE_TYPE_SLIDE As Byte = 14
Public Const TILE_TYPE_CHAT As Byte = 15
Public Const TILE_TYPE_APPEAR As Byte = 16

' Direction constants
Public Const DIR_UP As Byte = 0
Public Const DIR_DOWN As Byte = 1
Public Const DIR_LEFT As Byte = 2
Public Const DIR_RIGHT As Byte = 3

' Constants for player movement: Tiles per Second
Public Const MOVING_WALKING As Byte = 1
Public Const MOVING_RUNNING As Byte = 2

' Admin constants
Public Const ACCESS_NORMAL As Byte = 1
Public Const ACCESS_MONITOR As Byte = 2
Public Const ACCESS_GAMEMASTER As Byte = 3
Public Const ACCESS_ADMINISTRATOR As Byte = 4
Public Const ACCESS_SUPERIOR As Byte = 5

' NPC constants
Public Const NPC_BEHAVIOUR_NONE As Byte = 0
Public Const NPC_BEHAVIOUR_FRIENDLY As Byte = 1
Public Const NPC_BEHAVIOUR_PATROL As Byte = 2
Public Const NPC_BEHAVIOUR_SHOPKEEPER As Byte = 3
Public Const NPC_BEHAVIOUR_MONSTER As Byte = 4
Public Const NPC_BEHAVIOUR_BOSS As Byte = 5

