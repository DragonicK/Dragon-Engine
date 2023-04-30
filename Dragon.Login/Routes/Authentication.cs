using Dragon.Network;
using Dragon.Network.Outgoing;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Database;
using Dragon.Database.Handler;

using Dragon.Core.Jwt;
using Dragon.Core.Logs;
using Dragon.Core.Model;
using Dragon.Core.Common;
using Dragon.Core.Services;
using Dragon.Core.Cryptography;
using Dragon.Core.Model.Accounts;

using Dragon.Login.Network;

namespace Dragon.Login.Routes;

public sealed class Authentication : PacketRoute, IPacketRoute {
    public MessageHeader Header =>  MessageHeader.Authentication;

    public Authentication(IServiceInjector injector) : base(injector) { }

    public async void Process(IConnection connection, object packet) {
        var received = packet as CpAuthentication;
        var logger = GetLogger();
        
        if (received is not null) {
            var token = string.Empty;
            var version = received.Version;
            var username = received.Username;
            var passphrase = received.Passphrase;

            var factory = GetFactory();

            try {
                using var handler = factory.GetMembershipHandler(ConfigurationService!.DatabaseMembership);

                var account = await GetAccountAsync(handler, username);

                var result = GetAuthenticationResult(version, account, passphrase);

                if (result == AuthenticationResult.Success) {
                    token = GenerateToken(account);

                    SendResult(connection, token);

                    await UpdateLastLoginDataAsync(connection, handler, account);
                }
                else {
                    SendResult(connection, result);
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

    private string GenerateToken(Account account) {
        var tokenData = new JwtTokenData() {
            CharacterId = 0,
            Username = account!.Username,
            AccountId = account!.AccountId,
            AccountLevel = account!.AccountLevelCode
        };

        var handler = new JwtTokenHandler(ConfigurationService!.JwtSettings);

        return handler.GerenateToken(tokenData);
    }

    private async Task<Account> GetAccountAsync(MembershipHandler handler, string username) {
        return await handler.GetAccountWithLockAsync(username);
    }

    private AuthenticationResult GetAuthenticationResult(ClientVersion version, Account? account, string passphrase) {
        if (ConfigurationService!.Maintenance) {
            return AuthenticationResult.Maintenance;
        }

        if (ConfigurationService!.ClientVersion != version) {
            return AuthenticationResult.VersionOutdated;
        }

        if (account is null) {
            return AuthenticationResult.WrongUserData;
        }

        if (account.AccountLock is not null) {
            if (!IsBanExpired(account)) {
                return AuthenticationResult.AccountIsBanned;
            }
        }

        if (account.ActivatedCode == 0) {
            return AuthenticationResult.AccountIsNotActivated;
        }

        if (!IsPassphraseOk(account, passphrase)) {
            return AuthenticationResult.WrongUserData;
        }

        return AuthenticationResult.Success;
    }

    private bool IsPassphraseOk(Account account, string passphrase) {
        var salt = Hash.ComputeToHex(account?.Username + passphrase);
        var password = Hash.ComputeToHex(passphrase + salt);

        return account?.Passphrase.CompareTo(password) == 0;
    }

    private bool IsBanExpired(Account account) {
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

    private Task<int> UpdateLastLoginDataAsync(IConnection connection, MembershipHandler handler, Account account) {
        account!.LastLoginDate = DateTime.Now;
        account!.LastLoginIp = connection.IpAddress;

        return handler.PutAccountAsync(account);
    }

    private static bool IsDateExpired(DateTime date) {
        return DateTime.Now.CompareTo(date) == 1;
    }

    private void SendResult(IConnection connection, string token) {
        var writer = GetMessageWriter();

        var server = ConfigurationService!.GameServer;

        var p = new SpAuthenticationResult() {
            IpAddress = server.Ip,
            Port = server.Port,
            Token = token
        };

        var msg = writer!.CreateMessage(p);

        msg.DestinationPeers.Add(connection!.Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        writer.Enqueue(msg);
    }

    private void SendResult(IConnection connection, AuthenticationResult result) {
        var writer = GetMessageWriter();

        var packet = new SpAlertMessage() {
            AlertMessage = GetAlertMessage(result)
        };

        var msg = writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(connection!.Id);
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