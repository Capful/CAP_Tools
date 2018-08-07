using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CAP_Tools.Pages.List.NcProgram
{
    /// <summary>
    /// Interaction logic for NCMerge.xaml
    /// </summary>
    public partial class NCMerge : System.Windows.Controls.UserControl
    {
        public NCMerge()
        {
            InitializeComponent();
        }

        private void Xz_Click(object sender, RoutedEventArgs e)
        {
            ///打开选择文件夹对话框
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string m_Dir = m_Dialog.SelectedPath.Trim();
            this.NXRoute.Text = m_Dir;
            ///判断选择的文件夹中是否含有后缀名为NC的文件
            if (System.IO.Directory.GetFiles(m_Dir, "*.nc").Length > 0)
            {
                ///如果存在，将替换按钮显示
                this.CL.IsEnabled = true;
                ///读取选择的文件夹中NC文件
                ///清空ListBox
                list.Items.Clear();
                string s = null;
                string a = null;
                DirectoryInfo d = new DirectoryInfo(m_Dir);
                FileInfo[] Files = d.GetFiles("*.nc");
                List<string> lstr = new List<string>();
                ///获取文件夹下文件全路径
                foreach (FileInfo file in Files)
                {
                    s = file.FullName;
                    lstr.Add(s);
                }
                ///获取文件夹下文件名，将路径显示到ListBox
                foreach (FileInfo file in Files)
                {
                    a = file.Name;
                    list.Items.Add(a);
                }
                }
                else
                {
                ///不存在
                this.CL.IsEnabled = false;
                ///清空ListBox
                list.Items.Clear();
                ModernDialog.ShowMessage("您选择的文件夹不存在.NC文件程序", "警告", MessageBoxButton.OK);
                }
        }

        private void CL_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
