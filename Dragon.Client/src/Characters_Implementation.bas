Attribute VB_Name = "Characters_Implementation"
Option Explicit

' Processa e exibe o tempo restante para exclusão do personagem.
Public Sub ProcessCharacterDeleteDate()
    If Not inMenu Then
        Exit Sub
    End If

    Dim i As Long
    Dim WindowIndex As Long
    Dim ControlIndex As Long
    Dim Hour As Byte, Minutes As Byte, Seconds As Byte
    Dim DateString As String

    WindowIndex = GetWindowIndex("winModels")

    For i = 1 To MAX_CHARS
        If CharPendingExclusion(i) Then

            Hour = Val(Format(CharDate(i), "hh"))
            Minutes = Val(Format(CharDate(i), "nn"))
            Seconds = Val(Format(CharDate(i), "ss"))

            If Hour = 0 And Minutes = 0 And Seconds = 0 Then
                CharPendingExclusion(i) = False
                DateString = "Processando"
                CharEnabled(i) = False
            Else
                CharDate(i) = DateAdd("s", -1, CharDate(i))
                DateString = Format(CharDate(i), "hh:mm:ss")
                CharEnabled(i) = True
            End If

            ControlIndex = GetControlIndex("winModels", "lblCharDate_" & i)
            Windows(WindowIndex).Controls(ControlIndex).Text = DateString
        Else
            CharEnabled(i) = True
        End If
    Next

End Sub
