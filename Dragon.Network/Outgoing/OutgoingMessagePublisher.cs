using Dragon.Network.Pool;

namespace Dragon.Network.Outgoing;

public class OutgoingMessagePublisher : IOutgoingMessagePublisher {
    public IConnectionRepository ConnectionRepository { get; }

    public OutgoingMessagePublisher(IConnectionRepository connectionRepository) {
        ConnectionRepository = connectionRepository;
    }

    public void Broadcast(TransmissionTarget peers, IList<int> destination, int exceptDestination, IEngineBufferWriter buffer) {
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

    private void Broadcast(IList<int> destination, int except, IEngineBufferWriter buffer) {
        for (var i = 0; i < destination.Count; i++) {
            var id = destination[i];

            if (except != id) {
                var connection = ConnectionRepository.GetFromId(id);

                if (connection is not null) {
                    if (connection.Connected) {
                        Send(connection, buffer);
                    }
                }
            }
        }
    }

    private void Broadcast(IList<int> destination, IEngineBufferWriter buffer) {
        for (var i = 0; i < destination.Count; i++) {
            var connection = ConnectionRepository.GetFromId(destination[i]);

            if (connection is not null) {
                if (connection.Connected) {
                    Send(connection, buffer);
                }
            }
        }
    }

    private void Broadcast(IEngineBufferWriter buffer) {
        foreach (var (_, connection) in ConnectionRepository) {
            if (connection is not null) {
                if (connection.Connected) {
                    Send(connection, buffer);
                }
            }
        }
    }

    private void Send(IConnection connection, IEngineBufferWriter buffer) {
        var crypto = connection.CryptoEngine;

        var length = buffer.Length;

        var tmp = new byte[length];

        Buffer.BlockCopy(buffer.Content, 0, tmp, 0, length);

        crypto.AppendCheckSum(tmp, 0, length);
        crypto.Cipher(tmp, 4, length);

        IntegerToByteArray(length - 4, tmp, 0);

        connection.Send(tmp, length);

        if (!connection.CryptoEngine.IsKeyAlreadyUdpated) {
            connection.UpdateKey(connection.CipherKey);

            connection.CryptoEngine.IsKeyAlreadyUdpated = true;
        }
    }
     
    private void IntegerToByteArray(int value, byte[] buffer, int offset) {
        buffer[offset] = (byte)(value & 0xFF);
        buffer[offset + 1] = (byte)(value >> 8 & 0xFF);
        buffer[offset + 2] = (byte)(value >> 16 & 0xFF);
        buffer[offset + 3] = (byte)(value >> 24 & 0xFF);
    }
}