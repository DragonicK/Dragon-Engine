Attribute VB_Name = "NotificationIcon_Window"
Option Explicit

Public Sub ShowNotificationIconDescription(ByRef X As Long, ByRef Y As Long, ByRef Notification As NotificationIconRec)
    Dim Colour As Long, i As Long
    Dim WindowIndex As Long
    Dim CurrentHeight As Long
    Dim ControlType As Long
        
    ' set globals
    DescType = 6   ' currency
    DescItem = Notification.Id

    WindowIndex = GetWindowIndex("winDescription")
    CurrentHeight = Windows(WindowIndex).Window.Height

    ' set position
    Windows(WindowIndex).Window.Left = X
    Windows(WindowIndex).Window.Top = Y
    Windows(WindowIndex).Window.Width = 225

    ' show the window
    ShowWindow WindowIndex, , False

    ' exit out early if last is same
    If (DescItem = DescLastItem) And (DescType = DescLastType) Then
        Exit Sub
    End If
    
    ' set last to this
    DescLastType = DescType
    DescLastItem = DescItem

    ControlType = GetControlIndex("winDescription", "lblType")
    Windows(WindowIndex).Controls(ControlType).Visible = True

    ' set variables
    Dim ItemName As String
    Dim ControlIndex As Long

    With Windows(WindowIndex)
        ControlIndex = GetControlIndex("winDescription", "lblName")
        .Controls(ControlIndex).Text = Notification.Name
        .Controls(ControlIndex).textColour = Gold
        .Controls(ControlIndex).Width = 225
        .Controls(ControlIndex).align = Alignment.alignCentre

        .Controls(ControlType).Text = Notification.Description
        .Controls(ControlType).textColour = White
        .Controls(ControlType).align = Alignment.alignCentre
        .Controls(ControlType).Width = 225
    End With

    ReDim DescText(1 To 1) As TextColourRec

    ControlIndex = GetControlIndex("winDescription", "lblDesc")
    Windows(WindowIndex).Controls(ControlIndex).Width = 205

    Windows(WindowIndex).Window.Height = 150
    Windows(WindowIndex).Controls(ControlIndex).Visible = False

    ControlIndex = GetControlIndex("winDescription", "lblPrice")
    Windows(WindowIndex).Controls(ControlIndex).Visible = False

End Sub

