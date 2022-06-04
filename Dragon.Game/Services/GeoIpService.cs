﻿using Dragon.Core.Logs;
using Dragon.Core.Services;
using Dragon.Core.GeoIpCountry;

namespace Dragon.Game.Services;

public class GeoIpService : IService {
    public ServicePriority Priority => ServicePriority.High;
    public IGeoIpAddress? GeoIpAddress { get; private set; }
    public ConfigurationService? Configuration { get; private set; }

    public void Start() {
        const string File = "./Server/GeoIPCountryWhois.csv";

        if (Configuration is not null) {
            GeoIpAddress = new GeoIpAddress(Configuration.BlockedCountry);

            var reader = new GeoIpAddressReader(GeoIpAddress);
            var success = reader.Read(File);

            if (!success) {
                OutputLog.Write("Failed to read GeoIPCountryWhois");
            }
        }
    }

    public void Stop() {
        GeoIpAddress?.Clear();
    }
}