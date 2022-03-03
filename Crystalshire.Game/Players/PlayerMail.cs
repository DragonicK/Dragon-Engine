using Crystalshire.Core.Model.Mailing;
using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Game.Players {
    public class PlayerMail : IPlayerMail {
        public int Count => _mails.Count;

        private const int MaximumIndexes = byte.MaxValue;

        private readonly IList<CharacterMail> _mails;
        private readonly long _characterId;

        private readonly HashSet<int> indexes;

        public PlayerMail(long characterId, IList<CharacterMail> mails) {
            _mails = mails;
            _characterId = characterId;

            if (mails is null) {
                _mails = new List<CharacterMail>();
            }

            indexes = new HashSet<int>(MaximumIndexes);

            for (var i = 0; i < _mails.Count; ++i) {
                var index = i + 1;

                _mails[i].Index = index;

                indexes.Add(index);
            }
        }

        public void Add(CharacterMail mail) {
            mail.ReceiverCharacterId = _characterId;

            for (var i = 1; i <= MaximumIndexes; ++i) {
                if (!indexes.Contains(i)) {
                    mail.Index = i;

                    indexes.Add(i);

                    break;
                }
            }

            _mails.Add(mail);
        }

        public MailingOperationCode Delete(int index) {
            var selected = _mails.FirstOrDefault(x => x.Index == index);

            if (selected is not null) {
                if (IsSomethingAttached(selected)) {
                    var currency = selected.AttachCurrency;
                    var receivedCurrency = selected.AttachCurrencyReceiveFlag;
                    var receivedItem = selected.AttachItemReceiveFlag;

                    if (!receivedItem) {
                        return MailingOperationCode.AttachedNotReceived;
                    }

                    if (!receivedCurrency && currency > 0) {
                        return MailingOperationCode.AttachedNotReceived;
                    }
                }

                selected.DeleteFlag = true;
                selected.DeleteDate = DateTime.Now;
                selected.Index = 0;

                indexes.Remove(index);

                return MailingOperationCode.Deleted;
            }

            return MailingOperationCode.Invalid;
        }

        public void UpdateReadFlag(int index) {
            var selected = _mails.FirstOrDefault(x => x.Index == index);

            if (selected is not null) {
                selected.ReadFlag = true;
            }
        }

        public CharacterMail? Get(int index) {
            return _mails.FirstOrDefault(x => x.Index == index); ;
        }

        public IList<CharacterMail> ToList() {
            return _mails;
        }

        private bool IsSomethingAttached(CharacterMail mail) {
            return mail.AttachItemFlag || mail.AttachCurrency > 0;
        }
    }
}