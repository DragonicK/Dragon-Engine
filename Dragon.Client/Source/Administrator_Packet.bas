Attribute VB_Name = "Administrator_Packet"
Option Explicit

Private Const CommandTarget As Byte = 1

Public Sub SendCommandLevel(ByRef Commands() As String, ByVal CommandType As SuperiorCommands)
' Name = 0
' Target = 1
    Const Level As Byte = 2
    Const ParamCount As Long = 2

    If UBound(Commands) < Level Then
        Exit Sub
    End If

    If Not IsNumeric(Commands(Level)) Then
        Exit Sub
    End If

    Dim Buffer As clsBuffer
    Call CreateBuffer(Buffer)

    Buffer.WriteLong CommandType
    ' Write Param
    Buffer.WriteLong ParamCount
    Buffer.WriteString Commands(CommandTarget)
    Buffer.WriteString Commands(Level)

    Call SendCommand(Buffer)
End Sub

Public Sub SendCommandStat(ByRef Commands() As String, ByVal CommandType As SuperiorCommands)
' Name = 0
' Target = 1
    Const StatTarget As Byte = 2
    Const StatValue As Byte = 3
    Const ParamCount As Long = 4

    If UBound(Commands) < StatValue Then
        Exit Sub
    End If

    If Not IsNumeric(Commands(StatTarget)) Or Not IsNumeric(Commands(StatValue)) Then
        Exit Sub
    End If

    Dim Buffer As clsBuffer
    Call CreateBuffer(Buffer)

    Buffer.WriteLong CommandType
    ' Write Param
    Buffer.WriteLong ParamCount
    Buffer.WriteString Commands(CommandTarget)
    Buffer.WriteString Commands(StatTarget)
    Buffer.WriteString Commands(StatValue)

    Call SendCommand(Buffer)
End Sub

Public Sub SendCommandStatPoints(ByRef Commands() As String, ByVal CommandType As SuperiorCommands)
' Name = 0
' Target = 1
    Const StatPoints As Byte = 2
    Const ParamCount As Long = 3

    If UBound(Commands) < StatPoints Then
        Exit Sub
    End If

    If Not IsNumeric(Commands(StatPoints)) Then
        Exit Sub
    End If

    Dim Buffer As clsBuffer
    Call CreateBuffer(Buffer)

    Buffer.WriteLong CommandType
    ' Write Param
    Buffer.WriteLong ParamCount
    Buffer.WriteString Commands(CommandTarget)
    Buffer.WriteString Commands(StatPoints)

    If CommandType = SuperiorCommands.AddAttributePoint Then
        Buffer.WriteString 1
    Else
        Buffer.WriteString 0
    End If

    Call SendCommand(Buffer)
End Sub

Public Sub SendSetModel(ByRef Commands() As String)
' Name = 0
' Target = 1
    Const Model As Byte = 2
    Const ParamCount As Long = 2

    If UBound(Commands) < Model Then
        Exit Sub
    End If

    If Not IsNumeric(Commands(Model)) Then
        Exit Sub
    End If

    Dim Buffer As clsBuffer
    Call CreateBuffer(Buffer)

    Buffer.WriteLong SuperiorCommands.SetModel
    ' Write Param
    Buffer.WriteLong ParamCount
    Buffer.WriteString Commands(CommandTarget)
    Buffer.WriteString Commands(Model)

    Call SendCommand(Buffer)
End Sub

Public Sub SendSetClass(ByRef Commands() As String)
' Name = 0
' Target = 1
    Const Class As Byte = 2
    Const ParamCount As Long = 2

    If UBound(Commands) < Class Then
        Exit Sub
    End If

    If Not IsNumeric(Commands(Class)) Then
        Exit Sub
    End If

    Dim Buffer As clsBuffer
    Call CreateBuffer(Buffer)

    Buffer.WriteLong SuperiorCommands.SetClass
    ' Write Param
    Buffer.WriteLong ParamCount
    Buffer.WriteString Commands(CommandTarget)
    Buffer.WriteString Commands(Class)

    Call SendCommand(Buffer)
End Sub

Public Sub SendSetService(ByRef Commands() As String)
' Name = 0
' Target = 1
    Const Service As Byte = 2
    Const ParamCount As Long = 2

    If UBound(Commands) < Service Then
        Exit Sub
    End If

    If Not IsNumeric(Commands(Service)) Then
        Exit Sub
    End If

    Dim Buffer As clsBuffer
    Call CreateBuffer(Buffer)

    Buffer.WriteLong SuperiorCommands.SetService
    ' Write Param
    Buffer.WriteLong ParamCount
    Buffer.WriteString Commands(CommandTarget)
    Buffer.WriteString Commands(Service)

    Call SendCommand(Buffer)
End Sub

Public Sub SendSetActiveTitle(ByRef Commands() As String)
' Name = 0
' Target = 1
    Const Title As Byte = 2
    Const ParamCount As Long = 2

    If UBound(Commands) < Title Then
        Exit Sub
    End If

    If Not IsNumeric(Commands(Title)) Then
        Exit Sub
    End If

    Dim Buffer As clsBuffer
    Call CreateBuffer(Buffer)

    Buffer.WriteLong SuperiorCommands.SetActiveTitle
    ' Write Param
    Buffer.WriteLong ParamCount
    Buffer.WriteString Commands(CommandTarget)
    Buffer.WriteString Commands(Title)

    Call SendCommand(Buffer)
End Sub

Public Sub SendCommandCurrency(ByRef Commands() As String, ByVal CommandType As SuperiorCommands)
' Name = 0
' Target = 1
    Const CurrencyIndex As Byte = 2
    Const CurrencyValue As Byte = 3

    Const ParamCount As Long = 4

    If UBound(Commands) < CurrencyValue Then
        Exit Sub
    End If

    If Not IsNumeric(Commands(CurrencyIndex)) Or Not IsNumeric(Commands(CurrencyValue)) Then
        Exit Sub
    End If

    Dim Buffer As clsBuffer
    Call CreateBuffer(Buffer)

    Buffer.WriteLong SuperiorCommands.SetCurrency
    ' Write Param
    Buffer.WriteLong ParamCount
    Buffer.WriteString Commands(CommandTarget)
    Buffer.WriteString Commands(CurrencyIndex)
    Buffer.WriteString Commands(CurrencyValue)

    If CommandType = SuperiorCommands.AddCurrency Then
        Buffer.WriteString 1
    Else
        Buffer.WriteString 0
    End If

    Call SendCommand(Buffer)
End Sub

Public Sub SendCommandTitle(ByRef Commands() As String, ByVal CommandType As SuperiorCommands)
' Name = 0
' Target = 1
    Const Title As Byte = 2
    Const ParamCount As Long = 2

    If UBound(Commands) < Title Then
        Exit Sub
    End If

    If Not IsNumeric(Commands(Title)) Then
        Exit Sub
    End If

    Dim Buffer As clsBuffer
    Call CreateBuffer(Buffer)

    Buffer.WriteLong CommandType
    ' Write Param
    Buffer.WriteLong ParamCount
    Buffer.WriteString Commands(CommandTarget)
    Buffer.WriteString Commands(Title)

    Call SendCommand(Buffer)
End Sub
'
Public Sub SendCommandAchievement(ByRef Commands() As String, ByVal CommandType As SuperiorCommands)
' Name = 0
' Target = 1
    Const Achievement As Byte = 2
    Const ParamCount As Long = 2

    If UBound(Commands) < Achievement Then
        Exit Sub
    End If

    If Not IsNumeric(Commands(Achievement)) Then
        Exit Sub
    End If

    Dim Buffer As clsBuffer
    Call CreateBuffer(Buffer)

    Buffer.WriteLong CommandType
    ' Write Param
    Buffer.WriteLong ParamCount
    Buffer.WriteString Commands(CommandTarget)
    Buffer.WriteString Commands(Achievement)

    Call SendCommand(Buffer)
End Sub

Public Sub SendAddItem(ByRef Commands() As String, ByVal CommandType As SuperiorCommands)
' Name = 0
' Target = 1
    Const ItemId As Byte = 2
    Const ItemValue As Byte = 3
    Const ItemLevel As Byte = 4
    Const ItemBound As Byte = 5

    Const ParamCount As Long = 5

    Dim i As Long

    If UBound(Commands) < ItemBound Then
        Exit Sub
    End If

    For i = ItemId To ItemBound
        If Not IsNumeric(Commands(i)) Then
            Exit Sub
        End If
    Next

    Dim Buffer As clsBuffer
    Call CreateBuffer(Buffer)

    Buffer.WriteLong CommandType
    ' Write Param
    Buffer.WriteLong ParamCount
    Buffer.WriteString Commands(CommandTarget)

    For i = ItemId To ItemBound
        Buffer.WriteString Val(Commands(i))
    Next

    Call SendCommand(Buffer)
End Sub

Public Sub SendAddEffect(ByRef Commands() As String)
' Name = 0
' Target = 1
    Const BuffId As Byte = 2
    Const BuffLevel As Byte = 3
    Const BuffDuration As Byte = 4

    Const ParamCount As Long = 4

    Dim i As Long

    If UBound(Commands) < BuffDuration Then
        Exit Sub
    End If

    For i = BuffId To BuffDuration
        If Not IsNumeric(Commands(i)) Then
            Exit Sub
        End If
    Next

    Dim Buffer As clsBuffer
    Call CreateBuffer(Buffer)

    Buffer.WriteLong SuperiorCommands.AddBuff
    ' Write Param
    Buffer.WriteLong ParamCount
    Buffer.WriteString Commands(CommandTarget)

    For i = BuffId To BuffDuration
        Buffer.WriteString Val(Commands(i))
    Next

    Call SendCommand(Buffer)
End Sub

Public Sub SendAdminWarp(ByVal X As Long, ByVal Y As Long)
    If X < 0 Or Y < 0 Or X > CurrentMap.MapData.MaxX Or Y > CurrentMap.MapData.MaxY Then Exit Sub

    Const ParamCount As Long = 2

    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PSuperiorCommand
    Buffer.WriteLong SuperiorCommands.WarpToLocation
    ' Write Param
    Buffer.WriteLong ParamCount
    Buffer.WriteString Str(X)
    Buffer.WriteString Str(Y)

    SendGameMessage Buffer.ToArray()

    Set Buffer = Nothing
End Sub

Public Sub SendWarpTo(ByVal MapNum As Long)
    Const ParamCount As Long = 1

    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PSuperiorCommand
    Buffer.WriteLong SuperiorCommands.WarpTo
    ' Write Param
    Buffer.WriteLong ParamCount
    Buffer.WriteString Str(MapNum)

    SendGameMessage Buffer.ToArray()

    Set Buffer = Nothing
End Sub

Public Sub SendWarpMeTo(ByVal Name As String)
    Const ParamCount As Long = 1

    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PSuperiorCommand
    Buffer.WriteLong SuperiorCommands.WarpMeTo
    ' Write Param
    Buffer.WriteLong ParamCount
    Buffer.WriteString Name

    SendGameMessage Buffer.ToArray()

    Set Buffer = Nothing
End Sub

Public Sub SendWarpToMe(ByVal Name As String)
    Const ParamCount As Long = 1

    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PSuperiorCommand
    Buffer.WriteLong SuperiorCommands.WarpToMe
    ' Write Param
    Buffer.WriteLong ParamCount
    Buffer.WriteString Name

    SendGameMessage Buffer.ToArray()

    Set Buffer = Nothing
End Sub

Public Sub SendKick(ByVal Name As String)
    Const ParamCount As Long = 1

    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong EnginePacket.PSuperiorCommand
    Buffer.WriteLong SuperiorCommands.Kick
    ' Write Param
    Buffer.WriteLong ParamCount
    Buffer.WriteString Name

    SendGameMessage Buffer.ToArray()
    Set Buffer = Nothing
End Sub

Private Sub CreateBuffer(ByRef Buffer As clsBuffer)
    Set Buffer = New clsBuffer
    Buffer.WriteLong EnginePacket.PSuperiorCommand
End Sub
Private Sub SendCommand(ByRef Buffer As clsBuffer)
    SendGameMessage Buffer.ToArray()
    Set Buffer = Nothing
End Sub
