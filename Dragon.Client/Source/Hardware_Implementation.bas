Attribute VB_Name = "Hardware_Implementation"
Public MachineId As String
Public BIOSId As String
Public HardDiskId As String
Public CPUId As String
Public VideoId As String
Public MacAddressId As String
Public MotherBoardId As String

Public Sub LoadMachineId()
    MachineId = String(512, vbNullChar)
    BIOSId = String(512, vbNullChar)
    HardDiskId = String(512, vbNullChar)
    CPUId = String(512, vbNullChar)
    VideoId = String(512, vbNullChar)
    MacAddressId = String(512, vbNullChar)
    MotherBoardId = String(512, vbNullChar)

    Call GetCPUId(CPUId)
    Call GetBIOSId(BIOSId)
    Call GetDiskId(HardDiskId)
    Call GetBoardId(MotherBoardId)
    Call GetVideoId(VideoId)
    Call GetMacAddressId(MacAddressId)

    CPUId = Replace$(CPUId, vbNullChar, vbNullString)
    BIOSId = Replace$(BIOSId, vbNullChar, vbNullString)
    HardDiskId = Replace$(HardDiskId, vbNullChar, vbNullString)
    MotherBoardId = Replace$(MotherBoardId, vbNullChar, vbNullString)
    VideoId = Replace$(VideoId, vbNullChar, vbNullString)
    MacAddressId = Replace$(MacAddressId, vbNullChar, vbNullString)

    Dim Buffer(0 To 256) As Byte
    Dim Length As Long

    Length = Compute(BIOSId & HardDiskId & CPUId & VideoId & MacAddressId & MotherBoardId, ByVal VarPtr(Buffer(0)))

    Call ConvertToHexadecimal(ByVal VarPtr(Buffer(0)), Length, MachineId)

    MachineId = Replace$(MachineId, vbNullChar, vbNullString)

End Sub

