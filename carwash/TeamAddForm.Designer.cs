namespace carwash
{
    partial class TeamAddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeamAddForm));
            this.TeamAddLabel3 = new System.Windows.Forms.Label();
            this.TeamAddDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.TeamAddTb2 = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.TeamAddButton = new Guna.UI2.WinForms.Guna2Button();
            this.TeamAddLabel2 = new System.Windows.Forms.Label();
            this.TeamAddLabel1 = new System.Windows.Forms.Label();
            this.TeamAddTb1 = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.SuspendLayout();
            // 
            // TeamAddLabel3
            // 
            this.TeamAddLabel3.AutoSize = true;
            this.TeamAddLabel3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TeamAddLabel3.Location = new System.Drawing.Point(12, 123);
            this.TeamAddLabel3.Name = "TeamAddLabel3";
            this.TeamAddLabel3.Size = new System.Drawing.Size(217, 17);
            this.TeamAddLabel3.TabIndex = 51;
            this.TeamAddLabel3.Text = "Выберите дату формирования:";
            // 
            // TeamAddDate
            // 
            this.TeamAddDate.CheckedState.Parent = this.TeamAddDate;
            this.TeamAddDate.FillColor = System.Drawing.Color.DodgerBlue;
            this.TeamAddDate.FocusedColor = System.Drawing.Color.MediumBlue;
            this.TeamAddDate.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TeamAddDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.TeamAddDate.HoverState.Parent = this.TeamAddDate;
            this.TeamAddDate.Location = new System.Drawing.Point(15, 143);
            this.TeamAddDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.TeamAddDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.TeamAddDate.Name = "TeamAddDate";
            this.TeamAddDate.ShadowDecoration.Parent = this.TeamAddDate;
            this.TeamAddDate.Size = new System.Drawing.Size(311, 36);
            this.TeamAddDate.TabIndex = 50;
            this.TeamAddDate.Value = new System.DateTime(2023, 5, 30, 15, 38, 44, 622);
            // 
            // TeamAddTb2
            // 
            this.TeamAddTb2.BackColor = System.Drawing.SystemColors.Window;
            this.TeamAddTb2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TeamAddTb2.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.TeamAddTb2.ForeColor = System.Drawing.Color.Gray;
            this.TeamAddTb2.HintForeColor = System.Drawing.Color.Empty;
            this.TeamAddTb2.HintText = "";
            this.TeamAddTb2.isPassword = false;
            this.TeamAddTb2.LineFocusedColor = System.Drawing.Color.MediumBlue;
            this.TeamAddTb2.LineIdleColor = System.Drawing.Color.DodgerBlue;
            this.TeamAddTb2.LineMouseHoverColor = System.Drawing.Color.MediumBlue;
            this.TeamAddTb2.LineThickness = 3;
            this.TeamAddTb2.Location = new System.Drawing.Point(15, 87);
            this.TeamAddTb2.Margin = new System.Windows.Forms.Padding(4);
            this.TeamAddTb2.Name = "TeamAddTb2";
            this.TeamAddTb2.Size = new System.Drawing.Size(311, 32);
            this.TeamAddTb2.TabIndex = 49;
            this.TeamAddTb2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // TeamAddButton
            // 
            this.TeamAddButton.CheckedState.Parent = this.TeamAddButton;
            this.TeamAddButton.CustomImages.Parent = this.TeamAddButton;
            this.TeamAddButton.FillColor = System.Drawing.Color.DodgerBlue;
            this.TeamAddButton.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TeamAddButton.ForeColor = System.Drawing.Color.White;
            this.TeamAddButton.HoverState.Parent = this.TeamAddButton;
            this.TeamAddButton.Location = new System.Drawing.Point(15, 185);
            this.TeamAddButton.Name = "TeamAddButton";
            this.TeamAddButton.ShadowDecoration.Parent = this.TeamAddButton;
            this.TeamAddButton.Size = new System.Drawing.Size(311, 45);
            this.TeamAddButton.TabIndex = 48;
            this.TeamAddButton.Text = "Добавить";
            this.TeamAddButton.Click += new System.EventHandler(this.TeamAddButton_Click);
            // 
            // TeamAddLabel2
            // 
            this.TeamAddLabel2.AutoSize = true;
            this.TeamAddLabel2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TeamAddLabel2.Location = new System.Drawing.Point(12, 66);
            this.TeamAddLabel2.Name = "TeamAddLabel2";
            this.TeamAddLabel2.Size = new System.Drawing.Size(172, 17);
            this.TeamAddLabel2.TabIndex = 47;
            this.TeamAddLabel2.Text = "Введите номер бригады:";
            // 
            // TeamAddLabel1
            // 
            this.TeamAddLabel1.AutoSize = true;
            this.TeamAddLabel1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TeamAddLabel1.Location = new System.Drawing.Point(12, 9);
            this.TeamAddLabel1.Name = "TeamAddLabel1";
            this.TeamAddLabel1.Size = new System.Drawing.Size(140, 17);
            this.TeamAddLabel1.TabIndex = 46;
            this.TeamAddLabel1.Text = "Введите ID бригады:";
            // 
            // TeamAddTb1
            // 
            this.TeamAddTb1.BackColor = System.Drawing.SystemColors.Window;
            this.TeamAddTb1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TeamAddTb1.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.TeamAddTb1.ForeColor = System.Drawing.Color.Gray;
            this.TeamAddTb1.HintForeColor = System.Drawing.Color.Empty;
            this.TeamAddTb1.HintText = "";
            this.TeamAddTb1.isPassword = false;
            this.TeamAddTb1.LineFocusedColor = System.Drawing.Color.MediumBlue;
            this.TeamAddTb1.LineIdleColor = System.Drawing.Color.DodgerBlue;
            this.TeamAddTb1.LineMouseHoverColor = System.Drawing.Color.MediumBlue;
            this.TeamAddTb1.LineThickness = 3;
            this.TeamAddTb1.Location = new System.Drawing.Point(15, 30);
            this.TeamAddTb1.Margin = new System.Windows.Forms.Padding(4);
            this.TeamAddTb1.Name = "TeamAddTb1";
            this.TeamAddTb1.Size = new System.Drawing.Size(311, 32);
            this.TeamAddTb1.TabIndex = 45;
            this.TeamAddTb1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TeamAddTb1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TeamAddTb1_KeyDown);
            this.TeamAddTb1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TeamAddTb1_KeyPress);
            // 
            // TeamAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 254);
            this.Controls.Add(this.TeamAddLabel3);
            this.Controls.Add(this.TeamAddDate);
            this.Controls.Add(this.TeamAddTb2);
            this.Controls.Add(this.TeamAddButton);
            this.Controls.Add(this.TeamAddLabel2);
            this.Controls.Add(this.TeamAddLabel1);
            this.Controls.Add(this.TeamAddTb1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TeamAddForm";
            this.Text = "Добавить бригаду";
            this.Load += new System.EventHandler(this.TeamAddForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TeamAddLabel3;
        private Guna.UI2.WinForms.Guna2DateTimePicker TeamAddDate;
        private Bunifu.Framework.UI.BunifuMaterialTextbox TeamAddTb2;
        private Guna.UI2.WinForms.Guna2Button TeamAddButton;
        private System.Windows.Forms.Label TeamAddLabel2;
        private System.Windows.Forms.Label TeamAddLabel1;
        private Bunifu.Framework.UI.BunifuMaterialTextbox TeamAddTb1;
    }
}