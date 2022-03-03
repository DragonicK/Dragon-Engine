using Crystalshire.Core.Model.Mailing;
using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Game.Players {
    public interface IPlayerMail {
        int Count { get; }
        void Add(CharacterMail mail);
        MailingOperationCode Delete(int index);
        void UpdateReadFlag(int index);
        CharacterMail? Get(int index);
        IList<CharacterMail> ToList();
    }
}