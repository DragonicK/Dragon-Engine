namespace Crystalshire.Maps {
    partial class FormProperty {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProperty));
            this.GroupProperty = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // GroupProperty
            // 
            this.GroupProperty.Location = new System.Drawing.Point(12, 9);
            this.GroupProperty.Name = "GroupProperty";
            this.GroupProperty.Size = new System.Drawing.Size(615, 460);
            this.GroupProperty.TabIndex = 0;
            this.GroupProperty.TabStop = false;
            this.GroupProperty.Text = "Property";
            // 
            // FormProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 481);
            this.Controls.Add(this.GroupProperty);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormProperty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Property";
            this.Load += new System.EventHandler(this.FormProperty_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox GroupProperty;
    }
}