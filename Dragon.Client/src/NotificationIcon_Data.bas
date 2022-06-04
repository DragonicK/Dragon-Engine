Attribute VB_Name = "NotificationIcon_Data"
Option Explicit

Public MaximumNotificationIcons As Long

Public NotificationIcon() As NotificationIconRec

Public Enum NotificationIconType
    NotificationIconType_Item
    NotificationIconType_Skill
    NotificationIconType_Custom
End Enum

Public Type NotificationIconRec
    Id As Long
    Name As String
    Description As String
    IconId As Long
    IconType As NotificationIconType
End Type

Public Function GetNotificationIcon(ByVal Id As Long) As NotificationIconRec
    Dim i As Long

    If MaximumNotificationIcons > 0 Then
        For i = 1 To MaximumNotificationIcons
            If NotificationIcon(i).Id = Id Then
                GetNotificationIcon = NotificationIcon(i)
                Exit Function
            End If
        Next
    End If

End Function

Public Sub LoadNotificationIcons()
    Dim Index As Long
    Dim i As Long
    Index = GetFileHandler(App.Path & "\Data Files\Data\Icons.dat")

    If Index > 0 Then
        MaximumNotificationIcons = ReadInt32(Index)

        If MaximumNotificationIcons > 0 Then
            ReDim NotificationIcon(1 To MaximumNotificationIcons)

            For i = 1 To MaximumNotificationIcons

                With NotificationIcon(i)
                    .Id = ReadInt32(Index)
                    .Name = String(512, vbNullChar)
                    .Description = String(512, vbNullChar)

                    Call ReadString(Index, .Name)
                    Call ReadString(Index, .Description)

                    .IconId = ReadInt32(Index)
                    .IconType = ReadInt32(Index)

                    .Name = Replace$(.Name, vbNullChar, vbNullString)
                    .Description = Replace$(.Description, vbNullChar, vbNullString)

                End With
            Next
        End If
    End If

    Call CloseFileHandler(Index)
End Sub

