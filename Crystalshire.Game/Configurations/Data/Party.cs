namespace Crystalshire.Game.Configurations.Data;

public class Party {
    public int Maximum { get; set; }
    public int MaximumLevel { get; set; }
    public int MaximumMembers { get; set; }
    public int InviteTimeOut { get; set; }
    public int DisconnectionTimeOut { get; set; }
    public float AdditionalExperiencePerMember { get; set; }
    public float AdditionalExperiencePerLevel { get; set; }

    public Party() {
        Maximum = 255;
        MaximumLevel = 50;
        InviteTimeOut = 15;
        MaximumMembers = 5;
        DisconnectionTimeOut = 300;
        AdditionalExperiencePerLevel = 0.01f;
        AdditionalExperiencePerMember = 0.1f;
    }
}