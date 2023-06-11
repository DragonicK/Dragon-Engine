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
    Dim ReadBool As Long

    If Not FileExist(App.Path & "\Data Files\Data\Titles.dat") Then
        MsgBox ("\Data Files\Data\" & FileName & " not found.")

        Exit Sub
    End If

    Index = GetFileHandler(App.Path & "\Data Files\Data\" & FileName)

    If Index = 0 Then
        MaxAttributes = ReadInt32()

        If MaxAttributes > 0 Then
            ReDim Attributes(0 To MaxAttributes)

            For i = 1 To MaxAttributes
                Name = String(256, vbNullChar)
                Description = String(256, vbNullChar)

                Attributes(i).Id = ReadInt32()
                Call ReadString(Name)
                Call ReadString(Description)

                For n = 1 To Vitals.Vital_Count - 1
                    Attributes(i).Vital(n).Value = ReadSingle()
                    Attributes(i).Vital(n).Percentage = IIf(ReadBoolean() = 1, True, False)
                Next

                For n = 1 To Stats.Stat_Count - 1
                    Attributes(i).Stat(n).Value = ReadSingle()
                    Attributes(i).Stat(n).Percentage = IIf(ReadBoolean() = 1, True, False)
                Next

                Attributes(i).Attack.Value = ReadSingle()
                Attributes(i).Attack.Percentage = IIf(ReadBoolean() = 1, True, False)

                Attributes(i).Defense.Value = ReadSingle()
                Attributes(i).Defense.Percentage = IIf(ReadBoolean() = 1, True, False)

                Attributes(i).Accuracy.Value = ReadSingle()
                Attributes(i).Accuracy.Percentage = IIf(ReadBoolean() = 1, True, False)

                Attributes(i).Evasion.Value = ReadSingle()
                Attributes(i).Evasion.Percentage = IIf(ReadBoolean() = 1, True, False)

                Attributes(i).Parry.Value = ReadSingle()
                Attributes(i).Parry.Percentage = IIf(ReadBoolean() = 1, True, False)

                Attributes(i).Block.Value = ReadSingle()
                Attributes(i).Block.Percentage = IIf(ReadBoolean() = 1, True, False)

                Attributes(i).MagicAttack.Value = ReadSingle()
                Attributes(i).MagicAttack.Percentage = IIf(ReadBoolean() = 1, True, False)

                Attributes(i).MagicDefense.Value = ReadSingle()
                Attributes(i).MagicDefense.Percentage = IIf(ReadBoolean() = 1, True, False)

                Attributes(i).MagicAccuracy.Value = ReadSingle()
                Attributes(i).MagicAccuracy.Percentage = IIf(ReadBoolean() = 1, True, False)

                Attributes(i).MagicResist.Value = ReadSingle()
                Attributes(i).MagicResist.Percentage = IIf(ReadBoolean() = 1, True, False)

                Attributes(i).Concentration.Value = ReadSingle()
                Attributes(i).Concentration.Percentage = IIf(ReadBoolean() = 1, True, False)

                Attributes(i).SilenceResistance.Value = ReadSingle()
                Attributes(i).SilenceResistance.Percentage = IIf(ReadBoolean() = 1, True, False)

                Attributes(i).BlindResistance.Value = ReadSingle()
                Attributes(i).BlindResistance.Percentage = IIf(ReadBoolean() = 1, True, False)

                Attributes(i).StunResistance.Value = ReadSingle()
                Attributes(i).StunResistance.Percentage = IIf(ReadBoolean() = 1, True, False)

                Attributes(i).StumbleResistance.Value = ReadSingle()
                Attributes(i).StumbleResistance.Percentage = IIf(ReadBoolean() = 1, True, False)

                Attributes(i).CritRate = ReadSingle()
                Attributes(i).CritDamage = ReadSingle()
                Attributes(i).ResistCritRate = ReadSingle()
                Attributes(i).ResistCritDamage = ReadSingle()

                Attributes(i).HealingPower = ReadSingle()
                Attributes(i).FinalDamage = ReadSingle()
                Attributes(i).Amplification = ReadSingle()
                Attributes(i).Enmity = ReadSingle()
                Attributes(i).DamageSuppression = ReadSingle()
                Attributes(i).AttackSpeed = ReadSingle()
                Attributes(i).CastSpeed = ReadSingle()
                Attributes(i).PveAttack = ReadSingle()
                Attributes(i).PveDefense = ReadSingle()
                Attributes(i).PvpAttack = ReadSingle()
                Attributes(i).PvpDefense = ReadSingle()

                For n = 0 To Elements.Element_Count - 1
                    Attributes(i).ElementAttack(n).Value = ReadSingle()
                    Attributes(i).ElementAttack(n).Percentage = IIf(ReadBoolean() = 1, True, False)
                    
                    Attributes(i).ElementDefense(n).Value = ReadSingle()
                    Attributes(i).ElementDefense(n).Percentage = IIf(ReadBoolean() = 1, True, False)
                Next
            Next
        End If
    End If

    Call CloseFileHandler

End Sub
