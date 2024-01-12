using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATRANS
{
    public partial class SetTransformerForm : MetroFramework.Forms.MetroForm
    {
        private string inputFolderPath;
        private string outputFolderPath;
        private string transformType;
        private int totalLeftThread;
        private int threadCount;
        private List<string> typeList;

        public delegate void FormSendDataHandler(string inputFolderPath, string outputFolderPath, string transformType, int threadCount); //delegate 선언
        public event FormSendDataHandler FormSendEvent; //event 생성

        public SetTransformerForm(int leftThread, List<string> transformTypeList)
        {
            InitializeComponent();
            this.Text = "변환기 설정";

            totalLeftThread = leftThread;
            typeList = transformTypeList;


            transformType = typeList[0];
            metroComboBox1.DataSource = typeList;
            metroComboBox1.SelectedIndexChanged += OnChangedTransformType;


            List<int> threadCounts = new List<int>();
            for (int i = 1; i <= totalLeftThread; i++)
                threadCounts.Add(i);
            threadCount = 1;
            selectThreadCountCombobox.DataSource = threadCounts;
            selectThreadCountCombobox.SelectedIndexChanged += OnChangedSelectThreadCount;
            threadCountLabel.Text = threadCount + "/" + totalLeftThread;
        }

        public SetTransformerForm(int leftThread, List<string> transformTypeList,string currentType, string input, string output, int currentThread) 
        {
            InitializeComponent();
            totalLeftThread = leftThread;
            typeList = transformTypeList;
            transformType = currentType;
            inputFolderPath = input;
            outputFolderPath = output;
            threadCount = currentThread;

            inputFolderPathLabel.Text = inputFolderPath;
            outputFolderPathLabel.Text = outputFolderPath;

            Console.WriteLine("Type: " + transformType + ", input: " + inputFolderPath);

            metroComboBox1.DataSource = typeList;
            metroComboBox1.SelectedIndexChanged += OnChangedTransformType;
            metroComboBox1.SelectedIndex = typeList.IndexOf(currentType);


            List<int> threadCounts = new List<int>();
            for (int i = 1; i <= totalLeftThread; i++)
                threadCounts.Add(i);
            selectThreadCountCombobox.DataSource = threadCounts;
            selectThreadCountCombobox.SelectedIndexChanged += OnChangedSelectThreadCount;
            threadCountLabel.Text = threadCount + "/" + totalLeftThread;
            selectThreadCountCombobox.SelectedIndex = threadCount - 1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (saveFolderPathLabel.Text != "")
                saveFolderPathLabel.Text = $" {outputFolderPath}\\Atrans_save";
        }

        private void OnChangedSelectThreadCount (object sender, EventArgs e) 
        {
            threadCount = int.Parse(selectThreadCountCombobox.SelectedItem.ToString());
            threadCountLabel.Text = threadCount.ToString() + "/" + totalLeftThread;
        }
        private void OnChangedTransformType(object sender, EventArgs e) 
        {
            string selectedType = metroComboBox1.SelectedItem.ToString();
            transformType = selectedType;
        }

        private string SelectFolder()
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    return folderBrowserDialog.SelectedPath;
                }

                return null;
            }

        }

        private void selectInputFolderBtn_Click(object sender, EventArgs e)
        {
            inputFolderPath = SelectFolder();
            inputFolderPathLabel.Text = inputFolderPath;
        }

        private void selectOutputFolderBtn_Click(object sender, EventArgs e)
        {
            outputFolderPath = SelectFolder();
            outputFolderPathLabel.Text = outputFolderPath;
            saveFolderPathLabel.Text = $" {outputFolderPath}\\Atrans_save";
        }

        private Boolean checkFolderPath()
        {
            if (inputFolderPath == outputFolderPath)
            {
                return false;
            }
            return true;
        }

        private void addTransformType_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(inputFolderPath) || string.IsNullOrEmpty(outputFolderPath) || string.IsNullOrEmpty(transformType) || threadCount <= 0)
            {
                MessageBox.Show("모든 필드를 채워주세요.", "실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (checkFolderPath() == false)
            {
                MessageBox.Show("입력 경로와 출력 경로는 같을 수 없습니다.", "실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.FormSendEvent(inputFolderPath, outputFolderPath, transformType, threadCount);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }

}
