namespace Crystalshire.Network.Outgoing {
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
            IConnection connection;
            int id;

            for (var i = 0; i < destination.Count; i++) {
                id = destination[i];

                if (except != id) {
                    connection = ConnectionRepository.GetFromId(id);

                    if (connection is not null) {
                        if (connection.Connected) {
                            connection.Send(buffer);
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
                        connection.Send(buffer);
                    }
                }
            }
        }

        private void Broadcast(byte[] buffer) {
            foreach (var (id, connection) in ConnectionRepository) {
                if (connection is not null) {
                    if (connection.Connected) {
                        connection.Send(buffer);
                    }
                }
            }
        }

    }
}