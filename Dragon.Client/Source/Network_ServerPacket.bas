Attribute VB_Name = "Network_ServerPacket"
Option Explicit

' The order of the packets must match with the server's packet enumeration
' Packets sent by server to client
Public Enum ServerPackets
    SNpcAttack
    
    SNpcDead
       
    SSound
    
    SAnimation
    
    SCancelAnimation
    
    SPlayerAchievement
    SUpdateAchievement
    
    SDeadPanelOperation
    SPlayerDead
    SRessurrection
    
    SAttack
    
    SMapLoot
    SUpdateLootState
    SOpenLoot
    SCloseLoot
    SSortLootList
    SEnableDropTakeItem
    SRollDiceItem
      
    SCooldown
    SClearSpellBuffer
    SStunned
    ' Make sure SMsgCOUNT is below everything else
    SMsgCOUNT
End Enum
