namespace Dragon.Fonts {
    public class Header {
        public const int MaximumCharacters = 256;

        public Bitmap? Bitmap { get; set; }
        public int BitmapWidth { get; set; }
        public int BitmapHeight { get; set; }
        public int CellWidth { get; set; }
        public int CellHeight { get; set; }
        public byte BaseCharOffset { get; set; }
        public byte[] CharWidth { get; set; }

        public Header() {
            CharWidth = new byte[MaximumCharacters];
        }
    }
}