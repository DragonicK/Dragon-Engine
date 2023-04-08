Attribute VB_Name = "Chest_Data"
Option Explicit

Public Const MaximumChests As Long = 255
Public Const MaximumChestAnimationFrame = 3

Public Chests(1 To MaximumChests) As ChestRec
Public Chests_HighIndex As Long

Public Enum ChestAnimation
    ChestAnimation_Normal = 0
    ChestAnimation_Begin = 1
    ChestAnimation_Middle = 2
    ChestAnimation_Final = 3
End Enum

Private Type ChestRec
    Id As Long
    X As Long
    Y As Long
    Alpha As Long
    Sprite As Long
    AlreadyLooted As Boolean
End Type

