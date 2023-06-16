namespace carwash
{
    partial class EnlargedForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EnlargedPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.EnlargedPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // EnlargedPictureBox
            // 
            this.EnlargedPictureBox.Location = new System.Drawing.Point(0, 0);
            this.EnlargedPictureBox.Name = "EnlargedPictureBox";
            this.EnlargedPictureBox.Size = new System.Drawing.Size(600, 600);
            this.EnlargedPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.EnlargedPictureBox.TabIndex = 0;
            this.EnlargedPictureBox.TabStop = false;
            this.EnlargedPictureBox.Click += new System.EventHandler(this.EnlargedPictureBox_Click);
            // 
            // EnlargedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 600);
            this.ControlBox = false;
            this.Controls.Add(this.EnlargedPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EnlargedForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EnlargedForm";
            ((System.ComponentModel.ISupportInitialize)(this.EnlargedPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox EnlargedPictureBox;
    }
}