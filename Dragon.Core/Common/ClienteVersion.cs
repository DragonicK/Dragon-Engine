namespace Dragon.Core.Common;

public struct ClientVersion {

    public int ClientMajor { get; set; }

    public int ClientMinor { get; set; }

    public int ClientRevision { get; set; }

    private bool Compare(ClientVersion version) {
        if (ClientMajor != version.ClientMajor) {
            return false;
        }

        if (ClientMinor != version.ClientMinor) {
            return false;
        }

        if (ClientRevision != version.ClientRevision) {
            return false;
        }

        return true;
    }

    public override bool Equals(object? obj) {
        if (obj is ClientVersion version) {
            return Compare(version);
        }

        return false;
    }

    public static bool operator ==(ClientVersion left, ClientVersion right) {
        return left.Equals(right);
    }

    public static bool operator !=(ClientVersion left, ClientVersion right) {
        return !(left == right);
    }

    public override int GetHashCode() {
        return HashCode.Combine(ClientMajor, ClientMinor, ClientRevision);
    }
}