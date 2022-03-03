Attribute VB_Name = "Aura_Data"
Option Explicit

Public MaxAuraAttributes As Long
Public AuraAttributes() As AttributesRec
Public MaxAuraEnhancements As Long
Public AuraEnhancements() As AttributesRec

Public Sub LoadAuraAttributes()
    Call LoadAttributes(AuraAttributes, MaxAuraAttributes, "AuraAttributes.dat")
End Sub

Public Sub LoadAuraEnhancements()
     Call LoadAttributes(AuraEnhancements, MaxAuraEnhancements, "AuraEnhancements.dat")
End Sub
