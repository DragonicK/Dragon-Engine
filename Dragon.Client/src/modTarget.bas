Attribute VB_Name = "modTarget"
Option Explicit

Public MyTarget As Long
Public MyTargetType As Long

' Target type constants
Public Const TargetTypeNone As Byte = 0
Public Const TargetTypePlayer As Byte = 1
Public Const TargetTypeNpc As Byte = 2
Public Const TargetTypeLoot As Byte = 3

Private Const TARGET_WIDTH = 64
Private Const TARGET_HEIGHT = 32

Private Count_Target As Long
Private TargetTick As Long
Private TargetFrameIndex As Long

Private TargetTexture() As TextureStruct

Private Const TARGET_PATH As String = "\Data Files\Graphics\Target\"

Public Sub LoadTarget()
    Dim Data() As Byte
    Dim Count As Long
    Dim f As Long, i As Long, File As String

    TargetFrameIndex = 1
    Count = CountFiles
    Count_Target = Count

    If Count > 0 Then

        ReDim TargetTexture(1 To Count)

        For i = 1 To Count
            File = App.Path & TARGET_PATH & i & ".png"

            f = FreeFile
            Open File For Binary As #f
            ReDim Data(0 To LOF(f) - 1)
            Get #f, , Data
            Close #f

            Call LoadParallaxTexture(Data, TargetTexture(i))
        Next

    End If

End Sub

Private Function CountFiles() As Long
    Dim File As String, Count As Long

    File = Dir$(App.Path & TARGET_PATH & "*.png")

    Do While Len(File)
        File = Dir$

        Count = Count + 1
    Loop

    CountFiles = Count

End Function

Public Sub UpdateTargetCalculation()
    If GetTickCount >= TargetTick + 70 Then
        TargetFrameIndex = TargetFrameIndex + 1
        TargetTick = GetTickCount

        If TargetFrameIndex > Count_Target Then
            TargetFrameIndex = 1
        End If
    End If
End Sub

Public Sub DrawTarget()
    Dim X As Long, Y As Long

    If MyTarget > 0 Then
        If MyTargetType = TargetTypePlayer Then
            X = (Player(MyTarget).X * 32) + Player(MyTarget).xOffset
            Y = (Player(MyTarget).Y * 32) + Player(MyTarget).yOffset
        ElseIf MyTargetType = TargetTypeNpc Then
            X = (MapNpc(MyTarget).X * 32) + MapNpc(MyTarget).xOffset
            Y = (MapNpc(MyTarget).Y * 32) + MapNpc(MyTarget).yOffset
        End If
    Else
        Exit Sub
    End If

    X = X - 16
    Y = Y + 9
    X = ConvertMapX(X)
    Y = ConvertMapY(Y)

    RenderParallaxTexture TargetTexture(TargetFrameIndex), X, Y, 0, 0, TARGET_WIDTH, TARGET_HEIGHT, TARGET_WIDTH, TARGET_HEIGHT
End Sub

Public Sub DrawTargetHover()
    Dim i As Long, X As Long, Y As Long

    If diaIndex > 0 Then Exit Sub

    For i = 1 To Player_HighIndex

        If IsPlaying(i) And GetPlayerMap(MyIndex) = GetPlayerMap(i) Then
            X = (Player(i).X * 32) + Player(i).xOffset + 32
            Y = (Player(i).Y * 32) + Player(i).yOffset + 32

            If X >= GlobalX_Map And X <= GlobalX_Map + 32 Then
                If Y >= GlobalY_Map And Y <= GlobalY_Map + 32 Then
                    X = X - 48
                    Y = Y - 23
                    X = ConvertMapX(X)
                    Y = ConvertMapY(Y)

                    RenderParallaxTexture TargetTexture(TargetFrameIndex), X, Y, 0, 0, TARGET_WIDTH, TARGET_HEIGHT, TARGET_WIDTH, TARGET_HEIGHT, D3DColorARGB(140, 255, 255, 255)
                End If
            End If
        End If
    Next

    For i = 1 To Npc_HighIndex
        If MapNpc(i).Num > 0 Then
            If Not GetNpcDead(i) Then
                X = (MapNpc(i).X * 32) + MapNpc(i).xOffset + 32
                Y = (MapNpc(i).Y * 32) + MapNpc(i).yOffset + 32

                If X >= GlobalX_Map And X <= GlobalX_Map + 32 Then
                    If Y >= GlobalY_Map And Y <= GlobalY_Map + 32 Then
                        X = X - 48
                        Y = Y - 23
                        X = ConvertMapX(X)
                        Y = ConvertMapY(Y)

                        RenderParallaxTexture TargetTexture(TargetFrameIndex), X, Y, 0, 0, TARGET_WIDTH, TARGET_HEIGHT, TARGET_WIDTH, TARGET_HEIGHT, D3DColorARGB(140, 255, 255, 255)
                    End If
                End If
            End If
        End If
    Next

    For i = 1 To Corpse_HighIndex
        If Corpse(i).LootId > 0 Then
            X = Corpse(i).X + 32
            Y = Corpse(i).Y + 32

            If X >= GlobalX_Map And X <= GlobalX_Map + 32 Then
                If Y >= GlobalY_Map And Y <= GlobalY_Map + 32 Then
                    X = X - 48
                    Y = Y - 23
                    X = ConvertMapX(X)
                    Y = ConvertMapY(Y)

                    ' found our target!
                    RenderParallaxTexture TargetTexture(TargetFrameIndex), X, Y, 0, 0, TARGET_WIDTH, TARGET_HEIGHT, TARGET_WIDTH, TARGET_HEIGHT, D3DColorARGB(140, 255, 255, 255)
                    Exit Sub
                End If
            End If

        End If
    Next


End Sub
