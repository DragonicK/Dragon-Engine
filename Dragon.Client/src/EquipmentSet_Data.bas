Attribute VB_Name = "EquipmentSet_Data"
Option Explicit

Public MaxEquipmentSets As Long
Public EquipmentSet() As EquipmentSetRec
Public MaxEquipmentSetAttributes As Long
Public EquipmentSetAttributes() As AttributesRec

Public Const MaxPlayerEquipmentSet As Byte = 15

Public Enum EquipmentSetCount
    EquipmentSetCount_None
    EquipmentSetCount_One
    EquipmentSetCount_Two
    EquipmentSetCount_Three
    EquipmentSetCount_Four
    EquipmentSetCount_Five
    EquipmentSetCount_Six
    EquipmentSetCount_Seven
    EquipmentSetCount_Eight
    EquipmentSetCount_Nine
    EquipmentSetCount_Ten
    EquipmentSetCount_Eleven
    EquipmentSetCount_Twelven
    EquipmentSetCount_Thirteen
    EquipmentSetCount_Fourteen
    EquipmentSetCount_Fifteen
    EquipmentSetCount_Count
End Enum

Private Type EquipmentSetEffectRec
    PieceCount As EquipmentSetCount
    AttributeId As Long
    SkillId As Long
End Type

Private Type EquipmentSetRec
    Id As Long
    MaxEffects As Long
    Effect() As EquipmentSetEffectRec
End Type

Public Sub LoadEquipmentSets()
    Dim Index As Long
    Dim i As Long
    Dim Name As String
    Dim Description As String

    Index = GetFileHandler(App.Path & "\Data Files\Data\EquipmentSets.dat")

    If Index > 0 Then
        Dim n As Long
        
        MaxEquipmentSets = ReadInt32(Index)

        If MaxEquipmentSets > 0 Then
            ReDim EquipmentSet(1 To MaxEquipmentSets)

            For i = 1 To MaxEquipmentSets
                EquipmentSet(i).Id = ReadInt32(Index)

                Name = String(255, vbNullChar)
                Description = String(1024, vbNullChar)

                Call ReadString(Index, Name)
                Call ReadString(Index, Description)

                EquipmentSet(i).MaxEffects = ReadInt32(Index)

                If EquipmentSet(i).MaxEffects > 0 Then
                    ReDim EquipmentSet(i).Effect(1 To EquipmentSet(i).MaxEffects)

                    For n = 1 To EquipmentSet(i).MaxEffects
                        EquipmentSet(i).Effect(n).PieceCount = ReadInt32(Index)
                        EquipmentSet(i).Effect(n).AttributeId = ReadInt32(Index)
                        EquipmentSet(i).Effect(n).SkillId = ReadInt32(Index)
                    Next
                End If
            Next
            
        End If
    End If

    Call CloseFileHandler(Index)

End Sub

Public Sub LoadEquipmentSetAttributes()
    Call LoadAttributes(EquipmentSetAttributes, MaxEquipmentSetAttributes, "EquipmentSetAttributes.dat")
End Sub





