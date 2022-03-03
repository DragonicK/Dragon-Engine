using Crystalshire.Core.Jwt;
using Crystalshire.Core.Logs;
using Crystalshire.Core.Model;
using Crystalshire.Core.Common;
using Crystalshire.Core.Network;
using Crystalshire.Core.Database;
using Crystalshire.Core.Cryptography;
using Crystalshire.Core.Model.Accounts;
using Crystalshire.Core.Database.Handler;
using Crystalshire.Core.Network.Outgoing;
using Crystalshire.Core.Network.Messaging.SharedPackets;

using Crystalshire.Login.Services;

namespace Crystalshire.Login.Routes {
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
                if (Configuration!.Debug) {
                    OutputLog.Write("Packet Failed: Authentication Login");
                }
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
            if (uniqueKey.Length > 0) {
                var logger = GetLogger();

                var description = new Description() {
                    Name = "Authentication Result",
                    WarningCode = WarningCode.Success,
                    Message = $"Authentication Result: {result} ConnectionId: {Connection?.Id} IpAddress: {Connection?.IpAddress}",
                };

                logger?.Write(description);

                if (Configuration is not null) {
                    if (Configuration.Debug) {
                        OutputLog.Write($"Authentication Result: {GetString(result)} {username} {uniqueKey}");
                    }
                }
            }

            return Task.CompletedTask;
        }

        private Task WriteExceptionLog(string username, string message) {
            var logger = GetLogger();

            var description = new Description() {
                Name = "Authentication Excpetion",
                WarningCode = WarningCode.Error,
                Message = $"An error ocurred by {username} ... {message}",
            };

            logger?.Write(description);

            if (Configuration is not null) {
                if (Configuration.Debug) {
                    OutputLog.Write($"Authentication throw an exception ... ");
                }
            }

            return Task.CompletedTask;
        }

        private IDatabaseFactory GetFactory() {
            return DatabaseService!.DatabaseFactory!;
        }

        private IOutgoingMessageWriter GetMessageWriter() {
            return OutgoingMessageService!.OutgoingMessageWriter!;
        }

        private ILogger GetLogger() {
            return LoggerService!.ServerLogger!;
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
            AuthenticationResult.VersionOutdated => "VersionOutdated",
            AuthenticationResult.WrongUserData => "Wrong User Data",
            _ => string.Empty
        };
    }
}