Attribute VB_Name = "DeadPanel_Packet"
Option Explicit

Public Sub SelfRessurect(ByVal RessurrectionType As RessurrectionType)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong CRessurrectSelf
    Buffer.WriteByte RessurrectionType

    SendData Buffer.ToArray
    Set Buffer = Nothing
End Sub

Public Sub PlayerRessurrect(ByVal Confirmation As RessurrectionConfirmation)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer

    Buffer.WriteLong CRessurrectByPlayer
    Buffer.WriteByte Confirmation

    SendData Buffer.ToArray
    Set Buffer = Nothing

End Sub

Public Sub HandleDeadPanelOperation(ByVal Index As Long, ByRef Data() As Byte, ByVal StartAddr As Long, ByVal ExtraVar As Long)
    Dim Buffer As clsBuffer
    Set Buffer = New clsBuffer
    Dim OperationType As DeadPanelOperationType
    Dim PanelTime As Long, WindowIndex As Long
    Dim RessurrectedByCharacter As String

    Buffer.WriteBytes Data

    OperationType = Buffer.ReadByte
    PanelTime = Buffer.ReadLong
    RessurrectedByCharacter = Buffer.ReadString

    Set Buffer = Nothing

    ' Open
    If OperationType = DeadPanelOperationType.DeadPanelOperationType_Open Then
        MyTargetIndex = 0
        MyTargetType = TargetTypeNone
        
        Call SetDeadPanelTime(PanelTime)
        Call OpenDeadPanel(True, vbNullString)

        ' Close
    ElseIf OperationType = DeadPanelOperationType.DeadPanelOperationType_Close Then
        Call HideDeadPanel
        Call SetDeadPanelTick(0)
        Call SetDeadPanelTime(0)
                
        ' Update Text
    ElseIf OperationType = DeadPanelOperationType.DeadPanelOperationType_RessurrectionByPlayer Then

        Call SetDeadPanelTime(PanelTime)
        Call OpenDeadPanel(False, RessurrectedByCharacter)
        
        ' Decline
    ElseIf OperationType = DeadPanelOperationType.DeadPanelOperationType_Decline Then
        Call OpenDeadPanel(True, vbNullString)
        
    End If

End Sub

