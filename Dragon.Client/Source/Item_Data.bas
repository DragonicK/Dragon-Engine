Attribute VB_Name = "Item_Data"
Option Explicit

Public MaximumItems As Long
Public Item() As ItemRec

Public Enum ItemBoundType
    ItemBoundType_None
    ItemBoundType_Obtained
    ItemBoundType_Equiped
End Enum

Public Enum InventoryBoundType
    InventoryBoundType_None
    InventoryBoundType_Bound
End Enum

Public Enum RarityType
    RarityType_Common
    RarityType_Uncommon
    RarityType_Rare
    RarityType_Epic
    RarityType_Mythic
    RarityType_Ancient
    RarityType_Legendary
    RarityType_Ethereal
    RarityType_Count
End Enum

Enum ItemType
    ItemType_None
    ItemType_Equipment
    ItemType_Key
    ItemType_Skill
    ItemType_Food
    ItemType_Consume
    ItemType_Upgrade
    ItemType_Supplement
    ItemType_Recipe
    ItemType_GashaBox
    ItemType_Quest
    ItemType_Heraldry
    ItemType_Talisman
    ItemType_Material
    ItemType_Scroll
End Enum

Public Type ItemRec
    Id As Long
    Name As String
    Description As String
    Sound As String
    IconId As Long
    Type As ItemType
    Rarity As RarityType
    Bind As ItemBoundType
    Level As Long
    Price As Long
    Stackable As Boolean
    GashaBoxId As Long
    RecipeId As Long
    EquipmentId As Long
    SkillId As Long
    MaxStack As Long
    MaximumLevel As Long
    UpgradeId As Long

    ' Consume, potions
    Cooldown As Long
    EffectId As Long
    EffectLevel As Long
    EffectDuration As Long
    
    RecoveryVital(1 To Vitals.Vital_Count - 1) As Long

    Interval As Long
    Duration As Long
    ClassId As Long
End Type

Public Sub LoadItems()
    Dim Index As Long
    Dim i As Long
    Dim Name As String
    Dim Description As String
    Dim Sound As String

    If Not FileExist(App.Path & "\Data Files\Data\Items.dat") Then
        MsgBox ("\Data Files\Data\Items not found.")

        Exit Sub
    End If

    Index = GetFileHandler(App.Path & "\Data Files\Data\Items.dat")

    If Index = 0 Then
        Dim n As Long

        MaximumItems = ReadInt32()

        If MaximumItems > 0 Then
            ReDim Item(1 To MaximumItems)

            For i = 1 To MaximumItems
                Item(i).Id = ReadInt32()

                Name = String(255, vbNullChar)
                Description = String(1024, vbNullChar)
                Sound = String(255, vbNullChar)

                Call ReadString(Name)
                Call ReadString(Description)
                Call ReadString(Sound)

                Item(i).Name = Replace$(Name, vbNullChar, vbNullString)
                Item(i).Description = Replace$(Description, vbNullChar, vbNullString)
                Item(i).Sound = Replace$(Sound, vbNullChar, vbNullString)

                Item(i).IconId = ReadInt32()
                Item(i).Type = ReadInt32()
                Item(i).Rarity = ReadInt32()
                Item(i).Bind = ReadInt32()
                Item(i).Level = ReadInt32()
                Item(i).Price = ReadInt32()

                Item(i).MaxStack = ReadInt32()
                Item(i).Stackable = Item(i).MaxStack > 0

                Item(i).GashaBoxId = ReadInt32()
                Item(i).RecipeId = ReadInt32()
                Item(i).EquipmentId = ReadInt32()
                Item(i).SkillId = ReadInt32()
                Item(i).Cooldown = ReadInt32()
                Item(i).Interval = ReadInt32()
                Item(i).Duration = ReadInt32()
                Item(i).EffectId = ReadInt32()
                Item(i).EffectLevel = ReadInt32()
                Item(i).EffectDuration = ReadInt32()
                Item(i).ClassId = ReadInt32()
                Item(i).UpgradeId = ReadInt32()
                Item(i).MaximumLevel = ReadInt32()

                For n = 1 To Vitals.Vital_Count - 1
                    Item(i).RecoveryVital(n) = ReadInt32()
                Next

            Next
        End If
    End If

    Call CloseFileHandler
End Sub

