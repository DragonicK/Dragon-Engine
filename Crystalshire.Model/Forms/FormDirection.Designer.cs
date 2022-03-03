namespace Crystalshire.Model.Forms {
    partial class FormDirection {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDirection));
            this.GroupDirections = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TextName = new System.Windows.Forms.TextBox();
            this.TextId = new System.Windows.Forms.TextBox();
            this.ButtonRight = new System.Windows.Forms.Button();
            this.ButtonLeft = new System.Windows.Forms.Button();
            this.ButtonDown = new System.Windows.Forms.Button();
            this.ButtonUp = new System.Windows.Forms.Button();
            this.GroupDirections.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupDirections
            // 
            this.GroupDirections.Controls.Add(this.label2);
            this.GroupDirections.Controls.Add(this.label1);
            this.GroupDirections.Controls.Add(this.TextName);
            this.GroupDirections.Controls.Add(this.TextId);
            this.GroupDirections.Controls.Add(this.ButtonRight);
            this.GroupDirections.Controls.Add(this.ButtonLeft);
            this.GroupDirections.Controls.Add(this.ButtonDown);
            this.GroupDirections.Controls.Add(this.ButtonUp);
            this.GroupDirections.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GroupDirections.Location = new System.Drawing.Point(12, 12);
            this.GroupDirections.Name = "GroupDirections";
            this.GroupDirections.Size = new System.Drawing.Size(207, 222);
            this.GroupDirections.TabIndex = 0;
            this.GroupDirections.TabStop = false;
            this.GroupDirections.Text = "No Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Direction Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Direction Id:";
            // 
            // TextName
            // 
            this.TextName.Location = new System.Drawing.Point(25, 86);
            this.TextName.Name = "TextName";
            this.TextName.Size = new System.Drawing.Size(156, 23);
            this.TextName.TabIndex = 5;
            this.TextName.TextChanged += new System.EventHandler(this.TextName_TextChanged);
            // 
            // TextId
            // 
            this.TextId.Location = new System.Drawing.Point(25, 39);
            this.TextId.Name = "TextId";
            this.TextId.Size = new System.Drawing.Size(156, 23);
            this.TextId.TabIndex = 4;
            this.TextId.TextChanged += new System.EventHandler(this.TextId_TextChanged);
            // 
            // ButtonRight
            // 
            this.ButtonRight.Location = new System.Drawing.Point(106, 151);
            this.ButtonRight.Name = "ButtonRight";
            this.ButtonRight.Size = new System.Drawing.Size(75, 23);
            this.ButtonRight.TabIndex = 3;
            this.ButtonRight.Text = "Right";
            this.ButtonRight.UseVisualStyleBackColor = true;
            this.ButtonRight.Click += new System.EventHandler(this.ButtonRight_Click);
            // 
            // ButtonLeft
            // 
            this.ButtonLeft.Location = new System.Drawing.Point(25, 151);
            this.ButtonLeft.Name = "ButtonLeft";
            this.ButtonLeft.Size = new System.Drawing.Size(75, 23);
            this.ButtonLeft.TabIndex = 2;
            this.ButtonLeft.Text = "Left";
            this.ButtonLeft.UseVisualStyleBackColor = true;
            this.ButtonLeft.Click += new System.EventHandler(this.ButtonLeft_Click);
            // 
            // ButtonDown
            // 
            this.ButtonDown.Location = new System.Drawing.Point(68, 180);
            this.ButtonDown.Name = "ButtonDown";
            this.ButtonDown.Size = new System.Drawing.Size(75, 23);
            this.ButtonDown.TabIndex = 1;
            this.ButtonDown.Text = "Down";
            this.ButtonDown.UseVisualStyleBackColor = true;
            this.ButtonDown.Click += new System.EventHandler(this.ButtonDown_Click);
            // 
            // ButtonUp
            // 
            this.ButtonUp.Location = new System.Drawing.Point(68, 122);
            this.ButtonUp.Name = "ButtonUp";
            this.ButtonUp.Size = new System.Drawing.Size(75, 23);
            this.ButtonUp.TabIndex = 0;
            this.ButtonUp.Text = "Up";
            this.ButtonUp.UseVisualStyleBackColor = true;
            this.ButtonUp.Click += new System.EventHandler(this.ButtonUp_Click);
            // 
            // FormDirection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 245);
            this.Controls.Add(this.GroupDirections);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormDirection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Directions - No Name";
            this.GroupDirections.ResumeLayout(false);
            this.GroupDirections.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox GroupDirections;
        private Button ButtonDown;
        private Button ButtonUp;
        private Button ButtonRight;
        private Button ButtonLeft;
        private TextBox TextName;
        private TextBox TextId;
        private Label label1;
        private Label label2;
    }
}