using Dragon.Core.Model.Mailing;
using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

public interface IPlayerMail {
    int Count { get; }
    void Add(CharacterMail mail);
    MailingOperationCode Delete(int index);
    void UpdateReadFlag(int index);
    CharacterMail? Get(int index);
    IList<CharacterMail> ToList();
}