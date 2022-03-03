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

    Index = GetFileHandler(App.Path & "\Data Files\Data\Effects.dat")
    
    If Index > 0 Then
        MaxAttributeEffects = ReadInt32(Index)
        If MaxAttributeEffects > 0 Then
            ReDim AttributeEffect(0 To MaxAttributeEffects)

            For i = 1 To MaxAttributeEffects
                AttributeEffect(i).Id = ReadInt32(Index)
                
                Name = String(255, vbNullChar)
                Description = String(512, vbNullChar)
                
                Call ReadString(Index, Name)
                Call ReadString(Index, Description)
                
                AttributeEffect(i).Name = Replace$(Name, vbNullChar, vbNullString)
                AttributeEffect(i).Description = Replace$(Description, vbNullChar, vbNullString)
                
                AttributeEffect(i).EffectType = ReadInt32(Index)
                AttributeEffect(i).IconId = ReadInt32(Index)
                AttributeEffect(i).Duration = ReadInt32(Index)
                AttributeEffect(i).Dispelled = ReadBoolean(Index)
                AttributeEffect(i).RemoveOnDeath = ReadBoolean(Index)
                AttributeEffect(i).Unlimited = ReadBoolean(Index)
                AttributeEffect(i).AttributeId = ReadInt32(Index)
                AttributeEffect(i).UpgradeId = ReadInt32(Index)
                AttributeEffect(i).Override = ReadInt32(Index)
            Next
        End If
    End If

    Call CloseFileHandler(Index)
 
End Sub

Public Sub LoadAttributeEffectAttributes()
   Call LoadAttributes(AttributeEffectAttributes, MaxAttributeEffectAttributes, "EffectAttributes.dat")
End Sub

Public Sub LoadAttributeEffectEnhancements()
   Call LoadAttributes(AttributeEffectEnhancements, MaxAttributeEffectEnhancements, "EffectUpgrades.dat")
End Sub
