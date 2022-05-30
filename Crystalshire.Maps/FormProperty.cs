using Crystalshire.Maps.Model;
using Crystalshire.Maps.Editor;

using Crystalshire.Core.Model.Maps;

namespace Crystalshire.Maps;

public partial class FormProperty : Form {

    private readonly JetBrainsMono _jetBrainsMono;
    private readonly FormMain _main;

    private readonly IProperty _property;

    private readonly int lWidth;
    private readonly int lHeight;

    public FormProperty(JetBrainsMono jetBrainsMono, IProperty property, FormMain main) {
        InitializeComponent();

        _jetBrainsMono = jetBrainsMono;
        _property = property;
        _main = main;

        lHeight = _property.Height;
        lWidth = _property.Width;

        FillComboBox<Moral>(ComboMoral);
        FillComboBox<Blending>(ComboBlending);
        FillComboBox<Weather>(ComboWeather);

        ChangeFont(GroupProperty);

        UpdateProperties();
    }

    private void FillComboBox<T>(ComboBox combo) {
        var names = Enum.GetNames(typeof(T));

        for (var i = 0; i < names.Length; i++) {
            combo.Items.Add(names[i]);
        }

        if (names is not null) {
            if (names.Length > 0) {
                combo.SelectedIndex = 0;
            }
        }
    }

    private void ChangeFont(Control control) {
        var controls = control.Controls;

        ChangeFontStye(control);

        foreach (Control _control in controls) {
            ChangeFontStye(_control);
        }
    }

    private void ChangeFontStye(Control control) {
        control.Font = _jetBrainsMono.GetFont(FontStyle.Regular);
    }

    private void FormProperty_FormClosing(object sender, FormClosingEventArgs e) {
        if (lWidth != _property.Width || lHeight != _property.Height) {
            e.Cancel = true;

            _main.UpdateMapSize();

            e.Cancel = false;
        }
    }

    private void UpdateProperties() {
        TextName.Text = _property.Name;

        TextUp.Text = _property.Link.Up.ToString();
        TextLeft.Text = _property.Link.Left.ToString();
        TextRight.Text = _property.Link.Right.ToString();
        TextDown.Text = _property.Link.Down.ToString();

        ComboWeather.SelectedIndex = (int)_property.Weather;
        ComboMoral.SelectedIndex = (int)_property.Moral;

        TextBootId.Text = _property.Boot.Id.ToString();
        TextBootX.Text = _property.Boot.X.ToString();
        TextBootY.Text = _property.Boot.Y.ToString();

        TextMusic.Text = _property.Music;
        TextAmbience.Text = _property.Ambience;

        TextWidth.Text = _property.Width.ToString();
        TextHeight.Text = _property.Height.ToString();

        TextFogId.Text = _property.Fog.Id.ToString();
        ComboBlending.SelectedIndex = (int)_property.Fog.Blending;

        LabelOpacity.Text = $"Opacity: {_property.Fog.Opacity}";
        ScrollOpacity.Value = _property.Fog.Opacity;

        LabelRed.Text = $"Red: {_property.Fog.Red}";
        ScrollRed.Value = _property.Fog.Red;

        LabelGreen.Text = $"Green: {_property.Fog.Green}";
        ScrollGreen.Value = _property.Fog.Green;

        LabelBlue.Text = $"Blue: {_property.Fog.Blue}";
        ScrollBlue.Value = _property.Fog.Blue;

        TextKeyA.Text = _property.KeyA;
        TextKeyB.Text = _property.KeyB;
        TextKeyC.Text = _property.KeyC;
    }

    private int GetValue(TextBox text) {
        _ = int.TryParse(text.Text.Trim(), out var value);

        return value;
    }

    private void ButtonCopy_Click(object sender, EventArgs e) {
        Clipboard.SetText(_property.GetHashText());
    }

    #region Name

    private void TextName_TextChanged(object sender, EventArgs e) {
        if (_property is not null) {
            _property.Name = TextName.Text.Trim();
            _main.ChangeSelectedMapName(_property.Name);
        }
    }

    #endregion

    #region Link

    private void TextUp_TextChanged(object sender, EventArgs e) {
        if (_property is not null) {
            var link = _property.Link;

            link.Up = GetValue(TextUp);

            _property.Link = link;
        }
    }

    private void TextLeft_TextChanged(object sender, EventArgs e) {
        if (_property is not null) {
            var link = _property.Link;

            link.Left = GetValue(TextLeft);

            _property.Link = link;
        }
    }

    private void TextRight_TextChanged(object sender, EventArgs e) {
        if (_property is not null) {
            var link = _property.Link;

            link.Right = GetValue(TextRight);

            _property.Link = link;
        }
    }

    private void TextDown_TextChanged(object sender, EventArgs e) {
        if (_property is not null) {
            var link = _property.Link;

            link.Down = GetValue(TextDown);

            _property.Link = link;
        }
    }

    #endregion

    #region Moral

    private void ComboMoral_SelectedIndexChanged(object sender, EventArgs e) {
        if (_property is not null) {
            _property.Moral = (Moral)ComboMoral.SelectedIndex;
        }
    }

    #endregion

    #region Weather

    private void ComboWeather_SelectedIndexChanged(object sender, EventArgs e) {
        if (_property is not null) {
            _property.Weather = (Weather)ComboWeather.SelectedIndex;
        }
    }

    #endregion

    #region Boot

    private void TextBootId_TextChanged(object sender, EventArgs e) {
        if (_property is not null) {
            var boot = _property.Boot;

            boot.Id = GetValue(TextBootId);

            _property.Boot = boot;
        }
    }

    private void TextBootX_TextChanged(object sender, EventArgs e) {
        if (_property is not null) {
            var boot = _property.Boot;

            boot.X = GetValue(TextBootX);

            _property.Boot = boot;
        }
    }

    private void TextBootY_TextChanged(object sender, EventArgs e) {
        if (_property is not null) {
            var boot = _property.Boot;

            boot.Y = GetValue(TextBootY);

            _property.Boot = boot;
        }
    }

    #endregion

    #region Sound

    private void TextMusic_TextChanged(object sender, EventArgs e) {
        if (_property is not null) {
            _property.Music = TextMusic.Text.Trim();
        }
    }

    private void TextAmbience_TextChanged(object sender, EventArgs e) {
        if (_property is not null) {
            _property.Ambience = TextAmbience.Text.Trim();
        }
    }

    #endregion

    #region Size

    private void TextWidth_TextChanged(object sender, EventArgs e) {
        if (_property is not null) {
            var value = GetValue(TextWidth);

            _property.Width = (value > 0) ? value : 1; 
        }
    }

    private void TextHeight_TextChanged(object sender, EventArgs e) {
        if (_property is not null) {
            var value = GetValue(TextHeight);

            _property.Height = (value > 0) ? value : 1;
        }
    }

    #endregion

    #region Password

    private void TextKeyA_TextChanged(object sender, EventArgs e) {
        if (_property is not null) {
            _property.KeyA = TextKeyA.Text.Trim();
        }
    }

    private void TextKeyB_TextChanged(object sender, EventArgs e) {
        if (_property is not null) {
            _property.KeyB = TextKeyB.Text.Trim();
        }
    }

    private void TextKeyC_TextChanged(object sender, EventArgs e) {
        if (_property is not null) {
            _property.KeyC = TextKeyC.Text.Trim();
        }
    }

    #endregion

    #region Fog

    private void TextFogId_TextChanged(object sender, EventArgs e) {
        if (_property is not null) {
            var fog = _property.Fog;

            fog.Id = GetValue(TextFogId);

            _property.Fog = fog;
        }
    }

    private void ComboBlending_SelectedIndexChanged(object sender, EventArgs e) {
        if (_property is not null) {
            var fog = _property.Fog;

            fog.Blending = (Blending)ComboBlending.SelectedIndex;

            _property.Fog = fog;
        }
    }

    private void ScrollOpacity_Scroll(object sender, ScrollEventArgs e) {
        if (_property is not null) {
            var fog = _property.Fog;

            fog.Opacity = (byte)ScrollOpacity.Value;

            _property.Fog = fog;

            LabelOpacity.Text = $"Opacity: {fog.Opacity}";
        }
    }

    private void ScrollRed_Scroll(object sender, ScrollEventArgs e) {
        if (_property is not null) {
            var fog = _property.Fog;

            fog.Red = (byte)ScrollRed.Value;

            _property.Fog = fog;

            LabelRed.Text = $"Red: {fog.Red}";
        }
    }

    private void ScrollGreen_Scroll(object sender, ScrollEventArgs e) {
        if (_property is not null) {
            var fog = _property.Fog;

            fog.Green = (byte)ScrollGreen.Value;

            _property.Fog = fog;

            LabelRed.Text = $"Green: {fog.Green}";
        }
    }

    private void ScrollBlue_Scroll(object sender, ScrollEventArgs e) {
        if (_property is not null) {
            var fog = _property.Fog;

            fog.Blue = (byte)ScrollBlue.Value;

            _property.Fog = fog;

            LabelRed.Text = $"Blue: {fog.Blue}";
        }
    }

    #endregion
}