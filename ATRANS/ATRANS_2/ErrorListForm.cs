using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATRANS
{
    public partial class ErrorListForm : MetroFramework.Forms.MetroForm
    {
        private int threadIndex;
        private ObservableCollection<ListViewItem> items;
        public ErrorListForm(ObservableCollection<ListViewItem> errorList, int idx)
        {
            InitializeComponent();
            items = errorList;
            threadIndex = idx;
            LogInfoLabel.Text = $"자세한 로그는 로그파일 경로를 참조하세요";
            this.Text = $"스레드 {threadIndex} Error List";
        }

        private void metroPanel1_Paint(object sender, PaintEventArgs e)
        {
            errorListView.Items.Clear();
            errorListView.Items.AddRange(items.ToArray());
        }

        public void UpdateErrorList(ObservableCollection<ListViewItem> newList)
        {
            if (errorListView.InvokeRequired)
            {
                // 다른 스레드에서 호출된 경우 메인 스레드에서 업데이트하도록 Invoke 메서드를 사용
                errorListView.Invoke(new Action(() => UpdateErrorList(newList)));
            }
            else
            {
                // 메인 스레드에서 호출된 경우 직접 업데이트
                items = newList;
                errorListView.Items.Clear();
                errorListView.Items.AddRange(items.ToArray());
            }
        }

        private void errorListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //errorListView.Items.Clear();
            //foreach (ListViewItem error in items)
            //    errorListView.Items.Add(error);
        }
    }
}
