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
            _saveDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NX License Servers");
        }



        public object DialogResult { get; private set; }

        private void XZ_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            //string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);   //获取桌面路径
            //dialog.InitialDirectory = dir;      //默认打开桌面
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string m_Dir = dialog.FileName;
                XZRoute.Text = m_Dir;
                this.NXRoute.Text = m_Dir;
                DirectoryInfo dir = new DirectoryInfo(m_Dir);
                foreach (FileInfo file in dir.GetFiles("UGII.cab", SearchOption.AllDirectories))//在文件夹中搜索UGII.cab；
                {
                    ///截取路径添加到变量
                    string NXRoute = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(file.FullName)));
                    this.NXRoute.Text = NXRoute;
                    break;
                }
                DirectoryInfo dir2 = new DirectoryInfo(m_Dir);
                foreach (FileInfo file in dir.GetFiles("jt_catiav5.exe", SearchOption.AllDirectories))//在文件夹中搜索jt_catiav5.exe；
                {
                    ///截取路径添加到变量
                    string PJNXRoute = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName((System.IO.Path.GetDirectoryName(file.FullName)))));
                    PJRoute.Text = PJNXRoute;
                    PRoute.Text = PJNXRoute;
                    break;
                }
                ///判断安装哪个版本
                string route = NXRoute.Text;
                string nx10 = route + "\\nx100";
                string nx11 = route + "\\nx110";
                string nx12 = route + "\\nx120";
                string nx121 = route + "\\nx1201";
                string nx122 = route + "\\nx1202";
                string nx1847 = route + "\\nx";

                if (Directory.Exists(nx10))
                {
                    this.a.Text = "- 正在安装 NX10.0";
                    this.Version.Text = "NX10";
                    this.NXInstall.IsEnabled = true;
                    this.NXCrack.IsEnabled = true;
                }
                else
                {
                    if (Directory.Exists(nx11))
                    {
                        this.a.Text = "- 正在安装 NX11.0";
                        this.Version.Text = "NX11";
                        this.NXInstall.IsEnabled = true;
                    }
                    else
                    {
                        if (Directory.Exists(nx12))
                        {
                            this.a.Text = "- 正在安装 NX12.0";
                            this.Version.Text = "NX12";
                            this.NXInstall.IsEnabled = true;
                        }
                        else
                        {
                            if (Directory.Exists(nx122))
                            {

                                this.a.Text = "- 正在安装 NX12.0.2";
                                this.Version.Text = "NX12";
                                this.NXInstall.IsEnabled = true;
                            }
                            else
                            {
                                if (Directory.Exists(nx121))
                                {

                                    this.a.Text = "- 正在安装 NX12.0.1";
                                    this.Version.Text = "NX12";
                                    this.NXInstall.IsEnabled = true;
                                }
                                else
                                {
                                    if (Directory.Exists(nx1847))
                                    {
                                        if (nx1847.Contains("1872"))
                                        {
                                            this.a.Text = "- 正在安装 NX(1872系列)";
                                            this.Version.Text = "NX(1872系列)";
                                            this.NXInstall.IsEnabled = true;
                                        }
                                        else
                                        {
                                            this.a.Text = "- 正在安装 NX(1847系列)";
                                            this.Version.Text = "NX(1847系列)";
                                            this.NXInstall.IsEnabled = true;
                                        }
                                    }
                                    else
                                    {
                                        this.NXInstall.IsEnabled = false;
                                        this.NXCrack.IsEnabled = false;
                                        this.a.Text = "- 最新支持NX1872系列";
                                        ModernDialog.ShowMessage("NX安装主程序不存在，请重新选择文件夹或检测安装程序完整性！\n\r或者选择的安装包为NX10.0以下版本", "警告", MessageBoxButton.OK);
                                    }
                                }
                            }
                        }
                    }
                }
                ///判断破解文件是否存在
                string pjfile = PJRoute.Text;
                if (NXInstall.IsEnabled == true)
                {
                    if (Directory.Exists(pjfile))
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
            string path1 = NXRoute.Text;
            DirectoryInfo dir = new DirectoryInfo(path1);
            foreach (FileInfo file in dir.GetFiles("UGII.cab", SearchOption.AllDirectories))//在文件夹中搜索setup.exe；
            {
                System.Diagnostics.Process.Start((System.IO.Path.GetDirectoryName(file.FullName)) + "\\setup.exe");//运行setup.exe
                break;
            }
        }

        private void NXCrack_Click(object sender, RoutedEventArgs e)
        {
            string version = Version.Text;
            string pjfile = PJRoute.Text;
            MessageBoxResult result = ModernDialog.ShowMessage("确定要破解" + version + " 吗？请确保NX软件都己经关闭。", "提示", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (version == "NX10")
                {
                    if (CheckNX10() == true)
                    {
                        string NXPath = GetNXPath("Unigraphics V28.0");
                        ///创建bat批处理文件
                        string Path = "xcopy " + "\"" + pjfile + "\"" + " " + "\"" + NXPath + "\"" + " /c /e /r /y";
                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "CopyFile.bat", Path, Encoding.Default);
                        ///采用多线程运行bat复制文件
                        Prog.Visibility = Visibility.Visible;  //进度条显示
                        Prog.IsIndeterminate = true;  //切换进度条显示模式
                        NXCrack.IsEnabled = false; //破解按钮不可选
                        ThreadDelegate backWorkDel = new ThreadDelegate(CopyFile); //创建一个ThreadDelegate的实例，调用准备在后台运行的函数
                        backWorkDel.BeginInvoke(null, null);//使用异步的形式开始执行这个委托
                    }
                    else
                    {
                        ModernDialog.ShowMessage("抱歉，您未安装" + version+ "！请先安装" + version + "主程序后再进行破解文件", "警告", MessageBoxButton.OK);
                    }
                }
                else
                {
                    if (version == "NX11")
                    {
                        if (CheckNX11() == true)
                        {
                            string NXPath = GetNXPath("Unigraphics V29.0");
                            ///创建bat批处理文件
                            string Path = "xcopy " + "\"" + pjfile + "\"" + " " + "\"" + NXPath + "\"" + " " + " /c /e /r /y";
                            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "CopyFile.bat", Path, Encoding.Default);
                            ///采用多线程运行bat复制文件
                            Prog.Visibility = Visibility.Visible;  //进度条显示
                            Prog.IsIndeterminate = true;  //切换进度条显示模式
                            NXCrack.IsEnabled = false; //破解按钮不可选
                            ThreadDelegate backWorkDel = new ThreadDelegate(CopyFile); //创建一个ThreadDelegate的实例，调用准备在后台运行的函数
                            backWorkDel.BeginInvoke(null, null);//使用异步的形式开始执行这个委托
                        }
                        else
                        {
                            ModernDialog.ShowMessage("抱歉，您未安装" + version + "！请先安装" + version + "主程序后再进行破解文件", "警告", MessageBoxButton.OK);
                        }
                    }
                    else
                    {
                        if (version == "NX12")
                        {
                            if (CheckNX12() == true)
                            {
                                string NXPath = GetNXPath("Unigraphics V30.0");
                                ///创建bat批处理文件
                                string Path = "xcopy " + "\"" + pjfile + "\"" + " " + "\"" + NXPath + "\"" + " " + " /c /e /r /y";
                                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "CopyFile.bat", Path, Encoding.Default);
                                ///采用多线程运行bat复制文件
                                Prog.Visibility = Visibility.Visible;  //进度条显示
                                Prog.IsIndeterminate = true;  //切换进度条显示模式
                                NXCrack.IsEnabled = false; //破解按钮不可选
                                ThreadDelegate backWorkDel = new ThreadDelegate(CopyFile); //创建一个ThreadDelegate的实例，调用准备在后台运行的函数
                                backWorkDel.BeginInvoke(null, null);//使用异步的形式开始执行这个委托
                            }
                            else
                            {
                                ModernDialog.ShowMessage("抱歉，您未安装" + version + "！请先安装" + version + "主程序后再进行破解文件", "警告", MessageBoxButton.OK);
                            }
                        }
                        else
                        {
                            if (version == "NX(1847系列)")
                            {
                                if (CheckNX1847() == true)
                                {
                                    string NXPath = GetNXPath("Unigraphics V31.0");
                                    ///创建bat批处理文件
                                    string Path = "xcopy " + "\"" + pjfile + "\"" + " " + "\"" + NXPath + "\"" + " " + " /c /e /r /y";
                                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "CopyFile.bat", Path, Encoding.Default);
                                    ///采用多线程运行bat复制文件
                                    Prog.Visibility = Visibility.Visible;  //进度条显示
                                    Prog.IsIndeterminate = true;  //切换进度条显示模式
                                    NXCrack.IsEnabled = false; //破解按钮不可选
                                    ThreadDelegate backWorkDel = new ThreadDelegate(CopyFile); //创建一个ThreadDelegate的实例，调用准备在后台运行的函数
                                    backWorkDel.BeginInvoke(null, null);//使用异步的形式开始执行这个委托
                                }
                                else
                                {
                                    ModernDialog.ShowMessage("抱歉，您未安装" + version + "！请先安装" + version + "主程序后再进行破解文件", "警告", MessageBoxButton.OK);
                                }
                            }
                            else
                            {
                                if (version == "NX(1872系列)")
                                {
                                    if (CheckNX1872() == true)
                                    {
                                        string NXPath = GetNXPath("Unigraphics V32.0");
                                        ///创建bat批处理文件
                                        string Path = "xcopy "+ "\"" + pjfile + "\"" + " " + "\"" + NXPath+ "\"" + " " + " /c /e /r /y";
                                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "CopyFile.bat", Path, Encoding.Default);
                                        ///采用多线程运行bat复制文件
                                        Prog.Visibility = Visibility.Visible;  //进度条显示
                                        Prog.IsIndeterminate = true;  //切换进度条显示模式
                                        NXCrack.IsEnabled = false; //破解按钮不可选
                                        ThreadDelegate backWorkDel = new ThreadDelegate(CopyFile); //创建一个ThreadDelegate的实例，调用准备在后台运行的函数
                                        backWorkDel.BeginInvoke(null, null);//使用异步的形式开始执行这个委托
                                    }
                                    else
                                    {
                                        ModernDialog.ShowMessage("抱歉，您未安装" + version + "！请先安装" + version + "主程序后再进行破解文件", "警告", MessageBoxButton.OK);
                                    }
                                }
                                else
                                {
                                    ModernDialog.ShowMessage("抱歉，您未安装" + version + "！请先安装" + version + "主程序后再进行破解文件", "警告", MessageBoxButton.OK);
                                }
                            }
                        }
                    }
                }
            }
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

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
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

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.Prog.Minimum = 0;
            this.Prog.Maximum = (int)e.TotalBytesToReceive;
            this.Prog.Value = (int)e.BytesReceived;
            this.label1.Content = "许可证下载进度 : " + e.ProgressPercentage + "%"; 
            this.label2.Content = String.Format("{0}M/{1}M", Math.Round((double)e.BytesReceived / 1024 / 1024, 2), Math.Round((double)e.TotalBytesToReceive / 1024 / 1024, 2));
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            Prog.Visibility = Visibility.Visible;
            Prog.IsIndeterminate = true;
            NXCrack.IsEnabled = false;
            ThreadDelegate backWorkDel = new ThreadDelegate(CopyFile); //创建一个ThreadDelegate的实例，调用准备在后台运行的函数
            backWorkDel.BeginInvoke(null, null);//使用异步的形式开始执行这个委托

        }


        private delegate void ThreadDelegate(); //申明一个专用来调用更改线程函数的委托

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
    }
}

