namespace Dragon.Network.Outgoing;

public class OutgoingMessagePublisher : IOutgoingMessagePublisher {
    public IConnectionRepository ConnectionRepository { get; }

    public OutgoingMessagePublisher(IConnectionRepository connectionRepository) {
        ConnectionRepository = connectionRepository;
    }

    public void Broadcast(TransmissionTarget peers, IList<int> destination, int exceptDestination, byte[] buffer, int length) {
        switch (peers) {
            case TransmissionTarget.Destination:
                Broadcast(ref destination, ref buffer, length);
                break;

            case TransmissionTarget.Broadcast:
                Broadcast(ref buffer, length);
                break;

            case TransmissionTarget.BroadcastExcept:
                Broadcast(ref destination, exceptDestination, ref buffer, length);
                break;
        }
    }

    private void Broadcast(ref IList<int> destination, int except, ref byte[] buffer, int length) {
        for (var i = 0; i < destination.Count; i++) {
            var id = destination[i];

            if (except != id) {
                var connection = ConnectionRepository.GetFromId(id);

                if (connection is not null) {
                    if (connection.Connected) {
                        Send(ref buffer, connection, length);
                    }
                }
            }
        }
    }

    private void Broadcast(ref IList<int> destination, ref byte[] buffer, int length) {
        IConnection connection;

        for (var i = 0; i < destination.Count; i++) {
            connection = ConnectionRepository.GetFromId(destination[i]);

            if (connection is not null) {
                if (connection.Connected) {
                    Send(ref buffer, connection, length);
                }
            }
        }
    }

    private void Broadcast(ref byte[] buffer, int length) {
        foreach (var (_, connection) in ConnectionRepository) {
            if (connection is not null) {
                if (connection.Connected) {
                    Send(ref buffer, connection, length);
                }
            }
        }
    }

    private void Send(ref byte[] buffer, IConnection connection, int length) {
        var crypto = connection.CryptoEngine;

        var tmp = new byte[length];

        Buffer.BlockCopy(buffer, 0, tmp, 0, length);

        crypto.AppendCheckSum(tmp, 0, length);
        crypto.Cipher(tmp, 4, length);

        IntegerToByteArray(length - 4, ref tmp, 0);

        connection.Send(tmp, length);

        if (!connection.CryptoEngine.IsKeyAlreadyUdpated) {
            connection.UpdateKey(connection.CipherKey);

            connection.CryptoEngine.IsKeyAlreadyUdpated = true;
        }
    }
     
    private void IntegerToByteArray(int value, ref byte[] buffer, int offset) {
        buffer[offset] = (byte)(value & 0xFF);
        buffer[offset + 1] = (byte)(value >> 8 & 0xFF);
        buffer[offset + 2] = (byte)(value >> 16 & 0xFF);
        buffer[offset + 3] = (byte)(value >> 24 & 0xFF);
    }
}