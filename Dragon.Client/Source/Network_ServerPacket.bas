Attribute VB_Name = "Network_ServerPacket"
Option Explicit

' The order of the packets must match with the server's packet enumeration
' Packets sent by server to client
Public Enum ServerPackets
    SNpcAttack
    SNpcDead
    SSound
    
    SPlayerAchievement
    SUpdateAchievement
    
    SDeadPanelOperation
    SPlayerDead
    SRessurrection
    
    SAttack
    
    
    SUpdateLootState
    SOpenLoot
    SCloseLoot
    SSortLootList
    SEnableDropTakeItem
    SRollDiceItem
    
    SStunned
    ' Make sure SMsgCOUNT is below everything else
    SMsgCOUNT
End Enum
