Attribute VB_Name = "Currency_Data"
Option Explicit

' Moedas e Tokens
Public Enum CurrencyType
    Currency_Gold
End Enum

' Quantidade máxima de moedas ou tokens na conta do usuário.
Private Const MaximumCurrency As Long = 100

Private PlayerCurrency(0 To MaximumCurrency) As PlayerCurrencyRec
Private CurrencyData(0 To MaximumCurrency) As CurrencyRec

Private Type PlayerCurrencyRec
    Id As Long
    Value As Long
End Type

Public Type CurrencyRec
    Id As CurrencyType
    Name As String
    IconId As Long
End Type

Public Sub InitializeCurrency()
    Dim i As Long
    
    CurrencyData(0).Id = CurrencyType.Currency_Gold
    CurrencyData(0).IconId = 19
    CurrencyData(0).Name = "Ouro"
End Sub

Public Function GetCurrencyData(ByVal Id As Long) As CurrencyRec
    Dim i As Long

    For i = 0 To MaximumCurrency
        If CurrencyData(i).Id = Id Then
            GetCurrencyData = CurrencyData(i)
            
            Exit For
        End If
    Next

End Function

Public Sub ClearPlayerCurrency()
    Dim i As Long

    For i = 0 To MaximumCurrency
        PlayerCurrency(i).Id = 0
        PlayerCurrency(i).Value = 0
    Next
    
End Sub

Public Function GetPlayerCurrency(ByVal Id As CurrencyType) As Long
    Dim i As Long

    For i = 0 To MaximumCurrency
        If PlayerCurrency(i).Id = Id Then
            GetPlayerCurrency = PlayerCurrency(i).Value
            
            Exit For
        End If
    Next

End Function

Public Function SetPlayerCurrency(ByVal Id As CurrencyType, ByVal Value As Long)
    Dim i As Long

    For i = 0 To MaximumCurrency
        If PlayerCurrency(i).Id = Id Then
            PlayerCurrency(i).Value = Value
            
            Exit Function
        End If
    Next

    Call AddCurrency(Id, Value)

End Function

Private Sub AddCurrency(ByVal Id As CurrencyType, ByVal Value As Long)
    Dim i As Long

    For i = 0 To MaximumCurrency
        If PlayerCurrency(i).Id = 0 Then
            PlayerCurrency(i).Id = Id
            PlayerCurrency(i).Value = Value

            Exit For
        End If
    Next
End Sub

