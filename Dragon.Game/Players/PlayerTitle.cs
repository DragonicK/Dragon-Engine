using Dragon.Core.Content;
using Dragon.Core.Model.Titles;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.Characters;
using Dragon.Core.Model.Attributes;

namespace Dragon.Game.Players;

public sealed class PlayerTitle : IPlayerTitle {
    public IEntityAttribute Attributes { get; }
    public IDatabase<Title>? Titles { get; set; }
    public IDatabase<GroupAttribute>? TitleAttributes { get; set; }

    public int Count => _titles.Count;

    private long characterId;
    private readonly GroupAttribute defaultUpgrade;
    private readonly IList<CharacterTitle> _titles;


    private const int FirstLevel = 1;

    public PlayerTitle(long characterId, IList<CharacterTitle> titles) {
        this._titles = titles;
        this.characterId = characterId;

        defaultUpgrade = new GroupAttribute();
        Attributes = new EntityAttribute();
    }

    public bool Add(int id) {
        if (Contains(id)) {
            return false;
        }

        _titles.Add(new CharacterTitle() {
            CharacterId = characterId,
            TitleId = id
        });

        return true;
    }

    public bool Remove(int id) {
        var selected = _titles.FirstOrDefault(p => p.TitleId == id);

        if (selected is not null) {
            _titles.Remove(selected);
        }

        return selected is not null;
    }

    public int GetId(int index) {
        return _titles[index].TitleId;
    }

    public bool Contains(int id) {
        var selected = _titles.FirstOrDefault(p => p.TitleId == id);

        return selected is not null;
    }

    public int[] ToArrayId() {
        return _titles.Select(p => p.TitleId).ToArray();
    }

    public IList<CharacterTitle> ToList() {
        return _titles;
    }

    public void UpdateAttributes() {
        Attributes.Clear();

        foreach(var inventory in _titles) {
            var id = inventory.TitleId;

            if (id > 0) {
                var title = GetTitleData(id);

                if (title is not null) {
                    var attributes = GetAttributeData(title.Id);

                    if (attributes is not null) {
                        Attributes.Clear();
                        Attributes.Add(FirstLevel, attributes, defaultUpgrade);
                    }
                }
            }
        } 
    }

    private Title? GetTitleData(int id) {
        if (Titles is not null) {
            Titles.TryGet(id, out var title);

            return title;
        }

        return null;
    }

    private GroupAttribute? GetAttributeData(int id) {
        if (TitleAttributes is not null) {
            TitleAttributes.TryGet(id, out var attribute);

            return attribute;
        }

        return null;
    }
}