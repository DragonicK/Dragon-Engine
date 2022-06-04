Attribute VB_Name = "ActiveIcon_Data"
Option Explicit

Public Const ActiveIconTop As Long = 60
Public Const ActiveIconLeft As Long = 0
Public Const ActiveIconOffsetY As Long = 10
Public Const ActiveIconOffsetX As Long = 5
Public Const ActiveIconColumns As Long = 10

Public Const PositiveActiveIconTop As Long = 0
Public Const NegativeActiveIconTop As Long = 100

Public Const MaxEntityActiveIcon As Long = 40

Public PartyActiveIcon(1 To MaximumPartyMembers) As EntityActiveIconRec
Public PlayerActiveIcon(1 To MaxPlayers) As EntityActiveIconRec
Public MapNpcActiveIcon(1 To MaxMapNpcs) As EntityActiveIconRec

Public Enum ActiveIconTargetType
    ActiveIconTargetType_Npc
    ActiveIconTargetType_Party
    ActiveIconTargetType_Player
End Enum

Public Type ActiveIconRec
    IconType As IconType
    SkillType As IconSkillType
    DurationType As IconDurationType
    Exhibition As IconExhibitionType
    Id As Long
    Level As Long
    Duration As Long
End Type

Public Type EntityActiveIconRec
    ListEnabled As Boolean
    ActiveIconCount As Long
    ActiveIconIndex(1 To MaxEntityActiveIcon) As Long
    Icons(1 To MaxEntityActiveIcon) As ActiveIconRec
End Type

' Tipo de duração do ícone.
Public Enum IconDurationType
    ' Sem duração.
    IconDurationType_Unlimited
    ' Com duração.
    IconDurationType_Limited
End Enum

' Tipo de exibição do ícone.
Public Enum IconExhibitionType
    ' Exibe o ícone apenas para o personagem.
    IconExhibitionType_Self
    ' Exibe para jogadores.
    IconExhibitionType_Player
    ' Exibe o ícone somente para o grupo.
    IconExhibitionType_Party
End Enum

' Tipo de operação.
Public Enum IconOperationType
    ' Atualiza as informações de um ícone.
    IconOperationType_Update
    ' Remove um ícone da lista.
    IconOperationType_Remove
End Enum

' Tipo de habilidade. (Quando usado)
Public Enum IconSkillType
    IconSkillType_None
    IconSkillType_DamageOverTime
    IconSkillType_HealOverTime
    IconSkillType_Silence
    IconSkillType_Blind
    IconSkillType_Immobilize
    IconSkillType_Passive
    IconSkillType_Absortion
End Enum

' Tipo de informação.
Public Enum IconType
    IconType_None
    ' A descrição é obtida a partir do item.
    IconType_Item
    ' A descrição é obtida a partir da habilidade.
    IconType_Skill
    ' A descrição é obtida a partir do spell buff.
    IconType_Effect
    ' A descrição é obtida a partir de uma lista.
    IconType_Custom
End Enum
