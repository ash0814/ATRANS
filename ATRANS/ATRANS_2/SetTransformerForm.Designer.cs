namespace ATRANS
{
    partial class SetTransformerForm
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
            this.metroComboBox1 = new MetroFramework.Controls.MetroComboBox();
            this.selectInputFolderBtn = new MetroFramework.Controls.MetroButton();
            this.selectOutputFolderBtn = new MetroFramework.Controls.MetroButton();
            this.inputFolderPathLabel = new MetroFramework.Controls.MetroLabel();
            this.outputFolderPathLabel = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.threadCountLabel = new MetroFramework.Controls.MetroLabel();
            this.addTransformType = new MetroFramework.Controls.MetroButton();
            this.selectThreadCountCombobox = new MetroFramework.Controls.MetroComboBox();
            this.saveFolderLabel = new MetroFramework.Controls.MetroLabel();
            this.logFolderLabel = new MetroFramework.Controls.MetroLabel();
            this.saveFolderPathLabel = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // metroComboBox1
            // 
            this.metroComboBox1.FormattingEnabled = true;
            this.metroComboBox1.ItemHeight = 23;
            this.metroComboBox1.Items.AddRange(new object[] {
            "atxt - abin"});
            this.metroComboBox1.Location = new System.Drawing.Point(23, 80);
            this.metroComboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.metroComboBox1.Name = "metroComboBox1";
            this.metroComboBox1.Size = new System.Drawing.Size(121, 29);
            this.metroComboBox1.TabIndex = 0;
            // 
            // selectInputFolderBtn
            // 
            this.selectInputFolderBtn.Location = new System.Drawing.Point(23, 128);
            this.selectInputFolderBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.selectInputFolderBtn.Name = "selectInputFolderBtn";
            this.selectInputFolderBtn.Size = new System.Drawing.Size(121, 31);
            this.selectInputFolderBtn.TabIndex = 1;
            this.selectInputFolderBtn.Text = "InputFolder";
            this.selectInputFolderBtn.Click += new System.EventHandler(this.selectInputFolderBtn_Click);
            // 
            // selectOutputFolderBtn
            // 
            this.selectOutputFolderBtn.Location = new System.Drawing.Point(23, 182);
            this.selectOutputFolderBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.selectOutputFolderBtn.Name = "selectOutputFolderBtn";
            this.selectOutputFolderBtn.Size = new System.Drawing.Size(121, 31);
            this.selectOutputFolderBtn.TabIndex = 2;
            this.selectOutputFolderBtn.Text = "OutputFolder";
            this.selectOutputFolderBtn.Click += new System.EventHandler(this.selectOutputFolderBtn_Click);
            // 
            // inputFolderPathLabel
            // 
            this.inputFolderPathLabel.AutoSize = true;
            this.inputFolderPathLabel.Location = new System.Drawing.Point(165, 139);
            this.inputFolderPathLabel.Name = "inputFolderPathLabel";
            this.inputFolderPathLabel.Size = new System.Drawing.Size(52, 19);
            this.inputFolderPathLabel.TabIndex = 3;
            this.inputFolderPathLabel.Text = "Select...";
            // 
            // outputFolderPathLabel
            // 
            this.outputFolderPathLabel.AutoSize = true;
            this.outputFolderPathLabel.Location = new System.Drawing.Point(165, 194);
            this.outputFolderPathLabel.Name = "outputFolderPathLabel";
            this.outputFolderPathLabel.Size = new System.Drawing.Size(52, 19);
            this.outputFolderPathLabel.TabIndex = 4;
            this.outputFolderPathLabel.Text = "Select...";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(23, 278);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(173, 19);
            this.metroLabel1.TabIndex = 6;
            this.metroLabel1.Text = "선택 스레드 / 남은 스레드:";
            // 
            // threadCountLabel
            // 
            this.threadCountLabel.AutoSize = true;
            this.threadCountLabel.Location = new System.Drawing.Point(213, 278);
            this.threadCountLabel.Name = "threadCountLabel";
            this.threadCountLabel.Size = new System.Drawing.Size(0, 0);
            this.threadCountLabel.TabIndex = 7;
            // 
            // addTransformType
            // 
            this.addTransformType.Location = new System.Drawing.Point(23, 559);
            this.addTransformType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addTransformType.Name = "addTransformType";
            this.addTransformType.Size = new System.Drawing.Size(466, 48);
            this.addTransformType.TabIndex = 8;
            this.addTransformType.Text = "변환기 추가";
            this.addTransformType.Click += new System.EventHandler(this.addTransformType_Click);
            // 
            // selectThreadCountCombobox
            // 
            this.selectThreadCountCombobox.FormattingEnabled = true;
            this.selectThreadCountCombobox.ItemHeight = 23;
            this.selectThreadCountCombobox.Location = new System.Drawing.Point(23, 235);
            this.selectThreadCountCombobox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.selectThreadCountCombobox.Name = "selectThreadCountCombobox";
            this.selectThreadCountCombobox.Size = new System.Drawing.Size(121, 29);
            this.selectThreadCountCombobox.TabIndex = 9;
            // 
            // saveFolderLabel
            // 
            this.saveFolderLabel.AutoSize = true;
            this.saveFolderLabel.Location = new System.Drawing.Point(23, 498);
            this.saveFolderLabel.Name = "saveFolderLabel";
            this.saveFolderLabel.Size = new System.Drawing.Size(226, 19);
            this.saveFolderLabel.TabIndex = 10;
            this.saveFolderLabel.Text = "원본 파일 및 로그 파일 보관 경로 : ";
            // 
            // logFolderLabel
            // 
            this.logFolderLabel.AutoSize = true;
            this.logFolderLabel.Location = new System.Drawing.Point(24, 517);
            this.logFolderLabel.Name = "logFolderLabel";
            this.logFolderLabel.Size = new System.Drawing.Size(0, 0);
            this.logFolderLabel.TabIndex = 11;
            // 
            // saveFolderPathLabel
            // 
            this.saveFolderPathLabel.AutoSize = true;
            this.saveFolderPathLabel.Location = new System.Drawing.Point(24, 528);
            this.saveFolderPathLabel.Name = "saveFolderPathLabel";
            this.saveFolderPathLabel.Size = new System.Drawing.Size(126, 19);
            this.saveFolderPathLabel.TabIndex = 12;
            this.saveFolderPathLabel.Text = "{OutputFolder}/save";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 626);
            this.Controls.Add(this.saveFolderPathLabel);
            this.Controls.Add(this.logFolderLabel);
            this.Controls.Add(this.saveFolderLabel);
            this.Controls.Add(this.selectThreadCountCombobox);
            this.Controls.Add(this.addTransformType);
            this.Controls.Add(this.threadCountLabel);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.outputFolderPathLabel);
            this.Controls.Add(this.inputFolderPathLabel);
            this.Controls.Add(this.selectOutputFolderBtn);
            this.Controls.Add(this.selectInputFolderBtn);
            this.Controls.Add(this.metroComboBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form2";
            this.Padding = new System.Windows.Forms.Padding(21, 75, 21, 20);
            this.Text = "변환기 설정";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroComboBox metroComboBox1;
        private MetroFramework.Controls.MetroButton selectInputFolderBtn;
        private MetroFramework.Controls.MetroButton selectOutputFolderBtn;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel threadCountLabel;
        private MetroFramework.Controls.MetroButton addTransformType;
        private MetroFramework.Controls.MetroLabel inputFolderPathLabel;
        private MetroFramework.Controls.MetroLabel outputFolderPathLabel;
        private MetroFramework.Controls.MetroComboBox selectThreadCountCombobox;
        private MetroFramework.Controls.MetroLabel saveFolderLabel;
        private MetroFramework.Controls.MetroLabel logFolderLabel;
        private MetroFramework.Controls.MetroLabel saveFolderPathLabel;
    }
}