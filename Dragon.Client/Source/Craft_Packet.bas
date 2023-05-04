Attribute VB_Name = "Craft_Packet"
Option Explicit

Public Sub SendCraftItem(ByVal RecipeIndex As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PStartCraft
    Buffer.WriteLong RecipeIndex
    
    SendGameMessage Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendCraftProcessCompleted()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    
    Buffer.WriteLong EnginePacket.PCompletedCraft
    
    SendGameMessage Buffer.ToArray()
    
    Set Buffer = Nothing
End Sub

Public Sub SendCraftStopProcess()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    Buffer.WriteLong EnginePacket.PStopCraft
    SendGameMessage Buffer.ToArray()
    Set Buffer = Nothing
End Sub

Public Sub SendRemoveCraft()
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    Buffer.WriteLong EnginePacket.PDeleteCraft
    SendGameMessage Buffer.ToArray()
    Set Buffer = Nothing
End Sub

Public Sub HandleRecipes(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim MaxRecipes As Long
    Dim Buffer As clsBuffer
    Dim i As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    Call ClearRecipes

    MaxRecipes = Buffer.ReadLong

    For i = 1 To MaxRecipes
        If i <= MaxPlayerRecipes Then
            Call SetCraftRecipeNum(i, Buffer.ReadLong)
        End If
    Next

    Set Buffer = Nothing

End Sub

Public Sub HandleAddRecipe(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Dim i As Long

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    For i = 1 To MaxPlayerRecipes
        If GetCraftRecipeNum(i) = 0 Then
            Call SetCraftRecipeNum(i, Buffer.ReadLong)
            Exit For
        End If
    Next

    Set Buffer = Nothing
End Sub

Public Sub HandleCraftData(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data
    
    Call SetCraftType(Buffer.ReadLong)
    Call SetCraftLevel(Buffer.ReadLong)
    Call SetCraftExp(Buffer.ReadLong)
    Call SetCraftNextLevelExp(Buffer.ReadLong)

    Set Buffer = Nothing

    Call UpdateCraftWindow
End Sub

Public Sub HandleCraftClear(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Call ClearRecipes
    
    ClearCraft
    ClearCraftWindow
    
    Call UpdateCraftWindow
End Sub

Public Sub HandleCraftExperience(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    Call SetCraftExp(Buffer.ReadLong)
    Call SetCraftNextLevelExp(Buffer.ReadLong)

    Set Buffer = Nothing

    Call UpdateCraftWindow
End Sub

Public Sub HandleCraftStartProgressBar(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer

    Set Buffer = New clsBuffer
    Buffer.WriteBytes Data

    Call StartCraft(Buffer.ReadLong)

    Set Buffer = Nothing
End Sub

Public Sub HandleCraftOpen(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    ButtonMenu_Craft
    CanMoveNow = False
End Sub

