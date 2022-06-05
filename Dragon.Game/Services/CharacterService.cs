using Dragon.Database;

using Dragon.Core.Logs;
using Dragon.Core.Services;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Characters;
using Dragon.Game.Repository;

namespace Dragon.Game.Services;

public class CharacterService : IService, IUpdatableService {
    public ServicePriority Priority => ServicePriority.Low;
    public IPlayerRepository? Repository { get; private set; }
    public IDatabaseFactory? Factory { get; private set; }
    public ILogger? Logger { get; private set; }
    public PacketSenderService? PacketSenderService { get; set; }
    public ConfigurationService? Configuration { get; set; }
    public ConnectionService? ConnectionService { get; set; }
    public DatabaseService? DatabaseService { get; set; }
    public LoggerService? LoggerService { get; set; }

    private IList<IDeleteRequest>? requests;
    private IList<IDeleteRequest>? completed;
    private Queue<long>? canceled;

    private const int MaxRequests = 32;

    public void Start() {
        requests = new List<IDeleteRequest>(MaxRequests);
        completed = new List<IDeleteRequest>(MaxRequests);
        canceled = new Queue<long>(MaxRequests);

        Repository = ConnectionService!.PlayerRepository!;
        Factory = DatabaseService!.DatabaseFactory!;
        Logger = LoggerService!.Logger;
    }

    public void Stop() {
        requests?.Clear();
    }

    public void AddExclusion(CharacterDeleteRequest dbRequest) {
        var shouldBeZero = false;

        if (DateTime.Now > (DateTime)dbRequest.ExclusionDate!) {
            dbRequest.ExclusionDate = DateTime.Now;
            shouldBeZero = true;
        }

        var date = DateTime.Now.Subtract((DateTime)dbRequest.ExclusionDate);
        var seconds = shouldBeZero ? 0 : (int)date.TotalSeconds;

        var request = new DeleteRequest() {
            Id = dbRequest.Id,
            Name = dbRequest.Name,
            AccountId = dbRequest.AccountId,
            RequestDate = dbRequest.RequestDate,
            CharacterId = dbRequest.CharacterId,
            ExclusionDate = dbRequest.ExclusionDate,
            RemainingSeconds = seconds
        };

        requests?.Add(request);
    }

    public async Task<CharacterDeleteRequest> AddExclusion(CharacterPreview character, IPlayer player, int minutes) {
        var request = new DeleteRequest() {
            AccountId = player.AccountId,
            Name = character.Name,
            RequestDate = DateTime.Now,
            RemainingSeconds = minutes * 60,
            CharacterId = character.CharacterId,
            ExclusionDate = DateTime.Now.AddMinutes(minutes),
        };

        var dbRequest = await AddDeleteRequest(request, minutes > 0, player.IpAddress, player.MachineId);

        requests?.Add(request);

        return dbRequest;
    }

    public void CancelExclusion(long characterId) {
        canceled?.Enqueue(characterId);
    }

    public void Update(int deltaTime) {
        CancelRequests();
        ProcessRequests();
        ExecuteExclusion();
    }

    public bool IsAdded(long characterId) {
        return requests?.FirstOrDefault(x => x.CharacterId == characterId) is not null;
    }

    private void ProcessRequests() {
        var count = requests?.Count;

        if (requests is not null) {
            for (var i = 0; i < count; ++i) {
                var request = requests[i];

                request.Decrease();

                if (request.CanDelete()) {
                    completed?.Add(request);
                }
            }
        }
    }

    private void CancelRequests() {
        var count = canceled!.Count;

        for (var i = 0; i < count; ++i) {
            var characterId = canceled.Dequeue();

            var request = requests?.FirstOrDefault(x => x.CharacterId == characterId);

            if (request is not null) {
                requests?.Remove(request);
            }
        }
    }

    private void ExecuteExclusion() {
        var count = completed!.Count;

        for (var i = 0; i < count; ++i) {
            var complete = completed[i];

            Task.Run(async () => {
                var database = new CharacterDatabase(Configuration!, Factory!);

                await database.DeleteCharacterAsync(complete.CharacterId);
                await database.UpdateDeleteRequest(complete.Id);

                var player = Repository!.FindByAccountId(complete.AccountId);

                if (player is not null) {
                    player.Characters = await database.GetCharactersPreviewAsync(player.AccountId);

                    var sender = PacketSenderService!.PacketSender;
                    sender?.SendCharacters(player);
                }

                database.Dispose();
            });

            requests?.Remove(complete);
        }

        completed.Clear();
    }

    private async Task<CharacterDeleteRequest> AddDeleteRequest(IDeleteRequest request, bool isActive, string ipAddress, string machineId) {
        var database = new CharacterDatabase(Configuration!, Factory!);

        var dbRequest = await database.AddCharacterDeleteRequest(request, isActive, ipAddress, machineId);

        request.Id = dbRequest.Id;

        database.Dispose();

        return dbRequest;
    }
}