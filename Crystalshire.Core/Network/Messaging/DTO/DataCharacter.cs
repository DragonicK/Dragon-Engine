namespace Crystalshire.Core.Network.Messaging.DTO {
    public struct DataCharacter {
        
        public string Name { get; set; }

        
        public int Model { get; set; }

        
        public int Class { get; set; }

        
        public bool PendingExclusion { get; set; }

        
        public int Hour { get; set; }

        
        public int Minute { get; set; }

        
        public int Second { get; set; }
    }
}