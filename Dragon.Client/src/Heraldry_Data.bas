Attribute VB_Name = "Heraldry_Data"
Option Explicit

Public MaxHeraldryAttributes As Long
Public HeraldryAttributes() As AttributesRec
Public MaxHeraldryEnhancements As Long
Public HeraldryEnhancements() As AttributesRec

Public Sub LoadHeraldryAttributes()
    Call LoadAttributes(HeraldryAttributes, MaxHeraldryAttributes, "HeraldryAttributes.dat")
End Sub

Public Sub LoadHeraldryEnhancements()
    Call LoadAttributes(HeraldryEnhancements, MaxHeraldryEnhancements, "HeraldryUpgrades.dat")
End Sub
