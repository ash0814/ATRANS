namespace ATRANS
{
    partial class ErrorListForm
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
            this.components = new System.ComponentModel.Container();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.LogInfoLabel = new MetroFramework.Controls.MetroLabel();
            this.errorListView = new System.Windows.Forms.ListView();
            this.workingThreadNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.errorMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.workingTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.filePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.savePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.errorListLabel = new MetroFramework.Controls.MetroLabel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.metroPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.LogInfoLabel);
            this.metroPanel1.Controls.Add(this.errorListView);
            this.metroPanel1.Controls.Add(this.errorListLabel);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(23, 79);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(1173, 363);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            this.metroPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.metroPanel1_Paint);
            // 
            // LogInfoLabel
            // 
            this.LogInfoLabel.AutoSize = true;
            this.LogInfoLabel.Location = new System.Drawing.Point(13, 0);
            this.LogInfoLabel.Name = "LogInfoLabel";
            this.LogInfoLabel.Size = new System.Drawing.Size(0, 0);
            this.LogInfoLabel.TabIndex = 5;
            // 
            // errorListView
            // 
            this.errorListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.workingThreadNumber,
            this.fileName,
            this.errorMessage,
            this.filePath,
            this.workingTime,
            this.savePath});
            this.errorListView.HideSelection = false;
            this.errorListView.Location = new System.Drawing.Point(13, 21);
            this.errorListView.Name = "errorListView";
            this.errorListView.Size = new System.Drawing.Size(1147, 328);
            this.errorListView.TabIndex = 4;
            this.errorListView.UseCompatibleStateImageBehavior = false;
            this.errorListView.View = System.Windows.Forms.View.Details;
            this.errorListView.SelectedIndexChanged += new System.EventHandler(this.errorListView_SelectedIndexChanged);
            // 
            // workingThreadNumber
            // 
            this.workingThreadNumber.Text = "스레드";
            this.workingThreadNumber.Width = 64;
            // 
            // fileName
            // 
            this.fileName.Text = "파일명";
            this.fileName.Width = 157;
            // 
            // errorMessage
            // 
            this.errorMessage.Text = "오류 내용";
            this.errorMessage.Width = 304;
            // 
            // workingTime
            // 
            this.workingTime.DisplayIndex = 3;
            this.workingTime.Text = "작업일시";
            this.workingTime.Width = 141;
            // 
            // filePath
            // 
            this.filePath.DisplayIndex = 4;
            this.filePath.Text = "파일 경로";
            this.filePath.Width = 251;
            // 
            // savePath
            // 
            this.savePath.Text = "로그저장경로";
            this.savePath.Width = 323;
            // 
            // errorListLabel
            // 
            this.errorListLabel.AutoSize = true;
            this.errorListLabel.Location = new System.Drawing.Point(43, 35);
            this.errorListLabel.Name = "errorListLabel";
            this.errorListLabel.Size = new System.Drawing.Size(0, 0);
            this.errorListLabel.TabIndex = 2;
            // 
            // ErrorListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 465);
            this.Controls.Add(this.metroPanel1);
            this.Name = "ErrorListForm";
            this.Text = "ErrorListForm";
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroLabel errorListLabel;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ListView errorListView;
        private System.Windows.Forms.ColumnHeader fileName;
        private System.Windows.Forms.ColumnHeader workingThreadNumber;
        private System.Windows.Forms.ColumnHeader filePath;
        private System.Windows.Forms.ColumnHeader workingTime;
        private System.Windows.Forms.ColumnHeader errorMessage;
        private MetroFramework.Controls.MetroLabel LogInfoLabel;
        private System.Windows.Forms.ColumnHeader savePath;
    }
}