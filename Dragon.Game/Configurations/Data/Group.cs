namespace Dragon.Game.Configurations.Data;

public sealed class Group {
    public int Maximum { get; set; }
    public int MaximumLevel { get; set; }
    public int MaximumMembers { get; set; }
    public int InviteTimeOut { get; set; }
    public int DisconnectionTimeOut { get; set; }
    public float AdditionalExperiencePerMember { get; set; }
    public float AdditionalExperiencePerLevel { get; set; }

    public Dictionary<int, float> ExperienceByPartyMemberCount { get; set; }

    public Group() {
        Maximum = 255;
        MaximumLevel = 50;
        InviteTimeOut = 15;
        MaximumMembers = 5;
        DisconnectionTimeOut = 300;
        AdditionalExperiencePerLevel = 0.01f;

        ExperienceByPartyMemberCount = new Dictionary<int, float>() {
            { 1, 1 },
            { 2, 1f },
            { 3, 1.5f },
            { 4, 2.0f },
            { 5, 2.5f }
        };
    }
}