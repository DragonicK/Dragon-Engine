Attribute VB_Name = "Target_Data"
Option Explicit

Public MyTargetIndex As Long
Public MyTargetType As Long

Public Count_Target As Long
Public TargetTexture() As TextureStruct

' Target type constants
Public Const TargetTypeNone As Byte = 0
Public Const TargetTypePlayer As Byte = 1
Public Const TargetTypeNpc As Byte = 2
Public Const TargetTypeLoot As Byte = 3

Public Const Path_Target As String = "\Data Files\Graphics\Target\"

Public Sub LoadTarget()
    Dim Data() As Byte
    Dim Count As Long
    Dim f As Long, i As Long, File As String

    Count = CountFiles
    Count_Target = Count

    If Count > 0 Then

        ReDim TargetTexture(1 To Count)

        For i = 1 To Count
            File = App.Path & Path_Target & i & ".png"

            f = FreeFile
            Open File For Binary As #f
            ReDim Data(0 To LOF(f) - 1)
            Get #f, , Data
            Close #f

            Call LoadParallaxTexture(Data, TargetTexture(i))
        Next

    End If

End Sub

Private Function CountFiles() As Long
    Dim File As String, Count As Long

    File = Dir$(App.Path & Path_Target & "*.png")

    Do While Len(File)
        File = Dir$

        Count = Count + 1
    Loop

    CountFiles = Count

End Function
