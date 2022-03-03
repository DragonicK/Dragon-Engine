namespace Crystalshire.Core.Model.NotificationIcons {
    public class NotificationIcon {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IconId { get; set; }
        public NotificationIconType IconType { get; set; }

        public NotificationIcon() {
            Name = string.Empty;
            Description = string.Empty;
        }

        public override string ToString() {
            return Name;
        }
    }
}