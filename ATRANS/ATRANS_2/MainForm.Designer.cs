namespace ATRANS
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.startTransformBtn = new MetroFramework.Controls.MetroButton();
            this.addTransformerBtn = new MetroFramework.Controls.MetroButton();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.thread3ErrorCnt = new MetroFramework.Controls.MetroLabel();
            this.thread3SuccessCnt = new MetroFramework.Controls.MetroLabel();
            this.thread2ErrorCnt = new MetroFramework.Controls.MetroLabel();
            this.thread2SuccessCnt = new MetroFramework.Controls.MetroLabel();
            this.thread1ErrorCnt = new MetroFramework.Controls.MetroLabel();
            this.thread1SuccessCnt = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.threadStatus3 = new MetroFramework.Controls.MetroLabel();
            this.threadStatus2 = new MetroFramework.Controls.MetroLabel();
            this.threadStatus1 = new MetroFramework.Controls.MetroLabel();
            this.previousBtn = new MetroFramework.Controls.MetroButton();
            this.finishBtn = new MetroFramework.Controls.MetroButton();
            this.metroPanel1.SuspendLayout();
            this.metroPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.startTransformBtn);
            this.metroPanel1.Controls.Add(this.addTransformerBtn);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(23, 78);
            this.metroPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(826, 370);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // startTransformBtn
            // 
            this.startTransformBtn.Location = new System.Drawing.Point(687, 321);
            this.startTransformBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.startTransformBtn.Name = "startTransformBtn";
            this.startTransformBtn.Size = new System.Drawing.Size(123, 31);
            this.startTransformBtn.TabIndex = 1;
            this.startTransformBtn.Text = "START";
            this.startTransformBtn.Click += new System.EventHandler(this.startTransformBtn_Click);
            // 
            // addTransformerBtn
            // 
            this.addTransformerBtn.Location = new System.Drawing.Point(49, 25);
            this.addTransformerBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addTransformerBtn.Name = "addTransformerBtn";
            this.addTransformerBtn.Size = new System.Drawing.Size(180, 32);
            this.addTransformerBtn.TabIndex = 2;
            this.addTransformerBtn.Text = "Add Transformer";
            this.addTransformerBtn.Click += new System.EventHandler(this.addTransformerBtn_Click);
            // 
            // metroPanel2
            // 
            this.metroPanel2.Controls.Add(this.thread3ErrorCnt);
            this.metroPanel2.Controls.Add(this.thread3SuccessCnt);
            this.metroPanel2.Controls.Add(this.thread2ErrorCnt);
            this.metroPanel2.Controls.Add(this.thread2SuccessCnt);
            this.metroPanel2.Controls.Add(this.thread1ErrorCnt);
            this.metroPanel2.Controls.Add(this.thread1SuccessCnt);
            this.metroPanel2.Controls.Add(this.metroLabel2);
            this.metroPanel2.Controls.Add(this.metroLabel1);
            this.metroPanel2.Controls.Add(this.threadStatus3);
            this.metroPanel2.Controls.Add(this.threadStatus2);
            this.metroPanel2.Controls.Add(this.threadStatus1);
            this.metroPanel2.Controls.Add(this.previousBtn);
            this.metroPanel2.Controls.Add(this.finishBtn);
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(23, 74);
            this.metroPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(826, 374);
            this.metroPanel2.TabIndex = 1;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // thread3ErrorCnt
            // 
            this.thread3ErrorCnt.AutoSize = true;
            this.thread3ErrorCnt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.thread3ErrorCnt.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.thread3ErrorCnt.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.thread3ErrorCnt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.thread3ErrorCnt.Location = new System.Drawing.Point(732, 201);
            this.thread3ErrorCnt.Name = "thread3ErrorCnt";
            this.thread3ErrorCnt.Size = new System.Drawing.Size(22, 25);
            this.thread3ErrorCnt.Style = MetroFramework.MetroColorStyle.Red;
            this.thread3ErrorCnt.TabIndex = 15;
            this.thread3ErrorCnt.Text = "0";
            this.thread3ErrorCnt.UseStyleColors = true;
            this.thread3ErrorCnt.Click += new System.EventHandler(this.thread3ErrorCnt_Click);
            // 
            // thread3SuccessCnt
            // 
            this.thread3SuccessCnt.AutoSize = true;
            this.thread3SuccessCnt.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.thread3SuccessCnt.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.thread3SuccessCnt.Location = new System.Drawing.Point(645, 195);
            this.thread3SuccessCnt.Name = "thread3SuccessCnt";
            this.thread3SuccessCnt.Size = new System.Drawing.Size(22, 25);
            this.thread3SuccessCnt.Style = MetroFramework.MetroColorStyle.Green;
            this.thread3SuccessCnt.TabIndex = 14;
            this.thread3SuccessCnt.Text = "0";
            this.thread3SuccessCnt.UseStyleColors = true;
            // 
            // thread2ErrorCnt
            // 
            this.thread2ErrorCnt.AutoSize = true;
            this.thread2ErrorCnt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.thread2ErrorCnt.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.thread2ErrorCnt.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.thread2ErrorCnt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.thread2ErrorCnt.Location = new System.Drawing.Point(732, 141);
            this.thread2ErrorCnt.Name = "thread2ErrorCnt";
            this.thread2ErrorCnt.Size = new System.Drawing.Size(22, 25);
            this.thread2ErrorCnt.Style = MetroFramework.MetroColorStyle.Red;
            this.thread2ErrorCnt.TabIndex = 13;
            this.thread2ErrorCnt.Text = "0";
            this.thread2ErrorCnt.UseStyleColors = true;
            this.thread2ErrorCnt.Click += new System.EventHandler(this.thread2ErrorCnt_Click);
            // 
            // thread2SuccessCnt
            // 
            this.thread2SuccessCnt.AutoSize = true;
            this.thread2SuccessCnt.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.thread2SuccessCnt.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.thread2SuccessCnt.Location = new System.Drawing.Point(645, 141);
            this.thread2SuccessCnt.Name = "thread2SuccessCnt";
            this.thread2SuccessCnt.Size = new System.Drawing.Size(22, 25);
            this.thread2SuccessCnt.Style = MetroFramework.MetroColorStyle.Green;
            this.thread2SuccessCnt.TabIndex = 12;
            this.thread2SuccessCnt.Text = "0";
            this.thread2SuccessCnt.UseStyleColors = true;
            // 
            // thread1ErrorCnt
            // 
            this.thread1ErrorCnt.AutoSize = true;
            this.thread1ErrorCnt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.thread1ErrorCnt.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.thread1ErrorCnt.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.thread1ErrorCnt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.thread1ErrorCnt.Location = new System.Drawing.Point(732, 91);
            this.thread1ErrorCnt.Name = "thread1ErrorCnt";
            this.thread1ErrorCnt.Size = new System.Drawing.Size(22, 25);
            this.thread1ErrorCnt.Style = MetroFramework.MetroColorStyle.Red;
            this.thread1ErrorCnt.TabIndex = 11;
            this.thread1ErrorCnt.Text = "0";
            this.thread1ErrorCnt.UseStyleColors = true;
            this.thread1ErrorCnt.Click += new System.EventHandler(this.thread1ErrorCnt_Click);
            // 
            // thread1SuccessCnt
            // 
            this.thread1SuccessCnt.AutoSize = true;
            this.thread1SuccessCnt.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.thread1SuccessCnt.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.thread1SuccessCnt.Location = new System.Drawing.Point(645, 91);
            this.thread1SuccessCnt.Name = "thread1SuccessCnt";
            this.thread1SuccessCnt.Size = new System.Drawing.Size(22, 25);
            this.thread1SuccessCnt.Style = MetroFramework.MetroColorStyle.Green;
            this.thread1SuccessCnt.TabIndex = 10;
            this.thread1SuccessCnt.Text = "0";
            this.thread1SuccessCnt.UseStyleColors = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(724, 48);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(37, 19);
            this.metroLabel2.TabIndex = 9;
            this.metroLabel2.Text = "실패";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(629, 48);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(37, 19);
            this.metroLabel1.TabIndex = 8;
            this.metroLabel1.Text = "성공";
            // 
            // threadStatus3
            // 
            this.threadStatus3.AutoSize = true;
            this.threadStatus3.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.threadStatus3.Location = new System.Drawing.Point(75, 201);
            this.threadStatus3.Name = "threadStatus3";
            this.threadStatus3.Size = new System.Drawing.Size(169, 19);
            this.threadStatus3.TabIndex = 6;
            this.threadStatus3.Text = "THREAD NOT WORKING";
            // 
            // threadStatus2
            // 
            this.threadStatus2.AutoSize = true;
            this.threadStatus2.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.threadStatus2.Location = new System.Drawing.Point(75, 147);
            this.threadStatus2.Name = "threadStatus2";
            this.threadStatus2.Size = new System.Drawing.Size(169, 19);
            this.threadStatus2.TabIndex = 5;
            this.threadStatus2.Text = "THREAD NOT WORKING";
            // 
            // threadStatus1
            // 
            this.threadStatus1.AutoSize = true;
            this.threadStatus1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.threadStatus1.Location = new System.Drawing.Point(75, 97);
            this.threadStatus1.Name = "threadStatus1";
            this.threadStatus1.Size = new System.Drawing.Size(169, 19);
            this.threadStatus1.TabIndex = 4;
            this.threadStatus1.Text = "THREAD NOT WORKING";
            // 
            // previousBtn
            // 
            this.previousBtn.Location = new System.Drawing.Point(21, 318);
            this.previousBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.previousBtn.Name = "previousBtn";
            this.previousBtn.Size = new System.Drawing.Size(129, 39);
            this.previousBtn.TabIndex = 3;
            this.previousBtn.Text = "PREVIOUS";
            this.previousBtn.Click += new System.EventHandler(this.previousBtn_Click);
            // 
            // finishBtn
            // 
            this.finishBtn.Location = new System.Drawing.Point(682, 318);
            this.finishBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.finishBtn.Name = "finishBtn";
            this.finishBtn.Size = new System.Drawing.Size(129, 39);
            this.finishBtn.TabIndex = 2;
            this.finishBtn.Text = "FINISH";
            this.finishBtn.Click += new System.EventHandler(this.finishBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 496);
            this.Controls.Add(this.metroPanel2);
            this.Controls.Add(this.metroPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(21, 75, 21, 20);
            this.Text = "ATRANS";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroButton addTransformerBtn;
        private MetroFramework.Controls.MetroButton startTransformBtn;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MetroFramework.Controls.MetroButton previousBtn;
        private MetroFramework.Controls.MetroButton finishBtn;
        private MetroFramework.Controls.MetroLabel threadStatus3;
        private MetroFramework.Controls.MetroLabel threadStatus2;
        private MetroFramework.Controls.MetroLabel threadStatus1;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel thread3ErrorCnt;
        private MetroFramework.Controls.MetroLabel thread3SuccessCnt;
        private MetroFramework.Controls.MetroLabel thread2ErrorCnt;
        private MetroFramework.Controls.MetroLabel thread2SuccessCnt;
        private MetroFramework.Controls.MetroLabel thread1ErrorCnt;
        private MetroFramework.Controls.MetroLabel thread1SuccessCnt;
    }
}

