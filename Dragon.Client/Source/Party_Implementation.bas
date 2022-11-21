Attribute VB_Name = "Party_Implementation"
Public Sub ClearParty()
    Dim i As Long

    Call ZeroMemory(ByVal VarPtr(Party), LenB(Party))

    For i = 1 To MaximumPartyMembers
        Party.Member(i).Name = vbNullString
    Next
End Sub

Public Function FindPartyCharacterIndex(ByVal Character As String) As Long
    Dim i As Long
    Dim LowerTempChar As String

    Character = LCase$(Character)

    For i = 1 To MaximumPartyMembers
        LowerTempChar = LCase$(Party.Member(i).Name)

        If LowerTempChar = Character Then
            FindPartyCharacterIndex = Party.Member(i).Index

            Exit Function
        End If
    Next

End Function




