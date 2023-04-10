Attribute VB_Name = "Network_ClientPacket"
Option Explicit

' Packets sent by client to server
Public Enum ClientPackets
    CRequestNewMap = 5000
           
    CRessurrectSelf
    CRessurrectByPlayer
    
    CAttack
    
    ' Make sure CMsgCOUNT is below everything else
    CMsgCOUNT
End Enum
