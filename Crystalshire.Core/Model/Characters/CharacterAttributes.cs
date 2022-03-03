namespace Crystalshire.Core.Model.Characters {
    public abstract class CharacterAttributes {
        public int[] PrimaryAttributes { get; set; }
        public int[] Vital { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Accuracy { get; set; }
        public int Evasion { get; set; }
        public int Parry { get; set; }
        public int Block { get; set; }
        public int MagicAttack { get; set; }
        public int MagicDefense { get; set; }
        public int MagicAccuracy { get; set; }
        public int MagicResist { get; set; }
        public int Concentration { get; set; }
        public float ConcentrationPercentage { get; set; }
        public float[] ItemStatsPercentage { get; set; }
        public float[] StatsPercentage { get; set; }
        public float[] VitalPercentage { get; set; }
        public float AttackPercentage { get; set; }
        public float DefensePercentage { get; set; }
        public float AccuracyPercentage { get; set; }
        public float EvasionPercentage { get; set; }
        public float ParryPercentage { get; set; }
        public float BlockPercentage { get; set; }
        public float MagicAttackPercentage { get; set; }
        public float MagicDefensePercentage { get; set; }
        public float MagicAccuracyPercentage { get; set; }
        public float MagicResistPercentage { get; set; }
        public float CritRate { get; set; }
        public float CritDamage { get; set; }
        public float ResistCritRate { get; set; }
        public float ResistCritDamage { get; set; }
        public float HealingPower { get; set; }
        public float FinalDamage { get; set; }
        public float Amplification { get; set; }
        public float Enmity { get; set; }
        public float AttackSpeed { get; set; }
        public float CastSpeed { get; set; }
        public int[] ElementAttack { get; set; }
        public float[] ElementAttackPercentage { get; set; }
        public int[] ElementDefense { get; set; }
        public float[] ElementDefensePercentage { get; set; }
        public float PveAttack { get; set; }
        public float PveDefense { get; set; }
        public float PvpAttack { get; set; }
        public float PvpDefense { get; set; }
        public int SilenceResistance { get; set; }
        public int BlindResistance { get; set; }
        public int StunResistance { get; set; }
        public int StumbleResistance { get; set; }

        public float SilenceResistancePercentage { get; set; }
        public float BlindResistancePercentage { get; set; }
        public float StunResistancePercentage { get; set; }
        public float StumbleResistancePercentage { get; set; }
    }
}
