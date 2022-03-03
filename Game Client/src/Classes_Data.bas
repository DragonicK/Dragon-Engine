Attribute VB_Name = "Classes_Data"
Option Explicit

Public Const MaximumClasses As Long = 3

Public Class(1 To MaximumClasses) As ClassRec

Private Type ClassRec
    Id As Long
    Name As String
    Description As String
    MaleSprite() As Long
    FemaleSprite() As Long
End Type

Public Sub InitializeClasses()
    Dim i As Long, n As Long
    Dim MaxSprites As Long

    MaxSprites = 5

    Class(1).Id = 1
    Class(1).Name = "Warrior"
    Class(1).Description = "The way of a warrior has never been an easy one. Skilled use of a sword is not something learnt overnight. Being able to take a decent amount of hits is important for these characters and as such they weigh a lot of importance on endurance and strength."

    Class(2).Id = 2
    Class(2).Name = "Wizard"
    Class(2).Description = "Wizards are often mistrusted characters who have mastered the practise of using their own spirit to create Element entities. Generally seen as playful and almost childish because of the huge amounts of pleasure they take from setting things on fire."

    Class(3).Id = 3
    Class(3).Name = "Priest"
    Class(3).Description = "The art of healing is one which comes with tremendous amounts of pressure and guilt. Constantly being put under high-pressure situations where their abilities could mean the difference between life and death leads many Whisperers to insanity."

    For i = 1 To MaximumClasses
        ReDim Class(i).FemaleSprite(1 To MaxSprites)
        ReDim Class(i).MaleSprite(1 To MaxSprites)

        For n = 1 To MaxSprites
            Class(i).FemaleSprite(n) = n
            Class(i).MaleSprite(n) = n
        Next
    Next

End Sub

Public Function GetClassName(ByVal Id As Long) As String
    Dim i As Long

    For i = 1 To MaximumClasses
        If Class(i).Id = Id Then
            GetClassName = Class(i).Name

            Exit Function
        End If
    Next

    GetClassName = vbNullString

End Function

