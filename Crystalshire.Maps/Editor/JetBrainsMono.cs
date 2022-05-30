﻿namespace Crystalshire.Maps.Editor;
public class JetBrainsMono {
    private readonly FontLoader _loader;

    private Font? _regular;
    private Font? _bold;
    private Font? _italic;

    public JetBrainsMono(FontLoader loader) {
        _loader = loader;
    }

    public Font GetFont(FontStyle style) => style switch {
        FontStyle.Regular => GetRegular(),
        FontStyle.Bold => GetBold(),
        FontStyle.Italic => GetItalic(),
        _ => GetRegular()
    };

    private Font GetRegular() {
        if (_regular is null) {
            _regular = _loader.GetFont(FontStyle.Regular);
        }

        return _regular;
    }

    private Font GetBold() {
        if (_bold is null) {
            _bold = _loader.GetFont(FontStyle.Bold);
        }

        return _bold;
    }

    private Font GetItalic() {
        if (_italic is null) {
            _italic = _loader.GetFont(FontStyle.Italic);
        }

        return _italic;
    }
}