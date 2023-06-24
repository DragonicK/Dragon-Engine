Attribute VB_Name = "Color_Implementation"
Option Explicit

Public Enum ColorType
    Black
    Blue
    Green
    Cyan
    Red
    Magenta
    Brown
    Grey
    DarkGrey
    BrightBlue
    BrightGreen
    BrightCyan
    BrightRed
    Pink
    Yellow
    White
    DarkBrown
    Gold
    LightGreen
    Purple
    Coral
    Orange
    DeepSkyBlue
    HealGreen
End Enum

' Pointers
Public Const SayColor As Byte = Black
Public Const GlobalColor As Byte = BrightBlue
Public Const BroadcastColor As Byte = White
Public Const TellColor As Byte = BrightGreen
Public Const EmoteColor As Byte = BrightCyan
Public Const AdminColor As Byte = BrightCyan
Public Const HelpColor As Byte = BrightBlue
Public Const WhoColor As Byte = BrightBlue
Public Const JoinLeftColor As Byte = DarkGrey
Public Const NpcColor As Byte = Brown
Public Const AlertColor As Byte = Red
Public Const NewMapColor As Byte = BrightBlue

Public Function GetCurrencyColor(ByVal Amount As Long) As Long
    If CLng(Amount) < 1000000 Then
        GetCurrencyColor = White
    ElseIf CLng(Amount) >= 1000000 And CLng(Amount) <= 10000000 Then
        GetCurrencyColor = Yellow
    ElseIf CLng(Amount) > 10000000 Then
        GetCurrencyColor = Coral
    End If
End Function

Public Function GetRarityColor(ByVal Rarity As Long) As Long
    Select Case Rarity
    Case 0    ' Common
        GetRarityColor = White
    Case 1    ' Uncommon
        GetRarityColor = Green
    Case 2    ' Rare
        GetRarityColor = BrightBlue
    Case 3    ' Epic
        GetRarityColor = Coral
    Case 4    ' Mythic
        GetRarityColor = Purple
    Case 5    ' Ancient
        GetRarityColor = Pink
    Case 6    ' Legendary
        GetRarityColor = Gold
    Case 7   ' Ethereal
        GetRarityColor = BrightRed
    End Select
End Function

Public Function DX8Colour(ByVal ColourNum As ColorType, ByVal Alpha As Long) As Long
    Select Case ColourNum
    Case ColorType.Black
        DX8Colour = D3DColorARGB(Alpha, 0, 0, 0)
    Case ColorType.Blue
        DX8Colour = D3DColorARGB(Alpha, 16, 104, 237)
    Case ColorType.Green
        DX8Colour = D3DColorARGB(Alpha, 119, 188, 84)
    Case ColorType.Cyan
        DX8Colour = D3DColorARGB(Alpha, 16, 224, 237)
    Case ColorType.Red
        DX8Colour = D3DColorARGB(Alpha, 201, 0, 0)
    Case ColorType.Magenta
        DX8Colour = D3DColorARGB(Alpha, 255, 0, 255)
    Case ColorType.Brown
        DX8Colour = D3DColorARGB(Alpha, 175, 149, 92)
    Case ColorType.Grey
        DX8Colour = D3DColorARGB(Alpha, 192, 192, 192)
    Case ColorType.DarkGrey
        DX8Colour = D3DColorARGB(Alpha, 128, 128, 128)
    Case ColorType.BrightBlue
        DX8Colour = D3DColorARGB(Alpha, 126, 182, 240)
    Case ColorType.BrightGreen
        DX8Colour = D3DColorARGB(Alpha, 126, 240, 137)
    Case ColorType.BrightCyan
        DX8Colour = D3DColorARGB(Alpha, 157, 242, 242)
    Case ColorType.BrightRed
        DX8Colour = D3DColorARGB(Alpha, 255, 0, 0)
    Case ColorType.Pink
        DX8Colour = D3DColorARGB(Alpha, 255, 118, 221)
    Case ColorType.Yellow
        DX8Colour = D3DColorARGB(Alpha, 255, 255, 0)
    Case ColorType.White
        DX8Colour = D3DColorARGB(Alpha, 255, 255, 255)
    Case ColorType.DarkBrown
        DX8Colour = D3DColorARGB(Alpha, 98, 84, 52)
    Case ColorType.Gold
        DX8Colour = D3DColorARGB(Alpha, 255, 215, 0)
    Case ColorType.LightGreen
        DX8Colour = D3DColorARGB(Alpha, 124, 205, 80)
    Case ColorType.Purple
        DX8Colour = D3DColorARGB(Alpha, 125, 38, 205)
    Case ColorType.Coral
        DX8Colour = D3DColorARGB(Alpha, 255, 127, 80)
    Case ColorType.Orange
        DX8Colour = D3DColorARGB(Alpha, 255, 84, 0)
    Case ColorType.DeepSkyBlue
        DX8Colour = D3DColorARGB(Alpha, 0, 210, 255)
    Case ColorType.HealGreen
        DX8Colour = D3DColorARGB(Alpha, 76, 242, 25)
    End Select
End Function


