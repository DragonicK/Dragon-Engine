﻿using Dragon.Core.Services;
using Dragon.Core.GeoIpCountry;

namespace Dragon.Game.Services;

public sealed class GeoIpService : IService {
    public ServicePriority Priority => ServicePriority.High;
    public IGeoIpAddress? GeoIpAddress { get; private set; }
    public LoggerService? LoggerService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }

    public void Start() {
        const string File = "./Server/GeoIPCountryWhois.csv";
        const string Message = "Failed to read GeoIPCountryWhois";

        if (Configuration is not null) {
            GeoIpAddress = new GeoIpAddress(Configuration.BlockedCountry);

            var reader = new GeoIpAddressReader(GeoIpAddress);
            var success = reader.Read(File);

            var logger = LoggerService?.Logger;

            if (!success) {
                logger?.Error(GetType().Name, Message);
            }
        }
    }

    public void Stop() {
        GeoIpAddress?.Clear();
    }
}