Attribute VB_Name = "Attributes_Data"
Option Explicit

Public Type AttributeRec
    Value As Single
    Percentage As Boolean
End Type

Public Type AttributesRec
    Id As Long
    Vital(1 To Vitals.Vital_Count - 1) As AttributeRec
    Stat(1 To Stats.Stat_Count - 1) As AttributeRec

    Attack As AttributeRec
    Defense As AttributeRec
    Accuracy As AttributeRec
    Evasion As AttributeRec
    Parry As AttributeRec
    Block As AttributeRec

    MagicAttack As AttributeRec
    MagicDefense As AttributeRec
    MagicAccuracy As AttributeRec
    MagicResist As AttributeRec

    Concentration As AttributeRec

    CritRate As Single
    CritDamage As Single
    ResistCritRate As Single
    ResistCritDamage As Single

    PveAttack As Single
    PveDefense As Single
    PvpAttack As Single
    PvpDefense As Single

    HealingPower As Single
    FinalDamage As Single
    Amplification As Single
    Enmity As Single
    DamageSuppression As Single
    AttackSpeed As Single
    CastSpeed As Single

    SilenceResistance As AttributeRec
    BlindResistance As AttributeRec
    StumbleResistance As AttributeRec
    StunResistance As AttributeRec

    ElementAttack(0 To Elements.Element_Count - 1) As AttributeRec
    ElementDefense(0 To Elements.Element_Count - 1) As AttributeRec
End Type

Public Sub LoadAttributes(ByRef Attributes() As AttributesRec, ByRef MaxAttributes As Long, ByVal FileName As String)
    Dim Index As Long
    Dim i As Long
    Dim n As Long
    Dim Name As String
    Dim Description As String

    Index = GetFileHandler(App.Path & "\Data Files\Data\" & FileName)

    If Index > 0 Then
        MaxAttributes = ReadInt32(Index)

        If MaxAttributes > 0 Then
            ReDim Attributes(0 To MaxAttributes)

            For i = 1 To MaxAttributes
                Name = String(256, vbNullChar)
                Description = String(256, vbNullChar)

                Attributes(i).Id = ReadInt32(Index)
                Call ReadString(Index, Name)
                Call ReadString(Index, Description)

                For n = 1 To Vitals.Vital_Count - 1
                    Attributes(i).Vital(n).Value = ReadSingle(Index)
                    Attributes(i).Vital(n).Percentage = ReadBoolean(Index)
                Next

                For n = 1 To Stats.Stat_Count - 1
                    Attributes(i).Stat(n).Value = ReadSingle(Index)
                    Attributes(i).Stat(n).Percentage = ReadBoolean(Index)
                Next

                Attributes(i).Attack.Value = ReadSingle(Index)
                Attributes(i).Attack.Percentage = ReadBoolean(Index)

                Attributes(i).Defense.Value = ReadSingle(Index)
                Attributes(i).Defense.Percentage = ReadBoolean(Index)

                Attributes(i).Accuracy.Value = ReadSingle(Index)
                Attributes(i).Accuracy.Percentage = ReadBoolean(Index)

                Attributes(i).Evasion.Value = ReadSingle(Index)
                Attributes(i).Evasion.Percentage = ReadBoolean(Index)

                Attributes(i).Parry.Value = ReadSingle(Index)
                Attributes(i).Parry.Percentage = ReadBoolean(Index)

                Attributes(i).Block.Value = ReadSingle(Index)
                Attributes(i).Block.Percentage = ReadBoolean(Index)

                Attributes(i).MagicAttack.Value = ReadSingle(Index)
                Attributes(i).MagicAttack.Percentage = ReadBoolean(Index)

                Attributes(i).MagicDefense.Value = ReadSingle(Index)
                Attributes(i).MagicDefense.Percentage = ReadBoolean(Index)

                Attributes(i).MagicAccuracy.Value = ReadSingle(Index)
                Attributes(i).MagicAccuracy.Percentage = ReadBoolean(Index)

                Attributes(i).MagicResist.Value = ReadSingle(Index)
                Attributes(i).MagicResist.Percentage = ReadBoolean(Index)

                Attributes(i).Concentration.Value = ReadSingle(Index)
                Attributes(i).Concentration.Percentage = ReadBoolean(Index)

                Attributes(i).SilenceResistance.Value = ReadSingle(Index)
                Attributes(i).SilenceResistance.Percentage = ReadBoolean(Index)

                Attributes(i).BlindResistance.Value = ReadSingle(Index)
                Attributes(i).BlindResistance.Percentage = ReadBoolean(Index)

                Attributes(i).StunResistance.Value = ReadSingle(Index)
                Attributes(i).StunResistance.Percentage = ReadBoolean(Index)

                Attributes(i).StumbleResistance.Value = ReadSingle(Index)
                Attributes(i).StumbleResistance.Percentage = ReadBoolean(Index)

                Attributes(i).CritRate = ReadSingle(Index)
                Attributes(i).CritDamage = ReadSingle(Index)
                Attributes(i).ResistCritRate = ReadSingle(Index)
                Attributes(i).ResistCritDamage = ReadSingle(Index)

                Attributes(i).HealingPower = ReadSingle(Index)
                Attributes(i).FinalDamage = ReadSingle(Index)
                Attributes(i).Amplification = ReadSingle(Index)
                Attributes(i).Enmity = ReadSingle(Index)
                Attributes(i).DamageSuppression = ReadSingle(Index)
                Attributes(i).AttackSpeed = ReadSingle(Index)
                Attributes(i).CastSpeed = ReadSingle(Index)
                Attributes(i).PveAttack = ReadSingle(Index)
                Attributes(i).PveDefense = ReadSingle(Index)
                Attributes(i).PvpAttack = ReadSingle(Index)
                Attributes(i).PvpDefense = ReadSingle(Index)

                For n = 0 To Elements.Element_Count - 1
                    Attributes(i).ElementAttack(n).Value = ReadSingle(Index)
                    Attributes(i).ElementAttack(n).Percentage = ReadBoolean(Index)

                    Attributes(i).ElementDefense(n).Value = ReadSingle(Index)
                    Attributes(i).ElementDefense(n).Percentage = ReadBoolean(Index)
                Next
            Next
        End If
    End If

    Call CloseFileHandler(Index)

End Sub
