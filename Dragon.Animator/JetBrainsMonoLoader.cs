using System.Drawing.Text;

namespace Dragon.Animator;

public class JetBrainsMonoLoader {
    readonly PrivateFontCollection _collection;

    public JetBrainsMonoLoader() {
        _collection = new PrivateFontCollection();
    }

    public void LoadFromResource() {
        LoadFromResource(Properties.Resources.JetBrainsMono_Bold);
        LoadFromResource(Properties.Resources.JetBrainsMono_Regular);
        LoadFromResource(Properties.Resources.JetBrainsMono_Bold_Italic);
        LoadFromResource(Properties.Resources.JetBrainsMono_ExtraBold);
        LoadFromResource(Properties.Resources.JetBrainsMono_ExtraBold_Italic);
        LoadFromResource(Properties.Resources.JetBrainsMono_Italic);
        LoadFromResource(Properties.Resources.JetBrainsMono_Medium);
        LoadFromResource(Properties.Resources.JetBrainsMono_Medium_Italic);
    }

    public Font GetFont(FontStyle style, float size) {
        return new Font(_collection.Families[0], size, style, GraphicsUnit.Pixel, 0);
    }

    private void LoadFromResource(byte[] buffer) {
        unsafe {
            fixed (byte* p = buffer) {
                _collection.AddMemoryFont((IntPtr)p, buffer.Length);
            }
        }
    }
}