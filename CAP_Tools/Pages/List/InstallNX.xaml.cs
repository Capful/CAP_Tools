using System.Windows;
using System.Windows.Forms;
using System.IO;
using FirstFloor.ModernUI.Windows.Controls;

namespace CAP_Tools.Pages
{
    /// <summary>
    /// Interaction logic for InstallNX.xaml
    /// </summary>
    public partial class InstallNX : System.Windows.Controls.UserControl
    {
        public InstallNX()
        {
            InitializeComponent();
        }

        public object DialogResult { get; private set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string m_Dir = m_Dialog.SelectedPath.Trim();
            this.NXRoute.Text = m_Dir;
            string path1 = NXRoute.Text;
            string path2 = "Launch.exe";
            string newPath = System.IO.Path.Combine(path1, path2);
            if (File.Exists(@newPath))
            {
                this.Install.IsEnabled = true;
            }
            else
            {
                this.Install.IsEnabled = false;
                ModernDialog.ShowMessage("NX安装主程序不存在，请重新选择文件夹或检测安装程序完整性", "警告", MessageBoxButton.OK);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string path1 = NXRoute.Text;
            DirectoryInfo dir = new DirectoryInfo(@path1);
            foreach (FileInfo file in dir.GetFiles("setup.exe", SearchOption.AllDirectories))//第二个参数表示搜索包含子目录中的文件；
            {
                if (file.Name.Contains("setup.exe"))
                    System.Diagnostics.Process.Start(file.FullName);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}

