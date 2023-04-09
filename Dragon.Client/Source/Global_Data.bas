Attribute VB_Name = "Global_Data"
Option Explicit

Public GameServerPort As Long
Public GameServerIp As String

Public CanDestroyGame As Boolean

' Loading Screen
Public LoadingText As String

' Description
Public DescType As Byte
Public DescItem As Long
Public DescLevel As Long
Public DescBound As Byte
Public DescAttribute As Long
Public DescUpgrade As Long
Public DescLastType As Byte
Public DescLastItem As Long
Public DescLastLevel As Long
Public DescLastBound As Byte
Public DescLastAttribute As Long
Public DescLastUpgrade As Long

Public DescText() As TextColourRec

' Block Swap Items
Public CanSwapInvItems As Boolean

' Elastic bars
Public BarWidth_NpcHP(1 To MaxMapNpcs) As Long
Public BarWidth_PlayerHP(1 To MaxPlayers) As Long
Public BarWidth_NpcHP_Max(1 To MaxMapNpcs) As Long
Public BarWidth_PlayerHP_Max(1 To MaxPlayers) As Long

Public BarWidth_GuiHP As Long
Public BarWidth_GuiSP As Long
Public BarWidth_GuiEXP As Long
Public BarWidth_GuiHP_Max As Long
Public BarWidth_GuiSP_Max As Long
Public BarWidth_GuiEXP_Max As Long

Public BarWidth_CraftExp As Long
Public BarWidth_CraftExp_Max As Long

Public BarWidth_GuiGuildExp As Long
Public BarWidth_GuiGuildExp_Max As Long

Public BarWidth_TargetHP As Long
Public BarWidth_TargetMP As Long
Public BarWidth_TargetHP_Max As Long
Public BarWidth_TargetMP_Max As Long

' Fog
Public FogOffsetX As Long
Public FogOffsetY As Long

' Chat Bubble
Public ChatBubble(1 To MAX_BYTE) As ChatBubbleRec
Public ChatBubbleIndex As Long

' Tutorial
Public inTutorial As Long
Public tutorialState As Byte

' NPC Chat
Public chatNpc As Long
Public chatText As String
Public chatOpt(1 To 4) As String
' Gui
Public hideGUI As Boolean
Public chatOn As Boolean
Public chatShowLine As String * 1

' Menu
Public inMenu As Boolean

' Cursor
Public GlobalX As Long
Public GlobalY As Long
Public GlobalX_Map As Long
Public GlobalY_Map As Long

' Paperdoll rendering order
Public PaperdollOrder() As Long

' Music & Sound List Cache
Public MusicCache() As String
Public SoundCache() As String
Public HasPopulated As Boolean

' Gui
Public inChat As Boolean

' Player Variables
Public MyIndex As Long    ' Index of actual player
Public InventoryItemSelected As Long
Public SpellBuffer As Long
Public SpellBufferTimer As Long
Public SpellCd(1 To MaxPlayerSkill) As Long
Public StunDuration As Long
Public TNL As Long

' Stops movement when updating a map
Public CanMoveNow As Boolean

' Debug mode
Public DEBUG_MODE As Boolean

' TCP variables
Public PlayerBuffer As String

' Controls main gameloop
Public InGame As Boolean
Public isLogging As Boolean

' Game direction vars
Public ShiftDown As Boolean
Public ControlDown As Boolean
Public TabDown As Boolean
Public WDown As Boolean
Public SDown As Boolean
Public ADown As Boolean
Public DDown As Boolean

' Used to freeze controls when getting a new map
Public GettingMap As Boolean

' Used to check if FPS needs to be drawn
Public BFPS As Boolean
Public BLoc As Boolean

' FPS and Time-based movement vars
Public ElapsedTime As Long
Public GameFPS As Long

' Text vars
Public vbQuote As String

' Mouse cursor tile location
Public CurX As Long
Public CurY As Long

Public Camera As RECT
Public TileView As RECT

' Pinging
Public PingStart As Long
Public PingEnd As Long
Public Ping As Long

' indexing
Public ActionMsgIndex As Byte
Public AnimationIndex As Byte

' fps lock
Public FPS_Lock As Boolean

' Looping Saves
Public Player_HighIndex As Long
Public Npc_HighIndex As Long
Public Action_HighIndex As Long

' Fading
Public FadeAlpha As Long

' Screenshot Mode
Public ScreenshotMode As Long

' Right Click Menu
Public PlayerMenuIndex As Long

' Chat
Public inSmallChat As Boolean
Public actChatHeight As Long
Public actChatWidth As Long
Public ChatButtonUp As Boolean
Public ChatButtonDown As Boolean


