Attribute VB_Name = "Party_Window"
Option Explicit

Public Sub CreateWindow_Party()
' Create window
    CreateWindow "winParty", "", zOrder_Win, 4, 78, 252, 158, 0, , , , , , DesignTypes.DesignParty, DesignTypes.DesignParty, DesignTypes.DesignParty, , , , , , , , , False

    Dim i As Long
    Dim OffSetY As Long
    
    OffSetY = 95

    For i = 1 To MaximumPartyMembers - 1
        ' Names
        CreateLabel WindowCount, "lblName" & i, 60, 20 + (OffSetY * (i - 1)), 173, , "Richard - Level 10", OpenSans_Regular
        ' Empty Bars - HP
        CreatePictureBox WindowCount, "picEmptyBar_HP" & i, 58, 34 + (OffSetY * (i - 1)), 173, 9, , , , , Tex_GUI(39), Tex_GUI(39), Tex_GUI(39)
        ' Empty Bars - SP
        CreatePictureBox WindowCount, "picEmptyBar_SP" & i, 58, 44 + (OffSetY * (i - 1)), 173, 9, , , , , Tex_GUI(41), Tex_GUI(41), Tex_GUI(41)
        ' Filled bars - HP
        CreatePictureBox WindowCount, "picBar_HP" & i, 58, 34 + (OffSetY * (i - 1)), 173, 9, , , , , Tex_GUI(64), Tex_GUI(40), Tex_GUI(40)
        ' Filled bars - SP
        CreatePictureBox WindowCount, "picBar_SP" & i, 58, 44 + (OffSetY * (i - 1)), 173, 9, , , , , Tex_GUI(42), Tex_GUI(42), Tex_GUI(42)
        ' Models
        CreatePictureBox WindowCount, "picChar" & i, 20, 20 + (OffSetY * (i - 1)), 32, 32    ', , , , , Tex_Char(1), Tex_Char(1), Tex_Char(1)
    Next

    ' Name labels
    ' CreateLabel WindowCount, "lblName1", 60, 20, 173, , "Richard - Level 10", OpenSans_Regular
    ' CreateLabel WindowCount, "lblName2", 60, 60, 173, , "Anna - Level 18", OpenSans_Regular
    ' CreateLabel WindowCount, "lblName3", 60, 140, 173, , "Doleo - Level 25", OpenSans_Regular
    ' CreateLabel WindowCount, "lblName4", 60, 180, 173, , "Doleo - Level 25", OpenSans_Regular
    ' CreateLabel WindowCount, "lblName5", 60, 220, 173, , "Doleo - Level 25", OpenSans_Regular

    ' Empty Bars - HP
    'CreatePictureBox WindowCount, "picEmptyBar_HP1", 58, 34, 173, 9, , , , , Tex_GUI(62), Tex_GUI(62), Tex_GUI(62)
    'CreatePictureBox WindowCount, "picEmptyBar_HP2", 58, 74, 173, 9, , , , , Tex_GUI(62), Tex_GUI(62), Tex_GUI(62)
    'CreatePictureBox WindowCount, "picEmptyBar_HP3", 58, 114, 173, 9, , , , , Tex_GUI(62), Tex_GUI(62), Tex_GUI(62)

    ' Empty Bars - SP
    'CreatePictureBox WindowCount, "picEmptyBar_SP1", 58, 44, 173, 9, , , , , Tex_GUI(63), Tex_GUI(63), Tex_GUI(63)
    'CreatePictureBox WindowCount, "picEmptyBar_SP2", 58, 84, 173, 9, , , , , Tex_GUI(63), Tex_GUI(63), Tex_GUI(63)
    'CreatePictureBox WindowCount, "picEmptyBar_SP3", 58, 124, 173, 9, , , , , Tex_GUI(63), Tex_GUI(63), Tex_GUI(63)

    ' Filled bars - HP
    ' CreatePictureBox WindowCount, "picBar_HP1", 58, 34, 173, 9, , , , , Tex_GUI(64), Tex_GUI(64), Tex_GUI(64)
    ' CreatePictureBox WindowCount, "picBar_HP2", 58, 74, 173, 9, , , , , Tex_GUI(64), Tex_GUI(64), Tex_GUI(64)
    ' CreatePictureBox WindowCount, "picBar_HP3", 58, 114, 173, 9, , , , , Tex_GUI(64), Tex_GUI(64), Tex_GUI(64)

    ' Filled bars - SP
    ' CreatePictureBox WindowCount, "picBar_SP1", 58, 44, 173, 9, , , , , Tex_GUI(65), Tex_GUI(65), Tex_GUI(65)
    ' CreatePictureBox WindowCount, "picBar_SP2", 58, 84, 173, 9, , , , , Tex_GUI(65), Tex_GUI(65), Tex_GUI(65)
    ' CreatePictureBox WindowCount, "picBar_SP3", 58, 124, 173, 9, , , , , Tex_GUI(65), Tex_GUI(65), Tex_GUI(65)
    ' Shadows
    ' CreatePictureBox WindowCount, "picShadow1", 20, 24, 32, 32, , , , , Tex_Shadow, Tex_Shadow, Tex_Shadow
    ' CreatePictureBox WindowCount, "picShadow2", 20, 64, 32, 32, , , , , Tex_Shadow, Tex_Shadow, Tex_Shadow
    ' CreatePictureBox WindowCount, "picShadow3", 20, 104, 32, 32, , , , , Tex_Shadow, Tex_Shadow, Tex_Shadow
    ' Models
    ' CreatePictureBox WindowCount, "picChar1", 20, 20, 32, 32, , , , , Tex_Char(1), Tex_Char(1), Tex_Char(1)
    ' CreatePictureBox WindowCount, "picChar2", 20, 60, 32, 32, , , , , Tex_Char(1), Tex_Char(1), Tex_Char(1)
    ' CreatePictureBox WindowCount, "picChar3", 20, 100, 32, 32, , , , , Tex_Char(1), Tex_Char(1), Tex_Char(1)
End Sub

Public Sub UpdatePartyInterface()
    Dim i As Long, image(0 To 5) As Long, X As Long, CharacterNum As Long, Height As Long, cIn As Long

    ' unload it if we're not in a party
    If Party.Leader = 0 Then
        HideWindow GetWindowIndex("winParty")
        Exit Sub
    End If

    ' load the window
    ShowWindow GetWindowIndex("winParty")
    ' fill the controls
    With Windows(GetWindowIndex("winParty"))
        ' clear controls first
        For i = 1 To MaximumPartyMembers - 1
            .Controls(GetControlIndex("winParty", "lblName" & i)).Text = vbNullString
            .Controls(GetControlIndex("winParty", "picEmptyBar_HP" & i)).Visible = False
            .Controls(GetControlIndex("winParty", "picEmptyBar_SP" & i)).Visible = False
            .Controls(GetControlIndex("winParty", "picBar_HP" & i)).Visible = False
            .Controls(GetControlIndex("winParty", "picBar_SP" & i)).Visible = False
            .Controls(GetControlIndex("winParty", "picChar" & i)).Visible = False
            .Controls(GetControlIndex("winParty", "picChar" & i)).Value = 0
        Next

        ' labels
        cIn = 1

        For i = 1 To MaximumPartyMembers
            CharacterNum = Party.Member(i).Index

            If CharacterNum > 0 Then
                If Party.Member(i).Name <> GetPlayerName(MyIndex) Then
                    ' name and level
                    .Controls(GetControlIndex("winParty", "lblName" & cIn)).Visible = True
                    .Controls(GetControlIndex("winParty", "lblName" & cIn)).Text = Party.Member(i).Name
                    ' picture
                    .Controls(GetControlIndex("winParty", "picChar" & cIn)).Visible = True
                    .Controls(GetControlIndex("winParty", "picChar" & cIn)).Value = Party.Member(i).Model
                    ' bars
                    .Controls(GetControlIndex("winParty", "picEmptyBar_HP" & cIn)).Visible = True
                    .Controls(GetControlIndex("winParty", "picEmptyBar_SP" & cIn)).Visible = True
                    .Controls(GetControlIndex("winParty", "picBar_HP" & cIn)).Visible = True
                    .Controls(GetControlIndex("winParty", "picBar_SP" & cIn)).Visible = True
                    ' increment control usage
                    cIn = cIn + 1
                End If
            End If
        Next

        ' update the bars
        UpdatePartyBars

        ' set the window size
        Select Case Party.MemberCount
        Case 2: Height = 85
        Case 3: Height = 188
        Case 4: Height = 290
        Case 5: Height = 400
        Case 6: Height = 527

        End Select
        .Window.Height = Height
    End With
End Sub

Public Sub UpdatePartyBars()
    Dim i As Long, Sprite As Long, barWidth As Long, Width As Long, ControlCount As Long
    Dim Percentage As Single

    ' unload it if we're not in a party
    If Party.Leader = 0 Then
        Exit Sub
    End If

    ' max bar width
    barWidth = 173
    ControlCount = 1

    ' make sure we're in a party
    With Windows(GetWindowIndex("winParty"))
        For i = 1 To MaximumPartyMembers

            If Party.Member(i).Name <> GetPlayerName(MyIndex) Then
                ' get the sprite from the control
                If .Controls(GetControlIndex("winParty", "picChar" & ControlCount)).Visible = True Then
                    Sprite = .Controls(GetControlIndex("winParty", "picChar" & ControlCount)).Value

                    ' make sure they exist
                    If Sprite > 0 Then
                        ' get their health
                        If Party.Member(i).Vital(Vitals.HP) > 0 And Party.Member(i).MaxVital(Vitals.HP) > 0 Then
                            Percentage = CSng(Party.Member(i).Vital(Vitals.HP) / Party.Member(i).MaxVital(Vitals.HP))
                            Width = barWidth * Percentage

                            .Controls(GetControlIndex("winParty", "picBar_HP" & ControlCount)).Width = Width
                        Else
                            .Controls(GetControlIndex("winParty", "picBar_HP" & ControlCount)).Width = 0
                        End If
                        ' get their spirit
                        If Party.Member(i).Vital(Vitals.MP) > 0 And Party.Member(i).MaxVital(Vitals.MP) > 0 Then
                            Percentage = CSng(Party.Member(i).Vital(Vitals.MP) / Party.Member(i).MaxVital(Vitals.MP))
                            Width = barWidth * Percentage

                            .Controls(GetControlIndex("winParty", "picBar_SP" & ControlCount)).Width = Width
                        Else
                            .Controls(GetControlIndex("winParty", "picBar_SP" & ControlCount)).Width = 0
                        End If

                        ControlCount = ControlCount + 1
                    End If

                End If
            End If

        Next
    End With
End Sub


