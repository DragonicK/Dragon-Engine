Attribute VB_Name = "modGeneral"
Option Explicit

Public Sub AddChar(Name As String, Sex As Long, Class As Long, Sprite As Long)

    If ConnectToServer Then
        HideWindows
        Call SetStatus("Enviando informações do personagem.")
        Call SendAddChar(Name, Sex, Class, Sprite)
        Exit Sub
    Else
        ShowWindow GetWindowIndex("winLogin")
        ShowDialogue "Problema de Conexao", "Não pode conectar-se ao servidor de jogo.", "", DialogueTypeAlert
    End If

End Sub

Public Sub Login(Name As String, Password As String)
    TcpInit AUTH_SERVER_IP, AUTH_SERVER_PORT

    If ConnectToServer Then
    HideWindows
        Call SetStatus("Enviando informações de login.")
        Call SendAuthLogin(Name, Password)
        ' save details
        If Options.SaveUser Then Options.Username = Name Else Options.Username = vbNullString
        SaveOptions
        Exit Sub
    Else
        ShowWindow GetWindowIndex("winLogin")
        ShowDialogue "Problema de Conexao", "Não pode conectar-se ao servidor de login.", "Tente novamente mais tarde.", DialogueTypeAlert
    End If

End Sub

Public Sub LogoutGame()
    Dim i As Long
    isLogging = True
    InGame = False

    ClearData
    ClearParty
    ClearServices
    ClearCraft
    ClearCorpses
    ClearInventories

    DestroyTCP

    ' destroy the animations loaded
    For i = 1 To MAX_BYTE
        Call ClearAnimInstance(i)
    Next

    For i = 1 To MAX_CHARS
        CharPendingExclusion(i) = False
        CharHour(i) = 0
        CharMinutes(i) = 0
        CharSeconds(i) = 0
        CharDate(i) = 0
    Next

    ' destroy temp values
    MyIndex = 0
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
        CanDestroyGame = True

        DestroyDX8

        Call DestroyTCP

        ' destroy music & sound engines
        Destroy_Music

        Call UnloadAllForms

        End

    End If
End Sub

Public Sub UnloadAllForms()
    Dim frm As Form

    For Each frm In VB.Forms
        Unload frm
    Next

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

Public Function IsLoginLegal(ByVal Username As String, ByVal Password As String) As Boolean

    If LenB(Trim$(Username)) >= 3 Then
        If LenB(Trim$(Password)) >= 3 Then
            IsLoginLegal = True
        End If
    End If

End Function

Public Function IsStringLegal(ByVal sInput As String) As Boolean
    Dim i As Long, tmpNum As Long
    ' Prevent high ascii chars
    tmpNum = Len(sInput)

    For i = 1 To tmpNum

        If Asc(Mid$(sInput, i, 1)) < vbKeySpace Or Asc(Mid$(sInput, i, 1)) > vbKeyF15 Then
            ShowDialogue "Models ilegais", "O nome contém caracteres não permitidos.", "", DialogueTypeAlert
            Exit Function
        End If

    Next

    IsStringLegal = True
End Function


Public Sub ChkDir(ByVal tDir As String, ByVal tName As String)
    If LCase$(Dir$(tDir & tName, vbDirectory)) <> tName Then Call MkDir(tDir & tName)
End Sub

Public Function FileExist(ByVal FileName As String) As Boolean
    If LenB(Dir$(FileName)) > 0 Then
        FileExist = True
    End If
End Function

Sub ClearPlayer(ByVal Index As Long)
    Call ZeroMemory(ByVal VarPtr(Player(Index)), LenB(Player(Index)))
    Player(Index).Name = vbNullString
End Sub

Sub ClearAnimInstance(ByVal Index As Long)
    Call ZeroMemory(ByVal VarPtr(AnimInstance(Index)), LenB(AnimInstance(Index)))
End Sub

Sub ClearMapNpc(ByVal Index As Long)
    Call ZeroMemory(ByVal VarPtr(MapNpc(Index)), LenB(MapNpc(Index)))
    MapNpc(Index).Dead = True
End Sub

Sub ClearMapNpcs()
    Dim i As Long

    For i = 1 To MaxMapNpcs
        Call ClearMapNpc(i)
    Next

End Sub


