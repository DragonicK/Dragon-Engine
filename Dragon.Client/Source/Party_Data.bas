Attribute VB_Name = "Party_Data"
Option Explicit

Public Const MaximumPartyMembers As Byte = 5

Public Party As PartyRec

Private Type PartyMemberRec
    Index As Long
    Name As String
    Model As Long
    InstanceId As Long
    Disconnected As Boolean
    Vital(1 To Vitals.Vital_Count - 1)
    MaxVital(1 To Vitals.Vital_Count - 1)
End Type

Public Type PartyRec
    Leader As Long
    Level As Long
    Experience As Long
    MaxExperience As Long
    Member(1 To MaximumPartyMembers) As PartyMemberRec
    MemberCount As Long
End Type
