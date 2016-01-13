namespace AutomaticOrderGeneration
{
    partial class RegiterPathForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegiterPathForm));
            this.textBoxCurrentRegisterPath = new System.Windows.Forms.TextBox();
            this.ButtonChooseRegisterFile = new System.Windows.Forms.Button();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxCurrentRegisterPath
            // 
            this.textBoxCurrentRegisterPath.Location = new System.Drawing.Point(23, 66);
            this.textBoxCurrentRegisterPath.Name = "textBoxCurrentRegisterPath";
            this.textBoxCurrentRegisterPath.Size = new System.Drawing.Size(528, 26);
            this.textBoxCurrentRegisterPath.TabIndex = 0;
            // 
            // ButtonChooseRegisterFile
            // 
            this.ButtonChooseRegisterFile.Location = new System.Drawing.Point(573, 61);
            this.ButtonChooseRegisterFile.Name = "ButtonChooseRegisterFile";
            this.ButtonChooseRegisterFile.Size = new System.Drawing.Size(98, 37);
            this.ButtonChooseRegisterFile.TabIndex = 1;
            this.ButtonChooseRegisterFile.Text = "Обзор";
            this.ButtonChooseRegisterFile.UseVisualStyleBackColor = true;
            this.ButtonChooseRegisterFile.Click += new System.EventHandler(this.ButtonChooseRegisterFile_Click);
            // 
            // ButtonOk
            // 
            this.ButtonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonOk.Location = new System.Drawing.Point(212, 121);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(97, 33);
            this.ButtonOk.TabIndex = 2;
            this.ButtonOk.Text = "OK";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // ButttonCancel
            // 
            this.ButttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButttonCancel.Location = new System.Drawing.Point(342, 121);
            this.ButttonCancel.Name = "ButttonCancel";
            this.ButttonCancel.Size = new System.Drawing.Size(97, 32);
            this.ButttonCancel.TabIndex = 3;
            this.ButttonCancel.Text = "Отмена";
            this.ButttonCancel.UseVisualStyleBackColor = true;
            this.ButttonCancel.Click += new System.EventHandler(this.ButttonCancel_Click);
            // 
            // RegiterPathForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 181);
            this.Controls.Add(this.ButttonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.ButtonChooseRegisterFile);
            this.Controls.Add(this.textBoxCurrentRegisterPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegiterPathForm";
            this.Text = "Изменить расположение реестра";
            this.Load += new System.EventHandler(this.RegiterPathForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxCurrentRegisterPath;
        private System.Windows.Forms.Button ButtonChooseRegisterFile;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButttonCancel;
    }
}