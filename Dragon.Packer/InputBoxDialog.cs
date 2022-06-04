namespace Dragon.Packer;

public partial class InputBoxDialog : Form {
    public string Input { get; set; } = string.Empty;
    public string Caption { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;

    public InputBoxDialog() {
        InitializeComponent();
    }

    public DialogResult ShowInput() {
        TextInput.Text = Input;
        LabelTitle.Text = Caption;

        TextInput.SelectionStart = 0;
        TextInput.SelectionLength = TextInput.Text.Length;
        TextInput.Focus();

        return ShowDialog();
    }

    private void TextInput_KeyDown(object sender, KeyEventArgs e) {
        if (e.KeyCode == Keys.Enter) {
            ButtonOK_Click(null!, null!);
        }
    }

    private void ButtonOK_Click(object sender, EventArgs e) {
        Response = TextInput.Text.Trim();
        Close();
    }

    private void ButtonCancel_Click(object sender, EventArgs e) {
        Close();
    }
}