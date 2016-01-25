namespace AutomaticOrderGeneration
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GenerateOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RegisterPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RegisterSheetNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridOrder = new System.Windows.Forms.DataGridView();
            this.GenerateOrderButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.ServiceToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(989, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFileToolStripMenuItem,
            this.GenerateOrderToolStripMenuItem,
            this.toolStripSeparator1,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.FileToolStripMenuItem.Text = "Файл";
            // 
            // OpenFileToolStripMenuItem
            // 
            this.OpenFileToolStripMenuItem.Name = "OpenFileToolStripMenuItem";
            this.OpenFileToolStripMenuItem.Size = new System.Drawing.Size(294, 30);
            this.OpenFileToolStripMenuItem.Text = "Открыть...";
            this.OpenFileToolStripMenuItem.Click += new System.EventHandler(this.OpenFileToolStripMenuItem_Click);
            // 
            // GenerateOrderToolStripMenuItem
            // 
            this.GenerateOrderToolStripMenuItem.Name = "GenerateOrderToolStripMenuItem";
            this.GenerateOrderToolStripMenuItem.Size = new System.Drawing.Size(294, 30);
            this.GenerateOrderToolStripMenuItem.Text = "Сгенерировать выписку...";
            this.GenerateOrderToolStripMenuItem.Click += new System.EventHandler(this.GenerateOrderToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(291, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(294, 30);
            this.ExitToolStripMenuItem.Text = "Выход";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // ServiceToolStripMenuItem
            // 
            this.ServiceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingsToolStripMenuItem});
            this.ServiceToolStripMenuItem.Name = "ServiceToolStripMenuItem";
            this.ServiceToolStripMenuItem.Size = new System.Drawing.Size(83, 29);
            this.ServiceToolStripMenuItem.Text = "Сервис";
            // 
            // SettingsToolStripMenuItem
            // 
            this.SettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RegisterPathToolStripMenuItem,
            this.RegisterSheetNameToolStripMenuItem});
            this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            this.SettingsToolStripMenuItem.Size = new System.Drawing.Size(172, 30);
            this.SettingsToolStripMenuItem.Text = "Настройки";
            // 
            // RegisterPathToolStripMenuItem
            // 
            this.RegisterPathToolStripMenuItem.Name = "RegisterPathToolStripMenuItem";
            this.RegisterPathToolStripMenuItem.Size = new System.Drawing.Size(342, 30);
            this.RegisterPathToolStripMenuItem.Text = "Расположение реестра...";
            this.RegisterPathToolStripMenuItem.Click += new System.EventHandler(this.RegisterPathToolStripMenuItem_Click);
            // 
            // RegisterSheetNameToolStripMenuItem
            // 
            this.RegisterSheetNameToolStripMenuItem.Name = "RegisterSheetNameToolStripMenuItem";
            this.RegisterSheetNameToolStripMenuItem.Size = new System.Drawing.Size(342, 30);
            this.RegisterSheetNameToolStripMenuItem.Text = "Название страницы в реестре...";
            this.RegisterSheetNameToolStripMenuItem.Click += new System.EventHandler(this.RegisterSheetNameToolStripMenuItem_Click);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(93, 29);
            this.HelpToolStripMenuItem.Text = "Справка";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(209, 30);
            this.AboutToolStripMenuItem.Text = "О программе...";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // gridOrder
            // 
            this.gridOrder.AllowUserToAddRows = false;
            this.gridOrder.AllowUserToDeleteRows = false;
            this.gridOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridOrder.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridOrder.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.gridOrder.EnableHeadersVisualStyles = false;
            this.gridOrder.Location = new System.Drawing.Point(23, 45);
            this.gridOrder.Name = "gridOrder";
            this.gridOrder.RowTemplate.Height = 28;
            this.gridOrder.Size = new System.Drawing.Size(937, 288);
            this.gridOrder.TabIndex = 1;
            this.gridOrder.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridOrder_CellClick);
            this.gridOrder.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridOrder_CellContentClick);
            // 
            // GenerateOrderButton
            // 
            this.GenerateOrderButton.BackColor = System.Drawing.Color.SkyBlue;
            this.GenerateOrderButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GenerateOrderButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.GenerateOrderButton.Location = new System.Drawing.Point(0, 339);
            this.GenerateOrderButton.Name = "GenerateOrderButton";
            this.GenerateOrderButton.Size = new System.Drawing.Size(989, 72);
            this.GenerateOrderButton.TabIndex = 2;
            this.GenerateOrderButton.Text = "Сгенерировать выписку";
            this.GenerateOrderButton.UseVisualStyleBackColor = false;
            this.GenerateOrderButton.Click += new System.EventHandler(this.GenerateOrderButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 411);
            this.Controls.Add(this.GenerateOrderButton);
            this.Controls.Add(this.gridOrder);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Автоматический генератор выписок 1.1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GenerateOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridView gridOrder;
        private System.Windows.Forms.Button GenerateOrderButton;
        private System.Windows.Forms.ToolStripMenuItem ServiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RegisterPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RegisterSheetNameToolStripMenuItem;
    }
}

