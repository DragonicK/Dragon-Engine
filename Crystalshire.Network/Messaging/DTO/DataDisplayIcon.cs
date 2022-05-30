﻿using Crystalshire.Core.Model.DisplayIcon;

namespace Crystalshire.Network.Messaging.DTO {
    public struct DataDisplayIcon {
        public DisplayIconType IconType { get; set; }
        public DisplayIconSkill SkillType { get; set; }
        public DisplayIconDuration DurationType { get; set; }
        public DisplayIconOperation OperationType { get; set; }
        public DisplayIconExhibition ExhibitionType { get; set; }
        public int Id { get; set; }
        public int Level { get; set; }
        public int Duration { get; set; }
    }
}