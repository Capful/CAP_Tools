using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using System;
using System.Net;
using System.Threading;
using System.Windows.Controls;
using System.ComponentModel;

namespace CAP_Tools.Pages
{
    /// <summary>
    /// Interaction logic for InstallNX.xaml
    /// </summary>
    public partial class InstallNX : System.Windows.Controls.UserControl
    {
        //下载文件
        private string _saveDir;
        internal class FileMessage
        {
            public string FileName { get; set; }
            public string RelativeUrl { get; set; }
            public string Url { get; set; }
            public bool IsDownLoad { get; set; }
            public string SavePath { get; set; }
        }
        public InstallNX()
        {
            InitializeComponent();
            //在程序所在路径新建文件夹
            _saveDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NX License Servers");
        }

        private static RegistryKey NXregistry()
        {
            return Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Unigraphics Solutions\Installed Applications");
        }

        public object DialogResult { get; private set; }

        private void XZ_Click(object sender, RoutedEventArgs e)
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
                this.NXInstall.IsEnabled = true;
                this.NXCrack.IsEnabled = true;
            }
            else
            {
                this.NXInstall.IsEnabled = false;
                this.NXCrack.IsEnabled = false;
                ModernDialog.ShowMessage("NX安装主程序不存在，请重新选择文件夹或检测安装程序完整性", "警告", MessageBoxButton.OK);
            }
        }

        private void NXInstall_Click(object sender, RoutedEventArgs e)
        {
            string path1 = NXRoute.Text;
            DirectoryInfo dir = new DirectoryInfo(@path1);
            foreach (FileInfo file in dir.GetFiles("setup.exe", SearchOption.AllDirectories))//第二个参数表示搜索包含子目录中的文件；
            {
                if (file.Name.Contains("setup.exe"))
                    System.Diagnostics.Process.Start(file.FullName);
            }
        }

        private void NXCrack_Click(object sender, RoutedEventArgs e)
        {
            string path1 = NXRoute.Text;
            string nx85 = path1 + "\\NX_8.5.0_Win64_crack_SSQ";
            string nx10 = path1 + "\\破解文件\\NX 10.0";
            string nx11 = path1 + "\\破解文件\\NX 11.0";
            string nx12 = path1 + "\\破解文件\\NX 12.0";
            MessageBoxResult result = ModernDialog.ShowMessage("确定要破解NX吗？请确保NX软件都己经关闭。", "提示", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (Directory.Exists(nx85))//判断是否存在
                {
                    if (CheckNX85() == true)
                    {
                        ///获取NX安装路径
                        RegistryKey driverKey = NXregistry();
                        string EXE = (String)driverKey.GetValue("Unigraphics V26.5");
                        string Home = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@EXE)));
                        ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
                        ///打开主目录
                        Copy(nx85, Home);
                        ModernDialog.ShowMessage("破解完成，请继续安装许可证！", "提示", MessageBoxButton.OK);
                    }
                    else
                    {
                        ModernDialog.ShowMessage("抱歉，您未安装NX8.5！请先安装NX8.5主程序后再进行破解文件", "警告", MessageBoxButton.OK);
                    }
                }
                else
                {
                    if (Directory.Exists(nx10))//判断是否存在
                    {
                        if (CheckNX10() == true)
                        {
                            ///获取NX安装路径
                            RegistryKey driverKey = NXregistry();
                            string EXE = (String)driverKey.GetValue("Unigraphics V28.0");
                            string Home = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@EXE)));
                            ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
                            ///打开主目录
                            Copy(nx10, Home);
                            ModernDialog.ShowMessage("破解完成，请继续安装许可证！", "提示", MessageBoxButton.OK);
                        }
                        else
                        {
                            ModernDialog.ShowMessage("抱歉，您未安装NX10！请先安装NX10主程序后再进行破解文件", "警告", MessageBoxButton.OK);
                        }
                    }
                    else
                    {
                        if (Directory.Exists(nx11))//判断是否存在
                        {
                            if (CheckNX11() == true)
                            {
                                ///获取NX安装路径
                                RegistryKey driverKey = NXregistry();
                                string EXE = (String)driverKey.GetValue("Unigraphics V29.0");
                                string Home = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@EXE)));
                                ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
                                ///打开主目录
                                Copy(nx11, Home);
                                ModernDialog.ShowMessage("破解完成，请继续安装许可证！", "提示", MessageBoxButton.OK);
                            }
                            else
                            {
                                ModernDialog.ShowMessage("抱歉，您未安装NX11！请先安装NX11主程序后再进行破解文件", "警告", MessageBoxButton.OK);
                            }
                        }
                        else
                        {
                            if (Directory.Exists(nx12))//判断是否存在
                            {
                                if (CheckNX12() == true)
                                {
                                    ///获取NX安装路径
                                    RegistryKey driverKey = NXregistry();
                                    string EXE = (String)driverKey.GetValue("Unigraphics V30.0");
                                    string Home = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@EXE)));
                                    ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
                                    ///打开主目录
                                    Copy(nx12, Home);
                                    ModernDialog.ShowMessage("破解完成，请继续安装许可证！", "提示", MessageBoxButton.OK);
                                }
                                else
                                {
                                    ModernDialog.ShowMessage("抱歉，您未安装NX12！请先安装NX12主程序后再进行破解文件", "警告", MessageBoxButton.OK);
                                }
                            }
                            else
                            {

                                ModernDialog.ShowMessage("未在安装目录找到破解文件，请检查安装包是否完整。\n\r或者检查破解文件的目录名称是否为‘破解文件’，如果不是，请更改后重试", "警告", MessageBoxButton.OK);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 复制文件夹中的所有内容
        /// </summary>
        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        private bool CheckNX12()
        {
            RegistryKey driverKey = NXregistry();
            string NXEXE = (String)driverKey.GetValue("Unigraphics V30.0");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NXEXE)));
            if (NXPath != null)
            {
                return true;
            }
            return false;
        }

        private bool CheckNX11()
        {
            RegistryKey driverKey = NXregistry();
            string NXEXE = (String)driverKey.GetValue("Unigraphics V29.0");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NXEXE)));
            if (NXPath != null)
            {
                return true;
            }
            return false;
        }

        private bool CheckNX10()
        {
            RegistryKey driverKey = NXregistry();
            string NXEXE = (String)driverKey.GetValue("Unigraphics V28.0");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NXEXE)));
            if (NXPath != null)
            {
                return true;
            }
            return false;
        }

        private bool CheckNX85()
        {
            RegistryKey driverKey = NXregistry();
            string NXEXE = (String)driverKey.GetValue("Unigraphics V26.5");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NXEXE)));
            if (NXPath != null)
            {
                return true;
            }
            return false;
        }

        private void NXLicence_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\NX_License_Servers_v1.0.0.exe"))
            {
                System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\NX_License_Servers_v1.0.0.exe");
            }
            else
            {
                //许可证不存在是下载许可证
                //按钮不可用
                this.NXLicence.IsEnabled = false;
                //进度条显示
                this.Prog.Visibility = Visibility.Visible;
                //进度条百分比显示
                this.label1.Visibility = Visibility.Visible;
                this.label2.Visibility = Visibility.Visible;
                //远程文件路径
                string imageUrl = "https://www.baidupcs.com/rest/2.0/pcs/file?method=batchdownload&app_id=250528&zipcontent=%7B%22fs_id%22%3A%5B132603429003150%5D%7D&sign=DCb740ccc5511e5e8fedcff06b081203:n0hS4J5pfT0o31LI0VL7IAWSucI%3D&uid=573454139&time=1543294950&dp-logid=7643041996455772472&dp-callid=0&vuk=573454139&zipname=NX_License_Servers_v1.0.0.zip";
                string fileExt = Path.GetExtension(imageUrl);
                string fileNewName = Guid.NewGuid() + fileExt;
                bool isDownLoad = false;
                string filePath = Path.Combine(_saveDir, fileNewName);
                if (File.Exists(filePath))
                {
                    isDownLoad = true;
                }
                var file = new FileMessage
                {
                    FileName = fileNewName,
                    RelativeUrl = "NX License Servers.zip",
                    Url = imageUrl,
                    IsDownLoad = isDownLoad,
                    SavePath = filePath
                };
                if (!file.IsDownLoad)
                {
                    string fileDirPath = Path.GetDirectoryName(file.SavePath);
                    if (!Directory.Exists(fileDirPath))
                    {
                        Directory.CreateDirectory(fileDirPath);
                    }
                    try
                    {
                        WebClient client = new WebClient();
                        client.DownloadFileCompleted += client_DownloadFileCompleted;
                        client.DownloadProgressChanged += client_DownloadProgressChanged;
                        client.DownloadFileAsync(new Uri(file.Url), file.SavePath, file.FileName);
                    }
                    catch
                    {
                    }
                }
            }
            
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.UserState != null)
            {
                //下载完成
                this.label1.Content = "许可证下载完成";
                //解压文件
                ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\" + e.UserState.ToString(), AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\");
                //删除ZIP文件
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\" + e.UserState.ToString());
                //按钮可用
                this.NXLicence.IsEnabled = true;
                //运行许可证文件
                System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\NX_License_Servers_v1.0.0.exe");
                //进度条隐藏
                this.Prog.Visibility = Visibility.Hidden;
                //百分比隐藏
                this.label1.Visibility = Visibility.Hidden;
                this.label2.Visibility = Visibility.Hidden;
            }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.Prog.Minimum = 0;
            this.Prog.Maximum = (int)e.TotalBytesToReceive;
            this.Prog.Value = (int)e.BytesReceived;
            this.label1.Content = "许可证下载进度 : " + e.ProgressPercentage + "%"; 
            this.label2.Content = String.Format("{0}M/{1}M", Math.Round((double)e.BytesReceived / 1024 / 1024, 2), Math.Round((double)e.TotalBytesToReceive / 1024 / 1024, 2));
        }
    }
}

