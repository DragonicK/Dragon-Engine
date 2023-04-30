﻿using Dragon.Core.Logs;
using Dragon.Core.Model;

using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Chat.Services;
using Dragon.Chat.Configurations;

using System.Security.Cryptography;

namespace Dragon.Chat.Server;

public sealed class JoinServer {
    public LoggerService? LoggerService { get; init; }
    public IConfiguration? Configuration { get; init; }
    public OutgoingMessageService? OutgoingMessageService { get; init; }

    public void AcceptConnection(IConnection connection) {
        var logger = GetLogger();

        var id = connection is not null ? connection.Id : 0;
        var ipAddress = connection is not null ? connection.IpAddress : string.Empty;

        logger?.Info(GetType().Name, $"Approval Id: {id} IpAddress: {ipAddress}");

        const int CipherKeyLength = 16;

        logger?.Info(GetType().Name, $"Generating Cipher Key Id: {id}");

        connection!.CipherKey = RandomNumberGenerator.GetBytes(CipherKeyLength);

        var writer = OutgoingMessageService!.OutgoingMessageWriter!;

        var msg = writer.CreateMessage(new SpUpdateCipherKey() {
            GameState = GameState.Login,
            Key = connection.CipherKey,
        });

        msg.DestinationPeers.Add(connection!.Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        writer.Enqueue(msg);

        logger?.Info(GetType().Name, $"Cipher Key Sended Id: {id}");
    }

    private ILogger? GetLogger() {
        return LoggerService!.Logger;
    }
}