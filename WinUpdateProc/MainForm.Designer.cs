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
            this.CloseButton = new System.Windows.Forms.PictureBox();
            this.errorBox = new System.Windows.Forms.TextBox();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.BackGroupImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseButton)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.errorBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.errorBox.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.errorBox.ForeColor = System.Drawing.Color.Black;
            this.errorBox.Location = new System.Drawing.Point(3, -3);
            this.errorBox.Multiline = true;
            this.errorBox.Name = "errorBox";
            this.errorBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.errorBox.Size = new System.Drawing.Size(774, 174);
            this.errorBox.TabIndex = 4;
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.OutputTextBox.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OutputTextBox.ForeColor = System.Drawing.Color.Black;
            this.OutputTextBox.Location = new System.Drawing.Point(3, 3);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ReadOnly = true;
            this.OutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutputTextBox.Size = new System.Drawing.Size(774, 395);
            this.OutputTextBox.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.splitContainer1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.splitContainer1.Location = new System.Drawing.Point(111, 91);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.OutputTextBox);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.errorBox);
            this.splitContainer1.Size = new System.Drawing.Size(780, 583);
            this.splitContainer1.SplitterDistance = 399;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 5;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 676);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.BackGroupImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "风色物语";
            ((System.ComponentModel.ISupportInitialize)(this.BackGroupImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseButton)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox BackGroupImage;
        private System.Windows.Forms.PictureBox CloseButton;
        private System.Windows.Forms.TextBox errorBox;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.SplitContainer splitContainer1;



    }
}

