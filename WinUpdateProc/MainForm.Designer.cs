namespace WinUpdateProc {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose ();
            }
            base.Dispose (disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent () {
            this.BackGroupImage = new System.Windows.Forms.PictureBox();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.CloseButton = new System.Windows.Forms.PictureBox();
            this.errorBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.BackGroupImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseButton)).BeginInit();
            this.SuspendLayout();
            // 
            // BackGroupImage
            // 
            this.BackGroupImage.Image = global::WinUpdateProc.Properties.Resources.bg;
            this.BackGroupImage.Location = new System.Drawing.Point(389, -10);
            this.BackGroupImage.Name = "BackGroupImage";
            this.BackGroupImage.Size = new System.Drawing.Size(502, 490);
            this.BackGroupImage.TabIndex = 1;
            this.BackGroupImage.TabStop = false;
            this.BackGroupImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BackGroupImage_Down);
            this.BackGroupImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BackGroupImage_Move);
            this.BackGroupImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BackGroupImage_Up);
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.OutputTextBox.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OutputTextBox.ForeColor = System.Drawing.Color.Black;
            this.OutputTextBox.Location = new System.Drawing.Point(126, 100);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ReadOnly = true;
            this.OutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutputTextBox.Size = new System.Drawing.Size(765, 368);
            this.OutputTextBox.TabIndex = 2;
            // 
            // CloseButton
            // 
            this.CloseButton.Image = global::WinUpdateProc.Properties.Resources.close;
            this.CloseButton.Location = new System.Drawing.Point(834, 23);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(32, 33);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.TabStop = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // errorBox
            // 
            this.errorBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.errorBox.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.errorBox.ForeColor = System.Drawing.Color.Black;
            this.errorBox.Location = new System.Drawing.Point(126, 466);
            this.errorBox.Multiline = true;
            this.errorBox.Name = "errorBox";
            this.errorBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.errorBox.Size = new System.Drawing.Size(765, 208);
            this.errorBox.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 676);
            this.Controls.Add(this.errorBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.OutputTextBox);
            this.Controls.Add(this.BackGroupImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "风色物语";
            ((System.ComponentModel.ISupportInitialize)(this.BackGroupImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox BackGroupImage;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.PictureBox CloseButton;
        private System.Windows.Forms.TextBox errorBox;



    }
}

