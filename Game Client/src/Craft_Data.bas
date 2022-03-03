Attribute VB_Name = "Craft_Data"
Option Explicit

Public MaxRecipes As Long
Public Recipe() As RecipeRec

Public Type RecipeRec
    Id As Long
    Name As String
    Description As String
    Type As Byte
    Level As Long
    Experience As Long
    RequiredItem(1 To MaxRecipeRequiredItems) As InventoryRec
    RewardItem As InventoryRec
End Type

Public Sub LoadRecipes()
    Dim Index As Long
    Dim i As Long
    Dim Name As String
    Dim Description As String

    Index = GetFileHandler(App.Path & "\Data Files\Data\Recipes.dat")

    If Index > 0 Then
        Dim n As Long, Count As Long
        Dim Id As Long
        Dim Value As Long
        Dim Level As Long
        Dim Bound As Boolean

        MaxRecipes = ReadInt32(Index)

        If MaxRecipes > 0 Then
            ReDim Recipe(1 To MaxRecipes)

            For i = 1 To MaxRecipes
                Recipe(i).Id = ReadInt32(Index)

                Name = String(255, vbNullChar)
                Description = String(1024, vbNullChar)

                Call ReadString(Index, Name)
                Call ReadString(Index, Description)

                Recipe(i).Name = Replace$(Name, vbNullChar, vbNullString)
                Recipe(i).Description = Replace$(Description, vbNullChar, vbNullString)
                Recipe(i).Type = ReadInt32(Index)
                Recipe(i).Level = ReadInt32(Index)
                Recipe(i).Experience = ReadInt32(Index)

                Recipe(i).RewardItem.Num = ReadInt32(Index)
                Recipe(i).RewardItem.Value = ReadInt32(Index)
                Recipe(i).RewardItem.Level = ReadInt32(Index)
                Recipe(i).RewardItem.Bound = ReadBoolean(Index)

                Count = ReadInt32(Index)

                For n = 1 To Count
                    Id = ReadInt32(Index)
                    Value = ReadInt32(Index)
                    Level = ReadInt32(Index)
                    Bound = ReadBoolean(Index)

                    If Count <= MaxRecipeRequiredItems Then
                        Recipe(i).RequiredItem(n).Num = Id
                        Recipe(i).RequiredItem(n).Value = Value
                        Recipe(i).RequiredItem(n).Level = Level
                    End If
                Next

            Next
        End If
    End If

    Call CloseFileHandler(Index)

End Sub



