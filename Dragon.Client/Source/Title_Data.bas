Attribute VB_Name = "Title_Data"
Option Explicit

Public MaximumTitles As Long
Public MaxTitlesAttributes As Long

Public Title() As TitleRec
Public TitleAttributes() As AttributesRec

Private Type TitleRec
    Id As Long
    Name As String
    Description As String
    Rarity As Byte
    AttributeId As Long
End Type

Public Sub LoadTitles()
    Dim Index As Long
    Dim i As Long
    Dim Name As String
    Dim Description As String

    Index = GetFileHandler(App.Path & "\Data Files\Data\Titles.dat")

    If Index = 0 Then
        MaximumTitles = ReadInt32()

        If MaximumTitles > 0 Then
            ReDim Title(1 To MaximumTitles)
            
            For i = 1 To MaximumTitles
                Name = String(512, vbNullChar)
                Description = String(512, vbNullChar)
                
                Title(i).Id = ReadInt32()
                Call ReadString(Name)
                Call ReadString(Description)
                Title(i).Rarity = ReadInt32()
                Title(i).AttributeId = ReadInt32()
          
                Title(i).Name = Trim$(Replace(Name, vbNullChar, vbNullString))
                Title(i).Description = Trim$(Replace(Description, vbNullChar, vbNullString))
            Next
        End If
    End If

    Call CloseFileHandler

End Sub

Public Sub LoadTitleAttributes()
    Call LoadAttributes(TitleAttributes, MaxTitlesAttributes, "TitleAttributes.dat")
End Sub


