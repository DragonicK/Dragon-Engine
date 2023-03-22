Attribute VB_Name = "Equipment_Data"
Public MaximumEquipments As Long
Public MaxEquipmentAttributes As Long
Public MaxEquipmentUpgrades As Long

Public Equipment() As EquipmentRec
Public EquipmentAttributes() As AttributesRec
Public EquipmentEnhancements() As AttributesRec

Public Enum HandStyle
    HandStyle_None
    HandStyle_OneHanded
    HandStyle_TwoHanded
End Enum

Public Enum Equipments
    Weapon = 1
    Shield
    Helmet
    Armor
    Shoulder
    Belt
    Gloves
    Pants
    Boot
    Necklace
    Earring
    Ring
    Bracelet
    Costume
    ' Make sure Equipment_Count is below everything else
    Equipment_Count
End Enum

Public Type EquipmentSkillRec
    Id As Long
    Level As Long
    UnlockAtLevel As Long
End Type

Public Type EquipmentAttributeRec
    AttributeId As Long
    Chance As Integer
End Type

Public Type EquipmentRec
    Id As Long
    Type As Equipments
    HandStyle As HandStyle
    Proficiency As Byte
    Rarity As Byte
    EquipmentSetId As Long
    DisassembleId As Long
    MaxSockets As Long
    BaseAttackSpeed As Long
    AttackAnimationId As Long
    ModelId As Long

    UpgradeId As Long
    
    MaximumSkills As Long
    Skills() As EquipmentSkillRec
    MaxAttributes As Long
    Attributes() As EquipmentAttributeRec
End Type

Public Function GetEquipmentData(ByVal Id As Long) As EquipmentRec
    GetEquipmentData = Equipment(Id)
End Function

Public Sub LoadEquipments()
    Dim Index As Long
    Dim i As Long
    Dim Name As String
    Dim Description As String

    If Not FileExist(App.Path & "\Data Files\Data\Equipments.dat") Then
        MsgBox ("\Data Files\Data\Equipments not found.")

        Exit Sub
    End If

    Index = GetFileHandler(App.Path & "\Data Files\Data\Equipments.dat")

    If Index = 0 Then
        Dim n As Long

        MaximumEquipments = ReadInt32()

        If MaximumEquipments > 0 Then
            ReDim Equipment(1 To MaximumEquipments)

            For i = 1 To MaximumEquipments
                Id = ReadInt32()

                Name = String(255, vbNullChar)

                Call ReadString(Name)

                Equipment(i).Type = ReadInt32()
                Equipment(i).HandStyle = ReadInt32()
                Equipment(i).Proficiency = ReadInt32()
                Equipment(i).ModelId = ReadInt32()
                Equipment(i).UpgradeId = ReadInt32()
                Equipment(i).EquipmentSetId = ReadInt32()
                Equipment(i).DisassembleId = ReadInt32()
                Equipment(i).MaxSockets = ReadInt32()
                Equipment(i).BaseAttackSpeed = ReadInt32()
                Equipment(i).AttackAnimationId = ReadInt32()

                Equipment(i).MaximumSkills = ReadInt32()

                If Equipment(i).MaximumSkills > 0 Then
                    ReDim Equipment(i).Skills(1 To Equipment(i).MaximumSkills)

                    For n = 1 To Equipment(i).MaximumSkills
                        Equipment(i).Skills(n).Id = ReadInt32()
                        Equipment(i).Skills(n).Level = ReadInt32()
                        Equipment(i).Skills(n).UnlockAtLevel = ReadInt32()
                    Next
                End If

                Equipment(i).MaxAttributes = ReadInt32()

                If Equipment(i).MaxAttributes > 0 Then
                    ReDim Equipment(i).Attributes(1 To Equipment(i).MaxAttributes)

                    For n = 1 To Equipment(i).MaxAttributes
                        Equipment(i).Attributes(n).AttributeId = ReadInt32()
                        Equipment(i).Attributes(n).Chance = ReadInt32()
                    Next
                End If
            Next
        End If
    End If

    Call CloseFileHandler

End Sub

Public Sub LoadEquipmentAttributes()
    Call LoadAttributes(EquipmentAttributes, MaxEquipmentAttributes, "EquipmentAttributes.dat")
End Sub
Public Sub LoadEquipmentEnhancements()
    Call LoadAttributes(EquipmentEnhancements, MaxEquipmentUpgrades, "EquipmentUpgrades.dat")
End Sub
