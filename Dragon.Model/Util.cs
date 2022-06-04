namespace Dragon.Model;

public static class Util {
    public const string None = "None.";

    public const int NotSelected = -1;

    public static void FillComboBox<T>(ComboBox combo) {
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

    public static int GetValue(TextBox textBox) {
        var value = textBox.Text.Trim();

        return GetValue(value);
    }

    public static int GetValue(string value) {
        if (int.TryParse(value, out var result)) {
            return result;
        }

        return 0;
    }

    public static int GetListSelectedId(string selected) {
        var index = selected!.IndexOf(':');
        var id = selected.Substring(0, index);

        return GetValue(id);
    }

    public static void FillTextBoxWithZero(Control root) {
        var controls = root.Controls;

        foreach (var control in controls) {
            if (control is TextBox textBox) {
                textBox.Text = "0";
            }
        }
    }

    public static void ResetCheckBox(Control root) {
        var controls = root.Controls;

        foreach (var control in controls) {
            if (control is CheckBox checkBox) {
                checkBox.Checked = false;
            }
        }
    }

}