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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //调用
            copyDirectory(@"C:\Users\Administrator\Desktop\12", @"C:\Users\Administrator\Desktop\13");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 递归拷贝所有子目录。
        /// </summary>
        /// <param >源目录</param>
        /// <param >目的目录</param>
        private void copyDirectory(string sPath, string dPath)
        {
            string[] directories = System.IO.Directory.GetDirectories(sPath);
            if (!System.IO.Directory.Exists(dPath))
                System.IO.Directory.CreateDirectory(dPath);
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(sPath);
            System.IO.DirectoryInfo[] dirs = dir.GetDirectories();
            CopyFile(dir, dPath);
            if (dirs.Length > 0)
            {
                foreach (System.IO.DirectoryInfo temDirectoryInfo in dirs)
                {
                    string sourceDirectoryFullName = temDirectoryInfo.FullName;
                    string destDirectoryFullName = sourceDirectoryFullName.Replace(sPath, dPath);
                    if (!System.IO.Directory.Exists(destDirectoryFullName))
                    {
                        System.IO.Directory.CreateDirectory(destDirectoryFullName);
                    }
                    CopyFile(temDirectoryInfo, destDirectoryFullName);
                    copyDirectory(sourceDirectoryFullName, destDirectoryFullName);
                }
            }

        }

        /// <summary>
        /// 拷贝目录下的所有文件到目的目录。
        /// </summary>
        /// <param >源路径</param>
        /// <param >目的路径</param>
        private void CopyFile(System.IO.DirectoryInfo path, string desPath)
        {
            string sourcePath = path.FullName;
            System.IO.FileInfo[] files = path.GetFiles();
            foreach (System.IO.FileInfo file in files)
            {
                string sourceFileFullName = file.FullName;
                string destFileFullName = sourceFileFullName.Replace(sourcePath, desPath);
                file.CopyTo(destFileFullName, true);
            }
        }

        
    }
}

