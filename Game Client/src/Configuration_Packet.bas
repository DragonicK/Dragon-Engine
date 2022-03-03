Attribute VB_Name = "Configuration_Packet"
Option Explicit

Public Sub HandlePlayerConfiguration(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteBytes Data

    Dim MaxAuras, MaxHeraldries, MaxInventories, MaxLevel As Long
    Dim MaxRecipes, MaximumSkills, MaxSpellBuffs, MaxWarehouse As Long

    MaxAuras = Buffer.ReadLong()
    MaxHeraldries = Buffer.ReadLong()
    MaxInventories = Buffer.ReadLong()
    MaxLevel = Buffer.ReadLong()
    MaxRecipes = Buffer.ReadLong()
    MaximumSkills = Buffer.ReadLong()
    MaxSpellBuffs = Buffer.ReadLong()
    MaxPlayerTitles = Buffer.ReadLong()
    MaxWarehouse = Buffer.ReadLong()
    MaxPlayerMail = Buffer.ReadLong
    
    Call Buffer.Flush
    Set Buffer = Nothing
       
    Call InitializePlayerTitles
    Call InitializePlayerMails
End Sub
