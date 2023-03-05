using Dragon.Network;
using Dragon.Network.Outgoing;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Database;
using Dragon.Database.Handler;

using Dragon.Core.Jwt;
using Dragon.Core.Logs;
using Dragon.Core.Model;
using Dragon.Core.Common;

using Dragon.Core.Cryptography;
using Dragon.Core.Model.Accounts;

using Dragon.Login.Services;

namespace Dragon.Login.Routes;

public sealed class Authentication {
    public IConnection? Connection { get; set; }
    public CpAuthentication? Packet { get; set; }
    public OutgoingMessageService? OutgoingMessageService { get; init; }
    public ConfigurationService? Configuration { get; init; }
    public DatabaseService? DatabaseService { get; init; }
    public LoggerService? LoggerService { get; init; }

    private IDatabaseFactory? factory;
    private Account? account;

    public async void Process() {
        var logger = GetLogger();

        if (Packet is not null) {
            var token = string.Empty;
            var version = Packet.Version;
            var username = Packet.Username;
            var passphrase = Packet.Passphrase;

            factory = GetFactory();

            try {
                using var handler = factory.GetMembershipHandler(Configuration!.DatabaseMembership);
                var result = await GetAuthenticationResultAsync(handler, version, username, passphrase);

                if (result == AuthenticationResult.Success) {
                    token = GenerateToken();

                    SendResult(token);

                    await UpdateLastLoginDataAsync(handler);
                }
                else {
                    SendResult(result);
                }

                await WriteTokenLog(result, username, token);
            }
            catch (Exception ex) {
                await WriteExceptionLog(username, ex.Message);
            }
        }
        else {
            logger?.Error(GetType().Name, "Packet Failed: Authentication Login");
        }
    }

    private string GenerateToken() {
        var tokenData = new JwtTokenData() {
            CharacterId = 0,
            Username = account!.Username,
            AccountId = account!.AccountId,
            AccountLevel = account!.AccountLevelCode
        };

        var handler = new JwtTokenHandler(Configuration!.JwtSettings);

        return handler.GerenateToken(tokenData);
    }

    private async Task<AuthenticationResult> GetAuthenticationResultAsync(MembershipHandler handler, ClientVersion version, string username, string passphrase) {
        if (Configuration!.Maintenance) {
            return AuthenticationResult.Maintenance;
        }

        if (Configuration!.ClientVersion != version) {
            return AuthenticationResult.VersionOutdated;
        }

        account = await handler.GetAccountWithLockAsync(username);

        if (account is null) {
            return AuthenticationResult.WrongUserData;
        }

        if (account.AccountLock is not null) {
            if (!IsBanExpired()) {
                return AuthenticationResult.AccountIsBanned;
            }
        }

        if (account.ActivatedCode == 0) {
            return AuthenticationResult.AccountIsNotActivated;
        }

        if (!IsPassphraseOk(passphrase)) {
            return AuthenticationResult.WrongUserData;
        }

        return AuthenticationResult.Success;
    }

    private bool IsPassphraseOk(string passphrase) {
        var salt = Hash.ComputeToHex(account?.Username + passphrase);
        var password = Hash.ComputeToHex(passphrase + salt);

        return account!.Passphrase.CompareTo(password) == 0;
    }

    private bool IsBanExpired() {
        var isExpired = true;

        account?.AccountLock?.ForEach(
            (x) => {
                if (!x.Expired && !x.Permanent) {
                    x.Expired = IsDateExpired(x.ExpireDate);

                    if (!x.Expired) {
                        isExpired = false;
                    }
                }

                if (x.Permanent) {
                    isExpired = false;
                }
            }
        );

        return isExpired;
    }

    private Task<int> UpdateLastLoginDataAsync(MembershipHandler handler) {
        account!.LastLoginDate = DateTime.Now;
        account!.LastLoginIp = Connection!.IpAddress;

        return handler.PutAccountAsync(account);
    }

    private static bool IsDateExpired(DateTime date) {
        return DateTime.Now.CompareTo(date) == 1;
    }

    private void SendResult(string token) {
        var writer = GetMessageWriter();

        var p = new SpAuthenticationResult() {
            IpAddress = Configuration!.GameServer.Ip,
            Port = Configuration!.GameServer.Port,
            Token = token
        };

        var msg = writer!.CreateMessage(p);

        msg.DestinationPeers.Add(Connection!.Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        writer.Enqueue(msg);
    }

    private void SendResult(AuthenticationResult result) {
        var writer = GetMessageWriter();

        var packet = new SpAlertMessage() {
            AlertMessage = GetAlertMessage(result)
        };

        var msg = writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(Connection!.Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        writer.Enqueue(msg);
    }

    private Task WriteTokenLog(AuthenticationResult result, string username, string uniqueKey) {
        var logger = GetLogger();

        if (uniqueKey.Length > 0) {
            logger?.Warning(GetType().Name, $"Authentication: {GetString(result)} {username}");
        }

        return Task.CompletedTask;
    }

    private Task WriteExceptionLog(string username, string message) {
        var logger = GetLogger();

        logger?.Error(GetType().Name, $"Authentication: An error ocurred by {username} ... {message}");

        return Task.CompletedTask;
    }

    private IDatabaseFactory GetFactory() {
        return DatabaseService!.DatabaseFactory!;
    }

    private ILogger? GetLogger() {
        return LoggerService!.Logger;
    }

    private IOutgoingMessageWriter GetMessageWriter() {
        return OutgoingMessageService!.OutgoingMessageWriter!;
    }

    public static AlertMessageType GetAlertMessage(AuthenticationResult authentication) => authentication switch {
        AuthenticationResult.AccountIsBanned => AlertMessageType.AccountIsBanned,
        AuthenticationResult.AccountIsNotActivated => AlertMessageType.AccountIsNotActivated,
        AuthenticationResult.Failed => AlertMessageType.Failed,
        AuthenticationResult.Maintenance => AlertMessageType.Maintenance,
        AuthenticationResult.VersionOutdated => AlertMessageType.VersionOutdated,
        AuthenticationResult.WrongUserData => AlertMessageType.WrongAccountData,
        _ => AlertMessageType.None
    };

    public static string GetString(AuthenticationResult authentication) => authentication switch {
        AuthenticationResult.AccountIsBanned => "Account Is Banned",
        AuthenticationResult.AccountIsNotActivated => "Account Is Not Activated",
        AuthenticationResult.Failed => "Failed",
        AuthenticationResult.Success => "Success",
        AuthenticationResult.Maintenance => "Maintenance",
        AuthenticationResult.VersionOutdated => "Version Outdated",
        AuthenticationResult.WrongUserData => "Wrong User Data",
        _ => string.Empty
    };
}