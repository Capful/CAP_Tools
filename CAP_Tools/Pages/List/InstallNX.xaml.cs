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
using System.Runtime.InteropServices;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Threading;
using System.Text;
using System.ServiceProcess;

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
            //进度条隐藏
            this.Prog.Visibility = Visibility.Hidden;
            //百分比隐藏
            this.label1.Visibility = Visibility.Hidden;
            this.label2.Visibility = Visibility.Hidden;
            //测试按钮隐藏
            this.Test.Visibility = Visibility.Hidden;
            //许可证文件
            _saveDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NX License Servers");
            //许可证状态
            Licence_Status.Text = GetSystemServiceStatusString("Siemens PLM License Server");
            System.Timers.Timer Check = new System.Timers.Timer(1000);//每隔2秒执行一次，没用winfrom自带的
            Check.Elapsed += Check_Licence;//委托，要执行的方法
            Check.AutoReset = true;//获取该定时器自动执行
            Check.Enabled = true;//这个一定要写，要不然定时器不会执行的
        }



        public object DialogResult { get; private set; }
        private delegate void ThreadDelegate(); //申明一个专用来调用更改线程函数的委托

        private void XZ_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                //string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);   //获取桌面路径
                //dialog.InitialDirectory = dir;      //默认打开桌面
                IsFolderPicker = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string m_Dir = dialog.FileName;
                XZRoute.Text = m_Dir;
                NXRoute.Text = m_Dir;
                DirectoryInfo dir = new DirectoryInfo(m_Dir);
                foreach (FileInfo file in dir.GetFiles("UGII.cab", SearchOption.AllDirectories))//在文件夹中搜索UGII.cab；
                {
                    ///截取路径添加到变量
                    NXRoute.Text = Path.GetDirectoryName(Path.GetDirectoryName(file.FullName));
                    break;
                }
                DirectoryInfo dir2 = new DirectoryInfo(m_Dir);
                foreach (FileInfo file in dir.GetFiles("jt_catiav5.exe", SearchOption.AllDirectories))//在文件夹中搜索jt_catiav5.exe；
                {
                    ///截取路径添加到变量
                    PJRoute.Text = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(file.FullName))); ;
                    PRoute.Text = PJRoute.Text;
                    break;
                }
                ///判断安装哪个版本
                string nx10 = NXRoute.Text + "\\nx100";
                string nx11 = NXRoute.Text + "\\nx110";
                string nx12 = NXRoute.Text + "\\nx120";
                string nx121 = NXRoute.Text + "\\nx1201";
                string nx122 = NXRoute.Text + "\\nx1202";
                string nx1847 = NXRoute.Text + "\\nx";

                if (Directory.Exists(nx10))
                {
                    Title.Text = "- 正在安装 NX10.0";
                    Version.Text = "NX10";
                    NXInstall.IsEnabled = true;
                    NXCrack.IsEnabled = true;
                }
                else
                {
                    if (Directory.Exists(nx11))
                    {
                        Title.Text = "- 正在安装 NX11.0";
                        Version.Text = "NX11";
                        NXInstall.IsEnabled = true;
                    }
                    else
                    {
                        if (Directory.Exists(nx12))
                        {
                            Title.Text = "- 正在安装 NX12.0";
                            Version.Text = "NX12";
                            NXInstall.IsEnabled = true;
                        }
                        else
                        {
                            if (Directory.Exists(nx122))
                            {

                                Title.Text = "- 正在安装 NX12.0.2";
                                Version.Text = "NX12";
                                NXInstall.IsEnabled = true;
                            }
                            else
                            {
                                if (Directory.Exists(nx121))
                                {

                                    Title.Text = "- 正在安装 NX12.0.1";
                                    Version.Text = "NX12";
                                    NXInstall.IsEnabled = true;
                                }
                                else
                                {
                                    if (Directory.Exists(nx1847))
                                    {
                                        if (nx1847.Contains("1872"))
                                        {
                                            Title.Text = "- 正在安装 NX(1872系列)";
                                            Version.Text = "NX(1872系列)";
                                            NXInstall.IsEnabled = true;
                                        }
                                        else
                                        {
                                            Title.Text = "- 正在安装 NX(1847系列)";
                                            Version.Text = "NX(1847系列)";
                                            NXInstall.IsEnabled = true;
                                        }
                                    }
                                    else
                                    {
                                        NXInstall.IsEnabled = false;
                                        NXCrack.IsEnabled = false;
                                        Title.Text = "- 最新支持NX1872系列";
                                        ModernDialog.ShowMessage("NX安装主程序不存在，请重新选择文件夹或检测安装程序完整性！\n\r或者选择的安装包为NX10.0以下版本", "警告", MessageBoxButton.OK);
                                    }
                                }
                            }
                        }
                    }
                }
                ///判断破解文件是否存在
                if (NXInstall.IsEnabled == true)
                {
                    if (Directory.Exists(PJRoute.Text))
                    {
                        NXCrack.IsEnabled = true;
                    }
                    else
                    {
                        NXCrack.IsEnabled = false;
                        PRoute.Text = "未检测到破解文件，请解压到主程序目录后重试";
                        ModernDialog.ShowMessage("未在安装目录找到破解文件，请检查安装包是否包含破解文件。\n\r请将破解文件整个文件夹复制到安装目录后重试！", "警告", MessageBoxButton.OK);
                    }
                }
            }
            
        }

        private void NXInstall_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(NXRoute.Text);
            foreach (FileInfo file in dir.GetFiles("UGII.cab", SearchOption.AllDirectories))//在文件夹中搜索setup.exe；
            {
                Process.Start(Path.GetDirectoryName(file.FullName) + "\\setup.exe");//运行setup.exe
                break;
            }
        }

        private void NXCrack_Click(object sender, RoutedEventArgs e)
        {
            string Warning = "抱歉，您未安装" + Version.Text + "！请先安装" + Version.Text + "主程序后再进行破解文件";

            MessageBoxResult result = ModernDialog.ShowMessage("确定要破解" + Version.Text + " 吗？请确保NX软件都己经关闭。", "提示", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (Version.Text == "NX10")
                {
                    if (CheckNX10() == true)
                    {
                        ///使用BAT复制破解文件
                        PJFile("Unigraphics V28.0");
                    }
                    else
                    {
                        ModernDialog.ShowMessage(Warning, "警告", MessageBoxButton.OK);
                    }
                }
                else
                {
                    if (Version.Text == "NX11")
                    {
                        if (CheckNX11() == true)
                        {
                            ///使用BAT复制破解文件
                            PJFile("Unigraphics V29.0");
                        }
                        else
                        {
                            ModernDialog.ShowMessage(Warning, "警告", MessageBoxButton.OK);
                        }
                    }
                    else
                    {
                        if (Version.Text == "NX12")
                        {
                            if (CheckNX12() == true)
                            {
                                ///使用BAT复制破解文件
                                PJFile("Unigraphics V30.0");
                            }
                            else
                            {
                                ModernDialog.ShowMessage(Warning, "警告", MessageBoxButton.OK);
                            }
                        }
                        else
                        {
                            if (Version.Text == "NX(1847系列)")
                            {
                                if (CheckNX1847() == true)
                                {
                                    ///使用BAT复制破解文件
                                    PJFile("Unigraphics V31.0");
                                }
                                else
                                {
                                    ModernDialog.ShowMessage(Warning, "警告", MessageBoxButton.OK);
                                }
                            }
                            else
                            {
                                if (Version.Text == "NX(1872系列)")
                                {
                                    if (CheckNX1872() == true)
                                    {
                                        ///使用BAT复制破解文件
                                        PJFile("Unigraphics V32.0");
                                    }
                                    else
                                    {
                                        ModernDialog.ShowMessage(Warning, "警告", MessageBoxButton.OK);
                                    }
                                }
                                else
                                {
                                    ModernDialog.ShowMessage(Warning, "警告", MessageBoxButton.OK);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        ///  破解NX文件
        /// </summary>
        /// <param name="Versions">NX注册表版本,如"Unigraphics V28.0"</param>
        private void PJFile(string Version)
        {
            ///创建bat批处理文件
            string Path = "xcopy " + "\"" + PJRoute.Text + "\"" + " " + "\"" + GetNXPath(Version) + "\"" + " /c /e /r /y";
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "CopyFile.bat", Path, Encoding.Default);

            ///采用多线程运行bat复制文件
            Prog.Visibility = Visibility.Visible;  //进度条显示
            Prog.IsIndeterminate = true;  //切换进度条显示模式
            NXCrack.IsEnabled = false; //破解按钮不可选

            ///创建一个ThreadDelegate的实例，调用准备在后台运行的函数
            ThreadDelegate backWorkDel = new ThreadDelegate(CopyFile);

            ///使用异步的形式开始执行这个委托
            backWorkDel.BeginInvoke(null, null);
        }

        private bool CheckNX1872()
        {
            string NXPath = GetNXPath("Unigraphics V32.0");
            if (NXPath != null)
            {
                return true;
            }
            return false;
        }

        private bool CheckNX1847()
        {
            string NXPath = GetNXPath("Unigraphics V31.0");
            if (NXPath != null)
            {
                return true;
            }
            return false;
        }

        private bool CheckNX12()
        {
            string NXPath = GetNXPath("Unigraphics V30.0");
            if (NXPath != null)
            {
                return true;
            }
            return false;
        }

        private bool CheckNX11()
        {
            string NXPath = GetNXPath("Unigraphics V29.0");
            if (NXPath != null)
            {
                return true;
            }
            return false;
        }

        private bool CheckNX10()
        {
            string NXPath = GetNXPath("Unigraphics V28.0");
            if (NXPath != null)
            {
                return true;
            }
            return false;
        }


        private void NXLicence_Click(object sender, RoutedEventArgs e)
        {
            if (HttpFileExist("https://capful.oss-cn-beijing.aliyuncs.com/NX/NX%20License%20Servers%20v%202.3.1906.exe"))
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\NX License Servers v 2.3.1906.exe"))
                {
                    System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\NX License Servers v 2.3.1906.exe");
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
                    string imageUrl = "https://capful.oss-cn-beijing.aliyuncs.com/NX/NX%20License%20Servers%20v%202.3.1906.exe";
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
                            client.DownloadFileCompleted += Client_DownloadFileCompleted;
                            client.DownloadProgressChanged += Client_DownloadProgressChanged;
                            client.DownloadFileAsync(new Uri(file.Url), file.SavePath, file.FileName);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            else
            {
                ModernDialog.ShowMessage("网络连接失败，请检查网络！\n\r也可能是下载链接已失效，联系Capful", "警告", MessageBoxButton.OK);
            }
        }

        /// <summary>
        ///  判断远程文件是否存在
        /// </summary>
        /// <param name="fileUrl">文件URL</param>
        /// <returns>存在-true，不存在-false</returns>
        private bool HttpFileExist(string http_file_url)
        {
            WebResponse response = null;
            bool result = false;//下载结果
            try
            {
                response = WebRequest.Create(http_file_url).GetResponse();
                result = response == null ? false : true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return result;
        }

        void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.UserState != null)
            {
                //下载完成
                this.label1.Content = "许可证下载完成";
                //重命名文件
                File.Move(AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\" + e.UserState.ToString(), AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\NX License Servers v 2.3.1906.exe");
                //解压文件
                //ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\" + e.UserState.ToString(), AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\");
                //删除ZIP文件
                //File.Delete(AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\" + e.UserState.ToString());
                //按钮可用
                this.NXLicence.IsEnabled = true;
                //运行许可证文件
                System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\NX License Servers v 2.3.1906.exe");
                //进度条隐藏
                this.Prog.Visibility = Visibility.Hidden;
                //百分比隐藏
                this.label1.Visibility = Visibility.Hidden;
                this.label2.Visibility = Visibility.Hidden;
            }
        }

        void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.Prog.Minimum = 0;
            this.Prog.Maximum = (int)e.TotalBytesToReceive;
            this.Prog.Value = (int)e.BytesReceived;
            this.label1.Content = "许可证下载进度 : " + e.ProgressPercentage + "%"; 
            this.label2.Content = String.Format("{0}M/{1}M", Math.Round((double)e.BytesReceived / 1024 / 1024, 2), Math.Round((double)e.TotalBytesToReceive / 1024 / 1024, 2));
        }

        /// <summary>
        ///  利用BAT复制文件
        /// </summary>
        private void CopyFile()
        {
            RunBat(AppDomain.CurrentDomain.BaseDirectory + "CopyFile.bat"); //运行Bat文件
            ThreadDelegate changeTetBoxDel = delegate ()  //后台中要更改主线程中的UI，于是我们还是用委托来实现，再创建一个实例
            {
                Prog.IsIndeterminate = false;
                Prog.Visibility = Visibility.Hidden;
                NXCrack.IsEnabled = true;
                ModernDialog.ShowMessage(Version.Text + " 破解完成，请继续安装许可证！", "提示", MessageBoxButton.OK);
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "CopyFile.bat");
            };//要调用的过程
            this.Dispatcher.BeginInvoke(DispatcherPriority.Send, changeTetBoxDel); //使用分发器让这个委托等待执行
        }

        /// <summary>
        ///  获取NX安装路径
        /// </summary>
        /// <param name="Versions">NX注册表版本,如"Unigraphics V28.0"</param>
        private string GetNXPath(string Versions)
        {
            ///获取NX安装路径
            RegistryKey driverKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Unigraphics Solutions\Installed Applications");
            ///指定对应版本
            string EXE = (String)driverKey.GetValue(Versions);
            ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
            string Home = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@EXE)));
            return Home;
        }

        /// <summary>
        ///  运行BAT批处理文件
        /// </summary>
        /// <param name="filename">BAT文件名</param>
        private void RunBat(string filename)
        {
            Process pro = new Process();
            FileInfo file = new FileInfo(filename);
            pro.StartInfo.WorkingDirectory = file.Directory.FullName;
            pro.StartInfo.FileName = filename;
            pro.StartInfo.CreateNoWindow = false;
            pro.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pro.Start();
            pro.WaitForExit();
        }

        private void NXRoute_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(NXRoute.Text))
            {
                ///如果存在
                Process.Start(NXRoute.Text);
            }
        }

        private void PRoute_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(PRoute.Text))
            {
                ///如果存在
                Process.Start(PRoute.Text);
            }
        }

        /// <summary>
        /// 返回服务状态
        /// </summary>
        /// <param name="serviceName">系统服务名称</param>
        /// <returns>1:服务未运行 2:服务正在启动 3:服务正在停止 4:服务正在运行 5:服务即将继续 6:服务即将暂停 7:服务已暂停 0:未知状态</returns>
        public static string GetSystemServiceStatusString(string serviceName)
        {
            try
            {
                using (var control = new ServiceController(serviceName))
                {
                    var status = string.Empty;
                    switch ((int)control.Status)
                    {
                        case 1:
                            status = "服务未运行";
                            break;
                        case 2:
                            status = "服务正在启动...";
                            break;
                        case 3:
                            status = "服务正在停止...";
                            break;
                        case 4:
                            status = "服务正在运行";
                            break;
                        case 5:
                            status = "服务即将继续";
                            break;
                        case 6:
                            status = "服务即将暂停";
                            break;
                        case 7:
                            status = "服务已暂停";
                            break;
                        case 0:
                            status = "未知状态";
                            break;
                    }
                    return status;
                }
            }
            catch
            {
                return "未知状态";
            }
        }

        /// <summary>
        ///  多线程检查许可证状态并更新
        /// </summary>
        private void Check_Licence(object sender, System.Timers.ElapsedEventArgs e)
        {
            Licence_Status.Dispatcher.Invoke(
               new Action(
                    delegate
                    {
                        Licence_Status.Text = GetSystemServiceStatusString("Siemens PLM License Server");
                    }
               )
         );
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}

