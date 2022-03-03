Attribute VB_Name = "Options_Data"
Public Options As OptionsRec

Private Type OptionsRec
    Music As Byte
    Sound As Byte
    NoAuto As Byte
    Render As Byte
    Username As String
    SaveUser As Long
    ChannelState(0 To Channel_Count - 1) As Byte
    PlayIntro As Byte
    Resolution As Byte
    Fullscreen As Byte
End Type
