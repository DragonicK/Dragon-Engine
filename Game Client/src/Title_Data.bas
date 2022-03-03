Attribute VB_Name = "Title_Data"
Option Explicit

Public MaxTitles As Long
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

    If Index > 0 Then
        MaxTitles = ReadInt32(Index)

        If MaxTitles > 0 Then
            ReDim Title(1 To MaxTitles)
            
            For i = 1 To MaxTitles
                Name = String(512, vbNullChar)
                Description = String(512, vbNullChar)
                
                Title(i).Id = ReadInt32(Index)
                Call ReadString(Index, Name)
                Call ReadString(Index, Description)
                Title(i).Rarity = ReadInt32(Index)
                Title(i).AttributeId = ReadInt32(Index)
          
                Title(i).Name = Trim$(Replace(Name, vbNullChar, vbNullString))
                Title(i).Description = Trim$(Replace(Description, vbNullChar, vbNullString))
            Next
        End If
    End If

    Call CloseFileHandler(Index)

End Sub

Public Sub LoadTitleAttributes()
    Call LoadAttributes(TitleAttributes, MaxTitlesAttributes, "TitleAttributes.dat")
End Sub


