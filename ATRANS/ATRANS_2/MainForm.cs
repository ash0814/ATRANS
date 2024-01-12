using MetroFramework.Controls;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace ATRANS
{
    public partial class MainForm : MetroFramework.Forms.MetroForm
    {
        private List<Transformer> transformers = new List<Transformer>();
        private List<string> transformTypeList = new List<string>();

        public Dictionary<int, ObservableCollection<ListViewItem>> errorList = new Dictionary<int, ObservableCollection<ListViewItem>>();

        private int leftThread = 3;
        private int transformerTileIndex = 0;
        public  int t_idx = 0;

        private ErrorListForm errorWindow_1;
        private ErrorListForm errorWindow_2;
        private ErrorListForm errorWindow_3;

        private string CloseWarningMessage = "작업 중(IN PROGRESS)인 스레드가 모두 종료되며, 파일이 불완전하게 저장될 수 있습니다. 계속하시겠습니까?";

        public MainForm()
        {
            InitializeComponent();
            this.Text = "파일 변환 TOOL";
            transformTypeList.Add("ATRANS");
            metroPanel1.Visible = true;
            metroPanel2.Visible = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (transformers.Count == 0)
                startTransformBtn.Enabled = false;
        }

        private void addTransformerBtn_Click(object sender, EventArgs e)
        {
            if (leftThread <= 0)
            {
                MessageBox.Show($"사용 가능한 스레드 3 개를 모두 사용하였습니다.");
                return ;
            }
            if (transformTypeList.Count == 0)
            {
                MessageBox.Show($"사용 가능한 변환 타입을 모두 사용하였습니다.");
                return;
            }
            SetTransformerForm getNewTransformer = new SetTransformerForm(leftThread, transformTypeList);
            getNewTransformer.FormSendEvent += new SetTransformerForm.FormSendDataHandler(getNewTransformerData);
            getNewTransformer.ShowDialog();

            if (getNewTransformer.DialogResult == DialogResult.OK)
            {
                addNewTile(transformerTileIndex);
            }
        }

        private void editTransformerSetting(int index, MetroTile tile)
        {
            Transformer current = transformers[index];
            leftThread += current.threadCount;
            transformTypeList.Add(current.transformType);
            SetTransformerForm editTransform = new SetTransformerForm(leftThread, transformTypeList, current.transformType, current.inputPath, current.outputPath, current.threadCount);
            editTransform.FormSendEvent += new SetTransformerForm.FormSendDataHandler(getNewTransformerData);
            editTransform.ShowDialog();

            if (editTransform.DialogResult == DialogResult.OK)
            {
                transformers.Remove(current);
                this.metroPanel1.Controls.Remove(tile);
                addNewTile(index);
            } else
            {
                leftThread -= current.threadCount;
                transformTypeList.Remove(current.transformType);
            }
        }

        private void addNewTile(int idx) 
        {
            MetroTile tile = new MetroTile();
            Transformer tmp = transformers[idx];
            tile.Click += new System.EventHandler((object sender, EventArgs e) => editTransformerSetting(idx, tile));
            tile.Name = tmp.transformerIndex.ToString();
            tile.Text = 
                $"Type: {tmp.transformType}\n" +
                $"Thread : {tmp.threadCount}\n";
            tile.TextAlign = ContentAlignment.MiddleCenter;
            tile.BackColor = Color.White;
            tile.TabIndex = idx + 3;
            tile.Size = new System.Drawing.Size(240, 115);
            if (idx == 0)
            {
                tile.Location = new System.Drawing.Point(20, 100);

            } else if (idx == 1)
            {
                tile.Location = new System.Drawing.Point(290, 100);
            }
            else 
            {
                tile.Location = new System.Drawing.Point(560, 100);
            }
            this.metroPanel1.Controls.Add(tile);
            transformerTileIndex++;
        }

        private void getNewTransformerData(string inputFolderPath, string outputFolderPath, string transformType, int threadCount)
        {
            int idx = transformers.Count();
            Transformer transformer = new Transformer(inputFolderPath, outputFolderPath, threadCount, idx, transformType, UpdateThreadStatusUI, UpdateThreadCountUI, this);
            leftThread -= threadCount;
            transformTypeList.Remove(transformType);
            transformers.Add(transformer);
            if (transformers.Count > 0)
                startTransformBtn.Enabled = true;
        }

        
        private void formCloseEvent(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(CloseWarningMessage, "경고", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                foreach (var t in transformers)
                    t.Stop();
                e.Cancel = false;
            }
            else
                e.Cancel = true;
        }


        private void startTransformBtn_Click(object sender, EventArgs e)
        {
            try
            {
                startTransformers();
                metroPanel1.Visible = false;
                metroPanel2.Visible = true;
                this.FormClosing += formCloseEvent;
            }
            catch (Exception ex)
            {
                MessageBox.Show("변환기 생성에 실패하였습니다. 다시 시도해주세요.\n\n에러 메시지: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateThreadStatusUI(int threadIdx, string status)
        {
            Label label;
            if (threadIdx == 0) { label = threadStatus1; }
            else if (threadIdx == 1) { label = threadStatus2; }
            else { label = threadStatus3; }
            UpdateUI(label, status);
        }

        private void UpdateThreadCountUI(int threadIdx, bool success)
        {
            Label label;

            if (threadIdx == 0) {
                if (success)
                    label = thread1SuccessCnt;
                else
                    label = thread1ErrorCnt; 
            } else if (threadIdx == 1)
            {
                if (success)
                    label = thread2SuccessCnt;
                else label = thread2ErrorCnt;
            } else
            {
                if (success)
                    label = thread3SuccessCnt;
                else label= thread3ErrorCnt;
            }
            UpdateCnt(label);
        }

        private void UpdateUI(Label label , string status)
        { 
            if (label.InvokeRequired)
                label.Invoke(new Action(() => UpdateUI(label, status)));
            else
                label.Text = status;
        }

        private void UpdateCnt(Label label)
        {
            if ( (label.InvokeRequired))
            {
                label.Invoke(new Action(() => UpdateCnt(label)));
            }
            else
            {
                label.Text = (int.Parse(label.Text) + 1).ToString();
            }
        }

        private void startTransformers()
        {
           
            foreach (var transformer in transformers)
            {
                transformer.initTransformer();
            }       
        }

        private void finishBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(CloseWarningMessage, "경고", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                this.FormClosing -= formCloseEvent;
                foreach (var t in transformers)
                    t.Stop();
                Application.Exit();
            }
        }

        private void initFormTextUI()
        {
            threadStatus1.Text = "THREAD NOT WORKING";
            threadStatus2.Text = "THREAD NOT WORKING";
            threadStatus3.Text = "THREAD NOT WORKING";

            thread1ErrorCnt.Text = "0";
            thread2ErrorCnt.Text = "0";
            thread3ErrorCnt.Text = "0";
            thread1SuccessCnt.Text = "0";
            thread2SuccessCnt.Text = "0";
            thread3SuccessCnt.Text = "0";
        }

        private void previousBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(CloseWarningMessage, "경고", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                foreach (var t in transformers)
                {
                    t.Stop();
                }

                if (errorWindow_1 != null)
                    errorWindow_1.Close();
                if (errorWindow_2 != null) 
                    errorWindow_2.Close();
                if (errorWindow_3 != null) 
                    errorWindow_3.Close();

                metroPanel1.Visible = true;
                metroPanel2.Visible = false;

                this.FormClosing -= formCloseEvent;

                initFormTextUI();

                t_idx = 0;
                errorList.Clear();
            }
        }
        private void thread1ErrorCnt_Click(object sender, EventArgs e)
        {
            if (errorList.ContainsKey(0))
            {
                ObservableCollection<ListViewItem> items = errorList[0];

                if (errorWindow_1 == null || errorWindow_1.IsDisposed)
                {
                    errorWindow_1 = new ErrorListForm(items, 0);
                    errorWindow_1.Closed += (s, events) =>
                    {
                        errorWindow_1 = null;
                    };
                    items.CollectionChanged += (s, events) =>
                    {
                        if (errorWindow_1 != null )
                            errorWindow_1.UpdateErrorList(items);
                    };
                    errorWindow_1.Show();
                }
                else
                {
                    errorWindow_1.Activate();
                }
            }
        }

        private void thread2ErrorCnt_Click(object sender, EventArgs e)
        {
            if (errorList.ContainsKey(1))
            {
                ObservableCollection<ListViewItem> items = errorList[1];

                if (errorWindow_2 == null || errorWindow_2.IsDisposed)
                {
                    errorWindow_2 = new ErrorListForm(items,1);
                    errorWindow_2.Closed += (s, events) =>
                    {
                        errorWindow_2 = null;
                    };
                    items.CollectionChanged += (s, events) =>
                    {
                        if (errorWindow_2 != null)
                            errorWindow_2.UpdateErrorList(items);
                    };
                    errorWindow_2.Show();
                }
                else
                {
                    errorWindow_2.Activate();
                }
            }
        }

        private void thread3ErrorCnt_Click(object sender, EventArgs e)
        {
            if (errorList.ContainsKey(2))
            {
                ObservableCollection<ListViewItem> items = errorList[2];

                if (errorWindow_3 == null || errorWindow_3.IsDisposed)
                {
                    errorWindow_3 = new ErrorListForm(items,2);
                    errorWindow_3.Closed += (s, events) =>
                    {
                        errorWindow_3 = null;
                    };
                    items.CollectionChanged += (s, events) =>
                    {
                        if (errorWindow_3 != null)
                            errorWindow_3.UpdateErrorList(items);
                    };
                    errorWindow_3.Show();
                }
                else
                {
                    errorWindow_3.Activate();
                }
            }
        }
    }
}
