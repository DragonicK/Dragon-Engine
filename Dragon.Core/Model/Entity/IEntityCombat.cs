namespace Dragon.Core.Model.Entity;

public interface IEntityCombat {
    bool IsBufferedSkill { get; set; }
    int BufferedSkillIndex { get; set; }
    int BufferedSkillTime { get; set; }
    void BufferSkill(int index);
    void CastSkill(int index);
    void ClearBufferedSkill();
}