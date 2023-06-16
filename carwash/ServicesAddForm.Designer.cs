namespace carwash
{
    partial class ServicesAddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServicesAddForm));
            this.ServiceAddButton = new Guna.UI2.WinForms.Guna2Button();
            this.ServiceAddLabel3 = new System.Windows.Forms.Label();
            this.ServiceAddLabel2 = new System.Windows.Forms.Label();
            this.ServiceAddLabel1 = new System.Windows.Forms.Label();
            this.ServiceAddNum = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.ServiceAddTb1 = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.ServiceAddTb2 = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            ((System.ComponentModel.ISupportInitialize)(this.ServiceAddNum)).BeginInit();
            this.SuspendLayout();
            // 
            // ServiceAddButton
            // 
            this.ServiceAddButton.CheckedState.Parent = this.ServiceAddButton;
            this.ServiceAddButton.CustomImages.Parent = this.ServiceAddButton;
            this.ServiceAddButton.FillColor = System.Drawing.Color.DodgerBlue;
            this.ServiceAddButton.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ServiceAddButton.ForeColor = System.Drawing.Color.White;
            this.ServiceAddButton.HoverState.Parent = this.ServiceAddButton;
            this.ServiceAddButton.Location = new System.Drawing.Point(12, 185);
            this.ServiceAddButton.Name = "ServiceAddButton";
            this.ServiceAddButton.ShadowDecoration.Parent = this.ServiceAddButton;
            this.ServiceAddButton.Size = new System.Drawing.Size(311, 45);
            this.ServiceAddButton.TabIndex = 31;
            this.ServiceAddButton.Text = "Добавить";
            this.ServiceAddButton.Click += new System.EventHandler(this.ServiceAddButton_Click);
            // 
            // ServiceAddLabel3
            // 
            this.ServiceAddLabel3.AutoSize = true;
            this.ServiceAddLabel3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ServiceAddLabel3.Location = new System.Drawing.Point(12, 123);
            this.ServiceAddLabel3.Name = "ServiceAddLabel3";
            this.ServiceAddLabel3.Size = new System.Drawing.Size(183, 17);
            this.ServiceAddLabel3.TabIndex = 29;
            this.ServiceAddLabel3.Text = "Укажите стоимость услуги:";
            // 
            // ServiceAddLabel2
            // 
            this.ServiceAddLabel2.AutoSize = true;
            this.ServiceAddLabel2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ServiceAddLabel2.Location = new System.Drawing.Point(12, 66);
            this.ServiceAddLabel2.Name = "ServiceAddLabel2";
            this.ServiceAddLabel2.Size = new System.Drawing.Size(210, 17);
            this.ServiceAddLabel2.TabIndex = 25;
            this.ServiceAddLabel2.Text = "Введите наименование услуги:";
            // 
            // ServiceAddLabel1
            // 
            this.ServiceAddLabel1.AutoSize = true;
            this.ServiceAddLabel1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ServiceAddLabel1.Location = new System.Drawing.Point(12, 9);
            this.ServiceAddLabel1.Name = "ServiceAddLabel1";
            this.ServiceAddLabel1.Size = new System.Drawing.Size(123, 17);
            this.ServiceAddLabel1.TabIndex = 24;
            this.ServiceAddLabel1.Text = "Введите ID услуги:";
            // 
            // ServiceAddNum
            // 
            this.ServiceAddNum.BackColor = System.Drawing.Color.Transparent;
            this.ServiceAddNum.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ServiceAddNum.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.ServiceAddNum.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ServiceAddNum.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ServiceAddNum.DisabledState.Parent = this.ServiceAddNum;
            this.ServiceAddNum.DisabledState.UpDownButtonFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(177)))), ((int)(((byte)(177)))));
            this.ServiceAddNum.DisabledState.UpDownButtonForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(203)))), ((int)(((byte)(203)))));
            this.ServiceAddNum.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ServiceAddNum.FocusedState.Parent = this.ServiceAddNum;
            this.ServiceAddNum.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServiceAddNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.ServiceAddNum.Location = new System.Drawing.Point(12, 143);
            this.ServiceAddNum.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.ServiceAddNum.Name = "ServiceAddNum";
            this.ServiceAddNum.ShadowDecoration.Parent = this.ServiceAddNum;
            this.ServiceAddNum.Size = new System.Drawing.Size(311, 36);
            this.ServiceAddNum.TabIndex = 22;
            this.ServiceAddNum.UpDownButtonFillColor = System.Drawing.Color.DodgerBlue;
            // 
            // ServiceAddTb1
            // 
            this.ServiceAddTb1.BackColor = System.Drawing.SystemColors.Window;
            this.ServiceAddTb1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ServiceAddTb1.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.ServiceAddTb1.ForeColor = System.Drawing.Color.Gray;
            this.ServiceAddTb1.HintForeColor = System.Drawing.Color.Empty;
            this.ServiceAddTb1.HintText = "";
            this.ServiceAddTb1.isPassword = false;
            this.ServiceAddTb1.LineFocusedColor = System.Drawing.Color.MediumBlue;
            this.ServiceAddTb1.LineIdleColor = System.Drawing.Color.DodgerBlue;
            this.ServiceAddTb1.LineMouseHoverColor = System.Drawing.Color.MediumBlue;
            this.ServiceAddTb1.LineThickness = 3;
            this.ServiceAddTb1.Location = new System.Drawing.Point(13, 30);
            this.ServiceAddTb1.Margin = new System.Windows.Forms.Padding(4);
            this.ServiceAddTb1.Name = "ServiceAddTb1";
            this.ServiceAddTb1.Size = new System.Drawing.Size(311, 32);
            this.ServiceAddTb1.TabIndex = 17;
            this.ServiceAddTb1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ServiceAddTb1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ServiceAddTb1_KeyDown);
            this.ServiceAddTb1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ServiceAddTb1_KeyPress);
            // 
            // ServiceAddTb2
            // 
            this.ServiceAddTb2.BackColor = System.Drawing.SystemColors.Window;
            this.ServiceAddTb2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ServiceAddTb2.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.ServiceAddTb2.ForeColor = System.Drawing.Color.Gray;
            this.ServiceAddTb2.HintForeColor = System.Drawing.Color.Empty;
            this.ServiceAddTb2.HintText = "";
            this.ServiceAddTb2.isPassword = false;
            this.ServiceAddTb2.LineFocusedColor = System.Drawing.Color.MediumBlue;
            this.ServiceAddTb2.LineIdleColor = System.Drawing.Color.DodgerBlue;
            this.ServiceAddTb2.LineMouseHoverColor = System.Drawing.Color.MediumBlue;
            this.ServiceAddTb2.LineThickness = 3;
            this.ServiceAddTb2.Location = new System.Drawing.Point(13, 87);
            this.ServiceAddTb2.Margin = new System.Windows.Forms.Padding(4);
            this.ServiceAddTb2.Name = "ServiceAddTb2";
            this.ServiceAddTb2.Size = new System.Drawing.Size(311, 32);
            this.ServiceAddTb2.TabIndex = 32;
            this.ServiceAddTb2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // ServicesAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 245);
            this.Controls.Add(this.ServiceAddTb2);
            this.Controls.Add(this.ServiceAddButton);
            this.Controls.Add(this.ServiceAddLabel3);
            this.Controls.Add(this.ServiceAddLabel2);
            this.Controls.Add(this.ServiceAddLabel1);
            this.Controls.Add(this.ServiceAddNum);
            this.Controls.Add(this.ServiceAddTb1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServicesAddForm";
            this.Text = "Добавить услугу";
            this.Load += new System.EventHandler(this.ServicesAddForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ServiceAddNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button ServiceAddButton;
        private System.Windows.Forms.Label ServiceAddLabel3;
        private System.Windows.Forms.Label ServiceAddLabel2;
        private System.Windows.Forms.Label ServiceAddLabel1;
        private Guna.UI2.WinForms.Guna2NumericUpDown ServiceAddNum;
        private Bunifu.Framework.UI.BunifuMaterialTextbox ServiceAddTb1;
        private Bunifu.Framework.UI.BunifuMaterialTextbox ServiceAddTb2;
    }
}