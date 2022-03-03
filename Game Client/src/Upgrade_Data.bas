Attribute VB_Name = "Upgrade_Data"
Option Explicit

' Número do inventário onde está o item.
Public UpgradeInventoryIndex As Long

Public Const MaximumUpgradeRequirements As Long = 5

Public Upgrade As UpgradeRec

Public Type UpgradeRequirementRec
    Id As Long
    Amount As Long

    ' Cliente Side Only.
    InventoryIndex As Long
    InventoryAmount As Long
End Type

Public Type UpgradeRec
    Break As Long
    Success As Long
    Reduce As Long
    ReduceValue As Long
    CostValue As Long

    Requirement(1 To MaximumUpgradeRequirements) As UpgradeRequirementRec
End Type

