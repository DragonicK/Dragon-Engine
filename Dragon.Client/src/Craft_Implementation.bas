Attribute VB_Name = "Craft_Implementation"
Option Explicit

Public Const MaxPlayerRecipes As Long = 35
Public Const MaxRecipeRequiredItems As Long = 5

Public Craft As PlayerCraftRec

Private Type PlayerCraftRec
    Type As CraftType
    Level As Long
    Experience As Long
    NextLevelExp As Long
    Recipes(1 To MaxPlayerRecipes) As Long
End Type

Public Enum CraftType
    CraftType_None
    CraftType_Weaponsmithing
    CraftType_Leatherworking
    CraftType_Armorsmithing
    CraftType_Tailoring
    CraftType_Jewelry
    CraftType_Alchemy
    CraftType_Cooking
    CraftType_Handcraft
End Enum

Public Sub ClearRecipes()
    Dim i As Long

    For i = 1 To MaxPlayerRecipes
        Call SetCraftRecipeNum(i, 0)
    Next

End Sub

Public Sub ClearCraft()
    Call ZeroMemory(ByVal VarPtr(Craft), LenB(Craft))
    CurrentCraftName = "Nenhum"
End Sub

Public Function GetCraftName(ByVal CType As CraftType) As String
    Select Case CType
    Case CraftType.CraftType_None
        GetCraftName = "Nenhum"
    Case CraftType.CraftType_Weaponsmithing
        GetCraftName = "Armeiro"
    Case CraftType.CraftType_Armorsmithing
        GetCraftName = "Ferreiro"
    Case CraftType.CraftType_Leatherworking
        GetCraftName = "Couraria"
    Case CraftType.CraftType_Tailoring
        GetCraftName = "Alfaiataria"
    Case CraftType.CraftType_Jewelry
        GetCraftName = "Joalheria"
    Case CraftType.CraftType_Alchemy
        GetCraftName = "Alquimia"
    Case CraftType.CraftType_Cooking
        GetCraftName = "Culinária"
    Case CraftType.CraftType_Handcraft
        GetCraftName = "Artesanato"
    End Select
End Function

Public Function GetCraftType() As CraftType
    GetCraftType = Craft.Type
End Function
Public Sub SetCraftType(ByVal CType As CraftType)
    Craft.Type = CType
End Sub
Public Function GetCraftLevel() As Long
    GetCraftLevel = Craft.Level
End Function
Public Sub SetCraftLevel(ByVal Level As Long)
    Craft.Level = Level
End Sub
Public Function GetCraftExp() As Long
    GetCraftExp = Craft.Experience
End Function
Public Sub SetCraftExp(ByVal Experience As Long)
    Craft.Experience = Experience
End Sub
Public Function GetCraftNextLevelExp() As Long
    GetCraftNextLevelExp = Craft.NextLevelExp
End Function
Public Sub SetCraftNextLevelExp(ByVal Experience As Long)
    Craft.NextLevelExp = Experience
End Sub
Public Function GetCraftRecipeNum(ByVal RecipeIndex As Integer) As Long
   GetCraftRecipeNum = Craft.Recipes(RecipeIndex)
End Function
Public Sub SetCraftRecipeNum(ByVal RecipeIndex As Integer, ByVal RecipeNum As Long)
    Craft.Recipes(RecipeIndex) = RecipeNum
End Sub
