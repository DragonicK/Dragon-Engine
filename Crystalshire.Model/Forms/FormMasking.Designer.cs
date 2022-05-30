namespace Crystalshire.Model.Forms;

partial class FormMasking {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMasking));
        this.PictureFrame = new System.Windows.Forms.PictureBox();
        ((System.ComponentModel.ISupportInitialize)(this.PictureFrame)).BeginInit();
        this.SuspendLayout();
        // 
        // PictureFrame
        // 
        this.PictureFrame.BackColor = System.Drawing.Color.Black;
        this.PictureFrame.Location = new System.Drawing.Point(14, 12);
        this.PictureFrame.Name = "PictureFrame";
        this.PictureFrame.Size = new System.Drawing.Size(320, 320);
        this.PictureFrame.TabIndex = 0;
        this.PictureFrame.TabStop = false;
        this.PictureFrame.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureFrame_Paint);
        // 
        // FormMasking
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(346, 345);
        this.Controls.Add(this.PictureFrame);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.Name = "FormMasking";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Sprite Masking";
        ((System.ComponentModel.ISupportInitialize)(this.PictureFrame)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private PictureBox PictureFrame;
}