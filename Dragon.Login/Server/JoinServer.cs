using Dragon.Core.Logs;
using Dragon.Core.Model;

using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Login.Services;
using Dragon.Login.Configurations;

using System.Security.Cryptography;

namespace Dragon.Login.Server;

public sealed class JoinServer {
    public ILogger? Logger { get; init; }
    public IConnection? Connection { get; init; }
    public IConfiguration? Configuration { get; init; }
    public OutgoingMessageService? OutgoingMessageService { get; init; }

    public void AcceptConnection() {
        var id = Connection is not null ? Connection.Id : 0;
        var ipAddress = Connection is not null ? Connection.IpAddress : string.Empty;

        Logger?.Info(GetType().Name, $"Approval Id: {id} IpAddress: {ipAddress}");

        const int CipherKeyLength = 16;

        Logger?.Info(GetType().Name, $"Generating Cipher Key Id: {id}");

        Connection!.CipherKey = RandomNumberGenerator.GetBytes(CipherKeyLength);

        var writer = OutgoingMessageService!.OutgoingMessageWriter!;
 
        var msg = writer.CreateMessage(new SpUpdateCipherKey() {
            GameState = GameState.Login,
            Key = Connection.CipherKey,
        }); 

        msg.DestinationPeers.Add(Connection!.Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        writer.Enqueue(msg);

        Logger?.Info(GetType().Name, $"Cipher Key Sended Id: {id}");
    }
}