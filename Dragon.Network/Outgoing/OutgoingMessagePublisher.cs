namespace Dragon.Network.Outgoing;

public class OutgoingMessagePublisher : IOutgoingMessagePublisher {
    public IConnectionRepository ConnectionRepository { get; }

    public OutgoingMessagePublisher(IConnectionRepository connectionRepository) {
        ConnectionRepository = connectionRepository;
    }

    public void Broadcast(TransmissionTarget peers, IList<int> destination, int exceptDestination, byte[] buffer) {
        switch (peers) {
            case TransmissionTarget.Destination:
                Broadcast(destination, buffer);
                break;

            case TransmissionTarget.Broadcast:
                Broadcast(buffer);
                break;

            case TransmissionTarget.BroadcastExcept:
                Broadcast(destination, exceptDestination, buffer);
                break;
        }
    }

    private void Broadcast(IList<int> destination, int except, byte[] buffer) {
        for (var i = 0; i < destination.Count; i++) {
            var id = destination[i];

            if (except != id) {
                var connection = ConnectionRepository.GetFromId(id);

                if (connection is not null) {
                    if (connection.Connected) {
                        Send(buffer, connection);
                    }
                }
            }
        }
    }

    private void Broadcast(IList<int> destination, byte[] buffer) {
        IConnection connection;

        for (var i = 0; i < destination.Count; i++) {
            connection = ConnectionRepository.GetFromId(destination[i]);

            if (connection is not null) {
                if (connection.Connected) {
                    Send(buffer, connection);
                }
            }
        }
    }

    private void Broadcast(byte[] buffer) {
        foreach (var (id, connection) in ConnectionRepository) {
            if (connection is not null) {
                if (connection.Connected) {
                    Send(buffer, connection);
                }
            }
        }
    }

    private void Send(byte[] buffer, IConnection connection) {
        var length = buffer.Length;
        var crypto = connection.CryptoEngine;

        var tmp = new byte[buffer.Length];

        Buffer.BlockCopy(buffer, 0, tmp, 0, buffer.Length);

        crypto.AppendCheckSum(tmp, 0, length);
        crypto.Cipher(tmp, 4, length);

        IntegerToByteArray(length - 4, tmp, 0);

        connection.Send(tmp, length);
    }
     
    private void IntegerToByteArray(int value, byte[] buffer, int offset) {
        buffer[offset] = (byte)(value & 0xFF);
        buffer[offset + 1] = (byte)(value >> 8 & 0xFF);
        buffer[offset + 2] = (byte)(value >> 16 & 0xFF);
        buffer[offset + 3] = (byte)(value >> 24 & 0xFF);
    }
}