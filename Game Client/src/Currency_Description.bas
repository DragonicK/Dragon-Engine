Attribute VB_Name = "Currency_Description"
Option Explicit

Public Sub ShowCurrencyDesc(ByRef X As Long, ByRef Y As Long, ByVal CurType As CurrencyType, ByVal CurrencyValue As Long)
    Dim Colour As Long, i As Long
    Dim WindowIndex As Long
    Dim CurrentHeight As Long
    Dim ControlType As Long

    ' set globals
    DescType = 5   ' currency
    ' Evita que a janela fique aberta.
    ' Use a quantidade de dinheiro para simular o id de um item.
    DescItem = CurrencyValue
    ' Se por acaso a quantidade estiver como zero, altera para 1.
    If CurrencyValue = 0 Then DescItem = 1

    DescLevel = 0
    DescBound = 0

    WindowIndex = GetWindowIndex("winDescription")
    CurrentHeight = Windows(WindowIndex).Window.Height

    ' set position
    Windows(WindowIndex).Window.Left = X
    Windows(WindowIndex).Window.Top = Y
    Windows(WindowIndex).Window.Width = 225

    ' show the window
    ShowWindow WindowIndex, , False

    ' exit out early if last is same
    If (DescLastType = DescType) And (DescLastItem = DescItem) Then
        Exit Sub
    End If

    ' set last to this
    DescLastType = DescType
    DescLastItem = DescItem
    DescLastLevel = DescLevel

    ControlType = GetControlIndex("winDescription", "lblType")
    Windows(WindowIndex).Controls(ControlType).Visible = True

    ' set variables
    Dim ItemName As String
    Dim ControlIndex As Long

    With Windows(WindowIndex)
        ControlIndex = GetControlIndex("winDescription", "lblName")
        .Controls(ControlIndex).Text = GetCurrencyData(CurType).Name
        .Controls(ControlIndex).textColour = Gold
        .Controls(ControlIndex).Width = 225
        .Controls(ControlIndex).align = Alignment.alignCentre

        .Controls(ControlType).Text = "Quantidade: " & CurrencyValue
        .Controls(ControlType).textColour = White
        .Controls(ControlType).align = Alignment.alignCentre
        .Controls(ControlType).Width = 225
    End With

    ReDim DescText(1 To 1) As TextColourRec

    ControlIndex = GetControlIndex("winDescription", "lblDesc")
    Windows(WindowIndex).Controls(ControlIndex).Width = 205

    Windows(WindowIndex).Window.Height = 50
    Windows(WindowIndex).Controls(ControlIndex).Visible = False

    ControlIndex = GetControlIndex("winDescription", "lblPrice")
    Windows(WindowIndex).Controls(ControlIndex).Visible = False

End Sub
