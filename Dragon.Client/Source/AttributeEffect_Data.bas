Attribute VB_Name = "AttributeEffect_Data"
Option Explicit

Public MaxAttributeEffects As Long
Public AttributeEffect() As AttributeEffectRec

Public MaxAttributeEffectAttributes As Long
Public AttributeEffectAttributes() As AttributesRec

Public MaxAttributeEffectEnhancements As Long
Public AttributeEffectEnhancements() As AttributesRec

Public Enum AttributeEffectType
    AttributeEffectType_Increase
    AttributeEffectType_Decrease
End Enum

Public Enum AttributeEffectDataType
    AttributeEffectDataType_Raw
    AttributeEffectDataType_Percentage
End Enum

Public Const BUFF_TYPE_INCREASE As Byte = 0
Public Const BUFF_TYPE_DECREASE As Byte = 1
Public Const BUFF_TYPE_DATA_RAW As Byte = 0
Public Const BUFF_TYPE_DATA_PERCENTAGE As Byte = 1

Public Type AttributeEffectRec
    Id As Long
    Name As String
    Description As String
    EffectType As AttributeEffectType
    IconId As Long
    Duration As Long
    Dispelled As Byte
    RemoveOnDeath As Byte
    Unlimited As Byte
    AttributeId As Long
    UpgradeId As Long
    Override As Long
End Type

Public Sub LoadAttributeEffects()
    Dim Index As Long
    Dim i As Long
    Dim Name As String
    Dim Description As String

    If Not FileExist(App.Path & "\Data Files\Data\Effects.dat") Then
        MsgBox ("\Data Files\Data\Effects not found.")

        Exit Sub
    End If

    Index = GetFileHandler(App.Path & "\Data Files\Data\Effects.dat")
    
    If Index = 0 Then
        MaxAttributeEffects = ReadInt32()
        If MaxAttributeEffects > 0 Then
            ReDim AttributeEffect(0 To MaxAttributeEffects)

            For i = 1 To MaxAttributeEffects
                AttributeEffect(i).Id = ReadInt32()
                
                Name = String(255, vbNullChar)
                Description = String(512, vbNullChar)
                
                Call ReadString(Name)
                Call ReadString(Description)
                
                AttributeEffect(i).Name = Replace$(Name, vbNullChar, vbNullString)
                AttributeEffect(i).Description = Replace$(Description, vbNullChar, vbNullString)
                
                AttributeEffect(i).EffectType = ReadInt32()
                AttributeEffect(i).IconId = ReadInt32()
                AttributeEffect(i).Duration = ReadInt32()
                AttributeEffect(i).Dispelled = ReadBoolean()
                AttributeEffect(i).RemoveOnDeath = ReadBoolean()
                AttributeEffect(i).Unlimited = ReadBoolean()
                AttributeEffect(i).AttributeId = ReadInt32()
                AttributeEffect(i).UpgradeId = ReadInt32()
                AttributeEffect(i).Override = ReadInt32()
            Next
        End If
    End If

    Call CloseFileHandler
 
End Sub

Public Sub LoadAttributeEffectAttributes()
   Call LoadAttributes(AttributeEffectAttributes, MaxAttributeEffectAttributes, "EffectAttributes.dat")
End Sub

Public Sub LoadAttributeEffectEnhancements()
   Call LoadAttributes(AttributeEffectEnhancements, MaxAttributeEffectEnhancements, "EffectUpgrades.dat")
End Sub
