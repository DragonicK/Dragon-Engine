Attribute VB_Name = "General_Implementation"
Option Explicit

Public Sub LogoutGame()
    Dim i As Long

    isLogging = True
    InGame = False

    ClearData
    ClearParty
    ClearServices
    ClearCraft
    ClearChests
    ClearInventories

    For i = 1 To MAX_BYTE
        Call ClearAnimInstance(i)
    Next

    DestroyGameClient

    For i = 1 To MAX_CHARS
        CharPendingExclusion(i) = False
        CharHour(i) = 0
        CharMinutes(i) = 0
        CharSeconds(i) = 0
        CharDate(i) = 0
    Next

    ' destroy temp values
    MyIndex = 0
    MyTargetType = 0
    MyTargetIndex = 0

    InventoryItemSelected = 0
    InventoryentoryTabIndex = 0
    
    SpellBuffer = 0
    SpellBufferTimer = 0

    ' clear chat
    For i = 1 To ChatLines
        Chat(i).Text = vbNullString
    Next

    inMenu = True

    Erase GroundParallax
    Erase FringeParallax

    MenuLoop
End Sub

Public Sub DestroyGame()
    If Not CanDestroyGame Then
        Dim frm As Form

        CanDestroyGame = True

        DestroyDX8

        Call DestroyGameClient

        ' destroy music & sound engines
        Destroy_Music

        For Each frm In VB.Forms
            Unload frm
        Next

        End
    End If
End Sub

Public Sub SetStatus(ByVal caption As String)
    If Len(Trim$(caption)) > 0 Then
        ShowWindow GetWindowIndex("winLoading")
        Windows(GetWindowIndex("winLoading")).Controls(GetControlIndex("winLoading", "lblLoading")).Text = caption
    Else
        HideWindow GetWindowIndex("winLoading")
        Windows(GetWindowIndex("winLoading")).Controls(GetControlIndex("winLoading", "lblLoading")).Text = vbNullString
    End If
End Sub

Public Function Rand(ByVal Low As Long, ByVal High As Long) As Long
    Rand = Int((High - Low + 1) * Rnd) + Low
End Function

Public Sub ChkDir(ByVal tDir As String, ByVal tName As String)
    If LCase$(Dir$(tDir & tName, vbDirectory)) <> tName Then Call MkDir(tDir & tName)
End Sub

Public Function FileExist(ByVal FileName As String) As Boolean
    If LenB(Dir$(FileName)) > 0 Then
        FileExist = True
    End If
End Function




