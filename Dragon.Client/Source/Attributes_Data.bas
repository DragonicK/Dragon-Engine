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
                    Attributes(i).Vital(n).Percentage = ReadBoolean()
                Next

                For n = 1 To Stats.Stat_Count - 1
                    Attributes(i).Stat(n).Value = ReadSingle()
                    Attributes(i).Stat(n).Percentage = ReadBoolean()
                Next

                Attributes(i).Attack.Value = ReadSingle()
                Attributes(i).Attack.Percentage = ReadBoolean()

                Attributes(i).Defense.Value = ReadSingle()
                Attributes(i).Defense.Percentage = ReadBoolean()

                Attributes(i).Accuracy.Value = ReadSingle()
                Attributes(i).Accuracy.Percentage = ReadBoolean()

                Attributes(i).Evasion.Value = ReadSingle()
                Attributes(i).Evasion.Percentage = ReadBoolean()

                Attributes(i).Parry.Value = ReadSingle()
                Attributes(i).Parry.Percentage = ReadBoolean()

                Attributes(i).Block.Value = ReadSingle()
                Attributes(i).Block.Percentage = ReadBoolean()

                Attributes(i).MagicAttack.Value = ReadSingle()
                Attributes(i).MagicAttack.Percentage = ReadBoolean()

                Attributes(i).MagicDefense.Value = ReadSingle()
                Attributes(i).MagicDefense.Percentage = ReadBoolean()

                Attributes(i).MagicAccuracy.Value = ReadSingle()
                Attributes(i).MagicAccuracy.Percentage = ReadBoolean()

                Attributes(i).MagicResist.Value = ReadSingle()
                Attributes(i).MagicResist.Percentage = ReadBoolean()

                Attributes(i).Concentration.Value = ReadSingle()
                Attributes(i).Concentration.Percentage = ReadBoolean()

                Attributes(i).SilenceResistance.Value = ReadSingle()
                Attributes(i).SilenceResistance.Percentage = ReadBoolean()

                Attributes(i).BlindResistance.Value = ReadSingle()
                Attributes(i).BlindResistance.Percentage = ReadBoolean()

                Attributes(i).StunResistance.Value = ReadSingle()
                Attributes(i).StunResistance.Percentage = ReadBoolean()

                Attributes(i).StumbleResistance.Value = ReadSingle()
                Attributes(i).StumbleResistance.Percentage = ReadBoolean()

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
                    Attributes(i).ElementAttack(n).Percentage = ReadBoolean()

                    Attributes(i).ElementDefense(n).Value = ReadSingle()
                    Attributes(i).ElementDefense(n).Percentage = ReadBoolean()
                Next
            Next
        End If
    End If

    Call CloseFileHandler

End Sub
